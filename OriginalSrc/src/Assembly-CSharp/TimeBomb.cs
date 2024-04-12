using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class TimeBomb : Unit
{
	// Token: 0x060001A0 RID: 416 RVA: 0x0001423C File Offset: 0x0001263C
	public TimeBomb(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_updater = false;
		this.m_emissionVal = 0f;
		this.m_dim = false;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.FloatingBombPrefab_GameObject);
		this.m_lightMaterial = gameObject.transform.Find("Lights").GetComponent<Renderer>().sharedMaterial;
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		this.m_bombPrefabC = PrefabS.AddComponent(this.m_tc, new Vector3(0f, 0f, 0f), gameObject);
		ucpCircleShape ucpCircleShape = new ucpCircleShape(35f, Vector2.zero, 17895696U, 15f, 0.1f, 0.9f, (ucpCollisionType)4, false);
		this.m_cmb = ChipmunkProS.AddDynamicBody(this.m_tc, ucpCircleShape, null);
		this.m_cmb.customComponent = this.m_unitC;
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.SetBodyGravity(this.m_cmb, Vector2.zero);
			ChipmunkProS.SetBodyLinearDamp(this.m_cmb, new Vector2(0.993f, 0.993f));
			TransformC transformC = TransformS.AddComponent(this.m_entity, "RotaryLimit", _graphElement.m_position);
			ChipmunkProS.AddRotaryLimitJoint(this.m_cmb, ChipmunkProS.m_staticBody, transformC, 0f, 0f);
			this.m_tween = TweenS.AddTween(this.m_entity, TweenStyle.QuadInOut, -2.5f, 2.5f, 1f, 0f);
			TweenS.SetAdditionalTweenProperties(this.m_tween, -1, true, TweenStyle.QuadInOut);
		}
		this.CreateEditorTouchArea(50f, 50f, null, default(Vector2));
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		base.m_graphElement.m_isRotateable = false;
		this.m_checkForCrushing = true;
		this.m_reactToBlastWaves = true;
		int num = 0;
		for (int i = 0; i < PsS.m_units.m_aliveCount; i++)
		{
			if (num > 0)
			{
				break;
			}
			UnitC unitC = PsS.m_units.m_array[PsS.m_units.m_aliveIndices[i]];
			TimeBomb timeBomb = unitC.m_unit as TimeBomb;
			if (timeBomb != null && timeBomb != this && !timeBomb.m_isDead)
			{
				num++;
			}
			else if (timeBomb != null && timeBomb == this)
			{
				break;
			}
		}
		if (num == 0)
		{
			this.m_updater = true;
		}
		if (!this.m_minigame.m_editing)
		{
			this.CreateCamTarget();
		}
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x000144CF File Offset: 0x000128CF
	public override void SetGravity(Vector2 _gravity)
	{
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000144D1 File Offset: 0x000128D1
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Impact, 3f);
		this.SetBaseArmor(DamageType.Electric, 10f);
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000144F1 File Offset: 0x000128F1
	public override void Damage(Damage _damage, float _multiplier, Unit _source)
	{
		if (_source != null && _source.m_graphElement.GetType() == typeof(LevelPlayerNode))
		{
			_multiplier = 0.2f;
		}
		base.Damage(_damage, _multiplier, _source);
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00014524 File Offset: 0x00012924
	public override void Destroy()
	{
		if (this.m_updater)
		{
			for (int i = 0; i < PsS.m_units.m_aliveCount; i++)
			{
				UnitC unitC = PsS.m_units.m_array[PsS.m_units.m_aliveIndices[i]];
				TimeBomb timeBomb = unitC.m_unit as TimeBomb;
				if (timeBomb != null && timeBomb != this)
				{
					timeBomb.m_updater = true;
					break;
				}
			}
		}
		base.Destroy();
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0001459C File Offset: 0x0001299C
	public void CreateCamTarget()
	{
		CameraTargetC cameraTargetC = CameraS.AddTargetComponent(this.m_tc, 150f, 150f, new Vector2(0f, 0f));
		cameraTargetC.activeRadius = 500f;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x000145DC File Offset: 0x000129DC
	public override void Update()
	{
		base.Update();
		if (!this.m_isDead)
		{
			if (this.m_updater)
			{
				this.m_lightMaterial.SetFloat("_Emission", this.m_emissionVal);
				this.m_emissionVal += ((!this.m_dim) ? 0.01f : (-0.01f));
				if ((this.m_dim && this.m_emissionVal <= 0f) || (!this.m_dim && this.m_emissionVal >= 0.5f))
				{
					this.m_dim = !this.m_dim;
				}
			}
			if (!this.m_minigame.m_editing)
			{
				this.m_bombPrefabC.p_gameObject.transform.Rotate(0f, -0.5f, 0f);
				this.m_bombPrefabC.p_gameObject.transform.localPosition = new Vector3(0f, this.m_tween.currentValue.x, 0f);
			}
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x000146EF File Offset: 0x00012AEF
	public override void EmergencyKill()
	{
		if (!this.m_isDead)
		{
			this.Kill(DamageType.Impact, float.MaxValue);
		}
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00014708 File Offset: 0x00012B08
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		if (_damageType != DamageType.BlackHole)
		{
			TweenS.RemoveComponent(this.m_tween);
			this.m_tween = null;
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
			PsS.ApplyBlastWave(vector, new Vector2(50f, 4000f), 600f, 180f, 30f);
			EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.FloatingBombExplosion_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 2f, "GTAG_AUTODESTROY", null);
			SoundS.PlaySingleShot("/Ingame/Units/BarrelExplosion", new Vector3(vector.x, vector.y, 0f), 1f);
			this.Destroy();
		}
	}

	// Token: 0x0400017C RID: 380
	private ChipmunkBodyC m_cmb;

	// Token: 0x0400017D RID: 381
	private PrefabC m_bombPrefabC;

	// Token: 0x0400017E RID: 382
	private TweenC m_tween;

	// Token: 0x0400017F RID: 383
	private Material m_lightMaterial;

	// Token: 0x04000180 RID: 384
	private float m_emissionVal;

	// Token: 0x04000181 RID: 385
	private bool m_dim;

	// Token: 0x04000182 RID: 386
	private bool m_updater;

	// Token: 0x04000183 RID: 387
	private TransformC m_tc;
}
