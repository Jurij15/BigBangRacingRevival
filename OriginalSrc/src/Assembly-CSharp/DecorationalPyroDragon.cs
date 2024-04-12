using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class DecorationalPyroDragon : Unit
{
	// Token: 0x0600023A RID: 570 RVA: 0x0001C2A4 File Offset: 0x0001A6A4
	public DecorationalPyroDragon(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_pyroNode = base.m_graphElement as LevelDecorationNode;
		this.m_tc = TransformS.AddComponent(this.m_entity, "Dragon");
		TransformS.SetTransform(this.m_tc, this.m_pyroNode.m_position + new Vector3(0f, 0f, 64f) + base.GetZBufferBias(), this.m_pyroNode.m_rotation);
		this.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.DecorationPyrotehnicsDragonWorkPrefab_GameObject);
		this.m_dragon = PrefabS.AddComponent(this.m_tc, Vector3.zero, this.m_mainPrefab, string.Empty, true, true);
		this.m_pyroNode.m_isFlippable = true;
		if (this.m_pyroNode.m_flipped)
		{
			TransformS.SetScale(this.m_tc, new Vector3(-1f, 1f, 1f));
		}
		this.CreateEditorTouchArea(50f, 50f, this.m_tc, default(Vector2));
		if (!this.m_minigame.m_editing)
		{
			ucpCircleShape ucpCircleShape = new ucpCircleShape(250f, Vector2.zero, 16777216U, 1f, 1f, 1f, (ucpCollisionType)10, true);
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddStaticBody(this.m_tc, ucpCircleShape, null);
			ChipmunkProS.AddCollisionHandler(chipmunkBodyC, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, false);
		}
		this.m_triggered = false;
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0001C418 File Offset: 0x0001A818
	private void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (!this.m_triggered)
		{
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
			UnitC unitC = chipmunkBodyC.customComponent as UnitC;
			if (unitC == null || unitC.m_unit == null)
			{
				return;
			}
			this.m_dragon.p_gameObject.transform.GetChild(0).gameObject.SendMessage("PlayEffect");
			this.m_dragon.p_gameObject.transform.GetChild(1).gameObject.SendMessage("PlayEffect");
			this.m_triggered = true;
		}
	}

	// Token: 0x0400024B RID: 587
	public LevelDecorationNode m_pyroNode;

	// Token: 0x0400024C RID: 588
	public GameObject m_mainPrefab;

	// Token: 0x0400024D RID: 589
	public PrefabC m_dragon;

	// Token: 0x0400024E RID: 590
	public TransformC m_tc;

	// Token: 0x0400024F RID: 591
	private bool m_triggered;
}
