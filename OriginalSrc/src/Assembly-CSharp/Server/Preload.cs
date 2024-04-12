using System;

namespace Server
{
	// Token: 0x02000434 RID: 1076
	public static class Preload
	{
		// Token: 0x06001DED RID: 7661 RVA: 0x00155270 File Offset: 0x00153670
		public static HttpC CheckClientVersion(string _clientVersion, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/preload/checkVersion");
			woeurl.AddParameter("version", _clientVersion);
			return Request.Get(woeurl, null, _okCallback, _failureCallback, _errorCallback);
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x001552A0 File Offset: 0x001536A0
		public static HttpC Check(string _fileName, Action<FileMetaData> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			ResponseHandler<FileMetaData> responseHandler = new ResponseHandler<FileMetaData>(_okCallback, new Func<HttpC, FileMetaData>(Preload.ParseFromResponse));
			WOEURL woeurl = new WOEURL("/v1/preload/checkFile");
			woeurl.AddParameter("name", _fileName);
			return Request.Get(woeurl, null, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, new Action(PsMetagameManager.GetPreloadCheckERROR));
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x0015531C File Offset: 0x0015371C
		public static HttpC Download(FileMetaData _meta, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			HttpC httpC = Request.PreloaderGet(_meta.path, null, _okCallback, _failureCallback, _errorCallback);
			httpC.objectData = _meta;
			return httpC;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00155341 File Offset: 0x00153741
		public static FileMetaData ParseFromResponse(HttpC _c)
		{
			return new FileMetaData(ClientTools.ParseServerResponse(_c.www.text));
		}
	}
}
