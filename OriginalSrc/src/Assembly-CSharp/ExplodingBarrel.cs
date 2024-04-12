using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class ExplodingBarrel : Unit
{
	// Token: 0x0600015F RID: 351 RVA: 0x00010D2C File Offset: 0x0000F12C
	public ExplodingBarrel(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.ExplodingBarrelPrefab_GameObject);
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		PrefabC prefabC = PrefabS.AddComponent(this.m_tc, new Vector3(0f, 0f, 0f), gameObject.transform.Find("ExplosiveBarrel").gameObject);
		int num = 0;
		if (_graphElement is LevelIntNode)
		{
			LevelIntNode levelIntNode = _graphElement as LevelIntNode;
			num = levelIntNode.m_int;
			if (_graphElement.m_flipped)
			{
				levelIntNode.m_flipped = false;
				if (levelIntNode.m_int == 0)
				{
					levelIntNode.m_int = 1;
				}
				else
				{
					levelIntNode.m_int = 0;
				}
				num = levelIntNode.m_int;
			}
		}
		if (num == 0)
		{
			ucpPolyShape[] array = ChipmunkProS.GeneratePolyShapesFromChildren(gameObject.transform.Find("Collision1").gameObject, Vector2.zero, 18f, 0.1f, 0.9f, (ucpCollisionType)4, 17895696U, false, false);
			this.m_cmb = ChipmunkProS.AddDynamicBody(this.m_tc, array, null);
			this.m_cmb.customComponent = this.m_unitC;
		}
		else
		{
			ucpCircleShape ucpCircleShape = new ucpCircleShape(22f, Vector2.zero, 17895696U, 18f, 0.1f, 0.9f, (ucpCollisionType)4, false);
			this.m_cmb = ChipmunkProS.AddDynamicBody(this.m_tc, ucpCircleShape, null);
			this.m_cmb.customComponent = this.m_unitC;
			prefabC.p_gameObject.transform.localRotation = Quaternion.Euler(Vector3.right * 90f);
		}
		if (base.m_graphElement.m_storedRotation == Vector3.zero)
		{
			base.m_graphElement.m_storedRotation = new Vector3(0f, Random.Range(0f, 360f), 0f);
		}
		prefabC.p_gameObject.transform.Rotate(base.m_graphElement.m_storedRotation);
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.m_canElectrify = true;
		this.m_ticks = 0;
		this.m_detonated = false;
		this.m_smokeEntity = null;
		this.CreateEditorTouchArea(50f, 50f, null, default(Vector2));
		base.m_graphElement.m_isRotateable = true;
		if (!this.m_minigame.m_editing)
		{
			this.CreateCamTarget();
		}
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00010FA2 File Offset: 0x0000F3A2
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Impact, 80f);
		this.SetBaseArmor(DamageType.Electric, 10f);
		this.SetBaseArmor(DamageType.Weapon, 17f);
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00010FCE File Offset: 0x0000F3CE
	public override void Damage(Damage _damage, float _multiplier, Unit _source)
	{
		if (_source != null && _source.m_graphElement.GetType() == typeof(LevelPlayerNode))
		{
			_multiplier = 1f;
		}
		base.Damage(_damage, _multiplier, _source);
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00011000 File Offset: 0x0000F400
	public override void Update()
	{
		base.Update();
		if (this.m_detonated)
		{
			this.m_ticks++;
			if (this.m_ticks >= this.m_detonationTicks)
			{
				this.Kill(DamageType.Impact, float.MaxValue);
			}
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0001103E File Offset: 0x0000F43E
	public void CreateCamTarget()
	{
		this.m_ctc = CameraS.AddTargetComponent(this.m_tc, 250f, 250f, Vector2.zero);
		this.m_ctc.activeRadius = 750f;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00011070 File Offset: 0x0000F470
	public override void EmergencyKill()
	{
		this.Kill(DamageType.Impact, float.MaxValue);
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00011080 File Offset: 0x0000F480
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		if (this.m_isDead)
		{
			return;
		}
		if (_damageType == DamageType.Weapon)
		{
			if (!this.m_detonated)
			{
				this.m_detonated = true;
				this.m_detonationTicks = 120 + Random.Range(-15, 15);
				this.m_smokeEntity = EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.EngineBreakdown_GameObject), this.m_tc.transform.position + new Vector3(0f, 20.11f, -2.91f), Vector3.zero, 999f, "GTAG_AUTODESTROY", this.m_tc).p_entity;
				this.m_ctc.activeRadius = 1250f;
				SoundC soundC = SoundS.AddCombineSoundComponent(this.m_tc, "BurningSound", "/InGame/Units/BarrelBurn", 1f);
				SoundS.PlaySound(soundC, false);
			}
		}
		else if (_damageType == DamageType.BlackHole)
		{
			base.Kill(_damageType, _totalDamage);
			if (this.m_detonated)
			{
				PrefabS.PauseParticleEmission(EntityManager.GetComponentByEntity(ComponentType.Prefab, this.m_smokeEntity) as PrefabC, true);
			}
		}
		else
		{
			base.Kill(_damageType, _totalDamage);
			if (this.m_detonated)
			{
				PrefabS.PauseParticleEmission(EntityManager.GetComponentByEntity(ComponentType.Prefab, this.m_smokeEntity) as PrefabC, true);
			}
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
			PsS.ApplyBlastWave(vector, new Vector2(50f, 2500f), 600f, 150f, 25f);
			EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.ExplodingBarrelExplosion_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 2f, "GTAG_AUTODESTROY", null);
			SoundS.PlaySingleShot("/Ingame/Units/BarrelExplosion", new Vector3(vector.x, vector.y, 0f), 1f);
			PsAchievementManager.IncrementProgress("blowTwoHundredBarrels", 1);
			PsAchievementManager.IncrementProgress("blowTwoThousandBarrels", 1);
			this.Destroy();
		}
	}

	// Token: 0x04000146 RID: 326
	private ChipmunkBodyC m_cmb;

	// Token: 0x04000147 RID: 327
	private TransformC m_tc;

	// Token: 0x04000148 RID: 328
	private const int DETONATION_TICKS = 120;

	// Token: 0x04000149 RID: 329
	private int m_detonationTicks;

	// Token: 0x0400014A RID: 330
	private int m_ticks;

	// Token: 0x0400014B RID: 331
	private bool m_detonated;

	// Token: 0x0400014C RID: 332
	private Entity m_smokeEntity;

	// Token: 0x0400014D RID: 333
	private CameraTargetC m_ctc;
}
