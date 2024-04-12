using System;
using System.Text;
using UnityEngine;

// Token: 0x02000571 RID: 1393
public class Keyboard
{
	// Token: 0x06002898 RID: 10392 RVA: 0x001AFDB6 File Offset: 0x001AE1B6
	public Keyboard(Action<string> _doneCallback, Action _cancelCallback, Action _tooShortStringCallback = null, Action<string> _keyPressedCallback = null, TouchScreenKeyboardType _keyboardType = 1)
	{
		this.m_keyboardDataMode = KeyboardDataMode.Callback;
		this.m_doneCallback = _doneCallback;
		this.m_cancelCallback = _cancelCallback;
		this.m_keyPressedCallback = _keyPressedCallback;
		this.Construct();
	}

	// Token: 0x06002899 RID: 10393 RVA: 0x001AFDE1 File Offset: 0x001AE1E1
	public Keyboard(TextModel _model, Action _tooShortStringCallbackUIComponent = null, TouchScreenKeyboardType _keyboardType = 1)
	{
		this.m_keyboardDataMode = KeyboardDataMode.TextModel;
		this.m_keyboardType = _keyboardType;
		this.m_textModel = _model;
		this.m_text = this.m_textModel.m_text;
		this.Construct();
	}

	// Token: 0x0600289A RID: 10394 RVA: 0x001AFE15 File Offset: 0x001AE215
	public void Construct()
	{
		this.m_placeholderText = string.Empty;
		this.m_multiline = false;
		this.m_minCharacterCount = -1;
		this.m_maxCharacterCount = 128;
	}

	// Token: 0x0600289B RID: 10395 RVA: 0x001AFE3B File Offset: 0x001AE23B
	public void SetPlaceholderText(string _placeholder)
	{
		this.m_placeholderText = _placeholder;
	}

	// Token: 0x0600289C RID: 10396 RVA: 0x001AFE44 File Offset: 0x001AE244
	public void SetMaxCharacterCount(int _value)
	{
		this.m_maxCharacterCount = _value;
	}

	// Token: 0x0600289D RID: 10397 RVA: 0x001AFE4D File Offset: 0x001AE24D
	public void SetMinCharacterCount(int _value)
	{
		this.m_minCharacterCount = _value;
	}

	// Token: 0x0600289E RID: 10398 RVA: 0x001AFE56 File Offset: 0x001AE256
	public void SetValue(object _value)
	{
		this.m_text = _value.ToString();
		if (this.m_keyboardDataMode == KeyboardDataMode.TextModel)
		{
			this.m_textModel.m_uiModel.SetValue(_value.ToString(), "m_text");
		}
	}

	// Token: 0x0600289F RID: 10399 RVA: 0x001AFE8C File Offset: 0x001AE28C
	public virtual void OpenKeyboard()
	{
		Debug.Log("OPEN KEYBOARD", null);
		Keyboard.m_active = true;
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

	// Token: 0x060028A0 RID: 10400 RVA: 0x001AFF38 File Offset: 0x001AE338
	public void CloseKeyboard(bool _cancel = false)
	{
		Debug.Log("CLOSE KEYBOARD", null);
		Keyboard.m_active = false;
		if (this.m_keyboard != null)
		{
			this.m_keyboard.active = false;
		}
		if (this.m_text.Length >= this.m_minCharacterCount || this.m_tooShortStringCallback == null)
		{
			if (this.m_keyboard != null)
			{
				if (this.m_keyboard.wasCanceled || _cancel)
				{
					if (this.m_cancelCallback != null)
					{
						this.m_cancelCallback.Invoke();
					}
					this.m_textModel.m_uiModel.SetValue(true, "m_cancelled");
				}
				else if (this.m_textModel == null)
				{
					if (this.m_doneCallback != null)
					{
						this.m_doneCallback.Invoke(this.m_text);
					}
				}
				else
				{
					if (this.m_keyboardType == 4)
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
					this.m_textModel.m_uiModel.SetValue(true, "m_done");
				}
			}
		}
		else if (this.m_tooShortStringCallback != null)
		{
			this.m_tooShortStringCallback.Invoke();
		}
	}

	// Token: 0x060028A1 RID: 10401 RVA: 0x001B00C0 File Offset: 0x001AE4C0
	public static string RemoveSurrogatePairs(string str, string replacementCharacter = "")
	{
		if (str == null)
		{
			return null;
		}
		StringBuilder stringBuilder = null;
		for (int i = 0; i < str.Length; i++)
		{
			char c = str.get_Chars(i);
			if (char.IsSurrogate(c))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(str, 0, i, str.Length);
				}
				stringBuilder.Append(replacementCharacter);
				if (i + 1 < str.Length && char.IsHighSurrogate(c) && char.IsLowSurrogate(str.get_Chars(i + 1)))
				{
					i++;
				}
			}
			else if (stringBuilder != null)
			{
				stringBuilder.Append(c);
			}
		}
		return (stringBuilder != null) ? stringBuilder.ToString() : str;
	}

	// Token: 0x060028A2 RID: 10402 RVA: 0x001B0174 File Offset: 0x001AE574
	public virtual void UpdateKeyboard()
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
			this.SetValue(ToolBox.GetUTF8String(text));
			if (this.m_keyPressedCallback != null)
			{
				this.m_keyPressedCallback.Invoke(text);
			}
		}
	}

	// Token: 0x04002DCB RID: 11723
	private KeyboardDataMode m_keyboardDataMode;

	// Token: 0x04002DCC RID: 11724
	private Action<string> m_doneCallback;

	// Token: 0x04002DCD RID: 11725
	private Action<string> m_keyPressedCallback;

	// Token: 0x04002DCE RID: 11726
	private Action m_cancelCallback;

	// Token: 0x04002DCF RID: 11727
	private Action m_tooShortStringCallback;

	// Token: 0x04002DD0 RID: 11728
	private int m_arrayTargetIndex;

	// Token: 0x04002DD1 RID: 11729
	private TouchScreenKeyboard m_keyboard;

	// Token: 0x04002DD2 RID: 11730
	private TouchScreenKeyboardType m_keyboardType;

	// Token: 0x04002DD3 RID: 11731
	private bool m_multiline;

	// Token: 0x04002DD4 RID: 11732
	private int m_maxCharacterCount;

	// Token: 0x04002DD5 RID: 11733
	private int m_minCharacterCount;

	// Token: 0x04002DD6 RID: 11734
	private string m_originalText;

	// Token: 0x04002DD7 RID: 11735
	private string m_text;

	// Token: 0x04002DD8 RID: 11736
	private string m_placeholderText;

	// Token: 0x04002DD9 RID: 11737
	private TextModel m_textModel;

	// Token: 0x04002DDA RID: 11738
	public static bool m_active;
}
