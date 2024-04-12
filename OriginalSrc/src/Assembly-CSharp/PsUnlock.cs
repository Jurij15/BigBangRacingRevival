using System;
using System.Collections.Generic;

// Token: 0x02000133 RID: 307
public class PsUnlock
{
	// Token: 0x06000960 RID: 2400 RVA: 0x00064C87 File Offset: 0x00063087
	public PsUnlock(string _identifier)
	{
		this.m_name = _identifier;
		this.m_unlockables = new List<PsUnlockable>();
		this.m_levels = new List<MetagameNodeData>();
	}

	// Token: 0x040008BD RID: 2237
	public string m_name;

	// Token: 0x040008BE RID: 2238
	public List<PsUnlockable> m_unlockables;

	// Token: 0x040008BF RID: 2239
	public List<MetagameNodeData> m_levels;

	// Token: 0x040008C0 RID: 2240
	public MetagameNodeData m_introLevel;

	// Token: 0x040008C1 RID: 2241
	public bool m_unlocked;
}
