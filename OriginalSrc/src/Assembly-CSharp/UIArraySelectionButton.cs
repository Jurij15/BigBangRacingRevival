using System;
using UnityEngine;

// Token: 0x02000593 RID: 1427
public class UIArraySelectionButton : UIComponent
{
	// Token: 0x0600298E RID: 10638 RVA: 0x001B56C4 File Offset: 0x001B3AC4
	public UIArraySelectionButton(UIComponent _parent, UIScrollableCanvas _scrollableCanvas, UIPagedCanvas _pagedCanvas, string _tag, UIModel _model, string _fieldName, string _label, object _value)
		: base(_parent, true, _tag, null, _model, _fieldName)
	{
		this.SetWidth(UIArraySelectionButton.m_defaultWidth, UIArraySelectionButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIArraySelectionButton.m_defaultHeight, UIArraySelectionButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIArraySelectionButton.m_defaultMargins, UIArraySelectionButton.m_defaultMarginsRelativeTo);
		this.m_TAC.m_letTouchesThrough = true;
		this.m_scrollableCanvas = _scrollableCanvas;
		this.m_pagedCanvas = _pagedCanvas;
		this.m_value = _value;
		this.m_label = new UIText(this, false, _tag, _label, "HurmeRegular_Font", 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_label.SetAlign(1f, 0f);
	}

	// Token: 0x0600298F RID: 10639 RVA: 0x001B5765 File Offset: 0x001B3B65
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		this.m_changeValue = true;
	}

	// Token: 0x06002990 RID: 10640 RVA: 0x001B5775 File Offset: 0x001B3B75
	protected override void OnTouchDragStart(TLTouch _touch)
	{
		base.OnTouchDragStart(_touch);
		this.m_changeValue = false;
	}

	// Token: 0x06002991 RID: 10641 RVA: 0x001B5788 File Offset: 0x001B3B88
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (this.m_changeValue && _inside)
		{
			this.SetValue(this.m_value);
			if (this.m_pagedCanvas != null)
			{
				this.m_scrollableCanvas.Destroy();
				this.m_pagedCanvas.Update();
				this.m_pagedCanvas.PreviousPage();
			}
		}
	}

	// Token: 0x06002992 RID: 10642 RVA: 0x001B57E8 File Offset: 0x001B3BE8
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(this.m_actualWidth, this.m_actualHeight * 0.95f, Vector2.zero);
		Color color;
		color..ctor(0.4f, 0.4f, 0.4f);
		if (this.m_highlight)
		{
			color..ctor(0.3f, 0.3f, 0.3f);
		}
		Camera camera = CameraS.m_uiCamera;
		if (this.m_parent != null)
		{
			camera = this.m_parent.m_camera;
		}
		uint num = DebugDraw.ColorToUInt(color);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, "UIComponent: Prefab", null);
	}

	// Token: 0x04002E9C RID: 11932
	public static float m_defaultWidth = 1f;

	// Token: 0x04002E9D RID: 11933
	public static float m_defaultHeight = 0.05f;

	// Token: 0x04002E9E RID: 11934
	public static cpBB m_defaultMargins = new cpBB(0f);

	// Token: 0x04002E9F RID: 11935
	public static RelativeTo m_defaultWidthRelativeTo = RelativeTo.ParentWidth;

	// Token: 0x04002EA0 RID: 11936
	public static RelativeTo m_defaultHeightRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002EA1 RID: 11937
	public static RelativeTo m_defaultMarginsRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002EA2 RID: 11938
	protected UIScrollableCanvas m_scrollableCanvas;

	// Token: 0x04002EA3 RID: 11939
	protected UIPagedCanvas m_pagedCanvas;

	// Token: 0x04002EA4 RID: 11940
	protected UIText m_label;

	// Token: 0x04002EA5 RID: 11941
	protected object m_value;

	// Token: 0x04002EA6 RID: 11942
	protected bool m_changeValue;
}
