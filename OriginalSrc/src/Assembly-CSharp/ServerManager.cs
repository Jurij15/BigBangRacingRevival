using System;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x02000441 RID: 1089
public static class ServerManager
{
	// Token: 0x06001E16 RID: 7702 RVA: 0x00156014 File Offset: 0x00154414
	public static HttpC Login()
	{
		if (!PsState.m_inLoginFlow && !PsState.m_inIapFlow)
		{
			PsState.m_inLoginFlow = true;
			PsState.m_sessionId = PlayerPrefsX.GetSession();
			ServerManager.m_playerAuthenticated = false;
			ServerManager.AddConnectingPopup();
			Debug.LogWarning("LOGIN");
			return LoginFlow.Start(new Action<HttpC>(ServerManager.LoginOk), new Action<HttpC>(ServerManager.WoeLoginFailed), null);
		}
		Debug.LogWarning("Already logging in");
		return null;
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x001560A8 File Offset: 0x001544A8
	private static void LoginOk(HttpC _c)
	{
		PushMessageManager.RegisterForNotifications();
		ServerManager.RemoveConnectingPopup();
		PsMetagameManager.m_queueWaitTicks = 5;
		ServerManager.m_playerAuthenticated = true;
		PsState.m_inLoginFlow = false;
		PsMetagameManager.InitNewEditorItems();
		if (PsState.m_activeMinigame != null)
		{
			if (!PsMetagameManager.m_playerStats.copperReset)
			{
				PsMetagameManager.m_playerStats.copper += PsState.m_activeMinigame.m_collectedCopper;
			}
			else
			{
				PsMetagameManager.m_playerStats.copper = PsState.m_activeMinigame.m_collectedCopper;
			}
			if (!PsMetagameManager.m_playerStats.shardReset)
			{
				PsMetagameManager.m_playerStats.shards += PsState.m_activeMinigame.m_collectedShards;
			}
			else
			{
				PsMetagameManager.m_playerStats.shards = PsState.m_activeMinigame.m_collectedShards;
			}
			PsMetagameManager.m_playerStats.coins += PsState.m_activeMinigame.m_collectedCoins;
			PsMetagameManager.m_playerStats.diamonds += PsState.m_activeMinigame.m_collectedDiamonds;
			if (PsState.m_activeGameLoop is PsGameLoopTournament)
			{
				PsMetagameManager.m_playerStats.tournamentBoosters -= Mathf.Abs(PsState.m_activeMinigame.m_usedBoosters);
			}
			else
			{
				PsMetagameManager.m_playerStats.boosters -= Mathf.Abs(PsState.m_activeMinigame.m_usedBoosters);
			}
		}
		else if (PsState.m_activeGameLoop == null && Main.m_currentGame.m_currentScene.GetCurrentState() is PsUIBaseState && (Main.m_currentGame.m_currentScene.GetCurrentState() as PsUIBaseState).m_baseCanvas.m_mainContent is PsUITabbedJoinedTeam && !string.IsNullOrEmpty(PsMetagameManager.m_playerStats.m_teamKickReason))
		{
			if (PsUITabbedTeam.m_selectedTab == 1 || PsUITabbedTeam.m_selectedTab == 4)
			{
				for (int i = PsState.m_openPopups.Count - 1; i >= 0; i--)
				{
					PsState.m_openPopups[i].Destroy();
				}
			}
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
		}
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x001562D8 File Offset: 0x001546D8
	private static void WoeLoginFailed(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), delegate
		{
			PsState.m_inLoginFlow = false;
			ServerManager.m_playerAuthenticated = false;
			return ServerManager.Login();
		}, null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x00156327 File Offset: 0x00154727
	public static void ThrowServerErrorException(string _title, WWW _www, Func<HttpC> _httpcCallBack = null, Action _actionCallBack = null)
	{
		ServerManager.ThrowServerErrorException(_title, ServerErrors.GetNetworkError(_www.error), _httpcCallBack, _actionCallBack, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x00156344 File Offset: 0x00154744
	public static void ThrowServerErrorException(string _title, string _message, Func<HttpC> _httpcCallBack = null, Action _actionCallBack = null, StringID _proceedText = StringID.TRY_AGAIN_SERVER)
	{
		ServerManager.RemoveConnectingPopup();
		PsMetagameManager.m_queueWaitTicks = 5;
		ServerManager.m_retryCallbacks.Add(new ServerManager.PsRetryCallBack(_httpcCallBack, _actionCallBack));
		ServerManager.CreateServerErrorPopup(_title, _message, PsStrings.Get(_proceedText), delegate
		{
			ServerManager.ServerErrorButtonClicked(null);
		});
		Debug.LogStackTrace();
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x001563A0 File Offset: 0x001547A0
	public static void CreateServerErrorPopup(string _title, string _message, string _proceedText, Action _proceedAction)
	{
		Debug.LogWarning(_title + ": " + _message);
		if (!ServerManager.m_hasAlertViewOnScreen)
		{
			HttpS.m_gatesFreeze = true;
			TouchAreaS.CancelAllTouches(null);
			if (TouchAreaS.m_disabled)
			{
				ServerManager.touchesWereDisabled = true;
				TouchAreaS.Enable();
			}
			if (PsState.m_canAutoPause)
			{
				PsState.m_activeGameLoop.PauseMinigame();
				Main.m_currentGame.m_currentScene.m_stateMachine.Update();
				Debug.Log("Game autopaused.", null);
			}
			PsUIBasePopup psUIBasePopup = new PsUIBasePopup(typeof(PsUIErrorPopup), null, null, null, false, true, InitialPage.Center, false, false, false);
			psUIBasePopup.SetAction("Proceed", _proceedAction);
			(psUIBasePopup.m_mainContent as PsUIErrorPopup).CreateHeader(_title);
			(psUIBasePopup.m_mainContent as PsUIErrorPopup).CreateContent(_message, _proceedText);
			EntityManager.RemoveAllTagsFromEntity(psUIBasePopup.m_TC.p_entity, true);
			psUIBasePopup.Update();
			ServerManager.m_errorPopup = psUIBasePopup;
			ServerManager.m_hasAlertViewOnScreen = true;
		}
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x00156488 File Offset: 0x00154888
	private static void RemoveServerErrorPopup()
	{
		if (ServerManager.m_hasAlertViewOnScreen)
		{
			Debug.Log("Removing server error popup", null);
			ServerManager.m_errorPopup.Destroy();
			ServerManager.m_errorPopup = null;
			ServerManager.m_hasAlertViewOnScreen = false;
			HttpS.m_gatesFreeze = false;
			if (ServerManager.touchesWereDisabled && Main.m_currentGame.m_sceneManager.m_loadingScene != null)
			{
				TouchAreaS.Disable();
				ServerManager.touchesWereDisabled = false;
			}
			else
			{
				TouchAreaS.Enable();
				ServerManager.touchesWereDisabled = false;
			}
		}
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x001564FF File Offset: 0x001548FF
	private static void ServerErrorButtonClicked(string _string)
	{
		ServerManager.RemoveServerErrorPopup();
		ServerManager.AddConnectingPopup();
		ServerManager.ExecuteRetryCallBacks();
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x00156510 File Offset: 0x00154910
	public static void ClearRetryCallBacks()
	{
		ServerManager.RemoveServerErrorPopup();
		ServerManager.m_retryCallbacks.Clear();
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x00156524 File Offset: 0x00154924
	public static void ExecuteRetryCallBacks()
	{
		if (ServerManager.m_retryCallbacks.Count > 0)
		{
			for (int i = 0; i < ServerManager.m_retryCallbacks.Count; i++)
			{
				if (ServerManager.m_retryCallbacks[i].m_httpcCallBack != null)
				{
					Debug.Log("Calling retry function: " + ServerManager.m_retryCallbacks[i].m_httpcCallBack.Method.Name, null);
					HttpC httpC = ServerManager.m_retryCallbacks[i].m_httpcCallBack.Invoke();
					ServerManager.m_httpcErrorList.Add(httpC);
				}
				if (ServerManager.m_retryCallbacks[i].m_actionCallBack != null)
				{
					ServerManager.m_retryCallbacks[i].m_actionCallBack.Invoke();
				}
				ServerManager.m_retryCallbacks[i] = null;
			}
			ServerManager.m_retryCallbacks.Clear();
		}
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x001565FC File Offset: 0x001549FC
	public static void AddConnectingPopup()
	{
		if (!ServerManager.m_dontShowLoginPopup)
		{
			Debug.Log("Adding connecting popup", null);
			if (ServerManager.m_connectingPopup == null)
			{
				TouchAreaS.CancelAllTouches(null);
				TouchAreaS.Disable();
				ServerManager.m_connectingPopup = new PsUIBasePopup(typeof(PsUIBlockingMessagePopup), null, null, null, false, true, InitialPage.Center, false, false, false);
				(ServerManager.m_connectingPopup.m_mainContent as PsUIBlockingMessagePopup).SetText(PsStrings.Get(StringID.CONNECTING));
			}
			else
			{
				Debug.Log("Already has connecting popup.", null);
			}
			if (PsState.m_canAutoPause)
			{
				PsState.m_activeGameLoop.PauseMinigame();
			}
		}
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x0015668F File Offset: 0x00154A8F
	public static void RemoveConnectingPopup()
	{
		if (ServerManager.m_connectingPopup != null)
		{
			Debug.Log("Removing connecting popup", null);
			TouchAreaS.Enable();
			ServerManager.m_connectingPopup.Destroy();
			ServerManager.m_connectingPopup = null;
		}
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x001566BB File Offset: 0x00154ABB
	public static void ThrowOldVersionMessage(string _message)
	{
		ServerManager.RemoveConnectingPopup();
		ServerManager.CreateServerErrorPopup(PsStrings.Get(StringID.UPDATE_POPUP), _message, PsStrings.Get(StringID.GET_IT), new Action(ServerManager.UpdateButtonClicked));
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x001566F6 File Offset: 0x00154AF6
	private static void UpdateButtonClicked()
	{
		if (Application.platform == 8 || Application.platform == 11)
		{
			Application.OpenURL("market://details?id=com.traplight.bigbangracing");
		}
		else
		{
			Debug.LogError("RAISE YOUR VERSION NUMBER FROM THE TL/BuildEditor menu, for example: 2.0.0");
		}
	}

	// Token: 0x04002168 RID: 8552
	public static bool m_hasAlertViewOnScreen = false;

	// Token: 0x04002169 RID: 8553
	public static bool m_playerAuthenticated = false;

	// Token: 0x0400216A RID: 8554
	public static bool m_sendDeviceToken = false;

	// Token: 0x0400216B RID: 8555
	private static Action m_buttonPressedAction;

	// Token: 0x0400216C RID: 8556
	public static PsUIBasePopup m_connectingPopup;

	// Token: 0x0400216D RID: 8557
	private static PsUIBasePopup m_errorPopup;

	// Token: 0x0400216E RID: 8558
	public static bool m_dontShowLoginPopup = false;

	// Token: 0x0400216F RID: 8559
	private static PlayerData[] m_mergePlayersData;

	// Token: 0x04002170 RID: 8560
	private static bool touchesWereDisabled = false;

	// Token: 0x04002171 RID: 8561
	private static List<ServerManager.PsRetryCallBack> m_retryCallbacks = new List<ServerManager.PsRetryCallBack>();

	// Token: 0x04002172 RID: 8562
	public static List<HttpC> m_httpcErrorList = new List<HttpC>();

	// Token: 0x02000442 RID: 1090
	public class PsRetryCallBack
	{
		// Token: 0x06001E27 RID: 7719 RVA: 0x00156777 File Offset: 0x00154B77
		public PsRetryCallBack(Func<HttpC> _httpcCallBack = null, Action _actionCallBack = null)
		{
			this.m_httpcCallBack = _httpcCallBack;
			this.m_actionCallBack = _actionCallBack;
		}

		// Token: 0x04002178 RID: 8568
		public Func<HttpC> m_httpcCallBack;

		// Token: 0x04002179 RID: 8569
		public Action m_actionCallBack;
	}
}
