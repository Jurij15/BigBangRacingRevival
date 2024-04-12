using System;
using UnityEngine;

namespace Server
{
	// Token: 0x02000414 RID: 1044
	public static class Comment
	{
		// Token: 0x06001D13 RID: 7443 RVA: 0x0014C1C0 File Offset: 0x0014A5C0
		public static HttpC Save(string gameId, string comment, string _tag, string _tournamentId, Action<CommentData[]> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null)
		{
			ResponseHandler<CommentData[]> responseHandler = new ResponseHandler<CommentData[]>(_okCallback, new Func<HttpC, CommentData[]>(ClientTools.ParseComments));
			WOEURL woeurl = new WOEURL("/v2/minigame/comment/save");
			woeurl.AddParameter("gameId", gameId);
			woeurl.AddParameter("comment", WWW.EscapeURL(comment));
			woeurl.AddParameter("hash", ClientTools.GenerateHash(gameId + PlayerPrefsX.GetUserId() + comment));
			woeurl.AddParameter("tag", _tag);
			if (!string.IsNullOrEmpty(_tournamentId))
			{
				woeurl.AddParameter("tournamentId", _tournamentId);
			}
			return Request.Post(woeurl, null, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, null, new string[] { "Comment" });
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0014C280 File Offset: 0x0014A680
		public static HttpC Get(string _gameId, Action<CommentData[]> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null, int _commentLimit = 50)
		{
			ResponseHandler<CommentData[]> responseHandler = new ResponseHandler<CommentData[]>(_okCallback, new Func<HttpC, CommentData[]>(ClientTools.ParseComments));
			WOEURL woeurl = new WOEURL("/v2/minigame/comment/find");
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("limit", _commentLimit);
			return Request.Get(woeurl, "GET_COMMENTS", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}
	}
}
