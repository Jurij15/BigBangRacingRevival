using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000652 RID: 1618
	internal static class MessageListenerHelper
	{
		// Token: 0x06002F0D RID: 12045
		[DllImport("gpg")]
		internal static extern void MessageListenerHelper_SetOnMessageReceivedCallback(HandleRef self, MessageListenerHelper.OnMessageReceivedCallback callback, IntPtr callback_arg);

		// Token: 0x06002F0E RID: 12046
		[DllImport("gpg")]
		internal static extern void MessageListenerHelper_SetOnDisconnectedCallback(HandleRef self, MessageListenerHelper.OnDisconnectedCallback callback, IntPtr callback_arg);

		// Token: 0x06002F0F RID: 12047
		[DllImport("gpg")]
		internal static extern IntPtr MessageListenerHelper_Construct();

		// Token: 0x06002F10 RID: 12048
		[DllImport("gpg")]
		internal static extern void MessageListenerHelper_Dispose(HandleRef self);

		// Token: 0x02000653 RID: 1619
		// (Invoke) Token: 0x06002F12 RID: 12050
		internal delegate void OnMessageReceivedCallback(long arg0, string arg1, IntPtr arg2, UIntPtr arg3, [MarshalAs(3)] bool arg4, IntPtr arg5);

		// Token: 0x02000654 RID: 1620
		// (Invoke) Token: 0x06002F16 RID: 12054
		internal delegate void OnDisconnectedCallback(long arg0, string arg1, IntPtr arg2);
	}
}
