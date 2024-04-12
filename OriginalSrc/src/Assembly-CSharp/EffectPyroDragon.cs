using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class EffectPyroDragon : MonoBehaviour
{
	// Token: 0x06000CE3 RID: 3299 RVA: 0x0007C298 File Offset: 0x0007A698
	public void PlayEffect()
	{
		this.tubeRenderer.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0f));
		this.dragonAnimator.SetTrigger("Dragon");
		this.sparks.Play();
		this.smoke.Play();
		this.fireBall.Play();
		SoundS.PlaySingleShot("/Ingame/Units/Decorations/DecorationPyrotechnicsFire", base.transform.position, 1f);
	}

	// Token: 0x04000E14 RID: 3604
	public float animationLength = 1f;

	// Token: 0x04000E15 RID: 3605
	public Renderer flameRenderer;

	// Token: 0x04000E16 RID: 3606
	public Renderer tubeRenderer;

	// Token: 0x04000E17 RID: 3607
	public ParticleSystem sparks;

	// Token: 0x04000E18 RID: 3608
	public ParticleSystem smoke;

	// Token: 0x04000E19 RID: 3609
	public ParticleSystem fireBall;

	// Token: 0x04000E1A RID: 3610
	public Animator dragonAnimator;
}
