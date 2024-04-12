using System;
using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames
{
	// Token: 0x0200072C RID: 1836
	public static class NearbyConnectionClientFactory
	{
		// Token: 0x0600352D RID: 13613 RVA: 0x001CF600 File Offset: 0x001CDA00
		public static void Create(Action<INearbyConnectionClient> callback)
		{
			if (Application.isEditor)
			{
				Logger.d("Creating INearbyConnection in editor, using DummyClient.");
				callback.Invoke(new DummyNearbyConnectionClient());
			}
			Logger.d("Creating real INearbyConnectionClient");
			NativeNearbyConnectionClientFactory.Create(callback);
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x001CF634 File Offset: 0x001CDA34
		private static InitializationStatus ToStatus(NearbyConnectionsStatus.InitializationStatus status)
		{
			switch (status + 4)
			{
			case (NearbyConnectionsStatus.InitializationStatus)0:
				return InitializationStatus.VersionUpdateRequired;
			case (NearbyConnectionsStatus.InitializationStatus)2:
				return InitializationStatus.InternalError;
			case (NearbyConnectionsStatus.InitializationStatus)5:
				return InitializationStatus.Success;
			}
			Logger.w("Unknown initialization status: " + status);
			return InitializationStatus.InternalError;
		}
	}
}
