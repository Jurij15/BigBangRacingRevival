using System;

// Token: 0x02000547 RID: 1351
public class PsReplayUnit : ReplayItem
{
	// Token: 0x060027B1 RID: 10161 RVA: 0x001AA636 File Offset: 0x001A8A36
	public PsReplayUnit(Unit _unit)
		: base(_unit.GetType().ToString())
	{
		this.m_unit = _unit;
	}

	// Token: 0x04002D1F RID: 11551
	public Unit m_unit;
}
