using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;

namespace Server
{
	// Token: 0x0200044F RID: 1103
	public static class Team
	{
		// Token: 0x06001E8D RID: 7821 RVA: 0x001588AC File Offset: 0x00156CAC
		public static HttpC Get(string _teamId, bool _own, Action<TeamData> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/team/get");
			ResponseHandler<TeamData> responseHandler = new ResponseHandler<TeamData>(_okCallback, new Func<HttpC, TeamData>(ClientTools.ParseTeam));
			woeurl.AddParameter("id", _teamId);
			if (_own)
			{
				woeurl.AddParameter("own", _own.ToString().ToLower());
			}
			return Request.Get(woeurl, "TEAM_GET", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00158934 File Offset: 0x00156D34
		public static HttpC GetSuggestions(Action<TeamData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/team/suggest");
			ResponseHandler<TeamData[]> responseHandler = new ResponseHandler<TeamData[]>(_okCallback, new Func<HttpC, TeamData[]>(ClientTools.ParseTeamList));
			return Request.Get(woeurl, "TEAM_SUGGEST", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x0015898C File Offset: 0x00156D8C
		public static HttpC Search(string _query, Action<TeamData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/team/search");
			ResponseHandler<TeamData[]> responseHandler = new ResponseHandler<TeamData[]>(_okCallback, new Func<HttpC, TeamData[]>(ClientTools.ParseTeamList));
			woeurl.AddParameter("query", WWW.EscapeURL(_query));
			return Request.Get(woeurl, "TEAM_SEARCH", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x001589F4 File Offset: 0x00156DF4
		public static HttpC GetLeaderboard(Action<TeamData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/team/leaderboard");
			ResponseHandler<TeamData[]> responseHandler = new ResponseHandler<TeamData[]>(_okCallback, new Func<HttpC, TeamData[]>(ClientTools.ParseTeamList));
			return Request.Get(woeurl, "TEAM_LEADERBOARD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x00158A49 File Offset: 0x00156E49
		public static HttpC Create(TeamData _team, Action<TeamData> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			return Team.Update(_team, _okCallback, _failureCallback, _errorCallback);
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x00158A54 File Offset: 0x00156E54
		public static HttpC Update(TeamData _team, Action<TeamData> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/team/update");
			ResponseHandler<TeamData> responseHandler = new ResponseHandler<TeamData>(_okCallback, new Func<HttpC, TeamData>(ClientTools.ParseTeam));
			Hashtable hashtable = ClientTools.CreateTeamDataHashtable(_team);
			string text = Json.Serialize(hashtable);
			return Request.Post(woeurl, "TEAM_UPDATE", text, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, null, null, false);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x00158AC0 File Offset: 0x00156EC0
		public static HttpC Join(string _teamId, Action<TeamData> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/team/join");
			ResponseHandler<TeamData> responseHandler = new ResponseHandler<TeamData>(_okCallback, new Func<HttpC, TeamData>(ClientTools.ParseTeam));
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("id", _teamId);
			return Request.Post(woeurl, "TEAM_JOIN", Json.Serialize(dictionary), new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, null, null, false);
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00158B30 File Offset: 0x00156F30
		public static HttpC Leave(string _teamId, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/team/leave");
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("id", _teamId);
			return Request.Post(woeurl, "TEAM_LEAVE", Json.Serialize(dictionary), _okCallback, _failureCallback, _errorCallback, null, null, false);
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x00158B74 File Offset: 0x00156F74
		public static HttpC Kick(string _teamId, string _playerId, string _reason, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/team/kick");
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("teamId", _teamId);
			dictionary.Add("idToKick", _playerId);
			dictionary.Add("reason", _reason);
			return Request.Post(woeurl, "TEAM_KICK", Json.Serialize(dictionary), _okCallback, _failureCallback, _errorCallback, null, null, false);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00158BD0 File Offset: 0x00156FD0
		public static HttpC ClaimKick(string _reason, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/team/kick/claim");
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("reason", _reason);
			return Request.Post(woeurl, "TEAM_CLAIM_KICK", Json.Serialize(dictionary), _okCallback, _failureCallback, _errorCallback, null, null, false);
		}
	}
}
