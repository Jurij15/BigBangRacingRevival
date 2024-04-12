using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class PowerSensor : Unit
{
	// Token: 0x06000224 RID: 548 RVA: 0x0001B344 File Offset: 0x00019744
	public PowerSensor(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_powerSource = base.m_graphElement as LevelPowerLink;
		this.m_powerSource.m_name = "PowerSensor";
		this.m_powerSource.m_inputLimit = 0;
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position + new Vector3(0f, 0f, 50f), _graphElement.m_rotation);
		this.m_shape = new ucpCircleShape(45f, Vector2.zero, 17895696U, 0f, 0f, 0f, (ucpCollisionType)10, true);
		this.m_body = ChipmunkProS.AddStaticBody(this.m_tc, this.m_shape, null);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_body, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, true);
		}
		else
		{
			Vector2[] circle = DebugDraw.GetCircle(45f, 32, Vector2.zero);
			PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_tc, Vector3.zero, circle, 4f, Color.yellow, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), CameraS.m_mainCamera, Position.Center, true);
		}
		if (this.m_minigame.m_editing)
		{
			this.CreateGraphElementTouchArea(45f, 45f, null, default(Vector2));
		}
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0001B4AC File Offset: 0x000198AC
	protected void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (_phase == ucpCollisionPhase.Begin)
		{
			this.m_collidingCount++;
			if (this.m_collidingCount == 1)
			{
				this.m_powerSource.Trigger();
			}
		}
		else if (_phase == ucpCollisionPhase.Separate)
		{
			if (this.m_collidingCount == 1)
			{
				this.m_powerSource.Trigger();
			}
			this.m_collidingCount--;
		}
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0001B515 File Offset: 0x00019915
	public override void SyncPositionToGraphElementPosition()
	{
		base.SyncPositionToGraphElementPosition();
		if (this.m_powerSource != null)
		{
			this.m_powerSource.UpdateLinkPositions();
		}
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0001B533 File Offset: 0x00019933
	public override void Update()
	{
		if (this.m_minigame.m_editing)
		{
		}
	}

	// Token: 0x04000229 RID: 553
	private const int RADIUS = 45;

	// Token: 0x0400022A RID: 554
	private ChipmunkBodyC m_body;

	// Token: 0x0400022B RID: 555
	private ucpShape m_shape;

	// Token: 0x0400022C RID: 556
	private TransformC m_tc;

	// Token: 0x0400022D RID: 557
	private LevelPowerLink m_powerSource;

	// Token: 0x0400022E RID: 558
	public int m_collidingCount;
}
