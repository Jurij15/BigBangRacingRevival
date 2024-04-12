using System;

// Token: 0x02000005 RID: 5
public class PsBackupGooglePlay : PsBackup
{
	// Token: 0x06000013 RID: 19 RVA: 0x000023D9 File Offset: 0x000007D9
	public PsBackupGooglePlay(string _fileName)
		: base(_fileName)
	{
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000023E2 File Offset: 0x000007E2
	public override string GetServiceName()
	{
		return "AndroidGooglePlayGames";
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000023EC File Offset: 0x000007EC
	public override void ReadBackup()
	{
		if (GooglePlayManager.IsAuthenticated())
		{
			GooglePlayManager.LoadFromCloud(base.GetFileName(), new Action<string>(this.LoadSucceed), new Action(this.LoadFailed));
		}
		else
		{
			GooglePlayManager.RemoveLoginSucceedCallback(new Action(this.ReadBackup));
			GooglePlayManager.AddLoginSucceedCallback(new Action(this.ReadBackup));
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000244F File Offset: 0x0000084F
	private void LoadSucceed(string _json)
	{
		base.SetPlayer(_json);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002458 File Offset: 0x00000858
	private void LoadFailed()
	{
		Debug.LogError("Load Failed");
		this.m_status = PsBackup.Status.Loading;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x0000246C File Offset: 0x0000086C
	protected override void SetBackup(string _json)
	{
		this.m_status = PsBackup.Status.Loading;
		GooglePlayManager.SaveToCloud(base.GetFileName(), _json, delegate
		{
			this.SaveSucceed(_json);
		}, new Action(this.SaveFailed));
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000024BD File Offset: 0x000008BD
	private void SaveSucceed(string _json)
	{
		this.m_status = PsBackup.Status.Ready;
		this.m_player = base.ParsePlayerFromBackupJSON(_json);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000024D3 File Offset: 0x000008D3
	private void SaveFailed()
	{
		this.m_status = PsBackup.Status.Loading;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000024DC File Offset: 0x000008DC
	public override void Clear()
	{
		GooglePlayManager.SaveToCloud(base.GetFileName(), string.Empty, delegate
		{
			this.SaveSucceed(string.Empty);
		}, new Action(this.SaveFailed));
	}
}
