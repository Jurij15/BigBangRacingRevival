using System;

// Token: 0x0200038A RID: 906
public class PsUIEditorItemPurchasePopup : UISprite
{
	// Token: 0x06001A19 RID: 6681 RVA: 0x00121890 File Offset: 0x0011FC90
	public PsUIEditorItemPurchasePopup(UIComponent _parent)
		: base(_parent, true, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_editor_item_card", null), true)
	{
		this.SetSize(this.m_frame.width / this.m_frame.height * 0.44f, 0.44f, RelativeTo.ScreenHeight);
		this.SetMargins(0.04f, 0.04f, 0.04f, -0.04f, RelativeTo.OwnHeight);
		TouchAreaS.AddBeginTouchDelegate(new Func<TouchAreaC, bool>(this.TouchOutsideDelegate));
	}

	// Token: 0x06001A1A RID: 6682 RVA: 0x0012191C File Offset: 0x0011FD1C
	private bool TouchOutsideDelegate(TouchAreaC _touchAreaC)
	{
		if (_touchAreaC == this.m_TAC || (this.m_purchaseButton != null && _touchAreaC == this.m_purchaseButton.m_TAC))
		{
			return false;
		}
		PsUIBasePopup psUIBasePopup = this.GetRoot() as PsUIBasePopup;
		if (psUIBasePopup != null)
		{
			psUIBasePopup.CallAction("Exit");
		}
		return true;
	}

	// Token: 0x06001A1B RID: 6683 RVA: 0x00121974 File Offset: 0x0011FD74
	public void CreateContent(PsEditorItem _editorItem)
	{
		this.DestroyChildren();
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.5f, RelativeTo.ParentHeight);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_editorItem.m_iconImage, null), true, true);
		uifittedSprite.SetVerticalAlign(0f);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(0f);
		uiverticalList.SetMargins(0f, 0f, 0f, 0.225f, RelativeTo.ParentHeight);
		uiverticalList.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(_editorItem.m_name), PsFontManager.GetFont(PsFonts.HurmeBold), 0.08f, RelativeTo.ParentHeight, false, Align.Center, Align.Middle, "a1ee55", true, null);
		uitextbox.SetWidth(0.94f, RelativeTo.ParentWidth);
		uitextbox.SetVerticalAlign(0.48f);
		UITextbox uitextbox2 = new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.EDITOR_POPUP_PURCHASE), PsFontManager.GetFont(PsFonts.HurmeBold), 0.06f, RelativeTo.ParentHeight, false, Align.Center, Align.Middle, "ffffff", true, null);
		uitextbox2.SetWidth(0.92f, RelativeTo.ParentWidth);
		uitextbox2.SetVerticalAlign(0.28f);
		this.m_purchaseButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		this.m_purchaseButton.SetText(_editorItem.m_price.ToString(), 0.03f, 0.09f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_purchaseButton.SetGreenColors(true);
		this.m_purchaseButton.SetVerticalAlign(0f);
		this.m_purchaseButton.SetDepthOffset(-3f);
		PsCurrency currency = _editorItem.m_currency;
		if (currency != PsCurrency.Coin)
		{
			if (currency == PsCurrency.Gem)
			{
				this.m_purchaseButton.SetIcon("menu_resources_diamond_icon", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			}
		}
		else
		{
			this.m_purchaseButton.SetIcon("menu_resources_coin_icon", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		}
		this.Update();
	}

	// Token: 0x06001A1C RID: 6684 RVA: 0x00121BB3 File Offset: 0x0011FFB3
	public override void Step()
	{
		if (this.m_purchaseButton != null && this.m_purchaseButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Purchase");
		}
		base.Step();
	}

	// Token: 0x04001C7D RID: 7293
	private PsUIGenericButton m_purchaseButton;
}
