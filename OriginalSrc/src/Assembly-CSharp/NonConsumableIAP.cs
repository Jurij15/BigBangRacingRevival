using System;

// Token: 0x0200008C RID: 140
public class NonConsumableIAP
{
	// Token: 0x06000311 RID: 785 RVA: 0x0002FBF4 File Offset: 0x0002DFF4
	public NonConsumableIAP(StringID _displayNameID, string _storeIdentifier, StringID _descriptionID, StringID _playerOwnsID, string _bannerFrame, string _frame, bool _showPrice = true, bool _leaveToShopAsInactive = true)
	{
		this.m_displayNameID = _displayNameID;
		this.m_storeIdentifier = _storeIdentifier;
		this.m_descriptionID = _descriptionID;
		this.m_playerOwnsID = _playerOwnsID;
		this.m_bannerFrame = _bannerFrame;
		this.m_purchasedPopupFrame = _frame;
		this.m_showPrice = _showPrice;
		this.m_leaveToShop = _leaveToShopAsInactive;
	}

	// Token: 0x040003E8 RID: 1000
	public StringID m_displayNameID;

	// Token: 0x040003E9 RID: 1001
	public string m_storeIdentifier;

	// Token: 0x040003EA RID: 1002
	public StringID m_descriptionID;

	// Token: 0x040003EB RID: 1003
	public StringID m_playerOwnsID;

	// Token: 0x040003EC RID: 1004
	public string m_bannerFrame;

	// Token: 0x040003ED RID: 1005
	public string m_purchasedPopupFrame;

	// Token: 0x040003EE RID: 1006
	public bool m_showPrice;

	// Token: 0x040003EF RID: 1007
	public bool m_leaveToShop;
}
