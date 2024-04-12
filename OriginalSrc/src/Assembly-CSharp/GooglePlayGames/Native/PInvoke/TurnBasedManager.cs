using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000720 RID: 1824
	internal class TurnBasedManager
	{
		// Token: 0x060034C0 RID: 13504 RVA: 0x001CEA51 File Offset: 0x001CCE51
		internal TurnBasedManager(GameServices services)
		{
			this.mGameServices = services;
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x001CEA60 File Offset: 0x001CCE60
		internal void GetMatch(string matchId, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_FetchMatch(this.mGameServices.AsHandle(), matchId, new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x001CEA96 File Offset: 0x001CCE96
		[MonoPInvokeCallback(typeof(TurnBasedMultiplayerManager.TurnBasedMatchCallback))]
		internal static void InternalTurnBasedMatchCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("TurnBasedManager#InternalTurnBasedMatchCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x001CEAA5 File Offset: 0x001CCEA5
		internal void CreateMatch(TurnBasedMatchConfig config, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_CreateTurnBasedMatch(this.mGameServices.AsHandle(), config.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x001CEAE0 File Offset: 0x001CCEE0
		internal void ShowPlayerSelectUI(uint minimumPlayers, uint maxiumPlayers, bool allowAutomatching, Action<PlayerSelectUIResponse> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_ShowPlayerSelectUI(this.mGameServices.AsHandle(), minimumPlayers, maxiumPlayers, allowAutomatching, new TurnBasedMultiplayerManager.PlayerSelectUICallback(TurnBasedManager.InternalPlayerSelectUIcallback), Callbacks.ToIntPtr<PlayerSelectUIResponse>(callback, new Func<IntPtr, PlayerSelectUIResponse>(PlayerSelectUIResponse.FromPointer)));
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x001CEB41 File Offset: 0x001CCF41
		[MonoPInvokeCallback(typeof(TurnBasedMultiplayerManager.PlayerSelectUICallback))]
		internal static void InternalPlayerSelectUIcallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("TurnBasedManager#PlayerSelectUICallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x001CEB50 File Offset: 0x001CCF50
		internal void GetAllTurnbasedMatches(Action<TurnBasedManager.TurnBasedMatchesResponse> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_FetchMatches(this.mGameServices.AsHandle(), new TurnBasedMultiplayerManager.TurnBasedMatchesCallback(TurnBasedManager.InternalTurnBasedMatchesCallback), Callbacks.ToIntPtr<TurnBasedManager.TurnBasedMatchesResponse>(callback, new Func<IntPtr, TurnBasedManager.TurnBasedMatchesResponse>(TurnBasedManager.TurnBasedMatchesResponse.FromPointer)));
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x001CEBAD File Offset: 0x001CCFAD
		[MonoPInvokeCallback(typeof(TurnBasedMultiplayerManager.TurnBasedMatchesCallback))]
		internal static void InternalTurnBasedMatchesCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("TurnBasedManager#TurnBasedMatchesCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x001CEBBC File Offset: 0x001CCFBC
		internal void AcceptInvitation(MultiplayerInvitation invitation, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
		{
			Logger.d("Accepting invitation: " + invitation.AsPointer().ToInt64());
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_AcceptInvitation(this.mGameServices.AsHandle(), invitation.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x001CEC24 File Offset: 0x001CD024
		internal void DeclineInvitation(MultiplayerInvitation invitation)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_DeclineInvitation(this.mGameServices.AsHandle(), invitation.AsPointer());
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x001CEC3C File Offset: 0x001CD03C
		internal void TakeTurn(NativeTurnBasedMatch match, byte[] data, MultiplayerParticipant nextParticipant, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TakeMyTurn(this.mGameServices.AsHandle(), match.AsPointer(), data, new UIntPtr((uint)data.Length), match.Results().AsPointer(), nextParticipant.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x001CEC9D File Offset: 0x001CD09D
		[MonoPInvokeCallback(typeof(TurnBasedMultiplayerManager.MatchInboxUICallback))]
		internal static void InternalMatchInboxUICallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("TurnBasedManager#MatchInboxUICallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x001CECAC File Offset: 0x001CD0AC
		internal void ShowInboxUI(Action<TurnBasedManager.MatchInboxUIResponse> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_ShowMatchInboxUI(this.mGameServices.AsHandle(), new TurnBasedMultiplayerManager.MatchInboxUICallback(TurnBasedManager.InternalMatchInboxUICallback), Callbacks.ToIntPtr<TurnBasedManager.MatchInboxUIResponse>(callback, new Func<IntPtr, TurnBasedManager.MatchInboxUIResponse>(TurnBasedManager.MatchInboxUIResponse.FromPointer)));
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x001CED0C File Offset: 0x001CD10C
		[MonoPInvokeCallback(typeof(TurnBasedMultiplayerManager.MultiplayerStatusCallback))]
		internal static void InternalMultiplayerStatusCallback(CommonErrorStatus.MultiplayerStatus status, IntPtr data)
		{
			Logger.d("InternalMultiplayerStatusCallback: " + status);
			Action<CommonErrorStatus.MultiplayerStatus> action = Callbacks.IntPtrToTempCallback<Action<CommonErrorStatus.MultiplayerStatus>>(data);
			try
			{
				action.Invoke(status);
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered executing InternalMultiplayerStatusCallback. Smothering to avoid passing exception into Native: " + ex);
			}
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x001CED68 File Offset: 0x001CD168
		internal void LeaveDuringMyTurn(NativeTurnBasedMatch match, MultiplayerParticipant nextParticipant, Action<CommonErrorStatus.MultiplayerStatus> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_LeaveMatchDuringMyTurn(this.mGameServices.AsHandle(), match.AsPointer(), nextParticipant.AsPointer(), new TurnBasedMultiplayerManager.MultiplayerStatusCallback(TurnBasedManager.InternalMultiplayerStatusCallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x001CEDB4 File Offset: 0x001CD1B4
		internal void FinishMatchDuringMyTurn(NativeTurnBasedMatch match, byte[] data, ParticipantResults results, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_FinishMatchDuringMyTurn(this.mGameServices.AsHandle(), match.AsPointer(), data, new UIntPtr((uint)data.Length), results.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x001CEE0A File Offset: 0x001CD20A
		internal void ConfirmPendingCompletion(NativeTurnBasedMatch match, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_ConfirmPendingCompletion(this.mGameServices.AsHandle(), match.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x001CEE45 File Offset: 0x001CD245
		internal void LeaveMatchDuringTheirTurn(NativeTurnBasedMatch match, Action<CommonErrorStatus.MultiplayerStatus> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_LeaveMatchDuringTheirTurn(this.mGameServices.AsHandle(), match.AsPointer(), new TurnBasedMultiplayerManager.MultiplayerStatusCallback(TurnBasedManager.InternalMultiplayerStatusCallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x001CEE80 File Offset: 0x001CD280
		internal void CancelMatch(NativeTurnBasedMatch match, Action<CommonErrorStatus.MultiplayerStatus> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_CancelMatch(this.mGameServices.AsHandle(), match.AsPointer(), new TurnBasedMultiplayerManager.MultiplayerStatusCallback(TurnBasedManager.InternalMultiplayerStatusCallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x001CEEBB File Offset: 0x001CD2BB
		internal void Rematch(NativeTurnBasedMatch match, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
		{
			TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_Rematch(this.mGameServices.AsHandle(), match.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x001CEEF6 File Offset: 0x001CD2F6
		private static IntPtr ToCallbackPointer(Action<TurnBasedManager.TurnBasedMatchResponse> callback)
		{
			return Callbacks.ToIntPtr<TurnBasedManager.TurnBasedMatchResponse>(callback, new Func<IntPtr, TurnBasedManager.TurnBasedMatchResponse>(TurnBasedManager.TurnBasedMatchResponse.FromPointer));
		}

		// Token: 0x04003320 RID: 13088
		private readonly GameServices mGameServices;

		// Token: 0x02000721 RID: 1825
		// (Invoke) Token: 0x060034D6 RID: 13526
		internal delegate void TurnBasedMatchCallback(TurnBasedManager.TurnBasedMatchResponse response);

		// Token: 0x02000722 RID: 1826
		internal class MatchInboxUIResponse : BaseReferenceHolder
		{
			// Token: 0x060034D9 RID: 13529 RVA: 0x001CEF1B File Offset: 0x001CD31B
			internal MatchInboxUIResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x060034DA RID: 13530 RVA: 0x001CEF24 File Offset: 0x001CD324
			internal CommonErrorStatus.UIStatus UiStatus()
			{
				return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_MatchInboxUIResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x060034DB RID: 13531 RVA: 0x001CEF31 File Offset: 0x001CD331
			internal NativeTurnBasedMatch Match()
			{
				if (this.UiStatus() != CommonErrorStatus.UIStatus.VALID)
				{
					return null;
				}
				return new NativeTurnBasedMatch(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_MatchInboxUIResponse_GetMatch(base.SelfPtr()));
			}

			// Token: 0x060034DC RID: 13532 RVA: 0x001CEF51 File Offset: 0x001CD351
			protected override void CallDispose(HandleRef selfPointer)
			{
				TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_MatchInboxUIResponse_Dispose(selfPointer);
			}

			// Token: 0x060034DD RID: 13533 RVA: 0x001CEF59 File Offset: 0x001CD359
			internal static TurnBasedManager.MatchInboxUIResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new TurnBasedManager.MatchInboxUIResponse(pointer);
			}
		}

		// Token: 0x02000723 RID: 1827
		internal class TurnBasedMatchResponse : BaseReferenceHolder
		{
			// Token: 0x060034DE RID: 13534 RVA: 0x001CEF7F File Offset: 0x001CD37F
			internal TurnBasedMatchResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x060034DF RID: 13535 RVA: 0x001CEF88 File Offset: 0x001CD388
			internal CommonErrorStatus.MultiplayerStatus ResponseStatus()
			{
				return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x060034E0 RID: 13536 RVA: 0x001CEF95 File Offset: 0x001CD395
			internal bool RequestSucceeded()
			{
				return this.ResponseStatus() > (CommonErrorStatus.MultiplayerStatus)0;
			}

			// Token: 0x060034E1 RID: 13537 RVA: 0x001CEFA0 File Offset: 0x001CD3A0
			internal NativeTurnBasedMatch Match()
			{
				if (!this.RequestSucceeded())
				{
					return null;
				}
				return new NativeTurnBasedMatch(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchResponse_GetMatch(base.SelfPtr()));
			}

			// Token: 0x060034E2 RID: 13538 RVA: 0x001CEFBF File Offset: 0x001CD3BF
			protected override void CallDispose(HandleRef selfPointer)
			{
				TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchResponse_Dispose(selfPointer);
			}

			// Token: 0x060034E3 RID: 13539 RVA: 0x001CEFC7 File Offset: 0x001CD3C7
			internal static TurnBasedManager.TurnBasedMatchResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new TurnBasedManager.TurnBasedMatchResponse(pointer);
			}
		}

		// Token: 0x02000724 RID: 1828
		internal class TurnBasedMatchesResponse : BaseReferenceHolder
		{
			// Token: 0x060034E4 RID: 13540 RVA: 0x001CEFED File Offset: 0x001CD3ED
			internal TurnBasedMatchesResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x060034E5 RID: 13541 RVA: 0x001CEFF6 File Offset: 0x001CD3F6
			protected override void CallDispose(HandleRef selfPointer)
			{
				TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_Dispose(base.SelfPtr());
			}

			// Token: 0x060034E6 RID: 13542 RVA: 0x001CF003 File Offset: 0x001CD403
			internal CommonErrorStatus.MultiplayerStatus Status()
			{
				return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x060034E7 RID: 13543 RVA: 0x001CF010 File Offset: 0x001CD410
			internal IEnumerable<MultiplayerInvitation> Invitations()
			{
				return PInvokeUtilities.ToEnumerable<MultiplayerInvitation>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetInvitations_Length(base.SelfPtr()), (UIntPtr index) => new MultiplayerInvitation(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetInvitations_GetElement(base.SelfPtr(), index)));
			}

			// Token: 0x060034E8 RID: 13544 RVA: 0x001CF030 File Offset: 0x001CD430
			internal int InvitationCount()
			{
				return (int)TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetInvitations_Length(base.SelfPtr()).ToUInt32();
			}

			// Token: 0x060034E9 RID: 13545 RVA: 0x001CF050 File Offset: 0x001CD450
			internal IEnumerable<NativeTurnBasedMatch> MyTurnMatches()
			{
				return PInvokeUtilities.ToEnumerable<NativeTurnBasedMatch>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetMyTurnMatches_Length(base.SelfPtr()), (UIntPtr index) => new NativeTurnBasedMatch(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetMyTurnMatches_GetElement(base.SelfPtr(), index)));
			}

			// Token: 0x060034EA RID: 13546 RVA: 0x001CF070 File Offset: 0x001CD470
			internal int MyTurnMatchesCount()
			{
				return (int)TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetMyTurnMatches_Length(base.SelfPtr()).ToUInt32();
			}

			// Token: 0x060034EB RID: 13547 RVA: 0x001CF090 File Offset: 0x001CD490
			internal IEnumerable<NativeTurnBasedMatch> TheirTurnMatches()
			{
				return PInvokeUtilities.ToEnumerable<NativeTurnBasedMatch>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetTheirTurnMatches_Length(base.SelfPtr()), (UIntPtr index) => new NativeTurnBasedMatch(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetTheirTurnMatches_GetElement(base.SelfPtr(), index)));
			}

			// Token: 0x060034EC RID: 13548 RVA: 0x001CF0B0 File Offset: 0x001CD4B0
			internal int TheirTurnMatchesCount()
			{
				return (int)TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetTheirTurnMatches_Length(base.SelfPtr()).ToUInt32();
			}

			// Token: 0x060034ED RID: 13549 RVA: 0x001CF0D0 File Offset: 0x001CD4D0
			internal IEnumerable<NativeTurnBasedMatch> CompletedMatches()
			{
				return PInvokeUtilities.ToEnumerable<NativeTurnBasedMatch>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetCompletedMatches_Length(base.SelfPtr()), (UIntPtr index) => new NativeTurnBasedMatch(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetCompletedMatches_GetElement(base.SelfPtr(), index)));
			}

			// Token: 0x060034EE RID: 13550 RVA: 0x001CF0F0 File Offset: 0x001CD4F0
			internal int CompletedMatchesCount()
			{
				return (int)TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetCompletedMatches_Length(base.SelfPtr()).ToUInt32();
			}

			// Token: 0x060034EF RID: 13551 RVA: 0x001CF110 File Offset: 0x001CD510
			internal static TurnBasedManager.TurnBasedMatchesResponse FromPointer(IntPtr pointer)
			{
				if (PInvokeUtilities.IsNull(pointer))
				{
					return null;
				}
				return new TurnBasedManager.TurnBasedMatchesResponse(pointer);
			}
		}
	}
}
