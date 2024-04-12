using System;

// Token: 0x020002C7 RID: 711
public class PsPowerFuelData
{
	// Token: 0x060014FF RID: 5375 RVA: 0x000DB513 File Offset: 0x000D9913
	public PsPowerFuelData(int _price, int _cc, float _handicap, string _frameName, StringID _stringId, string _color)
	{
		this.m_price = _price;
		this.m_cc = _cc;
		this.m_handicap = _handicap;
		this.m_frameName = _frameName;
		this.m_stringID = _stringId;
		this.m_color = _color;
	}

	// Token: 0x040017B3 RID: 6067
	public int m_price;

	// Token: 0x040017B4 RID: 6068
	public int m_cc;

	// Token: 0x040017B5 RID: 6069
	public float m_handicap;

	// Token: 0x040017B6 RID: 6070
	public string m_frameName;

	// Token: 0x040017B7 RID: 6071
	public StringID m_stringID;

	// Token: 0x040017B8 RID: 6072
	public string m_color;
}
