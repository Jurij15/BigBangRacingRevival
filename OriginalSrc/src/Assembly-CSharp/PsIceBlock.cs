using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class PsIceBlock : PsBlock
{
	// Token: 0x06000172 RID: 370 RVA: 0x00011B80 File Offset: 0x0000FF80
	public PsIceBlock(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_materialName = "IceMaterial";
		this.CreatePhysicsAndModel();
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, false, true, false);
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, false, true, false);
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)4, (ucpCollisionType)2, false, true, false);
		}
		this.m_meltTicks = 0;
		this.m_scale = this.m_cmb.TC.transform.localScale;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00011C2B File Offset: 0x0001002B
	public override void SetPhysicsParameters()
	{
		this.m_massMultiplier = 0.01f;
		this.m_friction = 0.25f;
		this.m_elasticity = 0.1f;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00011C50 File Offset: 0x00010050
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted && !this.m_isDead)
		{
			this.m_meltTicks++;
			this.m_scale -= Vector3.one * 0.0001f;
			if (this.m_scale.x < 0.33f)
			{
				this.Kill(DamageType.Impact, 5000f);
			}
			else if (this.m_meltTicks % 10 == this.m_entity.m_index % 10)
			{
				this.m_cmb.TC.transform.localScale = new Vector3(this.m_scale.x, this.m_scale.y, this.m_scale.z * 0.95f);
				ChipmunkProWrapper.ucpBodySetMass(this.m_cmb.body, Mathf.Max(this.m_area * this.m_massMultiplier * this.m_scale.x, 0.2f));
				ChipmunkProWrapper.ucpBodyActivate(this.m_cmb.body);
			}
		}
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00011D84 File Offset: 0x00010184
	private void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		float magnitude = _pair.friction.magnitude;
		this.m_scale -= Vector3.one * (magnitude / 100000f);
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00011DC0 File Offset: 0x000101C0
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
		SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
		EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.WoodenCratePuff_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 1f, "GTAG_AUTODESTROY", null);
		this.Destroy();
	}

	// Token: 0x04000154 RID: 340
	private const float m_scaleReduction = 0.0001f;

	// Token: 0x04000155 RID: 341
	private int m_meltTicks;

	// Token: 0x04000156 RID: 342
	private Vector3 m_scale;
}
