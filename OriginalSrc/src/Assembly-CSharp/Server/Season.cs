using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiniJSON;

namespace Server
{
	// Token: 0x0200043C RID: 1084
	public static class Season
	{
		// Token: 0x06001E0C RID: 7692 RVA: 0x00155B58 File Offset: 0x00153F58
		public static HttpC Claim(Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/season/claim");
			Hashtable hashtable = new Hashtable();
			List<string> list = new List<string>();
			Hashtable hashtable2 = ClientTools.GeneratePlayerSetData(new Hashtable(), ref list);
			hashtable.Add("update", hashtable2);
			if (!list.Contains("SetData"))
			{
				list.Add("SetData");
			}
			string text = Json.Serialize(hashtable);
			string[] array = null;
			if (list.Count > 0)
			{
				array = list.ToArray();
			}
			return Request.Post(woeurl, "SEASON_CLAIM", text, _okCallback, _failureCallback, _errorCallback, null, array, false);
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00155BE8 File Offset: 0x00153FE8
		public static HttpC GetPreviousSeasons(int _start, int _count, Action<Dictionary<int, Season.SeasonTop>> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/season/previous");
			woeurl.AddParameter("from", _start);
			woeurl.AddParameter("limit", _count);
			ResponseHandler<Dictionary<int, Season.SeasonTop>> responseHandler = new ResponseHandler<Dictionary<int, Season.SeasonTop>>(_okCallback, new Func<HttpC, Dictionary<int, Season.SeasonTop>>(Season.ParsePreviousSeasons));
			return Request.Get(woeurl, "PREVIOUS_SEASONS_GET", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00155C60 File Offset: 0x00154060
		public static Dictionary<int, Season.SeasonTop> ParsePreviousSeasons(HttpC _c)
		{
			Dictionary<int, Season.SeasonTop> dictionary = new Dictionary<int, Season.SeasonTop>();
			Dictionary<string, object> dictionary2 = ClientTools.ParseServerResponse(_c.www.text);
			if (dictionary2.ContainsKey("data"))
			{
				List<object> list = dictionary2["data"] as List<object>;
				for (int i = 0; i < list.Count; i++)
				{
					Season.SeasonTop seasonTop = new Season.SeasonTop();
					Dictionary<string, object> dictionary3 = list[i] as Dictionary<string, object>;
					if (dictionary3.ContainsKey("number"))
					{
						if (!dictionary.ContainsKey(Convert.ToInt32(dictionary3["number"])))
						{
							dictionary.Add(Convert.ToInt32(dictionary3["number"]), seasonTop);
						}
						if (dictionary3.ContainsKey("topTeams"))
						{
							List<object> list2 = dictionary3["topTeams"] as List<object>;
							seasonTop.topTeams = new List<Season.SeasonTop.SeasonTopEntry>(list2.Count);
							int num = 0;
							while (num < list2.Count && num < 3)
							{
								Dictionary<string, object> dictionary4 = list2[num] as Dictionary<string, object>;
								Season.SeasonTop.SeasonTopEntry seasonTopEntry = new Season.SeasonTop.SeasonTopEntry();
								if (dictionary4.ContainsKey("name"))
								{
									seasonTopEntry.name = Convert.ToString(dictionary4["name"]);
								}
								if (dictionary4.ContainsKey("lastSeasonEndScore"))
								{
									seasonTopEntry.score = Convert.ToInt32(dictionary4["lastSeasonEndScore"]);
								}
								seasonTop.topTeams.Add(seasonTopEntry);
								num++;
							}
							dictionary[Convert.ToInt32(dictionary3["number"])].topTeams = Enumerable.ToList<Season.SeasonTop.SeasonTopEntry>(Enumerable.OrderByDescending<Season.SeasonTop.SeasonTopEntry, int>(seasonTop.topTeams, (Season.SeasonTop.SeasonTopEntry o) => o.score));
						}
						if (dictionary3.ContainsKey("topPlayers"))
						{
							List<object> list3 = dictionary3["topPlayers"] as List<object>;
							seasonTop.topPlayers = new List<Season.SeasonTop.SeasonTopEntry>(list3.Count);
							int num2 = 0;
							while (num2 < list3.Count && num2 < 3)
							{
								Dictionary<string, object> dictionary5 = list3[num2] as Dictionary<string, object>;
								Season.SeasonTop.SeasonTopEntry seasonTopEntry2 = new Season.SeasonTop.SeasonTopEntry();
								if (dictionary5.ContainsKey("name"))
								{
									seasonTopEntry2.name = Convert.ToString(dictionary5["name"]);
								}
								if (dictionary5.ContainsKey("totalTrophies"))
								{
									seasonTopEntry2.score = Convert.ToInt32(dictionary5["totalTrophies"]);
								}
								seasonTop.topPlayers.Add(seasonTopEntry2);
								num2++;
							}
							dictionary[Convert.ToInt32(dictionary3["number"])].topPlayers = Enumerable.ToList<Season.SeasonTop.SeasonTopEntry>(Enumerable.OrderByDescending<Season.SeasonTop.SeasonTopEntry, int>(seasonTop.topPlayers, (Season.SeasonTop.SeasonTopEntry o) => o.score));
						}
					}
				}
				return dictionary;
			}
			return new Dictionary<int, Season.SeasonTop>();
		}

		// Token: 0x0200043D RID: 1085
		public class SeasonTop
		{
			// Token: 0x040020B4 RID: 8372
			public int number;

			// Token: 0x040020B5 RID: 8373
			public List<Season.SeasonTop.SeasonTopEntry> topTeams;

			// Token: 0x040020B6 RID: 8374
			public List<Season.SeasonTop.SeasonTopEntry> topPlayers;

			// Token: 0x040020B7 RID: 8375
			public List<Season.SeasonTop.SeasonTopEntry> topCreators;

			// Token: 0x0200043E RID: 1086
			public class SeasonTopEntry
			{
				// Token: 0x040020B8 RID: 8376
				public string name;

				// Token: 0x040020B9 RID: 8377
				public int position;

				// Token: 0x040020BA RID: 8378
				public int score;
			}
		}
	}
}
