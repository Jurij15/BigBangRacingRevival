using System;
using Google.Developers;

namespace Com.Google.Android.Gms.Common.Api
{
	// Token: 0x02000611 RID: 1553
	public class GoogleApiClient : JavaObjWrapper
	{
		// Token: 0x06002D87 RID: 11655 RVA: 0x001C1879 File Offset: 0x001BFC79
		public GoogleApiClient(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x001C1882 File Offset: 0x001BFC82
		public GoogleApiClient()
			: base("com.google.android.gms.common.api.GoogleApiClient")
		{
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x001C188F File Offset: 0x001BFC8F
		public object getContext()
		{
			return base.InvokeCall<object>("getContext", "()Landroid/content/Context;", new object[0]);
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x001C18A7 File Offset: 0x001BFCA7
		public void connect()
		{
			base.InvokeCallVoid("connect", "()V", new object[0]);
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x001C18BF File Offset: 0x001BFCBF
		public void disconnect()
		{
			base.InvokeCallVoid("disconnect", "()V", new object[0]);
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x001C18D7 File Offset: 0x001BFCD7
		public void dump(string arg_string_1, object arg_object_2, object arg_object_3, string[] arg_string_4)
		{
			base.InvokeCallVoid("dump", "(Ljava/lang/String;Ljava/io/FileDescriptor;Ljava/io/PrintWriter;[Ljava/lang/String;)V", new object[] { arg_string_1, arg_object_2, arg_object_3, arg_string_4 });
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x001C1900 File Offset: 0x001BFD00
		public ConnectionResult blockingConnect(long arg_long_1, object arg_object_2)
		{
			return base.InvokeCall<ConnectionResult>("blockingConnect", "(JLjava/util/concurrent/TimeUnit;)Lcom/google/android/gms/common/ConnectionResult;", new object[] { arg_long_1, arg_object_2 });
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x001C1925 File Offset: 0x001BFD25
		public ConnectionResult blockingConnect()
		{
			return base.InvokeCall<ConnectionResult>("blockingConnect", "()Lcom/google/android/gms/common/ConnectionResult;", new object[0]);
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x001C193D File Offset: 0x001BFD3D
		public PendingResult<Status> clearDefaultAccountAndReconnect()
		{
			return base.InvokeCall<PendingResult<Status>>("clearDefaultAccountAndReconnect", "()Lcom/google/android/gms/common/api/PendingResult;", new object[0]);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x001C1955 File Offset: 0x001BFD55
		public ConnectionResult getConnectionResult(object arg_object_1)
		{
			return base.InvokeCall<ConnectionResult>("getConnectionResult", "(Lcom/google/android/gms/common/api/Api;)Lcom/google/android/gms/common/ConnectionResult;", new object[] { arg_object_1 });
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x001C1971 File Offset: 0x001BFD71
		public int getSessionId()
		{
			return base.InvokeCall<int>("getSessionId", "()I", new object[0]);
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x001C1989 File Offset: 0x001BFD89
		public bool isConnecting()
		{
			return base.InvokeCall<bool>("isConnecting", "()Z", new object[0]);
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x001C19A1 File Offset: 0x001BFDA1
		public bool isConnectionCallbacksRegistered(object arg_object_1)
		{
			return base.InvokeCall<bool>("isConnectionCallbacksRegistered", "(Lcom/google/android/gms/common/api/GoogleApiClient$ConnectionCallbacks;)Z", new object[] { arg_object_1 });
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x001C19BD File Offset: 0x001BFDBD
		public bool isConnectionFailedListenerRegistered(object arg_object_1)
		{
			return base.InvokeCall<bool>("isConnectionFailedListenerRegistered", "(Lcom/google/android/gms/common/api/GoogleApiClient$OnConnectionFailedListener;)Z", new object[] { arg_object_1 });
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x001C19D9 File Offset: 0x001BFDD9
		public void reconnect()
		{
			base.InvokeCallVoid("reconnect", "()V", new object[0]);
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x001C19F1 File Offset: 0x001BFDF1
		public void registerConnectionCallbacks(object arg_object_1)
		{
			base.InvokeCallVoid("registerConnectionCallbacks", "(Lcom/google/android/gms/common/api/GoogleApiClient$ConnectionCallbacks;)V", new object[] { arg_object_1 });
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x001C1A0D File Offset: 0x001BFE0D
		public void registerConnectionFailedListener(object arg_object_1)
		{
			base.InvokeCallVoid("registerConnectionFailedListener", "(Lcom/google/android/gms/common/api/GoogleApiClient$OnConnectionFailedListener;)V", new object[] { arg_object_1 });
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x001C1A29 File Offset: 0x001BFE29
		public void stopAutoManage(object arg_object_1)
		{
			base.InvokeCallVoid("stopAutoManage", "(Landroid/support/v4/app/FragmentActivity;)V", new object[] { arg_object_1 });
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x001C1A45 File Offset: 0x001BFE45
		public void unregisterConnectionCallbacks(object arg_object_1)
		{
			base.InvokeCallVoid("unregisterConnectionCallbacks", "(Lcom/google/android/gms/common/api/GoogleApiClient$ConnectionCallbacks;)V", new object[] { arg_object_1 });
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x001C1A61 File Offset: 0x001BFE61
		public void unregisterConnectionFailedListener(object arg_object_1)
		{
			base.InvokeCallVoid("unregisterConnectionFailedListener", "(Lcom/google/android/gms/common/api/GoogleApiClient$OnConnectionFailedListener;)V", new object[] { arg_object_1 });
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x001C1A7D File Offset: 0x001BFE7D
		public bool hasConnectedApi(object arg_object_1)
		{
			return base.InvokeCall<bool>("hasConnectedApi", "(Lcom/google/android/gms/common/api/Api;)Z", new object[] { arg_object_1 });
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x001C1A99 File Offset: 0x001BFE99
		public object getLooper()
		{
			return base.InvokeCall<object>("getLooper", "()Landroid/os/Looper;", new object[0]);
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x001C1AB1 File Offset: 0x001BFEB1
		public bool isConnected()
		{
			return base.InvokeCall<bool>("isConnected", "()Z", new object[0]);
		}

		// Token: 0x04003176 RID: 12662
		private const string CLASS_NAME = "com/google/android/gms/common/api/GoogleApiClient";
	}
}
