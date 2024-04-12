using System;
using UnityEngine;

namespace Server
{
	// Token: 0x02000426 RID: 1062
	public static class InAppPurchase
	{
		// Token: 0x06001D73 RID: 7539 RVA: 0x00150170 File Offset: 0x0014E570
		public static HttpC GetConfig(Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/iap/config");
			return Request.Get(woeurl, null, _okCallback, _failureCallback, _errorCallback);
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00150194 File Offset: 0x0014E594
		public static HttpC CompletePurchase(string _json, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/iap/purchase");
			woeurl.AddParameter("platform", Application.platform.ToString());
			woeurl.AddParameter("id", PlayerPrefsX.GetUserId());
			Debug.Log(woeurl.ConstructURL(), null);
			return Request.Post(woeurl, "SERVER_COMPLETE_PURCHASE", _json, _okCallback, _failureCallback, _errorCallback, null, new string[] { "ValidateReceipt" }, false);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00150208 File Offset: 0x0014E608
		public static HttpC GetNonce(string _productId, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/iap/nonce");
			woeurl.AddParameter("productId", _productId);
			return Request.Get(woeurl, null, _okCallback, _failureCallback, _errorCallback);
		}
	}
}
