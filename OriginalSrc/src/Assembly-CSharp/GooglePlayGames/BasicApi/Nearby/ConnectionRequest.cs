using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020005E3 RID: 1507
	public struct ConnectionRequest
	{
		// Token: 0x06002BD9 RID: 11225 RVA: 0x001BDA47 File Offset: 0x001BBE47
		public ConnectionRequest(string remoteEndpointId, string remoteEndpointName, string serviceId, byte[] payload)
		{
			Logger.d("Constructing ConnectionRequest");
			this.mRemoteEndpoint = new EndpointDetails(remoteEndpointId, remoteEndpointName, serviceId);
			this.mPayload = Misc.CheckNotNull<byte[]>(payload);
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x001BDA6E File Offset: 0x001BBE6E
		public EndpointDetails RemoteEndpoint
		{
			get
			{
				return this.mRemoteEndpoint;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x001BDA76 File Offset: 0x001BBE76
		public byte[] Payload
		{
			get
			{
				return this.mPayload;
			}
		}

		// Token: 0x040030B0 RID: 12464
		private readonly EndpointDetails mRemoteEndpoint;

		// Token: 0x040030B1 RID: 12465
		private readonly byte[] mPayload;
	}
}
