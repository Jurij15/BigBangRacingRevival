using System;
using Com.Google.Android.Gms.Common.Api;
using Google.Developers;

namespace Com.Google.Android.Gms.Games
{
	// Token: 0x02000619 RID: 1561
	public class Games_BaseGamesApiMethodImpl<R> : JavaObjWrapper where R : Result
	{
		// Token: 0x06002E00 RID: 11776 RVA: 0x001C2337 File Offset: 0x001C0737
		public Games_BaseGamesApiMethodImpl(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x001C2340 File Offset: 0x001C0740
		public Games_BaseGamesApiMethodImpl(GoogleApiClient arg_GoogleApiClient_1)
		{
			base.CreateInstance("com/google/android/gms/games/Games$BaseGamesApiMethodImpl", new object[] { arg_GoogleApiClient_1 });
		}

		// Token: 0x0400317C RID: 12668
		private const string CLASS_NAME = "com/google/android/gms/games/Games$BaseGamesApiMethodImpl";
	}
}
