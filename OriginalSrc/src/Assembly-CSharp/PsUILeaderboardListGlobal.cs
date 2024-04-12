using System;

// Token: 0x02000274 RID: 628
public class PsUILeaderboardListGlobal : PsUILeaderboardList
{
	// Token: 0x060012C6 RID: 4806 RVA: 0x000B9B31 File Offset: 0x000B7F31
	public PsUILeaderboardListGlobal(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x000B9B3A File Offset: 0x000B7F3A
	public override void CreateContent(Leaderboard _leaderboard)
	{
		this.CreateContent(_leaderboard.global);
	}
}
