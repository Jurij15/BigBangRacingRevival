using System;
using System.Collections.Generic;

// Token: 0x02000134 RID: 308
public class PsUnlockableCategory : PsUnlockable
{
	// Token: 0x06000961 RID: 2401 RVA: 0x00064CC2 File Offset: 0x000630C2
	public PsUnlockableCategory(string _categoryName)
		: base(PsUnlockableType.Undefined, _categoryName)
	{
		this.m_items = new List<PsUnlockable>();
	}

	// Token: 0x040008C2 RID: 2242
	public List<PsUnlockable> m_items;
}
