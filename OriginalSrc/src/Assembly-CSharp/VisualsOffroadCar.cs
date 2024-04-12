using System;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class VisualsOffroadCar : MonoBehaviour, IVisualsVehicle
{
	// Token: 0x06000E23 RID: 3619 RVA: 0x00083C42 File Offset: 0x00082042
	private void Awake()
	{
		this.offroadCarAnimator = base.GetComponent<Animator>();
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x00083C50 File Offset: 0x00082050
	public void UpgradePop()
	{
		this.offroadCarAnimator.SetTrigger("UpgradePop");
		this.collisionPlane.SetActive(true);
		foreach (ParticleSystem particleSystem in this.pSystems)
		{
			particleSystem.Play();
		}
	}

	// Token: 0x04001122 RID: 4386
	private Animator offroadCarAnimator;

	// Token: 0x04001123 RID: 4387
	public GameObject collisionPlane;

	// Token: 0x04001124 RID: 4388
	public ParticleSystem[] pSystems;
}
