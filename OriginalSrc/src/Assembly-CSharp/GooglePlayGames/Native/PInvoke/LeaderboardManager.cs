using System;
using AOT;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006E4 RID: 1764
	internal class LeaderboardManager
	{
		// Token: 0x060032BE RID: 12990 RVA: 0x001CAA33 File Offset: 0x001C8E33
		internal LeaderboardManager(GameServices services)
		{
			this.mServices = Misc.CheckNotNull<GameServices>(services);
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x001CAA47 File Offset: 0x001C8E47
		internal int LeaderboardMaxResults
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x001CAA4C File Offset: 0x001C8E4C
		internal void SubmitScore(string leaderboardId, long score, string metadata)
		{
			Misc.CheckNotNull<string>(leaderboardId, "leaderboardId");
			Logger.d(string.Concat(new object[] { "Native Submitting score: ", score, " for lb ", leaderboardId, " with metadata: ", metadata }));
			LeaderboardManager.LeaderboardManager_SubmitScore(this.mServices.AsHandle(), leaderboardId, (ulong)score, metadata ?? string.Empty);
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x001CAABD File Offset: 0x001C8EBD
		internal void ShowAllUI(Action<CommonErrorStatus.UIStatus> callback)
		{
			Misc.CheckNotNull<Action<CommonErrorStatus.UIStatus>>(callback);
			LeaderboardManager.LeaderboardManager_ShowAllUI(this.mServices.AsHandle(), new LeaderboardManager.ShowAllUICallback(Callbacks.InternalShowUICallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x001CAAF9 File Offset: 0x001C8EF9
		internal void ShowUI(string leaderboardId, LeaderboardTimeSpan span, Action<CommonErrorStatus.UIStatus> callback)
		{
			Misc.CheckNotNull<Action<CommonErrorStatus.UIStatus>>(callback);
			LeaderboardManager.LeaderboardManager_ShowUI(this.mServices.AsHandle(), leaderboardId, (Types.LeaderboardTimeSpan)span, new LeaderboardManager.ShowUICallback(Callbacks.InternalShowUICallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x001CAB38 File Offset: 0x001C8F38
		public void LoadLeaderboardData(string leaderboardId, LeaderboardStart start, int rowCount, LeaderboardCollection collection, LeaderboardTimeSpan timeSpan, string playerId, Action<LeaderboardScoreData> callback)
		{
			NativeScorePageToken nativeScorePageToken = new NativeScorePageToken(LeaderboardManager.LeaderboardManager_ScorePageToken(this.mServices.AsHandle(), leaderboardId, (Types.LeaderboardStart)start, (Types.LeaderboardTimeSpan)timeSpan, (Types.LeaderboardCollection)collection));
			ScorePageToken token = new ScorePageToken(nativeScorePageToken, leaderboardId, collection, timeSpan);
			LeaderboardManager.LeaderboardManager_Fetch(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, leaderboardId, new LeaderboardManager.FetchCallback(LeaderboardManager.InternalFetchCallback), Callbacks.ToIntPtr<FetchResponse>(delegate(FetchResponse rsp)
			{
				this.HandleFetch(token, rsp, playerId, rowCount, callback);
			}, new Func<IntPtr, FetchResponse>(FetchResponse.FromPointer)));
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x001CABF3 File Offset: 0x001C8FF3
		[MonoPInvokeCallback(typeof(LeaderboardManager.FetchCallback))]
		private static void InternalFetchCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("LeaderboardManager#InternalFetchCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x001CAC04 File Offset: 0x001C9004
		internal void HandleFetch(ScorePageToken token, FetchResponse response, string selfPlayerId, int maxResults, Action<LeaderboardScoreData> callback)
		{
			LeaderboardScoreData data = new LeaderboardScoreData(token.LeaderboardId, (ResponseStatus)response.GetStatus());
			if (response.GetStatus() != CommonErrorStatus.ResponseStatus.VALID && response.GetStatus() != CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
			{
				Logger.w("Error returned from fetch: " + response.GetStatus());
				callback.Invoke(data);
				return;
			}
			data.Title = response.Leaderboard().Title();
			data.Id = token.LeaderboardId;
			LeaderboardManager.LeaderboardManager_FetchScoreSummary(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, token.LeaderboardId, (Types.LeaderboardTimeSpan)token.TimeSpan, (Types.LeaderboardCollection)token.Collection, new LeaderboardManager.FetchScoreSummaryCallback(LeaderboardManager.InternalFetchSummaryCallback), Callbacks.ToIntPtr<FetchScoreSummaryResponse>(delegate(FetchScoreSummaryResponse rsp)
			{
				this.HandleFetchScoreSummary(data, rsp, selfPlayerId, maxResults, token, callback);
			}, new Func<IntPtr, FetchScoreSummaryResponse>(FetchScoreSummaryResponse.FromPointer)));
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x001CAD45 File Offset: 0x001C9145
		[MonoPInvokeCallback(typeof(LeaderboardManager.FetchScoreSummaryCallback))]
		private static void InternalFetchSummaryCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("LeaderboardManager#InternalFetchSummaryCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x001CAD54 File Offset: 0x001C9154
		internal void HandleFetchScoreSummary(LeaderboardScoreData data, FetchScoreSummaryResponse response, string selfPlayerId, int maxResults, ScorePageToken token, Action<LeaderboardScoreData> callback)
		{
			if (response.GetStatus() != CommonErrorStatus.ResponseStatus.VALID && response.GetStatus() != CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
			{
				Logger.w("Error returned from fetchScoreSummary: " + response);
				data.Status = (ResponseStatus)response.GetStatus();
				callback.Invoke(data);
				return;
			}
			NativeScoreSummary scoreSummary = response.GetScoreSummary();
			data.ApproximateCount = scoreSummary.ApproximateResults();
			data.PlayerScore = scoreSummary.LocalUserScore().AsScore(data.Id, selfPlayerId);
			if (maxResults <= 0)
			{
				callback.Invoke(data);
				return;
			}
			this.LoadScorePage(data, maxResults, token, callback);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x001CADE8 File Offset: 0x001C91E8
		public void LoadScorePage(LeaderboardScoreData data, int maxResults, ScorePageToken token, Action<LeaderboardScoreData> callback)
		{
			if (data == null)
			{
				data = new LeaderboardScoreData(token.LeaderboardId);
			}
			NativeScorePageToken nativeScorePageToken = (NativeScorePageToken)token.InternalObject;
			LeaderboardManager.LeaderboardManager_FetchScorePage(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, nativeScorePageToken.AsPointer(), (uint)maxResults, new LeaderboardManager.FetchScorePageCallback(LeaderboardManager.InternalFetchScorePage), Callbacks.ToIntPtr<FetchScorePageResponse>(delegate(FetchScorePageResponse rsp)
			{
				this.HandleFetchScorePage(data, token, rsp, callback);
			}, new Func<IntPtr, FetchScorePageResponse>(FetchScorePageResponse.FromPointer)));
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x001CAEAD File Offset: 0x001C92AD
		[MonoPInvokeCallback(typeof(LeaderboardManager.FetchScorePageCallback))]
		private static void InternalFetchScorePage(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("LeaderboardManager#InternalFetchScorePage", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x001CAEBC File Offset: 0x001C92BC
		internal void HandleFetchScorePage(LeaderboardScoreData data, ScorePageToken token, FetchScorePageResponse rsp, Action<LeaderboardScoreData> callback)
		{
			data.Status = (ResponseStatus)rsp.GetStatus();
			if (rsp.GetStatus() != CommonErrorStatus.ResponseStatus.VALID && rsp.GetStatus() != CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
			{
				callback.Invoke(data);
			}
			NativeScorePage scorePage = rsp.GetScorePage();
			if (!scorePage.Valid())
			{
				callback.Invoke(data);
			}
			if (scorePage.HasNextScorePage())
			{
				data.NextPageToken = new ScorePageToken(scorePage.GetNextScorePageToken(), token.LeaderboardId, token.Collection, token.TimeSpan);
			}
			if (scorePage.HasPrevScorePage())
			{
				data.PrevPageToken = new ScorePageToken(scorePage.GetPreviousScorePageToken(), token.LeaderboardId, token.Collection, token.TimeSpan);
			}
			foreach (NativeScoreEntry nativeScoreEntry in scorePage)
			{
				data.AddScore(nativeScoreEntry.AsScore(data.Id));
			}
			callback.Invoke(data);
		}

		// Token: 0x040032D3 RID: 13011
		private readonly GameServices mServices;
	}
}
