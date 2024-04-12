using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006E6 RID: 1766
	internal class FetchResponse : BaseReferenceHolder
	{
		// Token: 0x060032D0 RID: 13008 RVA: 0x001CB0AD File Offset: 0x001C94AD
		internal FetchResponse(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x001CB0B6 File Offset: 0x001C94B6
		protected override void CallDispose(HandleRef selfPointer)
		{
			LeaderboardManager.LeaderboardManager_FetchResponse_Dispose(base.SelfPtr());
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x001CB0C3 File Offset: 0x001C94C3
		internal NativeLeaderboard Leaderboard()
		{
			return NativeLeaderboard.FromPointer(LeaderboardManager.LeaderboardManager_FetchResponse_GetData(base.SelfPtr()));
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x001CB0D5 File Offset: 0x001C94D5
		internal CommonErrorStatus.ResponseStatus GetStatus()
		{
			return LeaderboardManager.LeaderboardManager_FetchResponse_GetStatus(base.SelfPtr());
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x001CB0E2 File Offset: 0x001C94E2
		internal static FetchResponse FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new FetchResponse(pointer);
		}
	}
}
