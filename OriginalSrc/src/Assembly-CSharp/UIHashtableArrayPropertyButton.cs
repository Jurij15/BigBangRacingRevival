using System;
using System.Collections;

// Token: 0x02000597 RID: 1431
public class UIHashtableArrayPropertyButton : UIVerticalListButton
{
	// Token: 0x060029A3 RID: 10659 RVA: 0x001B636C File Offset: 0x001B476C
	public UIHashtableArrayPropertyButton(UIComponent _parent, UIScrollableCanvas _scrollableCanvas, UIPagedCanvas _pagedCanvas, string _tag, string _label, UIModel _model, string _fieldName, string _key)
		: base(_parent, _scrollableCanvas, _pagedCanvas, _tag, _model, _fieldName)
	{
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		this.m_key = _key;
		Hashtable hashtable = (Hashtable)this.GetValue();
		Debug.LogWarning("UPGRADES: " + hashtable);
		Debug.LogWarning("ARRAY: " + hashtable[this.m_key]);
		string text = "-";
		int num = 0;
		object[] array = hashtable[this.m_key] as object[];
		this.m_length = array.Length;
		IEnumerator enumerator = hashtable.Keys.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text2 = (string)obj;
				if (num == 0)
				{
					text = text2;
				}
				else
				{
					text = text + ", " + text2;
				}
				num++;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
		this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_value.SetAlign(1f, 0f);
	}

