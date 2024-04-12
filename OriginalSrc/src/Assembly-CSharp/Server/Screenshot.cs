using System;

namespace Server
{
	// Token: 0x0200043A RID: 1082
	public static class Screenshot
	{
		// Token: 0x06001E09 RID: 7689 RVA: 0x00155A5C File Offset: 0x00153E5C
		public static void Save(string gameId, byte[] data, Action<HttpC> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/minigame/screenshot/save");
			woeurl.AddParameter("gameId", gameId);
			Request.Post(woeurl, null, data, _okCallback, _failureCallback, _errorCallback, null, null);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00155A90 File Offset: 0x00153E90
		public static HttpC Get(string gameId, Action<byte[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			if (string.IsNullOrEmpty(gameId))
			{
				Debug.LogError("NULL OR EMPTY ID FORBIDDEN");
			}
			ResponseHandler<byte[]> responseHandler = new ResponseHandler<byte[]>(_okCallback, new Func<HttpC, byte[]>(ClientTools.ParseBytes));
			WOEURL woeurl = new WOEURL("/v1/minigame/screenshot/find");
			woeurl.AddParameter("gameId", gameId);
			return Request.Get(woeurl, null, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, new Action(PsMetagameManager.GetScreenshotERROR));
		}
	}
}
