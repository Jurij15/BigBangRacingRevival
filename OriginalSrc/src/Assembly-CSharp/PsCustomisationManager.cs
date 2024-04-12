using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class PsCustomisationManager
{
	// Token: 0x060004A1 RID: 1185 RVA: 0x00039517 File Offset: 0x00037917
	static PsCustomisationManager()
	{
		PsCustomisationManager.Initialize();
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00039528 File Offset: 0x00037928
	public static void SetData(Dictionary<string, object> _data)
	{
		Dictionary<string, bool> dictionary;
		if (_data.ContainsKey("OffroadCarVisual"))
		{
			dictionary = PsCustomisationManager.ParseData(_data["OffroadCarVisual"]);
		}
		else
		{
			dictionary = new Dictionary<string, bool>();
		}
		Dictionary<string, bool> dictionary2;
		if (_data.ContainsKey("MotorcycleVisual"))
		{
			dictionary2 = PsCustomisationManager.ParseData(_data["MotorcycleVisual"]);
		}
		else
		{
			dictionary2 = new Dictionary<string, bool>();
		}
		Dictionary<string, bool> dictionary3;
		if (_data.ContainsKey("CharacterVisual"))
		{
			dictionary3 = PsCustomisationManager.ParseData(_data["CharacterVisual"]);
		}
		else
		{
			dictionary3 = new Dictionary<string, bool>();
		}
		for (int i = 0; i < PsCustomisationManager.m_characterCustomisationData.m_customisationItems.Length; i++)
		{
			PsCustomisationItem psCustomisationItem = PsCustomisationManager.m_characterCustomisationData.m_customisationItems[i];
			if (dictionary3.ContainsKey(psCustomisationItem.m_identifier))
			{
				psCustomisationItem.m_unlocked = true;
				psCustomisationItem.m_installed = dictionary3[psCustomisationItem.m_identifier];
			}
		}
		PsCustomisationData psCustomisationData = PsCustomisationManager.m_vehicleCustomisationDatas[typeof(OffroadCar).ToString()];
		for (int j = 0; j < psCustomisationData.m_customisationItems.Length; j++)
		{
			PsCustomisationItem psCustomisationItem2 = psCustomisationData.m_customisationItems[j];
			if (dictionary.ContainsKey(psCustomisationItem2.m_identifier))
			{
				psCustomisationItem2.m_unlocked = true;
				psCustomisationItem2.m_installed = dictionary[psCustomisationItem2.m_identifier];
			}
			else if (psCustomisationItem2.m_identifier == "Helmet" && PsMetagameManager.m_playerStats.dirtBikeBundle)
			{
				psCustomisationItem2.m_unlocked = true;
			}
			else if (psCustomisationItem2.m_identifier.StartsWith("trail") && PsMetagameManager.m_playerStats.trailsPurchased.Contains(psCustomisationItem2.m_identifier))
			{
				psCustomisationItem2.m_unlocked = true;
			}
			else if (psCustomisationItem2.m_category == PsCustomisationManager.CustomisationCategory.HAT && PsMetagameManager.m_playerStats.hatsPurchased.Contains(psCustomisationItem2.m_iapIdentifier))
			{
				psCustomisationItem2.m_unlocked = true;
			}
			else if (PsCustomisationManager.m_oldFormat)
			{
				if (psCustomisationItem2.m_requiredAchievements != null && psCustomisationItem2.m_requiredAchievements.Count > 0)
				{
					for (int k = 0; k < psCustomisationItem2.m_requiredAchievements.Count; k++)
					{
						if (PsAchievementManager.IsCompleted(psCustomisationItem2.m_requiredAchievements[k]))
						{
							psCustomisationItem2.m_unlocked = true;
							break;
						}
					}
				}
				else if (psCustomisationItem2.m_identifier == "BaseballHat")
				{
					psCustomisationItem2.m_unlocked = true;
				}
			}
		}
		psCustomisationData = PsCustomisationManager.m_vehicleCustomisationDatas[typeof(Motorcycle).ToString()];
		for (int l = 0; l < psCustomisationData.m_customisationItems.Length; l++)
		{
			PsCustomisationItem psCustomisationItem3 = psCustomisationData.m_customisationItems[l];
			if (dictionary2.ContainsKey(psCustomisationItem3.m_identifier))
			{
				psCustomisationItem3.m_unlocked = true;
				psCustomisationItem3.m_installed = dictionary2[psCustomisationItem3.m_identifier];
			}
			else if (psCustomisationItem3.m_identifier == "Helmet" && PsMetagameManager.m_playerStats.dirtBikeBundle)
			{
				psCustomisationItem3.m_unlocked = true;
			}
			else if (psCustomisationItem3.m_identifier.StartsWith("trail") && PsMetagameManager.m_playerStats.trailsPurchased.Contains(psCustomisationItem3.m_identifier))
			{
				psCustomisationItem3.m_unlocked = true;
			}
			else if (psCustomisationItem3.m_category == PsCustomisationManager.CustomisationCategory.HAT && PsMetagameManager.m_playerStats.hatsPurchased.Contains(psCustomisationItem3.m_iapIdentifier))
			{
				psCustomisationItem3.m_unlocked = true;
			}
			else if (PsCustomisationManager.m_oldFormat)
			{
				if (psCustomisationItem3.m_requiredAchievements != null && psCustomisationItem3.m_requiredAchievements.Count > 0)
				{
					for (int m = 0; m < psCustomisationItem3.m_requiredAchievements.Count; m++)
					{
						if (PsAchievementManager.IsCompleted(psCustomisationItem3.m_requiredAchievements[m]))
						{
							psCustomisationItem3.m_unlocked = true;
							break;
						}
					}
				}
				else if (psCustomisationItem3.m_identifier == "BaseballHat")
				{
					psCustomisationItem3.m_unlocked = true;
				}
			}
		}
		List<string> followRewards = PsMetagameManager.GetFollowRewards(PlayerPrefsX.GetUserId());
		if (followRewards != null && followRewards.Count > 0)
		{
			bool flag = false;
			for (int n = 0; n < followRewards.Count; n++)
			{
				PsCustomisationItem itemByIdentifier = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(followRewards[n]);
				if (itemByIdentifier != null && itemByIdentifier.m_category == PsCustomisationManager.CustomisationCategory.HAT && !itemByIdentifier.m_unlocked)
				{
					for (int num = 0; num < PsState.m_vehicleTypes.Length; num++)
					{
						PsCustomisationManager.UnlockItem(PsState.m_vehicleTypes[num], followRewards[n]);
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

	// Token: 0x060004A4 RID: 1188 RVA: 0x00039A84 File Offset: 0x00037E84
	public static Dictionary<string, bool> ParseData(object _data)
	{
		Dictionary<string, object> dictionary = _data as Dictionary<string, object>;
		Dictionary<string, bool> dictionary2 = new Dictionary<string, bool>();
		if (dictionary != null)
		{
			dictionary2 = ClientTools.ParseDictionaryObjectToBoolean(dictionary);
		}
		else
		{
			List<string> list = ClientTools.ParseList<string>(_data as List<object>);
			for (int i = 0; i < list.Count; i++)
			{
				dictionary2.Add(list[i], true);
			}
			PsCustomisationManager.m_oldFormat = true;
		}
		return dictionary2;
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00039AE8 File Offset: 0x00037EE8
	public static void Initialize()
	{
		PsCustomisationManager.m_vehicleCustomisationDatas = new Dictionary<string, PsCustomisationData>();
		PsCustomisationManager.m_vehicleCustomisationDatas.Add(typeof(OffroadCar).ToString(), new PsCustomisationData(PsCustomisationManager.GetDefaultUpgradeItems().ToArray()));
		PsCustomisationManager.m_vehicleCustomisationDatas.Add(typeof(Motorcycle).ToString(), new PsCustomisationData(PsCustomisationManager.GetDefaultUpgradeItems().ToArray()));
		List<PsCustomisationItem> list = new List<PsCustomisationItem>();
		PsCustomisationManager.m_characterCustomisationData = new PsCustomisationData(list.ToArray());
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x00039B68 File Offset: 0x00037F68
	private static List<PsCustomisationItem> GetDefaultUpgradeItems()
	{
		List<PsCustomisationItem> list = new List<PsCustomisationItem>();
		list.Add(new PsCustomisationItem("MotocrossHelmet", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_COMMON_1, "Nice hat!", "menu_hat_icon_motocrosshelmet", PsRarity.Common, null, true));
		list.Add(new PsCustomisationItem("BaseballHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_COMMON_2, "Nice hat!", "menu_hat_icon_cap_1", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("MushroomHat", "hat_common_mushroomhat", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_COMMON_3, "Nice hat!", "menu_hat_icon_toad_1", PsRarity.Common, new string[] { "blowTwoHundredBarrels" }, false));
		list.Add(new PsCustomisationItem("BarbarianHelmet", "hat_common_barbarianhelmet", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_COMMON_4, "Nice hat!", "menu_hat_icon_barbarian_1", PsRarity.Common, new string[] { "blowTwoThousandBarrels" }, false));
		list.Add(new PsCustomisationItem("CowboyHat", "hat_common_cowboyhat", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_COMMON_5, "Nice hat!", "menu_hat_icon_cowboy_1", PsRarity.Common, new string[] { "breakTwoHundredPlanks" }, false));
		list.Add(new PsCustomisationItem("HorseHead", "hat_common_horsehead", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_COMMON_6, "Nice hat!", "menu_hat_icon_horse_1", PsRarity.Common, new string[] { "reachHundredHPBike" }, false));
		list.Add(new PsCustomisationItem("BootHat", "hat_common_boothat", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EPIC_4, "Nice hat!", "menu_hat_icon_BootHat", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("WitchHat", "hat_common_witchhat", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_COMMON_MAGIC, "Nice hat!", "menu_hat_icon_WitchHat", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("GirlyHair", "hat_common_girlyhair", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_COMMON_BLONDIE, "Nice hat!", "menu_hat_icon_GirlyHair", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("ChickenHat", "hat_rare_chickenhat", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_RARE_1, "Nice hat!", "menu_hat_icon_chicken_1", PsRarity.Rare, new string[] { "triggerFortyChickens" }, false));
		list.Add(new PsCustomisationItem("KnightHelmet", "hat_rare_knighthelmet", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_RARE_2, "Nice hat!", "menu_hat_icon_knight_1", PsRarity.Rare, new string[] { "breakTwoThousandPlanks" }, false));
		list.Add(new PsCustomisationItem("Mask", "hat_rare_mask", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_RARE_3, "Nice hat!", "menu_hat_icon_mask_1", PsRarity.Rare, new string[] { "reachHundredHPCar" }, false));
		list.Add(new PsCustomisationItem("PaperBag", "hat_rare_paperbag", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_RARE_4, "Nice hat!", "menu_hat_icon_paperbag_1", PsRarity.Rare, new string[] { "breakTwoThousandBoxes" }, false));
		list.Add(new PsCustomisationItem("PilotHat", "hat_rare_pilothat", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_RARE_5, "Nice hat!", "menu_hat_icon_pilot_1", PsRarity.Rare, new string[] { "hundredBackFlips" }, false));
		list.Add(new PsCustomisationItem("DealWithItGlasses", "hat_rare_dealwithitglasses", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_RARE_6, "Nice hat!", "menu_hat_icon_glasses_1", PsRarity.Rare, new string[] { "fiveHundredFrontFlips" }, false));
		list.Add(new PsCustomisationItem("PumpkinHat", "hat_rare_pumpkinhat", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_RARE_PUMPKIN, "Nice hat!", "menu_hat_icon_PumpkinHat", PsRarity.Rare, null, false));
		list.Add(new PsCustomisationItem("GoldenShades", "hat_epic_goldenshades", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EPIC_1, "Nice hat!", "menu_hat_icon_goldenshades", PsRarity.Epic, null, false));
		list.Add(new PsCustomisationItem("Fish", "hat_epic_fish", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EPIC_2, "Nice hat!", "menu_hat_icon_fishhat", PsRarity.Epic, null, false));
		list.Add(new PsCustomisationItem("VR", "hat_epic_vr", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EPIC_3, "Nice hat!", "menu_hat_icon_vrgoggles", PsRarity.Epic, null, false));
		list.Add(new PsCustomisationItem("WerewolfMask", "hat_epic_werewolfmask", PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EPIC_WEREWOLF, "Nice hat!", "menu_hat_icon_WerewolfMask", PsRarity.Epic, null, false));
		list.Add(new PsCustomisationItem("Helmet", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_1, "Nice hat!", "menu_hat_icon_racehelmet_1", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("MrBaconHair", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_BACON, "Nice hat!", "menu_hat_icon_MrBaconHair", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("ReversalCrown", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_CROWN, "Nice hat!", "menu_hat_icon_ReversalCrown", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("HawkMask", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_HAWK, "Nice hat!", "menu_hat_icon_HawkMask", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("SteelMask", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_ROBOT, "Nice hat!", "menu_hat_icon_SteelMask", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("PowerHelmet", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_POWERHELMET, "Nice hat!", "menu_hat_icon_PowerHelmet", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("CatHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_KITTY, "Nice hat!", "menu_hat_icon_CatHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("FeatherHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_SWAG, "Nice hat!", "menu_hat_icon_FeatherHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("OrangeHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_ORANGE, "Nice hat!", "menu_hat_icon_AnnoyingOrangeHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("ToadHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_TOADIE, "Nice hat!", "menu_hat_icon_ToadHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("BuilderHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_HARDHAT, "Nice hat!", "menu_hat_icon_BuilderHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("WinterHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_WINTERHAT, "Nice hat!", "menu_hat_icon_WinterHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("WinterCap", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_WINTERCAP, "Nice hat!", "menu_hat_icon_WinterCap", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("UnicornMask", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_UNICORN, "Nice hat!", "menu_hat_icon_Unicorn", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("LovelyHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_PINKHAT, "Nice hat!", "menu_hat_icon_LovelyHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("ReindeerHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_HORNS, "Nice hat!", "menu_hat_icon_Reindeer", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("GoldenCarHelmet", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EXCLUSIVE_GOLDEN_HAT, "Nice hat!", "menu_hat_icon_GoldenCarHelmet", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("AnglerFishHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_NICKATNYTE_FISH, "Nice hat!", "menu_hat_icon_AnglerFishHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("BobbleHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_CAMSUCKSATGAMING, "Nice hat!", "menu_hat_icon_BobbleHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("MilkJugHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_EPIPHANY, "Nice hat!", "menu_hat_icon_MilkJugHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("TimeTravellerHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_DEJOTA, "Nice hat!", "menu_hat_icon_TimeTravellerHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("LorpHeadband", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_BANDANA, "Nice hat!", "menu_hat_icon_LorpHeadband", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("AnniversaryPartyHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_ANNIVERSARY_PARTY_HAT, "Nice hat!", "menu_hat_icon_AnniversaryPartyHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("AnniversaryCandleHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_ANNIVERSARY_CANDLE_HAT, "Nice hat!", "menu_hat_icon_AnniversaryCandleHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("IceCreamHat", string.Empty, PsCustomisationManager.CustomisationCategory.HAT, StringID.HAT_ICE_CREAM, "Nice hat!", "menu_hat_icon_IceCreamHat", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("trail_bubble", "trail_bubble", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_BUBBLE, "Nice hat!", "menu_trail_icon_soap", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("trail_feather", "trail_feather", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_FEATHER, "Nice hat!", "menu_trail_icon_feather", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("trail_fire", "trail_fire", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_FIRE, "Nice hat!", "menu_trail_icon_fire", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("trail_cash", "trail_cash", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_CASH, "Nice hat!", "menu_trail_icon_cash", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("trail_rainbow", "trail_rainbow", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_RAINBOW, "Nice hat!", "menu_trail_icon_rainbow", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("trail_scifi", "trail_scifi", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_SCIFI, "Nice hat!", "menu_trail_icon_turquoise", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("trail_death", "trail_death", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_DEATH, "Nice hat!", "menu_trail_icon_death", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("trail_bat", "trail_bat", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_BAT, "Nice hat!", "menu_trail_icon_bat", PsRarity.Common, null, false));
		list.Add(new PsCustomisationItem("trail_snow", "trail_snow", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_SNOW, "Nice hat!", "menu_trail_icon_winter", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("trail_kittypaw", "Trail_kittypaw", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_KITTYPAW, "Nice hat!", "menu_trail_icon_kittypaw", PsRarity.Exclusive, null, false));
		list.Add(new PsCustomisationItem("trail_anniversary", "Trail_anniversary", PsCustomisationManager.CustomisationCategory.TRAIL, StringID.TRAIL_ANNIVERSARY, "Nice hat!", "menu_trail_icon_anniversary", PsRarity.Exclusive, null, false));
		return list;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0003A4C0 File Offset: 0x000388C0
	public static PsCustomisationData GetVehicleCustomisationData(Type _vehicleType)
	{
		PsCustomisationData psCustomisationData;
		if (PsCustomisationManager.m_vehicleCustomisationDatas.TryGetValue(_vehicleType.ToString(), ref psCustomisationData))
		{
			return psCustomisationData;
		}
		return null;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0003A4E7 File Offset: 0x000388E7
	public static PsCustomisationData GetCharacterCustomisationData()
	{
		return PsCustomisationManager.m_characterCustomisationData;
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0003A4F0 File Offset: 0x000388F0
	public static void UnlockItem(Type _vehicleType, string _identifier)
	{
		PsCustomisationData psCustomisationData;
		if (PsCustomisationManager.m_vehicleCustomisationDatas.TryGetValue(_vehicleType.ToString(), ref psCustomisationData))
		{
			PsCustomisationItem itemByIdentifier = psCustomisationData.GetItemByIdentifier(_identifier);
			itemByIdentifier.m_unlocked = true;
			PsCustomisationManager.m_dirty = true;
		}
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0003A52C File Offset: 0x0003892C
	public static bool InstallVehicleItem(Type _vehicleType, string _identifier)
	{
		PsCustomisationData psCustomisationData;
		if (PsCustomisationManager.m_vehicleCustomisationDatas.TryGetValue(_vehicleType.ToString(), ref psCustomisationData))
		{
			for (int i = 0; i < psCustomisationData.m_customisationItems.Length; i++)
			{
				if (_identifier == psCustomisationData.m_customisationItems[i].m_identifier)
				{
					PsCustomisationItem installedItemByCategory = psCustomisationData.GetInstalledItemByCategory(psCustomisationData.m_customisationItems[i].m_category);
					if (installedItemByCategory != null)
					{
						installedItemByCategory.m_installed = false;
					}
					psCustomisationData.m_customisationItems[i].m_installed = true;
					PsCustomisationManager.m_dirty = true;
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0003A5BC File Offset: 0x000389BC
	public static bool UninstallVehicleItem(Type _vehicleType, string _identifier)
	{
		PsCustomisationData psCustomisationData;
		if (PsCustomisationManager.m_vehicleCustomisationDatas.TryGetValue(_vehicleType.ToString(), ref psCustomisationData))
		{
			for (int i = 0; i < psCustomisationData.m_customisationItems.Length; i++)
			{
				if (_identifier == psCustomisationData.m_customisationItems[i].m_identifier && psCustomisationData.m_customisationItems[i].m_installed)
				{
					psCustomisationData.m_customisationItems[i].m_installed = false;
					PsCustomisationManager.m_dirty = true;
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0003A63C File Offset: 0x00038A3C
	public static bool InstallCharacterItem(string _identifier)
	{
		for (int i = 0; i < PsCustomisationManager.m_characterCustomisationData.m_customisationItems.Length; i++)
		{
			if (_identifier == PsCustomisationManager.m_characterCustomisationData.m_customisationItems[i].m_identifier)
			{
				PsCustomisationItem installedItemByCategory = PsCustomisationManager.m_characterCustomisationData.GetInstalledItemByCategory(PsCustomisationManager.m_characterCustomisationData.m_customisationItems[i].m_category);
				if (installedItemByCategory != null)
				{
					installedItemByCategory.m_installed = false;
				}
				PsCustomisationManager.m_characterCustomisationData.m_customisationItems[i].m_installed = true;
				PsCustomisationManager.m_dirty = true;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0003A6C8 File Offset: 0x00038AC8
	public static GameObject GetHatPrefabByIdentifier(string _hatIdentifier)
	{
		ResourcePool.Asset asset;
		if (_hatIdentifier != null)
		{
			if (PsCustomisationManager.<>f__switch$map0 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(45);
				dictionary.Add("BaseballHat", 0);
				dictionary.Add("Helmet", 1);
				dictionary.Add("BarbarianHelmet", 2);
				dictionary.Add("CowboyHat", 3);
				dictionary.Add("HorseHead", 4);
				dictionary.Add("ChickenHat", 5);
				dictionary.Add("KnightHelmet", 6);
				dictionary.Add("Mask", 7);
				dictionary.Add("PaperBag", 8);
				dictionary.Add("PilotHat", 9);
				dictionary.Add("MushroomHat", 10);
				dictionary.Add("DealWithItGlasses", 11);
				dictionary.Add("GoldenShades", 12);
				dictionary.Add("Fish", 13);
				dictionary.Add("VR", 14);
				dictionary.Add("MotocrossHelmet", 15);
				dictionary.Add("MrBaconHair", 16);
				dictionary.Add("ReversalCrown", 17);
				dictionary.Add("HawkMask", 18);
				dictionary.Add("SteelMask", 19);
				dictionary.Add("PowerHelmet", 20);
				dictionary.Add("CatHat", 21);
				dictionary.Add("FeatherHat", 22);
				dictionary.Add("BootHat", 23);
				dictionary.Add("OrangeHat", 24);
				dictionary.Add("PumpkinHat", 25);
				dictionary.Add("GirlyHair", 26);
				dictionary.Add("WitchHat", 27);
				dictionary.Add("WerewolfMask", 28);
				dictionary.Add("ToadHat", 29);
				dictionary.Add("BuilderHat", 30);
				dictionary.Add("WinterHat", 31);
				dictionary.Add("WinterCap", 32);
				dictionary.Add("UnicornMask", 33);
				dictionary.Add("LovelyHat", 34);
				dictionary.Add("ReindeerHat", 35);
				dictionary.Add("GoldenCarHelmet", 36);
				dictionary.Add("AnglerFishHat", 37);
				dictionary.Add("BobbleHat", 38);
				dictionary.Add("MilkJugHat", 39);
				dictionary.Add("TimeTravellerHat", 40);
				dictionary.Add("LorpHeadband", 41);
				dictionary.Add("AnniversaryPartyHat", 42);
				dictionary.Add("AnniversaryCandleHat", 43);
				dictionary.Add("IceCreamHat", 44);
				PsCustomisationManager.<>f__switch$map0 = dictionary;
			}
			int num;
			if (PsCustomisationManager.<>f__switch$map0.TryGetValue(_hatIdentifier, ref num))
			{
				switch (num)
				{
				case 0:
					asset = RESOURCE.AlienBaseballHatPrefab_GameObject;
					goto IL_52D;
				case 1:
					asset = RESOURCE.AlienHelmetPrefab_GameObject;
					goto IL_52D;
				case 2:
					asset = RESOURCE.BarbarianHelmetPrefab_GameObject;
					goto IL_52D;
				case 3:
					asset = RESOURCE.CowboyHatPrefab_GameObject;
					goto IL_52D;
				case 4:
					asset = RESOURCE.HorseHeadMaskPrefab_GameObject;
					goto IL_52D;
				case 5:
					asset = RESOURCE.ChickenHatPrefab_GameObject;
					goto IL_52D;
				case 6:
					asset = RESOURCE.KnightHelmPrefab_GameObject;
					goto IL_52D;
				case 7:
					asset = RESOURCE.DeathMaskPrefab_GameObject;
					goto IL_52D;
				case 8:
					asset = RESOURCE.PaperBagHatPrefab_GameObject;
					goto IL_52D;
				case 9:
					asset = RESOURCE.FlightCapPrefab_GameObject;
					goto IL_52D;
				case 10:
					asset = RESOURCE.MushroomHatPrefab_GameObject;
					goto IL_52D;
				case 11:
					asset = RESOURCE.DealWithItGlassesPrefab_GameObject;
					goto IL_52D;
				case 12:
					asset = RESOURCE.GoldenShadesPrefab_GameObject;
					goto IL_52D;
				case 13:
					asset = RESOURCE.FishHatPrefab_GameObject;
					goto IL_52D;
				case 14:
					asset = RESOURCE.VRGogglesPrefab_GameObject;
					goto IL_52D;
				case 15:
					asset = RESOURCE.MotocrossHelmetPrefab_GameObject;
					goto IL_52D;
				case 16:
					asset = RESOURCE.MrBaconHairPrefab_GameObject;
					goto IL_52D;
				case 17:
					asset = RESOURCE.ReversalCrownPrefab_GameObject;
					goto IL_52D;
				case 18:
					asset = RESOURCE.HawkMaskPrefab_GameObject;
					goto IL_52D;
				case 19:
					asset = RESOURCE.SteelMaskPrefab_GameObject;
					goto IL_52D;
				case 20:
					asset = RESOURCE.PowerHelmetPrefab_GameObject;
					goto IL_52D;
				case 21:
					asset = RESOURCE.CatHatPrefab_GameObject;
					goto IL_52D;
				case 22:
					asset = RESOURCE.FeatherHatPrefab_GameObject;
					goto IL_52D;
				case 23:
					asset = RESOURCE.BootHatPrefab_GameObject;
					goto IL_52D;
				case 24:
					asset = RESOURCE.AnnoyingOrangeHatPrefab_GameObject;
					goto IL_52D;
				case 25:
					asset = RESOURCE.PumpkinHatPrefab_GameObject;
					goto IL_52D;
				case 26:
					asset = RESOURCE.GirlyHairPrefab_GameObject;
					goto IL_52D;
				case 27:
					asset = RESOURCE.WitchHatPrefab_GameObject;
					goto IL_52D;
				case 28:
					asset = RESOURCE.WerewolfMaskPrefab_GameObject;
					goto IL_52D;
				case 29:
					asset = RESOURCE.ToadHatPrefab_GameObject;
					goto IL_52D;
				case 30:
					asset = RESOURCE.BuilderHatPrefab_GameObject;
					goto IL_52D;
				case 31:
					asset = RESOURCE.WinterHatPrefab_GameObject;
					goto IL_52D;
				case 32:
					asset = RESOURCE.WinterCapPrefab_GameObject;
					goto IL_52D;
				case 33:
					asset = RESOURCE.UnicornMaskPrefab_GameObject;
					goto IL_52D;
				case 34:
					asset = RESOURCE.LovelyHatPrefab_GameObject;
					goto IL_52D;
				case 35:
					asset = RESOURCE.ReindeerHatPrefab_GameObject;
					goto IL_52D;
				case 36:
					asset = RESOURCE.GoldenCarHelmetPrefab_GameObject;
					goto IL_52D;
				case 37:
					asset = RESOURCE.AnglerFishHatPrefab_GameObject;
					goto IL_52D;
				case 38:
					asset = RESOURCE.BobbleHatPrefab_GameObject;
					goto IL_52D;
				case 39:
					asset = RESOURCE.MilkJugHatPrefab_GameObject;
					goto IL_52D;
				case 40:
					asset = RESOURCE.TimeTravellerHatPrefab_GameObject;
					goto IL_52D;
				case 41:
					asset = RESOURCE.LorpHeadbandPrefab_GameObject;
					goto IL_52D;
				case 42:
					asset = RESOURCE.AnniversaryPartyHatPrefab_GameObject;
					goto IL_52D;
				case 43:
					asset = RESOURCE.AnniversaryCandleHatPrefab_GameObject;
					goto IL_52D;
				case 44:
					asset = RESOURCE.IceCreamHatPrefab_GameObject;
					goto IL_52D;
				}
			}
		}
		asset = RESOURCE.MotocrossHelmetPrefab_GameObject;
		IL_52D:
		return ResourceManager.GetGameObject(asset);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0003AC08 File Offset: 0x00039008
	public static GameObject GetTrailPrefabByIdentifier(string _trailIdentifier)
	{
		if (_trailIdentifier != null)
		{
			if (PsCustomisationManager.<>f__switch$map1 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(12);
				dictionary.Add("trail_bubble", 0);
				dictionary.Add("trail_feather", 1);
				dictionary.Add("trail_fire", 2);
				dictionary.Add("trail_cash", 3);
				dictionary.Add("trail_rainbow", 4);
				dictionary.Add("trail_scifi", 5);
				dictionary.Add("trail_death", 6);
				dictionary.Add("trail_boss", 7);
				dictionary.Add("trail_bat", 8);
				dictionary.Add("trail_snow", 9);
				dictionary.Add("trail_kittypaw", 10);
				dictionary.Add("trail_anniversary", 11);
				PsCustomisationManager.<>f__switch$map1 = dictionary;
			}
			int num;
			if (PsCustomisationManager.<>f__switch$map1.TryGetValue(_trailIdentifier, ref num))
			{
				ResourcePool.Asset asset;
				switch (num)
				{
				case 0:
					asset = RESOURCE.EffectBubbleTrailPrefab_GameObject;
					break;
				case 1:
					asset = RESOURCE.EffectChickenTrailPrefab_GameObject;
					break;
				case 2:
					asset = RESOURCE.EffectFireTrailPrefab_GameObject;
					break;
				case 3:
					asset = RESOURCE.EffectMoneyTrailPrefab_GameObject;
					break;
				case 4:
					asset = RESOURCE.EffectRainbowTrailPrefab_GameObject;
					break;
				case 5:
					asset = RESOURCE.EffectScifiTrailPrefab_GameObject;
					break;
				case 6:
					asset = RESOURCE.EffectDeathTrailPrefab_GameObject;
					break;
				case 7:
					asset = RESOURCE.EffectBossTrailPrefab_GameObject;
					break;
				case 8:
					asset = RESOURCE.EffectBatTrailPrefab_GameObject;
					break;
				case 9:
					asset = RESOURCE.EffectWinterTrailPrefab_GameObject;
					break;
				case 10:
					asset = RESOURCE.EffectKittyPawTrailPrefab_GameObject;
					break;
				case 11:
					asset = RESOURCE.EffectAnniversaryTrailPrefab_GameObject;
					break;
				case 12:
					goto IL_186;
				default:
					goto IL_186;
				}
				return ResourceManager.GetGameObject(asset);
			}
		}
		IL_186:
		return null;
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0003ADA4 File Offset: 0x000391A4
	public static Hashtable GetUpdatedData(Hashtable _customisationHashtable = null)
	{
		if (!PsCustomisationManager.m_dirty)
		{
			return _customisationHashtable;
		}
		if (_customisationHashtable == null)
		{
			_customisationHashtable = new Hashtable();
		}
		_customisationHashtable.Add("CharacterVisual", PsCustomisationManager.m_characterCustomisationData.GetUnlockAndInstallationData());
		_customisationHashtable.Add("OffroadCarVisual", PsCustomisationManager.m_vehicleCustomisationDatas[typeof(OffroadCar).ToString()].GetUnlockAndInstallationData());
		_customisationHashtable.Add("MotorcycleVisual", PsCustomisationManager.m_vehicleCustomisationDatas[typeof(Motorcycle).ToString()].GetUnlockAndInstallationData());
		PsCustomisationManager.m_dirty = false;
		return _customisationHashtable;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0003AE38 File Offset: 0x00039238
	public static PsCustomisationData GetCustomData(List<string> _customisations)
	{
		PsCustomisationData psCustomisationData = new PsCustomisationData(PsCustomisationManager.GetDefaultUpgradeItems().ToArray());
		for (int i = 0; i < psCustomisationData.m_customisationItems.Length; i++)
		{
			PsCustomisationItem psCustomisationItem = psCustomisationData.m_customisationItems[i];
			if (_customisations.Contains(psCustomisationItem.m_identifier))
			{
				psCustomisationItem.m_unlocked = true;
				psCustomisationItem.m_installed = true;
			}
		}
		return psCustomisationData;
	}

	// Token: 0x04000600 RID: 1536
	public const string MOTORCYCLE_KEY = "MotorcycleVisual";

	// Token: 0x04000601 RID: 1537
	public const string OFFROAD_CAR_KEY = "OffroadCarVisual";

	// Token: 0x04000602 RID: 1538
	public const string CHARACTER_KEY = "CharacterVisual";

	// Token: 0x04000603 RID: 1539
	private static bool m_dirty;

	// Token: 0x04000604 RID: 1540
	private static Dictionary<string, PsCustomisationData> m_vehicleCustomisationDatas;

	// Token: 0x04000605 RID: 1541
	private static PsCustomisationData m_characterCustomisationData;

	// Token: 0x04000606 RID: 1542
	public static bool m_updated;

	// Token: 0x04000607 RID: 1543
	private static bool m_oldFormat;

	// Token: 0x020000D9 RID: 217
	public enum CustomisationCategory
	{
		// Token: 0x0400060D RID: 1549
		PRIMARY_COLOR,
		// Token: 0x0400060E RID: 1550
		PATTERN_COLOR,
		// Token: 0x0400060F RID: 1551
		PATTERN,
		// Token: 0x04000610 RID: 1552
		HAT,
		// Token: 0x04000611 RID: 1553
		TRAIL
	}
}
