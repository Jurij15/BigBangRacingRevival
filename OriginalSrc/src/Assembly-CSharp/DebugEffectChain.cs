using System;
using UnityEngine;

// Token: 0x02000186 RID: 390
public class DebugEffectChain : MonoBehaviour
{
	// Token: 0x06000CB1 RID: 3249 RVA: 0x0007AB70 File Offset: 0x00078F70
	private void EvaluateEffect()
	{
		int num = this.particleSystems.Length;
		float num2 = 1f / (float)num;
		for (int i = 0; i < this.particleSystems.Length; i++)
		{
			if (this.currentSystem != this.particleSystems[i] && num2 * (float)i <= this.range && num2 * (float)(i + 1) > this.range)
			{
				if (this.currentSystem != null)
				{
					this.currentSystem.Stop();
				}
				this.particleSystems[i].Play();
				Debug.Log("Playing effect " + this.particleSystems[i], null);
				this.currentSystem = this.particleSystems[i];
				break;
			}
		}
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x0007AC34 File Offset: 0x00079034
	private void Update()
	{
		this.EvaluateEffect();
	}

	// Token: 0x04000DBC RID: 3516
	private ParticleSystem currentSystem;

	// Token: 0x04000DBD RID: 3517
	public ParticleSystem[] particleSystems;

	// Token: 0x04000DBE RID: 3518
	[Range(0f, 1f)]
	public float range;
}
