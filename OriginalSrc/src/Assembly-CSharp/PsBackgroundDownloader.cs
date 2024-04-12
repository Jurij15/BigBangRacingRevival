using System;
using System.Collections.Generic;
using System.IO;
using Server;
using UnityEngine;

// Token: 0x02000131 RID: 305
public static class PsBackgroundDownloader
{
	// Token: 0x06000951 RID: 2385 RVA: 0x000644B8 File Offset: 0x000628B8
	public static void Initialize()
	{
		string text = Application.streamingAssetsPath + "/FMOD_bank_list.txt";
		try
		{
			WWW www = new WWW(text);
			while (!www.isDone)
			{
			}
			string[] array = www.text.Split(new char[] { '\n' });
			www.Dispose();
			foreach (string text2 in array)
			{
				if (!text2.StartsWith("Master") && !string.IsNullOrEmpty(text2) && text2.Length > 5)
				{
					PsBackgroundDownloader.m_checkCount++;
					string text3 = text2.Substring(0, text2.Length - 5);
					Debug.Log(text3 + "/" + PsBackgroundDownloader.m_checkCount, null);
					HttpC httpC = Preload.Check(text3, new Action<FileMetaData>(PsBackgroundDownloader.FileCheckSUCCEED), new Action<HttpC>(PsBackgroundDownloader.FileCheckFAILED), null);
					httpC.tag = text3;
				}
			}
		}
		catch
		{
			Debug.LogError("BANK LIST NOT FOUND");
		}
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0006460C File Offset: 0x00062A0C
	private static void FileCheckSUCCEED(FileMetaData _meta)
	{
		PsBackgroundDownloader.m_checkCount--;
		if (_meta.version > PlayerPrefsX.GetFileVersion(_meta.name) || !File.Exists(Application.persistentDataPath + "/" + _meta.name + ".bank"))
		{
			if (File.Exists(Application.persistentDataPath + "/" + _meta.name + ".bank"))
			{
				File.Delete(Application.persistentDataPath + "/" + _meta.name + ".bank");
			}
			if (PsBackgroundDownloader.m_fireNext)
			{
				PsBackgroundDownloader.DownloadFile(_meta);
			}
			else
			{
				PsBackgroundDownloader.m_metas.Add(_meta);
			}
		}
		if (PsBackgroundDownloader.m_checkCount == 0)
		{
			PsBackgroundDownloader.m_checksDone = true;
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x000646D4 File Offset: 0x00062AD4
	private static void DownloadFile(FileMetaData _meta)
	{
		PsBackgroundDownloader.m_fireNext = false;
		HttpC httpC = Preload.Download(_meta, new Action<HttpC>(PsBackgroundDownloader.FileDownloadSUCCEED), new Action<HttpC>(PsBackgroundDownloader.FileDownloadFAILED), null);
		httpC.ignoreInQueue = true;
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x00064730 File Offset: 0x00062B30
	private static void FileCheckFAILED(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), delegate
		{
			HttpC httpC = Preload.Check(_c.tag, new Action<FileMetaData>(PsBackgroundDownloader.FileCheckSUCCEED), new Action<HttpC>(PsBackgroundDownloader.FileCheckFAILED), null);
			httpC.tag = _c.tag;
			return httpC;
		}, null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00064780 File Offset: 0x00062B80
	private static void FileDownloadSUCCEED(HttpC _c)
	{
		PsBackgroundDownloader.m_fireNext = true;
		FileMetaData fileMetaData = _c.objectData as FileMetaData;
		Debug.Log("FILE DOWNLOADED: " + fileMetaData.name, null);
		PlayerPrefsX.SetFileVersion(fileMetaData.name, fileMetaData.version);
		ClientTools.SaveFile(fileMetaData.name, ".bank", _c.www.bytes);
		if (SoundS.m_initialized)
		{
			PsFMODManager.loadBank(fileMetaData.name + ".bank");
		}
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00064800 File Offset: 0x00062C00
	private static void FileDownloadFAILED(HttpC _c)
	{
		Debug.Log("FILE DOWNLOAD FAILED", null);
		HttpC httpC = Preload.Download(_c.objectData as FileMetaData, new Action<HttpC>(PsBackgroundDownloader.FileDownloadSUCCEED), new Action<HttpC>(PsBackgroundDownloader.FileDownloadFAILED), null);
		httpC.ignoreInQueue = true;
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0006486B File Offset: 0x00062C6B
	public static void Update()
	{
		if (PsBackgroundDownloader.m_metas.Count > 0 && PsBackgroundDownloader.m_fireNext)
		{
			PsBackgroundDownloader.DownloadFile(PsBackgroundDownloader.m_metas[0]);
			PsBackgroundDownloader.m_metas.RemoveAt(0);
		}
	}

	// Token: 0x040008A7 RID: 2215
	private static int m_checkCount = 0;

	// Token: 0x040008A8 RID: 2216
	public static bool m_checksDone = false;

	// Token: 0x040008A9 RID: 2217
	private static bool m_fireNext = true;

	// Token: 0x040008AA RID: 2218
	private static List<FileMetaData> m_metas = new List<FileMetaData>();
}
