using System;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class PsUITutorialArrowUI : ITutorial
{
	// Token: 0x06001259 RID: 4697 RVA: 0x000B54A8 File Offset: 0x000B38A8
	public PsUITutorialArrowUI(UIComponent _target, bool _fadeIn = true, Action _callBack = null, float _delay = 2f, Vector3 _pointerOffset = default(Vector3), bool _usePrefabCamera = false)
	{
		this.m_usePrefabCamera = _usePrefabCamera;
		PsMetagameManager.m_tutorialArrow = this;
		this.m_uiTarget = _target;
		this.m_fadeIn = _fadeIn;
		this.m_callBack = _callBack;
		this.m_originalCamera = _target.m_camera;
		this.m_pointerOffset = _pointerOffset;
		this.GetTouchArea();
		this.SetCamera();
		TouchAreaS.CancelAllTouches(null);
		TimerS.AddComponent(this.m_uiTarget.m_TC.p_entity, string.Empty, 0f, 0.2f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			TouchAreaS.Enable();
		});
		TimerS.AddComponent(this.m_uiTarget.m_TC.p_entity, string.Empty, 0f, 0f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			if (!this.m_destroyed)
			{
				this.DrawCanvas();
			}
		});
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x000B5579 File Offset: 0x000B3979
	public void GetTouchArea()
	{
		this.m_touchArea = this.m_uiTarget.m_TAC;
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x000B558C File Offset: 0x000B398C
	public void DrawCanvas()
	{
		bool flag = false;
		this.m_canvas = new UICanvas(null, false, "TutorialCanvas", null, string.Empty);
		this.m_canvas.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_canvas.RemoveDrawHandler();
		this.m_canvas.SetDepthOffset(-420f);
		this.offset = new UIComponent(this.m_canvas, false, "Tutorial offset", null, null, string.Empty);
		this.offset.SetSize(0f, 0f, RelativeTo.ScreenWidth);
		this.offset.SetMargins(-1f, -1f, -1f, -1f, RelativeTo.ScreenHeight);
		this.m_emptyCanvas = new UICanvas(this.offset, false, "Empty Tutorial Canvas", null, string.Empty);
		this.m_emptyCanvas.SetSize(0f, 0f, RelativeTo.ScreenWidth);
		this.m_emptyCanvas.SetDepthOffset(-10f);
		this.m_emptyCanvas.SetMargins(-1f, -1f, -1f, -1f, RelativeTo.ScreenHeight);
		this.m_finger = new UIFittedSprite(this.m_emptyCanvas, false, "Tutorial Finger", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_pointer_finger", null), true, true);
		this.m_finger.SetHeight(0.1f, RelativeTo.ScreenWidth);
		SoundS.PlaySingleShot("/UI/TutorialHandAppear", Vector3.zero, 1f);
		Vector3 vector = this.m_tutorialCamera.WorldToViewportPoint(this.m_uiTarget.m_TC.transform.position + this.m_pointerOffset);
		float num = 0.022f;
		float num2 = 0.06f;
		this.offset.SetAlign(vector.x, vector.y);
		this.m_emptyCanvas.SetAlign(0.5f - num2, 0.5f - num);
		float num3 = -this.m_uiTarget.m_actualWidth / 2f;
		if ((double)this.m_tutorialCamera.WorldToViewportPoint(this.m_uiTarget.m_TC.transform.position).x < 0.5)
		{
			num3 *= -1f;
			flag = true;
		}
		Vector3 vector2 = this.m_tutorialCamera.WorldToViewportPoint(this.m_uiTarget.m_TC.transform.position + new Vector3(num3, 0f, 0f));
		this.offset.SetAlign(vector2.x, vector2.y);
		this.m_canvas.Update();
		this.m_finger.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find("WOE/Unlit/VertexColorUnlitDouble2");
		if (flag)
		{
			this.offset.m_TC.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		Camera camera = null;
		GameObject gameObject = GameObject.Find("OverlayCamera");
		if (gameObject != null && gameObject.GetComponent<Camera>() != null)
		{
			camera = gameObject.GetComponent<Camera>();
		}
		if (camera == null)
		{
			camera = this.m_originalCamera;
		}
		this.m_fadeOutSpriteSheet = SpriteS.AddSpriteSheet(camera, ResourceManager.GetMaterial(RESOURCE.LoadingScreenFadeMat_Material), 1f);
		SpriteC spriteC = SpriteS.AddComponent(this.m_canvas.m_TC, new Frame(0f, 0f, (float)Screen.width, (float)Screen.height), this.m_fadeOutSpriteSheet);
		Color black = Color.black;
		if (this.m_fadeIn)
		{
			SpriteS.SetColor(spriteC, new Color(black.r, black.g, black.b, 0f));
			TweenC tweenC = TweenS.AddTransformTween(this.m_canvas.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, Vector3.zero, new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 0f, true);
			tweenC.useUnscaledDeltaTime = true;
		}
		else
		{
			SpriteS.SetColor(spriteC, new Color(black.r, black.g, black.b, 0.5f));
		}
		Vector3 vector3;
		vector3..ctor(-25f, 0f, 0f);
		TweenC tweenC2 = TweenS.AddTransformTween(this.m_finger.m_TC, TweenedProperty.Position, TweenStyle.QuadIn, vector3, Vector3.zero, 0.4f, 0f, true);
		tweenC2.useUnscaledDeltaTime = true;
		TweenS.SetAdditionalTweenProperties(tweenC2, -1, true, TweenStyle.QuadOut);
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x000B5A00 File Offset: 0x000B3E00
	public void SetCamera()
	{
		this.layernumberUI = 11;
		GameObject gameObject = new GameObject("Tutorial Camera");
		this.m_tutorialCamera = gameObject.AddComponent<Camera>();
		CameraS.m_cameras.Add(this.m_tutorialCamera);
		this.m_tutorialCamera.CopyFrom(CameraS.m_uiCamera);
		gameObject.transform.position += new Vector3(0f, 0f, -200f);
		gameObject.layer = this.layernumberUI;
		this.m_tutorialCamera.cullingMask = 0;
		this.m_tutorialCamera.cullingMask |= 1 << this.layernumberUI;
		this.m_tutorialCamera.depth = 15f;
		this.SetTouchAreas(this.m_tutorialCamera);
		if (this.m_usePrefabCamera)
		{
			PrefabS.SetCamera(this.m_uiTarget.m_TC.transform.gameObject, this.m_tutorialCamera);
		}
		else
		{
			this.m_uiTarget.SetCamera(this.m_tutorialCamera, true, true);
		}
		this.m_uiTarget.Update();
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x000B5B18 File Offset: 0x000B3F18
	public void SetTouchAreas(Camera _camera)
	{
		if (this.m_touchArea == null)
		{
			return;
		}
		TouchEventDelegate d_TouchEventDelegate = this.m_touchArea.d_TouchEventDelegate;
		TouchAreaS.RemoveAllTouchEventListeners(this.m_touchArea);
		TouchAreaS.AddTouchEventListener(this.m_touchArea, d_TouchEventDelegate);
		TouchAreaS.AddTouchEventListener(this.m_touchArea, new TouchEventDelegate(this.TouchHandler));
		TouchAreaS.SetCamera(this.m_touchArea, _camera);
		TouchAreaS.SetTouchCameraFilter(_camera);
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x000B5B80 File Offset: 0x000B3F80
	public virtual void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchCount == 1 && (_touchPhase == TouchAreaPhase.ReleaseIn || (!_touchArea.m_allowPrimary && _touchPhase == TouchAreaPhase.RollIn)))
		{
			TouchAreaS.RemoveTouchCameraFilter();
			Debug.LogWarning("touchi: " + _touchArea.m_name);
			this.Destroy();
			if (this.m_callBack != null)
			{
				this.m_callBack.Invoke();
			}
		}
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x000B5BE4 File Offset: 0x000B3FE4
	public virtual void Step()
	{
		if (this.m_destroy)
		{
		}
		if (this.m_canvas == null)
		{
			return;
		}
		float num = -this.m_uiTarget.m_actualWidth / 2f;
		if ((double)this.m_tutorialCamera.WorldToViewportPoint(this.m_uiTarget.m_TC.transform.position).x < 0.5)
		{
			num *= -1f;
		}
		Vector3 vector = this.m_tutorialCamera.WorldToViewportPoint(this.m_uiTarget.m_TC.transform.position + new Vector3(num, 0f, 0f) + this.m_pointerOffset);
		this.offset.SetAlign(vector.x, vector.y);
		this.offset.UpdateAlign();
		if (this.m_uiTarget.m_TC.transform.lossyScale == Vector3.zero)
		{
			if (this.m_callBack != null)
			{
				this.m_callBack.Invoke();
			}
			TouchAreaS.RemoveTouchCameraFilter();
			this.Destroy();
		}
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x000B5D04 File Offset: 0x000B4104
	public virtual void Destroy()
	{
		if (PsMetagameManager.m_tutorialArrow == this)
		{
			PsMetagameManager.m_tutorialArrow = null;
		}
		if (this.m_fadeOutSpriteSheet != null)
		{
			SpriteS.RemoveSpriteSheet(this.m_fadeOutSpriteSheet);
			this.m_fadeOutSpriteSheet = null;
		}
		if (this.m_canvas != null)
		{
			this.m_canvas.Destroy();
		}
		TouchAreaS.RemoveTouchCameraFilter();
		TouchAreaS.RemoveTouchEventListener(this.m_touchArea, new TouchEventDelegate(this.TouchHandler));
		this.m_uiTarget.SetCamera(this.m_originalCamera, true, false);
		this.m_uiTarget.Update();
		for (int i = 0; i < TouchAreaS.GetTouchesOnTAC(this.m_touchArea).Count; i++)
		{
			TouchAreaS.GetTouchesOnTAC(this.m_touchArea)[i].m_consumingCamera = this.m_originalCamera;
		}
		CameraS.m_cameras.Remove(this.m_tutorialCamera);
		Object.Destroy(this.m_tutorialCamera.gameObject);
		this.m_destroyed = true;
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x000B5DF8 File Offset: 0x000B41F8
	public static void DrawHandler(UIComponent _c)
	{
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Color color;
		color..ctor(0f, 0f, 0f, 0.3f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x04001582 RID: 5506
	protected int layernumberUI;

	// Token: 0x04001583 RID: 5507
	protected Camera m_tutorialCamera;

	// Token: 0x04001584 RID: 5508
	protected Camera m_originalCamera;

	// Token: 0x04001585 RID: 5509
	protected SpriteSheet m_fadeOutSpriteSheet;

	// Token: 0x04001586 RID: 5510
	protected UICanvas m_canvas;

	// Token: 0x04001587 RID: 5511
	protected UIFittedSprite m_finger;

	// Token: 0x04001588 RID: 5512
	protected UIComponent offset;

	// Token: 0x04001589 RID: 5513
	public UICanvas m_emptyCanvas;

	// Token: 0x0400158A RID: 5514
	protected UIComponent m_uiTarget;

	// Token: 0x0400158B RID: 5515
	protected TouchAreaC m_touchArea;

	// Token: 0x0400158C RID: 5516
	protected bool m_destroy;

	// Token: 0x0400158D RID: 5517
	private bool m_fadeIn;

	// Token: 0x0400158E RID: 5518
	public Action m_callBack;

	// Token: 0x0400158F RID: 5519
	public bool m_destroyed;

	// Token: 0x04001590 RID: 5520
	private Vector3 m_pointerOffset;

	// Token: 0x04001591 RID: 5521
	private bool m_usePrefabCamera;
}
