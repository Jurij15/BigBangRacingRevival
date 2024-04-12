using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000728 RID: 1832
	internal class GetCaptureCapabilitiesResponse : BaseReferenceHolder
	{
		// Token: 0x06003513 RID: 13587 RVA: 0x001CF469 File Offset: 0x001CD869
		internal GetCaptureCapabilitiesResponse(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x001CF472 File Offset: 0x001CD872
		protected override void CallDispose(HandleRef selfPointer)
		{
			VideoManager.VideoManager_GetCaptureCapabilitiesResponse_Dispose(base.SelfPtr());
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x001CF47F File Offset: 0x001CD87F
		internal CommonErrorStatus.ResponseStatus GetStatus()
		{
			return VideoManager.VideoManager_GetCaptureCapabilitiesResponse_GetStatus(base.SelfPtr());
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x001CF48C File Offset: 0x001CD88C
		internal bool RequestSucceeded()
		{
			return this.GetStatus() > (CommonErrorStatus.ResponseStatus)0;
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x001CF497 File Offset: 0x001CD897
		internal NativeVideoCapabilities GetData()
		{
			return NativeVideoCapabilities.FromPointer(VideoManager.VideoManager_GetCaptureCapabilitiesResponse_GetVideoCapabilities(base.SelfPtr()));
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x001CF4A9 File Offset: 0x001CD8A9
		internal static GetCaptureCapabilitiesResponse FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new GetCaptureCapabilitiesResponse(pointer);
		}
	}
}
