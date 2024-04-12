using System;
using System.Collections.Generic;

// Token: 0x02000034 RID: 52
public class PsStreak
{
	// Token: 0x0600014C RID: 332 RVA: 0x0000FDB5 File Offset: 0x0000E1B5
	public PsStreak()
	{
		this.m_tier = -1;
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000FDC4 File Offset: 0x0000E1C4
	public void SetCoins(List<PsCoin> _coins)
	{
		this.m_coins = _coins;
		this.m_total = _coins.Count;
		this.m_collected = 0;
	}

	// Token: 0x0400012B RID: 299
	public List<PsCoin> m_coins;

	// Token: 0x0400012C RID: 300
	public int m_total;

	// Token: 0x0400012D RID: 301
	public int m_collected;

	// Token: 0x0400012E RID: 302
	public int m_tier;
}
