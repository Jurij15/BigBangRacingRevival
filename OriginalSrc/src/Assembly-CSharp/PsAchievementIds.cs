using System;
using System.Collections.Generic;

// Token: 0x020000EE RID: 238
public static class PsAchievementIds
{
	// Token: 0x0600053C RID: 1340 RVA: 0x00045620 File Offset: 0x00043A20
	public static string GetGPGSID(string _id)
	{
		if (_id != null)
		{
			if (PsAchievementIds.<>f__switch$map2 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(40);
				dictionary.Add("triggerFortyChickens", 0);
				dictionary.Add("unlockBike", 1);
				dictionary.Add("userRustBucket", 2);
				dictionary.Add("upgradeMax", 3);
				dictionary.Add("fiftyFrontFlips", 4);
				dictionary.Add("fiveHundredFrontFlips", 5);
				dictionary.Add("hundredBackFlips", 6);
				dictionary.Add("fiveHundredBackFlips", 7);
				dictionary.Add("collectCoinsTenLevels", 8);
				dictionary.Add("collectCoinsFiftyLevels", 9);
				dictionary.Add("blowTwoThousandBarrels", 10);
				dictionary.Add("blowTwoHundredBarrels", 11);
				dictionary.Add("breakTwoThousandPlanks", 12);
				dictionary.Add("breakTwoHundredPlanks", 13);
				dictionary.Add("breakTwoThousandBoxes", 14);
				dictionary.Add("hatsOffFifteenTimes", 15);
				dictionary.Add("reachHundredHPCar", 16);
				dictionary.Add("reachHundredHPBike", 17);
				dictionary.Add("reachFiveHundredHPBike", 18);
				dictionary.Add("reachDomeForty", 19);
				dictionary.Add("reachDomeHundred", 20);
				dictionary.Add("gainFiftyGoldMedals", 21);
				dictionary.Add("finishHardLevel", 22);
				dictionary.Add("finishHardLevelFirstTry", 23);
				dictionary.Add("dealWithIt", 24);
				dictionary.Add("epicDealWithIt", 25);
				dictionary.Add("impossibru", 26);
				dictionary.Add("winThreeRacesInARow", 27);
				dictionary.Add("publishLevel", 28);
				dictionary.Add("useTeleport", 29);
				dictionary.Add("longTimeInEditor", 30);
				dictionary.Add("useFifteenItems", 31);
				dictionary.Add("challengeFriend", 32);
				dictionary.Add("challengeAccepted", 33);
				dictionary.Add("playRateThirty", 34);
				dictionary.Add("testFriendLevels", 35);
				dictionary.Add("rateThirtyPathLevels", 36);
				dictionary.Add("uniqueSnowflake", 37);
				dictionary.Add("connectSocial", 38);
				dictionary.Add("gain100KCoins", 39);
				PsAchievementIds.<>f__switch$map2 = dictionary;
			}
			int num;
			if (PsAchievementIds.<>f__switch$map2.TryGetValue(_id, ref num))
			{
				switch (num)
				{
				case 0:
					return "CgkI7p2Cr-8UEAIQCA";
				case 5:
					return "CgkI7p2Cr-8UEAIQBA";
				case 6:
					return "CgkI7p2Cr-8UEAIQDQ";
				case 10:
					return "CgkI7p2Cr-8UEAIQCg";
				case 11:
					return "CgkI7p2Cr-8UEAIQCQ";
				case 12:
					return "CgkI7p2Cr-8UEAIQCw";
				case 13:
					return "CgkI7p2Cr-8UEAIQAQ";
				case 14:
					return "CgkI7p2Cr-8UEAIQDA";
				case 16:
					return "CgkI7p2Cr-8UEAIQEg";
				case 17:
					return "CgkI7p2Cr-8UEAIQEw";
				case 18:
					return "CgkI7p2Cr-8UEAIQFA";
				case 22:
					return "CgkI7p2Cr-8UEAIQDg";
				case 24:
					return "CgkI7p2Cr-8UEAIQEA";
				case 25:
					return "CgkI7p2Cr-8UEAIQEQ";
				case 26:
					return "CgkI7p2Cr-8UEAIQDw";
				case 28:
					return "CgkI7p2Cr-8UEAIQBg";
				case 30:
					return "CgkI7p2Cr-8UEAIQFQ";
				case 35:
					return "CgkI7p2Cr-8UEAIQBw";
				}
			}
		}
		return null;
	}

	// Token: 0x040006A2 RID: 1698
	public const string TRIGGER_CHICKENS_40 = "triggerFortyChickens";

	// Token: 0x040006A3 RID: 1699
	public const string UNLOCK_BIKE = "unlockBike";

