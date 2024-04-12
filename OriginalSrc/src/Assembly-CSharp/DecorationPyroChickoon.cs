using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class DecorationPyroChickoon : MonoBehaviour
{
	// Token: 0x06000CBA RID: 3258 RVA: 0x0007AD98 File Offset: 0x00079198
	public void PlayEffect()
	{
		this.chickoonOffsetGo.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.chickoonGo.transform.rotation = this.chickoonOffsetGo.transform.rotation;
		this.bucketAnimator.SetInteger("Randomizer", Random.Range(0, 101));
		this.bucketAnimator.SetTrigger("Bucket");
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x0007AE14 File Offset: 0x00079214
	private void PopLid()
	{
		this.bucketRenderer.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0f));
		this.particleLid.Play();
		if (this.bucketAnimator.GetInteger("Randomizer") > 0)
		{
			this.particleFeather.Play();
			this.smoke.Play();
			SoundS.PlaySingleShot("/Ingame/Units/Decorations/DecorationPyrotechnicsChickoon_ShootChicken", base.transform.position, 1f);
		}
		else if (this.bucketAnimator.GetInteger("Randomizer") == 0)
		{
			SoundS.PlaySingleShot("/Ingame/Units/Decorations/DecorationPyrotechnicsChickoon_ShootBroiler", base.transform.position, 1f);
			this.greaseUp.Play();
		}
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x0007AED8 File Offset: 0x000792D8
	private void ExplodeChickoon()
	{
		if (this.bucketAnimator.GetInteger("Randomizer") > 0)
		{
			this.chickoonExplosion.transform.position = this.chickoonGo.transform.position;
			this.chickoonExplosion.Play();
			this.chickoonSmokesplosion.transform.position = this.chickoonGo.transform.position;
			this.chickoonSmokesplosion.Play();
			SoundS.PlaySingleShot("/Ingame/Units/Decorations/DecorationPyrotechnicsChickoon_ChickenExplosion", base.transform.position, 1f);
		}
		else if (this.bucketAnimator.GetInteger("Randomizer") == 0)
		{
			this.greaseExplosion.Play();
			SoundS.PlaySingleShot("/Ingame/Units/Decorations/DecorationPyrotechnicsChickoon_BroilerExplosion", base.transform.position, 1f);
		}
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x0007AFAA File Offset: 0x000793AA
	private void startFlyRoutine()
	{
		base.StartCoroutine("FlyRoutine");
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x0007AFB8 File Offset: 0x000793B8
	private IEnumerator FlyRoutine()
	{
		this.flyTime = 0f;
		float randomDirection = Random.Range(0f, 1f);
		float randomRotation = Random.Range(0f, 1f);
		while (this.flyTime <= 2f)
		{
			if (PsState.m_physicsPaused)
			{
				yield return null;
			}
			else
			{
				this.flyTime += Main.m_gameDeltaTime;
				Vector3 oldPosition = this.chickoonGo.transform.position;
				float xOffset = Mathf.Lerp(this.flyLeft.Evaluate(this.flyTime), this.flyRight.Evaluate(this.flyTime), randomDirection);
				this.chickoonOffsetGo.transform.Translate(new Vector3(xOffset, this.flyLeft.Evaluate(this.flyTime) * 2f, 0f));
				Vector3 movementDirection = (this.chickoonGo.transform.position - oldPosition).normalized;
				movementDirection.y = 2f;
				this.chickoonGo.transform.rotation = Quaternion.Slerp(this.chickoonGo.transform.rotation, Quaternion.LookRotation(movementDirection, new Vector3(0f, -1f, 0f)), Time.deltaTime * 4f);
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x04000DC4 RID: 3524
	public Renderer[] chickoonParts;

	// Token: 0x04000DC5 RID: 3525
	public Renderer bucketRenderer;

	// Token: 0x04000DC6 RID: 3526
	public Animator bucketAnimator;

	// Token: 0x04000DC7 RID: 3527
	public ParticleSystem particleLid;

	// Token: 0x04000DC8 RID: 3528
	public ParticleSystem particleFeather;

	// Token: 0x04000DC9 RID: 3529
	public ParticleSystem chickoonExplosion;

	// Token: 0x04000DCA RID: 3530
	public ParticleSystem chickoonSmokesplosion;

	// Token: 0x04000DCB RID: 3531
	public ParticleSystem greaseUp;

	// Token: 0x04000DCC RID: 3532
	public ParticleSystem greaseExplosion;

	// Token: 0x04000DCD RID: 3533
	public ParticleSystem smoke;

	// Token: 0x04000DCE RID: 3534
	public GameObject chickoonOffsetGo;

	// Token: 0x04000DCF RID: 3535
	public GameObject chickoonGo;

	// Token: 0x04000DD0 RID: 3536
	public AnimationCurve flyLeft;

	// Token: 0x04000DD1 RID: 3537
	public AnimationCurve flyRight;

	// Token: 0x04000DD2 RID: 3538
	private float flyTime;
}
