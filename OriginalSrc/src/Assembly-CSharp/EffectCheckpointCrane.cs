using System;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public class EffectCheckpointCrane : EffectCheckpoint
{
	// Token: 0x06000D4A RID: 3402 RVA: 0x0007E03D File Offset: 0x0007C43D
	public override void SetLocked()
	{
		this.checkpointAnimator.CrossFade("IdleDeactive", 0.1f);
		base.SetBorderState(false);
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x0007E05B File Offset: 0x0007C45B
	public override void Activate()
	{
		this.checkpointAnimator.CrossFade("Activate", 0.1f);
		base.SetBorderState(true);
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0007E079 File Offset: 0x0007C479
	public override void SetIdleActive()
	{
		this.checkpointAnimator.CrossFade("IdleActive", 0.1f);
		base.SetBorderState(true);
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0007E097 File Offset: 0x0007C497
	public override void Claim()
	{
		this.checkpointAnimator.CrossFade("Deactivate", 0.25f);
		base.SetBorderState(true);
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0007E0B5 File Offset: 0x0007C4B5
	public override void SetClaimed()
	{
		this.checkpointAnimator.CrossFade("IdleDeactive", 0.1f);
		base.SetBorderState(true);
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0007E0D3 File Offset: 0x0007C4D3
	public override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x04000E87 RID: 3719
	public Animator checkpointAnimator;
}
