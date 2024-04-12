using System;

// Token: 0x020003DA RID: 986
public class UIPublishPopupFooter : UIScaleToContentCanvas
{
	// Token: 0x06001BD2 RID: 7122 RVA: 0x00135A60 File Offset: 0x00133E60
	public UIPublishPopupFooter(UIComponent _parent, string _tag)
		: base(_parent, _tag, true, true)
	{
		this.SetMargins(0.05f, 0.05f, 0.05f, 0f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		this.m_cancelButton = new UITextButton(this, "SavePanelCancelButton", "Cancel", PsFontManager.GetFont(PsFonts.HurmeBold), 0.0375f, RelativeTo.ScreenHeight, true);
		this.m_cancelButton.SetMargins(0.025f, 0.025f, 0.02f, 0.015f, RelativeTo.ScreenHeight);
		this.m_cancelButton.SetHorizontalAlign(0f);
		this.m_cancelButton.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.NegativeButton));
		this.m_publishButton = new UITextButton(this, "SavePanelSaveButton", "Go Live!", PsFontManager.GetFont(PsFonts.HurmeBold), 0.0375f, RelativeTo.ScreenHeight, true);
		this.m_publishButton.SetMargins(0.025f, 0.025f, 0.02f, 0.015f, RelativeTo.ScreenHeight);
		this.m_publishButton.SetHorizontalAlign(1f);
		this.m_publishButton.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.PositiveButton));
	}

	// Token: 0x04001E19 RID: 7705
	public UITextButton m_cancelButton;

	// Token: 0x04001E1A RID: 7706
	public UITextButton m_publishButton;
}
