using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020005C4 RID: 1476
	public enum UIStatus
	{
		// Token: 0x04003041 RID: 12353
		Valid = 1,
		// Token: 0x04003042 RID: 12354
		InternalError = -2,
		// Token: 0x04003043 RID: 12355
		NotAuthorized = -3,
		// Token: 0x04003044 RID: 12356
		VersionUpdateRequired = -4,
		// Token: 0x04003045 RID: 12357
		Timeout = -5,
		// Token: 0x04003046 RID: 12358
		UserClosedUI = -6,
		// Token: 0x04003047 RID: 12359
		UiBusy = -12,
		// Token: 0x04003048 RID: 12360
		LeftRoom = -18
	}
}
