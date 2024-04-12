using System;
using UnityEngine;

// Token: 0x02000566 RID: 1382
public class GGVertex
{
	// Token: 0x06002861 RID: 10337 RVA: 0x001AD754 File Offset: 0x001ABB54
	public GGVertex Copy(bool _asDuplicate = false)
	{
		return new GGVertex
		{
			vertex = this.vertex,
			uv = this.uv,
			color = this.color,
			isDuplicate = _asDuplicate
		};
	}

	// Token: 0x04002D8C RID: 11660
	public Vector3 vertex;

	// Token: 0x04002D8D RID: 11661
	public Vector2 uv;

	// Token: 0x04002D8E RID: 11662
	public Color color = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04002D8F RID: 11663
	public bool isDuplicate;
}
