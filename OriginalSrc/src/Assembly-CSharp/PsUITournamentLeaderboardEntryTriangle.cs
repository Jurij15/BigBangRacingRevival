using System;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class PsUITournamentLeaderboardEntryTriangle : UIComponent
{
	// Token: 0x0600134C RID: 4940 RVA: 0x000BF714 File Offset: 0x000BDB14
	public PsUITournamentLeaderboardEntryTriangle(UIComponent _parent, int _index)
		: base(_parent, false, string.Empty, null, null, string.Empty)
	{
		switch (_index)
		{
		case 0:
			this.m_color = "#FF6A0B";
			break;
		case 1:
			this.m_color = "#0595FF";
			break;
		case 2:
			this.m_color = "#00ff87";
			break;
		default:
			this.m_color = "#FFFFFF";
			break;
		}
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x000BF798 File Offset: 0x000BDB98
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		rect[0].y = (rect[3].y = (rect[0].y + rect[3].y) / 2f);
		Color color = DebugDraw.HexToColor(this.m_color);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x0400162E RID: 5678
	private string m_color = "#000000";
}
