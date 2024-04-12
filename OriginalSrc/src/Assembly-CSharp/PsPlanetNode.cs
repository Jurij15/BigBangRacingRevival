using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class PsPlanetNode
{
	// Token: 0x060008BD RID: 2237 RVA: 0x0005B800 File Offset: 0x00059C00
	public PsPlanetNode(PsGameLoop _loop, bool tutorial = false)
	{
		this.m_loop = _loop;
		this.m_loop.m_node = this;
		if (tutorial)
		{
			this.m_planetNodeSpriteSheet = this.m_loop.m_planet.m_planetSpriteSheetTutorial;
		}
		else
		{
			this.m_planetNodeSpriteSheet = this.m_loop.m_planet.m_planetSpriteSheet;
		}
		this.CreateBase();
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0005B870 File Offset: 0x00059C70
	public static float GetXPosition(PsGameLoop _loop, int _rowOffset = 0)
	{
		int num = _loop.m_nodeId + _loop.m_path.m_startNodeId + _rowOffset;
		float num2 = 0f;
		List<PsGameLoop> row = PsPlanetManager.GetCurrentPlanet().GetRow(num);
		if (row.Count > 1)
		{
			for (int i = 0; i < row.Count; i++)
			{
				num2 += (float)row[i].m_path.m_lane;
			}
			if (row.Count > 0)
			{
				num2 /= (float)row.Count;
			}
		}
		float num3 = 1f;
		if (_loop.m_path.m_startNodeId > 0)
		{
			if (_loop.m_nodeId + _rowOffset < 0)
			{
				num3 *= -3f;
			}
			else if (_loop.m_nodeId + _rowOffset < 1)
			{
				num3 = 0f;
			}
		}
		float num4 = 0f;
		float num5 = (float)num + num4;
		return Mathf.Sin(num5 * 60f * 0.017453292f) * 5f + Mathf.Sin(num5 * 25f * 0.017453292f) * 5f - num2 * 15f + (float)(15 * _loop.m_path.m_lane) * num3 - PsPlanetNode.m_roadXOffset;
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0005B9A0 File Offset: 0x00059DA0
	public virtual void CreateBase()
	{
		int num = this.m_loop.m_nodeId + this.m_loop.m_path.m_startNodeId;
		float xposition = PsPlanetNode.GetXPosition(this.m_loop, 0);
		float num2 = 0f;
		float num3 = (float)num + num2;
		float num4 = Mathf.Cos((this.m_loop.m_planet.m_pathStartOffset + num3 * this.m_loop.m_planet.m_nodeRowAngleInterval) * 0.017453292f) * this.m_loop.m_planet.m_planetRadius;
		float num5 = Mathf.Sin((this.m_loop.m_planet.m_pathStartOffset + num3 * this.m_loop.m_planet.m_nodeRowAngleInterval) * 0.017453292f) * this.m_loop.m_planet.m_planetRadius;
		Vector3 vector;
		vector..ctor(xposition, num4, num5);
		Vector3 vector2 = vector.normalized * this.m_loop.m_planet.m_planetRadius;
		float num6 = Mathf.Atan2(-num4, num5) * 57.29578f + 180f;
		float num7 = Mathf.Atan2(this.m_loop.m_planet.m_planetRadius, xposition) * 57.29578f + 90f;
		Entity entity = EntityManager.AddEntity(new string[] { "PlanetUI", "Tutorial" });
		this.m_tc = TransformS.AddComponent(entity, this.GetTransformName(), vector2);
		TransformS.ParentComponent(this.m_tc, this.m_loop.m_planet.m_alienPlanetTC);
		TransformS.SetRotation(this.m_tc, new Vector3(num6, 0f, 0f));
		this.m_tc.transform.Rotate(new Vector3(90f, num7, 0f), 1);
		this.Initialize();
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0005BB5B File Offset: 0x00059F5B
	public virtual string GetTransformName()
	{
		return "PlanetNode" + this.m_loop.m_nodeId;
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0005BB78 File Offset: 0x00059F78
	public virtual void Initialize()
	{
		this.m_uiTC = TransformS.AddComponent(this.m_tc.p_entity);
		this.m_numberTC = TransformS.AddComponent(this.m_uiTC.p_entity);
		this.m_keyTC = TransformS.AddComponent(this.m_uiTC.p_entity, "keyTC");
		this.m_starsTC = TransformS.AddComponent(this.m_uiTC.p_entity);
		this.m_leftStarTC = TransformS.AddComponent(this.m_tc.p_entity);
		this.m_centerStarTC = TransformS.AddComponent(this.m_tc.p_entity);
		this.m_rightStarTC = TransformS.AddComponent(this.m_tc.p_entity);
		this.m_trophyTC = TransformS.AddComponent(this.m_tc.p_entity);
		TransformS.ParentComponent(this.m_uiTC, this.m_tc, Vector3.zero);
		TransformS.ParentComponent(this.m_numberTC, this.m_uiTC, new Vector3(0f, 1.5f, -6f));
		TransformS.ParentComponent(this.m_starsTC, this.m_uiTC, new Vector3(0f, -1.5f, -10f));
		TransformS.ParentComponent(this.m_keyTC, this.m_starsTC, Vector3.up * -1.5f + Vector3.forward * -0.1f);
		TransformS.ParentComponent(this.m_leftStarTC, this.m_starsTC, Vector3.right * -2.8f);
		TransformS.ParentComponent(this.m_centerStarTC, this.m_starsTC, Vector3.up * -0.75f + Vector3.forward * -0.1f);
		TransformS.ParentComponent(this.m_rightStarTC, this.m_starsTC, Vector3.right * 2.8f);
		TransformS.ParentComponent(this.m_trophyTC, this.m_starsTC, Vector3.right * 6.3f);
		this.m_unlockTC = TransformS.AddComponent(this.m_tc.p_entity);
		TransformS.ParentComponent(this.m_unlockTC, this.m_uiTC, new Vector3(0f, 14f, 0f));
		this.m_unlockShiftTC = TransformS.AddComponent(this.m_tc.p_entity);
		TransformS.ParentComponent(this.m_unlockShiftTC, this.m_unlockTC, Vector3.zero);
		this.m_uiTC.transform.LookAt(this.m_loop.m_planet.m_planetCamera.gameObject.transform, this.m_loop.m_planet.m_planetCamera.gameObject.transform.up);
		this.m_uiTC.transform.localRotation *= Quaternion.Euler(Vector3.up * 180f);
		SpriteS.ConvertSpritesToPrefabComponent(this.m_uiTC, true);
		this.m_fontColor = DebugDraw.GetColor(48f, 30f, 26f, 255f);
		this.Create();
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0005BE74 File Offset: 0x0005A274
	public virtual void Create()
	{
		this.m_starCreationTime = 0f;
		this.CreatePrefab();
		if (this.m_loop.m_path != this.m_loop.m_planet.GetMainPath())
		{
			TransformS.SetScale(this.m_tc, this.m_sidePathDomeScale);
		}
		this.SetState();
		this.CreateRoad();
		if (this.m_loop.m_nodeId <= this.m_loop.m_path.m_currentNodeId)
		{
			this.CreateTouchArea();
		}
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0005BEF8 File Offset: 0x0005A2F8
	public virtual void CreateTouchArea()
	{
		if (this.m_tac == null)
		{
			this.m_tac = TouchAreaS.AddCircleArea(this.m_uiTC, "PlanetNode" + this.m_loop.m_nodeId, 5f, this.m_loop.m_planet.m_planetCamera, null);
			TouchAreaS.AddTouchEventListener(this.m_tac, new TouchEventDelegate(this.TouchHandler));
		}
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0005BF6C File Offset: 0x0005A36C
	public virtual void CreateRoad()
	{
		this.DestroyRoad();
		if (this.m_loop.m_nodeId > 1 || this.m_loop.m_path.m_startNodeId > 0)
		{
			int num = 10;
			float num2 = 1f / (float)num;
			float num3 = this.m_loop.m_planet.m_nodeRowAngleInterval / (float)num;
			List<Vector3> list = new List<Vector3>();
			List<Vector3> list2 = new List<Vector3>();
			Vector2 zero = Vector2.zero;
			Vector2 vector = Vector2.up * 10f;
			Vector2 vector2 = Vector2.up * 20f;
			Vector2 vector3 = Vector2.up * 30f;
			zero.x = PsPlanetNode.GetXPosition(this.m_loop, -2);
			vector.x = PsPlanetNode.GetXPosition(this.m_loop, -1);
			vector2.x = PsPlanetNode.GetXPosition(this.m_loop, 0);
			vector3.x = PsPlanetNode.GetXPosition(this.m_loop, 1);
			float num4 = 0f;
			for (int i = 0; i < num; i++)
			{
				int num5 = this.m_loop.m_nodeId + this.m_loop.m_path.m_startNodeId;
				float num6 = 0f;
				if (this.m_loop.m_nodeId == 1)
				{
					num6 *= (float)i / (float)num;
				}
				float num7 = (float)num5 + num6;
				float num8 = ToolBox.PointOnBezierSpline(zero, vector, vector2, vector3, num4).x;
				float num9 = Mathf.Cos((this.m_loop.m_planet.m_pathStartOffset + (num7 - 1f) * this.m_loop.m_planet.m_nodeRowAngleInterval + (float)i * num3) * 0.017453292f) * this.m_loop.m_planet.m_planetRadius;
				float num10 = Mathf.Sin((this.m_loop.m_planet.m_pathStartOffset + (num7 - 1f) * this.m_loop.m_planet.m_nodeRowAngleInterval + (float)i * num3) * 0.017453292f) * this.m_loop.m_planet.m_planetRadius;
				Vector3 vector4;
				vector4..ctor(num8, num9, num10);
				Vector3 vector5 = vector4.normalized * (this.m_loop.m_planet.m_planetRadius - 0.1f);
				if ((float)i < this.m_roadPhase * (float)num)
				{
					list.Add(vector5);
				}
				else if (list2.Count == 0)
				{
					float num11 = this.m_roadPhase * (float)num;
					num8 = ToolBox.PointOnBezierSpline(zero, vector, vector2, vector3, this.m_roadPhase).x;
					num9 = Mathf.Cos((this.m_loop.m_planet.m_pathStartOffset + (num7 - 1f) * this.m_loop.m_planet.m_nodeRowAngleInterval + num11 * num3) * 0.017453292f) * this.m_loop.m_planet.m_planetRadius;
					num10 = Mathf.Sin((this.m_loop.m_planet.m_pathStartOffset + (num7 - 1f) * this.m_loop.m_planet.m_nodeRowAngleInterval + num11 * num3) * 0.017453292f) * this.m_loop.m_planet.m_planetRadius;
					Vector3 vector6;
					vector6..ctor(num8, num9, num10);
					Vector3 vector7 = vector6.normalized * (this.m_loop.m_planet.m_planetRadius - 0.1f);
					list.Add(vector7);
					list2.Add(vector7);
				}
				else
				{
					list2.Add(vector5);
				}
				num4 += num2;
			}
			Color color = DebugDraw.HexToColor("FF8116");
			Color color2 = DebugDraw.HexToColor("b3a8ae");
			float num12 = 3f;
			if (this.m_loop.m_path.m_startNodeId > 0)
			{
				num12 = 1.8539999f;
			}
			if (list.Count > 1)
			{
				this.m_roadPrefab = PrefabS.CreateSphericalPathFromVectorArray(this.m_tc.parent, Vector3.zero, list.ToArray(), this.m_roadWidth * num12, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line32Mat_Material), PsPlanetManager.GetCurrentPlanet().m_planetCamera, Position.Center, false);
			}
			if (list2.Count > 1)
			{
				this.m_roadPrefab2 = PrefabS.CreateSphericalPathFromVectorArray(this.m_tc.parent, Vector3.zero, list2.ToArray(), this.m_roadWidth * num12, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line32Mat_Material), PsPlanetManager.GetCurrentPlanet().m_planetCamera, Position.Center, false);
			}
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0005C3C4 File Offset: 0x0005A7C4
	public virtual void DestroyRoad()
	{
		if (this.m_roadPrefab != null)
		{
			PrefabS.RemoveComponent(this.m_roadPrefab, true);
			this.m_roadPrefab = null;
		}
		if (this.m_roadPrefab2 != null)
		{
			PrefabS.RemoveComponent(this.m_roadPrefab2, true);
			this.m_roadPrefab2 = null;
		}
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0005C402 File Offset: 0x0005A802
	public virtual void CreatePrefab()
	{
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0005C404 File Offset: 0x0005A804
	public virtual void CreateUI()
	{
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0005C406 File Offset: 0x0005A806
	public virtual void RemoveUI()
	{
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0005C408 File Offset: 0x0005A808
	public virtual void Update()
	{
		if (this.m_loop.m_drawRoad)
		{
			float num = Mathf.Min(0.5f, Main.m_resettingGameTime - this.m_loop.m_roadDrawStart);
			this.m_roadPhase = TweenS.tween(TweenStyle.Linear, num, 0.5f, 0f, 1f);
			this.CreateRoad();
			if (num == 0.5f)
			{
				this.m_loop.m_drawRoad = false;
			}
		}
		if (this.m_loop.m_scaleRoad)
		{
			float num2 = Mathf.Min(1f, Main.m_resettingGameTime - this.m_loop.m_roadScaleStart);
			this.m_roadWidth = TweenS.tween(TweenStyle.ElasticOut, num2, 1f, 0f, 1f);
			this.CreateRoad();
			if (num2 == 1f)
			{
				this.m_loop.m_scaleRoad = false;
			}
		}
		float num3 = TweenS.tween(TweenStyle.CubicOut, Mathf.Min(0.5f, Main.m_resettingGameTime - this.m_starCreationTime), 0.5f, 0f, 1f);
		float num4 = Vector3.Dot(this.m_tc.transform.position.normalized, Vector3.up);
		this.m_uiTC.transform.LookAt(this.m_loop.m_planet.m_planetCamera.gameObject.transform, this.m_loop.m_planet.m_planetCamera.gameObject.transform.up);
		this.m_uiTC.transform.localRotation *= Quaternion.Euler(Vector3.up * 180f);
		float num5 = 1f;
		if (num4 > 0.9f)
		{
			num5 = 0f;
		}
		else if (num4 > 0.8f && num4 < 0.9f)
		{
			num5 = TweenS.tween(TweenStyle.CubicIn, num4 - 0.8f, 0.1f, 1f, -1f);
		}
		TransformS.SetPosition(this.m_numberTC, new Vector3(0f, 1.5f * num4, -6f) + Vector3.up * -2.25f * (1f - num5));
		TransformS.SetPosition(this.m_starsTC, new Vector3(0f, 0.5f + -6f * (1f - num4), -10f) + Vector3.up * -1f * (1f - num5));
		if (num3 < 1f && this.m_loop.m_path == this.m_loop.m_planet.GetMainPath())
		{
			Quaternion quaternion = Quaternion.Euler(Vector3.right * 110f * (1f - num3));
			this.m_starsTC.transform.localRotation = quaternion;
			this.m_keyTC.transform.localRotation = quaternion;
		}
		else if (this.m_hideUI)
		{
			this.m_keyTC.transform.localScale *= Mathf.Lerp(this.m_keyTC.transform.localScale.x, 0f, 0.1f);
			this.m_keyTC.updateScale = true;
		}
		else
		{
			Quaternion quaternion2 = Quaternion.Euler(Vector3.right * 110f * (1f - num5));
			this.m_numberTC.transform.localRotation = quaternion2;
			this.m_starsTC.transform.localRotation = quaternion2;
			this.m_keyTC.transform.localRotation = quaternion2;
		}
		SpriteS.SetColorByTransformComponent(this.m_starsTC, new Color(1f, 1f, 1f, num5), true, false);
		this.m_fontColor.a = num5;
		if (this.m_numberText != null)
		{
			this.m_numberText.m_textMesh.GetComponent<Renderer>().material.SetColor("_Color", this.m_fontColor);
		}
		if (this.m_highlightTween != null)
		{
			Color color = this.m_highlightPrefab.p_gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>()
				.material.GetColor("_TintColor");
			color.a = this.m_highlightTC.transform.localScale.y * 0.35f;
			this.m_highlightPrefab.p_gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>()
				.material.SetColor("_TintColor", color);
			this.m_highlightPrefab.p_gameObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>()
				.material.SetColor("_TintColor", color);
		}
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0005C8E0 File Offset: 0x0005ACE0
	public virtual void Destroy()
	{
		if (this.m_popup != null)
		{
			this.m_popup.CallAction("Exit");
		}
		this.DestroyRoad();
		EntityManager.RemoveEntity(this.m_tc.p_entity);
		if (this.m_highlightTC != null)
		{
			Object.Destroy(this.m_highlightPrefab.p_gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>()
				.material);
			Object.Destroy(this.m_highlightPrefab.p_gameObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>()
				.material);
			EntityManager.RemoveEntity(this.m_highlightTC.p_entity);
		}
		this.m_loop.m_scaleRoad = false;
		this.m_loop.m_drawRoad = false;
		this.m_loop.m_node = null;
		this.m_loop = null;
		this.m_created = false;
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0005C9C2 File Offset: 0x0005ADC2
	public virtual void Deactivate()
	{
		if (this.m_checkpointEffect != null)
		{
			this.m_checkpointEffect.Claim();
		}
		this.RemoveHighlight();
		this.PlayDeactivateSound();
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0005C9EC File Offset: 0x0005ADEC
	public virtual void PlayDeactivateSound()
	{
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0005C9EE File Offset: 0x0005ADEE
	public virtual void SetInactive()
	{
		if (this.m_checkpointEffect != null)
		{
			this.m_checkpointEffect.SetLocked();
		}
		this.RemoveHighlight();
		this.m_roadWidth = 1f;
		this.m_roadPhase = 0f;
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0005CA28 File Offset: 0x0005AE28
	public virtual void Activate()
	{
		if (this.m_unlocked)
		{
			this.SetActive();
			return;
		}
		this.m_roadWidth = 1f;
		this.m_roadPhase = 0f;
		this.m_loop.m_drawRoad = true;
		this.m_loop.m_roadDrawStart = Main.m_resettingGameTime;
		TimerS.AddComponent(this.m_tc.p_entity, string.Empty, 0.4f, 0f, false, new TimerComponentDelegate(this.Activate2));
		this.m_unlocked = false;
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0005CAB0 File Offset: 0x0005AEB0
	public virtual void Activate2(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		if (this.m_checkpointEffect != null)
		{
			this.m_checkpointEffect.Activate();
		}
		this.m_starCreationTime = Main.m_resettingGameTime;
		this.CreateUI();
		this.Highlight();
		this.m_unlocked = true;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0005CB00 File Offset: 0x0005AF00
	public virtual void SetActive()
	{
		if (this.m_checkpointEffect != null)
		{
			this.m_checkpointEffect.SetIdleActive();
		}
		this.CreateUI();
		this.Highlight();
		this.m_roadWidth = 1f;
		this.m_roadPhase = 1f;
		this.m_loop.m_drawRoad = true;
		this.m_unlocked = true;
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0005CB5E File Offset: 0x0005AF5E
	public virtual void SetClaimed()
	{
		if (this.m_checkpointEffect != null)
		{
			this.m_checkpointEffect.SetClaimed();
		}
		this.m_roadWidth = 1f;
		this.m_roadPhase = 1f;
		this.m_unlocked = true;
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0005CB99 File Offset: 0x0005AF99
	public virtual void Claim()
	{
		if (this.m_checkpointEffect != null)
		{
			this.m_checkpointEffect.Claim();
		}
		this.RemoveHighlight();
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x0005CBBD File Offset: 0x0005AFBD
	public virtual void SetHidden()
	{
		this.SetInactive();
		this.m_tc.transform.localScale = Vector3.zero;
		this.m_roadWidth = 0f;
		this.m_roadPhase = 0f;
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x0005CBF0 File Offset: 0x0005AFF0
	public virtual void Reveal()
	{
		if (this.m_tc != null && this.m_tc.p_entity != null && this.m_loop != null)
		{
			Vector3 vector = Vector3.one;
			if (this.m_loop.m_path != this.m_loop.m_planet.GetMainPath())
			{
				vector *= this.m_sidePathDomeScale;
			}
			TweenS.AddTransformTween(this.m_tc, TweenedProperty.Scale, TweenStyle.ElasticOut, vector, 0.5f, 0f, true);
			this.m_loop.m_keepNodeHidden = false;
			this.SetState();
			this.m_roadWidth = 0f;
			this.m_roadPhase = 0f;
			this.m_loop.m_scaleRoad = true;
			this.m_loop.m_roadScaleStart = Main.m_resettingGameTime;
		}
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0005CCB8 File Offset: 0x0005B0B8
	public virtual void SetState()
	{
		bool keepNodeHidden = this.m_loop.m_keepNodeHidden;
		bool flag = this.m_loop.m_nodeId > this.m_loop.m_path.m_currentNodeId;
		bool flag2 = this.m_loop.m_nodeId < this.m_loop.m_path.m_currentNodeId;
		bool flag3 = this.m_loop.m_nodeId == this.m_loop.m_path.m_currentNodeId;
		bool flag4 = this.m_loop == PsState.m_activeGameLoop;
		bool flag5 = PsState.m_activeGameLoop != null;
		bool flag6 = flag5 && ((PsState.m_activeGameLoop.m_path == this.m_loop.m_path && PsState.m_activeGameLoop.m_nodeId + 1 == this.m_loop.m_nodeId) || (PsState.m_activeGameLoop.m_sidePath == this.m_loop.m_path && this.m_loop.m_nodeId == 1));
		if (keepNodeHidden)
		{
			this.SetHidden();
		}
		else if (flag || (flag3 && flag6))
		{
			this.SetInactive();
		}
		else if (flag4 || flag3)
		{
			this.SetActive();
		}
		else if (flag2)
		{
			this.SetClaimed();
		}
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0005CE0C File Offset: 0x0005B20C
	public virtual void GiveTrophies(TimerComponentDelegate _trophiesGivenDelegate)
	{
		PsGameLoopRacing psGameLoopRacing = this.m_loop as PsGameLoopRacing;
		PsGameModeRacing psGameModeRacing = psGameLoopRacing.m_gameMode as PsGameModeRacing;
		if ((psGameLoopRacing.m_ghostWon && psGameModeRacing.m_trophyGhost.trophyWin != 0) || (!psGameLoopRacing.m_ghostWon && psGameModeRacing.m_trophyGhost.trophyLoss != 0))
		{
			if (psGameLoopRacing.m_ghostWon)
			{
				PsMainMenuState.m_trophyCount.SetText((Convert.ToInt32(PsMainMenuState.m_trophyCount.m_text) - psGameModeRacing.m_trophyGhost.trophyWin).ToString());
				PsMainMenuState.m_currentTrophyAmount -= psGameModeRacing.m_trophyGhost.trophyWin;
			}
			else
			{
				PsMainMenuState.m_trophyCount.SetText((Convert.ToInt32(PsMainMenuState.m_trophyCount.m_text) + Mathf.Abs(psGameModeRacing.m_trophyGhost.trophyLoss)).ToString());
				PsMainMenuState.m_currentTrophyAmount += Mathf.Abs(psGameModeRacing.m_trophyGhost.trophyLoss);
			}
			Vector2 vector = PsMainMenuState.m_raceButton.m_camera.WorldToScreenPoint(PsMainMenuState.m_raceButton.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
			UIComponent raceGachaArea = PsMainMenuState.m_raceGachaArea;
			Vector2 vector2 = raceGachaArea.m_camera.WorldToScreenPoint(raceGachaArea.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
			PsMetagameManager.m_playerStats.CreateFlyingTrophies((!psGameLoopRacing.m_ghostWon) ? psGameModeRacing.m_trophyGhost.trophyLoss : psGameModeRacing.m_trophyGhost.trophyWin, vector, vector2, 0f, null, null, null);
		}
		TimerC timerC = TimerS.AddComponent(this.m_tc.p_entity, string.Empty, 0.5f, 0f, false, _trophiesGivenDelegate);
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x0005D014 File Offset: 0x0005B414
	public virtual void GiveFlyingMapPieces()
	{
		PsGameLoopAdventure psGameLoopAdventure = this.m_loop as PsGameLoopAdventure;
		Vector2 vector = PsPlanetManager.GetCurrentPlanet().m_planetCamera.WorldToScreenPoint(this.m_prefab.p_parentTC.transform.position + Vector3.up * 0.005f * (float)Screen.height) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
		UIComponent adventureGachaArea = PsMainMenuState.m_adventureGachaArea;
		Vector2 vector2 = adventureGachaArea.m_camera.WorldToScreenPoint(adventureGachaArea.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
		int num = Mathf.Max(0, this.m_loop.m_scoreCurrent - this.m_loop.m_scoreOld);
		PsMetagameManager.m_playerStats.CreateFlyingMapPieces(num, vector, vector2, 0f, null, null, null);
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x0005D11C File Offset: 0x0005B51C
	public virtual void GivePositionNumber()
	{
		int position = (this.m_loop as PsGameLoopRacing).GetPosition();
		string text = "menu_position_";
		switch (position)
		{
		case 1:
			text += "1st";
			break;
		case 2:
			text += "2nd";
			break;
		case 3:
			text += "3rd";
			break;
		case 4:
			text += "4th";
			break;
		default:
			return;
		}
		Frame frame = this.m_planetNodeSpriteSheet.m_atlas.GetFrame(text, null);
		SpriteS.RemoveComponentsByTransformComponent(this.m_centerStarTC);
		SpriteC spriteC = SpriteS.AddComponent(this.m_centerStarTC, frame, this.m_planetNodeSpriteSheet);
		SpriteS.SetSortValue(spriteC, -1f);
		SpriteS.SetDimensions(spriteC, 4f * (frame.width / frame.height), 4f);
		SpriteS.SetColor(spriteC, Color.white);
		TweenS.AddTransformTween(this.m_centerStarTC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one * 1.25f, Vector3.one, 0.5f, 0f, true);
		SoundS.PlaySingleShot("/Metagame/StarAppear", Vector3.zero, 1f);
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x0005D248 File Offset: 0x0005B648
	public virtual void GiveMapPieces(TimerComponentDelegate _callback)
	{
		int num = Mathf.Max(0, this.m_loop.m_scoreCurrent - this.m_loop.m_scoreOld);
		if (PsMainMenuState.m_adventureGacha.m_state == PsUIGachaState.Collecting)
		{
			this.GiveFlyingMapPieces();
		}
		TimerC timerC = TimerS.AddComponent(this.m_tc.p_entity, string.Empty, (float)num * 0.25f + 0.5f, 0f, false, _callback);
		for (int i = this.m_loop.m_scoreOld + 1; i <= this.m_loop.m_scoreCurrent; i++)
		{
			TimerComponentDelegate timerComponentDelegate = new TimerComponentDelegate(this.GiveMapPiece);
			timerC = TimerS.AddComponent(this.m_tc.p_entity, string.Empty, (float)i * 0.25f + 0.4f, 0f, false, timerComponentDelegate);
			timerC.customObject = i;
		}
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x0005D324 File Offset: 0x0005B724
	protected virtual void GiveMapPiece(TimerC _c)
	{
		int num = Convert.ToInt32(_c.customObject);
		SpriteS.RemoveComponentsByTransformComponent(this.m_centerStarTC);
		Frame frame = this.m_planetNodeSpriteSheet.m_atlas.GetFrame("menu_map_pieces_small_" + num, null);
		SpriteC spriteC = SpriteS.AddComponent(this.m_centerStarTC, frame, this.m_planetNodeSpriteSheet);
		SpriteS.SetDimensions(spriteC, 4f * (frame.width / frame.height), 4f);
		SpriteS.SetColor(spriteC, Color.white);
		TweenS.AddTransformTween(this.m_centerStarTC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one * 1.25f, Vector3.one, 0.5f, 0f, true);
		TimerS.RemoveComponent(_c);
		SoundS.PlaySingleShot("/Metagame/StarAppear", Vector3.zero, 1f);
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x0005D3F0 File Offset: 0x0005B7F0
	public virtual void GiveStars(TimerComponentDelegate _starsGivenDelegate)
	{
		TimerC timerC = TimerS.AddComponent(this.m_tc.p_entity, string.Empty, (float)this.m_loop.m_scoreBest * 0.25f + 0.5f, 0f, false, _starsGivenDelegate);
		int num = Mathf.Max(0, this.m_loop.m_scoreCurrent - this.m_loop.m_scoreOld);
		PsPlanetPath mainPath = PsPlanetManager.GetCurrentPlanet().GetMainPath();
		PsGameLoop nodeInfo = mainPath.GetNodeInfo(mainPath.GetLastBlockId());
		bool flag = nodeInfo.m_planet.m_banners != null && nodeInfo.m_planet.m_banners.Count > 0;
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x0005D490 File Offset: 0x0005B890
	protected virtual void GiveStar(TimerC _c)
	{
		Frame frame = this.m_planetNodeSpriteSheet.m_atlas.GetFrame("menu_star_small_full", null);
		TransformC transformC = _c.customComponent as TransformC;
		SpriteS.RemoveComponentsByTransformComponent(transformC);
		SpriteC spriteC = SpriteS.AddComponent(transformC, frame, this.m_planetNodeSpriteSheet);
		if (transformC == this.m_centerStarTC)
		{
			SpriteS.SetSortValue(spriteC, -1f);
		}
		SpriteS.SetDimensions(spriteC, 4f * (frame.width / frame.height), 4f);
		SpriteS.SetColor(spriteC, Color.white);
		TweenS.AddTransformTween(transformC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one * 1.25f, Vector3.one, 0.5f, 0f, true);
		TimerS.RemoveComponent(_c);
		SoundS.PlaySingleShot("/Metagame/StarAppear", Vector3.zero, 1f);
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x0005D558 File Offset: 0x0005B958
	public virtual void Highlight()
	{
		this.CreateTouchArea();
		if (this.m_highlightTC == null)
		{
			Entity entity = EntityManager.AddEntity();
			this.m_highlightTC = TransformS.AddComponent(entity);
			this.m_highlightTC.transform.localScale = new Vector3(1f, 1f, 1f);
			TransformS.ParentComponent(this.m_highlightTC, this.m_tc, Vector3.zero + new Vector3(0f, 0.25f, 0f));
			this.m_highlightTC.transform.localRotation = Quaternion.identity;
			GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.PathButtonActiveEffectPrefab_GameObject);
			this.m_highlightPrefab = PrefabS.AddComponent(this.m_highlightTC, Vector3.zero, gameObject);
			this.m_highlightPrefab.p_gameObject.transform.localScale = Vector3.one * 1.6f;
			PrefabS.SetCamera(this.m_highlightPrefab.p_gameObject, this.m_loop.m_planet.m_planetCamera);
			if (!PsGachaManager.IsSlotEmpty(PsGachaManager.SlotType.ADVENTURE) || PsMetagameManager.m_vehicleGachaData.m_mapPieceCount >= PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax)
			{
				Animator componentInChildren = this.m_highlightPrefab.p_gameObject.GetComponentInChildren<Animator>();
				componentInChildren.SetBool("inActive", true);
			}
		}
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x0005D698 File Offset: 0x0005BA98
	public virtual void RemoveHighlight()
	{
		if (this.m_highlightTC != null)
		{
			this.m_highlightTween = TweenS.AddTransformTween(this.m_highlightTC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one, Vector3.zero, 0.5f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_highlightTween, new TweenEventDelegate(this.RemoveHighlightDelegate));
		}
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x0005D6F0 File Offset: 0x0005BAF0
	public void ActivateHighlight()
	{
		if (this.m_highlightPrefab != null)
		{
			Animator componentInChildren = this.m_highlightPrefab.p_gameObject.GetComponentInChildren<Animator>();
			componentInChildren.SetBool("inActive", false);
			this.m_highlightTween = TweenS.AddTransformTween(this.m_highlightTC, TweenedProperty.Scale, TweenStyle.CubicOut, new Vector3(1f, 0f, 1f), Vector3.one, 1f, 0f, false);
		}
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x0005D75C File Offset: 0x0005BB5C
	public void DeActivateHighlight()
	{
		if (this.m_highlightPrefab != null)
		{
			Animator componentInChildren = this.m_highlightPrefab.p_gameObject.GetComponentInChildren<Animator>();
			componentInChildren.SetBool("inActive", true);
			if (this.m_highlightTween != null)
			{
				TweenS.RemoveComponent(this.m_highlightTween);
			}
			this.m_highlightTween = null;
		}
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x0005D7B0 File Offset: 0x0005BBB0
	protected void RemoveHighlightDelegate(TweenC _c)
	{
		if (this.m_highlightTC != null)
		{
			Object.Destroy(this.m_highlightPrefab.p_gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>()
				.material);
			Object.Destroy(this.m_highlightPrefab.p_gameObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>()
				.material);
			EntityManager.RemoveEntity(this.m_highlightTC.p_entity);
			this.m_highlightTC = null;
			this.m_highlightTween = null;
		}
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x0005D83C File Offset: 0x0005BC3C
	public virtual void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchCount == 1 && _touchPhase == TouchAreaPhase.ReleaseIn && !_touches[0].m_dragged)
		{
			PsMetrics.ButtonPressed("PlanetNode", "mainmenu", PsState.GetCurrentVehicleType(false).ToString());
			TouchAreaS.CancelAllTouches(null);
			if (PsState.m_activeGameLoop == null && this.m_loop.GetType() == typeof(PsGameLoopAdventure) && this.m_loop == this.m_loop.m_path.GetCurrentNodeInfo() && this.m_loop.m_scoreBest < 3 && (!PsGachaManager.IsSlotEmpty(PsGachaManager.SlotType.ADVENTURE) || PsMetagameManager.m_vehicleGachaData.m_mapPieceCount >= PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax))
			{
				if (this.m_loop.m_nodeId <= this.m_loop.m_path.m_currentNodeId)
				{
					SoundS.PlaySingleShot("/UI/DomeSelect", Vector3.zero, 1f);
				}
				if (PsMetagameManager.GetOpenedChestCount() == 0 || (PsMetagameManager.GetOpenedChestCount() == 1 && !PsGachaManager.IsSlotEmpty(PsGachaManager.SlotType.ADVENTURE)))
				{
					PsDialogue dialogueByIdentifier = PsMetagameData.GetDialogueByIdentifier("first_chest_gotten");
					PsMainMenuState.HideUI(true, false, null, true, null);
					new PsMenuDialogueFlow(dialogueByIdentifier, 0f, delegate
					{
						PsMainMenuState.ShowUI(true, null);
						PsUIAdventureGacha adventureGacha = PsMainMenuState.m_adventureGacha;
						bool flag2 = true;
						Vector3 vector = new Vector3(0f, 25f, 0f);
						new PsUITutorialArrowUI(adventureGacha, flag2, null, 2f, vector, false);
					}, false, false);
				}
				else
				{
					CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
					this.m_popup = new PsUIBasePopup(typeof(PsUIAdventureChestSlotPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
					this.m_popup.SetAction("Play", delegate
					{
						if (this.m_loop != null)
						{
							this.m_loop.StartLoop();
							TouchAreaS.CancelAllTouches(null);
						}
						if (this.m_popup != null)
						{
							this.m_popup.UnhideUIOnDestroy(false);
							this.m_popup.Destroy();
							this.m_popup = null;
						}
					});
					this.m_popup.SetAction("Exit", delegate
					{
						if (this.m_popup != null)
						{
							this.m_popup.Destroy();
							this.m_popup = null;
						}
						CameraS.RemoveBlur();
					});
					EntityManager.SetActivityOfEntitiesWithTag("AdventureGacha", true, true, true, true, false, false);
					TouchAreaS.SetCamera(PsMainMenuState.m_adventureGacha.m_TAC, this.m_popup.m_scrollableCanvas.m_camera);
					TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				}
			}
			else if (this is PsPlanetBlockNode && this.m_loop.m_nodeId == this.m_loop.m_path.m_currentNodeId)
			{
				bool flag = true;
				if (this.m_loop.m_planet.m_banners != null)
				{
					for (int i = 0; i < this.m_loop.m_planet.m_banners.Count; i++)
					{
						if (this.m_loop.m_planet.m_banners[i].m_gameLoop == this.m_loop)
						{
							this.m_loop.m_planet.m_banners[i].TouchEvent();
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					this.m_loop.StartLoop();
					TouchAreaS.CancelAllTouches(null);
				}
			}
			else
			{
				if (this.m_loop.m_nodeId <= this.m_loop.m_path.m_currentNodeId && !(this is PsPlanetBlockNode))
				{
					SoundS.PlaySingleShot("/UI/DomeSelect", Vector3.zero, 1f);
				}
				this.m_loop.StartLoop();
				TouchAreaS.CancelAllTouches(null);
			}
		}
	}

	// Token: 0x04000837 RID: 2103
	public SpriteSheet m_planetNodeSpriteSheet;

	// Token: 0x04000838 RID: 2104
	public TransformC m_tc;

	// Token: 0x04000839 RID: 2105
	public TransformC m_uiTC;

	// Token: 0x0400083A RID: 2106
	public TransformC m_numberTC;

	// Token: 0x0400083B RID: 2107
	public TransformC m_keyTC;

	// Token: 0x0400083C RID: 2108
	public TransformC m_starsTC;

	// Token: 0x0400083D RID: 2109
	protected TransformC m_leftStarTC;

	// Token: 0x0400083E RID: 2110
	protected TransformC m_centerStarTC;

	// Token: 0x0400083F RID: 2111
	protected TransformC m_rightStarTC;

	// Token: 0x04000840 RID: 2112
	protected TransformC m_trophyTC;

	// Token: 0x04000841 RID: 2113
	public TransformC m_highlightTC;

	// Token: 0x04000842 RID: 2114
	protected TweenC m_highlightTween;

	// Token: 0x04000843 RID: 2115
	protected PrefabC m_highlightPrefab;

	// Token: 0x04000844 RID: 2116
	protected PrefabC m_roadPrefab;

	// Token: 0x04000845 RID: 2117
	protected PrefabC m_roadPrefab2;

	// Token: 0x04000846 RID: 2118
	public float m_roadPhase;

	// Token: 0x04000847 RID: 2119
	public float m_roadWidth;

	// Token: 0x04000848 RID: 2120
	public bool m_unlocked;

	// Token: 0x04000849 RID: 2121
	protected TransformC m_unlockTC;

	// Token: 0x0400084A RID: 2122
	protected TransformC m_unlockShiftTC;

	// Token: 0x0400084B RID: 2123
	public TextMeshC m_starCounterText;

	// Token: 0x0400084C RID: 2124
	public PsGameLoop m_loop;

	// Token: 0x0400084D RID: 2125
	public bool m_created;

	// Token: 0x0400084E RID: 2126
	public PrefabC m_prefab;

	// Token: 0x0400084F RID: 2127
	public TextMeshC m_numberText;

	// Token: 0x04000850 RID: 2128
	public TouchAreaC m_tac;

	// Token: 0x04000851 RID: 2129
	public bool m_hideUI;

	// Token: 0x04000852 RID: 2130
	protected Color m_fontColor;

	// Token: 0x04000853 RID: 2131
	public EffectCheckpoint m_checkpointEffect;

	// Token: 0x04000854 RID: 2132
	protected float m_starCreationTime;

	// Token: 0x04000855 RID: 2133
	public static float m_roadXOffset = 17.5f;

	// Token: 0x04000856 RID: 2134
	private float m_sidePathDomeScale = 0.87f;

	// Token: 0x04000857 RID: 2135
	private PsUIBasePopup m_popup;
}
