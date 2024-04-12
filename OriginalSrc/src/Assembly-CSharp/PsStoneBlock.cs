using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class PsStoneBlock : PsBlock
{
	// Token: 0x06000177 RID: 375 RVA: 0x00011E39 File Offset: 0x00010239
	public PsStoneBlock(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_materialName = "MetalMaterial";
		this.CreatePhysicsAndModel();
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.m_canElectrify = true;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00011E6C File Offset: 0x0001026C
	public override void SetPhysicsParameters()
	{
		this.m_massMultiplier = 0.015f;
		this.m_friction = 0.9f;
		this.m_elasticity = 0.1f;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00011E90 File Offset: 0x00010290
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
		SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
		EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.MetalBoxCrush_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 1f, "GTAG_AUTODESTROY", null);
		this.Destroy();
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00011F0C File Offset: 0x0001030C
	public override void SetElectrified(bool _electrify, Vector2 _contactPoint)
	{
		base.SetElectrified(_electrify, _contactPoint);
		if (this.m_pc.p_gameObject == null)
		{
			return;
		}
		if (_electrify && this.m_electricity == null)
		{
			this.m_electricity = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.EffectEletricity_GameObject));
			this.m_electricity.p_gameObject.transform.parent = this.m_pc.p_gameObject.transform;
			this.m_electricity.p_gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			ParticleSystem[] componentsInChildren = this.m_electricity.p_gameObject.transform.GetComponentsInChildren<ParticleSystem>();
			Mesh mesh = this.m_pc.p_gameObject.GetComponent<MeshFilter>().mesh;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ParticleSystem.ShapeModule shape = componentsInChildren[i].shape;
				shape.shapeType = 6;
				shape.mesh = mesh;
				shape.enabled = true;
				this.m_electricity.p_gameObject.SetActive(false);
				this.m_electricity.p_gameObject.SetActive(true);
			}
			Color color = Color.red;
			color = Color.Lerp(color, Color.white, 0.5f);
			PrefabS.SetShaderColor(this.m_pc, color);
		}
		else if (!_electrify && this.m_electricity != null)
		{
			PrefabS.RemoveComponent(this.m_electricity, true);
			this.m_electricity = null;
			PrefabS.SetShaderColor(this.m_pc, Color.white);
		}
	}

	// Token: 0x04000157 RID: 343
	private PrefabC m_electricity;
}
