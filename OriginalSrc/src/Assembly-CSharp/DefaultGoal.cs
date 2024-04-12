using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
internal class DefaultGoal : GoalRepresentation
{
	// Token: 0x060001C0 RID: 448 RVA: 0x000157A1 File Offset: 0x00013BA1
	public DefaultGoal(Goal _parent)
		: base(_parent, false)
	{
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x000157C5 File Offset: 0x00013BC5
	public override ResourcePool.Asset GetAsset()
	{
		return RESOURCE.GoalBotPrefab_GameObject;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x000157CC File Offset: 0x00013BCC
	public override void CreateBody(out ucpShape _shape, out ChipmunkBodyC _body)
	{
		PsMetrics.GoalBallStateChanged("default");
		float num = 500f;
		float num2 = 0.5f;
		float num3 = 0.9f;
		ucpCollisionType ucpCollisionType = (ucpCollisionType)4;
		_shape = new ucpCircleShape(base.m_radius, Vector2.zero, 17895696U, num, num2, num3, ucpCollisionType, false);
		_body = ChipmunkProS.AddDynamicBody(base.m_tc, _shape, this.m_parent.m_unitC);
		ChipmunkProS.SetBodyLinearDamp(_body, new Vector2(0.99f, 0.99f));
		ChipmunkProS.SetBodyGravity(_body, Vector2.zero);
		_body.m_identifier = 2;
		this.m_tweenC = TweenS.AddTween(this.m_parent.m_entity, TweenStyle.QuadInOut, -10f, 10f, 2f, 0f);
		TweenS.SetAdditionalTweenProperties(this.m_tweenC, -1, true, TweenStyle.QuadInOut);
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00015890 File Offset: 0x00013C90
	public override PrefabC CreatePrefab()
	{
		PrefabC prefabC = base.CreatePrefab();
		prefabC.p_gameObject.transform.Rotate(new Vector3(0f, 180f, 0f));
		return prefabC;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x000158CC File Offset: 0x00013CCC
	public override void Update()
	{
		TransformC playerTC = this.m_parent.m_minigame.m_playerTC;
		if (playerTC != null)
		{
			base.m_tc.transform.LookAt(playerTC.transform.position + this.m_offset);
		}
		else
		{
			LevelPlayerNode levelPlayerNode = LevelManager.m_currentLevel.m_currentLayer.GetElement("Player") as LevelPlayerNode;
			if (levelPlayerNode != null)
			{
				base.m_tc.transform.LookAt(levelPlayerNode.m_position + this.m_offset);
			}
		}
		if (this.m_tweenC != null)
		{
			Vector3 vector;
			vector..ctor(0f, this.m_tweenC.currentValue.x, 0f);
			this.m_prefabC.p_gameObject.transform.localPosition = vector;
		}
	}

	// Token: 0x04000198 RID: 408
	private TweenC m_tweenC;

	// Token: 0x04000199 RID: 409
	private Vector3 m_offset = new Vector3(0f, 0f, -200f);
}
