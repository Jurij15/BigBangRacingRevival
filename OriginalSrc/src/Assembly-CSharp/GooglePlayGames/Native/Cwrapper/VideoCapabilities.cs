using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x020006B2 RID: 1714
	internal static class VideoCapabilities
	{
		// Token: 0x060030E8 RID: 12520
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoCapabilities_IsCameraSupported(HandleRef self);

		// Token: 0x060030E9 RID: 12521
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoCapabilities_IsMicSupported(HandleRef self);

		// Token: 0x060030EA RID: 12522
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoCapabilities_IsWriteStorageSupported(HandleRef self);

		// Token: 0x060030EB RID: 12523
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoCapabilities_SupportsCaptureMode(HandleRef self, Types.VideoCaptureMode capture_mode);

		// Token: 0x060030EC RID: 12524
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoCapabilities_SupportsQualityLevel(HandleRef self, Types.VideoQualityLevel quality_level);

		// Token: 0x060030ED RID: 12525
		[DllImport("gpg")]
		internal static extern void VideoCapabilities_Dispose(HandleRef self);

		// Token: 0x060030EE RID: 12526
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool VideoCapabilities_Valid(HandleRef self);
	}
}
