using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020005CC RID: 1484
	public class CommonTypesUtil
	{
		// Token: 0x06002B0F RID: 11023 RVA: 0x001BCE3E File Offset: 0x001BB23E
		public static bool StatusIsSuccess(ResponseStatus status)
		{
			return status > (ResponseStatus)0;
		}
	}
}
