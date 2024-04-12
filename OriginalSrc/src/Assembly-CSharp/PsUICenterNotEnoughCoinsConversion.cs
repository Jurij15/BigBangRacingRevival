using System;

// Token: 0x02000300 RID: 768
public class PsUICenterNotEnoughCoinsConversion : PsUIHeaderedCanvas
{
	// Token: 0x06001694 RID: 5780 RVA: 0x000EE3A4 File Offset: 0x000EC7A4
	public PsUICenterNotEnoughCoinsConversion(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
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

	// Token: 0x06001695 RID: 5781 RVA: 0x000EE488 File Offset: 0x000EC888
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

	// Token: 0x06001696 RID: 5782 RVA: 0x000EE5A4 File Offset: 0x000EC9A4
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		string text = ((PsMetagameManager.CoinsToDiamonds(PsMetagameManager.m_coinsToDiamondsConvertAmount, true) >= 2) ? PsStrings.Get(StringID.NOT_ENOUGH_COINS_MULTIPLE) : PsStrings.Get(StringID.NOT_ENOUGH_COINS_SINGLE));
		text = text.Replace("%1", "<color=#fbdd28>" + PsMetagameManager.m_coinsToDiamondsConvertAmount + "</color>");
		text = text.Replace("%2", "<color=#f15bfb>" + PsMetagameManager.CoinsToDiamonds(PsMetagameManager.m_coinsToDiamondsConvertAmount, true) + "</color> ");
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(1f, 0f);
		uihorizontalList.SetMargins(0f, 0.1f, 0.05f, -0.05f, RelativeTo.ScreenHeight);
		this.m_buy = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_buy.SetAlign(1f, 1f);
		this.m_buy.SetText(PsStrings.Get(StringID.GET_COINS), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_buy.SetDiamondPrice(PsMetagameManager.CoinsToDiamonds(PsMetagameManager.m_coinsToDiamondsConvertAmount, true), 0.05f);
		this.m_buy.SetGreenColors(true);
	}

	// Token: 0x06001697 RID: 5783 RVA: 0x000EE710 File Offset: 0x000ECB10
	public override void Step()
	{
		if (this.m_buy.m_hit)
		{
			if (PsMetagameManager.m_playerStats.diamonds >= PsMetagameManager.CoinsToDiamonds(PsMetagameManager.m_coinsToDiamondsConvertAmount, true))
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Upgrade");
			}
			else if (this.m_customShopAction != null)
			{
				this.m_customShopAction.Invoke();
			}
			else
			{
				new PsGetDiamondsFlow(null, null, null);
			}
		}
		base.Step();
	}

	// Token: 0x04001950 RID: 6480
	private PsUIGenericButton m_buy;

	// Token: 0x04001951 RID: 6481
	public Action m_customShopAction;
}
