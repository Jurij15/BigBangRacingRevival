using System;

// Token: 0x02000268 RID: 616
public class PsUIGenericInputField : PsUIInputTextField
{
	// Token: 0x06001271 RID: 4721 RVA: 0x000B6CC6 File Offset: 0x000B50C6
	public PsUIGenericInputField()
		: this(null)
	{
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x000B6CCF File Offset: 0x000B50CF
	public PsUIGenericInputField(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x000B6CD8 File Offset: 0x000B50D8
	public PsUIGenericInputField(UIComponent _parent, float _width, float _height, RelativeTo _widthRelativeTo, RelativeTo _heightRelativeTo, cpBB _margins, bool _hideTitle)
		: base(_parent, _width, _height, _widthRelativeTo, _heightRelativeTo, _margins, _hideTitle, string.Empty)
	{
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x000B6CFC File Offset: 0x000B50FC
	protected override void ConstructUI()
	{
		base.SetMinMaxCharacterCount(3, 22);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList.SetWidth(this.m_widthRatio, this.m_textWidthRelativeTo);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		string text = string.Empty;
		if (!this.m_hideTitle)
		{
			if (this.m_parent is PsUIGenericInputField && !(this.m_parent as PsUIGenericInputField).m_hideTitle)
			{
				text = (this.m_parent as PsUIGenericInputField).m_title.m_text;
			}
			this.m_title = new UIText(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), 0.045f, RelativeTo.ScreenHeight, null, null);
			this.m_title.SetColor("#60caf5", null);
			this.m_title.SetHorizontalAlign(0.07f);
		}
		this.m_textArea = new UICanvas(uiverticalList, true, string.Empty, null, string.Empty);
		this.m_textArea.SetHeight(this.m_heightRatio, this.m_textHeightRelativeTo);
		this.m_textArea.SetWidth(this.m_widthRatio, this.m_textWidthRelativeTo);
		this.m_textArea.SetMargins(this.m_fieldMargins.l, this.m_fieldMargins.r, this.m_fieldMargins.t, this.m_fieldMargins.b, RelativeTo.ScreenHeight);
		this.m_textArea.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.TextfieldDark));
		UIFittedText uifittedText = new UIFittedText(this.m_textArea, true, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), true, this.m_textColor, null);
		uifittedText.SetHorizontalAlign(0f);
		base.SetTextField(uifittedText);
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x000B6ECC File Offset: 0x000B52CC
	public override void SetTextColor(string _color)
	{
		this.m_textColor = _color;
		if (this.m_textField != null)
		{
			if (this.m_textField is UITextbox)
			{
				(this.m_textField as UITextbox).SetColor(this.m_textColor, null);
			}
			else if (this.m_textField is UIFittedText)
			{
				(this.m_textField as UIFittedText).SetColor(this.m_textColor, null);
			}
			else if (this.m_textField is UIText)
			{
				(this.m_textField as UIText).SetColor(this.m_textColor, null);
			}
		}
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x000B6F6A File Offset: 0x000B536A
	public void SetTextAreaDrawhandler(UIDrawDelegate _drawHandler)
	{
		this.m_textArea.SetDrawHandler(_drawHandler);
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x000B6F78 File Offset: 0x000B5378
	public override void SetText(string text)
	{
		if (this.m_textField != null)
		{
			if (this.m_textField is UITextbox)
			{
				(this.m_textField as UITextbox).SetText(text);
			}
			else if (this.m_textField is UIFittedText)
			{
				(this.m_textField as UIFittedText).SetText(text);
				this.m_textField.m_parent.Update();
			}
			else if (this.m_textField is UIText)
			{
				(this.m_textField as UIText).SetText(text);
				this.m_textField.m_parent.Update();
			}
		}
		if (this.m_root != null && this.m_root is PsUIGenericInputField)
		{
			if (this.m_title != null && !this.m_hideTitle && !(this.m_root as PsUIGenericInputField).m_hideTitle)
			{
				this.m_title.SetText((this.m_root as PsUIGenericInputField).m_title.m_text);
			}
			base.SetMinMaxCharacterCount((this.m_root as PsUIGenericInputField).m_minCharacterCount, (this.m_root as PsUIGenericInputField).m_maxCharacterCount);
		}
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x000B70A9 File Offset: 0x000B54A9
	public void ChangeTitleText(string _text)
	{
		this.m_title.SetText(_text);
	}

	// Token: 0x040015A3 RID: 5539
	private UIText m_title;

	// Token: 0x040015A4 RID: 5540
	private UICanvas m_textArea;

	// Token: 0x040015A5 RID: 5541
	private UIVerticalList m_column;
}
