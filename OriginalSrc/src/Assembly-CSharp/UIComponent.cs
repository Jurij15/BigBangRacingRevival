using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005AF RID: 1455
public class UIComponent
{
	// Token: 0x06002A33 RID: 10803 RVA: 0x0003AE98 File Offset: 0x00039298
	public UIComponent(UIComponent _parent, bool _touchable, string _tag = "", Camera _camera = null, UIModel _model = null, string _fieldName = "")
	{
		UIComponent.m_instanceCount++;
		this.m_childs = new List<UIComponent>();
		this.m_camera = _camera;
		if (this.m_camera == null)
		{
			if (_parent != null)
			{
				this.m_camera = _parent.m_camera;
			}
			else
			{
				this.m_camera = CameraS.m_uiCamera;
			}
		}
		this.m_model = _model;
		this.m_fieldName = _fieldName;
		this.m_disabled = false;
		this.m_highlight = false;
		this.m_highlightSecondary = false;
		this.m_hit = false;
		this.m_preventHit = false;
		this.m_began = false;
		this.m_end = false;
		this.m_hittingTouch = null;
		this.m_width = 1f;
		this.m_height = 1f;
		this.m_horizontalAlign = 0.5f;
		this.m_verticalAlign = 0.5f;
		this.m_depthOffset = -1f;
		this.m_margins = new cpBB(0f);
		this.m_touchAreaSizeMultipler = 1f;
		if (_parent == null)
		{
			this.m_widthRelativeTo = RelativeTo.ScreenWidth;
			this.m_heightRelativeTo = RelativeTo.ScreenHeight;
		}
		else
		{
			this.m_widthRelativeTo = RelativeTo.ParentWidth;
			this.m_heightRelativeTo = RelativeTo.ParentHeight;
		}
		this.m_marginsRelativeTo = RelativeTo.ScreenShortest;
		Entity entity = EntityManager.AddEntity(new string[] { "UIComponent", _tag });
		entity.m_persistent = true;
		this.m_TC = TransformS.AddComponent(entity, "UI Component: " + this.ToString() + ": " + _tag);
		if (_touchable)
		{
			this.m_TAC = TouchAreaS.AddRectArea(this.m_TC, _tag, this.m_width, this.m_height, this.m_camera, null, default(Vector2));
			TouchAreaS.AddTouchEventListener(this.m_TAC, new TouchEventDelegate(this.TouchHandler));
		}
		this.d_Draw = new UIDrawDelegate(this.DrawHandler);
		this.Parent(_parent);
	}

	// Token: 0x06002A34 RID: 10804 RVA: 0x0003B06C File Offset: 0x0003946C
	~UIComponent()
	{
		UIComponent.m_instanceCount--;
	}

	// Token: 0x06002A35 RID: 10805 RVA: 0x0003B0A4 File Offset: 0x000394A4
	public virtual object GetValue()
	{
		if (this.m_model != null)
		{
			return this.m_model.GetValue(this, true);
		}
		return null;
	}

	// Token: 0x06002A36 RID: 10806 RVA: 0x0003B0C0 File Offset: 0x000394C0
	public virtual void SetValue(object _value)
	{
		if (this.m_model != null)
		{
			this.m_model.SetValue(_value, this);
		}
		else
		{
			Debug.LogError("No model associated with this component");
		}
	}

	// Token: 0x06002A37 RID: 10807 RVA: 0x0003B0E9 File Offset: 0x000394E9
	public virtual void OnValueChange(object _newValue, object _oldValue)
	{
	}

	// Token: 0x06002A38 RID: 10808 RVA: 0x0003B0EC File Offset: 0x000394EC
	public virtual void Parent(UIComponent _parent)
	{
		this.DetachFromParent(false);
		if (_parent != null)
		{
			this.m_parent = _parent;
			this.m_parent.m_childs.Add(this);
			TransformS.ParentComponent(this.m_TC, this.m_parent.m_TC);
			UIManager.m_uiComponents.Remove(this);
			if (this.m_camera != this.m_parent.m_camera)
			{
				this.SetCamera(this.m_parent.m_camera, true, false);
			}
			this.InheritParentClip();
		}
		else if (!UIManager.m_uiComponents.Contains(this))
		{
			UIManager.m_uiComponents.Add(this);
		}
	}

	// Token: 0x06002A39 RID: 10809 RVA: 0x0003B194 File Offset: 0x00039594
	public void MoveToIndexAtParentsChildList(int _index)
	{
		if (this.m_parent == null)
		{
			return;
		}
		this.m_parent.m_childs.Remove(this);
		this.m_parent.m_childs.Insert(_index, this);
	}

	// Token: 0x06002A3A RID: 10810 RVA: 0x0003B1C8 File Offset: 0x000395C8
	public virtual void InheritParentClip()
	{
		if (this.m_TAC == null)
		{
			return;
		}
		UIComponent uicomponent = this.m_parent;
		while (uicomponent != null && (uicomponent.m_TAC == null || (uicomponent.m_TAC != null && !uicomponent.m_TAC.m_clip)))
		{
			uicomponent = uicomponent.m_parent;
		}
		if (uicomponent != null && uicomponent.m_TAC != null && uicomponent.m_TAC.m_clip)
		{
			this.m_TAC.m_clip = true;
			this.m_TAC.m_clipBB = uicomponent.m_TAC.m_clipBB;
		}
	}

	// Token: 0x06002A3B RID: 10811 RVA: 0x0003B263 File Offset: 0x00039663
	public virtual void DetachFromParent()
	{
		this.DetachFromParent(true);
	}

