using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020005E4 RID: 1508
	public struct ConnectionResponse
	{
		// Token: 0x06002BDC RID: 11228 RVA: 0x001BDA7E File Offset: 0x001BBE7E
		private ConnectionResponse(long localClientId, string remoteEndpointId, ConnectionResponse.Status code, byte[] payload)
		{
			this.mLocalClientId = localClientId;
			this.mRemoteEndpointId = Misc.CheckNotNull<string>(remoteEndpointId);
			this.mResponseStatus = code;
			this.mPayload = Misc.CheckNotNull<byte[]>(payload);
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x001BDAA7 File Offset: 0x001BBEA7
		public long LocalClientId
		{
			get
			{
				return this.mLocalClientId;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x001BDAAF File Offset: 0x001BBEAF
		public string RemoteEndpointId
		{
			get
			{
				return this.mRemoteEndpointId;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x001BDAB7 File Offset: 0x001BBEB7
		public ConnectionResponse.Status ResponseStatus
		{
			get
			{
				return this.mResponseStatus;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x001BDABF File Offset: 0x001BBEBF
		public byte[] Payload
		{
			get
			{
				return this.mPayload;
			}
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x001BDAC7 File Offset: 0x001BBEC7
		public static ConnectionResponse Rejected(long localClientId, string remoteEndpointId)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.Rejected, ConnectionResponse.EmptyPayload);
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x001BDAD6 File Offset: 0x001BBED6
		public static ConnectionResponse NetworkNotConnected(long localClientId, string remoteEndpointId)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorNetworkNotConnected, ConnectionResponse.EmptyPayload);
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x001BDAE5 File Offset: 0x001BBEE5
		public static ConnectionResponse InternalError(long localClientId, string remoteEndpointId)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorInternal, ConnectionResponse.EmptyPayload);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x001BDAF4 File Offset: 0x001BBEF4
		public static ConnectionResponse EndpointNotConnected(long localClientId, string remoteEndpointId)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorEndpointNotConnected, ConnectionResponse.EmptyPayload);
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x001BDB03 File Offset: 0x001BBF03
		public static ConnectionResponse Accepted(long localClientId, string remoteEndpointId, byte[] payload)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.Accepted, payload);
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x001BDB0E File Offset: 0x001BBF0E
		public static ConnectionResponse AlreadyConnected(long localClientId, string remoteEndpointId)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorAlreadyConnected, ConnectionResponse.EmptyPayload);
		}

		// Token: 0x040030B2 RID: 12466
		private static readonly byte[] EmptyPayload = new byte[0];

		// Token: 0x040030B3 RID: 12467
		private readonly long mLocalClientId;

		// Token: 0x040030B4 RID: 12468
		private readonly string mRemoteEndpointId;

		// Token: 0x040030B5 RID: 12469
		private readonly ConnectionResponse.Status mResponseStatus;

		// Token: 0x040030B6 RID: 12470
		private readonly byte[] mPayload;

		// Token: 0x020005E5 RID: 1509
		public enum Status
		{
			// Token: 0x040030B8 RID: 12472
			Accepted,
			// Token: 0x040030B9 RID: 12473
			Rejected,
			// Token: 0x040030BA RID: 12474
			ErrorInternal,
			// Token: 0x040030BB RID: 12475
			ErrorNetworkNotConnected,
			// Token: 0x040030BC RID: 12476
			ErrorEndpointNotConnected,
			// Token: 0x040030BD RID: 12477
			ErrorAlreadyConnected
		}
	}
}
