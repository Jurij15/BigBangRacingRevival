using System;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class DecorationalPyrotechnics : Unit
{
	// Token: 0x0600023C RID: 572 RVA: 0x0001C4B4 File Offset: 0x0001A8B4
	public DecorationalPyrotechnics(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_pyroNode = base.m_graphElement as GraphNode;
		this.m_tc = TransformS.AddComponent(this.m_entity, "Confetti");
		TransformS.SetTransform(this.m_tc, this.m_pyroNode.m_position + new Vector3(0f, 0f, 64f) + base.GetZBufferBias(), this.m_pyroNode.m_rotation);
		this.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.DecorationPyrotechnicsConfettiWorkPrefab_GameObject);
		this.m_confetti = PrefabS.AddComponent(this.m_tc, Vector3.zero, this.m_mainPrefab, string.Empty, true, true);
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

	// Token: 0x0600023D RID: 573 RVA: 0x0001C628 File Offset: 0x0001AA28
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
			this.m_confetti.p_gameObject.transform.GetChild(0).gameObject.SendMessage("PlayEffect");
			this.m_confetti.p_gameObject.transform.GetChild(1).gameObject.SendMessage("PlayEffect");
			this.m_triggered = true;
		}
	}

	// Token: 0x04000250 RID: 592
	public GraphNode m_pyroNode;

	// Token: 0x04000251 RID: 593
	public GameObject m_mainPrefab;

	// Token: 0x04000252 RID: 594
	public PrefabC m_confetti;

	// Token: 0x04000253 RID: 595
	public TransformC m_tc;

	// Token: 0x04000254 RID: 596
	private bool m_triggered;
}
