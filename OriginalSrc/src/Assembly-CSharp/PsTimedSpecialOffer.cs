using System;

// Token: 0x02000335 RID: 821
public class PsTimedSpecialOffer
{
	// Token: 0x04001AC9 RID: 6857
	public const int NEW = 0;

	// Token: 0x04001ACA RID: 6858
	public const int ACTIVE = 1;

	// Token: 0x04001ACB RID: 6859
	public const int OLD = 2;

	// Token: 0x04001ACC RID: 6860
	public int m_state;

	// Token: 0x04001ACD RID: 6861
	public string m_productId;

	// Token: 0x04001ACE RID: 6862
	public long m_startTime;

	// Token: 0x04001ACF RID: 6863
	public int m_timeLeft;
}
