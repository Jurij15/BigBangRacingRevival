using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000670 RID: 1648
	internal static class RealTimeMultiplayerManager
	{
		// Token: 0x06002FC3 RID: 12227
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_CreateRealTimeRoom(HandleRef self, IntPtr config, IntPtr helper, RealTimeMultiplayerManager.RealTimeRoomCallback callback, IntPtr callback_arg);

		// Token: 0x06002FC4 RID: 12228
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_LeaveRoom(HandleRef self, IntPtr room, RealTimeMultiplayerManager.LeaveRoomCallback callback, IntPtr callback_arg);

		// Token: 0x06002FC5 RID: 12229
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_SendUnreliableMessage(HandleRef self, IntPtr room, IntPtr[] participants, UIntPtr participants_size, byte[] data, UIntPtr data_size);

		// Token: 0x06002FC6 RID: 12230
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_ShowWaitingRoomUI(HandleRef self, IntPtr room, uint min_participants_to_start, RealTimeMultiplayerManager.WaitingRoomUICallback callback, IntPtr callback_arg);

		// Token: 0x06002FC7 RID: 12231
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_ShowPlayerSelectUI(HandleRef self, uint minimum_players, uint maximum_players, [MarshalAs(3)] bool allow_automatch, RealTimeMultiplayerManager.PlayerSelectUICallback callback, IntPtr callback_arg);

		// Token: 0x06002FC8 RID: 12232
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_DismissInvitation(HandleRef self, IntPtr invitation);

		// Token: 0x06002FC9 RID: 12233
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_DeclineInvitation(HandleRef self, IntPtr invitation);

		// Token: 0x06002FCA RID: 12234
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_SendReliableMessage(HandleRef self, IntPtr room, IntPtr participant, byte[] data, UIntPtr data_size, RealTimeMultiplayerManager.SendReliableMessageCallback callback, IntPtr callback_arg);

		// Token: 0x06002FCB RID: 12235
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_AcceptInvitation(HandleRef self, IntPtr invitation, IntPtr helper, RealTimeMultiplayerManager.RealTimeRoomCallback callback, IntPtr callback_arg);

		// Token: 0x06002FCC RID: 12236
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_FetchInvitations(HandleRef self, RealTimeMultiplayerManager.FetchInvitationsCallback callback, IntPtr callback_arg);

		// Token: 0x06002FCD RID: 12237
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_SendUnreliableMessageToOthers(HandleRef self, IntPtr room, byte[] data, UIntPtr data_size);

		// Token: 0x06002FCE RID: 12238
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_ShowRoomInboxUI(HandleRef self, RealTimeMultiplayerManager.RoomInboxUICallback callback, IntPtr callback_arg);

		// Token: 0x06002FCF RID: 12239
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_RealTimeRoomResponse_Dispose(HandleRef self);

		// Token: 0x06002FD0 RID: 12240
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.MultiplayerStatus RealTimeMultiplayerManager_RealTimeRoomResponse_GetStatus(HandleRef self);

		// Token: 0x06002FD1 RID: 12241
		[DllImport("gpg")]
		internal static extern IntPtr RealTimeMultiplayerManager_RealTimeRoomResponse_GetRoom(HandleRef self);

		// Token: 0x06002FD2 RID: 12242
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_RoomInboxUIResponse_Dispose(HandleRef self);

		// Token: 0x06002FD3 RID: 12243
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.UIStatus RealTimeMultiplayerManager_RoomInboxUIResponse_GetStatus(HandleRef self);

		// Token: 0x06002FD4 RID: 12244
		[DllImport("gpg")]
		internal static extern IntPtr RealTimeMultiplayerManager_RoomInboxUIResponse_GetInvitation(HandleRef self);

		// Token: 0x06002FD5 RID: 12245
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_WaitingRoomUIResponse_Dispose(HandleRef self);

		// Token: 0x06002FD6 RID: 12246
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.UIStatus RealTimeMultiplayerManager_WaitingRoomUIResponse_GetStatus(HandleRef self);

		// Token: 0x06002FD7 RID: 12247
		[DllImport("gpg")]
		internal static extern IntPtr RealTimeMultiplayerManager_WaitingRoomUIResponse_GetRoom(HandleRef self);

		// Token: 0x06002FD8 RID: 12248
		[DllImport("gpg")]
		internal static extern void RealTimeMultiplayerManager_FetchInvitationsResponse_Dispose(HandleRef self);

		// Token: 0x06002FD9 RID: 12249
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus RealTimeMultiplayerManager_FetchInvitationsResponse_GetStatus(HandleRef self);

		// Token: 0x06002FDA RID: 12250
		[DllImport("gpg")]
		internal static extern UIntPtr RealTimeMultiplayerManager_FetchInvitationsResponse_GetInvitations_Length(HandleRef self);

		// Token: 0x06002FDB RID: 12251
		[DllImport("gpg")]
		internal static extern IntPtr RealTimeMultiplayerManager_FetchInvitationsResponse_GetInvitations_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x02000671 RID: 1649
		// (Invoke) Token: 0x06002FDD RID: 12253
		internal delegate void RealTimeRoomCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000672 RID: 1650
		// (Invoke) Token: 0x06002FE1 RID: 12257
		internal delegate void LeaveRoomCallback(CommonErrorStatus.ResponseStatus arg0, IntPtr arg1);

		// Token: 0x02000673 RID: 1651
		// (Invoke) Token: 0x06002FE5 RID: 12261
		internal delegate void SendReliableMessageCallback(CommonErrorStatus.MultiplayerStatus arg0, IntPtr arg1);

		// Token: 0x02000674 RID: 1652
		// (Invoke) Token: 0x06002FE9 RID: 12265
		internal delegate void RoomInboxUICallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000675 RID: 1653
		// (Invoke) Token: 0x06002FED RID: 12269
		internal delegate void PlayerSelectUICallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000676 RID: 1654
		// (Invoke) Token: 0x06002FF1 RID: 12273
		internal delegate void WaitingRoomUICallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000677 RID: 1655
		// (Invoke) Token: 0x06002FF5 RID: 12277
		internal delegate void FetchInvitationsCallback(IntPtr arg0, IntPtr arg1);
	}
}
