using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A8 RID: 680
public class PsUICenterGarage : UICanvas
{
	// Token: 0x06001467 RID: 5223 RVA: 0x000D05E0 File Offset: 0x000CE9E0
	public PsUICenterGarage(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.m_vehicleType = PsState.GetCurrentVehicleType(true);
		if (PsUICenterGarage.m_fixedVehicle)
		{
			this.m_vehicleType = PsUICenterGarage.m_selectVehicle;
			PsUICenterGarage.m_fixedVehicle = false;
		}
		PsMetrics.GarageEntered(this.m_vehicleType);
		FrbMetrics.SetCurrentScreen("garage_" + this.m_vehicleType.ToString());
		this.m_resources = false;
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		this.m_scrollableCanvas = new UIScrollableSnappingCanvas(this, "scrollCanvas", 2);
		this.m_scrollableCanvas.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_scrollableCanvas.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_scrollableCanvas.DisableTouchAreas(true);
		this.m_scrollableCanvas.m_maxScrollInertialX = 500f * (float.Parse(Screen.width.ToString()) / 1024f);
		this.m_scrollableCanvas.m_maxScrollInertialY = 0f;
		this.m_scrollableCanvas.RemoveDrawHandler();
		float num = 0f;
		if (PsUICenterGarage.m_startView == -1)
		{
			this.m_scrollableCanvas.SetScrollPosition(0f, 0f);
			this.m_scrollableCanvas.m_currentPageX = 0;
		}
		else
		{
			this.m_scrollableCanvas.SetScrollPosition(1f, 0f);
			this.m_scrollableCanvas.m_currentPageX = 1;
			num = 1f;
		}
		this.m_scrollableCanvasContent = new UIHorizontalList(this.m_scrollableCanvas, "hlist");
		this.m_scrollableCanvasContent.SetSpacing(0f, RelativeTo.ScreenHeight);
		this.m_scrollableCanvasContent.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_scrollableCanvasContent.SetVerticalAlign(0f);
		this.m_scrollableCanvasContent.SetHorizontalAlign(num);
		this.m_scrollableCanvasContent.RemoveDrawHandler();
		this.m_upgradeView = new PsUIUpgradeView(this.m_scrollableCanvasContent, this.m_vehicleType);
		this.m_upgradeView.UpgradeWasPurchased += this.UpgradeWasPurchased;
		UICanvas uicanvas = new UICanvas(this.m_scrollableCanvasContent, false, "RightArea", null, string.Empty);
		uicanvas.SetHeight(1f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.5f, RelativeTo.ScreenWidth);
		uicanvas.RemoveDrawHandler();
		int num2 = (int)(PsUpgradeManager.GetBasePerformance(this.m_vehicleType) / 4f);
		this.m_vehicleStats = new PsUIVehicleStats2(uicanvas, this.m_vehicleType, (int)PsUpgradeManager.GetCurrentPerformance(this.m_vehicleType), (int)PsUpgradeManager.GetCurrentEfficiency(this.m_vehicleType, PsUpgradeManager.UpgradeType.SPEED) + num2, (int)PsUpgradeManager.GetCurrentEfficiency(this.m_vehicleType, PsUpgradeManager.UpgradeType.GRIP) + num2, (int)PsUpgradeManager.GetCurrentEfficiency(this.m_vehicleType, PsUpgradeManager.UpgradeType.HANDLING) + num2, PsUpgradeManager.GetPowerUpItemsCurrentPerformance(this.m_vehicleType) + num2);
		this.m_vehicleStats.SetVerticalAlign(0.03f);
		this.m_modelCanvas = new UI3DRenderTextureCanvas(uicanvas, "RotateCanvas", null, true);
		this.m_modelCanvas.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_modelCanvas.SetWidth(1.3f, RelativeTo.ParentWidth);
		this.m_modelCanvas.SetAlign(0.5f, 0f);
		this.m_modelCanvas.m_3DCamera.fieldOfView = 18f;
		this.m_modelCanvas.m_3DCameraPivot.transform.Rotate(0f, -40f, 0f, 0);
		this.m_modelCanvas.m_3DCameraPivot.transform.Rotate(5f, 0f, 0f, 1);
		this.m_modelCanvas.m_3DCameraPivot.transform.Translate(Vector3.up * 10f, 1);
		this.m_modelCanvas.SetTouchHandler(new TouchEventDelegate(this.RotateCamera));
		this.m_basePrefab = this.m_modelCanvas.AddGameObject(ResourceManager.GetGameObject(RESOURCE.GarageBase_GameObject), new Vector3(0f, -30f, 0f), new Vector3(-90f, 0f, 0f));
		this.m_categoryButtonHolder = new UIHorizontalList(uicanvas, "CategoryButtons");
		this.m_categoryButtonHolder.SetVerticalAlign(0f);
		this.m_categoryButtonHolder.SetMargins(0f, 0f, 0f, 0.06f, RelativeTo.ScreenHeight);
		this.m_categoryButtonHolder.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		this.m_categoryButtonHolder.SetDepthOffset(-2f);
		this.m_categoryButtonHolder.RemoveDrawHandler();
		this.m_categryButtons = new List<PsUIGenericButton>();
		PsUIGenericButton psUIGenericButton = this.CreateCategoryButton(this.m_categoryButtonHolder, "menu_garage_icon_trails", new cpBB(0f, 0f, 0f, 0f));
		psUIGenericButton.m_customObject = 1;
		this.m_categryButtons.Add(psUIGenericButton);
		PsUIGenericButton psUIGenericButton2 = this.CreateCategoryButton(this.m_categoryButtonHolder, "menu_garage_icon_hats", new cpBB(0.01f, 0.01f, 0.01f, 0.01f));
		psUIGenericButton2.m_customObject = 0;
		this.m_categryButtons.Add(psUIGenericButton2);
		if (PsUICenterGarage.m_startView == 0)
		{
			this.ActivateButton(psUIGenericButton2);
		}
		else
		{
			this.ActivateButton(psUIGenericButton);
		}
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_button_customize", null);
		this.m_customisetionViewButton = new UIRectSpriteButton(uicanvas, string.Empty, PsState.m_uiSheet, frame, true, false);
		this.m_customisetionViewButton.SetSound("/UI/GarageScreenSwitch");
		this.m_customisetionViewButton.SetHeight(0.18f, RelativeTo.ScreenHeight);
		this.m_customisetionViewButton.SetAlign(0.96f, 0.5f);
		this.m_customisetionViewButton.SetDepthOffset(-5f);
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_icon_hats", null);
		UISprite uisprite = new UISprite(this.m_customisetionViewButton, false, "Icon", PsState.m_uiSheet, frame2, true);
		uisprite.SetSize(1f, frame2.height / frame2.width, RelativeTo.ParentWidth);
		frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_button_upgrade", null);
		this.m_upgradeViewButton = new UIRectSpriteButton(uicanvas, string.Empty, PsState.m_uiSheet, frame, true, false);
		EntityManager.AddTagForEntity(this.m_upgradeViewButton.m_TC.p_entity, "ChangeView");
		this.m_upgradeViewButton.SetSound("/UI/GarageScreenSwitch");
		this.m_upgradeViewButton.SetHeight(0.18f, RelativeTo.ScreenHeight);
		this.m_upgradeViewButton.SetAlign(0.04f, 0.5f);
		this.m_upgradeViewButton.SetDepthOffset(-5f);
		Frame frame3 = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_icon_upgrade", null);
		UISprite uisprite2 = new UISprite(this.m_upgradeViewButton, false, "Icon", PsState.m_uiSheet, frame3, true);
		uisprite2.SetSize(1f, 1f, RelativeTo.ParentWidth);
		this.m_customisationViewHolder = new UICanvas(this.m_scrollableCanvasContent, false, string.Empty, null, string.Empty);
		this.m_customisationViewHolder.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_customisationViewHolder.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.m_customisationViewHolder.RemoveDrawHandler();
		this.CreateCustomisationView(PsUICenterGarage.m_startView);
		this.LoadVehicle();
		PsUICenterGarage.m_startView = -1;
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x000D0CF4 File Offset: 0x000CF0F4
	public static void SetVehicle(Type _vehicleType)
	{
		PsUICenterGarage.m_selectVehicle = _vehicleType;
		PsUICenterGarage.m_fixedVehicle = true;
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x000D0D02 File Offset: 0x000CF102
	public void StartInCustomisationView()
	{
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x000D0D04 File Offset: 0x000CF104
	private PsUIGenericButton CreateCategoryButton(UIComponent _parent, string _iconName, cpBB _iconMargins)
	{
		PsUIGenericButton psUIGenericButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		EntityManager.AddTagForEntity(psUIGenericButton.m_TC.p_entity, "ChangeView");
		PsUIGenericButton psUIGenericButton2 = psUIGenericButton;
		float num = 0.12f;
		float num2 = 0.14f;
		psUIGenericButton2.SetIcon(_iconName, num, num2, "#FFFFFF", _iconMargins, true);
		psUIGenericButton.m_UIsprite.SetDepthOffset(-5f);
		psUIGenericButton.SetHeight(0.13f, RelativeTo.ScreenHeight);
		psUIGenericButton.SetMargins(0f, RelativeTo.ScreenHeight);
		return psUIGenericButton;
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x000D0D8C File Offset: 0x000CF18C
	private void ActivateButton(PsUIGenericButton _button)
	{
		if (this.m_activeButton != null)
		{
			this.m_activeButton.SetBlueColors(true);
			this.m_activeButton.EnableTouchAreas(true);
			this.m_activeButton.Update();
		}
		_button.SetBlueColors(false);
		_button.DisableTouchAreas(true);
		_button.Update();
		this.m_activeButton = _button;
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x000D0DE4 File Offset: 0x000CF1E4
	private void SetCustomisationView(int _view)
	{
		if (this.m_currentCustomisationViews != null)
		{
			this.m_currentCustomisationViews.HideUI(delegate
			{
				this.CreateCustomisationView(_view);
				this.m_customisationViewHolder.Update();
				this.m_currentCustomisationViews.ShowUI();
			});
		}
		else
		{
			this.CreateCustomisationView(_view);
			this.m_customisationViewHolder.Update();
		}
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x000D0E44 File Offset: 0x000CF244
	private void CreateCustomisationView(int _view)
	{
		if (_view != 0)
		{
			if (_view != 1)
			{
			}
			if (this.m_currentCustomisationViews != null)
			{
				this.m_currentCustomisationViews.Destroy();
				this.m_currentCustomisationViews = null;
			}
			this.m_currentCustomisationViews = new PsUITrailSelectionView(this.m_customisationViewHolder, this.m_vehicleType);
			this.m_currentCustomisationViews.m_itemChangedAction = new Action<string>(this.SetTrail);
		}
		else
		{
			if (this.m_currentCustomisationViews != null)
			{
				this.m_currentCustomisationViews.Destroy();
				this.m_currentCustomisationViews = null;
			}
			this.m_currentCustomisationViews = new PsUIHatSelectionView(this.m_customisationViewHolder, this.m_vehicleType);
			this.m_currentCustomisationViews.m_itemChangedAction = new Action<string>(this.SetPropToLocator);
		}
		PsUIBasePopup psUIBasePopup = this.GetRoot() as PsUIBasePopup;
		if (psUIBasePopup != null && psUIBasePopup.m_overlayCamera != null)
		{
			CameraS.MoveToFront(psUIBasePopup.m_overlayCamera);
		}
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x000D0F34 File Offset: 0x000CF334
	private void SetTrail(string _identifier)
	{
		if (this.m_currentVisualTrail == _identifier)
		{
			return;
		}
		this.m_currentVisualTrail = _identifier;
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
			this.m_trailBase.SetPreviewMode(this.m_vehiclePrefab.p_parentTC.transform, Vector3.up * 10f, this.m_modelCanvas.m_3DCamera.gameObject.layer);
		}
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x000D1050 File Offset: 0x000CF450
	public void FreezePreviewRotation()
	{
		this.m_frozenPreviewRotation = true;
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x000D1059 File Offset: 0x000CF459
	public void ReleasePreviewRotation()
	{
		this.m_frozenPreviewRotation = false;
		if (this.m_previewRotationTween != null)
		{
			TweenS.RemoveComponent(this.m_previewRotationTween);
			this.m_previewRotationTween = null;
		}
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x000D1080 File Offset: 0x000CF480
	public void RotateVehicle()
	{
		if (!this.m_frozenPreviewRotation)
		{
			this.m_rotateSpeed *= 0.95f;
			this.m_rotateSpeed = ToolBox.limitBetween(this.m_rotateSpeed, -100f, 100f);
			float num = 0.2f;
			TransformS.SetGlobalRotation(this.m_modelCanvas.m_3DCameraPivot, this.m_modelCanvas.m_3DCameraPivot.transform.rotation.eulerAngles + new Vector3(0f, this.m_rotateSpeed + num, 0f));
		}
		else if (this.m_previewRotationTween == null)
		{
			Vector3 eulerAngles = this.m_modelCanvas.m_3DCameraPivot.transform.rotation.eulerAngles;
			float num2 = ((eulerAngles.y >= 30f) ? 210f : (-150f));
			this.m_previewRotationTween = TweenS.AddTransformTween(this.m_modelCanvas.m_3DCameraPivot, TweenedProperty.Rotation, TweenStyle.BackOut, new Vector3(eulerAngles.x, num2, eulerAngles.z), 1f, 0f, false);
		}
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x000D11A0 File Offset: 0x000CF5A0
	public void RotateCamera(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (!this.m_frozenPreviewRotation && !_touchIsSecondary && (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut))
		{
			this.m_rotateSpeed += _touches[0].m_deltaPosition.x * (80f / (float)Screen.width);
		}
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x000D11F4 File Offset: 0x000CF5F4
	public override void Update()
	{
		this.m_modelCanvas.m_3DCameraOffset = -600f * ((float)Screen.height / (float)Screen.width * 2f);
		base.Update();
		this.CheckButtons();
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x000D1226 File Offset: 0x000CF626
	public void UpdateUpgradeAndCustomisationItems()
	{
		PsMetagameManager.m_playerStats.updated = true;
		if (this.m_upgradeView != null)
		{
			this.m_upgradeView.UpdateCards();
		}
		if (this.m_currentCustomisationViews != null)
		{
			this.m_currentCustomisationViews.UpdateItems();
		}
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x000D1260 File Offset: 0x000CF660
	private void LoadVehicle()
	{
		if (this.m_vehiclePrefab != null)
		{
			PrefabS.RemoveComponent(this.m_vehiclePrefab, true);
			this.m_vehiclePrefab = null;
		}
		Vector3 vector = ((this.m_vehicleType != typeof(OffroadCar)) ? new Vector3(0f, 7f, 0f) : (Vector3.right * 9f));
		this.UpdateUpgradeValues();
		this.m_vehiclePrefab = this.m_modelCanvas.AddGameObject(ResourceManager.GetGameObject(this.m_vehicleType.ToString() + "Prefab_GameObject"), vector, default(Vector3));
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
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x000D139C File Offset: 0x000CF79C
	private void CreateCharacter()
	{
		this.m_characterPrefab = this.m_modelCanvas.AddGameObject(ResourceManager.GetGameObject("AlienNewPrefab_GameObject"), Vector3.zero, default(Vector3));
		this.DisableColliderRenderers(this.m_characterPrefab.p_gameObject.transform);
		this.m_characterAnimator = this.m_characterPrefab.p_gameObject.GetComponent<Animator>();
		this.m_animatorController = ResourceManager.GetResource<RuntimeAnimatorController>(RESOURCE.AlienAnimatorController_AnimatorController);
		this.m_characterAnimator.runtimeAnimatorController = this.m_animatorController;
		this.m_characterPrefab.p_gameObject.transform.Rotate(0f, 90f, 0f);
		Transform transform;
		if (this.m_vehicleType == typeof(OffroadCar))
		{
			transform = this.m_vehiclePrefab.p_gameObject.transform.Find("OffroadCar/CharacterLocator");
			this.m_drivePoseState = Animator.StringToHash("Base.Drive");
		}
		else
		{
			transform = this.m_vehiclePrefab.p_gameObject.transform.Find("DirtBike/CharacterLocator");
			this.m_drivePoseState = Animator.StringToHash("Base.DriveMoto");
		}
		this.m_vehicleInterface = this.m_vehiclePrefab.p_gameObject.GetComponent<IVisualsVehicle>();
		this.m_vehiclePrefab.p_gameObject.transform.Find("Shadow").gameObject.SetActive(true);
		this.m_characterPrefab.p_gameObject.transform.SetParent(transform, false);
		this.m_bindPoseState = Animator.StringToHash("Base.Bind");
		this.m_standPoseState = Animator.StringToHash("Base.Stand");
		this.m_characterAnimator.Play(this.m_drivePoseState);
		this.m_alienEffects = this.m_characterPrefab.p_gameObject.GetComponent<AlienEffects>();
		if (this.m_alienEffects != null)
		{
			this.m_alienEffects.Initialize();
		}
		PsCustomisationData vehicleCustomisationData = PsCustomisationManager.GetVehicleCustomisationData(this.m_vehicleType);
		PsCustomisationItem installedItemByCategory = vehicleCustomisationData.GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		string text = string.Empty;
		if (installedItemByCategory != null)
		{
			text = installedItemByCategory.m_identifier;
		}
		this.SetPropToLocator(text);
		PsCustomisationItem installedItemByCategory2 = PsCustomisationManager.GetVehicleCustomisationData(this.m_vehicleType).GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.TRAIL);
		if (installedItemByCategory2 != null)
		{
			this.SetTrail(installedItemByCategory2.m_identifier);
		}
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x000D15C0 File Offset: 0x000CF9C0
	private void UpdateCharacter()
	{
		this.m_blinkTimer--;
		if (this.m_blinkTimer <= 0)
		{
			this.m_characterAnimator.SetTrigger("Blink");
			this.m_blinkTimer = Random.Range(30, 300);
		}
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x000D1600 File Offset: 0x000CFA00
	public void SetPropToLocator(string _hatIdentifier)
	{
		GameObject hatPrefabByIdentifier = PsCustomisationManager.GetHatPrefabByIdentifier(_hatIdentifier);
		if (this.m_hatPrefab != null)
		{
			PrefabS.RemoveComponent(this.m_hatPrefab, true);
			this.m_hatPrefab = null;
		}
		this.m_hatPrefab = this.m_modelCanvas.AddGameObject(hatPrefabByIdentifier, Vector3.zero, default(Vector3));
		if (_hatIdentifier == "ToadHat")
		{
			VisualsToadHat componentInChildren = this.m_hatPrefab.p_gameObject.GetComponentInChildren<VisualsToadHat>();
			if (componentInChildren != null)
			{
				componentInChildren.m_forceAnimations = true;
			}
		}
		Transform transform = this.m_characterPrefab.p_gameObject.transform.Find("Hips/Spine1/Spine2/Neck/Head/HeadCollider/HeadGearLocator");
		this.m_hatPrefab.p_gameObject.transform.parent = transform;
		this.m_hatPrefab.p_gameObject.transform.localPosition = Vector3.zero;
		this.m_hatPrefab.p_gameObject.transform.localRotation = Quaternion.Euler(Vector3.up * -90f);
		this.m_hatPrefab.p_gameObject.transform.localScale = Vector3.one;
		this.m_alienEffects.WobbleHead();
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x000D1720 File Offset: 0x000CFB20
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

	// Token: 0x0600147A RID: 5242 RVA: 0x000D1784 File Offset: 0x000CFB84
	public Texture GetTierTexture(string _vehicleName, int _tier)
	{
		int num = Mathf.Min(_tier - 1, 4);
		string text = string.Concat(new object[] { _vehicleName, "DifT", num, "_Texture2D" });
		return ResourceManager.GetTexture(text);
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x000D17CC File Offset: 0x000CFBCC
	private void UpdateUpgradeValues()
	{
		this.m_upgrades = new List<KeyValuePair<string, int>>();
		if (PsState.GetCurrentVehicleType(true) == typeof(OffroadCar))
		{
			this.m_upgrades.Add(new KeyValuePair<string, int>("power", 3));
			this.m_upgrades.Add(new KeyValuePair<string, int>("grip", 3));
			this.m_upgrades.Add(new KeyValuePair<string, int>("handling", 3));
		}
		else if (PsState.GetCurrentVehicleType(true) == typeof(Motorcycle))
		{
			this.m_upgrades.Add(new KeyValuePair<string, int>("power", 2));
			this.m_upgrades.Add(new KeyValuePair<string, int>("grip", 3));
			this.m_upgrades.Add(new KeyValuePair<string, int>("handling", 2));
		}
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x000D1898 File Offset: 0x000CFC98
	public void UpgradeWasPurchased()
	{
		this.m_characterAnimator.SetInteger("Random", 0);
		this.m_characterAnimator.SetTrigger("JumpCheer");
		SoundS.PlaySingleShot("/InGame/JumpCheer", Vector3.zero, 1f);
		if (this.m_vehicleInterface != null)
		{
			this.m_vehicleInterface.UpgradePop();
		}
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x000D18F0 File Offset: 0x000CFCF0
	public override void Step()
	{
		this.UpdateCharacter();
		if (!this.m_resources || PsMetagameManager.m_menuResourceView == null)
		{
			PsMetagameManager.ShowResources((this.GetRoot() as PsUIBasePopup).m_overlayCamera, true, true, true, false, 0.025f, true, false, false);
			PsMetagameManager.m_menuResourceView.m_customShopOpenAction = delegate
			{
				PsUIBaseState psUIBaseState = new PsUIBaseState(typeof(PsUICenterShopAll), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
				psUIBaseState.SetAction("Exit", delegate
				{
					PsUICenterGarage.m_createGarageAction.Invoke();
				});
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState);
			};
			this.m_resources = true;
		}
		if (this.m_customisetionViewButton.m_hit)
		{
			this.m_customisetionViewButton.m_disabled = true;
			this.m_upgradeViewButton.m_disabled = false;
			this.m_scrollableCanvas.m_changePage = 1;
			EntityManager.SetActivityOfEntitiesWithTag("VehicleStats", false, true, true, true, false, false);
			EntityManager.SetActivityOfEntitiesWithTag("CategoryButtons", true, true, true, true, false, false);
			EntityManager.SetActivityOfEntity(this.m_categoryButtonHolder.m_TC.p_entity, true, true, true, true, true, true);
			this.TweenButton(false, this.m_customisetionViewButton.m_TC, ref this.m_customisationViewButtonTween);
			this.TweenButton(true, this.m_upgradeViewButton.m_TC, ref this.m_upgradeViewButtonTween);
		}
		else if (this.m_upgradeViewButton.m_hit)
		{
			this.m_customisetionViewButton.m_disabled = false;
			this.m_upgradeViewButton.m_disabled = true;
			this.m_scrollableCanvas.m_changePage = -1;
			EntityManager.SetActivityOfEntitiesWithTag("VehicleStats", true, true, true, true, false, false);
			EntityManager.SetActivityOfEntitiesWithTag("CategoryButtons", false, true, true, true, false, false);
			EntityManager.SetActivityOfEntity(this.m_categoryButtonHolder.m_TC.p_entity, false, true, true, true, true, true);
			this.TweenButton(false, this.m_upgradeViewButton.m_TC, ref this.m_upgradeViewButtonTween);
			this.TweenButton(true, this.m_customisetionViewButton.m_TC, ref this.m_customisationViewButtonTween);
		}
		else
		{
			for (int i = 0; i < this.m_categryButtons.Count; i++)
			{
				if (this.m_categryButtons[i].m_hit)
				{
					this.SetCustomisationView((int)this.m_categryButtons[i].m_customObject);
					this.ActivateButton(this.m_categryButtons[i]);
				}
			}
		}
		this.RotateVehicle();
		base.Step();
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x000D1B20 File Offset: 0x000CFF20
	private void TweenButton(bool show, TransformC _transformC, ref TweenC _tweenC)
	{
		if (_tweenC != null)
		{
			TweenS.RemoveComponent(_tweenC);
			_tweenC = null;
		}
		if (show)
		{
			EntityManager.SetActivityOfEntity(_transformC.p_entity, true, true, true, true, true, true);
			_tweenC = TweenS.AddTransformTween(_transformC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.one, 0.25f, 0f, false);
		}
		else
		{
			_tweenC = TweenS.AddTransformTween(_transformC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.zero, 0.1f, 0f, false);
		}
		TweenS.AddTweenEndEventListener(_tweenC, delegate(TweenC _c)
		{
			if (_c != null)
			{
				if (!show)
				{
					EntityManager.SetActivityOfEntity(_c.p_entity, false, true, true, true, true, true);
				}
				TweenS.RemoveComponent(_c);
				if (_c == this.m_customisationViewButtonTween)
				{
					this.m_customisationViewButtonTween = null;
				}
				else if (_c == this.m_upgradeViewButtonTween)
				{
					this.m_upgradeViewButtonTween = null;
				}
				_c = null;
			}
		});
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x000D1BBC File Offset: 0x000CFFBC
	private void CheckButtons()
	{
		if (this.m_scrollableCanvas.m_currentPageX == 0)
		{
			TransformS.SetScale(this.m_upgradeViewButton.m_TC, Vector3.zero);
			EntityManager.SetActivityOfEntity(this.m_upgradeViewButton.m_TC.p_entity, false, true, true, true, true, true);
			EntityManager.SetActivityOfEntity(this.m_categoryButtonHolder.m_TC.p_entity, false, true, true, true, true, true);
			EntityManager.SetActivityOfEntitiesWithTag("VehicleStats", true, true, true, true, false, false);
		}
		else if (this.m_scrollableCanvas.m_currentPageX == 1)
		{
			TransformS.SetScale(this.m_customisetionViewButton.m_TC, Vector3.zero);
			EntityManager.SetActivityOfEntity(this.m_customisetionViewButton.m_TC.p_entity, false, true, true, true, true, true);
			EntityManager.SetActivityOfEntity(this.m_categoryButtonHolder.m_TC.p_entity, true, true, true, true, true, true);
			EntityManager.SetActivityOfEntitiesWithTag("VehicleStats", false, true, true, true, false, false);
		}
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x000D1CA8 File Offset: 0x000D00A8
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

	// Token: 0x06001481 RID: 5249 RVA: 0x000D1D44 File Offset: 0x000D0144
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

	// Token: 0x06001482 RID: 5250 RVA: 0x000D1DD8 File Offset: 0x000D01D8
	public override void Destroy()
	{
		if (this.m_previewRotationTween != null)
		{
			TweenS.RemoveComponent(this.m_previewRotationTween);
			this.m_previewRotationTween = null;
		}
		if (this.m_hatPrefab != null)
		{
			PrefabS.RemoveComponent(this.m_hatPrefab, true);
			this.m_hatPrefab = null;
		}
		if (this.m_trailBase != null)
		{
			this.m_trailBase.Destroy();
			this.m_trailBase = null;
			Object.Destroy(this.m_trail);
			this.m_trail = null;
		}
		if (this.m_vehiclePrefab != null)
		{
			PrefabS.RemoveComponent(this.m_vehiclePrefab, true);
			this.m_vehiclePrefab = null;
		}
		if (this.m_basePrefab != null)
		{
			PrefabS.RemoveComponent(this.m_basePrefab, true);
			this.m_basePrefab = null;
		}
		PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), new Hashtable(), PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().GetIdentifier(), false);
		PsMetrics.GarageExited(this.m_vehicleType);
		if (this.m_vehicleType == typeof(Motorcycle))
		{
			FrbMetrics.TrackMotorcycleCc();
		}
		else if (this.m_vehicleType == typeof(OffroadCar))
		{
			FrbMetrics.TrackOffroadCc();
		}
		base.Destroy();
	}

	// Token: 0x0400172D RID: 5933
	public const int UPGRADE_VIEW = -1;

	// Token: 0x0400172E RID: 5934
	public const int HAT_VIEW = 0;

	// Token: 0x0400172F RID: 5935
	public const int TRAIL_VIEW = 1;

	// Token: 0x04001730 RID: 5936
	public static Action m_createGarageAction;

	// Token: 0x04001731 RID: 5937
	private UIRectSpriteButton m_customisetionViewButton;

	// Token: 0x04001732 RID: 5938
	private UIRectSpriteButton m_upgradeViewButton;

	// Token: 0x04001733 RID: 5939
	private UIText m_title;

	// Token: 0x04001734 RID: 5940
	private UI3DRenderTextureCanvas m_modelCanvas;

	// Token: 0x04001735 RID: 5941
	private PrefabC m_vehiclePrefab;

	// Token: 0x04001736 RID: 5942
	private PrefabC m_basePrefab;

	// Token: 0x04001737 RID: 5943
	private UIHorizontalList m_scrollableCanvasContent;

	// Token: 0x04001738 RID: 5944
	private PsUIUpgradeView m_upgradeView;

	// Token: 0x04001739 RID: 5945
	public PsUIVehicleStats2 m_vehicleStats;

	// Token: 0x0400173A RID: 5946
	private UICanvas m_customisationViewHolder;

	// Token: 0x0400173B RID: 5947
	private PsUICurtomisationView m_currentCustomisationViews;

	// Token: 0x0400173C RID: 5948
	private List<PsUIGenericButton> m_categryButtons;

	// Token: 0x0400173D RID: 5949
	private UIHorizontalList m_categoryButtonHolder;

	// Token: 0x0400173E RID: 5950
	private bool m_modelLoaded;

	// Token: 0x0400173F RID: 5951
	private List<KeyValuePair<string, int>> m_upgrades;

	// Token: 0x04001740 RID: 5952
	private UIVerticalList m_locked;

	// Token: 0x04001741 RID: 5953
	private Type m_vehicleType;

	// Token: 0x04001742 RID: 5954
	private bool m_resources;

	// Token: 0x04001743 RID: 5955
	private UIScrollableSnappingCanvas m_scrollableCanvas;

	// Token: 0x04001744 RID: 5956
	public Action m_createGarage;

	// Token: 0x04001745 RID: 5957
	private UICanvas m_customisationNotificationBase;

	// Token: 0x04001746 RID: 5958
	private static bool m_fixedVehicle;

	// Token: 0x04001747 RID: 5959
	private static Type m_selectVehicle;

	// Token: 0x04001748 RID: 5960
	public static int m_startView = -1;

	// Token: 0x04001749 RID: 5961
	private PsUIGenericButton m_activeButton;

	// Token: 0x0400174A RID: 5962
	private GameObject m_trail;

	// Token: 0x0400174B RID: 5963
	private PsTrailBase m_trailBase;

	// Token: 0x0400174C RID: 5964
	private string m_currentVisualTrail;

	// Token: 0x0400174D RID: 5965
	private float m_rotateSpeed;

	// Token: 0x0400174E RID: 5966
	private bool m_frozenPreviewRotation;

	// Token: 0x0400174F RID: 5967
	private TweenC m_previewRotationTween;

	// Token: 0x04001750 RID: 5968
	private PrefabC m_characterPrefab;

	// Token: 0x04001751 RID: 5969
	private Animator m_characterAnimator;

	// Token: 0x04001752 RID: 5970
	private IVisualsVehicle m_vehicleInterface;

	// Token: 0x04001753 RID: 5971
	private RuntimeAnimatorController m_animatorController;

	// Token: 0x04001754 RID: 5972
	private int m_bindPoseState;

	// Token: 0x04001755 RID: 5973
	private int m_standPoseState;

	// Token: 0x04001756 RID: 5974
	private int m_drivePoseState;

	// Token: 0x04001757 RID: 5975
	private AlienEffects m_alienEffects;

	// Token: 0x04001758 RID: 5976
	private PrefabC m_hatPrefab;

	// Token: 0x04001759 RID: 5977
	private int m_blinkTimer = 60;

	// Token: 0x0400175A RID: 5978
	private TweenC m_customisationViewButtonTween;

	// Token: 0x0400175B RID: 5979
	private TweenC m_upgradeViewButtonTween;
}
