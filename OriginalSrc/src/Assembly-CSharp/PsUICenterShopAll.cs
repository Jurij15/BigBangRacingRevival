using System;
using System.Collections.Generic;
using System.Linq;
using InAppPurchases;
using Prime31;
using UnityEngine;

// Token: 0x0200033A RID: 826
public class PsUICenterShopAll : UICanvas
{
	// Token: 0x06001825 RID: 6181 RVA: 0x00104D78 File Offset: 0x00103178
	public PsUICenterShopAll(UIComponent _parent)
		: base(_parent, false, "CenterShopAll", null, string.Empty)
	{
		FrbMetrics.SetCurrentScreen("shop");
		PsUICenterShopAll.m_shopState++;
		PlayerPrefsX.SetShopNotification(false);
		if (PsMetagameManager.m_menuResourceView != null)
		{
			this.m_lastResourceView = PsMetagameManager.m_menuResourceView.SetLastView();
		}
		PsMetagameManager.ShowResources(null, false, true, true, false, 0.025f, false, false, false);
		this.m_upgradeButtons = new List<UIRectSpriteButton>();
		this.m_chestButtons = new List<UIRectSpriteButton>();
		this.m_coinButtons = new List<UIRectSpriteButton>();
		this.m_gemButtons = new List<UIRectSpriteButton>();
		this.m_nonConsumableButtons = new Dictionary<string, PsUINonconsumableBanner>();
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.RemoveDrawHandler();
		this.m_scrollArea = new UIScrollableCanvas(this, string.Empty);
		this.m_scrollArea.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_scrollArea.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_scrollArea.SetMargins(0.2f, 0.2f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_scrollArea.RemoveDrawHandler();
		this.m_contentArea = new UIVerticalList(this.m_scrollArea, string.Empty);
		this.m_contentArea.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_contentArea.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		this.m_contentArea.SetMargins(0.03f, 0.03f, 0.1125f, 0.05f, RelativeTo.ScreenHeight);
		this.m_contentArea.SetVerticalAlign(1f);
		this.m_contentArea.RemoveDrawHandler();
		List<PsTimedSpecialOffer> startedTimedSpecialOffers = PsMetagameManager.GetStartedTimedSpecialOffers();
		if (startedTimedSpecialOffers != null && startedTimedSpecialOffers.Count > 0)
		{
			UIVerticalList uiverticalList = this.CreateCategory(out this.m_offerShop, this.m_contentArea, "menu_special_offer_icon", PsStrings.Get(StringID.SPECIAL_OFFER_PLURAL), string.Empty, string.Empty);
			this.CreateSpecialOfferBanners(uiverticalList, string.Empty);
		}
		this.m_cards = new UIVerticalList(this.m_contentArea, string.Empty);
		this.m_cards.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		this.m_cards.RemoveDrawHandler();
		this.CreateUpgradeCards();
		UIVerticalList uiverticalList2 = this.CreateCategory(out this.m_chestShop, this.m_contentArea, "menu_scoreboard_prize_chest", PsStrings.Get(StringID.SHOP_HEADER_CHESTS), PsStrings.Get(StringID.SHOP_INFO_CHESTS), string.Empty);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList2, string.Empty);
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		GachaType[] array = new GachaType[]
		{
			GachaType.RARE,
			GachaType.EPIC,
			GachaType.SUPER
		};
		for (int i = 0; i < array.Length; i++)
		{
			UIRectSpriteButton uirectSpriteButton = this.CreatePurchaseItemButton(uihorizontalList, PsGachaManager.GetGachaName(array[i]), PsGachaManager.GetShopPrice(array[i]).ToString(), PsCurrency.Gem);
			uirectSpriteButton.m_identifier = array[i].ToString();
			this.m_chestButtons.Add(uirectSpriteButton);
			PsUICenterShopAll.CreateChest(uirectSpriteButton, array[i]);
		}
		List<ServerProduct> list = PsIAPManager.GetProducts();
		if (PsState.m_gemShopEnabled && list != null && list.Count > 0)
		{
			list = Enumerable.ToList<ServerProduct>(Enumerable.OrderBy<ServerProduct, int>(list.FindAll((ServerProduct product) => product.resource == "gems"), (ServerProduct product) => product.amount));
			list.RemoveAll((ServerProduct product) => !product.visible);
			UIVerticalList uiverticalList3 = this.CreateCategory(out this.m_gemShop, this.m_contentArea, "hud_big_diamond_top", PsStrings.Get(StringID.SHOP_HEADER_GEMS), string.Empty, string.Empty);
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int j = 0; j < list.Count; j++)
			{
				dictionary.Add(list[j].amount, j + 1);
			}
			list = Enumerable.ToList<ServerProduct>(Enumerable.OrderBy<ServerProduct, int>(list, (ServerProduct product) => product.order));
			int num = (int)((float)list.Count / 3f) + ((list.Count % 3 != 0) ? 1 : 0);
			for (int k = 0; k < num; k++)
			{
				UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList3, string.Empty);
				uihorizontalList2.SetSpacing(0.03f, RelativeTo.ScreenHeight);
				uihorizontalList2.RemoveDrawHandler();
				int num2 = k * 3;
				while (num2 < (k + 1) * 3 && num2 < list.Count)
				{
					IAPProduct iapproductById = PsIAPManager.GetIAPProductById(list[num2].identifier);
					string text = string.Empty;
					string text2 = string.Empty;
					if (iapproductById != null)
					{
						text = iapproductById.price;
						text2 = iapproductById.title;
					}
					else
					{
						text2 = PsStrings.Get(StringID.SHOP_PRICE_GEMS);
						text = "Loading...";
					}
					UIRectSpriteButton uirectSpriteButton2 = this.CreatePurchaseItemButton(uihorizontalList2, text2, text, PsCurrency.Real);
					uirectSpriteButton2.m_identifier = list[num2].identifier;
					this.m_gemButtons.Add(uirectSpriteButton2);
					string text3 = "menu_shop_item_gems_tier" + Mathf.Clamp(dictionary[list[num2].amount], 1, 6);
					UISprite uisprite = new UISprite(uirectSpriteButton2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text3, null), true);
					uisprite.SetSize(0.9f, uisprite.m_frame.height / uisprite.m_frame.width * 0.9f, RelativeTo.ParentWidth);
					UICanvas uicanvas = new UICanvas(uisprite, false, string.Empty, null, string.Empty);
					uicanvas.SetHeight(0.3f, RelativeTo.ParentHeight);
					uicanvas.RemoveDrawHandler();
					uicanvas.SetMargins(0.15f, RelativeTo.OwnHeight);
					uicanvas.SetVerticalAlign(1f);
					string text4 = string.Format("{0:n0}", list[num2].amount).Replace(",", " ");
					UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, text4, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", "#000000");
					uifittedText.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
					if (!string.IsNullOrEmpty(list[num2].sticker))
					{
						this.CreateSticker(list[num2].sticker, uisprite);
					}
					num2++;
				}
			}
			this.CreateDirtbikeBundleBanner(uiverticalList3, "dirt_bike_bundle");
		}
		UIVerticalList uiverticalList4 = this.CreateCategory(out this.m_coinShop, this.m_contentArea, "menu_scoreboard_prize_coins", PsStrings.Get(StringID.SHOP_HEADER_COINS), string.Empty, string.Empty);
		UIHorizontalList uihorizontalList3 = new UIHorizontalList(uiverticalList4, string.Empty);
		uihorizontalList3.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList3.RemoveDrawHandler();
		for (int l = 0; l < PsUICenterShopAll.m_coinPrices.Length; l++)
		{
			string text5 = string.Format("{0:n0}", PsMetagameManager.CoinsToDiamonds(PsUICenterShopAll.m_coinPrices[l].m_amount, false)).Replace(",", " ");
			UIRectSpriteButton uirectSpriteButton3 = this.CreatePurchaseItemButton(uihorizontalList3, PsStrings.Get(PsUICenterShopAll.m_coinPrices[l].m_nameID), text5, PsCurrency.Gem);
			uirectSpriteButton3.m_identifier = l.ToString();
			this.m_coinButtons.Add(uirectSpriteButton3);
			UISprite uisprite2 = new UISprite(uirectSpriteButton3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(PsUICenterShopAll.m_coinPrices[l].m_iconName, null), true);
			uisprite2.SetSize(0.9f, uisprite2.m_frame.height / uisprite2.m_frame.width * 0.9f, RelativeTo.ParentWidth);
			UICanvas uicanvas2 = new UICanvas(uisprite2, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.3f, RelativeTo.ParentHeight);
			uicanvas2.RemoveDrawHandler();
			uicanvas2.SetMargins(0.15f, RelativeTo.OwnHeight);
			uicanvas2.SetVerticalAlign(1f);
			string text6 = string.Format("{0:n0}", PsUICenterShopAll.m_coinPrices[l].m_amount).Replace(",", " ");
			UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, text6, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", "#000000");
			uifittedText2.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		}
		this.CreateCoinDoublerBanner(uiverticalList4, "coin_booster");
		if (PsUICenterShopAll.m_scrollIndex != 0)
		{
			switch (PsUICenterShopAll.m_scrollIndex)
			{
			case 1:
				this.ScrollToGemShop();
				break;
			case 2:
				this.ScrollToCoinShop();
				break;
			case 3:
				this.ScrollToChestShop();
				break;
			case 4:
				this.ScrollToDirtBikeBundle();
				break;
			}
			PsUICenterShopAll.m_scrollIndex = 0;
		}
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x00105666 File Offset: 0x00103A66
	public static int GetCoinAmoumt(int _index)
	{
		if (_index < PsUICenterShopAll.m_coinPrices.Length)
		{
			return PsUICenterShopAll.m_coinPrices[_index].m_amount;
		}
		return 0;
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x00105683 File Offset: 0x00103A83
	public void ScrollToCoinShop()
	{
		if (this.m_coinShop != null)
		{
			this.m_scrollArea.SetScrollPositionToChild(this.m_coinShop);
		}
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x001056A1 File Offset: 0x00103AA1
	public void ScrollToGemShop()
	{
		if (this.m_gemShop != null)
		{
			this.m_scrollArea.SetScrollPositionToChild(this.m_gemShop);
		}
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x001056BF File Offset: 0x00103ABF
	public void ScrollToChestShop()
	{
		if (this.m_chestShop != null)
		{
			this.m_scrollArea.SetScrollPositionToChild(this.m_chestShop);
		}
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x001056DD File Offset: 0x00103ADD
	public void ScrollToDirtBikeBundle()
	{
		if (this.m_nonConsumableButtons != null && this.m_nonConsumableButtons.ContainsKey("dirt_bike_bundle"))
		{
			this.m_scrollArea.SetScrollPositionToChild(this.m_nonConsumableButtons["dirt_bike_bundle"]);
		}
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x0010571C File Offset: 0x00103B1C
	private UIVerticalList CreateCategory(out UIComponent _holder, UIComponent _parent, string _icon, string _title, string _description, string _timerText)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.RemoveDrawHandler();
		_holder = uiverticalList;
		UIComponent uicomponent = new UIComponent(uiverticalList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicomponent.SetHeight(0.08f, RelativeTo.ScreenHeight);
		uicomponent.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicomponent, string.Empty);
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.SetMargins(0f, 0.03f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.007f, RelativeTo.ScreenHeight);
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TopLabelBackground));
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(_icon, null);
		float num = frame.width / frame.height;
		float num2 = 0.08f;
		UIComponent uicomponent2 = new UIComponent(uihorizontalList, false, string.Empty, null, null, string.Empty);
		uicomponent2.SetHeight(num2, RelativeTo.ScreenHeight);
		uicomponent2.SetWidth(num * num2, RelativeTo.ScreenHeight);
		uicomponent2.SetMargins(0.006f, -0.006f, -0.01f, 0.01f, RelativeTo.ScreenHeight);
		uicomponent2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicomponent2, false, string.Empty, PsState.m_uiSheet, frame, true, true);
		uifittedSprite.SetHorizontalAlign(0f);
		UIVerticalList uiverticalList2 = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList2.SetMargins(0.015f, 0.015f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList2, false, string.Empty, _title, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, "#FED742", null);
		uitext.SetHorizontalAlign(0f);
		if (!string.IsNullOrEmpty(_timerText))
		{
			string text = _timerText + " <color=#79DF15>" + PsMetagameManager.GetTimeStringFromSeconds(PsMetagameManager.m_daySecondsLeft) + "</color>";
			UIText uitext2 = new UIText(uiverticalList2, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, null, null);
			uitext2.SetHorizontalAlign(0f);
			this.m_timers.Add(uitext2);
		}
		if (!string.IsNullOrEmpty(_description))
		{
			UIText uitext3 = new UIText(uihorizontalList, false, string.Empty, _description, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, null, null);
			uitext3.SetHorizontalAlign(0f);
		}
		UIVerticalList uiverticalList3 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList3.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uiverticalList3.SetMargins(0.03f, 0.03f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		uiverticalList3.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList3.SetDrawHandler(new UIDrawDelegate(PsUICenterShopAll.ItemListDrawHandler));
		return uiverticalList3;
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x001059E8 File Offset: 0x00103DE8
	private UIRectSpriteButton CreatePurchaseItemButton(UIComponent _parent, string _title, string _price, PsCurrency _currency)
	{
		UIRectSpriteButton uirectSpriteButton = new UIRectSpriteButton(_parent, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null), true, false);
		uirectSpriteButton.m_TAC.m_letTouchesThrough = true;
		uirectSpriteButton.SetWidth(0.31f, RelativeTo.ParentWidth);
		uirectSpriteButton.SetHeight(uirectSpriteButton.m_frame.height / uirectSpriteButton.m_frame.width * 0.31f, RelativeTo.ParentWidth);
		uirectSpriteButton.SetMargins(0.08f, 0.08f, 0.06f, 0.04f, RelativeTo.OwnWidth);
		UICanvas uicanvas = new UICanvas(uirectSpriteButton, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.13f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetVerticalAlign(1f);
		UITextbox uitextbox = new UITextbox(uicanvas, false, string.Empty, _title, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.12f, RelativeTo.ParentWidth, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetMaxRows(2);
		uitextbox.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		uitextbox.UseDotsWhenWrapping(true);
		if (_currency == PsCurrency.Real)
		{
			UICanvas uicanvas2 = new UICanvas(uirectSpriteButton, false, string.Empty, null, string.Empty);
			uicanvas2.SetSize(0.6f, 0.15f, RelativeTo.ParentHeight);
			uicanvas2.SetVerticalAlign(0f);
			uicanvas2.SetMargins(0.1f, RelativeTo.OwnHeight);
			uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.CurrencyLabelBackground));
			UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, _price, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#000000", null);
		}
		else
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(uirectSpriteButton, string.Empty);
			uihorizontalList.SetVerticalAlign(0f);
			uihorizontalList.SetSpacing(0.05f, RelativeTo.ParentWidth);
			uihorizontalList.SetHeight(0.16f, RelativeTo.ParentHeight);
			uihorizontalList.RemoveDrawHandler();
			if (_currency != PsCurrency.None)
			{
				string text = string.Empty;
				if (_currency != PsCurrency.Coin)
				{
					if (_currency == PsCurrency.Gem)
					{
						text = "menu_resources_diamond_icon";
					}
				}
				else
				{
					text = "menu_resources_coin_icon";
				}
				UIText uitext = new UIText(uihorizontalList, false, string.Empty, _price, PsFontManager.GetFont(PsFonts.HurmeBold), 0.95f, RelativeTo.ParentHeight, "#FFFFFF", "#000000");
				uitext.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
				UISprite uisprite = new UISprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true);
				uisprite.SetSize(uisprite.m_frame.width / uisprite.m_frame.height, 1f, RelativeTo.ParentHeight);
			}
			else
			{
				UIFittedText uifittedText2 = new UIFittedText(uihorizontalList, false, string.Empty, _price, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", "#000000");
				uifittedText2.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
			}
		}
		return uirectSpriteButton;
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x00105CC8 File Offset: 0x001040C8
	private void CreateDiscountSticker(UIComponent _parent, string _text)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		float num = 0.2f;
		float num2 = 0.6f;
		float num3 = 1.12f;
		uicanvas.SetMargins(-num3 - num, num3 - num, -num2 - num, num2 - num, RelativeTo.ParentWidth);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_advert_badge", null), true, true);
		string text = "#ff3b00";
		string text2 = "#ffffff";
		uifittedSprite.SetColor(DebugDraw.HexToColor(text));
		uifittedSprite.SetMargins(0.08f, RelativeTo.OwnHeight);
		UIFittedText uifittedText = new UIFittedText(uifittedSprite, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.HurmeBold), true, text2, null);
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x00105D84 File Offset: 0x00104184
	private void CreateSticker(string _stickerIdentifier, UIComponent _parent)
	{
		string text = string.Empty;
		string text2 = _stickerIdentifier.ToLower();
		if (text2 != null)
		{
			string text3;
			string text4;
			if (!(text2 == "bestvalue"))
			{
				if (!(text2 == "mostpopular"))
				{
					return;
				}
				text = PsStrings.Get(StringID.SHOP_MOST_POPULAR).ToUpper();
				text3 = "#014666";
				text4 = "#69efff";
			}
			else
			{
				text = PsStrings.Get(StringID.SHOP_BEST_VALUE).ToUpper();
				text3 = "#066601";
				text4 = "#85ff31";
			}
			UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			uicanvas.SetMargins(0.51f, -0.06f, 0.15f, 0.3f, RelativeTo.OwnHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_advert_badge", null), true, true);
			uifittedSprite.SetColor(DebugDraw.HexToColor(text4));
			uifittedSprite.SetMargins(0.16f, RelativeTo.OwnHeight);
			UIFittedText uifittedText = new UIFittedText(uifittedSprite, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), true, text3, null);
			return;
		}
	}

	// Token: 0x0600182F RID: 6191 RVA: 0x00105EB0 File Offset: 0x001042B0
	private void CreateDirtbikeBundleBanner(UIComponent _parent, string _productIdentifier = "dirt_bike_bundle")
	{
		IAPProduct iapproductById = PsIAPManager.GetIAPProductById(_productIdentifier);
		if (iapproductById != null)
		{
			this.m_nonConsumableButtons[_productIdentifier] = new PsUINonconsumableBanner(_parent, _productIdentifier, iapproductById.price);
			UIVerticalList uiverticalList = new UIVerticalList(this.m_nonConsumableButtons[_productIdentifier].m_sprite, string.Empty);
			uiverticalList.RemoveDrawHandler();
			uiverticalList.SetSpacing(-0.05f, RelativeTo.ParentHeight);
			uiverticalList.SetVerticalAlign(0.03f);
			UIText uitext = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.SHOP_BUNDLE).ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.21f, RelativeTo.ParentHeight, "#6f2301", null);
			if (!PsMetagameManager.m_playerStats.dirtBikeBundle)
			{
				new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.SHOP_TAP_INFO), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), 0.13f, RelativeTo.ParentHeight, "#FFFFFF", "#000000");
			}
		}
	}

	// Token: 0x06001830 RID: 6192 RVA: 0x00105F88 File Offset: 0x00104388
	private void CreateCoinDoublerBanner(UIComponent _parent, string _productIdentifier = "coin_booster")
	{
		IAPProduct iapproductById = PsIAPManager.GetIAPProductById(_productIdentifier);
		if (iapproductById != null)
		{
			this.m_nonConsumableButtons[_productIdentifier] = new PsUINonconsumableBanner(_parent, _productIdentifier, iapproductById.price);
		}
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x00105FBC File Offset: 0x001043BC
	private void CreateSpecialOfferBanners(UIComponent _parent, string _productIdentifier)
	{
		this.m_specialOfferBanners = new List<PsUITimedSpecialOfferBanner>();
		List<PsTimedSpecialOffer> startedTimedSpecialOffers = PsMetagameManager.GetStartedTimedSpecialOffers();
		if (startedTimedSpecialOffers != null && startedTimedSpecialOffers.Count > 0)
		{
			for (int i = 0; i < startedTimedSpecialOffers.Count; i++)
			{
				PsUITimedSpecialOfferBanner psUITimedSpecialOfferBanner = new PsUITimedSpecialOfferBanner(_parent, startedTimedSpecialOffers[i], new Action(this.DestroySpecialOfferCategory), true);
				this.m_specialOfferBanners.Add(psUITimedSpecialOfferBanner);
			}
		}
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x0010602A File Offset: 0x0010442A
	private void DestroySpecialOfferCategory()
	{
		this.m_offerShop.Destroy();
		this.m_offerShop = null;
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x00106040 File Offset: 0x00104440
	public override void Destroy()
	{
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
		}
		this.m_popup = null;
		PsUICenterShopAll.m_scrollIndex = 0;
		PsUICenterShopAll.m_shopState--;
		if (this.m_lastResourceView != null && PsMetagameManager.m_menuResourceView != null)
		{
			PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastResourceView);
		}
		PsMetagameManager.d_shopUpdateAction = null;
		base.Destroy();
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x001060B0 File Offset: 0x001044B0
	public static void ItemListDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		int num = 60;
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_category_bg", null);
		Frame frame2 = new Frame(frame.x, frame.y, frame.width, (float)num);
		Frame frame3 = new Frame(frame.x, frame.y + (float)num, frame.width, frame.height - (float)num * 2f);
		Frame frame4 = new Frame(frame.x, frame.y + frame.height - (float)num, frame.width, (float)num);
		float num2 = _c.m_actualHeight * ((float)num / _c.m_actualHeight);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth, num2);
		SpriteS.SetOffset(spriteC, Vector3.up * ((_c.m_actualHeight - num2) / 2f), 0f);
		float num3 = _c.m_actualHeight * ((float)num / _c.m_actualHeight);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame4, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, _c.m_actualWidth, num3);
		SpriteS.SetOffset(spriteC2, Vector3.down * ((_c.m_actualHeight - num3) / 2f), 0f);
		float num4 = _c.m_actualHeight - num2 - num3;
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC3, _c.m_actualWidth, num4);
		SpriteS.SetOffset(spriteC3, new Vector3(0f, 0f, 0f), 0f);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x00106268 File Offset: 0x00104668
	public static void ItemHolderDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 0f), 0f);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x001062F3 File Offset: 0x001046F3
	public void UpdateUpgradeCards()
	{
		this.CreateUpgradeCards();
		this.m_cards.Update();
		this.m_contentArea.ArrangeContents();
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x00106314 File Offset: 0x00104714
	public void CreateUpgradeCards()
	{
		this.m_cards.DestroyChildren();
		this.m_timers = new List<UIText>();
		List<string> list = new List<string>();
		List<Type> list2 = new List<Type>();
		list.Add("offroader");
		list2.Add(typeof(OffroadCar));
		if (PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)))
		{
			list.Add("dirtbike");
			list2.Add(typeof(Motorcycle));
		}
		for (int i = 0; i < list.Count; i++)
		{
			UIComponent uicomponent;
			UIVerticalList uiverticalList = this.CreateCategory(out uicomponent, this.m_cards, "menu_vehicle_logo_" + list[i], PsStrings.Get(StringID.SHOP_HEADER_UPGRADES), null, PsStrings.Get(StringID.SHOP_CARDS_APPEAR));
			UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
			uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			for (int j = i * 3; j < 3 * (i + 1); j++)
			{
				int resourcesNeededToMaxLevel = PsUpgradeManager.GetUpgradeItem(list2[i], PsMetagameManager.m_shopUpgradeItems[j].m_identifier).m_resourcesNeededToMaxLevel;
				int upgradeResourceCount = PsUpgradeManager.GetUpgradeResourceCount(PsMetagameManager.m_shopUpgradeItems[j].m_identifier);
				string text = string.Empty;
				PsUpgradeItem psUpgradeItem;
				if (PsMetagameManager.m_shopUpgradeItems[j].m_identifier.StartsWith("Car"))
				{
					psUpgradeItem = PsUpgradeManager.GetUpgradeItem(typeof(OffroadCar), PsMetagameManager.m_shopUpgradeItems[j].m_identifier);
				}
				else
				{
					psUpgradeItem = PsUpgradeManager.GetUpgradeItem(typeof(Motorcycle), PsMetagameManager.m_shopUpgradeItems[j].m_identifier);
				}
				if (psUpgradeItem != null)
				{
					text = PsStrings.Get(psUpgradeItem.m_title).ToUpper();
				}
				UIRectSpriteButton uirectSpriteButton;
				if (resourcesNeededToMaxLevel > upgradeResourceCount)
				{
					int priceForUpgrade = PsUpgradeManager.GetPriceForUpgrade(PsMetagameManager.m_shopUpgradeItems[j]);
					if (priceForUpgrade > -1)
					{
						uirectSpriteButton = this.CreatePurchaseItemButton(uihorizontalList, text, priceForUpgrade.ToString(), PsCurrency.Coin);
						uirectSpriteButton.m_identifier = PsMetagameManager.m_shopUpgradeItems[j].m_identifier;
						this.m_upgradeButtons.Add(uirectSpriteButton);
					}
					else
					{
						uirectSpriteButton = this.CreatePurchaseItemButton(uihorizontalList, text, PsStrings.Get(StringID.BUTTON_OUT_OF_STOCK).ToUpper(), PsCurrency.None);
					}
				}
				else
				{
					uirectSpriteButton = this.CreatePurchaseItemButton(uihorizontalList, text, PsStrings.Get(StringID.SHOP_PROMPT_MAXED_OUT), PsCurrency.None);
				}
				UICanvas uicanvas = new UICanvas(uirectSpriteButton, false, "cardHolder", null, string.Empty);
				uicanvas.SetWidth(0.34f, RelativeTo.ParentWidth);
				uicanvas.SetHeight(0.52f, RelativeTo.ParentHeight);
				uicanvas.SetVerticalAlign(0.65f);
				uicanvas.RemoveDrawHandler();
				PsUIUpgradeView.UpgradeItemButton upgradeItemButton = new PsUIUpgradeView.UpgradeItemButton(uicanvas, psUpgradeItem, true, null, true, "cardfront");
				upgradeItemButton.RemoveTouchAreas();
				EventGiftTimedUpgradeItemCardDiscount activeEventGift = PsMetagameManager.GetActiveEventGift<EventGiftTimedUpgradeItemCardDiscount>();
				if (activeEventGift != null)
				{
					string discountTag = activeEventGift.GetDiscountTag();
					this.CreateDiscountSticker(uicanvas, discountTag);
				}
			}
		}
		PsMetagameManager.d_shopUpdateAction = new Action(this.UpdateUpgradeCards);
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x00106610 File Offset: 0x00104A10
	public static PsUIUpgradeView.UpgradeItemButton CreateUpgradeItemCard(UIComponent _parent, string _identifier)
	{
		PsUpgradeItem psUpgradeItem;
		if (_identifier.StartsWith("Car"))
		{
			psUpgradeItem = PsUpgradeManager.GetUpgradeItem(typeof(OffroadCar), _identifier);
		}
		else
		{
			psUpgradeItem = PsUpgradeManager.GetUpgradeItem(typeof(Motorcycle), _identifier);
		}
		PsUIUpgradeView.UpgradeItemButton upgradeItemButton = new PsUIUpgradeView.UpgradeItemButton(_parent, psUpgradeItem, true, null, true, "cardfront");
		upgradeItemButton.RemoveTouchAreas();
		return upgradeItemButton;
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x00106670 File Offset: 0x00104A70
	public static void CreateChest(UIComponent _parent, GachaType _type)
	{
		UI3DRenderTextureCanvas ui3DRenderTextureCanvas = new UI3DRenderTextureCanvas(_parent, string.Empty, null, false);
		ui3DRenderTextureCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		ui3DRenderTextureCanvas.SetHeight(0.8f, RelativeTo.ParentHeight);
		ui3DRenderTextureCanvas.SetVerticalAlign(1f);
		ui3DRenderTextureCanvas.m_3DCamera.fieldOfView = 22f;
		ui3DRenderTextureCanvas.m_3DCameraPivot.transform.Rotate(0f, -20f, 0f, 0);
		ui3DRenderTextureCanvas.m_3DCameraPivot.transform.Rotate(16f, 0f, 0f, 1);
		ui3DRenderTextureCanvas.m_3DCameraOffset = -6f;
		ui3DRenderTextureCanvas.m_3DCamera.nearClipPlane = 1f;
		ui3DRenderTextureCanvas.m_3DCamera.farClipPlane = 500f;
		ui3DRenderTextureCanvas.RemoveTouchAreas();
		CameraS.MoveToFront(ui3DRenderTextureCanvas.m_3DCamera);
		Texture texture = null;
		ResourcePool.Asset asset = RESOURCE.RewardChest_GameObject;
		switch (_type)
		{
		case GachaType.WOOD:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureWood_Texture2D);
			break;
		case GachaType.COMMON:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureWood_Texture2D);
			break;
		case GachaType.BRONZE:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureBronze_Texture2D);
			break;
		case GachaType.SILVER:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureSilver_Texture2D);
			break;
		case GachaType.GOLD:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureGold_Texture2D);
			break;
		case GachaType.RARE:
			asset = RESOURCE.ShopRewardChestT1_GameObject;
			break;
		case GachaType.EPIC:
			asset = RESOURCE.ShopRewardChestT2_GameObject;
			break;
		case GachaType.SUPER:
			asset = RESOURCE.ShopRewardChestT3_GameObject;
			break;
		case GachaType.BOSS:
			asset = RESOURCE.ShopRewardChestBoss_GameObject;
			break;
		}
		PrefabC prefabC = ui3DRenderTextureCanvas.AddGameObject(ResourceManager.GetGameObject(asset), new Vector3(0f, -0.72f, 0f), new Vector3(0f, 180f, 0f));
		if (texture != null)
		{
			Renderer[] componentsInChildren = prefabC.p_gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].material.name.StartsWith("RewardChest"))
				{
					componentsInChildren[i].material.mainTexture = texture;
				}
			}
		}
		VisualsRewardChest component = prefabC.p_gameObject.GetComponent<VisualsRewardChest>();
		component.SetToActiveState();
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x00106898 File Offset: 0x00104C98
	public override void Step()
	{
		if (this.m_timers != null && PsMetagameManager.m_daySecondsLeftUpdated)
		{
			string text = PsStrings.Get(StringID.SHOP_CARDS_APPEAR) + " <color=#79DF15>" + PsMetagameManager.GetTimeStringFromSeconds(PsMetagameManager.m_daySecondsLeft) + "</color>";
			for (int l = 0; l < this.m_timers.Count; l++)
			{
				this.m_timers[l].SetText(text);
			}
		}
		int m;
		for (m = 0; m < this.m_upgradeButtons.Count; m++)
		{
			if (this.m_upgradeButtons[m].m_hit)
			{
				if (this.m_popup != null)
				{
					this.m_popup.Destroy();
				}
				string upgradeIdentifier = this.m_upgradeButtons[m].m_identifier;
				this.m_popup = new PsUIBasePopup(typeof(PsUICenterConfirmPurchaseUpgrade), null, null, null, false, true, InitialPage.Center, false, false, false);
				ShopUpgradeItemData shopUpgradeItemData = PsMetagameManager.m_shopUpgradeItems.Find((ShopUpgradeItemData info) => info.m_identifier == upgradeIdentifier);
				(this.m_popup.m_mainContent as PsUICenterConfirmPurchase).SetInfo(shopUpgradeItemData, upgradeIdentifier);
				this.m_popup.SetAction("Confirm", delegate
				{
					PsPurchaseHelper.PurchaseUpgrade(this.m_upgradeButtons[m].m_identifier, delegate
					{
						ShopUpgradeItemData shopUpgradeItemData2 = PsMetagameManager.m_shopUpgradeItems.Find((ShopUpgradeItemData info) => info.m_identifier == upgradeIdentifier);
						this.CreateUpgradeCards();
						this.m_contentArea.Update();
						PsUICenterConfirmPurchase psUICenterConfirmPurchase = this.m_popup.m_mainContent as PsUICenterConfirmPurchase;
						if (psUICenterConfirmPurchase != null)
						{
							psUICenterConfirmPurchase.SetPrice(PsUpgradeManager.GetPriceForUpgrade(shopUpgradeItemData2));
							psUICenterConfirmPurchase.UpdateUpgradeCard();
						}
					});
				});
				this.m_popup.SetAction("Exit", delegate
				{
					this.m_popup.Destroy();
					this.m_popup = null;
				});
				TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				base.Step();
				return;
			}
		}
		int i;
		for (i = 0; i < this.m_chestButtons.Count; i++)
		{
			if (this.m_chestButtons[i].m_hit)
			{
				if (this.m_popup != null)
				{
					this.m_popup.Destroy();
				}
				this.m_popup = new PsUIBasePopup(typeof(PsUICenterConfirmPurchaseChest), null, null, null, false, true, InitialPage.Center, false, false, false);
				(this.m_popup.m_mainContent as PsUICenterConfirmPurchaseChest).SetInfo(this.m_chestButtons[i].m_identifier);
				this.m_popup.SetAction("Confirm", delegate
				{
					this.m_popup.Destroy();
					this.m_popup = null;
					PsPurchaseHelper.PurchaseChest(this.m_chestButtons[i].m_identifier, delegate
					{
						this.CreateUpgradeCards();
						this.m_contentArea.Update();
					});
				});
				this.m_popup.SetAction("Exit", delegate
				{
					this.m_popup.Destroy();
					this.m_popup = null;
				});
				TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				base.Step();
				return;
			}
		}
		for (int j = 0; j < this.m_coinButtons.Count; j++)
		{
			if (this.m_coinButtons[j].m_hit)
			{
				if (this.m_popup != null)
				{
					this.m_popup.Destroy();
				}
				this.m_popup = new PsUIBasePopup(typeof(PsUICenterConfirmPurchase), null, null, null, false, true, InitialPage.Center, false, false, false);
				int index = int.Parse(this.m_coinButtons[j].m_identifier);
				(this.m_popup.m_mainContent as PsUICenterConfirmPurchase).SetInfo(index);
				this.m_popup.SetAction("Confirm", delegate
				{
					this.m_popup.Destroy();
					this.m_popup = null;
					PsPurchaseHelper.PurchaseCoins(PsUICenterShopAll.GetCoinAmoumt(index));
				});
				this.m_popup.SetAction("Exit", delegate
				{
					this.m_popup.Destroy();
					this.m_popup = null;
				});
				TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				base.Step();
				return;
			}
		}
		for (int k = 0; k < this.m_gemButtons.Count; k++)
		{
			if (this.m_gemButtons[k].m_hit)
			{
				PsPurchaseHelper.PurchaseIAP(this.m_gemButtons[k].m_identifier, null, null);
				base.Step();
				return;
			}
		}
		foreach (KeyValuePair<string, PsUINonconsumableBanner> keyValuePair in this.m_nonConsumableButtons)
		{
			PsUINonconsumableBanner banner = keyValuePair.Value;
			if (banner.IsHit())
			{
				string identifier = banner.m_identifier;
				if (identifier.Equals("dirt_bike_bundle"))
				{
					if (this.m_popup != null)
					{
						this.m_popup.Destroy();
					}
					this.m_popup = new PsUIBasePopup(typeof(PsDirtbikeBundlePopup), null, null, null, false, true, InitialPage.Center, false, false, false);
					IAPProduct iapproductById = PsIAPManager.GetIAPProductById(identifier);
					string text2 = ((iapproductById != null) ? iapproductById.price : "NaN");
					(this.m_popup.m_mainContent as PsDirtbikeBundlePopup).SetPrice(text2);
					(this.m_popup.m_mainContent as PsDirtbikeBundlePopup).CreateContent();
					this.m_popup.SetAction("Purchased", delegate
					{
						this.m_popup.Destroy();
						this.m_popup = null;
						PsPurchaseHelper.PurchaseIAP(identifier, null, new Action(banner.Purchased));
					});
					this.m_popup.SetAction("Exit", delegate
					{
						this.m_popup.Destroy();
						this.m_popup = null;
					});
					TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				}
				else
				{
					PsPurchaseHelper.PurchaseIAP(identifier, null, new Action(banner.Purchased));
				}
			}
		}
		base.Step();
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x00106F20 File Offset: 0x00105320
	public void GetMoreGems()
	{
		this.ScrollToGemShop();
		this.m_scrollArea.Step();
	}

	// Token: 0x04001AE6 RID: 6886
	public static int m_shopState = 0;

	// Token: 0x04001AE7 RID: 6887
	public const int GEMS_SCROLL_INDEX = 1;

	// Token: 0x04001AE8 RID: 6888
	public const int COINS_SCROLL_INDEX = 2;

	// Token: 0x04001AE9 RID: 6889
	public const int CHESTS_SCROLL_INDEX = 3;

	// Token: 0x04001AEA RID: 6890
	public const int DIRTBIKE_BUNDLE_SCROLL_INDEX = 4;

	// Token: 0x04001AEB RID: 6891
	protected UIScrollableCanvas m_scrollArea;

	// Token: 0x04001AEC RID: 6892
	protected UIVerticalList m_contentArea;

	// Token: 0x04001AED RID: 6893
	private UIVerticalList m_cards;

	// Token: 0x04001AEE RID: 6894
	private List<UIRectSpriteButton> m_upgradeButtons;

	// Token: 0x04001AEF RID: 6895
	private List<UIRectSpriteButton> m_chestButtons;

	// Token: 0x04001AF0 RID: 6896
	private List<UIRectSpriteButton> m_coinButtons;

	// Token: 0x04001AF1 RID: 6897
	private List<UIRectSpriteButton> m_gemButtons;

	// Token: 0x04001AF2 RID: 6898
	private Dictionary<string, PsUINonconsumableBanner> m_nonConsumableButtons;

	// Token: 0x04001AF3 RID: 6899
	public List<UIText> m_timers;

	// Token: 0x04001AF4 RID: 6900
	public UIComponent m_gemShop;

	// Token: 0x04001AF5 RID: 6901
	public UIComponent m_coinShop;

	// Token: 0x04001AF6 RID: 6902
	public UIComponent m_offerShop;

	// Token: 0x04001AF7 RID: 6903
	public UIComponent m_chestShop;

	// Token: 0x04001AF8 RID: 6904
	private PsUIBasePopup m_popup;

	// Token: 0x04001AF9 RID: 6905
	public static PsUICenterShopAll.CoinItem[] m_coinPrices = new PsUICenterShopAll.CoinItem[]
	{
		new PsUICenterShopAll.CoinItem(StringID.SHOP_COINS_JAR, 1000, "menu_shop_item_coins_tier1"),
		new PsUICenterShopAll.CoinItem(StringID.SHOP_COINS_BARREL, 10000, "menu_shop_item_coins_tier3"),
		new PsUICenterShopAll.CoinItem(StringID.SHOP_COINS_VAULT, 100000, "menu_shop_item_coins_tier5")
	};

	// Token: 0x04001AFA RID: 6906
	private LastResourceView m_lastResourceView;

	// Token: 0x04001AFB RID: 6907
	public static int m_scrollIndex = 0;

	// Token: 0x04001AFC RID: 6908
	private List<PsUITimedSpecialOfferBanner> m_specialOfferBanners;

	// Token: 0x0200033B RID: 827
	public class CoinItem
	{
		// Token: 0x06001841 RID: 6209 RVA: 0x00106FCA File Offset: 0x001053CA
		public CoinItem(StringID _nameID, int _amount, string _iconName)
		{
			this.m_nameID = _nameID;
			this.m_amount = _amount;
			this.m_iconName = _iconName;
		}

		// Token: 0x04001B04 RID: 6916
		public StringID m_nameID;

		// Token: 0x04001B05 RID: 6917
		public int m_amount;

		// Token: 0x04001B06 RID: 6918
		public string m_iconName;
	}
}
