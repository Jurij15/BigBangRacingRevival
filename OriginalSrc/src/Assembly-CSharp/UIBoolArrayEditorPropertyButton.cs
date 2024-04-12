using System;

// Token: 0x02000594 RID: 1428
public class UIBoolArrayEditorPropertyButton : UIVerticalListButton
{
	// Token: 0x06002994 RID: 10644 RVA: 0x001B58DC File Offset: 0x001B3CDC
	public UIBoolArrayEditorPropertyButton(UIComponent _parent, UIScrollableCanvas _scrollableCanvas, UIPagedCanvas _pagedCanvas, string _tag, string _label, UIModel _model, string _BOOLARRAYfieldName)
		: base(_parent, _scrollableCanvas, _pagedCanvas, _tag, _model, _BOOLARRAYfieldName)
	{
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		string text = "-";
		bool[] array = (bool[])this.GetValue();
		for (int i = 0; i < array.Length; i++)
		{
			if (i == 0)
			{
				text = array.GetValue(i).ToString();
			}
			else
			{
				text = text + ", " + array.GetValue(i).ToString();
			}
		}
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x06002995 RID: 10645 RVA: 0x001B59E4 File Offset: 0x001B3DE4
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		string text = "-";
		bool[] array = (bool[])this.GetValue();
		for (int i = 0; i < array.Length; i++)
		{
			if (i == 0)
			{
				text = array.GetValue(i).ToString();
			}
			else
			{
				text = text + ", " + array.GetValue(i).ToString();
			}
		}
		this.m_value.SetText(text);
	}

	// Token: 0x06002996 RID: 10646 RVA: 0x001B5A54 File Offset: 0x001B3E54
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
				bool[] array = (bool[])this.GetValue();
				for (int i = 0; i < array.Length; i++)
				{
					new UIBoolPropertyButton(uiverticalList, "BoolEditButton", "[" + i + "]", this.m_model, this.m_fieldName, new string[] { "No", "Yes" }, i);
				}
				this.m_backButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
				this.m_backButton.SetText("Back", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
				this.m_backButton.SetOrangeColors(true);
				this.m_pagedCanvas.NextPage();
				this.m_pagedCanvas.Update();
			}
		}
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x001B5BC4 File Offset: 0x001B3FC4
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

	// Token: 0x04002EA7 RID: 11943
	public UIText m_label;

	// Token: 0x04002EA8 RID: 11944
	public UIText m_value;

	// Token: 0x04002EA9 RID: 11945
	public UIScrollableCanvas scrollableCanvas;

	// Token: 0x04002EAA RID: 11946
	public PsUIGenericButton m_backButton;
}
