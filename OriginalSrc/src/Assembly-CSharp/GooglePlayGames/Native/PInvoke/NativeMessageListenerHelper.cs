using System;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006F2 RID: 1778
	internal class NativeMessageListenerHelper : BaseReferenceHolder
	{
		// Token: 0x06003342 RID: 13122 RVA: 0x001CBBCF File Offset: 0x001C9FCF
		internal NativeMessageListenerHelper()
			: base(MessageListenerHelper.MessageListenerHelper_Construct())
		{
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x001CBBDC File Offset: 0x001C9FDC
		protected override void CallDispose(HandleRef selfPointer)
		{
			MessageListenerHelper.MessageListenerHelper_Dispose(selfPointer);
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x001CBBE4 File Offset: 0x001C9FE4
		internal void SetOnMessageReceivedCallback(NativeMessageListenerHelper.OnMessageReceived callback)
		{
			MessageListenerHelper.MessageListenerHelper_SetOnMessageReceivedCallback(base.SelfPtr(), new MessageListenerHelper.OnMessageReceivedCallback(NativeMessageListenerHelper.InternalOnMessageReceivedCallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x001CBC14 File Offset: 0x001CA014
		[MonoPInvokeCallback(typeof(MessageListenerHelper.OnMessageReceivedCallback))]
		private static void InternalOnMessageReceivedCallback(long id, string name, IntPtr data, UIntPtr dataLength, bool isReliable, IntPtr userData)
		{
			NativeMessageListenerHelper.OnMessageReceived onMessageReceived = Callbacks.IntPtrToPermanentCallback<NativeMessageListenerHelper.OnMessageReceived>(userData);
			if (onMessageReceived == null)
			{
				return;
			}
			try
			{
				onMessageReceived(id, name, Callbacks.IntPtrAndSizeToByteArray(data, dataLength), isReliable);
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered executing NativeMessageListenerHelper#InternalOnMessageReceivedCallback. Smothering to avoid passing exception into Native: " + ex);
			}
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x001CBC6C File Offset: 0x001CA06C
		internal void SetOnDisconnectedCallback(Action<long, string> callback)
		{
			MessageListenerHelper.MessageListenerHelper_SetOnDisconnectedCallback(base.SelfPtr(), new MessageListenerHelper.OnDisconnectedCallback(NativeMessageListenerHelper.InternalOnDisconnectedCallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x001CBC9C File Offset: 0x001CA09C
		[MonoPInvokeCallback(typeof(MessageListenerHelper.OnDisconnectedCallback))]
		private static void InternalOnDisconnectedCallback(long id, string lostEndpointId, IntPtr userData)
		{
			Action<long, string> action = Callbacks.IntPtrToPermanentCallback<Action<long, string>>(userData);
			if (action == null)
			{
				return;
			}
			try
			{
				action.Invoke(id, lostEndpointId);
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered executing NativeMessageListenerHelper#InternalOnDisconnectedCallback. Smothering to avoid passing exception into Native: " + ex);
			}
		}

		// Token: 0x020006F3 RID: 1779
		// (Invoke) Token: 0x06003349 RID: 13129
		internal delegate void OnMessageReceived(long localClientId, string remoteEndpointId, byte[] data, bool isReliable);
	}
}
