using System;
using UnityEngine;

// Token: 0x0200029C RID: 668
public class PsUICenterGameCard : UICanvas
{
	// Token: 0x0600142E RID: 5166 RVA: 0x000CCC10 File Offset: 0x000CB010
	public PsUICenterGameCard(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		this.m_metadataLoaded = !PsState.m_activeGameLoop.m_loadingMetaData;
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetHeight(0.85f, RelativeTo.ParentHeight);
		this.SetVerticalAlign(0f);
		this.RemoveDrawHandler();
		this.CreateHeader();
		this.CreateMinigameCard();
		this.CreateButtons();
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x000CCCC8 File Offset: 0x000CB0C8
	public override void Step()
	{
		if (!this.m_metadataLoaded && !PsState.m_activeGameLoop.m_loadingMetaData)
		{
			this.SetMetadataInfo();
		}
		if (this.m_playButton.m_hit)
		{
			SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
			(this.GetRoot() as PsUIBasePopup).CallAction("Play");
			(Main.m_currentGame.m_currentScene.GetCurrentState() as PsUIBaseState).DestroyCanvas();
		}
		base.Step();
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x000CCD50 File Offset: 0x000CB150
	public virtual void SetMetadataInfo()
	{
		this.m_animation.Destroy();
		this.m_animation = null;
		this.m_screenshot.m_gameId = PsState.m_activeGameLoop.GetGameId();
		this.m_screenshot.LoadPicture();
		this.m_profileIcon.m_facebookId = PsState.m_activeGameLoop.GetFacebookId();
		this.m_profileIcon.LoadPicture();
		this.CreateLevelAndCreatorNames(this.m_nameList);
		this.m_description.SetText(PsState.m_activeGameLoop.GetDescription());
		this.m_playButton.SetGreenColors(true);
		this.m_playButton.EnableTouchAreas(true);
		this.m_metadataLoaded = true;
		(this.GetRoot() as PsUIBasePopup).Update();
		this.Update();
	}

	// Token: 0x06001431 RID: 5169 RVA: 0x000CCE05 File Offset: 0x000CB205
	public virtual void CreateHeader()
	{
	}

	// Token: 0x06001432 RID: 5170 RVA: 0x000CCE08 File Offset: 0x000CB208
	public virtual void CreateHeaderIcon(UICanvas _parent)
	{
		string text = null;
		if (!this.m_metadataLoaded || PsState.m_activeGameLoop.GetPlayerUnit() == "OffroadCar")
		{
			text = "item_player_monster_car_icon";
		}
		else if (PsState.m_activeGameLoop.GetPlayerUnit() == "Motorcycle")
		{
			text = "item_player_motorcycle_icon";
		}
		if (text != null)
		{
			this.m_unitIcon = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
			if (!this.m_metadataLoaded)
			{
				this.m_unitIcon.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
			}
			this.m_unitIcon.SetHorizontalAlign(0f);
		}
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x000CCEC8 File Offset: 0x000CB2C8
	public virtual void CreateHeaderText(UICanvas _parent)
	{
		string text = "Path Level " + PsState.m_activeGameLoop.m_levelNumber;
		this.m_contextText = new UIFittedText(_parent, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", "#5c3a0a");
		this.m_contextText.SetHorizontalAlign(0f);
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x000CCF24 File Offset: 0x000CB324
	public virtual void CreateHeaderScore(UICanvas _parent)
	{
		int num = 0;
		if (this.m_metadataLoaded)
		{
			num = PsState.m_activeGameLoop.m_scoreBest;
		}
		string text = "menu_mode_star_" + num;
		this.m_scoreIcon = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
		this.m_scoreIcon.SetAlign(1f, 1f);
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x000CCF9C File Offset: 0x000CB39C
	public virtual void CreateMinigameCard()
	{
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetDrawHandler(new UIDrawDelegate(this.MiddleCardDrawHandler));
		uicanvas.SetHeight(0.5f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas, string.Empty);
		uihorizontalList.SetMargins(0.035f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_screenshot = new PsUIScreenshot(uihorizontalList, false, "Screenshot", Vector2.zero, PsState.m_activeGameLoop, this.m_metadataLoaded, true, 0.045f, false);
		this.m_screenshot.SetSize(0.2f, 0.2f, RelativeTo.ScreenWidth);
		this.m_screenshot.SetVerticalAlign(1f);
		this.m_screenshot.SetDepthOffset(-10f);
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList.SetMargins(0f, 0f, 0.025f, 0f, RelativeTo.ScreenShortest);
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uiverticalList.RemoveDrawHandler();
		this.m_profileCanvas = new UICanvas(uiverticalList, true, "GameCardProfileCanvas", null, string.Empty);
		this.m_profileCanvas.SetWidth(0.4f, RelativeTo.ScreenWidth);
		this.m_profileCanvas.SetHeight(0.05f, RelativeTo.ScreenWidth);
		this.m_profileCanvas.SetHorizontalAlign(0f);
		this.m_profileCanvas.RemoveDrawHandler();
		this.m_profileCanvas.m_TAC.m_letTouchesThrough = true;
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(this.m_profileCanvas, string.Empty);
		uihorizontalList2.SetHorizontalAlign(0f);
		uihorizontalList2.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList2.RemoveDrawHandler();
		string text = string.Empty;
		string text2 = string.Empty;
		if (this.m_metadataLoaded)
		{
			text = PsState.m_activeGameLoop.GetFacebookId();
			text2 = PsState.m_activeGameLoop.GetGamecenterId();
		}
		this.m_profileIcon = new PsUIProfileImage(uihorizontalList2, false, string.Empty, text, text2, -1, this.m_metadataLoaded, false, false, 0.1f, 0.06f, "fff9e6", null, false, true);
		this.m_profileIcon.SetSize(0.05f, 0.05f, RelativeTo.ScreenWidth);
		this.m_nameList = new UIVerticalList(uihorizontalList2, string.Empty);
		this.m_nameList.SetMargins(0f, 0f, -0.01f, 0f, RelativeTo.ScreenShortest);
		this.m_nameList.SetVerticalAlign(1f);
		this.m_nameList.RemoveDrawHandler();
		if (this.m_metadataLoaded)
		{
			this.CreateLevelAndCreatorNames(this.m_nameList);
		}
		string text3 = "Receiving data, hold still!";
		if (this.m_metadataLoaded)
		{
			text3 = PsState.m_activeGameLoop.GetDescription();
		}
		this.m_description = new UITextbox(uiverticalList, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Left, Align.Top, null, true, null);
		this.m_description.SetWidth(0.5f, RelativeTo.ParentWidth);
		this.m_description.SetAlign(0f, 1f);
		if (!this.m_metadataLoaded)
		{
			this.m_animation = new PsUILoadingAnimation(this, false);
		}
		uiverticalList.SetVerticalAlign(1f);
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x000CD2A8 File Offset: 0x000CB6A8
	public virtual void CreateLevelAndCreatorNames(UIComponent _parent)
	{
		UIText uitext = new UIText(_parent, false, string.Empty, PsState.m_activeGameLoop.GetName(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenWidth, "#a0ff00", null);
		uitext.SetHorizontalAlign(0f);
		UIText uitext2 = new UIText(_parent, false, string.Empty, PsStrings.Get(StringID.CREATOR_TEXT) + " " + PsState.m_activeGameLoop.GetCreatorName(), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.0175f, RelativeTo.ScreenWidth, "#fffec6", null);
		uitext2.SetHorizontalAlign(0f);
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x000CD334 File Offset: 0x000CB734
	public virtual void CreateButtons()
	{
		this.m_playButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		this.m_playButton.m_camera = this.m_camera;
		this.m_playButton.SetGreenColors(true);
		this.m_playButton.SetText(PsStrings.Get(StringID.PLAY), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_playButton.SetAlign(0.8f, 0.15f);
		if (!this.m_metadataLoaded)
		{
			this.m_playButton.SetGreenColors(false);
			this.m_playButton.DisableTouchAreas(true);
		}
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x000CD3D8 File Offset: 0x000CB7D8
	public void HeaderDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, (float)Screen.width * 0.025f, 8, Vector2.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 5f, roundedRect, DebugDraw.HexToUint(this.m_colorBoxDown), DebugDraw.HexToUint(this.m_colorBoxUp), ResourceManager.GetMaterial(RESOURCE.MenuMetalLightMat_Material), _c.m_camera, "BG", null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, roundedRect, (float)Screen.height * 0.0125f, DebugDraw.HexToColor(this.m_colorStroke), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line16Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x000CD4A8 File Offset: 0x000CB8A8
	public void MiddleCardDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		string text = "008ce5";
		string text2 = "0061a3";
		string text3 = "46acec";
		string text4 = "003b64";
		Color color = DebugDraw.HexToColor(text4);
		color.a = 0.5f;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, (float)Screen.width * 0.1125f, 16, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth - (float)Screen.height * 0.02f, _c.m_actualHeight - (float)Screen.height * 0.02f, (float)Screen.width * 0.1075f, 16, Vector2.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 5f, roundedRect, DebugDraw.HexToUint(text2), DebugDraw.HexToUint(text), ResourceManager.GetMaterial(RESOURCE.MenuMetalMat_Material), _c.m_camera, "BG", null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, roundedRect, (float)Screen.height * 0.005f, DebugDraw.HexToColor(text4), DebugDraw.HexToColor(text3), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, roundedRect2, (float)Screen.height * 0.0075f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x040016F1 RID: 5873
	public UICanvas m_profileCanvas;

	// Token: 0x040016F2 RID: 5874
	public PsUIGenericButton m_playButton;

	// Token: 0x040016F3 RID: 5875
	protected string m_colorBoxUp = "0072d7";

	// Token: 0x040016F4 RID: 5876
	protected string m_colorBoxDown = "005cb5";

	// Token: 0x040016F5 RID: 5877
	protected string m_colorStroke = "1fb7f6";

	// Token: 0x040016F6 RID: 5878
	protected string m_colorBevelUp = "0072d7";

	// Token: 0x040016F7 RID: 5879
	protected string m_colorBevelDown = "0072d7";

	// Token: 0x040016F8 RID: 5880
	protected bool m_metadataLoaded;

	// Token: 0x040016F9 RID: 5881
	protected UIFittedSprite m_unitIcon;

	// Token: 0x040016FA RID: 5882
	protected UIFittedText m_contextText;

	// Token: 0x040016FB RID: 5883
	protected UIFittedSprite m_scoreIcon;

	// Token: 0x040016FC RID: 5884
	protected PsUIScreenshot m_screenshot;

	// Token: 0x040016FD RID: 5885
	protected PsUIProfileImage m_profileIcon;

	// Token: 0x040016FE RID: 5886
	protected UIVerticalList m_nameList;

	// Token: 0x040016FF RID: 5887
	protected UITextbox m_description;

	// Token: 0x04001700 RID: 5888
	protected PsUILoadingAnimation m_animation;
}
