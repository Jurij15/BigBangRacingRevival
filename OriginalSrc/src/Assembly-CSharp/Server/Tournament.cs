using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;

namespace Server
{
	// Token: 0x02000454 RID: 1108
	public static class Tournament
	{
		// Token: 0x06001EAD RID: 7853 RVA: 0x001598CC File Offset: 0x00157CCC
		public static HttpC Join(Tournament.TournamentJoinData _data, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/join");
			woeurl.AddParameter("tournamentId", _data.tournamentId);
			List<string> list = new List<string>();
			list.Add("SetData");
			string text = Json.Serialize(_data.setData);
			return Request.Post(woeurl, "TOURNAMENT_JOIN", text, _okCallback, _failureCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0015992C File Offset: 0x00157D2C
		public static HttpC Claim(Hashtable _setData, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/claim");
			List<string> list = new List<string>();
			list.Add("SetData");
			string text = Json.Serialize(_setData);
			return Request.Post(woeurl, "TOURNAMENT_CLAIM", text, _okCallback, _failureCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00159974 File Offset: 0x00157D74
		public static HttpC ClaimYoutubeNitros(Hashtable _setData, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/claimYoutuberNitros");
			List<string> list = new List<string>();
			list.Add("SetData");
			string text = Json.Serialize(_setData);
			return Request.Post(woeurl, "TOURNAMENT_CLAIM_YOUTUBENITROS", text, _okCallback, _failureCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x001599BC File Offset: 0x00157DBC
		public static HttpC GetTournamentNitro(Tournament.GetTournamentNitroData _data, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/tournamentNitro");
			woeurl.AddParameter("packType", _data.nitroPack.ToString());
			List<string> list = new List<string>();
			list.Add("SetData");
			string text = Json.Serialize(_data.setData);
			return Request.Post(woeurl, "TOURNAMENT_CLAIM", text, _okCallback, _failureCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00159A24 File Offset: 0x00157E24
		public static HttpC SetSuperFuel(Hashtable _setData, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/setSuperFuel");
			string text = Json.Serialize((_setData != null) ? _setData : new Hashtable());
			WOEURL woeurl2 = woeurl;
			string text2 = "TOURNAMENT_SET_SUPER_FUEL";
			string text3 = text;
			string[] array = new string[] { "SetData" };
			return Request.Post(woeurl2, text2, text3, _okCallback, _failureCallback, _errorCallback, null, array, false);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00159A8C File Offset: 0x00157E8C
		public static HttpC SendScore(Tournament.TournamentSendScoreData _data, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/score/send");
			Hashtable hashtable = new Hashtable();
			hashtable.Add("gameId", _data.m_gameId);
			hashtable.Add("time", _data.m_time);
			hashtable.Add("playerUnit", _data.m_playerUnit);
			hashtable.Add("version", 3);
			List<string> list = new List<string>();
			list.Add("SetData");
			Hashtable hashtable2 = ClientTools.GeneratePlayerSetData(new Hashtable(), ref list);
			hashtable.Add("update", hashtable2);
			string text = Json.Serialize(hashtable);
			DataBlob dataBlob = default(DataBlob);
			if (_data.m_ghostData.data != null)
			{
				dataBlob = ClientTools.CreateDataBlob(text, _data.m_ghostData);
			}
			else
			{
				dataBlob = ClientTools.CreateDataBlob(text);
			}
			return Request.Post(woeurl, "TOURNAMENT_SCORE_SEND", dataBlob.data, _okCallback, _failureCallback, _errorCallback, dataBlob.header, list.ToArray());
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00159B7C File Offset: 0x00157F7C
		public static HttpC GetLeaderboard(Action<Tournament.TournamentLeaderboard> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null, string _tournamentId = null)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/score/get");
			if (!string.IsNullOrEmpty(_tournamentId))
			{
				woeurl.AddParameter("tournamentId", _tournamentId);
			}
			ResponseHandler<Tournament.TournamentLeaderboard> responseHandler = new ResponseHandler<Tournament.TournamentLeaderboard>(_okCallback, new Func<HttpC, Tournament.TournamentLeaderboard>(Tournament.ParseTournamentLeaderboard));
			return Request.Get(woeurl, "TOURNAMENT_SCORE_GET", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00159BE8 File Offset: 0x00157FE8
		public static HttpC HostChangeRoom(int _newRoom, Action<Tournament.TournamentLeaderboard> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/ownerRoomChange");
			woeurl.AddParameter("newRoom", _newRoom);
			ResponseHandler<Tournament.TournamentLeaderboard> responseHandler = new ResponseHandler<Tournament.TournamentLeaderboard>(_okCallback, new Func<HttpC, Tournament.TournamentLeaderboard>(Tournament.ParseTournamentLeaderboard));
			return Request.Get(woeurl, "TOURNAMENT_HOST_ROOMCHANGE", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, null);
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00159C50 File Offset: 0x00158050
		public static Tournament.TournamentLeaderboard ParseTournamentLeaderboard(HttpC _c)
		{
			Tournament.TournamentLeaderboard tournamentLeaderboard = new Tournament.TournamentLeaderboard();
			Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
			if (dictionary.ContainsKey("globalParticipants"))
			{
				tournamentLeaderboard.globalParticipants = Convert.ToInt32(dictionary["globalParticipants"]);
			}
			if (dictionary.ContainsKey("acceptingNewScores"))
			{
				tournamentLeaderboard.acceptingNewScores = Convert.ToBoolean(dictionary["acceptingNewScores"]);
			}
			if (dictionary.ContainsKey("globalNitroPot"))
			{
				tournamentLeaderboard.globalNitroPot = Convert.ToInt32(dictionary["globalNitroPot"]);
			}
			if (dictionary.ContainsKey("roomNitroPot"))
			{
				tournamentLeaderboard.roomNitroPot = Convert.ToInt32(dictionary["roomNitroPot"]);
			}
			if (dictionary.ContainsKey("ownerTime"))
			{
				tournamentLeaderboard.ownerTime = Convert.ToInt32(dictionary["ownerTime"]);
			}
			if (tournamentLeaderboard.ownerTime == 0)
			{
				tournamentLeaderboard.ownerTime = int.MaxValue;
			}
			if (dictionary.ContainsKey("room"))
			{
				tournamentLeaderboard.room = Convert.ToInt32(dictionary["room"]);
			}
			if (dictionary.ContainsKey("roomCount"))
			{
				tournamentLeaderboard.roomCount = Convert.ToInt32(dictionary["roomCount"]);
			}
			if (tournamentLeaderboard.roomCount < 1)
			{
				tournamentLeaderboard.roomCount = 1;
			}
			tournamentLeaderboard.m_leaderboard = HighScores.ParseDataEntriesJSON(dictionary);
			return tournamentLeaderboard;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00159DB4 File Offset: 0x001581B4
		public static HttpC GetGhosts(Action<GhostData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null, int _timeScore = 0)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/ghosts");
			if (_timeScore > 0 && _timeScore != 2147483647)
			{
				woeurl.AddParameter("time", _timeScore);
			}
			ResponseHandler<GhostData[]> responseHandler = new ResponseHandler<GhostData[]>(_okCallback, new Func<HttpC, GhostData[]>(ClientTools.ParseGhostDatas));
			return Request.Get(woeurl, "TOURNAMENT_GET_GHOSTS", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00159E2C File Offset: 0x0015822C
		public static HttpC GetGhostsByIds(string _playerIdsSeparatedByComma, Action<GhostData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/ghost");
			woeurl.AddParameter("playerIds", _playerIdsSeparatedByComma);
			ResponseHandler<GhostData[]> responseHandler = new ResponseHandler<GhostData[]>(_okCallback, new Func<HttpC, GhostData[]>(ClientTools.ParseGhostDatas));
			return Request.Get(woeurl, "TOURNAMENT_GET_GHOSTS", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00159E90 File Offset: 0x00158290
		public static HttpC GetLatestGhosts(Action<GhostData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/tournament/ghosts/latest");
			ResponseHandler<GhostData[]> responseHandler = new ResponseHandler<GhostData[]>(_okCallback, new Func<HttpC, GhostData[]>(ClientTools.ParseGhostDatas));
			return Request.Get(woeurl, "TOURNAMENT_GET_GHOSTS", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00159EE8 File Offset: 0x001582E8
		public static HttpC SaveComment(string _comment, Action<CommentData[]> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null)
		{
			Debug.LogError("SaveComment");
			ResponseHandler<CommentData[]> responseHandler = new ResponseHandler<CommentData[]>(_okCallback, new Func<HttpC, CommentData[]>(ClientTools.ParseComments));
			WOEURL woeurl = new WOEURL("/v1/tournament/comment/save");
			woeurl.AddParameter("comment", WWW.EscapeURL(_comment));
			return Request.Post(woeurl, null, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, null, new string[] { "Comment" });
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00159F64 File Offset: 0x00158364
		public static HttpC GetComments(Action<CommentData[]> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null, int _commentLimit = 50)
		{
			ResponseHandler<CommentData[]> responseHandler = new ResponseHandler<CommentData[]>(_okCallback, new Func<HttpC, CommentData[]>(ClientTools.ParseComments));
			WOEURL woeurl = new WOEURL("/v1/tournament/comment/get");
			woeurl.AddParameter("limit", _commentLimit);
			return Request.Get(woeurl, "GET_COMMENTS", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x02000455 RID: 1109
		public class TournamentJoinData
		{
			// Token: 0x040021FE RID: 8702
			public string tournamentId;

			// Token: 0x040021FF RID: 8703
			public Hashtable setData;
		}

		// Token: 0x02000456 RID: 1110
		public enum NitroPack
		{
			// Token: 0x04002201 RID: 8705
			pack1,
			// Token: 0x04002202 RID: 8706
			pack2,
			// Token: 0x04002203 RID: 8707
			adPack
		}

		// Token: 0x02000457 RID: 1111
		public class GetTournamentNitroData
		{
			// Token: 0x04002204 RID: 8708
			public Hashtable setData;

			// Token: 0x04002205 RID: 8709
			public Tournament.NitroPack nitroPack;
		}

		// Token: 0x02000458 RID: 1112
		public class TournamentSendScoreData
		{
			// Token: 0x06001EBD RID: 7869 RVA: 0x00159FDA File Offset: 0x001583DA
			public TournamentSendScoreData(string _gameId, int _time, string _playerUnit, DataBlob _ghostData)
			{
				this.m_gameId = _gameId;
				this.m_time = _time;
				this.m_playerUnit = _playerUnit;
				this.m_ghostData = _ghostData;
			}

			// Token: 0x04002206 RID: 8710
			public string m_playerUnit;

			// Token: 0x04002207 RID: 8711
			public string m_gameId;

			// Token: 0x04002208 RID: 8712
			public int m_time;

			// Token: 0x04002209 RID: 8713
			public DataBlob m_ghostData;
		}

		// Token: 0x02000459 RID: 1113
		public class TournamentLeaderboard
		{
			// Token: 0x0400220A RID: 8714
			public List<HighscoreDataEntry> m_leaderboard;

			// Token: 0x0400220B RID: 8715
			public int globalParticipants;

			// Token: 0x0400220C RID: 8716
			public bool acceptingNewScores = true;

			// Token: 0x0400220D RID: 8717
			public int roomNitroPot;

			// Token: 0x0400220E RID: 8718
			public int globalNitroPot;

			// Token: 0x0400220F RID: 8719
			public int roomCount;

			// Token: 0x04002210 RID: 8720
			public int ownerTime;

			// Token: 0x04002211 RID: 8721
			public int room;

			// Token: 0x04002212 RID: 8722
			public double timeStamp;
		}
	}
}
