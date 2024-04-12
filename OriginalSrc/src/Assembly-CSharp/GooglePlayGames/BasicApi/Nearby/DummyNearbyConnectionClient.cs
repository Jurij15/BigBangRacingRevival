using System;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020005E6 RID: 1510
	public class DummyNearbyConnectionClient : INearbyConnectionClient
	{
		// Token: 0x06002BE9 RID: 11241 RVA: 0x001BDB32 File Offset: 0x001BBF32
		public int MaxUnreliableMessagePayloadLength()
		{
			return 1168;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x001BDB39 File Offset: 0x001BBF39
		public int MaxReliableMessagePayloadLength()
		{
			return 4096;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x001BDB40 File Offset: 0x001BBF40
		public void SendReliable(List<string> recipientEndpointIds, byte[] payload)
		{
			Debug.LogError("SendReliable called from dummy implementation");
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x001BDB4C File Offset: 0x001BBF4C
		public void SendUnreliable(List<string> recipientEndpointIds, byte[] payload)
		{
			Debug.LogError("SendUnreliable called from dummy implementation");
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x001BDB58 File Offset: 0x001BBF58
		public void StartAdvertising(string name, List<string> appIdentifiers, TimeSpan? advertisingDuration, Action<AdvertisingResult> resultCallback, Action<ConnectionRequest> connectionRequestCallback)
		{
			AdvertisingResult advertisingResult = new AdvertisingResult(ResponseStatus.LicenseCheckFailed, string.Empty);
			resultCallback.Invoke(advertisingResult);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x001BDB7A File Offset: 0x001BBF7A
		public void StopAdvertising()
		{
			Debug.LogError("StopAvertising in dummy implementation called");
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x001BDB88 File Offset: 0x001BBF88
		public void SendConnectionRequest(string name, string remoteEndpointId, byte[] payload, Action<ConnectionResponse> responseCallback, IMessageListener listener)
		{
			Debug.LogError("SendConnectionRequest called from dummy implementation");
			if (responseCallback != null)
			{
				ConnectionResponse connectionResponse = ConnectionResponse.Rejected(0L, string.Empty);
				responseCallback.Invoke(connectionResponse);
			}
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x001BDBBB File Offset: 0x001BBFBB
		public void AcceptConnectionRequest(string remoteEndpointId, byte[] payload, IMessageListener listener)
		{
			Debug.LogError("AcceptConnectionRequest in dummy implementation called");
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x001BDBC7 File Offset: 0x001BBFC7
		public void StartDiscovery(string serviceId, TimeSpan? advertisingTimeout, IDiscoveryListener listener)
		{
			Debug.LogError("StartDiscovery in dummy implementation called");
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x001BDBD3 File Offset: 0x001BBFD3
		public void StopDiscovery(string serviceId)
		{
			Debug.LogError("StopDiscovery in dummy implementation called");
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x001BDBDF File Offset: 0x001BBFDF
		public void RejectConnectionRequest(string requestingEndpointId)
		{
			Debug.LogError("RejectConnectionRequest in dummy implementation called");
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x001BDBEB File Offset: 0x001BBFEB
		public void DisconnectFromEndpoint(string remoteEndpointId)
		{
			Debug.LogError("DisconnectFromEndpoint in dummy implementation called");
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x001BDBF7 File Offset: 0x001BBFF7
		public void StopAllConnections()
		{
			Debug.LogError("StopAllConnections in dummy implementation called");
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x001BDC03 File Offset: 0x001BC003
		public string LocalEndpointId()
		{
			return string.Empty;
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x001BDC0A File Offset: 0x001BC00A
		public string LocalDeviceId()
		{
			return "DummyDevice";
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x001BDC11 File Offset: 0x001BC011
		public string GetAppBundleId()
		{
			return "dummy.bundle.id";
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x001BDC18 File Offset: 0x001BC018
		public string GetServiceId()
		{
			return "dummy.service.id";
		}
	}
}
