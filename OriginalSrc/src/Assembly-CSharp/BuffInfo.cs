using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class BuffInfo
{
	// Token: 0x0600032A RID: 810 RVA: 0x00030194 File Offset: 0x0002E594
	public BuffInfo(Buff _buff, IAssembledClass _source, IAssembledClass _target, Vector2 _contactPoint)
	{
		this.m_buff = _buff;
		this.m_new = true;
		this.m_source = _source;
		this.m_target = _target;
		this.m_contactPoint = _contactPoint;
		this.m_nextTick = -1f;
		this.m_lastTick = -1f;
	}

	// Token: 0x04000415 RID: 1045
	public Buff m_buff;

	// Token: 0x04000416 RID: 1046
	public bool m_new;

	// Token: 0x04000417 RID: 1047
	public IAssembledClass m_source;

	// Token: 0x04000418 RID: 1048
	public IAssembledClass m_target;

	// Token: 0x04000419 RID: 1049
	public Vector2 m_contactPoint;

	// Token: 0x0400041A RID: 1050
	public float m_nextTick;

	// Token: 0x0400041B RID: 1051
	public float m_lastTick;
}
