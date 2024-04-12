using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006ED RID: 1773
	internal class NativeConnectionResponse : BaseReferenceHolder
	{
		// Token: 0x06003317 RID: 13079 RVA: 0x001CB73B File Offset: 0x001C9B3B
		internal NativeConnectionResponse(IntPtr pointer)
			: base(pointer)
		{
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x001CB744 File Offset: 0x001C9B44
		internal string RemoteEndpointId()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_arg, UIntPtr out_size) => NearbyConnectionTypes.ConnectionResponse_GetRemoteEndpointId(base.SelfPtr(), out_arg, out_size));
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x001CB757 File Offset: 0x001C9B57
		internal NearbyConnectionTypes.ConnectionResponse_ResponseCode ResponseCode()
		{
			return NearbyConnectionTypes.ConnectionResponse_GetStatus(base.SelfPtr());
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x001CB764 File Offset: 0x001C9B64
		internal byte[] Payload()
		{
			return PInvokeUtilities.OutParamsToArray<byte>((byte[] out_arg, UIntPtr out_size) => NearbyConnectionTypes.ConnectionResponse_GetPayload(base.SelfPtr(), out_arg, out_size));
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x001CB777 File Offset: 0x001C9B77
		protected override void CallDispose(HandleRef selfPointer)
		{
			NearbyConnectionTypes.ConnectionResponse_Dispose(selfPointer);
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x001CB780 File Offset: 0x001C9B80
		internal ConnectionResponse AsResponse(long localClientId)
		{
			NearbyConnectionTypes.ConnectionResponse_ResponseCode connectionResponse_ResponseCode = this.ResponseCode();
			switch (connectionResponse_ResponseCode + 4)
			{
			case (NearbyConnectionTypes.ConnectionResponse_ResponseCode)0:
				return ConnectionResponse.EndpointNotConnected(localClientId, this.RemoteEndpointId());
			case NearbyConnectionTypes.ConnectionResponse_ResponseCode.ACCEPTED:
				return ConnectionResponse.AlreadyConnected(localClientId, this.RemoteEndpointId());
			case NearbyConnectionTypes.ConnectionResponse_ResponseCode.REJECTED:
				return ConnectionResponse.NetworkNotConnected(localClientId, this.RemoteEndpointId());
			case (NearbyConnectionTypes.ConnectionResponse_ResponseCode)3:
				return ConnectionResponse.InternalError(localClientId, this.RemoteEndpointId());
			case (NearbyConnectionTypes.ConnectionResponse_ResponseCode)5:
				return ConnectionResponse.Accepted(localClientId, this.RemoteEndpointId(), this.Payload());
			case (NearbyConnectionTypes.ConnectionResponse_ResponseCode)6:
				return ConnectionResponse.Rejected(localClientId, this.RemoteEndpointId());
			}
			throw new InvalidOperationException("Found connection response of unknown type: " + this.ResponseCode());
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x001CB82B File Offset: 0x001C9C2B
		internal static NativeConnectionResponse FromPointer(IntPtr pointer)
		{
			if (pointer == IntPtr.Zero)
			{
				return null;
			}
			return new NativeConnectionResponse(pointer);
		}
	}
}
