using System;
using System.Collections;
using System.Collections.Generic;
using Server;

// Token: 0x02000148 RID: 328
public static class PsServerRequest
{
	// Token: 0x06000AE8 RID: 2792 RVA: 0x0006EA2B File Offset: 0x0006CE2B
	[Obsolete("Functionality has been removed.")]
	public static HttpC GiveLevelTemplate(string _templateId, Hashtable _ht, Action<PsMinigameMetaData> _okCallback, Action<HttpC> _failCallback, Action _errorCallbacl = null)
	{
		return null;
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x0006EA30 File Offset: 0x0006CE30
	public static HttpC ServerClaimMinigame(PsMinigameMetaData _data, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = MiniGame.ClaimReward(_data.id, _okCallback, _failCallback, null);
		httpC.objectData = _data;
		EntityManager.AddComponentToEntity(PsMetagameManager.m_utilityEntity, httpC);
		return httpC;
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x0006EA60 File Offset: 0x0006CE60
	public static HttpC ServerSendRating(PsMetagameManager.SaveRatingData _data, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Rating.Save(_data, _okCallback, _failCallback, null);
		httpC.objectData = _data;
		EntityManager.AddComponentToEntity(PsMetagameManager.m_utilityEntity, httpC);
		return httpC;
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x0006EA8C File Offset: 0x0006CE8C
	public static HttpC ServerPlayerSetData(Hashtable _setData, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		bool flag = false;
		if (PsMetagameManager.m_suspiciousActivity)
		{
			flag = true;
			PsMetagameManager.m_suspiciousActivity = false;
		}
		HttpC httpC = Player.SetData(_setData, null, null, null, null, null, _okCallback, _failCallback, _errorCallback, flag);
		httpC.objectData = _setData;
		return httpC;
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x0006EAC8 File Offset: 0x0006CEC8
	public static HttpC ServerPlayerSetDataAndProgression(Hashtable _setData, Hashtable _pathJson, Hashtable _customisations, List<Hashtable> _chests, Dictionary<string, int> _editorResources, List<Dictionary<string, object>> _achievements, bool _sendMetrics, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		Player.ProgressionAndSetDataObject progressionAndSetDataObject = new Player.ProgressionAndSetDataObject();
		progressionAndSetDataObject.pathJson = _pathJson;
		progressionAndSetDataObject.setData = _setData;
		progressionAndSetDataObject.customisations = _customisations;
		progressionAndSetDataObject.achievements = _achievements;
		progressionAndSetDataObject.sendMetrics = _sendMetrics;
		progressionAndSetDataObject.chests = _chests;
		progressionAndSetDataObject.editorResources = _editorResources;
		bool flag = false;
		if (PsMetagameManager.m_suspiciousActivity)
		{
			flag = true;
			PsMetagameManager.m_suspiciousActivity = false;
		}
		HttpC httpC = Player.SetData(progressionAndSetDataObject.setData, progressionAndSetDataObject.pathJson, progressionAndSetDataObject.customisations, progressionAndSetDataObject.achievements, progressionAndSetDataObject.chests, progressionAndSetDataObject.editorResources, _okCallback, _failCallback, _errorCallback, flag);
		httpC.objectData = progressionAndSetDataObject;
		return httpC;
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0006EB5C File Offset: 0x0006CF5C
	public static HttpC ServerChangeName(string _newName, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return Player.ChangeName(_newName, _okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0006EB74 File Offset: 0x0006CF74
	public static HttpC ServerSendQuit(SendQuitData _sendQuitData, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null, bool _mainPath = false)
	{
		HttpC httpC = HighScore.SendQuit(_sendQuitData.gameLoop.m_context, _sendQuitData.gameLoop.ElapsedTime(), _sendQuitData.startCount, _sendQuitData.gameLoop.m_minigameMetaData, _okCallback, _failCallback, _errorCallback, _mainPath, 0);
		httpC.objectData = _sendQuitData;
		return httpC;
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x0006EBBC File Offset: 0x0006CFBC
	public static HttpC ServerGetFriends(string _playerId, Action<Friends> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Player.Friends(_okCallback, _failCallback, _playerId, _errorCallback);
		httpC.objectData = _playerId;
		return httpC;
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x0006EBDC File Offset: 0x0006CFDC
	public static HttpC ServerGetFollowees(string _playerId, Action<PlayerData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Player.Followees(_okCallback, _failCallback, _playerId, _errorCallback);
		httpC.objectData = _playerId;
		return httpC;
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0006EBFC File Offset: 0x0006CFFC
	public static HttpC ServerGetFollowers(string _playerId, Action<PlayerData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Player.Followers(_okCallback, _failCallback, _playerId, _errorCallback);
		httpC.objectData = _playerId;
		return httpC;
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0006EC1C File Offset: 0x0006D01C
	public static HttpC ServerUnFollow(string _playerId, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Player.UnFollow(_playerId, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _playerId;
		return httpC;
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x0006EC3C File Offset: 0x0006D03C
	public static HttpC ServerComment(PsMetagameManager.CommentInfo _info, Action<CommentData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Comment.Save(_info.gameId, _info.comment, _info.tag, _info.tournamentId, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _info;
		return httpC;
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x0006EC7C File Offset: 0x0006D07C
	public static HttpC ServerGetComments(string _id, Action<CommentData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Comment.Get(_id, _okCallback, _failCallback, _errorCallback, 50);
		httpC.objectData = _id;
		return httpC;
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x0006ECA0 File Offset: 0x0006D0A0
	public static HttpC ServerAchievement(string _json, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = global::Server.Achievement.Upsert(_json, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _json;
		return httpC;
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0006ECC0 File Offset: 0x0006D0C0
	public static HttpC ServerSaveProgression(SendPathData _data, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		bool flag = false;
		if (PsMetagameManager.m_suspiciousActivity)
		{
			flag = true;
			PsMetagameManager.m_suspiciousActivity = false;
		}
		HttpC httpC = Player.SetData(null, _data.jsonData, null, null, null, null, _okCallback, _failCallback, _errorCallback, flag);
		httpC.objectData = _data;
		return httpC;
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0006ED00 File Offset: 0x0006D100
	public static HttpC ServerPlayerSkipLevel(string _playerUnit, Hashtable _setData, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Player.SkipLevel(_playerUnit, _setData, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _playerUnit;
		return httpC;
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x0006ED24 File Offset: 0x0006D124
	public static HttpC ServerDeleteMinigame(MiniGame.DeleteMinigameData _data, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = MiniGame.Delete(_data.minigameId, _data.editorResources, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _data;
		return httpC;
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0006ED50 File Offset: 0x0006D150
	public static HttpC ServerClaimAllMinigames(Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return MiniGame.ClaimAllRewards(_okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0006ED68 File Offset: 0x0006D168
	public static HttpC ServerCreateNewTeam(TeamData _team, Action<TeamData> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Team.Create(_team, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _team;
		return httpC;
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x0006ED88 File Offset: 0x0006D188
	public static HttpC ServerSaveTeam(TeamData _team, Action<TeamData> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Team.Update(_team, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _team;
		return httpC;
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x0006EDA8 File Offset: 0x0006D1A8
	public static HttpC ServerGetTeam(string _teamId, bool _own, Action<TeamData> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Team.Get(_teamId, _own, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _teamId;
		return httpC;
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x0006EDCC File Offset: 0x0006D1CC
	public static HttpC ServerGetTeamSuggestions(Action<TeamData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return Team.GetSuggestions(_okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x0006EDE4 File Offset: 0x0006D1E4
	public static HttpC ServerGetTeamLeaderboard(Action<TeamData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return Team.GetLeaderboard(_okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x0006EDFC File Offset: 0x0006D1FC
	public static HttpC ServerTeamSearch(string _query, Action<TeamData[]> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Team.Search(_query, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _query;
		return httpC;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0006EE1C File Offset: 0x0006D21C
	public static HttpC ServerLeaveTeam(string _teamId, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Team.Leave(_teamId, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _teamId;
		return httpC;
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x0006EE3C File Offset: 0x0006D23C
	public static HttpC ServerJoinTeam(string _teamId, Action<TeamData> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Team.Join(_teamId, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _teamId;
		return httpC;
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x0006EE5C File Offset: 0x0006D25C
	public static HttpC ServerClaimSeasonRewards(Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return global::Server.Season.Claim(_okCallback, _failCallback, _errorCallback);
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x0006EE74 File Offset: 0x0006D274
	public static HttpC ServerUpdatePlayerSettings(PlayerUpdateSettings _settings, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Player.UpdateSettings(_settings, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _settings;
		return httpC;
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x0006EE98 File Offset: 0x0006D298
	public static HttpC ServerClaimTeamKick(string _teamKickReason, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Team.ClaimKick(_teamKickReason, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _teamKickReason;
		return httpC;
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x0006EEB8 File Offset: 0x0006D2B8
	public static HttpC ServerClaimEventMessage(int _id, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Event.Claim(_id, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _id;
		return httpC;
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x0006EEDC File Offset: 0x0006D2DC
	public static HttpC ServerClaimPathNote(int _id, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Event.ClaimPatchNotes(_id, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _id;
		return httpC;
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x0006EF00 File Offset: 0x0006D300
	public static HttpC ServerClaimGift(Event.GiftClaimData _data, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Event.ClaimGift(_data, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _data;
		return httpC;
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x0006EF20 File Offset: 0x0006D320
	public static HttpC ServerOpenChest(Player.ChestOpenData _data, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC httpC = Player.OpenChest(_data, _okCallback, _failCallback, _errorCallback);
		httpC.objectData = _data;
		return httpC;
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x0006EF40 File Offset: 0x0006D340
	public static HttpC ServerGetYoutubeChannels(string _name, Action<List<YoutubeChannelInfo>> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC channels = Youtube.GetChannels(_name, null, _okCallback, _failCallback, _errorCallback);
		channels.objectData = _name;
		return channels;
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x0006EF60 File Offset: 0x0006D360
	public static HttpC ServerGetYoutubeChannelById(string _id, Action<List<YoutubeChannelInfo>> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		HttpC channels = Youtube.GetChannels(null, _id, _okCallback, _failCallback, _errorCallback);
		channels.objectData = _id;
		return channels;
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x0006EF80 File Offset: 0x0006D380
	public static HttpC ServerChangeYoutuber(string _name, string _id, int _subscribers, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
	{
		return Player.ChangeYoutuber(_name, _id, (long)_subscribers, _okCallback, _failCallback, _errorCallback);
	}
}
