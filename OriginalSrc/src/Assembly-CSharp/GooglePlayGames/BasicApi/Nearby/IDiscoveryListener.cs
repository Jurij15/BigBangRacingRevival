using System;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020005EA RID: 1514
	public interface IDiscoveryListener
	{
		// Token: 0x06002C0F RID: 11279
		void OnEndpointFound(EndpointDetails discoveredEndpoint);

		// Token: 0x06002C10 RID: 11280
		void OnEndpointLost(string lostEndpointId);
	}
}
