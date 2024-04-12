using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class TeleportBase : Unit
{
	// Token: 0x06000460 RID: 1120 RVA: 0x0001CDF0 File Offset: 0x0001B1F0
	public TeleportBase(GraphElement _graphElement, float _radius, string _mainPrefabName, float _depth = 0f)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_depth = _depth;
		GraphNode graphNode = base.m_graphElement as GraphNode;
		graphNode.m_moveChilds = false;
		graphNode.m_nodeType = GraphNodeType.Invisible;
		if (graphNode.m_childElements.Count == 0)
		{
			this.m_node1 = new GraphNode(GraphNodeType.IndependentChild, typeof(TouchableAssembledClass), "TeleportChildNode1", _graphElement.m_position + Vector3.right * 100f + Vector3.forward * _depth, Vector3.zero, Vector3.one);
			this.m_node2 = new GraphNode(GraphNodeType.IndependentChild, typeof(TouchableAssembledClass), "TeleportChildNode2", _graphElement.m_position + Vector3.right * -100f + Vector3.forward * _depth, Vector3.zero, Vector3.one);
			graphNode.AddElement(this.m_node1);
			graphNode.AddElement(this.m_node2);
		}
		else
		{
			this.m_node1 = graphNode.m_childElements[0] as GraphNode;
			this.m_node2 = graphNode.m_childElements[1] as GraphNode;
			this.m_node1.m_position.z = _depth;
			this.m_node2.m_position.z = _depth;
		}
		base.m_graphElement.m_position = (this.m_node1.m_position + this.m_node2.m_position) / 2f;
		this.m_node1.m_minDistanceFromParent = 100f;
		this.m_node1.m_maxDistanceFromParent = 10000f;
		this.m_node2.m_minDistanceFromParent = 100f;
		this.m_node2.m_maxDistanceFromParent = 10000f;
		this.m_radius = _radius;
		this.m_mainPrefab = ResourceManager.GetGameObject(_mainPrefabName + "_GameObject");
		this.m_renderers = new List<Renderer>();
		this.m_node1TC = TransformS.AddComponent(this.m_entity, "TeleportNode1");
		TransformS.SetGlobalTransform(this.m_node1TC, this.m_node1.m_position, this.m_node1.m_rotation);
		GameObject gameObject = this.m_mainPrefab.transform.Find("Teleport1").gameObject;
		this.m_portal1 = PrefabS.AddComponent(this.m_node1TC, Vector3.zero, gameObject);
		Renderer[] array = this.m_portal1.p_gameObject.GetComponentsInChildren<Renderer>();
		this.m_renderIndices1 = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			this.m_renderers.Add(array[i]);
			this.m_renderIndices1[i] = i;
		}
		this.m_node2TC = TransformS.AddComponent(this.m_entity, "TeleportNode2");
		TransformS.SetGlobalTransform(this.m_node2TC, this.m_node2.m_position, this.m_node2.m_rotation);
		GameObject gameObject2 = this.m_mainPrefab.transform.Find("Teleport2").gameObject;
		this.m_portal2 = PrefabS.AddComponent(this.m_node2TC, Vector3.zero, gameObject2);
		array = this.m_portal2.p_gameObject.GetComponentsInChildren<Renderer>();
		this.m_renderIndices2 = new int[array.Length];
		int count = this.m_renderers.Count;
		for (int j = 0; j < array.Length; j++)
		{
			this.m_renderers.Add(array[j]);
			this.m_renderIndices2[j] = count + j;
		}
		if (!this.m_minigame.m_editing)
		{
			ucpCircleShape ucpCircleShape = new ucpCircleShape(this.m_radius, Vector2.zero, 257U, 1f, 0.1f, 0.1f, (ucpCollisionType)4, true);
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddStaticBody(this.m_node1TC, ucpCircleShape, null);
			this.m_node1Shape = ucpCircleShape.shapePtr;
			ucpCircleShape ucpCircleShape2 = new ucpCircleShape(this.m_radius, Vector2.zero, 257U, 1f, 0.1f, 0.1f, (ucpCollisionType)4, true);
			ChipmunkBodyC chipmunkBodyC2 = ChipmunkProS.AddStaticBody(this.m_node2TC, ucpCircleShape2, null);
			this.m_node2Shape = ucpCircleShape2.shapePtr;
			ChipmunkProS.AddCollisionHandler(chipmunkBodyC, new CollisionDelegate(this.TeleportCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, false, true);
			ChipmunkProS.AddCollisionHandler(chipmunkBodyC2, new CollisionDelegate(this.TeleportCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, false, true);
			ChipmunkProS.AddCollisionHandler(chipmunkBodyC, new CollisionDelegate(this.TeleportCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, true, false, true);
			ChipmunkProS.AddCollisionHandler(chipmunkBodyC2, new CollisionDelegate(this.TeleportCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, true, false, true);
		}
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x0001D25C File Offset: 0x0001B65C
	public override void Update()
	{
		base.Update();
		if (this.m_minigame.m_editing)
		{
			TransformS.SetGlobalTransform(this.m_node1TC, this.m_node1.m_position, this.m_node1.m_rotation);
			TransformS.SetGlobalTransform(this.m_node2TC, this.m_node2.m_position, this.m_node2.m_rotation);
			base.m_graphElement.m_position = (this.m_node1.m_position + this.m_node2.m_position) / 2f;
			TransformS.SetGlobalTransform(base.m_graphElement.m_TC, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
		}
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0001D317 File Offset: 0x0001B717
	public virtual void TeleportCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
	}

	// Token: 0x040005BA RID: 1466
	protected GameObject m_mainPrefab;

	// Token: 0x040005BB RID: 1467
	protected GraphNode m_node1;

	// Token: 0x040005BC RID: 1468
	protected GraphNode m_node2;

	// Token: 0x040005BD RID: 1469
	protected TransformC m_node1TC;

	// Token: 0x040005BE RID: 1470
	protected TransformC m_node2TC;

	// Token: 0x040005BF RID: 1471
	protected List<Renderer> m_renderers;

	// Token: 0x040005C0 RID: 1472
	protected int[] m_renderIndices1;

	// Token: 0x040005C1 RID: 1473
	protected int[] m_renderIndices2;

	// Token: 0x040005C2 RID: 1474
	protected PrefabC m_portal1;

	// Token: 0x040005C3 RID: 1475
	protected PrefabC m_portal2;

	// Token: 0x040005C4 RID: 1476
	protected IntPtr m_node1Shape;

	// Token: 0x040005C5 RID: 1477
	protected IntPtr m_node2Shape;

	// Token: 0x040005C6 RID: 1478
	protected float m_radius;

	// Token: 0x040005C7 RID: 1479
	protected float m_depth;
}
