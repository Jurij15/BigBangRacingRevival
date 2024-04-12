using System;
using System.Collections.Generic;

// Token: 0x020000EF RID: 239
public static class PsAchievementManager
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600053D RID: 1341 RVA: 0x000459E2 File Offset: 0x00043DE2
	public static bool IsInitialized
	{
		get
		{
			return AchievementManager.IsInitialized;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x0600053E RID: 1342 RVA: 0x000459E9 File Offset: 0x00043DE9
	public static List<Achievement> All
	{
		get
		{
			return AchievementManager.All;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x0600053F RID: 1343 RVA: 0x000459F0 File Offset: 0x00043DF0
	public static List<Achievement> Completed
	{
		get
		{
			return AchievementManager.Completed;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000540 RID: 1344 RVA: 0x000459F7 File Offset: 0x00043DF7
	public static List<Achievement> Locked
	{
		get
		{
			return AchievementManager.Locked;
		}
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00045A00 File Offset: 0x00043E00
	public static void Initialize()
	{
		PsAchievementManager.m_updated = new List<string>();
		List<Achievement> list = new List<Achievement>();
		list.Add(new PsAchievement("fiveHundredFrontFlips", "Frontflip pro!", "Perform frontflips\nto unlock", 500, 0));
		list.Add(new PsAchievement("unlockBike", "New vehicle!", "Unlock the motorbike\nto unlock", 1, 0));
		list.Add(new PsAchievement("upgradeMax", "Grease monkey", "Upgrade any vehicle to the max\nto unlock", 1, 0));
		list.Add(new PsAchievement("publishLevel", "Very constructive", "Publish a level\nto unlock", 1, 0));
		list.Add(new PsAchievement("challengeFriend", "A Challenger Appears", "Challenged a friend to a friend race\nto unlock", 1, 0));
		list.Add(new PsAchievement("challengeAccepted", "Challenge accepted", "Accept a friend challenge\nto unlock", 1, 0));
		list.Add(new PsAchievement("testFriendLevels", "Buddies", "Test a friend level from the friend list\nto unlock", 1, 0));
		list.Add(new PsAchievement("triggerFortyChickens", "Birdie!", "Trigger chickens\nto unlock", 40, 0));
		list.Add(new PsAchievement("blowTwoHundredBarrels", "This blows", "Blow up barrels\nto unlock", 200, 0));
		list.Add(new PsAchievement("blowTwoThousandBarrels", "The Big Banger", "Blow up barrels\nto unlock", 2000, 0));
		list.Add(new PsAchievement("breakTwoHundredPlanks", "Chop it up!", "Break planks\nto unlock", 200, 0));
		list.Add(new PsAchievement("breakTwoThousandPlanks", "Professional logger", "Break planks\nto unlock", 2000, 0));
		list.Add(new PsAchievement("breakTwoThousandBoxes", "Logistics expert", "Break boxes\nto unlock", 2000, 0));
		list.Add(new PsAchievement("hundredBackFlips", "Flippin' mad", "Perform backflips\nto unlock", 100, 0));
		list.Add(new PsAchievement("finishHardLevel", "It's tricky!", "Finish a tricky level\nto unlock", 1, 0));
		list.Add(new PsAchievement("impossibru", "Utterly insane", "Finish a insane level\nto unlock", 1, 0));
		list.Add(new PsAchievement("dealWithIt", "Hard as a... hard thing!", "Finish a hard level\nto unlock", 1, 0));
		list.Add(new PsAchievement("epicDealWithIt", "Deal with it.", "Finish hard levels\nto unlock", 20, 0));
		list.Add(new PsAchievement("gainFiftyGoldMedals", "Star by star", "Finish 50 times with 3 star rating\nto unlock", 50, 0));
		list.Add(new PsAchievement("reachHundredHPCar", "Strong as a horse", "Upgrade Car to 100pp to unlock", 100, (int)PsUpgradeManager.GetBasePerformance(typeof(OffroadCar))));
		list.Add(new PsAchievement("reachHundredHPBike", "Biker", "Upgrade Bike to 100pp to unlock", 100, (int)PsUpgradeManager.GetBasePerformance(typeof(Motorcycle))));
		list.Add(new PsAchievement("reachFiveHundredHPBike", "Pro Biker", "Upgrade Bike to 500pp to unlock", 500, (int)PsUpgradeManager.GetBasePerformance(typeof(Motorcycle))));
		list.Add(new PsAchievement("longTimeInEditor", "Real art takes time", "Spend 180 active minutes in the editor\nto unlock", 180, 0));
		AchievementManager.Initialize(list);
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00045CFA File Offset: 0x000440FA
	public static Achievement GetByIdentifier(string _identifier)
	{
		return AchievementManager.GetByIdentifier(_identifier);
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00045D02 File Offset: 0x00044102
	public static bool IsCompleted(string _identifier)
	{
		return AchievementManager.IsInitialized && AchievementManager.IsCompleted(_identifier);
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00045D18 File Offset: 0x00044118
	public static void IncrementProgress(string _identifier, int _value)
	{
		if (AchievementManager.IsInitialized)
		{
			Debug.Log(string.Concat(new object[] { "Achievement increase: ", _identifier, " -- ", _value }), null);
			List<string> list = AchievementManager.IncrementProgress(_identifier, _value);
			foreach (string text in list)
			{
				if (PsAchievementManager.m_updated != null && !PsAchievementManager.m_updated.Contains(text))
				{
					PsAchievementManager.m_updated.Add(text);
				}
			}
		}
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00045DD0 File Offset: 0x000441D0
	public static void Complete(string _identifier)
	{
		if (AchievementManager.IsInitialized)
		{
			List<string> list = AchievementManager.Complete(_identifier);
			foreach (string text in list)
			{
				if (PsAchievementManager.m_updated != null && !PsAchievementManager.m_updated.Contains(text))
				{
					PsAchievementManager.m_updated.Add(text);
				}
			}
		}
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00045E58 File Offset: 0x00044258
	public static void SendToServer()
	{
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00045E5C File Offset: 0x0004425C
	public static List<Dictionary<string, object>> GetUpdatedAchievements(bool _clear = true)
	{
		List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
		if (AchievementManager.IsInitialized)
		{
			List<Achievement> list2 = PsAchievementManager.All.FindAll((Achievement obj) => PsAchievementManager.m_updated.Contains(obj.id));
			foreach (Achievement achievement in list2)
			{
				list.Add(achievement.ToDict());
			}
			if (_clear)
			{
				PsAchievementManager.m_updated.Clear();
			}
		}
		return list;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00045F04 File Offset: 0x00044304
	public static void UpdateFromList(List<object> _list)
	{
		AchievementManager.UpdateFromList(_list);
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00045F0C File Offset: 0x0004430C
	private static void SendToServer(List<Achievement> _update)
	{
		foreach (Achievement achievement in _update)
		{
			PsMetagameManager.UpsertAchievement(achievement.ToJson());
		}
	}

	// Token: 0x040006CB RID: 1739
	private static List<string> m_updated;
}
