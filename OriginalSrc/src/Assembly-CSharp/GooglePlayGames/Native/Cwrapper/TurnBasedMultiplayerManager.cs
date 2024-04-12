using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000696 RID: 1686
	internal static class TurnBasedMultiplayerManager
	{
		// Token: 0x060030AD RID: 12461
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_ShowPlayerSelectUI(HandleRef self, uint minimum_players, uint maximum_players, [MarshalAs(3)] bool allow_automatch, TurnBasedMultiplayerManager.PlayerSelectUICallback callback, IntPtr callback_arg);

		// Token: 0x060030AE RID: 12462
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_CancelMatch(HandleRef self, IntPtr match, TurnBasedMultiplayerManager.MultiplayerStatusCallback callback, IntPtr callback_arg);

		// Token: 0x060030AF RID: 12463
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_DismissMatch(HandleRef self, IntPtr match);

		// Token: 0x060030B0 RID: 12464
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_ShowMatchInboxUI(HandleRef self, TurnBasedMultiplayerManager.MatchInboxUICallback callback, IntPtr callback_arg);

		// Token: 0x060030B1 RID: 12465
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_SynchronizeData(HandleRef self);

		// Token: 0x060030B2 RID: 12466
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_Rematch(HandleRef self, IntPtr match, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

		// Token: 0x060030B3 RID: 12467
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_DismissInvitation(HandleRef self, IntPtr invitation);

		// Token: 0x060030B4 RID: 12468
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_FetchMatch(HandleRef self, string match_id, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

		// Token: 0x060030B5 RID: 12469
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_DeclineInvitation(HandleRef self, IntPtr invitation);

		// Token: 0x060030B6 RID: 12470
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_FinishMatchDuringMyTurn(HandleRef self, IntPtr match, byte[] match_data, UIntPtr match_data_size, IntPtr results, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

		// Token: 0x060030B7 RID: 12471
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_FetchMatches(HandleRef self, TurnBasedMultiplayerManager.TurnBasedMatchesCallback callback, IntPtr callback_arg);

		// Token: 0x060030B8 RID: 12472
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_CreateTurnBasedMatch(HandleRef self, IntPtr config, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

		// Token: 0x060030B9 RID: 12473
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_AcceptInvitation(HandleRef self, IntPtr invitation, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

		// Token: 0x060030BA RID: 12474
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_TakeMyTurn(HandleRef self, IntPtr match, byte[] match_data, UIntPtr match_data_size, IntPtr results, IntPtr next_participant, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

		// Token: 0x060030BB RID: 12475
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_ConfirmPendingCompletion(HandleRef self, IntPtr match, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

		// Token: 0x060030BC RID: 12476
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_LeaveMatchDuringMyTurn(HandleRef self, IntPtr match, IntPtr next_participant, TurnBasedMultiplayerManager.MultiplayerStatusCallback callback, IntPtr callback_arg);

		// Token: 0x060030BD RID: 12477
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_LeaveMatchDuringTheirTurn(HandleRef self, IntPtr match, TurnBasedMultiplayerManager.MultiplayerStatusCallback callback, IntPtr callback_arg);

		// Token: 0x060030BE RID: 12478
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_TurnBasedMatchResponse_Dispose(HandleRef self);

		// Token: 0x060030BF RID: 12479
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.MultiplayerStatus TurnBasedMultiplayerManager_TurnBasedMatchResponse_GetStatus(HandleRef self);

		// Token: 0x060030C0 RID: 12480
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMultiplayerManager_TurnBasedMatchResponse_GetMatch(HandleRef self);

		// Token: 0x060030C1 RID: 12481
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_TurnBasedMatchesResponse_Dispose(HandleRef self);

		// Token: 0x060030C2 RID: 12482
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.MultiplayerStatus TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetStatus(HandleRef self);

		// Token: 0x060030C3 RID: 12483
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetInvitations_Length(HandleRef self);

		// Token: 0x060030C4 RID: 12484
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetInvitations_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x060030C5 RID: 12485
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetMyTurnMatches_Length(HandleRef self);

		// Token: 0x060030C6 RID: 12486
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetMyTurnMatches_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x060030C7 RID: 12487
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetTheirTurnMatches_Length(HandleRef self);

		// Token: 0x060030C8 RID: 12488
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetTheirTurnMatches_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x060030C9 RID: 12489
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetCompletedMatches_Length(HandleRef self);

		// Token: 0x060030CA RID: 12490
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetCompletedMatches_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x060030CB RID: 12491
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_MatchInboxUIResponse_Dispose(HandleRef self);

		// Token: 0x060030CC RID: 12492
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.UIStatus TurnBasedMultiplayerManager_MatchInboxUIResponse_GetStatus(HandleRef self);

		// Token: 0x060030CD RID: 12493
		[DllImport("gpg")]
		internal static extern IntPtr TurnBasedMultiplayerManager_MatchInboxUIResponse_GetMatch(HandleRef self);

		// Token: 0x060030CE RID: 12494
		[DllImport("gpg")]
		internal static extern void TurnBasedMultiplayerManager_PlayerSelectUIResponse_Dispose(HandleRef self);

		// Token: 0x060030CF RID: 12495
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.UIStatus TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetStatus(HandleRef self);

		// Token: 0x060030D0 RID: 12496
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_Length(HandleRef self);

		// Token: 0x060030D1 RID: 12497
		[DllImport("gpg")]
		internal static extern UIntPtr TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_GetElement(HandleRef self, UIntPtr index, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x060030D2 RID: 12498
		[DllImport("gpg")]
		internal static extern uint TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMinimumAutomatchingPlayers(HandleRef self);

		// Token: 0x060030D3 RID: 12499
		[DllImport("gpg")]
		internal static extern uint TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMaximumAutomatchingPlayers(HandleRef self);

		// Token: 0x02000697 RID: 1687
		// (Invoke) Token: 0x060030D5 RID: 12501
		internal delegate void TurnBasedMatchCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000698 RID: 1688
		// (Invoke) Token: 0x060030D9 RID: 12505
		internal delegate void MultiplayerStatusCallback(CommonErrorStatus.MultiplayerStatus arg0, IntPtr arg1);

		// Token: 0x02000699 RID: 1689
		// (Invoke) Token: 0x060030DD RID: 12509
		internal delegate void TurnBasedMatchesCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x0200069A RID: 1690
		// (Invoke) Token: 0x060030E1 RID: 12513
		internal delegate void MatchInboxUICallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x0200069B RID: 1691
		// (Invoke) Token: 0x060030E5 RID: 12517
		internal delegate void PlayerSelectUICallback(IntPtr arg0, IntPtr arg1);
	}
}
