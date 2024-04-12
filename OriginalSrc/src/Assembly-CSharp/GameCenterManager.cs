using System;
using System.Collections.Generic;
using Server;
using UnityEngine;
using UnityEngine.SocialPlatforms;

// Token: 0x02000445 RID: 1093
public static class GameCenterManager
{
	// Token: 0x06001E50 RID: 7760 RVA: 0x0015770C File Offset: 0x00155B0C
	public static void Logout(Action _GCLogoutComplete)
	{
		Debug.Log("LOGGING OUT OF GAME CENTER", null);
		PlayerPrefsX.DeleteKey("GameCenterId");
		PlayerPrefsX.DeleteKey("GameCenterName");
		GameCenterManager.m_gcCallbackHandled = false;
		GameCenterManager.disableGcLogin = true;
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x0015773C File Offset: 0x00155B3C
	public static void CheckAccountChange()
	{
		string gameCenterId = PlayerPrefsX.GetGameCenterId();
		if (Social.localUser.authenticated && !GameCenterManager.disableGcLogin)
		{
			if (!string.IsNullOrEmpty(gameCenterId))
			{
				if (Social.localUser.id != PlayerPrefsX.GetGameCenterId())
				{
					GameCenterManager.CheckAccount(new Action<HttpC>(GameCenterManager.GcLoginSendOk), new Action<HttpC>(GameCenterManager.GcLoginSendFail), false);
				}
				else
				{
					GameCenterManager.LoadFriends();
				}
			}
			else
			{
				GameCenterManager.CheckAccount(new Action<HttpC>(GameCenterManager.GcLoginSendOk), new Action<HttpC>(GameCenterManager.GcLoginSendFail), true);
			}
		}
	}

	// Token: 0x06001E52 RID: 7762 RVA: 0x0015781C File Offset: 0x00155C1C
	public static void GCLoginCallback(bool _success)
	{
		Debug.Log("GC Login returned to callback.", null);
		if (GameCenterManager.m_gcCallbackHandled)
		{
			Debug.LogWarning("GC CALLBACK CANCELLED, LOGIN SUCCESS: " + _success);
			return;
		}
		if (_success)
		{
			Debug.Log("GC Login successful", null);
			GameCenterManager.CheckAccountChange();
		}
		else
		{
			Debug.Log("GC Login failed", null);
		}
		GameCenterManager.m_gcCallbackHandled = true;
	}

	// Token: 0x06001E53 RID: 7763 RVA: 0x00157880 File Offset: 0x00155C80
	public static void Login()
	{
		Debug.Log("CALLED GC LOGIN.", null);
		Debug.Log(GameCenterManager.disableGcLogin, null);
		if (!GameCenterManager.disableGcLogin)
		{
			if (!Social.localUser.authenticated)
			{
				Debug.Log("GC Starting to authenticate...", null);
				Social.localUser.Authenticate(new Action<bool>(GameCenterManager.GCLoginCallback));
			}
			else
			{
				Debug.Log("GC already logged in.", null);
				GameCenterManager.m_gcCallbackHandled = true;
				GameCenterManager.CheckAccountChange();
			}
		}
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x00157910 File Offset: 0x00155D10
	public static void LoadFriends()
	{
		if (!PsState.m_restarting && Social.localUser.authenticated && !string.IsNullOrEmpty(PlayerPrefsX.GetGameCenterId()) && !GameCenterManager.disableGcLogin)
		{
			Debug.Log("Starting GC Friend loading", null);
			Social.localUser.LoadFriends(delegate(bool success)
			{
				if (success)
				{
					Player.SendSocialFriends("gamecenter", Social.localUser.id, GameCenterManager.GenerateGCFriendArray(), new Action<HttpC>(GameCenterManager.FriendSendOk), new Action<HttpC>(GameCenterManager.FriendSendFail), null);
				}
				else
				{
					Debug.Log("GC Friends not loaded, trying on next login...", null);
				}
			});
		}
	}

	// Token: 0x06001E55 RID: 7765 RVA: 0x00157984 File Offset: 0x00155D84
	public static void CheckAccount(Action<HttpC> _success, Action<HttpC> _fail, bool autoJoin = true)
	{
		string[] array = new string[0];
		if (Social.localUser.authenticated)
		{
			Player.CheckSocialLogin("gamecenter", Social.localUser.id, array, autoJoin, _success, _fail, null, true);
		}
		else
		{
			Debug.Log("GameCenter info send to server fail: Not authenticated to GC!", null);
		}
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x001579D1 File Offset: 0x00155DD1
	private static void GcLoginSendOk(HttpC _c)
	{
	}

	// Token: 0x06001E57 RID: 7767 RVA: 0x001579D3 File Offset: 0x00155DD3
	private static void RestartWithNewAccount()
	{
		ResetFlow.Start(false);
	}

	// Token: 0x06001E58 RID: 7768 RVA: 0x001579DC File Offset: 0x00155DDC
	private static void RestartWithExistingId(string _id)
	{
		string[] array = new string[0];
		string userId = PlayerPrefsX.GetUserId();
		Player.ResolveSocialLogin("gamecenter", Social.localUser.id, _id, userId, array, new Action<HttpC>(GameCenterManager.ResolveOk), new Action<HttpC>(GameCenterManager.ResolveFail), null);
		PsState.m_restarting = true;
		PlayerPrefsX.SetUserId(_id);
		PlayerPrefsX.SetGameCenterId(Social.localUser.id);
		if (PsMetagameManager.m_serverQueueItems != null)
		{
			for (int i = 0; i < PsMetagameManager.m_serverQueueItems.Count; i++)
			{
				PsMetagameManager.m_serverQueueItems[i] = null;
			}
			PsMetagameManager.m_serverQueueItems.Clear();
		}
		Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), new FadeLoadingScene(Color.black, true, 0.25f));
	}

	// Token: 0x06001E59 RID: 7769 RVA: 0x00157AC8 File Offset: 0x00155EC8
	private static void TransferGCToCurrentId(string _oldId)
	{
		string[] array = new string[0];
		Player.ResolveSocialLogin("gamecenter", Social.localUser.id, PlayerPrefsX.GetUserId(), _oldId, array, new Action<HttpC>(GameCenterManager.ResolveOk), new Action<HttpC>(GameCenterManager.ResolveFail), null);
	}

	// Token: 0x06001E5A RID: 7770 RVA: 0x00157B32 File Offset: 0x00155F32
	private static void DisconnectGC()
	{
		GameCenterManager.Logout(delegate
		{
			Debug.Log("Disconnected user from GC", null);
		});
	}

	// Token: 0x06001E5B RID: 7771 RVA: 0x00157B56 File Offset: 0x00155F56
	private static void ResolveOk(HttpC _c)
	{
		GameCenterManager.LoadFriends();
	}

	// Token: 0x06001E5C RID: 7772 RVA: 0x00157B5D File Offset: 0x00155F5D
	private static void ResolveFail(HttpC _c)
	{
		Debug.Log("Social resolve failed.", null);
	}

	// Token: 0x06001E5D RID: 7773 RVA: 0x00157B6A File Offset: 0x00155F6A
	private static void FriendSendOk(HttpC _c)
	{
		Debug.Log("GC friends sent to server", null);
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x00157B78 File Offset: 0x00155F78
	private static void GcLoginSendFail(HttpC _c)
	{
		Dictionary<string, string> responseHeaders = _c.www.responseHeaders;
		if (responseHeaders["PLAY_ERROR"] == "SOCIAL_ERROR")
		{
			GameCenterManager.disableGcLogin = true;
		}
		Debug.Log("GC login send to server fail", null);
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x00157BBC File Offset: 0x00155FBC
	private static void FriendSendFail(HttpC _c)
	{
		Debug.Log("GC friend send to server fail", null);
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x00157BCC File Offset: 0x00155FCC
	private static string[] GenerateGCFriendArray()
	{
		string[] array = new string[Social.localUser.friends.Length];
		for (int i = Social.localUser.friends.Length - 1; i > -1; i--)
		{
			array[i] = Social.localUser.friends[i].id;
		}
		return array;
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x00157C1F File Offset: 0x0015601F
	public static bool IsLoggedIn()
	{
		return PlayerPrefsX.GetGameCenterId() != null;
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x00157C2C File Offset: 0x0015602C
	public static void GetPicture(string _gameCenterId, Action<Texture2D> _callback)
	{
		if (Social.localUser.authenticated)
		{
			if (_gameCenterId == PlayerPrefsX.GetGameCenterId())
			{
				_callback.Invoke(Social.localUser.image);
			}
			else
			{
				GameCenterManager.GameCenterPictureDownloader gameCenterPictureDownloader = new GameCenterManager.GameCenterPictureDownloader(_gameCenterId);
				GameCenterManager.GameCenterPictureDownloader gameCenterPictureDownloader2 = gameCenterPictureDownloader;
				gameCenterPictureDownloader2.m_callback = (Action<Texture2D>)Delegate.Combine(gameCenterPictureDownloader2.m_callback, _callback);
			}
		}
	}

	// Token: 0x0400219D RID: 8605
	public static bool disableGcLogin;

	// Token: 0x0400219E RID: 8606
	public static bool m_gcCallbackHandled;

	// Token: 0x02000446 RID: 1094
	public class GameCenterPictureDownloader
	{
		// Token: 0x06001E66 RID: 7782 RVA: 0x00157D14 File Offset: 0x00156114
		public GameCenterPictureDownloader(string _gameCenterId)
		{
			Social.LoadUsers(new string[] { _gameCenterId }, new Action<IUserProfile[]>(this.ImageLoaded));
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x00157D44 File Offset: 0x00156144
		private void ImageLoaded(IUserProfile[] users)
		{
			this.m_callback.Invoke(users[0].image);
		}

		// Token: 0x040021AC RID: 8620
		public Action<Texture2D> m_callback;
	}
}
