using System;

// Token: 0x020001DF RID: 479
public class EditorEditTextPopupState : BasicState
{
	// Token: 0x06000E5A RID: 3674 RVA: 0x00085C4B File Offset: 0x0008404B
	public EditorEditTextPopupState(TextModel _model)
	{
		Debug.Log(_model.m_text, null);
		this.m_textModel = _model;
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x00085C68 File Offset: 0x00084068
	public override void Enter(IStatedObject _parent)
	{
		this.m_model = new UIModel(this.m_textModel, null);
		this.m_savePopupContainer = new UIScrollableCanvas(null, "SavePopupContainer");
		this.m_savePopupContainer.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_savePopupContainer.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_savePopupContainer.SetMargins(0.125f, 0.125f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_savePopupContainer.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupBackground));
		UIVerticalList uiverticalList = new UIVerticalList(this.m_savePopupContainer, "SavePopup");
		uiverticalList.RemoveDrawHandler();
		new UIPopupHeader(uiverticalList, "SavePopupHeader", "edit", string.Empty);
		UIPopupContentArea uipopupContentArea = new UIPopupContentArea(uiverticalList, "ContentArea");
		this.m_nameField = new UITextArea(uipopupContentArea, "SavePopupNameField", "Text", string.Empty, null, this.m_model, "m_text", -1, 128, "464646", "464646");
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetHorizontalAlign(1f);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		this.m_cancelButton = new UITextButton(uihorizontalList, "SavePanelCancelButton", "Cancel", PsFontManager.GetFont(PsFonts.HurmeBold), 0.0375f, RelativeTo.ScreenHeight, true);
		this.m_cancelButton.SetMargins(0.025f, 0.025f, 0.02f, 0.015f, RelativeTo.ScreenHeight);
		this.m_cancelButton.SetHorizontalAlign(0f);
		this.m_cancelButton.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.NegativeButton));
		this.m_saveButton = new UITextButton(uihorizontalList, "SavePanelSaveButton", "Save", PsFontManager.GetFont(PsFonts.HurmeBold), 0.0375f, RelativeTo.ScreenHeight, true);
		this.m_saveButton.SetMargins(0.025f, 0.025f, 0.02f, 0.015f, RelativeTo.ScreenHeight);
		this.m_saveButton.SetHorizontalAlign(1f);
		this.m_saveButton.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.PositiveButton));
		this.m_savePopupContainer.Update();
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x00085E9C File Offset: 0x0008429C
	public override void Execute()
	{
		if (this.m_saveButton.m_hit)
		{
			this.m_textModel.m_done = true;
		}
		else if (this.m_cancelButton.m_hit)
		{
			this.m_textModel.m_cancelled = true;
		}
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x00085EDB File Offset: 0x000842DB
	public override void Exit()
	{
		if (this.m_savePopupContainer != null)
		{
			this.m_savePopupContainer.Destroy();
			this.m_savePopupContainer = null;
		}
	}

	// Token: 0x04001154 RID: 4436
	public TextModel m_textModel;

	// Token: 0x04001155 RID: 4437
	public UIModel m_model;

	// Token: 0x04001156 RID: 4438
	private UIScrollableCanvas m_savePopupContainer;

	// Token: 0x04001157 RID: 4439
	private UITextArea m_nameField;

	// Token: 0x04001158 RID: 4440
	private UITextArea m_descriptionField;

	// Token: 0x04001159 RID: 4441
	private UISavePopupFooter m_footer;

	// Token: 0x0400115A RID: 4442
	private UITextbox m_cancelButton;

	// Token: 0x0400115B RID: 4443
	private UITextbox m_saveButton;
}
