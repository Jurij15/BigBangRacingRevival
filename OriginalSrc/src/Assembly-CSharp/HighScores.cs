using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000425 RID: 1061
public static class HighScores
{
	// Token: 0x06001D66 RID: 7526 RVA: 0x0014FB10 File Offset: 0x0014DF10
	public static string TicksToTimeString(int _ticks)
	{
		float num = (float)_ticks / 60f;
		return ToolBox.getTimeStringFromSeconds(num);
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x0014FB2C File Offset: 0x0014DF2C
	public static float TicksToTime(int _ticks)
	{
		return (float)_ticks / 60f;
	}

	// Token: 0x06001D68 RID: 7528 RVA: 0x0014FB38 File Offset: 0x0014DF38
	public static string TimeScoreToTimeString(int _score)
	{
		float num = (float)_score / 1000f / 60f;
		return ToolBox.getTimeStringFromSeconds(num);
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x0014FB5A File Offset: 0x0014DF5A
	public static float TimeScoreToTime(int _score)
	{
		return (float)_score / 1000f / 60f;
	}

	// Token: 0x06001D6A RID: 7530 RVA: 0x0014FB6A File Offset: 0x0014DF6A
	public static int TimeToTimeScore(float _time)
	{
		return (int)(_time * 1000f * 60f);
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x0014FB7C File Offset: 0x0014DF7C
	public static int TicksToTimeScore(int _ticks, bool _generateRandomFractions)
	{
		Random.seed = (int)(Time.realtimeSinceStartup * 1000f);
		return _ticks * 1000 + ((!_generateRandomFractions) ? 0 : Random.Range(0, 900));
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x0014FBBC File Offset: 0x0014DFBC
	public static ScoreTable ParseScoreTableJSON(HttpC _c)
	{
		ScoreTable scoreTable = new ScoreTable();
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("topScores"))
		{
			List<object> list = dictionary["topScores"] as List<object>;
			HighscoreDataEntry[] array = new HighscoreDataEntry[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
				array[i] = HighScores.ParseDataEntry(dictionary2);
			}
			scoreTable.m_top = array;
		}
		if (dictionary.ContainsKey("followeeScores"))
		{
			List<object> list2 = dictionary["followeeScores"] as List<object>;
			HighscoreDataEntry[] array2 = new HighscoreDataEntry[list2.Count];
			for (int j = 0; j < list2.Count; j++)
			{
				Dictionary<string, object> dictionary3 = list2[j] as Dictionary<string, object>;
				array2[j] = HighScores.ParseDataEntry(dictionary3);
			}
			scoreTable.m_friends = array2;
		}
		if (dictionary.ContainsKey("topRewards"))
		{
			List<object> list3 = dictionary["topRewards"] as List<object>;
			scoreTable.m_topRewards = new int[list3.Count];
			for (int k = 0; k < list3.Count; k++)
			{
				scoreTable.m_topRewards[k] = Convert.ToInt32(list3[k]);
			}
		}
		return scoreTable;
	}

	// Token: 0x06001D6D RID: 7533 RVA: 0x0014FD24 File Offset: 0x0014E124
	public static HighscoreData ParseHighscoreJSON(HttpC _c)
	{
		HighscoreData highscoreData = new HighscoreData();
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		List<object> list = dictionary["globalScores"] as List<object>;
		List<object> list2 = dictionary["followeeScores"] as List<object>;
		HighscoreDataEntry[] array = new HighscoreDataEntry[list.Count];
		for (int i = 0; i < list.Count; i++)
		{
			Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
			array[i] = HighScores.ParseDataEntry(dictionary2);
		}
		HighscoreDataEntry[] array2 = new HighscoreDataEntry[list2.Count];
		for (int j = 0; j < list2.Count; j++)
		{
			Dictionary<string, object> dictionary3 = list2[j] as Dictionary<string, object>;
			array2[j] = HighScores.ParseDataEntry(dictionary3);
		}
		highscoreData.m_entries = array;
		highscoreData.m_friends = array2;
		object obj;
		if (dictionary.TryGetValue("percentile", ref obj))
		{
			highscoreData.m_percentile = Convert.ToInt32(obj);
		}
		if (dictionary.TryGetValue("nextPercentileTime", ref obj))
		{
			highscoreData.m_nextPercentileTime = Convert.ToInt32(obj);
		}
		if (dictionary.TryGetValue("position", ref obj))
		{
			highscoreData.m_position = Convert.ToInt32(obj);
		}
		return highscoreData;
	}

	// Token: 0x06001D6E RID: 7534 RVA: 0x0014FE60 File Offset: 0x0014E260
	public static List<HighscoreDataEntry> ParseDataEntriesJSON(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return HighScores.ParseDataEntriesJSON(dictionary);
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x0014FE84 File Offset: 0x0014E284
	public static List<HighscoreDataEntry> ParseDataEntriesJSON(Dictionary<string, object> _response)
	{
		List<HighscoreDataEntry> list = new List<HighscoreDataEntry>();
		List<HighscoreDataEntry> list2 = new List<HighscoreDataEntry>();
		if (_response.ContainsKey("data"))
		{
			List<object> list3 = _response["data"] as List<object>;
			if (list3 != null)
			{
				foreach (object obj in list3)
				{
					HighscoreDataEntry highscoreDataEntry = HighScores.ParseDataEntry(obj as Dictionary<string, object>);
					if (highscoreDataEntry.time != 0 && highscoreDataEntry.time != 2147483647)
					{
						list.Add(highscoreDataEntry);
					}
					else
					{
						list2.Add(highscoreDataEntry);
					}
				}
			}
			else
			{
				Debug.LogError("Entry parse failed");
			}
		}
		list.AddRange(list2);
		return list;
	}

	// Token: 0x06001D70 RID: 7536 RVA: 0x0014FF60 File Offset: 0x0014E360
	public static HighscoreDataEntry ParseDataEntryJSON(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		HighscoreDataEntry highscoreDataEntry = HighScores.ParseDataEntry(dictionary);
		GhostGetData ghostGetData = (GhostGetData)_c.objectData;
		if (ghostGetData.minigameInfo != null && highscoreDataEntry != null)
		{
			highscoreDataEntry.gameId = ghostGetData.minigameInfo.GetGameId();
		}
		return highscoreDataEntry;
	}

	// Token: 0x06001D71 RID: 7537 RVA: 0x0014FFB4 File Offset: 0x0014E3B4
	public static HighscoreDataEntry ParseDataEntry(Dictionary<string, object> _dict)
	{
		if (!_dict.ContainsKey("n"))
		{
			return null;
		}
		HighscoreDataEntry highscoreDataEntry = new HighscoreDataEntry();
		highscoreDataEntry.name = (string)_dict["n"];
		if (_dict.ContainsKey("t"))
		{
			highscoreDataEntry.time = Convert.ToInt32(_dict["t"]);
		}
		if (_dict.ContainsKey("s"))
		{
			highscoreDataEntry.stars = Convert.ToInt32(_dict["s"]);
		}
		if (_dict.ContainsKey("fb"))
		{
			highscoreDataEntry.facebookId = (string)_dict["fb"];
		}
		if (_dict.ContainsKey("gc"))
		{
			highscoreDataEntry.gameCenterId = (string)_dict["gc"];
		}
		if (_dict.ContainsKey("p"))
		{
			highscoreDataEntry.playerId = (string)_dict["p"];
		}
		if (_dict.ContainsKey("countryCode"))
		{
			highscoreDataEntry.country = (string)_dict["countryCode"];
		}
		if (highscoreDataEntry.time == 0)
		{
			highscoreDataEntry.time = int.MaxValue;
		}
		return highscoreDataEntry;
	}

	// Token: 0x06001D72 RID: 7538 RVA: 0x001500EC File Offset: 0x0014E4EC
	public static Hashtable GenerateDataEntryHashtable(HighscoreDataEntry _dataEntry)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("n", _dataEntry.name);
		hashtable.Add("t", _dataEntry.time);
		hashtable.Add("s", _dataEntry.stars);
		hashtable.Add("fb", _dataEntry.facebookId);
		hashtable.Add("gc", _dataEntry.gameCenterId);
		hashtable.Add("p", _dataEntry.playerId);
		return hashtable;
	}
}
