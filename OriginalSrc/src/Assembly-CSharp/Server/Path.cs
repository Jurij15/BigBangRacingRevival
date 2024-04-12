using System;

namespace Server
{
	// Token: 0x0200042D RID: 1069
	public static class Path
	{
		// Token: 0x06001DAA RID: 7594 RVA: 0x00152514 File Offset: 0x00150914
		public static HttpC GetInitial(Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null, int minorVersion = 0, string _planetIdentifier = "Adventure")
		{
			WOEURL woeurl = new WOEURL("/v1/path/db/find");
			woeurl.AddParameter("planet", _planetIdentifier);
			if (minorVersion != 0)
			{
				woeurl.AddParameter("version", minorVersion);
			}
			return Request.Get(woeurl, "PATH_DB_FIND", _okCallback, _failureCallback, _errorCallback);
		}
	}
}
