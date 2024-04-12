using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F4 RID: 500
public class LinkableGizmo : BasicGizmo
{
	// Token: 0x06000ED6 RID: 3798 RVA: 0x0008BF58 File Offset: 0x0008A358
	public LinkableGizmo(List<GraphElement> _graphElements)
		: base(_graphElements)
	{
		GizmoManager.m_visualGizmos.Add(this);
		float num = (float)Screen.height / 768f * 50f;
		float num2 = num * 2f;
		TransformC transformC = TransformS.AddComponent(this.m_uiTC.p_entity, this.m_uiTC.transform.position + Vector3.forward * -10f);
		TransformS.ParentComponent(transformC, this.m_uiTC);
		SpriteC spriteC = SpriteS.AddComponent(this.m_uiTC, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_selection_circle", null), PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, num2, num2);
		SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 50f), 0f);
		SpriteS.ConvertSpritesToPrefabComponent(this.m_uiTC, true);
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0008C02D File Offset: 0x0008A42D
	protected override void SetGizmoRotation(Vector3 _uiRot)
	{
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0008C02F File Offset: 0x0008A42F
	public void SetHighlight()
	{
		TransformS.Rotate(this.m_uiTC, new Vector3(0f, 0f, -1f));
	}

	// Token: 0x040011C2 RID: 4546
	private float m_minDistanceFromParent;

	// Token: 0x040011C3 RID: 4547
	private float m_maxDistanceFromParent;
}
