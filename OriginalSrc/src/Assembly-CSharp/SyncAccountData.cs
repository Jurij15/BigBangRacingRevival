using System;

// Token: 0x02000142 RID: 322
public class SyncAccountData
{
	// Token: 0x06000A12 RID: 2578 RVA: 0x00068445 File Offset: 0x00066845
	public SyncAccountData(string _id, int _totalTrophies, int _levelsCompleted, bool _cloud, string _name = "", string _serviceName = "")
	{
		this.m_id = _id;
		this.m_totalTrophies = _totalTrophies;
		this.m_levelsCompleted = _levelsCompleted;
		this.m_cloud = _cloud;
		this.m_name = _name;
		this.m_serviceName = _serviceName;
	}

	// Token: 0x0400093D RID: 2365
	public string m_id;

	// Token: 0x0400093E RID: 2366
	public int m_totalTrophies;

	// Token: 0x0400093F RID: 2367
	public int m_levelsCompleted;

	// Token: 0x04000940 RID: 2368
	public bool m_cloud;

	// Token: 0x04000941 RID: 2369
	public string m_name;

	// Token: 0x04000942 RID: 2370
	public string m_serviceName;
}
