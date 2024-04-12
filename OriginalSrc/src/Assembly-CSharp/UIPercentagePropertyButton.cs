using System;
using UnityEngine;

// Token: 0x0200059F RID: 1439
public class UIPercentagePropertyButton : UIVerticalListButton
{
	// Token: 0x060029CB RID: 10699 RVA: 0x001B7A14 File Offset: 0x001B5E14
	public UIPercentagePropertyButton(UIComponent _parent, UIScrollableCanvas _scrollableCanvas, UIPagedCanvas _pagedCanvas, string _tag, string _label, UIModel _model, string _FLOATfieldName, float _minValue, float _maxValue)
		: base(_parent, _scrollableCanvas, _pagedCanvas, _tag, _model, _FLOATfieldName)
	{
		this.m_minValue = _minValue;
		this.m_maxValue = _maxValue;
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		float num = (float)this.GetValue();
		string text = ((num - this.m_minValue) / (this.m_maxValue - this.m_minValue)).ToString("0%");
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x060029CC RID: 10700 RVA: 0x001B7B08 File Offset: 0x001B5F08
	protected override void OnTouchDragStart(TLTouch _touch)
	{
		base.OnTouchDragStart(_touch);
		Vector2 vector = _touch.m_currentPosition - _touch.m_startPosition;
		if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
		{
			this.FreezeVerticalScroll(false);
			this.m_changeValue = true;
		}
	}

	// Token: 0x060029CD RID: 10701 RVA: 0x001B7B5C File Offset: 0x001B5F5C
	protected override void OnTouchMove(TLTouch _touch, bool _inside)
	{
		base.OnTouchMove(_touch, _inside);
		if (this.m_changeValue)
		{
			float x = _touch.m_deltaPosition.x;
			float num = (float)this.GetValue();
			float num2 = Mathf.Max(this.m_minValue, Mathf.Min(this.m_maxValue, num + x / (Mathf.Max(this.m_actualWidth, this.m_maxValue - this.m_minValue) / (this.m_maxValue - this.m_minValue))));
			this.SetValue(num2);
			float num3 = (num2 - this.m_minValue) / (this.m_maxValue - this.m_minValue);
			this.m_value.SetText(num3.ToString("0%"));
		}
	}

	// Token: 0x060029CE RID: 10702 RVA: 0x001B7C0F File Offset: 0x001B600F
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		this.UnfreezeVerticalScroll(false);
		this.m_changeValue = false;
	}

	// Token: 0x060029CF RID: 10703 RVA: 0x001B7C28 File Offset: 0x001B6028
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		float num = float.Parse(_newValue.ToString());
		float num2 = (num - this.m_minValue) / (this.m_maxValue - this.m_minValue);
		this.m_value.SetText(num2.ToString("0%"));
	}

	// Token: 0x04002EEF RID: 12015
	public float m_minValue;

	// Token: 0x04002EF0 RID: 12016
	public float m_maxValue;

	// Token: 0x04002EF1 RID: 12017
	protected bool m_changeValue;

	// Token: 0x04002EF2 RID: 12018
	public UIText m_label;

	// Token: 0x04002EF3 RID: 12019
	public UIText m_value;
}
