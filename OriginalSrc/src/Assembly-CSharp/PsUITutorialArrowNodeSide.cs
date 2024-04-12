using System;
using System.Collections.Generic;

// Token: 0x02000263 RID: 611
public class PsUITutorialArrowNodeSide : PsUITutorialArrowNode
{
	// Token: 0x06001255 RID: 4693 RVA: 0x000B5326 File Offset: 0x000B3726
	public PsUITutorialArrowNodeSide()
		: base(0)
	{
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x000B5330 File Offset: 0x000B3730
	public override void GetTarget()
	{
		this.m_targetLoop = PsPlanetManager.GetCurrentPlanet().GetMainPath().GetCurrentSidePathNodeInfo();
		if (this.m_targetLoop != null && this.m_targetLoop.m_node != null)
		{
			this.m_newNode = null;
			List<PlanetRow> nodeRows = PsPlanetManager.GetCurrentPlanet().m_nodeRows;
			for (int i = 0; i < nodeRows.Count; i++)
			{
				for (int j = 0; j < nodeRows[i].m_nodes.Count; j++)
				{
					if (nodeRows[i].m_nodes[j].m_loop == this.m_targetLoop)
					{
						this.m_targetNode = nodeRows[i].m_nodes[j];
						Type type = this.m_targetLoop.m_node.GetType();
						this.m_newNode = Activator.CreateInstance(type, new object[] { this.m_targetLoop, true }) as PsPlanetNode;
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
			if (this.m_newNode != null)
			{
				base.GetTargetSprites(this.m_newNode.m_tc.p_entity);
			}
		}
	}
}
