using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class PsPumpkin : Unit
{
	// Token: 0x06000181 RID: 385 RVA: 0x000127EC File Offset: 0x00010BEC
	public PsPumpkin(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_tc = TransformS.AddComponent(this.m_entity, "PsPumpkin");
		TransformS.SetTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.PumpkinLanternPrefab_GameObject);
		this.m_prefabC = PrefabS.AddComponent(this.m_tc, new Vector3(0f, 0f, 0f), gameObject);
		ucpPolyShape[] array = ChipmunkProS.GeneratePolyShapesFromChildren(gameObject.transform.Find("Collision1").gameObject, Vector2.zero, 14f, 0.4f, 0.75f, (ucpCollisionType)4, 17895696U, false, false);
		this.m_cmb = ChipmunkProS.AddDynamicBody(this.m_tc, array, null);
		this.m_cmb.customComponent = this.m_unitC;
		this.m_cmb.m_identifier = 3;
		if (base.m_graphElement.m_storedRotation == Vector3.zero)
		{
			base.m_graphElement.m_storedRotation = new Vector3(0f, Random.Range(0f, 25f) * (float)Random.Range(-1, 2) + 0.01f, 0f);
		}
		this.m_prefabC.p_gameObject.transform.Rotate(base.m_graphElement.m_storedRotation);
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.m_canElectrify = true;
		this.CreateEditorTouchArea(50f, 50f, null, default(Vector2));
		base.m_graphElement.m_isRotateable = true;
		if (!this.m_minigame.m_editing)
		{
			this.m_ctc = CameraS.AddTargetComponent(this.m_tc, 150f, 150f, Vector2.zero);
			this.m_ctc.activeRadius = 100f;
		}
		this.m_checkForCrushing = true;
		this.m_canBurn = true;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x000129C5 File Offset: 0x00010DC5
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Impact, 32f);
		this.SetBaseArmor(DamageType.Weapon, 15f);
	}

	// Token: 0x06000183 RID: 387 RVA: 0x000129E5 File Offset: 0x00010DE5
	public override void Damage(Damage _damage, float _multiplier, Unit _source)
	{
		if (_source != null && _source.m_graphElement.GetType() == typeof(LevelPlayerNode))
		{
			_multiplier = 1f;
		}
		base.Damage(_damage, _multiplier, _source);
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00012A17 File Offset: 0x00010E17
	public override void Update()
	{
		base.Update();
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00012A1F File Offset: 0x00010E1F
	public void CreateCamTarget()
	{
		this.m_ctc = CameraS.AddTargetComponent(this.m_tc, 250f, 250f, Vector2.zero);
		this.m_ctc.activeRadius = 750f;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00012A51 File Offset: 0x00010E51
	public override void EmergencyKill()
	{
		this.Kill(DamageType.Impact, float.MaxValue);
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00012A60 File Offset: 0x00010E60
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		if (this.m_isDead)
		{
			return;
		}
		if (_damageType == DamageType.BlackHole)
		{
			base.Kill(_damageType, _totalDamage);
		}
		else
		{
			base.Kill(_damageType, _totalDamage);
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
			PsS.ApplyBlastWave(vector, new Vector2(10f, 3500f), 360f, 50f, 0f);
			EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.PumpkinLanternExplosionPrefab_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 1f, "GTAG_AUTODESTROY", null);
			SoundS.PlaySingleShot("/Ingame/Units/PumpkinExplosion", new Vector3(vector.x, vector.y, 0f), 1f);
			this.Destroy();
		}
	}

	// Token: 0x04000163 RID: 355
	public TransformC m_tc;

	// Token: 0x04000164 RID: 356
	public PrefabC m_prefabC;

	// Token: 0x04000165 RID: 357
	public ChipmunkBodyC m_cmb;

	// Token: 0x04000166 RID: 358
	public CameraTargetC m_ctc;
}
