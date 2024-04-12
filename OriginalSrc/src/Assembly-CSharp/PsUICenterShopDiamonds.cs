using System;
using System.Collections.Generic;

// Token: 0x0200033E RID: 830
public class PsUICenterShopDiamonds : PsUIScrollableBase
{
	// Token: 0x06001859 RID: 6233 RVA: 0x00108134 File Offset: 0x00106534
	public PsUICenterShopDiamonds(UIComponent _parent)
		: base(_parent)
	{
		this.m_purchasing = false;
		PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, false, false, false);
		PsUIInfoBar psUIInfoBar = new PsUIInfoBar(this.GetRoot(), string.Empty, false);
		psUIInfoBar.SetVerticalAlign(0f);
		psUIInfoBar.SetText("Speed up your progression with GEMS.");
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x0010818C File Offset: 0x0010658C
	public override void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, "Get more gems", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
		UICanvas uicanvas = new UICanvas(uitext, false, string.Empty, null, string.Empty);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetMargins(-1.5f, 0f, -0.125f, -0.125f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_diamond_icon", null);
		UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true);
		uisprite.SetSize(1f, 1f * (frame.height / frame.width), RelativeTo.ParentHeight);
		uisprite.SetHorizontalAlign(0f);
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x00108298 File Offset: 0x00106698
	public override void CreateContent(UIComponent _parent)
	{
		this.m_buttons = new List<PsUIGenericButton>();
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenHeight);
		PsUIGenericButton psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetShopButton("<color=#749fc0>$4.99</color>", "500 GEMS", "menu_shop_item_gems_tier1", false, false, false, false);
		psUIGenericButton.m_identifier = "500gems";
		psUIGenericButton.m_TAC.m_letTouchesThrough = true;
		this.m_buttons.Add(psUIGenericButton);
		psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetShopButton("<color=#749fc0>$9.99</color>", "1,200 GEMS", "menu_shop_item_gems_tier2", false, false, false, false);
		psUIGenericButton.m_identifier = "1200gems";
		psUIGenericButton.m_TAC.m_letTouchesThrough = true;
		this.m_buttons.Add(psUIGenericButton);
		psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetShopButton("<color=#749fc0>$19.99</color>", "2,500 GEMS", "menu_shop_item_gems_tier3", false, false, false, false);
		psUIGenericButton.m_identifier = "2500gems";
		psUIGenericButton.m_TAC.m_letTouchesThrough = true;
		this.m_buttons.Add(psUIGenericButton);
		psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetShopButton("<color=#749fc0>$49.99</color>", "6,500 GEMS", "menu_shop_item_gems_tier4", false, false, false, false);
		psUIGenericButton.m_identifier = "6500gems";
		psUIGenericButton.m_TAC.m_letTouchesThrough = true;
		this.m_buttons.Add(psUIGenericButton);
		psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetShopButton("<color=#749fc0>$99.99</color>", "14,000 GEMS", "menu_shop_item_gems_tier5", false, false, false, false);
		psUIGenericButton.m_identifier = "14000gems";
		psUIGenericButton.m_TAC.m_letTouchesThrough = true;
		this.m_buttons.Add(psUIGenericButton);
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x00108498 File Offset: 0x00106898
	public override void Step()
	{
		if (this.m_purchasing)
		{
			base.Step();
			return;
		}
		for (int i = 0; i < this.m_buttons.Count; i++)
		{
			if (this.m_buttons[i].m_hit)
			{
				TouchAreaS.Disable();
				this.m_purchasing = true;
				break;
			}
		}
		base.Step();
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x00108500 File Offset: 0x00106900
	private static void ShowLoadingPopup()
	{
		Debug.Log("Show loading popup", null);
		if (PsUICenterShopDiamonds.m_waitPopup != null)
		{
			PsUICenterShopDiamonds.m_waitPopup.Destroy();
			PsUICenterShopDiamonds.m_waitPopup = null;
		}
		PsUICenterShopDiamonds.m_waitPopup = new PsUIPurchaseWaitPopup(null);
	}

	// Token: 0x0600185E RID: 6238 RVA: 0x00108532 File Offset: 0x00106932
	private static void HideLoadingPopup()
	{
		Debug.Log("Hide loading popup", null);
		if (PsUICenterShopDiamonds.m_waitPopup != null)
		{
			PsUICenterShopDiamonds.m_waitPopup.Destroy();
			PsUICenterShopDiamonds.m_waitPopup = null;
		}
	}

	// Token: 0x04001B13 RID: 6931
	private List<PsUIGenericButton> m_buttons;

	// Token: 0x04001B14 RID: 6932
	private bool m_purchasing;

	// Token: 0x04001B15 RID: 6933
	private static PsUIPurchaseWaitPopup m_waitPopup;
}
