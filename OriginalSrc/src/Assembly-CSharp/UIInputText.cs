using System;
using UnityEngine;

// Token: 0x020005A8 RID: 1448
public class UIInputText : UIScrollableCanvas
{
	// Token: 0x060029FA RID: 10746 RVA: 0x001B88B8 File Offset: 0x001B6CB8
	public UIInputText(PsUIInputTextField _content, params char[] _ignoreCharacters)
		: base(null, string.Empty)
	{
		this.m_text = _content.GetText();
		this.m_ignoreCharacters = _ignoreCharacters;
		this.m_placeholderText = string.Empty;
		this.m_multiline = false;
		this.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.m_maxScrollInertialY = 0f;
		this.m_mainArea = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_mainArea.SetHeight(0.4f, RelativeTo.ScreenHeight);
		this.m_mainArea.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_mainArea.SetAlign(0f, 1f);
		this.m_mainArea.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_mainArea, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_backButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_backButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_backButton.SetSound("/UI/ButtonBack");
		this.m_backButton.SetOrangeColors(true);
		this.m_content = _content;
		this.m_content.Parent(this.m_mainArea);
		this.m_content.SetAlign(0.5f, 0.5f);
	}

	// Token: 0x060029FB RID: 10747 RVA: 0x001B8A55 File Offset: 0x001B6E55
	public override void Update()
	{
		this.OpenKeyboard();
		base.Update();
	}

	// Token: 0x060029FC RID: 10748 RVA: 0x001B8A63 File Offset: 0x001B6E63
	protected virtual void UpdateView()
	{
		this.m_content.SetText(this.m_text);
		this.m_mainArea.Update();
	}

	// Token: 0x060029FD RID: 10749 RVA: 0x001B8A81 File Offset: 0x001B6E81
	public override void Step()
	{
		if (this.m_backButton.m_hit)
		{
			this.CloseKeyboard(true);
			return;
		}
		this.UpdateKeyboard();
		base.Step();
	}

	// Token: 0x060029FE RID: 10750 RVA: 0x001B8AA8 File Offset: 0x001B6EA8
	protected virtual void OpenKeyboard()
	{
		if (this.m_keyboard == null)
		{
			if (this.m_placeholderText != string.Empty)
			{
				this.m_keyboard = TouchScreenKeyboard.Open(this.m_text, this.m_content.m_keyboardType, false, this.m_multiline, false, true, this.m_placeholderText);
			}
			else
			{
				this.m_keyboard = TouchScreenKeyboard.Open(this.m_text, this.m_content.m_keyboardType, false, this.m_multiline, false, true);
				this.m_keyboard.text = this.m_text;
			}
		}
		else
		{
			this.m_keyboard.active = true;
		}
	}

	// Token: 0x060029FF RID: 10751 RVA: 0x001B8B4C File Offset: 0x001B6F4C
	protected virtual void UpdateKeyboard()
	{
		string text = this.m_text;
		text = this.m_keyboard.text;
		if (this.m_ignoreCharacters != null)
		{
			for (int i = 0; i < this.m_ignoreCharacters.Length; i++)
			{
				text = text.Replace(this.m_ignoreCharacters[i].ToString(), string.Empty);
			}
			this.m_keyboard.text = text;
		}
		text = Keyboard.RemoveSurrogatePairs(text, string.Empty);
		this.m_keyboard.text = text;
		if (this.m_content.m_maxCharacterCount > -1 && this.m_keyboard.text.Length > this.m_content.m_maxCharacterCount)
		{
			text = this.m_keyboard.text.Substring(0, this.m_content.m_maxCharacterCount);
			this.m_keyboard.text = text;
		}
		if (this.m_keyboard.done)
		{
			this.CloseKeyboard(this.m_keyboard.wasCanceled);
			return;
		}
		if (!this.m_keyboard.active)
		{
			this.CloseKeyboard(true);
		}
		if (this.m_text != text)
		{
			this.m_text = ToolBox.GetUTF8String(text);
			if (this.m_text.IndexOf(" ", 1) == 0)
			{
				this.m_text = this.m_text.Remove(0, 1);
			}
			this.UpdateView();
		}
	}

	// Token: 0x06002A00 RID: 10752 RVA: 0x001B8CB8 File Offset: 0x001B70B8
	public void CloseKeyboard(bool _cancel = false)
	{
		if (this.m_keyboard != null)
		{
			this.m_keyboard.active = false;
		}
		if (this.m_text.Length >= this.m_content.m_minCharacterCount || _cancel)
		{
			if (this.m_keyboard != null)
			{
				if (this.m_keyboard.wasCanceled || _cancel)
				{
					this.m_content.Cancel();
				}
				else
				{
					this.m_content.Done();
				}
			}
			this.Destroy();
		}
		else if (this.m_errorPopup == null)
		{
			this.m_errorPopup = new UITooShortNamePopup(this.m_content.m_minCharacterCount, new Action(this.ErrorPopupClosed));
		}
	}

	// Token: 0x06002A01 RID: 10753 RVA: 0x001B8D71 File Offset: 0x001B7171
	private void ErrorPopupClosed()
	{
		this.m_errorPopup = null;
		this.CloseKeyboard(true);
	}

	// Token: 0x04002F34 RID: 12084
	private UICanvas m_mainArea;

	// Token: 0x04002F35 RID: 12085
	private PsUIInputTextField m_content;

	// Token: 0x04002F36 RID: 12086
	private PsUIGenericButton m_backButton;

	// Token: 0x04002F37 RID: 12087
	private UITooShortNamePopup m_errorPopup;

	// Token: 0x04002F38 RID: 12088
	private string m_placeholderText;

	// Token: 0x04002F39 RID: 12089
	private bool m_multiline;

	// Token: 0x04002F3A RID: 12090
	private char[] m_ignoreCharacters;

	// Token: 0x04002F3B RID: 12091
	private string m_text = string.Empty;

	// Token: 0x04002F3C RID: 12092
	private TouchScreenKeyboard m_keyboard;
}
