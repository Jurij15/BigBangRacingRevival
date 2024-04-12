using System;
using UnityEngine;

// Token: 0x0200019A RID: 410
[ExecuteInEditMode]
public class OLDTVScreen : MonoBehaviour
{
	// Token: 0x06000CFC RID: 3324 RVA: 0x0007D0D0 File Offset: 0x0007B4D0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.tvMaterialScreen.SetFloat("_Saturation", this.screenSaturation);
		this.tvMaterialScreen.SetFloat("_ChromaticAberrationMagnetude", this.chromaticAberrationMagnetude);
		this.tvMaterialScreen.SetTextureOffset("_NoiseTex", new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
		this.tvMaterialScreen.SetFloat("_NoiseMagnetude", this.noiseMagnetude);
		this.tvMaterialScreen.SetTextureOffset("_StaticTex", new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
		this.tvMaterialScreen.SetFloat("_StaticMagnetude", this.staticMagnetude);
		Graphics.Blit(source, destination, this.tvMaterialScreen);
	}

	// Token: 0x04000E4D RID: 3661
	public Material tvMaterialScreen;

	// Token: 0x04000E4E RID: 3662
	public float screenSaturation;

	// Token: 0x04000E4F RID: 3663
	public Texture chromaticAberrationPattern;

	// Token: 0x04000E50 RID: 3664
	public float chromaticAberrationMagnetude = 0.015f;

	// Token: 0x04000E51 RID: 3665
	public Texture noisePattern;

	// Token: 0x04000E52 RID: 3666
	public float noiseMagnetude = 0.1f;

	// Token: 0x04000E53 RID: 3667
	public Texture staticPattern;

	// Token: 0x04000E54 RID: 3668
	public Texture staticMask;

	// Token: 0x04000E55 RID: 3669
	public float staticMagnetude = 0.015f;
}
