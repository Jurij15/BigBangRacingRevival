using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class MetalPlatformMedium : Unit
{
	// Token: 0x0600024C RID: 588 RVA: 0x0001DE28 File Offset: 0x0001C228
	public MetalPlatformMedium(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		GameObject gameObject = ResourceManager.GetGameObject("Units/MetalPlatformMediumPrefab");
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(transformC, _graphElement.m_position, _graphElement.m_rotation);
		GameObject gameObject2 = gameObject.transform.Find("MetalPlatformMedium").gameObject;
		PrefabC prefabC = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject2);
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject.transform.Find("Collision1").gameObject, Vector2.zero, 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, false);
		this.m_cmb = ChipmunkProS.AddStaticBody(transformC, ucpPolyShape, this.m_unitC);
		this.CreateEditorTouchArea(prefabC.p_gameObject, null);
		base.m_graphElement.m_isRotateable = true;
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0001DF0E File Offset: 0x0001C30E
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x04000278 RID: 632
	private ChipmunkBodyC m_cmb;
}
