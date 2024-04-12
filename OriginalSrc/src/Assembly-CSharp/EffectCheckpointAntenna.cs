using System;
using UnityEngine;

// Token: 0x020001A5 RID: 421
public class EffectCheckpointAntenna : EffectCheckpoint
{
	// Token: 0x06000D35 RID: 3381 RVA: 0x0007DC3C File Offset: 0x0007C03C
	public void SetTextureToIndex(int index)
	{
		this.instancedMaterial = this.meshRenderers[0].material;
		this.instancedMaterial.mainTexture = this.textures[index];
		for (int i = 0; i < this.meshRenderers.Length; i++)
		{
			this.meshRenderers[i].sharedMaterial = this.instancedMaterial;
		}
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0007DC9B File Offset: 0x0007C09B
	public override void SetLocked()
	{
		this.checkpointAnimator.CrossFade("IdleDeactive", 0.1f);
		base.SetBorderState(false);
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0007DCB9 File Offset: 0x0007C0B9
	public override void Activate()
	{
		this.checkpointAnimator.CrossFade("Activate", 0.1f);
		this.particlesA.Play();
		this.effectScreen.SetActive(true);
		this.SetTextureToIndex(0);
		base.SetBorderState(true);
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0007DCF5 File Offset: 0x0007C0F5
	public override void SetIdleActive()
	{
		this.checkpointAnimator.CrossFade("IdleActive", 0.1f);
		base.SetBorderState(true);
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0007DD13 File Offset: 0x0007C113
	public override void Claim()
	{
		this.checkpointAnimator.CrossFade("Deactivate", 0.25f);
		this.particlesA.Stop();
		this.effectScreen.SetActive(false);
		this.SetTextureToIndex(1);
		base.SetBorderState(true);
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0007DD4F File Offset: 0x0007C14F
	public override void SetClaimed()
	{
		this.checkpointAnimator.CrossFade("IdleDeactive", 0.1f);
		this.SetTextureToIndex(1);
		base.SetBorderState(true);
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0007DD74 File Offset: 0x0007C174
	public override void OnDestroy()
	{
		base.OnDestroy();
		Object.Destroy(this.instancedMaterial);
	}

	// Token: 0x04000E79 RID: 3705
	public Animator checkpointAnimator;

	// Token: 0x04000E7A RID: 3706
	public Texture[] textures;

	// Token: 0x04000E7B RID: 3707
	public ParticleSystem particlesA;

	// Token: 0x04000E7C RID: 3708
	public GameObject effectScreen;

	// Token: 0x04000E7D RID: 3709
	public MeshRenderer[] meshRenderers;

	// Token: 0x04000E7E RID: 3710
	private Material instancedMaterial;
}
