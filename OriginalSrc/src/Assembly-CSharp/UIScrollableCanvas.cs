using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200058B RID: 1419
public class UIScrollableCanvas : UICanvas
{
	// Token: 0x0600294E RID: 10574 RVA: 0x000BFD24 File Offset: 0x000BE124
	public UIScrollableCanvas(UIComponent _parent, string _tag)
		: base(_parent, true, _tag, null, string.Empty)
	{
		this.m_TAC.m_allowSecondary = true;
		this.m_TAC.m_clip = true;
		this.SetScrollable();
		this.SetScrollPosition(0f, 0f);
		this.m_maxScrollInertialX = 0f;
		this.m_maxScrollInertialY = 50f / (1024f / (float)Screen.width);
		this.m_scrollFallOff = 0.9f;
		this.m_scrollInertiaMultiplerX = 1f;
		this.m_scrollInertiaMultiplerY = 1f;
		this.m_allowScroll = false;
		this.m_freezeParentScrollWhenDragged = false;
		this.m_updateSize = 0.2f;
		this.m_updateRelativeTo = RelativeTo.ScreenHeight;
		this.m_setScrollToTarget = false;
		this.m_setScrollTarget = null;
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x000BFDE0 File Offset: 0x000BE1E0
	public virtual void SetDragBottomDelegate(Action _delegate, float _distanceFromBotton = 0f, RelativeTo _distanceFromBottonRelativeTo = RelativeTo.ScreenHeight)
	{
		this.m_allowDragToBottom = true;
		this.m_dragToBottomSize = _distanceFromBotton;
		this.m_dragToBottomRelativeTo = _distanceFromBottonRelativeTo;
		this.m_dragBottomDelegate = _delegate;
	}

	// Token: 0x06002950 RID: 10576 RVA: 0x000BFE00 File Offset: 0x000BE200
	protected virtual void SetScrollable()
	{
		this.m_uniqueCamera = true;
		this.m_camera = CameraS.AddCamera("UI Container Camera", true, 3);
		this.m_scrollTC = TransformS.AddComponent(this.m_TAC.p_entity, "Scroll Transform");
		TransformS.ParentComponent(this.m_scrollTC, this.m_TC, Vector3.zero);
		this.m_camera.transform.parent = this.m_scrollTC.transform;
		this.m_camera.transform.localPosition = Vector3.zero;
		GameObject gameObject = this.m_TC.transform.gameObject;
		GameObject gameObject2 = this.m_scrollTC.transform.gameObject;
		gameObject2.layer = gameObject.layer;
		if (this.m_TAC != null)
		{
			MeshCollider collider = this.m_TAC.m_collider;
			TouchAreaBootstrap component = gameObject.GetComponent<TouchAreaBootstrap>();
			MeshCollider meshCollider = gameObject2.AddComponent<MeshCollider>();
			meshCollider.sharedMesh = collider.sharedMesh;
			this.m_TAC.m_collider = meshCollider;
			this.m_TAC.m_TC = this.m_scrollTC;
			TouchAreaBootstrap touchAreaBootstrap = gameObject2.AddComponent<TouchAreaBootstrap>();
			touchAreaBootstrap.m_TAC = this.m_TAC;
			Object.DestroyImmediate(collider);
			Object.DestroyImmediate(component);
		}
		this.AssignCameraToComponentAndChildren(this.m_camera);
	}

	// Token: 0x06002951 RID: 10577 RVA: 0x000BFF34 File Offset: 0x000BE334
	protected virtual void AssignCameraToComponentAndChildren(Camera _camera)
	{
		if (this.m_TAC != null)
		{
			this.m_TAC.m_camera = _camera;
			this.m_TAC.m_TC.transform.gameObject.layer = _camera.gameObject.layer;
		}
		this.m_TC.transform.gameObject.layer = _camera.gameObject.layer;
		for (int i = 0; i < this.m_TC.p_entity.m_components.Count; i++)
		{
			IComponent component = this.m_TC.p_entity.m_components[i];
			if (component.m_componentType == ComponentType.TouchArea)
			{
				TouchAreaS.SetCamera(component as TouchAreaC, _camera);
			}
			else if (component.m_componentType == ComponentType.Prefab)
			{
				PrefabS.SetCamera(component as PrefabC, _camera);
			}
		}
	}

	// Token: 0x06002952 RID: 10578 RVA: 0x000C000F File Offset: 0x000BE40F
	public virtual void SetScrollPosition(float _horizontal, float _vertical)
	{
		this.m_currentScrollX = _horizontal;
		this.m_currentScrollY = -_vertical;
		this.m_overrideScrollPosition = true;
		this.ResetScroll();
	}

	// Token: 0x06002953 RID: 10579 RVA: 0x000C002D File Offset: 0x000BE42D
	public virtual void SetScrollPositionToChild(UIComponent _child)
	{
		this.m_setScrollTarget = _child;
		this.m_setScrollToTarget = true;
		this.m_overrideScrollPosition = false;
	}

	// Token: 0x06002954 RID: 10580 RVA: 0x000C0044 File Offset: 0x000BE444
	public virtual void ScrollToPosition(Vector2 _pos, Action _scrollToDelegate = null)
	{
		this.m_scrollToPosition = _pos - this.m_scrollTC.transform.position;
		this.m_scrollToDelegate = _scrollToDelegate;
		this.m_allowScroll = true;
		this.m_scrollToPos = true;
	}

	// Token: 0x06002955 RID: 10581 RVA: 0x000C007C File Offset: 0x000BE47C
	public virtual void ResetScroll()
	{
		this.m_scrollToPos = false;
		this.m_scrollToDelegate = null;
	}

	// Token: 0x06002956 RID: 10582 RVA: 0x000C008C File Offset: 0x000BE48C
	public override void FreezeHorizontalScroll(bool _affectWholeHierarchy)
	{
		this.m_scrollInertiaMultiplerX = 0f;
		if (_affectWholeHierarchy)
		{
			base.FreezeHorizontalScroll(_affectWholeHierarchy);
		}
	}

	// Token: 0x06002957 RID: 10583 RVA: 0x000C00A6 File Offset: 0x000BE4A6
	public override void UnfreezeHorizontalScroll(bool _affectWholeHierarchy)
	{
		this.m_scrollInertiaMultiplerX = 1f;
		if (_affectWholeHierarchy)
		{
			base.UnfreezeHorizontalScroll(_affectWholeHierarchy);
		}
	}

	// Token: 0x06002958 RID: 10584 RVA: 0x000C00C0 File Offset: 0x000BE4C0
	public override void FreezeVerticalScroll(bool _affectWholeHierarchy)
	{
		this.m_scrollInertiaMultiplerY = 0f;
		if (_affectWholeHierarchy)
		{
			base.FreezeVerticalScroll(_affectWholeHierarchy);
		}
	}

	// Token: 0x06002959 RID: 10585 RVA: 0x000C00DA File Offset: 0x000BE4DA
	public override void UnfreezeVerticalScroll(bool _affectWholeHierarchy)
	{
		this.m_scrollInertiaMultiplerY = 1f;
		if (_affectWholeHierarchy)
		{
			base.UnfreezeVerticalScroll(_affectWholeHierarchy);
		}
	}

	// Token: 0x0600295A RID: 10586 RVA: 0x000C00F4 File Offset: 0x000BE4F4
	public override void UpdateChildren()
	{
		this.InheritParentClip();
		this.UpdateFrozenChildren();
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			if (this.m_frozenComponents == null || !this.m_frozenComponents.Contains(this.m_childs[i]))
			{
				this.m_childs[i].Update();
			}
		}
	}

