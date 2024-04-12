using System;
using System.Collections.Generic;

// Token: 0x02000278 RID: 632
public class PsUILeaderboardListGame : PsUILeaderboardList
{
	// Token: 0x060012DB RID: 4827 RVA: 0x000BA263 File Offset: 0x000B8663
	public PsUILeaderboardListGame(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x000BA26C File Offset: 0x000B866C
	public override void LoadingAnimation()
	{
		this.Load();
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x000BA274 File Offset: 0x000B8674
	protected override void CreateTrophies(UIComponent _parent, LeaderboardEntry _entry)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.175f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(-0.125f, 0.125f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		string text = HighScores.TimeScoreToTimeString(_entry.time);
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), true, null, null);
		uifittedText.SetHorizontalAlign(1f);
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x000BA2FC File Offset: 0x000B86FC
	public void CreateLeaderBoardContent(List<LeaderboardEntry> _list)
	{
		this.CreateContent(_list);
	}
}
