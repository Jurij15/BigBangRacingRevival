using System;

// Token: 0x020003C1 RID: 961
public class PsUICenterShopPurchaseWait : PsUIHeaderedCanvas
{
	// Token: 0x06001B62 RID: 7010 RVA: 0x00131990 File Offset: 0x0012FD90
	public PsUICenterShopPurchaseWait(UIComponent _parent)
		: base(_parent, "GemWait", false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.BgDrawhandler));
		this.SetWidth(0.65f, RelativeTo.ScreenHeight);
		this.SetHeight(0.5f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x00131A9C File Offset: 0x0012FE9C
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.LOADING), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x00131B14 File Offset: 0x0012FF14
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(null);
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.22f, 0.22f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		new PsUILoadingAnimation(uicanvas, false);
		new UIText(uiverticalList, false, PsStrings.Get(StringID.LOADING), PsStrings.Get(StringID.SHOP_PROMPT_STORELOADING) + "!", PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.035f, RelativeTo.ScreenHeight, null, null);
	}
}
