using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x0200000F RID: 15
[Serializable]
public class Minigame : Level
{
	// Token: 0x06000061 RID: 97 RVA: 0x00003F00 File Offset: 0x00002300
	public Minigame(int _width, int _height)
		: base(_width, _height)
	{
		PsMetagameManager.m_playerStats.copperReset = false;
		this.m_itemCount = new Dictionary<string, ObscuredInt>();
		this.m_groundCount = new Dictionary<string, int>();
		this.m_complexity = 0;
		this.m_levelRequirement = 0;
		this.m_playerUnitName = null;
		this.m_generateDefaultName = true;
		this.m_changed = true;
		this.m_globalGravity = PsState.m_defaultGravity;
		this.m_gravityMultipler = 1;
		this.CreateMiniGameSpriteSheet();
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00003FEC File Offset: 0x000023EC
	public Minigame(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_generateDefaultName = false;
		this.m_changed = false;
		this.m_globalGravity = PsState.m_defaultGravity;
		this.m_gravityMultipler = 1;
		this.CreateMiniGameSpriteSheet();
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000040A2 File Offset: 0x000024A2
	public void CreateMiniGameSpriteSheet()
	{
		if (this.m_miniGameSpriteSheet == null)
		{
			this.m_miniGameSpriteSheet = SpriteS.AddSpriteSheet(CameraS.m_mainCamera, ResourceManager.GetMaterial(RESOURCE.IngameAtlasMat_Material), ResourceManager.GetTextAsset(RESOURCE.UiAtlas_TextAsset), 1f);
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000040D8 File Offset: 0x000024D8
	public override void AddItem(string _key)
	{
		if (this.m_itemCount == null)
		{
			this.m_itemCount = new Dictionary<string, ObscuredInt>();
		}
		PsEditorItem unlockableByIdentifier = PsMetagameData.GetUnlockableByIdentifier(_key);
		if (unlockableByIdentifier != null)
		{
			if (unlockableByIdentifier.m_graphNodeClassName == "LevelPlayerNode")
			{
				this.m_playerUnitName = unlockableByIdentifier.m_identifier;
			}
			else if (this.m_itemCount.ContainsKey(_key))
			{
				this.m_itemCount[_key] = this.m_itemCount[_key] + 1;
			}
			else
			{
				this.m_itemCount.Add(_key, 1);
			}
			this.m_complexity += unlockableByIdentifier.m_complexity;
			if (unlockableByIdentifier.m_itemLevel > this.m_levelRequirement)
			{
				this.m_levelRequirement = unlockableByIdentifier.m_itemLevel;
			}
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000041AC File Offset: 0x000025AC
	public override void RemoveItem(string _key)
	{
		if (this.m_itemCount == null)
		{
			this.m_itemCount = new Dictionary<string, ObscuredInt>();
			return;
		}
		PsEditorItem unlockableByIdentifier = PsMetagameData.GetUnlockableByIdentifier(_key);
		if (unlockableByIdentifier != null && this.m_itemCount.ContainsKey(_key))
		{
			this.m_itemCount[_key] = this.m_itemCount[_key] - 1;
			if (this.m_itemCount[_key] <= 0)
			{
				this.m_itemCount.Remove(_key);
				if (this.m_levelRequirement != 0 && this.m_levelRequirement == unlockableByIdentifier.m_itemLevel)
				{
					this.m_levelRequirement = 0;
					foreach (string text in this.m_itemCount.Keys)
					{
						PsEditorItem unlockableByIdentifier2 = PsMetagameData.GetUnlockableByIdentifier(text);
						if (unlockableByIdentifier2.m_itemLevel > this.m_levelRequirement)
						{
							this.m_levelRequirement = unlockableByIdentifier2.m_itemLevel;
						}
					}
				}
			}
			this.m_complexity -= unlockableByIdentifier.m_complexity;
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000042E0 File Offset: 0x000026E0
	public override void ClearItems()
	{
		if (this.m_itemCount != null)
		{
			this.m_itemCount.Clear();
		}
		this.m_complexity = 0;
		this.m_levelRequirement = 0;
		this.m_playerUnitName = null;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00004310 File Offset: 0x00002710
	public override void Destroy()
	{
		if (this.m_environmentEntity != null)
		{
			if (this.m_environmentEntity != null)
			{
				EntityManager.RemoveEntity(this.m_environmentEntity);
			}
			if (this.m_domeEntity != null)
			{
				EntityManager.RemoveEntity(this.m_domeEntity);
			}
			this.m_domeEntity = null;
			this.m_environmentEntity = null;
		}
		this.m_groundBody = null;
		this.m_groundC = null;
		this.m_itemCount.Clear();
		this.m_itemCount = null;
		this.m_complexity = 0;
		this.m_levelRequirement = 0;
		this.m_playerUnitName = null;
		if (this.m_miniGameSpriteSheet != null)
		{
			SpriteS.RemoveSpriteSheet(this.m_miniGameSpriteSheet);
			this.m_miniGameSpriteSheet = null;
		}
		base.Destroy();
		AutoGeometryManager.Free();
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000043C0 File Offset: 0x000027C0
	public void ApplySettings(bool _createAreaLimits = true, bool _updateDomeGfx = true)
	{
		Debug.LogWarning("APPLY SETTINGS: " + _createAreaLimits);
		LevelPlayerNode levelPlayerNode = LevelManager.m_currentLevel.m_currentLayer.GetElement("Player") as LevelPlayerNode;
		if (levelPlayerNode != null)
		{
			PsState.m_editorCameraPos = levelPlayerNode.m_position;
		}
		if (this.m_environmentEntity == null)
		{
			this.m_environmentEntity = EntityManager.AddEntity("MinigameEnvironmentEntity");
			this.m_environmentTC = TransformS.AddComponent(this.m_environmentEntity, "MinigameEnvironmentTC", new Vector3(0f, (float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * -0.5f, 0f));
		}
		int num = 2;
		if (this.m_settings.Contains("domeSizeIndex"))
		{
			Debug.LogWarning("HAS DOME SIZE: " + (int)this.m_settings["domeSizeIndex"]);
			num = (int)this.m_settings["domeSizeIndex"];
		}
		else
		{
			Debug.LogWarning("DOESN'T HAVE DOME SIZE");
			LevelLayer currentLayer = LevelManager.m_currentLevel.m_currentLayer;
			int num2 = currentLayer.m_layerWidth / 2048;
			Debug.Log("OLD LEVEL - CALCULATING DOME SIZE AUTOMATICALLY: " + num2, null);
			for (int i = 0; i < PsState.m_domeSizes.Length; i++)
			{
				if (PsState.m_domeSizes[i].x >= (float)num2)
				{
					num = i;
					break;
				}
			}
		}
		PsState.m_wizardDomeIndex = num;
		Debug.Log(num, null);
		if (_createAreaLimits)
		{
			this.UpdateAreaLimits(num, false, _updateDomeGfx);
		}
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00004554 File Offset: 0x00002954
	public override void SetLayerItems()
	{
		if (this.m_groundNode == null)
		{
			this.m_groundNode = LevelManager.m_currentLevel.m_currentLayer.GetElement("LevelGround") as LevelGroundNode;
		}
		if (this.m_groundCount == null)
		{
			this.m_groundCount = new Dictionary<string, int>();
		}
		bool flag = false;
		Debug.LogWarning(this.m_groundNode != null);
		for (int i = 0; i < this.m_groundNode.m_AGLayer.Length; i++)
		{
			if (this.m_groundNode.m_AGLayer[i].m_tileHash.Count <= 0)
			{
				if (this.m_groundCount.ContainsKey(this.m_groundNode.m_AGLayer[i].m_groundC.m_ground.m_groundItemIdentifier))
				{
					this.m_groundCount.Remove(this.m_groundNode.m_AGLayer[i].m_groundC.m_ground.m_groundItemIdentifier);
					if (!flag)
					{
						PsEditorItem unlockableByIdentifier = PsMetagameData.GetUnlockableByIdentifier(this.m_groundNode.m_AGLayer[i].m_groundC.m_ground.m_groundItemIdentifier);
						if (this.m_levelRequirement != 0 && this.m_levelRequirement == unlockableByIdentifier.m_itemLevel)
						{
							flag = true;
						}
					}
				}
			}
			else
			{
				if (this.m_groundCount.ContainsKey(this.m_groundNode.m_AGLayer[i].m_groundC.m_ground.m_groundItemIdentifier))
				{
					this.m_groundCount[this.m_groundNode.m_AGLayer[i].m_groundC.m_ground.m_groundItemIdentifier] = this.m_groundNode.m_AGLayer[i].m_tileHash.Count;
				}
				else
				{
					this.m_groundCount.Add(this.m_groundNode.m_AGLayer[i].m_groundC.m_ground.m_groundItemIdentifier, this.m_groundNode.m_AGLayer[i].m_tileHash.Count);
				}
				PsEditorItem unlockableByIdentifier2 = PsMetagameData.GetUnlockableByIdentifier(this.m_groundNode.m_AGLayer[i].m_groundC.m_ground.m_groundItemIdentifier);
				if (this.m_levelRequirement < unlockableByIdentifier2.m_itemLevel)
				{
					this.m_levelRequirement = unlockableByIdentifier2.m_itemLevel;
				}
			}
		}
		if (flag)
		{
			this.m_levelRequirement = 0;
			foreach (string text in this.m_groundCount.Keys)
			{
				PsEditorItem unlockableByIdentifier3 = PsMetagameData.GetUnlockableByIdentifier(text);
				if (unlockableByIdentifier3.m_itemLevel > this.m_levelRequirement)
				{
					this.m_levelRequirement = unlockableByIdentifier3.m_itemLevel;
				}
			}
		}
		Debug.Log("Levelrequirement: " + this.m_levelRequirement, null);
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00004820 File Offset: 0x00002C20
	public void InitializeDrawMaterials()
	{
		int count = PsMetagameData.m_gameMaterials[0].m_items.Count;
		this.m_layerIndexTable = new int[count];
		for (int i = 0; i < count; i++)
		{
			PsEditorItem psEditorItem = PsMetagameData.m_gameMaterials[0].m_items[i] as PsEditorItem;
			this.m_layerIndexTable[i] = this.GetLayerIndexForMaterial(psEditorItem.m_identifier);
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00004894 File Offset: 0x00002C94
	public int GetLayerIndexForMaterial(string _matIdentifier)
	{
		for (int i = 0; i < AutoGeometryManager.m_layers.Count; i++)
		{
			if (AutoGeometryManager.m_layers[i].m_groundC.m_ground.m_groundItemIdentifier.Equals(_matIdentifier))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x000048E4 File Offset: 0x00002CE4
	public int GetMatIndexFromLayerIndex(int _layerId)
	{
		for (int i = 0; i < this.m_layerIndexTable.Length; i++)
		{
			if (this.m_layerIndexTable[i] == _layerId)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0000491B File Offset: 0x00002D1B
	public void AddDomeSizeChangeListener(Action<int> _listener)
	{
		this.d_domeSizeChange = (Action<int>)Delegate.Remove(this.d_domeSizeChange, _listener);
		this.d_domeSizeChange = (Action<int>)Delegate.Combine(this.d_domeSizeChange, _listener);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0000494B File Offset: 0x00002D4B
	public void RemoveDomeSizeChangeListener(Action<int> _listener)
	{
		this.d_domeSizeChange = (Action<int>)Delegate.Remove(this.d_domeSizeChange, _listener);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00004964 File Offset: 0x00002D64
	public void UpdateAreaLimits(int _domeIndex, bool _updateLookupTables = true, bool _updateDomeGfx = true)
	{
		this.m_domeSizeIndex = _domeIndex;
		this.d_domeSizeChange.Invoke(_domeIndex);
		Vector2 vector = PsState.m_domeSizes[_domeIndex];
		this.m_settings["domeSizeIndex"] = _domeIndex;
		Debug.LogWarning("UPDATING AREA LIMITS - SETTING DOME SIZE INDEX TO " + _domeIndex);
		if (this.m_domeEntity != null)
		{
			EntityManager.RemoveEntity(this.m_domeEntity);
		}
		PsEditorItem psEditorItem = PsMetagameData.m_gameAreas[0].m_items[_domeIndex] as PsEditorItem;
		if (psEditorItem != null)
		{
			if (psEditorItem.m_identifier == "AreaSmall")
			{
				this.m_maxComplexity = 1000;
			}
			else if (psEditorItem.m_identifier == "AreaMedium")
			{
				this.m_maxComplexity = 1250;
			}
			else if (psEditorItem.m_identifier == "AreaLarge")
			{
				this.m_maxComplexity = 1500;
			}
		}
		else
		{
			this.m_maxComplexity = 1000;
		}
		vector = vector * 2048f / 16f;
		AutoGeometryManager.SetDomeSize((int)vector.x, (int)vector.y, 30f);
		Vector2 vector2 = vector * 16f;
		float num = AutoGeometryManager.m_domeDiameter.y / AutoGeometryManager.m_domeDiameter.x;
		LevelLayer currentLayer = LevelManager.m_currentLevel.m_currentLayer;
		this.m_domeEntity = EntityManager.AddEntity();
		TransformC transformC = TransformS.AddComponent(this.m_domeEntity, Vector2.zero);
		int num2 = 16 + (int)((float)AutoGeometryManager.m_width / 500f);
		float num3 = -90f;
		float num4 = 180f / (float)(num2 - 1);
		Vector2 vector3 = AutoGeometryManager.m_domeDiameter * 16f;
		Vector2[] array = new Vector2[num2];
		for (int i = 0; i < num2; i++)
		{
			float num5 = (vector3.x * 0.5f + 80f) * Mathf.Sin(num3 * 0.017453292f);
			float num6 = (float)(-(float)AutoGeometryManager.m_height) * 0.5f + (vector3.y * 0.5f + 80f * num + 30f) * Mathf.Cos(num3 * 0.017453292f);
			array[i] = new Vector2(num5, num6);
			num3 += num4;
		}
		ucpSegmentShape[] array2 = new ucpSegmentShape[num2];
		Vector2 vector4;
		vector4..ctor(0f, (float)(-(float)currentLayer.m_layerHeight) * 0.5f);
		for (int j = 1; j < num2; j++)
		{
			array2[j - 1] = new ucpSegmentShape(array[j - 1], array[j], 32f, Vector2.zero, 257U, 0f, 0.5f, 0.8f, (ucpCollisionType)2, false);
		}
		Vector2 vector5;
		vector5..ctor((float)currentLayer.m_layerWidth * -0.5f, 0f);
		Vector2 vector6;
		vector6..ctor((float)currentLayer.m_layerWidth * 0.5f, 0f);
		array2[num2 - 1] = new ucpSegmentShape(vector5, vector6, 30f, vector4, 257U, 0f, 0.5f, 0.8f, (ucpCollisionType)2, false);
		this.m_groundBody = ChipmunkProS.AddStaticBody(transformC, array2, null);
		this.m_groundC = PsS.AddGround(this.m_domeEntity, new SandGround(this.m_groundNode));
		this.m_groundBody.customComponent = this.m_groundC;
		if (this.m_settings.Contains("bgSeed"))
		{
			int num7 = (int)this.m_settings["bgSeed"];
		}
		if (_updateLookupTables)
		{
			AutoGeometryManager.UpdateMaxValueLookupTable(AutoGeometryManager.m_layers[PsState.m_drawLayer], LookupUpdateMode.LEVEL_LOADED, true);
		}
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00004D38 File Offset: 0x00003138
	public void GenerateDefaultNameAndDescription()
	{
		if (!this.m_generateDefaultName)
		{
			return;
		}
		PsState.m_activeGameLoop.m_minigameMetaData.name = Minigame.GetRandomName();
		PsState.m_activeGameLoop.m_minigameMetaData.description = Minigame.GetDefaultDescription(PsState.m_activeGameLoop.m_minigameMetaData.gameMode);
		Debug.Log("Changing minigame default name to: " + PsState.m_activeGameLoop.m_minigameMetaData.name, null);
		this.m_generateDefaultName = false;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00004DB0 File Offset: 0x000031B0
	public static string GetRandomName()
	{
		Random.seed = Random.Range(0, (int)(Main.m_gameTimeSinceAppStarted * 72.0));
		int num = PsResources.LevelNameGeneratorBaseStrings.Length;
		return PsResources.LevelNameGeneratorBaseStrings[Random.Range(0, num)];
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00004DED File Offset: 0x000031ED
	public static string GetDefaultDescription(PsGameMode _gameMode)
	{
		return "Play this level!";
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00004DF4 File Offset: 0x000031F4
	public void AddGameModeSetListener(Action<string> _listener)
	{
		this.d_gamemodeSet = (Action<string>)Delegate.Remove(this.d_gamemodeSet, _listener);
		this.d_gamemodeSet = (Action<string>)Delegate.Combine(this.d_gamemodeSet, _listener);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00004E24 File Offset: 0x00003224
	public void RemoveGameModeSetListener(Action<string> _listener)
	{
		this.d_gamemodeSet = (Action<string>)Delegate.Remove(this.d_gamemodeSet, _listener);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00004E40 File Offset: 0x00003240
	public void SetGameMode(string _gameModeName, Vector2 _pos, bool _firstSelect)
	{
		this.d_gamemodeSet.Invoke(_gameModeName);
		PsState.m_activeGameLoop.m_minigameMetaData.gameMode = (PsGameMode)Enum.Parse(typeof(PsGameMode), _gameModeName);
		GraphElement[] elements = LevelManager.m_currentLevel.m_currentLayer.GetElements("CollectibleStar");
		PsGameMode gameMode = PsState.m_activeGameLoop.m_minigameMetaData.gameMode;
		if (gameMode != PsGameMode.Race)
		{
			if (gameMode == PsGameMode.StarCollect)
			{
				if (elements.Length == 0)
				{
					this.CreateStars();
				}
			}
		}
		else
		{
			for (int i = 0; i < elements.Length; i++)
			{
				elements[i].Dispose();
			}
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00004EEC File Offset: 0x000032EC
	public void CreateStars()
	{
		for (int i = 0; i < 3; i++)
		{
			float num = -500f + (float)i * 500f;
			float num2 = (float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * -0.5f + 500f;
			Vector3 vector;
			vector..ctor(num, num2, 0f);
			GraphElement graphElement = new GraphNode(GraphNodeType.Normal, Type.GetType("CollectibleStar"), "CollectibleStar", vector, Vector3.zero, Vector3.one);
			LevelManager.m_currentLevel.m_currentLayer.AddElement(graphElement);
			graphElement.Assemble();
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00004F7F File Offset: 0x0000337F
	public void SetPlayer(Unit _player, TransformC _playerTC, Type _controllerType)
	{
		this.m_playerUnit = _player;
		this.m_playerNode = _player.m_graphElement as LevelPlayerNode;
		this.m_playerTC = _playerTC;
		this.m_playerControllerType = _controllerType;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00004FA7 File Offset: 0x000033A7
	public void AddPlayerUnitSetListener(Action<Unit> _listener)
	{
		this.d_playerUnitSet = (Action<Unit>)Delegate.Remove(this.d_playerUnitSet, _listener);
		this.d_playerUnitSet = (Action<Unit>)Delegate.Combine(this.d_playerUnitSet, _listener);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00004FD7 File Offset: 0x000033D7
	public void RemovePlayerUnitSetListener(Action<Unit> _listener)
	{
		this.d_playerUnitSet = (Action<Unit>)Delegate.Remove(this.d_playerUnitSet, _listener);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00004FF0 File Offset: 0x000033F0
	public void SetPlayerNode(Unit _player)
	{
		this.d_playerUnitSet.Invoke(_player);
		this.m_playerNode = _player.m_graphElement as LevelPlayerNode;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0000500F File Offset: 0x0000340F
	public void SetGoalNode(Unit _goal)
	{
		this.m_goalNode = _goal.m_graphElement as GraphNode;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00005022 File Offset: 0x00003422
	public void RemovePlayer()
	{
		this.m_playerTC = null;
		this.m_playerUnit = null;
		this.m_playerNode = null;
		this.m_playerControllerType = null;
		SoundS.SetListener(CameraS.m_mainCamera.gameObject, true);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00005050 File Offset: 0x00003450
	public override void ItemChanged()
	{
		base.ItemChanged();
		this.m_itemsModificationCount++;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00005066 File Offset: 0x00003466
	public void SetOverrideCC(float _overrideCC)
	{
		this.m_overrideCC = _overrideCC;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00005070 File Offset: 0x00003470
	public override void Update()
	{
		base.Update();
		this.UpdateTimer();
		if (this.m_editing && this.m_changed && this.m_newEditSession)
		{
			this.m_newEditSession = false;
			this.m_editSessionCount++;
		}
		else if (!this.m_newEditSession && this.m_gameStarted)
		{
			this.m_newEditSession = true;
		}
		if (PsState.m_activeGameLoop != null)
		{
			if (PsState.m_activeGameLoop.m_gameMode != null)
			{
				PsState.m_activeGameLoop.m_gameMode.UpdateGhosts(this.m_gameStarted && !this.m_gamePaused);
			}
			PsState.m_activeGameLoop.UpdateGIFRecord();
		}
		if (this.m_timeScaling || this.m_waitingForTimeScaleCallback)
		{
			this.m_timeScaleCurrentTime += Main.m_dt;
			if (this.m_timeScaling)
			{
				if (this.m_timeScaleCurrentTime >= this.m_timeScaleDuration)
				{
					Main.SetTimeScale(this.m_timeScaleTarget);
					this.m_timeScaling = false;
					if (!this.m_waitingForTimeScaleCallback && this.m_timeScaleCallback != null)
					{
						this.m_timeScaleCallback.Invoke();
					}
				}
				else
				{
					float num = TweenS.tween(this.m_timeScaleTweenStyle, this.m_timeScaleCurrentTime, this.m_timeScaleDuration, this.m_timeScaleStart, this.m_timeScaleTarget - this.m_timeScaleStart);
					Main.SetTimeScale(num);
				}
			}
			else if (this.m_timeScaleCurrentTime >= this.m_timeScaleDuration + this.m_timeScaleCallbackDelay)
			{
				if (this.m_timeScaleCallback != null)
				{
					this.m_timeScaleCallback.Invoke();
				}
				this.m_waitingForTimeScaleCallback = false;
				this.m_timeScaleCallback = null;
			}
		}
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00005218 File Offset: 0x00003618
	public virtual void TweenTimeScale(float _targetTimeScale, TweenStyle _tweenStyle, float _duration, Action _callback = null, float _callbackDelay = 0f)
	{
		this.m_timeScaleStart = Main.m_timeScale;
		this.m_timeScaleTarget = _targetTimeScale;
		this.m_timeScaleTweenStyle = _tweenStyle;
		this.m_timeScaleCurrentTime = 0f;
		this.m_timeScaleDuration = _duration;
		this.m_timeScaleCallback = _callback;
		this.m_timeScaleCallbackDelay = _callbackDelay;
		this.m_timeScaling = true;
		if (this.m_timeScaleCallback != null && this.m_timeScaleCallbackDelay > 0f)
		{
			this.m_waitingForTimeScaleCallback = true;
		}
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00005289 File Offset: 0x00003689
	public virtual void RemoveTimeScale()
	{
		this.m_timeScaling = false;
		this.m_waitingForTimeScaleCallback = false;
		this.m_timeScaleCallback = null;
		Main.SetTimeScale(1f);
	}

	// Token: 0x06000082 RID: 130 RVA: 0x000052AA File Offset: 0x000036AA
	public virtual void InitializeTimer()
	{
		this.m_timeElapsed = 0.0;
		this.m_timeElapsedInEditor = 0.0;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000052CA File Offset: 0x000036CA
	public void ResetEditDateCounters()
	{
		this.m_editSessionCount = 0;
		this.m_itemsModificationCount = 0;
		this.m_groundsModificationCount = 0;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000052E1 File Offset: 0x000036E1
	public virtual int GetTimeSinceInit()
	{
		return Convert.ToInt32(Math.Round(this.m_timeElapsed));
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000052F3 File Offset: 0x000036F3
	public virtual int GetEditTimeSinceInit()
	{
		return Convert.ToInt32(Math.Round(this.m_timeElapsedInEditor));
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00005305 File Offset: 0x00003705
	protected virtual void UpdateTimer()
	{
		if (this.m_editing)
		{
			this.m_timeElapsedInEditor += (double)Time.unscaledDeltaTime;
		}
		this.m_timeElapsed += (double)Time.unscaledDeltaTime;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00005338 File Offset: 0x00003738
	public void SetGamePaused(bool _pause)
	{
		if (_pause != PsState.m_physicsPaused)
		{
			if (_pause)
			{
				SoundS.SetPauseForAllSounds(true);
				EntityManager.SetActivityOfEntitiesWithTag("GTAG_AUTODESTROY", false, false, false, true, false, false);
				EntityManager.SetActivityOfEntitiesWithTag("GTAG_UNIT", false, false, false, true, false, false);
				CameraS.m_updateComponents = false;
				PsState.m_physicsPaused = true;
				this.m_gamePaused = true;
				this.m_gameOn = false;
				if (this.m_currentSign != null)
				{
					this.m_currentSign.Hide();
				}
				if (PsGravitySwitch.m_reversedGravityEffect != null && PsGravitySwitch.m_reversedGravityEffect.activeSelf)
				{
					ParticleSystem component = PsGravitySwitch.m_reversedGravityEffect.GetComponent<ParticleSystem>();
					if (component && component.isPlaying)
					{
						component.Pause();
					}
				}
			}
			else
			{
				SoundS.SetPauseForAllSounds(false);
				EntityManager.SetActivityOfEntitiesWithTag("GTAG_AUTODESTROY", true, false, false, true, false, false);
				EntityManager.SetActivityOfEntitiesWithTag("GTAG_UNIT", true, false, false, true, false, false);
				PsState.m_physicsPaused = false;
				CameraS.m_updateComponents = true;
				this.m_gamePaused = false;
				this.m_gameOn = true;
				if (this.m_currentSign != null)
				{
					this.m_currentSign.Show();
				}
				if (PsGravitySwitch.m_reversedGravityEffect != null && PsGravitySwitch.m_reversedGravityEffect.activeSelf)
				{
					ParticleSystem component2 = PsGravitySwitch.m_reversedGravityEffect.GetComponent<ParticleSystem>();
					if (component2 && component2.isPaused)
					{
						component2.Play();
					}
				}
			}
			EveryplayManager.SetPaused(_pause);
		}
	}

	// Token: 0x06000088 RID: 136 RVA: 0x000054A0 File Offset: 0x000038A0
	public void HideScene()
	{
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_AUTODESTROY", false, true, true, true, false, false);
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_UNIT", false, true, true, true, false, false);
		EntityManager.SetActivityOfEntity(this.m_environmentEntity, false, true, true, true, true, true);
		EntityManager.SetActivityOfEntity(this.m_domeEntity, false, true, true, true, true, true);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x000054F4 File Offset: 0x000038F4
	public void ShowScene()
	{
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_AUTODESTROY", true, true, true, true, false, false);
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_UNIT", true, true, true, true, false, false);
		EntityManager.SetActivityOfEntity(this.m_environmentEntity, true, true, true, true, true, true);
		EntityManager.SetActivityOfEntity(this.m_domeEntity, true, true, true, true, true, true);
	}

	// Token: 0x0400002F RID: 47
	public bool m_editing;

	// Token: 0x04000030 RID: 48
	public bool m_gamePaused;

	// Token: 0x04000031 RID: 49
	public bool m_gameOn;

	// Token: 0x04000032 RID: 50
	public int m_gameDeathCount;

	// Token: 0x04000033 RID: 51
	public Unit m_playerUnit;

	// Token: 0x04000034 RID: 52
	public LevelPlayerNode m_playerNode;

	// Token: 0x04000035 RID: 53
	public GraphNode m_goalNode;

	// Token: 0x04000036 RID: 54
	public TransformC m_playerTC;

	// Token: 0x04000037 RID: 55
	public Type m_playerControllerType;

	// Token: 0x04000038 RID: 56
	public bool m_playerReachedGoal;

	// Token: 0x04000039 RID: 57
	public int m_playerReachedGoalCount;

	// Token: 0x0400003A RID: 58
	public bool m_gameStarted;

	// Token: 0x0400003B RID: 59
	public bool m_gameEnded;

	// Token: 0x0400003C RID: 60
	public bool m_gameTicksFreezed;

	// Token: 0x0400003D RID: 61
	public float m_gameTicks;

	// Token: 0x0400003E RID: 62
	public float m_realTimeSpent;

	// Token: 0x0400003F RID: 63
	public int m_gameStartCount;

	// Token: 0x04000040 RID: 64
	public bool m_playerBeamingOut;

	// Token: 0x04000041 RID: 65
	public bool m_highscoresSent;

	// Token: 0x04000042 RID: 66
	public Ghost m_lastLoadedGhost;

	// Token: 0x04000043 RID: 67
	public int m_lastSentTimeScore;

	// Token: 0x04000044 RID: 68
	public int m_lastSentScore;

	// Token: 0x04000045 RID: 69
	public ObscuredInt m_collectedCopper;

	// Token: 0x04000046 RID: 70
	public ObscuredInt m_collectedCoins;

	// Token: 0x04000047 RID: 71
	public ObscuredInt m_collectedCoinsForDoubleUp;

	// Token: 0x04000048 RID: 72
	public ObscuredInt m_collectedDiamonds;

	// Token: 0x04000049 RID: 73
	public ObscuredInt m_collectedShards;

	// Token: 0x0400004A RID: 74
	public ObscuredInt m_usedBoosters;

	// Token: 0x0400004B RID: 75
	public bool m_isGamePlayedThrough;

	// Token: 0x0400004C RID: 76
	public int m_collectedStars;

	// Token: 0x0400004D RID: 77
	public Entity m_environmentEntity;

	// Token: 0x0400004E RID: 78
	public Entity m_domeEntity;

	// Token: 0x0400004F RID: 79
	public TransformC m_environmentTC;

	// Token: 0x04000050 RID: 80
	public PrefabC m_backgroundPrefab;

	// Token: 0x04000051 RID: 81
	public int m_domeSizeIndex;

	// Token: 0x04000052 RID: 82
	public LevelGroundNode m_groundNode;

	// Token: 0x04000053 RID: 83
	public ChipmunkBodyC m_groundBody;

	// Token: 0x04000054 RID: 84
	public GroundC m_groundC;

	// Token: 0x04000055 RID: 85
	public SoundC m_music;

	// Token: 0x04000056 RID: 86
	public Dictionary<string, ObscuredInt> m_itemCount;

	// Token: 0x04000057 RID: 87
	public Dictionary<string, int> m_groundCount;

	// Token: 0x04000058 RID: 88
	public int m_maxComplexity;

	// Token: 0x04000059 RID: 89
	public int m_complexity;

	// Token: 0x0400005A RID: 90
	public int m_levelRequirement;

	// Token: 0x0400005B RID: 91
	public string m_playerUnitName;

	// Token: 0x0400005C RID: 92
	public const int DOME_WORLD_YOFFSET = 30;

	// Token: 0x0400005D RID: 93
	public bool m_generateDefaultName;

	// Token: 0x0400005E RID: 94
	public TutorialSign m_currentSign;

	// Token: 0x0400005F RID: 95
	public Vector2 m_globalGravity;

	// Token: 0x04000060 RID: 96
	public int m_gravityMultipler;

	// Token: 0x04000061 RID: 97
	public float m_overrideCC = -1f;

	// Token: 0x04000062 RID: 98
	public SpriteSheet m_miniGameSpriteSheet;

	// Token: 0x04000063 RID: 99
	public int[] m_layerIndexTable;

	// Token: 0x04000064 RID: 100
	private Action<int> d_domeSizeChange = delegate
	{
	};

	// Token: 0x04000065 RID: 101
	private Action<string> d_gamemodeSet = delegate
	{
	};

	// Token: 0x04000066 RID: 102
	private Action<Unit> d_playerUnitSet = delegate
	{
	};

	// Token: 0x04000067 RID: 103
	public double m_timeElapsed;

	// Token: 0x04000068 RID: 104
	public double m_timeElapsedInEditor;

	// Token: 0x04000069 RID: 105
	private bool m_newEditSession = true;

	// Token: 0x0400006A RID: 106
	public int m_editSessionCount;

	// Token: 0x0400006B RID: 107
	public int m_itemsModificationCount;

	// Token: 0x0400006C RID: 108
	public int m_groundsModificationCount;

	// Token: 0x0400006D RID: 109
	private float m_timeScaleStart;

	// Token: 0x0400006E RID: 110
	private float m_timeScaleTarget;

	// Token: 0x0400006F RID: 111
	private TweenStyle m_timeScaleTweenStyle;

	// Token: 0x04000070 RID: 112
	private float m_timeScaleDuration;

	// Token: 0x04000071 RID: 113
	private float m_timeScaleCurrentTime;

	// Token: 0x04000072 RID: 114
	private bool m_timeScaling;

	// Token: 0x04000073 RID: 115
	private bool m_waitingForTimeScaleCallback;

	// Token: 0x04000074 RID: 116
	private Action m_timeScaleCallback;

	// Token: 0x04000075 RID: 117
	private float m_timeScaleCallbackDelay;
}
