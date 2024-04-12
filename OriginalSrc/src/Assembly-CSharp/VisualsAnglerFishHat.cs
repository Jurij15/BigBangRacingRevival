using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001BB RID: 443
public class VisualsAnglerFishHat : MonoBehaviour
{
	// Token: 0x06000DA3 RID: 3491 RVA: 0x0007FE88 File Offset: 0x0007E288
	private void Awake()
	{
		this.AnglerFishLeftEye.eyeObject = this.leftAnglerEye;
		this.AnglerFishLeftEye.eyeStartRotation = this.leftAnglerEye.transform.localRotation;
		this.AnglerFishRightEye.eyeObject = this.rightAnglerEye;
		this.AnglerFishRightEye.eyeStartRotation = this.rightAnglerEye.transform.localRotation;
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x0007FEF0 File Offset: 0x0007E2F0
	public IEnumerator RandomOrientEye(AnglerFishEye eye)
	{
		Quaternion targetRotation = Quaternion.Euler(eye.eyeStartRotation.eulerAngles.x + Random.Range(-this.eyeRotationAmount, this.eyeRotationAmount), eye.eyeStartRotation.eulerAngles.y + Random.Range(-this.eyeRotationAmount, this.eyeRotationAmount), eye.eyeStartRotation.eulerAngles.z);
		Quaternion eyeLoopStartRot = eye.eyeObject.transform.localRotation;
		float rotationTimer = this.rotationSpeed;
		while (rotationTimer > 0f)
		{
			rotationTimer -= Main.GetDeltaTime();
			eye.eyeObject.transform.localRotation = Quaternion.Slerp(eyeLoopStartRot, targetRotation, 1f - rotationTimer / this.rotationSpeed);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x0007FF14 File Offset: 0x0007E314
	private void Update()
	{
		if (this.eyeMovementTimerLeft <= 0f)
		{
			this.eyeMovementTimerLeft = Random.Range(0.25f, 2f);
			base.StartCoroutine("RandomOrientEye", this.AnglerFishRightEye);
		}
		else
		{
			this.eyeMovementTimerLeft -= Main.GetDeltaTime();
		}
		if (this.eyeMovementTimerRight <= 0f)
		{
			this.eyeMovementTimerRight = Random.Range(0.25f, 2f);
			base.StartCoroutine("RandomOrientEye", this.AnglerFishLeftEye);
		}
		else
		{
			this.eyeMovementTimerRight -= Main.GetDeltaTime();
		}
		if (this.anglerwiggleTimer <= 0f)
		{
			this.anglertailwiggleTimer = Random.Range(2f, 5f);
			this.anglerfishAnimator.SetTrigger("AnglerFishWiggle");
		}
		else
		{
			this.anglertailwiggleTimer -= Main.GetDeltaTime();
		}
		if (this.anglerwiggleTimer <= 0f)
		{
			this.anglerwiggleTimer = Random.Range(1f, 3f);
			this.anglerfishAnimator.SetTrigger("AnglerWiggle");
		}
		else
		{
			this.anglerwiggleTimer -= Main.GetDeltaTime();
		}
	}

	// Token: 0x04001027 RID: 4135
	public Animator anglerfishAnimator;

	// Token: 0x04001028 RID: 4136
	private float anglertailwiggleTimer = 1f;

	// Token: 0x04001029 RID: 4137
	private float anglerwiggleTimer = 1f;

	// Token: 0x0400102A RID: 4138
	public GameObject leftAnglerEye;

	// Token: 0x0400102B RID: 4139
	public GameObject rightAnglerEye;

	// Token: 0x0400102C RID: 4140
	private float eyeRotationAmount = 20f;

	// Token: 0x0400102D RID: 4141
	private float rotationSpeed = 0.1f;

	// Token: 0x0400102E RID: 4142
	private float eyeMovementTime = 2f;

	// Token: 0x0400102F RID: 4143
	private float eyeMovementTimerLeft;

	// Token: 0x04001030 RID: 4144
	private float eyeMovementTimerRight;

	// Token: 0x04001031 RID: 4145
	private AnglerFishEye AnglerFishLeftEye;

	// Token: 0x04001032 RID: 4146
	private AnglerFishEye AnglerFishRightEye;
}
