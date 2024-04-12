using System;

namespace Server
{
	// Token: 0x020003E0 RID: 992
	public static class Achievement
	{
		// Token: 0x06001C44 RID: 7236 RVA: 0x00140348 File Offset: 0x0013E748
		public static HttpC Upsert(string _json, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/achievement/upsert");
			return Request.Post(woeurl, "UPSERT_ACHIEVEMENT", _json, _okCallback, _failureCallback, _errorCallback, null, null, false);
		}
	}
}
