using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000679 RID: 1657
	internal static class RealTimeRoomConfig
	{
		// Token: 0x06003004 RID: 12292
		[DllImport("gpg")]
		internal static extern UIntPtr RealTimeRoomConfig_PlayerIdsToInvite_Length(HandleRef self);

		// Token: 0x06003005 RID: 12293
		[DllImport("gpg")]
		internal static extern UIntPtr RealTimeRoomConfig_PlayerIdsToInvite_GetElement(HandleRef self, UIntPtr index, [In] [Out] char[] out_arg, UIntPtr out_size);

		// Token: 0x06003006 RID: 12294
		[DllImport("gpg")]
		internal static extern uint RealTimeRoomConfig_Variant(HandleRef self);

		// Token: 0x06003007 RID: 12295
		[DllImport("gpg")]
		internal static extern long RealTimeRoomConfig_ExclusiveBitMask(HandleRef self);

		// Token: 0x06003008 RID: 12296
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool RealTimeRoomConfig_Valid(HandleRef self);

		// Token: 0x06003009 RID: 12297
		[DllImport("gpg")]
		internal static extern uint RealTimeRoomConfig_MaximumAutomatchingPlayers(HandleRef self);

		// Token: 0x0600300A RID: 12298
		[DllImport("gpg")]
		internal static extern uint RealTimeRoomConfig_MinimumAutomatchingPlayers(HandleRef self);

		// Token: 0x0600300B RID: 12299
		[DllImport("gpg")]
		internal static extern void RealTimeRoomConfig_Dispose(HandleRef self);
	}
}
