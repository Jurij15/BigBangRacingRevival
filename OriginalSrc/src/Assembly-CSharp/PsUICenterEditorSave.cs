using System;
using UnityEngine;

// Token: 0x020002A1 RID: 673
public class PsUICenterEditorSave : PsUIHeaderedCanvas
{
	// Token: 0x06001449 RID: 5193 RVA: 0x000CE3E8 File Offset: 0x000CC7E8
	public PsUICenterEditorSave(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DebriefBackground));
		this.SetWidth(0.8f, RelativeTo.ScreenWidth);
		this.SetHeight(0.75f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
		this.m_model = new UIModel(this, null);
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x000CE500 File Offset: 0x000CC900
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, "SAVE LEVEL", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
		UICanvas uicanvas = new UICanvas(uitext, false, string.Empty, null, string.Empty);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetMargins(-1.5f, 0f, -0.125f, -0.125f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("hud_icon_header_save", null);
		UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true);
		uisprite.SetSize(1f, 1f * (frame.height / frame.width), RelativeTo.ParentHeight);
		uisprite.SetHorizontalAlign(0f);
		uisprite.SetColor(DebugDraw.HexToColor("#95e5ff") * Color.gray);
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x000CE628 File Offset: 0x000CCA28
	public void CreateContent(UIComponent _parent)
	{
		PsState.m_activeMinigame.GenerateDefaultNameAndDescription();
		this.SetMargins(0.03f, 0.03f, 0f, 0.05f, RelativeTo.ScreenHeight);
		this.SetHeight(0.65f, RelativeTo.ScreenHeight);
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetVerticalAlign(0.5f);
		UIVerticalList uiverticalList = new UIVerticalList(_parent, "ContentArea");
		uiverticalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		this.m_levelName = new PsUILevelNameField(uiverticalList);
		this.m_levelName.SetText(PsState.m_activeGameLoop.m_minigameMetaData.name);
		this.m_levelDescription = new PsUILevelDescriptionField(uiverticalList);
		this.m_levelDescription.SetText(PsState.m_activeGameLoop.m_minigameMetaData.description);
		this.m_levelDescription.SetHorizontalAlign(0f);
		this.m_saveButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		this.m_saveButton.SetText("SAVE", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_saveButton.SetIcon("hud_icon_header_save", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_saveButton.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_saveButton.SetGreenColors(true);
		this.m_saveButton.SetDepthOffset(-4f);
		this.m_saveButton.SetAlign(0.85f, -0.25f);
		if (PsState.m_adminMode)
		{
			UIVerticalList uiverticalList2 = new UIVerticalList(this, string.Empty);
			uiverticalList2.SetSpacing(0.01f, RelativeTo.ScreenHeight);
			uiverticalList2.SetAlign(0.5f, -0.25f);
			uiverticalList2.RemoveDrawHandler();
			UIText uitext = new UIText(uiverticalList2, false, string.Empty, "Save As Template", PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, null, null);
			uitext.SetColor("#a8e2ff", null);
			this.m_saveAsTemplate = new PsUISwitch(uiverticalList2, PsState.m_activeGameLoop.m_minigameMetaData.template, 0.03f, "On", "Off", 0.05f, null);
		}
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x000CE830 File Offset: 0x000CCC30
	public override void Step()
	{
		if (this.m_saveButton.m_hit)
		{
			PsIngameMenu.CloseAll();
			EditorBaseState.RemoveTransformGizmo();
			UndoManager.Purge();
			EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
			if (!PsState.m_activeMinigame.m_editing)
			{
				(LevelManager.m_currentLevel as Minigame).m_groundNode.RevertGroundFromPlay();
			}
			LevelManager.ResetCurrentLevel();
			PsState.m_activeMinigame.SetLayerItems();
			string[] array = new string[PsState.m_activeMinigame.m_itemCount.Keys.Count];
			int num = 0;
			foreach (string text in PsState.m_activeMinigame.m_itemCount.Keys)
			{
				array[num] = text;
				num++;
			}
			if (this.m_levelName.GetText() != PsState.m_activeGameLoop.m_minigameMetaData.name || this.m_levelDescription.GetText() != PsState.m_activeGameLoop.m_minigameMetaData.description)
			{
				PsState.m_activeMinigame.m_changed = true;
			}
			PsState.m_activeGameLoop.m_minigameMetaData.name = this.m_levelName.GetText();
			PsState.m_activeGameLoop.m_minigameMetaData.description = this.m_levelDescription.GetText();
			PsState.m_activeGameLoop.m_minigameMetaData.template = this.m_saveAsTemplate != null && this.m_saveAsTemplate.m_enabled;
			PsState.m_activeGameLoop.m_minigameMetaData.complexity = PsState.m_activeMinigame.m_complexity;
			PsState.m_activeGameLoop.m_minigameMetaData.levelRequirement = PsState.m_activeMinigame.m_levelRequirement;
			PsState.m_activeGameLoop.m_minigameMetaData.playerUnit = PsState.m_activeMinigame.m_playerUnitName;
			PsState.m_activeGameLoop.m_minigameMetaData.itemsUsed = array;
			PsState.m_activeGameLoop.m_minigameMetaData.itemsCount = PsState.m_activeMinigame.m_itemCount;
			PsState.m_activeGameLoop.m_minigameMetaData.timeSpentEditing += PsState.m_activeMinigame.GetTimeSinceInit();
			PsState.m_activeGameLoop.m_minigameMetaData.timeSpentInEditMode += PsState.m_activeMinigame.GetEditTimeSinceInit();
			PsState.m_activeGameLoop.m_minigameMetaData.lastPlaySessionStartCount = PsState.m_activeMinigame.m_gameStartCount;
			PsState.m_activeGameLoop.m_minigameMetaData.editSessionCount += PsState.m_activeMinigame.m_editSessionCount;
			PsState.m_activeGameLoop.m_minigameMetaData.groundsModificationCount += PsState.m_activeMinigame.m_groundsModificationCount;
			PsState.m_activeGameLoop.m_minigameMetaData.itemsModificationCount += PsState.m_activeMinigame.m_itemsModificationCount;
			PsState.m_activeGameLoop.m_minigameMetaData.overrideCC = PsState.m_activeMinigame.m_overrideCC;
			PsState.m_activeMinigame.InitializeTimer();
			PsState.m_activeMinigame.ResetEditDateCounters();
			this.GetRoot().Destroy();
			if (PsState.m_activeMinigame.m_changed)
			{
				Debug.LogWarning("level changed, saving");
				Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorSaveState(false));
			}
			else
			{
				Debug.LogWarning("level not changed, just exit");
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLoadingScene(Color.black, true, 0.25f));
			}
		}
		base.Step();
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x000CEB9C File Offset: 0x000CCF9C
	private void NameDoneCallback(string _value)
	{
		Debug.Log("DONE: " + _value, null);
		this.m_nameField.SetValue(_value);
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x000CEBBB File Offset: 0x000CCFBB
	private void MissionDoneCallback(string _value)
	{
		Debug.Log("DONE: " + _value, null);
		this.m_missionField.SetValue(_value);
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x000CEBDA File Offset: 0x000CCFDA
	private void KeyPressedCallback(string _value)
	{
		Debug.Log("PRESSED: " + _value, null);
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x000CEBED File Offset: 0x000CCFED
	private void CancelCallback()
	{
		Debug.Log("CANCEL", null);
	}

	// Token: 0x0400170D RID: 5901
	private PsUIGenericButton m_saveButton;

	// Token: 0x0400170E RID: 5902
	private UITextArea m_nameField;

	// Token: 0x0400170F RID: 5903
	private UITextArea m_missionField;

	// Token: 0x04001710 RID: 5904
	private PsUILevelNameField m_levelName;

	// Token: 0x04001711 RID: 5905
	private PsUILevelDescriptionField m_levelDescription;

	// Token: 0x04001712 RID: 5906
	private new UIModel m_model;

	// Token: 0x04001713 RID: 5907
	public PsUISwitch m_saveAsTemplate;
}
