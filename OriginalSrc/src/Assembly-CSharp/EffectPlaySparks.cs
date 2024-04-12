using System;
using UnityEngine;

// Token: 0x020001AC RID: 428
public class EffectPlaySparks : MonoBehaviour
{
	// Token: 0x06000D63 RID: 3427 RVA: 0x0007E2C8 File Offset: 0x0007C6C8
	public void PlaySparks()
	{
		this.activateSparks.Play();
	}

	// Token: 0x04000E94 RID: 3732
	public ParticleSystem activateSparks;
}
