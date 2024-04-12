using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CodeStage.AntiCheat.Detectors;
using CodeStage.AntiCheat.ObscuredTypes;
using InAppPurchases;
using MiniJSON;
using Prime31;
using Server;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000143 RID: 323
public static class PsMetagameManager
{
	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0006847C File Offset: 0x0006687C
	public static DateTime CurrentDateTime
	{
		get
		{
			DateTime dateTime;
			dateTime..ctor(1970, 1, 1);
			DateTime dateTime2 = dateTime.AddDays((double)PsMetagameManager.m_epochDays);
			TimeSpan timeSpan;
			timeSpan..ctor(0, 0, PsMetagameManager.m_daySecondsLeft);
			return dateTime2.AddDays(1.0).Subtract(timeSpan);
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000A14 RID: 2580 RVA: 0x000684D0 File Offset: 0x000668D0
	public static TimeSpan MonthTimeLeft
	{
		get
		{
			DateTime currentDateTime = PsMetagameManager.CurrentDateTime;
			DateTime dateTime;
			dateTime..ctor(currentDateTime.Year, currentDateTime.Month, 1);
			return dateTime.AddMonths(1).Subtract(PsMetagameManager.CurrentDateTime);
		}
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00068510 File Offset: 0x00066910
	public static void ParseGemPrices(Dictionary<string, object> _config)
	{
		if (_config.ContainsKey("skipPrice"))
		{
			PsMetagameManager.m_skipPrice = Convert.ToInt32(_config["skipPrice"]);
		}
		if (_config.ContainsKey("gemsPerMinute"))
		{
			PsMetagameManager.m_timerPriceGemsPerMinute = Convert.ToSingle(_config["gemsPerMinute"]);
		}
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x00068574 File Offset: 0x00066974
	public static void ParseEditorItemLimits(Dictionary<string, object> _config)
	{
		if (_config.ContainsKey("commonLimit"))
		{
			PsMetagameManager.m_commonLimit = Convert.ToInt32(_config["commonLimit"]);
		}
		if (_config.ContainsKey("rareLimit"))
		{
			PsMetagameManager.m_rareLimit = Convert.ToInt32(_config["rareLimit"]);
		}
		if (_config.ContainsKey("epicLimit"))
		{
			PsMetagameManager.m_epicLimit = Convert.ToInt32(_config["epicLimit"]);
		}
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00068600 File Offset: 0x00066A00
	public static void Initialize()
	{
		PsMetagameManager.m_utilityEntity = EntityManager.AddEntity("MetagameManagerUtility");
		PsMetagameManager.m_utilityEntity.m_persistent = true;
		PsMetagameManager.m_keyReloadTimeSeconds = PsMetagameManager.GetSecondsFromTimeString(PsMetagameManager.GetKeyReloadTime());
		PsMetagameManager.m_dailyChallenges = new List<PsChallengeMetaData>();
		ObscuredCheatingDetector.StartDetection(new UnityAction(PsMetagameManager.ClownDetection));
		PsMetagameManager.m_initialized = true;
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00068668 File Offset: 0x00066A68
	public static void ClownDetection()
	{
		PsMetagameManager.m_suspiciousActivity = true;
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00068670 File Offset: 0x00066A70
	public static void ShowResources(Camera _camera = null, bool _horizontal = false, bool _coins = true, bool _diamonds = true, bool _separateShopButton = false, float _topMargin = 0.03f, bool _showShopButtons = false, bool _showCopper = false, bool _showShards = false)
	{
		if (PsMetagameManager.m_menuResourceView != null)
		{
			PsMetagameManager.HideResources();
		}
		PsMetagameManager.m_menuResourceView = new PsUIMainMenuResources(_camera, _horizontal, _coins, _diamonds, _topMargin, _showShopButtons, _separateShopButton, null, _showCopper, _showShards);
		PsMetagameManager.m_menuResourceView.SetAlign(1f, 1f);
		PsMetagameManager.m_menuResourceView.Update();
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x000686C4 File Offset: 0x00066AC4
	public static void CreateSplashBG()
	{
		if (PsMetagameManager.m_splashBG == null)
		{
			PsMetagameManager.m_splashBG = new PsUIBasePopup(typeof(PsSplashScreen), null, null, null, true, false, InitialPage.Center, false, false, false);
			EntityManager.RemoveAllTagsFromEntity(PsMetagameManager.m_splashBG.m_TC.p_entity, false);
			PsMetagameManager.m_splashBG.m_TC.p_entity.m_persistent = true;
		}
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00068722 File Offset: 0x00066B22
	public static void DestroySplashBG()
	{
		if (PsMetagameManager.m_splashBG != null)
		{
			PsMetagameManager.m_splashBG.Destroy();
			PsMetagameManager.m_splashBG = null;
		}
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x0006873E File Offset: 0x00066B3E
	public static void HideResources()
	{
		if (PsMetagameManager.m_menuResourceView == null)
		{
			return;
		}
		PsMetagameManager.m_menuResourceView.Destroy();
		PsMetagameManager.m_menuResourceView = null;
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x0006875B File Offset: 0x00066B5B
	public static void SetSeasonEndData(SeasonEndData _data)
	{
		PsMetagameManager.m_seasonEndData = _data;
		PsMetagameManager.m_seasonTimeleft = PsMetagameManager.m_seasonEndData.currentSeason.timeLeft;
		PsMetagameManager.m_seasonTimerStart = (int)Math.Floor(Main.m_EPOCHSeconds);
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00068787 File Offset: 0x00066B87
	public static void SetLastLoginInfo()
	{
		PsMetagameManager.m_secondsSinceLastLogin = 0;
		PsMetagameManager.m_sinceLastLoginStart = (int)Math.Floor(Main.m_EPOCHSeconds);
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x000687A0 File Offset: 0x00066BA0
	public static void InitializeGachas()
	{
		PsMetagameManager.m_gachasInitialized = true;
		PsMetagameManager.ParseGachaData();
		if (PsMetagameManager.m_vehicleGachaData == null)
		{
			Debug.LogWarning("NO GACHA DATA");
			PsMetagameManager.CreateNewGachaData();
		}
		else
		{
			Debug.LogWarning("GACHA DATA FOUND: " + PsMetagameManager.m_playerStats.gachaData);
		}
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x000687F0 File Offset: 0x00066BF0
	public static void ParseGachaData()
	{
		Debug.LogWarning("PARSING GACHA DATA");
		if (string.IsNullOrEmpty(PsMetagameManager.m_playerStats.gachaData))
		{
			PsMetagameManager.m_vehicleGachaData = null;
		}
		else
		{
			List<PsMetagameManager.PsVehicleGachaData> list = new List<PsMetagameManager.PsVehicleGachaData>();
			char[] array = new char[] { ';' };
			char[] array2 = new char[] { ':' };
			string[] array3 = PsMetagameManager.m_playerStats.gachaData.Split(array, 0);
			for (int i = 0; i < array3.Length; i++)
			{
				string[] array4 = array3[i].Split(array2, 0);
				PsMetagameManager.PsVehicleGachaData psVehicleGachaData = new PsMetagameManager.PsVehicleGachaData();
				psVehicleGachaData.m_vehicleIndex = Convert.ToInt32(array4[0]);
				psVehicleGachaData.m_rivalGhostId = array4[1];
				psVehicleGachaData.m_rivalWonCount = Convert.ToInt32(array4[2]);
				psVehicleGachaData.m_mapSeed = Convert.ToInt32(array4[3]);
				psVehicleGachaData.m_mapPieceCount = Convert.ToInt32(array4[4]);
				psVehicleGachaData.m_mapPiecesMax = 12;
				if (array4.Length > 6)
				{
					psVehicleGachaData.m_racingGachaCount = Convert.ToInt32(array4[6]);
				}
				if (array4.Length > 7)
				{
					psVehicleGachaData.m_adventureGachaCount = Convert.ToInt32(array4[7]);
				}
				if (array4.Length > 8)
				{
					psVehicleGachaData.m_offroadCarUpgradeChest = Convert.ToInt32(array4[8]);
				}
				if (array4.Length > 9)
				{
					psVehicleGachaData.m_motorcycleUpgradeChest = Convert.ToInt32(array4[9]);
				}
				list.Add(psVehicleGachaData);
			}
			if (PsState.m_previousLoginVersion <= 267 && Main.CLIENT_VERSION() > 267)
			{
				if (!PsGachaManager.m_gachaSet)
				{
					PsGachaManager.m_giveConsolation = true;
				}
				else
				{
					for (int j = 0; j < 2; j++)
					{
						PsGachaManager.AddGacha(new PsGacha(GachaType.GOLD)
						{
							m_unlocked = true,
							m_unlockTimeLeft = 0,
							m_unlockTime = 0,
							m_unlockStartedTime = 0.0
						}, j, false);
						PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
					}
				}
				list[0].m_adventureGachaCount++;
				list[0].m_racingGachaCount++;
				list[0].m_mapPieceCount = 12;
				list[0].m_rivalWonCount = 4;
				if (list.Count > 1)
				{
					list[1] = null;
				}
				PsMetagameManager.m_vehicleGachaData = list[0];
				PsMetagameManager.SendCurrentGachaData(true);
			}
			else
			{
				PsMetagameManager.m_vehicleGachaData = list[0];
			}
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00068A8B File Offset: 0x00066E8B
	public static void ClearGachaDataRival(int _vehicleIndex, bool _sendToServer)
	{
		if (PsMetagameManager.m_vehicleGachaData == null)
		{
			return;
		}
		PsMetagameManager.m_vehicleGachaData.m_rivalGhostId = string.Empty;
		PsMetagameManager.m_vehicleGachaData.m_rivalWonCount = 0;
		PsMetagameManager.SendCurrentGachaData(_sendToServer);
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00068AB8 File Offset: 0x00066EB8
	public static void ClearGachaDataMap(int _vehicleIndex, bool _sendToServer)
	{
		if (PsMetagameManager.m_vehicleGachaData == null)
		{
			return;
		}
		PsMetagameManager.m_vehicleGachaData.m_mapSeed = (int)(Time.realtimeSinceStartup * 7431f * (Random.value + 1f));
		PsMetagameManager.m_vehicleGachaData.m_mapPieceCount = 0;
		PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax = 12;
		PsMetagameManager.SendCurrentGachaData(_sendToServer);
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00068B0F File Offset: 0x00066F0F
	public static int GetLastOpenedUpgradeChest(Type _vehicleType)
	{
		if (_vehicleType == typeof(Motorcycle))
		{
			return PsMetagameManager.m_vehicleGachaData.m_motorcycleUpgradeChest;
		}
		if (_vehicleType == typeof(OffroadCar))
		{
			return PsMetagameManager.m_vehicleGachaData.m_offroadCarUpgradeChest;
		}
		return -1;
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00068B48 File Offset: 0x00066F48
	public static void SetLastOpenedUpgradeChest(Type _vehicleType, int _upgradeChest)
	{
		if (_vehicleType == typeof(Motorcycle))
		{
			PsMetagameManager.m_vehicleGachaData.m_motorcycleUpgradeChest = _upgradeChest;
		}
		else
		{
			if (_vehicleType != typeof(OffroadCar))
			{
				return;
			}
			PsMetagameManager.m_vehicleGachaData.m_offroadCarUpgradeChest = _upgradeChest;
		}
		PsMetagameManager.SendCurrentGachaData(true);
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x00068B9C File Offset: 0x00066F9C
	public static int GetOpenedChestCount()
	{
		int num = 0;
		num += PsMetagameManager.m_vehicleGachaData.m_adventureGachaCount;
		return num + PsMetagameManager.m_vehicleGachaData.m_racingGachaCount;
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00068BC8 File Offset: 0x00066FC8
	public static void CreateNewGachaData()
	{
		List<PsMetagameManager.PsVehicleGachaData> list = new List<PsMetagameManager.PsVehicleGachaData>();
		for (int i = 0; i < 1; i++)
		{
			PsMetagameManager.PsVehicleGachaData psVehicleGachaData = new PsMetagameManager.PsVehicleGachaData();
			psVehicleGachaData.m_vehicleIndex = i;
			psVehicleGachaData.m_rivalGhostId = string.Empty;
			psVehicleGachaData.m_rivalWonCount = 0;
			psVehicleGachaData.m_mapSeed = (int)(Time.realtimeSinceStartup * 7431f * (Random.value + 1f));
			if (PsMetagameManager.m_firstTimeLogin && PsState.m_vehicleTypes.Length >= i + 1 && PsState.m_vehicleTypes[i] == typeof(OffroadCar))
			{
				psVehicleGachaData.m_mapPieceCount = 6;
				PsMetagameManager.m_firstTimeLogin = false;
			}
			else
			{
				psVehicleGachaData.m_mapPieceCount = 0;
			}
			psVehicleGachaData.m_mapPiecesMax = 12;
			list.Add(psVehicleGachaData);
		}
		PsMetagameManager.m_vehicleGachaData = list[0];
		PsMetagameManager.SendCurrentGachaData(true);
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x00068C94 File Offset: 0x00067094
	public static void SendCurrentGachaData(bool _sendToServer = true)
	{
		if (PsMetagameManager.m_vehicleGachaData == null)
		{
			PsMetagameManager.CreateNewGachaData();
		}
		else
		{
			string text = string.Empty;
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				PsMetagameManager.m_vehicleGachaData.m_vehicleIndex,
				":",
				PsMetagameManager.m_vehicleGachaData.m_rivalGhostId,
				":",
				PsMetagameManager.m_vehicleGachaData.m_rivalWonCount,
				":",
				PsMetagameManager.m_vehicleGachaData.m_mapSeed,
				":",
				PsMetagameManager.m_vehicleGachaData.m_mapPieceCount,
				":",
				PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax,
				":",
				PsMetagameManager.m_vehicleGachaData.m_racingGachaCount,
				":",
				PsMetagameManager.m_vehicleGachaData.m_adventureGachaCount,
				":",
				PsMetagameManager.m_vehicleGachaData.m_offroadCarUpgradeChest,
				":",
				PsMetagameManager.m_vehicleGachaData.m_motorcycleUpgradeChest
			});
			PsMetagameManager.m_playerStats.gachaData = text;
			if (_sendToServer)
			{
				PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
			}
			Debug.LogWarning("NEW GACHA DATA: " + PsMetagameManager.m_playerStats.gachaData);
		}
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x00068E40 File Offset: 0x00067240
	public static void InitializeTimedSpecialOffers(params PsTimedSpecialOffer[] _timedSpecialOffers)
	{
		if (PsMetagameManager.m_timedSpecialOffers == null)
		{
			PsMetagameManager.m_timedSpecialOffers = new List<PsTimedSpecialOffer>();
			PsMetagameManager.m_timedSpecialOffers.AddRange(_timedSpecialOffers);
		}
		else if (_timedSpecialOffers != null)
		{
			for (int i = 0; i < _timedSpecialOffers.Length; i++)
			{
				if (_timedSpecialOffers[i] != null)
				{
					PsMetagameManager.AddTimedSpecialOffer(_timedSpecialOffers[i]);
				}
			}
			for (int j = PsMetagameManager.m_timedSpecialOffers.Count - 1; j >= 0; j--)
			{
				if (PsMetagameManager.m_timedSpecialOffers[j] != null && PsMetagameManager.m_timedSpecialOffers[j].m_state == 2)
				{
					PsMetagameManager.m_timedSpecialOffers.RemoveAt(j);
				}
			}
		}
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00068EEC File Offset: 0x000672EC
	private static void UpdateTimedSpecialOffers()
	{
		if (PsMetagameManager.m_timedSpecialOffers != null && PsMetagameManager.m_timedSpecialOffers.Count > 0)
		{
			for (int i = PsMetagameManager.m_timedSpecialOffers.Count - 1; i >= 0; i--)
			{
				if (PsMetagameManager.m_timedSpecialOffers[i] != null && PsMetagameManager.m_timedSpecialOffers[i].m_state == 1)
				{
					PsMetagameManager.m_timedSpecialOffers[i].m_timeLeft = PsMetagameManager.m_specialOfferDurationMinutes * 60 - (int)((long)Math.Floor(Main.m_EPOCHSeconds) - PsMetagameManager.m_timedSpecialOffers[i].m_startTime);
					if (PsMetagameManager.m_timedSpecialOffers[i].m_timeLeft <= 0)
					{
						PsMetagameManager.m_timedSpecialOffers[i].m_state = 2;
						PsMetagameManager.m_timedSpecialOffers.RemoveAt(i);
					}
				}
			}
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00068FC4 File Offset: 0x000673C4
	public static void AddTimedSpecialOffer(PsTimedSpecialOffer _specialOffer)
	{
		if (_specialOffer == null)
		{
			return;
		}
		for (int i = 0; i < PsMetagameManager.m_timedSpecialOffers.Count; i++)
		{
			if (PsMetagameManager.m_timedSpecialOffers[i] != null && PsMetagameManager.m_timedSpecialOffers[i].m_productId == _specialOffer.m_productId)
			{
				PsMetagameManager.m_timedSpecialOffers[i].m_state = _specialOffer.m_state;
				PsMetagameManager.m_timedSpecialOffers[i].m_startTime = _specialOffer.m_startTime;
				PsMetagameManager.m_timedSpecialOffers[i].m_timeLeft = _specialOffer.m_timeLeft;
				return;
			}
		}
		PsMetagameManager.m_timedSpecialOffers.Add(_specialOffer);
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00069074 File Offset: 0x00067474
	public static List<PsTimedSpecialOffer> GetStartedTimedSpecialOffers()
	{
		List<PsTimedSpecialOffer> list = new List<PsTimedSpecialOffer>();
		if (PsMetagameManager.m_timedSpecialOffers != null && PsMetagameManager.m_timedSpecialOffers.Count > 0)
		{
			for (int i = 0; i < PsMetagameManager.m_timedSpecialOffers.Count; i++)
			{
				if (PsMetagameManager.m_timedSpecialOffers[i] != null && PsMetagameManager.m_timedSpecialOffers[i].m_state == 1)
				{
					list.Add(PsMetagameManager.m_timedSpecialOffers[i]);
				}
			}
		}
		return list;
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x000690F4 File Offset: 0x000674F4
	public static void RemoveTimedSpecialOffer(PsTimedSpecialOffer _specialOffer)
	{
		if (PsMetagameManager.m_timedSpecialOffers != null && PsMetagameManager.m_timedSpecialOffers.Count > 0)
		{
			for (int i = 0; i < PsMetagameManager.m_timedSpecialOffers.Count; i++)
			{
				if (PsMetagameManager.m_timedSpecialOffers[i] != null && PsMetagameManager.m_timedSpecialOffers[i] == _specialOffer)
				{
					PsMetagameManager.m_timedSpecialOffers.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00069164 File Offset: 0x00067564
	public static PsTimedSpecialOffer GetUnstartedTimedSpecialOffer()
	{
		if (PsMetagameManager.m_timedSpecialOffers != null && PsMetagameManager.m_timedSpecialOffers.Count > 0)
		{
			for (int i = 0; i < PsMetagameManager.m_timedSpecialOffers.Count; i++)
			{
				if (PsMetagameManager.m_timedSpecialOffers[i] != null && PsMetagameManager.m_timedSpecialOffers[i].m_state == 0)
				{
					return PsMetagameManager.m_timedSpecialOffers[i];
				}
			}
		}
		return null;
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x000691D8 File Offset: 0x000675D8
	public static void StartSpecialOffer(PsTimedSpecialOffer _specialOffer)
	{
		_specialOffer.m_state = 1;
		_specialOffer.m_startTime = (long)Main.m_EPOCHSeconds;
		_specialOffer.m_timeLeft = PsMetagameManager.m_specialOfferDurationMinutes * 60 - (int)((long)Math.Floor(Main.m_EPOCHSeconds) - _specialOffer.m_startTime);
		SpecialOffer.StartData startData = new SpecialOffer.StartData();
		startData.specialOfferId = _specialOffer.m_productId;
		startData.startTime = (long)(Main.m_EPOCHSeconds * 1000.0);
		HttpC httpC = SpecialOffer.Start(startData, new Action<HttpC>(PsMetagameManager.SpecialOfferStartSUCCEED), new Action<HttpC>(PsMetagameManager.SpecialOfferStartFAILED), null);
		httpC.objectData = startData;
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x00069290 File Offset: 0x00067690
	private static void SpecialOfferStartSUCCEED(HttpC _httpC)
	{
		Debug.Log("SPECIAL OFFER START SUCCEED", null);
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x000692A0 File Offset: 0x000676A0
	private static void SpecialOfferStartFAILED(HttpC _httpC)
	{
		Debug.Log("SPECIAL OFFER START FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _httpC.www, delegate
		{
			HttpC httpC = SpecialOffer.Start(_httpC.objectData as SpecialOffer.StartData, new Action<HttpC>(PsMetagameManager.SpecialOfferStartSUCCEED), new Action<HttpC>(PsMetagameManager.SpecialOfferStartFAILED), null);
			httpC.objectData = _httpC.objectData;
			return httpC;
		}, null);
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x000692EC File Offset: 0x000676EC
	public static void OpenSpecialOfferChest(GachaType _chest)
	{
		PsGachaManager.m_lastOpenedGacha = _chest;
		PsGachaManager.m_lastGachaRewards = PsGachaManager.OpenGacha(new PsGacha(_chest), -1, false);
		Hashtable hashtable = new Hashtable();
		FrbMetrics.ChestOpened("special_offer");
		Hashtable updatedData = PsCustomisationManager.GetUpdatedData(PsUpgradeManager.GetUpdatedData(null));
		if (updatedData != null)
		{
			hashtable.Add("customisation", updatedData);
		}
		List<Dictionary<string, object>> updatedAchievements = PsAchievementManager.GetUpdatedAchievements(true);
		if (updatedAchievements != null)
		{
			hashtable.Add("achievements", updatedAchievements);
		}
		Dictionary<string, int> updatedEditorResources = PsMetagameManager.m_playerStats.GetUpdatedEditorResources();
		if (updatedEditorResources != null)
		{
			hashtable.Add("editorResources", updatedEditorResources);
		}
		List<string> list = new List<string>();
		hashtable.Add("update", ClientTools.GeneratePlayerSetData(new Hashtable(), ref list));
		SpecialOffer.ClaimData claimData = new SpecialOffer.ClaimData();
		claimData.chest = _chest.ToString();
		claimData.data = hashtable;
		new PsServerQueueFlow(null, delegate
		{
			HttpC httpC = SpecialOffer.ClaimChest(claimData, new Action<HttpC>(PsMetagameManager.ClaimSpecialOfferChestSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimSpecialOfferChestFAILED), null);
			httpC.objectData = claimData;
		}, new string[] { "SetData" });
		PsPurchaseHelper.StoreShopUpdateAction();
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterOpenGacha), null, null, null, true, true, InitialPage.Center, false, false, false);
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
			PsPurchaseHelper.UnstoreShopUpdateAction();
		});
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0006942B File Offset: 0x0006782B
	public static void ClaimSpecialOfferChestSUCCEED(HttpC _c)
	{
		Debug.Log("CLAIM SPECIAL OFFER CHEST SUCCEED", null);
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x00069438 File Offset: 0x00067838
	public static void ClaimSpecialOfferChestFAILED(HttpC _c)
	{
		Debug.Log("CLAIM SPECIAL OFFER CHEST FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC httpC = SpecialOffer.ClaimChest((SpecialOffer.ClaimData)_c.objectData, new Action<HttpC>(PsMetagameManager.ClaimSpecialOfferChestSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimSpecialOfferChestFAILED), null);
			httpC.objectData = _c.objectData;
			return httpC;
		}, null);
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x00069484 File Offset: 0x00067884
	public static int GetSpecialOfferValue(float _offerPrice, int _gemAmount, float _gemValue, int _coinsInGems, int _chestInGems, float _roundingThreshold = 0.75f)
	{
		float num = (float)(_gemAmount + _coinsInGems + _chestInGems) / _offerPrice / ((float)_gemAmount / _gemValue);
		float num2 = num - Mathf.Floor(num);
		if (num2 <= _roundingThreshold)
		{
			return Mathf.FloorToInt(num);
		}
		return Mathf.CeilToInt(num);
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x000694C0 File Offset: 0x000678C0
	public static void InitializeShop()
	{
		PsMetagameManager.m_shopInitialized = true;
		PsMetagameManager.m_lastShopUpdatedTime = Main.m_EPOCHSeconds - (double)(86400 - PsMetagameManager.m_daySecondsLeft);
		PsMetagameManager.ParseShopData();
		if (PsMetagameManager.m_shopUpgradeItems == null)
		{
			Debug.LogWarning("NO SHOP DATA");
			PsMetagameManager.CreateNewShopData();
		}
		else
		{
			Debug.LogWarning("SHOP DATA FOUND: " + PsMetagameManager.m_playerStats.cardPurchases);
			if (PsMetagameManager.m_shopUpgradeDay != PsMetagameManager.m_epochDays)
			{
				Debug.LogWarning("SHOP DATA OUT OF DATE");
				PsMetagameManager.CreateNewShopData();
			}
		}
		if (PsMetagameManager.d_shopUpdateAction != null)
		{
			PsMetagameManager.d_shopUpdateAction.Invoke();
		}
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00069558 File Offset: 0x00067958
	public static void CreateNewShopData()
	{
		PlayerPrefsX.SetShopNotification(true);
		PsMetagameManager.m_shopUpgradeDay = PsMetagameManager.m_epochDays;
		PsMetagameManager.m_shopUpgradeItems = PsMetagameManager.GetShopUpgrades();
		string text = PsMetagameManager.m_shopUpgradeDay.ToString();
		for (int i = 0; i < PsMetagameManager.m_shopUpgradeItems.Count; i++)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				";",
				PsMetagameManager.m_shopUpgradeItems[i].m_identifier,
				":",
				PsMetagameManager.m_shopUpgradeItems[i].m_purchaseCount
			});
		}
		PsMetagameManager.m_playerStats.cardPurchases = text;
		PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
		Debug.LogWarning("NEW SHOP DATA: " + PsMetagameManager.m_playerStats.cardPurchases);
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00069664 File Offset: 0x00067A64
	public static string GetCardPurchaseString()
	{
		if (PsMetagameManager.m_shopUpgradeItems == null)
		{
			return null;
		}
		string text = PsMetagameManager.m_shopUpgradeDay.ToString();
		for (int i = 0; i < PsMetagameManager.m_shopUpgradeItems.Count; i++)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				";",
				PsMetagameManager.m_shopUpgradeItems[i].m_identifier,
				":",
				PsMetagameManager.m_shopUpgradeItems[i].m_purchaseCount
			});
		}
		return text;
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x000696F8 File Offset: 0x00067AF8
	public static void ParseShopData()
	{
		Debug.LogWarning("PARSING SHOP DATA");
		if (string.IsNullOrEmpty(PsMetagameManager.m_playerStats.cardPurchases))
		{
			PsMetagameManager.m_shopUpgradeItems = null;
		}
		else
		{
			List<ShopUpgradeItemData> list = new List<ShopUpgradeItemData>();
			char[] array = new char[] { ';' };
			char[] array2 = new char[] { ':' };
			string[] array3 = PsMetagameManager.m_playerStats.cardPurchases.Split(array, 0);
			for (int i = 1; i < array3.Length; i++)
			{
				string[] array4 = array3[i].Split(array2, 0);
				list.Add(new ShopUpgradeItemData(array4[0], Convert.ToInt32(array4[1])));
			}
			PsMetagameManager.m_shopUpgradeDay = Convert.ToInt32(array3[0]);
			PsMetagameManager.m_shopUpgradeItems = list;
		}
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x000697B0 File Offset: 0x00067BB0
	public static ShopUpgradeItemData GetShopUpgradeItem(string _identifier)
	{
		if (PsMetagameManager.m_shopUpgradeItems == null)
		{
			Debug.LogError("INITIALIZE SHOP");
			return null;
		}
		for (int i = 0; i < PsMetagameManager.m_shopUpgradeItems.Count; i++)
		{
			if (PsMetagameManager.m_shopUpgradeItems[i].m_identifier == _identifier)
			{
				return PsMetagameManager.m_shopUpgradeItems[i];
			}
		}
		Debug.LogError("NO SHOP ITEM FOUND");
		return null;
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00069820 File Offset: 0x00067C20
	public static void ShopDayChange()
	{
		PsMetagameManager.m_daySecondsLeftPrevious = -1;
		PsMetagameManager.m_daySecondsLeft = PsMetagameManager.m_shopUpdateTime;
		PsMetagameManager.m_lastShopUpdatedTime = Main.m_EPOCHSeconds;
		PsMetagameManager.m_epochDays++;
		PsMetagameManager.CreateNewShopData();
		if (PsMetagameManager.d_shopUpdateAction != null)
		{
			PsMetagameManager.d_shopUpdateAction.Invoke();
		}
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x0006986C File Offset: 0x00067C6C
	public static List<ShopUpgradeItemData> GetShopUpgrades()
	{
		List<PsUpgradeItem> list = new List<PsUpgradeItem>();
		List<PsUpgradeItem> list2 = new List<PsUpgradeItem>();
		PsUpgradeData vehicleUpgradeData = PsUpgradeManager.GetVehicleUpgradeData(typeof(OffroadCar));
		for (int i = 0; i <= PsMetagameManager.m_playerStats.carRank; i++)
		{
			list2.AddRange(vehicleUpgradeData.GetUpgradeItemsByTier(i + 1));
		}
		PsUpgradeData vehicleUpgradeData2 = PsUpgradeManager.GetVehicleUpgradeData(typeof(Motorcycle));
		for (int j = 0; j <= PsMetagameManager.m_playerStats.mcRank; j++)
		{
			list.AddRange(vehicleUpgradeData2.GetUpgradeItemsByTier(j + 1));
		}
		GachaMachine<string> gachaMachine = new GachaMachine<string>();
		GachaMachine<string> gachaMachine2 = new GachaMachine<string>();
		GachaMachine<string> gachaMachine3 = new GachaMachine<string>();
		GachaMachine<string> gachaMachine4 = new GachaMachine<string>();
		GachaMachine<string> gachaMachine5 = new GachaMachine<string>();
		GachaMachine<string> gachaMachine6 = new GachaMachine<string>();
		for (int k = 0; k < list.Count; k++)
		{
			PsRarity rarity = list[k].m_rarity;
			if (rarity != PsRarity.Common)
			{
				if (rarity != PsRarity.Rare)
				{
					if (rarity == PsRarity.Epic)
					{
						gachaMachine3.AddItem(list[k].m_identifier, 1f, 1);
					}
				}
				else
				{
					gachaMachine2.AddItem(list[k].m_identifier, 1f, 1);
				}
			}
			else
			{
				gachaMachine.AddItem(list[k].m_identifier, 1f, 1);
			}
		}
		for (int l = 0; l < list2.Count; l++)
		{
			PsRarity rarity2 = list2[l].m_rarity;
			if (rarity2 != PsRarity.Common)
			{
				if (rarity2 != PsRarity.Rare)
				{
					if (rarity2 == PsRarity.Epic)
					{
						gachaMachine6.AddItem(list2[l].m_identifier, 1f, 1);
					}
				}
				else
				{
					gachaMachine5.AddItem(list2[l].m_identifier, 1f, 1);
				}
			}
			else
			{
				gachaMachine4.AddItem(list2[l].m_identifier, 1f, 1);
			}
		}
		List<ShopUpgradeItemData> list3 = new List<ShopUpgradeItemData>();
		list3.Add(new ShopUpgradeItemData(gachaMachine4.GetItem(true), 0));
		list3.Add(new ShopUpgradeItemData(gachaMachine5.GetItem(true), 0));
		list3.Add(new ShopUpgradeItemData(gachaMachine6.GetItem(true), 0));
		list3.Add(new ShopUpgradeItemData(gachaMachine.GetItem(true), 0));
		list3.Add(new ShopUpgradeItemData(gachaMachine2.GetItem(true), 0));
		list3.Add(new ShopUpgradeItemData(gachaMachine3.GetItem(true), 0));
		return list3;
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00069AF8 File Offset: 0x00067EF8
	public static bool IsVehicleUnlocked(Type _vehicleType)
	{
		return typeof(OffroadCar) == _vehicleType || (typeof(Motorcycle) == _vehicleType && (PsMetagameManager.m_playerStats.dirtBikeBundle || PsUpgradeManager.GetCurrentPerformance(typeof(OffroadCar)) >= 250f || PlayerPrefsX.GetMotorcyclePlay()));
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00069B5C File Offset: 0x00067F5C
	public static void Update()
	{
		if (ServerManager.m_playerAuthenticated && PsMetagameManager.m_initialized && !HttpS.m_gatesFreeze)
		{
			if (!PsMetagameManager.m_shopInitialized)
			{
				PsMetagameManager.InitializeShop();
			}
			if (!PsMetagameManager.m_gachasInitialized)
			{
				PsMetagameManager.InitializeGachas();
			}
			PsMetagameManager.m_queueWaitTicks--;
			if (ServerManager.m_httpcErrorList.Count > 0)
			{
				for (int i = ServerManager.m_httpcErrorList.Count - 1; i >= 0; i--)
				{
					if (ServerManager.m_httpcErrorList[i].executed)
					{
						ServerManager.m_httpcErrorList.Remove(ServerManager.m_httpcErrorList[i]);
					}
				}
				if (Main.m_gameTicks % 60 == 0 || ServerManager.m_httpcErrorList.Count == 0)
				{
					Debug.LogWarning("httpc error count: " + ServerManager.m_httpcErrorList.Count);
				}
				if (ServerManager.m_httpcErrorList.Count == 0)
				{
					ServerManager.RemoveConnectingPopup();
				}
			}
			if (PsMetagameManager.m_queueWaitTicks <= 0)
			{
				for (int j = 0; j < PsMetagameManager.m_serverQueueItems.Count; j++)
				{
					if (!PsMetagameManager.m_serverQueueItems[j].done)
					{
						PsMetagameManager.m_serverQueueItems[j].Update();
					}
					if (PsMetagameManager.m_serverQueueItems[j].done)
					{
						PsMetagameManager.m_serverQueueItems[j] = null;
						PsMetagameManager.m_serverQueueItems.Remove(PsMetagameManager.m_serverQueueItems[j]);
						j--;
					}
				}
				PsMetagameManager.m_queueWaitTicks = 0;
			}
		}
		if (PsMetagameManager.m_tutorialArrow != null)
		{
			PsMetagameManager.m_tutorialArrow.Step();
		}
		if (!PsMetagameManager.m_initialized || !ServerManager.m_playerAuthenticated || PsState.m_inLoginFlow || Main.m_currentGame.m_currentScene.m_name == "StartupScene" || Main.m_currentGame.m_currentScene.m_name == "PreloadingScene")
		{
			return;
		}
		if (PsIAPManager.ProductsReceived && PsIAPManager.HavePendingPurcheses && !PsState.m_inIapFlow)
		{
			HttpS.m_gatesFreeze = true;
			PsMetagameManager.RetryPendingPurchases();
		}
		PsMetagameManager.UpdateTimedSpecialOffers();
		PsGachaManager.Update(false);
		if (!PsMetagameManager.m_editorItemsGiven)
		{
			PsMetagameManager.m_editorItemsGiven = true;
			bool flag = false;
			if (PsState.m_previousLoginVersion < 308 && Main.CLIENT_VERSION() >= 308 && PsMetagameManager.m_playerStats.GetEditorResourceCount("PsPumpkin") == 0 && PsMetagameManager.m_playerStats.GetEditorResourceCount("DecorationalMagicCauldron") == 0)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				dictionary.Add("PsPumpkin", 2);
				dictionary.Add("DecorationalMagicCauldron", 2);
				PsMetagameManager.m_playerStats.CumulateEditorResources(dictionary);
				flag = true;
			}
			if (PsState.m_previousLoginVersion < 301 && Main.CLIENT_VERSION() >= 301 && PsMetagameManager.m_playerStats.GetEditorResourceCount("PsGravitySwitch") == 0 && PsMetagameManager.m_playerStats.GetEditorResourceCount("PsBlackHole") == 0)
			{
				Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
				dictionary2.Add("PsGravitySwitch", 2);
				dictionary2.Add("PsBlackHole", 2);
				PsMetagameManager.m_playerStats.CumulateEditorResources(dictionary2);
				flag = true;
			}
			if (PsState.m_previousLoginVersion < 273 && Main.CLIENT_VERSION() >= 273)
			{
				Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
				for (int k = 0; k < PsMetagameData.m_units.Count - 1; k++)
				{
					for (int l = 0; l < PsMetagameData.m_units[k].m_items.Count; l++)
					{
						if (PsMetagameData.m_units[k].m_items[l] is PsEditorItem)
						{
							PsEditorItem psEditorItem = PsMetagameData.m_units[k].m_items[l] as PsEditorItem;
							dictionary3.Add(psEditorItem.m_identifier, 99);
						}
					}
				}
				PsMetagameManager.m_playerStats.CumulateEditorResources(dictionary3);
				flag = true;
			}
			if (flag)
			{
				PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), new Hashtable(), PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().GetIdentifier(), false);
			}
		}
		Hashtable hashtable = null;
		if (hashtable != null)
		{
			Debug.LogError("Boostervalues: " + Json.Serialize(hashtable));
			PsMetagameManager.SetPlayerData(hashtable, false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
		}
		if (PsMetagameManager.m_seasonTimerStart < (int)Math.Floor(Main.m_EPOCHSeconds))
		{
			PsMetagameManager.m_seasonTimerStart = (int)Math.Floor(Main.m_EPOCHSeconds);
			PsMetagameManager.m_seasonTimeleft--;
			if (PsMetagameManager.m_seasonTimeleft < 0)
			{
				PsMetagameManager.m_seasonTimeleft = 0;
			}
		}
		if (PsMetagameManager.m_sinceLastLoginStart < (int)Math.Floor(Main.m_EPOCHSeconds))
		{
			PsMetagameManager.m_sinceLastLoginStart = (int)Math.Floor(Main.m_EPOCHSeconds);
			PsMetagameManager.m_secondsSinceLastLogin++;
		}
		PsMetagameManager.m_daySecondsLeftUpdated = false;
		PsMetagameManager.m_daySecondsLeft = (int)Math.Ceiling(PsMetagameManager.m_lastShopUpdatedTime + (double)PsMetagameManager.m_shopUpdateTime - Main.m_EPOCHSeconds);
		if (PsMetagameManager.m_daySecondsLeft != PsMetagameManager.m_daySecondsLeftPrevious)
		{
			if (PsMetagameManager.m_daySecondsLeft <= 0)
			{
				PsMetagameManager.ShopDayChange();
			}
			PsMetagameManager.m_daySecondsLeftUpdated = true;
			PsMetagameManager.m_daySecondsLeftPrevious = PsMetagameManager.m_daySecondsLeft;
		}
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0006A09C File Offset: 0x0006849C
	public static bool IsTimedGiftActive(EventGiftTimedType _type)
	{
		List<EventMessage> activeGifts = PsMetagameManager.m_giftEvents.GetActiveGifts();
		int num = 0;
		while (activeGifts != null && num < activeGifts.Count)
		{
			EventGiftTimed eventGiftTimed = activeGifts[num].giftContent as EventGiftTimed;
			if (eventGiftTimed != null && eventGiftTimed.m_timedType == _type)
			{
				return true;
			}
			num++;
		}
		return false;
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x0006A0F8 File Offset: 0x000684F8
	public static T GetActiveEventGift<T>() where T : class
	{
		List<EventMessage> activeGifts = PsMetagameManager.m_giftEvents.GetActiveGifts();
		int num = 0;
		while (activeGifts != null && num < activeGifts.Count)
		{
			if (activeGifts[num].giftContent.GetType() == typeof(T))
			{
				return activeGifts[num].giftContent as T;
			}
			num++;
		}
		return (T)((object)null);
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x0006A16F File Offset: 0x0006856F
	public static int GetSecondaryGhostCoinReward()
	{
		return 20;
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x0006A173 File Offset: 0x00068573
	public static int GetSecondaryGhostDiamondReward()
	{
		return 1;
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0006A178 File Offset: 0x00068578
	public static bool IsFreshLevelAvailable()
	{
		Debug.Log(string.Concat(new object[]
		{
			"FreshLimit: freshavailable?: fresh level count: ",
			PsMetagameManager.GetFreshLevelCount(),
			", fresh time out seconds: ",
			PsMetagameManager.GetFreshTimeOutSeconds()
		}), null);
		return PsMetagameManager.GetFreshLevelCount() > 0 || PsMetagameManager.GetFreshTimeOutSeconds() < 0;
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x0006A1D8 File Offset: 0x000685D8
	public static void FreshLevelIsCreated()
	{
		int freshLevelCount = PsMetagameManager.GetFreshLevelCount();
		if (PsMetagameManager.GetFreshTimeOutSeconds() < 0)
		{
			PsMetagameManager.SetFreshLevelCount(PlayerPrefsX.GetFreshStartCount() - 1);
		}
		else if (freshLevelCount > 0)
		{
			PsMetagameManager.SetFreshLevelCount(freshLevelCount - 1);
		}
		PsMetagameManager.SetFreshLevelTimeOut();
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x0006A21C File Offset: 0x0006861C
	public static int GetFreshLevelCount()
	{
		int num = PlayerPrefsX.GetFreshCount();
		if (num == -1)
		{
			num = PlayerPrefsX.GetFreshStartCount();
			PsMetagameManager.SetFreshLevelCount(num);
		}
		return num;
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0006A243 File Offset: 0x00068643
	public static void SetFreshLevelCount(int _count)
	{
		PlayerPrefsX.SetFreshCount(_count);
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0006A24C File Offset: 0x0006864C
	public static int GetFreshTimeOutSeconds()
	{
		string freshTimeOut = PlayerPrefsX.GetFreshTimeOut();
		int num;
		if (freshTimeOut != null)
		{
			num = Convert.ToInt32(DateTime.ParseExact(freshTimeOut, "O", CultureInfo.InvariantCulture).Subtract(DateTime.Now).TotalSeconds);
		}
		else
		{
			num = PlayerPrefsX.GetFreshStartTimeOut();
			PsMetagameManager.SetFreshLevelTimeOut();
		}
		return num;
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0006A2A4 File Offset: 0x000686A4
	public static void SetFreshLevelTimeOut()
	{
		int freshStartTimeOut = PlayerPrefsX.GetFreshStartTimeOut();
		PlayerPrefsX.SetFreshTimeOut(DateTime.Now.AddSeconds((double)freshStartTimeOut).ToString("O"));
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0006A2D8 File Offset: 0x000686D8
	public static void VersusChallengeRewards(bool _win)
	{
		if (_win)
		{
			if (PsMetagameManager.m_playerStats.mcRank >= PsState.m_versusRankCap)
			{
				if (PsMetagameManager.m_playerStats.cups < 3)
				{
					PsMetagameManager.m_playerStats.cups++;
				}
			}
			else
			{
				PsMetagameManager.m_playerStats.cups++;
				if (PsMetagameManager.m_playerStats.cups > 3)
				{
					PsMetagameManager.m_playerStats.mcRank++;
					PsMetagameManager.m_playerStats.cups -= 3;
				}
			}
		}
		else if (PsMetagameManager.m_playerStats.mcRank > 5)
		{
			PsMetagameManager.m_playerStats.cups--;
			if (PsMetagameManager.m_playerStats.cups < 0)
			{
				PsMetagameManager.m_playerStats.mcRank--;
				PsMetagameManager.m_playerStats.cups = 2;
			}
		}
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0006A3C0 File Offset: 0x000687C0
	public static void AddNewEditorItems(List<PsUnlockable> _unlockables)
	{
		string[] array = new string[0];
		if (_unlockables != null)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < _unlockables.Count; i++)
			{
				if (_unlockables[i].m_container.m_type == PsUnlockableType.Unit || _unlockables[i].m_container.m_type == PsUnlockableType.GameMaterial)
				{
					list.Add(_unlockables[i].m_identifier);
				}
			}
			array = list.ToArray();
		}
		string[] newItems = PlayerPrefsX.GetNewItems();
		string[] array2 = Enumerable.ToArray<string>(Enumerable.Union<string>(newItems, array));
		PlayerPrefsX.SetNewItems(array2);
		PsState.m_newEditorItems = array2;
		PsState.m_newEditorItemCount = array2.Length;
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0006A46C File Offset: 0x0006886C
	public static void RemoveNewEditorItem(string _identifier)
	{
		string[] newItems = PlayerPrefsX.GetNewItems();
		List<string> list = new List<string>();
		for (int i = 0; i < newItems.Length; i++)
		{
			if (newItems[i] != _identifier)
			{
				list.Add(newItems[i]);
			}
		}
		PlayerPrefsX.SetNewItems(list.ToArray());
		PsState.m_newEditorItems = list.ToArray();
		PsState.m_newEditorItemCount = list.Count;
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0006A4D4 File Offset: 0x000688D4
	public static void InitNewEditorItems()
	{
		string[] newItems = PlayerPrefsX.GetNewItems();
		PsState.m_newEditorItems = newItems;
		PsState.m_newEditorItemCount = newItems.Length;
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0006A4F8 File Offset: 0x000688F8
	public static void GiveGoalReward(int _stars, int _diamonds, PsGameDifficulty _difficulty)
	{
		int goalCoinReward = PsMetagameManager.GetGoalCoinReward(_stars, _difficulty);
		Debug.Log(string.Concat(new object[] { "REWARD FOR ", _stars, " STARS: ", goalCoinReward }), null);
		PsMetagameManager.UpdateResourceStats(goalCoinReward, _diamonds, _stars);
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0006A548 File Offset: 0x00068948
	public static int GetGoalCoinReward(int _score, PsGameDifficulty _difficulty)
	{
		int num = 1;
		return 5 * _score * num * Mathf.Min(PsMetagameManager.m_playerStats.level, 40);
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0006A570 File Offset: 0x00068970
	public static string NumberToString(int _amount)
	{
		double num = (double)_amount;
		string text = string.Empty;
		if (_amount > 999)
		{
			if (_amount > 999949)
			{
				if (_amount > 999949999)
				{
					num = Math.Round((double)((float)_amount / 1E+09f), 1);
					text = "B";
				}
				else
				{
					num = Math.Round((double)((float)_amount / 1000000f), 1);
					text = "M";
				}
			}
			else
			{
				num = Math.Round((double)((float)_amount / 1000f), 1);
				text = "K";
			}
		}
		return num + text;
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0006A604 File Offset: 0x00068A04
	public static float GetHoursFromSeconds(long _seconds, int _decimals = 0)
	{
		return (float)Math.Round((double)_seconds / 3600.0, _decimals);
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0006A630 File Offset: 0x00068A30
	public static int GetSecondsFromTimeString(string _timeString)
	{
		int num = Mathf.Max(0, _timeString.IndexOf("h"));
		int num2 = Mathf.Max(0, _timeString.IndexOf("m"));
		int num3 = Mathf.Max(0, _timeString.IndexOf("s"));
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		if (num > 0)
		{
			num4 = int.Parse(_timeString.Substring(0, num));
			num7 = 2;
		}
		if (num2 > 0)
		{
			num5 = int.Parse(_timeString.Substring(num + num7, num2 - (num + num7)));
			num7 = 2;
		}
		if (num3 > 0)
		{
			num6 = int.Parse(_timeString.Substring(num2 + num7, num3 - (num2 + num7)));
		}
		return num4 * 60 * 60 + num5 * 60 + num6;
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x0006A6E5 File Offset: 0x00068AE5
	public static string GetTimeStringFromSeconds(int _seconds)
	{
		return PsMetagameManager.GetTimeStringFromSeconds(_seconds, false, true);
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0006A6F0 File Offset: 0x00068AF0
	public static string GetTimeStringFromSeconds(int _seconds, bool _alwaysShowSeconds, bool _space)
	{
		string text = ((!_space) ? string.Empty : " ");
		int num = Mathf.FloorToInt((float)_seconds / 60f);
		if (num <= 0)
		{
			return _seconds + "s";
		}
		int num2 = _seconds - num * 60;
		int num3 = Mathf.FloorToInt((float)num / 60f);
		if (num3 <= 0)
		{
			return string.Concat(new object[] { num, "m", text, num2, "s" });
		}
		num -= num3 * 60;
		int num4 = Mathf.FloorToInt((float)num3 / 24f);
		if (num4 > 0)
		{
			num3 -= num4 * 24;
			if (_alwaysShowSeconds)
			{
				return string.Concat(new object[]
				{
					num4, "d", text, num3, "h", text, num, "m", text, num2,
					"s"
				});
			}
			return string.Concat(new object[] { num4, "d", text, num3, "h" });
		}
		else
		{
			if (_alwaysShowSeconds)
			{
				return string.Concat(new object[] { num3, "h", text, num, "m", text, num2, "s" });
			}
			return string.Concat(new object[] { num3, "h", text, num, "m" });
		}
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0006A8D0 File Offset: 0x00068CD0
	public static void UseGemsForVehicleRent(int _price, bool _sendToServer = true)
	{
		if (PsMetagameManager.m_playerStats.diamonds >= _price)
		{
			PsMetagameManager.m_playerStats.CumulateDiamonds(-_price);
			if (_sendToServer)
			{
				PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
			}
			Debug.Log("KEY USED ######################## left: " + PsMetagameManager.m_playerStats.mcBoosters, null);
		}
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0006A964 File Offset: 0x00068D64
	public static string GetKeyReloadTime()
	{
		int carRefreshMinutes = PlayerPrefsX.GetClientConfig().carRefreshMinutes;
		if (carRefreshMinutes > 0)
		{
			return carRefreshMinutes.ToString() + "m";
		}
		return "6m";
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0006A9A0 File Offset: 0x00068DA0
	public static int GetKeyReloadPrice()
	{
		int num = PsMetagameManager.m_keyReloadTimeLeft + PsMetagameManager.m_keyReloadTimeSeconds * (PsMetagameManager.m_playerStats.maxMcBoosters - 1);
		return PsMetagameManager.SecondsToDiamonds(num);
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0006A9CC File Offset: 0x00068DCC
	public static int GetKeyIncreasePrice(int _newMax)
	{
		return (_newMax - PlayerPrefsX.GetClientConfig().keysAtStart) * 40 + 40;
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0006A9E0 File Offset: 0x00068DE0
	public static bool PurchaseRefillBoosters()
	{
		int boosterRefillPrice = PsMetagameManager.GetBoosterRefillPrice();
		if (PsMetagameManager.m_playerStats.diamonds >= boosterRefillPrice)
		{
			Hashtable hashtable = new Hashtable();
			int num = PsMetagameManager.m_playerStats.maxBoosters - PsMetagameManager.m_playerStats.boosters;
			PsMetagameManager.m_playerStats.CumulateDiamonds(-boosterRefillPrice);
			PsMetagameManager.m_playerStats.CumulateBoosters(num);
			PsMetagameManager.SetPlayerData(hashtable, false, new Action<HttpC>(PsMetagameManager.PurchaseKeysSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
			FrbMetrics.SpendVirtualCurrency("race_booster_refill", "gems", (double)boosterRefillPrice);
			return true;
		}
		return false;
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0006AA8C File Offset: 0x00068E8C
	public static bool AddBoosters(int _amount)
	{
		Hashtable hashtable = new Hashtable();
		PsMetagameManager.m_playerStats.CumulateBoosters(_amount);
		PsMetagameManager.SetPlayerData(hashtable, false, new Action<HttpC>(PsMetagameManager.PurchaseKeysSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
		return true;
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0006AAED File Offset: 0x00068EED
	public static int GetBoosterRefillPrice()
	{
		return (PsMetagameManager.m_playerStats.maxBoosters - PsMetagameManager.m_playerStats.boosters) * 2;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0006AB08 File Offset: 0x00068F08
	public static bool PurchaseStars(int _stars, bool _sendToServer = true)
	{
		if (PsMetagameManager.m_playerStats.diamonds >= PsMetagameManager.StarsToDiamonds(_stars))
		{
			PsMetagameManager.m_playerStats.CumulateDiamonds(-PsMetagameManager.StarsToDiamonds(_stars));
			if (_sendToServer)
			{
				PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0006AB8C File Offset: 0x00068F8C
	public static void ClaimMinigameReward(PsMinigameMetaData _metaData, Vector2 _buttonPos)
	{
		Debug.Log("CLAIMING MINIGAME REWARD", null);
		PsMetagameManager.m_playerStats.CumulateCoinsWithFlyingResources(_metaData.rewardCoins, _buttonPos, 0f);
		FrbMetrics.ReceiveVirtualCurrency("coins", (double)_metaData.rewardCoins, "level_likes_reward");
		new PsServerQueueFlow(null, delegate
		{
			PsServerRequest.ServerClaimMinigame(_metaData, new Action<HttpC>(PsMetagameManager.ClaimMinigameSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimMinigameFAILED), null);
		}, new string[] { "SetData" });
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x0006AC08 File Offset: 0x00069008
	public static void ClaimAllMinigames(int _amount, Vector2 _buttonPos)
	{
		Debug.Log("CLAIMING ALL MINIGAMES", null);
		PsMetagameManager.m_playerStats.CumulateCoinsWithFlyingResources(_amount, _buttonPos, 0f);
		new PsServerQueueFlow(null, delegate
		{
			PsServerRequest.ServerClaimAllMinigames(new Action<HttpC>(PsMetagameManager.ClaimAllMinigamesSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimAllMinigamesFAILED), null);
		}, new string[] { "SetData" });
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0006AC63 File Offset: 0x00069063
	public static int SecondsToDiamonds(int timeInSeconds)
	{
		return Mathf.CeilToInt((float)timeInSeconds / 60f * PsMetagameManager.m_timerPriceGemsPerMinute);
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0006AC80 File Offset: 0x00069080
	public static int CoinsToDiamonds(int _coinAmount, bool _directConversion = true)
	{
		float num = ((!_directConversion) ? 1f : 1.2f);
		ObscuredInt[] array = new ObscuredInt[] { 10, 1000, 10000, 100000, 1000000, 10000000 };
		ObscuredInt[] array2 = new ObscuredInt[] { 1, 20, 180, 1600, 16000, 160000 };
		if (_coinAmount <= 0)
		{
			return 0;
		}
		if (_coinAmount <= array[0])
		{
			return array2[0];
		}
		for (int i = 1; i < array.Length - 1; i++)
		{
			if (_coinAmount <= array[i])
			{
				return (int)(num * ((float)(_coinAmount - array[i - 1]) / ((float)(array[i] - array[i - 1]) / (float)(array2[i] - array2[i - 1])) + (float)array2[i - 1]));
			}
		}
		return (int)(num * ((float)(_coinAmount - array[array.Length - 2]) / ((float)(array[array.Length - 1] - array[array.Length - 2]) / (float)(array2[array2.Length - 1] - array2[array2.Length - 2])) + (float)array2[array2.Length - 2]));
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0006AF18 File Offset: 0x00069318
	public static int StarsToDiamonds(int _starAmount)
	{
		int[] array = new int[] { 1, 3, 6, 10, 20, 60, 200 };
		int[] array2 = new int[] { 25, 60, 100, 140, 250, 600, 1000 };
		if (_starAmount <= 0)
		{
			return 0;
		}
		if (_starAmount <= array[0])
		{
			return array2[0];
		}
		for (int i = 1; i < array.Length - 1; i++)
		{
			if (_starAmount <= array[i])
			{
				return (int)((float)(_starAmount - array[i - 1]) / ((float)(array[i] - array[i - 1]) / (float)(array2[i] - array2[i - 1])) + (float)array2[i - 1]);
			}
		}
		return (int)((float)(_starAmount - array[array.Length - 2]) / ((float)(array[array.Length - 1] - array[array.Length - 2]) / (float)(array2[array2.Length - 1] - array2[array2.Length - 2])) + (float)array2[array2.Length - 2]);
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0006AFDC File Offset: 0x000693DC
	public static void PurchaseConsumable(string _identifier, Action<string> _okCallback, Action<string, string, bool> _errorCallback)
	{
		string text = string.Empty;
		text = "Android";
		PsMetagameManager.IAPDebug("IAPDebug_Start", new KeyValuePair<string, object>[]
		{
			new KeyValuePair<string, object>("clientVersion", Main.m_currentVersion),
			new KeyValuePair<string, object>("identifier", _identifier),
			new KeyValuePair<string, object>("platform", text)
		});
		Debug.Log("IAP: Start purchasing " + _identifier, null);
		new PsServerQueueFlow(null, delegate
		{
			PsIAPManager.PurchaseConsumable(_identifier, delegate(string id, bool result, string error, string transactionId)
			{
				PsMetagameManager.FinishPurchase(id, result, error, transactionId, _okCallback, _errorCallback);
			});
		}, new string[] { "SetData" });
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0006B0AC File Offset: 0x000694AC
	public static void PurchaseNonConsumable(string _identifier, Action<string> _okCallback, Action<string, string, bool> _errorCallback)
	{
		Debug.Log("IAP: Start purchasing non consumable " + _identifier, null);
		new PsServerQueueFlow(null, delegate
		{
			PsIAPManager.PurchaseNonConsumable(_identifier, delegate(string id, bool result, string error, string transactionId)
			{
				PsMetagameManager.FinishPurchase(id, result, error, transactionId, _okCallback, _errorCallback);
			});
		}, new string[] { "SetData" });
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0006B10C File Offset: 0x0006950C
	public static void RetryPendingPurchases()
	{
		string text = string.Empty;
		text = "Android";
		PsMetagameManager.IAPDebug("IAPDebug_StartRetry", 3, new KeyValuePair<string, object>[]
		{
			new KeyValuePair<string, object>("clientVersion", Main.m_currentVersion),
			new KeyValuePair<string, object>("platform", text)
		});
		Debug.Log("IAP: Check & retry unfinished purchases", null);
		PsIAPManager.RetryPendingPurchases(delegate(string id, bool result, string error, string transactionId)
		{
			PsMetagameManager.FinishPurchase(id, result, error, transactionId, delegate(string s)
			{
				PsMetagameManager.PurchaseRestoreSucceed(s, id);
			}, new Action<string, string, bool>(PsMetagameManager.PurchaseRestoreError));
		});
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0006B19C File Offset: 0x0006959C
	public static void PurchaseRestoreSucceed(string _m, string _identifier)
	{
		Debug.Log("PurchaseSucceed " + _m + ", identifier: " + _identifier, null);
		if (PsIAPManager.unfinishedPurchases != null)
		{
			PsIAPManager.unfinishedPurchases.Remove(PsIAPManager.unfinishedPurchases.Find((PsIAPManager.UnfinishedPurchase obj) => obj.productId == _identifier));
		}
		if (ServerManager.m_httpcErrorList.Count == 0)
		{
			HttpS.m_gatesFreeze = false;
		}
		PsState.m_inIapFlow = false;
		PsMetagameManager.IAPDebug("IAPDebug_RetryCompletedSucceed", 2, new KeyValuePair<string, object>[0]);
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0006B22C File Offset: 0x0006962C
	public static void PurchaseRestoreError(string _e, string _id, bool _result)
	{
		Debug.Log(string.Concat(new object[] { "PurchaseError ", _e, " ", _id, " ", _result }), null);
		if (ServerManager.m_httpcErrorList.Count == 0)
		{
			HttpS.m_gatesFreeze = false;
		}
		PsState.m_inIapFlow = false;
		PsMetagameManager.IAPDebug("IAPDebug_RetryCompletedError", 2, new KeyValuePair<string, object>[]
		{
			new KeyValuePair<string, object>("error", _e)
		});
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0006B2B8 File Offset: 0x000696B8
	public static void FinishPurchase(string _identifier, bool _result, string _error, string _transactionId, Action<string> _okCallback, Action<string, string, bool> _errorCallback)
	{
		PsMetagameManager.IAPDebug("IAPDebug_FinishPurchase", 2, new KeyValuePair<string, object>[0]);
		Debug.Log("E_Test FinishPurchase", null);
		if (_result && !string.IsNullOrEmpty(_identifier))
		{
			Debug.Log("IAP: Finishing purchase of " + _identifier, null);
			ServerProduct serverProductById = PsIAPManager.GetServerProductById(_identifier);
			IAPProduct iapproductById = PsIAPManager.GetIAPProductById(_identifier);
			if (serverProductById != null)
			{
				if (PsMetagameManager.m_menuResourceView != null && PsMetagameManager.m_menuResourceView.m_diamonds != null && Convert.ToInt32(PsMetagameManager.m_menuResourceView.m_diamonds.m_text) != PsMetagameManager.m_playerStats.diamonds)
				{
					PsMetagameManager.ShowFlyingDiamonds(serverProductById.amount);
				}
				PsMetrics.TrackRealMoneyPurchase(_identifier, serverProductById.amount, _transactionId);
				FrbMetrics.ReceiveVirtualCurrency("gems", (double)serverProductById.amount, "iap_" + _identifier);
				if (iapproductById != null)
				{
					Debug.Log("IAP: Send revenue event", null);
					PsMetrics.ValidatedPurchase(serverProductById.identifier, iapproductById.price, iapproductById.currencyCode, _transactionId);
				}
				_okCallback.Invoke("Purchase successful");
			}
			else
			{
				_errorCallback.Invoke("Product not found", null, _result);
			}
		}
		else
		{
			_errorCallback.Invoke(_error, _identifier, _result);
		}
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0006B3DE File Offset: 0x000697DE
	private static void ShowFlyingDiamonds(int _amount)
	{
		if (PsMetagameManager.m_menuResourceView != null && PsMetagameManager.m_menuResourceView.m_showDiamonds)
		{
			PsMetagameManager.m_menuResourceView.CreateAddedResources(ResourceType.Diamonds, _amount, 0f);
		}
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x0006B40C File Offset: 0x0006980C
	public static bool ShouldShowRateAppDialogue(PsGameLoop _gameLoop)
	{
		int ratingStatus = PlayerPrefsX.GetRatingStatus();
		if (ratingStatus > PsMetagameManager.m_rateRemindTimes || ratingStatus < 0 || PsMetagameManager.m_rateAlertShownInThisSession)
		{
			return false;
		}
		int currentNodeId = _gameLoop.m_path.m_currentNodeId;
		return _gameLoop != null && currentNodeId >= PsMetagameManager.m_rateAppNodeID && ((_gameLoop.m_scoreBest == 3 && _gameLoop.m_scoreOld < 3) || (_gameLoop is PsGameLoopRacing && (_gameLoop as PsGameLoopRacing).GetPosition() == 1));
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0006B494 File Offset: 0x00069894
	public static List<string> GetFollowRewards(string _playerIdOrFacebookId)
	{
		if (PsMetagameManager.m_followRewardData != null)
		{
			for (int i = 0; i < PsMetagameManager.m_followRewardData.Count; i++)
			{
				if ((!string.IsNullOrEmpty(PsMetagameManager.m_followRewardData[i].playerID) && PsMetagameManager.m_followRewardData[i].playerID == _playerIdOrFacebookId) || (!string.IsNullOrEmpty(PsMetagameManager.m_followRewardData[i].facebookID) && PsMetagameManager.m_followRewardData[i].facebookID == _playerIdOrFacebookId))
				{
					return PsMetagameManager.m_followRewardData[i].rewardIdentifiers;
				}
			}
		}
		return new List<string>();
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x0006B558 File Offset: 0x00069958
	public static void ShowRateAppDialogue(Action _exitCallBack)
	{
		int rateStatus = PlayerPrefsX.GetRatingStatus();
		Action action = delegate
		{
			int num = rateStatus + 1;
			PlayerPrefsX.SetRatingStatus(num);
			PsMetagameManager.m_ratingStatusChanged = true;
		};
		Action action2 = delegate
		{
			PlayerPrefsX.SetRatingStatus(-1);
			PsMetagameManager.m_ratingStatusChanged = true;
		};
		new PsMetagameManager.RateThisGameFlow(action2, action, _exitCallBack);
		PsMetagameManager.m_rateAlertShownInThisSession = true;
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x0006B5B0 File Offset: 0x000699B0
	public static void UpdateResourceStats(int _coins, int _diamonds, int _stars)
	{
		PsMetagameManager.m_playerStats.coins += _coins;
		PsMetagameManager.m_playerStats.diamonds += _diamonds;
		PsMetagameManager.m_playerStats.stars += _stars;
		PsMetagameManager.m_playerStats.updated = true;
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0006B600 File Offset: 0x00069A00
	public static void SendRating(PsRating _newRating, PsGameLoop _minigameInfo, Minigame _minigame, bool _skipped = false)
	{
		Hashtable hashtable = new Hashtable();
		if (_newRating != _minigameInfo.GetRating())
		{
			_minigameInfo.SetRating(_newRating);
			PsMetagameManager.SaveRatingData saveRatingData = new PsMetagameManager.SaveRatingData();
			saveRatingData.m_rating = _newRating;
			saveRatingData.m_minigameId = _minigameInfo.GetGameId();
			saveRatingData.m_minigameName = _minigameInfo.GetName();
			saveRatingData.m_minigameContext = _minigameInfo.m_context.ToString();
			saveRatingData.m_minigameGameMode = _minigameInfo.GetGameMode().ToString();
			saveRatingData.m_playerReachedGoal = _minigame.m_playerReachedGoal;
			saveRatingData.m_playerUnitName = _minigame.m_playerUnitName;
			saveRatingData.m_playerLevel = PsMetagameManager.m_playerStats.level;
			saveRatingData.m_playerSkipped = _skipped;
			saveRatingData.m_isFresh = _minigameInfo is PsGameLoopFresh;
			if (_minigameInfo.m_gameMode is PsGameModeRace || _minigameInfo.m_gameMode is PsGameModeRacing)
			{
				int num = _minigameInfo.GetPosition();
				int raceGhostCount = _minigameInfo.m_raceGhostCount;
				if (num == 0)
				{
					num = raceGhostCount + 1;
				}
				int num2 = raceGhostCount - (num - 1);
				saveRatingData.m_ghostCount = raceGhostCount;
				saveRatingData.m_ghostsWon = num2;
			}
			if (hashtable.Count > 0)
			{
				saveRatingData.setData = hashtable;
			}
			PsMetrics.LevelRated(saveRatingData);
			PsServerRequest.ServerSendRating(saveRatingData, new Action<HttpC>(PsMetagameManager.SendRatingSUCCEED), new Action<HttpC>(PsMetagameManager.SendRatingFAILED), null);
			Debug.Log("SENDING RATING: " + _newRating.ToString(), null);
		}
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0006B784 File Offset: 0x00069B84
	public static void SetPlayerData(Hashtable _data, bool _sendMetrics, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		PsMetagameManager.SetPlayerDataAndProgression(_data, null, _sendMetrics, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0006B794 File Offset: 0x00069B94
	public static void SetPlayerDataAndProgression(Hashtable _data, Hashtable _progressionJson, string _planet, bool _sendMetrics)
	{
		PsMetagameManager.SetPlayerDataAndProgression(_data, _progressionJson, _sendMetrics, delegate(HttpC c)
		{
			PsMetagameManager.PlayerDataAndProgressionSetSUCCEED(c, _planet);
		}, delegate(HttpC c)
		{
			PsMetagameManager.PlayerDataAndProgressionSetFAILED(c, _planet);
		}, null);
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0006B7D0 File Offset: 0x00069BD0
	public static void SetPlayerDataAndProgression(Hashtable _data, Hashtable _progressionJson, bool _sendMetrics, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		new PsServerQueueFlow(null, delegate
		{
			PsServerRequest.ServerPlayerSetDataAndProgression(_data, _progressionJson, PsCustomisationManager.GetUpdatedData(PsUpgradeManager.GetUpdatedData(null)), PsGachaManager.GetUpdatedData(), PsMetagameManager.m_playerStats.GetUpdatedEditorResources(), PsAchievementManager.GetUpdatedAchievements(true), _sendMetrics, _okCallback, _failCallback, _errorCallback);
		}, new string[] { "SetData" });
		PsMetagameManager.m_playerStats.SetDirty(false);
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0006B83C File Offset: 0x00069C3C
	public static void SetPlayer(PlayerData _player)
	{
		PsMetagameManager.m_playerStats.cheater = _player.cheater;
		PlayerPrefsX.SetUserId(_player.playerId);
		PlayerPrefsX.SetCountryCode(_player.countryCode);
		PlayerPrefsX.SetClientConfig(_player.clientConfig);
		PlayerPrefsX.SetTeamId(_player.teamId);
		PlayerPrefsX.SetTeamName(_player.teamName);
		PlayerPrefsX.SetTeamRole(_player.teamRole);
		PlayerPrefsX.SetTeamJoined(_player.hasJoinedTeam);
		PlayerPrefsX.SetYoutubeName(_player.youtubeName);
		PlayerPrefsX.SetYoutubeId(_player.youtubeId);
		PsMetagameManager.m_playerStats.youtubeSubscriberCount = _player.youtubeSubscriberCount;
		PsMetagameManager.m_playerStats.m_teamKickReason = _player.teamKickReason;
		PsMetagameManager.m_playerStats.coins = _player.coins;
		PsMetagameManager.m_playerStats.copper = _player.copper;
		PsMetagameManager.m_playerStats.diamonds = _player.diamonds;
		PsMetagameManager.m_playerStats.shards = _player.shards;
		PsMetagameManager.m_playerStats.stars = _player.stars;
		PsMetagameManager.m_playerStats.bigBangPoints = _player.bigBangPoints;
		PsMetagameManager.m_playerStats.adventureLevels = _player.adventureLevelsCompleted;
		PsMetagameManager.m_playerStats.racesCompleted = _player.racesWon;
		PsMetagameManager.m_playerStats.newLevelsRated = _player.newLevelsRated;
		PsMetagameManager.m_playerStats.levelsMade = _player.publishedMinigameCount;
		PsMetagameManager.m_playerStats.likesEarned = _player.totalLikes;
		PsMetagameManager.m_playerStats.megaLikesEarned = _player.totalSuperLikes;
		PsMetagameManager.m_playerStats.m_lastSeasonMcTrophies = _player.lastSeasonEndMcTrophies;
		PsMetagameManager.m_playerStats.m_lastSeasonCarTrophies = _player.lastSeasonEndCarTrophies;
		PsMetagameManager.m_playerStats.followerCount = _player.followerCount;
		PsMetagameManager.m_playerStats.racesThisSeason = _player.racesThisSeason;
		PsMetagameManager.m_playerStats.m_superLikeRefreshEnd = _player.superLikeRefreshTimeLeft;
		PsMetagameManager.m_playerStats.fbClaimed = _player.fbClaimed;
		PsMetagameManager.m_playerStats.igClaimed = _player.igClaimed;
		PsMetagameManager.m_playerStats.forumClaimed = _player.forumClaimed;
		PsMetagameManager.m_playerStats.mcBoosters = _player.mcBoosters;
		PsMetagameManager.m_playerStats.carBoosters = _player.carBoosters;
		PsMetagameManager.m_playerStats.tournamentBoosters = _player.tournamentBoosters;
		PsMetagameManager.m_playerStats.itemLevel = _player.itemLevel;
		PsMetagameManager.m_playerStats.level = _player.level;
		PsMetagameManager.m_playerStats.cups = _player.cups;
		PsMetagameManager.m_playerStats.mcRank = _player.mcRank;
		PsMetagameManager.m_playerStats.mcTrophies = _player.mcTrophies;
		PsMetagameManager.m_playerStats.carTrophies = _player.carTrophies;
		PsMetagameManager.m_playerStats.carRank = _player.carRank;
		PsMetagameManager.m_playerStats.cardPurchases = _player.cardPurchases;
		PsMetagameManager.m_playerStats.gachaData = _player.gachaData;
		PsMetagameManager.m_playerStats.editorResources = _player.editorResources;
		PsMetagameManager.m_playerStats.xp = _player.xp;
		PsMetagameManager.m_playerStats.mcHandicap = _player.mcHandicap;
		PsMetagameManager.m_playerStats.carHandicap = _player.carHandicap;
		PsMetagameManager.m_playerStats.coinDoubler = _player.coinDoubler;
		PsMetagameManager.m_playerStats.dirtBikeBundle = _player.dirtBikeBundle;
		PsMetagameManager.m_playerStats.trailsPurchased = _player.trailsPurchased;
		PsMetagameManager.m_playerStats.hatsPurchased = _player.hatsPurchased;
		PsMetagameManager.m_playerStats.bundlesPurchased = _player.bundlesPurchased;
		PsMetagameManager.m_playerStats.pendingSpecialOfferChests = _player.pendingSpecialOfferChests;
		PsMetagameManager.m_playerStats.completedSurvey = _player.completedSurvey;
		PsMetagameManager.m_playerStats.ageGroup = _player.ageGroup;
		PsMetagameManager.m_playerStats.gender = _player.gender;
		if (_player.upgrades != null)
		{
			PsMetagameManager.m_playerStats.upgrades = _player.upgrades;
		}
		if (_player.data != null)
		{
			PsMetagamePlayerData.m_playerData = _player.data;
		}
		if (_player.claimedTutorials != null)
		{
			PsMetagameData.m_unlockedTutorials = _player.claimedTutorials;
		}
		if (_player.clientConfig != null)
		{
			PsMetagameManager.m_specialOfferCooldownMinutes = _player.clientConfig.offerCooldownMinutes;
			PsMetagameManager.m_specialOfferDurationMinutes = _player.clientConfig.offerDurationMinutes;
		}
		PsMetagameManager.m_playerStats.updated = true;
		PsMetagameManager.m_gachasInitialized = false;
		PsMetagameManager.m_shopInitialized = false;
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0006BC90 File Offset: 0x0006A090
	public static void SendQuit(PsGameLoop _minigameInfo)
	{
		SendQuitData sendQuitData = new SendQuitData();
		sendQuitData.startCount = PsState.m_activeMinigame.m_gameStartCount;
		if (sendQuitData.startCount < 1)
		{
			return;
		}
		sendQuitData.gameLoop = _minigameInfo;
		bool flag = _minigameInfo.m_context == PsMinigameContext.Level;
		PsServerRequest.ServerSendQuit(sendQuitData, new Action<HttpC>(PsMetagameManager.SendQuitSUCCEED), new Action<HttpC>(PsMetagameManager.SendQuitFAILED), null, flag);
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0006BD1E File Offset: 0x0006A11E
	public static HttpC CreateNewTeam(TeamData _team, Action<TeamData> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerCreateNewTeam(_team, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0006BD29 File Offset: 0x0006A129
	public static HttpC SaveTeam(TeamData _team, Action<TeamData> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerSaveTeam(_team, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0006BD34 File Offset: 0x0006A134
	public static HttpC GetTeam(string _teamId, Action<TeamData> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerGetTeam(_teamId, false, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0006BD40 File Offset: 0x0006A140
	public static HttpC GetTeamSuggestions(Action<TeamData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerGetTeamSuggestions(_okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0006BD4A File Offset: 0x0006A14A
	public static HttpC GetTeamLeaderboards(Action<TeamData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerGetTeamLeaderboard(_okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0006BD54 File Offset: 0x0006A154
	public static HttpC SearchTeams(string _query, Action<TeamData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerTeamSearch(_query, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0006BD5F File Offset: 0x0006A15F
	public static HttpC JoinTeam(string _teamId, Action<TeamData> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerJoinTeam(_teamId, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0006BD6A File Offset: 0x0006A16A
	public static HttpC LeaveTeam(string _teamId, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerLeaveTeam(_teamId, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0006BD75 File Offset: 0x0006A175
	public static HttpC ClaimTeamKick(string _teamKickReason, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerClaimTeamKick(_teamKickReason, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0006BD80 File Offset: 0x0006A180
	public static HttpC ClaimEventMessage(int _id, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerClaimEventMessage(_id, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0006BD8B File Offset: 0x0006A18B
	public static HttpC ClaimPatchNote(int _id, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerClaimPathNote(_id, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0006BD98 File Offset: 0x0006A198
	public static void ClaimGift(int _id, Hashtable _setData, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		Event.GiftClaimData data = new Event.GiftClaimData();
		data.id = _id;
		PsMetagameManager.m_giftEvents.lastClaimedGift = _id;
		data.setData = _setData;
		if (PsMetagameManager.m_giftEvents != null && PsMetagameManager.m_giftEvents.lastClaimedGift > _id)
		{
			PsMetagameManager.m_giftEvents.lastClaimedGift = _id;
		}
		new PsServerQueueFlow(null, delegate
		{
			PsServerRequest.ServerClaimGift(data, _okCallback, _failCallback, _errorCallback);
		}, new string[] { "SetData" });
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0006BE38 File Offset: 0x0006A238
	public static Hashtable GetChestResourceHashtable(Hashtable _setData, Hashtable _pathJson)
	{
		Hashtable hashtable = new Hashtable();
		List<string> list = new List<string>();
		Hashtable hashtable2 = ClientTools.GeneratePlayerSetData(_setData, ref list);
		hashtable.Add("update", hashtable2);
		Hashtable updatedData = PsCustomisationManager.GetUpdatedData(PsUpgradeManager.GetUpdatedData(null));
		List<Dictionary<string, object>> updatedAchievements = PsAchievementManager.GetUpdatedAchievements(true);
		List<Hashtable> updatedData2 = PsGachaManager.GetUpdatedData();
		Dictionary<string, int> updatedEditorResources = PsMetagameManager.m_playerStats.GetUpdatedEditorResources();
		if (_pathJson != null)
		{
			hashtable.Add("progression", _pathJson);
		}
		if (updatedData != null)
		{
			hashtable.Add("customisation", updatedData);
		}
		if (updatedAchievements != null)
		{
			hashtable.Add("achievements", updatedAchievements);
		}
		if (updatedData2 != null)
		{
			hashtable.Add("chest", updatedData2);
		}
		if (updatedEditorResources != null)
		{
			hashtable.Add("editorResources", updatedEditorResources);
		}
		return hashtable;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x0006BEF0 File Offset: 0x0006A2F0
	public static void OpenChest(int _id, GachaType _chestType, Hashtable _setData, Hashtable _pathJson, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		Hashtable chestResourceHashtable = PsMetagameManager.GetChestResourceHashtable(_setData, _pathJson);
		Player.ChestOpenData chestData = new Player.ChestOpenData();
		chestData.id = _id;
		chestData.gachaType = _chestType.ToString();
		chestData.setData = chestResourceHashtable;
		new PsServerQueueFlow(null, delegate
		{
			PsServerRequest.ServerOpenChest(chestData, _okCallback, _failCallback, _errorCallback);
		}, new string[] { "SetData" });
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0006BF7F File Offset: 0x0006A37F
	public static HttpC GetYoutubeChannels(string _name, Action<List<YoutubeChannelInfo>> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerGetYoutubeChannels(_name, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0006BF8A File Offset: 0x0006A38A
	public static HttpC GetYoutubeChannelById(string _id, Action<List<YoutubeChannelInfo>> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerGetYoutubeChannelById(_id, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x0006BF95 File Offset: 0x0006A395
	public static HttpC ChangeYoutuber(string _name, string _id, int _subscribers, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerChangeYoutuber(_name, _id, _subscribers, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0006BFA4 File Offset: 0x0006A3A4
	public static void GiveLevelTemplate(PsLevelTemplate _template, Action<PsMinigameMetaData> _succeedCallback = null)
	{
		Random.seed = DateTime.Now.Millisecond;
		int num = Mathf.Clamp(Random.Range(0, _template.m_templateMinigames.Count), 0, _template.m_templateMinigames.Count - 1);
		string text = _template.m_templateMinigames[num];
		Hashtable hashtable = new Hashtable();
		hashtable.Add("researchIdentifier", _template.m_identifier);
		PsGameMode psGameMode = _template.m_gameMode;
		if (_template.m_gameMode == PsGameMode.Any)
		{
			psGameMode = ((Random.value < 0.5f) ? PsGameMode.StarCollect : PsGameMode.Race);
		}
		hashtable.Add("name", Minigame.GetRandomName());
		hashtable.Add("description", Minigame.GetDefaultDescription(psGameMode));
		hashtable.Add("gameMode", psGameMode);
		if (_template.m_playerUnit != "Any")
		{
			hashtable.Add("playerUnit", _template.m_playerUnit);
		}
		if (_template.m_limitedItems != null && _template.m_limitedItems.Count > 0)
		{
			hashtable.Add("itemsUsed", _template.m_limitedItems.ToArray());
		}
		Debug.LogWarning("dublicating template level: " + text);
		if (_succeedCallback == null)
		{
			_succeedCallback = new Action<PsMinigameMetaData>(PsMetagameManager.GiveLevelTemplateSUCCEED);
		}
		PsServerRequest.GiveLevelTemplate(text, hashtable, _succeedCallback, new Action<HttpC>(PsMetagameManager.GiveLevelTemplateFAILED), null);
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0006C11F File Offset: 0x0006A51F
	public static bool IsFriend(string _playerId)
	{
		return PsMetagameManager.m_friends.IsFriend(_playerId);
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0006C12C File Offset: 0x0006A52C
	public static bool IsFollowee(string _playerId)
	{
		for (int i = 0; i < PsMetagameManager.m_friends.followees.Count; i++)
		{
			if (PsMetagameManager.m_friends.followees[i].playerId == _playerId)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0006C180 File Offset: 0x0006A580
	public static void GetFriends(Action<Friends> _callback, bool _forceRefresh = false)
	{
		if (!PsMetagameManager.m_loadingFriends && PsMetagameManager.m_friends != null && PsMetagameManager.m_friends.HasContacts() && !_forceRefresh)
		{
			Debug.Log("local friends", null);
			_callback.Invoke(PsMetagameManager.m_friends);
		}
		else
		{
			if (_callback != null)
			{
				if (PsMetagameManager.m_loadFriendsCallback != null)
				{
					PsMetagameManager.m_loadFriendsCallback = (Action<Friends>)Delegate.Combine(PsMetagameManager.m_loadFriendsCallback, _callback);
				}
				else
				{
					PsMetagameManager.m_loadFriendsCallback = _callback;
				}
			}
			if (!PsMetagameManager.m_loadingFriends)
			{
				PsMetagameManager.m_loadingFriends = true;
				Debug.Log("Getting friends from server", null);
				PsServerRequest.ServerGetFriends(PlayerPrefsX.GetUserId(), new Action<Friends>(PsMetagameManager.LoadFriendsSucceed), new Action<HttpC>(PsMetagameManager.LoadFriendsFailed), null);
			}
		}
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0006C264 File Offset: 0x0006A664
	public static void LoadFriendsSucceed(Friends _data)
	{
		PsMetagameManager.m_loadingFriends = false;
		PsMetagameManager.m_friends = _data;
		if (PsMetagameManager.m_loadFriendsCallback != null)
		{
			PsMetagameManager.m_loadFriendsCallback.Invoke(PsMetagameManager.m_friends);
			PsMetagameManager.m_loadFriendsCallback = null;
		}
		if (PsMetagameManager.m_followRewardData != null && PsMetagameManager.m_followRewardData.Count > 0)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < PsMetagameManager.m_friends.friendList.Count; i++)
			{
				list.AddRange(PsMetagameManager.GetFollowRewards(PsMetagameManager.m_friends.friendList[i].playerId));
				list.AddRange(PsMetagameManager.GetFollowRewards(PsMetagameManager.m_friends.friendList[i].facebookId));
			}
			for (int j = 0; j < PsMetagameManager.m_friends.followees.Count; j++)
			{
				list.AddRange(PsMetagameManager.GetFollowRewards(PsMetagameManager.m_friends.followees[j].playerId));
				list.AddRange(PsMetagameManager.GetFollowRewards(PsMetagameManager.m_friends.followees[j].facebookId));
			}
			if (list.Count > 0)
			{
				bool flag = false;
				for (int k = 0; k < list.Count; k++)
				{
					PsCustomisationItem itemByIdentifier = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(list[k]);
					if (itemByIdentifier != null && itemByIdentifier.m_category == PsCustomisationManager.CustomisationCategory.HAT && !itemByIdentifier.m_unlocked)
					{
						for (int l = 0; l < PsState.m_vehicleTypes.Length; l++)
						{
							PsCustomisationManager.UnlockItem(PsState.m_vehicleTypes[l], list[k]);
						}
						flag = true;
					}
				}
				if (flag)
				{
					PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
				}
			}
		}
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x0006C47C File Offset: 0x0006A87C
	public static void LoadFriendsFailed(HttpC _c)
	{
		PsServerRequest.ServerGetFriends(PlayerPrefsX.GetUserId(), new Action<Friends>(PsMetagameManager.LoadFriendsSucceed), new Action<HttpC>(PsMetagameManager.LoadFriendsFailed), null);
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0006C4D0 File Offset: 0x0006A8D0
	public static void GetOwnTeam(Action<TeamData> _callback, bool _forceRefresh = false)
	{
		if (!PsMetagameManager.m_loadingTeam && PsMetagameManager.m_team != null && !_forceRefresh)
		{
			Debug.Log("local team", null);
			_callback.Invoke(PsMetagameManager.m_team);
		}
		else
		{
			if (PsMetagameManager.m_loadTeamCallback != null)
			{
				PsMetagameManager.m_loadTeamCallback = (Action<TeamData>)Delegate.Combine(PsMetagameManager.m_loadTeamCallback, _callback);
			}
			else
			{
				PsMetagameManager.m_loadTeamCallback = _callback;
			}
			if (!PsMetagameManager.m_loadingTeam)
			{
				PsMetagameManager.m_loadingTeam = true;
				Debug.Log("Getting own team from server", null);
				PsServerRequest.ServerGetTeam(PlayerPrefsX.GetTeamId(), true, new Action<TeamData>(PsMetagameManager.LoadOwnTeamSucceed), new Action<HttpC>(PsMetagameManager.LoadOwnTeamFailed), null);
			}
		}
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0006C59E File Offset: 0x0006A99E
	public static void LoadOwnTeamSucceed(TeamData _data)
	{
		PsMetagameManager.m_loadingTeam = false;
		PsMetagameManager.m_team = _data;
		if (PsMetagameManager.m_loadTeamCallback != null)
		{
			PsMetagameManager.m_loadTeamCallback.Invoke(PsMetagameManager.m_team);
			PsMetagameManager.m_loadTeamCallback = null;
		}
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0006C5CC File Offset: 0x0006A9CC
	public static void LoadOwnTeamFailed(HttpC _c)
	{
		PsServerRequest.ServerGetTeam(PlayerPrefsX.GetTeamId(), true, new Action<TeamData>(PsMetagameManager.LoadOwnTeamSucceed), new Action<HttpC>(PsMetagameManager.LoadOwnTeamFailed), null);
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0006C620 File Offset: 0x0006AA20
	public static void GetFollowees(string _playerId)
	{
		PsServerRequest.ServerGetFollowees(_playerId, new Action<PlayerData[]>(PsMetagameManager.FolloweesSUCCEED), new Action<HttpC>(PsMetagameManager.FolloweesFAILED), null);
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0006C670 File Offset: 0x0006AA70
	public static void GetFollowers(string _playerId)
	{
		PsServerRequest.ServerGetFollowers(_playerId, new Action<PlayerData[]>(PsMetagameManager.FollowersSUCCEED), new Action<HttpC>(PsMetagameManager.FollowersFAILED), null);
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0006C6C0 File Offset: 0x0006AAC0
	public static List<PlayerData> GetFriends(List<PlayerData> _followees, List<PlayerData> _followers)
	{
		List<PlayerData> list = new List<PlayerData>();
		for (int i = _followers.Count - 1; i >= 0; i--)
		{
			for (int j = 0; j < _followees.Count; j++)
			{
				if (PsMetagameManager.m_followers[i].playerId == PsMetagameManager.m_followees[j].playerId)
				{
					list.Add(PsMetagameManager.m_followers[i]);
				}
			}
		}
		return list;
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0006C748 File Offset: 0x0006AB48
	public static HttpC Comment(string _gameId, string _comment, string _tag, string _tournamentId, Action<CommentData[]> _okCallback = null, Action<HttpC> _failCallback = null)
	{
		PsMetagameManager.CommentInfo commentInfo = default(PsMetagameManager.CommentInfo);
		commentInfo.gameId = _gameId;
		commentInfo.comment = _comment;
		commentInfo.tag = _tag;
		commentInfo.tournamentId = _tournamentId;
		if (_okCallback == null)
		{
			_okCallback = new Action<CommentData[]>(PsMetagameManager.CommentSUCCEED);
		}
		if (_failCallback == null)
		{
			_failCallback = new Action<HttpC>(PsMetagameManager.CommentFAILED);
		}
		return PsServerRequest.ServerComment(commentInfo, _okCallback, _failCallback, null);
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0006C7D4 File Offset: 0x0006ABD4
	public static HttpC GetComments(string _id, Action<CommentData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return PsServerRequest.ServerGetComments(_id, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0006C7DF File Offset: 0x0006ABDF
	public static void ClaimSeasonRewards()
	{
		new PsServerQueueFlow(null, delegate
		{
			PsServerRequest.ServerClaimSeasonRewards(new Action<HttpC>(PsMetagameManager.ClaimSeasonRewardsSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimSeasonRewardsFAILED), null);
		}, new string[] { "SetData" });
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x0006C814 File Offset: 0x0006AC14
	public static void UpsertAchievement(string _json)
	{
		PsServerRequest.ServerAchievement(_json, new Action<HttpC>(PsMetagameManager.AchievementUpsertSUCCEED), new Action<HttpC>(PsMetagameManager.AchievementUpsertFAILED), null);
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x0006C864 File Offset: 0x0006AC64
	public static void SkipLevel(int _diamondPrice, string _playerUnit)
	{
		PsMetagameManager.m_playerStats.CumulateDiamonds(-_diamondPrice);
		PsMetagameManager.m_playerStats.CumulateStars(1);
		PsServerRequest.ServerPlayerSkipLevel(_playerUnit, new Hashtable(), new Action<HttpC>(PsMetagameManager.PlayerSkipLevelSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerSkipLevelFAILED), null);
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0006C8D0 File Offset: 0x0006ACD0
	public static void SaveProgression(Hashtable _progressionJSON, string _planetIdentifier, bool _sendMetrics)
	{
		SendPathData data = new SendPathData();
		data.jsonData = _progressionJSON;
		data.sendMetrics = _sendMetrics;
		new PsServerQueueFlow(delegate
		{
			PsServerRequest.ServerSaveProgression(data, delegate(HttpC c)
			{
				PsMetagameManager.SaveProgressionSUCCEED(c, _planetIdentifier);
			}, delegate(HttpC c)
			{
				PsMetagameManager.SaveProgressionFAILED(c, _planetIdentifier);
			}, null);
		}, null, new string[] { "SetData" });
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0006C930 File Offset: 0x0006AD30
	public static void DeleteMinigame(string _minigameId, Dictionary<string, int> _returnEditorItems, Action<HttpC> _succeed = null)
	{
		MiniGame.DeleteMinigameData deleteMinigameData = new MiniGame.DeleteMinigameData();
		deleteMinigameData.minigameId = _minigameId;
		deleteMinigameData.editorResources = _returnEditorItems;
		if (_succeed == null)
		{
			PsServerRequest.ServerDeleteMinigame(deleteMinigameData, new Action<HttpC>(PsMetagameManager.DeleteMinigameSUCCEED), new Action<HttpC>(PsMetagameManager.DeleteMinigameFAILED), null);
		}
		else
		{
			PsServerRequest.ServerDeleteMinigame(deleteMinigameData, _succeed, new Action<HttpC>(PsMetagameManager.DeleteMinigameFAILED), null);
		}
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0006C9C4 File Offset: 0x0006ADC4
	public static void UpdatePlayerSettings()
	{
		PlayerUpdateSettings settings = default(PlayerUpdateSettings);
		settings.acceptNotifications = PlayerPrefsX.GetAcceptNotifications();
		settings.locale = PsStrings.GetLanguage().ToString().ToUpper();
		new PsServerQueueFlow(null, delegate
		{
			PsServerRequest.ServerUpdatePlayerSettings(settings, new Action<HttpC>(PsMetagameManager.UpdatePlayerSettingsSUCCEED), new Action<HttpC>(PsMetagameManager.UpdatePlayerSettingsFAILED), null);
		}, new string[] { "PlayerSettings" });
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0006CA3A File Offset: 0x0006AE3A
	public static void UpdatePlayerSettingsSUCCEED(HttpC _c)
	{
		Debug.LogWarning("UPDATE SETTINGS SUCCEED");
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0006CA48 File Offset: 0x0006AE48
	public static void UpdatePlayerSettingsFAILED(HttpC _c)
	{
		Debug.LogWarning("UPDATE SETTINGS FAILED");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerUpdatePlayerSettings((PlayerUpdateSettings)_c.objectData, new Action<HttpC>(PsMetagameManager.UpdatePlayerSettingsSUCCEED), new Action<HttpC>(PsMetagameManager.UpdatePlayerSettingsFAILED), null), null);
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0006CA93 File Offset: 0x0006AE93
	public static void GiveLevelTemplateSUCCEED(PsMinigameMetaData _metaData)
	{
		Debug.LogWarning("TEMPLATE DUPLICATE SUCCEED");
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0006CAA0 File Offset: 0x0006AEA0
	public static void GiveLevelTemplateFAILED(HttpC _c)
	{
		Debug.LogWarning("TEMPLATE DUPLICATE FAILED");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.GiveLevelTemplate((string)_c.objectData, new Hashtable(), new Action<PsMinigameMetaData>(PsMetagameManager.GiveLevelTemplateSUCCEED), new Action<HttpC>(PsMetagameManager.GiveLevelTemplateFAILED), null), null);
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0006CAEB File Offset: 0x0006AEEB
	public static void ClaimAllMinigamesSUCCEED(HttpC _c)
	{
		Debug.Log("CLAIM ALL MINIGAMES SUCCEED", null);
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0006CAF8 File Offset: 0x0006AEF8
	public static void ClaimAllMinigamesFAILED(HttpC _c)
	{
		Debug.Log("CLAIM ALL MINIGAMES FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerClaimAllMinigames(new Action<HttpC>(PsMetagameManager.ClaimAllMinigamesSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimAllMinigamesFAILED), null), null);
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0006CB38 File Offset: 0x0006AF38
	public static void DeleteMinigameSUCCEED(HttpC _c)
	{
		Debug.Log("DELETE MINIGAME SUCCEED", null);
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x0006CB48 File Offset: 0x0006AF48
	public static void DeleteMinigameFAILED(HttpC _c)
	{
		Debug.Log("DELETE MINIGAME FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerDeleteMinigame((MiniGame.DeleteMinigameData)_c.objectData, new Action<HttpC>(PsMetagameManager.DeleteMinigameSUCCEED), new Action<HttpC>(PsMetagameManager.DeleteMinigameFAILED), null), null);
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x0006CBAC File Offset: 0x0006AFAC
	public static void SaveProgressionSUCCEED(HttpC _c, string _planetIdentifier)
	{
		Debug.Log("PROGRESSION SAVE SUCCEED", null);
		if (((SendPathData)_c.objectData).sendMetrics)
		{
			PsMetrics.Progression(PlanetTools.m_planetProgressionInfos[_planetIdentifier].GetMainPath().m_currentNodeId, _planetIdentifier);
			if (PsPlanetManager.GetCurrentPlanet().GetMainPathCurrentNodeId() == 2 && !PsMetagameManager.m_firstLevelComplete)
			{
				PsMetagameManager.m_firstLevelComplete = true;
				PsMetrics.Tutorial(true);
				FrbMetrics.TutorialComplete();
			}
		}
		PsPlanetSerializer.SaveProgressionLocally(_planetIdentifier);
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0006CC28 File Offset: 0x0006B028
	public static void SaveProgressionFAILED(HttpC _c, string _planetIdentifier)
	{
		Debug.Log("PROGRESSION SAVE FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerSaveProgression((SendPathData)_c.objectData, delegate(HttpC c)
		{
			PsMetagameManager.SaveProgressionSUCCEED(c, _planetIdentifier);
		}, delegate(HttpC c)
		{
			PsMetagameManager.SaveProgressionFAILED(c, _planetIdentifier);
		}, null), null);
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0006CC7B File Offset: 0x0006B07B
	public static void CommentSUCCEED(CommentData[] _comments)
	{
		Debug.Log("COMMENT SUCCEED", null);
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0006CC88 File Offset: 0x0006B088
	public static void CommentFAILED(HttpC _c)
	{
		Debug.Log("COMMENT FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerComment((PsMetagameManager.CommentInfo)_c.objectData, new Action<CommentData[]>(PsMetagameManager.CommentSUCCEED), new Action<HttpC>(PsMetagameManager.CommentFAILED), null), null);
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x0006CCD4 File Offset: 0x0006B0D4
	public static void ShopPurchaseSUCCEED(HttpC _c)
	{
		Debug.Log("SHOP PURCHASE SUCCEED", null);
		if (PsMetagameManager.m_purchaseWaitPopup != null)
		{
			PsMetagameManager.m_purchaseWaitPopup.Close();
			PsMetagameManager.m_purchaseWaitPopup = null;
		}
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0006CCFC File Offset: 0x0006B0FC
	public static void ShopPurchaseFAILED(HttpC _c)
	{
		Debug.Log("SHOP PURCHASE FAILED", null);
		Hashtable setData = _c.objectData as Hashtable;
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerPlayerSetData(setData, new Action<HttpC>(PsMetagameManager.ShopPurchaseSUCCEED), new Action<HttpC>(PsMetagameManager.ShopPurchaseFAILED), null), null);
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0006CD50 File Offset: 0x0006B150
	public static void PlayerDataAndProgressionSetSUCCEED(HttpC _c, string _planet)
	{
		PsPlanetSerializer.SaveProgressionLocally(_planet);
		if (((Player.ProgressionAndSetDataObject)_c.objectData).sendMetrics)
		{
			PsMetrics.Progression(PlanetTools.m_planetProgressionInfos[_planet].GetMainPath().m_currentNodeId, _planet);
		}
		Debug.Log("PLAYER DATA & PROGRESSION SET SUCCEED", null);
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x0006CDA0 File Offset: 0x0006B1A0
	public static void PlayerDataAndProgressionSetFAILED(HttpC _c, string _planet)
	{
		Debug.Log("PLAYER DATA & PROGRESSION SET FAILED", null);
		Player.ProgressionAndSetDataObject data = _c.objectData as Player.ProgressionAndSetDataObject;
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerPlayerSetDataAndProgression(data.setData, data.pathJson, data.customisations, data.chests, data.editorResources, data.achievements, data.sendMetrics, delegate(HttpC c)
		{
			PsMetagameManager.PlayerDataAndProgressionSetSUCCEED(c, _planet);
		}, delegate(HttpC c)
		{
			PsMetagameManager.PlayerDataAndProgressionSetFAILED(c, _planet);
		}, null), null);
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x0006CDF8 File Offset: 0x0006B1F8
	public static void PlayerDataSetSUCCEED(HttpC _c)
	{
		Debug.Log("PLAYER DATA SET SUCCEED", null);
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0006CE08 File Offset: 0x0006B208
	public static void PlayerDataSetFAILED(HttpC _c)
	{
		Debug.Log("PLAYER DATA SET FAILED", null);
		Hashtable setData = _c.objectData as Hashtable;
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerPlayerSetData(setData, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null), null);
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0006CE59 File Offset: 0x0006B259
	private static void ParticipateTournamentSUCCEED(HttpC _c)
	{
		Debug.Log("PARTICIPATE TOURNAMENT SUCCEED", null);
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0006CE68 File Offset: 0x0006B268
	private static long GetTournamentIdHash(string _tournamentId)
	{
		long num = 0L;
		for (int i = 0; i < _tournamentId.Length; i++)
		{
			num += Convert.ToInt64(_tournamentId.get_Chars(i)) * 32L * (long)(i + 1);
		}
		return num;
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0006CEA8 File Offset: 0x0006B2A8
	private static void ClaimMinigameSUCCEED(HttpC _c)
	{
		Debug.Log("CLAIM MINIGAME SUCCEED", null);
		(_c.objectData as PsMinigameMetaData).rewardCoins = 0;
		int num = ((PlayerPrefsX.GetOwnLevelClaimCount() <= 0) ? 0 : (PlayerPrefsX.GetOwnLevelClaimCount() - 1));
		PlayerPrefsX.SetOwnLevelClaimCount(num);
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0006CEF0 File Offset: 0x0006B2F0
	private static void ClaimMinigameFAILED(HttpC _c)
	{
		Debug.Log("CLAIM MINIGAME FAILED", null);
		PsMinigameMetaData data = (PsMinigameMetaData)_c.objectData;
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerClaimMinigame(data, new Action<HttpC>(PsMetagameManager.ClaimMinigameSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimMinigameFAILED), null), null);
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0006CF41 File Offset: 0x0006B341
	private static void PurchaseKeysSUCCEED(HttpC _c)
	{
		Debug.Log("PURCHASE KEYS SUCCEED", null);
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0006CF4E File Offset: 0x0006B34E
	private static void FillKeysSUCCEED(HttpC _c)
	{
		Debug.Log("FILL KEYS SUCCEED", null);
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0006CF5B File Offset: 0x0006B35B
	private static void SendRatingSUCCEED(HttpC _c)
	{
		Debug.Log("SEND RATING SUCCEED", null);
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0006CF68 File Offset: 0x0006B368
	private static void SendRatingFAILED(HttpC _c)
	{
		Debug.Log("Send rating FAILED", null);
		PsMetagameManager.SaveRatingData data = (PsMetagameManager.SaveRatingData)_c.objectData;
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerSendRating(data, new Action<HttpC>(PsMetagameManager.SendRatingSUCCEED), new Action<HttpC>(PsMetagameManager.SendRatingFAILED), null), null);
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0006CFB9 File Offset: 0x0006B3B9
	private static void UseKeySUCCEED(HttpC _c)
	{
		PsMetricsData.m_testersUsed++;
		Debug.Log("USE KEY SUCCEED", null);
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0006CFD2 File Offset: 0x0006B3D2
	public static void GetScreenshotERROR()
	{
		Debug.LogWarning("SCREENSHOT NOT FOUND");
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0006CFDE File Offset: 0x0006B3DE
	public static void GetPreloadCheckERROR()
	{
		Debug.LogWarning("FILE NOT FOUND");
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0006CFEA File Offset: 0x0006B3EA
	public static void SendQuitSUCCEED(HttpC _c)
	{
		Debug.Log("SEND QUIT SUCCEED", null);
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0006CFF8 File Offset: 0x0006B3F8
	public static void SendQuitFAILED(HttpC _c)
	{
		Debug.Log("SEND QUIT FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerSendQuit(_c.objectData as SendQuitData, new Action<HttpC>(PsMetagameManager.SendQuitSUCCEED), new Action<HttpC>(PsMetagameManager.SendQuitFAILED), null, false), null);
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0006D044 File Offset: 0x0006B444
	public static void FolloweesSUCCEED(PlayerData[] _data)
	{
		Debug.Log("FOLLOWEES SUCCEED", null);
		PsMetagameManager.m_followees = new List<PlayerData>(_data);
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x0006D05C File Offset: 0x0006B45C
	public static void FolloweesFAILED(HttpC _c)
	{
		Debug.Log("FOLLOWEES FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerGetFollowees((string)_c.objectData, new Action<PlayerData[]>(PsMetagameManager.FolloweesSUCCEED), new Action<HttpC>(PsMetagameManager.FolloweesFAILED), null), null);
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0006D0A8 File Offset: 0x0006B4A8
	public static void FollowersSUCCEED(PlayerData[] _data)
	{
		Debug.Log("LOAD FOLLOWERS SUCCEED", null);
		PsMetagameManager.m_followers = new List<PlayerData>(_data);
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0006D0C0 File Offset: 0x0006B4C0
	public static void FollowersFAILED(HttpC _c)
	{
		Debug.Log("LOAD FOLLOWERS FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerGetFollowers((string)_c.objectData, new Action<PlayerData[]>(PsMetagameManager.FollowersSUCCEED), new Action<HttpC>(PsMetagameManager.FollowersFAILED), null), null);
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0006D10C File Offset: 0x0006B50C
	public static void UnFollowSUCCEED(HttpC _c)
	{
		Debug.Log("UNFOLLOW SUCCEED", null);
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0006D11C File Offset: 0x0006B51C
	public static void UnFollowFAILED(HttpC _c)
	{
		Debug.Log("UNFOLLOW FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerUnFollow((string)_c.objectData, new Action<HttpC>(PsMetagameManager.UnFollowSUCCEED), new Action<HttpC>(PsMetagameManager.UnFollowFAILED), null), null);
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0006D168 File Offset: 0x0006B568
	public static void PlayerSkipLevelSUCCEED(HttpC _c)
	{
		Debug.Log("SKIP LEVEL SUCCEED", null);
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x0006D178 File Offset: 0x0006B578
	public static void PlayerSkipLevelFAILED(HttpC _c)
	{
		Debug.Log("SKIP LEVEL FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerPlayerSkipLevel((string)_c.objectData, new Hashtable(), new Action<HttpC>(PsMetagameManager.PlayerSkipLevelSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerSkipLevelFAILED), null), null);
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x0006D1C4 File Offset: 0x0006B5C4
	public static void AchievementUpsertSUCCEED(HttpC _c)
	{
		Debug.Log("ACHIEVEMENT UPSERT SUCCEED", null);
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x0006D1D4 File Offset: 0x0006B5D4
	public static void AchievementUpsertFAILED(HttpC _c)
	{
		Debug.Log("ACHIEVEMENT UPSERT FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerAchievement((string)_c.objectData, new Action<HttpC>(PsMetagameManager.AchievementUpsertSUCCEED), new Action<HttpC>(PsMetagameManager.AchievementUpsertFAILED), null), null);
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x0006D220 File Offset: 0x0006B620
	public static void LeaveTeamSUCCEED(HttpC _c)
	{
		Debug.Log("LEAVE TEAM SUCCEED", null);
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x0006D230 File Offset: 0x0006B630
	public static void LeaveTeamFAILED(HttpC _c)
	{
		Debug.Log("LEAVE TEAM FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerLeaveTeam((string)_c.objectData, new Action<HttpC>(PsMetagameManager.LeaveTeamSUCCEED), new Action<HttpC>(PsMetagameManager.LeaveTeamFAILED), null), null);
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x0006D27C File Offset: 0x0006B67C
	public static void ClaimSeasonRewardsSUCCEED(HttpC _c)
	{
		Debug.Log("CLAIM SEASON SUCCEED", null);
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x0006D289 File Offset: 0x0006B689
	public static void ClaimSeasonRewardsFAILED(HttpC _c)
	{
		Debug.Log("CLAIM SEASON FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerClaimSeasonRewards(new Action<HttpC>(PsMetagameManager.ClaimSeasonRewardsSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimSeasonRewardsFAILED), null), null);
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x0006D2C9 File Offset: 0x0006B6C9
	public static void ClaimTeamKickSUCCEED(HttpC _c)
	{
		Debug.Log("CLAIM TEAM KICK SUCCEED", null);
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x0006D2D8 File Offset: 0x0006B6D8
	public static void ClaimTeamKickFAILED(HttpC _c)
	{
		Debug.Log("CLAIM TEAM KICK FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerClaimTeamKick((string)_c.objectData, new Action<HttpC>(PsMetagameManager.ClaimTeamKickSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimTeamKickFAILED), null), null);
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x0006D324 File Offset: 0x0006B724
	public static void ClaimEventMessageSUCCEED(HttpC _c)
	{
		Debug.Log("CLAIM EVENT MESSAGE SUCCEED", null);
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x0006D334 File Offset: 0x0006B734
	public static void ClaimEventMessageFAILED(HttpC _c)
	{
		Debug.Log("CLAIM EVENT MESSAGE FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerClaimEventMessage((int)_c.objectData, new Action<HttpC>(PsMetagameManager.ClaimEventMessageSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimEventMessageFAILED), null), null);
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x0006D380 File Offset: 0x0006B780
	public static void ClaimPatchNoteSUCCEED(HttpC _c)
	{
		Debug.Log("CLAIM PATCH NOTE SUCCEED", null);
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0006D390 File Offset: 0x0006B790
	public static void ClaimPatchNoteFAILED(HttpC _c)
	{
		Debug.Log("CLAIM PATCH NOTE FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerClaimPathNote((int)_c.objectData, new Action<HttpC>(PsMetagameManager.ClaimPatchNoteSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimPatchNoteFAILED), null), null);
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x0006D3DC File Offset: 0x0006B7DC
	public static void ClaimGiftSUCCEED(HttpC _c)
	{
		Debug.Log("CLAIM GIFT SUCCEED", null);
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0006D3EC File Offset: 0x0006B7EC
	public static void ClaimGiftFAILED(HttpC _c)
	{
		Debug.Log("CLAIM GIFT NOTE FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerClaimGift((Event.GiftClaimData)_c.objectData, new Action<HttpC>(PsMetagameManager.ClaimGiftSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimGiftFAILED), null), null);
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x0006D438 File Offset: 0x0006B838
	public static void OpenChestSUCCEED(HttpC _c)
	{
		Debug.Log("OPEN CHEST SUCCEED", null);
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x0006D448 File Offset: 0x0006B848
	public static void OpenChestFAILED(HttpC _c)
	{
		Debug.Log("OPEN CHEST FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerOpenChest((Player.ChestOpenData)_c.objectData, new Action<HttpC>(PsMetagameManager.OpenChestSUCCEED), new Action<HttpC>(PsMetagameManager.OpenChestFAILED), null), null);
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x0006D494 File Offset: 0x0006B894
	public static void ChangeYoutuberSUCCEED(HttpC _c)
	{
		Debug.Log("E_Test " + PlayerPrefsX.GetYoutubeName(), null);
		if (PlayerPrefsX.GetYoutubeName() == null)
		{
			PsMetrics.YoutubeAccountRemoved();
		}
		else
		{
			PsMetrics.YoutubeAccountSet();
		}
		Debug.Log("CHANGE YOUTUBER SUCCEED", null);
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x0006D4CF File Offset: 0x0006B8CF
	public static void ChangeYoutuberFAILED(HttpC _c)
	{
		Debug.Log("CHANGE YOUTUBER FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerChangeYoutuber(PlayerPrefsX.GetYoutubeName(), PlayerPrefsX.GetYoutubeId(), PsMetagameManager.m_playerStats.youtubeSubscriberCount, new Action<HttpC>(PsMetagameManager.ChangeYoutuberSUCCEED), new Action<HttpC>(PsMetagameManager.ChangeYoutuberFAILED), null), null);
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x0006D510 File Offset: 0x0006B910
	public static void IAPDebug(string _eventName, params KeyValuePair<string, object>[] _data)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		if (_data != null)
		{
			for (int i = 0; i < _data.Length; i++)
			{
				if (!dictionary.ContainsKey(_data[i].Key))
				{
					dictionary.Add(_data[i].Key, _data[i].Value);
				}
			}
		}
		PsMetagameManager.IAPDebug(_eventName, dictionary, 1);
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x0006D57C File Offset: 0x0006B97C
	public static void IAPDebug(string _eventName, int _debugLevel, params KeyValuePair<string, object>[] _data)
	{
		if (_debugLevel > Manager.m_debugLevel)
		{
			return;
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		if (_data != null)
		{
			for (int i = 0; i < _data.Length; i++)
			{
				if (!dictionary.ContainsKey(_data[i].Key))
				{
					dictionary.Add(_data[i].Key, _data[i].Value);
				}
			}
		}
		PsMetagameManager.IAPDebug(_eventName, dictionary, _debugLevel);
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x0006D5F4 File Offset: 0x0006B9F4
	public static void IAPDebug(string _eventName, Dictionary<string, object> _data, int _debugLevel)
	{
		if (_debugLevel > Manager.m_debugLevel)
		{
			return;
		}
		if (_data == null)
		{
			_data = new Dictionary<string, object>();
		}
		if (!_data.ContainsKey("clientTimestamp"))
		{
			TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1);
			Dictionary<string, long> dictionary = new Dictionary<string, long>();
			dictionary.Add("$date", (long)timeSpan.TotalMilliseconds);
			_data.Add("clientTimestamp", dictionary);
		}
		string text = Json.Serialize(_data);
		Debug.Log(_eventName + " : " + text, null);
		Analytics.SendEvent(_eventName, text);
	}

	// Token: 0x04000943 RID: 2371
	public static Entity m_utilityEntity;

	// Token: 0x04000944 RID: 2372
	public static int m_coinsToDiamondsConvertAmount;

	// Token: 0x04000945 RID: 2373
	public static double m_lastKeyUsedTime;

	// Token: 0x04000946 RID: 2374
	public static int m_keyReloadTimeSeconds;

	// Token: 0x04000947 RID: 2375
	public static int m_keyReloadTimeLeft;

	// Token: 0x04000948 RID: 2376
	public static int m_keyReloadTimeLeftPrevious;

	// Token: 0x04000949 RID: 2377
	public static bool m_keyReloadTimeUpdated;

	// Token: 0x0400094A RID: 2378
	public static PlayerStats m_playerStats = new PlayerStats();

	// Token: 0x0400094B RID: 2379
	public static PsUIMainMenuResources m_menuResourceView;

	// Token: 0x0400094C RID: 2380
	public static bool m_initialized;

	// Token: 0x0400094D RID: 2381
	public static SeasonEndData m_seasonEndData;

	// Token: 0x0400094E RID: 2382
	public static SeasonConfig m_seasonConfig;

	// Token: 0x0400094F RID: 2383
	public static List<PsTournamentMetaData> m_dailyTournaments = new List<PsTournamentMetaData>();

	// Token: 0x04000950 RID: 2384
	public static List<PsTournamentMetaData> m_activeTournaments = new List<PsTournamentMetaData>();

	// Token: 0x04000951 RID: 2385
	public static List<PsChallengeMetaData> m_dailyChallenges;

	// Token: 0x04000952 RID: 2386
	public static PsMinigameMetaData m_freshMetadata;

	// Token: 0x04000953 RID: 2387
	public static bool m_levelUpPopup = false;

	// Token: 0x04000954 RID: 2388
	public static List<ServerQueueItem> m_serverQueueItems = new List<ServerQueueItem>();

	// Token: 0x04000955 RID: 2389
	public static int m_queueWaitTicks = 5;

	// Token: 0x04000956 RID: 2390
	public static PsUIPurchaseWaitPopup m_purchaseWaitPopup;

	// Token: 0x04000957 RID: 2391
	public static TeamData m_team;

	// Token: 0x04000958 RID: 2392
	public static Friends m_friends;

	// Token: 0x04000959 RID: 2393
	public static Action<Friends> m_loadFriendsCallback = null;

	// Token: 0x0400095A RID: 2394
	public static bool m_loadingFriends = false;

	// Token: 0x0400095B RID: 2395
	public static bool m_loadingTeam = false;

	// Token: 0x0400095C RID: 2396
	public static Action<TeamData> m_loadTeamCallback = null;

	// Token: 0x0400095D RID: 2397
	public static List<PlayerData> m_followees;

	// Token: 0x0400095E RID: 2398
	public static List<PlayerData> m_followers;

	// Token: 0x0400095F RID: 2399
	public static ITutorial m_tutorialArrow;

	// Token: 0x04000960 RID: 2400
	public static string m_tutorialKey = null;

	// Token: 0x04000961 RID: 2401
	private static bool m_firstLevelComplete = false;

	// Token: 0x04000962 RID: 2402
	public static PsUIBasePopup m_splashBG;

	// Token: 0x04000963 RID: 2403
	public static bool m_suspiciousActivity = false;

	// Token: 0x04000964 RID: 2404
	public static int m_daySecondsLeft;

	// Token: 0x04000965 RID: 2405
	public static int m_epochDays;

	// Token: 0x04000966 RID: 2406
	public static int m_daySecondsLeftPrevious;

	// Token: 0x04000967 RID: 2407
	public static bool m_daySecondsLeftUpdated;

	// Token: 0x04000968 RID: 2408
	public static double m_lastShopUpdatedTime;

	// Token: 0x04000969 RID: 2409
	public static int m_shopUpdateTime = 86400;

	// Token: 0x0400096A RID: 2410
	public static int m_seasonTimeleft;

	// Token: 0x0400096B RID: 2411
	public static int m_secondsSinceLastLogin;

	// Token: 0x0400096C RID: 2412
	private static int m_seasonTimerStart;

	// Token: 0x0400096D RID: 2413
	private static int m_sinceLastLoginStart;

	// Token: 0x0400096E RID: 2414
	public static List<FollowRewardData> m_followRewardData;

	// Token: 0x0400096F RID: 2415
	public static bool m_ratingStatusChanged = false;

	// Token: 0x04000970 RID: 2416
	public static EventMessage m_patchNotes;

	// Token: 0x04000971 RID: 2417
	public static EventMessage m_eventMessage;

	// Token: 0x04000972 RID: 2418
	public static List<EventMessage> m_eventList = null;

	// Token: 0x04000973 RID: 2419
	public static EventGifts m_giftEvents = null;

	// Token: 0x04000974 RID: 2420
	public static List<EventMessage> m_tournaments = null;

	// Token: 0x04000975 RID: 2421
	public static EventMessage m_activeTournament;

	// Token: 0x04000976 RID: 2422
	public static EventMessage m_doubleValueGoodOrBadEvent;

	// Token: 0x04000977 RID: 2423
	public static ObscuredInt m_skipPrice = 5;

	// Token: 0x04000978 RID: 2424
	public static ObscuredFloat m_timerPriceGemsPerMinute = 0.1f;

	// Token: 0x04000979 RID: 2425
	public static ObscuredInt m_commonLimit = 99;

	// Token: 0x0400097A RID: 2426
	public static ObscuredInt m_rareLimit = 50;

	// Token: 0x0400097B RID: 2427
	public static ObscuredInt m_epicLimit = 20;

	// Token: 0x0400097C RID: 2428
	public static ObscuredInt m_specialOfferDurationMinutes = 60;

	// Token: 0x0400097D RID: 2429
	public static ObscuredInt m_specialOfferCooldownMinutes = 4320;

	// Token: 0x0400097E RID: 2430
	public static PsMetagameManager.PsVehicleGachaData m_vehicleGachaData;

	// Token: 0x0400097F RID: 2431
	public static bool m_firstTimeLogin = false;

	// Token: 0x04000980 RID: 2432
	private static List<PsTimedSpecialOffer> m_timedSpecialOffers;

	// Token: 0x04000981 RID: 2433
	public static bool m_shopInitialized = false;

	// Token: 0x04000982 RID: 2434
	public static bool m_gachasInitialized = false;

	// Token: 0x04000983 RID: 2435
	public static Action d_shopUpdateAction;

	// Token: 0x04000984 RID: 2436
	public static int m_shopUpgradeDay;

	// Token: 0x04000985 RID: 2437
	public static List<ShopUpgradeItemData> m_shopUpgradeItems;

	// Token: 0x04000986 RID: 2438
	private static bool m_editorItemsGiven = false;

	// Token: 0x04000987 RID: 2439
	private static int ghostindex = 0;

	// Token: 0x04000988 RID: 2440
	public static readonly int m_rateAppNodeID = 15;

	// Token: 0x04000989 RID: 2441
	public static readonly int m_rateRemindTimes = 1;

	// Token: 0x0400098A RID: 2442
	private static bool m_rateAlertShownInThisSession = false;

	// Token: 0x02000144 RID: 324
	public class PsVehicleGachaData
	{
		// Token: 0x040009FF RID: 2559
		public const int MAP_PIECE_COUNT_AT_START = 6;

		// Token: 0x04000A00 RID: 2560
		public const int MAP_PIECE_MAX = 12;

		// Token: 0x04000A01 RID: 2561
		public int m_vehicleIndex;

		// Token: 0x04000A02 RID: 2562
		public int m_rivalWonCount;

		// Token: 0x04000A03 RID: 2563
		public string m_rivalGhostId;

		// Token: 0x04000A04 RID: 2564
		public int m_mapSeed;

		// Token: 0x04000A05 RID: 2565
		public int m_mapPieceCount;

		// Token: 0x04000A06 RID: 2566
		public int m_mapPiecesMax;

		// Token: 0x04000A07 RID: 2567
		public int m_racingGachaCount;

		// Token: 0x04000A08 RID: 2568
		public int m_adventureGachaCount;

		// Token: 0x04000A09 RID: 2569
		public int m_offroadCarUpgradeChest;

		// Token: 0x04000A0A RID: 2570
		public int m_motorcycleUpgradeChest;
	}

	// Token: 0x02000145 RID: 325
	public class RateThisGameFlow : Flow
	{
		// Token: 0x06000ADB RID: 2779 RVA: 0x0006D9C6 File Offset: 0x0006BDC6
		public RateThisGameFlow(Action _proceed, Action _cancel, Action _exit)
			: base(_proceed, _cancel, null)
		{
			this.ExitAction = _exit;
			CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
			this.OpenDoYouLikeThisGamePopup();
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0006D9F0 File Offset: 0x0006BDF0
		private void OpenDoYouLikeThisGamePopup()
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUIDoYouLikeThisGamePopup), null, null, null, true, true, InitialPage.Center, false, false, false);
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
			this.m_popup.SetAction("Continue", delegate
			{
				this.DestroyContent();
				this.OpenRateThisGamePopup();
			});
			this.m_popup.SetAction("Cancel", delegate
			{
				this.DestroyContent();
				this.OpenContactUsPopup();
			});
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0006DA90 File Offset: 0x0006BE90
		private void OpenRateThisGamePopup()
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUIPleaseRatePopup), null, null, null, true, true, InitialPage.Center, false, false, false);
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
			this.m_popup.SetAction("Continue", delegate
			{
				this.ExitFlow();
				this.Proceed.Invoke();
				Application.OpenURL("market://details?id=com.traplight.bigbangracing");
			});
			this.m_popup.SetAction("Cancel", delegate
			{
				this.ExitFlow();
				this.Cancel.Invoke();
			});
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0006DB30 File Offset: 0x0006BF30
		private void OpenContactUsPopup()
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUIContactUsPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
			this.m_popup.SetAction("Continue", delegate
			{
				this.ExitFlow();
				this.Proceed.Invoke();
				string text = "Big Bang Racing contact";
				string text2 = string.Concat(new string[]
				{
					"Player name: ",
					PlayerPrefsX.GetUserName(),
					"\rPlayer TAG: ",
					PlayerPrefsX.GetUserTag(),
					"\rDevice: ",
					SystemInfo.deviceModel,
					"\n\rDescribe your issue with Big Bang Racing below the line:\r------------------------------\r"
				});
				text = WWW.EscapeURL(text).Replace("+", "%20");
				text2 = WWW.EscapeURL(text2).Replace("+", "%20");
				string text3 = "mailto:support@traplightgames.com?subject=" + text + "&body=" + text2;
				text3 = "https://bigbangracing.zendesk.com/";
				Application.OpenURL(text3);
			});
			this.m_popup.SetAction("Cancel", delegate
			{
				this.ExitFlow();
				this.Proceed.Invoke();
			});
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0006DBD0 File Offset: 0x0006BFD0
		private void DestroyContent()
		{
			if (this.m_popup != null)
			{
				this.m_popup.Destroy();
				this.m_popup = null;
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0006DBEF File Offset: 0x0006BFEF
		private void ExitFlow()
		{
			this.DestroyContent();
			CameraS.RemoveBlur();
			this.ExitAction.Invoke();
		}

		// Token: 0x04000A0B RID: 2571
		private PsUIBasePopup m_popup;

		// Token: 0x04000A0C RID: 2572
		private Action ExitAction;
	}

	// Token: 0x02000146 RID: 326
	public class SaveRatingData
	{
		// Token: 0x04000A0D RID: 2573
		public PsRating m_rating;

		// Token: 0x04000A0E RID: 2574
		public string m_minigameId;

		// Token: 0x04000A0F RID: 2575
		public string m_minigameContext;

		// Token: 0x04000A10 RID: 2576
		public string m_minigameName;

		// Token: 0x04000A11 RID: 2577
		public string m_minigameGameMode;

		// Token: 0x04000A12 RID: 2578
		public bool m_playerReachedGoal;

		// Token: 0x04000A13 RID: 2579
		public int m_playerLevel;

		// Token: 0x04000A14 RID: 2580
		public string m_playerUnitName;

		// Token: 0x04000A15 RID: 2581
		public bool m_playerSkipped;

		// Token: 0x04000A16 RID: 2582
		public Hashtable setData;

		// Token: 0x04000A17 RID: 2583
		public bool m_isFresh;

		// Token: 0x04000A18 RID: 2584
		public int m_ghostsWon = -1;

		// Token: 0x04000A19 RID: 2585
		public int m_ghostCount = -1;
	}

	// Token: 0x02000147 RID: 327
	public struct CommentInfo
	{
		// Token: 0x04000A1A RID: 2586
		public string gameId;

		// Token: 0x04000A1B RID: 2587
		public string comment;

		// Token: 0x04000A1C RID: 2588
		public string tag;

		// Token: 0x04000A1D RID: 2589
		public string tournamentId;
	}
}
