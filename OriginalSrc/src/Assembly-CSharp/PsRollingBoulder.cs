using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class PsRollingBoulder : Unit
{
	// Token: 0x06000188 RID: 392 RVA: 0x00012B30 File Offset: 0x00010F30
	public PsRollingBoulder(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position + new Vector3(0f, 0f, 0f) + base.GetZBufferBias(), _graphElement.m_rotation);
		this.m_crushTolerance *= 2;
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.CreateBody();
		this.m_checkForCrushing = true;
		base.m_graphElement.m_isRotateable = false;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00012BD0 File Offset: 0x00010FD0
	private void CreateBody()
	{
		if (this.m_minigame.m_editing)
		{
			LevelRollingBoulderNode levelRollingBoulderNode = base.m_graphElement as LevelRollingBoulderNode;
			int num = Enum.GetNames(typeof(PsRollingBoulderRadius)).Length;
			if (levelRollingBoulderNode.m_flipped)
			{
				int num2 = (int)(levelRollingBoulderNode.m_boulderRadius + 1);
				if (num2 >= num)
				{
					num2 = 0;
				}
				levelRollingBoulderNode.m_boulderRadius = (PsRollingBoulderRadius)Enum.GetValues(typeof(PsRollingBoulderRadius)).GetValue(num2);
				levelRollingBoulderNode.m_flipped = false;
			}
		}
		float num3 = 1f;
		string text = "BoulderBigPrefab_GameObject";
		PsRollingBoulderRadius boulderRadius = ((LevelRollingBoulderNode)base.m_graphElement).m_boulderRadius;
		if (boulderRadius != PsRollingBoulderRadius.Large260)
		{
			if (boulderRadius != PsRollingBoulderRadius.Medium125)
			{
				if (boulderRadius == PsRollingBoulderRadius.Small60)
				{
					num3 = 30f;
					text = "BoulderSmallPrefab_GameObject";
					this.m_effect = RESOURCE.EffectBoulderSmallBreak_GameObject;
				}
			}
			else
			{
				num3 = 62.5f;
				text = "BoulderMediumPrefab_GameObject";
				this.m_effect = RESOURCE.EffectBoulderMediumBreak_GameObject;
			}
		}
		else
		{
			num3 = 130f;
			text = "BoulderBigPrefab_GameObject";
			this.m_effect = RESOURCE.EffectBoulderBigBreak_GameObject;
		}
		float num4 = ChipmunkProWrapper.ucpAreaForCircle(0f, num3);
		float num5 = 0.05f;
		this.m_pc = PrefabS.AddComponent(this.m_tc, new Vector3(0f, 0f, 0f), ResourceManager.GetGameObject(text));
		ucpCircleShape ucpCircleShape = new ucpCircleShape(num3, Vector2.zero, 17895696U, num4 * num5, 0.25f, 0.5f, (ucpCollisionType)4, false);
		this.m_cmb = ChipmunkProS.AddDynamicBody(this.m_tc, ucpCircleShape, null);
		this.m_cmb.customComponent = this.m_unitC;
		if (!this.m_minigame.m_editing)
		{
			this.m_ctc = CameraS.AddTargetComponent(this.m_tc, 400f, 400f, Vector2.zero);
			this.m_ctc.activeRadius = 0f;
			this.m_ctc.frameScaleVelocityMultiplier = 0.1f;
		}
		else
		{
			this.CreateEditorTouchArea(this.m_pc.p_gameObject, null);
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00012DD0 File Offset: 0x000111D0
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetVel(this.m_cmb.body);
			float magnitude = vector.magnitude;
			if (magnitude > 5f)
			{
				Vector3 vector2 = this.m_minigame.m_playerUnit.GetUnitCenterPosition() - base.GetUnitCenterPosition();
				float magnitude2 = Vector3.Project(vector, vector2).magnitude;
				this.m_ctc.activeRadius = ToolBox.limitBetween(magnitude2 * 4f, 0f, 1400f);
			}
			else
			{
				this.m_ctc.activeRadius = 0f;
			}
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00012E80 File Offset: 0x00011280
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		if (_damageType != DamageType.BlackHole)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
			SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
			EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(this.m_effect), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 3f, "GTAG_AUTODESTROY", null);
		}
		base.Kill(_damageType, _totalDamage);
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00012EFB File Offset: 0x000112FB
	public override void EmergencyKill()
	{
		this.Kill(DamageType.Impact, float.MaxValue);
		base.EmergencyKill();
	}

	// Token: 0x04000167 RID: 359
	private ChipmunkBodyC m_cmb;

	// Token: 0x04000168 RID: 360
	private PrefabC m_pc;

	// Token: 0x04000169 RID: 361
	private TransformC m_tc;

	// Token: 0x0400016A RID: 362
	private CameraTargetC m_ctc;

	// Token: 0x0400016B RID: 363
	private ResourcePool.Asset m_effect;
}
