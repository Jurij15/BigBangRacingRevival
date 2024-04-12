using System;
using UnityEngine;

namespace Server
{
	// Token: 0x020003E5 RID: 997
	public static class Chat
	{
		// Token: 0x06001C4D RID: 7245 RVA: 0x0014062C File Offset: 0x0013EA2C
		public static HttpC SaveComment(string _comment, Action<CommentData[]> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null)
		{
			ResponseHandler<CommentData[]> responseHandler = new ResponseHandler<CommentData[]>(_okCallback, new Func<HttpC, CommentData[]>(ClientTools.ParseComments));
			WOEURL woeurl = new WOEURL("/v1/global/chat/save");
			woeurl.AddParameter("message", WWW.EscapeURL(_comment));
			return Request.Post(woeurl, null, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, null, new string[] { "Comment" });
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x001406A0 File Offset: 0x0013EAA0
		public static HttpC GetComments(Action<CommentData[]> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null, int _commentLimit = 50)
		{
			ResponseHandler<CommentData[]> responseHandler = new ResponseHandler<CommentData[]>(_okCallback, new Func<HttpC, CommentData[]>(ClientTools.ParseComments));
			WOEURL woeurl = new WOEURL("/v1/global/chat/find");
			woeurl.AddParameter("limit", _commentLimit);
			return Request.Get(woeurl, "GET_COMMENTS", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}
	}
}
