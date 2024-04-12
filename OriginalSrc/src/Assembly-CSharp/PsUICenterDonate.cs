using System;

// Token: 0x020003AA RID: 938
public class PsUICenterDonate : PsUIHeaderedCanvas
{
	// Token: 0x06001AE7 RID: 6887 RVA: 0x0012B92C File Offset: 0x00129D2C
	public PsUICenterDonate(UIComponent _parent)
		: base(_parent, "ShopConfirmation", true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.BgDrawhandler));
		this.m_container.CreateTouchAreas();
		this.SetWidth(0.55f, RelativeTo.ScreenWidth);
		this.SetHeight(0.65f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001AE8 RID: 6888 RVA: 0x0012BA44 File Offset: 0x00129E44
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, "Donate", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#FFFFFF", null);
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x0012BABC File Offset: 0x00129EBC
	public void CreateContent(UIComponent _parent)
	{
		PsUICreatorInfo psUICreatorInfo = new PsUICreatorInfo(_parent, false, false, false, false, false, false);
		psUICreatorInfo.SetVerticalAlign(1f);
		string text = "Show your appreciation by donating\nsome coins to the Level Creator.";
		UIText uitext = new UIText(_parent, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, "#ffffff", null);
		int[] array = new int[] { 50, 1500, 50000 };
		int num = 0;
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetVerticalAlign(0f);
		uiverticalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		PsUIGenericButton psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.m_textColor = "#FFE539";
		psUIGenericButton.SetText(array[num++].ToString(), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		psUIGenericButton.SetIcon("menu_resources_coin_icon", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		psUIGenericButton.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.m_textColor = "#FFE539";
		psUIGenericButton.SetText(array[num++].ToString(), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		psUIGenericButton.SetIcon("menu_resources_coin_icon", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		psUIGenericButton.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		psUIGenericButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.m_textColor = "#FFE539";
		psUIGenericButton.SetText(array[num++].ToString(), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		psUIGenericButton.SetIcon("menu_resources_coin_icon", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		psUIGenericButton.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
	}

	// Token: 0x04001D53 RID: 7507
	private PsUIGenericButton m_buy;

	// Token: 0x04001D54 RID: 7508
	private UITextbox m_sub;
}
