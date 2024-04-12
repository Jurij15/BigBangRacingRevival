using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000655 RID: 1621
	internal static class MultiplayerInvitation
	{
		// Token: 0x06002F19 RID: 12057
		[DllImport("gpg")]
		internal static extern uint MultiplayerInvitation_AutomatchingSlotsAvailable(HandleRef self);

		// Token: 0x06002F1A RID: 12058
		[DllImport("gpg")]
		internal static extern IntPtr MultiplayerInvitation_InvitingParticipant(HandleRef self);

		// Token: 0x06002F1B RID: 12059
		[DllImport("gpg")]
		internal static extern uint MultiplayerInvitation_Variant(HandleRef self);

		// Token: 0x06002F1C RID: 12060
		[DllImport("gpg")]
		internal static extern ulong MultiplayerInvitation_CreationTime(HandleRef self);

		// Token: 0x06002F1D RID: 12061
		[DllImport("gpg")]
		internal static extern UIntPtr MultiplayerInvitation_Participants_Length(HandleRef self);

		// Token: 0x06002F1E RID: 12062
		[DllImport("gpg")]
		internal static extern IntPtr MultiplayerInvitation_Participants_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x06002F1F RID: 12063
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool MultiplayerInvitation_Valid(HandleRef self);

		// Token: 0x06002F20 RID: 12064
		[DllImport("gpg")]
		internal static extern Types.MultiplayerInvitationType MultiplayerInvitation_Type(HandleRef self);

		// Token: 0x06002F21 RID: 12065
		[DllImport("gpg")]
		internal static extern UIntPtr MultiplayerInvitation_Id(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F22 RID: 12066
		[DllImport("gpg")]
		internal static extern void MultiplayerInvitation_Dispose(HandleRef self);
	}
}
