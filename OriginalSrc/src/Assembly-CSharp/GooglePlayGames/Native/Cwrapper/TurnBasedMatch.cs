using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000693 RID: 1683
	internal static class TurnBasedMatch
	{
		// Token: 0x06003084 RID: 12420
		[DllImport("gpg")]
		internal static extern uint TurnBasedMatch_AutomatchingSlotsAvailable(HandleRef self);

		// Token: 0x06003085 RID: 12421
		[DllImport("gpg")]
		internal static extern ulong TurnBasedMatch_CreationTime(HandleRef self);

		// Token: 0x06003086 RID: 12422
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMatch_Participants_Length(HandleRef self);

		// Token: 0x06003087 RID: 12423
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMatch_Participants_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x06003088 RID: 12424
		[DllImport("gpg")]
		internal static extern uint TurnBasedMatch_Version(HandleRef self);

		// Token: 0x06003089 RID: 12425
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMatch_ParticipantResults(HandleRef self);

		// Token: 0x0600308A RID: 12426
		[DllImport("gpg")]
		internal static extern Types.MatchStatus TurnBasedMatch_Status(HandleRef self);

		// Token: 0x0600308B RID: 12427
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMatch_Description(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x0600308C RID: 12428
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMatch_PendingParticipant(HandleRef self);

		// Token: 0x0600308D RID: 12429
		[DllImport("gpg")]
		internal static extern uint TurnBasedMatch_Variant(HandleRef self);

		// Token: 0x0600308E RID: 12430
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool TurnBasedMatch_HasPreviousMatchData(HandleRef self);

		// Token: 0x0600308F RID: 12431
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMatch_Data(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06003090 RID: 12432
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMatch_LastUpdatingParticipant(HandleRef self);

		// Token: 0x06003091 RID: 12433
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool TurnBasedMatch_HasData(HandleRef self);

		// Token: 0x06003092 RID: 12434
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMatch_SuggestedNextParticipant(HandleRef self);

		// Token: 0x06003093 RID: 12435
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMatch_PreviousMatchData(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06003094 RID: 12436
		[DllImport("gpg")]
		internal static extern ulong TurnBasedMatch_LastUpdateTime(HandleRef self);

		// Token: 0x06003095 RID: 12437
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMatch_RematchId(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06003096 RID: 12438
		[DllImport("gpg")]
		internal static extern uint TurnBasedMatch_Number(HandleRef self);

		// Token: 0x06003097 RID: 12439
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool TurnBasedMatch_HasRematchId(HandleRef self);

		// Token: 0x06003098 RID: 12440
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool TurnBasedMatch_Valid(HandleRef self);

		// Token: 0x06003099 RID: 12441
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMatch_CreatingParticipant(HandleRef self);

		// Token: 0x0600309A RID: 12442
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMatch_Id(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x0600309B RID: 12443
		[DllImport("gpg")]
		internal static extern void TurnBasedMatch_Dispose(HandleRef self);
	}
}
