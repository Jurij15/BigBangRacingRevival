using System;
using System.Collections;
using System.Collections.Generic;
using DeepLink;
using Prime31;
using UnityEngine;

// Token: 0x0200022D RID: 557
public class PsMainMenuState : BasicState, IStatedObject
{
	// Token: 0x06001056 RID: 4182 RVA: 0x00097DFD File Offset: 0x000961FD
	public static string GetVehiclePlanetIdentifier(int _index)
	{
		if (_index == 0)
		{
			return "AdventureOffroadCar";
		}
		if (_index != 1)
		{
			Debug.LogError("Wrong index");
			return "AdventureOffroadCar";
		}
		return "AdventureMotorcycle";
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x00097E2C File Offset: 0x0009622C
	public static string GetCurrentPlanetIdentifier()
	{
		return PsMainMenuState.GetVehiclePlanetIdentifier(PsState.GetVehicleIndex());
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x00097E38 File Offset: 0x00096238
	public static void ResetTrophyCumulateValues()
	{
		PsMainMenuState.m_trophyCumulateAmount = 0;
		PsMainMenuState.m_remainingAmount = 0;
		PsMainMenuState.m_trophyCumulateDur = 0;
		PsMainMenuState.m_trophyCumulateCur = 0;
		PsMainMenuState.m_startProg = 0f;
		PsMainMenuState.m_progDiff = 0f;
		PsMainMenuState.m_rankChange = false;
		PsMainMenuState.m_cumulateTrophyText = false;
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x00097E74 File Offset: 0x00096274
	public override void Enter(IStatedObject _parent)
	{
		FrbMetrics.SetCurrentScreen("main_menu");
		PsCaches.m_friendsLevelList.Clear();
		PsMainMenuState.m_startHidden = false;
		PsMainMenuState.m_lockRacing = !PlayerPrefsX.GetOffroadRacing();
		PsMainMenuState.m_lockAll = !PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && PsState.GetCurrentVehicleType(false) == typeof(Motorcycle);
		PsMainMenuState.ResetTrophyCumulateValues();
		PsMenuScene.m_lastState = "PsMainMenuState";
		CameraS.RemoveBlur();
		PsMainMenuState.m_uiCanvas = new UICanvas(null, false, "MainMenuUICanvas", null, string.Empty);
		PsMainMenuState.m_uiCanvas.SetHeight(1f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_uiCanvas.SetWidth(1f, RelativeTo.ScreenWidth);
		PsMainMenuState.m_uiCanvas.RemoveDrawHandler();
		string empty = string.Empty;
		PsMainMenuState.m_topLeftArea = new UIHorizontalList(PsMainMenuState.m_uiCanvas, string.Empty);
		PsMainMenuState.m_topLeftArea.SetMargins(0.06f, 0f, 0.025f, 0f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_topLeftArea.SetAlign(0f, 1f);
		PsMainMenuState.m_topLeftArea.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_topLeftArea.RemoveDrawHandler();
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetCharacterCustomisationData().GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		string text = null;
		if (installedItemByCategory != null)
		{
			text = installedItemByCategory.m_identifier;
		}
		PsMainMenuState.m_profileImage = new PsUIProfileImage(PsMainMenuState.m_topLeftArea, true, "ProfileImage", PlayerPrefsX.GetFacebookId(), PlayerPrefsX.GetGameCenterId(), -1, true, false, true, 0.075f, 0.06f, "ffffff", text, true, true);
		PsMainMenuState.m_profileImage.SetSize(0.115f, 0.115f, RelativeTo.ScreenHeight);
		UICanvas uicanvas = new UICanvas(PsMainMenuState.m_profileImage, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.0385f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.315f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(0f, 0f);
		uicanvas.SetMargins(0f, 0f, 0.04f, -0.04f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PlayerPrefsX.GetUserName(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "91ee57", "313131");
		uifittedText.SetHorizontalAlign(0f);
		PsMainMenuState.m_createButton = new PsUIGenericButton(PsMainMenuState.m_topLeftArea, 0.25f, 0.25f, 0.0065f, "Button");
		PsMainMenuState.m_createButton.SetSound("/UI/ButtonTransition");
		PsMainMenuState.m_createButton.SetMenuButton(PsStrings.Get(StringID.CREATE_LEVELS), "menu_icon_editor", null, false);
		if (PlayerPrefsX.GetOwnLevelClaimCount() > 0)
		{
			PsMainMenuState.CreateNotification(PsMainMenuState.m_createButton, PlayerPrefsX.GetOwnLevelClaimCount().ToString());
		}
		PsMainMenuState.m_teamButton = new PsUIGenericButton(PsMainMenuState.m_topLeftArea, 0.25f, 0.25f, 0.0065f, "Button");
		PsMainMenuState.m_teamButton.SetSound("/UI/ButtonTransition");
		if (PsMetagameManager.m_playerStats.carRank >= 2 || PsMetagameManager.m_playerStats.mcRank >= 2)
		{
			PsMainMenuState.m_teamButton.SetMenuButton(PsStrings.Get(StringID.BUTTON_TEAMS), "menu_icon_teams", string.Empty, false);
		}
		else
		{
			PsMainMenuState.m_teamButton.SetMenuButton(PsStrings.Get(StringID.BUTTON_TEAMS), "menu_icon_teams", string.Empty, true);
		}
		if (PlayerPrefsX.GetNewComments() > 0 && !string.IsNullOrEmpty(PlayerPrefsX.GetTeamId()))
		{
			UICanvas uicanvas2 = new UICanvas(PsMainMenuState.m_teamButton, false, "notification", null, string.Empty);
			uicanvas2.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
			uicanvas2.SetMargins(0.03f, -0.03f, -0.02f, 0.02f, RelativeTo.ScreenHeight);
			uicanvas2.SetRogue();
			uicanvas2.SetAlign(1f, 1f);
			uicanvas2.SetDepthOffset(-10f);
			uicanvas2.RemoveDrawHandler();
			UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
			uicanvas3.SetSize(1f, 1f, RelativeTo.ParentHeight);
			uicanvas3.SetMargins(0.15f, RelativeTo.OwnHeight);
			uicanvas3.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
			TweenC tweenC = TweenS.AddTransformTween(uicanvas3.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
			UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, PlayerPrefsX.GetNewComments().ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		}
		PsMainMenuState.m_eventsButton = new PsUIGenericButton(PsMainMenuState.m_topLeftArea, 0.25f, 0.25f, 0.0065f, "Button");
		PsMainMenuState.m_eventsButton.SetMenuButton(PsStrings.Get(StringID.TOUR_EVENTS_MENU), "menu_tournament_logo", null, false);
		this.CreateEventTimer();
		PsMainMenuState.m_vehicleButtonArea = new UIVerticalList(PsMainMenuState.m_uiCanvas, string.Empty);
		PsMainMenuState.m_vehicleButtonArea.SetAlign(0f, 1f);
		PsMainMenuState.m_vehicleButtonArea.SetMargins(0.015f, -0.015f, 0.3f, -0.3f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_vehicleButtonArea.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_vehicleButtonArea.RemoveDrawHandler();
		if (PsMainMenuState.m_vehicleButtons == null)
		{
			PsMainMenuState.m_vehicleButtons = new List<PsUIGenericButton>();
		}
		PsMainMenuState.m_vehicleButtons.Clear();
		string text3;
		float num;
		float num2;
		cpBB cpBB;
		for (int i = 0; i < PsState.m_vehicleTypes.Length; i++)
		{
			PsUIGenericButton psUIGenericButton = new PsUIGenericButton(PsMainMenuState.m_vehicleButtonArea, 0.25f, 0.25f, 0.005f, "Button");
			psUIGenericButton.SetSound(null);
			string text2 = "item_player_monster_car_icon";
			if (PsState.m_vehicleTypes[i] == typeof(Motorcycle))
			{
				text2 = "item_player_motorcycle_icon";
			}
			PsUIGenericButton psUIGenericButton2 = psUIGenericButton;
			text3 = text2;
			num = 0.12f;
			num2 = 0.14f;
			cpBB = new cpBB(-0.015f, -0.015f, -0.015f, -0.015f);
			psUIGenericButton2.SetIcon(text3, num, num2, "#FFFFFF", cpBB, true);
			psUIGenericButton.m_UIsprite.SetDepthOffset(-5f);
			psUIGenericButton.SetHeight(0.13f, RelativeTo.ScreenHeight);
			psUIGenericButton.SetMargins(0f, RelativeTo.ScreenHeight);
			int upgradeableItemCount = PsUpgradeManager.GetUpgradeableItemCount(PsState.m_vehicleTypes[i]);
			bool flag = (PsState.m_vehicleTypes[i] == typeof(Motorcycle) && PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && PlayerPrefsX.GetMotorcycleChecked()) || PsState.m_vehicleTypes[i] == typeof(OffroadCar);
			if (PsState.GetCurrentVehicleType(false) == PsState.m_vehicleTypes[i])
			{
				psUIGenericButton.SetBlueColors(false);
				psUIGenericButton.DisableTouchAreas(true);
			}
			else if (flag && upgradeableItemCount > 0)
			{
				UICanvas uicanvas4 = new UICanvas(psUIGenericButton, false, "notification", null, string.Empty);
				uicanvas4.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
				uicanvas4.SetMargins(0.2f, -0.2f, -0.1f, 0.1f, RelativeTo.OwnWidth);
				uicanvas4.SetRogue();
				uicanvas4.SetAlign(1f, 1f);
				uicanvas4.SetDepthOffset(-10f);
				uicanvas4.RemoveDrawHandler();
				UICanvas uicanvas5 = new UICanvas(uicanvas4, false, string.Empty, null, string.Empty);
				uicanvas5.SetSize(1f, 1f, RelativeTo.ParentHeight);
				uicanvas5.SetMargins(0.15f, RelativeTo.OwnHeight);
				uicanvas5.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
				TweenC tweenC2 = TweenS.AddTransformTween(uicanvas5.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
				TweenS.SetAdditionalTweenProperties(tweenC2, -1, true, TweenStyle.CubicInOut);
				UIFittedText uifittedText3 = new UIFittedText(uicanvas5, false, string.Empty, upgradeableItemCount.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
			}
			PsMainMenuState.m_vehicleButtons.Add(psUIGenericButton);
		}
		PsMainMenuState.m_vehicleInfoArea = new UICanvas(PsMainMenuState.m_uiCanvas, false, string.Empty, null, string.Empty);
		PsMainMenuState.m_vehicleInfoArea.SetSize(0.65f, 0.75f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_vehicleInfoArea.SetAlign(0f, 0f);
		PsMainMenuState.m_vehicleInfoArea.RemoveDrawHandler();
		PsMainMenuState.m_modelHolder = new UICanvas(PsMainMenuState.m_vehicleInfoArea, false, string.Empty, null, string.Empty);
		PsMainMenuState.m_modelHolder.SetSize(0.65f, 0.93f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_modelHolder.SetAlign(0f, 0f);
		PsMainMenuState.m_modelHolder.SetMargins(0.05f, -0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_modelHolder.SetDepthOffset(15f);
		PsMainMenuState.m_modelHolder.SetRogue();
		PsMainMenuState.m_modelHolder.RemoveDrawHandler();
		PsMainMenuState.m_garageTouchArea = new UICanvas(PsMainMenuState.m_modelHolder, true, string.Empty, null, string.Empty);
		PsMainMenuState.m_garageTouchArea.SetSize(0.45f, 0.5f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_garageTouchArea.SetAlign(0.55f, 0.5f);
		PsMainMenuState.m_garageTouchArea.RemoveDrawHandler();
		PsMainMenuState.m_modelCanvas = new UI3DRenderTextureCanvas(PsMainMenuState.m_modelHolder, string.Empty, null, false);
		PsMainMenuState.m_modelCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		PsMainMenuState.m_modelCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		PsMainMenuState.m_modelCanvas.SetDepthOffset(15f);
		PsMainMenuState.m_modelCanvas.m_3DCamera.fieldOfView = 26f;
		PsMainMenuState.m_modelCanvas.m_3DCameraPivot.transform.Rotate(0f, -58f, 0f, 0);
		PsMainMenuState.m_modelCanvas.m_3DCameraPivot.transform.Rotate(3.25f, 0f, 0f, 1);
		PsMainMenuState.m_modelCanvas.m_3DCameraPivot.transform.Translate(Vector3.up * 1.95f + Vector3.right * 4f, 1);
		CameraS.MoveToBehindOther(PsMainMenuState.m_modelCanvas.m_3DCamera, CameraS.m_mainCamera);
		PrefabC prefabC = PsMainMenuState.m_modelCanvas.AddGameObject(ResourceManager.GetGameObject(RESOURCE.GarageBase_GameObject), new Vector3(0f, -30f, 0f), new Vector3(-90f, 0f, 0f));
		prefabC.p_gameObject.transform.localScale = Vector3.one * 0.9f;
		this.SetVehicle(PsState.GetVehicleIndex());
		int upgradeableItemCount2 = PsUpgradeManager.GetUpgradeableItemCount(PsState.GetCurrentVehicleType(false));
		if (upgradeableItemCount2 > 0 && !PsMainMenuState.m_lockAll)
		{
			PsMainMenuState.m_notificationBase = new UICanvas(PsMainMenuState.m_modelHolder, false, "notification", null, string.Empty);
			PsMainMenuState.m_notificationBase.SetSize(0.1f, 0.1f, RelativeTo.ScreenHeight);
			PsMainMenuState.m_notificationBase.SetAlign(0.865f, 0.65f);
			PsMainMenuState.m_notificationBase.SetDepthOffset(-10f);
			PsMainMenuState.m_notificationBase.SetDrawHandler(new UIDrawDelegate(PsMainMenuState.UpgradeBubble));
			TweenC tweenC3 = TweenS.AddTransformTween(PsMainMenuState.m_notificationBase.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.65f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC3, -1, true, TweenStyle.CubicInOut);
			string text4 = "menu_icon_upgrade";
			string text5 = upgradeableItemCount2.ToString();
			UIFittedSprite uifittedSprite = new UIFittedSprite(PsMainMenuState.m_notificationBase, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text4, null), true, true);
			uifittedSprite.SetHeight(0.075f, RelativeTo.ScreenHeight);
			uifittedSprite.SetAlign(0.2f, 0.2f);
			uifittedSprite.SetDepthOffset(-3f);
			UICanvas uicanvas6 = new UICanvas(PsMainMenuState.m_notificationBase, false, string.Empty, null, string.Empty);
			uicanvas6.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
			uicanvas6.SetAlign(1f, 1f);
			uicanvas6.SetMargins(0.02f, -0.02f, -0.02f, 0.02f, RelativeTo.ScreenHeight);
			uicanvas6.SetDepthOffset(-3f);
			uicanvas6.RemoveDrawHandler();
			UICanvas uicanvas7 = new UICanvas(uicanvas6, false, string.Empty, null, string.Empty);
			uicanvas7.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
			uicanvas7.SetAlign(1f, 1f);
			uicanvas7.SetMargins(0.15f, RelativeTo.OwnHeight);
			uicanvas7.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
			UIFittedText uifittedText4 = new UIFittedText(uicanvas7, false, string.Empty, text5, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		}
		PsMainMenuState.m_racingArea = new UIHorizontalList(PsMainMenuState.m_vehicleInfoArea, string.Empty);
		PsMainMenuState.m_racingArea.SetAlign(0f, 0f);
		PsMainMenuState.m_racingArea.SetMargins(0.06f, -0.05f, 0f, 0.0175f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_racingArea.SetSpacing(0.06f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_racingArea.SetHeight(0.33f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_racingArea.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(PsMainMenuState.m_racingArea, string.Empty);
		uiverticalList.SetSpacing(0f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(0f);
		uiverticalList.SetWidth(0.6f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		PsMainMenuState.m_leagueArea = new UIVerticalList(uiverticalList, string.Empty);
		PsMainMenuState.m_leagueArea.SetSpacing(-0.025f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_leagueArea.SetMargins(0f, 0f, 0.1f, 0.035f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_leagueArea.RemoveDrawHandler();
		string text6 = "offroader";
		if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
		{
			text6 = "dirtbike";
		}
		PsMainMenuState.m_vehicleIcon = new UIFittedSprite(PsMainMenuState.m_leagueArea, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_vehicle_logo_" + text6, null), true, true);
		PsMainMenuState.m_vehicleIcon.SetHeight(0.05f, RelativeTo.ScreenHeight);
		UICanvas uicanvas8 = new UICanvas(PsMainMenuState.m_leagueArea, false, string.Empty, null, string.Empty);
		uicanvas8.SetHeight(0.11f, RelativeTo.ScreenHeight);
		uicanvas8.SetWidth(0.65f, RelativeTo.ParentWidth);
		uicanvas8.SetMargins(0f, 0f, 0.001f, -0.001f, RelativeTo.ScreenHeight);
		uicanvas8.RemoveDrawHandler();
		string name = PsMetagameData.m_leagueData[PsMetagameData.GetCurrentLeagueIndex()].m_name;
		PsMainMenuState.m_leagueName = new UIFittedText(uicanvas8, false, string.Empty, name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, "000000");
		int currentLeagueIndex = PsMetagameData.GetCurrentLeagueIndex();
		UICanvas uicanvas9 = new UICanvas(PsMainMenuState.m_leagueArea, false, string.Empty, null, string.Empty);
		uicanvas9.SetRogue();
		uicanvas9.SetHeight(0.16f, RelativeTo.ScreenHeight);
		uicanvas9.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas9.SetAlign(0.5f, 0f);
		uicanvas9.SetDepthOffset(15f);
		uicanvas9.SetMargins(0f, 0f, 0.05f, -0.05f, RelativeTo.ScreenHeight);
		uicanvas9.RemoveDrawHandler();
		PsMainMenuState.m_playerLeagueBanner = new UIFittedSprite(uicanvas9, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(PsMetagameData.m_leagueData[currentLeagueIndex].m_bannerSprite, null), true, true);
		PsMainMenuState.m_playerLeagueBanner.SetHeight(0.16f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_playerLeagueBanner.SetAlign(0.5f, 1f);
		Vector3 vector = Vector3.one;
		if (PsGachaManager.IsSlotEmpty(PsGachaManager.SlotType.RACING) && PsMetagameManager.m_vehicleGachaData.m_rivalWonCount < 4)
		{
			vector = Vector3.one * 1.065f;
		}
		PsMainMenuState.m_raceButton = new PsUIAttentionButton(uiverticalList, vector, 0.25f, 0.25f, 0.01f);
		PsMainMenuState.m_raceButton.SetSound("/UI/ButtonTransition");
		PsMainMenuState.m_raceButton.SetOrangeColors(true);
		PsMainMenuState.m_raceButton.SetHeight(0.125f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_raceButton.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_raceButton.SetMargins(0.06f, 0.06f, 0f, 0f, RelativeTo.ScreenHeight);
		string text7 = PsStrings.Get(StringID.RACE);
		PsMainMenuState.m_raceButton.SetText(text7, 0.07f, 0.22f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		UIComponent uicomponent = new UIComponent(PsMainMenuState.m_vehicleInfoArea, false, string.Empty, null, null, string.Empty);
		uicomponent.SetMargins(0.015f, -0.015f, -0.015f, 0.015f, RelativeTo.ScreenHeight);
		uicomponent.RemoveDrawHandler();
		PsMainMenuState.m_leagueButton = new PsUIGenericButton(uicomponent, 0.25f, 0.25f, 0.005f, "Button");
		PsMainMenuState.m_leagueButton.SetSound("/UI/ButtonTransition");
		PsMainMenuState.m_leagueButton.SetAlign(0f, 0f);
		PsUIGenericButton leagueButton = PsMainMenuState.m_leagueButton;
		text3 = "menu_mode_trophy_1";
		num2 = 0.12f;
		num = 0.12f;
		cpBB = new cpBB(0f, 0.035f, 0f, 0.005f);
		leagueButton.SetIcon(text3, num2, num, "#FFFFFF", cpBB, true);
		PsMainMenuState.m_leagueButton.m_UIsprite.SetDepthOffset(-5f);
		PsMainMenuState.m_leagueButton.SetHeight(0.125f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_progress = new UICanvas(PsMainMenuState.m_leagueButton, false, string.Empty, null, string.Empty);
		PsMainMenuState.m_progress.SetRogue();
		PsMainMenuState.m_progress.SetVerticalAlign(0f);
		PsMainMenuState.m_progress.SetHeight(0.035f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_progress.SetWidth(0.13f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_progress.SetDrawHandler(new UIDrawDelegate(this.ProgressDrawhandler));
		PsMainMenuState.m_progress.SetDepthOffset(-10f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(PsMainMenuState.m_progress, string.Empty);
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		if (!PsMainMenuState.m_trophiesSet)
		{
			PsMainMenuState.m_currentTrophyAmount = PsMetagameManager.m_playerStats.trophies;
		}
		PsMainMenuState.m_progressAmount = 1f;
		PsLeagueData league = PsMetagameData.GetLeague(PsMetagameData.GetLeagueIndex(PsMainMenuState.m_currentTrophyAmount));
		PsLeagueData psLeagueData = null;
		if (PsMetagameData.GetLeagueIndex(PsMainMenuState.m_currentTrophyAmount) < PsMetagameData.m_leagueData.Count - 1)
		{
			psLeagueData = PsMetagameData.GetLeague(PsMetagameData.GetLeagueIndex(PsMainMenuState.m_currentTrophyAmount) + 1);
		}
		if (psLeagueData != null)
		{
			int num3 = PsMainMenuState.m_currentTrophyAmount - league.m_trophyLimit;
			int num4 = psLeagueData.m_trophyLimit - league.m_trophyLimit;
			PsMainMenuState.m_progressAmount = (float)num3 / (float)num4;
		}
		PsMainMenuState.m_trophyCount = new UIText(uihorizontalList, false, string.Empty, PsMainMenuState.m_currentTrophyAmount.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, "#fffb42", "000000");
		PsMainMenuState.m_trophyCount.SetShadowShift(new Vector2(0.5f, -0.1f), 0.05f);
		UICanvas uicanvas10 = new UICanvas(PsMainMenuState.m_uiCanvas, false, string.Empty, null, string.Empty);
		uicanvas10.SetSize(0.09f, 0.09f, RelativeTo.ScreenHeight);
		uicanvas10.SetAlign(0f, 1f);
		uicanvas10.SetMargins(0.02f, 0f, 0.02f, 0f, RelativeTo.ScreenHeight);
		uicanvas10.RemoveDrawHandler();
		uicanvas10.SetDepthOffset(-10f);
		PsMainMenuState.m_settingsButton = new PsUIGenericButton(uicanvas10, 0.25f, 0.25f, 0.005f, "Button");
		PsMainMenuState.m_settingsButton.SetSound("/UI/ButtonTransition");
		PsMainMenuState.m_settingsButton.SetIcon("menu_icon_settings", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		PsMainMenuState.m_settingsButton.SetMargins(0.0085f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_settingsButton.SetAlign(0f, 1f);
		PsMainMenuState.m_settingsButton.SetDepthOffset(-10f);
		PsMainMenuState.CreateGachaList(PsMainMenuState.m_uiCanvas, false);
		PsMainMenuState.m_uiCanvas.Update();
		PsMetagameManager.ShowResources(null, false, true, true, true, 0.025f, true, false, false);
		if (PsState.m_activeGameLoop != null)
		{
			PsState.m_activeGameLoop.BackAtMenu();
		}
		PsMainMenuState.UpdateLockables(false);
		if (PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && !PlayerPrefsX.GetMotorcycleChecked())
		{
			PsMainMenuState.CreateMCNotification();
			if (PsState.m_showMcDialogue)
			{
				PsDialogue dialogueByIdentifier = PsMetagameData.GetDialogueByIdentifier("unlock_motorcycle_planet");
				PsMainMenuState.HideUI(true, false, null, true, null);
				new PsMenuDialogueFlow(dialogueByIdentifier, 0f, delegate
				{
					PsMainMenuState.ShowUI(true, null);
				}, false, false);
				PsState.m_showMcDialogue = false;
			}
		}
		if (PlayerPrefsX.GetExistingNotify())
		{
			PsMainMenuState.CreateExistingNotification();
		}
		if (!PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && !PlayerPrefsX.GetOffroadRacing() && !PlayerPrefsX.GetNameChanged() && PlanetTools.m_planetProgressionInfos[PlanetTools.GetVehiclePlanetIdentifier()].m_mainPath.m_currentNodeId <= 1)
		{
			PsMetrics.FirstLevelOffered();
			PsMainMenuState.HideUI(false, false, null, false, PsMainMenuState.GetAllUIComponents());
			PsMainMenuState.m_startHidden = true;
			PlanetTools.m_planetProgressionInfos[PlanetTools.GetVehiclePlanetIdentifier()].m_mainPath.GetCurrentNodeInfo().ActivateCurrentLoop();
		}
		else if (PsMainMenuState.m_tweenIn)
		{
			PsMainMenuState.HideUI(false, false, null, false, PsMainMenuState.GetAllUIComponents());
			PsMainMenuState.ShowUI(true, PsMainMenuState.GetAllUIComponents());
			PsMainMenuState.m_tweenIn = false;
			if (!PsMainMenuState.m_mainMenuReached)
			{
				PsMainMenuState.m_mainMenuReached = true;
				PsMetrics.MainMenuReached();
			}
		}
		else if (!PsMainMenuState.m_mainMenuReached)
		{
			PsMainMenuState.m_mainMenuReached = true;
			PsMetrics.MainMenuReached();
		}
		bool flag2 = true;
		int leagueIndex = PsMetagameData.GetLeagueIndex(PsMainMenuState.m_currentTrophyAmount);
		int leagueIndex2 = PsMetagameData.GetLeagueIndex(PsMetagameManager.m_playerStats.trophies);
		if (leagueIndex != leagueIndex2)
		{
			PsMainMenuState.m_rankChangeComing = true;
		}
		PsMainMenuState.CheckNotificationPayload();
		bool flag3 = false;
		if (PsState.m_activeGameLoop != null)
		{
			flag3 = PsState.m_activeGameLoop.m_dialogues != null && ((PsState.m_activeGameLoop.m_nodeId == PsPlanetManager.GetCurrentPlanet().GetMainPathCurrentNodeId() - 1 && PsState.m_activeGameLoop.m_dialogues.ContainsKey(PsNodeEventTrigger.LoopReleased.ToString())) || (PsState.m_activeGameLoop.m_nodeId == PsPlanetManager.GetCurrentPlanet().GetMainPathCurrentNodeId() && PsState.m_activeGameLoop.m_dialogues.ContainsKey(PsNodeEventTrigger.LoopActivated.ToString())));
			if (!flag3)
			{
				flag3 = PsGachaManager.IsSlotEmpty(PsGachaManager.SlotType.ADVENTURE) && PsMetagameManager.m_vehicleGachaData.m_adventureGachaCount == 0 && PsMetagameManager.m_vehicleGachaData.m_mapPieceCount >= PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax;
			}
		}
		if (PsMetagameManager.m_patchNotes != null && !PsMainMenuState.m_startHidden && PsMetagameManager.m_patchNotes.showAtLogin && PsMainMenuState.m_popup == null && PsState.m_dialogues.Count == 0 && !flag3)
		{
			flag2 = false;
			Type popupType = PsGameLoopNews.GetPopupType(PsMetagameManager.m_patchNotes);
			PsMainMenuState.m_popup = new PsUIBasePopup(popupType, null, null, null, false, true, InitialPage.Center, false, false, false);
			(PsMainMenuState.m_popup.m_mainContent as PsUIEventMessagePopup).SetEventMessage(PsMetagameManager.m_patchNotes, false);
			PsMainMenuState.m_popup.Update();
			PsMainMenuState.m_popup.SetAction("Claim", delegate
			{
				PsMetagameManager.ClaimPatchNote(PsMetagameManager.m_patchNotes.messageId, new Action<HttpC>(PsMetagameManager.ClaimPatchNoteSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimPatchNoteFAILED), null);
				PsMetagameManager.m_patchNotes = null;
			});
			PsMainMenuState.m_popup.SetAction("Continue", delegate
			{
				PsMainMenuState.m_popup.Destroy();
				PsMainMenuState.m_popup = null;
				PsMetagameManager.ClaimPatchNote(PsMetagameManager.m_patchNotes.messageId, new Action<HttpC>(PsMetagameManager.ClaimPatchNoteSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimPatchNoteFAILED), null);
				PsMetagameManager.m_patchNotes = null;
			});
			PsMainMenuState.m_popup.SetAction("Exit", delegate
			{
				PsMainMenuState.m_popup.Destroy();
				PsMainMenuState.m_popup = null;
				PsMetagameManager.m_patchNotes = null;
			});
			TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		else if (!string.IsNullOrEmpty(PsMetagameManager.m_playerStats.m_teamKickReason) && !PsMainMenuState.m_rankChangeComing && !PsMainMenuState.m_startHidden && PsMainMenuState.m_popup == null && PsState.m_dialogues.Count == 0)
		{
			flag2 = false;
			PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterKickedFromTeam), null, null, null, false, true, InitialPage.Center, false, false, false);
			PsMainMenuState.m_popup.SetAction("Continue", delegate
			{
				PsMainMenuState.m_popup.Destroy();
				PsMainMenuState.m_popup = null;
			});
			TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		else if (PlayerPrefsX.GetSeasonEnded() && PlayerPrefsX.GetTeamUnlocked() && !PsMainMenuState.m_rankChangeComing && !PsMainMenuState.m_startHidden && PsMainMenuState.m_popup == null && PsState.m_dialogues.Count == 0 && !flag3)
		{
			flag2 = false;
			PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterSeasonEnded), null, null, null, false, true, InitialPage.Center, false, false, false);
			PsMainMenuState.m_popup.SetAction("Continue", delegate
			{
				PsMainMenuState.m_popup.Destroy();
				PsMainMenuState.m_popup = null;
				PlayerPrefsX.SetSeasonEnded(false);
				PsMetagameManager.ClaimSeasonRewards();
			});
			TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		else if (PlayerPrefsX.GetSeasonEnded() && !PlayerPrefsX.GetTeamUnlocked() && !PsMainMenuState.m_startHidden && PsState.m_dialogues.Count == 0 && !flag3)
		{
			PlayerPrefsX.SetSeasonEnded(false);
			PsMetagameManager.m_playerStats.bigBangPoints = PsMetagameManager.m_seasonEndData.bigBangPoints;
			PsMetagameManager.m_playerStats.mcTrophies = PsMetagameManager.m_seasonEndData.mcTrophies;
			PsMetagameManager.m_playerStats.carTrophies = PsMetagameManager.m_seasonEndData.carTrophies;
			PsMetagameManager.ClaimSeasonRewards();
		}
		else if (!PsMainMenuState.m_startHidden && !PsMainMenuState.m_rankChangeComing && PlanetTools.m_planetProgressionInfos[PlanetTools.PlanetTypes.AdventureOffroadCar.ToString()].m_mainPath.m_currentNodeId > 12 && PsMainMenuState.m_popup == null && PsState.m_dialogues.Count == 0 && !flag3)
		{
			if (PsMetagameManager.m_playerStats.pendingSpecialOfferChests != null && PsMetagameManager.m_playerStats.pendingSpecialOfferChests.Count > 0)
			{
				PsMetagameManager.OpenSpecialOfferChest(PsMetagameManager.m_playerStats.pendingSpecialOfferChests[0]);
				PsMetagameManager.m_playerStats.pendingSpecialOfferChests.RemoveAt(0);
			}
			else
			{
				PsTimedSpecialOffer unstartedTimedSpecialOffer = PsMetagameManager.GetUnstartedTimedSpecialOffer();
				if (unstartedTimedSpecialOffer != null && PsIAPManager.ProductsReceived)
				{
					TouchAreaS.CancelAllTouches(null);
					flag2 = false;
					PsMetagameManager.StartSpecialOffer(unstartedTimedSpecialOffer);
					PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUITimedSpecialOfferPopup), null, null, null, false, true, InitialPage.Center, false, false, false);
					(PsMainMenuState.m_popup.m_mainContent as PsUITimedSpecialOfferPopup).CreateContent(unstartedTimedSpecialOffer);
					PsMainMenuState.m_popup.Update();
					PsMainMenuState.m_popup.SetAction("Exit", delegate
					{
						PsMainMenuState.m_popup.Destroy();
						PsMainMenuState.m_popup = null;
					});
					PsMetagameManager.m_menuResourceView.CreateSpecialOfferTimer();
					PsMetagameManager.m_menuResourceView.m_separateShopButton.Update();
					TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				}
			}
		}
		if (flag2 && !PsMainMenuState.m_rankChangeComing && !PlayerPrefsX.GetTeamUnlocked() && !PlayerPrefsX.GetTeamJoined() && (PsMetagameManager.m_playerStats.carRank >= 2 || PsMetagameManager.m_playerStats.mcRank >= 2) && !PsMainMenuState.m_startHidden && PsMainMenuState.m_popup == null && PsState.m_dialogues.Count == 0 && !flag3)
		{
			PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterTeamUnlocked), null, null, null, true, true, InitialPage.Center, false, false, false);
			PsMainMenuState.m_popup.SetAction("Exit", delegate
			{
				PsMainMenuState.ChangeToTeamState();
			});
			TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		PsMainMenuState.m_trophiesSet = false;
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x00099A90 File Offset: 0x00097E90
	public void CreateEventTimer()
	{
		this.m_activeTournamentEvent = PsMetagameManager.m_activeTournament;
		if (PsMetagameManager.m_doubleValueGoodOrBadEvent != null)
		{
			if (PsMetagameManager.m_activeTournament != null && (PsMetagameManager.m_activeTournament.timeLeft > 0 || (PsMetagameManager.m_activeTournament.tournament != null && PsMetagameManager.m_activeTournament.tournament.joined && !PsMetagameManager.m_activeTournament.tournament.claimed)))
			{
				EventMessage eventMessage = PsMetagameManager.m_activeTournament;
				PsUIEventTimer psUIEventTimer = new PsUITournamentTimer(PsMainMenuState.m_eventsButton, eventMessage, eventMessage.tournament.ownerName);
			}
			else
			{
				Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("hud_big_diamond_top", null);
				EventMessage eventMessage = PsMetagameManager.m_doubleValueGoodOrBadEvent;
				PsUIEventTimer psUIEventTimer = new PsUIEventTimer(PsMainMenuState.m_eventsButton, eventMessage, PsStrings.Get(StringID.DOUBLE_GEMS_POPUP), frame);
			}
		}
		else if (PsMetagameManager.m_activeTournament != null && PsMetagameManager.m_activeTournament.tournament != null)
		{
			EventMessage eventMessage = PsMetagameManager.m_activeTournament;
			PsUIEventTimer psUIEventTimer = new PsUITournamentTimer(PsMainMenuState.m_eventsButton, eventMessage, eventMessage.tournament.ownerName);
		}
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x00099B94 File Offset: 0x00097F94
	public static void CreateEventNode()
	{
		if (PlayerPrefsX.GetNameChanged())
		{
			bool flag = false;
			if (PsState.m_activeGameLoop != null)
			{
				flag = PsState.m_activeGameLoop.m_dialogues != null && ((PsState.m_activeGameLoop.m_nodeId == PsPlanetManager.GetCurrentPlanet().GetMainPathCurrentNodeId() - 1 && PsState.m_activeGameLoop.m_dialogues.ContainsKey(PsNodeEventTrigger.LoopReleased.ToString())) || (PsState.m_activeGameLoop.m_nodeId == PsPlanetManager.GetCurrentPlanet().GetMainPathCurrentNodeId() && PsState.m_activeGameLoop.m_dialogues.ContainsKey(PsNodeEventTrigger.LoopActivated.ToString())));
				if (!flag)
				{
					flag = PsGachaManager.IsSlotEmpty(PsGachaManager.SlotType.ADVENTURE) && PsMetagameManager.m_vehicleGachaData.m_adventureGachaCount == 0 && PsMetagameManager.m_vehicleGachaData.m_mapPieceCount >= PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax;
				}
			}
			if (PsMetagameManager.m_patchNotes != null && PsMetagameManager.m_patchNotes.showAsFloatingNode)
			{
				PsFloaters.CreateNewsDome(PsMetagameManager.m_patchNotes, false, false);
			}
			else if (PsMetagameManager.m_eventMessage != null && PsGameLoopNews.IsAvailableForPlayer(PsMetagameManager.m_eventMessage) && (double)PsMetagameManager.m_eventMessage.localStartTime <= Main.m_EPOCHSeconds && (double)PsMetagameManager.m_eventMessage.localEndTime >= Main.m_EPOCHSeconds)
			{
				if (PsMetagameManager.m_eventMessage.showAsFloatingNode)
				{
					if (Main.m_EPOCHSeconds - (double)PsMetagameManager.m_eventMessage.localEndTime <= 0.0)
					{
						PsFloaters.CreateNewsDome(PsMetagameManager.m_eventMessage, true, false);
					}
				}
				else if (PsMainMenuState.m_popup == null && PsState.m_dialogues.Count == 0 && !flag)
				{
					Type popupType = PsGameLoopNews.GetPopupType(PsMetagameManager.m_eventMessage);
					PsMainMenuState.m_popup = new PsUIBasePopup(popupType, null, null, null, false, true, InitialPage.Center, false, false, false);
					PsMainMenuState.m_popup.SetAction("Claim", delegate
					{
						PsMetagameManager.ClaimEventMessage(PsMetagameManager.m_eventMessage.messageId, new Action<HttpC>(PsMetagameManager.ClaimEventMessageSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimEventMessageFAILED), null);
						PsMetagameManager.m_eventMessage = null;
					});
					PsMainMenuState.m_popup.SetAction("Continue", delegate
					{
						PsMainMenuState.m_popup.Destroy();
						PsMainMenuState.m_popup = null;
						PsMetagameManager.ClaimEventMessage(PsMetagameManager.m_eventMessage.messageId, new Action<HttpC>(PsMetagameManager.ClaimEventMessageSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimEventMessageFAILED), null);
						PsMetagameManager.m_eventMessage = null;
					});
					PsMainMenuState.m_popup.SetAction("Exit", delegate
					{
						PsMainMenuState.m_popup.Destroy();
						PsMainMenuState.m_popup = null;
						PsMetagameManager.m_eventMessage = null;
					});
					(PsMainMenuState.m_popup.m_mainContent as PsUIEventMessagePopup).SetEventMessage(PsMetagameManager.m_eventMessage, false);
					PsMainMenuState.m_popup.Update();
					TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				}
			}
			if (PsMetagameManager.m_giftEvents != null)
			{
				EventMessage nextActiveGift = PsMetagameManager.m_giftEvents.GetNextActiveGift();
				if (nextActiveGift != null)
				{
					Debug.Log("Holiday: Create gift node: " + nextActiveGift.header, null);
					PsFloaters.CreateGiftNode(nextActiveGift);
				}
			}
		}
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x00099E8C File Offset: 0x0009828C
	private static void CheckNotificationPayload()
	{
		object objectFromNotification = Launch.GetObjectFromNotification("eventId");
		if (objectFromNotification != null)
		{
			int eventId = -1;
			try
			{
				eventId = Convert.ToInt32(objectFromNotification);
			}
			catch
			{
				Debug.LogError("BBR neweventId was not int");
			}
			if (eventId != -1)
			{
				EventMessage eventMessage = PsMetagameManager.m_eventList.Find((EventMessage c) => c.messageId == eventId);
				if (eventMessage != null)
				{
					eventMessage.showAtLogin = true;
					PsMetagameManager.m_patchNotes = eventMessage;
				}
				else
				{
					Debug.Log("BBR Eventmessage not found, id: " + eventId, null);
				}
			}
		}
		else
		{
			Debug.Log("BBR Did not contain eventId", null);
		}
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x00099F50 File Offset: 0x00098350
	private static void CreateNotification(UIComponent _parent, string _text)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, "notification", null, string.Empty);
		uicanvas.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.03f, -0.03f, -0.02f, 0.02f, RelativeTo.ScreenHeight);
		uicanvas.SetRogue();
		uicanvas.SetAlign(1f, 1f);
		uicanvas.SetDepthOffset(-10f);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas2.SetMargins(0.15f, RelativeTo.OwnHeight);
		uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
		TweenC tweenC = TweenS.AddTransformTween(uicanvas2.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x0009A064 File Offset: 0x00098464
	private static void CreateGachaList(UIComponent _parent, bool _update = false)
	{
		Vector3 vector = Vector3.zero;
		if (PsMainMenuState.m_gachaList != null)
		{
			vector = PsMainMenuState.m_gachaList.m_TC.transform.localPosition;
			PsMainMenuState.m_gachaList.Destroy();
		}
		PsMainMenuState.m_gachaList = new UIVerticalList(_parent, "GachaVerticalList");
		PsMainMenuState.m_gachaList.SetAlign(1f, 0f);
		PsMainMenuState.m_gachaList.SetSpacing(0.0225f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_gachaList.RemoveDrawHandler();
		if (PsMetagameManager.GetOpenedChestCount() > 0)
		{
			PsMainMenuState.m_freeGachaArea = new UICanvas(PsMainMenuState.m_gachaList, false, string.Empty, null, string.Empty);
			PsMainMenuState.m_freeGachaArea.SetHeight(PsMainMenuState.m_gachaUiHeight, RelativeTo.ScreenHeight);
			PsMainMenuState.m_freeGachaArea.SetWidth(PsMainMenuState.m_gachaUiHeight * PsMainMenuState.m_gachaWidthRatio, RelativeTo.ScreenHeight);
			PsMainMenuState.m_freeGachaArea.SetMargins(-0.01f, 0.01f, -0.01f, 0.01f, RelativeTo.ScreenHeight);
			PsMainMenuState.m_freeGachaArea.RemoveDrawHandler();
			PsMainMenuState.CreateFreeGacha();
		}
		if (!PsMainMenuState.m_lockRacing)
		{
			PsMainMenuState.CreateRacingGacha(false);
		}
		PsMainMenuState.m_adventureGachaArea = new UICanvas(PsMainMenuState.m_gachaList, false, string.Empty, null, string.Empty);
		PsMainMenuState.m_adventureGachaArea.SetHeight(PsMainMenuState.m_gachaUiHeight, RelativeTo.ScreenHeight);
		PsMainMenuState.m_adventureGachaArea.SetWidth(PsMainMenuState.m_gachaUiHeight * PsMainMenuState.m_gachaWidthRatio, RelativeTo.ScreenHeight);
		PsMainMenuState.m_adventureGachaArea.SetMargins(-0.01f, 0.01f, -0.01f, 0.01f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_adventureGachaArea.RemoveDrawHandler();
		PsMainMenuState.CreateAdventureGacha();
		if (_update)
		{
			PsMainMenuState.m_gachaList.Update();
			vector..ctor(vector.x, PsMainMenuState.m_gachaList.m_TC.transform.localPosition.y, PsMainMenuState.m_gachaList.m_TC.transform.localPosition.z);
			PsMainMenuState.m_gachaList.m_TC.transform.localPosition = vector;
		}
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x0009A244 File Offset: 0x00098644
	private static void UpgradeBubble(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] circle = DebugDraw.GetCircle(_c.m_actualHeight * 0.6f, 30, Vector2.zero);
		for (int i = 0; i < circle.Length; i++)
		{
			float num = 0f;
			float num2 = 0f;
			if (circle[i].x > 0f)
			{
				num = 1f;
				if (circle[i].y > 0f)
				{
					num2 = 0.0075f;
				}
				else if (circle[i].y < 0f)
				{
					num2 = -0.0025f;
				}
			}
			else if (circle[i].x < 0f)
			{
				num = -1f;
				if (circle[i].y > 0f)
				{
					num2 = -0.002f;
				}
				else if (circle[i].y < 0f)
				{
					num2 = -0.0035f;
				}
			}
			circle[i] += new Vector2(_c.m_actualHeight * 0.1f * num, _c.m_actualHeight * num2);
		}
		circle[12] += new Vector2(_c.m_actualHeight * -0.175f, _c.m_actualHeight * -0.095f);
		GGData ggdata = new GGData(circle);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 2f, circle, 0.15f * _c.m_actualHeight, Color.black, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, circle, 0.1f * _c.m_actualHeight, Color.white, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line6Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x0009A471 File Offset: 0x00098871
	public static void SetCurrentTrophies(int _amount)
	{
		PsMainMenuState.m_currentTrophyAmount = _amount;
		PsMainMenuState.m_trophiesSet = true;
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x0009A480 File Offset: 0x00098880
	private static void CreateAdventureGacha()
	{
		if (PsMainMenuState.m_adventureGachaArea == null)
		{
			return;
		}
		PsMainMenuState.m_adventureGachaArea.DestroyChildren();
		int slotIndex = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.ADVENTURE);
		PsMetagameManager.PsVehicleGachaData vehicleGachaData = PsMetagameManager.m_vehicleGachaData;
		PsMainMenuState.m_adventureGacha = new PsUIAdventureGacha(PsMainMenuState.m_adventureGachaArea, slotIndex);
		PsMainMenuState.m_adventureGachaArea.Update();
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x0009A4C9 File Offset: 0x000988C9
	private static void CreateFreeGacha()
	{
		if (PsMainMenuState.m_freeGachaArea == null)
		{
			return;
		}
		PsMainMenuState.m_freeGacha = new PsUIFreeGacha(PsMainMenuState.m_freeGachaArea);
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x0009A4E8 File Offset: 0x000988E8
	public static void CreateRacingGacha(bool _update = false)
	{
		if (PsMainMenuState.m_raceGachaArea != null)
		{
			PsMainMenuState.m_raceGachaArea.DestroyChildren();
		}
		PsMainMenuState.m_raceGachaArea = new UICanvas(PsMainMenuState.m_gachaList, false, string.Empty, null, string.Empty);
		PsMainMenuState.m_raceGachaArea.SetHeight(PsMainMenuState.m_gachaUiHeight, RelativeTo.ScreenHeight);
		PsMainMenuState.m_raceGachaArea.SetWidth(PsMainMenuState.m_gachaUiHeight * PsMainMenuState.m_gachaWidthRatio, RelativeTo.ScreenHeight);
		PsMainMenuState.m_raceGachaArea.SetMargins(-0.01f, 0.01f, -0.01f, 0.01f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_raceGachaArea.RemoveDrawHandler();
		int slotIndex = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.RACING);
		PsMainMenuState.m_racingGacha = new PsUIRacingGacha(PsMainMenuState.m_raceGachaArea, slotIndex);
		if (_update)
		{
			PsMainMenuState.m_raceGachaArea.Update();
		}
	}

	// Token: 0x06001064 RID: 4196 RVA: 0x0009A59C File Offset: 0x0009899C
	public static void CreateMCNotification()
	{
		UIComponent uicomponent = null;
		for (int i = 0; i < PsMainMenuState.m_vehicleButtons.Count; i++)
		{
			if (PsState.m_vehicleTypes[i] == typeof(Motorcycle))
			{
				uicomponent = PsMainMenuState.m_vehicleButtons[i];
				break;
			}
		}
		UICanvas uicanvas = new UICanvas(uicomponent, false, "notification", null, string.Empty);
		uicanvas.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.02f, -0.02f, -0.02f, 0.02f, RelativeTo.ScreenHeight);
		uicanvas.SetRogue();
		uicanvas.SetAlign(1f, 1f);
		uicanvas.SetDepthOffset(-10f);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas2.SetMargins(0.15f, RelativeTo.OwnHeight);
		uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
		TweenC tweenC = TweenS.AddTransformTween(uicanvas2.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.65f, 0f, false);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, "!", PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uicanvas.Update();
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x0009A704 File Offset: 0x00098B04
	public static void CreateExistingNotification()
	{
		UIComponent uicomponent = null;
		for (int i = 0; i < PsMainMenuState.m_vehicleButtons.Count; i++)
		{
			if (PsState.m_vehicleTypes[i] == typeof(OffroadCar))
			{
				uicomponent = PsMainMenuState.m_vehicleButtons[i];
				break;
			}
		}
		UICanvas uicanvas = new UICanvas(uicomponent, false, "notification", null, string.Empty);
		uicanvas.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.02f, -0.02f, -0.02f, 0.02f, RelativeTo.ScreenHeight);
		uicanvas.SetRogue();
		uicanvas.SetAlign(1f, 1f);
		uicanvas.SetDepthOffset(-10f);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas2.SetMargins(0.15f, RelativeTo.OwnHeight);
		uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
		TweenC tweenC = TweenS.AddTransformTween(uicanvas2.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.65f, 0f, false);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, "!", PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uicanvas.Update();
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x0009A86C File Offset: 0x00098C6C
	public static void UpdateUIInfo()
	{
		PsMainMenuState.m_raceButton.Hide();
		for (int i = 0; i < PsMainMenuState.m_vehicleButtons.Count; i++)
		{
			bool flag = (PsState.m_vehicleTypes[i] == typeof(Motorcycle) && PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && PlayerPrefsX.GetMotorcycleChecked()) || PsState.m_vehicleTypes[i] == typeof(OffroadCar);
			if (PsState.m_vehicleTypes[i] == PsState.GetCurrentVehicleType(false))
			{
				PsMainMenuState.m_vehicleButtons[i].SetBlueColors(false);
				PsMainMenuState.m_vehicleButtons[i].DisableTouchAreas(true);
				PsMainMenuState.m_vehicleButtons[i].DestroyChildren(1);
			}
			else
			{
				PsMainMenuState.m_vehicleButtons[i].SetBlueColors(true);
				PsMainMenuState.m_vehicleButtons[i].EnableTouchAreas(true);
				int upgradeableItemCount = PsUpgradeManager.GetUpgradeableItemCount(PsState.m_vehicleTypes[i]);
				if (flag && upgradeableItemCount > 0)
				{
					UICanvas uicanvas = new UICanvas(PsMainMenuState.m_vehicleButtons[i], false, "notification", null, string.Empty);
					uicanvas.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
					uicanvas.SetMargins(0f, 0f, 0f, 0f, RelativeTo.OwnHeight);
					uicanvas.SetRogue();
					uicanvas.SetAlign(1f, 1f);
					uicanvas.SetDepthOffset(-10f);
					uicanvas.RemoveDrawHandler();
					UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
					uicanvas2.SetSize(1f, 1f, RelativeTo.ParentHeight);
					uicanvas2.SetMargins(0.15f, RelativeTo.OwnHeight);
					uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
					TweenC tweenC = TweenS.AddTransformTween(uicanvas2.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
					TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
					UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, upgradeableItemCount.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
				}
			}
			PsMainMenuState.m_vehicleButtons[i].Update();
		}
		(PsMainMenuState.m_vehicleButtons[0].m_parent as UIVerticalList).ArrangeContents();
		PsMainMenuState.ResetTrophyCumulateValues();
		PsMainMenuState.m_lockRacing = !PlayerPrefsX.GetOffroadRacing();
		PsMainMenuState.m_lockAll = !PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && PsState.GetCurrentVehicleType(false) == typeof(Motorcycle);
		string name = PsMetagameData.m_leagueData[PsMetagameData.GetCurrentLeagueIndex()].m_name;
		PsMainMenuState.m_leagueName.SetText(name);
		PsMainMenuState.m_currentTrophyAmount = PsMetagameManager.m_playerStats.trophies;
		string text = "menu_vehicle_logo_offroader";
		if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
		{
			text = "menu_vehicle_logo_dirtbike";
		}
		PsMainMenuState.m_vehicleIcon.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame(text, null));
		PsMainMenuState.m_vehicleIcon.SetHeight(0.05f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_vehicleIcon.m_parent.Update();
		PsMainMenuState.m_playerLeagueBanner.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame(PsMetagameData.m_leagueData[PsMetagameData.GetCurrentLeagueIndex()].m_bannerSprite, null));
		PsMainMenuState.m_playerLeagueBanner.SetHeight(0.16f, RelativeTo.ScreenHeight);
		PsMainMenuState.m_playerLeagueBanner.Update();
		if (PsMetagameData.GetNextLeague() == null)
		{
			PsMainMenuState.m_progressAmount = 1f;
		}
		else
		{
			int num = PsMainMenuState.m_currentTrophyAmount - PsMetagameData.GetCurrentLeague().m_trophyLimit;
			int num2 = PsMetagameData.GetNextLeague().m_trophyLimit - PsMetagameData.GetCurrentLeague().m_trophyLimit;
			PsMainMenuState.m_progressAmount = (float)num / (float)num2;
		}
		PsMainMenuState.m_progress.d_Draw(PsMainMenuState.m_progress);
		PsMainMenuState.m_trophyCount.SetText(PsMetagameManager.m_playerStats.trophies.ToString());
		PsMainMenuState.m_trophyCount.Update();
		PsMainMenuState.m_trophyCount.m_parent.ArrangeContents();
		int upgradeableItemCount2 = PsUpgradeManager.GetUpgradeableItemCount(PsState.GetCurrentVehicleType(false));
		if (upgradeableItemCount2 > 0 && !PsMainMenuState.m_lockAll)
		{
			if (PsMainMenuState.m_notificationBase != null)
			{
				PsMainMenuState.m_notificationBase.Destroy();
				PsMainMenuState.m_notificationBase = null;
			}
			PsMainMenuState.m_notificationBase = new UICanvas(PsMainMenuState.m_modelHolder, false, "notification", null, string.Empty);
			PsMainMenuState.m_notificationBase.SetSize(0.1f, 0.1f, RelativeTo.ScreenHeight);
			PsMainMenuState.m_notificationBase.SetAlign(0.865f, 0.65f);
			PsMainMenuState.m_notificationBase.SetDepthOffset(-10f);
			PsMainMenuState.m_notificationBase.SetDrawHandler(new UIDrawDelegate(PsMainMenuState.UpgradeBubble));
			TweenC tweenC2 = TweenS.AddTransformTween(PsMainMenuState.m_notificationBase.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.65f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC2, -1, true, TweenStyle.CubicInOut);
			UIFittedSprite uifittedSprite = new UIFittedSprite(PsMainMenuState.m_notificationBase, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_upgrade", null), true, true);
			uifittedSprite.SetHeight(0.075f, RelativeTo.ScreenHeight);
			uifittedSprite.SetAlign(0.2f, 0.2f);
			uifittedSprite.SetDepthOffset(-3f);
			UICanvas uicanvas3 = new UICanvas(PsMainMenuState.m_notificationBase, false, string.Empty, null, string.Empty);
			uicanvas3.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
			uicanvas3.SetAlign(1f, 1f);
			uicanvas3.SetMargins(0.02f, -0.02f, -0.02f, 0.02f, RelativeTo.ScreenHeight);
			uicanvas3.SetDepthOffset(-3f);
			uicanvas3.RemoveDrawHandler();
			UICanvas uicanvas4 = new UICanvas(uicanvas3, false, string.Empty, null, string.Empty);
			uicanvas4.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
			uicanvas4.SetAlign(1f, 1f);
			uicanvas4.SetMargins(0.15f, RelativeTo.OwnHeight);
			uicanvas4.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
			UIFittedText uifittedText2 = new UIFittedText(uicanvas4, false, string.Empty, upgradeableItemCount2.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
			PsMainMenuState.m_modelHolder.Update();
		}
		else if (PsMainMenuState.m_notificationBase != null)
		{
			PsMainMenuState.m_notificationBase.Destroy();
			PsMainMenuState.m_notificationBase = null;
		}
		if (!PsMainMenuState.m_lockRacing && PsMainMenuState.m_racingGacha == null)
		{
			PsMainMenuState.CreateGachaList(PsMainMenuState.m_uiCanvas, true);
		}
		PsMainMenuState.UpdateLockables(true);
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x0009AF04 File Offset: 0x00099304
	public static void UpdateLockables(bool _tween = true)
	{
		PsMainMenuState.m_lockRacing = !PlayerPrefsX.GetOffroadRacing();
		PsMainMenuState.m_lockAll = !PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && PsState.GetCurrentVehicleType(false) == typeof(Motorcycle);
		if (PsMainMenuState.m_lockAll || PsMainMenuState.m_lockRacing)
		{
			EntityManager.SetActivityOfEntity(PsMainMenuState.m_racingArea.m_TC.p_entity, false, true, true, true, true, true);
			EntityManager.SetActivityOfEntity(PsMainMenuState.m_leagueButton.m_TC.p_entity, false, true, true, true, true, true);
		}
		else
		{
			EntityManager.SetActivityOfEntity(PsMainMenuState.m_racingArea.m_TC.p_entity, true, true, true, true, true, true);
			EntityManager.SetActivityOfEntity(PsMainMenuState.m_leagueButton.m_TC.p_entity, true, true, true, true, true, true);
		}
		if (PsMainMenuState.m_lockRacing && !PsMainMenuState.m_lockAll && PsMainMenuState.m_lockedRace == null)
		{
			PsMainMenuState.m_lockedRace = new UIVerticalList(PsMainMenuState.m_vehicleInfoArea, string.Empty);
			PsMainMenuState.m_lockedRace.SetDepthOffset(-25f);
			PsMainMenuState.m_lockedRace.SetWidth(0.4f, RelativeTo.ScreenHeight);
			PsMainMenuState.m_lockedRace.SetRogue();
			PsMainMenuState.m_lockedRace.SetAlign(0.65f, 0f);
			PsMainMenuState.m_lockedRace.SetMargins(0f, 0f, 0f, 0.03f, RelativeTo.ScreenHeight);
			PsMainMenuState.m_lockedRace.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(PsMainMenuState.m_lockedRace, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_lock", null), true, true);
			uifittedSprite.SetHeight(0.08f, RelativeTo.ScreenHeight);
			string text = PsStrings.Get(StringID.UNLOCK_RACE).Replace("\n", " ");
			UITextbox uitextbox = new UITextbox(PsMainMenuState.m_lockedRace, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, "000000");
			PsMainMenuState.m_lockedRace.Update();
		}
		else if ((!PsMainMenuState.m_lockRacing || PsMainMenuState.m_lockAll) && PsMainMenuState.m_lockedRace != null)
		{
			PsMainMenuState.m_lockedRace.Destroy();
			PsMainMenuState.m_lockedRace = null;
		}
		if (PsMainMenuState.m_lockAll)
		{
			PsMainMenuState.CreateDisabledCanvas(_tween);
			if (PsMainMenuState.m_notificationBase != null)
			{
				PsMainMenuState.m_notificationBase.Destroy();
				PsMainMenuState.m_notificationBase = null;
			}
			PsMainMenuState.m_garageTouchArea.DisableTouchAreas(true);
			PsMainMenuState.m_modelCanvas.m_material.shader = Shader.Find("WOE/Fx/GreyscaleUnlitAlpha");
			PsMainMenuState.m_modelCanvas.m_material.SetColor("_TintColor", DebugDraw.HexToColor("1D3D08"));
		}
		else
		{
			PsMainMenuState.m_garageTouchArea.EnableTouchAreas(true);
			PsMainMenuState.m_modelCanvas.m_material.shader = Shader.Find("Framework/VertexColorUnlitDoubleRenderTexture");
		}
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x0009B1B4 File Offset: 0x000995B4
	private static void CreateDisabledCanvas(bool _tween = true)
	{
		if (PsMainMenuState.m_disabledCanvas == null)
		{
			PsMainMenuState.m_disabledCanvas = new UICanvas(PsMainMenuState.m_uiCanvas, true, "touchDisabler", null, string.Empty);
			PsMainMenuState.m_disabledCanvas.SetHeight(1f, RelativeTo.ScreenHeight);
			PsMainMenuState.m_disabledCanvas.SetWidth(1f, RelativeTo.ScreenWidth);
			PsMainMenuState.m_disabledCanvas.SetDepthOffset(50f);
			PsMainMenuState.m_disabledCanvas.RemoveDrawHandler();
			PrefabC prefabC = PrefabS.CreateRect(PsMainMenuState.m_disabledCanvas.m_TC, Vector3.zero, (float)Screen.width, (float)Screen.height, Color.black, new Material(Shader.Find("WOE/Unlit/ColorUnlitTransparentBg")), PsMainMenuState.m_disabledCanvas.m_camera);
			prefabC.p_gameObject.GetComponent<Renderer>().material.color = Color.black * 0.65f;
			if (!_tween)
			{
				UIVerticalList uiverticalList = new UIVerticalList(PsMainMenuState.m_disabledCanvas, string.Empty);
				uiverticalList.RemoveDrawHandler();
				uiverticalList.SetAlign(0.6f, 0.35f);
				uiverticalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
				uiverticalList.SetWidth(0.6f, RelativeTo.ScreenWidth);
				uiverticalList.SetDepthOffset(-24f);
				UIFittedSprite uifittedSprite = new UIFittedSprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_lock", null), true, true);
				uifittedSprite.SetHeight(0.115f, RelativeTo.ScreenHeight);
				UIText uitext = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.UNLOCK_BIKE_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, "#a7e0ff", "#000000");
				UIText uitext2 = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.UNLOCK_BIKE).Replace("%1", "250cc"), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, null, "#000000");
				IAPProduct iapproductById = PsIAPManager.GetIAPProductById("dirt_bike_bundle");
				if (iapproductById != null)
				{
					UIText uitext3 = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.SHOP_GET_IT_NOW).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, "#ffd200", "#000000");
					PsMainMenuState.m_dirtBikebundlebutton = new UIRectSpriteButton(uiverticalList, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_banner_dirtbike_bundle", null), true, false);
					PsMainMenuState.m_dirtBikebundlebutton.SetSize(0.6f, PsMainMenuState.m_dirtBikebundlebutton.m_frame.height / PsMainMenuState.m_dirtBikebundlebutton.m_frame.width * 0.6f, RelativeTo.ScreenWidth);
					PsMainMenuState.m_dirtBikebundlebutton.SetDepthOffset(-25f);
					UIVerticalList uiverticalList2 = new UIVerticalList(PsMainMenuState.m_dirtBikebundlebutton, string.Empty);
					uiverticalList2.RemoveDrawHandler();
					uiverticalList2.SetSpacing(-0.05f, RelativeTo.ParentHeight);
					uiverticalList2.SetVerticalAlign(0.03f);
					UIText uitext4 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.SHOP_BUNDLE).ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.21f, RelativeTo.ParentHeight, "#6f2301", null);
					UIText uitext5 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.SHOP_TAP_INFO), PsFontManager.GetFont(PsFonts.HurmeBold), 0.13f, RelativeTo.ParentHeight, "#ff3300", null);
				}
			}
			PsMainMenuState.m_disabledCanvas.Update();
			if (_tween)
			{
				TweenC tweenC = TweenS.AddTransformTween(PsMainMenuState.m_disabledCanvas.m_TC, TweenedProperty.Alpha, TweenStyle.CubicIn, Vector3.zero, Vector3.one * 0.65f, 1f, 0f, true);
				TweenS.SetTweenAlphaProperties(tweenC, false, true, false, null);
				TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
				{
					UIVerticalList uiverticalList3 = new UIVerticalList(PsMainMenuState.m_disabledCanvas, string.Empty);
					uiverticalList3.RemoveDrawHandler();
					uiverticalList3.SetAlign(0.6f, 0.35f);
					uiverticalList3.SetSpacing(0.03f, RelativeTo.ScreenHeight);
					UIFittedSprite uifittedSprite2 = new UIFittedSprite(uiverticalList3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_lock", null), true, true);
					uifittedSprite2.SetHeight(0.115f, RelativeTo.ScreenHeight);
					UIText uitext6 = new UIText(uiverticalList3, false, string.Empty, PsStrings.Get(StringID.UNLOCK_BIKE_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.06f, RelativeTo.ScreenHeight, "#a7e0ff", "000000");
					UIText uitext7 = new UIText(uiverticalList3, false, string.Empty, PsStrings.Get(StringID.UNLOCK_BIKE).Replace("%1", "250cc"), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, null, "000000");
					IAPProduct iapproductById2 = PsIAPManager.GetIAPProductById("dirt_bike_bundle");
					if (iapproductById2 != null)
					{
						UIText uitext8 = new UIText(uiverticalList3, false, string.Empty, PsStrings.Get(StringID.SHOP_GET_IT_NOW).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, "#ffd200", "#000000");
						PsMainMenuState.m_dirtBikebundlebutton = new UIRectSpriteButton(uiverticalList3, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_banner_dirtbike_bundle", null), true, false);
						PsMainMenuState.m_dirtBikebundlebutton.SetSize(0.55f, PsMainMenuState.m_dirtBikebundlebutton.m_frame.height / PsMainMenuState.m_dirtBikebundlebutton.m_frame.width * 0.55f, RelativeTo.ScreenWidth);
						PsMainMenuState.m_dirtBikebundlebutton.SetDepthOffset(-25f);
						UIVerticalList uiverticalList4 = new UIVerticalList(PsMainMenuState.m_dirtBikebundlebutton, string.Empty);
						uiverticalList4.RemoveDrawHandler();
						uiverticalList4.SetSpacing(-0.05f, RelativeTo.ParentHeight);
						uiverticalList4.SetVerticalAlign(0.03f);
						UIText uitext9 = new UIText(uiverticalList4, false, string.Empty, PsStrings.Get(StringID.SHOP_BUNDLE).ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.21f, RelativeTo.ParentHeight, "#6f2301", null);
						UIText uitext10 = new UIText(uiverticalList4, false, string.Empty, PsStrings.Get(StringID.SHOP_TAP_INFO), PsFontManager.GetFont(PsFonts.HurmeBold), 0.13f, RelativeTo.ParentHeight, "#ff3300", null);
					}
					uiverticalList3.Update();
				});
			}
		}
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x0009B520 File Offset: 0x00099920
	private static void RemoveDisabledCanvas()
	{
		if (PsMainMenuState.m_disabledCanvas != null)
		{
			TweenC tweenC = TweenS.AddTransformTween(PsMainMenuState.m_disabledCanvas.m_TC, TweenedProperty.Alpha, TweenStyle.CubicIn, Vector3.one * 0.65f, Vector3.zero, 0.1f, 0f, true);
			TweenS.SetTweenAlphaProperties(tweenC, false, true, false, null);
			TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
			{
				PsMainMenuState.m_disabledCanvas.Destroy();
				PsMainMenuState.m_disabledCanvas = null;
			});
		}
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x0009B598 File Offset: 0x00099998
	private static List<UIComponent> GetHideableUIComponents()
	{
		List<UIComponent> list = new List<UIComponent>();
		if (PsMainMenuState.m_vehicleInfoArea != null)
		{
			list.Add(PsMainMenuState.m_vehicleInfoArea);
		}
		if (PsMainMenuState.m_vehicleButtonArea != null)
		{
			list.Add(PsMainMenuState.m_vehicleButtonArea);
		}
		return list;
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x0009B5D8 File Offset: 0x000999D8
	public static List<UIComponent> GetAllUIComponents()
	{
		List<UIComponent> list = new List<UIComponent>();
		if (PsMainMenuState.m_topLeftArea != null)
		{
			list.Add(PsMainMenuState.m_topLeftArea);
		}
		if (PsMainMenuState.m_vehicleInfoArea != null)
		{
			list.Add(PsMainMenuState.m_vehicleInfoArea);
		}
		if (PsMainMenuState.m_gachaList != null)
		{
			list.Add(PsMainMenuState.m_gachaList);
		}
		if (PsMainMenuState.m_vehicleButtonArea != null)
		{
			list.Add(PsMainMenuState.m_vehicleButtonArea);
		}
		if (PsMetagameManager.m_menuResourceView != null)
		{
			list.Add(PsMetagameManager.m_menuResourceView);
		}
		if (PsMainMenuState.m_settingsButton != null)
		{
			list.Add(PsMainMenuState.m_settingsButton);
		}
		return list;
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x0009B66C File Offset: 0x00099A6C
	public static void ShowUI(bool _tween = true, List<UIComponent> _uiList = null)
	{
		if (!PsMainMenuState.m_startHidden)
		{
			List<UIComponent> list = ((_uiList == null) ? PsMainMenuState.GetHideableUIComponents() : _uiList);
			for (int i = 0; i < list.Count; i++)
			{
				TransformC tc = list[i].m_TC;
				Vector3 vector = CameraS.m_uiCamera.WorldToViewportPoint(tc.transform.position);
				int num = (((int)(vector.x + 0.5f) != 0) ? (-1) : 1);
				if (vector.x < 0f || vector.x > 1f)
				{
					Vector3 vector2 = tc.transform.localPosition + Vector3.right * (float)Screen.width * (float)num;
					if (_tween)
					{
						TweenS.AddTransformTween(tc, TweenedProperty.Position, TweenStyle.QuadInOut, vector2, 0.5f, 0f, true);
					}
					else
					{
						tc.transform.localPosition = vector2;
					}
				}
			}
		}
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x0009B76C File Offset: 0x00099B6C
	public static void HideUI(bool _tween = true, bool _updateUI = false, Action _tweenEndAction = null, bool _hideJustLeftSide = false, List<UIComponent> _uiList = null)
	{
		if (!PsMainMenuState.m_startHidden)
		{
			List<UIComponent> list = ((_uiList == null) ? PsMainMenuState.GetHideableUIComponents() : _uiList);
			for (int i = 0; i < list.Count; i++)
			{
				TransformC tc = list[i].m_TC;
				Vector3 vector = CameraS.m_uiCamera.WorldToViewportPoint(tc.transform.position);
				int num = (int)(vector.x + 0.5f);
				int num2 = ((num != 0) ? num : (-1));
				if (vector.x > 0f && vector.x < 1f && (!_hideJustLeftSide || num2 != 1))
				{
					Vector3 vector2 = tc.transform.localPosition + Vector3.right * (float)Screen.width * (float)num2;
					if (_tween)
					{
						TweenC tweenC = TweenS.AddTransformTween(tc, TweenedProperty.Position, TweenStyle.QuadInOut, vector2, 0.49f, 0f, true);
						if (i == 0 && _updateUI)
						{
							TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC t)
							{
								if (_tweenEndAction != null)
								{
									_tweenEndAction.Invoke();
								}
								PsMainMenuState.UpdateUIInfo();
							});
						}
					}
					else
					{
						tc.transform.localPosition = vector2;
						if (i == list.Count - 1 && _updateUI)
						{
							PsMainMenuState.UpdateUIInfo();
						}
					}
				}
			}
		}
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x0009B8C4 File Offset: 0x00099CC4
	public static void CumulateTrophies(int _amount)
	{
		PsMainMenuState.m_cumulateTrophyText = true;
		PsMainMenuState.m_trophyCumulateAmount = _amount;
		PsMainMenuState.m_trophyCumulateDur = 45;
		PsMainMenuState.m_trophyCumulateCur = 0;
		int leagueIndex = PsMetagameData.GetLeagueIndex(PsMainMenuState.m_currentTrophyAmount);
		int leagueIndex2 = PsMetagameData.GetLeagueIndex(PsMetagameManager.m_playerStats.trophies);
		if (leagueIndex != leagueIndex2)
		{
			TouchAreaS.Disable();
			PsMainMenuState.m_rankChange = true;
			int num = PsMainMenuState.m_currentTrophyAmount - PsMetagameData.GetLeague(leagueIndex).m_trophyLimit;
			int num2 = num;
			if (leagueIndex < PsMetagameData.m_leagueData.Count - 1)
			{
				num2 = PsMetagameData.GetLeague(leagueIndex + 1).m_trophyLimit - PsMetagameData.GetLeague(leagueIndex).m_trophyLimit;
			}
			PsMainMenuState.m_progressAmount = (float)num / (float)num2;
			if (leagueIndex2 - leagueIndex > 0)
			{
				TouchAreaS.Disable();
				PsMainMenuState.m_remainingAmount = _amount - (PsMetagameData.GetLeague(leagueIndex2).m_trophyLimit - PsMainMenuState.m_currentTrophyAmount);
				PsMainMenuState.m_trophyCumulateAmount -= PsMainMenuState.m_remainingAmount;
				PsMainMenuState.m_progDiff = 1f - PsMainMenuState.m_progressAmount;
			}
			else
			{
				PsMainMenuState.m_remainingAmount = _amount + (PsMainMenuState.m_currentTrophyAmount - PsMetagameData.GetLeague(leagueIndex2 + 1).m_trophyLimit);
				PsMainMenuState.m_trophyCumulateAmount -= PsMainMenuState.m_remainingAmount;
				PsMainMenuState.m_progDiff = 0f - PsMainMenuState.m_progressAmount;
			}
		}
		else if (leagueIndex < PsMetagameData.m_leagueData.Count - 1)
		{
			int num3 = PsMainMenuState.m_currentTrophyAmount - PsMetagameData.GetLeague(leagueIndex).m_trophyLimit;
			int num4 = PsMetagameData.GetLeague(leagueIndex + 1).m_trophyLimit - PsMetagameData.GetLeague(leagueIndex).m_trophyLimit;
			PsMainMenuState.m_progressAmount = (float)num3 / (float)num4;
			float num5 = (float)(PsMetagameManager.m_playerStats.trophies - PsMetagameData.GetLeague(leagueIndex).m_trophyLimit) / (float)(PsMetagameData.GetLeague(leagueIndex + 1).m_trophyLimit - PsMetagameData.GetLeague(leagueIndex).m_trophyLimit);
			PsMainMenuState.m_progDiff = num5 - PsMainMenuState.m_progressAmount;
		}
		PsMainMenuState.m_startProg = PsMainMenuState.m_progressAmount;
		if (PsMainMenuState.m_progress.m_TC.m_active)
		{
			PsMainMenuState.m_progress.d_Draw(PsMainMenuState.m_progress);
		}
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x0009BAAC File Offset: 0x00099EAC
	private void ProgressDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = _c.m_actualWidth - 0.008f * (float)Screen.height;
		float num2 = _c.m_actualHeight - 0.008f * (float)Screen.height;
		float num3 = _c.m_actualWidth - 0.004f * (float)Screen.height;
		float num4 = _c.m_actualHeight - 0.004f * (float)Screen.height;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(num3, num4, _c.m_actualHeight * 0.3f, 8, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(num, num2, _c.m_actualHeight * 0.2f, 8, Vector2.zero);
		Vector2[] roundedRect3 = DebugDraw.GetRoundedRect(num * PsMainMenuState.m_progressAmount, num2, _c.m_actualHeight * 0.2f, 8, new Vector2(num * PsMainMenuState.m_progressAmount * 0.5f - 0.5f * num, 0f));
		Color color = DebugDraw.HexToColor("#0c61c9");
		Color color2 = DebugDraw.HexToColor("#4db9f5");
		color = DebugDraw.HexToColor("#555555");
		color2 = DebugDraw.HexToColor("#4b4b4b");
		Color white = Color.white;
		Color color3 = DebugDraw.HexToColor("000000");
		Color color4 = DebugDraw.HexToColor("#202020");
		Color color5 = DebugDraw.HexToColor("#ea7b0d");
		Color color6 = DebugDraw.HexToColor("#f9cd2f");
		white.a = 0.4f;
		GGData ggdata = new GGData(roundedRect2);
		GGData ggdata2 = new GGData(roundedRect3);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, roundedRect, 0.2f * _c.m_actualHeight, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1.5f, roundedRect2, 0.15f * _c.m_actualHeight, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		if (PsMainMenuState.m_progressAmount > 0f)
		{
			PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 1f, ggdata2, color6, color5, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f, roundedRect3, 0.15f * _c.m_actualHeight, color6, color5, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		}
		DebugDraw.ScaleVectorArray(roundedRect2, new Vector2(0.98f, 0.95f));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, roundedRect2, 0.2f * _c.m_actualHeight, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x0009BD94 File Offset: 0x0009A194
	private void VehicleBGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth * 0.9f, _c.m_actualHeight * 0.6f, 0.015f * (float)Screen.height, 8, new Vector2(0f, _c.m_actualHeight * -0.15f));
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#204643");
		Color color2 = DebugDraw.HexToColor("#204643");
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, roundedRect, 0.008f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x0009BE70 File Offset: 0x0009A270
	private void BackgroundDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect((float)Screen.width * 0.495f, (float)Screen.height * 0.995f, 0.025f * (float)Screen.height, 6, new Vector2((float)Screen.width * 0.25f, 0f));
		GGData ggdata = new GGData(rect);
		ggdata.EnsureLoop();
		GGData ggdata2 = new GGData(roundedRect);
		ggdata2.ReverseVertexOrder();
		ggdata2.Add(ggdata);
		Color color = DebugDraw.HexToColor("#152323");
		Color black = Color.black;
		black.a = 0.65f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 38f, roundedRect, 0.04f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		DebugDraw.ScaleVectorArray(roundedRect, new Vector2(0.955f, 0.975f));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 39f + Vector3.right * 0.01f * (float)Screen.width, roundedRect, 0.035f * (float)Screen.height, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 40f, ggdata2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x0009C000 File Offset: 0x0009A400
	private void SetVehicle(int _index)
	{
		_index = Mathf.Clamp(_index, 0, PsState.m_vehicleTypes.Length - 1);
		string text = PsState.m_vehicleTypes[_index].ToString();
		PsUpgradeableEditorItem psUpgradeableEditorItem = PsMetagameData.GetUnlockableByIdentifier(text) as PsUpgradeableEditorItem;
		PsState.SetVehicleIndex(_index);
		this.LoadVehicle();
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x0009C044 File Offset: 0x0009A444
	private void ChangeVehicle(int _index)
	{
		SoundS.PlaySingleShot("/UI/ButtonChangeVehicle", Vector3.zero, 1f);
		TouchAreaS.Disable();
		Action action = delegate
		{
			_index = Mathf.Clamp(_index, 0, PsState.m_vehicleTypes.Length - 1);
			string text = PsState.m_vehicleTypes[_index].ToString();
			PsUpgradeableEditorItem psUpgradeableEditorItem = PsMetagameData.GetUnlockableByIdentifier(text) as PsUpgradeableEditorItem;
			PsState.SetVehicleIndex(_index);
			this.LoadVehicle();
			if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle) && PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && !PlayerPrefsX.GetMotorcycleChecked())
			{
				PlayerPrefsX.SetMotorcycleChecked(true);
				PsMainMenuState.m_vehicleButtons[PsState.GetVehicleIndex()].DestroyChildren(1);
			}
		};
		if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar) && PlayerPrefsX.GetExistingNotify())
		{
			PlayerPrefsX.SetExistingNotify(false);
			PsMainMenuState.m_vehicleButtons[PsState.GetVehicleIndex()].DestroyChildren(1);
		}
		PsMainMenuState.HideUI(true, true, action, false, null);
		PsMainMenuState.RemoveDisabledCanvas();
		PsPlanetManager.GetCurrentPlanet().ChangePlanetSequence();
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x0009C0E0 File Offset: 0x0009A4E0
	private void LoadVehicle()
	{
		if (this.m_vehiclePrefab != null)
		{
			PrefabS.RemoveComponent(this.m_vehiclePrefab, true);
			this.m_vehiclePrefab = null;
		}
		Vector3 vector = ((PsState.GetCurrentVehicleType(false) != typeof(OffroadCar)) ? new Vector3(0f, 7f, 0f) : (Vector3.right * 9f));
		this.UpdateUpgradeValues();
		Type currentVehicleType = PsState.GetCurrentVehicleType(false);
		this.m_vehiclePrefab = PsMainMenuState.m_modelCanvas.AddGameObject(ResourceManager.GetGameObject(currentVehicleType.ToString() + "Prefab_GameObject"), vector, default(Vector3));
		Transform transform = this.FindChild(this.m_vehiclePrefab.p_gameObject.transform, "Parts");
		if (transform != null)
		{
			transform.gameObject.SetActive(false);
		}
		foreach (KeyValuePair<string, int> keyValuePair in this.m_upgrades)
		{
			this.CheckVisualUpgrade(keyValuePair.Key, keyValuePair.Value);
		}
		this.CreateCharacter();
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetVehicleCustomisationData(currentVehicleType).GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.TRAIL);
		if (installedItemByCategory != null)
		{
			this.SetTrail(installedItemByCategory.m_identifier);
		}
		else
		{
			this.SetTrail(string.Empty);
		}
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x0009C250 File Offset: 0x0009A650
	private void CreateCharacter()
	{
		if (this.m_characterPrefab != null)
		{
			PrefabS.RemoveComponent(this.m_characterPrefab, true);
			this.m_characterPrefab = null;
			if (this.m_shadow != null)
			{
				Object.Destroy(this.m_shadow.GetComponent<Renderer>().material);
				Object.Destroy(this.m_shadow.gameObject);
			}
		}
		this.m_characterPrefab = PsMainMenuState.m_modelCanvas.AddGameObject(ResourceManager.GetGameObject("AlienNewPrefab_GameObject"), Vector3.zero, default(Vector3));
		this.DisableColliderRenderers(this.m_characterPrefab.p_gameObject.transform);
		this.m_animator = this.m_characterPrefab.p_gameObject.GetComponent<Animator>();
		this.m_animatorController = ResourceManager.GetResource<RuntimeAnimatorController>(RESOURCE.AlienAnimatorController_AnimatorController);
		this.m_animator.runtimeAnimatorController = this.m_animatorController;
		this.m_characterPrefab.p_gameObject.transform.Rotate(0f, 90f, 0f);
		Transform transform;
		if (PsState.GetVehicleIndex() == 0)
		{
			transform = this.m_vehiclePrefab.p_gameObject.transform.Find("OffroadCar/CharacterLocator");
			this.m_drivePoseState = Animator.StringToHash("Base.Drive");
		}
		else
		{
			transform = this.m_vehiclePrefab.p_gameObject.transform.Find("DirtBike/CharacterLocator");
			this.m_drivePoseState = Animator.StringToHash("Base.DriveMoto");
		}
		this.m_shadow = this.m_vehiclePrefab.p_gameObject.transform.Find("Shadow");
		this.m_shadow.gameObject.SetActive(true);
		this.m_shadow.parent = this.m_vehiclePrefab.p_parentTC.transform;
		Renderer component = this.m_shadow.GetComponent<Renderer>();
		component.material.shader = Shader.Find("WOE/Units/Illumin-Alpha");
		component.material.color = Color.black;
		component.material.SetFloat("_Emission", 1f);
		this.m_characterPrefab.p_gameObject.transform.SetParent(transform, false);
		this.m_bindPoseState = Animator.StringToHash("Base.Bind");
		this.m_standPoseState = Animator.StringToHash("Base.Stand");
		this.m_animator.Play(this.m_drivePoseState);
		this.m_alienEffects = this.m_characterPrefab.p_gameObject.GetComponent<AlienEffects>();
		if (this.m_alienEffects != null)
		{
			this.m_alienEffects.Initialize();
		}
		PsCustomisationData vehicleCustomisationData = PsCustomisationManager.GetVehicleCustomisationData(PsState.GetCurrentVehicleType(false));
		PsCustomisationItem installedItemByCategory = vehicleCustomisationData.GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		string text = string.Empty;
		if (installedItemByCategory != null)
		{
			text = installedItemByCategory.m_identifier;
		}
		this.SetPropToLocator(text, "Hips/Spine1/Spine2/Neck/Head/HeadCollider/HeadGearLocator");
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x0009C4F8 File Offset: 0x0009A8F8
	private void SetTrail(string _identifier)
	{
		GameObject trailPrefabByIdentifier = PsCustomisationManager.GetTrailPrefabByIdentifier(_identifier);
		if (this.m_trailBase != null)
		{
			this.m_trailBase.Destroy();
			this.m_trailBase = null;
			Object.Destroy(this.m_trail);
			this.m_trail = null;
		}
		if (trailPrefabByIdentifier != null)
		{
			this.m_trail = Object.Instantiate<GameObject>(trailPrefabByIdentifier);
			Vector3 position = this.m_trail.transform.position;
			this.m_trail.transform.parent = this.m_vehiclePrefab.p_parentTC.transform;
			this.m_trail.transform.localPosition = position;
			this.m_trailBase = this.m_trail.gameObject.GetComponent<PsTrailBase>();
			this.m_trailBase.Init();
			this.m_trailBase.SetPreviewMode(this.m_vehiclePrefab.p_parentTC.transform, Vector3.up * 10f, PsMainMenuState.m_modelCanvas.m_3DCamera.gameObject.layer);
		}
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x0009C5FC File Offset: 0x0009A9FC
	private void UpdateCharacter()
	{
		this.m_blinkTimer--;
		if (this.m_blinkTimer <= 0 && this.m_characterPrefab.p_gameObject.activeSelf)
		{
			this.m_animator.SetTrigger("Blink");
			this.m_blinkTimer = Random.Range(30, 300);
		}
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x0009C65C File Offset: 0x0009AA5C
	public void SetPropToLocator(string _hatIdentifier, string _locatorPathName)
	{
		GameObject hatPrefabByIdentifier = PsCustomisationManager.GetHatPrefabByIdentifier(_hatIdentifier);
		if (this.m_hatPrefab != null)
		{
			PrefabS.RemoveComponent(this.m_hatPrefab, true);
			this.m_hatPrefab = null;
		}
		this.m_hatPrefab = PsMainMenuState.m_modelCanvas.AddGameObject(hatPrefabByIdentifier, Vector3.zero, default(Vector3));
		Transform transform = this.m_characterPrefab.p_gameObject.transform.Find(_locatorPathName);
		this.m_hatPrefab.p_gameObject.transform.parent = transform;
		this.m_hatPrefab.p_gameObject.transform.localPosition = Vector3.zero;
		this.m_hatPrefab.p_gameObject.transform.localRotation = Quaternion.Euler(Vector3.up * -90f);
		this.m_hatPrefab.p_gameObject.transform.localScale = Vector3.one;
		this.m_alienEffects.WobbleHead();
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0009C744 File Offset: 0x0009AB44
	public void DisableColliderRenderers(Transform _tc)
	{
		if (_tc.GetComponent<Renderer>() != null && _tc.name.Contains("Collider"))
		{
			_tc.GetComponent<Renderer>().enabled = false;
		}
		for (int i = 0; i < _tc.childCount; i++)
		{
			this.DisableColliderRenderers(_tc.GetChild(i));
		}
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x0009C7A8 File Offset: 0x0009ABA8
	public Texture GetTierTexture(string _vehicleName, int _tier)
	{
		int num = Mathf.Min(_tier - 1, 4);
		string text = string.Concat(new object[] { _vehicleName, "DifT", num, "_Texture2D" });
		return ResourceManager.GetTexture(text);
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0009C7F0 File Offset: 0x0009ABF0
	private void UpdateUpgradeValues()
	{
		this.m_upgrades = new List<KeyValuePair<string, int>>();
		if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar))
		{
			this.m_upgrades.Add(new KeyValuePair<string, int>("power", 3));
			this.m_upgrades.Add(new KeyValuePair<string, int>("grip", 3));
			this.m_upgrades.Add(new KeyValuePair<string, int>("handling", 3));
		}
		else if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
		{
			this.m_upgrades.Add(new KeyValuePair<string, int>("power", 2));
			this.m_upgrades.Add(new KeyValuePair<string, int>("grip", 3));
			this.m_upgrades.Add(new KeyValuePair<string, int>("handling", 2));
		}
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0009C8BC File Offset: 0x0009ACBC
	private Transform FindChild(Transform t, string name)
	{
		IEnumerator enumerator = t.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				Transform transform2;
				if (transform.name == name)
				{
					transform2 = transform;
				}
				else
				{
					transform2 = this.FindChild(transform, name);
				}
				if (transform2 != null)
				{
					return transform2;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x0009C950 File Offset: 0x0009AD50
	public void CheckVisualUpgrade(string _type, int _tier)
	{
		_type = char.ToUpper(_type.get_Chars(0)) + _type.Substring(1);
		for (int i = 0; i < 4; i++)
		{
			Transform transform = this.FindChild(this.m_vehiclePrefab.p_gameObject.transform, _type + "T" + i);
			if (transform != null)
			{
				if (i == _tier)
				{
					transform.gameObject.SetActive(true);
				}
				else
				{
					transform.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0009C9EA File Offset: 0x0009ADEA
	public static void ExitToMainMenuState()
	{
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x0009CA08 File Offset: 0x0009AE08
	private static void ExitPopup()
	{
		PsMainMenuState.m_popup.Destroy();
		PsMainMenuState.m_popup = null;
		CameraS.RemoveBlur();
		PsMetagameManager.ShowResources(null, false, true, true, false, 0.025f, true, false, false);
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x0009CA3C File Offset: 0x0009AE3C
	public void ContinueCumulate()
	{
		PsMainMenuState.m_trophyCumulateAmount = PsMainMenuState.m_remainingAmount;
		PsMainMenuState.m_trophyCumulateDur = 45;
		PsMainMenuState.m_trophyCumulateCur = 0;
		PsMainMenuState.m_remainingAmount = 0;
		PsMainMenuState.m_rankChange = false;
		if (PsMainMenuState.m_racingGacha.m_level != null)
		{
			PsMainMenuState.m_racingGacha.m_level.SetText(PsStrings.Get(StringID.LEVEL).ToUpper() + " " + (PsMetagameManager.m_playerStats.gachaLevel + 1));
		}
		if (PsMainMenuState.m_adventureGacha.m_level != null)
		{
			PsMainMenuState.m_adventureGacha.m_level.SetText(PsStrings.Get(StringID.LEVEL).ToUpper() + " " + (PsMetagameManager.m_playerStats.gachaLevel + 1));
		}
		int num = PsMainMenuState.m_currentTrophyAmount - PsMetagameData.GetCurrentLeague().m_trophyLimit;
		if (PsMetagameData.GetNextLeague() != null)
		{
			int num2 = PsMetagameData.GetNextLeague().m_trophyLimit - PsMetagameData.GetCurrentLeague().m_trophyLimit;
			PsMainMenuState.m_progressAmount = (float)num / (float)num2;
			PsMainMenuState.m_startProg = PsMainMenuState.m_progressAmount;
			float num3 = (float)(PsMetagameManager.m_playerStats.trophies - PsMetagameData.GetCurrentLeague().m_trophyLimit) / (float)num2;
			PsMainMenuState.m_progDiff = num3 - PsMainMenuState.m_progressAmount;
		}
		else
		{
			PsMainMenuState.m_progressAmount = 1f;
			PsMainMenuState.m_startProg = 1f;
			PsMainMenuState.m_progDiff = 0f;
		}
		if (PsMainMenuState.m_progress.m_TC.m_active)
		{
			PsMainMenuState.m_progress.d_Draw(PsMainMenuState.m_progress);
		}
		PsMainMenuState.m_cumulateTrophyText = true;
		string name = PsMetagameData.m_leagueData[PsMetagameData.GetCurrentLeagueIndex()].m_name;
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x0009CBCC File Offset: 0x0009AFCC
	public static void ChangeToCreateState()
	{
		PsPlanetManager.GetCurrentPlanet().FastForward();
		PsMainMenuState.m_state = new PsUIBaseState(typeof(PsUITabbedCreate), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
		PsMainMenuState.m_state.SetAction("Exit", new Action(PsMainMenuState.ExitToMainMenuState));
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(PsMainMenuState.m_state);
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		TouchAreaS.CancelAllTouches(null);
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0009CC60 File Offset: 0x0009B060
	public static void ChangeToLeagueState()
	{
		PsPlanetManager.GetCurrentPlanet().FastForward();
		PsUITabbedCreate.m_selectedTab = 1;
		PsMainMenuState.m_state = new PsUIBaseState(typeof(PsUICenterLeagues), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
		PsMainMenuState.m_state.SetAction("Exit", new Action(PsMainMenuState.ExitToMainMenuState));
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(PsMainMenuState.m_state);
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		TouchAreaS.CancelAllTouches(null);
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0009CCFC File Offset: 0x0009B0FC
	public static void ChangeToLeaderboardState()
	{
		PsPlanetManager.GetCurrentPlanet().FastForward();
		PsMainMenuState.m_state = new PsUIBaseState(typeof(PsUICenterLeaderboard), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
		PsMainMenuState.m_state.SetAction("Exit", new Action(PsMainMenuState.ExitToMainMenuState));
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(PsMainMenuState.m_state);
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		TouchAreaS.CancelAllTouches(null);
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0009CD90 File Offset: 0x0009B190
	public static void ChangeToGarageState(Action _exitAction = null)
	{
		PsUICenterGarage.m_createGarageAction = delegate
		{
			PsMainMenuState.ChangeToGarageState(_exitAction);
		};
		PsPlanetManager.GetCurrentPlanet().FastForward();
		PsMainMenuState.m_state = new PsUIBaseState(typeof(PsUICenterGarage), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
		if (_exitAction == null)
		{
			PsMainMenuState.m_state.SetAction("Exit", new Action(PsMainMenuState.ExitToMainMenuState));
		}
		else
		{
			PsMainMenuState.m_state.SetAction("Exit", _exitAction);
		}
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(PsMainMenuState.m_state);
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		TouchAreaS.CancelAllTouches(null);
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x0009CE68 File Offset: 0x0009B268
	public static void ChangeToShopState(Action _exitAction = null)
	{
		PsPlanetManager.GetCurrentPlanet().FastForward();
		PsMainMenuState.m_state = new PsUIBaseState(typeof(PsUICenterShopAll), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
		if (_exitAction == null)
		{
			PsMainMenuState.m_state.SetAction("Exit", new Action(PsMainMenuState.ExitToMainMenuState));
		}
		else
		{
			PsMainMenuState.m_state.SetAction("Exit", _exitAction);
		}
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(PsMainMenuState.m_state);
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		TouchAreaS.CancelAllTouches(null);
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x0009CF18 File Offset: 0x0009B318
	public static void ChangeToSettingsState()
	{
		PsPlanetManager.GetCurrentPlanet().FastForward();
		PsMainMenuState.m_state = new PsUIBaseState(typeof(PsUICenterProfilePopup), null, null, null, false, InitialPage.Center);
		PsMainMenuState.m_state.SetAction("Exit", new Action(PsMainMenuState.ExitToMainMenuState));
		PlayerData user = default(PlayerData);
		user.playerId = PlayerPrefsX.GetUserId();
		user.name = PlayerPrefsX.GetUserName();
		user.tag = PlayerPrefsX.GetUserTag();
		user.facebookId = PlayerPrefsX.GetFacebookId();
		user.gameCenterId = PlayerPrefsX.GetGameCenterId();
		user.teamId = PlayerPrefsX.GetTeamId();
		user.teamName = PlayerPrefsX.GetTeamName();
		user.teamRole = PlayerPrefsX.GetTeamRole();
		user.teamRoleName = ClientTools.GetRoleName(user.teamRole);
		user.bigBangPoints = PsMetagameManager.m_playerStats.bigBangPoints;
		user.mcTrophies = PsMetagameManager.m_playerStats.mcTrophies;
		user.carTrophies = PsMetagameManager.m_playerStats.carTrophies;
		user.publishedMinigameCount = PsMetagameManager.m_playerStats.levelsMade;
		user.followerCount = PsMetagameManager.m_playerStats.followerCount;
		user.totalLikes = PsMetagameManager.m_playerStats.likesEarned;
		user.totalSuperLikes = PsMetagameManager.m_playerStats.megaLikesEarned;
		user.adventureLevelsCompleted = PsMetagameManager.m_playerStats.adventureLevels;
		user.racesWon = PsMetagameManager.m_playerStats.racesCompleted;
		user.newLevelsRated = PsMetagameManager.m_playerStats.newLevelsRated;
		user.countryCode = PlayerPrefsX.GetCountryCode();
		user.lastSeasonEndMcTrophies = PsMetagameManager.m_playerStats.m_lastSeasonMcTrophies;
		user.lastSeasonEndCarTrophies = PsMetagameManager.m_playerStats.m_lastSeasonCarTrophies;
		user.racesThisSeason = PsMetagameManager.m_playerStats.racesThisSeason;
		user.teamData = PsMetagameManager.m_team;
		user.youtubeName = PlayerPrefsX.GetYoutubeName();
		user.youtubeId = PlayerPrefsX.GetYoutubeId();
		user.youtubeSubscriberCount = PsMetagameManager.m_playerStats.youtubeSubscriberCount;
		PsMainMenuState.m_state.SetCreateDelegate(delegate
		{
			(PsMainMenuState.m_state.m_baseCanvas.m_mainContent as PsUICenterProfilePopup).SetUser(user, false);
			PsMainMenuState.m_state.m_baseCanvas.Update();
		});
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(PsMainMenuState.m_state);
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		TouchAreaS.CancelAllTouches(null);
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x0009D1DC File Offset: 0x0009B5DC
	public static void ChangeToEventsState()
	{
		PsPlanetManager.GetCurrentPlanet().FastForward();
		PsMainMenuState.m_state = new PsUIBaseState(typeof(PsUITabbedEvents), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
		PsMainMenuState.m_state.SetAction("Exit", new Action(PsMainMenuState.ExitToMainMenuState));
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(PsMainMenuState.m_state);
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		TouchAreaS.CancelAllTouches(null);
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x0009D270 File Offset: 0x0009B670
	public static void ChangeToTeamState()
	{
		PlayerPrefsX.SetTeamUnlocked(true);
		FrbMetrics.TrackTeamsUnlocked();
		PsPlanetManager.GetCurrentPlanet().FastForward();
		if (PlayerPrefsX.GetTeamId() == null)
		{
			PsMainMenuState.m_state = new PsUIBaseState(typeof(PsUITabbedTeam), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
		}
		else
		{
			PsMainMenuState.m_state = new PsUIBaseState(typeof(PsUITabbedJoinedTeam), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
		}
		PsMainMenuState.m_state.SetAction("Exit", new Action(PsMainMenuState.ExitToMainMenuState));
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(PsMainMenuState.m_state);
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		TouchAreaS.CancelAllTouches(null);
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x0009D340 File Offset: 0x0009B740
	public override void Execute()
	{
		if (PsMainMenuState.m_uiCanvas.m_TC.p_entity.m_active && !this.m_lastActive && PsMainMenuState.m_lockAll)
		{
			PsMainMenuState.m_garageTouchArea.DisableTouchAreas(true);
			PsMainMenuState.m_vehicleButtons[PsState.GetVehicleIndex()].DisableTouchAreas(true);
		}
		this.UpdateCharacter();
		PsPlanetManager.Update();
		if (PsUrlLaunch.ToSearchState())
		{
			PsMainMenuState.ChangeToCreateState();
			return;
		}
		if (PsMainMenuState.m_cumulateTrophyText && PsMainMenuState.m_progress.m_TC.m_active)
		{
			int num = PsMainMenuState.m_trophyCumulateDur - PsMainMenuState.m_trophyCumulateCur;
			int num2 = Mathf.FloorToInt((float)PsMainMenuState.m_trophyCumulateAmount / (float)num);
			int num3 = Convert.ToInt32(PsMainMenuState.m_trophyCount.m_text);
			num3 += num2;
			PsMainMenuState.m_currentTrophyAmount = num3;
			PsMainMenuState.m_trophyCount.SetText(num3.ToString());
			PsMainMenuState.m_trophyCumulateAmount -= num2;
			PsMainMenuState.m_trophyCumulateCur++;
			if (PsMainMenuState.m_trophyCumulateCur >= PsMainMenuState.m_trophyCumulateDur)
			{
				PsMainMenuState.m_trophyCumulateCur = PsMainMenuState.m_trophyCumulateDur;
			}
			PsMainMenuState.m_progressAmount = TweenS.tween(TweenStyle.CubicInOut, (float)PsMainMenuState.m_trophyCumulateCur / 60f, (float)PsMainMenuState.m_trophyCumulateDur / 60f, PsMainMenuState.m_startProg, PsMainMenuState.m_progDiff);
			PsMainMenuState.m_progress.d_Draw(PsMainMenuState.m_progress);
			if (PsMainMenuState.m_trophyCumulateCur >= PsMainMenuState.m_trophyCumulateDur)
			{
				TouchAreaS.Enable();
				PsMainMenuState.m_cumulateTrophyText = false;
				if (PsMainMenuState.m_rankChange && PsMainMenuState.m_remainingAmount >= 0 && PsMainMenuState.m_popup == null)
				{
					TouchAreaS.Enable();
					PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterLeaguesRankUp), typeof(PsUITopBackButton), null, null, true, true, InitialPage.Center, false, false, false);
					PsMainMenuState.m_popup.SetAction("Exit", delegate
					{
						PsMainMenuState.m_popup.Destroy();
						PsMainMenuState.m_popup = null;
						CameraS.RemoveBlur();
						if (!string.IsNullOrEmpty(PsMetagameManager.m_playerStats.m_teamKickReason) && !PsMainMenuState.m_rankChangeComing)
						{
							PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterKickedFromTeam), null, null, null, false, true, InitialPage.Center, false, false, false);
							PsMainMenuState.m_popup.SetAction("Continue", delegate
							{
								PsMainMenuState.m_popup.Destroy();
								PsMainMenuState.m_popup = null;
							});
							TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
						}
						else if ((PsMetagameManager.m_playerStats.carRank >= 2 || PsMetagameManager.m_playerStats.mcRank >= 2) && !PlayerPrefsX.GetTeamUnlocked() && !PlayerPrefsX.GetTeamJoined())
						{
							PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterTeamUnlocked), null, null, null, true, true, InitialPage.Center, false, false, false);
							PsMainMenuState.m_popup.SetAction("Exit", delegate
							{
								PsMainMenuState.ChangeToTeamState();
							});
							TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
						}
						else if (PlayerPrefsX.GetSeasonEnded() && PlayerPrefsX.GetTeamUnlocked())
						{
							PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterSeasonEnded), null, null, null, false, true, InitialPage.Center, false, false, false);
							PsMainMenuState.m_popup.SetAction("Continue", delegate
							{
								PsMainMenuState.m_rankChangeComing = false;
								PsMainMenuState.m_popup.Destroy();
								PsMainMenuState.m_popup = null;
								PlayerPrefsX.SetSeasonEnded(false);
								PsMetagameManager.ClaimSeasonRewards();
							});
							TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
						}
						else
						{
							PsMainMenuState.m_rankChangeComing = false;
							this.ContinueCumulate();
						}
					});
					TweenS.AddTransformTween(PsMainMenuState.m_popup.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.zero, Vector3.one, 0.3f, 0f, true);
					CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
				}
				else if (PsMainMenuState.m_rankChange && PsMainMenuState.m_remainingAmount < 0)
				{
					this.ContinueCumulate();
				}
			}
		}
		if (!string.IsNullOrEmpty(PsMetagameManager.m_playerStats.m_teamKickReason) && !PsMainMenuState.m_rankChangeComing && PsMainMenuState.m_popup == null && PsState.m_dialogues.Count == 0)
		{
			PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterKickedFromTeam), null, null, null, false, true, InitialPage.Center, false, false, false);
			PsMainMenuState.m_popup.SetAction("Continue", delegate
			{
				PsMainMenuState.m_popup.Destroy();
				PsMainMenuState.m_popup = null;
			});
			TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		else if (PlayerPrefsX.GetSeasonEnded() && PlayerPrefsX.GetTeamUnlocked() && !PsMainMenuState.m_rankChangeComing && PsMainMenuState.m_popup == null && PsState.m_dialogues.Count == 0)
		{
			PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterSeasonEnded), null, null, null, false, true, InitialPage.Center, false, false, false);
			PsMainMenuState.m_popup.SetAction("Continue", delegate
			{
				PsMainMenuState.m_popup.Destroy();
				PsMainMenuState.m_popup = null;
				PlayerPrefsX.SetSeasonEnded(false);
				PsMetagameManager.ClaimSeasonRewards();
			});
			TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		else if (!PsMainMenuState.m_rankChangeComing && PsMainMenuState.m_popup == null && !PlayerPrefsX.GetTeamUnlocked() && !PlayerPrefsX.GetTeamJoined() && (PsMetagameManager.m_playerStats.carRank >= 2 || PsMetagameManager.m_playerStats.mcRank >= 2) && PsState.m_dialogues.Count == 0)
		{
			PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterTeamUnlocked), null, null, null, true, true, InitialPage.Center, false, false, false);
			PsMainMenuState.m_popup.SetAction("Exit", delegate
			{
				PsMainMenuState.ChangeToTeamState();
			});
			TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		if (this.m_charTweenTC != null)
		{
			this.m_vehiclePrefab.p_gameObject.transform.localScale = this.m_charTweenTC.transform.localScale;
		}
		if (PsMainMenuState.m_garageTouchArea != null && PsMainMenuState.m_garageTouchArea.m_began)
		{
			if (this.m_charTweenTC == null)
			{
				this.m_charTweenTC = TransformS.AddComponent(PsMainMenuState.m_uiCanvas.m_TC.p_entity);
			}
			TweenS.RemoveAllTweensFromEntity(this.m_charTweenTC.p_entity);
			TweenC tweenC = TweenS.AddTransformTween(this.m_charTweenTC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.035f, 1.035f, 1.035f), 0.225f, 0f, true);
		}
		else if (PsMainMenuState.m_garageTouchArea != null && PsMainMenuState.m_garageTouchArea.m_end)
		{
			if (this.m_charTweenTC == null)
			{
				this.m_charTweenTC = TransformS.AddComponent(PsMainMenuState.m_uiCanvas.m_TC.p_entity);
			}
			TweenS.RemoveAllTweensFromEntity(this.m_charTweenTC.p_entity);
			TweenC tweenC2 = TweenS.AddTransformTween(this.m_charTweenTC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.one, 0.2f, 0f, true);
		}
		PsUIGenericButton separateShopButton = PsMetagameManager.m_menuResourceView.m_separateShopButton;
		if (separateShopButton != null && separateShopButton.m_hit && PsState.m_activeGameLoop == null)
		{
			PsMetrics.ButtonPressed("shop", "mainmenu", PsState.GetCurrentVehicleType(false).ToString());
			PsMainMenuState.ChangeToShopState(null);
		}
		else if (((PsMainMenuState.m_settingsButton != null && PsMainMenuState.m_settingsButton.m_hit) || (PsMainMenuState.m_profileImage != null && PsMainMenuState.m_profileImage.m_hit)) && PsState.m_activeGameLoop == null)
		{
			if (PsMainMenuState.m_profileImage != null && PsMainMenuState.m_profileImage.m_hit)
			{
				SoundS.PlaySingleShot("/UI/ButtonTransition", Vector3.zero, 1f);
			}
			PsMetrics.ButtonPressed("settings", "mainmenu", PsState.GetCurrentVehicleType(false).ToString());
			PsMainMenuState.ChangeToSettingsState();
		}
		else if (PsMainMenuState.m_createButton != null && PsMainMenuState.m_createButton.m_hit && PsState.m_activeGameLoop == null)
		{
			PsMetrics.ButtonPressed("create", "mainmenu", PsState.GetCurrentVehicleType(false).ToString());
			PsUITabbedCreate.m_selectedTab = 1;
			PsMainMenuState.ChangeToCreateState();
		}
		else if (PsMainMenuState.m_teamButton != null && PsMainMenuState.m_teamButton.m_hit && PsState.m_activeGameLoop == null)
		{
			PsMetrics.ButtonPressed("team", "mainmenu", PsState.GetCurrentVehicleType(false).ToString());
			if (PsMetagameManager.m_playerStats.carRank >= 2 || PsMetagameManager.m_playerStats.mcRank >= 2 || PlayerPrefsX.GetTeamUnlocked() || PlayerPrefsX.GetTeamJoined())
			{
				PsMainMenuState.ChangeToTeamState();
			}
			else if (PsMainMenuState.m_popup == null)
			{
				PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterTeamLocked), null, null, null, true, true, InitialPage.Center, false, false, false);
				PsMainMenuState.m_popup.SetAction("Exit", delegate
				{
					PsMainMenuState.m_popup.Destroy();
					PsMainMenuState.m_popup = null;
					CameraS.RemoveBlur();
				});
				TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
			}
		}
		else if (PsMainMenuState.m_eventsButton != null && PsMainMenuState.m_eventsButton.m_hit && PsState.m_activeGameLoop == null)
		{
			PsMetrics.ButtonPressed("events", "mainmenu", PsState.GetCurrentVehicleType(false).ToString());
			PsMainMenuState.ChangeToEventsState();
		}
		else if (PsMainMenuState.m_garageTouchArea != null && PsMainMenuState.m_garageTouchArea.m_hit && PsState.m_activeGameLoop == null)
		{
			SoundS.PlaySingleShot("/UI/ButtonTransition", Vector3.zero, 1f);
			PsMetrics.ButtonPressed("garage", "mainmenu", PsState.GetCurrentVehicleType(false).ToString());
			PsMainMenuState.ChangeToGarageState(null);
		}
		else if (PsMainMenuState.m_leagueButton != null && PsMainMenuState.m_leagueButton.m_hit && PsState.m_activeGameLoop == null)
		{
			PsMetrics.ButtonPressed("league", "mainmenu", PsState.GetCurrentVehicleType(false).ToString());
			PsMainMenuState.ChangeToLeagueState();
		}
		else if (PsMainMenuState.m_raceButton != null && PsMainMenuState.m_raceButton.m_hit && PsState.m_activeGameLoop == null && PsMainMenuState.m_popup == null)
		{
			PsMetrics.ButtonPressed("race", "mainmenu", PsState.GetCurrentVehicleType(false).ToString());
			if (PlayerPrefsX.GetNameChanged())
			{
				TouchAreaS.CancelAllTouches(null);
				if (!PsGachaManager.IsSlotEmpty(PsGachaManager.SlotType.RACING) || PsMetagameManager.m_vehicleGachaData.m_rivalWonCount >= 4)
				{
					CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
					PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUIRacingChestSlotPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
					PsMainMenuState.m_popup.SetAction("Play", delegate
					{
						PsMainMenuState.m_popup.Destroy();
						PsMainMenuState.m_popup = null;
						PlanetTools.PlayNextLevelOfPlanet(PlanetTools.GetVehicleRacingPlanetIdentifier(), true);
					});
					PsMainMenuState.m_popup.SetAction("Exit", delegate
					{
						PsMainMenuState.m_popup.Destroy();
						PsMainMenuState.m_popup = null;
						CameraS.RemoveBlur();
					});
					EntityManager.SetActivityOfEntitiesWithTag("RacingGacha", true, true, true, true, false, false);
					TouchAreaS.SetCamera(PsMainMenuState.m_racingGacha.m_TAC, PsMainMenuState.m_popup.m_scrollableCanvas.m_camera);
					TweenS.AddTransformTween(PsMainMenuState.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				}
				else
				{
					PlanetTools.PlayNextLevelOfPlanet(PlanetTools.GetVehicleRacingPlanetIdentifier(), true);
				}
			}
			else
			{
				PsUserNameInputState psUserNameInputState = new PsUserNameInputState();
				psUserNameInputState.m_lastState = Main.m_currentGame.m_currentScene.GetCurrentState();
				psUserNameInputState.m_continueAction = delegate
				{
					PlanetTools.PlayNextLevelOfPlanet(PlanetTools.GetVehicleRacingPlanetIdentifier(), true);
				};
				PsMenuScene.m_lastIState = psUserNameInputState;
				PsMenuScene.m_lastState = null;
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUserNameInputState);
			}
		}
		else if (PsMainMenuState.m_dirtBikebundlebutton != null && PsMainMenuState.m_dirtBikebundlebutton.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsDirtbikeBundlePopup), null, null, null, true, true, InitialPage.Center, false, false, false);
			IAPProduct iapproductById = PsIAPManager.GetIAPProductById("dirt_bike_bundle");
			string text = ((iapproductById != null) ? iapproductById.price : "NaN");
			(popup.m_mainContent as PsDirtbikeBundlePopup).SetPrice(text);
			(popup.m_mainContent as PsDirtbikeBundlePopup).CreateContent();
			popup.SetAction("Purchased", delegate
			{
				popup.Destroy();
				PsPurchaseHelper.PurchaseIAP("dirt_bike_bundle", null, delegate
				{
					PsMainMenuState.UpdateUIInfo();
					PsMainMenuState.RemoveDisabledCanvas();
				});
			});
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
			});
			TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		for (int i = 0; i < PsMainMenuState.m_vehicleButtons.Count; i++)
		{
			if (PsMainMenuState.m_vehicleButtons[i].m_hit && i != PsState.GetVehicleIndex() && PsState.m_activeGameLoop == null)
			{
				PsMetrics.ButtonPressed("vehiclechange" + PsState.m_vehicleTypes[i], "mainmenu", PsState.GetCurrentVehicleType(false).ToString());
				PsState.SetVehicleIndex(i);
				this.ChangeVehicle(PsState.GetVehicleIndex());
				break;
			}
		}
		if (PsPlanet.m_released)
		{
			PsPlanet.m_rollInertia *= 0.85f;
		}
		PsPlanet currentPlanet = PsPlanetManager.GetCurrentPlanet();
		float num4 = PsPlanet.m_rollInertia;
		if (PsPlanet.m_released)
		{
			if (-currentPlanet.m_currentAngle < currentPlanet.m_minAngle)
			{
				num4 += (currentPlanet.m_minAngle - -currentPlanet.m_currentAngle) * -0.0927f;
			}
			else if (-currentPlanet.m_currentAngle > currentPlanet.m_maxAngle)
			{
				num4 += (currentPlanet.m_maxAngle - -currentPlanet.m_currentAngle) * -0.0927f;
			}
		}
		else if (-currentPlanet.m_currentAngle < currentPlanet.m_minAngle)
		{
			float num5 = Mathf.Sin((currentPlanet.m_minAngle - -currentPlanet.m_currentAngle) * 0.017453292f) * (float)Screen.height;
			float num6 = Mathf.Max(0f, ((float)Screen.height - num5) / (float)Screen.height * 0.618f);
			num4 *= num6;
		}
		else if (-currentPlanet.m_currentAngle > currentPlanet.m_maxAngle)
		{
			float num7 = Mathf.Sin(-(currentPlanet.m_maxAngle - -currentPlanet.m_currentAngle) * 0.017453292f) * (float)Screen.height;
			float num8 = Mathf.Max(0f, ((float)Screen.height - num7) / (float)Screen.height * 0.618f);
			num4 *= num8;
		}
		if (Mathf.Abs(num4) > 0.01f)
		{
			currentPlanet.m_currentAngle += num4;
		}
		else
		{
			PsPlanet.m_rollInertia = 0f;
		}
		if (PsMainMenuState.m_popup == null && Main.m_currentGame.m_sceneManager.m_loadingScene == null && Main.AndroidBackButtonPressed(null))
		{
			Debug.LogWarning("QUIT PRESSED");
			Application.Quit();
		}
		this.m_lastActive = PsMainMenuState.m_uiCanvas.m_TC.p_entity.m_active;
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x0009E178 File Offset: 0x0009C578
	public override void Exit()
	{
		if (this.m_trailBase != null)
		{
			this.m_trailBase.Destroy();
			this.m_trailBase = null;
			Object.Destroy(this.m_trail);
			this.m_trail = null;
		}
		PsMetagameManager.HideResources();
		PsMainMenuState.m_rankChangeComing = false;
		if (PsMainMenuState.m_popup != null)
		{
			PsMainMenuState.m_popup.Destroy();
		}
		PsMainMenuState.m_popup = null;
		if (PsMainMenuState.m_uiCanvas != null)
		{
			PsMainMenuState.m_uiCanvas.Destroy();
		}
		PsMainMenuState.m_uiCanvas = null;
		if (this.m_shadow != null)
		{
			Object.Destroy(this.m_shadow.GetComponent<Renderer>().material);
			Object.Destroy(this.m_shadow.gameObject);
		}
		this.m_shadow = null;
		PsMainMenuState.m_currentTrophyAmount = 0;
		this.NullStaticUIElements();
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x0009E244 File Offset: 0x0009C644
	public void NullStaticUIElements()
	{
		PsMainMenuState.m_disabledCanvas = null;
		PsMainMenuState.m_popup = null;
		PsMainMenuState.m_uiCanvas = null;
		PsMainMenuState.m_profileImage = null;
		PsMainMenuState.m_createButton = null;
		PsMainMenuState.m_raceButton = null;
		PsMainMenuState.m_settingsButton = null;
		PsMainMenuState.m_vehicleInfoArea = null;
		PsMainMenuState.m_racingArea = null;
		PsMainMenuState.m_topLeftArea = null;
		PsMainMenuState.m_modelHolder = null;
		PsMainMenuState.m_notificationBase = null;
		PsMainMenuState.m_trophyIcon = null;
		PsMainMenuState.m_lockedRace = null;
		PsMainMenuState.m_adventureGachaArea = null;
		PsMainMenuState.m_raceGachaArea = null;
		PsMainMenuState.m_planetList = null;
		PsMainMenuState.m_progress = null;
		PsMainMenuState.m_trophyCount = null;
		PsMainMenuState.m_adventureGacha = null;
		PsMainMenuState.m_racingGacha = null;
		PsMainMenuState.m_vehicleIcon = null;
		PsMainMenuState.m_leagueName = null;
		PsMainMenuState.m_playerLeagueBanner = null;
		PsMainMenuState.m_vehicleButtons = null;
		PsMainMenuState.m_garageTouchArea = null;
		PsMainMenuState.m_leagueArea = null;
		PsMainMenuState.m_leagueButton = null;
		PsMainMenuState.m_vehicleButtonArea = null;
		PsMainMenuState.m_teamButton = null;
		PsMainMenuState.m_gachaList = null;
		this.m_tournamentTimerContainer = null;
		this.m_tournamentTimer = null;
		this.m_tournamentShine = null;
		this.m_tournamentShineRotationTween = null;
		this.m_tournamentShineScaleTween = null;
	}

	// Token: 0x0400131F RID: 4895
	public static UICanvas m_disabledCanvas;

	// Token: 0x04001320 RID: 4896
	public static PsUIBasePopup m_popup;

	// Token: 0x04001321 RID: 4897
	public static PsUIBaseState m_state;

	// Token: 0x04001322 RID: 4898
	private static UICanvas m_uiCanvas;

	// Token: 0x04001323 RID: 4899
	public static PsUIProfileImage m_profileImage;

	// Token: 0x04001324 RID: 4900
	public static PsUIGenericButton m_createButton;

	// Token: 0x04001325 RID: 4901
	public static PsUIGenericButton m_teamButton;

	// Token: 0x04001326 RID: 4902
	public static PsUIGenericButton m_eventsButton;

	// Token: 0x04001327 RID: 4903
	private static UI3DRenderTextureCanvas m_modelCanvas;

	// Token: 0x04001328 RID: 4904
	private PrefabC m_vehiclePrefab;

	// Token: 0x04001329 RID: 4905
	private List<KeyValuePair<string, int>> m_upgrades;

	// Token: 0x0400132A RID: 4906
	private static List<PsUIGenericButton> m_vehicleButtons;

	// Token: 0x0400132B RID: 4907
	public static PsUIGenericButton m_raceButton;

	// Token: 0x0400132C RID: 4908
	private static PsUIGenericButton m_settingsButton;

	// Token: 0x0400132D RID: 4909
	private static UICanvas m_vehicleInfoArea;

	// Token: 0x0400132E RID: 4910
	private static UIHorizontalList m_racingArea;

	// Token: 0x0400132F RID: 4911
	private static UIHorizontalList m_topLeftArea;

	// Token: 0x04001330 RID: 4912
	private static UICanvas m_modelHolder;

	// Token: 0x04001331 RID: 4913
	private static UICanvas m_notificationBase;

	// Token: 0x04001332 RID: 4914
	private static UIFittedSprite m_trophyIcon;

	// Token: 0x04001333 RID: 4915
	private static UIVerticalList m_lockedRace;

	// Token: 0x04001334 RID: 4916
	private static UIFittedSprite m_vehicleIcon;

	// Token: 0x04001335 RID: 4917
	private static UIFittedText m_leagueName;

	// Token: 0x04001336 RID: 4918
	private static UIFittedSprite m_playerLeagueBanner;

	// Token: 0x04001337 RID: 4919
	private static UICanvas m_garageTouchArea;

	// Token: 0x04001338 RID: 4920
	private static UIVerticalList m_leagueArea;

	// Token: 0x04001339 RID: 4921
	private static PsUIGenericButton m_leagueButton;

	// Token: 0x0400133A RID: 4922
	private static UIVerticalList m_vehicleButtonArea;

	// Token: 0x0400133B RID: 4923
	public static UIVerticalList m_gachaList;

	// Token: 0x0400133C RID: 4924
	public static UICanvas m_adventureGachaArea;

	// Token: 0x0400133D RID: 4925
	public static UICanvas m_raceGachaArea;

	// Token: 0x0400133E RID: 4926
	public static UICanvas m_freeGachaArea;

	// Token: 0x0400133F RID: 4927
	private static UIHorizontalList m_planetList;

	// Token: 0x04001340 RID: 4928
	public static UICanvas m_progress;

	// Token: 0x04001341 RID: 4929
	public static UIText m_trophyCount;

	// Token: 0x04001342 RID: 4930
	public static int m_currentTrophyAmount;

	// Token: 0x04001343 RID: 4931
	private static int m_trophyCumulateAmount;

	// Token: 0x04001344 RID: 4932
	private static int m_remainingAmount;

	// Token: 0x04001345 RID: 4933
	private static int m_trophyCumulateDur;

	// Token: 0x04001346 RID: 4934
	private static int m_trophyCumulateCur;

	// Token: 0x04001347 RID: 4935
	private static float m_progressAmount;

	// Token: 0x04001348 RID: 4936
	private static float m_startProg;

	// Token: 0x04001349 RID: 4937
	private static float m_progDiff;

	// Token: 0x0400134A RID: 4938
	private static bool m_rankChange;

	// Token: 0x0400134B RID: 4939
	public static bool m_cumulateTrophyText;

	// Token: 0x0400134C RID: 4940
	private static bool m_trophiesSet;

	// Token: 0x0400134D RID: 4941
	private static bool m_rankChangeComing;

	// Token: 0x0400134E RID: 4942
	private static bool m_lockAll;

	// Token: 0x0400134F RID: 4943
	private static bool m_lockRacing;

	// Token: 0x04001350 RID: 4944
	private static bool m_startHidden;

	// Token: 0x04001351 RID: 4945
	public static bool m_tweenIn;

	// Token: 0x04001352 RID: 4946
	public static bool m_createExistingPopup;

	// Token: 0x04001353 RID: 4947
	private static bool m_mainMenuReached;

	// Token: 0x04001354 RID: 4948
	private static float m_gachaUiHeight = 0.14f;

	// Token: 0x04001355 RID: 4949
	private static float m_gachaWidthRatio = 1.257f;

	// Token: 0x04001356 RID: 4950
	public bool m_lastActive = true;

	// Token: 0x04001357 RID: 4951
	private UIFittedText m_tournamentTimer;

	// Token: 0x04001358 RID: 4952
	private UISprite m_tournamentShine;

	// Token: 0x04001359 RID: 4953
	private UICanvas m_tournamentTimerContainer;

	// Token: 0x0400135A RID: 4954
	private TweenC m_tournamentShineRotationTween;

	// Token: 0x0400135B RID: 4955
	private TweenC m_tournamentShineScaleTween;

	// Token: 0x0400135C RID: 4956
	private EventMessage m_activeTournamentEvent;

	// Token: 0x0400135D RID: 4957
	private int m_timeLeft;

	// Token: 0x0400135E RID: 4958
	private static bool m_hasStarted;

	// Token: 0x0400135F RID: 4959
	private static bool m_hasEnded;

	// Token: 0x04001360 RID: 4960
	public static PsUIAdventureGacha m_adventureGacha;

	// Token: 0x04001361 RID: 4961
	public static PsUIRacingGacha m_racingGacha;

	// Token: 0x04001362 RID: 4962
	public static PsUIFreeGacha m_freeGacha;

	// Token: 0x04001363 RID: 4963
	private static UIRectSpriteButton m_dirtBikebundlebutton;

	// Token: 0x04001364 RID: 4964
	private PrefabC m_characterPrefab;

	// Token: 0x04001365 RID: 4965
	private Animator m_animator;

	// Token: 0x04001366 RID: 4966
	private RuntimeAnimatorController m_animatorController;

	// Token: 0x04001367 RID: 4967
	private int m_bindPoseState;

	// Token: 0x04001368 RID: 4968
	private int m_standPoseState;

	// Token: 0x04001369 RID: 4969
	private int m_drivePoseState;

	// Token: 0x0400136A RID: 4970
	private AlienEffects m_alienEffects;

	// Token: 0x0400136B RID: 4971
	private PrefabC m_hatPrefab;

	// Token: 0x0400136C RID: 4972
	private Transform m_shadow;

	// Token: 0x0400136D RID: 4973
	private GameObject m_trail;

	// Token: 0x0400136E RID: 4974
	private PsTrailBase m_trailBase;

	// Token: 0x0400136F RID: 4975
	private int m_blinkTimer = 60;

	// Token: 0x04001370 RID: 4976
	private TransformC m_charTweenTC;

	// Token: 0x04001371 RID: 4977
	public static float m_currentScroll;

	// Token: 0x04001372 RID: 4978
	private int m_changePage;

	// Token: 0x04001373 RID: 4979
	private float m_prevSnapPos;

	// Token: 0x04001374 RID: 4980
	public static int m_snapPos;

	// Token: 0x04001375 RID: 4981
	public static int m_targetPage = 9999;

	// Token: 0x04001376 RID: 4982
	private bool m_forcePage;

	// Token: 0x04001377 RID: 4983
	public static int m_enterMenuAfterRoll = 9999;

	// Token: 0x04001378 RID: 4984
	public static int m_forcedSubMenu = -1;
}
