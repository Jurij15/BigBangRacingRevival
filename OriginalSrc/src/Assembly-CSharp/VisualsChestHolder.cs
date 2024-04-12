using System;
using UnityEngine;

// Token: 0x020001B1 RID: 433
public class VisualsChestHolder : MonoBehaviour
{
	// Token: 0x06000D78 RID: 3448 RVA: 0x0007E65A File Offset: 0x0007CA5A
	public void PopIn()
	{
		this.holderAnimator.SetTrigger("PopIn");
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0007E66C File Offset: 0x0007CA6C
	public void PopOut()
	{
		this.holderAnimator.SetTrigger("PopOut");
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0007E67E File Offset: 0x0007CA7E
	public void Nudge()
	{
		this.holderAnimator.SetTrigger("Nudge");
	}

	// Token: 0x04000E9B RID: 3739
	public Animator holderAnimator;
}
