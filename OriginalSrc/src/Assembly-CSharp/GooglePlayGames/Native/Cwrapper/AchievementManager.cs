using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000624 RID: 1572
	internal static class AchievementManager
	{
		// Token: 0x06002E41 RID: 11841
		[DllImport("gpg")]
		internal static extern void AchievementManager_FetchAll(HandleRef self, Types.DataSource data_source, AchievementManager.FetchAllCallback callback, IntPtr callback_arg);

		// Token: 0x06002E42 RID: 11842
		[DllImport("gpg")]
		internal static extern void AchievementManager_Reveal(HandleRef self, string achievement_id);

		// Token: 0x06002E43 RID: 11843
		[DllImport("gpg")]
		internal static extern void AchievementManager_Unlock(HandleRef self, string achievement_id);

		// Token: 0x06002E44 RID: 11844
		[DllImport("gpg")]
		internal static extern void AchievementManager_ShowAllUI(HandleRef self, AchievementManager.ShowAllUICallback callback, IntPtr callback_arg);

		// Token: 0x06002E45 RID: 11845
		[DllImport("gpg")]
		internal static extern void AchievementManager_SetStepsAtLeast(HandleRef self, string achievement_id, uint steps);

		// Token: 0x06002E46 RID: 11846
		[DllImport("gpg")]
		internal static extern void AchievementManager_Increment(HandleRef self, string achievement_id, uint steps);

		// Token: 0x06002E47 RID: 11847
		[DllImport("gpg")]
		internal static extern void AchievementManager_Fetch(HandleRef self, Types.DataSource data_source, string achievement_id, AchievementManager.FetchCallback callback, IntPtr callback_arg);

		// Token: 0x06002E48 RID: 11848
		[DllImport("gpg")]
		internal static extern void AchievementManager_FetchAllResponse_Dispose(HandleRef self);

		// Token: 0x06002E49 RID: 11849
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus AchievementManager_FetchAllResponse_GetStatus(HandleRef self);

		// Token: 0x06002E4A RID: 11850
		[DllImport("gpg")]
		internal static extern UIntPtr AchievementManager_FetchAllResponse_GetData_Length(HandleRef self);

		// Token: 0x06002E4B RID: 11851
		[DllImport("gpg")]
		internal static extern IntPtr AchievementManager_FetchAllResponse_GetData_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x06002E4C RID: 11852
		[DllImport("gpg")]
		internal static extern void AchievementManager_FetchResponse_Dispose(HandleRef self);

		// Token: 0x06002E4D RID: 11853
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus AchievementManager_FetchResponse_GetStatus(HandleRef self);

		// Token: 0x06002E4E RID: 11854
		[DllImport("gpg")]
		internal static extern IntPtr AchievementManager_FetchResponse_GetData(HandleRef self);

		// Token: 0x02000625 RID: 1573
		// (Invoke) Token: 0x06002E50 RID: 11856
		internal delegate void FetchAllCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000626 RID: 1574
		// (Invoke) Token: 0x06002E54 RID: 11860
		internal delegate void FetchCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000627 RID: 1575
		// (Invoke) Token: 0x06002E58 RID: 11864
		internal delegate void ShowAllUICallback(CommonErrorStatus.UIStatus arg0, IntPtr arg1);
	}
}
