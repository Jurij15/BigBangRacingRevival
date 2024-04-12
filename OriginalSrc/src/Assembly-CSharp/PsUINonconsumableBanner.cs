using System;
using UnityEngine;

// Token: 0x0200032B RID: 811
public class PsUINonconsumableBanner : UIVerticalList
{
	// Token: 0x060017CA RID: 6090 RVA: 0x00100DDD File Offset: 0x000FF1DD
	public PsUINonconsumableBanner(UIComponent _parent, string _productIdentifier, string _price)
		: base(_parent, string.Empty)
	{
		this.m_iap = PsNonConsumableIAPs.FindItemWithStoreId(_productIdentifier);
		this.m_identifier = _productIdentifier;
		this.RemoveDrawHandler();
		this.m_sprite = new PsUINonconsumableBanner.PsUIBannerSprite(this, this.m_iap, _price);
	}

	// Token: 0x060017CB RID: 6091 RVA: 0x00100E17 File Offset: 0x000FF217
	public void Purchased()
	{
		this.m_sprite.Destroy();
		this.m_sprite = new PsUINonconsumableBanner.PsUIBannerSprite(this, this.m_iap, string.Empty);
		this.m_sprite.Update();
	}

	// Token: 0x060017CC RID: 6092 RVA: 0x00100E46 File Offset: 0x000FF246
	public bool IsHit()
	{
		if (this.m_sprite.m_hit && !PsNonConsumableIAPs.PlayerHasPurchasedItem(this.m_iap.m_storeIdentifier))
		{
			SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
			return true;
		}
		return false;
	}

	// Token: 0x04001A9B RID: 6811
	private NonConsumableIAP m_iap;

	// Token: 0x04001A9C RID: 6812
	public PsUINonconsumableBanner.PsUIBannerSprite m_sprite;

	// Token: 0x0200032C RID: 812
	public class PsUIBannerSprite : UIFittedSprite
	{
		// Token: 0x060017CD RID: 6093 RVA: 0x00100E84 File Offset: 0x000FF284
		public PsUIBannerSprite(UIComponent _parent, NonConsumableIAP _iap, string _price)
			: base(_parent, true, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_iap.m_bannerFrame, null), true, true)
		{
			this.m_TAC.m_letTouchesThrough = true;
			this.m_price = _price;
			this.m_iap = _iap;
			this.m_name = PsStrings.Get(_iap.m_displayNameID);
			this.m_description = ((_iap.m_descriptionID != StringID.EMPTY) ? PsStrings.Get(_iap.m_descriptionID) : null);
			this.Construct();
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00100F14 File Offset: 0x000FF314
		public void Construct()
		{
			string text = "#71230F";
			if (PsNonConsumableIAPs.PlayerHasPurchasedItem(this.m_iap.m_storeIdentifier))
			{
				this.m_bannerDisabled = true;
				this.m_description = ((this.m_iap.m_playerOwnsID != StringID.EMPTY) ? PsStrings.Get(this.m_iap.m_playerOwnsID) : null);
				text = "#4A1707";
				this.m_price = PsStrings.Get(StringID.SHOP_PRICE_PURCHASED);
				Color white = Color.white;
				white.r = (white.g = (white.b = 0.5f));
				base.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
				base.SetColor(white);
			}
			if (this.m_name != null && this.m_description != null)
			{
				UIComponent uicomponent = new UIComponent(this, false, string.Empty, null, null, string.Empty);
				uicomponent.SetVerticalAlign(0.9f);
				uicomponent.SetHeight(0.2f, RelativeTo.ParentHeight);
				uicomponent.SetWidth(0.485f, RelativeTo.ParentWidth);
				uicomponent.RemoveDrawHandler();
				UIFittedText uifittedText = new UIFittedText(uicomponent, false, "title", this.m_name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, text, null);
				bool flag = false;
				string empty = string.Empty;
				string description = this.m_description;
				string font = PsFontManager.GetFont(PsFonts.KGSecondChances);
				float num = 0.15f;
				RelativeTo relativeTo = RelativeTo.ParentHeight;
				bool flag2 = false;
				Align align = Align.Center;
				Align align2 = Align.Middle;
				string text2 = text;
				UITextbox uitextbox = new UITextbox(this, flag, empty, description, font, num, relativeTo, flag2, align, align2, null, true, text2);
				uitextbox.SetVerticalAlign(0.295f);
				uitextbox.SetWidth(0.545f, RelativeTo.ParentWidth);
			}
			if (this.m_iap.m_showPrice)
			{
				UIComponent uicomponent2 = new UIComponent(this, false, string.Empty, null, null, string.Empty);
				uicomponent2.SetHeight(0.2f, RelativeTo.ParentHeight);
				uicomponent2.SetWidth(0.175f, RelativeTo.ParentWidth);
				uicomponent2.SetMargins(0.1f, RelativeTo.OwnHeight);
				uicomponent2.SetVerticalAlign(0.11f);
				uicomponent2.SetHorizontalAlign(0.94f);
				uicomponent2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.CurrencyLabelBackground));
				UIFittedText uifittedText2 = new UIFittedText(uicomponent2, false, string.Empty, this.m_price, PsFontManager.GetFont(PsFonts.HurmeSemiBold), true, "#000000", null);
				uifittedText2.SetDepthOffset(-5f);
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00101158 File Offset: 0x000FF558
		protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
		{
			base.OnTouchRollIn(_touch, _secondary);
			if (this.m_bannerDisabled)
			{
				return;
			}
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x001011CC File Offset: 0x000FF5CC
		protected override void OnTouchBegan(TLTouch _touch)
		{
			base.OnTouchBegan(_touch);
			if (this.m_bannerDisabled)
			{
				return;
			}
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0010123C File Offset: 0x000FF63C
		protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
		{
			base.OnTouchRollOut(_touch, _secondary);
			if (this.m_bannerDisabled)
			{
				return;
			}
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x001012B0 File Offset: 0x000FF6B0
		protected override void OnTouchRelease(TLTouch _touch, bool _inside)
		{
			base.OnTouchRelease(_touch, _inside);
			if (this.m_bannerDisabled)
			{
				return;
			}
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
		}

		// Token: 0x04001A9D RID: 6813
		private bool m_bannerDisabled;

		// Token: 0x04001A9E RID: 6814
		private string m_price;

		// Token: 0x04001A9F RID: 6815
		private string m_name;

		// Token: 0x04001AA0 RID: 6816
		private string m_description;

		// Token: 0x04001AA1 RID: 6817
		private NonConsumableIAP m_iap;
	}
}
