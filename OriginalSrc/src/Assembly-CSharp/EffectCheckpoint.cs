using System;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public class EffectCheckpoint : MonoBehaviour
{
	// Token: 0x06000D2A RID: 3370 RVA: 0x0007DB65 File Offset: 0x0007BF65
	public virtual void ShowObject()
	{
		if (this.insideObject != null)
		{
			this.insideObject.SetActive(true);
		}
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0007DB84 File Offset: 0x0007BF84
	public virtual void HideObject()
	{
		if (this.insideObject != null)
		{
			this.insideObject.SetActive(false);
		}
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0007DBA3 File Offset: 0x0007BFA3
	public virtual void SetLocked()
	{
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0007DBA5 File Offset: 0x0007BFA5
	public virtual void Activate()
	{
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0007DBA7 File Offset: 0x0007BFA7
	public virtual void SetIdleActive()
	{
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0007DBA9 File Offset: 0x0007BFA9
	public virtual void Claim()
	{
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0007DBAB File Offset: 0x0007BFAB
	public virtual void SetClaimed()
	{
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0007DBAD File Offset: 0x0007BFAD
	public virtual void Hide()
	{
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0007DBB0 File Offset: 0x0007BFB0
	public void SetBorderState(bool state)
	{
		if (this.borderRenderer != null)
		{
			this.instancedBorderMaterial = this.borderRenderer.material;
			if (state)
			{
				this.instancedBorderMaterial.color = DebugDraw.HexToColor("FF8116");
			}
			else
			{
				this.instancedBorderMaterial.color = DebugDraw.HexToColor("b3a8ae");
			}
		}
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0007DC14 File Offset: 0x0007C014
	public virtual void OnDestroy()
	{
		if (this.instancedBorderMaterial != null)
		{
			Object.Destroy(this.instancedBorderMaterial);
		}
	}

	// Token: 0x04000E76 RID: 3702
	public Renderer borderRenderer;

	// Token: 0x04000E77 RID: 3703
	public GameObject insideObject;

	// Token: 0x04000E78 RID: 3704
	private Material instancedBorderMaterial;
}
