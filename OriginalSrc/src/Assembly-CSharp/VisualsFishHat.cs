using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001BD RID: 445
public class VisualsFishHat : MonoBehaviour
{
	// Token: 0x06000DA7 RID: 3495 RVA: 0x00080240 File Offset: 0x0007E640
	private void Awake()
	{
		this.leftFishEye.eyeObject = this.leftEye;
		this.leftFishEye.eyeStartRotation = this.leftEye.transform.localRotation;
		this.rightFishEye.eyeObject = this.rightEye;
		this.rightFishEye.eyeStartRotation = this.rightEye.transform.localRotation;
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x000802A8 File Offset: 0x0007E6A8
	public IEnumerator RandomOrientEye(FishEye eye)
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

	// Token: 0x06000DA9 RID: 3497 RVA: 0x000802CC File Offset: 0x0007E6CC
	private void Update()
	{
		if (this.eyeMovementTimerLeft <= 0f)
		{
			this.eyeMovementTimerLeft = Random.Range(0.25f, 2f);
			base.StartCoroutine("RandomOrientEye", this.rightFishEye);
		}
		else
		{
			this.eyeMovementTimerLeft -= Main.GetDeltaTime();
		}
		if (this.eyeMovementTimerRight <= 0f)
		{
			this.eyeMovementTimerRight = Random.Range(0.25f, 2f);
			base.StartCoroutine("RandomOrientEye", this.leftFishEye);
		}
		else
		{
			this.eyeMovementTimerRight -= Main.GetDeltaTime();
		}
		if (this.wiggleTimer <= 0f)
		{
			this.wiggleTimer = Random.Range(2f, 5f);
			this.fishAnimator.SetTrigger("TailWiggle");
		}
		else
		{
			this.wiggleTimer -= Main.GetDeltaTime();
		}
		if (this.breathTimer <= 0f)
		{
			this.breathTimer = Random.Range(1f, 3f);
			this.fishAnimator.SetTrigger("Breath");
		}
		else
		{
			this.breathTimer -= Main.GetDeltaTime();
		}
	}

	// Token: 0x04001035 RID: 4149
	public Animator fishAnimator;

	// Token: 0x04001036 RID: 4150
	private float wiggleTimer = 1f;

	// Token: 0x04001037 RID: 4151
	private float breathTimer = 1f;

	// Token: 0x04001038 RID: 4152
	public GameObject leftEye;

	// Token: 0x04001039 RID: 4153
	public GameObject rightEye;

	// Token: 0x0400103A RID: 4154
	private float eyeRotationAmount = 25f;

	// Token: 0x0400103B RID: 4155
	private float rotationSpeed = 0.1f;

	// Token: 0x0400103C RID: 4156
	private float eyeMovementTime = 2f;

	// Token: 0x0400103D RID: 4157
	private float eyeMovementTimerLeft;

	// Token: 0x0400103E RID: 4158
	private float eyeMovementTimerRight;

	// Token: 0x0400103F RID: 4159
	private FishEye leftFishEye;

	// Token: 0x04001040 RID: 4160
	private FishEye rightFishEye;
}
