using System;
using UnityEngine;

// Token: 0x020001C5 RID: 453
public class EffectBlackhole : MonoBehaviour
{
	// Token: 0x06000DC8 RID: 3528 RVA: 0x000817FB File Offset: 0x0007FBFB
	private void Start()
	{
		this.twirlMaterial = this.twirlObject.GetComponent<Renderer>().material;
		this.twirlEdgeMaterial = this.twirlEdgeObject.GetComponent<Renderer>().material;
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x0008182C File Offset: 0x0007FC2C
	private void Update()
	{
		this.blackSparkObject.transform.Rotate(0f, 0f, this.sparkRotationSpeed * Time.deltaTime);
		this.blackholeScaler += Time.deltaTime * 5f;
		this.blackholeWidth = 0.75f + Mathf.PerlinNoise(this.blackholeScaler, 0f) * 0.25f;
		this.blackholeHeight = 0.75f + Mathf.PerlinNoise(this.blackholeScaler, 0.5f) * 0.25f;
		this.blackHoleObject.transform.localScale = new Vector3(this.blackholeWidth, this.blackholeHeight, 1f);
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

	// Token: 0x0400108E RID: 4238
	public GameObject blackSparkObject;

	// Token: 0x0400108F RID: 4239
	public float sparkRotationSpeed;

	// Token: 0x04001090 RID: 4240
	public GameObject twirlObject;

	// Token: 0x04001091 RID: 4241
	public GameObject twirlEdgeObject;

	// Token: 0x04001092 RID: 4242
	public float twirlInSpeed;

	// Token: 0x04001093 RID: 4243
	public float twirlAroundSpeed;

	// Token: 0x04001094 RID: 4244
	private float twirlInAmount;

	// Token: 0x04001095 RID: 4245
	private float twirlAroundAmount;

	// Token: 0x04001096 RID: 4246
	private Material twirlMaterial;

	// Token: 0x04001097 RID: 4247
	private Material twirlEdgeMaterial;

	// Token: 0x04001098 RID: 4248
	public GameObject blackHoleObject;

	// Token: 0x04001099 RID: 4249
	private float blackholeWidth = 1f;

	// Token: 0x0400109A RID: 4250
	private float blackholeHeight = 1f;

	// Token: 0x0400109B RID: 4251
	private float blackholeScaler;
}
