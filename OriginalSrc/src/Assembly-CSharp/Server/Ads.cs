using System;

namespace Server
{
	// Token: 0x020003E1 RID: 993
	public static class Ads
	{
		// Token: 0x06001C45 RID: 7237 RVA: 0x00140374 File Offset: 0x0013E774
		public static HttpC GetAdNetworkConfig(Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/ads/config");
			return Request.Get(woeurl, null, _okCallback, _failureCallback, _errorCallback);
		}
	}
}
