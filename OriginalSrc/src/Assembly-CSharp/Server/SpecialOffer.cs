using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace Server
{
	// Token: 0x02000449 RID: 1097
	public static class SpecialOffer
	{
		// Token: 0x06001E82 RID: 7810 RVA: 0x0015843C File Offset: 0x0015683C
		public static HttpC ClaimChest(SpecialOffer.ClaimData _claimData, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/specialOffer/claim");
			List<string> list = new List<string>();
			list.Add("SetData");
			_claimData.data.Add("chest", _claimData.chest);
			string text = Json.Serialize(_claimData.data);
			return Request.Post(woeurl, "SPECIAL_OFFER_CHEST_CLAIM", text, _okCallback, _failureCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x001584A0 File Offset: 0x001568A0
		public static HttpC Start(SpecialOffer.StartData _startData, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/specialOffer/start");
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("currentOfferId", _startData.specialOfferId);
			dictionary.Add("startTime", _startData.startTime);
			string text = Json.Serialize(dictionary);
			return Request.Post(woeurl, "SPECIAL_OFFER_START", text, _okCallback, _failureCallback, _errorCallback, null, null, false);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00158500 File Offset: 0x00156900
		public static HttpC GetSpecialOffers(Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/specialOffer");
			return Request.Get(woeurl, "SPECIAL_OFFER_GET", _okCallback, _failureCallback, _errorCallback);
		}

		// Token: 0x0200044A RID: 1098
		public class ClaimData
		{
			// Token: 0x040021B4 RID: 8628
			public string chest;

			// Token: 0x040021B5 RID: 8629
			public Hashtable data;
		}

		// Token: 0x0200044B RID: 1099
		public class StartData
		{
			// Token: 0x040021B6 RID: 8630
			public string specialOfferId;

			// Token: 0x040021B7 RID: 8631
			public long startTime;
		}
	}
}
