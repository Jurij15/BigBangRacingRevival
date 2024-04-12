using System;
using System.IO;
using UnityEngine;

namespace DeepLink
{
	// Token: 0x0200055D RID: 1373
	public static class Share
	{
		// Token: 0x06002811 RID: 10257 RVA: 0x001AB2FB File Offset: 0x001A96FB
		public static void ShareTextOnPlatform(string _text)
		{
			Share.ShareTextOnAndroid(_text, string.Empty, string.Empty);
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x001AB310 File Offset: 0x001A9710
		private static void ShareTextOnAndroid(string _text, string _subject = "", string _title = "")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent", new object[0]);
			androidJavaObject.Call<AndroidJavaObject>("setAction", new object[] { androidJavaClass.GetStatic<string>("ACTION_SEND") });
			androidJavaObject.Call<AndroidJavaObject>("setType", new object[] { "text/plain" });
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_SUBJECT"),
				_subject
			});
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
				_text
			});
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
			@static.Call("startActivity", new object[] { androidJavaObject });
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x001AB3E8 File Offset: 0x001A97E8
		public static void ShareImageOnPlatform(byte[] _image, string _textMessage = "")
		{
			string text = Application.persistentDataPath + "/MyImage.png";
			File.WriteAllBytes(text, _image);
			Share.ShareImageOnAndroid(text, _textMessage, string.Empty, string.Empty);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x001AB420 File Offset: 0x001A9820
		private static void ShareImageOnAndroid(string _path, string _textMessage, string _subject = "", string _title = "")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent", new object[0]);
			androidJavaObject.Call<AndroidJavaObject>("setAction", new object[] { androidJavaClass.GetStatic<string>("ACTION_SEND") });
			androidJavaObject.Call<AndroidJavaObject>("setType", new object[] { "image/*" });
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_SUBJECT"),
				_subject
			});
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_TITLE"),
				_title
			});
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
				_textMessage
			});
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.net.Uri");
			AndroidJavaClass androidJavaClass3 = new AndroidJavaClass("java.io.File");
			AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.io.File", new object[] { _path });
			AndroidJavaObject androidJavaObject3 = androidJavaClass2.CallStatic<AndroidJavaObject>("fromFile", new object[] { androidJavaObject2 });
			bool flag = androidJavaObject2.Call<bool>("exists", new object[0]);
			Debug.Log("File exist : " + flag, null);
			if (flag)
			{
				androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
				{
					androidJavaClass.GetStatic<string>("EXTRA_STREAM"),
					androidJavaObject3
				});
			}
			AndroidJavaClass androidJavaClass4 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass4.GetStatic<AndroidJavaObject>("currentActivity");
			@static.Call("startActivity", new object[] { androidJavaObject });
		}
	}
}
