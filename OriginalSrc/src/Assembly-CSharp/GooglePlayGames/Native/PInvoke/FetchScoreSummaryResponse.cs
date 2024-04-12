using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006E7 RID: 1767
	internal class FetchScoreSummaryResponse : BaseReferenceHolder
	{
		// Token: 0x060032D5 RID: 13013 RVA: 0x001CB108 File Offset: 0x001C9508
		internal FetchScoreSummaryResponse(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x001CB111 File Offset: 0x001C9511
		protected override void CallDispose(HandleRef selfPointer)
		{
			LeaderboardManager.LeaderboardManager_FetchScoreSummaryResponse_Dispose(selfPointer);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x001CB119 File Offset: 0x001C9519
		internal CommonErrorStatus.ResponseStatus GetStatus()
		{
			return LeaderboardManager.LeaderboardManager_FetchScoreSummaryResponse_GetStatus(base.SelfPtr());
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x001CB126 File Offset: 0x001C9526
		internal NativeScoreSummary GetScoreSummary()
		{
			return NativeScoreSummary.FromPointer(LeaderboardManager.LeaderboardManager_FetchScoreSummaryResponse_GetData(base.SelfPtr()));
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x001CB138 File Offset: 0x001C9538
		internal static FetchScoreSummaryResponse FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new FetchScoreSummaryResponse(pointer);
		}
	}
}
