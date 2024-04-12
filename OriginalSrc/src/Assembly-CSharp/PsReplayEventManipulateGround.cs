using System;

// Token: 0x0200054C RID: 1356
public class PsReplayEventManipulateGround : ReplayEvent
{
	// Token: 0x060027B6 RID: 10166 RVA: 0x001AA682 File Offset: 0x001A8A82
	public PsReplayEventManipulateGround()
		: base((ReplayEventType)32)
	{
	}

	// Token: 0x04002D31 RID: 11569
	public byte[] m_originalBytes;

	// Token: 0x04002D32 RID: 11570
	public byte[] m_changedBytes;
}
