using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class PulsatingBlob : Gadget
{
	// Token: 0x06000277 RID: 631 RVA: 0x0001FF28 File Offset: 0x0001E328
	public PulsatingBlob(GraphElement _graphElement)
		: base(_graphElement)
	{
		base.SetInputOffset(new Vector3(0f, -16f, 15f));
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.BouncePlatformPrefab_GameObject);
		this.m_group = base.GetGroup();
		TransformC transformC = TransformS.AddComponent(this.m_entity, "BumperBottom");
		TransformS.SetTransform(transformC, _graphElement.m_position, _graphElement.m_rotation);
		GameObject gameObject2 = gameObject.transform.Find("BouncePlatformBody").gameObject;
		PrefabS.AddComponent(transformC, new Vector3(0f, -17f, 10f) + base.GetZBufferBias(), gameObject2);
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject.transform.Find("Collision2").gameObject, Vector2.zero, 1f, 0f, 0.9f, (ucpCollisionType)4, 257U, false, false);
		ucpPolyShape.group = this.m_group;
		this.m_bumperBottomBody = ChipmunkProS.AddStaticBody(transformC, ucpPolyShape, null);
		this.m_bumperBottomBody.customComponent = this.m_unitC;
		transformC = TransformS.AddComponent(this.m_entity, "BumperTop");
		TransformS.SetTransform(transformC, _graphElement.m_position, _graphElement.m_rotation);
		this.m_TC = transformC;
		gameObject2 = gameObject.transform.Find("BouncePlatform").gameObject;
		PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject2);
		ucpPolyShape ucpPolyShape2 = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject.transform.Find("Collision1").gameObject, Vector2.zero, 100f, 0.1f, 0.9f, (ucpCollisionType)4, 1118208U, false, false);
		ucpPolyShape2.group = this.m_group;
		this.m_bumperTopBody = ChipmunkProS.AddDynamicBody(transformC, ucpPolyShape2, null);
		this.m_bumperTopBody.customComponent = this.m_unitC;
		TransformC transformC2 = TransformS.AddComponent(this.m_entity, "Groove1", _graphElement.m_position + new Vector3(0f, 120f, 0f));
		TransformC transformC3 = TransformS.AddComponent(this.m_entity, "Groove2", _graphElement.m_position + new Vector3(0f, 0f, 0f));
		TransformC transformC4 = TransformS.AddComponent(this.m_entity, "GrooveAnchor", _graphElement.m_position + new Vector3(0f, 0f, 0f));
		ChipmunkProS.AddGrooveJoint(this.m_bumperBottomBody, this.m_bumperTopBody, transformC2, transformC3, transformC4);
		this.m_restLength = 20f;
		transformC2 = TransformS.AddComponent(this.m_entity, "LeftSpringAnchor1", _graphElement.m_position + new Vector3(-100f, -this.m_restLength));
		transformC3 = TransformS.AddComponent(this.m_entity, "LeftSpringAnchor2", _graphElement.m_position + new Vector3(-100f, 0f));
		this.m_spring1 = ChipmunkProS.AddDampedSpring(this.m_bumperBottomBody, this.m_bumperTopBody, transformC2, transformC3, this.m_restLength, 60000f, 500f);
		transformC2 = TransformS.AddComponent(this.m_entity, "RightSpringAnchor1", _graphElement.m_position + new Vector3(100f, -this.m_restLength));
		transformC3 = TransformS.AddComponent(this.m_entity, "RightSpringAnchor2", _graphElement.m_position + new Vector3(100f, 0f));
		this.m_spring2 = ChipmunkProS.AddDampedSpring(this.m_bumperBottomBody, this.m_bumperTopBody, transformC2, transformC3, this.m_restLength, 60000f, 500f);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_bumperTopBody, new CollisionDelegate(this.BlobCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, true, true, false);
			ChipmunkProS.AddCollisionHandler(this.m_bumperTopBody, new CollisionDelegate(this.BlobCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, true, false);
		}
		this.CreateEditorTouchArea(gameObject2, null);
		base.m_graphElement.m_isRotateable = true;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00020328 File Offset: 0x0001E728
	public override void Update()
	{
		base.Update();
		if (this.m_wasHit)
		{
			float num = 80f;
			this.m_restLength = ToolBox.limitBetween(this.m_restLength + this.m_accel, 20f, num);
			this.m_accel += 0.75f;
			if (this.m_restLength >= num)
			{
				this.m_wasHit = false;
			}
		}
		else
		{
			this.m_accel = 0f;
			float num2 = 80f;
			this.m_restLength -= 1.8f;
			this.m_restLength = ToolBox.limitBetween(this.m_restLength, 20f, num2);
			this.m_coolDownTimer -= Main.m_gameDeltaTime;
			if (this.m_coolDownTimer <= 0f)
			{
				this.m_onCooldown = false;
			}
		}
		ChipmunkProWrapper.ucpDampedSpringSetRestLength(this.m_spring1.constraint, this.m_restLength);
		ChipmunkProWrapper.ucpDampedSpringSetRestLength(this.m_spring2.constraint, this.m_restLength);
	}

	// Token: 0x06000279 RID: 633 RVA: 0x00020424 File Offset: 0x0001E824
	private void BlobCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		Vector3 vector = this.m_bumperTopBody.TC.transform.InverseTransformPoint(_pair.point);
		if ((_phase == ucpCollisionPhase.Begin || _phase == ucpCollisionPhase.Persist) && Mathf.Abs(vector.x) <= 70f)
		{
			this.Triggered();
		}
	}

	// Token: 0x0600027A RID: 634 RVA: 0x000204B0 File Offset: 0x0001E8B0
	public void Triggered()
	{
		if (!this.m_onCooldown && this.m_powerOn)
		{
			this.m_onCooldown = true;
			this.m_wasHit = true;
			this.m_coolDownTimer = 1f;
			SoundS.PlaySingleShot("/InGame/Units/BouncyPlatform", this.m_TC.transform.position, 1f);
		}
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0002050B File Offset: 0x0001E90B
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x040002AA RID: 682
	private const float MAX_DIST = 60f;

	// Token: 0x040002AB RID: 683
	private const float ACCEL_START = 0f;

	// Token: 0x040002AC RID: 684
	private const float ACCEL_INCREMENT = 0.75f;

	// Token: 0x040002AD RID: 685
	public uint m_group;

	// Token: 0x040002AE RID: 686
	public bool m_wasHit;

	// Token: 0x040002AF RID: 687
	public bool m_onCooldown;

	// Token: 0x040002B0 RID: 688
	public float m_accel;

	// Token: 0x040002B1 RID: 689
	private ChipmunkBodyC m_bumperBottomBody;

	// Token: 0x040002B2 RID: 690
	private ChipmunkBodyC m_bumperTopBody;

	// Token: 0x040002B3 RID: 691
	private ChipmunkConstraintC m_spring1;

	// Token: 0x040002B4 RID: 692
	private ChipmunkConstraintC m_spring2;

	// Token: 0x040002B5 RID: 693
	private float m_restLength;

	// Token: 0x040002B6 RID: 694
	private float m_coolDownTimer = 1f;

	// Token: 0x040002B7 RID: 695
	private TransformC m_TC;
}
