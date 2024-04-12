using System;
using UnityEngine;

// Token: 0x02000591 RID: 1425
public class UIVerticalListButton : UIComponent
{
	// Token: 0x06002988 RID: 10632 RVA: 0x001B5368 File Offset: 0x001B3768
	public UIVerticalListButton(UIComponent _parent, UIScrollableCanvas _scrollableCanvas, UIPagedCanvas _pagedCanvas, string _tag, UIModel _model, string _fieldName)
		: base(_parent, true, _tag, null, _model, _fieldName)
	{
		this.m_scrollableCanvas = _scrollableCanvas;
		this.m_pagedCanvas = _pagedCanvas;
		this.m_TAC.m_letTouchesThrough = true;
		this.SetWidth(UIVerticalListButton.m_defaultWidth, UIVerticalListButton.m_defaultWidthRelativeTo);
		this.SetHeight(UIVerticalListButton.m_defaultHeight, UIVerticalListButton.m_defaultHeightRelativeTo);
		this.SetMargins(UIVerticalListButton.m_defaultMargins, UIVerticalListButton.m_defaultMarginsRelativeTo);
	}

	// Token: 0x06002989 RID: 10633 RVA: 0x001B53D0 File Offset: 0x001B37D0
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

	// Token: 0x04002E91 RID: 11921
	public static float m_defaultWidth = 1f;

	// Token: 0x04002E92 RID: 11922
	public static float m_defaultHeight = 0.05f;

	// Token: 0x04002E93 RID: 11923
	public static cpBB m_defaultMargins = new cpBB(0f);

	// Token: 0x04002E94 RID: 11924
	public static RelativeTo m_defaultWidthRelativeTo = RelativeTo.ParentWidth;

	// Token: 0x04002E95 RID: 11925
	public static RelativeTo m_defaultHeightRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002E96 RID: 11926
	public static RelativeTo m_defaultMarginsRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002E97 RID: 11927
	protected UIScrollableCanvas m_scrollableCanvas;

	// Token: 0x04002E98 RID: 11928
	protected UIPagedCanvas m_pagedCanvas;
}