	// Token: 0x060029A4 RID: 10660 RVA: 0x001B64F8 File Offset: 0x001B48F8
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		Debug.LogWarning("VALUE CHANGE HASHTABLE BUTTON");
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x001B6504 File Offset: 0x001B4904
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		if (!this.m_pagedCanvas.IsChangingPage())
		{
			base.OnTouchRelease(_touch, _inside);
			if (_inside)
			{
				this.CreateArrays();
			}
		}
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x001B652C File Offset: 0x001B492C
	public void CreateArrays()
	{
		this.scrollableCanvas = new UIScrollableCanvas(this.m_scrollableCanvas.m_parent, string.Empty);
		this.scrollableCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		this.scrollableCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		UIVerticalList uiverticalList = new UIVerticalList(this.scrollableCanvas, "VerticalArea");
		uiverticalList.SetVerticalAlign(1f);
		UIText uitext = new UIText(uiverticalList, false, string.Empty, this.m_label.m_text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, "#000000", null);
		new UIHashtableArrayPropertyButton.UIArrayLengthButton(uiverticalList, string.Empty, "LENGTH", this.m_model, this.m_fieldName, this.m_key, 0, 20);
		Hashtable hashtable = (Hashtable)this.GetValue();
		IEnumerator enumerator = hashtable.Keys.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text = (string)obj;
				new UIHashtableArrayPropertyButton.UIStringArrayEditorButton(uiverticalList, this.m_scrollableCanvas, this.m_pagedCanvas, string.Empty, text, this.m_model, this.m_fieldName, text);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
		this.m_backButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_backButton.SetText("Back", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_backButton.SetOrangeColors(true);
		this.m_pagedCanvas.NextPage();
		this.m_pagedCanvas.Update();
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x001B66CC File Offset: 0x001B4ACC
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

	// Token: 0x04002EBC RID: 11964
	public UIText m_label;

	// Token: 0x04002EBD RID: 11965
	public UIText m_value;

	// Token: 0x04002EBE RID: 11966
	public UIScrollableCanvas scrollableCanvas;

	// Token: 0x04002EBF RID: 11967
	public PsUIGenericButton m_backButton;

	// Token: 0x04002EC0 RID: 11968
	public int m_length;

	// Token: 0x04002EC1 RID: 11969
	private string m_key;

	// Token: 0x02000598 RID: 1432
	private class UIArrayLengthButton : UIVerticalListButton
	{
		// Token: 0x060029A8 RID: 10664 RVA: 0x001B6724 File Offset: 0x001B4B24
		public UIArrayLengthButton(UIComponent _parent, string _tag, string _label, UIModel _model, string _fieldName, string _key, int _minValue = 0, int _maxValue = 99999)
			: base(_parent, null, null, _tag, _model, _fieldName)
		{
			this.m_minValue = (float)_minValue;
			this.m_maxValue = (float)_maxValue;
			this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
			this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
			this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
			this.m_key = _key;
			Hashtable hashtable = (Hashtable)this.GetValue();
			object[] array = (object[])hashtable[this.m_key];
			this.m_currentLength = array.Length;
			string text = this.m_currentLength.ToString();
			this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
			this.m_label.SetAlign(0f, 1f);
			this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
			this.m_value.SetAlign(1f, 0f);
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x001B6824 File Offset: 0x001B4C24
		protected override void OnTouchRelease(TLTouch _touch, bool _inside)
		{
			base.OnTouchRelease(_touch, _inside);
			if (!_touch.m_dragged)
			{
				UITextInput uitextInput = new UITextInput(this.m_label.m_text, new Action<string>(this.DoneCallback), new Action(this.CancelCallback), new Action<string>(this.KeyPressedCallback), 2, false, 64);
				uitextInput.SetText(this.m_currentLength.ToString());
				uitextInput.Update();
			}
			this.UnfreezeVerticalScroll(false);
			this.m_changeValue = false;
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x001B68A8 File Offset: 0x001B4CA8
		public override void OnValueChange(object _newValue, object _oldValue)
		{
			Hashtable hashtable = (Hashtable)this.GetValue();
			object[] array = (object[])hashtable[this.m_key];
			this.m_currentLength = array.Length;
			this.m_value.SetText(this.m_currentLength.ToString());
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x001B68F8 File Offset: 0x001B4CF8
		private void DoneCallback(string _value)
		{
			Debug.Log("DONE: " + _value, null);
			Hashtable hashtable = (Hashtable)this.GetValue();
			Hashtable hashtable2 = new Hashtable();
			IEnumerator enumerator = hashtable.Keys.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					string text = (string)obj;
					object[] array = (object[])hashtable[text];
					object[] array2 = new object[Convert.ToInt32(_value)];
					Array.Copy(array, array2, (array.Length <= array2.Length) ? array.Length : array2.Length);
					hashtable2.Add(text, array2);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
			this.SetValue(hashtable2);
			this.m_currentLength = Convert.ToInt32(_value);
			this.m_value.SetText(_value);
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x001B69E4 File Offset: 0x001B4DE4
		private void KeyPressedCallback(string _value)
		{
			Debug.Log("PRESSED: " + _value, null);
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x001B69F7 File Offset: 0x001B4DF7
		private void CancelCallback()
		{
			Debug.Log("CANCEL", null);
		}

		// Token: 0x04002EC2 RID: 11970
		protected int m_tempVal;

		// Token: 0x04002EC3 RID: 11971
		public float m_minValue;

		// Token: 0x04002EC4 RID: 11972
		public float m_maxValue;

		// Token: 0x04002EC5 RID: 11973
		protected float m_currentFloatValue;

		// Token: 0x04002EC6 RID: 11974
		protected bool m_changeValue;

		// Token: 0x04002EC7 RID: 11975
		private string m_key;

		// Token: 0x04002EC8 RID: 11976
		public UIText m_label;

		// Token: 0x04002EC9 RID: 11977
		public UIText m_value;

		// Token: 0x04002ECA RID: 11978
		private int m_currentLength;
	}

	// Token: 0x02000599 RID: 1433
	private class UIStringArrayEditorButton : UIVerticalListButton
	{
		// Token: 0x060029AE RID: 10670 RVA: 0x001B6A04 File Offset: 0x001B4E04
		public UIStringArrayEditorButton(UIComponent _parent, UIScrollableCanvas _scrollableCanvas, UIPagedCanvas _pagedCanvas, string _tag, string _label, UIModel _model, string _fieldName, string _key)
			: base(_parent, _scrollableCanvas, _pagedCanvas, _tag, _model, _fieldName)
		{
			this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
			this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
			this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
			this.m_key = _key;
			string text = "0";
			Hashtable hashtable = (Hashtable)this.GetValue();
			this.m_array = (object[])hashtable[this.m_key];
			for (int i = 0; i < this.m_array.Length; i++)
			{
				if (i == 0)
				{
					text = (string)this.m_array.GetValue(i);
				}
				else
				{
					text = text + ", " + (string)this.m_array.GetValue(i);
				}
			}
			this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
			this.m_label.SetAlign(0f, 1f);
			this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
			this.m_value.SetAlign(1f, 0f);
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x001B6B3C File Offset: 0x001B4F3C
		public override void OnValueChange(object _newValue, object _oldValue)
		{
			Debug.LogWarning("VALUE CHANGE ARRAY BUTTON");
			string text = "0";
			Hashtable hashtable = (Hashtable)this.GetValue();
			this.m_array = (object[])hashtable[this.m_key];
			for (int i = 0; i < this.m_array.Length; i++)
			{
				if (i == 0)
				{
					text = (string)this.m_array.GetValue(i);
				}
				else
				{
					text = text + ", " + (string)this.m_array.GetValue(i);
				}
			}
			this.m_value.SetText(text);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x001B6BDC File Offset: 0x001B4FDC
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
					for (int i = 0; i < this.m_array.Length; i++)
					{
						new UIHashtableArrayPropertyButton.UIHashtableStringArrayEditButton(uiverticalList, string.Empty, this.m_model, this.m_fieldName, this.m_key, i);
					}
					this.m_backButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
					this.m_backButton.SetText("Back", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
					this.m_backButton.SetOrangeColors(true);
					this.m_pagedCanvas.NextPage();
					this.m_pagedCanvas.Update();
				}
			}
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x001B6D20 File Offset: 0x001B5120
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

		// Token: 0x04002ECB RID: 11979
		public UIText m_label;

		// Token: 0x04002ECC RID: 11980
		public UIText m_value;

		// Token: 0x04002ECD RID: 11981
		public UIScrollableCanvas scrollableCanvas;

		// Token: 0x04002ECE RID: 11982
		public PsUIGenericButton m_backButton;

		// Token: 0x04002ECF RID: 11983
		private string m_key;

		// Token: 0x04002ED0 RID: 11984
		private object[] m_array;
	}

	// Token: 0x0200059A RID: 1434
	private class UIHashtableStringArrayEditButton : UIVerticalListButton
	{
		// Token: 0x060029B2 RID: 10674 RVA: 0x001B6D78 File Offset: 0x001B5178
		public UIHashtableStringArrayEditButton(UIComponent _parent, string _tag, UIModel _model, string _fieldName, string _key, int _arrayTargetIndex)
			: base(_parent, null, null, _tag, _model, _fieldName)
		{
			this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
			this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
			this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
			this.m_arrayTargetIndex = _arrayTargetIndex;
			this.m_key = _key;
			string text = "0";
			Hashtable hashtable = (Hashtable)this.GetValue();
			this.m_array = (object[])hashtable[this.m_key];
			if (this.m_array[_arrayTargetIndex] != null)
			{
				text = this.m_array[_arrayTargetIndex].ToString();
			}
			this.m_label = new UIText(this, false, _tag, "[" + _arrayTargetIndex + "]", "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
			this.m_label.SetAlign(0f, 1f);
			this.m_value = new UIText(this, false, _tag, text, "HurmeSemiBold_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
			this.m_value.SetAlign(1f, 0f);
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x001B6E90 File Offset: 0x001B5290
		protected override void OnTouchRelease(TLTouch _touch, bool _inside)
		{
			base.OnTouchRelease(_touch, _inside);
			if (_inside)
			{
				UITextInput uitextInput = new UITextInput(this.m_label.m_text, new Action<string>(this.DoneCallback), new Action(this.CancelCallback), new Action<string>(this.KeyPressedCallback), 2, false, 64);
				string text = "0";
				Hashtable hashtable = (Hashtable)this.GetValue();
				this.m_array = (object[])hashtable[this.m_key];
				if (this.m_array[this.m_arrayTargetIndex] != null)
				{
					text = this.m_array[this.m_arrayTargetIndex].ToString();
				}
				uitextInput.SetText(text);
				uitextInput.Update();
			}
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x001B6F40 File Offset: 0x001B5340
		public override void OnValueChange(object _newValue, object _oldValue)
		{
			Debug.LogWarning("VALUE CHANGE EDIT BUTTON");
			string text = "0";
			Hashtable hashtable = (Hashtable)this.GetValue();
			this.m_array = (object[])hashtable[this.m_key];
			if (this.m_array[this.m_arrayTargetIndex] != null)
			{
				text = this.m_array[this.m_arrayTargetIndex].ToString();
			}
			this.m_value.SetText(text);
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x001B6FB4 File Offset: 0x001B53B4
		private void DoneCallback(string _value)
		{
			Debug.Log("DONE: " + _value, null);
			Hashtable hashtable = (Hashtable)this.GetValue();
			this.m_array = (object[])hashtable[this.m_key];
			this.m_array[this.m_arrayTargetIndex] = _value;
			hashtable[this.m_key] = this.m_array;
			this.SetValue(hashtable);
			this.m_value.SetText(_value);
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x001B7028 File Offset: 0x001B5428
		private void KeyPressedCallback(string _value)
		{
			Debug.Log("PRESSED: " + _value, null);
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x001B703B File Offset: 0x001B543B
		private void CancelCallback()
		{
			Debug.Log("CANCEL", null);
		}

		// Token: 0x04002ED1 RID: 11985
		public UIText m_label;

		// Token: 0x04002ED2 RID: 11986
		public UIText m_value;

		// Token: 0x04002ED3 RID: 11987
		public int m_arrayTargetIndex;

		// Token: 0x04002ED4 RID: 11988
		private object[] m_array;

		// Token: 0x04002ED5 RID: 11989
		private string m_key;
	}
}
