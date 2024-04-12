using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000645 RID: 1605
	internal static class GameServices
	{
		// Token: 0x06002EC2 RID: 11970
		[DllImport("gpg")]
		internal static extern void GameServices_Flush(HandleRef self, GameServices.FlushCallback callback, IntPtr callback_arg);

		// Token: 0x06002EC3 RID: 11971
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool GameServices_IsAuthorized(HandleRef self);

		// Token: 0x06002EC4 RID: 11972
		[DllImport("gpg")]
		internal static extern void GameServices_Dispose(HandleRef self);

		// Token: 0x06002EC5 RID: 11973
		[DllImport("gpg")]
		internal static extern void GameServices_SignOut(HandleRef self);

		// Token: 0x06002EC6 RID: 11974
		[DllImport("gpg")]
		internal static extern void GameServices_StartAuthorizationUI(HandleRef self);

		// Token: 0x02000646 RID: 1606
		// (Invoke) Token: 0x06002EC8 RID: 11976
		internal delegate void FlushCallback(CommonErrorStatus.FlushStatus arg0, IntPtr arg1);
	}
}
