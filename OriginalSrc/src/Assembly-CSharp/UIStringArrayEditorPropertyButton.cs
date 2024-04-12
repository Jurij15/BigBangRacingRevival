using System;

// Token: 0x020005A1 RID: 1441
public class UIStringArrayEditorPropertyButton : UIVerticalListButton
{
	// Token: 0x060029D3 RID: 10707 RVA: 0x001B7E60 File Offset: 0x001B6260
	public UIStringArrayEditorPropertyButton(UIComponent _parent, UIScrollableCanvas _scrollableCanvas, UIPagedCanvas _pagedCanvas, string _tag, string _label, UIModel _model, string _STRINGARRAYfieldName)
		: base(_parent, _scrollableCanvas, _pagedCanvas, _tag, _model, _STRINGARRAYfieldName)
	{
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		string text = "-";
		Array array = (object[])this.GetValue();
		for (int i = 0; i < array.Length; i++)
		{
			if (i == 0)
			{
				text = (string)array.GetValue(i);
			}
			else
			{
				text = text + ", " + (string)array.GetValue(i);
			}
		}
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x060029D4 RID: 10708 RVA: 0x001B7F6C File Offset: 0x001B636C
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		string text = "-";
		Array array = (object[])this.GetValue();
		for (int i = 0; i < array.Length; i++)
		{
			if (i == 0)
			{
				text = (string)array.GetValue(i);
			}
			else
			{
				text = text + ", " + (string)array.GetValue(i);
			}
		}
		this.m_value.SetText(text);
	}

	// Token: 0x060029D5 RID: 10709 RVA: 0x001B7FE0 File Offset: 0x001B63E0
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
				object[] array = (object[])this.GetValue();
				for (int i = 0; i < array.Length; i++)
				{
					new UIStringArrayEditButton(uiverticalList, "ArrayEditButton", this.m_model, this.m_fieldName, i);
				}
				this.m_backButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
				this.m_backButton.SetText("Back", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
				this.m_backButton.SetOrangeColors(true);
				this.m_pagedCanvas.NextPage();
				this.m_pagedCanvas.Update();
			}
		}
	}

	// Token: 0x060029D6 RID: 10710 RVA: 0x001B8128 File Offset: 0x001B6528
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

	// Token: 0x04002EF7 RID: 12023
	public UIText m_label;

	// Token: 0x04002EF8 RID: 12024
	public UIText m_value;

	// Token: 0x04002EF9 RID: 12025
	public UIScrollableCanvas scrollableCanvas;

	// Token: 0x04002EFA RID: 12026
	public PsUIGenericButton m_backButton;
}
