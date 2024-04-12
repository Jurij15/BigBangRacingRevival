using System;

// Token: 0x0200054B RID: 1355
public class ReplayEvent
{
	// Token: 0x060027B5 RID: 10165 RVA: 0x001AA673 File Offset: 0x001A8A73
	public ReplayEvent(ReplayEventType _type)
	{
		this.m_type = _type;
	}

	// Token: 0x04002D30 RID: 11568
	public ReplayEventType m_type;
}
