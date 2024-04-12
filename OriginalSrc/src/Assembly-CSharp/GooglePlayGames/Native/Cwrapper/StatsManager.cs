using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000688 RID: 1672
	internal static class StatsManager
	{
		// Token: 0x0600307C RID: 12412
		[DllImport("gpg")]
		internal static extern void StatsManager_FetchForPlayer(HandleRef self, Types.DataSource data_source, StatsManager.FetchForPlayerCallback callback, IntPtr callback_arg);

		// Token: 0x0600307D RID: 12413
		[DllImport("gpg")]
		internal static extern void StatsManager_FetchForPlayerResponse_Dispose(HandleRef self);

		// Token: 0x0600307E RID: 12414
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus StatsManager_FetchForPlayerResponse_GetStatus(HandleRef self);

		// Token: 0x0600307F RID: 12415
		[DllImport("gpg")]
		internal static extern IntPtr StatsManager_FetchForPlayerResponse_GetData(HandleRef self);

		// Token: 0x02000689 RID: 1673
		// (Invoke) Token: 0x06003081 RID: 12417
		internal delegate void FetchForPlayerCallback(IntPtr arg0, IntPtr arg1);
	}
}
