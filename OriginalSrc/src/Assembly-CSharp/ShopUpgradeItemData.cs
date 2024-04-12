using System;

// Token: 0x02000140 RID: 320
public class ShopUpgradeItemData
{
	// Token: 0x06000A11 RID: 2577 RVA: 0x0006842F File Offset: 0x0006682F
	public ShopUpgradeItemData(string _identifier, int _purchaseCount)
	{
		this.m_identifier = _identifier;
		this.m_purchaseCount = _purchaseCount;
	}

	// Token: 0x04000938 RID: 2360
	public string m_identifier;

	// Token: 0x04000939 RID: 2361
	public int m_purchaseCount;
}
