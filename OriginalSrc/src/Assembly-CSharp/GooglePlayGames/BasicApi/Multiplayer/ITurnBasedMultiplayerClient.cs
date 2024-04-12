using System;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x020005D7 RID: 1495
	public interface ITurnBasedMultiplayerClient
	{
		// Token: 0x06002B93 RID: 11155
		void CreateQuickMatch(uint minOpponents, uint maxOpponents, uint variant, Action<bool, TurnBasedMatch> callback);

		// Token: 0x06002B94 RID: 11156
		void CreateQuickMatch(uint minOpponents, uint maxOpponents, uint variant, ulong exclusiveBitmask, Action<bool, TurnBasedMatch> callback);

		// Token: 0x06002B95 RID: 11157
		void CreateWithInvitationScreen(uint minOpponents, uint maxOpponents, uint variant, Action<bool, TurnBasedMatch> callback);

		// Token: 0x06002B96 RID: 11158
		void CreateWithInvitationScreen(uint minOpponents, uint maxOpponents, uint variant, Action<UIStatus, TurnBasedMatch> callback);

		// Token: 0x06002B97 RID: 11159
		void GetAllInvitations(Action<Invitation[]> callback);

		// Token: 0x06002B98 RID: 11160
		void GetAllMatches(Action<TurnBasedMatch[]> callback);

		// Token: 0x06002B99 RID: 11161
		void AcceptFromInbox(Action<bool, TurnBasedMatch> callback);

		// Token: 0x06002B9A RID: 11162
		void AcceptInvitation(string invitationId, Action<bool, TurnBasedMatch> callback);

		// Token: 0x06002B9B RID: 11163
		void RegisterMatchDelegate(MatchDelegate del);

		// Token: 0x06002B9C RID: 11164
		void TakeTurn(TurnBasedMatch match, byte[] data, string pendingParticipantId, Action<bool> callback);

		// Token: 0x06002B9D RID: 11165
		int GetMaxMatchDataSize();

		// Token: 0x06002B9E RID: 11166
		void Finish(TurnBasedMatch match, byte[] data, MatchOutcome outcome, Action<bool> callback);

		// Token: 0x06002B9F RID: 11167
		void AcknowledgeFinished(TurnBasedMatch match, Action<bool> callback);

		// Token: 0x06002BA0 RID: 11168
		void Leave(TurnBasedMatch match, Action<bool> callback);

		// Token: 0x06002BA1 RID: 11169
		void LeaveDuringTurn(TurnBasedMatch match, string pendingParticipantId, Action<bool> callback);

		// Token: 0x06002BA2 RID: 11170
		void Cancel(TurnBasedMatch match, Action<bool> callback);

		// Token: 0x06002BA3 RID: 11171
		void Rematch(TurnBasedMatch match, Action<bool, TurnBasedMatch> callback);

		// Token: 0x06002BA4 RID: 11172
		void DeclineInvitation(string invitationId);
	}
}