	// Token: 0x06002A3C RID: 10812 RVA: 0x0003B26C File Offset: 0x0003966C
	public virtual void DetachFromParent(bool _addToManager)
	{
		if (this.m_parent != null)
		{
			this.m_parent.m_childs.Remove(this);
			TransformS.UnparentComponent(this.m_parent.m_TC, true);
			this.m_parent = null;
			this.m_widthRelativeTo = RelativeTo.ScreenWidth;
			this.m_heightRelativeTo = RelativeTo.ScreenHeight;
			if (this.m_TAC != null)
			{
				this.m_TAC.m_clip = false;
			}
		}
		if (_addToManager && !UIManager.m_uiComponents.Contains(this))
		{
			UIManager.m_uiComponents.Add(this);
		}
	}

	// Token: 0x06002A3D RID: 10813 RVA: 0x0003B2F4 File Offset: 0x000396F4
	public virtual void DestroyChildren()
	{
		while (this.m_childs.Count > 0)
		{
			int num = this.m_childs.Count - 1;
			this.m_childs[num].Destroy();
		}
	}

	// Token: 0x06002A3E RID: 10814 RVA: 0x0003B338 File Offset: 0x00039738
	public virtual void DestroyChildren(int _startIndex)
	{
		if (_startIndex < 0 || _startIndex >= this.m_childs.Count)
		{
			return;
		}
		for (int i = this.m_childs.Count - 1; i > _startIndex - 1; i--)
		{
			this.m_childs[i].Destroy();
		}
	}

	// Token: 0x06002A3F RID: 10815 RVA: 0x0003B390 File Offset: 0x00039790
	public virtual void DestroyChildren(int _startIndex, int _endIndex)
	{
		if (_startIndex < 0 || _startIndex > this.m_childs.Count || _endIndex < _startIndex || _endIndex > this.m_childs.Count)
		{
			return;
		}
		for (int i = _endIndex; i > _startIndex - 1; i--)
		{
			this.m_childs[i].Destroy();
		}
	}

	// Token: 0x06002A40 RID: 10816 RVA: 0x0003B3F3 File Offset: 0x000397F3
	public virtual void Destroy()
	{
		this.Destroy(true);
	}

	// Token: 0x06002A41 RID: 10817 RVA: 0x0003B3FC File Offset: 0x000397FC
	public virtual void Destroy(bool _removeFromManager)
	{
		this.DestroyChildren();
		EntityManager.RemoveEntity(this.m_TC.p_entity, true, true);
		this.m_TAC = null;
		if (this.m_parent != null)
		{
			this.m_parent.m_childs.Remove(this);
			this.m_parent = null;
		}
		if (_removeFromManager)
		{
			UIManager.m_uiComponents.Remove(this);
		}
		if (this.m_model != null)
		{
			this.m_model.RemoveBinding(this);
		}
		if (this.m_uniqueCamera)
		{
			CameraS.RemoveCamera(this.m_camera);
		}
		this.m_camera = null;
		this.m_hit = false;
		this.m_hittingTouch = null;
	}

	// Token: 0x06002A42 RID: 10818 RVA: 0x0003B4A0 File Offset: 0x000398A0
	public virtual UIComponent GetRoot()
	{
		UIComponent uicomponent = this;
		while (uicomponent.m_parent != null)
		{
			uicomponent = uicomponent.m_parent;
		}
		return uicomponent;
	}

	// Token: 0x06002A43 RID: 10819 RVA: 0x0003B4C8 File Offset: 0x000398C8
	public virtual void DestroyRoot()
	{
		UIComponent uicomponent = this;
		while (uicomponent.m_parent != null)
		{
			uicomponent = uicomponent.m_parent;
		}
		if (uicomponent != null)
		{
			uicomponent.Destroy();
		}
	}

	// Token: 0x06002A44 RID: 10820 RVA: 0x0003B4FA File Offset: 0x000398FA
	public virtual Camera GetUniqueCameraFromParentHierarchy()
	{
		if (this.m_uniqueCamera)
		{
			return this.m_camera;
		}
		if (this.m_parent != null)
		{
			return this.m_parent.GetUniqueCameraFromParentHierarchy();
		}
		return null;
	}

	// Token: 0x06002A45 RID: 10821 RVA: 0x0003B528 File Offset: 0x00039928
	public virtual void SetCamera(Camera _camera, bool _affectChildren = false, bool _uniqueCamera = true)
	{
		if (this.m_parent == null && _affectChildren && this.m_uniqueCamera)
		{
			Object.Destroy(this.m_camera);
		}
		this.m_uniqueCamera = _uniqueCamera;
		this.m_camera = _camera;
		this.m_TC.transform.gameObject.layer = _camera.gameObject.layer;
		if (this.m_TAC != null)
		{
			TouchAreaS.SetCamera(this.m_TAC, _camera);
		}
		if (_affectChildren)
		{
			for (int i = 0; i < this.m_childs.Count; i++)
			{
				this.m_childs[i].SetCamera(_camera, _affectChildren, false);
			}
		}
	}

	// Token: 0x06002A46 RID: 10822 RVA: 0x0003B5D7 File Offset: 0x000399D7
	public virtual void SetSize(float _widthRatio, float _heightRatio, RelativeTo _relativeTo)
	{
		this.SetWidth(_widthRatio, _relativeTo);
		this.SetHeight(_heightRatio, _relativeTo);
	}

	// Token: 0x06002A47 RID: 10823 RVA: 0x0003B5EC File Offset: 0x000399EC
	public virtual void SetWidth(float _widthRatio, RelativeTo _relativeTo)
	{
		if (_relativeTo == RelativeTo.OwnWidth)
		{
			_relativeTo = RelativeTo.ParentWidth;
			Debug.LogWarning("Can't set width relative to own width. Changing to parent width.");
		}
		if (this.m_parent == null && _relativeTo == RelativeTo.ParentWidth)
		{
			_relativeTo = RelativeTo.ScreenWidth;
		}
		else if (this.m_parent == null && _relativeTo == RelativeTo.ParentHeight)
		{
			_relativeTo = RelativeTo.ScreenHeight;
		}
		this.m_widthRelativeTo = _relativeTo;
		if (_relativeTo == RelativeTo.OwnHeight && this.m_heightRelativeTo == RelativeTo.OwnWidth)
		{
			this.m_heightRelativeTo = RelativeTo.ParentHeight;
			Debug.LogWarning("Setting height relative to parent height.");
		}
		this.m_width = _widthRatio;
	}

