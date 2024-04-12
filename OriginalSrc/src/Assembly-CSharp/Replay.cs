using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000545 RID: 1349
public class Replay
{
	// Token: 0x060027AF RID: 10159 RVA: 0x001AA5DE File Offset: 0x001A89DE
	public Replay(string _levelId)
	{
		this.m_levelId = _levelId;
		this.m_data = new Hashtable();
		this.m_items = new List<ReplayItem>();
	}

	// Token: 0x04002D17 RID: 11543
	public string m_levelId;

	// Token: 0x04002D18 RID: 11544
	public Hashtable m_data;

	// Token: 0x04002D19 RID: 11545
	public List<ReplayItem> m_items;
}
