using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class PsConcretePlatform : Unit
{
	// Token: 0x060001F9 RID: 505 RVA: 0x00017260 File Offset: 0x00015660
	public PsConcretePlatform(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_density = 0.01f;
		this.m_elasticity = 0.1f;
		this.m_friction = 1f;
		GraphNode graphNode = base.m_graphElement as GraphNode;
		if (graphNode.m_childElements.Count == 0)
		{
			this.m_node1 = new GraphNode(GraphNodeType.Child, typeof(TouchableAssembledClass), "ChildNode1", _graphElement.m_position + Vector3.right * 100f + Vector3.forward * -5f, Vector3.zero, Vector3.one);
			this.m_node2 = new GraphNode(GraphNodeType.Child, typeof(TouchableAssembledClass), "ChildNode2", _graphElement.m_position + Vector3.right * -100f + Vector3.forward * -5f, Vector3.zero, Vector3.one);
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
		float num2 = magnitude * 20f * this.m_density;
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetGlobalTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		ucpPolyShape ucpPolyShape = new ucpPolyShape(magnitude, 20f, Vector2.zero, 257U, num2, this.m_elasticity, this.m_friction, (ucpCollisionType)4, false);
		this.m_cmb = ChipmunkProS.AddStaticBody(this.m_tc, ucpPolyShape, null);
		this.m_cmb.customComponent = this.m_unitC;
		this.m_pc = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.ConcretePlatformPrefab_GameObject));
		this.m_pc.p_gameObject.transform.localScale = new Vector3(magnitude / 100f, 1f, 1f);
		this.m_plankMesh = this.m_pc.p_gameObject.transform.Find("plank").gameObject.GetComponent<MeshFilter>().mesh;
		this.CalculateUVs(magnitude);
		this.CreateEditorTouchArea(magnitude, 20f, null, default(Vector2));
	}

	// Token: 0x060001FA RID: 506 RVA: 0x00017550 File Offset: 0x00015950
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

	// Token: 0x060001FB RID: 507 RVA: 0x00017618 File Offset: 0x00015A18
	public void CalculateUVs(float _len)
	{
		Vector2[] uv = this.m_plankMesh.uv;
		for (int i = 0; i < uv.Length; i++)
		{
			if (uv[i].x > 0.05f)
			{
				uv[i].x = _len / 100f;
			}
		}
		this.m_plankMesh.uv = uv;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0001767C File Offset: 0x00015A7C
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
				this.CalculateUVs(magnitude);
			}
		}
	}

	// Token: 0x040001DB RID: 475
	private TransformC m_tc;

	// Token: 0x040001DC RID: 476
	private ChipmunkBodyC m_cmb;

	// Token: 0x040001DD RID: 477
	private PrefabC m_pc;

	// Token: 0x040001DE RID: 478
	private Mesh m_plankMesh;

	// Token: 0x040001DF RID: 479
	private GraphNode m_node1;

	// Token: 0x040001E0 RID: 480
	private GraphNode m_node2;

	// Token: 0x040001E1 RID: 481
	private float m_length;

	// Token: 0x040001E2 RID: 482
	private float m_density;

	// Token: 0x040001E3 RID: 483
	private float m_elasticity;

	// Token: 0x040001E4 RID: 484
	private float m_friction;
}
