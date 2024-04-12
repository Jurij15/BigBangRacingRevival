using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020005C3 RID: 1475
	public enum ResponseStatus
	{
		// Token: 0x04003039 RID: 12345
		Success = 1,
		// Token: 0x0400303A RID: 12346
		SuccessWithStale,
		// Token: 0x0400303B RID: 12347
		LicenseCheckFailed = -1,
		// Token: 0x0400303C RID: 12348
		InternalError = -2,
		// Token: 0x0400303D RID: 12349
		NotAuthorized = -3,
		// Token: 0x0400303E RID: 12350
		VersionUpdateRequired = -4,
		// Token: 0x0400303F RID: 12351
		Timeout = -5
	}
}
