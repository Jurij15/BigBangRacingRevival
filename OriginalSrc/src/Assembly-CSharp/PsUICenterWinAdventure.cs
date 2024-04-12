using System;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class PsUICenterWinAdventure : PsUICenterStartAdventure
{
	// Token: 0x060015FC RID: 5628 RVA: 0x000D5B00 File Offset: 0x000D3F00
	public PsUICenterWinAdventure(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060015FD RID: 5629 RVA: 0x000D5B14 File Offset: 0x000D3F14
	protected virtual void FlyingReward(UIComponent _uicomponent)
	{
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x000D5B16 File Offset: 0x000D3F16
	protected virtual string GetCollectableName()
	{
		return PsStrings.Get(StringID.MAP_PIECES);
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x000D5B22 File Offset: 0x000D3F22
	protected virtual string GetCollactableFrame(int _index)
	{
		return "menu_debrief_adventure_map_piece" + (_index + 1);
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x000D5B38 File Offset: 0x000D3F38
	public override void CreateGoButton(UIComponent _parent)
	{
		if (PsState.m_activeGameLoop.m_timeScoreBest < 2147483647 || PsState.m_activeMinigame.m_playerReachedGoal || (PsState.m_activeGameLoop.m_path != null && PsState.m_activeGameLoop.m_nodeId < PsState.m_activeGameLoop.m_path.m_currentNodeId))
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
		if (PsState.m_activeGameLoop.m_scoreBest < 3)
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
		if (PsState.m_activeMinigame.m_gameEnded || PsState.m_activeGameLoop.m_scoreBest > 0)
		{
			this.m_go.SetFittedText(PsStrings.Get(StringID.PLAY_AGAIN), 0.04f, 0.3f, RelativeTo.ScreenHeight, true);
		}
		else
		{
			this.m_go.SetFittedText("Play!", 0.04f, 0.2f, RelativeTo.ScreenHeight, true);
		}
		this.m_go.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_go.SetHorizontalAlign(1f);
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x000D5D8C File Offset: 0x000D418C
	public override void CreateCenterContent()
	{
		if (this.m_showRentStatsButtonArea != null)
		{
			this.m_showRentStatsButtonArea.Destroy();
		}
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetAlign(0.5f, 0.65f);
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		int num = 3;
		string text = PsStrings.Get(StringID.NUMBER_OF);
		text = text.Replace("%1", PsState.m_activeGameLoop.m_scoreBest.ToString());
		text = text.Replace("%2", num.ToString());
		text = text.Replace("%3", this.GetCollectableName());
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, "#C2FF16", "313131");
		uitext.SetShadowShift(new Vector2(0.5f, -0.15f), 0.1f);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList2.RemoveDrawHandler();
		for (int i = 0; i < 3; i++)
		{
			string collactableFrame = this.GetCollactableFrame(i);
			UIFittedSprite map = new UIFittedSprite(uihorizontalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(collactableFrame, null), true, true);
			map.SetHeight(0.24f, RelativeTo.ScreenHeight);
			map.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			if (i > PsState.m_activeGameLoop.m_rewardOld - 1)
			{
				map.SetColor(DebugDraw.HexToColor("#535E6A"));
			}
			if (PsState.m_activeGameLoop.m_scoreBest - 1 >= i)
			{
				TweenC tweenC = TweenS.AddTransformTween(map.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.one * 1.25f, 0.3f, 1f + (float)i * 0.5f, true);
				TweenS.SetAdditionalTweenProperties(tweenC, 0, true, TweenStyle.CubicInOut);
				int reward = i + 1;
				bool sparks = reward > PsState.m_activeGameLoop.m_rewardOld;
				TimerS.AddComponent(map.m_TC.p_entity, string.Empty, 0.3f, 1f + (float)i * 0.5f, false, delegate(TimerC c)
				{
					TimerS.RemoveComponent(c);
					map.SetColor(Color.white);
					if (sparks)
					{
						this.FlyingReward(map);
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
					if (this.GetCollactableFrame(0) == "hud_big_diamond_top")
					{
						SoundS.PlaySingleShotWithParameter("/Ingame/Events/GameEnd_DiamondGain", Vector3.zero, "Result", (float)reward, 1f);
					}
					else
					{
						SoundS.PlaySingleShotWithParameter("/Ingame/Events/GameEnd_StarGain", Vector3.zero, "Result", (float)reward, 1f);
					}
				});
			}
		}
	}

	// Token: 0x06001602 RID: 5634 RVA: 0x000D6020 File Offset: 0x000D4420
	private void FarRightHandler(TweenC _c)
	{
		TweenC tweenC = TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Rotation, TweenStyle.QuadOut, new Vector3(0f, 0f, 360f), 0.4f, 0f, true);
		TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Scale, TweenStyle.QuadOut, new Vector3(1.1f, 1.1f, 1.1f), Vector3.one, 0.4f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.MiddleToLeft));
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x000D60A0 File Offset: 0x000D44A0
	private void MiddleToLeft(TweenC _c)
	{
		TweenC tweenC = TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Rotation, TweenStyle.QuadIn, new Vector3(0f, 0f, 1.5f), 0.4f, 0f, true);
		TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Scale, TweenStyle.QuadIn, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f), 0.4f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.FarLeftHandler));
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x000D6120 File Offset: 0x000D4520
	private void FarLeftHandler(TweenC _c)
	{
		TweenC tweenC = TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Rotation, TweenStyle.QuadOut, new Vector3(0f, 0f, 0f), 0.4f, 0f, true);
		TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Scale, TweenStyle.QuadOut, new Vector3(1.1f, 1.1f, 1.1f), Vector3.one, 0.4f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.MiddleToRight));
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x000D61A0 File Offset: 0x000D45A0
	private void MiddleToRight(TweenC _c)
	{
		TweenC tweenC = TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Rotation, TweenStyle.QuadIn, new Vector3(0f, 0f, -1.5f), 0.4f, 0f, true);
		TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Scale, TweenStyle.QuadIn, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f), 0.4f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.FarRightHandler));
	}

	// Token: 0x06001606 RID: 5638 RVA: 0x000D6220 File Offset: 0x000D4620
	public PsUIProfileImage CreateProfileImage(UIComponent _parent, bool _own, float _xSize, float _ySize)
	{
		PsGameLoopRacing psGameLoopRacing = PsState.m_activeGameLoop as PsGameLoopRacing;
		PsGameModeRacing psGameModeRacing = psGameLoopRacing.m_gameMode as PsGameModeRacing;
		string text = PlayerPrefsX.GetUserName();
		string text2 = PlayerPrefsX.GetFacebookId();
		string text3 = PlayerPrefsX.GetGameCenterId();
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetCharacterCustomisationData().GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		string text4 = null;
		if (installedItemByCategory != null)
		{
			text4 = installedItemByCategory.m_identifier;
		}
		if (!_own)
		{
			text = psGameModeRacing.m_trophyGhost.name;
			text2 = psGameModeRacing.m_trophyGhost.facebookId;
			text3 = psGameModeRacing.m_trophyGhost.gameCenterId;
			this.m_opponentName = text;
			if (psGameModeRacing.m_playbackGhosts != null && psGameModeRacing.m_playbackGhosts.Count > 0)
			{
				text4 = ((psGameModeRacing.m_playbackGhosts[0].m_ghost.m_characterVisualItems == null || psGameModeRacing.m_playbackGhosts[0].m_ghost.m_characterVisualItems.Count <= 0) ? "MotocrossHelmet" : psGameModeRacing.m_playbackGhosts[0].m_ghost.m_characterVisualItems[0]);
			}
		}
		bool flag = false;
		string empty = string.Empty;
		string text5 = text2;
		string text6 = text3;
		string text7 = text4;
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(_parent, flag, empty, text5, text6, -1, true, false, true, 0.1f, 0.06f, "fff9e6", text7, false, true);
		psUIProfileImage.SetSize(_xSize, _ySize, RelativeTo.ScreenHeight);
		return psUIProfileImage;
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x000D6378 File Offset: 0x000D4778
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

	// Token: 0x06001608 RID: 5640 RVA: 0x000D647C File Offset: 0x000D487C
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

	// Token: 0x040018DC RID: 6364
	private string m_opponentName = string.Empty;
}
