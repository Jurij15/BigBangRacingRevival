using System;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006EF RID: 1775
	internal class NativeEndpointDiscoveryListenerHelper : BaseReferenceHolder
	{
		// Token: 0x0600332A RID: 13098 RVA: 0x001CB919 File Offset: 0x001C9D19
		internal NativeEndpointDiscoveryListenerHelper()
			: base(EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_Construct())
		{
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x001CB926 File Offset: 0x001C9D26
		protected override void CallDispose(HandleRef selfPointer)
		{
			EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_Dispose(selfPointer);
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x001CB930 File Offset: 0x001C9D30
		internal void SetOnEndpointFound(Action<long, NativeEndpointDetails> callback)
		{
			EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_SetOnEndpointFoundCallback(base.SelfPtr(), new EndpointDiscoveryListenerHelper.OnEndpointFoundCallback(NativeEndpointDiscoveryListenerHelper.InternalOnEndpointFoundCallback), Callbacks.ToIntPtr<long, NativeEndpointDetails>(callback, new Func<IntPtr, NativeEndpointDetails>(NativeEndpointDetails.FromPointer)));
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x001CB988 File Offset: 0x001C9D88
		[MonoPInvokeCallback(typeof(EndpointDiscoveryListenerHelper.OnEndpointFoundCallback))]
		private static void InternalOnEndpointFoundCallback(long id, IntPtr data, IntPtr userData)
		{
			Callbacks.PerformInternalCallback<long>("NativeEndpointDiscoveryListenerHelper#InternalOnEndpointFoundCallback", Callbacks.Type.Permanent, id, data, userData);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x001CB998 File Offset: 0x001C9D98
		internal void SetOnEndpointLostCallback(Action<long, string> callback)
		{
			EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_SetOnEndpointLostCallback(base.SelfPtr(), new EndpointDiscoveryListenerHelper.OnEndpointLostCallback(NativeEndpointDiscoveryListenerHelper.InternalOnEndpointLostCallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x001CB9C8 File Offset: 0x001C9DC8
		[MonoPInvokeCallback(typeof(EndpointDiscoveryListenerHelper.OnEndpointLostCallback))]
		private static void InternalOnEndpointLostCallback(long id, string lostEndpointId, IntPtr userData)
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
				Logger.e("Error encountered executing NativeEndpointDiscoveryListenerHelper#InternalOnEndpointLostCallback. Smothering to avoid passing exception into Native: " + ex);
			}
		}
	}
}
