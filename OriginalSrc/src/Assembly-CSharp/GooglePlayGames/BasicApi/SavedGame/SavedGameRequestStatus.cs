using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020005F1 RID: 1521
	public enum SavedGameRequestStatus
	{
		// Token: 0x040030F4 RID: 12532
		Success = 1,
		// Token: 0x040030F5 RID: 12533
		TimeoutError = -1,
		// Token: 0x040030F6 RID: 12534
		InternalError = -2,
		// Token: 0x040030F7 RID: 12535
		AuthenticationError = -3,
		// Token: 0x040030F8 RID: 12536
		BadInputError = -4
	}
}
