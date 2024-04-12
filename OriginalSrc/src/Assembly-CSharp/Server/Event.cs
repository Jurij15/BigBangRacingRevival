using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace Server
{
	// Token: 0x02000417 RID: 1047
	public static class Event
	{
		// Token: 0x06001D1D RID: 7453 RVA: 0x0014CB1C File Offset: 0x0014AF1C
		public static HttpC Claim(int _id, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v2/event/claim");
			woeurl.AddParameter("id", _id);
			return Request.Post(woeurl, "EVENT_CLAIM", _okCallback, _failureCallback, _errorCallback, null, null);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0014CB58 File Offset: 0x0014AF58
		public static HttpC ClaimPatchNotes(int _id, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v2/event/claim");
			woeurl.AddParameter("id", _id);
			woeurl.AddParameter("type", "Patchnotes");
			return Request.Post(woeurl, "EVENT_CLAIM", _okCallback, _failureCallback, _errorCallback, null, null);
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0014CBA4 File Offset: 0x0014AFA4
		public static HttpC ClaimGift(Event.GiftClaimData _claimdata, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			list.Add("SetData");
			string text = Json.Serialize(_claimdata.setData);
			WOEURL woeurl = new WOEURL("/v2/event/claim");
			woeurl.AddParameter("id", _claimdata.id);
			woeurl.AddParameter("type", "Gift");
			return Request.Post(woeurl, "EVENT_CLAIM", text, _okCallback, _failureCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0014CC18 File Offset: 0x0014B018
		public static HttpC GetFeed(Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/event/feed");
			return Request.Get(woeurl, "EVENT_FEED", _okCallback, _failureCallback, _errorCallback);
		}

		// Token: 0x02000418 RID: 1048
		public class GiftClaimData
		{
			// Token: 0x04001FE4 RID: 8164
			public int id;

			// Token: 0x04001FE5 RID: 8165
			public Hashtable setData;
		}
	}
}
