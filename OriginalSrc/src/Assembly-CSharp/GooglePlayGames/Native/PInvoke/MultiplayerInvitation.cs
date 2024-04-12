using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006E8 RID: 1768
	internal class MultiplayerInvitation : BaseReferenceHolder
	{
		// Token: 0x060032DA RID: 13018 RVA: 0x001CB15E File Offset: 0x001C955E
		internal MultiplayerInvitation(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x001CB168 File Offset: 0x001C9568
		internal MultiplayerParticipant Inviter()
		{
			MultiplayerParticipant multiplayerParticipant = new MultiplayerParticipant(MultiplayerInvitation.MultiplayerInvitation_InvitingParticipant(base.SelfPtr()));
			if (!multiplayerParticipant.Valid())
			{
				multiplayerParticipant.Dispose();
				return null;
			}
			return multiplayerParticipant;
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x001CB19A File Offset: 0x001C959A
		internal uint Variant()
		{
			return MultiplayerInvitation.MultiplayerInvitation_Variant(base.SelfPtr());
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x001CB1A7 File Offset: 0x001C95A7
		internal Types.MultiplayerInvitationType Type()
		{
			return MultiplayerInvitation.MultiplayerInvitation_Type(base.SelfPtr());
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x001CB1B4 File Offset: 0x001C95B4
		internal string Id()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => MultiplayerInvitation.MultiplayerInvitation_Id(base.SelfPtr(), out_string, size));
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x001CB1C7 File Offset: 0x001C95C7
		protected override void CallDispose(HandleRef selfPointer)
		{
			MultiplayerInvitation.MultiplayerInvitation_Dispose(selfPointer);
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x001CB1CF File Offset: 0x001C95CF
		internal uint AutomatchingSlots()
		{
			return MultiplayerInvitation.MultiplayerInvitation_AutomatchingSlotsAvailable(base.SelfPtr());
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x001CB1DC File Offset: 0x001C95DC
		internal uint ParticipantCount()
		{
			return MultiplayerInvitation.MultiplayerInvitation_Participants_Length(base.SelfPtr()).ToUInt32();
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x001CB1FC File Offset: 0x001C95FC
		private static Invitation.InvType ToInvType(Types.MultiplayerInvitationType invitationType)
		{
			if (invitationType == Types.MultiplayerInvitationType.REAL_TIME)
			{
				return Invitation.InvType.RealTime;
			}
			if (invitationType != Types.MultiplayerInvitationType.TURN_BASED)
			{
				Logger.d("Found unknown invitation type: " + invitationType);
				return Invitation.InvType.Unknown;
			}
			return Invitation.InvType.TurnBased;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x001CB22C File Offset: 0x001C962C
		internal Invitation AsInvitation()
		{
			Invitation.InvType invType = MultiplayerInvitation.ToInvType(this.Type());
			string text = this.Id();
			int num = (int)this.Variant();
			Participant participant;
			using (MultiplayerParticipant multiplayerParticipant = this.Inviter())
			{
				participant = ((multiplayerParticipant != null) ? multiplayerParticipant.AsParticipant() : null);
			}
			return new Invitation(invType, text, participant, num);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x001CB2A0 File Offset: 0x001C96A0
		internal static MultiplayerInvitation FromPointer(IntPtr selfPointer)
		{
			if (PInvokeUtilities.IsNull(selfPointer))
			{
				return null;
			}
			return new MultiplayerInvitation(selfPointer);
		}
	}
}
