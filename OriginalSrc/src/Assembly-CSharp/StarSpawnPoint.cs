using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class StarSpawnPoint : BasicAssembledClass
{
	// Token: 0x0600012A RID: 298 RVA: 0x0000DB80 File Offset: 0x0000BF80
	public StarSpawnPoint(GraphElement _graphElement)
		: base(_graphElement)
	{
		_graphElement.m_isCopyable = true;
		_graphElement.m_isRemovable = true;
		_graphElement.m_name = "StarSpawnPoint";
		this.m_entity = EntityManager.AddEntity(new string[] { _graphElement.m_name });
		base.m_assembledEntities.Add(this.m_entity);
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		TextMeshC textMeshC = TextMeshS.AddComponent(this.m_tc, Vector3.zero, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0f, 0f, 50f, Align.Center, Align.Middle, CameraS.m_mainCamera, string.Empty);
		TextMeshS.SetText(textMeshC, "Star", false);
		this.CreateGraphElementTouchArea(50f, this.m_tc);
	}

	// Token: 0x040000DA RID: 218
	public Entity m_entity;

	// Token: 0x040000DB RID: 219
	public TransformC m_tc;
}
