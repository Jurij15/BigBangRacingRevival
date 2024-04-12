using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001C4 RID: 452
public class EffectMagicCauldron : MonoBehaviour
{
	// Token: 0x06000DC2 RID: 3522 RVA: 0x00081594 File Offset: 0x0007F994
	private void Awake()
	{
		this.cauldronAnimator = base.GetComponentInChildren<Animator>();
		this.boomMaterial = this.cauldronBoom.GetComponent<Renderer>().material;
		this.boomMaterial.SetFloat("_Fade", 0f);
		this.cauldronBoom.GetComponent<Renderer>().sharedMaterial = this.boomMaterial;
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x000815EE File Offset: 0x0007F9EE
	public void SplatCauldron()
	{
		this.cauldronAnimator.SetTrigger("PopCauldron");
		this.bubbles.gameObject.SetActive(false);
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x00081611 File Offset: 0x0007FA11
	public void ShowBoom()
	{
		this.superSplatter.Play();
		this.cauldronBoom.SetActive(true);
		base.StopCoroutine("BoomAnimation");
		base.StartCoroutine("BoomAnimation");
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00081641 File Offset: 0x0007FA41
	public void HideBoom()
	{
		this.bubbles.gameObject.SetActive(true);
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x00081654 File Offset: 0x0007FA54
	private IEnumerator BoomAnimation()
	{
		this.boomTimer = this.boomTime;
		while (this.boomTimer > 0f && this.boomMaterial != null)
		{
			this.boomMaterial.SetFloat("_Fade", this.boomFadeCurve.Evaluate(1f - this.boomTimer / this.boomTime));
			this.boomTimer -= Main.GetDeltaTime();
			yield return null;
		}
		Debug.Log("End of coroutine", null);
		if (this.boomMaterial != null)
		{
			this.boomMaterial.SetFloat("_Fade", 0f);
		}
		this.cauldronBoom.SetActive(false);
		yield break;
	}

	// Token: 0x04001086 RID: 4230
	public ParticleSystem bubbles;

	// Token: 0x04001087 RID: 4231
	public ParticleSystem superSplatter;

	// Token: 0x04001088 RID: 4232
	public GameObject cauldronBoom;

	// Token: 0x04001089 RID: 4233
	public AnimationCurve boomFadeCurve;

	// Token: 0x0400108A RID: 4234
	private float boomTime = 0.35f;

	// Token: 0x0400108B RID: 4235
	private float boomTimer;

	// Token: 0x0400108C RID: 4236
	private Material boomMaterial;

	// Token: 0x0400108D RID: 4237
	private Animator cauldronAnimator;
}
