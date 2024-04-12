using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class PsTrailBase : MonoBehaviour
{
	// Token: 0x060003F1 RID: 1009 RVA: 0x00037F74 File Offset: 0x00036374
	public void Init()
	{
		this.m_initialized = true;
		this.m_entity = EntityManager.AddEntity();
		this.m_tc = TransformS.AddComponent(this.m_entity, "Trail");
		this.m_pc = PrefabS.AddComponent(this.m_tc, Vector3.zero);
		this.m_pc.p_gameObject.GetComponent<Renderer>().material = this.m_material;
		PrefabS.SetCamera(this.m_pc, CameraS.m_mainCamera);
		this.m_points = new List<TrailData>();
		this.m_shapeByAgeTable = new AnimationCurveLookupTable(this.m_shapeByAge, 128);
		if (this.m_idleParticles != null)
		{
			this.m_idleParticles.Play();
		}
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00038027 File Offset: 0x00036427
	public void SetBoostParticleStartSpeed(int _value)
	{
		if (this.m_boostParticles != null)
		{
			this.SetBoostParticleStartSpeed(this.m_boostParticles, _value);
		}
		if (this.m_idleParticles != null)
		{
			this.SetBoostParticleStartSpeed(this.m_idleParticles, _value);
		}
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00038068 File Offset: 0x00036468
	public void SetBoostParticleStartSpeed(ParticleSystem _particles, int _value)
	{
		ParticleSystem[] componentsInChildren = _particles.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].startSpeed = (float)_value;
		}
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0003809C File Offset: 0x0003649C
	public void SetPreviewMode(Vector3 _pos, int _cameraLayer)
	{
		this.m_tc.transform.position = _pos;
		this.m_previewMode = true;
		this.SetBoostActive(true);
		PrefabS.SetCameraLayer(this.m_pc.p_gameObject, _cameraLayer);
		if (this.m_boostParticles != null)
		{
			PrefabS.SetCameraLayer(this.m_boostParticles.gameObject, _cameraLayer);
			this.SetPreviewEmission(this.m_boostParticles);
		}
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00038108 File Offset: 0x00036508
	public void SetPreviewMode(Transform _parent, Vector3 _offset, int _cameraLayer)
	{
		this.m_tc.transform.SetParent(_parent);
		this.m_tc.transform.localPosition = _offset;
		this.m_previewMode = true;
		this.SetBoostActive(true);
		PrefabS.SetCameraLayer(this.m_pc.p_gameObject, _cameraLayer);
		if (this.m_boostParticles != null)
		{
			PrefabS.SetCameraLayer(this.m_boostParticles.gameObject, _cameraLayer);
			this.SetPreviewEmission(this.m_boostParticles);
		}
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00038184 File Offset: 0x00036584
	public void SetPreviewEmission(ParticleSystem _particles)
	{
		foreach (ParticleSystem particleSystem in _particles.GetComponentsInChildren<ParticleSystem>())
		{
			Renderer component = particleSystem.GetComponent<Renderer>();
			if (component != null && component.material != null)
			{
				component.material.renderQueue++;
			}
			Vector3 localPosition = particleSystem.transform.localPosition;
			localPosition.z = 0f;
			particleSystem.transform.localPosition = localPosition;
			ParticleSystem.EmissionModule emission = particleSystem.emission;
			if (emission.type != null)
			{
				emission.type = 0;
				ParticleSystem.MinMaxCurve rate = emission.rate;
				rate.constantMax *= 500f;
				emission.rate = rate;
			}
		}
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00038250 File Offset: 0x00036650
	public void SetGhost()
	{
		this.m_isGhost = true;
		if (this.m_boostParticles != null)
		{
			this.SetParticleSystemStartColorAlpha(this.m_boostParticles, this.m_maximumGhostAlpha);
		}
		if (this.m_idleParticles != null)
		{
			this.SetParticleSystemStartColorAlpha(this.m_idleParticles, this.m_maximumGhostAlpha);
		}
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x000382AC File Offset: 0x000366AC
	public void SetParticleSystemStartColorAlpha(ParticleSystem _particles, float _alpha)
	{
		foreach (ParticleSystem particleSystem in _particles.GetComponentsInChildren<ParticleSystem>())
		{
			particleSystem.GetComponent<Renderer>().material.SetFloat("_AlphaFade", _alpha);
		}
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x000382F0 File Offset: 0x000366F0
	public void SetPoint(TrailData _td)
	{
		if (!this.m_trail)
		{
			return;
		}
		if (this.m_points.Count > 1 && this.skip == 1)
		{
			this.m_points.RemoveAt(0);
			this.skip *= -1;
		}
		this.m_points.Insert(0, _td);
		while (this.m_points.Count > this.m_maxLength)
		{
			this.m_points.RemoveAt(this.m_points.Count - 1);
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00038380 File Offset: 0x00036780
	public void SetTeleporting(bool _value)
	{
		if (this.m_teleporting != _value)
		{
			this.m_teleporting = _value;
			if (!this.m_teleporting)
			{
				this.m_points.Clear();
				this.m_teleportEnd = true;
			}
			this.m_fadeMultipler = 0f;
			if (this.m_teleporting)
			{
				if (this.m_boostParticles != null)
				{
					this.m_boostParticles.Stop();
				}
				if (this.m_idleParticles != null)
				{
					this.m_idleParticles.Stop();
				}
			}
			else if (this.m_boostActive)
			{
				if (this.m_boostParticles != null)
				{
					this.m_boostParticles.Play();
				}
			}
			else if (this.m_idleParticles != null)
			{
				this.m_idleParticles.Play();
			}
		}
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00038458 File Offset: 0x00036858
	public void SetBoostActive(bool _value)
	{
		if (this.m_boostActive != _value)
		{
			this.m_boostActive = _value;
			if (this.m_boostActive)
			{
				if (this.m_boostParticles != null)
				{
					this.m_boostParticles.Play();
				}
				if (this.m_idleParticles != null)
				{
					this.m_idleParticles.Stop();
				}
			}
			else
			{
				if (this.m_boostParticles != null)
				{
					this.m_boostParticles.Stop();
				}
				if (this.m_idleParticles != null)
				{
					this.m_idleParticles.Play();
				}
			}
		}
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x000384F8 File Offset: 0x000368F8
	private void Update()
	{
		if (!this.m_previewMode)
		{
			if (this.m_pc != null && this.m_pc.m_active && PsState.m_activeMinigame != null && !PsState.m_activeMinigame.m_gamePaused)
			{
				this.m_speed = (base.gameObject.transform.position - this.m_tc.transform.position).magnitude;
				this.m_tc.transform.position = base.gameObject.transform.position;
				float num = this.m_maximumIdleTrailAlpha;
				if (this.m_boostActive)
				{
					num = 1f;
				}
				if (this.m_fadeMultipler < this.m_maximumIdleTrailAlpha)
				{
					this.m_fadeMultipler = this.m_maximumIdleTrailAlpha;
				}
				if (this.m_boostActive)
				{
					if (this.m_fadeMultipler < num)
					{
						this.m_fadeMultipler += this.m_trailStartFadeSpeed;
					}
					if (this.m_fadeMultipler > num)
					{
						this.m_fadeMultipler -= this.m_trailEndFadeSpeed;
					}
				}
				else
				{
					if (this.m_fadeMultipler > this.m_maximumIdleTrailAlpha)
					{
						this.m_fadeMultipler -= this.m_trailEndFadeSpeed;
					}
					if (this.m_fadeMultipler < this.m_maximumIdleTrailAlpha)
					{
						this.m_fadeMultipler += this.m_trailStartFadeSpeed;
					}
				}
				float num2 = 1f;
				if (this.m_isGhost)
				{
					num2 = this.m_maximumGhostAlpha;
				}
				float num3 = 1f;
				if (!this.m_teleporting)
				{
					if (this.m_points.Count > 0)
					{
						num3 = 0f;
					}
					float num4 = Mathf.Min(1f, Mathf.Max(0f, (this.m_speed - this.m_fadeInStartSpeed) / this.m_fadeInEndSpeed));
					this.SetPoint(new TrailData(this.m_tc.transform.position, num3, num4 * this.m_fadeMultipler * num2, this.m_boostActive));
					if (this.m_teleportEnd)
					{
						this.SetPoint(new TrailData(this.m_tc.transform.position, num3, num4 * this.m_fadeMultipler * num2, this.m_boostActive));
						this.m_teleportEnd = false;
					}
				}
				if (this.m_points.Count > 0)
				{
					int num5 = this.m_points.Count - 1;
					if (this.m_points[num5].m_age > 1f)
					{
						this.m_points.RemoveAt(num5);
					}
				}
				if (this.m_points.Count > 1)
				{
					this.CreateTrailMesh(this.m_pc.p_mesh, -this.m_tc.transform.position, this.m_points, this.m_width, this.m_material);
				}
			}
			if (PsState.m_physicsPaused && !this.m_particlesPaused)
			{
				this.m_particlesPaused = true;
				this.PauseParticles(this.m_particlesPaused);
			}
			else if (!PsState.m_physicsPaused && this.m_particlesPaused)
			{
				this.m_particlesPaused = false;
				this.PauseParticles(this.m_particlesPaused);
			}
		}
		else if (this.m_boostActive)
		{
			if (!this.m_freeze)
			{
				float num6 = Main.m_resettingGameTime * -2f;
				this.m_pc.p_gameObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0f, num6));
			}
			this.m_points.Clear();
			int num7 = 15;
			for (int i = 0; i < num7; i++)
			{
				float num8 = (float)i / (float)(num7 - 1);
				TrailData trailData = new TrailData(Vector3.right * -15f * (float)i + Vector3.up * Mathf.Sin(Main.m_resettingGameTime + (float)i * -0.5f) * 5f * num8, num8, 1f, true);
				this.m_points.Add(trailData);
			}
			this.CreateTrailMesh(this.m_pc.p_mesh, new Vector3(-10f, 10f, 0f), this.m_points, this.m_width, this.m_material);
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00038960 File Offset: 0x00036D60
	private void PauseParticles(bool _pause = true)
	{
		ParticleSystem[] componentsInChildren = base.gameObject.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem in componentsInChildren)
		{
			if (_pause && particleSystem.IsAlive())
			{
				particleSystem.Pause();
			}
			else if (particleSystem.isPaused)
			{
				particleSystem.Play(true);
			}
		}
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x000389C4 File Offset: 0x00036DC4
	public void Destroy()
	{
		EntityManager.RemoveEntity(this.m_entity);
		this.m_entity = null;
		if (this.m_boostParticles != null)
		{
			Object.Destroy(this.m_boostParticles);
			this.m_boostParticles = null;
		}
		if (this.m_idleParticles != null)
		{
			Object.Destroy(this.m_idleParticles);
			this.m_idleParticles = null;
		}
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00038A2C File Offset: 0x00036E2C
	public void CreateTrailMesh(Mesh _mesh, Vector3 _offset, List<TrailData> _points, float _width, Material _material)
	{
		Vector3[] array = new Vector3[_points.Count * 2];
		Vector3[] array2 = new Vector3[_points.Count * 2];
		Vector2[] array3 = new Vector2[_points.Count * 2];
		Color[] array4 = new Color[_points.Count * 2];
		int[] array5 = new int[_points.Count * 6];
		float num = 0f;
		for (int i = 0; i < _points.Count; i++)
		{
			Vector3 point = _points[i].m_point;
			_points[i].m_age += this.m_trailAgingSpeed;
			Vector2 vector = point;
			Vector2 vector2;
			Vector2 vector3;
			if (i == 0)
			{
				vector2 = point - (_points[i + 1].m_point - point);
				vector3 = _points[i + 1].m_point;
			}
			else if (i == _points.Count - 1)
			{
				vector2 = _points[i - 1].m_point;
				vector3 = point + (point - _points[i - 1].m_point);
			}
			else
			{
				vector2 = _points[i - 1].m_point;
				vector3 = _points[i + 1].m_point;
			}
			Vector2 normalized = (vector2 - vector).normalized;
			Vector2 normalized2 = (vector - vector3).normalized;
			float num2 = Mathf.Atan2(-normalized.y, normalized.x);
			float num3 = Mathf.Sin(num2);
			float num4 = Mathf.Cos(num2);
			Vector2 vector4;
			vector4..ctor(num3, num4);
			float num5 = Mathf.Atan2(-normalized2.y, normalized2.x);
			float num6 = Mathf.Sin(num5);
			float num7 = Mathf.Cos(num5);
			Vector2 vector5;
			vector5..ctor(num6, num7);
			Vector2 normalized3 = ((vector4 + vector5) * 0.5f).normalized;
			Vector3 vector6;
			vector6..ctor(normalized3.x, normalized3.y, 0f);
			Vector3 vector7;
			vector7..ctor(vector.x, vector.y, 0f);
			float value = this.m_shapeByAgeTable.GetValue(Mathf.Min(1f, _points[i].m_age));
			Vector3 vector8 = vector7 + vector6 * _width * value * 0.5f;
			Vector3 vector9 = vector7 - vector6 * _width * value * 0.5f;
			array[i * 2] = vector8 + _offset;
			array[i * 2 + 1] = vector9 + _offset;
			num += (vector - vector2).magnitude;
			array3[i * 2] = Vector2.up * (this.m_distance + num);
			array3[i * 2 + 1] = Vector2.up * (this.m_distance + num) + Vector2.right;
			if (this.m_freeze)
			{
				array3[i * 2] = Vector2.up * ((float)i / (float)(_points.Count - 1));
				array3[i * 2 + 1] = Vector2.up * ((float)i / (float)(_points.Count - 1)) + Vector2.right;
			}
			array2[i * 2] = Vector3.forward;
			array2[i * 2 + 1] = Vector3.forward;
			float num8 = Mathf.Min(1f, _points[i].m_age);
			Color color = this.m_colorByAge.Evaluate(num8);
			color.a *= _points[i].m_mul;
			array4[i * 2] = color;
			array4[i * 2 + 1] = color;
			if (i < _points.Count - 1)
			{
				array5[i * 6] = i * 2;
				array5[i * 6 + 1] = i * 2 + 1;
				array5[i * 6 + 2] = i * 2 + 2;
				array5[i * 6 + 3] = i * 2 + 2;
				array5[i * 6 + 4] = i * 2 + 1;
				array5[i * 6 + 5] = i * 2 + 3;
			}
		}
		this.m_distance -= num * 0.02f;
		_mesh.triangles = null;
		_mesh.vertices = null;
		_mesh.vertices = array;
		_mesh.triangles = array5;
		_mesh.uv = array3;
		_mesh.colors = array4;
		_mesh.normals = array2;
		_mesh.RecalculateBounds();
		_mesh.RecalculateNormals();
	}

	// Token: 0x04000514 RID: 1300
	protected Entity m_entity;

	// Token: 0x04000515 RID: 1301
	protected TransformC m_tc;

	// Token: 0x04000516 RID: 1302
	protected PrefabC m_pc;

	// Token: 0x04000517 RID: 1303
	protected List<TrailData> m_points;

	// Token: 0x04000518 RID: 1304
	protected bool m_initialized;

	// Token: 0x04000519 RID: 1305
	protected float m_distance;

	// Token: 0x0400051A RID: 1306
	protected float m_speed;

	// Token: 0x0400051B RID: 1307
	public bool m_trail = true;

	// Token: 0x0400051C RID: 1308
	public float m_width = 10f;

	// Token: 0x0400051D RID: 1309
	protected int m_maxLength = 50;

	// Token: 0x0400051E RID: 1310
	public Material m_material;

	// Token: 0x0400051F RID: 1311
	public AnimationCurve m_shapeByAge;

	// Token: 0x04000520 RID: 1312
	protected AnimationCurveLookupTable m_shapeByAgeTable;

	// Token: 0x04000521 RID: 1313
	public Gradient m_colorByAge;

	// Token: 0x04000522 RID: 1314
	[Range(0f, 1f)]
	public float m_maximumIdleTrailAlpha = 0.5f;

	// Token: 0x04000523 RID: 1315
	public bool m_freeze;

	// Token: 0x04000524 RID: 1316
	protected float m_fadeInStartSpeed = 2.5f;

	// Token: 0x04000525 RID: 1317
	protected float m_fadeInEndSpeed = 5f;

	// Token: 0x04000526 RID: 1318
	protected bool m_teleporting;

	// Token: 0x04000527 RID: 1319
	protected bool m_boostAvailable;

	// Token: 0x04000528 RID: 1320
	protected bool m_boostActive;

	// Token: 0x04000529 RID: 1321
	protected float m_fadeMultipler;

	// Token: 0x0400052A RID: 1322
	[Range(0.02f, 1f)]
	public float m_trailAgingSpeed = 0.02f;

	// Token: 0x0400052B RID: 1323
	protected float m_trailStartFadeSpeed = 0.1f;

	// Token: 0x0400052C RID: 1324
	protected float m_trailEndFadeSpeed = 0.1f;

	// Token: 0x0400052D RID: 1325
	public ParticleSystem m_boostParticles;

	// Token: 0x0400052E RID: 1326
	public ParticleSystem m_idleParticles;

	// Token: 0x0400052F RID: 1327
	protected bool m_isGhost;

	// Token: 0x04000530 RID: 1328
	public float m_maximumGhostAlpha = 0.25f;

	// Token: 0x04000531 RID: 1329
	protected bool m_previewMode;

	// Token: 0x04000532 RID: 1330
	private bool m_teleportEnd;

	// Token: 0x04000533 RID: 1331
	private int skip = 1;

	// Token: 0x04000534 RID: 1332
	private bool m_particlesPaused;
}
