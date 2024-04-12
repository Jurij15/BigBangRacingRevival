using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000649 RID: 1609
	internal static class Leaderboard
	{
		// Token: 0x06002ED1 RID: 11985
		[DllImport("gpg")]
		internal static extern UIntPtr Leaderboard_Name(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002ED2 RID: 11986
		[DllImport("gpg")]
		internal static extern UIntPtr Leaderboard_Id(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002ED3 RID: 11987
		[DllImport("gpg")]
		internal static extern UIntPtr Leaderboard_IconUrl(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002ED4 RID: 11988
		[DllImport("gpg")]
		internal static extern void Leaderboard_Dispose(HandleRef self);

		// Token: 0x06002ED5 RID: 11989
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool Leaderboard_Valid(HandleRef self);

		// Token: 0x06002ED6 RID: 11990
		[DllImport("gpg")]
		internal static extern Types.LeaderboardOrder Leaderboard_Order(HandleRef self);
	}
}
