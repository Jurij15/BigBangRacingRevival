using System;
using UnityEngine;

// Token: 0x020001A7 RID: 423
public class EffectCheckpointChest : EffectCheckpoint
{
	// Token: 0x06000D44 RID: 3396 RVA: 0x0007DEFA File Offset: 0x0007C2FA
	public override void Activate()
	{
		this.checkpointAnimator.CrossFade("Activate", 0.1f);
		base.SetBorderState(true);
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0007DF18 File Offset: 0x0007C318
	public override void SetIdleActive()
	{
		this.checkpointAnimator.Play("IdleActive");
		if (this.particleSystemA != null)
		{
			this.particleSystemA.Stop();
		}
		base.SetBorderState(true);
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0007DF50 File Offset: 0x0007C350
	public override void Claim()
	{
		this.checkpointAnimator.SetTrigger("open");
		if (this.particleSystemA != null)
		{
			this.particleSystemA.Stop();
		}
		if (this.particleSystemB != null)
		{
			this.particleSystemB.Play();
		}
		base.SetBorderState(true);
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x0007DFAC File Offset: 0x0007C3AC
	public override void SetLocked()
	{
		this.checkpointAnimator.Play("IdleDeactive");
		if (this.particleSystemA != null)
		{
			this.particleSystemA.Stop();
		}
		base.SetBorderState(false);
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0007DFE4 File Offset: 0x0007C3E4
	public override void SetClaimed()
	{
		this.checkpointAnimator.Play("Deactivate", 0, 1f);
		if (this.particleSystemA != null)
		{
			this.particleSystemA.transform.gameObject.SetActive(false);
		}
		base.SetBorderState(true);
	}

	// Token: 0x04000E84 RID: 3716
	public Animator checkpointAnimator;

	// Token: 0x04000E85 RID: 3717
	public ParticleSystem particleSystemA;

	// Token: 0x04000E86 RID: 3718
	public ParticleSystem particleSystemB;
}
