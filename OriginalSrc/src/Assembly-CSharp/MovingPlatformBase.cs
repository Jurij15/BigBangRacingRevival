using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class MovingPlatformBase : Gadget
{
	// Token: 0x0600044F RID: 1103 RVA: 0x000177A0 File Offset: 0x00015BA0
	public MovingPlatformBase(GraphElement _graphElement, string _mainPrefabName)
		: base(_graphElement)
	{
		this.m_mainPrefab = ResourceManager.GetGameObject(_mainPrefabName + "_GameObject");
		this.m_platformTC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_platformTC, _graphElement.m_position, _graphElement.m_rotation);
		this.m_cylinderTC = TransformS.AddComponent(this.m_entity, "Cylinder");
		TransformS.SetTransform(this.m_cylinderTC, _graphElement.m_position, _graphElement.m_rotation);
		this.m_gearTC = TransformS.AddComponent(this.m_entity, "Gear");
		TransformS.SetTransform(this.m_gearTC, _graphElement.m_position, _graphElement.m_rotation);
		this.m_pipeTC = TransformS.AddComponent(this.m_entity, "Pipe");
		TransformS.SetTransform(this.m_pipeTC, _graphElement.m_position, _graphElement.m_rotation);
		this.m_startTC = TransformS.AddComponent(this.m_entity, "StartNode");
		TransformS.SetTransform(this.m_startTC, _graphElement.m_position, _graphElement.m_rotation);
		this.m_endTC = TransformS.AddComponent(this.m_entity, "EndNode");
		TransformS.SetTransform(this.m_endTC, _graphElement.m_position, _graphElement.m_rotation);
		GraphNode graphNode = base.m_graphElement as GraphNode;
		if (graphNode.m_childElements.Count == 0)
		{
			graphNode.AddElement(this.CreateChild());
		}
		else
		{
			this.m_childNode = graphNode.m_childElements[0] as GraphNode;
		}
		this.m_moveDir = (this.m_childNode.m_position - base.m_graphElement.m_position).normalized;
		this.m_moveMagnitude = (this.m_childNode.m_position - base.m_graphElement.m_position).magnitude;
		this.m_duration = 0.007142857f * this.m_moveMagnitude;
		this.CreateObject();
		TransformS.SetGlobalRotation(this.m_pipeTC, new Vector3(0f, 0f, Mathf.Atan2(this.m_moveDir.y, this.m_moveDir.x) * 57.29578f));
		TransformS.SetGlobalRotation(this.m_cylinderTC, this.m_pipeTC.transform.rotation.eulerAngles);
		TransformS.SetGlobalRotation(this.m_startTC, this.m_pipeTC.transform.rotation.eulerAngles);
		TransformS.SetGlobalRotation(this.m_endTC, this.m_pipeTC.transform.rotation.eulerAngles);
		TransformS.SetGlobalPosition(this.m_cylinderTC, base.m_graphElement.m_position + Vector3.forward * 60f);
		TransformS.SetGlobalPosition(this.m_startTC, base.m_graphElement.m_position + Vector3.forward * 60f + this.m_moveDir * -50f);
		TransformS.SetGlobalPosition(this.m_endTC, base.m_graphElement.m_position + Vector3.forward * 60f + this.m_moveDir * (this.m_moveMagnitude + 50f));
		TransformS.SetGlobalPosition(this.m_gearTC, base.m_graphElement.m_position + Vector3.forward * 50f);
		TransformS.SetGlobalPosition(this.m_pipeTC, (base.m_graphElement.m_position + this.m_childNode.m_position) / 2f + Vector3.forward * 60f);
		this.m_rotMultiplier = 1f;
		this.m_currentTime = -1f;
		this.m_currentValue = 0f;
		this.CreateEditorTouchArea(this.m_platformPrefabC.p_gameObject, null);
		this.m_startPos = base.m_graphElement.m_position;
		if (!this.m_minigame.m_editing)
		{
			this.CreateColliders();
			this.m_moveSound = SoundS.AddCombineSoundComponent(this.m_platformTC, "ElevatorMoveLoopSound", "/InGame/Units/MachineMovementLoop", 2f);
			MovingPlatformBase.m_combSound = SoundS.GetCombineSoundWithKey("ElevatorMoveLoopSound");
			SoundS.PlaySound(this.m_moveSound, false);
		}
		else
		{
			MovingPlatformBase.m_combSound = null;
			this.m_moveSound = null;
		}
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x00017C14 File Offset: 0x00016014
	public virtual GraphNode CreateChild()
	{
		this.m_childNode = new GraphNode(GraphNodeType.Child, typeof(TouchableChildNode), "ChildNode", base.m_graphElement.m_position + Vector2.up * 100f, Vector3.zero, Vector3.one);
		this.m_childNode.m_isRemovable = false;
		this.m_childNode.m_isCopyable = false;
		this.m_childNode.m_minDistanceFromParent = 100f;
		this.m_childNode.m_maxDistanceFromParent = 1000f;
		return this.m_childNode;
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x00017CB0 File Offset: 0x000160B0
	public virtual void CreateColliders()
	{
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(this.m_mainPrefab.transform.Find("Collision1").gameObject, Vector2.zero, 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, false);
		ucpPolyShape.group = base.GetGroup();
		this.m_cmb = ChipmunkProS.AddKinematicBody(this.m_platformTC, ucpPolyShape, this.m_unitC);
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00017D20 File Offset: 0x00016120
	public virtual void CreateObject()
	{
		GameObject gameObject = this.m_mainPrefab.transform.Find("MovingPlatform").gameObject;
		this.m_platformPrefabC = PrefabS.AddComponent(this.m_platformTC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject);
		GameObject gameObject2 = this.m_mainPrefab.transform.Find("RailCylinder").gameObject;
		PrefabS.AddComponent(this.m_cylinderTC, new Vector3(0f, 0f, 60f) + base.GetZBufferBias(), gameObject2);
		GameObject gameObject3 = this.m_mainPrefab.transform.Find("GearWheel").gameObject;
		PrefabS.AddComponent(this.m_gearTC, new Vector3(0f, 0f, 50f) + base.GetZBufferBias(), gameObject3);
		GameObject gameObject4 = this.m_mainPrefab.transform.Find("Rail").gameObject;
		PrefabS.AddComponent(this.m_pipeTC, new Vector3(0f, 0f, 60f) + base.GetZBufferBias(), gameObject4);
		this.m_pipeTC.transform.localScale = new Vector3((this.m_moveMagnitude + 100f) / 100f, 1f, 1f);
		GameObject gameObject5 = this.m_mainPrefab.transform.Find("RailNode").gameObject;
		PrefabS.AddComponent(this.m_startTC, new Vector3(0f, 0f, 60f) + base.GetZBufferBias(), gameObject5);
		this.m_startTC.transform.localScale = new Vector3(-1f, 1f, 1f);
		GameObject gameObject6 = this.m_mainPrefab.transform.Find("RailNode").gameObject;
		PrefabS.AddComponent(this.m_endTC, new Vector3(0f, 0f, 60f) + base.GetZBufferBias(), gameObject6);
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00017F38 File Offset: 0x00016338
	public override void Update()
	{
		base.Update();
		this.EditUpdate();
		TransformS.SetGlobalPosition(this.m_cylinderTC, this.m_platformTC.transform.position + Vector3.forward * 60f);
		TransformS.SetGlobalPosition(this.m_gearTC, this.m_platformTC.transform.position + Vector3.forward * 50f);
		if (this.m_moveSound != null && this.m_cmb != null && MovingPlatformBase.m_combSound != null && !MovingPlatformBase.m_combSound.isOutOfRange && !MovingPlatformBase.m_combSound.isPaused && MovingPlatformBase.m_combSound.nearestSoundC == this.m_moveSound)
		{
			float magnitude = ChipmunkProWrapper.ucpBodyGetVel(this.m_cmb.body).magnitude;
			float positionBetween = ToolBox.getPositionBetween(magnitude, 0f, 80f);
			SoundS.SetSoundParameter(this.m_moveSound, "Speed", positionBetween);
		}
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x00018040 File Offset: 0x00016440
	public virtual void EditUpdate()
	{
		if (this.m_minigame.m_editing)
		{
			this.SyncPositionToGraphElementPosition();
			this.m_moveDir = (this.m_childNode.m_position - base.m_graphElement.m_position).normalized;
			this.m_moveMagnitude = (this.m_childNode.m_position - base.m_graphElement.m_position).magnitude;
			this.m_duration = 0.007142857f * this.m_moveMagnitude;
			this.m_pipeTC.transform.localScale = new Vector3((this.m_moveMagnitude + 100f) / 100f, 1f, 1f);
			TransformS.SetGlobalRotation(this.m_pipeTC, new Vector3(0f, 0f, Mathf.Atan2(this.m_moveDir.y, this.m_moveDir.x) * 57.29578f));
			TransformS.SetGlobalRotation(this.m_cylinderTC, this.m_pipeTC.transform.rotation.eulerAngles);
			TransformS.SetGlobalRotation(this.m_startTC, this.m_pipeTC.transform.rotation.eulerAngles);
			TransformS.SetGlobalRotation(this.m_endTC, this.m_pipeTC.transform.rotation.eulerAngles);
			TransformS.SetGlobalPosition(this.m_pipeTC, (base.m_graphElement.m_position + this.m_childNode.m_position) / 2f + Vector3.forward * 60f);
			TransformS.SetGlobalPosition(this.m_startTC, base.m_graphElement.m_position + Vector3.forward * 60f + this.m_moveDir * -50f);
			TransformS.SetGlobalPosition(this.m_endTC, base.m_graphElement.m_position + Vector3.forward * 60f + this.m_moveDir * (this.m_moveMagnitude + 50f));
			if (this.m_ghostEntity == null)
			{
				this.CreateEndGhost();
			}
			else
			{
				this.UpdateEndGhost();
				TransformS.SetGlobalRotation(this.m_ghostCylinderTC, this.m_pipeTC.transform.rotation.eulerAngles);
			}
		}
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x000182C4 File Offset: 0x000166C4
	public void DestroyEndGhost()
	{
		if (this.m_ghostEntity != null)
		{
			EntityManager.RemoveEntity(this.m_ghostEntity);
			this.m_ghostEntity = null;
		}
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x000182E4 File Offset: 0x000166E4
	public void UpdateEndGhost()
	{
		if (this.m_ghostEntity != null)
		{
			TransformS.SetGlobalTransform(this.m_ghostPlatformTC, this.m_childNode.m_position, base.m_graphElement.m_TC.transform.rotation.eulerAngles);
			TransformS.SetGlobalPosition(this.m_ghostCylinderTC, this.m_ghostPlatformTC.transform.position + Vector3.forward * 60f);
			TransformS.SetGlobalTransform(this.m_ghostGearTC, this.m_ghostPlatformTC.transform.position + Vector3.forward * 50f, this.m_gearTC.transform.rotation.eulerAngles);
		}
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x000183A8 File Offset: 0x000167A8
	public void CreateEndGhost()
	{
		if (this.m_ghostEntity == null)
		{
			this.m_ghostEntity = EntityManager.AddEntity();
			this.m_ghostPlatformTC = TransformS.AddComponent(this.m_ghostEntity, "GhostPlatform");
			this.m_ghostCylinderTC = TransformS.AddComponent(this.m_ghostEntity, "GhostCylinder");
			this.m_ghostGearTC = TransformS.AddComponent(this.m_ghostEntity, "GhostGear");
			this.CreateGhostObject();
			Material material = ResourceManager.GetMaterial(RESOURCE.UnitGhostMaterial_Material);
			List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Transform, this.m_ghostEntity);
			foreach (IComponent component in componentsByEntity)
			{
				TransformC transformC = (TransformC)component;
				GameObject gameObject = transformC.transform.gameObject;
				Component[] componentsInChildren = gameObject.GetComponentsInChildren(typeof(Renderer));
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Renderer renderer = componentsInChildren[i] as Renderer;
					renderer.material = material;
				}
			}
			TransformS.SetGlobalTransform(this.m_ghostPlatformTC, this.m_childNode.m_position, base.m_graphElement.m_rotation);
			TransformS.SetGlobalPosition(this.m_ghostCylinderTC, this.m_ghostPlatformTC.transform.position + Vector3.forward * 60f);
			TransformS.SetGlobalTransform(this.m_ghostGearTC, this.m_ghostPlatformTC.transform.position + Vector3.forward * 50f, this.m_gearTC.transform.rotation.eulerAngles);
		}
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0001855C File Offset: 0x0001695C
	public virtual void CreateGhostObject()
	{
		PrefabS.AddComponent(this.m_ghostPlatformTC, Vector3.forward * 10f, this.m_mainPrefab.transform.Find("MovingPlatform").gameObject);
		PrefabS.AddComponent(this.m_ghostCylinderTC, this.m_ghostPlatformTC.transform.position + Vector3.forward * 60f, this.m_mainPrefab.transform.Find("RailCylinder").gameObject);
		PrefabS.AddComponent(this.m_ghostGearTC, this.m_ghostPlatformTC.transform.position + Vector3.forward * 50f, this.m_mainPrefab.transform.Find("GearWheel").gameObject);
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00018632 File Offset: 0x00016A32
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00018642 File Offset: 0x00016A42
	public override void Destroy()
	{
		base.Destroy();
		this.DestroyEndGhost();
	}

	// Token: 0x04000595 RID: 1429
	private static CombineSound m_combSound;

	// Token: 0x04000596 RID: 1430
	protected GameObject m_mainPrefab;

	// Token: 0x04000597 RID: 1431
	protected ChipmunkBodyC m_cmb;

	// Token: 0x04000598 RID: 1432
	protected PrefabC m_platformPrefabC;

	// Token: 0x04000599 RID: 1433
	protected TransformC m_platformTC;

	// Token: 0x0400059A RID: 1434
	protected TransformC m_cylinderTC;

	// Token: 0x0400059B RID: 1435
	protected TransformC m_pipeTC;

	// Token: 0x0400059C RID: 1436
	protected TransformC m_gearTC;

	// Token: 0x0400059D RID: 1437
	protected TransformC m_endTC;

	// Token: 0x0400059E RID: 1438
	protected TransformC m_startTC;

	// Token: 0x0400059F RID: 1439
	protected Entity m_ghostEntity;

	// Token: 0x040005A0 RID: 1440
	protected TransformC m_ghostCylinderTC;

	// Token: 0x040005A1 RID: 1441
	protected TransformC m_ghostPlatformTC;

	// Token: 0x040005A2 RID: 1442
	protected TransformC m_ghostGearTC;

	// Token: 0x040005A3 RID: 1443
	protected GraphNode m_childNode;

	// Token: 0x040005A4 RID: 1444
	protected Vector2 m_startPos;

	// Token: 0x040005A5 RID: 1445
	protected Vector2 m_moveDir;

	// Token: 0x040005A6 RID: 1446
	protected float m_moveMagnitude;

	// Token: 0x040005A7 RID: 1447
	protected float m_duration;

	// Token: 0x040005A8 RID: 1448
	protected float m_rotMultiplier;

	// Token: 0x040005A9 RID: 1449
	protected float m_currentTime;

	// Token: 0x040005AA RID: 1450
	protected float m_currentValue;

	// Token: 0x040005AB RID: 1451
	protected SoundC m_moveSound;
}
