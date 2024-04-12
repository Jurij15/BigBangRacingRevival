using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200065D RID: 1629
	internal static class NearbyConnectionTypes
	{
		// Token: 0x06002F4A RID: 12106
		[DllImport("gpg")]
		internal static extern void AppIdentifier_Dispose(HandleRef self);

		// Token: 0x06002F4B RID: 12107
		[DllImport("gpg")]
		internal static extern UIntPtr AppIdentifier_GetIdentifier(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F4C RID: 12108
		[DllImport("gpg")]
		internal static extern void StartAdvertisingResult_Dispose(HandleRef self);

		// Token: 0x06002F4D RID: 12109
		[DllImport("gpg")]
		[return: MarshalAs(7)]
		internal static extern int StartAdvertisingResult_GetStatus(HandleRef self);

		// Token: 0x06002F4E RID: 12110
		[DllImport("gpg")]
		internal static extern UIntPtr StartAdvertisingResult_GetLocalEndpointName(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F4F RID: 12111
		[DllImport("gpg")]
		internal static extern void EndpointDetails_Dispose(HandleRef self);

		// Token: 0x06002F50 RID: 12112
		[DllImport("gpg")]
		internal static extern UIntPtr EndpointDetails_GetEndpointId(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F51 RID: 12113
		[DllImport("gpg")]
		internal static extern UIntPtr EndpointDetails_GetName(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F52 RID: 12114
		[DllImport("gpg")]
		internal static extern UIntPtr EndpointDetails_GetServiceId(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F53 RID: 12115
		[DllImport("gpg")]
		internal static extern void ConnectionRequest_Dispose(HandleRef self);

		// Token: 0x06002F54 RID: 12116
		[DllImport("gpg")]
		internal static extern UIntPtr ConnectionRequest_GetRemoteEndpointId(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F55 RID: 12117
		[DllImport("gpg")]
		internal static extern UIntPtr ConnectionRequest_GetRemoteEndpointName(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F56 RID: 12118
		[DllImport("gpg")]
		internal static extern UIntPtr ConnectionRequest_GetPayload(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F57 RID: 12119
		[DllImport("gpg")]
		internal static extern void ConnectionResponse_Dispose(HandleRef self);

		// Token: 0x06002F58 RID: 12120
		[DllImport("gpg")]
		internal static extern UIntPtr ConnectionResponse_GetRemoteEndpointId(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06002F59 RID: 12121
		[DllImport("gpg")]
		internal static extern NearbyConnectionTypes.ConnectionResponse_ResponseCode ConnectionResponse_GetStatus(HandleRef self);

		// Token: 0x06002F5A RID: 12122
		[DllImport("gpg")]
		internal static extern UIntPtr ConnectionResponse_GetPayload(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x0200065E RID: 1630
		internal enum ConnectionResponse_ResponseCode
		{
			// Token: 0x040031C3 RID: 12739
			ACCEPTED = 1,
			// Token: 0x040031C4 RID: 12740
			REJECTED,
			// Token: 0x040031C5 RID: 12741
			ERROR_INTERNAL = -1,
			// Token: 0x040031C6 RID: 12742
			ERROR_NETWORK_NOT_CONNECTED = -2,
			// Token: 0x040031C7 RID: 12743
			ERROR_ENDPOINT_ALREADY_CONNECTED = -3,
			// Token: 0x040031C8 RID: 12744
			ERROR_ENDPOINT_NOT_CONNECTED = -4
		}

		// Token: 0x0200065F RID: 1631
		// (Invoke) Token: 0x06002F5C RID: 12124
		internal delegate void ConnectionRequestCallback(long arg0, IntPtr arg1, IntPtr arg2);

		// Token: 0x02000660 RID: 1632
		// (Invoke) Token: 0x06002F60 RID: 12128
		internal delegate void StartAdvertisingCallback(long arg0, IntPtr arg1, IntPtr arg2);

		// Token: 0x02000661 RID: 1633
		// (Invoke) Token: 0x06002F64 RID: 12132
		internal delegate void ConnectionResponseCallback(long arg0, IntPtr arg1, IntPtr arg2);
	}
}
