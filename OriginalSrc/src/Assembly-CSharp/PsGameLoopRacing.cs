using System;
using System.Collections;
using Server;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class PsGameLoopRacing : PsGameLoop
{
	// Token: 0x06000705 RID: 1797 RVA: 0x0004F39C File Offset: 0x0004D79C
	public PsGameLoopRacing(PsMinigameMetaData _data)
		: this(_data.id, new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any), null, -1, -1, _data.score, false, 0, false, true, 0, null, 0, false, null, null, 0)
	{
		this.m_minigameMetaData = _data;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0004F3DC File Offset: 0x0004D7DC
	public PsGameLoopRacing(string _minigameId, MinigameSearchParametres _searchParametres, PsPlanetPath _path, int _id, int _levelNumber, int _score, bool _unlocked, int _currentHeat, bool _wonTrophyGhost, bool _practiceDisabled, int _secondarysWon, string _trophyGhostIDs, int _raceGhostCount, bool _trophiesRewarded, string[] _medalTimes = null, string _fixedTrophies = null, int _purchasedRuns = 0)
		: base(PsMinigameContext.Level, _minigameId, _path, _id, _levelNumber, _score, _unlocked, _medalTimes)
	{
		this.m_minigameSearchParametres = _searchParametres;
		string text = _path.m_planet.Replace("Racing", string.Empty);
		this.m_minigameSearchParametres.m_playerUnitFilter = text;
		this.m_minigameSearchParametres.m_gameMode = PsGameMode.Race;
		this.m_heatNumber = _currentHeat;
		this.m_ghostWon = _wonTrophyGhost;
		this.m_practiceDisabled = _practiceDisabled;
		this.m_trophyGhostIds = _trophyGhostIDs;
		this.m_secondarysWon = _secondarysWon;
		this.m_initialSecondarysWon = this.m_secondarysWon;
		this.m_raceGhostCount = _raceGhostCount;
		this.m_trophiesRewarded = _trophiesRewarded;
		this.m_initialTrophiesRewarded = this.m_trophiesRewarded;
		this.m_briefingShown = this.m_trophiesRewarded;
		this.m_fixedTrophies = _fixedTrophies;
		this.m_purchasedRuns = _purchasedRuns;
		if (_id < this.m_path.m_currentNodeId)
		{
			this.m_trophiesRewarded = true;
			this.m_initialTrophiesRewarded = true;
			this.m_briefingShown = true;
		}
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0004F4CB File Offset: 0x0004D8CB
	public override float GetOverrideCC()
	{
		if (this.m_minigameMetaData != null)
		{
			return this.m_minigameMetaData.overrideCC;
		}
		return -1f;
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0004F4EC File Offset: 0x0004D8EC
	public override int GetPosition()
	{
		if (this.m_heatNumber <= 1)
		{
			return 0;
		}
		if (this.m_secondarysWon >= 4 || this.m_ghostWon)
		{
			return 1;
		}
		if (this.m_secondarysWon >= 2 || this.m_raceGhostCount < 2)
		{
			return 2;
		}
		if (this.m_secondarysWon == 1 || this.m_raceGhostCount < 3)
		{
			return 3;
		}
		return 4;
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0004F555 File Offset: 0x0004D955
	public override void StartLoop()
	{
		this.m_winFirstTime = false;
		if (PsState.m_activeGameLoop != null)
		{
			return;
		}
		this.m_backAtMenu = false;
		this.LoopUnlocked();
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0004F578 File Offset: 0x0004D978
	public override void LoopUnlocked()
	{
		if (this.m_nodeId == this.m_path.m_currentNodeId && !base.m_unlocked)
		{
			base.m_unlocked = true;
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
		if (this.m_nodeId <= this.m_path.m_currentNodeId && !Input.GetKey(304) && (!PsState.m_adminMode || !PsState.m_adminModeSkipping))
		{
			base.m_unlocked = true;
			new PsMenuDialogueFlow(this, PsNodeEventTrigger.LoopStart, 0f, delegate
			{
				this.SetAsActiveLoop();
				if (this.m_nodeId < this.m_path.m_currentNodeId)
				{
					this.LoadMinigame();
				}
				else
				{
					if (this.m_node != null)
					{
						this.m_node.m_hideUI = true;
					}
					TimerS.AddComponent(EntityManager.AddEntity(), "Timer", 0.3f, 0f, true, new TimerComponentDelegate(this.FadeInLoadingScreen));
				}
			}, true);
		}
		else if (Input.GetKey(304) || (PsState.m_adminMode && PsState.m_adminModeSkipping))
		{
			base.m_unlocked = true;
			this.m_backAtMenu = false;
			new PsMenuDialogueFlow(this, PsNodeEventTrigger.LoopStart, 0f, delegate
			{
				this.SetAsActiveLoop();
				this.m_ghostWon = true;
				this.SaveProgression(true);
				this.BackAtMenu();
			}, true);
		}
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0004F6B1 File Offset: 0x0004DAB1
	public void FadeInLoadingScreen(TimerC _c)
	{
		this.LoadMinigame();
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0004F6BC File Offset: 0x0004DABC
	public override void LoadMinigame()
	{
		if (this.m_loadingMetaData)
		{
			Debug.LogWarning("------ ALREADY LOADING MINIGAME META DATA. ADDING TO CALLBACK");
			if (this.m_loadMetadataCallback != null)
			{
				this.m_loadMetadataCallback = (Action)Delegate.Combine(this.m_loadMetadataCallback, new Action(this.LoadMinigame));
			}
			else
			{
				this.m_loadMetadataCallback = new Action(this.LoadMinigame);
			}
			return;
		}
		base.LoadMinigame();
		this.PreviewLoop();
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0004F734 File Offset: 0x0004DB34
	public override void LoadMinigameMetaData(Action _callback)
	{
		this.m_loadMetadataCallback = (Action)Delegate.Combine(this.m_loadMetadataCallback, _callback);
		Debug.LogWarning("------- LOAD ONLY MINIGAME METADATA: " + this.m_minigameId);
		if (this.m_minigameId != string.Empty && this.m_minigameMetaData == null)
		{
			this.m_loadingMetaData = true;
			HttpC httpC = MiniGame.Get(this.m_minigameId, new Action<PsMinigameMetaData>(this.LoadOnlyMetaDataSUCCESS), new Action<HttpC>(base.LoadOnlyMetaDataFAILED), null);
			httpC.objectData = this.m_minigameId;
			Debug.LogWarning("------- LOAD METADATA: " + this.m_minigameId);
		}
		else if (string.IsNullOrEmpty(this.m_minigameId))
		{
			this.m_loadingMetaData = true;
			if (this.m_minigameSearchParametres == null)
			{
				this.m_minigameSearchParametres = new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any);
			}
			HttpC httpC2 = MiniGame.SearchMinigames(this.m_minigameSearchParametres, new Action<PsMinigameMetaData[]>(this.SearchOnlyMetaDataSUCCESS), new Action<HttpC>(base.SearchOnlyMetaDataFAILED), null, 1);
			Debug.LogWarning("------- SEARCH METADATA");
		}
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0004F843 File Offset: 0x0004DC43
	protected override void LoadOnlyMetaDataSUCCESS(PsMinigameMetaData _minigameMetaData)
	{
		base.LoadOnlyMetaDataSUCCESS(_minigameMetaData);
		this.m_timeScoreBest = int.MaxValue;
		this.m_timeScoreOld = this.m_timeScoreBest;
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0004F863 File Offset: 0x0004DC63
	protected override void LoadMetaDataSUCCESS(PsMinigameMetaData _minigameMetaData)
	{
		base.LoadMetaDataSUCCESS(_minigameMetaData);
		this.m_timeScoreBest = int.MaxValue;
		this.m_timeScoreOld = this.m_timeScoreBest;
		this.LoadMinigameBytes();
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0004F889 File Offset: 0x0004DC89
	public override void PreviewLoop()
	{
		Main.m_currentGame.m_sceneManager.ChangeScene(new GameScene("GameScene", null), new PsRacingLoadingScene());
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0004F8AA File Offset: 0x0004DCAA
	protected override void MinigameDownloadOk(byte[] _levelData)
	{
		base.MinigameDownloadOk(_levelData);
		(this.m_gameMode as PsGameModeRacing).CalculateMedalTimes();
		this.LoadGhosts();
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0004F8C9 File Offset: 0x0004DCC9
	public override void CreateGameMode()
	{
		this.m_gameMode = new PsGameModeRacing(this);
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0004F8D8 File Offset: 0x0004DCD8
	public override void EnterMinigame()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		if (this.m_minigameMetaData.playerUnit != null)
		{
			text = this.m_minigameMetaData.playerUnit;
		}
		if (this.m_gameMode != null)
		{
			text2 = this.m_gameMode.ToString();
		}
		PsMetrics.LevelEntered(text, text2, "level_racing");
		FrbMetrics.SetCurrentScreen("level_racing");
		FrbMetrics.LevelEntered();
		this.m_initialGachaFull = PsMetagameManager.m_vehicleGachaData.m_rivalWonCount >= 4;
		this.m_rivalWon = (this.m_initialRivalWon = this.GetPosition() > 0 && this.GetPosition() <= this.m_rivalPos);
		this.m_startPosition = this.GetPosition();
		Debug.LogWarning("ENTER MINIGAME");
		PsGameLoop nodeInfo = this.m_path.GetNodeInfo(this.m_nodeId + 1);
		if (nodeInfo != null && nodeInfo.m_minigameMetaData == null)
		{
			nodeInfo.LoadMinigameMetaData(null);
		}
		CameraS.m_updateComponents = true;
		CameraS.m_zoomMultipler = 0f;
		CameraS.m_zoomNeutralizer = 0.985f;
		GameLevelPreview.InitLevelPreview(true);
		this.m_initialTrophiesRewarded = this.m_trophiesRewarded;
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameEnter, 0.5f, delegate
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameStartState());
			this.m_gameMode.EnterMinigame();
		}, null);
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0004FA0C File Offset: 0x0004DE0C
	public override void InitializeMinigame()
	{
		base.InitializeMinigame();
		this.SetPlaybackGhostAndCoins(true);
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0004FA1C File Offset: 0x0004DE1C
	public override void BeginHeat()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		if (this.m_minigameMetaData.playerUnit != null)
		{
			text = this.m_minigameMetaData.playerUnit;
		}
		if (this.m_gameMode != null)
		{
			text2 = this.m_gameMode.ToString();
		}
		PsMetrics.LevelRunStarted(text, text2, "level_racing");
		this.m_startPosition = this.GetPosition();
		this.m_practiceRun = false;
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_COIN", true, true, true, true, false, false);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameBeginHeatState());
		this.m_gameMode.StartMinigame();
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x0004FABC File Offset: 0x0004DEBC
	public override void CancelBegin()
	{
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameStartState());
		this.m_gameMode.EnterMinigame();
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x0004FAE2 File Offset: 0x0004DEE2
	public override void WaitForUserToStart()
	{
		(PsIngameMenu.m_playMenu as PsUITopPlayRacing).CreateGoText();
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0004FAF4 File Offset: 0x0004DEF4
	public override void StartMinigame()
	{
		base.StartMinigame();
		this.m_heatLost = false;
		this.m_practiceDisabled = true;
		PsState.m_activeMinigame.m_gameStarted = true;
		(PsIngameMenu.m_playMenu as PsUITopPlayRacing).CreateGoTween();
		Random.seed = (int)(Main.m_gameTimeSinceAppStarted * 60.0);
		PsState.m_physicsPaused = false;
		PsState.m_activeMinigame.m_gameOn = true;
		PsState.m_activeMinigame.m_gameStartCount++;
		PsState.m_activeMinigame.m_playerReachedGoal = false;
		this.m_heatNumber++;
		SoundS.PlaySingleShot("/InGame/GameStart", Vector3.zero, 1f);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GamePlayState());
		EveryplayManager.StartRecording();
		CameraS.m_zoomNeutralizer = 0.97f;
		(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.ResetCameraTarget();
		(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_lastGroundContact = Main.m_resettingGameTime;
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

	// Token: 0x06000719 RID: 1817 RVA: 0x0004FC48 File Offset: 0x0004E048
	public void SavePlayerdataAndLoop()
	{
		if (this.m_path.GetPathType() == PsPlanetPathType.main)
		{
			Hashtable hashtable = ClientTools.GenerateProgressionPathJson(this, this.m_path.m_currentNodeId, true, true, true);
			PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable, this.m_path.m_planet, false);
		}
		else
		{
			Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_path);
			PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable2, this.m_path.m_planet, false);
		}
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x0004FCBC File Offset: 0x0004E0BC
	public override void PauseMinigame()
	{
		if (PsState.m_activeMinigame.m_gamePaused || !PsState.m_activeMinigame.m_gameStarted || PsState.m_activeMinigame.m_gameEnded)
		{
			return;
		}
		base.PauseMinigame();
		this.m_paused = true;
		PsState.m_activeMinigame.SetGamePaused(true);
		base.PauseElapsedTimer();
		this.m_gameMode.PauseMinigame();
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x0004FD20 File Offset: 0x0004E120
	public override void ResumeMinigame()
	{
		base.ResumeMinigame();
		this.m_gameMode.CreatePlayMenu(new Action(this.SelfDestructPlayer), new Action(this.PauseMinigame));
		(PsIngameMenu.m_playMenu as PsUITopPlay).CreateRestartArea((PsIngameMenu.m_playMenu as PsUITopPlay).m_rightArea);
		(PsIngameMenu.m_playMenu as PsUITopPlay).CreateLeftArea();
		(PsIngameMenu.m_playMenu as PsUITopPlay).CreateCoinArea();
		(PsIngameMenu.m_playMenu as PsUITopPlay).Update();
		PsState.m_activeMinigame.SetGamePaused(false);
		if (this.m_practiceRun)
		{
			EntityManager.SetActivityOfEntitiesWithTag("GTAG_COIN", false, true, true, true, false, false);
		}
		base.ResumeElapsedTimer();
		this.m_paused = false;
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x0004FDD8 File Offset: 0x0004E1D8
	public override void RestartMinigame()
	{
		PsMetrics.LevelRestarted();
		(this.m_gameMode as PsGameModeRacing).m_rewardCoins = ((this.m_gameMode as PsGameModeRacing).m_rewardDiamonds = ((this.m_gameMode as PsGameModeRacing).m_rewardSecondaryTrophies = ((this.m_gameMode as PsGameModeRacing).m_rewardTrophies = false)));
		this.m_briefingShown = this.m_trophiesRewarded;
		base.RestartMinigame();
		EveryplayManager.StopRecording(0f);
		if (this.m_gameMode.m_waitForHighscoreAndGhost || this.m_gameMode.m_waitForNextGhost)
		{
			PsUIBasePopup psUIBasePopup = new PsUIBasePopup(typeof(PsUICenterWaitForHighscoresAndGhost), null, null, null, false, true, InitialPage.Center, false, false, false);
			psUIBasePopup.SetAction("Done", new Action(this.RestartAction));
		}
		else
		{
			this.RestartAction();
		}
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0004FEAC File Offset: 0x0004E2AC
	public virtual void RestartAction()
	{
		this.m_paused = false;
		base.ResumeElapsedTimer();
		PsState.m_activeMinigame.SetGamePaused(false);
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		PsState.m_activeMinigame.m_groundNode.RevertGroundFromPlay();
		LevelManager.ResetCurrentLevel();
		this.InitializeMinigame();
		this.BeginHeat();
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0004FF00 File Offset: 0x0004E300
	public override void LoseMinigame()
	{
		base.LoseMinigame();
		this.m_heatLost = true;
		this.m_gameMode.StopRecordingGhost();
		PsIngameMenu.CloseAll();
		base.StopEveryplayAfterDelay();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_gameOn = false;
		PsState.m_activeMinigame.m_gameDeathCount++;
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameLose, 1f, delegate
		{
			this.m_gameMode.LoseMinigame();
			string text = string.Empty;
			string text2 = string.Empty;
			if (this.m_minigameMetaData.playerUnit != null)
			{
				text = this.m_minigameMetaData.playerUnit;
			}
			if (this.m_gameMode != null)
			{
				text2 = this.m_gameMode.ToString();
			}
			Debug.Log("E_Test " + text + " " + text2, null);
			PsMetrics.PlayerDied();
		}, null);
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0004FF74 File Offset: 0x0004E374
	public override void WinMinigame()
	{
		base.WinMinigame();
		this.m_reportPerformance = true;
		this.m_gameMode.StopRecordingGhost();
		PsIngameMenu.CloseAll();
		base.StopEveryplayAfterDelay();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_playerReachedGoal = true;
		PsState.m_activeMinigame.m_gameOn = false;
		this.m_exitingMinigame = false;
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameWin, 1f, delegate
		{
			this.m_gameMode.WinMinigame();
			string text = string.Empty;
			string text2 = string.Empty;
			if (this.m_minigameMetaData.playerUnit != null)
			{
				text = this.m_minigameMetaData.playerUnit;
			}
			if (this.m_gameMode != null)
			{
				text2 = this.m_gameMode.ToString();
			}
			PsMetrics.LevelGoalReached(text, text2, "level_racing", this.GetPosition());
		}, null);
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x0004FFE6 File Offset: 0x0004E3E6
	public void SaveBeforeExit(bool _save)
	{
		this.m_trophiesRewarded = true;
		this.m_exitRating = true;
		if (_save)
		{
			this.SaveProgression(true);
		}
		this.ExitMinigame();
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0005000C File Offset: 0x0004E40C
	public override void ExitMinigame()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		if (this.m_minigameMetaData.playerUnit != null)
		{
			text = this.m_minigameMetaData.playerUnit;
		}
		if (this.m_gameMode != null)
		{
			text2 = this.m_gameMode.ToString();
		}
		PsMetrics.LevelExited(text, text2, "level_racing");
		FrbMetrics.LevelExited(this.m_gameMode.ToString());
		base.ExitMinigame();
		EveryplayManager.StopRecording(0f);
		this.m_exitingMinigame = true;
		this.m_gameMode.m_waitForNextGhost = false;
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameExit, 0f, delegate
		{
			PsIngameMenu.CloseAll();
			if (PsMetagameManager.ShouldShowRateAppDialogue(this))
			{
				BasicState basicState = new PsRateAppState();
				PsMenuScene.m_lastIState = basicState;
				PsMenuScene.m_lastState = null;
			}
			if (PsState.m_activeMinigame.m_collectedCopper > 0 || PsState.m_activeMinigame.m_collectedCoins > 0 || PsState.m_activeMinigame.m_collectedDiamonds > 0 || PsState.m_activeMinigame.m_collectedShards > 0 || PsState.m_activeMinigame.m_usedBoosters > 0)
			{
				PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
			}
			PsMetagameManager.HideResources();
			bool flag = this.GetRating() == PsRating.Unrated;
			if (this.m_exitRating)
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new PsRatingLoadingScene(flag ? null : typeof(PsUICenterLoadingWithoutRating)));
			}
			else if (!flag)
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new PsRatingLoadingScene(typeof(PsUICenterLoadingWithoutRating)));
			}
			else
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLevelEndLoadingScene(Color.black, this, 0.25f));
			}
		}, null);
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x000500B1 File Offset: 0x0004E4B1
	public override void SkipMinigame()
	{
		base.SkipMinigame();
		PsIngameMenu.CloseAll();
		this.SaveSkipProgression();
		Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLoadingScene(Color.black, true, 0.25f));
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x000500EE File Offset: 0x0004E4EE
	public void CloseGameCard()
	{
		PsIngameMenu.CloseAll();
		Main.m_currentGame.m_currentScene.m_stateMachine.RevertToPreviousState();
		this.ReleaseLoop();
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00050110 File Offset: 0x0004E510
	public void SaveSkipProgression()
	{
		this.m_scoreBest = (this.m_scoreCurrent = 1);
		this.m_path.m_currentNodeId = this.m_nodeId + 1;
		if (this.m_sidePath != null && this.m_sidePath.m_currentNodeId < 1)
		{
			this.m_sidePath.m_currentNodeId = 1;
		}
		if (this.m_path.GetPathType() == PsPlanetPathType.main)
		{
			Hashtable hashtable = ClientTools.GenerateProgressionPathJson(this, this.m_path.m_currentNodeId, true, true, true);
			PsMetagameManager.SaveProgression(hashtable, this.m_path.m_planet, true);
		}
		else
		{
			Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_path);
			PsMetagameManager.SaveProgression(hashtable2, this.m_path.m_planet, false);
		}
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x000501C4 File Offset: 0x0004E5C4
	public override void SaveProgression(bool _save = true)
	{
		bool flag = false;
		bool flag2 = false;
		if (this.m_nodeId == this.m_path.m_currentNodeId)
		{
			this.m_path.m_currentNodeId = this.m_nodeId + 1;
			flag = true;
			flag2 = true;
		}
		if (this.m_sidePath != null && this.m_sidePath.m_currentNodeId < 1)
		{
			this.m_sidePath.m_currentNodeId = 1;
		}
		if (this.m_scoreBest > this.m_scoreOld)
		{
			flag = true;
		}
		if (flag)
		{
			if (this.m_path.GetPathType() == PsPlanetPathType.main)
			{
				Hashtable hashtable = ClientTools.GenerateProgressionPathJson(this, this.m_path.m_currentNodeId, true, true, true);
				PsMetagameManager.SaveProgression(hashtable, this.m_path.m_planet, flag2);
				if (this.m_nodeId == 2 && this.m_reportPerformance)
				{
					PsMetrics.ReportPerformance(PsState.m_activeMinigame.m_gameTicks / 60f / PsState.m_activeMinigame.m_realTimeSpent);
					this.m_reportPerformance = false;
				}
			}
			else
			{
				Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_path);
				PsMetagameManager.SaveProgression(hashtable2, this.m_path.m_planet, false);
			}
		}
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x000502DB File Offset: 0x0004E6DB
	public override void DestroyMinigame()
	{
		base.DestroyMinigame();
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		LevelManager.DestroyCurrentLevel();
		PsState.m_activeMinigame = null;
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x00050300 File Offset: 0x0004E700
	public override void BackAtMenu()
	{
		if (!this.m_backAtMenu && !this.m_released)
		{
			this.m_backAtMenu = true;
			PsGameLoop currentNodeInfo = this.m_path.GetCurrentNodeInfo();
			if (currentNodeInfo != null && currentNodeInfo.m_node != null && !this.m_winFirstTime)
			{
				currentNodeInfo.m_node.SetActive();
			}
			if (this.m_sidePath != null)
			{
				PsGameLoop currentNodeInfo2 = this.m_sidePath.GetCurrentNodeInfo();
				if (currentNodeInfo2 != null && currentNodeInfo2.m_node != null && !this.m_winFirstTime)
				{
					currentNodeInfo2.m_node.SetActive();
				}
			}
			if (this.m_node != null)
			{
				this.m_node.GivePositionNumber();
			}
			bool flag = this.m_trophiesRewarded != this.m_initialTrophiesRewarded;
			bool flag2 = this.m_rivalWon != this.m_initialRivalWon;
			if (flag || flag2)
			{
				if (flag)
				{
					this.GiveTrophies();
				}
				if (flag2)
				{
					this.GiveMedals();
				}
				TimerC timerC = TimerS.AddComponent(PsMainMenuState.m_raceButton.m_TC.p_entity, string.Empty, 1.3f, 0f, false, new TimerComponentDelegate(this.TrophiesGiven));
			}
			else
			{
				this.ReleaseLoop();
			}
		}
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00050440 File Offset: 0x0004E840
	public virtual void GiveTrophies()
	{
		PsGameModeRacing psGameModeRacing = this.m_gameMode as PsGameModeRacing;
		if (this.m_rewardTrophyAmount != 0)
		{
			Vector2 vector = PsMainMenuState.m_raceButton.m_camera.WorldToScreenPoint(PsMainMenuState.m_raceButton.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
			Vector2 vector2 = PsMainMenuState.m_progress.m_camera.WorldToScreenPoint(PsMainMenuState.m_progress.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
			PsMetagameManager.m_playerStats.CreateFlyingTrophies(this.m_rewardTrophyAmount, vector, vector2, 0f, new float[] { 0.02f, -0.05f }, new float[] { -0.09f, -0.07f }, new float[] { -0.065f, 0.065f });
		}
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0005055C File Offset: 0x0004E95C
	public virtual void GiveMedals()
	{
		Vector2 vector = PsMainMenuState.m_raceButton.m_camera.WorldToScreenPoint(PsMainMenuState.m_raceButton.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
		UIComponent raceGachaArea = PsMainMenuState.m_raceGachaArea;
		Vector2 vector2 = raceGachaArea.m_camera.WorldToScreenPoint(raceGachaArea.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
		if (this.m_rivalWon != this.m_initialRivalWon && !this.m_initialGachaFull)
		{
			PlayerStats playerStats = PsMetagameManager.m_playerStats;
			int num = 1;
			Vector2 vector3 = vector;
			Vector2 vector4 = vector2;
			float[] array = new float[] { 0.02f, -0.05f };
			playerStats.CreateFlyingGacha(num, vector3, vector4, 0f, array, new float[] { -0.09f, -0.07f }, new float[] { -0.065f, 0.065f });
		}
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x00050681 File Offset: 0x0004EA81
	private void TrophiesGiven(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		if (this.m_rivalWon != this.m_initialRivalWon && PsMainMenuState.m_racingGacha != null)
		{
			PsMainMenuState.m_racingGacha.RevealBadge();
		}
		this.ReleaseLoop();
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x000506B4 File Offset: 0x0004EAB4
	public override void ReleaseLoop()
	{
		if (this.m_released)
		{
			return;
		}
		GameLevelPreview.RemoveLevelPreview();
		base.ReleaseLoop();
		Debug.LogStackTrace();
		this.m_scoreOld = this.m_scoreBest;
		this.m_rewardOld = this.m_scoreBest;
		if (this.m_nodeId != this.m_path.m_currentNodeId)
		{
			if (this.m_node != null)
			{
				this.m_node.RemoveHighlight();
			}
			if (this.m_nodeId + 1 == this.m_path.m_currentNodeId)
			{
				if (this.m_rewardTrophyAmount <= 0 && !this.m_initialTrophiesRewarded && this.CanShowUpgradeReminder())
				{
					this.ShowUpgradeReminder();
				}
				else
				{
					PlayerPrefsX.SetLosingStreak(0);
					new PsMenuDialogueFlow(this, PsNodeEventTrigger.LoopReleased, 0f, delegate
					{
						this.LoopReleased();
					}, true);
				}
			}
			else
			{
				this.LoopReleased();
			}
		}
		else
		{
			this.SetActiveLoopNull();
		}
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x000507A0 File Offset: 0x0004EBA0
	public void ShowUpgradeReminder()
	{
		StringID stringID = StringID.RACE_LOSE_INSTALL_OK;
		StringID stringID2 = StringID.RACE_INSTALL_UPGRADES;
		if (PsUpgradeManager.GetUpgradeableItemCount(this.GetRentVehicleType()) < 1)
		{
			stringID = StringID.RACE_LOSE_INSTALL_FALSE;
			stringID2 = StringID.RACE_MORE_TREASURES;
		}
		PsDialogue psDialogue = new PsDialogue("manual", PsNodeEventTrigger.Manual);
		psDialogue.AddStep(PsDialogueCharacter.Mechanic, PsDialogueCharacterPosition.Left, stringID, stringID2);
		PsMenuDialogueFlow psMenuDialogueFlow = new PsMenuDialogueFlow(psDialogue, 0f, delegate
		{
			this.LoopReleased();
			PsMainMenuState.ChangeToGarageState(null);
		}, true, true);
		PlayerPrefsX.SetLastUpgradeRemindTime((int)Main.m_EPOCHSeconds);
		PlayerPrefsX.SetRacingUpgradeRemindCount(PlayerPrefsX.GetRacingUpgradeRemindCount() + 1);
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00050820 File Offset: 0x0004EC20
	private bool CanShowUpgradeReminder()
	{
		int num = PlayerPrefsX.GetLosingStreak() + 1;
		int num2 = 1;
		PlayerPrefsX.SetLosingStreak(num);
		int racingUpgradeRemindCount = PlayerPrefsX.GetRacingUpgradeRemindCount();
		int num3;
		if (racingUpgradeRemindCount != 1)
		{
			if (racingUpgradeRemindCount != 2)
			{
				num3 = 0;
			}
			else
			{
				num3 = 72;
			}
		}
		else
		{
			num3 = 24;
		}
		int num4 = PlayerPrefsX.GetLastUpgradeRemindTime() + num3 * 3600 - (int)Main.m_EPOCHSeconds;
		Debug.LogError(string.Concat(new object[] { "Timeleft seconds for upgrade reminder: ", num4, ", remindCount: ", racingUpgradeRemindCount, ", Losing streak: ", num }));
		if (num >= num2)
		{
			if (racingUpgradeRemindCount == 0)
			{
				return true;
			}
			if (racingUpgradeRemindCount < 3)
			{
				return num4 < 0;
			}
		}
		return false;
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x000508E6 File Offset: 0x0004ECE6
	public void LoopReleased()
	{
		this.SetActiveLoopNull();
		if (this.m_nodeId + 1 == this.m_path.m_currentNodeId)
		{
			this.ActivateCurrentLoop();
			this.m_winFirstTime = false;
		}
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00050914 File Offset: 0x0004ED14
	public override void ActivateCurrentLoop()
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
		if (this.m_nodeId + 1 == this.m_path.m_currentNodeId && currentNodeInfo != null && !currentNodeInfo.m_activated)
		{
			currentNodeInfo.SaveAsActivated();
			new PsMenuDialogueFlow(currentNodeInfo, PsNodeEventTrigger.LoopActivated, 0.1f, null, true);
		}
	}

	// Token: 0x04000792 RID: 1938
	private PsUIBasePopup m_waitingPopup;

	// Token: 0x04000793 RID: 1939
	private bool m_reportPerformance;

	// Token: 0x04000794 RID: 1940
	public int m_heatNumber;

	// Token: 0x04000795 RID: 1941
	public bool m_ghostWon;

	// Token: 0x04000796 RID: 1942
	public bool m_heatLost;

	// Token: 0x04000797 RID: 1943
	public bool m_practiceRun;

	// Token: 0x04000798 RID: 1944
	public bool m_practiceDisabled;

	// Token: 0x04000799 RID: 1945
	public int m_secondarysWon;

	// Token: 0x0400079A RID: 1946
	public string m_trophyGhostIds;

	// Token: 0x0400079B RID: 1947
	public string m_fixedTrophies;

	// Token: 0x0400079C RID: 1948
	public bool m_exitRating;

	// Token: 0x0400079D RID: 1949
	public bool m_trophiesRewarded;

	// Token: 0x0400079E RID: 1950
	public bool m_initialTrophiesRewarded;

	// Token: 0x0400079F RID: 1951
	public const int MAX_HEAT_COUNT = 5;

	// Token: 0x040007A0 RID: 1952
	public bool m_paused;

	// Token: 0x040007A1 RID: 1953
	public int m_startPosition;

	// Token: 0x040007A2 RID: 1954
	public int m_initialSecondarysWon;

	// Token: 0x040007A3 RID: 1955
	public bool m_initialGachaFull;

	// Token: 0x040007A4 RID: 1956
	public int m_rewardTrophyAmount;

	// Token: 0x040007A5 RID: 1957
	public bool m_briefingShown;

	// Token: 0x040007A6 RID: 1958
	public int m_rivalPos;

	// Token: 0x040007A7 RID: 1959
	public bool m_initialRivalWon;

	// Token: 0x040007A8 RID: 1960
	public bool m_rivalWon;

	// Token: 0x040007A9 RID: 1961
	public int m_purchasedRuns;

	// Token: 0x040007AA RID: 1962
	public int m_purchasedRunsLimit = 5;
}
