using System;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class SawBlade : Unit
{
	// Token: 0x0600025A RID: 602 RVA: 0x0001E860 File Offset: 0x0001CC60
	public SawBlade(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		GameObject gameObject = ResourceManager.GetGameObject("Units/SawBladePrefab");
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(transformC, _graphElement.m_position, _graphElement.m_rotation);
		PrefabC prefabC = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 0f) + base.GetZBufferBias(), gameObject);
		ucpCircleShape ucpCircleShape = new ucpCircleShape(10f, Vector2.zero, 17895696U, 1f, 0.25f, 0.8f, (ucpCollisionType)4, false);
		this.m_cmb = ChipmunkProS.AddKinematicBody(transformC, ucpCircleShape, this.m_unitC);
		this.CreateEditorTouchArea(prefabC.p_gameObject, null);
		base.m_graphElement.m_isRotateable = true;
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0001E920 File Offset: 0x0001CD20
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted)
		{
			this.m_angle = ToolBox.getCappedAngle(this.m_angle - 0.3f);
			ChipmunkProWrapper.ucpBodySetAngle(this.m_cmb.body, (base.m_graphElement.m_rotation.z + this.m_angle) * 0.017453292f);
		}
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0001E997 File Offset: 0x0001CD97
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x04000282 RID: 642
	private ChipmunkBodyC m_cmb;

	// Token: 0x04000283 RID: 643
	private float m_angle;
}
