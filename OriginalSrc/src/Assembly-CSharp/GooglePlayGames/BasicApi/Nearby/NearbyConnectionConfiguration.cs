using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020005EC RID: 1516
	public struct NearbyConnectionConfiguration
	{
		// Token: 0x06002C11 RID: 11281 RVA: 0x001BDC5D File Offset: 0x001BC05D
		public NearbyConnectionConfiguration(Action<InitializationStatus> callback, long localClientId)
		{
			this.mInitializationCallback = Misc.CheckNotNull<Action<InitializationStatus>>(callback);
			this.mLocalClientId = localClientId;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x001BDC72 File Offset: 0x001BC072
		public long LocalClientId
		{
			get
			{
				return this.mLocalClientId;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x001BDC7A File Offset: 0x001BC07A
		public Action<InitializationStatus> InitializationCallback
		{
			get
			{
				return this.mInitializationCallback;
			}
		}

		// Token: 0x040030C5 RID: 12485
		public const int MaxUnreliableMessagePayloadLength = 1168;

		// Token: 0x040030C6 RID: 12486
		public const int MaxReliableMessagePayloadLength = 4096;

		// Token: 0x040030C7 RID: 12487
		private readonly Action<InitializationStatus> mInitializationCallback;

		// Token: 0x040030C8 RID: 12488
		private readonly long mLocalClientId;
	}
}
