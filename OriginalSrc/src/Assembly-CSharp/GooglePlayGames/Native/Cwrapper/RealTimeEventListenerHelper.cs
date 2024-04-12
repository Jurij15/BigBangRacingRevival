using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000669 RID: 1641
	internal static class RealTimeEventListenerHelper
	{
		// Token: 0x06002FA3 RID: 12195
		[DllImport("gpg")]
		internal static extern void RealTimeEventListenerHelper_SetOnParticipantStatusChangedCallback(HandleRef self, RealTimeEventListenerHelper.OnParticipantStatusChangedCallback callback, IntPtr callback_arg);

		// Token: 0x06002FA4 RID: 12196
		[DllImport("gpg")]
		internal static extern IntPtr RealTimeEventListenerHelper_Construct();

		// Token: 0x06002FA5 RID: 12197
		[DllImport("gpg")]
		internal static extern void RealTimeEventListenerHelper_SetOnP2PDisconnectedCallback(HandleRef self, RealTimeEventListenerHelper.OnP2PDisconnectedCallback callback, IntPtr callback_arg);

		// Token: 0x06002FA6 RID: 12198
		[DllImport("gpg")]
		internal static extern void RealTimeEventListenerHelper_SetOnDataReceivedCallback(HandleRef self, RealTimeEventListenerHelper.OnDataReceivedCallback callback, IntPtr callback_arg);

		// Token: 0x06002FA7 RID: 12199
		[DllImport("gpg")]
		internal static extern void RealTimeEventListenerHelper_SetOnRoomStatusChangedCallback(HandleRef self, RealTimeEventListenerHelper.OnRoomStatusChangedCallback callback, IntPtr callback_arg);

		// Token: 0x06002FA8 RID: 12200
		[DllImport("gpg")]
		internal static extern void RealTimeEventListenerHelper_SetOnP2PConnectedCallback(HandleRef self, RealTimeEventListenerHelper.OnP2PConnectedCallback callback, IntPtr callback_arg);

		// Token: 0x06002FA9 RID: 12201
		[DllImport("gpg")]
		internal static extern void RealTimeEventListenerHelper_SetOnRoomConnectedSetChangedCallback(HandleRef self, RealTimeEventListenerHelper.OnRoomConnectedSetChangedCallback callback, IntPtr callback_arg);

		// Token: 0x06002FAA RID: 12202
		[DllImport("gpg")]
		internal static extern void RealTimeEventListenerHelper_Dispose(HandleRef self);

		// Token: 0x0200066A RID: 1642
		// (Invoke) Token: 0x06002FAC RID: 12204
		internal delegate void OnRoomStatusChangedCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x0200066B RID: 1643
		// (Invoke) Token: 0x06002FB0 RID: 12208
		internal delegate void OnRoomConnectedSetChangedCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x0200066C RID: 1644
		// (Invoke) Token: 0x06002FB4 RID: 12212
		internal delegate void OnP2PConnectedCallback(IntPtr arg0, IntPtr arg1, IntPtr arg2);

		// Token: 0x0200066D RID: 1645
		// (Invoke) Token: 0x06002FB8 RID: 12216
		internal delegate void OnP2PDisconnectedCallback(IntPtr arg0, IntPtr arg1, IntPtr arg2);

		// Token: 0x0200066E RID: 1646
		// (Invoke) Token: 0x06002FBC RID: 12220
		internal delegate void OnParticipantStatusChangedCallback(IntPtr arg0, IntPtr arg1, IntPtr arg2);

		// Token: 0x0200066F RID: 1647
		// (Invoke) Token: 0x06002FC0 RID: 12224
		internal delegate void OnDataReceivedCallback(IntPtr arg0, IntPtr arg1, IntPtr arg2, UIntPtr arg3, [MarshalAs(3)] bool arg4, IntPtr arg5);
	}
}
