using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace Server
{
	// Token: 0x0200045D RID: 1117
	public static class Trophy
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x0015A190 File Offset: 0x00158590
		public static HttpC GetGhostsByIds(string _gameId, string _commaSeparatedIds, string _playerUnit, Action<GhostData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/trophy/ghostsByIds");
			woeurl.AddParameter("ghostIds", _commaSeparatedIds);
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("playerUnit", _playerUnit);
			ResponseHandler<GhostData[]> responseHandler = new ResponseHandler<GhostData[]>(_okCallback, new Func<HttpC, GhostData[]>(ClientTools.ParseGhostDatas));
			return Request.Get(woeurl, "TROPHY_GHOST_GET_BY_IDS", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0015A20C File Offset: 0x0015860C
		public static HttpC GetGhostsByTime(string _gameId, string _commaSeparatedTimes, string _playerUnit, Action<GhostData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/trophy/ghostsByTime");
			woeurl.AddParameter("ghostTimes", _commaSeparatedTimes);
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("playerUnit", _playerUnit);
			ResponseHandler<GhostData[]> responseHandler = new ResponseHandler<GhostData[]>(_okCallback, new Func<HttpC, GhostData[]>(ClientTools.ParseGhostDatas));
			return Request.Get(woeurl, "TROPHY_GHOST_GET_BY_TIME", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x0015A288 File Offset: 0x00158688
		public static HttpC GetGhostsByTrophies(string _gameId, string _commaSeparatedTrophies, string _playerUnit, Action<GhostData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/trophy/ghostsByTrophies");
			woeurl.AddParameter("trophies", _commaSeparatedTrophies);
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("playerUnit", _playerUnit);
			ResponseHandler<GhostData[]> responseHandler = new ResponseHandler<GhostData[]>(_okCallback, new Func<HttpC, GhostData[]>(ClientTools.ParseGhostDatas));
			return Request.Get(woeurl, "TROPHY_GHOST_GET_BY_TROPHIES", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0015A304 File Offset: 0x00158704
		public static HttpC SendScore(RacingGhostData _data, OpponentData[] _opponents, bool _finished, bool _final, Hashtable _pathJson, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/trophy/score/send");
			Hashtable hashtable = new Hashtable();
			hashtable.Add("gameId", _data.m_gameId);
			hashtable.Add("time", _data.m_time);
			hashtable.Add("playerUnit", _data.m_playerUnit);
			hashtable.Add("version", 3);
			hashtable.Add("finished", _finished);
			hashtable.Add("finalGhost", _final);
			List<Hashtable> list = new List<Hashtable>();
			if (_opponents != null)
			{
				foreach (OpponentData opponentData in _opponents)
				{
					list.Add(opponentData.TojsonHashtable());
				}
			}
			hashtable.Add("opponents", list);
			List<string> list2 = new List<string>();
			Hashtable hashtable2 = ClientTools.GeneratePlayerSetData(new Hashtable(), ref list2);
			hashtable.Add("update", hashtable2);
			if (_pathJson != null)
			{
				hashtable.Add("progression", _pathJson);
			}
			List<Hashtable> updatedData = PsGachaManager.GetUpdatedData();
			if (updatedData != null)
			{
				hashtable.Add("chest", updatedData);
			}
			string text = Json.Serialize(hashtable);
			string[] array = list2.ToArray();
			DataBlob dataBlob = default(DataBlob);
			if (_data.m_ghostData.data != null)
			{
				dataBlob = ClientTools.CreateDataBlob(text, _data.m_ghostData);
			}
			else
			{
				dataBlob = ClientTools.CreateDataBlob(text);
			}
			return Request.Post(woeurl, "TROPHY_SCORE_SEND", dataBlob.data, _okCallback, _failureCallback, _errorCallback, dataBlob.header, array);
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x0015A48C File Offset: 0x0015888C
		public static HttpC Leaderboard(string _playerUnit, Action<Leaderboard> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/trophy/leaderboard");
			woeurl.AddParameter("playerUnit", _playerUnit);
			ResponseHandler<Leaderboard> responseHandler = new ResponseHandler<Leaderboard>(_okCallback, new Func<HttpC, Leaderboard>(ClientTools.ParseLeaderboards));
			return Request.Get(woeurl, "TROPHY_LEADERBOARD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x0015A4F0 File Offset: 0x001588F0
		public static HttpC LeaderboardByGame(string _gameId, Action<List<LeaderboardEntry>> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/trophy/leaderboardByGame");
			woeurl.AddParameter("gameId", _gameId);
			ResponseHandler<List<LeaderboardEntry>> responseHandler = new ResponseHandler<List<LeaderboardEntry>>(_okCallback, new Func<HttpC, List<LeaderboardEntry>>(ClientTools.ParseGameLeaderboard));
			return Request.Get(woeurl, "TROPHY_GAME_LEADERBOARD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x0015A554 File Offset: 0x00158954
		public static HttpC GetTopCreators(Action<CreatorLeaderboard> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/creator/top");
			Trophy.SetTopCreatorParams(PsUITabbedCreate.m_selectedSubTab, woeurl);
			ResponseHandler<CreatorLeaderboard> responseHandler = new ResponseHandler<CreatorLeaderboard>(_okCallback, new Func<HttpC, CreatorLeaderboard>(ClientTools.ParseCreatorLeaderboard));
			return Request.Get(woeurl, "TROPHY_CREATOR_LEADERBOARD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0015A5B4 File Offset: 0x001589B4
		private static void SetTopCreatorParams(int _subTabIndex, WOEURL url)
		{
			string text = null;
			object obj = null;
			if (_subTabIndex != 1)
			{
				if (_subTabIndex != 2)
				{
					if (_subTabIndex == 3)
					{
						text = "friends";
						obj = "true";
					}
				}
				else
				{
					text = "countryCode";
					obj = PlayerPrefsX.GetCountryCode();
				}
			}
			if (text != null && obj != null)
			{
				url.AddParameter(text, obj);
			}
		}
	}
}
