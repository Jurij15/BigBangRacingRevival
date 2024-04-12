using System;
using UnityEngine;

// Token: 0x02000386 RID: 902
public class UITextArea : UIVerticalList
{
	// Token: 0x06001A08 RID: 6664 RVA: 0x0011FEF0 File Offset: 0x0011E2F0
	public UITextArea(UIComponent _parent, string _tag, string _label, string _tip, Camera _camera, UIModel _model, string _STRINGfieldName, int rows = -1, int _maxCharacterCount = 128, string _labelColor = "464646", string _color = "464646")
		: base(_parent, _tag)
	{
		this.m_model = _model;
		this.m_fieldName = _STRINGfieldName;
		this.m_labelText = _label;
		this.m_maxCharacterCount = _maxCharacterCount;
		this.CreateTouchAreas();
		this.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this, _tag);
		uiverticalList.SetSpacing(-0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		if (!string.IsNullOrEmpty(_label))
		{
			this.m_label = new UITextbox(uiverticalList, false, _tag, _label, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.0225f, RelativeTo.ScreenHeight, true, Align.Left, Align.Middle, _labelColor, true, null);
			this.m_label.SetMargins(0.025f, 0.025f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			this.m_label.SetHorizontalAlign(0f);
			this.m_label.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.Textfield));
		}
		UIVerticalList uiverticalList2 = uiverticalList;
		bool flag = false;
		string text = (string)this.GetValue();
		string font = PsFontManager.GetFont(PsFonts.HurmeSemiBold);
		float num = 0.04f;
		RelativeTo relativeTo = RelativeTo.ScreenHeight;
		this.m_value = new UITextbox(uiverticalList2, flag, _tag, text, font, num, relativeTo, false, Align.Left, Align.Top, _color, true, null);
		this.m_value.SetMargins(0.0375f, 0.0375f, 0.025f, 0.025f, RelativeTo.ScreenHeight);
		this.m_value.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_value.SetHeight(0.3f, RelativeTo.OwnWidth);
		this.m_value.SetHorizontalAlign(0f);
		this.m_value.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.Textfield));
		if (rows > 0)
		{
			this.m_value.SetMinRows(rows);
			this.m_value.SetMaxRows(rows);
		}
		TextMeshS.SetAlign(this.m_value.m_tmc, Align.Left, Align.Top, true);
		if (_tip != string.Empty)
		{
			this.m_tip = new UITextbox(this, false, _tag, _tip, PsFontManager.GetFont(PsFonts.HurmeRegular), 0.02f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
			this.m_tip.SetMargins(0.025f, 0f, 0.015f, 0f, RelativeTo.ScreenHeight);
			this.m_tip.RemoveDrawHandler();
		}
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x00120122 File Offset: 0x0011E522
	public override void SetValue(object _value)
	{
		base.SetValue(_value);
		this.m_value.SetText((string)_value);
	}

	// Token: 0x04001C66 RID: 7270
	public UITextbox m_label;

	// Token: 0x04001C67 RID: 7271
	public UITextbox m_value;

	// Token: 0x04001C68 RID: 7272
	public UITextbox m_tip;

	// Token: 0x04001C69 RID: 7273
	protected string m_labelText;

	// Token: 0x04001C6A RID: 7274
	private int m_maxCharacterCount;
}
