using System;

// Token: 0x02000097 RID: 151
public class StatModifierInfo
{
	// Token: 0x0600032C RID: 812 RVA: 0x000301FF File Offset: 0x0002E5FF
	public StatModifierInfo(StatModifier _modifier)
	{
		this.m_modifier = _modifier;
		this.m_endTime = -1f;
	}

	// Token: 0x0400041E RID: 1054
	public StatModifier m_modifier;

	// Token: 0x0400041F RID: 1055
	public float m_endTime;

	// Token: 0x04000420 RID: 1056
	public int m_stack;

	// Token: 0x04000421 RID: 1057
	public int m_maxStack;
}
