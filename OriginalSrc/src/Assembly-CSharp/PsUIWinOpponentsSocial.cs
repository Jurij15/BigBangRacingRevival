using System;

// Token: 0x020002D8 RID: 728
public class PsUIWinOpponentsSocial : PsUIWinOpponents
{
	// Token: 0x06001598 RID: 5528 RVA: 0x000E03D8 File Offset: 0x000DE7D8
	public PsUIWinOpponentsSocial(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x000E03E1 File Offset: 0x000DE7E1
	protected override void CreateTriesRibbon(UIComponent _parent)
	{
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x000E03E3 File Offset: 0x000DE7E3
	protected override int GetGhostRewardAmount(int _i, GhostData _ghost)
	{
		return _ghost.trophyWin;
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x000E03EC File Offset: 0x000DE7EC
	public override void GetLoopData()
	{
		PsGameLoopSocial psGameLoopSocial = PsState.m_activeGameLoop as PsGameLoopSocial;
		PsGameModeRaceSocial psGameModeRaceSocial = psGameLoopSocial.m_gameMode as PsGameModeRaceSocial;
		this.m_startPosition = psGameLoopSocial.m_startPosition;
		this.m_raceGhostCount = psGameLoopSocial.m_raceGhostCount;
		this.m_currentPos = psGameLoopSocial.GetPosition();
		this.m_ghostWon = false;
		this.m_heatNumber = 0;
		this.m_briefingShown = false;
		this.m_rewardTrophies = psGameModeRaceSocial.m_rewardTrophies;
		this.m_trophiesRewarded = false;
		this.m_initialTrophiesRewarded = false;
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x000E0464 File Offset: 0x000DE864
	public override GhostData GetGhost(int i)
	{
		PsGameModeRaceSocial psGameModeRaceSocial = PsState.m_activeGameLoop.m_gameMode as PsGameModeRaceSocial;
		return (i != 0) ? ((i != 1) ? psGameModeRaceSocial.m_coinGhost : psGameModeRaceSocial.m_diamondGhost) : psGameModeRaceSocial.m_trophyGhost;
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x000E04AC File Offset: 0x000DE8AC
	public override void CreateProfile(RacerProfile _profile, int _position = -1, bool _margin = true)
	{
		PsUIRaceProfileSocial psUIRaceProfileSocial = new PsUIRaceProfileSocial(this.m_playerList, _profile, this.m_winScreen, _margin, _position);
		this.m_players.Add(psUIRaceProfileSocial);
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x000E04DA File Offset: 0x000DE8DA
	protected override void CreateTrophyColumn()
	{
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000E04DC File Offset: 0x000DE8DC
	public override void MoveEventHandler(TweenC _c)
	{
		int num = (int)_c.customObject;
		RacerProfile racerProfile = this.m_profiles[num - 1];
		UIFittedSprite positionSprite = this.m_players[num - 1].m_positionSprite;
		positionSprite.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame(base.GetPositionIconName(num), null));
		positionSprite.Update();
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
}
