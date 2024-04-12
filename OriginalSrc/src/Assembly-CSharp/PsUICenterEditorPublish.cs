using System;
using UnityEngine;

// Token: 0x020002A0 RID: 672
public class PsUICenterEditorPublish : PsUIHeaderedCanvas
{
	// Token: 0x06001444 RID: 5188 RVA: 0x000CDB00 File Offset: 0x000CBF00
	public PsUICenterEditorPublish(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
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

	// Token: 0x06001445 RID: 5189 RVA: 0x000CDC18 File Offset: 0x000CC018
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.EDITOR_PUBLISH_HEADER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
		UICanvas uicanvas = new UICanvas(uitext, false, string.Empty, null, string.Empty);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetMargins(-1.5f, 0f, -0.125f, -0.125f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("hud_icon_header_publish", null);
		UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true);
		uisprite.SetSize(1f, 1f * (frame.height / frame.width), RelativeTo.ParentHeight);
		uisprite.SetHorizontalAlign(0f);
		uisprite.SetColor(DebugDraw.HexToColor("#95e5ff") * Color.gray);
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x000CDD48 File Offset: 0x000CC148
	public void CreateContent(UIComponent _parent)
	{
		PsState.m_activeMinigame.GenerateDefaultNameAndDescription();
		this.SetMargins(0.03f, 0.03f, 0f, 0.05f, RelativeTo.ScreenHeight);
		this.SetHeight(0.65f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.4f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas.SetMargins(0.03f, 0.03f, 0.07f, 0.03f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetAlign(0f, 1f);
		this.m_screenshotSelector = new UIEditorPublishScreenshotSelect(uicanvas, "Screenshot area");
		this.m_screenshotSelector.SetVerticalAlign(1f);
		UICanvas uicanvas2 = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(0.6f, RelativeTo.ParentWidth);
		uicanvas2.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas2.SetMargins(0.03f, 0.03f, 0.07f, 0.03f, RelativeTo.ScreenHeight);
		uicanvas2.SetAlign(1f, 1f);
		uicanvas2.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(uicanvas2, "vList");
		uiverticalList.SetSpacing(0.085f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.RemoveDrawHandler();
		this.m_levelName = new PsUILevelNameField(uiverticalList);
		this.m_levelName.SetText(Minigame.GetRandomName());
		UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList2.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList2.SetSpacing(0f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.PUBLISH_DISCLAIMER), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, true, Align.Left, Align.Top, null, true, null);
		uitextbox.SetHorizontalAlign(0f);
		this.m_tos = new PsUITermsOfServiceLink(uiverticalList2);
		this.m_tos.SetHorizontalAlign(0f);
		this.m_goLiveButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		this.m_goLiveButton.SetDepthOffset(-4f);
		this.m_goLiveButton.SetText(PsStrings.Get(StringID.EDITOR_BUTTON_GO_LIVE).ToUpper(), 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_goLiveButton.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_goLiveButton.SetIcon("hud_icon_header_publish", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_goLiveButton.SetGreenColors(true);
		this.m_goLiveButton.SetAlign(0.85f, -0.25f);
		if (PsState.m_adminMode)
		{
			UIVerticalList uiverticalList3 = new UIVerticalList(this, string.Empty);
			uiverticalList3.SetSpacing(0.01f, RelativeTo.ScreenHeight);
			uiverticalList3.SetAlign(0.5f, -0.25f);
			uiverticalList3.RemoveDrawHandler();
			UIText uitext = new UIText(uiverticalList3, false, string.Empty, "Publish Hidden", PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, null, null);
			uitext.SetColor("#a8e2ff", null);
			this.m_publishHidden = new PsUISwitch(uiverticalList3, true, 0.03f, "On", "Off", 0.05f, null);
		}
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x000CE068 File Offset: 0x000CC468
	private void Underline(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] line = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f), new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f), 0);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, line, 0.003f * (float)Screen.height, DebugDraw.HexToColor("38a6ea"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line16Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x000CE104 File Offset: 0x000CC504
	public override void Step()
	{
		if (this.m_goLiveButton.m_hit)
		{
			PsIngameMenu.CloseAll();
			EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
			(LevelManager.m_currentLevel as Minigame).m_groundNode.RevertGroundFromPlay();
			PsState.m_activeGameLoop.m_minigameMetaData.creatorUpgrades = (PsState.m_activeMinigame.m_playerUnit as Vehicle).GetActualPerformanceValues();
			Debug.LogWarning("PUBLISH UPGRADES: " + PsState.m_activeGameLoop.m_minigameMetaData.creatorUpgrades);
			string[] array = new string[PsState.m_activeMinigame.m_itemCount.Keys.Count];
			int num = 0;
			foreach (string text in PsState.m_activeMinigame.m_itemCount.Keys)
			{
				array[num] = text;
				num++;
			}
			PsState.m_activeGameLoop.m_minigameMetaData.name = this.m_levelName.GetText();
			PsState.m_activeGameLoop.m_minigameMetaData.description = PsState.m_activeGameLoop.m_minigameMetaData.description;
			PsState.m_activeGameLoop.m_minigameMetaData.hidden = this.m_publishHidden != null && this.m_publishHidden.m_enabled;
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
			PsState.m_activeMinigame.InitializeTimer();
			PsState.m_activeMinigame.ResetEditDateCounters();
			Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorPublishState());
			this.DestroyRoot();
		}
		else if (this.m_tos.m_hit)
		{
			Application.OpenURL("http://www.traplightgames.com/terms/");
		}
		base.Step();
	}

	// Token: 0x04001703 RID: 5891
	private new UIModel m_model;

	// Token: 0x04001704 RID: 5892
	private PsUIGenericButton m_goLiveButton;

	// Token: 0x04001705 RID: 5893
	private UIEditorPublishScreenshotSelect m_screenshotSelector;

	// Token: 0x04001706 RID: 5894
	private PsUILevelNameField m_levelName;

	// Token: 0x04001707 RID: 5895
	private PsUILevelDescriptionField m_levelDescription;

	// Token: 0x04001708 RID: 5896
	private PsUITermsOfServiceLink m_tos;

	// Token: 0x04001709 RID: 5897
	public PsUISwitch m_publishHidden;
}
