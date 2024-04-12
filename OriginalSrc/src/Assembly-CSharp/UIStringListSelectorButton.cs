using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005A2 RID: 1442
public class UIStringListSelectorButton : UIVerticalListButton
{
	// Token: 0x060029D7 RID: 10711 RVA: 0x001B8180 File Offset: 0x001B6580
	public UIStringListSelectorButton(UIComponent _parent, string _tag, UIModel _model, string _selectedSTRINGLISTfieldName, string _selectionValue, string _selectionLabel, bool _registerAsBound = false)
		: base(_parent, null, null, _tag, _model, _selectedSTRINGLISTfieldName)
	{
		this.m_minValue = 0f;
		this.m_maxValue = 1f;
		this.m_selectionValue = _selectionValue;
		this.m_registerAsBound = _registerAsBound;
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
		List<string> list = (List<string>)this.GetValue();
		if (list.Contains(this.m_selectionValue))
		{
			this.m_isSelected = true;
		}
		else
		{
			this.m_isSelected = false;
		}
		this.m_label = new UIText(this, false, _tag, _selectionLabel, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(0f, 1f);
	}

	// Token: 0x060029D8 RID: 10712 RVA: 0x001B8252 File Offset: 0x001B6652
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		this.m_changeValue = true;
	}

	// Token: 0x060029D9 RID: 10713 RVA: 0x001B8262 File Offset: 0x001B6662
	protected override void OnTouchDragStart(TLTouch _touch)
	{
		base.OnTouchDragStart(_touch);
		this.m_changeValue = false;
	}

	// Token: 0x060029DA RID: 10714 RVA: 0x001B8274 File Offset: 0x001B6674
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (this.m_changeValue)
		{
			List<string> list = (List<string>)this.GetValue();
			if (list.Contains(this.m_selectionValue))
			{
				list.Remove(this.m_selectionValue);
				this.m_isSelected = false;
			}
			else
			{
				list.Add(this.m_selectionValue);
				this.m_isSelected = true;
			}
			this.SetValue(list);
			this.m_changeValue = false;
			this.d_Draw(this);
		}
		this.UnfreezeVerticalScroll(false);
	}

	// Token: 0x060029DB RID: 10715 RVA: 0x001B82FE File Offset: 0x001B66FE
	public override object GetValue()
	{
		if (this.m_model != null)
		{
			return this.m_model.GetValue(this, this.m_registerAsBound);
		}
		return null;
	}

	// Token: 0x060029DC RID: 10716 RVA: 0x001B8320 File Offset: 0x001B6720
	public override void OnValueChange(object _newValue, object _oldValue)
	{
		List<string> list = (List<string>)_newValue;
		if (list.Contains(this.m_selectionValue))
		{
			this.m_isSelected = true;
		}
		else
		{
			this.m_isSelected = false;
		}
	}

	// Token: 0x060029DD RID: 10717 RVA: 0x001B8358 File Offset: 0x001B6758
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(this.m_actualWidth, this.m_actualHeight * 0.95f, Vector2.zero);
		Color color;
		if (this.m_isSelected)
		{
			color..ctor(0.3f, 0.6f, 0.3f);
			if (this.m_highlight)
			{
				color..ctor(0.2f, 0.5f, 0.2f);
			}
		}
		else
		{
			color..ctor(0.4f, 0.4f, 0.4f);
			if (this.m_highlight)
			{
				color..ctor(0.3f, 0.3f, 0.3f);
			}
		}
		Camera camera = CameraS.m_uiCamera;
		if (this.m_parent != null)
		{
			camera = this.m_parent.m_camera;
		}
		uint num = DebugDraw.ColorToUInt(color);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, "UIComponent: Prefab", null);
	}

	// Token: 0x04002EFB RID: 12027
	protected string[] m_valueLabels;

	// Token: 0x04002EFC RID: 12028
	public float m_minValue;

	// Token: 0x04002EFD RID: 12029
	public float m_maxValue;

	// Token: 0x04002EFE RID: 12030
	protected bool m_tempVal;

	// Token: 0x04002EFF RID: 12031
	protected float m_currentFloatValue;

	// Token: 0x04002F00 RID: 12032
	protected bool m_changeValue;

	// Token: 0x04002F01 RID: 12033
	public UIText m_label;

	// Token: 0x04002F02 RID: 12034
	private string m_selectionValue;

	// Token: 0x04002F03 RID: 12035
	private bool m_isSelected;

	// Token: 0x04002F04 RID: 12036
	private bool m_registerAsBound;
}
