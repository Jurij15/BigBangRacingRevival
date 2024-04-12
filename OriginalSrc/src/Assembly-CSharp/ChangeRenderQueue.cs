using System;
using UnityEngine;

// Token: 0x0200019F RID: 415
public class ChangeRenderQueue : MonoBehaviour
{
	// Token: 0x06000D0C RID: 3340 RVA: 0x0007D456 File Offset: 0x0007B856
	private void Start()
	{
		this.material = base.transform.GetComponent<Renderer>().material;
		this.material.renderQueue = this.renderQueue;
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0007D47F File Offset: 0x0007B87F
	private void OnDestroy()
	{
		Object.Destroy(this.material);
	}

	// Token: 0x04000E67 RID: 3687
	public int renderQueue;

	// Token: 0x04000E68 RID: 3688
	private Material material;
}
