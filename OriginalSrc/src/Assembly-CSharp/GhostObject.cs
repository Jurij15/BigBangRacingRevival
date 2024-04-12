using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041D RID: 1053
public class GhostObject
{
	// Token: 0x06001D45 RID: 7493 RVA: 0x0014D438 File Offset: 0x0014B838
	public GhostObject(string _name, int _time, string _playerId, string _ghostId, string _countryCode, Ghost _ghost, string _frameName = "", string _facebookId = "", int _ghostVersion = 0)
	{
		this.m_version = _ghostVersion;
		this.m_name = _name;
		this.m_time = _time;
		this.m_playerId = _playerId;
		this.m_ghostId = _ghostId;
		this.m_ghost = _ghost;
		this.m_frameName = _frameName;
		this.m_countryCode = _countryCode;
		this.m_hideAtIntro = true;
		this.m_hidden = false;
		if (!string.IsNullOrEmpty(_frameName) && _frameName.Equals("menu_chest_badge_active"))
		{
			this.m_hideAtIntro = false;
		}
		this.m_destroyed = false;
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x0014D4F8 File Offset: 0x0014B8F8
	public virtual void SetAlienCharacter(Vector3 _offset, string _prefabName = "AlienBossPrefab_GameObject", string _driveState = "Drive")
	{
		this.m_ghostCharacter = new AlienCharacter(new GraphNode(GraphNodeType.Normal, "Boss")
		{
			m_position = this.m_chassis.transform.position + _offset
		}, _driveState, _prefabName);
		TransformS.ParentComponent(this.m_ghostCharacter.m_mainTC, this.m_chassis);
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x0014D551 File Offset: 0x0014B951
	public void SetHat()
	{
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x0014D554 File Offset: 0x0014B954
	public void Destroy()
	{
		if (this.m_destroyed)
		{
			return;
		}
		this.DestroyVisuals();
		Debug.LogWarning(this.m_name + ": Destroy ghost data");
		this.m_ghost.Destroy();
		this.m_ghost = null;
		this.m_nameTC = null;
		this.m_iconTC = null;
		this.m_nameTMC = null;
		this.m_material = null;
		this.m_ghostNodes = null;
		this.m_parts = null;
		this.m_prizeIconMat = null;
		this.m_destroyed = true;
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x0014D5D4 File Offset: 0x0014B9D4
	public virtual void CreateVisuals(GhostPart[] _parts, string _trailIdentifier, GhostBoostEffect[] _boostEffects = null, bool _hasHat = true, bool _showAtIntroOverride = false, string _identifierColor = "")
	{
		this.m_hideAtIntro = ((!_showAtIntroOverride) ? this.m_hideAtIntro : (!_showAtIntroOverride));
		this.m_hasHat = _hasHat;
		this.m_identifierColor = _identifierColor;
		this.m_reachedGoalGameTick = 0f;
		this.m_ghostDriveDir = 0;
		this.m_ghostLean = 0;
		this.m_ghost.m_speedUpSeconds = 0f;
		this.m_ghost.m_slowDownSeconds = 0f;
		this.m_collectedPieceCount = 0;
		this.m_activateBoost = false;
		this.m_boostActive = false;
		this.m_gainBoost = false;
		this.m_dropBoost = false;
		this.m_boostRight = false;
		this.m_boostLeft = false;
		this.m_teleporting = false;
		this.m_hatDetached = false;
		this.m_reachedGoal = false;
		this.m_teleportStartTicks = this.m_ghost.GetEventGameTicks(GhostEventType.TeleportStart);
		this.m_minigame = LevelManager.m_currentLevel as Minigame;
		if (this.m_entity != null)
		{
			this.DestroyVisuals();
		}
		this.m_hidden = false;
		this.m_entity = EntityManager.AddEntity("GTAG_UNIT");
		int num = _parts.Length;
		this.m_parts = new TransformC[num];
		if (_boostEffects != null)
		{
			this.m_ghostNodes = new GhostNode[num + _boostEffects.Length];
		}
		else
		{
			this.m_ghostNodes = new GhostNode[num];
		}
		this.m_chassis = null;
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < num; i++)
		{
			TransformC transformC = TransformS.AddComponent(this.m_entity, _parts[i].nodeName);
			if (_parts[i].nodeName == "chassis")
			{
				this.m_chassis = transformC;
				vector = _parts[i].offset;
			}
			if (i == 0)
			{
				CameraTargetC cameraTargetC = CameraS.AddTargetComponent(transformC, 300f, 300f, new Vector2(0f, 0f));
				cameraTargetC.activeRadius = 500f;
			}
			PrefabC prefabC = PrefabS.AddComponent(transformC, _parts[i].offset, _parts[i].prefab);
			if (_parts[i].flipX)
			{
				TransformS.SetScale(transformC, new Vector3(-1f, 1f, 1f));
			}
			this.m_parts[i] = transformC;
			this.m_ghostNodes[i] = this.m_ghost.m_nodes[_parts[i].nodeName] as GhostNode;
		}
		if (_boostEffects != null)
		{
			this.m_boostTCs = new TransformC[_boostEffects.Length];
			this.m_boosts = new EffectBoost[_boostEffects.Length];
			for (int j = 0; j < _boostEffects.Length; j++)
			{
				TransformC transformC2 = TransformS.AddComponent(this.m_entity, _boostEffects[j].nodeName);
				PrefabC prefabC2 = PrefabS.AddComponent(transformC2, _boostEffects[j].offset, _boostEffects[j].prefab);
				this.m_boosts[j] = prefabC2.p_gameObject.GetComponent<EffectBoost>();
				this.m_boostTCs[j] = transformC2;
				this.m_ghostNodes[num + j] = this.m_ghost.m_nodes[_boostEffects[j].nodeName] as GhostNode;
			}
		}
		this.SetTrail(_trailIdentifier, vector);
		this.SetGhost2DGraphics();
		this.Update(false);
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x0014D900 File Offset: 0x0014BD00
	protected virtual void SetGhost2DGraphics()
	{
		this.m_nameTC = TransformS.AddComponent(this.m_entity, "GhostNameTC");
		this.m_iconTC = TransformS.AddComponent(this.m_entity, "GhostIcon");
		Vector3 vector;
		vector..ctor(-6f, 0f, 0f);
		Vector3 vector2;
		vector2..ctor(0f, 30f, 0f);
		this.m_nameTMC = TextMeshS.AddComponent(this.m_iconTC, vector, PsFontManager.GetFont(PsFonts.KGSecondChances), 0f, 100f, 24.576f, Align.Center, Align.Middle, CameraS.m_mainCamera, string.Empty);
		TextMeshS.SetText(this.m_nameTMC, "<color=#FFFFFF>" + this.m_name + "</color>", false);
		this.m_nameTMC.m_textMesh.characterSize = 5f;
		Vector2 vector3 = TextMeshS.GetTextSize(this.m_nameTMC, this.m_name) * 0.5f;
		this.m_nameTMC.m_renderer.material.shader = Shader.Find("WOE/Unlit/ColorFontOverlay");
		float num = 20f;
		Vector3 vector4 = vector + new Vector3(num * 0.5f, 0f, 0f);
		Color black = Color.black;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(vector3.x + 20f + num, 20f, 5f, 8, Vector2.zero);
		PrefabC prefabC = PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_iconTC, vector4, roundedRect, DebugDraw.ColorToUInt(black), DebugDraw.ColorToUInt(black), ResourceManager.GetMaterial(RESOURCE.SolidOverlay_Material), CameraS.m_mainCamera, "nameBG", null);
		prefabC.p_gameObject.transform.localPosition = new Vector3(0f, 0f, 50f);
		Renderer renderer = prefabC.p_gameObject.transform.gameObject.GetComponentInChildren<Renderer>();
		if (renderer != null)
		{
			this.m_textBgMat = renderer.material;
		}
		bool flag = true;
		if (PsState.m_activeGameLoop is PsGameLoopFresh)
		{
			this.m_frameName = "menu_resources_shard_icon";
		}
		if (PsState.m_activeGameLoop is PsGameLoopSocial || PsState.m_activeGameLoop is PsGameLoopTournament)
		{
			flag = false;
		}
		if (flag && !string.IsNullOrEmpty(this.m_frameName))
		{
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(this.m_frameName, null);
			float num2 = 35f;
			if (!this.m_frameName.Equals("menu_chest_badge_active"))
			{
				num2 *= 0.7f;
			}
			SpriteC spriteC = SpriteS.AddComponent(this.m_iconTC, frame, PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC, num2 * (frame.width / frame.height), num2);
			SpriteS.SetOffset(spriteC, vector2, 0f);
		}
		if (!string.IsNullOrEmpty(this.m_identifierColor) && !this.m_spectateGhost)
		{
			this.m_ghostIdentifier = TransformS.AddComponent(this.m_entity, "GhostColorTC");
			Vector2[] rect = DebugDraw.GetRect(16f, 16f, Vector2.zero);
			Vector2[] array = rect;
			int num3 = 0;
			Vector2[] array2 = rect;
			int num4 = 1;
			array[num3].x = (array2[num4].x = array2[num4].x + 8f);
			PrefabC prefabC2 = PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_ghostIdentifier, Vector3.forward * 55f, rect, DebugDraw.HexToUint(this.m_identifierColor), DebugDraw.HexToUint(this.m_identifierColor), ResourceManager.GetMaterial(RESOURCE.SolidOverlay_Material), CameraS.m_mainCamera, "ghostIdentifier", null);
			this.m_ghostIdentifierRenderer = prefabC2.p_gameObject.transform.gameObject.GetComponentInChildren<Renderer>();
			this.m_identifierC = DebugDraw.HexToColor(this.m_identifierColor);
			if (this.m_ghostIdentifierRenderer != null)
			{
				this.m_ghostIdentifierRenderer.material.color = this.m_identifierC;
			}
		}
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame(this.m_countryCode, "_alien");
		float num5 = 12f;
		SpriteC spriteC2 = SpriteS.AddComponent(this.m_iconTC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num5 * (frame2.width / frame2.height), num5);
		SpriteS.SetOffset(spriteC2, vector + new Vector3(12f + vector3.x * 0.5f, 0f, 0f), 0f);
		prefabC = SpriteS.ConvertSpritesToPrefabComponent(this.m_iconTC, CameraS.m_mainCamera, true, Shader.Find("WOE/Unlit/ColorUnlitTransparentOverlay"))[0];
		renderer = prefabC.p_gameObject.transform.gameObject.GetComponentInChildren<Renderer>();
		if (renderer != null)
		{
			this.m_prizeIconMat = renderer.material;
		}
		if (this.m_ghostIdentifier != null)
		{
			TransformS.ParentComponent(this.m_ghostIdentifier, this.m_nameTC, new Vector3(0f, 100f, 0f));
		}
		TransformS.ParentComponent(this.m_iconTC, this.m_nameTC, new Vector3(0f, 120f, 0f));
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x0014DDF4 File Offset: 0x0014C1F4
	protected virtual void SetTrail(string _identifier, Vector3 _chassisOffset)
	{
		if (!string.IsNullOrEmpty(_identifier))
		{
			GameObject trailPrefabByIdentifier = PsCustomisationManager.GetTrailPrefabByIdentifier(_identifier);
			if (trailPrefabByIdentifier != null)
			{
				this.m_trail = Object.Instantiate<GameObject>(trailPrefabByIdentifier);
				Vector3 vector = this.m_trail.transform.position + Vector3.up * -10f + _chassisOffset;
				this.m_trail.transform.parent = this.m_chassis.transform;
				this.m_trail.transform.localPosition = vector;
				this.m_trailBase = this.m_trail.gameObject.GetComponent<PsTrailBase>();
				this.m_trailBase.Init();
				this.m_trailBase.SetGhost();
			}
		}
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x0014DEB0 File Offset: 0x0014C2B0
	public void DestroyVisuals()
	{
		if (this.m_entity != null)
		{
			Debug.LogWarning(this.m_name + ": Destroy visuals");
			EntityManager.RemoveEntity(this.m_entity);
			this.m_entity = null;
		}
		if (this.m_trailBase != null)
		{
			this.m_trailBase.Destroy();
			this.m_trailBase = null;
			Object.Destroy(this.m_trail);
			this.m_trail = null;
		}
		if (this.m_ghostCharacter != null)
		{
			this.m_ghostCharacter.Destroy();
			this.m_ghostCharacter = null;
		}
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x0014DF44 File Offset: 0x0014C344
	private void HandleEvents(List<GhostEventType> _events)
	{
		for (int i = 0; i < _events.Count; i++)
		{
			switch (_events[i])
			{
			case GhostEventType.BoostStart:
				this.m_boostActive = true;
				this.m_gainBoost = true;
				break;
			case GhostEventType.BoostEnd:
				this.m_boostActive = false;
				this.m_dropBoost = true;
				this.m_boostRight = false;
				this.m_boostLeft = false;
				break;
			case GhostEventType.BoostRightStart:
				this.m_boostRight = true;
				break;
			case GhostEventType.BoostLeftStart:
				this.m_boostLeft = true;
				break;
			case GhostEventType.BoostRightEnd:
				this.m_boostRight = false;
				break;
			case GhostEventType.BoostLeftEnd:
				this.m_boostLeft = false;
				break;
			case GhostEventType.TeleportStart:
				this.m_teleporting = true;
				if (this.m_trailBase != null)
				{
					this.m_trailBase.SetTeleporting(true);
				}
				break;
			case GhostEventType.TeleportEnd:
				this.m_teleporting = false;
				if (this.m_trailBase != null)
				{
					this.m_trailBase.SetTeleporting(false);
				}
				break;
			case GhostEventType.HatDetached:
				this.m_hatDetached = true;
				break;
			case GhostEventType.MapPieceCollected:
				this.CollectedMapPiece();
				break;
			case GhostEventType.LeanBack:
				this.LeanGhostCharacter(-1);
				break;
			case GhostEventType.LeanFront:
				this.LeanGhostCharacter(1);
				break;
			case GhostEventType.LeanCenter:
				this.LeanGhostCharacter(0);
				break;
			case GhostEventType.DriveDirForward:
				this.GhostDriveDirection(1);
				break;
			case GhostEventType.DriveDirBackward:
				this.GhostDriveDirection(-1);
				break;
			}
		}
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x0014E0C0 File Offset: 0x0014C4C0
	private void LeanGhostCharacter(int _dir)
	{
		this.m_ghostLean = _dir;
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x0014E0C9 File Offset: 0x0014C4C9
	private void GhostDriveDirection(int _dir)
	{
		this.m_ghostDriveDir = _dir;
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x0014E0D2 File Offset: 0x0014C4D2
	protected virtual void CollectedMapPiece()
	{
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x0014E0D4 File Offset: 0x0014C4D4
	private void SlowDownGame(float _targetTimescale)
	{
		Debug.LogError("Starting slowmo");
		this.m_ghostPowerUp = true;
		PsState.m_activeMinigame.TweenTimeScale(_targetTimescale, TweenStyle.ExpoIn, 0.1f, delegate
		{
			Debug.LogError("Slowmo startween ended");
			Entity e = EntityManager.AddEntity();
			TimerC timerC = TimerS.AddComponent(e, string.Empty, 0f, 3f, false, delegate(TimerC c)
			{
				TimerS.RemoveComponent(c);
				Debug.LogError("Slowmo endtween started");
				PsState.m_activeMinigame.TweenTimeScale(1f, TweenStyle.ExpoIn, 0.1f, delegate
				{
					this.m_ghostPowerUp = false;
					Debug.LogError("Slowmo ended");
					EntityManager.RemoveEntity(e);
				}, 0f);
			});
			timerC.useUnscaledDeltaTime = true;
		}, 0f);
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x0014E109 File Offset: 0x0014C509
	private void SpeedUpGhost()
	{
		this.m_ghost.SpeedUpPlayback(2f, 2f);
	}

	// Token: 0x06001D53 RID: 7507 RVA: 0x0014E120 File Offset: 0x0014C520
	private void UpdateBoosts()
	{
		for (int i = 0; i < this.m_boosts.Length; i++)
		{
			if (this.m_gainBoost)
			{
				this.m_boosts[i].GainBoost();
				this.m_boosts[i].IdleBoost();
				if (this.m_trailBase != null)
				{
					this.m_trailBase.SetBoostActive(false);
				}
			}
			if (this.m_boostActive)
			{
				if (this.m_boostRight || this.m_boostLeft)
				{
					this.m_boosts[i].ActivateBoost((!this.m_boostRight || !this.m_boostLeft) ? ((!this.m_boostRight) ? EffectBoost.BoostDirection.Left : EffectBoost.BoostDirection.Right) : EffectBoost.BoostDirection.Both);
					if (this.m_trailBase != null)
					{
						this.m_trailBase.SetBoostActive(true);
					}
				}
				else
				{
					this.m_boosts[i].IdleBoost();
					if (this.m_trailBase != null)
					{
						this.m_trailBase.SetBoostActive(false);
					}
				}
			}
			else if (this.m_dropBoost)
			{
				this.m_boosts[i].DropBoost();
				if (this.m_trailBase != null)
				{
					this.m_trailBase.SetBoostActive(false);
				}
			}
		}
		this.m_gainBoost = false;
		this.m_dropBoost = false;
	}

	// Token: 0x06001D54 RID: 7508 RVA: 0x0014E278 File Offset: 0x0014C678
	protected virtual float GetPlaybackSpeed()
	{
		return 1f;
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x0014E280 File Offset: 0x0014C680
	public void Update(bool _updateLogic)
	{
		if (this.m_entity != null && this.m_ghost != null)
		{
			if (this.m_hideAtIntro)
			{
				bool flag = !GameLevelPreview.m_camIsTurned;
				PsGameLoopRacing psGameLoopRacing = PsState.m_activeGameLoop as PsGameLoopRacing;
				bool flag2 = false;
				if (psGameLoopRacing != null)
				{
					flag2 = psGameLoopRacing.m_practiceRun;
				}
				if (!flag || flag2)
				{
					if (!this.m_hidden)
					{
						EntityManager.SetActivityOfEntity(this.m_entity, false, true, true, true, true, true);
						this.m_hidden = true;
					}
				}
				else if (this.m_hidden)
				{
					EntityManager.SetActivityOfEntity(this.m_entity, true, true, true, true, true, true);
					this.m_hidden = false;
				}
			}
			if (_updateLogic)
			{
				this.m_ghost.UpdatePlayback(this.GetPlaybackSpeed());
			}
			if (this.m_ghost.m_keyframeCount > 0)
			{
				List<GhostEventType> eventsOnTick = this.m_ghost.GetEventsOnTick(Mathf.RoundToInt(this.m_ghost.GetPlaybackTick()));
				if (eventsOnTick != null)
				{
					this.HandleEvents(eventsOnTick);
				}
				if (this.m_ghostCharacter != null)
				{
					this.m_ghostCharacter.AnimateCharacter("LeanDir", this.m_ghostLean);
					this.m_ghostCharacter.AnimateCharacter("DriveDir", this.m_ghostDriveDir);
				}
				this.UpdateBoosts();
				if (this.m_hatDetached && this.m_hasHat)
				{
					this.m_hatDetached = false;
					Transform child = this.m_parts[this.m_parts.Length - 1].transform.GetChild(0);
					SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", child.position, 1f);
					EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.EffectGenericHatsplosion_GameObject), new Vector3(child.position.x, child.position.y, 0f), Vector3.zero, 2f, "GTAG_AUTODESTROY", null);
					Object.Destroy(child.gameObject);
				}
				if (!this.m_teleporting)
				{
					int num = -1;
					int num2 = Mathf.FloorToInt(this.m_ghost.m_playbackTick);
					for (int i = 0; i < this.m_teleportStartTicks.Count; i++)
					{
						if (this.m_teleportStartTicks[i] >= num2 * this.m_ghost.m_frameSkip && this.m_teleportStartTicks[i] < Mathf.Min(num2 + 1, this.m_ghost.m_keyframeCount - 1) * this.m_ghost.m_frameSkip)
						{
							num = 1;
							break;
						}
						if (this.m_teleportStartTicks[i] >= num2 * this.m_ghost.m_frameSkip && this.m_teleportStartTicks[i] < Mathf.Min(num2 + 2, this.m_ghost.m_keyframeCount - 1) * this.m_ghost.m_frameSkip)
						{
							num = 2;
							break;
						}
					}
					for (int j = 0; j < this.m_parts.Length; j++)
					{
						GhostNode ghostNode = this.m_ghostNodes[j];
						TransformS.SetScale(this.m_parts[j], Vector3.one);
						TransformS.SetPosition(this.m_parts[j], this.m_ghost.GetCurrentPosition(ghostNode, this.m_ghost.m_playbackTick, num) + this.m_ghostZOffset);
						TransformS.SetRotation(this.m_parts[j], new Vector3(0f, 0f, this.m_ghost.GetCurrentRotation(ghostNode)));
					}
					if (this.m_boostTCs != null)
					{
						for (int k = 0; k < this.m_boostTCs.Length; k++)
						{
							GhostNode ghostNode2 = this.m_ghostNodes[this.m_parts.Length + k];
							TransformS.SetScale(this.m_boostTCs[k], Vector3.one);
							TransformS.SetPosition(this.m_boostTCs[k], this.m_ghost.GetCurrentPosition(ghostNode2, this.m_ghost.m_playbackTick, num) + this.m_ghostZOffset);
							TransformS.SetRotation(this.m_boostTCs[k], Vector3.forward * this.m_parts[0].transform.localRotation.eulerAngles.z);
						}
					}
					if (this.m_nameTC != null)
					{
						TransformS.SetScale(this.m_nameTC, Vector3.one);
						TransformS.SetPosition(this.m_nameTC, this.m_ghost.GetCurrentPosition(this.m_ghostNodes[0], this.m_ghost.m_playbackTick, num) + this.m_ghostZOffset);
					}
				}
				else if (this.m_teleporting)
				{
					for (int l = 0; l < this.m_parts.Length; l++)
					{
						TransformS.SetScale(this.m_parts[l], Vector3.zero);
					}
					if (this.m_boostTCs != null)
					{
						for (int m = 0; m < this.m_boostTCs.Length; m++)
						{
							TransformS.SetScale(this.m_boostTCs[m], Vector3.zero);
						}
					}
					if (this.m_nameTC != null)
					{
						TransformS.SetScale(this.m_nameTC, Vector3.zero);
					}
				}
				if (this.m_iconTC != null)
				{
					TransformS.LookAt(this.m_iconTC, CameraS.m_mainCamera.transform, new Vector3(0f, 1f, 0f));
					TransformS.Rotate(this.m_iconTC, new Vector3(0f, 180f, 0f));
				}
				if (this.m_ghostIdentifier != null)
				{
					TransformS.LookAt(this.m_ghostIdentifier, CameraS.m_mainCamera.transform, new Vector3(0f, 1f, 0f));
					TransformS.Rotate(this.m_ghostIdentifier, new Vector3(0f, 180f, 0f));
					if (this.m_ghostIdentifierRenderer != null)
					{
						float magnitude = (PsState.m_activeMinigame.m_playerTC.transform.position - this.m_parts[0].transform.position).magnitude;
						float num3 = 1f;
						if (this.m_minigame.m_gameStarted)
						{
							num3 = ToolBox.getPositionBetween(magnitude, 90f, 120f);
						}
						else if (GameLevelPreview.m_camIsTurned)
						{
							float positionBetween = ToolBox.getPositionBetween(Mathf.Abs(CameraS.m_mainCameraRotationOffset.magnitude), 0f, 40f);
							num3 *= positionBetween;
						}
						else
						{
							num3 = 0f;
						}
						Color identifierC = this.m_identifierC;
						identifierC.a = num3;
						this.m_ghostIdentifierRenderer.material.color = identifierC;
					}
				}
				if (this.m_nameTC != null)
				{
					float magnitude2 = (PsState.m_activeMinigame.m_playerTC.transform.position - this.m_parts[0].transform.position).magnitude;
					float num4 = 1f;
					if (!this.m_spectateGhost)
					{
						if (this.m_minigame.m_gameStarted)
						{
							num4 = ToolBox.getPositionBetween(magnitude2, 90f, 120f);
						}
						else if (GameLevelPreview.m_camIsTurned)
						{
							float positionBetween2 = ToolBox.getPositionBetween(Mathf.Abs(CameraS.m_mainCameraRotationOffset.magnitude), 0f, 40f);
							num4 *= positionBetween2;
						}
						else
						{
							num4 = 0f;
						}
					}
					Color color;
					color..ctor(1f, 1f, 1f, num4);
					Color color2;
					color2..ctor(0f, 0f, 0f, num4 * 0.3f);
					this.m_prizeIconMat.color = color;
					this.m_textBgMat.color = color2;
					this.m_nameTMC.m_renderer.material.color = color;
				}
				if (this.m_ghost.PlaybackEnded())
				{
					SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", this.m_parts[0].transform.position, 1f);
					EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.EffectGenericHatsplosion_GameObject), new Vector3(this.m_parts[0].transform.position.x, this.m_parts[0].transform.position.y, 0f), Vector3.zero, 2f, "GTAG_AUTODESTROY", null);
					this.DestroyVisuals();
				}
			}
		}
		else if (!this.m_reachedGoal)
		{
			this.m_reachedGoal = true;
			this.m_reachedGoalGameTick = PsState.m_activeMinigame.m_gameTicks;
		}
	}

	// Token: 0x06001D56 RID: 7510 RVA: 0x0014EAED File Offset: 0x0014CEED
	public bool ReachedGoal()
	{
		return this.m_reachedGoal;
	}

	// Token: 0x04001FFF RID: 8191
	public bool m_destroyed;

	// Token: 0x04002000 RID: 8192
	public Vector3 m_ghostZOffset = new Vector3(0f, 0f, 80f);

	// Token: 0x04002001 RID: 8193
	public string m_name;

	// Token: 0x04002002 RID: 8194
	public int m_time;

	// Token: 0x04002003 RID: 8195
	public string m_playerId;

	// Token: 0x04002004 RID: 8196
	public string m_ghostId;

	// Token: 0x04002005 RID: 8197
	public string m_countryCode;

	// Token: 0x04002006 RID: 8198
	public Entity m_entity;

	// Token: 0x04002007 RID: 8199
	public Ghost m_ghost;

	// Token: 0x04002008 RID: 8200
	public TransformC[] m_parts;

	// Token: 0x04002009 RID: 8201
	public GhostNode[] m_ghostNodes;

	// Token: 0x0400200A RID: 8202
	public string m_identifierColor;

	// Token: 0x0400200B RID: 8203
	public Color m_identifierC;

	// Token: 0x0400200C RID: 8204
	public Material m_material;

	// Token: 0x0400200D RID: 8205
	public TransformC m_ghostIdentifier;

	// Token: 0x0400200E RID: 8206
	private Renderer m_ghostIdentifierRenderer;

	// Token: 0x0400200F RID: 8207
	public TransformC m_nameTC;

	// Token: 0x04002010 RID: 8208
	public TransformC m_iconTC;

	// Token: 0x04002011 RID: 8209
	public TextMeshC m_nameTMC;

	// Token: 0x04002012 RID: 8210
	public string m_frameName;

	// Token: 0x04002013 RID: 8211
	private Material m_prizeIconMat;

	// Token: 0x04002014 RID: 8212
	private Material m_textBgMat;

	// Token: 0x04002015 RID: 8213
	private Minigame m_minigame;

	// Token: 0x04002016 RID: 8214
	public int m_version;

	// Token: 0x04002017 RID: 8215
	protected bool m_hideAtIntro;

	// Token: 0x04002018 RID: 8216
	private bool m_hidden;

	// Token: 0x04002019 RID: 8217
	private TransformC[] m_boostTCs;

	// Token: 0x0400201A RID: 8218
	private EffectBoost[] m_boosts;

	// Token: 0x0400201B RID: 8219
	public PsTrailBase m_trailBase;

	// Token: 0x0400201C RID: 8220
	public GameObject m_trail;

	// Token: 0x0400201D RID: 8221
	private bool m_activateBoost;

	// Token: 0x0400201E RID: 8222
	private bool m_boostActive;

	// Token: 0x0400201F RID: 8223
	private bool m_gainBoost;

	// Token: 0x04002020 RID: 8224
	private bool m_dropBoost;

	// Token: 0x04002021 RID: 8225
	private bool m_boostRight;

	// Token: 0x04002022 RID: 8226
	private bool m_boostLeft;

	// Token: 0x04002023 RID: 8227
	private bool m_teleporting;

	// Token: 0x04002024 RID: 8228
	private bool m_hatDetached;

	// Token: 0x04002025 RID: 8229
	private bool m_reachedGoal;

	// Token: 0x04002026 RID: 8230
	public bool m_spectateGhost;

	// Token: 0x04002027 RID: 8231
	public TransformC m_chassis;

	// Token: 0x04002028 RID: 8232
	public AlienCharacter m_ghostCharacter;

	// Token: 0x04002029 RID: 8233
	private int m_ghostDriveDir = 1;

	// Token: 0x0400202A RID: 8234
	private int m_ghostLean;

	// Token: 0x0400202B RID: 8235
	public float[] m_collectedPieceGameTick = new float[3];

	// Token: 0x0400202C RID: 8236
	public int m_collectedPieceCount;

	// Token: 0x0400202D RID: 8237
	public float m_reachedGoalGameTick;

	// Token: 0x0400202E RID: 8238
	public List<int> m_teleportStartTicks;

	// Token: 0x0400202F RID: 8239
	private bool m_hasHat = true;

	// Token: 0x04002030 RID: 8240
	public bool m_ghostPowerUp;
}
