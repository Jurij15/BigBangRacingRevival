using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006E5 RID: 1765
	internal class FetchScorePageResponse : BaseReferenceHolder
	{
		// Token: 0x060032CB RID: 13003 RVA: 0x001CB052 File Offset: 0x001C9452
		internal FetchScorePageResponse(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x001CB05B File Offset: 0x001C945B
		protected override void CallDispose(HandleRef selfPointer)
		{
			LeaderboardManager.LeaderboardManager_FetchScorePageResponse_Dispose(base.SelfPtr());
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x001CB068 File Offset: 0x001C9468
		internal CommonErrorStatus.ResponseStatus GetStatus()
		{
			return LeaderboardManager.LeaderboardManager_FetchScorePageResponse_GetStatus(base.SelfPtr());
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x001CB075 File Offset: 0x001C9475
		internal NativeScorePage GetScorePage()
		{
			return NativeScorePage.FromPointer(LeaderboardManager.LeaderboardManager_FetchScorePageResponse_GetData(base.SelfPtr()));
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x001CB087 File Offset: 0x001C9487
		internal static FetchScorePageResponse FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new FetchScorePageResponse(pointer);
		}
	}
}
