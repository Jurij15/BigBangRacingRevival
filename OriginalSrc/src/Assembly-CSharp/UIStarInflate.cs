using System;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class UIStarInflate : MonoBehaviour
{
	// Token: 0x06000D87 RID: 3463 RVA: 0x0007EB0B File Offset: 0x0007CF0B
	private void Start()
	{
		this.iMaterial = this.blendMesh.material;
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x0007EB20 File Offset: 0x0007CF20
	public void Reset()
	{
		this.blendMesh.enabled = true;
		this.blendMesh.SetBlendShapeWeight(0, 0f);
		this.iMaterial.SetColor("_Color", Color.white);
		if (this.starExplosion != null)
		{
			this.starExplosion.Stop();
		}
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0007EB7C File Offset: 0x0007CF7C
	public void SetInflationAmount(float amount)
	{
		float num = amount * 100f;
		if (num < 0f)
		{
			num = 0f;
		}
		if (num >= 100f)
		{
			this.blendMesh.enabled = false;
			this.exploded = true;
			if (this.starExplosion != null)
			{
				this.starExplosion.Play();
				SoundS.PlaySingleShot("/Ingame/Events/LoseStar", Vector2.zero, 1f);
			}
		}
		else
		{
			this.blendMesh.SetBlendShapeWeight(0, num);
			if (this.iMaterial != null)
			{
				this.iMaterial.SetColor("_Color", Color.Lerp(new Color(1f, 1f, 1f, 1f), this.endColor, amount));
			}
		}
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0007EC4D File Offset: 0x0007D04D
	private void OnDestroy()
	{
		Object.Destroy(this.iMaterial);
	}

	// Token: 0x04000FFB RID: 4091
	public SkinnedMeshRenderer blendMesh;

	// Token: 0x04000FFC RID: 4092
	public ParticleSystem starExplosion;

	// Token: 0x04000FFD RID: 4093
	public Color endColor;

	// Token: 0x04000FFE RID: 4094
	public bool exploded;

	// Token: 0x04000FFF RID: 4095
	private Material iMaterial;
}
