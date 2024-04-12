using System;

// Token: 0x020000C6 RID: 198
public class TouchableAssembledClass : BasicAssembledClass
{
	// Token: 0x060003EB RID: 1003 RVA: 0x00037D54 File Offset: 0x00036154
	public TouchableAssembledClass(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_entity = EntityManager.AddEntity(new string[] { "GTAG_UNIT", _graphElement.m_name });
		base.m_assembledEntities.Add(this.m_entity);
		this.CreateGraphElementTouchArea(50f, null);
	}

	// Token: 0x0400050D RID: 1293
	public Entity m_entity;
}
