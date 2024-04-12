using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class PsUpgradeManager
{
	// Token: 0x0600051C RID: 1308 RVA: 0x0004318C File Offset: 0x0004158C
	static PsUpgradeManager()
	{
		PsUpgradeManager.Initialize();
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x0004349C File Offset: 0x0004189C
	public static void UpdateVehicleDatas()
	{
		List<PsUpgradeData> list = new List<PsUpgradeData>(PsUpgradeManager.m_vehicleUpgradeDatas.Values);
		for (int i = 0; i < list.Count; i++)
		{
			list[i].UpdateValues();
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x000434DC File Offset: 0x000418DC
	public static void SetData(Dictionary<string, object> _data)
	{
		if (_data.ContainsKey("OffroadCarUpgrades"))
		{
			PsUpgradeManager.m_offroadCarUpgrades = ClientTools.ParseDictionaryObjectToObscuredInt(_data["OffroadCarUpgrades"] as Dictionary<string, object>);
		}
		else
		{
			PsUpgradeManager.m_offroadCarUpgrades = new Dictionary<string, ObscuredInt>();
		}
		PsUpgradeManager.m_offroadCarUpgradesIsDirty = false;
		PsUpgradeData psUpgradeData = PsUpgradeManager.m_vehicleUpgradeDatas[typeof(OffroadCar).ToString()];
		for (int i = 0; i < psUpgradeData.m_upgradeItems.Length; i++)
		{
			if (PsUpgradeManager.m_offroadCarUpgrades.ContainsKey(psUpgradeData.m_upgradeItems[i].m_identifier))
			{
				psUpgradeData.m_upgradeItems[i].SetCurrentLevel(PsUpgradeManager.m_offroadCarUpgrades[psUpgradeData.m_upgradeItems[i].m_identifier]);
			}
		}
		psUpgradeData.UpdateValues();
		if (_data.ContainsKey("MotorcycleUpgrades"))
		{
			PsUpgradeManager.m_motorcycleUpgrades = ClientTools.ParseDictionaryObjectToObscuredInt(_data["MotorcycleUpgrades"] as Dictionary<string, object>);
		}
		else
		{
			PsUpgradeManager.m_motorcycleUpgrades = new Dictionary<string, ObscuredInt>();
		}
		PsUpgradeManager.m_motorcycleUpgradesIsDirty = false;
		PsUpgradeData psUpgradeData2 = PsUpgradeManager.m_vehicleUpgradeDatas[typeof(Motorcycle).ToString()];
		for (int j = 0; j < psUpgradeData2.m_upgradeItems.Length; j++)
		{
			if (PsUpgradeManager.m_motorcycleUpgrades.ContainsKey(psUpgradeData2.m_upgradeItems[j].m_identifier))
			{
				psUpgradeData2.m_upgradeItems[j].SetCurrentLevel(PsUpgradeManager.m_motorcycleUpgrades[psUpgradeData2.m_upgradeItems[j].m_identifier]);
			}
		}
		psUpgradeData2.UpdateValues();
		if (_data.ContainsKey("UpgradesResources"))
		{
			PsUpgradeManager.m_upgradeResources = ClientTools.ParseDictionaryObjectToObscuredInt(_data["UpgradesResources"] as Dictionary<string, object>);
		}
		else
		{
			PsUpgradeManager.m_upgradeResources = new Dictionary<string, ObscuredInt>();
		}
		PsUpgradeManager.m_upgradeResourcesIsDirty = false;
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x000436A4 File Offset: 0x00041AA4
	public static void Initialize()
	{
		PsUpgradeManager.m_vehicleUpgradeDatas = new Dictionary<string, PsUpgradeData>();
		List<PsUpgradeItem> defaultUpgradeItems = PsUpgradeManager.GetDefaultUpgradeItems(typeof(Motorcycle));
		PsUpgradeData psUpgradeData = new PsUpgradeData(64f, 6, new float[] { 0f, 1f, 2f, 3f, 4f, 5f }, defaultUpgradeItems.ToArray());
		PsUpgradeManager.m_vehicleUpgradeDatas.Add(typeof(Motorcycle).ToString(), psUpgradeData);
		List<PsUpgradeItem> defaultUpgradeItems2 = PsUpgradeManager.GetDefaultUpgradeItems(typeof(OffroadCar));
		PsUpgradeData psUpgradeData2 = new PsUpgradeData(64f, 6, new float[] { 0f, 1f, 2f, 3f, 4f, 5f }, defaultUpgradeItems2.ToArray());
		PsUpgradeManager.m_vehicleUpgradeDatas.Add(typeof(OffroadCar).ToString(), psUpgradeData2);
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00043758 File Offset: 0x00041B58
	private static List<PsUpgradeItem> GetDefaultUpgradeItems(Type _vehicleType)
	{
		List<PsUpgradeItem> list = new List<PsUpgradeItem>();
		string text = "#ffd200";
		string text2 = "#0cdddb";
		string text3 = "#fc539e";
		string empty = string.Empty;
		if (_vehicleType == typeof(OffroadCar))
		{
			ObscuredInt[] array = PsUpgradeManager.m_CommonUpgradePrice;
			float[] array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			int[] array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("CarSpeed1", 1, StringID.O_SPEED1, StringID.O_SPEED1_DESC, StringID.SPEED, text, 3, "menu_upgrade_car_1a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("CarSpeed2", 2, StringID.O_SPEED2, StringID.O_SPEED2_DESC, StringID.SPEED, text, 5, "menu_upgrade_car_2a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Rare));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("CarSpeed3", 3, StringID.O_SPEED3, StringID.O_SPEED3_DESC, StringID.SPEED, text, 7, "menu_upgrade_car_3a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Epic));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("CarSpeed4", 4, StringID.O_SPEED4, StringID.O_SPEED4_DESC, StringID.SPEED, text, 3, "menu_upgrade_car_4a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("CarSpeed5", 5, StringID.O_SPEED5, StringID.O_SPEED5_DESC, StringID.SPEED, text, 5, "menu_upgrade_car_5a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Rare));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("CarSpeed6", 6, StringID.O_SPEED6, StringID.O_SPEED6_DESC, StringID.SPEED, text, 7, "menu_upgrade_car_6a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Epic));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("CarGrip1", 1, StringID.O_GRIP1, StringID.O_GRIP1_DESC, StringID.GRIP, text2, 5, "menu_upgrade_car_1b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Rare));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("CarGrip2", 2, StringID.O_GRIP2, StringID.O_GRIP2_DESC, StringID.GRIP, text2, 3, "menu_upgrade_car_2b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("CarGrip3", 3, StringID.O_GRIP3, StringID.O_GRIP3_DESC, StringID.GRIP, text2, 5, "menu_upgrade_car_3b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Rare));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("CarGrip4", 4, StringID.O_GRIP4, StringID.O_GRIP4_DESC, StringID.GRIP, text2, 7, "menu_upgrade_car_4b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Epic));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("CarGrip5", 5, StringID.O_GRIP5, StringID.O_GRIP5_DESC, StringID.GRIP, text2, 3, "menu_upgrade_car_5b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Common));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("CarGrip6", 6, StringID.O_GRIP6, StringID.O_GRIP6_DESC, StringID.GRIP, text2, 7, "menu_upgrade_car_6b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Epic));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("CarHandling1", 1, StringID.O_HANDLING1, StringID.O_HANDLING1_DESC, StringID.HANDLING, text3, 3, "menu_upgrade_car_1c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("CarHandling2", 2, StringID.O_HANDLING2, StringID.O_HANDLING2_DESC, StringID.HANDLING, text3, 5, "menu_upgrade_car_2c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Rare));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("CarHandling3", 3, StringID.O_HANDLING3, StringID.O_HANDLING3_DESC, StringID.HANDLING, text3, 3, "menu_upgrade_car_3c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("CarHandling4", 4, StringID.O_HANDLING4, StringID.O_HANDLING4_DESC, StringID.HANDLING, text3, 5, "menu_upgrade_car_4c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Rare));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("CarHandling5", 5, StringID.O_HANDLING5, StringID.O_HANDLING5_DESC, StringID.HANDLING, text3, 7, "menu_upgrade_car_5c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Epic));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("CarHandling6", 6, StringID.O_HANDLING6, StringID.O_HANDLING6_DESC, StringID.HANDLING, text3, 7, "menu_upgrade_car_6c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Epic));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("CarSpecial1", 1, StringID.SPECIAL1, StringID.SPECIAL1_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_car_1d", array, array2, array3, PsUpgradeManager.UpgradeType.FLIP_BOOST_POWER, true, PsRarity.Epic));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("CarSpecial2", 2, StringID.SPECIAL2, StringID.SPECIAL2_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_car_2d", array, array2, array3, PsUpgradeManager.UpgradeType.NITRO_BOOST_POWER, true, PsRarity.Epic));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f,
				1f, 1f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("CarSpecial3", 3, StringID.SPECIAL3, StringID.SPECIAL3_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_car_3d", array, array2, array3, PsUpgradeManager.UpgradeType.NITRO_BOOST_COUNT, true, PsRarity.Common));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				17f, 17f, 17f, 17f, 17f, 17f, 17f, 17f, 17f, 17f,
				17f, 17f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("CarSpecial4", 4, StringID.SPECIAL4, StringID.SPECIAL4_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_car_4d", array, array2, array3, PsUpgradeManager.UpgradeType.COIN_MAGNET, true, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("CarSpecial5", 5, StringID.SPECIAL5, StringID.SPECIAL5_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_car_5d", array, array2, array3, PsUpgradeManager.UpgradeType.NITRO_BOOST_DURATION, true, PsRarity.Rare));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("CarSpecial6", 6, StringID.SPECIAL6, StringID.SPECIAL6_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_car_6d", array, array2, array3, PsUpgradeManager.UpgradeType.FLIP_BOOST_DURATION, true, PsRarity.Rare));
		}
		else if (_vehicleType == typeof(Motorcycle))
		{
			ObscuredInt[] array = PsUpgradeManager.m_CommonUpgradePrice;
			float[] array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			int[] array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("Speed1", 1, StringID.D_SPEED1, StringID.D_SPEED1_DESC, StringID.SPEED, text, 3, "menu_upgrade_bike_1a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("Speed2", 2, StringID.D_SPEED2, StringID.D_SPEED2_DESC, StringID.SPEED, text, 5, "menu_upgrade_bike_2a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Rare));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("Speed3", 3, StringID.D_SPEED3, StringID.D_SPEED3_DESC, StringID.SPEED, text, 7, "menu_upgrade_bike_3a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Epic));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("Speed4", 4, StringID.D_SPEED4, StringID.D_SPEED4_DESC, StringID.SPEED, text, 3, "menu_upgrade_bike_4a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("Speed5", 5, StringID.D_SPEED5, StringID.D_SPEED5_DESC, StringID.SPEED, text, 5, "menu_upgrade_bike_5a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Rare));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("Speed6", 6, StringID.D_SPEED6, StringID.D_SPEED6_DESC, StringID.SPEED, text, 7, "menu_upgrade_bike_6a", array, array2, array3, PsUpgradeManager.UpgradeType.SPEED, false, PsRarity.Epic));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("Grip1", 1, StringID.D_GRIP1, StringID.D_GRIP1_DESC, StringID.GRIP, text2, 5, "menu_upgrade_bike_1b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Rare));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("Grip2", 2, StringID.D_GRIP2, StringID.D_GRIP2_DESC, StringID.GRIP, text2, 3, "menu_upgrade_bike_2b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("Grip3", 3, StringID.D_GRIP3, StringID.D_GRIP3_DESC, StringID.GRIP, text2, 5, "menu_upgrade_bike_3b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Rare));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("Grip4", 4, StringID.D_GRIP4, StringID.D_GRIP4_DESC, StringID.GRIP, text2, 7, "menu_upgrade_bike_4b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Epic));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("Grip5", 5, StringID.D_GRIP5, StringID.D_GRIP5_DESC, StringID.GRIP, text2, 3, "menu_upgrade_bike_5b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Common));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("Grip6", 6, StringID.D_GRIP6, StringID.D_GRIP6_DESC, StringID.GRIP, text2, 7, "menu_upgrade_bike_6b", array, array2, array3, PsUpgradeManager.UpgradeType.GRIP, false, PsRarity.Epic));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("Handling1", 1, StringID.D_HANDLING1, StringID.D_HANDLING1_DESC, StringID.HANDLING, text3, 3, "menu_upgrade_bike_1c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("Handling2", 2, StringID.D_HANDLING2, StringID.D_HANDLING2_DESC, StringID.HANDLING, text3, 5, "menu_upgrade_bike_2c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Rare));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f,
				3f, 3f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("Handling3", 3, StringID.D_HANDLING3, StringID.D_HANDLING3_DESC, StringID.HANDLING, text3, 3, "menu_upgrade_bike_3c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("Handling4", 4, StringID.D_HANDLING4, StringID.D_HANDLING4_DESC, StringID.HANDLING, text3, 5, "menu_upgrade_bike_4c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Rare));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("Handling5", 5, StringID.D_HANDLING5, StringID.D_HANDLING5_DESC, StringID.HANDLING, text3, 7, "menu_upgrade_bike_5c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Epic));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("Handling6", 6, StringID.D_HANDLING6, StringID.D_HANDLING6_DESC, StringID.HANDLING, text3, 7, "menu_upgrade_bike_6c", array, array2, array3, PsUpgradeManager.UpgradeType.HANDLING, false, PsRarity.Epic));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("Special1", 1, StringID.SPECIAL1, StringID.SPECIAL1_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_bike_1d", array, array2, array3, PsUpgradeManager.UpgradeType.FLIP_BOOST_POWER, true, PsRarity.Epic));
			array = PsUpgradeManager.m_EpicUpgradePrice;
			array2 = new float[] { 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f };
			array3 = PsUpgradeManager.m_EpicCardAmounts;
			list.Add(new PsUpgradeItem("Special2", 2, StringID.SPECIAL2, StringID.SPECIAL2_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_bike_2d", array, array2, array3, PsUpgradeManager.UpgradeType.NITRO_BOOST_POWER, true, PsRarity.Epic));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f,
				1f, 1f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("Special3", 3, StringID.SPECIAL3, StringID.SPECIAL3_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_bike_3d", array, array2, array3, PsUpgradeManager.UpgradeType.NITRO_BOOST_COUNT, true, PsRarity.Common));
			array = PsUpgradeManager.m_CommonUpgradePrice;
			array2 = new float[]
			{
				17f, 17f, 17f, 17f, 17f, 17f, 17f, 17f, 17f, 17f,
				17f, 17f
			};
			array3 = PsUpgradeManager.m_CommonCardAmounts;
			list.Add(new PsUpgradeItem("Special4", 4, StringID.SPECIAL4, StringID.SPECIAL4_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_bike_4d", array, array2, array3, PsUpgradeManager.UpgradeType.COIN_MAGNET, true, PsRarity.Common));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("Special5", 5, StringID.SPECIAL5, StringID.SPECIAL5_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_bike_5d", array, array2, array3, PsUpgradeManager.UpgradeType.NITRO_BOOST_DURATION, true, PsRarity.Rare));
			array = PsUpgradeManager.m_RareUpgradePrice;
			array2 = new float[] { 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
			array3 = PsUpgradeManager.m_RareCardAmounts;
			list.Add(new PsUpgradeItem("Special6", 6, StringID.SPECIAL6, StringID.SPECIAL6_DESC, StringID.SPECIAL, empty, 1, "menu_upgrade_bike_6d", array, array2, array3, PsUpgradeManager.UpgradeType.FLIP_BOOST_DURATION, true, PsRarity.Rare));
		}
		return list;
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00044610 File Offset: 0x00042A10
	public static int GetPriceForUpgrade(ShopUpgradeItemData _item)
	{
		int num = 10;
		int num2 = 100;
		Type type = typeof(Motorcycle);
		if (_item.m_identifier.StartsWith("Car"))
		{
			type = typeof(OffroadCar);
		}
		PsUpgradeItem upgradeItem = PsUpgradeManager.GetUpgradeItem(type, _item.m_identifier);
		if (upgradeItem.m_rarity == PsRarity.Rare)
		{
			num = 100;
			num2 = 50;
		}
		else if (upgradeItem.m_rarity == PsRarity.Epic)
		{
			num = 3000;
			num2 = 10;
		}
		if (_item.m_purchaseCount >= num2)
		{
			return -1;
		}
		float num3 = (float)num;
		num3 = (float)((_item.m_purchaseCount + 1) * num);
		EventGiftTimedUpgradeItemCardDiscount activeEventGift = PsMetagameManager.GetActiveEventGift<EventGiftTimedUpgradeItemCardDiscount>();
		if (activeEventGift != null)
		{
			num3 *= activeEventGift.GetPriceMultiplier();
		}
		return Mathf.RoundToInt(num3);
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x000446C7 File Offset: 0x00042AC7
	public static int GetUpgradeResourceCount(string _identifier)
	{
		if (PsUpgradeManager.m_upgradeResources.ContainsKey(_identifier))
		{
			return PsUpgradeManager.m_upgradeResources[_identifier];
		}
		return 0;
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x000446EC File Offset: 0x00042AEC
	public static bool IsDiscovered(string _identifier)
	{
		return PsUpgradeManager.m_upgradeResources.ContainsKey(_identifier) || PsUpgradeManager.m_motorcycleUpgrades.ContainsKey(_identifier);
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0004471C File Offset: 0x00042B1C
	public static void IncreaseResources(Dictionary<string, int> _upgradeResources)
	{
		if (_upgradeResources.Count > 0)
		{
			List<string> list = new List<string>(_upgradeResources.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				PsUpgradeManager.IncreaseResources(list[i], _upgradeResources[list[i]]);
			}
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00044774 File Offset: 0x00042B74
	public static void IncreaseResources(string _itemIdentifier, int _count)
	{
		if (_count > 0)
		{
			if (PsUpgradeManager.m_upgradeResources.ContainsKey(_itemIdentifier))
			{
				Dictionary<string, ObscuredInt> upgradeResources;
				(upgradeResources = PsUpgradeManager.m_upgradeResources)[_itemIdentifier] = upgradeResources[_itemIdentifier] + _count;
			}
			else
			{
				PsUpgradeManager.m_upgradeResources.Add(_itemIdentifier, _count);
			}
			PsUpgradeManager.m_upgradeResourcesIsDirty = true;
		}
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x000447D8 File Offset: 0x00042BD8
	public static float GetNormalizedUpgradeValue(Type _vehicleType, PsUpgradeManager.UpgradeType _upgradeType)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData) && psUpgradeData.m_typeDatas.ContainsKey(_upgradeType))
		{
			return psUpgradeData.m_typeDatas[_upgradeType].m_currentEfficiency / psUpgradeData.m_typeDatas[_upgradeType].m_maxEfficiency;
		}
		return 0f;
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x0004483C File Offset: 0x00042C3C
	public static float GetItemNextNormalizedEfficiency(Type _vehicleType, string _itemIdentifier)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			PsUpgradeItem upgradeItemByIdentifier = psUpgradeData.GetUpgradeItemByIdentifier(_itemIdentifier);
			return upgradeItemByIdentifier.m_nextLevelEfficiency / psUpgradeData.m_typeDatas[upgradeItemByIdentifier.m_upgradeType].m_maxEfficiency;
		}
		return 0f;
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00044890 File Offset: 0x00042C90
	public static float GetMaxEfficiency(Type _vehicleType, PsUpgradeManager.UpgradeType _upgradeType)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData) && psUpgradeData.m_typeDatas.ContainsKey(_upgradeType))
		{
			return psUpgradeData.m_typeDatas[_upgradeType].m_maxEfficiency;
		}
		return 0f;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x000448E0 File Offset: 0x00042CE0
	public static float GetMaxPerformance(Type _vehicleType)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			return psUpgradeData.m_maxPerformance;
		}
		return 0f;
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00044910 File Offset: 0x00042D10
	public static float GetCurrentEfficiency(Type _vehicleType, PsUpgradeManager.UpgradeType _upgradeType)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData) && psUpgradeData.m_typeDatas.ContainsKey(_upgradeType))
		{
			return psUpgradeData.m_typeDatas[_upgradeType].m_currentEfficiency;
		}
		return 0f;
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x00044960 File Offset: 0x00042D60
	public static float GetCurrentPerformance(Type _vehicleType)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			return psUpgradeData.m_currentPerformance + psUpgradeData.m_basePerformance;
		}
		return 0f;
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x00044998 File Offset: 0x00042D98
	public static int GetUpgredeLevel(Type _vehicleType)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			return Mathf.RoundToInt(psUpgradeData.m_currentPerformance / (psUpgradeData.m_maxPerformance - psUpgradeData.m_basePerformance) * 100f);
		}
		return 0;
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x000449E0 File Offset: 0x00042DE0
	public static float GetBasePerformance(Type _vehicleType)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			return psUpgradeData.m_basePerformance;
		}
		return 0f;
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00044A10 File Offset: 0x00042E10
	public static float GetItemNextPerformanceEfficiency(Type _vehicleType, string _itemIdentifier)
	{
		PsUpgradeItem upgradeItem = PsUpgradeManager.GetUpgradeItem(_vehicleType, _itemIdentifier);
		if (upgradeItem == null || upgradeItem.m_powerUpItem)
		{
			return 0f;
		}
		return upgradeItem.m_nextLevelEfficiency;
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x00044A44 File Offset: 0x00042E44
	public static PsUpgradeData GetVehicleUpgradeData(Type _vehicleType)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			return psUpgradeData;
		}
		return null;
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00044A6C File Offset: 0x00042E6C
	public static PsUpgradeItem GetUpgradeItem(Type _vehicleType, string _itemIdentifier)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			return psUpgradeData.GetUpgradeItemByIdentifier(_itemIdentifier);
		}
		return null;
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00044A9C File Offset: 0x00042E9C
	public static int GetItemNextPrice(Type _vehicleType, string _itemIdentifier)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			PsUpgradeItem upgradeItemByIdentifier = psUpgradeData.GetUpgradeItemByIdentifier(_itemIdentifier);
			if (upgradeItemByIdentifier != null)
			{
				return upgradeItemByIdentifier.m_nextLevelPrice;
			}
		}
		return -1;
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00044ADC File Offset: 0x00042EDC
	public static int GetPowerUpItemsCurrentPerformance(Type _vehicleType)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			return psUpgradeData.GetPowerUpItemsCurrentPerformance();
		}
		return 0;
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00044B08 File Offset: 0x00042F08
	public static bool IsTierUnlocked(int _tier, Type _vehicleType)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			if (_vehicleType == typeof(Motorcycle))
			{
				return (float)PsMetagameManager.m_playerStats.mcRank >= psUpgradeData.m_tierLimits[_tier - 1];
			}
			if (_vehicleType == typeof(OffroadCar))
			{
				return (float)PsMetagameManager.m_playerStats.carRank >= psUpgradeData.m_tierLimits[_tier - 1];
			}
		}
		return false;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00044B84 File Offset: 0x00042F84
	public static int GetUpgradeableItemCount(Type _vehicleType)
	{
		int num = 0;
		List<string> list = new List<string>(PsUpgradeManager.m_upgradeResources.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			if (PsUpgradeManager.m_upgradeResources[list[i]] > 0)
			{
				PsUpgradeItem upgradeItem = PsUpgradeManager.GetUpgradeItem(_vehicleType, list[i]);
				if (upgradeItem != null && upgradeItem.m_nextLevelResourceRequrement <= PsUpgradeManager.m_upgradeResources[list[i]] && upgradeItem.m_currentLevel < upgradeItem.m_maxLevel && PsUpgradeManager.IsTierUnlocked(upgradeItem.m_tier, _vehicleType))
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00044C38 File Offset: 0x00043038
	public static bool PurchaseUpgradeItem(Type _vehicleType, string _identifier, bool _withDiamonds)
	{
		PsUpgradeData psUpgradeData;
		if (PsUpgradeManager.m_vehicleUpgradeDatas.TryGetValue(_vehicleType.ToString(), ref psUpgradeData))
		{
			for (int i = 0; i < psUpgradeData.m_upgradeItems.Length; i++)
			{
				if (_identifier == psUpgradeData.m_upgradeItems[i].m_identifier && psUpgradeData.m_upgradeItems[i].m_currentLevel < psUpgradeData.m_upgradeItems[i].m_maxLevel)
				{
					int num;
					if (psUpgradeData.m_upgradeItems[i].m_powerUpItem)
					{
						num = psUpgradeData.m_upgradeItems[i].GetPowerUpItemPerformance();
					}
					else
					{
						num = (int)psUpgradeData.m_upgradeItems[i].m_nextLevelEfficiency;
					}
					if (_vehicleType == typeof(Motorcycle))
					{
						PsAchievementManager.IncrementProgress("reachHundredHPBike", num);
						PsAchievementManager.IncrementProgress("reachFiveHundredHPBike", num);
					}
					else if (_vehicleType == typeof(OffroadCar))
					{
						PsAchievementManager.IncrementProgress("reachHundredHPCar", num);
					}
					List<Dictionary<string, object>> updatedAchievements = PsAchievementManager.GetUpdatedAchievements(false);
					if (_withDiamonds)
					{
						int num2 = psUpgradeData.m_upgradeItems[i].m_nextLevelPrice - PsMetagameManager.m_playerStats.coins;
						int coins = PsMetagameManager.m_playerStats.coins;
						int num3 = PsMetagameManager.CoinsToDiamonds(num2, true);
						PsMetagameManager.m_playerStats.CumulateCoins(-PsMetagameManager.m_playerStats.coins);
						PsMetagameManager.m_playerStats.CumulateDiamonds(-PsMetagameManager.CoinsToDiamonds(num2, true));
						PsMetrics.VehicleUpgrade(_vehicleType.ToString(), _identifier, psUpgradeData.m_upgradeItems[i].m_nextLevelPrice, psUpgradeData.m_upgradeItems[i].m_nextLevelEfficiency);
						FrbMetrics.SpendVirtualCurrency("upgrade", "coins", (double)coins);
						FrbMetrics.SpendVirtualCurrency("coin_substitute", "gems", (double)num3);
					}
					else
					{
						int num4 = psUpgradeData.m_upgradeItems[i].m_nextLevelPrice;
						PsMetagameManager.m_playerStats.CumulateCoins(-psUpgradeData.m_upgradeItems[i].m_nextLevelPrice);
						PsMetrics.VehicleUpgrade(_vehicleType.ToString(), _identifier, psUpgradeData.m_upgradeItems[i].m_nextLevelPrice, psUpgradeData.m_upgradeItems[i].m_nextLevelEfficiency);
						FrbMetrics.SpendVirtualCurrency("upgrade", "coins", (double)num4);
					}
					Dictionary<string, ObscuredInt> dictionary;
					string identifier;
					(dictionary = PsUpgradeManager.m_upgradeResources)[identifier = psUpgradeData.m_upgradeItems[i].m_identifier] = dictionary[identifier] - psUpgradeData.m_upgradeItems[i].m_nextLevelResourceRequrement;
					PsUpgradeManager.m_upgradeResourcesIsDirty = true;
					bool flag = false;
					if (_vehicleType == typeof(OffroadCar))
					{
						flag = PsUpgradeManager.GetCurrentPerformance(typeof(OffroadCar)) < 250f;
						if (PsUpgradeManager.m_offroadCarUpgrades.ContainsKey(_identifier))
						{
							(dictionary = PsUpgradeManager.m_offroadCarUpgrades)[_identifier] = ObscuredInt.op_Increment(dictionary[_identifier]);
						}
						else
						{
							PsUpgradeManager.m_offroadCarUpgrades.Add(_identifier, 1);
						}
						BossBattles.AlterHandicap(typeof(OffroadCar), BossBattles.upgradeVehicleChange);
						PsUpgradeManager.m_offroadCarUpgradesIsDirty = true;
					}
					else if (_vehicleType == typeof(Motorcycle))
					{
						if (PsUpgradeManager.m_motorcycleUpgrades.ContainsKey(_identifier))
						{
							(dictionary = PsUpgradeManager.m_motorcycleUpgrades)[_identifier] = ObscuredInt.op_Increment(dictionary[_identifier]);
						}
						else
						{
							PsUpgradeManager.m_motorcycleUpgrades.Add(_identifier, 1);
						}
						BossBattles.AlterHandicap(typeof(Motorcycle), BossBattles.upgradeVehicleChange);
						PsUpgradeManager.m_motorcycleUpgradesIsDirty = true;
					}
					psUpgradeData.m_upgradeItems[i].LevelUp();
					psUpgradeData.UpdateValues();
					if (flag && PsUpgradeManager.GetCurrentPerformance(typeof(OffroadCar)) >= 250f)
					{
						PsState.m_showMcDialogue = true;
						FrbMetrics.TrackMotorcycleUnlocked();
					}
					PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), new Hashtable(), PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().GetIdentifier(), false);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00044FFC File Offset: 0x000433FC
	public static Hashtable GetUpdatedData(Hashtable _customisationHashtable = null)
	{
		if (!PsUpgradeManager.m_offroadCarUpgradesIsDirty && !PsUpgradeManager.m_motorcycleUpgradesIsDirty && !PsUpgradeManager.m_upgradeResourcesIsDirty)
		{
			return _customisationHashtable;
		}
		if (_customisationHashtable == null)
		{
			_customisationHashtable = new Hashtable();
		}
		if (PsUpgradeManager.m_offroadCarUpgradesIsDirty)
		{
			_customisationHashtable.Add("offroadCarLevel", PsUpgradeManager.GetUpgredeLevel(typeof(OffroadCar)));
			_customisationHashtable.Add("OffroadCarUpgrades", PsUpgradeManager.m_offroadCarUpgrades);
			PsUpgradeManager.m_offroadCarUpgradesIsDirty = false;
		}
		if (PsUpgradeManager.m_motorcycleUpgradesIsDirty)
		{
			_customisationHashtable.Add("motorcycleLevel", PsUpgradeManager.GetUpgredeLevel(typeof(Motorcycle)));
			_customisationHashtable.Add("MotorcycleUpgrades", PsUpgradeManager.m_motorcycleUpgrades);
			PsUpgradeManager.m_motorcycleUpgradesIsDirty = false;
		}
		if (PsUpgradeManager.m_upgradeResourcesIsDirty)
		{
			_customisationHashtable.Add("UpgradesResources", PsUpgradeManager.m_upgradeResources);
			PsUpgradeManager.m_upgradeResourcesIsDirty = false;
		}
		return _customisationHashtable;
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x000450D8 File Offset: 0x000434D8
	public static PsUpgradeData GetCustomUpgradeData(Dictionary<string, ObscuredInt> _upgrades, Type _vehicleType)
	{
		List<PsUpgradeItem> defaultUpgradeItems = PsUpgradeManager.GetDefaultUpgradeItems(_vehicleType);
		PsUpgradeData psUpgradeData = new PsUpgradeData(64f, 6, new float[] { 0f, 1f, 2f, 3f, 4f, 5f }, defaultUpgradeItems.ToArray());
		for (int i = 0; i < psUpgradeData.m_upgradeItems.Length; i++)
		{
			if (_upgrades.ContainsKey(psUpgradeData.m_upgradeItems[i].m_identifier))
			{
				psUpgradeData.m_upgradeItems[i].SetCurrentLevel(_upgrades[psUpgradeData.m_upgradeItems[i].m_identifier]);
			}
		}
		psUpgradeData.UpdateValues();
		return psUpgradeData;
	}

	// Token: 0x04000684 RID: 1668
	public const int COMMON_ITEM_PERFORMANCE = 3;

	// Token: 0x04000685 RID: 1669
	public const int RARE_ITEM_PERFORMANCE = 5;

	// Token: 0x04000686 RID: 1670
	public const int EPIC_ITEM_PERFORMANCE = 7;

	// Token: 0x04000687 RID: 1671
	private static ObscuredInt[] m_CommonUpgradePrice = new ObscuredInt[]
	{
		30, 100, 200, 300, 400, 1000, 2000, 4000, 8000, 20000,
		50000, 100000
	};

	// Token: 0x04000688 RID: 1672
	private static ObscuredInt[] m_RareUpgradePrice = new ObscuredInt[] { 50, 300, 400, 1000, 2000, 4000, 8000, 20000, 50000, 100000 };

	// Token: 0x04000689 RID: 1673
	private static ObscuredInt[] m_EpicUpgradePrice = new ObscuredInt[] { 70, 1000, 2000, 4000, 8000, 20000, 50000, 100000 };

	// Token: 0x0400068A RID: 1674
	private static int[] m_CommonCardAmounts = new int[]
	{
		1, 2, 4, 10, 20, 40, 80, 150, 200, 300,
		500, 1000
	};

	// Token: 0x0400068B RID: 1675
	private static int[] m_RareCardAmounts = new int[] { 1, 2, 4, 10, 20, 40, 80, 150, 200, 300 };

	// Token: 0x0400068C RID: 1676
	private static int[] m_EpicCardAmounts = new int[] { 1, 2, 4, 8, 15, 20, 40, 100 };

	// Token: 0x0400068D RID: 1677
	private static Dictionary<string, PsUpgradeData> m_vehicleUpgradeDatas;

	// Token: 0x0400068E RID: 1678
	public const string MOTORCYCLE_KEY = "MotorcycleUpgrades";

	// Token: 0x0400068F RID: 1679
	public const string OFFROAD_CAR_KEY = "OffroadCarUpgrades";

	// Token: 0x04000690 RID: 1680
	public const string UPGRADE_RESOURCES_KEY = "UpgradesResources";

	// Token: 0x04000691 RID: 1681
	public static Dictionary<string, ObscuredInt> m_offroadCarUpgrades;

	// Token: 0x04000692 RID: 1682
	private static bool m_offroadCarUpgradesIsDirty;

	// Token: 0x04000693 RID: 1683
	public static Dictionary<string, ObscuredInt> m_motorcycleUpgrades;

	// Token: 0x04000694 RID: 1684
	private static bool m_motorcycleUpgradesIsDirty;

	// Token: 0x04000695 RID: 1685
	public static Dictionary<string, ObscuredInt> m_upgradeResources;

	// Token: 0x04000696 RID: 1686
	private static bool m_upgradeResourcesIsDirty;

	// Token: 0x020000EC RID: 236
	public enum UpgradeType
	{
		// Token: 0x04000698 RID: 1688
		SPEED,
		// Token: 0x04000699 RID: 1689
		GRIP,
		// Token: 0x0400069A RID: 1690
		HANDLING,
		// Token: 0x0400069B RID: 1691
		NITRO_BOOST_COUNT,
		// Token: 0x0400069C RID: 1692
		NITRO_BOOST_POWER,
		// Token: 0x0400069D RID: 1693
		NITRO_BOOST_DURATION,
		// Token: 0x0400069E RID: 1694
		FLIP_BOOST_COUNT,
		// Token: 0x0400069F RID: 1695
		FLIP_BOOST_POWER,
		// Token: 0x040006A0 RID: 1696
		FLIP_BOOST_DURATION,
		// Token: 0x040006A1 RID: 1697
		COIN_MAGNET
	}
}
