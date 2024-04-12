using System;
using UnityEngine;

// Token: 0x02000231 RID: 561
public class PsUIAdventureGacha : UICanvas
{
	// Token: 0x060010B4 RID: 4276 RVA: 0x0009F778 File Offset: 0x0009DB78
	public PsUIAdventureGacha(UIComponent _parent, int _slotIndex)
		: base(_parent, true, "AdventureGacha", null, string.Empty)
	{
		int lastAddedIndex = PsGachaManager.m_lastAddedIndex;
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetDrawHandler(new UIDrawDelegate(this.GachaDrawhandler));
		this.m_contentHolder = new UICanvas(this, false, "AdventureGacha", null, string.Empty);
		this.m_contentHolder.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_contentHolder.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_contentHolder.RemoveDrawHandler();
		this.m_contentHolder.RemoveTouchAreas();
		this.m_tweenUnlockTexts = false;
		this.Init(_slotIndex);
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x0009F84C File Offset: 0x0009DC4C
	private void GachaDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.125f, 8, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#344744");
		Color color2 = DebugDraw.HexToColor("#4893a9");
		color.a = 0.8f;
		color2.a = 0.75f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 10f, roundedRect, 0.005f * (float)Screen.height, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line6Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 15f, ggdata, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0009F92C File Offset: 0x0009DD2C
	public void Init(int _slotIndex)
	{
		this.m_slotIndex = _slotIndex;
		if (PsState.m_activeGameLoop is PsGameLoopAdventure)
		{
			this.m_earnedPieces = PsState.m_activeGameLoop.m_scoreCurrent - PsState.m_activeGameLoop.m_scoreOld;
		}
		int vehicleIndex = PsState.GetVehicleIndex();
		this.m_mapPiecesMax = PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax;
		this.m_mapPiecesCount = PsMetagameManager.m_vehicleGachaData.m_mapPieceCount;
		if (this.m_mapSeed != PsMetagameManager.m_vehicleGachaData.m_mapSeed || this.m_pieces == null)
		{
			this.m_mapSeed = PsMetagameManager.m_vehicleGachaData.m_mapSeed;
			GachaMachine<int> gachaMachine = new GachaMachine<int>();
			this.m_pieces = new int[this.m_mapPiecesMax];
			for (int i = 0; i < this.m_mapPiecesMax; i++)
			{
				gachaMachine.AddItem(i, 1f, 1);
			}
			Random.seed = this.m_mapSeed;
			for (int j = 0; j < this.m_pieces.Length; j++)
			{
				this.m_pieces[j] = gachaMachine.GetItem(false);
			}
		}
		if (PsGachaManager.m_gachas[this.m_slotIndex] != null)
		{
			if (PsGachaManager.m_gachas[this.m_slotIndex].m_unlocked)
			{
				this.m_state = PsUIGachaState.Open;
			}
			else
			{
				PsGachaManager.UnlockGacha(PsGachaManager.m_gachas[this.m_slotIndex], true);
				if (this.m_state == PsUIGachaState.Collecting)
				{
					this.m_tweenUnlockTexts = true;
				}
				this.m_state = PsUIGachaState.Unlocking;
			}
		}
		else if (this.m_mapPiecesCount - this.m_earnedPieces >= this.m_mapPiecesMax)
		{
			PsGachaManager.AddGacha(PsGachaManager.GetNextGacha(), _slotIndex, true);
			PsMetagameManager.m_vehicleGachaData.m_adventureGachaCount++;
			PsMetagameManager.SendCurrentGachaData(true);
			PsGachaManager.UnlockGacha(PsGachaManager.m_gachas[this.m_slotIndex], true);
			if (this.m_state == PsUIGachaState.Collecting)
			{
				this.m_tweenUnlockTexts = true;
			}
			this.m_state = PsUIGachaState.Unlocking;
		}
		else
		{
			this.m_state = PsUIGachaState.Collecting;
		}
		this.Draw();
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x0009FB0C File Offset: 0x0009DF0C
	public void Draw()
	{
		this.m_contentHolder.DestroyChildren();
		this.m_level = null;
		if (this.m_state == PsUIGachaState.Open)
		{
			UIText uitext = new UIText(this.m_contentHolder, false, "AdventureGacha", PsStrings.Get(StringID.OPEN), PsFontManager.GetFont(PsFonts.HurmeBold), 0.035f, RelativeTo.ScreenHeight, "#ffffff", "#014b63");
			uitext.SetVerticalAlign(0.95f);
			uitext.SetDepthOffset(-8f);
			TweenC tweenC = TweenS.AddTransformTween(uitext.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one * 1.1f, 0.4f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicIn);
			this.CreateChest(this.m_state);
		}
		else if (this.m_state == PsUIGachaState.Unlocking)
		{
			this.m_timerArea = new UICanvas(this.m_contentHolder, false, "AdventureGacha", null, string.Empty);
			this.m_timerArea.SetHeight(0.2f, RelativeTo.ParentHeight);
			this.m_timerArea.SetWidth(0.8f, RelativeTo.ParentWidth);
			this.m_timerArea.SetAlign(0.5f, 0.925f);
			this.m_timerArea.SetMargins(0f, 0f, -0.02f, 0.02f, RelativeTo.ScreenHeight);
			this.m_timerArea.SetDrawHandler(new UIDrawDelegate(this.TimerBGDrawhandler));
			this.m_timeString = PsMetagameManager.GetTimeStringFromSeconds(PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft);
			this.m_timer = new UIText(this.m_timerArea, false, "AdventureGacha", this.m_timeString, PsFontManager.GetFont(PsFonts.HurmeBold), 0.95f, RelativeTo.ParentHeight, "#ffffff", "#014b63");
			this.m_timer.SetVerticalAlign(0.95f);
			this.m_timer.SetDepthOffset(-8f);
			if (this.m_tweenUnlockTexts)
			{
				TweenS.AddTransformTween(this.m_timerArea.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 0f, 1f), Vector3.one, 0.3f, 0f, true);
			}
			UICanvas uicanvas = new UICanvas(this.m_contentHolder, false, "AdventureGacha", null, string.Empty);
			uicanvas.SetHeight(0.2f, RelativeTo.ParentHeight);
			uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas.SetAlign(0.5f, 0f);
			uicanvas.SetDrawHandler(new UIDrawDelegate(this.UnlockDrawhandler));
			UIComponent uicomponent = new UIComponent(uicanvas, false, "AdventureGacha", null, null, string.Empty);
			uicomponent.SetHorizontalAlign(0.075f);
			uicomponent.SetHeight(0.75f, RelativeTo.ParentHeight);
			uicomponent.SetWidth(0.6f, RelativeTo.ParentWidth);
			uicomponent.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicomponent, false, "AdventureGacha", PsStrings.Get(StringID.OPEN_NOW), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#ffffff", "#014b63");
			uifittedText.SetDepthOffset(-8f);
			UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas, "AdventureGacha");
			uihorizontalList.SetVerticalAlign(0.05f);
			uihorizontalList.SetHorizontalAlign(1.025f);
			uihorizontalList.SetSpacing(0.0035f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			this.m_price = PsMetagameManager.SecondsToDiamonds(PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft);
			this.m_priceText = new UIText(uihorizontalList, false, "AdventureGacha", this.m_price.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.95f, RelativeTo.ParentHeight, "#ffffff", "#014b63");
			this.m_priceText.SetDepthOffset(-8f);
			UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, "AdventureGacha", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_diamond_small_full", null), true, true);
			uifittedSprite.SetHeight(0.025f, RelativeTo.ScreenHeight);
			uifittedSprite.SetDepthOffset(-8f);
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(this.m_contentHolder, false, "AdventureGacha", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_gacha_background_adventure", null), true, true);
			uifittedSprite2.SetHeight(0.9f, RelativeTo.ParentHeight);
			uifittedSprite2.SetVerticalAlign(0.5f);
			uifittedSprite2.SetDepthOffset(14f);
			if (this.m_tweenUnlockTexts)
			{
				TweenS.AddTransformTween(uicanvas.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 0f, 1f), Vector3.one, 0.3f, 0f, true);
				TweenS.AddTransformTween(uifittedSprite2.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, 0.35f, 0f, true);
			}
			this.CreateChest(this.m_state);
		}
		else if (this.m_state == PsUIGachaState.Collecting)
		{
			UICanvas uicanvas2 = new UICanvas(this.m_contentHolder, false, "AdventureGacha", null, string.Empty);
			uicanvas2.SetHeight(0.2f, RelativeTo.ParentHeight);
			uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas2.SetAlign(0.5f, 0f);
			uicanvas2.SetMargins(0.004f, RelativeTo.ScreenHeight);
			uicanvas2.SetDrawHandler(new UIDrawDelegate(this.UnlockDrawhandler));
			this.m_level = new UIFittedText(uicanvas2, false, "AdventureGacha", PsStrings.Get(StringID.LEVEL).ToUpper() + " " + (PsMetagameManager.m_playerStats.gachaLevel + 1), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#83ECFF", null);
			this.m_level.SetDepthOffset(-8f);
			this.CreateChest(this.m_state);
			this.m_chestCanvas.Update();
			TransformS.SetScale(this.m_chestCanvas.m_3DContent, Vector3.zero);
			this.CreateMap();
		}
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x000A006C File Offset: 0x0009E46C
	private void UnlockDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[19];
		float num = _c.m_parent.m_actualHeight * 0.125f;
		Vector2[] arc = DebugDraw.GetArc(num, 8, 90f, 270f, new Vector2(_c.m_actualWidth * 0.5f - num, -_c.m_actualHeight * 0.5f + num));
		Vector2[] arc2 = DebugDraw.GetArc(num, 8, 90f, 180f, new Vector2(_c.m_actualWidth * -0.5f + num, -_c.m_actualHeight * 0.5f + num));
		arc.CopyTo(array, 1);
		arc2.CopyTo(array, 9);
		array[0] = array[1] + Vector2.up * 0.01f * (float)Screen.height;
		array[array.Length - 2] = arc2[arc2.Length - 1] + Vector2.up * 0.01f * (float)Screen.height;
		array[array.Length - 1] = array[0];
		GGData ggdata = new GGData(array);
		Color color = DebugDraw.HexToColor("#334948");
		color.a = 0.85f;
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 12.5f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x000A0200 File Offset: 0x0009E600
	private void TimerBGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.35f, 8, new Vector2(0f, -_c.m_actualMargins.t));
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#177094");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, roundedRect, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		DebugDraw.ScaleVectorArray(roundedRect, new Vector2(0.98f, 1f));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f + Vector3.down * 0.0025f * (float)Screen.height, roundedRect, 0.01f * (float)Screen.height, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x000A0338 File Offset: 0x0009E738
	public void RevealPiece()
	{
		if (this.m_mapScript == null || this.m_earnedPieces <= 0)
		{
			return;
		}
		this.m_earnedPieces--;
		this.m_mapScript.RevealPiece(this.m_pieces[this.m_mapPiecesCount - this.m_earnedPieces - 1]);
		if (this.m_mapPiecesCount - this.m_earnedPieces >= this.m_mapPiecesMax)
		{
			this.m_mapScript.CompleteTransition();
			TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 0.7f, 0f, false, delegate(TimerC _c)
			{
				PsGameLoop currentNodeInfo = PsPlanetManager.GetCurrentPlanet().GetMainPath().GetCurrentNodeInfo();
				if (currentNodeInfo.m_node != null)
				{
					currentNodeInfo.m_node.DeActivateHighlight();
				}
				this.TweenChest();
				TimerS.RemoveComponent(_c);
			});
		}
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x000A03E2 File Offset: 0x0009E7E2
	public bool IsOpenAnimationRunning()
	{
		return this.m_openAnimation;
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x000A03EA File Offset: 0x0009E7EA
	public void AddOpenAnimationDoneCallback(Action _callback)
	{
		this.m_openAnimationDoneCallback = (Action)Delegate.Remove(this.m_openAnimationDoneCallback, _callback);
		this.m_openAnimationDoneCallback = (Action)Delegate.Combine(this.m_openAnimationDoneCallback, _callback);
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x000A041C File Offset: 0x0009E81C
	private void TweenChest()
	{
		this.m_openAnimation = true;
		SoundS.PlaySingleShot("/Metagame/ChangeIntoChest", Vector3.zero, 1f);
		TweenC tweenC = TweenS.AddTransformTween(this.m_chestCanvas.m_3DContent, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, new Vector3(1f, 1f, 1f), 0.35f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
		{
			TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 0.72f, 0f, false, delegate(TimerC c)
			{
				this.Init(this.m_slotIndex);
				this.Update();
				this.m_tweenUnlockTexts = false;
				TweenC tweenC2 = TweenS.AddTransformTween(this.m_chestCanvas.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.025f, 1.025f, 1f), 0.15f, 0f, true);
				TweenS.SetAdditionalTweenProperties(tweenC2, 0, true, TweenStyle.CubicInOut);
				TimerS.RemoveComponent(c);
				this.m_openAnimation = false;
				this.m_openAnimationDoneCallback.Invoke();
				this.m_openAnimationDoneCallback = delegate
				{
					Debug.Log("Chest open animations done!", null);
				};
			});
		});
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x000A0490 File Offset: 0x0009E890
	private void CreateMap()
	{
		UI3DRenderTextureCanvas ui3DRenderTextureCanvas = new UI3DRenderTextureCanvas(this.m_contentHolder, "AdventureGacha", null, false);
		ui3DRenderTextureCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		ui3DRenderTextureCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		ui3DRenderTextureCanvas.m_3DCamera.fieldOfView = 19f;
		ui3DRenderTextureCanvas.m_3DCameraPivot.transform.Rotate(0f, -180f, 0f, 0);
		ui3DRenderTextureCanvas.m_3DCameraOffset = -860f;
		ui3DRenderTextureCanvas.m_3DCamera.nearClipPlane = 1f;
		ui3DRenderTextureCanvas.m_3DCamera.farClipPlane = 950f;
		ui3DRenderTextureCanvas.RemoveTouchAreas();
		ui3DRenderTextureCanvas.SetAlign(0f, 0f);
		ui3DRenderTextureCanvas.SetDepthOffset(-2f);
		PrefabC prefabC = ui3DRenderTextureCanvas.AddGameObject(ResourceManager.GetGameObject(RESOURCE.TreasureMap3x4_GameObject), new Vector3(0f, 26f, 0f), default(Vector3));
		this.m_mapScript = prefabC.p_gameObject.GetComponent<TreasureMap>();
		if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar))
		{
			this.m_mapScript.SwapBackground(TreasureMapType.OffroadCar);
		}
		else
		{
			this.m_mapScript.SwapBackground(TreasureMapType.Motorcycle);
		}
		this.m_mapScript.HideAll();
		int[] array = new int[Mathf.Clamp(this.m_mapPiecesCount - this.m_earnedPieces, 0, this.m_mapPiecesMax)];
		if (array.Length > 0)
		{
			Array.Copy(this.m_pieces, array, Mathf.Clamp(this.m_mapPiecesCount - this.m_earnedPieces, 0, this.m_mapPiecesMax));
			this.m_mapScript.ShowPiece(array);
		}
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x000A061C File Offset: 0x0009EA1C
	private void CreateChest(PsUIGachaState _state)
	{
		this.m_chestCanvas = new UI3DRenderTextureCanvas(this.m_contentHolder, "AdventureGacha", null, false);
		this.m_chestCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_chestCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_chestCanvas.m_3DCamera.fieldOfView = 24f;
		this.m_chestCanvas.m_3DCameraPivot.transform.Rotate(0f, -10f, 0f, 0);
		this.m_chestCanvas.m_3DCameraPivot.transform.Rotate(15f, 0f, 0f, 1);
		this.m_chestCanvas.m_3DCameraPivot.transform.Translate(Vector3.up * -0.12f, 0);
		this.m_chestCanvas.m_3DCameraOffset = -4f;
		this.m_chestCanvas.m_3DCamera.nearClipPlane = 1f;
		this.m_chestCanvas.m_3DCamera.farClipPlane = 500f;
		this.m_chestCanvas.RemoveTouchAreas();
		this.m_chestCanvas.SetDepthOffset(2f);
		this.m_chestCanvas.SetMargins(0.1f, 0.1f, 0.3f, 0f, RelativeTo.OwnWidth);
		Texture texture = null;
		ResourcePool.Asset asset = RESOURCE.RewardChest_GameObject;
		Vector3 one = Vector3.one;
		if (_state == PsUIGachaState.Collecting)
		{
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureDisabled_Texture2D);
		}
		else
		{
			switch (PsGachaManager.m_gachas[this.m_slotIndex].m_gachaType)
			{
			case GachaType.WOOD:
				texture = ResourceManager.GetTexture(RESOURCE.ChestTextureWood_Texture2D);
				break;
			case GachaType.COMMON:
				texture = ResourceManager.GetTexture(RESOURCE.ChestTextureWood_Texture2D);
				break;
			case GachaType.BRONZE:
				texture = ResourceManager.GetTexture(RESOURCE.ChestTextureBronze_Texture2D);
				break;
			case GachaType.SILVER:
				texture = ResourceManager.GetTexture(RESOURCE.ChestTextureSilver_Texture2D);
				break;
			case GachaType.GOLD:
				texture = ResourceManager.GetTexture(RESOURCE.ChestTextureGold_Texture2D);
				break;
			case GachaType.RARE:
				asset = RESOURCE.ShopRewardChestT1_GameObject;
				one..ctor(0.9f, 0.9f, 0.9f);
				break;
			case GachaType.EPIC:
				asset = RESOURCE.ShopRewardChestT2_GameObject;
				one..ctor(0.9f, 0.9f, 0.9f);
				break;
			case GachaType.SUPER:
				asset = RESOURCE.ShopRewardChestT3_GameObject;
				one..ctor(0.9f, 0.9f, 0.9f);
				break;
			}
		}
		PrefabC prefabC = this.m_chestCanvas.AddGameObject(ResourceManager.GetGameObject(asset), new Vector3(0f, -0.45f, 0f), new Vector3(0f, 180f, 0f));
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
		VisualsRewardChest component = prefabC.p_gameObject.GetComponent<VisualsRewardChest>();
		if (component != null)
		{
			if (_state == PsUIGachaState.Unlocking)
			{
				component.SetToIdleState();
			}
			else if (_state == PsUIGachaState.Open)
			{
				component.SetToActiveState();
			}
		}
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x000A095B File Offset: 0x0009ED5B
	public void StartUnlocking()
	{
		Debug.LogWarning("Start");
		PsGachaManager.UnlockGacha(PsGachaManager.m_gachas[this.m_slotIndex], true);
		this.m_state = PsUIGachaState.Unlocking;
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x000A0980 File Offset: 0x0009ED80
	public void Open(bool _bought)
	{
		Debug.LogWarning("Open");
		PsGachaManager.m_lastOpenedGacha = PsGachaManager.m_gachas[this.m_slotIndex].m_gachaType;
		PsMetagameManager.ClearGachaDataMap(PsState.GetVehicleIndex(), false);
		PsGachaManager.m_lastGachaRewards = PsGachaManager.OpenGacha(PsGachaManager.m_gachas[this.m_slotIndex], (!_bought) ? this.m_slotIndex : (-1), true);
		PsMetrics.ChestOpened("Adventure");
		FrbMetrics.ChestOpened("adventure");
		this.m_state = PsUIGachaState.Collecting;
		PsPlanetManager.GetCurrentPlanet().FastForward();
		PsGameLoop currentNodeInfo = PsPlanetManager.GetCurrentPlanet().GetMainPath().GetCurrentNodeInfo();
		if (currentNodeInfo.m_node != null)
		{
			currentNodeInfo.m_node.ActivateHighlight();
		}
		PsUIBaseState psUIBaseState = new PsUIBaseState(typeof(PsUICenterOpenGacha), null, null, null, false, InitialPage.Center);
		psUIBaseState.SetAction("Exit", delegate
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
		});
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState);
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x000A0A7E File Offset: 0x0009EE7E
	public override void Destroy()
	{
		this.DestroyPopup();
		base.Destroy();
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x000A0A8C File Offset: 0x0009EE8C
	public override void Step()
	{
		if (PsState.m_activeGameLoop != null || !this.m_TC.p_entity.m_active)
		{
			base.Step();
			return;
		}
		if (this.m_state == PsUIGachaState.Unlocking)
		{
			if (PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft == 0)
			{
				this.m_state = PsUIGachaState.Open;
				this.Init(this.m_slotIndex);
				this.m_contentHolder.Update();
				return;
			}
			if (this.m_timer != null)
			{
				string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft);
				if (timeStringFromSeconds != this.m_timeString)
				{
					this.m_timeString = timeStringFromSeconds;
					this.m_timer.SetText(timeStringFromSeconds);
					if (this.m_priceText != null)
					{
						int num = PsMetagameManager.SecondsToDiamonds(PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft);
						if (num != this.m_price)
						{
							this.m_price = num;
							this.m_priceText.SetText(num.ToString());
						}
					}
				}
			}
		}
		if (this.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			for (int i = 0; i < PsState.m_openPopups.Count; i++)
			{
				if (PsState.m_openPopups[i] is PsUIBasePopup)
				{
					PsUIBasePopup psUIBasePopup = PsState.m_openPopups[i] as PsUIBasePopup;
					if (psUIBasePopup.m_mainContent is PsUIAdventureChestSlotPopup)
					{
						psUIBasePopup.CallAction("Exit");
						break;
					}
				}
			}
			if (this.m_state == PsUIGachaState.Open)
			{
				SoundS.PlaySingleShot("/UI/ButtonChestOpen", Vector3.zero, 1f);
				this.Open(false);
			}
			else if (this.m_state == PsUIGachaState.Unlocking)
			{
				SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
				CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
				this.DestroyPopup();
				this.m_popup = new PsUIBasePopup(typeof(PsUICenterUnlockChest), null, null, null, true, true, InitialPage.Center, false, false, false);
				int num2 = PsMetagameManager.SecondsToDiamonds(PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft);
				(this.m_popup.m_mainContent as PsUICenterUnlockChest).SetInfo(PsGachaManager.m_gachas[this.m_slotIndex].m_gachaType.ToString(), num2, PsStrings.Get(StringID.GACHA_LABEL_ADVENTURE).ToUpper(), true, this.m_slotIndex);
				this.m_popup.SetAction("Confirm", delegate
				{
					new PsUnlockGachaFlow(new Action(this.UnlockGachaWithDiamonds), delegate
					{
						this.DestroyPopup();
						CameraS.RemoveBlur();
					}, PsMetagameManager.SecondsToDiamonds(PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft));
				});
				this.m_popup.SetAction("Exit", delegate
				{
					this.DestroyPopup();
					CameraS.RemoveBlur();
				});
				TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
			}
		}
		base.Step();
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x000A0D62 File Offset: 0x0009F162
	public void DestroyPopup()
	{
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
			this.m_popup = null;
		}
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x000A0D84 File Offset: 0x0009F184
	private void UnlockGachaWithDiamonds()
	{
		int num = PsMetagameManager.SecondsToDiamonds(PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft);
		PsMetagameManager.m_playerStats.diamonds -= num;
		PsMetagameManager.m_playerStats.SetDirty(true);
		this.Open(true);
		BossBattles.AlterBothVehicleHandicaps(BossBattles.instaOpenChestChange);
		FrbMetrics.SpendVirtualCurrency("adventure_chest_timer", "gems", (double)num);
	}

	// Token: 0x040013B8 RID: 5048
	public PsUIGachaState m_state;

	// Token: 0x040013B9 RID: 5049
	public int m_slotIndex;

	// Token: 0x040013BA RID: 5050
	private UIText m_timer;

	// Token: 0x040013BB RID: 5051
	private UICanvas m_contentHolder;

	// Token: 0x040013BC RID: 5052
	private UICanvas m_timerArea;

	// Token: 0x040013BD RID: 5053
	private UIText m_priceText;

	// Token: 0x040013BE RID: 5054
	private bool m_tweenUnlockTexts;

	// Token: 0x040013BF RID: 5055
	private int m_mapPiecesCount;

	// Token: 0x040013C0 RID: 5056
	private int m_mapPiecesMax;

	// Token: 0x040013C1 RID: 5057
	private int m_mapSeed;

	// Token: 0x040013C2 RID: 5058
	private int m_earnedPieces;

	// Token: 0x040013C3 RID: 5059
	private int[] m_pieces;

	// Token: 0x040013C4 RID: 5060
	public UIFittedText m_level;

	// Token: 0x040013C5 RID: 5061
	private bool m_openAnimation;

	// Token: 0x040013C6 RID: 5062
	private Action m_openAnimationDoneCallback = delegate
	{
		Debug.Log("Chest open animations done!", null);
	};

	// Token: 0x040013C7 RID: 5063
	private TreasureMap m_mapScript;

	// Token: 0x040013C8 RID: 5064
	private UI3DRenderTextureCanvas m_chestCanvas;

	// Token: 0x040013C9 RID: 5065
	private PsUIBasePopup m_popup;

	// Token: 0x040013CA RID: 5066
	private string m_timeString;

	// Token: 0x040013CB RID: 5067
	private int m_price;
}
