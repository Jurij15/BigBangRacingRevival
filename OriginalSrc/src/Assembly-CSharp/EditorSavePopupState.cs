using System;

// Token: 0x020001E4 RID: 484
public class EditorSavePopupState : BasicState
{
	// Token: 0x06000E88 RID: 3720 RVA: 0x000874BC File Offset: 0x000858BC
	public override void Enter(IStatedObject _parent)
	{
		if (string.IsNullOrEmpty(PsState.m_activeGameLoop.m_minigameMetaData.id))
		{
			PsState.m_activeMinigame.GenerateDefaultNameAndDescription();
		}
		this.m_levelName = PsState.m_activeGameLoop.m_minigameMetaData.name;
		this.m_levelDescription = PsState.m_activeGameLoop.m_minigameMetaData.description;
		this.m_model = new UIModel(this, null);
		this.m_savePopupContainer = new UIScrollableCanvas(null, "SavePopupContainer");
		this.m_savePopupContainer.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_savePopupContainer.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_savePopupContainer.SetMargins(0.125f, 0.125f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_savePopupContainer.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupBackground));
		UIVerticalList uiverticalList = new UIVerticalList(this.m_savePopupContainer, "SavePopup");
		uiverticalList.RemoveDrawHandler();
		new UIPopupHeader(uiverticalList, "SavePopupHeader", "save", "Enter a name and press Save");
		UIPopupContentArea uipopupContentArea = new UIPopupContentArea(uiverticalList, "ContentArea");
		this.m_nameField = new UITextArea(uipopupContentArea, "SavePopupNameField", "Track name", string.Empty, null, this.m_model, "m_levelName", -1, 128, "464646", "464646");
		this.m_descriptionField = new UITextArea(uipopupContentArea, "SavePopupDescriptionField", "Track description", string.Empty, null, this.m_model, "m_levelDescription", 3, 128, "464646", "464646");
		this.m_footer = new UISavePopupFooter(uiverticalList, "SavePopupFooter");
		this.m_savePopupContainer.Update();
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x00087661 File Offset: 0x00085A61
	private void NameDoneCallback(string _value)
	{
		Debug.Log("DONE: " + _value, null);
		this.m_nameField.SetValue(_value);
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x00087680 File Offset: 0x00085A80
	private void DescriptionDoneCallback(string _value)
	{
		Debug.Log("DONE: " + _value, null);
		this.m_descriptionField.SetValue(_value);
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x0008769F File Offset: 0x00085A9F
	private void KeyPressedCallback(string _value)
	{
		Debug.Log("PRESSED: " + _value, null);
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x000876B2 File Offset: 0x00085AB2
	private void CancelCallback()
	{
		Debug.Log("CANCEL", null);
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x000876C0 File Offset: 0x00085AC0
	public override void Execute()
	{
		if (this.m_footer.m_saveButton.m_hit)
		{
			string[] array = new string[PsState.m_activeMinigame.m_itemCount.Keys.Count];
			int num = 0;
			foreach (string text in PsState.m_activeMinigame.m_itemCount.Keys)
			{
				array[num] = text;
				num++;
			}
			PsState.m_activeGameLoop.m_minigameMetaData.name = this.m_levelName;
			PsState.m_activeGameLoop.m_minigameMetaData.description = this.m_levelDescription;
			PsState.m_activeGameLoop.m_minigameMetaData.complexity = PsState.m_activeMinigame.m_complexity;
			PsState.m_activeGameLoop.m_minigameMetaData.levelRequirement = PsState.m_activeMinigame.m_levelRequirement;
			PsState.m_activeGameLoop.m_minigameMetaData.playerUnit = PsState.m_activeMinigame.m_playerUnitName;
			PsState.m_activeGameLoop.m_minigameMetaData.itemsUsed = array;
			PsState.m_activeGameLoop.m_minigameMetaData.itemsCount = PsState.m_activeMinigame.m_itemCount;
			PsState.m_activeGameLoop.m_minigameMetaData.timeSpentEditing += PsState.m_activeMinigame.GetTimeSinceInit();
			PsState.m_activeMinigame.InitializeTimer();
			Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorSaveState(false));
		}
		else if (this.m_nameField.m_hit)
		{
			UITextInput uitextInput = new UITextInput("Track name", new Action<string>(this.NameDoneCallback), new Action(this.CancelCallback), null, 1, true, 128);
			uitextInput.SetText((string)this.m_nameField.GetValue());
			uitextInput.SetMaxCharacterCount(32);
			uitextInput.SetMinCharacterCount(3);
			uitextInput.Update();
		}
		else if (this.m_descriptionField.m_hit)
		{
			UITextInput uitextInput2 = new UITextInput("Track description", new Action<string>(this.DescriptionDoneCallback), new Action(this.CancelCallback), null, 1, true, 128);
			uitextInput2.SetText((string)this.m_descriptionField.GetValue());
			uitextInput2.Update();
		}
		else if (this.m_footer.m_cancelButton.m_hit)
		{
			Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorBaseState());
		}
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x00087948 File Offset: 0x00085D48
	public override void Exit()
	{
		if (this.m_savePopupContainer != null)
		{
			this.m_savePopupContainer.Destroy();
			this.m_savePopupContainer = null;
		}
	}

	// Token: 0x0400117B RID: 4475
	public UIModel m_model;

	// Token: 0x0400117C RID: 4476
	public string m_levelName;

	// Token: 0x0400117D RID: 4477
	public string m_levelDescription;

	// Token: 0x0400117E RID: 4478
	private UIScrollableCanvas m_savePopupContainer;

	// Token: 0x0400117F RID: 4479
	private UITextArea m_nameField;

	// Token: 0x04001180 RID: 4480
	private UITextArea m_descriptionField;

	// Token: 0x04001181 RID: 4481
	private UISavePopupFooter m_footer;
}
