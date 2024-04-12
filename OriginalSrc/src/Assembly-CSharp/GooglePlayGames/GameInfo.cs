using System;

namespace GooglePlayGames
{
	// Token: 0x020005FE RID: 1534
	public static class GameInfo
	{
		// Token: 0x06002C93 RID: 11411 RVA: 0x001BE3AB File Offset: 0x001BC7AB
		public static bool ApplicationIdInitialized()
		{
			return !string.IsNullOrEmpty("717089705710") && !"717089705710".Equals(GameInfo.ToEscapedToken("APP_ID"));
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x001BE3D6 File Offset: 0x001BC7D6
		public static bool IosClientIdInitialized()
		{
			return !string.IsNullOrEmpty("__IOS_CLIENTID__") && !"__IOS_CLIENTID__".Equals(GameInfo.ToEscapedToken("IOS_CLIENTID"));
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x001BE401 File Offset: 0x001BC801
		public static bool WebClientIdInitialized()
		{
			return !string.IsNullOrEmpty(string.Empty) && !string.Empty.Equals(GameInfo.ToEscapedToken("WEB_CLIENTID"));
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x001BE42C File Offset: 0x001BC82C
		public static bool NearbyConnectionsInitialized()
		{
			return !string.IsNullOrEmpty(string.Empty) && !string.Empty.Equals(GameInfo.ToEscapedToken("NEARBY_SERVICE_ID"));
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x001BE457 File Offset: 0x001BC857
		private static string ToEscapedToken(string token)
		{
			return string.Format("__{0}__", token);
		}

		// Token: 0x0400311A RID: 12570
		private const string UnescapedApplicationId = "APP_ID";

		// Token: 0x0400311B RID: 12571
		private const string UnescapedIosClientId = "IOS_CLIENTID";

		// Token: 0x0400311C RID: 12572
		private const string UnescapedWebClientId = "WEB_CLIENTID";

		// Token: 0x0400311D RID: 12573
		private const string UnescapedNearbyServiceId = "NEARBY_SERVICE_ID";

		// Token: 0x0400311E RID: 12574
		public const string ApplicationId = "717089705710";

		// Token: 0x0400311F RID: 12575
		public const string IosClientId = "__IOS_CLIENTID__";

		// Token: 0x04003120 RID: 12576
		public const string WebClientId = "";

		// Token: 0x04003121 RID: 12577
		public const string NearbyConnectionServiceId = "";
	}
}
