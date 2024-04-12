using System;
using UnityEngine;

// Token: 0x020004F0 RID: 1264
public struct cpBB
{
	// Token: 0x0600236C RID: 9068 RVA: 0x001931E0 File Offset: 0x001915E0
	public cpBB(float _value)
	{
		this.l = _value;
		this.r = _value;
		this.t = _value;
		this.b = _value;
	}

	// Token: 0x0600236D RID: 9069 RVA: 0x001931FE File Offset: 0x001915FE
	public cpBB(float _l, float _b, float _r, float _t)
	{
		this.l = _l;
		this.b = _b;
		this.r = _r;
		this.t = _t;
	}

	// Token: 0x0600236E RID: 9070 RVA: 0x00193220 File Offset: 0x00191620
	public override string ToString()
	{
		return string.Concat(new object[] { "(", this.l, ", ", this.b, ", ", this.r, ", ", this.t, ")" });
	}

	// Token: 0x0600236F RID: 9071 RVA: 0x0019329C File Offset: 0x0019169C
	public bool isNull()
	{
		return this.l == 0f && this.r == 0f && this.b == 0f && this.t == 0f;
	}

	// Token: 0x06002370 RID: 9072 RVA: 0x001932E9 File Offset: 0x001916E9
	public Vector2 getCenter()
	{
		return new Vector2(this.l + (this.r - this.l) * 0.5f, this.t - (this.t - this.b) * 0.5f);
	}

	// Token: 0x06002371 RID: 9073 RVA: 0x00193324 File Offset: 0x00191724
	public Vector2 getDimensions()
	{
		return new Vector2(this.r - this.l, this.t - this.b);
	}

	// Token: 0x04002A52 RID: 10834
	public float l;

	// Token: 0x04002A53 RID: 10835
	public float b;

	// Token: 0x04002A54 RID: 10836
	public float r;

	// Token: 0x04002A55 RID: 10837
	public float t;
}
