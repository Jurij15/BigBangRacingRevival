using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000FB RID: 251
public class EventGiftResourceNitro : EventGiftResource
{
	// Token: 0x06000589 RID: 1417 RVA: 0x0004777A File Offset: 0x00045B7A
	public EventGiftResourceNitro(Dictionary<string, object> _dict)
	{
		base.ParseAmount(_dict);
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0004778C File Offset: 0x00045B8C
	public override void Claim(Hashtable _data)
	{
		if (this.m_amount > 0)
		{
			for (int i = 0; i < PsState.m_vehicleTypes.Length; i++)
			{
				PsMetagameManager.m_playerStats.CumulateBoosters(this.m_amount, PsState.m_vehicleTypes[i]);
			}
		}
		base.Claim(_data);
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x000477DB File Offset: 0x00045BDB
	public override string GetPicture()
	{
		return "menu_ shop_item_fill_boosters";
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x000477E2 File Offset: 0x00045BE2
	public override string GetName()
	{
		return PsStrings.Get(StringID.GACHA_OPEN_NITROS);
	}
}
