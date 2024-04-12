using System;
using UnityEngine;

// Token: 0x020003AF RID: 943
public class PsUICenterTournamentLoading : UICanvas
{
	// Token: 0x06001AFF RID: 6911 RVA: 0x0012D320 File Offset: 0x0012B720
	public PsUICenterTournamentLoading(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.m_scene = Main.m_currentGame.m_sceneManager.m_loadingScene as PsRacingLoadingScene;
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetMargins(0.1f, RelativeTo.ScreenHeight);
		this.SetDepthOffset(-5f);
		this.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.1f, 0.1f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(-0.07f, 0.07f, -0.07f, 0.07f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(0f, 1f);
		uicanvas.RemoveDrawHandler();
		if (PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) || PlayerPrefsX.GetOffroadRacing() || PlayerPrefsX.GetNameChanged() || PlanetTools.m_planetProgressionInfos[PlanetTools.GetVehiclePlanetIdentifier()].m_mainPath.m_currentNodeId > 1)
		{
			this.m_exitButton = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
			this.m_exitButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_exitButton.SetSound("/UI/ButtonBack");
			this.m_exitButton.SetOrangeColors(true);
			this.m_exitButton.SetAlign(0f, 1f);
			this.m_exitButton.SetDepthOffset(-10f);
		}
		this.m_mainContainer = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_mainContainer.SetHeight(0.4f, RelativeTo.ScreenHeight);
		this.m_mainContainer.SetWidth(1.2f, RelativeTo.ScreenHeight);
		this.m_mainContainer.RemoveDrawHandler();
		this.m_leftContainer = new UICanvas(this.m_mainContainer, false, string.Empty, null, string.Empty);
		this.m_leftContainer.SetWidth(0.5f, RelativeTo.ParentWidth);
		this.m_leftContainer.SetHorizontalAlign(0f);
		this.m_leftContainer.RemoveDrawHandler();
		this.m_creatorInfoParent = new UICanvas(this.m_leftContainer, false, string.Empty, null, string.Empty);
		this.m_creatorInfoParent.SetHeight(0.3f, RelativeTo.ParentHeight);
		this.m_creatorInfoParent.SetVerticalAlign(1f);
		this.m_creatorInfoParent.SetMargins(-0.25f, 0.25f, -0.33f, 0.33f, RelativeTo.OwnHeight);
		this.m_creatorInfoParent.RemoveDrawHandler();
		this.UpdateCreatorInfo();
		this.UpdateScreenshot();
		float num = 0.5f;
		float num2 = 0.7f;
		this.m_middleBanner = new UICanvas(this.m_mainContainer, false, string.Empty, null, string.Empty);
		this.m_middleBanner.SetHeight(num, RelativeTo.ParentHeight);
		this.m_middleBanner.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_middleBanner.SetVerticalAlign(num2);
		this.m_middleBanner.RemoveDrawHandler();
		float num3 = (float)Screen.height / 2200f;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.TournamentStreaksPrefab_GameObject);
		this.m_loadingEffectPrefab = PrefabS.AddComponent(this.m_middleBanner.m_TC, Vector3.zero, gameObject);
		PrefabS.SetCamera(this.m_loadingEffectPrefab, this.m_middleBanner.m_camera);
		Vector3 vector = this.m_loadingEffectPrefab.p_gameObject.transform.localScale;
		vector..ctor(vector.x, vector.y * num3, vector.z);
		this.m_loadingEffectPrefab.p_gameObject.transform.localScale = vector;
		this.m_loadingEffectPrefab.p_gameObject.transform.Translate(new Vector3(0f, -60f * num3, 400f));
		this.m_loadingEffectPrefab.p_gameObject.transform.Rotate(Vector3.up, 180f);
		this.m_loadingEffectParticleSystem = this.m_loadingEffectPrefab.p_gameObject.GetComponentInChildren<ParticleSystem>();
		if (this.m_loadingEffectParticleSystem != null)
		{
			vector = this.m_loadingEffectParticleSystem.gameObject.transform.localScale;
			vector..ctor(vector.x * num3, vector.y * num3, vector.z);
			this.m_loadingEffectParticleSystem.gameObject.transform.localScale = vector;
			this.m_loadingEffectParticleSystem.Pause();
			this.m_loadingEffectParticleSystem.Clear();
		}
		Renderer[] componentsInChildren = this.m_loadingEffectPrefab.p_gameObject.transform.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].material != null)
				{
					componentsInChildren[i].material.renderQueue = 2800;
				}
			}
		}
		this.m_middleBanner.m_TC.transform.localScale = Vector3.zero;
		this.m_rightContainer = new UICanvas(this.m_mainContainer, false, string.Empty, null, string.Empty);
		this.m_rightContainer.SetWidth(0.5f, RelativeTo.ParentWidth);
		this.m_rightContainer.SetHeight(num, RelativeTo.ParentHeight);
		this.m_rightContainer.SetAlign(1f, num2);
		this.m_rightContainer.RemoveDrawHandler();
		this.UpdateTournamentInfo();
		this.m_loadingText = new UIText(this, false, string.Empty, PsStrings.Get(StringID.LOADING), PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, "#ffffff", null);
		this.m_loadingText.SetAlign(1f, 0f);
		this.m_topBanner = new UICanvas(null, false, "BANNER", null, string.Empty);
		this.m_topBanner.SetCamera(this.m_camera, false, true);
		this.m_topBanner.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_topBanner.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_topBanner.SetAlign(0.5f, 1f);
		this.m_topBanner.RemoveDrawHandler();
		this.m_topBanner.SetDepthOffset(10f);
		this.m_topBanner.Update();
		this.m_bottomBanner = new UICanvas(null, false, "BANNER", null, string.Empty);
		this.m_bottomBanner.SetCamera(this.m_camera, false, true);
		this.m_bottomBanner.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_bottomBanner.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_bottomBanner.SetAlign(0.5f, 0f);
		this.m_bottomBanner.RemoveDrawHandler();
		this.m_bottomBanner.SetDepthOffset(10f);
		this.m_bottomBanner.Update();
		TweenS.AddTransformTween(this.m_topBanner.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_topBanner.m_TC.transform.localPosition + Vector3.up * (float)Screen.height * 0.12f, this.m_topBanner.m_TC.transform.localPosition, 0.2f, 0f, true);
		TweenS.AddTransformTween(this.m_bottomBanner.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_bottomBanner.m_TC.transform.localPosition + Vector3.down * (float)Screen.height * 0.12f, this.m_bottomBanner.m_TC.transform.localPosition, 0.2f, 0f, true);
	}

	// Token: 0x06001B00 RID: 6912 RVA: 0x0012DA94 File Offset: 0x0012BE94
	private void UpdateScreenshot()
	{
		if (PsState.m_activeGameLoop == null)
		{
			return;
		}
		if (PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			UICanvas uicanvas = new UICanvas(this.m_leftContainer, false, string.Empty, null, string.Empty);
			uicanvas.RemoveDrawHandler();
			this.m_screenshot = new PsUITournamentLoadingScreenshot(uicanvas, false, "Screenshot", Vector2.zero, PsState.m_activeGameLoop, false, true, 0.045f, false);
			this.m_screenshot.m_gameId = PsState.m_activeGameLoop.GetGameId();
			this.m_screenshot.LoadPicture();
			return;
		}
		if (!PsState.m_activeGameLoop.m_loadingMetaData)
		{
			PsState.m_activeGameLoop.LoadMinigameMetaData(new Action(this.UpdateScreenshot));
			return;
		}
		PsGameLoop activeGameLoop = PsState.m_activeGameLoop;
		activeGameLoop.m_loadMetadataCallback = (Action)Delegate.Combine(activeGameLoop.m_loadMetadataCallback, new Action(this.UpdateScreenshot));
	}

	// Token: 0x06001B01 RID: 6913 RVA: 0x0012DB6C File Offset: 0x0012BF6C
	private void UpdateCreatorInfo()
	{
		if (PsState.m_activeGameLoop == null)
		{
			return;
		}
		if (PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			this.m_creator = new PsUITournamentCreatorInfo(this.m_creatorInfoParent, false, false, false, false, false);
			this.m_creator.SetAlign(0f, 1f);
			this.m_creator.SetDepthOffset(-20f);
			return;
		}
		if (!PsState.m_activeGameLoop.m_loadingMetaData)
		{
			PsState.m_activeGameLoop.LoadMinigameMetaData(new Action(this.UpdateCreatorInfo));
			return;
		}
		PsGameLoop activeGameLoop = PsState.m_activeGameLoop;
		activeGameLoop.m_loadMetadataCallback = (Action)Delegate.Combine(activeGameLoop.m_loadMetadataCallback, new Action(this.UpdateCreatorInfo));
	}

	// Token: 0x06001B02 RID: 6914 RVA: 0x0012DC1C File Offset: 0x0012C01C
	private void UpdateTournamentInfo()
	{
		if (PsState.m_activeGameLoop == null)
		{
			return;
		}
		if (PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			this.m_infoList = new UIVerticalList(this.m_rightContainer, string.Empty);
			this.m_infoList.SetVerticalAlign(1f);
			this.m_infoList.SetSpacing(0.06f, RelativeTo.ParentHeight);
			this.m_infoList.RemoveDrawHandler();
			UICanvas uicanvas = new UICanvas(this.m_infoList, false, string.Empty, null, string.Empty);
			uicanvas.SetRogue();
			uicanvas.SetVerticalAlign(1f);
			uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
			uicanvas.SetWidth(0.8f, RelativeTo.ParentWidth);
			uicanvas.SetHorizontalAlign(0.25f);
			uicanvas.SetMargins(0f, 0f, -0.75f, 0.7f, RelativeTo.OwnHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_logo", null), true, true);
			uifittedSprite.SetWidth(1f, RelativeTo.ParentWidth);
			UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_infoList, string.Empty);
			uihorizontalList.SetHeight(0.22f, RelativeTo.ParentHeight);
			uihorizontalList.SetHorizontalAlign(0f);
			uihorizontalList.SetSpacing(0.4f, RelativeTo.OwnHeight);
			uihorizontalList.RemoveDrawHandler();
			int num = (int)(PsState.m_activeGameLoop as PsGameLoopTournament).GetCcCap();
			UIText uitext = new UIText(uihorizontalList, false, string.Empty, num + "cc", PsFontManager.GetFont(PsFonts.KGSecondChances), 1f, RelativeTo.ParentHeight, "#F7B52D", "#0F0F0F");
			uitext.SetHorizontalAlign(0f);
			UIFittedText uifittedText = new UIFittedText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.TOUR_TOURNAMENT), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#66CFFD", "#0F0F0F");
			uifittedText.SetHorizontalAlign(0f);
			UICanvas uicanvas2 = new UICanvas(this.m_infoList, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.35f, RelativeTo.ParentHeight);
			uicanvas2.RemoveDrawHandler();
			UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, PsState.m_activeGameLoop.GetName(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#F7B52D", "#0F0F0F");
			uifittedText2.SetHorizontalAlign(0f);
			return;
		}
		if (!PsState.m_activeGameLoop.m_loadingMetaData)
		{
			PsState.m_activeGameLoop.LoadMinigameMetaData(new Action(this.UpdateTournamentInfo));
			return;
		}
		PsGameLoop activeGameLoop = PsState.m_activeGameLoop;
		activeGameLoop.m_loadMetadataCallback = (Action)Delegate.Combine(activeGameLoop.m_loadMetadataCallback, new Action(this.UpdateTournamentInfo));
	}

	// Token: 0x06001B03 RID: 6915 RVA: 0x0012DE9C File Offset: 0x0012C29C
	public void BannerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, rect, (float)Screen.height * 0.015f, Color.black, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, Color.black, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001B04 RID: 6916 RVA: 0x0012DF4C File Offset: 0x0012C34C
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && this.m_exitButton.m_TAC.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		else if (!this.m_building && PsState.m_activeGameLoop.m_minigameBytes != null && this.m_loadingText != null)
		{
			this.m_building = true;
			this.m_loadingText.SetText(PsStrings.Get(StringID.BUILDING_LEVEL));
			this.SetBackButtonActive(false);
		}
		else if (this.m_scene.m_outroStarted && !this.m_gameStarted)
		{
			this.m_exitTimer--;
			if (this.m_exitTimer <= 0)
			{
				this.m_gameStarted = true;
				(this.GetRoot() as PsUIBasePopup).CallAction("StartGame");
				TweenS.AddTransformTween(this.m_leftContainer.m_TC, TweenedProperty.Scale, TweenStyle.Linear, this.m_leftContainer.m_TC.transform.localScale, Vector3.zero, 0.2f, 0f, true);
				TweenS.AddTransformTween(this.m_middleBanner.m_TC, TweenedProperty.Scale, TweenStyle.Linear, this.m_middleBanner.m_TC.transform.localScale, Vector3.zero, 0.15f, 0f, true);
				TweenS.AddTransformTween(this.m_infoList.m_TC, TweenedProperty.Scale, TweenStyle.Linear, this.m_infoList.m_TC.transform.localScale, Vector3.zero, 0.2f, 0f, true);
				if (this.m_loadingEffectParticleSystem != null)
				{
					this.m_loadingEffectParticleSystem.Pause();
					this.m_loadingEffectParticleSystem.Clear();
				}
			}
		}
		if (this.m_creator != null && !this.m_creatorAnimationDone)
		{
			this.m_creatorAnimationDone = true;
			this.Update();
			TweenS.AddTransformTween(this.m_creator.m_TC, TweenedProperty.Scale, TweenStyle.BounceOut, Vector3.zero, this.m_creator.m_TC.transform.localScale, 0.5f, 0f, true);
			SoundS.PlaySingleShot("/UI/DomeScreenAppear", Vector2.zero, 1f);
		}
		if (this.m_infoList != null && !this.m_infoAnimationDone)
		{
			this.m_infoAnimationDone = true;
			this.Update();
			TweenS.AddTransformTween(this.m_infoList.m_TC, TweenedProperty.Scale, TweenStyle.BounceOut, Vector3.zero, this.m_infoList.m_TC.transform.localScale, 0.5f, 0f, true);
			TweenS.AddTransformTween(this.m_middleBanner.m_TC, TweenedProperty.Scale, TweenStyle.BounceOut, Vector3.one, 0.4f, 0f, true);
			if (this.m_loadingEffectParticleSystem != null)
			{
				this.m_loadingEffectParticleSystem.Play();
			}
			this.m_loadingEffectPrefab.m_active = false;
		}
		base.Step();
	}

	// Token: 0x06001B05 RID: 6917 RVA: 0x0012E27C File Offset: 0x0012C67C
	public void SetBackButtonActive(bool flag)
	{
		if (this.m_exitButton != null)
		{
			if (flag)
			{
				this.m_exitButton.SetOrangeColors(true);
				this.m_exitButton.m_TAC.m_active = true;
			}
			else
			{
				this.m_exitButton.SetGrayColors();
				this.m_exitButton.m_TAC.m_active = false;
			}
			this.m_exitButton.Update();
		}
	}

	// Token: 0x06001B06 RID: 6918 RVA: 0x0012E2E3 File Offset: 0x0012C6E3
	public override void Destroy()
	{
		this.m_topBanner.Destroy();
		this.m_bottomBanner.Destroy();
		this.m_topBanner = null;
		this.m_bottomBanner = null;
		base.Destroy();
	}

	// Token: 0x04001D68 RID: 7528
	private PsUIScreenshot m_screenshot;

	// Token: 0x04001D69 RID: 7529
	private PsUITournamentCreatorInfo m_creator;

	// Token: 0x04001D6A RID: 7530
	private PsRacingLoadingScene m_scene;

	// Token: 0x04001D6B RID: 7531
	private UIText m_loadingText;

	// Token: 0x04001D6C RID: 7532
	private PsUIGenericButton m_exitButton;

	// Token: 0x04001D6D RID: 7533
	private bool m_building;

	// Token: 0x04001D6E RID: 7534
	private bool m_creatorAnimationDone;

	// Token: 0x04001D6F RID: 7535
	private bool m_infoAnimationDone;

	// Token: 0x04001D70 RID: 7536
	private bool m_gameStarted;

	// Token: 0x04001D71 RID: 7537
	private int m_exitTimer = 60;

	// Token: 0x04001D72 RID: 7538
	private UICanvas m_topBanner;

	// Token: 0x04001D73 RID: 7539
	private UICanvas m_bottomBanner;

	// Token: 0x04001D74 RID: 7540
	private UICanvas m_mainContainer;

	// Token: 0x04001D75 RID: 7541
	private UICanvas m_creatorInfoParent;

	// Token: 0x04001D76 RID: 7542
	private UICanvas m_leftContainer;

	// Token: 0x04001D77 RID: 7543
	private UICanvas m_rightContainer;

	// Token: 0x04001D78 RID: 7544
	private UICanvas m_middleBanner;

	// Token: 0x04001D79 RID: 7545
	private PrefabC m_loadingEffectPrefab;

	// Token: 0x04001D7A RID: 7546
	private ParticleSystem m_loadingEffectParticleSystem;

	// Token: 0x04001D7B RID: 7547
	private UIVerticalList m_infoList;
}
