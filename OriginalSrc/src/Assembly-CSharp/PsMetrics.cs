using System;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Firebase.Analytics;
using Server;
using UnityEngine;

// Token: 0x0200014C RID: 332
public static class PsMetrics
{
	// Token: 0x06000B38 RID: 2872 RVA: 0x0007037C File Offset: 0x0006E77C
	public static void Initialize()
	{
		Metrics.Initialize("m4ck5yjbw7pc");
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x00070388 File Offset: 0x0006E788
	public static void GameStart()
	{
		Debug.Log("GameStartFunnel: E_Test GameStart", null);
		if (string.IsNullOrEmpty(PlayerPrefsX.GetUserId()))
		{
			FrbMetrics.TrackNewUser(true);
			FrbMetrics.TrackFirstTutorialStarted(false);
			FrbMetrics.TrackLastTutorialFinished(false);
		}
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("GameStart", array);
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x000703E4 File Offset: 0x0006E7E4
	public static void SplashScreenStarted()
	{
		Debug.Log("GameStartFunnel: E_Test SplashScreenStarted", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("SplashScreenStarted", array);
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00070420 File Offset: 0x0006E820
	public static void SplashScreenFinished()
	{
		Debug.Log("GameStartFunnel: E_Test SplashScreenFinished", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("SplashScreenFinished", array);
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x0007045C File Offset: 0x0006E85C
	public static void PreloadStart()
	{
		Debug.Log("GameStartFunnel: E_Test PreloadStart", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PreloadStart", array);
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00070498 File Offset: 0x0006E898
	public static void PreloadFinished(bool _didDownload = true)
	{
		Debug.Log("GameStartFunnel: E_Test PreloadFinished", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PreloadFinished", array);
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x000704D4 File Offset: 0x0006E8D4
	public static void CreatingNewUser()
	{
		Debug.Log("GameStartFunnel: E_Test CreatingNewUser", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("CreatingNewUser", array);
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00070510 File Offset: 0x0006E910
	public static void FirstLoginStarted()
	{
		Debug.Log("GameStartFunnel: E_Test FirstLoginStarted", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("FirstLoginStarted", array);
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x0007054C File Offset: 0x0006E94C
	public static void FirstLoginFinished()
	{
		Debug.Log("GameStartFunnel: E_Test FirstLoginFinished", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("FirstLoginFinished", array);
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x00070588 File Offset: 0x0006E988
	public static void FirstLevelOffered()
	{
		Debug.Log("GameStartFunnel: E_Test FirstLevelOffered", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("FirstLevelOffered", array);
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x000705C4 File Offset: 0x0006E9C4
	public static void FirstLevelLoaded()
	{
		Debug.Log("GameStartFunnel: E_Test FirstLevelLoaded", null);
		FrbMetrics.TrackFirstTutorialStarted(true);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("FirstLevelLoaded", array);
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x00070604 File Offset: 0x0006EA04
	public static void FirstLevelRunStarted()
	{
		Debug.Log("GameStartFunnel: E_Test FirstLevelRunStarted", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("FirstLevelRunStarted", array);
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x00070640 File Offset: 0x0006EA40
	public static void FirstLevelGoalReached()
	{
		Debug.Log("GameStartFunnel: E_Test FirstLevelGoalReached", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("FirstLevelGoalReached", array);
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x0007067C File Offset: 0x0006EA7C
	public static void FirstLevelRated()
	{
		Debug.Log("GameStartFunnel: E_Test FirstLevelRated", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("FirstLevelRated", array);
		FrbMetrics.TrackLastTutorialFinished(true);
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x000706BC File Offset: 0x0006EABC
	public static void ProgressionChoiceOffered()
	{
		Debug.Log("GameStartFunnel: E_Test ProgressionChoiceOffered", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("ProgressionChoiceOffered", array);
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x000706F8 File Offset: 0x0006EAF8
	public static void ConnectedToNewProgression()
	{
		Debug.Log("GameStartFunnel: E_Test ConnectedToNewProgression", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("ConnectedToNewProgression", array);
		FrbMetrics.SetInitialUserProperties();
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x00070738 File Offset: 0x0006EB38
	public static void ConnectedToOldProgression(string _serviceName)
	{
		Debug.Log("GameStartFunnel: E_Test ConnectedToOldProgression", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("serviceName", _serviceName),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("ConnectedToOldProgression", array);
		FrbMetrics.SetInitialUserProperties();
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00070784 File Offset: 0x0006EB84
	public static void MainMenuReached()
	{
		Debug.Log("GameStartFunnel: E_Test MainMenuReached", null);
		FrbMetrics.TrackNewUser(false);
		FrbMetrics.TrackFirstTutorialStarted(true);
		FrbMetrics.TrackLastTutorialFinished(true);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("MainMenuReached", array);
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x000707D0 File Offset: 0x0006EBD0
	public static void LevelLoadStart()
	{
		Debug.Log("E_Test LevelLoadStart", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelLoadStart", array);
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x0007080C File Offset: 0x0006EC0C
	public static void LevelLoadFinished()
	{
		Debug.Log("E_Test LevelLoadFinished", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelLoadFinished", array);
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00070848 File Offset: 0x0006EC48
	public static void ReportPerformance(float _perfValue)
	{
		Debug.Log("E_Test ReportPerformance", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("performanceValue", (double)_perfValue),
			new Parameter("cpuSpeedInMHz", (long)SystemInfo.processorFrequency),
			new Parameter("cpuName", SystemInfo.processorType),
			new Parameter("gpuName", SystemInfo.graphicsDeviceName),
			new Parameter("gpuVendor", SystemInfo.graphicsDeviceVendor),
			new Parameter("gpuMemory", (long)SystemInfo.graphicsMemorySize),
			new Parameter(FirebaseAnalytics.ParameterValue, (double)_perfValue)
		};
		FirebaseAnalytics.LogEvent("DevicePerformance", array);
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x000708EC File Offset: 0x0006ECEC
	public static void Tutorial(bool _completed = true)
	{
		Debug.Log("E_Test Tutorial", null);
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("Completed", _completed.ToString());
		Metrics.SendEvent("Tutorial", dictionary, true);
		FacebookManager.SendTutorialComplete(_completed);
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00070934 File Offset: 0x0006ED34
	public static void Progression(int _currentNode, string _pathIdentifier)
	{
		Debug.Log(string.Concat(new object[] { "E_Test Progression _pathIdentifier: ", _pathIdentifier, " _currentnode: ", _currentNode }), null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("progressionNode", (long)_currentNode),
			new Parameter("progressionPath", _pathIdentifier)
		};
		FirebaseAnalytics.LogEvent("PathProgression", array);
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x000709A0 File Offset: 0x0006EDA0
	public static void TrackRealMoneyPurchase(string _identifier, int _amount, string _transactionId = "")
	{
		Debug.Log("E_Test TrackRealMoneyPurchase", null);
		Dictionary<string, double> dictionary = new Dictionary<string, double>();
		dictionary.Add("80gemss", 0.99);
		dictionary.Add("180gemss", 1.99);
		dictionary.Add("500gemss", 4.99);
		dictionary.Add("1200gemss", 9.99);
		dictionary.Add("2500gemss", 19.99);
		dictionary.Add("4500gemss", 29.99);
		dictionary.Add("6500gemss", 49.99);
		dictionary.Add("7000gemss", 49.99);
		dictionary.Add("14000gemss", 99.99);
		dictionary.Add("coin_booster", 2.99);
		dictionary.Add("dirt_bike_bundle", 4.99);
		dictionary.Add("trail_bubble", 1.99);
		dictionary.Add("trail_feather", 1.99);
		dictionary.Add("trail_fire", 1.99);
		dictionary.Add("trail_cash", 1.99);
		dictionary.Add("trail_rainbow", 1.99);
		dictionary.Add("trail_scifi", 1.99);
		dictionary.Add("trail_death", 1.99);
		dictionary.Add("trail_bat", 1.99);
		dictionary.Add("league_bundle_1", 1.99);
		dictionary.Add("league_bundle_2", 1.99);
		dictionary.Add("league_bundle_3", 1.99);
		dictionary.Add("league_bundle_4", 4.99);
		dictionary.Add("league_bundle_5", 4.99);
		dictionary.Add("league_bundle_6", 4.99);
		dictionary.Add("league_bundle_7", 4.99);
		dictionary.Add("special_offer_2", 1.99);
		dictionary.Add("special_offer_5", 4.99);
		dictionary.Add("special_offer_10", 9.99);
		dictionary.Add("special_offer_20", 19.99);
		if (_identifier == "coin_booster")
		{
			FrbMetrics.TrackCoinDoubler();
		}
		if (_identifier == "dirt_bike_bundle")
		{
			FrbMetrics.TrackMotorcycleUnlocked();
		}
		string empty = string.Empty;
		if (_identifier == "coin_booster" || _identifier == "dirt_bike_bundle" || _identifier.Contains("trail"))
		{
			_amount = 1;
		}
		double num;
		if (dictionary.ContainsKey(_identifier))
		{
			num = dictionary[_identifier];
		}
		else if (_identifier.Contains("common"))
		{
			num = 0.99;
			_amount = 1;
		}
		else if (_identifier.Contains("rare"))
		{
			num = 2.99;
			_amount = 1;
		}
		else if (_identifier.Contains("epic"))
		{
			num = 4.99;
			_amount = 1;
		}
		else
		{
			num = 0.01;
		}
		if (_identifier.Contains("gemss"))
		{
			PsMetrics.m_trackDiamondsAfterPurchase = true;
		}
		Parameter[] array = new Parameter[]
		{
			new Parameter("identifier", _identifier),
			new Parameter("purchasedAmount", (long)_amount),
			new Parameter("transactionId", _transactionId),
			new Parameter("nominalPrice", num),
			new Parameter(FirebaseAnalytics.ParameterValue, num)
		};
		FirebaseAnalytics.LogEvent("RealMoneyPurchase", array);
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x00070D7C File Offset: 0x0006F17C
	public static void StarterPackOffered(string _context)
	{
		Debug.Log("E_Test StarterPackOffered", null);
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("context", _context);
		Metrics.SendEvent("StarterPackOffered", dictionary, true);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("StarterPackOffered", array);
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x00070DD4 File Offset: 0x0006F1D4
	public static void StarterPackPurchased(string _context)
	{
		Debug.Log("E_Test StarterPackPurchased", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("StarterPackPurchased", array);
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x00070E10 File Offset: 0x0006F210
	public static void NewSpecialOfferShown(long _startTime, string _chest, int _coins, int _diamonds, string _price)
	{
		Debug.Log("E_Test NewSpecialOfferShown ", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("specialOfferStartTime", _startTime),
			new Parameter("specialOfferChest", _chest),
			new Parameter("specialOfferCoins", (long)_coins),
			new Parameter("specialOfferDiamonds", (long)_diamonds),
			new Parameter("specialOfferPrice", _price),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("NewSpecialOfferShown", array);
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00070E94 File Offset: 0x0006F294
	public static void SpecialOfferClaimed(long _startTime)
	{
		Debug.Log("E_Test SpecialOfferClaimed", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("specialOfferStartTime", _startTime),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("SpecialOfferClaimed", array);
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00070EDC File Offset: 0x0006F2DC
	public static void GarageEntered(Type _vehicleType)
	{
		Debug.Log("E_Test GarageEntered", null);
		PsMetrics.m_tempCC = (int)PsUpgradeManager.GetCurrentPerformance(_vehicleType);
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetVehicleCustomisationData(_vehicleType).GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		if (installedItemByCategory != null)
		{
			PsMetrics.m_tempHat = installedItemByCategory.m_identifier;
		}
		else
		{
			PsMetrics.m_tempHat = "MotocrossHelmet";
		}
		PsCustomisationItem installedItemByCategory2 = PsCustomisationManager.GetVehicleCustomisationData(_vehicleType).GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.TRAIL);
		if (installedItemByCategory2 != null)
		{
			PsMetrics.m_temp_trail = installedItemByCategory2.m_identifier;
		}
		else
		{
			PsMetrics.m_temp_trail = "NoTrail";
		}
		Debug.Log(string.Concat(new object[]
		{
			"E_Test ",
			PsMetrics.m_tempCC,
			" ",
			PsMetrics.m_tempHat,
			" ",
			PsMetrics.m_temp_trail
		}), null);
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x00070FA0 File Offset: 0x0006F3A0
	public static void GarageExited(Type _vehicleType)
	{
		Debug.Log("E_Test GarageExited()", null);
		int num = (int)PsUpgradeManager.GetCurrentPerformance(_vehicleType);
		if (PsMetrics.m_tempCC != num)
		{
			PsMetrics.VehiclePerformanceChanged(_vehicleType);
		}
		string text = string.Empty;
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetVehicleCustomisationData(_vehicleType).GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		if (installedItemByCategory != null)
		{
			text = installedItemByCategory.m_identifier;
		}
		else
		{
			text = "MotocrossHelmet";
		}
		if (text != PsMetrics.m_tempHat)
		{
			PsMetrics.HatChanged(_vehicleType, text);
		}
		string text2 = string.Empty;
		PsCustomisationItem installedItemByCategory2 = PsCustomisationManager.GetVehicleCustomisationData(_vehicleType).GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.TRAIL);
		if (installedItemByCategory2 != null)
		{
			text2 = installedItemByCategory2.m_identifier;
		}
		else
		{
			text2 = "NoTrail";
		}
		if (text2 != PsMetrics.m_temp_trail)
		{
			PsMetrics.TrailChanged(_vehicleType, text2);
		}
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x00071058 File Offset: 0x0006F458
	public static void VehicleUpgrade(string _vehicle, string _upgradeName, int _coinValue, float _ccValue)
	{
		Debug.Log("E_Test VehicleUpgrade() " + _ccValue, null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("vehicle", _vehicle),
			new Parameter("upgradeName", _upgradeName),
			new Parameter("upgradeValue", (long)_coinValue),
			new Parameter("ccValue", (double)_ccValue),
			new Parameter(FirebaseAnalytics.ParameterValue, (long)_coinValue)
		};
		FirebaseAnalytics.LogEvent("VehicleUpgrade", array);
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x000710D8 File Offset: 0x0006F4D8
	private static void VehiclePerformanceChanged(Type _vehicleType)
	{
		int num = (int)PsUpgradeManager.GetCurrentPerformance(_vehicleType);
		int num2 = (int)PsUpgradeManager.GetCurrentEfficiency(_vehicleType, PsUpgradeManager.UpgradeType.SPEED) + 16;
		int num3 = (int)PsUpgradeManager.GetCurrentEfficiency(_vehicleType, PsUpgradeManager.UpgradeType.GRIP) + 16;
		int num4 = (int)PsUpgradeManager.GetCurrentEfficiency(_vehicleType, PsUpgradeManager.UpgradeType.HANDLING) + 16;
		int num5 = PsUpgradeManager.GetPowerUpItemsCurrentPerformance(_vehicleType) + 16;
		Parameter[] array = new Parameter[]
		{
			new Parameter("vehicle", _vehicleType.ToString()),
			new Parameter("totalPerformance", (long)num),
			new Parameter("speedPerformance", (long)num2),
			new Parameter("gripPerformance", (long)num3),
			new Parameter("handlingPerformance", (long)num4),
			new Parameter("powerupPerformance", (long)num5),
			new Parameter(FirebaseAnalytics.ParameterValue, (long)num)
		};
		FirebaseAnalytics.LogEvent("VehiclePerformance", array);
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x000711A0 File Offset: 0x0006F5A0
	private static void HatChanged(Type _vehicleType, string _hat)
	{
		Parameter[] array = new Parameter[]
		{
			new Parameter("vehicle", _vehicleType.ToString()),
			new Parameter("currentHat", _hat),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("HatChanged", array);
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x000711F0 File Offset: 0x0006F5F0
	private static void TrailChanged(Type _vehicleType, string _trail)
	{
		Parameter[] array = new Parameter[]
		{
			new Parameter("vehicle", _vehicleType.ToString()),
			new Parameter("currentTrail", _trail),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("TrailChanged", array);
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00071240 File Offset: 0x0006F640
	public static void ChestOpened(string _context)
	{
		Debug.Log("E_Test ChestOpened()", null);
		int count = PsGachaManager.m_lastGachaRewards.m_editorItems.Count;
		int count2 = PsGachaManager.m_lastGachaRewards.m_upgradeItems.Count;
		string text = PsGachaManager.m_lastOpenedGacha.ToString();
		foreach (KeyValuePair<string, int> keyValuePair in PsGachaManager.m_lastGachaRewards.m_editorItems)
		{
			PsMetrics.EditorItemsFromChest(keyValuePair.Key, keyValuePair.Value, text);
		}
		foreach (KeyValuePair<string, int> keyValuePair2 in PsGachaManager.m_lastGachaRewards.m_upgradeItems)
		{
			PsMetrics.UpgradesFromChest(keyValuePair2.Key, keyValuePair2.Value, text);
		}
		string text2 = PsState.GetCurrentVehicleType(false).ToString();
		string text3;
		if (PsGachaManager.m_lastGachaRewards.m_hat != string.Empty)
		{
			text3 = PsGachaManager.m_lastGachaRewards.m_hat;
		}
		else
		{
			text3 = "NoHat";
		}
		Parameter[] array = new Parameter[]
		{
			new Parameter("context", _context),
			new Parameter("chestType", text),
			new Parameter("chestCoins", (long)PsGachaManager.m_lastGachaRewards.m_coins),
			new Parameter("chestGems", (long)PsGachaManager.m_lastGachaRewards.m_gems),
			new Parameter("chestNitros", (long)PsGachaManager.m_lastGachaRewards.m_nitros),
			new Parameter("chestHat", text3),
			new Parameter("chestEditorItems", (long)count),
			new Parameter("chestUpgradeItems", (long)count2),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("ChestOpened", array);
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x0007143C File Offset: 0x0006F83C
	private static void EditorItemsFromChest(string _identifier, int _amount, string _chestType)
	{
		Debug.Log(string.Concat(new object[] { "E_Test EditorItemsFromChest ", _chestType, " ", _identifier, " ", _amount }), null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("chestType", _chestType),
			new Parameter("identifier", _identifier),
			new Parameter("amount", (long)_amount),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("ChestEditorItems", array);
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x000714D0 File Offset: 0x0006F8D0
	private static void UpgradesFromChest(string _identifier, int _amount, string _chestType)
	{
		Debug.Log(string.Concat(new object[] { "E_Test UpgradesFromChest ", _chestType, " ", _identifier, " ", _amount }), null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("chestType", _chestType),
			new Parameter("identifier", _identifier),
			new Parameter("amount", (long)_amount),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("ChestUpgradeItems", array);
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x00071564 File Offset: 0x0006F964
	public static void LevelShared(string _context, string _sharedLevelId, string _sharedLevelName, string _sharedLevelCreatorId, string _sharedLevelCreatorName)
	{
		Parameter[] array = new Parameter[]
		{
			new Parameter("context", _context),
			new Parameter("sharedLevelId", _sharedLevelId),
			new Parameter("sharedLevelName", _sharedLevelName),
			new Parameter("sharedLevelCreatorId", _sharedLevelCreatorId),
			new Parameter("sharedLevelCreatorName", _sharedLevelCreatorName),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelShared", array);
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x000715DC File Offset: 0x0006F9DC
	public static void OpenedFromLink(string _sharedLevelId)
	{
		Parameter[] array = new Parameter[]
		{
			new Parameter("sharedLevelId", _sharedLevelId),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("OpenedFromLink", array);
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x00071618 File Offset: 0x0006FA18
	public static void GifShared(bool _reshare = false)
	{
		Debug.Log("E_Test GifShared()", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("minigameId", PsState.m_activeGameLoop.m_minigameMetaData.id),
			new Parameter("minigameName", PsState.m_activeGameLoop.m_minigameMetaData.name),
			new Parameter("creatorName", PsState.m_activeGameLoop.m_minigameMetaData.creatorName),
			new Parameter("hidden", PsState.m_activeGameLoop.m_minigameMetaData.hidden.ToString()),
			new Parameter("gameMode", PsState.m_activeGameLoop.m_gameMode.ToString()),
			new Parameter("playerUnit", PsState.m_activeGameLoop.m_minigameMetaData.playerUnit),
			new Parameter("levelNumber", (long)PsState.m_activeGameLoop.m_levelNumber),
			new Parameter("reshare", _reshare.ToString()),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("GifShared", array);
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00071738 File Offset: 0x0006FB38
	public static void SupportMessageWindowOpened()
	{
		Debug.Log("E_Test SupportMessageWindowOpened", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("SupportWindowOpened", array);
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00071774 File Offset: 0x0006FB74
	public static void FacebookPageOpened()
	{
		Debug.Log("E_Test FacebookPageOpened", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("loveUsPopupOpen", PsMetrics.m_loveUsPopupOpen.ToString()),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("FacebookPageOpened", array);
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x000717CC File Offset: 0x0006FBCC
	public static void InstagramPageOpened()
	{
		Debug.Log("E_Test InstagramPageOpened", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("loveUsPopupOpen", PsMetrics.m_loveUsPopupOpen.ToString()),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("InstagramPageOpened", array);
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00071824 File Offset: 0x0006FC24
	public static void ForumPageOpened()
	{
		Debug.Log("E_Test ForumPageOpened", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("loveUsPopupOpen", PsMetrics.m_loveUsPopupOpen.ToString()),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("ForumPageOpened", array);
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x0007187A File Offset: 0x0006FC7A
	public static void LoveUsPopupShown()
	{
		Debug.Log("E_Test LoveUsPopupShown", null);
		PsMetrics.m_loveUsPopupOpen = true;
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0007188D File Offset: 0x0006FC8D
	public static void LoveUsPopupClosed()
	{
		Debug.Log("E_Test LoveUsPopupClosed", null);
		PsMetrics.m_loveUsPopupOpen = false;
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x000718A0 File Offset: 0x0006FCA0
	public static void YoutubeAccountSet()
	{
		Debug.Log("E_Test YoutubeAccountSet", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("tubeName", PlayerPrefsX.GetYoutubeName()),
			new Parameter("tubeId", PlayerPrefsX.GetYoutubeId()),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("YoutubeAccountSet", array);
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x00071900 File Offset: 0x0006FD00
	public static void YoutubeAccountRemoved()
	{
		Debug.Log("E_Test YoutubeAccountRemoved", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("YoutubeAccountRemoved", array);
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x0007193C File Offset: 0x0006FD3C
	public static void YoutubeLinkOffered(string _context, string _tubeId, string _tubeName, string _linkingPlayerId, string _linkingPlayerName)
	{
		Parameter[] array = new Parameter[]
		{
			new Parameter("context", _context),
			new Parameter("tubeId", _tubeId),
			new Parameter("tubeName", _tubeName),
			new Parameter("linkingPlayerId", _linkingPlayerId),
			new Parameter("linkingPlayerName", _linkingPlayerName),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PlayerYoutubeLinkOffered", array);
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x000719B4 File Offset: 0x0006FDB4
	public static void YoutubePageOpened(string _context, string _tubeId, string _tubeName)
	{
		Parameter[] array = new Parameter[]
		{
			new Parameter("context", _context),
			new Parameter("tubeId", _tubeId),
			new Parameter("tubeName", _tubeName),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PlayerYoutubePageOpened", array);
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x00071A0C File Offset: 0x0006FE0C
	public static void YoutubePageOpened(string _context, string _tubeId, string _tubeName, string _linkingPlayerId, string _linkingPlayerName)
	{
		Parameter[] array = new Parameter[]
		{
			new Parameter("context", _context),
			new Parameter("tubeId", _tubeId),
			new Parameter("tubeName", _tubeName),
			new Parameter("linkingPlayerId", _linkingPlayerId),
			new Parameter("linkingPlayerName", _linkingPlayerName),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PlayerYoutubePageOpened", array);
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x00071A84 File Offset: 0x0006FE84
	public static void TeamCreated(string _teamId, string _teamName, string _joinType, string _requiredTrophies)
	{
		Debug.Log("E_Test TeamCreated", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("teamId", _teamId),
			new Parameter("teamName", _teamName),
			new Parameter("joinType", _joinType),
			new Parameter("requiredTrophies", _requiredTrophies),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("TeamCreated", array);
		PsMetrics.PlayerJoinedTeam(_teamId, _teamName);
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x00071AFC File Offset: 0x0006FEFC
	public static void PlayerJoinedTeam(string _teamId, string _teamName)
	{
		Debug.Log("E_Test PlayerJoinedTeam", null);
		FrbMetrics.TrackTeamId();
		Parameter[] array = new Parameter[]
		{
			new Parameter("teamId", _teamId),
			new Parameter("teamName", _teamName),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PlayerJoinedTeam", array);
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x00071B58 File Offset: 0x0006FF58
	public static void PlayerLeftTeam()
	{
		Debug.Log("E_Test PlayerLeftTeam", null);
		FrbMetrics.TrackTeamId();
		Parameter[] array = new Parameter[]
		{
			new Parameter("teamId", PlayerPrefsX.GetTeamId()),
			new Parameter("teamName", PlayerPrefsX.GetTeamName()),
			new Parameter("teamRole", PlayerPrefsX.GetTeamRole().ToString()),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PlayerLeftTeam", array);
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x00071BDC File Offset: 0x0006FFDC
	public static void PlayerKickedFromTeam(string _teamId, string _kickedPlayerId)
	{
		Debug.Log("E_Test PlayerKickedFromTeam", null);
		FrbMetrics.TrackTeamId();
		Parameter[] array = new Parameter[]
		{
			new Parameter("teamId", _teamId),
			new Parameter("kickedPlayerId", _kickedPlayerId),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PlayerKickedFromTeam", array);
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00071C36 File Offset: 0x00070036
	public static void SetAdNetworkName(string _adNetwork)
	{
		Debug.Log("E_Test SetAdNetworkName", null);
		PsMetrics.m_adNetwork = _adNetwork;
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00071C4C File Offset: 0x0007004C
	public static void AdOffered(string _context)
	{
		Debug.Log("E_Test AdOffered " + _context, null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("context", _context),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("AdOffered", array);
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x00071C9C File Offset: 0x0007009C
	public static void AdWatched(string _context, string _adResult)
	{
		Debug.Log("E_Test AdWatched context: " + _context + "adResult: " + _adResult, null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("context", _context),
			new Parameter("adResult", _adResult.ToLower()),
			new Parameter("adNetwork", PsMetrics.m_adNetwork),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("AdWatched", array);
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x00071D14 File Offset: 0x00070114
	public static void AdNotAvailable(string _context)
	{
		Debug.Log("E_Test AdNotAvailable " + _context, null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("context", _context),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("AdNotAvailable", array);
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x00071D64 File Offset: 0x00070164
	public static void LevelRated(PsMetagameManager.SaveRatingData _data)
	{
		Debug.Log("E_Test LevelRated", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("minigameId", PsState.m_activeGameLoop.m_minigameMetaData.id),
			new Parameter("minigameName", PsState.m_activeGameLoop.m_minigameMetaData.name),
			new Parameter("creatorName", PsState.m_activeGameLoop.m_minigameMetaData.creatorName),
			new Parameter("hidden", PsState.m_activeGameLoop.m_minigameMetaData.hidden.ToString()),
			new Parameter("gameMode", PsState.m_activeGameLoop.m_gameMode.ToString()),
			new Parameter("context", PsState.m_activeGameLoop.m_context.ToString()),
			new Parameter("rating", PsState.m_activeGameLoop.m_minigameMetaData.rating.ToString()),
			new Parameter("playerUnit", PsState.m_activeGameLoop.m_minigameMetaData.playerUnit),
			new Parameter("nodeNumber", (long)PsState.m_activeGameLoop.m_nodeId),
			new Parameter("levelNumber", (long)PsState.m_activeGameLoop.m_levelNumber),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelRated", array);
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00071ECC File Offset: 0x000702CC
	public static void TournamentSignup(string _tournamentId, int _roomNumber, float _tournamentCC, Type _vehicleType, float _playerCC)
	{
		Debug.Log("E_Test TournamentSignup", null);
		float num = _playerCC - _tournamentCC;
		Parameter[] array = new Parameter[]
		{
			new Parameter("tournamentId", _tournamentId),
			new Parameter("roomNumber", (long)_roomNumber),
			new Parameter("tournamentVehicle", _vehicleType.ToString()),
			new Parameter("tournamentCC", (double)_tournamentCC),
			new Parameter("playerCC", (double)_playerCC),
			new Parameter("ccDifference", (double)num),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("TournamentSignup", array);
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x00071F68 File Offset: 0x00070368
	public static void TournamentRoomEntered(string _tournamentId, int _roomNumber)
	{
		Debug.Log("E_Test TournamentRoomEntered", null);
		PsMetrics.m_tournamentPlayStartTime = (double)Time.time;
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x00071F80 File Offset: 0x00070380
	public static void TournamentRoomExited(string _tournamentId, int _roomNumber)
	{
		Debug.Log("E_Test TournamentRoomExited", null);
		double num = Math.Round((double)Time.time - PsMetrics.m_tournamentPlayStartTime, 2);
		Parameter[] array = new Parameter[]
		{
			new Parameter("tournamentId", _tournamentId),
			new Parameter("roomNumber", (long)_roomNumber),
			new Parameter("timeSpent", num),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("TournamentRoomExited", array);
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x00071FF8 File Offset: 0x000703F8
	public static void TournamentRunStarted(string _tournamentId, int _roomNumber)
	{
		Debug.Log("E_Test TournamentRunStarted", null);
		PsMetrics.m_tournamentRunStartTime = (double)Time.time;
		Parameter[] array = new Parameter[]
		{
			new Parameter("tournamentId", _tournamentId),
			new Parameter("roomNumber", (long)_roomNumber),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("TournamentRunStarted", array);
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x0007205C File Offset: 0x0007045C
	public static void TournamentGoalReached(string _tournamentId, int _roomNumber, int _runResultTime, int _roomTopScore, bool _usedNitro, bool _timeImproved, bool _positionImproved)
	{
		Debug.Log("E_Test TournamentGoalReached", null);
		double num = Math.Round((double)Time.time - PsMetrics.m_tournamentRunStartTime, 2);
		Parameter[] array = new Parameter[]
		{
			new Parameter("tournamentId", _tournamentId),
			new Parameter("roomNumber", (long)_roomNumber),
			new Parameter("runResultTime", (long)_runResultTime),
			new Parameter("roomTopScore", (long)_roomTopScore),
			new Parameter("usedNitro", _usedNitro.ToString()),
			new Parameter("timeImproved", _timeImproved.ToString()),
			new Parameter("positionImproved", _positionImproved.ToString()),
			new Parameter("timeSpent", num),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("TournamentGoalReached", array);
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x00072140 File Offset: 0x00070540
	public static void TournamentRunDeath(string _tournamentId, int _roomNumber, int _roomTopScore, bool _usedNitro, bool _didEject)
	{
		Debug.Log("E_Test TournamentRunDeath", null);
		double num = Math.Round((double)Time.time - PsMetrics.m_tournamentRunStartTime, 2);
		Parameter[] array = new Parameter[]
		{
			new Parameter("tournamentId", _tournamentId),
			new Parameter("roomNumber", (long)_roomNumber),
			new Parameter("roomTopScore", (long)_roomTopScore),
			new Parameter("usedNitro", _usedNitro.ToString()),
			new Parameter("didEject", _didEject.ToString()),
			new Parameter("timeSpent", num),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("TournamentRunDeath", array);
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x000721FC File Offset: 0x000705FC
	public static void TournamentNitrosFilled(string _tournamentId, int _roomNumber, int _nitroAmount, int _price, bool _paidWithAds)
	{
		Debug.Log("E_Test TournamentNitrosFilled", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("tournamentId", _tournamentId),
			new Parameter("roomNumber", (long)_roomNumber),
			new Parameter("nitroAmount", (long)_nitroAmount),
			new Parameter("price", (long)_price),
			new Parameter("paidWithAds", _paidWithAds.ToString()),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("TournamentNitrosFilled", array);
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x0007228A File Offset: 0x0007068A
	public static void TournamentGhostWatched()
	{
		Debug.Log("E_Test TournamentGhostWatched", null);
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00072298 File Offset: 0x00070698
	public static void LevelEntered(string _vehicle, string _gameMode, string _context)
	{
		Debug.Log("E_Test2 LevelEntered (with parameters) ", null);
		PsMetrics.m_levelStartTime = (double)Time.time;
		PsMetrics.m_goalReached = false;
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = "false";
		int num = 0;
		int num2 = 0;
		PsMetrics.m_levelRunCount = 0;
		PsMetrics.m_goalReachedCount = 0;
		if (PsState.m_activeGameLoop != null && PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			if (PsState.m_activeGameLoop.m_minigameMetaData.id != null)
			{
				text = PsState.m_activeGameLoop.m_minigameMetaData.id;
			}
			if (PsState.m_activeGameLoop.m_minigameMetaData.name != null)
			{
				text2 = PsState.m_activeGameLoop.m_minigameMetaData.name;
			}
			text3 = PsState.m_activeGameLoop.m_minigameMetaData.hidden.ToString();
			num = PsState.m_activeGameLoop.m_nodeId;
			num2 = PsState.m_activeGameLoop.m_levelNumber;
		}
		Parameter[] array = new Parameter[]
		{
			new Parameter("vehicle", _vehicle),
			new Parameter("gameMode", _gameMode),
			new Parameter("context", _context),
			new Parameter("minigameId", text),
			new Parameter("minigameName", text2),
			new Parameter("hidden", text3),
			new Parameter("nodeNumber", (long)num),
			new Parameter("levelNumber", (long)num2),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelEntered", array);
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x00072410 File Offset: 0x00070810
	public static void LevelExited(string _vehicle, string _gameMode, string _context)
	{
		Debug.Log("E_Test2 LevelExited (with parameters) " + PsMetagameManager.m_playerStats.adventureLevels, null);
		PsMetrics.m_endposition = 0;
		double num = Math.Round((double)Time.time - PsMetrics.m_levelStartTime, 2);
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = "false";
		int num2 = 0;
		int num3 = 0;
		if (PsState.m_activeGameLoop != null && PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			if (PsState.m_activeGameLoop.m_minigameMetaData.id != null)
			{
				text = PsState.m_activeGameLoop.m_minigameMetaData.id;
			}
			if (PsState.m_activeGameLoop.m_minigameMetaData.name != null)
			{
				text2 = PsState.m_activeGameLoop.m_minigameMetaData.name;
			}
			text3 = PsState.m_activeGameLoop.m_minigameMetaData.hidden.ToString();
			num2 = PsState.m_activeGameLoop.m_nodeId;
			num3 = PsState.m_activeGameLoop.m_levelNumber;
		}
		Parameter[] array = new Parameter[]
		{
			new Parameter("vehicle", _vehicle),
			new Parameter("gameMode", _gameMode),
			new Parameter("context", _context),
			new Parameter("timeSpent", num),
			new Parameter("minigameId", text),
			new Parameter("minigameName", text2),
			new Parameter("hidden", text3),
			new Parameter("nodeNumber", (long)num2),
			new Parameter("levelNumber", (long)num3),
			new Parameter("levelRunCount", (long)PsMetrics.m_levelRunCount),
			new Parameter("goalReachedCount", (long)PsMetrics.m_goalReachedCount),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelExited", array);
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x000725D0 File Offset: 0x000709D0
	public static void LevelRunStarted(string _vehicle, string _gameMode, string _context)
	{
		Debug.Log("E_Test LevelRunStarted", null);
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = "false";
		int num = 0;
		int num2 = 0;
		PsMetrics.m_levelRunCount++;
		if (PsState.m_activeGameLoop != null && PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			if (PsState.m_activeGameLoop.m_minigameMetaData.id != null)
			{
				text = PsState.m_activeGameLoop.m_minigameMetaData.id;
			}
			if (PsState.m_activeGameLoop.m_minigameMetaData.name != null)
			{
				text2 = PsState.m_activeGameLoop.m_minigameMetaData.name;
			}
			text3 = PsState.m_activeGameLoop.m_minigameMetaData.hidden.ToString();
			num = PsState.m_activeGameLoop.m_nodeId;
			num2 = PsState.m_activeGameLoop.m_levelNumber;
		}
		Parameter[] array = new Parameter[]
		{
			new Parameter("vehicle", _vehicle),
			new Parameter("gameMode", _gameMode),
			new Parameter("context", _context),
			new Parameter("minigameId", text),
			new Parameter("minigameName", text2),
			new Parameter("hidden", text3),
			new Parameter("nodeNumber", (long)num),
			new Parameter("levelNumber", (long)num2),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelRunStarted", array);
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00072738 File Offset: 0x00070B38
	public static void LevelGoalReached()
	{
		Debug.Log("E_Test LevelGoalReached", null);
		PsMetrics.m_goalReached = true;
		PsMetrics.m_goalReachedCount++;
		Parameter[] array = new Parameter[]
		{
			new Parameter("minigameId", PsState.m_activeGameLoop.m_minigameMetaData.id),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelGoalReached", array);
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x000727A0 File Offset: 0x00070BA0
	public static void LevelGoalReached(string _vehicle, string _gameMode, string _context, int _position = 0)
	{
		Debug.Log("E_Test LevelGoalReached params position: " + _position, null);
		PsMetrics.m_goalReached = true;
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = "false";
		int num = 0;
		int num2 = 0;
		PsMetrics.m_endposition = _position;
		PsMetrics.m_goalReachedCount++;
		if (PsState.m_activeGameLoop != null && PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			if (PsState.m_activeGameLoop.m_minigameMetaData.id != null)
			{
				text = PsState.m_activeGameLoop.m_minigameMetaData.id;
			}
			if (PsState.m_activeGameLoop.m_minigameMetaData.name != null)
			{
				text2 = PsState.m_activeGameLoop.m_minigameMetaData.name;
			}
			text3 = PsState.m_activeGameLoop.m_minigameMetaData.hidden.ToString();
			num = PsState.m_activeGameLoop.m_nodeId;
			num2 = PsState.m_activeGameLoop.m_levelNumber;
		}
		Parameter[] array = new Parameter[]
		{
			new Parameter("vehicle", _vehicle),
			new Parameter("gameMode", _gameMode),
			new Parameter("context", _context),
			new Parameter("minigameId", text),
			new Parameter("minigameName", text2),
			new Parameter("hidden", text3),
			new Parameter("nodeNumber", (long)num),
			new Parameter("levelNumber", (long)num2),
			new Parameter("endPosition", (long)_position),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelGoalReached", array);
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x0007292C File Offset: 0x00070D2C
	public static void LevelRestarted()
	{
		Debug.Log("E_Test LevelRestarted", null);
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("minigameId", PsState.m_activeGameLoop.m_minigameMetaData.id);
		dictionary.Add("wonPreviousRun", PsMetrics.m_goalReached.ToString());
		Metrics.SendEvent("LevelRestarted", dictionary, true);
		Parameter[] array = new Parameter[]
		{
			new Parameter("minigameId", PsState.m_activeGameLoop.m_minigameMetaData.id),
			new Parameter("wonPreviousRun", PsMetrics.m_goalReached.ToString()),
			new Parameter("previousPosition", (long)PsMetrics.m_endposition),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelRestarted", array);
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x000729F8 File Offset: 0x00070DF8
	public static void MinigameSkipped(string _context, PsMinigameMetaData _meta, long _timeSpent)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("Context", _context);
		dictionary.Add("Mode", _meta.gameMode.ToString());
		dictionary.Add("minigameId", _meta.id);
		dictionary.Add("minigameName", _meta.name);
		dictionary.Add("timeSpent", _timeSpent.ToString());
		Metrics.SendEvent("MinigameSkipped", dictionary, true);
		Parameter[] array = new Parameter[]
		{
			new Parameter("context", _context),
			new Parameter("gameMode", _meta.gameMode.ToString()),
			new Parameter("minigameId", _meta.id),
			new Parameter("minigameName", _meta.name),
			new Parameter("timeSpent", _timeSpent),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("LevelSkipped", array);
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00072AFC File Offset: 0x00070EFC
	public static void PlayerDied()
	{
		Debug.Log("E_Test PlayerDied", null);
		PsMetrics.m_goalReached = false;
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("minigameId", PsState.m_activeGameLoop.m_minigameMetaData.id);
		Metrics.SendEvent("PlayerDied", dictionary, true);
		string text = string.Empty;
		string empty = string.Empty;
		if (PsState.m_activeGameLoop != null && PsState.m_activeGameLoop.m_minigameMetaData != null && PsState.m_activeGameLoop.m_minigameMetaData.id != null)
		{
			text = PsState.m_activeGameLoop.m_minigameMetaData.id;
		}
		Parameter[] array = new Parameter[]
		{
			new Parameter("minigameId", text),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PlayerDied", array);
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00072BCC File Offset: 0x00070FCC
	public static void PlayerDied(string _vehicle, string _gameMode, string _context)
	{
		Debug.Log("E_Test PlayerDied", null);
		PsMetrics.m_goalReached = false;
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("minigameId", PsState.m_activeGameLoop.m_minigameMetaData.id);
		Metrics.SendEvent("PlayerDied", dictionary, true);
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = "false";
		int num = 0;
		int num2 = 0;
		if (PsState.m_activeGameLoop != null && PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			if (PsState.m_activeGameLoop.m_minigameMetaData.id != null)
			{
				text = PsState.m_activeGameLoop.m_minigameMetaData.id;
			}
			if (PsState.m_activeGameLoop.m_minigameMetaData.name != null)
			{
				text2 = PsState.m_activeGameLoop.m_minigameMetaData.name;
			}
			text3 = PsState.m_activeGameLoop.m_minigameMetaData.hidden.ToString();
			num = PsState.m_activeGameLoop.m_nodeId;
			num2 = PsState.m_activeGameLoop.m_levelNumber;
		}
		Parameter[] array = new Parameter[]
		{
			new Parameter("vehicle", _vehicle),
			new Parameter("gameMode", _gameMode),
			new Parameter("context", _context),
			new Parameter("minigameId", text),
			new Parameter("minigameName", text2),
			new Parameter("hidden", text3),
			new Parameter("nodeNumber", (long)num),
			new Parameter("levelNumber", (long)num2),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PlayerDied", array);
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x00072D5C File Offset: 0x0007115C
	public static void RaceTriesOffered()
	{
		Debug.Log("E_Test RaceTriesOffered", null);
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		Metrics.SendEvent("RaceTriesOffered", dictionary, true);
		Parameter[] array = new Parameter[]
		{
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("RaceTriesOffered", array);
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x00072DA8 File Offset: 0x000711A8
	public static void RaceTriesBought(int _count, string _type, int _price)
	{
		Debug.Log(string.Concat(new object[] { "E_Test RaceTriesBought ", _count, " ", _type, " ", _price }), null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("raceTriesAmountReceived", (long)_count),
			new Parameter("raceTriesPaymentType", _type),
			new Parameter("raceTriesPrice", (long)_price),
			new Parameter(FirebaseAnalytics.ParameterValue, (long)_count)
		};
		FirebaseAnalytics.LogEvent("RaceTriesBought", array);
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00072E41 File Offset: 0x00071241
	public static void MinigameStarted(PsGameLoop _minigameInfo)
	{
		Analytics.MinigameStarted(_minigameInfo.GetGameId());
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x00072E4F File Offset: 0x0007124F
	public static void LevelEditorEntered()
	{
		Debug.Log("E_Test LevelEditorEntered", null);
		PsMetrics.m_editorStartTime = (double)Time.time;
		if (!PlayerPrefsX.GetEditorUseFlag())
		{
			PlayerPrefsX.SetEditorUseFlag(true);
		}
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x00072E77 File Offset: 0x00071277
	public static void GoalBallStateChanged(string _newState)
	{
		PsMetrics.m_goalBallStyle = _newState;
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x00072E80 File Offset: 0x00071280
	private static void EditorItemsInPubs(string _minigameId, string _identifier, int _amount)
	{
		Debug.Log(string.Concat(new object[] { "E_Test EditorItemsInPubs ", _identifier, " ", _amount }), null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("minigameId", _minigameId),
			new Parameter("itemName", _identifier),
			new Parameter("count", (long)_amount),
			new Parameter(FirebaseAnalytics.ParameterValue, (long)_amount)
		};
		FirebaseAnalytics.LogEvent("EditorItemsInPubs", array);
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x00072F08 File Offset: 0x00071308
	public static void LevelEditorExited(bool _published)
	{
		Debug.Log("E_Test LevelEditorExited ", null);
		if (PsState.m_activeGameLoop.m_minigameMetaData.id != null)
		{
			if (_published)
			{
				foreach (KeyValuePair<string, ObscuredInt> keyValuePair in PsState.m_activeGameLoop.m_minigameMetaData.itemsCount)
				{
					PsMetrics.EditorItemsInPubs(PsState.m_activeGameLoop.m_minigameMetaData.id, keyValuePair.Key, keyValuePair.Value);
				}
				FrbMetrics.TrackPublishedLevelCount();
			}
			if (PsMetrics.m_timeSpent == 0.0)
			{
				PsMetrics.m_timeSpent = Math.Round((double)Time.time - PsMetrics.m_editorStartTime, 2);
			}
			string text = string.Empty;
			if (PsState.m_activeMinigame.m_settings.Contains("domeSizeIndex"))
			{
				text = PsMetrics.DomeSizeToString((int)PsState.m_activeMinigame.m_settings["domeSizeIndex"]);
			}
			PsMetrics.m_timeSpent = 0.0;
			if (_published && !PlayerPrefsX.GetPublishFlag())
			{
				PlayerPrefsX.SetPublishFlag(true);
			}
			float overrideCC = PsState.m_activeGameLoop.m_minigameMetaData.overrideCC;
			Parameter[] array = new Parameter[]
			{
				new Parameter("minigameId", PsState.m_activeGameLoop.m_minigameMetaData.id),
				new Parameter("vehicle", PsState.m_activeGameLoop.m_minigameMetaData.playerUnit.ToString()),
				new Parameter("gameMode", PsState.m_activeGameLoop.m_minigameMetaData.gameMode.ToString()),
				new Parameter("domeSize", text),
				new Parameter("timeSpent", PsMetrics.m_timeSpent),
				new Parameter("published", _published.ToString()),
				new Parameter("goalBallStyle", PsMetrics.m_goalBallStyle),
				new Parameter("overrideCC", (double)overrideCC),
				new Parameter(FirebaseAnalytics.ParameterValue, 1L)
			};
			FirebaseAnalytics.LogEvent("LevelEditorExited", array);
		}
		else
		{
			PsMetrics.m_levelEditorExitedDelayed = true;
			PsMetrics.m_timeSpent = Math.Round((double)Time.time - PsMetrics.m_editorStartTime, 2);
		}
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x00073158 File Offset: 0x00071558
	private static void DelayedLevelEditorExited()
	{
		PsMetrics.m_levelEditorExitedDelayed = false;
		PsMetrics.LevelEditorExited(false);
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x00073168 File Offset: 0x00071568
	private static string DomeSizeToString(int _domeSize)
	{
		string text = string.Empty;
		if (_domeSize == 0)
		{
			text = "tiny";
		}
		else if (_domeSize == 1)
		{
			text = "big";
		}
		else if (_domeSize == 2)
		{
			text = "huge";
		}
		else
		{
			text = _domeSize.ToString();
		}
		return text;
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x000731BF File Offset: 0x000715BF
	public static void LevelSaved()
	{
		if (PsMetrics.m_levelEditorExitedDelayed)
		{
			PsMetrics.DelayedLevelEditorExited();
		}
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x000731D0 File Offset: 0x000715D0
	public static void PublishedLevelDeleted(string _miniGameId)
	{
		Debug.Log("E_Test PublishedLevelDeleted", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("minigameId", _miniGameId),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PubLevelDeleted", array);
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00073218 File Offset: 0x00071618
	public static void PublishedLevelBackToSaved(string _miniGameId)
	{
		Debug.Log("E_Test PublishedLevelBackToSaved", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("minigameId", _miniGameId),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PubLevelBackToSaved", array);
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x0007325F File Offset: 0x0007165F
	public static void ValidatedPurchase(string _identifier, string _total, string _currency, string _transactionId)
	{
		Metrics.SendRevenueEvent(_total, _currency, _transactionId);
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x00073269 File Offset: 0x00071669
	public static void ButtonPressed(string _label, string _context = null, string _playerUnit = null)
	{
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0007326C File Offset: 0x0007166C
	public static void NewsOpened(string _newsType, string _newsName, string _newsHeader, string _context)
	{
		Debug.Log("E_Test NewsOpened() ", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("newsType", _newsType),
			new Parameter("newsName", _newsName),
			new Parameter("newsHeader", _newsHeader),
			new Parameter("context", _context),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("NewsOpened", array);
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x000732E0 File Offset: 0x000716E0
	public static void PushMessageLaunch(string _type = "")
	{
		Debug.Log("E_Test PushMessageLaunch ", null);
		Parameter[] array = new Parameter[]
		{
			new Parameter("pushType", _type),
			new Parameter(FirebaseAnalytics.ParameterValue, 1L)
		};
		FirebaseAnalytics.LogEvent("PushMessageLaunch", array);
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00073327 File Offset: 0x00071727
	public static void AnnouncementVideoOpened(string _eventType = "eventType", string _eventName = "eventName", string _eventStateId = "eventStateId", string _eventStateName = "eventStateName")
	{
		Debug.Log("E_Test AnnouncementVideoOpened", null);
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00073334 File Offset: 0x00071734
	public static void ParticipationFormOpened(string _eventType = "eventType", string _eventName = "eventName")
	{
		Debug.Log("E_Test ParticipationFormOpened", null);
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00073341 File Offset: 0x00071741
	public static void EntryVideoListOpened(string _eventType = "eventType", string _eventName = "eventName", string _eventStateId = "eventStateId", string _eventStateName = "eventStateName")
	{
		Debug.Log("E_Test EntryVideoListOpened", null);
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x0007334E File Offset: 0x0007174E
	public static void SubmittedLevelSearchOpened(string _eventName, string _levelId, string _levelName, string _levelCreator)
	{
		Debug.Log("E_Test SubmittedLevelSearchOpened", null);
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x0007335B File Offset: 0x0007175B
	public static void EntryVideoOpened(string _eventName, string _levelId, string _levelName, string _levelCreator)
	{
		Debug.Log("E_Test EntryVideoOpened", null);
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x00073368 File Offset: 0x00071768
	public static void LevelMakingOfVideoOpened(string _levelId, string _levelName, string _creatorId, string _creatorName, string _youtubeId, string _youtubeName)
	{
		Debug.Log("E_Test MakingOfVideoWatched", null);
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x00073375 File Offset: 0x00071775
	public static void ChristmasGiftClaimed(string _giftName)
	{
		Debug.Log("E_Test ChristmasGiftClaimed " + _giftName, null);
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00073388 File Offset: 0x00071788
	private static void LogDict(Dictionary<string, string> _dict)
	{
		foreach (KeyValuePair<string, string> keyValuePair in _dict)
		{
			Debug.Log("E_Test Analytics: " + keyValuePair.Key + " - " + keyValuePair.Value, null);
		}
	}

	// Token: 0x04000A28 RID: 2600
	private static int m_diamondUsesAfterPurchase;

	// Token: 0x04000A29 RID: 2601
	private static bool m_trackDiamondsAfterPurchase;

	// Token: 0x04000A2A RID: 2602
	private static double m_levelStartTime;

	// Token: 0x04000A2B RID: 2603
	private static double m_editorStartTime;

	// Token: 0x04000A2C RID: 2604
	private static double m_tournamentPlayStartTime;

	// Token: 0x04000A2D RID: 2605
	private static double m_tournamentRunStartTime;

	// Token: 0x04000A2E RID: 2606
	private static double m_timeSpent;

	// Token: 0x04000A2F RID: 2607
	private static bool m_levelEditorExitedDelayed;

	// Token: 0x04000A30 RID: 2608
	private static bool m_sessionwideCheat;

	// Token: 0x04000A31 RID: 2609
	private static bool m_goalReached;

	// Token: 0x04000A32 RID: 2610
	private static int m_goalReachedCount;

	// Token: 0x04000A33 RID: 2611
	private static int m_tempCC = 66;

	// Token: 0x04000A34 RID: 2612
	private static string m_tempHat = "MotocrossHelmet";

	// Token: 0x04000A35 RID: 2613
	private static string m_adNetwork = string.Empty;

	// Token: 0x04000A36 RID: 2614
	private static bool m_currentBossDefeated;

	// Token: 0x04000A37 RID: 2615
	private static float m_currentHandicap;

	// Token: 0x04000A38 RID: 2616
	private static int m_powerupsReceived;

	// Token: 0x04000A39 RID: 2617
	private static int m_powerupsUsed;

	// Token: 0x04000A3A RID: 2618
	private static string m_temp_trail = string.Empty;

	// Token: 0x04000A3B RID: 2619
	private static int m_coin_booster_debug_counter;

	// Token: 0x04000A3C RID: 2620
	private static int m_dirtbike_bundle_debug_counter;

	// Token: 0x04000A3D RID: 2621
	private static bool m_loveUsPopupOpen;

	// Token: 0x04000A3E RID: 2622
	private static int m_endposition;

	// Token: 0x04000A3F RID: 2623
	private static int m_levelRunCount;

	// Token: 0x04000A40 RID: 2624
	private static string m_goalBallStyle = "default";
}
