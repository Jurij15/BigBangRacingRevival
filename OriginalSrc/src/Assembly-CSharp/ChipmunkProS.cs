using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000502 RID: 1282
public static class ChipmunkProS
{
	// Token: 0x060024D2 RID: 9426 RVA: 0x00196330 File Offset: 0x00194730
	public static void Initialize(int _collisionTypeCount)
	{
		ChipmunkProS.m_collisionTypeCount = _collisionTypeCount + 1;
		ChipmunkProS.m_bodies = new DynamicArray<ChipmunkBodyC>(100, 0.5f, 0.25f, 0.5f);
		ChipmunkProS.m_constraints = new DynamicArray<ChipmunkConstraintC>(100, 0.5f, 0.25f, 0.5f);
		ChipmunkProWrapper.ucpInitialize();
		ChipmunkProWrapper.ucpSpaceSetGravity(Vector2.up * -400f);
		ChipmunkProWrapper.ucpSpaceSetCollisionBias(0.001f);
		ChipmunkProS.m_staticBodyEntity = EntityManager.AddEntity();
		ChipmunkProS.m_staticBodyEntity.m_persistent = true;
		ChipmunkProS.m_staticBodyTC = TransformS.AddComponent(ChipmunkProS.m_staticBodyEntity, "Chipmunk Static Body", Vector3.zero);
		ChipmunkProS.m_staticBody = ChipmunkProS.AddStaticBody(ChipmunkProS.m_staticBodyTC, null);
		ChipmunkProS.m_globalCollisionDelegates = new CollisionDelegate[ChipmunkProS.m_collisionTypeCount, ChipmunkProS.m_collisionTypeCount, 3];
		ChipmunkProS.m_collisionDelegatePhases = new uint[ChipmunkProS.m_collisionTypeCount, ChipmunkProS.m_collisionTypeCount];
		ChipmunkProS.m_collisionDelegateCounts = new int[ChipmunkProS.m_collisionTypeCount, ChipmunkProS.m_collisionTypeCount];
	}

