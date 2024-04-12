using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class VisualsWinterLights : MonoBehaviour
{
	// Token: 0x06000E02 RID: 3586 RVA: 0x00082F26 File Offset: 0x00081326
	private void Awake()
	{
		this.lightsData = new List<VisualsWinterLights.LightData>();
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x00082F33 File Offset: 0x00081333
	private void Start()
	{
		this.UpdateLine();
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x00082F3C File Offset: 0x0008133C
	private void UpdateLine()
	{
		float num = Vector3.Distance(this.startObject.transform.position, this.endObject.transform.position);
		if (num < 0f)
		{
			num = 0f;
		}
		float num2 = Mathf.Abs(Vector3.Dot(new Vector3(1f, 0f, 0f), Vector3.Normalize(this.startObject.transform.position - this.endObject.transform.position)));
		float num3 = num * num * this.lineLooseness * 0.001f * num2;
		int num4 = Mathf.RoundToInt(num * this.lineSmoothness);
		if (num4 <= 4)
		{
			num4 = 5;
		}
		this.lineRenderer.SetVertexCount(num4);
		Vector3 vector = this.startObject.transform.position - this.endObject.transform.position;
		for (int i = 0; i < num4; i++)
		{
			float num5 = (float)i / ((float)num4 - 1f);
			Vector3 vector2 = vector.normalized * num5 * num;
			Vector3 vector3 = this.startObject.transform.position - vector2;
			vector3..ctor(vector3.x, vector3.y - Mathf.Abs(Mathf.Sin(3.1415927f * num5 * ((float)this.midPoints + 1f))) * num3, vector3.z);
			this.lineRenderer.SetPosition(i, vector3);
		}
		int num6 = Mathf.FloorToInt(num * this.lightDensity) - 2;
		if (num6 <= 0)
		{
			num6 = 1;
		}
		this.PoolLights(num6);
		for (int j = 1; j < num6 + 1; j++)
		{
			float num7 = (float)j / ((float)num6 + 1f);
			Vector3 vector4 = vector.normalized * num7 * num;
			Vector3 vector5 = this.startObject.transform.position - vector4;
			vector5..ctor(vector5.x, vector5.y - Mathf.Abs(Mathf.Sin(3.1415927f * num7 * ((float)this.midPoints + 1f))) * num3, vector5.z);
			this.lightsData[j - 1].lightObject.SetActive(true);
			this.lightsData[j - 1].lightObject.transform.position = vector5;
		}
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x000831BC File Offset: 0x000815BC
	private void PoolLights(int lightsAmount)
	{
		int num = lightsAmount - this.lightsData.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.lightObjects[Random.Range(0, this.lightObjects.Length)], base.gameObject.transform);
				GameObject gameObject2 = gameObject;
				gameObject2.name += this.lightsData.Count;
				gameObject.transform.Rotate(Random.Range(-45f, 45f), 0f, Random.Range(-45f, 45f));
				VisualsWinterLights.LightData lightData = new VisualsWinterLights.LightData();
				lightData.lightObject = gameObject;
				if (this.lightStartStateSwitch)
				{
					lightData.lightObject.GetComponent<MeshRenderer>().material = this.lightOffMaterial;
					lightData.lightOn = true;
					this.lightStartStateSwitch = false;
				}
				else
				{
					lightData.lightObject.GetComponent<MeshRenderer>().material = this.lightOnMaterial;
					lightData.lightOn = false;
					this.lightStartStateSwitch = true;
				}
				this.lightsData.Add(lightData);
			}
		}
		for (int j = lightsAmount; j < this.lightsData.Count; j++)
		{
			this.lightsData[j].lightObject.SetActive(false);
		}
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x0008330E File Offset: 0x0008170E
	public void SetPowerMode(bool _mode)
	{
		this.m_powerOn = _mode;
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x00083318 File Offset: 0x00081718
	public void ToggleLights()
	{
		if (!this.m_powerOn)
		{
			return;
		}
		for (int i = 0; i < this.lightsData.Count; i++)
		{
			if (this.lightsData[i].lightOn)
			{
				this.lightsData[i].lightObject.GetComponent<MeshRenderer>().material = this.lightOffMaterial;
				this.lightsData[i].lightOn = false;
			}
			else
			{
				this.lightsData[i].lightObject.GetComponent<MeshRenderer>().material = this.lightOnMaterial;
				this.lightsData[i].lightOn = true;
			}
		}
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x000833D0 File Offset: 0x000817D0
	private void Update()
	{
		if (this.startObject.transform.hasChanged || this.endObject.transform.hasChanged)
		{
			this.UpdateLine();
			this.startObject.transform.hasChanged = false;
			this.endObject.transform.hasChanged = false;
		}
		if (this.lightSwitchTimer <= 0f)
		{
			this.lightSwitchTimer = this.lightSwitchInterval;
			this.lightStartStateSwitch = !this.lightStartStateSwitch;
			this.ToggleLights();
		}
		else
		{
			this.lightSwitchTimer -= Main.GetDeltaTime();
		}
	}

	// Token: 0x040010FB RID: 4347
	public GameObject startObject;

	// Token: 0x040010FC RID: 4348
	public GameObject endObject;

	// Token: 0x040010FD RID: 4349
	public LineRenderer lineRenderer;

	// Token: 0x040010FE RID: 4350
	public float lineSmoothness = 1f;

	// Token: 0x040010FF RID: 4351
	public float lineLooseness = 10f;

	// Token: 0x04001100 RID: 4352
	public int midPoints;

	// Token: 0x04001101 RID: 4353
	public Material lightOnMaterial;

	// Token: 0x04001102 RID: 4354
	public Material lightOffMaterial;

	// Token: 0x04001103 RID: 4355
	public float lightDensity = 1f;

	// Token: 0x04001104 RID: 4356
	public GameObject[] lightObjects;

	// Token: 0x04001105 RID: 4357
	private List<VisualsWinterLights.LightData> lightsData;

	// Token: 0x04001106 RID: 4358
	public float lightSwitchInterval = 0.5f;

	// Token: 0x04001107 RID: 4359
	private float lightSwitchTimer;

	// Token: 0x04001108 RID: 4360
	private bool lightStartStateSwitch;

	// Token: 0x04001109 RID: 4361
	public bool m_powerOn = true;

	// Token: 0x020001D1 RID: 465
	private class LightData
	{
		// Token: 0x0400110A RID: 4362
		public bool lightOn;

		// Token: 0x0400110B RID: 4363
		public GameObject lightObject;
	}
}
