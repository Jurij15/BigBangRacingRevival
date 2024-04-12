using System;
using Com.Google.Android.Gms.Common.Api;

namespace Com.Google.Android.Gms.Games.Stats
{
	// Token: 0x0200061D RID: 1565
	public interface Stats
	{
		// Token: 0x06002E1D RID: 11805
		PendingResult<Stats_LoadPlayerStatsResultObject> loadPlayerStats(GoogleApiClient arg_GoogleApiClient_1, bool arg_bool_2);
	}
}
