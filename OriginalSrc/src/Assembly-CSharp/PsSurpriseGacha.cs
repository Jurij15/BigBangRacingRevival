using System;
using System.Collections.Generic;

// Token: 0x020000AC RID: 172
public class PsSurpriseGacha : PsRouletteGacha
{
	// Token: 0x0600038D RID: 909 RVA: 0x00035B18 File Offset: 0x00033F18
	private static GachaMachine<GachaType> GetChestGachaMachine()
	{
		GachaMachine<GachaType> gachaMachine = new GachaMachine<GachaType>();
		gachaMachine.AddItem(GachaType.COMMON, 37.5f, -1);
		gachaMachine.AddItem(GachaType.BRONZE, 30f, -1);
		gachaMachine.AddItem(GachaType.SILVER, 20f, -1);
		gachaMachine.AddItem(GachaType.GOLD, 10f, -1);
		gachaMachine.AddItem(GachaType.RARE, 2f, -1);
		gachaMachine.AddItem(GachaType.EPIC, 0.4f, -1);
		gachaMachine.AddItem(GachaType.SUPER, 0.1f, -1);
		return gachaMachine;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00035B88 File Offset: 0x00033F88
	public static PsGacha GetSurpriceChest()
	{
		GachaMachine<GachaType> chestGachaMachine = PsSurpriseGacha.GetChestGachaMachine();
		GachaType item = chestGachaMachine.GetItem(true);
		PsGacha psGacha = new PsGacha(item);
		psGacha.m_unlockTime = (psGacha.m_unlockTimeLeft = PsSurpriseGacha.GetUnlockTime());
		return psGacha;
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00035BBF File Offset: 0x00033FBF
	private static int GetUnlockTime()
	{
		return 14400;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00035BC8 File Offset: 0x00033FC8
	public static List<GachaType> GetVisualChestTypes(GachaType _prize, int _count)
	{
		GachaMachine<GachaType> chestGachaMachine = PsSurpriseGacha.GetChestGachaMachine();
		Dictionary<GachaType, int> several = chestGachaMachine.GetSeveral(_count, _count, 6);
		List<GachaType> list = PsRouletteGacha.ConvertDictionaryToList<GachaType>(several);
		return PsRouletteGacha.GetListInRandomOrder<GachaType>(list, _prize, null);
	}
}
