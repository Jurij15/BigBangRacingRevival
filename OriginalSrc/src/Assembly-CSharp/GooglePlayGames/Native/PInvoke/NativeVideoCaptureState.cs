using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000702 RID: 1794
	internal class NativeVideoCaptureState : BaseReferenceHolder
	{
		// Token: 0x060033D6 RID: 13270 RVA: 0x001CCA3F File Offset: 0x001CAE3F
		internal NativeVideoCaptureState(IntPtr selfPtr)
			: base(selfPtr)
		{
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x001CCA48 File Offset: 0x001CAE48
		protected override void CallDispose(HandleRef selfPointer)
		{
			VideoCaptureState.VideoCaptureState_Dispose(selfPointer);
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x001CCA50 File Offset: 0x001CAE50
		internal bool IsCapturing()
		{
			return VideoCaptureState.VideoCaptureState_IsCapturing(base.SelfPtr());
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x001CCA5D File Offset: 0x001CAE5D
		internal Types.VideoCaptureMode CaptureMode()
		{
			return VideoCaptureState.VideoCaptureState_CaptureMode(base.SelfPtr());
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x001CCA6A File Offset: 0x001CAE6A
		internal Types.VideoQualityLevel QualityLevel()
		{
			return VideoCaptureState.VideoCaptureState_QualityLevel(base.SelfPtr());
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x001CCA77 File Offset: 0x001CAE77
		internal bool IsOverlayVisible()
		{
			return VideoCaptureState.VideoCaptureState_IsOverlayVisible(base.SelfPtr());
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x001CCA84 File Offset: 0x001CAE84
		internal bool IsPaused()
		{
			return VideoCaptureState.VideoCaptureState_IsPaused(base.SelfPtr());
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x001CCA91 File Offset: 0x001CAE91
		internal static NativeVideoCaptureState FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new NativeVideoCaptureState(pointer);
		}
	}
}
