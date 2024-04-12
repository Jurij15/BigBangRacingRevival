using System;
using System.Collections.Generic;
using Firebase.Messaging;
using MiniJSON;

// Token: 0x02000435 RID: 1077
public static class PushMessageManager
{
	// Token: 0x06001DF1 RID: 7665 RVA: 0x00155358 File Offset: 0x00153758
	public static void RegisterForNotifications()
	{
		PushMessageManager.m_sendDeviceToken = true;
		FirebaseCloudMessageManager.RegisterForNotifications(delegate(TokenReceivedEventArgs c)
		{
			if (PushMessageManager.m_sendDeviceToken)
			{
				PushMessageManager.m_sendDeviceToken = false;
				string token = c.Token;
				string text = PlayerPrefsX.GetDeviceToken();
				text = ((!string.IsNullOrEmpty(text)) ? text : null);
				if (token != text)
				{
					PushMessageManager.addDeviceToken(token, null, null);
				}
			}
		}, delegate(MessageReceivedEventArgs c)
		{
			Debug.Log("Received push through through FCM, from: " + c.Message.From, null);
			if (c.Message.Data != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in c.Message.Data)
				{
					Debug.Log(keyValuePair.Key + ": " + keyValuePair.Value, null);
				}
				if (!c.Message.Data.ContainsKey("origin") || c.Message.Data["origin"].Equals("helpshift"))
				{
				}
			}
		});
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x001553AC File Offset: 0x001537AC
	private static void addDeviceToken(string _newToken, string _oldToken = null, Action _errorCallback = null)
	{
		WOEURL woeurl = new WOEURL("/v1/push/token/save");
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("newToken", _newToken);
		if (_oldToken != null)
		{
			dictionary.Add("oldToken", _oldToken);
		}
		woeurl.AddParameter("platform", "FCM");
		HttpC httpC = woeurl.AddPostComponent(null, Json.Serialize(dictionary), null, null, true);
		ErrorHandler errorHandler = new ErrorHandler(delegate(HttpC cb)
		{
			PushMessageManager.TokenSendOk(cb, _newToken);
		}, _errorCallback);
		httpC.requestComplete += new Action<HttpC>(errorHandler.CheckForErrors);
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x00155444 File Offset: 0x00153844
	private static void TokenSendOk(HttpC _c, string _token)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (ClientTools.ServerResponseOk(dictionary))
		{
			Debug.Log("TOKEN SEND: Token send OK", null);
			PlayerPrefsX.SetDeviceToken(_token);
		}
		else
		{
			Debug.LogError("TOKEN SEND ERROR: " + (string)dictionary["status"]);
		}
	}

	// Token: 0x040020A5 RID: 8357
	public static bool m_sendDeviceToken;
}
