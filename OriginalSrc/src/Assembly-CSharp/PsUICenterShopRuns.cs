using System;
using AdMediation;
using UnityEngine;

// Token: 0x0200033F RID: 831
public class PsUICenterShopRuns : PsUIHeaderedCanvas
{
	// Token: 0x06001860 RID: 6240 RVA: 0x0010855C File Offset: 0x0010695C
	public PsUICenterShopRuns(UIComponent _parent)
		: base(_parent, "RacingRunsShop", true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		CameraS.CreateBlur(CameraS.m_mainCamera, null);
		PsMetagameManager.ShowResources(this.m_camera, true, false, true, false, 0.03f, false, false, false);
		TweenS.AddTransformTween(_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
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
		int triesForGems = ShopRuns.GetTriesForGems();
		UICanvas uicanvas = new UICanvas(this.m_purchaseButton, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.13f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetVerticalAlign(1f);
		string text = PsStrings.Get(StringID.SHOP_ITEM_TRIES);
		text = text.Replace("%1", string.Empty + triesForGems);
		string text2 = text;
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		uifittedText.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UISprite uisprite = new UISprite(this.m_purchaseButton, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tries", null), true);
		uisprite.SetSize(0.9f, uisprite.m_frame.height / uisprite.m_frame.width * 0.9f, RelativeTo.ParentWidth);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(this.m_purchaseButton, string.Empty);
		uihorizontalList2.SetVerticalAlign(0f);
		uihorizontalList2.SetSpacing(0.05f, RelativeTo.ParentWidth);
		uihorizontalList2.RemoveDrawHandler();
		UIText uitext = new UIText(uihorizontalList2, false, string.Empty, ShopRuns.GetGemPrice().ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.15f, RelativeTo.ParentHeight, "#FFFFFF", "#000000");
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
			int triesForAd = ShopRuns.GetTriesForAd();
			UICanvas uicanvas2 = new UICanvas(this.m_watchAdButton, false, string.Empty, null, string.Empty);
			uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas2.SetHeight(0.13f, RelativeTo.ParentHeight);
			uicanvas2.RemoveDrawHandler();
			uicanvas2.SetVerticalAlign(1f);
			string text3 = PsStrings.Get(StringID.SHOP_ITEM_TRIES);
			text3 = text3.Replace("%1", string.Empty + triesForAd);
			string text4 = text3;
			UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, text4, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
			uifittedText2.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
			UISprite uisprite3 = new UISprite(this.m_watchAdButton, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tries", null), true);
			uisprite3.SetSize(0.9f, uisprite3.m_frame.height / uisprite3.m_frame.width * 0.9f, RelativeTo.ParentWidth);
			UISprite uisprite4 = new UISprite(uisprite3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_watch_ad_badge", null), true);
			uisprite4.SetSize(0.36f, 0.36f, RelativeTo.ParentWidth);
			uisprite4.SetVerticalAlign(0.075f);
			uisprite4.SetHorizontalAlign(0.925f);
			UICanvas uicanvas3 = new UICanvas(this.m_watchAdButton, false, string.Empty, null, string.Empty);
			uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas3.SetHeight(0.12f, RelativeTo.ParentHeight);
			uicanvas3.RemoveDrawHandler();
			uicanvas3.SetVerticalAlign(0f);
			UIFittedText uifittedText3 = new UIFittedText(uicanvas3, false, string.Empty, PsStrings.Get(StringID.NITRO_FILL_WATCH_AD), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
			uifittedText3.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
			PsMetrics.AdOffered("runsReloaded");
		}
		else
		{
			PsMetrics.AdNotAvailable("runsReloaded");
		}
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001861 RID: 6241 RVA: 0x00108BF4 File Offset: 0x00106FF4
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.075f, 0.075f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		string text = PsStrings.Get(StringID.SHOP_TRIES_EMPTY);
		UIFittedText uifittedText = new UIFittedText(uihorizontalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x00108C70 File Offset: 0x00107070
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		if (this.m_purchaseButton != null && this.m_purchaseButton.m_hit)
		{
			int gemPrice = ShopRuns.GetGemPrice();
			if (PsMetagameManager.m_playerStats.diamonds >= gemPrice)
			{
				PsMetagameManager.m_playerStats.CumulateDiamonds(-gemPrice);
				this.GiveRuns(ShopRuns.GetTriesForGems(), "Gems", gemPrice);
				(this.GetRoot() as PsUIBasePopup).CallAction("Purchased");
			}
			else
			{
				new PsGetDiamondsFlow(null, null, null);
			}
		}
		else if (this.m_watchAdButton != null && this.m_watchAdButton.m_hit)
		{
			PsAdMediation.ShowAd(new Action<AdResult>(this.AdCallBack), null);
		}
		else if (this.m_giveup != null && this.m_giveup.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Giveup");
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x00108DA4 File Offset: 0x001071A4
	protected void AdCallBack(AdResult _result)
	{
		TouchAreaS.Enable();
		SoundS.PauseMixer(false);
		Debug.Log("Ad display result: " + _result.ToString(), null);
		PsMetrics.AdWatched("raceTryReload", _result.ToString());
		if (_result == AdResult.Finished)
		{
			this.GiveRuns(ShopRuns.GetTriesForAd(), "Ad", 0);
			(this.GetRoot() as PsUIBasePopup).CallAction("Purchased");
		}
		else if (_result == AdResult.Failed || _result == AdResult.Skipped)
		{
			Debug.LogError("Ad skipped or failed");
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x00108E50 File Offset: 0x00107250
	public void GiveRuns(int _count, string _type, int _price = 0)
	{
		PsMetrics.RaceTriesBought(_count, _type, _price);
		if (_type == "Gems")
		{
			FrbMetrics.SpendVirtualCurrency("race_runs", "gems", 15.0);
		}
		(PsState.m_activeGameLoop as PsGameLoopRacing).m_purchasedRuns += _count;
		(PsState.m_activeGameLoop as PsGameLoopRacing).SavePlayerdataAndLoop();
	}

	// Token: 0x06001865 RID: 6245 RVA: 0x00108EB3 File Offset: 0x001072B3
	public override void Destroy()
	{
		CameraS.RemoveBlur();
		base.Destroy();
	}

	// Token: 0x04001B16 RID: 6934
	private UIRectSpriteButton m_purchaseButton;

	// Token: 0x04001B17 RID: 6935
	private UIRectSpriteButton m_giveup;

	// Token: 0x04001B18 RID: 6936
	private UIRectSpriteButton m_watchAdButton;
}
