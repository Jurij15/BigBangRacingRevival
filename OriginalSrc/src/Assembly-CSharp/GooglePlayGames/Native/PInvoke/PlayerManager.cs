using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x0200070A RID: 1802
	internal class PlayerManager
	{
		// Token: 0x06003416 RID: 13334 RVA: 0x001CD2AC File Offset: 0x001CB6AC
		internal PlayerManager(GameServices services)
		{
			this.mGameServices = Misc.CheckNotNull<GameServices>(services);
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x001CD2C0 File Offset: 0x001CB6C0
		internal void FetchSelf(Action<PlayerManager.FetchSelfResponse> callback)
		{
			PlayerManager.PlayerManager_FetchSelf(this.mGameServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, new PlayerManager.FetchSelfCallback(PlayerManager.InternalFetchSelfCallback), Callbacks.ToIntPtr<PlayerManager.FetchSelfResponse>(callback, new Func<IntPtr, PlayerManager.FetchSelfResponse>(PlayerManager.FetchSelfResponse.FromPointer)));
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x001CD31E File Offset: 0x001CB71E
		[MonoPInvokeCallback(typeof(PlayerManager.FetchSelfCallback))]
		private static void InternalFetchSelfCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("PlayerManager#InternalFetchSelfCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x001CD330 File Offset: 0x001CB730
		internal void FetchList(string[] userIds, Action<NativePlayer[]> callback)
		{
			PlayerManager.FetchResponseCollector coll = new PlayerManager.FetchResponseCollector();
			coll.pendingCount = userIds.Length;
			coll.callback = callback;
			foreach (string text in userIds)
			{
				PlayerManager.PlayerManager_Fetch(this.mGameServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, text, new PlayerManager.FetchCallback(PlayerManager.InternalFetchCallback), Callbacks.ToIntPtr<PlayerManager.FetchResponse>(delegate(PlayerManager.FetchResponse rsp)
				{
					this.HandleFetchResponse(coll, rsp);
				}, new Func<IntPtr, PlayerManager.FetchResponse>(PlayerManager.FetchResponse.FromPointer)));
			}
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x001CD3E6 File Offset: 0x001CB7E6
		[MonoPInvokeCallback(typeof(PlayerManager.FetchCallback))]
		private static void InternalFetchCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("PlayerManager#InternalFetchCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x001CD3F8 File Offset: 0x001CB7F8
		internal void HandleFetchResponse(PlayerManager.FetchResponseCollector collector, PlayerManager.FetchResponse resp)
		{
			if (resp.Status() == CommonErrorStatus.ResponseStatus.VALID || resp.Status() == CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
			{
				NativePlayer player = resp.GetPlayer();
				collector.results.Add(player);
			}
			collector.pendingCount--;
			if (collector.pendingCount == 0)
			{
				collector.callback.Invoke(collector.results.ToArray());
			}
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x001CD460 File Offset: 0x001CB860
		internal void FetchFriends(Action<ResponseStatus, List<GooglePlayGames.BasicApi.Multiplayer.Player>> callback)
		{
			PlayerManager.PlayerManager_FetchConnected(this.mGameServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, new PlayerManager.FetchListCallback(PlayerManager.InternalFetchConnectedCallback), Callbacks.ToIntPtr<PlayerManager.FetchListResponse>(delegate(PlayerManager.FetchListResponse rsp)
			{
				this.HandleFetchCollected(rsp, callback);
			}, new Func<IntPtr, PlayerManager.FetchListResponse>(PlayerManager.FetchListResponse.FromPointer)));
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x001CD4DD File Offset: 0x001CB8DD
		[MonoPInvokeCallback(typeof(PlayerManager.FetchListCallback))]
		private static void InternalFetchConnectedCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("PlayerManager#InternalFetchConnectedCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x001CD4EC File Offset: 0x001CB8EC
		internal void HandleFetchCollected(PlayerManager.FetchListResponse rsp, Action<ResponseStatus, List<GooglePlayGames.BasicApi.Multiplayer.Player>> callback)
		{
			List<GooglePlayGames.BasicApi.Multiplayer.Player> list = new List<GooglePlayGames.BasicApi.Multiplayer.Player>();
			if (rsp.Status() == CommonErrorStatus.ResponseStatus.VALID || rsp.Status() == CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
			{
				Logger.d("Got " + rsp.Length().ToUInt64() + " players");
				foreach (NativePlayer nativePlayer in rsp)
				{
					list.Add(nativePlayer.AsPlayer());
				}
			}
			callback.Invoke((ResponseStatus)rsp.Status(), list);
		}

		// Token: 0x040032EF RID: 13039
		private readonly GameServices mGameServices;

		// Token: 0x0200070B RID: 1803
		internal class FetchListResponse : BaseReferenceHolder, IEnumerable<NativePlayer>, IEnumerable
		{
			// Token: 0x0600341F RID: 13343 RVA: 0x001CD598 File Offset: 0x001CB998
			internal FetchListResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x06003420 RID: 13344 RVA: 0x001CD5A1 File Offset: 0x001CB9A1
			protected override void CallDispose(HandleRef selfPointer)
			{
				PlayerManager.PlayerManager_FetchListResponse_Dispose(base.SelfPtr());
			}

			// Token: 0x06003421 RID: 13345 RVA: 0x001CD5AE File Offset: 0x001CB9AE
			internal CommonErrorStatus.ResponseStatus Status()
			{
				return PlayerManager.PlayerManager_FetchListResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x06003422 RID: 13346 RVA: 0x001CD5BB File Offset: 0x001CB9BB
			public IEnumerator<NativePlayer> GetEnumerator()
			{
				return PInvokeUtilities.ToEnumerator<NativePlayer>(this.Length(), (UIntPtr index) => this.GetElement(index));
			}

			// Token: 0x06003423 RID: 13347 RVA: 0x001CD5D4 File Offset: 0x001CB9D4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003424 RID: 13348 RVA: 0x001CD5DC File Offset: 0x001CB9DC
			internal UIntPtr Length()
			{
				return PlayerManager.PlayerManager_FetchListResponse_GetData_Length(base.SelfPtr());
			}

			// Token: 0x06003425 RID: 13349 RVA: 0x001CD5EC File Offset: 0x001CB9EC
			internal NativePlayer GetElement(UIntPtr index)
			{
				if (index.ToUInt64() >= this.Length().ToUInt64())
				{
					throw new ArgumentOutOfRangeException();
				}
				return new NativePlayer(PlayerManager.PlayerManager_FetchListResponse_GetData_GetElement(base.SelfPtr(), index));
			}

			// Token: 0x06003426 RID: 13350 RVA: 0x001CD62A File Offset: 0x001CBA2A
			internal static PlayerManager.FetchListResponse FromPointer(IntPtr selfPointer)
			{
				if (PInvokeUtilities.IsNull(selfPointer))
				{
					return null;
				}
				return new PlayerManager.FetchListResponse(selfPointer);
			}
		}

		// Token: 0x0200070C RID: 1804
		internal class FetchResponseCollector
		{
			// Token: 0x040032F6 RID: 13046
			internal int pendingCount;

			// Token: 0x040032F7 RID: 13047
			internal List<NativePlayer> results = new List<NativePlayer>();

			// Token: 0x040032F8 RID: 13048
			internal Action<NativePlayer[]> callback;
		}

		// Token: 0x0200070D RID: 1805
		internal class FetchResponse : BaseReferenceHolder
		{
			// Token: 0x06003429 RID: 13353 RVA: 0x001CD65B File Offset: 0x001CBA5B
			internal FetchResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x0600342A RID: 13354 RVA: 0x001CD664 File Offset: 0x001CBA64
			protected override void CallDispose(HandleRef selfPointer)
			{
				PlayerManager.PlayerManager_FetchResponse_Dispose(base.SelfPtr());
			}

			// Token: 0x0600342B RID: 13355 RVA: 0x001CD671 File Offset: 0x001CBA71
			internal NativePlayer GetPlayer()
			{
				return new NativePlayer(PlayerManager.PlayerManager_FetchResponse_GetData(base.SelfPtr()));
			}

			// Token: 0x0600342C RID: 13356 RVA: 0x001CD683 File Offset: 0x001CBA83
			internal CommonErrorStatus.ResponseStatus Status()
			{
				return PlayerManager.PlayerManager_FetchResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x0600342D RID: 13357 RVA: 0x001CD690 File Offset: 0x001CBA90
			internal static PlayerManager.FetchResponse FromPointer(IntPtr selfPointer)
			{
				if (PInvokeUtilities.IsNull(selfPointer))
				{
					return null;
				}
				return new PlayerManager.FetchResponse(selfPointer);
			}
		}

		// Token: 0x0200070E RID: 1806
		internal class FetchSelfResponse : BaseReferenceHolder
		{
			// Token: 0x0600342E RID: 13358 RVA: 0x001CD6A5 File Offset: 0x001CBAA5
			internal FetchSelfResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x0600342F RID: 13359 RVA: 0x001CD6AE File Offset: 0x001CBAAE
			internal CommonErrorStatus.ResponseStatus Status()
			{
				return PlayerManager.PlayerManager_FetchSelfResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x06003430 RID: 13360 RVA: 0x001CD6BB File Offset: 0x001CBABB
			internal NativePlayer Self()
			{
				return new NativePlayer(PlayerManager.PlayerManager_FetchSelfResponse_GetData(base.SelfPtr()));
			}

			// Token: 0x06003431 RID: 13361 RVA: 0x001CD6CD File Offset: 0x001CBACD
			protected override void CallDispose(HandleRef selfPointer)
			{
				PlayerManager.PlayerManager_FetchSelfResponse_Dispose(base.SelfPtr());
			}

			// Token: 0x06003432 RID: 13362 RVA: 0x001CD6DA File Offset: 0x001CBADA
			internal static PlayerManager.FetchSelfResponse FromPointer(IntPtr selfPointer)
			{
				if (PInvokeUtilities.IsNull(selfPointer))
				{
					return null;
				}
				return new PlayerManager.FetchSelfResponse(selfPointer);
			}
		}
	}
}
