using System;
using UnityEngine;

// Token: 0x020001D5 RID: 469
public class VisualsMotorcycle : MonoBehaviour, IVisualsVehicle
{
	// Token: 0x06000E12 RID: 3602 RVA: 0x0008372F File Offset: 0x00081B2F
	private void Awake()
	{
		this.dirtbikeAnimator = base.GetComponent<Animator>();
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x00083740 File Offset: 0x00081B40
	public void UpgradePop()
	{
		this.dirtbikeAnimator.SetTrigger("UpgradePop");
		this.collisionPlane.SetActive(true);
		foreach (ParticleSystem particleSystem in this.pSystems)
		{
			particleSystem.Play();
		}
	}

	// Token: 0x04001111 RID: 4369
	private Animator dirtbikeAnimator;

	// Token: 0x04001112 RID: 4370
	public GameObject collisionPlane;

	// Token: 0x04001113 RID: 4371
	public ParticleSystem[] pSystems;
}
