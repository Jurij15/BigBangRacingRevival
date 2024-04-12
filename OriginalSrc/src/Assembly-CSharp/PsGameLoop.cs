using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class PsGameLoop
{
	// Token: 0x060005AE RID: 1454 RVA: 0x00047EA0 File Offset: 0x000462A0
	public PsGameLoop(PsMinigameMetaData _data)
		: this(PsMinigameContext.Undefined, _data.id, null, -1, -1, _data.score, false, null)
	{
		this.m_minigameMetaData = _data;
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00047ED0 File Offset: 0x000462D0
	public PsGameLoop(PsMinigameContext _context, string _minigameId, PsPlanetPath _path, int _id, int _levelNumber, int _score, bool _unlocked, string[] _medalTimes = null)
	{
		this.m_planet = PsPlanetManager.GetCurrentPlanet();
		this.m_context = _context;
		this.m_minigameId = _minigameId;
		this.m_path = _path;
		this.m_nodeId = _id;
		this.m_levelNumber = _levelNumber;
		this.m_scoreBest = _score;
		this.m_scoreCurrent = this.m_scoreBest;
		this.m_scoreOld = this.m_scoreBest;
		this.m_rewardOld = this.m_scoreBest;
		this.m_unlocked = _unlocked;
		this.m_levelWasUnlocked = false;
		this.m_currentRunScore = 0;
		this.m_activated = false;
		if (this.m_unlocked)
		{
			this.m_activated = true;
		}
		this.m_forcedMedalTimes = _medalTimes;
		this.m_sidePath = null;
		this.m_loadingMetaData = false;
		this.m_sendQuit = true;
		this.m_startTime = DateTime.UtcNow;
		this.m_backAtMenu = false;
		this.m_released = false;
		this.m_boosterUsed = false;
		this.m_freeUnlock = false;
		this.m_adRent = false;
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00047FDA File Offset: 0x000463DA
	// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00047FE2 File Offset: 0x000463E2
	public bool m_unlocked
	{
		get
		{
			return this.unlocked;
		}
		set
		{
			if (value && !this.unlocked)
			{
				this.m_levelWasUnlocked = true;
			}
			this.unlocked = value;
		}
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00048004 File Offset: 0x00046404
	public virtual bool UseCreatorVehicle()
	{
		return this.m_useCreatorUpgrades && this.m_minigameMetaData != null && this.m_minigameMetaData.creatorUpgrades != null && this.m_minigameMetaData.creatorUpgrades.ContainsKey("version") && Convert.ToInt32(this.m_minigameMetaData.creatorUpgrades["version"]) >= 2;
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00048074 File Offset: 0x00046474
	public virtual void SetOverrideCC(float _overrideCC)
	{
		this.m_overrideCC = _overrideCC;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0004807D File Offset: 0x0004647D
	public virtual float GetOverrideCC()
	{
		return this.m_overrideCC;
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00048088 File Offset: 0x00046488
	public bool IsFirstLevel()
	{
		return !PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && this.m_path != null && this.m_path.m_planet == "AdventureOffroadCar" && this.m_path.m_currentNodeId <= 1;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x000480E2 File Offset: 0x000464E2
	public virtual Type GetLoadingScreenComponent()
	{
		return typeof(PsUICenterRacingLoading);
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x000480F0 File Offset: 0x000464F0
	public virtual PlayerData GetCreator()
	{
		PlayerData playerData = default(PlayerData);
		if (this.m_minigameMetaData == null)
		{
			return playerData;
		}
		playerData.playerId = this.m_minigameMetaData.creatorId;
		playerData.name = this.m_minigameMetaData.creatorName;
		playerData.facebookId = this.m_minigameMetaData.creatorFacebookId;
		playerData.gameCenterId = this.m_minigameMetaData.creatorGameCenterId;
		playerData.countryCode = this.m_minigameMetaData.creatorCountryCode;
		return playerData;
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0004816D File Offset: 0x0004656D
	public bool IsOwnLevel()
	{
		return this.m_minigameMetaData.creatorId == PlayerPrefsX.GetUserId();
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00048184 File Offset: 0x00046584
	public virtual int GetVisualLikes()
	{
		return this.m_minigameMetaData.upThumbs;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00048191 File Offset: 0x00046591
	public virtual int GetVisualDislikes()
	{
		return this.m_minigameMetaData.downThumbs;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0004819E File Offset: 0x0004659E
	public virtual void WaitForUserToStart()
	{
		Debug.LogError("Override");
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x000481AA File Offset: 0x000465AA
	public virtual void BeginHeat()
	{
		Debug.LogError("Override");
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x000481B6 File Offset: 0x000465B6
	public virtual void BeginAdventure()
	{
		Debug.LogError("Override");
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x000481C2 File Offset: 0x000465C2
	public virtual void CancelBegin()
	{
		Debug.LogError("Override");
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x000481CE File Offset: 0x000465CE
	public virtual int GetPosition()
	{
		Debug.LogError("Override");
		return 0;
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x000481DB File Offset: 0x000465DB
	public virtual void CollectibleCollected()
	{
		PsState.m_activeGameLoop.m_gameMode.AddGhostEvent(GhostEventType.MapPieceCollected, Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
		PsState.m_activeMinigame.m_collectedStars++;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0004820F File Offset: 0x0004660F
	public virtual void SetPreviousLoop(PsGameLoop _loop)
	{
		this.m_previousLoop = _loop;
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x00048218 File Offset: 0x00046618
	public virtual void SetUnlockedProgressionName(string _name)
	{
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x0004821A File Offset: 0x0004661A
	public virtual string GetUnlockedProgressionName()
	{
		return string.Empty;
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00048221 File Offset: 0x00046621
	public virtual string GetWillUnlockProgressionName()
	{
		return string.Empty;
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00048228 File Offset: 0x00046628
	public virtual void StartLoop()
	{
		this.m_winFirstTime = false;
		if (PsState.m_activeGameLoop != null)
		{
			return;
		}
		this.SetAsActiveLoop();
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00048242 File Offset: 0x00046642
	public virtual void LoopUnlocked()
	{
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00048244 File Offset: 0x00046644
	public virtual void StartCancelled()
	{
		PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, true, false, false);
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00048263 File Offset: 0x00046663
	public virtual float GiveCheckpointReward()
	{
		return 0f;
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x0004826A File Offset: 0x0004666A
	public virtual void SelfDestructPlayer()
	{
		PsState.m_lastDeathReason = DeathReason.EJECT;
		PsState.m_activeMinigame.m_playerUnit.EmergencyKill();
		PsState.m_lastDeathReason = DeathReason.EJECT;
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00048287 File Offset: 0x00046687
	public virtual void ResumeSelfDestruct()
	{
		this.ResumeMinigame();
		this.SelfDestructPlayer();
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x00048298 File Offset: 0x00046698
	public virtual void LoadMinigameMetaData(Action _callback = null)
	{
		this.m_loadMetadataCallback = (Action)Delegate.Combine(this.m_loadMetadataCallback, _callback);
		Debug.LogWarning("------- LOAD ONLY MINIGAME METADATA: " + this.m_minigameId);
		if (!string.IsNullOrEmpty(this.m_minigameId) && this.m_minigameMetaData == null)
		{
			this.m_loadingMetaData = true;
			HttpC httpC = MiniGame.Get(this.m_minigameId, new Action<PsMinigameMetaData>(this.LoadOnlyMetaDataSUCCESS), new Action<HttpC>(this.LoadOnlyMetaDataFAILED), null);
			httpC.objectData = this.m_minigameId;
			Debug.LogWarning("------- LOAD METADATA: " + this.m_minigameId);
		}
		else if (string.IsNullOrEmpty(this.m_minigameId))
		{
			this.m_loadingMetaData = true;
			HttpC httpC2 = this.SearchMetadata();
			Debug.LogWarning("------- SEARCH METADATA");
		}
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x00048368 File Offset: 0x00046768
	protected virtual HttpC SearchMetadata()
	{
		if (this.m_minigameSearchParametres == null)
		{
			this.m_minigameSearchParametres = new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any);
		}
		HttpC httpC = MiniGame.SearchMinigames(this.m_minigameSearchParametres, new Action<PsMinigameMetaData[]>(this.SearchOnlyMetaDataSUCCESS), new Action<HttpC>(this.SearchOnlyMetaDataFAILED), null, 1);
		httpC.objectData = this.m_minigameSearchParametres;
		return httpC;
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x000483C4 File Offset: 0x000467C4
	protected void SearchOnlyMetaDataFAILED(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => MiniGame.SearchMinigames((MinigameSearchParametres)_c.objectData, new Action<PsMinigameMetaData[]>(this.SearchOnlyMetaDataSUCCESS), new Action<HttpC>(this.SearchOnlyMetaDataFAILED), null, 1), null);
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0004840C File Offset: 0x0004680C
	protected void LoadOnlyMetaDataFAILED(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => MiniGame.Get((string)_c.objectData, new Action<PsMinigameMetaData>(this.LoadOnlyMetaDataSUCCESS), new Action<HttpC>(this.LoadOnlyMetaDataFAILED), null), null);
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00048454 File Offset: 0x00046854
	protected virtual void SearchOnlyMetaDataSUCCESS(PsMinigameMetaData[] _minigameMetaData)
	{
		if (_minigameMetaData.Length > 0)
		{
			this.LoadOnlyMetaDataSUCCESS(_minigameMetaData[0]);
		}
		else
		{
			Debug.LogError("------- SERVER RETURNED EMPTY LEVEL LIST");
		}
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00048478 File Offset: 0x00046878
	protected virtual void LoadOnlyMetaDataSUCCESS(PsMinigameMetaData _minigameMetaData)
	{
		this.m_loadingMetaData = false;
		this.m_minigameMetaData = _minigameMetaData;
		this.m_minigameId = _minigameMetaData.id;
		this.m_gameModeFromProgressionData = this.m_minigameMetaData.gameMode;
		if (this.m_gameMode == null)
		{
			this.CreateGameMode();
		}
		this.m_scoreCurrent = 0;
		this.m_timeScoreCurrent = int.MaxValue;
		this.m_timeScoreBest = _minigameMetaData.timeScore;
		if (this.m_timeScoreBest == 0)
		{
			this.m_timeScoreBest = int.MaxValue;
		}
		this.m_timeScoreOld = this.m_timeScoreBest;
		this.m_scoreOld = this.m_scoreBest;
		this.m_rewardOld = this.m_scoreBest;
		if (this.m_path != null)
		{
			if (this.m_path.GetPathType() == PsPlanetPathType.main)
			{
				Hashtable hashtable = ClientTools.GenerateProgressionPathJson(this, this.m_path.m_currentNodeId, true, true, true);
				PsMetagameManager.SaveProgression(hashtable, this.m_path.m_planet, false);
			}
			else
			{
				Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_path);
				PsMetagameManager.SaveProgression(hashtable2, this.m_path.m_planet, false);
			}
		}
		if (this.m_loadMetadataCallback != null)
		{
			Debug.LogWarning("------- CALLING CALLBACKS:");
			Delegate[] invocationList = this.m_loadMetadataCallback.GetInvocationList();
			foreach (Delegate @delegate in invocationList)
			{
				Debug.LogWarning(@delegate.Method.Name);
			}
			this.m_loadMetadataCallback.Invoke();
		}
		this.m_loadMetadataCallback = null;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x000485E8 File Offset: 0x000469E8
	public virtual void LoadMinigame()
	{
		if (this.m_loaderEntity != null)
		{
			EntityManager.RemoveEntity(this.m_loaderEntity);
		}
		this.m_loaderEntity = null;
		this.m_loaderEntity = EntityManager.AddEntity();
		this.m_loaderEntity.m_persistent = true;
		if (!string.IsNullOrEmpty(this.m_minigameId) && this.m_minigameMetaData == null)
		{
			this.m_loadingMetaData = true;
			HttpC httpC = MiniGame.Get(this.m_minigameId, new Action<PsMinigameMetaData>(this.LoadMetaDataSUCCESS), new Action<HttpC>(this.LoadMetaDataFAILED), null);
			httpC.objectData = this.m_minigameId;
			EntityManager.AddComponentToEntity(this.m_loaderEntity, httpC);
			Debug.LogWarning("------- LOAD METADATA");
		}
		else if (string.IsNullOrEmpty(this.m_minigameId))
		{
			this.m_loadingMetaData = true;
			HttpC httpC2 = this.SearchMinigame();
			EntityManager.AddComponentToEntity(this.m_loaderEntity, httpC2);
			Debug.LogWarning("------- SEARCH METADATA");
		}
		else if (!string.IsNullOrEmpty(this.m_minigameId) && this.m_minigameMetaData != null)
		{
			this.LoadMinigameBytes();
		}
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x000486F4 File Offset: 0x00046AF4
	protected virtual HttpC SearchMinigame()
	{
		if (this.m_minigameSearchParametres == null)
		{
			this.m_minigameSearchParametres = new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any);
		}
		HttpC httpC = MiniGame.SearchMinigames(this.m_minigameSearchParametres, new Action<PsMinigameMetaData[]>(this.SearchMetaDataSUCCESS), new Action<HttpC>(this.SearchMetaDataFAILED), null, 1);
		httpC.objectData = this.m_minigameSearchParametres;
		return httpC;
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00048754 File Offset: 0x00046B54
	protected virtual void SearchMetaDataFAILED(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), () => MiniGame.SearchMinigames((MinigameSearchParametres)_c.objectData, new Action<PsMinigameMetaData[]>(this.SearchMetaDataSUCCESS), new Action<HttpC>(this.SearchMetaDataFAILED), null, 1), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x000487AC File Offset: 0x00046BAC
	protected virtual void LoadMetaDataFAILED(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), () => MiniGame.Get((string)_c.objectData, new Action<PsMinigameMetaData>(this.LoadMetaDataSUCCESS), new Action<HttpC>(this.LoadMetaDataFAILED), null), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00048803 File Offset: 0x00046C03
	protected virtual void SearchMetaDataSUCCESS(PsMinigameMetaData[] _minigameMetaData)
	{
		if (_minigameMetaData.Length > 0)
		{
			this.LoadMetaDataSUCCESS(_minigameMetaData[0]);
		}
		else
		{
			Debug.LogError("------- SERVER RETURNED EMPTY LEVEL LIST");
		}
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00048828 File Offset: 0x00046C28
	protected virtual void LoadMetaDataSUCCESS(PsMinigameMetaData _minigameMetaData)
	{
		this.m_loadingMetaData = false;
		this.m_minigameMetaData = _minigameMetaData;
		this.m_minigameId = _minigameMetaData.id;
		Debug.LogWarning(string.Concat(new object[]
		{
			this.m_minigameId,
			" ",
			this.m_levelNumber,
			" ",
			this.m_path.m_name
		}));
		if (this.m_gameMode == null)
		{
			this.CreateGameMode();
		}
		this.m_scoreCurrent = 0;
		this.m_timeScoreCurrent = int.MaxValue;
		this.m_timeScoreBest = _minigameMetaData.timeScore;
		if (this.m_timeScoreBest == 0)
		{
			this.m_timeScoreBest = int.MaxValue;
		}
		this.m_timeScoreOld = this.m_timeScoreBest;
		this.m_scoreOld = this.m_scoreBest;
		this.m_rewardOld = this.m_scoreBest;
		if (this.m_path.GetPathType() == PsPlanetPathType.main)
		{
			Hashtable hashtable = ClientTools.GenerateProgressionPathJson(this, this.m_path.m_currentNodeId, true, true, true);
			PsMetagameManager.SaveProgression(hashtable, this.m_path.m_planet, false);
		}
		else
		{
			Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_path);
			PsMetagameManager.SaveProgression(hashtable2, this.m_path.m_planet, false);
		}
		Debug.LogWarning("------- BEST TIME: " + HighScores.TimeScoreToTime(this.m_timeScoreBest));
		Debug.LogWarning("------- BEST SCORE: " + this.m_scoreBest);
		if (this.m_loadMetadataCallback != null)
		{
			Debug.LogWarning("------- CALLING CALLBACKS:");
			Delegate[] invocationList = this.m_loadMetadataCallback.GetInvocationList();
			foreach (Delegate @delegate in invocationList)
			{
				Debug.LogWarning(@delegate.Method.Name);
			}
			this.m_loadMetadataCallback.Invoke();
		}
		this.m_loadMetadataCallback = null;
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x000489F8 File Offset: 0x00046DF8
	protected virtual void LoadMinigameBytes()
	{
		if (this.m_minigameBytes == null && !this.m_loadingMinigameBytes)
		{
			PsMetrics.LevelLoadStart();
			Debug.LogWarning("------- LOAD BYTES " + this.m_minigameId);
			HttpC levelData = MiniGame.GetLevelData(this.m_minigameMetaData.id, new Action<byte[]>(this.MinigameDownloadOk), new Action<HttpC>(this.MinigameDownloadFailed), null);
			if (this.m_loaderEntity == null)
			{
				this.m_loaderEntity = EntityManager.AddEntity();
				this.m_loaderEntity.m_persistent = true;
			}
			EntityManager.AddComponentToEntity(this.m_loaderEntity, levelData);
			this.m_loadingMinigameBytes = true;
		}
		else if (this.m_minigameBytes != null)
		{
			this.MinigameDownloadOk(this.m_minigameBytes);
		}
		else
		{
			Debug.LogWarning("TRYING TO LOAD MINIGAMEBYTES, BUT NO LUCK");
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x00048AC1 File Offset: 0x00046EC1
	protected virtual void MinigameDownloadFailed(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), () => MiniGame.GetLevelData(this.m_minigameMetaData.id, new Action<byte[]>(this.MinigameDownloadOk), new Action<HttpC>(this.MinigameDownloadFailed), null), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00048AF4 File Offset: 0x00046EF4
	protected virtual void MinigameDownloadOk(byte[] _levelData)
	{
		this.m_loadingMinigameBytes = false;
		PsMetrics.LevelLoadFinished();
		if (_levelData.Length > 0)
		{
			this.m_minigameBytes = _levelData;
		}
		if (this.m_gameMode == null)
		{
			this.CreateGameMode();
		}
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00048B24 File Offset: 0x00046F24
	protected virtual void SaveMinigame(bool _exiting = false, Action _customExitAction = null)
	{
		Debug.Log(this.m_autosave, null);
		if (_exiting && PsState.m_adminMode && this.m_autosave)
		{
			new PsUIBasePopup(typeof(PsUICenterEditorSave), null, null, null, true, false, InitialPage.Center, false, false, false);
		}
		else if (!PsState.m_activeMinigame.m_changed)
		{
			Debug.LogWarning("level not changed");
			if (_exiting)
			{
				new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameExit, 0f, delegate
				{
					Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLevelEndLoadingScene(Color.black, this, 0.25f));
				}, null);
			}
		}
		else if (this.m_autosave)
		{
			Debug.LogWarning("level changed, saving");
			PsState.m_activeMinigame.m_changed = false;
			string[] array = new string[PsState.m_activeMinigame.m_itemCount.Keys.Count];
			int num = 0;
			foreach (string text in PsState.m_activeMinigame.m_itemCount.Keys)
			{
				array[num] = text;
				num++;
			}
			PsState.m_activeGameLoop.m_minigameMetaData.complexity = PsState.m_activeMinigame.m_complexity;
			PsState.m_activeGameLoop.m_minigameMetaData.levelRequirement = PsState.m_activeMinigame.m_levelRequirement;
			PsState.m_activeGameLoop.m_minigameMetaData.playerUnit = PsState.m_activeMinigame.m_playerUnitName;
			PsState.m_activeGameLoop.m_minigameMetaData.itemsUsed = array;
			PsState.m_activeGameLoop.m_minigameMetaData.itemsCount = PsState.m_activeMinigame.m_itemCount;
			PsState.m_activeGameLoop.m_minigameMetaData.timeSpentEditing += PsState.m_activeMinigame.GetTimeSinceInit();
			PsState.m_activeGameLoop.m_minigameMetaData.timeSpentInEditMode += PsState.m_activeMinigame.GetEditTimeSinceInit();
			PsState.m_activeGameLoop.m_minigameMetaData.lastPlaySessionStartCount = PsState.m_activeMinigame.m_gameStartCount;
			PsState.m_activeGameLoop.m_minigameMetaData.editSessionCount += PsState.m_activeMinigame.m_editSessionCount;
			PsState.m_activeGameLoop.m_minigameMetaData.groundsModificationCount += PsState.m_activeMinigame.m_groundsModificationCount;
			PsState.m_activeGameLoop.m_minigameMetaData.itemsModificationCount += PsState.m_activeMinigame.m_itemsModificationCount;
			PsState.m_activeMinigame.InitializeTimer();
			PsState.m_activeMinigame.ResetEditDateCounters();
			if (_exiting)
			{
				Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorSaveState(false));
			}
			else
			{
				this.SendLevelToServer();
			}
		}
		else if (_exiting)
		{
			if (_customExitAction != null)
			{
				_customExitAction.Invoke();
			}
			else
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLevelEndLoadingScene(Color.black, this, 0.25f));
			}
		}
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x00048E08 File Offset: 0x00047208
	protected HttpC SendLevelToServer()
	{
		if (PsState.UsingEditorResources())
		{
			PsMetagameManager.m_playerStats.CumulateEditorResources(EditorScene.m_reservedResources);
			EditorScene.m_reservedResources.Clear();
		}
		if (PsState.m_activeGameLoop.m_minigameMetaData.published)
		{
			PsState.m_activeGameLoop.m_minigameMetaData.id = string.Empty;
		}
		PsState.m_activeMinigame.SetLayerItems();
		string text = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
		return MiniGame.Save(PsState.m_activeGameLoop, null, PsMetagameManager.m_playerStats.GetUpdatedEditorResources(), new Action<HttpC>(this.LevelSendToServerOk), new Action<HttpC>(this.LevelSendToServerFailed), false, text, null);
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x00048EB0 File Offset: 0x000472B0
	protected void LevelSendToServerOk(HttpC _c)
	{
		PsMinigameMetaData psMinigameMetaData = ClientTools.ParseMinigameMetaData(_c);
		this.ProcessReceivedMinigameMetaData(psMinigameMetaData);
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x00048ECC File Offset: 0x000472CC
	protected void ProcessReceivedMinigameMetaData(PsMinigameMetaData _metaData)
	{
		PsState.m_activeGameLoop.m_minigameMetaData = _metaData;
		PsState.m_activeGameLoop.m_minigameId = _metaData.id;
		PsMetrics.LevelSaved();
		Debug.Log("MINIGAME SAVED", null);
		Debug.Log("TIME EDITED: " + _metaData.timeSpentEditing, null);
		PsMetricsData.m_timeInEditor += PsState.m_activeMinigame.GetTimeSinceInit();
		PsCaches.m_savedList.Clear();
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x00048F3E File Offset: 0x0004733E
	protected void LevelSendToServerFailed(HttpC _c)
	{
		Debug.LogError("MINIGAME SAVE FAILED");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, new Func<HttpC>(this.SendLevelToServer), null);
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00048F6C File Offset: 0x0004736C
	public virtual void CreateGameMode()
	{
		if (this.m_minigameMetaData.gameMode == PsGameMode.Race)
		{
			this.m_gameMode = new PsGameModeRace(this);
		}
		else if (this.m_minigameMetaData.gameMode == PsGameMode.StarCollect)
		{
			this.m_gameMode = new PsGameModeAdventure(this);
		}
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00048FB8 File Offset: 0x000473B8
	public virtual void LoadGhosts()
	{
		Debug.LogWarning("LOAD GHOST");
		this.m_gameMode.LoadGhosts(null);
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x00048FD0 File Offset: 0x000473D0
	public virtual void GhostsLoaded()
	{
		if (string.IsNullOrEmpty(this.m_playerData.playerId))
		{
			HttpC playerProfile = Player.GetPlayerProfile(this.GetCreatorId(), new Action<PlayerData>(this.PlayerDataLoaded), new Action<HttpC>(this.PlayerLoadFailed), null);
			EntityManager.AddComponentToEntity(this.m_loaderEntity, playerProfile);
		}
		else
		{
			this.PlayerDataLoaded(this.m_playerData);
		}
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x00049036 File Offset: 0x00047436
	public virtual void PlayerLoadFailed(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), () => Player.GetPlayerProfile(this.GetCreatorId(), new Action<PlayerData>(this.PlayerDataLoaded), new Action<HttpC>(this.PlayerLoadFailed), null), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00049069 File Offset: 0x00047469
	public virtual void PlayerDataLoaded(PlayerData _data)
	{
		this.m_playerData = _data;
		if (Main.m_currentGame.m_sceneManager.m_loadingScene is PsRacingLoadingScene)
		{
			(Main.m_currentGame.m_sceneManager.m_loadingScene as PsRacingLoadingScene).LevelLoaded();
		}
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x000490A4 File Offset: 0x000474A4
	public virtual void CreatePlaybackGhosts()
	{
		Debug.LogWarning("CREATE PLAYBACK GHOST VISUALS");
		this.m_gameMode.CreatePlaybackGhostVisuals();
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x000490BB File Offset: 0x000474BB
	public virtual void PreviewLoop()
	{
		Debug.LogError("OVERRIDE!");
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x000490C7 File Offset: 0x000474C7
	public void StartElapsedTimer()
	{
		this.m_startTime = DateTime.UtcNow;
		this.m_elapsedtime = 0L;
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x000490DC File Offset: 0x000474DC
	public void PauseElapsedTimer()
	{
		this.m_elapsedtime = this.ElapsedTime();
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x000490EA File Offset: 0x000474EA
	public void ResumeElapsedTimer()
	{
		this.m_startTime = DateTime.UtcNow;
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x000490F8 File Offset: 0x000474F8
	public virtual void EnteredMinigameScene()
	{
		PsMetagameManager.HideResources();
		this.StartElapsedTimer();
		this.CreateMinigame();
		this.CreateEnvironment();
		this.InitializeMinigame();
		PsMetrics.MinigameStarted(PsState.m_activeGameLoop);
		if (Main.m_currentGame.m_sceneManager.m_loadingScene == null)
		{
			this.EnterMinigame();
		}
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x00049148 File Offset: 0x00047548
	public virtual void CreateMinigame()
	{
		LevelManager.LoadLevel(FilePacker.UnZipBytes(this.m_minigameBytes), true);
		PsState.m_activeMinigame = LevelManager.m_currentLevel as Minigame;
		if (LevelManager.m_currentLevel != null)
		{
			PsState.m_activeMinigame.m_groundNode = LevelManager.m_currentLevel.m_currentLayer.GetElement("LevelGround") as LevelGroundNode;
			this.m_gameMode.ApplySettings(true, false);
		}
		else
		{
			Debug.LogError("ERROR LOADING LEVEL FROM BYTES!!");
		}
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x000491BE File Offset: 0x000475BE
	public virtual void CreateEnvironment()
	{
		this.m_gameMode.CreateEnvironment();
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x000491CB File Offset: 0x000475CB
	public virtual void EnterEditor()
	{
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x000491CD File Offset: 0x000475CD
	public virtual void InitializeEditor()
	{
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x000491CF File Offset: 0x000475CF
	public virtual void ExitEditor(bool _published, Action _customExitAction = null)
	{
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x000491D1 File Offset: 0x000475D1
	public virtual void EnterMinigame()
	{
		Debug.LogError("OVERRIDE!");
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x000491DD File Offset: 0x000475DD
	public virtual void StartEditorRecording()
	{
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x000491E0 File Offset: 0x000475E0
	public virtual void InitializeMinigame()
	{
		PsState.m_physicsPaused = true;
		PsState.m_activeMinigame.m_gameOn = false;
		PsState.m_activeMinigame.m_gameTicks = 0f;
		PsState.m_activeMinigame.m_realTimeSpent = 0f;
		PsState.m_activeMinigame.m_gameEnded = false;
		PsState.m_activeMinigame.m_gameStarted = false;
		PsState.m_activeMinigame.m_gamePaused = false;
		PsState.m_activeMinigame.m_gameTicksFreezed = false;
		PsState.m_activeMinigame.m_playerBeamingOut = false;
		PsState.m_activeMinigame.m_collectedStars = 0;
		PsState.m_activeMinigame.m_gravityMultipler = 1;
		CameraS.SnapMainCameraFrame();
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x00049270 File Offset: 0x00047670
	public virtual void SetPlaybackGhostAndCoins(bool _regenerateCoins = true)
	{
		this.m_gameMode.CreatePlaybackGhostVisuals();
		if (this.m_gameMode.m_playbackGhosts.Count > 0)
		{
			if (_regenerateCoins)
			{
				if (this.m_gameMode.m_gameLoop.m_context == PsMinigameContext.Fresh)
				{
					this.m_gameMode.SetCoinStreakStyle(CoinStreakStyle.ALL_SHARDS);
				}
				else if (PsMetagameManager.IsTimedGiftActive(EventGiftTimedType.goldCoinStreak))
				{
					this.m_gameMode.SetCoinStreakStyle(CoinStreakStyle.ALL_GOLD);
				}
				else
				{
					this.m_gameMode.SetCoinStreakStyle(CoinStreakStyle.BASIC);
				}
				this.m_gameMode.GenerateCollectableCoins();
			}
			this.m_gameMode.PlaceCoins();
			Debug.LogWarning("PLACE COINS");
		}
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00049313 File Offset: 0x00047713
	public virtual void StartMinigame()
	{
		PsState.m_activeMinigame.m_collectedCoinsForDoubleUp = 0;
		if (PlayerPrefsX.GetGifDeathRecording())
		{
			this.StartGIFRecord();
		}
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00049338 File Offset: 0x00047738
	public virtual void PauseMinigame()
	{
		if (PsMetagameManager.m_tutorialArrow != null)
		{
			PsUITutorialArrowUI psUITutorialArrowUI = PsMetagameManager.m_tutorialArrow as PsUITutorialArrowUI;
			if (psUITutorialArrowUI != null && psUITutorialArrowUI.m_callBack != null)
			{
				psUITutorialArrowUI.m_callBack.Invoke();
			}
			PsMetagameManager.m_tutorialArrow.Destroy();
			PsMetagameManager.m_tutorialArrow = null;
		}
		GifMaker.PauseRecord(true);
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0004938C File Offset: 0x0004778C
	public virtual void ResumeMinigame()
	{
		GifMaker.PauseRecord(false);
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x00049394 File Offset: 0x00047794
	public virtual void RestartMinigame()
	{
		this.StopGIFRecord(0);
		this.m_sendQuit = true;
		this.m_boosterUsed = false;
		PsState.m_activeMinigame.RemoveTimeScale();
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x000493B5 File Offset: 0x000477B5
	public virtual void LoseMinigame()
	{
		this.StopGIFRecord(210);
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x000493C4 File Offset: 0x000477C4
	public virtual void WinMinigame()
	{
		this.StopGIFRecord(0);
		if ((this.m_timeScoreBest == 0 || this.m_timeScoreBest == 2147483647) && this.m_scoreOld == 0)
		{
			this.m_winFirstTime = true;
		}
		this.m_sendQuit = false;
		PsState.m_activeMinigame.RemoveTimeScale();
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x00049416 File Offset: 0x00047816
	public virtual void SaveProgression(bool _save = true)
	{
		Debug.LogError("OVERRIDE!");
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00049422 File Offset: 0x00047822
	public virtual void ExitMinigame()
	{
		GifMaker.FreeMem();
		this.m_gameMode.CheckAchievementsOnEnd();
		this.m_levelWasUnlocked = false;
		this.m_loadingMinigameBytes = false;
		this.m_loadingMetaData = false;
		this.m_boosterUsed = false;
		this.Exit();
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x00049456 File Offset: 0x00047856
	public void Exit()
	{
		PsState.m_activeMinigame.RemoveTimeScale();
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x00049464 File Offset: 0x00047864
	public virtual void SkipMinigame()
	{
		this.m_winFirstTime = true;
		PsMetrics.MinigameSkipped(PsState.m_activeGameLoop.m_context.ToString(), PsState.m_activeGameLoop.m_minigameMetaData, PsState.m_activeGameLoop.ElapsedTime());
		FrbMetrics.SpendVirtualCurrency("level_skip", "gems", (double)PsMetagameManager.m_skipPrice);
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x000494C0 File Offset: 0x000478C0
	public virtual void DestroyMinigame()
	{
		GhostManager.Free();
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x000494C7 File Offset: 0x000478C7
	public virtual void BackAtMenu()
	{
		Debug.LogError("OVERRIDE!");
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x000494D4 File Offset: 0x000478D4
	public void IncreaseItemLevel(string _unlockIdentifier)
	{
		if (!string.IsNullOrEmpty(_unlockIdentifier))
		{
			PsUnlock unlockByIdentifier = PsMetagameData.GetUnlockByIdentifier(_unlockIdentifier, this.m_path.m_planet);
			if (unlockByIdentifier != null)
			{
				PsMetagameData.Unlock(unlockByIdentifier);
				int num = 0;
				this.m_unlockableList = new List<PsUnlockable>();
				if (unlockByIdentifier.m_unlockables != null)
				{
					for (int i = 0; i < unlockByIdentifier.m_unlockables.Count; i++)
					{
						Debug.LogWarning(unlockByIdentifier.m_unlockables[i].m_name + ", ITEM LEVEL: " + unlockByIdentifier.m_unlockables[i].m_itemLevel);
						if (unlockByIdentifier.m_unlockables[i].m_itemLevel > num)
						{
							num = unlockByIdentifier.m_unlockables[i].m_itemLevel;
						}
						if (unlockByIdentifier.m_unlockables[i].m_container.m_type != PsUnlockableType.Menu)
						{
							Debug.LogWarning("TO UNLOCK SCREEN: " + unlockByIdentifier.m_unlockables[i].m_name);
							this.m_unlockableList.Add(unlockByIdentifier.m_unlockables[i]);
						}
					}
				}
				Debug.LogWarning(string.Concat(new object[]
				{
					"ITEM LEVEL: ",
					num,
					", OLD ITEM LEVEL: ",
					PsMetagameManager.m_playerStats.itemLevel
				}));
				if (num > PsMetagameManager.m_playerStats.itemLevel)
				{
					PsMetagameManager.m_playerStats.itemLevel = num;
					Debug.LogWarning("SETTING ITEM LEVEL TO: " + num);
				}
				if (this.m_unlockableList.Count > 0)
				{
					PsMenuScene.m_lastState = "PsResearchUnlockState";
				}
			}
		}
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x00049678 File Offset: 0x00047A78
	public void StartGIFRecord()
	{
		Debug.Log("Started recording GIF", null);
		if (GifMaker.IsRecording)
		{
			GifMaker.StopRecord();
		}
		this.m_recordingGif = true;
		GifMaker.GifRecordOptions gifRecordOptions = new GifMaker.GifRecordOptions(Camera.main, 0.1f, new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 256, 256, null, null, 50);
		GifMaker.StartRecord(gifRecordOptions);
		this.framesUntilStop = 0;
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x000496F0 File Offset: 0x00047AF0
	public void UpdateGIFRecord()
	{
		if (this.m_recordingGif && this.framesUntilStop > 0 && !GifMaker.IsPaused)
		{
			this.framesUntilStop--;
			if (this.framesUntilStop == 0)
			{
				this.StopGIFRecord(0);
			}
		}
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0004973E File Offset: 0x00047B3E
	public void StopGIFRecord(int _stopDelay = 0)
	{
		if (_stopDelay > 0)
		{
			this.framesUntilStop = _stopDelay;
			return;
		}
		this.framesUntilStop = 0;
		this.m_recordingGif = false;
		Debug.Log("Stopped recording GIF", null);
		GifMaker.StopRecord();
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x00049770 File Offset: 0x00047B70
	public void CleanLoop()
	{
		this.m_released = true;
		if (this.m_gameMode != null)
		{
			this.m_gameMode.Destroy();
			this.m_gameMode = null;
		}
		if (this.m_loaderEntity != null)
		{
			EntityManager.RemoveEntity(this.m_loaderEntity);
		}
		this.m_loaderEntity = null;
		this.m_minigameBytes = null;
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x000497C5 File Offset: 0x00047BC5
	public virtual void ReleaseLoop()
	{
		Debug.LogWarning("LOOP RELEASED");
		this.CleanLoop();
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x000497D8 File Offset: 0x00047BD8
	public virtual void SetAsActiveLoop()
	{
		if (PsState.m_activeGameLoop != null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"trying to set ",
				this,
				" as active loop, but active loop is already set: ",
				PsState.m_activeGameLoop
			}));
		}
		Debug.LogWarning(this + ": is PsState.m_activeLoop");
		PsState.m_activeGameLoop = this;
		PsState.m_activeGameLoop.m_released = false;
		this.m_userHasSelectedVehicle = false;
		this.m_selectedVehicle = PsSelectedVehicle.None;
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0004984A File Offset: 0x00047C4A
	public virtual void SetActiveLoopNull()
	{
		if (PsState.m_activeGameLoop == null)
		{
			Debug.LogError("trying to set active loop to null, but it already is null");
		}
		Debug.LogWarning(this + ": setting PsState.m_activeLoop to null");
		PsState.m_activeGameLoop = null;
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00049878 File Offset: 0x00047C78
	public virtual void ExitLoadingScene()
	{
		this.m_loadingMetaData = false;
		this.m_loadMetadataCallback = null;
		Main.m_currentGame.m_sceneManager.StopSceneChange();
		if (this.m_loaderEntity != null)
		{
			EntityManager.RemoveEntity(this.m_loaderEntity);
		}
		this.m_loaderEntity = null;
		this.m_loadingMinigameBytes = false;
		this.m_loaderEntity = null;
		this.ReleaseLoop();
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x000498D4 File Offset: 0x00047CD4
	public virtual void ActivateCurrentLoop()
	{
		PsGameLoop currentNodeInfo = this.m_path.GetCurrentNodeInfo();
		if (currentNodeInfo != null && currentNodeInfo.m_node != null)
		{
			currentNodeInfo.m_node.Activate();
		}
		if (this.m_sidePath != null)
		{
			PsGameLoop currentNodeInfo2 = this.m_sidePath.GetCurrentNodeInfo();
			if (currentNodeInfo2 != null && currentNodeInfo2.m_node != null)
			{
				currentNodeInfo2.m_node.Activate();
			}
		}
		if (this.m_winFirstTime && currentNodeInfo != null && !currentNodeInfo.m_activated)
		{
			currentNodeInfo.SaveAsActivated();
			new PsMenuDialogueFlow(currentNodeInfo, PsNodeEventTrigger.LoopActivated, 0.1f, null, true);
		}
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0004996D File Offset: 0x00047D6D
	public virtual void SaveAsActivated()
	{
		if (!this.m_activated)
		{
			this.m_activated = true;
		}
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x00049981 File Offset: 0x00047D81
	public virtual int GetLevelRequirement()
	{
		return this.m_minigameMetaData.levelRequirement;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0004998E File Offset: 0x00047D8E
	public virtual string GetCreatorName()
	{
		return this.m_minigameMetaData.creatorName;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0004999B File Offset: 0x00047D9B
	public virtual string GetCreatorCountryCode()
	{
		return this.m_minigameMetaData.creatorCountryCode;
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x000499A8 File Offset: 0x00047DA8
	public virtual string GetCreatorId()
	{
		return this.m_minigameMetaData.creatorId;
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x000499B5 File Offset: 0x00047DB5
	public virtual string GetName()
	{
		return this.m_minigameMetaData.name;
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x000499C2 File Offset: 0x00047DC2
	public virtual string GetFacebookId()
	{
		return this.m_minigameMetaData.creatorFacebookId;
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x000499CF File Offset: 0x00047DCF
	public virtual string GetGamecenterId()
	{
		return this.m_minigameMetaData.creatorGameCenterId;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x000499DC File Offset: 0x00047DDC
	public virtual string GetPlayerUnit()
	{
		return this.m_minigameMetaData.playerUnit;
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x000499E9 File Offset: 0x00047DE9
	public virtual PsRating GetRating()
	{
		return this.m_minigameMetaData.rating;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x000499F6 File Offset: 0x00047DF6
	public virtual PsGameDifficulty GetDifficulty()
	{
		return this.m_minigameMetaData.difficulty;
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x00049A03 File Offset: 0x00047E03
	public virtual string GetGameId()
	{
		if (string.IsNullOrEmpty(this.m_minigameMetaData.id))
		{
			Debug.LogError("Minigame ID should not be null!");
		}
		return this.m_minigameMetaData.id;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x00049A2F File Offset: 0x00047E2F
	public virtual string GetTournamentId()
	{
		return null;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x00049A32 File Offset: 0x00047E32
	public virtual string GetDescription()
	{
		return this.m_minigameMetaData.description;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x00049A3F File Offset: 0x00047E3F
	public virtual void SetRating(PsRating _newRating)
	{
		this.m_minigameMetaData.rating = _newRating;
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x00049A4D File Offset: 0x00047E4D
	public virtual int GetTime()
	{
		return this.m_minigameMetaData.timeScore;
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x00049A5A File Offset: 0x00047E5A
	public virtual PsGameMode GetGameMode()
	{
		return this.m_minigameMetaData.gameMode;
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x00049A67 File Offset: 0x00047E67
	public virtual bool IsGameMode(PsGameMode _gameMode)
	{
		return this.m_minigameMetaData.gameMode == _gameMode;
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x00049A7D File Offset: 0x00047E7D
	public virtual int GetScore()
	{
		return this.m_scoreBest;
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x00049A85 File Offset: 0x00047E85
	protected void StopEveryplayAfterDelay()
	{
		EveryplayManager.StopRecording(10f);
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x00049A94 File Offset: 0x00047E94
	public long ElapsedTime()
	{
		return this.m_elapsedtime + Convert.ToInt64((DateTime.UtcNow - this.m_startTime).TotalSeconds);
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x00049AC5 File Offset: 0x00047EC5
	public virtual Hashtable GetRentUpgradeValues()
	{
		return this.m_minigameMetaData.creatorUpgrades;
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x00049AD2 File Offset: 0x00047ED2
	public virtual Type GetRentVehicleType()
	{
		return Type.GetType(this.m_minigameMetaData.playerUnit);
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x00049AE4 File Offset: 0x00047EE4
	public virtual List<float> GetRentVehicleValues()
	{
		return PsState.m_activeMinigame.m_playerUnit.ParseUpgradeValues(PsState.m_activeGameLoop.GetRentUpgradeValues());
	}

	// Token: 0x04000712 RID: 1810
	public PlayerData m_playerData;

	// Token: 0x04000713 RID: 1811
	public PsPlanet m_planet;

	// Token: 0x04000714 RID: 1812
	public PsMinigameContext m_context;

	// Token: 0x04000715 RID: 1813
	public PsGameLoop m_previousLoop;

	// Token: 0x04000716 RID: 1814
	public bool m_createFlyingNode;

	// Token: 0x04000717 RID: 1815
	public bool m_exitingMinigame;

	// Token: 0x04000718 RID: 1816
	public bool m_recordingGif;

	// Token: 0x04000719 RID: 1817
	public int m_raceGhostCount;

	// Token: 0x0400071A RID: 1818
	public PsPlanetPath m_path;

	// Token: 0x0400071B RID: 1819
	public int m_nodeId;

	// Token: 0x0400071C RID: 1820
	public int m_levelNumber;

	// Token: 0x0400071D RID: 1821
	public PsPlanetPath m_sidePath;

	// Token: 0x0400071E RID: 1822
	public bool m_levelWasUnlocked;

	// Token: 0x0400071F RID: 1823
	private bool unlocked;

	// Token: 0x04000720 RID: 1824
	public bool m_sendQuit;

	// Token: 0x04000721 RID: 1825
	public bool m_freeUnlock;

	// Token: 0x04000722 RID: 1826
	public bool m_adRent;

	// Token: 0x04000723 RID: 1827
	public bool m_activated;

	// Token: 0x04000724 RID: 1828
	public PsGameMode m_gameModeFromProgressionData;

	// Token: 0x04000725 RID: 1829
	public string m_minigameId;

	// Token: 0x04000726 RID: 1830
	public MinigameSearchParametres m_minigameSearchParametres;

	// Token: 0x04000727 RID: 1831
	public PsMinigameMetaData m_minigameMetaData;

	// Token: 0x04000728 RID: 1832
	public byte[] m_minigameBytes;

	// Token: 0x04000729 RID: 1833
	public VersusMetaData m_versusData;

	// Token: 0x0400072A RID: 1834
	public string[] m_forcedMedalTimes;

	// Token: 0x0400072B RID: 1835
	public PsGameModeBase m_gameMode;

	// Token: 0x0400072C RID: 1836
	public int m_scoreCurrent;

	// Token: 0x0400072D RID: 1837
	public int m_scoreBest;

	// Token: 0x0400072E RID: 1838
	public int m_scoreOld;

	// Token: 0x0400072F RID: 1839
	public int m_rewardOld;

	// Token: 0x04000730 RID: 1840
	public int m_currentRunScore;

	// Token: 0x04000731 RID: 1841
	public int m_timeScoreCurrent;

	// Token: 0x04000732 RID: 1842
	public int m_timeScoreBest;

	// Token: 0x04000733 RID: 1843
	public int m_timeScoreOld;

	// Token: 0x04000734 RID: 1844
	public bool m_boosterUsed;

	// Token: 0x04000735 RID: 1845
	public bool m_winFirstTime;

	// Token: 0x04000736 RID: 1846
	public Hashtable m_dialogues;

	// Token: 0x04000737 RID: 1847
	public string m_temporaryUnlock = string.Empty;

	// Token: 0x04000738 RID: 1848
	private bool m_unlockLoopAfterLoading;

	// Token: 0x04000739 RID: 1849
	public bool m_loadingMetaData;

	// Token: 0x0400073A RID: 1850
	public bool m_loadingMinigameBytes;

	// Token: 0x0400073B RID: 1851
	public bool m_rentedVehicle;

	// Token: 0x0400073C RID: 1852
	public List<float> m_rentUpgradeValues;

	// Token: 0x0400073D RID: 1853
	public bool m_userHasSelectedVehicle;

	// Token: 0x0400073E RID: 1854
	public PsSelectedVehicle m_selectedVehicle;

	// Token: 0x0400073F RID: 1855
	public string m_freeConsumableUnlock = string.Empty;

	// Token: 0x04000740 RID: 1856
	public PsPlanetNode m_node;

	// Token: 0x04000741 RID: 1857
	public bool m_keepNodeHidden;

	// Token: 0x04000742 RID: 1858
	public float m_roadDrawStart;

	// Token: 0x04000743 RID: 1859
	public bool m_drawRoad;

	// Token: 0x04000744 RID: 1860
	public float m_roadScaleStart;

	// Token: 0x04000745 RID: 1861
	public bool m_scaleRoad;

	// Token: 0x04000746 RID: 1862
	public Action m_loadMetadataCallback;

	// Token: 0x04000747 RID: 1863
	protected DateTime m_startTime;

	// Token: 0x04000748 RID: 1864
	protected long m_elapsedtime;

	// Token: 0x04000749 RID: 1865
	public bool m_backAtMenu;

	// Token: 0x0400074A RID: 1866
	public bool m_released;

	// Token: 0x0400074B RID: 1867
	protected bool m_autosave;

	// Token: 0x0400074C RID: 1868
	public Entity m_loaderEntity;

	// Token: 0x0400074D RID: 1869
	public string m_loopStartLoginTime;

	// Token: 0x0400074E RID: 1870
	public string m_loopStartSessionId;

	// Token: 0x0400074F RID: 1871
	protected float m_overrideCC = -1f;

	// Token: 0x04000750 RID: 1872
	protected bool m_useCreatorUpgrades;

	// Token: 0x04000751 RID: 1873
	public List<PsUnlockable> m_unlockableList;

	// Token: 0x04000752 RID: 1874
	private int framesUntilStop;
}
