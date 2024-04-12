using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200018D RID: 397
public class EffectCollectible : MonoBehaviour
{
	// Token: 0x06000CC9 RID: 3273 RVA: 0x0007B519 File Offset: 0x00079919
	private void Start()
	{
		this.collectibleRenderer = this.collectible.GetComponent<Renderer>();
		this.collectibleParent.Rotate(0f, Random.Range(0f, 360f), 0f);
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x0007B550 File Offset: 0x00079950
	private void IdleCollectible()
	{
		this.curTime += Main.m_gameDeltaTime;
		if (this.curTime > this.idleLength)
		{
			this.curTime -= this.idleLength;
		}
		Vector3 localPosition = this.collectibleParent.localPosition;
		localPosition.y = this.idleHover.Evaluate(this.curTime);
		this.collectibleParent.localPosition = localPosition;
		this.collectibleParent.Rotate(0f, this.idleRotation, 0f);
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0007B5E0 File Offset: 0x000799E0
	private IEnumerator SpinCollectibleRoutine()
	{
		this.isCoroutineRunning = true;
		float curSpinTime = 0f;
		while (curSpinTime <= this.spinLength)
		{
			curSpinTime += Main.m_gameDeltaTime;
			Vector3 localPos = this.collectible.localPosition;
			localPos.y = this.spinHover.Evaluate(curSpinTime);
			this.collectible.localPosition = localPos;
			this.collectible.Rotate(0f, 0f, this.spinRotate.Evaluate(curSpinTime));
			yield return null;
		}
		this.isCoroutineRunning = false;
		yield break;
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0007B5FB File Offset: 0x000799FB
	public void SpinCoin()
	{
		if (!this.isCoroutineRunning)
		{
			base.StartCoroutine("SpinCollectibleRoutine");
		}
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0007B614 File Offset: 0x00079A14
	private void Update()
	{
		if (PsState.m_activeMinigame != null)
		{
			if (!PsState.m_activeMinigame.m_gamePaused)
			{
				if (!this.collectibleRenderer.isVisible && this.collectibleFx != null)
				{
					if (this.collectibleFx.gameObject.activeSelf)
					{
						this.collectibleFx.gameObject.SetActive(false);
					}
					return;
				}
				if (this.collectibleFx != null)
				{
					if (this.hideFx)
					{
						if (this.collectibleFx.gameObject.activeSelf)
						{
							this.collectibleFx.gameObject.SetActive(false);
						}
					}
					else if (!this.collectibleFx.gameObject.activeSelf)
					{
						this.collectibleFx.gameObject.SetActive(true);
					}
				}
				this.IdleCollectible();
			}
		}
		else
		{
			this.IdleCollectible();
		}
	}

	// Token: 0x04000DE5 RID: 3557
	public AnimationCurve idleHover = new AnimationCurve();

	// Token: 0x04000DE6 RID: 3558
	public float idleRotation = 0.2f;

	// Token: 0x04000DE7 RID: 3559
	public float idleLength = 1f;

	// Token: 0x04000DE8 RID: 3560
	public AnimationCurve spinHover = new AnimationCurve();

	// Token: 0x04000DE9 RID: 3561
	public AnimationCurve spinRotate = new AnimationCurve();

	// Token: 0x04000DEA RID: 3562
	public float spinLength = 1f;

	// Token: 0x04000DEB RID: 3563
	public Transform collectible;

	// Token: 0x04000DEC RID: 3564
	public Transform collectibleParent;

	// Token: 0x04000DED RID: 3565
	public Transform collectibleFx;

	// Token: 0x04000DEE RID: 3566
	private float curTime;

	// Token: 0x04000DEF RID: 3567
	private bool isCoroutineRunning;

	// Token: 0x04000DF0 RID: 3568
	private bool isCoroutineBRunning;

	// Token: 0x04000DF1 RID: 3569
	private Renderer collectibleRenderer;

	// Token: 0x04000DF2 RID: 3570
	public bool hideFx;
}
