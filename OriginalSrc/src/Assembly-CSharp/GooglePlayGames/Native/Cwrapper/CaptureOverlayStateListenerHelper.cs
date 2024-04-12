using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000633 RID: 1587
	internal static class CaptureOverlayStateListenerHelper
	{
		// Token: 0x06002E95 RID: 11925
		[DllImport("gpg")]
		internal static extern void CaptureOverlayStateListenerHelper_SetOnCaptureOverlayStateChangedCallback(HandleRef self, CaptureOverlayStateListenerHelper.OnCaptureOverlayStateChangedCallback callback, IntPtr callback_arg);

		// Token: 0x06002E96 RID: 11926
		[DllImport("gpg")]
		internal static extern IntPtr CaptureOverlayStateListenerHelper_Construct();

		// Token: 0x06002E97 RID: 11927
		[DllImport("gpg")]
		internal static extern void CaptureOverlayStateListenerHelper_Dispose(HandleRef self);

		// Token: 0x02000634 RID: 1588
		// (Invoke) Token: 0x06002E99 RID: 11929
		internal delegate void OnCaptureOverlayStateChangedCallback(Types.VideoCaptureOverlayState arg0, IntPtr arg1);
	}
}
