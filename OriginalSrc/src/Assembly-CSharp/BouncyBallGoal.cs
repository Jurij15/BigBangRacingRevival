using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
internal class BouncyBallGoal : GoalRepresentation
{
	// Token: 0x060001B9 RID: 441 RVA: 0x0001546F File Offset: 0x0001386F
	public BouncyBallGoal(Goal _parent)
		: base(_parent, true)
	{
		this.m_impactSound = "/Ingame/Units/GoalBall_Bouncy_Impact";
		this.m_maxForce = 900000;
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0001548F File Offset: 0x0001388F
	public override ResourcePool.Asset GetAsset()
	{
		return RESOURCE.GoalBotBouncyPrefab_GameObject;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00015498 File Offset: 0x00013898
	public override void Update()
	{
		TransformC playerTC = this.m_parent.m_minigame.m_playerTC;
		if (playerTC != null && this.m_goalReached)
		{
			float x = this.m_rotationTween.currentValue.x;
			base.m_tc.transform.eulerAngles = new Vector3(0f, x, 15f);
			float x2 = this.m_hoverTween.currentValue.x;
			this.m_prefabC.p_gameObject.transform.localPosition = new Vector3(0f, x2, 0f);
		}
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00015530 File Offset: 0x00013930
	public override PrefabC CreatePrefab()
	{
		PrefabC prefabC = base.CreatePrefab();
		prefabC.p_gameObject.transform.Rotate(new Vector3(-15f, 0f, 0f));
		return prefabC;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0001556C File Offset: 0x0001396C
	public override void CreateBody(out ucpShape _shape, out ChipmunkBodyC _body)
	{
		PsMetrics.GoalBallStateChanged("default");
		float num = 500f;
		float num2 = 1.5f;
		float num3 = 0.9f;
		ucpCollisionType ucpCollisionType = (ucpCollisionType)4;
		_shape = new ucpCircleShape(base.m_radius, Vector2.zero, 17895696U, num, num2, num3, ucpCollisionType, false);
		this.m_subShape = new ucpCircleShape(15f, Vector2.zero, 17895696U, num, num2, num3, ucpCollisionType, false);
		ChipmunkBodyC chipmunkBodyC;
		_body = (chipmunkBodyC = ChipmunkProS.AddDynamicBody(base.m_tc, _shape, this.m_parent.m_unitC));
		this.m_body = chipmunkBodyC;
		ChipmunkProS.SetBodyLinearDamp(_body, new Vector2(0.9975f, 0.9975f));
		_body.m_identifier = 2;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00015618 File Offset: 0x00013A18
	public override void OnCollision()
	{
		this.ActivateSubBody();
		Transform transform = this.m_prefabC.p_gameObject.transform;
		string text = "GoalBotBouncyBody/GoalBotBouncyFlagBase/PurpleSmokeLocator";
		Transform transform2 = transform.Find(text);
		this.LaunchParticles(RESOURCE.PurpleSmoke_GameObject, transform2, true);
		this.m_hoverTween = TweenS.AddTween(this.m_parent.m_entity, TweenStyle.QuadInOut, -5f, 5f, 1.5f, 0f);
		TweenS.SetAdditionalTweenProperties(this.m_hoverTween, -1, true, TweenStyle.QuadInOut);
		this.m_rotationTween = TweenS.AddTween(this.m_parent.m_entity, TweenStyle.Linear, 0f, 360f, 5f, 0f);
		TweenS.SetAdditionalTweenProperties(this.m_rotationTween, -1, false, TweenStyle.Linear);
		base.OnCollision();
	}

	// Token: 0x060001BF RID: 447 RVA: 0x000156D0 File Offset: 0x00013AD0
	private void ActivateSubBody()
	{
		ChipmunkProS.RemoveBody(this.m_body);
		this.m_body = ChipmunkProS.AddDynamicBody(base.m_tc, this.m_subShape, this.m_parent.m_unitC);
		ChipmunkProS.SetBodyLinearDamp(this.m_body, new Vector2(0.9975f, 0.9975f));
		ChipmunkProS.SetBodyGravity(this.m_body, Vector2.zero);
		if (this.m_parent.m_camTarget != null)
		{
			this.m_parent.m_camTarget.offset = Vector2.up * 100f;
			this.m_parent.m_projection.m_offset = new Vector2(0f, 10f);
			this.m_parent.m_projection.m_projector.orthographicSize = 35f;
		}
	}

	// Token: 0x04000194 RID: 404
	private ChipmunkBodyC m_body;

	// Token: 0x04000195 RID: 405
	private ucpCircleShape m_subShape;

	// Token: 0x04000196 RID: 406
	private TweenC m_hoverTween;

	// Token: 0x04000197 RID: 407
	private TweenC m_rotationTween;
}
