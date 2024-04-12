using System;
using UnityEngine;

// Token: 0x0200038E RID: 910
public class UIEditorDrawMenu : UIHorizontalList
{
	// Token: 0x06001A34 RID: 6708 RVA: 0x00123BFC File Offset: 0x00121FFC
	public UIEditorDrawMenu(UIComponent _parent, string _tag)
		: base(_parent, _tag)
	{
		this.SetAlign(0f, 0f);
		this.SetMargins(0.02f, RelativeTo.ScreenShortest);
		this.SetSpacing(0.02f, RelativeTo.ScreenShortest);
		this.RemoveDrawHandler();
		if (PsState.m_editorIsLefty)
		{
			PsState.m_drawButtonWindowPosition = 0.975f;
			PsState.m_drawMenuAlign = 1f;
			PsState.m_objectMenuButtonAlign = 0f;
		}
		else
		{
			PsState.m_drawButtonWindowPosition = 0.025f;
			PsState.m_drawMenuAlign = 0f;
			PsState.m_objectMenuButtonAlign = 1f;
		}
		this.ApplyLeftySettings();
		PsState.m_activeMinigame.InitializeDrawMaterials();
		this.m_currentPage = PsState.m_activeMinigame.GetMatIndexFromLayerIndex(PsState.m_drawLayer);
		this.m_forcePage = true;
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x00123CC4 File Offset: 0x001220C4
	public void ApplyLeftySettings()
	{
		if (this.m_drawWindow != null)
		{
			this.m_currentPage = PsState.m_activeMinigame.GetMatIndexFromLayerIndex(PsState.m_drawLayer);
			this.m_forcePage = true;
			if (PsState.m_editorIsLefty)
			{
				this.OpenDrawWindow(true);
			}
			else
			{
				this.OpenDrawWindow(false);
			}
			this.m_drawWindow.SetHorizontalAlign(PsState.m_drawButtonWindowPosition);
			this.m_drawWindow.Update();
		}
		this.SetHorizontalAlign(PsState.m_drawMenuAlign);
		this.Update();
	}

	// Token: 0x06001A36 RID: 6710 RVA: 0x00123D44 File Offset: 0x00122144
	public void OpenDrawWindow(bool _lefty)
	{
		if (this.m_drawWindow != null)
		{
			this.m_drawWindow.Destroy();
		}
		bool flag = true;
		this.m_drawWindow = new UIEditorDrawButtonsWindow(null, "DrawWindow");
		if (flag)
		{
			float num = (float)Screen.height * 0.25f;
			float num2 = (float)Screen.height * 0.125f;
			float num3 = (float)Screen.height * 0.205f;
			float num4 = (float)Screen.height * 0.175f;
			float num5 = (float)Screen.height * 0.205f;
			float num6 = (float)Screen.height * 0.3f;
			if (this.m_rollEntity != null)
			{
				EntityManager.RemoveEntity(this.m_rollEntity);
			}
			Vector3 vector;
			vector..ctor((float)Screen.width * -0.5f, (float)Screen.height * -0.5f);
			float num7 = 0f;
			int num8 = 1;
			if (_lefty)
			{
				vector..ctor((float)Screen.width * 0.5f, (float)Screen.height * -0.5f);
				num7 = 90f;
				num8 = -1;
			}
			this.m_itemCount = 5;
			this.m_minAngle = (float)(-45 * (this.m_itemCount - 1));
			this.m_maxAngle = 0f;
			this.m_rollEntity = EntityManager.AddEntity(new string[] { "DrawMenu", "UIComponent" });
			TransformC transformC = TransformS.AddComponent(this.m_rollEntity, vector);
			transformC.forceRotation = true;
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("hud_wheel_quarter", null);
			frame.flipX = _lefty;
			SpriteC spriteC = SpriteS.AddComponent(transformC, frame, PsState.m_uiSheet);
			SpriteS.SetSortValue(spriteC, 10f);
			SpriteS.SetOffset(spriteC, new Vector2(num * 0.5f * (float)num8, num * 0.5f), 0f);
			SpriteS.SetDimensions(spriteC, num, num);
			TouchAreaC touchAreaC = TouchAreaS.AddCircleArea(transformC, "roll", num6, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(touchAreaC, new TouchEventDelegate(this.RollTouchHandler));
			bool flag2 = false;
			for (int i = 0; i < this.m_itemCount; i++)
			{
				PsUnlockable psUnlockable = PsMetagameData.m_gameMaterials[0].m_items[i];
				for (int j = 0; j < PsState.m_newEditorItems.Length; j++)
				{
					if (PsState.m_newEditorItems[j] == psUnlockable.m_identifier)
					{
						flag2 = true;
						break;
					}
				}
				if (flag2)
				{
					break;
				}
			}
			if (flag2)
			{
				this.m_masterNotification = EntityManager.AddEntity("UIComponent");
				TransformC transformC2 = TransformS.AddComponent(this.m_masterNotification, "MASTERNOTIFICATION");
				TransformS.ParentComponent(transformC2, transformC, new Vector3((!_lefty) ? (0.05f * (float)Screen.height) : (-0.05f * (float)Screen.height), 0.05f * (float)Screen.height, -25f));
				Vector2[] circle = DebugDraw.GetCircle(0.03f * (float)Screen.height, 30, Vector2.zero, true);
				PrefabS.CreatePathPrefabComponentFromVectorArray(transformC2, Vector3.forward * -1f, circle, (float)Screen.height * 0.012f, Color.white, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), CameraS.m_uiCamera, Position.Center, true);
				GGData ggdata = new GGData(circle);
				Color color = DebugDraw.HexToColor("#cc0909");
				Color color2 = DebugDraw.HexToColor("#FF0C0C");
				PrefabS.CreateFlatPrefabComponentsFromPolygon(transformC2, Vector3.zero, ggdata, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), CameraS.m_uiCamera);
				TextMeshC textMeshC = TextMeshS.AddComponent(transformC2, Vector3.forward * -2f, PsFontManager.GetFont(PsFonts.HurmeRegular), 0f, 0f, 20f, Align.Center, Align.Middle, CameraS.m_uiCamera, string.Empty);
				TextMeshS.SetText(textMeshC, "!", true);
				TweenC tweenC = TweenS.AddTransformTween(transformC2, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.15f, 1.15f, 1.15f), 0.5f, 0f, false);
				TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
			}
			this.m_rollTC = TransformS.AddComponent(this.m_rollEntity);
			TransformS.SetRotation(this.m_rollTC, Vector3.forward * (45f + num7 + this.m_currentScroll));
			TransformS.ParentComponent(this.m_rollTC, transformC, Vector3.zero);
			TransformC transformC3 = TransformS.AddComponent(this.m_rollEntity);
			TransformS.ParentComponent(transformC3, transformC, new Vector3(Mathf.Cos((45f + num7) * 0.017453292f), Mathf.Sin((45f + num7) * 0.017453292f)) * num5);
			Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("hud_wheel_selector", null);
			frame2.flipX = _lefty;
			SpriteC spriteC2 = SpriteS.AddComponent(transformC3, frame2, PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC2, num4, num4);
			SpriteS.SetSortValue(spriteC2, 5f);
			this.m_itemSC = new SpriteC[this.m_itemCount];
			this.m_notifications = new Entity[this.m_itemCount];
			float num9 = 0f;
			for (int k = 0; k < this.m_itemCount; k++)
			{
				TransformC transformC4 = TransformS.AddComponent(this.m_rollEntity);
				transformC4.forceRotation = true;
				TransformS.ParentComponent(transformC4, this.m_rollTC, new Vector3(Mathf.Cos(num9 * 0.017453292f), Mathf.Sin(num9 * 0.017453292f)) * num3);
				string editorWheelFrameName = AutoGeometryManager.m_layers[PsState.m_activeMinigame.m_layerIndexTable[k]].m_groundC.m_ground.m_editorWheelFrameName;
				Frame frame3 = PsState.m_uiSheet.m_atlas.GetFrame(editorWheelFrameName, null);
				if (PsState.m_editorIsLefty)
				{
					frame3.flipX = true;
				}
				this.m_itemSC[k] = SpriteS.AddComponent(transformC4, frame3, PsState.m_uiSheet);
				SpriteS.SetDimensions(this.m_itemSC[k], num2 * 1.2f, num2);
				if (!PsState.m_activeGameLoop.m_gameMode.isMaterialAvailable(k))
				{
					TextMeshC textMeshC2 = TextMeshS.AddComponent(transformC4, Vector3.zero, PsFontManager.GetFont(PsFonts.HurmeRegular), 0f, 0f, 20f, Align.Center, Align.Middle, CameraS.m_uiCamera, string.Empty);
					TextMeshS.SetText(textMeshC2, "Locked", true);
				}
				if (PsState.m_newEditorItemCount > 0)
				{
					PsUnlockable psUnlockable2 = PsMetagameData.m_gameMaterials[0].m_items[k];
					bool flag3 = false;
					for (int l = 0; l < PsState.m_newEditorItems.Length; l++)
					{
						if (PsState.m_newEditorItems[l] == psUnlockable2.m_identifier)
						{
							flag3 = true;
							break;
						}
					}
					if (flag3)
					{
						this.m_notifications[k] = EntityManager.AddEntity("UIComponent");
						TransformC transformC5 = TransformS.AddComponent(this.m_notifications[k], "NOTIFICATION");
						transformC5.forceRotation = true;
						TransformS.ParentComponent(transformC5, transformC4, new Vector3(-num2 * 0.5f, num2 * 0.4f, -25f));
						Vector2[] circle2 = DebugDraw.GetCircle(0.02f * (float)Screen.height, 30, Vector2.zero, true);
						PrefabS.CreatePathPrefabComponentFromVectorArray(transformC5, Vector3.forward * -1f, circle2, (float)Screen.height * 0.0075f, Color.white, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), CameraS.m_uiCamera, Position.Center, true);
						GGData ggdata2 = new GGData(circle2);
						Color color3 = DebugDraw.HexToColor("#cc0909");
						Color color4 = DebugDraw.HexToColor("#FF0C0C");
						PrefabS.CreateFlatPrefabComponentsFromPolygon(transformC5, Vector3.zero, ggdata2, color3, color4, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), CameraS.m_uiCamera);
						TextMeshC textMeshC3 = TextMeshS.AddComponent(transformC5, Vector3.forward * -2f, PsFontManager.GetFont(PsFonts.HurmeRegular), 0f, 0f, 20f, Align.Center, Align.Middle, CameraS.m_uiCamera, string.Empty);
						TextMeshS.SetText(textMeshC3, "!", true);
						TweenC tweenC2 = TweenS.AddTransformTween(transformC5, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.15f, 1.15f, 1.15f), 0.5f, 0f, false);
						TweenS.SetAdditionalTweenProperties(tweenC2, -1, true, TweenStyle.CubicInOut);
					}
				}
				num9 -= 45f;
			}
			if (this.m_forcePage)
			{
				this.m_currentScroll = (float)this.m_currentPage * 45f;
				TransformS.Rotate(this.m_rollTC, Vector3.forward * this.m_currentScroll);
				PsState.m_drawMenuTargetPage = this.m_currentPage;
				this.m_forcePage = false;
			}
			this.Update();
			this.UpdateMaterialSpriteColors(this.m_currentPage);
		}
		this.SwitchIconAndHighlight(false);
		this.m_drawWindow.RegenerateDrawButtonBackgrounds();
		bool flag4 = PsState.m_activeGameLoop.m_gameMode.isMaterialAvailable(PsState.m_activeMinigame.GetMatIndexFromLayerIndex(PsState.m_drawLayer));
		if (flag4)
		{
			this.m_drawWindow.SetDrawButtonsActivity(true);
		}
		else
		{
			this.m_drawWindow.SetDrawButtonsActivity(false);
		}
	}

	// Token: 0x06001A37 RID: 6711 RVA: 0x001245F4 File Offset: 0x001229F4
	private void UpdateMaterialSpriteColors(int _selected)
	{
		float num = 0.35f;
		float num2 = 0.5f;
		Color color;
		color..ctor(num, num, num);
		Color color2;
		color2..ctor(num2, num2, num2);
		for (int i = 0; i < this.m_itemCount; i++)
		{
			if (i != _selected)
			{
				SpriteS.SetColor(this.m_itemSC[i], color);
			}
			else
			{
				SpriteS.SetColor(this.m_itemSC[i], color2);
			}
		}
	}

	// Token: 0x06001A38 RID: 6712 RVA: 0x00124668 File Offset: 0x00122A68
	private void RollTouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		Vector2 vector = _touches[0].m_currentPosition;
		if (PsState.m_editorIsLefty)
		{
			vector -= new Vector2((float)Screen.width, 0f);
		}
		if (_touchPhase == TouchAreaPhase.Began)
		{
			this.m_flinkCanChangePage = true;
			this.m_touching = true;
			this.m_rollAngle = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			this.m_prevRollAngle = this.m_rollStartAngle;
		}
		else if (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut)
		{
			this.m_rollAngle = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			this.m_rollInertia = this.m_rollAngle - this.m_prevRollAngle;
		}
		else if (_touchPhase == TouchAreaPhase.ReleaseIn || _touchPhase == TouchAreaPhase.ReleaseOut)
		{
			this.m_rollAngle = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			float num = (this.m_rollAngle - this.m_prevRollAngle + this.m_lastFrameRollInertia) / 2f;
			float num2 = (this.m_lastFrameTouchMagnitude + _touches[0].m_deltaPosition.magnitude) / 2f;
			if (this.m_flinkCanChangePage && num2 > (float)Screen.width * Main.m_gameDeltaTime * 0.4f)
			{
				if (num > 150f * Main.m_gameDeltaTime)
				{
					this.ChangePage(this.m_currentPage + 1);
				}
				else if (num < -150f * Main.m_gameDeltaTime)
				{
					this.ChangePage(this.m_currentPage - 1);
				}
			}
			this.m_touching = false;
			this.m_rollAngle = 0f;
		}
		this.m_lastFrameRollInertia = this.m_rollInertia;
		this.m_lastFrameTouchMagnitude = _touches[0].m_deltaPosition.magnitude;
	}

	// Token: 0x06001A39 RID: 6713 RVA: 0x00124828 File Offset: 0x00122C28
	public override void Step()
	{
		if (this.m_touching)
		{
			int num = Mathf.RoundToInt(this.m_currentScroll / 45f);
			num = Mathf.Clamp(num, 0, this.m_itemCount - 1);
			if (num != this.m_currentPage)
			{
				this.m_flinkCanChangePage = false;
				this.ChangePage(num);
			}
		}
		else
		{
			if (PsState.m_drawMenuTargetPage != this.m_currentPage)
			{
				this.ChangePage(PsState.m_drawMenuTargetPage);
			}
			else if (Input.GetKeyDown(275))
			{
				this.ChangePage(this.m_currentPage + 1);
			}
			else if (Input.GetKeyDown(276))
			{
				this.ChangePage(this.m_currentPage - 1);
			}
			float num2 = (float)this.m_currentPage * 45f;
			if (this.m_currentScroll != num2)
			{
				this.m_rollInertia = Mathf.Lerp(this.m_currentScroll, num2, 0.1f) - this.m_currentScroll;
			}
		}
		if (Mathf.Abs(this.m_rollInertia) > 0f)
		{
			float num3 = -45f;
			float num4 = 45f * (float)this.m_itemCount;
			float num5 = this.m_currentScroll + this.m_rollInertia;
			if (num5 < num3)
			{
				this.m_rollInertia = num3 - this.m_currentScroll;
				num5 = num3;
			}
			else if (num5 > num4)
			{
				this.m_rollInertia = num4 - this.m_currentScroll;
				num5 = num4;
			}
			this.m_currentScroll = num5;
			TransformS.Rotate(this.m_rollTC, Vector3.forward * this.m_rollInertia);
			this.m_rollInertia = 0f;
		}
		this.m_prevRollAngle = this.m_rollAngle;
		base.Step();
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x001249C8 File Offset: 0x00122DC8
	private void ChangePage(int _page)
	{
		this.m_currentPage = Mathf.Clamp(_page, 0, this.m_itemCount - 1);
		PsState.m_drawMenuTargetPage = this.m_currentPage;
		if (this.m_notifications[this.m_currentPage] != null)
		{
			PsMetagameManager.RemoveNewEditorItem(PsMetagameData.m_gameMaterials[0].m_items[this.m_currentPage].m_identifier);
			EntityManager.RemoveEntity(this.m_notifications[this.m_currentPage], true, true);
			this.m_notifications[this.m_currentPage] = null;
			bool flag = false;
			for (int i = 0; i < this.m_notifications.Length; i++)
			{
				if (this.m_notifications[i] != null)
				{
					flag = true;
					break;
				}
			}
			if (!flag && this.m_masterNotification != null)
			{
				EntityManager.RemoveEntity(this.m_masterNotification, true, true);
				this.m_masterNotification = null;
			}
		}
		if (PsState.m_activeMinigame != null)
		{
			PsState.m_drawLayer = PsState.m_activeMinigame.m_layerIndexTable[this.m_currentPage];
		}
		this.UpdateMaterialSpriteColors(this.m_currentPage);
		this.SwitchIconAndHighlight(true);
		if (PsState.m_activeGameLoop.m_gameMode.isMaterialAvailable(this.m_currentPage))
		{
			this.m_drawWindow.SetDrawButtonsActivity(true);
			this.m_drawWindow.RegenerateDrawButtonBackgrounds();
		}
		else
		{
			this.m_drawWindow.SetDrawButtonsActivity(false);
		}
		AutoGeometryLayer autoGeometryLayer = AutoGeometryManager.m_layers[PsState.m_drawLayer];
		AutoGeometryManager.UpdateMaxValueLookupTable(autoGeometryLayer, LookupUpdateMode.IN_EDITOR, false);
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x00124B2F File Offset: 0x00122F2F
	public void UpdateDrawButtonActivity()
	{
		if (PsState.m_activeGameLoop.m_gameMode.isMaterialAvailable(this.m_currentPage) && !this.m_drawWindow.m_buttonsActive)
		{
			this.m_drawWindow.SetDrawButtonsActivity(true);
		}
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x00124B68 File Offset: 0x00122F68
	public void CloseDrawWindow()
	{
		if (this.m_rollEntity != null)
		{
			EntityManager.RemoveEntity(this.m_rollEntity, true, true);
		}
		for (int i = 0; i < this.m_notifications.Length; i++)
		{
			if (this.m_notifications[i] != null)
			{
				EntityManager.RemoveEntity(this.m_notifications[i], true, true);
				this.m_notifications[i] = null;
			}
		}
		if (this.m_masterNotification != null)
		{
			EntityManager.RemoveEntity(this.m_masterNotification, true, true);
			this.m_masterNotification = null;
		}
		if (this.m_drawWindow != null)
		{
			this.m_drawWindow.Destroy();
			this.m_drawWindow = null;
		}
		if (AutoGeometryManager.m_layers.Count > PsState.m_drawLayer)
		{
			AutoGeometryLayer autoGeometryLayer = AutoGeometryManager.m_layers[PsState.m_drawLayer];
			AutoGeometryManager.HighlightLayer(autoGeometryLayer, 0f, 0.1f);
		}
	}

	// Token: 0x06001A3D RID: 6717 RVA: 0x00124C3C File Offset: 0x0012303C
	public void SwitchIconAndHighlight(bool _playSound = true)
	{
		if (_playSound)
		{
			SoundS.PlaySingleShot("/UI/ButtonSwitchMaterial", Vector3.zero, 1f);
		}
		AutoGeometryLayer autoGeometryLayer = AutoGeometryManager.m_layers[PsState.m_drawLayer];
		AutoGeometryManager.HighlightLayer(autoGeometryLayer, 0.3f, 0.1f);
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x00124C83 File Offset: 0x00123083
	public override void Destroy()
	{
		if (this.m_drawWindow != null)
		{
			this.CloseDrawWindow();
		}
		base.Destroy();
	}

	// Token: 0x04001CAD RID: 7341
	public UIEditorDrawButtonsWindow m_drawWindow;

	// Token: 0x04001CAE RID: 7342
	public UIFittedSprite m_materialButton;

	// Token: 0x04001CAF RID: 7343
	private Entity m_rollEntity;

	// Token: 0x04001CB0 RID: 7344
	private TransformC m_rollTC;

	// Token: 0x04001CB1 RID: 7345
	private float m_prevRollAngle;

	// Token: 0x04001CB2 RID: 7346
	private float m_rollAngle;

	// Token: 0x04001CB3 RID: 7347
	private float m_rollInertia;

	// Token: 0x04001CB4 RID: 7348
	public float m_currentScroll;

	// Token: 0x04001CB5 RID: 7349
	private SpriteC[] m_itemSC;

	// Token: 0x04001CB6 RID: 7350
	private bool m_touching;

	// Token: 0x04001CB7 RID: 7351
	private float m_rollSpeedMultiplier = 1f;

	// Token: 0x04001CB8 RID: 7352
	private int m_itemCount;

	// Token: 0x04001CB9 RID: 7353
	private int m_currentPage;

	// Token: 0x04001CBA RID: 7354
	private float m_rollStartAngle;

	// Token: 0x04001CBB RID: 7355
	private float m_minAngle;

	// Token: 0x04001CBC RID: 7356
	private float m_maxAngle;

	// Token: 0x04001CBD RID: 7357
	private bool m_forcePage;

	// Token: 0x04001CBE RID: 7358
	private float m_lastFrameTouchMagnitude;

	// Token: 0x04001CBF RID: 7359
	private float m_lastFrameRollInertia;

	// Token: 0x04001CC0 RID: 7360
	private Entity[] m_notifications;

	// Token: 0x04001CC1 RID: 7361
	private Entity m_masterNotification;

	// Token: 0x04001CC2 RID: 7362
	private bool m_flinkCanChangePage;
}
