using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
public class PsWinterLights : Unit
{
	// Token: 0x06000275 RID: 629 RVA: 0x0001FB44 File Offset: 0x0001DF44
	public PsWinterLights(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		GraphNode graphNode = base.m_graphElement as GraphNode;
		graphNode.m_moveChilds = false;
		graphNode.m_nodeType = GraphNodeType.Invisible;
		if (base.m_graphElement.m_inFront)
		{
			this.m_depth = -50f;
		}
		else
		{
			this.m_depth = 75f;
		}
		if (graphNode.m_childElements.Count == 0)
		{
			this.m_startNode = new GraphNode(GraphNodeType.IndependentChild, typeof(TouchableAssembledClass), "WinterLightStartNode", _graphElement.m_position + Vector3.right * 50f + Vector3.forward * this.m_depth + Vector3.up * 100f, Vector3.zero, Vector3.one);
			this.m_endNode = new GraphNode(GraphNodeType.IndependentChild, typeof(TouchableAssembledClass), "WinterLightEndNode", _graphElement.m_position + Vector3.right * -50f + Vector3.forward * this.m_depth + Vector3.up * 100f, Vector3.zero, Vector3.one);
			graphNode.AddElement(this.m_startNode);
			graphNode.AddElement(this.m_endNode);
		}
		else
		{
			this.m_startNode = graphNode.m_childElements[0] as GraphNode;
			this.m_endNode = graphNode.m_childElements[1] as GraphNode;
			this.m_startNode.m_position.z = this.m_depth;
			this.m_endNode.m_position.z = this.m_depth;
		}
		base.m_graphElement.m_position = (this.m_startNode.m_position + this.m_endNode.m_position) / 2f;
		this.m_startNode.m_minDistanceFromParent = 50f;
		this.m_startNode.m_maxDistanceFromParent = 400f;
		this.m_startNode.m_isRemovable = true;
		this.m_startNode.m_isCopyable = true;
		this.m_startNode.m_isForegroundable = true;
		this.m_endNode.m_minDistanceFromParent = 50f;
		this.m_endNode.m_maxDistanceFromParent = 400f;
		this.m_endNode.m_isRemovable = true;
		this.m_endNode.m_isCopyable = true;
		this.m_endNode.m_isForegroundable = true;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.WinterLightsPrefab_GameObject);
		PrefabC prefabC = PrefabS.AddComponent(_graphElement.m_TC, Vector3.zero, gameObject);
		this.m_startTransform = prefabC.p_gameObject.transform.Find("Start");
		this.m_startTransform.position = this.m_startNode.m_position + Vector3.forward * this.m_depth;
		this.m_endTransform = prefabC.p_gameObject.transform.Find("End");
		this.m_endTransform.position = this.m_endNode.m_position + Vector3.forward * this.m_depth;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0001FE58 File Offset: 0x0001E258
	public override void Update()
	{
		base.Update();
		if (this.m_minigame.m_editing)
		{
			base.m_graphElement.m_position = (this.m_endNode.m_position + this.m_startNode.m_position) * 0.5f;
			TransformS.SetGlobalTransform(base.m_graphElement.m_TC, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
			this.m_startTransform.position = this.m_startNode.m_position + Vector3.forward * this.m_depth;
			this.m_endTransform.position = this.m_endNode.m_position + Vector3.forward * this.m_depth;
		}
	}

	// Token: 0x040002A3 RID: 675
	private const float FOREGROUND_DEPTH = -50f;

	// Token: 0x040002A4 RID: 676
	private const float BACKGROUND_DEPTH = 75f;

	// Token: 0x040002A5 RID: 677
	private float m_depth;

	// Token: 0x040002A6 RID: 678
	private GraphNode m_startNode;

	// Token: 0x040002A7 RID: 679
	private Transform m_startTransform;

	// Token: 0x040002A8 RID: 680
	private GraphNode m_endNode;

	// Token: 0x040002A9 RID: 681
	private Transform m_endTransform;
}
