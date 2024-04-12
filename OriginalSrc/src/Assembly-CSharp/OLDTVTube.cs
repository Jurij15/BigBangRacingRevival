using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200019B RID: 411
[ExecuteInEditMode]
public class OLDTVTube : MonoBehaviour
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000CFE RID: 3326 RVA: 0x0007D1F4 File Offset: 0x0007B5F4
	// (remove) Token: 0x06000CFF RID: 3327 RVA: 0x0007D22C File Offset: 0x0007B62C
	[field: DebuggerBrowsable(0)]
	public event OLDTVTube.RepaintAction WantRepaint;

	// Token: 0x06000D00 RID: 3328 RVA: 0x0007D262 File Offset: 0x0007B662
	private void Repaint()
	{
		if (this.WantRepaint != null)
		{
			this.WantRepaint();
		}
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0007D27C File Offset: 0x0007B67C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		float num = 1f;
		bool flag = true;
		if (Screen.orientation == 3 || Screen.orientation == 3 || Screen.orientation == 4)
		{
			if (flag)
			{
				num = 0f;
			}
			this.tvMaterialTube.SetFloat("_ScreenHorizontal", num);
		}
		else
		{
			if (flag)
			{
				num = 1f;
			}
			this.tvMaterialTube.SetFloat("_ScreenHorizontal", num);
		}
		this.tvMaterialTube.SetFloat("_ReflexMagnetude", this.reflexMagnetude);
		this.tvMaterialTube.SetFloat("_Distortion", this.radialDistortion);
		this.tvMaterialTube.SetFloat("_Saturation", this.screenSaturation);
		this.tvMaterialTube.SetFloat("_ChromaticAberrationMagnetude", this.chromaticAberrationMagnetude);
		this.tvMaterialTube.SetTextureOffset("_NoiseTex", new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
		this.tvMaterialTube.SetFloat("_NoiseMagnetude", this.noiseMagnetude);
		this.tvMaterialTube.SetTextureOffset("_StaticTex", new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
		this.tvMaterialTube.SetFloat("_StaticMagnetude", this.staticMagnetude);
		Graphics.Blit(source, destination, this.tvMaterialTube);
	}

	// Token: 0x04000E56 RID: 3670
	public Material tvMaterialTube;

	// Token: 0x04000E57 RID: 3671
	public Texture mask;

	// Token: 0x04000E58 RID: 3672
	public Texture reflex;

	// Token: 0x04000E59 RID: 3673
	public float reflexMagnetude = 0.5f;

	// Token: 0x04000E5A RID: 3674
	public float radialDistortion = 0.2f;

	// Token: 0x04000E5B RID: 3675
	public float screenSaturation;

	// Token: 0x04000E5C RID: 3676
	public Texture chromaticAberrationPattern;

	// Token: 0x04000E5D RID: 3677
	public float chromaticAberrationMagnetude = 0.015f;

	// Token: 0x04000E5E RID: 3678
	public Texture noisePattern;

	// Token: 0x04000E5F RID: 3679
	public float noiseMagnetude = 0.1f;

	// Token: 0x04000E60 RID: 3680
	public Texture staticPattern;

	// Token: 0x04000E61 RID: 3681
	public Texture staticMask;

	// Token: 0x04000E62 RID: 3682
	public float staticMagnetude = 0.015f;

	// Token: 0x0200019C RID: 412
	// (Invoke) Token: 0x06000D03 RID: 3331
	public delegate void RepaintAction();
}
