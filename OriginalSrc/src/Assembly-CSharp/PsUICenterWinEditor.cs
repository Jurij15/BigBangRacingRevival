using System;
using UnityEngine;

// Token: 0x020002A4 RID: 676
public class PsUICenterWinEditor : UICanvas
{
	// Token: 0x06001456 RID: 5206 RVA: 0x000CF1DC File Offset: 0x000CD5DC
	public PsUICenterWinEditor(UIComponent _parent)
		: base(_parent, false, "CenterContent", null, string.Empty)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DebriefBackground));
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0f);
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "RightTop");
		uihorizontalList.SetAlign(1f, 1f);
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.RemoveDrawHandler();
		this.m_restartButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
		this.m_restartButton.SetMargins(0.035f, 0.035f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		if (PsState.m_activeGameLoop.m_gameMode is PsGameModeAdventure)
		{
			this.CreateAdventureMiddleArea(this);
		}
		else if (PsState.m_activeGameLoop.m_gameMode is PsGameModeRace)
		{
			this.CreateRaceMiddleArea(this);
		}
		UIVerticalList uiverticalList = new UIVerticalList(this, "Center");
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetVerticalAlign(0f);
		uiverticalList.SetMargins(0f, 0f, 0f, 0.05f, RelativeTo.ScreenHeight);
		bool flag = PsState.m_activeGameLoop.m_scoreCurrent == 3;
		string text = PsStrings.Get(StringID.GET_MAP_PIECES);
		if (flag)
		{
			text = PsStrings.Get(StringID.EDITOR_GO_LIVE_TEXT);
		}
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetWidth(0.55f, RelativeTo.ScreenHeight);
		uitextbox.SetHeight(0.15f, RelativeTo.ScreenHeight);
		this.m_publishButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_publishButton.SetFittedText(PsStrings.Get(StringID.EDITOR_BUTTON_GO_LIVE).ToUpper(), 0.07f, 0.25f, RelativeTo.ScreenWidth, false);
		this.m_publishButton.SetIcon("hud_icon_header_publish", 0.08f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_publishButton.SetBlueColors(true);
		if (!flag)
		{
			this.m_publishButton.RemoveTouchAreas();
			this.m_publishButton.SetGrayColors();
		}
		UIVerticalList uiverticalList2 = new UIVerticalList(this, "Center");
		uiverticalList2.SetSpacing(0.03f, RelativeTo.ScreenShortest);
		uiverticalList2.SetAlign(1f, 0f);
		uiverticalList2.SetMargins(0f, 0.05f, 0f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList2.SetWidth(0.2f, RelativeTo.ScreenWidth);
		uiverticalList2.RemoveDrawHandler();
		this.m_editButton = new PsUIGenericButton(uiverticalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_editButton.SetText(PsStrings.Get(StringID.EDITOR_BUTTON_KEEP_EDITING).ToUpper(), 0.036f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_editButton.SetMargins(0.04f, 0.04f, 0.007f, 0.007f, RelativeTo.ScreenHeight);
		this.m_editButton.SetOrangeColors(true);
		this.m_editButton.SetHorizontalAlign(1f);
		this.m_saveButton = new PsUIGenericButton(uiverticalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_saveButton.SetText(PsStrings.Get(StringID.EDITOR_BUTTON_SAVE_EXIT), 0.036f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_saveButton.SetSpacing(0f, RelativeTo.ScreenHeight);
		this.m_saveButton.SetOrangeColors(true);
		this.m_saveButton.SetHorizontalAlign(1f);
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x000CF5CC File Offset: 0x000CD9CC
	protected virtual void CreateAdventureMiddleArea(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		for (int i = 0; i < 3; i++)
		{
			string text = "menu_debrief_adventure_map_piece" + (i + 1);
			UIFittedSprite map = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
			map.SetHeight(0.3f, RelativeTo.ScreenHeight);
			map.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			map.SetColor(DebugDraw.HexToColor("#535E6A"));
			if (PsState.m_activeGameLoop.m_scoreCurrent - 1 >= i)
			{
				float num = 0.1575f;
				TweenC tweenC = TweenS.AddTransformTween(map.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.one * 1.25f, 0.3f, num + (float)i * 0.5f, true);
				TweenS.SetAdditionalTweenProperties(tweenC, 0, true, TweenStyle.CubicInOut);
				int reward = i + 1;
				bool sparks = reward > PsState.m_activeGameLoop.m_rewardOld;
				TimerS.AddComponent(map.m_TC.p_entity, string.Empty, 0.3f, num + (float)i * 0.5f, false, delegate(TimerC c)
				{
					TimerS.RemoveComponent(c);
					map.SetColor(Color.white);
					if (sparks)
					{
						PrefabC prefabC = PrefabS.AddComponent(map.m_TC, Vector3.zero, ResourceManager.GetGameObject("StarReward" + reward + "_GameObject"));
						prefabC.p_gameObject.transform.position -= new Vector3(0f, 0f, 250f);
						prefabC.p_gameObject.transform.Rotate(90f, 0f, 0f);
						PrefabS.SetCamera(prefabC, this.m_camera);
						ParticleSystem[] array = prefabC.p_gameObject.GetComponents<ParticleSystem>();
						for (int j = 0; j < array.Length; j++)
						{
							array[j].Play();
						}
						array = prefabC.p_gameObject.GetComponentsInChildren<ParticleSystem>();
						for (int k = 0; k < array.Length; k++)
						{
							array[k].Play();
						}
					}
					SoundS.PlaySingleShotWithParameter("/Ingame/Events/GameEnd_StarGain", Vector3.zero, "Result", (float)reward, 1f);
				});
			}
		}
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x000CF748 File Offset: 0x000CDB48
	protected virtual void CreateStarCollectMiddleArea(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.SetHeight(0.42f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(-0.07f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		string text = "hud_big_star_top";
		string text2 = "hud_big_star_bottom";
		UIFittedSprite m_leftSlot = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text2, null), true, true);
		m_leftSlot.SetHeight(0.35f, RelativeTo.ScreenHeight);
		m_leftSlot.SetVerticalAlign(0f);
		m_leftSlot.SetDepthOffset(3f);
		if (PsState.m_activeGameLoop.m_scoreCurrent > 0)
		{
			UIFittedSprite uifittedSprite = new UIFittedSprite(m_leftSlot, false, "LeftStar", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
			uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
			TweenS.AddTransformTween(uifittedSprite.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, this.m_starAnimDuration, 0f, true);
			TimerS.AddComponent(m_leftSlot.m_TC.p_entity, "EffectTimer", 0f, this.m_starEffectDelay, false, delegate(TimerC _c)
			{
				this.CreateStarEffect(m_leftSlot.m_TC, 1);
				TimerS.RemoveComponent(_c);
			});
		}
		UIFittedSprite m_centerSlot = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text2, null), true, true);
		m_centerSlot.SetHeight(0.35f, RelativeTo.ScreenHeight);
		m_centerSlot.SetVerticalAlign(1f);
		if (PsState.m_activeGameLoop.m_scoreCurrent > 1)
		{
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(m_centerSlot, false, "CenterStar", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
			uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
			TweenS.AddTransformTween(uifittedSprite2.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, this.m_starAnimDuration, this.m_starAnimDuration, true);
			uifittedSprite2.SetDepthOffset(-1f);
			TimerS.AddComponent(m_centerSlot.m_TC.p_entity, "EffectTimer", 0f, this.m_starAnimDuration + this.m_starEffectDelay, false, delegate(TimerC _c)
			{
				this.CreateStarEffect(m_centerSlot.m_TC, 2);
				TimerS.RemoveComponent(_c);
			});
		}
		UIFittedSprite m_rightSlot = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text2, null), true, true);
		m_rightSlot.SetHeight(0.35f, RelativeTo.ScreenHeight);
		m_rightSlot.SetVerticalAlign(0f);
		m_rightSlot.SetDepthOffset(3f);
		if (PsState.m_activeGameLoop.m_scoreCurrent > 2)
		{
			UIFittedSprite uifittedSprite3 = new UIFittedSprite(m_rightSlot, false, "RightStar", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
			uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
			TweenS.AddTransformTween(uifittedSprite3.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, this.m_starAnimDuration, this.m_starAnimDuration * 2f, true);
			TimerS.AddComponent(m_rightSlot.m_TC.p_entity, "EffectTimer", 0f, this.m_starAnimDuration * 2f + this.m_starEffectDelay, false, delegate(TimerC _c)
			{
				this.CreateStarEffect(m_rightSlot.m_TC, 3);
				TimerS.RemoveComponent(_c);
			});
		}
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x000CFAA4 File Offset: 0x000CDEA4
	protected virtual void CreateRaceMiddleArea(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetSpacing(-0.02f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.EDITOR_YOUR_TIME), PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, "#ffffff", "#121313");
		UIText uitext2 = uitext;
		Vector2 vector;
		vector..ctor(1f, -1f);
		uitext2.SetShadowShift(vector.normalized, 0.035f);
		UIText uitext3 = new UIText(uiverticalList, false, string.Empty, HighScores.TicksToTimeString(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks)), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.11f, RelativeTo.ScreenHeight, "#2ce768", "#121313");
		UIText uitext4 = uitext3;
		Vector2 vector2;
		vector2..ctor(1f, -1f);
		uitext4.SetShadowShift(vector2.normalized, 0.035f);
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x000CFB7C File Offset: 0x000CDF7C
	protected virtual void CreateStarEffect(TransformC _tc, int _starEffectNumber)
	{
		PrefabC prefabC = PrefabS.AddComponent(_tc, Vector3.zero, ResourceManager.GetGameObject("StarReward" + _starEffectNumber + "_GameObject"));
		prefabC.p_gameObject.transform.position -= new Vector3(0f, 0f, 250f);
		prefabC.p_gameObject.transform.Rotate(90f, 0f, 0f);
		PrefabS.SetCamera(prefabC, this.m_camera);
		ParticleSystem[] array = prefabC.p_gameObject.GetComponents<ParticleSystem>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Play();
		}
		array = prefabC.p_gameObject.GetComponentsInChildren<ParticleSystem>();
		for (int j = 0; j < array.Length; j++)
		{
			array[j].Play();
		}
		SoundS.PlaySingleShotWithParameter("/Ingame/Events/GameEnd_StarGain", Vector3.zero, "Result", (float)_starEffectNumber, 1f);
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x000CFC74 File Offset: 0x000CE074
	public override void Step()
	{
		if (this.m_restartButton.m_hit)
		{
			PsIngameMenu.CloseAll();
			PsState.m_activeGameLoop.RestartMinigame();
		}
		else if (this.m_editButton.m_hit)
		{
			PsIngameMenu.CloseAll();
			PsState.m_activeGameLoop.ExitMinigame();
		}
		else if (this.m_saveButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid))
		{
			if (ScreenCapture.isRecording)
			{
				PsUIBasePopup popup2 = new PsUIBasePopup(typeof(PsUIRecordingPopup), null, null, null, true, true, InitialPage.Center, false, true, false);
				popup2.SetAction("Close", new Action(popup2.Destroy));
				popup2.SetAction("Preview", new Action(popup2.Destroy));
				popup2.SetAction("Discard", delegate
				{
					popup2.Destroy();
					PsState.m_activeGameLoop.ExitEditor(false, null);
				});
			}
			else
			{
				PsState.m_activeGameLoop.ExitEditor(false, null);
			}
		}
		else if (this.m_publishButton.m_hit)
		{
			if (ScreenCapture.isRecording)
			{
				PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUIRecordingPopup), null, null, null, true, true, InitialPage.Center, false, true, false);
				popup.SetAction("Close", new Action(popup.Destroy));
				popup.SetAction("Preview", new Action(popup.Destroy));
				popup.SetAction("Discard", delegate
				{
					popup.Destroy();
					PsUIBasePopup subPopup2 = new PsUIBasePopup(typeof(PsUICenterEditorPublish), null, null, null, true, true, InitialPage.Center, false, false, false);
					subPopup2.SetAction("Exit", delegate
					{
						subPopup2.Destroy();
					});
				});
			}
			else
			{
				PsUIBasePopup subPopup = new PsUIBasePopup(typeof(PsUICenterEditorPublish), null, null, null, true, true, InitialPage.Center, false, false, false);
				subPopup.SetAction("Exit", delegate
				{
					subPopup.Destroy();
				});
			}
		}
		base.Step();
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x000CFE80 File Offset: 0x000CE280
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x04001722 RID: 5922
	private PsUIGenericButton m_restartButton;

	// Token: 0x04001723 RID: 5923
	private PsUIGenericButton m_editButton;

	// Token: 0x04001724 RID: 5924
	private PsUIGenericButton m_saveButton;

	// Token: 0x04001725 RID: 5925
	private PsUIGenericButton m_publishButton;

	// Token: 0x04001726 RID: 5926
	protected float m_starAnimDuration = 0.4f;

	// Token: 0x04001727 RID: 5927
	protected float m_starEffectDelay = 0.1f;
}
