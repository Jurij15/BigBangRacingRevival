using System;
using UnityEngine;

// Token: 0x02000582 RID: 1410
public class UIRectButton : UIComponent
{
	// Token: 0x0600290A RID: 10506 RVA: 0x001B3963 File Offset: 0x001B1D63
	public UIRectButton(UIComponent _parent, string _tag, Camera _camera)
		: base(_parent, true, _tag, _camera, null, string.Empty)
	{
		this.SetWidth(UIRectButton.m_defaultWidth, RelativeTo.ScreenWidth);
		this.SetHeight(UIRectButton.m_defaultHeight, RelativeTo.ScreenHeight);
		this.SetMargins(UIRectButton.m_defaultMargins, RelativeTo.ScreenHeight);
	}

	// Token: 0x0600290B RID: 10507 RVA: 0x001B399C File Offset: 0x001B1D9C
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(this.m_actualWidth, this.m_actualHeight, Vector2.zero, false);
		Color color;
		color..ctor(0.4f, 0.4f, 0.4f);
		uint num = DebugDraw.ColorToUInt(color);
		if (this.m_highlight)
		{
			color..ctor(0.3f, 1f, 0.3f);
		}
		Camera camera = CameraS.m_uiCamera;
		if (this.m_parent != null)
		{
			camera = this.m_parent.m_camera;
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x04002E16 RID: 11798
	public static float m_defaultWidth = 0.2f;

	// Token: 0x04002E17 RID: 11799
	public static float m_defaultHeight = 0.09f;

	// Token: 0x04002E18 RID: 11800
	public static cpBB m_defaultMargins = new cpBB(0.01f, 0.01f, 0.01f, 0.01f);
}
