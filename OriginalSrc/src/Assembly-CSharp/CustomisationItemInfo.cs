using System;
using Prime31;

// Token: 0x020000DF RID: 223
public class CustomisationItemInfo : UIVerticalList
{
	// Token: 0x060004DA RID: 1242 RVA: 0x00040110 File Offset: 0x0003E510
	public CustomisationItemInfo(UIComponent _parent, PsCustomisationItem _customisationItem, Action _purchaseAction)
		: base(_parent, string.Empty)
	{
		this.m_purchaseAction = _purchaseAction;
		string text = PsStrings.Get(StringID.LOADING);
		string text2 = PsStrings.Get(_customisationItem.m_title);
		bool flag = false;
		IAPProduct iapproductById = PsIAPManager.GetIAPProductById(_customisationItem.m_iapIdentifier);
		if (iapproductById != null)
		{
			flag = true;
			text = iapproductById.price;
			if (!string.IsNullOrEmpty(iapproductById.title))
			{
				text2 = iapproductById.title;
			}
		}
		this.SetWidth(1.4f, RelativeTo.ParentWidth);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.UpgradeInfoBox));
		this.SetDepthOffset(-5f);
		this.SetVerticalAlign(0.7f);
		this.m_contentHolder = new UIVerticalList(this, "cardback");
		this.m_contentHolder.SetMargins(0.1f, 0.1f, 0.1f, 0.15f, RelativeTo.OwnWidth);
		this.m_contentHolder.SetSpacing(0.05f, RelativeTo.OwnWidth);
		this.m_contentHolder.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(this.m_contentHolder, false, string.Empty, text2.ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.09f, RelativeTo.ParentWidth, false, Align.Center, Align.Top, null, true, null);
		UICanvas uicanvas = new UICanvas(this.m_contentHolder, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.66f, 0.66f, RelativeTo.ParentWidth);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_customisationItem.m_iconName, null), true, true);
		if (!string.IsNullOrEmpty(_customisationItem.m_iapIdentifier))
		{
			this.m_purchaseButton = new PsUIGenericButton(this.m_contentHolder, 0.25f, 0.25f, 0.005f, "Button");
			this.m_purchaseButton.SetFittedText(text, 0.03f, 0.9f, RelativeTo.ParentWidth, false);
			this.m_purchaseButton.SetWidth(1f, RelativeTo.ParentWidth);
			this.m_purchaseButton.SetGreenColors(true);
			if (!flag)
			{
				this.m_purchaseButton.SetGrayColors();
				this.m_purchaseButton.DisableTouchAreas(true);
			}
		}
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0004031A File Offset: 0x0003E71A
	public override void Step()
	{
		if (this.m_purchaseButton != null && this.m_purchaseButton.m_hit && this.m_purchaseAction != null)
		{
			this.m_purchaseAction.Invoke();
		}
		base.Step();
	}

	// Token: 0x0400062F RID: 1583
	private PsUIGenericButton m_purchaseButton;

	// Token: 0x04000630 RID: 1584
	private Action m_purchaseAction;

	// Token: 0x04000631 RID: 1585
	public UIVerticalList m_contentHolder;

	// Token: 0x04000632 RID: 1586
	public PsUIResourceProgressBar m_resourceBar;
}
