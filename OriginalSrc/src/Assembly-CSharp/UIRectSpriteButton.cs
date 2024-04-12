using System;
using UnityEngine;

// Token: 0x02000399 RID: 921
public class UIRectSpriteButton : UIFittedSprite
{
	// Token: 0x06001A62 RID: 6754 RVA: 0x00041426 File Offset: 0x0003F826
	public UIRectSpriteButton(UIComponent _parent, string _tag, SpriteSheet _spriteSheet, Frame _frame, bool _convertToPrefab = true, bool _noSound = false)
		: base(_parent, true, _tag, _spriteSheet, _frame, _convertToPrefab, true)
	{
		this.m_noSound = _noSound;
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x0004144A File Offset: 0x0003F84A
	public override void DrawHandler(UIComponent _c)
	{
		base.DrawHandler(_c);
	}

	// Token: 0x06001A64 RID: 6756 RVA: 0x00041453 File Offset: 0x0003F853
	public void SetSound(string _sound)
	{
		this.m_sound = _sound;
	}

	// Token: 0x06001A65 RID: 6757 RVA: 0x0004145C File Offset: 0x0003F85C
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollIn(_touch, _secondary);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
		}
	}

	// Token: 0x06001A66 RID: 6758 RVA: 0x000414CC File Offset: 0x0003F8CC
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
		}
	}

	// Token: 0x06001A67 RID: 6759 RVA: 0x0004153C File Offset: 0x0003F93C
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollOut(_touch, _secondary);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
		}
	}

	// Token: 0x06001A68 RID: 6760 RVA: 0x000415AC File Offset: 0x0003F9AC
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
			if (!this.m_noSound && _inside && this.m_hit && this.m_sound != null)
			{
				SoundS.PlaySingleShot(this.m_sound, Vector3.zero, 1f);
			}
		}
	}

	// Token: 0x04001CF2 RID: 7410
	private bool m_noSound;

	// Token: 0x04001CF3 RID: 7411
	private string m_sound = "/UI/ButtonNormal";
}
