using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000656 RID: 1622
	internal static class MultiplayerParticipant
	{
		// Token: 0x06002F23 RID: 12067
		[DllImport("gpg")]
		internal static extern Types.ParticipantStatus MultiplayerParticipant_Status(HandleRef self);

		// Token: 0x06002F24 RID: 12068
		[DllImport("gpg")]
		internal static extern uint MultiplayerParticipant_MatchRank(HandleRef self);

		// Token: 0x06002F25 RID: 12069
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool MultiplayerParticipant_IsConnectedToRoom(HandleRef self);

		// Token: 0x06002F26 RID: 12070
		[DllImport("gpg")]
		internal static extern UIntPtr MultiplayerParticipant_DisplayName(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F27 RID: 12071
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool MultiplayerParticipant_HasPlayer(HandleRef self);

		// Token: 0x06002F28 RID: 12072
		[DllImport("gpg")]
		internal static extern UIntPtr MultiplayerParticipant_AvatarUrl(HandleRef self, Types.ImageResolution resolution, [In] [Out] char[] out_arg, UIntPtr out_size);

		// Token: 0x06002F29 RID: 12073
		[DllImport("gpg")]
		internal static extern Types.MatchResult MultiplayerParticipant_MatchResult(HandleRef self);

		// Token: 0x06002F2A RID: 12074
		[DllImport("gpg")]
		internal static extern IntPtr MultiplayerParticipant_Player(HandleRef self);

		// Token: 0x06002F2B RID: 12075
		[DllImport("gpg")]
		internal static extern void MultiplayerParticipant_Dispose(HandleRef self);

		// Token: 0x06002F2C RID: 12076
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool MultiplayerParticipant_Valid(HandleRef self);

		// Token: 0x06002F2D RID: 12077
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool MultiplayerParticipant_HasMatchResult(HandleRef self);

		// Token: 0x06002F2E RID: 12078
		[DllImport("gpg")]
		internal static extern UIntPtr MultiplayerParticipant_Id(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);
	}
}
