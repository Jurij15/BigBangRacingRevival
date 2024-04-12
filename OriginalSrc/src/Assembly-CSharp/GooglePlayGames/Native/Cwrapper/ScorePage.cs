using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200067C RID: 1660
	internal static class ScorePage
	{
		// Token: 0x0600301A RID: 12314
		[DllImport("gpg")]
		internal static extern void ScorePage_Dispose(HandleRef self);

		// Token: 0x0600301B RID: 12315
		[DllImport("gpg")]
		internal static extern Types.LeaderboardTimeSpan ScorePage_TimeSpan(HandleRef self);

		// Token: 0x0600301C RID: 12316
		[DllImport("gpg")]
		internal static extern UIntPtr ScorePage_LeaderboardId(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x0600301D RID: 12317
		[DllImport("gpg")]
		internal static extern Types.LeaderboardCollection ScorePage_Collection(HandleRef self);

		// Token: 0x0600301E RID: 12318
		[DllImport("gpg")]
		internal static extern Types.LeaderboardStart ScorePage_Start(HandleRef self);

		// Token: 0x0600301F RID: 12319
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool ScorePage_Valid(HandleRef self);

		// Token: 0x06003020 RID: 12320
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool ScorePage_HasPreviousScorePage(HandleRef self);

		// Token: 0x06003021 RID: 12321
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool ScorePage_HasNextScorePage(HandleRef self);

		// Token: 0x06003022 RID: 12322
		[DllImport("gpg")]
		internal static extern IntPtr ScorePage_PreviousScorePageToken(HandleRef self);

		// Token: 0x06003023 RID: 12323
		[DllImport("gpg")]
		internal static extern IntPtr ScorePage_NextScorePageToken(HandleRef self);

		// Token: 0x06003024 RID: 12324
		[DllImport("gpg")]
		internal static extern UIntPtr ScorePage_Entries_Length(HandleRef self);

		// Token: 0x06003025 RID: 12325
		[DllImport("gpg")]
		internal static extern IntPtr ScorePage_Entries_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x06003026 RID: 12326
		[DllImport("gpg")]
		internal static extern void ScorePage_Entry_Dispose(HandleRef self);

		// Token: 0x06003027 RID: 12327
		[DllImport("gpg")]
		internal static extern UIntPtr ScorePage_Entry_PlayerId(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06003028 RID: 12328
		[DllImport("gpg")]
		internal static extern ulong ScorePage_Entry_LastModified(HandleRef self);

		// Token: 0x06003029 RID: 12329
		[DllImport("gpg")]
		internal static extern IntPtr ScorePage_Entry_Score(HandleRef self);

		// Token: 0x0600302A RID: 12330
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool ScorePage_Entry_Valid(HandleRef self);

		// Token: 0x0600302B RID: 12331
		[DllImport("gpg")]
		internal static extern ulong ScorePage_Entry_LastModifiedTime(HandleRef self);

		// Token: 0x0600302C RID: 12332
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool ScorePage_ScorePageToken_Valid(HandleRef self);

		// Token: 0x0600302D RID: 12333
		[DllImport("gpg")]
		internal static extern void ScorePage_ScorePageToken_Dispose(HandleRef self);
	}
}
