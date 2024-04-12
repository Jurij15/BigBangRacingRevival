using System;
using UnityEngine;

// Token: 0x020004D0 RID: 1232
public class TextMeshC : BasicComponent
{
	// Token: 0x060022E5 RID: 8933 RVA: 0x00191460 File Offset: 0x0018F860
	public TextMeshC()
		: base(ComponentType.TextMesh)
	{
	}

	// Token: 0x060022E6 RID: 8934 RVA: 0x00191469 File Offset: 0x0018F869
	public override void Reset()
	{
		base.Reset();
		this.m_textboxWidth = 0f;
		this.m_textboxHeight = 0f;
		this.m_textWidth = 0f;
		this.m_textHeight = 0f;
	}

	// Token: 0x04002959 RID: 10585
	public bool m_wasVisible;

	// Token: 0x0400295A RID: 10586
	public GameObject m_go;

	// Token: 0x0400295B RID: 10587
	public Renderer m_renderer;

	// Token: 0x0400295C RID: 10588
	public TextMesh m_textMesh;

	// Token: 0x0400295D RID: 10589
	public TransformC m_TC;

	// Token: 0x0400295E RID: 10590
	public float m_textboxWidth;

	// Token: 0x0400295F RID: 10591
	public float m_textboxHeight;

	// Token: 0x04002960 RID: 10592
	public float m_textHeight;

	// Token: 0x04002961 RID: 10593
	public float m_textWidth;

	// Token: 0x04002962 RID: 10594
	public Align m_horizontalAlign;

	// Token: 0x04002963 RID: 10595
	public Align m_verticalAlign;
}
