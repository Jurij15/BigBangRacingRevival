using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
internal class MetalBallGoal : GoalRepresentation
{
	// Token: 0x060001D1 RID: 465 RVA: 0x0001599E File Offset: 0x00013D9E
	public MetalBallGoal(Goal _parent)
		: base(_parent, true)
	{
		base.m_radius = 46.5f;
		this.m_impactSound = "/Ingame/Units/GoalBall_Metal_Impact";
		this.m_maxForce = 200000;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x000159C9 File Offset: 0x00013DC9
	public override ResourcePool.Asset GetAsset()
	{
		return RESOURCE.GoalBotMetalPrefab_GameObject;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x000159D0 File Offset: 0x00013DD0
	public override void Update()
	{
		TransformC playerTC = this.m_parent.m_minigame.m_playerTC;
		if (playerTC != null && this.m_goalReached)
		{
			base.m_tc.transform.eulerAngles = Vector3.zero;
		}
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00015A14 File Offset: 0x00013E14
	public override PrefabC CreatePrefab()
	{
		PrefabC prefabC = base.CreatePrefab();
		prefabC.p_gameObject.transform.Rotate(new Vector3(-15f, 0f, 0f));
		return prefabC;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00015A50 File Offset: 0x00013E50
	public override void CreateBody(out ucpShape _shape, out ChipmunkBodyC _body)
	{
		PsMetrics.GoalBallStateChanged("default");
		float num = 500f;
		float num2 = 0f;
		float num3 = 0.9f;
		ucpCollisionType ucpCollisionType = (ucpCollisionType)4;
		ucpShape ucpShape;
		_shape = (ucpShape = new ucpCircleShape(base.m_radius, Vector2.zero, 17895696U, num, num2, num3, ucpCollisionType, false));
		this.m_shape = ucpShape;
		ChipmunkBodyC chipmunkBodyC;
		_body = (chipmunkBodyC = ChipmunkProS.AddDynamicBody(base.m_tc, _shape, this.m_parent.m_unitC));
		this.m_body = chipmunkBodyC;
		ChipmunkProS.SetBodyLinearDamp(_body, new Vector2(0.9975f, 0.9975f));
		_body.m_identifier = 2;
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00015AE8 File Offset: 0x00013EE8
	public override void OnCollision()
	{
		ChipmunkProS.RemoveBody(this.m_body);
		this.m_body = ChipmunkProS.AddStaticBody(base.m_tc, this.m_shape, null);
		base.m_affectedByGravity = false;
		Transform transform = this.m_prefabC.p_gameObject.transform;
		Transform transform2 = transform.Find("GoalBotMetalBody/BallSparkLocator");
		this.LaunchParticles(RESOURCE.MetalBallSpark_GameObject, transform2, true);
		base.OnCollision();
	}

	// Token: 0x040001A1 RID: 417
	private ChipmunkBodyC m_body;

	// Token: 0x040001A2 RID: 418
	private ucpShape m_shape;
}
