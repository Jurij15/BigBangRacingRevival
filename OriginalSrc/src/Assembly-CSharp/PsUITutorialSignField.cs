using System;

// Token: 0x0200026F RID: 623
public class PsUITutorialSignField : PsUIInputTextField
{
	// Token: 0x0600129B RID: 4763 RVA: 0x000B7945 File Offset: 0x000B5D45
	public PsUITutorialSignField()
		: base(null)
	{
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x000B794E File Offset: 0x000B5D4E
	public PsUITutorialSignField(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x000B7958 File Offset: 0x000B5D58
	protected override void ConstructUI()
	{
		base.SetMinMaxCharacterCount(1, 20);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetWidth(0.58f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, "Text", PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, null, null);
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
