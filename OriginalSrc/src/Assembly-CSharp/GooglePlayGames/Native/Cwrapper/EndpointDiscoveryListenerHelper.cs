using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200063E RID: 1598
	internal static class EndpointDiscoveryListenerHelper
	{
		// Token: 0x06002E9C RID: 11932
		[DllImport("gpg")]
		internal static extern IntPtr EndpointDiscoveryListenerHelper_Construct();

		// Token: 0x06002E9D RID: 11933
		[DllImport("gpg")]
		internal static extern void EndpointDiscoveryListenerHelper_SetOnEndpointLostCallback(HandleRef self, EndpointDiscoveryListenerHelper.OnEndpointLostCallback callback, IntPtr callback_arg);

		// Token: 0x06002E9E RID: 11934
		[DllImport("gpg")]
		internal static extern void EndpointDiscoveryListenerHelper_SetOnEndpointFoundCallback(HandleRef self, EndpointDiscoveryListenerHelper.OnEndpointFoundCallback callback, IntPtr callback_arg);

		// Token: 0x06002E9F RID: 11935
		[DllImport("gpg")]
		internal static extern void EndpointDiscoveryListenerHelper_Dispose(HandleRef self);

		// Token: 0x0200063F RID: 1599
		// (Invoke) Token: 0x06002EA1 RID: 11937
		internal delegate void OnEndpointFoundCallback(long arg0, IntPtr arg1, IntPtr arg2);

		// Token: 0x02000640 RID: 1600
		// (Invoke) Token: 0x06002EA5 RID: 11941
		internal delegate void OnEndpointLostCallback(long arg0, string arg1, IntPtr arg2);
	}
}
