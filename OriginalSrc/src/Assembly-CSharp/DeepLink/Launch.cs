using System;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;

namespace DeepLink
{
	// Token: 0x0200055E RID: 1374
	public static class Launch
	{
		// Token: 0x06002815 RID: 10261 RVA: 0x001AB5BC File Offset: 0x001A99BC
		public static string GetLaunchUrl()
		{
			string @string = PlayerPrefs.GetString("DLUrl");
			PlayerPrefs.SetString("DLUrl", string.Empty);
			return @string;
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x001AB5E4 File Offset: 0x001A99E4
		public static void SetLaunchUrl(string _url)
		{
			PlayerPrefs.SetString("DLUrl", _url);
			Debug.LogError("Launch url was set to: " + _url);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x001AB604 File Offset: 0x001A9A04
		private static string GetNotificationPayload()
		{
			string @string = PlayerPrefs.GetString("DLNotification");
			PlayerPrefs.SetString("DLNotification", string.Empty);
			return @string;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x001AB62C File Offset: 0x001A9A2C
		private static Dictionary<string, object> GetLastNotificationPayload()
		{
			return Launch.m_lastPayload;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x001AB634 File Offset: 0x001A9A34
		private static Dictionary<string, object> GetNotificationPayloadDictionary()
		{
			string notificationPayload = Launch.GetNotificationPayload();
			Dictionary<string, object> dictionary = null;
			if (!string.IsNullOrEmpty(notificationPayload))
			{
				try
				{
					dictionary = Json.Deserialize(notificationPayload) as Dictionary<string, object>;
					Launch.m_lastPayload = dictionary;
				}
				catch
				{
					Debug.LogError("Payload was not json");
				}
			}
			return dictionary;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x001AB68C File Offset: 0x001A9A8C
		public static object GetObjectFromNotification(string _key)
		{
			Dictionary<string, object> dictionary = Launch.GetNotificationPayloadDictionary();
			if (dictionary == null)
			{
				dictionary = Launch.GetLastNotificationPayload();
			}
			if (dictionary == null)
			{
				return null;
			}
			if (dictionary.ContainsKey(_key))
			{
				object obj = dictionary[_key];
				dictionary.Remove(_key);
				return obj;
			}
			return null;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x001AB6D2 File Offset: 0x001A9AD2
		public static void SetNotificationPayload(string _payload)
		{
			PlayerPrefs.SetString("DLNotification", _payload);
			Debug.LogError("Notification payload was set to: " + _payload);
		}

		// Token: 0x04002D7C RID: 11644
		private const string LAUNCH_URL = "DLUrl";

		// Token: 0x04002D7D RID: 11645
		private const string NOTIFICATION_PAYLOAD = "DLNotification";

		// Token: 0x04002D7E RID: 11646
		private static Dictionary<string, object> m_lastPayload;
	}
}
