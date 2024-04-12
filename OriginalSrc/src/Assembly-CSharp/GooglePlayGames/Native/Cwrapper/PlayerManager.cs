using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000664 RID: 1636
	internal static class PlayerManager
	{
		// Token: 0x06002F78 RID: 12152
		[DllImport("gpg")]
		internal static extern void PlayerManager_FetchInvitable(HandleRef self, Types.DataSource data_source, PlayerManager.FetchListCallback callback, IntPtr callback_arg);

		// Token: 0x06002F79 RID: 12153
		[DllImport("gpg")]
		internal static extern void PlayerManager_FetchConnected(HandleRef self, Types.DataSource data_source, PlayerManager.FetchListCallback callback, IntPtr callback_arg);

		// Token: 0x06002F7A RID: 12154
		[DllImport("gpg")]
		internal static extern void PlayerManager_Fetch(HandleRef self, Types.DataSource data_source, string player_id, PlayerManager.FetchCallback callback, IntPtr callback_arg);

		// Token: 0x06002F7B RID: 12155
		[DllImport("gpg")]
		internal static extern void PlayerManager_FetchRecentlyPlayed(HandleRef self, Types.DataSource data_source, PlayerManager.FetchListCallback callback, IntPtr callback_arg);

		// Token: 0x06002F7C RID: 12156
		[DllImport("gpg")]
		internal static extern void PlayerManager_FetchSelf(HandleRef self, Types.DataSource data_source, PlayerManager.FetchSelfCallback callback, IntPtr callback_arg);

		// Token: 0x06002F7D RID: 12157
		[DllImport("gpg")]
		internal static extern void PlayerManager_FetchSelfResponse_Dispose(HandleRef self);

		// Token: 0x06002F7E RID: 12158
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus PlayerManager_FetchSelfResponse_GetStatus(HandleRef self);

		// Token: 0x06002F7F RID: 12159
		[DllImport("gpg")]
		internal static extern IntPtr PlayerManager_FetchSelfResponse_GetData(HandleRef self);

		// Token: 0x06002F80 RID: 12160
		[DllImport("gpg")]
		internal static extern void PlayerManager_FetchResponse_Dispose(HandleRef self);

		// Token: 0x06002F81 RID: 12161
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus PlayerManager_FetchResponse_GetStatus(HandleRef self);

		// Token: 0x06002F82 RID: 12162
		[DllImport("gpg")]
		internal static extern IntPtr PlayerManager_FetchResponse_GetData(HandleRef self);

		// Token: 0x06002F83 RID: 12163
		[DllImport("gpg")]
		internal static extern void PlayerManager_FetchListResponse_Dispose(HandleRef self);

		// Token: 0x06002F84 RID: 12164
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus PlayerManager_FetchListResponse_GetStatus(HandleRef self);

		// Token: 0x06002F85 RID: 12165
		[DllImport("gpg")]
		internal static extern UIntPtr PlayerManager_FetchListResponse_GetData_Length(HandleRef self);

		// Token: 0x06002F86 RID: 12166
		[DllImport("gpg")]
		internal static extern IntPtr PlayerManager_FetchListResponse_GetData_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x02000665 RID: 1637
		// (Invoke) Token: 0x06002F88 RID: 12168
		internal delegate void FetchSelfCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000666 RID: 1638
		// (Invoke) Token: 0x06002F8C RID: 12172
		internal delegate void FetchCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000667 RID: 1639
		// (Invoke) Token: 0x06002F90 RID: 12176
		internal delegate void FetchListCallback(IntPtr arg0, IntPtr arg1);
	}
}
