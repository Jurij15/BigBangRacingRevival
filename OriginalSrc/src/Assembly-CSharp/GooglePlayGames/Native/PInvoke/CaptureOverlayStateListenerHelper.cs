using System;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006DB RID: 1755
	internal class CaptureOverlayStateListenerHelper : BaseReferenceHolder
	{
		// Token: 0x06003280 RID: 12928 RVA: 0x001CA3C7 File Offset: 0x001C87C7
		internal CaptureOverlayStateListenerHelper(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x001CA3D0 File Offset: 0x001C87D0
		protected override void CallDispose(HandleRef selfPointer)
		{
			CaptureOverlayStateListenerHelper.CaptureOverlayStateListenerHelper_Dispose(selfPointer);
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x001CA3D8 File Offset: 0x001C87D8
		internal CaptureOverlayStateListenerHelper SetOnCaptureOverlayStateChangedCallback(Action<Types.VideoCaptureOverlayState> callback)
		{
			CaptureOverlayStateListenerHelper.CaptureOverlayStateListenerHelper_SetOnCaptureOverlayStateChangedCallback(base.SelfPtr(), new CaptureOverlayStateListenerHelper.OnCaptureOverlayStateChangedCallback(CaptureOverlayStateListenerHelper.InternalOnCaptureOverlayStateChangedCallback), Callbacks.ToIntPtr(callback));
			return this;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x001CA40C File Offset: 0x001C880C
		[MonoPInvokeCallback(typeof(CaptureOverlayStateListenerHelper.OnCaptureOverlayStateChangedCallback))]
		internal static void InternalOnCaptureOverlayStateChangedCallback(Types.VideoCaptureOverlayState response, IntPtr data)
		{
			Action<Types.VideoCaptureOverlayState> action = Callbacks.IntPtrToPermanentCallback<Action<Types.VideoCaptureOverlayState>>(data);
			try
			{
				action.Invoke(response);
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered executing InternalOnCaptureOverlayStateChangedCallback. Smothering to avoid passing exception into Native: " + ex);
			}
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x001CA454 File Offset: 0x001C8854
		internal static CaptureOverlayStateListenerHelper Create()
		{
			return new CaptureOverlayStateListenerHelper(CaptureOverlayStateListenerHelper.CaptureOverlayStateListenerHelper_Construct());
		}
	}
}
