using System;
using Com.Google.Android.Gms.Common.Api;
using Google.Developers;

namespace GooglePlayGames.Android
{
	// Token: 0x0200060D RID: 1549
	internal class TokenResult : JavaObjWrapper, Result
	{
		// Token: 0x06002D6C RID: 11628 RVA: 0x001C179D File Offset: 0x001BFB9D
		public TokenResult(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x001C17A8 File Offset: 0x001BFBA8
		public Status getStatus()
		{
			IntPtr intPtr = base.InvokeCall<IntPtr>("getStatus", "()Lcom/google/android/gms/common/api/Status;", new object[0]);
			return new Status(intPtr);
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x001C17D2 File Offset: 0x001BFBD2
		public int getStatusCode()
		{
			return base.InvokeCall<int>("getStatusCode", "()I", new object[0]);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x001C17EA File Offset: 0x001BFBEA
		public string getAuthCode()
		{
			return base.InvokeCall<string>("getAuthCode", "()Ljava/lang/String;", new object[0]);
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x001C1802 File Offset: 0x001BFC02
		public string getEmail()
		{
			return base.InvokeCall<string>("getEmail", "()Ljava/lang/String;", new object[0]);
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x001C181A File Offset: 0x001BFC1A
		public string getIdToken()
		{
			return base.InvokeCall<string>("getIdToken", "()Ljava/lang/String;", new object[0]);
		}
	}
}
