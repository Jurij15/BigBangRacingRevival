using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public abstract class Vehicle : Unit
{
	// Token: 0x06000463 RID: 1123 RVA: 0x00022888 File Offset: 0x00020C88
	public Vehicle(GraphElement _graphElement)
		: base(_graphElement, UnitType.Vehicle)
	{
		this.m_emergencyKillAction = null;
		this.m_lateEmergencyKill = false;
		this.m_killScaleTime = true;
		this.m_tireRollSound = null;
		this.m_engineSound = null;
		this.m_boostSound = null;
		this.m_followingCoins = new List<CollectibleCoin>();
		if (this.m_minigame.m_editing && Vehicle.SHOW_TUTORIAL && string.IsNullOrEmpty(PsState.m_activeGameLoop.m_minigameId))
		{
			this.m_editorTutorial = new UIText(null, false, "playerTutorial", PsStrings.Get(StringID.TUTORIAL_TAP_TO_MOVE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, null, "313131");
			this.m_editorTutorial.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = Color.white;
			this.m_editorTutorial.m_shadowtmc.m_textMesh.GetComponent<Renderer>().material.color = DebugDraw.HexToColor("313131");
			this.m_editorTutorial.Update();
		}
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00022990 File Offset: 0x00020D90
	public virtual void InitMotors(ChipmunkBodyC _rearWheel, ChipmunkBodyC _frontWheel)
	{
		TransformC transformC = TransformS.AddComponent(this.m_entity, _rearWheel.TC.transform.position);
		this.m_rearMotor = ChipmunkProS.AddSimpleMotor(this.m_chassisBody, _rearWheel, transformC, 0f, 0f);
		ChipmunkProWrapper.ucpSimpleMotorSetRate(this.m_rearMotor.constraint, 0f);
		ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_rearMotor.constraint, 0f);
		transformC = TransformS.AddComponent(this.m_entity, _frontWheel.TC.transform.position);
		this.m_frontMotor = ChipmunkProS.AddSimpleMotor(this.m_chassisBody, _frontWheel, transformC, 0f, 0f);
		ChipmunkProWrapper.ucpSimpleMotorSetRate(this.m_frontMotor.constraint, 0f);
		ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_frontMotor.constraint, 0f);
		this.m_frontMotorForces = new Vector2(750000f, 1250000f);
		this.m_rearMotorForces = new Vector2(750000f, 1250000f);
		this.m_frontMaxRate = 50f;
		this.m_rearMaxRate = 50f;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00022AA3 File Offset: 0x00020EA3
	public void ReachedGoal()
	{
		if (this.m_booster != null)
		{
			this.m_booster = null;
		}
		if (this.m_trailBase != null)
		{
			this.m_trailBase.SetBoostActive(false);
		}
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00022AD4 File Offset: 0x00020ED4
	public override void Update()
	{
		if (this.m_lateEmergencyKill && this.m_emergencyKillAction != null && this.m_entity != null && this.m_entity.m_active)
		{
			this.m_emergencyKillAction.Invoke();
			this.m_emergencyKillAction = null;
			this.m_lateEmergencyKill = false;
		}
		if (this.m_minigame.m_editing && this.m_editorTutorial != null)
		{
			Vector3 vector = CameraS.m_mainCamera.WorldToScreenPoint(base.m_graphElement.m_TC.transform.position + Vector3.up * 80f);
			Vector3 vector2 = CameraS.m_uiCamera.ScreenToWorldPoint(vector);
			Vector3 vector3 = vector2;
			this.m_editorTutorial.m_TC.transform.position = new Vector3(vector3.x, vector3.y, 0f);
			if (base.m_graphElement.m_selected && !this.m_tutorialFade)
			{
				this.m_tutorialFade = true;
				TweenC tweenC = TweenS.AddTransformTween(this.m_editorTutorial.m_TC, TweenedProperty.Alpha, TweenStyle.CubicIn, Vector3.zero, 2f, 2f, true);
				TweenS.SetTweenAlphaProperties(tweenC, false, false, true, Shader.Find("Framework/FontShader"));
				TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
				{
					this.m_editorTutorial.Destroy();
					this.m_editorTutorial = null;
					Vehicle.SHOW_TUTORIAL = false;
				});
			}
		}
		base.Update();
		if (this.m_chassisBody != null)
		{
			this.m_lastFrameVelocity = ChipmunkProWrapper.ucpBodyGetVel(this.m_chassisBody.body);
		}
		else
		{
			this.m_lastFrameVelocity = Vector2.zero;
		}
		this.UpdateBoostSound();
		this.UpdateFollowingCoins();
		this.CallVehicleListeners();
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00022C68 File Offset: 0x00021068
	public void CallVehicleListeners()
	{
		if (this.m_vehicleListeners != null)
		{
			for (int i = 0; i < this.m_vehicleListeners.Count; i++)
			{
				this.m_vehicleListeners[i].Invoke(this);
			}
		}
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00022CAE File Offset: 0x000210AE
	public void AddVehicleListener(Action<Vehicle> _listener)
	{
		if (this.m_vehicleListeners == null)
		{
			this.m_vehicleListeners = new List<Action<Vehicle>>();
		}
		this.m_vehicleListeners.Add(_listener);
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00022CD2 File Offset: 0x000210D2
	public void RemoveVehicleListener(Action<Vehicle> _listener)
	{
		if (this.m_vehicleListeners != null)
		{
			this.m_vehicleListeners.Remove(_listener);
		}
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00022CEC File Offset: 0x000210EC
	public void RemoveAllVehicleListeners()
	{
		if (this.m_vehicleListeners != null)
		{
			while (this.m_vehicleListeners.Count > 0)
			{
				this.m_vehicleListeners.RemoveAt(0);
			}
		}
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00022D1C File Offset: 0x0002111C
	private void UpdateFollowingCoins()
	{
		if (this.m_chassisBody != null)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_chassisBody.body) + new Vector2(0f, 15f);
			for (int i = this.m_followingCoins.Count - 1; i > -1; i--)
			{
				CollectibleCoin collectibleCoin = this.m_followingCoins[i];
				if (!collectibleCoin.m_coin.m_collected)
				{
					Vector2 vector2 = ChipmunkProWrapper.ucpBodyGetPos(collectibleCoin.m_body.body);
					Vector2 vector3 = vector2 - vector;
					ChipmunkProWrapper.ucpBodySetPos(collectibleCoin.m_body.body, vector2 - vector3 * collectibleCoin.m_followStrength);
					collectibleCoin.m_followStrength += 0.005f;
					if (vector3.magnitude < 50f)
					{
						collectibleCoin.CollectCoin();
						this.m_followingCoins.RemoveAt(i);
					}
				}
			}
		}
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00022E08 File Offset: 0x00021208
	private void ClearAllFollowingCoins()
	{
		Debug.Log("Following coins cleared", null);
		for (int i = 0; i < this.m_followingCoins.Count; i++)
		{
			this.m_followingCoins[i].m_coin.m_collected = true;
		}
		this.m_followingCoins.Clear();
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00022E60 File Offset: 0x00021260
	protected override bool IsImmuneToGroundEffects(ContactInfo _ci)
	{
		ucpPointQueryInfo[] array = new ucpPointQueryInfo[5];
		ucpShapeFilter ucpShapeFilter = default(ucpShapeFilter);
		ucpShapeFilter.ucpShapeFilterAll();
		ucpShapeFilter.group = base.GetGroup();
		int num = ChipmunkProWrapper.ucpSpacePointQuery(_ci.m_contactPoint, 5f, ucpShapeFilter, array, 5);
		bool flag = false;
		for (int i = 0; i < num; i++)
		{
			IntPtr intPtr = ChipmunkProWrapper.ucpShapeGetBody(array[i].shape);
			if (intPtr != _ci.m_contactBody.body && intPtr != _ci.m_cmb.body && ChipmunkProWrapper.ucpBodyGetType(intPtr) == ucpBodyType.DYNAMIC)
			{
				flag = true;
				break;
			}
		}
		return flag;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00022F12 File Offset: 0x00021312
	public virtual void SetMotorParameters(Vector2 _frontForces, float _frontRate, Vector2 _rearForces, float _rearRate)
	{
		this.m_frontMotorForces = _frontForces;
		this.m_rearMotorForces = _rearForces;
		this.m_frontMaxRate = _frontRate;
		this.m_rearMaxRate = _rearRate;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00022F34 File Offset: 0x00021334
	public virtual void UpdateMotors(float _gasAmount, bool _rightIsForward = true, float _forceMultipler = 1f, bool _rearOnly = false)
	{
		if (_gasAmount != 0f)
		{
			bool flag;
			if (_rightIsForward)
			{
				flag = _gasAmount > 0f;
			}
			else
			{
				flag = _gasAmount < 0f;
			}
			float num = ((!flag) ? this.m_rearMotorForces.y : this.m_rearMotorForces.x) * _forceMultipler * Mathf.Abs(_gasAmount);
			ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_rearMotor.constraint, num);
			ChipmunkProWrapper.ucpSimpleMotorSetRate(this.m_rearMotor.constraint, this.m_rearMaxRate * (float)((_gasAmount >= 0f) ? 1 : (-1)));
			if (!_rearOnly)
			{
				float num2 = ((!flag) ? this.m_frontMotorForces.y : this.m_frontMotorForces.x) * _forceMultipler * Mathf.Abs(_gasAmount);
				ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_frontMotor.constraint, num2);
				ChipmunkProWrapper.ucpSimpleMotorSetRate(this.m_frontMotor.constraint, this.m_frontMaxRate * (float)((_gasAmount >= 0f) ? 1 : (-1)));
			}
		}
		else
		{
			ChipmunkProWrapper.ucpSimpleMotorSetRate(this.m_rearMotor.constraint, 0f);
			ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_rearMotor.constraint, 0f);
			if (!_rearOnly)
			{
				ChipmunkProWrapper.ucpSimpleMotorSetRate(this.m_frontMotor.constraint, 0f);
				ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_frontMotor.constraint, 0f);
			}
		}
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0002309C File Offset: 0x0002149C
	public virtual void SetHandBrake(bool _brake, float _force = 1000000f)
	{
		if (this.m_isDead)
		{
			return;
		}
		ChipmunkProWrapper.ucpSimpleMotorSetRate(this.m_rearMotor.constraint, 0f);
		ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_rearMotor.constraint, (!_brake) ? 0f : _force);
		ChipmunkProWrapper.ucpSimpleMotorSetRate(this.m_frontMotor.constraint, 0f);
		ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_frontMotor.constraint, (!_brake) ? 0f : _force);
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00023124 File Offset: 0x00021524
	public virtual void SetFrontBrake(bool _brake, float _force = 1000000f)
	{
		if (this.m_isDead)
		{
			return;
		}
		ChipmunkProWrapper.ucpSimpleMotorSetRate(this.m_frontMotor.constraint, 0f);
		ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_frontMotor.constraint, (!_brake) ? 0f : _force);
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00023173 File Offset: 0x00021573
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		this.ClearAllFollowingCoins();
		base.Kill(_damageType, _totalDamage);
		this.UpdateMotors(0f, true, 1f, false);
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00023195 File Offset: 0x00021595
	public override void Damage(Damage _damage, float _multiplier, Unit _source)
	{
		if (PsState.m_activeMinigame.m_playerReachedGoal)
		{
			return;
		}
		base.Damage(_damage, _multiplier, _source);
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x000231B0 File Offset: 0x000215B0
	public virtual void ExplosionEffect(Vector3 _position)
	{
		Entity entity = EntityManager.AddEntity(new string[] { "GTAG_AUTODESTROY" });
		TransformC transformC = TransformS.AddComponent(entity);
		transformC.transform.position = _position;
		PrefabC prefabC = PrefabS.AddComponent(transformC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.VehicleCrushEffect_GameObject));
		PrefabS.SetCamera(prefabC, CameraS.m_mainCamera);
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x00023205 File Offset: 0x00021605
	public override void SetGravity(Vector2 _gravity)
	{
		base.SetGravity(_gravity);
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x00023210 File Offset: 0x00021610
	public void SetBoostSound(TransformC _tc)
	{
		if (this.m_boostSound == null)
		{
			this.m_boostSound = SoundS.AddComponent(_tc, "/Ingame/Vehicles/BoosterLoop", 1f, false);
			SoundS.PlaySound(this.m_boostSound, false);
			SoundS.SetSoundParameter(this.m_boostSound, "RPM", this.m_currentRPM);
		}
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00023261 File Offset: 0x00021661
	public void RemoveBoostSound()
	{
		if (this.m_boostSound != null)
		{
			SoundS.RemoveComponent(this.m_boostSound);
			this.m_boostSound = null;
		}
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x00023280 File Offset: 0x00021680
	public void UpdateBoostSound()
	{
		if (this.m_boostSound != null)
		{
			SoundS.SetSoundParameter(this.m_boostSound, "RPM", this.m_currentRPM * this.m_currentLoad);
		}
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x000232AC File Offset: 0x000216AC
	public void SetEngineSound(TransformC _tc, string _sound, int _minRPM)
	{
		if (this.m_engineSound == null)
		{
			this.m_engineSound = SoundS.AddComponent(_tc, _sound, 1f, false);
			SoundS.PlaySound(this.m_engineSound, false);
			this.m_currentRPM = (float)_minRPM;
			this.m_currentLoad = 0f;
			SoundS.SetSoundParameter(this.m_engineSound, "RPM", this.m_currentRPM);
			SoundS.SetVolume(this.m_engineSound, 0f);
		}
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x0002331C File Offset: 0x0002171C
	public void RemoveEngineSound()
	{
		if (this.m_engineSound != null)
		{
			SoundS.RemoveComponent(this.m_engineSound);
			this.m_engineSound = null;
		}
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0002333C File Offset: 0x0002173C
	public void UpdateEngineSound(float _targetRPM, float _targetLoad)
	{
		if (this.m_engineSound != null)
		{
			this.m_currentRPM += (_targetRPM - this.m_currentRPM) * 0.1f;
			this.m_currentLoad += (_targetLoad - this.m_currentLoad) * 0.1f;
			SoundS.SetSoundParameter(this.m_engineSound, "RPM", this.m_currentRPM);
			SoundS.SetSoundParameter(this.m_engineSound, "Load", this.m_currentLoad);
			if (GameSceneEffectManager.m_signalNoiseMusicPlaying)
			{
				SoundS.SetVolume(this.m_engineSound, 0f);
			}
			else
			{
				SoundS.SetVolume(this.m_engineSound, 1f);
			}
		}
		else
		{
			this.m_currentRPM = 0f;
			this.m_currentLoad = 0f;
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x00023400 File Offset: 0x00021800
	public void SetTireRollSound(TransformC _tc, string _sound)
	{
		if (_sound != null && this.m_tireRollSound != null && _sound.Equals(this.m_tireRollSound.name))
		{
			SoundS.SetVolume(this.m_tireRollSound, 1f);
			return;
		}
		this.RemoveTireRollSound();
		if (this.m_tireRollSound == null && _sound != null)
		{
			this.m_tireRollSound = SoundS.AddComponent(_tc, _sound, 1f, false);
			SoundS.SetSoundParameter(this.m_tireRollSound, "RPM", this.m_currentRPM);
			SoundS.PlaySound(this.m_tireRollSound, false);
		}
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x00023491 File Offset: 0x00021891
	public void UpdateTireRollSound()
	{
		if (this.m_tireRollSound != null)
		{
			SoundS.SetSoundParameter(this.m_tireRollSound, "RPM", this.m_currentRPM);
		}
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x000234B4 File Offset: 0x000218B4
	public void MuteTireRollSound()
	{
		if (this.m_tireRollSound != null)
		{
			SoundS.SetVolume(this.m_tireRollSound, 0f);
		}
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x000234D1 File Offset: 0x000218D1
	public void RemoveTireRollSound()
	{
		if (this.m_tireRollSound != null)
		{
			SoundS.RemoveComponent(this.m_tireRollSound);
			this.m_tireRollSound = null;
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x000234F0 File Offset: 0x000218F0
	public virtual void LaunchRagdoll(bool _explodeBones = false)
	{
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x000234F4 File Offset: 0x000218F4
	public void StartDriveFx(TransformC _tc, bool _destructibleGround, string _fx)
	{
		if (this.m_drivingFx == null && !this.m_isDead && _fx != null)
		{
			Vector3 zero = Vector3.zero;
			if (!_destructibleGround)
			{
				zero..ctor(-33.13f, -20f, -2.91f);
			}
			else
			{
				zero..ctor(0f, -20f, -2.91f);
			}
			GameObject gameObject = ResourceManager.GetGameObject(_fx);
			this.m_drivingFx = PrefabS.AddComponent(_tc, zero, gameObject, string.Empty, false, true);
			this.m_drivingFxName = _fx;
		}
		else if (this.m_drivingFx != null && !_destructibleGround)
		{
			this.m_drivingFx.p_gameObject.GetComponent<ParticleSystem>().Play();
		}
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x000235A8 File Offset: 0x000219A8
	public void PauseDriveFx()
	{
		if (this.m_drivingFx != null)
		{
			this.m_drivingFx.p_gameObject.GetComponent<ParticleSystem>().Stop();
		}
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x000235CC File Offset: 0x000219CC
	public void RemoveDriveFx()
	{
		if (this.m_drivingFx != null)
		{
			TimerC timerC = TimerS.AddComponent(this.m_entity, "RemoveTimerForDriveFxEmitter", 2f, 0f, false, new TimerComponentDelegate(this.RemoveDriveFxTimerDelegate));
			timerC.customComponent = this.m_drivingFx;
			this.m_drivingFx.p_gameObject.GetComponent<ParticleSystem>().Stop();
			this.m_drivingFx = null;
			this.m_drivingFxName = string.Empty;
		}
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0002363F File Offset: 0x00021A3F
	public void RemoveDriveFxTimerDelegate(TimerC _c)
	{
		PrefabS.RemoveComponent(_c.customComponent as PrefabC, true);
		TimerS.RemoveComponent(_c);
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00023658 File Offset: 0x00021A58
	protected Vector2 GetRandomDebrisVel()
	{
		return this.m_lastFrameVelocity * 0.7f + new Vector2(Random.Range(-300f, 300f), Random.Range(200f, 400f));
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00023692 File Offset: 0x00021A92
	protected float GetRandomDebrisAngVel()
	{
		return Random.Range(-600f, 600f) * 0.017453292f;
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x000236AC File Offset: 0x00021AAC
	protected void CreateRandomImpactDebris(string[] chassisPartNames, GameObject debrisPrefab)
	{
		Vector2 randomDebrisVel = this.GetRandomDebrisVel();
		float randomDebrisAngVel = this.GetRandomDebrisAngVel();
		if (chassisPartNames.Length > 0)
		{
			int num = Random.Range(0, chassisPartNames.Length);
			Transform transform = this.m_chassisPrefab.p_gameObject.transform.Find(chassisPartNames[num]);
			if (transform != null && transform.gameObject.activeInHierarchy)
			{
				Debris.CreateDebrisFromGO(transform, randomDebrisVel, randomDebrisAngVel, true, base.GetGroup(), false, null, default(Vector3), -1f, 1U);
			}
		}
		if (debrisPrefab.transform.childCount > 0)
		{
			Vector3 vector = Vector3.forward * Random.Range(-5f, 5f);
			int num = Random.Range(0, debrisPrefab.transform.childCount);
			Debris.CreateDebrisFromGO(debrisPrefab.transform.GetChild(num), this.m_chassisBody.TC.transform.position + vector, randomDebrisVel, randomDebrisAngVel, false, base.GetGroup(), false, null, Vector3.zero, 5f, 1U);
		}
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x000237B4 File Offset: 0x00021BB4
	public void CheckVisualUpgrade(string _type, int _tier)
	{
		Transform transform;
		for (int i = 0; i <= 3; i++)
		{
			transform = this.m_chassisPrefab.p_gameObject.transform.Find(_type + "T" + i);
			if (transform != null)
			{
				transform.gameObject.SetActive(false);
			}
		}
		int num = _tier;
		transform = this.m_chassisPrefab.p_gameObject.transform.Find(_type + "T" + num);
		while (transform == null)
		{
			if (num == 0)
			{
				break;
			}
			num--;
			transform = this.m_chassisPrefab.p_gameObject.transform.Find(_type + "T" + num);
		}
		if (transform != null)
		{
			transform.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0002389C File Offset: 0x00021C9C
	public Texture GetTierTexture(int _tier)
	{
		int num = Mathf.Min(_tier - 1, 4);
		string text = string.Concat(new object[]
		{
			base.GetType().Name,
			"DifT",
			num,
			"_Texture2D"
		});
		return ResourceManager.GetTexture(text);
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x000238EC File Offset: 0x00021CEC
	public void DisableCollidersRenderer(Transform _transform)
	{
		IEnumerator enumerator = _transform.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.name.ToLower().Contains("collider"))
				{
					MeshRenderer component = transform.gameObject.GetComponent<MeshRenderer>();
					if (component != null)
					{
						component.enabled = false;
					}
				}
				this.DisableCollidersRenderer(transform);
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

	// Token: 0x0600048B RID: 1163 RVA: 0x0002398C File Offset: 0x00021D8C
	public static string GetUpgradeName(string _key)
	{
		if (_key != null)
		{
			if (_key == "power")
			{
				return "Power";
			}
			if (_key == "grip")
			{
				return "Grip";
			}
			if (_key == "handling")
			{
				return "Suspension";
			}
		}
		return null;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x000239E8 File Offset: 0x00021DE8
	public override void Destroy()
	{
		if (this.m_editorTutorial != null)
		{
			this.m_editorTutorial.Destroy();
		}
		this.m_editorTutorial = null;
		this.ClearAllFollowingCoins();
		base.Destroy();
		if (this.m_materialInstances != null)
		{
			for (int i = 0; i < this.m_materialInstances.Length; i++)
			{
				Object.Destroy(this.m_materialInstances[i]);
				this.m_materialInstances[i] = null;
			}
			this.m_materialInstances = null;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600048D RID: 1165 RVA: 0x00023A5F File Offset: 0x00021E5F
	// (set) Token: 0x0600048E RID: 1166 RVA: 0x00023A67 File Offset: 0x00021E67
	public int health
	{
		get
		{
			return this.m_health;
		}
		set
		{
			this.m_health = Mathf.Clamp(value, -1, 3);
		}
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x00023A77 File Offset: 0x00021E77
	public bool DecreaseHealth(int damage = 1)
	{
		if (this.health > 0)
		{
			this.m_health = Mathf.Max(this.m_health - damage, 0);
		}
		return this.m_health == 0;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00023AA8 File Offset: 0x00021EA8
	public Hashtable GetActualPerformanceValues()
	{
		Hashtable hashtable = new Hashtable();
		IEnumerator enumerator = Enum.GetValues(typeof(PsUpgradeManager.UpgradeType)).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				PsUpgradeManager.UpgradeType upgradeType = (PsUpgradeManager.UpgradeType)obj;
				hashtable.Add(upgradeType.ToString(), this.GetUpgradeValue(base.GetType(), upgradeType));
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
		hashtable.Add("version", 2);
		float currentPerformance = PsUpgradeManager.GetCurrentPerformance(base.GetType());
		hashtable.Add("cc", currentPerformance);
		Debug.LogError("Upgrades: " + Json.Serialize(hashtable));
		return hashtable;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00023B80 File Offset: 0x00021F80
	protected float GetUpgradeValue(Type _vehicleType, PsUpgradeManager.UpgradeType _upgradeType)
	{
		float num = PsUpgradeManager.GetNormalizedUpgradeValue(base.GetType(), _upgradeType);
		float currentPerformance = PsUpgradeManager.GetCurrentPerformance(_vehicleType);
		float num2 = PsState.m_activeGameLoop.GetOverrideCC();
		float maxEfficiency = PsUpgradeManager.GetMaxEfficiency(_vehicleType, _upgradeType);
		if (PsState.m_activeGameLoop != null && num2 >= 64f)
		{
			num = (num2 - 64f) * (maxEfficiency / 1136f) / maxEfficiency;
		}
		else if (PsState.m_activeGameLoop.UseCreatorVehicle())
		{
			if (PsState.m_activeGameLoop.m_minigameMetaData != null)
			{
				if (PsState.m_activeGameLoop.m_minigameMetaData.creatorUpgrades.ContainsKey(_upgradeType.ToString()))
				{
					num = Convert.ToSingle(PsState.m_activeGameLoop.m_minigameMetaData.creatorUpgrades[_upgradeType.ToString()]);
				}
				else
				{
					Debug.LogError("CREATOR VEHICLE DID NOT CONTAIN UPGRADETYPE: " + _upgradeType.ToString());
				}
			}
			else
			{
				Debug.LogError("Minigamemetadata was null!!!!");
			}
		}
		else if (PsState.m_activeGameLoop is PsGameLoopTournament)
		{
			num2 = (PsState.m_activeGameLoop as PsGameLoopTournament).GetCcCap();
			float num3 = (num2 - 64f) * (maxEfficiency / 1136f) / maxEfficiency;
			if (currentPerformance >= num2)
			{
				num = num3;
			}
			else
			{
				num = Math.Min(num, num3);
			}
		}
		if (float.IsNaN(num))
		{
			return -1f;
		}
		return num;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x00023CDC File Offset: 0x000220DC
	protected float GetUpgradeEfficiency(Type _vehicleType, PsUpgradeManager.UpgradeType _upgradeType)
	{
		float num = PsUpgradeManager.GetCurrentEfficiency(base.GetType(), _upgradeType);
		float currentPerformance = PsUpgradeManager.GetCurrentPerformance(_vehicleType);
		float overrideCC = PsState.m_activeGameLoop.GetOverrideCC();
		float maxEfficiency = PsUpgradeManager.GetMaxEfficiency(_vehicleType, _upgradeType);
		if (PsState.m_activeGameLoop != null && overrideCC >= 64f)
		{
			num = maxEfficiency * ((overrideCC - 64f) / 1136f);
		}
		else if (PsState.m_activeGameLoop.UseCreatorVehicle())
		{
			if (PsState.m_activeGameLoop.m_minigameMetaData != null)
			{
				if (PsState.m_activeGameLoop.m_minigameMetaData.creatorUpgrades.ContainsKey(_upgradeType.ToString()))
				{
					num = Convert.ToSingle(PsState.m_activeGameLoop.m_minigameMetaData.creatorUpgrades[_upgradeType.ToString()]);
				}
				else
				{
					Debug.LogError("CREATOR VEHICLE DID NOT CONTAIN UPGRADETYPE: " + _upgradeType.ToString());
				}
			}
			else
			{
				Debug.LogError("Minigamemetadata was null!!!!");
			}
		}
		else if (PsState.m_activeGameLoop is PsGameLoopTournament)
		{
			float ccCap = (PsState.m_activeGameLoop as PsGameLoopTournament).GetCcCap();
			float num2 = maxEfficiency * ((ccCap - 64f) / 1136f);
			if (currentPerformance >= ccCap)
			{
				num = num2;
			}
			else
			{
				num = Math.Min(num, num2);
			}
		}
		if (float.IsNaN(num))
		{
			return -1f;
		}
		return num;
	}

	// Token: 0x040005C8 RID: 1480
	public const int UPGRADE_TIER_1 = 5;

	// Token: 0x040005C9 RID: 1481
	public const int UPGRADE_TIER_2 = 10;

	// Token: 0x040005CA RID: 1482
	public const int UPGRADE_TIER_3 = 15;

	// Token: 0x040005CB RID: 1483
	public const int DOUBLE_FLIP_EXTRA_DURATION = 15;

	// Token: 0x040005CC RID: 1484
	public const int BOOST_BASE_DURATION = 60;

	// Token: 0x040005CD RID: 1485
	public const float BOOST_BASE_POWER = 1.5f;

	// Token: 0x040005CE RID: 1486
	protected string[] m_chassisDebrisParts;

	// Token: 0x040005CF RID: 1487
	public PrefabC m_chassisPrefab;

	// Token: 0x040005D0 RID: 1488
	public Shield m_shield;

	// Token: 0x040005D1 RID: 1489
	public Armor m_armor;

	// Token: 0x040005D2 RID: 1490
	public Weapon m_weapon;

	// Token: 0x040005D3 RID: 1491
	public ChipmunkBodyC m_chassisBody;

	// Token: 0x040005D4 RID: 1492
	public ChipmunkConstraintC m_rearMotor;

	// Token: 0x040005D5 RID: 1493
	public ChipmunkConstraintC m_frontMotor;

	// Token: 0x040005D6 RID: 1494
	private Vector2 m_frontMotorForces;

	// Token: 0x040005D7 RID: 1495
	private Vector2 m_rearMotorForces;

	// Token: 0x040005D8 RID: 1496
	private float m_frontMaxRate;

	// Token: 0x040005D9 RID: 1497
	private float m_rearMaxRate;

	// Token: 0x040005DA RID: 1498
	public int m_upgradeSteps;

	// Token: 0x040005DB RID: 1499
	public AlienCharacter m_alienCharacter;

	// Token: 0x040005DC RID: 1500
	public TransformC m_camTargetTC;

	// Token: 0x040005DD RID: 1501
	public SoundC m_engineSound;

	// Token: 0x040005DE RID: 1502
	public float m_currentRPM;

	// Token: 0x040005DF RID: 1503
	public float m_currentLoad;

	// Token: 0x040005E0 RID: 1504
	public SoundC m_tireRollSound;

	// Token: 0x040005E1 RID: 1505
	public SoundC m_boostSound;

	// Token: 0x040005E2 RID: 1506
	protected Material[] m_materialInstances;

	// Token: 0x040005E3 RID: 1507
	public PrefabC m_drivingFx;

	// Token: 0x040005E4 RID: 1508
	public string m_drivingFxName;

	// Token: 0x040005E5 RID: 1509
	public Booster m_booster;

	// Token: 0x040005E6 RID: 1510
	public PsPowerUp m_powerUp;

	// Token: 0x040005E7 RID: 1511
	public List<CollectibleCoin> m_followingCoins;

	// Token: 0x040005E8 RID: 1512
	public bool m_killScaleTime;

	// Token: 0x040005E9 RID: 1513
	protected Action m_emergencyKillAction;

	// Token: 0x040005EA RID: 1514
	public bool m_lateEmergencyKill;

	// Token: 0x040005EB RID: 1515
	public bool m_hasBrokenDown;

	// Token: 0x040005EC RID: 1516
	public PsTrailBase m_trailBase;

	// Token: 0x040005ED RID: 1517
	public GameObject m_trail;

	// Token: 0x040005EE RID: 1518
	private UIText m_editorTutorial;

	// Token: 0x040005EF RID: 1519
	private bool m_tutorialFade;

	// Token: 0x040005F0 RID: 1520
	public static bool SHOW_TUTORIAL = true;

	// Token: 0x040005F1 RID: 1521
	private List<Action<Vehicle>> m_vehicleListeners;

	// Token: 0x040005F2 RID: 1522
	public Action d_boostTriggeredDelegate;

	// Token: 0x040005F3 RID: 1523
	private int m_health = 2;

	// Token: 0x040005F4 RID: 1524
	public const int MAX_HEALTH = 3;
}
