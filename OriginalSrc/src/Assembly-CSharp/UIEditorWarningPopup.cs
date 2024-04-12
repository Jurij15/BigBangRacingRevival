using System;

// Token: 0x02000397 RID: 919
public class UIEditorWarningPopup : PsUIHeaderedCanvas
{
	// Token: 0x06001A5C RID: 6748 RVA: 0x00126688 File Offset: 0x00124A88
	public UIEditorWarningPopup(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0625f, 0.0625f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.m_headerText = new UIFittedText(this.m_header, false, string.Empty, PsStrings.Get(StringID.EDITOR_POPUP_MAX_LIMIT_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
		this.m_textBox = new UITextbox(this, false, string.Empty, PsStrings.Get(StringID.EDITOR_PROMPT_MAX_COMPLEXITY_REACHED), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "nameChangeButtons");
		uihorizontalList.SetHeight(0.1f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetVerticalAlign(0f);
		uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
		this.m_okButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_okButton.SetText("Ok", 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_okButton.SetGreenColors(true);
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x00126874 File Offset: 0x00124C74
	public override void CreateCloseButton()
	{
		UICanvas uicanvas = new UICanvas(this.m_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.125f, 0.125f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(1f, 1f);
		uicanvas.SetMargins(0.4f, -0.4f, -0.4f, 0.4f, RelativeTo.OwnHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetDepthOffset(-20f);
		this.m_exitButton = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetOrangeColors(true);
		this.m_exitButton.SetSound("/UI/ButtonBack");
		this.m_exitButton.SetIcon("menu_icon_close", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x00126947 File Offset: 0x00124D47
	public override void Step()
	{
		if (this.m_okButton.m_hit || this.m_exitButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).Destroy();
		}
		base.Step();
	}

	// Token: 0x04001CE6 RID: 7398
	private PsUIGenericButton m_okButton;

	// Token: 0x04001CE7 RID: 7399
	public UIFittedText m_headerText;

	// Token: 0x04001CE8 RID: 7400
	public UITextbox m_textBox;
}
