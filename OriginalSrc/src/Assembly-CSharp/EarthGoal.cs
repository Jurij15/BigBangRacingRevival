using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class EarthGoal : SensorGoal
{
	// Token: 0x060001A9 RID: 425 RVA: 0x00014A78 File Offset: 0x00012E78
	public EarthGoal(GraphElement _graphElement)
		: base(_graphElement)
	{
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00014A84 File Offset: 0x00012E84
	public override ucpShape CreateBodyShape()
	{
		this.m_mainPrefab = ResourceManager.GetGameObject("GoalGatePrefab_GameObject");
		Vector2[] rect = DebugDraw.GetRect(this.m_mainPrefab.GetComponent<Renderer>().bounds.size.x / 2f, this.m_mainPrefab.GetComponent<Renderer>().bounds.size.y, Vector2.zero);
		return new ucpPolyShape(rect, Vector2.zero, 17895696U, 500f, 0.5f, 0.9f, (ucpCollisionType)6, true);
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00014B14 File Offset: 0x00012F14
	public override void SetGoalPrefab()
	{
		this.m_prefabC = PrefabS.AddComponent(this.m_tc, new Vector3(0f, -this.m_mainPrefab.GetComponent<Renderer>().bounds.size.y / 2f), this.m_mainPrefab);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_goalCmb, new CollisionDelegate(base.GoalCollisionHandler), (ucpCollisionType)6, (ucpCollisionType)3, true, false, false);
		}
		else
		{
			Vector2[] circle = DebugDraw.GetCircle(45f, 32, Vector2.zero);
			this.m_minigame.SetGoalNode(this);
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00014BB8 File Offset: 0x00012FB8
	protected override void WinMiniGame()
	{
		PsState.m_activeGameLoop.WinMinigame();
	}
}
