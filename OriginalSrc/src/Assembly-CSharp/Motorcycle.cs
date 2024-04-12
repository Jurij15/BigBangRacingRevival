using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class Motorcycle : Vehicle, ISpeedBoost
{
	// Token: 0x060002A3 RID: 675 RVA: 0x00023E5C File Offset: 0x0002225C
	public Motorcycle()
		: base(new GraphNode(GraphNodeType.Normal))
	{
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x00023FDC File Offset: 0x000223DC
	public Motorcycle(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.CalculateUpgrades();
		this.m_minigame = LevelManager.m_currentLevel as Minigame;
		this.m_tireBrush = new AutoGeometryBrush(1.7f, false, 0.25f, 0f);
		Motorcycle.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.MotorcyclePrefab_GameObject);
		Motorcycle.m_debrisPrefab = ResourceManager.GetGameObject(RESOURCE.NutsBoltsSpringsPrefab_GameObject);
		base.DisableCollidersRenderer(Motorcycle.m_mainPrefab.transform);
		base.m_graphElement.m_isCopyable = false;
		base.m_graphElement.m_isRemovable = false;
		base.m_graphElement.m_isRotateable = false;
		float num = 0.45f;
		_graphElement.m_width = 130f * num;
		_graphElement.m_height = 50f * num;
		this.flipMult = ((!base.m_graphElement.m_flipped) ? 1 : (-1));
		this.m_group = base.GetGroup();
		Vector3 vector;
		vector..ctor(0f, 5f, 0f);
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		this.m_chassisTC = transformC;
		TransformS.SetTransform(transformC, _graphElement.m_position - this.m_centerOfGravityOffset + vector, _graphElement.m_rotation);
		this.m_camTargetTC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_camTargetTC, _graphElement.m_position - new Vector3(1f, 17f) + vector, _graphElement.m_rotation);
		TransformS.ParentComponent(this.m_camTargetTC, this.m_chassisTC);
		ucpPolyShape[] array = ChipmunkProS.GeneratePolyShapesFromChildren(Motorcycle.m_mainPrefab.transform.Find("DirtBike/DirtBikeColliders").gameObject, this.m_centerOfGravityOffset, 67f, 0.2f, 0.9f, (ucpCollisionType)3, 17895696U, false, false);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].group = this.m_group;
		}
		this.m_chassisBody = ChipmunkProS.AddDynamicBody(transformC, array, this.m_unitC);
		this.m_chassisPrefab = PrefabS.AddComponent(transformC, this.m_centerOfGravityOffset + new Vector3(0f, -3f, 0f), Motorcycle.m_mainPrefab.transform.Find("DirtBike/Chassis").gameObject);
		this.m_chassisDebrisParts = new string[] { "FrontFender", "Light", "Feet" };
		base.CheckVisualUpgrade("Power", 2);
		this.m_chassisBottomTC = TransformS.AddComponent(this.m_entity);
		TransformS.SetPosition(this.m_chassisBottomTC, transformC.transform.position + new Vector3((float)this.flipMult, -7f, 0f));
		TransformS.ParentComponent(this.m_chassisBottomTC, this.m_chassisTC);
		ChipmunkProS.SetBodyLinearDamp(this.m_chassisBody, new Vector2(this.UNIT_LINEAR_DAMP.x, this.UNIT_LINEAR_DAMP.y));
		ChipmunkProS.SetBodyAngularDamp(this.m_chassisBody, 0.97f);
		this.m_gearTC = TransformS.AddComponent(this.m_entity, "Gear");
		Vector3 vector2 = this.m_centerOfGravityOffset + this.m_chassisTC.transform.position;
		TransformS.SetTransform(this.m_gearTC, vector2, _graphElement.m_rotation);
		this.m_gearTransform = this.m_chassisPrefab.p_gameObject.transform.Find("Gear");
		float num2 = 34f * num;
		float num3 = 38f * num;
		Vector2 vector3 = this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(_graphElement.m_width * 0.54f * (float)this.flipMult, -_graphElement.m_height * 0.8f - num2 * 0.5f);
		this.m_frontWheelBody = this.CreateTire(this.m_entity, this.m_chassisBody, vector3, num2, 7f, true);
		this.m_frontWheelBody.customComponent = this.m_unitC;
		TransformC transformC2 = TransformS.AddComponent(this.m_entity, "FrontWheelJoint1", vector3 + new Vector2(-6.5f, 18.5f));
		TransformC transformC3 = TransformS.AddComponent(this.m_entity, "FrontWheelJoint2", vector3);
		TransformC transformC4 = TransformS.AddComponent(this.m_entity, "FrontWheelJoint3", vector3);
		ChipmunkProS.AddGrooveJoint(this.m_chassisBody, this.m_frontWheelBody, transformC2, transformC3, transformC4);
		transformC2 = TransformS.AddComponent(this.m_entity, "FrontWheelSpringAnchor1", vector3);
		transformC3 = TransformS.AddComponent(this.m_entity, "FrontWheelSpringAnchor2", vector3);
		this.m_frontWheelSpring = ChipmunkProS.AddDampedSpring(this.m_chassisBody, this.m_frontWheelBody, transformC2, transformC3, 0f, this.m_frontSpringForce, this.m_frontSpringDamp);
		int num4 = 2;
		this.m_suspensionUpgradeTier = "T" + num4;
		while (Motorcycle.m_mainPrefab.transform.Find("Parts/SuspensionFront" + this.m_suspensionUpgradeTier) == null)
		{
			if (num4 == 0)
			{
				break;
			}
			num4--;
			this.m_suspensionUpgradeTier = "T" + num4;
		}
		int num5 = 0;
		this.m_powerUpgradeTier = "T" + num5;
		while (Motorcycle.m_mainPrefab.transform.Find("DirtBike/Chassis/Power" + this.m_powerUpgradeTier + "/Exhaust" + this.m_powerUpgradeTier) == null)
		{
			if (num5 == 0)
			{
				break;
			}
			num5--;
			this.m_powerUpgradeTier = "T" + num5;
		}
		this.m_ftSuspenserTC = TransformS.AddComponent(this.m_entity, "FtSuspenser");
		TransformS.SetTransform(this.m_ftSuspenserTC, vector3, Vector3.forward * 22.5f);
		PrefabS.AddComponent(this.m_ftSuspenserTC, new Vector3(0f, 8.5f, 0f), Motorcycle.m_mainPrefab.transform.Find("Parts/SuspensionFront" + this.m_suspensionUpgradeTier).gameObject);
		this.m_ftSuspenserTC.transform.GetChild(0).name = "SuspensionFront" + this.m_suspensionUpgradeTier;
		Vector2 vector4 = this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(-_graphElement.m_width * 0.5f * (float)this.flipMult, -_graphElement.m_height * 0.7f - num3 * 0.5f);
		this.m_rearWheelBody = this.CreateTire(this.m_entity, this.m_chassisBody, vector4, num3, 7f, false);
		this.m_rearWheelBody.customComponent = this.m_unitC;
		transformC2 = TransformS.AddComponent(this.m_entity, "RearWheelJoint1", this.m_gearTC.transform.position);
		transformC3 = TransformS.AddComponent(this.m_entity, "RearWheelJoint2", vector4);
		ChipmunkProS.AddPinJoint(this.m_chassisBody, this.m_rearWheelBody, transformC2, transformC3);
		TransformS.ParentComponent(transformC2, this.m_chassisTC);
		TransformS.ParentComponent(transformC3, this.m_chassisTC);
		transformC2 = TransformS.AddComponent(this.m_entity, "RearWheelSpringAnchor1", vector4);
		transformC3 = TransformS.AddComponent(this.m_entity, "RearWheelSpringAnchor2", vector4);
		this.m_rearWheelSpring = ChipmunkProS.AddDampedSpring(this.m_chassisBody, this.m_rearWheelBody, transformC2, transformC3, 0f, this.m_rearSpringForce, this.m_rearSpringDamp);
		Vector2 vector5 = this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(2f, -8f);
		this.m_rtSuspenserTC = TransformS.AddComponent(this.m_entity, "RtSuspenser");
		TransformS.SetTransform(this.m_rtSuspenserTC, vector5, Vector3.forward * Vector3.Angle(this.m_chassisTC.transform.up, vector4 - vector5));
		PrefabS.AddComponent(this.m_rtSuspenserTC, Vector3.zero, Motorcycle.m_mainPrefab.transform.Find("Parts/SuspensionRear" + this.m_suspensionUpgradeTier).gameObject);
		this.m_rtSuspenserTC.transform.GetChild(0).name = "SuspensionRear" + this.m_suspensionUpgradeTier;
		float num6 = 1f;
		if (base.m_graphElement.m_flipped)
		{
			num6 = -1f;
		}
		this.m_rtBottomTC = TransformS.AddComponent(this.m_entity);
		TransformS.SetPosition(this.m_rtBottomTC, this.m_rearWheelBody.TC.transform.position + new Vector3(-num6 * num3 * 0.35f, -num3 * 0.33f, 0f));
		TransformS.ParentComponent(this.m_rtBottomTC, this.m_chassisTC);
		TransformS.ParentComponent(this.m_ftSuspenserTC, this.m_chassisTC);
		TransformS.ParentComponent(this.m_rtBottomTC, this.m_chassisTC);
		TransformS.ParentComponent(this.m_rtSuspenserTC, this.m_chassisTC);
		this.InitMotors(this.m_rearWheelBody, this.m_frontWheelBody);
		this.SetMotorParameters(this.m_frontTireForces, this.m_tireRate, this.m_rearTireForces, this.m_tireRate);
		this.m_minigame.SetPlayer(this, this.m_chassisTC, typeof(MotorcycleController));
		if (!this.m_minigame.m_editing)
		{
			base.SetEngineSound(this.m_chassisTC, "/InGame/Vehicles/DirtBikeEngine", 300);
			this.m_minigame.SetPlayer(this, this.m_chassisTC, typeof(MotorcycleController));
			Hashtable upgradeValues = this.GetUpgradeValues();
			PsGameModeBase gameMode = PsState.m_activeGameLoop.m_gameMode;
			gameMode.CreateRecordingGhost(base.GetType().ToString(), upgradeValues);
			if (gameMode.m_recordingGhost != null)
			{
				gameMode.m_recordingGhost.AddNode("chassis", this.m_chassisPrefab.p_gameObject.transform);
				gameMode.m_recordingGhost.AddNode("rearWheel", this.m_rearWheelBody.TC.transform);
				gameMode.m_recordingGhost.AddNode("frontWheel", this.m_frontWheelBody.TC.transform);
				Debug.LogWarning("CREATED RECORDING GHOST");
			}
			ChipmunkProS.AddCollisionHandler(this.m_frontWheelBody, new CollisionDelegate(this.TireCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)2, true, true, true);
			ChipmunkProS.AddCollisionHandler(this.m_frontWheelBody, new CollisionDelegate(this.TireCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)4, true, true, true);
			ChipmunkProS.AddCollisionHandler(this.m_rearWheelBody, new CollisionDelegate(this.TireCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)2, true, true, true);
			ChipmunkProS.AddCollisionHandler(this.m_rearWheelBody, new CollisionDelegate(this.TireCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)4, true, true, true);
			ChipmunkProS.AddCollisionHandler(this.m_chassisBody, new CollisionDelegate(this.ChassisCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)2, true, true, true);
			ChipmunkProS.AddCollisionHandler(this.m_chassisBody, new CollisionDelegate(this.ChassisCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)4, true, true, true);
			PsCustomisationData vehicleCustomisationData = PsCustomisationManager.GetVehicleCustomisationData(PsState.GetCurrentVehicleType(true));
			PsCustomisationItem installedItemByCategory = vehicleCustomisationData.GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.TRAIL);
			if (installedItemByCategory != null && !string.IsNullOrEmpty(installedItemByCategory.m_identifier))
			{
				GameObject trailPrefabByIdentifier = PsCustomisationManager.GetTrailPrefabByIdentifier(installedItemByCategory.m_identifier);
				if (trailPrefabByIdentifier != null)
				{
					this.m_trail = Object.Instantiate<GameObject>(trailPrefabByIdentifier);
					Vector3 position = this.m_trail.transform.position;
					this.m_trail.transform.parent = this.m_chassisTC.transform;
					this.m_trail.transform.localPosition = position;
					this.m_trailBase = this.m_trail.gameObject.GetComponent<PsTrailBase>();
					this.m_trailBase.Init();
					this.m_trailBase.SetBoostParticleStartSpeed(0);
				}
			}
		}
		else
		{
			this.m_minigame.SetPlayerNode(this);
		}
		int num7 = 1280;
		this.m_playerShadow = ProjectorS.AddComponent(this.m_chassisTC, ResourceManager.GetMaterial(RESOURCE.IngameShadow_Material), num7, new Vector3(0f, 40f));
		this.CreateEditorTouchArea(_graphElement.m_width, _graphElement.m_height, null, default(Vector2));
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.m_checkForCrushing = true;
		this.m_crushTolerance = 4000;
		this.m_canElectrify = true;
		this.m_canBurn = true;
		this.m_canFreeze = true;
		DebugDraw.defaultColor = new Color(1f, 1f, 1f, 1f);
		this.m_airTimeSoundTriggered = true;
		if (base.m_graphElement.m_flipped)
		{
			TransformS.SetScale(this.m_chassisTC, new Vector3(-1f, 1f, 1f));
		}
		Vector3 vector6 = Motorcycle.m_mainPrefab.transform.Find("DirtBike/CharacterLocator").localPosition + this.m_centerOfGravityOffset + new Vector3(0f, -3f, 0f);
		this.m_alienCharacter = new AlienCharacter(_graphElement, "DriveMoto", "AlienNewPrefab_GameObject");
		TransformS.SetGlobalPosition(this.m_alienCharacter.m_mainTC, this.m_chassisTC.transform.position + vector6);
		TransformS.ParentComponent(this.m_alienCharacter.m_mainTC, this.m_chassisTC);
		base.health = 2;
		this.m_alienCharacter.SetHat();
		Vector2 vector7 = this.m_chassisTC.transform.position + new Vector2(10f, 38f);
		Vector2 vector8;
		vector8..ctor(0f, 10f);
		TransformC transformC5 = TransformS.AddComponent(this.m_entity, "Alien");
		TransformS.SetTransform(transformC5, vector7, Vector2.zero);
		Transform transform = ToolBox.SearchHierarchyForBone(this.m_alienCharacter.m_mainPC.p_gameObject.transform, "HeadCollider");
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(transform.gameObject, vector8, 5f, 0.2f, 0.5f, (ucpCollisionType)3, 17895696U, true, false);
		ucpPolyShape.group = this.m_group;
		this.m_alienHead = ChipmunkProS.AddDynamicBody(transformC5, ucpPolyShape, this.m_unitC);
		this.m_boneModifier = transformC5.transform.gameObject.AddComponent<BoneModifier>();
		this.m_boneModifier.AddChipmunkModifier(this.m_alienHead, ToolBox.SearchHierarchyForBone(this.m_alienCharacter.m_mainPC.p_gameObject.transform, "Spine2"), 0.5f, this.m_chassisTC.transform);
		this.m_boneModifier.AddChipmunkModifier(this.m_alienHead, ToolBox.SearchHierarchyForBone(this.m_alienCharacter.m_mainPC.p_gameObject.transform, "Neck"), 0.5f, this.m_chassisTC.transform);
		this.m_boneModifier.AddChipmunkModifier(this.m_alienHead, ToolBox.SearchHierarchyForBone(this.m_alienCharacter.m_mainPC.p_gameObject.transform, "Head"), 0.5f, this.m_chassisTC.transform);
		transformC2 = TransformS.AddComponent(this.m_entity, "AlienJoint1", vector7 + new Vector2(-13f, -9f));
		ChipmunkProS.AddPivotJoint(this.m_chassisBody, this.m_alienHead, transformC2);
		ChipmunkProS.AddRotaryLimitJoint(this.m_chassisBody, this.m_alienHead, transformC2, -0.2617994f, 0.6981317f);
		this.m_headJoint = ChipmunkProS.AddDampedRotarySpring(this.m_chassisBody, this.m_alienHead, transformC2, 0f, 2500000f, 45000f);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_alienHead, new CollisionDelegate(this.AlienCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)2, true, false, true);
			ChipmunkProS.AddCollisionHandler(this.m_alienHead, new CollisionDelegate(this.AlienCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)4, true, false, true);
			this.m_alienCharacter.CreateCamTarget(this.m_camTargetTC);
			CameraS.m_cameraTargetLimits.b = (float)(-(float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight) * 0.5f;
			base.MoveUnitRandomly(0.1f);
		}
		base.m_graphElement.m_isCopyable = false;
		base.m_graphElement.m_isRemovable = false;
		base.m_graphElement.m_isRotateable = false;
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x000251AB File Offset: 0x000235AB
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Weapon, 17f);
		this.SetBaseArmor(DamageType.Electric, 20f);
		this.SetBaseArmor(DamageType.Fire, 20f);
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x000251D8 File Offset: 0x000235D8
	public override void Update()
	{
		if (!PsState.m_activeMinigame.m_gameStarted && !PsState.m_activeMinigame.m_gameEnded && !PsState.m_activeMinigame.m_gamePaused && (!(Main.m_currentGame.m_currentScene.GetCurrentState() is GameBeginHeatState) || GamePlayState.m_canStart))
		{
			List<string> list = new List<string>();
			list.Add("BoostButton");
			if (Controller.GetAnyButton(list))
			{
				(PsIngameMenu.m_controller as MotorcycleController).FadeTutorials();
				PsState.m_activeGameLoop.StartMinigame();
				this.m_alienCharacter.ResetCameraTarget();
				this.m_lastGroundContact = Main.m_resettingGameTime;
			}
		}
		if (this.m_headHitTimer > 0)
		{
			this.m_headHitTimer--;
		}
		base.Update();
		if (!this.m_minigame.m_editing)
		{
			if (Input.GetKey(304) && Input.GetKeyDown(114))
			{
				this.Kill(DamageType.Impact, float.MaxValue);
			}
			if (!this.m_tiresBlown && this.m_entity != null)
			{
				TransformS.SetGlobalTransform(this.m_ftSuspenserTC, this.m_frontWheelBody.TC.transform.position, this.m_chassisTC.transform.rotation.eulerAngles + Vector3.forward * 22.5f);
				TransformS.SetGlobalRotation(this.m_rtSuspenserTC, Vector3.forward * (Vector3.Angle(this.m_chassisTC.transform.up, this.m_rearWheelBody.TC.transform.position - this.m_rtSuspenserTC.transform.position) + this.m_chassisTC.transform.rotation.eulerAngles.z));
				if (this.m_gearTransform != null)
				{
					this.m_gearTransform.rotation = this.m_rearWheelBody.TC.transform.rotation;
				}
			}
			if (!this.m_isDead && !this.m_hasBrokenDown)
			{
				bool flag = Controller.GetButtonState("BoostButton") == ControllerButtonState.ON;
				if (flag && !this.m_lastframeBoostPressed)
				{
					if (this.m_powerUp != null)
					{
						if (this.m_powerUp.Use())
						{
							this.m_powerUp = null;
						}
					}
					else if (this.m_booster != null)
					{
						this.m_booster.Use(this);
					}
				}
				this.m_lastframeBoostPressed = flag;
				if (PsState.m_activeMinigame.m_gameStarted)
				{
					ChipmunkProWrapper.ucpBodyResetForces(this.m_chassisBody.body);
					int num = 0;
					bool flag2 = false;
					bool flag3 = false;
					int num2 = 0;
					bool flag4 = this.m_contactState == ContactState.OnAir;
					bool flag5 = base.GetContactState(this.m_rearWheelBody) == ContactState.OnContact;
					bool flag6 = base.GetContactState(this.m_frontWheelBody) == ContactState.OnContact;
					bool flag7 = !flag6 && !flag5;
					bool flag8 = flag6 && flag5;
					if (flag4)
					{
						this.m_airTime++;
					}
					else
					{
						this.m_airTime = 0;
						this.m_airTimeSoundTriggered = false;
					}
					bool flag9 = Controller.GetButtonState("Throttle") == ControllerButtonState.ON;
					bool flag10 = Controller.GetButtonState("Reverse") == ControllerButtonState.ON;
					float num3 = 1f;
					if (this.m_minigame.m_gameStarted)
					{
						flag2 = ((!base.m_graphElement.m_flipped) ? (Controller.GetButtonState("LeftButton1") == ControllerButtonState.ON) : (Controller.GetButtonState("LeftButton2") == ControllerButtonState.ON));
						flag3 = ((!base.m_graphElement.m_flipped) ? (Controller.GetButtonState("LeftButton2") == ControllerButtonState.ON) : (Controller.GetButtonState("LeftButton1") == ControllerButtonState.ON));
						if (this.m_looseGripTimer > 0)
						{
							if (!flag10)
							{
								num3 = 0.1f;
							}
							this.m_looseGripTimer--;
						}
						if (flag2)
						{
							num2 = -1;
						}
						else if (flag3)
						{
							num2 = 1;
						}
					}
					if (this.m_contactState == ContactState.OnAir)
					{
						if (flag9 && this.m_affectingBlackHoles != null && this.m_affectingBlackHoles.Count > 0)
						{
							ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, this.m_chassisTC.transform.right * 50f, this.m_chassisTC.transform.position);
						}
						float num4 = ChipmunkProWrapper.ucpBodyGetAngle(this.m_chassisBody.body);
						float num5 = 6.2831855f;
						float num6 = ((this.m_minigame.m_gravityMultipler <= 0) ? 3.1415927f : 0f);
						if (this.m_canGiveBooster)
						{
							float num7 = Mathf.Abs(this.m_boosterReferenceAngle - num4);
							if (ToolBox.IsBetween(Mathf.Abs(this.m_boosterReferenceAngle - this.m_boosterBaseAngle), 3.1415927f - this.m_trickBoostAngle, 3.1415927f + this.m_trickBoostAngle))
							{
								if (num7 > this.m_trickBoostAngle)
								{
									this.TrickBoost(1, false);
									if (flag2)
									{
										PsAchievementManager.IncrementProgress("hundredBackFlips", 1);
									}
									else
									{
										PsAchievementManager.IncrementProgress("fiveHundredFrontFlips", 1);
									}
									this.m_canGiveBooster = false;
								}
							}
							else if (Mathf.Abs(num4 - this.m_boosterBaseAngle) >= 3.1415927f)
							{
								this.TrickBoost(1, false);
								if (flag2)
								{
									PsAchievementManager.IncrementProgress("hundredBackFlips", 1);
								}
								else
								{
									PsAchievementManager.IncrementProgress("fiveHundredFrontFlips", 1);
								}
								this.m_canGiveBooster = false;
							}
						}
						else if (Mathf.Abs(num4 % num5 * num5) < this.m_trickBoostAngle)
						{
							this.m_canGiveBooster = true;
							this.m_boosterReferenceAngle = (float)Mathf.RoundToInt(num4 / num5) * num5;
							this.m_boosterBaseAngle = this.m_boosterReferenceAngle;
						}
					}
					if (num2 != this.m_lastLeanDir && flag8 && this.m_leanJumpTimer == 0)
					{
						this.m_leanJumpTimer = 4;
					}
					if (num2 != this.m_lastLeanDir)
					{
						GhostEventType ghostEventType;
						if (num2 == 1)
						{
							ghostEventType = GhostEventType.LeanFront;
						}
						else if (num2 == -1)
						{
							ghostEventType = GhostEventType.LeanBack;
						}
						else
						{
							ghostEventType = GhostEventType.LeanCenter;
						}
						PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(ghostEventType, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
					}
					this.m_lastLeanDir = num2;
					if (base.m_graphElement.m_flipped)
					{
						if (flag9 && !this.m_gasWasPressed)
						{
							SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/BrakePressed", Vector3.zero, "Load", this.m_currentLoad, 1f);
						}
						if (flag10 && !this.m_brakeWasPressed)
						{
							SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/GasPressed_DirtBike", Vector3.zero, "Load", this.m_currentLoad, 1f);
						}
					}
					else
					{
						if (flag9 && !this.m_gasWasPressed)
						{
							SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/GasPressed_DirtBike", Vector3.zero, "Load", this.m_currentLoad, 1f);
						}
						if (flag10 && !this.m_brakeWasPressed)
						{
							SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/BrakePressed", Vector3.zero, "Load", this.m_currentLoad, 1f);
						}
					}
					if (flag2)
					{
						num = -1;
					}
					else if (flag3)
					{
						num = 1;
					}
					if (num != this.oldPressedDir)
					{
					}
					ucpSegmentQueryInfo ucpSegmentQueryInfo = default(ucpSegmentQueryInfo);
					ucpShapeFilter ucpShapeFilter = default(ucpShapeFilter);
					ucpShapeFilter.ucpShapeFilterAll();
					ucpShapeFilter.group = this.m_group;
					ChipmunkProWrapper.ucpSpaceSegmentQueryFirst(this.m_chassisTC.transform.position, this.m_chassisTC.transform.position + new Vector2(0f, (float)(-200 * this.m_minigame.m_gravityMultipler)), 1f, ucpShapeFilter, ref ucpSegmentQueryInfo);
					if (this.m_airTime > 20 && !this.m_airTimeSoundTriggered && ucpSegmentQueryInfo.alpha > 0.5f)
					{
						int num8 = Random.Range(0, 2);
						if (num8 == 0)
						{
							SoundS.PlaySingleShot("/InGame/JumpCheer", Vector3.zero, 1f);
						}
						else
						{
							SoundS.PlaySingleShot("/InGame/BigAirJump", Vector3.zero, 1f);
						}
						this.m_alienCharacter.AnimateCharacterRandom("JumpCheer", num8);
						this.m_airTimeSoundTriggered = true;
						PsState.m_cameraManTakeShot = true;
					}
					if (this.m_minigame.m_gameStarted && !GameLevelPreview.m_levelPreviewRunning)
					{
						CameraTargetC camTarget = this.m_alienCharacter.m_camTarget;
						if (ucpSegmentQueryInfo.alpha == 1f)
						{
							this.camTargetYOffset = -100f * (float)this.m_minigame.m_gravityMultipler;
						}
						else
						{
							this.camTargetYOffset = (-100f + (1f - ucpSegmentQueryInfo.alpha) * 170f) * (float)this.m_minigame.m_gravityMultipler;
						}
						CameraTargetC cameraTargetC = camTarget;
						cameraTargetC.offset.y = cameraTargetC.offset.y + (this.camTargetYOffset - camTarget.offset.y) * 0.1f;
					}
					this.m_brakeWasPressed = flag10;
					this.m_gasWasPressed = flag9;
					this.oldPressedDir = num;
					Vector2 velocityRelativeToContacts = base.GetVelocityRelativeToContacts(this.m_rearWheelBody);
					bool flag11 = base.GetRogueContactsCount(this.m_rearWheelBody, false) > 0;
					float num9 = 1f - ToolBox.limitBetween(Mathf.Abs(velocityRelativeToContacts.x) / 600f, 0f, 0.6f);
					float num10 = this.m_tireRate;
					Vector2 vector = this.m_frontTireForces;
					bool flag12 = false;
					bool flag13 = false;
					if (!flag7)
					{
						if (flag11 && !flag9 && !flag10 && velocityRelativeToContacts.magnitude < 80f)
						{
							float num11 = 1f - ToolBox.getPositionBetween(velocityRelativeToContacts.magnitude, 0f, 80f);
							num10 = 0f;
							num9 = 0.4f * num11;
							vector = this.m_rearTireForces * num9 * 0.66f;
							flag12 = true;
						}
						float num12 = ChipmunkProWrapper.ucpBodyGetAngle(this.m_chassisBody.body) % 6.2831855f;
						if (num12 < 0f)
						{
							num12 += 6.2831855f;
						}
						float num13 = Vector2.Angle(Vector2.right, velocityRelativeToContacts) * 0.017453292f;
						if (velocityRelativeToContacts.y < 0f)
						{
							num13 = 6.2831855f - num13;
						}
						float num14 = 1.5707964f;
						bool flag14 = num12 < num13 + num14 && num12 > num13 - num14;
						if (flag10)
						{
							num10 = this.m_tireRate / 2f;
							if (flag14 && ChipmunkProWrapper.ucpBodyGetAngVel(this.m_frontWheelBody.body) < 0f)
							{
								this.SetFrontBrake(true, 1000000f);
								flag13 = true;
							}
							else
							{
								this.SetFrontBrake(false, 1000000f);
							}
						}
						else if (flag9)
						{
							if (flag14 && ChipmunkProWrapper.ucpBodyGetAngVel(this.m_frontWheelBody.body) > 0f)
							{
								this.SetFrontBrake(true, 1000000f);
								flag13 = true;
							}
							else
							{
								this.SetFrontBrake(false, 1000000f);
							}
						}
						else
						{
							this.SetFrontBrake(false, 1000000f);
						}
					}
					this.m_boostEffectFrontTireTC.forcedRotation = Quaternion.Euler(Vector3.forward * this.m_chassisTC.transform.eulerAngles.z);
					this.m_boostEffectRearTireTC.forcedRotation = Quaternion.Euler(Vector3.forward * this.m_chassisTC.transform.eulerAngles.z);
					this.m_boostEffectFrontTireTC.updateRotation = true;
					this.m_boostEffectRearTireTC.updateRotation = true;
					this.SetMotorParameters(vector, num10, this.m_rearTireForces * num9, num10);
					int num15 = ((!flag10 || velocityRelativeToContacts.x >= 10f) ? 1 : (-1));
					this.m_alienCharacter.AnimateCharacter("DriveDir", num15);
					if (this.m_lastDriveDir != num15)
					{
						GhostEventType ghostEventType2 = ((num15 != 1) ? GhostEventType.DriveDirBackward : GhostEventType.DriveDirForward);
						PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(ghostEventType2, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
					}
					this.m_lastDriveDir = num15;
					if (flag9 && !flag10)
					{
						if (flag2)
						{
							this.m_gasAmount = ToolBox.limitBetween(this.m_gasAmount + 0.3f, 0f, 1f);
						}
						else
						{
							this.m_gasAmount = ToolBox.limitBetween(this.m_gasAmount + 0.055f, 0f, 1f);
						}
					}
					else if (flag10 && !flag9)
					{
						this.m_gasAmount = ToolBox.limitBetween(this.m_gasAmount - 0.12f, -1f, 0f);
					}
					else
					{
						this.m_gasAmount *= 0.9f;
					}
					if (!flag12)
					{
						float num16 = Mathf.Abs(ChipmunkProWrapper.ucpBodyGetAngVel(this.m_rearMotor.bodyB.body));
						if (num16 / num10 > Mathf.Abs(this.m_gasAmount))
						{
							this.m_gasAmount = num16 / num10 * ((!flag10 || flag9) ? 1f : (-1f));
						}
						if (num16 > num10 && !flag13)
						{
							this.SetMotorParameters(Vector2.zero, num10, Vector2.zero, num10);
						}
						this.UpdateMotors((!flag9 && !flag10) ? 0f : (this.m_gasAmount * this.m_trickBoostForce), !base.m_graphElement.m_flipped, this.m_trickBoostForce * num3, true);
					}
					else
					{
						this.UpdateMotors(1f, !base.m_graphElement.m_flipped, 1f, true);
					}
					Vector2 vector2 = ChipmunkProWrapper.ucpBodyGetPos(this.m_frontWheelBody.body);
					Vector2 vector3 = ChipmunkProWrapper.ucpBodyGetPos(this.m_rearWheelBody.body);
					if (flag2 || flag3)
					{
						this.m_alienCharacter.AnimateCharacter("LeanDir", (!flag2) ? 1 : (-1));
						if (this.m_boneModifier != null)
						{
							this.m_boneModifier.m_globalWeight = Mathf.Lerp(this.m_boneModifier.m_globalWeight, 0f, 0.08f);
						}
						if (flag2)
						{
							float num17 = Mathf.Lerp(ChipmunkProWrapper.ucpDampedRotarySpringGetRestAngle(this.m_headJoint.constraint), -0.61086524f, 0.08f);
							ChipmunkProWrapper.ucpDampedRotarySpringSetRestAngle(this.m_headJoint.constraint, num17);
						}
						else
						{
							float num18 = Mathf.Lerp(ChipmunkProWrapper.ucpDampedRotarySpringGetRestAngle(this.m_headJoint.constraint), 0.17453292f, 0.08f);
							ChipmunkProWrapper.ucpDampedRotarySpringSetRestAngle(this.m_headJoint.constraint, 0.17453292f);
						}
						if (flag2 && !this.m_leaningBackward && !flag3)
						{
							this.m_leaningBackward = true;
							this.m_leaningForward = false;
							this.m_leaningCenter = false;
							if (flag6)
							{
								ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, this.m_chassisTC.transform.up * 4000f, vector2);
								ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, this.m_chassisTC.transform.up * -1000f, vector3);
							}
						}
						else if (flag3 && !this.m_leaningForward && !flag2)
						{
							this.m_leaningBackward = false;
							this.m_leaningForward = true;
							this.m_leaningCenter = false;
						}
						if (this.m_contactState == ContactState.OnContact)
						{
							float magnitude = ChipmunkProWrapper.ucpBodyGetVel(this.m_chassisBody.body).magnitude;
							float num19 = ChipmunkProWrapper.ucpBodyGetAngVel(this.m_chassisBody.body);
							ucpSegmentQueryInfo ucpSegmentQueryInfo2 = default(ucpSegmentQueryInfo);
							ucpSegmentQueryInfo ucpSegmentQueryInfo3 = default(ucpSegmentQueryInfo);
							ucpShapeFilter.ucpShapeFilterAll();
							ucpShapeFilter.group = this.m_group;
							ChipmunkProWrapper.ucpSpaceSegmentQueryFirst(vector2, vector2 + this.m_chassisTC.transform.up * -100f, 1f, ucpShapeFilter, ref ucpSegmentQueryInfo2);
							ChipmunkProWrapper.ucpSpaceSegmentQueryFirst(vector3, vector3 + this.m_chassisTC.transform.up * -100f, 1f, ucpShapeFilter, ref ucpSegmentQueryInfo3);
							float num20 = 1f - ucpSegmentQueryInfo2.alpha * ucpSegmentQueryInfo2.alpha;
							float num21 = 1f - ucpSegmentQueryInfo3.alpha * ucpSegmentQueryInfo3.alpha;
							if (flag3 && flag5 && !flag6)
							{
								if (num19 > 0f)
								{
									num19 *= Math.Min(120f / magnitude, 1f);
								}
								ChipmunkProWrapper.ucpBodySetAngVel(this.m_chassisBody.body, num19 - Math.Min(200f / magnitude, 0.5f));
								ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, this.m_chassisTC.transform.up * -600f * num20 * Math.Min(120f / magnitude, 1f), vector3);
							}
							else if (flag2 && flag5 && !flag6)
							{
								if (num19 < 0f && !flag10)
								{
									num19 *= Math.Min(120f / magnitude, 1f);
								}
								ChipmunkProWrapper.ucpBodySetAngVel(this.m_chassisBody.body, num19 + Math.Min(magnitude / 3000f, 0.3f));
							}
							else if (flag2 && !flag5 && !flag6)
							{
								ChipmunkProWrapper.ucpBodySetAngVel(this.m_chassisBody.body, num19 + 0.3f);
							}
							if (this.m_leaningForward && flag6 && flag5)
							{
								ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, this.m_chassisTC.transform.up * -500f * num21, vector2);
								ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, this.m_chassisTC.transform.up * -100f * num20, vector3);
							}
							if (this.m_leaningBackward && flag6)
							{
								ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, this.m_chassisTC.transform.up * -300f * num20, vector3);
							}
							if (this.m_chassisHasPersistentContact)
							{
								if (this.m_leaningForward)
								{
									this.TurnOver(1f);
								}
								if (this.m_leaningBackward)
								{
									this.TurnOver(-1f);
								}
							}
							if (flag5 && !flag6)
							{
								ChipmunkProWrapper.ucpBodySetAngVel(this.m_chassisBody.body, Mathf.Clamp(ChipmunkProWrapper.ucpBodyGetAngVel(this.m_chassisBody.body), -1.8f, 1.8f));
							}
							else if (!flag5 && flag6)
							{
								ChipmunkProWrapper.ucpBodySetAngVel(this.m_chassisBody.body, Mathf.Clamp(ChipmunkProWrapper.ucpBodyGetAngVel(this.m_chassisBody.body), -1.8f, 4f));
							}
							else if (!flag5 && !flag6)
							{
								ChipmunkProWrapper.ucpBodySetAngVel(this.m_chassisBody.body, Mathf.Clamp(ChipmunkProWrapper.ucpBodyGetAngVel(this.m_chassisBody.body), -5.5f, 5.5f));
							}
						}
						if (this.m_turnOver)
						{
							float num22 = (this.m_turnOverStarted + 0.5f - Main.m_resettingGameTime) * 2f;
							if (num22 > 0f)
							{
								ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, this.m_chassisTC.transform.up * -500f * this.m_turnOverDir * num22, vector2);
								ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, this.m_chassisTC.transform.up * 500f * this.m_turnOverDir * num22, vector3);
							}
							else
							{
								this.m_turnOver = false;
							}
						}
						if (velocityRelativeToContacts.magnitude < 60f && ((flag2 && flag6) || (flag3 && flag5)) && Mathf.Abs(Mathf.DeltaAngle(this.m_chassisTC.transform.rotation.eulerAngles.z, 0f)) > 75f)
						{
							Vector2 vector4 = this.m_chassisTC.transform.up;
							float num23 = ((!flag3) ? ((!flag2) ? 0f : (-1.45f)) : 1f);
							ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, vector4 * -150f * num23, vector2);
							ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, vector4 * 150f * num23, vector3);
						}
						if (this.m_contactState == ContactState.OnAir || this.m_groundedChainIndex == -1)
						{
							float num24 = ChipmunkProWrapper.ucpBodyGetAngVel(this.m_chassisBody.body);
							if (flag2 && !flag3)
							{
								if (num24 < 0f)
								{
									num24 = 0f;
									ChipmunkProWrapper.ucpBodySetAngVel(this.m_chassisBody.body, num24);
								}
								num24 += 0.2f;
							}
							else if (flag3 && !flag2)
							{
								if (num24 > 0f)
								{
									num24 = 0f;
									ChipmunkProWrapper.ucpBodySetAngVel(this.m_chassisBody.body, num24);
								}
								num24 -= 0.2f;
							}
							num24 = ToolBox.limitBetween(num24, -5.5f, 5.5f);
							if (base.Contact(CMBIdentifiers.Airflow))
							{
								Vector2 vector5 = ChipmunkProWrapper.ucpBodyGetVel(this.m_chassisBody.body);
								if ((num24 < 0f && vector5.x < 350f) || (num24 > 0f && vector5.x > -350f))
								{
									ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, Vector2.right * 15f * -num24, this.m_chassisTC.transform.position);
								}
							}
							ChipmunkProWrapper.ucpBodySetAngVel(this.m_chassisBody.body, num24);
						}
					}
					else if (!this.m_leaningCenter)
					{
						this.m_leaningBackward = false;
						this.m_leaningForward = false;
						this.m_alienCharacter.AnimateCharacter("LeanDir", 0);
						if (this.m_boneModifier != null)
						{
							this.m_boneModifier.m_globalWeight = Mathf.Lerp(this.m_boneModifier.m_globalWeight, 1f, 0.08f);
						}
						float num25 = Mathf.Lerp(ChipmunkProWrapper.ucpDampedRotarySpringGetRestAngle(this.m_headJoint.constraint), 0f, 0.08f);
						ChipmunkProWrapper.ucpDampedRotarySpringSetRestAngle(this.m_headJoint.constraint, num25);
						if (num25 == 0f && this.m_boneModifier != null && this.m_boneModifier.m_globalWeight == 1f)
						{
							this.m_leaningCenter = true;
						}
					}
					bool flag15 = false;
					if (this.m_destructibleGroundContacts > 0 && (!flag9 || !flag10) && (flag9 || flag10))
					{
						flag15 = true;
						this.m_skiddingEndTimer = 15;
						if (Main.m_gameTicks % 2 == 0)
						{
							this.SkidTire(this.m_rtBottomTC, this.m_rearWheelBody);
						}
					}
					if (this.m_skiddingEndTimer > 0)
					{
						this.m_skiddingEndTimer--;
					}
					if (flag7 && this.m_drivingFx != null)
					{
						base.PauseDriveFx();
					}
					else if (this.m_drivingFx != null)
					{
						ParticleSystem component = this.m_drivingFx.p_gameObject.GetComponent<ParticleSystem>();
						if (this.m_drivingFxName.Equals("MudSplatter_GameObject"))
						{
							if (!flag15 && this.m_skiddingEndTimer == 0 && component.isPlaying)
							{
								component.Stop();
							}
							else if (flag15 && component.isStopped)
							{
								component.Play();
							}
						}
						else
						{
							float num26 = ToolBox.getPositionBetween(this.m_currentRPM, 500f, 5000f) * 30f;
							component.emissionRate = num26;
						}
					}
					if (this.m_engineSound != null)
					{
						float num27 = Mathf.Abs(ChipmunkProWrapper.ucpBodyGetAngVel(this.m_rearWheelBody.body));
						float positionBetween = ToolBox.getPositionBetween(num27, 0f, 50f);
						float num28 = ((!flag9 && !flag10) ? 300f : Mathf.Lerp(300f, 6000f, positionBetween));
						float num29 = (float)((!flag7) ? 1 : 0);
						base.UpdateEngineSound(num28, num29);
					}
					if (this.m_tireRollSound != null)
					{
						if (flag7)
						{
							base.MuteTireRollSound();
						}
						else
						{
							base.UpdateTireRollSound();
						}
					}
					if (this.m_trickBoosting || this.m_resourceBoosting)
					{
						if ((this.m_gasWasPressed || this.m_brakeWasPressed) && flag5)
						{
							float num30 = 1f;
							if (this.m_brakeWasPressed)
							{
								num30 = -1f;
							}
							PsState.m_activeMinigame.m_playerUnit.AddSpeed(this.m_chassisTC.transform.right * this.m_trickBoostForce * num30, 1000f);
							this.m_trickBoostTicks--;
							if (this.m_gasWasPressed && !this.m_rightBoostEventTriggered)
							{
								PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.BoostRightStart, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
								this.m_rightBoostEventTriggered = true;
							}
							if (this.m_brakeWasPressed && !this.m_leftBoostEventTriggered)
							{
								PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.BoostLeftStart, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
								this.m_leftBoostEventTriggered = true;
							}
							base.SetBoostSound(this.m_chassisTC);
							this.m_boostEffectFrontTire.ActivateBoost((!this.m_gasWasPressed || !this.m_brakeWasPressed) ? ((!this.m_gasWasPressed) ? EffectBoost.BoostDirection.Left : EffectBoost.BoostDirection.Right) : EffectBoost.BoostDirection.Both);
							this.m_boostEffectRearTire.ActivateBoost((!this.m_gasWasPressed || !this.m_brakeWasPressed) ? ((!this.m_gasWasPressed) ? EffectBoost.BoostDirection.Left : EffectBoost.BoostDirection.Right) : EffectBoost.BoostDirection.Both);
							if (this.m_trailBase != null)
							{
								this.m_trailBase.SetBoostActive(true);
							}
						}
						else
						{
							if (this.m_rightBoostEventTriggered)
							{
								PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.BoostRightEnd, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
							}
							if (this.m_leftBoostEventTriggered)
							{
								PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.BoostLeftEnd, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
							}
							this.m_rightBoostEventTriggered = (this.m_leftBoostEventTriggered = false);
							this.m_boostEffectFrontTire.IdleBoost();
							this.m_boostEffectRearTire.IdleBoost();
							if (this.m_trailBase != null)
							{
								this.m_trailBase.SetBoostActive(false);
							}
						}
						if (this.m_trickBoostTicks <= 0)
						{
							float num31 = Mathf.Lerp(this.m_suspensionValues[0], this.m_suspensionValues[this.m_suspensionValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
							float num32 = 10400f * this.m_suspensionMultiplier * -1f;
							float num33 = 9600f * this.m_suspensionMultiplier * -1f;
							float num34 = Mathf.Lerp(this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
							float num35 = Mathf.Lerp(this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
							this.m_tireRate = Mathf.Lerp(this.m_tireRateValues[0], this.m_tireRateValues[this.m_tireRateValues.Length - 1], this.m_powerNormalizedUpgradeValue);
							ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_frontWheelSpring.constraint, num32);
							ChipmunkProWrapper.ucpDampedSpringSetDamping(this.m_frontWheelSpring.constraint, num34);
							ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_rearWheelSpring.constraint, num33);
							ChipmunkProWrapper.ucpDampedSpringSetDamping(this.m_rearWheelSpring.constraint, num35);
							float num36 = 2.25f * Mathf.Lerp(this.m_gripValues[0], this.m_gripValues[this.m_gripValues.Length - 1], this.m_gripNormalizedUpgradeValue);
							ChipmunkProWrapper.ucpShapeSetFriction(this.m_frontWheelShape.shapePtr, num36);
							ChipmunkProWrapper.ucpShapeSetFriction(this.m_rearWheelShape.shapePtr, num36);
							base.RemoveBoostSound();
							this.m_boostEffectFrontTire.DropBoost();
							this.m_boostEffectRearTire.DropBoost();
							PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.BoostEnd, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
							if (this.m_trailBase != null)
							{
								this.m_trailBase.SetBoostActive(false);
							}
							this.m_trickBoosting = (this.m_resourceBoosting = false);
						}
					}
					else
					{
						this.m_trickBoostForce = 1f;
					}
					if (this.m_trickBoostTicks <= 0 && (this.m_boostEffectFrontTire.SomethingActive() || this.m_boostEffectRearTire.SomethingActive()))
					{
						this.DropBoostEffects();
					}
				}
			}
			else if (this.m_trickBoosting || this.m_resourceBoosting || (this.m_trickBoostTicks <= 0 && (this.m_boostEffectFrontTire.SomethingActive() || this.m_boostEffectRearTire.SomethingActive())))
			{
				this.DropBoostEffects();
			}
		}
		this.m_frontWheelSeparated = false;
		this.m_chassisHasPersistentContact = false;
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x00026FE8 File Offset: 0x000253E8
	public void DropBoostEffects()
	{
		base.RemoveBoostSound();
		this.m_boostEffectFrontTire.DropBoost();
		this.m_boostEffectRearTire.DropBoost();
		this.m_trickBoosting = (this.m_resourceBoosting = false);
		if (this.m_trailBase != null)
		{
			this.m_trailBase.SetBoostActive(false);
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0002703E File Offset: 0x0002543E
	private void TurnOver(float _dir)
	{
		this.m_turnOverDir = _dir;
		if (!this.m_turnOver)
		{
			this.m_turnOver = true;
			this.m_turnOverStarted = Main.m_resettingGameTime;
		}
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x00027064 File Offset: 0x00025464
	public void Skid(AutoGeometryBrush _brush, Vector2 _pos)
	{
		_pos += new Vector2(16f, 16f);
		foreach (AutoGeometryLayer autoGeometryLayer in AutoGeometryManager.m_layers)
		{
			if (autoGeometryLayer.m_groundC.m_ground.m_skiddable)
			{
				autoGeometryLayer.PaintWithBrush(_brush, _pos, AGDrawMode.SUB, true, ref autoGeometryLayer.m_bytes, true, false);
			}
		}
	}

	// Token: 0x060002AA RID: 682 RVA: 0x000270F8 File Offset: 0x000254F8
	public bool SkidTire(TransformC _tc, ChipmunkBodyC _body)
	{
		float num = ChipmunkProWrapper.ucpBodyGetAngVel(_body.body);
		Vector2 vector = _tc.transform.position;
		vector += new Vector2(16f, 16f);
		foreach (AutoGeometryLayer autoGeometryLayer in AutoGeometryManager.m_layers)
		{
			if (autoGeometryLayer.m_groundC.m_ground.m_skiddable)
			{
				autoGeometryLayer.PaintWithBrush(this.m_tireBrush, vector, AGDrawMode.SUB, true, ref autoGeometryLayer.m_bytes, true, false);
			}
		}
		return true;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x000271AC File Offset: 0x000255AC
	public ChipmunkBodyC CreateTire(Entity _e, ChipmunkBodyC _chassis, Vector2 _pos, float _rad, float _weight, bool _front)
	{
		TransformC transformC = TransformS.AddComponent(_e, "Bike Tire");
		TransformC transformC2 = TransformS.AddComponent(_e, "Effect");
		transformC2.forceRotation = true;
		TransformS.ParentComponent(transformC2, transformC, Vector3.zero);
		TransformS.SetTransform(transformC, _pos, Vector2.zero);
		ucpCircleShape ucpCircleShape = new ucpCircleShape(_rad, Vector2.zero, 17895696U, _weight, 0.15f, this.m_tireGrip, (ucpCollisionType)3, false);
		ucpCircleShape.group = this.m_group;
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, ucpCircleShape, null);
		if (_front)
		{
			PrefabC prefabC = PrefabS.AddComponent(transformC2, Vector3.forward * -0.8f, ResourceManager.GetGameObject(RESOURCE.EffectBoostMotorcycleFront_GameObject), "BoostEffect", true, true);
			this.m_boostEffectFrontTire = prefabC.p_gameObject.GetComponent<EffectBoost>();
			this.m_boostEffectFrontTireTC = transformC2;
		}
		else
		{
			PrefabC prefabC2 = PrefabS.AddComponent(transformC2, Vector3.forward * -0.8f, ResourceManager.GetGameObject(RESOURCE.EffectBoostMotorcycleBack_GameObject), "BoostEffect", true, true);
			this.m_boostEffectRearTire = prefabC2.p_gameObject.GetComponent<EffectBoost>();
			this.m_boostEffectRearTireTC = transformC2;
		}
		int num = 1;
		string text = "T" + num;
		while (Motorcycle.m_mainPrefab.transform.Find("Parts/BikeTireFront" + text) == null)
		{
			num--;
			text = "T" + num;
			if (num == 0)
			{
				break;
			}
		}
		if (_front)
		{
			PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 0f), Motorcycle.m_mainPrefab.transform.Find("Parts/BikeTireFront" + text).gameObject);
			this.m_frontWheelShape = ucpCircleShape;
		}
		else
		{
			PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 0f), Motorcycle.m_mainPrefab.transform.Find("Parts/BikeTireBack" + text).gameObject);
			this.m_rearWheelShape = ucpCircleShape;
		}
		ChipmunkProS.SetBodyLinearDamp(chipmunkBodyC, new Vector2(this.UNIT_LINEAR_DAMP.x, this.UNIT_LINEAR_DAMP.y));
		ChipmunkProS.SetBodyAngularDamp(chipmunkBodyC, 0.997f);
		TransformS.ParentComponent(transformC, this.m_chassisTC);
		return chipmunkBodyC;
	}

	// Token: 0x060002AC RID: 684 RVA: 0x000273F0 File Offset: 0x000257F0
	public void TrickBoost(int _tricks, bool _resourceBoost = false)
	{
		bool flag = true;
		int num = 60;
		this.m_trickBoostForce = 1.5f;
		if (!_resourceBoost)
		{
			this.m_trickBoostForce += base.GetUpgradeEfficiency(base.GetType(), PsUpgradeManager.UpgradeType.FLIP_BOOST_POWER);
			num += (int)base.GetUpgradeEfficiency(base.GetType(), PsUpgradeManager.UpgradeType.FLIP_BOOST_DURATION);
			if (this.m_trickBoostTicks >= num + 15)
			{
				flag = false;
			}
			else
			{
				this.m_trickBoostTicks = Mathf.Min(this.m_trickBoostTicks + num, num + 15);
			}
		}
		else
		{
			this.m_trickBoostForce += base.GetUpgradeEfficiency(base.GetType(), PsUpgradeManager.UpgradeType.NITRO_BOOST_POWER);
			num += (int)base.GetUpgradeEfficiency(base.GetType(), PsUpgradeManager.UpgradeType.NITRO_BOOST_DURATION);
			this.m_trickBoostTicks += num;
		}
		Debug.LogWarning("Boost duration: " + this.m_trickBoostTicks);
		Debug.LogWarning("Boost power: " + this.m_trickBoostForce);
		this.m_resourceBoosting = _resourceBoost;
		this.m_trickBoosting = this.m_trickBoosting || !_resourceBoost;
		Debug.LogWarning("TRICK BOOST x ");
		if (flag)
		{
			SoundS.PlaySingleShot("/Ingame/Vehicles/BoosterActivate", Vector2.zero, 1f);
			PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.BoostStart, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
			this.m_boostEffectFrontTire.GainBoost();
			this.m_boostEffectRearTire.GainBoost();
		}
		float num2 = Mathf.Lerp(this.m_suspensionValues[0], this.m_suspensionValues[this.m_suspensionValues.Length - 1], 1f);
		float num3 = 10400f * this.m_suspensionMultiplier * -1f;
		float num4 = 9600f * this.m_suspensionMultiplier * -1f;
		float num5 = Mathf.Lerp(this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1], 1f);
		float num6 = Mathf.Lerp(this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1], 1f);
		float num7 = 2.25f * Mathf.Lerp(this.m_gripValues[0], this.m_gripValues[this.m_gripValues.Length - 1], 1f);
		this.m_tireRate = Mathf.Lerp(this.m_tireRateValues[0], this.m_tireRateValues[this.m_tireRateValues.Length - 1], 1f) + 4f;
		ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_frontWheelSpring.constraint, num3);
		ChipmunkProWrapper.ucpDampedSpringSetDamping(this.m_frontWheelSpring.constraint, num5);
		ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_rearWheelSpring.constraint, num4);
		ChipmunkProWrapper.ucpDampedSpringSetDamping(this.m_rearWheelSpring.constraint, num6);
		ChipmunkProWrapper.ucpShapeSetFriction(this.m_frontWheelShape.shapePtr, num7);
		ChipmunkProWrapper.ucpShapeSetFriction(this.m_rearWheelShape.shapePtr, num7);
		this.m_boostEffectFrontTire.IdleBoost();
		this.m_boostEffectRearTire.IdleBoost();
		if (this.m_trailBase)
		{
			this.m_trailBase.SetBoostActive(false);
		}
		if (this.d_boostTriggeredDelegate != null)
		{
			this.d_boostTriggeredDelegate.Invoke();
			this.d_boostTriggeredDelegate = null;
		}
	}

	// Token: 0x060002AD RID: 685 RVA: 0x00027708 File Offset: 0x00025B08
	public override void UnitContactEnd(ContactInfo _contactInfo)
	{
		base.UnitContactEnd(_contactInfo);
		if (this.m_isDead || this.m_hasBrokenDown)
		{
			return;
		}
		this.m_boosterReferenceAngle = ChipmunkProWrapper.ucpBodyGetAngle(this.m_chassisBody.body);
		this.m_boosterBaseAngle = (float)Mathf.RoundToInt(this.m_boosterReferenceAngle / 6.2831855f) * 3.1415927f * 2f;
		this.m_canGiveBooster = true;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x00027774 File Offset: 0x00025B74
	public override void UnitContactStart(ContactInfo _contactInfo)
	{
		base.UnitContactStart(_contactInfo);
		if (this.m_isDead || this.m_hasBrokenDown)
		{
			return;
		}
	}

	// Token: 0x060002AF RID: 687 RVA: 0x00027794 File Offset: 0x00025B94
	public override void GroundContactStart(ContactInfo _contactInfo)
	{
		base.GroundContactStart(_contactInfo);
		if (this.m_isDead || this.m_hasBrokenDown)
		{
			return;
		}
		Ground ground = _contactInfo.m_ground;
		if (_contactInfo.m_contactBody.body == this.m_rearWheelBody.body)
		{
			base.SetTireRollSound(this.m_chassisTC, ground.m_tireRollSound);
			bool flag = true;
			if (ground.m_driveFX != null && this.m_drivingFx != null && ground.m_driveFX.Equals(this.m_drivingFxName))
			{
				flag = false;
			}
			if (flag)
			{
				base.RemoveDriveFx();
			}
			base.StartDriveFx(this.m_chassisTC, ground.m_skiddable, ground.m_driveFX);
			if (ground.m_skiddable)
			{
				this.m_destructibleGroundContacts++;
			}
		}
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00027868 File Offset: 0x00025C68
	public override void GroundContactEnd(ContactInfo _contactInfo)
	{
		base.GroundContactEnd(_contactInfo);
		if (this.m_isDead || this.m_hasBrokenDown)
		{
			return;
		}
		this.m_boosterReferenceAngle = ChipmunkProWrapper.ucpBodyGetAngle(this.m_chassisBody.body);
		this.m_boosterBaseAngle = (float)Mathf.RoundToInt(this.m_boosterReferenceAngle / 6.2831855f) * 3.1415927f * 2f;
		this.m_canGiveBooster = true;
		Ground ground = _contactInfo.m_ground;
		if (_contactInfo.m_contactBody.body == this.m_rearWheelBody.body)
		{
			if (ground.m_skiddable)
			{
				this.m_destructibleGroundContacts--;
			}
			Ground ground2 = null;
			for (int i = this.m_contacts.Count - 1; i > -1; i--)
			{
				if (this.m_contacts[i].m_ground != ground)
				{
					ground2 = this.m_contacts[i].m_ground;
					break;
				}
			}
			base.MuteTireRollSound();
			base.PauseDriveFx();
			if (ground2 != null)
			{
				if (ground2.m_tireRollSound != null)
				{
					base.SetTireRollSound(this.m_chassisTC, ground2.m_tireRollSound);
				}
				if (ground2.m_driveFX != null)
				{
					if (!ground2.m_driveFX.Equals(this.m_drivingFxName))
					{
						base.RemoveDriveFx();
					}
					base.StartDriveFx(this.m_chassisTC, ground2.m_skiddable, ground2.m_driveFX);
				}
			}
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x000279D0 File Offset: 0x00025DD0
	private void TireCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (this.m_isDead || this.m_hasBrokenDown)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin && Mathf.Abs(_pair.impulse.normalized.y) > 0.5f)
		{
			float positionBetween = ToolBox.getPositionBetween(_pair.impulse.magnitude, 1600f, 10000f);
			if (positionBetween > 0f)
			{
				SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/TireImpact_DirtBike", Vector3.zero, "Force", positionBetween, 1f);
			}
		}
		if (_phase != ucpCollisionPhase.Separate)
		{
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexA];
			if (chipmunkBodyC == this.m_rearWheelBody)
			{
				this.m_rearTireSurfaceNormal = _pair.normal;
			}
			else if (chipmunkBodyC == this.m_frontWheelBody)
			{
				this.m_frontWheelSeparated = true;
			}
		}
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x00027AAC File Offset: 0x00025EAC
	private void ChassisCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (this.m_isDead || this.m_hasBrokenDown)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin)
		{
			float positionBetween = ToolBox.getPositionBetween(_pair.impulse.magnitude, 2500f, 10000f);
			if (positionBetween > 0f)
			{
				SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/ChassisImpact", Vector3.zero, "Force", positionBetween, 1f);
				if (positionBetween > 0.8f)
				{
					base.CreateRandomImpactDebris(this.m_chassisDebrisParts, Motorcycle.m_debrisPrefab);
				}
			}
		}
		if (_phase == ucpCollisionPhase.Persist)
		{
			this.m_chassisHasPersistentContact = true;
			if (Mathf.Abs(Mathf.DeltaAngle(this.m_chassisTC.transform.rotation.eulerAngles.z, 0f)) > 60f && !ChipmunkProWrapper.ucpShapeGetSensor(_pair.shapeB))
			{
				ChipmunkBodyShape bodyShapeByTag = ChipmunkProS.GetBodyShapeByTag(this.m_chassisBody, "DirtBikeColliderRear");
				if (_pair.shapeA == bodyShapeByTag.shapePtr && this.m_looseGripTimer < 50)
				{
					this.m_looseGripTimer += 5;
				}
			}
		}
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x00027BD0 File Offset: 0x00025FD0
	private void AlienCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		int identifier = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB].m_identifier;
		if (_phase == ucpCollisionPhase.Begin && this.m_headHitTimer == 0 && identifier != 2 && identifier != 3)
		{
			float positionBetween = ToolBox.getPositionBetween(_pair.impulse.magnitude, 1600f, 6000f);
			if (positionBetween > 0f)
			{
				SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/AlienHeadImpact", Vector3.zero, "Force", positionBetween, 1f);
				this.m_alienCharacter.AnimateCharacter("Hit");
			}
			float positionBetween2 = ToolBox.getPositionBetween(_pair.impulse.magnitude, 3600f, 10000f);
			if (positionBetween2 > 0f)
			{
				EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.HeadHit_GameObject), new Vector3(_pair.point.x, _pair.point.y, 0f), Vector3.zero, 2f, "GTAG_AUTODESTROY", null);
				this.m_headHitTimer = 15;
				if (!ChipmunkProWrapper.ucpShapeGetSensor(_pair.shapeB))
				{
					if (base.DecreaseHealth(1))
					{
						if (this.m_entity.m_active)
						{
							this.Kill(DamageType.Impact, float.MaxValue);
						}
						else
						{
							this.m_emergencyKillAction = delegate
							{
								this.Kill(DamageType.Impact, float.MaxValue);
							};
							this.m_lateEmergencyKill = true;
						}
						this.DetachHeadwear();
					}
					else if (base.health == 1)
					{
						this.DetachHeadwear();
					}
				}
			}
		}
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x00027D50 File Offset: 0x00026150
	public void DetachHeadwear()
	{
		PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.HatDetached, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
		Vector2 vector = Vector2.zero;
		if (this.m_alienHead != null)
		{
			vector += ChipmunkProWrapper.ucpBodyGetVel(this.m_alienHead.body);
		}
		if (this.m_alienCharacter != null)
		{
			this.m_alienCharacter.ThrowHelmet(vector, 0f, false, 17895696U, false, true);
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00027DC8 File Offset: 0x000261C8
	public override void EmergencyKill()
	{
		if (this.m_entity != null && !this.m_hasBrokenDown)
		{
			this.m_emergencyKillAction = delegate
			{
				this.m_killScaleTime = false;
				Debug.Log("EMERGENCY KILL MOTORCYCLE", null);
				this.ExplosionEffect(this.m_chassisTC.transform.position);
				if (PsState.m_lastDeathReason == DeathReason.EJECT)
				{
					Vector2 vector = this.m_chassisTC.transform.position;
					PsS.ApplyBlastWaveToGround(vector, 95f);
				}
				this.Kill(DamageType.Weapon, float.MaxValue);
				Debris.CreateDebrisFromChildren(this.m_chassisPrefab.p_gameObject.transform, this.m_lastFrameVelocity * 0.7f, new Vector2(2000f, 2000f), 2000f, true, this.m_group);
			};
			if (this.m_entity.m_active)
			{
				this.m_emergencyKillAction.Invoke();
			}
			else
			{
				this.m_lateEmergencyKill = true;
			}
		}
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00027E24 File Offset: 0x00026224
	public void DestroyBoneModifier()
	{
		if (this.m_boneModifier != null)
		{
			Object.DestroyImmediate(this.m_boneModifier);
			this.m_boneModifier = null;
		}
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00027E4C File Offset: 0x0002624C
	public override void LaunchRagdoll(bool _explodeBones = false)
	{
		TransformS.UnparentComponent(this.m_alienCharacter.m_mainTC, true);
		this.DetachHeadwear();
		Vector2 vector = this.m_lastFrameVelocity * 0.7f;
		if (vector.y < 0f)
		{
			vector.y *= 0.5f;
		}
		this.m_alienCharacter.ConstructRagdoll(false);
		this.m_alienCharacter.AddLinearVelocityToRagDoll(this.m_alienCharacter.m_ragdoll, vector + this.m_chassisTC.transform.up * 200f, 60f);
		this.DestroyBoneModifier();
		if (this.m_alienHead != null && this.m_alienHead.body != IntPtr.Zero)
		{
			ChipmunkProS.RemoveBody(this.m_alienHead);
		}
		this.m_alienHead = null;
		ChipmunkBodyShape bodyShapeByTag = ChipmunkProS.GetBodyShapeByTag(this.m_chassisBody, "DirtBikeColliderMiddle");
		ChipmunkProWrapper.ucpShapeSetGroup(bodyShapeByTag.shapePtr, this.m_alienCharacter.m_group);
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00027F58 File Offset: 0x00026358
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		PsState.SetDeathReasonByDamagetype(_damageType);
		if (!this.m_hasBrokenDown)
		{
			this.m_hasBrokenDown = true;
			this.UpdateMotors(0f, true, 1f, false);
			if (this.m_booster != null)
			{
				this.m_booster = null;
			}
			base.RemoveEngineSound();
			base.RemoveTireRollSound();
			base.RemoveDriveFx();
			ChipmunkProWrapper.ucpBodyResetForces(this.m_chassisBody.body);
			if (_damageType != DamageType.BlackHole)
			{
				if (!this.m_minigame.m_gameEnded && this.m_killScaleTime)
				{
					PsState.m_activeMinigame.TweenTimeScale(0.05f, TweenStyle.ExpoIn, 0.1f, delegate
					{
						PsState.m_activeMinigame.TweenTimeScale(1f, TweenStyle.ExpoIn, 0.25f, null, 0f);
					}, 0.5f);
				}
				this.m_alienCharacter.AnimateCharacter("Death");
				this.m_alienCharacter.m_teleported = this.m_teleported;
				this.LaunchRagdoll(false);
			}
			SoundS.PlaySingleShot("/Ingame/Characters/AlienDisappointment", Vector3.zero, 1f);
			if (_damageType == DamageType.Weapon)
			{
				SoundS.PlaySingleShot("/Ingame/Characters/AlienDeathScream", Vector3.zero, 1f);
			}
			else if (_damageType == DamageType.BlackHole)
			{
				SoundS.PlaySingleShot("/Ingame/Units/BlackHoleSuck_Alien", Vector3.zero, 1f);
			}
			if (!this.m_minigame.m_playerReachedGoal)
			{
				PsState.m_activeGameLoop.LoseMinigame();
			}
		}
		if (!this.m_tiresBlown && _damageType == DamageType.Weapon)
		{
			if (!this.m_minigame.m_gameEnded && this.m_killScaleTime)
			{
				PsState.m_activeMinigame.TweenTimeScale(0.05f, TweenStyle.ExpoIn, 0.1f, delegate
				{
					PsState.m_activeMinigame.TweenTimeScale(1f, TweenStyle.ExpoIn, 0.25f, null, 0f);
				}, 0.5f);
			}
			SoundS.PlaySingleShot("/InGame/Vehicles/OffroadCarDeath", Vector3.zero, 1f);
			ChipmunkProS.RemoveBody(this.m_chassisBody);
			this.m_chassisBody = null;
			ProjectorS.RemoveComponent(this.m_playerShadow);
			uint group = this.m_alienCharacter.m_group;
			Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("FrontFender"), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, group, false, null, Vector3.zero, -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("Light"), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, group, false, null, Vector3.zero, -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("FrontFork"), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, group, false, null, Vector3.zero, -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("Gear"), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, group, false, null, Vector3.zero, -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("GasTank"), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, group, false, "EngineBreakdown_GameObject", new Vector3(0f, 0f, 10f), -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("Seat"), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, group, false, null, Vector3.zero, -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("Feet"), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, group, false, null, Vector3.zero, -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("Power" + this.m_powerUpgradeTier + "/Exhaust" + this.m_powerUpgradeTier), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, group, false, null, Vector3.zero, -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_ftSuspenserTC.transform.GetChild(0), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, group, false, null, Vector3.zero, -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_rtSuspenserTC.transform.GetChild(0), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, group, true, null, Vector3.zero, -1f, 17895696U);
			ChipmunkProWrapper.ucpBodySetVel(this.m_rearWheelBody.body, base.GetRandomDebrisVel());
			ChipmunkProWrapper.ucpBodySetVel(this.m_frontWheelBody.body, base.GetRandomDebrisVel());
			ChipmunkProWrapper.ucpBodySetAngVel(this.m_rearWheelBody.body, base.GetRandomDebrisAngVel());
			ChipmunkProWrapper.ucpBodySetAngVel(this.m_frontWheelBody.body, base.GetRandomDebrisAngVel());
			Vector2 vector = PsState.m_activeMinigame.m_globalGravity * (float)PsState.m_activeMinigame.m_gravityMultipler;
			ChipmunkProWrapper.ucpBodySetGravity(this.m_rearWheelBody.body, vector);
			ChipmunkProWrapper.ucpBodyActivate(this.m_rearWheelBody.body);
			ChipmunkProWrapper.ucpBodySetGravity(this.m_frontWheelBody.body, vector);
			ChipmunkProWrapper.ucpBodyActivate(this.m_frontWheelBody.body);
			this.m_tiresBlown = true;
		}
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00028498 File Offset: 0x00026898
	public override void Destroy()
	{
		if (this.m_booster != null)
		{
			this.m_booster = null;
		}
		if (this.m_trailBase != null)
		{
			this.m_trailBase.Destroy();
			this.m_trailBase = null;
			Object.Destroy(this.m_trail);
			this.m_trail = null;
		}
		if (this.m_minigame != null)
		{
			this.m_minigame.RemovePlayer();
		}
		this.DestroyBoneModifier();
		base.Destroy();
		if (this.m_alienCharacter != null)
		{
			this.m_alienCharacter.Destroy();
		}
		if (this.m_tireBrush != null)
		{
			this.m_tireBrush.Destroy();
		}
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0002853C File Offset: 0x0002693C
	public override List<float> ParseUpgradeValues(Hashtable _upgrades)
	{
		this.ParseMinAndMaxValues();
		float num = ((_upgrades != null) ? Convert.ToSingle(_upgrades["tireForce"]) : 1f);
		float num2 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["tireRate"]) : 65f);
		float num3 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["grip"]) : 1f);
		float num4 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["suspensionForce"]) : (-1f));
		float num5 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["suspensionDamp"]) : 200f);
		float num6 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["COG"]) : 0f);
		float num7 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["handlingForce"]) : 1f);
		List<float> list = new List<float>();
		List<float> list2 = new List<float>();
		List<float> list3 = new List<float>();
		List<float> list4 = new List<float>();
		list.Add(ToolBox.getPositionBetween(num, this.m_tireForceValues[0], this.m_tireForceValues[this.m_tireForceValues.Length - 1]));
		list.Add(ToolBox.getPositionBetween(num2, this.m_tireRateValues[0], this.m_tireRateValues[this.m_tireRateValues.Length - 1]));
		list2.Add(ToolBox.getPositionBetween(num3, this.m_gripValues[0], this.m_gripValues[this.m_gripValues.Length - 1]));
		list3.Add(ToolBox.getPositionBetween(num4, this.m_suspensionValues[0], this.m_suspensionValues[this.m_suspensionValues.Length - 1]));
		list3.Add(ToolBox.getPositionBetween(num5, this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1]));
		list3.Add(ToolBox.getPositionBetween(num6, this.m_COGValues[0], this.m_COGValues[this.m_COGValues.Length - 1]));
		list3.Add(ToolBox.getPositionBetween(num7, this.m_handlingValues[0], this.m_handlingValues[this.m_handlingValues.Length - 1]));
		list4.Add(Enumerable.Max(list));
		list4.Add(Enumerable.Max(list2));
		list4.Add(Enumerable.Max(list3));
		return list4;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00028790 File Offset: 0x00026B90
	public override Hashtable GetUpgradeValues()
	{
		this.CalculateUpgrades();
		Debug.LogWarning("GETTING VEHICLE UPGRADE VALUES");
		Hashtable hashtable = new Hashtable();
		hashtable.Add("tireForce", this.m_tireForceMultiplier);
		hashtable.Add("tireRate", this.m_tireRate);
		hashtable.Add("grip", this.m_tireGrip / 2.25f);
		hashtable.Add("suspensionForce", this.m_suspensionMultiplier);
		hashtable.Add("suspensionDamp", this.m_rearSpringDamp);
		hashtable.Add("COG", this.m_COGAdd);
		hashtable.Add("handlingForce", this.m_handlingForce);
		return hashtable;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00028854 File Offset: 0x00026C54
	public override List<string> GetNodeUpgradeNames()
	{
		List<string> list = new List<string>();
		list.Add("RANGES");
		list.Add("tireForceValues");
		list.Add("tireRateValues");
		list.Add("gripValues");
		list.Add("suspensionValues");
		list.Add("dampValues");
		list.Add("COGValues");
		list.Add("handlingValues");
		return list;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x000288C0 File Offset: 0x00026CC0
	public override List<KeyValuePair<string, int>> GetUpgrades()
	{
		List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
		KeyValuePair<string, int> keyValuePair = new KeyValuePair<string, int>("power", PsMetagameManager.m_playerStats.GetUpgradeValue(base.GetType(), "power", 0));
		list.Add(keyValuePair);
		keyValuePair = new KeyValuePair<string, int>("grip", PsMetagameManager.m_playerStats.GetUpgradeValue(base.GetType(), "grip", 0));
		list.Add(keyValuePair);
		keyValuePair = new KeyValuePair<string, int>("handling", PsMetagameManager.m_playerStats.GetUpgradeValue(base.GetType(), "handling", 0));
		list.Add(keyValuePair);
		return list;
	}

	// Token: 0x060002BE RID: 702 RVA: 0x00028950 File Offset: 0x00026D50
	public void ParseMinAndMaxValues()
	{
		PsUpgradeableEditorItem psUpgradeableEditorItem = PsMetagameData.GetUnlockableByIdentifier(base.GetType().ToString()) as PsUpgradeableEditorItem;
		if (psUpgradeableEditorItem != null)
		{
			Hashtable upgradeValues = psUpgradeableEditorItem.m_upgradeValues;
			this.m_ranges = Array.ConvertAll<object, int>((object[])upgradeValues["RANGES"], new Converter<object, int>(base.ToInt));
			this.m_tireForceValues = Array.ConvertAll<object, float>((object[])upgradeValues["tireForceValues"], new Converter<object, float>(base.ToFloat));
			this.m_tireRateValues = Array.ConvertAll<object, float>((object[])upgradeValues["tireRateValues"], new Converter<object, float>(base.ToFloat));
			this.m_gripValues = Array.ConvertAll<object, float>((object[])upgradeValues["gripValues"], new Converter<object, float>(base.ToFloat));
			this.m_suspensionValues = Array.ConvertAll<object, float>((object[])upgradeValues["suspensionValues"], new Converter<object, float>(base.ToFloat));
			this.m_dampValues = Array.ConvertAll<object, float>((object[])upgradeValues["dampValues"], new Converter<object, float>(base.ToFloat));
			this.m_COGValues = Array.ConvertAll<object, float>((object[])upgradeValues["COGValues"], new Converter<object, float>(base.ToFloat));
			this.m_handlingValues = Array.ConvertAll<object, float>((object[])upgradeValues["handlingValues"], new Converter<object, float>(base.ToFloat));
			this.m_upgradeSteps = psUpgradeableEditorItem.m_upgradeSteps;
		}
	}

	// Token: 0x060002BF RID: 703 RVA: 0x00028AC4 File Offset: 0x00026EC4
	public void CalculateUpgrades()
	{
		this.ParseMinAndMaxValues();
		if (PsState.m_activeGameLoop.m_selectedVehicle == PsSelectedVehicle.CreatorVehicle)
		{
			if (PsState.m_activeGameLoop.m_rentUpgradeValues == null || PsState.m_activeGameLoop.m_rentUpgradeValues.Count == 0)
			{
				PsState.m_activeGameLoop.m_rentUpgradeValues = this.ParseUpgradeValues(PsState.m_activeGameLoop.m_minigameMetaData.creatorUpgrades);
			}
			this.m_powerNormalizedUpgradeValue = PsState.m_activeGameLoop.m_rentUpgradeValues[0];
			this.m_gripNormalizedUpgradeValue = PsState.m_activeGameLoop.m_rentUpgradeValues[1];
			this.m_handlingNormalizedUpgradeValue = PsState.m_activeGameLoop.m_rentUpgradeValues[2];
			Debug.LogWarning("RENT VEHICLE: ");
		}
		else if (PsState.m_activeGameLoop.m_selectedVehicle == PsSelectedVehicle.GhostVehicle && PsState.m_activeGameLoop.m_gameMode is PsGameModeRacing)
		{
			List<float> list = this.ParseUpgradeValues((PsState.m_activeGameLoop.m_gameMode as PsGameModeRacing).m_playbackGhosts[0].m_ghost.m_upgradeValues);
			this.m_powerNormalizedUpgradeValue = list[0];
			this.m_gripNormalizedUpgradeValue = list[1];
			this.m_handlingNormalizedUpgradeValue = list[2];
			Debug.LogWarning("GHOST VEHICLE: ");
		}
		else
		{
			float num = 0f;
			if (PsState.m_activeGameLoop is PsGameLoopAdventureBattle)
			{
				PsGameLoopAdventureBattle psGameLoopAdventureBattle = PsState.m_activeGameLoop as PsGameLoopAdventureBattle;
				if (psGameLoopAdventureBattle.m_purchasedPowerFuels != null)
				{
					for (int i = 0; i < psGameLoopAdventureBattle.m_purchasedPowerFuels.Count; i++)
					{
						num += (float)psGameLoopAdventureBattle.m_purchasedPowerFuels[i];
					}
					num /= 900f;
					Debug.LogError("Power fuel efficiency: " + num);
				}
			}
			this.m_powerNormalizedUpgradeValue = base.GetUpgradeValue(base.GetType(), PsUpgradeManager.UpgradeType.SPEED) + num;
			this.m_gripNormalizedUpgradeValue = base.GetUpgradeValue(base.GetType(), PsUpgradeManager.UpgradeType.GRIP) + num;
			this.m_handlingNormalizedUpgradeValue = base.GetUpgradeValue(base.GetType(), PsUpgradeManager.UpgradeType.HANDLING) + num;
			Debug.LogWarning("OWN VEHICLE: ");
		}
		Debug.LogWarning("CC: Power: " + this.m_powerNormalizedUpgradeValue);
		Debug.LogWarning("CC: Grip: " + this.m_gripNormalizedUpgradeValue);
		Debug.LogWarning("CC: Handling: " + this.m_handlingNormalizedUpgradeValue);
		this.m_tireForceMultiplier = Mathf.Lerp(this.m_tireForceValues[0], this.m_tireForceValues[this.m_tireForceValues.Length - 1], this.m_powerNormalizedUpgradeValue);
		this.m_frontTireForces = Vector3.zero * this.m_tireForceMultiplier;
		this.m_rearTireForces = new Vector2(1250000f, 600000f) * this.m_tireForceMultiplier;
		this.m_tireRate = Mathf.Lerp(this.m_tireRateValues[0], this.m_tireRateValues[this.m_tireRateValues.Length - 1], this.m_powerNormalizedUpgradeValue);
		this.m_tireGrip = 2.25f * Mathf.Lerp(this.m_gripValues[0], this.m_gripValues[this.m_gripValues.Length - 1], this.m_gripNormalizedUpgradeValue);
		this.m_suspensionMultiplier = Mathf.Lerp(this.m_suspensionValues[0], this.m_suspensionValues[this.m_suspensionValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
		this.m_frontSpringForce = 6500f * this.m_suspensionMultiplier * -1f;
		this.m_rearSpringForce = 6500f * this.m_suspensionMultiplier * -1f;
		this.m_frontSpringDamp = Mathf.Lerp(this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
		this.m_rearSpringDamp = Mathf.Lerp(this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
		this.m_COGAdd = Mathf.Lerp(this.m_COGValues[0], this.m_COGValues[this.m_COGValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
		this.m_centerOfGravityOffset = new Vector2(1f, 17f + this.m_COGAdd);
		this.m_handlingForce = Mathf.Lerp(this.m_handlingValues[0], this.m_handlingValues[this.m_handlingValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
	}

	// Token: 0x040002E6 RID: 742
	public float[] m_tireForceValues = new float[] { 0.7f, 1f, 1.2f, 1.275f, 1.3f };

	// Token: 0x040002E7 RID: 743
	public float[] m_tireRateValues = new float[] { 50f, 60f, 65f, 68f, 70f };

	// Token: 0x040002E8 RID: 744
	public float[] m_suspensionValues = new float[] { -1.2f, -1f, -0.9f, -0.87f, -0.85f };

	// Token: 0x040002E9 RID: 745
	public float[] m_dampValues = new float[] { 180f, 200f, 210f, 216f, 220f };

	// Token: 0x040002EA RID: 746
	public float[] m_handlingValues = new float[] { 0.9f, 1.05f, 1.15f, 1.21f, 1.25f };

	// Token: 0x040002EB RID: 747
	public float[] m_gripValues = new float[] { 0.7f, 1.01f, 1.15f, 1.25f, 1.3f };

	// Token: 0x040002EC RID: 748
	public float[] m_COGValues = new float[] { -4.75f, -1.75f, 0f, 1f, 1.25f };

	// Token: 0x040002ED RID: 749
	public int[] m_ranges = new int[] { 0, 5, 10, 15, 20 };

	// Token: 0x040002EE RID: 750
	public float m_powerNormalizedUpgradeValue;

	// Token: 0x040002EF RID: 751
	public float m_handlingNormalizedUpgradeValue;

	// Token: 0x040002F0 RID: 752
	public float m_gripNormalizedUpgradeValue;

	// Token: 0x040002F1 RID: 753
	private float m_tireRate = 65f;

	// Token: 0x040002F2 RID: 754
	private float m_frontSpringForce = 6500f;

	// Token: 0x040002F3 RID: 755
	private float m_frontSpringDamp = 200f;

	// Token: 0x040002F4 RID: 756
	private float m_rearSpringForce = 6500f;

	// Token: 0x040002F5 RID: 757
	private float m_rearSpringDamp = 200f;

	// Token: 0x040002F6 RID: 758
	private float m_tireGrip = 2.25f;

	// Token: 0x040002F7 RID: 759
	public Vector2 m_frontTireForces = Vector2.zero;

	// Token: 0x040002F8 RID: 760
	public Vector3 m_rearTireForces = new Vector2(1250000f, 600000f);

	// Token: 0x040002F9 RID: 761
	private float m_handlingForce = 0.001f;

	// Token: 0x040002FA RID: 762
	private float m_tireForceMultiplier = 1f;

	// Token: 0x040002FB RID: 763
	private float m_suspensionMultiplier = -1f;

	// Token: 0x040002FC RID: 764
	private float m_COGAdd;

	// Token: 0x040002FD RID: 765
	public Vector2 UNIT_LINEAR_DAMP = new Vector2(0.997f, 0.997f);

	// Token: 0x040002FE RID: 766
	private const float TIRE_ELASTICITY = 0.15f;

	// Token: 0x040002FF RID: 767
	private const float FRONT_WHEEL_RAD = 34f;

	// Token: 0x04000300 RID: 768
	private const float FRONT_WHEEL_WEIGHT = 7f;

	// Token: 0x04000301 RID: 769
	private const float REAR_WHEEL_RAD = 38f;

	// Token: 0x04000302 RID: 770
	private const float REAR_WHEEL_WEIGHT = 7f;

	// Token: 0x04000303 RID: 771
	private const float CHASSIS_WEIGHT = 67f;

	// Token: 0x04000304 RID: 772
	private const float ALIEN_HEAD_WEIGHT = 5f;

	// Token: 0x04000305 RID: 773
	private const float TIRE_ANGULAR_DAMP = 0.997f;

	// Token: 0x04000306 RID: 774
	private const float CHASSIS_ANGULAR_DAMP = 0.97f;

	// Token: 0x04000307 RID: 775
	private const float SELF_BALANCE_FORCE = 40f;

	// Token: 0x04000308 RID: 776
	public uint m_group;

	// Token: 0x04000309 RID: 777
	public ChipmunkBodyC m_frontWheelBody;

	// Token: 0x0400030A RID: 778
	public ChipmunkBodyC m_rearWheelBody;

	// Token: 0x0400030B RID: 779
	public ucpCircleShape m_frontWheelShape;

	// Token: 0x0400030C RID: 780
	public ucpCircleShape m_rearWheelShape;

	// Token: 0x0400030D RID: 781
	public ChipmunkBodyC m_alienHead;

	// Token: 0x0400030E RID: 782
	public ChipmunkConstraintC m_frontWheelSpring;

	// Token: 0x0400030F RID: 783
	public ChipmunkConstraintC m_rearWheelSpring;

	// Token: 0x04000310 RID: 784
	public ChipmunkConstraintC m_balancerSpring;

	// Token: 0x04000311 RID: 785
	public TransformC m_chassisTC;

	// Token: 0x04000312 RID: 786
	public TransformC m_chassisBottomTC;

	// Token: 0x04000313 RID: 787
	public TransformC m_balancerTC;

	// Token: 0x04000314 RID: 788
	public TransformC m_rtBottomTC;

	// Token: 0x04000315 RID: 789
	public TransformC m_gearTC;

	// Token: 0x04000316 RID: 790
	public TransformC m_ftSuspenserTC;

	// Token: 0x04000317 RID: 791
	public TransformC m_rtSuspenserTC;

	// Token: 0x04000318 RID: 792
	public float m_gasAmount;

	// Token: 0x04000319 RID: 793
	public bool m_gasWasPressed;

	// Token: 0x0400031A RID: 794
	public bool m_brakeWasPressed;

	// Token: 0x0400031B RID: 795
	public int m_lastLeanDir;

	// Token: 0x0400031C RID: 796
	public int m_lastDriveDir;

	// Token: 0x0400031D RID: 797
	public int m_leanJumpTimer;

	// Token: 0x0400031E RID: 798
	public BoneModifier m_boneModifier;

	// Token: 0x0400031F RID: 799
	private static GameObject m_mainPrefab;

	// Token: 0x04000320 RID: 800
	private static GameObject m_debrisPrefab;

	// Token: 0x04000321 RID: 801
	public AutoGeometryBrush m_tireBrush;

	// Token: 0x04000322 RID: 802
	public string m_tireRollSoundName;

	// Token: 0x04000323 RID: 803
	private int m_headHitTimer;

	// Token: 0x04000324 RID: 804
	public ProjectorC m_playerShadow;

	// Token: 0x04000325 RID: 805
	public Transform m_gearTransform;

	// Token: 0x04000326 RID: 806
	private bool m_tiresBlown;

	// Token: 0x04000327 RID: 807
	private bool m_rightBoostEventTriggered;

	// Token: 0x04000328 RID: 808
	private bool m_leftBoostEventTriggered;

	// Token: 0x04000329 RID: 809
	private string m_suspensionUpgradeTier;

	// Token: 0x0400032A RID: 810
	private string m_powerUpgradeTier;

	// Token: 0x0400032B RID: 811
	public EffectBoost m_boostEffectFrontTire;

	// Token: 0x0400032C RID: 812
	public EffectBoost m_boostEffectRearTire;

	// Token: 0x0400032D RID: 813
	public TransformC m_boostEffectFrontTireTC;

	// Token: 0x0400032E RID: 814
	public TransformC m_boostEffectRearTireTC;

	// Token: 0x0400032F RID: 815
	private const int MIN_RPM = 300;

	// Token: 0x04000330 RID: 816
	private const int MAX_RPM = 6000;

	// Token: 0x04000331 RID: 817
	public int m_airTime;

	// Token: 0x04000332 RID: 818
	public bool m_airTimeSoundTriggered;

	// Token: 0x04000333 RID: 819
	public int m_destructibleGroundContacts;

	// Token: 0x04000334 RID: 820
	public int m_skiddingEndTimer;

	// Token: 0x04000335 RID: 821
	public bool m_chassisHasPersistentContact;

	// Token: 0x04000336 RID: 822
	public int m_looseGripTimer;

	// Token: 0x04000337 RID: 823
	private int oldPressedDir;

	// Token: 0x04000338 RID: 824
	private float camTargetYOffset;

	// Token: 0x04000339 RID: 825
	public Vector2 m_centerOfGravityOffset;

	// Token: 0x0400033A RID: 826
	private int flipMult;

	// Token: 0x0400033B RID: 827
	private Vector3 suspenserOriginalRot;

	// Token: 0x0400033C RID: 828
	private float suspenserOriginalAngle;

	// Token: 0x0400033D RID: 829
	private ChipmunkConstraintC m_headJoint;

	// Token: 0x0400033E RID: 830
	private float m_boosterReferenceAngle;

	// Token: 0x0400033F RID: 831
	private float m_boosterBaseAngle;

	// Token: 0x04000340 RID: 832
	private float m_trickBoostAngle = 0.7853982f;

	// Token: 0x04000341 RID: 833
	public Motorcycle.LeanDirection m_leanDirection;

	// Token: 0x04000342 RID: 834
	private bool m_leaningForward;

	// Token: 0x04000343 RID: 835
	private bool m_leaningBackward;

	// Token: 0x04000344 RID: 836
	private bool m_leaningCenter;

	// Token: 0x04000345 RID: 837
	private bool m_frontWheelSeparated;

	// Token: 0x04000346 RID: 838
	private bool m_canGiveBooster = true;

	// Token: 0x04000347 RID: 839
	private bool m_lastframeBoostPressed;

	// Token: 0x04000348 RID: 840
	private bool m_turnOver;

	// Token: 0x04000349 RID: 841
	private float m_turnOverDir;

	// Token: 0x0400034A RID: 842
	private float m_turnOverStarted;

	// Token: 0x0400034B RID: 843
	public int m_trickBoostTicks;

	// Token: 0x0400034C RID: 844
	public bool m_trickBoosting;

	// Token: 0x0400034D RID: 845
	public bool m_resourceBoosting;

	// Token: 0x0400034E RID: 846
	public float m_trickBoostForce;

	// Token: 0x0400034F RID: 847
	private Vector2 m_rearTireSurfaceNormal;

	// Token: 0x02000085 RID: 133
	public enum LeanDirection
	{
		// Token: 0x04000353 RID: 851
		None,
		// Token: 0x04000354 RID: 852
		Back,
		// Token: 0x04000355 RID: 853
		Forward
	}
}
