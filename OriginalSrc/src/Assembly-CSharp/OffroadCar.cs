using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class OffroadCar : Vehicle, ISpeedBoost
{
	// Token: 0x060002C5 RID: 709 RVA: 0x00028FE4 File Offset: 0x000273E4
	public OffroadCar()
		: base(new GraphNode(GraphNodeType.Normal))
	{
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0002918C File Offset: 0x0002758C
	public OffroadCar(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.CalculateUpgrades();
		this.m_tireBrush = new AutoGeometryBrush(1.5f, false, 0.25f, 0f);
		this.m_tireBrushSmall = new AutoGeometryBrush(1.5f, false, 0.25f, 0f);
		string text = "OffroadCarPrefab_GameObject";
		this.m_mainPrefab = ResourceManager.GetGameObject(text);
		this.m_debrisPrefab = ResourceManager.GetGameObject(RESOURCE.NutsBoltsSpringsPrefab_GameObject);
		base.DisableCollidersRenderer(this.m_mainPrefab.transform);
		float num = 0.45f;
		_graphElement.m_width = 130f * num;
		_graphElement.m_height = 50f * num;
		int num2 = ((!base.m_graphElement.m_flipped) ? 1 : (-1));
		this.m_group = base.GetGroup();
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		this.m_chassisTC = transformC;
		TransformS.SetTransform(transformC, _graphElement.m_position - this.m_centerOfGravityOffset, _graphElement.m_rotation);
		this.m_camTargetTC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_camTargetTC, _graphElement.m_position - new Vector3(0f, 16f), _graphElement.m_rotation);
		TransformS.ParentComponent(this.m_camTargetTC, this.m_chassisTC);
		this.m_chassisOffset = this.m_centerOfGravityOffset + new Vector2(0f, 2f);
		ucpPolyShape[] array = ChipmunkProS.GeneratePolyShapesFromChildren(this.m_mainPrefab.transform.Find("OffroadCar/OffroadCarCollider").gameObject, this.m_chassisOffset, 100f, 0.2f, 0.5f, (ucpCollisionType)3, 17895696U, false, false);
		foreach (ucpPolyShape ucpPolyShape in array)
		{
			ucpPolyShape.group = this.m_group;
		}
		this.m_chassisBody = ChipmunkProS.AddDynamicBody(transformC, array, this.m_unitC);
		this.m_chassisPrefab = PrefabS.AddComponent(transformC, this.m_chassisOffset, this.m_mainPrefab.transform.Find("OffroadCar/Chassis").gameObject);
		this.m_chassisDebrisParts = new string[] { "Bumper", "Hood", "Motor", "SteeringWheel", "DoorLeft", "DoorRight" };
		base.CheckVisualUpgrade("Handling", 3);
		base.CheckVisualUpgrade("Power", 3);
		this.m_chassisBottomTC = TransformS.AddComponent(this.m_entity);
		TransformS.SetPosition(this.m_chassisBottomTC, transformC.transform.position + new Vector3((float)num2, -7f, 0f));
		TransformS.ParentComponent(this.m_chassisBottomTC, this.m_chassisTC);
		ChipmunkProS.SetBodyLinearDamp(this.m_chassisBody, new Vector2(this.UNIT_LINEAR_DAMP.x, this.UNIT_LINEAR_DAMP.y));
		ChipmunkProS.SetBodyAngularDamp(this.m_chassisBody, 0.97f);
		float num3 = 34f * num;
		float num4 = 41f * num;
		Vector2 vector = this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(_graphElement.m_width * 0.5f * (float)num2, -_graphElement.m_height * 0.58f - num3 * 0.5f);
		this.m_frontWheelBody = this.CreateTire(this.m_entity, this.m_chassisBody, vector, num3, 5f, true);
		this.m_frontWheelBody.customComponent = this.m_unitC;
		this.m_ftSuspensersTC = TransformS.AddComponent(this.m_entity, "FtSuspensers", vector + new Vector3(-7.5f, 17.5f), Vector3.forward * -Vector3.Angle(this.m_chassisTC.transform.up, new Vector3(7.5f, -17.5f)));
		PrefabC prefabC = PrefabS.AddComponent(this.m_ftSuspensersTC, new Vector3(0f, 0f, -17.5f), this.m_mainPrefab.transform.Find("Parts/Suspension").gameObject);
		PrefabC prefabC2 = PrefabS.AddComponent(this.m_ftSuspensersTC, new Vector3(0f, 0f, 17.5f), this.m_mainPrefab.transform.Find("Parts/Suspension").gameObject);
		this.m_ftSuspensersTC.transform.GetChild(0).name = "Suspension";
		this.m_ftSuspensersTC.transform.GetChild(1).name = "Suspension";
		TransformS.ParentComponent(this.m_ftSuspensersTC, this.m_chassisTC);
		TransformC transformC2 = TransformS.AddComponent(this.m_entity, "FrontWheelJoint1", this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(0f, -_graphElement.m_height * 0.5f));
		TransformC transformC3 = TransformS.AddComponent(this.m_entity, "FrontWheelJoint2", vector);
		ChipmunkProS.AddPinJoint(this.m_chassisBody, this.m_frontWheelBody, transformC2, transformC3);
		transformC2 = TransformS.AddComponent(this.m_entity, "FrontWheelSpringAnchor1", vector);
		transformC3 = TransformS.AddComponent(this.m_entity, "FrontWheelSpringAnchor2", vector);
		this.m_frontWheelSpring = ChipmunkProS.AddDampedSpring(this.m_chassisBody, this.m_frontWheelBody, transformC2, transformC3, 0f, this.m_frontSpringForce, this.m_frontSpringDamp);
		Vector2 vector2 = this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(-_graphElement.m_width * 0.55f * (float)num2, -_graphElement.m_height * 0.33f - num4 * 0.5f);
		this.m_rearWheelBody = this.CreateTire(this.m_entity, this.m_chassisBody, vector2, num4, 5f, false);
		this.m_rearWheelBody.customComponent = this.m_unitC;
		this.m_rtSuspensersTC = TransformS.AddComponent(this.m_entity, "RtSuspensers", vector2 + new Vector3(3f, 22f), Vector3.forward * Vector3.Angle(this.m_chassisTC.transform.up, new Vector3(-3f, -22f)));
		PrefabC prefabC3 = PrefabS.AddComponent(this.m_rtSuspensersTC, new Vector3(0f, 0f, -16.5f), this.m_mainPrefab.transform.Find("Parts/Suspension").gameObject);
		PrefabC prefabC4 = PrefabS.AddComponent(this.m_rtSuspensersTC, new Vector3(0f, 0f, 16.5f), this.m_mainPrefab.transform.Find("Parts/Suspension").gameObject);
		this.m_rtSuspensersTC.transform.GetChild(0).name = "Suspension";
		this.m_rtSuspensersTC.transform.GetChild(1).name = "Suspension";
		TransformS.ParentComponent(this.m_rtSuspensersTC, this.m_chassisTC);
		transformC2 = TransformS.AddComponent(this.m_entity, "RearWheelJoint1", this.m_centerOfGravityOffset + this.m_chassisTC.transform.position + new Vector2(0f, -_graphElement.m_height * 0.5f));
		transformC3 = TransformS.AddComponent(this.m_entity, "RearWheelJoint2", vector2);
		ChipmunkProS.AddPinJoint(this.m_chassisBody, this.m_rearWheelBody, transformC2, transformC3);
		TransformS.ParentComponent(transformC2, this.m_chassisTC);
		TransformS.ParentComponent(transformC3, this.m_chassisTC);
		transformC2 = TransformS.AddComponent(this.m_entity, "RearWheelSpringAnchor1", vector2);
		transformC3 = TransformS.AddComponent(this.m_entity, "RearWheelSpringAnchor2", vector2);
		this.m_rearWheelSpring = ChipmunkProS.AddDampedSpring(this.m_chassisBody, this.m_rearWheelBody, transformC2, transformC3, 0f, this.m_rearSpringForce, this.m_rearSpringDamp);
		TransformS.ParentComponent(transformC2, this.m_chassisTC);
		TransformS.ParentComponent(transformC3, this.m_chassisTC);
		float num5 = 1f;
		if (base.m_graphElement.m_flipped)
		{
			num5 = -1f;
		}
		this.m_ftBottomTC = TransformS.AddComponent(this.m_entity);
		TransformS.SetPosition(this.m_ftBottomTC, this.m_frontWheelBody.TC.transform.position + new Vector3(num5 * num4 * 0.25f, -num3 * 0.33f, 0f));
		TransformS.ParentComponent(this.m_ftBottomTC, this.m_chassisTC);
		this.m_rtBottomTC = TransformS.AddComponent(this.m_entity);
		TransformS.SetPosition(this.m_rtBottomTC, this.m_rearWheelBody.TC.transform.position + new Vector3(-num5 * num4 * 0.25f, -num4 * 0.33f, 0f));
		TransformS.ParentComponent(this.m_rtBottomTC, this.m_chassisTC);
		TransformS.ParentComponent(this.m_ftBottomTC, this.m_chassisTC);
		TransformS.ParentComponent(this.m_rtBottomTC, this.m_chassisTC);
		this.InitMotors(this.m_rearWheelBody, this.m_frontWheelBody);
		float num6 = 0.8292683f;
		this.SetMotorParameters(this.m_frontTireForces, this.m_tireRate, this.m_rearTireForces, this.m_tireRate * num6);
		this.m_minigame.SetPlayer(this, this.m_chassisTC, typeof(MotorcycleController));
		if (!this.m_minigame.m_editing)
		{
			base.SetEngineSound(this.m_chassisTC, "/InGame/Vehicles/OffroadCarEngine", 500);
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
			ChipmunkProS.AddCollisionHandler(this.m_frontWheelBody, new CollisionDelegate(this.TireCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)2, true, false, true);
			ChipmunkProS.AddCollisionHandler(this.m_frontWheelBody, new CollisionDelegate(this.TireCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)4, true, false, true);
			ChipmunkProS.AddCollisionHandler(this.m_rearWheelBody, new CollisionDelegate(this.TireCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)2, true, false, true);
			ChipmunkProS.AddCollisionHandler(this.m_rearWheelBody, new CollisionDelegate(this.TireCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)4, true, false, true);
			ChipmunkProS.AddCollisionHandler(this.m_chassisBody, new CollisionDelegate(this.ChassisCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)2, true, false, true);
			ChipmunkProS.AddCollisionHandler(this.m_chassisBody, new CollisionDelegate(this.ChassisCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)4, true, false, true);
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
		ProjectorS.AddComponent(this.m_chassisTC, ResourceManager.GetMaterial(RESOURCE.IngameShadow_Material), num7, new Vector3(0f, 40f));
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
		Vector3 vector3 = this.m_mainPrefab.transform.Find("OffroadCar/CharacterLocator").localPosition + this.m_centerOfGravityOffset;
		this.m_alienCharacter = new AlienCharacter(_graphElement, "Drive", "AlienNewPrefab_GameObject");
		TransformS.SetGlobalPosition(this.m_alienCharacter.m_mainTC, this.m_chassisTC.transform.position + vector3);
		TransformS.ParentComponent(this.m_alienCharacter.m_mainTC, this.m_chassisTC);
		base.health = 2;
		this.m_alienCharacter.SetHat();
		Vector2 vector4 = this.m_chassisTC.transform.localPosition + new Vector2(-2f, 28f);
		Vector2 vector5;
		vector5..ctor(0f, 13f);
		TransformC transformC4 = TransformS.AddComponent(this.m_entity, "Alien");
		TransformS.SetTransform(transformC4, vector4, Vector2.zero);
		Transform transform = ToolBox.SearchHierarchyForBone(this.m_alienCharacter.m_mainPC.p_gameObject.transform, "HeadCollider");
		ucpPolyShape ucpPolyShape2 = ChipmunkProS.GeneratePolyShapeFromGameObject(transform.gameObject, vector5, 10f, 0.2f, 0.5f, (ucpCollisionType)3, 17895696U, true, false);
		ucpPolyShape2.group = this.m_group;
		this.m_alienBody = ChipmunkProS.AddDynamicBody(transformC4, ucpPolyShape2, this.m_unitC);
		this.m_boneModifier = transformC4.transform.gameObject.AddComponent<BoneModifier>();
		this.m_boneModifier.AddChipmunkModifier(this.m_alienBody, ToolBox.SearchHierarchyForBone(this.m_alienCharacter.m_mainPC.p_gameObject.transform, "Spine2"), 0.5f, this.m_chassisTC.transform);
		this.m_boneModifier.AddChipmunkModifier(this.m_alienBody, ToolBox.SearchHierarchyForBone(this.m_alienCharacter.m_mainPC.p_gameObject.transform, "Neck"), 0.5f, this.m_chassisTC.transform);
		this.m_boneModifier.AddChipmunkModifier(this.m_alienBody, ToolBox.SearchHierarchyForBone(this.m_alienCharacter.m_mainPC.p_gameObject.transform, "Head"), 0.5f, this.m_chassisTC.transform);
		transformC2 = TransformS.AddComponent(this.m_entity, "AlienJoint1", vector4 + new Vector2(-8f, -8f));
		ChipmunkProS.AddPivotJoint(this.m_chassisBody, this.m_alienBody, transformC2);
		ChipmunkProS.AddRotaryLimitJoint(this.m_chassisBody, this.m_alienBody, transformC2, -0.034906585f, 0.34906584f);
		this.m_headJoint = ChipmunkProS.AddDampedRotarySpring(this.m_chassisBody, this.m_alienBody, transformC2, 0f, 2500000f, 68000f);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_alienBody, new CollisionDelegate(this.AlienCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)2, true, false, true);
			ChipmunkProS.AddCollisionHandler(this.m_alienBody, new CollisionDelegate(this.AlienCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)4, true, false, true);
			this.m_alienCharacter.CreateCamTarget(this.m_camTargetTC);
			CameraS.m_cameraTargetLimits.b = (float)(-(float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight) * 0.5f;
			base.MoveUnitRandomly(0.1f);
		}
		base.m_graphElement.m_isCopyable = false;
		base.m_graphElement.m_isRemovable = false;
		base.m_graphElement.m_isRotateable = false;
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0002A35F File Offset: 0x0002875F
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Weapon, 17f);
		this.SetBaseArmor(DamageType.Electric, 20f);
		this.SetBaseArmor(DamageType.Fire, 20f);
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0002A38C File Offset: 0x0002878C
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
			if (this.m_frontWheelBody.m_active && this.m_rearWheelBody.m_active)
			{
				Vector3 vector = this.m_frontWheelBody.TC.transform.position - this.m_ftSuspensersTC.transform.position;
				TransformS.SetGlobalRotation(this.m_ftSuspensersTC, new Vector3(0f, 0f, Mathf.Atan2(vector.y, vector.x) * 57.29578f - 90f));
				Vector3 vector2 = this.m_rearWheelBody.TC.transform.position - this.m_rtSuspensersTC.transform.position;
				TransformS.SetGlobalRotation(this.m_rtSuspensersTC, new Vector3(0f, 0f, Mathf.Atan2(vector2.y, vector2.x) * 57.29578f - 90f));
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
					int num2 = 0;
					bool flag3 = base.GetContactState(this.m_rearWheelBody) == ContactState.OnContact;
					bool flag4 = base.GetContactState(this.m_frontWheelBody) == ContactState.OnContact;
					bool flag5 = Controller.GetButtonState("Throttle") == ControllerButtonState.ON;
					bool flag6 = Controller.GetButtonState("Reverse") == ControllerButtonState.ON;
					bool flag7 = Controller.GetButtonState("LeftButton1") == ControllerButtonState.ON;
					bool flag8 = Controller.GetButtonState("LeftButton2") == ControllerButtonState.ON;
					if (flag8)
					{
						num2 = -1;
					}
					else if (flag7)
					{
						num2 = 1;
					}
					if (this.m_contactState == ContactState.OnAir)
					{
						if (flag5 && this.m_affectingBlackHoles != null && this.m_affectingBlackHoles.Count > 0)
						{
							ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, this.m_chassisTC.transform.right * 50f, this.m_chassisTC.transform.position);
						}
						float num3 = ChipmunkProWrapper.ucpBodyGetAngle(this.m_chassisBody.body);
						float num4 = 6.2831855f;
						if (this.m_canGiveBooster)
						{
							float num5 = Mathf.Abs(this.m_boosterReferenceAngle - num3);
							if (ToolBox.IsBetween(Mathf.Abs(this.m_boosterReferenceAngle - this.m_boosterBaseAngle), 3.1415927f - this.m_trickBoostAngle, 3.1415927f + this.m_trickBoostAngle))
							{
								if (num5 > this.m_trickBoostAngle)
								{
									this.TrickBoost(1, false);
									if (flag7)
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
							else if (Mathf.Abs(num3 - this.m_boosterBaseAngle) >= 3.1415927f)
							{
								this.TrickBoost(1, false);
								if (flag7)
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
						else if (Mathf.Abs(num3 % num4 * num4) < this.m_trickBoostAngle)
						{
							this.m_canGiveBooster = true;
							this.m_boosterReferenceAngle = (float)Mathf.RoundToInt(num3 / num4) * num4;
							this.m_boosterBaseAngle = this.m_boosterReferenceAngle;
						}
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
						if (flag5 && !this.m_gasWasPressed)
						{
							SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/BrakePressed", Vector3.zero, "Load", this.m_currentLoad, 1f);
						}
						if (flag6 && !this.m_brakeWasPressed)
						{
							SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/GasPressed", Vector3.zero, "Load", this.m_currentLoad, 1f);
						}
					}
					else
					{
						if (flag5 && !this.m_gasWasPressed)
						{
							SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/GasPressed", Vector3.zero, "Load", this.m_currentLoad, 1f);
						}
						if (flag6 && !this.m_brakeWasPressed)
						{
							SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/BrakePressed", Vector3.zero, "Load", this.m_currentLoad, 1f);
						}
					}
					ucpSegmentQueryInfo ucpSegmentQueryInfo = default(ucpSegmentQueryInfo);
					ucpShapeFilter ucpShapeFilter = default(ucpShapeFilter);
					ucpShapeFilter.ucpShapeFilterAll();
					ucpShapeFilter.group = this.m_group;
					ChipmunkProWrapper.ucpSpaceSegmentQueryFirst(this.m_chassisTC.transform.position, this.m_chassisTC.transform.position + new Vector2(0f, (float)(-200 * this.m_minigame.m_gravityMultipler)), 1f, ucpShapeFilter, ref ucpSegmentQueryInfo);
					if (this.m_airTime > 20 && !this.m_airTimeSoundTriggered && ucpSegmentQueryInfo.alpha > 0.5f)
					{
						int num6 = Random.Range(0, 2);
						if (num6 == 0)
						{
							SoundS.PlaySingleShot("/InGame/JumpCheer", Vector3.zero, 1f);
						}
						else
						{
							SoundS.PlaySingleShot("/InGame/BigAirJump", Vector3.zero, 1f);
						}
						this.m_alienCharacter.AnimateCharacterRandom("JumpCheer", num6);
						this.m_airTimeSoundTriggered = true;
						PsState.m_cameraManTakeShot = true;
					}
					if (flag8)
					{
						num = -1;
					}
					else if (flag7)
					{
						num = 1;
					}
					if (num != this.oldPressedDir)
					{
						flag2 = true;
					}
					this.m_brakeWasPressed = flag6;
					this.m_gasWasPressed = flag5;
					this.oldPressedDir = num;
					if (this.m_minigame.m_gameStarted && !GameLevelPreview.m_levelPreviewRunning)
					{
						CameraTargetC camTarget = this.m_alienCharacter.m_camTarget;
						if (ucpSegmentQueryInfo.alpha == 1f)
						{
							this.camTargetYOffset = (float)(-100 * this.m_minigame.m_gravityMultipler);
						}
						else
						{
							this.camTargetYOffset = (-100f + (1f - ucpSegmentQueryInfo.alpha) * 170f) * (float)this.m_minigame.m_gravityMultipler;
						}
						CameraTargetC cameraTargetC = camTarget;
						cameraTargetC.offset.y = cameraTargetC.offset.y + (this.camTargetYOffset - camTarget.offset.y) * 0.1f;
					}
					this.m_inAir = this.m_contactState == ContactState.OnAir || this.m_groundedChainIndex == -1;
					bool flag9 = base.GetContactState(this.m_frontWheelBody) == ContactState.OnAir && base.GetContactState(this.m_rearWheelBody) == ContactState.OnAir;
					Vector2 vector3 = ChipmunkProWrapper.ucpBodyGetVel(this.m_chassisBody.body);
					this.m_lastFrameVelocity = vector3;
					if (this.m_inAir)
					{
						this.m_airTime++;
						this.m_targetFakeRotationX = ToolBox.limitBetween(vector3.y / 20f, -10f, 10f);
					}
					else
					{
						this.m_targetFakeRotationX = 0f;
						this.m_airTime = 0;
						this.m_airTimeSoundTriggered = false;
					}
					this.m_fakeRotationX -= (this.m_fakeRotationX - this.m_targetFakeRotationX) * 0.1f;
					this.m_chassisTC.transform.Rotate(new Vector3(1f, 0f, 0f), this.m_fakeRotationX);
					float z = this.m_chassisBody.TC.transform.rotation.eulerAngles.z;
					float num7 = 0f;
					if (flag5 && !flag6)
					{
						this.m_gasAmount = ToolBox.limitBetween(this.m_gasAmount + 0.05f, 0f, 1f);
					}
					else if (flag6 && !flag5)
					{
						this.m_gasAmount = ToolBox.limitBetween(this.m_gasAmount - 0.05f, -1f, 0f);
					}
					else
					{
						this.m_gasAmount *= 0.98f;
					}
					if (flag7)
					{
						num7 = this.m_handlingAngle;
					}
					else if (flag8)
					{
						num7 = -this.m_handlingAngle;
					}
					float num8 = (float)((this.m_minigame.m_gravityMultipler <= 0) ? 180 : 0);
					float num9 = ToolBox.limitBetween(Mathf.DeltaAngle(z, num8), -90f, 90f);
					Vector2 velocityRelativeToContacts = base.GetVelocityRelativeToContacts(this.m_rearWheelBody);
					bool flag10 = base.GetRogueContactsCount(this.m_rearWheelBody, false) > 0;
					float num10 = 1f - ToolBox.limitBetween(Mathf.Abs(velocityRelativeToContacts.x) / 600f, 0f, 0.5f);
					float num11 = 1f;
					bool flag11 = false;
					if (!flag9)
					{
						if (Mathf.Abs(num9) < 60f)
						{
							if (flag6 && velocityRelativeToContacts.x * (float)this.m_minigame.m_gravityMultipler > 200f && !flag9)
							{
								flag11 = true;
							}
							if (flag5 && velocityRelativeToContacts.x * (float)this.m_minigame.m_gravityMultipler < -200f && !flag9)
							{
								flag11 = true;
							}
						}
						if (flag11)
						{
							num11 = 0f;
							num10 = 1.5f;
						}
						if (flag10 && !flag5 && !flag6 && velocityRelativeToContacts.magnitude < 80f)
						{
							float num12 = 1f - ToolBox.getPositionBetween(velocityRelativeToContacts.magnitude, 0f, 80f);
							num11 = 0f;
							num10 = 0.4f * num12;
							flag11 = true;
						}
					}
					this.SetMotorParameters(this.m_frontTireForces * num10, this.m_tireRate * num11, this.m_rearTireForces * num10, this.m_tireRate * 0.8292683f * num11);
					int num13 = ((!flag6 || velocityRelativeToContacts.x >= 10f) ? 1 : (-1));
					this.m_alienCharacter.AnimateCharacter("DriveDir", num13);
					if (this.m_lastDriveDir != num13)
					{
						GhostEventType ghostEventType2 = ((num13 != 1) ? GhostEventType.DriveDirBackward : GhostEventType.DriveDirForward);
						PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(ghostEventType2, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
					}
					this.m_lastDriveDir = num13;
					if (!flag11)
					{
						float num14 = Mathf.Abs(ChipmunkProWrapper.ucpBodyGetAngVel(this.m_frontMotor.bodyB.body));
						if (num14 > this.m_tireRate)
						{
							this.SetMotorParameters(Vector2.zero, this.m_tireRate, Vector2.zero, this.m_tireRate);
						}
						this.UpdateMotors((!flag5 && !flag6) ? 0f : (this.m_gasAmount * this.m_trickBoostForce), !base.m_graphElement.m_flipped, this.m_trickBoostForce, false);
					}
					else
					{
						this.UpdateMotors(1f, !base.m_graphElement.m_flipped, 1f, false);
					}
					bool flag12 = false;
					if (this.m_destructibleGroundContacts > 0 && (!flag5 || !flag6) && (flag5 || flag6))
					{
						flag12 = true;
						this.m_skiddingEndTimer = 15;
						if (Main.m_gameTicks % 2 == 0)
						{
							bool flag13 = this.SkidTire(this.m_ftBottomTC);
							bool flag14 = this.SkidTire(this.m_rtBottomTC);
							if (flag13 || flag14)
							{
								this.Skid(this.m_tireBrushSmall, this.m_chassisBottomTC.transform.position);
							}
						}
					}
					if (this.m_skiddingEndTimer > 0)
					{
						this.m_skiddingEndTimer--;
					}
					float num15 = 1f;
					float num16 = this.m_handlingForce;
					if (flag8 || flag7)
					{
						if (!flag6 || velocityRelativeToContacts.x >= 10f)
						{
							this.m_alienCharacter.AnimateCharacter("LeanDir", (!flag8) ? (-1) : 1);
							if (this.m_boneModifier != null)
							{
								this.m_boneModifier.m_globalWeight = Mathf.Lerp(this.m_boneModifier.m_globalWeight, 0f, 0.08f);
							}
							if (flag8)
							{
								float num17 = Mathf.Lerp(ChipmunkProWrapper.ucpDampedRotarySpringGetRestAngle(this.m_headJoint.constraint), 0.034906585f, 0.08f);
								ChipmunkProWrapper.ucpDampedRotarySpringSetRestAngle(this.m_headJoint.constraint, num17);
							}
							else
							{
								float num18 = Mathf.Lerp(ChipmunkProWrapper.ucpDampedRotarySpringGetRestAngle(this.m_headJoint.constraint), -0.34906584f, 0.08f);
								ChipmunkProWrapper.ucpDampedRotarySpringSetRestAngle(this.m_headJoint.constraint, num18);
							}
						}
						else
						{
							this.m_alienCharacter.AnimateCharacter("LeanDir", 0);
							if (this.m_boneModifier != null)
							{
								this.m_boneModifier.m_globalWeight = Mathf.Lerp(this.m_boneModifier.m_globalWeight, 1f, 0.08f);
							}
							float num19 = Mathf.Lerp(ChipmunkProWrapper.ucpDampedRotarySpringGetRestAngle(this.m_headJoint.constraint), 0f, 0.08f);
							ChipmunkProWrapper.ucpDampedRotarySpringSetRestAngle(this.m_headJoint.constraint, num19);
						}
						if (flag9 || this.m_groundedChainIndex == -1)
						{
							this.m_tiltHeldDownCounter += 0.0167f;
						}
						else
						{
							this.m_tiltHeldDownCounter = 0f;
						}
						num16 *= 5f;
					}
					else
					{
						this.m_tiltHeldDownCounter = 0f;
						this.m_alienCharacter.AnimateCharacter("LeanDir", 0);
						if (this.m_boneModifier != null)
						{
							this.m_boneModifier.m_globalWeight = Mathf.Lerp(this.m_boneModifier.m_globalWeight, 1f, 0.08f);
						}
						float num20 = Mathf.Lerp(ChipmunkProWrapper.ucpDampedRotarySpringGetRestAngle(this.m_headJoint.constraint), 0f, 0.08f);
						ChipmunkProWrapper.ucpDampedRotarySpringSetRestAngle(this.m_headJoint.constraint, num20);
					}
					if (flag9 || this.m_groundedChainIndex == -1)
					{
						ToolBox.limitBetween(Mathf.DeltaAngle(z, num7), -90f, 90f);
					}
					else
					{
						num9 = num7;
					}
					float num21 = ChipmunkProWrapper.ucpBodyGetAngVel(this.m_chassisBody.body);
					float rolledValue = ToolBox.getRolledValue(z, -180f, 180f);
					if (flag7 && num9 < 0f && Mathf.Abs(rolledValue) > 130f)
					{
						num9 = Mathf.Abs(num9);
					}
					else if (flag8 && num9 > 0f && Mathf.Abs(rolledValue) > 90f)
					{
						num9 = -num9;
					}
					if (flag2)
					{
						num21 *= 0.75f;
					}
					float num22 = ToolBox.limitBetween(num9 * num16, -num15, num15);
					if (this.m_contactState == ContactState.OnAir || this.m_groundedChainIndex == -1)
					{
						float num23 = Mathf.Max(0f, Mathf.Min(1f, ucpSegmentQueryInfo.alpha));
						float num24 = Mathf.Min(1f, Main.m_resettingGameTime - this.m_lastGroundContact);
						float num25 = Mathf.Max(0.3f * num23 + 0.2f, Mathf.Max(num23, num24) * this.m_tiltHeldDownCounter);
						float num26 = ToolBox.limitBetween(num7 * num25 * 0.017453292f, -2f, 2f);
						num21 += num26;
						num21 = ToolBox.limitBetween(num21, -5f, 5f);
						if (base.Contact(CMBIdentifiers.Airflow))
						{
							Vector2 vector4 = ChipmunkProWrapper.ucpBodyGetVel(this.m_chassisBody.body);
							if ((num21 < 0f && vector4.x < 350f) || (num21 > 0f && vector4.x > -350f))
							{
								ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(this.m_chassisBody.body, Vector2.right * 15f * -num21, this.m_chassisTC.transform.position);
							}
						}
					}
					else
					{
						num21 += num22;
					}
					if (!flag4 && !flag3)
					{
						num21 = ToolBox.limitBetween(num21, -5f, 5f);
					}
					ChipmunkProWrapper.ucpBodySetAngVel(this.m_chassisBody.body, num21);
					if (flag9 && this.m_drivingFx != null)
					{
						base.PauseDriveFx();
					}
					else if (this.m_drivingFx != null)
					{
						ParticleSystem component = this.m_drivingFx.p_gameObject.GetComponent<ParticleSystem>();
						if (this.m_drivingFxName.Equals("MudSplatter_GameObject"))
						{
							if (!flag12 && this.m_skiddingEndTimer == 0 && component.isPlaying)
							{
								component.Stop();
							}
							else if (flag12 && component.isStopped)
							{
								component.Play();
							}
						}
						else
						{
							float num27 = ToolBox.getPositionBetween(this.m_currentRPM, 500f, 5000f) * 30f;
							component.emissionRate = num27;
						}
					}
					this.m_boostEffectFrontTireTC.forcedRotation = Quaternion.Euler(Vector3.forward * this.m_chassisTC.transform.eulerAngles.z);
					this.m_boostEffectRearTireTC.forcedRotation = Quaternion.Euler(Vector3.forward * this.m_chassisTC.transform.eulerAngles.z);
					this.m_boostEffectFrontTireTC.updateRotation = true;
					this.m_boostEffectRearTireTC.updateRotation = true;
					if (this.m_engineSound != null)
					{
						float num28 = Mathf.Abs(ChipmunkProWrapper.ucpBodyGetAngVel(this.m_rearWheelBody.body));
						float positionBetween = ToolBox.getPositionBetween(num28, 0f, 50f);
						float num29 = ((!flag5 && !flag6) ? 500f : Mathf.Lerp(500f, 4000f, positionBetween));
						float num30 = (float)((!flag9) ? 1 : 0);
						base.UpdateEngineSound(num29, num30);
					}
					if (this.m_tireRollSound != null)
					{
						if (flag9)
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
						if ((this.m_gasWasPressed || this.m_brakeWasPressed) && (flag3 || flag4))
						{
							float num31 = 1f;
							if (this.m_brakeWasPressed)
							{
								num31 = -1f;
							}
							PsState.m_activeMinigame.m_playerUnit.AddSpeed(this.m_chassisTC.transform.right * this.m_trickBoostForce * num31, 1000f);
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
							this.m_trickBoosting = (this.m_resourceBoosting = false);
							float num32 = Mathf.Lerp(this.m_suspensionValues[0], this.m_suspensionValues[this.m_suspensionValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
							float num33 = 10400f * this.m_suspensionMultiplier * -1f;
							float num34 = 9600f * this.m_suspensionMultiplier * -1f;
							float num35 = Mathf.Lerp(this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
							float num36 = Mathf.Lerp(this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
							float num37 = 1.5f * Mathf.Lerp(this.m_gripValues[0], this.m_gripValues[this.m_gripValues.Length - 1], this.m_gripNormalizedUpgradeValue);
							this.m_tireRate = Mathf.Lerp(this.m_tireRateValues[0], this.m_tireRateValues[this.m_tireRateValues.Length - 1], this.m_powerNormalizedUpgradeValue);
							ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_frontWheelSpring.constraint, num33);
							ChipmunkProWrapper.ucpDampedSpringSetDamping(this.m_frontWheelSpring.constraint, num35);
							ChipmunkProWrapper.ucpConstraintSetMaxForce(this.m_rearWheelSpring.constraint, num34);
							ChipmunkProWrapper.ucpDampedSpringSetDamping(this.m_rearWheelSpring.constraint, num36);
							ChipmunkProWrapper.ucpShapeSetFriction(this.m_frontWheelShape.shapePtr, num37);
							ChipmunkProWrapper.ucpShapeSetFriction(this.m_rearWheelShape.shapePtr, num37);
							base.RemoveBoostSound();
							this.m_boostEffectFrontTire.DropBoost();
							this.m_boostEffectRearTire.DropBoost();
							PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.BoostEnd, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
							if (this.m_trailBase != null)
							{
								this.m_trailBase.SetBoostActive(false);
							}
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
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0002BBB4 File Offset: 0x00029FB4
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

	// Token: 0x060002CA RID: 714 RVA: 0x0002BC0C File Offset: 0x0002A00C
	private void UpdateAntenna()
	{
		Vector3 vector = this.m_chassisBody.TC.transform.TransformPoint(new Vector3(-35f, 43f));
		Vector3 vector2 = vector - this.m_antennaRoot.TC.transform.position;
		float num = ChipmunkProWrapper.ucpBodyGetAngle(this.m_chassisBody.body);
		float num2 = num - ChipmunkProWrapper.ucpBodyGetAngle(this.m_antennaRoot.body);
		ChipmunkProWrapper.ucpBodySetPos(this.m_antennaRoot.body, vector);
		ChipmunkProWrapper.ucpBodySetAngle(this.m_antennaRoot.body, num);
		ChipmunkProWrapper.ucpBodySetVel(this.m_antennaRoot.body, vector2 * (1f / Main.m_gameDeltaTime));
		ChipmunkProWrapper.ucpBodySetAngVel(this.m_antennaRoot.body, num2 * (1f / Main.m_gameDeltaTime));
		TransformS.SetPosition(this.m_antennaRoot.TC, vector);
		TransformS.SetRotation(this.m_antennaRoot.TC, Vector3.forward * num * 57.29578f);
		string text = "Antenna/Bone1";
		for (int i = 1; i < this.m_antennaSegments.Count; i++)
		{
			text = text + "/Bone" + (i + 1);
			Transform transform = this.m_antennaPrefab.p_gameObject.transform.Find(text);
			if (transform != null)
			{
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, this.m_antennaSegments[i].TC.transform.rotation.eulerAngles.z));
			}
		}
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0002BDCC File Offset: 0x0002A1CC
	private void CreateAntenna(GameObject _antennaPrefab, Entity _e, ChipmunkBodyC _chassis, Vector2 _pos)
	{
		float num = 8f;
		TransformC transformC = TransformS.AddComponent(_e, "Antenna");
		TransformS.ParentComponent(transformC, _chassis.TC, new Vector3(-35f, 43f, 0f));
		this.m_antennaPrefab = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, -20f), _antennaPrefab);
		this.m_antennaSegments = new List<ChipmunkBodyC>();
		for (int i = 1; i <= 5; i++)
		{
			TransformC transformC2 = TransformS.AddComponent(_e, "AntennaSegment" + i);
			TransformS.SetTransform(transformC2, Vector3.up * num * (float)(i - 1) + transformC.transform.position, Vector2.zero);
			ucpPolyShape ucpPolyShape = new ucpPolyShape(4f, num, Vector2.zero, 17895696U, 6f / (float)i, 0f, 0f, (ucpCollisionType)3, false);
			ucpPolyShape.group = this.m_group;
			ChipmunkBodyC chipmunkBodyC;
			if (this.m_antennaSegments.Count == 0)
			{
				chipmunkBodyC = ChipmunkProS.AddKinematicBody(transformC2, ucpPolyShape, null);
				this.m_antennaRoot = chipmunkBodyC;
			}
			else
			{
				chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC2, ucpPolyShape, null);
			}
			if (this.m_antennaSegments.Count > 0)
			{
				TransformC transformC3 = TransformS.AddComponent(_e, "AntennaJoint" + i);
				TransformS.SetTransform(transformC3, transformC2.transform.position - Vector3.up * (num / 2f), Vector2.zero);
				ChipmunkProS.AddPivotJoint(this.m_antennaSegments[this.m_antennaSegments.Count - 1], chipmunkBodyC, transformC3);
				ChipmunkProS.AddRotaryLimitJoint(this.m_antennaSegments[this.m_antennaSegments.Count - 1], chipmunkBodyC, transformC2, -1.0471976f, 1.0471976f);
				ChipmunkProS.AddDampedRotarySpring(this.m_antennaSegments[this.m_antennaSegments.Count - 1], chipmunkBodyC, transformC2, 0f, 2000000f / (float)i, 10000f);
			}
			this.m_antennaSegments.Add(chipmunkBodyC);
		}
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0002BFE8 File Offset: 0x0002A3E8
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

	// Token: 0x060002CD RID: 717 RVA: 0x0002C07C File Offset: 0x0002A47C
	public bool SkidTire(TransformC _c)
	{
		Vector2 vector = _c.transform.position;
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

	// Token: 0x060002CE RID: 718 RVA: 0x0002C124 File Offset: 0x0002A524
	public void AddDirt(Vector2 _pos)
	{
		_pos += new Vector2(16f, 16f);
		AutoGeometryBrush autoGeometryBrush = new AutoGeometryBrush(1.5f, false, 0.5f, 0f);
		AutoGeometryLayer autoGeometryLayer = AutoGeometryManager.m_layers[0];
		autoGeometryLayer.PaintWithBrush(autoGeometryBrush, _pos, AGDrawMode.ADD, true, ref autoGeometryLayer.m_bytes, true, false);
		autoGeometryBrush.Destroy();
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0002C184 File Offset: 0x0002A584
	private ChipmunkBodyC CreateTire(Entity _e, ChipmunkBodyC _chassis, Vector2 _pos, float _rad, float _weight, bool _front)
	{
		TransformC transformC = TransformS.AddComponent(_e, "Car Tire");
		TransformC transformC2 = TransformS.AddComponent(_e, "Effect");
		transformC2.forceRotation = true;
		TransformS.ParentComponent(transformC2, transformC, Vector3.zero);
		TransformS.SetTransform(transformC, _pos, Vector2.zero);
		ucpCircleShape ucpCircleShape = new ucpCircleShape(_rad, Vector2.zero, 17895696U, _weight, 0.2f, this.m_tireGrip, (ucpCollisionType)3, false);
		ucpCircleShape.group = this.m_group;
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, ucpCircleShape, null);
		if (_front)
		{
			PrefabC prefabC = PrefabS.AddComponent(transformC2, Vector3.forward * -24f, ResourceManager.GetGameObject(RESOURCE.EffectBoostOffroadCarFront_GameObject), "BoostEffect", true, true);
			this.m_boostEffectFrontTire = prefabC.p_gameObject.GetComponent<EffectBoost>();
			this.m_boostEffectFrontTireTC = transformC2;
		}
		else
		{
			PrefabC prefabC2 = PrefabS.AddComponent(transformC2, Vector3.forward * -24f, ResourceManager.GetGameObject(RESOURCE.EffectBoostOffroadCarBack_GameObject), "BoostEffect", true, true);
			this.m_boostEffectRearTire = prefabC2.p_gameObject.GetComponent<EffectBoost>();
			this.m_boostEffectRearTireTC = transformC2;
		}
		string text = "T3";
		PrefabC prefabC4;
		if (_front)
		{
			PrefabC prefabC3 = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, -23f), this.m_mainPrefab.transform.Find("Parts/CarTireFront" + text).gameObject);
			prefabC4 = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 23f), this.m_mainPrefab.transform.Find("Parts/CarTireFront" + text).gameObject);
			prefabC4.m_identifier = 10;
			prefabC3.m_identifier = 10;
			this.m_frontWheelShape = ucpCircleShape;
		}
		else
		{
			PrefabC prefabC3 = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, -23f), this.m_mainPrefab.transform.Find("Parts/CarTireRear" + text).gameObject);
			prefabC4 = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 23f), this.m_mainPrefab.transform.Find("Parts/CarTireRear" + text).gameObject);
			prefabC4.m_identifier = 11;
			prefabC3.m_identifier = 11;
			this.m_rearWheelShape = ucpCircleShape;
		}
		prefabC4.p_gameObject.transform.localScale = new Vector3(1f, 1f, -1f);
		ChipmunkProS.SetBodyLinearDamp(chipmunkBodyC, new Vector2(this.UNIT_LINEAR_DAMP.x, this.UNIT_LINEAR_DAMP.y));
		ChipmunkProS.SetBodyAngularDamp(chipmunkBodyC, 0.997f);
		TransformS.ParentComponent(transformC, this.m_chassisTC);
		return chipmunkBodyC;
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0002C434 File Offset: 0x0002A834
	private void RemoveTire(ChipmunkBodyC _c, int _identifier = 10)
	{
		List<IComponent> componentsByIdentifier = EntityManager.GetComponentsByIdentifier(this.m_entity, _identifier);
		for (int i = 0; i < componentsByIdentifier.Count; i++)
		{
			PrefabC prefabC = componentsByIdentifier[i] as PrefabC;
			PrefabS.RemoveComponent(prefabC, true);
		}
		TransformS.RemoveComponent(_c.TC);
		ChipmunkProS.RemoveBody(_c);
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x0002C48C File Offset: 0x0002A88C
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
		this.m_resourceBoosting = _resourceBoost;
		this.m_trickBoosting = this.m_trickBoosting || !_resourceBoost;
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
		float num7 = 1.5f * Mathf.Lerp(this.m_gripValues[0], this.m_gripValues[this.m_gripValues.Length - 1], 1f);
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

	// Token: 0x060002D2 RID: 722 RVA: 0x0002C764 File Offset: 0x0002AB64
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

	// Token: 0x060002D3 RID: 723 RVA: 0x0002C7D0 File Offset: 0x0002ABD0
	public override void UnitContactStart(ContactInfo _contactInfo)
	{
		base.UnitContactStart(_contactInfo);
		if (this.m_isDead || this.m_hasBrokenDown)
		{
			return;
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0002C7F0 File Offset: 0x0002ABF0
	public override void GroundContactStart(ContactInfo _contactInfo)
	{
		base.GroundContactStart(_contactInfo);
		if (this.m_isDead || this.m_hasBrokenDown)
		{
			return;
		}
		Ground ground = _contactInfo.m_ground;
		if (_contactInfo.m_contactBody.body == this.m_rearWheelBody.body || _contactInfo.m_contactBody.body == this.m_frontWheelBody.body)
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

	// Token: 0x060002D5 RID: 725 RVA: 0x0002C8E4 File Offset: 0x0002ACE4
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
		if (_contactInfo.m_contactBody.body == this.m_rearWheelBody.body || _contactInfo.m_contactBody.body == this.m_frontWheelBody.body)
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

	// Token: 0x060002D6 RID: 726 RVA: 0x0002CA6C File Offset: 0x0002AE6C
	private void TireCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
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
				SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/TireImpact", Vector3.zero, "Force", 0.2f + positionBetween * 0.8f, 1f);
			}
		}
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x0002CAE4 File Offset: 0x0002AEE4
	private void ChassisCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (this.m_isDead || this.m_hasBrokenDown)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin)
		{
			float positionBetween = ToolBox.getPositionBetween(_pair.impulse.magnitude, 3500f, 25000f);
			if (positionBetween > 0f)
			{
				SoundS.PlaySingleShotWithParameter("/InGame/Vehicles/ChassisImpact", Vector3.zero, "Force", 0.2f + positionBetween * 0.8f, 1f);
				if (positionBetween > 0.9f)
				{
					base.CreateRandomImpactDebris(this.m_chassisDebrisParts, this.m_debrisPrefab);
				}
			}
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0002CB78 File Offset: 0x0002AF78
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

	// Token: 0x060002D9 RID: 729 RVA: 0x0002CCF4 File Offset: 0x0002B0F4
	public void DetachHeadwear()
	{
		PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.HatDetached, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
		Vector2 vector = Vector2.zero;
		if (this.m_alienBody != null)
		{
			vector = ChipmunkProWrapper.ucpBodyGetVel(this.m_alienBody.body);
		}
		if (this.m_alienCharacter != null)
		{
			this.m_alienCharacter.ThrowHelmet(vector, 0f, false, 17895696U, false, true);
		}
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0002CD68 File Offset: 0x0002B168
	public override void EmergencyKill()
	{
		if (this.m_entity != null && !this.m_hasBrokenDown)
		{
			this.m_emergencyKillAction = delegate
			{
				this.m_killScaleTime = false;
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

	// Token: 0x060002DB RID: 731 RVA: 0x0002CDC4 File Offset: 0x0002B1C4
	public override void LaunchRagdoll(bool _explodeBones = false)
	{
		ChipmunkBodyShape bodyShapeByTag = ChipmunkProS.GetBodyShapeByTag(this.m_chassisBody, "OffroadCarColliderMiddle");
		ChipmunkProWrapper.ucpShapeSetLayers(bodyShapeByTag.shapePtr, 0U);
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
		if (this.m_alienBody != null && this.m_alienBody.body != IntPtr.Zero)
		{
			ChipmunkProS.RemoveBody(this.m_alienBody);
		}
		this.m_alienBody = null;
	}

	// Token: 0x060002DC RID: 732 RVA: 0x0002CEC4 File Offset: 0x0002B2C4
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
			if (_damageType != DamageType.Impact && _damageType != DamageType.BlackHole)
			{
				Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("Bumper"), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, this.m_alienCharacter.m_group, false, null, Vector3.zero, -1f, 17895696U);
				Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("Hood"), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, this.m_alienCharacter.m_group, false, null, Vector3.zero, -1f, 17895696U);
				Debris.CreateDebrisFromGO(this.m_chassisPrefab.p_gameObject.transform.Find("Motor"), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, this.m_alienCharacter.m_group, false, null, Vector3.zero, -1f, 17895696U);
			}
			SoundS.PlaySingleShot("/InGame/Characters/AlienDisappointment", Vector3.zero, 1f);
			if (_damageType == DamageType.Weapon)
			{
				SoundS.PlaySingleShot("/Ingame/Characters/AlienDeathScream", Vector3.zero, 1f);
			}
			else if (_damageType == DamageType.BlackHole)
			{
				SoundS.PlaySingleShot("/Ingame/Units/BlackHoleSuck_Alien", Vector3.zero, 1f);
			}
			if (_damageType != DamageType.Impact && _damageType != DamageType.BlackHole)
			{
				PrefabS.AddComponent(this.m_chassisTC, new Vector3(33.13f, 30.11f, -2.91f), ResourceManager.GetGameObject(RESOURCE.EngineBreakdown_GameObject));
			}
			if (!this.m_minigame.m_playerReachedGoal)
			{
				PsState.m_activeGameLoop.LoseMinigame();
			}
		}
		if (!this.m_tiresBlown && _damageType == DamageType.Weapon)
		{
			SoundS.PlaySingleShot("/InGame/Vehicles/OffroadCarDeath", Vector3.zero, 1f);
			if (!this.m_minigame.m_gameEnded && this.m_killScaleTime)
			{
				PsState.m_activeMinigame.TweenTimeScale(0.025f, TweenStyle.ExpoIn, 0.1f, delegate
				{
					PsState.m_activeMinigame.TweenTimeScale(1f, TweenStyle.ExpoIn, 0.25f, null, 0f);
				}, 0.5f);
			}
			base.CreateRandomImpactDebris(this.m_chassisDebrisParts, this.m_debrisPrefab);
			this.CreateTireDebris();
			this.m_tiresBlown = true;
		}
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0002D1D0 File Offset: 0x0002B5D0
	public void CreateTireDebris()
	{
		int num = Random.Range(1, 4);
		for (int i = ((num != 2) ? num : 1); i <= ((num != 2) ? (num + 1) : 4); i++)
		{
			Entity entity = EntityManager.AddEntity(new string[] { "GTAG_AUTODESTROY", "GTAG_DEBRIS" });
			TransformC transformC = TransformS.AddComponent(entity, "Tire" + i);
			TransformS.SetTransform(transformC, (i >= 3) ? this.m_rearWheelBody.TC.transform.position : this.m_frontWheelBody.TC.transform.position, Vector2.zero);
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, new ucpCircleShape(((i >= 3) ? 41f : 34f) * 0.45f, Vector2.zero, 17895696U, (i >= 3) ? 5f : 5f, 0.2f, this.m_tireGrip, (ucpCollisionType)7, false)
			{
				group = (uint)((ulong)this.m_group + (ulong)((i >= 3) ? 2L : 1L))
			}, null);
			PrefabC prefabC;
			if (i < 3)
			{
				prefabC = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, (float)((i % 2 != 1) ? 23 : (-23))), this.m_mainPrefab.transform.Find("Parts/CarTireFrontT3").gameObject);
			}
			else
			{
				prefabC = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, (float)((i % 2 != 1) ? 23 : (-23))), this.m_mainPrefab.transform.Find("Parts/CarTireRearT3").gameObject);
			}
			if (i % 2 == 0)
			{
				prefabC.p_gameObject.transform.localScale = new Vector3(1f, 1f, -1f);
			}
			ChipmunkProWrapper.ucpBodySetVel(chipmunkBodyC.body, base.GetRandomDebrisVel());
			ChipmunkProWrapper.ucpBodySetAngVel(chipmunkBodyC.body, base.GetRandomDebrisAngVel());
			Vector2 vector = PsState.m_activeMinigame.m_globalGravity * (float)PsState.m_activeMinigame.m_gravityMultipler;
			ChipmunkProWrapper.ucpBodySetGravity(chipmunkBodyC.body, vector);
			ChipmunkProWrapper.ucpBodyActivate(chipmunkBodyC.body);
		}
		if (num <= 2)
		{
			Debris.CreateDebrisFromGO(this.m_ftSuspensersTC.transform.GetChild(0), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, this.m_alienCharacter.m_group, true, null, Vector3.zero, -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_ftSuspensersTC.transform.GetChild(0), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, this.m_alienCharacter.m_group, true, null, Vector3.zero, -1f, 17895696U);
			this.RemoveTire(this.m_frontWheelBody, 10);
		}
		if (num >= 2)
		{
			Debris.CreateDebrisFromGO(this.m_rtSuspensersTC.transform.GetChild(0), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, this.m_alienCharacter.m_group, true, null, Vector3.zero, -1f, 17895696U);
			Debris.CreateDebrisFromGO(this.m_rtSuspensersTC.transform.GetChild(0), base.GetRandomDebrisVel(), base.GetRandomDebrisAngVel(), true, this.m_alienCharacter.m_group, true, null, Vector3.zero, -1f, 17895696U);
			this.RemoveTire(this.m_rearWheelBody, 11);
		}
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0002D54E File Offset: 0x0002B94E
	public void DestroyBoneModifier()
	{
		if (this.m_boneModifier != null)
		{
			Object.DestroyImmediate(this.m_boneModifier);
			this.m_boneModifier = null;
		}
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0002D574 File Offset: 0x0002B974
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
		if (this.m_tireBrushSmall != null)
		{
			this.m_tireBrushSmall.Destroy();
		}
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0002D62C File Offset: 0x0002BA2C
	public override List<float> ParseUpgradeValues(Hashtable _upgrades)
	{
		this.ParseMinAndMaxValues();
		float num = ((_upgrades != null) ? Convert.ToSingle(_upgrades["tireForce"]) : 1f);
		float num2 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["tireRate"]) : 60f);
		float num3 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["grip"]) : 1f);
		float num4 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["suspensionForce"]) : (-1f));
		float num5 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["suspensionDamp"]) : 220f);
		float num6 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["COG"]) : 0f);
		float num7 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["handlingForce"]) : 1f) / 1000f;
		float num8 = ((_upgrades != null) ? Convert.ToSingle(_upgrades["handlingAngle"]) : 30f);
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
		list3.Add(ToolBox.getPositionBetween(num8, this.m_angleValues[0], this.m_angleValues[this.m_angleValues.Length - 1]));
		list4.Add(Enumerable.Max(list));
		list4.Add(Enumerable.Max(list2));
		list4.Add(Enumerable.Max(list3));
		return list4;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0002D8D0 File Offset: 0x0002BCD0
	public override Hashtable GetUpgradeValues()
	{
		this.CalculateUpgrades();
		Debug.LogWarning("GETTING VEHICLE UPGRADE VALUES");
		Hashtable hashtable = new Hashtable();
		hashtable.Add("tireForce", this.m_tireForceMultiplier);
		hashtable.Add("tireRate", this.m_tireRate);
		hashtable.Add("grip", this.m_tireGrip / 1.5f);
		hashtable.Add("suspensionForce", this.m_suspensionMultiplier);
		hashtable.Add("suspensionDamp", this.m_rearSpringDamp);
		hashtable.Add("COG", this.m_COGAdd);
		hashtable.Add("handlingForce", this.m_handlingForce * 1000f);
		hashtable.Add("handlingAngle", this.m_handlingAngle);
		return hashtable;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0002D9B0 File Offset: 0x0002BDB0
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
		list.Add("angleValues");
		return list;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0002DA28 File Offset: 0x0002BE28
	public override List<KeyValuePair<string, int>> GetUpgrades()
	{
		Debug.LogWarning("GETTING PLAYER UPGRADES");
		List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
		KeyValuePair<string, int> keyValuePair = new KeyValuePair<string, int>("power", PsMetagameManager.m_playerStats.GetUpgradeValue(base.GetType(), "power", 0));
		list.Add(keyValuePair);
		keyValuePair = new KeyValuePair<string, int>("grip", PsMetagameManager.m_playerStats.GetUpgradeValue(base.GetType(), "grip", 0));
		list.Add(keyValuePair);
		keyValuePair = new KeyValuePair<string, int>("handling", PsMetagameManager.m_playerStats.GetUpgradeValue(base.GetType(), "handling", 0));
		list.Add(keyValuePair);
		return list;
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0002DAC4 File Offset: 0x0002BEC4
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
			this.m_angleValues = Array.ConvertAll<object, float>((object[])upgradeValues["angleValues"], new Converter<object, float>(base.ToFloat));
			this.m_upgradeSteps = psUpgradeableEditorItem.m_upgradeSteps;
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0002DC60 File Offset: 0x0002C060
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
				}
			}
			this.m_powerNormalizedUpgradeValue = base.GetUpgradeValue(base.GetType(), PsUpgradeManager.UpgradeType.SPEED) + num;
			this.m_gripNormalizedUpgradeValue = base.GetUpgradeValue(base.GetType(), PsUpgradeManager.UpgradeType.GRIP) + num;
			this.m_handlingNormalizedUpgradeValue = base.GetUpgradeValue(base.GetType(), PsUpgradeManager.UpgradeType.HANDLING) + num;
			Debug.LogWarning("OWN VEHICLE: ");
		}
		this.m_tireForceMultiplier = Mathf.Lerp(this.m_tireForceValues[0], this.m_tireForceValues[this.m_tireForceValues.Length - 1], this.m_powerNormalizedUpgradeValue);
		this.m_frontTireForces = new Vector2(960000f, 960000f) * this.m_tireForceMultiplier;
		this.m_rearTireForces = new Vector2(1280000f, 1280000f) * this.m_tireForceMultiplier;
		this.m_tireRate = Mathf.Lerp(this.m_tireRateValues[0], this.m_tireRateValues[this.m_tireRateValues.Length - 1], this.m_powerNormalizedUpgradeValue);
		this.m_tireGrip = 1.5f * Mathf.Lerp(this.m_gripValues[0], this.m_gripValues[this.m_gripValues.Length - 1], this.m_gripNormalizedUpgradeValue);
		this.m_suspensionMultiplier = Mathf.Lerp(this.m_suspensionValues[0], this.m_suspensionValues[this.m_suspensionValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
		this.m_frontSpringForce = 10400f * this.m_suspensionMultiplier * -1f;
		this.m_rearSpringForce = 9600f * this.m_suspensionMultiplier * -1f;
		this.m_frontSpringDamp = Mathf.Lerp(this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
		this.m_rearSpringDamp = Mathf.Lerp(this.m_dampValues[0], this.m_dampValues[this.m_dampValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
		this.m_COGAdd = Mathf.Lerp(this.m_COGValues[0], this.m_COGValues[this.m_COGValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
		this.m_centerOfGravityOffset = new Vector2(0f, 16f + this.m_COGAdd);
		this.m_handlingForce = 0.001f;
		this.m_handlingAngle = Mathf.Lerp(this.m_angleValues[0], this.m_angleValues[this.m_angleValues.Length - 1], this.m_handlingNormalizedUpgradeValue);
	}

	// Token: 0x04000356 RID: 854
	public float[] m_tireForceValues = new float[] { 0.7f, 1f, 1.2f, 1.275f, 1.3f };

	// Token: 0x04000357 RID: 855
	public float[] m_tireRateValues = new float[] { 40f, 55f, 62.5f, 67.5f, 70f };

	// Token: 0x04000358 RID: 856
	public float[] m_suspensionValues = new float[] { -1.2f, -0.9f, -0.8f, -0.75f, -0.7f };

	// Token: 0x04000359 RID: 857
	public float[] m_dampValues = new float[] { 180f, 210f, 220f, 225f, 230f };

	// Token: 0x0400035A RID: 858
	public float[] m_angleValues = new float[] { 20f, 27f, 31f, 33.5f, 35f };

	// Token: 0x0400035B RID: 859
	public float[] m_handlingValues = new float[] { 0.75f, 0.95f, 1.05f, 1.12f, 1.15f };

	// Token: 0x0400035C RID: 860
	public float[] m_gripValues = new float[] { 0.55f, 1.2f, 1.45f, 1.67f, 1.85f };

	// Token: 0x0400035D RID: 861
	public float[] m_COGValues = new float[] { -1.8f, 3f, 4.5f, 5.5f, 6f };

	// Token: 0x0400035E RID: 862
	public int[] m_ranges = new int[] { 0, 5, 10, 15, 20 };

	// Token: 0x0400035F RID: 863
	public float m_powerNormalizedUpgradeValue;

	// Token: 0x04000360 RID: 864
	public float m_handlingNormalizedUpgradeValue;

	// Token: 0x04000361 RID: 865
	public float m_gripNormalizedUpgradeValue;

	// Token: 0x04000362 RID: 866
	public Vector2 UNIT_LINEAR_DAMP = new Vector2(0.995f, 0.997f);

	// Token: 0x04000363 RID: 867
	public Vector2 m_frontTireForces = new Vector2(960000f, 960000f);

	// Token: 0x04000364 RID: 868
	public Vector2 m_rearTireForces = new Vector2(1280000f, 1280000f);

	// Token: 0x04000365 RID: 869
	private float m_tireRate = 60f;

	// Token: 0x04000366 RID: 870
	private float m_frontSpringForce = 10400f;

	// Token: 0x04000367 RID: 871
	private float m_frontSpringDamp = 220f;

	// Token: 0x04000368 RID: 872
	private float m_rearSpringForce = 9600f;

	// Token: 0x04000369 RID: 873
	private float m_rearSpringDamp = 220f;

	// Token: 0x0400036A RID: 874
	private float m_tireGrip = 1.5f;

	// Token: 0x0400036B RID: 875
	private float m_handlingForce = 0.001f;

	// Token: 0x0400036C RID: 876
	private float m_handlingAngle = 30f;

	// Token: 0x0400036D RID: 877
	private float m_tireForceMultiplier = 1f;

	// Token: 0x0400036E RID: 878
	private float m_suspensionMultiplier = -1f;

	// Token: 0x0400036F RID: 879
	private float m_COGAdd;

	// Token: 0x04000370 RID: 880
	private const float TIRE_ELASTICITY = 0.2f;

	// Token: 0x04000371 RID: 881
	private const float RATE_SCALE = 0.8292683f;

	// Token: 0x04000372 RID: 882
	private const float FRONT_WHEEL_RAD = 34f;

	// Token: 0x04000373 RID: 883
	private const float FRONT_WHEEL_WEIGHT = 5f;

	// Token: 0x04000374 RID: 884
	private const float REAR_WHEEL_RAD = 41f;

	// Token: 0x04000375 RID: 885
	private const float REAR_WHEEL_WEIGHT = 5f;

	// Token: 0x04000376 RID: 886
	private const float CHASSIS_WEIGHT = 100f;

	// Token: 0x04000377 RID: 887
	private const float ALIEN_WEIGHT = 10f;

	// Token: 0x04000378 RID: 888
	private const float TIRE_ANGULAR_DAMP = 0.997f;

	// Token: 0x04000379 RID: 889
	private const float CHASSIS_ANGULAR_DAMP = 0.97f;

	// Token: 0x0400037A RID: 890
	private const float SELF_BALANCE_FORCE = 200f;

	// Token: 0x0400037B RID: 891
	public BoneModifier m_boneModifier;

	// Token: 0x0400037C RID: 892
	public uint m_group;

	// Token: 0x0400037D RID: 893
	public ChipmunkBodyC m_frontWheelBody;

	// Token: 0x0400037E RID: 894
	public ChipmunkBodyC m_rearWheelBody;

	// Token: 0x0400037F RID: 895
	public ucpCircleShape m_frontWheelShape;

	// Token: 0x04000380 RID: 896
	public ucpCircleShape m_rearWheelShape;

	// Token: 0x04000381 RID: 897
	public ChipmunkBodyC m_alienBody;

	// Token: 0x04000382 RID: 898
	public ChipmunkConstraintC m_frontWheelSpring;

	// Token: 0x04000383 RID: 899
	public ChipmunkConstraintC m_rearWheelSpring;

	// Token: 0x04000384 RID: 900
	public TransformC m_chassisTC;

	// Token: 0x04000385 RID: 901
	public TransformC m_ftSuspensersTC;

	// Token: 0x04000386 RID: 902
	public TransformC m_rtSuspensersTC;

	// Token: 0x04000387 RID: 903
	public TransformC m_chassisBottomTC;

	// Token: 0x04000388 RID: 904
	public TransformC m_ftBottomTC;

	// Token: 0x04000389 RID: 905
	public TransformC m_rtBottomTC;

	// Token: 0x0400038A RID: 906
	public float m_gasAmount;

	// Token: 0x0400038B RID: 907
	public bool m_gasWasPressed;

	// Token: 0x0400038C RID: 908
	public bool m_brakeWasPressed;

	// Token: 0x0400038D RID: 909
	public int m_lastLeanDir;

	// Token: 0x0400038E RID: 910
	public int m_lastDriveDir;

	// Token: 0x0400038F RID: 911
	private bool m_rightBoostEventTriggered;

	// Token: 0x04000390 RID: 912
	private bool m_leftBoostEventTriggered;

	// Token: 0x04000391 RID: 913
	public GameObject m_mainPrefab;

	// Token: 0x04000392 RID: 914
	public GameObject m_debrisPrefab;

	// Token: 0x04000393 RID: 915
	public AutoGeometryBrush m_tireBrush;

	// Token: 0x04000394 RID: 916
	public AutoGeometryBrush m_tireBrushSmall;

	// Token: 0x04000395 RID: 917
	private int m_headHitTimer;

	// Token: 0x04000396 RID: 918
	private bool m_tiresBlown;

	// Token: 0x04000397 RID: 919
	public EffectBoost m_boostEffectFrontTire;

	// Token: 0x04000398 RID: 920
	public EffectBoost m_boostEffectRearTire;

	// Token: 0x04000399 RID: 921
	public TransformC m_boostEffectFrontTireTC;

	// Token: 0x0400039A RID: 922
	public TransformC m_boostEffectRearTireTC;

	// Token: 0x0400039B RID: 923
	private const int MIN_RPM = 500;

	// Token: 0x0400039C RID: 924
	private const int MAX_RPM = 4000;

	// Token: 0x0400039D RID: 925
	public int m_airTime;

	// Token: 0x0400039E RID: 926
	public bool m_airTimeSoundTriggered;

	// Token: 0x0400039F RID: 927
	public int m_destructibleGroundContacts;

	// Token: 0x040003A0 RID: 928
	public int m_skiddingEndTimer;

	// Token: 0x040003A1 RID: 929
	public bool m_inAir;

	// Token: 0x040003A2 RID: 930
	public Vector2 m_centerOfGravityOffset;

	// Token: 0x040003A3 RID: 931
	private float m_targetFakeRotationX;

	// Token: 0x040003A4 RID: 932
	private float m_fakeRotationX;

	// Token: 0x040003A5 RID: 933
	private int oldPressedDir;

	// Token: 0x040003A6 RID: 934
	private float camTargetYOffset;

	// Token: 0x040003A7 RID: 935
	private Vector2 m_chassisOffset;

	// Token: 0x040003A8 RID: 936
	private float m_tiltHeldDownCounter;

	// Token: 0x040003A9 RID: 937
	private float m_lastGroundContactAngle;

	// Token: 0x040003AA RID: 938
	private float m_boosterReferenceAngle;

	// Token: 0x040003AB RID: 939
	private float m_boosterBaseAngle;

	// Token: 0x040003AC RID: 940
	private float m_trickBoostAngle = 0.7853982f;

	// Token: 0x040003AD RID: 941
	private bool m_canGiveBooster = true;

	// Token: 0x040003AE RID: 942
	private ChipmunkConstraintC m_headJoint;

	// Token: 0x040003AF RID: 943
	private bool m_lastframeBoostPressed;

	// Token: 0x040003B0 RID: 944
	private PrefabC m_antennaPrefab;

	// Token: 0x040003B1 RID: 945
	private ChipmunkBodyC m_antennaRoot;

	// Token: 0x040003B2 RID: 946
	private List<ChipmunkBodyC> m_antennaSegments;

	// Token: 0x040003B3 RID: 947
	public int m_trickBoostTicks;

	// Token: 0x040003B4 RID: 948
	public bool m_trickBoosting;

	// Token: 0x040003B5 RID: 949
	public bool m_resourceBoosting;

	// Token: 0x040003B6 RID: 950
	public float m_trickBoostForce;
}
