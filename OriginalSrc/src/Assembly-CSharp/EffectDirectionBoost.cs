using System;
using UnityEngine;

// Token: 0x020001C6 RID: 454
public class EffectDirectionBoost : MonoBehaviour, IMonoBehaviour
{
	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000DCB RID: 3531 RVA: 0x000819AE File Offset: 0x0007FDAE
	// (set) Token: 0x06000DCC RID: 3532 RVA: 0x000819B6 File Offset: 0x0007FDB6
	public MonoBehaviourC m_component
	{
		get
		{
			return this._component;
		}
		set
		{
			this._component = value;
		}
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x000819BF File Offset: 0x0007FDBF
	public void StartEffect()
	{
		this.effectTimer = this.boostEffectLength;
		this.particleBoostSparks.Play();
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x000819D8 File Offset: 0x0007FDD8
	private void AnimateEffect()
	{
		this.effectTimer -= Main.GetDeltaTime();
		if (this.effectTimer <= 0f)
		{
			this.circleTransform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			float num = this.yCurve.Evaluate(1f - this.effectTimer / this.boostEffectLength);
			float num2 = this.xzCurve.Evaluate(1f - this.effectTimer / this.boostEffectLength);
			this.circleTransform.localScale = new Vector3(num2, num2, num);
		}
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x00081A7C File Offset: 0x0007FE7C
	public void TLUpdate()
	{
		if (this.effectTimer > 0f)
		{
			this.AnimateEffect();
		}
	}

	// Token: 0x0400109C RID: 4252
	private MonoBehaviourC _component;

	// Token: 0x0400109D RID: 4253
	public float boostEffectLength = 0.5f;

	// Token: 0x0400109E RID: 4254
	private float effectTimer;

	// Token: 0x0400109F RID: 4255
	public AnimationCurve yCurve;

	// Token: 0x040010A0 RID: 4256
	public AnimationCurve xzCurve;

	// Token: 0x040010A1 RID: 4257
	public Transform circleTransform;

	// Token: 0x040010A2 RID: 4258
	public ParticleSystem particleCircles;

	// Token: 0x040010A3 RID: 4259
	public ParticleSystem particleSparks;

	// Token: 0x040010A4 RID: 4260
	public ParticleSystem particleBoostSparks;
}
