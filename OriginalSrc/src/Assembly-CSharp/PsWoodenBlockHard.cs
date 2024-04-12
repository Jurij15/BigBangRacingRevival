using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class PsWoodenBlockHard : PsBlock
{
	// Token: 0x0600018D RID: 397 RVA: 0x00012F10 File Offset: 0x00011310
	public PsWoodenBlockHard(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_materialName = "WoodMaterial";
		this.CreatePhysicsAndModel();
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.m_canBurn = true;
		float num = this.m_area * 0.5f;
		this.SetBaseArmor(DamageType.Impact, num);
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00012F63 File Offset: 0x00011363
	public override void SetPhysicsParameters()
	{
		this.m_massMultiplier = 0.007f;
		this.m_friction = 0.9f;
		this.m_elasticity = 0.1f;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00012F86 File Offset: 0x00011386
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Weapon, 20f);
		this.SetBaseArmor(DamageType.Fire, 20f);
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00012FA8 File Offset: 0x000113A8
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
		SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
		EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.WoodenCratePuff_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 1f, "GTAG_AUTODESTROY", null);
		this.Destroy();
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00013021 File Offset: 0x00011421
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
