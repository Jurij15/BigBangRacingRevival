using System;

namespace Server
{
	// Token: 0x020003E2 RID: 994
	public static class AdventureBattle
	{
		// Token: 0x06001C46 RID: 7238 RVA: 0x00140398 File Offset: 0x0013E798
		public static HttpC GetGhost(string _gameId, string _playerUnit, Action<GhostData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/ghost/bossbattle/get");
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("playerUnit", _playerUnit);
			ResponseHandler<GhostData[]> responseHandler = new ResponseHandler<GhostData[]>(_okCallback, new Func<HttpC, GhostData[]>(ClientTools.ParseGhostDatas));
			return Request.Get(woeurl, "TROPHY_GHOST_GET_BY_TIME", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x00140408 File Offset: 0x0013E808
		public static HttpC SearchMinigame(string _playerUnit, Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			ResponseHandler<PsMinigameMetaData[]> responseHandler = new ResponseHandler<PsMinigameMetaData[]>(_okCallback, new Func<HttpC, PsMinigameMetaData[]>(ClientTools.ParseMinigameList));
			WOEURL woeurl = new WOEURL("/v1/bossbattle/new");
			woeurl.AddParameter("playerUnit", _playerUnit);
			HttpC httpC = Request.Get(woeurl, "MINIGAME_SEARCH", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
			httpC.objectData = _playerUnit;
			return httpC;
		}
	}
}