	// Token: 0x060024D3 RID: 9427 RVA: 0x0019641C File Offset: 0x0019481C
	public static void Free()
	{
		if (ChipmunkProS.m_bodies.m_aliveCount == 0 && ChipmunkProS.m_constraints.m_aliveCount == 0)
		{
			EntityManager.RemoveEntity(ChipmunkProS.m_staticBodyEntity);
			ChipmunkProS.m_staticBodyEntity = null;
			ChipmunkProS.m_staticBodyTC = null;
			ChipmunkProS.m_staticBody = null;
			ChipmunkProWrapper.ucpFree();
		}
		else
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Trying to clear populated chipmunk world. Bodies left: ",
				ChipmunkProS.m_bodies.m_aliveCount,
				" / Constraints left:",
				ChipmunkProS.m_constraints.m_aliveCount
			}));
		}
	}

	// Token: 0x060024D4 RID: 9428 RVA: 0x001964B4 File Offset: 0x001948B4
	public static int GetTotalCollisionDelegateCount()
	{
		int num = 0;
		uint num2 = 0U;
		while ((ulong)num2 < (ulong)((long)ChipmunkProS.m_collisionTypeCount))
		{
			uint num3 = 0U;
			while ((ulong)num3 < (ulong)((long)ChipmunkProS.m_collisionTypeCount))
			{
				num += ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)num2), (int)((UIntPtr)num3)];
				num3 += 1U;
			}
			num2 += 1U;
		}
		return num;
	}

	// Token: 0x060024D5 RID: 9429 RVA: 0x00196508 File Offset: 0x00194908
	public static IntPtr AddShapeToBody(ChipmunkBodyC _cmb, ucpShape _shape, float _currentMass, float _currentMoment)
	{
		IntPtr intPtr = IntPtr.Zero;
		float num = _currentMoment;
		if (_shape.shapeType == ucpShapeType.Circle)
		{
			ucpCircleShape ucpCircleShape = _shape as ucpCircleShape;
			num += ChipmunkProWrapper.ucpMomentForCircle(ucpCircleShape.mass, ucpCircleShape.innerRadius, ucpCircleShape.outerRadius, ucpCircleShape.offset);
			intPtr = ChipmunkProWrapper.ucpCircleShapeNew(_cmb.body, ucpCircleShape.outerRadius, ucpCircleShape.offset, ucpCircleShape.collisionType);
		}
		else if (_shape.shapeType == ucpShapeType.Poly)
		{
			ucpPolyShape ucpPolyShape = _shape as ucpPolyShape;
			num += ChipmunkProWrapper.ucpMomentForPoly(ucpPolyShape.mass, ucpPolyShape.numVerts, ucpPolyShape.verts, ucpPolyShape.offset, 0f);
			if (num <= 0f)
			{
				Debug.LogError("Probably WRONG WINDING! should be CCW");
			}
			intPtr = ChipmunkProWrapper.ucpPolyShapeNew(_cmb.body, ucpPolyShape.numVerts, ucpPolyShape.verts, new ucpTransform(ucpPolyShape.offset.x, ucpPolyShape.offset.y), 0f, ucpPolyShape.collisionType);
		}
		else if (_shape.shapeType == ucpShapeType.Segment)
		{
			ucpSegmentShape ucpSegmentShape = _shape as ucpSegmentShape;
			num += ChipmunkProWrapper.ucpMomentForSegment(ucpSegmentShape.mass, ucpSegmentShape.a, ucpSegmentShape.b, 0f);
			intPtr = ChipmunkProWrapper.ucpSegmentShapeNew(_cmb.body, ucpSegmentShape.a + ucpSegmentShape.offset, ucpSegmentShape.b + ucpSegmentShape.offset, ucpSegmentShape.radius, ucpSegmentShape.collisionType);
		}
		if (intPtr != IntPtr.Zero)
		{
			ChipmunkProWrapper.ucpSpaceAddShape(intPtr);
		}
		else
		{
			Debug.LogError("NOOOO!! Shape ptr is zero");
		}
		if (ChipmunkProWrapper.ucpBodyGetType(_cmb.body) == ucpBodyType.DYNAMIC)
		{
			float num2 = _currentMass + _shape.mass;
			if (num2 > 0f)
			{
				ChipmunkProWrapper.ucpBodySetMass(_cmb.body, num2);
			}
			_cmb.m_mass = num2;
			if (num > 0f)
			{
				ChipmunkProWrapper.ucpBodySetMoment(_cmb.body, num);
			}
			_cmb.m_moment = num;
		}
		ChipmunkProWrapper.ucpShapeSetSensor(intPtr, _shape.sensor);
		ChipmunkProWrapper.ucpShapeSetElasticity(intPtr, _shape.elasticity);
		ChipmunkProWrapper.ucpShapeSetFriction(intPtr, _shape.friction);
		ucpShapeFilter ucpShapeFilter = default(ucpShapeFilter);
		ucpShapeFilter.ucpShapeFilterNone();
		ucpShapeFilter.group = _shape.group;
		ucpShapeFilter.categories = _shape.layers;
		ucpShapeFilter.mask = _shape.layers;
		ChipmunkProWrapper.ucpShapeSetFilter(intPtr, ucpShapeFilter);
		ChipmunkProWrapper.ucpShapeSetSurfaceVelocity(intPtr, _shape.surfaceVelocity);
		_cmb.shapes.Add(new ChipmunkBodyShape(intPtr, _shape.layers, _shape.tag));
		_shape.shapePtr = intPtr;
		return intPtr;
	}

	// Token: 0x060024D6 RID: 9430 RVA: 0x0019678C File Offset: 0x00194B8C
	public static ChipmunkBodyC AddDynamicBody(TransformC _tc, ucpShape _shape, IComponent _customComponent = null)
	{
		ucpShape[] array = new ucpShape[] { _shape };
		return ChipmunkProS.AddDynamicBody(_tc, array, _customComponent);
	}

	// Token: 0x060024D7 RID: 9431 RVA: 0x001967AC File Offset: 0x00194BAC
	public static ChipmunkBodyC AddDynamicBody(TransformC _tc, ucpShape[] _shapes, IComponent _customComponent = null)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.AddItem();
		chipmunkBodyC.customComponent = _customComponent;
		chipmunkBodyC.m_isDynamic = true;
		chipmunkBodyC.TC = _tc;
		chipmunkBodyC.body = ChipmunkProWrapper.ucpAddBody(1f, 1f, chipmunkBodyC.m_index);
		ChipmunkProWrapper.ucpBodySetPos(chipmunkBodyC.body, _tc.transform.position);
		float num = _tc.transform.rotation.eulerAngles.z;
		if (num > 180f)
		{
			num -= 360f;
		}
		ChipmunkProWrapper.ucpBodySetAngle(chipmunkBodyC.body, num * 0.017453292f);
		if (_shapes != null)
		{
			for (int i = 0; i < _shapes.Length; i++)
			{
				float num2 = 0f;
				float num3 = 0f;
				if (chipmunkBodyC.shapes.Count > 0)
				{
					num2 = ChipmunkProWrapper.ucpBodyGetMass(chipmunkBodyC.body);
					num3 = ChipmunkProWrapper.ucpBodyGetMoment(chipmunkBodyC.body);
				}
				ChipmunkProS.AddShapeToBody(chipmunkBodyC, _shapes[i], num2, num3);
			}
		}
		EntityManager.AddComponentToEntity(_tc.p_entity, chipmunkBodyC);
		return chipmunkBodyC;
	}

	// Token: 0x060024D8 RID: 9432 RVA: 0x001968C4 File Offset: 0x00194CC4
	public static ChipmunkBodyC AddKinematicBody(TransformC _tc, ucpShape _shape, IComponent _customComponent = null)
	{
		ucpShape[] array = new ucpShape[] { _shape };
		return ChipmunkProS.AddKinematicBody(_tc, array, _customComponent);
	}

	// Token: 0x060024D9 RID: 9433 RVA: 0x001968E4 File Offset: 0x00194CE4
	public static ChipmunkBodyC AddKinematicBody(TransformC _tc, ucpShape[] _shapes, IComponent _customComponent = null)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.AddItem();
		chipmunkBodyC.customComponent = _customComponent;
		chipmunkBodyC.m_isKinematic = true;
		chipmunkBodyC.TC = _tc;
		chipmunkBodyC.body = ChipmunkProWrapper.ucpAddKinematicBody(chipmunkBodyC.m_index);
		ChipmunkProWrapper.ucpBodySetPos(chipmunkBodyC.body, _tc.transform.position);
		float num = _tc.transform.rotation.eulerAngles.z;
		if (num > 180f)
		{
			num -= 360f;
		}
		ChipmunkProWrapper.ucpBodySetAngle(chipmunkBodyC.body, num * 0.017453292f);
		if (_shapes != null)
		{
			for (int i = 0; i < _shapes.Length; i++)
			{
				float num2 = 0f;
				float num3 = 0f;
				if (chipmunkBodyC.shapes.Count > 0)
				{
					num2 = ChipmunkProWrapper.ucpBodyGetMass(chipmunkBodyC.body);
					num3 = ChipmunkProWrapper.ucpBodyGetMoment(chipmunkBodyC.body);
				}
				if (_shapes[i].group == 0U)
				{
					_shapes[i].group = uint.MaxValue;
				}
				ChipmunkProS.AddShapeToBody(chipmunkBodyC, _shapes[i], num2, num3);
			}
		}
		EntityManager.AddComponentToEntity(_tc.p_entity, chipmunkBodyC);
		return chipmunkBodyC;
	}

	// Token: 0x060024DA RID: 9434 RVA: 0x00196A0C File Offset: 0x00194E0C
	public static ChipmunkBodyC AddStaticBody(TransformC _transformComponent, IComponent _customComponent = null)
	{
		ucpShape[] array = null;
		return ChipmunkProS.AddStaticBody(_transformComponent, array, _customComponent);
	}

	// Token: 0x060024DB RID: 9435 RVA: 0x00196A24 File Offset: 0x00194E24
	public static ChipmunkBodyC AddStaticBody(TransformC _transformComponent, ucpShape _shape, IComponent _customComponent = null)
	{
		ucpShape[] array = new ucpShape[] { _shape };
		return ChipmunkProS.AddStaticBody(_transformComponent, array, _customComponent);
	}

	// Token: 0x060024DC RID: 9436 RVA: 0x00196A44 File Offset: 0x00194E44
	public static ChipmunkBodyC AddStaticBody(TransformC _transformComponent, ucpShape[] _shapes, IComponent _customComponent = null)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.AddItem();
		chipmunkBodyC.customComponent = _customComponent;
		chipmunkBodyC.m_isStatic = true;
		chipmunkBodyC.TC = _transformComponent;
		chipmunkBodyC.body = ChipmunkProWrapper.ucpAddStaticBody(chipmunkBodyC.m_index);
		ChipmunkProWrapper.ucpBodySetPos(chipmunkBodyC.body, chipmunkBodyC.TC.transform.position);
		float num = chipmunkBodyC.TC.transform.rotation.eulerAngles.z;
		if (num > 180f)
		{
			num -= 360f;
		}
		ChipmunkProWrapper.ucpBodySetAngle(chipmunkBodyC.body, num * 0.017453292f);
		if (_shapes != null)
		{
			for (int i = 0; i < _shapes.Length; i++)
			{
				if (_shapes[i].group == 0U)
				{
					_shapes[i].group = uint.MaxValue;
				}
				ChipmunkProS.AddShapeToBody(chipmunkBodyC, _shapes[i], 0f, 0f);
			}
		}
		EntityManager.AddComponentToEntity(_transformComponent.p_entity, chipmunkBodyC);
		return chipmunkBodyC;
	}

	// Token: 0x060024DD RID: 9437 RVA: 0x00196B40 File Offset: 0x00194F40
	public static void SetBodyShapeMask(ChipmunkBodyC _body, uint _mask)
	{
		for (int i = 0; i < _body.shapes.Count; i++)
		{
			ucpShapeFilter ucpShapeFilter = ChipmunkProWrapper.ucpShapeGetFilter(_body.shapes[i].shapePtr);
			ucpShapeFilter.mask = _mask;
			ucpShapeFilter.categories = _mask;
			ChipmunkProWrapper.ucpShapeSetFilter(_body.shapes[i].shapePtr, ucpShapeFilter);
		}
	}

	// Token: 0x060024DE RID: 9438 RVA: 0x00196BA7 File Offset: 0x00194FA7
	public static void SetBodyGravity(ChipmunkBodyC _body, Vector2 _gravity)
	{
		ChipmunkProWrapper.ucpBodySetGravity(_body.body, _gravity);
	}

	// Token: 0x060024DF RID: 9439 RVA: 0x00196BB5 File Offset: 0x00194FB5
	public static void SetBodyLinearDamp(ChipmunkBodyC _body, Vector2 _linearDamp)
	{
		_body.m_linearDamp = _linearDamp;
		ChipmunkProWrapper.ucpBodySetLinearDamp(_body.body, new Vector2(Mathf.Pow(_linearDamp.x, (float)Main.m_logicFPS), Mathf.Pow(_linearDamp.y, (float)Main.m_logicFPS)));
	}

	// Token: 0x060024E0 RID: 9440 RVA: 0x00196BF2 File Offset: 0x00194FF2
	public static void SetBodyAngularDamp(ChipmunkBodyC _body, float _angularDamp)
	{
		_body.m_angularDamp = _angularDamp;
		ChipmunkProWrapper.ucpBodySetAngularDamp(_body.body, Mathf.Pow(_angularDamp, (float)Main.m_logicFPS));
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x00196C14 File Offset: 0x00195014
	public static ChipmunkConstraintC AddDampedRotarySpring(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _anchor, float _restAngle, float _stiffness, float _damping)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		if (_anchor.parent == null)
		{
			TransformS.ParentComponent(_anchor, _bodyA.TC);
		}
		IntPtr intPtr = ChipmunkProWrapper.ucpDampedRotarySpringNew(_bodyA.body, _bodyB.body, _restAngle, _stiffness, _damping);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		chipmunkConstraintC.anchor1 = _anchor;
		chipmunkConstraintC.type = ucpConstraintType.DampedRotarySpring;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x00196C98 File Offset: 0x00195098
	public static ChipmunkConstraintC AddDampedSpring(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _anchor1, TransformC _anchor2, float _restLength, float _stiffness, float _damping)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		TransformS.ParentComponent(_anchor1, _bodyA.TC);
		TransformS.ParentComponent(_anchor2, _bodyB.TC);
		IntPtr intPtr = ChipmunkProWrapper.ucpDampedSpringNew(_bodyA.body, _bodyB.body, _anchor1.transform.localPosition, _anchor2.transform.localPosition, _restLength, _stiffness, _damping);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		chipmunkConstraintC.type = ucpConstraintType.DampedSpring;
		chipmunkConstraintC.anchor1 = _anchor1;
		chipmunkConstraintC.anchor2 = _anchor2;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor1.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x00196D44 File Offset: 0x00195144
	public static ChipmunkConstraintC AddGearJoint(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _anchor, float _phase, float _ratio)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		if (_anchor.parent == null)
		{
			TransformS.ParentComponent(_anchor, _bodyA.TC);
		}
		IntPtr intPtr = ChipmunkProWrapper.ucpGearJointNew(_bodyA.body, _bodyB.body, _phase, _ratio);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		chipmunkConstraintC.type = ucpConstraintType.GearJoint;
		chipmunkConstraintC.anchor1 = _anchor;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024E4 RID: 9444 RVA: 0x00196DC8 File Offset: 0x001951C8
	public static ChipmunkConstraintC AddGrooveJoint(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _grooveA, TransformC _grooveB, TransformC _anchor)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		TransformS.ParentComponent(_grooveA, _bodyA.TC);
		TransformS.ParentComponent(_grooveB, _bodyA.TC);
		TransformS.ParentComponent(_anchor, _bodyB.TC);
		IntPtr intPtr = ChipmunkProWrapper.ucpGrooveJointNew(_bodyA.body, _bodyB.body, _grooveA.transform.localPosition, _grooveB.transform.localPosition, _anchor.transform.localPosition);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		chipmunkConstraintC.type = ucpConstraintType.GrooveJoint;
		chipmunkConstraintC.anchor1 = _anchor;
		chipmunkConstraintC.grooveA = _grooveA;
		chipmunkConstraintC.grooveB = _grooveB;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024E5 RID: 9445 RVA: 0x00196E98 File Offset: 0x00195298
	public static ChipmunkConstraintC AddPinJoint(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _anchor1, TransformC _anchor2)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		TransformS.ParentComponent(_anchor1, _bodyA.TC);
		TransformS.ParentComponent(_anchor2, _bodyB.TC);
		IntPtr intPtr = ChipmunkProWrapper.ucpPinJointNew(_bodyA.body, _bodyB.body, _anchor1.transform.localPosition, _anchor2.transform.localPosition);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		chipmunkConstraintC.type = ucpConstraintType.PinJoint;
		chipmunkConstraintC.anchor1 = _anchor1;
		chipmunkConstraintC.anchor2 = _anchor2;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor1.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024E6 RID: 9446 RVA: 0x00196F40 File Offset: 0x00195340
	public static ChipmunkConstraintC AddPivotJoint(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _anchor)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		if (_anchor.parent == null)
		{
			TransformS.ParentComponent(_anchor, _bodyA.TC);
		}
		IntPtr intPtr = ChipmunkProWrapper.ucpPivotJointNew(_bodyA.body, _bodyB.body, _anchor.transform.position);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		chipmunkConstraintC.type = ucpConstraintType.PivotJoint;
		chipmunkConstraintC.anchor1 = _anchor;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x00196FD0 File Offset: 0x001953D0
	public static ChipmunkConstraintC AddPivotJoint2(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _anchor1, TransformC _anchor2)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		TransformS.ParentComponent(_anchor1, _bodyA.TC);
		TransformS.ParentComponent(_anchor2, _bodyB.TC);
		IntPtr intPtr = ChipmunkProWrapper.ucpPivotJointNew2(_bodyA.body, _bodyB.body, _anchor1.transform.localPosition, _anchor2.transform.localPosition);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		chipmunkConstraintC.type = ucpConstraintType.PivotJoint;
		chipmunkConstraintC.anchor1 = _anchor1;
		chipmunkConstraintC.anchor2 = _anchor2;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor1.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x00197078 File Offset: 0x00195478
	public static ChipmunkConstraintC AddRatchetJoint(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _anchor, float _phase, float _ratchet)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		if (_anchor.parent == null)
		{
			TransformS.ParentComponent(_anchor, _bodyA.TC);
		}
		IntPtr intPtr = ChipmunkProWrapper.ucpRatchetJointNew(_bodyA.body, _bodyB.body, _phase, _ratchet);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		chipmunkConstraintC.type = ucpConstraintType.RatchetJoint;
		chipmunkConstraintC.anchor1 = _anchor;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x001970FC File Offset: 0x001954FC
	public static ChipmunkConstraintC AddRotaryLimitJoint(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _anchor, float _min, float _max)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		if (_anchor.parent == null)
		{
			TransformS.ParentComponent(_anchor, _bodyA.TC);
		}
		IntPtr intPtr = ChipmunkProWrapper.ucpRotaryLimitJointNew(_bodyA.body, _bodyB.body, _min, _max);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		chipmunkConstraintC.type = ucpConstraintType.RotaryLimitJoint;
		chipmunkConstraintC.anchor1 = _anchor;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x00197180 File Offset: 0x00195580
	public static ChipmunkConstraintC AddSimpleMotor(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _anchor, float _rate, float _maxForce)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		if (_anchor.parent == null)
		{
			TransformS.ParentComponent(_anchor, _bodyA.TC);
		}
		IntPtr intPtr = ChipmunkProWrapper.ucpSimpleMotorNew(_bodyA.body, _bodyB.body, _rate);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		ChipmunkProWrapper.ucpConstraintSetMaxForce(intPtr, _maxForce);
		chipmunkConstraintC.type = ucpConstraintType.SimpleMotor;
		chipmunkConstraintC.anchor1 = _anchor;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024EB RID: 9451 RVA: 0x00197208 File Offset: 0x00195608
	public static ChipmunkConstraintC AddSlideJoint(ChipmunkBodyC _bodyA, ChipmunkBodyC _bodyB, TransformC _anchor1, TransformC _anchor2, float _min, float _max)
	{
		ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.AddItem();
		TransformS.ParentComponent(_anchor1, _bodyA.TC);
		TransformS.ParentComponent(_anchor2, _bodyB.TC);
		IntPtr intPtr = ChipmunkProWrapper.ucpSlideJointNew(_bodyA.body, _bodyB.body, _anchor1.transform.localPosition, _anchor2.transform.localPosition, _min, _max);
		ChipmunkProWrapper.ucpAddConstraint(intPtr, chipmunkConstraintC.m_index);
		chipmunkConstraintC.type = ucpConstraintType.SlideJoint;
		chipmunkConstraintC.anchor1 = _anchor1;
		chipmunkConstraintC.anchor2 = _anchor2;
		chipmunkConstraintC.bodyA = _bodyA;
		chipmunkConstraintC.bodyB = _bodyB;
		chipmunkConstraintC.constraint = intPtr;
		EntityManager.AddComponentToEntity(_anchor1.p_entity, chipmunkConstraintC);
		return chipmunkConstraintC;
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x001972B4 File Offset: 0x001956B4
	public static void RemoveBody(ChipmunkBodyC _c)
	{
		if (ChipmunkProS.m_isInsideHandleCollisions)
		{
			Debug.LogWarning("Removing body inside HandleCollisions... inserting in to queue.");
			if (!ChipmunkProS.m_handleCollisionsRemoveBodyQueue.Contains(_c))
			{
				ChipmunkProS.m_handleCollisionsRemoveBodyQueue.Add(_c);
			}
			return;
		}
		if (_c.body == IntPtr.Zero)
		{
			Debug.LogError("Trying to remove a body twice!");
			return;
		}
		ChipmunkProWrapper.ucpClearCollisionLists();
		ChipmunkProS.RemoveConstraintsFromBody(_c);
		int num = ChipmunkProWrapper.ucpRemoveBody(_c.body, ChipmunkProS.m_removedConstraintResults, ChipmunkProS.m_maxRemovedConstraintResultsCount);
		ChipmunkProS.HandleCollisionEvents();
		ChipmunkProS.RemoveAllCollisionHandlers(_c);
		_c.TC = null;
		_c.customComponent = null;
		_c.body = IntPtr.Zero;
		EntityManager.RemoveComponentFromEntity(_c);
		ChipmunkProS.m_bodies.RemoveItem(_c);
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x00197368 File Offset: 0x00195768
	public static void RemoveConstraintsFromBody(ChipmunkBodyC _c)
	{
		for (int i = ChipmunkProS.m_constraints.m_aliveCount - 1; i > -1; i--)
		{
			ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.m_array[ChipmunkProS.m_constraints.m_aliveIndices[i]];
			if (chipmunkConstraintC.bodyA == _c || chipmunkConstraintC.bodyB == _c)
			{
				ChipmunkProS.RemoveConstraint(chipmunkConstraintC);
			}
		}
	}

	// Token: 0x060024EE RID: 9454 RVA: 0x001973C8 File Offset: 0x001957C8
	public static void RemoveConstraint(ChipmunkConstraintC _c)
	{
		if (_c.constraint != IntPtr.Zero)
		{
			if (!_c.addedToSpace)
			{
				ChipmunkProWrapper.ucpSpaceAddConstraint(_c.constraint);
			}
			ChipmunkProWrapper.ucpRemoveConstraint(_c.constraint);
		}
		EntityManager.RemoveComponentFromEntity(_c);
		ChipmunkProS.m_constraints.RemoveItem(_c);
	}

	// Token: 0x060024EF RID: 9455 RVA: 0x00197420 File Offset: 0x00195820
	public static void AddGlobalCollisionHandler(CollisionDelegate _collisionHandler, ucpCollisionType _collisionTypeA, ucpCollisionType _collisionTypeB, bool _handleBegin, bool _handlePersist, bool _handleSeparate)
	{
		if (_handleBegin)
		{
			ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB), 0] = _collisionHandler;
			ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)]++;
			uint num = ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)];
			ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] = num | 256U;
		}
		if (_handlePersist)
		{
			ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB), 1] = _collisionHandler;
			ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)]++;
			uint num2 = ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)];
			ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] = num2 | 16U;
		}
		if (_handleSeparate)
		{
			ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB), 2] = _collisionHandler;
			ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)]++;
			uint num3 = ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)];
			ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] = num3 | 1U;
		}
		uint num4 = ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] | ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeB), (int)((UIntPtr)_collisionTypeA)];
		ChipmunkProWrapper.ucpSpaceAddCollisionHandler(_collisionTypeA, _collisionTypeB, (num4 & 256U) == 256U, (num4 & 16U) == 16U, (num4 & 1U) == 1U);
	}

	// Token: 0x060024F0 RID: 9456 RVA: 0x00197550 File Offset: 0x00195950
	public static void AddCollisionHandler(ChipmunkBodyC _c, CollisionDelegate _collisionHandler, ucpCollisionType _collisionTypeA, ucpCollisionType _collisionTypeB, bool _handleBegin, bool _handlePersist, bool _handleSeparate)
	{
		if (_handleBegin)
		{
			_c.m_collisionDelegates[(int)((UIntPtr)_collisionTypeB), 0] = _collisionHandler;
			_c.m_collisionDelegateTypes[(int)((UIntPtr)_collisionTypeB), 0] = _collisionTypeA;
			ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)]++;
			uint num = ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)];
			ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] = num | 256U;
		}
		if (_handlePersist)
		{
			_c.m_collisionDelegates[(int)((UIntPtr)_collisionTypeB), 1] = _collisionHandler;
			_c.m_collisionDelegateTypes[(int)((UIntPtr)_collisionTypeB), 1] = _collisionTypeA;
			ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)]++;
			uint num2 = ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)];
			ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] = num2 | 16U;
		}
		if (_handleSeparate)
		{
			_c.m_collisionDelegates[(int)((UIntPtr)_collisionTypeB), 2] = _collisionHandler;
			_c.m_collisionDelegateTypes[(int)((UIntPtr)_collisionTypeB), 2] = _collisionTypeA;
			ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)]++;
			uint num3 = ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)];
			ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] = num3 | 1U;
		}
		uint num4 = ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] | ChipmunkProS.m_collisionDelegatePhases[(int)((UIntPtr)_collisionTypeB), (int)((UIntPtr)_collisionTypeA)];
		ChipmunkProWrapper.ucpSpaceAddCollisionHandler(_collisionTypeA, _collisionTypeB, (num4 & 256U) == 256U, (num4 & 16U) == 16U, (num4 & 1U) == 1U);
	}

	// Token: 0x060024F1 RID: 9457 RVA: 0x001976AC File Offset: 0x00195AAC
	public static void RemoveGlobalCollisionHandler(ucpCollisionType _collisionTypeA, ucpCollisionType _collisionTypeB, bool _began, bool _persist, bool _separate)
	{
		if (_began)
		{
			ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB), 0] = null;
			ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)]--;
			if (ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] == 0 && ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeB), (int)((UIntPtr)_collisionTypeA)] == 0)
			{
				ChipmunkProWrapper.ucpSpaceRemoveCollisionHandler(_collisionTypeA, _collisionTypeB);
			}
		}
		if (_persist)
		{
			ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB), 1] = null;
			ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)]--;
			if (ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] == 0 && ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeB), (int)((UIntPtr)_collisionTypeA)] == 0)
			{
				ChipmunkProWrapper.ucpSpaceRemoveCollisionHandler(_collisionTypeA, _collisionTypeB);
			}
		}
		if (_separate)
		{
			ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB), 2] = null;
			ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)]--;
			if (ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeA), (int)((UIntPtr)_collisionTypeB)] == 0 && ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_collisionTypeB), (int)((UIntPtr)_collisionTypeA)] == 0)
			{
				ChipmunkProWrapper.ucpSpaceRemoveCollisionHandler(_collisionTypeA, _collisionTypeB);
			}
		}
	}

	// Token: 0x060024F2 RID: 9458 RVA: 0x001977BC File Offset: 0x00195BBC
	public static void RemoveCollisionHandler(ChipmunkBodyC _c, CollisionDelegate _collisionHandler)
	{
		for (int i = 0; i < _c.m_collisionDelegates.GetLength(0); i++)
		{
			if (_c.m_collisionDelegates[i, 0] == _collisionHandler)
			{
				_c.m_collisionDelegates[i, 0] = null;
				ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 0]), i]--;
				if (ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 0]), i] == 0 && ChipmunkProS.m_collisionDelegateCounts[i, (int)((UIntPtr)_c.m_collisionDelegateTypes[i, 0])] == 0)
				{
					ChipmunkProWrapper.ucpSpaceRemoveCollisionHandler(_c.m_collisionDelegateTypes[i, 0], (ucpCollisionType)i);
				}
			}
			if (_c.m_collisionDelegates[i, 1] == _collisionHandler)
			{
				_c.m_collisionDelegates[i, 1] = null;
				ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 1]), i]--;
				if (ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 1]), i] == 0 && ChipmunkProS.m_collisionDelegateCounts[i, (int)((UIntPtr)_c.m_collisionDelegateTypes[i, 1])] == 0)
				{
					ChipmunkProWrapper.ucpSpaceRemoveCollisionHandler(_c.m_collisionDelegateTypes[i, 1], (ucpCollisionType)i);
				}
			}
			if (_c.m_collisionDelegates[i, 2] == _collisionHandler)
			{
				_c.m_collisionDelegates[i, 2] = null;
				ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 2]), i]--;
				if (ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 2]), i] == 0 && ChipmunkProS.m_collisionDelegateCounts[i, (int)((UIntPtr)_c.m_collisionDelegateTypes[i, 2])] == 0)
				{
					ChipmunkProWrapper.ucpSpaceRemoveCollisionHandler(_c.m_collisionDelegateTypes[i, 2], (ucpCollisionType)i);
				}
			}
		}
	}

	// Token: 0x060024F3 RID: 9459 RVA: 0x001979A0 File Offset: 0x00195DA0
	public static void RemoveAllCollisionHandlers(ChipmunkBodyC _c)
	{
		for (int i = 0; i < _c.m_collisionDelegates.GetLength(0); i++)
		{
			if (_c.m_collisionDelegates[i, 0] != null)
			{
				_c.m_collisionDelegates[i, 0] = null;
				ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 0]), i]--;
				if ((ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 0]), i] == 0) & (ChipmunkProS.m_collisionDelegateCounts[i, (int)((UIntPtr)_c.m_collisionDelegateTypes[i, 0])] == 0))
				{
					ChipmunkProWrapper.ucpSpaceRemoveCollisionHandler(_c.m_collisionDelegateTypes[i, 0], (ucpCollisionType)i);
				}
			}
			if (_c.m_collisionDelegates[i, 1] != null)
			{
				_c.m_collisionDelegates[i, 1] = null;
				ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 1]), i]--;
				if ((ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 1]), i] == 0) & (ChipmunkProS.m_collisionDelegateCounts[i, (int)((UIntPtr)_c.m_collisionDelegateTypes[i, 1])] == 0))
				{
					ChipmunkProWrapper.ucpSpaceRemoveCollisionHandler(_c.m_collisionDelegateTypes[i, 1], (ucpCollisionType)i);
				}
			}
			if (_c.m_collisionDelegates[i, 2] != null)
			{
				_c.m_collisionDelegates[i, 2] = null;
				ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 2]), i]--;
				if ((ChipmunkProS.m_collisionDelegateCounts[(int)((UIntPtr)_c.m_collisionDelegateTypes[i, 2]), i] == 0) & (ChipmunkProS.m_collisionDelegateCounts[i, (int)((UIntPtr)_c.m_collisionDelegateTypes[i, 2])] == 0))
				{
					ChipmunkProWrapper.ucpSpaceRemoveCollisionHandler(_c.m_collisionDelegateTypes[i, 2], (ucpCollisionType)i);
				}
			}
		}
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x00197B78 File Offset: 0x00195F78
	private static ucpCollisionPair ReverseCollisionPair(ucpCollisionPair _pair)
	{
		ucpCollisionPair ucpCollisionPair = _pair;
		ucpCollisionPair.shapeA = _pair.shapeB;
		ucpCollisionPair.shapeB = _pair.shapeA;
		ucpCollisionPair.ucpComponentIndexA = _pair.ucpComponentIndexB;
		ucpCollisionPair.ucpComponentIndexB = _pair.ucpComponentIndexA;
		ucpCollisionPair.normal = -_pair.normal;
		ucpCollisionPair.impulse = -_pair.impulse;
		ucpCollisionPair.friction = -_pair.friction;
		return ucpCollisionPair;
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x00197BFC File Offset: 0x00195FFC
	public static int HandleCollisionEvents()
	{
		int num = ChipmunkProWrapper.ucpGetBeginCollisionCount();
		if (num > 0)
		{
			ucpCollisionPair[] array = new ucpCollisionPair[num];
			ChipmunkProWrapper.ucpGetBeginCollisions(array, array.Length);
			for (int i = 0; i < num; i++)
			{
				ucpCollisionPair ucpCollisionPair = array[i];
				ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[ucpCollisionPair.ucpComponentIndexA];
				ChipmunkBodyC chipmunkBodyC2 = ChipmunkProS.m_bodies.m_array[ucpCollisionPair.ucpComponentIndexB];
				uint typeA = ucpCollisionPair.typeA;
				uint typeB = ucpCollisionPair.typeB;
				if ((ulong)typeA <= (ulong)((long)ChipmunkProS.m_collisionTypeCount) && (ulong)typeB <= (ulong)((long)ChipmunkProS.m_collisionTypeCount))
				{
					if (ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeA), (int)((UIntPtr)typeB), 0] != null)
					{
						ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeA), (int)((UIntPtr)typeB), 0](ucpCollisionPair, ucpCollisionPhase.Begin);
					}
					if (typeA != typeB && ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeB), (int)((UIntPtr)typeA), 0] != null)
					{
						ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeB), (int)((UIntPtr)typeA), 0](ucpCollisionPair, ucpCollisionPhase.Begin);
					}
					if (chipmunkBodyC.m_collisionDelegates[(int)((UIntPtr)typeB), 0] != null)
					{
						chipmunkBodyC.m_collisionDelegates[(int)((UIntPtr)typeB), 0](ucpCollisionPair, ucpCollisionPhase.Begin);
					}
					if (chipmunkBodyC2.m_collisionDelegates[(int)((UIntPtr)typeA), 0] != null)
					{
						chipmunkBodyC2.m_collisionDelegates[(int)((UIntPtr)typeA), 0](ChipmunkProS.ReverseCollisionPair(ucpCollisionPair), ucpCollisionPhase.Begin);
					}
				}
			}
		}
		int num2 = ChipmunkProWrapper.ucpGetPersistCollisionCount();
		if (num2 > 0)
		{
			ucpCollisionPair[] array2 = new ucpCollisionPair[num2];
			ChipmunkProWrapper.ucpGetPersistCollisions(array2, array2.Length);
			for (int j = 0; j < num2; j++)
			{
				ucpCollisionPair ucpCollisionPair2 = array2[j];
				ChipmunkBodyC chipmunkBodyC3 = ChipmunkProS.m_bodies.m_array[ucpCollisionPair2.ucpComponentIndexA];
				ChipmunkBodyC chipmunkBodyC4 = ChipmunkProS.m_bodies.m_array[ucpCollisionPair2.ucpComponentIndexB];
				uint typeA2 = ucpCollisionPair2.typeA;
				uint typeB2 = ucpCollisionPair2.typeB;
				if ((ulong)typeA2 <= (ulong)((long)ChipmunkProS.m_collisionTypeCount) && (ulong)typeB2 <= (ulong)((long)ChipmunkProS.m_collisionTypeCount))
				{
					if (ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeA2), (int)((UIntPtr)typeB2), 1] != null)
					{
						ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeA2), (int)((UIntPtr)typeB2), 1](ucpCollisionPair2, ucpCollisionPhase.Persist);
					}
					if (typeA2 != typeB2 && ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeB2), (int)((UIntPtr)typeA2), 1] != null)
					{
						ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeB2), (int)((UIntPtr)typeA2), 1](ucpCollisionPair2, ucpCollisionPhase.Persist);
					}
					if (chipmunkBodyC3.m_collisionDelegates[(int)((UIntPtr)typeB2), 1] != null)
					{
						chipmunkBodyC3.m_collisionDelegates[(int)((UIntPtr)typeB2), 1](ucpCollisionPair2, ucpCollisionPhase.Persist);
					}
					if (chipmunkBodyC4.m_collisionDelegates[(int)((UIntPtr)typeA2), 1] != null)
					{
						chipmunkBodyC4.m_collisionDelegates[(int)((UIntPtr)typeA2), 1](ChipmunkProS.ReverseCollisionPair(ucpCollisionPair2), ucpCollisionPhase.Persist);
					}
				}
			}
		}
		int num3 = ChipmunkProWrapper.ucpGetSeparateCollisionCount();
		if (num3 > 0)
		{
			ucpCollisionPair[] array3 = new ucpCollisionPair[num3];
			ChipmunkProWrapper.ucpGetSeparateCollisions(array3, array3.Length);
			for (int k = 0; k < num3; k++)
			{
				ucpCollisionPair ucpCollisionPair3 = array3[k];
				ChipmunkBodyC chipmunkBodyC5 = ChipmunkProS.m_bodies.m_array[ucpCollisionPair3.ucpComponentIndexA];
				ChipmunkBodyC chipmunkBodyC6 = ChipmunkProS.m_bodies.m_array[ucpCollisionPair3.ucpComponentIndexB];
				uint typeA3 = ucpCollisionPair3.typeA;
				uint typeB3 = ucpCollisionPair3.typeB;
				if ((ulong)typeA3 <= (ulong)((long)ChipmunkProS.m_collisionTypeCount) && (ulong)typeB3 <= (ulong)((long)ChipmunkProS.m_collisionTypeCount))
				{
					if (ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeA3), (int)((UIntPtr)typeB3), 2] != null)
					{
						ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeA3), (int)((UIntPtr)typeB3), 2](ucpCollisionPair3, ucpCollisionPhase.Separate);
					}
					if (typeA3 != typeB3 && ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeB3), (int)((UIntPtr)typeA3), 2] != null)
					{
						ChipmunkProS.m_globalCollisionDelegates[(int)((UIntPtr)typeB3), (int)((UIntPtr)typeA3), 2](ucpCollisionPair3, ucpCollisionPhase.Separate);
					}
					if (chipmunkBodyC5.m_collisionDelegates[(int)((UIntPtr)typeB3), 2] != null)
					{
						chipmunkBodyC5.m_collisionDelegates[(int)((UIntPtr)typeB3), 2](ucpCollisionPair3, ucpCollisionPhase.Separate);
					}
					if (chipmunkBodyC6.m_collisionDelegates[(int)((UIntPtr)typeA3), 2] != null)
					{
						chipmunkBodyC6.m_collisionDelegates[(int)((UIntPtr)typeA3), 2](ChipmunkProS.ReverseCollisionPair(ucpCollisionPair3), ucpCollisionPhase.Separate);
					}
				}
			}
		}
		return num + num3 + num2;
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x00198034 File Offset: 0x00196434
	public static void RemoveAllQueuedBodies()
	{
		if (ChipmunkProS.m_handleCollisionsRemoveBodyQueue.Count > 0)
		{
			foreach (ChipmunkBodyC chipmunkBodyC in ChipmunkProS.m_handleCollisionsRemoveBodyQueue)
			{
				ChipmunkProS.RemoveBody(chipmunkBodyC);
			}
			ChipmunkProS.m_handleCollisionsRemoveBodyQueue.Clear();
		}
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x001980A8 File Offset: 0x001964A8
	public static void Update(float _dt)
	{
		if (ChipmunkProS.CHIPMUNK_DEBUG_DRAW)
		{
			DebugDraw.Clear(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC);
		}
		ChipmunkProWrapper.ucpClearCollisionLists();
		ChipmunkProWrapper.ucpSpaceStep(_dt);
		ChipmunkProS.m_handleCollisionsRemoveBodyQueue.Clear();
		ChipmunkProS.m_isInsideHandleCollisions = true;
		ChipmunkProS.HandleCollisionEvents();
		ChipmunkProWrapper.ucpClearCollisionLists();
		ChipmunkProS.m_isInsideHandleCollisions = false;
		ChipmunkProS.RemoveAllQueuedBodies();
		int num = ChipmunkProS.m_bodies.m_aliveCount;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[ChipmunkProS.m_bodies.m_aliveIndices[i]];
			if (chipmunkBodyC.m_active)
			{
				if (ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body) != ucpBodyType.DYNAMIC)
				{
					bool flag = ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body) == ucpBodyType.KINEMATIC;
					if (flag || chipmunkBodyC.m_updateStaticVisuals)
					{
						bool flag2 = ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body) == ucpBodyType.STATIC;
						bool flag3 = false;
						bool flag4 = false;
						Vector3 vector = ChipmunkProWrapper.ucpBodyGetPos(chipmunkBodyC.body);
						float num3 = ChipmunkProWrapper.ucpBodyGetAngle(chipmunkBodyC.body);
						float num4 = ChipmunkProWrapper.ucpBodyGetScale(chipmunkBodyC.body);
						Vector2 vector2 = chipmunkBodyC.TC.transform.position - vector;
						float num5 = Mathf.DeltaAngle(chipmunkBodyC.TC.transform.rotation.eulerAngles.z, num3 * 57.29578f);
						float num6 = chipmunkBodyC.TC.transform.lossyScale.z - num4;
						if (num6 != 0f)
						{
							ChipmunkProWrapper.ucpBodySetScale(chipmunkBodyC.body, chipmunkBodyC.TC.transform.lossyScale.z);
						}
						if (Mathf.Abs(num5) > 0.0001f)
						{
							if (flag && _dt > 0f)
							{
								ChipmunkProWrapper.ucpBodySetAngVel(chipmunkBodyC.body, num5 * 0.017453292f / _dt);
							}
							flag4 = true;
						}
						if (vector2 != Vector2.zero)
						{
							if (flag && _dt > 0f)
							{
								ChipmunkProWrapper.ucpBodySetVel(chipmunkBodyC.body, -vector2 / _dt * Main.m_timeScale);
							}
							flag3 = true;
						}
						if (flag2)
						{
							if (flag3 || flag4)
							{
								Debug.LogWarning("Moving or rotating static body. This is expensive!");
								ChipmunkProWrapper.ucpSpaceReindexShapesForBody(chipmunkBodyC.body);
								TransformS.SetGlobalTransform(chipmunkBodyC.TC, vector, Vector3.forward * num3 * 57.29578f);
							}
						}
						else
						{
							if (flag3 || flag4)
							{
								ChipmunkProWrapper.ucpBodyUpdatePosition(chipmunkBodyC.body, _dt);
								TransformS.SetGlobalTransform(chipmunkBodyC.TC, vector, Vector3.forward * num3 * 57.29578f);
							}
							if (!flag3)
							{
								ChipmunkProWrapper.ucpBodySetVel(chipmunkBodyC.body, Vector2.zero);
							}
							if (!flag4)
							{
								ChipmunkProWrapper.ucpBodySetAngVel(chipmunkBodyC.body, 0f);
							}
						}
						chipmunkBodyC.m_updateStaticVisuals = false;
					}
				}
				else
				{
					if (!ChipmunkProWrapper.ucpBodyIsSleeping(chipmunkBodyC.body))
					{
						chipmunkBodyC.m_isSleeping = false;
						num2++;
						Vector3 vector = ChipmunkProWrapper.ucpBodyGetPos(chipmunkBodyC.body);
						vector.z = chipmunkBodyC.TC.transform.position.z;
						float num3 = ChipmunkProWrapper.ucpBodyGetAngle(chipmunkBodyC.body) * 57.29578f;
						float num4 = ChipmunkProWrapper.ucpBodyGetScale(chipmunkBodyC.body);
						float num7 = chipmunkBodyC.TC.transform.lossyScale.z - num4;
						if (num7 != 0f)
						{
							ChipmunkProWrapper.ucpBodySetScale(chipmunkBodyC.body, chipmunkBodyC.TC.transform.lossyScale.z);
							ChipmunkProWrapper.ucpSpaceReindexShapesForBody(chipmunkBodyC.body);
						}
						TransformS.SetGlobalTransform(chipmunkBodyC.TC, vector, Vector3.forward * num3);
					}
					else
					{
						chipmunkBodyC.m_isSleeping = true;
					}
					if (ChipmunkProS.CHIPMUNK_DEBUG_DRAW)
					{
						if (chipmunkBodyC.m_isSleeping)
						{
							DebugDraw.defaultColor = new Color(0f, 1f, 0f, 1f);
						}
						else
						{
							DebugDraw.defaultColor = new Color(1f, 0f, 0f, 1f);
						}
						for (int j = 0; j < chipmunkBodyC.shapes.Count; j++)
						{
							IntPtr shapePtr = chipmunkBodyC.shapes[j].shapePtr;
							cpBB cpBB = ChipmunkProWrapper.ucpShapeGetBB(shapePtr);
							float num8 = cpBB.r - cpBB.l;
							float num9 = cpBB.t - cpBB.b;
							Vector2 vector3;
							vector3..ctor(cpBB.l + num8 * 0.5f, cpBB.b + num9 * 0.5f);
							DebugDraw.CreateBox(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, vector3, num8, num9, false);
						}
					}
				}
			}
		}
		if (ChipmunkProS.CHIPMUNK_DEBUG_DRAW)
		{
			DebugDraw.defaultColor = new Color(0f, 0f, 1f, 1f);
			num = ChipmunkProS.m_constraints.m_aliveCount;
			for (int k = 0; k < num; k++)
			{
				ChipmunkConstraintC chipmunkConstraintC = ChipmunkProS.m_constraints.m_array[ChipmunkProS.m_constraints.m_aliveIndices[k]];
				if (chipmunkConstraintC.m_active)
				{
					if (chipmunkConstraintC.type == ucpConstraintType.PivotJoint)
					{
						TransformS.SetPosition(chipmunkConstraintC.anchor1, ChipmunkProWrapper.ucpPivotJointGetAnchr1(chipmunkConstraintC.constraint));
						DebugDraw.CreateBox(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, chipmunkConstraintC.anchor1.transform.position, 4f, 4f, false);
					}
					else if (chipmunkConstraintC.type != ucpConstraintType.PinJoint)
					{
						if (chipmunkConstraintC.type == ucpConstraintType.SlideJoint)
						{
							TransformS.SetPosition(chipmunkConstraintC.anchor1, ChipmunkProWrapper.ucpSlideJointGetAnchr1(chipmunkConstraintC.constraint));
							TransformS.SetPosition(chipmunkConstraintC.anchor2, ChipmunkProWrapper.ucpSlideJointGetAnchr2(chipmunkConstraintC.constraint));
							DebugDraw.CreateBox(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, chipmunkConstraintC.anchor1.transform.position, 4f, 4f, false);
							DebugDraw.CreateBox(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, chipmunkConstraintC.anchor2.transform.position, 4f, 4f, false);
							DebugDraw.CreateLine(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, chipmunkConstraintC.anchor1.transform.position, chipmunkConstraintC.anchor2.transform.position);
						}
						else if (chipmunkConstraintC.type == ucpConstraintType.DampedSpring)
						{
							TransformS.SetPosition(chipmunkConstraintC.anchor1, ChipmunkProWrapper.ucpDampedSpringGetAnchr1(chipmunkConstraintC.constraint));
							TransformS.SetPosition(chipmunkConstraintC.anchor2, ChipmunkProWrapper.ucpDampedSpringGetAnchr2(chipmunkConstraintC.constraint));
							DebugDraw.CreateBox(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, chipmunkConstraintC.anchor1.transform.position, 4f, 4f, false);
							DebugDraw.CreateBox(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, chipmunkConstraintC.anchor2.transform.position, 4f, 4f, false);
							DebugDraw.CreateLine(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, chipmunkConstraintC.anchor1.transform.position, chipmunkConstraintC.anchor2.transform.position);
						}
						else if (chipmunkConstraintC.type == ucpConstraintType.GrooveJoint)
						{
							TransformS.SetPosition(chipmunkConstraintC.anchor1, ChipmunkProWrapper.ucpGrooveJointGetAnchr2(chipmunkConstraintC.constraint));
							TransformS.SetPosition(chipmunkConstraintC.grooveA, ChipmunkProWrapper.ucpGrooveJointGetGrooveA(chipmunkConstraintC.constraint));
							TransformS.SetPosition(chipmunkConstraintC.grooveB, ChipmunkProWrapper.ucpGrooveJointGetGrooveB(chipmunkConstraintC.constraint));
							Vector3 vector4 = chipmunkConstraintC.grooveB.transform.localPosition - chipmunkConstraintC.grooveA.transform.localPosition;
							vector4 = ChipmunkProWrapper.ucpvperp(vector4).normalized;
							DebugDraw.CreateBox(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, chipmunkConstraintC.anchor1.transform.position, 4f, 4f, false);
							DebugDraw.CreateLine(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, chipmunkConstraintC.grooveA.transform.position - vector4 * 2f, chipmunkConstraintC.grooveB.transform.position - vector4 * 2f);
							DebugDraw.CreateLine(CameraS.m_mainCamera, ChipmunkProS.m_staticBodyTC, chipmunkConstraintC.grooveA.transform.position + vector4 * 2f, chipmunkConstraintC.grooveB.transform.position + vector4 * 2f);
						}
					}
				}
			}
			DebugDraw.defaultColor = new Color(1f, 1f, 1f, 1f);
		}
		ChipmunkProS.m_bodies.Update();
		ChipmunkProS.m_constraints.Update();
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x00198994 File Offset: 0x00196D94
	public static bool IsCrushing(ChipmunkBodyC _c, float _forceTolerance, float _crushTolerance = 0.8f)
	{
		float magnitude = ChipmunkProWrapper.ucpBodyGetForceVectorSum(_c.body).magnitude;
		float num = ChipmunkProWrapper.ucpBodyGetForceMagnitudeSum(_c.body);
		if (num > _forceTolerance)
		{
			float num2 = magnitude / num;
			if (!float.IsNaN(num2))
			{
				float num3 = 1f - num2;
				return num3 > _crushTolerance;
			}
		}
		return false;
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x001989E8 File Offset: 0x00196DE8
	public static Vector2[] GenerateCollisionVertexArrayFromGameObject(GameObject _gameObject, bool _zyOrientation = false, bool _flipX = false)
	{
		MeshFilter component = _gameObject.GetComponent<MeshFilter>();
		Mesh sharedMesh = component.sharedMesh;
		Vector3[] vertices = sharedMesh.vertices;
		Vector2[] array = new Vector2[vertices.Length];
		int num = 0;
		for (int i = 0; i < vertices.Length; i++)
		{
			if (!_zyOrientation)
			{
				array[i].x = vertices[i].x;
			}
			else
			{
				array[i].x = vertices[i].z;
			}
			if (_flipX)
			{
				Vector2[] array2 = array;
				int num2 = i;
				array2[num2].x = array2[num2].x * -1f;
			}
			array[i].y = vertices[i].y;
			if (Mathf.Abs(array[i].x) < 0.1f)
			{
				num++;
			}
		}
		array = ChipmunkProS.RemoveDuplicateVerts(array);
		if (num == vertices.Length)
		{
			Debug.LogError("Invalid vertex data. Wrong local orientation!");
			return null;
		}
		array = ChipmunkProS.SortVerticeArrayCCW(array);
		int num3 = ChipmunkProWrapper.ucpConvexHull(array.Length, array, 0f);
		Vector2[] array3 = new Vector2[num3];
		Array.Copy(array, array3, array3.Length);
		return array3;
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x00198B14 File Offset: 0x00196F14
	public static ucpPolyShape GeneratePolyShapeFromGameObject(GameObject _gameObject, Vector2 _offset, float _mass, float _elasticity, float _friction, ucpCollisionType _collisionType, uint _layers, bool _zyOrientation = false, bool _flipX = false)
	{
		Component component = _gameObject.GetComponent(typeof(MeshFilter));
		Mesh sharedMesh = (component as MeshFilter).sharedMesh;
		Vector3[] vertices = sharedMesh.vertices;
		Vector2[] array = new Vector2[vertices.Length];
		int num = 0;
		Vector2 zero = Vector2.zero;
		if (!_zyOrientation)
		{
			zero..ctor(component.gameObject.transform.localPosition.x, component.gameObject.transform.localPosition.y);
		}
		else
		{
			zero..ctor(component.gameObject.transform.localPosition.z, component.gameObject.transform.localPosition.y);
		}
		for (int i = 0; i < vertices.Length; i++)
		{
			if (!_zyOrientation)
			{
				array[i].x = vertices[i].x;
			}
			else
			{
				array[i].x = vertices[i].z;
			}
			if (_flipX)
			{
				Vector2[] array2 = array;
				int num2 = i;
				array2[num2].x = array2[num2].x * -1f;
			}
			array[i].y = vertices[i].y;
			if (Mathf.Abs(array[i].x) < 0.1f)
			{
				num++;
			}
		}
		array = ChipmunkProS.RemoveDuplicateVerts(array);
		if (num == vertices.Length)
		{
			Debug.LogError("Invalid vertex data. Wrong local orientation!");
			return null;
		}
		array = ChipmunkProS.SortVerticeArrayCCW(array);
		int num3 = ChipmunkProWrapper.ucpConvexHull(array.Length, array, 0f);
		Vector2[] array3 = new Vector2[num3];
		Array.Copy(array, array3, array3.Length);
		return new ucpPolyShape(array, _offset + zero, _layers, _mass, _elasticity, _friction, _collisionType, false)
		{
			tag = _gameObject.name
		};
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x00198CFC File Offset: 0x001970FC
	public static ucpPolyShape[] GeneratePolyShapesFromChildren(GameObject _parentGameObject, Vector2 _offset, float _totalMass, float _elasticity, float _friction, ucpCollisionType _collisionType, uint _layers, bool _zyOrientation = false, bool _flipX = false)
	{
		Component[] componentsInChildren = _parentGameObject.GetComponentsInChildren(typeof(MeshFilter), true);
		if (componentsInChildren.Length == 0)
		{
			return null;
		}
		ucpPolyShape[] array = new ucpPolyShape[componentsInChildren.Length];
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Mesh sharedMesh = (componentsInChildren[i] as MeshFilter).sharedMesh;
			Vector3[] vertices = sharedMesh.vertices;
			Vector2[] array2 = new Vector2[vertices.Length];
			Vector2 zero = Vector2.zero;
			if (!_zyOrientation)
			{
				zero..ctor(componentsInChildren[i].gameObject.transform.localPosition.x, componentsInChildren[i].gameObject.transform.localPosition.y);
			}
			else
			{
				zero..ctor(componentsInChildren[i].gameObject.transform.localPosition.z, componentsInChildren[i].gameObject.transform.localPosition.y);
			}
			int num = 0;
			for (int j = 0; j < vertices.Length; j++)
			{
				if (!_zyOrientation)
				{
					array2[j].x = vertices[j].x;
				}
				else
				{
					array2[j].x = vertices[j].z;
				}
				if (_flipX)
				{
					Vector2[] array3 = array2;
					int num2 = j;
					array3[num2].x = array3[num2].x * -1f;
				}
				array2[j].y = vertices[j].y;
				if (Mathf.Abs(array2[j].x) < 0.1f)
				{
					num++;
				}
			}
			array2 = ChipmunkProS.RemoveDuplicateVerts(array2);
			if (num == vertices.Length)
			{
				Debug.LogError("Invalid vertex data. Wrong local orientation!");
			}
			else
			{
				array2 = ChipmunkProS.SortVerticeArrayCCW(array2);
				int num3 = ChipmunkProWrapper.ucpConvexHull(array2.Length, array2, 0f);
				Vector2[] array4 = new Vector2[num3];
				Array.Copy(array2, array4, array4.Length);
				array[i] = new ucpPolyShape(array4, _offset + zero, _layers, 0f, _elasticity, _friction, _collisionType, false);
				array[i].tag = componentsInChildren[i].gameObject.name;
			}
		}
		float num4 = 0f;
		for (int k = 0; k < array.Length; k++)
		{
			num4 += array[k].area;
		}
		for (int l = 0; l < array.Length; l++)
		{
			array[l].mass = _totalMass * array[l].area / num4;
		}
		return array;
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x00198F98 File Offset: 0x00197398
	public static ChipmunkBodyShape GetBodyShapeByTag(ChipmunkBodyC _c, string _tag)
	{
		for (int i = 0; i < _c.shapes.Count; i++)
		{
			if (_c.shapes[i].tag != null && _c.shapes[i].tag == _tag)
			{
				return _c.shapes[i];
			}
		}
		return null;
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x00199004 File Offset: 0x00197404
	public static ChipmunkBodyC GetBodyByShape(IntPtr _shape)
	{
		if (_shape != IntPtr.Zero)
		{
			int num = ChipmunkProWrapper.ucpGetBodyComponentIndexByShape(_shape);
			if (num > -1)
			{
				return ChipmunkProS.m_bodies.m_array[num];
			}
		}
		return null;
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x00199040 File Offset: 0x00197440
	public static Vector2[] RemoveDuplicateVerts(Vector2[] _verts)
	{
		List<Vector2> list = new List<Vector2>();
		foreach (Vector2 vector in _verts)
		{
			if (!list.Contains(vector))
			{
				list.Add(vector);
			}
		}
		return list.ToArray();
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x00199090 File Offset: 0x00197490
	public static Vector2[] SortVerticeArrayCCW(Vector2[] _verts)
	{
		Vector2 center = Vector2.zero;
		for (int i = 0; i < _verts.Length; i++)
		{
			center += _verts[i];
		}
		center /= (float)_verts.Length;
		Array.Sort<Vector2>(_verts, (Vector2 v1, Vector2 v2) => (!ChipmunkProS.vertSortIsLess(v1, v2, center)) ? 1 : (-1));
		return _verts;
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x00199108 File Offset: 0x00197508
	private static bool vertSortIsLess(Vector2 a, Vector2 b, Vector2 center)
	{
		int num = (int)((a.x - center.x) * (b.y - center.y) - (b.x - center.x) * (a.y - center.y));
		if (num < 0)
		{
			return true;
		}
		if (num > 0)
		{
			return false;
		}
		int num2 = (int)((a.x - center.x) * (a.x - center.x) + (a.y - center.y) * (a.y - center.y));
		int num3 = (int)((b.x - center.x) * (b.x - center.x) + (b.y - center.y) * (b.y - center.y));
		return num2 < num3;
	}

	// Token: 0x04002AD6 RID: 10966
	public static bool CHIPMUNK_DEBUG_DRAW = false;

	// Token: 0x04002AD7 RID: 10967
	public static DynamicArray<ChipmunkBodyC> m_bodies;

	// Token: 0x04002AD8 RID: 10968
	public static DynamicArray<ChipmunkConstraintC> m_constraints;

	// Token: 0x04002AD9 RID: 10969
	private static int m_maxRemovedConstraintResultsCount = 50;

	// Token: 0x04002ADA RID: 10970
	private static ucpConstraintData[] m_removedConstraintResults = new ucpConstraintData[ChipmunkProS.m_maxRemovedConstraintResultsCount];

	// Token: 0x04002ADB RID: 10971
	private static CollisionDelegate[,,] m_globalCollisionDelegates;

	// Token: 0x04002ADC RID: 10972
	private static uint[,] m_collisionDelegatePhases;

	// Token: 0x04002ADD RID: 10973
	private static int[,] m_collisionDelegateCounts;

	// Token: 0x04002ADE RID: 10974
	public static int m_collisionTypeCount;

	// Token: 0x04002ADF RID: 10975
	private static Entity m_staticBodyEntity;

	// Token: 0x04002AE0 RID: 10976
	public static ChipmunkBodyC m_staticBody;

	// Token: 0x04002AE1 RID: 10977
	public static TransformC m_staticBodyTC;

	// Token: 0x04002AE2 RID: 10978
	public static bool m_isInsideHandleCollisions = false;

	// Token: 0x04002AE3 RID: 10979
	public static List<ChipmunkBodyC> m_handleCollisionsRemoveBodyQueue = new List<ChipmunkBodyC>();
}
