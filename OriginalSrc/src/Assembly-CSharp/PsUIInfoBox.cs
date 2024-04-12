using System;

// Token: 0x0200024A RID: 586
public class PsUIInfoBox : UITextbox
{
	// Token: 0x060011CB RID: 4555 RVA: 0x000ADA30 File Offset: 0x000ABE30
	public PsUIInfoBox(UIComponent _parent, string _text, string _tag = "InfoBox", bool _touchable = false, string _fontResourcePath = "KGSecondChances_Font", float _fontSize = 0.025f, RelativeTo _relation = RelativeTo.ScreenHeight)
		: base(_parent, _touchable, _tag, _text, _fontResourcePath, _fontSize, _relation, false, Align.Left, Align.Top, null, true, null)
	{
		this.SetWidth(0.575f, RelativeTo.ScreenHeight);
		this.SetMargins(0.035f, 0.035f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetRogue();
		uicanvas.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(0f, 1f);
		uicanvas.SetMargins(-0.055f, 0.055f, -0.04f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
	}
}
