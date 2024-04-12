using System;
using System.Collections.Generic;

// Token: 0x0200008D RID: 141
public static class PsNonConsumableIAPs
{
	// Token: 0x06000312 RID: 786 RVA: 0x0002FC44 File Offset: 0x0002E044
	public static void Initialize()
	{
		PsNonConsumableIAPs.m_items.Add(new NonConsumableIAP(StringID.SHOP_IAP_COINBOOSTER, "coin_booster", StringID.SHOP_IAP_COINBOOSTER_DESC, StringID.SHOP_IAP_COINBOOSTER_INFO, "menu_shop_banner_coindoubler", "menu_coindoubler_noconsume", true, true));
		PsNonConsumableIAPs.m_items.Add(new NonConsumableIAP(StringID.SHOP_IAP_DIRTBIKE_NAME, "dirt_bike_bundle", StringID.EMPTY, StringID.EMPTY, "menu_shop_banner_dirtbike_bundle", "menu_shop_dirtbike_bundle_motocross_helmet", false, false));
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0002FCB0 File Offset: 0x0002E0B0
	public static NonConsumableIAP FindItemWithStoreId(string _storeIdentifier)
	{
		foreach (NonConsumableIAP nonConsumableIAP in PsNonConsumableIAPs.m_items)
		{
			if (nonConsumableIAP.m_storeIdentifier.Equals(_storeIdentifier))
			{
				return nonConsumableIAP;
			}
		}
		return null;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x0002FD20 File Offset: 0x0002E120
	public static bool PlayerHasPurchasedItem(string _storeIdentifier)
	{
		NonConsumableIAP nonConsumableIAP = PsNonConsumableIAPs.FindItemWithStoreId(_storeIdentifier);
		if (nonConsumableIAP != null)
		{
			if (_storeIdentifier.Equals("coin_booster"))
			{
				return PsMetagameManager.m_playerStats.coinDoubler;
			}
			if (_storeIdentifier.Equals("dirt_bike_bundle"))
			{
				return PsMetagameManager.m_playerStats.dirtBikeBundle;
			}
		}
		return false;
	}

	// Token: 0x040003F0 RID: 1008
	public static List<NonConsumableIAP> m_items = new List<NonConsumableIAP>();
}
