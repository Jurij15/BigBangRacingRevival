using System;
using UnityEngine;

// Token: 0x02000248 RID: 584
public class PsUIGIFDisplay : UIComponent
{
	// Token: 0x060011B8 RID: 4536 RVA: 0x000AC264 File Offset: 0x000AA664
	public PsUIGIFDisplay(UIComponent _parent, Texture[] _frames, float _frameDelay, bool _touchable, string _tag, float _cornerSize = 0.005f)
		: base(_parent, _touchable, _tag, null, null, string.Empty)
	{
		this.SetMargins(0.02f, 0.02f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_cornerSize = _cornerSize;
		this.m_material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ScreenshotMat_Material));
		this.m_material.mainTexture = ResourceManager.GetTexture(RESOURCE.ScreenshotDefault_Texture2D);
		this.m_overlayMat = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.Gif_Overlay_Mat_Material));
		this.m_frames = _frames;
		this.m_currentFrame = 0;
		this.m_frameDelay = _frameDelay;
		this.m_time = 0f;
		this.m_lastTime = 0f;
		this.SetFrame(0);
		UIComponent uicomponent = new UIComponent(this, false, string.Empty, null, null, string.Empty);
		uicomponent.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicomponent.SetWidth(0.9f, RelativeTo.ParentHeight);
		uicomponent.SetVerticalAlign(1f);
		uicomponent.RemoveDrawHandler();
		this.m_t1 = new UIFittedText(uicomponent, false, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
		UIComponent uicomponent2 = new UIComponent(this, false, string.Empty, null, null, string.Empty);
		uicomponent2.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicomponent2.SetWidth(0.95f, RelativeTo.ParentHeight);
		uicomponent2.SetVerticalAlign(0f);
		uicomponent2.RemoveDrawHandler();
		this.m_t2 = new UIFittedText(uicomponent2, false, string.Empty, PsStrings.Get(StringID.DEATH_SCREEN_RECORDING), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
		this.m_canTouch = false;
		this.m_scaledUp = false;
		this.m_currentScale = Vector3.one;
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x000AC3FF File Offset: 0x000AA7FF
	public void SetFrames(Texture[] _frames)
	{
		this.m_canTouch = true;
		this.m_t2.SetText(this.shareText);
		this.m_t2.Update();
		this.m_frames = _frames;
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x000AC42C File Offset: 0x000AA82C
	public void SetToProcessState()
	{
		this.m_canTouch = false;
		TweenS.RemoveAllTweensFromEntity(this.m_TC.p_entity);
		TransformS.SetScale(this.m_TC, 1f);
		this.m_t1.SetText(string.Empty);
		this.m_t2.SetText(string.Empty);
		if (this.m_progressBar != null)
		{
			return;
		}
		if (this.m_progressBar == null)
		{
			this.m_progressBar = new PsUIResourceProgressBar(this, 0, 100, "gif progress", false, string.Empty);
			this.m_progressBar.SetHeight(0.05f, RelativeTo.ScreenHeight);
			this.m_progressBar.SetWidth(0.9f, RelativeTo.ParentWidth);
			this.m_progressBar.SetHorizontalAlign(0.5f);
			this.m_progressBar.SetVerticalAlign(0.05f);
		}
		this.Update();
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x000AC4F9 File Offset: 0x000AA8F9
	public void UpdateProgress(int _n, int _max = -1)
	{
		if (this.m_progressBar != null)
		{
			this.m_progressBar.SetValues(_n, (_max >= 0) ? _max : this.m_frames.Length);
		}
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x000AC527 File Offset: 0x000AA927
	public void SetProgressText(string _s)
	{
		if (this.m_progressBar != null)
		{
			this.m_progressBar.m_countText.SetText(_s);
		}
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x000AC545 File Offset: 0x000AA945
	public void SetToFinishedState()
	{
		this.m_canTouch = true;
		if (this.m_progressBar != null)
		{
			this.m_progressBar.Destroy();
			this.m_progressBar = null;
		}
		this.m_t2.SetText(this.shareText);
		this.Update();
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x000AC584 File Offset: 0x000AA984
	public void SetToFailedState()
	{
		this.m_canTouch = true;
		if (this.m_progressBar != null)
		{
			this.m_progressBar.Destroy();
			this.m_progressBar = null;
		}
		this.m_t1.SetText(PsStrings.Get(StringID.DEATH_SCREEN_GIF_FAILED));
		this.m_t2.SetText(PsStrings.Get(StringID.DEATH_SCREEN_GIF_TRYAGAIN));
		this.Update();
	}

	// Token: 0x060011BF RID: 4543 RVA: 0x000AC5E8 File Offset: 0x000AA9E8
	public void SetFrame(int _index)
	{
		if (this.m_frames != null)
		{
			this.m_material.mainTexture = this.m_frames[_index];
			this.m_texture = this.m_material.mainTexture;
		}
		else
		{
			this.m_material.mainTexture = ResourceManager.GetTexture(RESOURCE.ScreenshotDefault_Texture2D);
			this.m_texture = this.m_material.mainTexture;
		}
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x000AC650 File Offset: 0x000AAA50
	public override void Step()
	{
		base.Step();
		if (this.m_frames != null)
		{
			if (this.m_time >= this.m_lastTime + this.m_frameDelay)
			{
				this.SetFrame(this.m_currentFrame);
				this.m_currentFrame = ToolBox.getRolledValue(this.m_currentFrame + 1, 0, this.m_frames.Length - 1);
				this.m_lastTime = this.m_time;
			}
			this.m_time += 0.016666668f;
		}
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x000AC6D0 File Offset: 0x000AAAD0
	public override void Destroy()
	{
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		if (this.m_material != null)
		{
			Object.Destroy(this.m_material);
			this.m_material = null;
		}
		if (this.m_overlayMat != null)
		{
			Object.Destroy(this.m_overlayMat);
			this.m_overlayMat = null;
		}
		base.Destroy();
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x000AC73C File Offset: 0x000AAB3C
	public override void DrawHandler(UIComponent _c)
	{
		_c.m_TC.transform.localScale = Vector3.one;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, false);
		if (this.m_texture != null)
		{
			this.m_material.mainTexture = this.m_texture;
		}
		Vector2[] bezierRect = DebugDraw.GetBezierRect(_c.m_actualWidth - this.m_cornerSize * 3f * (float)Screen.height, _c.m_actualHeight - this.m_cornerSize * 3f * (float)Screen.height, this.m_cornerSize * (float)Screen.height, 20, Vector2.zero);
		Vector2[] bezierRect2 = DebugDraw.GetBezierRect(_c.m_actualWidth - this.m_cornerSize * 2.5f * (float)Screen.height, _c.m_actualHeight - this.m_cornerSize * 2.5f * (float)Screen.height, this.m_cornerSize * (float)Screen.height, 20, Vector2.zero);
		UVRect uvrect = new UVRect(0f, 0f, 1f, 1f);
		UVRect uvrect2 = new UVRect(0f, 0f, 1f, 1f);
		uint num = DebugDraw.HexToUint("#0156b2");
		uint num2 = DebugDraw.HexToUint("#2e8599");
		uint num3 = DebugDraw.HexToUint("#1a64a8");
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, bezierRect2, (float)Screen.height * 0.01f, DebugDraw.UIntToColor(num), DebugDraw.UIntToColor(num2), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 1f + Vector3.up * 2.5f, bezierRect, uint.MaxValue, uint.MaxValue, this.m_material, this.m_camera, string.Empty, uvrect);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * -1.5f + Vector3.up * 2.5f, bezierRect, uint.MaxValue, uint.MaxValue, this.m_overlayMat, this.m_camera, string.Empty, uvrect2);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f + Vector3.up * 2.5f, bezierRect, (float)Screen.height * 0.008f, DebugDraw.HexToColor("#41acee"), DebugDraw.HexToColor("#86d9f9"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f + Vector3.up * 2.5f, bezierRect, (float)Screen.height * 0.03f, new Color(1f, 1f, 1f, 0.3f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Inside, true);
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x000ACA20 File Offset: 0x000AAE20
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		if (!this.m_canTouch)
		{
			return;
		}
		TweenS.RemoveAllTweensFromEntity(this.m_TC.p_entity);
		TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale + new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, true);
		this.m_currentScale += new Vector3(0.05f, 0.05f, 0.05f);
		this.m_scaledUp = true;
		base.OnTouchRollIn(_touch, _secondary);
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x000ACAB8 File Offset: 0x000AAEB8
	protected override void OnTouchBegan(TLTouch _touch)
	{
		if (!this.m_canTouch)
		{
			return;
		}
		TweenS.RemoveAllTweensFromEntity(this.m_TC.p_entity);
		TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale + new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, true);
		this.m_currentScale += new Vector3(0.05f, 0.05f, 0.05f);
		this.m_scaledUp = true;
		base.OnTouchBegan(_touch);
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x000ACB4C File Offset: 0x000AAF4C
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		if (!this.m_canTouch)
		{
			return;
		}
		TweenS.RemoveAllTweensFromEntity(this.m_TC.p_entity);
		TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale - new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, true);
		this.m_currentScale -= new Vector3(0.05f, 0.05f, 0.05f);
		this.m_scaledUp = false;
		base.OnTouchRollOut(_touch, _secondary);
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x000ACBE4 File Offset: 0x000AAFE4
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		if (!this.m_canTouch)
		{
			return;
		}
		if (_inside || this.m_scaledUp)
		{
			TweenS.RemoveAllTweensFromEntity(this.m_TC.p_entity);
			TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale - new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, true);
			this.m_currentScale -= new Vector3(0.05f, 0.05f, 0.05f);
			this.m_scaledUp = false;
		}
		base.OnTouchRelease(_touch, _inside);
	}

	// Token: 0x040014AF RID: 5295
	public Texture[] m_frames;

	// Token: 0x040014B0 RID: 5296
	public Material m_material;

	// Token: 0x040014B1 RID: 5297
	public Material m_overlayMat;

	// Token: 0x040014B2 RID: 5298
	public Texture m_texture;

	// Token: 0x040014B3 RID: 5299
	protected float m_cornerSize;

	// Token: 0x040014B4 RID: 5300
	private int m_currentFrame;

	// Token: 0x040014B5 RID: 5301
	private Vector3 m_currentScale;

	// Token: 0x040014B6 RID: 5302
	private bool m_scaledUp;

	// Token: 0x040014B7 RID: 5303
	private PsUIResourceProgressBar m_progressBar;

	// Token: 0x040014B8 RID: 5304
	private bool m_canTouch;

	// Token: 0x040014B9 RID: 5305
	private UIFittedText m_t1;

	// Token: 0x040014BA RID: 5306
	private UIFittedText m_t2;

	// Token: 0x040014BB RID: 5307
	private float m_frameDelay;

	// Token: 0x040014BC RID: 5308
	private float m_time;

	// Token: 0x040014BD RID: 5309
	private float m_lastTime;

	// Token: 0x040014BE RID: 5310
	private string shareText = PsStrings.Get(StringID.DEATH_SCREEN_TAP_TO_SHARE);
}
