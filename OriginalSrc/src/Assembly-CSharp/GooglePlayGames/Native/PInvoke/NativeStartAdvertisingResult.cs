using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006FF RID: 1791
	internal class NativeStartAdvertisingResult : BaseReferenceHolder
	{
		// Token: 0x060033AE RID: 13230 RVA: 0x001CC525 File Offset: 0x001CA925
		internal NativeStartAdvertisingResult(IntPtr pointer)
			: base(pointer)
		{
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x001CC52E File Offset: 0x001CA92E
		internal int GetStatus()
		{
			return NearbyConnectionTypes.StartAdvertisingResult_GetStatus(base.SelfPtr());
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x001CC53B File Offset: 0x001CA93B
		internal string LocalEndpointName()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_arg, UIntPtr out_size) => NearbyConnectionTypes.StartAdvertisingResult_GetLocalEndpointName(base.SelfPtr(), out_arg, out_size));
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x001CC54E File Offset: 0x001CA94E
		protected override void CallDispose(HandleRef selfPointer)
		{
			NearbyConnectionTypes.StartAdvertisingResult_Dispose(selfPointer);
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x001CC556 File Offset: 0x001CA956
		internal AdvertisingResult AsResult()
		{
			return new AdvertisingResult((ResponseStatus)Enum.ToObject(typeof(ResponseStatus), this.GetStatus()), this.LocalEndpointName());
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x001CC57D File Offset: 0x001CA97D
		internal static NativeStartAdvertisingResult FromPointer(IntPtr pointer)
		{
			if (pointer == IntPtr.Zero)
			{
				return null;
			}
			return new NativeStartAdvertisingResult(pointer);
		}
	}
}
