using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
public class AlienAnimationTester : MonoBehaviour
{
	// Token: 0x06000D8C RID: 3468 RVA: 0x0007EC72 File Offset: 0x0007D072
	private void Awake()
	{
		this.curAnim = base.GetComponentInChildren<Animator>();
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0007EC80 File Offset: 0x0007D080
	private void Update()
	{
		if (Random.Range(0, this.blinkDensity) == 1 && !this.curAnim.GetCurrentAnimatorStateInfo(2).IsName("Hit"))
		{
			this.curAnim.SetTrigger("Hit");
		}
		if (Random.Range(0, this.cheerDensity) == 1 && !this.curAnim.GetCurrentAnimatorStateInfo(2).IsName("JumpCheer_A"))
		{
			this.curAnim.SetTrigger("JumpCheer");
		}
	}

	// Token: 0x04001000 RID: 4096
	public int blinkDensity = 75;

	// Token: 0x04001001 RID: 4097
	public int cheerDensity = 10;

	// Token: 0x04001002 RID: 4098
	private Animator curAnim;
}
