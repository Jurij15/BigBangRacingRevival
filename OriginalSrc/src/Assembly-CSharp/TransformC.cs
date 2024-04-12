using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D6 RID: 1238
public class TransformC : BasicComponent
{
	// Token: 0x060022F1 RID: 8945 RVA: 0x0019164C File Offset: 0x0018FA4C
	public TransformC()
		: base(ComponentType.Transform)
	{
		this.transform = Object.Instantiate<GameObject>(TransformS.m_transformHelper).transform;
		this.childs = new List<TransformC>();
		TransformC.m_componentCount++;
	}

	// Token: 0x060022F2 RID: 8946 RVA: 0x001916A4 File Offset: 0x0018FAA4
	public override void Reset()
	{
		base.Reset();
		this.transform.name = "TransformComponent";
		this.transform.gameObject.SetActive(false);
		this.childs.Clear();
		this.forceRotation = false;
		this.forceScale = false;
		this.parent = null;
		base.m_active = false;
		this.updatedPosition = false;
		this.updatedRotation = false;
		this.updatedScale = false;
		this.updatePosition = true;
		this.updateRotation = true;
		this.updateScale = true;
		this.transform.localScale = Vector3.one;
		this.transform.position = Vector3.zero;
		this.transform.localRotation = Quaternion.identity;
		this.forcedRotation = Quaternion.identity;
		this.forcedScale = Vector3.one;
		this.level = 0;
		this.parentedToPhysics = false;
		this.delta = Vector3.zero;
		this.lastLocalPos = Vector3.zero;
	}

	// Token: 0x060022F3 RID: 8947 RVA: 0x00191793 File Offset: 0x0018FB93
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x0019179C File Offset: 0x0018FB9C
	~TransformC()
	{
		TransformC.m_componentCount--;
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x001917D4 File Offset: 0x0018FBD4
	public TransformC GetRoot()
	{
		TransformC transformC = this;
		while (transformC.parent != null)
		{
			transformC = transformC.parent;
		}
		return transformC;
	}

	// Token: 0x040029A8 RID: 10664
	public static int m_componentCount;

	// Token: 0x040029A9 RID: 10665
	public bool updatePosition;

	// Token: 0x040029AA RID: 10666
	public bool updateRotation;

	// Token: 0x040029AB RID: 10667
	public bool updateScale;

	// Token: 0x040029AC RID: 10668
	public bool updatedPosition;

	// Token: 0x040029AD RID: 10669
	public bool updatedRotation;

	// Token: 0x040029AE RID: 10670
	public bool updatedScale;

	// Token: 0x040029AF RID: 10671
	public TransformC parent;

	// Token: 0x040029B0 RID: 10672
	public List<TransformC> childs;

	// Token: 0x040029B1 RID: 10673
	public int level;

	// Token: 0x040029B2 RID: 10674
	public bool parentedToPhysics;

	// Token: 0x040029B3 RID: 10675
	public Vector3 lastLocalPos;

	// Token: 0x040029B4 RID: 10676
	public Vector3 delta;

	// Token: 0x040029B5 RID: 10677
	public bool forceRotation;

	// Token: 0x040029B6 RID: 10678
	public Quaternion forcedRotation = Quaternion.identity;

	// Token: 0x040029B7 RID: 10679
	public bool forceScale;

	// Token: 0x040029B8 RID: 10680
	public Vector3 forcedScale = Vector3.one;

	// Token: 0x040029B9 RID: 10681
	public Transform transform;
}
