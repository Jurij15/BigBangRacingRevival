using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class PsGachaManager
{
	// Token: 0x06000370 RID: 880 RVA: 0x00032708 File Offset: 0x00030B08
	static PsGachaManager()
	{
		PsGachaManager.Initialize();
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00032720 File Offset: 0x00030B20
	public static void SetData(Dictionary<string, object> _data)
	{
		if (_data.ContainsKey("chest"))
		{
			List<Dictionary<string, object>> list = ClientTools.ParseList<Dictionary<string, object>>(_data["chest"] as List<object>);
			if (PsState.m_previousLoginVersion <= 267 && Main.CLIENT_VERSION() > 267)
			{
				PsGachaManager.m_giveConsolation = true;
			}
			if (!PsGachaManager.m_giveConsolation)
			{
				for (int i = 0; i < list.Count; i++)
				{
					bool flag = false;
					GachaData gachaData = new GachaData();
					if (list[i].ContainsKey("id"))
					{
						gachaData.id = Convert.ToInt32(list[i]["id"]);
					}
					if (list[i].ContainsKey("timeLeft"))
					{
						gachaData.timeLeft = Convert.ToInt32(list[i]["timeLeft"]);
						flag = true;
					}
					if (list[i].ContainsKey("type"))
					{
						gachaData.type = (string)list[i]["type"];
					}
					if (list[i].ContainsKey("notified"))
					{
						gachaData.notified = (bool)list[i]["notified"];
					}
					else
					{
						gachaData.notified = false;
					}
					GachaType gachaType = GachaType.BRONZE;
					if (!string.IsNullOrEmpty(gachaData.type))
					{
						gachaType = (GachaType)Enum.Parse(typeof(GachaType), gachaData.type);
					}
					PsGachaManager.m_gachas[gachaData.id] = new PsGacha(gachaType);
					if (gachaData.timeLeft > 0)
					{
						PsGachaManager.m_gachas[gachaData.id].m_unlockStartedTime = Main.m_EPOCHSeconds - (double)(PsGachaManager.m_gachas[gachaData.id].m_unlockTime - gachaData.timeLeft);
						PsGachaManager.m_gachas[gachaData.id].m_unlockTimeLeft = gachaData.timeLeft;
						PsGachaManager.m_gachas[gachaData.id].m_notified = false;
					}
					else if (flag)
					{
						PsGachaManager.m_gachas[gachaData.id].m_unlocked = true;
						PsGachaManager.m_gachas[gachaData.id].m_unlockTimeLeft = 0;
						PsGachaManager.m_gachas[gachaData.id].m_notified = gachaData.notified;
					}
				}
			}
			else
			{
				for (int j = 0; j < 2; j++)
				{
					PsGacha psGacha = new PsGacha(GachaType.GOLD);
					psGacha.m_unlocked = true;
					psGacha.m_unlockTimeLeft = 0;
					psGacha.m_unlockTime = 0;
					psGacha.m_unlockStartedTime = 0.0;
					PsGachaManager.m_gachas[j] = psGacha;
					PsGachaManager.m_gachas[j + 2] = null;
				}
				PsGachaManager.m_giveConsolation = false;
			}
		}
		PsGachaManager.m_gachaSet = true;
		PsGachaManager.m_dirty = false;
	}

	// Token: 0x06000373 RID: 883 RVA: 0x000329D8 File Offset: 0x00030DD8
	public static void Initialize()
	{
		PsGachaManager.m_upgradeChests = new Dictionary<int, GachaType>();
		PsGachaManager.m_upgradeChests.Add(100, GachaType.SILVER);
		PsGachaManager.m_upgradeChests.Add(200, GachaType.GOLD);
		PsGachaManager.m_upgradeChests.Add(300, GachaType.RARE);
		PsGachaManager.m_upgradeChests.Add(400, GachaType.SILVER);
		PsGachaManager.m_upgradeChests.Add(500, GachaType.GOLD);
		PsGachaManager.m_upgradeChests.Add(600, GachaType.EPIC);
		PsGachaManager.m_upgradeChests.Add(700, GachaType.GOLD);
		PsGachaManager.m_upgradeChests.Add(800, GachaType.RARE);
		PsGachaManager.m_upgradeChests.Add(900, GachaType.GOLD);
		PsGachaManager.m_upgradeChests.Add(1000, GachaType.EPIC);
		PsGachaManager.m_upgradeChests.Add(1100, GachaType.GOLD);
		PsGachaManager.m_upgradeChests.Add(1200, GachaType.SUPER);
		PsGachaManager.m_fixedChests = new Dictionary<int, GachaType>();
		PsGachaManager.m_fixedChests.Add(1, GachaType.WOOD);
		PsGachaManager.m_fixedChests.Add(2, GachaType.SILVER);
		PsGachaManager.m_fixedChests.Add(3, GachaType.SILVER);
		PsGachaManager.m_fixedChests.Add(4, GachaType.GOLD);
		PsGachaManager.m_fixedChests.Add(5, GachaType.SILVER);
		PsGachaManager.m_fixedChests.Add(6, GachaType.SILVER);
		PsGachaManager.m_fixedChests.Add(7, GachaType.SILVER);
		PsGachaManager.m_fixedChests.Add(8, GachaType.GOLD);
		PsGachaManager.m_fixedChests.Add(9, GachaType.SILVER);
		PsGachaManager.m_fixedChests.Add(10, GachaType.SILVER);
		PsGachaManager.m_fixedChests.Add(11, GachaType.GOLD);
		PsGachaManager.m_fixedChests.Add(12, GachaType.SILVER);
		PsGachaManager.m_fixedChests.Add(13, GachaType.GOLD);
		PsGachaManager.m_rotationChests = new List<GachaType>();
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.RARE);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.EPIC);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.SILVER);
		PsGachaManager.m_rotationChests.Add(GachaType.GOLD);
		PsGachaManager.m_superChestProbability = 0.0025f;
		PsGachaManager.m_gachas = new PsGacha[6];
		PsGachaManager.m_gachaRewardDatas = new Dictionary<GachaType, PsGachaRewardData[]>();
		PsGachaRewardData[] array = new PsGachaRewardData[]
		{
			new PsGachaRewardData(2, 0, 1, 1, 160, 190, 2, 1, 0, 1, 0, 1f, 0, -1)
		};
		PsGachaManager.m_gachaRewardDatas.Add(GachaType.WOOD, array);
		PsGachaRewardData[] array2 = new PsGachaRewardData[]
		{
			new PsGachaRewardData(3, 0, 0, 2, 30, 40, 2, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(4, 0, 0, 2, 35, 45, 2, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(5, 0, 0, 2, 40, 50, 2, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(6, 0, 0, 2, 45, 55, 2, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(7, 0, 0, 2, 50, 60, 3, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(8, 0, 0, 2, 55, 65, 3, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(9, 0, 0, 2, 60, 70, 3, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(10, 0, 0, 2, 65, 75, 3, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(11, 0, 0, 2, 70, 80, 4, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(12, 0, 0, 2, 75, 85, 4, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(13, 0, 0, 2, 80, 90, 4, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(14, 0, 0, 2, 85, 95, 4, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(15, 0, 0, 2, 90, 100, 5, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(16, 0, 0, 2, 95, 105, 5, 0, 0, 1, 0, 0.002f, 0, -1),
			new PsGachaRewardData(17, 0, 0, 2, 100, 110, 5, 0, 0, 1, 0, 0.002f, 0, -1)
		};
		PsGachaManager.m_gachaRewardDatas.Add(GachaType.COMMON, array2);
		PsGachaRewardData[] array3 = new PsGachaRewardData[]
		{
			new PsGachaRewardData(4, 0, 0, 2, 60, 70, 3, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(5, 0, 0, 2, 65, 75, 3, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(6, 0, 0, 2, 70, 80, 3, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(7, 0, 0, 2, 75, 85, 4, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(8, 0, 0, 2, 80, 90, 4, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(9, 0, 0, 2, 85, 95, 4, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(10, 0, 0, 2, 90, 100, 5, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(11, 0, 0, 2, 95, 105, 5, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(12, 0, 0, 2, 100, 110, 5, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(13, 0, 0, 2, 105, 115, 6, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(14, 0, 0, 2, 110, 120, 6, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(15, 0, 0, 2, 115, 125, 6, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(16, 0, 0, 2, 120, 130, 7, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(17, 0, 0, 2, 125, 135, 7, 0, 0, 1, 1, 0.0025f, 0, -1),
			new PsGachaRewardData(18, 0, 0, 2, 130, 140, 7, 0, 0, 1, 1, 0.0025f, 0, -1)
		};
		PsGachaManager.m_gachaRewardDatas.Add(GachaType.BRONZE, array3);
		PsGachaRewardData[] array4 = new PsGachaRewardData[]
		{
			new PsGachaRewardData(8, 0, 0, 2, 120, 150, 6, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(9, 0, 0, 2, 135, 165, 6, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(10, 0, 0, 2, 150, 180, 6, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(11, 0, 0, 2, 165, 195, 7, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(12, 0, 0, 2, 180, 210, 7, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(13, 0, 0, 2, 195, 225, 7, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(14, 0, 0, 2, 210, 240, 8, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(15, 0, 0, 2, 225, 255, 8, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(16, 0, 0, 2, 240, 270, 8, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(17, 0, 0, 2, 255, 285, 9, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(18, 0, 0, 2, 270, 300, 9, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(19, 0, 0, 2, 285, 315, 9, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(20, 0, 0, 2, 300, 330, 10, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(21, 0, 0, 2, 315, 345, 10, 0, 0, 2, 2, 0.005f, 0, -1),
			new PsGachaRewardData(22, 0, 0, 2, 330, 360, 10, 0, 0, 2, 2, 0.005f, 0, -1)
		};
		PsGachaManager.m_gachaRewardDatas.Add(GachaType.SILVER, array4);
		PsGachaRewardData[] array5 = new PsGachaRewardData[]
		{
			new PsGachaRewardData(16, 1, 0, 4, 240, 300, 10, 1, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(17, 1, 0, 4, 270, 330, 10, 1, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(18, 1, 0, 4, 300, 360, 11, 1, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(19, 1, 0, 4, 330, 390, 11, 1, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(20, 1, 0, 4, 360, 420, 12, 1, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(21, 1, 0, 4, 390, 450, 12, 1, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(22, 2, 0, 4, 420, 480, 13, 2, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(23, 2, 0, 4, 450, 510, 13, 2, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(24, 2, 0, 4, 480, 540, 14, 2, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(25, 2, 0, 4, 510, 570, 14, 2, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(26, 2, 0, 4, 540, 600, 15, 1, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(27, 2, 0, 4, 570, 630, 15, 2, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(28, 2, 0, 4, 600, 660, 16, 2, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(29, 2, 0, 4, 630, 690, 16, 2, 0, 2, 3, 0.0075f, 0, -1),
			new PsGachaRewardData(30, 2, 0, 4, 660, 720, 17, 2, 0, 2, 3, 0.0075f, 0, -1)
		};
		PsGachaManager.m_gachaRewardDatas.Add(GachaType.GOLD, array5);
		PsGachaRewardData[] array6 = new PsGachaRewardData[]
		{
			new PsGachaRewardData(30, 10, 0, 5, 700, 900, 30, 10, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(36, 12, 0, 5, 800, 1000, 36, 12, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(42, 14, 0, 5, 900, 1100, 42, 14, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(48, 16, 0, 5, 1000, 1200, 48, 16, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(54, 18, 0, 5, 1100, 1300, 54, 18, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(60, 20, 0, 5, 1200, 1400, 60, 20, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(66, 22, 0, 5, 1300, 1500, 66, 22, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(72, 24, 0, 5, 1400, 1600, 72, 24, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(78, 26, 0, 5, 1500, 1700, 78, 26, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(84, 28, 0, 5, 1600, 1800, 84, 28, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(90, 30, 0, 5, 1700, 1900, 90, 30, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(96, 32, 0, 5, 1800, 2000, 96, 32, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(102, 34, 0, 5, 1900, 2100, 102, 34, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(108, 36, 0, 5, 2000, 2200, 108, 36, 0, 3, 8, 1f, 0, -1),
			new PsGachaRewardData(114, 38, 0, 5, 2100, 2300, 114, 38, 0, 3, 8, 1f, 0, -1)
		};
		PsGachaManager.m_gachaRewardDatas.Add(GachaType.RARE, array6);
		PsGachaRewardData[] array7 = new PsGachaRewardData[]
		{
			new PsGachaRewardData(50, 4, 3, 9, 900, 1300, 50, 4, 3, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(58, 5, 3, 9, 1100, 1500, 58, 5, 3, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(66, 6, 3, 9, 1300, 1700, 66, 6, 3, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(74, 7, 3, 9, 1500, 1900, 74, 7, 3, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(82, 8, 3, 9, 1700, 2100, 82, 8, 3, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(90, 9, 3, 9, 1900, 2300, 90, 9, 3, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(98, 10, 3, 9, 2100, 2500, 98, 10, 3, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(106, 11, 4, 9, 2300, 2700, 106, 11, 4, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(114, 12, 4, 9, 2500, 2900, 114, 12, 4, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(122, 13, 4, 9, 2700, 3100, 122, 13, 4, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(130, 14, 4, 9, 2900, 3300, 130, 14, 4, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(138, 15, 4, 9, 3100, 3500, 138, 15, 4, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(146, 16, 4, 9, 3300, 3700, 146, 16, 4, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(154, 17, 4, 9, 3500, 3900, 154, 17, 4, 3, 10, 1f, 0, PsRarity.Rare, -1),
			new PsGachaRewardData(162, 18, 5, 9, 3700, 4100, 162, 18, 5, 3, 10, 1f, 0, PsRarity.Rare, -1)
		};
		PsGachaManager.m_gachaRewardDatas.Add(GachaType.EPIC, array7);
		PsGachaRewardData[] array8 = new PsGachaRewardData[]
		{
			new PsGachaRewardData(80, 20, 8, 11, 2000, 3000, 80, 20, 8, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(120, 28, 10, 11, 2500, 3500, 120, 28, 10, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(160, 36, 12, 11, 3000, 4000, 160, 36, 12, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(200, 44, 14, 11, 3500, 4500, 200, 44, 14, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(240, 52, 16, 11, 4000, 5000, 240, 52, 16, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(280, 60, 18, 11, 4500, 5500, 280, 60, 18, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(320, 68, 20, 11, 5000, 6000, 320, 68, 20, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(360, 76, 22, 11, 5500, 6500, 360, 76, 22, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(400, 84, 24, 11, 6000, 7000, 400, 84, 24, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(440, 92, 26, 11, 6500, 7500, 440, 92, 26, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(480, 100, 28, 11, 7000, 8000, 480, 100, 28, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(520, 108, 30, 11, 7500, 8500, 520, 108, 30, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(560, 116, 32, 11, 8000, 9000, 560, 116, 32, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(600, 124, 34, 11, 8500, 9500, 600, 124, 34, 4, 12, 1f, 0, PsRarity.Epic, -1),
			new PsGachaRewardData(640, 132, 36, 11, 9000, 10000, 640, 132, 36, 4, 12, 1f, 0, PsRarity.Epic, -1)
		};
		PsGachaManager.m_gachaRewardDatas.Add(GachaType.SUPER, array8);
		PsGachaRewardData[] array9 = new PsGachaRewardData[]
		{
			new PsGachaRewardData(1, 0, 1, 1, 0, 0, 7, 6, 1, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(1, 0, 1, 1, 0, 0, 7, 6, 1, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(1, 0, 1, 1, 0, 0, 7, 6, 1, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(2, 0, 2, 2, 0, 0, 8, 6, 2, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(2, 0, 2, 2, 0, 0, 8, 6, 2, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(2, 0, 2, 2, 0, 0, 8, 6, 2, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(3, 0, 3, 3, 0, 0, 8, 5, 3, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(3, 0, 3, 3, 0, 0, 8, 5, 3, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(3, 0, 3, 3, 0, 0, 9, 5, 4, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(4, 0, 4, 4, 0, 0, 9, 4, 5, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(4, 0, 4, 4, 0, 0, 9, 3, 6, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(4, 0, 4, 4, 0, 0, 9, 3, 6, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(5, 0, 5, 5, 0, 0, 9, 3, 6, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(5, 0, 5, 5, 0, 0, 10, 4, 6, 4, 3, 0f, 0, PsRarity.Epic, 0),
			new PsGachaRewardData(5, 0, 5, 5, 0, 0, 10, 3, 7, 4, 3, 0f, 0, PsRarity.Epic, 0)
		};
		PsGachaManager.m_gachaRewardDatas.Add(GachaType.BOSS, array9);
		PsGachaManager.m_shopPrices = new Dictionary<GachaType, ObscuredInt[]>();
		PsGachaManager.m_shopPrices.Add(GachaType.RARE, new ObscuredInt[]
		{
			110, 130, 145, 160, 175, 190, 200, 210, 220, 225,
			230, 235, 240, 245, 250
		});
		PsGachaManager.m_shopPrices.Add(GachaType.EPIC, new ObscuredInt[]
		{
			170, 185, 200, 210, 220, 230, 240, 260, 265, 270,
			275, 280, 285, 290, 300
		});
		PsGachaManager.m_shopPrices.Add(GachaType.SUPER, new ObscuredInt[]
		{
			340, 460, 570, 670, 770, 860, 950, 1050, 1100, 1150,
			1200, 1250, 1300, 1350, 1400
		});
	}

	// Token: 0x06000374 RID: 884 RVA: 0x0003448C File Offset: 0x0003288C
	public static int GetShopPrice(GachaType _gachaType)
	{
		int num = 0;
		if (PsGachaManager.m_shopPrices.ContainsKey(_gachaType) && PsGachaManager.m_shopPrices[_gachaType].Length > 0)
		{
			int num2 = Mathf.Clamp(PsMetagameManager.m_playerStats.gachaLevel, 0, PsGachaManager.m_shopPrices[_gachaType].Length - 1);
			num = PsGachaManager.m_shopPrices[_gachaType][num2];
		}
		return num;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x000344FB File Offset: 0x000328FB
	public static GachaType GetUpgradeChestType(int _upgradeChestLevel)
	{
		if (PsGachaManager.m_upgradeChests.ContainsKey(_upgradeChestLevel))
		{
			return PsGachaManager.m_upgradeChests[_upgradeChestLevel];
		}
		return GachaType.SILVER;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0003451C File Offset: 0x0003291C
	public static PsGachaReward GenerateRewards(GachaType _gachaType, int _gachaLevel)
	{
		_gachaLevel = Mathf.Clamp(_gachaLevel, 0, PsGachaManager.m_gachaRewardDatas[_gachaType].Length - 1);
		PsGachaRewardData psGachaRewardData = PsGachaManager.m_gachaRewardDatas[_gachaType][_gachaLevel];
		List<PsUpgradeItem> list = new List<PsUpgradeItem>();
		PsUpgradeData vehicleUpgradeData = PsUpgradeManager.GetVehicleUpgradeData(typeof(OffroadCar));
		for (int i = 0; i <= PsMetagameManager.m_playerStats.carRank; i++)
		{
			list.AddRange(vehicleUpgradeData.GetUpgradeItemsByTier(i + 1));
		}
		if (PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)))
		{
			PsUpgradeData vehicleUpgradeData2 = PsUpgradeManager.GetVehicleUpgradeData(typeof(Motorcycle));
			for (int j = 0; j <= PsMetagameManager.m_playerStats.mcRank; j++)
			{
				list.AddRange(vehicleUpgradeData2.GetUpgradeItemsByTier(j + 1));
			}
		}
		GachaMachine<string> gachaMachine = new GachaMachine<string>();
		GachaMachine<string> gachaMachine2 = new GachaMachine<string>();
		GachaMachine<string> gachaMachine3 = new GachaMachine<string>();
		for (int k = 0; k < list.Count; k++)
		{
			int num = Mathf.Max(0, list[k].m_resourcesNeededToMaxLevel - PsUpgradeManager.GetUpgradeResourceCount(list[k].m_identifier));
			PsRarity rarity = list[k].m_rarity;
			if (rarity != PsRarity.Common)
			{
				if (rarity != PsRarity.Rare)
				{
					if (rarity == PsRarity.Epic)
					{
						gachaMachine3.AddItem(list[k].m_identifier, 2f, num);
						gachaMachine.AddItem(list[k].m_identifier, 2f, num);
					}
				}
				else
				{
					gachaMachine2.AddItem(list[k].m_identifier, 12f, num);
					gachaMachine.AddItem(list[k].m_identifier, 12f, num);
				}
			}
			else
			{
				gachaMachine.AddItem(list[k].m_identifier, 86f, num);
			}
		}
		int num2 = 0;
		if (_gachaType == GachaType.GOLD)
		{
			num2 = 1;
		}
		else if (_gachaType == GachaType.RARE || _gachaType == GachaType.WOOD)
		{
			num2 = 2;
		}
		else if (_gachaType == GachaType.EPIC || _gachaType == GachaType.SUPER)
		{
			num2 = 5;
		}
		Dictionary<string, int> several = gachaMachine.GetSeveral(psGachaRewardData.m_totalUpgradeItemCount - psGachaRewardData.m_minRareUpgradeItemCount - psGachaRewardData.m_minEpicUpgradeItemCount, 0, psGachaRewardData.m_maxDifferendUpgradeItems - num2);
		if (gachaMachine2.count > 0 && psGachaRewardData.m_minRareUpgradeItemCount > 0)
		{
			Dictionary<string, int> several2 = gachaMachine2.GetSeveral(psGachaRewardData.m_minRareUpgradeItemCount, 0, 3);
			List<string> list2 = new List<string>(several2.Keys);
			for (int l = 0; l < list2.Count; l++)
			{
				string text = list2[l];
				if (several.ContainsKey(text))
				{
					Dictionary<string, int> dictionary;
					string text2;
					(dictionary = several)[text2 = text] = dictionary[text2] + several2[text];
				}
				else
				{
					several.Add(text, several2[text]);
				}
			}
		}
		if (gachaMachine3.count > 0 && psGachaRewardData.m_minEpicUpgradeItemCount > 0)
		{
			Dictionary<string, int> several3 = gachaMachine3.GetSeveral(psGachaRewardData.m_minEpicUpgradeItemCount, 0, 2);
			List<string> list3 = new List<string>(several3.Keys);
			for (int m = 0; m < list3.Count; m++)
			{
				string text3 = list3[m];
				if (several.ContainsKey(text3))
				{
					Dictionary<string, int> dictionary;
					string text4;
					(dictionary = several)[text4 = text3] = dictionary[text4] + several3[text3];
				}
				else
				{
					several.Add(text3, several3[text3]);
				}
			}
		}
		GachaMachine<string> gachaMachine4 = new GachaMachine<string>();
		GachaMachine<string> gachaMachine5 = new GachaMachine<string>();
		GachaMachine<string> gachaMachine6 = new GachaMachine<string>();
		int num3 = ((!PsState.m_adminMode) ? (PsMetagameData.m_units.Count - 1) : PsMetagameData.m_units.Count);
		int likesEarned = PsMetagameManager.m_playerStats.likesEarned;
		int creatorRank = PlayerPrefsX.GetClientConfig().creatorRank3;
		bool flag = likesEarned >= creatorRank;
		for (int n = 0; n < PsMetagameData.m_units.Count - 1; n++)
		{
			for (int num4 = 0; num4 < PsMetagameData.m_units[n].m_items.Count; num4++)
			{
				if (PsMetagameData.m_units[n].m_items[num4] is PsEditorItem)
				{
					PsEditorItem psEditorItem = PsMetagameData.m_units[n].m_items[num4] as PsEditorItem;
					if (!(PsMetagameData.m_units[n].m_name == "EDITOR_CATEGORY_ADVANCED") || flag)
					{
						gachaMachine4.AddItem(psEditorItem.m_identifier, psEditorItem.m_gachaProbability, -1);
						PsRarity rarity2 = psEditorItem.m_rarity;
						if (rarity2 != PsRarity.Rare)
						{
							if (rarity2 == PsRarity.Epic)
							{
								gachaMachine6.AddItem(psEditorItem.m_identifier, psEditorItem.m_gachaProbability, -1);
							}
						}
						else
						{
							gachaMachine5.AddItem(psEditorItem.m_identifier, psEditorItem.m_gachaProbability, -1);
						}
					}
				}
			}
		}
		Dictionary<string, int> several4 = gachaMachine4.GetSeveral(psGachaRewardData.m_totalEditorItemCount - psGachaRewardData.m_minRareEditorItemCount - psGachaRewardData.m_minEpicEditorItemCount, 0, psGachaRewardData.m_maxDifferendEditorItems);
		if (gachaMachine5.count > 0 && psGachaRewardData.m_minRareEditorItemCount > 0)
		{
			Dictionary<string, int> several5 = gachaMachine5.GetSeveral(psGachaRewardData.m_minRareEditorItemCount, 0, 2);
			List<string> list4 = new List<string>(several5.Keys);
			for (int num5 = 0; num5 < list4.Count; num5++)
			{
				string text5 = list4[num5];
				if (several4.ContainsKey(text5))
				{
					Dictionary<string, int> dictionary;
					string text6;
					(dictionary = several4)[text6 = text5] = dictionary[text6] + several5[text5];
				}
				else
				{
					several4.Add(text5, several5[text5]);
				}
			}
		}
		if (gachaMachine6.count > 0 && psGachaRewardData.m_minEpicEditorItemCount > 0)
		{
			Dictionary<string, int> several6 = gachaMachine6.GetSeveral(psGachaRewardData.m_minEpicEditorItemCount, 0, 2);
			List<string> list5 = new List<string>(several6.Keys);
			for (int num6 = 0; num6 < list5.Count; num6++)
			{
				string text7 = list5[num6];
				if (several4.ContainsKey(text7))
				{
					Dictionary<string, int> dictionary;
					string text8;
					(dictionary = several4)[text8 = text7] = dictionary[text8] + several6[text7];
				}
				else
				{
					several4.Add(text7, several6[text7]);
				}
			}
		}
		int num7 = 0;
		while (several.Count > psGachaRewardData.m_totalUpgradeItemCount)
		{
			int num8 = Random.Range(0, several.Count);
			int num9 = 0;
			foreach (KeyValuePair<string, int> keyValuePair in several)
			{
				if (num9 == num8)
				{
					several.Remove(keyValuePair.Key);
					break;
				}
				num9++;
			}
			num7++;
			if (num7 > 1000)
			{
				break;
			}
		}
		num7 = 0;
		while (several4.Count > psGachaRewardData.m_totalEditorItemCount)
		{
			int num10 = Random.Range(0, several4.Count);
			int num11 = 0;
			foreach (KeyValuePair<string, int> keyValuePair2 in several4)
			{
				if (num11 == num10)
				{
					several4.Remove(keyValuePair2.Key);
					break;
				}
				num11++;
			}
			num7++;
			if (num7 > 1000)
			{
				break;
			}
		}
		int num12 = Random.Range(psGachaRewardData.m_minCoinReward, psGachaRewardData.m_maxCoinReward + 1);
		int num13 = 0;
		if (psGachaRewardData.m_maxGemReward >= 0)
		{
			num13 = Random.Range(0, psGachaRewardData.m_maxGemReward + 1);
		}
		else if (Random.value > 0.9f)
		{
			num13 = 4;
		}
		int bossHandicap = psGachaRewardData.m_bossHandicap;
		int nitroCount = psGachaRewardData.m_nitroCount;
		string text9 = string.Empty;
		if (_gachaType == GachaType.WOOD)
		{
			PsCustomisationData vehicleCustomisationData = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar));
			PsCustomisationItem itemByIdentifier = vehicleCustomisationData.GetItemByIdentifier("BaseballHat");
			if (!itemByIdentifier.m_unlocked)
			{
				text9 = "BaseballHat";
			}
		}
		else if (_gachaType == GachaType.BOSS)
		{
			PsCustomisationData vehicleCustomisationData2 = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar));
			PsCustomisationItem itemByIdentifier2 = vehicleCustomisationData2.GetItemByIdentifier("WitchHat");
			if (!itemByIdentifier2.m_unlocked)
			{
				text9 = "WitchHat";
			}
		}
		else if (Random.value <= psGachaRewardData.m_hatProbabilityValue)
		{
			PsCustomisationData vehicleCustomisationData3 = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar));
			List<PsCustomisationItem> lockedItemsByCategory = vehicleCustomisationData3.GetLockedItemsByCategory(PsCustomisationManager.CustomisationCategory.HAT);
			GachaMachine<string> gachaMachine7 = new GachaMachine<string>();
			GachaMachine<string> gachaMachine8 = new GachaMachine<string>();
			GachaMachine<string> gachaMachine9 = new GachaMachine<string>();
			for (int num14 = 0; num14 < lockedItemsByCategory.Count; num14++)
			{
				PsRarity rarity3 = lockedItemsByCategory[num14].m_rarity;
				if (rarity3 != PsRarity.Common)
				{
					if (rarity3 != PsRarity.Rare)
					{
						if (rarity3 == PsRarity.Epic)
						{
							gachaMachine9.AddItem(lockedItemsByCategory[num14].m_identifier, 1f, -1);
						}
					}
					else
					{
						gachaMachine8.AddItem(lockedItemsByCategory[num14].m_identifier, 1f, -1);
					}
				}
				else
				{
					gachaMachine7.AddItem(lockedItemsByCategory[num14].m_identifier, 1f, -1);
				}
			}
			if (psGachaRewardData.m_guaranteedHatRarity == PsRarity.Epic && gachaMachine9.count > 0)
			{
				text9 = gachaMachine9.GetItem(true);
			}
			else if (psGachaRewardData.m_guaranteedHatRarity != PsRarity.Common && gachaMachine8.count > 0)
			{
				text9 = gachaMachine8.GetItem(true);
			}
			else if (gachaMachine7.count > 0 || gachaMachine8.count > 0 || gachaMachine9.count > 0)
			{
				GachaMachine<PsRarity> gachaMachine10 = new GachaMachine<PsRarity>();
				if (gachaMachine7.count > 0)
				{
					gachaMachine10.AddItem(PsRarity.Common, 86f, -1);
				}
				if (gachaMachine8.count > 0)
				{
					gachaMachine10.AddItem(PsRarity.Rare, 12f, -1);
				}
				if (gachaMachine9.count > 0)
				{
					gachaMachine10.AddItem(PsRarity.Epic, 2f, -1);
				}
				if (gachaMachine10.count > 0)
				{
					PsRarity item = gachaMachine10.GetItem(true);
					if (item != PsRarity.Common)
					{
						if (item != PsRarity.Rare)
						{
							if (item == PsRarity.Epic)
							{
								text9 = gachaMachine9.GetItem(true);
							}
						}
						else
						{
							text9 = gachaMachine8.GetItem(true);
						}
					}
					else
					{
						text9 = gachaMachine7.GetItem(true);
					}
				}
			}
		}
		return new PsGachaReward(several, several4, bossHandicap, num12, num13, nitroCount, text9);
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00034FF4 File Offset: 0x000333F4
	public static bool IsSlotEmpty(PsGachaManager.SlotType _slotType)
	{
		return PsGachaManager.m_gachas[PsGachaManager.GetSlotIndex(_slotType)] == null;
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00035008 File Offset: 0x00033408
	public static PsGacha GetNextGacha()
	{
		int openedChestCount = PsMetagameManager.GetOpenedChestCount();
		if (PsGachaManager.m_fixedChests != null && PsGachaManager.m_fixedChests.ContainsKey(openedChestCount + 1))
		{
			return new PsGacha(PsGachaManager.m_fixedChests[openedChestCount + 1]);
		}
		if (Random.value > 1f - PsGachaManager.m_superChestProbability)
		{
			return new PsGacha(GachaType.SUPER);
		}
		int num = openedChestCount % PsGachaManager.m_rotationChests.Count;
		return new PsGacha(PsGachaManager.m_rotationChests[Mathf.Clamp(num, 0, PsGachaManager.m_rotationChests.Count - 1)]);
	}

	// Token: 0x06000379 RID: 889 RVA: 0x00035098 File Offset: 0x00033498
	public static void Update(bool _update = true)
	{
		for (int i = 0; i < PsGachaManager.m_gachas.Length; i++)
		{
			if (PsGachaManager.m_gachas[i] != null && !PsGachaManager.m_gachas[i].m_unlocked && PsGachaManager.m_gachas[i].m_unlockStartedTime > 0.0)
			{
				PsGachaManager.m_gachas[i].m_unlockTimeLeft = (int)Math.Ceiling(PsGachaManager.m_gachas[i].m_unlockStartedTime + (double)PsGachaManager.m_gachas[i].m_unlockTime - Main.m_EPOCHSeconds);
				if (PsGachaManager.m_gachas[i].m_unlockTimeLeft <= 0)
				{
					PsGachaManager.m_gachas[i].m_unlockTimeLeft = 0;
					PsGachaManager.m_gachas[i].m_unlocked = true;
					if (_update)
					{
						PsGachaManager.m_dirty = true;
						PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), new Hashtable(), PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().GetIdentifier(), false);
					}
				}
			}
		}
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00035180 File Offset: 0x00033580
	public static void UnlockGacha(PsGacha _gacha, bool _sendToServer = true)
	{
		for (int i = 0; i < PsGachaManager.m_gachas.Length; i++)
		{
			if (PsGachaManager.m_gachas[i] == _gacha)
			{
				if (!PsGachaManager.m_gachas[i].m_unlocked && PsGachaManager.m_gachas[i].m_unlockStartedTime == 0.0)
				{
					PsGachaManager.m_gachas[i].m_unlockStartedTime = Main.m_EPOCHSeconds;
					PsGachaManager.m_dirty = true;
					if (_sendToServer)
					{
						PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), new Hashtable(), PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().GetIdentifier(), false);
					}
				}
				break;
			}
		}
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00035220 File Offset: 0x00033620
	public static void UnlockGachaImmediately(PsGacha _gacha, bool _sendToServer = true)
	{
		for (int i = 0; i < PsGachaManager.m_gachas.Length; i++)
		{
			if (PsGachaManager.m_gachas[i] == _gacha)
			{
				if (!PsGachaManager.m_gachas[i].m_unlocked)
				{
					PsGachaManager.m_gachas[i].m_unlockStartedTime = Main.m_EPOCHSeconds - (double)PsGachaManager.m_gachas[i].m_unlockTime;
					PsGachaManager.m_gachas[i].m_unlockTimeLeft = 0;
					PsGachaManager.m_gachas[i].m_unlocked = true;
					PsGachaManager.m_dirty = true;
					if (_sendToServer)
					{
						PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), new Hashtable(), PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().GetIdentifier(), false);
					}
				}
				break;
			}
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000352CC File Offset: 0x000336CC
	public static Hashtable OpenCustomGacha(GachaType _gachaType, string _bonusHatIdentifier, int _bonusMoney)
	{
		PsGachaReward psGachaReward = PsGachaManager.GenerateRewards(_gachaType, PsMetagameManager.m_playerStats.gachaLevel);
		psGachaReward.m_bonusHat = _bonusHatIdentifier;
		psGachaReward.m_bonusMoney = _bonusMoney;
		PsUpgradeManager.IncreaseResources(psGachaReward.m_upgradeItems);
		PsMetagameManager.m_playerStats.CumulateEditorResources(psGachaReward.m_editorItems);
		if (psGachaReward.m_bonusMoney > 0)
		{
			PsMetagameManager.m_playerStats.coins += psGachaReward.m_bonusMoney;
			FrbMetrics.ReceiveVirtualCurrency("coins", (double)psGachaReward.m_bonusMoney, "tournament_reward");
		}
		if (psGachaReward.m_coins > 0)
		{
			PsMetagameManager.m_playerStats.coins += psGachaReward.m_coins;
			FrbMetrics.ReceiveVirtualCurrency("coins", (double)psGachaReward.m_coins, "tournament_reward_chest");
		}
		if (psGachaReward.m_gems > 0)
		{
			PsMetagameManager.m_playerStats.diamonds += psGachaReward.m_gems;
			FrbMetrics.ReceiveVirtualCurrency("gems", (double)psGachaReward.m_gems, "tournament_reward_chest");
		}
		if (psGachaReward.m_nitros > 0)
		{
			for (int i = 0; i < PsState.m_vehicleTypes.Length; i++)
			{
				PsMetagameManager.m_playerStats.CumulateBoosters(psGachaReward.m_nitros, PsState.m_vehicleTypes[i]);
			}
		}
		if (!string.IsNullOrEmpty(psGachaReward.m_hat))
		{
			for (int j = 0; j < PsState.m_vehicleTypes.Length; j++)
			{
				PsCustomisationManager.UnlockItem(PsState.m_vehicleTypes[j], psGachaReward.m_hat);
			}
		}
		if (!string.IsNullOrEmpty(psGachaReward.m_bonusHat))
		{
			for (int k = 0; k < PsState.m_vehicleTypes.Length; k++)
			{
				PsCustomisationManager.UnlockItem(PsState.m_vehicleTypes[k], psGachaReward.m_bonusHat);
			}
		}
		PsGachaManager.m_lastOpenedGacha = _gachaType;
		PsGachaManager.m_lastGachaRewards = psGachaReward;
		return PsMetagameManager.GetChestResourceHashtable(new Hashtable(), new Hashtable());
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00035488 File Offset: 0x00033888
	public static PsGachaReward OpenGacha(PsGacha _gacha, int _gachaIndex = -1, bool _sendToServer = true)
	{
		Debug.Log("E_Test OpenGacha() " + _gacha.m_gachaType.ToString(), null);
		int slotIndex = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.ADVENTURE);
		GachaType gachaType = _gacha.m_gachaType;
		for (int i = 0; i < PsGachaManager.m_gachas.Length; i++)
		{
			if (PsGachaManager.m_gachas[i] == _gacha)
			{
				PsGachaManager.m_gachas[i] = null;
			}
		}
		PsGachaReward psGachaReward = PsGachaManager.GenerateRewards(gachaType, PsMetagameManager.m_playerStats.gachaLevel);
		PsUpgradeManager.IncreaseResources(psGachaReward.m_upgradeItems);
		PsMetagameManager.m_playerStats.CumulateEditorResources(psGachaReward.m_editorItems);
		if (psGachaReward.m_coins > 0)
		{
			PsMetagameManager.m_playerStats.coins += psGachaReward.m_coins;
		}
		if (psGachaReward.m_gems > 0)
		{
			PsMetagameManager.m_playerStats.diamonds += psGachaReward.m_gems;
		}
		if (psGachaReward.m_nitros > 0)
		{
			for (int j = 0; j < PsState.m_vehicleTypes.Length; j++)
			{
				PsMetagameManager.m_playerStats.CumulateBoosters(psGachaReward.m_nitros, PsState.m_vehicleTypes[j]);
			}
		}
		if (!string.IsNullOrEmpty(psGachaReward.m_hat))
		{
			for (int k = 0; k < PsState.m_vehicleTypes.Length; k++)
			{
				PsCustomisationManager.UnlockItem(PsState.m_vehicleTypes[k], psGachaReward.m_hat);
			}
		}
		if (psGachaReward.m_boss > 0)
		{
			BossBattles.AlterBothVehicleHandicaps(BossBattles.GachaTypeHandicap(_gacha.m_gachaType));
		}
		BossBattles.AlterBothVehicleHandicaps(BossBattles.openChestChange);
		PsGachaManager.m_dirty = true;
		if (_sendToServer)
		{
			PsMetagameManager.OpenChest(_gachaIndex, _gacha.m_gachaType, new Hashtable(), new Hashtable(), new Action<HttpC>(PsMetagameManager.OpenChestSUCCEED), new Action<HttpC>(PsMetagameManager.OpenChestFAILED), null);
		}
		return psGachaReward;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00035668 File Offset: 0x00033A68
	public static void ReduceGachaTime(int _gachaIndex, int _secondsToReduce, bool _sendToServer = true)
	{
		PsGachaManager.m_gachas[_gachaIndex].m_unlockTime -= _secondsToReduce;
		PsGachaManager.m_gachas[_gachaIndex].m_unlockTimeLeft -= _secondsToReduce;
		PsGachaManager.m_dirty = true;
		if (_sendToServer)
		{
			PsMetagameManager.SendCurrentGachaData(true);
		}
	}

	// Token: 0x0600037F RID: 895 RVA: 0x000356A4 File Offset: 0x00033AA4
	public static void AddGacha(PsGacha _gacha, PsGachaManager.SlotType _slotType, bool _sendToServer = false)
	{
		PsGachaManager.AddGacha(_gacha, PsGachaManager.GetSlotIndex(_slotType), _sendToServer);
	}

	// Token: 0x06000380 RID: 896 RVA: 0x000356B4 File Offset: 0x00033AB4
	public static void AddGacha(PsGacha _gacha, int _index, bool _sendToServer = false)
	{
		if (_index >= 0 && _index < PsGachaManager.m_gachas.Length)
		{
			PsGachaManager.m_gachas[_index] = _gacha;
			PsGachaManager.m_lastAddedIndex = _index;
			PsGachaManager.m_dirty = true;
			if (_sendToServer)
			{
				PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), new Hashtable(), PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().GetIdentifier(), false);
			}
		}
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00035710 File Offset: 0x00033B10
	public static List<Hashtable> GetUpdatedData()
	{
		if (!PsGachaManager.m_dirty)
		{
			return null;
		}
		List<Hashtable> list = new List<Hashtable>();
		for (int i = 0; i < PsGachaManager.m_gachas.Length; i++)
		{
			if (PsGachaManager.m_gachas[i] != null)
			{
				Hashtable hashtable = new Hashtable();
				hashtable.Add("id", i);
				if (PsGachaManager.m_gachas[i].m_unlockStartedTime != 0.0 || PsGachaManager.m_gachas[i].m_unlockTimeLeft == 0)
				{
					hashtable.Add("timeLeft", PsGachaManager.m_gachas[i].m_unlockTimeLeft);
				}
				hashtable.Add("type", PsGachaManager.m_gachas[i].m_gachaType.ToString());
				if (PsGachaManager.m_gachas[i].m_notified)
				{
					hashtable.Add("notified", PsGachaManager.m_gachas[i].m_notified);
				}
				list.Add(hashtable);
			}
		}
		PsGachaManager.m_dirty = false;
		return list;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0003580E File Offset: 0x00033C0E
	public static int GetSlotIndex(PsGachaManager.SlotType _slotType)
	{
		switch (_slotType)
		{
		case PsGachaManager.SlotType.ADVENTURE:
			return 0;
		case PsGachaManager.SlotType.RACING:
			return 1;
		case PsGachaManager.SlotType.FREE:
			return 2;
		case PsGachaManager.SlotType.CAR_PATH:
			return 4;
		case PsGachaManager.SlotType.MOTO_PATH:
			return 5;
		default:
			return -1;
		}
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0003583C File Offset: 0x00033C3C
	public static string GetGachaNameWithChest(GachaType _gachaType)
	{
		switch (_gachaType)
		{
		case GachaType.WOOD:
			return PsStrings.Get(StringID.GACHA_NAME_WOOD).ToUpper();
		case GachaType.COMMON:
			return PsStrings.Get(StringID.GACHA_NAME_WOOD).ToUpper();
		case GachaType.BRONZE:
			return PsStrings.Get(StringID.GACHA_NAME_BRONZE).ToUpper();
		case GachaType.SILVER:
			return PsStrings.Get(StringID.GACHA_NAME_SILVER).ToUpper();
		case GachaType.GOLD:
			return PsStrings.Get(StringID.GACHA_NAME_GOLD).ToUpper();
		case GachaType.RARE:
			return PsStrings.Get(StringID.GACHA_NAME_RARE).ToUpper();
		case GachaType.EPIC:
			return PsStrings.Get(StringID.GACHA_NAME_EPIC).ToUpper();
		case GachaType.SUPER:
			return PsStrings.Get(StringID.GACHA_NAME_SUPER).ToUpper();
		case GachaType.BOSS:
			return PsStrings.Get(StringID.GACHA_NAME_SKULL).ToUpper();
		default:
			return "UNKNOWN";
		}
	}

	// Token: 0x06000384 RID: 900 RVA: 0x000358F8 File Offset: 0x00033CF8
	public static string GetGachaName(GachaType _gachaType)
	{
		switch (_gachaType)
		{
		case GachaType.WOOD:
			return PsStrings.Get(StringID.GACHA_RARITY_WOOD).ToUpper();
		case GachaType.COMMON:
			return PsStrings.Get(StringID.GACHA_RARITY_WOOD).ToUpper();
		case GachaType.BRONZE:
			return PsStrings.Get(StringID.GACHA_RARITY_BRONZE).ToUpper();
		case GachaType.SILVER:
			return PsStrings.Get(StringID.GACHA_RARITY_SILVER).ToUpper();
		case GachaType.GOLD:
			return PsStrings.Get(StringID.GACHA_RARITY_GOLD).ToUpper();
		case GachaType.RARE:
			return PsStrings.Get(StringID.GACHA_RARITY_RARE).ToUpper();
		case GachaType.EPIC:
			return PsStrings.Get(StringID.GACHA_RARITY_EPIC).ToUpper();
		case GachaType.SUPER:
			return PsStrings.Get(StringID.GACHA_RARITY_SUPER).ToUpper();
		case GachaType.BOSS:
			return PsStrings.Get(StringID.BOSS_BATTLE_STARTSCREEN_HEADER).ToUpper();
		default:
			return "UNKNOWN";
		}
	}

	// Token: 0x06000385 RID: 901 RVA: 0x000359B4 File Offset: 0x00033DB4
	public static string GetChestIconName(GachaType _gachaType)
	{
		switch (_gachaType)
		{
		case GachaType.WOOD:
		case GachaType.COMMON:
			return "menu_icon_chest_wood";
		case GachaType.BRONZE:
			return "menu_icon_chest_bronze";
		case GachaType.SILVER:
			return "menu_icon_chest_silver";
		case GachaType.GOLD:
			return "menu_icon_chest_gold";
		case GachaType.RARE:
			return "menu_icon_chest_T1";
		case GachaType.EPIC:
			return "menu_icon_chest_T2";
		case GachaType.SUPER:
			return "menu_icon_chest_T3";
		default:
			return string.Empty;
		}
	}

	// Token: 0x0400045E RID: 1118
	public const float COMMON_UPGRADE_ITEM_PROBABILITY = 86f;

	// Token: 0x0400045F RID: 1119
	public const float RARE_UPGRADE_ITEM_PROBABILITY = 12f;

	// Token: 0x04000460 RID: 1120
	public const float EPIC_UPGRADE_ITEM_PROBABILITY = 2f;

	// Token: 0x04000461 RID: 1121
	public const float COMMON_HAT_PROBABILITY = 86f;

	// Token: 0x04000462 RID: 1122
	public const float RARE_HAT_PROBABILITY = 12f;

	// Token: 0x04000463 RID: 1123
	public const float EPIC_HAT_PROBABILITY = 2f;

	// Token: 0x04000464 RID: 1124
	private static bool m_dirty;

	// Token: 0x04000465 RID: 1125
	public static PsGacha[] m_gachas;

	// Token: 0x04000466 RID: 1126
	public static int m_lastAddedIndex = -1;

	// Token: 0x04000467 RID: 1127
	public static PsGachaReward m_lastGachaRewards;

	// Token: 0x04000468 RID: 1128
	public static GachaType m_lastOpenedGacha;

	// Token: 0x04000469 RID: 1129
	public static Dictionary<GachaType, PsGachaRewardData[]> m_gachaRewardDatas;

	// Token: 0x0400046A RID: 1130
	private static Dictionary<int, GachaType> m_fixedChests;

	// Token: 0x0400046B RID: 1131
	private static Dictionary<int, GachaType> m_upgradeChests;

	// Token: 0x0400046C RID: 1132
	private static List<GachaType> m_rotationChests;

	// Token: 0x0400046D RID: 1133
	private static float m_superChestProbability;

	// Token: 0x0400046E RID: 1134
	public static bool m_giveConsolation;

	// Token: 0x0400046F RID: 1135
	public static bool m_gachaSet;

	// Token: 0x04000470 RID: 1136
	private static Dictionary<GachaType, ObscuredInt[]> m_shopPrices;

	// Token: 0x020000A7 RID: 167
	public enum SlotType
	{
		// Token: 0x04000474 RID: 1140
		ADVENTURE,
		// Token: 0x04000475 RID: 1141
		RACING,
		// Token: 0x04000476 RID: 1142
		FREE,
		// Token: 0x04000477 RID: 1143
		CAR_PATH,
		// Token: 0x04000478 RID: 1144
		MOTO_PATH
	}
}
