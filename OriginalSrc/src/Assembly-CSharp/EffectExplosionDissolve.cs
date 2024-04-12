using System;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class EffectExplosionDissolve : MonoBehaviour
{
	// Token: 0x06000CD8 RID: 3288 RVA: 0x0007BCE1 File Offset: 0x0007A0E1
	private void Start()
	{
		this.tempMat = this.pRenderer.material;
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0007BCF4 File Offset: 0x0007A0F4
	private void Update()
	{
		if (!PsState.m_activeMinigame.m_gamePaused && this.pRenderer != null && this.dissolveTimer < this.dissolveSpeed)
		{
			this.dissolveTimer += Time.deltaTime;
			float num = this.dissolveTimer / this.dissolveSpeed;
			this.tempMat.SetFloat("_AlphaCutoff", num);
		}
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x0007BD63 File Offset: 0x0007A163
	private void OnDestroy()
	{
		Object.Destroy(this.tempMat);
	}

	// Token: 0x04000E03 RID: 3587
	public ParticleRenderer pRenderer;

	// Token: 0x04000E04 RID: 3588
	public float dissolveSpeed = 1f;

	// Token: 0x04000E05 RID: 3589
	private float dissolveTimer;

	// Token: 0x04000E06 RID: 3590
	private Material tempMat;
}
