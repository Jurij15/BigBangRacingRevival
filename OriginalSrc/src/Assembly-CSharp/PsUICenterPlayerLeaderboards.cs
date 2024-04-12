using System;
using System.Collections.Generic;
using Server;

// Token: 0x02000271 RID: 625
public class PsUICenterPlayerLeaderboards : PsUILeaderboards<LeaderboardEntry>
{
	// Token: 0x060012A7 RID: 4775 RVA: 0x000B8E80 File Offset: 0x000B7280
	public PsUICenterPlayerLeaderboards(UIComponent _parent)
		: base(_parent, PsStrings.Get(StringID.TEAM_BIG_BANG_POINTS_INFO), PsStrings.Get(StringID.LEADERBOARD_INFO_TEXT_TEAMS), PsStrings.Get(StringID.LEADERBOARD_INFO_TOP_TEAMS))
	{
		PsUITabbedTeam.m_selectedTab = 4;
		base.ChangeBoard(PsUITabbedTeam.m_selectedSubTab);
		this.CreateSeasonTop();
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x000B8EC0 File Offset: 0x000B72C0
	protected override HttpC GetLeaderboards()
	{
		HttpC httpC = Trophy.Leaderboard("All", new Action<Leaderboard>(this.LeaderboardLoadSucceed), new Action<HttpC>(this.LeaderboardLoadFailed), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
		return httpC;
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x000B8F05 File Offset: 0x000B7305
	protected override void LeaderboardLoadSucceed(Leaderboard<LeaderboardEntry> _leaderboard)
	{
		base.LeaderboardLoadSucceed(_leaderboard);
		base.OnTabSelected(PsUITabbedTeam.m_selectedSubTab);
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x000B8F19 File Offset: 0x000B7319
	public override int GetTimeRemaining()
	{
		return PsMetagameManager.m_seasonTimeleft;
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x000B8F20 File Offset: 0x000B7320
	public override void CreateListItem(int _i)
	{
		PsUITeamProfileBanner psUITeamProfileBanner = new PsUITeamProfileBanner(this.m_playerArea, _i, this.m_currentList[_i].user, false, true, false, false, false, false);
		psUITeamProfileBanner.Update();
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x000B8F5A File Offset: 0x000B735A
	protected override bool FriendsTabSelected()
	{
		return PsUITabbedTeam.m_selectedSubTab == 3;
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x000B8F64 File Offset: 0x000B7364
	private void CreateSeasonTop()
	{
		HttpC previousSeasons = global::Server.Season.GetPreviousSeasons(PsMetagameManager.m_seasonEndData.currentSeason.number, 10, new Action<Dictionary<int, global::Server.Season.SeasonTop>>(this.PreviousSeasonsOK), new Action<HttpC>(this.PreviousSeasonsFailed), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, previousSeasons);
	}

	// Token: 0x060012AE RID: 4782 RVA: 0x000B8FB4 File Offset: 0x000B73B4
	private void PreviousSeasonsOK(Dictionary<int, global::Server.Season.SeasonTop> _seasons)
	{
		if (_seasons.Count > 0 && this.m_rightContainer != null)
		{
			int num = PsMetagameManager.m_seasonEndData.currentSeason.number - 1;
			string text = PsStrings.Get(StringID.LEADERBOARD_TOP_3_PLAYERS);
			PsUISeasonTopPlayersBanner psUISeasonTopPlayersBanner = new PsUISeasonTopPlayersBanner(this.m_rightContainer, _seasons, text, num);
			this.m_rightContainer.m_parent.Update();
		}
	}

	// Token: 0x060012AF RID: 4783 RVA: 0x000B9014 File Offset: 0x000B7414
	private void PreviousSeasonsFailed(HttpC _httpc)
	{
		Debug.LogError("fail");
	}
}
