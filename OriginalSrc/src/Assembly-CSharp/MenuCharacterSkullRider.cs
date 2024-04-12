using System;
using UnityEngine;

// Token: 0x020001A1 RID: 417
public class MenuCharacterSkullRider : MonoBehaviour
{
	// Token: 0x06000D1A RID: 3354 RVA: 0x0007DA20 File Offset: 0x0007BE20
	public void PopIn()
	{
		this.skullRiderAnimator.SetTrigger("PopIn");
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0007DA32 File Offset: 0x0007BE32
	public void Talk()
	{
		this.skullRiderAnimator.SetTrigger("Talk");
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0007DA44 File Offset: 0x0007BE44
	public void Taunt()
	{
		this.skullRiderAnimator.SetTrigger("Taunt");
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x0007DA56 File Offset: 0x0007BE56
	public void Death()
	{
		this.skullRiderAnimator.SetTrigger("Death");
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x0007DA68 File Offset: 0x0007BE68
	private void Update()
	{
		if (Input.GetKeyDown(49))
		{
			this.PopIn();
		}
		else if (Input.GetKeyDown(50))
		{
			this.Talk();
		}
		else if (Input.GetKeyDown(51))
		{
			this.Taunt();
		}
		else if (Input.GetKeyDown(52))
		{
			this.Death();
		}
	}

	// Token: 0x04000E73 RID: 3699
	public Animator skullRiderAnimator;
}
