using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public class EffectGemMeteorStorm : MonoBehaviour
{
	// Token: 0x06000DBC RID: 3516 RVA: 0x0008103C File Offset: 0x0007F43C
	private void Start()
	{
		this.mainCamera = Camera.main;
		this.atmoBurnMat = this.meteorAtmoBurnObject.GetComponent<Renderer>().material;
		this.smallMeteorPool = new List<GameObject>();
		this.activeSmallMeteors = new List<GameObject>();
		for (int i = 0; i < this.pooledSmallMeteorAmount; i++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.smallMeteorPrefab);
			gameObject.transform.SetParent(base.transform);
			gameObject.SetActive(false);
			this.smallMeteorPool.Add(gameObject);
			gameObject.GetComponent<EffectGemMeteorStormSmallMeteor>().meteorExplosion = this.meteorExplosion;
		}
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x000810D8 File Offset: 0x0007F4D8
	private void Update()
	{
		if (PsState.m_activeMinigame != null && PsState.m_activeMinigame.m_gamePaused)
		{
			return;
		}
		float deltaTime = Main.GetDeltaTime();
		this.meteorObject.transform.Rotate(this.rotationSpeed * deltaTime);
		this.atmoBurnSideAlphaOffset += deltaTime * this.atmoBurnSideAlphaSpeed;
		this.atmoBurnTopAlphaOffset += deltaTime * this.atmoBurnTopAlphaSpeed;
		if (this.atmoBurnSideAlphaOffset > 1f)
		{
			this.atmoBurnSideAlphaOffset -= 1f;
		}
		if (this.atmoBurnTopAlphaOffset > 1f)
		{
			this.atmoBurnTopAlphaOffset -= 1f;
		}
		this.atmoBurnMat.SetTextureOffset("_SideAlpha", new Vector2(0f, this.atmoBurnSideAlphaOffset));
		this.atmoBurnMat.SetTextureOffset("_TopAlpha", new Vector2(this.atmoBurnTopAlphaOffset, 0f));
		this.atmoBurnScaler += deltaTime * 2.5f;
		float num = 1f + Mathf.PerlinNoise(this.atmoBurnScaler, 0f) * 0.25f;
		float num2 = 1f + Mathf.PerlinNoise(this.atmoBurnScaler, 0.5f) * 0.25f;
		this.atmoBurnHaloObject.transform.localScale = new Vector3(num, num2, 1f);
		if (this.smallMeteorSpawnTimer < 0f && this.smallMeteorPool.Count > 0)
		{
			GameObject gameObject = this.smallMeteorPool[0];
			this.smallMeteorPool.RemoveAt(0);
			gameObject.transform.position = new Vector3(Random.Range(6000f, 15000f) + this.mainCamera.transform.position.x, 10000f + this.mainCamera.transform.position.y, Random.Range(9000f, 15000f) + this.mainCamera.transform.position.z);
			gameObject.SetActive(true);
			gameObject.GetComponent<EffectGemMeteorStormSmallMeteor>().explosionToggled = false;
			this.activeSmallMeteors.Add(gameObject);
			this.smallMeteorSpawnTimer = Random.Range(this.smallMeteorMinSpawnTime, this.smallMeteorMaxSpawnTime);
		}
		else
		{
			this.smallMeteorSpawnTimer -= Main.GetDeltaTime();
		}
		for (int i = this.activeSmallMeteors.Count - 1; i > -1; i--)
		{
			GameObject gameObject2 = this.activeSmallMeteors[i];
			gameObject2.transform.Translate(-Vector3.right * this.smallMeteorSpeed);
			if (gameObject2.transform.position.y < -2000f + this.mainCamera.transform.position.y)
			{
				gameObject2.SetActive(false);
				this.smallMeteorPool.Add(gameObject2);
				this.activeSmallMeteors.RemoveAt(i);
			}
		}
	}

	// Token: 0x04001067 RID: 4199
	public GameObject meteorObject;

	// Token: 0x04001068 RID: 4200
	public Vector3 rotationSpeed;

	// Token: 0x04001069 RID: 4201
	public GameObject meteorAtmoBurnObject;

	// Token: 0x0400106A RID: 4202
	public float atmoBurnSideAlphaSpeed = 1f;

	// Token: 0x0400106B RID: 4203
	public float atmoBurnTopAlphaSpeed = 1f;

	// Token: 0x0400106C RID: 4204
	private float atmoBurnSideAlphaOffset;

	// Token: 0x0400106D RID: 4205
	private float atmoBurnTopAlphaOffset;

	// Token: 0x0400106E RID: 4206
	private Material atmoBurnMat;

	// Token: 0x0400106F RID: 4207
	public GameObject atmoBurnHaloObject;

	// Token: 0x04001070 RID: 4208
	private float atmoBurnScaler;

	// Token: 0x04001071 RID: 4209
	public GameObject smallMeteorPrefab;

	// Token: 0x04001072 RID: 4210
	public int pooledSmallMeteorAmount;

	// Token: 0x04001073 RID: 4211
	public float smallMeteorMinSpawnTime = 1f;

	// Token: 0x04001074 RID: 4212
	public float smallMeteorMaxSpawnTime;

	// Token: 0x04001075 RID: 4213
	public float smallMeteorSpeed = 1f;

	// Token: 0x04001076 RID: 4214
	private float smallMeteorSpawnTimer;

	// Token: 0x04001077 RID: 4215
	private List<GameObject> smallMeteorPool;

	// Token: 0x04001078 RID: 4216
	private List<GameObject> activeSmallMeteors;

	// Token: 0x04001079 RID: 4217
	public ParticleSystem meteorExplosion;

	// Token: 0x0400107A RID: 4218
	private Camera mainCamera;
}
