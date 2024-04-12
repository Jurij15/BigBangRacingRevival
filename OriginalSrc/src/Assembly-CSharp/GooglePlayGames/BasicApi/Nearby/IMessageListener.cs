using System;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020005E9 RID: 1513
	public interface IMessageListener
	{
		// Token: 0x06002C0D RID: 11277
		void OnMessageReceived(string remoteEndpointId, byte[] data, bool isReliableMessage);

		// Token: 0x06002C0E RID: 11278
		void OnRemoteEndpointDisconnected(string remoteEndpointId);
	}
}
