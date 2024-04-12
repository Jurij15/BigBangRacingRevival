using System;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public class UnscaledTimeParticle : MonoBehaviour
{
	// Token: 0x06000D76 RID: 3446 RVA: 0x0007E62F File Offset: 0x0007CA2F
	private void Update()
	{
		if (Time.timeScale < 1f)
		{
			base.GetComponent<ParticleSystem>().Simulate(Main.m_dt, true, false);
		}
	}
}
