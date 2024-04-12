using System;

// Token: 0x0200000D RID: 13
public class UnitC : BasicComponent
{
	// Token: 0x06000054 RID: 84 RVA: 0x000035E1 File Offset: 0x000019E1
	public UnitC()
		: base((ComponentType)30)
	{
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000035EB File Offset: 0x000019EB
	public override void Reset()
	{
		base.Reset();
		this.m_unit = null;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000035FA File Offset: 0x000019FA
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x0400002B RID: 43
	public Unit m_unit;
}
