using System;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class PsUIWinOpponentsFresh : PsUIWinOpponents
{
	// Token: 0x0600158E RID: 5518 RVA: 0x000DFDAA File Offset: 0x000DE1AA
	public PsUIWinOpponentsFresh(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x000DFDB3 File Offset: 0x000DE1B3
	protected override void CreateTriesRibbon(UIComponent _parent)
	{
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x000DFDB5 File Offset: 0x000DE1B5
	protected override int GetGhostRewardAmount(int _i, GhostData _ghost)
	{
		return _ghost.trophyWin;
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x000DFDC0 File Offset: 0x000DE1C0
	public override void GetLoopData()
	{
		PsGameLoopFresh psGameLoopFresh = PsState.m_activeGameLoop as PsGameLoopFresh;
		PsGameModeRaceFresh psGameModeRaceFresh = psGameLoopFresh.m_gameMode as PsGameModeRaceFresh;
		this.m_startPosition = psGameLoopFresh.m_startPosition;
		this.m_raceGhostCount = psGameLoopFresh.m_raceGhostCount;
		this.m_currentPos = psGameLoopFresh.GetPosition();
		this.m_ghostWon = false;
		this.m_heatNumber = 0;
		this.m_briefingShown = false;
		this.m_rewardTrophies = psGameModeRaceFresh.m_rewardTrophies;
		this.m_trophiesRewarded = false;
		this.m_initialTrophiesRewarded = false;
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x000DFE38 File Offset: 0x000DE238
	public override GhostData GetGhost(int i)
	{
		PsGameModeRaceFresh psGameModeRaceFresh = PsState.m_activeGameLoop.m_gameMode as PsGameModeRaceFresh;
		return (i != 0) ? ((i != 1) ? psGameModeRaceFresh.m_coinGhost : psGameModeRaceFresh.m_diamondGhost) : psGameModeRaceFresh.m_trophyGhost;
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x000DFE80 File Offset: 0x000DE280
	public override void CreateProfile(RacerProfile _profile, int _position = -1, bool _margin = true)
	{
		PsUIRaceProfile psUIRaceProfile = new PsUIRaceProfileFresh(this.m_playerList, _profile, this.m_winScreen, _margin, _position);
		this.m_players.Add(psUIRaceProfile);
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x000DFEAE File Offset: 0x000DE2AE
	protected override void CreateTrophyColumn()
	{
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x000DFEB0 File Offset: 0x000DE2B0
	public override void CreateRewardInfo(int _index)
	{
		PsGameModeRaceFresh psGameModeRaceFresh = PsState.m_activeGameLoop.m_gameMode as PsGameModeRaceFresh;
		int num = 0;
		for (int i = 0; i < psGameModeRaceFresh.m_allGhosts.Count; i++)
		{
			if (i < _index)
			{
				num += psGameModeRaceFresh.m_allGhosts[i].trophyLoss;
			}
			else
			{
				num += psGameModeRaceFresh.m_allGhosts[i].trophyWin;
			}
		}
		string text = "a3fd3a";
		string text2 = "+" + num.ToString();
		if (num < 0)
		{
			text = "fd601c";
			text2 = num.ToString();
		}
		UIComponent uicomponent = new UIComponent(this.m_infoList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetSize(0.17f, 0.085f, RelativeTo.ScreenHeight);
		uicomponent.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicomponent, string.Empty);
		uihorizontalList.SetHeight(0.085f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(1f);
		uihorizontalList.RemoveDrawHandler();
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, text, "313131");
		uitext.SetShadowShift(new Vector2(0.5f, -0.1f), 0.05f);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_scoreboard_prize_diamond", null), true, true);
		uifittedSprite.SetHeight(0.085f, RelativeTo.ScreenHeight);
		if (this.m_profiles[_index].wonAtCreate)
		{
			uitext.m_tmc.m_textMesh.GetComponent<Renderer>().material.shader = Shader.Find("Framework/FontShader");
			uitext.m_shadowtmc.m_textMesh.GetComponent<Renderer>().material.shader = Shader.Find("Framework/FontShader");
			Color white = Color.white;
			white.a = 0f;
			uitext.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = white;
			uitext.m_shadowtmc.m_textMesh.GetComponent<Renderer>().material.color = white;
			uifittedSprite.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			uifittedSprite.SetColor(white);
		}
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x000E00FC File Offset: 0x000DE4FC
	public override void Step()
	{
		PsGameModeRaceFresh psGameModeRaceFresh = PsState.m_activeGameLoop.m_gameMode as PsGameModeRaceFresh;
		if (psGameModeRaceFresh != null && PsMetagameManager.m_menuResourceView.m_cumulateResourceDidWrap && psGameModeRaceFresh.m_diamondChange > 0 && !this.m_diamondAdded)
		{
			this.m_diamondAdded = true;
			PsMetagameManager.m_menuResourceView.CreateAddedResources(ResourceType.Diamonds, 1, 0f);
		}
		base.Step();
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x000E0164 File Offset: 0x000DE564
	public override void MoveEventHandler(TweenC _c)
	{
		int num = (int)_c.customObject;
		RacerProfile racerProfile = this.m_profiles[num - 1];
		UIFittedSprite positionSprite = this.m_players[num - 1].m_positionSprite;
		positionSprite.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame(base.GetPositionIconName(num), null));
		positionSprite.Update();
		if (racerProfile.won && !racerProfile.wonAtCreate)
		{
			UIComponent winIcon = this.m_players[num - 1].m_winIcon;
			(this.m_players[num - 1] as PsUIRaceProfileFresh).RewardAlphaTween();
			base.CreateParticleEffect(winIcon.m_TC);
			SoundS.PlaySingleShot("/Metagame/BadgeCollected", Vector3.zero, 1f);
			int num2 = racerProfile.amount * PsState.m_bigShardValue;
			PsMetagameManager.m_menuResourceView.CreateFlyingResources(num2, this.m_camera.WorldToScreenPoint(winIcon.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f, ResourceType.Shards, 0f, null, null, null, null, default(Vector2));
		}
		this.m_players[num - 1].m_childs[0].ArrangeContents();
		this.m_players[num - 1].MoveToIndexAtParentsChildList(num);
		base.MoveProfileToIndex(racerProfile, num);
		base.MovePlayerToIndex(this.m_players[num - 1], num);
		this.m_playerCurrentPos--;
		this.m_moveAmount--;
		this.m_moving = false;
		this.m_players[num].m_childs[0].SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TiltedFadedDrawhandler));
		this.m_players[num].m_childs[0].d_Draw(this.m_players[num].m_childs[0]);
		if (this.m_infoList != null)
		{
			base.FadeChildren(this.m_infoList.m_childs[num].m_childs[0] as UIHorizontalList, 0.35f, 0.25f);
		}
		this.m_players[num - 1].m_childs[0].ArrangeContents();
	}

	// Token: 0x04001855 RID: 6229
	private bool m_diamondAdded;
}
