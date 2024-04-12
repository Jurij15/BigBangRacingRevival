using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class TouchableChildNode : BasicAssembledClass
{
	// Token: 0x060003EC RID: 1004 RVA: 0x00037DA8 File Offset: 0x000361A8
	public TouchableChildNode(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_entity = EntityManager.AddEntity(new string[] { "GTAG_UNIT", _graphElement.m_name });
		base.m_assembledEntities.Add(this.m_entity);
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_position + Vector3.forward * 100f);
		this.CreateGraphElementTouchArea(50f, transformC);
	}

	// Token: 0x0400050E RID: 1294
	public Entity m_entity;
}
