using System;

// Token: 0x020002EE RID: 750
public class PsUICenterWinBlockLevel : UICanvas
{
	// Token: 0x06001620 RID: 5664 RVA: 0x000E6E78 File Offset: 0x000E5278
	public PsUICenterWinBlockLevel(UIComponent _parent)
		: base(_parent, false, "CenterContent", null, string.Empty)
	{
		PsMetagameManager.ShowResources(this.m_camera, true, true, false, false, 0.03f, false, false, false);
		this.SetWidth(0.85f, RelativeTo.ScreenWidth);
		this.SetHeight(0.85f, RelativeTo.ParentHeight);
		this.SetVerticalAlign(0f);
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DebriefBackground));
		this.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this, "Center");
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetHeight(0.42f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(-0.07f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.RemoveDrawHandler();
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList2.SetHeight(0.35f, RelativeTo.ScreenHeight);
		uihorizontalList2.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList2.RemoveDrawHandler();
		this.m_likeArea = new UIVerticalList(uihorizontalList2, string.Empty);
		this.m_likeArea.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_likeArea.SetWidth(0.225f, RelativeTo.ScreenHeight);
		this.m_likeArea.RemoveDrawHandler();
		UIVerticalList uiverticalList2 = new UIVerticalList(uihorizontalList2, string.Empty);
		uiverticalList2.SetVerticalAlign(0f);
		uiverticalList2.RemoveDrawHandler();
		this.m_continueButton = new PsUIGenericButton(uiverticalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continueButton.SetText(PsStrings.Get(StringID.CONTINUE), 0.07f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		UIVerticalList uiverticalList3 = new UIVerticalList(uihorizontalList2, string.Empty);
		uiverticalList3.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList3.SetWidth(0.25f, RelativeTo.ScreenHeight);
		uiverticalList3.SetAlign(1f, 1f);
		uiverticalList3.RemoveDrawHandler();
		this.m_restartButton = new PsUIGenericButton(uiverticalList3, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.1f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
		this.m_restartButton.SetHorizontalAlign(1f);
		this.m_restartButton.SetMargins(0.035f, 0.035f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x000E70F0 File Offset: 0x000E54F0
	public override void Step()
	{
		if (this.m_restartButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Restart");
		}
		else if (this.m_continueButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x040018E2 RID: 6370
	private PsUIGenericButton m_restartButton;

	// Token: 0x040018E3 RID: 6371
	private PsUIGenericButton m_continueButton;

	// Token: 0x040018E4 RID: 6372
	private PsUIGenericButton m_everyplayButton;

	// Token: 0x040018E5 RID: 6373
	private UIVerticalList m_likeArea;

	// Token: 0x040018E6 RID: 6374
	private bool m_ratingDone;
}