	// Token: 0x0600295B RID: 10587 RVA: 0x000C0164 File Offset: 0x000BE564
	public virtual void UpdateFrozenChildren()
	{
		if (this.m_frozenComponents != null)
		{
			for (int i = this.m_updatedFrozenCount; i < this.m_frozenComponents.Count; i++)
			{
				this.m_frozenComponents[i].Update();
				Vector3 localPosition = this.m_frozenComponents[i].m_TC.transform.localPosition;
				TransformS.ParentComponent(this.m_frozenComponents[i].m_TC, this.m_scrollTC, localPosition);
			}
			this.m_updatedFrozenCount = this.m_frozenComponents.Count;
		}
	}

	// Token: 0x0600295C RID: 10588 RVA: 0x000C01F8 File Offset: 0x000BE5F8
	public override void Update()
	{
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateAlign();
		this.UpdateMargins();
		if (this.d_Draw != null)
		{
			this.d_Draw(this);
		}
		this.UpdateChildren();
		this.ArrangeContents();
		this.UpdateUniqueCamera();
		if (this.m_parent == null)
		{
			this.UpdateSpecial();
		}
	}

	// Token: 0x0600295D RID: 10589 RVA: 0x000C0257 File Offset: 0x000BE657
	public override void UpdateSpecial()
	{
		this.UpdateUniqueCamera();
		base.UpdateSpecial();
	}