	// Token: 0x06002A48 RID: 10824 RVA: 0x0003B670 File Offset: 0x00039A70
	public virtual void SetHeight(float _heightRatio, RelativeTo _relativeTo)
	{
		if (_relativeTo == RelativeTo.OwnHeight)
		{
			_relativeTo = RelativeTo.ParentHeight;
			Debug.LogWarning("Can't set height relative to own height. Changing to parent height.");
		}
		if (this.m_parent == null && _relativeTo == RelativeTo.ParentWidth)
		{
			_relativeTo = RelativeTo.ScreenWidth;
		}
		else if (this.m_parent == null && _relativeTo == RelativeTo.ParentHeight)
		{
			_relativeTo = RelativeTo.ScreenHeight;
		}
		this.m_heightRelativeTo = _relativeTo;
		if (_relativeTo == RelativeTo.OwnWidth && this.m_widthRelativeTo == RelativeTo.OwnHeight)
		{
			this.m_widthRelativeTo = RelativeTo.ParentWidth;
			Debug.LogWarning("Setting width relative to parent width.");
		}
		this.m_height = _heightRatio;
	}

	// Token: 0x06002A49 RID: 10825 RVA: 0x0003B6F3 File Offset: 0x00039AF3
	public virtual void SetMargins(cpBB _marginsBB, RelativeTo _relativeTo = RelativeTo.ScreenHeight)
	{
		this.m_margins = _marginsBB;
		this.m_marginsRelativeTo = _relativeTo;
	}

	// Token: 0x06002A4A RID: 10826 RVA: 0x0003B703 File Offset: 0x00039B03
	public virtual void SetMargins(float _left, float _right, float _top, float _bottom, RelativeTo _relativeTo = RelativeTo.ScreenHeight)
	{
		this.m_margins.l = _left;
		this.m_margins.r = _right;
		this.m_margins.t = _top;
		this.m_margins.b = _bottom;
		this.m_marginsRelativeTo = _relativeTo;
	}

	// Token: 0x06002A4B RID: 10827 RVA: 0x0003B73E File Offset: 0x00039B3E
	public virtual void SetMargins(float _margin, RelativeTo _relativeTo = RelativeTo.ScreenHeight)
	{
		this.m_margins.l = _margin;
		this.m_margins.r = _margin;
		this.m_margins.t = _margin;
		this.m_margins.b = _margin;
		this.m_marginsRelativeTo = _relativeTo;
	}

	// Token: 0x06002A4C RID: 10828 RVA: 0x0003B777 File Offset: 0x00039B77
	public virtual void SetAlign(float _horizontal, float _vertical)
	{
		this.SetHorizontalAlign(_horizontal);
		this.SetVerticalAlign(_vertical);
	}

	// Token: 0x06002A4D RID: 10829 RVA: 0x0003B787 File Offset: 0x00039B87
	public virtual void SetTouchAreaSizeMultipler(float _sizeMultipler)
	{
		this.m_touchAreaSizeMultipler = _sizeMultipler;
	}

	// Token: 0x06002A4E RID: 10830 RVA: 0x0003B790 File Offset: 0x00039B90
	public virtual void SetHorizontalAlign(float _horizontal)
	{
		this.m_horizontalAlign = _horizontal;
	}

	// Token: 0x06002A4F RID: 10831 RVA: 0x0003B799 File Offset: 0x00039B99
	public virtual void SetVerticalAlign(float _vertical)
	{
		this.m_verticalAlign = _vertical;
	}

	// Token: 0x06002A50 RID: 10832 RVA: 0x0003B7A2 File Offset: 0x00039BA2
	public virtual void SetDepthOffset(float _offset)
	{
		this.m_depthOffset = _offset;
	}

	// Token: 0x06002A51 RID: 10833 RVA: 0x0003B7AC File Offset: 0x00039BAC
	public virtual void EnableTouchAreas(bool _affectChildren = true)
	{
		if (this.m_TAC != null && this.m_TAC.m_wasActive)
		{
			this.m_TAC.m_active = true;
		}
		if (_affectChildren)
		{
			for (int i = 0; i < this.m_childs.Count; i++)
			{
				this.m_childs[i].EnableTouchAreas(_affectChildren);
			}
		}
	}

	// Token: 0x06002A52 RID: 10834 RVA: 0x0003B814 File Offset: 0x00039C14
	public virtual void DisableTouchAreas(bool _affectChildren = true)
	{
		if (this.m_TAC != null)
		{
			this.m_TAC.m_active = false;
		}
		if (_affectChildren)
		{
			for (int i = 0; i < this.m_childs.Count; i++)
			{
				this.m_childs[i].DisableTouchAreas(_affectChildren);
			}
		}
	}

	// Token: 0x06002A53 RID: 10835 RVA: 0x0003B86C File Offset: 0x00039C6C
	public virtual void CreateTouchAreas()
	{
		if (this.m_TAC == null)
		{
			this.m_TAC = TouchAreaS.AddRectArea(this.m_TC, this.m_TC.transform.name, this.m_width, this.m_height, this.m_camera, null, default(Vector2));
			TouchAreaS.AddTouchEventListener(this.m_TAC, new TouchEventDelegate(this.TouchHandler));
		}
	}

	// Token: 0x06002A54 RID: 10836 RVA: 0x0003B8D9 File Offset: 0x00039CD9
	public virtual void RemoveTouchAreas()
	{
		if (this.m_TAC != null)
		{
			TouchAreaS.RemoveArea(this.m_TAC);
			this.m_TAC = null;
		}
	}

