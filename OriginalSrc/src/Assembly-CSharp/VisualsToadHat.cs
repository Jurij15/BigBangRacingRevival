using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001BF RID: 447
public class VisualsToadHat : MonoBehaviour
{
	// Token: 0x06000DAE RID: 3502 RVA: 0x000806A4 File Offset: 0x0007EAA4
	private void Awake()
	{
		try
		{
			this.m_soundEntity = EntityManager.AddEntity();
		}
		catch (Exception ex)
		{
			this.m_soundEntity = null;
		}
		this.leftToadEye.eyeObject = this.leftEye;
		this.leftToadEye.eyeStartRotation = this.leftEye.transform.localRotation;
		this.leftEyeRenderer = this.leftToadEye.eyeObject.GetComponent<MeshRenderer>();
		this.rightToadEye.eyeObject = this.rightEye;
		this.rightToadEye.eyeStartRotation = this.rightEye.transform.localRotation;
		this.rightEyeRenderer = this.rightToadEye.eyeObject.GetComponent<MeshRenderer>();
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00080764 File Offset: 0x0007EB64
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

	// Token: 0x06000DB0 RID: 3504 RVA: 0x00080786 File Offset: 0x0007EB86
	public void PoopEyesOn()
	{
		this.poopActive = true;
		this.leftEyeRenderer.material = this.eyesClosedMat;
		this.rightEyeRenderer.material = this.eyesClosedMat;
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x000807B1 File Offset: 0x0007EBB1
	public void PoopEyesOff()
	{
		this.poopActive = false;
		this.leftEyeRenderer.material = this.eyesOpenMat;
		this.rightEyeRenderer.material = this.eyesOpenMat;
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x000807DC File Offset: 0x0007EBDC
	private void Update()
	{
		if (this.debugMode)
		{
			if (Input.GetKeyDown(49))
			{
				this.toadAnimator.SetTrigger("Ribbit");
			}
			else if (Input.GetKeyDown(50))
			{
				this.toadAnimator.SetTrigger("Poop");
				this.poopActive = true;
				this.poopEffect.Play();
			}
		}
		else if (Main.m_currentGame.m_currentScene is PsMenuScene || this.m_forceAnimations || (PsState.m_activeMinigame != null && !PsState.m_activeMinigame.m_gamePaused && PsState.m_activeMinigame.m_gameStarted && !PsState.m_activeMinigame.m_gameEnded))
		{
			if (this.actionTimer <= 0f)
			{
				this.actionTimer = Random.Range(4f, 10f);
				Vector3 position = base.transform.position;
				if (Main.m_currentGame.m_currentScene is PsMenuScene || (PsState.m_activeMinigame != null && (!PsState.m_activeMinigame.m_gameStarted || PsState.m_activeMinigame.m_gameEnded || PsState.m_activeMinigame.m_gamePaused)))
				{
					position = CameraS.m_mainCamera.transform.position;
				}
				if (Random.Range(0f, 1f) > 0.3f)
				{
					this.toadAnimator.SetTrigger("Ribbit");
					SoundS.PlaySingleShot("/Ingame/Units/ToadCroak", position, 1f);
				}
				else if (this.m_soundEntity != null)
				{
					this.toadAnimator.SetTrigger("Poop");
					TimerS.AddComponent(this.m_soundEntity, "ToadFart", 0.9f, 0f, false, delegate(TimerC _c)
					{
						TimerS.RemoveComponent(_c);
						SoundS.PlaySingleShotWithParameter("/Ingame/Units/ToadFart", position, "Part", 0f, 1f);
						TimerS.AddComponent(this.m_soundEntity, "ToadFart", 0.75f, 0f, false, delegate(TimerC _c2)
						{
							TimerS.RemoveComponent(_c2);
							SoundS.PlaySingleShotWithParameter("/Ingame/Units/ToadFart", position, "Part", 1f, 1f);
							TimerS.AddComponent(this.m_soundEntity, "ToadFart", 0.3f, 0f, false, delegate(TimerC _c3)
							{
								TimerS.RemoveComponent(_c3);
								SoundS.PlaySingleShotWithParameter("/Ingame/Units/ToadFart", position, "Part", 2f, 1f);
							});
						});
					});
					this.poopActive = true;
					this.poopEffect.Play();
				}
			}
			else
			{
				this.actionTimer -= Main.GetDeltaTime();
			}
		}
		if (this.eyeMovementTimerLeft <= 0f && !this.poopActive)
		{
			this.eyeMovementTimerLeft = Random.Range(0.25f, 2f);
			base.StartCoroutine("RandomOrientEye", this.rightToadEye);
		}
		else
		{
			this.eyeMovementTimerLeft -= Main.GetDeltaTime();
		}
		if (this.eyeMovementTimerRight <= 0f && !this.poopActive)
		{
			this.eyeMovementTimerRight = Random.Range(0.25f, 2f);
			base.StartCoroutine("RandomOrientEye", this.leftToadEye);
		}
		else
		{
			this.eyeMovementTimerRight -= Main.GetDeltaTime();
		}
		if (this.blinkTimer <= 0f && !this.poopActive)
		{
			this.leftEyeRenderer.material = this.eyesClosedMat;
			this.rightEyeRenderer.material = this.eyesClosedMat;
			if (this.blinkDurationTimer <= 0f)
			{
				this.blinkDurationTimer = Random.Range(0.1f, 0.25f);
				this.blinkTimer = Random.Range(0.5f, 2.5f);
				this.leftEyeRenderer.material = this.eyesOpenMat;
				this.rightEyeRenderer.material = this.eyesOpenMat;
			}
			else
			{
				this.blinkDurationTimer -= Main.GetDeltaTime();
			}
		}
		else
		{
			this.blinkTimer -= Main.GetDeltaTime();
		}
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x00080B73 File Offset: 0x0007EF73
	public void OnDestroy()
	{
		if (this.m_soundEntity != null)
		{
			EntityManager.RemoveEntity(this.m_soundEntity);
			this.m_soundEntity = null;
		}
	}

	// Token: 0x04001045 RID: 4165
	public bool debugMode;

	// Token: 0x04001046 RID: 4166
	public Animator toadAnimator;

	// Token: 0x04001047 RID: 4167
	private float actionTimer = 2f;

	// Token: 0x04001048 RID: 4168
	private bool poopActive;

	// Token: 0x04001049 RID: 4169
	public ParticleSystem poopEffect;

	// Token: 0x0400104A RID: 4170
	public GameObject leftEye;

	// Token: 0x0400104B RID: 4171
	public GameObject rightEye;

	// Token: 0x0400104C RID: 4172
	private float eyeRotationAmount = 25f;

	// Token: 0x0400104D RID: 4173
	private float rotationSpeed = 0.1f;

	// Token: 0x0400104E RID: 4174
	private float eyeMovementTime = 2f;

	// Token: 0x0400104F RID: 4175
	private float eyeMovementTimerLeft;

	// Token: 0x04001050 RID: 4176
	private float eyeMovementTimerRight;

	// Token: 0x04001051 RID: 4177
	public Material eyesClosedMat;

	// Token: 0x04001052 RID: 4178
	public Material eyesOpenMat;

	// Token: 0x04001053 RID: 4179
	private Renderer leftEyeRenderer;

	// Token: 0x04001054 RID: 4180
	private Renderer rightEyeRenderer;

	// Token: 0x04001055 RID: 4181
	private float blinkTimer;

	// Token: 0x04001056 RID: 4182
	private float blinkDurationTimer;

	// Token: 0x04001057 RID: 4183
	private FishEye leftToadEye;

	// Token: 0x04001058 RID: 4184
	private FishEye rightToadEye;

	// Token: 0x04001059 RID: 4185
	private Entity m_soundEntity;

	// Token: 0x0400105A RID: 4186
	public bool m_forceAnimations;
}
