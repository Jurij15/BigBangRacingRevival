using System;

namespace Server
{
	// Token: 0x0200041B RID: 1051
	public static class Ghost
	{
		// Token: 0x06001D2F RID: 7471 RVA: 0x0014D2C8 File Offset: 0x0014B6C8
		public static HttpC GetVersusNew(string _playerId, string _challengeId, Action<GhostData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/ghost/versus/get");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("playerId", _playerId);
			ResponseHandler<GhostData[]> responseHandler = new ResponseHandler<GhostData[]>(_okCallback, new Func<HttpC, GhostData[]>(ClientTools.ParseGhostData));
			return Request.Get(woeurl, "GHOST_VERSUS", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0014D338 File Offset: 0x0014B738
		public static HttpC GetFriendly(string _playerId, string _challengeId, Action<GhostData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/ghost/friendly/get");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("playerId", _playerId);
			ResponseHandler<GhostData[]> responseHandler = new ResponseHandler<GhostData[]>(_okCallback, new Func<HttpC, GhostData[]>(ClientTools.ParseGhostData));
			return Request.Get(woeurl, "FRIENDLY_VERSUS", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0014D3A8 File Offset: 0x0014B7A8
		public static HttpC GetCreatorNew(PsMinigameMetaData _meta, Action<GhostData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/ghost/creator/get");
			woeurl.AddParameter("gameId", _meta.id);
			woeurl.AddParameter("creatorId", _meta.creatorId);
			woeurl.AddParameter("gameMode", _meta.gameMode);
			ResponseHandler<GhostData[]> responseHandler = new ResponseHandler<GhostData[]>(_okCallback, new Func<HttpC, GhostData[]>(ClientTools.ParseGhostData));
			return Request.Get(woeurl, "GHOST_CREATOR", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}
	}
}
