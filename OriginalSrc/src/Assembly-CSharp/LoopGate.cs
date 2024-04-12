using System;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class LoopGate : Unit
{
	// Token: 0x06000246 RID: 582 RVA: 0x0001D7B4 File Offset: 0x0001BBB4
	public LoopGate(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		base.m_graphElement.m_isFlippable = true;
		base.m_graphElement.m_isRotateable = true;
		GameObject gameObject = ResourceManager.GetGameObject("Units/LoopGatePrefab");
		TransformC transformC = TransformS.AddComponent(this.m_entity, base.m_graphElement.m_name);
		TransformS.SetTransform(transformC, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
		GameObject gameObject2 = gameObject.transform.Find("concrete").gameObject;
		PrefabS.AddComponent(transformC, Vector3.zero, gameObject2);
		ucpPolyShape[] array = ChipmunkProS.GeneratePolyShapesFromChildren(gameObject.transform.Find("Colliders").gameObject, Vector2.zero, 100f, 0.1f, 0.9f, (ucpCollisionType)4, 1118208U, false, false);
		this.m_concreteBody = ChipmunkProS.AddKinematicBody(transformC, array, null);
		TransformC transformC2 = TransformS.AddComponent(this.m_entity, "LoopGatePlank");
		TransformS.SetTransform(transformC2, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
		GameObject gameObject3 = gameObject.transform.Find("plank").gameObject;
		PrefabS.AddComponent(transformC2, Vector3.up * 5f, gameObject3);
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject3.transform.Find("plankCollider").gameObject, Vector2.zero, 100f, 0.1f, 0.9f, (ucpCollisionType)4, 17895696U, false, false);
		this.m_cmb = ChipmunkProS.AddDynamicBody(transformC2, ucpPolyShape, null);
		TransformC transformC3 = TransformS.AddComponent(this.m_entity, "anchor");
		TransformS.SetTransform(transformC3, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
		ChipmunkProS.AddPivotJoint(this.m_concreteBody, this.m_cmb, transformC3);
		ChipmunkProS.AddRotaryLimitJoint(this.m_concreteBody, this.m_cmb, transformC3, -0.61086524f, 0.61086524f);
		this.SetBodyRot();
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.PlankCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, true, true);
		}
		this.m_motor = ChipmunkProS.AddSimpleMotor(this.m_concreteBody, this.m_cmb, transformC3, 140f * ((!base.m_graphElement.m_flipped) ? (-1f) : 1f), 7500000f);
		this.m_currentRate = 0f;
		this.m_active = false;
		this.m_separated = false;
		this.m_separateTimer = 0f;
		this.CreateEditorTouchArea(100f, 100f, null, default(Vector2));
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0001DA44 File Offset: 0x0001BE44
	public void SetBodyRot()
	{
		float num = base.m_graphElement.m_rotation.z + 35f;
		if (base.m_graphElement.m_flipped)
		{
			num -= 70f;
		}
		ChipmunkProWrapper.ucpBodySetAngle(this.m_cmb.body, num * 0.017453292f);
		TransformS.SetGlobalRotation(this.m_cmb.TC, Vector3.forward * num);
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0001DAB4 File Offset: 0x0001BEB4
	public override void Update()
	{
		base.Update();
		if (this.m_minigame.m_editing)
		{
			this.SetBodyRot();
		}
		else if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted)
		{
			if (this.m_separated)
			{
				this.m_separateTimer -= Main.m_gameDeltaTime;
				if (this.m_separateTimer <= 0f)
				{
					this.m_separateTimer = 0f;
					this.m_active = true;
					this.m_separated = false;
					if (this.m_cmb.TC.transform.rotation.z - this.m_concreteBody.TC.transform.rotation.z < 0f)
					{
						this.m_currentRate = -140f;
					}
					else
					{
						this.m_currentRate = 140f;
					}
					ChipmunkProWrapper.ucpSimpleMotorSetRate(this.m_motor.constraint, this.m_currentRate);
				}
			}
			if (this.m_active)
			{
				if (this.m_currentRate < 0f && this.m_cmb.TC.transform.rotation.z - this.m_concreteBody.TC.transform.rotation.z > 32.5f)
				{
					this.m_active = false;
				}
				else if (this.m_currentRate > 0f && this.m_cmb.TC.transform.rotation.z - this.m_concreteBody.TC.transform.rotation.z < -32.5f)
				{
					this.m_active = false;
				}
			}
		}
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0001DC88 File Offset: 0x0001C088
	private void PlankCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		Vector3 vector = this.m_cmb.TC.transform.InverseTransformPoint(_pair.point);
		if ((_phase == ucpCollisionPhase.Begin || _phase == ucpCollisionPhase.Persist) && Mathf.Abs(vector.x) <= 140f)
		{
			this.m_separated = false;
		}
		else if (_phase == ucpCollisionPhase.Separate)
		{
			this.m_separated = true;
			this.m_separateTimer = 0.5f;
		}
	}

	// Token: 0x0400026E RID: 622
	private ChipmunkBodyC m_concreteBody;

	// Token: 0x0400026F RID: 623
	private ChipmunkBodyC m_cmb;

	// Token: 0x04000270 RID: 624
	private ChipmunkBodyC m_triggerBody;

	// Token: 0x04000271 RID: 625
	private ChipmunkConstraintC m_motor;

	// Token: 0x04000272 RID: 626
	private bool m_active;

	// Token: 0x04000273 RID: 627
	private bool m_separated;

	// Token: 0x04000274 RID: 628
	private float m_separateTimer;

	// Token: 0x04000275 RID: 629
	private const float INITIAL_RATE = 140f;

	// Token: 0x04000276 RID: 630
	private float m_currentRate;
}
