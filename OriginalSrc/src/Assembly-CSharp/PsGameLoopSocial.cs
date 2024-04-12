using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class PsGameLoopSocial : PsGameLoop
{
	// Token: 0x06000738 RID: 1848 RVA: 0x00050CF6 File Offset: 0x0004F0F6
	public PsGameLoopSocial(PsMinigameMetaData _data)
		: this(_data.id, _data, null, -1, -1, 0, false)
	{
		this.m_minigameMetaData = _data;
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00050D14 File Offset: 0x0004F114
	public PsGameLoopSocial(string _minigameId, PsMinigameMetaData _metaData, PsPlanetPath _path = null, int _id = -1, int _levelNumber = -1, int _score = 0, bool _unlocked = false)
		: base(PsMinigameContext.Social, _minigameId, _path, _id, _levelNumber, _score, _unlocked, null)
	{
		this.m_minigameMetaData = _metaData;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00050D3A File Offset: 0x0004F13A
	public override float GetOverrideCC()
	{
		if (this.m_minigameMetaData != null)
		{
			return this.m_minigameMetaData.overrideCC;
		}
		return -1f;
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00050D58 File Offset: 0x0004F158
	public override void WaitForUserToStart()
	{
		(PsIngameMenu.m_playMenu as PsUITopPlayRacing).CreateGoText();
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x00050D6C File Offset: 0x0004F16C
	public override void CreateGameMode()
	{
		if (this.m_minigameMetaData.gameMode == PsGameMode.Race)
		{
			this.m_gameMode = new PsGameModeRaceSocial(this);
		}
		else if (this.m_minigameMetaData.gameMode == PsGameMode.StarCollect)
		{
			this.m_gameMode = new PsGameModeAdventureSocial(this);
		}
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x00050DB8 File Offset: 0x0004F1B8
	public override void BeginHeat()
	{
		this.m_startPosition = this.GetPosition();
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_COIN", true, true, true, true, false, false);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameBeginHeatStateFresh());
		this.m_gameMode.StartMinigame();
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x00050E06 File Offset: 0x0004F206
	public override void BeginAdventure()
	{
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_COIN", true, true, true, true, false, false);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameBeginAdventureState());
		this.m_gameMode.StartMinigame();
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00050E3D File Offset: 0x0004F23D
	public override void CancelBegin()
	{
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameStartState());
		this.m_gameMode.EnterMinigame();
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x00050E63 File Offset: 0x0004F263
	public override void StartLoop()
	{
		if (PsState.m_activeGameLoop != null)
		{
			return;
		}
		this.SetAsActiveLoop();
		if (this.m_minigameMetaData != null)
		{
			this.LoadMinigame();
		}
		else
		{
			this.LoadMinigameMetaData(new Action(this.LoadMinigame));
		}
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x00050EA0 File Offset: 0x0004F2A0
	public override int GetPosition()
	{
		if (this.m_raceGhostCount == 0)
		{
			return 1;
		}
		int num = 4;
		if (this.m_secondarysWon >= 4)
		{
			num = 1;
		}
		else if (this.m_secondarysWon >= 2 || this.m_raceGhostCount < 2)
		{
			num = 2;
		}
		else if (this.m_secondarysWon == 1 || this.m_raceGhostCount < 3)
		{
			num = 3;
		}
		return num;
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00050F09 File Offset: 0x0004F309
	public void FadeInLoadingScreen(TimerC _c)
	{
		this.LoadMinigame();
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00050F11 File Offset: 0x0004F311
	public override void LoopUnlocked()
	{
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00050F14 File Offset: 0x0004F314
	public override void LoadMinigame()
	{
		if (this.m_loaderEntity != null)
		{
			EntityManager.RemoveEntity(this.m_loaderEntity);
		}
		this.m_loaderEntity = null;
		this.m_loaderEntity = EntityManager.AddEntity();
		this.m_loaderEntity.m_persistent = true;
		this.m_loadingMetaData = false;
		if (this.m_gameMode == null)
		{
			this.CreateGameMode();
		}
		this.m_scoreCurrent = 0;
		this.m_timeScoreCurrent = int.MaxValue;
		this.m_timeScoreBest = this.m_minigameMetaData.timeScore;
		if (this.m_timeScoreBest == 0)
		{
			this.m_timeScoreBest = int.MaxValue;
		}
		this.m_timeScoreOld = this.m_timeScoreBest;
		this.m_scoreOld = this.m_scoreBest;
		this.m_rewardOld = this.m_scoreBest;
		Debug.LogWarning("------- BEST TIME: " + HighScores.TimeScoreToTime(this.m_timeScoreBest));
		Debug.LogWarning("------- BEST SCORE: " + this.m_scoreBest);
		this.m_loadMetadataCallback = null;
		this.LoadMinigameBytes();
		this.PreviewLoop();
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00051016 File Offset: 0x0004F416
	public override void PreviewLoop()
	{
		Main.m_currentGame.m_sceneManager.ChangeScene(new GameScene("GameScene", null), new PsRacingLoadingScene());
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00051037 File Offset: 0x0004F437
	protected override void MinigameDownloadOk(byte[] _levelData)
	{
		base.MinigameDownloadOk(_levelData);
		if (this.m_gameMode is PsGameModeRace)
		{
			(this.m_gameMode as PsGameModeRace).CalculateMedalTimes();
		}
		this.LoadGhosts();
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00051068 File Offset: 0x0004F468
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
		PsMetrics.LevelEntered(text, text2, "level_social");
		FrbMetrics.SetCurrentScreen("level_social");
		FrbMetrics.LevelEntered();
		this.m_startPosition = this.GetPosition();
		Debug.LogWarning("ENTER MINIGAME");
		CameraS.m_updateComponents = true;
		CameraS.m_zoomMultipler = 2f;
		CameraS.m_zoomNeutralizer = 0.985f;
		CameraS.m_mainCameraRotationOffset = Vector3.up * -50f;
		CameraS.m_mainCameraPositionOffset = new Vector3(0f, 25f, 75f);
		CameraS.m_mainCameraZoomOffset = 100f;
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameStartState());
		this.m_gameMode.EnterMinigame();
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x0005115A File Offset: 0x0004F55A
	public override void InitializeMinigame()
	{
		base.InitializeMinigame();
		this.SetPlaybackGhostAndCoins(true);
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00051169 File Offset: 0x0004F569
	public override void SetPlaybackGhostAndCoins(bool _regenerateCoins = true)
	{
		this.m_gameMode.CreatePlaybackGhostVisuals();
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00051178 File Offset: 0x0004F578
	public override void StartMinigame()
	{
		base.StartMinigame();
		PsState.m_activeMinigame.m_gameStarted = true;
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
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0005130C File Offset: 0x0004F70C
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

	// Token: 0x0600074C RID: 1868 RVA: 0x0005136C File Offset: 0x0004F76C
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

	// Token: 0x0600074D RID: 1869 RVA: 0x00051400 File Offset: 0x0004F800
	public override void RestartMinigame()
	{
		PsMetrics.LevelRestarted();
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

	// Token: 0x0600074E RID: 1870 RVA: 0x00051480 File Offset: 0x0004F880
	public virtual void RestartAction()
	{
		base.ResumeElapsedTimer();
		PsState.m_activeMinigame.SetGamePaused(false);
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		PsState.m_activeMinigame.m_groundNode.RevertGroundFromPlay();
		LevelManager.ResetCurrentLevel();
		this.InitializeMinigame();
		if (this.m_gameMode is PsGameModeAdventure)
		{
			this.BeginAdventure();
		}
		else
		{
			this.BeginHeat();
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x000514E8 File Offset: 0x0004F8E8
	public override void LoseMinigame()
	{
		base.LoseMinigame();
		this.m_gameMode.StopRecordingGhost();
		PsIngameMenu.CloseAll();
		base.StopEveryplayAfterDelay();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_gameOn = false;
		PsState.m_activeMinigame.m_gameDeathCount++;
		this.m_gameMode.LoseMinigame();
		PsMetrics.PlayerDied();
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x0005154C File Offset: 0x0004F94C
	public override void WinMinigame()
	{
		base.WinMinigame();
		this.m_gameMode.StopRecordingGhost();
		PsIngameMenu.CloseAll();
		base.StopEveryplayAfterDelay();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_playerReachedGoal = true;
		PsState.m_activeMinigame.m_gameOn = false;
		this.m_exitingMinigame = false;
		this.m_gameMode.WinMinigame();
		PsMetrics.LevelGoalReached();
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x000515B0 File Offset: 0x0004F9B0
	public override void ExitMinigame()
	{
		string empty = string.Empty;
		if (base.IsOwnLevel())
		{
		}
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
		PsMetrics.LevelExited(text, text2, "level_social");
		FrbMetrics.LevelExited(this.m_gameMode.ToString());
		base.ExitMinigame();
		EveryplayManager.StopRecording(0f);
		this.m_exitingMinigame = true;
		this.m_gameMode.m_waitForNextGhost = false;
		PsIngameMenu.CloseAll();
		this.m_gameMode.SendQuit();
		PsMetagameManager.HideResources();
		bool flag = this.GetRating() == PsRating.Unrated;
		bool flag2 = this.GetCreatorId() == PlayerPrefsX.GetUserId();
		if (this.m_returnToChat)
		{
			PsUIBaseState psUIBaseState = new PsTeamState();
			PsMenuScene.m_lastIState = psUIBaseState;
			PsMenuScene.m_lastState = null;
		}
		else
		{
			if ((PsCaches.m_searchedLevelList.GetItemCount() > 0 || PsCaches.m_searchedPlayersList.GetItemCount() > 0) && PsUITabbedCreate.m_selectedTab == 3)
			{
				PsUICenterSearch.m_lastLevelId = this.GetGameId();
				PsUICenterSearch.m_lastPlayerId = this.GetCreatorId();
			}
			else if (PsUITabbedCreate.m_selectedTab == 2)
			{
				PsUIFriendProfiles.m_lastLevelId = this.GetGameId();
				PsUIFriendProfiles.m_lastPlayerId = this.GetCreatorId();
			}
			if (!this.m_returnToMenu)
			{
				PsUIBaseState psUIBaseState2 = new PsCreateState();
				PsMenuScene.m_lastIState = psUIBaseState2;
				PsMenuScene.m_lastState = null;
			}
		}
		Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new PsRatingLoadingScene(flag ? ((!flag2) ? typeof(PsUICenterRatingFresh) : typeof(PsUICenterLoadingRatingOwnLevel)) : typeof(PsUICenterLoadingWithoutRating)));
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0005178C File Offset: 0x0004FB8C
	public override void DestroyMinigame()
	{
		base.DestroyMinigame();
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		LevelManager.DestroyCurrentLevel();
		PsState.m_activeMinigame = null;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x000517AE File Offset: 0x0004FBAE
	public override void BackAtMenu()
	{
		if (!this.m_backAtMenu && !this.m_released)
		{
			this.m_backAtMenu = true;
			this.ReleaseLoop();
		}
		this.m_backAtMenu = false;
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x000517DC File Offset: 0x0004FBDC
	public override void ReleaseLoop()
	{
		PsMenuScene.m_lastIState = null;
		if (this.m_released)
		{
			return;
		}
		GameLevelPreview.RemoveLevelPreview();
		base.ReleaseLoop();
		this.m_scoreOld = this.m_scoreBest;
		this.m_rewardOld = this.m_scoreBest;
		this.SetActiveLoopNull();
		this.ActivateCurrentLoop();
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0005182C File Offset: 0x0004FC2C
	public override void ActivateCurrentLoop()
	{
		PsGameLoop currentNodeInfo = this.m_planet.GetMainPath().GetCurrentNodeInfo();
		if (currentNodeInfo != null && currentNodeInfo.m_node != null)
		{
			currentNodeInfo.m_node.Activate();
		}
		if (this.m_winFirstTime && currentNodeInfo != null && !currentNodeInfo.m_activated)
		{
			currentNodeInfo.SaveAsActivated();
		}
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00051888 File Offset: 0x0004FC88
	public override void CreateMinigame()
	{
		base.CreateMinigame();
	}

	// Token: 0x040007AD RID: 1965
	public int m_startPosition;

	// Token: 0x040007AE RID: 1966
	public int m_secondarysWon;

	// Token: 0x040007AF RID: 1967
	public bool m_returnToMenu;

	// Token: 0x040007B0 RID: 1968
	public bool m_returnToChat;
}
