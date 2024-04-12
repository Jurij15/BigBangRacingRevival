using System;

// Token: 0x020001E2 RID: 482
public class EditorPublishPopupState : BasicState
{
	// Token: 0x06000E76 RID: 3702 RVA: 0x00086A6C File Offset: 0x00084E6C
	public override void Enter(IStatedObject _parent)
	{
		if (string.IsNullOrEmpty(PsState.m_activeGameLoop.m_minigameMetaData.id))
		{
			PsState.m_activeMinigame.GenerateDefaultNameAndDescription();
		}
		this.m_levelName = PsState.m_activeGameLoop.GetName();
		this.m_levelDescription = PsState.m_activeGameLoop.GetDescription();
		this.m_model = new UIModel(this, null);
		this.m_publishPopupContainer = new UIScrollableCanvas(null, "PublishPopupContainer");
		this.m_publishPopupContainer.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_publishPopupContainer.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_publishPopupContainer.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupBackground));
		UIVerticalList uiverticalList = new UIVerticalList(this.m_publishPopupContainer, "PublishPopup");
		uiverticalList.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		new UIPopupHeader(uiverticalList, "PublishPopupHeader", "GO LIVE!", "Enter a name and press 'Go Live!'");
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, "hList");
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupContentArea));
		this.m_screenshotSelector = new UIEditorPublishScreenshotSelect(uihorizontalList, "Screenshot area");
		this.m_screenshotSelector.SetScreenshot(EditorScene.SwitchScreenshot(true));
		UIVerticalList uiverticalList2 = new UIVerticalList(uihorizontalList, "vList");
		uiverticalList2.SetMargins(0.075f, 0.075f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList2.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uiverticalList2.SetWidth(0.6f, RelativeTo.ScreenWidth);
		uiverticalList2.RemoveDrawHandler();
		this.m_nameField = new UITextArea(uiverticalList2, "PublishPopupNameField", "Track name", string.Empty, null, this.m_model, "m_levelName", -1, 128, "464646", "464646");
		this.m_descriptionField = new UITextArea(uiverticalList2, "SavePopupDescriptionField", "Track description", string.Empty, null, this.m_model, "m_levelDescription", 3, 128, "464646", "464646");
		this.m_footer = new UIPublishPopupFooter(uiverticalList, "PublishPopupFooter");
		this.m_publishPopupContainer.Update();
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x00086C8C File Offset: 0x0008508C
	private void NameDoneCallback(string _value)
	{
		Debug.Log("DONE: " + _value, null);
		this.m_nameField.SetValue(_value);
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x00086CAB File Offset: 0x000850AB
	private void DescriptionDoneCallback(string _value)
	{
		Debug.Log("DONE: " + _value, null);
		this.m_descriptionField.SetValue(_value);
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x00086CCA File Offset: 0x000850CA
	private void KeyPressedCallback(string _value)
	{
		Debug.Log("PRESSED: " + _value, null);
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x00086CDD File Offset: 0x000850DD
	private void CancelCallback()
	{
		Debug.Log("CANCEL", null);
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x00086CEC File Offset: 0x000850EC
	public override void Execute()
	{
		if (this.m_footer.m_publishButton.m_hit)
		{
			Debug.LogWarning("PUBLISH UPGRADES: " + PsState.m_activeGameLoop.m_minigameMetaData.creatorUpgrades);
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
			Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorPublishState());
			PsState.m_activeGameLoop.m_minigameMetaData.timeSpentEditing += PsState.m_activeMinigame.GetTimeSinceInit();
			PsState.m_activeMinigame.InitializeTimer();
		}
		else if (this.m_footer.m_cancelButton.m_hit)
		{
			Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorTestEndState());
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
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x00086F90 File Offset: 0x00085390
	public override void Exit()
	{
		if (this.m_publishPopupContainer != null)
		{
			this.m_publishPopupContainer.Destroy();
			this.m_publishPopupContainer = null;
		}
	}

	// Token: 0x0400116C RID: 4460
	public UIModel m_model;

	// Token: 0x0400116D RID: 4461
	public string m_levelName;

	// Token: 0x0400116E RID: 4462
	public string m_levelDescription;

	// Token: 0x0400116F RID: 4463
	private UITextArea m_nameField;

	// Token: 0x04001170 RID: 4464
	private UITextArea m_descriptionField;

	// Token: 0x04001171 RID: 4465
	private UICanvas m_publishPopupContainer;

	// Token: 0x04001172 RID: 4466
	private UIPublishPopupFooter m_footer;

	// Token: 0x04001173 RID: 4467
	private UIEditorPublishScreenshotSelect m_screenshotSelector;
}
