using System;

// Token: 0x02000592 RID: 1426
public class UIArrayPropertyButton : UIVerticalListButton
{
	// Token: 0x0600298B RID: 10635 RVA: 0x001B54C4 File Offset: 0x001B38C4
	public UIArrayPropertyButton(UIComponent _parent, UIScrollableCanvas _scrollableCanvas, UIPagedCanvas _pagedCanvas, string _tag, string _label, UIModel _model, string _fieldName, object[] _values)
		: base(_parent, _scrollableCanvas, _pagedCanvas, _tag, _model, _fieldName)
	{
		this.m_values = _values;
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

	// Token: 0x0600298C RID: 10636 RVA: 0x001B559C File Offset: 0x001B399C
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		string text = "-";
		if (_newValue != null)
		{
			text = _newValue.ToString();
		}
		this.m_value.SetText(text);
	}

	// Token: 0x0600298D RID: 10637 RVA: 0x001B55C8 File Offset: 0x001B39C8
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		if (!this.m_pagedCanvas.IsChangingPage())
		{
			base.OnTouchRelease(_touch, _inside);
			if (_inside)
			{
				UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(this.m_scrollableCanvas.m_parent, string.Empty);
				uiscrollableCanvas.SetWidth(1f, RelativeTo.ParentWidth);
				uiscrollableCanvas.SetHeight(1f, RelativeTo.ParentHeight);
				UIVerticalList uiverticalList = new UIVerticalList(uiscrollableCanvas, "VerticalArea");
				uiverticalList.SetVerticalAlign(1f);
				UIText uitext = new UIText(uiverticalList, false, string.Empty, this.m_label.m_text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, "#000000", null);
				for (int i = 0; i < this.m_values.Length; i++)
				{
					new UIArraySelectionButton(uiverticalList, uiscrollableCanvas, this.m_pagedCanvas, "ListSelectorButton", this.m_model, this.m_fieldName, this.m_values[i].ToString(), this.m_values[i]);
				}
				this.m_pagedCanvas.NextPage();
				this.m_pagedCanvas.Update();
			}
		}
	}

	// Token: 0x04002E99 RID: 11929
	protected object[] m_values;

	// Token: 0x04002E9A RID: 11930
	public UIText m_label;

	// Token: 0x04002E9B RID: 11931
	public UIText m_value;
}
