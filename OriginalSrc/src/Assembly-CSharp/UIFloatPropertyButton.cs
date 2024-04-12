using System;
using UnityEngine;

// Token: 0x02000596 RID: 1430
public class UIFloatPropertyButton : UIVerticalListButton
{
	// Token: 0x0600299E RID: 10654 RVA: 0x001B60D4 File Offset: 0x001B44D4
	public UIFloatPropertyButton(UIComponent _parent, string _tag, string _label, UIModel _model, string _FLOATfieldName, float _minValue = 0f, float _maxValue = 99999f)
		: base(_parent, null, null, _tag, _model, _FLOATfieldName)
	{
		this.m_minValue = _minValue;
		this.m_maxValue = _maxValue;
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		string text = ((float)this.GetValue()).ToString("0.000");
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x0600299F RID: 10655 RVA: 0x001B61AC File Offset: 0x001B45AC
	protected override void OnTouchDragStart(TLTouch _touch)
	{
		base.OnTouchDragStart(_touch);
		Vector2 vector = _touch.m_currentPosition - _touch.m_startPosition;
		if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
		{
			this.FreezeVerticalScroll(false);
			this.m_changeValue = true;
			this.m_tempVal = (float)this.GetValue();
		}
	}

	// Token: 0x060029A0 RID: 10656 RVA: 0x001B6210 File Offset: 0x001B4610
	protected override void OnTouchMove(TLTouch _touch, bool _inside)
	{
		base.OnTouchMove(_touch, _inside);
		if (this.m_changeValue)
		{
			float x = _touch.m_deltaPosition.x;
			float tempVal = this.m_tempVal;
			this.m_tempVal = Mathf.Max(this.m_minValue, Mathf.Min(this.m_maxValue, tempVal + x / (Mathf.Max(this.m_actualWidth, (this.m_maxValue - this.m_minValue) * 10f) / (this.m_maxValue - this.m_minValue))));
			this.m_value.SetText(this.m_tempVal.ToString("0.000"));
		}
	}

	// Token: 0x060029A1 RID: 10657 RVA: 0x001B62AC File Offset: 0x001B46AC
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (!_touch.m_dragged)
		{
			UITextInput uitextInput = new UITextInput(this.m_label.m_text, this.m_model, this.m_fieldName, 2, true, 128);
			uitextInput.SetText(this.GetValue().ToString());
			uitextInput.Update();
		}
		else
		{
			this.SetValue(float.Parse(this.m_tempVal.ToString("0.000")));
		}
		this.UnfreezeVerticalScroll(false);
		this.m_changeValue = false;
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x001B633C File Offset: 0x001B473C
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		float num = float.Parse(_newValue.ToString());
		this.m_value.SetText(num.ToString("0.000"));
	}

	// Token: 0x04002EB6 RID: 11958
	protected float m_tempVal;

	// Token: 0x04002EB7 RID: 11959
	public float m_minValue;

	// Token: 0x04002EB8 RID: 11960
	public float m_maxValue;

	// Token: 0x04002EB9 RID: 11961
	protected bool m_changeValue;

	// Token: 0x04002EBA RID: 11962
	public UIText m_label;

	// Token: 0x04002EBB RID: 11963
	public UIText m_value;
}
