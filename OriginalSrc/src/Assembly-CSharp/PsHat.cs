using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class PsHat : Unit
{
	// Token: 0x0600016C RID: 364 RVA: 0x000118A4 File Offset: 0x0000FCA4
	public PsHat(Transform _hatTransform, Vector2 _linVel, float _angVel)
		: base(new GraphNode(GraphNodeType.Normal, "Hat"), UnitType.Basic)
	{
		EntityManager.AddTagForEntity(this.m_entity, "GTAG_AUTODESTROY");
		this.m_tc = TransformS.AddComponent(this.m_entity, "Hat");
		TransformS.SetTransform(this.m_tc, _hatTransform.position, _hatTransform.rotation.eulerAngles);
		this.m_checkForCrushing = true;
		Vector2 vector;
		vector..ctor(_hatTransform.localPosition.x - _hatTransform.parent.transform.localPosition.x, _hatTransform.transform.localPosition.y - _hatTransform.parent.transform.localPosition.y);
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(_hatTransform.gameObject, -vector, 1f, this.m_elasticity, this.m_friction, (ucpCollisionType)4, 17895696U, false, false);
		ucpPolyShape.mass = ucpPolyShape.area * this.m_massMultiplier;
		this.m_cmb = ChipmunkProS.AddDynamicBody(this.m_tc, ucpPolyShape, null);
		this.m_cmb.customComponent = this.m_unitC;
		this.m_cmb.m_identifier = 3;
		this.m_prefabC = PrefabS.AddComponent(this.m_tc, Vector3.zero, _hatTransform.gameObject);
		ChipmunkProWrapper.ucpBodySetVel(this.m_cmb.body, _linVel);
		ChipmunkProWrapper.ucpBodySetAngVel(this.m_cmb.body, _angVel);
		Object.Destroy(_hatTransform.gameObject);
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.m_canBurn = true;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00011A60 File Offset: 0x0000FE60
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Impact, 145f);
		this.SetBaseArmor(DamageType.Weapon, 20f);
		this.SetBaseArmor(DamageType.Fire, 20f);
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00011A8C File Offset: 0x0000FE8C
	public override void Damage(Damage _damage, float _multiplier, Unit _source)
	{
		if (_source != null && _source.m_graphElement.GetType() == typeof(LevelPlayerNode))
		{
			_multiplier = 5f;
		}
		base.Damage(_damage, _multiplier, _source);
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00011AC0 File Offset: 0x0000FEC0
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
		SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
		EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.EffectGenericHatsplosion_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 2f, "GTAG_AUTODESTROY", null);
		this.Destroy();
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00011B39 File Offset: 0x0000FF39
	public override void EmergencyKill()
	{
		if (!this.m_isDead)
		{
			this.Kill(DamageType.Impact, float.MaxValue);
		}
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00011B52 File Offset: 0x0000FF52
	public override void SetBurning(bool _burn)
	{
		if (_burn)
		{
			PrefabS.SetShaderColor(this.m_prefabC, Color.red);
		}
		else
		{
			PrefabS.SetShaderColor(this.m_prefabC, Color.white);
		}
	}

	// Token: 0x0400014E RID: 334
	public TransformC m_tc;

	// Token: 0x0400014F RID: 335
	public PrefabC m_prefabC;

	// Token: 0x04000150 RID: 336
	public ChipmunkBodyC m_cmb;

	// Token: 0x04000151 RID: 337
	protected float m_massMultiplier = 0.007f;

	// Token: 0x04000152 RID: 338
	protected float m_friction = 0.4f;

	// Token: 0x04000153 RID: 339
	protected float m_elasticity = 0.25f;
}
