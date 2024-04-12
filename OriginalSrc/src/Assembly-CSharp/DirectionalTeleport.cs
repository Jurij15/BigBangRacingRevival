using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class DirectionalTeleport : TeleportBase
{
	// Token: 0x06000243 RID: 579 RVA: 0x0001D31C File Offset: 0x0001B71C
	public DirectionalTeleport(GraphElement _graphElement)
		: base(_graphElement, 75f, string.Empty, 0f)
	{
		ucpPolyShape ucpPolyShape = new ucpPolyShape(this.m_radius * 2f, 10f, new Vector2(0f, -(this.m_radius + 5f)), 257U, 1f, 0.1f, 0.5f, (ucpCollisionType)4, false);
		ChipmunkProS.AddKinematicBody(this.m_node1TC, ucpPolyShape, null);
		ChipmunkProS.AddKinematicBody(this.m_node2TC, ucpPolyShape, null);
		ucpPolyShape = new ucpPolyShape(10f, this.m_radius * 2f, new Vector2(-(this.m_radius + 5f), 0f), 257U, 1f, 0.1f, 0.5f, (ucpCollisionType)4, false);
		ChipmunkProS.AddKinematicBody(this.m_node1TC, ucpPolyShape, null);
		ChipmunkProS.AddKinematicBody(this.m_node2TC, ucpPolyShape, null);
		ucpPolyShape = new ucpPolyShape(10f, this.m_radius * 2f, new Vector2(this.m_radius + 5f, 0f), 257U, 1f, 0.1f, 0.5f, (ucpCollisionType)4, false);
		ChipmunkProS.AddKinematicBody(this.m_node1TC, ucpPolyShape, null);
		ChipmunkProS.AddKinematicBody(this.m_node2TC, ucpPolyShape, null);
		this.m_node1.m_isRotateable = true;
		this.m_node2.m_isRotateable = true;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0001D478 File Offset: 0x0001B878
	public override void Update()
	{
		base.Update();
		DebugDraw.CreateLine(CameraS.m_mainCamera, this.m_node1TC, Vector3.zero, Vector3.up * (this.m_radius + 25f));
		DebugDraw.CreateBox(CameraS.m_mainCamera, this.m_node1TC, new Vector3(0f, -(this.m_radius + 5f)), this.m_radius * 2f, 10f, false);
		DebugDraw.CreateBox(CameraS.m_mainCamera, this.m_node1TC, new Vector3(this.m_radius + 5f, 0f), 10f, this.m_radius * 2f, false);
		DebugDraw.CreateBox(CameraS.m_mainCamera, this.m_node1TC, new Vector3(-(this.m_radius + 5f), 0f), 10f, this.m_radius * 2f, false);
		DebugDraw.Clear(CameraS.m_mainCamera, this.m_node2TC);
		DebugDraw.CreateLine(CameraS.m_mainCamera, this.m_node2TC, Vector3.zero, Vector3.up * (this.m_radius + 25f));
		DebugDraw.CreateBox(CameraS.m_mainCamera, this.m_node2TC, new Vector3(0f, -(this.m_radius + 5f)), this.m_radius * 2f, 10f, false);
		DebugDraw.CreateBox(CameraS.m_mainCamera, this.m_node2TC, new Vector3(this.m_radius + 5f, 0f), 10f, this.m_radius * 2f, false);
		DebugDraw.CreateBox(CameraS.m_mainCamera, this.m_node2TC, new Vector3(-(this.m_radius + 5f), 0f), 10f, this.m_radius * 2f, false);
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0001D648 File Offset: 0x0001BA48
	public override void TeleportCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin && !unitC.m_unit.m_teleported)
		{
			if (_pair.shapeA == this.m_node1Shape && Vector3.Angle(_pair.point - this.m_node1.m_position, this.m_node1.m_TC.transform.up) <= 45f)
			{
				unitC.m_unit.SetAsTeleporting(15, this.m_node1.m_TC.transform, this.m_node2.m_TC.transform, false, true, false, true);
			}
			else if (_pair.shapeA == this.m_node2Shape && Vector3.Angle(_pair.point - this.m_node2.m_position, this.m_node2.m_TC.transform.up) <= 45f)
			{
				unitC.m_unit.SetAsTeleporting(15, this.m_node2.m_TC.transform, this.m_node1.m_TC.transform, false, true, false, true);
			}
		}
	}
}
