using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x0200013D RID: 317
public class PlayerStats
{
	// Token: 0x17000017 RID: 23
	// (set) Token: 0x06000989 RID: 2441 RVA: 0x00066C06 File Offset: 0x00065006
	public Dictionary<string, ObscuredInt> editorResources
	{
		set
		{
			this.m_editorResources = value;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x0600098A RID: 2442 RVA: 0x00066C0F File Offset: 0x0006500F
	public bool isDirty
	{
		get
		{
			return this.m_isDirty;
		}
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x00066C17 File Offset: 0x00065017
	public void SetDirty(bool value = true)
	{
		this.m_isDirty = value;
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00066C20 File Offset: 0x00065020
	public int SuperLikeSecondsLeft()
	{
		return (int)(this.m_superLikeRefreshEnd - Main.m_EPOCHSeconds);
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00066C44 File Offset: 0x00065044
	public void SuperLikeSet()
	{
		int num = PlayerPrefsX.GetSuperLikeInterval() * 60;
		this.m_superLikeRefreshEnd = Main.m_EPOCHSeconds + (double)num;
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x0600098E RID: 2446 RVA: 0x00066C6D File Offset: 0x0006506D
	// (set) Token: 0x0600098F RID: 2447 RVA: 0x00066C7A File Offset: 0x0006507A
	public int adventureLevels
	{
		get
		{
			return this.m_adventureLevels;
		}
		set
		{
			if (this.m_adventureLevels != value)
			{
				this.m_adventureLevels = value;
			}
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000990 RID: 2448 RVA: 0x00066C99 File Offset: 0x00065099
	// (set) Token: 0x06000991 RID: 2449 RVA: 0x00066CA6 File Offset: 0x000650A6
	public int racesCompleted
	{
		get
		{
			return this.m_racesCompleted;
		}
		set
		{
			if (this.m_racesCompleted != value)
			{
				this.m_racesCompleted = value;
			}
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000992 RID: 2450 RVA: 0x00066CC5 File Offset: 0x000650C5
	// (set) Token: 0x06000993 RID: 2451 RVA: 0x00066CD2 File Offset: 0x000650D2
	public int levelsMade
	{
		get
		{
			return this.m_levelsMade;
		}
		set
		{
			if (this.m_levelsMade != value)
			{
				this.m_levelsMade = value;
			}
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000994 RID: 2452 RVA: 0x00066CF1 File Offset: 0x000650F1
	// (set) Token: 0x06000995 RID: 2453 RVA: 0x00066CFE File Offset: 0x000650FE
	public int likesEarned
	{
		get
		{
			return this.m_likesEarned;
		}
		set
		{
			if (this.m_likesEarned != value)
			{
				this.m_likesEarned = value;
			}
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000996 RID: 2454 RVA: 0x00066D1D File Offset: 0x0006511D
	// (set) Token: 0x06000997 RID: 2455 RVA: 0x00066D2A File Offset: 0x0006512A
	public int megaLikesEarned
	{
		get
		{
			return this.m_megaLikesEarned;
		}
		set
		{
			if (this.m_megaLikesEarned != value)
			{
				this.m_megaLikesEarned = value;
			}
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000998 RID: 2456 RVA: 0x00066D49 File Offset: 0x00065149
	// (set) Token: 0x06000999 RID: 2457 RVA: 0x00066D56 File Offset: 0x00065156
	public int newLevelsRated
	{
		get
		{
			return this.m_newLevelsRated;
		}
		set
		{
			if (this.m_newLevelsRated != value)
			{
				this.m_newLevelsRated = value;
			}
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600099A RID: 2458 RVA: 0x00066D75 File Offset: 0x00065175
	// (set) Token: 0x0600099B RID: 2459 RVA: 0x00066D82 File Offset: 0x00065182
	public bool cheater
	{
		get
		{
			return this.m_cheater;
		}
		set
		{
			this.m_cheater = value;
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x0600099C RID: 2460 RVA: 0x00066D90 File Offset: 0x00065190
	// (set) Token: 0x0600099D RID: 2461 RVA: 0x00066D9D File Offset: 0x0006519D
	public int coins
	{
		get
		{
			return this.m_coins;
		}
		set
		{
			if (this.m_coins != value)
			{
				this.m_coins = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x0600099E RID: 2462 RVA: 0x00066DC3 File Offset: 0x000651C3
	// (set) Token: 0x0600099F RID: 2463 RVA: 0x00066DD0 File Offset: 0x000651D0
	public int copper
	{
		get
		{
			return this.m_copper;
		}
		set
		{
			if (this.m_copper != value)
			{
				this.m_copper = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00066DF6 File Offset: 0x000651F6
	// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00066E03 File Offset: 0x00065203
	public int diamonds
	{
		get
		{
			return this.m_diamonds;
		}
		set
		{
			if (this.m_diamonds != value)
			{
				this.m_diamonds = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00066E29 File Offset: 0x00065229
	// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00066E36 File Offset: 0x00065236
	public int shards
	{
		get
		{
			return this.m_shards;
		}
		set
		{
			if (this.m_shards != value)
			{
				this.m_shards = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00066E5C File Offset: 0x0006525C
	// (set) Token: 0x060009A5 RID: 2469 RVA: 0x00066E69 File Offset: 0x00065269
	public bool fbClaimed
	{
		get
		{
			return this.m_fbClaimed;
		}
		set
		{
			if (this.m_fbClaimed != value)
			{
				this.m_fbClaimed = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00066E8F File Offset: 0x0006528F
	// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00066E9C File Offset: 0x0006529C
	public bool igClaimed
	{
		get
		{
			return this.m_igClaimed;
		}
		set
		{
			if (this.m_igClaimed != value)
			{
				this.m_igClaimed = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00066EC2 File Offset: 0x000652C2
	// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00066ECF File Offset: 0x000652CF
	public bool forumClaimed
	{
		get
		{
			return this.m_forumClaimed;
		}
		set
		{
			if (this.m_forumClaimed != value)
			{
				this.m_forumClaimed = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060009AA RID: 2474 RVA: 0x00066EF5 File Offset: 0x000652F5
	// (set) Token: 0x060009AB RID: 2475 RVA: 0x00066F02 File Offset: 0x00065302
	public bool completedSurvey
	{
		get
		{
			return this.m_completedSurvey;
		}
		set
		{
			if (this.m_completedSurvey != value)
			{
				this.m_completedSurvey = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060009AC RID: 2476 RVA: 0x00066F28 File Offset: 0x00065328
	// (set) Token: 0x060009AD RID: 2477 RVA: 0x00066F35 File Offset: 0x00065335
	public string gender
	{
		get
		{
			return this.m_gender;
		}
		set
		{
			if (this.m_gender != value)
			{
				this.m_gender = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060009AE RID: 2478 RVA: 0x00066F60 File Offset: 0x00065360
	// (set) Token: 0x060009AF RID: 2479 RVA: 0x00066F6D File Offset: 0x0006536D
	public string ageGroup
	{
		get
		{
			return this.m_ageGroup;
		}
		set
		{
			if (this.m_ageGroup != value)
			{
				this.m_ageGroup = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00066F98 File Offset: 0x00065398
	// (set) Token: 0x060009B1 RID: 2481 RVA: 0x00066FA5 File Offset: 0x000653A5
	public int tournamentBoosters
	{
		get
		{
			return this.m_tournamentBoosters;
		}
		set
		{
			if (this.m_tournamentBoosters != value)
			{
				this.m_tournamentBoosters = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00066FCB File Offset: 0x000653CB
	// (set) Token: 0x060009B3 RID: 2483 RVA: 0x00066FD8 File Offset: 0x000653D8
	public int mcBoosters
	{
		get
		{
			return this.m_mcBoosters;
		}
		set
		{
			if (this.m_mcBoosters != value)
			{
				this.m_mcBoosters = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00066FFE File Offset: 0x000653FE
	// (set) Token: 0x060009B5 RID: 2485 RVA: 0x0006700B File Offset: 0x0006540B
	public int carBoosters
	{
		get
		{
			return this.m_carBoosters;
		}
		set
		{
			if (this.m_carBoosters != value)
			{
				this.m_carBoosters = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00067031 File Offset: 0x00065431
	public int maxMcBoosters
	{
		get
		{
			return 6 + (int)PsUpgradeManager.GetCurrentEfficiency(typeof(Motorcycle), PsUpgradeManager.UpgradeType.NITRO_BOOST_COUNT);
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00067046 File Offset: 0x00065446
	public int maxCarBoosters
	{
		get
		{
			return 6 + (int)PsUpgradeManager.GetCurrentEfficiency(typeof(OffroadCar), PsUpgradeManager.UpgradeType.NITRO_BOOST_COUNT);
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0006705B File Offset: 0x0006545B
	public int gachaLevel
	{
		get
		{
			return PsMetagameData.GetLeagueIndex(this.m_mcTrophies) + PsMetagameData.GetLeagueIndex(this.m_carTrophies);
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0006707E File Offset: 0x0006547E
	// (set) Token: 0x060009BA RID: 2490 RVA: 0x0006708B File Offset: 0x0006548B
	public bool coinDoubler
	{
		get
		{
			return this.m_coinDoubler;
		}
		set
		{
			if (this.m_coinDoubler != value)
			{
				this.m_coinDoubler = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060009BB RID: 2491 RVA: 0x000670B1 File Offset: 0x000654B1
	// (set) Token: 0x060009BC RID: 2492 RVA: 0x000670BE File Offset: 0x000654BE
	public bool dirtBikeBundle
	{
		get
		{
			return this.m_dirtBikeUnlocked;
		}
		set
		{
			if (this.m_dirtBikeUnlocked != value)
			{
				this.m_dirtBikeUnlocked = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060009BD RID: 2493 RVA: 0x000670E4 File Offset: 0x000654E4
	// (set) Token: 0x060009BE RID: 2494 RVA: 0x000670EC File Offset: 0x000654EC
	public List<ObscuredString> trailsPurchased
	{
		get
		{
			return this.m_trailsPurchased;
		}
		set
		{
			this.m_trailsPurchased = value;
			this.m_isDirty = true;
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060009BF RID: 2495 RVA: 0x000670FC File Offset: 0x000654FC
	// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00067104 File Offset: 0x00065504
	public List<ObscuredString> hatsPurchased
	{
		get
		{
			return this.m_hatsPurchased;
		}
		set
		{
			this.m_hatsPurchased = value;
			this.m_isDirty = true;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00067114 File Offset: 0x00065514
	// (set) Token: 0x060009C2 RID: 2498 RVA: 0x0006711C File Offset: 0x0006551C
	public List<ObscuredString> bundlesPurchased
	{
		get
		{
			return this.m_bundlesPurchased;
		}
		set
		{
			this.m_bundlesPurchased = value;
			this.m_isDirty = true;
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0006712C File Offset: 0x0006552C
	// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00067134 File Offset: 0x00065534
	public List<GachaType> pendingSpecialOfferChests
	{
		get
		{
			return this.m_pendingSpecialOfferChests;
		}
		set
		{
			this.m_pendingSpecialOfferChests = value;
			this.m_isDirty = true;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00067144 File Offset: 0x00065544
	// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00067151 File Offset: 0x00065551
	public int followerCount
	{
		get
		{
			return this.m_followerCount;
		}
		set
		{
			this.m_followerCount = value;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0006715F File Offset: 0x0006555F
	// (set) Token: 0x060009C8 RID: 2504 RVA: 0x0006716C File Offset: 0x0006556C
	public int racesThisSeason
	{
		get
		{
			return this.m_racesThisSeason;
		}
		set
		{
			this.m_racesThisSeason = value;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0006717A File Offset: 0x0006557A
	// (set) Token: 0x060009CA RID: 2506 RVA: 0x00067187 File Offset: 0x00065587
	public int youtubeSubscriberCount
	{
		get
		{
			return this.m_youtuberSubscriberCount;
		}
		set
		{
			this.m_youtuberSubscriberCount = value;
		}
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x00067198 File Offset: 0x00065598
	public void InitializeBoosters()
	{
		this.m_boosterInfos = new Dictionary<string, BoosterInfo>();
		this.m_boosterInfos.Add(typeof(OffroadCar).ToString(), new BoosterInfo(typeof(OffroadCar), "car", 0));
		this.m_boosterInfos.Add(typeof(Motorcycle).ToString(), new BoosterInfo(typeof(Motorcycle), "mc", 0));
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x060009CC RID: 2508 RVA: 0x00067210 File Offset: 0x00065610
	// (set) Token: 0x060009CD RID: 2509 RVA: 0x00067288 File Offset: 0x00065688
	public int boosters
	{
		get
		{
			if (PsState.m_activeGameLoop != null && PsState.m_activeGameLoop is PsGameLoopTournament)
			{
				return this.m_tournamentBoosters;
			}
			if (PsState.GetCurrentVehicleType(true) == typeof(OffroadCar))
			{
				return this.m_carBoosters;
			}
			if (PsState.GetCurrentVehicleType(true) == typeof(Motorcycle))
			{
				return this.m_mcBoosters;
			}
			return 0;
		}
		set
		{
			if (PsState.GetCurrentVehicleType(true) == typeof(Motorcycle))
			{
				if (this.m_mcBoosters != value)
				{
					this.m_mcBoosters = value;
					this.m_isDirty = true;
				}
			}
			else if (PsState.GetCurrentVehicleType(true) == typeof(OffroadCar) && this.m_carBoosters != value)
			{
				this.m_carBoosters = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060009CE RID: 2510 RVA: 0x0006730C File Offset: 0x0006570C
	public int maxBoosters
	{
		get
		{
			if (PsState.GetCurrentVehicleType(true) == typeof(OffroadCar))
			{
				return this.maxCarBoosters;
			}
			if (PsState.GetCurrentVehicleType(true) == typeof(Motorcycle))
			{
				return this.maxMcBoosters;
			}
			return 0;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060009CF RID: 2511 RVA: 0x00067347 File Offset: 0x00065747
	// (set) Token: 0x060009D0 RID: 2512 RVA: 0x0006734F File Offset: 0x0006574F
	public int stars
	{
		get
		{
			return this.m_stars;
		}
		set
		{
			if (this.m_stars != value)
			{
				this.m_stars = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0006736B File Offset: 0x0006576B
	// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00067373 File Offset: 0x00065773
	public int level
	{
		get
		{
			return this.m_level;
		}
		set
		{
			if (this.m_level != value)
			{
				this.m_level = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0006738F File Offset: 0x0006578F
	// (set) Token: 0x060009D4 RID: 2516 RVA: 0x00067397 File Offset: 0x00065797
	public bool updated
	{
		get
		{
			return this.m_updated;
		}
		set
		{
			if (this.m_updated != value)
			{
				this.m_updated = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x060009D5 RID: 2517 RVA: 0x000673B3 File Offset: 0x000657B3
	// (set) Token: 0x060009D6 RID: 2518 RVA: 0x000673BB File Offset: 0x000657BB
	public bool upgraded
	{
		get
		{
			return this.m_upgraded;
		}
		set
		{
			if (this.m_upgraded != value)
			{
				this.m_upgraded = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000673D7 File Offset: 0x000657D7
	// (set) Token: 0x060009D8 RID: 2520 RVA: 0x000673DF File Offset: 0x000657DF
	public int itemLevel
	{
		get
		{
			return this.m_itemLevel;
		}
		set
		{
			if (this.m_itemLevel != value)
			{
				this.m_itemLevel = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000673FB File Offset: 0x000657FB
	// (set) Token: 0x060009DA RID: 2522 RVA: 0x00067408 File Offset: 0x00065808
	public int bigBangPoints
	{
		get
		{
			return this.m_bigBangPoints;
		}
		set
		{
			if (this.m_bigBangPoints != value)
			{
				this.m_bigBangPoints = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x060009DB RID: 2523 RVA: 0x0006742E File Offset: 0x0006582E
	// (set) Token: 0x060009DC RID: 2524 RVA: 0x00067436 File Offset: 0x00065836
	public int cups
	{
		get
		{
			return this.m_cups;
		}
		set
		{
			if (this.m_cups != value)
			{
				this.m_cups = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x060009DD RID: 2525 RVA: 0x00067452 File Offset: 0x00065852
	// (set) Token: 0x060009DE RID: 2526 RVA: 0x0006745A File Offset: 0x0006585A
	public int mcRank
	{
		get
		{
			return this.m_mcRank;
		}
		set
		{
			if (this.m_mcRank != value)
			{
				this.m_mcRank = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x060009DF RID: 2527 RVA: 0x00067476 File Offset: 0x00065876
	// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0006747E File Offset: 0x0006587E
	public int carRank
	{
		get
		{
			return this.m_carRank;
		}
		set
		{
			if (this.m_carRank != value)
			{
				this.m_carRank = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0006749A File Offset: 0x0006589A
	// (set) Token: 0x060009E2 RID: 2530 RVA: 0x000674A8 File Offset: 0x000658A8
	public int mcTrophies
	{
		get
		{
			return this.m_mcTrophies;
		}
		set
		{
			if (this.m_mcTrophies != value)
			{
				this.m_mcTrophies = value;
				this.m_isDirty = true;
				if (PsMetagameData.GetLeagueIndex(this.m_mcTrophies) > this.m_mcRank)
				{
					this.mcRank = PsMetagameData.GetLeagueIndex(this.m_mcTrophies);
				}
			}
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0006750A File Offset: 0x0006590A
	// (set) Token: 0x060009E4 RID: 2532 RVA: 0x00067518 File Offset: 0x00065918
	public int carTrophies
	{
		get
		{
			return this.m_carTrophies;
		}
		set
		{
			if (this.m_carTrophies != value)
			{
				this.m_carTrophies = value;
				this.m_isDirty = true;
				if (PsMetagameData.GetLeagueIndex(this.m_carTrophies) > this.m_carRank)
				{
					this.carRank = PsMetagameData.GetLeagueIndex(this.m_carTrophies);
				}
			}
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0006757A File Offset: 0x0006597A
	public int totalTrophies
	{
		get
		{
			return this.carTrophies + this.mcTrophies;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00067589 File Offset: 0x00065989
	// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00067596 File Offset: 0x00065996
	public float mcHandicap
	{
		get
		{
			return this.m_mcHandicap;
		}
		set
		{
			if (this.m_mcHandicap != value)
			{
				this.m_mcHandicap = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000675BC File Offset: 0x000659BC
	// (set) Token: 0x060009E9 RID: 2537 RVA: 0x000675C9 File Offset: 0x000659C9
	public float carHandicap
	{
		get
		{
			return this.m_carHandicap;
		}
		set
		{
			if (this.m_carHandicap != value)
			{
				this.m_carHandicap = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060009EA RID: 2538 RVA: 0x000675EF File Offset: 0x000659EF
	// (set) Token: 0x060009EB RID: 2539 RVA: 0x000675FC File Offset: 0x000659FC
	public int xp
	{
		get
		{
			return this.m_xp;
		}
		set
		{
			if (this.m_xp != value)
			{
				this.m_xp = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060009EC RID: 2540 RVA: 0x00067622 File Offset: 0x00065A22
	// (set) Token: 0x060009ED RID: 2541 RVA: 0x0006762F File Offset: 0x00065A2F
	public string cardPurchases
	{
		get
		{
			return this.m_cardPurchases;
		}
		set
		{
			if (this.m_cardPurchases != value)
			{
				this.m_cardPurchases = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060009EE RID: 2542 RVA: 0x0006765A File Offset: 0x00065A5A
	// (set) Token: 0x060009EF RID: 2543 RVA: 0x00067667 File Offset: 0x00065A67
	public string gachaData
	{
		get
		{
			return this.m_gachaData;
		}
		set
		{
			if (this.m_gachaData != value)
			{
				this.m_gachaData = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00067694 File Offset: 0x00065A94
	// (set) Token: 0x060009F1 RID: 2545 RVA: 0x000676E4 File Offset: 0x00065AE4
	public int trophies
	{
		get
		{
			if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar))
			{
				return this.m_carTrophies;
			}
			if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
			{
				return this.m_mcTrophies;
			}
			return 0;
		}
		set
		{
			if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
			{
				if (this.m_mcTrophies != value)
				{
					this.m_mcTrophies = value;
					this.m_isDirty = true;
					if (PsMetagameData.GetLeagueIndex(this.m_mcTrophies) > this.m_mcRank)
					{
						this.mcRank = PsMetagameData.GetLeagueIndex(this.m_mcTrophies);
					}
				}
			}
			else if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar) && this.m_carTrophies != value)
			{
				this.m_carTrophies = value;
				this.m_isDirty = true;
				if (PsMetagameData.GetLeagueIndex(this.m_carTrophies) > this.m_carRank)
				{
					this.carRank = PsMetagameData.GetLeagueIndex(this.m_carTrophies);
				}
			}
		}
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x000677CC File Offset: 0x00065BCC
	public int GetCurrentMinigameVehicleTrophies()
	{
		if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar))
		{
			return this.m_carTrophies;
		}
		if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
		{
			return this.m_mcTrophies;
		}
		return 0;
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0006781C File Offset: 0x00065C1C
	// (set) Token: 0x060009F4 RID: 2548 RVA: 0x00067858 File Offset: 0x00065C58
	public int rank
	{
		get
		{
			if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar))
			{
				return this.m_carRank;
			}
			if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
			{
				return this.m_mcRank;
			}
			return 0;
		}
		set
		{
			if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
			{
				if (this.m_mcRank != value)
				{
					this.m_mcRank = value;
					this.m_isDirty = true;
				}
			}
			else if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar) && this.m_carRank != value)
			{
				this.m_carRank = value;
				this.m_isDirty = true;
			}
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060009F5 RID: 2549 RVA: 0x000678C8 File Offset: 0x00065CC8
	// (set) Token: 0x060009F6 RID: 2550 RVA: 0x000678D0 File Offset: 0x00065CD0
	public Hashtable upgrades
	{
		get
		{
			return this.m_upgrades;
		}
		set
		{
			this.m_upgrades = value;
			this.m_isDirty = true;
		}
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x000678E0 File Offset: 0x00065CE0
	[Obsolete("", false)]
	public int GetUpgradeValue(Type _vehicleType, string _key, int _defaultValue = 0)
	{
		if (this.m_upgrades.ContainsKey(_vehicleType.ToString()))
		{
			Hashtable hashtable = this.m_upgrades[_vehicleType.ToString()] as Hashtable;
			if (hashtable != null && hashtable.ContainsKey(_key))
			{
				return Convert.ToInt32(hashtable[_key]);
			}
			if (hashtable == null)
			{
				hashtable = new Hashtable();
			}
			hashtable.Add(_key, _defaultValue);
			this.m_upgrades[_vehicleType.ToString()] = hashtable;
			this.SetDirty(true);
		}
		else
		{
			Hashtable hashtable2 = new Hashtable();
			hashtable2.Add(_key, _defaultValue);
			this.m_upgrades.Add(_vehicleType.ToString(), hashtable2);
			this.SetDirty(true);
		}
		return _defaultValue;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x000679A4 File Offset: 0x00065DA4
	[Obsolete("", false)]
	public bool TierChanged(Type _vehicleType)
	{
		if (!this.m_upgrades.ContainsKey(_vehicleType.ToString()))
		{
			return false;
		}
		Hashtable hashtable = this.m_upgrades[_vehicleType.ToString()] as Hashtable;
		if (hashtable == null || hashtable.Count < 3)
		{
			return false;
		}
		int upgradeValue = this.GetUpgradeValue(_vehicleType, "tier", 1);
		int num = 999999;
		IEnumerator enumerator = hashtable.Keys.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text = (string)obj;
				if (!text.Equals("tier") && Convert.ToInt32(hashtable[text]) < num)
				{
					num = Convert.ToInt32(hashtable[text]);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
		if (Mathf.FloorToInt((float)num / 5f) + 1 > upgradeValue)
		{
			hashtable["tier"] = upgradeValue + 1;
			this.m_upgrades[_vehicleType.ToString()] = hashtable;
			this.SetDirty(true);
			return true;
		}
		return false;
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00067AD4 File Offset: 0x00065ED4
	[Obsolete("", false)]
	public void SetUpgradeValue(Type _vehicleType, string _key, int _value)
	{
		if (this.m_upgrades.ContainsKey(_vehicleType.ToString()))
		{
			Hashtable hashtable = this.m_upgrades[_vehicleType.ToString()] as Hashtable;
			if (hashtable == null || !hashtable.ContainsKey(_key))
			{
				if (hashtable == null)
				{
					hashtable = new Hashtable();
				}
				hashtable.Add(_key, _value);
			}
			else
			{
				hashtable[_key] = _value;
			}
			this.m_upgrades[_vehicleType.ToString()] = hashtable;
		}
		else
		{
			Hashtable hashtable2 = new Hashtable();
			hashtable2.Add(_key, _value);
			this.m_upgrades.Add(_vehicleType.ToString(), hashtable2);
		}
		this.SetDirty(true);
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x00067B90 File Offset: 0x00065F90
	public void CumulateUpgradeValue(Type _vehicleType, string _key)
	{
		if (this.m_upgrades.ContainsKey(_vehicleType.ToString()))
		{
			Hashtable hashtable = this.m_upgrades[_vehicleType.ToString()] as Hashtable;
			if (hashtable == null || !hashtable.ContainsKey(_key))
			{
				if (hashtable == null)
				{
					hashtable = new Hashtable();
				}
				hashtable.Add(_key, 1);
			}
			else
			{
				hashtable[_key] = Convert.ToInt32(hashtable[_key]) + 1;
			}
			this.m_upgrades[_vehicleType.ToString()] = hashtable;
		}
		else
		{
			Hashtable hashtable2 = new Hashtable();
			hashtable2.Add(_key, 1);
			this.m_upgrades.Add(_vehicleType.ToString(), hashtable2);
		}
		this.SetDirty(true);
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x00067C58 File Offset: 0x00066058
	public void CumulateCoins(int _amount)
	{
		this.m_coins += _amount;
		if (PsState.m_activeMinigame != null)
		{
			Minigame activeMinigame = PsState.m_activeMinigame;
			activeMinigame.m_collectedCoins += _amount;
		}
		if (PsMetagameManager.m_menuResourceView != null && PsMetagameManager.m_menuResourceView.m_TC.p_entity.m_visible && PsMetagameManager.m_menuResourceView.m_showCoins)
		{
			PsMetagameManager.m_menuResourceView.CreateAddedResources(ResourceType.Coins, _amount, 0f);
		}
		this.SetDirty(true);
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x00067CF0 File Offset: 0x000660F0
	public void CumulateCoinsWithFlyingResources(int _amount, Vector2 _startPos, float _delay = 0f)
	{
		this.m_coins += _amount;
		if (PsState.m_activeMinigame != null)
		{
			Minigame activeMinigame = PsState.m_activeMinigame;
			activeMinigame.m_collectedCoins += _amount;
		}
		if (PsMetagameManager.m_menuResourceView != null && PsMetagameManager.m_menuResourceView.m_TC.p_entity.m_visible)
		{
			PsMetagameManager.m_menuResourceView.CreateFlyingResources(_amount, _startPos, ResourceType.Coins, _delay, null, null, null, null, default(Vector2));
		}
		this.SetDirty(true);
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00067D80 File Offset: 0x00066180
	public void CumulateDiamonds(int _amount)
	{
		this.m_diamonds += _amount;
		if (PsState.m_activeMinigame != null)
		{
			Minigame activeMinigame = PsState.m_activeMinigame;
			activeMinigame.m_collectedDiamonds += _amount;
		}
		if (PsMetagameManager.m_menuResourceView != null && PsMetagameManager.m_menuResourceView.m_TC.p_entity.m_visible && PsMetagameManager.m_menuResourceView.m_showDiamonds)
		{
			PsMetagameManager.m_menuResourceView.CreateAddedResources(ResourceType.Diamonds, _amount, 0f);
		}
		this.SetDirty(true);
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x00067E18 File Offset: 0x00066218
	public void CumulateShards(int _amount, bool _cumulateDiamonds = true)
	{
		if (PsState.m_activeMinigame != null)
		{
			Minigame activeMinigame = PsState.m_activeMinigame;
			activeMinigame.m_collectedShards += _amount;
		}
		PsMetagameManager.m_playerStats.shards += _amount;
		if (PsMetagameManager.m_playerStats.shards > 99)
		{
			PsMetagameManager.m_playerStats.shards -= 100;
			PsMetagameManager.m_playerStats.shardReset = true;
			if (PsState.m_activeMinigame != null)
			{
				PsState.m_activeMinigame.m_collectedShards = PsMetagameManager.m_playerStats.shards;
			}
			if (_cumulateDiamonds)
			{
				this.CumulateDiamonds(1);
			}
		}
		if (PsMetagameManager.m_menuResourceView != null && PsMetagameManager.m_menuResourceView.m_showShards)
		{
			PsMetagameManager.m_menuResourceView.CreateAddedResources(ResourceType.Shards, _amount, 0f);
		}
		this.SetDirty(true);
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00067EF0 File Offset: 0x000662F0
	public void CumulateDiamondsWithFlyingResources(int _amount, Vector2 _startPos, float _delay = 0f)
	{
		this.m_diamonds += _amount;
		if (PsState.m_activeMinigame != null)
		{
			Minigame activeMinigame = PsState.m_activeMinigame;
			activeMinigame.m_collectedDiamonds += _amount;
		}
		if (PsMetagameManager.m_menuResourceView != null)
		{
			PsMetagameManager.m_menuResourceView.CreateFlyingResources(_amount, _startPos, ResourceType.Diamonds, _delay, null, null, null, null, default(Vector2));
		}
		this.SetDirty(true);
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x00067F68 File Offset: 0x00066368
	public void CreateFlyingGacha(int _amount, Vector2 _startPos, Vector2 _endPos, float _delay = 0f, float[] _control0XMinMax = null, float[] _control1XMinMax = null, float[] _heightMinMax = null)
	{
		if (PsMetagameManager.m_menuResourceView != null && PsMetagameManager.m_menuResourceView.m_TC.p_entity.m_visible)
		{
			PsUIMainMenuResources menuResourceView = PsMetagameManager.m_menuResourceView;
			ResourceType resourceType = ResourceType.Gacha;
			menuResourceView.CreateFlyingResources(_amount, _startPos, resourceType, _delay, null, _control0XMinMax, _control1XMinMax, _heightMinMax, _endPos);
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00067FBC File Offset: 0x000663BC
	public void CreateFlyingMapPieces(int _amount, Vector2 _startPos, Vector2 _endPos, float _delay = 0f, float[] _control0XMinMax = null, float[] _control1XMinMax = null, float[] _heightMinMax = null)
	{
		if (PsMetagameManager.m_menuResourceView != null && PsMetagameManager.m_menuResourceView.m_TC.p_entity.m_visible)
		{
			PsUIMainMenuResources menuResourceView = PsMetagameManager.m_menuResourceView;
			ResourceType resourceType = ResourceType.Map;
			menuResourceView.CreateFlyingResources(_amount, _startPos, resourceType, _delay, null, _control0XMinMax, _control1XMinMax, _heightMinMax, _endPos);
		}
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00068010 File Offset: 0x00066410
	public void CreateFlyingTrophies(int _cumulateAmount, Vector2 _startPos, Vector2 _endPos, float _delay = 0f, float[] _control0XMinMax = null, float[] _control1XMinMax = null, float[] _heightMinMax = null)
	{
		if (PsMetagameManager.m_menuResourceView != null && PsMetagameManager.m_menuResourceView.m_TC.p_entity.m_visible)
		{
			PsUIMainMenuResources menuResourceView = PsMetagameManager.m_menuResourceView;
			ResourceType resourceType = ResourceType.Trophies;
			menuResourceView.CreateFlyingResources(_cumulateAmount, _startPos, resourceType, _delay, null, _control0XMinMax, _control1XMinMax, _heightMinMax, _endPos);
			TimerC timerC = TimerS.AddComponent(PsMetagameManager.m_menuResourceView.m_TC.p_entity, "trophyTimer", 0f, 0.5f, false, delegate(TimerC c)
			{
				this.TrophyTimerDelegate(c);
			});
			timerC.customObject = _cumulateAmount;
			SoundS.PlaySingleShot("/Metagame/BadgeFly", Vector3.zero, 1f);
		}
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x000680B8 File Offset: 0x000664B8
	public void TrophyTimerDelegate(TimerC _c)
	{
		int num = (int)_c.customObject;
		TimerS.RemoveComponent(_c);
		if (Main.m_currentGame.m_currentScene.GetCurrentState() is PsMainMenuState)
		{
			PsMainMenuState.CumulateTrophies(num);
		}
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x000680F6 File Offset: 0x000664F6
	public void CumulateBoosters(int _amount)
	{
		this.CumulateBoosters(_amount, PsState.GetCurrentVehicleType(true));
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00068108 File Offset: 0x00066508
	public void CumulateBoosters(int _amount, Type _vehicleType)
	{
		if (_vehicleType == typeof(Motorcycle))
		{
			this.mcBoosters = Math.Min(this.mcBoosters + _amount, PsMetagameManager.m_playerStats.maxMcBoosters);
		}
		else if (_vehicleType == typeof(OffroadCar))
		{
			this.carBoosters = Math.Min(this.carBoosters + _amount, PsMetagameManager.m_playerStats.maxCarBoosters);
		}
		this.SetDirty(true);
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0006817B File Offset: 0x0006657B
	public void CumulateTournamentBoosters(int _amount)
	{
		this.m_tournamentBoosters += _amount;
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x00068195 File Offset: 0x00066595
	public void CumulateStars(int _amount)
	{
		this.m_stars += _amount;
		this.SetDirty(true);
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x000681AC File Offset: 0x000665AC
	public int GetEditorResourceCount(string _itemIdentifier)
	{
		if (this.m_editorResources.ContainsKey(_itemIdentifier))
		{
			return this.m_editorResources[_itemIdentifier];
		}
		return 0;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x000681D4 File Offset: 0x000665D4
	public void CumulateEditorResources(Dictionary<string, int> _resources)
	{
		if (_resources.Count > 0)
		{
			List<string> list = new List<string>(_resources.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				this.CumulateEditorResources(list[i], _resources[list[i]]);
			}
		}
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0006822C File Offset: 0x0006662C
	public void CumulateEditorResources(Dictionary<string, ObscuredInt> _resources)
	{
		if (_resources.Count > 0)
		{
			List<string> list = new List<string>(_resources.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				this.CumulateEditorResources(list[i], _resources[list[i]]);
			}
		}
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00068288 File Offset: 0x00066688
	public void CumulateEditorResources(string _itemIdentifier, int _count)
	{
		if (_count != 0)
		{
			int editorItemLimit = this.GetEditorItemLimit(_itemIdentifier);
			if (this.m_editorResources.ContainsKey(_itemIdentifier))
			{
				if (editorItemLimit >= 0)
				{
					_count = Mathf.Clamp(this.m_editorResources[_itemIdentifier] + _count, 0, editorItemLimit);
				}
				else
				{
					_count = this.m_editorResources[_itemIdentifier] + _count;
				}
				this.m_editorResources[_itemIdentifier] = _count;
			}
			else
			{
				if (editorItemLimit >= 0)
				{
					_count = Mathf.Clamp(_count, 0, editorItemLimit);
				}
				this.m_editorResources.Add(_itemIdentifier, _count);
			}
			if (this.m_dirtyEditorResources == null)
			{
				this.m_dirtyEditorResources = new List<string>();
				this.m_dirtyEditorResources.Add(_itemIdentifier);
			}
			else if (!this.m_dirtyEditorResources.Contains(_itemIdentifier))
			{
				this.m_dirtyEditorResources.Add(_itemIdentifier);
			}
		}
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0006836F File Offset: 0x0006676F
	public int GetEditorItemLimit(string _itemIdentifier)
	{
		return -1;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x00068374 File Offset: 0x00066774
	public Dictionary<string, int> GetUpdatedEditorResources()
	{
		if (this.m_dirtyEditorResources == null || this.m_dirtyEditorResources.Count == 0)
		{
			return null;
		}
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		for (int i = 0; i < this.m_dirtyEditorResources.Count; i++)
		{
			if (this.m_editorResources.ContainsKey(this.m_dirtyEditorResources[i]))
			{
				dictionary.Add(this.m_dirtyEditorResources[i], this.m_editorResources[this.m_dirtyEditorResources[i]]);
			}
		}
		this.m_dirtyEditorResources.Clear();
		return dictionary;
	}

	// Token: 0x040008FB RID: 2299
	private bool m_isDirty;

	// Token: 0x040008FC RID: 2300
	private int m_stars;

	// Token: 0x040008FD RID: 2301
	private int m_level;

	// Token: 0x040008FE RID: 2302
	private bool m_updated;

	// Token: 0x040008FF RID: 2303
	private bool m_upgraded;

	// Token: 0x04000900 RID: 2304
	private int m_itemLevel;

	// Token: 0x04000901 RID: 2305
	private int m_cups;

	// Token: 0x04000902 RID: 2306
	private int m_mcRank;

	// Token: 0x04000903 RID: 2307
	private int m_carRank;

	// Token: 0x04000904 RID: 2308
	private Dictionary<string, ObscuredInt> m_editorResources;

	// Token: 0x04000905 RID: 2309
	private ObscuredBool m_cheater = false;

	// Token: 0x04000906 RID: 2310
	private ObscuredInt m_coins;

	// Token: 0x04000907 RID: 2311
	private ObscuredInt m_copper;

	// Token: 0x04000908 RID: 2312
	private ObscuredInt m_diamonds;

	// Token: 0x04000909 RID: 2313
	private ObscuredInt m_shards;

	// Token: 0x0400090A RID: 2314
	private ObscuredInt m_tournamentBoosters;

	// Token: 0x0400090B RID: 2315
	private ObscuredInt m_mcBoosters;

	// Token: 0x0400090C RID: 2316
	private ObscuredInt m_carBoosters;

	// Token: 0x0400090D RID: 2317
	private ObscuredInt m_mcTrophies;

	// Token: 0x0400090E RID: 2318
	private ObscuredInt m_carTrophies;

	// Token: 0x0400090F RID: 2319
	private ObscuredInt m_bigBangPoints;

	// Token: 0x04000910 RID: 2320
	private ObscuredString m_cardPurchases;

	// Token: 0x04000911 RID: 2321
	private ObscuredString m_gachaData;

	// Token: 0x04000912 RID: 2322
	private ObscuredInt m_xp;

	// Token: 0x04000913 RID: 2323
	private ObscuredFloat m_mcHandicap = BossBattles.startHandicap;

	// Token: 0x04000914 RID: 2324
	private ObscuredFloat m_carHandicap = BossBattles.startHandicap;

	// Token: 0x04000915 RID: 2325
	private ObscuredBool m_coinDoubler;

	// Token: 0x04000916 RID: 2326
	private ObscuredBool m_dirtBikeUnlocked;

	// Token: 0x04000917 RID: 2327
	private List<ObscuredString> m_trailsPurchased;

	// Token: 0x04000918 RID: 2328
	private List<ObscuredString> m_hatsPurchased;

	// Token: 0x04000919 RID: 2329
	private List<ObscuredString> m_bundlesPurchased;

	// Token: 0x0400091A RID: 2330
	private List<GachaType> m_pendingSpecialOfferChests;

	// Token: 0x0400091B RID: 2331
	private ObscuredBool m_fbClaimed;

	// Token: 0x0400091C RID: 2332
	private ObscuredBool m_igClaimed;

	// Token: 0x0400091D RID: 2333
	private ObscuredBool m_forumClaimed;

	// Token: 0x0400091E RID: 2334
	private ObscuredBool m_completedSurvey;

	// Token: 0x0400091F RID: 2335
	private ObscuredString m_gender;

	// Token: 0x04000920 RID: 2336
	private ObscuredString m_ageGroup;

	// Token: 0x04000921 RID: 2337
	private ObscuredInt m_adventureLevels;

	// Token: 0x04000922 RID: 2338
	private ObscuredInt m_racesCompleted;

	// Token: 0x04000923 RID: 2339
	private ObscuredInt m_levelsMade;

	// Token: 0x04000924 RID: 2340
	private ObscuredInt m_likesEarned;

	// Token: 0x04000925 RID: 2341
	private ObscuredInt m_megaLikesEarned;

	// Token: 0x04000926 RID: 2342
	private ObscuredInt m_newLevelsRated;

	// Token: 0x04000927 RID: 2343
	private ObscuredInt m_followerCount;

	// Token: 0x04000928 RID: 2344
	private ObscuredInt m_racesThisSeason;

	// Token: 0x04000929 RID: 2345
	private Hashtable m_upgrades = new Hashtable();

	// Token: 0x0400092A RID: 2346
	public string m_teamKickReason;

	// Token: 0x0400092B RID: 2347
	public ObscuredInt m_lastSeasonMcTrophies = 0;

	// Token: 0x0400092C RID: 2348
	public ObscuredInt m_lastSeasonCarTrophies = 0;

	// Token: 0x0400092D RID: 2349
	private ObscuredInt m_youtuberSubscriberCount = 0;

	// Token: 0x0400092E RID: 2350
	public ObscuredDouble m_superLikeRefreshEnd;

	// Token: 0x0400092F RID: 2351
	public bool copperReset;

	// Token: 0x04000930 RID: 2352
	public bool shardReset;

	// Token: 0x04000931 RID: 2353
	public Dictionary<string, BoosterInfo> m_boosterInfos;

	// Token: 0x04000932 RID: 2354
	public List<string> m_dirtyEditorResources;
}
