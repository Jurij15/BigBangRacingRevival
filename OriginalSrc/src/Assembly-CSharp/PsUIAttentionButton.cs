using System;
using UnityEngine;

// Token: 0x02000238 RID: 568
public class PsUIAttentionButton : PsUIGenericButton
{
	// Token: 0x060010FB RID: 4347 RVA: 0x000A6F20 File Offset: 0x000A5320
	public PsUIAttentionButton(UIComponent _parent, Vector3 _tween = default(Vector3), float _gradientSize = 0.25f, float _gradientPos = 0.25f, float _cornerSize = 0.005f)
		: base(_parent, _gradientSize, _gradientPos, _cornerSize, "Button")
	{
		if (_tween == default(Vector3))
		{
			_tween..ctor(0.95f, 0.95f, 1f);
		}
		this.m_tweenScale = _tween;
		this.m_diff = _tween - Vector3.one;
		this.m_attentionTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.QuadInOut, Vector3.one, _tween, 0.65f, 0.4f, false);
		TweenS.SetAdditionalTweenProperties(this.m_attentionTween, -1, true, TweenStyle.QuadInOut);
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x000A6FB2 File Offset: 0x000A53B2
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		if (this.m_attentionTween != null)
		{
			TweenS.RemoveComponent(this.m_attentionTween);
			this.m_attentionTween = null;
		}
		base.OnTouchRollIn(_touch, _secondary);
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x000A6FD9 File Offset: 0x000A53D9
	protected override void OnTouchBegan(TLTouch _touch)
	{
		if (this.m_attentionTween != null)
		{
			TweenS.RemoveComponent(this.m_attentionTween);
			this.m_attentionTween = null;
		}
		base.OnTouchBegan(_touch);
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x000A7000 File Offset: 0x000A5400
	private void MiddleTweener(TweenC _c)
	{
		this.m_attentionTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.QuadInOut, Vector3.one, this.m_tweenScale, 0.65f, 0f, false);
		TweenS.SetAdditionalTweenProperties(this.m_attentionTween, -1, true, TweenStyle.QuadInOut);
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x000A7044 File Offset: 0x000A5444
	public void SetNewTween(Vector3 _tween)
	{
		this.m_tweenScale = _tween;
		this.m_diff = _tween - Vector3.one;
		TweenS.RemoveAllTweensFromEntity(this.m_TC.p_entity);
		this.m_attentionTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.QuadInOut, Vector3.one, _tween, 0.65f, 0f, false);
		TweenS.SetAdditionalTweenProperties(this.m_attentionTween, -1, true, TweenStyle.QuadInOut);
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x000A70AC File Offset: 0x000A54AC
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		if (this.m_scaledUp)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.Hide();
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale - new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_touchScaleTween, new TweenEventDelegate(this.MiddleTweener));
			this.m_currentScale -= new Vector3(0.05f, 0.05f, 0.05f);
			this.m_scaledUp = false;
		}
		if (this.m_end)
		{
			return;
		}
		if (_secondary)
		{
			this.m_end = true;
			this.HighlightSecondary(false);
		}
		else
		{
			this.m_end = true;
			this.Highlight(false);
		}
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x000A719C File Offset: 0x000A559C
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (!_inside && this.m_attentionTween == null)
		{
			this.m_attentionTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.QuadInOut, Vector3.one, this.m_tweenScale, 0.65f, 0f, false);
			TweenS.SetAdditionalTweenProperties(this.m_attentionTween, -1, true, TweenStyle.QuadInOut);
		}
	}

	// Token: 0x040013F1 RID: 5105
	private TweenC m_attentionTween;

	// Token: 0x040013F2 RID: 5106
	private Vector3 m_diff;

	// Token: 0x040013F3 RID: 5107
	private Vector3 m_tweenScale;
}
