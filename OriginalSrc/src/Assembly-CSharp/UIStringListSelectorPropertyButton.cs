using System;
using System.Collections.Generic;

// Token: 0x020005A3 RID: 1443
public class UIStringListSelectorPropertyButton : UIVerticalListButton
{
	// Token: 0x060029DE RID: 10718 RVA: 0x001B845C File Offset: 0x001B685C
	public UIStringListSelectorPropertyButton(UIComponent _parent, UIScrollableCanvas _scrollableCanvas, UIPagedCanvas _pagedCanvas, string _tag, string _label, UIModel _model, string _selectedSTRINGLISTfieldName, string[] _selectionOptions, string[] _selectionLabels)
		: base(_parent, _scrollableCanvas, _pagedCanvas, _tag, _model, _selectedSTRINGLISTfieldName)
	{
		this.m_selectionOptions = _selectionOptions;
		this.m_selectionLabels = _selectionLabels;
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		string text = "-";
		List<string> list = (List<string>)this.GetValue();
		for (int i = 0; i < list.Count; i++)
		{
			if (i == 0)
			{
				text = list[i];
			}
			else
			{
				text = text + ", " + list[i];
			}
		}
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x060029DF RID: 10719 RVA: 0x001B856C File Offset: 0x001B696C
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		string text = "-";
		List<string> list = (List<string>)this.GetValue();
		for (int i = 0; i < list.Count; i++)
		{
			if (i == 0)
			{
				text = list[i];
			}
			else
			{
				text = text + ", " + list[i];
			}
		}
		this.m_value.SetText(text);
	}

	// Token: 0x060029E0 RID: 10720 RVA: 0x001B85D4 File Offset: 0x001B69D4
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		if (!this.m_pagedCanvas.IsChangingPage())
		{
			base.OnTouchRelease(_touch, _inside);
			if (_inside)
			{
				this.scrollableCanvas = new UIScrollableCanvas(this.m_scrollableCanvas.m_parent, string.Empty);
				this.scrollableCanvas.SetWidth(1f, RelativeTo.ParentWidth);
				this.scrollableCanvas.SetHeight(1f, RelativeTo.ParentHeight);
				UIVerticalList uiverticalList = new UIVerticalList(this.scrollableCanvas, "VerticalArea");
				uiverticalList.SetVerticalAlign(1f);
				UIText uitext = new UIText(uiverticalList, false, string.Empty, this.m_label.m_text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, "#000000", null);
				for (int i = 0; i < this.m_selectionOptions.Length; i++)
				{
					new UIStringListSelectorButton(uiverticalList, "ArraySelectorButton", this.m_model, this.m_fieldName, this.m_selectionOptions[i], this.m_selectionLabels[i], false);
				}
				this.m_backButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
				this.m_backButton.SetText("Back", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
				this.m_backButton.SetOrangeColors(true);
				this.m_pagedCanvas.NextPage();
				this.m_pagedCanvas.Update();
			}
		}
	}

	// Token: 0x060029E1 RID: 10721 RVA: 0x001B8724 File Offset: 0x001B6B24
	public override void Step()
	{
		if (this.m_backButton != null && this.m_backButton.m_hit)
		{
			this.scrollableCanvas.Destroy();
			this.scrollableCanvas = null;
			this.m_pagedCanvas.PreviousPage();
			this.m_pagedCanvas.Update();
		}
		base.Step();
	}

	// Token: 0x04002F05 RID: 12037
	public UIText m_label;

	// Token: 0x04002F06 RID: 12038
	public UIText m_value;

	// Token: 0x04002F07 RID: 12039
	public UIScrollableCanvas scrollableCanvas;

	// Token: 0x04002F08 RID: 12040
	public PsUIGenericButton m_backButton;

	// Token: 0x04002F09 RID: 12041
	private string[] m_selectionOptions;

	// Token: 0x04002F0A RID: 12042
	private string[] m_selectionLabels;
}
