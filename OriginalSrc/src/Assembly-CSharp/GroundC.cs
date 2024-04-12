using System;

// Token: 0x0200000C RID: 12
public class GroundC : BasicComponent
{
	// Token: 0x06000051 RID: 81 RVA: 0x000035C0 File Offset: 0x000019C0
	public GroundC()
		: base((ComponentType)32)
	{
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000035CA File Offset: 0x000019CA
	public override void Reset()
	{
		base.Reset();
		this.m_ground = null;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x000035D9 File Offset: 0x000019D9
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x0400002A RID: 42
	public Ground m_ground;
}
