using System;
using UnityEngine;

// Token: 0x0200022F RID: 559
public class PsUIFreeGacha : UICanvas
{
	// Token: 0x060010A8 RID: 4264 RVA: 0x0009EC84 File Offset: 0x0009D084
	public PsUIFreeGacha(UIComponent _parent)
		: base(_parent, true, string.Empty, null, string.Empty)
	{
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetDrawHandler(new UIDrawDelegate(this.GachaDrawhandler));
		this.SetVerticalAlign(0f);
		this.m_contentHolder = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_contentHolder.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_contentHolder.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_contentHolder.RemoveDrawHandler();
		this.m_contentHolder.RemoveTouchAreas();
		this.Init();
		this.Draw(false);
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0009ED38 File Offset: 0x0009D138
	private void Init()
	{
		this.m_slotIndex = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.FREE);
		if (PsGachaManager.m_gachas[this.m_slotIndex] == null)
		{
			PsGacha surpriceChest = PsSurpriseGacha.GetSurpriceChest();
			PsGachaManager.AddGacha(surpriceChest, PsGachaManager.SlotType.FREE, false);
			PsGachaManager.UnlockGacha(surpriceChest, true);
		}
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x0009ED7C File Offset: 0x0009D17C
	private void Draw(bool _update = false)
	{
		this.m_timer = (this.m_open = null);
		this.m_contentHolder.DestroyChildren();
		if (PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft > 0)
		{
			this.RemoveDrawHandler();
			this.SetHeight(1f, RelativeTo.ParentHeight);
			UIText uitext = new UIText(this.m_contentHolder, false, string.Empty, PsStrings.Get(StringID.NEXT_SURPRISE), PsFontManager.GetFont(PsFonts.HurmeBold), 0.175f, RelativeTo.ParentHeight, "#B4F3FF", "#000000");
			uitext.SetAlign(0.5f, 0.6f);
			UICanvas uicanvas = new UICanvas(this.m_contentHolder, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.2f, RelativeTo.ParentHeight);
			uicanvas.SetWidth(0.8f, RelativeTo.ParentWidth);
			uicanvas.SetAlign(0.5f, 0f);
			uicanvas.SetMargins(0f, 0f, -0.005f, 0.005f, RelativeTo.ScreenHeight);
			uicanvas.SetDrawHandler(new UIDrawDelegate(this.TimerBGDrawhandler));
			this.m_timer = new UIText(uicanvas, false, string.Empty, this.GetTimeString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.95f, RelativeTo.ParentHeight, "#558D02", null);
			this.m_timer.SetVerticalAlign(0.95f);
			this.m_timer.SetDepthOffset(-8f);
			this.m_state = PsUIGachaState.Unlocking;
			this.CreateChest(false);
		}
		else
		{
			this.SetDrawHandler(new UIDrawDelegate(this.GachaDrawhandler));
			this.SetHeight(1f, RelativeTo.ParentHeight);
			UIText uitext2 = new UIText(this.m_contentHolder, false, string.Empty, PsStrings.Get(StringID.OPEN), PsFontManager.GetFont(PsFonts.HurmeBold), 0.035f, RelativeTo.ScreenHeight, "#ffffff", "#014b63");
			uitext2.SetVerticalAlign(0.95f);
			uitext2.SetDepthOffset(-8f);
			TweenC tweenC = TweenS.AddTransformTween(uitext2.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one * 1.1f, 0.4f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicIn);
			this.m_open = new UIText(this.m_contentHolder, false, string.Empty, "?", PsFontManager.GetFont(PsFonts.HurmeBold), 0.6f, RelativeTo.ParentHeight, "#9BFF13", "#000000");
			this.m_open.SetShadowShift(new Vector2(0f, -1f), 0.05f);
			this.m_open.SetVerticalAlign(0.4f);
			TweenC tweenC2 = TweenS.AddTransformTween(this.m_open.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one * 1.1f, 0.4f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC2, -1, true, TweenStyle.CubicIn);
			UIComponent uicomponent = new UIComponent(this.m_contentHolder, false, "shine base", null, null, string.Empty);
			uicomponent.SetDepthOffset(4f);
			uicomponent.RemoveDrawHandler();
			uicomponent.SetMargins(-0.3f, -0.3f, -0.3f, -0.3f, RelativeTo.ParentHeight);
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicomponent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shine", null), true, true);
			TweenC tweenC3 = TweenS.AddTransformTween(uifittedSprite.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, -360f), 25f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC3, -1, false, TweenStyle.Linear);
			this.m_state = PsUIGachaState.Open;
			this.CreateChest(true);
		}
		if (_update)
		{
			this.Update();
		}
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x0009F0CF File Offset: 0x0009D4CF
	private string GetTimeString()
	{
		return PsMetagameManager.GetTimeStringFromSeconds(PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft);
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x0009F0E8 File Offset: 0x0009D4E8
	public override void Step()
	{
		if (PsState.m_activeGameLoop != null)
		{
			base.Step();
			return;
		}
		if (this.m_state == PsUIGachaState.Unlocking)
		{
			if (PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft < 1)
			{
				this.m_state = PsUIGachaState.Open;
				this.Draw(true);
			}
			else
			{
				this.m_timer.SetText(this.GetTimeString());
			}
		}
		if (this.m_hit)
		{
			if (this.m_state == PsUIGachaState.Open)
			{
				TouchAreaS.CancelAllTouches(null);
				SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
				this.Open();
			}
			else if (this.m_state == PsUIGachaState.Unlocking)
			{
				Debug.LogError("Still unlocking: " + PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft);
			}
		}
		base.Step();
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x0009F1C0 File Offset: 0x0009D5C0
	public void Open()
	{
		PsPlanetManager.GetCurrentPlanet().FastForward();
		PsGameLoop currentNodeInfo = PsPlanetManager.GetCurrentPlanet().GetMainPath().GetCurrentNodeInfo();
		if (currentNodeInfo != null && currentNodeInfo.m_node != null)
		{
			currentNodeInfo.m_node.ActivateHighlight();
		}
		int slotIndex = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.FREE);
		PsGacha psGacha = PsGachaManager.m_gachas[slotIndex];
		PsUICenterRouletteChest.m_prize = psGacha.m_gachaType;
		Debug.Log("prize is: " + psGacha.m_gachaType, null);
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		PsMainMenuState.m_popup = new PsUIBasePopup(typeof(PsUICenterRouletteChest), null, null, null, true, true, InitialPage.Center, false, false, false);
		PsMainMenuState.m_popup.SetAction("Exit", delegate
		{
			this.Init();
			PsMainMenuState.m_popup.Destroy();
			PsMainMenuState.m_popup = null;
			CameraS.RemoveBlur();
		});
		PsMainMenuState.m_popup.SetAction("Confirm", delegate
		{
			PsUIBaseState psUIBaseState = new PsUIBaseState(typeof(PsUICenterOpenGacha), null, null, null, false, InitialPage.Center);
			psUIBaseState.SetAction("Exit", delegate
			{
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
			});
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState);
		});
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0009F2AC File Offset: 0x0009D6AC
	private void TimerBGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.35f, 8, new Vector2(0f, -_c.m_actualMargins.t));
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#7FD709");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, roundedRect, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		DebugDraw.ScaleVectorArray(roundedRect, new Vector2(0.98f, 1f));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f + Vector3.down * 0.0025f * (float)Screen.height, roundedRect, 0.01f * (float)Screen.height, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0009F3E4 File Offset: 0x0009D7E4
	private void GachaDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualWidth * 0.125f, 8, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#344744");
		Color color2 = DebugDraw.HexToColor("#7FD709");
		color.a = 0.8f;
		color2.a = 0.75f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 10f, roundedRect, 0.005f * (float)Screen.height, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line6Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 15f, ggdata, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0009F4C4 File Offset: 0x0009D8C4
	private void CreateChest(bool _activateChest)
	{
		this.m_chestCanvas = new UI3DRenderTextureCanvas(this.m_contentHolder, string.Empty, null, false);
		this.m_chestCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_chestCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_chestCanvas.m_3DCamera.fieldOfView = 24f;
		this.m_chestCanvas.m_3DCameraPivot.transform.Rotate(0f, -10f, 0f, 0);
		this.m_chestCanvas.m_3DCameraPivot.transform.Rotate(15f, 0f, 0f, 1);
		this.m_chestCanvas.m_3DCameraPivot.transform.Translate(Vector3.up * -0.12f, 0);
		this.m_chestCanvas.m_3DCameraOffset = -3.5f;
		this.m_chestCanvas.m_3DCamera.nearClipPlane = 1f;
		this.m_chestCanvas.m_3DCamera.farClipPlane = 500f;
		this.m_chestCanvas.RemoveTouchAreas();
		this.m_chestCanvas.SetDepthOffset(2f);
		this.m_chestCanvas.SetMargins(0.1f, 0.1f, 0.3f, 0f, RelativeTo.OwnWidth);
		Vector3 one = Vector3.one;
		Texture texture = ResourceManager.GetTexture(RESOURCE.ChestTextureDisabled_Texture2D);
		PrefabC prefabC = this.m_chestCanvas.AddGameObject(ResourceManager.GetGameObject(RESOURCE.RewardChest_GameObject), new Vector3(0f, -0.5f, 0f), new Vector3(0f, 180f, 0f));
		prefabC.p_gameObject.transform.localScale = one;
		if (texture != null)
		{
			Renderer[] componentsInChildren = prefabC.p_gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].material.name.StartsWith("RewardChest"))
				{
					componentsInChildren[i].material.mainTexture = texture;
				}
			}
		}
		if (_activateChest)
		{
			VisualsRewardChest component = prefabC.p_gameObject.GetComponent<VisualsRewardChest>();
			component.SetToActiveState();
		}
	}

	// Token: 0x040013AA RID: 5034
	private UIText m_timer;

	// Token: 0x040013AB RID: 5035
	private UIText m_open;

	// Token: 0x040013AC RID: 5036
	private UICanvas m_contentHolder;

	// Token: 0x040013AD RID: 5037
	private int m_slotIndex;

	// Token: 0x040013AE RID: 5038
	private PsUIGachaState m_state;

	// Token: 0x040013AF RID: 5039
	private UI3DRenderTextureCanvas m_chestCanvas;
}
