using System;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class EffectWhitehole : MonoBehaviour
{
	// Token: 0x06000DFF RID: 3583 RVA: 0x00082D4B File Offset: 0x0008114B
	private void Start()
	{
		this.twirlMaterial = this.twirlObject.GetComponent<Renderer>().material;
		this.twirlEdgeMaterial = this.twirlEdgeObject.GetComponent<Renderer>().material;
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x00082D7C File Offset: 0x0008117C
	private void Update()
	{
		this.whiteSparkObject.transform.Rotate(0f, 0f, this.sparkRotationSpeed * Time.deltaTime);
		this.whiteholeScaler += Time.deltaTime * 5f;
		this.whiteholeWidth = 0.75f + Mathf.PerlinNoise(this.whiteholeScaler, 0f) * 0.25f;
		this.whiteholeHeight = 0.75f + Mathf.PerlinNoise(this.whiteholeScaler, 0.5f) * 0.25f;
		this.whiteHoleObject.transform.localScale = new Vector3(this.whiteholeWidth, this.whiteholeHeight, 1f);
		this.twirlInAmount += Time.deltaTime * this.twirlInSpeed;
		this.twirlAroundAmount += Time.deltaTime * this.twirlAroundSpeed;
		if (this.twirlInAmount > 1f)
		{
			this.twirlInAmount -= 1f;
		}
		if (this.twirlAroundAmount > 1f)
		{
			this.twirlAroundAmount -= 1f;
		}
		this.twirlMaterial.SetTextureOffset("_MainTex", new Vector2(-this.twirlAroundAmount, this.twirlInAmount));
		this.twirlEdgeMaterial.SetTextureOffset("_MainTex", new Vector2(this.twirlAroundAmount, -this.twirlInAmount));
	}

	// Token: 0x040010ED RID: 4333
	public GameObject whiteSparkObject;

	// Token: 0x040010EE RID: 4334
	public float sparkRotationSpeed;

	// Token: 0x040010EF RID: 4335
	public GameObject twirlObject;

	// Token: 0x040010F0 RID: 4336
	public GameObject twirlEdgeObject;

	// Token: 0x040010F1 RID: 4337
	public float twirlInSpeed;

	// Token: 0x040010F2 RID: 4338
	public float twirlAroundSpeed;

	// Token: 0x040010F3 RID: 4339
	private float twirlInAmount;

	// Token: 0x040010F4 RID: 4340
	private float twirlAroundAmount;

	// Token: 0x040010F5 RID: 4341
	private Material twirlMaterial;

	// Token: 0x040010F6 RID: 4342
	private Material twirlEdgeMaterial;

	// Token: 0x040010F7 RID: 4343
	public GameObject whiteHoleObject;

	// Token: 0x040010F8 RID: 4344
	private float whiteholeWidth = 1f;

	// Token: 0x040010F9 RID: 4345
	private float whiteholeHeight = 1f;

	// Token: 0x040010FA RID: 4346
	private float whiteholeScaler;
}
