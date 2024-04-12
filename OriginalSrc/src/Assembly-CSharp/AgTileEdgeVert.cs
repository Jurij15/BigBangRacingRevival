using System;
using UnityEngine;

// Token: 0x020004A4 RID: 1188
public struct AgTileEdgeVert
{
	// Token: 0x060021EC RID: 8684 RVA: 0x0018A122 File Offset: 0x00188522
	public AgTileEdgeVert(Vector2 _pos, Vector2 _normal, bool _isCorner = false)
	{
		this.pos = _pos;
		this.normal = _normal;
		this.fixedNormal = _normal;
		this.polyline = IntPtr.Zero;
		this.cTangent = Vector2.zero;
		this.wasTravelled = false;
		this.isCorner = _isCorner;
	}

	// Token: 0x04002811 RID: 10257
	public Vector2 pos;

	// Token: 0x04002812 RID: 10258
	public Vector2 normal;

	// Token: 0x04002813 RID: 10259
	public Vector2 fixedNormal;

	// Token: 0x04002814 RID: 10260
	public IntPtr polyline;

	// Token: 0x04002815 RID: 10261
	public Vector2 cTangent;

	// Token: 0x04002816 RID: 10262
	public bool wasTravelled;

	// Token: 0x04002817 RID: 10263
	public bool isCorner;
}
