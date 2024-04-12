using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006EB RID: 1771
	internal class NativeAppIdentifier : BaseReferenceHolder
	{
		// Token: 0x06003307 RID: 13063 RVA: 0x001CB63C File Offset: 0x001C9A3C
		internal NativeAppIdentifier(IntPtr pointer)
			: base(pointer)
		{
		}

		// Token: 0x06003308 RID: 13064
		[DllImport("gpg")]
		internal static extern IntPtr NearbyUtils_ConstructAppIdentifier(string appId);

		// Token: 0x06003309 RID: 13065 RVA: 0x001CB645 File Offset: 0x001C9A45
		internal string Id()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_arg, UIntPtr out_size) => NearbyConnectionTypes.AppIdentifier_GetIdentifier(base.SelfPtr(), out_arg, out_size));
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x001CB658 File Offset: 0x001C9A58
		protected override void CallDispose(HandleRef selfPointer)
		{
			NearbyConnectionTypes.AppIdentifier_Dispose(selfPointer);
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x001CB660 File Offset: 0x001C9A60
		internal static NativeAppIdentifier FromString(string appId)
		{
			return new NativeAppIdentifier(NativeAppIdentifier.NearbyUtils_ConstructAppIdentifier(appId));
		}
	}
}
