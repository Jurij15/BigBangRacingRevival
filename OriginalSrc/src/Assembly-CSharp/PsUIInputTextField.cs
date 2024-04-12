using System;
using UnityEngine;

// Token: 0x02000269 RID: 617
public abstract class PsUIInputTextField : UIHorizontalList
{
	// Token: 0x06001279 RID: 4729 RVA: 0x000B64E4 File Offset: 0x000B48E4
	public PsUIInputTextField(UIComponent _parent)
		: base(_parent, "InputTextField")
	{
		this.m_keyboardType = 1;
		this.RemoveDrawHandler();
		this.ConstructUI();
	}

	// Token: 0x0600127A RID: 4730 RVA: 0x000B656C File Offset: 0x000B496C
	public PsUIInputTextField(UIComponent _parent, float _width, float _height, RelativeTo _widthRelativeTo, RelativeTo _heightRelativeTo, cpBB _margins, bool _hideTitle, string _hintText = "")
		: base(_parent, "InputTextField")
	{
		this.m_heightRatio = _height;
		this.m_widthRatio = _width;
		this.m_textHeightRelativeTo = _heightRelativeTo;
		this.m_textWidthRelativeTo = _widthRelativeTo;
		this.m_fieldMargins = _margins;
		this.m_hideTitle = _hideTitle;
		if (!string.IsNullOrEmpty(_hintText))
		{
			this.SetHintText(_hintText, false);
		}
		this.m_keyboardType = 1;
		this.RemoveDrawHandler();
		this.ConstructUI();
	}

	// Token: 0x0600127B RID: 4731
	protected abstract void ConstructUI();

	// Token: 0x0600127C RID: 4732 RVA: 0x000B6638 File Offset: 0x000B4A38
	public override void Step()
	{
		if (this.m_root == null && ((this.m_textField != null && this.m_textField.m_hit) || this.m_textField.m_parent.m_hit))
		{
			if (this.m_hintTextHolder != null)
			{
				this.m_hintTextHolder.Hide();
			}
			PsUIInputTextField psUIInputTextField = Activator.CreateInstance(base.GetType()) as PsUIInputTextField;
			psUIInputTextField.m_textField.RemoveTouchAreas();
			psUIInputTextField.m_root = this;
			psUIInputTextField.SetTextColor(this.m_textColor);
			psUIInputTextField.SetText(this.GetText());
			UIInputText uiinputText;
			if (PsState.m_adminMode)
			{
				uiinputText = new UIInputText(psUIInputTextField, new char[0]);
			}
			else
			{
				uiinputText = new UIInputText(psUIInputTextField, new char[] { '>', '<', '\\', '/' });
			}
			uiinputText.Update();
			TouchAreaS.CancelAllTouches(null);
		}
		base.Step();
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x000B6717 File Offset: 0x000B4B17
	public virtual void SetTextColor(string _color)
	{
		this.m_textColor = _color;
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x000B6720 File Offset: 0x000B4B20
	public void SetTextField(UITextbox _field)
	{
		this.m_textField = _field;
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x000B6729 File Offset: 0x000B4B29
	public void SetTextField(UIFittedText _field)
	{
		this.m_textField = _field;
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x000B6734 File Offset: 0x000B4B34
	public virtual void SetText(string text)
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
		}
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x000B67A4 File Offset: 0x000B4BA4
	public virtual void SetHintText(string _hint, bool _hideHintAfterFirst = false)
	{
		if (!string.IsNullOrEmpty(_hint))
		{
			this.m_hideHint = _hideHintAfterFirst;
			this.m_hintText = _hint;
			if (string.IsNullOrEmpty(this.GetText()))
			{
				this.m_hintTextHolder = new UIText(this.m_textField, false, string.Empty, _hint, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, "#1B405A", null);
			}
		}
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x000B6804 File Offset: 0x000B4C04
	public string GetText()
	{
		if (this.m_textField != null)
		{
			if (this.m_textField is UITextbox)
			{
				return (this.m_textField as UITextbox).m_text;
			}
			if (this.m_textField is UIFittedText)
			{
				return (this.m_textField as UIFittedText).m_text;
			}
		}
		return string.Empty;
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x000B6863 File Offset: 0x000B4C63
	public void SetCallbacks(Action<string> _done, Action _cancel = null)
	{
		this.m_doneCallback = _done;
		this.m_cancelCallback = _cancel;
	}

	// Token: 0x06001284 RID: 4740 RVA: 0x000B6874 File Offset: 0x000B4C74
	public void Done()
	{
		if (this.m_root != null)
		{
			if (this.m_hintTextHolder != null && string.IsNullOrEmpty(this.GetText()) && !this.m_hideHint)
			{
				this.m_hintTextHolder.Show();
			}
			this.m_root.SetText(this.GetText());
			if (this.m_root.m_doneCallback != null)
			{
				this.m_root.m_doneCallback.Invoke(this.GetText());
			}
		}
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x000B68F4 File Offset: 0x000B4CF4
	public void Cancel()
	{
		if (this.m_root != null)
		{
			if (this.m_hintTextHolder != null && string.IsNullOrEmpty(this.GetText()) && !this.m_hideHint)
			{
				this.m_hintTextHolder.Show();
			}
			if (this.m_root.m_cancelCallback != null)
			{
				this.m_root.m_cancelCallback.Invoke();
			}
		}
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x000B695D File Offset: 0x000B4D5D
	public void SetMinMaxCharacterCount(int _minCharacterCount, int _maxCharacterCount)
	{
		this.m_minCharacterCount = _minCharacterCount;
		this.m_maxCharacterCount = _maxCharacterCount;
	}

	// Token: 0x040015A7 RID: 5543
	protected PsUIInputTextField m_root;

	// Token: 0x040015A8 RID: 5544
	public TouchScreenKeyboardType m_keyboardType;

	// Token: 0x040015A9 RID: 5545
	public int m_minCharacterCount = -1;

	// Token: 0x040015AA RID: 5546
	public int m_maxCharacterCount = -1;

	// Token: 0x040015AB RID: 5547
	public UIComponent m_textField;

	// Token: 0x040015AC RID: 5548
	private Action<string> m_doneCallback;

	// Token: 0x040015AD RID: 5549
	private Action m_cancelCallback;

	// Token: 0x040015AE RID: 5550
	protected float m_heightRatio = 0.09f;

	// Token: 0x040015AF RID: 5551
	protected float m_widthRatio = 0.6f;

	// Token: 0x040015B0 RID: 5552
	protected RelativeTo m_textHeightRelativeTo = RelativeTo.ScreenHeight;

	// Token: 0x040015B1 RID: 5553
	protected RelativeTo m_textWidthRelativeTo = RelativeTo.ScreenHeight;

	// Token: 0x040015B2 RID: 5554
	protected cpBB m_fieldMargins = new cpBB(0.02f, 0.015f, 0.02f, 0.015f);

	// Token: 0x040015B3 RID: 5555
	protected bool m_hideTitle;

	// Token: 0x040015B4 RID: 5556
	public string m_textColor = "#b1f92a";

	// Token: 0x040015B5 RID: 5557
	public string m_hintText;

	// Token: 0x040015B6 RID: 5558
	public bool m_hideHint;

	// Token: 0x040015B7 RID: 5559
	private UIText m_hintTextHolder;
}
