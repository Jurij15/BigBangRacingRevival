using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000FF RID: 255
public abstract class EventGiftTimed : EventGiftComponent
{
	// Token: 0x06000597 RID: 1431 RVA: 0x000478FC File Offset: 0x00045CFC
	public static EventGiftTimed GetTimedType(Dictionary<string, object> _dict)
	{
		if (_dict.ContainsKey("identifier"))
		{
			try
			{
				switch ((EventGiftTimedType)Enum.Parse(typeof(EventGiftTimedType), (string)_dict["identifier"]))
				{
				case EventGiftTimedType.goldCoinStreak:
					return new EventGiftTimedGoldStreak(_dict);
				case EventGiftTimedType.unlimitedNitros:
					return new EventGiftTimedUnlimitedNitros(_dict);
				case EventGiftTimedType.upgrade50discount:
					return new EventGiftTimedUpgradeInstallDiscount(_dict);
				case EventGiftTimedType.upgradeItem90discount:
					return new EventGiftTimedUpgradeItemCardDiscount(_dict);
				}
			}
			catch
			{
				Debug.LogError("parsing failed, string was: " + (string)_dict["identifier"]);
			}
		}
		else
		{
			Debug.LogError("Did not contain identifier");
		}
		return null;
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x000479D8 File Offset: 0x00045DD8
	public override void Claim(Hashtable _data)
	{
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x000479DA File Offset: 0x00045DDA
	private string GetPicture()
	{
		if (this.m_timedType == EventGiftTimedType.goldCoinStreak)
		{
			return "gold_graphics_image";
		}
		if (this.m_timedType == EventGiftTimedType.unlimitedNitros)
		{
			return "unlimited_nitros";
		}
		return string.Empty;
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00047A04 File Offset: 0x00045E04
	public override string GetName()
	{
		if (this.m_timedType == EventGiftTimedType.goldCoinStreak)
		{
			return PsStrings.Get(StringID.GIFT_TYPE_GOLD_DAY);
		}
		if (this.m_timedType == EventGiftTimedType.unlimitedNitros)
		{
			return PsStrings.Get(StringID.GIFT_TYPE_NITRO_DAY);
		}
		if (this.m_timedType == EventGiftTimedType.upgrade50discount)
		{
			return PsStrings.Get(StringID.GIFT_TYPE_UPGRADE_DAY);
		}
		if (this.m_timedType == EventGiftTimedType.upgradeItem90discount)
		{
			return PsStrings.Get(StringID.GIFT_TYPE_DISCOUNT_DAY);
		}
		return string.Empty;
	}

	// Token: 0x0400070F RID: 1807
	public EventGiftTimedType m_timedType;
}
