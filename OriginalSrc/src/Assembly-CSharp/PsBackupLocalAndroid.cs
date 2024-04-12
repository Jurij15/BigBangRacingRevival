using System;

// Token: 0x02000006 RID: 6
public class PsBackupLocalAndroid : PsBackup
{
	// Token: 0x0600001D RID: 29 RVA: 0x0000252E File Offset: 0x0000092E
	public PsBackupLocalAndroid(string _fileName)
		: base(_fileName)
	{
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002537 File Offset: 0x00000937
	public override string GetServiceName()
	{
		return "AndroidLocalStorage";
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000253E File Offset: 0x0000093E
	public override void ReadBackup()
	{
		base.SetPlayer(PsPersistentAndroid.GetString(base.GetFileName()));
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002551 File Offset: 0x00000951
	protected override void SetBackup(string _json)
	{
		PsPersistentAndroid.SaveString(base.GetFileName(), _json);
		this.m_status = PsBackup.Status.Ready;
		this.m_player = base.ParsePlayerFromBackupJSON(_json);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002574 File Offset: 0x00000974
	public override void Clear()
	{
		PsPersistentAndroid.SaveString(base.GetFileName(), string.Empty);
		this.m_status = PsBackup.Status.Ready;
		this.m_player = default(PsBackup.Player);
	}
}
