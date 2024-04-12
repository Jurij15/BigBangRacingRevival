using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020005E2 RID: 1506
	public struct AdvertisingResult
	{
		// Token: 0x06002BD5 RID: 11221 RVA: 0x001BDA17 File Offset: 0x001BBE17
		public AdvertisingResult(ResponseStatus status, string localEndpointName)
		{
			this.mStatus = status;
			this.mLocalEndpointName = Misc.CheckNotNull<string>(localEndpointName);
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x001BDA2C File Offset: 0x001BBE2C
		public bool Succeeded
		{
			get
			{
				return this.mStatus == ResponseStatus.Success;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x001BDA37 File Offset: 0x001BBE37
		public ResponseStatus Status
		{
			get
			{
				return this.mStatus;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x001BDA3F File Offset: 0x001BBE3F
		public string LocalEndpointName
		{
			get
			{
				return this.mLocalEndpointName;
			}
		}

		// Token: 0x040030AE RID: 12462
		private readonly ResponseStatus mStatus;

		// Token: 0x040030AF RID: 12463
		private readonly string mLocalEndpointName;
	}
}
