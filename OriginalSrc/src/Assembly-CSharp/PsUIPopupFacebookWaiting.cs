using System;

// Token: 0x02000313 RID: 787
public class PsUIPopupFacebookWaiting : PsUIHeaderedCanvas
{
	// Token: 0x0600173E RID: 5950 RVA: 0x000FA988 File Offset: 0x000F8D88
	public PsUIPopupFacebookWaiting(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.Destroy();
		string text = PsStrings.Get(StringID.CONNECTING);
		if (PlayerPrefsX.GetFacebookId() != null)
		{
			text = PsStrings.Get(StringID.POPUP_DISCONNECTING_FROM_FB);
		}
		new UITextbox(this, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
	}
}
