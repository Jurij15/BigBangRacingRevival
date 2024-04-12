using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class AlienCharacter : Unit
{
	// Token: 0x0600012D RID: 301 RVA: 0x0000DD14 File Offset: 0x0000C114
	public AlienCharacter(GraphElement _graphElement, string driveState, string _assetName = "AlienNewPrefab_GameObject")
		: base(_graphElement, UnitType.Character)
	{
		this.m_group = base.GetGroup();
		this.m_bones = new List<Transform>();
		this.m_mainTC = TransformS.AddComponent(this.m_entity, "AlienCharacter");
		TransformS.SetTransform(this.m_mainTC, _graphElement.m_position, Vector3.zero);
		this.m_mainPC = PrefabS.AddComponent(this.m_mainTC, Vector3.zero, ResourceManager.GetGameObject(_assetName));
		PrefabS.SetCamera(this.m_mainPC, CameraS.m_mainCamera);
		this.DisableColliderRenderers(this.m_mainPC.p_gameObject.transform);
		this.m_animator = this.m_mainPC.p_gameObject.GetComponent<Animator>();
		this.m_animatorController = ResourceManager.GetResource<RuntimeAnimatorController>(RESOURCE.AlienAnimatorController_AnimatorController);
		this.m_animator.runtimeAnimatorController = this.m_animatorController;
		if (_graphElement.m_flipped)
		{
			TransformS.SetGlobalRotation(this.m_mainTC, Vector3.up * 270f);
		}
		else
		{
			TransformS.SetGlobalRotation(this.m_mainTC, Vector3.up * 90f);
		}
		this.m_bindPoseState = Animator.StringToHash("Base.Bind");
		this.m_standPoseState = Animator.StringToHash("Base.Stand");
		this.m_drivePoseState = Animator.StringToHash("Base." + driveState);
		this.m_animator.Play(this.m_drivePoseState);
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.m_canElectrify = true;
		this.m_checkForCrushing = true;
		this.m_crushTolerance = 2000;
		this.m_beamOutTimer = 40;
		this.m_alienEffects = this.m_mainPC.p_gameObject.GetComponent<AlienEffects>();
		if (this.m_alienEffects != null)
		{
			this.m_alienEffects.Initialize();
		}
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000DF20 File Offset: 0x0000C320
	public override void Destroy()
	{
		if (this.m_materialInstances != null)
		{
			for (int i = 0; i < this.m_materialInstances.Length; i++)
			{
				Object.Destroy(this.m_materialInstances[i]);
				this.m_materialInstances[i] = null;
			}
			this.m_materialInstances = null;
		}
		while (this.m_bones.Count > 0)
		{
			int num = this.m_bones.Count - 1;
			Object.Destroy(this.m_bones[num].gameObject);
			this.m_bones.RemoveAt(num);
		}
		base.Destroy();
		this.m_ragdoll = null;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0000DFC4 File Offset: 0x0000C3C4
	public void DisableColliderRenderers(Transform _tc)
	{
		if (_tc.GetComponent<Renderer>() != null && _tc.name.Contains("Collider"))
		{
			_tc.GetComponent<Renderer>().enabled = false;
		}
		for (int i = 0; i < _tc.childCount; i++)
		{
			this.DisableColliderRenderers(_tc.GetChild(i));
		}
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0000E027 File Offset: 0x0000C427
	public void SetPropToLocator(ResourcePool.Asset _resourceObject, string _locatorPathName)
	{
		this.SetPropToLocator(ResourceManager.GetGameObject(_resourceObject), _locatorPathName);
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000E038 File Offset: 0x0000C438
	public void SetPropToLocator(GameObject _resourceGameObject, string _locatorPathName)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(_resourceGameObject);
		PrefabS.SetCamera(gameObject, CameraS.m_mainCamera);
		Transform transform = this.m_mainPC.p_gameObject.transform.Find(_locatorPathName);
		gameObject.transform.parent = transform;
		gameObject.transform.localPosition = Vector3.zero;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0000E08C File Offset: 0x0000C48C
	public string SetHat()
	{
		PsCustomisationData vehicleCustomisationData = PsCustomisationManager.GetVehicleCustomisationData(PsState.GetCurrentVehicleType(true));
		PsCustomisationItem installedItemByCategory = vehicleCustomisationData.GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		if (installedItemByCategory != null)
		{
			this.m_hatIdentifier = installedItemByCategory.m_identifier;
		}
		GameObject hatPrefabByIdentifier = PsCustomisationManager.GetHatPrefabByIdentifier(this.m_hatIdentifier);
		this.SetPropToLocator(hatPrefabByIdentifier, "Hips/Spine1/Spine2/Neck/Head/HeadCollider/HeadGearLocator");
		return this.m_hatIdentifier;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000E0E0 File Offset: 0x0000C4E0
	public void SetHat(string _identifier)
	{
		GameObject hatPrefabByIdentifier = PsCustomisationManager.GetHatPrefabByIdentifier(_identifier);
		this.SetPropToLocator(hatPrefabByIdentifier, "Hips/Spine1/Spine2/Neck/Head/HeadCollider/HeadGearLocator");
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000E100 File Offset: 0x0000C500
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Weapon, 0f);
		this.SetBaseArmor(DamageType.Electric, 0f);
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000E120 File Offset: 0x0000C520
	public void AnimateCharacterRandom(string _trigger, int _value)
	{
		if (this.m_animator != null)
		{
			this.m_animator.SetInteger("Random", _value);
			this.m_animator.SetTrigger(_trigger);
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000E150 File Offset: 0x0000C550
	public void AnimateCharacter(string _trigger)
	{
		if (this.m_animator != null)
		{
			this.m_animator.SetTrigger(_trigger);
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000E16F File Offset: 0x0000C56F
	public void AnimateCharacter(string _param, int _value)
	{
		if (this.m_animator != null)
		{
			this.m_animator.SetInteger(_param, _value);
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000E18F File Offset: 0x0000C58F
	public override void InactiveUpdate()
	{
		base.InactiveUpdate();
		this.BeamOutUpdate();
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000E19D File Offset: 0x0000C59D
	public override void Update()
	{
		base.Update();
		if (this.m_electrificationTime > 1f && this.m_canElectrify)
		{
			this.ExplodeRagdoll(this.m_ragdoll);
		}
		this.Blink();
		this.BeamOutUpdate();
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000E1D8 File Offset: 0x0000C5D8
	private void Blink()
	{
		if (this.m_animator != null && !this.m_dontBlink)
		{
			if (this.m_blinkTimer <= 0f)
			{
				if (!this.m_animator.GetCurrentAnimatorStateInfo(1).IsName("Blink") && this.m_animator.GetCurrentAnimatorStateInfo(2).IsName("Default"))
				{
					this.AnimateCharacter("Blink");
				}
				this.m_blinkTimer = Random.Range(1f, this.m_blinkFrequency);
			}
			else
			{
				this.m_blinkTimer -= Main.m_gameDeltaTime;
			}
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000E288 File Offset: 0x0000C688
	public void BeamOutUpdate()
	{
		if (this.m_minigame.m_playerBeamingOut)
		{
			bool flag = !this.m_minigame.m_playerReachedGoal && !this.m_crushed;
			if (!this.m_beamInitialized)
			{
				this.m_beamOutTimer = 40;
				this.m_beamInitialized = true;
			}
			if (flag && ((!this.m_minigame.m_gameEnded && this.m_beamOutTimer == 40) || (this.m_minigame.m_gameEnded && this.m_beamOutTimer == 40)))
			{
				SoundS.PlaySingleShot("/InGame/Events/RestartBeam", Vector3.zero, 1f);
				this.m_beamFxTC = TransformS.AddComponent(this.m_entity, this.m_mainTC.transform.position);
				PrefabS.AddComponent(this.m_beamFxTC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.BeamUp_GameObject));
				this.m_beamFxTC.transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
			}
			if (this.m_beamOutTimer <= 0)
			{
				this.m_minigame.m_playerBeamingOut = true;
			}
			if (flag)
			{
				float num = 1f - ToolBox.getPositionBetween((float)this.m_beamOutTimer, 10f, 40f);
				TransformS.SetPosition(this.m_beamFxTC, this.m_mainTC.transform.position);
				Component[] componentsInChildren = this.m_mainPC.p_gameObject.GetComponentsInChildren(typeof(Renderer), false);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					(componentsInChildren[i] as Renderer).material.SetFloat("_Cutoff", num);
				}
				if (this.m_beamOutTimer == 0)
				{
					this.m_animator = null;
					EntityManager.SetActivityOfEntity(this.m_entity, false, true, true, false, true, true);
				}
			}
			this.m_beamOutTimer--;
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000E464 File Offset: 0x0000C864
	public void AddLinearVelocityToRagDoll(RagdollNode _node, Vector2 _addition, float _random)
	{
		if (_node != null)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetVel(_node.m_cmb.body);
			float num = Random.value * _random - _random * 0.5f;
			float num2 = Random.value * _random - _random * 0.5f;
			Vector2 vector2 = vector + _addition + new Vector2(num, num2);
			ChipmunkProWrapper.ucpBodySetVel(_node.m_cmb.body, vector + _addition + new Vector2(num, num2));
			for (int i = 0; i < _node.m_childs.Count; i++)
			{
				this.AddLinearVelocityToRagDoll(_node.m_childs[i], _addition, _random);
			}
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000E514 File Offset: 0x0000C914
	public void ConstructRagdoll(bool _skipJoints = false)
	{
		if (this.m_ragdoll == null)
		{
			if (this.m_mainTC.parent != null)
			{
				TransformS.UnparentComponent(this.m_mainTC, true);
			}
			Vector3 eulerAngles = this.m_mainTC.transform.eulerAngles;
			if (eulerAngles.x > 180f)
			{
				eulerAngles.x -= 360f;
			}
			if (eulerAngles.z == 180f)
			{
				eulerAngles.x = -eulerAngles.x + 180f;
				if (base.m_graphElement.m_flipped)
				{
					eulerAngles.y = 270f;
				}
				else
				{
					eulerAngles.y = 90f;
				}
				eulerAngles.z = 0f;
			}
			this.m_mainTC.transform.rotation = Quaternion.Euler(eulerAngles);
			this.m_ragdoll = this.CreateRagdoll(this.m_mainPC.p_gameObject.transform, _skipJoints, null);
			this.m_ragdoll.m_ragdollJointBootstrap.m_mainTC = this.m_mainTC;
			this.CreateCamTarget(this.m_mainTC);
			Vector2 vector = this.m_minigame.m_globalGravity * (float)this.m_minigame.m_gravityMultipler;
			this.SetGravity(vector);
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000E658 File Offset: 0x0000CA58
	public void ThrowHelmet(Vector2 _force = default(Vector2), float _angVel = 0f, bool _toBackground = false, uint _layer = 17895696U, bool _tween = false, bool flyUp = false)
	{
		if (!this.m_hasHelmet)
		{
			return;
		}
		Transform transform = this.m_mainPC.p_gameObject.transform.Find("Hips/Spine1/Spine2/Neck/Head/HeadCollider/HeadGearLocator");
		if (transform != null && transform.childCount > 0 && transform.GetChild(0).childCount > 0)
		{
			if (_force == Vector2.zero || flyUp)
			{
				_force += new Vector2(this.m_mainTC.transform.up.x, this.m_mainTC.transform.up.y) * 400f;
			}
			string[] array = new string[]
			{
				"Fish", "CatHat", "GirlyHair", "WitchHat", "MrBaconHair", "ReversalCrown", "FeatherHat", "SteelMask", "HawkMask", "PowerHelmet",
				"BootHat", "ToadHat", "ReindeerHat", "AnglerFishHat", "BobbleHat", "MilkJugHat", "LorpHeadband"
			};
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				flag = this.m_hatIdentifier == array[i];
				if (flag)
				{
					break;
				}
			}
			if (flag)
			{
				Vector2 vector = transform.GetChild(0).GetChild(0).position;
				GameObject gameObject;
				if (this.m_hatIdentifier == "ToadHat")
				{
					gameObject = ResourceManager.GetGameObject(RESOURCE.EffectToadSplurt_GameObject);
					SoundS.PlaySingleShot("/Ingame/Units/ToadDeath", vector, 1f);
				}
				else
				{
					gameObject = ResourceManager.GetGameObject(RESOURCE.EffectGenericHatsplosion_GameObject);
					SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
				}
				if (gameObject != null)
				{
					EntityManager.AddTimedFXEntity(gameObject, new Vector3(vector.x, vector.y, 0f), Vector3.zero, 2f, "GTAG_AUTODESTROY", null);
				}
			}
			else
			{
				new PsHat(transform.GetChild(0).GetChild(0), _force, _angVel);
			}
			Object.Destroy(transform.gameObject);
			this.m_hasHelmet = false;
		}
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000E8D0 File Offset: 0x0000CCD0
	public override void EmergencyKill()
	{
		if (!this.m_crushed)
		{
			Debug.Log("EMERGENCY KILL CHARACTER", null);
			if (this.m_ragdoll != null)
			{
				this.ExplodeRagdoll(this.m_ragdoll);
			}
			this.m_animator = null;
			this.m_crushed = true;
			CameraS.RemoveAllTargetComponents();
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000E920 File Offset: 0x0000CD20
	public void CreateCamTarget(TransformC _tc)
	{
		if (!GameLevelPreview.m_levelPreviewRunning)
		{
			if (this.m_camTarget != null)
			{
				CameraS.RemoveTargetComponent(this.m_camTarget);
			}
			this.m_camTarget = CameraS.AddTargetComponent(_tc, this.m_targetWidth, this.m_targetHeight, this.m_targetOffset);
			this.m_camTarget.interpolateSpeed = 0.033333335f;
			this.m_camTarget.frameGrowVelocityMultiplier = new Vector2(10f, 10f);
			this.m_camTarget.framePosVelocityMultiplier = new Vector2(41f, 30f);
			this.m_camTarget.frameScaleVelocityMultiplier = 0.02f;
			this.m_camTarget.frameSlopRadiusMinMax = new Vector2(0f, 200f);
			this.m_camTarget.velAngleChangeMult = new Vector2(2f, 3.5f);
			this.m_camTarget.angleLimits = new Vector2(8f, 16f);
			this.m_camTarget.framePeekShiftMax = new Vector2(0f, 0f);
			this.m_camTarget.framePeekShiftMultiplier = 8f;
			this.m_camTarget.frameWorldBounds.b = (float)(-(float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight) * 0.5f - 100f;
		}
		SoundS.SetListener(_tc.transform.gameObject, true);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000EA75 File Offset: 0x0000CE75
	public void SetVehicleSelectTarget(float _width, float _height, Vector2 _offset)
	{
		if (this.m_camTarget != null)
		{
			this.m_camTarget.offset = _offset;
			CameraS.SetTargetBB(this.m_camTarget, _width, _height);
		}
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0000EA9B File Offset: 0x0000CE9B
	public void ResetCameraTarget()
	{
		if (this.m_camTarget != null)
		{
			this.m_camTarget.offset = this.m_targetOffset;
			CameraS.SetTargetBB(this.m_camTarget, this.m_targetWidth, this.m_targetHeight);
		}
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000EAD0 File Offset: 0x0000CED0
	private void ExplodeRagdoll(RagdollNode _node)
	{
		this.m_canElectrify = false;
		this.m_electricChainIndex = -1;
		Transform transform = null;
		for (int i = 0; i < _node.m_transform.childCount; i++)
		{
			Transform child = _node.m_transform.GetChild(i);
			if (child.name.Contains("AlienBones"))
			{
				transform = child;
				if (child.name == "AlienBones_LegUpperRight" || child.name == "AlienBones_LegUpperLeft" || child.name == "AlienBones_Skull" || child.name == "AlienBones_ArmUpperRight" || child.name == "AlienBones_ArmUpperLeft")
				{
					TransformS.UnparentComponent(_node.m_tc, true);
					if (_node.m_pivotJoint != IntPtr.Zero)
					{
						ChipmunkProWrapper.ucpRemoveConstraint(_node.m_pivotJoint);
						_node.m_pivotJoint = IntPtr.Zero;
					}
					if (_node.m_rotaryLimitJoint != IntPtr.Zero)
					{
						ChipmunkProWrapper.ucpRemoveConstraint(_node.m_rotaryLimitJoint);
						_node.m_rotaryLimitJoint = IntPtr.Zero;
					}
					break;
				}
			}
		}
		if (transform != null)
		{
			transform.parent = _node.m_tc.transform;
			this.m_bones.Add(transform);
		}
		for (int j = 0; j < _node.m_childs.Count; j++)
		{
			this.ExplodeRagdoll(_node.m_childs[j]);
		}
		if (_node == this.m_ragdoll)
		{
			SoundS.PlaySingleShot("/Ingame/Characters/AlienCrush", Vector3.zero, 1f);
			this.SetElectrified(false, Vector2.zero);
			this.m_canElectrify = false;
			this.m_crushed = true;
			this.m_checkForCrushing = false;
			if (this.m_alienEffects != null)
			{
				this.m_alienEffects.Explode();
			}
			Vector2 vector = this.m_lastFrameVelocity * 0.7f;
			if (vector.y < 0f)
			{
				vector.y *= 0.5f;
			}
			this.AddLinearVelocityToRagDoll(this.m_ragdoll, Vector2.up * 200f, 300f);
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000ED08 File Offset: 0x0000D108
	private RagdollNode CreateRagdoll(Transform _transform, bool _skipJoints = false, RagdollNode _parentNode = null)
	{
		if (_parentNode == null)
		{
			this.m_animator.Play(this.m_drivePoseState);
			this.AnimateCharacter("DriveDir", 1);
			this.AnimateCharacter("LeanDir", 0);
			this.m_animator.Update(10f);
			if (_skipJoints)
			{
				this.m_crushed = true;
				this.m_checkForCrushing = false;
			}
		}
		RagdollNode ragdollNode = null;
		bool flag = false;
		Transform transform = null;
		Transform transform2 = null;
		for (int i = 0; i < _transform.childCount; i++)
		{
			Transform child = _transform.GetChild(i);
			if (child.name.Contains("Collider"))
			{
				transform = child;
			}
			if (_skipJoints && child.name.Contains("AlienBones"))
			{
				transform2 = child;
				if (child.name == "AlienBones_LegUpperRight" || child.name == "AlienBones_LegUpperLeft" || child.name == "AlienBones_Skull" || child.name == "AlienBones_ArmUpperRight" || child.name == "AlienBones_ArmUpperLeft")
				{
					flag = true;
				}
			}
		}
		if (transform != null)
		{
			ragdollNode = new RagdollNode(transform.parent);
			ragdollNode.m_flipped = base.m_graphElement.m_flipped;
			Vector2[] array = ChipmunkProS.GenerateCollisionVertexArrayFromGameObject(transform.gameObject, true, ragdollNode.m_flipped);
			ragdollNode.m_centeroid = ChipmunkProWrapper.ucpCenteroidForPoly(array.Length, array);
			float num = ChipmunkProWrapper.ucpAreaForPoly(array.Length, array, 0f);
			num /= 1100f;
			float num2 = (2f + 4f * num) * 2.5f;
			ragdollNode.m_tc = TransformS.AddComponent(this.m_entity, transform.position + ragdollNode.m_centeroid);
			ragdollNode.m_tc.transform.name = ragdollNode.m_name;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			for (int j = 0; j < transform.childCount; j++)
			{
				Transform child2 = transform.GetChild(j);
				if (child2.name.Contains("Angle"))
				{
					Vector2 vector = child2.position - transform.position;
					num3 = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
					if (ragdollNode.m_flipped)
					{
						num3 += 180f;
					}
				}
				else if (child2.name.Contains("Min"))
				{
					num4 = child2.localEulerAngles.x;
					if (num4 >= 180f)
					{
						num4 -= 360f;
					}
				}
				else if (child2.name.Contains("Max"))
				{
					num5 = child2.localEulerAngles.x;
					if (num5 >= 180f)
					{
						num5 -= 360f;
					}
				}
			}
			ragdollNode.m_tc.transform.RotateAround(transform.position, Vector3.forward, num3);
			ucpPolyShape ucpPolyShape = new ucpPolyShape(array, -ragdollNode.m_centeroid, 17895696U, num2, 0.5f, 1f, (ucpCollisionType)4, false);
			ucpPolyShape.group = this.m_group;
			if (transform.name == "HeadCollider")
			{
				ucpPolyShape.group += 1U;
			}
			ragdollNode.m_cmb = ChipmunkProS.AddDynamicBody(ragdollNode.m_tc, ucpPolyShape, null);
			ChipmunkProS.SetBodyAngularDamp(ragdollNode.m_cmb, 0.9f);
			ChipmunkProS.SetBodyLinearDamp(ragdollNode.m_cmb, new Vector2(0.997f, 0.997f));
			ChipmunkProS.AddCollisionHandler(ragdollNode.m_cmb, new CollisionDelegate(this.RagdollCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)2, true, false, false);
			ChipmunkProS.AddCollisionHandler(ragdollNode.m_cmb, new CollisionDelegate(this.RagdollCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, true, false, false);
			if (this.m_unitC != null)
			{
				ragdollNode.m_cmb.customComponent = this.m_unitC;
			}
			if (transform2 != null)
			{
				transform2.parent = ragdollNode.m_tc.transform;
			}
			if (_parentNode != null)
			{
				_parentNode.m_childs.Add(ragdollNode);
				ragdollNode.m_parent = _parentNode;
				if (!flag)
				{
					TransformS.ParentComponent(ragdollNode.m_tc, ragdollNode.m_parent.m_tc);
					ragdollNode.m_offset = Vector3.zero;
					if (ragdollNode.m_parent != null)
					{
						ragdollNode.m_offset = ragdollNode.m_transform.eulerAngles - ragdollNode.m_parent.m_transform.eulerAngles;
					}
					ragdollNode.m_pivotJoint = ChipmunkProWrapper.ucpPivotJointNew(_parentNode.m_cmb.body, ragdollNode.m_cmb.body, transform.position);
					ChipmunkProWrapper.ucpAddConstraint(ragdollNode.m_pivotJoint, -1);
					ragdollNode.m_rotaryLimitJoint = ChipmunkProWrapper.ucpRotaryLimitJointNew(_parentNode.m_cmb.body, ragdollNode.m_cmb.body, num4 * 0.017453292f, num5 * 0.017453292f);
					ChipmunkProWrapper.ucpConstraintSetMaxForce(ragdollNode.m_rotaryLimitJoint, 50000f * num2);
					ChipmunkProWrapper.ucpAddConstraint(ragdollNode.m_rotaryLimitJoint, -1);
				}
			}
		}
		if (ragdollNode == null && _parentNode != null)
		{
			ragdollNode = _parentNode;
		}
		for (int k = 0; k < _transform.childCount; k++)
		{
			Transform child3 = _transform.GetChild(k);
			if (_skipJoints && transform == null && transform2 != null)
			{
				transform2.parent = ragdollNode.m_tc.transform;
			}
			RagdollNode ragdollNode2 = this.CreateRagdoll(child3, _skipJoints, ragdollNode);
			if (ragdollNode == null)
			{
				ragdollNode = ragdollNode2;
			}
		}
		return ragdollNode;
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000F2B8 File Offset: 0x0000D6B8
	private void RagdollCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (_phase == ucpCollisionPhase.Begin)
		{
			float positionBetween = ToolBox.getPositionBetween(_pair.impulse.magnitude, 3000f, 10000f);
			if (positionBetween > 0f)
			{
				SoundS.PlaySingleShotWithParameter("/InGame/Characters/AlienBodyImpact", Vector3.zero, "Force", 0.2f + positionBetween * 0.8f, 1f);
			}
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000F318 File Offset: 0x0000D718
	public override void SetElectrified(bool _electrify, Vector2 _contactPoint)
	{
		base.SetElectrified(_electrify, _contactPoint);
		if (this.m_isElectrified && this.m_nextElectrifiedEffect < Main.m_resettingGameTime)
		{
			this.m_nextElectrifiedEffect = Main.m_resettingGameTime + 1f;
			SoundS.PlaySingleShot("/Ingame/Characters/AlienElectrocution", Vector2.zero, 1f);
		}
		if (this.m_alienEffects != null && !this.m_crushed)
		{
			if (_electrify)
			{
				this.m_alienEffects.PlayElectrocute();
			}
			else
			{
				this.m_alienEffects.StopElectrocute();
			}
		}
	}

	// Token: 0x040000E9 RID: 233
	private const int BEAMING_OUT_DELAY = 40;

	// Token: 0x040000EA RID: 234
	public uint m_group;

	// Token: 0x040000EB RID: 235
	public CameraTargetC m_camTarget;

	// Token: 0x040000EC RID: 236
	public TransformC m_mainTC;

	// Token: 0x040000ED RID: 237
	public PrefabC m_mainPC;

	// Token: 0x040000EE RID: 238
	public Animator m_animator;

	// Token: 0x040000EF RID: 239
	private RuntimeAnimatorController m_animatorController;

	// Token: 0x040000F0 RID: 240
	private int m_bindPoseState;

	// Token: 0x040000F1 RID: 241
	private int m_standPoseState;

	// Token: 0x040000F2 RID: 242
	private int m_drivePoseState;

	// Token: 0x040000F3 RID: 243
	public RagdollNode m_ragdoll;

	// Token: 0x040000F4 RID: 244
	public bool m_crushed;

	// Token: 0x040000F5 RID: 245
	private float m_blinkTimer;

	// Token: 0x040000F6 RID: 246
	private float m_blinkFrequency = 2.5f;

	// Token: 0x040000F7 RID: 247
	public int m_beamOutTimer;

	// Token: 0x040000F8 RID: 248
	private float m_targetWidth = 600f;

	// Token: 0x040000F9 RID: 249
	private float m_targetHeight = 400f;

	// Token: 0x040000FA RID: 250
	private Vector2 m_targetOffset = new Vector2(0f, 35f);

	// Token: 0x040000FB RID: 251
	private bool m_dontBlink;

	// Token: 0x040000FC RID: 252
	private Material[] m_materialInstances;

	// Token: 0x040000FD RID: 253
	public AlienEffects m_alienEffects;

	// Token: 0x040000FE RID: 254
	public List<Transform> m_bones;

	// Token: 0x040000FF RID: 255
	private string m_hatIdentifier = string.Empty;

	// Token: 0x04000100 RID: 256
	private bool m_beamInitialized;

	// Token: 0x04000101 RID: 257
	private TransformC m_beamFxTC;

	// Token: 0x04000102 RID: 258
	private bool m_hasHelmet = true;

	// Token: 0x04000103 RID: 259
	private float m_nextElectrifiedEffect;
}
