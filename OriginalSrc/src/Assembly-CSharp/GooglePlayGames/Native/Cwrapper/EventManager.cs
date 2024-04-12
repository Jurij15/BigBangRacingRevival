using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000642 RID: 1602
	internal static class EventManager
	{
		// Token: 0x06002EB1 RID: 11953
		[DllImport("gpg")]
		internal static extern void EventManager_FetchAll(HandleRef self, Types.DataSource data_source, EventManager.FetchAllCallback callback, IntPtr callback_arg);

		// Token: 0x06002EB2 RID: 11954
		[DllImport("gpg")]
		internal static extern void EventManager_Fetch(HandleRef self, Types.DataSource data_source, string event_id, EventManager.FetchCallback callback, IntPtr callback_arg);

		// Token: 0x06002EB3 RID: 11955
		[DllImport("gpg")]
		internal static extern void EventManager_Increment(HandleRef self, string event_id, uint steps);

		// Token: 0x06002EB4 RID: 11956
		[DllImport("gpg")]
		internal static extern void EventManager_FetchAllResponse_Dispose(HandleRef self);

		// Token: 0x06002EB5 RID: 11957
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus EventManager_FetchAllResponse_GetStatus(HandleRef self);

		// Token: 0x06002EB6 RID: 11958
		[DllImport("gpg")]
		internal static extern UIntPtr EventManager_FetchAllResponse_GetData(HandleRef self, IntPtr[] out_arg, UIntPtr out_size);

		// Token: 0x06002EB7 RID: 11959
		[DllImport("gpg")]
		internal static extern void EventManager_FetchResponse_Dispose(HandleRef self);

		// Token: 0x06002EB8 RID: 11960
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus EventManager_FetchResponse_GetStatus(HandleRef self);

		// Token: 0x06002EB9 RID: 11961
		[DllImport("gpg")]
		internal static extern IntPtr EventManager_FetchResponse_GetData(HandleRef self);

		// Token: 0x02000643 RID: 1603
		// (Invoke) Token: 0x06002EBB RID: 11963
		internal delegate void FetchAllCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000644 RID: 1604
		// (Invoke) Token: 0x06002EBF RID: 11967
		internal delegate void FetchCallback(IntPtr arg0, IntPtr arg1);
	}
}
