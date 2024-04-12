using System;

// Token: 0x02000136 RID: 310
public class PsEditorItem : PsUnlockable
{
	// Token: 0x06000963 RID: 2403 RVA: 0x00064CD7 File Offset: 0x000630D7
	public PsEditorItem(string _name)
		: base(PsUnlockableType.Unit, _name)
	{
		this.m_orderIndex = 0;
	}

	// Token: 0x040008CC RID: 2252
	public string m_graphNodeClassName;

	// Token: 0x040008CD RID: 2253
	public int m_maxAmount;

	// Token: 0x040008CE RID: 2254
	public int m_complexity;

	// Token: 0x040008CF RID: 2255
	public int m_orderIndex;

	// Token: 0x040008D0 RID: 2256
	public float m_gachaProbability;

	// Token: 0x040008D1 RID: 2257
	public PsRarity m_rarity;

	// Token: 0x040008D2 RID: 2258
	public PsCurrency m_currency;

	// Token: 0x040008D3 RID: 2259
	public int m_price;
}
