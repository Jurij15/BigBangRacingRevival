using System;
using System.Collections.Generic;

// Token: 0x02000220 RID: 544
public class PsMetagameDataHelper
{
	// Token: 0x06000FAC RID: 4012 RVA: 0x00093F17 File Offset: 0x00092317
	public PsMetagameDataHelper()
	{
		this.m_inputs = new List<PsMetagameDataHelper>();
		this.m_outputs = new List<PsMetagameDataHelper>();
	}

	// Token: 0x04001265 RID: 4709
	public int m_id;

	// Token: 0x04001266 RID: 4710
	public string m_name;

	// Token: 0x04001267 RID: 4711
	public float m_x;

	// Token: 0x04001268 RID: 4712
	public float m_y;

	// Token: 0x04001269 RID: 4713
	public MetagameNodeData m_metagameNodeData;

	// Token: 0x0400126A RID: 4714
	public List<PsMetagameDataHelper> m_inputs;

	// Token: 0x0400126B RID: 4715
	public List<PsMetagameDataHelper> m_outputs;
}
