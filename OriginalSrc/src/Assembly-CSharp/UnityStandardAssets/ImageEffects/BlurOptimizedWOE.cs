using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000198 RID: 408
	public class BlurOptimizedWOE : PostEffectsBase
	{
		// Token: 0x06000CF8 RID: 3320 RVA: 0x0007CE03 File Offset: 0x0007B203
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.blurMaterial = base.CheckShaderAndCreateMaterial(this.blurShader, this.blurMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0007CE3C File Offset: 0x0007B23C
		public void OnDisable()
		{
			if (this.blurMaterial)
			{
				Object.DestroyImmediate(this.blurMaterial);
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0007CE5C File Offset: 0x0007B25C
		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (!this.update && !this.forceRefresh)
			{
				if (this.done && this.storedTex != null)
				{
					Graphics.Blit(this.storedTex, destination);
				}
				return;
			}
			float num = 1f / (1f * (float)(1 << this.downsample));
			this.blurMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num, -this.blurSize * num, this.blurSize, 0f));
			source.filterMode = 1;
			int num2 = source.width >> this.downsample;
			int num3 = source.height >> this.downsample;
			if (this.storedTex == null)
			{
				RenderTextureFormat renderTextureFormat = 4;
				this.storedTex = new RenderTexture(num2, num3, 0, renderTextureFormat);
				this.storedTex.filterMode = 1;
				CameraS.m_renderTextureViewMaterial.mainTexture = this.storedTex;
			}
			RenderTexture renderTexture = RenderTexture.GetTemporary(num2, num3, 0, source.format);
			renderTexture.filterMode = 1;
			Graphics.Blit(source, renderTexture, this.blurMaterial, 0);
			int num4 = ((this.blurType != BlurOptimizedWOE.BlurType.StandardGauss) ? 2 : 0);
			for (int i = 0; i < this.blurIterations; i++)
			{
				float num5 = (float)i * 1f;
				this.blurMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num + num5, -this.blurSize * num - num5, this.blurSize, 0f));
				RenderTexture renderTexture2 = RenderTexture.GetTemporary(num2, num3, 0, source.format);
				renderTexture2.filterMode = 1;
				Graphics.Blit(renderTexture, renderTexture2, this.blurMaterial, 1 + num4);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = renderTexture2;
				renderTexture2 = RenderTexture.GetTemporary(num2, num3, 0, source.format);
				renderTexture2.filterMode = 1;
				Graphics.Blit(renderTexture, renderTexture2, this.blurMaterial, 2 + num4);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = renderTexture2;
			}
			Graphics.Blit(renderTexture, destination);
			Graphics.Blit(renderTexture, this.storedTex);
			RenderTexture.ReleaseTemporary(renderTexture);
			if (this.forceRefresh)
			{
				this.forceRefresh = false;
				this.hideSpaceScene = false;
			}
		}

		// Token: 0x04000E3F RID: 3647
		public bool update;

		// Token: 0x04000E40 RID: 3648
		public bool done;

		// Token: 0x04000E41 RID: 3649
		public bool hideSpaceScene;

		// Token: 0x04000E42 RID: 3650
		private bool forceRefresh;

		// Token: 0x04000E43 RID: 3651
		[Range(0f, 2f)]
		public int downsample = 2;

		// Token: 0x04000E44 RID: 3652
		[Range(0f, 10f)]
		public float blurSize = 3f;

		// Token: 0x04000E45 RID: 3653
		[Range(1f, 4f)]
		public int blurIterations = 2;

		// Token: 0x04000E46 RID: 3654
		public BlurOptimizedWOE.BlurType blurType;

		// Token: 0x04000E47 RID: 3655
		public Shader blurShader;

		// Token: 0x04000E48 RID: 3656
		private Material blurMaterial;

		// Token: 0x04000E49 RID: 3657
		public RenderTexture storedTex;

		// Token: 0x02000199 RID: 409
		public enum BlurType
		{
			// Token: 0x04000E4B RID: 3659
			StandardGauss,
			// Token: 0x04000E4C RID: 3660
			SgxGauss
		}
	}
}
