using System;
using System.Collections.Generic;
using System.Linq;
using InAppPurchases;
using Prime31;
using UnityEngine;

// Token: 0x02000342 RID: 834
public class PsUITimedSpecialOfferBanner : UICanvas
{
	// Token: 0x06001875 RID: 6261 RVA: 0x00109AA8 File Offset: 0x00107EA8
	public PsUITimedSpecialOfferBanner(UIComponent _parent, PsTimedSpecialOffer _specialOffer, Action _purchasedCallback, bool _asPopup)
		: base(_parent, true, string.Empty, null, string.Empty)
	{
		this.m_specialOffer = _specialOffer;
		this.m_purchasedCallback = _purchasedCallback;
		this.m_purchasedCallback = (Action)Delegate.Combine(this.m_purchasedCallback, new Action(this.OpenChests));
		this.m_asPopup = _asPopup;
		this.m_TAC.m_letTouchesThrough = true;
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SpecialOfferBackground));
		this.SetSize(1f, 0.58f, RelativeTo.ParentWidth);
		this.CreateContent();
		if (_asPopup)
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_parent, string.Empty);
			uihorizontalList.SetSpacing(0.02f, RelativeTo.ParentWidth);
			uihorizontalList.RemoveDrawHandler();
			string text = PsStrings.Get(StringID.SPECIAL_OFFER_ENDS);
			text = text.Replace("%1", string.Empty);
			UIText uitext = new UIText(uihorizontalList, false, string.Empty, text + ":", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ParentWidth, "#FED037", null);
			this.m_timerSeconds = Mathf.CeilToInt((float)this.m_specialOffer.m_timeLeft);
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timerSeconds, true, true);
			this.m_timer = new UIText(uihorizontalList, false, string.Empty, timeStringFromSeconds, PsFontManager.GetFont(PsFonts.KGSecondChancesMN), 0.06f, RelativeTo.ParentWidth, "#FED037", null);
		}
	}

	// Token: 0x06001876 RID: 6262 RVA: 0x00109C00 File Offset: 0x00108000
	private void CreateContent()
	{
		ServerProduct serverProductById = PsIAPManager.GetServerProductById(this.m_specialOffer.m_productId);
		IAPProduct iapproductById = PsIAPManager.GetIAPProductById(this.m_specialOffer.m_productId);
		PsIAPManager.ResourceBundle resourceBundle = default(PsIAPManager.ResourceBundle);
		if (serverProductById != null)
		{
			resourceBundle = PsIAPManager.ParseResourceBundle(serverProductById.bundle);
		}
		ServerProduct serverProduct = null;
		int num = 1;
		List<ServerProduct> list = PsIAPManager.GetProducts();
		if (list != null)
		{
			list = Enumerable.ToList<ServerProduct>(Enumerable.OrderBy<ServerProduct, int>(list.FindAll((ServerProduct product) => product.resource == "gems" && product.visible), (ServerProduct product) => product.amount));
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].amount == resourceBundle.gems)
				{
					serverProduct = list[i];
					num = Mathf.Clamp(i + 1, 1, 6);
				}
			}
		}
		if (serverProduct == null)
		{
			serverProduct = Manager.GetServerProductByResourceAmount("gems", resourceBundle.gems);
		}
		IAPProduct iapproduct = null;
		if (serverProduct != null)
		{
			iapproduct = PsIAPManager.GetIAPProductById(serverProduct.identifier);
		}
		if (iapproductById == null || iapproduct == null)
		{
			if (this.m_loadingAnimation == null)
			{
				this.m_loadingAnimation = new PsUILoadingAnimation(this, false);
			}
			return;
		}
		if (this.m_loadingAnimation != null)
		{
			this.m_loadingAnimation.Destroy();
			this.m_loadingAnimation = null;
		}
		GachaType gachaType = GachaType.RARE;
		if (resourceBundle.chests != null && resourceBundle.chests.Count > 0)
		{
			gachaType = resourceBundle.chests[0];
		}
		int shopPrice = PsGachaManager.GetShopPrice(gachaType);
		int num2 = PsMetagameManager.CoinsToDiamonds(resourceBundle.coins, false);
		if (this.m_asPopup)
		{
			PsMetrics.NewSpecialOfferShown(this.m_specialOffer.m_startTime, gachaType.ToString(), resourceBundle.coins, resourceBundle.gems, iapproductById.price);
		}
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.SetHeight(0.16f, RelativeTo.ParentHeight);
		uicanvas.SetMargins(1.5f, 1.5f, 0.11f, 0.13f, RelativeTo.OwnHeight);
		uicanvas.RemoveDrawHandler();
		if (!string.IsNullOrEmpty(iapproductById.title))
		{
			new UIFittedText(uicanvas, false, string.Empty, iapproductById.title.ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FC1368", null);
		}
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ParentWidth);
		uihorizontalList.SetVerticalAlign(0.58f);
		uihorizontalList.RemoveDrawHandler();
		string gachaNameWithChest = PsGachaManager.GetGachaNameWithChest(gachaType);
		string text = PsStrings.Get(StringID.LEVEL).ToUpper() + " " + (PsMetagameManager.m_playerStats.gachaLevel + 1);
		string text2 = string.Format("{0:n0}", shopPrice).Replace(",", " ");
		new PsUITimedSpecialOfferBanner.Item(uihorizontalList, gachaNameWithChest, "menu_shop_item_bluebox", text, text2, PsCurrency.Gem, PsGachaManager.GetChestIconName(gachaType));
		new UIText(uihorizontalList, false, string.Empty, "+", PsFontManager.GetFont(PsFonts.HurmeBold), 0.08f, RelativeTo.ScreenHeight, null, null);
		string title = iapproduct.title;
		string text3 = string.Format("{0:n0}", serverProduct.amount).Replace(",", " ");
		string price = iapproduct.price;
		new PsUITimedSpecialOfferBanner.Item(uihorizontalList, title, "menu_shop_item_gems_tier" + num, text3, price, PsCurrency.Real, string.Empty);
		new UIText(uihorizontalList, false, string.Empty, "+", PsFontManager.GetFont(PsFonts.HurmeBold), 0.08f, RelativeTo.ScreenHeight, null, null);
		int num3 = 0;
		if (resourceBundle.coins > 95000)
		{
			num3 = 2;
		}
		else if (resourceBundle.coins > 9500)
		{
			num3 = 1;
		}
		string text4 = PsStrings.Get(PsUICenterShopAll.m_coinPrices[num3].m_nameID);
		string text5 = string.Format("{0:n0}", resourceBundle.coins).Replace(",", " ");
		string text6 = string.Format("{0:n0}", num2).Replace(",", " ");
		new PsUITimedSpecialOfferBanner.Item(uihorizontalList, text4, PsUICenterShopAll.m_coinPrices[num3].m_iconName, text5, text6, PsCurrency.Gem, string.Empty);
		UICanvas uicanvas2 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas2.SetVerticalAlign(0f);
		uicanvas2.SetHeight(0.16f, RelativeTo.ParentHeight);
		uicanvas2.SetMargins(1.5f, 1.5f, 0.11f, 0.11f, RelativeTo.OwnHeight);
		uicanvas2.RemoveDrawHandler();
		new UIFittedText(uicanvas2, false, string.Empty, iapproductById.price, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#ffffff", null);
		float num4 = (float)ClientTools.GetPriceFromString(iapproductById.price);
		float num5 = (float)ClientTools.GetPriceFromString(iapproduct.price);
		int specialOfferValue = PsMetagameManager.GetSpecialOfferValue(num4, serverProduct.amount, num5, num2, shopPrice, 0.75f);
		if (specialOfferValue >= 2)
		{
			this.CreateValueSticker(this, specialOfferValue);
		}
		this.m_contentHasCreated = true;
	}

	// Token: 0x06001877 RID: 6263 RVA: 0x0010A11C File Offset: 0x0010851C
	private void CreateValueSticker(UIComponent _parent, int _value)
	{
		string text = PsStrings.Get(StringID.SPECIAL_OFFER_VALUE);
		text = text.Replace("%1 ", string.Empty);
		string text2 = text;
		string text3 = "#085B02";
		string text4 = "#6DE327";
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetMargins(0.21f, -0.21f, -0.22f, 0.22f, RelativeTo.OwnHeight);
		uicanvas.SetAlign(1f, 1f);
		uicanvas.SetSize(0.29f, 0.29f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_advert_badge", null), true, true);
		uifittedSprite.SetColor(DebugDraw.HexToColor(text4));
		uifittedSprite.SetMargins(0.16f, RelativeTo.OwnHeight);
		UIVerticalList uiverticalList = new UIVerticalList(uifittedSprite, string.Empty);
		uiverticalList.SetMargins(0f, RelativeTo.ParentHeight);
		uiverticalList.SetSpacing(-0.1f, RelativeTo.ParentHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, _value + "x", PsFontManager.GetFont(PsFonts.HurmeBold), 0.58f, RelativeTo.ParentHeight, text3, null);
		UICanvas uicanvas2 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.5f, RelativeTo.ParentHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, text2.ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), true, text3, null);
		this.m_stickerTween = TweenS.AddTransformTween(uifittedSprite.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, -15f), 2f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_stickerTween, new TweenEventDelegate(this.TweenLeft));
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x0010A2E4 File Offset: 0x001086E4
	private void TweenLeft(TweenC _tweenC)
	{
		TransformC p_TC = _tweenC.p_TC;
		if (this.m_stickerTween != null)
		{
			TweenS.RemoveComponent(this.m_stickerTween);
		}
		this.m_stickerTween = TweenS.AddTransformTween(p_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, 359f), 2f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_stickerTween, new TweenEventDelegate(this.TweenRight));
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x0010A354 File Offset: 0x00108754
	private void TweenRight(TweenC _tweenC)
	{
		TransformC p_TC = _tweenC.p_TC;
		if (this.m_stickerTween != null)
		{
			TweenS.RemoveComponent(this.m_stickerTween);
		}
		this.m_stickerTween = TweenS.AddTransformTween(p_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, 345f), 2f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_stickerTween, new TweenEventDelegate(this.TweenLeft));
	}

	// Token: 0x0600187A RID: 6266 RVA: 0x0010A3C4 File Offset: 0x001087C4
	public override void Step()
	{
		if (!this.m_contentHasCreated)
		{
			if (this.m_refreshTimer <= 0)
			{
				this.CreateContent();
				if (this.m_contentHasCreated)
				{
					this.Update();
				}
				this.m_refreshTimer = 60;
			}
			else
			{
				this.m_refreshTimer--;
			}
		}
		else if (this.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			PsPurchaseHelper.PurchaseIAP(this.m_specialOffer.m_productId, null, this.m_purchasedCallback);
		}
		int num = Mathf.CeilToInt((float)this.m_specialOffer.m_timeLeft);
		if (this.m_timer != null && num != this.m_timerSeconds)
		{
			this.m_timerSeconds = num;
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timerSeconds, true, true);
			this.m_timer.SetText(timeStringFromSeconds);
			this.m_timer.m_parent.Update();
		}
		base.Step();
	}

	// Token: 0x0600187B RID: 6267 RVA: 0x0010A4A8 File Offset: 0x001088A8
	public void OpenChests()
	{
		PsMetrics.SpecialOfferClaimed(this.m_specialOffer.m_startTime);
		if (PsMetagameManager.m_playerStats.pendingSpecialOfferChests != null && PsMetagameManager.m_playerStats.pendingSpecialOfferChests.Count > 0)
		{
			PsMetagameManager.OpenSpecialOfferChest(PsMetagameManager.m_playerStats.pendingSpecialOfferChests[0]);
			PsMetagameManager.m_playerStats.pendingSpecialOfferChests.RemoveAt(0);
		}
	}

	// Token: 0x0600187C RID: 6268 RVA: 0x0010A50E File Offset: 0x0010890E
	public override void Destroy()
	{
		if (this.m_stickerTween != null)
		{
			TweenS.RemoveComponent(this.m_stickerTween);
			this.m_stickerTween = null;
		}
		base.Destroy();
	}

	// Token: 0x0600187D RID: 6269 RVA: 0x0010A534 File Offset: 0x00108934
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollIn(_touch, _secondary);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.02f, 1.02f, 1f), 0.1f, 0f, false);
	}

	// Token: 0x0600187E RID: 6270 RVA: 0x0010A59C File Offset: 0x0010899C
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.02f, 1.02f, 1f), 0.1f, 0f, false);
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x0010A600 File Offset: 0x00108A00
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollOut(_touch, _secondary);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x0010A668 File Offset: 0x00108A68
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
		if (_inside && this.m_hit)
		{
			SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
		}
	}

	// Token: 0x04001B22 RID: 6946
	public PsTimedSpecialOffer m_specialOffer;

	// Token: 0x04001B23 RID: 6947
	private Action m_purchasedCallback;

	// Token: 0x04001B24 RID: 6948
	private UIText m_timer;

	// Token: 0x04001B25 RID: 6949
	private int m_timerSeconds;

	// Token: 0x04001B26 RID: 6950
	private bool m_contentHasCreated;

	// Token: 0x04001B27 RID: 6951
	private PsUILoadingAnimation m_loadingAnimation;

	// Token: 0x04001B28 RID: 6952
	private bool m_asPopup;

	// Token: 0x04001B29 RID: 6953
	private TweenC m_stickerTween;

	// Token: 0x04001B2A RID: 6954
	private const int REFRESH_INTERVAL = 60;

	// Token: 0x04001B2B RID: 6955
	private int m_refreshTimer;

	// Token: 0x04001B2C RID: 6956
	private TweenC m_touchScaleTween;

	// Token: 0x04001B2D RID: 6957
	private const float TOUCH_SCALE = 1.02f;

	// Token: 0x02000343 RID: 835
	public class Item : UICanvas
	{
		// Token: 0x06001883 RID: 6275 RVA: 0x0010A71C File Offset: 0x00108B1C
		public Item(UIComponent _parent, string _title, string _icon, string _amountLevel, string _price, PsCurrency _currency, string _icon2)
			: base(_parent, false, string.Empty, null, string.Empty)
		{
			this.SetSize(0.336f, 0.574f, RelativeTo.ParentHeight);
			this.RemoveDrawHandler();
			UITextbox uitextbox = new UITextbox(this, false, string.Empty, _title, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.085f, RelativeTo.ParentHeight, false, Align.Center, Align.Middle, "#A54A12", true, null);
			uitextbox.SetShadowShift(new Vector2(1f, -1f), 0.1f);
			uitextbox.SetVerticalAlign(1f);
			uitextbox.SetMaxRows(2);
			uitextbox.SetMinRows(2);
			uitextbox.UseDotsWhenWrapping(true);
			UIFittedSprite uifittedSprite = new UIFittedSprite(this, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_icon, null), true, true);
			uifittedSprite.SetVerticalAlign(0.47f);
			UICanvas uicanvas = new UICanvas(uifittedSprite, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.3f, RelativeTo.ParentHeight);
			uicanvas.SetMargins(0.15f, RelativeTo.OwnHeight);
			uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas.RemoveDrawHandler();
			uicanvas.SetVerticalAlign(1f);
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, _amountLevel, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", "#000000");
			uifittedText.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
			if (!string.IsNullOrEmpty(_icon2))
			{
				UICanvas uicanvas2 = new UICanvas(uifittedSprite, false, string.Empty, null, string.Empty);
				uicanvas2.SetHeight(0.7f, RelativeTo.ParentHeight);
				uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
				uicanvas2.RemoveDrawHandler();
				uicanvas2.SetVerticalAlign(0f);
				UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_icon2, null), true, true);
			}
			if (_currency == PsCurrency.Real)
			{
				UICanvas uicanvas3 = new UICanvas(this, false, string.Empty, null, string.Empty);
				uicanvas3.SetSize(0.6f, 0.15f, RelativeTo.ParentHeight);
				uicanvas3.SetVerticalAlign(0f);
				uicanvas3.SetMargins(0.1f, RelativeTo.OwnHeight);
				uicanvas3.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.CurrencyLabelBackground));
				UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, _price, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#000000", null);
				this.CreateRedLine(uicanvas3);
			}
			else
			{
				UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
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
					UIFittedText uifittedText3 = new UIFittedText(uihorizontalList, false, string.Empty, _price, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", "#000000");
					uifittedText3.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
				}
				this.CreateRedLine(uihorizontalList);
			}
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0010AAC0 File Offset: 0x00108EC0
		private void CreateRedLine(UIComponent _parent)
		{
			UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			uicanvas.SetDrawHandler(new UIDrawDelegate(this.RedDrawhandler));
			uicanvas.SetHeight(0.04f, RelativeTo.ParentWidth);
			uicanvas.SetWidth(1.2f, RelativeTo.ParentWidth);
			uicanvas.SetDepthOffset(-5f);
			uicanvas.SetRogue();
			uicanvas.m_TC.transform.Rotate(Vector3.forward, 15f);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0010AB38 File Offset: 0x00108F38
		public void RedDrawhandler(UIComponent _c)
		{
			PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
			Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
			Color red = Color.red;
			red.a = 0.75f;
			GGData ggdata = new GGData(rect);
			Material material = new Material(Shader.Find("Framework/VertexColorUnlit"));
			material.renderQueue = 3000;
			PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, red, material, this.m_camera);
		}
	}
}
