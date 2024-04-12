using System;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class EffectCheckpointGarage : EffectCheckpoint
{
	// Token: 0x06000D51 RID: 3409 RVA: 0x0007E0E4 File Offset: 0x0007C4E4
	public void SetTextureToIndex(int index)
	{
		this.instancedMaterial = this.meshRenderers[0].material;
		this.instancedMaterial.mainTexture = this.textures[index];
		for (int i = 0; i < this.meshRenderers.Length; i++)
		{
			this.meshRenderers[i].sharedMaterial = this.instancedMaterial;
		}
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0007E143 File Offset: 0x0007C543
	public override void SetLocked()
	{
		this.checkpointAnimator.CrossFade("IdleDeactive", 0.1f);
		base.SetBorderState(false);
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x0007E161 File Offset: 0x0007C561
	public override void Activate()
	{
		this.checkpointAnimator.CrossFade("Activate", 0.1f);
		this.SetTextureToIndex(0);
		base.SetBorderState(true);
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x0007E186 File Offset: 0x0007C586
	public override void SetIdleActive()
	{
		this.checkpointAnimator.CrossFade("IdleActive", 0.1f);
		base.SetBorderState(true);
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x0007E1A4 File Offset: 0x0007C5A4
	public override void Claim()
	{
		this.checkpointAnimator.CrossFade("Deactivate", 0.1f);
		this.SetTextureToIndex(1);
		base.SetBorderState(true);
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x0007E1C9 File Offset: 0x0007C5C9
	public override void SetClaimed()
	{
		this.checkpointAnimator.CrossFade("IdleDeactive", 0.1f);
		this.SetTextureToIndex(1);
		base.SetBorderState(true);
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x0007E1EE File Offset: 0x0007C5EE
	public override void OnDestroy()
	{
		base.OnDestroy();
		Object.Destroy(this.instancedMaterial);
	}

	// Token: 0x04000E88 RID: 3720
	public Animator checkpointAnimator;

	// Token: 0x04000E89 RID: 3721
	public Texture[] textures;

	// Token: 0x04000E8A RID: 3722
	public MeshRenderer[] meshRenderers;

	// Token: 0x04000E8B RID: 3723
	private Material instancedMaterial;
}
