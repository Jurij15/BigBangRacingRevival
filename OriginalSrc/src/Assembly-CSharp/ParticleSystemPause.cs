using System;
using UnityEngine;

// Token: 0x02000181 RID: 385
public class ParticleSystemPause : MonoBehaviour
{
	// Token: 0x06000CA6 RID: 3238 RVA: 0x0007A9B8 File Offset: 0x00078DB8
	private void Start()
	{
		this.pSystem = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x0007A9C8 File Offset: 0x00078DC8
	private void Update()
	{
		if (PsState.m_physicsPaused && !this.pSystemPaused)
		{
			this.pSystem.Pause();
			this.pSystemPaused = true;
		}
		else if (!PsState.m_physicsPaused && this.pSystemPaused)
		{
			this.pSystem.Play();
			this.pSystemPaused = false;
		}
	}

	// Token: 0x04000DB3 RID: 3507
	private ParticleSystem pSystem;

	// Token: 0x04000DB4 RID: 3508
	private bool pSystemPaused;
}
