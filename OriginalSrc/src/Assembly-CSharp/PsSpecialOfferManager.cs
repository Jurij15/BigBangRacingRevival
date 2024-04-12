using System;

// Token: 0x02000333 RID: 819
public static class PsSpecialOfferManager
{
	// Token: 0x060017EC RID: 6124 RVA: 0x0010230C File Offset: 0x0010070C
	public static PsSpecialOfferData GetSpecialOfferById(string _identifier)
	{
		if (_identifier.StartsWith("league_bundle"))
		{
			return PsSpecialOfferManager.GetLeagueSpecialOfferById(_identifier);
		}
		if (_identifier.StartsWith("car_bundle"))
		{
			return PsSpecialOfferManager.GetOffroadCarSpecialOfferById(_identifier);
		}
		if (_identifier.StartsWith("mc_bundle"))
		{
			return PsSpecialOfferManager.GetDirtbikeSpecialOfferById(_identifier);
		}
		return default(PsSpecialOfferData);
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x00102368 File Offset: 0x00100768
	public static PsSpecialOfferData GetOffroadCarSpecialOfferById(string _identifier)
	{
		string[] array = _identifier.Split(new char[] { '_' });
		switch (Convert.ToInt32(array[array.Length - 1]))
		{
		case 1:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 2:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 3:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 4:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 5:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 6:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 7:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		default:
			return default(PsSpecialOfferData);
		}
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x00102494 File Offset: 0x00100894
	public static PsSpecialOfferData GetDirtbikeSpecialOfferById(string _identifier)
	{
		string[] array = _identifier.Split(new char[] { '_' });
		switch (Convert.ToInt32(array[array.Length - 1]))
		{
		case 1:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 2:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 3:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 4:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 5:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 6:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		case 7:
			return new PsSpecialOfferData(10000, 200, "menu_shop_item_coins_tier2", "menu_shop_item_gems_tier1", null, null, 0);
		default:
			return default(PsSpecialOfferData);
		}
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x001025C0 File Offset: 0x001009C0
	public static PsSpecialOfferData GetLeagueSpecialOfferById(string _identifier)
	{
		string[] array = _identifier.Split(new char[] { '_' });
		switch (Convert.ToInt32(array[array.Length - 1]))
		{
		case 1:
			return new PsSpecialOfferData(8500, 135, "menu_shop_item_coins", "menu_shop_item_gems", null, null, 50);
		case 2:
			return new PsSpecialOfferData(10000, 160, "menu_shop_item_coins", "menu_shop_item_gems", null, null, 75);
		case 3:
			return new PsSpecialOfferData(11500, 180, "menu_shop_item_coins", "menu_shop_item_gems", null, null, 100);
		case 4:
			return new PsSpecialOfferData(31500, 500, "menu_shop_item_coins", "menu_shop_item_gems", null, null, 100);
		case 5:
			return new PsSpecialOfferData(40000, 625, "menu_shop_item_coins", "menu_shop_item_gems", null, null, 150);
		case 6:
			return new PsSpecialOfferData(47000, 750, "menu_shop_item_coins", "menu_shop_item_gems", null, null, 200);
		case 7:
			return new PsSpecialOfferData(55000, 875, "menu_shop_item_coins", "menu_shop_item_gems", null, null, 250);
		default:
			return default(PsSpecialOfferData);
		}
	}

	// Token: 0x060017F0 RID: 6128 RVA: 0x001026F9 File Offset: 0x00100AF9
	public static string GetBundleIdentifier(Type _vehicleType, int _id)
	{
		if (_vehicleType == typeof(OffroadCar) || _vehicleType == typeof(Motorcycle))
		{
			return "league_bundle_" + _id;
		}
		return null;
	}
}
