using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001A0 RID: 416
public class MenuCharacter : MonoBehaviour
{
	// Token: 0x06000D0F RID: 3343 RVA: 0x0007D4B5 File Offset: 0x0007B8B5
	public virtual void Talk()
	{
		this.charAnimator.SetTrigger("Talk");
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0007D4C7 File Offset: 0x0007B8C7
	public virtual void Poke()
	{
		this.charAnimator.SetTrigger("Poke");
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0007D4D9 File Offset: 0x0007B8D9
	public void LookAtCamera()
	{
		if (this.eyeLids != null)
		{
			base.StartCoroutine("LookAtCameraCR");
		}
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0007D4F8 File Offset: 0x0007B8F8
	public void LookAwayFromCamera()
	{
		if (this.eyeLids != null)
		{
			base.StartCoroutine("LookAwayFromCameraCR");
		}
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0007D518 File Offset: 0x0007B918
	public void Activate()
	{
		this.sleepZ.Stop();
		this.charAnimator.SetTrigger("Poke");
		MeshRenderer[] componentsInChildren = base.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			if (meshRenderer.gameObject.name != "AlphaMask")
			{
				meshRenderer.material.shader = Shader.Find("WOE/Units/Transparent-Unlit");
			}
		}
		if (this.eyeLids != null)
		{
			this.eyeLids.SetActive(false);
		}
		if (this.eyeLidsHalf != null)
		{
			this.eyeLidsHalf.SetActive(true);
		}
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0007D5CC File Offset: 0x0007B9CC
	public void Deactivate()
	{
		if (this.sleepZ != null)
		{
			this.sleepZ.Play();
		}
		this.isSleeping = true;
		if (this.eyeLids != null)
		{
			this.eyeLids.SetActive(true);
		}
		if (this.eyeLidsHalf != null)
		{
			this.eyeLidsHalf.SetActive(false);
		}
		MeshRenderer[] componentsInChildren = base.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			if (meshRenderer.gameObject.name != "AlphaMask")
			{
				meshRenderer.material.shader = Shader.Find("WOE/Fx/GreyscaleUnlitAlphaZ");
			}
		}
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0007D688 File Offset: 0x0007BA88
	private IEnumerator BlinkCR()
	{
		float blinkCounter = 0.1f;
		this.eyeLids.SetActive(true);
		while (blinkCounter >= 0f)
		{
			blinkCounter -= Main.GetDeltaTime();
			yield return null;
		}
		this.eyeLids.SetActive(false);
		yield break;
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0007D6A4 File Offset: 0x0007BAA4
	private IEnumerator LookAtCameraCR()
	{
		float blinkCounter = 0.1f;
		this.eyeLids.SetActive(true);
		this.eyesForward.SetActive(false);
		this.eyesCamera.SetActive(true);
		while (blinkCounter >= 0f)
		{
			blinkCounter -= Main.GetDeltaTime();
			yield return null;
		}
		this.eyeLids.SetActive(false);
		yield break;
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0007D6C0 File Offset: 0x0007BAC0
	private IEnumerator LookAwayFromCameraCR()
	{
		float blinkCounter = 0.1f;
		this.eyeLids.SetActive(true);
		this.eyesForward.SetActive(true);
		this.eyesCamera.SetActive(false);
		while (blinkCounter >= 0f)
		{
			blinkCounter -= Main.GetDeltaTime();
			yield return null;
		}
		this.eyeLids.SetActive(false);
		yield break;
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0007D6DC File Offset: 0x0007BADC
	private void Update()
	{
		if (this.eyeLids != null && !this.isSleeping)
		{
			if (this.blinkTimer <= 0f)
			{
				this.blinkTimer = Random.Range(this.minBlink, this.maxBlink);
				base.StartCoroutine("BlinkCR");
			}
			else
			{
				this.blinkTimer -= Main.GetDeltaTime();
			}
		}
	}

	// Token: 0x04000E69 RID: 3689
	public Animator charAnimator;

	// Token: 0x04000E6A RID: 3690
	public GameObject eyeLids;

	// Token: 0x04000E6B RID: 3691
	public GameObject eyeLidsHalf;

	// Token: 0x04000E6C RID: 3692
	public GameObject eyesForward;

	// Token: 0x04000E6D RID: 3693
	public GameObject eyesCamera;

	// Token: 0x04000E6E RID: 3694
	public ParticleSystem sleepZ;

	// Token: 0x04000E6F RID: 3695
	private bool isSleeping;

	// Token: 0x04000E70 RID: 3696
	private float blinkTimer = 1f;

	// Token: 0x04000E71 RID: 3697
	private float minBlink = 3f;

	// Token: 0x04000E72 RID: 3698
	private float maxBlink = 6f;
}
