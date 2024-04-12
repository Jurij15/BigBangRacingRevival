using System;
using System.Collections;
using Server;
using UnityEngine;

// Token: 0x0200010A RID: 266
public class PsGameLoopEditor : PsGameLoop
{
	// Token: 0x060006A9 RID: 1705 RVA: 0x0004D2C4 File Offset: 0x0004B6C4
	public PsGameLoopEditor(PsMinigameMetaData _data)
		: this(_data.id, _data, true)
	{
		this.m_minigameMetaData = _data;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0004D2DC File Offset: 0x0004B6DC
	public PsGameLoopEditor(string _minigameId, PsMinigameMetaData _minigameMetaData, bool _autosave = true)
		: base(PsMinigameContext.Editor, _minigameId, null, -1, -1, -1, false, null)
	{
		this.m_minigameMetaData = _minigameMetaData;
		if (this.m_minigameMetaData != null)
		{
			this.m_existingMinigame = true;
		}
		this.m_autosave = _autosave;
		Debug.LogWarning("AUTOSAVE: " + this.m_autosave);
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0004D338 File Offset: 0x0004B738
	public override void StartLoop()
	{
		if (PsState.m_activeGameLoop != null)
		{
			return;
		}
		this.SetAsActiveLoop();
		Debug.LogWarning("AUTOSAVE: " + this.m_autosave);
		this.ResetTutorials();
		this.LoadMinigame();
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0004D374 File Offset: 0x0004B774
	public override void LoadMinigame()
	{
		CameraS.RemoveBlur();
		if (this.m_loaderEntity != null)
		{
			EntityManager.RemoveEntity(this.m_loaderEntity);
		}
		this.m_loaderEntity = null;
		this.m_loaderEntity = EntityManager.AddEntity();
		this.m_loaderEntity.m_persistent = true;
		if (this.m_minigameBytes != null)
		{
			this.MinigameDownloadOk(this.m_minigameBytes);
		}
		else if (this.m_minigameMetaData != null)
		{
			this.LoadMinigameBytes();
		}
		else if (!string.IsNullOrEmpty(this.m_minigameId))
		{
			this.m_loadingMetaData = true;
			HttpC httpC = MiniGame.Get(this.m_minigameId, new Action<PsMinigameMetaData>(this.LoadMetaDataSUCCESS), new Action<HttpC>(this.LoadMetaDataFAILED), null);
			httpC.objectData = this.m_minigameId;
		}
		Main.m_currentGame.m_sceneManager.ChangeScene(new EditorScene("EditorScene", false), new FadeLoadingScene(Color.black, true, 0.25f));
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0004D460 File Offset: 0x0004B860
	public void ResetTutorials()
	{
		Goal.SHOW_TUTORIAL = true;
		Vehicle.SHOW_TUTORIAL = true;
		CollectibleStar.SHOW_TUTORIAL = true;
		CollectibleStar.SHOULD_FADE = false;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0004D47A File Offset: 0x0004B87A
	protected override void LoadMetaDataSUCCESS(PsMinigameMetaData _minigameMetaData)
	{
		this.m_loadingMetaData = false;
		this.m_minigameMetaData = _minigameMetaData;
		this.m_minigameId = _minigameMetaData.id;
		this.LoadMinigameBytes();
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0004D49C File Offset: 0x0004B89C
	protected override void MinigameDownloadOk(byte[] _levelData)
	{
		if (_levelData.Length > 0)
		{
			this.m_minigameBytes = _levelData;
		}
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0004D4B0 File Offset: 0x0004B8B0
	public override void CreateMinigame()
	{
		Debug.LogWarning("GENERATE MINIGAME");
		bool flag;
		if (this.m_minigameBytes != null)
		{
			LevelManager.LoadLevel(FilePacker.UnZipBytes(this.m_minigameBytes), false);
			PsState.m_activeMinigame = LevelManager.m_currentLevel as Minigame;
			PsState.m_activeMinigame.m_editing = true;
			this.m_minigameMetaData.name = "Saved level " + this.m_saveNumber;
			if (LevelManager.m_currentLevel != null)
			{
				PsState.m_activeMinigame.m_groundNode = LevelManager.m_currentLevel.m_currentLayer.GetElement("LevelGround") as LevelGroundNode;
			}
			else
			{
				Debug.LogError("ERROR LOADING LEVEL FROM BYTES!!");
			}
			LevelManager.AssembleCurrentLevel();
			flag = true;
			this.CreateGameMode();
			this.m_gameMode.ApplySettings(true, false);
		}
		else
		{
			this.m_minigameMetaData = new PsMinigameMetaData();
			PsState.m_activeMinigame = DefaultMinigame.Assemble();
			PsState.m_activeMinigame.GenerateDefaultNameAndDescription();
			this.m_minigameMetaData.name = "Saved level " + this.m_saveNumber;
			PsState.m_activeMinigame.m_editing = true;
			PsState.m_activeMinigame.m_groundNode = LevelManager.m_currentLevel.m_currentLayer.GetElement("LevelGround") as LevelGroundNode;
			flag = false;
			this.CreateGameMode();
			this.m_gameMode.ApplySettings(true, false);
			CameraS.SnapMainCameraPos(Vector2.zero);
		}
		this.m_scoreCurrent = 0;
		this.m_timeScoreBest = this.m_minigameMetaData.timeScore;
		this.m_timeScoreCurrent = 0;
		if (flag)
		{
			AutoGeometryManager.UpdateMaxValueLookupTable(AutoGeometryManager.m_layers[PsState.m_drawLayer], LookupUpdateMode.LEVEL_LOADED, true);
		}
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x0004D644 File Offset: 0x0004BA44
	public override void EnteredMinigameScene()
	{
		EditorBaseState.m_enteredEditor = true;
		base.StartElapsedTimer();
		this.CreateMinigame();
		this.CreateEnvironment();
		this.EnterEditor();
		this.InitializeEditor();
		if (!PsState.m_muteMusic)
		{
			Minigame minigame = LevelManager.m_currentLevel as Minigame;
			minigame.m_music = SoundS.AddComponent(minigame.m_environmentTC, "/Music/EditorMusic", 1f, true);
			SoundS.PlaySound(minigame.m_music, true);
		}
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0004D6B4 File Offset: 0x0004BAB4
	public override void CreateEnvironment()
	{
		if (PsState.m_activeMinigame.m_backgroundPrefab != null)
		{
			PrefabS.RemoveComponent(PsState.m_activeMinigame.m_backgroundPrefab, true);
			PsState.m_activeMinigame.m_backgroundPrefab = null;
		}
		int num = (int)PsState.m_activeMinigame.m_settings["domeSizeIndex"];
		GameObject gameObject = null;
		if (PsState.GetVehicleIndex() == 0)
		{
			if (num != 0)
			{
				if (num != 1)
				{
					if (num == 2)
					{
						gameObject = ResourceManager.GetGameObject(RESOURCE.Map_Jungle_Large_GameObject);
					}
				}
				else
				{
					gameObject = ResourceManager.GetGameObject(RESOURCE.Map_Jungle_Medium_GameObject);
				}
			}
			else
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.Map_Jungle_Small_GameObject);
			}
		}
		else if (num != 0)
		{
			if (num != 1)
			{
				if (num == 2)
				{
					gameObject = ResourceManager.GetGameObject(RESOURCE.Map_Desert_Large_GameObject);
				}
			}
			else
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.Map_Desert_Medium_GameObject);
			}
		}
		else
		{
			gameObject = ResourceManager.GetGameObject(RESOURCE.Map_Desert_Small_GameObject);
		}
		PsState.m_activeMinigame.m_backgroundPrefab = PrefabS.AddComponent(PsState.m_activeMinigame.m_environmentTC, new Vector3(0f, 30f, 70f), gameObject);
		GameObject gameObject2 = PsState.m_activeMinigame.m_backgroundPrefab.p_gameObject.transform.Find("Static").gameObject;
		if (gameObject2)
		{
			StaticBatchingUtility.Combine(gameObject2);
		}
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0004D80C File Offset: 0x0004BC0C
	public override void CreateGameMode()
	{
		MotorcycleController.m_startCreate = false;
		if (this.m_minigameMetaData.gameMode == PsGameMode.Race)
		{
			this.m_gameMode = new PsGameModeRaceEditor(this);
		}
		else if (this.m_minigameMetaData.gameMode == PsGameMode.StarCollect)
		{
			this.m_gameMode = new PsGameModeAdventureEditor(this);
		}
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0004D860 File Offset: 0x0004BC60
	public override void EnterEditor()
	{
		Debug.LogWarning("ENTER EDITOR");
		PsState.m_activeMinigame.m_editing = true;
		PsState.m_physicsPaused = true;
		CameraS.m_updateComponents = true;
		if (string.IsNullOrEmpty(this.m_minigameMetaData.id))
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorPlayer(EditorSelectorContext.START_WIZARD));
		}
		else
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorBaseState());
		}
		if (this.m_minigameBytes != null)
		{
			this.m_gameMode.CreateEditMenu(delegate
			{
				this.ExitEditor(false, null);
			}, new Action(this.EnterMinigame));
		}
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0004D90A File Offset: 0x0004BD0A
	public void CreateEditMenu()
	{
		AutoGeometryManager.UpdateMaxValueLookupTable(AutoGeometryManager.m_layers[PsState.m_drawLayer], LookupUpdateMode.LEVEL_LOADED, true);
		this.m_gameMode.CreateEditMenu(delegate
		{
			this.ExitEditor(false, null);
		}, new Action(this.EnterMinigame));
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0004D946 File Offset: 0x0004BD46
	public override void InitializeEditor()
	{
		PsState.m_physicsPaused = true;
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0004D950 File Offset: 0x0004BD50
	public override void ExitEditor(bool _published = false, Action _customExitAction = null)
	{
		PsMetrics.LevelEditorExited(_published);
		PsIngameMenu.CloseAll();
		GizmoManager.ClearSelection();
		GizmoManager.Update();
		if (!_published)
		{
			this.SaveMinigame(true, _customExitAction);
		}
		else
		{
			PsMetagameManager.HideResources();
			Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLevelEndLoadingScene(Color.black, this, 0.25f));
		}
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0004D9B4 File Offset: 0x0004BDB4
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
		PsMetrics.LevelEntered(text, text2, "level_editor_test");
		FrbMetrics.SetCurrentScreen("level_editor_test");
		this.m_backAtMenu = false;
		Debug.LogWarning("ENTER MINIGAME");
		GizmoManager.ClearSelection();
		GizmoManager.Update();
		this.CreateGameMode();
		if (this.m_gameMode.GetType() == typeof(PsGameModeRaceEditor))
		{
			(this.m_gameMode as PsGameModeRaceEditor).CalculateMedalTimes();
		}
		PsState.m_activeMinigame.m_editing = false;
		PsState.m_physicsPaused = true;
		Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorTestState());
		(LevelManager.m_currentLevel as Minigame).m_groundNode.SaveGroundBeforePlay();
		LevelManager.ResetCurrentLevel();
		this.InitializeMinigame();
		this.m_gameMode.CreatePlayMenu(new Action(this.RestartMinigame), new Action(this.ExitMinigame));
		this.SaveMinigame(false, null);
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0004DADC File Offset: 0x0004BEDC
	public override void InitializeMinigame()
	{
		base.InitializeMinigame();
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x0004DAE4 File Offset: 0x0004BEE4
	public override void StartMinigame()
	{
		base.StartMinigame();
		SoundS.PlaySingleShot("/InGame/GameStart", Vector3.zero, 1f);
		Random.seed = (int)(Main.m_gameTimeSinceAppStarted * 60.0);
		PsState.m_physicsPaused = false;
		PsState.m_activeMinigame.m_gameStarted = true;
		PsState.m_activeMinigame.m_gameStartCount++;
		PsState.m_activeMinigame.m_playerReachedGoal = false;
		EveryplayManager.StartRecording();
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0004DB54 File Offset: 0x0004BF54
	public override void PauseMinigame()
	{
		if (PsState.m_activeMinigame.m_gamePaused || !PsState.m_activeMinigame.m_gameStarted || PsState.m_activeMinigame.m_gameEnded)
		{
			return;
		}
		base.PauseMinigame();
		PsState.m_activeMinigame.SetGamePaused(true);
		base.PauseElapsedTimer();
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x0004DBA6 File Offset: 0x0004BFA6
	public override void ResumeMinigame()
	{
		base.ResumeMinigame();
		PsState.m_activeMinigame.SetGamePaused(false);
		base.ResumeElapsedTimer();
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x0004DBC0 File Offset: 0x0004BFC0
	public override void LoseMinigame()
	{
		base.LoseMinigame();
		this.m_gameMode.StopRecordingGhost();
		PsIngameMenu.CloseAll();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_gameDeathCount++;
		PsMetrics.PlayerDied();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterLoseRace), typeof(PsUITopLoseRace), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", new Action(this.RestartMinigame));
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0004DC64 File Offset: 0x0004C064
	public override void WinMinigame()
	{
		base.WinMinigame();
		this.m_gameMode.StopRecordingGhost();
		EveryplayManager.StopRecording(0f);
		PsIngameMenu.CloseAll();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_playerReachedGoal = true;
		Debug.LogWarning(PsState.m_activeMinigame.m_gameTicks);
		if (this.GetGameMode() == PsGameMode.StarCollect)
		{
			this.m_scoreCurrent = PsState.m_activeMinigame.m_collectedStars;
		}
		else if (this.GetGameMode() == PsGameMode.Race)
		{
			this.m_scoreCurrent = 3;
		}
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterWinEditor), null, null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Win", new Action(this.WinMinigame));
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0004DD28 File Offset: 0x0004C128
	public override void RestartMinigame()
	{
		PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), null, PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().GetIdentifier(), false);
		base.RestartMinigame();
		EditorScene.ClearScreenshots();
		EveryplayManager.StopRecording(0f);
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		PsState.m_activeMinigame.m_groundNode.RevertGroundFromPlay();
		LevelManager.ResetCurrentLevel();
		this.InitializeMinigame();
		PsIngameMenu.CloseAll();
		this.m_gameMode.CreatePlayMenu(new Action(this.RestartMinigame), new Action(this.ExitMinigame));
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x0004DDC0 File Offset: 0x0004C1C0
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
		PsMetrics.LevelExited(text, text2, "level_editor_test");
		base.ExitMinigame();
		EveryplayManager.StopRecording(0f);
		PsIngameMenu.CloseAll();
		PsState.m_activeMinigame.m_editing = true;
		PsState.m_activeMinigame.m_gameStartCount = 0;
		PsState.m_activeMinigame.m_gameStarted = false;
		PsState.m_physicsPaused = true;
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		PsState.m_activeMinigame.m_groundNode.RevertGroundFromPlay();
		LevelManager.ResetCurrentLevel();
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorBaseState());
		this.m_gameMode.CreateEditMenu(delegate
		{
			this.ExitEditor(false, null);
		}, new Action(this.EnterMinigame));
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x0004DEAF File Offset: 0x0004C2AF
	public override void DestroyMinigame()
	{
		base.DestroyMinigame();
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		LevelManager.DestroyCurrentLevel();
		PsState.m_activeMinigame = null;
		PsState.m_activeGameLoop.m_minigameMetaData = null;
		PsState.m_activeGameLoop.m_minigameBytes = null;
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0004DEE2 File Offset: 0x0004C2E2
	public override void BackAtMenu()
	{
		if (!this.m_backAtMenu && !this.m_released)
		{
			this.m_backAtMenu = true;
			PsUITabbedCreate.m_selectedTab = 1;
			PsMainMenuState.ChangeToCreateState();
			this.ReleaseLoop();
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0004DF12 File Offset: 0x0004C312
	public override void ReleaseLoop()
	{
		if (this.m_released)
		{
			return;
		}
		base.ReleaseLoop();
		this.SetActiveLoopNull();
		this.ActivateCurrentLoop();
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0004DF34 File Offset: 0x0004C334
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

	// Token: 0x04000783 RID: 1923
	private bool m_existingMinigame;

	// Token: 0x04000784 RID: 1924
	public int m_saveNumber = -1;
}
