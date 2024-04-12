using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x0200072A RID: 1834
	internal class IsCaptureAvailableResponse : BaseReferenceHolder
	{
		// Token: 0x0600351F RID: 13599 RVA: 0x001CF535 File Offset: 0x001CD935
		internal IsCaptureAvailableResponse(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x001CF53E File Offset: 0x001CD93E
		protected override void CallDispose(HandleRef selfPointer)
		{
			VideoManager.VideoManager_IsCaptureAvailableResponse_Dispose(selfPointer);
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x001CF546 File Offset: 0x001CD946
		internal CommonErrorStatus.ResponseStatus GetStatus()
		{
			return VideoManager.VideoManager_IsCaptureAvailableResponse_GetStatus(base.SelfPtr());
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x001CF553 File Offset: 0x001CD953
		internal bool RequestSucceeded()
		{
			return this.GetStatus() > (CommonErrorStatus.ResponseStatus)0;
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x001CF55E File Offset: 0x001CD95E
		internal bool IsCaptureAvailable()
		{
			return VideoManager.VideoManager_IsCaptureAvailableResponse_GetIsCaptureAvailable(base.SelfPtr());
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x001CF56B File Offset: 0x001CD96B
		internal static IsCaptureAvailableResponse FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new IsCaptureAvailableResponse(pointer);
		}
	}
}
