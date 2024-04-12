using System;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x020005DE RID: 1502
	public interface RealTimeMultiplayerListener
	{
		// Token: 0x06002BBD RID: 11197
		void OnRoomSetupProgress(float percent);

		// Token: 0x06002BBE RID: 11198
		void OnRoomConnected(bool success);

		// Token: 0x06002BBF RID: 11199
		void OnLeftRoom();

		// Token: 0x06002BC0 RID: 11200
		void OnParticipantLeft(Participant participant);

		// Token: 0x06002BC1 RID: 11201
		void OnPeersConnected(string[] participantIds);

		// Token: 0x06002BC2 RID: 11202
		void OnPeersDisconnected(string[] participantIds);

		// Token: 0x06002BC3 RID: 11203
		void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data);
	}
}
