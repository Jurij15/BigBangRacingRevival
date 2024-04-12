using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class DangerPlatform : Unit
{
	// Token: 0x06000206 RID: 518 RVA: 0x000191D8 File Offset: 0x000175D8
	public DangerPlatform(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.DangerPlatformPrefab_GameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(transformC, _graphElement.m_position, _graphElement.m_rotation);
		PrefabC prefabC = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject);
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject.transform.Find("Collision2").gameObject, Vector2.zero, 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, false);
		this.m_cmb = ChipmunkProS.AddStaticBody(transformC, ucpPolyShape, this.m_unitC);
		this.m_cmb.m_identifier = 1337;
		ucpPolyShape ucpPolyShape2 = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject.transform.Find("Collision1").gameObject, Vector2.zero, 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, false);
		ChipmunkProS.AddStaticBody(transformC, ucpPolyShape2, this.m_unitC);
		this.CreateEditorTouchArea(prefabC.p_gameObject, null);
		base.m_graphElement.m_isRotateable = true;
		this.m_isElectrified = true;
		this.m_canElectrify = false;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0001930B File Offset: 0x0001770B
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x040001F1 RID: 497
	public const int STATIC_ELECTRIC_UNIT = 1337;

	// Token: 0x040001F2 RID: 498
	private ChipmunkBodyC m_cmb;
}
