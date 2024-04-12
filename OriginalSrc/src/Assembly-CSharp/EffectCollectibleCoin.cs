using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200018E RID: 398
public class EffectCollectibleCoin : MonoBehaviour
{
	// Token: 0x06000CCF RID: 3279 RVA: 0x0007B8A8 File Offset: 0x00079CA8
	private void Start()
	{
		this.coin = base.transform.Find("CoinHover/Coin");
		this.coinFx = base.transform.Find("CoinHover/Coin/Fx");
		this.coinRenderer = this.coin.gameObject.GetComponent<Renderer>();
		this.coinHover = base.transform.Find("CoinHover");
		this.pickUpEffect = base.transform.Find("PickUpSparks").GetComponent<ParticleSystem>();
		this.coinHover.Rotate(0f, Random.Range(0f, 360f), 0f);
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0007B94C File Offset: 0x00079D4C
	private void IdleCoin()
	{
		this.curTime += Main.m_gameDeltaTime;
		if (this.curTime > this.idleLength)
		{
			this.curTime -= this.idleLength;
		}
		Vector3 localPosition = this.coinHover.localPosition;
		localPosition.y = this.idleHover.Evaluate(this.curTime);
		this.coinHover.localPosition = localPosition;
		this.coinHover.Rotate(0f, this.idleRotation, 0f);
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x0007B9DC File Offset: 0x00079DDC
	private IEnumerator SpinCoinRoutine()
	{
		this.isCoroutineRunning = true;
		float curSpinTime = 0f;
		while (curSpinTime <= this.spinLength)
		{
			curSpinTime += Main.m_gameDeltaTime;
			Vector3 localPos = this.coin.localPosition;
			localPos.y = this.spinHover.Evaluate(curSpinTime);
			this.coin.localPosition = localPos;
			this.coin.Rotate(0f, 0f, this.spinRotate.Evaluate(curSpinTime));
			yield return null;
		}
		this.isCoroutineRunning = false;
		yield break;
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0007B9F8 File Offset: 0x00079DF8
	private IEnumerator PickUpCoinRoutine()
	{
		this.isCoroutineBRunning = true;
		this.coinHover.gameObject.SetActive(false);
		this.pickUpEffect.Play();
		while (this.pickUpEffect.IsAlive())
		{
			yield return null;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0007BA13 File Offset: 0x00079E13
	public void SpinCoin()
	{
		if (!this.isCoroutineRunning)
		{
			base.StartCoroutine("SpinCoinRoutine");
		}
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0007BA2C File Offset: 0x00079E2C
	public void PickUpCoin()
	{
		if (!this.isCoroutineBRunning)
		{
			base.StartCoroutine("PickUpCoinRoutine");
		}
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0007BA48 File Offset: 0x00079E48
	private void Update()
	{
		if (!PsState.m_physicsPaused)
		{
			if (!this.coinRenderer.isVisible)
			{
				this.coinFx.gameObject.SetActive(false);
				return;
			}
			this.coinFx.gameObject.SetActive(true);
			this.IdleCoin();
		}
	}

	// Token: 0x04000DF3 RID: 3571
	public AnimationCurve idleHover = new AnimationCurve();

	// Token: 0x04000DF4 RID: 3572
	public float idleRotation = 0.2f;

	// Token: 0x04000DF5 RID: 3573
	public float idleLength = 1f;

	// Token: 0x04000DF6 RID: 3574
	public AnimationCurve spinHover = new AnimationCurve();

	// Token: 0x04000DF7 RID: 3575
	public AnimationCurve spinRotate = new AnimationCurve();

	// Token: 0x04000DF8 RID: 3576
	public float spinLength = 1f;

	// Token: 0x04000DF9 RID: 3577
	private Transform coin;

	// Token: 0x04000DFA RID: 3578
	private Transform coinHover;

	// Token: 0x04000DFB RID: 3579
	private Transform coinFx;

	// Token: 0x04000DFC RID: 3580
	private ParticleSystem pickUpEffect;

	// Token: 0x04000DFD RID: 3581
	private float curTime;

	// Token: 0x04000DFE RID: 3582
	private bool isCoroutineRunning;

	// Token: 0x04000DFF RID: 3583
	private bool isCoroutineBRunning;

	// Token: 0x04000E00 RID: 3584
	private Renderer coinRenderer;
}
