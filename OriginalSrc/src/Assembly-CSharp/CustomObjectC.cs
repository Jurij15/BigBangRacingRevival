using System;

// Token: 0x0200000B RID: 11
public class CustomObjectC : BasicComponent
{
	// Token: 0x0600004E RID: 78 RVA: 0x0000359F File Offset: 0x0000199F
	public CustomObjectC()
		: base((ComponentType)31)
	{
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000035A9 File Offset: 0x000019A9
	public override void Reset()
	{
		base.Reset();
		this.m_customObject = null;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000035B8 File Offset: 0x000019B8
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x04000029 RID: 41
	public object m_customObject;
}
