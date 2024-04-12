using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class ConcreteArc90 : Unit
{
	// Token: 0x060001F8 RID: 504 RVA: 0x00017108 File Offset: 0x00015508
	public ConcreteArc90(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetGlobalTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.ConcreteArc90Prefab_GameObject);
		this.m_pc = PrefabS.AddComponent(this.m_tc, Vector3.zero + base.GetZBufferBias(), gameObject.transform.Find("arc").gameObject);
		ucpSegmentShape[] array = new ucpSegmentShape[11];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new ucpSegmentShape(gameObject.transform.Find("Colliders/locator" + (i + 1)).localPosition, gameObject.transform.Find("Colliders/locator" + (i + 2)).localPosition, 10f, Vector2.zero, 257U, 20f, 0.1f, 1f, (ucpCollisionType)4, false);
		}
		this.m_cmb = ChipmunkProS.AddStaticBody(this.m_tc, array, null);
		this.m_cmb.customComponent = this.m_unitC;
		base.m_graphElement.m_isRotateable = true;
		this.CreateEditorTouchArea(this.m_pc.p_gameObject, null);
	}

	// Token: 0x040001D8 RID: 472
	private TransformC m_tc;

	// Token: 0x040001D9 RID: 473
	private ChipmunkBodyC m_cmb;

	// Token: 0x040001DA RID: 474
	private PrefabC m_pc;
}
