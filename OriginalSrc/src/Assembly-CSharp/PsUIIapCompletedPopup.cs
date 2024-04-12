using System;

// Token: 0x020003C2 RID: 962
public class PsUIIapCompletedPopup : PsUIHeaderedCanvas
{
	// Token: 0x06001B65 RID: 7013 RVA: 0x00131BC4 File Offset: 0x0012FFC4
	public PsUIIapCompletedPopup(UIComponent _parent)
		: base(_parent, "IapSucceed", false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.BgDrawhandler));
		this.SetWidth(0.65f, RelativeTo.ScreenHeight);
		this.SetHeight(0.5f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x00131CBB File Offset: 0x001300BB
	public void CreateContent(string _title, string _frame)
	{
		this.CreateContent(this, _frame);
		this.CreateHeaderContent(this.m_header, _title);
		this.m_container.Update();
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x00131CE0 File Offset: 0x001300E0
	public void CreateHeaderContent(UIComponent _parent, string _title)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIComponent uicomponent = new UIComponent(uihorizontalList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetHeight(0.055f, RelativeTo.ScreenHeight);
		uicomponent.RemoveDrawHandler();
		_title = ((!string.IsNullOrEmpty(_title)) ? _title : "Completed");
		UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, _title, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x00131D90 File Offset: 0x00130190
	public void CreateContent(UIComponent _parent, string _frame)
	{
		_parent.RemoveTouchAreas();
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(null);
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.22f, 0.22f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_frame, null), true, true);
		new UIText(uiverticalList, false, "message", PsStrings.Get(StringID.SHOP_PURCHASE_COMPLETED), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.035f, RelativeTo.ScreenHeight, null, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_okButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_okButton.SetText(PsStrings.Get(StringID.OK).ToUpper() + "!", 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_okButton.SetGreenColors(true);
		this.m_okButton.SetVerticalAlign(0f);
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x00131EF8 File Offset: 0x001302F8
	public override void Step()
	{
		if (this.m_okButton != null && this.m_okButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001DC5 RID: 7621
	private PsUIGenericButton m_okButton;
}
