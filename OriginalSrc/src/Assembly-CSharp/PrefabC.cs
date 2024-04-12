using System;
using UnityEngine;

// Token: 0x020004CB RID: 1227
public class PrefabC : BasicComponent
{
	// Token: 0x060022D4 RID: 8916 RVA: 0x001910B8 File Offset: 0x0018F4B8
	public PrefabC()
		: base(ComponentType.Prefab)
	{
		PrefabC.m_componentCount++;
	}

	// Token: 0x060022D5 RID: 8917 RVA: 0x001910CD File Offset: 0x0018F4CD
	public override void Reset()
	{
		base.Reset();
		this.p_mesh = null;
		this.p_parentTC = null;
		this.p_gameObject = null;
		this.p_animators = null;
		this.p_animatorInfos = null;
	}

	// Token: 0x060022D6 RID: 8918 RVA: 0x001910F8 File Offset: 0x0018F4F8
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x060022D7 RID: 8919 RVA: 0x00191100 File Offset: 0x0018F500
	~PrefabC()
	{
		PrefabC.m_componentCount--;
	}

	// Token: 0x04002916 RID: 10518
	public static int m_componentCount;

	// Token: 0x04002917 RID: 10519
	public string m_name;

	// Token: 0x04002918 RID: 10520
	public bool m_wasVisible;

	// Token: 0x04002919 RID: 10521
	public GameObject p_gameObject;

	// Token: 0x0400291A RID: 10522
	public Mesh p_mesh;

	// Token: 0x0400291B RID: 10523
	public TransformC p_parentTC;

	// Token: 0x0400291C RID: 10524
	public Animator[] p_animators;

	// Token: 0x0400291D RID: 10525
	public LastAnimatorStateInfo[] p_animatorInfos;
}
