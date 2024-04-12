using System;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class EffectBoost : MonoBehaviour
{
	// Token: 0x06000CC3 RID: 3267 RVA: 0x0007B305 File Offset: 0x00079705
	public void GainBoost()
	{
		this.boostGain.Play();
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x0007B314 File Offset: 0x00079714
	public void ActivateBoost(EffectBoost.BoostDirection boostDirection)
	{
		if (boostDirection == EffectBoost.BoostDirection.Left && !this.m_activeLeft)
		{
			this.boostIdle.Stop();
			this.boostActiveLeft.Play();
			this.m_activeRight = false;
			this.m_activeLeft = true;
			this.m_idle = false;
			this.m_drop = false;
		}
		else if (boostDirection == EffectBoost.BoostDirection.Right && !this.m_activeRight)
		{
			this.boostIdle.Stop();
			this.boostActiveRight.Play();
			this.m_activeRight = true;
			this.m_activeLeft = false;
			this.m_idle = false;
			this.m_drop = false;
		}
		else if (boostDirection == EffectBoost.BoostDirection.Both)
		{
			this.IdleBoost();
		}
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x0007B3C0 File Offset: 0x000797C0
	public bool SomethingActive()
	{
		if (this.boostIdle.isPlaying || this.boostActiveLeft.isPlaying || this.boostActiveRight.isPlaying)
		{
			if (this.m_drop)
			{
				this.m_drop = false;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0007B414 File Offset: 0x00079814
	public void IdleBoost()
	{
		if (this.m_idle)
		{
			return;
		}
		this.m_activeRight = false;
		this.m_activeLeft = false;
		this.m_idle = true;
		this.m_drop = false;
		this.boostIdle.Play();
		this.boostActiveRight.Stop();
		this.boostActiveLeft.Stop();
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x0007B46C File Offset: 0x0007986C
	public void DropBoost()
	{
		if (this.m_drop)
		{
			return;
		}
		this.m_activeRight = false;
		this.m_activeLeft = false;
		this.m_idle = false;
		this.m_drop = true;
		this.boostIdle.Stop();
		this.boostActiveRight.Stop();
		this.boostActiveLeft.Stop();
	}

	// Token: 0x04000DD9 RID: 3545
	public ParticleSystem boostIdle;

	// Token: 0x04000DDA RID: 3546
	public ParticleSystem boostActiveLeft;

	// Token: 0x04000DDB RID: 3547
	public ParticleSystem boostActiveRight;

	// Token: 0x04000DDC RID: 3548
	public ParticleSystem boostGain;

	// Token: 0x04000DDD RID: 3549
	private bool m_activeLeft;

	// Token: 0x04000DDE RID: 3550
	private bool m_activeRight;

	// Token: 0x04000DDF RID: 3551
	private bool m_idle;

	// Token: 0x04000DE0 RID: 3552
	private bool m_drop;

	// Token: 0x0200018C RID: 396
	public enum BoostDirection
	{
		// Token: 0x04000DE2 RID: 3554
		Left,
		// Token: 0x04000DE3 RID: 3555
		Right,
		// Token: 0x04000DE4 RID: 3556
		Both
	}
}
