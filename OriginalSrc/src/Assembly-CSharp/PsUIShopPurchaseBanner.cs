using System;

// Token: 0x020003D0 RID: 976
public class PsUIShopPurchaseBanner : PsUIGenericButton
{
	// Token: 0x06001B8F RID: 7055 RVA: 0x001339F8 File Offset: 0x00131DF8
	public PsUIShopPurchaseBanner(UIVerticalList _parent, PsUnlockable _shopItem, Action _itemClicked)
		: base(_parent, 0.25f, 0.25f, 0.005f, "Button")
	{
		this.m_shopItem = _shopItem;
		this.m_itemClicked = _itemClicked;
		base.SetGreenColors(true);
		base.SetText(_shopItem.m_name, 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.Update();
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x00133A54 File Offset: 0x00131E54
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		PsMetrics.ButtonPressed(this.m_shopItem.m_identifier, "shop", null);
		if (!this.m_virtualCurrency)
		{
			this.m_itemClicked.Invoke();
		}
	}

	// Token: 0x06001B91 RID: 7057 RVA: 0x00133A8F File Offset: 0x00131E8F
	protected override void OnTouchDragStart(TLTouch _touch)
	{
		this.Highlight(false);
		this.HighlightSecondary(false);
		this.m_end = true;
	}

	// Token: 0x06001B92 RID: 7058 RVA: 0x00133AA6 File Offset: 0x00131EA6
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x04001DF3 RID: 7667
	private PsUnlockable m_shopItem;

	// Token: 0x04001DF4 RID: 7668
	private Action m_itemClicked;

	// Token: 0x04001DF5 RID: 7669
	private bool m_virtualCurrency;

	// Token: 0x020003D1 RID: 977
	private class PsUIShopPopupNotEnoughResources : PsUIPopup
	{
		// Token: 0x06001B93 RID: 7059 RVA: 0x00133AB0 File Offset: 0x00131EB0
		public PsUIShopPopupNotEnoughResources(bool _coins, Action _proceed = null, Action _cancel = null)
			: base(_proceed, _cancel, false, "Popup")
		{
			UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
			uiverticalList.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
			uiverticalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			uiverticalList.SetDrawHandler(new UIDrawDelegate(PsUIPopup.PopupDrawHandler));
			string text = ((!_coins) ? "diamonds" : "coins");
			string text2 = PsStrings.Get(StringID.NOT_ENOUGH);
			text2 = text2.Replace("%1", text);
			new UITextbox(uiverticalList, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Left, Align.Top, null, true, null);
			UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.RemoveTouchAreas();
			uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_ok.SetText("Ok", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_ok.SetGreenColors(true);
			this.m_ok.Update();
			this.Update();
			base.UpdatePopupBG();
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00133BF3 File Offset: 0x00131FF3
		public override void Step()
		{
			if (this.m_ok.m_hit)
			{
				this.Destroy();
				return;
			}
			base.Step();
		}

		// Token: 0x04001DF6 RID: 7670
		private PsUIGenericButton m_ok;
	}
}
