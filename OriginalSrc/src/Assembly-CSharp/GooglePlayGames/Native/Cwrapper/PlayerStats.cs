using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000668 RID: 1640
	internal static class PlayerStats
	{
		// Token: 0x06002F93 RID: 12179
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool PlayerStats_Valid(HandleRef self);

		// Token: 0x06002F94 RID: 12180
		[DllImport("gpg")]
		internal static extern void PlayerStats_Dispose(HandleRef self);

		// Token: 0x06002F95 RID: 12181
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool PlayerStats_HasAverageSessionLength(HandleRef self);

		// Token: 0x06002F96 RID: 12182
		[DllImport("gpg")]
		internal static extern float PlayerStats_AverageSessionLength(HandleRef self);

		// Token: 0x06002F97 RID: 12183
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool PlayerStats_HasChurnProbability(HandleRef self);

		// Token: 0x06002F98 RID: 12184
		[DllImport("gpg")]
		internal static extern float PlayerStats_ChurnProbability(HandleRef self);

		// Token: 0x06002F99 RID: 12185
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool PlayerStats_HasDaysSinceLastPlayed(HandleRef self);

		// Token: 0x06002F9A RID: 12186
		[DllImport("gpg")]
		internal static extern int PlayerStats_DaysSinceLastPlayed(HandleRef self);

		// Token: 0x06002F9B RID: 12187
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool PlayerStats_HasNumberOfPurchases(HandleRef self);

		// Token: 0x06002F9C RID: 12188
		[DllImport("gpg")]
		internal static extern int PlayerStats_NumberOfPurchases(HandleRef self);

		// Token: 0x06002F9D RID: 12189
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool PlayerStats_HasNumberOfSessions(HandleRef self);

		// Token: 0x06002F9E RID: 12190
		[DllImport("gpg")]
		internal static extern int PlayerStats_NumberOfSessions(HandleRef self);

		// Token: 0x06002F9F RID: 12191
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool PlayerStats_HasSessionPercentile(HandleRef self);

		// Token: 0x06002FA0 RID: 12192
		[DllImport("gpg")]
		internal static extern float PlayerStats_SessionPercentile(HandleRef self);

		// Token: 0x06002FA1 RID: 12193
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool PlayerStats_HasSpendPercentile(HandleRef self);

		// Token: 0x06002FA2 RID: 12194
		[DllImport("gpg")]
		internal static extern float PlayerStats_SpendPercentile(HandleRef self);
	}
}
