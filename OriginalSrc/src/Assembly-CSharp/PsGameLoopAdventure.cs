using System;
using System.Collections;
using System.Collections.Generic;
using Prime31;
using Server;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class PsGameLoopAdventure : PsGameLoop
{
	// Token: 0x0600062B RID: 1579 RVA: 0x00049E04 File Offset: 0x00048204
	public PsGameLoopAdventure(PsMinigameMetaData _data)
		: this(PsMinigameContext.Level, _data.id, new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any), null, -1, -1, _data.score, false, null)
	{
		this.m_minigameMetaData = _data;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00049E3C File Offset: 0x0004823C
	public PsGameLoopAdventure(string _minigameId, MinigameSearchParametres _searchParametres, PsPlanetPath _path, int _id, int _levelNumber, int _score, bool _unlocked, string[] _medalTimes = null)
		: base(PsMinigameContext.Level, _minigameId, _path, _id, _levelNumber, _score, _unlocked, _medalTimes)
	{
		if (_searchParametres != null)
		{
			this.m_minigameSearchParametres = _searchParametres;
		}
		else
		{
			this.m_minigameSearchParametres = new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any);
		}
		if (_path != null)
		{
			string text = _path.m_planet.Replace("Adventure", string.Empty);
			this.m_minigameSearchParametres.m_playerUnitFilter = text;
		}
		this.m_minigameSearchParametres.m_gameMode = PsGameMode.StarCollect;
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00049EB4 File Offset: 0x000482B4
	public PsGameLoopAdventure(PsMinigameContext _context, string _minigameId, MinigameSearchParametres _searchParametres, PsPlanetPath _path, int _id, int _levelNumber, int _score, bool _unlocked, string[] _medalTimes = null)
		: base(_context, _minigameId, _path, _id, _levelNumber, _score, _unlocked, _medalTimes)
	{
		if (_searchParametres != null)
		{
			this.m_minigameSearchParametres = _searchParametres;
		}
		else
		{
			this.m_minigameSearchParametres = new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any);
		}
		if (_path != null)
		{
			string text = _path.m_planet.Replace("Adventure", string.Empty);
			this.m_minigameSearchParametres.m_playerUnitFilter = text;
		}
		this.m_minigameSearchParametres.m_gameMode = PsGameMode.StarCollect;
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00049F2D File Offset: 0x0004832D
	public override float GetOverrideCC()
	{
		if (this.m_minigameMetaData != null)
		{
			return this.m_minigameMetaData.overrideCC;
		}
		return -1f;
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x00049F4B File Offset: 0x0004834B
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

	// Token: 0x06000630 RID: 1584 RVA: 0x00049F6C File Offset: 0x0004836C
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
		if (this.CheckIfFixNeeded())
		{
			base.m_unlocked = true;
			this.m_backAtMenu = false;
			this.SetAsActiveLoop();
			this.SaveProgression(true);
			this.BackAtMenu();
		}
		else if (this.m_nodeId <= this.m_path.m_currentNodeId && !Input.GetKey(304) && (!PsState.m_adminMode || !PsState.m_adminModeSkipping))
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
				this.SaveProgression(true);
				this.BackAtMenu();
			}, true);
		}
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0004A0D8 File Offset: 0x000484D8
	public virtual bool CheckIfFixNeeded()
	{
		return this.m_nodeId <= this.m_path.m_currentNodeId && this == this.m_path.GetLastLevel() && this.m_nodeId > this.m_path.GetLastBlockId() && base.m_unlocked && (this.m_skipped || this.m_scoreBest > 0);
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0004A147 File Offset: 0x00048547
	public void FadeInLoadingScreen(TimerC _c)
	{
		this.LoadMinigame();
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0004A150 File Offset: 0x00048550
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

	// Token: 0x06000634 RID: 1588 RVA: 0x0004A1C5 File Offset: 0x000485C5
	protected override void LoadMetaDataSUCCESS(PsMinigameMetaData _minigameMetaData)
	{
		base.LoadMetaDataSUCCESS(_minigameMetaData);
		this.LoadMinigameBytes();
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0004A1D4 File Offset: 0x000485D4
	public override void PreviewLoop()
	{
		Main.m_currentGame.m_sceneManager.ChangeScene(new GameScene("GameScene", null), new PsRacingLoadingScene());
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0004A1F5 File Offset: 0x000485F5
	protected override void MinigameDownloadOk(byte[] _levelData)
	{
		base.MinigameDownloadOk(_levelData);
		if (this.m_gameMode is PsGameModeRace)
		{
			(this.m_gameMode as PsGameModeRace).CalculateMedalTimes();
		}
		this.LoadGhosts();
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0004A224 File Offset: 0x00048624
	public override void CreateGameMode()
	{
		this.m_gameMode = new PsGameModeAdventure(this);
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0004A234 File Offset: 0x00048634
	public override void EnterMinigame()
	{
		Debug.Log("E_Test PsGameLoopAdventure EnterMinigame", null);
		PsMetrics.LevelEntered(this.m_minigameMetaData.playerUnit, this.m_gameMode.ToString(), "level_adventure");
		FrbMetrics.SetCurrentScreen("level_adventure");
		FrbMetrics.LevelEntered();
		if (base.IsFirstLevel())
		{
			PsMetrics.FirstLevelLoaded();
		}
		Debug.LogWarning("ENTER MINIGAME");
		PsGameLoop nodeInfo = this.m_path.GetNodeInfo(this.m_nodeId + 1);
		if (nodeInfo != null && nodeInfo.m_minigameMetaData == null)
		{
			nodeInfo.LoadMinigameMetaData(null);
		}
		if (this.m_path.m_currentNodeId > this.m_nodeId)
		{
			this.m_askForRating = true;
		}
		GameLevelPreview.InitLevelPreview(false);
		CameraS.m_updateComponents = true;
		CameraS.m_zoomMultipler = 2f;
		CameraS.m_zoomNeutralizer = 0.985f;
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameEnter, 0.5f, delegate
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameStartState());
			this.m_gameMode.EnterMinigame();
		}, null);
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0004A318 File Offset: 0x00048718
	public override void InitializeMinigame()
	{
		base.InitializeMinigame();
		this.SetPlaybackGhostAndCoins(true);
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0004A328 File Offset: 0x00048728
	public override void BeginAdventure()
	{
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_COIN", true, true, true, true, false, false);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameBeginAdventureState());
		if (base.IsFirstLevel())
		{
			PsMetrics.FirstLevelRunStarted();
		}
		this.m_gameMode.StartMinigame();
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0004A37A File Offset: 0x0004877A
	public override void CancelBegin()
	{
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameStartState());
		this.m_gameMode.EnterMinigame();
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0004A3A0 File Offset: 0x000487A0
	public virtual void CreateIngameUI()
	{
		(PsIngameMenu.m_playMenu as PsUITopPlayAdventure).CreateRestartArea((PsIngameMenu.m_playMenu as PsUITopPlayAdventure).m_rightArea);
		(PsIngameMenu.m_playMenu as PsUITopPlayAdventure).CreateCoinArea();
		(PsIngameMenu.m_playMenu as PsUITopPlayAdventure).Update();
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0004A3E0 File Offset: 0x000487E0
	public override void StartMinigame()
	{
		base.StartMinigame();
		this.CreateIngameUI();
		PsState.m_activeMinigame.m_gameStarted = true;
		Random.seed = (int)(Main.m_gameTimeSinceAppStarted * 60.0);
		PsState.m_physicsPaused = false;
		PsState.m_activeMinigame.m_gameOn = true;
		PsState.m_activeMinigame.m_gameStartCount++;
		PsState.m_activeMinigame.m_playerReachedGoal = false;
		this.CreatePlaybackGhosts();
		SoundS.PlaySingleShot("/InGame/GameStart", Vector3.zero, 1f);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GamePlayState());
		EveryplayManager.StartRecording();
		CameraS.m_zoomNeutralizer = 0.97f;
		(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.ResetCameraTarget();
		(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_lastGroundContact = Main.m_resettingGameTime;
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0004A4BC File Offset: 0x000488BC
	public override void PauseMinigame()
	{
		if (PsState.m_activeMinigame.m_gamePaused || !PsState.m_activeMinigame.m_gameStarted || PsState.m_activeMinigame.m_gameEnded)
		{
			return;
		}
		base.PauseMinigame();
		PsState.m_activeMinigame.SetGamePaused(true);
		base.PauseElapsedTimer();
		this.m_gameMode.PauseMinigame();
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0004A51C File Offset: 0x0004891C
	public override void ResumeMinigame()
	{
		base.ResumeMinigame();
		this.m_gameMode.CreatePlayMenu(new Action(this.SelfDestructPlayer), new Action(this.PauseMinigame));
		this.CreateIngameUI();
		PsState.m_activeMinigame.SetGamePaused(false);
		base.ResumeElapsedTimer();
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0004A56C File Offset: 0x0004896C
	public override void RestartMinigame()
	{
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

	// Token: 0x06000641 RID: 1601 RVA: 0x0004A5E8 File Offset: 0x000489E8
	public virtual void RestartAction()
	{
		base.ResumeElapsedTimer();
		PsState.m_activeMinigame.SetGamePaused(false);
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		PsState.m_activeMinigame.m_groundNode.RevertGroundFromPlay();
		LevelManager.ResetCurrentLevel();
		this.InitializeMinigame();
		this.BeginAdventure();
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0004A638 File Offset: 0x00048A38
	public override void LoseMinigame()
	{
		base.LoseMinigame();
		this.m_gameMode.StopRecordingGhost();
		PsIngameMenu.CloseAll();
		base.StopEveryplayAfterDelay();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_gameOn = false;
		PsState.m_activeMinigame.m_gameDeathCount++;
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameLose, 1f, delegate
		{
			this.m_gameMode.LoseMinigame();
		}, null);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0004A6A4 File Offset: 0x00048AA4
	public override void WinMinigame()
	{
		Debug.Log("E_Test PsGameLoopAdventure WinMinigame", null);
		PsMetrics.LevelGoalReached(this.m_minigameMetaData.playerUnit, this.m_gameMode.ToString(), this.m_context.ToString(), 0);
		if (base.IsFirstLevel())
		{
			PsMetrics.FirstLevelGoalReached();
		}
		base.WinMinigame();
		if ((this.m_timeScoreBest == 0 || this.m_timeScoreBest == 2147483647) && this.m_scoreOld == 0)
		{
			this.m_reportPerformance = true;
		}
		this.m_gameMode.StopRecordingGhost();
		PsIngameMenu.CloseAll();
		base.StopEveryplayAfterDelay();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_playerReachedGoal = true;
		PsState.m_activeMinigame.m_gameOn = false;
		this.m_exitingMinigame = false;
		this.CheckWinCondition();
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0004A770 File Offset: 0x00048B70
	public virtual void CheckWinCondition()
	{
		Debug.Log("E_Test PsGameLoopAdventure CheckWinCondition", null);
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameWin, 1f, delegate
		{
			this.m_gameMode.WinMinigame();
			if (this.m_nodeId == this.m_path.m_currentNodeId)
			{
				this.m_askForRating = true;
			}
			this.SaveProgression(true);
		}, null);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0004A797 File Offset: 0x00048B97
	public void SaveAndExit()
	{
		this.ExitMinigame();
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0004A7A0 File Offset: 0x00048BA0
	private bool WasFirstLevel()
	{
		return !PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && this.m_path != null && this.m_path.m_planet == "AdventureOffroadCar" && this.m_path.m_currentNodeId <= 2;
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0004A7FC File Offset: 0x00048BFC
	public override void ExitMinigame()
	{
		PsMetrics.LevelExited(this.m_minigameMetaData.playerUnit, this.m_gameMode.ToString(), "level_adventure");
		FrbMetrics.LevelExited(this.m_gameMode.ToString());
		base.ExitMinigame();
		EveryplayManager.StopRecording(0f);
		this.m_exitingMinigame = true;
		this.m_gameMode.m_waitForNextGhost = false;
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameExit, 0f, delegate
		{
			PsIngameMenu.CloseAll();
			if (PsState.m_activeMinigame.m_gameStartCount > 0 && this.m_sendQuit && this.m_timeScoreBest == 2147483647)
			{
				this.SendQuit();
			}
			PsMetagameManager.HideResources();
			if (this.WasFirstLevel())
			{
				PsMenuScene.m_lastIState = new PsCheckBackupsState();
				PsMenuScene.m_lastState = null;
			}
			else if (PsMetagameManager.ShouldShowRateAppDialogue(this))
			{
				BasicState basicState = new PsRateAppState();
				PsMenuScene.m_lastIState = basicState;
				PsMenuScene.m_lastState = null;
			}
			if (this.GetRating() == PsRating.Unrated && this.m_askForRating)
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new PsRatingLoadingScene((!this.m_skipped) ? null : typeof(PsUICenterSkipRatingLoading)));
			}
			else if (this.GetRating() != PsRating.Unrated)
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new PsRatingLoadingScene(typeof(PsUICenterLoadingWithoutRating)));
			}
			else
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLevelEndLoadingScene(Color.black, this, 0.25f));
			}
		}, null);
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0004A878 File Offset: 0x00048C78
	private static int getSDKInt()
	{
		int @static;
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Build$VERSION"))
		{
			@static = androidJavaClass.GetStatic<int>("SDK_INT");
		}
		return @static;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0004A8C0 File Offset: 0x00048CC0
	public void PermissionCallback(bool _success)
	{
		bool flag = NoodlePermissionGranter.ShouldShowRequestPermissionRationale(NoodlePermissionGranter.AndroidPermission.WRITE_EXTERNAL_STORAGE);
		if (flag && !_success && !this.m_retried)
		{
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterLocalSavePrompt), null, null, null, true, true, InitialPage.Center, false, false, false);
			popup.SetAction("Continue", delegate
			{
				popup.Destroy();
				this.m_retried = true;
				NoodlePermissionGranter.GrantPermission(NoodlePermissionGranter.AndroidPermission.WRITE_EXTERNAL_STORAGE);
			});
		}
		else
		{
			PsPersistentData.CheckBackups(null);
			PlayerPrefsX.SetLocalSavePrompted(true);
		}
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0004A944 File Offset: 0x00048D44
	public void SendQuit()
	{
		SendQuitData data = new SendQuitData();
		data.startCount = PsState.m_activeMinigame.m_gameStartCount;
		data.gameLoop = this;
		data.playerUnit = PsState.m_activeMinigame.m_playerUnitName;
		new PsServerQueueFlow(null, delegate
		{
			this.ServerSendQuit(data);
		}, new string[] { "SetData" });
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0004A9C0 File Offset: 0x00048DC0
	public HttpC ServerSendQuit(SendQuitData _data)
	{
		return StarCollect.Lose(_data, null, new Action<HttpC>(this.QuitSendSUCCEED), delegate(HttpC k)
		{
			this.QuitSendFAILED(k, _data);
		}, null);
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0004AA08 File Offset: 0x00048E08
	public void QuitSendSUCCEED(HttpC _c)
	{
		Debug.Log("SEND QUIT SUCCEED", null);
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0004AA18 File Offset: 0x00048E18
	public void QuitSendFAILED(HttpC _c, SendQuitData _data)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), () => this.ServerSendQuit(_data), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0004AA6A File Offset: 0x00048E6A
	public override void SkipMinigame()
	{
		base.SkipMinigame();
		PsIngameMenu.CloseAll();
		this.SaveSkipProgression();
		this.m_askForRating = true;
		this.m_skipped = true;
		this.ExitMinigame();
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0004AA91 File Offset: 0x00048E91
	public void CloseGameCard()
	{
		PsIngameMenu.CloseAll();
		Main.m_currentGame.m_currentScene.m_stateMachine.RevertToPreviousState();
		this.ReleaseLoop();
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0004AAB4 File Offset: 0x00048EB4
	public void SaveSkipProgression()
	{
		this.m_scoreBest = (this.m_scoreCurrent = 0);
		PsState.m_showStarterPackPopup = this.m_nodeId == 10 && !PsMetagameManager.m_playerStats.dirtBikeBundle;
		this.m_path.m_currentNodeId = this.m_nodeId + 1;
		PsGameLoop psGameLoop = null;
		if (this.m_sidePath != null && this.m_sidePath.m_currentNodeId < 1)
		{
			this.m_sidePath.m_currentNodeId = 1;
		}
		if (this.m_nodeId == this.m_path.GetLastLevelId() && this.m_nodeId > this.m_path.GetLastBlockId())
		{
			psGameLoop = this.m_planet.FixBrokenPathWithBlock(this);
		}
		if (this.m_path.GetPathType() == PsPlanetPathType.main)
		{
			Hashtable hashtable;
			if (psGameLoop == null)
			{
				hashtable = ClientTools.GenerateProgressionPathJson(this, this.m_path.m_currentNodeId, true, true, true);
			}
			else
			{
				List<PsGameLoop> list = new List<PsGameLoop>();
				list.Add(this);
				list.Add(psGameLoop);
				hashtable = ClientTools.GenerateProgressionPathJson(list, this.m_path.m_currentNodeId, this.m_path.m_planet, true, true, true, null);
			}
			PsMetagameManager.SaveProgression(hashtable, this.m_path.m_planet, true);
		}
		else
		{
			Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_path);
			PsMetagameManager.SaveProgression(hashtable2, this.m_path.m_planet, false);
		}
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0004AC08 File Offset: 0x00049008
	public override void SaveProgression(bool _save = true)
	{
		bool flag = false;
		bool flag2 = false;
		PsGameLoop psGameLoop = null;
		if (this.m_nodeId == this.m_path.m_currentNodeId && _save)
		{
			PsState.m_showStarterPackPopup = this.m_nodeId == 10 && !PsMetagameManager.m_playerStats.dirtBikeBundle;
			if (this.m_nodeId == this.m_path.GetLastLevelId() && this.m_nodeId > this.m_path.GetLastBlockId())
			{
				psGameLoop = this.m_planet.FixBrokenPathWithBlock(this);
			}
			this.m_path.m_currentNodeId = this.m_nodeId + 1;
			flag = true;
			flag2 = true;
		}
		if (this.m_nodeId == this.m_path.GetLastLevelId() && this.m_nodeId > this.m_path.GetLastBlockId())
		{
			psGameLoop = this.m_planet.FixBrokenPathWithBlock(this);
			this.m_path.m_currentNodeId = this.m_nodeId + 1;
		}
		if (this.m_sidePath != null && this.m_sidePath.m_currentNodeId < 1)
		{
			this.m_sidePath.m_currentNodeId = 1;
		}
		if (this.m_scoreBest > this.m_scoreOld)
		{
			flag = true;
		}
		if (flag || psGameLoop != null)
		{
			if (this.m_path.GetPathType() == PsPlanetPathType.main)
			{
				Hashtable hashtable;
				if (psGameLoop == null)
				{
					hashtable = ClientTools.GenerateProgressionPathJson(this, this.m_path.m_currentNodeId, true, true, true);
				}
				else
				{
					List<PsGameLoop> list = new List<PsGameLoop>();
					list.Add(this);
					list.Add(psGameLoop);
					hashtable = ClientTools.GenerateProgressionPathJson(list, this.m_path.m_currentNodeId, this.m_path.m_planet, true, true, true, null);
				}
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

	// Token: 0x06000652 RID: 1618 RVA: 0x0004AE1C File Offset: 0x0004921C
	public override void DestroyMinigame()
	{
		base.DestroyMinigame();
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		LevelManager.DestroyCurrentLevel();
		PsState.m_activeMinigame = null;
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0004AE40 File Offset: 0x00049240
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
			if (this.m_node != null && this.m_scoreBest != this.m_scoreOld)
			{
				this.m_node.GiveMapPieces(new TimerComponentDelegate(this.StarsGiven));
				this.GiveGachaPieces();
				this.ReleaseLoop();
			}
			else
			{
				this.ReleaseLoop();
			}
		}
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0004AF28 File Offset: 0x00049328
	private void GiveGachaPieces()
	{
		for (int i = this.m_scoreOld + 1; i <= this.m_scoreCurrent; i++)
		{
			if (PsMainMenuState.m_adventureGacha != null)
			{
				TimerComponentDelegate timerComponentDelegate = null;
				timerComponentDelegate = (TimerComponentDelegate)Delegate.Combine(timerComponentDelegate, delegate(TimerC _c)
				{
					if (PsMainMenuState.m_adventureGacha != null)
					{
						PsMainMenuState.m_adventureGacha.RevealPiece();
					}
					TimerS.RemoveComponent(_c);
				});
				TimerS.AddComponent(PsMainMenuState.m_adventureGacha.m_TC.p_entity, string.Empty, (float)i * 0.25f + 0.4f, 0f, false, timerComponentDelegate);
			}
		}
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0004AFB7 File Offset: 0x000493B7
	private void StarsGiven(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		this.ReleaseLoop();
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0004AFC8 File Offset: 0x000493C8
	public override void ReleaseLoop()
	{
		PsMenuScene.m_lastIState = null;
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
				new PsMenuDialogueFlow(this, PsNodeEventTrigger.LoopReleased, 0f, delegate
				{
					this.LoopReleased();
				}, true);
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

	// Token: 0x06000657 RID: 1623 RVA: 0x0004B084 File Offset: 0x00049484
	public void LoopReleased()
	{
		if (PsState.m_showStarterPackPopup)
		{
			TimerS.AddComponent(EntityManager.AddEntity(), "StarterPackPopup", 2f, 0f, true, delegate(TimerC _c)
			{
				PsState.m_showStarterPackPopup = false;
				TouchAreaS.Enable();
				this.ShowStarterPackPopup();
			});
		}
		this.SetActiveLoopNull();
		this.ActivateCurrentLoop();
		this.m_winFirstTime = false;
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0004B0D8 File Offset: 0x000494D8
	private void ShowStarterPackPopup()
	{
		PsMetrics.StarterPackOffered("starterPackPopup");
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUIStarterPackPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
		IAPProduct iapproductById = PsIAPManager.GetIAPProductById("dirt_bike_bundle");
		string text = ((iapproductById != null) ? iapproductById.price : "NaN");
		(popup.m_mainContent as PsUIStarterPackPopup).SetPrice(text);
		(popup.m_mainContent as PsUIStarterPackPopup).CreateContent();
		popup.SetAction("Purchased", delegate
		{
			popup.Destroy();
			PsPurchaseHelper.PurchaseIAP("dirt_bike_bundle", null, delegate
			{
				PsMetrics.StarterPackPurchased("starterPackPopup");
				int slotIndex = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.ADVENTURE);
				PsMetagameManager.m_vehicleGachaData.m_mapPieceCount = PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax;
				if (PsGachaManager.m_gachas[slotIndex] == null)
				{
					PsGachaManager.AddGacha(PsGachaManager.GetNextGacha(), slotIndex, false);
					PsMetagameManager.m_vehicleGachaData.m_adventureGachaCount++;
				}
				PsGachaManager.UnlockGachaImmediately(PsGachaManager.m_gachas[slotIndex], false);
				int slotIndex2 = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.RACING);
				PsMetagameManager.m_vehicleGachaData.m_rivalWonCount = 4;
				if (PsGachaManager.m_gachas[slotIndex2] == null)
				{
					PsGachaManager.AddGacha(PsGachaManager.GetNextGacha(), slotIndex2, true);
					PsMetagameManager.m_vehicleGachaData.m_racingGachaCount++;
				}
				PsGachaManager.UnlockGachaImmediately(PsGachaManager.m_gachas[slotIndex2], false);
				PsMetagameManager.SendCurrentGachaData(true);
				PsMainMenuState.UpdateUIInfo();
				if (PsMainMenuState.m_adventureGacha != null)
				{
					PsMainMenuState.m_adventureGacha.Init(slotIndex);
					PsMainMenuState.m_adventureGacha.Update();
				}
				if (PsMainMenuState.m_racingGacha != null)
				{
					PsMainMenuState.m_racingGacha.Init(slotIndex2);
					PsMainMenuState.m_racingGacha.Update();
				}
			});
		});
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
		});
		TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0004B1D8 File Offset: 0x000495D8
	public override void ActivateCurrentLoop()
	{
		PsGameLoop currentLoop = this.m_path.GetCurrentNodeInfo();
		if (currentLoop == null || PsState.m_dialogues.Count > 0 || PsMetagameManager.m_tutorialArrow != null)
		{
			return;
		}
		if (currentLoop.m_node != null)
		{
			currentLoop.m_node.Activate();
		}
		if (this.m_sidePath != null)
		{
			PsGameLoop currentNodeInfo = this.m_sidePath.GetCurrentNodeInfo();
			if (currentNodeInfo != null && currentNodeInfo.m_node != null)
			{
				currentNodeInfo.m_node.Activate();
			}
		}
		Action action = null;
		if (((this.m_nodeId + 1 == this.m_path.m_currentNodeId && this.m_askForRating) || this.m_nodeId == this.m_path.m_currentNodeId) && PsGachaManager.IsSlotEmpty(PsGachaManager.SlotType.ADVENTURE) && PsMetagameManager.m_vehicleGachaData.m_adventureGachaCount == 0 && PsMetagameManager.m_vehicleGachaData.m_mapPieceCount >= PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax)
		{
			action = delegate
			{
				PsDialogue dialogueByIdentifier = PsMetagameData.GetDialogueByIdentifier("first_chest_gotten");
				PsMainMenuState.HideUI(true, false, null, true, null);
				new PsMenuDialogueFlow(dialogueByIdentifier, 0f, delegate
				{
					TouchAreaS.Disable();
					PsMainMenuState.ShowUI(true, null);
					if (PsMainMenuState.m_adventureGacha.IsOpenAnimationRunning())
					{
						PsMainMenuState.m_adventureGacha.AddOpenAnimationDoneCallback(delegate
						{
							PsUIAdventureGacha adventureGacha2 = PsMainMenuState.m_adventureGacha;
							bool flag2 = true;
							Vector3 vector2 = new Vector3(0f, 25f, 0f);
							new PsUITutorialArrowUI(adventureGacha2, flag2, null, 2f, vector2, false);
						});
					}
					else
					{
						PsUIAdventureGacha adventureGacha = PsMainMenuState.m_adventureGacha;
						bool flag = true;
						Vector3 vector = new Vector3(0f, 25f, 0f);
						new PsUITutorialArrowUI(adventureGacha, flag, null, 2f, vector, false);
					}
				}, false, false);
			};
		}
		if (this.m_nodeId + 1 == this.m_path.m_currentNodeId && this.m_askForRating)
		{
			currentLoop.SaveAsActivated();
			new PsMenuDialogueFlow(currentLoop, PsNodeEventTrigger.LoopActivated, 0.1f, action, true);
		}
		else if (action != null)
		{
			action.Invoke();
		}
		else if (currentLoop.m_nodeId <= 1 && currentLoop.m_dialogues != null && currentLoop.m_dialogues.ContainsKey(PsNodeEventTrigger.LoopActivated.ToString()))
		{
			new PsMenuDialogueFlow(currentLoop, PsNodeEventTrigger.LoopActivated, 0.1f, delegate
			{
				TouchAreaS.Disable();
				TimerS.AddComponent(PsPlanetManager.GetCurrentPlanet().m_spaceEntity, "Tutorial finger timer", 0f, 0.3f, false, delegate(TimerC _c)
				{
					TimerS.RemoveComponent(_c);
					PsUITutorialArrowNode psUITutorialArrowNode = new PsUITutorialArrowNode(currentLoop.m_nodeId);
				});
			}, true);
		}
	}

	// Token: 0x04000762 RID: 1890
	private PsUIBasePopup m_waitingPopup;

	// Token: 0x04000763 RID: 1891
	private bool m_reportPerformance;

	// Token: 0x04000764 RID: 1892
	public bool m_askForRating;

	// Token: 0x04000765 RID: 1893
	private bool m_retried;

	// Token: 0x04000766 RID: 1894
	public bool m_skipped;
}
