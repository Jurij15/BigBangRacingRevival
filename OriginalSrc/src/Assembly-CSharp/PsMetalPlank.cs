using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class PsMetalPlank : Unit
{
	// Token: 0x0600017B RID: 379 RVA: 0x00012090 File Offset: 0x00010490
	public PsMetalPlank(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_density = 0.01f;
		this.m_elasticity = 0.1f;
		this.m_friction = 0.9f;
		this.m_canElectrify = true;
		this.m_checkForCrushing = true;
		GraphNode graphNode = base.m_graphElement as GraphNode;
		if (graphNode.m_childElements.Count == 0)
		{
			this.m_node1 = new GraphNode(GraphNodeType.Child, typeof(TouchableAssembledClass), "PlankChildNode1", _graphElement.m_position + Vector3.right * 100f + Vector3.forward * -5f, Vector3.zero, Vector3.one);
			this.m_node2 = new GraphNode(GraphNodeType.Child, typeof(TouchableAssembledClass), "PlankChildNode2", _graphElement.m_position + Vector3.right * -100f + Vector3.forward * -5f, Vector3.zero, Vector3.one);
			graphNode.AddElement(this.m_node1);
			graphNode.AddElement(this.m_node2);
		}
		else
		{
			this.m_node1 = graphNode.m_childElements[0] as GraphNode;
			this.m_node2 = graphNode.m_childElements[1] as GraphNode;
		}
		this.m_node1.m_minDistanceFromParent = 50f;
		this.m_node1.m_maxDistanceFromParent = 200f;
		this.m_node2.m_minDistanceFromParent = 50f;
		this.m_node2.m_maxDistanceFromParent = 200f;
		Vector2 vector = this.m_node2.m_position - this.m_node1.m_position;
		float magnitude = vector.magnitude;
		this.m_length = magnitude;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		float num2 = magnitude * 10f * this.m_density;
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetGlobalTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		ucpPolyShape ucpPolyShape = new ucpPolyShape(magnitude, 10f, Vector2.zero, 17895696U, num2, this.m_elasticity, this.m_friction, (ucpCollisionType)4, false);
		this.m_cmb = ChipmunkProS.AddDynamicBody(this.m_tc, ucpPolyShape, null);
		this.m_cmb.customComponent = this.m_unitC;
		this.m_pc = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.PlankPrefab_GameObject));
		this.m_pc.p_gameObject.transform.localScale = new Vector3(magnitude / 100f, 1f, 1f);
		this.m_plankMesh = this.m_pc.p_gameObject.transform.Find("plank").gameObject.GetComponent<MeshFilter>().mesh;
		this.CalculateUVs(magnitude);
		this.CreateEditorTouchArea(magnitude, 10f, null, default(Vector2));
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00012390 File Offset: 0x00010790
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

	// Token: 0x0600017D RID: 381 RVA: 0x00012458 File Offset: 0x00010858
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

	// Token: 0x0600017E RID: 382 RVA: 0x000124BC File Offset: 0x000108BC
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

	// Token: 0x0600017F RID: 383 RVA: 0x000125E0 File Offset: 0x000109E0
	public override void EmergencyKill()
	{
		if (!this.m_isDead)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
			SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
			EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.MetalBoxCrush_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 1f, "GTAG_AUTODESTROY", null);
			this.Destroy();
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0001265C File Offset: 0x00010A5C
	public override void SetElectrified(bool _electrify, Vector2 _contactPoint)
	{
		base.SetElectrified(_electrify, _contactPoint);
		if (this.m_pc.p_gameObject == null)
		{
			return;
		}
		if (_electrify && this.m_electricity == null)
		{
			this.m_electricity = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.EffectEletricity_GameObject));
			this.m_electricity.p_gameObject.transform.parent = this.m_pc.p_gameObject.transform;
			this.m_electricity.p_gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			ParticleSystem[] componentsInChildren = this.m_electricity.p_gameObject.transform.GetComponentsInChildren<ParticleSystem>();
			Mesh plankMesh = this.m_plankMesh;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ParticleSystem.ShapeModule shape = componentsInChildren[i].shape;
				shape.shapeType = 6;
				shape.mesh = plankMesh;
				shape.enabled = true;
				this.m_electricity.p_gameObject.transform.localScale = Vector3.one;
				this.m_electricity.p_gameObject.SetActive(false);
				this.m_electricity.p_gameObject.SetActive(true);
			}
			Color color = Color.red;
			color = Color.Lerp(color, Color.white, 0.5f);
			PrefabS.SetShaderColor(this.m_pc, color);
		}
		else if (!_electrify && this.m_electricity != null)
		{
			PrefabS.RemoveComponent(this.m_electricity, true);
			this.m_electricity = null;
			PrefabS.SetShaderColor(this.m_pc, Color.white);
		}
	}

	// Token: 0x04000158 RID: 344
	private TransformC m_tc;

	// Token: 0x04000159 RID: 345
	private ChipmunkBodyC m_cmb;

	// Token: 0x0400015A RID: 346
	private PrefabC m_pc;

	// Token: 0x0400015B RID: 347
	private Mesh m_plankMesh;

	// Token: 0x0400015C RID: 348
	private GraphNode m_node1;

	// Token: 0x0400015D RID: 349
	private GraphNode m_node2;

	// Token: 0x0400015E RID: 350
	private float m_length;

	// Token: 0x0400015F RID: 351
	private float m_density;

	// Token: 0x04000160 RID: 352
	private float m_elasticity;

	// Token: 0x04000161 RID: 353
	private float m_friction;

	// Token: 0x04000162 RID: 354
	private PrefabC m_electricity;
}
