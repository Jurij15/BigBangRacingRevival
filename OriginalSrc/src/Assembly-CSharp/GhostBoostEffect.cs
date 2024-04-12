using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public struct GhostBoostEffect
{
	// Token: 0x0600039E RID: 926 RVA: 0x00035C13 File Offset: 0x00034013
	public GhostBoostEffect(GameObject _prefab, Vector3 _offset, string _nodeName)
	{
		this.prefab = _prefab;
		this.offset = _offset;
		this.nodeName = _nodeName;
	}

	// Token: 0x040004B9 RID: 1209
	public GameObject prefab;

	// Token: 0x040004BA RID: 1210
	public Vector3 offset;

	// Token: 0x040004BB RID: 1211
	public string nodeName;
}
