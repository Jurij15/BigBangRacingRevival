using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006E9 RID: 1769
	internal class MultiplayerParticipant : BaseReferenceHolder
	{
		// Token: 0x060032E6 RID: 13030 RVA: 0x001CB2C4 File Offset: 0x001C96C4
		internal MultiplayerParticipant(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x001CB2CD File Offset: 0x001C96CD
		internal Types.ParticipantStatus Status()
		{
			return MultiplayerParticipant.MultiplayerParticipant_Status(base.SelfPtr());
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x001CB2DA File Offset: 0x001C96DA
		internal bool IsConnectedToRoom()
		{
			return MultiplayerParticipant.MultiplayerParticipant_IsConnectedToRoom(base.SelfPtr()) || this.Status() == Types.ParticipantStatus.JOINED;
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x001CB2F8 File Offset: 0x001C96F8
		internal string DisplayName()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => MultiplayerParticipant.MultiplayerParticipant_DisplayName(base.SelfPtr(), out_string, size));
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x001CB30B File Offset: 0x001C970B
		internal NativePlayer Player()
		{
			if (!MultiplayerParticipant.MultiplayerParticipant_HasPlayer(base.SelfPtr()))
			{
				return null;
			}
			return new NativePlayer(MultiplayerParticipant.MultiplayerParticipant_Player(base.SelfPtr()));
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x001CB32F File Offset: 0x001C972F
		internal string Id()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => MultiplayerParticipant.MultiplayerParticipant_Id(base.SelfPtr(), out_string, size));
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x001CB342 File Offset: 0x001C9742
		internal bool Valid()
		{
			return MultiplayerParticipant.MultiplayerParticipant_Valid(base.SelfPtr());
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x001CB34F File Offset: 0x001C974F
		protected override void CallDispose(HandleRef selfPointer)
		{
			MultiplayerParticipant.MultiplayerParticipant_Dispose(selfPointer);
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x001CB358 File Offset: 0x001C9758
		internal Participant AsParticipant()
		{
			NativePlayer nativePlayer = this.Player();
			return new Participant(this.DisplayName(), this.Id(), MultiplayerParticipant.StatusConversion[this.Status()], (nativePlayer != null) ? nativePlayer.AsPlayer() : null, this.IsConnectedToRoom());
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x001CB3A5 File Offset: 0x001C97A5
		internal static MultiplayerParticipant FromPointer(IntPtr pointer)
		{
			if (PInvokeUtilities.IsNull(pointer))
			{
				return null;
			}
			return new MultiplayerParticipant(pointer);
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x001CB3BA File Offset: 0x001C97BA
		internal static MultiplayerParticipant AutomatchingSentinel()
		{
			return new MultiplayerParticipant(Sentinels.Sentinels_AutomatchingParticipant());
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x001CB3C8 File Offset: 0x001C97C8
		// Note: this type is marked as 'beforefieldinit'.
		static MultiplayerParticipant()
		{
			Dictionary<Types.ParticipantStatus, Participant.ParticipantStatus> dictionary = new Dictionary<Types.ParticipantStatus, Participant.ParticipantStatus>();
			dictionary.Add(Types.ParticipantStatus.INVITED, Participant.ParticipantStatus.Invited);
			dictionary.Add(Types.ParticipantStatus.JOINED, Participant.ParticipantStatus.Joined);
			dictionary.Add(Types.ParticipantStatus.DECLINED, Participant.ParticipantStatus.Declined);
			dictionary.Add(Types.ParticipantStatus.LEFT, Participant.ParticipantStatus.Left);
			dictionary.Add(Types.ParticipantStatus.NOT_INVITED_YET, Participant.ParticipantStatus.NotInvitedYet);
			dictionary.Add(Types.ParticipantStatus.FINISHED, Participant.ParticipantStatus.Finished);
			dictionary.Add(Types.ParticipantStatus.UNRESPONSIVE, Participant.ParticipantStatus.Unresponsive);
			MultiplayerParticipant.StatusConversion = dictionary;
		}

		// Token: 0x040032DC RID: 13020
		private static readonly Dictionary<Types.ParticipantStatus, Participant.ParticipantStatus> StatusConversion;
	}
}
