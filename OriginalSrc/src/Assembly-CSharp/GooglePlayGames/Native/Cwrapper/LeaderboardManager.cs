using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200064A RID: 1610
	internal static class LeaderboardManager
	{
		// Token: 0x06002ED7 RID: 11991
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_FetchAll(HandleRef self, Types.DataSource data_source, LeaderboardManager.FetchAllCallback callback, IntPtr callback_arg);

		// Token: 0x06002ED8 RID: 11992
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_FetchScoreSummary(HandleRef self, Types.DataSource data_source, string leaderboard_id, Types.LeaderboardTimeSpan time_span, Types.LeaderboardCollection collection, LeaderboardManager.FetchScoreSummaryCallback callback, IntPtr callback_arg);

		// Token: 0x06002ED9 RID: 11993
		[DllImport("gpg")]
		internal static extern IntPtr LeaderboardManager_ScorePageToken(HandleRef self, string leaderboard_id, Types.LeaderboardStart start, Types.LeaderboardTimeSpan time_span, Types.LeaderboardCollection collection);

		// Token: 0x06002EDA RID: 11994
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_ShowAllUI(HandleRef self, LeaderboardManager.ShowAllUICallback callback, IntPtr callback_arg);

		// Token: 0x06002EDB RID: 11995
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_FetchScorePage(HandleRef self, Types.DataSource data_source, IntPtr token, uint max_results, LeaderboardManager.FetchScorePageCallback callback, IntPtr callback_arg);

		// Token: 0x06002EDC RID: 11996
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_FetchAllScoreSummaries(HandleRef self, Types.DataSource data_source, string leaderboard_id, LeaderboardManager.FetchAllScoreSummariesCallback callback, IntPtr callback_arg);

		// Token: 0x06002EDD RID: 11997
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_ShowUI(HandleRef self, string leaderboard_id, Types.LeaderboardTimeSpan time_span, LeaderboardManager.ShowUICallback callback, IntPtr callback_arg);

		// Token: 0x06002EDE RID: 11998
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_Fetch(HandleRef self, Types.DataSource data_source, string leaderboard_id, LeaderboardManager.FetchCallback callback, IntPtr callback_arg);

		// Token: 0x06002EDF RID: 11999
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_SubmitScore(HandleRef self, string leaderboard_id, ulong score, string metadata);

		// Token: 0x06002EE0 RID: 12000
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_FetchResponse_Dispose(HandleRef self);

		// Token: 0x06002EE1 RID: 12001
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus LeaderboardManager_FetchResponse_GetStatus(HandleRef self);

		// Token: 0x06002EE2 RID: 12002
		[DllImport("gpg")]
		internal static extern IntPtr LeaderboardManager_FetchResponse_GetData(HandleRef self);

		// Token: 0x06002EE3 RID: 12003
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_FetchAllResponse_Dispose(HandleRef self);

		// Token: 0x06002EE4 RID: 12004
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus LeaderboardManager_FetchAllResponse_GetStatus(HandleRef self);

		// Token: 0x06002EE5 RID: 12005
		[DllImport("gpg")]
		internal static extern UIntPtr LeaderboardManager_FetchAllResponse_GetData_Length(HandleRef self);

		// Token: 0x06002EE6 RID: 12006
		[DllImport("gpg")]
		internal static extern IntPtr LeaderboardManager_FetchAllResponse_GetData_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x06002EE7 RID: 12007
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_FetchScorePageResponse_Dispose(HandleRef self);

		// Token: 0x06002EE8 RID: 12008
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus LeaderboardManager_FetchScorePageResponse_GetStatus(HandleRef self);

		// Token: 0x06002EE9 RID: 12009
		[DllImport("gpg")]
		internal static extern IntPtr LeaderboardManager_FetchScorePageResponse_GetData(HandleRef self);

		// Token: 0x06002EEA RID: 12010
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_FetchScoreSummaryResponse_Dispose(HandleRef self);

		// Token: 0x06002EEB RID: 12011
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus LeaderboardManager_FetchScoreSummaryResponse_GetStatus(HandleRef self);

		// Token: 0x06002EEC RID: 12012
		[DllImport("gpg")]
		internal static extern IntPtr LeaderboardManager_FetchScoreSummaryResponse_GetData(HandleRef self);

		// Token: 0x06002EED RID: 12013
		[DllImport("gpg")]
		internal static extern void LeaderboardManager_FetchAllScoreSummariesResponse_Dispose(HandleRef self);

		// Token: 0x06002EEE RID: 12014
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus LeaderboardManager_FetchAllScoreSummariesResponse_GetStatus(HandleRef self);

		// Token: 0x06002EEF RID: 12015
		[DllImport("gpg")]
		internal static extern UIntPtr LeaderboardManager_FetchAllScoreSummariesResponse_GetData_Length(HandleRef self);

		// Token: 0x06002EF0 RID: 12016
		[DllImport("gpg")]
		internal static extern IntPtr LeaderboardManager_FetchAllScoreSummariesResponse_GetData_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x0200064B RID: 1611
		// (Invoke) Token: 0x06002EF2 RID: 12018
		internal delegate void FetchCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x0200064C RID: 1612
		// (Invoke) Token: 0x06002EF6 RID: 12022
		internal delegate void FetchAllCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x0200064D RID: 1613
		// (Invoke) Token: 0x06002EFA RID: 12026
		internal delegate void FetchScorePageCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x0200064E RID: 1614
		// (Invoke) Token: 0x06002EFE RID: 12030
		internal delegate void FetchScoreSummaryCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x0200064F RID: 1615
		// (Invoke) Token: 0x06002F02 RID: 12034
		internal delegate void FetchAllScoreSummariesCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000650 RID: 1616
		// (Invoke) Token: 0x06002F06 RID: 12038
		internal delegate void ShowAllUICallback(CommonErrorStatus.UIStatus arg0, IntPtr arg1);

		// Token: 0x02000651 RID: 1617
		// (Invoke) Token: 0x06002F0A RID: 12042
		internal delegate void ShowUICallback(CommonErrorStatus.UIStatus arg0, IntPtr arg1);
	}
}
