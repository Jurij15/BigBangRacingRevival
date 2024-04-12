using System;
using UnityEngine;

// Token: 0x020001CA RID: 458
public class EffectGravitationSwitch : MonoBehaviour
{
	// Token: 0x06000DE1 RID: 3553 RVA: 0x00082763 File Offset: 0x00080B63
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x00082774 File Offset: 0x00080B74
	public bool Toggle()
	{
		if (!this.isEnabled)
		{
			this.isEnabled = true;
			this.coilLightning.Play();
			this.animator.SetTrigger("Toggle");
		}
		else
		{
			this.isEnabled = false;
			this.coilLightning.Stop();
			this.animator.SetTrigger("Toggle");
		}
		return this.isEnabled;
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x000827DC File Offset: 0x00080BDC
	public void ToggleButton()
	{
		if (!this.isEnabled)
		{
			this.switchUp.SetActive(false);
			this.switchDown.SetActive(true);
		}
		else
		{
			this.switchUp.SetActive(true);
			this.switchDown.SetActive(false);
		}
	}

	// Token: 0x040010D1 RID: 4305
	public GameObject switchUp;

	// Token: 0x040010D2 RID: 4306
	public GameObject switchDown;

	// Token: 0x040010D3 RID: 4307
	public ParticleSystem coilLightning;

	// Token: 0x040010D4 RID: 4308
	private Animator animator;

	// Token: 0x040010D5 RID: 4309
	private bool isEnabled;
}
