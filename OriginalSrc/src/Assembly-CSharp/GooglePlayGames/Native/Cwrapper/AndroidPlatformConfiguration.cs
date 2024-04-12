using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000628 RID: 1576
	internal static class AndroidPlatformConfiguration
	{
		// Token: 0x06002E5B RID: 11867
		[DllImport("gpg")]
		internal static extern void AndroidPlatformConfiguration_SetOnLaunchedWithSnapshot(HandleRef self, AndroidPlatformConfiguration.OnLaunchedWithSnapshotCallback callback, IntPtr callback_arg);

		// Token: 0x06002E5C RID: 11868
		[DllImport("gpg")]
		internal static extern IntPtr AndroidPlatformConfiguration_Construct();

		// Token: 0x06002E5D RID: 11869
		[DllImport("gpg")]
		internal static extern void AndroidPlatformConfiguration_SetOptionalIntentHandlerForUI(HandleRef self, AndroidPlatformConfiguration.IntentHandler intent_handler, IntPtr intent_handler_arg);

		// Token: 0x06002E5E RID: 11870
		[DllImport("gpg")]
		internal static extern void AndroidPlatformConfiguration_Dispose(HandleRef self);

		// Token: 0x06002E5F RID: 11871
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool AndroidPlatformConfiguration_Valid(HandleRef self);

		// Token: 0x06002E60 RID: 11872
		[DllImport("gpg")]
		internal static extern void AndroidPlatformConfiguration_SetActivity(HandleRef self, IntPtr android_app_activity);

		// Token: 0x06002E61 RID: 11873
		[DllImport("gpg")]
		internal static extern void AndroidPlatformConfiguration_SetOnLaunchedWithQuest(HandleRef self, AndroidPlatformConfiguration.OnLaunchedWithQuestCallback callback, IntPtr callback_arg);

		// Token: 0x06002E62 RID: 11874
		[DllImport("gpg")]
		internal static extern void AndroidPlatformConfiguration_SetOptionalViewForPopups(HandleRef self, IntPtr android_view);

		// Token: 0x02000629 RID: 1577
		// (Invoke) Token: 0x06002E64 RID: 11876
		internal delegate void IntentHandler(IntPtr arg0, IntPtr arg1);

		// Token: 0x0200062A RID: 1578
		// (Invoke) Token: 0x06002E68 RID: 11880
		internal delegate void OnLaunchedWithSnapshotCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x0200062B RID: 1579
		// (Invoke) Token: 0x06002E6C RID: 11884
		internal delegate void OnLaunchedWithQuestCallback(IntPtr arg0, IntPtr arg1);
	}
}
