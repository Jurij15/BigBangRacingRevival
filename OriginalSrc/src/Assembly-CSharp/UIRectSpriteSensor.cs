using System;
using UnityEngine;

// Token: 0x0200039A RID: 922
public class UIRectSpriteSensor : UIFittedSprite
{
	// Token: 0x06001A69 RID: 6761 RVA: 0x00126CD4 File Offset: 0x001250D4
	public UIRectSpriteSensor(UIComponent _parent, string _tag, SpriteSheet _spriteSheet, Frame _frame, bool _convertToPrefab = true)
		: base(_parent, true, _tag, _spriteSheet, _frame, _convertToPrefab, true)
	{
		this.m_TAC.m_allowSecondary = true;
		this.m_TAC.m_allowPrimary = false;
		this.m_TAC.m_cancelOtherTouches = false;
	}

	// Token: 0x06001A6A RID: 6762 RVA: 0x00126D0C File Offset: 0x0012510C
	public override void Update()
	{
		if (this.m_spriteTC.forceScale)
		{
			this.m_spriteTC.forcedScale = Vector3.one;
		}
		else
		{
			this.m_spriteTC.transform.localScale = Vector3.one;
		}
		base.Update();
	}

	// Token: 0x06001A6B RID: 6763 RVA: 0x00126D5C File Offset: 0x0012515C
	public override void DrawHandler(UIComponent _c)
	{
		TweenS.RemoveAllTweensFromEntity(this.m_spriteTC.p_entity);
		if (this.m_highlight || this.m_highlightSecondary)
		{
			TweenS.AddTransformTween(this.m_spriteTC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(0.8f, 0.8f, 1f), 0.1f, 0f, true);
		}
		else
		{
			TweenS.AddTransformTween(this.m_spriteTC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, true);
		}
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x00126DF0 File Offset: 0x001251F0
	protected override void Highlight(bool _value)
	{
		base.Highlight(_value);
		this.d_Draw(this);
	}

	// Token: 0x06001A6D RID: 6765 RVA: 0x00126E05 File Offset: 0x00125205
	protected override void HighlightSecondary(bool _value)
	{
		base.HighlightSecondary(_value);
		this.d_Draw(this);
	}

	// Token: 0x06001A6E RID: 6766 RVA: 0x00126E1A File Offset: 0x0012521A
	protected override void OnTouchStationary(TLTouch _touch, bool _inside)
	{
		if (_inside && !this.m_began && !this.m_highlightSecondary)
		{
			this.OnTouchRollIn(_touch, true);
		}
		else
		{
			base.HighlightSecondary(_inside);
		}
	}
}
