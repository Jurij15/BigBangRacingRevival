using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000FC RID: 252
public class EventGiftResourceCoin : EventGiftResource
{
	// Token: 0x0600058D RID: 1421 RVA: 0x000477EB File Offset: 0x00045BEB
	public EventGiftResourceCoin(Dictionary<string, object> _dict)
	{
		base.ParseAmount(_dict);
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x000477FA File Offset: 0x00045BFA
	public override void Claim(Hashtable _data)
	{
		PsMetagameManager.m_playerStats.CumulateCoins(this.m_amount);
		base.Claim(_data);
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00047814 File Offset: 0x00045C14
	private int GetIndex()
	{
		int num = 1;
		if (this.m_amount > 95000)
		{
			num = 5;
		}
		else if (this.m_amount > 9500)
		{
			num = 3;
		}
		return num;
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x0004784D File Offset: 0x00045C4D
	public override string GetPicture()
	{
		return "menu_shop_item_coins_tier" + this.GetIndex();
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00047864 File Offset: 0x00045C64
	public override string GetName()
	{
		int index = this.GetIndex();
		if (index == 3)
		{
			return PsStrings.Get(StringID.SHOP_COINS_BARREL);
		}
		if (index != 5)
		{
			return PsStrings.Get(StringID.SHOP_COINS_JAR);
		}
		return PsStrings.Get(StringID.SHOP_COINS_VAULT);
	}
}
