using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class ArrowSign : Unit
{
	// Token: 0x0600027C RID: 636 RVA: 0x0002051C File Offset: 0x0001E91C
	public ArrowSign(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.ArrowSignPrefab_GameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, "ArrowSign");
		TransformS.SetTransform(transformC, _graphElement.m_position + new Vector3(0f, -0f, 300f) + base.GetZBufferBias(), _graphElement.m_rotation);
		PrefabS.AddComponent(transformC, Vector3.zero, gameObject);
		this.CreateEditorTouchArea(50f, 50f, transformC, default(Vector2));
		base.m_graphElement.m_isRotateable = true;
	}
}
