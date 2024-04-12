using System;
using UnityEngine;

// Token: 0x02000187 RID: 391
public class DebugParticleModules : MonoBehaviour
{
	// Token: 0x06000CB4 RID: 3252 RVA: 0x0007AC44 File Offset: 0x00079044
	private void Awake()
	{
		this.pSystem = base.GetComponent<ParticleSystem>();
		this.emissionModule = this.pSystem.emission;
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x0007AC64 File Offset: 0x00079064
	private void Update()
	{
		if (Input.GetKeyDown(32))
		{
			if (!this.trigger)
			{
				this.emissionModule.type = 1;
				this.trigger = true;
			}
			else if (this.trigger)
			{
				this.emissionModule.type = 0;
				this.trigger = false;
			}
		}
	}

	// Token: 0x04000DBF RID: 3519
	private ParticleSystem pSystem;

	// Token: 0x04000DC0 RID: 3520
	private ParticleSystem.EmissionModule emissionModule;

	// Token: 0x04000DC1 RID: 3521
	private bool trigger;
}
