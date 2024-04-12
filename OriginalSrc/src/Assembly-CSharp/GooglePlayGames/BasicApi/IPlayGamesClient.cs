using System;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Video;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020005D1 RID: 1489
	public interface IPlayGamesClient
	{
		// Token: 0x06002B40 RID: 11072
		void Authenticate(Action<bool, string> callback, bool silent);

		// Token: 0x06002B41 RID: 11073
		bool IsAuthenticated();

		// Token: 0x06002B42 RID: 11074
		void SignOut();

		// Token: 0x06002B43 RID: 11075
		string GetUserId();

		// Token: 0x06002B44 RID: 11076
		void LoadFriends(Action<bool> callback);

		// Token: 0x06002B45 RID: 11077
		string GetUserDisplayName();

		// Token: 0x06002B46 RID: 11078
		string GetIdToken();

		// Token: 0x06002B47 RID: 11079
		string GetServerAuthCode();

		// Token: 0x06002B48 RID: 11080
		void GetAnotherServerAuthCode(bool reAuthenticateIfNeeded, Action<string> callback);

		// Token: 0x06002B49 RID: 11081
		string GetUserEmail();

		// Token: 0x06002B4A RID: 11082
		string GetUserImageUrl();

		// Token: 0x06002B4B RID: 11083
		void GetPlayerStats(Action<CommonStatusCodes, PlayerStats> callback);

		// Token: 0x06002B4C RID: 11084
		void LoadUsers(string[] userIds, Action<IUserProfile[]> callback);

		// Token: 0x06002B4D RID: 11085
		Achievement GetAchievement(string achievementId);

		// Token: 0x06002B4E RID: 11086
		void LoadAchievements(Action<Achievement[]> callback);

		// Token: 0x06002B4F RID: 11087
		void UnlockAchievement(string achievementId, Action<bool> successOrFailureCalllback);

		// Token: 0x06002B50 RID: 11088
		void RevealAchievement(string achievementId, Action<bool> successOrFailureCalllback);

		// Token: 0x06002B51 RID: 11089
		void IncrementAchievement(string achievementId, int steps, Action<bool> successOrFailureCalllback);

		// Token: 0x06002B52 RID: 11090
		void SetStepsAtLeast(string achId, int steps, Action<bool> callback);

		// Token: 0x06002B53 RID: 11091
		void ShowAchievementsUI(Action<UIStatus> callback);

		// Token: 0x06002B54 RID: 11092
		void ShowLeaderboardUI(string leaderboardId, LeaderboardTimeSpan span, Action<UIStatus> callback);

		// Token: 0x06002B55 RID: 11093
		void LoadScores(string leaderboardId, LeaderboardStart start, int rowCount, LeaderboardCollection collection, LeaderboardTimeSpan timeSpan, Action<LeaderboardScoreData> callback);

		// Token: 0x06002B56 RID: 11094
		void LoadMoreScores(ScorePageToken token, int rowCount, Action<LeaderboardScoreData> callback);

		// Token: 0x06002B57 RID: 11095
		int LeaderboardMaxResults();

		// Token: 0x06002B58 RID: 11096
		void SubmitScore(string leaderboardId, long score, Action<bool> successOrFailureCalllback);

		// Token: 0x06002B59 RID: 11097
		void SubmitScore(string leaderboardId, long score, string metadata, Action<bool> successOrFailureCalllback);

		// Token: 0x06002B5A RID: 11098
		IRealTimeMultiplayerClient GetRtmpClient();

		// Token: 0x06002B5B RID: 11099
		ITurnBasedMultiplayerClient GetTbmpClient();

		// Token: 0x06002B5C RID: 11100
		ISavedGameClient GetSavedGameClient();

		// Token: 0x06002B5D RID: 11101
		IEventsClient GetEventsClient();

		// Token: 0x06002B5E RID: 11102
		IVideoClient GetVideoClient();

		// Token: 0x06002B5F RID: 11103
		void RegisterInvitationDelegate(InvitationReceivedDelegate invitationDelegate);

		// Token: 0x06002B60 RID: 11104
		IUserProfile[] GetFriends();

		// Token: 0x06002B61 RID: 11105
		IntPtr GetApiClient();

		// Token: 0x06002B62 RID: 11106
		void SetGravityForPopups(Gravity gravity);
	}
}
