using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200025F RID: 607
public abstract class PsUITutorialArrowBase : ITutorial
{
	// Token: 0x0600123E RID: 4670 RVA: 0x000B4431 File Offset: 0x000B2831
	public virtual void Step()
	{
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x000B4433 File Offset: 0x000B2833
	public virtual void Destroy()
	{
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x000B4435 File Offset: 0x000B2835
	protected virtual void GetTarget()
	{
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x000B4438 File Offset: 0x000B2838
	protected void DrawCanvas()
	{
		this.m_canvas = new UICanvas(null, false, "TutorialCanvas", null, string.Empty);
		this.m_canvas.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_canvas.RemoveDrawHandler();
		this.m_canvas.SetDepthOffset(-230f);
		this.offset = new UIComponent(this.m_canvas, false, "Tutorial offset", null, null, string.Empty);
		this.offset.SetSize(0f, 0f, RelativeTo.ScreenWidth);
		this.offset.SetMargins(-1f, -1f, -1f, -1f, RelativeTo.ScreenHeight);
		UICanvas uicanvas = new UICanvas(this.offset, false, "Empty Tutorial Canvas", null, string.Empty);
		uicanvas.SetSize(0f, 0f, RelativeTo.ScreenWidth);
		uicanvas.SetMargins(-1f, -1f, -1f, -1f, RelativeTo.ScreenHeight);
		this.m_finger = new UIFittedSprite(uicanvas, false, "Tutorial Finger", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_pointer_finger", null), true, true);
		this.m_finger.SetHeight(0.1f, RelativeTo.ScreenWidth);
		SoundS.PlaySingleShot("/UI/TutorialHandAppear", Vector3.zero, 1f);
		Vector3 vector = this.m_3Dcamera.WorldToViewportPoint(this.m_goTarget.transform.position);
		float num = 0.022f;
		float num2 = 0.1f;
		this.offset.SetAlign(vector.x, vector.y);
		uicanvas.SetAlign(0.5f - num2, 0.5f - num);
		this.m_canvas.Update();
		this.m_finger.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find("WOE/Unlit/VertexColorUnlitDouble2");
		this.m_fadeOutSpriteSheet = SpriteS.AddSpriteSheet(this.m_canvas.m_camera, ResourceManager.GetMaterial(RESOURCE.LoadingScreenFadeMat_Material), 1f);
		SpriteC spriteC = SpriteS.AddComponent(this.m_canvas.m_TC, new Frame(0f, 0f, (float)Screen.width, (float)Screen.height), this.m_fadeOutSpriteSheet);
		Color black = Color.black;
		SpriteS.SetColor(spriteC, new Color(black.r, black.g, black.b, 0f));
		TweenC tweenC = TweenS.AddTransformTween(this.m_canvas.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, Vector3.zero, new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 0f, true);
		Vector3 vector2;
		vector2..ctor(-25f, 0f, 0f);
		TweenC tweenC2 = TweenS.AddTransformTween(this.m_finger.m_TC, TweenedProperty.Position, TweenStyle.QuadIn, vector2, Vector3.zero, 0.4f, 0f, true);
		TweenS.SetAdditionalTweenProperties(tweenC2, -1, true, TweenStyle.QuadOut);
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x000B4710 File Offset: 0x000B2B10
	protected virtual void SetCamera()
	{
		this.SetSpriteSheetToCamera();
		PrefabS.SetCamera(this.m_goTarget, this.m_3Dcamera);
		TouchAreaS.AddTouchEventListener(this.m_touchArea, new TouchEventDelegate(this.TouchHandler));
		TouchAreaS.SetCamera(this.m_touchArea, this.m_3Dcamera);
		TouchAreaS.SetTouchCameraFilter(this.m_3Dcamera);
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x000B4768 File Offset: 0x000B2B68
	public void GetTargetSprites(Entity e)
	{
		this.m_spriteList = EntityManager.GetComponentsByEntity(ComponentType.Sprite, e).ConvertAll<SpriteC>((IComponent x) => x as SpriteC);
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x000B4799 File Offset: 0x000B2B99
	protected virtual void SetSpriteSheetToCamera()
	{
		if (this.m_spriteList.Count > 0)
		{
			this.m_spriteList[0].p_spriteSheet.SetCamera(this.m_3Dcamera);
			this.m_spriteSheetSetToCamera = true;
		}
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x000B47CF File Offset: 0x000B2BCF
	public virtual void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchCount == 1 && _touchPhase == TouchAreaPhase.ReleaseIn && !_touches[0].m_dragged)
		{
			TouchAreaS.RemoveTouchCameraFilter();
			Debug.LogWarning("touchi: " + _touchArea.m_name);
			this.Destroy();
		}
	}

	// Token: 0x0400156A RID: 5482
	protected Camera m_3Dcamera;

	// Token: 0x0400156B RID: 5483
	protected Camera m_originalCamera;

	// Token: 0x0400156C RID: 5484
	protected SpriteSheet m_fadeOutSpriteSheet;

	// Token: 0x0400156D RID: 5485
	protected UICanvas m_canvas;

	// Token: 0x0400156E RID: 5486
	protected UIFittedSprite m_finger;

	// Token: 0x0400156F RID: 5487
	protected UIComponent offset;

	// Token: 0x04001570 RID: 5488
	protected List<SpriteC> m_spriteList;

	// Token: 0x04001571 RID: 5489
	protected bool m_spriteSheetSetToCamera;

	// Token: 0x04001572 RID: 5490
	protected GameObject m_goTarget;

	// Token: 0x04001573 RID: 5491
	protected TouchAreaC m_touchArea;

	// Token: 0x04001574 RID: 5492
	protected bool m_destroy;
}
