using System;
using UnityEngine;

// Token: 0x02000580 RID: 1408
public class UICircleButton : UIComponent
{
	// Token: 0x06002906 RID: 10502 RVA: 0x001B3110 File Offset: 0x001B1510
	public UICircleButton(UIComponent _parent, string _tag, Camera _camera)
		: base(_parent, false, _tag, _camera, null, string.Empty)
	{
		this.SetWidth(UICircleButton.m_defaultRadius, UICircleButton.m_defaultWidthRelativeTo);
		this.SetHeight(UICircleButton.m_defaultRadius, UICircleButton.m_defaultHeightRelativeTo);
		this.SetMargins(UICircleButton.m_defaultMargins, UICircleButton.m_defaultMarginsRelativeTo);
		this.m_TAC = TouchAreaS.AddCircleArea(this.m_TC, _tag, UICircleButton.m_defaultRadius, this.m_camera, null);
		TouchAreaS.AddTouchEventListener(this.m_TAC, new TouchEventDelegate(this.TouchHandler));
	}

	// Token: 0x06002907 RID: 10503 RVA: 0x001B3194 File Offset: 0x001B1594
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		float num = Math.Min(this.m_actualWidth, this.m_actualHeight) * 0.5f;
		Vector2[] circle = DebugDraw.GetCircle(num, 16, Vector2.zero, true);
		Color color;
		color..ctor(0.4f, 0.4f, 0.4f);
		if (this.m_highlight)
		{
			color..ctor(0.3f, 1f, 0.3f);
		}
		Camera camera = CameraS.m_uiCamera;
		if (this.m_parent != null)
		{
			camera = this.m_parent.m_camera;
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_TC, Vector3.zero, circle, 2f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, Position.Center, true);
	}

	// Token: 0x04002E11 RID: 11793
	public static float m_defaultRadius = 0.1f;

	// Token: 0x04002E12 RID: 11794
	public static cpBB m_defaultMargins = new cpBB(0f);

	// Token: 0x04002E13 RID: 11795
	public static RelativeTo m_defaultWidthRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002E14 RID: 11796
	public static RelativeTo m_defaultHeightRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002E15 RID: 11797
	public static RelativeTo m_defaultMarginsRelativeTo = RelativeTo.ScreenShortest;
}
