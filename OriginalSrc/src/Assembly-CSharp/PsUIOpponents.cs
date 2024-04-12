using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class PsUIOpponents : UIHorizontalList
{
	// Token: 0x06001573 RID: 5491 RVA: 0x000DD8D4 File Offset: 0x000DBCD4
	public PsUIOpponents(UIComponent _parent)
		: base(_parent, "Opponents")
	{
		this.GetLoopData();
		this.m_playerList = new UIVerticalList(this, "players");
		this.m_playerList.SetSpacing(this.m_playerSpacing, RelativeTo.ScreenHeight);
		this.m_playerList.RemoveDrawHandler();
		PsGameModeBase gameMode = PsState.m_activeGameLoop.m_gameMode;
		int startPosition = this.m_startPosition;
		for (int i = 0; i < gameMode.m_playbackGhosts.Count; i++)
		{
			RacerProfile racerProfile = default(RacerProfile);
			GhostData ghost = this.GetGhost(i);
			racerProfile.name = ghost.name;
			racerProfile.fbId = ghost.facebookId;
			racerProfile.gcId = ghost.gameCenterId;
			racerProfile.trophies = ghost.trophies;
			if (PsState.m_activeGameLoop is PsGameLoopRacing && !string.IsNullOrEmpty((PsState.m_activeGameLoop as PsGameLoopRacing).m_fixedTrophies))
			{
				string[] array = (PsState.m_activeGameLoop as PsGameLoopRacing).m_fixedTrophies.Split(new char[] { ',' });
				racerProfile.trophies = Convert.ToInt32(array[i]);
			}
			if (gameMode.m_playbackGhosts[i].m_ghost.m_vehicleUpgradeItems != null)
			{
				PsUpgradeData customUpgradeData = PsUpgradeManager.GetCustomUpgradeData(gameMode.m_playbackGhosts[i].m_ghost.m_vehicleUpgradeItems, Type.GetType(gameMode.m_playbackGhosts[i].m_ghost.m_unitClass));
				racerProfile.cc = ((int)(customUpgradeData.m_basePerformance + customUpgradeData.m_currentPerformance)).ToString();
			}
			else if (PsState.m_activeGameLoop.m_gameMode.m_playbackGhosts[i].m_ghost.m_upgradeValues != null)
			{
				List<float> list = PsState.m_activeMinigame.m_playerUnit.ParseUpgradeValues(PsState.m_activeGameLoop.m_gameMode.m_playbackGhosts[i].m_ghost.m_upgradeValues);
				float num = PsUpgradeManager.GetMaxPerformance(PsState.m_activeMinigame.m_playerUnit.GetType()) - PsUpgradeManager.GetBasePerformance(PsState.m_activeMinigame.m_playerUnit.GetType());
				float basePerformance = PsUpgradeManager.GetBasePerformance(PsState.m_activeMinigame.m_playerUnit.GetType());
				float num2 = num / 3f * list[0];
				float num3 = num / 3f * list[1];
				float num4 = num / 3f * list[2];
				racerProfile.cc = ((float)((int)num2 + (int)num3 + (int)num4 + (int)basePerformance)).ToString();
			}
			else
			{
				racerProfile.cc = "?";
			}
			racerProfile.teamName = ghost.teamName;
			string text = ((gameMode.m_playbackGhosts[i].m_ghost.m_characterVisualItems == null || gameMode.m_playbackGhosts[i].m_ghost.m_characterVisualItems.Count <= 0) ? "MotocrossHelmet" : gameMode.m_playbackGhosts[i].m_ghost.m_characterVisualItems[0]);
			racerProfile.rival = gameMode.m_allGhosts[i].rival;
			racerProfile.hatIdentifier = text;
			racerProfile.countryCode = ghost.countryCode;
			racerProfile.amount = this.GetGhostRewardAmount(i, ghost);
			racerProfile.type = ((i != 0) ? ((i != 1) ? ResourceType.Coins : ResourceType.Diamonds) : ResourceType.Trophies);
			racerProfile.wonAtCreate = startPosition > 0 && startPosition - 1 <= i;
			racerProfile.won = this.m_currentPos > 0 && this.m_currentPos - 1 <= i;
			this.m_profiles.Add(racerProfile);
		}
		this.RemoveDrawHandler();
	}

	// Token: 0x06001574 RID: 5492 RVA: 0x000DDCD3 File Offset: 0x000DC0D3
	protected virtual int GetGhostRewardAmount(int _i, GhostData _ghost)
	{
		return (_i != 0) ? ((_i != 1) ? PsMetagameManager.GetSecondaryGhostCoinReward() : PsMetagameManager.GetSecondaryGhostDiamondReward()) : ((this.m_currentPos != 1) ? _ghost.trophyLoss : _ghost.trophyWin);
	}

	// Token: 0x06001575 RID: 5493 RVA: 0x000DDD14 File Offset: 0x000DC114
	public virtual void GetLoopData()
	{
		PsGameLoopRacing psGameLoopRacing = PsState.m_activeGameLoop as PsGameLoopRacing;
		PsGameModeRacing psGameModeRacing = psGameLoopRacing.m_gameMode as PsGameModeRacing;
		this.m_startPosition = psGameLoopRacing.m_startPosition;
		this.m_raceGhostCount = psGameLoopRacing.m_raceGhostCount;
		this.m_currentPos = psGameLoopRacing.GetPosition();
		this.m_ghostWon = psGameLoopRacing.m_ghostWon;
		this.m_heatNumber = psGameLoopRacing.m_heatNumber;
		this.m_briefingShown = psGameLoopRacing.m_briefingShown;
		this.m_rewardTrophies = psGameModeRacing.m_rewardTrophies;
		this.m_trophiesRewarded = psGameLoopRacing.m_trophiesRewarded;
		this.m_initialTrophiesRewarded = psGameLoopRacing.m_initialTrophiesRewarded;
		this.m_purchasedRuns = psGameLoopRacing.m_purchasedRuns;
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x000DDDB0 File Offset: 0x000DC1B0
	public virtual GhostData GetGhost(int i)
	{
		PsGameModeRacing psGameModeRacing = PsState.m_activeGameLoop.m_gameMode as PsGameModeRacing;
		return (i != 0) ? ((i != 1) ? psGameModeRacing.m_coinGhost : psGameModeRacing.m_diamondGhost) : psGameModeRacing.m_trophyGhost;
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x000DDDF8 File Offset: 0x000DC1F8
	public void AddPlayer(int _position)
	{
		RacerProfile racerProfile = default(RacerProfile);
		racerProfile.name = PlayerPrefsX.GetUserName();
		racerProfile.fbId = PlayerPrefsX.GetFacebookId();
		racerProfile.gcId = PlayerPrefsX.GetGameCenterId();
		racerProfile.teamName = PlayerPrefsX.GetTeamName();
		racerProfile.trophies = PsMetagameManager.m_playerStats.trophies;
		racerProfile.cc = ((int)PsUpgradeManager.GetCurrentPerformance(PsState.m_activeMinigame.m_playerUnit.GetType())).ToString();
		string text = "Helmet";
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetCharacterCustomisationData().GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		if (installedItemByCategory != null)
		{
			text = installedItemByCategory.m_identifier;
		}
		racerProfile.hatIdentifier = text;
		racerProfile.countryCode = PlayerPrefsX.GetCountryCode();
		racerProfile.amount = 0;
		racerProfile.type = ResourceType.Coins;
		racerProfile.playerId = PlayerPrefsX.GetUserId();
		if (this.m_profiles.Count > 0)
		{
			this.m_profiles.Insert(_position - 1, racerProfile);
		}
		else
		{
			this.m_profiles.Add(racerProfile);
		}
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x000DDEF8 File Offset: 0x000DC2F8
	public string GetRewardIconName(RacerProfile _profile)
	{
		string text = null;
		if (_profile.rival && _profile.wonAtCreate)
		{
			return "menu_chest_badge_active";
		}
		if (_profile.rival && !_profile.wonAtCreate)
		{
			return "menu_chest_badge_inactive";
		}
		switch (_profile.type)
		{
		case ResourceType.Coins:
			text = "menu_scoreboard_prize_coins";
			break;
		case ResourceType.Diamonds:
			text = "menu_scoreboard_prize_diamond";
			break;
		case ResourceType.Trophies:
			text = "menu_scoreboard_prize_chest";
			break;
		case ResourceType.Shards:
			text = "menu_resources_shard_icon";
			break;
		}
		return text;
	}

	// Token: 0x06001579 RID: 5497 RVA: 0x000DDF9C File Offset: 0x000DC39C
	public string GetPositionIconName(int _position)
	{
		string text = null;
		switch (_position)
		{
		case 0:
			text = "menu_position_1st";
			break;
		case 1:
			text = "menu_position_2nd";
			break;
		case 2:
			text = "menu_position_3rd";
			break;
		case 3:
			text = "menu_position_4th";
			break;
		}
		return text;
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x000DDFF4 File Offset: 0x000DC3F4
	public virtual void CreateProfile(RacerProfile _profile, int _position = -1, bool _margin = true)
	{
		PsUIRaceProfile psUIRaceProfile = new PsUIRaceProfile(this.m_playerList, _profile, this.m_winScreen, false, _position);
		this.m_players.Add(psUIRaceProfile);
		if (_profile.playerId == PlayerPrefsX.GetUserId())
		{
			this.m_ownProfile = psUIRaceProfile;
		}
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x000DE040 File Offset: 0x000DC440
	public void RewardInfoDrawhandler(UIComponent _c)
	{
		float num = 0.625f * (float)Screen.height * 0.02f;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num2 = 0.17f * (float)Screen.height;
		float actualHeight = _c.m_actualHeight;
		float num3 = (float)Screen.height * 0.0034f;
		Vector2 zero = Vector2.zero;
		float num4 = num * (float)this.m_players.Count + (float)(this.m_players.Count - 1) * num * (this.m_playerSpacing / 0.085f);
		Vector2[] array = new Vector2[41];
		Vector2[] arc = DebugDraw.GetArc(num3, 10, 85f, 255f, new Vector2(num2 * 0.5f - num3 - num4, actualHeight * -0.5f + num3) + zero);
		Vector2[] arc2 = DebugDraw.GetArc(num3, 10, 60f, 180f, new Vector2(num2 * -0.5f + num3 - num4, actualHeight * -0.5f + num3) + zero);
		Vector2[] arc3 = DebugDraw.GetArc(num3, 10, 85f, 75f, new Vector2(num2 * -0.5f + num3, actualHeight * 0.5f - num3) + zero);
		Vector2[] arc4 = DebugDraw.GetArc(num3, 10, 60f, 0f, new Vector2(num2 * 0.5f - num3, actualHeight * 0.5f - num3) + zero);
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		arc3.CopyTo(array, 20);
		arc4.CopyTo(array, 30);
		array[array.Length - 1] = arc[0];
		Color color = DebugDraw.HexToColor("#000000");
		color.a = 0.65f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f, array, (float)Screen.height * 0.0075f, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 5f, ggdata, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x04001838 RID: 6200
	protected List<RacerProfile> m_profiles = new List<RacerProfile>();

	// Token: 0x04001839 RID: 6201
	public bool m_winScreen;

	// Token: 0x0400183A RID: 6202
	public bool m_coinsCumulated = true;

	// Token: 0x0400183B RID: 6203
	public bool m_diamondsCumulated = true;

	// Token: 0x0400183C RID: 6204
	public List<PsUIRaceProfile> m_players = new List<PsUIRaceProfile>();

	// Token: 0x0400183D RID: 6205
	public PsUIRaceProfile m_ownProfile;

	// Token: 0x0400183E RID: 6206
	public UIVerticalList m_playerList;

	// Token: 0x0400183F RID: 6207
	protected UIVerticalList m_infoList;

	// Token: 0x04001840 RID: 6208
	protected float m_playerSpacing = 0.0225f;

	// Token: 0x04001841 RID: 6209
	public int m_startPosition;

	// Token: 0x04001842 RID: 6210
	public int m_raceGhostCount;

	// Token: 0x04001843 RID: 6211
	public int m_currentPos;

	// Token: 0x04001844 RID: 6212
	public bool m_ghostWon;

	// Token: 0x04001845 RID: 6213
	public int m_heatNumber;

	// Token: 0x04001846 RID: 6214
	public bool m_briefingShown;

	// Token: 0x04001847 RID: 6215
	public bool m_rewardTrophies;

	// Token: 0x04001848 RID: 6216
	public bool m_trophiesRewarded;

	// Token: 0x04001849 RID: 6217
	public bool m_initialTrophiesRewarded;

	// Token: 0x0400184A RID: 6218
	public int m_purchasedRuns;
}
