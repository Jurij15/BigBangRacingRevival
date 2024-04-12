using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000662 RID: 1634
	internal static class ParticipantResults
	{
		// Token: 0x06002F67 RID: 12135
		[DllImport("gpg")]
		internal static extern IntPtr ParticipantResults_WithResult(HandleRef self, string participant_id, uint placing, Types.MatchResult result);

		// Token: 0x06002F68 RID: 12136
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool ParticipantResults_Valid(HandleRef self);

		// Token: 0x06002F69 RID: 12137
		[DllImport("gpg")]
		internal static extern Types.MatchResult ParticipantResults_MatchResultForParticipant(HandleRef self, string participant_id);

		// Token: 0x06002F6A RID: 12138
		[DllImport("gpg")]
		internal static extern uint ParticipantResults_PlaceForParticipant(HandleRef self, string participant_id);

		// Token: 0x06002F6B RID: 12139
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool ParticipantResults_HasResultsForParticipant(HandleRef self, string participant_id);

		// Token: 0x06002F6C RID: 12140
		[DllImport("gpg")]
		internal static extern void ParticipantResults_Dispose(HandleRef self);
	}
}
