using System;

// Token: 0x020005A0 RID: 1440
public class UIStringArrayEditButton : UIVerticalListButton
{
	// Token: 0x060029D0 RID: 10704 RVA: 0x001B7C70 File Offset: 0x001B6070
	public UIStringArrayEditButton(UIComponent _parent, string _tag, UIModel _model, string _STRINGARRAYfieldName, int _arrayTargetIndex)
		: base(_parent, null, null, _tag, _model, _STRINGARRAYfieldName)
	{
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		this.m_arrayTargetIndex = _arrayTargetIndex;
		string text = "-";
		if (((object[])this.GetValue())[_arrayTargetIndex] != null)
		{
			text = ((object[])this.GetValue())[_arrayTargetIndex].ToString();
		}
		this.m_label = new UIText(this, false, _tag, "[" + _arrayTargetIndex + "]", "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x060029D1 RID: 10705 RVA: 0x001B7D68 File Offset: 0x001B6168
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (_inside)
		{
			UITextInput uitextInput = new UITextInput(string.Concat(new object[]
			{
				this.m_label.m_text,
				"[",
				this.m_arrayTargetIndex,
				"]"
			}), this.m_model, this.m_fieldName, this.m_arrayTargetIndex, 1, true, 128);
			string text = "-";
			if (((object[])this.GetValue())[this.m_arrayTargetIndex] != null)
			{
				text = ((object[])this.GetValue())[this.m_arrayTargetIndex].ToString();
			}
			uitextInput.SetText(text);
			uitextInput.Update();
		}
	}

	// Token: 0x060029D2 RID: 10706 RVA: 0x001B7E1C File Offset: 0x001B621C
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		string text = "-";
		if (((object[])_newValue)[this.m_arrayTargetIndex] != null)
		{
			text = ((object[])_newValue)[this.m_arrayTargetIndex].ToString();
		}
		this.m_value.SetText(text);
	}

	// Token: 0x04002EF4 RID: 12020
	public UIText m_label;

	// Token: 0x04002EF5 RID: 12021
	public UIText m_value;

	// Token: 0x04002EF6 RID: 12022
	public int m_arrayTargetIndex;
}
