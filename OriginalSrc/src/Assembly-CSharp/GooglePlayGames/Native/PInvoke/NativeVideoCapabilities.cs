using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000701 RID: 1793
	internal class NativeVideoCapabilities : BaseReferenceHolder
	{
		// Token: 0x060033CE RID: 13262 RVA: 0x001CC9C5 File Offset: 0x001CADC5
		internal NativeVideoCapabilities(IntPtr selfPtr)
			: base(selfPtr)
		{
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x001CC9CE File Offset: 0x001CADCE
		protected override void CallDispose(HandleRef selfPointer)
		{
			VideoCapabilities.VideoCapabilities_Dispose(selfPointer);
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x001CC9D6 File Offset: 0x001CADD6
		internal bool IsCameraSupported()
		{
			return VideoCapabilities.VideoCapabilities_IsCameraSupported(base.SelfPtr());
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x001CC9E3 File Offset: 0x001CADE3
		internal bool IsMicSupported()
		{
			return VideoCapabilities.VideoCapabilities_IsMicSupported(base.SelfPtr());
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x001CC9F0 File Offset: 0x001CADF0
		internal bool IsWriteStorageSupported()
		{
			return VideoCapabilities.VideoCapabilities_IsWriteStorageSupported(base.SelfPtr());
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x001CC9FD File Offset: 0x001CADFD
		internal bool SupportsCaptureMode(Types.VideoCaptureMode captureMode)
		{
			return VideoCapabilities.VideoCapabilities_SupportsCaptureMode(base.SelfPtr(), captureMode);
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x001CCA0B File Offset: 0x001CAE0B
		internal bool SupportsQualityLevel(Types.VideoQualityLevel qualityLevel)
		{
			return VideoCapabilities.VideoCapabilities_SupportsQualityLevel(base.SelfPtr(), qualityLevel);
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x001CCA19 File Offset: 0x001CAE19
		internal static NativeVideoCapabilities FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new NativeVideoCapabilities(pointer);
		}
	}
}
