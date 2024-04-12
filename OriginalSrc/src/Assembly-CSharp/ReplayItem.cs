using System;
using System.Collections.Generic;

// Token: 0x02000546 RID: 1350
public class ReplayItem
{
	// Token: 0x060027B0 RID: 10160 RVA: 0x001AA603 File Offset: 0x001A8A03
	public ReplayItem(string _className)
	{
		this.m_className = _className;
		this.m_snapshots = new List<ReplaySnapshot>();
		this.m_currentSnapshotIndex = 0;
		this.m_events = new List<ReplayEventFrame>();
		this.m_currentEventIndex = 0;
	}

	// Token: 0x04002D1A RID: 11546
	public string m_className;

	// Token: 0x04002D1B RID: 11547
	public List<ReplaySnapshot> m_snapshots;

	// Token: 0x04002D1C RID: 11548
	public int m_currentSnapshotIndex;

	// Token: 0x04002D1D RID: 11549
	public List<ReplayEventFrame> m_events;

	// Token: 0x04002D1E RID: 11550
	public int m_currentEventIndex;
}
