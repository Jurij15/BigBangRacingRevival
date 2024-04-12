using System;
using AdMediation;
using UnityEngine;

// Token: 0x0200021A RID: 538
public class PsUINitroFillUpPopup : PsUIHeaderedCanvas
{
	// Token: 0x06000F92 RID: 3986 RVA: 0x00092294 File Offset: 0x00090694
	public PsUINitroFillUpPopup(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.75f, RelativeTo.ScreenHeight);
		this.SetHeight(0.55f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.45f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0.5f);
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		this.m_purchaseButton = new UIRectSpriteButton(uihorizontalList, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null), true, false);
		this.m_purchaseButton.m_TAC.m_letTouchesThrough = true;
		this.m_purchaseButton.SetWidth(0.31f, RelativeTo.ParentWidth);
		this.m_purchaseButton.SetHeight(this.m_purchaseButton.m_frame.height / this.m_purchaseButton.m_frame.width * 0.31f, RelativeTo.ParentWidth);
		this.m_purchaseButton.SetMargins(0.08f, 0.08f, 0.06f, 0.04f, RelativeTo.OwnWidth);
		UICanvas uicanvas = new UICanvas(this.m_purchaseButton, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.13f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetVerticalAlign(1f);
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.NITRO_FILL_PURCHASE).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		uifittedText.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UISprite uisprite = new UISprite(this.m_purchaseButton, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_ shop_item_fill_boosters", null), true);
		uisprite.SetSize(0.9f, uisprite.m_frame.height / uisprite.m_frame.width * 0.9f, RelativeTo.ParentWidth);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(this.m_purchaseButton, string.Empty);
		uihorizontalList2.SetVerticalAlign(0f);
		uihorizontalList2.SetSpacing(0.05f, RelativeTo.ParentWidth);
		uihorizontalList2.RemoveDrawHandler();
		UIText uitext = new UIText(uihorizontalList2, false, string.Empty, PsMetagameManager.GetBoosterRefillPrice().ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.15f, RelativeTo.ParentHeight, "#FFFFFF", "#000000");
		uitext.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UISprite uisprite2 = new UISprite(uihorizontalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_diamond_icon", null), true);
		uisprite2.SetSize(0.16f, uisprite2.m_frame.height / uisprite2.m_frame.width * 0.16f, RelativeTo.ParentHeight);
		if (PsAdMediation.adsAvailable())
		{
			this.m_watchAdButton = new UIRectSpriteButton(uihorizontalList, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null), true, false);
			this.m_watchAdButton.m_TAC.m_letTouchesThrough = true;
			this.m_watchAdButton.SetWidth(0.31f, RelativeTo.ParentWidth);
			this.m_watchAdButton.SetHeight(this.m_purchaseButton.m_frame.height / this.m_purchaseButton.m_frame.width * 0.31f, RelativeTo.ParentWidth);
			this.m_watchAdButton.SetMargins(0.08f, 0.08f, 0.06f, 0.06f, RelativeTo.OwnWidth);
			UICanvas uicanvas2 = new UICanvas(this.m_watchAdButton, false, string.Empty, null, string.Empty);
			uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas2.SetHeight(0.13f, RelativeTo.ParentHeight);
			uicanvas2.RemoveDrawHandler();
			uicanvas2.SetVerticalAlign(1f);
			UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.NITRO_FILL_REWARD).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
			uifittedText2.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
			UISprite uisprite3 = new UISprite(this.m_watchAdButton, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_ shop_item_fill_boosters", null), true);
			uisprite3.SetSize(0.9f, uisprite3.m_frame.height / uisprite3.m_frame.width * 0.9f, RelativeTo.ParentWidth);
			UISprite uisprite4 = new UISprite(uisprite3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_watch_ad_badge", null), true);
			uisprite4.SetSize(0.5f, 0.5f, RelativeTo.ParentWidth);
			UICanvas uicanvas3 = new UICanvas(this.m_watchAdButton, false, string.Empty, null, string.Empty);
			uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas3.SetHeight(0.12f, RelativeTo.ParentHeight);
			uicanvas3.RemoveDrawHandler();
			uicanvas3.SetVerticalAlign(0f);
			UIFittedText uifittedText3 = new UIFittedText(uicanvas3, false, string.Empty, PsStrings.Get(StringID.NITRO_FILL_WATCH_AD), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
			uifittedText3.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
			PsMetrics.AdOffered("boosterReload");
		}
		else
		{
			PsMetrics.AdNotAvailable("boosterReload");
		}
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x00092870 File Offset: 0x00090C70
	public void ItemHolderDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 0f), 0f);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x000928FC File Offset: 0x00090CFC
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.075f, 0.075f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIFittedText uifittedText = new UIFittedText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.NITRO_FILL_HEADER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x0009297C File Offset: 0x00090D7C
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		if (this.m_purchaseButton != null && this.m_purchaseButton.m_hit)
		{
			if (PsMetagameManager.m_playerStats.diamonds >= PsMetagameManager.GetBoosterRefillPrice())
			{
				PsMetagameManager.PurchaseRefillBoosters();
				(this.GetRoot() as PsUIBasePopup).CallAction("Purchased");
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
			else
			{
				new PsGetDiamondsFlow(null, null, null);
			}
		}
		else if (this.m_watchAdButton != null && this.m_watchAdButton.m_hit)
		{
			TouchAreaS.Disable();
			PsAdMediation.ShowAd(new Action<AdResult>(this.adCallback), null);
		}
		base.Step();
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x00092A68 File Offset: 0x00090E68
	private void adCallback(AdResult _result)
	{
		TouchAreaS.Enable();
		Debug.Log("Ad display result: " + _result.ToString(), null);
		PsMetrics.AdWatched("boosterReload", _result.ToString());
		if (_result == AdResult.Finished)
		{
			PsMetagameManager.AddBoosters(2);
			(this.GetRoot() as PsUIBasePopup).CallAction("Purchased");
		}
		(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
	}

	// Token: 0x04001249 RID: 4681
	private UIRectSpriteButton m_purchaseButton;

	// Token: 0x0400124A RID: 4682
	private UIRectSpriteButton m_watchAdButton;
}
