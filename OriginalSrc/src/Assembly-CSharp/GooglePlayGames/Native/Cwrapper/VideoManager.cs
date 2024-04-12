using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x020006B4 RID: 1716
	internal static class VideoManager
	{
		// Token: 0x060030F6 RID: 12534
		[DllImport("gpg")]
		internal static extern void VideoManager_GetCaptureCapabilities(HandleRef self, VideoManager.CaptureCapabilitiesCallback callback, IntPtr callback_arg);

		// Token: 0x060030F7 RID: 12535
		[DllImport("gpg")]
		internal static extern void VideoManager_ShowCaptureOverlay(HandleRef self);

		// Token: 0x060030F8 RID: 12536
		[DllImport("gpg")]
		internal static extern void VideoManager_GetCaptureState(HandleRef self, VideoManager.CaptureStateCallback callback, IntPtr callback_arg);

		// Token: 0x060030F9 RID: 12537
		[DllImport("gpg")]
		internal static extern void VideoManager_IsCaptureAvailable(HandleRef self, Types.VideoCaptureMode capture_mode, VideoManager.IsCaptureAvailableCallback callback, IntPtr callback_arg);

		// Token: 0x060030FA RID: 12538
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoManager_IsCaptureSupported(HandleRef self);

		// Token: 0x060030FB RID: 12539
		[DllImport("gpg")]
		internal static extern void VideoManager_RegisterCaptureOverlayStateChangedListener(HandleRef self, IntPtr helper);

		// Token: 0x060030FC RID: 12540
		[DllImport("gpg")]
		internal static extern void VideoManager_UnregisterCaptureOverlayStateChangedListener(HandleRef self);

		// Token: 0x060030FD RID: 12541
		[DllImport("gpg")]
		internal static extern void VideoManager_GetCaptureCapabilitiesResponse_Dispose(HandleRef self);

		// Token: 0x060030FE RID: 12542
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus VideoManager_GetCaptureCapabilitiesResponse_GetStatus(HandleRef self);

		// Token: 0x060030FF RID: 12543
		[DllImport("gpg")]
		internal static extern IntPtr VideoManager_GetCaptureCapabilitiesResponse_GetVideoCapabilities(HandleRef self);

		// Token: 0x06003100 RID: 12544
		[DllImport("gpg")]
		internal static extern void VideoManager_GetCaptureStateResponse_Dispose(HandleRef self);

		// Token: 0x06003101 RID: 12545
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus VideoManager_GetCaptureStateResponse_GetStatus(HandleRef self);

		// Token: 0x06003102 RID: 12546
		[DllImport("gpg")]
		internal static extern IntPtr VideoManager_GetCaptureStateResponse_GetVideoCaptureState(HandleRef self);

		// Token: 0x06003103 RID: 12547
		[DllImport("gpg")]
		internal static extern void VideoManager_IsCaptureAvailableResponse_Dispose(HandleRef self);

		// Token: 0x06003104 RID: 12548
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus VideoManager_IsCaptureAvailableResponse_GetStatus(HandleRef self);

		// Token: 0x06003105 RID: 12549
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoManager_IsCaptureAvailableResponse_GetIsCaptureAvailable(HandleRef self);

		// Token: 0x020006B5 RID: 1717
		// (Invoke) Token: 0x06003107 RID: 12551
		internal delegate void CaptureCapabilitiesCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x020006B6 RID: 1718
		// (Invoke) Token: 0x0600310B RID: 12555
		internal delegate void CaptureStateCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x020006B7 RID: 1719
		// (Invoke) Token: 0x0600310F RID: 12559
		internal delegate void IsCaptureAvailableCallback(IntPtr arg0, IntPtr arg1);
	}
}
