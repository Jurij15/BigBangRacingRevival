using System;

// Token: 0x02000332 RID: 818
public struct PsSpecialOfferData
{
	// Token: 0x060017EB RID: 6123 RVA: 0x001022D2 File Offset: 0x001006D2
	public PsSpecialOfferData(int _coins = 0, int _gems = 0, string _coinIconId = null, string _gemIconId = null, string _hatId = null, string _trailId = null, int _percentage = 0)
	{
		this.coins = _coins;
		this.gems = _gems;
		this.coinIconIdentifier = _coinIconId;
		this.gemIconIdentifier = _gemIconId;
		this.hatIdentifier = _hatId;
		this.trailIdentifier = _trailId;
		this.percentage = _percentage;
	}

	// Token: 0x04001ABD RID: 6845
	public int coins;

	// Token: 0x04001ABE RID: 6846
	public int gems;

	// Token: 0x04001ABF RID: 6847
	public int percentage;

	// Token: 0x04001AC0 RID: 6848
	public string coinIconIdentifier;

	// Token: 0x04001AC1 RID: 6849
	public string gemIconIdentifier;

	// Token: 0x04001AC2 RID: 6850
	public string hatIdentifier;

	// Token: 0x04001AC3 RID: 6851
	public string trailIdentifier;
}
