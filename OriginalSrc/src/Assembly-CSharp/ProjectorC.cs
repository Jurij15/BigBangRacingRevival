using System;
using UnityEngine;

// Token: 0x020004CC RID: 1228
public class ProjectorC : BasicComponent
{
	// Token: 0x060022D8 RID: 8920 RVA: 0x00191138 File Offset: 0x0018F538
	public ProjectorC()
		: base(ComponentType.Projector)
	{
		ProjectorC.m_componentCount++;
	}

	// Token: 0x060022D9 RID: 8921 RVA: 0x0019114E File Offset: 0x0018F54E
	public override void Reset()
	{
		base.Reset();
		this.m_projector = null;
		this.m_offset = Vector3.zero;
		this.m_wasVisible = false;
		this.p_TC = null;
	}

	// Token: 0x060022DA RID: 8922 RVA: 0x00191178 File Offset: 0x0018F578
	~ProjectorC()
	{
		ProjectorC.m_componentCount--;
	}

	// Token: 0x0400291E RID: 10526
	public static int m_componentCount;

	// Token: 0x0400291F RID: 10527
	public bool m_wasVisible;

	// Token: 0x04002920 RID: 10528
	public TransformC p_TC;

	// Token: 0x04002921 RID: 10529
	public Projector m_projector;

	// Token: 0x04002922 RID: 10530
	public Vector3 m_offset;
}
