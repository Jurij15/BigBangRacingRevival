using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x020001DE RID: 478
public class EditorBaseState : BasicState
{
	// Token: 0x06000E42 RID: 3650 RVA: 0x000843CC File Offset: 0x000827CC
	public override void Enter(IStatedObject _parent)
	{
		if (EditorBaseState.m_enteredEditor)
		{
			PsMetrics.LevelEditorEntered();
			FrbMetrics.SetCurrentScreen("editor");
			EditorBaseState.m_enteredEditor = false;
		}
		AutoGeometryManager.SetDirtyTileTracking(false);
		this.m_startZoom = Vector2.zero;
		this.m_startDistance = 0f;
		this.m_pinching = false;
		this.m_lastBrushGroundIndex = -1;
		Entity entity = EntityManager.AddEntity();
		TransformC transformC = TransformS.AddComponent(entity, "Editor Camera Target");
		PsState.m_editorCamTarget = CameraS.AddTargetComponent(transformC, 1200f, 1200f, Vector2.zero);
		PsState.m_editorCamTarget.interpolateSpeed = 0.3333f;
		PsState.m_editorCamTarget.targetTC.transform.position = PsState.m_editorCameraPos;
		this.SetZoomAmount(PsState.m_editorCameraZoom);
		entity = EntityManager.AddEntity();
		transformC = TransformS.AddComponent(entity, Vector3.forward * 200f);
		transformC.transform.name = "EditorCanvasTransform";
		float num = (float)Mathf.Max(Screen.width, Screen.height);
		this.m_fullscreenTAC = TouchAreaS.AddRectArea(transformC, "first", num, num, CameraS.m_uiCamera, null, default(Vector2));
		TouchAreaS.AddTouchEventListener(this.m_fullscreenTAC, new TouchEventDelegate(this.TouchHandler));
		this.m_fullscreenTAC.m_allowSecondary = false;
		this.m_fullscreenTAC.m_maxTouches = 2;
		this.m_fullscreenTAC.m_cancelOtherTouches = false;
		this.m_everyplayPauseTicks = 0;
		this.m_recording = true;
		if (PsState.m_adminMode)
		{
		}
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x00084534 File Offset: 0x00082934
	private void OverrideLevelData()
	{
		Debug.LogWarning("OVERRIDING LEVEL DATA");
		string id = PsState.m_activeGameLoop.m_minigameMetaData.id;
		if (string.IsNullOrEmpty(id))
		{
			Debug.LogWarning("OVERRIDE CANCELLED - level not saved before");
		}
		else
		{
			PsState.m_activeMinigame.SetLayerItems();
			byte[] array = ClientTools.CreateLevelZipWithoutMetadataAndScreenshot();
			if (array != null)
			{
				MiniGame.OverrideData(id, array, new Action<HttpC>(this.OverrideDataSUCCEED), new Action<HttpC>(this.OverrideDataFAILED), null);
			}
		}
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x000845AC File Offset: 0x000829AC
	private void OverrideDataSUCCEED(HttpC _c)
	{
		Debug.LogWarning("OVERRIDE DATA SUCCEED");
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x000845B8 File Offset: 0x000829B8
	private void OverrideDataFAILED(HttpC _c)
	{
		Debug.LogWarning("OVERRIDE DATA FAILED");
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x000845C4 File Offset: 0x000829C4
	public override void Execute()
	{
		if (!this.m_recording)
		{
			this.m_everyplayPauseTicks++;
			EveryplayManager.SetPaused(true);
			if (this.m_everyplayPauseTicks >= 3)
			{
				this.m_recording = true;
				this.m_everyplayPauseTicks = 0;
			}
		}
		else
		{
			EveryplayManager.SetPaused(false);
			this.m_recording = false;
			this.m_everyplayPauseTicks = 0;
		}
		if (PsState.m_currentTool == EditorTool.Camera && this.m_fullscreenTAC.m_touchCount == 0)
		{
			PsState.m_currentTool = EditorTool.None;
		}
		if (Input.GetKey(97) || Input.mouseScrollDelta.y > 0f)
		{
			this.SetZoomAmount(PsState.m_editorCameraZoom - 25f);
		}
		else if (Input.GetKey(122) || Input.mouseScrollDelta.y < 0f)
		{
			this.SetZoomAmount(PsState.m_editorCameraZoom + 25f);
		}
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x000846B1 File Offset: 0x00082AB1
	private void BackToMenu()
	{
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorBaseState());
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x000846CC File Offset: 0x00082ACC
	public static void CreateTransformGizmo(GraphElement _graphElement)
	{
		GizmoManager.ClearSelection();
		GizmoManager.AddToSelection(_graphElement);
		_graphElement.Select(true);
		GizmoManager.Update();
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x000846E5 File Offset: 0x00082AE5
	public static void RemoveTransformGizmo()
	{
		GizmoManager.ClearSelection();
		GizmoManager.Update();
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x000846F4 File Offset: 0x00082AF4
	private Vector3 CalcGroundBoundsForPos(Vector3 _campos)
	{
		float num = Mathf.Abs(PsState.m_editorCamTarget.targetFrame.b - PsState.m_editorCamTarget.targetFrame.t);
		_campos.y = Mathf.Max(_campos.y, (float)(-(float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight) * 0.5f + Mathf.Max(750f, num) * 0.2f);
		return _campos;
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x00084764 File Offset: 0x00082B64
	public void SetZoomAmount(float _amount)
	{
		float num = 200f * this.m_zoomLimitMultiplier;
		float num2 = 1500f * this.m_zoomLimitMultiplier;
		PsState.m_editorCameraZoom = ToolBox.limitBetween(_amount, num, num2);
		this.m_currentZoomPos = ToolBox.getPositionBetween(_amount, num, num2);
		CameraS.SetTargetBB(PsState.m_editorCamTarget, PsState.m_editorCameraZoom * 2f, PsState.m_editorCameraZoom * 2f);
		PsState.m_editorCamTarget.targetTC.transform.position = this.CalcGroundBoundsForPos(PsState.m_editorCamTarget.targetTC.transform.position);
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x000847F4 File Offset: 0x00082BF4
	private void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchIsSecondary)
		{
			return;
		}
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		AutoGeometryLayer autoGeometryLayer = minigame.m_groundNode.m_AGLayer[PsState.m_drawLayer];
		if (PsState.m_currentTool == EditorTool.Camera)
		{
			if (_touchCount == 1)
			{
				if (_touchArea.m_wasDragged)
				{
					Vector3 vector = PsState.m_editorCamTarget.targetTC.transform.position;
					vector -= _touches[0].m_deltaPosition / CameraS.m_mainCameraDistanceMultipler;
					vector = BasicGizmo.LimitInsideDome(vector);
					vector = this.CalcGroundBoundsForPos(vector);
					PsState.m_editorCamTarget.targetTC.transform.position = vector;
					PsState.m_editorCameraPos = vector;
				}
				if (_touchPhase == TouchAreaPhase.ReleaseIn)
				{
					PsState.m_currentTool = EditorTool.None;
					this.m_pinching = false;
				}
			}
			else if (_touchCount == 2)
			{
				if (_touchIsSecondary)
				{
					return;
				}
				Vector3 vector2 = (_touches[0].m_deltaPosition + _touches[1].m_deltaPosition) * 0.5f;
				Vector2 vector3 = _touches[0].m_currentPosition - _touches[1].m_currentPosition;
				if (_touches[0].m_primaryPhase == TouchAreaPhase.Began || _touches[1].m_primaryPhase == TouchAreaPhase.Began)
				{
					cpBB targetFrame = PsState.m_editorCamTarget.targetFrame;
					this.m_startZoom = new Vector2(targetFrame.r - targetFrame.l, targetFrame.t - targetFrame.b);
					this.m_startDistance = (_touches[0].m_currentPosition - _touches[1].m_currentPosition).magnitude;
					this.m_startDistanceMultipler = CameraS.m_mainCameraDistanceMultipler;
					this.m_pinching = true;
				}
				if (this.m_pinching)
				{
					float num = (vector3.magnitude - this.m_startDistance) * 2f / this.m_startDistanceMultipler;
					float num2 = (this.m_startZoom.x - num) * 0.5f;
					this.SetZoomAmount(num2);
				}
				if (_touchArea.m_wasDragged)
				{
					Vector3 vector4 = PsState.m_editorCamTarget.targetTC.transform.position;
					vector4 -= vector2 / CameraS.m_mainCameraDistanceMultipler;
					vector4.x = Mathf.Min((float)LevelManager.m_currentLevel.m_currentLayer.m_layerWidth * 0.5f, Mathf.Max((float)LevelManager.m_currentLevel.m_currentLayer.m_layerWidth * -0.5f, vector4.x));
					vector4.y = Mathf.Min((float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * 0.5f, Mathf.Max((float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * -0.5f, vector4.y));
					PsState.m_editorCamTarget.targetTC.transform.position = vector4;
					PsState.m_editorCameraPos = vector4;
				}
				if (_touchPhase == TouchAreaPhase.ReleaseIn)
				{
					PsState.m_currentTool = EditorTool.None;
					this.m_pinching = false;
				}
			}
			else
			{
				PsState.m_currentTool = EditorTool.None;
			}
		}
		else if (PsState.m_currentTool == EditorTool.Paint)
		{
			int num3 = 0;
			for (int i = 0; i < _touchCount; i++)
			{
				if (_touches[i].m_type == 2)
				{
					num3 = i;
					break;
				}
			}
			Vector2 currentPosition = _touches[num3].m_currentPosition;
			TouchPhase phase = _touches[num3].m_phase;
			if (phase == null || !this.m_drawing)
			{
				Debug.Log("DRAWING STARTED", null);
				minigame.m_groundsModificationCount++;
				this.m_drawing = true;
				this.m_brushPos = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, currentPosition, 90f) + this.brushXYOffset;
				autoGeometryLayer.TakeSnapshot();
				autoGeometryLayer.ResetUndoRect();
				SoundS.PlaySingleShot("/UI/DrawBegin", Vector3.zero, 1f);
				this.m_brushSoundVelocity = 0f;
			}
			this.PaintWithBrush(currentPosition, 0.5f, _touches[num3].m_pressure, true, _touches[num3].m_type);
			if (phase == 3 || phase == 4)
			{
				Debug.Log("DRAWING ENDED", null);
				this.m_drawing = false;
				this.RemoveDrawFx();
				if (this.m_brushVisualTC != null)
				{
					EntityManager.RemoveEntity(this.m_brushVisualTC.p_entity);
					this.m_brushVisualTC = null;
				}
				SoundS.PlaySingleShot("/UI/DrawEnd", Vector3.zero, 1f);
				LevelManager.m_currentLevel.SetLayerItems();
			}
		}
		else if (PsState.m_currentTool == EditorTool.None)
		{
			if (this.m_drawing)
			{
				this.m_drawing = false;
				this.RemoveDrawFx();
				EntityManager.RemoveEntity(this.m_brushVisualTC.p_entity);
				this.m_brushVisualTC = null;
				SoundS.PlaySingleShot("/UI/DrawEnd", Vector3.zero, 1f);
				LevelManager.m_currentLevel.SetLayerItems();
			}
			if (_touchCount == 2)
			{
				if (_touchIsSecondary)
				{
					return;
				}
				if (_touches[0].m_primaryPhase == TouchAreaPhase.Began || _touches[1].m_primaryPhase == TouchAreaPhase.Began)
				{
					PsState.m_currentTool = EditorTool.Camera;
					cpBB targetFrame2 = PsState.m_editorCamTarget.targetFrame;
					this.m_startZoom = new Vector2(targetFrame2.r - targetFrame2.l, targetFrame2.t - targetFrame2.b);
					this.m_startDistance = (_touches[0].m_currentPosition - _touches[1].m_currentPosition).magnitude;
					this.m_startDistanceMultipler = CameraS.m_mainCameraDistanceMultipler;
					this.m_pinching = true;
				}
			}
			else if (_touchCount == 1)
			{
				if (_touchPhase == TouchAreaPhase.DragStart || (_touchArea.m_wasDragged && _touchPhase == TouchAreaPhase.MoveIn))
				{
					PsState.m_currentTool = EditorTool.Camera;
				}
				else if (_touchPhase == TouchAreaPhase.ReleaseIn && !_touchArea.m_wasDragged && !_touchIsSecondary)
				{
					if (GizmoManager.GetSelection().Count > 0)
					{
						List<GraphElement> selection = GizmoManager.GetSelection();
						GizmoManager.ClearSelection();
						new SelectUndoAction(selection, GizmoManager.GetSelection());
						GizmoManager.Update();
					}
					float num4 = 25f * (1f / CameraS.m_mainCameraDistanceMultipler);
					Vector3 touchWorldPos = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, _touches[0].m_currentPosition, 0f);
					Vector3 position = CameraS.m_mainCamera.transform.position;
					Vector3 vector5 = touchWorldPos - position;
					RaycastHit[] array = Physics.SphereCastAll(touchWorldPos - vector5.normalized * 500f, num4, vector5.normalized, 50000f, 1 << CameraS.m_mainCamera.gameObject.layer);
					TouchAreaC touchAreaC = null;
					float num5 = 999999f;
					foreach (RaycastHit raycastHit in array)
					{
						float magnitude = (touchWorldPos - raycastHit.transform.position).magnitude;
						if (magnitude < num5)
						{
							TouchAreaBootstrap touchAreaBootstrap = raycastHit.transform.GetComponent("TouchAreaBootstrap") as TouchAreaBootstrap;
							if (touchAreaBootstrap != null)
							{
								num5 = magnitude;
								touchAreaC = touchAreaBootstrap.m_TAC;
							}
						}
					}
					if (touchAreaC != null)
					{
						touchAreaC.m_touchCount = 1;
						_touches[0].m_primaryArea = touchAreaC;
						touchAreaC.d_TouchEventDelegate(touchAreaC, _touchPhase, _touchIsSecondary, 1, _touches);
						_touchArea.m_touchCount = 0;
						return;
					}
					if (this.m_groundPicking)
					{
						Debug.LogWarning("Ground piking");
						int num6 = AutoGeometryManager.GetLayerAtWorldPos(TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, _touches[0].m_currentPosition, 90f) + this.brushXYOffset * 0.5f, 0);
						if (num6 < 0)
						{
							num6 = AutoGeometryManager.GetLayerAtWorldPos(TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, _touches[0].m_currentPosition, -210f) + this.brushXYOffset * 0.5f, 0);
						}
						if (num6 >= 0 && PsState.m_activeGameLoop.m_gameMode.isMaterialAvailable(PsState.m_activeMinigame.GetMatIndexFromLayerIndex(num6)))
						{
							PsState.m_drawLayer = num6;
							PsState.m_drawMenuTargetPage = PsState.m_activeMinigame.GetMatIndexFromLayerIndex(num6);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x00084FDC File Offset: 0x000833DC
	private void CreateBrushPrefab(bool _rectBrush)
	{
		Vector2[] array;
		if (_rectBrush)
		{
			array = DebugDraw.GetRoundedRect((float)(this.m_dynamicBrush.m_width * 16), (float)(this.m_dynamicBrush.m_width * 16), 8f, 2, Vector2.zero, false);
		}
		else
		{
			array = DebugDraw.GetCircle((float)(this.m_dynamicBrush.m_width * 8), 36, Vector2.zero, false);
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_brushVisualTC, Vector3.zero, array, 8f, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), CameraS.m_mainCamera, Position.Center, true);
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x00085070 File Offset: 0x00083470
	private void PaintWithBrush(Vector2 _touchPos, float _speed, float _pressure, bool _soften, TouchType _type)
	{
		_pressure = ToolBox.limitBetween(_pressure * 2f, 0f, 1f);
		Minigame activeMinigame = PsState.m_activeMinigame;
		activeMinigame.m_changed = true;
		bool rectBrush = activeMinigame.m_groundNode.m_AGLayer[PsState.m_drawLayer].m_groundC.m_ground.m_rectBrush;
		bool flag = false;
		bool touchPressureSupported = Input.touchPressureSupported;
		if (touchPressureSupported || Mathf.Abs(this.m_lastBrushCameraDistance - CameraS.m_mainCameraDistanceMultipler) > 0.01f || this.m_lastBrushGroundIndex != PsState.m_drawLayer)
		{
			this.m_lastBrushCameraDistance = CameraS.m_mainCameraDistanceMultipler;
			this.m_lastBrushGroundIndex = PsState.m_drawLayer;
			float num;
			if (!rectBrush)
			{
				num = Mathf.Lerp(2.5f, 7.5f, this.m_currentZoomPos);
			}
			else
			{
				num = Mathf.Lerp(1.5f, 6.25f, this.m_currentZoomPos);
			}
			if (this.m_dynamicBrush != null)
			{
				this.m_dynamicBrush.Destroy();
			}
			float num2 = 0.5f;
			if (touchPressureSupported)
			{
				if (_type == 2)
				{
					num2 = 0.08f + _pressure * 0.5f;
					num *= 0.8f + _pressure * 0.5f;
				}
				else
				{
					num2 = 0.1f + _pressure * 0.5f;
					num *= 1f + _pressure * 0.3f;
				}
				flag = true;
			}
			this.m_dynamicBrush = new AutoGeometryBrush(num, rectBrush, num2, 0f);
		}
		float num3 = (float)this.m_dynamicBrush.m_width;
		Vector2 vector = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, _touchPos, 90f);
		Vector2 vector2 = vector + this.brushXYOffset - this.m_brushPos;
		float num4 = 200f * _speed;
		vector2 = vector2.normalized * ToolBox.limitBetween(vector2.magnitude, 0f, num4);
		if (this.m_brushVisualTC == null)
		{
			Entity entity = EntityManager.AddEntity();
			this.m_brushVisualTC = TransformS.AddComponent(entity);
			this.CreateBrushPrefab(rectBrush);
			string text;
			if (PsState.m_addDown)
			{
				text = activeMinigame.m_groundNode.m_AGLayer[PsState.m_drawLayer].m_groundC.m_ground.m_drawSound;
			}
			else
			{
				text = "/UI/EraseLoop";
			}
			this.m_brushSound = SoundS.AddComponent(this.m_brushVisualTC, text, 1f, false);
			SoundS.SetSoundParameter(this.m_brushSound, "Velocity", 0f);
			SoundS.PlaySound(this.m_brushSound, false);
			this.StartDrawFx(vector + Vector3.forward * -90f);
		}
		else
		{
			float positionBetween = ToolBox.getPositionBetween(vector2.magnitude, 0.1f, 20f);
			this.m_brushSoundVelocity += (positionBetween - this.m_brushSoundVelocity) * 0.25f;
			SoundS.SetSoundParameter(this.m_brushSound, "Velocity", this.m_brushSoundVelocity);
			if (flag)
			{
				PrefabS.RemoveComponentsByEntity(this.m_brushVisualTC.p_entity, true);
				this.CreateBrushPrefab(rectBrush);
			}
		}
		TransformS.SetPosition(this.m_brushVisualTC, vector + Vector3.forward * -90f);
		if (this.m_drawFx != null)
		{
			TransformS.SetPosition(this.m_drawFxTC, this.m_brushVisualTC.transform.localPosition);
		}
		if (vector2.magnitude > 1f)
		{
			this.m_brushPos = this.PaintLine(this.m_brushPos, this.m_brushPos + vector2, num3, _soften);
		}
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x000853E4 File Offset: 0x000837E4
	private Vector2 PaintLine(Vector2 _startPos, Vector2 _endPos, float _maxStep, bool _soften)
	{
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		Vector2 vector = _endPos - _startPos;
		Vector2 vector2 = _startPos;
		int num = Mathf.FloorToInt(1f + vector.magnitude / _maxStep);
		Vector2 vector3 = vector / (float)num;
		for (int i = 0; i < num; i++)
		{
			vector2 += vector3;
			AutoGeometryManager.PaintWithBrush(minigame.m_groundNode.m_AGLayer[PsState.m_drawLayer], this.m_dynamicBrush, vector2, PsState.m_addDown, this.m_dynamicBrush.m_subPixelAccuracy, true);
		}
		return vector2;
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x00085474 File Offset: 0x00083874
	private void StartDrawFx(Vector3 _pos)
	{
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		string drawFx = minigame.m_groundNode.m_AGLayer[PsState.m_drawLayer].m_groundC.m_ground.m_drawFx;
		if (this.m_drawFx == null && drawFx != null)
		{
			Entity entity = EntityManager.AddEntity("GTAG_AUTODESTROY");
			this.m_drawFxTC = TransformS.AddComponent(entity, _pos);
			GameObject gameObject = ResourceManager.GetGameObject(drawFx);
			this.m_drawFx = PrefabS.AddComponent(this.m_drawFxTC, Vector3.zero, gameObject, drawFx, false, true);
		}
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x000854F8 File Offset: 0x000838F8
	private void RemoveDrawFx()
	{
		if (this.m_drawFx != null)
		{
			TimerS.AddComponent(this.m_drawFxTC.p_entity, "RemoveTimerForDrawFx", 1f, 0f, true, null);
			this.m_drawFx.p_gameObject.GetComponent<ParticleSystem>().Stop(true);
			this.m_drawFx = null;
		}
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0008554F File Offset: 0x0008394F
	public override void Exit()
	{
		if (PsState.m_editorCamTarget != null)
		{
			EntityManager.RemoveEntity(PsState.m_editorCamTarget.p_entity);
			PsState.m_editorCamTarget = null;
		}
		EntityManager.RemoveEntity(this.m_fullscreenTAC.p_entity);
		this.m_fullscreenTAC = null;
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x00085588 File Offset: 0x00083988
	public void ShowEditorItemPurchasePopup(PsEditorItem _editorItem, Action _purchasedCallback)
	{
		CameraS.CreateBlur(null);
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUIEditorItemPurchasePopup), null, null, null, false, true, InitialPage.Center, false, true, false);
		(popup.m_mainContent as PsUIEditorItemPurchasePopup).CreateContent(_editorItem);
		popup.SetAction("Purchase", delegate
		{
			popup.Destroy();
			if ((_editorItem.m_currency == PsCurrency.Coin && PsMetagameManager.m_playerStats.coins >= _editorItem.m_price) || (_editorItem.m_currency == PsCurrency.Gem && PsMetagameManager.m_playerStats.diamonds >= _editorItem.m_price))
			{
				this.SetResourceView();
				this.PurchaseEditorItem(_editorItem, _purchasedCallback);
				CameraS.RemoveBlur();
			}
			else if (_editorItem.m_currency == PsCurrency.Coin)
			{
				this.OpenCoinPopup();
			}
			else if (_editorItem.m_currency == PsCurrency.Gem)
			{
				this.OpenGemPopup();
			}
		});
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
			CameraS.RemoveBlur();
			this.SetResourceView();
		});
		if (PsMetagameManager.m_menuResourceView != null)
		{
			this.m_lastResourceView = PsMetagameManager.m_menuResourceView.SetLastView();
		}
		TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, new Vector3(0.75f, 0.75f, 0f), Vector3.one, 0.75f, 0f, true);
		PsMetagameManager.ShowResources(popup.m_mainContent.m_camera, true, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x000856A4 File Offset: 0x00083AA4
	private void PurchaseEditorItem(PsEditorItem _editorItem, Action _purchasedCallback)
	{
		PsCurrency currency = _editorItem.m_currency;
		if (currency != PsCurrency.Coin)
		{
			if (currency == PsCurrency.Gem)
			{
				PsMetagameManager.m_playerStats.CumulateDiamonds(-_editorItem.m_price);
				FrbMetrics.SpendVirtualCurrency("editor_" + _editorItem.m_name, "gems", (double)_editorItem.m_price);
			}
		}
		else
		{
			PsMetagameManager.m_playerStats.CumulateCoins(-_editorItem.m_price);
			FrbMetrics.SpendVirtualCurrency("editor_" + _editorItem.m_name, "coins", (double)_editorItem.m_price);
		}
		PsMetagameManager.m_playerStats.CumulateEditorResources(_editorItem.m_identifier, 1);
		PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
		_purchasedCallback.Invoke();
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x00085798 File Offset: 0x00083B98
	private void OpenCoinPopup()
	{
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterNotEnoughCoins), null, null, null, false, true, InitialPage.Center, false, true, false);
		popup.SetAction("EnterShop", delegate
		{
			popup.Destroy();
			this.OpenShopPopup(PsCurrency.Coin);
		});
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
			CameraS.RemoveBlur();
			this.SetResourceView();
		});
		TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		PsMetagameManager.ShowResources(popup.m_mainContent.m_camera, true, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x00085868 File Offset: 0x00083C68
	private void OpenGemPopup()
	{
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterNotEnoughDiamonds), null, null, null, false, true, InitialPage.Center, false, true, false);
		popup.SetAction("EnterShop", delegate
		{
			popup.Destroy();
			this.OpenShopPopup(PsCurrency.Gem);
		});
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
			CameraS.RemoveBlur();
			this.SetResourceView();
		});
		TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, new Vector3(0.75f, 0.75f, 0f), Vector3.one, 0.75f, 0f, true);
		PsMetagameManager.ShowResources(popup.m_mainContent.m_camera, true, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x0008593C File Offset: 0x00083D3C
	public void OpenShopPopup(PsCurrency _currency)
	{
		if (PsMetagameManager.m_menuResourceView != null && this.m_lastResourceView == null)
		{
			this.m_lastResourceView = PsMetagameManager.m_menuResourceView.SetLastView();
		}
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterShopAll), typeof(PsUITopBackButton), null, null, false, true, InitialPage.Center, false, true, false);
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
			PsUIEditorEdit psUIEditorEdit = PsIngameMenu.m_playMenu as PsUIEditorEdit;
			if (psUIEditorEdit != null && psUIEditorEdit.m_editorItemSelector != null)
			{
				psUIEditorEdit.m_editorItemSelector.UpdateCards();
			}
			CameraS.RemoveBlur();
			this.SetResourceView();
		});
		if (_currency == PsCurrency.Coin)
		{
			(popup.m_mainContent as PsUICenterShopAll).ScrollToCoinShop();
		}
		else if (_currency == PsCurrency.Gem)
		{
			(popup.m_mainContent as PsUICenterShopAll).ScrollToGemShop();
		}
		else
		{
			(popup.m_mainContent as PsUICenterShopAll).ScrollToChestShop();
		}
		popup.Step();
		PsMetagameManager.ShowResources(popup.m_mainContent.m_camera, false, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x00085A40 File Offset: 0x00083E40
	private void SetResourceView()
	{
		if (this.m_lastResourceView != null && PsMetagameManager.m_menuResourceView != null)
		{
			PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastResourceView);
			this.m_lastResourceView = null;
		}
		else
		{
			PsMetagameManager.HideResources();
		}
	}

	// Token: 0x04001139 RID: 4409
	private const int BRUSH_Z_OFFSET = 90;

	// Token: 0x0400113A RID: 4410
	private Vector2 brushXYOffset = new Vector2(16f, 16f);

	// Token: 0x0400113B RID: 4411
	private TouchAreaC m_fullscreenTAC;

	// Token: 0x0400113C RID: 4412
	private float m_startDistance;

	// Token: 0x0400113D RID: 4413
	private float m_startDistanceMultipler;

	// Token: 0x0400113E RID: 4414
	private Vector2 m_startZoom;

	// Token: 0x0400113F RID: 4415
	private bool m_pinching;

	// Token: 0x04001140 RID: 4416
	private bool m_drawing;

	// Token: 0x04001141 RID: 4417
	private Vector2 m_brushPos;

	// Token: 0x04001142 RID: 4418
	private AutoGeometryBrush m_dynamicBrush;

	// Token: 0x04001143 RID: 4419
	private AutoGeometryBrush m_dynamicMaskBrush;

	// Token: 0x04001144 RID: 4420
	private float m_lastBrushCameraDistance;

	// Token: 0x04001145 RID: 4421
	private int m_lastBrushGroundIndex;

	// Token: 0x04001146 RID: 4422
	private TransformC m_brushVisualTC;

	// Token: 0x04001147 RID: 4423
	private SoundC m_brushSound;

	// Token: 0x04001148 RID: 4424
	private float m_brushSoundVelocity;

	// Token: 0x04001149 RID: 4425
	private float m_currentZoomPos;

	// Token: 0x0400114A RID: 4426
	private TransformC m_drawFxTC;

	// Token: 0x0400114B RID: 4427
	private PrefabC m_drawFx;

	// Token: 0x0400114C RID: 4428
	private int m_everyplayPauseTicks;

	// Token: 0x0400114D RID: 4429
	private bool m_recording;

	// Token: 0x0400114E RID: 4430
	public float m_zoomLimitMultiplier = 1f;

	// Token: 0x0400114F RID: 4431
	public bool m_groundPicking = true;

	// Token: 0x04001150 RID: 4432
	public static bool m_enteredEditor;

	// Token: 0x04001151 RID: 4433
	private LastResourceView m_lastResourceView;
}
