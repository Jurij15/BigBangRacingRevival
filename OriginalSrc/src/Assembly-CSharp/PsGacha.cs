using System;

// Token: 0x020000A5 RID: 165
public class PsGacha
{
	// Token: 0x0600036F RID: 879 RVA: 0x00032640 File Offset: 0x00030A40
	public PsGacha(GachaType _gachaType)
	{
		this.m_gachaType = _gachaType;
		switch (this.m_gachaType)
		{
		case GachaType.WOOD:
		case GachaType.BOSS:
			this.m_unlockTime = 0;
			break;
		case GachaType.BRONZE:
			this.m_unlockTime = 3600;
			break;
		case GachaType.SILVER:
			this.m_unlockTime = 10800;
			break;
		case GachaType.GOLD:
			this.m_unlockTime = 28800;
			break;
		case GachaType.RARE:
			this.m_unlockTime = 43200;
			break;
		case GachaType.EPIC:
			this.m_unlockTime = 43200;
			break;
		case GachaType.SUPER:
			this.m_unlockTime = 86400;
			break;
		}
		this.m_unlockTimeLeft = this.m_unlockTime;
	}

	// Token: 0x04000458 RID: 1112
	public int m_unlockTime;

	// Token: 0x04000459 RID: 1113
	public double m_unlockStartedTime;

	// Token: 0x0400045A RID: 1114
	public int m_unlockTimeLeft;

	// Token: 0x0400045B RID: 1115
	public bool m_unlocked;

	// Token: 0x0400045C RID: 1116
	public bool m_notified;

	// Token: 0x0400045D RID: 1117
	public GachaType m_gachaType;
}
