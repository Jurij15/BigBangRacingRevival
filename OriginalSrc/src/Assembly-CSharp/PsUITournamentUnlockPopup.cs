using System;

// Token: 0x02000385 RID: 901
public class PsUITournamentUnlockPopup : PsUIHeaderedCanvas
{
	// Token: 0x06001A04 RID: 6660 RVA: 0x0011FC3C File Offset: 0x0011E03C
	public PsUITournamentUnlockPopup(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.SetHeight(0.5f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_headerText = PsStrings.Get(StringID.TOUR_TOURNAMENT_LOCKED);
		this.m_contentText = PsStrings.Get(StringID.TOUR_UNLOCK_PLAYBUTTON);
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x0011FD54 File Offset: 0x0011E154
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, this.m_headerText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x06001A06 RID: 6662 RVA: 0x0011FDCC File Offset: 0x0011E1CC
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, this.m_contentText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		uitextbox.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_ok.SetAlign(1f, 1f);
		this.m_ok.SetGreenColors(true);
		this.m_ok.SetText(PsStrings.Get(StringID.OK), 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
	}

	// Token: 0x06001A07 RID: 6663 RVA: 0x0011FEBC File Offset: 0x0011E2BC
	public override void Step()
	{
		if (this.m_ok != null && this.m_ok.m_hit)
		{
			base.CallAction("Exit");
			TouchAreaS.CancelAllTouches(null);
		}
		base.Step();
	}

	// Token: 0x04001C61 RID: 7265
	protected string m_headerText = string.Empty;

	// Token: 0x04001C62 RID: 7266
	protected string m_contentText = string.Empty;

	// Token: 0x04001C63 RID: 7267
	private PsUIGenericButton m_ok;
}
