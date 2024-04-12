using System;
using UnityEngine;

// Token: 0x02000534 RID: 1332
public class GraphConnectionVisual
{
	// Token: 0x06002736 RID: 10038 RVA: 0x00008840 File Offset: 0x00006C40
	public GraphConnectionVisual(TransformC _parent, Vector3 _start, Vector3 _end)
	{
		this.m_parentTC = _parent;
		this.m_entity = EntityManager.AddEntity();
		this.m_TC = TransformS.AddComponent(this.m_entity, "GizmoConnection");
		TransformS.ParentComponent(this.m_TC, this.m_parentTC);
		TransformS.SetPosition(this.m_TC, Vector3.zero);
		this.CreateConnectionVisual(_start, _end);
	}

	// Token: 0x06002737 RID: 10039 RVA: 0x000088A4 File Offset: 0x00006CA4
	public virtual void CreateConnectionVisual(Vector3 _start, Vector3 _end)
	{
		this.m_TC.transform.gameObject.layer = 8;
		this.m_line = this.m_TC.transform.gameObject.AddComponent<LineRenderer>();
		this.m_line.positionCount = 2;
		this.m_line.startWidth = 5f;
		this.m_line.endWidth = 5f;
		this.m_line.SetPositions(new Vector3[] { _start, _end });
		this.m_line.SetColors(Color.white, Color.white);
	}

	// Token: 0x06002738 RID: 10040 RVA: 0x0000894E File Offset: 0x00006D4E
	public virtual void SetEndPosition(Vector3 _endPos)
	{
		this.m_line.SetPosition(1, _endPos);
	}

	// Token: 0x06002739 RID: 10041 RVA: 0x0000895D File Offset: 0x00006D5D
	public virtual void Destroy()
	{
		if (this.m_line != null)
		{
			Object.Destroy(this.m_line);
		}
		EntityManager.RemoveEntity(this.m_entity);
	}

	// Token: 0x04002CAA RID: 11434
	private LineRenderer m_line;

	// Token: 0x04002CAB RID: 11435
	protected TransformC m_TC;

	// Token: 0x04002CAC RID: 11436
	protected TransformC m_parentTC;

	// Token: 0x04002CAD RID: 11437
	protected Entity m_entity;
}
