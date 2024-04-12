using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006EC RID: 1772
	internal class NativeConnectionRequest : BaseReferenceHolder
	{
		// Token: 0x0600330D RID: 13069 RVA: 0x001CB67C File Offset: 0x001C9A7C
		internal NativeConnectionRequest(IntPtr pointer)
			: base(pointer)
		{
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x001CB685 File Offset: 0x001C9A85
		internal string RemoteEndpointId()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_arg, UIntPtr out_size) => NearbyConnectionTypes.ConnectionRequest_GetRemoteEndpointId(base.SelfPtr(), out_arg, out_size));
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x001CB698 File Offset: 0x001C9A98
		internal string RemoteEndpointName()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_arg, UIntPtr out_size) => NearbyConnectionTypes.ConnectionRequest_GetRemoteEndpointName(base.SelfPtr(), out_arg, out_size));
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x001CB6AB File Offset: 0x001C9AAB
		internal byte[] Payload()
		{
			return PInvokeUtilities.OutParamsToArray<byte>((byte[] out_arg, UIntPtr out_size) => NearbyConnectionTypes.ConnectionRequest_GetPayload(base.SelfPtr(), out_arg, out_size));
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x001CB6BE File Offset: 0x001C9ABE
		protected override void CallDispose(HandleRef selfPointer)
		{
			NearbyConnectionTypes.ConnectionRequest_Dispose(selfPointer);
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x001CB6C8 File Offset: 0x001C9AC8
		internal ConnectionRequest AsRequest()
		{
			ConnectionRequest connectionRequest = new ConnectionRequest(this.RemoteEndpointId(), this.RemoteEndpointName(), NearbyConnectionsManager.ServiceId, this.Payload());
			return connectionRequest;
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x001CB6F4 File Offset: 0x001C9AF4
		internal static NativeConnectionRequest FromPointer(IntPtr pointer)
		{
			if (pointer == IntPtr.Zero)
			{
				return null;
			}
			return new NativeConnectionRequest(pointer);
		}
	}
}
