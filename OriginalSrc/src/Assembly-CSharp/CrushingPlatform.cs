using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class CrushingPlatform : MovingPlatformBase
{
	// Token: 0x060001FD RID: 509 RVA: 0x00018650 File Offset: 0x00016A50
	public CrushingPlatform(GraphElement _graphElement)
		: base(_graphElement, "CrushingPlatformPrefab")
	{
		base.SetInputOffset(new Vector3(0f, 0f, 60f));
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.PlatformCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, true, false, false);
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.PlatformCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, false, false);
			this.m_startPos = this.m_cmb.TC.transform.position;
			this.m_shakeStartRot = this.m_cmb.TC.transform.rotation.eulerAngles;
		}
		this.m_duration = 0.0007142857f * this.m_moveMagnitude;
		this.m_shaking = true;
		this.m_shakeMultiplier = 1f;
		this.m_shakeTicks = 80f;
		this.m_shakeAmount = 0.025f;
		this.m_shakeAdd = 0f;
		this.m_powerOffPosition = this.m_startPos;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00018760 File Offset: 0x00016B60
	public override void CreateObject()
	{
		GameObject gameObject = this.m_mainPrefab.transform.Find("Crusher").gameObject;
		this.m_platformPrefabC = PrefabS.AddComponent(this.m_platformTC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject);
		GameObject gameObject2 = this.m_mainPrefab.transform.Find("Rail").gameObject;
		PrefabS.AddComponent(this.m_pipeTC, new Vector3(0f, 0f, 60f) + base.GetZBufferBias(), gameObject2);
		this.m_pipeTC.transform.localScale = new Vector3((this.m_moveMagnitude + 100f) / 100f, 1f, 1f);
		GameObject gameObject3 = this.m_mainPrefab.transform.Find("RailNode").gameObject;
		PrefabS.AddComponent(this.m_startTC, new Vector3(0f, 0f, 60f) + base.GetZBufferBias(), gameObject3);
		this.m_startTC.transform.localScale = new Vector3(-1f, 1f, 1f);
		GameObject gameObject4 = this.m_mainPrefab.transform.Find("RailNode").gameObject;
		PrefabS.AddComponent(this.m_endTC, new Vector3(0f, 0f, 60f) + base.GetZBufferBias(), gameObject4);
		this.m_stopperTC = TransformS.AddComponent(this.m_entity, "Stopper");
		TransformS.SetTransform(this.m_stopperTC, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
		GameObject gameObject5 = this.m_mainPrefab.transform.Find("CrusherPlatform").gameObject;
		PrefabS.AddComponent(this.m_stopperTC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject5);
		TransformS.SetGlobalTransform(this.m_stopperTC, base.m_graphElement.m_position + this.m_moveDir * (this.m_moveMagnitude + 25f), base.m_graphElement.m_TC.transform.rotation.eulerAngles);
		this.m_powerOffAngle = base.m_graphElement.m_rotation.z * 0.017453292f;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x000189D4 File Offset: 0x00016DD4
	public override void CreateColliders()
	{
		ucpPolyShape[] array = new ucpPolyShape[2];
		array[0] = ChipmunkProS.GeneratePolyShapeFromGameObject(this.m_mainPrefab.transform.Find("Collision1").gameObject, Vector2.zero, 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, false);
		array[0].group = base.GetGroup();
		array[1] = ChipmunkProS.GeneratePolyShapeFromGameObject(this.m_mainPrefab.transform.Find("Collision2").gameObject, Vector2.zero, 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, false);
		array[1].group = base.GetGroup();
		this.m_cmb = ChipmunkProS.AddKinematicBody(this.m_platformTC, array, this.m_unitC);
		this.m_spikeShape = array[0].shapePtr;
		ucpPolyShape[] array2 = new ucpPolyShape[]
		{
			ChipmunkProS.GeneratePolyShapeFromGameObject(this.m_mainPrefab.transform.Find("Collision3").gameObject, Vector2.up * 56f, 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, false),
			ChipmunkProS.GeneratePolyShapeFromGameObject(this.m_mainPrefab.transform.Find("Collision4").gameObject, Vector2.up * 56f, 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, false)
		};
		this.m_stopperCmb = ChipmunkProS.AddStaticBody(this.m_stopperTC, array2, this.m_unitC);
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00018B58 File Offset: 0x00016F58
	public override GraphNode CreateChild()
	{
		this.m_childNode = new GraphNode(GraphNodeType.Child, typeof(TouchableChildNode), "ChildNode", base.m_graphElement.m_position - Vector2.up * 100f, Vector3.zero, Vector3.one);
		this.m_childNode.m_isRemovable = false;
		this.m_childNode.m_isCopyable = false;
		this.m_childNode.m_minDistanceFromParent = 100f;
		this.m_childNode.m_maxDistanceFromParent = 1000f;
		return this.m_childNode;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00018BF1 File Offset: 0x00016FF1
	public override void CreateGhostObject()
	{
		PrefabS.AddComponent(this.m_ghostPlatformTC, Vector3.forward * 10f, this.m_mainPrefab.transform.Find("Crusher").gameObject);
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00018C28 File Offset: 0x00017028
	public override void EditUpdate()
	{
		base.EditUpdate();
		if (this.m_minigame.m_editing)
		{
			TransformS.SetGlobalRotation(this.m_platformTC, base.m_graphElement.m_rotation);
			TransformS.SetGlobalTransform(this.m_stopperTC, base.m_graphElement.m_position + this.m_moveDir * (this.m_moveMagnitude + 25f), base.m_graphElement.m_TC.transform.rotation.eulerAngles);
		}
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00018CB5 File Offset: 0x000170B5
	public override void PowerStateChange(bool _state)
	{
		base.PowerStateChange(_state);
		if (!_state)
		{
			this.m_powerOffPosition = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
			this.m_powerOffAngle = ChipmunkProWrapper.ucpBodyGetAngle(this.m_cmb.body);
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00018CF0 File Offset: 0x000170F0
	public override void Update()
	{
		base.m_graphElement.m_rotation = this.m_pipeTC.transform.rotation.eulerAngles + new Vector3(0f, 0f, 90f);
		TransformS.SetGlobalTransform(base.m_graphElement.m_TC, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
		base.Update();
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted)
		{
			if (!this.m_powerOn)
			{
				ChipmunkProWrapper.ucpBodySetPos(this.m_cmb.body, this.m_powerOffPosition);
				ChipmunkProWrapper.ucpBodySetAngle(this.m_cmb.body, this.m_powerOffAngle);
				return;
			}
			if (this.m_shaking)
			{
				if (this.m_shakeTicks == 80f)
				{
					SoundS.PlaySingleShot("/Ingame/Units/CrusherTremble", this.m_cmb.TC.transform.position, 1f);
				}
				ChipmunkProWrapper.ucpBodySetAngle(this.m_cmb.body, (this.m_shakeStartRot.z + this.m_shakeAdd * this.m_shakeMultiplier) * 0.017453292f);
				this.m_shakeAdd += 0.05f * Main.m_timeScale;
				if (this.m_shakeAdd >= 2f)
				{
					this.m_shakeAdd = 2f;
				}
				this.m_shakeAmount += this.m_shakeAdd;
				this.m_shakeTicks -= 1f * Main.m_timeScale;
				if (this.m_shakeTicks % 4f == 0f)
				{
					this.m_shakeMultiplier *= -1f;
				}
				if (this.m_shakeTicks <= 0f)
				{
					this.m_shaking = false;
					ChipmunkProWrapper.ucpBodySetAngle(this.m_cmb.body, base.m_graphElement.m_rotation.z * 0.017453292f);
					TransformS.SetGlobalRotation(this.m_cmb.TC, base.m_graphElement.m_rotation);
				}
			}
			else
			{
				this.m_shakeTicks -= 1f;
				if (this.m_currentTime == -1f && this.m_shakeTicks <= 0f)
				{
					this.m_startPos = this.m_cmb.TC.transform.position;
					this.m_currentTime = 0f;
				}
				else if (this.m_currentValue == this.m_moveMagnitude * this.m_rotMultiplier)
				{
					this.m_currentTime = -1f;
					this.m_rotMultiplier *= -1f;
					if (this.m_rotMultiplier == 1f)
					{
						this.m_duration = 0.0007142857f * this.m_moveMagnitude;
						this.m_shaking = true;
						this.m_shakeTicks = 80f;
						this.m_shakeStartRot = this.m_cmb.TC.transform.rotation.eulerAngles;
						this.m_shakeAdd = 0f;
						this.m_shakeAmount = Random.Range(0.025f, 0.035f);
					}
					else
					{
						this.m_duration = 0.0092857145f * this.m_moveMagnitude;
						this.m_shakeTicks = 30f;
						SoundS.PlaySingleShot("/Ingame/Units/CrusherCrush", this.m_cmb.TC.transform.position, 1f);
					}
				}
				if (this.m_currentTime != -1f)
				{
					this.m_currentTime += Main.m_gameDeltaTime;
					if (this.m_currentTime >= this.m_duration)
					{
						this.m_currentTime = this.m_duration;
					}
					this.m_currentValue = TweenS.tween((this.m_rotMultiplier <= 0f) ? TweenStyle.QuadInOut : TweenStyle.QuadIn, this.m_currentTime, this.m_duration, 0f, this.m_moveMagnitude * this.m_rotMultiplier);
				}
				ChipmunkProWrapper.ucpBodySetPos(this.m_cmb.body, this.m_startPos + this.m_moveDir * this.m_currentValue);
				TransformS.SetGlobalRotation(this.m_gearTC, this.m_gearTC.transform.rotation.eulerAngles + new Vector3(0f, 0f, ChipmunkProWrapper.ucpBodyGetVel(this.m_cmb.body).magnitude * Main.m_gameDeltaTime) * this.m_rotMultiplier);
			}
		}
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00019170 File Offset: 0x00017570
	private void PlatformCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin && _pair.shapeA == this.m_spikeShape)
		{
			unitC.m_unit.EmergencyKill();
		}
	}

	// Token: 0x040001E5 RID: 485
	private const int SHAKE_TIME = 80;

	// Token: 0x040001E6 RID: 486
	private bool m_shaking;

	// Token: 0x040001E7 RID: 487
	private float m_shakeMultiplier;

	// Token: 0x040001E8 RID: 488
	private Vector3 m_shakeStartRot;

	// Token: 0x040001E9 RID: 489
	private float m_shakeTicks;

	// Token: 0x040001EA RID: 490
	private float m_shakeAmount;

	// Token: 0x040001EB RID: 491
	private float m_shakeAdd;

	// Token: 0x040001EC RID: 492
	private TransformC m_stopperTC;

	// Token: 0x040001ED RID: 493
	private ChipmunkBodyC m_stopperCmb;

	// Token: 0x040001EE RID: 494
	private IntPtr m_spikeShape;

	// Token: 0x040001EF RID: 495
	private Vector2 m_powerOffPosition;

	// Token: 0x040001F0 RID: 496
	private float m_powerOffAngle;
}
