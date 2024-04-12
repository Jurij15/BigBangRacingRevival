using System;
using UnityEngine;

// Token: 0x020005AB RID: 1451
public class UITextInput : UIScrollableCanvas
{
	// Token: 0x06002A18 RID: 10776 RVA: 0x001B9645 File Offset: 0x001B7A45
	public UITextInput(string _label, Action<string> _doneCallback, Action _cancelCallback, Action<string> _keyPressedCallback = null, TouchScreenKeyboardType _keyboardType = 1, bool _hideOtherUIComponents = true, int _maxCharacterCount = 128)
		: base(null, "TextInput")
	{
		this.m_textInputType = KeyboardDataMode.Callback;
		this.m_doneCallback = _doneCallback;
		this.m_cancelCallback = _cancelCallback;
		this.m_keyPressedCallback = _keyPressedCallback;
		this.m_labelText = _label;
		this.m_maxCharacterCount = _maxCharacterCount;
		this.Construct();
	}

	// Token: 0x06002A19 RID: 10777 RVA: 0x001B9688 File Offset: 0x001B7A88
	public UITextInput(string _label, UIModel _model, string _STRINGfieldName, TouchScreenKeyboardType _keyboardType = 1, bool _hideOtherUIComponents = true, int _maxCharacterCount = 128)
		: base(null, "TextInput")
	{
		this.m_textInputType = KeyboardDataMode.Reflection;
		this.m_keyboardType = _keyboardType;
		this.m_hideUI = _hideOtherUIComponents;
		if (this.m_hideUI)
		{
			EntityManager.SetVisibilityOfEntitiesWithTag("UIComponent", false);
		}
		this.m_labelText = _label;
		this.m_maxCharacterCount = _maxCharacterCount;
		this.m_model = _model;
		this.m_fieldName = _STRINGfieldName;
		this.Construct();
	}

	// Token: 0x06002A1A RID: 10778 RVA: 0x001B96F4 File Offset: 0x001B7AF4
	public UITextInput(string _label, UIModel _model, string _STRINGARRAYfieldName, int _arrayTargetIndex, TouchScreenKeyboardType _keyboardType = 1, bool _hideOtherUIComponents = true, int _maxCharacterCount = 128)
		: base(null, "TextInput")
	{
		this.m_textInputType = KeyboardDataMode.ArrayReflection;
		this.m_keyboardType = _keyboardType;
		this.m_hideUI = _hideOtherUIComponents;
		if (this.m_hideUI)
		{
			EntityManager.SetVisibilityOfEntitiesWithTag("UIComponent", false);
		}
		this.m_labelText = _label;
		this.m_maxCharacterCount = _maxCharacterCount;
		this.m_model = _model;
		this.m_fieldName = _STRINGARRAYfieldName;
		this.m_arrayTargetIndex = _arrayTargetIndex;
		this.Construct();
	}

