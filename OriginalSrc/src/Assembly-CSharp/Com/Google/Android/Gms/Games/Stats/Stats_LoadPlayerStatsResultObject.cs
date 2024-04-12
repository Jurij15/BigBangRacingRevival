using System;
using Com.Google.Android.Gms.Common.Api;
using Google.Developers;

namespace Com.Google.Android.Gms.Games.Stats
{
	// Token: 0x0200061A RID: 1562
	public class Stats_LoadPlayerStatsResultObject : JavaObjWrapper, Stats_LoadPlayerStatsResult, Result
	{
		// Token: 0x06002E02 RID: 11778 RVA: 0x001C235D File Offset: 0x001C075D
		public Stats_LoadPlayerStatsResultObject(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x001C2368 File Offset: 0x001C0768
		public PlayerStats getPlayerStats()
		{
			IntPtr intPtr = base.InvokeCall<IntPtr>("getPlayerStats", "()Lcom/google/android/gms/games/stats/PlayerStats;", new object[0]);
			return new PlayerStatsObject(intPtr);
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x001C2394 File Offset: 0x001C0794
		public Status getStatus()
		{
			IntPtr intPtr = base.InvokeCall<IntPtr>("getStatus", "()Lcom/google/android/gms/common/api/Status;", new object[0]);
			return new Status(intPtr);
		}

		// Token: 0x0400317D RID: 12669
		private const string CLASS_NAME = "com/google/android/gms/games/stats/Stats$LoadPlayerStatsResult";
	}
}
