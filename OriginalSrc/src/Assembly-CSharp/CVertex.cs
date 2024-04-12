using System;
using UnityEngine;

// Token: 0x020004AF RID: 1199
public struct CVertex
{
	// Token: 0x06002244 RID: 8772 RVA: 0x0018E026 File Offset: 0x0018C426
	public CVertex(Vector3 _vert, Vector2 _uv, Color _color, Vector3 _normal, bool _isDuplicate = false)
	{
		this.vert = _vert;
		this.uv = _uv;
		this.color = _color;
		this.isDuplicate = _isDuplicate;
		this.normal = _normal;
	}

	// Token: 0x04002879 RID: 10361
	public Vector3 vert;

	// Token: 0x0400287A RID: 10362
	public Vector2 uv;

	// Token: 0x0400287B RID: 10363
	public Color color;

	// Token: 0x0400287C RID: 10364
	public Vector3 normal;

	// Token: 0x0400287D RID: 10365
	public bool isDuplicate;
}
