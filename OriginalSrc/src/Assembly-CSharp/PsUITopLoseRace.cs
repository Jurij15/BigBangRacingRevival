using System;

// Token: 0x020002B5 RID: 693
public class PsUITopLoseRace : UICanvas
{
	// Token: 0x060014B6 RID: 5302 RVA: 0x000D8558 File Offset: 0x000D6958
	public PsUITopLoseRace(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		PsMetagameManager.HideResources();
		this.RemoveDrawHandler();
		this.m_leftArea = new UIHorizontalList(this, "UpperLeft");
		this.m_leftArea.SetMargins(0.025f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetAlign(0f, 1f);
		this.m_leftArea.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(this.m_leftArea, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetSound("/UI/ExitLevel");
		this.m_exitButton.SetIcon("hud_icon_menu_exit", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetOrangeColors(true);
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x000D863C File Offset: 0x000D6A3C
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001784 RID: 6020
	private PsUIGenericButton m_exitButton;

	// Token: 0x04001785 RID: 6021
	private PsUIGenericButton m_everyplayButton;

	// Token: 0x04001786 RID: 6022
	protected UIHorizontalList m_leftArea;
}
