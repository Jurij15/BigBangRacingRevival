using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Video;
using GooglePlayGames.OurUtils;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x02000603 RID: 1539
	public class PlayGamesPlatform : ISocialPlatform
	{
		// Token: 0x06002CD9 RID: 11481 RVA: 0x001BEC55 File Offset: 0x001BD055
		internal PlayGamesPlatform(IPlayGamesClient client)
		{
			this.mClient = Misc.CheckNotNull<IPlayGamesClient>(client);
			this.mLocalUser = new PlayGamesLocalUser(this);
			this.mConfiguration = PlayGamesClientConfiguration.DefaultConfiguration;
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x001BEC8B File Offset: 0x001BD08B
		private PlayGamesPlatform(PlayGamesClientConfiguration configuration)
		{
			Logger.w("Creating new PlayGamesPlatform");
			this.mLocalUser = new PlayGamesLocalUser(this);
			this.mConfiguration = configuration;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x001BECBB File Offset: 0x001BD0BB
		// (set) Token: 0x06002CDC RID: 11484 RVA: 0x001BECC2 File Offset: 0x001BD0C2
		public static bool DebugLogEnabled
		{
			get
			{
				return Logger.DebugLogEnabled;
			}
			set
			{
				Logger.DebugLogEnabled = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06002CDD RID: 11485 RVA: 0x001BECCA File Offset: 0x001BD0CA
		public static PlayGamesPlatform Instance
		{
			get
			{
				if (PlayGamesPlatform.sInstance == null)
				{
					Logger.d("Instance was not initialized, using default configuration.");
					PlayGamesPlatform.InitializeInstance(PlayGamesClientConfiguration.DefaultConfiguration);
				}
				return PlayGamesPlatform.sInstance;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x001BECF3 File Offset: 0x001BD0F3
		public static INearbyConnectionClient Nearby
		{
			get
			{
				if (PlayGamesPlatform.sNearbyConnectionClient == null && !PlayGamesPlatform.sNearbyInitializePending)
				{
					PlayGamesPlatform.sNearbyInitializePending = true;
					PlayGamesPlatform.InitializeNearby(null);
				}
				return PlayGamesPlatform.sNearbyConnectionClient;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x001BED22 File Offset: 0x001BD122
		public IRealTimeMultiplayerClient RealTime
		{
			get
			{
				return this.mClient.GetRtmpClient();
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x001BED2F File Offset: 0x001BD12F
		public ITurnBasedMultiplayerClient TurnBased
		{
			get
			{
				return this.mClient.GetTbmpClient();
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x001BED3C File Offset: 0x001BD13C
		public ISavedGameClient SavedGame
		{
			get
			{
				return this.mClient.GetSavedGameClient();
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x001BED49 File Offset: 0x001BD149
		public IEventsClient Events
		{
			get
			{
				return this.mClient.GetEventsClient();
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x001BED56 File Offset: 0x001BD156
		public IVideoClient Video
		{
			get
			{
				return this.mClient.GetVideoClient();
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x001BED63 File Offset: 0x001BD163
		public ILocalUser localUser
		{
			get
			{
				return this.mLocalUser;
			}
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x001BED6B File Offset: 0x001BD16B
		public static void InitializeInstance(PlayGamesClientConfiguration configuration)
		{
			if (PlayGamesPlatform.sInstance != null)
			{
				Logger.w("PlayGamesPlatform already initialized. Ignoring this call.");
				return;
			}
			PlayGamesPlatform.sInstance = new PlayGamesPlatform(configuration);
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x001BED94 File Offset: 0x001BD194
		public static void InitializeNearby(Action<INearbyConnectionClient> callback)
		{
			Debug.Log("Calling InitializeNearby!");
			if (PlayGamesPlatform.sNearbyConnectionClient == null)
			{
				NearbyConnectionClientFactory.Create(delegate(INearbyConnectionClient client)
				{
					Debug.Log("Nearby Client Created!!");
					PlayGamesPlatform.sNearbyConnectionClient = client;
					if (callback != null)
					{
						callback.Invoke(client);
					}
					else
					{
						Debug.Log("Initialize Nearby callback is null");
					}
				});
			}
			else if (callback != null)
			{
				Debug.Log("Nearby Already initialized: calling callback directly");
				callback.Invoke(PlayGamesPlatform.sNearbyConnectionClient);
			}
			else
			{
				Debug.Log("Nearby Already initialized");
			}
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x001BEE10 File Offset: 0x001BD210
		public static PlayGamesPlatform Activate()
		{
			Logger.d("Activating PlayGamesPlatform.");
			Social.Active = PlayGamesPlatform.Instance;
			Logger.d("PlayGamesPlatform activated: " + Social.Active);
			return PlayGamesPlatform.Instance;
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x001BEE3F File Offset: 0x001BD23F
		public IntPtr GetApiClient()
		{
			return this.mClient.GetApiClient();
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x001BEE4C File Offset: 0x001BD24C
		public void SetGravityForPopups(Gravity gravity)
		{
			this.mClient.SetGravityForPopups(gravity);
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x001BEE5A File Offset: 0x001BD25A
		public void AddIdMapping(string fromId, string toId)
		{
			this.mIdMap[fromId] = toId;
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x001BEE69 File Offset: 0x001BD269
		public void Authenticate(Action<bool> callback)
		{
			this.Authenticate(callback, false);
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x001BEE73 File Offset: 0x001BD273
		public void Authenticate(Action<bool, string> callback)
		{
			this.Authenticate(callback, false);
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x001BEE80 File Offset: 0x001BD280
		public void Authenticate(Action<bool> callback, bool silent)
		{
			this.Authenticate(delegate(bool success, string msg)
			{
				callback.Invoke(success);
			}, silent);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x001BEEAD File Offset: 0x001BD2AD
		public void Authenticate(Action<bool, string> callback, bool silent)
		{
			if (this.mClient == null)
			{
				Logger.d("Creating platform-specific Play Games client.");
				this.mClient = PlayGamesClientFactory.GetPlatformPlayGamesClient(this.mConfiguration);
			}
			this.mClient.Authenticate(callback, silent);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x001BEEE2 File Offset: 0x001BD2E2
		public void Authenticate(ILocalUser unused, Action<bool> callback)
		{
			this.Authenticate(callback, false);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x001BEEEC File Offset: 0x001BD2EC
		public void Authenticate(ILocalUser unused, Action<bool, string> callback)
		{
			this.Authenticate(callback, false);
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x001BEEF6 File Offset: 0x001BD2F6
		public bool IsAuthenticated()
		{
			return this.mClient != null && this.mClient.IsAuthenticated();
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x001BEF11 File Offset: 0x001BD311
		public void SignOut()
		{
			if (this.mClient != null)
			{
				this.mClient.SignOut();
			}
			this.mLocalUser = new PlayGamesLocalUser(this);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x001BEF35 File Offset: 0x001BD335
		public void LoadUsers(string[] userIds, Action<IUserProfile[]> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("GetUserId() can only be called after authentication.");
				callback.Invoke(new IUserProfile[0]);
				return;
			}
			this.mClient.LoadUsers(userIds, callback);
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x001BEF66 File Offset: 0x001BD366
		public string GetUserId()
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("GetUserId() can only be called after authentication.");
				return "0";
			}
			return this.mClient.GetUserId();
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x001BEF8E File Offset: 0x001BD38E
		public string GetIdToken()
		{
			if (this.mClient != null)
			{
				return this.mClient.GetIdToken();
			}
			Logger.e("No client available, returning null.");
			return null;
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x001BEFB2 File Offset: 0x001BD3B2
		public string GetServerAuthCode()
		{
			if (this.mClient != null && this.mClient.IsAuthenticated())
			{
				return this.mClient.GetServerAuthCode();
			}
			return null;
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x001BEFDC File Offset: 0x001BD3DC
		public void GetAnotherServerAuthCode(bool reAuthenticateIfNeeded, Action<string> callback)
		{
			if (this.mClient != null && this.mClient.IsAuthenticated())
			{
				this.mClient.GetAnotherServerAuthCode(reAuthenticateIfNeeded, callback);
			}
			else if (this.mClient != null && reAuthenticateIfNeeded)
			{
				this.mClient.Authenticate(delegate(bool success, string msg)
				{
					if (success)
					{
						callback.Invoke(this.mClient.GetServerAuthCode());
					}
					else
					{
						Logger.e("Re-authentication failed: " + msg);
						callback.Invoke(null);
					}
				}, false);
			}
			else
			{
				Logger.e("Cannot call GetAnotherServerAuthCode: not authenticated");
				callback.Invoke(null);
			}
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x001BF073 File Offset: 0x001BD473
		public string GetUserEmail()
		{
			return this.mClient.GetUserEmail();
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x001BF080 File Offset: 0x001BD480
		public void GetPlayerStats(Action<CommonStatusCodes, GooglePlayGames.BasicApi.PlayerStats> callback)
		{
			if (this.mClient != null && this.mClient.IsAuthenticated())
			{
				this.mClient.GetPlayerStats(callback);
			}
			else
			{
				Logger.e("GetPlayerStats can only be called after authentication.");
				callback.Invoke(CommonStatusCodes.SignInRequired, new GooglePlayGames.BasicApi.PlayerStats());
			}
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x001BF0CF File Offset: 0x001BD4CF
		public GooglePlayGames.BasicApi.Achievement GetAchievement(string achievementId)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("GetAchievement can only be called after authentication.");
				return null;
			}
			return this.mClient.GetAchievement(achievementId);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x001BF0F4 File Offset: 0x001BD4F4
		public string GetUserDisplayName()
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("GetUserDisplayName can only be called after authentication.");
				return string.Empty;
			}
			return this.mClient.GetUserDisplayName();
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x001BF11C File Offset: 0x001BD51C
		public string GetUserImageUrl()
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("GetUserImageUrl can only be called after authentication.");
				return null;
			}
			return this.mClient.GetUserImageUrl();
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x001BF140 File Offset: 0x001BD540
		public void ReportProgress(string achievementID, double progress, Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("ReportProgress can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(false);
				}
				return;
			}
			Logger.d(string.Concat(new object[] { "ReportProgress, ", achievementID, ", ", progress }));
			achievementID = this.MapId(achievementID);
			if (progress < 1E-06)
			{
				Logger.d("Progress 0.00 interpreted as request to reveal.");
				this.mClient.RevealAchievement(achievementID, callback);
				return;
			}
			int num = 0;
			int num2 = 0;
			GooglePlayGames.BasicApi.Achievement achievement = this.mClient.GetAchievement(achievementID);
			bool flag;
			if (achievement == null)
			{
				Logger.w("Unable to locate achievement " + achievementID);
				Logger.w("As a quick fix, assuming it's standard.");
				flag = false;
			}
			else
			{
				flag = achievement.IsIncremental;
				num = achievement.CurrentSteps;
				num2 = achievement.TotalSteps;
				Logger.d("Achievement is " + ((!flag) ? "STANDARD" : "INCREMENTAL"));
				if (flag)
				{
					Logger.d(string.Concat(new object[] { "Current steps: ", num, "/", num2 }));
				}
			}
			if (flag)
			{
				Logger.d("Progress " + progress + " interpreted as incremental target (approximate).");
				if (progress >= 0.0 && progress <= 1.0)
				{
					Logger.w("Progress " + progress + " is less than or equal to 1. You might be trying to use values in the range of [0,1], while values are expected to be within the range [0,100]. If you are using the latter, you can safely ignore this message.");
				}
				int num3 = (int)Math.Round(progress / 100.0 * (double)num2);
				int num4 = num3 - num;
				Logger.d(string.Concat(new object[] { "Target steps: ", num3, ", cur steps:", num }));
				Logger.d("Steps to increment: " + num4);
				if (num4 >= 0)
				{
					this.mClient.IncrementAchievement(achievementID, num4, callback);
				}
			}
			else if (progress >= 100.0)
			{
				Logger.d("Progress " + progress + " interpreted as UNLOCK.");
				this.mClient.UnlockAchievement(achievementID, callback);
			}
			else
			{
				Logger.d("Progress " + progress + " not enough to unlock non-incremental achievement.");
			}
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x001BF3A8 File Offset: 0x001BD7A8
		public void RevealAchievement(string achievementID, Action<bool> callback = null)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("RevealAchievement can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(false);
				}
				return;
			}
			Logger.d("RevealAchievement: " + achievementID);
			achievementID = this.MapId(achievementID);
			this.mClient.RevealAchievement(achievementID, callback);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x001BF400 File Offset: 0x001BD800
		public void UnlockAchievement(string achievementID, Action<bool> callback = null)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("UnlockAchievement can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(false);
				}
				return;
			}
			Logger.d("UnlockAchievement: " + achievementID);
			achievementID = this.MapId(achievementID);
			this.mClient.UnlockAchievement(achievementID, callback);
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x001BF458 File Offset: 0x001BD858
		public void IncrementAchievement(string achievementID, int steps, Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("IncrementAchievement can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(false);
				}
				return;
			}
			Logger.d(string.Concat(new object[] { "IncrementAchievement: ", achievementID, ", steps ", steps }));
			achievementID = this.MapId(achievementID);
			this.mClient.IncrementAchievement(achievementID, steps, callback);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x001BF4CC File Offset: 0x001BD8CC
		public void SetStepsAtLeast(string achievementID, int steps, Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("SetStepsAtLeast can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(false);
				}
				return;
			}
			Logger.d(string.Concat(new object[] { "SetStepsAtLeast: ", achievementID, ", steps ", steps }));
			achievementID = this.MapId(achievementID);
			this.mClient.SetStepsAtLeast(achievementID, steps, callback);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x001BF540 File Offset: 0x001BD940
		public void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("LoadAchievementDescriptions can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(null);
				}
				return;
			}
			this.mClient.LoadAchievements(delegate(GooglePlayGames.BasicApi.Achievement[] ach)
			{
				IAchievementDescription[] array = new IAchievementDescription[ach.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new PlayGamesAchievement(ach[i]);
				}
				callback.Invoke(array);
			});
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x001BF5A0 File Offset: 0x001BD9A0
		public void LoadAchievements(Action<IAchievement[]> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("LoadAchievements can only be called after authentication.");
				callback.Invoke(null);
				return;
			}
			this.mClient.LoadAchievements(delegate(GooglePlayGames.BasicApi.Achievement[] ach)
			{
				IAchievement[] array = new IAchievement[ach.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new PlayGamesAchievement(ach[i]);
				}
				callback.Invoke(array);
			});
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x001BF5F3 File Offset: 0x001BD9F3
		public IAchievement CreateAchievement()
		{
			return new PlayGamesAchievement();
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x001BF5FC File Offset: 0x001BD9FC
		public void ReportScore(long score, string board, Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("ReportScore can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(false);
				}
				return;
			}
			Logger.d(string.Concat(new object[] { "ReportScore: score=", score, ", board=", board }));
			string text = this.MapId(board);
			this.mClient.SubmitScore(text, score, callback);
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x001BF670 File Offset: 0x001BDA70
		public void ReportScore(long score, string board, string metadata, Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("ReportScore can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(false);
				}
				return;
			}
			Logger.d(string.Concat(new object[] { "ReportScore: score=", score, ", board=", board, " metadata=", metadata }));
			string text = this.MapId(board);
			this.mClient.SubmitScore(text, score, metadata, callback);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x001BF6F4 File Offset: 0x001BDAF4
		public void LoadScores(string leaderboardId, Action<IScore[]> callback)
		{
			this.LoadScores(leaderboardId, LeaderboardStart.PlayerCentered, this.mClient.LeaderboardMaxResults(), LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime, delegate(LeaderboardScoreData scoreData)
			{
				callback.Invoke(scoreData.Scores);
			});
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x001BF72F File Offset: 0x001BDB2F
		public void LoadScores(string leaderboardId, LeaderboardStart start, int rowCount, LeaderboardCollection collection, LeaderboardTimeSpan timeSpan, Action<LeaderboardScoreData> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("LoadScores can only be called after authentication.");
				callback.Invoke(new LeaderboardScoreData(leaderboardId, ResponseStatus.NotAuthorized));
				return;
			}
			this.mClient.LoadScores(leaderboardId, start, rowCount, collection, timeSpan, callback);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x001BF76A File Offset: 0x001BDB6A
		public void LoadMoreScores(ScorePageToken token, int rowCount, Action<LeaderboardScoreData> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("LoadMoreScores can only be called after authentication.");
				callback.Invoke(new LeaderboardScoreData(token.LeaderboardId, ResponseStatus.NotAuthorized));
				return;
			}
			this.mClient.LoadMoreScores(token, rowCount, callback);
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x001BF7A3 File Offset: 0x001BDBA3
		public ILeaderboard CreateLeaderboard()
		{
			return new PlayGamesLeaderboard(this.mDefaultLbUi);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x001BF7B0 File Offset: 0x001BDBB0
		public void ShowAchievementsUI()
		{
			this.ShowAchievementsUI(null);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x001BF7B9 File Offset: 0x001BDBB9
		public void ShowAchievementsUI(Action<UIStatus> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("ShowAchievementsUI can only be called after authentication.");
				return;
			}
			Logger.d("ShowAchievementsUI callback is " + callback);
			this.mClient.ShowAchievementsUI(callback);
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x001BF7ED File Offset: 0x001BDBED
		public void ShowLeaderboardUI()
		{
			Logger.d("ShowLeaderboardUI with default ID");
			this.ShowLeaderboardUI(this.MapId(this.mDefaultLbUi), null);
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x001BF80C File Offset: 0x001BDC0C
		public void ShowLeaderboardUI(string leaderboardId)
		{
			if (leaderboardId != null)
			{
				leaderboardId = this.MapId(leaderboardId);
			}
			this.mClient.ShowLeaderboardUI(leaderboardId, LeaderboardTimeSpan.AllTime, null);
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x001BF82B File Offset: 0x001BDC2B
		public void ShowLeaderboardUI(string leaderboardId, Action<UIStatus> callback)
		{
			this.ShowLeaderboardUI(leaderboardId, LeaderboardTimeSpan.AllTime, callback);
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x001BF838 File Offset: 0x001BDC38
		public void ShowLeaderboardUI(string leaderboardId, LeaderboardTimeSpan span, Action<UIStatus> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("ShowLeaderboardUI can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(UIStatus.NotAuthorized);
				}
				return;
			}
			Logger.d(string.Concat(new object[] { "ShowLeaderboardUI, lbId=", leaderboardId, " callback is ", callback }));
			this.mClient.ShowLeaderboardUI(leaderboardId, span, callback);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x001BF89F File Offset: 0x001BDC9F
		public void SetDefaultLeaderboardForUI(string lbid)
		{
			Logger.d("SetDefaultLeaderboardForUI: " + lbid);
			if (lbid != null)
			{
				lbid = this.MapId(lbid);
			}
			this.mDefaultLbUi = lbid;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x001BF8C7 File Offset: 0x001BDCC7
		public void LoadFriends(ILocalUser user, Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("LoadScores can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(false);
				}
				return;
			}
			this.mClient.LoadFriends(callback);
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x001BF8F8 File Offset: 0x001BDCF8
		public void LoadScores(ILeaderboard board, Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("LoadScores can only be called after authentication.");
				if (callback != null)
				{
					callback.Invoke(false);
				}
				return;
			}
			LeaderboardTimeSpan leaderboardTimeSpan;
			switch (board.timeScope)
			{
			case 0:
				leaderboardTimeSpan = LeaderboardTimeSpan.Daily;
				break;
			case 1:
				leaderboardTimeSpan = LeaderboardTimeSpan.Weekly;
				break;
			case 2:
				leaderboardTimeSpan = LeaderboardTimeSpan.AllTime;
				break;
			default:
				leaderboardTimeSpan = LeaderboardTimeSpan.AllTime;
				break;
			}
			((PlayGamesLeaderboard)board).loading = true;
			Logger.d(string.Concat(new object[] { "LoadScores, board=", board, " callback is ", callback }));
			this.mClient.LoadScores(board.id, LeaderboardStart.PlayerCentered, (board.range.count <= 0) ? this.mClient.LeaderboardMaxResults() : board.range.count, (board.userScope != 1) ? LeaderboardCollection.Public : LeaderboardCollection.Social, leaderboardTimeSpan, delegate(LeaderboardScoreData scoreData)
			{
				this.HandleLoadingScores((PlayGamesLeaderboard)board, scoreData, callback);
			});
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x001BFA48 File Offset: 0x001BDE48
		public bool GetLoading(ILeaderboard board)
		{
			return board != null && board.loading;
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x001BFA59 File Offset: 0x001BDE59
		public void RegisterInvitationDelegate(InvitationReceivedDelegate deleg)
		{
			this.mClient.RegisterInvitationDelegate(deleg);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x001BFA68 File Offset: 0x001BDE68
		internal void HandleLoadingScores(PlayGamesLeaderboard board, LeaderboardScoreData scoreData, Action<bool> callback)
		{
			bool flag = board.SetFromData(scoreData);
			if (flag && !board.HasAllScores() && scoreData.NextPageToken != null)
			{
				int num = board.range.count - board.ScoreCount;
				this.mClient.LoadMoreScores(scoreData.NextPageToken, num, delegate(LeaderboardScoreData nextScoreData)
				{
					this.HandleLoadingScores(board, nextScoreData, callback);
				});
			}
			else
			{
				callback.Invoke(flag);
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x001BFB0D File Offset: 0x001BDF0D
		internal IUserProfile[] GetFriends()
		{
			if (!this.IsAuthenticated())
			{
				Logger.d("Cannot get friends when not authenticated!");
				return new IUserProfile[0];
			}
			return this.mClient.GetFriends();
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x001BFB38 File Offset: 0x001BDF38
		private string MapId(string id)
		{
			if (id == null)
			{
				return null;
			}
			if (this.mIdMap.ContainsKey(id))
			{
				string text = this.mIdMap[id];
				Logger.d("Mapping alias " + id + " to ID " + text);
				return text;
			}
			return id;
		}

		// Token: 0x0400313F RID: 12607
		private static volatile PlayGamesPlatform sInstance;

		// Token: 0x04003140 RID: 12608
		private static volatile bool sNearbyInitializePending;

		// Token: 0x04003141 RID: 12609
		private static volatile INearbyConnectionClient sNearbyConnectionClient;

		// Token: 0x04003142 RID: 12610
		private readonly PlayGamesClientConfiguration mConfiguration;

		// Token: 0x04003143 RID: 12611
		private PlayGamesLocalUser mLocalUser;

		// Token: 0x04003144 RID: 12612
		private IPlayGamesClient mClient;

		// Token: 0x04003145 RID: 12613
		private string mDefaultLbUi;

		// Token: 0x04003146 RID: 12614
		private Dictionary<string, string> mIdMap = new Dictionary<string, string>();
	}
}
