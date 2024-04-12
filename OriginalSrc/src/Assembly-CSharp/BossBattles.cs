using System;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public static class BossBattles
{
	// Token: 0x06000555 RID: 1365 RVA: 0x0004630C File Offset: 0x0004470C
	public static void ParseConfig(Dictionary<string, object> _config)
	{
		if (_config.ContainsKey("bossMaxAddedSpeed"))
		{
			BossBattles.bossMaxAddedSpeed = Convert.ToSingle(_config["bossMaxAddedSpeed"]);
		}
		if (_config.ContainsKey("restartRandomHandicap"))
		{
			BossBattles.restartRandomHandicap = Convert.ToSingle(_config["restartRandomHandicap"]);
		}
		if (_config.ContainsKey("winBossRandomHandicap"))
		{
			BossBattles.winBossRandomHandicap = Convert.ToSingle(_config["winBossRandomHandicap"]);
		}
		if (_config.ContainsKey("checkPointBoostChance"))
		{
			BossBattles.checkPointBoostChance = Convert.ToSingle(_config["checkPointBoostChance"]);
		}
		else
		{
			Debug.Log("BossDebug: No checkPointBoostChance", null);
		}
		if (_config.ContainsKey("checkPointCoinChance"))
		{
			BossBattles.checkPointCoinChance = Convert.ToSingle(_config["checkPointCoinChance"]);
		}
		else
		{
			Debug.Log("BossDebug: no checkPointCoinChance", null);
		}
		if (_config.ContainsKey("checkPointCoinAmount"))
		{
			BossBattles.checkPointCoinAmount = Convert.ToInt32(_config["checkPointCoinAmount"]);
		}
		else
		{
			Debug.Log("BossDebug: no checkPointCoinAmount", null);
		}
		if (_config.ContainsKey("globalHandicap"))
		{
			BossBattles.globalHandicap = Convert.ToSingle(_config["globalHandicap"]);
		}
		if (_config.ContainsKey("maxHandicap"))
		{
			BossBattles.maxHandicap = Convert.ToSingle(_config["maxHandicap"]);
		}
		if (_config.ContainsKey("minHandicap"))
		{
			BossBattles.minHandicap = Convert.ToSingle(_config["minHandicap"]);
		}
		if (_config.ContainsKey("startHandicap"))
		{
			BossBattles.startHandicap = Convert.ToSingle(_config["startHandicap"]);
		}
		if (_config.ContainsKey("timeFreezerPowerUpBig"))
		{
			BossBattles.levelHandicapPerHour = Convert.ToSingle(_config["timeFreezerPowerUpBig"]);
		}
		if (_config.ContainsKey("timeSlowerPowerUpBig"))
		{
			BossBattles.maxLevelHandicap = Convert.ToSingle(_config["timeSlowerPowerUpBig"]);
		}
		if (_config.ContainsKey("timeBoosterPowerUpBig"))
		{
			BossBattles.goalReachedHandicap = Convert.ToSingle(_config["timeBoosterPowerUpBig"]);
		}
		if (_config.ContainsKey("timeFreezerPowerUp"))
		{
			BossBattles.maxGoalReachedHandicap = Convert.ToSingle(_config["timeFreezerPowerUp"]);
		}
		if (_config.ContainsKey("timeSlowerPowerUp"))
		{
			BossBattles.tryHandicap = Convert.ToSingle(_config["timeSlowerPowerUp"]);
		}
		if (_config.ContainsKey("timeBoosterPowerUp"))
		{
			BossBattles.maxTryHandicap = Convert.ToSingle(_config["timeBoosterPowerUp"]);
		}
		if (_config.ContainsKey("winActionChange"))
		{
			BossBattles.winActionChange = Convert.ToSingle(_config["winActionChange"]);
		}
		if (_config.ContainsKey("instaOpenChestChange"))
		{
			BossBattles.instaOpenChestChange = Convert.ToSingle(_config["instaOpenChestChange"]);
		}
		if (_config.ContainsKey("openChestChange"))
		{
			BossBattles.openChestChange = Convert.ToSingle(_config["openChestChange"]);
		}
		if (_config.ContainsKey("buyChestChange1"))
		{
			BossBattles.buyChestChange1 = Convert.ToSingle(_config["buyChestChange1"]);
		}
		if (_config.ContainsKey("buyChestChange2"))
		{
			BossBattles.buyChestChange2 = Convert.ToSingle(_config["buyChestChange2"]);
		}
		if (_config.ContainsKey("buyChestChange3"))
		{
			BossBattles.buyChestChange3 = Convert.ToSingle(_config["buyChestChange3"]);
		}
		if (_config.ContainsKey("upgradeVehicleChange"))
		{
			BossBattles.upgradeVehicleChange = Convert.ToSingle(_config["upgradeVehicleChange"]);
		}
		if (_config.ContainsKey("tuneUp1Change"))
		{
			BossBattles.tuneUp1Change = Convert.ToSingle(_config["tuneUp1Change"]);
		}
		if (_config.ContainsKey("tuneUp2Change"))
		{
			BossBattles.tuneUp2Change = Convert.ToSingle(_config["tuneUp2Change"]);
		}
		if (_config.ContainsKey("tuneUp3Change"))
		{
			BossBattles.tuneUp3Change = Convert.ToSingle(_config["tuneUp3Change"]);
		}
		if (_config.ContainsKey("powerupPrice"))
		{
			BossBattles.powerupPrice = Convert.ToInt32(_config["powerupPrice"]);
		}
		if (_config.ContainsKey("tuneUp1Price"))
		{
			BossBattles.tuneUp1Price = Convert.ToInt32(_config["tuneUp1Price"]);
		}
		if (_config.ContainsKey("tuneUp2Price"))
		{
			BossBattles.tuneUp2Price = Convert.ToInt32(_config["tuneUp2Price"]);
		}
		if (_config.ContainsKey("tuneUp3Price"))
		{
			BossBattles.tuneUp3Price = Convert.ToInt32(_config["tuneUp3Price"]);
		}
		if (_config.ContainsKey("powerupEffectiveRuns"))
		{
			BossBattles.powerupEffectiveRuns = Convert.ToInt32(_config["powerupEffectiveRuns"]);
		}
		if (_config.ContainsKey("tuneUp1EffectiveRuns"))
		{
			BossBattles.tuneUp1EffectiveRuns = Convert.ToInt32(_config["tuneUp1EffectiveRuns"]);
		}
		if (_config.ContainsKey("tuneUp2EffectiveRuns"))
		{
			BossBattles.tuneUp2EffectiveRuns = Convert.ToInt32(_config["tuneUp2EffectiveRuns"]);
		}
		if (_config.ContainsKey("tuneUp3EffectiveRuns"))
		{
			BossBattles.tuneUp3EffectiveRuns = Convert.ToInt32(_config["tuneUp3EffectiveRuns"]);
		}
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00046847 File Offset: 0x00044C47
	public static float GetFreezeDuration(float _handicap)
	{
		return Mathf.Max(0f, Mathf.Min(BossBattles.maxHandicap, _handicap));
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0004685E File Offset: 0x00044C5E
	public static float GetLevelHandicap(float _hoursPassed)
	{
		return Mathf.Max(0f, Mathf.Min(BossBattles.levelHandicapPerHour * _hoursPassed, BossBattles.maxLevelHandicap));
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0004687B File Offset: 0x00044C7B
	public static float GetGoalReachedHandicap(int _count)
	{
		return Mathf.Min(BossBattles.goalReachedHandicap * (float)_count, BossBattles.maxGoalReachedHandicap);
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x00046890 File Offset: 0x00044C90
	public static float GetBossWinRandomHandicap()
	{
		return Random.Range(0f, BossBattles.winBossRandomHandicap * 2f) - BossBattles.winBossRandomHandicap;
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x000468BA File Offset: 0x00044CBA
	public static float GetRandomRuntimeHandicap()
	{
		return Random.Range(0f, BossBattles.restartRandomHandicap * 2f) - BossBattles.restartRandomHandicap;
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x000468D7 File Offset: 0x00044CD7
	public static float GetTryCountHandicap(int _tries)
	{
		return Mathf.Min(BossBattles.maxTryHandicap, (float)_tries * BossBattles.tryHandicap);
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x000468EB File Offset: 0x00044CEB
	public static float GachaTypeHandicap(GachaType _gacha)
	{
		switch (_gacha)
		{
		case GachaType.RARE:
			return BossBattles.buyChestChange1;
		case GachaType.EPIC:
			return BossBattles.buyChestChange2;
		case GachaType.SUPER:
			return BossBattles.buyChestChange3;
		default:
			return 0f;
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0004691D File Offset: 0x00044D1D
	public static void AlterBothVehicleHandicaps(float _amount)
	{
		BossBattles.AlterHandicap(typeof(OffroadCar), _amount);
		BossBattles.AlterHandicap(typeof(Motorcycle), _amount);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00046940 File Offset: 0x00044D40
	public static void AlterHandicap(Type _vehicle, float _amount)
	{
		if (_vehicle == typeof(Motorcycle))
		{
			PsMetagameManager.m_playerStats.mcHandicap = Mathf.Min(BossBattles.maxHandicap, Mathf.Max(BossBattles.minHandicap, PsMetagameManager.m_playerStats.mcHandicap + _amount));
		}
		else if (_vehicle == typeof(OffroadCar))
		{
			PsMetagameManager.m_playerStats.carHandicap = Mathf.Min(BossBattles.maxHandicap, Mathf.Max(BossBattles.minHandicap, PsMetagameManager.m_playerStats.carHandicap + _amount));
		}
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x000469C8 File Offset: 0x00044DC8
	public static float GetCurrentVehicleHandicap()
	{
		if (PsState.m_activeMinigame.m_playerUnit is OffroadCar)
		{
			return PsMetagameManager.m_playerStats.carHandicap;
		}
		if (PsState.m_activeMinigame.m_playerUnit is Motorcycle)
		{
			return PsMetagameManager.m_playerStats.mcHandicap;
		}
		return 0f;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00046A18 File Offset: 0x00044E18
	public static float GetVehicleHandicap(Type _vehicle)
	{
		if (_vehicle == typeof(OffroadCar))
		{
			return PsMetagameManager.m_playerStats.carHandicap;
		}
		if (_vehicle == typeof(Motorcycle))
		{
			return PsMetagameManager.m_playerStats.mcHandicap;
		}
		return 0f;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00046A55 File Offset: 0x00044E55
	public static float GetGlobalHandicap()
	{
		return BossBattles.globalHandicap;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00046A5C File Offset: 0x00044E5C
	public static float HandicapBounds(float _handicap)
	{
		return Mathf.Min(Mathf.Max(BossBattles.minHandicap, _handicap), BossBattles.maxHandicap);
	}

	// Token: 0x040006DC RID: 1756
	public static float globalHandicap = 0f;

	// Token: 0x040006DD RID: 1757
	public static float maxHandicap = 10f;

	// Token: 0x040006DE RID: 1758
	public static float minHandicap = -10f;

	// Token: 0x040006DF RID: 1759
	public static float startHandicap = 3f;

	// Token: 0x040006E0 RID: 1760
	public static float levelHandicapPerHour = 0.05f;

	// Token: 0x040006E1 RID: 1761
	public static float maxLevelHandicap = 1.5f;

	// Token: 0x040006E2 RID: 1762
	public static float goalReachedHandicap = 0.5f;

	// Token: 0x040006E3 RID: 1763
	public static float maxGoalReachedHandicap = 6f;

	// Token: 0x040006E4 RID: 1764
	public static float tryHandicap = 0.025f;

	// Token: 0x040006E5 RID: 1765
	public static float maxTryHandicap = 0.25f;

	// Token: 0x040006E6 RID: 1766
	public static float restartRandomHandicap = 0.25f;

	// Token: 0x040006E7 RID: 1767
	public static float winBossRandomHandicap = 1.5f;

	// Token: 0x040006E8 RID: 1768
	public static float winActionChange = -2.5f;

	// Token: 0x040006E9 RID: 1769
	public static float openChestChange = 1f;

	// Token: 0x040006EA RID: 1770
	public static float instaOpenChestChange = 1.25f;

	// Token: 0x040006EB RID: 1771
	public static float buyChestChange1 = 3f;

	// Token: 0x040006EC RID: 1772
	public static float buyChestChange2 = 4f;

	// Token: 0x040006ED RID: 1773
	public static float buyChestChange3 = 5f;

	// Token: 0x040006EE RID: 1774
	public static float upgradeVehicleChange = 1f;

	// Token: 0x040006EF RID: 1775
	public static float tuneUp1Change = 1f;

	// Token: 0x040006F0 RID: 1776
	public static float tuneUp2Change = 2f;

	// Token: 0x040006F1 RID: 1777
	public static float tuneUp3Change = 3f;

	// Token: 0x040006F2 RID: 1778
	public static ObscuredInt powerupPrice = 6;

	// Token: 0x040006F3 RID: 1779
	public static ObscuredInt tuneUp1Price = 10;

	// Token: 0x040006F4 RID: 1780
	public static ObscuredInt tuneUp2Price = 40;

	// Token: 0x040006F5 RID: 1781
	public static ObscuredInt tuneUp3Price = 120;

	// Token: 0x040006F6 RID: 1782
	public static int powerupEffectiveRuns = 1;

	// Token: 0x040006F7 RID: 1783
	public static int tuneUp1EffectiveRuns = 5;

	// Token: 0x040006F8 RID: 1784
	public static int tuneUp2EffectiveRuns = 5;

	// Token: 0x040006F9 RID: 1785
	public static int tuneUp3EffectiveRuns = 5;

	// Token: 0x040006FA RID: 1786
	public static float checkPointBoostChance = 0.1f;

	// Token: 0x040006FB RID: 1787
	public static float checkPointCoinChance = 0.4f;

	// Token: 0x040006FC RID: 1788
	public static int checkPointCoinAmount = 1;

	// Token: 0x040006FD RID: 1789
	public static float bossMaxAddedSpeed = 0.25f;
}
