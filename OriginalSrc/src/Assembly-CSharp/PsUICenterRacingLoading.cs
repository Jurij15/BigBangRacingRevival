using System;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public class PsUICenterRacingLoading : UICanvas
{
	// Token: 0x06001ABC RID: 6844 RVA: 0x00129D24 File Offset: 0x00128124
	public PsUICenterRacingLoading(UIComponent _parent)
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
		this.UpdateScreenshot();
		this.UpdateCreatorInfo();
		this.m_loadingText = new UIText(this, false, string.Empty, PsStrings.Get(StringID.LOADING), PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, "#ffffff", null);
		this.m_loadingText.SetAlign(1f, 0f);
		this.m_topBanner = new UICanvas(null, false, "BANNER", null, string.Empty);
		this.m_topBanner.SetCamera(this.m_camera, false, true);
		this.m_topBanner.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_topBanner.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_topBanner.SetAlign(0.5f, 1f);
		this.m_topBanner.SetDrawHandler(new UIDrawDelegate(this.BannerDrawhandler));
		this.m_topBanner.SetDepthOffset(10f);
		this.m_topBanner.Update();
		this.m_bottomBanner = new UICanvas(null, false, "BANNER", null, string.Empty);
		this.m_bottomBanner.SetCamera(this.m_camera, false, true);
		this.m_bottomBanner.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_bottomBanner.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_bottomBanner.SetAlign(0.5f, 0f);
		this.m_bottomBanner.SetDrawHandler(new UIDrawDelegate(this.BannerDrawhandler));
		this.m_bottomBanner.SetDepthOffset(10f);
		this.m_bottomBanner.Update();
		TweenS.AddTransformTween(this.m_topBanner.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_topBanner.m_TC.transform.localPosition + Vector3.up * (float)Screen.height * 0.12f, this.m_topBanner.m_TC.transform.localPosition, 0.2f, 0f, true);
		TweenS.AddTransformTween(this.m_bottomBanner.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_bottomBanner.m_TC.transform.localPosition + Vector3.down * (float)Screen.height * 0.12f, this.m_bottomBanner.m_TC.transform.localPosition, 0.2f, 0f, true);
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x0012A110 File Offset: 0x00128510
	private void UpdateScreenshot()
	{
		if (PsState.m_activeGameLoop == null)
		{
			return;
		}
		if (PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
			uicanvas.SetSize(0.1f, 0.1f, RelativeTo.ScreenHeight);
			uicanvas.SetAlign(0.5f, 1f);
			uicanvas.SetMargins(0f, 0f, 0.235f, -0.235f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			this.m_screenshot = new PsUILoadingScreenshot(uicanvas, false, "Screenshot", Vector2.zero, PsState.m_activeGameLoop, false, true, 0.045f, false);
			this.m_screenshot.SetSize(0.71999997f, 0.45f, RelativeTo.ScreenHeight);
			this.m_screenshot.SetAlign(0.5f, 1f);
			this.m_screenshot.SetDepthOffset(-10f);
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

	// Token: 0x06001ABE RID: 6846 RVA: 0x0012A258 File Offset: 0x00128658
	private void UpdateCreatorInfo()
	{
		if (PsState.m_activeGameLoop == null)
		{
			return;
		}
		if (PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			this.m_creator = new PsUICreatorInfo(this, true, false, false, false, false, false);
			this.m_creator.SetAlign(0.5f, 1f);
			this.m_creator.SetMargins(0f, 0f, 0.05f, -0.05f, RelativeTo.ScreenHeight);
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

	// Token: 0x06001ABF RID: 6847 RVA: 0x0012A314 File Offset: 0x00128714
	public void BannerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, rect, (float)Screen.height * 0.015f, Color.black, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, Color.black, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x0012A3C4 File Offset: 0x001287C4
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
				TweenS.AddTransformTween(this.m_screenshot.m_TC, TweenedProperty.Scale, TweenStyle.Linear, this.m_screenshot.m_TC.transform.localScale, Vector3.zero, 0.2f, 0f, true);
				TweenS.AddTransformTween(this.m_creator.m_TC, TweenedProperty.Scale, TweenStyle.Linear, this.m_creator.m_TC.transform.localScale, Vector3.zero, 0.2f, 0f, true);
			}
		}
		if (this.m_creator != null && !this.m_animationDone)
		{
			this.m_animationDone = true;
			this.Update();
			TweenS.AddTransformTween(this.m_creator.m_TC, TweenedProperty.Scale, TweenStyle.BounceOut, Vector3.zero, this.m_creator.m_TC.transform.localScale, 0.5f, 0f, true);
			SoundS.PlaySingleShot("/UI/DomeScreenAppear", Vector2.zero, 1f);
		}
		base.Step();
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x0012A5EC File Offset: 0x001289EC
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

	// Token: 0x06001AC2 RID: 6850 RVA: 0x0012A653 File Offset: 0x00128A53
	public override void Destroy()
	{
		this.m_topBanner.Destroy();
		this.m_bottomBanner.Destroy();
		this.m_topBanner = null;
		this.m_bottomBanner = null;
		base.Destroy();
	}

	// Token: 0x04001D2D RID: 7469
	private PsUIScreenshot m_screenshot;

	// Token: 0x04001D2E RID: 7470
	private PsUICreatorInfo m_creator;

	// Token: 0x04001D2F RID: 7471
	private PsRacingLoadingScene m_scene;

	// Token: 0x04001D30 RID: 7472
	private UIText m_loadingText;

	// Token: 0x04001D31 RID: 7473
	private PsUIGenericButton m_exitButton;

	// Token: 0x04001D32 RID: 7474
	private bool m_building;

	// Token: 0x04001D33 RID: 7475
	private bool m_animationDone;

	// Token: 0x04001D34 RID: 7476
	private bool m_gameStarted;

	// Token: 0x04001D35 RID: 7477
	private int m_exitTimer = 60;

	// Token: 0x04001D36 RID: 7478
	private UICanvas m_topBanner;

	// Token: 0x04001D37 RID: 7479
	private UICanvas m_bottomBanner;
}
