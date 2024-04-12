using System;

// Token: 0x02000568 RID: 1384
public class UVRect
{
	// Token: 0x06002863 RID: 10339 RVA: 0x001AD79B File Offset: 0x001ABB9B
	public UVRect(float _left, float _bottom, float _width, float _height)
	{
		this.left = _left;
		this.bottom = _bottom;
		this.width = _width;
		this.height = _height;
	}

	// Token: 0x06002864 RID: 10340 RVA: 0x001AD7C0 File Offset: 0x001ABBC0
	public static UVRect Normal()
	{
		return new UVRect(0f, 0f, 1f, 1f);
	}

	// Token: 0x04002D96 RID: 11670
	public float left;

	// Token: 0x04002D97 RID: 11671
	public float bottom;

	// Token: 0x04002D98 RID: 11672
	public float width;

	// Token: 0x04002D99 RID: 11673
	public float height;
}
