using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200067D RID: 1661
	internal static class ScoreSummary
	{
		// Token: 0x0600302E RID: 12334
		[DllImport("gpg")]
		internal static extern ulong ScoreSummary_ApproximateNumberOfScores(HandleRef self);

		// Token: 0x0600302F RID: 12335
		[DllImport("gpg")]
		internal static extern Types.LeaderboardTimeSpan ScoreSummary_TimeSpan(HandleRef self);

		// Token: 0x06003030 RID: 12336
		[DllImport("gpg")]
		internal static extern UIntPtr ScoreSummary_LeaderboardId(HandleRef self, [In] [Out] char[] out_arg, UIntPtr out_size);

		// Token: 0x06003031 RID: 12337
		[DllImport("gpg")]
		internal static extern Types.LeaderboardCollection ScoreSummary_Collection(HandleRef self);

		// Token: 0x06003032 RID: 12338
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool ScoreSummary_Valid(HandleRef self);

		// Token: 0x06003033 RID: 12339
		[DllImport("gpg")]
		internal static extern IntPtr ScoreSummary_CurrentPlayerScore(HandleRef self);

		// Token: 0x06003034 RID: 12340
		[DllImport("gpg")]
		internal static extern void ScoreSummary_Dispose(HandleRef self);
	}
}
