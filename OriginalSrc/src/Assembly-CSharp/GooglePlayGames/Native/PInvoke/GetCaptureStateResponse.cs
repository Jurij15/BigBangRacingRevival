using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000729 RID: 1833
	internal class GetCaptureStateResponse : BaseReferenceHolder
	{
		// Token: 0x06003519 RID: 13593 RVA: 0x001CF4CF File Offset: 0x001CD8CF
		internal GetCaptureStateResponse(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x001CF4D8 File Offset: 0x001CD8D8
		protected override void CallDispose(HandleRef selfPointer)
		{
			VideoManager.VideoManager_GetCaptureStateResponse_Dispose(base.SelfPtr());
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x001CF4E5 File Offset: 0x001CD8E5
		internal NativeVideoCaptureState GetData()
		{
			return NativeVideoCaptureState.FromPointer(VideoManager.VideoManager_GetCaptureStateResponse_GetVideoCaptureState(base.SelfPtr()));
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x001CF4F7 File Offset: 0x001CD8F7
		internal CommonErrorStatus.ResponseStatus GetStatus()
		{
			return VideoManager.VideoManager_GetCaptureStateResponse_GetStatus(base.SelfPtr());
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x001CF504 File Offset: 0x001CD904
		internal bool RequestSucceeded()
		{
			return this.GetStatus() > (CommonErrorStatus.ResponseStatus)0;
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x001CF50F File Offset: 0x001CD90F
		internal static GetCaptureStateResponse FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new GetCaptureStateResponse(pointer);
		}
	}
}
