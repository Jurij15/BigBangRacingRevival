using System;
using UnityEngine;

// Token: 0x020004D5 RID: 1237
public class TouchAreaC : BasicComponent
{
	// Token: 0x060022EF RID: 8943 RVA: 0x00191578 File Offset: 0x0018F978
	public TouchAreaC()
		: base(ComponentType.TouchArea)
	{
		this.m_clipBB = default(cpBB);
	}

	// Token: 0x060022F0 RID: 8944 RVA: 0x0019159C File Offset: 0x0018F99C
	public override void Reset()
	{
		base.Reset();
		this.m_allowPrimary = true;
		this.m_allowSecondary = false;
		this.m_letTouchesThrough = false;
		this.m_cancelOtherTouches = true;
		this.m_ignoreDepth = false;
		this.m_maxTouches = 1;
		this.m_touchCount = 0;
		this.m_clip = false;
		this.m_isDragged = false;
		this.m_wasDragged = false;
		this.m_customComponent = null;
		this.m_customObject = null;
		this.m_delegatedCount = 0;
		this.d_TouchEventDelegate = null;
		if (Screen.height < Screen.width)
		{
			this.m_dragThreshold = 0.025f * (float)Screen.height;
		}
		else
		{
			this.m_dragThreshold = 0.025f * (float)Screen.width;
		}
	}

	// Token: 0x04002992 RID: 10642
	public TransformC m_TC;

	// Token: 0x04002993 RID: 10643
	public MeshCollider m_collider;

	// Token: 0x04002994 RID: 10644
	public ColliderShape m_colliderShape;

	// Token: 0x04002995 RID: 10645
	public Camera m_camera;

	// Token: 0x04002996 RID: 10646
	public bool m_allowPrimary;

	// Token: 0x04002997 RID: 10647
	public bool m_allowSecondary;

	// Token: 0x04002998 RID: 10648
	public bool m_letTouchesThrough;

	// Token: 0x04002999 RID: 10649
	public bool m_cancelOtherTouches;

	// Token: 0x0400299A RID: 10650
	public bool m_ignoreDepth;

	// Token: 0x0400299B RID: 10651
	public bool m_isDragged;

	// Token: 0x0400299C RID: 10652
	public bool m_wasDragged;

	// Token: 0x0400299D RID: 10653
	public float m_dragThreshold;

	// Token: 0x0400299E RID: 10654
	public Vector2 m_dragOffset;

	// Token: 0x0400299F RID: 10655
	public string m_name;

	// Token: 0x040029A0 RID: 10656
	public int m_maxTouches;

	// Token: 0x040029A1 RID: 10657
	public int m_touchCount;

	// Token: 0x040029A2 RID: 10658
	public IComponent m_customComponent;

	// Token: 0x040029A3 RID: 10659
	public object m_customObject;

	// Token: 0x040029A4 RID: 10660
	public int m_delegatedCount;

	// Token: 0x040029A5 RID: 10661
	public TouchEventDelegate d_TouchEventDelegate;

	// Token: 0x040029A6 RID: 10662
	public bool m_clip;

	// Token: 0x040029A7 RID: 10663
	public cpBB m_clipBB;
}
