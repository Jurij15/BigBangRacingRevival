using System;
using Firebase.Analytics;

// Token: 0x0200014B RID: 331
public static class FrbMetrics
{
	// Token: 0x06000B14 RID: 2836 RVA: 0x0006F690 File Offset: 0x0006DA90
	public static void SetUserId()
	{
		string userId = PlayerPrefsX.GetUserId();
		Debug.Log("E_Test SetUserId ID: " + userId, null);
		FirebaseAnalytics.SetUserId(userId);
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x0006F6BC File Offset: 0x0006DABC
	public static void SetInitialUserProperties()
	{
		Debug.Log("E_Test SetInitialUserProperties ", null);
		FrbMetrics.TrackServerCreationDate();
		FrbMetrics.TrackMusicSetting();
		FrbMetrics.TrackSoundSetting();
		FrbMetrics.TrackNotificationSetting();
		FrbMetrics.TrackLowEndSetting();
		FrbMetrics.TrackOffroadCc();
		FrbMetrics.TrackMotorcycleCc();
		FrbMetrics.TrackTeamsUnlocked();
		FrbMetrics.TrackTeamId();
		FrbMetrics.TrackCoinDoubler();
		FrbMetrics.TrackMotorcycleUnlocked();
		FrbMetrics.TrackCoinAmount();
		FrbMetrics.TrackGemAmount();
		FrbMetrics.TrackPublishedLevelCount();
		FrbMetrics.TrackAdventuresCompleted();
		FrbMetrics.TrackRacesCompleted();
		FrbMetrics.TrackOffroaderTrophies();
		FrbMetrics.TrackMotorcycleTrophies();
		FrbMetrics.TrackTotalLikes();
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x0006F734 File Offset: 0x0006DB34
	public static void TrackServerCreationDate()
	{
		Debug.Log("E_Test TrackServerCreationDate ", null);
		string text = PlayerPrefsX.GetFirstSeenDateTime().ToString();
		FirebaseAnalytics.SetUserProperty("server_creation_date", text);
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x0006F76C File Offset: 0x0006DB6C
	public static void TrackMusicSetting()
	{
		string text = (!PlayerPrefsX.GetMuteMusic()).ToString();
		Debug.Log("E_Test TrackMusicSetting: " + text, null);
		FirebaseAnalytics.SetUserProperty("music_on", text);
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x0006F7AC File Offset: 0x0006DBAC
	public static void TrackSoundSetting()
	{
		string text = (!PlayerPrefsX.GetMuteSoundFX()).ToString();
		Debug.Log("E_Test TrackSoundSetting: " + text, null);
		FirebaseAnalytics.SetUserProperty("sound_on", text);
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x0006F7EC File Offset: 0x0006DBEC
	public static void TrackNotificationSetting()
	{
		string text = PlayerPrefsX.GetAcceptNotifications().ToString();
		Debug.Log("E_Test TrackNotificationSetting: " + text, null);
		FirebaseAnalytics.SetUserProperty("notifications_on", text);
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x0006F82C File Offset: 0x0006DC2C
	public static void TrackLowEndSetting()
	{
		string text = PlayerPrefsX.GetPerfMode().ToString();
		Debug.Log("E_Test TrackLowEndSetting: " + text, null);
		FirebaseAnalytics.SetUserProperty("low_end_mode_on", text);
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x0006F86C File Offset: 0x0006DC6C
	public static void TrackCoinAmount()
	{
		double num = (double)PsMetagameManager.m_playerStats.coins;
		double num2 = (double)PsMetagameManager.m_playerStats.copper;
		num += num2 / 100.0;
		Debug.Log("E_Test TrackCoinAmount: " + num, null);
		FirebaseAnalytics.SetUserProperty("coin_amount", num.ToString());
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x0006F8CC File Offset: 0x0006DCCC
	public static void TrackGemAmount()
	{
		double num = (double)PsMetagameManager.m_playerStats.diamonds;
		double num2 = (double)PsMetagameManager.m_playerStats.shards;
		num += num2 / 100.0;
		Debug.Log("E_Test TrackGemAmount: " + num, null);
		FirebaseAnalytics.SetUserProperty("gem_amount", num.ToString());
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x0006F92C File Offset: 0x0006DD2C
	public static void TrackOffroadCc()
	{
		int num = (int)PsUpgradeManager.GetCurrentPerformance(typeof(OffroadCar));
		Debug.Log("E_Test TrackOffroadCc: " + num, null);
		FirebaseAnalytics.SetUserProperty("offroad_cc", num.ToString());
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x0006F978 File Offset: 0x0006DD78
	public static void TrackMotorcycleCc()
	{
		int num = (int)PsUpgradeManager.GetCurrentPerformance(typeof(Motorcycle));
		Debug.Log("E_Test TrackMotorcycleCc: " + num, null);
		FirebaseAnalytics.SetUserProperty("motorcycle_cc", num.ToString());
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x0006F9C4 File Offset: 0x0006DDC4
	public static void TrackPublishedLevelCount()
	{
		int levelsMade = PsMetagameManager.m_playerStats.levelsMade;
		Debug.Log("E_Test TrackPublishedLevelCount: " + levelsMade, null);
		FirebaseAnalytics.SetUserProperty("published_level_count", levelsMade.ToString());
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x0006FA0C File Offset: 0x0006DE0C
	public static void TrackAdventuresCompleted()
	{
		int adventureLevels = PsMetagameManager.m_playerStats.adventureLevels;
		Debug.Log("E_Test TrackAdventuresCompleted: " + adventureLevels, null);
		FirebaseAnalytics.SetUserProperty("adventures_completed", adventureLevels.ToString());
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x0006FA54 File Offset: 0x0006DE54
	public static void TrackRacesCompleted()
	{
		int racesCompleted = PsMetagameManager.m_playerStats.racesCompleted;
		Debug.Log("E_Test TrackRacesCompleted: " + racesCompleted, null);
		FirebaseAnalytics.SetUserProperty("races_completed", racesCompleted.ToString());
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x0006FA9C File Offset: 0x0006DE9C
	public static void TrackCoinDoubler()
	{
		bool coinDoubler = PsMetagameManager.m_playerStats.coinDoubler;
		Debug.Log("E_Test TrackCoinDoubler: " + coinDoubler, null);
		FirebaseAnalytics.SetUserProperty("coin_doubler", coinDoubler.ToString());
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x0006FAE4 File Offset: 0x0006DEE4
	public static void TrackMotorcycleUnlocked()
	{
		bool dirtBikeBundle = PsMetagameManager.m_playerStats.dirtBikeBundle;
		Debug.Log("E_Test TrackMotorcycleUnlocked: " + dirtBikeBundle, null);
		FirebaseAnalytics.SetUserProperty("motorcycle_unlocked", dirtBikeBundle.ToString());
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0006FB2C File Offset: 0x0006DF2C
	public static void TrackTeamsUnlocked()
	{
		bool teamUnlocked = PlayerPrefsX.GetTeamUnlocked();
		Debug.Log("E_Test TrackTeamsUnlocked: " + teamUnlocked, null);
		FirebaseAnalytics.SetUserProperty("teams_unlocked", teamUnlocked.ToString());
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x0006FB6C File Offset: 0x0006DF6C
	public static void TrackTeamId()
	{
		Debug.Log("E_Test TrackTeamId: " + PlayerPrefsX.GetTeamId(), null);
		if (PlayerPrefsX.GetTeamId() != null)
		{
			FirebaseAnalytics.SetUserProperty("teamId", PlayerPrefsX.GetTeamId());
		}
		else
		{
			FirebaseAnalytics.SetUserProperty("team_id", "noTeam");
		}
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x0006FBBC File Offset: 0x0006DFBC
	public static void TrackOffroaderTrophies()
	{
		int carTrophies = PsMetagameManager.m_playerStats.carTrophies;
		Debug.Log("E_Test TrackOffroaderTrophies: " + carTrophies, null);
		FirebaseAnalytics.SetUserProperty("offroader_trophies", carTrophies.ToString());
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x0006FC04 File Offset: 0x0006E004
	public static void TrackMotorcycleTrophies()
	{
		int mcTrophies = PsMetagameManager.m_playerStats.mcTrophies;
		Debug.Log("E_Test TrackMotorcycleTrophies: " + mcTrophies, null);
		FirebaseAnalytics.SetUserProperty("motorcycle_trophies", mcTrophies.ToString());
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x0006FC49 File Offset: 0x0006E049
	public static void TrackCreatorRank(int _creatorRank)
	{
		Debug.Log("E_Test TrackCreatorRank", null);
		FirebaseAnalytics.SetUserProperty("creator_rank", _creatorRank.ToString());
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x0006FC70 File Offset: 0x0006E070
	public static void TrackTotalLikes()
	{
		Debug.Log("E_Test TrackTotalLikes", null);
		int likesEarned = PsMetagameManager.m_playerStats.likesEarned;
		int megaLikesEarned = PsMetagameManager.m_playerStats.megaLikesEarned;
		FirebaseAnalytics.SetUserProperty("total_likes", (likesEarned + 10 * megaLikesEarned).ToString());
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x0006FCBC File Offset: 0x0006E0BC
	public static void TrackFirstTutorialStarted(bool _started)
	{
		Debug.Log("E_Test TrackFirstTutorialStarted: " + _started, null);
		FirebaseAnalytics.SetUserProperty("first_tutorial_started", _started.ToString());
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x0006FCEB File Offset: 0x0006E0EB
	public static void TrackLastTutorialFinished(bool _finished)
	{
		Debug.Log("E_Test TrackLastTutorialFinished: " + _finished, null);
		FirebaseAnalytics.SetUserProperty("last_tutorial_finished", _finished.ToString());
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x0006FD1A File Offset: 0x0006E11A
	public static void TrackNewUser(bool _isNew)
	{
		Debug.Log("E_Test TrackNewUser: " + _isNew, null);
		FirebaseAnalytics.SetUserProperty("new_user", _isNew.ToString());
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x0006FD4C File Offset: 0x0006E14C
	public static void ChestOpened(string _context)
	{
		int gems = PsGachaManager.m_lastGachaRewards.m_gems;
		int coins = PsGachaManager.m_lastGachaRewards.m_coins;
		if (gems > 0)
		{
			FrbMetrics.ReceiveVirtualCurrency("gems", (double)gems, "chest_" + _context);
		}
		if (coins > 0)
		{
			FrbMetrics.ReceiveVirtualCurrency("coins", (double)coins, "chest_" + _context);
		}
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x0006FDAC File Offset: 0x0006E1AC
	public static void RealMoneyPurchaseHelper(string _identifier, int _amount)
	{
		if (_identifier.Contains("gems"))
		{
			FrbMetrics.ReceiveVirtualCurrency("gems", (double)_amount, "iap_" + _identifier);
		}
		if (_identifier == "dirt_bike_bundle")
		{
			FrbMetrics.ReceiveVirtualCurrency("gems", 1000.0, "iap_" + _identifier);
			FrbMetrics.ReceiveVirtualCurrency("coins", 1000.0, "iap_" + _identifier);
		}
		if (_identifier == "special_offer_2")
		{
			FrbMetrics.ReceiveVirtualCurrency("gems", 500.0, "iap_" + _identifier);
			FrbMetrics.ReceiveVirtualCurrency("coins", 10000.0, "iap_" + _identifier);
		}
		if (_identifier == "special_offer_5")
		{
			FrbMetrics.ReceiveVirtualCurrency("gems", 1200.0, "iap_" + _identifier);
			FrbMetrics.ReceiveVirtualCurrency("coins", 10000.0, "iap_" + _identifier);
		}
		if (_identifier == "special_offer_10")
		{
			FrbMetrics.ReceiveVirtualCurrency("gems", 2500.0, "iap_" + _identifier);
			FrbMetrics.ReceiveVirtualCurrency("coins", 100000.0, "iap_" + _identifier);
		}
		if (_identifier == "special_offer_20")
		{
			FrbMetrics.ReceiveVirtualCurrency("gems", 7000.0, "iap_" + _identifier);
			FrbMetrics.ReceiveVirtualCurrency("coins", 100000.0, "iap_" + _identifier);
		}
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x0006FF5C File Offset: 0x0006E35C
	public static void ReceiveVirtualCurrency(string _currencyName, double _value, string _currencySource)
	{
		Debug.Log(string.Concat(new object[] { "E_Test ReceiveVirtualCurrency _currencyName: ", _currencyName, " _currencySource: ", _currencySource, " _value: ", _value }), null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterVirtualCurrencyName, _currencyName),
			new Parameter(FirebaseAnalytics.ParameterValue, _value),
			new Parameter("currencySource", _currencySource)
		};
		FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventEarnVirtualCurrency, array);
		if (_currencyName == "gems")
		{
			FrbMetrics.TrackGemAmount();
		}
		else if (_currencyName == "coins")
		{
			FrbMetrics.TrackCoinAmount();
		}
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00070010 File Offset: 0x0006E410
	public static void SpendVirtualCurrency(string _itemName, string _currencyName, double _value)
	{
		Debug.Log(string.Concat(new object[] { "E_Test SpendVirtualCurrency _itemName: ", _itemName, " _currencyName: ", _currencyName, " _value: ", _value }), null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterItemName, _itemName),
			new Parameter(FirebaseAnalytics.ParameterVirtualCurrencyName, _currencyName),
			new Parameter(FirebaseAnalytics.ParameterValue, _value)
		};
		FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSpendVirtualCurrency, array);
		if (_currencyName == "gems")
		{
			FrbMetrics.TrackGemAmount();
		}
		else if (_currencyName == "coins")
		{
			FrbMetrics.TrackCoinAmount();
		}
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x000700C4 File Offset: 0x0006E4C4
	public static void SetCurrentScreen(string _screenName = "noName")
	{
		Debug.Log("E_Test SetCurrentScreen _screenName: " + _screenName, null);
		Debug.Log("E_Test m_main_menu_visited: " + FrbMetrics.m_main_menu_visited, null);
		if (_screenName == "main_menu" && !FrbMetrics.m_main_menu_visited)
		{
			FrbMetrics.m_main_menu_visited = true;
			FrbMetrics.SetInitialUserProperties();
		}
		if (_screenName != "profile_own" && _screenName != "profile_other")
		{
			FrbMetrics.m_previousScreen = _screenName;
			Debug.Log("previous screen saved", null);
		}
		FirebaseAnalytics.SetCurrentScreen(_screenName, "test");
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x0007015D File Offset: 0x0006E55D
	public static void SetPreviousScreen()
	{
		Debug.Log("E_Test SetPreviousScreen _screenId: " + FrbMetrics.m_previousScreen, null);
		FrbMetrics.SetCurrentScreen(FrbMetrics.m_previousScreen);
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x0007017E File Offset: 0x0006E57E
	public static void TutorialStarted()
	{
		Debug.Log("E_Test TutorialStarted", null);
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x0007018B File Offset: 0x0006E58B
	public static void TutorialComplete()
	{
		Debug.Log("E_Test TutorialComplete", null);
		FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventTutorialComplete);
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x000701A4 File Offset: 0x0006E5A4
	public static void LevelEntered()
	{
		FrbMetrics.m_temp_coins = (double)PsMetagameManager.m_playerStats.coins;
		FrbMetrics.m_temp_copper = (double)PsMetagameManager.m_playerStats.copper;
		FrbMetrics.m_temp_gems = (double)PsMetagameManager.m_playerStats.diamonds;
		FrbMetrics.m_temp_shards = (double)PsMetagameManager.m_playerStats.shards;
		Debug.Log(string.Concat(new object[]
		{
			"E_Test F_LevelEntered ",
			FrbMetrics.m_temp_coins,
			" ",
			FrbMetrics.m_temp_copper,
			" ",
			FrbMetrics.m_temp_gems,
			" ",
			FrbMetrics.m_temp_shards
		}), null);
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00070258 File Offset: 0x0006E658
	public static void LevelExited(string _context)
	{
		Debug.Log("E_Test F_LevelExited ", null);
		double num = (double)PsMetagameManager.m_playerStats.coins - FrbMetrics.m_temp_coins;
		double num2 = (double)PsMetagameManager.m_playerStats.copper - FrbMetrics.m_temp_copper;
		double num3 = (double)PsMetagameManager.m_playerStats.diamonds - FrbMetrics.m_temp_gems;
		double num4 = (double)PsMetagameManager.m_playerStats.shards - FrbMetrics.m_temp_shards;
		if (num > 0.0 || num2 > 0.0)
		{
			FrbMetrics.ReceiveVirtualCurrency("coins", num + num2 / 100.0, "level_" + _context);
		}
		if (num3 > 0.0 || num4 > 0.0)
		{
			FrbMetrics.ReceiveVirtualCurrency("gems", num3 + num4 / 100.0, "level_" + _context);
		}
		FrbMetrics.m_temp_coins = 0.0;
		FrbMetrics.m_temp_copper = 0.0;
		FrbMetrics.m_temp_gems = 0.0;
		FrbMetrics.m_temp_shards = 0.0;
	}

	// Token: 0x04000A22 RID: 2594
	private static bool m_main_menu_visited;

	// Token: 0x04000A23 RID: 2595
	private static double m_temp_coins;

	// Token: 0x04000A24 RID: 2596
	private static double m_temp_copper;

	// Token: 0x04000A25 RID: 2597
	private static double m_temp_gems;

	// Token: 0x04000A26 RID: 2598
	private static double m_temp_shards;

	// Token: 0x04000A27 RID: 2599
	private static string m_previousScreen = "noName";
}
