using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class SpeedPlatform : Unit
{
	// Token: 0x0600025D RID: 605 RVA: 0x0001E9A8 File Offset: 0x0001CDA8
	public SpeedPlatform(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		GameObject gameObject = ResourceManager.GetGameObject("Units/MetalPlatformMediumPrefab");
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(transformC, _graphElement.m_position, _graphElement.m_rotation);
		GameObject gameObject2 = gameObject.transform.Find("MetalPlatformMedium").gameObject;
		PrefabC prefabC = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject2);
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject.transform.Find("Collision1").gameObject, Vector2.zero, 1f, 0.25f, 0.1f, (ucpCollisionType)4, 257U, false, base.m_graphElement.m_flipped);
		this.m_cmb = ChipmunkProS.AddStaticBody(transformC, new ucpShape[] { ucpPolyShape }, this.m_unitC);
		this.m_beltShape = ucpPolyShape.shapePtr;
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.PlatformCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, true, false);
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.PlatformCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, true, true, false);
		}
		this.CreateEditorTouchArea(prefabC.p_gameObject, null);
		base.m_graphElement.m_isRotateable = true;
		base.m_graphElement.m_isFlippable = true;
		if (base.m_graphElement.m_flipped)
		{
			TransformS.SetScale(this.m_cmb.TC, new Vector3(-1f, 1f, 1f));
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0001EB38 File Offset: 0x0001CF38
	private void PlatformCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		if ((_phase == ucpCollisionPhase.Begin || _phase == ucpCollisionPhase.Persist) && _pair.shapeA == this.m_beltShape && _phase == ucpCollisionPhase.Begin && this.m_soundTriggerTimer <= 0 && unitC.m_unit.m_entity == this.m_minigame.m_playerTC.p_entity)
		{
			this.m_soundTriggerTimer = 60;
			SoundS.PlaySingleShot("/Ingame/Units/BoostRamp", new Vector3(_pair.point.x, _pair.point.y, 0f), 1f);
		}
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0001EC05 File Offset: 0x0001D005
	public override void Update()
	{
		base.Update();
		if (this.m_soundTriggerTimer > 0)
		{
			this.m_soundTriggerTimer--;
		}
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0001EC27 File Offset: 0x0001D027
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x04000284 RID: 644
	private ChipmunkBodyC m_cmb;

	// Token: 0x04000285 RID: 645
	private int m_soundTriggerTimer;

	// Token: 0x04000286 RID: 646
	private IntPtr m_beltShape;
}
