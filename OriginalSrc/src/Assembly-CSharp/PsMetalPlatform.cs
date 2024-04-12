using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class PsMetalPlatform : Unit
{
	// Token: 0x0600026F RID: 623 RVA: 0x0001F5C0 File Offset: 0x0001D9C0
	public PsMetalPlatform(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_density = 0.03f;
		this.m_elasticity = 0.1f;
		this.m_friction = 0.9f;
		this.m_checkForCrushing = true;
		GraphNode graphNode = base.m_graphElement as GraphNode;
		if (graphNode.m_childElements.Count == 0)
		{
			this.m_node1 = new GraphNode(GraphNodeType.Child, typeof(TouchableAssembledClass), "PlatformChildNode1", _graphElement.m_position + Vector3.right * 100f + Vector3.forward * -5f, Vector3.zero, Vector3.one);
			this.m_node2 = new GraphNode(GraphNodeType.Child, typeof(TouchableAssembledClass), "PlatformChildNode2", _graphElement.m_position + Vector3.right * -100f + Vector3.forward * -5f, Vector3.zero, Vector3.one);
			graphNode.AddElement(this.m_node1);
			graphNode.AddElement(this.m_node2);
		}
		else
		{
			this.m_node1 = graphNode.m_childElements[0] as GraphNode;
			this.m_node2 = graphNode.m_childElements[1] as GraphNode;
		}
		this.m_node1.m_minDistanceFromParent = 25f;
		this.m_node1.m_maxDistanceFromParent = 500f;
		this.m_node2.m_minDistanceFromParent = 25f;
		this.m_node2.m_maxDistanceFromParent = 500f;
		Vector2 vector = this.m_node2.m_position - this.m_node1.m_position;
		float magnitude = vector.magnitude;
		this.m_length = magnitude;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		float num2 = magnitude * 10f * this.m_density;
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetGlobalTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		ucpPolyShape ucpPolyShape = new ucpPolyShape(magnitude, 20f, Vector2.zero, 257U, num2, this.m_elasticity, this.m_friction, (ucpCollisionType)4, false);
		this.m_cmb = ChipmunkProS.AddKinematicBody(this.m_tc, ucpPolyShape, this.m_unitC);
		this.m_pc = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.MetalPlatformPrefab_GameObject));
		this.m_pc.p_gameObject.transform.localScale = new Vector3(magnitude / 100f, 1f, 1f);
		PrefabS.SetCamera(this.m_pc, CameraS.m_mainCamera);
		this.CreateEditorTouchArea(magnitude, 10f, null, default(Vector2));
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0001F884 File Offset: 0x0001DC84
	public override void SyncPositionToGraphElementPosition()
	{
		for (int i = 0; i < base.m_assembledEntities.Count; i++)
		{
			List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, base.m_assembledEntities[i]);
			for (int j = 0; j < componentsByEntity.Count; j++)
			{
				ChipmunkBodyC chipmunkBodyC = componentsByEntity[j] as ChipmunkBodyC;
				ChipmunkProWrapper.ucpBodySetPos(chipmunkBodyC.body, base.m_graphElement.m_position);
				ChipmunkProWrapper.ucpBodySetAngle(chipmunkBodyC.body, base.m_graphElement.m_rotation.z * 0.017453292f);
				TransformS.SetGlobalPosition(chipmunkBodyC.TC, base.m_graphElement.m_position);
				TransformS.SetGlobalRotation(chipmunkBodyC.TC, base.m_graphElement.m_rotation);
			}
		}
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0001F94C File Offset: 0x0001DD4C
	public override void Update()
	{
		base.Update();
		if (this.m_minigame.m_editing)
		{
			Vector2 vector = this.m_node1.m_position - this.m_node2.m_position;
			float magnitude = vector.magnitude;
			if (magnitude != this.m_length)
			{
				this.m_length = magnitude;
				float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
				float num2 = magnitude * 10f * this.m_density;
				base.m_graphElement.m_position = this.m_node2.m_position + vector * 0.5f;
				base.m_graphElement.m_rotation = Vector3.forward * num;
				TransformS.SetGlobalTransform(base.m_graphElement.m_TC, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
				this.m_pc.p_gameObject.transform.localScale = new Vector3(magnitude / 100f, 1f, 1f);
			}
		}
	}

	// Token: 0x0400029A RID: 666
	private TransformC m_tc;

	// Token: 0x0400029B RID: 667
	private ChipmunkBodyC m_cmb;

	// Token: 0x0400029C RID: 668
	private PrefabC m_pc;

	// Token: 0x0400029D RID: 669
	private GraphNode m_node1;

	// Token: 0x0400029E RID: 670
	private GraphNode m_node2;

	// Token: 0x0400029F RID: 671
	private float m_length;

	// Token: 0x040002A0 RID: 672
	private float m_density;

	// Token: 0x040002A1 RID: 673
	private float m_elasticity;

	// Token: 0x040002A2 RID: 674
	private float m_friction;
}
