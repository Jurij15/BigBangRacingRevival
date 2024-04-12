using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200062C RID: 1580
	internal static class Builder
	{
		// Token: 0x06002E6F RID: 11887
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_SetOnAuthActionStarted(HandleRef self, Builder.OnAuthActionStartedCallback callback, IntPtr callback_arg);

		// Token: 0x06002E70 RID: 11888
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_AddOauthScope(HandleRef self, string scope);

		// Token: 0x06002E71 RID: 11889
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_SetLogging(HandleRef self, Builder.OnLogCallback callback, IntPtr callback_arg, Types.LogLevel min_level);

		// Token: 0x06002E72 RID: 11890
		[DllImport("gpg")]
		internal static extern IntPtr GameServices_Builder_Construct();

		// Token: 0x06002E73 RID: 11891
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_EnableSnapshots(HandleRef self);

		// Token: 0x06002E74 RID: 11892
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_SetOnLog(HandleRef self, Builder.OnLogCallback callback, IntPtr callback_arg, Types.LogLevel min_level);

		// Token: 0x06002E75 RID: 11893
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_SetDefaultOnLog(HandleRef self, Types.LogLevel min_level);

		// Token: 0x06002E76 RID: 11894
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_SetOnAuthActionFinished(HandleRef self, Builder.OnAuthActionFinishedCallback callback, IntPtr callback_arg);

		// Token: 0x06002E77 RID: 11895
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_SetOnTurnBasedMatchEvent(HandleRef self, Builder.OnTurnBasedMatchEventCallback callback, IntPtr callback_arg);

		// Token: 0x06002E78 RID: 11896
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_SetOnQuestCompleted(HandleRef self, Builder.OnQuestCompletedCallback callback, IntPtr callback_arg);

		// Token: 0x06002E79 RID: 11897
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_SetOnMultiplayerInvitationEvent(HandleRef self, Builder.OnMultiplayerInvitationEventCallback callback, IntPtr callback_arg);

		// Token: 0x06002E7A RID: 11898
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_SetShowConnectingPopup(HandleRef self, bool flag);

		// Token: 0x06002E7B RID: 11899
		[DllImport("gpg")]
		internal static extern IntPtr GameServices_Builder_Create(HandleRef self, IntPtr platform);

		// Token: 0x06002E7C RID: 11900
		[DllImport("gpg")]
		internal static extern void GameServices_Builder_Dispose(HandleRef self);

		// Token: 0x0200062D RID: 1581
		// (Invoke) Token: 0x06002E7E RID: 11902
		internal delegate void OnLogCallback(Types.LogLevel arg0, string arg1, IntPtr arg2);

		// Token: 0x0200062E RID: 1582
		// (Invoke) Token: 0x06002E82 RID: 11906
		internal delegate void OnAuthActionStartedCallback(Types.AuthOperation arg0, IntPtr arg1);

		// Token: 0x0200062F RID: 1583
		// (Invoke) Token: 0x06002E86 RID: 11910
		internal delegate void OnAuthActionFinishedCallback(Types.AuthOperation arg0, CommonErrorStatus.AuthStatus arg1, IntPtr arg2);

		// Token: 0x02000630 RID: 1584
		// (Invoke) Token: 0x06002E8A RID: 11914
		internal delegate void OnMultiplayerInvitationEventCallback(Types.MultiplayerEvent arg0, string arg1, IntPtr arg2, IntPtr arg3);

		// Token: 0x02000631 RID: 1585
		// (Invoke) Token: 0x06002E8E RID: 11918
		internal delegate void OnTurnBasedMatchEventCallback(Types.MultiplayerEvent arg0, string arg1, IntPtr arg2, IntPtr arg3);

		// Token: 0x02000632 RID: 1586
		// (Invoke) Token: 0x06002E92 RID: 11922
		internal delegate void OnQuestCompletedCallback(IntPtr arg0, IntPtr arg1);
	}
}
