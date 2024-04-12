using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using UnityEngine;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000703 RID: 1795
	internal class NearbyConnectionsManager : BaseReferenceHolder
	{
		// Token: 0x060033DE RID: 13278 RVA: 0x001CCAB7 File Offset: 0x001CAEB7
		internal NearbyConnectionsManager(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x001CCAC0 File Offset: 0x001CAEC0
		protected override void CallDispose(HandleRef selfPointer)
		{
			NearbyConnections.NearbyConnections_Dispose(selfPointer);
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x001CCAC8 File Offset: 0x001CAEC8
		internal void SendUnreliable(string remoteEndpointId, byte[] payload)
		{
			NearbyConnections.NearbyConnections_SendUnreliableMessage(base.SelfPtr(), remoteEndpointId, payload, new UIntPtr((ulong)((long)payload.Length)));
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x001CCAE0 File Offset: 0x001CAEE0
		internal void SendReliable(string remoteEndpointId, byte[] payload)
		{
			NearbyConnections.NearbyConnections_SendReliableMessage(base.SelfPtr(), remoteEndpointId, payload, new UIntPtr((ulong)((long)payload.Length)));
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x001CCAF8 File Offset: 0x001CAEF8
		internal void StartAdvertising(string name, List<NativeAppIdentifier> appIds, long advertisingDuration, Action<long, NativeStartAdvertisingResult> advertisingCallback, Action<long, NativeConnectionRequest> connectionRequestCallback)
		{
			NearbyConnections.NearbyConnections_StartAdvertising(base.SelfPtr(), name, Enumerable.ToArray<IntPtr>(Enumerable.Select<NativeAppIdentifier, IntPtr>(appIds, (NativeAppIdentifier id) => id.AsPointer())), new UIntPtr((ulong)((long)appIds.Count)), advertisingDuration, new NearbyConnectionTypes.StartAdvertisingCallback(NearbyConnectionsManager.InternalStartAdvertisingCallback), Callbacks.ToIntPtr<long, NativeStartAdvertisingResult>(advertisingCallback, new Func<IntPtr, NativeStartAdvertisingResult>(NativeStartAdvertisingResult.FromPointer)), new NearbyConnectionTypes.ConnectionRequestCallback(NearbyConnectionsManager.InternalConnectionRequestCallback), Callbacks.ToIntPtr<long, NativeConnectionRequest>(connectionRequestCallback, new Func<IntPtr, NativeConnectionRequest>(NativeConnectionRequest.FromPointer)));
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x001CCBC8 File Offset: 0x001CAFC8
		[MonoPInvokeCallback(typeof(NearbyConnectionTypes.StartAdvertisingCallback))]
		private static void InternalStartAdvertisingCallback(long id, IntPtr result, IntPtr userData)
		{
			Callbacks.PerformInternalCallback<long>("NearbyConnectionsManager#InternalStartAdvertisingCallback", Callbacks.Type.Permanent, id, result, userData);
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x001CCBD8 File Offset: 0x001CAFD8
		[MonoPInvokeCallback(typeof(NearbyConnectionTypes.ConnectionRequestCallback))]
		private static void InternalConnectionRequestCallback(long id, IntPtr result, IntPtr userData)
		{
			Callbacks.PerformInternalCallback<long>("NearbyConnectionsManager#InternalConnectionRequestCallback", Callbacks.Type.Permanent, id, result, userData);
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x001CCBE8 File Offset: 0x001CAFE8
		internal void StopAdvertising()
		{
			NearbyConnections.NearbyConnections_StopAdvertising(base.SelfPtr());
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x001CCBF8 File Offset: 0x001CAFF8
		internal void SendConnectionRequest(string name, string remoteEndpointId, byte[] payload, Action<long, NativeConnectionResponse> callback, NativeMessageListenerHelper listener)
		{
			NearbyConnections.NearbyConnections_SendConnectionRequest(base.SelfPtr(), name, remoteEndpointId, payload, new UIntPtr((ulong)((long)payload.Length)), new NearbyConnectionTypes.ConnectionResponseCallback(NearbyConnectionsManager.InternalConnectResponseCallback), Callbacks.ToIntPtr<long, NativeConnectionResponse>(callback, new Func<IntPtr, NativeConnectionResponse>(NativeConnectionResponse.FromPointer)), listener.AsPointer());
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x001CCC64 File Offset: 0x001CB064
		[MonoPInvokeCallback(typeof(NearbyConnectionTypes.ConnectionResponseCallback))]
		private static void InternalConnectResponseCallback(long localClientId, IntPtr response, IntPtr userData)
		{
			Callbacks.PerformInternalCallback<long>("NearbyConnectionManager#InternalConnectResponseCallback", Callbacks.Type.Temporary, localClientId, response, userData);
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x001CCC74 File Offset: 0x001CB074
		internal void AcceptConnectionRequest(string remoteEndpointId, byte[] payload, NativeMessageListenerHelper listener)
		{
			NearbyConnections.NearbyConnections_AcceptConnectionRequest(base.SelfPtr(), remoteEndpointId, payload, new UIntPtr((ulong)((long)payload.Length)), listener.AsPointer());
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x001CCC92 File Offset: 0x001CB092
		internal void DisconnectFromEndpoint(string remoteEndpointId)
		{
			NearbyConnections.NearbyConnections_Disconnect(base.SelfPtr(), remoteEndpointId);
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x001CCCA0 File Offset: 0x001CB0A0
		internal void StopAllConnections()
		{
			NearbyConnections.NearbyConnections_Stop(base.SelfPtr());
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x001CCCAD File Offset: 0x001CB0AD
		internal void StartDiscovery(string serviceId, long duration, NativeEndpointDiscoveryListenerHelper listener)
		{
			NearbyConnections.NearbyConnections_StartDiscovery(base.SelfPtr(), serviceId, duration, listener.AsPointer());
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x001CCCC2 File Offset: 0x001CB0C2
		internal void StopDiscovery(string serviceId)
		{
			NearbyConnections.NearbyConnections_StopDiscovery(base.SelfPtr(), serviceId);
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x001CCCD0 File Offset: 0x001CB0D0
		internal void RejectConnectionRequest(string remoteEndpointId)
		{
			NearbyConnections.NearbyConnections_RejectConnectionRequest(base.SelfPtr(), remoteEndpointId);
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x001CCCDE File Offset: 0x001CB0DE
		internal void Shutdown()
		{
			NearbyConnections.NearbyConnections_Stop(base.SelfPtr());
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x001CCCEC File Offset: 0x001CB0EC
		public string AppBundleId
		{
			get
			{
				string text;
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
					text = @static.Call<string>("getPackageName", new object[0]);
				}
				return text;
			}
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x001CCD48 File Offset: 0x001CB148
		internal static string ReadServiceId()
		{
			Debug.Log("Initializing ServiceId property!!!!");
			string text3;
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					string text = @static.Call<string>("getPackageName", new object[0]);
					AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getPackageManager", new object[0]);
					AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getApplicationInfo", new object[] { text, 128 });
					AndroidJavaObject androidJavaObject3 = androidJavaObject2.Get<AndroidJavaObject>("metaData");
					string text2 = androidJavaObject3.Call<string>("getString", new object[] { "com.google.android.gms.nearby.connection.SERVICE_ID" });
					Debug.Log("SystemId from Manifest: " + text2);
					text3 = text2;
				}
			}
			return text3;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x001CCE3C File Offset: 0x001CB23C
		public static string ServiceId
		{
			get
			{
				return NearbyConnectionsManager.sServiceId;
			}
		}

		// Token: 0x040032E5 RID: 13029
		private static readonly string sServiceId = NearbyConnectionsManager.ReadServiceId();
	}
}
