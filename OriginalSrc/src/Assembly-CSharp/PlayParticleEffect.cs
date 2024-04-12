using System;
using UnityEngine;

// Token: 0x020001B4 RID: 436
public class PlayParticleEffect : MonoBehaviour
{
	// Token: 0x06000D85 RID: 3461 RVA: 0x0007EAE5 File Offset: 0x0007CEE5
	public void PlayEffect()
	{
		if (base.GetComponent<ParticleSystem>() != null)
		{
			base.GetComponent<ParticleSystem>().Play();
		}
	}
}
