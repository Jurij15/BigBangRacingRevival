using System;
using UnityEngine;

// Token: 0x02000595 RID: 1429
public class UIBoolPropertyButton : UIVerticalListButton
{
	// Token: 0x06002998 RID: 10648 RVA: 0x001B5C1C File Offset: 0x001B401C
	public UIBoolPropertyButton(UIComponent _parent, string _tag, string _label, UIModel _model, string _BOOLfieldName, string[] _valueLabels, int _arrayIndex = -1)
		: base(_parent, null, null, _tag, _model, _BOOLfieldName)
	{
		this.m_minValue = 0f;
		this.m_maxValue = 1f;
		this.m_valueLabels = _valueLabels;
		this.m_arrayIndex = _arrayIndex;
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		if (this.m_arrayIndex == -1)
		{
			this.m_currentFloatValue = ((!(bool)this.GetValue()) ? 0.4f : 0.6f);
		}
		else
		{
			this.m_currentFloatValue = ((!((bool[])this.GetValue())[this.m_arrayIndex]) ? 0.4f : 0.6f);
		}
		int num = Mathf.RoundToInt(this.m_currentFloatValue);
		string text = this.m_valueLabels[num];
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x001B5D66 File Offset: 0x001B4166
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		this.m_changeValue = true;
		this.m_changeValueByDragging = false;
		this.m_valueChangedByDragging = false;
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x001B5D84 File Offset: 0x001B4184
	protected override void OnTouchDragStart(TLTouch _touch)
	{
		base.OnTouchDragStart(_touch);
		Vector2 vector = _touch.m_currentPosition - _touch.m_startPosition;
		if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
		{
			this.FreezeVerticalScroll(false);
			this.m_changeValueByDragging = true;
			if (this.m_arrayIndex == -1)
			{
				this.m_tempVal = (bool)this.GetValue();
			}
			else
			{
				this.m_tempVal = ((bool[])this.GetValue())[this.m_arrayIndex];
			}
			this.m_currentFloatValue = ((!this.m_tempVal) ? 0.4f : 0.6f);
		}
		else
		{
			this.m_changeValue = false;
		}
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x001B5E3C File Offset: 0x001B423C
	protected override void OnTouchMove(TLTouch _touch, bool _inside)
	{
		base.OnTouchMove(_touch, _inside);
		if (this.m_changeValueByDragging)
		{
			bool flag;
			if (this.m_arrayIndex == -1)
			{
				flag = (bool)this.GetValue();
			}
			else
			{
				flag = ((bool[])this.GetValue())[this.m_arrayIndex];
			}
			float x = _touch.m_deltaPosition.x;
			float currentFloatValue = this.m_currentFloatValue;
			float num = Mathf.Max(this.m_minValue, Mathf.Min(this.m_maxValue, currentFloatValue + x / (Mathf.Max(this.m_actualWidth, this.m_maxValue - this.m_minValue) / (this.m_maxValue - this.m_minValue))));
			this.m_currentFloatValue = Mathf.Max(0.4f, Mathf.Min(0.6f, num));
			int num2 = Mathf.RoundToInt(num);
			this.m_tempVal = num2 == 1;
			this.m_value.SetText(this.m_valueLabels[num2]);
			if (flag != this.m_tempVal)
			{
				this.m_valueChangedByDragging = true;
			}
		}
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x001B5F38 File Offset: 0x001B4338
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (!_touch.m_dragged && this.m_changeValue && !this.m_valueChangedByDragging)
		{
			bool flag;
			if (this.m_arrayIndex == -1)
			{
				flag = (bool)this.GetValue();
				flag = !flag;
				this.SetValue(flag);
			}
			else
			{
				bool[] array = (bool[])this.GetValue();
				flag = array[this.m_arrayIndex];
				flag = !flag;
				array[this.m_arrayIndex] = flag;
				this.SetValue(array);
			}
			int num = ((!flag) ? 0 : 1);
			this.m_value.SetText(this.m_valueLabels[num]);
		}
		else if (this.m_valueChangedByDragging)
		{
			if (this.m_arrayIndex == -1)
			{
				this.SetValue(this.m_tempVal);
			}
			else
			{
				bool[] array2 = (bool[])this.GetValue();
				array2[this.m_arrayIndex] = this.m_tempVal;
				this.SetValue(array2);
			}
		}
		this.UnfreezeVerticalScroll(false);
		this.m_changeValue = false;
		this.m_changeValueByDragging = false;
		this.m_valueChangedByDragging = false;
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x001B6058 File Offset: 0x001B4458
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		if (this.m_arrayIndex == -1)
		{
			int num = ((!bool.Parse(_newValue.ToString())) ? 0 : 1);
			this.m_value.SetText(this.m_valueLabels[num]);
		}
		else
		{
			int num2 = ((!((bool[])this.GetValue())[this.m_arrayIndex]) ? 0 : 1);
			this.m_value.SetText(this.m_valueLabels[num2]);
		}
	}

	// Token: 0x04002EAB RID: 11947
	protected string[] m_valueLabels;

	// Token: 0x04002EAC RID: 11948
	public float m_minValue;

	// Token: 0x04002EAD RID: 11949
	public float m_maxValue;

	// Token: 0x04002EAE RID: 11950
	protected bool m_tempVal;

	// Token: 0x04002EAF RID: 11951
	protected float m_currentFloatValue;

	// Token: 0x04002EB0 RID: 11952
	protected bool m_changeValue;

	// Token: 0x04002EB1 RID: 11953
	protected bool m_changeValueByDragging;

	// Token: 0x04002EB2 RID: 11954
	protected bool m_valueChangedByDragging;

	// Token: 0x04002EB3 RID: 11955
	public UIText m_label;

	// Token: 0x04002EB4 RID: 11956
	public UIText m_value;

	// Token: 0x04002EB5 RID: 11957
	private int m_arrayIndex;
}
