using System;
using UnityEngine;

// Token: 0x0200051F RID: 1311
public class GifListener : MonoBehaviour
{
	// Token: 0x060026A2 RID: 9890 RVA: 0x001A7781 File Offset: 0x001A5B81
	private void Start()
	{
	}

	// Token: 0x060026A3 RID: 9891 RVA: 0x001A7783 File Offset: 0x001A5B83
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		GifMaker.CaptureFrame(src);
	}
}
