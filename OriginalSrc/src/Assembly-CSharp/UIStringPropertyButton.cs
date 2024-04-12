using System;

// Token: 0x020005A4 RID: 1444
public class UIStringPropertyButton : UIVerticalListButton
{
	// Token: 0x060029E2 RID: 10722 RVA: 0x001B877C File Offset: 0x001B6B7C
	public UIStringPropertyButton(UIComponent _parent, string _tag, string _label, UIModel _model, string _STRINGfieldName)
		: base(_parent, null, null, _tag, _model, _STRINGfieldName)
	{
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		string text = "-";
		if ((string)this.GetValue() != null)
		{
			text = (string)this.GetValue();
		}
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x060029E3 RID: 10723 RVA: 0x001B884C File Offset: 0x001B6C4C
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (_inside)
		{
			UITextInput uitextInput = new UITextInput(this.m_label.m_text, this.m_model, this.m_fieldName, 1, true, 128);
			uitextInput.SetText((string)this.GetValue());
			uitextInput.Update();
		}
	}

	// Token: 0x060029E4 RID: 10724 RVA: 0x001B88A2 File Offset: 0x001B6CA2
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		this.m_value.SetText((string)_newValue);
	}

	// Token: 0x04002F0B RID: 12043
	public UIText m_label;

	// Token: 0x04002F0C RID: 12044
	public UIText m_value;
}
