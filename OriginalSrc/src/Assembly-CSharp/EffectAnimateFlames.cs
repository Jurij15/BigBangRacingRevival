using System;
using UnityEngine;

// Token: 0x0200018A RID: 394
public class EffectAnimateFlames : MonoBehaviour
{
	// Token: 0x06000CC0 RID: 3264 RVA: 0x0007B236 File Offset: 0x00079636
	private void Start()
	{
		this.curRenderer = base.GetComponent<Renderer>();
		this.flameMaterial = this.curRenderer.material;
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x0007B258 File Offset: 0x00079658
	private void Update()
	{
		this.twirlInAmount += Main.GetDeltaTime() * this.xSpeed;
		this.twirlAroundAmount += Main.GetDeltaTime() * this.ySpeed;
		if (this.twirlInAmount > 1f)
		{
			this.twirlInAmount -= 1f;
		}
		if (this.twirlAroundAmount > 1f)
		{
			this.twirlAroundAmount -= 1f;
		}
		this.flameMaterial.SetTextureOffset("_MainTex", new Vector2(this.twirlAroundAmount, -this.twirlInAmount));
	}

	// Token: 0x04000DD3 RID: 3539
	public float xSpeed;

	// Token: 0x04000DD4 RID: 3540
	public float ySpeed;

	// Token: 0x04000DD5 RID: 3541
	private float twirlInAmount;

	// Token: 0x04000DD6 RID: 3542
	private float twirlAroundAmount;

	// Token: 0x04000DD7 RID: 3543
	public Renderer curRenderer;

	// Token: 0x04000DD8 RID: 3544
	public Material flameMaterial;
}
