using System;

// Token: 0x0200059D RID: 1437
public class UIListPropertyButton : UIVerticalListButton
{
	// Token: 0x060029C5 RID: 10693 RVA: 0x001B76DC File Offset: 0x001B5ADC
	public UIListPropertyButton(UIComponent _parent, string _tag, string _label, UIModel _model, string _fieldName, Type _selectorClassType)
		: base(_parent, null, null, _tag, _model, _fieldName)
	{
		this.m_selectorClassType = _selectorClassType;
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		string text = "-";
		if (this.GetValue() != null)
		{
			text = this.GetValue().ToString();
		}
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x060029C6 RID: 10694 RVA: 0x001B77B0 File Offset: 0x001B5BB0
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		string text = "-";
		if (_newValue != null)
		{
			text = _newValue.ToString();
		}
		this.m_value.SetText(text);
	}

	// Token: 0x060029C7 RID: 10695 RVA: 0x001B77DC File Offset: 0x001B5BDC
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (_inside && !_touch.m_dragged && this.m_selectorClassType != null)
		{
			object[] array = new object[] { this.m_model, this.m_fieldName };
			Activator.CreateInstance(this.m_selectorClassType, array);
		}
	}

	// Token: 0x04002EE6 RID: 12006
	public UIText m_label;

	// Token: 0x04002EE7 RID: 12007
	public UIText m_value;

	// Token: 0x04002EE8 RID: 12008
	public Type m_selectorClassType;
}
