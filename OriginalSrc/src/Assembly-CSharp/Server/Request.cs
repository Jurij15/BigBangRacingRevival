using System;
using System.Collections;

namespace Server
{
	// Token: 0x02000437 RID: 1079
	public static class Request
	{
		// Token: 0x06001DF8 RID: 7672 RVA: 0x00155799 File Offset: 0x00153B99
		public static HttpC Get(WOEURL _url, string _tag)
		{
			return Request.Get(_url, _tag, new Action<HttpC>(Request.GenericSuccessCallback));
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x001557BF File Offset: 0x00153BBF
		public static HttpC Get(WOEURL _url, string _tag, Action<HttpC> _okCallback)
		{
			return Request.Get(_url, _tag, _okCallback, new Action<HttpC>(Request.GenericFailureCallback), null);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x001557E8 File Offset: 0x00153BE8
		public static HttpC Get(WOEURL _url, string _tag, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			ErrorHandler errorHandler = new ErrorHandler(_okCallback, _errorCallback);
			HttpC httpC = _url.AddGetComponent(_tag, null, true);
			httpC.requestComplete += new Action<HttpC>(errorHandler.CheckForErrors);
			httpC.requestFailed += _failureCallback;
			return httpC;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00155824 File Offset: 0x00153C24
		public static HttpC PreloaderGet(string _path, string _tag, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			HttpC httpC = HttpS.AddGetComponent(_path, _tag, null, true, new Hashtable());
			httpC.requestComplete += _okCallback;
			httpC.requestFailed += _failureCallback;
			return httpC;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0015584F File Offset: 0x00153C4F
		public static HttpC Post(WOEURL _url, string _tag)
		{
			return Request.Post(_url, _tag, null);
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x00155859 File Offset: 0x00153C59
		public static HttpC Post(WOEURL _url, string _tag, string _json)
		{
			return Request.Post(_url, _tag, _json, new Action<HttpC>(Request.GenericSuccessCallback));
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x00155880 File Offset: 0x00153C80
		public static HttpC Post(WOEURL _url, string _tag, string _json, Action<HttpC> _okCallback)
		{
			return Request.Post(_url, _tag, _json, _okCallback, new Action<HttpC>(Request.GenericFailureCallback), null, null, null, false);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x001558B8 File Offset: 0x00153CB8
		public static HttpC Post(WOEURL _url, string _tag, string _json, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null, Hashtable _headers = null, string[] _queueTags = null, bool disableAutoRetry = false)
		{
			ErrorHandler errorHandler = new ErrorHandler(_okCallback, _errorCallback);
			HttpC httpC = _url.AddPostComponent(_tag, _json, _headers, _queueTags, true);
			httpC.requestComplete += new Action<HttpC>(errorHandler.CheckForErrors);
			httpC.requestFailed += _failureCallback;
			if (disableAutoRetry)
			{
				httpC.retryCount = 0;
			}
			return httpC;
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x00155905 File Offset: 0x00153D05
		public static HttpC Post(WOEURL _url, string _tag, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null, Hashtable _headers = null, string[] _queueTags = null)
		{
			return Request.Post(_url, _tag, new byte[1], _okCallback, _failureCallback, _errorCallback, _headers, _queueTags);
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0015591C File Offset: 0x00153D1C
		public static HttpC Post(WOEURL _url, string _tag, byte[] _data, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null, Hashtable _headers = null, string[] _queueTags = null)
		{
			ErrorHandler errorHandler = new ErrorHandler(_okCallback, _errorCallback);
			HttpC httpC = _url.AddPostComponent(_tag, _data, _headers, _queueTags, true);
			httpC.requestComplete += new Action<HttpC>(errorHandler.CheckForErrors);
			httpC.requestFailed += _failureCallback;
			return httpC;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x0015595B File Offset: 0x00153D5B
		public static void GenericSuccessCallback(HttpC _c, string _requestIdentifier)
		{
			Debug.Log("SERVER REQUEST SUCCEEDED: " + _requestIdentifier, null);
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x0015596E File Offset: 0x00153D6E
		public static void GenericSuccessCallback(HttpC _c)
		{
			Debug.Log("SERVER REQUEST SUCCEEDED", null);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x0015597B File Offset: 0x00153D7B
		public static void GenericFailureCallback(HttpC _c)
		{
			ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), null, null, StringID.TRY_AGAIN_SERVER);
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x001559A3 File Offset: 0x00153DA3
		public static void OnFailureInstantRetryCallback(HttpC _c, Action _retryFunction)
		{
			ServerErrors.GetNetworkError(_c.www.error);
			_retryFunction.Invoke();
		}
	}
}
