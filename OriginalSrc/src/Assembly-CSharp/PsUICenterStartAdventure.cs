using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class PsUICenterStartAdventure : UICanvas
{
	// Token: 0x06001506 RID: 5382 RVA: 0x000D3DF8 File Offset: 0x000D21F8
	public PsUICenterStartAdventure(UIComponent _parent)
		: base(_parent, false, "CenterRacing", null, string.Empty)
	{
		if (this.m_buttonTimerEntity == null)
		{
			this.m_buttonTimerEntity = EntityManager.AddEntity();
		}
		this.RemoveDrawHandler();
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetMargins(0.025f, RelativeTo.ScreenHeight);
		this.SetAlign(0.5f, 0f);
		this.m_lowerLeft = new UIVerticalList(this, "lowerLeft");
		this.m_lowerLeft.SetSpacing(0.005f, RelativeTo.ScreenHeight);
		this.m_lowerLeft.SetAlign(0f, 0f);
		this.m_lowerLeft.RemoveDrawHandler();
		if (!GameScene.m_lowPerformance || PlayerPrefsX.GetLowEndPrompt())
		{
			this.CreateLowerLeftContent();
		}
		else
		{
			this.m_createContents = true;
		}
		this.m_lowerCenter = new UIVerticalList(this, "lowerCenter");
		this.m_lowerCenter.SetAlign(0.5f, 0f);
		this.m_lowerCenter.RemoveDrawHandler();
		if (!GameScene.m_lowPerformance || PlayerPrefsX.GetLowEndPrompt())
		{
			this.CreateLowerCenterContent();
		}
		this.m_lowerRight = new UIHorizontalList(this, "lowerRight");
		this.m_lowerRight.SetAlign(1f, 0f);
		this.m_lowerRight.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_lowerRight.RemoveDrawHandler();
		if (!GameScene.m_lowPerformance || PlayerPrefsX.GetLowEndPrompt())
		{
			this.CreateLowerRightContent();
		}
		this.CreateCenterContent();
		this.m_bottomBanner = new UICanvas(null, false, string.Empty, null, string.Empty);
		this.m_bottomBanner.SetCamera(this.m_camera, true, false);
		this.m_bottomBanner.SetDepthOffset(20f);
		this.m_bottomBanner.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_bottomBanner.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_bottomBanner.SetDrawHandler(new UIDrawDelegate(this.BottomDrawhandler));
		this.m_bottomBanner.SetAlign(0.5f, 0f);
		this.m_bottomBanner.Update();
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x000D4014 File Offset: 0x000D2414
	public virtual void CreateLowerLeftContent()
	{
		float num = ((!GameLevelPreview.m_camIsTurned) ? 0.5f : 0f);
		TimerS.AddComponent(this.m_buttonTimerEntity, string.Empty, num, 0f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			if (PlayerPrefsX.GetOffroadRacing() && !(PsState.m_activeGameLoop is PsGameLoopFresh))
			{
				this.m_boostButton = new PsUIBoosterButton(this.m_lowerLeft, false);
				this.m_boostButton.SetHorizontalAlign(0f);
				this.m_boostButton.ShowRefillButton();
			}
			this.CreateGarageButton(this.m_lowerLeft);
			this.m_lowerLeft.Update();
			if (this.m_boostButton != null && PsMetagameManager.m_playerStats.boosters < 1)
			{
				this.m_boostButton.GreyScaleOn();
			}
			if (!GameLevelPreview.m_camIsTurned)
			{
				this.m_lowerLeft.DisableTouchAreas(true);
				TweenC tweenC = TweenS.AddTransformTween(this.m_garage.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, 0.25f, 0f, false, true);
				tweenC.useUnscaledDeltaTime = true;
			}
		});
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x000D4060 File Offset: 0x000D2460
	public virtual void CreateLowerCenterContent()
	{
		if (!PsCoinRoulette.IsAvailable(PsState.m_activeGameLoop) || PsState.m_activeMinigame.m_playerReachedGoalCount > 0)
		{
			return;
		}
		PsMetrics.AdOffered("coinRoulette_button");
		this.m_hasCoinRouletteButton = true;
		float num = ((!GameLevelPreview.m_camIsTurned) ? 0.5f : 0f);
		TimerS.AddComponent(this.m_buttonTimerEntity, string.Empty, num, 0f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			this.m_coinRouletteButton = new PsUIAttentionButton(this.m_lowerCenter, default(Vector3), 0.25f, 0.25f, 0.005f);
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_roulette_star_unlit", null);
			Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_roulette_star_lit", null);
			this.m_coinRouletteButton.SetText("<color=#333333>" + PsStrings.Get(StringID.SPIN_GET_SUPER).ToUpper() + "</color>", 0.018f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_coinRouletteButton.m_textArea, "Spin text and stars");
			uihorizontalList.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, frame2, true, true);
			uifittedSprite.SetHeight(0.04f, RelativeTo.ParentHeight);
			uifittedSprite.SetHorizontalAlign(0.5f);
			new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.BUTTON_SPIN), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, null, null);
			uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, frame2, true, true);
			uifittedSprite.SetHeight(0.04f, RelativeTo.ParentHeight);
			uifittedSprite.SetHorizontalAlign(0.5f);
			this.m_coinRouletteButton.SetIcon("menu_watch_ad_badge", 0.08f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_coinRouletteButton.SetHorizontalAlign(0.5f);
			this.m_coinRouletteButton.SetVerticalAlign(0f);
			this.m_coinRouletteButton.SetGreenColors(true);
			this.m_coinRouletteButton.SetDepthOffset(-5f);
			this.m_lowerCenter.Update();
			if (!GameLevelPreview.m_camIsTurned)
			{
				this.m_lowerCenter.DisableTouchAreas(true);
				if (this.m_nextRace != null)
				{
					TweenC tweenC = TweenS.AddTransformTween(this.m_coinRouletteButton.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, 0.25f, 0f, false, true);
					tweenC.useUnscaledDeltaTime = true;
					TransformS.SetScale(this.m_coinRouletteButton.m_TC, Vector3.zero);
				}
			}
		});
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x000D40DC File Offset: 0x000D24DC
	public virtual void CreateLowerRightContent()
	{
		float num = ((!GameLevelPreview.m_camIsTurned) ? 0.5f : 0f);
		TimerS.AddComponent(this.m_buttonTimerEntity, string.Empty, num, 0f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			this.CreateGoButton(this.m_lowerRight);
			this.m_lowerRight.Update();
			if (!GameLevelPreview.m_camIsTurned)
			{
				this.m_lowerRight.DisableTouchAreas(true);
				if (this.m_nextRace != null)
				{
					TweenC tweenC = TweenS.AddTransformTween(this.m_nextRace.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, 0.25f, 0f, false, true);
					tweenC.useUnscaledDeltaTime = true;
					TransformS.SetScale(this.m_nextRace.m_TC, Vector3.zero);
				}
				TweenC tweenC2 = TweenS.AddTransformTween(this.m_go.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, 0.25f, 0f, false, true);
				TweenS.AddTweenEndEventListener(tweenC2, new TweenEventDelegate(this.ButtonTweenEventhandler));
				tweenC2.useUnscaledDeltaTime = true;
				TransformS.SetScale(this.m_go.m_TC, Vector3.zero);
			}
		});
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x000D4128 File Offset: 0x000D2528
	public virtual void CreateCenterContent()
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "mapArea");
		uihorizontalList.SetAlign(0.5f, 0.8f);
		uihorizontalList.RemoveDrawHandler();
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, this.GetMotivationString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.06f, RelativeTo.ScreenHeight, "#C2FF16", "313131");
		uitext.SetShadowShift(new Vector2(0.5f, -0.2f), 0.1f);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.GetGameModeFrame(), null), true, true);
		uifittedSprite.SetHeight(0.25f, RelativeTo.ScreenHeight);
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x000D41D0 File Offset: 0x000D25D0
	protected virtual string GetGameModeFrame()
	{
		return "item_mode_adventure";
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x000D41D7 File Offset: 0x000D25D7
	protected virtual string GetMotivationString()
	{
		return PsStrings.Get(StringID.GET_MAP_PIECES);
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x000D41E3 File Offset: 0x000D25E3
	private void ButtonTweenEventhandler(TweenC _c)
	{
		this.m_lowerLeft.EnableTouchAreas(true);
		this.m_lowerRight.EnableTouchAreas(true);
		this.m_lowerCenter.EnableTouchAreas(true);
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x000D4209 File Offset: 0x000D2609
	public override void Destroy(bool _removeFromManager)
	{
		base.Destroy(_removeFromManager);
		if (this.m_bottomBanner != null)
		{
			this.m_bottomBanner.Destroy();
			this.m_bottomBanner = null;
		}
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x000D4230 File Offset: 0x000D2630
	public virtual void CreateGarageButton(UIComponent _parent)
	{
		if (PsState.m_activeGameLoop.m_selectedVehicle != PsSelectedVehicle.GhostVehicle)
		{
			this.m_garage = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.011f, "Button");
			this.m_garage.SetSound("/UI/ButtonTransition");
			this.m_garage.SetGlareOffset(-0.01f);
			this.m_garage.SetUpperShineOffset(0.01f, -0.011f);
			this.m_garage.SetLowerShineOffset(-0.01f, 0.011f);
			this.m_garage.SetText(PsStrings.Get(StringID.UPGRADE_VEHICLE), 0.032f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
			this.m_garage.SetIcon("menu_icon_upgrade", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_upgradeableItemCount = PsUpgradeManager.GetUpgradeableItemCount(PsState.GetCurrentVehicleType(true));
			if (this.m_upgradeableItemCount > 0)
			{
				this.CreateUpgradeNotification();
			}
		}
		else
		{
			this.m_ownVehicle = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.01f, "Button");
			this.m_ownVehicle.SetBlueColors(true);
			this.m_ownVehicle.SetText("Switch back" + Environment.NewLine + "to own bike", 0.03375f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
			this.m_ownVehicle.SetMargins(0.05f, 0.05f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
			this.m_ownVehicle.SetDepthOffset(-5f);
			this.m_ownVehicle.SetGlareOffset(-0.01f);
			this.m_ownVehicle.SetUpperShineOffset(0.01f, -0.01f);
			this.m_ownVehicle.SetLowerShineOffset(-0.01f, 0.01f);
		}
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x000D43E4 File Offset: 0x000D27E4
	private void CreateUpgradeNotification()
	{
		this.m_notificationBase = new UICanvas(this.m_garage, false, "notification", null, string.Empty);
		this.m_notificationBase.SetSize(0.045f, 0.045f, RelativeTo.ScreenHeight);
		this.m_notificationBase.SetMargins(0.02f, -0.02f, -0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_notificationBase.SetRogue();
		this.m_notificationBase.SetAlign(1f, 1f);
		this.m_notificationBase.SetDepthOffset(-10f);
		this.m_notificationBase.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this.m_notificationBase, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas.SetMargins(0.15f, RelativeTo.OwnHeight);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
		TweenC tweenC = TweenS.AddTransformTween(uicanvas.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
		this.m_upgradeableItemCountText = new UIFittedText(uicanvas, false, string.Empty, this.m_upgradeableItemCount.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x000D453C File Offset: 0x000D293C
	public virtual void CreateGoButton(UIComponent _parent)
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
		if (PsState.m_activeMinigame.m_gameEnded || PsState.m_activeGameLoop.m_scoreBest > 0)
		{
			this.m_go.SetFittedText(PsStrings.Get(StringID.PLAY_AGAIN), 0.04f, 0.3f, RelativeTo.ScreenHeight, true);
		}
		else
		{
			this.m_go.SetFittedText(PsStrings.Get(StringID.PLAY), 0.04f, 0.2f, RelativeTo.ScreenHeight, true);
		}
		this.m_go.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_go.SetHorizontalAlign(1f);
	}

	// Token: 0x06001512 RID: 5394 RVA: 0x000D479C File Offset: 0x000D2B9C
	public override void Step()
	{
		if (this.m_createContents && this.m_lowEndPrompt == null)
		{
			this.CreateLowerLeftContent();
			this.CreateLowerCenterContent();
			this.CreateLowerRightContent();
			this.m_createContents = false;
		}
		if (PsState.m_activeGameLoop.m_selectedVehicle == PsSelectedVehicle.GhostVehicle && this.m_garage != null)
		{
			this.m_garage.Destroy();
			this.m_garage = null;
			this.m_notificationBase = null;
			this.m_upgradeableItemCountText = null;
			this.CreateGarageButton(this.m_lowerLeft);
			this.m_lowerLeft.Update();
		}
		else if (PsState.m_activeGameLoop.m_selectedVehicle != PsSelectedVehicle.GhostVehicle && this.m_ownVehicle != null)
		{
			this.m_ownVehicle.Destroy();
			this.m_ownVehicle = null;
			this.CreateGarageButton(this.m_lowerLeft);
			this.m_lowerLeft.Update();
		}
		if (this.m_coinRouletteButton != null && this.m_coinRouletteButton.m_hit)
		{
			(PsState.m_activeGameLoop.m_gameMode as PsGameModeAdventure).ShowAdPopup(false);
		}
		if (this.m_go != null && this.m_go.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Start");
		}
		else if (this.m_nextRace != null && this.m_nextRace.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		else if (this.m_everyplay != null && this.m_everyplay.m_hit)
		{
			Debug.Log("Opening everyplay...", null);
			EveryplayManager.StopRecording(0f);
			EveryplayManager.PlayLastRecording();
		}
		else if (this.m_garage != null && this.m_garage.m_hit)
		{
			this.m_lastView = PsMetagameManager.m_menuResourceView.SetLastView();
			this.CreateGaragePopup();
		}
		else if (this.m_ownVehicle != null && this.m_ownVehicle.m_hit)
		{
			PsState.m_activeGameLoop.m_selectedVehicle = PsSelectedVehicle.Motorcycle;
			PsState.m_activeGameLoop.m_userHasSelectedVehicle = true;
			if (PsState.m_activeMinigame.m_gamePaused || !PsState.m_activeMinigame.m_gameStarted)
			{
				PsState.m_activeMinigame.m_playerNode.Reset();
			}
		}
		else if (this.m_showRentStatsButton != null && this.m_showRentStatsButton.m_hit)
		{
			this.CreateRentStats();
			if (this.m_showRentStatsButtonArea != null)
			{
				this.m_showRentStatsButtonArea.Destroy();
				this.m_showRentStatsButtonArea = null;
			}
			TouchAreaS.AddBeginTouchDelegate(new Func<TouchAreaC, bool>(this.DestroyRentStatsByTouch));
		}
		base.Step();
	}

	// Token: 0x06001513 RID: 5395 RVA: 0x000D4A38 File Offset: 0x000D2E38
	public void CreateGaragePopup()
	{
		if (this.m_state == null)
		{
			this.m_state = Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState();
		}
		PsUICenterGarage.m_createGarageAction = new Action(this.CreateGaragePopup);
		PsUICenterGarage.SetVehicle(PsState.m_activeMinigame.m_playerUnit.GetType());
		PsUIBaseState psUIBaseState = new PsUIBaseState(typeof(PsUICenterGarage), typeof(PsUITopGarage), null, null, false, InitialPage.Center);
		psUIBaseState.SetAction("Exit", delegate
		{
			this.UpdateUpgradeNotification();
			CameraS.RemoveBlur();
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(this.m_state);
			this.m_state = null;
			PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastView);
			this.ResetCoinsAndPlayer();
		});
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState);
		CameraS.CreateBlur(CameraS.m_mainCamera, null);
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x000D4AE4 File Offset: 0x000D2EE4
	protected virtual void UpdateUpgradeNotification()
	{
		int upgradeableItemCount = PsUpgradeManager.GetUpgradeableItemCount(PsState.GetCurrentVehicleType(true));
		if (this.m_upgradeableItemCount != upgradeableItemCount)
		{
			this.m_upgradeableItemCount = upgradeableItemCount;
			if (this.m_upgradeableItemCountText != null)
			{
				if (this.m_upgradeableItemCount > 0)
				{
					this.m_upgradeableItemCountText.SetText(upgradeableItemCount.ToString());
				}
				else
				{
					this.m_notificationBase.Destroy();
					this.m_notificationBase = null;
					this.m_upgradeableItemCountText = null;
				}
			}
			else
			{
				this.CreateUpgradeNotification();
			}
		}
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x000D4B68 File Offset: 0x000D2F68
	private void ResetCoinsAndPlayer()
	{
		if (PsState.m_activeMinigame.m_gamePaused || !PsState.m_activeMinigame.m_gameStarted)
		{
			List<Entity> entitiesByTag = EntityManager.GetEntitiesByTag("GTAG_COIN");
			if (entitiesByTag.Count > 0)
			{
				foreach (Entity entity in entitiesByTag)
				{
					CustomObjectC customObjectC = EntityManager.GetComponentByEntity((ComponentType)31, entity) as CustomObjectC;
					CollectibleCoin collectibleCoin = customObjectC.m_customObject as CollectibleCoin;
					collectibleCoin.UpdateCollisionArea();
				}
			}
			PsState.m_activeMinigame.m_playerNode.Reset();
		}
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x000D4C20 File Offset: 0x000D3020
	public void ExitGarage()
	{
		this.UpdateUpgradeNotification();
		CameraS.RemoveBlur();
		this.m_garagePopup.Destroy();
		this.m_garagePopup = null;
		PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastView);
		this.ResetCoinsAndPlayer();
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x000D4C58 File Offset: 0x000D3058
	public void BottomDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, true);
		Color black = Color.black;
		Color black2 = Color.black;
		Color black3 = Color.black;
		black3.a = 0.5f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, rect, (float)Screen.height * 0.0075f, black, black2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, rect, (float)Screen.height * 0.015f, black3, black3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, black, black2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x000D4D54 File Offset: 0x000D3154
	public void OpponentAreaDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			default(Vector2),
			default(Vector2),
			default(Vector2),
			new Vector2(-_c.m_actualWidth * 0.5f + 0.025f * (float)Screen.height, -_c.m_actualHeight * 0.5f + 0.025f * (float)Screen.height)
		};
		array[2] = new Vector2(_c.m_actualWidth * 0.5f + 0.025f * (float)Screen.height, -_c.m_actualHeight * 0.5f + 0.025f * (float)Screen.height);
		array[1] = new Vector2(_c.m_actualWidth * 0.5f + 0.025f * (float)Screen.height, _c.m_actualHeight * 0.5f + 0.025f * (float)Screen.height);
		array[0] = new Vector2(-_c.m_actualWidth * 0.285f + 0.025f * (float)Screen.height, _c.m_actualHeight * 0.5f + 0.025f * (float)Screen.height);
		Color color = DebugDraw.HexToColor("#132233");
		Color color2 = DebugDraw.HexToColor("#132233");
		color.a = 0.65f;
		color2.a = 0.65f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, array, (float)Screen.height * 0.0075f, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 0f, ggdata, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x000D4F28 File Offset: 0x000D3328
	public override void Destroy()
	{
		if (this.m_buttonTimerEntity != null)
		{
			EntityManager.RemoveEntity(this.m_buttonTimerEntity);
		}
		this.m_buttonTimerEntity = null;
		CameraS.RemoveBlur();
		base.Destroy();
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x000D4F52 File Offset: 0x000D3352
	private void CreateShowRentStatsButton()
	{
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x000D4F54 File Offset: 0x000D3354
	private void CreateRentStats()
	{
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x000D4F56 File Offset: 0x000D3356
	private void DestroyRentStats()
	{
		CameraS.RemoveBlur();
		if (this.m_rentArea != null)
		{
			this.m_rentArea.Destroy();
			this.m_rentArea = null;
		}
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x000D4F7A File Offset: 0x000D337A
	private bool DestroyRentStatsByTouch(TouchAreaC _touchAreaC)
	{
		if (this.m_rentButton == null)
		{
			return true;
		}
		if (_touchAreaC == this.m_rentButton.m_TAC)
		{
			return false;
		}
		this.CreateShowRentStatsButton();
		this.DestroyRentStats();
		this.Update();
		return true;
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x000D4FB0 File Offset: 0x000D33B0
	public void RentSucceed()
	{
		PsState.m_activeGameLoop.m_rentedVehicle = true;
		Hashtable hashtable = null;
		if (PsState.m_activeGameLoop.m_path != null && PsState.m_activeGameLoop.m_path.GetPathType() != PsPlanetPathType.main)
		{
			hashtable = ClientTools.GenerateProgressionPathJson(PsState.m_activeGameLoop.m_path);
		}
		else if (PsState.m_activeGameLoop.m_path != null)
		{
			hashtable = ClientTools.GenerateProgressionPathJson(PsState.m_activeGameLoop, PsState.m_activeGameLoop.m_path.m_currentNodeId, true, true, true);
		}
		if (PsState.m_activeGameLoop.m_path != null)
		{
			PsMetagameManager.SaveProgression(hashtable, PsState.m_activeGameLoop.m_path.m_planet, false);
		}
		SoundS.PlaySingleShot("/Ingame/Vehicles/RentVehicle", Vector2.zero, 1f);
		this.DestroyRentStats();
		this.UseRentVehicle();
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x000D5078 File Offset: 0x000D3478
	public void RentCancelled()
	{
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x000D507C File Offset: 0x000D347C
	private void UseRentVehicle()
	{
		PsState.m_activeGameLoop.m_selectedVehicle = PsSelectedVehicle.GhostVehicle;
		PsState.m_activeGameLoop.m_userHasSelectedVehicle = true;
		if (PsState.m_activeMinigame.m_gamePaused || !PsState.m_activeMinigame.m_gameStarted)
		{
			PsState.m_activeMinigame.m_playerNode.Reset();
		}
	}

	// Token: 0x040017B9 RID: 6073
	protected PsUIBoosterButton m_boostButton;

	// Token: 0x040017BA RID: 6074
	protected PsUIGenericButton m_everyplay;

	// Token: 0x040017BB RID: 6075
	protected PsUIGenericButton m_go;

	// Token: 0x040017BC RID: 6076
	protected PsUIGenericButton m_nextRace;

	// Token: 0x040017BD RID: 6077
	protected PsUIGenericButton m_coinRouletteButton;

	// Token: 0x040017BE RID: 6078
	protected PsUIGenericButton m_garage;

	// Token: 0x040017BF RID: 6079
	protected PsUIGenericButton m_ownVehicle;

	// Token: 0x040017C0 RID: 6080
	protected PsUIBasePopup m_lowEndPrompt;

	// Token: 0x040017C1 RID: 6081
	protected bool m_lowEndShown;

	// Token: 0x040017C2 RID: 6082
	protected UIVerticalList m_lowerLeft;

	// Token: 0x040017C3 RID: 6083
	protected PsUIBasePopup m_garagePopup;

	// Token: 0x040017C4 RID: 6084
	protected UIHorizontalList m_lowerRight;

	// Token: 0x040017C5 RID: 6085
	protected UIVerticalList m_lowerCenter;

	// Token: 0x040017C6 RID: 6086
	protected UICanvas m_bottomBanner;

	// Token: 0x040017C7 RID: 6087
	protected UIFittedText m_upgradeableItemCountText;

	// Token: 0x040017C8 RID: 6088
	protected int m_upgradeableItemCount;

	// Token: 0x040017C9 RID: 6089
	protected UICanvas m_notificationBase;

	// Token: 0x040017CA RID: 6090
	protected Entity m_buttonTimerEntity;

	// Token: 0x040017CB RID: 6091
	protected LastResourceView m_lastView;

	// Token: 0x040017CC RID: 6092
	protected bool m_createContents;

	// Token: 0x040017CD RID: 6093
	protected bool m_hasCoinRouletteButton;

	// Token: 0x040017CE RID: 6094
	private IState m_state;

	// Token: 0x040017CF RID: 6095
	private PsUIGenericButton m_showRentStatsButton;

	// Token: 0x040017D0 RID: 6096
	private PsUIGenericButton m_rentButton;

	// Token: 0x040017D1 RID: 6097
	protected UICanvas m_showRentStatsButtonArea;

	// Token: 0x040017D2 RID: 6098
	private PsUIVehicleStats m_rentStats;

	// Token: 0x040017D3 RID: 6099
	private UIHorizontalList m_rentArea;

	// Token: 0x040017D4 RID: 6100
	private int m_rentPrice;
}
