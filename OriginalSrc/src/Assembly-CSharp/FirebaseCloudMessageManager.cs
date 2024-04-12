using System;
using Firebase.Messaging;

// Token: 0x02000419 RID: 1049
public static class FirebaseCloudMessageManager
{
	// Token: 0x06001D22 RID: 7458 RVA: 0x0014CC48 File Offset: 0x0014B048
	public static void RegisterForNotifications(Action<TokenReceivedEventArgs> _tokenReceived = null, Action<MessageReceivedEventArgs> _messageReceived = null)
	{
		FirebaseCloudMessageManager.m_tokenReceived = _tokenReceived;
		FirebaseCloudMessageManager.m_messageReceived = _messageReceived;
		FirebaseMessaging.TokenReceived += new EventHandler<TokenReceivedEventArgs>(FirebaseCloudMessageManager.OnTokenReceived);
		FirebaseMessaging.MessageReceived += new EventHandler<MessageReceivedEventArgs>(FirebaseCloudMessageManager.OnMessageReceived);
	}

	// Token: 0x06001D23 RID: 7459 RVA: 0x0014CCA5 File Offset: 0x0014B0A5
	public static void OnTokenReceived(object sender, TokenReceivedEventArgs _args)
	{
		Debug.LogError("FCM Token for debugging: " + _args.Token);
		if (FirebaseCloudMessageManager.m_tokenReceived != null)
		{
			FirebaseCloudMessageManager.m_tokenReceived.Invoke(_args);
		}
	}

	// Token: 0x06001D24 RID: 7460 RVA: 0x0014CCD4 File Offset: 0x0014B0D4
	public static void OnMessageReceived(object sender, MessageReceivedEventArgs _args)
	{
		if (_args.Message.Data != null)
		{
			if (_args.Message.Data.ContainsKey("pl"))
			{
				Debug.LogError("FCM: pl found: " + _args.Message.Data["pl"]);
			}
			else
			{
				Debug.LogError("FCM: pl NOT found");
			}
		}
		if (FirebaseCloudMessageManager.m_messageReceived != null)
		{
			FirebaseCloudMessageManager.m_messageReceived.Invoke(_args);
		}
	}

	// Token: 0x04001FE6 RID: 8166
	public static Action<TokenReceivedEventArgs> m_tokenReceived;

	// Token: 0x04001FE7 RID: 8167
	public static Action<MessageReceivedEventArgs> m_messageReceived;
}
