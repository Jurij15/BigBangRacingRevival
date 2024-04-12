using System;
using UnityEngine;

// Token: 0x0200051B RID: 1307
public class GifCamera : MonoBehaviour
{
	// Token: 0x06002678 RID: 9848 RVA: 0x001A5FDD File Offset: 0x001A43DD
	private void Start()
	{
	}

	// Token: 0x06002679 RID: 9849 RVA: 0x001A5FDF File Offset: 0x001A43DF
	private void Update()
	{
	}

	// Token: 0x0600267A RID: 9850 RVA: 0x001A5FE1 File Offset: 0x001A43E1
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (this.m_gifTexture != null)
		{
			Graphics.Blit(src, this.m_gifTexture);
		}
		Graphics.Blit(src, dest);
		this.m_gifTexture = null;
	}

	// Token: 0x04002BE0 RID: 11232
	public RenderTexture m_gifTexture;
}
