using System;

// Token: 0x02000275 RID: 629
public class PsUILeaderboardListLocal : PsUILeaderboardList
{
	// Token: 0x060012C8 RID: 4808 RVA: 0x000B9B48 File Offset: 0x000B7F48
	public PsUILeaderboardListLocal(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x000B9B51 File Offset: 0x000B7F51
	public override void CreateContent(Leaderboard _leaderboard)
	{
		this.CreateContent(_leaderboard.local);
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x000B9B5F File Offset: 0x000B7F5F
	protected override void AddFlag(UIComponent _parent, string _countryCode)
	{
	}
}
