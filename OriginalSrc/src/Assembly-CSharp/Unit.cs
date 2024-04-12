using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class Unit : BasicAssembledClass
{
	// Token: 0x06000402 RID: 1026 RVA: 0x00008ECC File Offset: 0x000072CC
	public Unit(GraphElement _graphElement, UnitType _unitType)
		: base(_graphElement)
	{
		this.m_bodyListChecksum = 0;
		this.m_entity = EntityManager.AddEntity(new string[] { "GTAG_UNIT", _graphElement.m_name });
		base.m_assembledEntities.Add(this.m_entity);
		this.m_unitC = PsS.AddUnit(this.m_entity, this);
		this.m_unitType = _unitType;
		this.m_bodyList = null;
		this.m_hitPointType = HitPointType.Health;
		this.m_hitPoints = 100f;
		this.m_maxHitPoints = 100f;
		this.m_reactToBlastWaves = true;
		this.m_energy = 100f;
		this.m_maxEnergy = 100f;
		this.m_baseShield = new float[6];
		this.m_currentShield = new float[6];
		this.m_baseArmor = new float[6];
		this.m_currentArmor = new float[6];
		this.m_shieldModifiers = new List<StatModifierInfo>();
		this.CalculateCurrentShield();
		this.m_armorModifiers = new List<StatModifierInfo>();
		this.SetAllBaseArmours();
		this.CalculateCurrentArmor();
		this.m_buffs = new List<BuffInfo>();
		this.m_debuffs = new List<BuffInfo>();
		this.m_temperature = 0f;
		this.m_freezeTemperature = -1f;
		this.m_burnTemperature = 1f;
		this.m_contacts = new List<ContactInfo>();
		this.m_contactEndTreshold = 0.1f;
		this.m_contactState = ContactState.OnAir;
		this.m_isTeleporting = false;
		this.m_teleportingTicks = (this.m_originalTeleTicks = 0);
		this.m_teleportingState = 0;
		this.m_teleportEndPos = Vector3.zero;
		this.m_centerMoveDelta = Vector3.zero;
		this.m_teleportModifyAngle = false;
		this.m_teleportFadeRotate = false;
		this.m_goalTeleport = false;
		this.m_teleportOutAngle = 0f;
		this.m_teleTweenCurrentValue = 0f;
		this.m_teleTweenDuration = 0f;
		this.m_teleTweenEndValue = 0f;
		this.m_teleTweenStartValue = 0f;
		this.m_currentTeleTime = 0f;
		this.m_teleported = false;
		this.m_checkForCrushing = false;
		this.m_crushFramesCount = 0;
		this.m_crushTolerance = 50000;
		this.m_affectingBlackHoles = new List<PsBlackHole>();
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x000090E4 File Offset: 0x000074E4
	public virtual void CreateEditorTouchArea(float _width = 100f, float _height = 100f, TransformC _parentTC = null, Vector2 _offset = default(Vector2))
	{
		this.CreateGraphElementTouchArea(_width, _height, _parentTC, _offset);
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x000090F4 File Offset: 0x000074F4
	public virtual void CreateEditorTouchArea(GameObject _collisionGO, TransformC _parentTC = null)
	{
		if (this.m_minigame.m_editing)
		{
			Mesh mesh = ToolBox.DuplicateMesh((_collisionGO.GetComponent("MeshFilter") as MeshFilter).sharedMesh);
			this.CreateGraphElementTouchArea(mesh, _parentTC);
		}
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00009134 File Offset: 0x00007534
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0000913C File Offset: 0x0000753C
	public Vector3 GetZBufferBias()
	{
		Random.seed = (int)base.m_graphElement.m_id;
		return new Vector3(0f, 0f, Random.Range(-0.5f, 0.5f));
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x0000916C File Offset: 0x0000756C
	public virtual void SetAllBaseArmours()
	{
		this.m_baseArmor[(int)((UIntPtr)1)] = -1f;
		this.m_baseArmor[(int)((UIntPtr)0)] = -1f;
		this.m_baseArmor[(int)((UIntPtr)2)] = -1f;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00009198 File Offset: 0x00007598
	public virtual void SetBaseArmor(DamageType _armorType, float _armorValue)
	{
		this.m_baseArmor[(int)((UIntPtr)_armorType)] = _armorValue;
		this.CalculateCurrentArmor();
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x000091AA File Offset: 0x000075AA
	public virtual void SetBaseShield(DamageType _shieldType, float _shieldValue)
	{
		this.m_baseShield[(int)((UIntPtr)_shieldType)] = _shieldValue;
		this.CalculateCurrentShield();
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x000091BC File Offset: 0x000075BC
	public void DestroyAllContacts()
	{
		for (int i = 0; i < this.m_contacts.Count; i++)
		{
			if (this.m_contacts[i].m_unit != null)
			{
				this.m_contacts[i].m_unit.RemoveUnitContactsImmediately(this);
			}
		}
		this.m_contacts.Clear();
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00009220 File Offset: 0x00007620
	public ContactState GetContactState(ChipmunkBodyC _body)
	{
		for (int i = this.m_contacts.Count - 1; i > -1; i--)
		{
			if (this.m_contacts[i].m_cmb.m_identifier != 1 && this.m_contacts[i].m_contactBody.body == _body.body)
			{
				return ContactState.OnContact;
			}
		}
		return ContactState.OnAir;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00009290 File Offset: 0x00007690
	public bool Contact(CMBIdentifiers _identifier)
	{
		for (int i = this.m_contacts.Count - 1; i > -1; i--)
		{
			if (this.m_contacts[i].m_cmb.m_identifier == (int)_identifier)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x000092DC File Offset: 0x000076DC
	public virtual void SetGravity(Vector2 _gravity)
	{
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, this.m_entity);
		for (int i = 0; i < componentsByEntity.Count; i++)
		{
			ChipmunkBodyC chipmunkBodyC = componentsByEntity[i] as ChipmunkBodyC;
			if (chipmunkBodyC.m_isDynamic)
			{
				if (!chipmunkBodyC.m_isDisabled)
				{
					ChipmunkProWrapper.ucpBodySetGravity(chipmunkBodyC.body, _gravity);
				}
				else
				{
					chipmunkBodyC.m_savedGravity = _gravity;
				}
				ChipmunkProWrapper.ucpBodyActivate(chipmunkBodyC.body);
			}
		}
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00009354 File Offset: 0x00007754
	public bool AddGroundContact(ChipmunkBodyC _contactBody, ChipmunkBodyC _groundBody, Ground _ground, Vector2 _contactPos)
	{
		bool flag = false;
		ContactInfo contactInfo = null;
		for (int i = this.m_contacts.Count - 1; i > -1; i--)
		{
			if (this.m_contacts[i].m_contactBody == _contactBody && this.m_contacts[i].m_ground == _ground)
			{
				contactInfo = this.m_contacts[i];
				break;
			}
		}
		if (contactInfo == null)
		{
			flag = true;
			contactInfo = new ContactInfo(_contactBody, _groundBody, _ground, _contactPos);
			this.m_contacts.Add(contactInfo);
		}
		else
		{
			contactInfo.m_contactCount++;
			contactInfo.m_endTime = -1f;
			contactInfo.m_end = false;
			contactInfo.m_contactPoint = _contactPos;
		}
		return flag;
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00009410 File Offset: 0x00007810
	public bool RemoveGroundContact(ChipmunkBodyC _contactBody, Ground _ground)
	{
		ContactInfo contactInfo = null;
		for (int i = this.m_contacts.Count - 1; i > -1; i--)
		{
			if (this.m_contacts[i].m_contactBody == _contactBody && this.m_contacts[i].m_ground == _ground)
			{
				contactInfo = this.m_contacts[i];
				break;
			}
		}
		if (contactInfo != null)
		{
			contactInfo.m_contactCount--;
			if (contactInfo.m_contactCount == 0)
			{
				contactInfo.m_end = true;
				contactInfo.m_endTime = Main.m_resettingGameTime;
			}
			return contactInfo.m_end;
		}
		return true;
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x000094B8 File Offset: 0x000078B8
	public void AddUnitContact(ChipmunkBodyC _contactBody, ChipmunkBodyC _cmb, Unit _unit, Vector2 _contactPos)
	{
		ContactInfo contactInfo = null;
		for (int i = this.m_contacts.Count - 1; i > -1; i--)
		{
			if (this.m_contacts[i].m_cmb == _cmb && this.m_contacts[i].m_contactBody == _contactBody && this.m_contacts[i].m_unit == _unit)
			{
				contactInfo = this.m_contacts[i];
				break;
			}
		}
		if (contactInfo == null)
		{
			this.m_contacts.Add(new ContactInfo(_contactBody, _cmb, _unit, _contactPos));
		}
		else
		{
			contactInfo.m_contactCount++;
			contactInfo.m_endTime = -1f;
			contactInfo.m_end = false;
			contactInfo.m_contactPoint = _contactPos;
		}
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00009584 File Offset: 0x00007984
	public void RemoveUnitContact(ChipmunkBodyC _contactBody, ChipmunkBodyC _cmb, Unit _unit)
	{
		ContactInfo contactInfo = null;
		for (int i = this.m_contacts.Count - 1; i > -1; i--)
		{
			if (this.m_contacts[i].m_contactBody == _contactBody && this.m_contacts[i].m_cmb == _cmb && this.m_contacts[i].m_unit == _unit)
			{
				contactInfo = this.m_contacts[i];
				break;
			}
		}
		if (contactInfo != null)
		{
			contactInfo.m_contactCount--;
			if (contactInfo.m_contactCount == 0)
			{
				contactInfo.m_end = true;
				contactInfo.m_endTime = Main.m_resettingGameTime;
			}
		}
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0000963C File Offset: 0x00007A3C
	public void RemoveUnitContactsImmediately(Unit _unit)
	{
		List<ContactInfo> list = new List<ContactInfo>();
		for (int i = 0; i < this.m_contacts.Count; i++)
		{
			ContactInfo contactInfo = this.m_contacts[i];
			if (contactInfo.m_unit == _unit)
			{
				contactInfo.m_contactCount = 0;
				contactInfo.m_end = true;
				contactInfo.m_endTime = Main.m_resettingGameTime;
				this.HandleContactEnd(contactInfo);
				list.Add(contactInfo);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			this.m_contacts.Remove(list[j]);
		}
		if (this.m_contacts.Count == 0)
		{
			this.m_contactState = ContactState.OnAir;
		}
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x000096EC File Offset: 0x00007AEC
	public virtual void KillingImpact(ChipmunkBodyC _collidingBody, Vector2 _point, Vector2 _normal, Vector2 _impulse)
	{
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x000096F0 File Offset: 0x00007AF0
	public List<IComponent> GetUnitBodyList()
	{
		if (this.m_bodyList == null || this.m_entity.m_componentsChecksum != this.m_bodyListChecksum)
		{
			this.m_bodyList = new List<IComponent>();
			this.m_bodyList = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, this.m_entity);
			this.m_bodyListChecksum = this.m_entity.m_componentsChecksum;
		}
		return this.m_bodyList;
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00009754 File Offset: 0x00007B54
	public void AddSpeed(Vector2 _force, float _speedLimit)
	{
		List<IComponent> unitBodyList = this.GetUnitBodyList();
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < unitBodyList.Count; i++)
		{
			ChipmunkBodyC chipmunkBodyC = unitBodyList[i] as ChipmunkBodyC;
			vector += ChipmunkProWrapper.ucpBodyGetVel(chipmunkBodyC.body);
		}
		if (unitBodyList.Count > 0)
		{
			vector /= (float)unitBodyList.Count;
			if ((vector + _force).magnitude < _speedLimit)
			{
				for (int j = 0; j < unitBodyList.Count; j++)
				{
					ChipmunkBodyC chipmunkBodyC2 = unitBodyList[j] as ChipmunkBodyC;
					ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(chipmunkBodyC2.body, _force * chipmunkBodyC2.m_mass, ChipmunkProWrapper.ucpBodyGetPos(chipmunkBodyC2.body));
				}
			}
		}
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00009824 File Offset: 0x00007C24
	public Vector3 GetUnitCenterPosition()
	{
		Vector2 vector = Vector2.zero;
		List<IComponent> unitBodyList = this.GetUnitBodyList();
		for (int i = 0; i < unitBodyList.Count; i++)
		{
			ChipmunkBodyC chipmunkBodyC = unitBodyList[i] as ChipmunkBodyC;
			vector += ChipmunkProWrapper.ucpBodyGetPos(chipmunkBodyC.body);
		}
		vector /= (float)unitBodyList.Count;
		return vector;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00009888 File Offset: 0x00007C88
	public bool isInsideGround()
	{
		bool flag = false;
		List<IComponent> unitBodyList = this.GetUnitBodyList();
		for (int i = 0; i < unitBodyList.Count; i++)
		{
			ChipmunkBodyC chipmunkBodyC = unitBodyList[i] as ChipmunkBodyC;
			for (int j = 0; j < chipmunkBodyC.shapes.Count; j++)
			{
				IntPtr shapePtr = chipmunkBodyC.shapes[j].shapePtr;
				int layerAtWorldPos = AutoGeometryManager.GetLayerAtWorldPos(((!this.m_minigame.m_editing) ? ChipmunkProWrapper.ucpShapeGetBB(shapePtr) : ChipmunkProWrapper.ucpShapeCacheBB(shapePtr)).getCenter(), 64);
				if (layerAtWorldPos >= 0)
				{
					flag = true;
				}
			}
		}
		return flag;
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00009938 File Offset: 0x00007D38
	public void MoveUnitRandomly(float _amount)
	{
		Vector2 vector;
		vector..ctor(Random.Range(-_amount, _amount), Random.Range(-_amount, _amount));
		List<IComponent> unitBodyList = this.GetUnitBodyList();
		for (int i = 0; i < unitBodyList.Count; i++)
		{
			ChipmunkBodyC chipmunkBodyC = unitBodyList[i] as ChipmunkBodyC;
			Vector2 vector2 = ChipmunkProWrapper.ucpBodyGetPos(chipmunkBodyC.body) + vector;
			ChipmunkProWrapper.ucpBodySetPos(chipmunkBodyC.body, vector2);
		}
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x000099A8 File Offset: 0x00007DA8
	public void SetAsTeleporting(int _ticks, Transform _from, Transform _to, bool _rotateInFade = true, bool _modifyOutAngle = false, bool _goalTeleport = false, bool _playDefaultSound = true)
	{
		this.m_teleportStartPos = _from.position;
		this.m_teleportEndPos = _to.position;
		this.m_teleportEndPos.z = 0f;
		this.m_centerMoveDelta = Vector3.zero;
		this.m_teleportOutAngle = 0f;
		Vector2 vector = Vector2.zero;
		if (_playDefaultSound)
		{
			SoundS.PlaySingleShot("/InGame/Units/PortalTeleport_In", _from.position, 1f);
		}
		List<IComponent> unitBodyList = this.GetUnitBodyList();
		this.m_teleportOutAngVels = new float[unitBodyList.Count];
		this.m_teleportOutVels = new Vector2[this.m_bodyList.Count];
		Vector2 vector2 = Vector2.zero;
		for (int i = 0; i < unitBodyList.Count; i++)
		{
			ChipmunkBodyC chipmunkBodyC = unitBodyList[i] as ChipmunkBodyC;
			vector += ChipmunkProWrapper.ucpBodyGetVel(chipmunkBodyC.body);
			vector2 += ChipmunkProWrapper.ucpBodyGetPos(chipmunkBodyC.body);
			this.m_teleportOutVels[i] = ChipmunkProWrapper.ucpBodyGetVel(chipmunkBodyC.body);
			this.m_teleportOutAngVels[i] = ChipmunkProWrapper.ucpBodyGetAngVel(chipmunkBodyC.body);
		}
		vector2 /= (float)unitBodyList.Count;
		this.m_centerMoveDelta = _from.position - vector2;
		this.m_centerMoveDelta /= (float)_ticks;
		float num = vector.magnitude;
		num /= (float)unitBodyList.Count;
		if (_modifyOutAngle)
		{
			float num2 = 1f;
			Vector3 vector3 = (((_to.rotation.eulerAngles.z >= 180f || _to.rotation.eulerAngles.z <= 0f) && (_to.rotation.eulerAngles.z >= -180f || _to.rotation.eulerAngles.z <= -360f)) ? Vector3.right : (-Vector3.right));
			if (Vector3.Cross(vector3, _to.up).z < 0f)
			{
				num2 = -1f;
			}
			Vector2 vector4 = num * _to.up;
			if ((Mathf.Abs(_to.rotation.eulerAngles.z) > 25f && Mathf.Abs(_to.rotation.z) < 155f) || (Mathf.Abs(_to.rotation.z) < 335f && Mathf.Abs(_to.rotation.eulerAngles.z) > 205f))
			{
				this.m_teleportEndPos += _to.right * vector3.x * 40f;
				this.m_teleportOutAngle = Vector3.Angle(vector3, _to.up) * num2 * 0.017453292f;
			}
			if (num <= 700f)
			{
				num += 300f;
			}
			for (int j = 0; j < this.m_teleportOutVels.Length; j++)
			{
				this.m_teleportOutVels[j] = _to.up * num;
			}
		}
		this.m_originalTeleTicks = _ticks;
		this.m_teleportingTicks = _ticks;
		this.m_teleportingState = 0;
		this.m_teleportModifyAngle = _modifyOutAngle;
		this.m_teleportFadeRotate = _rotateInFade;
		this.m_goalTeleport = _goalTeleport;
		this.m_teleTweenStyle = TweenStyle.QuadIn;
		this.m_teleTweenStartValue = 0f;
		this.m_teleTweenEndValue = 360f;
		this.m_currentTeleTime = 0f;
		this.m_teleTweenDuration = (float)_ticks / 60f;
		EntityManager.SetActivityOfEntity(this.m_entity, false, false, true, false, true, true);
		List<IComponent> list = EntityManager.GetComponentsByEntity(ComponentType.CameraTarget, this.m_entity);
		for (int k = 0; k < list.Count; k++)
		{
			CameraTargetC cameraTargetC = list[k] as CameraTargetC;
			cameraTargetC.m_active = true;
			cameraTargetC.frameTC.m_active = true;
		}
		this.m_isTeleporting = true;
		this.m_teleported = true;
		list = EntityManager.GetComponentsByEntity(ComponentType.Transform, this.m_entity);
		this.m_teleportTransform = TransformS.AddComponent(this.m_entity, vector2);
		foreach (IComponent component in list)
		{
			TransformC transformC = (TransformC)component;
			if (transformC.parent == null)
			{
				TransformS.ParentComponent(transformC, this.m_teleportTransform);
			}
		}
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00009E88 File Offset: 0x00008288
	public void TeleportUpdate()
	{
		if (this.m_isTeleporting)
		{
			TransformC teleportTransform = this.m_teleportTransform;
			if (teleportTransform != null)
			{
				this.m_currentTeleTime += Main.m_gameDeltaTime;
				this.m_teleTweenCurrentValue = TweenS.tween(this.m_teleTweenStyle, this.m_currentTeleTime, this.m_teleTweenDuration, this.m_teleTweenStartValue, this.m_teleTweenEndValue);
				switch (this.m_teleportingState)
				{
				case 0:
				{
					TransformS.SetGlobalPosition(teleportTransform, teleportTransform.transform.position + this.m_centerMoveDelta);
					if (this.m_teleportFadeRotate)
					{
						TransformS.SetGlobalRotation(teleportTransform, Vector3.forward * this.m_teleTweenCurrentValue);
					}
					float num = 0.1f + (float)this.m_teleportingTicks / (float)this.m_originalTeleTicks * 0.9f;
					TransformS.SetScale(teleportTransform, num);
					this.m_teleportingTicks--;
					if (this.m_teleportingTicks <= 0)
					{
						this.m_teleportingState++;
						if (this.m_goalTeleport)
						{
							this.m_teleportingState = 3;
							EntityManager.SetActivityOfEntity(this.m_entity, false, true, true, true, true, true);
						}
						this.m_teleportingTicks = this.m_originalTeleTicks;
						Vehicle vehicle = this as Vehicle;
						if (vehicle != null)
						{
							if (!this.m_goalTeleport)
							{
								PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.TeleportStart, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
							}
							if (vehicle.m_trailBase != null)
							{
								vehicle.m_trailBase.SetTeleporting(true);
							}
						}
					}
					break;
				}
				case 1:
				{
					TransformS.SetGlobalPosition(teleportTransform, this.m_teleportEndPos);
					this.m_teleTweenStyle = TweenStyle.QuadOut;
					this.m_currentTeleTime = 0f;
					this.m_teleTweenStartValue = 0f;
					this.m_teleTweenEndValue = 360f + 57.29578f * this.m_teleportOutAngle;
					this.m_teleportingState++;
					List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.CameraTarget, this.m_entity);
					for (int i = 0; i < componentsByEntity.Count; i++)
					{
						CameraTargetC cameraTargetC = componentsByEntity[i] as CameraTargetC;
						CameraS.ResetFramePosition(cameraTargetC);
						if (CameraS.isPrimaryTarget(cameraTargetC))
						{
							CameraS.SnapMainCameraPos(this.m_teleportEndPos);
						}
					}
					break;
				}
				case 2:
				{
					if (this.m_teleportFadeRotate)
					{
						TransformS.SetGlobalRotation(teleportTransform, Vector3.forward * this.m_teleTweenCurrentValue);
					}
					else
					{
						TransformS.SetGlobalRotation(teleportTransform, Vector3.forward * 57.29578f * this.m_teleportOutAngle);
					}
					float num2 = 1f - (float)this.m_teleportingTicks / (float)this.m_originalTeleTicks * 0.9f;
					TransformS.SetScale(teleportTransform, num2);
					this.m_teleportingTicks--;
					if (this.m_teleportingTicks <= 0)
					{
						this.m_teleportingState++;
					}
					break;
				}
				case 3:
					if (!this.m_goalTeleport)
					{
						TransformS.SetScale(teleportTransform, 1f);
						SoundS.PlaySingleShot("/InGame/Units/PortalTeleport_Out", this.m_teleportTransform.transform.position, 1f);
						TransformS.RemoveComponent(this.m_teleportTransform);
						this.m_teleportTransform = null;
						EntityManager.SetActivityOfEntity(this.m_entity, true, false, true, false, true, true);
						List<IComponent> componentsByEntity2 = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, this.m_entity);
						Vector3 vector;
						vector..ctor(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
						for (int j = 0; j < componentsByEntity2.Count; j++)
						{
							ChipmunkBodyC chipmunkBodyC = componentsByEntity2[j] as ChipmunkBodyC;
							ChipmunkProWrapper.ucpBodySetPos(chipmunkBodyC.body, chipmunkBodyC.TC.transform.position + vector);
							if (this.m_teleportModifyAngle)
							{
								ChipmunkProWrapper.ucpBodySetAngle(chipmunkBodyC.body, this.m_teleportOutAngle);
							}
							ChipmunkProWrapper.ucpBodySetVel(chipmunkBodyC.body, this.m_teleportOutVels[j]);
							ChipmunkProWrapper.ucpBodySetAngVel(chipmunkBodyC.body, this.m_teleportOutAngVels[j]);
						}
						this.m_isTeleporting = false;
						Vehicle vehicle2 = this as Vehicle;
						if (vehicle2 != null)
						{
							PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.TeleportEnd, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
							if (vehicle2.m_trailBase != null)
							{
								vehicle2.m_trailBase.SetTeleporting(false);
							}
						}
					}
					break;
				}
			}
		}
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0000A2F1 File Offset: 0x000086F1
	public virtual void InactiveUpdate()
	{
		if (!PsState.m_physicsPaused && this.m_entity != null)
		{
			this.TeleportUpdate();
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0000A310 File Offset: 0x00008710
	public virtual void Update()
	{
		if (!this.m_minigame.m_editing)
		{
			if (!this.m_minigame.m_editing && this.m_entity != null)
			{
				if (this.m_affectingBlackHoles != null && this.m_affectingBlackHoles.Count > 0)
				{
					Vector2 vector = Vector2.zero;
					float num = 1f;
					for (int i = 0; i < this.m_affectingBlackHoles.Count; i++)
					{
						PsBlackHole psBlackHole = this.m_affectingBlackHoles[i];
						Vector2 vector2 = this.GetUnitCenterPosition() - psBlackHole.m_tc.transform.transform.position;
						float num2 = vector2.magnitude / (500f * psBlackHole.m_pullRadiusMultiplier);
						float num3 = ToolBox.getPositionBetween(Mathf.Min(0.21f, Mathf.Max(1f - num2, 0.12f)), 0f, 0.21f);
						if (psBlackHole.m_isWhiteHole)
						{
							num3 *= -1f;
						}
						vector += vector2.normalized * PsState.m_defaultGravity.y * num3;
						if (num > num2)
						{
							num = num2;
						}
					}
					vector += this.m_minigame.m_globalGravity * (float)this.m_minigame.m_gravityMultipler * Mathf.Pow(num, 8f);
					this.m_gravityAttracted = true;
					this.SetGravity(vector);
				}
				else if (this.m_gravityAttracted)
				{
					this.m_gravityAttracted = false;
					this.SetGravity(this.m_minigame.m_globalGravity * (float)this.m_minigame.m_gravityMultipler);
				}
				this.shieldModifiersChanged = false;
				for (int j = this.m_shieldModifiers.Count - 1; j > -1; j--)
				{
					if (this.m_shieldModifiers[j].m_endTime != -1f && this.m_shieldModifiers[j].m_endTime < Main.m_resettingGameTime)
					{
						this.m_shieldModifiers.RemoveAt(j);
						this.shieldModifiersChanged = true;
					}
				}
				if (this.shieldModifiersChanged)
				{
					this.CalculateCurrentShield();
				}
				this.armorModifiersChanged = false;
				for (int k = this.m_armorModifiers.Count - 1; k > -1; k--)
				{
					if (this.m_armorModifiers[k].m_endTime != -1f && this.m_armorModifiers[k].m_endTime < Main.m_resettingGameTime)
					{
						this.m_armorModifiers.RemoveAt(k);
						this.armorModifiersChanged = true;
					}
				}
				if (this.armorModifiersChanged)
				{
					this.CalculateCurrentArmor();
				}
				for (int l = this.m_buffs.Count - 1; l > -1; l--)
				{
					this.HandleBuff(this.m_buffs[l]);
				}
				for (int m = this.m_debuffs.Count - 1; m > -1; m--)
				{
					this.HandleBuff(this.m_debuffs[m]);
				}
				int num4 = 0;
				int num5 = -1;
				int num6 = -1;
				Vector2 vector3 = Vector2.zero;
				for (int n = this.m_contacts.Count - 1; n > -1; n--)
				{
					ContactInfo contactInfo = this.m_contacts[n];
					bool flag = contactInfo.m_cmb.m_identifier == 1;
					if (contactInfo.m_began)
					{
						this.HandleContactBegin(contactInfo);
					}
					else if (contactInfo.m_end && contactInfo.m_endTime < Main.m_resettingGameTime - this.m_contactEndTreshold)
					{
						this.HandleContactEnd(contactInfo);
						this.m_contacts.Remove(contactInfo);
						flag = false;
					}
					if (flag)
					{
						num4++;
					}
					if (contactInfo.m_ground != null)
					{
						num5 = 0;
						if (contactInfo.m_ground.m_isElectrified)
						{
							num6 = 0;
							vector3 = contactInfo.m_contactPoint;
						}
					}
					else if (contactInfo.m_cmb != null && contactInfo.m_cmb.body != IntPtr.Zero && ChipmunkProWrapper.ucpBodyGetType(contactInfo.m_cmb.body) != ucpBodyType.DYNAMIC)
					{
						num5 = 0;
						if (contactInfo.m_unit.m_isElectrified && contactInfo.m_cmb.m_identifier == 1337)
						{
							num6 = 0;
							vector3 = contactInfo.m_contactPoint;
						}
					}
					else if (contactInfo.m_unit != null)
					{
						if (contactInfo.m_unit.m_groundedChainIndex > -1)
						{
							if (num5 == -1 && (contactInfo.m_unit.m_groundedChainIndex < this.m_groundedChainIndex || this.m_groundedChainIndex == -1))
							{
								num5 = contactInfo.m_unit.m_groundedChainIndex + 1;
							}
							else if (contactInfo.m_unit.m_groundedChainIndex < num5)
							{
								num5 = contactInfo.m_unit.m_groundedChainIndex + 1;
							}
						}
						if (contactInfo.m_unit.m_electricChainIndex > -1)
						{
							if (num6 == -1 && (contactInfo.m_unit.m_electricChainIndex < this.m_electricChainIndex || this.m_electricChainIndex == -1))
							{
								num6 = contactInfo.m_unit.m_electricChainIndex + 1;
								vector3 = contactInfo.m_contactPoint;
							}
							else if (contactInfo.m_unit.m_electricChainIndex < num6)
							{
								num6 = contactInfo.m_unit.m_electricChainIndex + 1;
								vector3 = contactInfo.m_contactPoint;
							}
						}
					}
				}
				if (num5 < 20)
				{
					this.m_groundedChainIndex = num5;
				}
				else
				{
					this.m_groundedChainIndex = -1;
				}
				if (this.m_canElectrify && num6 < 20)
				{
					this.m_electricChainIndex = num6;
				}
				else
				{
					this.m_electricChainIndex = -1;
				}
				if (this.m_contacts.Count - num4 == 0)
				{
					this.m_contactState = ContactState.OnAir;
					this.m_lastGroundContact = Main.m_resettingGameTime;
				}
				else
				{
					this.m_contactState = ContactState.OnContact;
				}
				if (this.m_electricChainIndex > -1)
				{
					if (!this.m_isElectrified && this.m_canElectrify)
					{
						this.SetElectrified(true, vector3);
					}
				}
				else if (this.m_isElectrified && this.m_canElectrify)
				{
					this.SetElectrified(false, vector3);
				}
				if (this.m_isElectrified)
				{
					this.m_electrificationTime += Main.m_gameDeltaTime;
				}
				if (this.m_checkForCrushing)
				{
					this.CheckCrush();
				}
			}
		}
		this.m_immuneToGroundEffects = false;
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x0000A99C File Offset: 0x00008D9C
	protected virtual bool IsImmuneToGroundEffects(ContactInfo _ci)
	{
		return false;
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x0000A9A0 File Offset: 0x00008DA0
	private void HandleBuff(BuffInfo _bi)
	{
		if (_bi.m_new)
		{
			if (_bi.m_buff.m_shieldModifier != null)
			{
				this.ModifyShield(_bi.m_buff.m_shieldModifier);
			}
			if (_bi.m_buff.m_armorModifier != null)
			{
				this.ModifyArmor(_bi.m_buff.m_armorModifier);
			}
			this.ApplyBuffBegan(_bi);
		}
		else if (_bi.m_buff.m_duration >= 0f && _bi.m_lastTick < Main.m_resettingGameTime)
		{
			this.RemoveBuff(_bi, true);
		}
		else if (_bi.m_buff.m_interval >= 0f && _bi.m_nextTick < Main.m_resettingGameTime)
		{
			this.ApplyBuffTick(_bi);
			_bi.m_nextTick += _bi.m_buff.m_interval;
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0000AA7C File Offset: 0x00008E7C
	private void HandleContactBegin(ContactInfo ci)
	{
		ci.m_began = false;
		bool flag = ci.m_ground != null;
		bool flag2 = ci.m_unit != null;
		if (flag)
		{
			this.GroundContactStart(ci);
		}
		else if (flag2)
		{
			this.UnitContactStart(ci);
		}
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x0000AACC File Offset: 0x00008ECC
	private void HandleContactEnd(ContactInfo ci)
	{
		bool flag = ci.m_ground != null;
		bool flag2 = ci.m_unit != null;
		if (flag)
		{
			this.GroundContactEnd(ci);
		}
		else if (flag2)
		{
			this.UnitContactEnd(ci);
		}
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0000AB14 File Offset: 0x00008F14
	private void CheckCrush()
	{
		if (this.m_contacts.Count < 2)
		{
			return;
		}
		List<IComponent> unitBodyList = this.GetUnitBodyList();
		for (int i = 0; i < unitBodyList.Count; i++)
		{
			ChipmunkBodyC chipmunkBodyC = unitBodyList[i] as ChipmunkBodyC;
			if (!chipmunkBodyC.m_isSleeping)
			{
				float magnitude = ChipmunkProWrapper.ucpBodyGetForceVectorSum(chipmunkBodyC.body).magnitude;
				float num = ChipmunkProWrapper.ucpBodyGetForceMagnitudeSum(chipmunkBodyC.body);
				if (num > (float)this.m_crushTolerance)
				{
					float num2 = magnitude / num;
					if (!float.IsNaN(num2))
					{
						float num3 = 1f - num2;
						if (num3 > 0.8f)
						{
							this.m_crushFramesCount++;
						}
						if (this.m_crushFramesCount > 2)
						{
							this.EmergencyKill();
							return;
						}
					}
				}
				else if (num < (float)this.m_crushTolerance * 0.5f)
				{
					this.m_crushFramesCount = 0;
				}
			}
		}
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x0000AC04 File Offset: 0x00009004
	public Vector2 GetVelocityRelativeToContacts(ChipmunkBodyC _body)
	{
		Vector2 vector = Vector2.zero;
		int num = 0;
		Vector2 vector2 = ChipmunkProWrapper.ucpBodyGetPos(_body.body);
		if (this.m_contactState == ContactState.OnContact)
		{
			for (int i = this.m_contacts.Count - 1; i > -1; i--)
			{
				if (this.m_contacts[i].m_contactBody == _body && this.m_contacts[i].m_cmb != null)
				{
					Vector2 vector3 = ChipmunkProWrapper.ucpBodyGetVelAtWorldPoint(this.m_contacts[i].m_cmb.body, vector2);
					vector += vector3;
					num++;
				}
			}
		}
		if (num == 0)
		{
			return ChipmunkProWrapper.ucpBodyGetVel(_body.body);
		}
		vector /= (float)num;
		return ChipmunkProWrapper.ucpBodyGetVel(_body.body) - vector;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x0000ACD4 File Offset: 0x000090D4
	public int GetGroundContactsCount(ChipmunkBodyC _body)
	{
		int num = 0;
		for (int i = this.m_contacts.Count - 1; i > -1; i--)
		{
			if (this.m_contacts[i].m_ground != null && this.m_contacts[i].m_contactBody == _body)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x0000AD34 File Offset: 0x00009134
	public int GetDynamicContactsCount(ChipmunkBodyC _body, bool _discardEnded = false)
	{
		int num = 0;
		for (int i = this.m_contacts.Count - 1; i > -1; i--)
		{
			if (this.m_contacts[i].m_cmb != null && this.m_contacts[i].m_contactBody == _body && ChipmunkProWrapper.ucpBodyGetType(this.m_contacts[i].m_cmb.body) == ucpBodyType.DYNAMIC && (!_discardEnded || (_discardEnded && !this.m_contacts[i].m_end)))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0000ADD8 File Offset: 0x000091D8
	public int GetRogueContactsCount(ChipmunkBodyC _body, bool _includeStatics)
	{
		int num = 0;
		for (int i = this.m_contacts.Count - 1; i > -1; i--)
		{
			if (this.m_contacts[i].m_cmb != null && this.m_contacts[i].m_contactBody == _body && ChipmunkProWrapper.ucpBodyGetType(this.m_contacts[i].m_cmb.body) == ucpBodyType.KINEMATIC)
			{
				bool flag = ChipmunkProWrapper.ucpBodyGetType(this.m_contacts[i].m_cmb.body) == ucpBodyType.STATIC;
				if (_includeStatics || !flag)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0000AE84 File Offset: 0x00009284
	public virtual void AddBuff(Buff _buff, IAssembledClass _source, Vector2 _contactPoint)
	{
		if (_buff == null || this.m_isDead)
		{
			return;
		}
		List<BuffInfo> list = ((!_buff.m_isDebuff) ? this.m_buffs : this.m_debuffs);
		BuffInfo buffInfo = null;
		bool flag = true;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].m_buff == _buff)
			{
				buffInfo = list[i];
				flag = false;
				break;
			}
		}
		if (buffInfo == null)
		{
			buffInfo = new BuffInfo(_buff, _source, this, _contactPoint);
			list.Add(buffInfo);
		}
		else
		{
			buffInfo.m_contactPoint = _contactPoint;
		}
		if (flag)
		{
			if (_buff.m_duration >= 0f)
			{
				buffInfo.m_lastTick = Main.m_resettingGameTime + _buff.m_duration;
			}
			else
			{
				buffInfo.m_lastTick = -1f;
			}
			if (_buff.m_tickEffect != null && _buff.m_interval >= 0f)
			{
				buffInfo.m_nextTick = Main.m_resettingGameTime + _buff.m_interval;
			}
			else
			{
				buffInfo.m_nextTick = -1f;
			}
			this.HandleBuff(buffInfo);
		}
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x0000AF9C File Offset: 0x0000939C
	public virtual void ApplyBuffBegan(BuffInfo _bi)
	{
		_bi.m_new = false;
		if (this.m_isDead || _bi.m_buff.m_beganEffect == null)
		{
			return;
		}
		_bi.m_buff.PlayEffects(this, BuffState.Began, _bi.m_contactPoint);
		if (_bi.m_buff.m_isDebuff)
		{
			this.Damage(_bi.m_buff.m_beganEffect, 1f, null);
		}
		else
		{
			this.Heal(_bi.m_buff.m_beganEffect, 1);
		}
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0000B020 File Offset: 0x00009420
	public virtual void ApplyBuffTick(BuffInfo _bi)
	{
		if (this.m_isDead || _bi.m_buff.m_tickEffect == null)
		{
			return;
		}
		_bi.m_buff.PlayEffects(this, BuffState.Tick, this.GetUnitCenterPosition());
		if (_bi.m_buff.m_isDebuff)
		{
			this.Damage(_bi.m_buff.m_tickEffect, 1f, null);
		}
		else
		{
			this.Heal(_bi.m_buff.m_tickEffect, 1);
		}
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x0000B0A0 File Offset: 0x000094A0
	public virtual void ApplyBuffEnd(BuffInfo _bi)
	{
		if (this.m_isDead || _bi.m_buff.m_endEffect == null)
		{
			return;
		}
		if (_bi.m_buff.m_isDebuff)
		{
			this.Damage(_bi.m_buff.m_endEffect, 1f, null);
		}
		else
		{
			this.Heal(_bi.m_buff.m_endEffect, 1);
		}
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x0000B108 File Offset: 0x00009508
	public virtual void RemoveBuff(BuffInfo _buffInfo, bool _applyEndEffect)
	{
		List<BuffInfo> list = ((!_buffInfo.m_buff.m_isDebuff) ? this.m_buffs : this.m_debuffs);
		if (_buffInfo.m_buff.m_endEffect != null && _applyEndEffect)
		{
			this.ApplyBuffEnd(_buffInfo);
		}
		_buffInfo.m_buff.PlayEffects(this, BuffState.End, this.GetUnitCenterPosition());
		if (_buffInfo.m_buff.m_shieldModifier != null)
		{
			this.RemoveStatModifier(_buffInfo.m_buff.m_shieldModifier, this.m_shieldModifiers);
			this.CalculateCurrentShield();
		}
		if (_buffInfo.m_buff.m_armorModifier != null)
		{
			this.RemoveStatModifier(_buffInfo.m_buff.m_armorModifier, this.m_armorModifiers);
			this.CalculateCurrentArmor();
		}
		list.Remove(_buffInfo);
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x0000B1D0 File Offset: 0x000095D0
	public virtual void RemoveBuff(Buff _buff)
	{
		Debug.LogWarning("remove: " + _buff.m_identifier);
		for (int i = this.m_buffs.Count; i > -1; i--)
		{
			if (this.m_buffs[i].m_buff == _buff)
			{
				this.RemoveBuff(this.m_buffs[i], false);
			}
		}
		for (int j = this.m_debuffs.Count; j > -1; j--)
		{
			if (this.m_debuffs[j].m_buff == _buff)
			{
				this.RemoveBuff(this.m_debuffs[j], false);
			}
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0000B280 File Offset: 0x00009680
	public virtual void RemoveStatModifier(StatModifier _modifier, List<StatModifierInfo> _list)
	{
		for (int i = _list.Count - 1; i > -1; i--)
		{
			if (_list[i].m_modifier == _modifier)
			{
				_list.RemoveAt(i);
			}
		}
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x0000B2C0 File Offset: 0x000096C0
	public virtual void ModifyShield(StatModifier _modifier)
	{
		if (_modifier == null || this.m_isDead)
		{
			return;
		}
		StatModifierInfo statModifierInfo = null;
		for (int i = 0; i < this.m_shieldModifiers.Count; i++)
		{
			if (this.m_shieldModifiers[i].m_modifier == _modifier)
			{
				statModifierInfo = this.m_shieldModifiers[i];
				break;
			}
		}
		if (statModifierInfo == null)
		{
			statModifierInfo = new StatModifierInfo(_modifier);
			statModifierInfo.m_stack = 1;
			this.m_shieldModifiers.Add(statModifierInfo);
		}
		else if (statModifierInfo.m_stack < statModifierInfo.m_maxStack)
		{
			statModifierInfo.m_stack++;
		}
		if (statModifierInfo.m_modifier.m_duration >= 0f)
		{
			statModifierInfo.m_endTime = Main.m_resettingGameTime + statModifierInfo.m_modifier.m_duration;
		}
		else
		{
			statModifierInfo.m_endTime = -1f;
		}
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x0000B3A8 File Offset: 0x000097A8
	public virtual void ModifyArmor(StatModifier _modifier)
	{
		if (_modifier == null)
		{
			return;
		}
		StatModifierInfo statModifierInfo = null;
		for (int i = 0; i < this.m_armorModifiers.Count; i++)
		{
			if (this.m_armorModifiers[i].m_modifier == _modifier)
			{
				statModifierInfo = this.m_armorModifiers[i];
				break;
			}
		}
		if (statModifierInfo == null)
		{
			statModifierInfo = new StatModifierInfo(_modifier);
			statModifierInfo.m_stack = 1;
			this.m_armorModifiers.Add(statModifierInfo);
		}
		else if (statModifierInfo.m_stack < statModifierInfo.m_maxStack)
		{
			statModifierInfo.m_stack++;
		}
		if (statModifierInfo.m_modifier.m_duration >= 0f)
		{
			statModifierInfo.m_endTime = Main.m_resettingGameTime + statModifierInfo.m_modifier.m_duration;
		}
		else
		{
			statModifierInfo.m_endTime = -1f;
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x0000B484 File Offset: 0x00009884
	public virtual void CalculateCurrentShield()
	{
		for (int i = 0; i < this.m_currentShield.Length; i++)
		{
			this.m_currentShield[i] = this.m_baseShield[i];
			if (this.m_currentShield[i] > -1f)
			{
				for (int j = 0; j < this.m_shieldModifiers.Count; j++)
				{
					this.m_currentShield[i] *= this.m_shieldModifiers[j].m_modifier.m_multipler;
				}
			}
		}
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x0000B510 File Offset: 0x00009910
	public virtual void CalculateCurrentArmor()
	{
		for (int i = 0; i < this.m_currentArmor.Length; i++)
		{
			this.m_currentArmor[i] = this.m_baseArmor[i];
			if (this.m_currentArmor[i] > -1f)
			{
				for (int j = 0; j < this.m_armorModifiers.Count; j++)
				{
					this.m_currentArmor[i] *= this.m_armorModifiers[j].m_modifier.m_multipler;
				}
			}
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x0000B59C File Offset: 0x0000999C
	public virtual void Heal(Damage _health, int _multipler = 1)
	{
		if (_health == null || this.m_isDead)
		{
			return;
		}
		float[] amount = _health.m_amount;
		float num = 0f;
		for (int i = 0; i < amount.Length; i++)
		{
			num += amount[i];
		}
		this.m_hitPoints += num * (float)_multipler;
		if (this.m_hitPoints > this.m_maxHitPoints)
		{
			this.m_hitPoints = this.m_maxHitPoints;
		}
		this.Nurture(num * (float)_multipler);
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x0000B61C File Offset: 0x00009A1C
	public virtual void Damage(Damage _damage, float _multiplier = 1f, Unit _source = null)
	{
		Vehicle vehicle = this as Vehicle;
		if (_damage == null || this.m_isDead || (vehicle != null && PsState.m_activeMinigame.m_playerReachedGoal))
		{
			return;
		}
		float[] amount = _damage.m_amount;
		int num = 0;
		float num2 = 0f;
		float num3 = 0f;
		for (int i = 0; i < amount.Length; i++)
		{
			if (this.m_currentArmor[i] != -1f && this.m_currentShield[i] != -1f && amount[i] != 0f)
			{
				float num4 = amount[i] * _multiplier;
				num4 -= this.m_currentShield[i];
				num4 -= this.m_currentArmor[i];
				if (num4 > 0f)
				{
					num3 += num4;
					if (num4 > num2)
					{
						num2 = num4;
						num = i;
					}
				}
			}
		}
		if (num3 > 0f)
		{
			if (this.m_hitPointType == HitPointType.Lives)
			{
				this.m_hitPoints -= 1f;
			}
			else
			{
				this.m_hitPoints -= num3;
			}
			if (this.m_hitPoints <= 0f)
			{
				this.Kill((DamageType)num, num3);
			}
			else
			{
				this.Wound((DamageType)num, num3);
			}
		}
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x0000B764 File Offset: 0x00009B64
	public virtual void Wound(DamageType _damageType, float _totalDamage)
	{
		Debug.Log(string.Concat(new object[] { this.m_name, " - Damage: ", _totalDamage, ", Hit Points: ", this.m_hitPoints, "/", this.m_maxHitPoints }), null);
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x0000B7C8 File Offset: 0x00009BC8
	public virtual void Nurture(float _totalHeal)
	{
		Debug.Log(string.Concat(new object[] { this.m_name, " - Heal: ", _totalHeal, ", Hit Points: ", this.m_hitPoints, "/", this.m_maxHitPoints }), null);
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x0000B82C File Offset: 0x00009C2C
	public virtual void Kill(DamageType _damageType, float _totalDamage)
	{
		this.m_isDead = true;
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0000B835 File Offset: 0x00009C35
	public virtual void EmergencyKill()
	{
		this.m_isDead = true;
		this.Destroy();
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x0000B844 File Offset: 0x00009C44
	public uint GetGroup()
	{
		return (uint)((this.m_entity.m_index + 1) * 100);
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x0000B856 File Offset: 0x00009C56
	public virtual void GroundContactStart(ContactInfo _contactInfo)
	{
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x0000B858 File Offset: 0x00009C58
	public virtual void GroundContactEnd(ContactInfo _contactInfo)
	{
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x0000B85A File Offset: 0x00009C5A
	public virtual void UnitContactStart(ContactInfo _contactInfo)
	{
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x0000B85C File Offset: 0x00009C5C
	public virtual void UnitContactEnd(ContactInfo _contactInfo)
	{
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0000B85E File Offset: 0x00009C5E
	public virtual void SetBurning(bool _burn)
	{
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0000B860 File Offset: 0x00009C60
	public virtual void SetFrozen(bool _burn)
	{
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0000B864 File Offset: 0x00009C64
	public virtual void SetElectrified(bool _electrify, Vector2 _contactPoint)
	{
		if (_electrify)
		{
			Damage damage = new Damage(DamageType.Electric, float.MaxValue);
			this.Damage(damage, 1f, null);
			if (this.m_lastElectrification + 0.25f < Main.m_resettingGameTime)
			{
				EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.ElectricBurst_GameObject), _contactPoint, Vector3.zero, 3f, "GTAG_AUTODESTROY", null);
				SoundS.PlaySingleShot("/Ingame/ElectricZap", _contactPoint, 1f);
				Debug.Log("ElectricZap", null);
			}
			this.m_isElectrified = true;
			this.m_lastElectrification = Main.m_resettingGameTime;
		}
		else
		{
			this.m_isElectrified = false;
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x0000B90A File Offset: 0x00009D0A
	public virtual void CreateUpgradeFX()
	{
		EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.GeneralUpgrade_GameObject), base.m_graphElement.m_position, Vector3.zero, 5f, "GTAG_AUTODESTROY", null);
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x0000B937 File Offset: 0x00009D37
	public virtual List<float> ParseUpgradeValues(Hashtable _upgrades)
	{
		return null;
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x0000B93A File Offset: 0x00009D3A
	public virtual Hashtable GetUpgradeValues()
	{
		return null;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x0000B93D File Offset: 0x00009D3D
	public virtual List<string> GetNodeUpgradeNames()
	{
		return null;
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x0000B940 File Offset: 0x00009D40
	public virtual List<KeyValuePair<string, int>> GetUpgrades()
	{
		return null;
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x0000B944 File Offset: 0x00009D44
	public virtual int GetTier(List<int> _upgradeValues)
	{
		int num = 99999999;
		for (int i = 0; i < _upgradeValues.Count; i++)
		{
			if (_upgradeValues[i] < num)
			{
				num = _upgradeValues[i];
			}
		}
		return Mathf.FloorToInt((float)num / 5f) + 1;
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x0000B994 File Offset: 0x00009D94
	public virtual void AdjustTiers(ref List<int> _upgradeValues)
	{
		bool flag = false;
		int num = this.GetTier(_upgradeValues) - 1;
		int num2 = num;
		for (int i = 0; i < _upgradeValues.Count; i++)
		{
			int num3 = Mathf.FloorToInt((float)_upgradeValues[i] / 5f);
			int num4 = _upgradeValues[i] - num3 * 5;
			if (num4 > 0 && num3 > num2)
			{
				num2 = num3;
				flag = true;
			}
			else if (num4 == 0 && num3 - num > 1 && num3 > num2)
			{
				num2 = num3 - 1;
				flag = true;
			}
		}
		if (flag)
		{
			for (int j = 0; j < _upgradeValues.Count; j++)
			{
				int num5 = Mathf.FloorToInt((float)_upgradeValues[j] / 5f);
				if (num5 < num2)
				{
					_upgradeValues[j] = num2 * 5;
				}
			}
		}
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x0000BA74 File Offset: 0x00009E74
	public int ToInt(object _value)
	{
		return Convert.ToInt32(_value);
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x0000BA7C File Offset: 0x00009E7C
	public float ToFloat(object _value)
	{
		return Convert.ToSingle(_value);
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x0000BA84 File Offset: 0x00009E84
	public virtual float UpgradeLevelToValue(int _level, int[] _ranges, float[] _values)
	{
		if (_level <= _ranges[0])
		{
			return _values[0];
		}
		if (_level >= _ranges[_ranges.Length - 1])
		{
			return _values[_values.Length - 1];
		}
		for (int i = 1; i < _ranges.Length - 1; i++)
		{
			if (_level <= _ranges[i])
			{
				return (float)(_level - _ranges[i - 1]) / ((float)(_ranges[i] - _ranges[i - 1]) / (_values[i] - _values[i - 1])) + _values[i - 1];
			}
		}
		return (float)(_level - _ranges[_ranges.Length - 2]) / ((float)(_ranges[_ranges.Length - 1] - _ranges[_ranges.Length - 2]) / (_values[_values.Length - 1] - _values[_values.Length - 2])) + _values[_values.Length - 2];
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0000BB2C File Offset: 0x00009F2C
	public virtual int VehicleValueToUpgradeLevel(float _value, int[] _ranges, float[] _values)
	{
		if (_value <= _values[0])
		{
			return _ranges[0];
		}
		if (_value >= _values[_values.Length - 1])
		{
			return _ranges[_ranges.Length - 1];
		}
		for (int i = 1; i < _values.Length - 1; i++)
		{
			if (_value <= _values[i])
			{
				return this.GetRoundedValue((_value - _values[i - 1]) / ((_values[i] - _values[i - 1]) / (float)(_ranges[i] - _ranges[i - 1])) + (float)_ranges[i - 1]);
			}
		}
		return this.GetRoundedValue((_value - _values[_values.Length - 2]) / ((_values[_values.Length - 1] - _values[_values.Length - 2]) / (float)(_ranges[_ranges.Length - 1] - _ranges[_ranges.Length - 2])) + (float)_ranges[_ranges.Length - 2]);
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x0000BBE0 File Offset: 0x00009FE0
	public int GetRoundedValue(float _value)
	{
		float num = _value - (float)Mathf.FloorToInt(_value);
		if (num > 0.1f)
		{
			return Mathf.CeilToInt(_value);
		}
		return Mathf.FloorToInt(_value);
	}

	// Token: 0x0400054A RID: 1354
	public UnitC m_unitC;

	// Token: 0x0400054B RID: 1355
	public UnitType m_unitType;

	// Token: 0x0400054C RID: 1356
	public string m_name;

	// Token: 0x0400054D RID: 1357
	public Entity m_entity;

	// Token: 0x0400054E RID: 1358
	private List<IComponent> m_bodyList;

	// Token: 0x0400054F RID: 1359
	private int m_bodyListChecksum;

	// Token: 0x04000550 RID: 1360
	public bool m_isDead;

	// Token: 0x04000551 RID: 1361
	public bool m_checkForCrushing;

	// Token: 0x04000552 RID: 1362
	public HitPointType m_hitPointType;

	// Token: 0x04000553 RID: 1363
	public float m_hitPoints;

	// Token: 0x04000554 RID: 1364
	public float m_maxHitPoints;

	// Token: 0x04000555 RID: 1365
	public bool m_reactToBlastWaves;

	// Token: 0x04000556 RID: 1366
	public float m_energy;

	// Token: 0x04000557 RID: 1367
	public float m_maxEnergy;

	// Token: 0x04000558 RID: 1368
	public float[] m_baseShield;

	// Token: 0x04000559 RID: 1369
	public float[] m_currentShield;

	// Token: 0x0400055A RID: 1370
	public List<StatModifierInfo> m_shieldModifiers;

	// Token: 0x0400055B RID: 1371
	public float[] m_baseArmor;

	// Token: 0x0400055C RID: 1372
	public float[] m_currentArmor;

	// Token: 0x0400055D RID: 1373
	public List<StatModifierInfo> m_armorModifiers;

	// Token: 0x0400055E RID: 1374
	public List<BuffInfo> m_buffs;

	// Token: 0x0400055F RID: 1375
	public List<BuffInfo> m_debuffs;

	// Token: 0x04000560 RID: 1376
	public bool m_isBurning;

	// Token: 0x04000561 RID: 1377
	public bool m_canBurn;

	// Token: 0x04000562 RID: 1378
	public bool m_isFrozen;

	// Token: 0x04000563 RID: 1379
	public bool m_canFreeze;

	// Token: 0x04000564 RID: 1380
	public bool m_isElectrified;

	// Token: 0x04000565 RID: 1381
	public bool m_canElectrify;

	// Token: 0x04000566 RID: 1382
	public int m_electricitySourceCount;

	// Token: 0x04000567 RID: 1383
	public float m_lastElectrification;

	// Token: 0x04000568 RID: 1384
	public float m_electrificationTime;

	// Token: 0x04000569 RID: 1385
	public float m_temperature;

	// Token: 0x0400056A RID: 1386
	public float m_burnTemperature;

	// Token: 0x0400056B RID: 1387
	public float m_freezeTemperature;

	// Token: 0x0400056C RID: 1388
	public List<ContactInfo> m_contacts;

	// Token: 0x0400056D RID: 1389
	public float m_contactEndTreshold;

	// Token: 0x0400056E RID: 1390
	public ContactState m_contactState;

	// Token: 0x0400056F RID: 1391
	public float m_lastGroundContact;

	// Token: 0x04000570 RID: 1392
	public bool m_isTeleporting;

	// Token: 0x04000571 RID: 1393
	private int m_teleportingTicks;

	// Token: 0x04000572 RID: 1394
	private int m_originalTeleTicks;

	// Token: 0x04000573 RID: 1395
	private int m_teleportingState;

	// Token: 0x04000574 RID: 1396
	public Vector3 m_teleportEndPos;

	// Token: 0x04000575 RID: 1397
	public Vector3 m_teleportStartPos;

	// Token: 0x04000576 RID: 1398
	private Vector3 m_centerMoveDelta;

	// Token: 0x04000577 RID: 1399
	private bool m_teleportModifyAngle;

	// Token: 0x04000578 RID: 1400
	private bool m_teleportFadeRotate;

	// Token: 0x04000579 RID: 1401
	private bool m_goalTeleport;

	// Token: 0x0400057A RID: 1402
	private float m_teleportOutAngle;

	// Token: 0x0400057B RID: 1403
	private Vector2[] m_teleportOutVels;

	// Token: 0x0400057C RID: 1404
	private float[] m_teleportOutAngVels;

	// Token: 0x0400057D RID: 1405
	private TweenStyle m_teleTweenStyle;

	// Token: 0x0400057E RID: 1406
	private float m_teleTweenCurrentValue;

	// Token: 0x0400057F RID: 1407
	private float m_teleTweenDuration;

	// Token: 0x04000580 RID: 1408
	private float m_teleTweenEndValue;

	// Token: 0x04000581 RID: 1409
	private float m_teleTweenStartValue;

	// Token: 0x04000582 RID: 1410
	private float m_currentTeleTime;

	// Token: 0x04000583 RID: 1411
	private TransformC m_teleportTransform;

	// Token: 0x04000584 RID: 1412
	public bool m_teleported;

	// Token: 0x04000585 RID: 1413
	public int m_crushTolerance;

	// Token: 0x04000586 RID: 1414
	public int m_crushFramesCount;

	// Token: 0x04000587 RID: 1415
	public Vector2 m_lastFrameVelocity;

	// Token: 0x04000588 RID: 1416
	public int m_groundedChainIndex = -1;

	// Token: 0x04000589 RID: 1417
	public int m_electricChainIndex = -1;

	// Token: 0x0400058A RID: 1418
	public List<PsBlackHole> m_affectingBlackHoles;

	// Token: 0x0400058B RID: 1419
	public bool m_immuneToGroundEffects;

	// Token: 0x0400058C RID: 1420
	protected bool shieldModifiersChanged;

	// Token: 0x0400058D RID: 1421
	protected bool armorModifiersChanged;

	// Token: 0x0400058E RID: 1422
	private bool m_gravityAttracted;
}
