using System;
using UnityEngine;

namespace Server
{
	// Token: 0x020003E3 RID: 995
	public static class Analytics
	{
		// Token: 0x06001C48 RID: 7240 RVA: 0x00140472 File Offset: 0x0013E872
		private static void AnalyticsOkCallback(HttpC _c)
		{
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x00140474 File Offset: 0x0013E874
		private static void AnalyticsFailCallback(HttpC _c, Action _retry)
		{
			ServerErrors.GetNetworkError(_c.www.error);
			_retry.Invoke();
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00140490 File Offset: 0x0013E890
		public static HttpC MinigameStarted(string _gameId)
		{
			WOEURL woeurl = new WOEURL("/v1/minigame/start");
			woeurl.AddParameter("gameId", _gameId);
			return Request.Post(woeurl, null, new Action<HttpC>(Analytics.AnalyticsOkCallback), delegate(HttpC c)
			{
				Analytics.AnalyticsFailCallback(c, delegate
				{
					Analytics.MinigameStarted(_gameId);
				});
			}, null, null, null);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x001404FC File Offset: 0x0013E8FC
		public static HttpC SendEvent(string _eventName, string _json)
		{
			WOEURL woeurl = new WOEURL("/v1/analytics/trackEvent");
			woeurl.AddParameter("event", WWW.EscapeURL(_eventName));
			return Request.Post(woeurl, null, _json, new Action<HttpC>(Analytics.AnalyticsOkCallback), delegate(HttpC c)
			{
				Analytics.AnalyticsFailCallback(c, delegate
				{
					Analytics.SendEvent(_eventName, _json);
				});
			}, null, null, null, false);
		}
	}
}
