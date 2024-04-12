using System;
using UnityEngine;

// Token: 0x020002AF RID: 687
public class PsUICenterLoseAdventure : PsUICenterStartAdventure
{
	// Token: 0x060014A2 RID: 5282 RVA: 0x000D54F7 File Offset: 0x000D38F7
	public PsUICenterLoseAdventure(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x000D550B File Offset: 0x000D390B
	protected virtual string GetCollectableName()
	{
		return PsStrings.Get(StringID.MAP_PIECES);
	}

	// Token: 0x060014A4 RID: 5284 RVA: 0x000D5517 File Offset: 0x000D3917
	protected virtual string GetCollactableFrame(int _index)
	{
		return "menu_debrief_adventure_map_piece" + (_index + 1);
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x000D552C File Offset: 0x000D392C
	public override void CreateCenterContent()
	{
		if (this.m_showRentStatsButtonArea != null)
		{
			this.m_showRentStatsButtonArea.Destroy();
		}
		this.m_scoreArea = new UIVerticalList(this, string.Empty);
		this.m_scoreArea.SetAlign(0.5f, 0.8f);
		this.m_scoreArea.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		this.m_scoreArea.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_scoreArea, string.Empty);
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		int num = 3;
		string text = PsStrings.Get(StringID.NUMBER_OF);
		text = text.Replace("%1", PsState.m_activeGameLoop.m_scoreBest.ToString());
		text = text.Replace("%2", num.ToString());
		text = text.Replace("%3", this.GetCollectableName());
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, "#C2FF16", "313131");
		uitext.SetShadowShift(new Vector2(0.5f, -0.15f), 0.1f);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(this.m_scoreArea, string.Empty);
		uihorizontalList2.RemoveDrawHandler();
		for (int i = 0; i < 3; i++)
		{
			string collactableFrame = this.GetCollactableFrame(i);
			UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(collactableFrame, null), false, true);
			uifittedSprite.SetHeight(0.24f, RelativeTo.ScreenHeight);
			uifittedSprite.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			if (i > PsState.m_activeGameLoop.m_rewardOld - 1)
			{
				uifittedSprite.SetColor(DebugDraw.HexToColor("#535E6A"));
			}
		}
		TweenS.AddTransformTween(this.m_scoreArea.m_TC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.zero, Vector3.zero, 0.1f, 0f, true);
		TweenC tweenC = TweenS.AddTransformTween(this.m_scoreArea.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.zero, Vector3.one, 0.3f, 0.5f, true);
		tweenC.useUnscaledDeltaTime = true;
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x000D5748 File Offset: 0x000D3B48
	public override void CreateGoButton(UIComponent _parent)
	{
		if ((PsState.m_activeGameLoop.m_timeScoreBest < 2147483647 || PsState.m_activeMinigame.m_playerReachedGoal) && PsState.m_activeGameLoop.m_path != null && PsState.m_activeGameLoop.m_nodeId == PsState.m_activeGameLoop.m_path.m_currentNodeId)
		{
			if (PsState.m_activeGameLoop.m_scoreBest >= 3)
			{
				this.m_nextRace = new PsUIAttentionButton(_parent, Vector3.one * 1.15f, 0.25f, 0.25f, 0.01f);
				this.m_nextRace.SetDepthOffset(-20f);
			}
			else
			{
				this.m_nextRace = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.01f, "Button");
			}
			this.m_nextRace.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			this.m_nextRace.SetGreenColors(true);
			this.m_nextRace.SetFittedText(PsStrings.Get(StringID.CONTINUE), 0.04f, 0.3f, RelativeTo.ScreenHeight, true);
			this.m_nextRace.SetHeight(0.12f, RelativeTo.ScreenHeight);
			this.m_nextRace.SetHorizontalAlign(1f);
		}
		if (PsState.m_activeGameLoop.m_scoreBest < 3 && !this.m_hasCoinRouletteButton)
		{
			this.m_go = new PsUIAttentionButton(_parent, Vector3.one * 1.15f, 0.25f, 0.25f, 0.01f);
			this.m_go.SetDepthOffset(-20f);
		}
		else
		{
			this.m_go = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.01f, "Button");
		}
		this.m_go.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_go.SetOrangeColors(true);
		this.m_go.SetFittedText(PsStrings.Get(StringID.TRY_AGAIN), 0.04f, 0.3f, RelativeTo.ScreenHeight, true);
		this.m_go.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_go.SetHorizontalAlign(1f);
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x000D5964 File Offset: 0x000D3D64
	public new void BottomDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, true);
		Color color = DebugDraw.HexToColor("##083f7c");
		Color color2 = DebugDraw.HexToColor("##004b9f");
		Color black = Color.black;
		black.a = 0.5f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, rect, (float)Screen.height * 0.0075f, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, rect, (float)Screen.height * 0.015f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x000D5A68 File Offset: 0x000D3E68
	public override void Step()
	{
		if (GameScene.m_lowPerformance && !PlayerPrefsX.GetLowEndPrompt() && this.m_lowEndPrompt == null && !this.m_lowEndShown)
		{
			this.m_lowEndPrompt = new PsUIBasePopup(typeof(PsUICenterLowPerformancePrompt), null, null, null, false, true, InitialPage.Center, true, false, false);
			this.m_lowEndPrompt.SetAction("Exit", delegate
			{
				PlayerPrefsX.SetLowEndPrompt(true);
				this.m_lowEndPrompt.Destroy();
				this.m_lowEndPrompt = null;
			});
			this.m_lowEndShown = true;
		}
		base.Step();
	}

	// Token: 0x04001781 RID: 6017
	public UIVerticalList m_scoreArea;

	// Token: 0x04001782 RID: 6018
	private string m_opponentName = string.Empty;
}
