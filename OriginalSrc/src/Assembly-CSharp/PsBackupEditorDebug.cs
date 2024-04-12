using System;
using System.IO;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class PsBackupEditorDebug : PsBackup
{
	// Token: 0x06000022 RID: 34 RVA: 0x000025A7 File Offset: 0x000009A7
	public PsBackupEditorDebug(string _fileName)
		: base(_fileName)
	{
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000025B0 File Offset: 0x000009B0
	public override string GetServiceName()
	{
		return "UnityEditorDebug";
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000025B7 File Offset: 0x000009B7
	public override void ReadBackup()
	{
		base.SetPlayer(this.Load(base.GetFileName()));
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000025CB File Offset: 0x000009CB
	protected override void SetBackup(string _json)
	{
		this.Save(base.GetFileName(), _json);
		this.m_status = PsBackup.Status.Ready;
		this.m_player = base.ParsePlayerFromBackupJSON(_json);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000025F0 File Offset: 0x000009F0
	public override void Clear()
	{
		this.Save(base.GetFileName(), string.Empty);
		this.m_status = PsBackup.Status.Ready;
		this.m_player = default(PsBackup.Player);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002624 File Offset: 0x00000A24
	private void Save(string _fileName, string _json)
	{
		string text = Application.persistentDataPath + "/" + _fileName;
		try
		{
			string text2 = Main.GetPersistentDataPath() + "/" + base.GetFileName();
			StreamWriter streamWriter = File.CreateText(text2);
			streamWriter.WriteLine(_json);
			streamWriter.Close();
			Debug.Log("Backup: Saved: " + _json, null);
		}
		catch
		{
			Debug.Log("Backup: Failed: " + _json, null);
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000026AC File Offset: 0x00000AAC
	private string Load(string _fileName)
	{
		string text = string.Empty;
		string text2 = Main.GetPersistentDataPath() + "/" + _fileName;
		if (File.Exists(text2))
		{
			StreamReader streamReader = File.OpenText(text2);
			for (string text3 = streamReader.ReadLine(); text3 != null; text3 = streamReader.ReadLine())
			{
				text += text3;
			}
			streamReader.Close();
		}
		return text;
	}
}
