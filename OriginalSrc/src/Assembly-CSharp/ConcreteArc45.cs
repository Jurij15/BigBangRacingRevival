using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class ConcreteArc45 : Unit
{
	// Token: 0x060001F7 RID: 503 RVA: 0x00016FB0 File Offset: 0x000153B0
	public ConcreteArc45(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetGlobalTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.ConcreteArc45Prefab_GameObject);
		this.m_pc = PrefabS.AddComponent(this.m_tc, Vector3.zero + base.GetZBufferBias(), gameObject.transform.Find("arc").gameObject);
		ucpSegmentShape[] array = new ucpSegmentShape[6];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new ucpSegmentShape(gameObject.transform.Find("Colliders/locator" + (i + 1)).localPosition, gameObject.transform.Find("Colliders/locator" + (i + 2)).localPosition, 10f, Vector2.zero, 257U, 20f, 0.1f, 1f, (ucpCollisionType)4, false);
		}
		this.m_cmb = ChipmunkProS.AddStaticBody(this.m_tc, array, null);
		this.m_cmb.customComponent = this.m_unitC;
		base.m_graphElement.m_isRotateable = true;
		this.CreateEditorTouchArea(this.m_pc.p_gameObject, null);
	}

	// Token: 0x040001D5 RID: 469
	private TransformC m_tc;

	// Token: 0x040001D6 RID: 470
	private ChipmunkBodyC m_cmb;

	// Token: 0x040001D7 RID: 471
	private PrefabC m_pc;
}
