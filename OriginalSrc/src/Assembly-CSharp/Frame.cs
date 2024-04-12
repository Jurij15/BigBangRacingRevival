using System;
using UnityEngine;

// Token: 0x020004CE RID: 1230
public class Frame
{
	// Token: 0x060022DE RID: 8926 RVA: 0x00191258 File Offset: 0x0018F658
	public Frame(float x, float y, float width, float height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		this.flipX = false;
		this.flipY = false;
		this.offset = Vector2.zero;
	}

	// Token: 0x060022DF RID: 8927 RVA: 0x00191296 File Offset: 0x0018F696
	public Frame(float x, float y, float width, float height, bool flipX, bool flipY)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		this.flipX = flipX;
		this.flipY = flipY;
		this.offset = Vector2.zero;
	}

	// Token: 0x060022E0 RID: 8928 RVA: 0x001912D6 File Offset: 0x0018F6D6
	public float GetWidthRelativeTo(float value)
	{
		return this.width / value;
	}

	// Token: 0x060022E1 RID: 8929 RVA: 0x001912E0 File Offset: 0x0018F6E0
	public float GetHeightRelativeTo(float value)
	{
		return this.height / value;
	}

	// Token: 0x0400292D RID: 10541
	public float x;

	// Token: 0x0400292E RID: 10542
	public float y;

	// Token: 0x0400292F RID: 10543
	public float width;

	// Token: 0x04002930 RID: 10544
	public float height;

	// Token: 0x04002931 RID: 10545
	public bool flipX;

	// Token: 0x04002932 RID: 10546
	public bool flipY;

	// Token: 0x04002933 RID: 10547
	public Vector2 offset;
}
