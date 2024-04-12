using System;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class PsUIProfileSavedBanner : UIHorizontalList
{
	// Token: 0x06001776 RID: 6006 RVA: 0x000FEFBC File Offset: 0x000FD3BC
	public PsUIProfileSavedBanner(UIComponent _parent, PsGameLoop _loop)
		: base(_parent, "savedBanner")
	{
		this.m_gameloop = _loop;
		this.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ProfileCardBackgroundDrawhandler));
		PsUIScreenshot psUIScreenshot = new PsUIScreenshot(this, false, string.Empty, Vector2.zero, this.m_gameloop, true, true, 0.03f, false);
		psUIScreenshot.SetDrawHandler(new UIDrawDelegate(PsUIScreenshot.BasicDrawHandler));
		psUIScreenshot.SetHeight(1f, RelativeTo.ParentHeight);
		psUIScreenshot.SetWidth(1.3f, RelativeTo.OwnHeight);
		psUIScreenshot.SetDepthOffset(-3f);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0.01f, 0.01f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		uiverticalList.SetDepthOffset(-3f);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.45f, RelativeTo.ParentHeight);
		uicanvas.SetWidth(0.27f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.EDITOR_BUTTON_SAVED_LEVEL) + " " + this.m_gameloop.GetName().Substring(this.m_gameloop.GetName().Length - 1, 1), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#AFFF2E", "#13245E");
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetHorizontalAlign(0.45f);
		uihorizontalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		this.m_build = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.003f, "Button");
		this.m_build.SetFittedText(PsStrings.Get(StringID.CONTINUE), 0.035f, 0.11f, RelativeTo.ScreenHeight, true);
		this.m_build.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_build.SetHeight(0.55f, RelativeTo.ParentHeight);
		this.m_delete = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.003f, "Button");
		this.m_delete.SetMargins(0.01f, 0.01f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_delete.SetHeight(0.55f, RelativeTo.ParentHeight);
		this.m_delete.SetIcon("menu_icon_delete", 1.5f, RelativeTo.ParentHeight, "#FFFFFF", default(cpBB));
		this.m_delete.SetRedColors();
	}

	// Token: 0x04001A3D RID: 6717
	public PsGameLoop m_gameloop;

	// Token: 0x04001A3E RID: 6718
	public PsUIGenericButton m_build;

	// Token: 0x04001A3F RID: 6719
	public PsUIGenericButton m_delete;
}
