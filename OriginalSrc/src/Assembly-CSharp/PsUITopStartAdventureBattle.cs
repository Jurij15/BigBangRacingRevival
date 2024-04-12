using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class PsUITopStartAdventureBattle : UICanvas
{
	// Token: 0x060015B6 RID: 5558 RVA: 0x000E192C File Offset: 0x000DFD2C
	public PsUITopStartAdventureBattle(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.SetHeight(0.1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetAlign(0.5f, 1f);
		this.SetDrawHandler(new UIDrawDelegate(this.TopDrawhandler));
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetIcon("hud_icon_menu_exit", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetSound("/UI/ExitLevel");
		this.m_exitButton.SetOrangeColors(true);
		this.m_exitButton.SetDepthOffset(-5f);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null);
		this.m_infoButton = new UIRectSpriteButton(uihorizontalList, string.Empty, PsState.m_uiSheet, frame, true, false);
		this.m_infoButton.SetHeight(0.1f, RelativeTo.ScreenHeight);
		this.m_infoButton.SetWidth(0.1f * (frame.width / frame.height), RelativeTo.ScreenHeight);
		this.m_infoButton.SetHorizontalAlign(0.2f);
		PsGameModeBase gameMode = PsState.m_activeGameLoop.m_gameMode;
		string text = "???";
		if (gameMode.m_playbackGhosts != null && gameMode.m_playbackGhosts.Count > 0)
		{
			if (gameMode.m_playbackGhosts[0].m_ghost.m_vehicleUpgradeItems != null)
			{
				PsUpgradeData customUpgradeData = PsUpgradeManager.GetCustomUpgradeData(gameMode.m_playbackGhosts[0].m_ghost.m_vehicleUpgradeItems, Type.GetType(gameMode.m_playbackGhosts[0].m_ghost.m_unitClass));
				text = ((int)(customUpgradeData.m_basePerformance + customUpgradeData.m_currentPerformance)).ToString();
			}
			else if (gameMode.m_playbackGhosts[0].m_ghost.m_upgradeValues != null)
			{
				List<float> list = PsState.m_activeMinigame.m_playerUnit.ParseUpgradeValues(PsState.m_activeGameLoop.m_gameMode.m_playbackGhosts[0].m_ghost.m_upgradeValues);
				float num = PsUpgradeManager.GetMaxPerformance(PsState.m_activeMinigame.m_playerUnit.GetType()) - PsUpgradeManager.GetBasePerformance(PsState.m_activeMinigame.m_playerUnit.GetType());
				float basePerformance = PsUpgradeManager.GetBasePerformance(PsState.m_activeMinigame.m_playerUnit.GetType());
				float num2 = num / 3f * list[0];
				float num3 = num / 3f * list[1];
				float num4 = num / 3f * list[2];
				text = ((float)((int)num2 + (int)num3 + (int)num4 + (int)basePerformance)).ToString();
			}
		}
		PsUISkullRiderHeader psUISkullRiderHeader = new PsUISkullRiderHeader(this, text);
		psUISkullRiderHeader.SetVerticalAlign(0.9f);
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x000E1C4C File Offset: 0x000E004C
	public void TopDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, 0.06f * (float)Screen.height, new Vector2(0f, 0.02f * (float)Screen.height), true);
		Color black = Color.black;
		Color black2 = Color.black;
		Color black3 = Color.black;
		black3.a = 0.5f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, rect, (float)Screen.height * 0.0075f, black, black2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, rect, (float)Screen.height * 0.015f, black3, black3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, black, black2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x000E1D5C File Offset: 0x000E015C
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		else if (this.m_infoButton != null && this.m_infoButton.m_hit)
		{
			this.ShowInfoContent();
		}
		base.Step();
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x000E1E10 File Offset: 0x000E0210
	public void ShowInfoContent()
	{
		EntityManager.SetActivityOfEntitiesWithTag("UIComponent", false, true, true, true, false, false);
		PsDialogue psDialogue = new PsDialogue("manual", PsNodeEventTrigger.Manual);
		psDialogue.AddStep(PsDialogueCharacter.Mechanic, PsDialogueCharacterPosition.Left, StringID.BOSS_BATTLE_HELPTEXT, StringID.OK);
		PsMinigameDialogueFlow psMinigameDialogueFlow = new PsMinigameDialogueFlow(psDialogue, 0f, new Action(this.InfoClosed), null);
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x000E1E62 File Offset: 0x000E0262
	private void InfoClosed()
	{
		EntityManager.SetActivityOfEntitiesWithTag("UIComponent", true, true, true, true, false, false);
	}

	// Token: 0x04001867 RID: 6247
	private PsUIGenericButton m_exitButton;

	// Token: 0x04001868 RID: 6248
	protected UIRectSpriteButton m_infoButton;
}
