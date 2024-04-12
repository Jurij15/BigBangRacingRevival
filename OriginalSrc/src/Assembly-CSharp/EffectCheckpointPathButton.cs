using System;
using UnityEngine;

// Token: 0x020001AB RID: 427
public class EffectCheckpointPathButton : EffectCheckpoint
{
	// Token: 0x06000D59 RID: 3417 RVA: 0x0007E209 File Offset: 0x0007C609
	public override void ShowObject()
	{
		base.ShowObject();
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0007E211 File Offset: 0x0007C611
	public override void HideObject()
	{
		base.HideObject();
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0007E219 File Offset: 0x0007C619
	public override void Activate()
	{
		this.activeObject.SetActive(true);
		this.deactiveObject.SetActive(false);
		base.SetBorderState(true);
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0007E23A File Offset: 0x0007C63A
	public override void Claim()
	{
		this.activeObject.SetActive(false);
		this.deactiveObject.SetActive(true);
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0007E254 File Offset: 0x0007C654
	public override void SetIdleActive()
	{
		this.Activate();
		base.SetBorderState(true);
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0007E263 File Offset: 0x0007C663
	public override void SetLocked()
	{
		this.Claim();
		base.SetBorderState(false);
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x0007E272 File Offset: 0x0007C672
	public override void SetClaimed()
	{
		this.Activate();
		base.SetBorderState(true);
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0007E281 File Offset: 0x0007C681
	public override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0007E289 File Offset: 0x0007C689
	public override void Hide()
	{
		this.activeObject.SetActive(false);
		this.deactiveObject.SetActive(false);
		if (this.insideObject != null)
		{
			this.insideObject.SetActive(false);
		}
	}

	// Token: 0x04000E92 RID: 3730
	public GameObject activeObject;

	// Token: 0x04000E93 RID: 3731
	public GameObject deactiveObject;
}
