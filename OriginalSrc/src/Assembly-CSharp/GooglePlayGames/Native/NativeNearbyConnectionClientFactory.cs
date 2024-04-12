using System;
using GooglePlayGames.Android;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Native
{
	// Token: 0x020006BC RID: 1724
	public class NativeNearbyConnectionClientFactory
	{
		// Token: 0x06003152 RID: 12626 RVA: 0x001C4908 File Offset: 0x001C2D08
		internal static NearbyConnectionsManager GetManager()
		{
			return NativeNearbyConnectionClientFactory.sManager;
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x001C4911 File Offset: 0x001C2D11
		public static void Create(Action<INearbyConnectionClient> callback)
		{
			if (NativeNearbyConnectionClientFactory.sManager == null)
			{
				NativeNearbyConnectionClientFactory.sCreationCallback = callback;
				NativeNearbyConnectionClientFactory.InitializeFactory();
			}
			else
			{
				callback.Invoke(new NativeNearbyConnectionsClient(NativeNearbyConnectionClientFactory.GetManager()));
			}
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x001C4940 File Offset: 0x001C2D40
		internal static void InitializeFactory()
		{
			PlayGamesHelperObject.CreateObject();
			NearbyConnectionsManager.ReadServiceId();
			NearbyConnectionsManagerBuilder nearbyConnectionsManagerBuilder = new NearbyConnectionsManagerBuilder();
			nearbyConnectionsManagerBuilder.SetOnInitializationFinished(new Action<NearbyConnectionsStatus.InitializationStatus>(NativeNearbyConnectionClientFactory.OnManagerInitialized));
			PlatformConfiguration platformConfiguration = new AndroidClient().CreatePlatformConfiguration(PlayGamesClientConfiguration.DefaultConfiguration);
			Debug.Log("Building manager Now");
			NativeNearbyConnectionClientFactory.sManager = nearbyConnectionsManagerBuilder.Build(platformConfiguration);
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x001C49AC File Offset: 0x001C2DAC
		internal static void OnManagerInitialized(NearbyConnectionsStatus.InitializationStatus status)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Nearby Init Complete: ",
				status,
				" sManager = ",
				NativeNearbyConnectionClientFactory.sManager
			}));
			if (status == NearbyConnectionsStatus.InitializationStatus.VALID)
			{
				if (NativeNearbyConnectionClientFactory.sCreationCallback != null)
				{
					NativeNearbyConnectionClientFactory.sCreationCallback.Invoke(new NativeNearbyConnectionsClient(NativeNearbyConnectionClientFactory.GetManager()));
					NativeNearbyConnectionClientFactory.sCreationCallback = null;
				}
			}
			else
			{
				Debug.LogError("ERROR: NearbyConnectionManager not initialized: " + status);
			}
		}

		// Token: 0x04003279 RID: 12921
		private static volatile NearbyConnectionsManager sManager;

		// Token: 0x0400327A RID: 12922
		private static Action<INearbyConnectionClient> sCreationCallback;
	}
}
