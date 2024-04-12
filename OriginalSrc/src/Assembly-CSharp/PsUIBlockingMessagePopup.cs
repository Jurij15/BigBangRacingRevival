using System;

// Token: 0x0200039B RID: 923
public class PsUIBlockingMessagePopup : PsUIHeaderedCanvas
{
	// Token: 0x06001A6F RID: 6767 RVA: 0x00126E4C File Offset: 0x0012524C
	public PsUIBlockingMessagePopup(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.SetHeight(0.2f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.Destroy();
		this.m_header = null;
		this.m_text = new UITextbox(this, false, string.Empty, "TEMP_POPUP", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		CameraS.SetAsOverlayCamera(this.m_camera);
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x00126F4B File Offset: 0x0012534B
	public void SetText(string _msg)
	{
		this.m_text.SetText(_msg);
	}

	// Token: 0x04001CF4 RID: 7412
	private UITextbox m_text;
}
