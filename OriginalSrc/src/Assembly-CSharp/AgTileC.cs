using System;

// Token: 0x020004A5 RID: 1189
public class AgTileC : BasicComponent
{
	// Token: 0x060021ED RID: 8685 RVA: 0x0018A15D File Offset: 0x0018855D
	public AgTileC()
		: base(ComponentType.None)
	{
		this.Reset();
	}

	// Token: 0x060021EE RID: 8686 RVA: 0x0018A16D File Offset: 0x0018856D
	public override void Reset()
	{
		base.Reset();
	}

	// Token: 0x060021EF RID: 8687 RVA: 0x0018A175 File Offset: 0x00188575
	public override void Destroy()
	{
		base.Destroy();
		this.m_tile = null;
	}

	// Token: 0x04002818 RID: 10264
	public AgTile m_tile;
}
