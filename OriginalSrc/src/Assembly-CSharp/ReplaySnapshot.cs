using System;
using System.Collections.Generic;

// Token: 0x02000548 RID: 1352
public class ReplaySnapshot
{
	// Token: 0x060027B2 RID: 10162 RVA: 0x001AA650 File Offset: 0x001A8A50
	public ReplaySnapshot()
	{
		this.m_states = new List<ReplayState>();
	}

	// Token: 0x04002D20 RID: 11552
	public int m_tick;

	// Token: 0x04002D21 RID: 11553
	public List<ReplayState> m_states;

	// Token: 0x04002D22 RID: 11554
	public bool m_alive;

	// Token: 0x04002D23 RID: 11555
	public bool m_electrified;

	// Token: 0x04002D24 RID: 11556
	public int m_boostTicks;
}