	// Token: 0x06002A55 RID: 10837 RVA: 0x0003B8F8 File Offset: 0x00039CF8
	public virtual void PreventChildrenHit(bool _value)
	{
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			this.m_childs[i].PreventHit(_value, true);
		}
	}

	// Token: 0x06002A56 RID: 10838 RVA: 0x0003B934 File Offset: 0x00039D34
	public virtual void PreventHit(bool _value, bool _affectChildren)
	{
		this.m_preventHit = _value;
		if (_affectChildren)
		{
			this.PreventChildrenHit(_value);
		}
	}

	// Token: 0x06002A57 RID: 10839 RVA: 0x0003B94A File Offset: 0x00039D4A
	public virtual void Focus()
	{
	}

	// Token: 0x06002A58 RID: 10840 RVA: 0x0003B94C File Offset: 0x00039D4C
	public virtual void SetListenCameraEvents(bool _listen)
	{
		this.m_listenCameraEvents = _listen;
	}

	// Token: 0x06002A59 RID: 10841 RVA: 0x0003B955 File Offset: 0x00039D55
	public virtual void EnterCamera()
	{
		this.m_onCamera = true;
		this.Show();
	}

	// Token: 0x06002A5A RID: 10842 RVA: 0x0003B964 File Offset: 0x00039D64
	public virtual void ExitCamera()
	{
		this.m_onCamera = false;
		this.Hide();
	}

	// Token: 0x06002A5B RID: 10843 RVA: 0x0003B973 File Offset: 0x00039D73
	public virtual void Hide()
	{
		this.m_hidden = true;
	}

	// Token: 0x06002A5C RID: 10844 RVA: 0x0003B97C File Offset: 0x00039D7C
	public virtual void Show()
	{
		this.m_hidden = false;
	}

	// Token: 0x06002A5D RID: 10845 RVA: 0x0003B985 File Offset: 0x00039D85
	public virtual void FreezeHorizontalScroll(bool _affectWholeHierarchy)
	{
		if (this.m_parent != null)
		{
			this.m_parent.FreezeHorizontalScroll(_affectWholeHierarchy);
		}
	}

	// Token: 0x06002A5E RID: 10846 RVA: 0x0003B99E File Offset: 0x00039D9E
	public virtual void UnfreezeHorizontalScroll(bool _affectWholeHierarchy)
	{
		if (this.m_parent != null)
		{
			this.m_parent.UnfreezeHorizontalScroll(_affectWholeHierarchy);
		}
	}

	// Token: 0x06002A5F RID: 10847 RVA: 0x0003B9B7 File Offset: 0x00039DB7
	public virtual void FreezeVerticalScroll(bool _affectWholeHierarchy)
	{
		if (this.m_parent != null)
		{
			this.m_parent.FreezeVerticalScroll(_affectWholeHierarchy);
		}
	}

	// Token: 0x06002A60 RID: 10848 RVA: 0x0003B9D0 File Offset: 0x00039DD0
	public virtual void UnfreezeVerticalScroll(bool _affectWholeHierarchy)
	{
		if (this.m_parent != null)
		{
			this.m_parent.UnfreezeVerticalScroll(_affectWholeHierarchy);
		}
	}

	// Token: 0x06002A61 RID: 10849 RVA: 0x0003B9E9 File Offset: 0x00039DE9
	public virtual void SetActive(bool _value)
	{
		this.Activate(_value);
	}

	// Token: 0x06002A62 RID: 10850 RVA: 0x0003B9F2 File Offset: 0x00039DF2
	protected virtual void Activate(bool _value)
	{
		this.m_disabled = !_value;
	}

	// Token: 0x06002A63 RID: 10851 RVA: 0x0003B9FE File Offset: 0x00039DFE
	protected virtual void Highlight(bool _value)
	{
		this.m_highlight = _value;
		this.m_isDown = _value;
	}

	// Token: 0x06002A64 RID: 10852 RVA: 0x0003BA0E File Offset: 0x00039E0E
	protected virtual void HighlightSecondary(bool _value)
	{
		this.m_highlightSecondary = _value;
		this.m_isDown = _value;
	}

	// Token: 0x06002A65 RID: 10853 RVA: 0x0003BA1E File Offset: 0x00039E1E
	protected virtual void Hit(TLTouch _hittingTouch)
	{
		this.m_hit = true;
		this.m_hittingTouch = _hittingTouch;
	}

	// Token: 0x06002A66 RID: 10854 RVA: 0x0003BA2E File Offset: 0x00039E2E
	public void SetRogue()
	{
		this.m_rogue = true;
	}

	// Token: 0x06002A67 RID: 10855 RVA: 0x0003BA38 File Offset: 0x00039E38
	public virtual void UpdateSize()
	{
		if (this.m_widthRelativeTo == RelativeTo.OwnHeight)
		{
			this.m_actualHeight = this.m_height * this.m_tempReferenceHeight;
			this.m_actualWidth = this.m_actualHeight * this.m_width;
		}
		else if (this.m_heightRelativeTo == RelativeTo.OwnWidth)
		{
			this.m_actualWidth = this.m_width * this.m_tempReferenceWidth;
			this.m_actualHeight = this.m_actualWidth * this.m_height;
		}
		else if (this.m_widthRelativeTo == RelativeTo.World)
		{
			this.m_actualWidth = this.m_width;
			if (this.m_heightRelativeTo == RelativeTo.World)
			{
				this.m_actualHeight = this.m_height;
			}
			else
			{
				this.m_actualHeight = this.m_height * this.m_tempReferenceHeight;
			}
		}
		else if (this.m_heightRelativeTo == RelativeTo.World)
		{
			this.m_actualHeight = this.m_height;
			if (this.m_widthRelativeTo == RelativeTo.World)
			{
				this.m_actualWidth = this.m_width;
			}
			else
			{
				this.m_actualWidth = this.m_width * this.m_tempReferenceWidth;
			}
		}
		else
		{
			this.m_actualWidth = this.m_width * this.m_tempReferenceWidth;
			this.m_actualHeight = this.m_height * this.m_tempReferenceHeight;
		}
		this.UpdateCollider();
	}

	// Token: 0x06002A68 RID: 10856 RVA: 0x0003BB80 File Offset: 0x00039F80
	public virtual void UpdateCollider()
	{
		if (this.m_TAC != null)
		{
			if (this.m_TAC.m_colliderShape == ColliderShape.Rect)
			{
				TouchAreaS.ResizeRectCollider(this.m_TAC, this.m_actualWidth * this.m_touchAreaSizeMultipler, this.m_actualHeight * this.m_touchAreaSizeMultipler, default(Vector2));
			}
			else if (this.m_TAC.m_colliderShape == ColliderShape.Circle)
			{
				TouchAreaS.ResizeCircleCollider(this.m_TAC, Mathf.Max(this.m_actualWidth, this.m_actualHeight) * this.m_touchAreaSizeMultipler * 0.5f);
			}
		}
	}

	// Token: 0x06002A69 RID: 10857 RVA: 0x0003BC18 File Offset: 0x0003A018
	public virtual void UpdateAlign()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		if (this.m_parent != null)
		{
			num = this.m_parent.m_actualWidth - this.m_tempParentMargins.l - this.m_tempParentMargins.r;
			num2 = this.m_parent.m_actualHeight - this.m_tempParentMargins.b - this.m_tempParentMargins.t;
		}
		float num3 = (num - this.m_actualWidth) * (-0.5f + this.m_horizontalAlign) + (this.m_tempParentMargins.l - this.m_tempParentMargins.r) * 0.5f;
		float num4 = (num2 - this.m_actualHeight) * (-0.5f + this.m_verticalAlign) + (this.m_tempParentMargins.b - this.m_tempParentMargins.t) * 0.5f;
		Vector3 vector;
		vector..ctor(num3, num4, this.m_depthOffset);
		TransformS.SetPosition(this.m_TC, vector);
	}

	// Token: 0x06002A6A RID: 10858 RVA: 0x0003BD0C File Offset: 0x0003A10C
	public virtual void UpdateHorizontalAlign(float _yPos)
	{
		float num = (float)Screen.width;
		if (this.m_parent != null)
		{
			num = this.m_parent.m_actualWidth - this.m_tempParentMargins.l - this.m_tempParentMargins.r;
		}
		float num2 = (num - this.m_actualWidth) * (-0.5f + this.m_horizontalAlign) + (this.m_tempParentMargins.l - this.m_tempParentMargins.r) * 0.5f;
		Vector3 vector;
		vector..ctor(num2, _yPos, this.m_depthOffset);
		TransformS.SetPosition(this.m_TC, vector);
	}

	// Token: 0x06002A6B RID: 10859 RVA: 0x0003BDA0 File Offset: 0x0003A1A0
	public virtual void UpdateVerticalAlign(float _xPos)
	{
		float num = (float)Screen.height;
		if (this.m_parent != null)
		{
			num = this.m_parent.m_actualHeight - this.m_tempParentMargins.b - this.m_tempParentMargins.t;
		}
		float num2 = (num - this.m_actualHeight) * (-0.5f + this.m_verticalAlign) + (this.m_tempParentMargins.b - this.m_tempParentMargins.t) * 0.5f;
		Vector3 vector;
		vector..ctor(_xPos, num2, this.m_depthOffset);
		TransformS.SetPosition(this.m_TC, vector);
	}

	// Token: 0x06002A6C RID: 10860 RVA: 0x0003BE34 File Offset: 0x0003A234
	public virtual void UpdateMargins()
	{
		float num = 0f;
		if (this.m_tempMarginsRelativeTo == RelativeTo.ParentWidth)
		{
			num = this.m_parent.m_actualWidth;
		}
		else if (this.m_tempMarginsRelativeTo == RelativeTo.ParentHeight)
		{
			num = this.m_parent.m_actualHeight;
		}
		else if (this.m_tempMarginsRelativeTo == RelativeTo.ScreenHeight)
		{
			num = (float)Screen.height;
		}
		else if (this.m_tempMarginsRelativeTo == RelativeTo.ScreenWidth)
		{
			num = (float)Screen.width;
		}
		else if (this.m_tempMarginsRelativeTo == RelativeTo.OwnHeight)
		{
			num = this.m_actualHeight;
		}
		else if (this.m_tempMarginsRelativeTo == RelativeTo.OwnWidth)
		{
			num = this.m_actualWidth;
		}
		else if (this.m_tempMarginsRelativeTo == RelativeTo.World)
		{
			num = 1f;
		}
		this.m_actualMargins.l = this.m_margins.l * num;
		this.m_actualMargins.r = this.m_margins.r * num;
		this.m_actualMargins.t = this.m_margins.t * num;
		this.m_actualMargins.b = this.m_margins.b * num;
	}

	// Token: 0x06002A6D RID: 10861 RVA: 0x0003BF54 File Offset: 0x0003A354
	public virtual void CalculateReferenceSizes()
	{
		this.m_tempParentMargins = default(cpBB);
		if (this.m_parent != null)
		{
			this.m_tempParentMargins = this.m_parent.m_actualMargins;
		}
		this.m_tempWidthRelativeTo = this.m_widthRelativeTo;
		if (this.m_widthRelativeTo == RelativeTo.ParentShortest)
		{
			this.m_tempWidthRelativeTo = ((this.m_parent.m_actualWidth <= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
		}
		else if (this.m_widthRelativeTo == RelativeTo.ParentLongest)
		{
			this.m_tempWidthRelativeTo = ((this.m_parent.m_actualWidth >= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
		}
		else if (this.m_widthRelativeTo == RelativeTo.ScreenShortest)
		{
			this.m_tempWidthRelativeTo = ((Screen.width <= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
		}
		else if (this.m_widthRelativeTo == RelativeTo.ScreenLongest)
		{
			this.m_tempWidthRelativeTo = ((Screen.width >= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
		}
		this.m_tempHeightRelativeTo = this.m_heightRelativeTo;
		if (this.m_heightRelativeTo == RelativeTo.ParentShortest)
		{
			this.m_tempHeightRelativeTo = ((this.m_parent.m_actualWidth <= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
		}
		else if (this.m_heightRelativeTo == RelativeTo.ParentLongest)
		{
			this.m_tempHeightRelativeTo = ((this.m_parent.m_actualWidth >= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
		}
		else if (this.m_heightRelativeTo == RelativeTo.ScreenShortest)
		{
			this.m_tempHeightRelativeTo = ((Screen.width <= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
		}
		else if (this.m_heightRelativeTo == RelativeTo.ScreenLongest)
		{
			this.m_tempHeightRelativeTo = ((Screen.width >= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
		}
		this.m_tempMarginsRelativeTo = this.m_marginsRelativeTo;
		if (this.m_marginsRelativeTo == RelativeTo.ParentShortest)
		{
			this.m_tempMarginsRelativeTo = ((this.m_parent.m_actualWidth <= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
		}
		else if (this.m_marginsRelativeTo == RelativeTo.ParentLongest)
		{
			this.m_tempMarginsRelativeTo = ((this.m_parent.m_actualWidth >= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
		}
		else if (this.m_marginsRelativeTo == RelativeTo.ScreenShortest)
		{
			this.m_tempMarginsRelativeTo = ((Screen.width <= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
		}
		else if (this.m_marginsRelativeTo == RelativeTo.ScreenLongest)
		{
			this.m_tempMarginsRelativeTo = ((Screen.width >= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
		}
		this.m_tempReferenceWidth = (float)Screen.width;
		if (this.m_tempWidthRelativeTo == RelativeTo.ScreenWidth)
		{
			this.m_tempReferenceWidth = (float)Screen.width;
		}
		else if (this.m_tempWidthRelativeTo == RelativeTo.ScreenHeight)
		{
			this.m_tempReferenceWidth = (float)Screen.height;
		}
		else if (this.m_tempWidthRelativeTo == RelativeTo.World)
		{
			this.m_tempReferenceWidth = 1f;
		}
		else if (this.m_tempWidthRelativeTo == RelativeTo.ParentWidth)
		{
			this.m_tempReferenceWidth = this.m_parent.m_actualWidth - this.m_tempParentMargins.l - this.m_tempParentMargins.r;
		}
		else if (this.m_tempWidthRelativeTo == RelativeTo.ParentHeight)
		{
			this.m_tempReferenceWidth = this.m_parent.m_actualHeight - this.m_tempParentMargins.b - this.m_tempParentMargins.t;
		}
		this.m_tempReferenceHeight = (float)Screen.height;
		if (this.m_tempHeightRelativeTo == RelativeTo.ScreenWidth)
		{
			this.m_tempReferenceHeight = (float)Screen.width;
		}
		else if (this.m_tempHeightRelativeTo == RelativeTo.ScreenHeight)
		{
			this.m_tempReferenceHeight = (float)Screen.height;
		}
		else if (this.m_tempHeightRelativeTo == RelativeTo.ParentWidth)
		{
			this.m_tempReferenceHeight = this.m_parent.m_actualWidth - this.m_tempParentMargins.l - this.m_tempParentMargins.r;
		}
		else if (this.m_tempHeightRelativeTo == RelativeTo.World)
		{
			this.m_tempReferenceHeight = 1f;
		}
		else if (this.m_tempHeightRelativeTo == RelativeTo.ParentHeight)
		{
			this.m_tempReferenceHeight = this.m_parent.m_actualHeight - this.m_tempParentMargins.b - this.m_tempParentMargins.t;
		}
	}

	// Token: 0x06002A6E RID: 10862 RVA: 0x0003C3A8 File Offset: 0x0003A7A8
	public virtual void UpdateChildren()
	{
		this.InheritParentClip();
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			this.m_childs[i].Update();
		}
	}

	// Token: 0x06002A6F RID: 10863 RVA: 0x0003C3E8 File Offset: 0x0003A7E8
	public virtual void UpdateChildrenSizes()
	{
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			this.m_childs[i].CalculateReferenceSizes();
			this.m_childs[i].UpdateSize();
			this.m_childs[i].ArrangeContents();
		}
	}

	// Token: 0x06002A70 RID: 10864 RVA: 0x0003C444 File Offset: 0x0003A844
	public virtual void UpdateChildrenAlign()
	{
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			this.m_childs[i].UpdateAlign();
		}
	}

	// Token: 0x06002A71 RID: 10865 RVA: 0x0003C480 File Offset: 0x0003A880
	public virtual void ChildrenInheritParentClip()
	{
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			this.m_childs[i].InheritParentClip();
			this.m_childs[i].ChildrenInheritParentClip();
		}
	}

	// Token: 0x06002A72 RID: 10866 RVA: 0x0003C4CB File Offset: 0x0003A8CB
	public virtual void ArrangeContents()
	{
	}

	// Token: 0x06002A73 RID: 10867 RVA: 0x0003C4CD File Offset: 0x0003A8CD
	public virtual void UpdateUniqueCamera()
	{
	}

	// Token: 0x06002A74 RID: 10868 RVA: 0x0003C4D0 File Offset: 0x0003A8D0
	public virtual void Update()
	{
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateAlign();
		this.UpdateMargins();
		if (!this.m_hidden && this.d_Draw != null)
		{
			this.d_Draw(this);
		}
		this.UpdateChildren();
		this.ArrangeContents();
		if (this.m_parent == null)
		{
			this.UpdateSpecial();
		}
		this.UpdateUniqueCamera();
	}

	// Token: 0x06002A75 RID: 10869 RVA: 0x0003C53C File Offset: 0x0003A93C
	public virtual void UpdateSpecial()
	{
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			this.m_childs[i].UpdateSpecial();
		}
	}

	// Token: 0x06002A76 RID: 10870 RVA: 0x0003C578 File Offset: 0x0003A978
	public virtual void Step()
	{
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			this.m_childs[i].Step();
		}
		this.m_began = false;
		this.m_end = false;
		this.m_hit = false;
		this.m_hittingTouch = null;
	}

	// Token: 0x06002A77 RID: 10871 RVA: 0x0003C5CE File Offset: 0x0003A9CE
	protected virtual void OnTouchBegan(TLTouch _touch)
	{
		this.m_began = true;
		this.m_end = false;
		this.Highlight(true);
	}

	// Token: 0x06002A78 RID: 10872 RVA: 0x0003C5E5 File Offset: 0x0003A9E5
	protected virtual void OnTouchMove(TLTouch _touch, bool _inside)
	{
	}

	// Token: 0x06002A79 RID: 10873 RVA: 0x0003C5E7 File Offset: 0x0003A9E7
	protected virtual void OnTouchStationary(TLTouch _touch, bool _inside)
	{
	}

	// Token: 0x06002A7A RID: 10874 RVA: 0x0003C5EC File Offset: 0x0003A9EC
	protected virtual void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		if (this.m_end)
		{
			return;
		}
		if (_inside)
		{
			if (!this.m_preventHit || (this.m_preventHit && !_touch.m_dragged))
			{
				this.Hit(_touch);
			}
			this.Highlight(false);
			this.HighlightSecondary(false);
		}
		this.m_end = true;
	}

	// Token: 0x06002A7B RID: 10875 RVA: 0x0003C648 File Offset: 0x0003AA48
	protected virtual void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		if (this.m_end)
		{
			return;
		}
		if (_secondary)
		{
			this.m_began = true;
			this.HighlightSecondary(true);
		}
		else
		{
			this.m_began = true;
			this.Highlight(true);
		}
	}

	// Token: 0x06002A7C RID: 10876 RVA: 0x0003C67D File Offset: 0x0003AA7D
	protected virtual void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		if (this.m_end)
		{
			return;
		}
		if (_secondary)
		{
			this.m_end = true;
			this.HighlightSecondary(false);
		}
		else
		{
			this.m_end = true;
			this.Highlight(false);
		}
	}

	// Token: 0x06002A7D RID: 10877 RVA: 0x0003C6B2 File Offset: 0x0003AAB2
	protected virtual void OnTouchDragStart(TLTouch _touch)
	{
		this.m_dragged = true;
	}

	// Token: 0x06002A7E RID: 10878 RVA: 0x0003C6BB File Offset: 0x0003AABB
	protected virtual void OnTouchDragEnd(TLTouch _touch)
	{
		this.m_dragged = false;
	}

	// Token: 0x06002A7F RID: 10879 RVA: 0x0003C6C4 File Offset: 0x0003AAC4
	public virtual void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (!this.m_disabled)
		{
			TLTouch tltouch = _touches[0];
			if (_touchIsSecondary)
			{
				if (_touchPhase == TouchAreaPhase.StationaryIn)
				{
					this.OnTouchStationary(tltouch, true);
				}
				else if (_touchPhase == TouchAreaPhase.StationaryOut)
				{
					this.OnTouchStationary(tltouch, false);
				}
				else if (_touchPhase == TouchAreaPhase.RollIn)
				{
					this.OnTouchRollIn(tltouch, true);
				}
				else if (_touchPhase == TouchAreaPhase.RollOut)
				{
					this.OnTouchRollOut(tltouch, true);
				}
			}
			else if (_touchPhase == TouchAreaPhase.MoveIn)
			{
				this.OnTouchMove(tltouch, true);
			}
			else if (_touchPhase == TouchAreaPhase.MoveOut)
			{
				this.OnTouchMove(tltouch, false);
			}
			else if (_touchPhase == TouchAreaPhase.StationaryIn)
			{
				this.OnTouchStationary(tltouch, true);
			}
			else if (_touchPhase == TouchAreaPhase.StationaryOut)
			{
				this.OnTouchStationary(tltouch, false);
			}
			else if (_touchPhase == TouchAreaPhase.Began)
			{
				this.OnTouchBegan(tltouch);
			}
			else if (_touchPhase == TouchAreaPhase.ReleaseIn)
			{
				this.OnTouchRelease(tltouch, true);
			}
			else if (_touchPhase == TouchAreaPhase.ReleaseOut)
			{
				this.OnTouchRelease(tltouch, false);
			}
			else if (_touchPhase == TouchAreaPhase.RollIn)
			{
				this.OnTouchRollIn(tltouch, false);
			}
			else if (_touchPhase == TouchAreaPhase.RollOut)
			{
				this.OnTouchRollOut(tltouch, false);
			}
			else if (_touchPhase == TouchAreaPhase.DragStart)
			{
				this.OnTouchDragStart(tltouch);
			}
			else if (_touchPhase == TouchAreaPhase.DragEnd)
			{
				this.OnTouchDragEnd(tltouch);
			}
		}
	}

	// Token: 0x06002A80 RID: 10880 RVA: 0x0003C812 File Offset: 0x0003AC12
	public virtual void SetDrawHandler(UIDrawDelegate _handler)
	{
		this.RemoveDrawHandler();
		if (_handler != null)
		{
			this.d_Draw = (UIDrawDelegate)Delegate.Combine(this.d_Draw, _handler);
		}
	}

	// Token: 0x06002A81 RID: 10881 RVA: 0x0003C837 File Offset: 0x0003AC37
	public virtual void SetTouchHandler(TouchEventDelegate _handler)
	{
		if (this.m_TAC != null)
		{
			TouchAreaS.RemoveAllTouchEventListeners(this.m_TAC);
			TouchAreaS.AddTouchEventListener(this.m_TAC, _handler);
		}
		else
		{
			Debug.LogWarning("UI element is not set touchable");
		}
	}

	// Token: 0x06002A82 RID: 10882 RVA: 0x0003C86C File Offset: 0x0003AC6C
	public virtual void RemoveDrawHandler()
	{
		if (this.d_Draw != null)
		{
			Delegate[] invocationList = this.d_Draw.GetInvocationList();
			foreach (Delegate @delegate in invocationList)
			{
				this.d_Draw = (UIDrawDelegate)Delegate.Remove(this.d_Draw, (UIDrawDelegate)@delegate);
			}
		}
	}

	// Token: 0x06002A83 RID: 10883 RVA: 0x0003C8C8 File Offset: 0x0003ACC8
	public virtual void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		DebugDraw.CreateBox(this.m_camera, this.m_TC, Vector2.zero, this.m_actualWidth, this.m_actualHeight, false);
		if (this.m_highlight)
		{
			SpriteS.SetColorByTransformComponent(this.m_TC, Color.green, false, false);
		}
		else if (this.m_highlightSecondary)
		{
			SpriteS.SetColorByTransformComponent(this.m_TC, Color.yellow, false, false);
		}
		else if (this.m_hit)
		{
			SpriteS.SetColorByTransformComponent(this.m_TC, Color.cyan, false, false);
		}
		Camera camera = this.m_camera;
		if (this.m_parent != null)
		{
			camera = this.m_parent.m_camera;
		}
		SpriteS.ConvertSpritesToPrefabComponent(this.m_TC, camera, true, null);
	}

	// Token: 0x04002F92 RID: 12178
	public static int m_instanceCount;

	// Token: 0x04002F93 RID: 12179
	public bool m_uniqueCamera;

	// Token: 0x04002F94 RID: 12180
	public Camera m_camera;

	// Token: 0x04002F95 RID: 12181
	public bool m_listenCameraEvents;

	// Token: 0x04002F96 RID: 12182
	public bool m_hidden;

	// Token: 0x04002F97 RID: 12183
	public bool m_onCamera;

	// Token: 0x04002F98 RID: 12184
	public TransformC m_TC;

	// Token: 0x04002F99 RID: 12185
	public TouchAreaC m_TAC;

	// Token: 0x04002F9A RID: 12186
	public UIDrawDelegate d_Draw;

	// Token: 0x04002F9B RID: 12187
	public UIModel m_model;

	// Token: 0x04002F9C RID: 12188
	public string m_fieldName;

	// Token: 0x04002F9D RID: 12189
	public string m_identifier;

	// Token: 0x04002F9E RID: 12190
	public UIComponent m_parent;

	// Token: 0x04002F9F RID: 12191
	public List<UIComponent> m_childs;

	// Token: 0x04002FA0 RID: 12192
	public bool m_disabled;

	// Token: 0x04002FA1 RID: 12193
	public bool m_highlight;

	// Token: 0x04002FA2 RID: 12194
	public bool m_highlightSecondary;

	// Token: 0x04002FA3 RID: 12195
	public bool m_hit;

	// Token: 0x04002FA4 RID: 12196
	public bool m_preventHit;

	// Token: 0x04002FA5 RID: 12197
	public bool m_began;

	// Token: 0x04002FA6 RID: 12198
	public bool m_end;

	// Token: 0x04002FA7 RID: 12199
	public bool m_isDown;

	// Token: 0x04002FA8 RID: 12200
	public bool m_dragged;

	// Token: 0x04002FA9 RID: 12201
	public bool m_rogue;

	// Token: 0x04002FAA RID: 12202
	public TLTouch m_hittingTouch;

	// Token: 0x04002FAB RID: 12203
	public float m_width;

	// Token: 0x04002FAC RID: 12204
	public float m_height;

	// Token: 0x04002FAD RID: 12205
	public float m_actualWidth;

	// Token: 0x04002FAE RID: 12206
	public float m_actualHeight;

	// Token: 0x04002FAF RID: 12207
	public RelativeTo m_widthRelativeTo;

	// Token: 0x04002FB0 RID: 12208
	public RelativeTo m_heightRelativeTo;

	// Token: 0x04002FB1 RID: 12209
	public cpBB m_margins;

	// Token: 0x04002FB2 RID: 12210
	public cpBB m_actualMargins;

	// Token: 0x04002FB3 RID: 12211
	public RelativeTo m_marginsRelativeTo;

	// Token: 0x04002FB4 RID: 12212
	protected float m_horizontalAlign;

	// Token: 0x04002FB5 RID: 12213
	protected float m_verticalAlign;

	// Token: 0x04002FB6 RID: 12214
	protected float m_depthOffset;

	// Token: 0x04002FB7 RID: 12215
	protected RelativeTo m_tempWidthRelativeTo;

	// Token: 0x04002FB8 RID: 12216
	protected RelativeTo m_tempHeightRelativeTo;

	// Token: 0x04002FB9 RID: 12217
	protected RelativeTo m_tempMarginsRelativeTo;

	// Token: 0x04002FBA RID: 12218
	protected float m_tempReferenceWidth;

	// Token: 0x04002FBB RID: 12219
	protected float m_tempReferenceHeight;

	// Token: 0x04002FBC RID: 12220
	protected cpBB m_tempParentMargins;

	// Token: 0x04002FBD RID: 12221
	protected float m_touchAreaSizeMultipler;
}
