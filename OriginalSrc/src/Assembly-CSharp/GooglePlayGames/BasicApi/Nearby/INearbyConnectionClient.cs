using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020005E8 RID: 1512
	public interface INearbyConnectionClient
	{
		// Token: 0x06002BFE RID: 11262
		int MaxUnreliableMessagePayloadLength();

		// Token: 0x06002BFF RID: 11263
		int MaxReliableMessagePayloadLength();

		// Token: 0x06002C00 RID: 11264
		void SendReliable(List<string> recipientEndpointIds, byte[] payload);

		// Token: 0x06002C01 RID: 11265
		void SendUnreliable(List<string> recipientEndpointIds, byte[] payload);

		// Token: 0x06002C02 RID: 11266
		void StartAdvertising(string name, List<string> appIdentifiers, TimeSpan? advertisingDuration, Action<AdvertisingResult> resultCallback, Action<ConnectionRequest> connectionRequestCallback);

		// Token: 0x06002C03 RID: 11267
		void StopAdvertising();

		// Token: 0x06002C04 RID: 11268
		void SendConnectionRequest(string name, string remoteEndpointId, byte[] payload, Action<ConnectionResponse> responseCallback, IMessageListener listener);

		// Token: 0x06002C05 RID: 11269
		void AcceptConnectionRequest(string remoteEndpointId, byte[] payload, IMessageListener listener);

		// Token: 0x06002C06 RID: 11270
		void StartDiscovery(string serviceId, TimeSpan? advertisingTimeout, IDiscoveryListener listener);

		// Token: 0x06002C07 RID: 11271
		void StopDiscovery(string serviceId);

		// Token: 0x06002C08 RID: 11272
		void RejectConnectionRequest(string requestingEndpointId);

		// Token: 0x06002C09 RID: 11273
		void DisconnectFromEndpoint(string remoteEndpointId);

		// Token: 0x06002C0A RID: 11274
		void StopAllConnections();

		// Token: 0x06002C0B RID: 11275
		string GetAppBundleId();

		// Token: 0x06002C0C RID: 11276
		string GetServiceId();
	}
}
