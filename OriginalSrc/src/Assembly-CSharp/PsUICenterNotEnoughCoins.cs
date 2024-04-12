using System;

// Token: 0x020002FF RID: 767
public class PsUICenterNotEnoughCoins : PsUIHeaderedCanvas
{
	// Token: 0x06001690 RID: 5776 RVA: 0x000EE098 File Offset: 0x000EC498
	public PsUICenterNotEnoughCoins(UIComponent _parent)
		: base(_parent, "CenterNotEnoughCoins", true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x000EE17C File Offset: 0x000EC57C
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.NOT_ENOUGH_COINS_TITLE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
		UICanvas uicanvas = new UICanvas(uitext, false, string.Empty, null, string.Empty);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetMargins(-1.5f, 0f, -0.125f, -0.125f, RelativeTo.ParentHeight);
		uicanvas.SetDepthOffset(-2f);
		uicanvas.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_icon", null);
		UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true);
		uisprite.SetSize(1f, 1f * (frame.height / frame.width), RelativeTo.ParentHeight);
		uisprite.SetHorizontalAlign(0f);
	}

	// Token: 0x06001692 RID: 5778 RVA: 0x000EE298 File Offset: 0x000EC698
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		string text = PsStrings.Get(StringID.NOT_ENOUGH_GEMS_TEXT);
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(1f, 0f);
		uihorizontalList.SetMargins(0f, 0.1f, 0.05f, -0.05f, RelativeTo.ScreenHeight);
		this.m_buy = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_buy.SetAlign(1f, 1f);
		this.m_buy.SetText(PsStrings.Get(StringID.ENTER_SHOP), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_buy.SetGreenColors(true);
	}

	// Token: 0x06001693 RID: 5779 RVA: 0x000EE376 File Offset: 0x000EC776
	public override void Step()
	{
		if (this.m_buy.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("EnterShop");
		}
		base.Step();
	}

	// Token: 0x0400194D RID: 6477
	private PsUIGenericButton m_buy;
}
