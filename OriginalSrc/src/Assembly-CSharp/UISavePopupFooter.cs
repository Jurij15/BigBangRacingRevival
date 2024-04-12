using System;

// Token: 0x020003DB RID: 987
public class UISavePopupFooter : UIScaleToContentCanvas
{
	// Token: 0x06001BD3 RID: 7123 RVA: 0x00135B90 File Offset: 0x00133F90
	public UISavePopupFooter(UIComponent _parent, string _tag)
		: base(_parent, _tag, true, true)
	{
		this.SetMargins(0.05f, 0.05f, 0.05f, 0f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		this.m_cancelButton = new UITextButton(this, "SavePanelCancelButton", "Cancel", PsFontManager.GetFont(PsFonts.HurmeBold), 0.0375f, RelativeTo.ScreenHeight, true);
		this.m_cancelButton.SetMargins(0.025f, 0.025f, 0.02f, 0.015f, RelativeTo.ScreenHeight);
		this.m_cancelButton.SetHorizontalAlign(0f);
		this.m_cancelButton.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.NegativeButton));
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, _tag);
		uihorizontalList.SetHorizontalAlign(1f);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		this.m_saveButton = new UITextButton(uihorizontalList, "SavePanelSaveButton", "Save", PsFontManager.GetFont(PsFonts.HurmeBold), 0.0375f, RelativeTo.ScreenHeight, true);
		this.m_saveButton.SetMargins(0.025f, 0.025f, 0.02f, 0.015f, RelativeTo.ScreenHeight);
		this.m_saveButton.SetHorizontalAlign(1f);
		this.m_saveButton.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.PositiveButton));
	}

	// Token: 0x04001E1D RID: 7709
	public UITextButton m_cancelButton;

	// Token: 0x04001E1E RID: 7710
	public UITextButton m_saveButton;

	// Token: 0x04001E1F RID: 7711
	public UITextButton m_publishButton;
}
