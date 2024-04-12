using System;

// Token: 0x020004EB RID: 1259
public struct ucpShapeFilter
{
	// Token: 0x0600236A RID: 9066 RVA: 0x001931B2 File Offset: 0x001915B2
	public void ucpShapeFilterAll()
	{
		this.group = 0U;
		this.categories = uint.MaxValue;
		this.mask = uint.MaxValue;
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x001931C9 File Offset: 0x001915C9
	public void ucpShapeFilterNone()
	{
		this.group = 0U;
		this.categories = 0U;
		this.mask = 0U;
	}

	// Token: 0x04002A3A RID: 10810
	public uint group;

	// Token: 0x04002A3B RID: 10811
	public uint categories;

	// Token: 0x04002A3C RID: 10812
	public uint mask;
}
