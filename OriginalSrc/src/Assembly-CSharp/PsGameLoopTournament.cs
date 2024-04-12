using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class PsGameLoopTournament : PsGameLoopNews
{
	// Token: 0x06000757 RID: 1879 RVA: 0x00051890 File Offset: 0x0004FC90
	public PsGameLoopTournament(EventMessage _msg, PsMinigameContext _ctx, PsPlanetPath _path)
		: base(_msg, _ctx, _path)
	{
		if (_msg.tournament != null && !string.IsNullOrEmpty(_msg.tournament.minigameId))
		{
			this.m_minigameId = _msg.tournament.minigameId;
		}
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x000518CC File Offset: 0x0004FCCC
	public override float GetOverrideCC()
	{
		return this.GetCcCap();
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x000518D4 File Offset: 0x0004FCD4
	public override PlayerData GetCreator()
	{
		PlayerData creator = base.GetCreator();
		creator.youtubeId = this.m_eventMessage.tournament.youtuberId;
		creator.youtubeName = this.m_eventMessage.tournament.youtuberName;
		creator.youtubeSubscriberCount = this.m_eventMessage.tournament.youtubeSubscriberCount;
		return creator;
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0005192E File Offset: 0x0004FD2E
	public override Type GetLoadingScreenComponent()
	{
		return typeof(PsUICenterTournamentLoading);
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0005193C File Offset: 0x0004FD3C
	public float GetCcCap()
	{
		if (this.m_useCreatorUpgrades && this.m_minigameMetaData != null && this.m_minigameMetaData.creatorUpgrades != null && this.m_minigameMetaData.creatorUpgrades.ContainsKey("cc"))
		{
			return Convert.ToSingle(this.m_minigameMetaData.creatorUpgrades["cc"]);
		}
		return this.m_eventMessage.tournament.cc;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x000519B4 File Offset: 0x0004FDB4
	public void PurchasePowerFuel()
	{
		this.m_eventMessage.tournament.hasSuperFuel = (this.m_hasSuperFuel = true);
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x000519DB File Offset: 0x0004FDDB
	public override void CancelBegin()
	{
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameStartState());
		this.m_gameMode.EnterMinigame();
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00051A04 File Offset: 0x0004FE04
	public override string GetBannerString()
	{
		if ((double)this.m_eventMessage.localStartTime > Main.m_EPOCHSeconds)
		{
			string text = PsStrings.Get(StringID.TOUR_TOURNAMENT_STARTS_IN);
			return text.Replace("%1", PsMetagameManager.GetTimeStringFromSeconds((int)((double)this.m_eventMessage.localStartTime - Main.m_EPOCHSeconds)));
		}
		return base.GetBannerString();
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x00051A60 File Offset: 0x0004FE60
	public override void StartLoop()
	{
		if (PsState.m_activeGameLoop == null && this.m_eventMessage != null && this.m_eventMessage.tournament != null && this.m_eventMessage.tournament.minigameId != null && this.m_eventMessage.messageId == PsMetagameManager.m_activeTournament.messageId)
		{
			this.m_hasSuperFuel = this.m_eventMessage.tournament.hasSuperFuel;
			this.m_overrideCC = this.m_eventMessage.tournament.cc;
			this.m_useCreatorUpgrades = this.m_eventMessage.tournament.useCreatorUpgrades;
			if ((double)this.m_eventMessage.localStartTime >= Main.m_EPOCHSeconds)
			{
				Debug.LogError("Tournament not yet started");
			}
			else
			{
				if (this.m_eventMessage.tournament.joined)
				{
					if (PsMetagameManager.m_activeTournament.tournament.time != 0)
					{
						this.m_timeScoreBest = PsMetagameManager.m_activeTournament.tournament.time;
					}
					else
					{
						this.m_timeScoreBest = int.MaxValue;
					}
					this.LoadLeaderboardData();
				}
				else
				{
					PlayerPrefsX.SetTournamentChatTimestamp(0);
					this.JoinTournament();
				}
				this.PlayTournament();
			}
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00051B94 File Offset: 0x0004FF94
	private void JoinTournament()
	{
		Tournament.TournamentJoinData tournamentJoinData = new Tournament.TournamentJoinData();
		tournamentJoinData.setData = new Hashtable();
		if (PsMetagameManager.m_playerStats.tournamentBoosters < PlayerPrefsX.GetMinimumTournamentsNitro())
		{
			PsMetagameManager.m_playerStats.tournamentBoosters = PlayerPrefsX.GetMinimumTournamentsNitro();
			List<string> list = new List<string>();
			tournamentJoinData.setData.Add("update", ClientTools.GeneratePlayerSetData(new Hashtable(), ref list));
		}
		tournamentJoinData.tournamentId = this.m_eventMessage.tournament.tournamentId;
		HttpC httpC = Tournament.Join(tournamentJoinData, new Action<HttpC>(this.JoinSucceed), new Action<HttpC>(this.JoinFailed), null);
		httpC.objectData = tournamentJoinData;
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00051C34 File Offset: 0x00050034
	private void JoinSucceed(HttpC _c)
	{
		this.m_eventMessage.tournament.joined = true;
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("tournamentRoom"))
		{
			this.m_eventMessage.tournament.room = Convert.ToInt32(dictionary["tournamentRoom"]);
		}
		else
		{
			Debug.LogError("No tournament room!");
		}
		this.m_freshJoin = true;
		this.LoadLeaderboardData();
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00051CAF File Offset: 0x000500AF
	private void LoadLeaderboardData()
	{
		PsUITournamentLeaderboard.GetLeaderboardData(delegate
		{
			this.m_leaderboardLoaded = true;
			this.ExtrasLoaded();
		});
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00051CC4 File Offset: 0x000500C4
	private void JoinFailed(HttpC _c)
	{
		Debug.LogError("JOIN FAILED");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC httpC = Tournament.Join(_c.objectData as Tournament.TournamentJoinData, new Action<HttpC>(this.JoinSucceed), new Action<HttpC>(this.JoinFailed), null);
			httpC.objectData = _c.objectData;
			return httpC;
		}, null);
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00051D16 File Offset: 0x00050116
	private void PlayTournament()
	{
		this.SetAsActiveLoop();
		this.LoadMinigame();
		this.PreviewLoop();
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00051D2A File Offset: 0x0005012A
	public override void PreviewLoop()
	{
		Main.m_currentGame.m_sceneManager.ChangeScene(new GameScene("GameScene", null), new PsRacingLoadingScene());
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x00051D4C File Offset: 0x0005014C
	protected override void LoadMetaDataSUCCESS(PsMinigameMetaData _minigameMetaData)
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

	// Token: 0x06000767 RID: 1895 RVA: 0x00051E56 File Offset: 0x00050256
	protected override void MinigameDownloadOk(byte[] _levelData)
	{
		base.MinigameDownloadOk(_levelData);
		this.LoadGhosts();
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00051E68 File Offset: 0x00050268
	public override void EnterMinigame()
	{
		FrbMetrics.LevelEntered();
		FrbMetrics.SetCurrentScreen("tournament_run");
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

	// Token: 0x06000769 RID: 1897 RVA: 0x00051EF9 File Offset: 0x000502F9
	public override void CreateGameMode()
	{
		this.m_gameMode = new PsGameModeTournament(this);
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00051F07 File Offset: 0x00050307
	public override void BeginHeat()
	{
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_COIN", true, true, true, true, false, false);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameBeginHeatStateFresh());
		this.m_gameMode.StartMinigame();
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00051F3E File Offset: 0x0005033E
	public override void WaitForUserToStart()
	{
		(PsIngameMenu.m_playMenu as PsUITopPlayRacing).CreateGoText();
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00051F50 File Offset: 0x00050350
	public override void StartMinigame()
	{
		base.StartMinigame();
		PsState.m_activeMinigame.m_gameStarted = true;
		(PsIngameMenu.m_playMenu as PsUITopPlayRacing).CreateGoTween();
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
		PsMetrics.TournamentRunStarted(this.m_eventMessage.tournament.tournamentId, this.m_eventMessage.tournament.room);
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00052054 File Offset: 0x00050454
	public void SendResources()
	{
		PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x000520A8 File Offset: 0x000504A8
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
		this.SendResources();
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0005211C File Offset: 0x0005051C
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

	// Token: 0x06000770 RID: 1904 RVA: 0x00052198 File Offset: 0x00050598
	public virtual void RestartAction()
	{
		base.ResumeElapsedTimer();
		PsState.m_activeMinigame.SetGamePaused(false);
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		PsState.m_activeMinigame.m_groundNode.RevertGroundFromPlay();
		LevelManager.ResetCurrentLevel();
		this.InitializeMinigame();
		this.BeginHeat();
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x000521E5 File Offset: 0x000505E5
	public override void InitializeMinigame()
	{
		base.InitializeMinigame();
		this.SetPlaybackGhostAndCoins(true);
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x000521F4 File Offset: 0x000505F4
	public void InitializeSpectate()
	{
		PsState.m_physicsPaused = true;
		PsState.m_activeMinigame.m_gameOn = false;
		PsState.m_activeMinigame.m_gameTicks = 0f;
		PsState.m_activeMinigame.m_gameTicksFreezed = false;
		PsState.m_activeMinigame.m_gravityMultipler = 1;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0005222C File Offset: 0x0005062C
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

	// Token: 0x06000774 RID: 1908 RVA: 0x00052294 File Offset: 0x00050694
	public override void ExitMinigame()
	{
		string empty = string.Empty;
		if (base.IsOwnLevel())
		{
		}
		FrbMetrics.LevelExited("tournament");
		base.ExitMinigame();
		EveryplayManager.StopRecording(0f);
		this.m_exitingMinigame = true;
		this.m_gameMode.m_waitForNextGhost = false;
		PsIngameMenu.CloseAll();
		PsMetagameManager.HideResources();
		Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new PsRatingLoadingScene(typeof(PsUICenterTournamentLoadingWithoutRating)));
		PsUITournamentLeaderboard.m_oldLeaderboard = null;
		string tournamentId = this.m_eventMessage.tournament.tournamentId;
		int room = this.m_eventMessage.tournament.room;
		PsMetrics.TournamentRoomExited(tournamentId, room);
		if ((this.m_eventMessage.localEndTime < (int)Math.Ceiling(Main.m_EPOCHSeconds) && !this.m_eventMessage.tournament.claimed && !this.m_eventMessage.tournament.acceptingNewScores && (this.m_eventMessage.tournament.time == 0 || this.m_eventMessage.tournament.time == 2147483647)) || (!this.m_eventMessage.tournament.acceptingNewScores && PsUICenterTournament.m_isHost && !this.m_eventMessage.tournament.claimed))
		{
			this.m_eventMessage.tournament.claimed = true;
			new PsServerQueueFlow(null, delegate
			{
				PsGameModeTournament.Claim(new Hashtable());
			}, new string[] { "SetData" });
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0005243D File Offset: 0x0005083D
	public override void BackAtMenu()
	{
		if (!this.m_backAtMenu && !this.m_released)
		{
			this.m_backAtMenu = true;
			this.ReleaseLoop();
		}
		this.m_backAtMenu = false;
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0005246C File Offset: 0x0005086C
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

	// Token: 0x06000777 RID: 1911 RVA: 0x000524BC File Offset: 0x000508BC
	public override void DestroyMinigame()
	{
		if (this.m_spectateGhostLoaderEntity != null)
		{
			Debug.LogError("Destroying spectate entity");
			EntityManager.RemoveEntity(this.m_spectateGhostLoaderEntity);
			this.m_spectateGhostLoaderEntity = null;
		}
		base.DestroyMinigame();
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		LevelManager.DestroyCurrentLevel();
		if (PsUICenterTournament.m_lb != null)
		{
			PsUICenterTournament.m_lb.Destroy();
			PsUICenterTournament.m_lb = null;
		}
		PsState.m_activeMinigame = null;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x0005252A File Offset: 0x0005092A
	public override void PlayerDataLoaded(PlayerData _data)
	{
		this.m_playerData = _data;
		this.ExtrasLoaded();
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0005253C File Offset: 0x0005093C
	public void ExtrasLoaded()
	{
		if (this.m_leaderboardLoaded && !string.IsNullOrEmpty(this.m_playerData.playerId))
		{
			if (this.m_freshJoin)
			{
				string tournamentId = this.m_eventMessage.tournament.tournamentId;
				int room = this.m_eventMessage.tournament.room;
				float cc = this.m_eventMessage.tournament.cc;
				Type currentVehicleType = PsState.GetCurrentVehicleType(true);
				float currentPerformance = PsUpgradeManager.GetCurrentPerformance(currentVehicleType);
				PsMetrics.TournamentSignup(tournamentId, room, cc, currentVehicleType, currentPerformance);
				this.m_freshJoin = false;
			}
			PsMetrics.TournamentRoomEntered(this.m_eventMessage.tournament.tournamentId, this.m_eventMessage.tournament.room);
			base.PlayerDataLoaded(this.m_playerData);
		}
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x000525FC File Offset: 0x000509FC
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

	// Token: 0x0600077B RID: 1915 RVA: 0x0005265C File Offset: 0x00050A5C
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

	// Token: 0x040007B1 RID: 1969
	private bool m_joined;

	// Token: 0x040007B2 RID: 1970
	public bool m_hasSuperFuel;

	// Token: 0x040007B3 RID: 1971
	public bool m_freshJoin;

	// Token: 0x040007B4 RID: 1972
	public Entity m_spectateGhostLoaderEntity;

	// Token: 0x040007B5 RID: 1973
	private bool m_leaderboardLoaded;
}
