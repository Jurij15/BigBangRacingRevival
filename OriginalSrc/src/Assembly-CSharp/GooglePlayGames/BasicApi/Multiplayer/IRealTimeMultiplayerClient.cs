using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x020005D6 RID: 1494
	public interface IRealTimeMultiplayerClient
	{
		// Token: 0x06002B81 RID: 11137
		void CreateQuickGame(uint minOpponents, uint maxOpponents, uint variant, RealTimeMultiplayerListener listener);

		// Token: 0x06002B82 RID: 11138
		void CreateQuickGame(uint minOpponents, uint maxOpponents, uint variant, ulong exclusiveBitMask, RealTimeMultiplayerListener listener);

		// Token: 0x06002B83 RID: 11139
		void CreateWithInvitationScreen(uint minOpponents, uint maxOppponents, uint variant, RealTimeMultiplayerListener listener);

		// Token: 0x06002B84 RID: 11140
		void ShowWaitingRoomUI();

		// Token: 0x06002B85 RID: 11141
		void GetAllInvitations(Action<Invitation[]> callback);

		// Token: 0x06002B86 RID: 11142
		void AcceptFromInbox(RealTimeMultiplayerListener listener);

		// Token: 0x06002B87 RID: 11143
		void AcceptInvitation(string invitationId, RealTimeMultiplayerListener listener);

		// Token: 0x06002B88 RID: 11144
		void SendMessageToAll(bool reliable, byte[] data);

		// Token: 0x06002B89 RID: 11145
		void SendMessageToAll(bool reliable, byte[] data, int offset, int length);

		// Token: 0x06002B8A RID: 11146
		void SendMessage(bool reliable, string participantId, byte[] data);

		// Token: 0x06002B8B RID: 11147
		void SendMessage(bool reliable, string participantId, byte[] data, int offset, int length);

		// Token: 0x06002B8C RID: 11148
		List<Participant> GetConnectedParticipants();

		// Token: 0x06002B8D RID: 11149
		Participant GetSelf();

		// Token: 0x06002B8E RID: 11150
		Participant GetParticipant(string participantId);

		// Token: 0x06002B8F RID: 11151
		Invitation GetInvitation();

		// Token: 0x06002B90 RID: 11152
		void LeaveRoom();

		// Token: 0x06002B91 RID: 11153
		bool IsRoomConnected();

		// Token: 0x06002B92 RID: 11154
		void DeclineInvitation(string invitationId);
	}
}
