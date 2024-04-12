using System;
using System.Collections.Generic;
using System.Linq;
using MiniJSON;

// Token: 0x0200049D RID: 1181
public static class AchievementManager
{
	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060021C3 RID: 8643 RVA: 0x00189468 File Offset: 0x00187868
	public static List<Achievement> All
	{
		get
		{
			return AchievementManager.m_achievements;
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060021C4 RID: 8644 RVA: 0x0018946F File Offset: 0x0018786F
	public static List<Achievement> Completed
	{
		get
		{
			return AchievementManager.m_achievements.FindAll((Achievement obj) => obj.completed);
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060021C5 RID: 8645 RVA: 0x00189498 File Offset: 0x00187898
	public static List<Achievement> Locked
	{
		get
		{
			return AchievementManager.m_achievements.FindAll((Achievement obj) => !obj.completed);
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060021C6 RID: 8646 RVA: 0x001894C1 File Offset: 0x001878C1
	public static string AllJson
	{
		get
		{
			return AchievementManager.ListToJson(AchievementManager.All);
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060021C7 RID: 8647 RVA: 0x001894CD File Offset: 0x001878CD
	public static string CompletedJson
	{
		get
		{
			return AchievementManager.ListToJson(AchievementManager.Completed);
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060021C8 RID: 8648 RVA: 0x001894D9 File Offset: 0x001878D9
	public static string LockedJson
	{
		get
		{
			return AchievementManager.ListToJson(AchievementManager.Locked);
		}
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x060021C9 RID: 8649 RVA: 0x001894E5 File Offset: 0x001878E5
	public static bool IsInitialized
	{
		get
		{
			return AchievementManager.m_initialized;
		}
	}

	// Token: 0x060021CA RID: 8650 RVA: 0x001894EC File Offset: 0x001878EC
	public static void Initialize(List<Achievement> _achievements)
	{
		AchievementManager.m_achievements = _achievements;
		AchievementManager.m_initialized = true;
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x001894FC File Offset: 0x001878FC
	public static Achievement GetByIdentifier(string _identifier)
	{
		for (int i = 0; i < AchievementManager.m_achievements.Count; i++)
		{
			if (AchievementManager.m_achievements[i].id == _identifier)
			{
				return AchievementManager.m_achievements[i];
			}
		}
		return null;
	}

	// Token: 0x060021CC RID: 8652 RVA: 0x0018954C File Offset: 0x0018794C
	public static bool IsCompleted(string _identifier)
	{
		if (AchievementManager.m_initialized)
		{
			bool flag = false;
			if (AchievementManager.m_achievements.Exists((Achievement obj) => obj.id == _identifier))
			{
				flag = Enumerable.Single<Achievement>(AchievementManager.m_achievements, (Achievement obj) => obj.id == _identifier).completed;
			}
			return flag;
		}
		Debug.LogError("AchievementManager is not initialized!");
		return false;
	}

	// Token: 0x060021CD RID: 8653 RVA: 0x001895B8 File Offset: 0x001879B8
	public static List<string> IncrementProgress(string _identifier, int _value)
	{
		List<string> list = new List<string>();
		if (AchievementManager.m_initialized)
		{
			foreach (Achievement achievement in Enumerable.Where<Achievement>(AchievementManager.m_achievements, (Achievement obj) => obj.id == _identifier))
			{
				if (achievement.percentCompleted < 100)
				{
					achievement.IncrementProgress(_value);
					list.Add(_identifier);
				}
				else
				{
					achievement.Complete();
				}
			}
		}
		else
		{
			Debug.LogError("AchievementManager is not initialized!");
		}
		return list;
	}

	// Token: 0x060021CE RID: 8654 RVA: 0x00189674 File Offset: 0x00187A74
	public static List<string> Complete(string _identifier)
	{
		List<string> list = new List<string>();
		if (AchievementManager.m_initialized)
		{
			foreach (Achievement achievement in Enumerable.Where<Achievement>(AchievementManager.m_achievements, (Achievement obj) => obj.id == _identifier))
			{
				if (!achievement.completed)
				{
					achievement.Complete();
					list.Add(_identifier);
				}
				else
				{
					achievement.Complete();
				}
			}
		}
		else
		{
			Debug.LogError("AchievementManager is not initialized!");
		}
		return list;
	}

	// Token: 0x060021CF RID: 8655 RVA: 0x0018972C File Offset: 0x00187B2C
	private static void UpdateProgress(string _identifier, int _value)
	{
		Debug.Log(string.Concat(new object[] { "Achievement update: ", _identifier, " Value: ", _value }), null);
		foreach (Achievement achievement in Enumerable.Where<Achievement>(AchievementManager.m_achievements, (Achievement obj) => obj.id == _identifier))
		{
			achievement.UpdateProgress(_value);
		}
	}

	// Token: 0x060021D0 RID: 8656 RVA: 0x001897D8 File Offset: 0x00187BD8
	private static string ListToJson(List<Achievement> _list)
	{
		List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
		foreach (Achievement achievement in _list)
		{
			list.Add(achievement.ToDict());
		}
		return Json.Serialize(list);
	}

	// Token: 0x060021D1 RID: 8657 RVA: 0x00189840 File Offset: 0x00187C40
	public static void UpdateFromList(List<object> _list)
	{
		if (AchievementManager.m_initialized)
		{
			foreach (object obj in _list)
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
				if (!dictionary.ContainsKey("id") && !dictionary.ContainsKey("value"))
				{
					Debug.LogError("Invalid achievement dictionary");
				}
				else
				{
					Debug.Log((string)dictionary["id"], null);
					AchievementManager.UpdateProgress((string)dictionary["id"], Convert.ToInt32(dictionary["value"]));
				}
			}
		}
		else
		{
			Debug.LogError("AchievementManager is not initialized!");
		}
	}

	// Token: 0x060021D2 RID: 8658 RVA: 0x00189918 File Offset: 0x00187D18
	public static void UpdateFromJson(string _json)
	{
		AchievementManager.UpdateFromList(Json.Deserialize(_json) as List<object>);
	}

	// Token: 0x040027F5 RID: 10229
	private static List<Achievement> m_achievements;

	// Token: 0x040027F6 RID: 10230
	private static bool m_initialized;
}
