using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200059B RID: 1435
public class UIHashtableFloatPropertyButton : UIVerticalListButton
{
	// Token: 0x060029B8 RID: 10680 RVA: 0x001B7048 File Offset: 0x001B5448
	public UIHashtableFloatPropertyButton(UIComponent _parent, string _tag, string _label, UIModel _model, string _fieldName, string _FLOATfieldName, float _minValue = 0f, float _maxValue = 99999f)
		: base(_parent, null, null, _tag, _model, _fieldName)
	{
		this.m_minValue = _minValue;
		this.m_maxValue = _maxValue;
		this.m_floatFieldName = _FLOATfieldName;
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		this.m_upgrades = (Hashtable)this.GetValue();
		string text = Convert.ToSingle(this.m_upgrades[this.m_floatFieldName]).ToString("0.000");
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x060029B9 RID: 10681 RVA: 0x001B7144 File Offset: 0x001B5544
	protected override void OnTouchDragStart(TLTouch _touch)
	{
		base.OnTouchDragStart(_touch);
		Vector2 vector = _touch.m_currentPosition - _touch.m_startPosition;
		if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
		{
			this.FreezeVerticalScroll(false);
			this.m_changeValue = true;
			this.m_tempVal = (float)((Hashtable)this.GetValue())[this.m_floatFieldName];
		}
	}

	// Token: 0x060029BA RID: 10682 RVA: 0x001B71B8 File Offset: 0x001B55B8
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

	// Token: 0x060029BB RID: 10683 RVA: 0x001B7254 File Offset: 0x001B5654
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (!_touch.m_dragged)
		{
			UITextInput uitextInput = new UITextInput(this.m_label.m_text, new Action<string>(this.DoneCallback), new Action(this.CancelCallback), new Action<string>(this.KeyPressedCallback), 2, false, 64);
			uitextInput.SetText(Convert.ToSingle(this.m_upgrades[this.m_floatFieldName]).ToString("0.000"));
			uitextInput.Update();
		}
		else
		{
			this.m_upgrades = (Hashtable)this.GetValue();
			this.m_upgrades[this.m_floatFieldName] = float.Parse(this.m_tempVal.ToString("0.000"));
			this.SetValue(this.m_upgrades);
		}
		this.UnfreezeVerticalScroll(false);
		this.m_changeValue = false;
	}

	// Token: 0x060029BC RID: 10684 RVA: 0x001B7338 File Offset: 0x001B5738
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		this.m_upgrades = (Hashtable)_newValue;
		float num = Convert.ToSingle(this.m_upgrades[this.m_floatFieldName]);
		this.m_value.SetText(num.ToString("0.000"));
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x001B7380 File Offset: 0x001B5780
	private void DoneCallback(string _value)
	{
		Debug.Log("DONE: " + _value, null);
		this.m_upgrades = (Hashtable)this.GetValue();
		float num = float.Parse(_value);
		this.m_upgrades[this.m_floatFieldName] = num;
		this.SetValue(this.m_upgrades);
		this.m_value.SetText(num.ToString("0.000"));
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x001B73F0 File Offset: 0x001B57F0
	private void KeyPressedCallback(string _value)
	{
		Debug.Log("PRESSED: " + _value, null);
	}

	// Token: 0x060029BF RID: 10687 RVA: 0x001B7403 File Offset: 0x001B5803
	private void CancelCallback()
	{
		Debug.Log("CANCEL", null);
	}

	// Token: 0x04002ED6 RID: 11990
	protected float m_tempVal;

	// Token: 0x04002ED7 RID: 11991
	public float m_minValue;

	// Token: 0x04002ED8 RID: 11992
	public float m_maxValue;

	// Token: 0x04002ED9 RID: 11993
	protected bool m_changeValue;

	// Token: 0x04002EDA RID: 11994
	protected string m_floatFieldName;

	// Token: 0x04002EDB RID: 11995
	private Hashtable m_upgrades;

	// Token: 0x04002EDC RID: 11996
	public UIText m_label;

	// Token: 0x04002EDD RID: 11997
	public UIText m_value;

	// Token: 0x04002EDE RID: 11998
	private UIModel m_keyboardModel;
}
