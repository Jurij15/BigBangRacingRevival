using System;

namespace Com.Google.Android.Gms.Games.Stats
{
	// Token: 0x0200061B RID: 1563
	public interface PlayerStats
	{
		// Token: 0x06002E05 RID: 11781
		float getAverageSessionLength();

		// Token: 0x06002E06 RID: 11782
		float getChurnProbability();

		// Token: 0x06002E07 RID: 11783
		int getDaysSinceLastPlayed();

		// Token: 0x06002E08 RID: 11784
		int getNumberOfPurchases();

		// Token: 0x06002E09 RID: 11785
		int getNumberOfSessions();

		// Token: 0x06002E0A RID: 11786
		float getSessionPercentile();

		// Token: 0x06002E0B RID: 11787
		float getSpendPercentile();

		// Token: 0x06002E0C RID: 11788
		float getSpendProbability();

		// Token: 0x06002E0D RID: 11789
		float getHighSpenderProbability();

		// Token: 0x06002E0E RID: 11790
		float getTotalSpendNext28Days();
	}
}
