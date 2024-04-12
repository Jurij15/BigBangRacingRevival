using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class EffectLogo : MonoBehaviour
{
	// Token: 0x06000CDC RID: 3292 RVA: 0x0007BDA0 File Offset: 0x0007A1A0
	private void Awake()
	{
		this.magentaMaterials.Clear();
		this.yellowMaterials.Clear();
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.name.StartsWith("Block"))
				{
					Material material = Object.Instantiate<Material>(transform.GetComponent<Renderer>().material);
					transform.GetComponent<MeshRenderer>().material = material;
					this.yellowMaterials.Add(material);
				}
				else if (transform.name.StartsWith("Letter"))
				{
					Material material2 = Object.Instantiate<Material>(transform.GetComponent<Renderer>().material);
					transform.GetComponent<MeshRenderer>().material = material2;
					this.magentaMaterials.Add(material2);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0007BE98 File Offset: 0x0007A298
	private void PlaySound()
	{
		this.soundPlayed = true;
		AudioSource component = base.gameObject.GetComponent<AudioSource>();
		if (component != null && !PlayerPrefsX.GetMuteSoundFX() && !component.playOnAwake)
		{
			component.Play();
		}
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0007BEDF File Offset: 0x0007A2DF
	private void Update()
	{
		this.tick++;
		if (!this.soundPlayed && this.soundPlayTick == this.tick)
		{
			this.PlaySound();
		}
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0007BF14 File Offset: 0x0007A314
	private void Start()
	{
		foreach (Material material in this.magentaMaterials)
		{
			base.StartCoroutine("FlashColors", new FlasherData
			{
				mat = material,
				grad = this.magentaGradients
			});
		}
		foreach (Material material2 in this.yellowMaterials)
		{
			base.StartCoroutine("FlashColors", new FlasherData
			{
				mat = material2,
				grad = this.yellowGradients
			});
		}
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0007C010 File Offset: 0x0007A410
	private IEnumerator FlashColors(FlasherData fData)
	{
		float startCounter = Random.Range(this.minStartDelay, this.maxStartDelay);
		float flashTime = Random.Range(this.minFlashTime, this.maxFlashTime);
		float flashStartTime = flashTime;
		Gradient grad = fData.grad[Random.Range(0, fData.grad.Count)];
		Material mat = fData.mat;
		while (startCounter > 0f)
		{
			startCounter -= Time.deltaTime;
			yield return null;
		}
		while (flashTime > 0f)
		{
			flashTime -= Time.deltaTime;
			mat.SetColor("_Color", grad.Evaluate(1f - flashTime / flashStartTime));
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0007C034 File Offset: 0x0007A434
	private void OnDestroy()
	{
		foreach (Material material in this.magentaMaterials)
		{
			Object.Destroy(material);
		}
		foreach (Material material2 in this.yellowMaterials)
		{
			Object.Destroy(material2);
		}
	}

	// Token: 0x04000E09 RID: 3593
	[HideInInspector]
	public List<Material> magentaMaterials;

	// Token: 0x04000E0A RID: 3594
	[HideInInspector]
	public List<Material> yellowMaterials;

	// Token: 0x04000E0B RID: 3595
	public float minStartDelay;

	// Token: 0x04000E0C RID: 3596
	public float maxStartDelay = 0.25f;

	// Token: 0x04000E0D RID: 3597
	public float minFlashTime = 0.2f;

	// Token: 0x04000E0E RID: 3598
	public float maxFlashTime = 0.5f;

	// Token: 0x04000E0F RID: 3599
	public List<Gradient> magentaGradients;

	// Token: 0x04000E10 RID: 3600
	public List<Gradient> yellowGradients;

	// Token: 0x04000E11 RID: 3601
	private bool soundPlayed;

	// Token: 0x04000E12 RID: 3602
	private int soundPlayTick = 8;

	// Token: 0x04000E13 RID: 3603
	private int tick;
}