	// Token: 0x040006A4 RID: 1700
	public const string USE_WORSE_VEHICLE = "userRustBucket";

	// Token: 0x040006A5 RID: 1701
	public const string UPGRADE_MAX = "upgradeMax";

	// Token: 0x040006A6 RID: 1702
	public const string FRONT_FLIP_50 = "fiftyFrontFlips";

	// Token: 0x040006A7 RID: 1703
	public const string FRONT_FLIP_500 = "fiveHundredFrontFlips";

	// Token: 0x040006A8 RID: 1704
	public const string BACK_FLIP_100 = "hundredBackFlips";

	// Token: 0x040006A9 RID: 1705
	public const string BACK_FLIP_500 = "fiveHundredBackFlips";

	// Token: 0x040006AA RID: 1706
	public const string COLLECT_COINS_10 = "collectCoinsTenLevels";

	// Token: 0x040006AB RID: 1707
	public const string COLLECT_COINS_50 = "collectCoinsFiftyLevels";

	// Token: 0x040006AC RID: 1708
	public const string BLOW_BARRELS_2000 = "blowTwoThousandBarrels";

	// Token: 0x040006AD RID: 1709
	public const string BLOW_BARRELS_200 = "blowTwoHundredBarrels";

	// Token: 0x040006AE RID: 1710
	public const string BREAK_PLANKS_2000 = "breakTwoThousandPlanks";

	// Token: 0x040006AF RID: 1711
	public const string BREAK_PLANKS_200 = "breakTwoHundredPlanks";

	// Token: 0x040006B0 RID: 1712
	public const string BREAK_BOXES_2000 = "breakTwoThousandBoxes";

	// Token: 0x040006B1 RID: 1713
	public const string HATS_OFF_15 = "hatsOffFifteenTimes";

	// Token: 0x040006B2 RID: 1714
	public const string REACH_100_HP_CAR = "reachHundredHPCar";

	// Token: 0x040006B3 RID: 1715
	public const string REACH_100_HP_BIKE = "reachHundredHPBike";

	// Token: 0x040006B4 RID: 1716
	public const string REACH_500_HP_BIKE = "reachFiveHundredHPBike";

	// Token: 0x040006B5 RID: 1717
	public const string REACH_DOME_40 = "reachDomeForty";

	// Token: 0x040006B6 RID: 1718
	public const string REACH_DOME_100 = "reachDomeHundred";

	// Token: 0x040006B7 RID: 1719
	public const string COIN_COUNT_100K = "gain100KCoins";

	// Token: 0x040006B8 RID: 1720
	public const string FIFTY_GOLD_MEDALS = "gainFiftyGoldMedals";

	// Token: 0x040006B9 RID: 1721
	public const string FINISH_TRICKY_LEVEL = "finishHardLevel";

	// Token: 0x040006BA RID: 1722
	public const string FINISH_TRICKY_LEVEL_FIRST = "finishHardLevelFirstTry";

	// Token: 0x040006BB RID: 1723
	public const string FINISH_HARD_LEVEL = "dealWithIt";

	// Token: 0x040006BC RID: 1724
	public const string FINISH_TWENTY_HARD_LEVELS = "epicDealWithIt";

	// Token: 0x040006BD RID: 1725
	public const string FINISH_INSANE_LEVEL = "impossibru";

	// Token: 0x040006BE RID: 1726
	public const string WIN_3_CONSECUTIVE_RACES = "winThreeRacesInARow";

	// Token: 0x040006BF RID: 1727
	public const string PUBLISH_LEVEL = "publishLevel";

	// Token: 0x040006C0 RID: 1728
	public const string USE_TELEPORT = "useTeleport";

	// Token: 0x040006C1 RID: 1729
	public const string SPEND_180MIN_IN_EDITOR = "longTimeInEditor";

	// Token: 0x040006C2 RID: 1730
	public const string USE_15_ITEMS = "useFifteenItems";

	// Token: 0x040006C3 RID: 1731
	public const string CHALLENGE_FRIEND = "challengeFriend";

	// Token: 0x040006C4 RID: 1732
	public const string CHALLENGE_ACCEPTED = "challengeAccepted";

	// Token: 0x040006C5 RID: 1733
	public const string PLAY_RATE_30 = "playRateThirty";

	// Token: 0x040006C6 RID: 1734
	public const string TEST_FRIEND_LEVELS = "testFriendLevels";

	// Token: 0x040006C7 RID: 1735
	public const string RATE_PATH_LEVELS_30 = "rateThirtyPathLevels";

	// Token: 0x040006C8 RID: 1736
	public const string CHANGE_NAME = "uniqueSnowflake";

	// Token: 0x040006C9 RID: 1737
	public const string CONNECT_SOCIAL = "connectSocial";
}
