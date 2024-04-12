using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CodeStage.AntiCheat.ObscuredTypes;
using MiniJSON;
using Server.Utility;
using UnityEngine;

// Token: 0x02000413 RID: 1043
public static class ClientTools
{
	// Token: 0x06001C92 RID: 7314 RVA: 0x001418BC File Offset: 0x0013FCBC
	public static EventMessage ParseEventMessageFromDict(Dictionary<string, object> _dict)
	{
		EventMessage eventMessage = new EventMessage();
		if (_dict.ContainsKey("eventName"))
		{
			eventMessage.eventName = (string)_dict["eventName"];
		}
		else
		{
			eventMessage.eventName = string.Empty;
		}
		if (_dict.ContainsKey("eventType"))
		{
			eventMessage.eventType = (string)_dict["eventType"];
		}
		if (_dict.ContainsKey("id"))
		{
			eventMessage.messageId = Convert.ToInt32(_dict["id"]);
		}
		else
		{
			eventMessage.messageId = -1;
		}
		if (_dict.ContainsKey("header"))
		{
			eventMessage.header = (string)_dict["header"];
		}
		else
		{
			eventMessage.header = string.Empty;
		}
		if (_dict.ContainsKey("message"))
		{
			eventMessage.message = (string)_dict["message"];
		}
		else
		{
			eventMessage.message = string.Empty;
		}
		if (_dict.ContainsKey("label"))
		{
			eventMessage.label = (string)_dict["label"];
		}
		else
		{
			eventMessage.label = string.Empty;
		}
		if (_dict.ContainsKey("popup"))
		{
			eventMessage.showAtLogin = (bool)_dict["popup"];
		}
		else
		{
			eventMessage.showAtLogin = false;
		}
		if (_dict.ContainsKey("floatingNode"))
		{
			eventMessage.showAsFloatingNode = (bool)_dict["floatingNode"];
		}
		else
		{
			eventMessage.showAsFloatingNode = false;
		}
		if (_dict.ContainsKey("newsFeed"))
		{
			eventMessage.showAtNewsFeed = (bool)_dict["newsFeed"];
		}
		else
		{
			eventMessage.showAtNewsFeed = true;
		}
		if (_dict.ContainsKey("startTime"))
		{
			eventMessage.startTime = Convert.ToInt64(_dict["startTime"]);
		}
		else
		{
			eventMessage.startTime = 0L;
		}
		if (_dict.ContainsKey("endTime"))
		{
			eventMessage.endTime = Convert.ToInt64(_dict["endTime"]);
			eventMessage.endTimeSeconds = (double)((long)_dict["endTime"]) / 1000.0;
		}
		else
		{
			eventMessage.endTime = 0L;
		}
		if (_dict.ContainsKey("uris"))
		{
			eventMessage.uris = new List<string>();
			List<object> list = _dict["uris"] as List<object>;
			foreach (object obj in list)
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
				eventMessage.uris.Add(Convert.ToString(dictionary["uri"]));
			}
		}
		else
		{
			eventMessage.uris = new List<string>();
		}
		if (_dict.ContainsKey("secondsToStart"))
		{
			eventMessage.timeToStart = Convert.ToInt32(_dict["secondsToStart"]);
			eventMessage.localStartTime = (int)Main.m_EPOCHSeconds + eventMessage.timeToStart;
		}
		else
		{
			eventMessage.localStartTime = (int)(eventMessage.startTime / 1000L);
		}
		if (_dict.ContainsKey("secondsLeft"))
		{
			eventMessage.timeLeft = Convert.ToInt32(_dict["secondsLeft"]);
			eventMessage.localEndTime = (int)(Main.m_EPOCHSeconds + (double)eventMessage.timeLeft);
		}
		if (_dict.ContainsKey("eventData"))
		{
			eventMessage.eventData = _dict["eventData"] as Dictionary<string, object>;
			if (eventMessage.eventData.ContainsKey("states"))
			{
				eventMessage.eventStates = ClientTools.ParseEventStates(eventMessage.eventData["states"] as List<object>);
				eventMessage.eventStates.Sort((EventState x, EventState y) => x.id.CompareTo(y.id));
			}
			if (eventMessage.eventType == "Gift")
			{
				eventMessage.giftContent = ClientTools.ParseEventGiftComponent(eventMessage.eventData);
			}
			else if (eventMessage.eventType == "LiveOps")
			{
				LiveOps liveOps = new LiveOps();
				if (eventMessage.eventData.ContainsKey("minigameId"))
				{
					liveOps.minigameId = (string)eventMessage.eventData["minigameId"];
				}
				if (eventMessage.eventData.ContainsKey("pictureUrl"))
				{
					liveOps.pictureUrl = (string)eventMessage.eventData["pictureUrl"];
				}
				if (eventMessage.eventData.ContainsKey("liveOpsEvent"))
				{
					liveOps.liveOpsEvent = (string)eventMessage.eventData["liveOpsEvent"];
				}
				eventMessage.liveOps = liveOps;
			}
			else if (eventMessage.eventType == "Tournament")
			{
				string text = string.Empty;
				string text2 = string.Empty;
				float num = -1f;
				int num2 = 500;
				bool flag = true;
				string text3 = string.Empty;
				string text4 = string.Empty;
				string text5 = string.Empty;
				string text6 = string.Empty;
				int num3 = 0;
				string text7 = string.Empty;
				string text8 = string.Empty;
				bool flag2 = false;
				if (eventMessage.eventData.ContainsKey("minigameId"))
				{
					text2 = (string)eventMessage.eventData["minigameId"];
				}
				if (eventMessage.eventData.ContainsKey("tournamentId"))
				{
					text = (string)eventMessage.eventData["tournamentId"];
				}
				if (eventMessage.eventData.ContainsKey("ccCap"))
				{
					num = Convert.ToSingle(eventMessage.eventData["ccCap"]);
				}
				if (eventMessage.eventData.ContainsKey("prizeCoins"))
				{
					num2 = Convert.ToInt32(eventMessage.eventData["prizeCoins"]);
				}
				if (eventMessage.eventData.ContainsKey("acceptingNewScores"))
				{
					flag = Convert.ToBoolean(eventMessage.eventData["acceptingNewScores"]);
				}
				if (eventMessage.eventData.ContainsKey("ownerId"))
				{
					text3 = (string)eventMessage.eventData["ownerId"];
				}
				if (eventMessage.eventData.ContainsKey("ownerName"))
				{
					text4 = (string)eventMessage.eventData["ownerName"];
				}
				if (eventMessage.eventData.ContainsKey("ownerFacebookId"))
				{
					text5 = (string)eventMessage.eventData["ownerFacebookId"];
				}
				if (eventMessage.eventData.ContainsKey("youtuber"))
				{
					text6 = (string)eventMessage.eventData["youtuber"];
				}
				if (eventMessage.eventData.ContainsKey("youtubeSubscriberCount"))
				{
					num3 = Convert.ToInt32(eventMessage.eventData["youtubeSubscriberCount"]);
				}
				if (eventMessage.eventData.ContainsKey("youtuberId"))
				{
					text7 = (string)eventMessage.eventData["youtuberId"];
				}
				if (eventMessage.eventData.ContainsKey("playerUnit"))
				{
					text8 = (string)eventMessage.eventData["playerUnit"];
				}
				if (eventMessage.eventData.ContainsKey("useCreatorUpgrades"))
				{
					flag2 = Convert.ToBoolean(eventMessage.eventData["useCreatorUpgrades"]);
				}
				TournamentInfo tournamentInfo = new TournamentInfo();
				tournamentInfo.minigameId = text2;
				tournamentInfo.tournamentId = text;
				tournamentInfo.cc = num;
				tournamentInfo.prizeCoins = num2;
				tournamentInfo.acceptingNewScores = flag;
				tournamentInfo.ownerId = text3;
				tournamentInfo.ownerName = text4;
				tournamentInfo.ownerFacebookId = text5;
				tournamentInfo.youtuberName = text6;
				tournamentInfo.youtubeSubscriberCount = num3;
				tournamentInfo.youtuberId = text7;
				tournamentInfo.playerUnit = text8;
				tournamentInfo.useCreatorUpgrades = flag2;
				Tournaments.ParseTournamentNitroPrices(tournamentInfo, eventMessage.eventData);
				eventMessage.tournament = tournamentInfo;
			}
		}
		else
		{
			eventMessage.eventData = null;
		}
		return eventMessage;
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x001420F4 File Offset: 0x001404F4
	public static EventGiftComponent ParseEventGiftComponent(Dictionary<string, object> _dict)
	{
		EventGiftComponent eventGiftComponent = null;
		if (_dict.ContainsKey("type"))
		{
			string text = (string)_dict["type"];
			if (text != null)
			{
				if (ClientTools.<>f__switch$map3 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
					dictionary.Add("upgradeItem", 0);
					dictionary.Add("editorItem", 1);
					dictionary.Add("chest", 2);
					dictionary.Add("timed", 3);
					dictionary.Add("resource", 4);
					dictionary.Add("hat", 5);
					dictionary.Add("trail", 6);
					ClientTools.<>f__switch$map3 = dictionary;
				}
				int num;
				if (ClientTools.<>f__switch$map3.TryGetValue(text, ref num))
				{
					switch (num)
					{
					case 0:
						eventGiftComponent = new EventGiftUpgradeItem(_dict);
						goto IL_13A;
					case 1:
						eventGiftComponent = new EventGiftEditorItem(_dict);
						goto IL_13A;
					case 2:
						eventGiftComponent = new EventGiftChest(_dict);
						goto IL_13A;
					case 3:
						eventGiftComponent = EventGiftTimed.GetTimedType(_dict);
						goto IL_13A;
					case 4:
						eventGiftComponent = EventGiftResource.GetResourceObject(_dict);
						goto IL_13A;
					case 5:
						eventGiftComponent = new EventGiftHat(_dict);
						goto IL_13A;
					case 6:
						eventGiftComponent = new EventGiftTrail(_dict);
						goto IL_13A;
					}
				}
			}
			Debug.LogError("Not found type: " + text);
			IL_13A:;
		}
		else
		{
			Debug.LogError("Did not have giftcontent type: " + Json.Serialize(_dict));
		}
		if (_dict.ContainsKey("texture"))
		{
			eventGiftComponent.texture = Convert.ToInt32(_dict["texture"]);
		}
		return eventGiftComponent;
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x0014227C File Offset: 0x0014067C
	public static List<EventState> ParseEventStates(List<object> _stateList)
	{
		List<EventState> list = new List<EventState>();
		for (int i = 0; i < _stateList.Count; i++)
		{
			EventState eventState = new EventState();
			Dictionary<string, object> dictionary = _stateList[i] as Dictionary<string, object>;
			if (dictionary.ContainsKey("id"))
			{
				eventState.id = Convert.ToInt32(dictionary["id"]);
			}
			if (dictionary.ContainsKey("header"))
			{
				eventState.title = Convert.ToString(dictionary["header"]);
			}
			if (dictionary.ContainsKey("message"))
			{
				eventState.description = Convert.ToString(dictionary["message"]);
			}
			if (dictionary.ContainsKey("timeLeft"))
			{
				eventState.timeLeft = Convert.ToInt64(dictionary["timeLeft"]) / 1000L;
				eventState.localEndTime = (long)(Main.m_EPOCHSeconds + 0.5) + eventState.timeLeft;
			}
			if (dictionary.ContainsKey("mainVideo"))
			{
				eventState.mainVideoUrl = Convert.ToString(dictionary["mainVideo"]);
			}
			if (dictionary.ContainsKey("mainSearchUrl"))
			{
				eventState.mainSearchUrl = Convert.ToString(dictionary["mainSearchUrl"]);
			}
			if (dictionary.ContainsKey("mainParticipateUrl"))
			{
				eventState.mainParticipateUrl = Convert.ToString(dictionary["mainParticipateUrl"]);
			}
			if (dictionary.ContainsKey("adventureWinner"))
			{
				eventState.adventureWinner = ClientTools.ParseLevelEntry(dictionary["adventureWinner"] as Dictionary<string, object>);
			}
			if (dictionary.ContainsKey("raceWinner"))
			{
				eventState.raceWinner = ClientTools.ParseLevelEntry(dictionary["raceWinner"] as Dictionary<string, object>);
			}
			if (dictionary.ContainsKey("runnerUps"))
			{
				eventState.entries = ClientTools.ParseLevelEntries(dictionary["runnerUps"] as List<object>);
			}
			list.Add(eventState);
		}
		return list;
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x00142470 File Offset: 0x00140870
	public static List<LevelEntry> ParseLevelEntries(List<object> _entryList)
	{
		List<LevelEntry> list = new List<LevelEntry>();
		for (int i = 0; i < _entryList.Count; i++)
		{
			list.Add(ClientTools.ParseLevelEntry(_entryList[i] as Dictionary<string, object>));
		}
		return list;
	}

	// Token: 0x06001C96 RID: 7318 RVA: 0x001424B4 File Offset: 0x001408B4
	public static LevelEntry ParseLevelEntry(Dictionary<string, object> _dict)
	{
		LevelEntry levelEntry = new LevelEntry();
		if (_dict.ContainsKey("id"))
		{
			levelEntry.levelId = Convert.ToString(_dict["id"]);
		}
		if (_dict.ContainsKey("name"))
		{
			levelEntry.levelName = Convert.ToString(_dict["name"]);
		}
		if (_dict.ContainsKey("creatorName"))
		{
			levelEntry.creatorName = Convert.ToString(_dict["creatorName"]);
		}
		if (_dict.ContainsKey("videoUrl"))
		{
			levelEntry.videoUrl = Convert.ToString(_dict["videoUrl"]);
		}
		return levelEntry;
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x00142560 File Offset: 0x00140960
	public static string GetTeamJoinTypeName(JoinType _joinType)
	{
		if (_joinType == JoinType.Closed)
		{
			return PsStrings.Get(StringID.TEAM_LIMIT_CLOSED);
		}
		if (_joinType == JoinType.Open)
		{
			return PsStrings.Get(StringID.TEAM_LIMIT_OPEN);
		}
		if (_joinType != JoinType.FriendsOnly)
		{
			return string.Empty;
		}
		return PsStrings.Get(StringID.TEAM_LIMIT_FRIENDS_ONLY);
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x001425AC File Offset: 0x001409AC
	public static string GetTeamJoinTypeName(int _joinIndex)
	{
		return ClientTools.GetTeamJoinTypeName((JoinType)_joinIndex);
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x001425C4 File Offset: 0x001409C4
	public static string gpw(string s)
	{
		Random.seed = 97139634;
		char[] array = s.ToCharArray();
		char[] array2 = ToolBox.shuffleArray(array);
		return new string(array2);
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x001425F0 File Offset: 0x001409F0
	public static string GenerateHash(string _string)
	{
		string text = _string + ClientTools.gpw(ServerConfig.GetHPW());
		return ToolBox.SHA256Sum(text);
	}

	// Token: 0x06001C9B RID: 7323 RVA: 0x00142614 File Offset: 0x00140A14
	public static string GenerateHash(string _string, byte[] _data)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		byte[] bytes = utf8Encoding.GetBytes(_string + ClientTools.gpw(ServerConfig.GetHPW()));
		byte[] array = new byte[_data.Length + bytes.Length];
		_data.CopyTo(array, 0);
		bytes.CopyTo(array, _data.Length);
		return ToolBox.SHA256Sum(array);
	}

	// Token: 0x06001C9C RID: 7324 RVA: 0x00142664 File Offset: 0x00140A64
	public static string GenerateHash(byte[] _data)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		byte[] bytes = utf8Encoding.GetBytes(ClientTools.gpw(ServerConfig.GetHPW()));
		byte[] array = new byte[_data.Length + bytes.Length];
		_data.CopyTo(array, 0);
		bytes.CopyTo(array, _data.Length);
		return ToolBox.SHA256Sum(array);
	}

	// Token: 0x06001C9D RID: 7325 RVA: 0x001426B0 File Offset: 0x00140AB0
	public static Dictionary<string, object> ParseServerResponse(string _response)
	{
		if (!_response.StartsWith("{"))
		{
			Debug.LogError("Server response not valid JSON: " + _response);
			return null;
		}
		Debug.Log("<color=cyan>RESPONSE</color>: " + _response, null);
		Dictionary<string, object> dictionary = Json.Deserialize(_response) as Dictionary<string, object>;
		if (ClientTools.ServerResponseOk(dictionary))
		{
			return dictionary;
		}
		Debug.LogWarning("ERROR RESPONSE: " + dictionary["error"]);
		return dictionary;
	}

	// Token: 0x06001C9E RID: 7326 RVA: 0x00142724 File Offset: 0x00140B24
	public static bool ServerResponseOk(Dictionary<string, object> dict)
	{
		if (dict == null)
		{
			Debug.LogError("NULL DICTIONARY!!!");
			return false;
		}
		return true;
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x0014273C File Offset: 0x00140B3C
	public static SeasonEndData ParseSeasonEndData(Dictionary<string, object> _json)
	{
		SeasonEndData seasonEndData = null;
		if (_json.ContainsKey("currentSeason"))
		{
			seasonEndData = new SeasonEndData();
			Dictionary<string, object> dictionary = _json["currentSeason"] as Dictionary<string, object>;
			seasonEndData.currentSeason = ClientTools.ParseSeason(dictionary);
		}
		if (seasonEndData != null)
		{
			if (_json.ContainsKey("latestSeason"))
			{
				Dictionary<string, object> dictionary2 = _json["latestSeason"] as Dictionary<string, object>;
				seasonEndData.latestSeason = ClientTools.ParseSeason(dictionary2);
			}
			if (_json.ContainsKey("bigBangPoints"))
			{
				seasonEndData.bigBangPoints = Convert.ToInt32(_json["bigBangPoints"]);
			}
			if (_json.ContainsKey("mcTrophies"))
			{
				seasonEndData.mcTrophies = Convert.ToInt32(_json["mcTrophies"]);
			}
			if (_json.ContainsKey("carTrophies"))
			{
				seasonEndData.carTrophies = Convert.ToInt32(_json["carTrophies"]);
			}
			if (_json.ContainsKey("seasonTeamReward"))
			{
				seasonEndData.seasonTeamReward = Convert.ToInt32(_json["seasonTeamReward"]);
			}
			if (_json.ContainsKey("teamEligibleForRewards"))
			{
				seasonEndData.teamEligibleForRewards = Convert.ToBoolean(_json["teamEligibleForRewards"]);
			}
			if (seasonEndData != null && seasonEndData.currentSeason.number > seasonEndData.latestSeason.number)
			{
				PlayerPrefsX.SetSeasonEnded(true);
			}
		}
		return seasonEndData;
	}

	// Token: 0x06001CA0 RID: 7328 RVA: 0x00142898 File Offset: 0x00140C98
	public static Season ParseSeason(Dictionary<string, object> _json)
	{
		Season season = new Season();
		if (_json.ContainsKey("number"))
		{
			season.number = Convert.ToInt32(_json["number"]);
		}
		if (_json.ContainsKey("timeLeft"))
		{
			season.timeLeft = Convert.ToInt32(_json["timeLeft"]);
		}
		return season;
	}

	// Token: 0x06001CA1 RID: 7329 RVA: 0x001428F8 File Offset: 0x00140CF8
	public static GhostData[] ParseGhostData(HttpC _c)
	{
		GhostData ghostData = new GhostData();
		Dictionary<string, string> responseHeaders = _c.www.responseHeaders;
		if (responseHeaders.ContainsKey("GHOST_TIME"))
		{
			ghostData.time = Convert.ToInt32(responseHeaders["GHOST_TIME"]);
		}
		if (responseHeaders.ContainsKey("GHOST_NAME"))
		{
			ghostData.name = WWW.UnEscapeURL(responseHeaders["GHOST_NAME"], Encoding.UTF8);
		}
		byte[] bytes = _c.www.bytes;
		ghostData.ghost = bytes;
		return new GhostData[] { ghostData };
	}

	// Token: 0x06001CA2 RID: 7330 RVA: 0x00142988 File Offset: 0x00140D88
	public static GhostData[] ParseGhostDatas(HttpC _c)
	{
		Dictionary<string, string> responseHeaders = _c.www.responseHeaders;
		string text = string.Empty;
		if (!responseHeaders.ContainsKey("FILE_SIZES") || string.IsNullOrEmpty(responseHeaders["FILE_SIZES"]))
		{
			Debug.LogError("No ghosts or error");
			return null;
		}
		text = responseHeaders["FILE_SIZES"];
		int[] array = Enumerable.ToArray<int>(Enumerable.Select<string, int>(text.Split(new char[] { ',' }), new Func<string, int>(Convert.ToInt32)));
		byte[] bytes = _c.www.bytes;
		GhostData[] array2 = new GhostData[array.Length / 2];
		for (int i = 0; i < array.Length; i += 2)
		{
			byte[] array3 = Enumerable.ToArray<byte>(Enumerable.Skip<byte>(bytes, Enumerable.Sum(Enumerable.Take<int>(array, i))));
			GhostData ghostData = new GhostData();
			byte[] array4 = Enumerable.ToArray<byte>(Enumerable.Take<byte>(array3, array[i]));
			byte[] array5 = Enumerable.ToArray<byte>(Enumerable.Skip<byte>(array3, array[i]));
			ghostData.ghost = Enumerable.ToArray<byte>(Enumerable.Take<byte>(array5, array[i + 1]));
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(utf8Encoding.GetString(array4));
			if (dictionary.ContainsKey("playerId"))
			{
				ghostData.playerId = (string)dictionary["playerId"];
			}
			if (dictionary.ContainsKey("name"))
			{
				ghostData.name = (string)dictionary["name"];
			}
			if (dictionary.ContainsKey("time"))
			{
				ghostData.time = Convert.ToInt32(dictionary["time"]);
			}
			if (dictionary.ContainsKey("trophyWin"))
			{
				ghostData.trophyWin = Convert.ToInt32(dictionary["trophyWin"]);
			}
			if (dictionary.ContainsKey("trophyLose"))
			{
				ghostData.trophyLoss = Convert.ToInt32(dictionary["trophyLose"]);
			}
			if (dictionary.ContainsKey("ghostWin"))
			{
				ghostData.ghostWin = Convert.ToInt32(dictionary["ghostWin"]);
			}
			if (dictionary.ContainsKey("ghostLose"))
			{
				ghostData.ghostLoss = Convert.ToInt32(dictionary["ghostLose"]);
			}
			if (dictionary.ContainsKey("ghostId"))
			{
				ghostData.ghostId = (string)dictionary["ghostId"];
			}
			if (dictionary.ContainsKey("trophies"))
			{
				ghostData.trophies = Convert.ToInt32(dictionary["trophies"]);
			}
			if (dictionary.ContainsKey("facebookId"))
			{
				ghostData.facebookId = (string)dictionary["facebookId"];
			}
			if (dictionary.ContainsKey("gameCenterId"))
			{
				ghostData.gameCenterId = (string)dictionary["gameCenterId"];
			}
			if (dictionary.ContainsKey("countryCode"))
			{
				ghostData.countryCode = (string)dictionary["countryCode"];
			}
			if (dictionary.ContainsKey("teamId"))
			{
				ghostData.teamId = (string)dictionary["teamId"];
			}
			if (dictionary.ContainsKey("teamName"))
			{
				ghostData.teamName = (string)dictionary["teamName"];
			}
			if (dictionary.ContainsKey("version"))
			{
				ghostData.version = Convert.ToInt32(dictionary["version"]);
			}
			array2[i / 2] = ghostData;
		}
		return array2;
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x00142D39 File Offset: 0x00141139
	public static byte[] ParseBytes(HttpC _c)
	{
		return _c.www.bytes;
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x00142D48 File Offset: 0x00141148
	public static Texture2D ParseTexture(HttpC _c)
	{
		Texture2D texture2D = new Texture2D(10, 10, 3, false);
		_c.www.LoadImageIntoTexture(texture2D);
		return texture2D;
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x00142D70 File Offset: 0x00141170
	public static SeasonConfig ParseSeasonConfig(Dictionary<string, object> _dictionary)
	{
		SeasonConfig seasonConfig = new SeasonConfig();
		if (_dictionary.ContainsKey("rewardLimits"))
		{
			seasonConfig.rewardLimits = ClientTools.ParseIntList(_dictionary["rewardLimits"] as List<object>);
		}
		else
		{
			seasonConfig.rewardLimits = new int[] { 4000, 8000, 12000, 15000, 20000, 25000, 30000, 40000, 50000, 60000 };
		}
		if (_dictionary.ContainsKey("finalTierCoinReward"))
		{
			seasonConfig.finalTierCoinReward = Convert.ToInt32(_dictionary["finalTierCoinReward"]);
		}
		if (_dictionary.ContainsKey("coinTopTenRewards"))
		{
			seasonConfig.coinTopTenRewards = ClientTools.ParseIntList(_dictionary["coinTopTenRewards"] as List<object>);
		}
		if (_dictionary.ContainsKey("coinRewardAmounts"))
		{
			seasonConfig.coinRewardAmounts = ClientTools.ParseIntList(_dictionary["coinRewardAmounts"] as List<object>);
		}
		return seasonConfig;
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x00142E48 File Offset: 0x00141248
	private static int[] ParseIntList(List<object> _values)
	{
		int[] array = new int[_values.Count];
		for (int i = 0; i < _values.Count; i++)
		{
			array[i] = Convert.ToInt32(_values[i]);
		}
		return array;
	}

	// Token: 0x06001CA7 RID: 7335 RVA: 0x00142E88 File Offset: 0x00141288
	public static ClientConfig ParsePlayerConfig(Dictionary<string, object> _dictionary, ClientConfig _c)
	{
		Debug.Log("Parse initial player config", null);
		if (_dictionary.ContainsKey("coins"))
		{
			_c.coinsAtStart = Convert.ToInt32(_dictionary["coins"]);
		}
		else
		{
			_c.coinsAtStart = 1000;
		}
		if (_dictionary.ContainsKey("diamonds"))
		{
			_c.diamondsAtStart = Convert.ToInt32(_dictionary["diamonds"]);
		}
		else
		{
			_c.diamondsAtStart = 50;
		}
		if (_dictionary.ContainsKey("bolts"))
		{
			_c.boltsAtStart = Convert.ToInt32(_dictionary["bolts"]);
		}
		else
		{
			_c.boltsAtStart = 0;
		}
		if (_dictionary.ContainsKey("keys"))
		{
			_c.keysAtStart = Convert.ToInt32(_dictionary["keys"]);
		}
		else
		{
			_c.keysAtStart = 5;
		}
		return _c;
	}

	// Token: 0x06001CA8 RID: 7336 RVA: 0x00142F70 File Offset: 0x00141370
	public static PlayerStatistics ParsePlayerStatistics(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParsePlayerStatistics(dictionary);
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x00142F94 File Offset: 0x00141394
	public static PlayerStatistics ParsePlayerStatistics(Dictionary<string, object> _dictionary)
	{
		PlayerStatistics playerStatistics = default(PlayerStatistics);
		if (_dictionary.ContainsKey("versusPlays"))
		{
			playerStatistics.versusPlays = Convert.ToInt32(_dictionary["versusPlays"]);
		}
		if (_dictionary.ContainsKey("versusWins"))
		{
			playerStatistics.versusWins = Convert.ToInt32(_dictionary["versusWins"]);
		}
		if (_dictionary.ContainsKey("versusLosses"))
		{
			playerStatistics.versusLosses = Convert.ToInt32(_dictionary["versusLosses"]);
		}
		if (_dictionary.ContainsKey("mcVersusStats"))
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)_dictionary["mcVersusStats"];
			if (dictionary.ContainsKey("totalPlays"))
			{
				playerStatistics.motorcycleVersusPlays = Convert.ToInt32(dictionary["totalPlays"]);
			}
			if (dictionary.ContainsKey("wins"))
			{
				playerStatistics.motorcycleVersusWins = Convert.ToInt32(dictionary["wins"]);
			}
			if (dictionary.ContainsKey("losses"))
			{
				playerStatistics.motorcycleVersusLosses = Convert.ToInt32(dictionary["losses"]);
			}
		}
		if (_dictionary.ContainsKey("carVersusStats"))
		{
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)_dictionary["carVersusStats"];
			if (dictionary2.ContainsKey("totalPlays"))
			{
				playerStatistics.offroadCarVersusPlays = Convert.ToInt32(dictionary2["totalPlays"]);
			}
			if (dictionary2.ContainsKey("wins"))
			{
				playerStatistics.offroadCarVersusWins = Convert.ToInt32(dictionary2["wins"]);
			}
			if (dictionary2.ContainsKey("losses"))
			{
				playerStatistics.offroadCarVersusLosses = Convert.ToInt32(dictionary2["losses"]);
			}
		}
		return playerStatistics;
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x0014314C File Offset: 0x0014154C
	public static List<string> ParseAvailableEditorItemsList(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		List<object> list = dictionary["data"] as List<object>;
		return ClientTools.ParseAvailableEditorItems(list);
	}

	// Token: 0x06001CAB RID: 7339 RVA: 0x00143184 File Offset: 0x00141584
	public static List<string> ParseAvailableEditorItems(List<object> _data)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < _data.Count; i++)
		{
			Dictionary<string, object> dictionary = _data[i] as Dictionary<string, object>;
			list.Add((string)dictionary["className"]);
		}
		return list;
	}

	// Token: 0x06001CAC RID: 7340 RVA: 0x001431D4 File Offset: 0x001415D4
	public static CommentData[] ParseComments(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("data"))
		{
			List<object> list = dictionary["data"] as List<object>;
			CommentData[] array = new CommentData[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
				if (dictionary2.ContainsKey("playerId"))
				{
					array[i].playerId = (string)dictionary2["playerId"];
				}
				if (dictionary2.ContainsKey("comment"))
				{
					array[i].comment = (string)dictionary2["comment"];
				}
				else if (dictionary2.ContainsKey("message"))
				{
					array[i].comment = (string)dictionary2["message"];
				}
				if (dictionary2.ContainsKey("name"))
				{
					array[i].name = (string)dictionary2["name"];
				}
				if (dictionary2.ContainsKey("tag"))
				{
					array[i].tag = (string)dictionary2["tag"];
				}
				if (dictionary2.ContainsKey("facebookId"))
				{
					array[i].facebookId = (string)dictionary2["facebookId"];
				}
				if (dictionary2.ContainsKey("gameCenterId"))
				{
					array[i].gameCenterId = (string)dictionary2["gameCenterId"];
				}
				if (dictionary2.ContainsKey("admin"))
				{
					array[i].admin = (bool)dictionary2["admin"];
				}
				if (dictionary2.ContainsKey("publishTime"))
				{
					Dictionary<string, object> dictionary3 = (Dictionary<string, object>)dictionary2["publishTime"];
					array[i].timestamp = (long)dictionary3["$date"];
				}
				if (dictionary2.ContainsKey("type"))
				{
					array[i].type = (string)dictionary2["type"];
				}
				if (dictionary2.ContainsKey("customData"))
				{
					CommentCustomData commentCustomData = default(CommentCustomData);
					Dictionary<string, object> dictionary4 = dictionary2["customData"] as Dictionary<string, object>;
					if (dictionary4.ContainsKey("gameId"))
					{
						commentCustomData.gameId = (string)dictionary4["gameId"];
					}
					if (dictionary4.ContainsKey("gameMode"))
					{
						commentCustomData.gameMode = (string)dictionary4["gameMode"];
					}
					if (dictionary4.ContainsKey("kickedName"))
					{
						commentCustomData.kickedName = (string)dictionary4["kickedName"];
					}
					if (dictionary4.ContainsKey("score"))
					{
						commentCustomData.score = Convert.ToInt32(dictionary4["score"]);
					}
					if (dictionary4.ContainsKey("reward"))
					{
						commentCustomData.reward = Convert.ToInt32(dictionary4["reward"]);
					}
					if (dictionary4.ContainsKey("text"))
					{
						commentCustomData.text = (string)dictionary4["text"];
					}
					if (dictionary4.ContainsKey("rewardType"))
					{
						commentCustomData.rewardType = (string)dictionary4["rewardType"];
					}
					else
					{
						commentCustomData.rewardType = "gem";
					}
					array[i].customData = commentCustomData;
				}
			}
			return array;
		}
		return new CommentData[0];
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x0014359C File Offset: 0x0014199C
	public static List<string> ParseLikes(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		object[] array = (dictionary["data"] as List<object>).ToArray();
		List<string> list = new List<string>();
		for (int i = 0; i < array.Length; i++)
		{
			list.Add((string)array[i]);
		}
		return list;
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x001435FC File Offset: 0x001419FC
	public static FeedData[] ParseGameFeedList(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		List<object> list = dictionary["data"] as List<object>;
		FeedData[] array = new FeedData[list.Count];
		for (int i = 0; i < list.Count; i++)
		{
			Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
			string text = (string)dictionary2["type"];
			array[i].targetId = (string)dictionary2["targetId"];
			array[i].type = text;
			Dictionary<string, object> dictionary3 = (Dictionary<string, object>)dictionary2["ts"];
			array[i].timestamp = new DateTime((long)dictionary3["$date"]);
			array[i].message = (string)dictionary2["message"];
			if (text.Equals("game"))
			{
				if (dictionary2.ContainsKey("friendCreator"))
				{
					array[i].friendCreator = (string)dictionary2["friendCreator"];
				}
				if (dictionary2.ContainsKey("friendPlayers"))
				{
					object[] array2 = (dictionary2["friendPlayers"] as List<object>).ToArray();
					array[i].friendPlayers = new string[array2.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array[i].friendPlayers[j] = (string)array2[j];
					}
				}
			}
		}
		return array;
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x0014379C File Offset: 0x00141B9C
	public static PsMinigameMetaData ParseTemplate(HttpC _c)
	{
		PsMinigameMetaData psMinigameMetaData = ClientTools.ParseMinigameMetaData(_c);
		psMinigameMetaData.template = true;
		return psMinigameMetaData;
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x001437B8 File Offset: 0x00141BB8
	public static PsMinigameMetaData ParseMinigameMetaData(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParseMinigameMetaData(dictionary);
	}

	// Token: 0x06001CB1 RID: 7345 RVA: 0x001437DC File Offset: 0x00141BDC
	public static PsMinigameMetaData ParseTemplate(Dictionary<string, object> _dict)
	{
		if (_dict == null || _dict.Count <= 1)
		{
			return null;
		}
		PsMinigameMetaData psMinigameMetaData = ClientTools.ParseMinigameMetaData(_dict);
		psMinigameMetaData.template = true;
		return psMinigameMetaData;
	}

	// Token: 0x06001CB2 RID: 7346 RVA: 0x0014380C File Offset: 0x00141C0C
	public static PsMinigameMetaData ParseMinigameMetaData(Dictionary<string, object> _dict)
	{
		if (_dict == null || _dict.Count <= 1)
		{
			return null;
		}
		if (!_dict.ContainsKey("error"))
		{
			PsMinigameMetaData psMinigameMetaData = new PsMinigameMetaData();
			psMinigameMetaData.name = (string)_dict["name"];
			psMinigameMetaData.id = (string)_dict["id"];
			if (_dict.ContainsKey("creatorId"))
			{
				psMinigameMetaData.creatorId = (string)_dict["creatorId"];
			}
			else
			{
				psMinigameMetaData.creatorId = "??";
			}
			if (_dict.ContainsKey("timeSpentEditing"))
			{
				psMinigameMetaData.timeSpentEditing = Convert.ToInt32(_dict["timeSpentEditing"]);
			}
			if (_dict.ContainsKey("timeSpentInEditMode"))
			{
				psMinigameMetaData.timeSpentInEditMode = Convert.ToInt32(_dict["timeSpentInEditMode"]);
			}
			if (_dict.ContainsKey("editSessionCount"))
			{
				psMinigameMetaData.editSessionCount = Convert.ToInt32(_dict["editSessionCount"]);
			}
			if (_dict.ContainsKey("groundsModificationCount"))
			{
				psMinigameMetaData.groundsModificationCount = Convert.ToInt32(_dict["groundsModificationCount"]);
			}
			if (_dict.ContainsKey("itemsModificationCount"))
			{
				psMinigameMetaData.itemsModificationCount = Convert.ToInt32(_dict["itemsModificationCount"]);
			}
			if (_dict.ContainsKey("lastPlaySessionStartCount"))
			{
				psMinigameMetaData.lastPlaySessionStartCount = Convert.ToInt32(_dict["lastPlaySessionStartCount"]);
			}
			if (_dict.ContainsKey("creatorFacebookId"))
			{
				psMinigameMetaData.creatorFacebookId = (string)_dict["creatorFacebookId"];
			}
			if (_dict.ContainsKey("creatorGameCenterId"))
			{
				psMinigameMetaData.creatorGameCenterId = (string)_dict["creatorGameCenterId"];
			}
			if (_dict.ContainsKey("creatorName"))
			{
				psMinigameMetaData.creatorName = (string)_dict["creatorName"];
			}
			else
			{
				psMinigameMetaData.creatorName = "??";
			}
			if (_dict.ContainsKey("videoUrl"))
			{
				psMinigameMetaData.videoUrl = (string)_dict["videoUrl"];
			}
			if (_dict.ContainsKey("countryCode"))
			{
				psMinigameMetaData.creatorCountryCode = (string)_dict["countryCode"];
			}
			if (_dict.ContainsKey("publishTime"))
			{
				psMinigameMetaData.published = true;
			}
			else
			{
				psMinigameMetaData.published = false;
			}
			if (_dict.ContainsKey("description"))
			{
				psMinigameMetaData.description = (string)_dict["description"];
			}
			if (_dict.ContainsKey("timesLiked"))
			{
				psMinigameMetaData.timesLiked = Convert.ToInt32(_dict["timesLiked"]);
			}
			if (_dict.ContainsKey("upThumbs"))
			{
				psMinigameMetaData.upThumbs = Convert.ToInt32(_dict["upThumbs"]);
			}
			if (_dict.ContainsKey("downThumbs"))
			{
				psMinigameMetaData.downThumbs = Convert.ToInt32(_dict["downThumbs"]);
			}
			if (_dict.ContainsKey("timesSuperLiked"))
			{
				psMinigameMetaData.timesSuperLiked = Convert.ToInt32(_dict["timesSuperLiked"]);
			}
			if (_dict.ContainsKey("timesAbused"))
			{
				psMinigameMetaData.timesAbused = Convert.ToInt32(_dict["timesAbused"]);
			}
			if (_dict.ContainsKey("timesRated"))
			{
				psMinigameMetaData.timesRated = Convert.ToInt32(_dict["timesRated"]);
			}
			if (_dict.ContainsKey("timesPlayed"))
			{
				psMinigameMetaData.timesPlayed = Convert.ToInt32(_dict["timesPlayed"]);
			}
			else
			{
				psMinigameMetaData.timesPlayed = 0;
			}
			if (_dict.ContainsKey("clientVersion"))
			{
				double num = Convert.ToDouble(_dict["clientVersion"]);
				psMinigameMetaData.clientVersion = (int)num;
			}
			if (_dict.ContainsKey("played"))
			{
				psMinigameMetaData.played = (bool)_dict["played"];
			}
			if (_dict.ContainsKey("score"))
			{
				psMinigameMetaData.score = Convert.ToInt32(_dict["score"]);
			}
			if (_dict.ContainsKey("researchIdentifier"))
			{
				psMinigameMetaData.researchIdentifier = (string)_dict["researchIdentifier"];
			}
			if (_dict.ContainsKey("totalWinners"))
			{
				psMinigameMetaData.totalWinners = Convert.ToInt32(_dict["totalWinners"]);
			}
			if (_dict.ContainsKey("oneStarWinners"))
			{
				psMinigameMetaData.oneStarWinners = Convert.ToInt32(_dict["oneStarWinners"]);
			}
			if (_dict.ContainsKey("twoStarWinners"))
			{
				psMinigameMetaData.twoStarWinners = Convert.ToInt32(_dict["twoStarWinners"]);
			}
			if (_dict.ContainsKey("threeStarWinners"))
			{
				psMinigameMetaData.threeStarWinners = Convert.ToInt32(_dict["threeStarWinners"]);
			}
			if (_dict.ContainsKey("participantCount"))
			{
				psMinigameMetaData.totalPlayers = Convert.ToInt32(_dict["participantCount"]);
			}
			if (_dict.ContainsKey("complexity"))
			{
				Convert.ToInt32(_dict["complexity"]);
			}
			if (_dict.ContainsKey("levelRequirement"))
			{
				double num2 = Convert.ToDouble(_dict["levelRequirement"]);
				psMinigameMetaData.levelRequirement = (int)num2;
			}
			if (_dict.ContainsKey("playerUnit"))
			{
				psMinigameMetaData.playerUnit = (string)_dict["playerUnit"];
			}
			if (_dict.ContainsKey("rating"))
			{
				string text = (string)_dict["rating"];
				psMinigameMetaData.rating = ClientTools.ParseRating(text);
			}
			if (_dict.ContainsKey("gameQuality"))
			{
				psMinigameMetaData.quality = (float)Convert.ToDouble(_dict["gameQuality"]);
			}
			else
			{
				Debug.LogError("NO GAME QUALITY");
			}
			if (_dict.ContainsKey("itemsUsed"))
			{
				object[] array = (_dict["itemsUsed"] as List<object>).ToArray();
				psMinigameMetaData.itemsUsed = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					psMinigameMetaData.itemsUsed[i] = (string)array[i];
				}
			}
			if (_dict.ContainsKey("itemsCount"))
			{
				psMinigameMetaData.itemsCount = ClientTools.ParseDictionaryObjectToObscuredInt(_dict["itemsCount"] as Dictionary<string, object>);
			}
			else
			{
				psMinigameMetaData.itemsCount = new Dictionary<string, ObscuredInt>();
			}
			if (_dict.ContainsKey("difficulty"))
			{
				string text2 = (string)_dict["difficulty"];
				psMinigameMetaData.difficulty = ClientTools.ParseDifficulty(text2);
			}
			else
			{
				psMinigameMetaData.difficulty = PsGameDifficulty.New;
			}
			if (_dict.ContainsKey("gameMode"))
			{
				string text3 = (string)_dict["gameMode"];
				if (text3 != null)
				{
					if (!(text3 == "Race"))
					{
						if (text3 == "StarCollect")
						{
							psMinigameMetaData.gameMode = PsGameMode.StarCollect;
						}
					}
					else
					{
						psMinigameMetaData.gameMode = PsGameMode.Race;
					}
				}
			}
			else
			{
				psMinigameMetaData.gameMode = PsGameMode.Race;
			}
			if (psMinigameMetaData.creatorId == PlayerPrefsX.GetUserId())
			{
				if (_dict.ContainsKey("rewardCoins"))
				{
					psMinigameMetaData.rewardCoins = Convert.ToInt32(_dict["rewardCoins"]);
				}
				if (_dict.ContainsKey("totalCoinsEarned"))
				{
					psMinigameMetaData.totalCoinsEarned = Convert.ToInt32(_dict["totalCoinsEarned"]);
				}
			}
			if (_dict.ContainsKey("t"))
			{
				psMinigameMetaData.timeScore = Convert.ToInt32(_dict["t"]);
			}
			if (_dict.ContainsKey("s"))
			{
				psMinigameMetaData.score = Convert.ToInt32(_dict["s"]);
			}
			if (_dict.ContainsKey("bestTime"))
			{
				psMinigameMetaData.bestTime = Convert.ToInt32(_dict["bestTime"]);
			}
			if (_dict.ContainsKey("creatorUpgrades") && _dict["creatorUpgrades"] != null)
			{
				psMinigameMetaData.creatorUpgrades = new Hashtable(_dict["creatorUpgrades"] as Dictionary<string, object>);
			}
			if (_dict.ContainsKey("overrideCC"))
			{
				psMinigameMetaData.overrideCC = Convert.ToSingle(_dict["overrideCC"]);
			}
			if (_dict.ContainsKey("state"))
			{
				string text4 = (string)_dict["state"];
				if (text4.ToLower() == "hidden")
				{
					psMinigameMetaData.hidden = true;
				}
				if ((string)_dict["state"] == "public")
				{
					psMinigameMetaData.m_state = PsMinigameServerState.published;
				}
				else
				{
					try
					{
						psMinigameMetaData.m_state = (PsMinigameServerState)Enum.Parse(typeof(PsMinigameServerState), (string)_dict["state"]);
					}
					catch
					{
						Debug.LogError("Wrong kind of state: " + _dict["state"].ToString());
					}
				}
			}
			SeasonEndData seasonEndData = ClientTools.ParseSeasonEndData(_dict);
			if (seasonEndData != null)
			{
				PsMetagameManager.SetSeasonEndData(seasonEndData);
			}
			return psMinigameMetaData;
		}
		return null;
	}

	// Token: 0x06001CB3 RID: 7347 RVA: 0x0014414C File Offset: 0x0014254C
	public static PsGameDifficulty ParseDifficulty(string _difficulty)
	{
		if (_difficulty != null)
		{
			if (_difficulty == "Easy")
			{
				return PsGameDifficulty.Easy;
			}
			if (_difficulty == "Tricky")
			{
				return PsGameDifficulty.Tricky;
			}
			if (_difficulty == "Insane")
			{
				return PsGameDifficulty.Insane;
			}
			if (_difficulty == "Impossible")
			{
				return PsGameDifficulty.Impossible;
			}
		}
		return PsGameDifficulty.New;
	}

	// Token: 0x06001CB4 RID: 7348 RVA: 0x001441B0 File Offset: 0x001425B0
	public static PsRating ParseRating(string _rating)
	{
		if (_rating != null)
		{
			if (ClientTools.<>f__switch$map4 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
				dictionary.Add("Elated", 0);
				dictionary.Add("Rejecting", 1);
				dictionary.Add("Positive", 2);
				dictionary.Add("Neutral", 3);
				dictionary.Add("Negative", 4);
				dictionary.Add("Unrated", 5);
				dictionary.Add("SuperLike", 6);
				ClientTools.<>f__switch$map4 = dictionary;
			}
			int num;
			if (ClientTools.<>f__switch$map4.TryGetValue(_rating, ref num))
			{
				switch (num)
				{
				case 0:
					return PsRating.Elated;
				case 1:
					return PsRating.Rejecting;
				case 2:
					return PsRating.Positive;
				case 3:
					return PsRating.Neutral;
				case 4:
					return PsRating.Negative;
				case 5:
					return PsRating.Unrated;
				case 6:
					return PsRating.SuperLike;
				}
			}
		}
		return PsRating.Unrated;
	}

	// Token: 0x06001CB5 RID: 7349 RVA: 0x0014427C File Offset: 0x0014267C
	public static PsTournamentMetaData ParseTournamentMetaData(Dictionary<string, object> _dict)
	{
		foreach (KeyValuePair<string, object> keyValuePair in _dict)
		{
			Debug.Log(keyValuePair.Key + ": " + keyValuePair.Value, null);
		}
		PsTournamentMetaData psTournamentMetaData = new PsTournamentMetaData();
		if (_dict.ContainsKey("id"))
		{
			psTournamentMetaData.tournamentId = (string)_dict["id"];
		}
		if (_dict.ContainsKey("timeLeft"))
		{
			psTournamentMetaData.timeLeft = Convert.ToInt32(_dict["timeLeft"]);
		}
		if (_dict.ContainsKey("duration"))
		{
			psTournamentMetaData.duration = Convert.ToInt32(_dict["duration"]);
		}
		if (_dict.ContainsKey("rewardCoins"))
		{
			psTournamentMetaData.playerRewardCoins = Convert.ToInt32(_dict["rewardCoins"]);
		}
		if (_dict.ContainsKey("rewardDiamonds"))
		{
			psTournamentMetaData.playerRewardDiamonds = Convert.ToInt32(_dict["rewardDiamonds"]);
		}
		if (_dict.ContainsKey("participationCoinsCost"))
		{
			psTournamentMetaData.participationCoinsCost = Convert.ToInt32(_dict["participationCoinsCost"]);
		}
		if (_dict.ContainsKey("participationDiamondsCost"))
		{
			psTournamentMetaData.participationDiamondsCost = Convert.ToInt32(_dict["participationDiamondsCost"]);
		}
		if (_dict.ContainsKey("participated"))
		{
			psTournamentMetaData.participated = (bool)_dict["participated"];
		}
		if (_dict.ContainsKey("participantCount"))
		{
			psTournamentMetaData.participantCount = Convert.ToInt32(_dict["participantCount"]);
		}
		if (_dict.ContainsKey("state"))
		{
			psTournamentMetaData.claimed = (string)_dict["state"] == "claimed";
		}
		if (_dict.ContainsKey("daily"))
		{
			psTournamentMetaData.daily = (bool)_dict["daily"];
		}
		if (_dict.ContainsKey("name"))
		{
			psTournamentMetaData.name = (string)_dict["name"];
		}
		if (_dict.ContainsKey("time"))
		{
			psTournamentMetaData.time = Convert.ToInt32(_dict["time"]);
		}
		if (_dict.ContainsKey("gameId"))
		{
			psTournamentMetaData.gameId = (string)_dict["gameId"];
		}
		if (_dict.ContainsKey("gameName"))
		{
			psTournamentMetaData.gameName = (string)_dict["gameName"];
		}
		if (_dict.ContainsKey("difficulty"))
		{
			psTournamentMetaData.difficulty = ClientTools.ParseDifficulty((string)_dict["difficulty"]);
		}
		if (_dict.ContainsKey("creatorName"))
		{
			psTournamentMetaData.creatorName = (string)_dict["creatorName"];
		}
		if (_dict.ContainsKey("creatorId"))
		{
			psTournamentMetaData.creatorId = (string)_dict["creatorId"];
		}
		if (_dict.ContainsKey("playerUnit"))
		{
			psTournamentMetaData.playerUnit = (string)_dict["playerUnit"];
		}
		if (_dict.ContainsKey("rating"))
		{
			psTournamentMetaData.rating = ClientTools.ParseRating((string)_dict["rating"]);
		}
		if (_dict.ContainsKey("facebookId"))
		{
			psTournamentMetaData.creatorFacebookId = (string)_dict["facebookId"];
		}
		if (_dict.ContainsKey("gameCenterId"))
		{
			psTournamentMetaData.creatorGameCenterId = (string)_dict["gameCenterId"];
		}
		if (_dict.ContainsKey("gameDescription"))
		{
			psTournamentMetaData.gameDescription = (string)_dict["gameDescription"];
		}
		if (_dict.ContainsKey("percentileField"))
		{
			psTournamentMetaData.percentiles = ClientTools.ParsePercentileList(_dict["percentileField"] as List<object>);
		}
		if (_dict.ContainsKey("topRewards"))
		{
			List<object> list = _dict["topRewards"] as List<object>;
			psTournamentMetaData.topThreeRewards = new int[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				psTournamentMetaData.topThreeRewards[i] = Convert.ToInt32(list[i]);
			}
		}
		if (_dict.ContainsKey("topScores"))
		{
			List<object> list2 = _dict["topScores"] as List<object>;
			psTournamentMetaData.topScores = new HighscoreDataEntry[list2.Count];
			for (int j = 0; j < list2.Count; j++)
			{
				Dictionary<string, object> dictionary = list2[j] as Dictionary<string, object>;
				psTournamentMetaData.topScores[j] = HighScores.ParseDataEntry(dictionary);
			}
		}
		if (_dict.ContainsKey("totalPot"))
		{
			psTournamentMetaData.totalPot = Convert.ToInt32(_dict["totalPot"]);
		}
		psTournamentMetaData.timeLeftSetTime = Main.m_EPOCHSeconds;
		return psTournamentMetaData;
	}

	// Token: 0x06001CB6 RID: 7350 RVA: 0x001447A0 File Offset: 0x00142BA0
	public static PercentileField[] ParsePercentileList(List<object> percentileList)
	{
		PercentileField[] array = new PercentileField[percentileList.Count];
		for (int i = 0; i < percentileList.Count; i++)
		{
			Dictionary<string, object> dictionary = percentileList[i] as Dictionary<string, object>;
			array[i] = ClientTools.ParsePercentileField(dictionary);
			array[i].index = i + 1;
		}
		return array;
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x001447F4 File Offset: 0x00142BF4
	public static PercentileField ParsePercentileField(Dictionary<string, object> _dict)
	{
		PercentileField percentileField = new PercentileField();
		if (_dict.ContainsKey("time"))
		{
			percentileField.percentile = Convert.ToInt32(_dict["time"]);
		}
		if (_dict.ContainsKey("reward"))
		{
			percentileField.reward = Convert.ToInt32(_dict["reward"]);
		}
		return percentileField;
	}

	// Token: 0x06001CB8 RID: 7352 RVA: 0x00144854 File Offset: 0x00142C54
	public static PsChallengeMetaData ParseChallengeMetaData(Dictionary<string, object> _dict)
	{
		PsChallengeMetaData psChallengeMetaData = new PsChallengeMetaData();
		foreach (KeyValuePair<string, object> keyValuePair in _dict)
		{
			Debug.Log(keyValuePair.Key, null);
		}
		if (_dict != null)
		{
			if (_dict.ContainsKey("meta"))
			{
				psChallengeMetaData.minigameMetaData = ClientTools.ParseMinigameMetaData((Dictionary<string, object>)_dict["meta"]);
			}
			if (_dict.ContainsKey("timeLeft"))
			{
				psChallengeMetaData.timeLeft = Convert.ToInt32(_dict["timeLeft"]);
			}
			if (_dict.ContainsKey("duration"))
			{
				psChallengeMetaData.duration = Convert.ToInt32(_dict["duration"]);
			}
			if (_dict.ContainsKey("timedEventId"))
			{
				psChallengeMetaData.timedEventId = (string)_dict["timedEventId"];
			}
			psChallengeMetaData.timeLeftSetTime = Main.m_EPOCHSeconds;
		}
		return psChallengeMetaData;
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x00144968 File Offset: 0x00142D68
	public static VersusMetaData ParseVersusMetaData(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParseVersusMetaData(dictionary);
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x0014498C File Offset: 0x00142D8C
	public static VersusMetaData ParseVersusMetaData(Dictionary<string, object> _dict)
	{
		VersusMetaData versusMetaData = new VersusMetaData();
		versusMetaData.timedEventId = (string)_dict["timedEventId"];
		versusMetaData.gameId = (string)_dict["gameId"];
		if (_dict.ContainsKey("meta"))
		{
			versusMetaData.gameMetaData = ClientTools.ParseMinigameMetaData((Dictionary<string, object>)_dict["meta"]);
		}
		if (_dict.ContainsKey("participants"))
		{
			versusMetaData.participants = ClientTools.ParseVersusParticipants((List<object>)_dict["participants"]);
		}
		if (_dict.ContainsKey("timeLeft"))
		{
			versusMetaData.timeLeft = Convert.ToInt32(_dict["timeLeft"]);
		}
		versusMetaData.timeLeftSetTime = Main.m_EPOCHSeconds;
		if (_dict.ContainsKey("currentPlayer"))
		{
			versusMetaData.currentPlayer = (string)_dict["currentPlayer"];
		}
		if (_dict.ContainsKey("winner"))
		{
			versusMetaData.winner = (string)_dict["winner"];
		}
		if (_dict.ContainsKey("round"))
		{
			versusMetaData.round = Convert.ToInt32(_dict["round"]);
		}
		if (_dict.ContainsKey("matchBall"))
		{
			versusMetaData.matchBall = (bool)_dict["matchBall"];
		}
		if (_dict.ContainsKey("challengedPlayer"))
		{
			versusMetaData.challengedPlayer = (string)_dict["challengedPlayer"];
		}
		if (_dict.ContainsKey("status"))
		{
			versusMetaData.status = (string)_dict["status"];
		}
		return versusMetaData;
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x00144B38 File Offset: 0x00142F38
	public static VersusParticipant[] ParseVersusParticipants(List<object> _participants)
	{
		VersusParticipant[] array = new VersusParticipant[_participants.Count];
		for (int i = 0; i < _participants.Count; i++)
		{
			Dictionary<string, object> dictionary = _participants[i] as Dictionary<string, object>;
			array[i] = ClientTools.ParseVersusParticipant(dictionary);
		}
		return array;
	}

	// Token: 0x06001CBC RID: 7356 RVA: 0x00144B80 File Offset: 0x00142F80
	public static VersusParticipant ParseVersusParticipant(Dictionary<string, object> _dict)
	{
		return new VersusParticipant
		{
			score = HighScores.ParseDataEntry(_dict["score"] as Dictionary<string, object>),
			rank = Convert.ToInt32(_dict["rank"]),
			stars = Convert.ToInt32(_dict["stars"]),
			tries = Convert.ToInt32(_dict["tries"])
		};
	}

	// Token: 0x06001CBD RID: 7357 RVA: 0x00144BF4 File Offset: 0x00142FF4
	public static PsMinigameMetaData[] ParseTemplateList(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("data"))
		{
			List<object> list = dictionary["data"] as List<object>;
			PsMinigameMetaData[] array = new PsMinigameMetaData[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
				array[i] = ClientTools.ParseTemplate(dictionary2);
			}
			return array;
		}
		return null;
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x00144C74 File Offset: 0x00143074
	public static PsMinigameMetaData[] ParseMinigameList(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("data"))
		{
			List<object> list = dictionary["data"] as List<object>;
			PsMinigameMetaData[] array = new PsMinigameMetaData[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
				array[i] = ClientTools.ParseMinigameMetaData(dictionary2);
			}
			return array;
		}
		return null;
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x00144CF4 File Offset: 0x001430F4
	public static OwnLevelsInfo ParseOwnMinigames(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		OwnLevelsInfo ownLevelsInfo = default(OwnLevelsInfo);
		if (dictionary.ContainsKey("data"))
		{
			List<object> list = dictionary["data"] as List<object>;
			PsMinigameMetaData[] array = new PsMinigameMetaData[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
				array[i] = ClientTools.ParseMinigameMetaData(dictionary2);
			}
			ownLevelsInfo.levels = array;
		}
		if (dictionary.ContainsKey("publishedMinigameCount"))
		{
			ownLevelsInfo.publishedMinigameCount = Convert.ToInt32(dictionary["publishedMinigameCount"]);
		}
		if (dictionary.ContainsKey("followerCount"))
		{
			ownLevelsInfo.followerCount = Convert.ToInt32(dictionary["followerCount"]);
		}
		if (dictionary.ContainsKey("totalCoinsEarned"))
		{
			ownLevelsInfo.totalCoinsEarned = Convert.ToInt32(dictionary["totalCoinsEarned"]);
		}
		if (dictionary.ContainsKey("totalLikes"))
		{
			ownLevelsInfo.totalLikes = Convert.ToInt32(dictionary["totalLikes"]);
		}
		if (dictionary.ContainsKey("totalSuperLikes"))
		{
			ownLevelsInfo.totalSuperLikes = Convert.ToInt32(dictionary["totalSuperLikes"]);
		}
		if (dictionary.ContainsKey("likesSeen"))
		{
			ownLevelsInfo.likesSeen = Convert.ToInt32(dictionary["likesSeen"]);
		}
		return ownLevelsInfo;
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x00144E70 File Offset: 0x00143270
	public static List<LeaderboardEntry> ParseGameLeaderboard(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParseLeaderboardEntrylist(dictionary["data"] as List<object>);
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x00144EA4 File Offset: 0x001432A4
	public static Leaderboard ParseLeaderboards(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParseLeaderboards(dictionary);
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x00144EC8 File Offset: 0x001432C8
	public static Leaderboard ParseLeaderboards(Dictionary<string, object> _dict)
	{
		Leaderboard leaderboard = new Leaderboard();
		if (_dict.ContainsKey("global"))
		{
			List<object> list = _dict["global"] as List<object>;
			leaderboard.global = ClientTools.ParseLeaderboardEntrylist(list);
		}
		if (_dict.ContainsKey("local"))
		{
			List<object> list2 = _dict["local"] as List<object>;
			leaderboard.local = ClientTools.ParseLeaderboardEntrylist(list2);
		}
		if (_dict.ContainsKey("friend"))
		{
			List<object> list3 = _dict["friend"] as List<object>;
			leaderboard.friends = ClientTools.ParseLeaderboardEntrylist(list3);
		}
		return leaderboard;
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x00144F64 File Offset: 0x00143364
	public static CreatorLeaderboard ParseCreatorLeaderboard(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		List<object> list = dictionary["data"] as List<object>;
		Dictionary<int, List<object>> dictionary2 = new Dictionary<int, List<object>>();
		for (int i = 0; i < list.Count; i++)
		{
			Dictionary<string, object> dictionary3 = list[i] as Dictionary<string, object>;
			int num = Convert.ToInt32(dictionary3["offset"]);
			dictionary2[num] = dictionary3["leaderboard"] as List<object>;
		}
		Dictionary<int, List<PlayerData>> dictionary4 = new Dictionary<int, List<PlayerData>>();
		foreach (KeyValuePair<int, List<object>> keyValuePair in dictionary2)
		{
			int key = keyValuePair.Key;
			dictionary4[key] = new List<PlayerData>();
			for (int j = 0; j < keyValuePair.Value.Count; j++)
			{
				Dictionary<string, object> dictionary5 = keyValuePair.Value[j] as Dictionary<string, object>;
				PlayerData playerData = ClientTools.ParsePlayerData(dictionary5);
				dictionary4[key].Add(playerData);
			}
		}
		return ClientTools.ParseCreatorLeaderboardEntryList(dictionary4);
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x001450A4 File Offset: 0x001434A4
	public static CreatorLeaderboard ParseCreatorLeaderboardEntryList(Dictionary<int, List<PlayerData>> _entries)
	{
		CreatorLeaderboard creatorLeaderboard = new CreatorLeaderboard();
		creatorLeaderboard.global = new List<CreatorLeaderboardEntry>();
		creatorLeaderboard.local = new List<CreatorLeaderboardEntry>();
		creatorLeaderboard.friends = new List<CreatorLeaderboardEntry>();
		List<PlayerData> list = _entries[0];
		List<PlayerData> list2 = _entries[1];
		Dictionary<string, Tuple<int, PlayerData>> dictionary = new Dictionary<string, Tuple<int, PlayerData>>();
		for (int i = 0; i < list2.Count; i++)
		{
			PlayerData playerData = list2[i];
			Tuple<int, PlayerData> tuple = new Tuple<int, PlayerData>(i, playerData);
			dictionary[playerData.playerId] = tuple;
		}
		for (int j = 0; j < list.Count; j++)
		{
			PlayerData playerData2 = list[j];
			CreatorLeaderboardEntry creatorLeaderboardEntry = default(CreatorLeaderboardEntry);
			creatorLeaderboardEntry.user = playerData2;
			string playerId = creatorLeaderboardEntry.user.playerId;
			Tuple<int, PlayerData> tuple2;
			if (dictionary.TryGetValue(playerId, ref tuple2))
			{
				int num = tuple2.Item1 + 1;
				int num2 = j + 1;
				creatorLeaderboardEntry.user.creatorRankingDelta = num - num2;
			}
			else
			{
				creatorLeaderboardEntry.user.creatorRankingDelta = 1;
			}
			int selectedSubTab = PsUITabbedCreate.m_selectedSubTab;
			if (selectedSubTab != 1)
			{
				if (selectedSubTab != 2)
				{
					if (selectedSubTab == 3)
					{
						creatorLeaderboard.friends.Add(creatorLeaderboardEntry);
					}
				}
				else
				{
					creatorLeaderboard.local.Add(creatorLeaderboardEntry);
				}
			}
			else
			{
				creatorLeaderboard.global.Add(creatorLeaderboardEntry);
			}
		}
		creatorLeaderboard.OffsetEntries = _entries;
		return creatorLeaderboard;
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x00145218 File Offset: 0x00143618
	public static List<LeaderboardEntry> ParseLeaderboardEntrylist(List<object> entryList)
	{
		List<LeaderboardEntry> list = new List<LeaderboardEntry>();
		for (int i = 0; i < entryList.Count; i++)
		{
			LeaderboardEntry leaderboardEntry = default(LeaderboardEntry);
			Dictionary<string, object> dictionary = entryList[i] as Dictionary<string, object>;
			leaderboardEntry.user.playerId = string.Empty;
			leaderboardEntry.user = ClientTools.ParsePlayerData(dictionary);
			if (dictionary.ContainsKey("trophies"))
			{
				leaderboardEntry.trophies = Convert.ToInt32(dictionary["trophies"]);
			}
			if (leaderboardEntry.user.carTrophies == 0 && leaderboardEntry.user.mcTrophies == 0)
			{
				leaderboardEntry.user.carTrophies = leaderboardEntry.trophies;
			}
			if (dictionary.ContainsKey("time"))
			{
				leaderboardEntry.time = Convert.ToInt32(dictionary["time"]);
			}
			if (dictionary.ContainsKey("_id") && (dictionary["_id"] as Dictionary<string, object>).ContainsKey("$oid"))
			{
				leaderboardEntry.user.playerId = (string)(dictionary["_id"] as Dictionary<string, object>)["$oid"];
			}
			list.Add(leaderboardEntry);
		}
		return list;
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x0014535C File Offset: 0x0014375C
	public static PsTimedEvents ParseTimedEvents(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParseTimedEvents(dictionary);
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x00145380 File Offset: 0x00143780
	public static PsTimedEvents ParseTimedEvents(Dictionary<string, object> _dict)
	{
		PsTimedEvents psTimedEvents = new PsTimedEvents();
		if (_dict.ContainsKey("dailyChallenges"))
		{
			List<object> list = _dict["dailyChallenges"] as List<object>;
			psTimedEvents.diamondChallenges = ClientTools.ParseChallengeList(list);
		}
		if (_dict.ContainsKey("versusChallenges"))
		{
			List<object> list2 = _dict["versusChallenges"] as List<object>;
			psTimedEvents.versusChallenges = ClientTools.ParseVersusList(list2);
		}
		if (_dict.ContainsKey("freshAndFree"))
		{
			Dictionary<string, object> dictionary = _dict["freshAndFree"] as Dictionary<string, object>;
			psTimedEvents.freshAndFree.Clear();
			psTimedEvents.freshAndFree.Add(ClientTools.ParseMinigameMetaData(dictionary));
		}
		if (_dict.ContainsKey("friendlyChallenges"))
		{
			List<object> list3 = _dict["friendlyChallenges"] as List<object>;
			psTimedEvents.friendlyChallenges = ClientTools.ParseVersusList(list3);
		}
		psTimedEvents.tournaments = ClientTools.ParseTournaments(_dict);
		return psTimedEvents;
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x00145468 File Offset: 0x00143868
	public static PsTournaments ParseTournaments(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParseTournaments(dictionary);
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x0014548C File Offset: 0x0014388C
	public static PsTournaments ParseTournaments(Dictionary<string, object> _dictionary)
	{
		PsTournaments psTournaments = new PsTournaments();
		if (_dictionary.ContainsKey("daily"))
		{
			List<object> list = _dictionary["daily"] as List<object>;
			psTournaments.dailyTournaments = ClientTools.ParseTournamentList(list);
		}
		if (_dictionary.ContainsKey("active"))
		{
			List<object> list2 = _dictionary["active"] as List<object>;
			psTournaments.activeTournaments = ClientTools.ParseTournamentList(list2);
			for (int i = 0; i < psTournaments.activeTournaments.Count; i++)
			{
				if (psTournaments.activeTournaments[i].timeLeft <= 0)
				{
					psTournaments.activeTournaments[i].waitingForRewards = true;
				}
			}
		}
		if (_dictionary.ContainsKey("claimable"))
		{
			List<object> list3 = _dictionary["claimable"] as List<object>;
			psTournaments.claimableTournaments = ClientTools.ParseTournamentList(list3);
		}
		if (_dictionary.ContainsKey("claimed"))
		{
			List<object> list4 = _dictionary["claimed"] as List<object>;
			psTournaments.claimedTournaments = ClientTools.ParseTournamentList(list4);
		}
		return psTournaments;
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x001455A0 File Offset: 0x001439A0
	public static PsTournamentMetaData ParseTournament(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParseTournamentMetaData(dictionary);
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x001455C4 File Offset: 0x001439C4
	public static List<PsTournamentMetaData> ParseTournamentList(HttpC _c)
	{
		return ClientTools.ParseTournamentList(ClientTools.ParseServerResponse(_c.www.text)["data"] as List<object>);
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x001455EC File Offset: 0x001439EC
	public static List<PsTournamentMetaData> ParseTournamentList(List<object> _dict)
	{
		List<PsTournamentMetaData> list = new List<PsTournamentMetaData>();
		for (int i = 0; i < _dict.Count; i++)
		{
			Dictionary<string, object> dictionary = _dict[i] as Dictionary<string, object>;
			list.Add(ClientTools.ParseTournamentMetaData(dictionary));
		}
		return list;
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x00145630 File Offset: 0x00143A30
	public static List<PsChallengeMetaData> ParseChallengeList(HttpC _c)
	{
		return ClientTools.ParseChallengeList(ClientTools.ParseServerResponse(_c.www.text)["data"] as List<object>);
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x00145658 File Offset: 0x00143A58
	public static List<PsChallengeMetaData> ParseChallengeList(List<object> _dict)
	{
		List<PsChallengeMetaData> list = new List<PsChallengeMetaData>();
		for (int i = 0; i < _dict.Count; i++)
		{
			Dictionary<string, object> dictionary = _dict[i] as Dictionary<string, object>;
			list.Add(ClientTools.ParseChallengeMetaData(dictionary));
		}
		return list;
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x0014569C File Offset: 0x00143A9C
	public static List<VersusMetaData> ParseVersusList(HttpC _c)
	{
		return ClientTools.ParseVersusList(ClientTools.ParseServerResponse(_c.www.text)["data"] as List<object>);
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x001456C4 File Offset: 0x00143AC4
	public static List<VersusMetaData> ParseVersusList(List<object> _dict)
	{
		List<VersusMetaData> list = new List<VersusMetaData>();
		for (int i = 0; i < _dict.Count; i++)
		{
			Dictionary<string, object> dictionary = _dict[i] as Dictionary<string, object>;
			list.Add(ClientTools.ParseVersusMetaData(dictionary));
		}
		return list;
	}

	// Token: 0x06001CD1 RID: 7377 RVA: 0x00145708 File Offset: 0x00143B08
	public static string[] ParseFBFriendIds(string _FBFriendString)
	{
		Debug.Log(_FBFriendString, null);
		Dictionary<string, object> dictionary = FacebookManager.ParseFBResponse(_FBFriendString);
		List<string> list = new List<string>();
		if (dictionary.ContainsKey("data"))
		{
			List<object> list2 = dictionary["data"] as List<object>;
			for (int i = 0; i < list2.Count; i++)
			{
				Dictionary<string, object> dictionary2 = list2[i] as Dictionary<string, object>;
				if (dictionary2.ContainsKey("installed"))
				{
					list.Add((string)dictionary2["id"]);
				}
			}
		}
		else
		{
			Debug.LogError("Wrong data from server when parsing FB friends!");
		}
		return list.ToArray();
	}

	// Token: 0x06001CD2 RID: 7378 RVA: 0x001457AC File Offset: 0x00143BAC
	public static Friends ParseFriends(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		Friends friends = new Friends();
		friends.followees = new List<PlayerData>(ClientTools.ParseFriendList(dictionary["followees"] as List<object>));
		friends.followers = new List<PlayerData>();
		friends.friendList = new List<PlayerData>(ClientTools.ParseFriendList(dictionary["friends"] as List<object>));
		friends.friends = new Dictionary<string, PlayerData>();
		foreach (PlayerData playerData in friends.friendList)
		{
			friends.friends[playerData.playerId] = playerData;
		}
		Debug.Log(string.Concat(new object[]
		{
			"Followees: ",
			friends.followees.Count,
			" Followers: ",
			friends.followers.Count,
			" Friends: ",
			(friends.friends == null) ? 0 : friends.friends.Count
		}), null);
		return friends;
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x001458F8 File Offset: 0x00143CF8
	public static string[] ParsePlayerIds(List<object> _idList)
	{
		string[] array = new string[_idList.Count];
		for (int i = 0; i < _idList.Count; i++)
		{
			array[i] = ClientTools.ParsePlayerIdData(_idList[i] as Dictionary<string, object>);
		}
		return array;
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x00145940 File Offset: 0x00143D40
	public static PlayerData[] ParsePlayerList(List<object> _playerList)
	{
		PlayerData[] array = new PlayerData[_playerList.Count];
		for (int i = 0; i < _playerList.Count; i++)
		{
			array[i] = ClientTools.ParsePlayerData(_playerList[i] as Dictionary<string, object>);
		}
		return array;
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x00145990 File Offset: 0x00143D90
	public static PlayerData[] ParseFriendList(List<object> _friendJson)
	{
		PlayerData[] array = new PlayerData[_friendJson.Count];
		for (int i = 0; i < _friendJson.Count; i++)
		{
			array[i] = ClientTools.ParseFollowPlayerData(_friendJson[i] as Dictionary<string, object>);
		}
		return array;
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x001459E0 File Offset: 0x00143DE0
	public static PlayerData[] ParseFollowPlayers(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParseFollowPlayers(dictionary);
	}

	// Token: 0x06001CD7 RID: 7383 RVA: 0x00145A04 File Offset: 0x00143E04
	public static PlayerData[] ParseFollowPlayers(Dictionary<string, object> _dictionary)
	{
		List<object> list = _dictionary["data"] as List<object>;
		return ClientTools.ParseFriendList(list);
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x00145A28 File Offset: 0x00143E28
	public static string ParsePlayerIdData(Dictionary<string, object> _dictionary)
	{
		return (string)_dictionary["id"];
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x00145A3C File Offset: 0x00143E3C
	public static PlayerData ParsePlayerData(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParsePlayerData(dictionary);
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x00145A60 File Offset: 0x00143E60
	public static PlayerData ParsePlayerData(Dictionary<string, object> _dictionary)
	{
		PlayerData playerData = default(PlayerData);
		if (_dictionary.ContainsKey("id"))
		{
			playerData.playerId = (string)_dictionary["id"];
		}
		else if (_dictionary.ContainsKey("playerId"))
		{
			playerData.playerId = (string)_dictionary["playerId"];
		}
		if (_dictionary.ContainsKey("name"))
		{
			playerData.name = (string)_dictionary["name"];
		}
		if (_dictionary.ContainsKey("tag"))
		{
			playerData.tag = (string)_dictionary["tag"];
		}
		if (_dictionary.ContainsKey("cheater"))
		{
			playerData.cheater = (bool)_dictionary["cheater"];
		}
		else
		{
			playerData.cheater = false;
		}
		if (_dictionary.ContainsKey("developer"))
		{
			playerData.developer = (bool)_dictionary["developer"];
		}
		else
		{
			playerData.developer = false;
		}
		if (_dictionary.ContainsKey("coins"))
		{
			playerData.coins = Convert.ToInt32(_dictionary["coins"]);
		}
		if (_dictionary.ContainsKey("copper"))
		{
			playerData.copper = Convert.ToInt32(_dictionary["copper"]);
		}
		if (_dictionary.ContainsKey("diamonds"))
		{
			playerData.diamonds = Convert.ToInt32(_dictionary["diamonds"]);
		}
		if (_dictionary.ContainsKey("shards"))
		{
			playerData.shards = Convert.ToInt32(_dictionary["shards"]);
		}
		if (_dictionary.ContainsKey("stars"))
		{
			playerData.stars = Convert.ToInt32(_dictionary["stars"]);
		}
		if (_dictionary.ContainsKey("level"))
		{
			playerData.level = Convert.ToInt32(_dictionary["level"]);
		}
		if (_dictionary.ContainsKey("mcBoosters"))
		{
			playerData.mcBoosters = Convert.ToInt32(_dictionary["mcBoosters"]);
		}
		if (_dictionary.ContainsKey("maxMcBoosters"))
		{
			playerData.maxMcBoosters = Convert.ToInt32(_dictionary["maxMcBoosters"]);
		}
		if (_dictionary.ContainsKey("carBoosters"))
		{
			playerData.carBoosters = Convert.ToInt32(_dictionary["carBoosters"]);
		}
		if (_dictionary.ContainsKey("maxCarBoosters"))
		{
			playerData.maxCarBoosters = Convert.ToInt32(_dictionary["maxCarBoosters"]);
		}
		if (_dictionary.ContainsKey("tournamentBoosters"))
		{
			playerData.tournamentBoosters = Convert.ToInt32(_dictionary["tournamentBoosters"]);
		}
		if (_dictionary.ContainsKey("itemLevel"))
		{
			playerData.itemLevel = Convert.ToInt32(_dictionary["itemLevel"]);
		}
		if (_dictionary.ContainsKey("cups"))
		{
			playerData.cups = Convert.ToInt32(_dictionary["cups"]);
		}
		if (_dictionary.ContainsKey("mcRank"))
		{
			playerData.mcRank = Convert.ToInt32(_dictionary["mcRank"]);
		}
		if (_dictionary.ContainsKey("carRank"))
		{
			playerData.carRank = Convert.ToInt32(_dictionary["carRank"]);
		}
		if (_dictionary.ContainsKey("mcTrophies"))
		{
			playerData.mcTrophies = Convert.ToInt32(_dictionary["mcTrophies"]);
		}
		if (_dictionary.ContainsKey("carTrophies"))
		{
			playerData.carTrophies = Convert.ToInt32(_dictionary["carTrophies"]);
		}
		if (_dictionary.ContainsKey("bigBangPoints"))
		{
			playerData.bigBangPoints = Convert.ToInt32(_dictionary["bigBangPoints"]);
		}
		if (_dictionary.ContainsKey("completedAdventures"))
		{
			playerData.adventureLevelsCompleted = Convert.ToInt32(_dictionary["completedAdventures"]);
		}
		if (_dictionary.ContainsKey("racesWon"))
		{
			playerData.racesWon = Convert.ToInt32(_dictionary["racesWon"]);
		}
		if (_dictionary.ContainsKey("goodOrBadLevelsRated"))
		{
			playerData.newLevelsRated = Convert.ToInt32(_dictionary["goodOrBadLevelsRated"]);
		}
		if (_dictionary.ContainsKey("xp"))
		{
			playerData.xp = Convert.ToInt32(_dictionary["xp"]);
		}
		if (_dictionary.ContainsKey("completedSurvey"))
		{
			playerData.completedSurvey = Convert.ToBoolean(_dictionary["completedSurvey"]);
		}
		if (_dictionary.ContainsKey("gender"))
		{
			playerData.gender = Convert.ToString(_dictionary["gender"]);
		}
		if (_dictionary.ContainsKey("ageGroup"))
		{
			playerData.ageGroup = Convert.ToString(_dictionary["ageGroup"]);
		}
		if (_dictionary.ContainsKey("mcHandicap"))
		{
			playerData.mcHandicap = Convert.ToSingle(_dictionary["mcHandicap"]);
		}
		else
		{
			playerData.mcHandicap = BossBattles.startHandicap;
		}
		if (_dictionary.ContainsKey("carHandicap"))
		{
			playerData.carHandicap = Convert.ToSingle(_dictionary["carHandicap"]);
		}
		else
		{
			playerData.carHandicap = BossBattles.startHandicap;
		}
		if (_dictionary.ContainsKey("fbClaimed"))
		{
			playerData.fbClaimed = (bool)_dictionary["fbClaimed"];
		}
		else
		{
			playerData.fbClaimed = false;
		}
		if (_dictionary.ContainsKey("igClaimed"))
		{
			playerData.igClaimed = (bool)_dictionary["igClaimed"];
		}
		else
		{
			playerData.igClaimed = false;
		}
		if (_dictionary.ContainsKey("forumClaimed"))
		{
			playerData.forumClaimed = (bool)_dictionary["forumClaimed"];
		}
		else
		{
			playerData.forumClaimed = false;
		}
		if (_dictionary.ContainsKey("cardPurchases"))
		{
			playerData.cardPurchases = (string)_dictionary["cardPurchases"];
		}
		if (_dictionary.ContainsKey("gachaData"))
		{
			playerData.gachaData = (string)_dictionary["gachaData"];
		}
		if (_dictionary.ContainsKey("upgrades"))
		{
			playerData.upgrades = ClientTools.ParsePlayerUpgrades(_dictionary["upgrades"] as Dictionary<string, object>);
		}
		if (_dictionary.ContainsKey("acceptNotifications"))
		{
			playerData.acceptNotifications = (bool)_dictionary["acceptNotifications"];
		}
		if (_dictionary.ContainsKey("facebookId"))
		{
			playerData.facebookId = (string)_dictionary["facebookId"];
		}
		if (_dictionary.ContainsKey("gameCenterId"))
		{
			playerData.gameCenterId = (string)_dictionary["gameCenterId"];
		}
		if (_dictionary.ContainsKey("ninjaCreationTimestamp"))
		{
			playerData.ninjaCreationTimestamp = (string)_dictionary["ninjaCreationTimestamp"];
		}
		if (_dictionary.ContainsKey("countryCode"))
		{
			playerData.countryCode = (string)_dictionary["countryCode"];
		}
		if (_dictionary.ContainsKey("itemDbVersion"))
		{
			playerData.itemDbVersion = Convert.ToInt32(_dictionary["itemDbVersion"]);
		}
		if (_dictionary.ContainsKey("teamId"))
		{
			playerData.teamId = (string)_dictionary["teamId"];
		}
		if (_dictionary.ContainsKey("teamName"))
		{
			playerData.teamName = (string)_dictionary["teamName"];
		}
		if (_dictionary.ContainsKey("teamRole"))
		{
			playerData.teamRole = (TeamRole)Enum.Parse(typeof(TeamRole), (string)_dictionary["teamRole"]);
		}
		if (_dictionary.ContainsKey("hasJoinedTeam"))
		{
			playerData.hasJoinedTeam = Convert.ToBoolean(_dictionary["hasJoinedTeam"]);
		}
		if (_dictionary.ContainsKey("teamKickReason"))
		{
			playerData.teamKickReason = (string)_dictionary["teamKickReason"];
		}
		if (_dictionary.ContainsKey("lastSeasonEndCarTrophies"))
		{
			playerData.lastSeasonEndCarTrophies = Convert.ToInt32(_dictionary["lastSeasonEndCarTrophies"]);
		}
		if (_dictionary.ContainsKey("lastSeasonEndMcTrophies"))
		{
			playerData.lastSeasonEndMcTrophies = Convert.ToInt32(_dictionary["lastSeasonEndMcTrophies"]);
		}
		if (_dictionary.ContainsKey("reward"))
		{
			playerData.seasonReward = Convert.ToInt32(_dictionary["reward"]);
		}
		if (_dictionary.ContainsKey("racesThisSeason"))
		{
			playerData.racesThisSeason = Convert.ToInt32(_dictionary["racesThisSeason"]);
		}
		if (_dictionary.ContainsKey("youtuber"))
		{
			playerData.youtubeName = (string)_dictionary["youtuber"];
		}
		if (_dictionary.ContainsKey("youtuberId"))
		{
			playerData.youtubeId = (string)_dictionary["youtuberId"];
		}
		if (_dictionary.ContainsKey("youtubeSubscriberCount"))
		{
			playerData.youtubeSubscriberCount = Convert.ToInt32(_dictionary["youtubeSubscriberCount"]);
		}
		playerData.teamRoleName = ClientTools.GetRoleName(playerData.teamRole);
		if (_dictionary.ContainsKey("data"))
		{
			Dictionary<string, object> dictionary = _dictionary["data"] as Dictionary<string, object>;
			if (dictionary != null)
			{
				PsMetagamePlayerData.m_playerData = new Hashtable(dictionary);
			}
		}
		if (_dictionary.ContainsKey("clientConfig"))
		{
			playerData.clientConfig = new ClientConfig(_dictionary["clientConfig"] as Dictionary<string, object>);
		}
		if (_dictionary.ContainsKey("playerConfig"))
		{
			playerData.clientConfig = ClientTools.ParsePlayerConfig(_dictionary["playerConfig"] as Dictionary<string, object>, playerData.clientConfig);
		}
		if (_dictionary.ContainsKey("editorResources"))
		{
			playerData.editorResources = ClientTools.ParseDictionaryObjectToObscuredInt(_dictionary["editorResources"] as Dictionary<string, object>);
		}
		else
		{
			playerData.editorResources = new Dictionary<string, ObscuredInt>();
		}
		if (_dictionary.ContainsKey("mcBoosterRefreshEnd") && (_dictionary["mcBoosterRefreshEnd"] as Dictionary<string, object>).ContainsKey("$date"))
		{
			long num = (long)(_dictionary["mcBoosterRefreshEnd"] as Dictionary<string, object>)["$date"];
			num = (long)((double)num * 0.001);
			playerData.mcBoosterRefreshTimeLeft = (double)num;
		}
		if (_dictionary.ContainsKey("carBoosterRefreshEnd") && (_dictionary["carBoosterRefreshEnd"] as Dictionary<string, object>).ContainsKey("$date"))
		{
			long num2 = (long)(_dictionary["carBoosterRefreshEnd"] as Dictionary<string, object>)["$date"];
			num2 = (long)((double)num2 * 0.001);
			playerData.carBoosterRefreshTimeLeft = (double)num2;
		}
		if (_dictionary.ContainsKey("superLikeRefreshEnd") && (_dictionary["superLikeRefreshEnd"] as Dictionary<string, object>).ContainsKey("$date"))
		{
			long num3 = (long)(_dictionary["superLikeRefreshEnd"] as Dictionary<string, object>)["$date"];
			num3 = (long)((double)num3 * 0.001);
			playerData.superLikeRefreshTimeLeft = (double)num3;
		}
		playerData.claimedTutorials = new List<string>();
		if (_dictionary.ContainsKey("claimedTutorials"))
		{
			object[] array = (_dictionary["claimedTutorials"] as List<object>).ToArray();
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					playerData.claimedTutorials.Add((string)array[i]);
				}
			}
		}
		if (_dictionary.ContainsKey("hash"))
		{
			playerData.hash = (string)_dictionary["hash"];
		}
		if (_dictionary.ContainsKey("publishedMinigameCount"))
		{
			playerData.publishedMinigameCount = Convert.ToInt32(_dictionary["publishedMinigameCount"]);
		}
		if (_dictionary.ContainsKey("followerCount"))
		{
			playerData.followerCount = Convert.ToInt32(_dictionary["followerCount"]);
		}
		if (_dictionary.ContainsKey("totalCoinsEarned"))
		{
			playerData.totalCoinsEarned = Convert.ToInt32(_dictionary["totalCoinsEarned"]);
		}
		if (_dictionary.ContainsKey("totalLikes"))
		{
			playerData.totalLikes = Convert.ToInt32(_dictionary["totalLikes"]);
		}
		if (_dictionary.ContainsKey("totalSuperLikes"))
		{
			playerData.totalSuperLikes = Convert.ToInt32(_dictionary["totalSuperLikes"]);
		}
		if (_dictionary.ContainsKey("likes"))
		{
			playerData.creatorLikes = Convert.ToInt32(_dictionary["likes"]);
		}
		if (_dictionary.ContainsKey("coinDoubler"))
		{
			playerData.coinDoubler = Convert.ToBoolean(_dictionary["coinDoubler"]);
		}
		else
		{
			playerData.coinDoubler = false;
		}
		if (_dictionary.ContainsKey("dirtBikeBundle"))
		{
			playerData.dirtBikeBundle = Convert.ToBoolean(_dictionary["dirtBikeBundle"]);
		}
		else
		{
			playerData.dirtBikeBundle = false;
		}
		if (_dictionary.ContainsKey("bundles"))
		{
			playerData.bundlesPurchased = new List<ObscuredString>();
			List<object> list = _dictionary["bundles"] as List<object>;
			foreach (object obj in list)
			{
				playerData.bundlesPurchased.Add((string)obj);
			}
		}
		else
		{
			playerData.bundlesPurchased = new List<ObscuredString>();
		}
		playerData.trailsPurchased = new List<ObscuredString>();
		if (_dictionary.ContainsKey("trails"))
		{
			List<object> list2 = _dictionary["trails"] as List<object>;
			if (list2 != null)
			{
				for (int j = 0; j < list2.Count; j++)
				{
					playerData.trailsPurchased.Add((string)list2[j]);
				}
			}
		}
		playerData.hatsPurchased = new List<ObscuredString>();
		if (_dictionary.ContainsKey("purchasedHats"))
		{
			List<object> list3 = _dictionary["purchasedHats"] as List<object>;
			if (list3 != null)
			{
				for (int k = 0; k < list3.Count; k++)
				{
					playerData.hatsPurchased.Add((string)list3[k]);
				}
			}
		}
		playerData.pendingSpecialOfferChests = new List<GachaType>();
		if (_dictionary.ContainsKey("pendingChests"))
		{
			List<object> list4 = _dictionary["pendingChests"] as List<object>;
			if (list4 != null)
			{
				for (int l = 0; l < list4.Count; l++)
				{
					try
					{
						playerData.pendingSpecialOfferChests.Add((GachaType)Enum.Parse(typeof(GachaType), (string)list4[l]));
					}
					catch (Exception ex)
					{
						Debug.LogError(ex.Message);
					}
				}
			}
		}
		if (_dictionary.ContainsKey("nameChangesDone"))
		{
			playerData.nameChangesDone = Convert.ToInt32(_dictionary["nameChangesDone"]);
		}
		return playerData;
	}

	// Token: 0x06001CDB RID: 7387 RVA: 0x001469E8 File Offset: 0x00144DE8
	public static PsTimedSpecialOffer ParseTimedSpecialOffer(Dictionary<string, object> _serverResponse)
	{
		if (_serverResponse.ContainsKey("currentOfferId"))
		{
			PsTimedSpecialOffer psTimedSpecialOffer = new PsTimedSpecialOffer();
			psTimedSpecialOffer.m_state = 2;
			psTimedSpecialOffer.m_productId = (string)_serverResponse["currentOfferId"];
			psTimedSpecialOffer.m_timeLeft = 0;
			psTimedSpecialOffer.m_startTime = 0L;
			bool flag = false;
			long num = 0L;
			long num2 = 0L;
			if (_serverResponse.ContainsKey("didPurchaseOffer"))
			{
				flag = (bool)_serverResponse["didPurchaseOffer"];
			}
			if (_serverResponse.ContainsKey("lastOfferUpdate"))
			{
				Dictionary<string, object> dictionary = _serverResponse["lastOfferUpdate"] as Dictionary<string, object>;
				if (dictionary != null && dictionary.ContainsKey("$date"))
				{
					num2 = (long)dictionary["$date"];
					num2 = (long)((double)num2 * 0.001);
				}
			}
			if (_serverResponse.ContainsKey("offerResetTime"))
			{
				Dictionary<string, object> dictionary2 = _serverResponse["offerResetTime"] as Dictionary<string, object>;
				if (dictionary2 != null && dictionary2.ContainsKey("$date"))
				{
					num = (long)dictionary2["$date"];
					num = (long)((double)num * 0.001);
				}
			}
			if (!flag)
			{
				int num3 = PsMetagameManager.m_specialOfferCooldownMinutes;
				if (num2 == num)
				{
					num3 += PsMetagameManager.m_specialOfferDurationMinutes;
				}
				if (num + (long)(num3 * 60) < (long)Main.m_EPOCHSeconds || (double)(num2 + (long)(PsMetagameManager.m_specialOfferDurationMinutes * 60)) > Main.m_EPOCHSeconds)
				{
					if (num2 > num)
					{
						psTimedSpecialOffer.m_state = 0;
						psTimedSpecialOffer.m_timeLeft = PsMetagameManager.m_specialOfferDurationMinutes * 60;
					}
					else
					{
						psTimedSpecialOffer.m_state = 1;
						psTimedSpecialOffer.m_timeLeft = (int)(psTimedSpecialOffer.m_startTime + (long)(PsMetagameManager.m_specialOfferDurationMinutes * 60) - (long)Math.Floor(Main.m_EPOCHSeconds));
					}
				}
			}
			psTimedSpecialOffer.m_startTime = num2;
			return psTimedSpecialOffer;
		}
		return null;
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x00146BC9 File Offset: 0x00144FC9
	public static string GetRoleName(TeamRole _role)
	{
		if (_role == TeamRole.Member)
		{
			return "Member";
		}
		if (_role == TeamRole.Creator)
		{
			return "Leader";
		}
		return string.Empty;
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x00146BEC File Offset: 0x00144FEC
	public static TeamData[] ParseTeamList(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("data"))
		{
			List<object> list = dictionary["data"] as List<object>;
			TeamData[] array = new TeamData[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
				array[i] = ClientTools.ParseTeamData(dictionary2);
			}
			return array;
		}
		return null;
	}

	// Token: 0x06001CDE RID: 7390 RVA: 0x00146C6C File Offset: 0x0014506C
	public static TeamData ParseTeam(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParseTeamData(dictionary);
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x00146C90 File Offset: 0x00145090
	public static TeamData ParseTeamData(Dictionary<string, object> _dictionary)
	{
		return new TeamData(_dictionary);
	}

	// Token: 0x06001CE0 RID: 7392 RVA: 0x00146C98 File Offset: 0x00145098
	public static Hashtable CreateTeamDataHashtable(TeamData _data)
	{
		Hashtable hashtable = new Hashtable();
		if (!string.IsNullOrEmpty(_data.id))
		{
			hashtable.Add("id", _data.id);
		}
		hashtable.Add("name", _data.name);
		hashtable.Add("description", _data.description);
		hashtable.Add("joinType", _data.joinType.ToString());
		hashtable.Add("requiredTrophies", _data.requiredTrophies);
		return hashtable;
	}

	// Token: 0x06001CE1 RID: 7393 RVA: 0x00146D24 File Offset: 0x00145124
	public static double GetPriceFromString(string _priceString)
	{
		double num = 0.0;
		try
		{
			string text = "0";
			if (!string.IsNullOrEmpty(_priceString))
			{
				text = new string(Enumerable.ToArray<char>(Enumerable.Where<char>(_priceString, (char c) => char.IsDigit(c) || c == ',' || c == '.')));
				if (Enumerable.Contains<char>(text, ','))
				{
					text = text.Replace(',', '.');
				}
				while (text.EndsWith("."))
				{
					text = text.Substring(0, text.Length - 1);
				}
				while (text.StartsWith("."))
				{
					text = text.Substring(1);
				}
				int num2 = text.LastIndexOf('.');
				if (num2 > 0)
				{
					int num3 = text.IndexOf('.', 0, num2);
					while (num2 > 0 && num3 > 0 && num3 != num2)
					{
						text = text.Remove(num3, 1);
						num2 = text.LastIndexOf('.');
						num3 = text.IndexOf('.', 0, num2);
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				num = double.Parse(text, 167, CultureInfo.InvariantCulture);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Parse error: " + ex);
		}
		return num;
	}

	// Token: 0x06001CE2 RID: 7394 RVA: 0x00146E7C File Offset: 0x0014527C
	public static List<T> ParseList<T>(List<object> _list)
	{
		List<T> list = new List<T>();
		if (_list != null)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				list.Add((T)((object)_list[i]));
			}
		}
		return list;
	}

	// Token: 0x06001CE3 RID: 7395 RVA: 0x00146EC0 File Offset: 0x001452C0
	public static Dictionary<string, ObscuredInt> ParseDictionaryObjectToObscuredInt(Dictionary<string, object> _dictionary)
	{
		Dictionary<string, ObscuredInt> dictionary = new Dictionary<string, ObscuredInt>();
		if (_dictionary != null && _dictionary.Count > 0)
		{
			List<string> list = new List<string>(_dictionary.Keys);
			List<object> list2 = new List<object>(_dictionary.Values);
			for (int i = 0; i < list.Count; i++)
			{
				dictionary.Add(list[i], Convert.ToInt32(list2[i]));
			}
		}
		return dictionary;
	}

	// Token: 0x06001CE4 RID: 7396 RVA: 0x00146F34 File Offset: 0x00145334
	public static Dictionary<string, int> ParseDictionaryObscuredIntToInt(Dictionary<string, ObscuredInt> _dictionary)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		if (_dictionary != null && _dictionary.Count > 0)
		{
			List<string> list = new List<string>(_dictionary.Keys);
			List<ObscuredInt> list2 = new List<ObscuredInt>(_dictionary.Values);
			for (int i = 0; i < list.Count; i++)
			{
				dictionary.Add(list[i], list2[i]);
			}
		}
		return dictionary;
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x00146FA4 File Offset: 0x001453A4
	public static Dictionary<string, bool> ParseDictionaryObjectToBoolean(Dictionary<string, object> _dictionary)
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		if (_dictionary != null && _dictionary.Count > 0)
		{
			List<string> list = new List<string>(_dictionary.Keys);
			List<object> list2 = new List<object>(_dictionary.Values);
			for (int i = 0; i < list.Count; i++)
			{
				dictionary.Add(list[i], (bool)list2[i]);
			}
		}
		return dictionary;
	}

	// Token: 0x06001CE6 RID: 7398 RVA: 0x00147014 File Offset: 0x00145414
	public static Hashtable ParsePlayerUpgrades(Dictionary<string, object> _dictionary)
	{
		Hashtable hashtable = new Hashtable();
		foreach (string text in _dictionary.Keys)
		{
			Dictionary<string, object> dictionary = _dictionary[text] as Dictionary<string, object>;
			if (dictionary != null)
			{
				hashtable.Add(text, new Hashtable(dictionary));
			}
		}
		return hashtable;
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x00147090 File Offset: 0x00145490
	public static PsPlayerProfilerData ParsePlayerProfilerData(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		return ClientTools.ParsePlayerProfilerData(dictionary);
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x001470B4 File Offset: 0x001454B4
	public static PsPlayerProfilerData ParsePlayerProfilerData(Dictionary<string, object> _dictionary)
	{
		PsPlayerProfilerData psPlayerProfilerData = new PsPlayerProfilerData();
		if (_dictionary.ContainsKey("itemLevel"))
		{
			psPlayerProfilerData.m_itemLevel = Convert.ToInt32(_dictionary["itemLevel"]);
		}
		if (_dictionary.ContainsKey("skill"))
		{
			psPlayerProfilerData.m_skills.Add(new PsPlayerProfilerData.ProfilerVehicleData("OffroadCar", Convert.ToInt32(_dictionary["skill"]), Convert.ToInt32(_dictionary["offroadCarPreference"])));
		}
		if (_dictionary.ContainsKey("motorcycleSkill"))
		{
			psPlayerProfilerData.m_skills.Add(new PsPlayerProfilerData.ProfilerVehicleData("Motorcycle", Convert.ToInt32(_dictionary["motorcycleSkill"]), Convert.ToInt32(_dictionary["motorcyclePreference"])));
		}
		if (_dictionary.ContainsKey("searchPrefs"))
		{
			List<object> list = _dictionary["searchPrefs"] as List<object>;
			if (list != null)
			{
				foreach (object obj in list)
				{
					Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
					if (dictionary != null)
					{
						foreach (KeyValuePair<string, object> keyValuePair in dictionary)
						{
							string key = keyValuePair.Key;
							double num = Convert.ToDouble(keyValuePair.Value);
							psPlayerProfilerData.m_subgenrePrefs.Add(key, (float)num);
						}
					}
				}
			}
		}
		return psPlayerProfilerData;
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x0014725C File Offset: 0x0014565C
	public static PlayerData ParseFollowPlayerData(Dictionary<string, object> _dictionary)
	{
		PlayerData playerData = default(PlayerData);
		playerData.playerId = (string)_dictionary["id"];
		playerData.name = (string)_dictionary["name"];
		if (_dictionary.ContainsKey("tag"))
		{
			playerData.tag = (string)_dictionary["tag"];
		}
		if (_dictionary.ContainsKey("acceptNotifications"))
		{
			playerData.acceptNotifications = (bool)_dictionary["acceptNotifications"];
		}
		if (_dictionary.ContainsKey("facebookId"))
		{
			playerData.facebookId = (string)_dictionary["facebookId"];
		}
		if (_dictionary.ContainsKey("gameCenterId"))
		{
			playerData.gameCenterId = (string)_dictionary["gameCenterId"];
		}
		if (_dictionary.ContainsKey("ninjaCreationTimestamp"))
		{
			playerData.ninjaCreationTimestamp = (string)_dictionary["ninjaCreationTimestamp"];
		}
		if (_dictionary.ContainsKey("countryCode"))
		{
			playerData.countryCode = (string)_dictionary["countryCode"];
		}
		if (_dictionary.ContainsKey("itemDbVersion"))
		{
			playerData.itemDbVersion = Convert.ToInt32(_dictionary["itemDbVersion"]);
		}
		if (_dictionary.ContainsKey("publishedMinigameCount"))
		{
			playerData.publishedMinigameCount = Convert.ToInt32(_dictionary["publishedMinigameCount"]);
		}
		if (_dictionary.ContainsKey("followerCount"))
		{
			playerData.followerCount = Convert.ToInt32(_dictionary["followerCount"]);
		}
		if (_dictionary.ContainsKey("totalCoinsEarned"))
		{
			playerData.totalCoinsEarned = Convert.ToInt32(_dictionary["totalCoinsEarned"]);
		}
		if (_dictionary.ContainsKey("totalLikes"))
		{
			playerData.totalLikes = Convert.ToInt32(_dictionary["totalLikes"]);
		}
		if (_dictionary.ContainsKey("totalSuperLikes"))
		{
			playerData.totalSuperLikes = Convert.ToInt32(_dictionary["totalSuperLikes"]);
		}
		if (_dictionary.ContainsKey("mcTrophies"))
		{
			playerData.mcTrophies = Convert.ToInt32(_dictionary["mcTrophies"]);
		}
		if (_dictionary.ContainsKey("carTrophies"))
		{
			playerData.carTrophies = Convert.ToInt32(_dictionary["carTrophies"]);
		}
		if (_dictionary.ContainsKey("bigBangPoints"))
		{
			playerData.bigBangPoints = Convert.ToInt32(_dictionary["bigBangPoints"]);
		}
		if (_dictionary.ContainsKey("completedAdventures"))
		{
			playerData.adventureLevelsCompleted = Convert.ToInt32(_dictionary["completedAdventures"]);
		}
		if (_dictionary.ContainsKey("racesWon"))
		{
			playerData.racesWon = Convert.ToInt32(_dictionary["racesWon"]);
		}
		if (_dictionary.ContainsKey("goodOrBadLevelsRated"))
		{
			playerData.newLevelsRated = Convert.ToInt32(_dictionary["goodOrBadLevelsRated"]);
		}
		if (_dictionary.ContainsKey("teamId"))
		{
			playerData.teamId = (string)_dictionary["teamId"];
		}
		if (_dictionary.ContainsKey("teamName"))
		{
			playerData.teamName = (string)_dictionary["teamName"];
		}
		if (_dictionary.ContainsKey("teamRole"))
		{
			playerData.teamRole = (TeamRole)Enum.Parse(typeof(TeamRole), (string)_dictionary["teamRole"]);
		}
		if (_dictionary.ContainsKey("hasJoinedTeam"))
		{
			playerData.hasJoinedTeam = Convert.ToBoolean(_dictionary["hasJoinedTeam"]);
		}
		if (_dictionary.ContainsKey("reward"))
		{
			playerData.seasonReward = Convert.ToInt32(_dictionary["reward"]);
		}
		if (_dictionary.ContainsKey("lastSeasonEndCarTrophies"))
		{
			playerData.lastSeasonEndCarTrophies = Convert.ToInt32(_dictionary["lastSeasonEndCarTrophies"]);
		}
		if (_dictionary.ContainsKey("lastSeasonEndMcTrophies"))
		{
			playerData.lastSeasonEndMcTrophies = Convert.ToInt32(_dictionary["lastSeasonEndMcTrophies"]);
		}
		if (_dictionary.ContainsKey("racesThisSeason"))
		{
			playerData.racesThisSeason = Convert.ToInt32(_dictionary["racesThisSeason"]);
		}
		if (_dictionary.ContainsKey("youtuber"))
		{
			playerData.youtubeName = (string)_dictionary["youtuber"];
		}
		if (_dictionary.ContainsKey("youtuberId"))
		{
			playerData.youtubeId = (string)_dictionary["youtuberId"];
		}
		if (_dictionary.ContainsKey("youtubeSubscriberCount"))
		{
			playerData.youtubeSubscriberCount = Convert.ToInt32(_dictionary["youtubeSubscriberCount"]);
		}
		if (_dictionary.ContainsKey("completedSurvey"))
		{
			playerData.completedSurvey = Convert.ToBoolean(_dictionary["completedSurvey"]);
		}
		if (_dictionary.ContainsKey("gender"))
		{
			playerData.gender = Convert.ToString(_dictionary["gender"]);
		}
		if (_dictionary.ContainsKey("ageGroup"))
		{
			playerData.ageGroup = Convert.ToString(_dictionary["ageGroup"]);
		}
		if (_dictionary.ContainsKey("developer"))
		{
			playerData.developer = (bool)_dictionary["developer"];
		}
		else
		{
			playerData.developer = false;
		}
		playerData.teamRoleName = ClientTools.GetRoleName(playerData.teamRole);
		return playerData;
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x001477DC File Offset: 0x00145BDC
	public static Hashtable CreateMetagameNodeDataJSON(MetagameNodeData _data)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("nodeType", _data.m_nodeDataType.ToString());
		if (_data.m_nodeDataType == MetagameNodeDataType.Unlock)
		{
			hashtable.Add("itemName", _data.m_unlockableName);
			hashtable.Add("itemDescription", _data.m_unlockableDescription);
			hashtable.Add("itemIcon", _data.m_unlockableIcon);
			hashtable.Add("itemClass", _data.m_dataIdentifier);
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.UnlockableCategory)
		{
			hashtable.Add("itemType", _data.m_unlockableType);
			hashtable.Add("itemName", _data.m_unlockableName);
			hashtable.Add("itemDescription", _data.m_unlockableDescription);
			hashtable.Add("itemIcon", _data.m_unlockableIcon);
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.Unlockable)
		{
			hashtable.Add("itemName", _data.m_unlockableName);
			hashtable.Add("itemDescription", _data.m_unlockableDescription);
			hashtable.Add("itemIcon", _data.m_unlockableIcon);
			hashtable.Add("itemNodeType", _data.m_unlockableGraphNodeClass);
			hashtable.Add("itemClass", _data.m_dataIdentifier);
			hashtable.Add("itemMaxCount", _data.m_unlockableMaxCount);
			hashtable.Add("itemComplexity", _data.m_unlockableComplexity);
			hashtable.Add("itemLevel", _data.m_unlockableItemLevel);
			hashtable.Add("itemGachaProbability", _data.m_unlockableGachaProbability);
			hashtable.Add("itemRarity", _data.m_unlockableRarity);
			hashtable.Add("itemCurrency", _data.m_unlockableCurrency);
			hashtable.Add("itemPrice", _data.m_unlockablePrice);
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.UnlockableSimple)
		{
			hashtable.Add("itemName", _data.m_unlockableName);
			hashtable.Add("itemDescription", _data.m_unlockableDescription);
			hashtable.Add("itemIcon", _data.m_unlockableIcon);
			hashtable.Add("itemClass", _data.m_dataIdentifier);
			hashtable.Add("itemLevel", _data.m_unlockableItemLevel);
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.UnlockableUpgradeable)
		{
			hashtable.Add("itemName", _data.m_unlockableName);
			hashtable.Add("itemDescription", _data.m_unlockableDescription);
			hashtable.Add("itemIcon", _data.m_unlockableIcon);
			hashtable.Add("itemNodeType", _data.m_unlockableGraphNodeClass);
			hashtable.Add("itemClass", _data.m_dataIdentifier);
			hashtable.Add("itemMaxCount", _data.m_unlockableMaxCount);
			hashtable.Add("itemComplexity", _data.m_unlockableComplexity);
			hashtable.Add("itemLevel", _data.m_unlockableItemLevel);
			hashtable.Add("vehicleUpgradeValues", _data.m_unlockableUpgradeValues);
			hashtable.Add("vehicleUpgradeSteps", _data.m_unlockableUpgradeSteps);
			hashtable.Add("vehicleUpgradePrices", _data.m_unlockableUpgradePrices);
			hashtable.Add("rentName", _data.m_unlockableRentName);
			hashtable.Add("rentButton", _data.m_unlockableRentButton);
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.Level)
		{
			if (_data.m_levelMinigameId != string.Empty)
			{
				hashtable.Add("levelMinigameId", _data.m_levelMinigameId);
			}
			hashtable.Add("levelDifficulty", _data.m_levelDifficulty);
			hashtable.Add("levelPlayerUnit", _data.m_levelPlayerUnit);
			hashtable.Add("levelSubgenre", _data.m_levelSubgenre.ToString());
			hashtable.Add("levelGameMode", _data.m_levelGameMode);
			hashtable.Add("levelItems", _data.m_levelItems.ToArray());
			if (_data.m_levelMedalTimes != null && _data.m_levelMedalTimes.Length > 0 && !string.IsNullOrEmpty(_data.m_levelMedalTimes[0]))
			{
				hashtable.Add("levelMedalTimes", _data.m_levelMedalTimes);
			}
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.EditorPuzzle)
		{
			if (_data.m_levelMinigameId != string.Empty)
			{
				hashtable.Add("levelMinigameId", _data.m_levelMinigameId);
			}
			hashtable.Add("puzzleLimitedItems", _data.m_levelItems.ToArray());
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.Signal)
		{
			hashtable.Add("levelMinigameId", _data.m_levelMinigameId);
			hashtable.Add("blockUnlocked", _data.m_blockUnlocked);
			hashtable.Add("blockWillUnlock", _data.m_blockWillUnlock);
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.VersusRace)
		{
			hashtable.Add("levelMinigameId", _data.m_levelMinigameId);
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.Block)
		{
			hashtable.Add("levelMinigameId", _data.m_levelMinigameId);
			hashtable.Add("blockRequiredStars", _data.m_blockRequiredStars);
			hashtable.Add("blockType", _data.m_blockType);
			hashtable.Add("blockChestType", _data.m_blockChestType);
			hashtable.Add("blockBolts", _data.m_blockBolts);
			hashtable.Add("blockCoins", _data.m_blockCoins);
			hashtable.Add("blockDiamonds", _data.m_blockDiamonds);
			hashtable.Add("blockKeys", _data.m_blockKeys);
			hashtable.Add("blockUnlocked", _data.m_blockUnlocked);
			hashtable.Add("blockWillUnlock", _data.m_blockWillUnlock);
			hashtable.Add("blockRewardCue", _data.m_dataIdentifier);
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.Dialogue)
		{
			hashtable.Add("dialogueIdentifier", _data.m_dataIdentifier);
			hashtable.Add("dialogueTrigger", _data.m_dialogueTrigger.ToString());
			hashtable.Add("dialogueCharacter", _data.m_dialogueCharacter.ToString());
			hashtable.Add("dialogueCharacterPosition", _data.m_dialogueCharacterPosition.ToString());
			hashtable.Add("dialogueText", _data.m_dialogueText);
			hashtable.Add("dialogueProceed", _data.m_dialogueProceed);
			hashtable.Add("dialogueCancel", _data.m_dialogueCancel);
			hashtable.Add("dialogueTextLocalized", _data.m_dialogueTextLocalized);
		}
		if (_data.m_nodeDataType == MetagameNodeDataType.LevelTemplate)
		{
			hashtable.Add("levelTemplateIdentifier", _data.m_dataIdentifier);
			hashtable.Add("levelMinigameId", _data.m_levelMinigameId);
			hashtable.Add("levelPlayerUnit", _data.m_levelPlayerUnit);
			hashtable.Add("levelGameMode", _data.m_levelGameMode.ToString());
			hashtable.Add("levelTemplateDomeSize", _data.m_levelTemplateDomeSize.ToString());
			hashtable.Add("levelTemplateItems", _data.m_levelItems.ToArray());
			hashtable.Add("levelTemplateMinigames", _data.m_levelTemplateMinigames.ToArray());
		}
		return hashtable;
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x00147EDC File Offset: 0x001462DC
	public static MetagameNodeData ParseMetagameNodeDataJSON(Dictionary<string, object> _dataDict)
	{
		MetagameNodeData metagameNodeData = new MetagameNodeData(string.Empty);
		try
		{
			metagameNodeData.m_nodeDataType = (MetagameNodeDataType)Enum.Parse(typeof(MetagameNodeDataType), (string)_dataDict["nodeType"], true);
		}
		catch
		{
			string text = (string)_dataDict["nodeType"];
			if (text == "ItemCategory")
			{
				metagameNodeData.m_nodeDataType = MetagameNodeDataType.UnlockableCategory;
			}
			else if (text == "Item")
			{
				metagameNodeData.m_nodeDataType = MetagameNodeDataType.Unlockable;
			}
			else if (text == "ItemPlayer")
			{
				metagameNodeData.m_nodeDataType = MetagameNodeDataType.UnlockableUpgradeable;
			}
			else
			{
				metagameNodeData.m_nodeDataType = MetagameNodeDataType.Undefined;
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.Unlock)
		{
			metagameNodeData.m_unlockableName = (string)_dataDict["itemName"];
			metagameNodeData.m_unlockableIcon = (string)_dataDict["itemIcon"];
			metagameNodeData.m_unlockableDescription = (string)_dataDict["itemDescription"];
			try
			{
				metagameNodeData.m_dataIdentifier = (string)_dataDict["itemClass"];
			}
			catch
			{
				metagameNodeData.m_dataIdentifier = string.Empty;
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.UnlockableCategory)
		{
			metagameNodeData.m_unlockableType = (PsUnlockableType)Enum.Parse(typeof(PsUnlockableType), (string)_dataDict["itemType"]);
			metagameNodeData.m_unlockableName = (string)_dataDict["itemName"];
			metagameNodeData.m_unlockableDescription = (string)_dataDict["itemDescription"];
			metagameNodeData.m_unlockableIcon = (string)_dataDict["itemIcon"];
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.Unlockable)
		{
			metagameNodeData.m_unlockableName = (string)_dataDict["itemName"];
			metagameNodeData.m_unlockableDescription = (string)_dataDict["itemDescription"];
			metagameNodeData.m_unlockableIcon = (string)_dataDict["itemIcon"];
			try
			{
				metagameNodeData.m_unlockableGraphNodeClass = (string)_dataDict["itemNodeType"];
			}
			catch
			{
				metagameNodeData.m_unlockableGraphNodeClass = typeof(GraphNode).ToString();
			}
			try
			{
				metagameNodeData.m_dataIdentifier = (string)_dataDict["itemClass"];
			}
			catch
			{
				metagameNodeData.m_dataIdentifier = typeof(BasicAssembledClass).ToString();
			}
			metagameNodeData.m_unlockableMaxCount = Convert.ToInt32(_dataDict["itemMaxCount"]);
			metagameNodeData.m_unlockableComplexity = Convert.ToInt32(_dataDict["itemComplexity"]);
			try
			{
				metagameNodeData.m_unlockableItemLevel = Convert.ToInt32(_dataDict["itemLevel"]);
			}
			catch
			{
				metagameNodeData.m_unlockableItemLevel = 0;
			}
			if (_dataDict.ContainsKey("itemGachaProbability"))
			{
				metagameNodeData.m_unlockableGachaProbability = (float)Convert.ToDouble(_dataDict["itemGachaProbability"]);
			}
			else
			{
				metagameNodeData.m_unlockableGachaProbability = 1f;
			}
			if (_dataDict.ContainsKey("itemRarity"))
			{
				metagameNodeData.m_unlockableRarity = (PsRarity)Enum.Parse(typeof(PsRarity), (string)_dataDict["itemRarity"]);
			}
			else
			{
				metagameNodeData.m_unlockableRarity = PsRarity.Common;
			}
			if (_dataDict.ContainsKey("itemCurrency"))
			{
				metagameNodeData.m_unlockableCurrency = (PsCurrency)Enum.Parse(typeof(PsCurrency), (string)_dataDict["itemCurrency"]);
			}
			else
			{
				metagameNodeData.m_unlockableCurrency = PsCurrency.None;
			}
			if (_dataDict.ContainsKey("itemPrice"))
			{
				metagameNodeData.m_unlockablePrice = Convert.ToInt32(_dataDict["itemPrice"]);
			}
			else
			{
				metagameNodeData.m_unlockablePrice = 0;
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.UnlockableSimple)
		{
			metagameNodeData.m_unlockableName = (string)_dataDict["itemName"];
			metagameNodeData.m_unlockableDescription = (string)_dataDict["itemDescription"];
			metagameNodeData.m_unlockableIcon = (string)_dataDict["itemIcon"];
			try
			{
				metagameNodeData.m_dataIdentifier = (string)_dataDict["itemClass"];
			}
			catch
			{
				metagameNodeData.m_dataIdentifier = typeof(BasicAssembledClass).ToString();
			}
			try
			{
				metagameNodeData.m_unlockableItemLevel = Convert.ToInt32(_dataDict["itemLevel"]);
			}
			catch
			{
				metagameNodeData.m_unlockableItemLevel = 0;
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.UnlockableUpgradeable)
		{
			metagameNodeData.m_unlockableName = (string)_dataDict["itemName"];
			metagameNodeData.m_unlockableDescription = (string)_dataDict["itemDescription"];
			metagameNodeData.m_unlockableIcon = (string)_dataDict["itemIcon"];
			metagameNodeData.m_unlockableGraphNodeClass = (string)_dataDict["itemNodeType"];
			metagameNodeData.m_dataIdentifier = (string)_dataDict["itemClass"];
			metagameNodeData.m_unlockableMaxCount = Convert.ToInt32(_dataDict["itemMaxCount"]);
			metagameNodeData.m_unlockableComplexity = Convert.ToInt32(_dataDict["itemComplexity"]);
			try
			{
				metagameNodeData.m_unlockableItemLevel = Convert.ToInt32(_dataDict["itemLevel"]);
			}
			catch
			{
				metagameNodeData.m_unlockableItemLevel = 0;
			}
			try
			{
				metagameNodeData.m_unlockableRentName = (string)_dataDict["rentName"];
			}
			catch
			{
				metagameNodeData.m_unlockableRentName = "Rentzal";
			}
			try
			{
				metagameNodeData.m_unlockableRentButton = (string)_dataDict["rentButton"];
			}
			catch
			{
				metagameNodeData.m_unlockableRentButton = "Rentz this";
			}
			if (_dataDict.ContainsKey("vehicleUpgradeValues"))
			{
				metagameNodeData.m_unlockableUpgradeValues = new Hashtable(_dataDict["vehicleUpgradeValues"] as Dictionary<string, object>);
				Hashtable hashtable = new Hashtable();
				IEnumerator enumerator = metagameNodeData.m_unlockableUpgradeValues.Keys.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						string text2 = (string)obj;
						List<object> list = metagameNodeData.m_unlockableUpgradeValues[text2] as List<object>;
						if (list != null)
						{
							object[] array = Enumerable.ToArray<object>(Enumerable.Cast<object>(list));
							if (array != null)
							{
								hashtable.Add(text2, array);
							}
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
				metagameNodeData.m_unlockableUpgradeValues = hashtable;
			}
			else
			{
				Debug.Log("UPGRADE VALUES NOT FOUND", null);
			}
			metagameNodeData.m_unlockableUpgradeSteps = Convert.ToInt32(_dataDict["vehicleUpgradeSteps"]);
			List<object> list2 = (List<object>)_dataDict["vehicleUpgradePrices"];
			metagameNodeData.m_unlockableUpgradePrices = new string[list2.Count];
			for (int i = 0; i < list2.Count; i++)
			{
				metagameNodeData.m_unlockableUpgradePrices[i] = (string)list2[i];
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.Level)
		{
			if (_dataDict.ContainsKey("levelMinigameId"))
			{
				metagameNodeData.m_levelMinigameId = (string)_dataDict["levelMinigameId"];
			}
			if (_dataDict.ContainsKey("levelDifficulty"))
			{
				metagameNodeData.m_levelDifficulty = (PsGameDifficulty)Enum.Parse(typeof(PsGameDifficulty), (string)_dataDict["levelDifficulty"], true);
			}
			if (_dataDict.ContainsKey("levelPlayerUnit"))
			{
				metagameNodeData.m_levelPlayerUnit = (string)_dataDict["levelPlayerUnit"];
			}
			if (_dataDict.ContainsKey("levelSubgenre"))
			{
				metagameNodeData.m_levelSubgenre = (PsSubgenre)Enum.Parse(typeof(PsSubgenre), (string)_dataDict["levelSubgenre"], true);
			}
			if (_dataDict.ContainsKey("levelGameMode"))
			{
				metagameNodeData.m_levelGameMode = (PsGameMode)Enum.Parse(typeof(PsGameMode), (string)_dataDict["levelGameMode"], true);
			}
			List<object> list3 = null;
			if (_dataDict.ContainsKey("levelItems"))
			{
				list3 = (List<object>)_dataDict["levelItems"];
			}
			metagameNodeData.m_levelItems = new List<string>();
			if (list3 != null)
			{
				for (int j = 0; j < list3.Count; j++)
				{
					metagameNodeData.m_levelItems.Add((string)list3[j]);
				}
			}
			if (_dataDict.ContainsKey("levelMedalTimes"))
			{
				List<object> list4 = (List<object>)_dataDict["levelMedalTimes"];
				metagameNodeData.m_levelMedalTimes = new string[3];
				for (int k = 0; k < list4.Count; k++)
				{
					metagameNodeData.m_levelMedalTimes[k] = (string)list4[k];
				}
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.EditorPuzzle)
		{
			if (_dataDict.ContainsKey("levelMinigameId"))
			{
				metagameNodeData.m_levelMinigameId = (string)_dataDict["levelMinigameId"];
			}
			metagameNodeData.m_levelItems = new List<string>();
			if (_dataDict.ContainsKey("puzzleLimitedItems"))
			{
				List<object> list5 = (List<object>)_dataDict["puzzleLimitedItems"];
				for (int l = 0; l < list5.Count; l++)
				{
					metagameNodeData.m_levelItems.Add((string)list5[l]);
				}
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.Signal)
		{
			metagameNodeData.m_levelMinigameId = (string)_dataDict["levelMinigameId"];
			metagameNodeData.m_blockUnlocked = (string)_dataDict["blockUnlocked"];
			try
			{
				metagameNodeData.m_blockWillUnlock = (string)_dataDict["blockWillUnlock"];
			}
			catch
			{
				metagameNodeData.m_blockWillUnlock = string.Empty;
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.VersusRace)
		{
			try
			{
				metagameNodeData.m_levelMinigameId = (string)_dataDict["levelMinigameId"];
			}
			catch
			{
				metagameNodeData.m_levelMinigameId = string.Empty;
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.Block)
		{
			try
			{
				metagameNodeData.m_levelMinigameId = (string)_dataDict["levelMinigameId"];
			}
			catch
			{
				metagameNodeData.m_levelMinigameId = string.Empty;
			}
			metagameNodeData.m_blockRequiredStars = Convert.ToInt32(_dataDict["blockRequiredStars"]);
			metagameNodeData.m_blockType = (PsPathBlockType)Enum.Parse(typeof(PsPathBlockType), (string)_dataDict["blockType"]);
			metagameNodeData.m_blockBolts = Convert.ToInt32(_dataDict["blockBolts"]);
			metagameNodeData.m_blockCoins = Convert.ToInt32(_dataDict["blockCoins"]);
			metagameNodeData.m_blockDiamonds = Convert.ToInt32(_dataDict["blockDiamonds"]);
			try
			{
				metagameNodeData.m_blockKeys = Convert.ToInt32(_dataDict["blockKeys"]);
			}
			catch
			{
				metagameNodeData.m_blockKeys = 0;
			}
			metagameNodeData.m_blockUnlocked = (string)_dataDict["blockUnlocked"];
			try
			{
				metagameNodeData.m_blockWillUnlock = (string)_dataDict["blockWillUnlock"];
			}
			catch
			{
				metagameNodeData.m_blockWillUnlock = string.Empty;
			}
			try
			{
				metagameNodeData.m_dataIdentifier = (string)_dataDict["blockRewardCue"];
			}
			catch
			{
				metagameNodeData.m_dataIdentifier = string.Empty;
			}
			try
			{
				metagameNodeData.m_blockChestType = (PsPathBlockChestType)Enum.Parse(typeof(PsPathBlockChestType), (string)_dataDict["blockChestType"]);
			}
			catch
			{
				metagameNodeData.m_blockChestType = PsPathBlockChestType.Silver;
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.Dialogue)
		{
			metagameNodeData.m_dataIdentifier = (string)_dataDict["dialogueIdentifier"];
			try
			{
				metagameNodeData.m_dialogueTrigger = (PsNodeEventTrigger)Enum.Parse(typeof(PsNodeEventTrigger), (string)_dataDict["dialogueTrigger"]);
			}
			catch
			{
				metagameNodeData.m_dialogueTrigger = PsNodeEventTrigger.MinigameLose;
			}
			metagameNodeData.m_dialogueCharacterPosition = (PsDialogueCharacterPosition)Enum.Parse(typeof(PsDialogueCharacterPosition), (string)_dataDict["dialogueCharacterPosition"]);
			metagameNodeData.m_dialogueCharacter = (PsDialogueCharacter)Enum.Parse(typeof(PsDialogueCharacter), (string)_dataDict["dialogueCharacter"]);
			metagameNodeData.m_dialogueText = (string)_dataDict["dialogueText"];
			metagameNodeData.m_dialogueProceed = (string)_dataDict["dialogueProceed"];
			metagameNodeData.m_dialogueCancel = (string)_dataDict["dialogueCancel"];
			try
			{
				metagameNodeData.m_dialogueTextLocalized = (StringID)Enum.Parse(typeof(StringID), (string)_dataDict["dialogueTextLocalized"]);
			}
			catch
			{
				metagameNodeData.m_dialogueTextLocalized = StringID.EMPTY;
			}
		}
		if (metagameNodeData.m_nodeDataType == MetagameNodeDataType.LevelTemplate)
		{
			metagameNodeData.m_dataIdentifier = (string)_dataDict["levelTemplateIdentifier"];
			try
			{
				metagameNodeData.m_levelMinigameId = (string)_dataDict["levelMinigameId"];
			}
			catch
			{
				metagameNodeData.m_levelMinigameId = string.Empty;
			}
			metagameNodeData.m_levelPlayerUnit = (string)_dataDict["levelPlayerUnit"];
			metagameNodeData.m_levelGameMode = (PsGameMode)Enum.Parse(typeof(PsGameMode), (string)_dataDict["levelGameMode"]);
			metagameNodeData.m_levelTemplateDomeSize = (PsGameArea)Enum.Parse(typeof(PsGameArea), (string)_dataDict["levelTemplateDomeSize"]);
			List<object> list6 = (List<object>)_dataDict["levelTemplateItems"];
			metagameNodeData.m_levelItems = new List<string>();
			for (int m = 0; m < list6.Count; m++)
			{
				metagameNodeData.m_levelItems.Add((string)list6[m]);
			}
			if (_dataDict.ContainsKey("levelTemplateMinigames"))
			{
				List<object> list7 = (List<object>)_dataDict["levelTemplateMinigames"];
				metagameNodeData.m_levelTemplateMinigames = new List<string>();
				for (int n = 0; n < list7.Count; n++)
				{
					metagameNodeData.m_levelTemplateMinigames.Add((string)list7[n]);
				}
			}
		}
		return metagameNodeData;
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x00148ED0 File Offset: 0x001472D0
	public static Dictionary<int, PsMetagameDataHelper> ParseInitialGraphNodes(Dictionary<string, object> _dataDict)
	{
		Dictionary<int, PsMetagameDataHelper> dictionary = new Dictionary<int, PsMetagameDataHelper>();
		if (_dataDict.ContainsKey("nodes"))
		{
			List<object> list = _dataDict["nodes"] as List<object>;
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
				int num = Convert.ToInt32(dictionary2["id"]);
				string text = (string)dictionary2["name"];
				float num2 = Convert.ToSingle(dictionary2["x"]);
				float num3 = Convert.ToSingle(dictionary2["y"]);
				Dictionary<string, object> dictionary3 = dictionary2["data"] as Dictionary<string, object>;
				MetagameNodeData metagameNodeData = ClientTools.ParseMetagameNodeDataJSON(dictionary3);
				dictionary.Add(num, new PsMetagameDataHelper
				{
					m_id = num,
					m_name = text,
					m_x = num2,
					m_y = num3,
					m_metagameNodeData = metagameNodeData
				});
			}
		}
		return dictionary;
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x00148FCC File Offset: 0x001473CC
	public static void LinkInitialGraphNodes(Dictionary<int, PsMetagameDataHelper> _nodes, Dictionary<string, object> _dataDict)
	{
		if (_dataDict.ContainsKey("edges"))
		{
			List<object> list = _dataDict["edges"] as List<object>;
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary = list[i] as Dictionary<string, object>;
				int num = Convert.ToInt32(dictionary["source"]);
				int num2 = Convert.ToInt32(dictionary["target"]);
				PsMetagameDataHelper psMetagameDataHelper = _nodes[num];
				PsMetagameDataHelper psMetagameDataHelper2 = _nodes[num2];
				psMetagameDataHelper.m_outputs.Add(psMetagameDataHelper2);
				psMetagameDataHelper2.m_inputs.Add(psMetagameDataHelper);
			}
		}
	}

	// Token: 0x06001CEE RID: 7406 RVA: 0x00149070 File Offset: 0x00147470
	public static void ParseInitialGraphData(HttpC _c, int _pathVersion = 0, string _planetIdentifier = "Adventure")
	{
		string @string = Encoding.UTF8.GetString(FilePacker.UnZipBytes(_c.www.bytes));
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(@string);
		ClientTools.ParseInitialGraphData(dictionary, (_pathVersion == 0) ? PlayerPrefsX.GetPathVersion() : _pathVersion, _planetIdentifier);
		PsPlanetSerializer.SaveLocalPlanetInitialData(Json.Serialize(dictionary), _planetIdentifier);
		dictionary["version"] = _pathVersion;
		PlanetTools.m_localPlanets[_planetIdentifier] = dictionary;
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x001490E0 File Offset: 0x001474E0
	public static void ParseInitialGraphData(Dictionary<string, object> _dataDict, int _pathVersion, string _planetIdentifier)
	{
		Dictionary<int, PsMetagameDataHelper> dictionary = ClientTools.ParseInitialGraphNodes(_dataDict);
		ClientTools.LinkInitialGraphNodes(dictionary, _dataDict);
		foreach (KeyValuePair<int, PsMetagameDataHelper> keyValuePair in dictionary)
		{
			PsMetagameDataHelper value = keyValuePair.Value;
			if (value.m_inputs.Count == 0)
			{
				if (value.m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.UnlockableCategory)
				{
					PsMetagameData.AddCategory(value, null);
				}
				else if (value.m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Dialogue)
				{
					PsMetagameData.AddDialogue(value);
				}
			}
			if (value.m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.LevelTemplate)
			{
				PsMetagameData.AddLevelTemplate(value);
			}
		}
		foreach (KeyValuePair<int, PsMetagameDataHelper> keyValuePair2 in dictionary)
		{
			PsMetagameDataHelper value2 = keyValuePair2.Value;
			if (value2.m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Unlock)
			{
				bool flag = false;
				for (int i = 0; i < value2.m_inputs.Count; i++)
				{
					if (value2.m_inputs[i].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Unlock)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					PsMetagameData.AddUnlocks(value2, _planetIdentifier);
				}
			}
		}
		Debug.Log("UNITS: " + PsMetagameData.m_units.Count, null);
		Debug.Log("PLAYER UNITS: " + PsMetagameData.m_playerUnits.Count, null);
		Debug.Log("GAME MODES: " + PsMetagameData.m_gameModes.Count, null);
		Debug.Log("GAME MATERIALS: " + PsMetagameData.m_gameMaterials.Count, null);
		Debug.Log("GAME AREAS: " + PsMetagameData.m_gameAreas.Count, null);
		Debug.Log("GAME ENVIRONMENTS: " + PsMetagameData.m_gameEnvironments.Count, null);
		Debug.Log("MENUS: " + PsMetagameData.m_menus.Count, null);
		if (PsMetagameData.GetPlanetUnlocks(_planetIdentifier) != null)
		{
			Debug.Log(_planetIdentifier + " UNLOCKS: " + PsMetagameData.GetPlanetUnlocks(_planetIdentifier).Count, null);
		}
		dictionary = null;
		PlayerPrefsX.SetLocalPlanetVersion(_planetIdentifier, _pathVersion);
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x0014936C File Offset: 0x0014776C
	public static List<PsPlanetPath> ParseProgressionPathData(List<object> _paths, bool _local = true)
	{
		List<PsPlanetPath> list = new List<PsPlanetPath>();
		PsPlanetPath psPlanetPath = null;
		List<PsPlanetPath> list2 = new List<PsPlanetPath>();
		for (int i = 0; i < _paths.Count; i++)
		{
			Dictionary<string, object> dictionary = _paths[i] as Dictionary<string, object>;
			string text = (string)dictionary["name"];
			int num = Convert.ToInt32(dictionary["currentNode"]);
			int num2 = 0;
			if (dictionary.ContainsKey("lane"))
			{
				num2 = Convert.ToInt32(dictionary["lane"]);
			}
			int num3 = 0;
			if (dictionary.ContainsKey("startNode"))
			{
				num3 = Convert.ToInt32(dictionary["startNode"]);
			}
			bool flag = false;
			if (dictionary.ContainsKey("overwrite"))
			{
				flag = (bool)dictionary["overwrite"];
			}
			PsPlanetPathType psPlanetPathType = PsPlanetPathType.none;
			if (dictionary.ContainsKey("type"))
			{
				psPlanetPathType = (PsPlanetPathType)Enum.Parse(typeof(PsPlanetPathType), (string)dictionary["type"], true);
			}
			if (psPlanetPathType == PsPlanetPathType.none)
			{
				if (text.Contains("MainPath"))
				{
					psPlanetPathType = PsPlanetPathType.main;
				}
				else if (text.Contains("FloatingPath"))
				{
					psPlanetPathType = PsPlanetPathType.floating;
				}
				else if (text.Contains("SidePath"))
				{
					psPlanetPathType = PsPlanetPathType.side;
				}
				Debug.LogError("Path '" + text + "': didnt have type, manual type was set to: " + psPlanetPathType.ToString());
			}
			string text2 = string.Empty;
			if (dictionary.ContainsKey("planet"))
			{
				text2 = (string)dictionary["planet"];
			}
			else
			{
				Debug.LogError("Path '" + text + "' did not have planet identifier! defaulting to 'RacingMotorcycle'");
				text2 = "RacingMotorcycle";
			}
			PsPlanetPath psPlanetPath2 = null;
			if (psPlanetPathType == PsPlanetPathType.main)
			{
				psPlanetPath = new PsPlanetPath(text, text2, psPlanetPathType);
				psPlanetPath2 = psPlanetPath;
			}
			else
			{
				psPlanetPath2 = new PsPlanetPath(text, text2, psPlanetPathType);
				list2.Add(psPlanetPath2);
			}
			psPlanetPath2.m_currentNodeId = num;
			psPlanetPath2.m_startNodeId = num3;
			psPlanetPath2.m_lane = num2;
			psPlanetPath2.m_overwrite = flag;
			list.Add(psPlanetPath2);
			List<object> list3 = dictionary["nodes"] as List<object>;
			for (int j = 0; j < list3.Count; j++)
			{
				Dictionary<string, object> dictionary2 = list3[j] as Dictionary<string, object>;
				int num4 = Convert.ToInt32(dictionary2["id"]);
				int num5 = Convert.ToInt32(dictionary2["levelNumber"]);
				int num6 = Convert.ToInt32(dictionary2["score"]);
				bool flag2 = false;
				if (dictionary2.ContainsKey("rented"))
				{
					flag2 = Convert.ToBoolean(dictionary2["rented"]);
				}
				string text3 = string.Empty;
				PsMinigameContext psMinigameContext = PsMinigameContext.Undefined;
				PsPathBlockType psPathBlockType = PsPathBlockType.Boss;
				GachaType gachaType = GachaType.SILVER;
				int num7 = 0;
				int num8 = 0;
				int num9 = 0;
				int num10 = 0;
				int num11 = 0;
				string text4 = string.Empty;
				string text5 = string.Empty;
				MinigameSearchParametres minigameSearchParametres = new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any);
				Hashtable hashtable = new Hashtable();
				bool flag3 = true;
				int num12 = 0;
				int num13 = 0;
				string text6 = string.Empty;
				string empty = string.Empty;
				bool flag4 = false;
				bool flag5 = false;
				string empty2 = string.Empty;
				string[] array = null;
				string text7 = string.Empty;
				PsGameMode psGameMode = PsGameMode.Any;
				int num14 = 1;
				bool flag6 = false;
				bool flag7 = false;
				string text8 = null;
				string text9 = null;
				int num15 = 0;
				int num16 = 0;
				bool flag8 = false;
				int num17 = 0;
				int num18 = 0;
				int num19 = 0;
				string text10 = string.Empty;
				double num20 = 0.0;
				int num21 = 0;
				if (dictionary2.ContainsKey("data"))
				{
					Dictionary<string, object> dictionary3 = dictionary2["data"] as Dictionary<string, object>;
					try
					{
						psMinigameContext = (PsMinigameContext)Enum.Parse(typeof(PsMinigameContext), (string)dictionary3["context"]);
					}
					catch
					{
					}
					text3 = (string)dictionary3["minigameId"];
					try
					{
						psGameMode = (PsGameMode)Enum.Parse(typeof(PsGameMode), (string)dictionary3["gameMode"]);
					}
					catch
					{
					}
					if (psMinigameContext == PsMinigameContext.Block)
					{
						if (dictionary3.ContainsKey("requiredStars"))
						{
							num7 = Convert.ToInt32(dictionary3["requiredStars"]);
						}
						psPathBlockType = (PsPathBlockType)Enum.Parse(typeof(PsPathBlockType), (string)dictionary3["blockType"]);
						if (dictionary3.ContainsKey("unlockedProgression"))
						{
							text4 = (string)dictionary3["unlockedProgression"];
						}
						if (dictionary3.ContainsKey("gachaType"))
						{
							gachaType = (GachaType)Enum.Parse(typeof(GachaType), (string)dictionary3["gachaType"]);
						}
						if (dictionary3.ContainsKey("willUnlockProgression"))
						{
							text5 = (string)dictionary3["willUnlockProgression"];
						}
						if (dictionary3.ContainsKey("blockBolts"))
						{
							num10 = Convert.ToInt32(dictionary3["blockBolts"]);
						}
						if (dictionary3.ContainsKey("blockCoins"))
						{
							num8 = Convert.ToInt32(dictionary3["blockCoins"]);
						}
						if (dictionary3.ContainsKey("blockDiamonds"))
						{
							num9 = Convert.ToInt32(dictionary3["blockDiamonds"]);
						}
						if (dictionary3.ContainsKey("blockKeys"))
						{
							num11 = Convert.ToInt32(dictionary3["blockKeys"]);
						}
						if (dictionary3.ContainsKey("blockRewardCue"))
						{
							text7 = (string)dictionary3["blockRewardCue"];
						}
						if (dictionary3.ContainsKey("tryCount"))
						{
							num17 = Convert.ToInt32(dictionary3["tryCount"]);
						}
						if (dictionary3.ContainsKey("sessionCount"))
						{
							num18 = Convert.ToInt32(dictionary3["sessionCount"]);
						}
						if (dictionary3.ContainsKey("reachedGoalCount"))
						{
							num19 = Convert.ToInt32(dictionary3["reachedGoalCount"]);
						}
						if (dictionary3.ContainsKey("firstTryDate"))
						{
							num20 = Convert.ToDouble(dictionary3["firstTryDate"]);
						}
						if (dictionary3.ContainsKey("powerFuels"))
						{
							text10 = (string)dictionary3["powerFuels"];
						}
					}
					else if (psMinigameContext == PsMinigameContext.Level)
					{
						if (dictionary3.ContainsKey("levelDifficulty"))
						{
							minigameSearchParametres.m_difficulty = (PsGameDifficulty)Enum.Parse(typeof(PsGameDifficulty), (string)dictionary3["levelDifficulty"]);
						}
						if (dictionary3.ContainsKey("levelFeatures"))
						{
							minigameSearchParametres.m_features = dictionary3["levelFeatures"] as string[];
						}
						if (dictionary3.ContainsKey("levelGameMode"))
						{
							minigameSearchParametres.m_gameMode = (PsGameMode)Enum.Parse(typeof(PsGameMode), (string)dictionary3["levelGameMode"]);
						}
						if (dictionary3.ContainsKey("levelItems"))
						{
							minigameSearchParametres.m_items = dictionary3["levelItems"] as string[];
						}
						if (dictionary3.ContainsKey("levelPlayerUnit"))
						{
							minigameSearchParametres.m_playerUnitFilter = (string)dictionary3["levelPlayerUnit"];
						}
						if (dictionary3.ContainsKey("heatNumber"))
						{
							num14 = Convert.ToInt32(dictionary3["heatNumber"]);
						}
						if (dictionary3.ContainsKey("trophyGhostWon"))
						{
							flag6 = Convert.ToBoolean(dictionary3["trophyGhostWon"]);
						}
						if (dictionary3.ContainsKey("practiceDisabled"))
						{
							flag7 = Convert.ToBoolean(dictionary3["practiceDisabled"]);
						}
						if (dictionary3.ContainsKey("secondarysWon"))
						{
							num15 = Convert.ToInt32(dictionary3["secondarysWon"]);
						}
						if (dictionary3.ContainsKey("trophyGhostId"))
						{
							text8 = Convert.ToString(dictionary3["trophyGhostId"]);
						}
						if (dictionary3.ContainsKey("raceGhostCount"))
						{
							num16 = Convert.ToInt32(dictionary3["raceGhostCount"]);
						}
						if (dictionary3.ContainsKey("trophiesRewarded"))
						{
							flag8 = Convert.ToBoolean(dictionary3["trophiesRewarded"]);
						}
						if (dictionary3.ContainsKey("fixedTrophies"))
						{
							text9 = Convert.ToString(dictionary3["fixedTrophies"]);
						}
						if (dictionary3.ContainsKey("purchasedRuns"))
						{
							num21 = Convert.ToInt32(dictionary3["purchasedRuns"]);
						}
						if (dictionary3.ContainsKey("levelMedalTimes"))
						{
							List<object> list4 = (List<object>)dictionary3["levelMedalTimes"];
							array = new string[3];
							for (int k = 0; k < list4.Count; k++)
							{
								array[k] = (string)list4[k];
							}
						}
					}
					else if (psMinigameContext == PsMinigameContext.Fresh)
					{
						if (dictionary3.ContainsKey("timeleft"))
						{
							num12 = Convert.ToInt32(dictionary3["timeleft"]);
						}
						if (dictionary3.ContainsKey("duration"))
						{
							num13 = Convert.ToInt32(dictionary3["duration"]);
						}
						if (dictionary3.ContainsKey("endTime"))
						{
							text6 = (string)dictionary3["endTime"];
						}
						if (dictionary3.ContainsKey("eventOver"))
						{
							flag4 = (bool)dictionary3["eventOver"];
						}
						if (dictionary3.ContainsKey("domeDestroyed"))
						{
							flag5 = (bool)dictionary3["domeDestroyed"];
						}
					}
					if (dictionary3.ContainsKey("dialogueIdentifiers"))
					{
						List<object> list5 = dictionary3["dialogueIdentifiers"] as List<object>;
						List<object> list6 = dictionary3["dialogueTriggers"] as List<object>;
						if (list5 != null)
						{
							for (int l = 0; l < list5.Count; l++)
							{
								hashtable.Add((string)list6[l], (string)list5[l]);
							}
						}
					}
					if (dictionary3.ContainsKey("unlocked"))
					{
						flag3 = (bool)dictionary3["unlocked"];
					}
				}
				PsGameLoop psGameLoop = null;
				if (psMinigameContext == PsMinigameContext.Level)
				{
					if (text2.Contains("Racing"))
					{
						psGameLoop = new PsGameLoopRacing(text3, minigameSearchParametres, psPlanetPath2, num4, num5, num6, flag3, num14, flag6, flag7, num15, text8, num16, flag8, array, text9, num21);
					}
					else
					{
						psGameLoop = new PsGameLoopAdventure(text3, minigameSearchParametres, psPlanetPath2, num4, num5, num6, flag3, array);
					}
				}
				else if (psMinigameContext == PsMinigameContext.Fresh)
				{
					psGameLoop = new PsGameLoopFresh(PsMinigameContext.Fresh, text3, psPlanetPath2, num4, num5, num6, num12, num13, text6, flag4, flag5);
				}
				else if (psMinigameContext == PsMinigameContext.Block)
				{
					if (psPathBlockType == PsPathBlockType.Boss)
					{
						psGameLoop = new PsGameLoopAdventureBattle(PsMinigameContext.Block, text3, null, psPlanetPath2, num4, num5, num6, flag3, text4, num18, num17, num19, text10, num20);
					}
					else
					{
						psGameLoop = new PsGameLoopBlockLevel(PsMinigameContext.Block, text3, psPathBlockType, gachaType, num7, text4, text5, psPlanetPath2, num4, num6, num8, num9, num10, num11, flag3, text7);
					}
				}
				if (psGameLoop != null)
				{
					psGameLoop.m_gameModeFromProgressionData = psGameMode;
					psGameLoop.m_rentedVehicle = flag2;
					if (hashtable.Count > 0)
					{
						psGameLoop.m_dialogues = hashtable;
					}
					psPlanetPath2.m_nodeInfos.Add(psGameLoop);
				}
			}
		}
		if (_local)
		{
			for (int m = 0; m < list2.Count; m++)
			{
				if (list2[m].GetPathType() == PsPlanetPathType.side && list2[m].m_startNodeId > 0)
				{
					psPlanetPath.GetNodeInfoFromPartialPath(list2[m].m_startNodeId).m_sidePath = list2[m];
				}
			}
		}
		return list;
	}

	// Token: 0x06001CF1 RID: 7409 RVA: 0x00149F74 File Offset: 0x00148374
	public static Hashtable GenerateProgressionFloatingPathJson(PsPlanetPath _floatingPath)
	{
		int num = 0;
		for (int i = 0; i < _floatingPath.m_nodeInfos.Count; i++)
		{
			if (_floatingPath.m_nodeInfos[i].m_context == PsMinigameContext.News)
			{
				num++;
			}
			else
			{
				_floatingPath.m_nodeInfos[i].m_nodeId = i + 1 - num;
			}
		}
		List<PsPlanetPath> list = new List<PsPlanetPath>();
		list.Add(_floatingPath);
		return ClientTools.GenerateProgressionPathJson(null, -1, _floatingPath.m_planet, false, false, false, list);
	}

	// Token: 0x06001CF2 RID: 7410 RVA: 0x00149FF4 File Offset: 0x001483F4
	public static Hashtable GenerateProgressionPathJson(PsPlanetPath _path)
	{
		List<PsPlanetPath> list = new List<PsPlanetPath>();
		list.Add(_path);
		return ClientTools.GenerateProgressionPathJson(null, -1, _path.m_planet, false, false, false, list);
	}

	// Token: 0x06001CF3 RID: 7411 RVA: 0x0014A020 File Offset: 0x00148420
	public static Hashtable GenerateProgressionPathJson(PsGameLoop _mainPathNode, int _currentNode, bool _includeMainPath = true, bool _includeMainPathNodes = true, bool _includeSidePathNodes = true)
	{
		List<PsGameLoop> list = new List<PsGameLoop>();
		list.Add(_mainPathNode);
		return ClientTools.GenerateProgressionPathJson(list, _currentNode, _mainPathNode.m_path.m_planet, _includeMainPath, _includeMainPathNodes, _includeSidePathNodes, null);
	}

	// Token: 0x06001CF4 RID: 7412 RVA: 0x0014A054 File Offset: 0x00148454
	public static Hashtable GenerateProgressionPathJson(List<PsGameLoop> _mainPathNodes, int _currentNode, string _planetIdentifier, bool _includeMainPath = true, bool _includeMainPathNodes = true, bool _includeSidePathNodes = true, List<PsPlanetPath> _additionalPaths = null)
	{
		Debug.Log("Generating progression path JSON: " + _planetIdentifier, null);
		Hashtable hashtable = new Hashtable();
		List<Hashtable> list = new List<Hashtable>();
		hashtable.Add("paths", list);
		Hashtable hashtable2 = new Hashtable();
		List<Hashtable> list2 = new List<Hashtable>();
		if (_includeMainPath)
		{
			int currentNodeId = PlanetTools.m_planetProgressionInfos[_planetIdentifier].m_mainPath.m_currentNodeId;
			if (_currentNode < currentNodeId)
			{
				Debug.LogError(string.Concat(new object[] { "SETTING MAINPATH CURRENT NODE TO: ", _currentNode, ". THIS IS A BUG! (path currentnode is: ", currentNodeId, ")" }));
				_currentNode = currentNodeId;
			}
			list.Add(hashtable2);
			hashtable2.Add("name", "MainPath");
			hashtable2.Add("type", PsPlanetPathType.main);
			hashtable2.Add("planet", _planetIdentifier);
			hashtable2.Add("currentNode", _currentNode);
			if (_includeMainPathNodes)
			{
				hashtable2.Add("nodes", list2);
			}
		}
		if (_additionalPaths == null)
		{
			_additionalPaths = new List<PsPlanetPath>();
		}
		if (_mainPathNodes != null)
		{
			for (int i = 0; i < _mainPathNodes.Count; i++)
			{
				if (_mainPathNodes[i].m_sidePath != null && _includeSidePathNodes)
				{
					_additionalPaths.Add(_mainPathNodes[i].m_sidePath);
				}
				ClientTools.AddPathNode(_mainPathNodes[i], list2, _includeMainPathNodes, _planetIdentifier);
			}
		}
		for (int j = 0; j < _additionalPaths.Count; j++)
		{
			if (_additionalPaths[j] != null)
			{
				Hashtable hashtable3 = new Hashtable();
				list.Add(hashtable3);
				List<Hashtable> list3 = new List<Hashtable>();
				hashtable3.Add("nodes", list3);
				if (_additionalPaths[j].m_overwrite)
				{
					hashtable3.Add("overwrite", true);
				}
				hashtable3.Add("lane", _additionalPaths[j].m_lane);
				hashtable3.Add("name", _additionalPaths[j].m_name);
				hashtable3.Add("type", _additionalPaths[j].GetPathType());
				hashtable3.Add("planet", _additionalPaths[j].m_planet);
				hashtable3.Add("currentNode", _additionalPaths[j].m_currentNodeId);
				if (_additionalPaths[j].m_startNodeId > 0)
				{
					hashtable3.Add("startNode", _additionalPaths[j].m_startNodeId);
				}
				if (_additionalPaths[j].m_parentPath != null)
				{
					hashtable3.Add("parent", _additionalPaths[j].m_parentPath.m_name);
				}
				for (int k = 0; k < _additionalPaths[j].m_nodeInfos.Count; k++)
				{
					if (_additionalPaths[j].m_nodeInfos[k].m_context != PsMinigameContext.News)
					{
						ClientTools.AddPathNode(_additionalPaths[j].m_nodeInfos[k], list3, true, _planetIdentifier);
					}
				}
			}
		}
		return hashtable;
	}

	// Token: 0x06001CF5 RID: 7413 RVA: 0x0014A39C File Offset: 0x0014879C
	private static void AddPathNode(PsGameLoop _node, List<Hashtable> _pathNodes, bool _includeRootNode, string _planetIdentifier)
	{
		if (_includeRootNode)
		{
			Hashtable hashtable = new Hashtable();
			_pathNodes.Add(hashtable);
			hashtable.Add("id", _node.m_nodeId);
			hashtable.Add("levelNumber", _node.m_levelNumber);
			hashtable.Add("score", _node.m_scoreBest);
			hashtable.Add("rented", _node.m_rentedVehicle);
			Hashtable hashtable2 = new Hashtable();
			hashtable.Add("data", hashtable2);
			hashtable2.Add("context", _node.m_context);
			hashtable2.Add("minigameId", _node.m_minigameId);
			hashtable2.Add("unlocked", _node.m_unlocked);
			if (_node.m_gameModeFromProgressionData != PsGameMode.Any)
			{
				hashtable2.Add("gameMode", _node.m_gameModeFromProgressionData);
			}
			if (_node.m_context == PsMinigameContext.Block)
			{
				if (_node is PsGameLoopAdventureBattle)
				{
					PsGameLoopAdventureBattle psGameLoopAdventureBattle = _node as PsGameLoopAdventureBattle;
					hashtable2.Add("blockType", PsPathBlockType.Boss);
					hashtable2.Add("unlockedProgression", psGameLoopAdventureBattle.GetUnlockedProgressionName());
					hashtable2.Add("tryCount", psGameLoopAdventureBattle.m_tryCount);
					hashtable2.Add("sessionCount", psGameLoopAdventureBattle.m_sessionCount);
					hashtable2.Add("reachedGoalCount", psGameLoopAdventureBattle.m_reachedGoalCount);
					hashtable2.Add("firstTryDate", psGameLoopAdventureBattle.m_firstTryTimeStamp);
					hashtable2.Add("powerFuels", Json.Serialize(psGameLoopAdventureBattle.m_purchasedPowerFuels));
				}
				else
				{
					PsGameLoopBlockLevel psGameLoopBlockLevel = _node as PsGameLoopBlockLevel;
					hashtable2.Add("blockType", psGameLoopBlockLevel.m_blockType);
					hashtable2.Add("gachaType", psGameLoopBlockLevel.m_gachaType);
					hashtable2.Add("requiredStars", psGameLoopBlockLevel.m_requiredStars);
					hashtable2.Add("unlockedProgression", psGameLoopBlockLevel.m_unlockedProgression);
					hashtable2.Add("willUnlockProgression", psGameLoopBlockLevel.m_willUnlockProgression);
					hashtable2.Add("blockCoins", psGameLoopBlockLevel.m_blockCoins);
					hashtable2.Add("blockDiamonds", psGameLoopBlockLevel.m_blockDiamonds);
					hashtable2.Add("blockKeys", psGameLoopBlockLevel.m_blockKeys);
					if (!string.IsNullOrEmpty(psGameLoopBlockLevel.m_rewardCue))
					{
						hashtable2.Add("blockRewardCue", psGameLoopBlockLevel.m_rewardCue);
					}
				}
			}
			else if (_node.m_context == PsMinigameContext.Level)
			{
				hashtable2.Add("levelDifficulty", _node.m_minigameSearchParametres.m_difficulty);
				hashtable2.Add("levelFeatures", _node.m_minigameSearchParametres.m_features);
				hashtable2.Add("levelGameMode", _node.m_minigameSearchParametres.m_gameMode);
				hashtable2.Add("levelItems", _node.m_minigameSearchParametres.m_items);
				hashtable2.Add("levelPlayerUnit", _node.m_minigameSearchParametres.m_playerUnitFilter);
				if (_planetIdentifier.Contains("Racing"))
				{
					PsGameLoopRacing psGameLoopRacing = _node as PsGameLoopRacing;
					hashtable2.Add("heatNumber", psGameLoopRacing.m_heatNumber);
					hashtable2.Add("trophyGhostWon", psGameLoopRacing.m_ghostWon);
					hashtable2.Add("practiceDisabled", psGameLoopRacing.m_practiceDisabled);
					hashtable2.Add("secondarysWon", psGameLoopRacing.m_secondarysWon);
					hashtable2.Add("trophyGhostId", psGameLoopRacing.m_trophyGhostIds);
					hashtable2.Add("fixedTrophies", psGameLoopRacing.m_fixedTrophies);
					hashtable2.Add("raceGhostCount", psGameLoopRacing.m_raceGhostCount);
					hashtable2.Add("trophiesRewarded", psGameLoopRacing.m_trophiesRewarded);
					if (psGameLoopRacing.m_purchasedRuns != 0)
					{
						hashtable2.Add("purchasedRuns", psGameLoopRacing.m_purchasedRuns);
					}
				}
				if (_node.m_forcedMedalTimes != null && _node.m_forcedMedalTimes.Length > 0 && !string.IsNullOrEmpty(_node.m_forcedMedalTimes[0]))
				{
					hashtable2.Add("levelMedalTimes", _node.m_forcedMedalTimes);
				}
			}
			else if (_node.m_context == PsMinigameContext.Fresh)
			{
				PsGameLoopFresh psGameLoopFresh = _node as PsGameLoopFresh;
				hashtable2.Add("duration", psGameLoopFresh.m_eventDuration);
				hashtable2.Add("timeleft", psGameLoopFresh.m_eventTimeLeft);
				hashtable2.Add("endTime", psGameLoopFresh.m_endTime.ToString("O"));
				hashtable2.Add("eventOver", psGameLoopFresh.m_eventOver);
				hashtable2.Add("domeDestroyed", psGameLoopFresh.m_domeDestroyed);
			}
			if (_node.m_dialogues != null && _node.m_dialogues.Count > 0)
			{
				string[] array = new string[_node.m_dialogues.Count];
				string[] array2 = new string[_node.m_dialogues.Count];
				int num = 0;
				IDictionaryEnumerator enumerator = _node.m_dialogues.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						array2[num] = (string)dictionaryEntry.Key;
						array[num] = (string)dictionaryEntry.Value;
						num++;
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
				hashtable2.Add("dialogueIdentifiers", array);
				hashtable2.Add("dialogueTriggers", array2);
			}
		}
	}

	// Token: 0x06001CF6 RID: 7414 RVA: 0x0014A920 File Offset: 0x00148D20
	public static string GetResourceHash(Hashtable _resources)
	{
		string text = string.Empty;
		if (_resources.ContainsKey("coins"))
		{
			text += _resources["coins"].ToString();
		}
		if (_resources.ContainsKey("diamonds"))
		{
			text += _resources["diamonds"].ToString();
		}
		if (_resources.ContainsKey("keys"))
		{
			text += _resources["keys"].ToString();
		}
		if (_resources.ContainsKey("maxKeys"))
		{
			text += _resources["maxKeys"].ToString();
		}
		if (_resources.ContainsKey("level"))
		{
			text += _resources["level"].ToString();
		}
		if (_resources.ContainsKey("bolts"))
		{
			text += _resources["bolts"].ToString();
		}
		if (_resources.ContainsKey("stars"))
		{
			text += _resources["stars"].ToString();
		}
		if (_resources.ContainsKey("itemLevel"))
		{
			text += _resources["itemLevel"].ToString();
		}
		if (_resources.ContainsKey("cups"))
		{
			text += _resources["cups"].ToString();
		}
		if (_resources.ContainsKey("mcRank"))
		{
			text += _resources["mcRank"].ToString();
		}
		if (_resources.ContainsKey("carRank"))
		{
			text += _resources["carRank"].ToString();
		}
		if (_resources.ContainsKey("tokens"))
		{
			text += _resources["tokens"].ToString();
		}
		if (_resources.ContainsKey("mcTrophies"))
		{
			text += _resources["mcTrophies"].ToString();
		}
		if (_resources.ContainsKey("carTrophies"))
		{
			text += _resources["carTrophies"].ToString();
		}
		if (_resources.ContainsKey("xp"))
		{
			text += _resources["xp"].ToString();
		}
		return ClientTools.GenerateHash(text);
	}

	// Token: 0x06001CF7 RID: 7415 RVA: 0x0014AB82 File Offset: 0x00148F82
	public static string GenerateJson(Hashtable _table)
	{
		return Json.Serialize(_table);
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x0014AB8A File Offset: 0x00148F8A
	public static string GeneratePlayerSetDataJson(Hashtable _setData, ref List<string> _tags)
	{
		return Json.Serialize(ClientTools.GeneratePlayerSetData(_setData, ref _tags));
	}

	// Token: 0x06001CF9 RID: 7417 RVA: 0x0014AB98 File Offset: 0x00148F98
	public static Hashtable GeneratePlayerSetData(Hashtable _setData, ref List<string> _tags)
	{
		Hashtable hashtable;
		if (_setData.ContainsKey("Resources"))
		{
			hashtable = (Hashtable)_setData["Resources"];
			if (!hashtable.ContainsKey("coins"))
			{
				hashtable.Add("coins", PsMetagameManager.m_playerStats.coins);
			}
			else
			{
				hashtable["coins"] = PsMetagameManager.m_playerStats.coins;
			}
			if (!hashtable.ContainsKey("copper"))
			{
				hashtable.Add("copper", PsMetagameManager.m_playerStats.copper);
			}
			else
			{
				hashtable["copper"] = PsMetagameManager.m_playerStats.copper;
			}
			if (!hashtable.ContainsKey("diamonds"))
			{
				hashtable.Add("diamonds", PsMetagameManager.m_playerStats.diamonds);
			}
			else
			{
				hashtable["diamonds"] = PsMetagameManager.m_playerStats.diamonds;
			}
			if (!hashtable.ContainsKey("shards"))
			{
				hashtable.Add("shards", PsMetagameManager.m_playerStats.shards);
			}
			else
			{
				hashtable["shards"] = PsMetagameManager.m_playerStats.shards;
			}
			if (!hashtable.ContainsKey("stars"))
			{
				hashtable.Add("stars", PsMetagameManager.m_playerStats.stars);
			}
			else
			{
				hashtable["stars"] = PsMetagameManager.m_playerStats.stars;
			}
			if (!hashtable.ContainsKey("level"))
			{
				hashtable.Add("level", PsMetagameManager.m_playerStats.level);
			}
			else
			{
				hashtable["level"] = PsMetagameManager.m_playerStats.level;
			}
			if (!hashtable.ContainsKey("mcBoosters"))
			{
				hashtable.Add("mcBoosters", PsMetagameManager.m_playerStats.mcBoosters);
			}
			else
			{
				hashtable["mcBoosters"] = PsMetagameManager.m_playerStats.mcBoosters;
			}
			if (!hashtable.ContainsKey("carBoosters"))
			{
				hashtable.Add("carBoosters", PsMetagameManager.m_playerStats.carBoosters);
			}
			else
			{
				hashtable["carBoosters"] = PsMetagameManager.m_playerStats.carBoosters;
			}
			if (!hashtable.ContainsKey("tournamentBoosters"))
			{
				hashtable.Add("tournamentBoosters", PsMetagameManager.m_playerStats.tournamentBoosters);
			}
			else
			{
				hashtable["tournamentBoosters"] = PsMetagameManager.m_playerStats.tournamentBoosters;
			}
			if (!hashtable.ContainsKey("cups"))
			{
				hashtable.Add("cups", PsMetagameManager.m_playerStats.cups);
			}
			else
			{
				hashtable["cups"] = PsMetagameManager.m_playerStats.cups;
			}
			if (!hashtable.ContainsKey("mcRank"))
			{
				hashtable.Add("mcRank", PsMetagameManager.m_playerStats.mcRank);
			}
			else
			{
				hashtable["mcRank"] = PsMetagameManager.m_playerStats.mcRank;
			}
			if (!hashtable.ContainsKey("carRank"))
			{
				hashtable.Add("carRank", PsMetagameManager.m_playerStats.carRank);
			}
			else
			{
				hashtable["carRank"] = PsMetagameManager.m_playerStats.carRank;
			}
			if (!hashtable.ContainsKey("mcTrophies"))
			{
				hashtable.Add("mcTrophies", PsMetagameManager.m_playerStats.mcTrophies);
			}
			else
			{
				hashtable["mcTrophies"] = PsMetagameManager.m_playerStats.mcTrophies;
			}
			if (!hashtable.ContainsKey("carTrophies"))
			{
				hashtable.Add("carTrophies", PsMetagameManager.m_playerStats.carTrophies);
			}
			else
			{
				hashtable["carTrophies"] = PsMetagameManager.m_playerStats.carTrophies;
			}
			if (!hashtable.ContainsKey("cardPurchases"))
			{
				hashtable.Add("cardPurchases", PsMetagameManager.GetCardPurchaseString());
			}
			else
			{
				hashtable["cardPurchases"] = PsMetagameManager.GetCardPurchaseString();
			}
			if (!hashtable.ContainsKey("gachaData"))
			{
				hashtable.Add("gachaData", PsMetagameManager.m_playerStats.gachaData);
			}
			else
			{
				hashtable["gachaData"] = PsMetagameManager.m_playerStats.gachaData;
			}
			if (!hashtable.ContainsKey("upgrades"))
			{
				hashtable.Add("upgrades", PsMetagameManager.m_playerStats.upgrades);
			}
			else
			{
				hashtable["upgrades"] = PsMetagameManager.m_playerStats.upgrades;
			}
			if (hashtable.ContainsKey("itemLevel"))
			{
				hashtable["itemLevel"] = PsMetagameManager.m_playerStats.itemLevel;
			}
			if (!hashtable.ContainsKey("xp"))
			{
				hashtable.Add("xp", PsMetagameManager.m_playerStats.xp);
			}
			else
			{
				hashtable["xp"] = PsMetagameManager.m_playerStats.xp;
			}
			if (!hashtable.ContainsKey("mcHandicap"))
			{
				hashtable.Add("mcHandicap", PsMetagameManager.m_playerStats.mcHandicap);
			}
			else
			{
				hashtable["mcHandicap"] = PsMetagameManager.m_playerStats.mcHandicap;
			}
			if (!hashtable.ContainsKey("carHandicap"))
			{
				hashtable.Add("carHandicap", PsMetagameManager.m_playerStats.carHandicap);
			}
			else
			{
				hashtable["carHandicap"] = PsMetagameManager.m_playerStats.carHandicap;
			}
			if (hashtable.ContainsKey("superLikeRefresh"))
			{
				_tags.Add("SuperLikeRefresh");
				hashtable["superLikeRefresh"] = PsMetagameManager.m_playerStats.SuperLikeSecondsLeft();
			}
			else
			{
				_tags.Add("SuperLikeRefresh");
				hashtable.Add("superLikeRefresh", PsMetagameManager.m_playerStats.SuperLikeSecondsLeft());
			}
			_setData["Resources"] = hashtable;
		}
		else
		{
			hashtable = new Hashtable();
			hashtable.Add("coins", PsMetagameManager.m_playerStats.coins);
			hashtable.Add("copper", PsMetagameManager.m_playerStats.copper);
			hashtable.Add("diamonds", PsMetagameManager.m_playerStats.diamonds);
			hashtable.Add("shards", PsMetagameManager.m_playerStats.shards);
			hashtable.Add("stars", PsMetagameManager.m_playerStats.stars);
			hashtable.Add("level", PsMetagameManager.m_playerStats.level);
			hashtable.Add("mcBoosters", PsMetagameManager.m_playerStats.mcBoosters);
			hashtable.Add("carBoosters", PsMetagameManager.m_playerStats.carBoosters);
			hashtable.Add("tournamentBoosters", PsMetagameManager.m_playerStats.tournamentBoosters);
			hashtable.Add("upgrades", PsMetagameManager.m_playerStats.upgrades);
			hashtable.Add("itemLevel", PsMetagameManager.m_playerStats.itemLevel);
			hashtable.Add("cups", PsMetagameManager.m_playerStats.cups);
			hashtable.Add("mcRank", PsMetagameManager.m_playerStats.mcRank);
			hashtable.Add("mcTrophies", PsMetagameManager.m_playerStats.mcTrophies);
			hashtable.Add("carRank", PsMetagameManager.m_playerStats.carRank);
			hashtable.Add("carTrophies", PsMetagameManager.m_playerStats.carTrophies);
			hashtable.Add("cardPurchases", PsMetagameManager.GetCardPurchaseString());
			hashtable.Add("gachaData", PsMetagameManager.m_playerStats.gachaData);
			hashtable.Add("xp", PsMetagameManager.m_playerStats.xp);
			hashtable.Add("mcHandicap", PsMetagameManager.m_playerStats.mcHandicap);
			hashtable.Add("carHandicap", PsMetagameManager.m_playerStats.carHandicap);
			hashtable.Add("superLikeRefresh", PsMetagameManager.m_playerStats.SuperLikeSecondsLeft());
			_setData.Add("Resources", hashtable);
		}
		if (PsMetagameManager.m_ratingStatusChanged)
		{
			_setData.Add("ratingStatus", PlayerPrefsX.GetRatingStatus());
			PsMetagameManager.m_ratingStatusChanged = false;
		}
		_setData["completedSurvey"] = PsMetagameManager.m_playerStats.completedSurvey;
		_setData["ageGroup"] = PsMetagameManager.m_playerStats.ageGroup;
		_setData["gender"] = PsMetagameManager.m_playerStats.gender;
		_tags.Add("SetData");
		if (_setData.ContainsKey("hash"))
		{
			_setData["hash"] = ClientTools.GetResourceHash(hashtable);
		}
		else
		{
			_setData.Add("hash", ClientTools.GetResourceHash(hashtable));
		}
		if (PsState.m_activeMinigame != null)
		{
			PsState.m_activeMinigame.m_collectedCopper = 0;
			PsState.m_activeMinigame.m_collectedCoins = 0;
			PsState.m_activeMinigame.m_collectedDiamonds = 0;
			PsState.m_activeMinigame.m_collectedShards = 0;
			PsState.m_activeMinigame.m_usedBoosters = 0;
			PsMetagameManager.m_playerStats.copperReset = false;
			PsMetagameManager.m_playerStats.shardReset = false;
		}
		return _setData;
	}

	// Token: 0x06001CFA RID: 7418 RVA: 0x0014B548 File Offset: 0x00149948
	public static HttpC ParsePathSync(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("lastPathSync"))
		{
			PlayerPrefsX.SetLastSync((string)dictionary["lastPathSync"]);
		}
		return _c;
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x0014B58C File Offset: 0x0014998C
	public static RewardData ParseRewardData(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		RewardData rewardData = new RewardData();
		if (dictionary.ContainsKey("reward"))
		{
			Dictionary<string, object> dictionary2 = dictionary["reward"] as Dictionary<string, object>;
			if (dictionary2.ContainsKey("rewardCoins"))
			{
				rewardData.m_coins = Convert.ToInt32(dictionary2["rewardCoins"]);
			}
			if (dictionary2.ContainsKey("rewardDiamonds"))
			{
				rewardData.m_diamonds = Convert.ToInt32(dictionary2["rewardDiamonds"]);
			}
			PsMetagameManager.m_playerStats.updated = true;
		}
		return rewardData;
	}

	// Token: 0x06001CFC RID: 7420 RVA: 0x0014B62C File Offset: 0x00149A2C
	public static NotificationData[] ParseNotifications(Dictionary<string, object> _dictionary)
	{
		List<object> list = _dictionary["data"] as List<object>;
		NotificationData[] array = new NotificationData[list.Count];
		for (int i = 0; i < list.Count; i++)
		{
			Dictionary<string, object> dictionary = list[i] as Dictionary<string, object>;
			array[i].playerId = (string)dictionary["playerId"];
			array[i].message = (string)dictionary["message"];
			array[i].read = (bool)dictionary["read"];
			array[i].type = (string)dictionary["type"];
			if (dictionary.ContainsKey("data"))
			{
				array[i].data = (string)dictionary["data"];
			}
		}
		return array;
	}

	// Token: 0x06001CFD RID: 7421 RVA: 0x0014B718 File Offset: 0x00149B18
	public static Hashtable CreateMinigameMetaDataHashtable(PsMinigameMetaData _minigameMetaData, bool _playerIsCreator = false)
	{
		Hashtable hashtable = new Hashtable();
		if (_playerIsCreator)
		{
			if (!string.IsNullOrEmpty(_minigameMetaData.id))
			{
				hashtable.Add("id", _minigameMetaData.id);
			}
			hashtable.Add("creatorName", PlayerPrefsX.GetUserName());
			hashtable.Add("creatorId", PlayerPrefsX.GetUserId());
			if (!string.IsNullOrEmpty(PlayerPrefsX.GetFacebookId()))
			{
				hashtable.Add("creatorFacebookId", PlayerPrefsX.GetFacebookId());
			}
			if (!string.IsNullOrEmpty(PlayerPrefsX.GetGameCenterId()))
			{
				hashtable.Add("creatorGameCenterId", PlayerPrefsX.GetGameCenterId());
			}
		}
		else
		{
			hashtable.Add("id", _minigameMetaData.id);
			hashtable.Add("creatorName", _minigameMetaData.creatorName);
			hashtable.Add("creatorId", _minigameMetaData.creatorId);
			hashtable.Add("creatorFacebookId", _minigameMetaData.creatorFacebookId);
			hashtable.Add("creatorGameCenterId", _minigameMetaData.creatorGameCenterId);
		}
		hashtable.Add("editSessionCount", _minigameMetaData.editSessionCount);
		hashtable.Add("groundsModificationCount", _minigameMetaData.groundsModificationCount);
		hashtable.Add("itemsModificationCount", _minigameMetaData.itemsModificationCount);
		hashtable.Add("lastPlaySessionStartCount", _minigameMetaData.lastPlaySessionStartCount);
		hashtable.Add("timeSpentInEditMode", _minigameMetaData.timeSpentInEditMode);
		hashtable.Add("name", _minigameMetaData.name);
		hashtable.Add("description", _minigameMetaData.description);
		hashtable.Add("clientVersion", Main.CLIENT_VERSION());
		hashtable.Add("gameMode", _minigameMetaData.gameMode);
		hashtable.Add("complexity", _minigameMetaData.complexity);
		hashtable.Add("playerUnit", _minigameMetaData.playerUnit);
		hashtable.Add("itemsUsed", _minigameMetaData.itemsUsed);
		hashtable.Add("itemsCount", ClientTools.ParseDictionaryObscuredIntToInt(_minigameMetaData.itemsCount));
		hashtable.Add("levelRequirement", _minigameMetaData.levelRequirement);
		hashtable.Add("timeSpentEditing", _minigameMetaData.timeSpentEditing);
		hashtable.Add("creatorUpgrades", _minigameMetaData.creatorUpgrades);
		hashtable.Add("overrideCC", _minigameMetaData.overrideCC);
		return hashtable;
	}

	// Token: 0x06001CFE RID: 7422 RVA: 0x0014B968 File Offset: 0x00149D68
	public static DataBlob CreateLevelDataBlob(byte[] _screenshotData, string _json)
	{
		MemoryStream memoryStream = LevelSerializer.SerializeLevelToStream(PsState.m_activeMinigame);
		MemoryStream memoryStream2 = FilePacker.ZipStream(memoryStream, 5);
		string text = ClientTools.ActiveMiniGameJson();
		Hashtable hashtable = new Hashtable();
		byte[] array;
		if (_screenshotData != null && _json != null)
		{
			array = FilePacker.CombineByteArrays(new byte[][]
			{
				FilePacker.StringToByteArray(text),
				memoryStream2.ToArray(),
				_screenshotData,
				FilePacker.StringToByteArray(_json)
			}, hashtable);
		}
		else if (_screenshotData != null)
		{
			array = FilePacker.CombineByteArrays(new byte[][]
			{
				FilePacker.StringToByteArray(text),
				memoryStream2.ToArray(),
				_screenshotData
			}, hashtable);
		}
		else if (_json != null)
		{
			array = FilePacker.CombineByteArrays(new byte[][]
			{
				FilePacker.StringToByteArray(text),
				memoryStream2.ToArray(),
				FilePacker.StringToByteArray(_json)
			}, hashtable);
		}
		else
		{
			array = FilePacker.CombineByteArrays(new byte[][]
			{
				FilePacker.StringToByteArray(text),
				memoryStream2.ToArray()
			}, hashtable);
		}
		return new DataBlob
		{
			header = hashtable,
			data = array
		};
	}

	// Token: 0x06001CFF RID: 7423 RVA: 0x0014BA74 File Offset: 0x00149E74
	public static string ActiveMiniGameJson()
	{
		Hashtable hashtable = ClientTools.CreateMinigameMetaDataHashtable(PsState.m_activeGameLoop.m_minigameMetaData, true);
		string text = Json.Serialize(hashtable);
		Debug.Log(text, null);
		return text;
	}

	// Token: 0x06001D00 RID: 7424 RVA: 0x0014BAA4 File Offset: 0x00149EA4
	public static DataBlob CreateDataBlob(string _json1, string _json2)
	{
		byte[] array = FilePacker.StringToByteArray(_json1);
		byte[] array2 = FilePacker.StringToByteArray(_json2);
		return ClientTools.GenerateDataBlob(new byte[][] { array, array2 });
	}

	// Token: 0x06001D01 RID: 7425 RVA: 0x0014BAD4 File Offset: 0x00149ED4
	public static DataBlob CreateDataBlob(string _json)
	{
		byte[] array = FilePacker.StringToByteArray(_json);
		return ClientTools.GenerateDataBlob(new byte[][] { array });
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x0014BAF8 File Offset: 0x00149EF8
	public static DataBlob CreateDataBlob(string _json, DataBlob _data)
	{
		byte[] array = FilePacker.StringToByteArray(_json);
		return ClientTools.GenerateDataBlob(new byte[][] { array, _data.data });
	}

	// Token: 0x06001D03 RID: 7427 RVA: 0x0014BB28 File Offset: 0x00149F28
	public static DataBlob CreatePublishLevelDataBlob(byte[] _screenshotData, string _json, DataBlob _ghostData)
	{
		MemoryStream memoryStream = FilePacker.ZipStream(LevelSerializer.SerializeLevelToStream(PsState.m_activeMinigame), 5);
		Hashtable hashtable = ClientTools.CreateMinigameMetaDataHashtable(PsState.m_activeGameLoop.m_minigameMetaData, true);
		hashtable["bestTime"] = PsState.m_activeMinigame.m_lastSentTimeScore;
		hashtable["median"] = PsState.m_activeMinigame.m_lastSentTimeScore;
		string text = Json.Serialize(hashtable);
		Debug.Log(text, null);
		if (!string.IsNullOrEmpty(_json))
		{
			return ClientTools.GenerateDataBlob(new byte[][]
			{
				FilePacker.StringToByteArray(text),
				memoryStream.ToArray(),
				_screenshotData,
				_ghostData.data,
				FilePacker.StringToByteArray(_json)
			});
		}
		return ClientTools.GenerateDataBlob(new byte[][]
		{
			FilePacker.StringToByteArray(text),
			memoryStream.ToArray(),
			_screenshotData,
			_ghostData.data
		});
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x0014BC04 File Offset: 0x0014A004
	public static DataBlob GenerateDataBlob(byte[][] byteArray)
	{
		Hashtable hashtable = new Hashtable();
		byte[] array = FilePacker.CombineByteArrays(byteArray, hashtable);
		return new DataBlob
		{
			header = hashtable,
			data = array
		};
	}

	// Token: 0x06001D05 RID: 7429 RVA: 0x0014BC3C File Offset: 0x0014A03C
	public static byte[] CreateLevelZipWithoutMetadataAndScreenshot()
	{
		MemoryStream memoryStream = LevelSerializer.SerializeLevelToStream(PsState.m_activeMinigame);
		MemoryStream memoryStream2 = FilePacker.ZipStream(memoryStream, 5);
		return memoryStream2.ToArray();
	}

	// Token: 0x06001D06 RID: 7430 RVA: 0x0014BC64 File Offset: 0x0014A064
	public static DataBlob CreatePathDataBlob(string _itemLevelJson, string _pathJson)
	{
		Hashtable hashtable = new Hashtable();
		byte[] array = FilePacker.ZipBytes(Encoding.UTF8.GetBytes(_pathJson), 3);
		byte[] array2 = FilePacker.CombineByteArrays(new byte[][]
		{
			FilePacker.StringToByteArray(_itemLevelJson),
			array
		}, hashtable);
		return new DataBlob
		{
			header = hashtable,
			data = array2
		};
	}

	// Token: 0x06001D07 RID: 7431 RVA: 0x0014BCC0 File Offset: 0x0014A0C0
	public static DataBlob CreateGhostDataBlob(Ghost _ghost)
	{
		MemoryStream memoryStream = Ghost.SerializeToStream(_ghost);
		MemoryStream memoryStream2 = FilePacker.ZipStream(memoryStream, 5);
		Debug.Log("ZIPPED GHOST SIZE: " + memoryStream2.Length, null);
		return new DataBlob
		{
			header = null,
			data = memoryStream2.ToArray()
		};
	}

	// Token: 0x06001D08 RID: 7432 RVA: 0x0014BD18 File Offset: 0x0014A118
	public static string GenerateUserJSON(Hashtable _ht = null)
	{
		Hashtable hashtable = _ht;
		if (hashtable == null)
		{
			hashtable = new Hashtable();
		}
		if (!string.IsNullOrEmpty(PlayerPrefsX.GetUserId()))
		{
			hashtable.Add("id", PlayerPrefsX.GetUserId());
		}
		if (!string.IsNullOrEmpty(PlayerPrefsX.GetUserName()))
		{
			hashtable.Add("name", PlayerPrefsX.GetUserName());
		}
		else
		{
			hashtable.Add("name", PlayerPrefsX.GetUserName());
		}
		if (PlayerPrefsX.GetPathVersion() != 0)
		{
			hashtable.Add("pathVersion", PlayerPrefsX.GetPathVersion());
		}
		hashtable.Add("clientVersion", Main.CLIENT_VERSION() + " " + Application.platform.ToString());
		hashtable.Add("locale", PsStrings.GetLanguage().ToString().ToUpper());
		hashtable.Add("acceptNotifications", PlayerPrefsX.GetAcceptNotifications());
		hashtable.Add("udid", Metrics.GetMetricsGUID());
		string text = Json.Serialize(hashtable);
		Debug.Log("USER JSON GENERATED: " + text, null);
		return text;
	}

	// Token: 0x06001D09 RID: 7433 RVA: 0x0014BE38 File Offset: 0x0014A238
	public static string GenerateIdListJson(List<PsMinigameMetaData> _games)
	{
		Hashtable hashtable = new Hashtable();
		string[] array = new string[_games.Count];
		for (int i = 0; i < _games.Count; i++)
		{
			array[i] = _games[i].id;
		}
		hashtable.Add("games", array);
		string text = Json.Serialize(hashtable);
		Debug.Log("Id list json generated: " + text, null);
		return text;
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x0014BEA4 File Offset: 0x0014A2A4
	public static Dictionary<string, object> LoadLocalPathData()
	{
		string text = Main.GetPersistentDataPath() + "/LocalInitialData.txt";
		if (!File.Exists(text))
		{
			Debug.LogWarning("NO LOCAL INITIAL PATH FILE: " + text);
			return null;
		}
		Debug.Log("LOADING LOCAL INITIAL PATH FILE: " + text, null);
		StreamReader streamReader = File.OpenText(text);
		string text2 = string.Empty;
		for (string text3 = streamReader.ReadLine(); text3 != null; text3 = streamReader.ReadLine())
		{
			text2 += text3;
		}
		if (string.IsNullOrEmpty(text2))
		{
			Debug.LogWarning("LOCAL INITIAL PATH FILE EXISTS, BUT WAS EMPTY: " + text);
			return null;
		}
		Debug.Log(string.Concat(new object[] { "DEVICES LOCAL PATH DATA (length:", text2.Length, "): ", text2 }), null);
		return Json.Deserialize(text2) as Dictionary<string, object>;
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x0014BF7C File Offset: 0x0014A37C
	public static void SaveFile(string _name, string _extension, byte[] _data)
	{
		string text = Application.persistentDataPath + "/" + _name;
		FileStream fileStream = File.Create(text);
		BinaryWriter binaryWriter = new BinaryWriter(fileStream, Encoding.UTF8);
		binaryWriter.Write(_data);
		binaryWriter.Close();
		File.Move(text, Path.ChangeExtension(text, _extension));
		fileStream.Close();
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x0014BFCD File Offset: 0x0014A3CD
	private static string GetSoundFileName(int _index)
	{
		if (_index != 0)
		{
			return "Master Bank";
		}
		return "FMOD_bank_list";
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x0014BFE5 File Offset: 0x0014A3E5
	private static string GetSoundFileExtension(int _index)
	{
		if (_index == 0)
		{
			return ".txt";
		}
		if (_index == 1)
		{
			return ".strings.bank";
		}
		if (_index != 2)
		{
			return ".txt";
		}
		return ".bank";
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x0014C018 File Offset: 0x0014A418
	public static RewardData ParseUnclaimedRewards(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		RewardData rewardData = new RewardData();
		if (dictionary.ContainsKey("coins"))
		{
			rewardData.m_coins = Convert.ToInt32(dictionary["coins"]);
		}
		return rewardData;
	}

	// Token: 0x06001D0F RID: 7439 RVA: 0x0014C063 File Offset: 0x0014A463
	public static T ParseEnum<T>(object obj)
	{
		return (T)((object)Enum.Parse(typeof(T), Convert.ToString(obj)));
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x0014C080 File Offset: 0x0014A480
	public static List<FollowRewardData> ParseFollowRewadData(Dictionary<string, object> _serverResponse)
	{
		List<FollowRewardData> list = new List<FollowRewardData>();
		if (_serverResponse.ContainsKey("followRewardUsers"))
		{
			List<Dictionary<string, object>> list2 = ClientTools.ParseList<Dictionary<string, object>>(_serverResponse["followRewardUsers"] as List<object>);
			if (list2 != null && list2.Count > 0)
			{
				for (int i = 0; i < list2.Count; i++)
				{
					FollowRewardData followRewardData = default(FollowRewardData);
					if (list2[i].ContainsKey("id"))
					{
						followRewardData.playerID = (string)list2[i]["id"];
					}
					if (list2[i].ContainsKey("facebookId"))
					{
						followRewardData.facebookID = (string)list2[i]["facebookId"];
					}
					if (list2[i].ContainsKey("followRewards"))
					{
						followRewardData.rewardIdentifiers = ClientTools.ParseList<string>(list2[i]["followRewards"] as List<object>);
					}
					list.Add(followRewardData);
				}
			}
		}
		return list;
	}
}
