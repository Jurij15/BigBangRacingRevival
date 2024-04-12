using System;

// Token: 0x020002E0 RID: 736
public class PsUITopStartRace : UICanvas
{
	// Token: 0x060015BD RID: 5565 RVA: 0x000E1FD0 File Offset: 0x000E03D0
	public PsUITopStartRace(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetSound("/UI/ExitLevel");
		this.m_exitButton.SetIcon("hud_icon_menu_exit", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetOrangeColors(true);
		if (PsState.m_activeGameLoop.m_path != null && PsState.m_activeGameLoop.m_path.GetPathType() == PsPlanetPathType.main && PsState.m_activeGameLoop.m_nodeId == PsState.m_activeGameLoop.m_path.m_currentNodeId && PsState.m_activeGameLoop.m_scoreBest == 0 && PsState.m_activeMinigame.m_gameStartCount > 0)
		{
			this.m_skipButton = new PsUISkipLevelButton(uihorizontalList, new Action(this.Skip), new Action(this.CancelSkip));
		}
	}

	// Token: 0x060015BE RID: 5566 RVA: 0x000E2114 File Offset: 0x000E0514
	public void Skip()
	{
		(this.GetRoot() as PsUIBasePopup).CallAction("Skip");
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000E212C File Offset: 0x000E052C
	public void CancelSkip()
	{
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x000E2130 File Offset: 0x000E0530
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x0400186A RID: 6250
	private PsUIGenericButton m_exitButton;

	// Token: 0x0400186B RID: 6251
	private PsUISkipLevelButton m_skipButton;
}
