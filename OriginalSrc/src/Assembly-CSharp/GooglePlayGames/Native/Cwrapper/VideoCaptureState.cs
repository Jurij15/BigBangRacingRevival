using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x020006B3 RID: 1715
	internal static class VideoCaptureState
	{
		// Token: 0x060030EF RID: 12527
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoCaptureState_IsCapturing(HandleRef self);

		// Token: 0x060030F0 RID: 12528
		[DllImport("gpg")]
		internal static extern Types.VideoCaptureMode VideoCaptureState_CaptureMode(HandleRef self);

		// Token: 0x060030F1 RID: 12529
		[DllImport("gpg")]
		internal static extern Types.VideoQualityLevel VideoCaptureState_QualityLevel(HandleRef self);

		// Token: 0x060030F2 RID: 12530
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoCaptureState_IsOverlayVisible(HandleRef self);

		// Token: 0x060030F3 RID: 12531
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoCaptureState_IsPaused(HandleRef self);

		// Token: 0x060030F4 RID: 12532
		[DllImport("gpg")]
		internal static extern void VideoCaptureState_Dispose(HandleRef self);

		// Token: 0x060030F5 RID: 12533
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoCaptureState_Valid(HandleRef self);
	}
}
