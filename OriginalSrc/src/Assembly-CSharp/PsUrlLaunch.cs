using System;
using System.Collections.Generic;
using DeepLink;
using UnityEngine;

// Token: 0x02000177 RID: 375
public static class PsUrlLaunch
{
	// Token: 0x06000C82 RID: 3202 RVA: 0x00076494 File Offset: 0x00074894
	public static string GetLevelLinkUrl(string _id, string _levelName, string _creatorName)
	{
		string text = string.Concat(new string[]
		{
			"'",
			_levelName,
			"' ",
			PsStrings.Get(StringID.CREATOR_TEXT),
			" '",
			_creatorName,
			"' "
		});
		return text + PsUrlLaunch.GetLevelLinkUrl(_id);
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x000764EE File Offset: 0x000748EE
	public static string GetLevelLinkUrl(string _id)
	{
		return "https://bbr.traplightgames.com/level/" + _id + "/";
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x00076500 File Offset: 0x00074900
	public static string GetUserLinkUrl(string _id)
	{
		return "https://bbr.traplightgames.com/user/" + _id + "/";
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x00076514 File Offset: 0x00074914
	public static bool ToSearchState()
	{
		bool delayedSearchState = PsUrlLaunch.m_delayedSearchState;
		PsUrlLaunch.m_delayedSearchState = false;
		return delayedSearchState;
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00076530 File Offset: 0x00074930
	public static void Launch()
	{
		string text = (string)global::DeepLink.Launch.GetObjectFromNotification("type");
		if (!string.IsNullOrEmpty(text))
		{
			PsMetrics.PushMessageLaunch(text);
		}
		PsUrlLaunch.m_url = global::DeepLink.Launch.GetLaunchUrl();
		if (string.IsNullOrEmpty(PsUrlLaunch.m_url))
		{
			string text2;
			try
			{
				text2 = (string)global::DeepLink.Launch.GetObjectFromNotification("level");
			}
			catch
			{
				text2 = null;
			}
			if (string.IsNullOrEmpty(text2))
			{
				return;
			}
			PsUrlLaunch.m_sharedLevel = text2;
		}
		else
		{
			Debug.Log("DeepLink: Had launch url: " + PsUrlLaunch.m_url, null);
		}
		if (string.IsNullOrEmpty(PsUrlLaunch.m_sharedLevel))
		{
			bool flag = false;
			for (int i = 0; i < PsUrlLaunch.m_baseLaunch.Length; i++)
			{
				if (PsUrlLaunch.m_url.Contains(PsUrlLaunch.m_baseLaunch[i]))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
		}
		bool flag2 = false;
		foreach (KeyValuePair<string, PlanetProgressionInfo> keyValuePair in PlanetTools.m_planetProgressionInfos)
		{
			if (keyValuePair.Value.GetMainPath().m_currentNodeId > 1)
			{
				flag2 = true;
				break;
			}
		}
		if (!flag2)
		{
			return;
		}
		if (PsUrlLaunch.HandleUrl() || !string.IsNullOrEmpty(PsUrlLaunch.m_sharedLevel))
		{
			PsMetrics.OpenedFromLink(PsUrlLaunch.m_sharedLevel);
			IScene currentScene = Main.m_currentGame.m_sceneManager.GetCurrentScene();
			ILoadingScene loadingScene = Main.m_currentGame.m_sceneManager.m_loadingScene;
			IState state = ((currentScene != null) ? currentScene.GetCurrentState() : null);
			Debug.Log("DeepLink: Current scene: " + ((currentScene != null) ? currentScene.m_name : "null"), null);
			Debug.Log("DeepLink: Current loadingScene: " + ((loadingScene != null) ? loadingScene.ToString() : "null"), null);
			Debug.Log("DeepLink: Current state: " + ((state != null) ? state.ToString() : "null"), null);
			if (currentScene is PsMenuScene && PsState.m_activeGameLoop == null)
			{
				Debug.Log("DeepLink: MenuScene: No active loop", null);
				PsUrlLaunch.SetSearchParameters();
				if (PsMainMenuState.m_popup != null)
				{
					PsMainMenuState.m_popup.Destroy();
					PsMainMenuState.m_popup = null;
				}
				PsMainMenuState.ChangeToCreateState();
			}
			else if ((state != null && state is UserLoginState) || (loadingScene != null && loadingScene is PsRatingLoadingScene))
			{
				Debug.Log("DeepLink: delayed search", null);
				PsUrlLaunch.m_delayedSearchState = true;
				PsUrlLaunch.SetSearchParameters();
			}
			else if (loadingScene != null && loadingScene is PsRacingLoadingScene)
			{
				Debug.Log("DeepLink: Loading level.", null);
				PsUrlLaunch.m_waitingForLevelLoad = true;
			}
			else if (loadingScene == null && (currentScene is GameScene || currentScene is EditorScene) && PsState.m_activeGameLoop != null)
			{
				PsUrlLaunch.CreatePopup(null);
			}
		}
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x00076848 File Offset: 0x00074C48
	public static void CreatePopup(Action _destroyAction = null)
	{
		PsUrlLaunch.CreateProceedPopup(delegate
		{
			PsUrlLaunch.SetSearchParameters();
			IState state = new PsTransitionSearchState();
			PsMenuScene.m_lastIState = state;
			PsMenuScene.m_lastState = null;
			if (PsState.m_activeGameLoop is PsGameLoopEditor)
			{
				(PsState.m_activeGameLoop as PsGameLoopEditor).ExitEditor(false, delegate
				{
					Debug.LogError("Custom exit action");
					Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLevelEndLoadingScene(Color.black, PsState.m_activeGameLoop, 0.25f));
				});
			}
			else
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLevelEndLoadingScene(Color.black, PsState.m_activeGameLoop, 0.25f));
			}
		}, _destroyAction);
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x00076870 File Offset: 0x00074C70
	public static bool IsWatingLevelLoad()
	{
		bool waitingForLevelLoad = PsUrlLaunch.m_waitingForLevelLoad;
		PsUrlLaunch.m_waitingForLevelLoad = false;
		return waitingForLevelLoad;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0007688C File Offset: 0x00074C8C
	private static bool HandleUrl()
	{
		if (string.IsNullOrEmpty(PsUrlLaunch.m_url))
		{
			return false;
		}
		bool flag = false;
		if (PsUrlLaunch.m_url.Contains("level/"))
		{
			PsUrlLaunch.m_sharedLevel = PsUrlLaunch.GetLaunchValue("level/", PsUrlLaunch.m_url);
			Debug.Log("DeepLink: sharedlevel: " + PsUrlLaunch.m_sharedLevel, null);
			flag = true;
		}
		else if (PsUrlLaunch.m_url.Contains("user/"))
		{
			PsUrlLaunch.m_sharedUser = PsUrlLaunch.GetLaunchValue("user/", PsUrlLaunch.m_url);
			Debug.Log("DeepLink: sharedUser: " + PsUrlLaunch.m_sharedUser, null);
			flag = true;
		}
		PsUrlLaunch.m_url = string.Empty;
		return flag;
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x0007693C File Offset: 0x00074D3C
	private static string GetLaunchValue(string _parameter, string _launchUrl)
	{
		if (!_launchUrl.Contains(_parameter))
		{
			return string.Empty;
		}
		string text = _launchUrl.Substring(_launchUrl.LastIndexOf(_parameter) + _parameter.Length);
		if (text.Contains("/"))
		{
			text = text.Substring(0, text.IndexOf("/"));
		}
		return text;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x00076994 File Offset: 0x00074D94
	private static void SetSearchParameters()
	{
		PsUICenterSearch.m_sharedLevel = PsUrlLaunch.m_sharedLevel;
		PsUICenterSearch.m_sharedUser = PsUrlLaunch.m_sharedUser;
		PsUITabbedCreate.m_selectedTab = 3;
		PsUrlLaunch.CleanSearchParameters();
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x000769B5 File Offset: 0x00074DB5
	private static void CleanSearchParameters()
	{
		PsUrlLaunch.m_sharedLevel = string.Empty;
		PsUrlLaunch.m_sharedUser = string.Empty;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x000769CC File Offset: 0x00074DCC
	private static void CreateProceedPopup(Action _proceed, Action _destroyAction = null)
	{
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterOpenSharedContent), null, null, null, true, true, InitialPage.Center, false, false, false);
		popup.SetAction("Proceed", delegate
		{
			popup.Destroy();
			Debug.Log("DeepLink: Go to search state", null);
			_proceed.Invoke();
		});
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
			if (_destroyAction != null)
			{
				_destroyAction.Invoke();
			}
		});
	}

	// Token: 0x04000BFD RID: 3069
	public const string m_sharingDomain = "https://bbr.traplightgames.com/";

	// Token: 0x04000BFE RID: 3070
	private static readonly string[] m_baseLaunch = new string[] { "traplightgames.com", "bigbangracing://" };

	// Token: 0x04000BFF RID: 3071
	private const string m_levelParameter = "level/";

	// Token: 0x04000C00 RID: 3072
	private const string m_levelParameterForSharing = "level/";

	// Token: 0x04000C01 RID: 3073
	private const string m_userParameter = "user/";

	// Token: 0x04000C02 RID: 3074
	private static string m_url;

	// Token: 0x04000C03 RID: 3075
	private static string m_sharedLevel;

	// Token: 0x04000C04 RID: 3076
	private static string m_sharedUser;

	// Token: 0x04000C05 RID: 3077
	private static bool m_delayedSearchState = false;

	// Token: 0x04000C06 RID: 3078
	private static bool m_waitingForLevelLoad = false;

	// Token: 0x02000178 RID: 376
	private class SharedLevelBehaviour
	{
	}
}
