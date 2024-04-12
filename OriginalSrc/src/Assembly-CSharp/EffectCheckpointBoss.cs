using System;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class EffectCheckpointBoss : EffectCheckpoint
{
	// Token: 0x06000D3D RID: 3389 RVA: 0x0007DD8F File Offset: 0x0007C18F
	public override void Activate()
	{
		this.checkpointAnimator.Play("IdleActive");
		base.SetBorderState(true);
		this.boneObjects.SetActive(false);
		this.skullObject.SetActive(true);
		this.skullSmoke.Play();
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0007DDCB File Offset: 0x0007C1CB
	public override void SetIdleActive()
	{
		this.checkpointAnimator.Play("IdleActive");
		base.SetBorderState(true);
		this.boneObjects.SetActive(false);
		this.skullObject.SetActive(true);
		this.skullSmoke.Play();
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0007DE07 File Offset: 0x0007C207
	public override void Claim()
	{
		this.checkpointAnimator.SetTrigger("open");
		base.SetBorderState(true);
		this.boneObjects.SetActive(false);
		this.skullObject.SetActive(true);
		this.skullSmoke.Stop();
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0007DE43 File Offset: 0x0007C243
	public override void SetLocked()
	{
		this.checkpointAnimator.Play("IdleActive");
		base.SetBorderState(false);
		this.boneObjects.SetActive(false);
		this.skullObject.SetActive(true);
		this.skullSmoke.Stop();
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x0007DE80 File Offset: 0x0007C280
	public override void SetClaimed()
	{
		this.checkpointAnimator.Play("Deactivate", 0, 1f);
		base.SetBorderState(true);
		this.boneObjects.SetActive(true);
		this.skullObject.SetActive(false);
		this.skullSmoke.Stop();
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0007DECD File Offset: 0x0007C2CD
	public void ExplodeSkull()
	{
		this.skullExplosion.Play();
		this.boneObjects.SetActive(true);
		this.skullObject.SetActive(false);
	}

	// Token: 0x04000E7F RID: 3711
	public Animator checkpointAnimator;

	// Token: 0x04000E80 RID: 3712
	public GameObject skullObject;

	// Token: 0x04000E81 RID: 3713
	public GameObject boneObjects;

	// Token: 0x04000E82 RID: 3714
	public ParticleSystem skullSmoke;

	// Token: 0x04000E83 RID: 3715
	public ParticleSystem skullExplosion;
}
