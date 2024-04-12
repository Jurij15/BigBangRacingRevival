using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class DecorationalMetalFence : Unit
{
	// Token: 0x06000236 RID: 566 RVA: 0x0001BF08 File Offset: 0x0001A308
	public DecorationalMetalFence(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_fenceNode = base.m_graphElement as LevelDecorationNode;
		this.m_fenceNode.m_isForegroundable = true;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.DecorationMetalFencePrefab_GameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, "MetalFence");
		float num = ((!this.m_fenceNode.m_inFront) ? 150f : (-75f));
		TransformS.SetTransform(transformC, _graphElement.m_position + new Vector3(0f, 0f, num) + base.GetZBufferBias(), _graphElement.m_rotation);
		PrefabS.AddComponent(transformC, Vector3.zero, gameObject, string.Empty, true, true);
		this.m_fenceNode.m_isFlippable = false;
		this.CreateEditorTouchArea(180f, 90f, transformC, default(Vector2));
	}

	// Token: 0x04000244 RID: 580
	public LevelDecorationNode m_fenceNode;
}
