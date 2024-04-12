using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CE RID: 718
public class PsUICenterStartRacing : UICanvas
{
	// Token: 0x06001538 RID: 5432 RVA: 0x000D68A0 File Offset: 0x000D4CA0
	public PsUICenterStartRacing(UIComponent _parent)
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
		UIVerticalList uiverticalList = new UIVerticalList(this, "lowerCenter");
		uiverticalList.SetAlign(0.5f, 0f);
		uiverticalList.RemoveDrawHandler();
		if (EveryplayManager.IsEnabled() && EveryplayManager.HasRecordedVideo())
		{
			this.m_everyplay = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
			this.m_everyplay.SetIcon("hud_button_replay", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_everyplay.SetText("Watch\nReplay!", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_everyplay.SetHorizontalAlign(0.5f);
			this.m_everyplay.SetVerticalAlign(0f);
			this.m_everyplay.SetBlueColors(true);
			this.m_everyplay.SetMargins(0.035f, 0.035f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
			this.m_everyplay.SetDepthOffset(-5f);
		}
		this.m_lowerRight = new UIHorizontalList(this, "lowerRight");
		this.m_lowerRight.SetAlign(1f, 0f);
		this.m_lowerRight.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_lowerRight.RemoveDrawHandler();
		if (!GameScene.m_lowPerformance || PlayerPrefsX.GetLowEndPrompt())
		{
			this.CreateLowerRightContent();
		}
		this.m_bottomBanner = new UICanvas(null, false, string.Empty, null, string.Empty);
		this.m_bottomBanner.SetCamera(this.m_camera, true, false);
		this.m_bottomBanner.SetDepthOffset(20f);
		this.m_bottomBanner.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_bottomBanner.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_bottomBanner.SetDrawHandler(new UIDrawDelegate(this.BottomDrawhandler));
		this.m_bottomBanner.SetAlign(0.5f, 0f);
		this.m_bottomBanner.Update();
		this.CreateOpponents();
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x000D6B64 File Offset: 0x000D4F64
	public virtual void CreateOpponents()
	{
		this.m_opponentArea = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_opponentArea.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_opponentArea.SetWidth(0.67f, RelativeTo.ScreenHeight);
		this.m_opponentArea.SetAlign(1f, 1f);
		this.m_opponentArea.SetDepthOffset(5f);
		this.m_opponentArea.SetDrawHandler(new UIDrawDelegate(this.OpponentAreaDrawhandler));
		this.m_opponents = new PsUIWinOpponents(this.m_opponentArea);
		this.m_opponents.SetMargins(0f, 0f, 0.175f, 0f, RelativeTo.ScreenHeight);
		this.m_opponents.SetAlign(1f, 1f);
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x000D6C2D File Offset: 0x000D502D
	public void InitOpponents()
	{
		(this.m_opponents as PsUIWinOpponents).PositionOpponents();
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x000D6C40 File Offset: 0x000D5040
	public void CreateLowerLeftContent()
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
				TransformS.SetScale(this.m_garage.m_TC, Vector3.zero);
			}
		});
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x000D6C8C File Offset: 0x000D508C
	public void CreateLowerRightContent()
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

	// Token: 0x0600153D RID: 5437 RVA: 0x000D6CD7 File Offset: 0x000D50D7
	private void ButtonTweenEventhandler(TweenC _c)
	{
		this.m_lowerLeft.EnableTouchAreas(true);
		this.m_lowerRight.EnableTouchAreas(true);
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x000D6CF1 File Offset: 0x000D50F1
	public override void Destroy(bool _removeFromManager)
	{
		base.Destroy(_removeFromManager);
		if (this.m_bottomBanner != null)
		{
			this.m_bottomBanner.Destroy();
			this.m_bottomBanner = null;
		}
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x000D6D18 File Offset: 0x000D5118
	public void RemoveButtons()
	{
		if (this.m_boostButton != null)
		{
			this.m_boostButton.Destroy();
		}
		if (this.m_everyplay != null)
		{
			this.m_everyplay.Destroy();
		}
		if (this.m_go != null)
		{
			this.m_go.Destroy();
		}
		if (this.m_nextRace != null)
		{
			this.m_nextRace.Destroy();
		}
		if (this.m_garage != null)
		{
			this.m_garage.Destroy();
		}
		if (this.m_ownVehicle != null)
		{
			this.m_ownVehicle.Destroy();
		}
		this.m_boostButton = null;
		this.m_everyplay = null;
		this.m_go = null;
		this.m_nextRace = null;
		this.m_garage = null;
		this.m_ownVehicle = null;
		this.m_notificationBase = null;
		this.m_upgradeableItemCountText = null;
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x000D6DE4 File Offset: 0x000D51E4
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
			this.m_garage.SetHorizontalAlign(1f);
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

	// Token: 0x06001541 RID: 5441 RVA: 0x000D6FA8 File Offset: 0x000D53A8
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

	// Token: 0x06001542 RID: 5442 RVA: 0x000D7100 File Offset: 0x000D5500
	public virtual void CreateGoButton(UIComponent _parent)
	{
		int purchasedRunsLimit = (PsState.m_activeGameLoop as PsGameLoopRacing).m_purchasedRunsLimit;
		int purchasedRuns = (PsState.m_activeGameLoop as PsGameLoopRacing).m_purchasedRuns;
		int heatNumber = (PsState.m_activeGameLoop as PsGameLoopRacing).m_heatNumber;
		if ((PsState.m_activeGameLoop as PsGameLoopRacing).m_ghostWon)
		{
			if (heatNumber > 1)
			{
				this.m_nextRace = new PsUIAttentionButton(_parent, Vector3.one * 1.15f, 0.25f, 0.25f, 0.01f);
			}
			this.m_go = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.01f, "Button");
		}
		else
		{
			if (heatNumber > 1)
			{
				this.m_nextRace = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.01f, "Button");
			}
			this.m_go = new PsUIAttentionButton(_parent, Vector3.one * 1.15f, 0.25f, 0.25f, 0.01f);
		}
		if (this.m_nextRace != null)
		{
			this.m_nextRace.SetNextRaceButton();
			this.m_nextRace.SetHeight(0.12f, RelativeTo.ScreenHeight);
			this.m_nextRace.SetHorizontalAlign(1f);
		}
		this.m_go.SetRaceButton(heatNumber, purchasedRuns, (PsState.m_activeGameLoop as PsGameLoopRacing).m_ghostWon);
		this.m_go.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_go.SetHorizontalAlign(1f);
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x000D726C File Offset: 0x000D566C
	public virtual void AnimateOpponents()
	{
		Vector3 localPosition = this.m_opponentArea.m_TC.transform.localPosition;
		TransformS.SetPosition(this.m_opponentArea.m_TC, this.m_opponentArea.m_TC.transform.localPosition + new Vector3((float)Screen.width * 0.5f, 0f, 0f));
		TweenC tweenC = TweenS.AddTransformTween(this.m_opponentArea.m_TC, TweenedProperty.Position, TweenStyle.CubicIn, localPosition, 0.3f, 0f, true);
		tweenC.useUnscaledDeltaTime = true;
		for (int i = 0; i < this.m_opponents.m_playerList.m_childs.Count; i++)
		{
			Vector3 localPosition2 = this.m_opponents.m_playerList.m_childs[i].m_TC.transform.localPosition;
			TransformS.SetPosition(this.m_opponents.m_playerList.m_childs[i].m_TC, this.m_opponents.m_playerList.m_childs[i].m_TC.transform.localPosition + new Vector3((float)Screen.width * 0.5f, 0f, 0f));
			tweenC = TweenS.AddTransformTween(this.m_opponents.m_playerList.m_childs[i].m_TC, TweenedProperty.Position, TweenStyle.BackOut, localPosition2, 0.3f, (float)i * 0.2f, true);
			tweenC.useUnscaledDeltaTime = true;
		}
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x000D73E8 File Offset: 0x000D57E8
	public override void Step()
	{
		if (this.m_createContents && this.m_lowEndPrompt == null)
		{
			this.CreateLowerLeftContent();
			this.CreateLowerRightContent();
			this.m_createContents = false;
		}
		bool nextRacePressed = (this.m_opponents as PsUIWinOpponents).m_nextRacePressed;
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
		if (this.m_go != null && this.m_go.m_hit && !nextRacePressed)
		{
			PsGameLoop activeGameLoop = PsState.m_activeGameLoop;
			if (activeGameLoop is PsGameLoopRacing && (PsState.m_activeGameLoop as PsGameLoopRacing).m_heatNumber > 5 + (PsState.m_activeGameLoop as PsGameLoopRacing).m_purchasedRuns && !(PsState.m_activeGameLoop as PsGameLoopRacing).m_trophiesRewarded)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Shop");
			}
			else
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Start");
			}
		}
		else if (this.m_nextRace != null && this.m_nextRace.m_hit && !nextRacePressed)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		else if (this.m_everyplay != null && this.m_everyplay.m_hit && !nextRacePressed)
		{
			Debug.Log("Opening everyplay...", null);
			EveryplayManager.StopRecording(0f);
			EveryplayManager.PlayLastRecording();
		}
		else if (this.m_garage != null && this.m_garage.m_hit && !nextRacePressed)
		{
			this.m_lastView = PsMetagameManager.m_menuResourceView.SetLastView();
			this.m_lastView = PsMetagameManager.m_menuResourceView.SetLastView();
			this.CreateGaragePopup();
		}
		else if (this.m_ownVehicle != null && this.m_ownVehicle.m_hit && !nextRacePressed)
		{
			PsState.m_activeGameLoop.m_selectedVehicle = PsSelectedVehicle.Motorcycle;
			PsState.m_activeGameLoop.m_userHasSelectedVehicle = true;
			if (PsState.m_activeMinigame.m_gamePaused || !PsState.m_activeMinigame.m_gameStarted)
			{
				PsState.m_activeMinigame.m_playerNode.Reset();
			}
		}
		else if (this.m_showRentStatsButton != null && this.m_showRentStatsButton.m_hit && !nextRacePressed)
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

	// Token: 0x06001545 RID: 5445 RVA: 0x000D76F8 File Offset: 0x000D5AF8
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
			if (this.m_opponents != null && this.m_opponents.m_ownProfile != null)
			{
				this.m_opponents.m_ownProfile.SetCC();
			}
		});
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState);
		CameraS.CreateBlur(CameraS.m_mainCamera, null);
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x000D77A4 File Offset: 0x000D5BA4
	private void UpdateUpgradeNotification()
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

	// Token: 0x06001547 RID: 5447 RVA: 0x000D7828 File Offset: 0x000D5C28
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

	// Token: 0x06001548 RID: 5448 RVA: 0x000D78E0 File Offset: 0x000D5CE0
	public void ExitGarage()
	{
		this.UpdateUpgradeNotification();
		CameraS.RemoveBlur();
		this.m_garagePopup.Destroy();
		this.m_garagePopup = null;
		PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastView);
		if (!this.m_opponents.m_coinsCumulated)
		{
			PsMetagameManager.m_menuResourceView.m_coins.SetText((Convert.ToInt32(PsMetagameManager.m_menuResourceView.m_coins.m_text) - PsMetagameManager.GetSecondaryGhostCoinReward()).ToString());
		}
		if (!this.m_opponents.m_diamondsCumulated)
		{
			PsMetagameManager.m_menuResourceView.m_diamonds.SetText((Convert.ToInt32(PsMetagameManager.m_menuResourceView.m_diamonds.m_text) - PsMetagameManager.GetSecondaryGhostDiamondReward()).ToString());
		}
		this.ResetCoinsAndPlayer();
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x000D79B0 File Offset: 0x000D5DB0
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

	// Token: 0x0600154A RID: 5450 RVA: 0x000D7AAC File Offset: 0x000D5EAC
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
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 19f, array, (float)Screen.height * 0.0075f, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 20f, ggdata, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x000D7C80 File Offset: 0x000D6080
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

	// Token: 0x0600154C RID: 5452 RVA: 0x000D7CAC File Offset: 0x000D60AC
	private void CreateShowRentStatsButton()
	{
		if (PsState.m_activeGameLoop.m_selectedVehicle != PsSelectedVehicle.GhostVehicle)
		{
			this.m_showRentStatsButtonArea = new UICanvas(this.m_opponents, false, string.Empty, null, string.Empty);
			this.m_showRentStatsButtonArea.SetMargins(-0.3f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
			this.m_showRentStatsButtonArea.RemoveDrawHandler();
			this.m_showRentStatsButton = new PsUIGenericButton(this.m_showRentStatsButtonArea, 0.25f, 0.25f, 0.005f, "Button");
			this.m_showRentStatsButton.SetText("Want to use" + Environment.NewLine + "opponent's bike?", 0.025f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_showRentStatsButton.SetBackgroundBubble(SpeechBubbleHandlePosition.Right);
			this.m_showRentStatsButton.SetDepthOffset(-10f);
			this.m_showRentStatsButton.SetAlign(0f, 0.9f);
		}
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x000D7D94 File Offset: 0x000D6194
	private void CreateRentStats()
	{
		CameraS.CreateBlur(CameraS.m_mainCamera, null);
		this.m_rentArea = new UIHorizontalList(this.m_opponents, string.Empty);
		this.m_rentArea.MoveToIndexAtParentsChildList(0);
		this.m_rentArea.SetVerticalAlign(0.9f);
		this.m_rentArea.SetMargins(0f, -0.06f, 0.06f, 0f, RelativeTo.ScreenHeight);
		this.m_rentArea.SetSpacing(0.033f, RelativeTo.ScreenHeight);
		this.m_rentArea.RemoveDrawHandler();
		List<float> list = PsState.m_activeMinigame.m_playerUnit.ParseUpgradeValues(PsState.m_activeGameLoop.m_gameMode.m_playbackGhosts[0].m_ghost.m_upgradeValues);
		float num = PsUpgradeManager.GetMaxPerformance(PsState.m_activeMinigame.m_playerUnit.GetType()) - PsUpgradeManager.GetBasePerformance(PsState.m_activeMinigame.m_playerUnit.GetType());
		float num2 = PsUpgradeManager.GetBasePerformance(PsState.m_activeMinigame.m_playerUnit.GetType()) / 4f;
		float num3 = num / 3f * list[0] + num2;
		float num4 = num / 3f * list[1] + num2;
		float num5 = num / 3f * list[2] + num2;
		float num6 = (float)((int)num3 + (int)num4 + (int)num5);
		float currentPerformance = PsUpgradeManager.GetCurrentPerformance(PsState.m_activeMinigame.m_playerUnit.GetType());
		this.m_rentStats = new PsUIVehicleStats(this.m_rentArea, (int)num6, (int)num3, (int)num4, (int)num5, (int)num2, 0, -1);
		if (PsState.m_activeGameLoop.m_rentedVehicle)
		{
			this.m_rentButton = new PsUIGenericButton(this.m_rentArea, 0.25f, 0.25f, 0.005f, "Button");
			this.m_rentButton.SetText("Use this", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_rentButton.SetBackgroundBubble(SpeechBubbleHandlePosition.Right);
		}
		else
		{
			this.m_rentPrice = 0;
			if (num6 > currentPerformance)
			{
				this.m_rentPrice = (int)((100f - currentPerformance / (num6 / 100f)) * 0.25f);
			}
			this.m_rentButton = new PsUIGenericButton(this.m_rentArea, 0.25f, 0.25f, 0.005f, "Button");
			this.m_rentButton.SetText("Rent this", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_rentButton.SetDiamondPrice(this.m_rentPrice, 0.04f);
			this.m_rentButton.SetBackgroundBubble(SpeechBubbleHandlePosition.Right);
		}
		this.Update();
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x000D8008 File Offset: 0x000D6408
	private void DestroyRentStats()
	{
		CameraS.RemoveBlur();
		if (this.m_rentArea != null)
		{
			this.m_rentArea.Destroy();
			this.m_rentArea = null;
		}
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x000D802C File Offset: 0x000D642C
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

	// Token: 0x06001550 RID: 5456 RVA: 0x000D8064 File Offset: 0x000D6464
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

	// Token: 0x06001551 RID: 5457 RVA: 0x000D812C File Offset: 0x000D652C
	public void RentCancelled()
	{
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x000D8130 File Offset: 0x000D6530
	private void UseRentVehicle()
	{
		PsState.m_activeGameLoop.m_selectedVehicle = PsSelectedVehicle.GhostVehicle;
		PsState.m_activeGameLoop.m_userHasSelectedVehicle = true;
		if (PsState.m_activeMinigame.m_gamePaused || !PsState.m_activeMinigame.m_gameStarted)
		{
			PsState.m_activeMinigame.m_playerNode.Reset();
		}
	}

	// Token: 0x040017E1 RID: 6113
	private PsUIBoosterButton m_boostButton;

	// Token: 0x040017E2 RID: 6114
	private PsUIGenericButton m_everyplay;

	// Token: 0x040017E3 RID: 6115
	protected PsUIGenericButton m_go;

	// Token: 0x040017E4 RID: 6116
	protected PsUIGenericButton m_nextRace;

	// Token: 0x040017E5 RID: 6117
	protected PsUIGenericButton m_garage;

	// Token: 0x040017E6 RID: 6118
	protected PsUIGenericButton m_ownVehicle;

	// Token: 0x040017E7 RID: 6119
	protected PsUIBasePopup m_lowEndPrompt;

	// Token: 0x040017E8 RID: 6120
	protected bool m_lowEndShown;

	// Token: 0x040017E9 RID: 6121
	private UIVerticalList m_lowerLeft;

	// Token: 0x040017EA RID: 6122
	private PsUIBasePopup m_garagePopup;

	// Token: 0x040017EB RID: 6123
	public PsUIOpponents m_opponents;

	// Token: 0x040017EC RID: 6124
	private UIHorizontalList m_lowerRight;

	// Token: 0x040017ED RID: 6125
	private UICanvas m_bottomBanner;

	// Token: 0x040017EE RID: 6126
	private UIFittedText m_upgradeableItemCountText;

	// Token: 0x040017EF RID: 6127
	private int m_upgradeableItemCount;

	// Token: 0x040017F0 RID: 6128
	private UICanvas m_notificationBase;

	// Token: 0x040017F1 RID: 6129
	protected UICanvas m_opponentArea;

	// Token: 0x040017F2 RID: 6130
	protected bool m_opponentsAnimated;

	// Token: 0x040017F3 RID: 6131
	private Entity m_buttonTimerEntity;

	// Token: 0x040017F4 RID: 6132
	private LastResourceView m_lastView;

	// Token: 0x040017F5 RID: 6133
	private bool m_createContents;

	// Token: 0x040017F6 RID: 6134
	private PsUIGenericButton runShop;

	// Token: 0x040017F7 RID: 6135
	private IState m_state;

	// Token: 0x040017F8 RID: 6136
	private PsUIGenericButton m_showRentStatsButton;

	// Token: 0x040017F9 RID: 6137
	private PsUIGenericButton m_rentButton;

	// Token: 0x040017FA RID: 6138
	protected UICanvas m_showRentStatsButtonArea;

	// Token: 0x040017FB RID: 6139
	private PsUIVehicleStats m_rentStats;

	// Token: 0x040017FC RID: 6140
	private UIHorizontalList m_rentArea;

	// Token: 0x040017FD RID: 6141
	private int m_rentPrice;
}
