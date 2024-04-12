using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004C7 RID: 1223
public class ChipmunkBodyC : BasicComponent
{
	// Token: 0x060022BF RID: 8895 RVA: 0x00190C3D File Offset: 0x0018F03D
	public ChipmunkBodyC()
		: base(ComponentType.ChipmunkBody)
	{
		this.shapes = new List<ChipmunkBodyShape>();
	}

	// Token: 0x060022C0 RID: 8896 RVA: 0x00190C54 File Offset: 0x0018F054
	public override void Reset()
	{
		base.Reset();
		this.shapes.Clear();
		this.m_angularDamp = 1f;
		this.m_linearDamp = Vector2.one;
		this.m_isDisabled = false;
		this.m_updateStaticVisuals = true;
		this.body = IntPtr.Zero;
		this.customComponent = null;
		this.customObject = null;
		this.m_isDynamic = false;
		this.m_isKinematic = false;
		this.m_isStatic = false;
		this.m_collisionDelegates = new CollisionDelegate[ChipmunkProS.m_collisionTypeCount, 3];
		this.m_collisionDelegateTypes = new ucpCollisionType[ChipmunkProS.m_collisionTypeCount, 3];
	}

	// Token: 0x060022C1 RID: 8897 RVA: 0x00190CE8 File Offset: 0x0018F0E8
	~ChipmunkBodyC()
	{
	}

	// Token: 0x040028E6 RID: 10470
	public TransformC TC;

	// Token: 0x040028E7 RID: 10471
	public IntPtr body;

	// Token: 0x040028E8 RID: 10472
	public List<ChipmunkBodyShape> shapes;

	// Token: 0x040028E9 RID: 10473
	public float m_mass;

	// Token: 0x040028EA RID: 10474
	public float m_moment;

	// Token: 0x040028EB RID: 10475
	public float m_angularDamp;

	// Token: 0x040028EC RID: 10476
	public Vector2 m_linearDamp;

	// Token: 0x040028ED RID: 10477
	public Vector2 m_savedGravity;

	// Token: 0x040028EE RID: 10478
	public bool m_isDisabled;

	// Token: 0x040028EF RID: 10479
	public bool m_updateStaticVisuals;

	// Token: 0x040028F0 RID: 10480
	public bool m_isSleeping;

	// Token: 0x040028F1 RID: 10481
	public bool m_isDynamic;

	// Token: 0x040028F2 RID: 10482
	public bool m_isKinematic;

	// Token: 0x040028F3 RID: 10483
	public bool m_isStatic;

	// Token: 0x040028F4 RID: 10484
	public CollisionDelegate[,] m_collisionDelegates;

	// Token: 0x040028F5 RID: 10485
	public ucpCollisionType[,] m_collisionDelegateTypes;

	// Token: 0x040028F6 RID: 10486
	public IComponent customComponent;

	// Token: 0x040028F7 RID: 10487
	public object customObject;
}
