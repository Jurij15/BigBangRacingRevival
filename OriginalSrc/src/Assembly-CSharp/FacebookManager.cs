using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using MiniJSON;
using Server;
using UnityEngine;

// Token: 0x02000444 RID: 1092
public static class FacebookManager
{
	// Token: 0x06001E2B RID: 7723 RVA: 0x00156828 File Offset: 0x00154C28
	public static void Login(Action _completeCallback)
	{
		if (!FacebookManager.m_disableFBLogin)
		{
			Debug.Log("FB Login start", null);
			FacebookManager.m_completeCallback = _completeCallback;
			if (FacebookManager.m_initComplete)
			{
				FacebookManager.OnFBInitComplete();
			}
			else
			{
				FB.Init(new InitDelegate(FacebookManager.OnFBInitComplete), null, null);
			}
		}
		else
		{
			_completeCallback.Invoke();
		}
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x00156893 File Offset: 0x00154C93
	public static void Initialize()
	{
		FB.Init(new InitDelegate(FacebookManager.InitComplete), null, null);
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x001568B9 File Offset: 0x00154CB9
	private static void InitComplete()
	{
		FB.ActivateApp();
		FacebookManager.m_initComplete = true;
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x001568C6 File Offset: 0x00154CC6
	public static void ReturnedFromBackground()
	{
		if (FB.IsInitialized)
		{
			FacebookManager.InitComplete();
		}
		else
		{
			FacebookManager.Initialize();
		}
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x001568E4 File Offset: 0x00154CE4
	public static void SendTutorialComplete(bool _complete)
	{
		if (_complete)
		{
			FB.LogAppEvent("fb_mobile_tutorial_completion", default(float?), null);
		}
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x0015690C File Offset: 0x00154D0C
	public static void SendToServer(FBUserData _fbUserData)
	{
		Player.CheckSocialLogin("facebook", _fbUserData.id, FacebookManager.m_friends, true, new Action<HttpC>(FacebookManager.FbLoginSendOk), new Action<HttpC>(FacebookManager.FbLoginSendFail), null, true);
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x0015696C File Offset: 0x00154D6C
	public static void CheckAccountChange()
	{
		Debug.Log("CheckAccountChange", null);
		string facebookId = PlayerPrefsX.GetFacebookId();
		if (FB.IsLoggedIn)
		{
			if (!string.IsNullOrEmpty(facebookId))
			{
				if (FacebookManager.m_userData.id != facebookId)
				{
					FacebookManager.CheckAccount(new Action<HttpC>(FacebookManager.FbLoginSendOk), new Action<HttpC>(FacebookManager.FbLoginSendFail), false);
				}
				else
				{
					FacebookManager.LoadFriends(true);
				}
			}
			else
			{
				FacebookManager.CheckAccount(new Action<HttpC>(FacebookManager.FbLoginSendOk), new Action<HttpC>(FacebookManager.FbLoginSendFail), true);
			}
		}
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x00156A44 File Offset: 0x00154E44
	public static void CheckAccount(Action<HttpC> _success, Action<HttpC> _fail, bool autoJoin = true)
	{
		if (FB.IsLoggedIn)
		{
			Player.CheckSocialLogin("facebook", FacebookManager.m_userData.id, FacebookManager.m_friends, autoJoin, _success, _fail, null, false);
		}
		else
		{
			Debug.Log("Facebook info send to server fail: Not authenticated to FB!", null);
		}
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x00156A80 File Offset: 0x00154E80
	private static void CreateSyncPopup(string _existingId)
	{
		FacebookManager.m_syncPopup = new PsUIBasePopup(typeof(PsUIPopupSyncFacebook), null, null, null, false, true, InitialPage.Center, false, false, false);
		PsState.m_openSocialPopup = FacebookManager.m_syncPopup;
		FacebookManager.m_syncPopup.SetAction("Cloud", delegate
		{
			PsUIBasePopup psUIBasePopup = new PsUIBasePopup(typeof(PsUIPopupConfirmSyncFacebook), null, null, null, false, true, InitialPage.Center, false, false, false);
			PsState.m_openSocialPopup = psUIBasePopup;
			psUIBasePopup.SetAction("Confirm", delegate
			{
				PsState.m_openSocialPopup = null;
				FacebookManager.RestartWithExistingId(_existingId);
			});
			psUIBasePopup.SetAction("Cancel", delegate
			{
				FacebookManager.CreateSyncPopup(_existingId);
			});
		});
		FacebookManager.m_syncPopup.SetAction("Current", delegate
		{
			PsUIPopupConfirmSyncAccount.m_selectedData = PsUIPopupSyncAccount.m_currentData;
			PsUIBasePopup psUIBasePopup2 = new PsUIBasePopup(typeof(PsUIPopupConfirmSyncFacebook), null, null, null, false, true, InitialPage.Center, false, false, false);
			PsState.m_openSocialPopup = psUIBasePopup2;
			psUIBasePopup2.SetAction("Confirm", delegate
			{
				PsState.m_openSocialPopup = null;
				FacebookManager.TransferFBToCurrentId(_existingId);
			});
			psUIBasePopup2.SetAction("Cancel", delegate
			{
				FacebookManager.CreateSyncPopup(_existingId);
			});
		});
		FacebookManager.m_syncPopup.SetAction("Cancel", delegate
		{
			FacebookManager.DisconnectFB();
			PsState.m_openSocialPopup = null;
		});
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x00156B24 File Offset: 0x00154F24
	private static void FbLoginSendOk(HttpC _c)
	{
		Debug.Log("FB login sent to server", null);
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("existingId"))
		{
			string text = string.Empty;
			text = (string)dictionary["existingId"];
			if (text != string.Empty)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				if (dictionary.ContainsKey("totalTrophies"))
				{
					num = Convert.ToInt32(dictionary["totalTrophies"]);
				}
				if (dictionary.ContainsKey("racesWon"))
				{
					num2 = Convert.ToInt32(dictionary["racesWon"]);
				}
				if (dictionary.ContainsKey("adventuresCompleted"))
				{
					num3 = Convert.ToInt32(dictionary["adventuresCompleted"]);
				}
				int num4 = num2 + num3;
				PsUIPopupSyncAccount.m_currentData = new SyncAccountData(PlayerPrefsX.GetUserId(), PsMetagameManager.m_playerStats.totalTrophies, PsMetagameManager.m_playerStats.racesCompleted + PsMetagameManager.m_playerStats.adventureLevels, false, string.Empty, "CurrentAccount");
				PsUIPopupSyncAccount.m_cloudData = new List<SyncAccountData>();
				PsUIPopupSyncAccount.m_cloudData.Add(new SyncAccountData(text, num, num4, true, string.Empty, "Facebook"));
				FacebookManager.CreateSyncPopup(text);
			}
		}
		else
		{
			PlayerPrefsX.SetFacebookId(FacebookManager.m_userData.id);
			if (FacebookManager.m_completeCallback != null)
			{
				FacebookManager.m_completeCallback.Invoke();
			}
		}
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x00156C84 File Offset: 0x00155084
	private static void FbLoginSendFail(HttpC _c)
	{
		Dictionary<string, string> responseHeaders = _c.www.responseHeaders;
		Debug.Log("FB login send to server fail", null);
		if (responseHeaders.ContainsKey("PLAYE_ERROR") && responseHeaders["PLAY_ERROR"] == "SOCIAL_ERROR")
		{
			FacebookManager.m_disableFBLogin = true;
		}
		if (FacebookManager.m_completeCallback != null)
		{
			FacebookManager.m_completeCallback.Invoke();
		}
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x00156CEC File Offset: 0x001550EC
	private static void RestartWithExistingId(string _id)
	{
		Debug.Log("Restarting with existing ID", null);
		string userId = PlayerPrefsX.GetUserId();
		PlayerPrefsX.SetFacebookId(FacebookManager.m_userData.id);
		Player.ResolveSocialLogin("facebook", FacebookManager.m_userData.id, _id, userId, FacebookManager.m_friends, new Action<HttpC>(FacebookManager.ResolveOkDontLoadFriends), new Action<HttpC>(FacebookManager.ResolveFail), null);
		FacebookManager.m_friendLoadPending = true;
		PsState.m_restarting = true;
		PlayerPrefsX.SetUserId(_id);
		if (PsMetagameManager.m_serverQueueItems != null)
		{
			for (int i = 0; i < PsMetagameManager.m_serverQueueItems.Count; i++)
			{
				PsMetagameManager.m_serverQueueItems[i] = null;
			}
			PsMetagameManager.m_serverQueueItems.Clear();
		}
		FacebookManager.m_completeCallback = null;
		Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), new FadeLoadingScene(Color.black, true, 0.25f));
	}

	// Token: 0x06001E37 RID: 7735 RVA: 0x00156DEB File Offset: 0x001551EB
	private static void RestartWithNewAccount()
	{
		Debug.Log("Restart with new account", null);
		FacebookManager.m_friendLoadPending = true;
		PlayerPrefsX.SetFacebookId(FacebookManager.m_userData.id);
		ResetFlow.Start(false);
	}

	// Token: 0x06001E38 RID: 7736 RVA: 0x00156E14 File Offset: 0x00155214
	private static void TransferFBToCurrentId(string _oldId)
	{
		Debug.Log("Transfer FB to current user", null);
		FacebookManager.m_friendLoadPending = true;
		PlayerPrefsX.SetFacebookId(FacebookManager.m_userData.id);
		Player.ResolveSocialLogin("facebook", FacebookManager.m_userData.id, PlayerPrefsX.GetUserId(), _oldId, FacebookManager.m_friends, new Action<HttpC>(FacebookManager.ResolveOk), new Action<HttpC>(FacebookManager.ResolveFail), null);
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x00156E9B File Offset: 0x0015529B
	private static void ResolveOkDontLoadFriends(HttpC _c)
	{
		Debug.Log("Social resolve ok.", null);
		FacebookManager.m_friendLoadPending = true;
	}

	// Token: 0x06001E3A RID: 7738 RVA: 0x00156EAE File Offset: 0x001552AE
	private static void ResolveOk(HttpC _c)
	{
		Debug.Log("ResolveOK", null);
		FacebookManager.LoadFriends(true);
	}

	// Token: 0x06001E3B RID: 7739 RVA: 0x00156EC1 File Offset: 0x001552C1
	private static void ResolveFail(HttpC _c)
	{
		Debug.Log("Social resolve failed.", null);
		if (FacebookManager.m_completeCallback != null)
		{
			FacebookManager.m_completeCallback.Invoke();
		}
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x00156EE2 File Offset: 0x001552E2
	private static void DisconnectFB()
	{
		FacebookManager.Logout(null, false);
	}

	// Token: 0x06001E3D RID: 7741 RVA: 0x00156EEC File Offset: 0x001552EC
	private static void LoginComplete(FBUserData? _fbUserData)
	{
		Debug.Log("FB Login complete", null);
		if (_fbUserData != null)
		{
			FacebookManager.m_userData = _fbUserData.Value;
			PlayerPrefsX.SetFacebookName(FacebookManager.m_userData.username);
			FacebookManager.LoadFriends(false);
		}
		else if (FacebookManager.m_completeCallback != null)
		{
			FacebookManager.m_completeCallback.Invoke();
		}
	}

	// Token: 0x06001E3E RID: 7742 RVA: 0x00156F4C File Offset: 0x0015534C
	public static void Logout(Action _completeCallback, bool remove = false)
	{
		if (_completeCallback != null)
		{
			FacebookManager.m_completeCallback = _completeCallback;
		}
		if (remove)
		{
			Debug.Log("LOGGING OUT OF FACEBOOK", null);
			string facebookId = PlayerPrefsX.GetFacebookId();
			FB.LogOut();
			PlayerPrefsX.DeleteKey("FacebookId");
			PlayerPrefsX.DeleteKey("FacebookName");
			Player.RemoveSocialLogin("facebook", facebookId, new Action<HttpC>(FacebookManager.RemoveOk), new Action<HttpC>(FacebookManager.RemoveFail), null);
		}
		else if (FacebookManager.m_completeCallback != null)
		{
			FacebookManager.m_completeCallback.Invoke();
		}
	}

	// Token: 0x06001E3F RID: 7743 RVA: 0x00156FF4 File Offset: 0x001553F4
	private static void RemoveOk(HttpC _c)
	{
		Debug.Log("Social remove succesful", null);
		if (FacebookManager.m_completeCallback != null)
		{
			FacebookManager.m_completeCallback.Invoke();
		}
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x00157015 File Offset: 0x00155415
	private static void RemoveFail(HttpC _c)
	{
		Debug.Log("Social remove from user failed on server", null);
		if (FacebookManager.m_completeCallback != null)
		{
			FacebookManager.m_completeCallback.Invoke();
		}
	}

	// Token: 0x06001E41 RID: 7745 RVA: 0x00157036 File Offset: 0x00155436
	private static void FriendSendOk(HttpC _c)
	{
		Debug.Log("FB friends sent to server", null);
		if (FacebookManager.m_completeCallback != null)
		{
			FacebookManager.m_completeCallback.Invoke();
		}
		FacebookManager.m_completeCallback = null;
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x0015705D File Offset: 0x0015545D
	private static void FriendSendFail(HttpC _c = null)
	{
		Debug.Log("FB friend send to server fail", null);
		if (FacebookManager.m_completeCallback != null)
		{
			FacebookManager.m_completeCallback.Invoke();
		}
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x00157080 File Offset: 0x00155480
	public static void LoadFriends(bool _pending = false)
	{
		Debug.Log("Load FB Friends", null);
		FB.API("/me/friends?fields=id,name,installed", 0, delegate(IGraphResult fbResult)
		{
			if (!string.IsNullOrEmpty(fbResult.Error))
			{
				Debug.LogError("Facebook friend load error " + fbResult.Error);
				FacebookManager.m_friends = null;
				FacebookManager.FriendSendFail(null);
			}
			else
			{
				FacebookManager.m_friends = ClientTools.ParseFBFriendIds(fbResult.RawResult);
				if (FacebookManager.m_friendLoadPending || _pending)
				{
					FacebookManager.m_friendLoadPending = false;
					Player.SendSocialFriends("facebook", FacebookManager.m_userData.id, FacebookManager.m_friends, new Action<HttpC>(FacebookManager.FriendSendOk), new Action<HttpC>(FacebookManager.FriendSendFail), null);
				}
				else
				{
					FacebookManager.CheckAccountChange();
				}
			}
		}, null);
	}

	// Token: 0x06001E44 RID: 7748 RVA: 0x001570C0 File Offset: 0x001554C0
	private static void JoinOk(HttpC _c)
	{
		Debug.Log("FB join success", null);
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		object obj = null;
		dictionary.TryGetValue("firstJoin", ref obj);
		if (obj != null && (bool)obj)
		{
			PsMetagameManager.m_playerStats.diamonds += PlayerPrefsX.GetClientConfig().fbConnectReward;
			PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
		}
		if (FacebookManager.m_completeCallback != null)
		{
			FacebookManager.m_completeCallback.Invoke();
		}
	}

	// Token: 0x06001E45 RID: 7749 RVA: 0x0015717F File Offset: 0x0015557F
	private static void JoinFail(HttpC _c)
	{
		Debug.Log("FB join fail", null);
		FacebookManager.Logout(null, false);
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x00157193 File Offset: 0x00155593
	public static bool IsLoggedIn()
	{
		return PlayerPrefsX.GetFacebookId() != null && FB.IsLoggedIn;
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x001571A8 File Offset: 0x001555A8
	private static void OnFBInitComplete()
	{
		FacebookManager.m_initComplete = true;
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn, null);
		if (FB.IsLoggedIn)
		{
			FB.API("/me?fields=id,first_name,last_name", 0, new FacebookDelegate<IGraphResult>(FacebookManager.FBUserDataCallback), null);
		}
		else
		{
			List<string> list = new List<string>();
			list.Add("user_friends");
			FB.LogInWithReadPermissions(list, new FacebookDelegate<ILoginResult>(FacebookManager.FBLoginCallback));
		}
		if (Application.platform != null && Application.platform != 7)
		{
			FB.GetAppLink(new FacebookDelegate<IAppLinkResult>(FacebookManager.DeepLinkCallback));
		}
	}

	// Token: 0x06001E48 RID: 7752 RVA: 0x00157278 File Offset: 0x00155678
	private static void FBLoginCallback(ILoginResult result)
	{
		if (result.Error != null)
		{
			Debug.LogError("Error Response from FB Login:\n" + result.Error);
			FacebookManager.LoginComplete(default(FBUserData?));
		}
		else if (FB.IsLoggedIn)
		{
			Debug.Log("FB Login was successful!", null);
			FB.API("/me?fields=id,first_name,last_name", 0, new FacebookDelegate<IGraphResult>(FacebookManager.FBUserDataCallback), null);
		}
		else
		{
			Debug.Log("FB Login cancelled by Player", null);
			FacebookManager.LoginComplete(default(FBUserData?));
		}
	}

	// Token: 0x06001E49 RID: 7753 RVA: 0x00157314 File Offset: 0x00155714
	private static void FBUserDataCallback(IGraphResult result)
	{
		if (result.Error != null)
		{
			Debug.LogError("Error Response:\n" + result.Error);
			FacebookManager.LoginComplete(default(FBUserData?));
		}
		else
		{
			Debug.Log("FB Me call was successful!", null);
			Debug.Log(result.RawResult, null);
			FBUserData fbuserData = FacebookManager.ParseFBUserData(result.RawResult);
			FacebookManager.LoginComplete(new FBUserData?(fbuserData));
		}
	}

	// Token: 0x06001E4A RID: 7754 RVA: 0x00157384 File Offset: 0x00155784
	private static FBUserData ParseFBUserData(string _FBUserDataString)
	{
		Dictionary<string, object> dictionary = FacebookManager.ParseFBResponse(_FBUserDataString);
		FBUserData fbuserData = default(FBUserData);
		fbuserData.id = (string)dictionary["id"];
		string text = (string)dictionary["first_name"];
		string text2 = (string)dictionary["last_name"];
		fbuserData.username = text + " " + ((text2.Length <= 0) ? string.Empty : text2.Substring(0, 1));
		return fbuserData;
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x0015740C File Offset: 0x0015580C
	public static Dictionary<string, object> ParseFBResponse(string _response)
	{
		if (!_response.StartsWith("{"))
		{
			Debug.LogError("Server response not valid JSON: " + _response);
			return null;
		}
		return Json.Deserialize(_response) as Dictionary<string, object>;
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x00157448 File Offset: 0x00155848
	public static HttpC GetPicture(string _facebookId, Action<Texture2D> _okCallback, Action<HttpC> _failureCallback, bool _largePicture)
	{
		ResponseHandler<Texture2D> responseHandler = new ResponseHandler<Texture2D>(_okCallback, new Func<HttpC, Texture2D>(ClientTools.ParseTexture));
		string text = "https://graph.facebook.com/" + _facebookId + "/picture";
		if (_largePicture)
		{
			text += "?width=200&height=200";
		}
		else
		{
			text += "?width=50&height=50";
		}
		HttpC httpC = HttpS.AddGetComponent(text, null, null, true, null);
		httpC.requestComplete += new Action<HttpC>(responseHandler.RequestOk);
		httpC.requestFailed += _failureCallback;
		return httpC;
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x001574D4 File Offset: 0x001558D4
	private static void DeepLinkCallback(IAppLinkResult result)
	{
		if (!string.IsNullOrEmpty(result.Url))
		{
			int num = new Uri(result.Url).Query.IndexOf("request_ids");
			if (num != -1)
			{
				Debug.Log("Deep link received", null);
			}
		}
	}

	// Token: 0x0400217E RID: 8574
	private static bool m_initComplete;

	// Token: 0x0400217F RID: 8575
	private static Action m_completeCallback;

	// Token: 0x04002180 RID: 8576
	private static string[] m_friends;

	// Token: 0x04002181 RID: 8577
	private static FBUserData m_userData;

	// Token: 0x04002182 RID: 8578
	public static bool m_friendLoadPending;

	// Token: 0x04002183 RID: 8579
	public static bool m_disableFBLogin;

	// Token: 0x04002184 RID: 8580
	private static PsUIBasePopup m_syncPopup;
}
