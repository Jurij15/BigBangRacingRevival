using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000FD RID: 253
public class EventGiftResourceGem : EventGiftResource
{
	// Token: 0x06000592 RID: 1426 RVA: 0x000478AB File Offset: 0x00045CAB
	public EventGiftResourceGem(Dictionary<string, object> _dict)
	{
		base.ParseAmount(_dict);
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x000478BA File Offset: 0x00045CBA
	public override void Claim(Hashtable _data)
	{
		PsMetagameManager.m_playerStats.CumulateDiamonds(this.m_amount);
		base.Claim(_data);
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x000478D3 File Offset: 0x00045CD3
	public override string GetPicture()
	{
		return "menu_shop_item_gems_tier" + 1;
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x000478E5 File Offset: 0x00045CE5
	public override string GetName()
	{
		return PsStrings.Get(StringID.SHOP_IAP_SACK);
	}
}
