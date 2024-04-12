using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000678 RID: 1656
	internal static class RealTimeRoom
	{
		// Token: 0x06002FF8 RID: 12280
		[DllImport("gpg")]
		internal static extern Types.RealTimeRoomStatus RealTimeRoom_Status(HandleRef self);

		// Token: 0x06002FF9 RID: 12281
		[DllImport("gpg")]
		internal static extern UIntPtr RealTimeRoom_Description(HandleRef self, [In] [Out] char[] out_arg, UIntPtr out_size);

		// Token: 0x06002FFA RID: 12282
		[DllImport("gpg")]
		internal static extern uint RealTimeRoom_Variant(HandleRef self);

		// Token: 0x06002FFB RID: 12283
		[DllImport("gpg")]
		internal static extern ulong RealTimeRoom_CreationTime(HandleRef self);

		// Token: 0x06002FFC RID: 12284
		[DllImport("gpg")]
		internal static extern UIntPtr RealTimeRoom_Participants_Length(HandleRef self);

		// Token: 0x06002FFD RID: 12285
		[DllImport("gpg")]
		internal static extern IntPtr RealTimeRoom_Participants_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x06002FFE RID: 12286
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool RealTimeRoom_Valid(HandleRef self);

		// Token: 0x06002FFF RID: 12287
		[DllImport("gpg")]
		internal static extern uint RealTimeRoom_RemainingAutomatchingSlots(HandleRef self);

		// Token: 0x06003000 RID: 12288
		[DllImport("gpg")]
		internal static extern ulong RealTimeRoom_AutomatchWaitEstimate(HandleRef self);

		// Token: 0x06003001 RID: 12289
		[DllImport("gpg")]
		internal static extern IntPtr RealTimeRoom_CreatingParticipant(HandleRef self);

		// Token: 0x06003002 RID: 12290
		[DllImport("gpg")]
		internal static extern UIntPtr RealTimeRoom_Id(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06003003 RID: 12291
		[DllImport("gpg")]
		internal static extern void RealTimeRoom_Dispose(HandleRef self);
	}
}