	// Token: 0x0600295E RID: 10590 RVA: 0x000C0265 File Offset: 0x000BE665
	public virtual void SetDragUpdate(float _size, RelativeTo _relativeTo, Action _delegate)
	{
		this.m_updateSize = _size;
		this.m_updateRelativeTo = _relativeTo;
		this.m_allowUpdate = true;
		this.m_dragUpdateDelegate = _delegate;
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x000C0284 File Offset: 0x000BE684
	public override void UpdateSize()
	{
		base.UpdateSize();
		if (this.m_updateRelativeTo == RelativeTo.OwnHeight)
		{
			this.m_actualUpdateSize = this.m_actualHeight * this.m_updateSize;
		}
		else if (this.m_updateRelativeTo == RelativeTo.OwnWidth)
		{
			this.m_actualUpdateSize = this.m_actualWidth * this.m_updateSize;
		}
		else
		{
			this.m_actualUpdateSize = this.m_updateSize * this.m_tempReferenceUpdate;
		}
		if (this.m_dragToBottomRelativeTo == RelativeTo.OwnHeight)
		{
			this.m_actualDragToBottomSize = this.m_actualHeight * this.m_dragToBottomSize;
		}
		else if (this.m_dragToBottomRelativeTo == RelativeTo.OwnWidth)
		{
			this.m_actualDragToBottomSize = this.m_actualWidth * this.m_dragToBottomSize;
		}
		else
		{
			this.m_actualDragToBottomSize = this.m_dragToBottomSize * this.m_tempReferenceDragToBottom;
		}
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x000C0350 File Offset: 0x000BE750
	public override void CalculateReferenceSizes()
	{
		base.CalculateReferenceSizes();
		if (this.m_allowUpdate)
		{
			this.m_tempUpdateRelativeTo = this.m_updateRelativeTo;
			if (this.m_updateRelativeTo == RelativeTo.ParentShortest)
			{
				this.m_tempUpdateRelativeTo = ((this.m_parent.m_actualWidth <= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
			}
			else if (this.m_updateRelativeTo == RelativeTo.ParentLongest)
			{
				this.m_tempUpdateRelativeTo = ((this.m_parent.m_actualWidth >= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
			}
			else if (this.m_updateRelativeTo == RelativeTo.ScreenShortest)
			{
				this.m_tempUpdateRelativeTo = ((Screen.width <= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
			}
			else if (this.m_updateRelativeTo == RelativeTo.ScreenLongest)
			{
				this.m_tempUpdateRelativeTo = ((Screen.width >= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
			}
			this.m_tempReferenceUpdate = (float)Screen.height;
			if (this.m_tempUpdateRelativeTo == RelativeTo.ScreenWidth)
			{
				this.m_tempReferenceUpdate = (float)Screen.width;
			}
			else if (this.m_tempUpdateRelativeTo == RelativeTo.ScreenHeight)
			{
				this.m_tempReferenceUpdate = (float)Screen.height;
			}
			else if (this.m_tempUpdateRelativeTo == RelativeTo.ParentWidth)
			{
				this.m_tempReferenceUpdate = this.m_parent.m_actualWidth - this.m_tempParentMargins.l - this.m_tempParentMargins.r;
			}
			else if (this.m_tempUpdateRelativeTo == RelativeTo.ParentHeight)
			{
				this.m_tempReferenceUpdate = this.m_parent.m_actualHeight - this.m_tempParentMargins.b - this.m_tempParentMargins.t;
			}
		}
		if (this.m_allowDragToBottom)
		{
			this.m_tempDragToBottomRelativeTo = this.m_dragToBottomRelativeTo;
			if (this.m_dragToBottomRelativeTo == RelativeTo.ParentShortest)
			{
				this.m_tempDragToBottomRelativeTo = ((this.m_parent.m_actualWidth <= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
			}
			else if (this.m_dragToBottomRelativeTo == RelativeTo.ParentLongest)
			{
				this.m_tempDragToBottomRelativeTo = ((this.m_parent.m_actualWidth >= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
			}
			else if (this.m_dragToBottomRelativeTo == RelativeTo.ScreenShortest)
			{
				this.m_tempDragToBottomRelativeTo = ((Screen.width <= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
			}
			else if (this.m_dragToBottomRelativeTo == RelativeTo.ScreenLongest)
			{
				this.m_tempDragToBottomRelativeTo = ((Screen.width >= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
			}
			this.m_tempReferenceDragToBottom = (float)Screen.height;
			if (this.m_tempDragToBottomRelativeTo == RelativeTo.ScreenWidth)
			{
				this.m_tempReferenceDragToBottom = (float)Screen.width;
			}
			else if (this.m_tempDragToBottomRelativeTo == RelativeTo.ScreenHeight)
			{
				this.m_tempReferenceDragToBottom = (float)Screen.height;
			}
			else if (this.m_tempDragToBottomRelativeTo == RelativeTo.ParentWidth)
			{
				this.m_tempReferenceDragToBottom = this.m_parent.m_actualWidth - this.m_tempParentMargins.l - this.m_tempParentMargins.r;
			}
			else if (this.m_tempDragToBottomRelativeTo == RelativeTo.ParentHeight)
			{
				this.m_tempReferenceDragToBottom = this.m_parent.m_actualHeight - this.m_tempParentMargins.b - this.m_tempParentMargins.t;
			}
		}
	}

	// Token: 0x06002961 RID: 10593 RVA: 0x000C0688 File Offset: 0x000BEA88
	public override void UpdateUniqueCamera()
	{
		this.m_camera.orthographicSize = this.m_actualHeight * 0.5f;
		Vector3 position = this.m_TC.transform.position;
		this.m_camera.transform.localPosition = Vector3.forward * -500f;
		this.AdjustCamera();
	}

	// Token: 0x06002962 RID: 10594 RVA: 0x000C06E4 File Offset: 0x000BEAE4
	public virtual void AdjustCamera()
	{
		if (this.m_camera != null && this.m_camera.transform != null)
		{
			UIComponent uicomponent = this.m_parent;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			float num = (float)Screen.width;
			float num2 = 0f;
			float num3 = (float)Screen.height;
			float num4 = 0f;
			while (uicomponent != null)
			{
				if (uicomponent is UIScrollableCanvas)
				{
					UIScrollableCanvas uiscrollableCanvas = uicomponent as UIScrollableCanvas;
					vector += uiscrollableCanvas.m_scrollTC.transform.localPosition;
					num = uiscrollableCanvas.m_camera.rect.xMax * (float)Screen.width;
					num2 = uiscrollableCanvas.m_camera.rect.xMin * (float)Screen.width;
					num3 = uiscrollableCanvas.m_camera.rect.yMax * (float)Screen.height;
					num4 = uiscrollableCanvas.m_camera.rect.yMin * (float)Screen.height;
					break;
				}
				vector2 += uicomponent.m_TC.transform.localPosition;
				uicomponent = uicomponent.m_parent;
			}
			Vector3 position = this.m_TC.transform.position;
			float num5 = position.x - vector.x - this.m_actualWidth * 0.5f + (float)Screen.width * 0.5f;
			float actualWidth = this.m_actualWidth;
			float num6 = 0f;
			float num7 = 0f;
			if (num5 < num2)
			{
				num6 = num2 - num5;
				num5 = num2;
			}
			if (num5 + actualWidth > num)
			{
				num7 = num5 + actualWidth - num;
			}
			float num8 = position.y - vector.y - this.m_actualHeight * 0.5f + (float)Screen.height * 0.5f;
			float actualHeight = this.m_actualHeight;
			float num9 = 0f;
			float num10 = 0f;
			if (num8 < num4)
			{
				num9 = num4 - num8;
				num8 = num4;
			}
			if (num8 + actualHeight > num3)
			{
				num10 = num8 + actualHeight - num3;
			}
			this.m_camera.transform.localPosition = new Vector3((-num7 + num6) * 0.5f, (-num10 + num9) * 0.5f, -500f);
			Rect rect;
			rect..ctor(num5 / (float)Screen.width, num8 / (float)Screen.height, (actualWidth - num6 - num7) / (float)Screen.width, (actualHeight - num9 - num10) / (float)Screen.height);
			if (rect.width <= 0.1f)
			{
				rect.width = 0f;
			}
			if (rect.height <= 0.1f)
			{
				rect.height = 0f;
			}
			this.m_camera.rect = rect;
			this.m_camera.orthographicSize = (actualHeight - num9 - num10) * 0.5f;
			if (this.m_TAC != null)
			{
				this.m_TAC.m_clip = true;
				this.m_TAC.m_clipBB.l = rect.xMin * (float)Screen.width;
				this.m_TAC.m_clipBB.r = rect.xMax * (float)Screen.width;
				this.m_TAC.m_clipBB.b = rect.yMin * (float)Screen.height;
				this.m_TAC.m_clipBB.t = rect.yMax * (float)Screen.height;
				if (this.m_prevClipRect.width != rect.width || this.m_prevClipRect.width != rect.height || this.m_prevClipRect.x != rect.x || this.m_prevClipRect.y != rect.y)
				{
					this.ChildrenInheritParentClip();
				}
				this.m_prevClipRect = rect;
			}
		}
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x000C0AC0 File Offset: 0x000BEEC0
	public override void ArrangeContents()
	{
		base.ArrangeContents();
		this.m_xMinLimit = 0f;
		this.m_xMaxLimit = Mathf.Max(this.m_contentWidth + this.m_actualMargins.l + this.m_actualMargins.r - this.m_actualWidth, 0f);
		this.m_yMinLimit = 0f;
		this.m_yMaxLimit = Mathf.Max(this.m_contentHeight - this.m_actualMargins.t - this.m_actualMargins.b - this.m_actualHeight, 0f);
		this.m_allowScroll = true;
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x000C0B5C File Offset: 0x000BEF5C
	public override void Step()
	{
		float num = 0f;
		float num2 = 0f;
		if (this.m_allowScroll)
		{
			if (this.m_contentWidth > this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r)
			{
				num = this.m_contentCenterX - this.m_contentWidth * 0.5f + this.m_actualWidth * 0.5f - this.m_actualMargins.l;
			}
			if (this.m_contentHeight > this.m_actualHeight - this.m_actualMargins.t - this.m_actualMargins.b)
			{
				num2 = this.m_contentCenterY + this.m_contentHeight * 0.5f - this.m_actualHeight * 0.5f + this.m_actualMargins.t;
			}
			this.m_scrollInertiaX = Mathf.Min(this.m_maxScrollInertialX, Mathf.Max(-this.m_maxScrollInertialX, this.m_scrollInertiaX));
			this.m_scrollInertiaY = Mathf.Min(this.m_maxScrollInertialY, Mathf.Max(-this.m_maxScrollInertialY, this.m_scrollInertiaY));
			this.m_xPos = this.m_scrollTC.transform.localPosition.x - num;
			float num3 = this.m_scrollTC.transform.localPosition.y - num2;
			float num4 = Mathf.Max(this.m_contentHeight + this.m_actualMargins.t + this.m_actualMargins.b - this.m_actualHeight, 0f);
			if (this.m_overrideScrollPosition)
			{
				this.m_xPos = this.m_xMaxLimit * this.m_currentScrollX + num;
				num3 = num4 * this.m_currentScrollY + num2;
				TransformS.SetPosition(this.m_scrollTC, new Vector3(this.m_xPos, num3, 0f));
				this.m_overrideScrollPosition = false;
				base.Step();
				return;
			}
			if (this.m_scrollToPos)
			{
				Vector3 vector = this.m_scrollToPosition * 0.15f;
				this.m_scrollToPosition -= vector;
				TransformS.Move(this.m_scrollTC, vector);
				if (this.m_scrollToPosition.sqrMagnitude <= 1f)
				{
					if (this.m_scrollToDelegate != null)
					{
						this.m_scrollToDelegate.Invoke();
					}
					this.m_scrollToDelegate = null;
				}
				if (this.m_scrollToPosition.sqrMagnitude <= 0.0001f)
				{
					this.m_scrollToPos = false;
				}
				base.Step();
				return;
			}
			float num5 = this.m_scrollInertiaX;
			if ((this.m_xPos < this.m_xMinLimit || this.m_xPos > this.m_xMaxLimit) && this.m_TAC != null && this.m_TAC.m_touchCount > 0)
			{
				num5 *= 0.382f;
			}
			else if (this.m_xPos < this.m_xMinLimit)
			{
				this.m_scrollInertiaX += this.m_xPos * -(1f - this.m_scrollFallOff * 0.618f);
				this.m_scrollInertiaX = Mathf.Max(this.m_scrollInertiaX, this.m_xPos * -(1f - this.m_scrollFallOff * 0.618f));
				num5 = (this.m_scrollInertiaX *= 0.382f);
			}
			else if (this.m_xPos > this.m_xMaxLimit)
			{
				this.m_scrollInertiaX += (this.m_xPos - this.m_xMaxLimit) * -(1f - this.m_scrollFallOff * 0.618f);
				this.m_scrollInertiaX = Mathf.Min(this.m_scrollInertiaX, (this.m_xPos - this.m_xMaxLimit) * -(1f - this.m_scrollFallOff * 0.618f));
				num5 = (this.m_scrollInertiaX *= 0.382f);
			}
			else
			{
				if (Mathf.Abs(this.m_scrollInertiaX) > 0.1f)
				{
					this.m_scrollInertiaX *= this.m_scrollFallOff;
				}
				else
				{
					this.m_scrollInertiaX = 0f;
				}
				num5 = this.m_scrollInertiaX;
			}
			if (!this.m_recoverFromUpdate && !this.m_draggedToUpdate && this.m_allowUpdate && num3 > this.m_actualUpdateSize)
			{
				this.m_draggedToUpdate = true;
				this.m_dragUpdateDelegate.Invoke();
			}
			if (num3 <= 0.1f && !this.m_draggedToUpdate && this.m_recoverFromUpdate)
			{
				this.m_recoverFromUpdate = false;
			}
			float num6 = 0f;
			if (this.m_draggedToUpdate)
			{
				num6 = this.m_actualUpdateSize;
			}
			if (!this.m_draggedToBottom && this.m_contentHeight > this.m_actualHeight && num3 <= -num4 + this.m_actualDragToBottomSize)
			{
				this.m_draggedToBottom = true;
				if (this.m_dragBottomDelegate != null)
				{
					this.m_dragBottomDelegate.Invoke();
				}
			}
			float num7 = this.m_scrollInertiaY;
			if ((num3 > num6 || num3 < -num4) && this.m_TAC != null && this.m_TAC.m_touchCount > 0)
			{
				num7 *= 0.382f;
			}
			else if (num3 > num6 + 1f)
			{
				this.m_scrollInertiaY += (num3 - num6) * -(1f - this.m_scrollFallOff * 0.618f);
				this.m_scrollInertiaY = Mathf.Max(this.m_scrollInertiaY, (num3 - num6) * -(1f - this.m_scrollFallOff * 0.618f));
				num7 = this.m_scrollInertiaY * 0.382f;
			}
			else if (num3 < -num4 - 1f)
			{
				this.m_scrollInertiaY += (num3 - -num4) * -(1f - this.m_scrollFallOff * 0.618f);
				this.m_scrollInertiaY = Mathf.Min(this.m_scrollInertiaY, (num3 - -num4) * -(1f - this.m_scrollFallOff * 0.618f));
				num7 = this.m_scrollInertiaY * 0.382f;
			}
			else
			{
				if (Mathf.Abs(this.m_scrollInertiaY) > 0.1f)
				{
					this.m_scrollInertiaY *= this.m_scrollFallOff;
				}
				else
				{
					this.m_scrollInertiaY = 0f;
				}
				num7 = this.m_scrollInertiaY;
			}
			if (this.m_xMaxLimit > 0f)
			{
				this.m_currentScrollX = this.m_xPos / this.m_xMaxLimit;
			}
			else
			{
				this.m_currentScrollX = 0f;
			}
			if (num4 > 0f)
			{
				this.m_currentScrollY = num3 / -num4;
			}
			else
			{
				this.m_currentScrollY = 0f;
			}
			num5 *= this.m_scrollInertiaMultiplerX;
			num7 *= this.m_scrollInertiaMultiplerY;
			if (num5 != 0f || num7 != 0f)
			{
				TransformS.Move(this.m_scrollTC, new Vector3(num5, num7, 0f));
			}
		}
		this.AdjustCamera();
		base.Step();
		if (this.m_setScrollToTarget)
		{
			if (this.m_xMaxLimit != 0f)
			{
				this.m_currentScrollX = (this.m_setScrollTarget.m_TC.transform.position.x + this.m_xMaxLimit * 0.5f * num) / this.m_xMaxLimit;
				this.m_xPos = this.m_xMaxLimit * this.m_currentScrollX + num;
				TransformS.SetPosition(this.m_scrollTC, new Vector3(this.m_xPos, this.m_scrollTC.transform.localPosition.y, 0f));
			}
			if (this.m_yMaxLimit != 0f)
			{
				float num8;
				if (this.m_setScrollTarget.m_TC.transform.position.y < -this.m_yMaxLimit + num2)
				{
					num8 = -this.m_yMaxLimit + num2;
				}
				else if (this.m_setScrollTarget.m_TC.transform.position.y > this.m_yMaxLimit - num2)
				{
					num8 = this.m_yMaxLimit - num2;
				}
				else
				{
					num8 = this.m_setScrollTarget.m_TC.transform.position.y;
				}
				TransformS.SetPosition(this.m_scrollTC, new Vector3(this.m_scrollTC.transform.localPosition.x, num8, 0f));
			}
			this.m_setScrollToTarget = false;
			return;
		}
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			this.AreChildrenVisible(this.m_childs[i]);
		}
	}

	// Token: 0x06002965 RID: 10597 RVA: 0x000C1408 File Offset: 0x000BF808
	public virtual void DragToUpdateHandled()
	{
		this.m_draggedToUpdate = false;
		this.m_recoverFromUpdate = true;
	}

	// Token: 0x06002966 RID: 10598 RVA: 0x000C1418 File Offset: 0x000BF818
	public virtual void DragToBottomHandled()
	{
		this.m_draggedToBottom = false;
	}

	// Token: 0x06002967 RID: 10599 RVA: 0x000C1424 File Offset: 0x000BF824
	public virtual void AreChildrenVisible(UIComponent _child)
	{
		if (_child.m_listenCameraEvents && _child.m_camera == this.m_camera)
		{
			float num = this.m_scrollTC.transform.position.y + this.m_actualHeight * 0.5f;
			float num2 = this.m_scrollTC.transform.position.y + this.m_actualHeight * -0.5f;
			float num3 = this.m_scrollTC.transform.position.x + this.m_actualWidth * -0.5f;
			float num4 = this.m_scrollTC.transform.position.x + this.m_actualWidth * 0.5f;
			Vector2 vector = _child.m_TC.transform.position;
			bool flag = false;
			if (this.m_camera.rect.width > 0f && this.m_camera.rect.height > 0f && vector.y + _child.m_actualHeight * 0.5f > num2 && vector.y + _child.m_actualHeight * -0.5f < num && vector.x + _child.m_actualWidth * 0.5f > num3 && vector.x + _child.m_actualWidth * -0.5f < num4)
			{
				flag = true;
			}
			if (flag && !_child.m_onCamera)
			{
				_child.EnterCamera();
			}
			if (!flag && _child.m_onCamera)
			{
				_child.ExitCamera();
			}
		}
		for (int i = 0; i < _child.m_childs.Count; i++)
		{
			this.AreChildrenVisible(_child.m_childs[i]);
		}
	}

	// Token: 0x06002968 RID: 10600 RVA: 0x000C1614 File Offset: 0x000BFA14
	public virtual void FreezeToCamera(UIComponent _c)
	{
		_c.SetRogue();
		if (this.m_frozenComponents == null)
		{
			this.m_frozenComponents = new List<UIComponent>();
		}
		this.m_frozenComponents.Add(_c);
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x000C1640 File Offset: 0x000BFA40
	public virtual void FreezeParentScroll()
	{
		for (UIComponent uicomponent = this.m_parent; uicomponent != null; uicomponent = uicomponent.m_parent)
		{
			if (uicomponent.GetType().IsSubclassOf(typeof(UIScrollableCanvas)))
			{
				uicomponent.FreezeHorizontalScroll(false);
				uicomponent.FreezeVerticalScroll(false);
				Debug.Log("FREEZE PARENT", null);
			}
		}
	}

	// Token: 0x0600296A RID: 10602 RVA: 0x000C169C File Offset: 0x000BFA9C
	public override void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		base.TouchHandler(_touchArea, _touchPhase, _touchIsSecondary, _touchCount, _touches);
		if (_touchCount == 1)
		{
			TLTouch tltouch = _touches[0];
			if (_touchPhase == TouchAreaPhase.Began)
			{
				this.m_allowScroll = false;
				this.m_scrollToPos = false;
				this.m_dragStartPos.x = _touches[0].m_currentPosition.x;
				this.m_dragStartPos.y = _touches[0].m_currentPosition.y;
				this.m_dragX = _touches[0].m_deltaPosition.x;
				this.m_dragY = _touches[0].m_deltaPosition.y;
			}
			if (_touchIsSecondary && tltouch.m_lockedSecondary == null && _touchPhase == TouchAreaPhase.RollIn)
			{
				TouchAreaS.LockSecondaryTouchArea(tltouch);
				this.m_allowScroll = false;
			}
			if (_touchPhase == TouchAreaPhase.DragStart)
			{
				this.m_allowScroll = true;
				this.m_scrollToPos = false;
				this.m_scrollInertiaX = -tltouch.m_deltaPosition.x;
				this.m_scrollInertiaY = -tltouch.m_deltaPosition.y;
				if (this.m_maxScrollInertialX != 0f || this.m_maxScrollInertialY != 0f)
				{
					this.PreventChildrenHit(true);
				}
			}
			else if (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut)
			{
				this.m_scrollInertiaX = (this.m_scrollInertiaX + -tltouch.m_deltaPosition.x) * 0.5f;
				this.m_scrollInertiaY = (this.m_scrollInertiaY + -tltouch.m_deltaPosition.y) * 0.5f;
				this.m_dragX = _touches[0].m_currentPosition.x - this.m_dragStartPos.x;
				this.m_dragY = _touches[0].m_currentPosition.y - this.m_dragStartPos.y;
			}
			else if (_touchPhase == TouchAreaPhase.StationaryIn || _touchPhase == TouchAreaPhase.StationaryOut)
			{
				this.m_scrollInertiaX *= 0.5f;
				this.m_scrollInertiaY *= 0.5f;
			}
			else if (_touchPhase == TouchAreaPhase.DragEnd)
			{
				this.m_scrollToPos = false;
				this.PreventChildrenHit(false);
				this.UnfreezeVerticalScroll(false);
				this.UnfreezeHorizontalScroll(false);
			}
		}
		if (this.m_passTouchesToScrollableParents)
		{
			for (UIComponent uicomponent = this.m_parent; uicomponent != null; uicomponent = uicomponent.m_parent)
			{
				if (uicomponent.GetType().IsSubclassOf(typeof(UIScrollableCanvas)))
				{
					uicomponent.TouchHandler(uicomponent.m_TAC, _touchPhase, _touchIsSecondary, _touchCount, _touches);
				}
			}
		}
	}

	// Token: 0x04002E3B RID: 11835
	public TransformC m_scrollTC;

	// Token: 0x04002E3C RID: 11836
	public float m_scrollInertiaY;

	// Token: 0x04002E3D RID: 11837
	public float m_scrollInertiaX;

	// Token: 0x04002E3E RID: 11838
	public float m_maxScrollInertialX;

	// Token: 0x04002E3F RID: 11839
	public float m_maxScrollInertialY;

	// Token: 0x04002E40 RID: 11840
	public float m_scrollFallOff;

	// Token: 0x04002E41 RID: 11841
	public float m_currentScrollX;

	// Token: 0x04002E42 RID: 11842
	public float m_currentScrollY;

	// Token: 0x04002E43 RID: 11843
	public bool m_overrideScrollPosition;

	// Token: 0x04002E44 RID: 11844
	public float m_scrollInertiaMultiplerX;

	// Token: 0x04002E45 RID: 11845
	public float m_scrollInertiaMultiplerY;

	// Token: 0x04002E46 RID: 11846
	public bool m_allowScroll;

	// Token: 0x04002E47 RID: 11847
	public bool m_draggedToUpdate;

	// Token: 0x04002E48 RID: 11848
	public bool m_recoverFromUpdate;

	// Token: 0x04002E49 RID: 11849
	public bool m_allowUpdate;

	// Token: 0x04002E4A RID: 11850
	public bool m_allowDragToBottom;

	// Token: 0x04002E4B RID: 11851
	public float m_updateSize;

	// Token: 0x04002E4C RID: 11852
	public RelativeTo m_updateRelativeTo;

	// Token: 0x04002E4D RID: 11853
	public RelativeTo m_tempUpdateRelativeTo;

	// Token: 0x04002E4E RID: 11854
	public float m_actualUpdateSize;

	// Token: 0x04002E4F RID: 11855
	public float m_tempReferenceUpdate;

	// Token: 0x04002E50 RID: 11856
	public Action m_dragUpdateDelegate;

	// Token: 0x04002E51 RID: 11857
	public float m_dragToBottomSize;

	// Token: 0x04002E52 RID: 11858
	public RelativeTo m_dragToBottomRelativeTo;

	// Token: 0x04002E53 RID: 11859
	public RelativeTo m_tempDragToBottomRelativeTo;

	// Token: 0x04002E54 RID: 11860
	public float m_actualDragToBottomSize;

	// Token: 0x04002E55 RID: 11861
	public float m_tempReferenceDragToBottom;

	// Token: 0x04002E56 RID: 11862
	public bool m_draggedToBottom;

	// Token: 0x04002E57 RID: 11863
	public Action m_dragBottomDelegate;

	// Token: 0x04002E58 RID: 11864
	public bool m_passTouchesToScrollableParents;

	// Token: 0x04002E59 RID: 11865
	protected Vector2 m_dragStartPos;

	// Token: 0x04002E5A RID: 11866
	protected float m_dragX;

	// Token: 0x04002E5B RID: 11867
	protected float m_dragY;

	// Token: 0x04002E5C RID: 11868
	protected bool m_freezeParentScrollWhenDragged;

	// Token: 0x04002E5D RID: 11869
	protected UIComponent m_setScrollTarget;

	// Token: 0x04002E5E RID: 11870
	protected bool m_setScrollToTarget;

	// Token: 0x04002E5F RID: 11871
	protected List<UIComponent> m_frozenComponents;

	// Token: 0x04002E60 RID: 11872
	protected int m_updatedFrozenCount;

	// Token: 0x04002E61 RID: 11873
	public bool m_scrollToPos;

	// Token: 0x04002E62 RID: 11874
	public Vector2 m_scrollToPosition;

	// Token: 0x04002E63 RID: 11875
	public Action m_scrollToDelegate;

	// Token: 0x04002E64 RID: 11876
	private Rect m_prevClipRect;

	// Token: 0x04002E65 RID: 11877
	public float m_xMinLimit;

	// Token: 0x04002E66 RID: 11878
	public float m_xMaxLimit;

	// Token: 0x04002E67 RID: 11879
	public float m_yMinLimit;

	// Token: 0x04002E68 RID: 11880
	public float m_yMaxLimit;

	// Token: 0x04002E69 RID: 11881
	public float m_xPos;
}
