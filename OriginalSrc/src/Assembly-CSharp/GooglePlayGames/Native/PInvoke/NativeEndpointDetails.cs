using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006EE RID: 1774
	internal class NativeEndpointDetails : BaseReferenceHolder
	{
		// Token: 0x06003320 RID: 13088 RVA: 0x001CB863 File Offset: 0x001C9C63
		internal NativeEndpointDetails(IntPtr pointer)
			: base(pointer)
		{
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x001CB86C File Offset: 0x001C9C6C
		internal string EndpointId()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_arg, UIntPtr out_size) => NearbyConnectionTypes.EndpointDetails_GetEndpointId(base.SelfPtr(), out_arg, out_size));
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x001CB87F File Offset: 0x001C9C7F
		internal string Name()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_arg, UIntPtr out_size) => NearbyConnectionTypes.EndpointDetails_GetName(base.SelfPtr(), out_arg, out_size));
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x001CB892 File Offset: 0x001C9C92
		internal string ServiceId()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_arg, UIntPtr out_size) => NearbyConnectionTypes.EndpointDetails_GetServiceId(base.SelfPtr(), out_arg, out_size));
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x001CB8A5 File Offset: 0x001C9CA5
		protected override void CallDispose(HandleRef selfPointer)
		{
			NearbyConnectionTypes.EndpointDetails_Dispose(selfPointer);
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x001CB8AD File Offset: 0x001C9CAD
		internal EndpointDetails ToDetails()
		{
			return new EndpointDetails(this.EndpointId(), this.Name(), this.ServiceId());
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x001CB8C6 File Offset: 0x001C9CC6
		internal static NativeEndpointDetails FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new NativeEndpointDetails(pointer);
		}
	}
}
