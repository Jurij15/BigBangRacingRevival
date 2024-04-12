using System;
using System.IO;
using UnityEngine;

// Token: 0x02000009 RID: 9
public static class PsPersistentAndroid
{
	// Token: 0x0600003A RID: 58 RVA: 0x00002EBC File Offset: 0x000012BC
	public static void SaveString(string _fileName, string _json)
	{
		string text = PsPersistentAndroid.GetPathString("/BigBangRacing");
		text = text + "/" + _fileName;
		try
		{
			StreamWriter streamWriter = File.CreateText(text);
			streamWriter.WriteLine(_json);
			streamWriter.Close();
			Debug.Log("Writing string worked: " + _json, null);
		}
		catch (Exception ex)
		{
			Debug.LogError("Writing string did not work: " + _json + ", exception: " + ex.Message);
		}
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002F3C File Offset: 0x0000133C
	public static string GetString(string _fileName)
	{
		string text = string.Empty;
		string text2 = PsPersistentAndroid.GetPathString("/BigBangRacing");
		text2 = text2 + "/" + _fileName;
		try
		{
			if (!string.IsNullOrEmpty(text2) && File.Exists(text2))
			{
				StreamReader streamReader = File.OpenText(text2);
				string text3 = string.Empty;
				for (string text4 = streamReader.ReadLine(); text4 != null; text4 = streamReader.ReadLine())
				{
					text3 += text4;
				}
				streamReader.Close();
				Debug.Log("Backup file: Text content: " + text3, null);
				text = text3;
			}
			else
			{
				Debug.LogError("File did not exist or path was empty");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("reading did not work: " + ex.Message);
		}
		return text;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000300C File Offset: 0x0000140C
	private static string GetPathString(string _folderName)
	{
		string text = PsPersistentAndroid.GetExpansionFilePath() + _folderName;
		if (!Directory.Exists(text))
		{
			try
			{
				Directory.CreateDirectory(text);
			}
			catch (Exception ex)
			{
				Debug.LogError("Could not make directory: " + ex.Message);
			}
		}
		return text;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003068 File Offset: 0x00001468
	private static string GetExpansionFilePath()
	{
		string text;
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Environment"))
		{
			if (androidJavaClass.CallStatic<string>("getExternalStorageState", new object[0]) != "mounted")
			{
				text = null;
			}
			else
			{
				using (AndroidJavaObject androidJavaObject = androidJavaClass.CallStatic<AndroidJavaObject>("getExternalStorageDirectory", new object[0]))
				{
					string text2 = androidJavaObject.Call<string>("getPath", new object[0]);
					Debug.Log("Backup file: Root: " + text2, null);
					text = text2;
				}
			}
		}
		return text;
	}

	// Token: 0x0400001B RID: 27
	private const string FOLDER_NAME = "/BigBangRacing";

	// Token: 0x0400001C RID: 28
	private const string Environment_MEDIA_MOUNTED = "mounted";
}
