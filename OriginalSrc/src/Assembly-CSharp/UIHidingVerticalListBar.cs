using System;
using UnityEngine;

// Token: 0x02000590 RID: 1424
public class UIHidingVerticalListBar : UIComponent
{
	// Token: 0x06002983 RID: 10627 RVA: 0x001B50C8 File Offset: 0x001B34C8
	public UIHidingVerticalListBar(UIVerticalList _parent, string _tag = "")
		: base(_parent, true, _tag, null, null, null)
	{
		this.SetWidth(UIHidingVerticalListBar.m_defaultWidth, UIHidingVerticalListBar.m_defaultWidthRelativeTo);
		this.SetHeight(UIHidingVerticalListBar.m_defaultHeight, UIHidingVerticalListBar.m_defaultHeightRelativeTo);
		this.SetMargins(UIHidingVerticalListBar.m_defaultMargins, UIHidingVerticalListBar.m_defaultMarginsRelativeTo);
		this.m_depthOffset = -5f;
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x001B511C File Offset: 0x001B351C
	public override void Update()
	{
		base.Update();
		this.m_posSaved = false;
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x001B512C File Offset: 0x001B352C
	public override void Step()
	{
		base.Step();
		if (!this.m_posSaved)
		{
			this.m_originalPosition = this.m_TC.transform.localPosition;
			this.m_posSaved = true;
		}
		float y = this.m_camera.transform.parent.localPosition.y;
		if (y < this.m_lowestCameraPosition)
		{
			this.m_lowestCameraPosition = y;
			if (this.m_highestCameraPosition - y > this.m_actualHeight)
			{
				this.m_highestCameraPosition = y + this.m_actualHeight;
			}
		}
		else if (y > this.m_highestCameraPosition)
		{
			this.m_highestCameraPosition = y;
			if (this.m_lowestCameraPosition - y < this.m_actualHeight)
			{
				this.m_lowestCameraPosition = y;
			}
		}
		if (this.m_lowestCameraPosition > 0f)
		{
			this.m_highestCameraPosition = y;
		}
		float num = y + this.m_highestCameraPosition - y;
		Vector3 originalPosition = this.m_originalPosition;
		originalPosition.y += num;
		TransformS.SetPosition(this.m_TC, originalPosition);
		this.m_previousCameraPosition = y;
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x001B5238 File Offset: 0x001B3638
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(this.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(this.m_actualWidth + this.m_parent.m_margins.l + this.m_parent.m_margins.r, this.m_actualHeight + this.m_parent.m_margins.t + this.m_parent.m_margins.b, Vector2.zero);
		Color color;
		color..ctor(0.4f, 0.3f, 0.4f);
		if (this.m_highlight)
		{
			color..ctor(0.5f, 0.3f, 0.3f);
		}
		Camera camera = CameraS.m_uiCamera;
		if (this.m_parent != null)
		{
			camera = this.m_parent.m_camera;
		}
		uint num = DebugDraw.ColorToUInt(color);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, "UIComponent: Prefab", null);
	}

	// Token: 0x04002E86 RID: 11910
	public static float m_defaultWidth = 1f;

	// Token: 0x04002E87 RID: 11911
	public static float m_defaultHeight = 0.1f;

	// Token: 0x04002E88 RID: 11912
	public static cpBB m_defaultMargins = new cpBB(0f);

	// Token: 0x04002E89 RID: 11913
	public static RelativeTo m_defaultWidthRelativeTo = RelativeTo.ParentWidth;

	// Token: 0x04002E8A RID: 11914
	public static RelativeTo m_defaultHeightRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002E8B RID: 11915
	public static RelativeTo m_defaultMarginsRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002E8C RID: 11916
	public Vector3 m_originalPosition;

	// Token: 0x04002E8D RID: 11917
	public bool m_posSaved;

	// Token: 0x04002E8E RID: 11918
	public float m_lowestCameraPosition;

	// Token: 0x04002E8F RID: 11919
	public float m_highestCameraPosition;

	// Token: 0x04002E90 RID: 11920
	public float m_previousCameraPosition;
}
