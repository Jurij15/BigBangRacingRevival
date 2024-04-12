using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000694 RID: 1684
	internal static class TurnBasedMatchConfig
	{
		// Token: 0x0600309C RID: 12444
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMatchConfig_PlayerIdsToInvite_Length(HandleRef self);

		// Token: 0x0600309D RID: 12445
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMatchConfig_PlayerIdsToInvite_GetElement(HandleRef self, UIntPtr index, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x0600309E RID: 12446
		[DllImport("gpg")]
		internal static extern uint TurnBasedMatchConfig_Variant(HandleRef self);

		// Token: 0x0600309F RID: 12447
		[DllImport("gpg")]
		internal static extern long TurnBasedMatchConfig_ExclusiveBitMask(HandleRef self);

		// Token: 0x060030A0 RID: 12448
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool TurnBasedMatchConfig_Valid(HandleRef self);

		// Token: 0x060030A1 RID: 12449
		[DllImport("gpg")]
		internal static extern uint TurnBasedMatchConfig_MaximumAutomatchingPlayers(HandleRef self);

		// Token: 0x060030A2 RID: 12450
		[DllImport("gpg")]
		internal static extern uint TurnBasedMatchConfig_MinimumAutomatchingPlayers(HandleRef self);

		// Token: 0x060030A3 RID: 12451
		[DllImport("gpg")]
		internal static extern void TurnBasedMatchConfig_Dispose(HandleRef self);
	}
}
