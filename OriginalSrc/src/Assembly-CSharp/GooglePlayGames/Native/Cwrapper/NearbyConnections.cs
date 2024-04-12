using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000657 RID: 1623
	internal static class NearbyConnections
	{
		// Token: 0x06002F2F RID: 12079
		[DllImport("gpg")]
		internal static extern void NearbyConnections_StartDiscovery(HandleRef self, string service_id, long duration, IntPtr helper);

		// Token: 0x06002F30 RID: 12080
		[DllImport("gpg")]
		internal static extern void NearbyConnections_RejectConnectionRequest(HandleRef self, string remote_endpoint_id);

		// Token: 0x06002F31 RID: 12081
		[DllImport("gpg")]
		internal static extern void NearbyConnections_Disconnect(HandleRef self, string remote_endpoint_id);

		// Token: 0x06002F32 RID: 12082
		[DllImport("gpg")]
		internal static extern void NearbyConnections_SendUnreliableMessage(HandleRef self, string remote_endpoint_id, byte[] payload, UIntPtr payload_size);

		// Token: 0x06002F33 RID: 12083
		[DllImport("gpg")]
		internal static extern void NearbyConnections_StopAdvertising(HandleRef self);

		// Token: 0x06002F34 RID: 12084
		[DllImport("gpg")]
		internal static extern void NearbyConnections_Dispose(HandleRef self);

		// Token: 0x06002F35 RID: 12085
		[DllImport("gpg")]
		internal static extern void NearbyConnections_SendReliableMessage(HandleRef self, string remote_endpoint_id, byte[] payload, UIntPtr payload_size);

		// Token: 0x06002F36 RID: 12086
		[DllImport("gpg")]
		internal static extern void NearbyConnections_StopDiscovery(HandleRef self, string service_id);

		// Token: 0x06002F37 RID: 12087
		[DllImport("gpg")]
		internal static extern void NearbyConnections_SendConnectionRequest(HandleRef self, string name, string remote_endpoint_id, byte[] payload, UIntPtr payload_size, NearbyConnectionTypes.ConnectionResponseCallback callback, IntPtr callback_arg, IntPtr helper);

		// Token: 0x06002F38 RID: 12088
		[DllImport("gpg")]
		internal static extern void NearbyConnections_StartAdvertising(HandleRef self, string name, IntPtr[] app_identifiers, UIntPtr app_identifiers_size, long duration, NearbyConnectionTypes.StartAdvertisingCallback start_advertising_callback, IntPtr start_advertising_callback_arg, NearbyConnectionTypes.ConnectionRequestCallback request_callback, IntPtr request_callback_arg);

		// Token: 0x06002F39 RID: 12089
		[DllImport("gpg")]
		internal static extern void NearbyConnections_Stop(HandleRef self);

		// Token: 0x06002F3A RID: 12090
		[DllImport("gpg")]
		internal static extern void NearbyConnections_AcceptConnectionRequest(HandleRef self, string remote_endpoint_id, byte[] payload, UIntPtr payload_size, IntPtr helper);
	}
}
