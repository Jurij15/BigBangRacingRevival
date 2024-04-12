using System;
using Server;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class PsGameLoopFresh : PsTimedEventLoop
{
	// Token: 0x060006C8 RID: 1736 RVA: 0x0004DFB0 File Offset: 0x0004C3B0
	public PsGameLoopFresh()
		: this(PsMinigameContext.Fresh, string.Empty, null, -1, -1, 0, 0, 0, null, false, false)
	{
		this.m_loadingMetaData = true;
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0004DFDC File Offset: 0x0004C3DC
	public PsGameLoopFresh(PsMinigameMetaData _data)
		: this(PsMinigameContext.Fresh, _data.id, null, -1, -1, _data.score, 0, 0, null, false, false)
	{
		this.m_minigameMetaData = _data;
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0004E00C File Offset: 0x0004C40C
	public PsGameLoopFresh(PsMinigameContext _context, string _minigameId, PsPlanetPath _path, int _id, int _levelNumber, int _score, int _timeleft, int _duration, string _endTime = null, bool _eventOver = false, bool _domeDestroyed = false)
	{
		this.m_preferredGameMode = string.Empty;
		this.m_scoreMultiplier = 1;
		base..ctor(_context, _minigameId, _minigameId, _path, -1, _levelNumber, _score, _timeleft, _duration, _endTime, _eventOver, _domeDestroyed);
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0004E044 File Offset: 0x0004C444
	public PsGameLoopFresh(string _minigameId, PsPlanetPath _path)
	{
		this.m_preferredGameMode = string.Empty;
		this.m_scoreMultiplier = 1;
		base..ctor(PsMinigameContext.Fresh, _minigameId, _minigameId, _path, -1, _path.m_nodeInfos.Count, 0, -1, -1, null, false, false);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0004E07F File Offset: 0x0004C47F
	public override float GetOverrideCC()
	{
		if (this.m_minigameMetaData != null)
		{
			return this.m_minigameMetaData.overrideCC;
		}
		return -1f;
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0004E0A0 File Offset: 0x0004C4A0
	public override void CreateGameMode()
	{
		if (this.m_minigameMetaData.gameMode == PsGameMode.Race)
		{
			this.m_gameMode = new PsGameModeRaceFresh(this);
		}
		else if (this.m_minigameMetaData.gameMode == PsGameMode.StarCollect)
		{
			this.m_gameMode = new PsGameModeAdventureFresh(this);
		}
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0004E0EC File Offset: 0x0004C4EC
	public override void BeginHeat()
	{
		this.m_startPosition = this.GetPosition();
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_COIN", true, true, true, true, false, false);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameBeginHeatStateFresh());
		this.m_gameMode.StartMinigame();
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x0004E13A File Offset: 0x0004C53A
	public override void BeginAdventure()
	{
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_COIN", true, true, true, true, false, false);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameBeginAdventureFreshState());
		this.m_gameMode.StartMinigame();
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0004E171 File Offset: 0x0004C571
	public override void CancelBegin()
	{
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameStartState());
		this.m_gameMode.EnterMinigame();
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0004E198 File Offset: 0x0004C598
	public override void WinMinigame()
	{
		base.WinMinigame();
		PsIngameMenu.CloseAll();
		this.m_gameMode.StopRecordingGhost();
		base.StopEveryplayAfterDelay();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_playerReachedGoal = true;
		PsState.m_activeMinigame.m_gameOn = false;
		this.m_exitingMinigame = false;
		PsIngameMenu.CloseAll();
		this.m_gameMode.WinMinigame();
		PsMetrics.LevelGoalReached();
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0004E1FE File Offset: 0x0004C5FE
	public override void WaitForUserToStart()
	{
		(PsIngameMenu.m_playMenu as PsUITopPlayRacing).CreateGoText();
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0004E20F File Offset: 0x0004C60F
	public override void StartLoop()
	{
		if (PsState.m_activeGameLoop != null)
		{
			return;
		}
		if (!this.m_eventOver)
		{
			new PsMenuDialogueFlow(this, PsNodeEventTrigger.LoopStart, 0f, delegate
			{
				this.SetAsActiveLoop();
				this.LoadMinigame();
			}, true);
		}
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0004E244 File Offset: 0x0004C644
	public override void LoadMinigame()
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
			Debug.LogWarning("LOAD METADATA");
		}
		else if (!string.IsNullOrEmpty(this.m_minigameId) && this.m_minigameMetaData != null)
		{
			this.LoadMinigameBytes();
		}
		else if (string.IsNullOrEmpty(this.m_minigameId))
		{
			this.m_loadingMetaData = true;
			this.GetFresh(null);
		}
		this.PreviewLoop();
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0004E340 File Offset: 0x0004C740
	public void GetFresh(HttpC _c = null)
	{
		HttpC fresh = MiniGame.GetFresh(new Action<PsMinigameMetaData>(this.LoadMetaDataSUCCESS), new Action<HttpC>(this.GetFreshFailed), PsState.GetCurrentVehicleType(false).ToString(), this.m_preferredGameMode, null);
		EntityManager.AddComponentToEntity(this.m_loaderEntity, fresh);
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x0004E38A File Offset: 0x0004C78A
	public void GetFreshFailed(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), () => MiniGame.GetFresh(new Action<PsMinigameMetaData>(this.LoadMetaDataSUCCESS), new Action<HttpC>(this.GetFreshFailed), PsState.GetCurrentVehicleType(false).ToString(), string.Empty, null), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x0004E3BD File Offset: 0x0004C7BD
	public override void PreviewLoop()
	{
		Main.m_currentGame.m_sceneManager.ChangeScene(new GameScene("GameScene", null), new PsRacingLoadingScene());
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0004E3DE File Offset: 0x0004C7DE
	public void CloseGameCard()
	{
		PsIngameMenu.CloseAll();
		this.ReleaseLoop();
		Main.m_currentGame.m_currentScene.m_stateMachine.RevertToPreviousState();
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0004E400 File Offset: 0x0004C800
	protected override void LoadMetaDataSUCCESS(PsMinigameMetaData _minigameMetaData)
	{
		if (_minigameMetaData != null)
		{
			this.m_loadingMetaData = false;
			this.m_minigameMetaData = _minigameMetaData;
			this.m_minigameId = _minigameMetaData.id;
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
			Debug.LogWarning("BEST TIME: " + HighScores.TimeScoreToTime(this.m_timeScoreBest));
			Debug.LogWarning("BEST SCORE: " + this.m_scoreBest);
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
			this.LoadMinigameBytes();
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0004E532 File Offset: 0x0004C932
	protected override void MinigameDownloadOk(byte[] _levelData)
	{
		base.MinigameDownloadOk(_levelData);
		if (this.m_gameMode is PsGameModeRace)
		{
			(this.m_gameMode as PsGameModeRace).CalculateMedalTimes();
		}
		this.LoadGhosts();
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0004E564 File Offset: 0x0004C964
	public override int GetPosition()
	{
		if (this.m_secondarysWon >= 4)
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

	// Token: 0x060006DC RID: 1756 RVA: 0x0004E5B4 File Offset: 0x0004C9B4
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
		PsMetrics.LevelEntered(text, text2, "level_fresh");
		FrbMetrics.SetCurrentScreen("level_fresh");
		FrbMetrics.LevelEntered();
		this.m_startPosition = this.GetPosition();
		Debug.LogWarning("ENTER MINIGAME");
		CameraS.m_updateComponents = true;
		CameraS.m_zoomMultipler = 2f;
		CameraS.m_zoomNeutralizer = 0.985f;
		CameraS.m_mainCameraRotationOffset = Vector3.up * -50f;
		CameraS.m_mainCameraPositionOffset = new Vector3(0f, 25f, 75f);
		CameraS.m_mainCameraZoomOffset = 100f;
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameEnter, 0.5f, delegate
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameStartState());
			this.m_gameMode.EnterMinigame();
		}, null);
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0004E69C File Offset: 0x0004CA9C
	public override void EnteredMinigameScene()
	{
		base.EnteredMinigameScene();
		this.SetPlaybackGhostAndCoins(true);
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0004E6AB File Offset: 0x0004CAAB
	public override void InitializeMinigame()
	{
		base.InitializeMinigame();
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0004E6B3 File Offset: 0x0004CAB3
	public override void CreateMinigame()
	{
		base.CreateMinigame();
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0004E6BC File Offset: 0x0004CABC
	public override void StartMinigame()
	{
		base.StartMinigame();
		this.m_eventOver = true;
		PsState.m_activeMinigame.m_gameStarted = true;
		this.m_gameMode.StartMinigame();
		if (this.m_gameMode is PsGameModeAdventure)
		{
			(PsIngameMenu.m_playMenu as PsUITopPlayAdventure).CreateRestartArea((PsIngameMenu.m_playMenu as PsUITopPlayAdventure).m_rightArea);
			(PsIngameMenu.m_playMenu as PsUITopPlayAdventure).CreateCoinArea();
			(PsIngameMenu.m_playMenu as PsUITopPlayAdventure).Update();
		}
		else
		{
			(PsIngameMenu.m_playMenu as PsUITopPlayRacing).CreateGoTween();
		}
		Random.seed = (int)(Main.m_gameTimeSinceAppStarted * 60.0);
		PsState.m_physicsPaused = false;
		PsState.m_activeMinigame.m_gameOn = true;
		PsState.m_activeMinigame.m_gameStartCount++;
		PsState.m_activeMinigame.m_playerReachedGoal = false;
		SoundS.PlaySingleShot("/InGame/GameStart", Vector3.zero, 1f);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GamePlayState());
		EveryplayManager.StartRecording();
		CameraS.m_zoomNeutralizer = 0.97f;
		(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.ResetCameraTarget();
		(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_lastGroundContact = Main.m_resettingGameTime;
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0004E800 File Offset: 0x0004CC00
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

	// Token: 0x060006E2 RID: 1762 RVA: 0x0004E860 File Offset: 0x0004CC60
	public override void ResumeMinigame()
	{
		base.ResumeMinigame();
		this.m_gameMode.CreatePlayMenu(new Action(this.SelfDestructPlayer), new Action(this.PauseMinigame));
		(PsIngameMenu.m_playMenu as PsUITopPlay).CreateRestartArea((PsIngameMenu.m_playMenu as PsUITopPlay).m_rightArea);
		(PsIngameMenu.m_playMenu as PsUITopPlay).CreateCoinArea();
		(PsIngameMenu.m_playMenu as PsUITopPlay).CreateLeftArea();
		(PsIngameMenu.m_playMenu as PsUITopPlay).Update();
		PsState.m_activeMinigame.SetGamePaused(false);
		base.ResumeElapsedTimer();
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0004E8F4 File Offset: 0x0004CCF4
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

	// Token: 0x060006E4 RID: 1764 RVA: 0x0004E970 File Offset: 0x0004CD70
	public virtual void RestartAction()
	{
		base.ResumeElapsedTimer();
		PsState.m_activeMinigame.SetGamePaused(false);
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		PsState.m_activeMinigame.m_groundNode.RevertGroundFromPlay();
		LevelManager.ResetCurrentLevel();
		this.InitializeMinigame();
		this.SetPlaybackGhostAndCoins(false);
		if (this.m_gameMode is PsGameModeAdventure)
		{
			this.BeginAdventure();
		}
		else
		{
			this.BeginHeat();
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x0004E9E0 File Offset: 0x0004CDE0
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
			PsMetrics.PlayerDied();
		}, null);
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0004EA4C File Offset: 0x0004CE4C
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
		PsMetrics.LevelExited(text, text2, "level_fresh");
		FrbMetrics.LevelExited(this.m_gameMode.ToString());
		base.ExitMinigame();
		EveryplayManager.StopRecording(0f);
		PsIngameMenu.CloseAll();
		PsMetagameManager.HideResources();
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameWin, 1f, delegate
		{
			this.EnterRating();
		}, null);
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0004EAE8 File Offset: 0x0004CEE8
	public void EnterRating()
	{
		PsIngameMenu.CloseAll();
		this.m_exitingMinigame = true;
		this.m_gameMode.m_waitForNextGhost = false;
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameExit, 0f, delegate
		{
			PsIngameMenu.CloseAll();
			this.m_gameMode.SendQuit();
			PsPlanetManager.m_timedEvents = null;
			PsMetagameManager.HideResources();
			if (this.m_eventOver)
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new PsRatingLoadingScene((this.GetRating() == PsRating.Unrated) ? typeof(PsUICenterRatingFresh) : typeof(PsUICenterLoadingWithoutRating)));
			}
			else
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLevelEndLoadingScene(Color.black, this, 0.25f));
			}
		}, null);
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0004EB1C File Offset: 0x0004CF1C
	public virtual Type GetRatingSceneType()
	{
		return typeof(PsUICenterRatingFresh);
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0004EB28 File Offset: 0x0004CF28
	public override void DestroyMinigame()
	{
		base.DestroyMinigame();
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		LevelManager.DestroyCurrentLevel();
		PsState.m_activeMinigame = null;
		this.ReleaseLoop();
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0004EB50 File Offset: 0x0004CF50
	public override void ReleaseLoop()
	{
		if (this.m_released)
		{
			return;
		}
		GameLevelPreview.RemoveLevelPreview();
		base.ReleaseLoop();
		this.m_scoreOld = this.m_scoreBest;
		this.m_rewardOld = this.m_scoreBest;
		this.SetActiveLoopNull();
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0004EB87 File Offset: 0x0004CF87
	public override string GetBannerString()
	{
		return PsStrings.Get(StringID.GOOD_OR_BAD);
	}

	// Token: 0x04000785 RID: 1925
	public string m_preferredGameMode;

	// Token: 0x04000786 RID: 1926
	public int m_startPosition;

	// Token: 0x04000787 RID: 1927
	public int m_secondarysWon;

	// Token: 0x04000788 RID: 1928
	public int m_scoreMultiplier;
}
