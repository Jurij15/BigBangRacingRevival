using System;
using System.Collections.Generic;

// Token: 0x020000A4 RID: 164
public class PsCoinRoulette : PsRouletteGacha
{
	// Token: 0x06000364 RID: 868 RVA: 0x00032248 File Offset: 0x00030648
	private static GachaMachine<CoinStreakStyle> GetCoinGachaMachine()
	{
		GachaMachine<CoinStreakStyle> gachaMachine = new GachaMachine<CoinStreakStyle>();
		gachaMachine.AddItem(CoinStreakStyle.COPPER_AND_GOLD, 40f, -1);
		gachaMachine.AddItem(CoinStreakStyle.ALL_GOLD, 40f, -1);
		gachaMachine.AddItem(CoinStreakStyle.GOLD_HOARDER, 10f, -1);
		gachaMachine.AddItem(CoinStreakStyle.GOLD_MANIAC, 5f, -1);
		gachaMachine.AddItem(CoinStreakStyle.ROYAL, 3f, -1);
		gachaMachine.AddItem(CoinStreakStyle.ALL_DIAMONDS, 0.5f, -1);
		return gachaMachine;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x000322AC File Offset: 0x000306AC
	public static CoinStreakStyle GetSurpriceCoinstreak()
	{
		GachaMachine<CoinStreakStyle> coinGachaMachine = PsCoinRoulette.GetCoinGachaMachine();
		return coinGachaMachine.GetItem(true);
	}

	// Token: 0x06000366 RID: 870 RVA: 0x000322C8 File Offset: 0x000306C8
	public static List<CoinStreakStyle> GetVisualCoinTypes(CoinStreakStyle _prize, int _count)
	{
		GachaMachine<CoinStreakStyle> coinGachaMachine = PsCoinRoulette.GetCoinGachaMachine();
		Dictionary<CoinStreakStyle, int> several = coinGachaMachine.GetSeveral(_count, _count, 6);
		List<CoinStreakStyle> list = PsRouletteGacha.ConvertDictionaryToList<CoinStreakStyle>(several);
		return PsRouletteGacha.GetListInRandomOrder<CoinStreakStyle>(list, _prize, null);
	}

	// Token: 0x06000367 RID: 871 RVA: 0x000322F4 File Offset: 0x000306F4
	public static string GetDescription(CoinStreakStyle _style)
	{
		switch (_style)
		{
		case CoinStreakStyle.COPPER_AND_GOLD:
			return PsStrings.Get(StringID.SPIN_COPPER_GOLD_DESC);
		case CoinStreakStyle.ALL_GOLD:
			return PsStrings.Get(StringID.SPIN_ALL_GOLD_DESC);
		case CoinStreakStyle.GOLD_HOARDER:
			return PsStrings.Get(StringID.SPIN_GOLD_HOARDER_DESC);
		case CoinStreakStyle.GOLD_MANIAC:
			return PsStrings.Get(StringID.SPIN_GOLD_MANIAC_DESC);
		case CoinStreakStyle.ROYAL:
			return PsStrings.Get(StringID.SPIN_ROYAL_DESC);
		case CoinStreakStyle.ALL_DIAMONDS:
			return PsStrings.Get(StringID.SPIN_ALL_GEMS_DESC);
		default:
			return "<color=red>error<color> with description: " + _style.ToString();
		}
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00032380 File Offset: 0x00030780
	public static string GetName(CoinStreakStyle _style)
	{
		switch (_style)
		{
		case CoinStreakStyle.COPPER_AND_GOLD:
			return PsStrings.Get(StringID.SPIN_COPPER_GOLD).ToUpper();
		case CoinStreakStyle.ALL_GOLD:
			return PsStrings.Get(StringID.SPIN_ALL_GOLD).ToUpper();
		case CoinStreakStyle.GOLD_HOARDER:
			return PsStrings.Get(StringID.SPIN_GOLD_HOARDER).ToUpper();
		case CoinStreakStyle.GOLD_MANIAC:
			return PsStrings.Get(StringID.SPIN_GOLD_MANIAC).ToUpper();
		case CoinStreakStyle.ROYAL:
			return PsStrings.Get(StringID.SPIN_ROYAL).ToUpper();
		case CoinStreakStyle.ALL_DIAMONDS:
			return PsStrings.Get(StringID.SPIN_ALL_GEMS).ToUpper();
		default:
			return "<color=red>error<color> with description: " + _style.ToString();
		}
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0003242C File Offset: 0x0003082C
	private static bool IsUnlocked()
	{
		string text = PlanetTools.PlanetTypes.AdventureOffroadCar.ToString();
		string text2 = PlanetTools.PlanetTypes.AdventureMotorcycle.ToString();
		bool flag = PlanetTools.m_planetProgressionInfos[text].m_mainPath.m_currentNodeId >= PsCoinRoulette.m_unlockLevelID;
		bool flag2 = PlanetTools.m_planetProgressionInfos[text2].m_mainPath.m_currentNodeId >= PsCoinRoulette.m_unlockLevelID;
		return flag || flag2;
	}

	// Token: 0x0600036A RID: 874 RVA: 0x000324A8 File Offset: 0x000308A8
	public static bool ShowPopupAd(PsGameLoop _loop)
	{
		if (_loop.m_path == null)
		{
			return false;
		}
		string text = PlanetTools.PlanetTypes.AdventureOffroadCar.ToString();
		string text2 = PlanetTools.PlanetTypes.AdventureMotorcycle.ToString();
		return PlanetTools.m_planetProgressionInfos[text].m_mainPath.m_currentNodeId <= PsCoinRoulette.m_unlockLevelID && PlanetTools.m_planetProgressionInfos[text].m_mainPath.m_currentNodeId <= PsCoinRoulette.m_unlockLevelID && _loop.m_nodeId == PsCoinRoulette.m_unlockLevelID;
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00032534 File Offset: 0x00030934
	public static bool IsAvailable(PsGameLoop _loop)
	{
		bool flag = _loop.m_gameMode.GetType() == typeof(PsGameModeAdventure);
		bool flag2 = _loop.m_path != null && _loop.m_nodeId == _loop.m_path.m_currentNodeId;
		bool flag3 = PsAdMediation.adsAvailable();
		if (!flag3)
		{
			PsMetrics.AdNotAvailable("coinRoulette");
		}
		bool flag4 = _loop.m_gameMode.m_playbackGhosts.Count > 0;
		bool flag5 = PsCoinRoulette.AvailableForThisLevel(_loop);
		return flag && flag2 && flag3 && flag4 && flag5 && PsCoinRoulette.IsUnlocked();
	}

	// Token: 0x0600036C RID: 876 RVA: 0x000325D8 File Offset: 0x000309D8
	public static void WasSpinned(PsGameLoop _loop)
	{
		if (_loop.m_path != null)
		{
			PlayerPrefsX.AdUsedForLevel(_loop.m_path.m_planet, _loop.m_nodeId);
		}
	}

	// Token: 0x0600036D RID: 877 RVA: 0x000325FC File Offset: 0x000309FC
	private static bool AvailableForThisLevel(PsGameLoop _loop)
	{
		if (_loop.m_path == null)
		{
			return false;
		}
		int num = PlayerPrefsX.AdUsedLastLevel(_loop.m_path.m_planet);
		return _loop.m_nodeId > num;
	}

	// Token: 0x04000457 RID: 1111
	public static int m_unlockLevelID = 4;
}
