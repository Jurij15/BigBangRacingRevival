using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class BreakingPlatform : Unit
{
	// Token: 0x060001E3 RID: 483 RVA: 0x00015F24 File Offset: 0x00014324
	public BreakingPlatform(GraphElement _graphElement)
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
		this.m_pc = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.CrumblingPlatformPrefab_GameObject));
		this.m_pc.p_gameObject.transform.localScale = new Vector3(magnitude / 100f, 1f, 1f);
		this.m_plankMesh = this.m_pc.p_gameObject.transform.Find("plank").gameObject.GetComponent<MeshFilter>().mesh;
		this.CalculateUVs(magnitude);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, false, false);
		}
		this.CreateEditorTouchArea(magnitude, 20f, null, default(Vector2));
		this.m_crumbleTimer = 0.8f;
		this.m_startedCrumbling = false;
		this.m_crumbleFxEntity = null;
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00016260 File Offset: 0x00014660
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

	// Token: 0x060001E5 RID: 485 RVA: 0x00016328 File Offset: 0x00014728
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

	// Token: 0x060001E6 RID: 486 RVA: 0x0001638C File Offset: 0x0001478C
	public override void Update()
	{
		if (this.m_isDead)
		{
			return;
		}
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
		if (this.m_startedCrumbling)
		{
			if (Main.m_gameTicks % 3 == 0)
			{
				this.m_moveDir *= -1;
			}
			this.m_crumbleTimer -= Main.m_gameDeltaTime;
			if (this.m_crumbleTimer <= 0f)
			{
				if (this.m_crumbleFxEntity != null)
				{
					List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Prefab, this.m_crumbleFxEntity);
					PrefabC prefabC = componentsByEntity[0] as PrefabC;
					if (prefabC != null)
					{
						PrefabS.PauseParticleEmission(prefabC, true);
					}
				}
				GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.CrumblingMaterialBreakdown_GameObject);
				ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
				if (componentsInChildren != null)
				{
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].gravityModifier = Mathf.Abs(componentsInChildren[i].gravityModifier) * (float)this.m_minigame.m_gravityMultipler;
					}
				}
				TransformC transformC = EntityManager.AddTimedFXEntity(gameObject, this.m_tc.transform.position, this.m_tc.transform.rotation.eulerAngles, 5f, "GTAG_AUTODESTROY", null);
				SoundS.PlaySingleShot("/Ingame/Units/CrumblingPlatformBreak", this.m_tc.transform.position, 1f);
				this.EmergencyKill();
				this.m_startedCrumbling = false;
			}
		}
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x000165FC File Offset: 0x000149FC
	private void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (!this.m_isDead && _phase == ucpCollisionPhase.Begin && !this.m_startedCrumbling)
		{
			this.m_startedCrumbling = true;
			GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.CrumblingMaterialCountdown_GameObject);
			ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
			if (componentsInChildren != null)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].gravityModifier = Mathf.Abs(componentsInChildren[i].gravityModifier) * (float)this.m_minigame.m_gravityMultipler;
				}
			}
			this.m_crumbleFxEntity = EntityManager.AddTimedFXEntity(gameObject, this.m_tc.transform.position, this.m_tc.transform.rotation.eulerAngles, 5.8f, "GTAG_AUTODESTROY", null).p_entity;
			SoundS.PlaySingleShot("/Ingame/Units/CrumblingPlatformCrumble", new Vector3(_pair.point.x, _pair.point.y, 0f), 1f);
		}
	}

	// Token: 0x040001B5 RID: 437
	private const float CRUMBLE_TIME_SECS = 0.8f;

	// Token: 0x040001B6 RID: 438
	private float m_crumbleTimer;

	// Token: 0x040001B7 RID: 439
	private bool m_startedCrumbling;

	// Token: 0x040001B8 RID: 440
	private Entity m_crumbleFxEntity;

	// Token: 0x040001B9 RID: 441
	private TransformC m_tc;

	// Token: 0x040001BA RID: 442
	private ChipmunkBodyC m_cmb;

	// Token: 0x040001BB RID: 443
	private PrefabC m_pc;

	// Token: 0x040001BC RID: 444
	private Mesh m_plankMesh;

	// Token: 0x040001BD RID: 445
	private GraphNode m_node1;

	// Token: 0x040001BE RID: 446
	private GraphNode m_node2;

	// Token: 0x040001BF RID: 447
	private float m_length;

	// Token: 0x040001C0 RID: 448
	private float m_density;

	// Token: 0x040001C1 RID: 449
	private float m_elasticity;

	// Token: 0x040001C2 RID: 450
	private float m_friction;

	// Token: 0x040001C3 RID: 451
	private int m_moveDir = 1;
}
