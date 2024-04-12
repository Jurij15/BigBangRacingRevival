using System;
using UnityEngine;

// Token: 0x020004ED RID: 1261
public struct ucpCollisionPair
{
	// Token: 0x04002A3F RID: 10815
	public int ucpComponentIndexA;

	// Token: 0x04002A40 RID: 10816
	public int ucpComponentIndexB;

	// Token: 0x04002A41 RID: 10817
	public IntPtr shapeA;

	// Token: 0x04002A42 RID: 10818
	public IntPtr shapeB;

	// Token: 0x04002A43 RID: 10819
	public uint typeA;

	// Token: 0x04002A44 RID: 10820
	public uint typeB;

	// Token: 0x04002A45 RID: 10821
	public Vector2 point;

	// Token: 0x04002A46 RID: 10822
	public Vector2 normal;

	// Token: 0x04002A47 RID: 10823
	public Vector2 impulse;

	// Token: 0x04002A48 RID: 10824
	public Vector2 friction;

	// Token: 0x04002A49 RID: 10825
	public float totalKE;
}
