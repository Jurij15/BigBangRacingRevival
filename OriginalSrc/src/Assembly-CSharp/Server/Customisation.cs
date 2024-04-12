using System;
using System.Collections;
using MiniJSON;

namespace Server
{
	// Token: 0x02000415 RID: 1045
	public static class Customisation
	{
		// Token: 0x06001D15 RID: 7445 RVA: 0x0014C2F4 File Offset: 0x0014A6F4
		public static HttpC Add(Hashtable _offroadCarVisual, Hashtable _offroadCarUpgrade, Hashtable _motorcycleVisual, Hashtable _motorcycleUpgrade, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/customisation/add");
			Hashtable hashtable = new Hashtable();
			if (_offroadCarVisual != null)
			{
				hashtable.Add("OffroadCarVisual", _offroadCarVisual);
			}
			if (_offroadCarUpgrade != null)
			{
				hashtable.Add("OffroadCarUpgrade", _offroadCarUpgrade);
			}
			if (_motorcycleVisual != null)
			{
				hashtable.Add("MotorcycleVisual", _motorcycleVisual);
			}
			if (_motorcycleUpgrade != null)
			{
				hashtable.Add("MotorcycleUpgrade", _motorcycleUpgrade);
			}
			string text = Json.Serialize(hashtable);
			return Request.Post(woeurl, "ADD_CUSTOMISATION", text, _okCallback, _failureCallback, _errorCallback, null, null, false);
		}
	}
}
