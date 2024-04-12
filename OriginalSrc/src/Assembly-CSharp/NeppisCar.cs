using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
public class NeppisCar : Vehicle
{
	// Token: 0x060002EA RID: 746 RVA: 0x0002E098 File Offset: 0x0002C498
	public NeppisCar(GraphElement _graphElement)
		: base(_graphElement)
	{
		if (NeppisCar.m_mainPrefab == null)
		{
			NeppisCar.m_mainPrefab = ResourceManager.GetGameObject("Units/NeppisCarPrefab");
		}
		base.m_graphElement.m_isCopyable = false;
		base.m_graphElement.m_isRemovable = false;
		base.m_graphElement.m_isRotateable = false;
		_graphElement.m_width = 70f;
		_graphElement.m_height = 25f;
		this.m_group = (uint)(this.m_entity.m_index + 1000);
		this.m_chassisTC = TransformS.AddComponent(this.m_entity, _graphElement.m_name, _graphElement.m_position, _graphElement.m_rotation);
		TransformS.SetTransform(this.m_chassisTC, _graphElement.m_position - this.m_centerOfGravityOffset, _graphElement.m_rotation);
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(NeppisCar.m_mainPrefab.transform.Find("Collision").gameObject, this.m_centerOfGravityOffset, 100f, 0.2f, 0.5f, (ucpCollisionType)3, 17895696U, false, false);
		ucpPolyShape.group = this.m_group;
		this.m_chassisBody = ChipmunkProS.AddDynamicBody(this.m_chassisTC, ucpPolyShape, null);
		this.m_chassisBody.customComponent = this.m_unitC;
		PrefabS.AddComponent(this.m_chassisTC, this.m_centerOfGravityOffset, NeppisCar.m_mainPrefab.transform.Find("NeppisBody").gameObject);
		ChipmunkProS.SetBodyLinearDamp(this.m_chassisBody, new Vector2(0.997f, 0.997f));
		ChipmunkProS.SetBodyAngularDamp(this.m_chassisBody, 0.97f);
		this.m_alienHeadTC = TransformS.AddComponent(this.m_entity, "Alien head");
		Vector3 vector = this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector3(0f, 5f, 0f);
		TransformS.SetTransform(this.m_alienHeadTC, vector, _graphElement.m_rotation);
		Vector2 vector2;
		vector2..ctor(0f, 12f);
		ucpCircleShape ucpCircleShape = new ucpCircleShape(16f, vector2, 17895696U, 6f, 0.2f, 0.5f, (ucpCollisionType)3, false);
		ucpCircleShape.group = this.m_group;
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddDynamicBody(this.m_alienHeadTC, ucpCircleShape, null);
		chipmunkBodyC.customComponent = this.m_unitC;
		PrefabS.AddComponent(this.m_alienHeadTC, Vector2.zero, NeppisCar.m_mainPrefab.transform.Find("NeppisChar").gameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, "AlienJoint1", vector + new Vector3(0f, -7f, 0f));
		ChipmunkProS.AddPivotJoint(this.m_chassisBody, chipmunkBodyC, transformC);
		ChipmunkProS.AddRotaryLimitJoint(this.m_chassisBody, chipmunkBodyC, transformC, -0.17453292f, 0.17453292f);
		transformC = TransformS.AddComponent(this.m_entity, "AlienSpring1", vector);
		TransformC transformC2 = TransformS.AddComponent(this.m_entity, "AlienSpring2", vector);
		ChipmunkProS.AddDampedSpring(this.m_chassisBody, chipmunkBodyC, transformC, transformC2, 0f, 2000f, 100f);
		float num = 14f;
		float num2 = 18f;
		Vector2 vector3 = this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(_graphElement.m_width * 0.55f, -_graphElement.m_height * 0.48f - num * 0.5f);
		this.m_frontWheelBody = this.createTire(this.m_entity, this.m_chassisBody, vector3, num, 5f, true);
		this.m_frontWheelBody.customComponent = this.m_unitC;
		transformC = TransformS.AddComponent(this.m_entity, "FrontWheelJoint1", this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(0f, -_graphElement.m_height * 0.5f));
		transformC2 = TransformS.AddComponent(this.m_entity, "FrontWheelJoint2", vector3);
		float magnitude = (transformC.transform.position - transformC2.transform.position).magnitude;
		ChipmunkProS.AddSlideJoint(this.m_chassisBody, this.m_frontWheelBody, transformC, transformC2, magnitude, magnitude);
		transformC = TransformS.AddComponent(this.m_entity, "FrontWheelSpringAnchor1", vector3);
		transformC2 = TransformS.AddComponent(this.m_entity, "FrontWheelSpringAnchor2", vector3);
		ChipmunkProS.AddDampedSpring(this.m_chassisBody, this.m_frontWheelBody, transformC, transformC2, 0f, 35000f, 600f);
		Vector2 vector4 = this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(-_graphElement.m_width * 0.35f, -_graphElement.m_height * 0.34f - num2 * 0.5f);
		this.m_rearWheelBody = this.createTire(this.m_entity, this.m_chassisBody, vector4, num2, 5f, false);
		this.m_rearWheelBody.customComponent = this.m_unitC;
		transformC = TransformS.AddComponent(this.m_entity, "RearWheelJoint1", this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(0f, -_graphElement.m_height * 0.5f));
		transformC2 = TransformS.AddComponent(this.m_entity, "RearWheelJoint2", vector4);
		ChipmunkProS.AddPinJoint(this.m_chassisBody, this.m_rearWheelBody, transformC, transformC2);
		transformC = TransformS.AddComponent(this.m_entity, "RearWheelSpringAnchor1", vector4);
		transformC2 = TransformS.AddComponent(this.m_entity, "RearWheelSpringAnchor2", vector4);
		ChipmunkProS.AddDampedSpring(this.m_chassisBody, this.m_rearWheelBody, transformC, transformC2, 0f, 35000f, 600f);
		this.m_assBallTC = TransformS.AddComponent(this.m_entity, "Ass Ball");
		this.m_assBallIdlePos = this.m_centerOfGravityOffset + new Vector2(-48f, 0f);
		this.m_assBallDrivePos = this.m_centerOfGravityOffset + new Vector2(-35f, 0f);
		PrefabS.AddComponent(this.m_assBallTC, Vector2.zero, NeppisCar.m_mainPrefab.transform.Find("AssBall").gameObject);
		TransformS.ParentComponent(this.m_assBallTC, this.m_chassisTC);
		TransformS.SetPosition(this.m_assBallTC, this.m_assBallIdlePos);
		this.m_assBallRubberTC = TransformS.AddComponent(this.m_entity, "Ass Ball Rubber");
		PrefabS.AddComponent(this.m_assBallRubberTC, Vector2.zero, NeppisCar.m_mainPrefab.transform.Find("AssBallRubber").gameObject);
		TransformS.SetPosition(this.m_assBallRubberTC, this.m_chassisTC.transform.position);
		TransformS.ParentComponent(this.m_assBallRubberTC, this.m_chassisTC);
		TransformS.SetPosition(this.m_assBallRubberTC, this.m_assBallIdlePos);
		this.InitMotors(this.m_rearWheelBody, this.m_frontWheelBody);
		if (!this.m_minigame.m_editing)
		{
			this.CreateCamera();
			this.m_minigame.SetPlayer(this, this.m_chassisTC, typeof(UnitTouchController));
			TouchAreaC touchAreaC = TouchAreaS.AddCircleArea(this.m_chassisTC, "Sling", 100f, CameraS.m_mainCamera, null);
			TouchAreaS.AddTouchEventListener(touchAreaC, new TouchEventDelegate(this.TouchHandler));
		}
		ProjectorS.AddComponent(this.m_chassisTC, ResourceManager.GetMaterial(RESOURCE.ShadowMaterial_Material), 256, new Vector3(5f, 40f));
		this.CreateEditorTouchArea(_graphElement.m_width, _graphElement.m_height, null, default(Vector2));
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.m_stopped = true;
		this.SetHandBrake(true, 1000000f);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0002E8B7 File Offset: 0x0002CCB7
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Weapon, 20f);
		this.SetBaseArmor(DamageType.Electric, 20f);
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0002E8D7 File Offset: 0x0002CCD7
	public void CreateCamera()
	{
	}

