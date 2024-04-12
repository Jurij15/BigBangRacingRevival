using System;

// Token: 0x020002A3 RID: 675
public class PsUICenterTutorialSignTextInput : UIScrollableCanvas
{
	// Token: 0x06001453 RID: 5203 RVA: 0x000CEE94 File Offset: 0x000CD294
	public PsUICenterTutorialSignTextInput(TextModel _model)
		: base(null, string.Empty)
	{
		this.m_textModel = _model;
		this.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.m_maxScrollInertialY = 0f;
		this.m_mainArea = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_mainArea.SetHeight(0.4f, RelativeTo.ScreenHeight);
		this.m_mainArea.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_mainArea.SetAlign(0f, 1f);
		this.m_mainArea.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_mainArea, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_backButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_backButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_backButton.SetOrangeColors(true);
		UIVerticalList uiverticalList = new UIVerticalList(this.m_mainArea, string.Empty);
		uiverticalList.SetWidth(0.58f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, "Sign text", PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, null, null);
		uitext.SetColor("#a8e2ff", null);
		uitext.SetMargins(0.03f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uitext.SetHorizontalAlign(0f);
		UICanvas uicanvas = new UICanvas(uiverticalList, true, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.58f, 0.065f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		uicanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.TextfieldDark));
		this.m_textField = new UIFittedText(uicanvas, true, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", null);
		this.m_textField.SetHorizontalAlign(0f);
		this.m_textField.m_tmc.m_textMesh.fontStyle = 2;
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x000CF0FD File Offset: 0x000CD4FD
	public override void Update()
	{
		if (this.m_keyboard == null)
		{
			this.m_keyboard = new Keyboard(this.m_textModel, null, 1);
			this.m_keyboard.OpenKeyboard();
		}
		base.Update();
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x000CF130 File Offset: 0x000CD530
	public override void Step()
	{
		this.m_keyboard.UpdateKeyboard();
		if (this.m_backButton.m_hit)
		{
			this.m_keyboard.CloseKeyboard(true);
			this.Destroy();
			return;
		}
		if (this.m_textModel.m_done || this.m_textModel.m_cancelled)
		{
			this.Destroy();
			return;
		}
		if (this.m_textModel.m_text != this.m_textField.m_text)
		{
			this.m_textField.SetText(this.m_textModel.m_text);
			this.m_textField.Update();
		}
		base.Step();
	}

	// Token: 0x0400171B RID: 5915
	private UICanvas m_mainArea;

	// Token: 0x0400171C RID: 5916
	private PsUIGenericButton m_backButton;

	// Token: 0x0400171D RID: 5917
	private Keyboard m_keyboard;

	// Token: 0x0400171E RID: 5918
	private UIFittedText m_textField;

	// Token: 0x0400171F RID: 5919
	private TextModel m_textModel;
}
