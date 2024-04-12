using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020004B1 RID: 1201
public class ByteArrayDiskCache
{
	// Token: 0x06002248 RID: 8776 RVA: 0x0018E9A0 File Offset: 0x0018CDA0
	public ByteArrayDiskCache(string _name, int _maxItems = 2147483647)
	{
		this.m_name = _name;
		this.m_maxItems = _maxItems;
		this.m_fileList = new List<string>();
		this.m_directory = Application.persistentDataPath + "/Cache/" + this.m_name + "/";
		if (!Directory.Exists(this.m_directory))
		{
			Directory.CreateDirectory(this.m_directory);
			Debug.Log("Creating dir for diskCache: " + this.m_directory, null);
		}
		this.ReadFileList();
		Debug.Log(string.Concat(new object[]
		{
			"Disk cache initialized: ",
			this.m_directory,
			" -- existing items: ",
			this.m_fileList.Count
		}), null);
	}

	// Token: 0x06002249 RID: 8777 RVA: 0x0018EA61 File Offset: 0x0018CE61
	public string GetCachePath()
	{
		return this.m_directory;
	}

	// Token: 0x0600224A RID: 8778 RVA: 0x0018EA6C File Offset: 0x0018CE6C
	public void SaveFileList()
	{
		string text = this.GetCachePath() + "filelist.txt";
		if (File.Exists(text))
		{
			File.Delete(text);
		}
		StreamWriter streamWriter = File.CreateText(text);
		foreach (string text2 in this.m_fileList)
		{
			streamWriter.WriteLine(text2);
		}
		streamWriter.Close();
	}

	// Token: 0x0600224B RID: 8779 RVA: 0x0018EAF8 File Offset: 0x0018CEF8
	private void ReadFileList()
	{
		string text = this.GetCachePath() + "filelist.txt";
		if (File.Exists(text))
		{
			StreamReader streamReader = File.OpenText(text);
			this.m_fileList.Clear();
			while (!streamReader.EndOfStream)
			{
				string text2 = streamReader.ReadLine();
				this.m_fileList.Add(text2);
			}
			streamReader.Close();
		}
	}

	// Token: 0x0600224C RID: 8780 RVA: 0x0018EB5C File Offset: 0x0018CF5C
	public int GetItemCount()
	{
		return this.m_fileList.Count;
	}

	// Token: 0x0600224D RID: 8781 RVA: 0x0018EB6C File Offset: 0x0018CF6C
	public void AddItem(string _key, byte[] _content)
	{
		if (_content == null || _content.Length == 0)
		{
			Debug.LogWarning("Cannot assign null or empty value to cache");
			return;
		}
		string text = this.GetCachePath() + _key;
		bool flag = this.m_fileList.Contains(_key);
		if (flag)
		{
			if (File.Exists(text))
			{
				File.Delete(text);
			}
		}
		else
		{
			if (this.m_fileList.Count >= this.m_maxItems)
			{
				string text2 = this.m_fileList[0];
				string text3 = this.GetCachePath() + text2;
				if (File.Exists(text3))
				{
					File.Delete(text3);
				}
				this.m_fileList.Remove(text2);
			}
			this.m_fileList.Add(_key);
			this.SaveFileList();
		}
		FileStream fileStream = File.Create(text);
		if (fileStream != null)
		{
			fileStream.Write(_content, 0, _content.Length);
			fileStream.Close();
		}
		else
		{
			Debug.LogError("Cannot create file: " + text);
		}
	}

	// Token: 0x0600224E RID: 8782 RVA: 0x0018EC64 File Offset: 0x0018D064
	public byte[] GetContent(string _key)
	{
		if (this.m_fileList.Contains(_key))
		{
			string text = this.GetCachePath() + _key;
			FileStream fileStream = File.Open(text, 3);
			if (fileStream != null)
			{
				byte[] array = new byte[(int)fileStream.Length];
				fileStream.Read(array, 0, (int)fileStream.Length);
				fileStream.Close();
				return array;
			}
			Debug.LogError("DiskCache file " + text + " does not exist!");
		}
		return null;
	}

	// Token: 0x0600224F RID: 8783 RVA: 0x0018ECD8 File Offset: 0x0018D0D8
	public void RemoveItem(string _key)
	{
		if (this.m_fileList.Contains(_key))
		{
			string text = this.GetCachePath() + _key;
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			this.m_fileList.Remove(_key);
			this.SaveFileList();
		}
	}

	// Token: 0x06002250 RID: 8784 RVA: 0x0018ED28 File Offset: 0x0018D128
	public void Clear()
	{
		string text = this.GetCachePath() + "filelist.txt";
		if (File.Exists(text))
		{
			File.Delete(text);
		}
		this.m_fileList.Clear();
		string cachePath = this.GetCachePath();
		if (Directory.Exists(cachePath))
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(cachePath);
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				fileInfo.Delete();
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				directoryInfo2.Delete(true);
			}
		}
		Debug.Log("Disk cache cleared: " + cachePath, null);
	}

	// Token: 0x04002881 RID: 10369
	private string m_name;

	// Token: 0x04002882 RID: 10370
	private int m_maxItems;

	// Token: 0x04002883 RID: 10371
	public string m_directory;

	// Token: 0x04002884 RID: 10372
	private List<string> m_fileList;
}