	// Token: 0x060002ED RID: 749 RVA: 0x0002E8DC File Offset: 0x0002CCDC
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing)
		{
			this.m_ticksOnGround++;
			if (!this.m_isDead)
			{
				bool flag = this.m_contactState == ContactState.OnAir;
				if (!flag)
				{
					this.m_ticksOnGround++;
				}
				else
				{
					this.m_ticksOnGround = 0;
				}
				float num = ChipmunkProWrapper.ucpBodyGetAngVel(this.m_frontWheelBody.body);
				float num2 = ChipmunkProWrapper.ucpBodyGetAngVel(this.m_rearWheelBody.body);
				float num3 = Mathf.Abs((num + num2) * 0.5f);
				if (num3 < 5f && this.m_ticksOnGround > 60 && !this.m_stopped && !flag)
				{
					this.m_minigame.m_gameTicksFreezed = true;
					this.SetHandBrake(true, 1000000f);
					TransformS.SetGlobalPosition(this.m_assBallTC, this.m_chassisTC.transform.position);
					TransformS.SetPosition(this.m_assBallTC, this.m_assBallIdlePos);
					this.m_stopped = true;
				}
				float num4 = 0f;
				float z = this.m_chassisBody.TC.transform.rotation.eulerAngles.z;
				float num5 = ChipmunkProWrapper.ucpBodyGetAngularDamp(this.m_frontWheelBody.body);
				float num6 = 0.997f;
				if (flag)
				{
					float num7 = this.m_chassisBody.TC.transform.rotation.eulerAngles.z * 0.017453292f;
					Vector2 vector;
					vector..ctor(Mathf.Sin(num7), -Mathf.Cos(num7));
					Vector2 normalized = vector.normalized;
					float num8 = ToolBox.limitBetween(Mathf.DeltaAngle(z, num4), -90f, 90f);
				}
				else
				{
					num6 = 0.3f;
				}
				this.m_tireAngularDamp += (num6 - num5) * 0.02f;
			}
		}
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0002EAD0 File Offset: 0x0002CED0
	private void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchIsSecondary)
		{
			return;
		}
		if (_touchPhase == TouchAreaPhase.Began && this.m_stopped)
		{
			this.m_dragStartedAfterStop = true;
		}
		if (this.m_dragStartedAfterStop)
		{
			Vector2 vector = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, _touches[0].m_currentPosition, 0f);
			float num = ChipmunkProWrapper.ucpBodyGetAngle(this.m_chassisBody.body);
			TransformS.SetGlobalPosition(this.m_assBallRubberTC, this.m_chassisTC.transform.position);
			TransformS.SetPosition(this.m_assBallRubberTC, this.m_assBallDrivePos);
			Vector2 vector2 = this.m_assBallRubberTC.transform.position;
			Vector2 vector3 = vector - vector2;
			float angleFromVector = ToolBox.getAngleFromVector2(-vector3.normalized);
			Vector2 vector4;
			vector4..ctor(Mathf.Cos(num), Mathf.Sin(num));
			float num2 = Vector2.Dot(vector4, vector3.normalized);
			float num3 = vector3.magnitude * num2;
			if (num3 > 0f)
			{
				num3 = 0f;
			}
			else if (num3 < -150f)
			{
				num3 = -150f;
			}
			Vector2 vector5 = vector2 + vector4 * num3;
			if (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut)
			{
				TransformS.SetGlobalPosition(this.m_assBallTC, vector5);
				TransformS.SetScale(this.m_assBallRubberTC, new Vector3(-num3 * 0.095f, 1.6f, 1f));
				TransformS.SetGlobalRotation(this.m_assBallRubberTC, new Vector3(0f, 0f, num * 57.29578f));
			}
			else if (_touchPhase == TouchAreaPhase.ReleaseIn || _touchPhase == TouchAreaPhase.ReleaseOut)
			{
				this.m_ticksOnGround = 0;
				this.m_minigame.m_gameTicksFreezed = false;
				this.m_tireAngularDamp = 0.997f;
				this.SetHandBrake(false, 1000000f);
				TransformS.SetGlobalPosition(this.m_assBallTC, this.m_chassisTC.transform.position);
				TransformS.SetPosition(this.m_assBallTC, this.m_assBallDrivePos);
				TransformS.SetScale(this.m_assBallRubberTC, new Vector3(0f, 1.6f, 1f));
				this.m_dragStartedAfterStop = false;
				this.m_stopped = false;
			}
		}
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0002ED0C File Offset: 0x0002D10C
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0002ED14 File Offset: 0x0002D114
	public ChipmunkBodyC createTire(Entity _e, ChipmunkBodyC _chassis, Vector2 _pos, float _rad, float _weight, bool _front)
	{
		TransformC transformC = TransformS.AddComponent(_e, "Car Tire");
		TransformS.SetTransform(transformC, _pos, Vector2.zero);
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, new ucpCircleShape(_rad, Vector2.zero, 17895696U, _weight, 0.2f, 1.5f, (ucpCollisionType)3, false)
		{
			group = this.m_group
		}, null);
		if (_front)
		{
			PrefabS.AddComponent(transformC, new Vector3(0f, 0f, -18f), NeppisCar.m_mainPrefab.transform.Find("FrontWheel").gameObject);
			PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 18f), NeppisCar.m_mainPrefab.transform.Find("FrontWheel2").gameObject);
		}
		else
		{
			PrefabS.AddComponent(transformC, new Vector3(0f, 0f, -25f), NeppisCar.m_mainPrefab.transform.Find("BackWheel").gameObject);
			PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 25f), NeppisCar.m_mainPrefab.transform.Find("BackWheel2").gameObject);
		}
		ChipmunkProS.SetBodyLinearDamp(chipmunkBodyC, new Vector2(0.997f, 0.997f));
		ChipmunkProS.SetBodyAngularDamp(chipmunkBodyC, 0.997f);
		return chipmunkBodyC;
	}

	// Token: 0x040003B9 RID: 953
	private const float TIRE_FRICTION = 1.5f;

	// Token: 0x040003BA RID: 954
	private const float TIRE_ELASTICITY = 0.2f;

	// Token: 0x040003BB RID: 955
	private const float TIRE_ANGULAR_DAMP = 0.997f;

	// Token: 0x040003BC RID: 956
	private const float TIRE_ANGULAR_DAMP_ONGROUND = 0.3f;

	// Token: 0x040003BD RID: 957
	private const float FRONT_WHEEL_RAD = 14f;

	// Token: 0x040003BE RID: 958
	private const float FRONT_WHEEL_WEIGHT = 5f;

	// Token: 0x040003BF RID: 959
	private const float REAR_WHEEL_RAD = 18f;

	// Token: 0x040003C0 RID: 960
	private const float REAR_WHEEL_WEIGHT = 5f;

	// Token: 0x040003C1 RID: 961
	private const float CHASSIS_WEIGHT = 100f;

	// Token: 0x040003C2 RID: 962
	private const float FRONT_SPRING_FORCE = 35000f;

	// Token: 0x040003C3 RID: 963
	private const float FRONT_SPRING_DAMP = 600f;

	// Token: 0x040003C4 RID: 964
	private const float REAR_SPRING_FORCE = 35000f;

	// Token: 0x040003C5 RID: 965
	private const float REAR_SPRING_DAMP = 600f;

	// Token: 0x040003C6 RID: 966
	private const float CHASSIS_ANGULAR_DAMP = 0.97f;

	// Token: 0x040003C7 RID: 967
	private const float CAR_LINEAR_DAMP = 0.997f;

	// Token: 0x040003C8 RID: 968
	private const float SELF_BALANCE_FORCE = 70f;

	// Token: 0x040003C9 RID: 969
	private static GameObject m_mainPrefab;

	// Token: 0x040003CA RID: 970
	public uint m_group;

	// Token: 0x040003CB RID: 971
	public ChipmunkBodyC m_frontWheelBody;

	// Token: 0x040003CC RID: 972
	public ChipmunkBodyC m_rearWheelBody;

	// Token: 0x040003CD RID: 973
	public new ChipmunkBodyC m_chassisBody;

	// Token: 0x040003CE RID: 974
	public TransformC m_chassisTC;

	// Token: 0x040003CF RID: 975
	public TransformC m_alienHeadTC;

	// Token: 0x040003D0 RID: 976
	public TransformC m_assBallTC;

	// Token: 0x040003D1 RID: 977
	public TransformC m_assBallRubberTC;

	// Token: 0x040003D2 RID: 978
	public float m_tireAngularDamp;

	// Token: 0x040003D3 RID: 979
	public int m_ticksOnGround;

	// Token: 0x040003D4 RID: 980
	private Vector2 m_centerOfGravityOffset = new Vector2(-5f, 15f);

	// Token: 0x040003D5 RID: 981
	private Vector2 m_assBallIdlePos;

	// Token: 0x040003D6 RID: 982
	private Vector2 m_assBallDrivePos;

	// Token: 0x040003D7 RID: 983
	private bool m_stopped;

	// Token: 0x040003D8 RID: 984
	private bool m_dragStartedAfterStop;
}
