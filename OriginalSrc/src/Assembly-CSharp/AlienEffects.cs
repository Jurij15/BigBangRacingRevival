using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B8 RID: 440
public class AlienEffects : MonoBehaviour
{
	// Token: 0x06000D93 RID: 3475 RVA: 0x0007EEAD File Offset: 0x0007D2AD
	private void Awake()
	{
		this.Initialize();
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x0007EEB8 File Offset: 0x0007D2B8
	public void Initialize()
	{
		this.alienAnimator = base.GetComponent<Animator>();
		this.bodyMat = base.transform.Find("Body").GetComponent<SkinnedMeshRenderer>().material;
		base.transform.Find("Body").GetComponent<SkinnedMeshRenderer>().sharedMaterial = this.bodyMat;
		base.transform.Find("Hips/Spine1/Spine2/Neck/Head/EyeLids").GetComponent<SkinnedMeshRenderer>().sharedMaterial = this.bodyMat;
		base.transform.Find("Hips/Spine1/Spine2/Neck/Head/Mouth").GetComponent<SkinnedMeshRenderer>().sharedMaterial = this.bodyMat;
		this.bodyMeshes = new SkinnedMeshRenderer[3];
		this.bodyMeshes[0] = base.transform.Find("Body").GetComponent<SkinnedMeshRenderer>();
		this.bodyMeshes[1] = base.transform.Find("Hips/Spine1/Spine2/Neck/Head/EyeLids").GetComponent<SkinnedMeshRenderer>();
		this.bodyMeshes[2] = base.transform.Find("Hips/Spine1/Spine2/Neck/Head/Mouth").GetComponent<SkinnedMeshRenderer>();
		this.eyeMat = base.transform.Find("Hips/Spine1/Spine2/Neck/Head/LeftEye/EyeLeft").GetComponent<MeshRenderer>().material;
		base.transform.Find("Hips/Spine1/Spine2/Neck/Head/LeftEye/EyeLeft").GetComponent<MeshRenderer>().sharedMaterial = this.eyeMat;
		base.transform.Find("Hips/Spine1/Spine2/Neck/Head/RightEye/EyeRight").GetComponent<MeshRenderer>().sharedMaterial = this.eyeMat;
		this.eyeBalls = new MeshRenderer[2];
		this.eyeBalls[0] = base.transform.Find("Hips/Spine1/Spine2/Neck/Head/LeftEye/EyeLeft").GetComponent<MeshRenderer>();
		this.eyeBalls[1] = base.transform.Find("Hips/Spine1/Spine2/Neck/Head/RightEye/EyeRight").GetComponent<MeshRenderer>();
		this.bones = new List<GameObject>();
		this.FindBones(base.gameObject);
		this.boneMat = this.bones[0].GetComponent<MeshRenderer>().material;
		foreach (GameObject gameObject in this.bones)
		{
			gameObject.GetComponent<MeshRenderer>().sharedMaterial = this.boneMat;
		}
		this.bones.Add(base.transform.Find("Hips/Spine1/Spine2/Neck/Head/AlienBones_Skull/EyeSocketLeft").gameObject);
		this.bones.Add(base.transform.Find("Hips/Spine1/Spine2/Neck/Head/AlienBones_Skull/EyeSocketRight").gameObject);
		this.boneColorWhite = new Color32(218, 208, 190, byte.MaxValue);
		this.boneColorBlack = new Color32(0, 0, 0, byte.MaxValue);
		this.boneColorBurned = new Color32(103, 99, 92, byte.MaxValue);
		this.particleEffectA = base.transform.Find("Hips/Spine1/Spine2/ElectricBurst").GetComponent<ParticleSystem>();
		this.particleEffectA.gameObject.SetActive(false);
		this.particleEffectB = base.transform.Find("Hips/Spine1/Spine2/EffectSmoke").GetComponent<ParticleSystem>();
		this.particleEffectB.gameObject.SetActive(false);
		this.alienExplosion = base.transform.Find("Hips/Spine1/Spine2/Neck/EffectAlienSplurt").GetComponent<ParticleSystem>();
		this.alienExplosion.gameObject.SetActive(false);
		this.particleEffectStreaks = new ParticleSystem[3];
		this.particleEffectStreaks[0] = base.transform.Find("Hips/Spine1/Spine2/Neck/Head/EffectElectrocutionStreaks").GetComponent<ParticleSystem>();
		this.particleEffectStreaks[1] = base.transform.Find("Hips/Spine1/Spine2/EffectElectrocutionStreaks").GetComponent<ParticleSystem>();
		this.particleEffectStreaks[2] = base.transform.Find("Hips/Spine1/Spine2/Neck/EffectElectrocutionStreaks").GetComponent<ParticleSystem>();
		this.headBone = base.transform.Find("Hips/Spine1/Spine2/Neck/Head");
		foreach (ParticleSystem particleSystem in this.particleEffectStreaks)
		{
			particleSystem.GetComponent<Renderer>().sharedMaterial = this.bodyMat;
		}
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x0007F2A0 File Offset: 0x0007D6A0
	public void FindBones(GameObject obj)
	{
		IEnumerator enumerator = obj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj2 = enumerator.Current;
				Transform transform = (Transform)obj2;
				if (transform.name.StartsWith("AlienBones_"))
				{
					this.bones.Add(transform.gameObject);
				}
				this.FindBones(transform.gameObject);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x0007F330 File Offset: 0x0007D730
	public void Explode()
	{
		foreach (GameObject gameObject in this.bones)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in this.bodyMeshes)
		{
			skinnedMeshRenderer.enabled = false;
		}
		foreach (MeshRenderer meshRenderer in this.eyeBalls)
		{
			meshRenderer.enabled = false;
		}
		this.alienExplosion.gameObject.SetActive(true);
		this.alienExplosion.Play();
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x0007F408 File Offset: 0x0007D808
	public void PlayElectrocute()
	{
		this.bodyMat.shader = Shader.Find("WOE/Units/UnlitBackface");
		this.bodyMat.SetColor("_Color", new Color(1f, 1f, 1f));
		this.eyeMat.shader = Shader.Find("WOE/Units/UnlitBackface");
		this.eyeMat.SetColor("_Color", new Color(0f, 0f, 0f));
		this.boneMat.SetColor("_Color", this.boneColorBlack);
		this.particleEffectA.gameObject.SetActive(true);
		this.particleEffectB.gameObject.SetActive(true);
		this.particleEffectA.Play();
		this.particleEffectB.Play();
		foreach (GameObject gameObject in this.bones)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
		this.isElectrocuting = true;
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x0007F538 File Offset: 0x0007D938
	public void StopElectrocute()
	{
		this.bodyMat.shader = Shader.Find("WOE/Units/ReflectiveSpecularRimUnit");
		this.bodyMat.SetColor("_Color", new Color(1f, 1f, 1f));
		this.eyeMat.shader = Shader.Find("WOE/Units/ReflectiveSpecularRimUnit");
		this.eyeMat.SetColor("_Color", new Color(1f, 1f, 1f));
		this.boneMat.SetColor("_Color", this.boneColorWhite);
		foreach (GameObject gameObject in this.bones)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		this.particleEffectA.Stop();
		this.particleEffectB.Stop();
		this.particleEffectA.gameObject.SetActive(false);
		this.particleEffectB.gameObject.SetActive(false);
		foreach (GameObject gameObject2 in this.bones)
		{
			gameObject2.GetComponent<MeshRenderer>().enabled = false;
		}
		this.isElectrocuting = false;
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x0007F6B8 File Offset: 0x0007DAB8
	public void Electrocute()
	{
		this.flashTimer -= Main.GetDeltaTime();
		if (this.flashTimer <= 0f)
		{
			if (this.swapperToggle)
			{
				this.boneMat.SetColor("_Color", this.boneColorBlack);
				this.bodyMat.SetColor("_Color", new Color(1f, 1f, 1f));
				this.eyeMat.SetColor("_Color", new Color(1f, 1f, 1f));
				this.swapperToggle = false;
			}
			else if (!this.swapperToggle)
			{
				this.boneMat.SetColor("_Color", this.boneColorWhite);
				this.bodyMat.SetColor("_Color", new Color(0f, 0f, 0f));
				this.eyeMat.SetColor("_Color", new Color(0f, 0f, 0f));
				this.swapperToggle = true;
			}
			this.flashTimer = Random.Range(0.01f, 0.1f);
		}
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x0007F7EB File Offset: 0x0007DBEB
	public void WobbleHead()
	{
		this.headWobbling = true;
		this.alienAnimator.SetTrigger("Blink");
		this.wobbleTimer = this.wobbleTime;
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x0007F810 File Offset: 0x0007DC10
	private void WobbleHeadUpdate()
	{
		if (this.wobbleTimer <= 0f)
		{
			this.headWobbling = false;
			this.headBone.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			this.wobbleTimer -= Main.GetDeltaTime();
			float num = this.wobbleCurve.Evaluate(1f - this.wobbleTimer / this.wobbleTime);
			float num2 = 2f - this.wobbleCurve.Evaluate(1f - this.wobbleTimer / this.wobbleTime);
			this.headBone.localScale = new Vector3(num2, num, num2);
		}
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0007F8C4 File Offset: 0x0007DCC4
	public IEnumerator ElectrocuteRoutine()
	{
		float effectTimer = this.electroCuteLength;
		float flashTimer = 0.15f;
		bool swapperToggle = true;
		this.bodyMat.shader = Shader.Find("WOE/Units/UnlitBackface");
		this.bodyMat.SetColor("_Color", new Color(1f, 1f, 1f));
		this.eyeMat.shader = Shader.Find("WOE/Units/UnlitBackface");
		this.eyeMat.SetColor("_Color", new Color(0f, 0f, 0f));
		this.boneMat.SetColor("_Color", this.boneColorBlack);
		this.particleEffectA.Play();
		foreach (GameObject gameObject in this.bones)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
		this.particleEffectB.Play();
		while (effectTimer > 0f)
		{
			effectTimer -= Main.GetDeltaTime();
			flashTimer -= Main.GetDeltaTime();
			if (flashTimer <= 0f)
			{
				if (swapperToggle)
				{
					this.boneMat.SetColor("_Color", this.boneColorBlack);
					this.bodyMat.SetColor("_Color", new Color(1f, 1f, 1f));
					this.eyeMat.SetColor("_Color", new Color(1f, 1f, 1f));
					swapperToggle = false;
				}
				else if (!swapperToggle)
				{
					this.boneMat.SetColor("_Color", this.boneColorWhite);
					this.bodyMat.SetColor("_Color", new Color(0f, 0f, 0f));
					this.eyeMat.SetColor("_Color", new Color(0f, 0f, 0f));
					swapperToggle = true;
				}
				flashTimer = Random.Range(0.01f, 0.1f);
			}
			yield return null;
		}
		this.bodyMat.shader = Shader.Find("WOE/Units/ReflectiveSpecularRimUnit");
		this.bodyMat.SetColor("_Color", new Color(1f, 1f, 1f));
		this.eyeMat.shader = Shader.Find("WOE/Units/ReflectiveSpecularRimUnit");
		this.eyeMat.SetColor("_Color", new Color(1f, 1f, 1f));
		this.boneMat.SetColor("_Color", this.boneColorWhite);
		foreach (GameObject gameObject2 in this.bones)
		{
			gameObject2.GetComponent<MeshRenderer>().enabled = false;
		}
		yield break;
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x0007F8DF File Offset: 0x0007DCDF
	private void Update()
	{
		if (this.isElectrocuting)
		{
			this.Electrocute();
		}
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0007F8F2 File Offset: 0x0007DCF2
	private void LateUpdate()
	{
		if (this.headWobbling)
		{
			this.WobbleHeadUpdate();
		}
	}

	// Token: 0x0400100B RID: 4107
	private Animator alienAnimator;

	// Token: 0x0400100C RID: 4108
	private SkinnedMeshRenderer[] bodyMeshes;

	// Token: 0x0400100D RID: 4109
	private MeshRenderer[] eyeBalls;

	// Token: 0x0400100E RID: 4110
	private List<GameObject> bones;

	// Token: 0x0400100F RID: 4111
	private List<GameObject> eyeShadows;

	// Token: 0x04001010 RID: 4112
	private float electroCuteLength = 1.25f;

	// Token: 0x04001011 RID: 4113
	private ParticleSystem particleEffectA;

	// Token: 0x04001012 RID: 4114
	private ParticleSystem particleEffectB;

	// Token: 0x04001013 RID: 4115
	private ParticleSystem[] particleEffectStreaks;

	// Token: 0x04001014 RID: 4116
	private ParticleSystem alienExplosion;

	// Token: 0x04001015 RID: 4117
	private Material bodyMat;

	// Token: 0x04001016 RID: 4118
	private Material eyeMat;

	// Token: 0x04001017 RID: 4119
	private Material boneMat;

	// Token: 0x04001018 RID: 4120
	private Color32 boneColorWhite;

	// Token: 0x04001019 RID: 4121
	private Color32 boneColorBlack;

	// Token: 0x0400101A RID: 4122
	private Color32 boneColorBurned;

	// Token: 0x0400101B RID: 4123
	private float colorSwapSpeed = 0.1f;

	// Token: 0x0400101C RID: 4124
	private bool isElectrocuting;

	// Token: 0x0400101D RID: 4125
	private float flashTimer = 0.15f;

	// Token: 0x0400101E RID: 4126
	private bool swapperToggle = true;

	// Token: 0x0400101F RID: 4127
	private Transform headBone;

	// Token: 0x04001020 RID: 4128
	private bool headWobbling;

	// Token: 0x04001021 RID: 4129
	private float wobbleTime = 0.375f;

	// Token: 0x04001022 RID: 4130
	private float wobbleTimer;

	// Token: 0x04001023 RID: 4131
	public AnimationCurve wobbleCurve;
}
