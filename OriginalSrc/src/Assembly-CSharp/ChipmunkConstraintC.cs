using System;

// Token: 0x020004C8 RID: 1224
public class ChipmunkConstraintC : BasicComponent
{
	// Token: 0x060022C2 RID: 8898 RVA: 0x00190D14 File Offset: 0x0018F114
	public ChipmunkConstraintC()
		: base(ComponentType.ChipmunkConstraint)
	{
	}

	// Token: 0x060022C3 RID: 8899 RVA: 0x00190D1D File Offset: 0x0018F11D
	public override void Reset()
	{
		base.Reset();
		this.grooveA = null;
		this.grooveB = null;
		this.anchor1 = null;
		this.anchor2 = null;
		this.addedToSpace = true;
	}

	// Token: 0x060022C4 RID: 8900 RVA: 0x00190D48 File Offset: 0x0018F148
	~ChipmunkConstraintC()
	{
	}

	// Token: 0x040028F8 RID: 10488
	public IntPtr constraint;

	// Token: 0x040028F9 RID: 10489
	public ucpConstraintType type;

	// Token: 0x040028FA RID: 10490
	public ChipmunkBodyC bodyA;

	// Token: 0x040028FB RID: 10491
	public ChipmunkBodyC bodyB;

	// Token: 0x040028FC RID: 10492
	public TransformC grooveA;

	// Token: 0x040028FD RID: 10493
	public TransformC grooveB;

	// Token: 0x040028FE RID: 10494
	public TransformC anchor1;

	// Token: 0x040028FF RID: 10495
	public TransformC anchor2;

	// Token: 0x04002900 RID: 10496
	public bool addedToSpace;
}
