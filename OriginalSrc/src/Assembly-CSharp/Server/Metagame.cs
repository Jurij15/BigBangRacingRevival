using System;

namespace Server
{
	// Token: 0x02000429 RID: 1065
	public static class Metagame
	{
		// Token: 0x06001D8F RID: 7567 RVA: 0x00151854 File Offset: 0x0014FC54
		public static HttpC GetTimedEvents(Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/metagame/getTimed");
			return Request.Get(woeurl, "TIMED_EVENT_GET", _okCallback, _failureCallback, _errorCallback);
		}
	}
}
