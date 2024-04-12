using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020005F0 RID: 1520
	public enum ConflictResolutionStrategy
	{
		// Token: 0x040030ED RID: 12525
		UseLongestPlaytime,
		// Token: 0x040030EE RID: 12526
		UseOriginal,
		// Token: 0x040030EF RID: 12527
		UseUnmerged,
		// Token: 0x040030F0 RID: 12528
		UseManual,
		// Token: 0x040030F1 RID: 12529
		UseLastKnownGood,
		// Token: 0x040030F2 RID: 12530
		UseMostRecentlySaved
	}
}
