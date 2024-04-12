using System;
using UnityEngine;

// Token: 0x020001A3 RID: 419
public class BossController : MonoBehaviour
{
	// Token: 0x06000D23 RID: 3363 RVA: 0x0007DB01 File Offset: 0x0007BF01
	public void Idle()
	{
		this.m_animator.Play("Idle");
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0007DB13 File Offset: 0x0007BF13
	public void PopIn()
	{
		this.m_animator.Play("PopIn");
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0007DB25 File Offset: 0x0007BF25
	public void Talk()
	{
		this.m_animator.Play("Talk");
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0007DB37 File Offset: 0x0007BF37
	public void Taunt()
	{
		this.m_animator.Play("Taunt");
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x0007DB49 File Offset: 0x0007BF49
	public void Death()
	{
		this.m_animator.Play("Death");
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0007DB5B File Offset: 0x0007BF5B
	private void Update()
	{
	}

	// Token: 0x04000E75 RID: 3701
	public Animator m_animator;
}
