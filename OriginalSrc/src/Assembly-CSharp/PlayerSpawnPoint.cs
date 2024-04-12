using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class PlayerSpawnPoint : BasicAssembledClass
{
	// Token: 0x06000129 RID: 297 RVA: 0x0000DAA8 File Offset: 0x0000BEA8
	public PlayerSpawnPoint(GraphElement _graphElement)
		: base(_graphElement)
	{
		_graphElement.m_isCopyable = true;
		_graphElement.m_isRemovable = true;
		_graphElement.m_name = "PlayerSpawnPoint";
		this.m_entity = EntityManager.AddEntity(new string[] { _graphElement.m_name });
		base.m_assembledEntities.Add(this.m_entity);
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		TextMeshC textMeshC = TextMeshS.AddComponent(this.m_tc, Vector3.zero, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0f, 0f, 50f, Align.Center, Align.Middle, CameraS.m_mainCamera, string.Empty);
		TextMeshS.SetText(textMeshC, "Player", false);
		this.CreateGraphElementTouchArea(50f, this.m_tc);
	}

	// Token: 0x040000D8 RID: 216
	public Entity m_entity;

	// Token: 0x040000D9 RID: 217
	public TransformC m_tc;
}
