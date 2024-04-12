using System;

// Token: 0x020002C4 RID: 708
public class PsUISkullRiderHeader : UICanvas
{
	// Token: 0x060014ED RID: 5357 RVA: 0x000DA450 File Offset: 0x000D8850
	public PsUISkullRiderHeader(UIComponent _parent, string _cc)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.SetHeight(0.16f, RelativeTo.ScreenHeight);
		this.SetWidth(0.42f, RelativeTo.ScreenWidth);
		this.SetMargins(-0.5f, 0.5f, 0.1f, -0.1f, RelativeTo.OwnHeight);
		this.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(this, false, string.Empty, PsStrings.Get(StringID.BOSS_BATTLE_STARTSCREEN_HEADER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ec1c96", "#000000");
		UICanvas uicanvas = new UICanvas(uifittedText, false, string.Empty, null, string.Empty);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetMargins(0f, -1f, 0f, 0f, RelativeTo.ParentHeight);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_skullrider_header_icon", null), true, true);
		uifittedSprite.SetHorizontalAlign(1f);
		UICanvas uicanvas2 = new UICanvas(uifittedSprite, false, string.Empty, null, string.Empty);
		uicanvas2.RemoveDrawHandler();
		uicanvas2.SetMargins(0f, 0f, 0.16f, -0.16f, RelativeTo.ParentHeight);
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_cc_small", null), true, true);
		uifittedSprite2.SetVerticalAlign(0f);
		uifittedSprite2.SetMargins(0.1f, 0.1f, 0.03f, 0.03f, RelativeTo.OwnHeight);
		UIFittedText uifittedText2 = new UIFittedText(uifittedSprite2, false, string.Empty, _cc + "cc", PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", null);
	}
}
