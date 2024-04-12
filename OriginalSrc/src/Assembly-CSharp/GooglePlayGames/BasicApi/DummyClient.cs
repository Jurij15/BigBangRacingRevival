using System;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Video;
using GooglePlayGames.OurUtils;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020005CD RID: 1485
	public class DummyClient : IPlayGamesClient
	{
		// Token: 0x06002B11 RID: 11025 RVA: 0x001BCE4C File Offset: 0x001BB24C
		public void Authenticate(Action<bool, string> callback, bool silent)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(false, "Not implemented on this platform");
			}
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x001BCE65 File Offset: 0x001BB265
		public bool IsAuthenticated()
		{
			DummyClient.LogUsage();
			return false;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x001BCE6D File Offset: 0x001BB26D
		public void SignOut()
		{
			DummyClient.LogUsage();
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x001BCE74 File Offset: 0x001BB274
		public string GetIdToken()
		{
			DummyClient.LogUsage();
			return null;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x001BCE7C File Offset: 0x001BB27C
		public string GetUserId()
		{
			DummyClient.LogUsage();
			return "DummyID";
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x001BCE88 File Offset: 0x001BB288
		public string GetServerAuthCode()
		{
			DummyClient.LogUsage();
			return null;
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x001BCE90 File Offset: 0x001BB290
		public void GetAnotherServerAuthCode(bool reAuthenticateIfNeeded, Action<string> callback)
		{
			DummyClient.LogUsage();
			callback.Invoke(null);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x001BCE9E File Offset: 0x001BB29E
		public string GetUserEmail()
		{
			return string.Empty;
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x001BCEA5 File Offset: 0x001BB2A5
		public void GetPlayerStats(Action<CommonStatusCodes, PlayerStats> callback)
		{
			DummyClient.LogUsage();
			callback.Invoke(CommonStatusCodes.ApiNotConnected, new PlayerStats());
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x001BCEB9 File Offset: 0x001BB2B9
		public string GetUserDisplayName()
		{
			DummyClient.LogUsage();
			return "Player";
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x001BCEC5 File Offset: 0x001BB2C5
		public string GetUserImageUrl()
		{
			DummyClient.LogUsage();
			return null;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x001BCECD File Offset: 0x001BB2CD
		public void LoadUsers(string[] userIds, Action<IUserProfile[]> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(null);
			}
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x001BCEE1 File Offset: 0x001BB2E1
		public void LoadAchievements(Action<Achievement[]> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(null);
			}
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x001BCEF5 File Offset: 0x001BB2F5
		public Achievement GetAchievement(string achId)
		{
			DummyClient.LogUsage();
			return null;
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x001BCEFD File Offset: 0x001BB2FD
		public void UnlockAchievement(string achId, Action<bool> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(false);
			}
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x001BCF11 File Offset: 0x001BB311
		public void RevealAchievement(string achId, Action<bool> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(false);
			}
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x001BCF25 File Offset: 0x001BB325
		public void IncrementAchievement(string achId, int steps, Action<bool> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(false);
			}
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x001BCF39 File Offset: 0x001BB339
		public void SetStepsAtLeast(string achId, int steps, Action<bool> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(false);
			}
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x001BCF4D File Offset: 0x001BB34D
		public void ShowAchievementsUI(Action<UIStatus> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(UIStatus.VersionUpdateRequired);
			}
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x001BCF62 File Offset: 0x001BB362
		public void ShowLeaderboardUI(string leaderboardId, LeaderboardTimeSpan span, Action<UIStatus> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(UIStatus.VersionUpdateRequired);
			}
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x001BCF77 File Offset: 0x001BB377
		public int LeaderboardMaxResults()
		{
			return 25;
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x001BCF7B File Offset: 0x001BB37B
		public void LoadScores(string leaderboardId, LeaderboardStart start, int rowCount, LeaderboardCollection collection, LeaderboardTimeSpan timeSpan, Action<LeaderboardScoreData> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(new LeaderboardScoreData(leaderboardId, ResponseStatus.LicenseCheckFailed));
			}
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x001BCF97 File Offset: 0x001BB397
		public void LoadMoreScores(ScorePageToken token, int rowCount, Action<LeaderboardScoreData> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(new LeaderboardScoreData(token.LeaderboardId, ResponseStatus.LicenseCheckFailed));
			}
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x001BCFB6 File Offset: 0x001BB3B6
		public void SubmitScore(string leaderboardId, long score, Action<bool> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(false);
			}
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x001BCFCA File Offset: 0x001BB3CA
		public void SubmitScore(string leaderboardId, long score, string metadata, Action<bool> callback)
		{
			DummyClient.LogUsage();
			if (callback != null)
			{
				callback.Invoke(false);
			}
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x001BCFE0 File Offset: 0x001BB3E0
		public IRealTimeMultiplayerClient GetRtmpClient()
		{
			DummyClient.LogUsage();
			return null;
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x001BCFE8 File Offset: 0x001BB3E8
		public ITurnBasedMultiplayerClient GetTbmpClient()
		{
			DummyClient.LogUsage();
			return null;
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x001BCFF0 File Offset: 0x001BB3F0
		public ISavedGameClient GetSavedGameClient()
		{
			DummyClient.LogUsage();
			return null;
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x001BCFF8 File Offset: 0x001BB3F8
		public IEventsClient GetEventsClient()
		{
			DummyClient.LogUsage();
			return null;
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x001BD000 File Offset: 0x001BB400
		public IVideoClient GetVideoClient()
		{
			DummyClient.LogUsage();
			return null;
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x001BD008 File Offset: 0x001BB408
		public void RegisterInvitationDelegate(InvitationReceivedDelegate invitationDelegate)
		{
			DummyClient.LogUsage();
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x001BD00F File Offset: 0x001BB40F
		public Invitation GetInvitationFromNotification()
		{
			DummyClient.LogUsage();
			return null;
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x001BD017 File Offset: 0x001BB417
		public bool HasInvitationFromNotification()
		{
			DummyClient.LogUsage();
			return false;
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x001BD01F File Offset: 0x001BB41F
		public void LoadFriends(Action<bool> callback)
		{
			DummyClient.LogUsage();
			callback.Invoke(false);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x001BD02D File Offset: 0x001BB42D
		public IUserProfile[] GetFriends()
		{
			DummyClient.LogUsage();
			return new IUserProfile[0];
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x001BD03A File Offset: 0x001BB43A
		public IntPtr GetApiClient()
		{
			DummyClient.LogUsage();
			return IntPtr.Zero;
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x001BD046 File Offset: 0x001BB446
		public void SetGravityForPopups(Gravity gravity)
		{
			DummyClient.LogUsage();
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x001BD04D File Offset: 0x001BB44D
		private static void LogUsage()
		{
			Logger.d("Received method call on DummyClient - using stub implementation.");
		}
	}
}
