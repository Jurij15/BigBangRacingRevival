using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000695 RID: 1685
	internal static class TurnBasedMatchConfigBuilder
	{
		// Token: 0x060030A4 RID: 12452
		[DllImport("gpg")]
		internal static extern void TurnBasedMatchConfig_Builder_PopulateFromPlayerSelectUIResponse(HandleRef self, IntPtr response);

		// Token: 0x060030A5 RID: 12453
		[DllImport("gpg")]
		internal static extern void TurnBasedMatchConfig_Builder_SetVariant(HandleRef self, uint variant);

		// Token: 0x060030A6 RID: 12454
		[DllImport("gpg")]
		internal static extern void TurnBasedMatchConfig_Builder_AddPlayerToInvite(HandleRef self, string player_id);

		// Token: 0x060030A7 RID: 12455
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMatchConfig_Builder_Construct();

		// Token: 0x060030A8 RID: 12456
		[DllImport("gpg")]
		internal static extern void TurnBasedMatchConfig_Builder_SetExclusiveBitMask(HandleRef self, ulong exclusive_bit_mask);

		// Token: 0x060030A9 RID: 12457
		[DllImport("gpg")]
		internal static extern void TurnBasedMatchConfig_Builder_SetMaximumAutomatchingPlayers(HandleRef self, uint maximum_automatching_players);

		// Token: 0x060030AA RID: 12458
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMatchConfig_Builder_Create(HandleRef self);

		// Token: 0x060030AB RID: 12459
		[DllImport("gpg")]
		internal static extern void TurnBasedMatchConfig_Builder_SetMinimumAutomatchingPlayers(HandleRef self, uint minimum_automatching_players);

		// Token: 0x060030AC RID: 12460
		[DllImport("gpg")]
		internal static extern void TurnBasedMatchConfig_Builder_Dispose(HandleRef self);
	}
}
