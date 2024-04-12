using System;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public class Crate : Unit
{
	// Token: 0x0600044E RID: 1102 RVA: 0x00038FEC File Offset: 0x000373EC
	public Crate(GraphElement _graphElement, float _size, float _friction = 0.8f, float _elasticity = 0.5f, float _weight = 1f, string _resource = "")
		: base(_graphElement, UnitType.Basic)
	{
		GameObject gameObject = ResourceManager.GetGameObject(_resource);
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(transformC, _graphElement.m_position, _graphElement.m_rotation);
		ucpPolyShape ucpPolyShape = new ucpPolyShape(_size, _size, Vector2.zero, 17895696U, _weight, _elasticity, _friction, (ucpCollisionType)4, false);
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, ucpPolyShape, this.m_unitC);
		this.m_cmb = chipmunkBodyC;
		this.boxPrefab = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 0f), gameObject);
		this.boxPrefab.p_gameObject.transform.localScale = new Vector3(_size, _size, _size) * 1.15f;
		this.boxPrefab.p_gameObject.transform.Rotate(new Vector3(0f, 180f, 0f));
		this.CreateEditorTouchArea(_size, _size, null, default(Vector2));
	}

	// Token: 0x04000593 RID: 1427
	public ChipmunkBodyC m_cmb;

	// Token: 0x04000594 RID: 1428
	protected PrefabC boxPrefab;
}
