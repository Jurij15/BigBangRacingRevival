using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class DecorationalPyroChickoon : Unit
{
	// Token: 0x06000238 RID: 568 RVA: 0x0001C0C0 File Offset: 0x0001A4C0
	public DecorationalPyroChickoon(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		float num = -43.7f;
		this.m_pyroNode = base.m_graphElement as LevelDecorationNode;
		this.m_tc = TransformS.AddComponent(this.m_entity, "Chickoon");
		TransformS.SetTransform(this.m_tc, this.m_pyroNode.m_position + new Vector3(0f, 0f, num) + base.GetZBufferBias(), this.m_pyroNode.m_rotation);
		this.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.DecorationPyrotechnicsChickoonBucketPrefab_GameObject);
		this.m_chickoon = PrefabS.AddComponent(this.m_tc, Vector3.zero, this.m_mainPrefab, string.Empty, true, true);
		this.m_pyroNode.m_isFlippable = false;
		this.CreateEditorTouchArea(50f, 50f, this.m_tc, default(Vector2));
		if (!this.m_minigame.m_editing)
		{
			ucpCircleShape ucpCircleShape = new ucpCircleShape(250f, Vector2.zero, 16777216U, 1f, 1f, 1f, (ucpCollisionType)10, true);
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddStaticBody(this.m_tc, ucpCircleShape, null);
			ChipmunkProS.AddCollisionHandler(chipmunkBodyC, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, false);
		}
		this.m_triggered = false;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0001C208 File Offset: 0x0001A608
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
			SoundS.PlaySingleShot("/Ingame/Units/Decorations/DecorationPyrotechnicsChickoon_ShootBroiler", Vector2.zero, 1f);
			this.m_chickoon.p_gameObject.transform.GetChild(0).gameObject.SendMessage("PlayEffect");
			this.m_triggered = true;
			PsAchievementManager.IncrementProgress("triggerFortyChickens", 1);
		}
	}

	// Token: 0x04000246 RID: 582
	public LevelDecorationNode m_pyroNode;

	// Token: 0x04000247 RID: 583
	public GameObject m_mainPrefab;

	// Token: 0x04000248 RID: 584
	public PrefabC m_chickoon;

	// Token: 0x04000249 RID: 585
	public TransformC m_tc;

	// Token: 0x0400024A RID: 586
	private bool m_triggered;
}