	// Token: 0x06002A1B RID: 10779 RVA: 0x001B9768 File Offset: 0x001B7B68
	public void Construct()
	{
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_text = string.Empty;
		this.m_placeholderText = string.Empty;
		this.m_multiline = false;
		this.m_minCharacterCount = -1;
		this.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.m_maxScrollInertialY = 0f;
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenHeight);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_cancelButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_cancelButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_cancelButton.SetSound("/UI/ButtonBack");
		this.m_cancelButton.SetOrangeColors(true);
		this.CreateView();
	}

	// Token: 0x06002A1C RID: 10780 RVA: 0x001B9873 File Offset: 0x001B7C73
	public void SetText(string _text)
	{
		this.m_text = _text;
		if (this.m_textbox != null)
		{
			this.m_textbox.SetText(this.m_text);
		}
	}

	// Token: 0x06002A1D RID: 10781 RVA: 0x001B9898 File Offset: 0x001B7C98
	public void SetPlaceholderText(string _placeholder)
	{
		this.m_placeholderText = _placeholder;
		if (this.m_textbox != null)
		{
			this.m_textbox.SetText(this.m_placeholderText);
		}
	}

	// Token: 0x06002A1E RID: 10782 RVA: 0x001B98BD File Offset: 0x001B7CBD
	public void SetMaxCharacterCount(int _value)
	{
		this.m_maxCharacterCount = _value;
	}

	// Token: 0x06002A1F RID: 10783 RVA: 0x001B98C6 File Offset: 0x001B7CC6
	public void SetMinCharacterCount(int _value)
	{
		this.m_minCharacterCount = _value;
	}

	// Token: 0x06002A20 RID: 10784 RVA: 0x001B98CF File Offset: 0x001B7CCF
	public override void Update()
	{
		this.OpenKeyboard();
		base.Update();
	}

	// Token: 0x06002A21 RID: 10785 RVA: 0x001B98DD File Offset: 0x001B7CDD
	public override void Step()
	{
		if (this.m_cancelButton.m_hit)
		{
			this.CloseKeyboard(true);
			return;
		}
		this.UpdateKeyboard();
		base.Step();
	}

	// Token: 0x06002A22 RID: 10786 RVA: 0x001B9904 File Offset: 0x001B7D04
	public override void SetValue(object _value)
	{
		if (this.m_textInputType == KeyboardDataMode.Reflection)
		{
			base.SetValue(_value);
		}
		else if (this.m_textInputType == KeyboardDataMode.ArrayReflection)
		{
			object[] array = (object[])this.GetValue();
			array[this.m_arrayTargetIndex] = _value;
			base.SetValue(array);
		}
	}

	// Token: 0x06002A23 RID: 10787 RVA: 0x001B9954 File Offset: 0x001B7D54
	public void CloseKeyboard(bool _cancel = false)
	{
		if (this.m_keyboard != null)
		{
			this.m_keyboard.active = false;
		}
		if (this.m_text.Length >= this.m_minCharacterCount || _cancel)
		{
			if (this.m_keyboard != null)
			{
				if (this.m_keyboard.wasCanceled || _cancel)
				{
					if (this.m_cancelCallback != null)
					{
						this.m_cancelCallback.Invoke();
					}
				}
				else if (this.m_model == null)
				{
					if (this.m_doneCallback != null)
					{
						this.m_doneCallback.Invoke(this.m_text);
					}
				}
				else if (this.m_keyboardType == 4)
				{
					int num = 0;
					bool flag = int.TryParse(this.m_text, ref num);
					if (flag)
					{
						this.SetValue(num);
					}
				}
				else if (this.m_keyboardType == 2)
				{
					float num2 = 0f;
					bool flag2 = float.TryParse(this.m_text, ref num2);
					if (flag2)
					{
						this.SetValue(num2);
					}
				}
				else
				{
					this.SetValue(this.m_text);
				}
			}
			this.Destroy();
		}
		else if (this.m_errorPopup == null)
		{
			this.m_errorPopup = new UITooShortNamePopup(this.m_minCharacterCount, new Action(this.ErrorPopupClosed));
		}
	}

	// Token: 0x06002A24 RID: 10788 RVA: 0x001B9AA8 File Offset: 0x001B7EA8
	private void ErrorPopupClosed()
	{
		this.m_errorPopup = null;
		this.m_keyboard = TouchScreenKeyboard.Open(this.m_text, this.m_keyboardType, false, this.m_multiline, false, true);
		this.m_keyboard.text = this.m_text;
		Debug.Log("error popup closed", null);
	}

	// Token: 0x06002A25 RID: 10789 RVA: 0x001B9AF8 File Offset: 0x001B7EF8
	protected virtual void OpenKeyboard()
	{
		if (this.m_keyboard == null)
		{
			if (this.m_placeholderText != string.Empty)
			{
				this.m_keyboard = TouchScreenKeyboard.Open(this.m_text, this.m_keyboardType, false, this.m_multiline, false, true, this.m_placeholderText);
			}
			else
			{
				this.m_keyboard = TouchScreenKeyboard.Open(this.m_text, this.m_keyboardType, false, this.m_multiline, false, true);
				this.m_keyboard.text = this.m_text;
			}
		}
		else
		{
			this.m_keyboard.active = true;
		}
	}

	// Token: 0x06002A26 RID: 10790 RVA: 0x001B9B94 File Offset: 0x001B7F94
	protected virtual void CreateView()
	{
		this.m_canvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_canvas.SetHeight(0.4f, RelativeTo.ScreenHeight);
		this.m_canvas.SetVerticalAlign(1f);
		this.m_canvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_canvas, string.Empty);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetHorizontalAlign(0.7f);
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList.SetSpacing(-0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		if (!string.IsNullOrEmpty(this.m_labelText))
		{
			this.m_label = new UITextbox(uiverticalList, false, string.Empty, "<color=#464646>" + this.m_labelText + "</color>", "KGSecondChances_Font", 0.0225f, RelativeTo.ScreenHeight, true, Align.Left, Align.Middle, null, true, null);
			this.m_label.SetMargins(0.025f, 0.025f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			this.m_label.SetHorizontalAlign(0f);
			this.m_label.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.Textfield));
		}
		string text = this.m_text;
		if (this.m_placeholderText != string.Empty)
		{
			text = this.m_placeholderText;
		}
		this.m_textbox = new UITextbox(uiverticalList, false, string.Empty, text, "KGSecondChances_Font", 0.035f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "#000000", true, null);
		this.m_textbox.SetWidth(0.618f, RelativeTo.ScreenWidth);
		this.m_textbox.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.DebugRect));
		this.m_textbox.SetMargins(0.0375f, 0.0375f, 0.025f, 0.025f, RelativeTo.ScreenHeight);
		this.m_textbox.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.Textfield));
	}

	// Token: 0x06002A27 RID: 10791 RVA: 0x001B9D9D File Offset: 0x001B819D
	protected virtual void DestroyView()
	{
	}

	// Token: 0x06002A28 RID: 10792 RVA: 0x001B9D9F File Offset: 0x001B819F
	protected virtual void UpdateView()
	{
		this.m_textbox.SetText(this.m_text);
		this.m_canvas.Update();
	}

	// Token: 0x06002A29 RID: 10793 RVA: 0x001B9DC0 File Offset: 0x001B81C0
	protected virtual void UpdateKeyboard()
	{
		string text = this.m_text;
		text = this.m_keyboard.text;
		if (this.m_maxCharacterCount > -1 && this.m_keyboard.text.Length > this.m_maxCharacterCount)
		{
			text = this.m_keyboard.text.Substring(0, this.m_maxCharacterCount);
			this.m_keyboard.text = text;
		}
		text = Keyboard.RemoveSurrogatePairs(text, string.Empty);
		this.m_keyboard.text = text;
		if (this.m_keyboard.done)
		{
			this.CloseKeyboard(this.m_keyboard.wasCanceled);
			return;
		}
		if (this.m_text != text)
		{
			this.m_text = ToolBox.GetUTF8String(text);
			if (this.m_keyPressedCallback != null)
			{
				this.m_keyPressedCallback.Invoke(text);
			}
			this.UpdateView();
		}
	}

	// Token: 0x06002A2A RID: 10794 RVA: 0x001B9E9F File Offset: 0x001B829F
	public override void Destroy()
	{
		this.DestroyView();
		base.Destroy();
		if (this.m_hideUI)
		{
			EntityManager.SetVisibilityOfEntitiesWithTag("UIComponent", true);
		}
	}

	// Token: 0x04002F69 RID: 12137
	private KeyboardDataMode m_textInputType;

	// Token: 0x04002F6A RID: 12138
	private Action<string> m_doneCallback;

	// Token: 0x04002F6B RID: 12139
	private Action<string> m_keyPressedCallback;

	// Token: 0x04002F6C RID: 12140
	private Action m_cancelCallback;

	// Token: 0x04002F6D RID: 12141
	private int m_arrayTargetIndex;

	// Token: 0x04002F6E RID: 12142
	private TouchScreenKeyboard m_keyboard;

	// Token: 0x04002F6F RID: 12143
	private TouchScreenKeyboardType m_keyboardType;

	// Token: 0x04002F70 RID: 12144
	private bool m_hideUI;

	// Token: 0x04002F71 RID: 12145
	private bool m_multiline;

	// Token: 0x04002F72 RID: 12146
	private int m_maxCharacterCount;

	// Token: 0x04002F73 RID: 12147
	private int m_minCharacterCount;

	// Token: 0x04002F74 RID: 12148
	private string m_originalText;

	// Token: 0x04002F75 RID: 12149
	private string m_text;

	// Token: 0x04002F76 RID: 12150
	private string m_placeholderText;

	// Token: 0x04002F77 RID: 12151
	private string m_labelText;

	// Token: 0x04002F78 RID: 12152
	private UITooShortNamePopup m_errorPopup;

	// Token: 0x04002F79 RID: 12153
	private PsUIGenericButton m_doneButton;

	// Token: 0x04002F7A RID: 12154
	private PsUIGenericButton m_cancelButton;

	// Token: 0x04002F7B RID: 12155
	private UICanvas m_canvas;

	// Token: 0x04002F7C RID: 12156
	public UITextbox m_textbox;

	// Token: 0x04002F7D RID: 12157
	private UITextbox m_label;
}
