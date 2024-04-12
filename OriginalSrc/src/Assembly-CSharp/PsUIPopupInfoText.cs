using System;

// Token: 0x02000326 RID: 806
public class PsUIPopupInfoText : PsUIHeaderedCanvas
{
	// Token: 0x060017AE RID: 6062 RVA: 0x0010032C File Offset: 0x000FE72C
	public PsUIPopupInfoText(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.8f, RelativeTo.ScreenWidth);
		this.SetHeight(0.5f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.375f);
		this.SetMargins(0.0125f, 0.0125f, 0.02f, 0.025f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.02f, 0.02f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_header.SetHorizontalAlign(0.5f);
		this.m_headerText = new UIFittedText(this.m_header, false, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
		UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(this, string.Empty);
		uiscrollableCanvas.RemoveTouchAreas();
		uiscrollableCanvas.RemoveDrawHandler();
		this.m_textBox = new UITextbox(uiscrollableCanvas, false, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Top, null, true, null);
	}

	// Token: 0x060017AF RID: 6063 RVA: 0x00100479 File Offset: 0x000FE879
	public void SetTexts(string _header, string _content)
	{
		this.m_headerText.SetText(_header);
		this.m_textBox.SetText(_content);
		this.Update();
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x0010049C File Offset: 0x000FE89C
	public static PsUIBasePopup Create(string _headerText, string _popupText, Action OnClose)
	{
		PsUIBasePopup psUIBasePopup = new PsUIBasePopup(typeof(PsUIPopupInfoText), null, null, null, false, true, InitialPage.Center, false, true, false);
		PsUIPopupInfoText psUIPopupInfoText = psUIBasePopup.m_mainContent as PsUIPopupInfoText;
		psUIPopupInfoText.SetTexts(_headerText, _popupText);
		psUIBasePopup.SetAction("Exit", OnClose);
		return psUIBasePopup;
	}

	// Token: 0x04001A80 RID: 6784
	private UITextbox m_textBox;

	// Token: 0x04001A81 RID: 6785
	private UIFittedText m_headerText;
}
