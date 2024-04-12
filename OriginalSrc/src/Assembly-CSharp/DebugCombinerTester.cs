using System;
using UnityEngine;

// Token: 0x02000472 RID: 1138
public class DebugCombinerTester : MonoBehaviour
{
	// Token: 0x06001EFD RID: 7933 RVA: 0x0017D5C6 File Offset: 0x0017B9C6
	private void Awake()
	{
		this.testMaterial = base.GetComponent<Renderer>().material;
		this.ChangeTexture();
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x0017D5DF File Offset: 0x0017B9DF
	private void Update()
	{
		if (Input.GetKeyDown(32))
		{
			this.ChangeTexture();
		}
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x0017D5F3 File Offset: 0x0017B9F3
	private void ChangeTexture()
	{
		this.testMaterial.SetTexture("_MainTex", TextureCombiner.CombineVehicleTextures(this.baseTexture, this.baseColorMask, this.patternColorMask, this.baseColor, this.patternColor));
	}

	// Token: 0x04002691 RID: 9873
	public Texture2D baseTexture;

	// Token: 0x04002692 RID: 9874
	public Texture2D baseColorMask;

	// Token: 0x04002693 RID: 9875
	public Texture2D patternColorMask;

	// Token: 0x04002694 RID: 9876
	public Color32 baseColor;

	// Token: 0x04002695 RID: 9877
	public Color32 patternColor;

	// Token: 0x04002696 RID: 9878
	private Material testMaterial;
}
