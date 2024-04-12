using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class PsWoodenBlock : PsBlock
{
	// Token: 0x06000166 RID: 358 RVA: 0x00011718 File Offset: 0x0000FB18
	public PsWoodenBlock(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_materialName = "CardboardMaterial";
		this.CreatePhysicsAndModel();
		this.m_cmb.m_identifier = 3;
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.m_canBurn = true;
		float num = this.m_area * 0.05f;
		this.SetBaseArmor(DamageType.Impact, num);
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00011777 File Offset: 0x0000FB77
	public override void SetPhysicsParameters()
	{
		this.m_massMultiplier = 0.007f;
		this.m_friction = 0.9f;
		this.m_elasticity = 0.1f;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0001179A File Offset: 0x0000FB9A
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Weapon, 20f);
		this.SetBaseArmor(DamageType.Fire, 20f);
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000117BA File Offset: 0x0000FBBA
	public override void Damage(Damage _damage, float _multiplier, Unit _source)
	{
		if (_source != null && _source.m_graphElement.GetType() == typeof(LevelPlayerNode))
		{
			_multiplier = 5f;
		}
		base.Damage(_damage, _multiplier, _source);
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000117EC File Offset: 0x0000FBEC
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		if (_damageType != DamageType.BlackHole)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
			SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
			EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.CardboardBoxPuff_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 2f, "GTAG_AUTODESTROY", null);
			PsAchievementManager.IncrementProgress("breakTwoThousandBoxes", 1);
			this.Destroy();
		}
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00011877 File Offset: 0x0000FC77
	public override void SetBurning(bool _burn)
	{
		if (_burn)
		{
			PrefabS.SetShaderColor(this.m_pc, Color.red);
		}
		else
		{
			PrefabS.SetShaderColor(this.m_pc, Color.white);
		}
	}
}
