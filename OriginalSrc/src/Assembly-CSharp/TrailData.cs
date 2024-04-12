using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class TrailData
{
	// Token: 0x060003ED RID: 1005 RVA: 0x00037E21 File Offset: 0x00036221
	public TrailData(Vector3 _point, float _age, float _mul, bool _boost)
	{
		this.m_point = _point;
		this.m_age = _age;
		this.m_mul = _mul;
		this.m_boost = _boost;
	}

	// Token: 0x0400050F RID: 1295
	public Vector3 m_point;

	// Token: 0x04000510 RID: 1296
	public float m_age;

	// Token: 0x04000511 RID: 1297
	public float m_mul;

	// Token: 0x04000512 RID: 1298
	public bool m_boost;
}
