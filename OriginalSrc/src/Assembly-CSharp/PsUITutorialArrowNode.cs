using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000262 RID: 610
public class PsUITutorialArrowNode : PsUITutorialArrow
{
	// Token: 0x06001250 RID: 4688 RVA: 0x000B4FEC File Offset: 0x000B33EC
	public PsUITutorialArrowNode(int _nodeId)
		: base(string.Empty, string.Empty, _nodeId)
	{
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x000B5000 File Offset: 0x000B3400
	public override void GetTarget()
	{
		this.m_nodeId = this.m_id;
		List<PsGameLoop> row = PsPlanetManager.GetCurrentPlanet().GetRow(this.m_nodeId);
		foreach (PsGameLoop psGameLoop in row)
		{
			if (psGameLoop.m_nodeId == this.m_nodeId)
			{
				this.m_targetLoop = psGameLoop;
				break;
			}
		}
		if (this.m_targetLoop != null)
		{
			this.m_newNode = new PsPlanetLevelNode(this.m_targetLoop, true);
			List<PlanetRow> nodeRows = PsPlanetManager.GetCurrentPlanet().m_nodeRows;
			for (int i = 0; i < nodeRows.Count; i++)
			{
				for (int j = 0; j < nodeRows[i].m_nodes.Count; j++)
				{
					if (nodeRows[i].m_nodes[j].m_loop.m_nodeId == this.m_nodeId)
					{
						this.m_targetNode = nodeRows[i].m_nodes[j];
						this.m_goTarget = this.m_newNode.m_prefab.p_gameObject.transform.parent.gameObject;
						this.m_touchArea = this.m_newNode.m_tac;
						this.hasTarget = true;
						this.m_transfound = true;
						break;
					}
				}
				if (this.hasTarget)
				{
					break;
				}
			}
			base.GetTargetSprites(this.m_newNode.m_tc.p_entity);
		}
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000B51A8 File Offset: 0x000B35A8
	public override void Step()
	{
		if (this.m_destroy)
		{
			this.Destroy();
			return;
		}
		if (this.hasTarget)
		{
			if (this.m_targetLoop == null)
			{
				this.hasTarget = false;
				TouchAreaS.RemoveTouchCameraFilter();
				this.m_canvas.Destroy();
			}
			else if (this.m_3Dcamera != null)
			{
				Vector3 vector = this.m_3Dcamera.WorldToViewportPoint(this.m_goTarget.transform.position);
				this.offset.SetAlign(vector.x, vector.y);
				this.offset.UpdateAlign();
				if (vector.x < 0f || vector.x > 1f || vector.y < 0f || vector.y > 1f)
				{
					this.m_destroy = true;
				}
			}
		}
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x000B5295 File Offset: 0x000B3695
	public override void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchCount == 1 && _touchPhase == TouchAreaPhase.ReleaseIn && !_touches[0].m_dragged)
		{
			TouchAreaS.RemoveTouchCameraFilter();
			Debug.LogWarning("touchi: " + _touchArea.m_name);
			this.Destroy();
		}
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x000B52D4 File Offset: 0x000B36D4
	public override void Destroy()
	{
		if (this.m_newNode != null)
		{
			this.m_newNode.Destroy();
			this.m_newNode = null;
		}
		this.m_targetLoop.m_node = this.m_targetNode;
		this.m_targetNode.m_loop = this.m_targetLoop;
		base.Destroy();
	}

	// Token: 0x0400157E RID: 5502
	protected int m_nodeId;

	// Token: 0x0400157F RID: 5503
	protected PsGameLoop m_targetLoop;

	// Token: 0x04001580 RID: 5504
	protected PsPlanetNode m_targetNode;

	// Token: 0x04001581 RID: 5505
	protected PsPlanetNode m_newNode;
}
