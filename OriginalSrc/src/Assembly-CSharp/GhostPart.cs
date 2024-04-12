using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public struct GhostPart
{
	// Token: 0x0600039D RID: 925 RVA: 0x00035BF4 File Offset: 0x00033FF4
	public GhostPart(GameObject _prefab, Vector3 _offset, string _nodeName, bool _flipX = false)
	{
		this.prefab = _prefab;
		this.offset = _offset;
		this.nodeName = _nodeName;
		this.flipX = _flipX;
	}

	// Token: 0x040004B5 RID: 1205
	public GameObject prefab;

	// Token: 0x040004B6 RID: 1206
	public Vector3 offset;

	// Token: 0x040004B7 RID: 1207
	public string nodeName;

	// Token: 0x040004B8 RID: 1208
	public bool flipX;
}
