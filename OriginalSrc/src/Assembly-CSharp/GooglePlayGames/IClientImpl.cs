using System;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.PInvoke;

namespace GooglePlayGames
{
	// Token: 0x02000620 RID: 1568
	internal interface IClientImpl
	{
		// Token: 0x06002E21 RID: 11809
		PlatformConfiguration CreatePlatformConfiguration(PlayGamesClientConfiguration clientConfig);

		// Token: 0x06002E22 RID: 11810
		TokenClient CreateTokenClient(bool reset);

		// Token: 0x06002E23 RID: 11811
		void GetPlayerStats(IntPtr apiClientPtr, Action<CommonStatusCodes, GooglePlayGames.BasicApi.PlayerStats> callback);

		// Token: 0x06002E24 RID: 11812
		void SetGravityForPopups(IntPtr apiClient, Gravity gravity);
	}
}
