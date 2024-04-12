using System;
using System.Collections.Generic;

// Token: 0x0200012F RID: 303
public class PlanetRow
{
	// Token: 0x06000944 RID: 2372 RVA: 0x00063E6E File Offset: 0x0006226E
	public PlanetRow(int _rowId)
	{
		this.m_rowId = _rowId;
		this.m_nodes = new List<PsPlanetNode>();
	}

	// Token: 0x040008A5 RID: 2213
	public int m_rowId;

	// Token: 0x040008A6 RID: 2214
	public List<PsPlanetNode> m_nodes;
}
