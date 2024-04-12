using System;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x02000608 RID: 1544
	public static class PlatformUtils
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x001C00BC File Offset: 0x001BE4BC
		public static bool Supported
		{
			get
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getPackageManager", new object[0]);
				AndroidJavaObject androidJavaObject2 = null;
				try
				{
					androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getLaunchIntentForPackage", new object[] { "com.google.android.play.games" });
				}
				catch (Exception)
				{
					return false;
				}
				return androidJavaObject2 != null;
			}
		}
	}
}
