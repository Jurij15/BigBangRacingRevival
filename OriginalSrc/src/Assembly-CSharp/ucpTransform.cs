using System;

// Token: 0x020004F1 RID: 1265
internal struct ucpTransform
{
	// Token: 0x06002372 RID: 9074 RVA: 0x00193345 File Offset: 0x00191745
	public ucpTransform(float _tx, float _ty)
	{
		this.a = 1f;
		this.b = 0f;
		this.c = 0f;
		this.d = 1f;
		this.tx = _tx;
		this.ty = _ty;
	}

	// Token: 0x06002373 RID: 9075 RVA: 0x00193384 File Offset: 0x00191784
	public void SetIdentity()
	{
		this.a = 1f;
		this.b = 0f;
		this.c = 0f;
		this.d = 1f;
		this.tx = 0f;
		this.ty = 0f;
	}

	// Token: 0x04002A56 RID: 10838
	public float a;

	// Token: 0x04002A57 RID: 10839
	public float b;

	// Token: 0x04002A58 RID: 10840
	public float c;

	// Token: 0x04002A59 RID: 10841
	public float d;

	// Token: 0x04002A5A RID: 10842
	public float tx;

	// Token: 0x04002A5B RID: 10843
	public float ty;
}
