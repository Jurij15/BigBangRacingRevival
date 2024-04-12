using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public class VisualsRewardChest : MonoBehaviour
{
	// Token: 0x06000D7C RID: 3452 RVA: 0x0007E698 File Offset: 0x0007CA98
	public void SetToDeactiveState()
	{
		this.animController.SetTrigger("Deactive");
		this.backgroundGlow.Stop();
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x0007E6B8 File Offset: 0x0007CAB8
	public void SetToIdleState()
	{
		this.animController.SetTrigger("Idle");
		this.upSparks.Stop();
		this.sideSparksLeft.Stop();
		this.sideSparksRight.Stop();
		this.openRays.SetActive(false);
		this.sideRayleft.SetActive(false);
		this.sideRayRight.SetActive(false);
		if (this.backgroundGlow != null)
		{
			this.backgroundGlow.Stop();
		}
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0007E738 File Offset: 0x0007CB38
	public void SetToActiveState()
	{
		this.animController.SetTrigger("Activate");
		this.upSparks.Stop();
		this.sideSparksLeft.Play();
		this.sideSparksRight.Play();
		this.openRays.SetActive(false);
		this.sideRayleft.SetActive(true);
		this.sideRayRight.SetActive(true);
		if (this.backgroundGlow != null)
		{
			this.backgroundGlow.Play();
		}
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0007E7B8 File Offset: 0x0007CBB8
	public void OpenChest()
	{
		this.animController.SetTrigger("Open");
		this.openRays.SetActive(false);
		this.upSparks.Play();
		this.sideSparksLeft.Stop();
		this.sideSparksRight.Stop();
		this.sideRayleft.SetActive(false);
		this.sideRayRight.SetActive(false);
		this.rewardPlane.SetActive(true);
		if (this.backgroundGlow != null)
		{
			this.backgroundGlow.Stop();
		}
		base.StartCoroutine("BurstParticles");
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0007E850 File Offset: 0x0007CC50
	public void PopReward(bool lastReward)
	{
		if (!this.openRays.activeSelf)
		{
			this.openRays.SetActive(true);
		}
		if (this.backgroundGlow != null && lastReward)
		{
			this.backgroundGlow.Stop();
		}
		this.animController.SetTrigger("PopReward");
		base.StartCoroutine("BurstParticlesLite", lastReward);
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0007E8C0 File Offset: 0x0007CCC0
	public IEnumerator BurstParticles()
	{
		float particleDelay = 0.4f;
		while (particleDelay > 0f)
		{
			particleDelay -= Main.GetDeltaTime();
			yield return null;
		}
		this.sparkBurst.Play();
		yield break;
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0007E8DC File Offset: 0x0007CCDC
	public IEnumerator BurstParticlesLite(bool lastReward)
	{
		float particleDelay = 0.2f;
		while (particleDelay > 0f)
		{
			particleDelay -= Main.GetDeltaTime();
			yield return null;
		}
		this.sparkBurstLite.Play();
		if (lastReward)
		{
			this.openRays.SetActive(false);
			this.rewardPlane.SetActive(false);
			this.upSparks.Stop();
		}
		yield break;
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x0007E8FE File Offset: 0x0007CCFE
	public void ShowRays()
	{
		this.openRays.SetActive(true);
		this.upSparks.Play();
	}

	// Token: 0x04000E9C RID: 3740
	public Animator animController;

	// Token: 0x04000E9D RID: 3741
	public ParticleSystem sparkBurst;

	// Token: 0x04000E9E RID: 3742
	public ParticleSystem sparkBurstLite;

	// Token: 0x04000E9F RID: 3743
	public ParticleSystem upSparks;

	// Token: 0x04000EA0 RID: 3744
	public ParticleSystem sideSparksLeft;

	// Token: 0x04000EA1 RID: 3745
	public ParticleSystem sideSparksRight;

	// Token: 0x04000EA2 RID: 3746
	public ParticleSystem backgroundGlow;

	// Token: 0x04000EA3 RID: 3747
	public GameObject openRays;

	// Token: 0x04000EA4 RID: 3748
	public GameObject rewardPlane;

	// Token: 0x04000EA5 RID: 3749
	public GameObject sideRayleft;

	// Token: 0x04000EA6 RID: 3750
	public GameObject sideRayRight;
}
