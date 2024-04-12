using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000474 RID: 1140
public class EffectGiftPackage : MonoBehaviour
{
	// Token: 0x06001F03 RID: 7939 RVA: 0x0017D708 File Offset: 0x0017BB08
	private void OpenGift()
	{
		this.packageNormal.SetActive(false);
		this.openingEffect.SetActive(true);
		for (int i = 0; i < this.packageSides.Length; i++)
		{
			this.packageSides[i].SetActive(true);
		}
		base.GetComponent<Animation>().Play("EffectGiftPackageOpen");
		base.StartCoroutine("Fade");
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x0017D774 File Offset: 0x0017BB74
	private IEnumerator Fade()
	{
		for (float f = 0f; f < this.fadeoutTime; f += Time.deltaTime)
		{
			float currentTime = f / this.fadeoutTime;
			this.currentColor.a = this.fadeoutCurve.Evaluate(currentTime);
			this.packageNormal.GetComponent<Renderer>().sharedMaterial.color = this.currentColor;
			yield return null;
		}
		this.currentColor.a = 0f;
		this.packageNormal.GetComponent<Renderer>().sharedMaterial.color = this.currentColor;
		yield break;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x0017D790 File Offset: 0x0017BB90
	private void Start()
	{
		this.packageNormal.GetComponent<Renderer>().sharedMaterial.color = new Color(1f, 1f, 1f, 1f);
		this.currentColor = this.packageNormal.GetComponent<Renderer>().sharedMaterial.color;
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x0017D7E6 File Offset: 0x0017BBE6
	private void Update()
	{
		if (Input.GetKeyDown("space") && this.debugEnabled)
		{
			this.openingEffect.SetActive(false);
			this.OpenGift();
		}
	}

	// Token: 0x0400269A RID: 9882
	public GameObject packageNormal;

	// Token: 0x0400269B RID: 9883
	public GameObject[] packageSides;

	// Token: 0x0400269C RID: 9884
	public GameObject openingEffect;

	// Token: 0x0400269D RID: 9885
	private Color currentColor;

	// Token: 0x0400269E RID: 9886
	private Color targetColor = new Color(1f, 1f, 1f, 0f);

	// Token: 0x0400269F RID: 9887
	public float fadeoutTime = 1f;

	// Token: 0x040026A0 RID: 9888
	public AnimationCurve fadeoutCurve;

	// Token: 0x040026A1 RID: 9889
	public bool debugEnabled;
}
