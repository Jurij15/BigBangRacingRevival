using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class PsCoin
{
	// Token: 0x0600014E RID: 334 RVA: 0x0000FDE0 File Offset: 0x0000E1E0
	public PsCoin(Vector2 _pos, PsStreak _streak)
	{
		this.m_streak = _streak;
		this.m_pos = _pos;
		this.m_collected = false;
		this.m_following = false;
		this.m_type = CoinType.COPPER;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000FE0B File Offset: 0x0000E20B
	public void SetType(CoinType _type)
	{
		this.m_type = _type;
	}

	// Token: 0x0400012F RID: 303
	public PsStreak m_streak;

	// Token: 0x04000130 RID: 304
	public Vector2 m_pos;

	// Token: 0x04000131 RID: 305
	public bool m_collected;

	// Token: 0x04000132 RID: 306
	public bool m_following;

	// Token: 0x04000133 RID: 307
	public CoinType m_type;
}
