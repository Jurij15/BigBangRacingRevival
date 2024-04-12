using System;
using UnityEngine;

// Token: 0x0200059C RID: 1436
public class UIIntPropertyButton : UIVerticalListButton
{
	// Token: 0x060029C0 RID: 10688 RVA: 0x001B7410 File Offset: 0x001B5810
	public UIIntPropertyButton(UIComponent _parent, string _tag, string _label, UIModel _model, string _INTfieldName, int _minValue = 0, int _maxValue = 99999)
		: base(_parent, null, null, _tag, _model, _INTfieldName)
	{
		this.m_minValue = (float)_minValue;
		this.m_maxValue = (float)_maxValue;
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		this.m_currentFloatValue = float.Parse(this.GetValue().ToString());
		string text = Mathf.RoundToInt(this.m_currentFloatValue).ToString();
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x060029C1 RID: 10689 RVA: 0x001B7500 File Offset: 0x001B5900
	protected override void OnTouchDragStart(TLTouch _touch)
	{
		base.OnTouchDragStart(_touch);
		Vector2 vector = _touch.m_currentPosition - _touch.m_startPosition;
		if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
		{
			this.FreezeVerticalScroll(false);
			this.m_changeValue = true;
			this.m_tempVal = (int)this.GetValue();
			this.m_currentFloatValue = float.Parse(this.m_tempVal.ToString());
		}
	}

	// Token: 0x060029C2 RID: 10690 RVA: 0x001B7580 File Offset: 0x001B5980
	protected override void OnTouchMove(TLTouch _touch, bool _inside)
	{
		base.OnTouchMove(_touch, _inside);
		if (this.m_changeValue)
		{
			float x = _touch.m_deltaPosition.x;
			float currentFloatValue = this.m_currentFloatValue;
			float num = Mathf.Max(this.m_minValue, Mathf.Min(this.m_maxValue, currentFloatValue + x / (Mathf.Max(this.m_actualWidth, this.m_maxValue - this.m_minValue) / (this.m_maxValue - this.m_minValue))));
			this.m_currentFloatValue = num;
			this.m_tempVal = Mathf.RoundToInt(num);
			this.m_value.SetText(this.m_tempVal.ToString());
		}
	}

	// Token: 0x060029C3 RID: 10691 RVA: 0x001B7624 File Offset: 0x001B5A24
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (!_touch.m_dragged)
		{
			UITextInput uitextInput = new UITextInput(this.m_label.m_text, this.m_model, this.m_fieldName, 4, true, 128);
			uitextInput.SetText(this.GetValue().ToString());
			uitextInput.Update();
		}
		else
		{
			this.SetValue(this.m_tempVal);
		}
		this.UnfreezeVerticalScroll(false);
		this.m_changeValue = false;
	}

	// Token: 0x060029C4 RID: 10692 RVA: 0x001B76A4 File Offset: 0x001B5AA4
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		float num = float.Parse(_newValue.ToString());
		int num2 = Mathf.RoundToInt(num);
		this.m_value.SetText(num2.ToString());
	}

	// Token: 0x04002EDF RID: 11999
	protected int m_tempVal;

	// Token: 0x04002EE0 RID: 12000
	public float m_minValue;

	// Token: 0x04002EE1 RID: 12001
	public float m_maxValue;

	// Token: 0x04002EE2 RID: 12002
	protected float m_currentFloatValue;

	// Token: 0x04002EE3 RID: 12003
	protected bool m_changeValue;

	// Token: 0x04002EE4 RID: 12004
	public UIText m_label;

	// Token: 0x04002EE5 RID: 12005
	public UIText m_value;
}
