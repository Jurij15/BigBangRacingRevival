using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000700 RID: 1792
	internal class NativeTurnBasedMatch : BaseReferenceHolder
	{
		// Token: 0x060033B5 RID: 13237 RVA: 0x001CC5A6 File Offset: 0x001CA9A6
		internal NativeTurnBasedMatch(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x001CC5AF File Offset: 0x001CA9AF
		internal uint AvailableAutomatchSlots()
		{
			return GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_AutomatchingSlotsAvailable(base.SelfPtr());
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x001CC5BC File Offset: 0x001CA9BC
		internal ulong CreationTime()
		{
			return GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_CreationTime(base.SelfPtr());
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x001CC5C9 File Offset: 0x001CA9C9
		internal IEnumerable<MultiplayerParticipant> Participants()
		{
			return PInvokeUtilities.ToEnumerable<MultiplayerParticipant>(GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Participants_Length(base.SelfPtr()), (UIntPtr index) => new MultiplayerParticipant(GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Participants_GetElement(base.SelfPtr(), index)));
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x001CC5E7 File Offset: 0x001CA9E7
		internal uint Version()
		{
			return GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Version(base.SelfPtr());
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x001CC5F4 File Offset: 0x001CA9F4
		internal uint Variant()
		{
			return GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Variant(base.SelfPtr());
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x001CC601 File Offset: 0x001CAA01
		internal ParticipantResults Results()
		{
			return new ParticipantResults(GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_ParticipantResults(base.SelfPtr()));
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x001CC614 File Offset: 0x001CAA14
		internal MultiplayerParticipant ParticipantWithId(string participantId)
		{
			foreach (MultiplayerParticipant multiplayerParticipant in this.Participants())
			{
				if (multiplayerParticipant.Id().Equals(participantId))
				{
					return multiplayerParticipant;
				}
				multiplayerParticipant.Dispose();
			}
			return null;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x001CC688 File Offset: 0x001CAA88
		internal MultiplayerParticipant PendingParticipant()
		{
			MultiplayerParticipant multiplayerParticipant = new MultiplayerParticipant(GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_PendingParticipant(base.SelfPtr()));
			if (!multiplayerParticipant.Valid())
			{
				multiplayerParticipant.Dispose();
				return null;
			}
			return multiplayerParticipant;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x001CC6BA File Offset: 0x001CAABA
		internal Types.MatchStatus MatchStatus()
		{
			return GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Status(base.SelfPtr());
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x001CC6C7 File Offset: 0x001CAAC7
		internal string Description()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Description(base.SelfPtr(), out_string, size));
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x001CC6DC File Offset: 0x001CAADC
		internal bool HasRematchId()
		{
			string text = this.RematchId();
			return string.IsNullOrEmpty(text) || !text.Equals("(null)");
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x001CC70C File Offset: 0x001CAB0C
		internal string RematchId()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_RematchId(base.SelfPtr(), out_string, size));
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x001CC71F File Offset: 0x001CAB1F
		internal byte[] Data()
		{
			if (!GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_HasData(base.SelfPtr()))
			{
				Logger.d("Match has no data.");
				return null;
			}
			return PInvokeUtilities.OutParamsToArray<byte>((byte[] bytes, UIntPtr size) => GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Data(base.SelfPtr(), bytes, size));
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x001CC74E File Offset: 0x001CAB4E
		internal string Id()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Id(base.SelfPtr(), out_string, size));
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x001CC761 File Offset: 0x001CAB61
		protected override void CallDispose(HandleRef selfPointer)
		{
			GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Dispose(selfPointer);
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x001CC76C File Offset: 0x001CAB6C
		internal GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch AsTurnBasedMatch(string selfPlayerId)
		{
			List<Participant> list = new List<Participant>();
			string text = null;
			string text2 = null;
			using (MultiplayerParticipant multiplayerParticipant = this.PendingParticipant())
			{
				if (multiplayerParticipant != null)
				{
					text2 = multiplayerParticipant.Id();
				}
			}
			foreach (MultiplayerParticipant multiplayerParticipant2 in this.Participants())
			{
				using (multiplayerParticipant2)
				{
					using (NativePlayer nativePlayer = multiplayerParticipant2.Player())
					{
						if (nativePlayer != null && nativePlayer.Id().Equals(selfPlayerId))
						{
							text = multiplayerParticipant2.Id();
						}
					}
					list.Add(multiplayerParticipant2.AsParticipant());
				}
			}
			bool flag = this.MatchStatus() == Types.MatchStatus.COMPLETED && !this.HasRematchId();
			return new GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch(this.Id(), this.Data(), flag, text, list, this.AvailableAutomatchSlots(), text2, NativeTurnBasedMatch.ToTurnStatus(this.MatchStatus()), NativeTurnBasedMatch.ToMatchStatus(text2, this.MatchStatus()), this.Variant(), this.Version());
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x001CC8D4 File Offset: 0x001CACD4
		private static GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus ToTurnStatus(Types.MatchStatus status)
		{
			switch (status)
			{
			case Types.MatchStatus.INVITED:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Invited;
			case Types.MatchStatus.THEIR_TURN:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.TheirTurn;
			case Types.MatchStatus.MY_TURN:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.MyTurn;
			case Types.MatchStatus.PENDING_COMPLETION:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Complete;
			case Types.MatchStatus.COMPLETED:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Complete;
			case Types.MatchStatus.CANCELED:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Complete;
			case Types.MatchStatus.EXPIRED:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Complete;
			default:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Unknown;
			}
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x001CC910 File Offset: 0x001CAD10
		private static GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus ToMatchStatus(string pendingParticipantId, Types.MatchStatus status)
		{
			switch (status)
			{
			case Types.MatchStatus.INVITED:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Active;
			case Types.MatchStatus.THEIR_TURN:
				return (pendingParticipantId != null) ? GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Active : GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.AutoMatching;
			case Types.MatchStatus.MY_TURN:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Active;
			case Types.MatchStatus.PENDING_COMPLETION:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Complete;
			case Types.MatchStatus.COMPLETED:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Complete;
			case Types.MatchStatus.CANCELED:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Cancelled;
			case Types.MatchStatus.EXPIRED:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Expired;
			default:
				return GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Unknown;
			}
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x001CC961 File Offset: 0x001CAD61
		internal static NativeTurnBasedMatch FromPointer(IntPtr selfPointer)
		{
			if (PInvokeUtilities.IsNull(selfPointer))
			{
				return null;
			}
			return new NativeTurnBasedMatch(selfPointer);
		}
	}
}
