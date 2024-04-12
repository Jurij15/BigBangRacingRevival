using System;
using System.Collections.Generic;
using Server;

// Token: 0x02000272 RID: 626
public class PsUICreatorLeaderboard : PsUILeaderboards<CreatorLeaderboardEntry>
{
	// Token: 0x060012B0 RID: 4784 RVA: 0x000B9020 File Offset: 0x000B7420
	public PsUICreatorLeaderboard(UIComponent _parent)
		: base(_parent, PsStrings.Get(StringID.CREATOR_LEADERBOARD_DESCRIPTION), PsStrings.Get(StringID.LEADERBOARD_INFO_TEXT_CREATORS), PsStrings.Get(StringID.LEADERBOARD_INFO_TOP_CREATORS))
	{
		PsUITabbedCreate.m_selectedTab = 4;
		base.ChangeBoard(PsUITabbedCreate.m_selectedSubTab);
	}

	// Token: 0x060012B1 RID: 4785 RVA: 0x000B9058 File Offset: 0x000B7458
	protected override HttpC GetLeaderboards()
	{
		HttpC topCreators = Trophy.GetTopCreators(new Action<CreatorLeaderboard>(this.CreatorLeaderboardLoadSucceed), new Action<HttpC>(this.LeaderboardLoadFailed), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, topCreators);
		return topCreators;
	}

	// Token: 0x060012B2 RID: 4786 RVA: 0x000B9098 File Offset: 0x000B7498
	protected void CreatorLeaderboardLoadSucceed(CreatorLeaderboard _leaderboard)
	{
		Dictionary<int, List<PlayerData>> offsetEntries = _leaderboard.OffsetEntries;
		this.CreateSeasonTop(offsetEntries);
		this.LeaderboardLoadSucceed(_leaderboard);
	}

	// Token: 0x060012B3 RID: 4787 RVA: 0x000B90BA File Offset: 0x000B74BA
	protected override void LeaderboardLoadSucceed(Leaderboard<CreatorLeaderboardEntry> _leaderboard)
	{
		base.LeaderboardLoadSucceed(_leaderboard);
		base.OnTabSelected(PsUITabbedCreate.m_selectedSubTab);
	}

	// Token: 0x060012B4 RID: 4788 RVA: 0x000B90D0 File Offset: 0x000B74D0
	public override int GetTimeRemaining()
	{
		return Convert.ToInt32(PsMetagameManager.MonthTimeLeft.TotalSeconds);
	}

	// Token: 0x060012B5 RID: 4789 RVA: 0x000B90F0 File Offset: 0x000B74F0
	public override void CreateListItem(int _i)
	{
		PsUITeamProfileBanner psUITeamProfileBanner = new PsUITeamProfileBanner(this.m_playerArea, _i, this.m_currentList[_i].user, false, true, true, false, true, true);
		psUITeamProfileBanner.Update();
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x000B912A File Offset: 0x000B752A
	protected override bool FriendsTabSelected()
	{
		return PsUITabbedCreate.m_selectedSubTab == 3;
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x000B9134 File Offset: 0x000B7534
	private void CreateSeasonTop(Dictionary<int, List<PlayerData>> _entries)
	{
		if (this.m_rightContainer != null)
		{
			Dictionary<int, global::Server.Season.SeasonTop> dictionary = this.ConvertToSeasonTop(_entries);
			PsUISeasonTopCreatorsBanner psUISeasonTopCreatorsBanner = new PsUISeasonTopCreatorsBanner(this.m_rightContainer, dictionary, PsStrings.Get(StringID.LEADERBOARD_TOP_3_CREATORS));
			this.m_rightContainer.m_parent.Update();
		}
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x000B917C File Offset: 0x000B757C
	private Dictionary<int, global::Server.Season.SeasonTop> ConvertToSeasonTop(Dictionary<int, List<PlayerData>> _entries)
	{
		Dictionary<int, global::Server.Season.SeasonTop> dictionary = new Dictionary<int, global::Server.Season.SeasonTop>();
		for (int i = 1; i < _entries.Count; i++)
		{
			List<PlayerData> list = _entries[i];
			dictionary[i] = new global::Server.Season.SeasonTop();
			dictionary[i].topCreators = new List<global::Server.Season.SeasonTop.SeasonTopEntry>();
			int num = 0;
			while (num < 3 && num < list.Count)
			{
				PlayerData playerData = list[num];
				global::Server.Season.SeasonTop.SeasonTopEntry seasonTopEntry = new global::Server.Season.SeasonTop.SeasonTopEntry();
				seasonTopEntry.name = playerData.name;
				seasonTopEntry.score = playerData.creatorLikes;
				dictionary[i].topCreators.Add(seasonTopEntry);
				num++;
			}
		}
		return dictionary;
	}
}
