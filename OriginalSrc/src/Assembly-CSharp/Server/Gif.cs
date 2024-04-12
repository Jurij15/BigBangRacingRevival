using System;
using System.Collections.Generic;
using MiniJSON;

namespace Server
{
	// Token: 0x0200041F RID: 1055
	public static class Gif
	{
		// Token: 0x06001D5A RID: 7514 RVA: 0x0014F52C File Offset: 0x0014D92C
		public static HttpC SaveWithLevel(string gameId, byte[] data, Action<string> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v2/gif/save");
			woeurl.AddParameter("gameId", gameId);
			ResponseHandler<string> responseHandler = new ResponseHandler<string>(_okCallback, new Func<HttpC, string>(Gif.ParseUrlFromJson));
			return Request.Post(woeurl, null, data, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, null, null);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0014F590 File Offset: 0x0014D990
		public static HttpC Save(byte[] data, Action<string> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/gif/save");
			ResponseHandler<string> responseHandler = new ResponseHandler<string>(_okCallback, new Func<HttpC, string>(Gif.ParseUrlFromJson));
			return Request.Post(woeurl, null, data, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, null, null);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0014F5E4 File Offset: 0x0014D9E4
		public static string ParseUrlFromJson(HttpC _c)
		{
			Dictionary<string, object> dictionary = Json.Deserialize(_c.www.text) as Dictionary<string, object>;
			if (dictionary.ContainsKey("url"))
			{
				return (string)dictionary["url"];
			}
			return string.Empty;
		}
	}
}
