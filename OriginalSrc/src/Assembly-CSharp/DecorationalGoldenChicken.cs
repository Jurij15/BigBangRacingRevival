using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class DecorationalGoldenChicken : Unit
{
	// Token: 0x06000232 RID: 562 RVA: 0x0001BADC File Offset: 0x00019EDC
	public DecorationalGoldenChicken(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_chickenNode = base.m_graphElement as LevelDecorationNode;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.DecorationalGoldenChicken_GameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, "GoldenChicken");
		TransformS.SetTransform(transformC, _graphElement.m_position + new Vector3(0f, 0f, 150f) + base.GetZBufferBias(), _graphElement.m_rotation);
		PrefabC prefabC = PrefabS.AddComponent(transformC, Vector3.zero, gameObject, string.Empty, true, true);
		this.m_chickenNode.m_isFlippable = true;
		if (this.m_chickenNode.m_flipped)
		{
			TransformS.SetScale(transformC, new Vector3(-1f, 1f, 1f));
		}
		this.CreateEditorTouchArea(50f, 50f, transformC, default(Vector2));
	}

	// Token: 0x0400023B RID: 571
	public LevelDecorationNode m_chickenNode;
}
