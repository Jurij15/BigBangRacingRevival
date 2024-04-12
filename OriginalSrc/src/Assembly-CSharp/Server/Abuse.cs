using System;
using UnityEngine;

namespace Server
{
	// Token: 0x020003DF RID: 991
	public static class Abuse
	{
		// Token: 0x06001C43 RID: 7235 RVA: 0x00140304 File Offset: 0x0013E704
		public static HttpC Report(string _targetId, string _message, Action<HttpC> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/abuse/report");
			woeurl.AddParameter("targetId", _targetId);
			woeurl.AddParameter("text", WWW.EscapeURL(_message));
			return Request.Post(woeurl, null, _okCallback, _failureCallback, _errorCallback, null, null);
		}
	}
}
