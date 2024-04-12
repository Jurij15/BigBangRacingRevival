using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class GoalSpawnPoint : BasicAssembledClass
{
	// Token: 0x06000128 RID: 296 RVA: 0x0000D9D0 File Offset: 0x0000BDD0
	public GoalSpawnPoint(GraphElement _graphElement)
		: base(_graphElement)
	{
		_graphElement.m_isCopyable = true;
		_graphElement.m_isRemovable = true;
		_graphElement.m_name = "GoalSpawnPoint";
		this.m_entity = EntityManager.AddEntity(new string[] { _graphElement.m_name });
		base.m_assembledEntities.Add(this.m_entity);
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		TextMeshC textMeshC = TextMeshS.AddComponent(this.m_tc, Vector3.zero, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0f, 0f, 50f, Align.Center, Align.Middle, CameraS.m_mainCamera, string.Empty);
		TextMeshS.SetText(textMeshC, "Goal", false);
		this.CreateGraphElementTouchArea(50f, this.m_tc);
	}

	// Token: 0x040000D6 RID: 214
	public Entity m_entity;

	// Token: 0x040000D7 RID: 215
	public TransformC m_tc;
}
