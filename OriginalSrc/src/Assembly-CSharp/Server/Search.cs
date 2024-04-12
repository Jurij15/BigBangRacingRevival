using System;
using UnityEngine;

namespace Server
{
	// Token: 0x0200043B RID: 1083
	public static class Search
	{
		// Token: 0x06001E0B RID: 7691 RVA: 0x00155B20 File Offset: 0x00153F20
		public static HttpC SearchGamesAndPlayers(string _query, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/search/gameAndPlayer");
			woeurl.AddParameter("query", WWW.EscapeURL(_query));
			return Request.Get(woeurl, "SEARCH_GAMES_AND_PLAYERS", _okCallback, _failureCallback, _errorCallback);
		}
	}
}
