using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020005F2 RID: 1522
	public enum SelectUIStatus
	{
		// Token: 0x040030FA RID: 12538
		SavedGameSelected = 1,
		// Token: 0x040030FB RID: 12539
		UserClosedUI,
		// Token: 0x040030FC RID: 12540
		InternalError = -1,
		// Token: 0x040030FD RID: 12541
		TimeoutError = -2,
		// Token: 0x040030FE RID: 12542
		AuthenticationError = -3,
		// Token: 0x040030FF RID: 12543
		BadInputError = -4
	}
}
