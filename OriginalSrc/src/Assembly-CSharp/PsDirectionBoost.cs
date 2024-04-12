using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C7 RID: 455
public class PsDirectionBoost : Unit
{
	// Token: 0x06000DD0 RID: 3536 RVA: 0x00081A94 File Offset: 0x0007FE94
	public PsDirectionBoost(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		base.m_graphElement.m_name = "DirectionBoost";
		base.m_graphElement.m_isCopyable = true;
		base.m_graphElement.m_isRemovable = true;
		base.m_graphElement.m_isRotateable = true;
		base.m_graphElement.m_isFlippable = false;
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position + new Vector3(0f, 0f, 0f), _graphElement.m_rotation);
		ucpShape ucpShape = new ucpCircleShape(20f, Vector2.zero, 17895696U, 0f, 0f, 0f, (ucpCollisionType)10, true);
		this.m_body = ChipmunkProS.AddStaticBody(this.m_tc, ucpShape, null);
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.DirectionBoostArrowPrefab_GameObject);
		this.m_prefab = PrefabS.AddComponent(this.m_tc, new Vector3(0f, 0f, 45f), gameObject);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_body, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)10, (ucpCollisionType)4, true, false, false);
			ChipmunkProS.AddCollisionHandler(this.m_body, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, false);
		}
		this.CreateEditorTouchArea(20f, 20f, null, default(Vector2));
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x00081BFF File Offset: 0x0007FFFF
	public override void CreateEditorTouchArea(float _width, float _height, TransformC _parentTC = null, Vector2 _offset = default(Vector2))
	{
		if (this.m_minigame.m_editing)
		{
			this.CreateGraphElementTouchArea(_width, _parentTC);
		}
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x00081C1C File Offset: 0x0008001C
	protected void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		ucpBodyType ucpBodyType = ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body);
		if (unitC == null || unitC.m_unit == null || ucpBodyType != ucpBodyType.DYNAMIC)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin)
		{
			if (this.m_coolDownTimer <= 0)
			{
				this.m_unit = unitC.m_unit;
				this.m_startBoost = true;
			}
		}
		else if (_phase == ucpCollisionPhase.Separate)
		{
		}
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00081CA0 File Offset: 0x000800A0
	public override void Update()
	{
		if (!this.m_minigame.m_editing && this.m_unit != null)
		{
			if (this.m_startBoost && this.m_coolDownTimer <= 0)
			{
				this.SetupUnit(this.m_unit);
			}
			else if (this.m_coolDownTimer > 0)
			{
				this.m_coolDownTimer--;
				int num = 60 - this.m_coolDownTimer;
				if (num <= 20)
				{
					float num2 = (float)num / 20f;
					Vector2 bezierPoint = ToolBox.GetBezierPoint(num2, this.m_unitCenterPosition, this.m_tc.transform.position, this.m_tc.transform.position, this.m_tc.transform.position + this.m_tc.transform.right * 50f);
					TransformS.SetGlobalPosition(this.m_transformHelper, bezierPoint);
					if (num == 20)
					{
						EntityManager.SetActivityOfEntity(this.m_unit.m_entity, true, true, true, true, true, true);
						TransformS.RemoveComponent(this.m_transformHelper);
						this.m_transformHelper = null;
						List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, this.m_unit.m_entity);
						Vector3 vector;
						vector..ctor(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
						for (int i = 0; i < componentsByEntity.Count; i++)
						{
							ChipmunkBodyC chipmunkBodyC = componentsByEntity[i] as ChipmunkBodyC;
							ChipmunkProWrapper.ucpBodySetPos(chipmunkBodyC.body, chipmunkBodyC.TC.transform.position + vector);
							Vector2 vector2 = ChipmunkProWrapper.ucpBodyGetVel(chipmunkBodyC.body);
							ChipmunkProWrapper.ucpBodySetVel(chipmunkBodyC.body, this.m_tc.transform.right * 1000f);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x00081E9C File Offset: 0x0008029C
	private void SetupUnit(Unit _unit)
	{
		this.m_startBoost = false;
		this.m_coolDownTimer = 60;
		this.m_unitCenterPosition = Vector2.zero;
		this.m_velocityAtStart = Vector2.zero;
		List<IComponent> unitBodyList = _unit.GetUnitBodyList();
		for (int i = 0; i < unitBodyList.Count; i++)
		{
			IntPtr body = (unitBodyList[i] as ChipmunkBodyC).body;
			this.m_unitCenterPosition += ChipmunkProWrapper.ucpBodyGetPos(body);
			this.m_velocityAtStart += ChipmunkProWrapper.ucpBodyGetVel(body);
			ChipmunkProWrapper.ucpBodyResetForces(body);
			ChipmunkProWrapper.ucpBodySetVel(body, Vector2.zero);
		}
		this.m_unitCenterPosition /= (float)unitBodyList.Count;
		this.m_velocityAtStart /= (float)unitBodyList.Count;
		EntityManager.SetActivityOfEntity(_unit.m_entity, false, false, true, false, true, true);
		this.m_transformHelper = TransformS.AddComponent(this.m_entity, this.m_unitCenterPosition);
		for (int j = 0; j < _unit.m_entity.m_components.Count; j++)
		{
			if (_unit.m_entity.m_components[j].m_componentType == ComponentType.Transform)
			{
				TransformC transformC = _unit.m_entity.m_components[j] as TransformC;
				if (transformC.parent == null)
				{
					TransformS.ParentComponent(transformC, this.m_transformHelper);
				}
			}
			else if (_unit.m_entity.m_components[j].m_componentType == ComponentType.CameraTarget)
			{
				CameraTargetC cameraTargetC = _unit.m_entity.m_components[j] as CameraTargetC;
				cameraTargetC.m_active = true;
				cameraTargetC.frameTC.m_active = true;
			}
		}
	}

	// Token: 0x040010A5 RID: 4261
	public const float COLLIDER_RADIUS = 20f;

	// Token: 0x040010A6 RID: 4262
	public const float TOTAL_VELOCITY = 1000f;

	// Token: 0x040010A7 RID: 4263
	public const float EXIT_OFFSET = 50f;

	// Token: 0x040010A8 RID: 4264
	public const int COOL_DOWN_TIME = 60;

	// Token: 0x040010A9 RID: 4265
	public const int TWEEN_TIME = 20;

	// Token: 0x040010AA RID: 4266
	private PrefabC m_prefab;

	// Token: 0x040010AB RID: 4267
	private ChipmunkBodyC m_body;

	// Token: 0x040010AC RID: 4268
	private TransformC m_tc;

	// Token: 0x040010AD RID: 4269
	private bool m_startBoost;

	// Token: 0x040010AE RID: 4270
	private int m_coolDownTimer;

	// Token: 0x040010AF RID: 4271
	private TransformC m_transformHelper;

	// Token: 0x040010B0 RID: 4272
	private Vector2 m_velocityAtStart;

	// Token: 0x040010B1 RID: 4273
	private Vector2 m_unitCenterPosition;

	// Token: 0x040010B2 RID: 4274
	private Unit m_unit;
}
