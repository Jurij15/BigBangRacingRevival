using System;
using System.Collections.Generic;

namespace Server
{
	// Token: 0x020003E4 RID: 996
	public static class Challenge
	{
		// Token: 0x06001C4C RID: 7244 RVA: 0x001405D4 File Offset: 0x0013E9D4
		public static HttpC GetDaily(Action<List<PsChallengeMetaData>> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			ResponseHandler<List<PsChallengeMetaData>> responseHandler = new ResponseHandler<List<PsChallengeMetaData>>(_okCallback, new Func<HttpC, List<PsChallengeMetaData>>(ClientTools.ParseChallengeList));
			WOEURL woeurl = new WOEURL("/v1/challenge/daily/find");
			return Request.Get(woeurl, "CHALLENGE_GET_DAILY", new Action<HttpC>(responseHandler.RequestOk), _failCallback, _errorCallback);
		}
	}
}
