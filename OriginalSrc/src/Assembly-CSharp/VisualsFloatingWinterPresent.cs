using System;
using UnityEngine;

// Token: 0x0200019E RID: 414
public class VisualsFloatingWinterPresent : MonoBehaviour
{
	// Token: 0x06000D09 RID: 3337 RVA: 0x0007D406 File Offset: 0x0007B806
	public void Activate()
	{
		this.activeEffect.Play();
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0007D413 File Offset: 0x0007B813
	public void SetPresentColor(int color)
	{
		if (this.presentMaterials.Length > 0)
		{
			color = Mathf.Min(Mathf.Max(color, 0), this.presentMaterials.Length - 1);
			this.presentRenderer.material = this.presentMaterials[color];
		}
	}

	// Token: 0x04000E64 RID: 3684
	public Renderer presentRenderer;

	// Token: 0x04000E65 RID: 3685
	public Material[] presentMaterials;

	// Token: 0x04000E66 RID: 3686
	public ParticleSystem activeEffect;
}
