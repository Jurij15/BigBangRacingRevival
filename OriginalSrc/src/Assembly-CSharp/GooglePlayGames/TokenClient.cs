using System;

namespace GooglePlayGames
{
	// Token: 0x0200072E RID: 1838
	internal interface TokenClient
	{
		// Token: 0x06003531 RID: 13617
		string GetEmail();

		// Token: 0x06003532 RID: 13618
		string GetAuthCode();

		// Token: 0x06003533 RID: 13619
		string GetIdToken();

		// Token: 0x06003534 RID: 13620
		void GetAnotherServerAuthCode(bool reAuthenticateIfNeeded, Action<string> callback);

		// Token: 0x06003535 RID: 13621
		void Signout();

		// Token: 0x06003536 RID: 13622
		void SetRequestAuthCode(bool flag, bool forceRefresh);

		// Token: 0x06003537 RID: 13623
		void SetRequestEmail(bool flag);

		// Token: 0x06003538 RID: 13624
		void SetRequestIdToken(bool flag);

		// Token: 0x06003539 RID: 13625
		void SetWebClientId(string webClientId);

		// Token: 0x0600353A RID: 13626
		void SetAccountName(string accountName);

		// Token: 0x0600353B RID: 13627
		void AddOauthScopes(string[] scopes);

		// Token: 0x0600353C RID: 13628
		void SetHidePopups(bool flag);

		// Token: 0x0600353D RID: 13629
		bool NeedsToRun();

		// Token: 0x0600353E RID: 13630
		void FetchTokens(Action<int> callback);
	}
}
