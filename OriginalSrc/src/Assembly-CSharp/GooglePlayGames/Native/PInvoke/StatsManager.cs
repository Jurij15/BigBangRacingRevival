using System;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x0200071E RID: 1822
	internal class StatsManager
	{
		// Token: 0x060034B8 RID: 13496 RVA: 0x001CE964 File Offset: 0x001CCD64
		internal StatsManager(GameServices services)
		{
			this.mServices = Misc.CheckNotNull<GameServices>(services);
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x001CE978 File Offset: 0x001CCD78
		internal void FetchForPlayer(Action<StatsManager.FetchForPlayerResponse> callback)
		{
			Misc.CheckNotNull<Action<StatsManager.FetchForPlayerResponse>>(callback);
			StatsManager.StatsManager_FetchForPlayer(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, new StatsManager.FetchForPlayerCallback(StatsManager.InternalFetchForPlayerCallback), Callbacks.ToIntPtr<StatsManager.FetchForPlayerResponse>(callback, new Func<IntPtr, StatsManager.FetchForPlayerResponse>(StatsManager.FetchForPlayerResponse.FromPointer)));
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x001CE9DD File Offset: 0x001CCDDD
		[MonoPInvokeCallback(typeof(StatsManager.FetchForPlayerCallback))]
		private static void InternalFetchForPlayerCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("StatsManager#InternalFetchForPlayerCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x0400331D RID: 13085
		private readonly GameServices mServices;

		// Token: 0x0200071F RID: 1823
		internal class FetchForPlayerResponse : BaseReferenceHolder
		{
			// Token: 0x060034BB RID: 13499 RVA: 0x001CE9EC File Offset: 0x001CCDEC
			internal FetchForPlayerResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x060034BC RID: 13500 RVA: 0x001CE9F5 File Offset: 0x001CCDF5
			internal CommonErrorStatus.ResponseStatus Status()
			{
				return StatsManager.StatsManager_FetchForPlayerResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x060034BD RID: 13501 RVA: 0x001CEA04 File Offset: 0x001CCE04
			internal NativePlayerStats PlayerStats()
			{
				IntPtr intPtr = StatsManager.StatsManager_FetchForPlayerResponse_GetData(base.SelfPtr());
				return new NativePlayerStats(intPtr);
			}

			// Token: 0x060034BE RID: 13502 RVA: 0x001CEA23 File Offset: 0x001CCE23
			protected override void CallDispose(HandleRef selfPointer)
			{
				StatsManager.StatsManager_FetchForPlayerResponse_Dispose(selfPointer);
			}

			// Token: 0x060034BF RID: 13503 RVA: 0x001CEA2B File Offset: 0x001CCE2B
			internal static StatsManager.FetchForPlayerResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new StatsManager.FetchForPlayerResponse(pointer);
			}
		}
	}
}
