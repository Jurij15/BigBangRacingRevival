using System;
using System.Collections.Generic;

// Token: 0x02000101 RID: 257
public class EventGiftTimedUnlimitedNitros : EventGiftTimed
{
	// Token: 0x0600059F RID: 1439 RVA: 0x00047BA1 File Offset: 0x00045FA1
	public EventGiftTimedUnlimitedNitros(Dictionary<string, object> _dict)
	{
		this.m_timedType = EventGiftTimedType.unlimitedNitros;
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00047BB0 File Offset: 0x00045FB0
	public override string GetName()
	{
		return PsStrings.Get(StringID.GIFT_TYPE_NITRO_DAY);
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00047BBC File Offset: 0x00045FBC
	public override void CreateUI(UIComponent _parent)
	{
		UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_ shop_item_fill_boosters", null), true, true);
	}
}
