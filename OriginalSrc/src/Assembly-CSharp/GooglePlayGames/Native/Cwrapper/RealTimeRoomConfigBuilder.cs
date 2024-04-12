using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200067A RID: 1658
	internal static class RealTimeRoomConfigBuilder
	{
		// Token: 0x0600300C RID: 12300
		[DllImport("gpg")]
		internal static extern void RealTimeRoomConfig_Builder_PopulateFromPlayerSelectUIResponse(HandleRef self, IntPtr response);

		// Token: 0x0600300D RID: 12301
		[DllImport("gpg")]
		internal static extern void RealTimeRoomConfig_Builder_SetVariant(HandleRef self, uint variant);

		// Token: 0x0600300E RID: 12302
		[DllImport("gpg")]
		internal static extern void RealTimeRoomConfig_Builder_AddPlayerToInvite(HandleRef self, string player_id);

		// Token: 0x0600300F RID: 12303
		[DllImport("gpg")]
		internal static extern IntPtr RealTimeRoomConfig_Builder_Construct();

		// Token: 0x06003010 RID: 12304
		[DllImport("gpg")]
		internal static extern void RealTimeRoomConfig_Builder_SetExclusiveBitMask(HandleRef self, ulong exclusive_bit_mask);

		// Token: 0x06003011 RID: 12305
		[DllImport("gpg")]
		internal static extern void RealTimeRoomConfig_Builder_SetMaximumAutomatchingPlayers(HandleRef self, uint maximum_automatching_players);

		// Token: 0x06003012 RID: 12306
		[DllImport("gpg")]
		internal static extern IntPtr RealTimeRoomConfig_Builder_Create(HandleRef self);

		// Token: 0x06003013 RID: 12307
		[DllImport("gpg")]
		internal static extern void RealTimeRoomConfig_Builder_SetMinimumAutomatchingPlayers(HandleRef self, uint minimum_automatching_players);

		// Token: 0x06003014 RID: 12308
		[DllImport("gpg")]
		internal static extern void RealTimeRoomConfig_Builder_Dispose(HandleRef self);
	}
}
