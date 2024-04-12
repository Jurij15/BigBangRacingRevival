using System;
using Com.Google.Android.Gms.Common.Api;
using Google.Developers;

namespace Com.Google.Android.Gms.Games.Stats
{
	// Token: 0x0200061F RID: 1567
	public class StatsObject : JavaObjWrapper, Stats
	{
		// Token: 0x06002E1F RID: 11807 RVA: 0x001C24EA File Offset: 0x001C08EA
		public StatsObject(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x001C24F4 File Offset: 0x001C08F4
		public PendingResult<Stats_LoadPlayerStatsResultObject> loadPlayerStats(GoogleApiClient arg_GoogleApiClient_1, bool arg_bool_2)
		{
			IntPtr intPtr = base.InvokeCall<IntPtr>("loadPlayerStats", "(Lcom/google/android/gms/common/api/GoogleApiClient;Z)Lcom/google/android/gms/common/api/PendingResult;", new object[] { arg_GoogleApiClient_1, arg_bool_2 });
			return new PendingResult<Stats_LoadPlayerStatsResultObject>(intPtr);
		}

		// Token: 0x0400317F RID: 12671
		private const string CLASS_NAME = "com/google/android/gms/games/stats/Stats";
	}
}
