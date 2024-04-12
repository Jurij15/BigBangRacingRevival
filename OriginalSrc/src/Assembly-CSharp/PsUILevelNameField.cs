using System;

// Token: 0x0200026C RID: 620
public class PsUILevelNameField : PsUIInputTextField
{
	// Token: 0x0600128E RID: 4750 RVA: 0x000B74B0 File Offset: 0x000B58B0
	public PsUILevelNameField()
		: base(null)
	{
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x000B74B9 File Offset: 0x000B58B9
	public PsUILevelNameField(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x000B74C4 File Offset: 0x000B58C4
	protected override void ConstructUI()
	{
		base.SetMinMaxCharacterCount(3, 20);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetWidth(0.58f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.EDITOR_PUBLISH_LEVEL_NAME), PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, null, null);
		uitext.SetColor("#a8e2ff", null);
		uitext.SetMargins(0.03f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uitext.SetHorizontalAlign(0f);
		UICanvas uicanvas = new UICanvas(uiverticalList, true, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.58f, 0.065f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		uicanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.TextfieldDark));
		UIFittedText uifittedText = new UIFittedText(uicanvas, true, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", null);
		uifittedText.SetHorizontalAlign(0f);
		uifittedText.m_tmc.m_textMesh.fontStyle = 2;
		base.SetTextField(uifittedText);
	}
}
