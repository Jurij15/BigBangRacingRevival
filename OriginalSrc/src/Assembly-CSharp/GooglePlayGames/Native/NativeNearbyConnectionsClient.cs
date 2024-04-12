using System;
using System.Collections.Generic;
using System.Linq;
using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native
{
	// Token: 0x020006BD RID: 1725
	internal class NativeNearbyConnectionsClient : INearbyConnectionClient
	{
		// Token: 0x06003156 RID: 12630 RVA: 0x001C4A31 File Offset: 0x001C2E31
		internal NativeNearbyConnectionsClient(NearbyConnectionsManager manager)
		{
			this.mManager = Misc.CheckNotNull<NearbyConnectionsManager>(manager);
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x001C4A45 File Offset: 0x001C2E45
		public int MaxUnreliableMessagePayloadLength()
		{
			return 1168;
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x001C4A4C File Offset: 0x001C2E4C
		public int MaxReliableMessagePayloadLength()
		{
			return 4096;
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x001C4A53 File Offset: 0x001C2E53
		public void SendReliable(List<string> recipientEndpointIds, byte[] payload)
		{
			this.InternalSend(recipientEndpointIds, payload, true);
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x001C4A5E File Offset: 0x001C2E5E
		public void SendUnreliable(List<string> recipientEndpointIds, byte[] payload)
		{
			this.InternalSend(recipientEndpointIds, payload, false);
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x001C4A6C File Offset: 0x001C2E6C
		private void InternalSend(List<string> recipientEndpointIds, byte[] payload, bool isReliable)
		{
			if (recipientEndpointIds == null)
			{
				throw new ArgumentNullException("recipientEndpointIds");
			}
			if (payload == null)
			{
				throw new ArgumentNullException("payload");
			}
			if (recipientEndpointIds.Contains(null))
			{
				throw new InvalidOperationException("Cannot send a message to a null recipient");
			}
			if (recipientEndpointIds.Count == 0)
			{
				Logger.w("Attempted to send a reliable message with no recipients");
				return;
			}
			if (isReliable)
			{
				if (payload.Length > this.MaxReliableMessagePayloadLength())
				{
					throw new InvalidOperationException("cannot send more than " + this.MaxReliableMessagePayloadLength() + " bytes");
				}
			}
			else if (payload.Length > this.MaxUnreliableMessagePayloadLength())
			{
				throw new InvalidOperationException("cannot send more than " + this.MaxUnreliableMessagePayloadLength() + " bytes");
			}
			foreach (string text in recipientEndpointIds)
			{
				if (isReliable)
				{
					this.mManager.SendReliable(text, payload);
				}
				else
				{
					this.mManager.SendUnreliable(text, payload);
				}
			}
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x001C4B98 File Offset: 0x001C2F98
		public void StartAdvertising(string name, List<string> appIdentifiers, TimeSpan? advertisingDuration, Action<AdvertisingResult> resultCallback, Action<ConnectionRequest> requestCallback)
		{
			Misc.CheckNotNull<List<string>>(appIdentifiers, "appIdentifiers");
			Misc.CheckNotNull<Action<AdvertisingResult>>(resultCallback, "resultCallback");
			Misc.CheckNotNull<Action<ConnectionRequest>>(requestCallback, "connectionRequestCallback");
			if (advertisingDuration != null && advertisingDuration.Value.Ticks < 0L)
			{
				throw new InvalidOperationException("advertisingDuration must be positive");
			}
			resultCallback = Callbacks.AsOnGameThreadCallback<AdvertisingResult>(resultCallback);
			requestCallback = Callbacks.AsOnGameThreadCallback<ConnectionRequest>(requestCallback);
			this.mManager.StartAdvertising(name, Enumerable.ToList<NativeAppIdentifier>(Enumerable.Select<string, NativeAppIdentifier>(appIdentifiers, new Func<string, NativeAppIdentifier>(NativeAppIdentifier.FromString))), NativeNearbyConnectionsClient.ToTimeoutMillis(advertisingDuration), delegate(long localClientId, NativeStartAdvertisingResult result)
			{
				resultCallback.Invoke(result.AsResult());
			}, delegate(long localClientId, NativeConnectionRequest request)
			{
				requestCallback.Invoke(request.AsRequest());
			});
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x001C4C8A File Offset: 0x001C308A
		private static long ToTimeoutMillis(TimeSpan? span)
		{
			return (span == null) ? 0L : PInvokeUtilities.ToMilliseconds(span.Value);
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x001C4CAB File Offset: 0x001C30AB
		public void StopAdvertising()
		{
			this.mManager.StopAdvertising();
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x001C4CB8 File Offset: 0x001C30B8
		public void SendConnectionRequest(string name, string remoteEndpointId, byte[] payload, Action<ConnectionResponse> responseCallback, IMessageListener listener)
		{
			Misc.CheckNotNull<string>(remoteEndpointId, "remoteEndpointId");
			Misc.CheckNotNull<byte[]>(payload, "payload");
			Misc.CheckNotNull<Action<ConnectionResponse>>(responseCallback, "responseCallback");
			Misc.CheckNotNull<IMessageListener>(listener, "listener");
			responseCallback = Callbacks.AsOnGameThreadCallback<ConnectionResponse>(responseCallback);
			using (NativeMessageListenerHelper nativeMessageListenerHelper = NativeNearbyConnectionsClient.ToMessageListener(listener))
			{
				this.mManager.SendConnectionRequest(name, remoteEndpointId, payload, delegate(long localClientId, NativeConnectionResponse response)
				{
					responseCallback.Invoke(response.AsResponse(localClientId));
				}, nativeMessageListenerHelper);
			}
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x001C4D60 File Offset: 0x001C3160
		private static NativeMessageListenerHelper ToMessageListener(IMessageListener listener)
		{
			listener = new NativeNearbyConnectionsClient.OnGameThreadMessageListener(listener);
			NativeMessageListenerHelper nativeMessageListenerHelper = new NativeMessageListenerHelper();
			nativeMessageListenerHelper.SetOnMessageReceivedCallback(delegate(long localClientId, string endpointId, byte[] data, bool isReliable)
			{
				listener.OnMessageReceived(endpointId, data, isReliable);
			});
			nativeMessageListenerHelper.SetOnDisconnectedCallback(delegate(long localClientId, string endpointId)
			{
				listener.OnRemoteEndpointDisconnected(endpointId);
			});
			return nativeMessageListenerHelper;
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x001C4DB8 File Offset: 0x001C31B8
		public void AcceptConnectionRequest(string remoteEndpointId, byte[] payload, IMessageListener listener)
		{
			Misc.CheckNotNull<string>(remoteEndpointId, "remoteEndpointId");
			Misc.CheckNotNull<byte[]>(payload, "payload");
			Misc.CheckNotNull<IMessageListener>(listener, "listener");
			Logger.d("Calling AcceptConncectionRequest");
			this.mManager.AcceptConnectionRequest(remoteEndpointId, payload, NativeNearbyConnectionsClient.ToMessageListener(listener));
			Logger.d("Called!");
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x001C4E10 File Offset: 0x001C3210
		public void StartDiscovery(string serviceId, TimeSpan? advertisingTimeout, IDiscoveryListener listener)
		{
			Misc.CheckNotNull<string>(serviceId, "serviceId");
			Misc.CheckNotNull<IDiscoveryListener>(listener, "listener");
			using (NativeEndpointDiscoveryListenerHelper nativeEndpointDiscoveryListenerHelper = NativeNearbyConnectionsClient.ToDiscoveryListener(listener))
			{
				this.mManager.StartDiscovery(serviceId, NativeNearbyConnectionsClient.ToTimeoutMillis(advertisingTimeout), nativeEndpointDiscoveryListenerHelper);
			}
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x001C4E74 File Offset: 0x001C3274
		private static NativeEndpointDiscoveryListenerHelper ToDiscoveryListener(IDiscoveryListener listener)
		{
			listener = new NativeNearbyConnectionsClient.OnGameThreadDiscoveryListener(listener);
			NativeEndpointDiscoveryListenerHelper nativeEndpointDiscoveryListenerHelper = new NativeEndpointDiscoveryListenerHelper();
			nativeEndpointDiscoveryListenerHelper.SetOnEndpointFound(delegate(long localClientId, NativeEndpointDetails endpoint)
			{
				listener.OnEndpointFound(endpoint.ToDetails());
			});
			nativeEndpointDiscoveryListenerHelper.SetOnEndpointLostCallback(delegate(long localClientId, string lostEndpointId)
			{
				listener.OnEndpointLost(lostEndpointId);
			});
			return nativeEndpointDiscoveryListenerHelper;
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x001C4ECA File Offset: 0x001C32CA
		public void StopDiscovery(string serviceId)
		{
			Misc.CheckNotNull<string>(serviceId, "serviceId");
			this.mManager.StopDiscovery(serviceId);
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x001C4EE4 File Offset: 0x001C32E4
		public void RejectConnectionRequest(string requestingEndpointId)
		{
			Misc.CheckNotNull<string>(requestingEndpointId, "requestingEndpointId");
			this.mManager.RejectConnectionRequest(requestingEndpointId);
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x001C4EFE File Offset: 0x001C32FE
		public void DisconnectFromEndpoint(string remoteEndpointId)
		{
			this.mManager.DisconnectFromEndpoint(remoteEndpointId);
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x001C4F0C File Offset: 0x001C330C
		public void StopAllConnections()
		{
			this.mManager.StopAllConnections();
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x001C4F19 File Offset: 0x001C3319
		public string GetAppBundleId()
		{
			return this.mManager.AppBundleId;
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x001C4F26 File Offset: 0x001C3326
		public string GetServiceId()
		{
			return NearbyConnectionsManager.ServiceId;
		}

		// Token: 0x0400327C RID: 12924
		private readonly NearbyConnectionsManager mManager;

		// Token: 0x020006BE RID: 1726
		protected class OnGameThreadMessageListener : IMessageListener
		{
			// Token: 0x0600316A RID: 12650 RVA: 0x001C4F2D File Offset: 0x001C332D
			public OnGameThreadMessageListener(IMessageListener listener)
			{
				this.mListener = Misc.CheckNotNull<IMessageListener>(listener);
			}

			// Token: 0x0600316B RID: 12651 RVA: 0x001C4F44 File Offset: 0x001C3344
			public void OnMessageReceived(string remoteEndpointId, byte[] data, bool isReliableMessage)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnMessageReceived(remoteEndpointId, data, isReliableMessage);
				});
			}

			// Token: 0x0600316C RID: 12652 RVA: 0x001C4F84 File Offset: 0x001C3384
			public void OnRemoteEndpointDisconnected(string remoteEndpointId)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnRemoteEndpointDisconnected(remoteEndpointId);
				});
			}

			// Token: 0x0400327E RID: 12926
			private readonly IMessageListener mListener;
		}

		// Token: 0x020006BF RID: 1727
		protected class OnGameThreadDiscoveryListener : IDiscoveryListener
		{
			// Token: 0x0600316D RID: 12653 RVA: 0x001C5002 File Offset: 0x001C3402
			public OnGameThreadDiscoveryListener(IDiscoveryListener listener)
			{
				this.mListener = Misc.CheckNotNull<IDiscoveryListener>(listener);
			}

			// Token: 0x0600316E RID: 12654 RVA: 0x001C5018 File Offset: 0x001C3418
			public void OnEndpointFound(EndpointDetails discoveredEndpoint)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnEndpointFound(discoveredEndpoint);
				});
			}

			// Token: 0x0600316F RID: 12655 RVA: 0x001C504C File Offset: 0x001C344C
			public void OnEndpointLost(string lostEndpointId)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnEndpointLost(lostEndpointId);
				});
			}

			// Token: 0x0400327F RID: 12927
			private readonly IDiscoveryListener mListener;
		}
	}
}
