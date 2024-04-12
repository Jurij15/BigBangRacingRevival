using System;
using UnityEngine;

// Token: 0x02000255 RID: 597
public class PsUIPlayerLevel : UICanvas
{
	// Token: 0x0600120B RID: 4619 RVA: 0x000B2C20 File Offset: 0x000B1020
	public PsUIPlayerLevel(UIComponent _parent, int _currentValue, int _fullValue)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.m_currentValue = _currentValue;
		this.m_fullValue = _fullValue;
		this.SetSize(1f, 0.15f, RelativeTo.ParentWidth);
		UIFittedText uifittedText = new UIFittedText(this, false, string.Empty, this.m_currentValue + "/" + this.m_fullValue, PsFontManager.GetFont(PsFonts.HurmeBold), false, "#ffffff", "#333333");
		uifittedText.SetShadowShift(new Vector2(1f, -1f), 0.1f);
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x000B2CB8 File Offset: 0x000B10B8
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = _c.m_actualHeight / 8f;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, num, 8, Vector2.zero);
		float num2 = _c.m_actualWidth - num * 2f;
		float num3 = num2 * ((float)this.m_currentValue / (float)this.m_fullValue);
		Vector2[] rect = DebugDraw.GetRect(num3, _c.m_actualHeight - num * 2f, new Vector2(num2, 0f));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(0f, 0f, -0.3f), roundedRect, (float)Screen.height * 0.01f, DebugDraw.HexToColor("#aa00aa"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0f, 0f, -0.6f), roundedRect, DebugDraw.HexToUint("#aa00aa"), DebugDraw.HexToUint("#aa00aa"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera, string.Empty, null);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0f, 0f, -0.9f), rect, DebugDraw.HexToUint("#ff00ff"), DebugDraw.HexToUint("#ff00ff"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera, string.Empty, null);
	}

	// Token: 0x04001530 RID: 5424
	private int m_currentValue;

	// Token: 0x04001531 RID: 5425
	private int m_fullValue;
}
