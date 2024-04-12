using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020005E7 RID: 1511
	public struct EndpointDetails
	{
		// Token: 0x06002BFA RID: 11258 RVA: 0x001BDC1F File Offset: 0x001BC01F
		public EndpointDetails(string endpointId, string name, string serviceId)
		{
			this.mEndpointId = Misc.CheckNotNull<string>(endpointId);
			this.mName = Misc.CheckNotNull<string>(name);
			this.mServiceId = Misc.CheckNotNull<string>(serviceId);
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x001BDC45 File Offset: 0x001BC045
		public string EndpointId
		{
			get
			{
				return this.mEndpointId;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x001BDC4D File Offset: 0x001BC04D
		public string Name
		{
			get
			{
				return this.mName;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x001BDC55 File Offset: 0x001BC055
		public string ServiceId
		{
			get
			{
				return this.mServiceId;
			}
		}

		// Token: 0x040030BE RID: 12478
		private readonly string mEndpointId;

		// Token: 0x040030BF RID: 12479
		private readonly string mName;

		// Token: 0x040030C0 RID: 12480
		private readonly string mServiceId;
	}
}
