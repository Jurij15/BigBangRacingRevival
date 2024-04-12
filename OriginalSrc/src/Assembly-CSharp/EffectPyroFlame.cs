using System;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class EffectPyroFlame : MonoBehaviour
{
	// Token: 0x06000CE5 RID: 3301 RVA: 0x0007C328 File Offset: 0x0007A728
	public void PlayEffect()
	{
		this.tubeRenderer.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0f));
		this.tubeAnimator.SetTrigger("Tube");
		this.confetti.Play();
		this.confettiB.Play();
		this.confettiC.Play();
		this.confettiGrouped.Play();
		this.lid.Play();
		this.smoke.Play();
		SoundS.PlaySingleShot("/Ingame/Units/Decorations/DecorationPyrotechnicsConfetti", base.transform.position, 1f);
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0007C3C5 File Offset: 0x0007A7C5
	private void Update()
	{
	}

	// Token: 0x04000E1B RID: 3611
	public float animationLength = 1f;

	// Token: 0x04000E1C RID: 3612
	public Renderer tubeRenderer;

	// Token: 0x04000E1D RID: 3613
	public ParticleSystem confetti;

	// Token: 0x04000E1E RID: 3614
	public ParticleSystem confettiB;

	// Token: 0x04000E1F RID: 3615
	public ParticleSystem confettiC;

	// Token: 0x04000E20 RID: 3616
	public ParticleSystem confettiGrouped;

	// Token: 0x04000E21 RID: 3617
	public ParticleSystem lid;

	// Token: 0x04000E22 RID: 3618
	public ParticleSystem smoke;

	// Token: 0x04000E23 RID: 3619
	public Animator tubeAnimator;
}
