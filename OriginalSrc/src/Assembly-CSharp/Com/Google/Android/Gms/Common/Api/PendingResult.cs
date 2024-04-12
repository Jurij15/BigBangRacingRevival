using System;
using Google.Developers;

namespace Com.Google.Android.Gms.Common.Api
{
	// Token: 0x02000612 RID: 1554
	public class PendingResult<R> : JavaObjWrapper where R : Result
	{
		// Token: 0x06002D9E RID: 11678 RVA: 0x001C1AC9 File Offset: 0x001BFEC9
		public PendingResult(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x001C1AD2 File Offset: 0x001BFED2
		public PendingResult()
			: base("com.google.android.gms.common.api.PendingResult")
		{
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x001C1ADF File Offset: 0x001BFEDF
		public R await(long arg_long_1, object arg_object_2)
		{
			return base.InvokeCall<R>("await", "(JLjava/util/concurrent/TimeUnit;)Lcom/google/android/gms/common/api/Result;", new object[] { arg_long_1, arg_object_2 });
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x001C1B04 File Offset: 0x001BFF04
		public R await()
		{
			return base.InvokeCall<R>("await", "()Lcom/google/android/gms/common/api/Result;", new object[0]);
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x001C1B1C File Offset: 0x001BFF1C
		public bool isCanceled()
		{
			return base.InvokeCall<bool>("isCanceled", "()Z", new object[0]);
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x001C1B34 File Offset: 0x001BFF34
		public void cancel()
		{
			base.InvokeCallVoid("cancel", "()V", new object[0]);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x001C1B4C File Offset: 0x001BFF4C
		public void setResultCallback(ResultCallback<R> arg_ResultCallback_1)
		{
			base.InvokeCallVoid("setResultCallback", "(Lcom/google/android/gms/common/api/ResultCallback;)V", new object[] { arg_ResultCallback_1 });
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x001C1B68 File Offset: 0x001BFF68
		public void setResultCallback(ResultCallback<R> arg_ResultCallback_1, long arg_long_2, object arg_object_3)
		{
			base.InvokeCallVoid("setResultCallback", "(Lcom/google/android/gms/common/api/ResultCallback;JLjava/util/concurrent/TimeUnit;)V", new object[] { arg_ResultCallback_1, arg_long_2, arg_object_3 });
		}

		// Token: 0x04003177 RID: 12663
		private const string CLASS_NAME = "com/google/android/gms/common/api/PendingResult";
	}
}
