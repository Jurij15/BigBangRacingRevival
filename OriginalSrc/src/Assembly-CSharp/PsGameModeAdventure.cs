using System;
using Server;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class PsGameModeAdventure : PsGameModeBase
{
	// Token: 0x06000787 RID: 1927 RVA: 0x000546C6 File Offset: 0x00052AC6
	protected PsGameModeAdventure(PsGameMode _gameMode, PsGameLoop _info)
		: base(_gameMode, _info)
	{
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x000546D0 File Offset: 0x00052AD0
	public PsGameModeAdventure(PsGameLoop _info)
		: base(PsGameMode.StarCollect, _info)
	{
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x000546DC File Offset: 0x00052ADC
	public override void EnterMinigame()
	{
		if (PsCoinRoulette.ShowPopupAd(this.m_gameLoop) && PsCoinRoulette.IsAvailable(this.m_gameLoop))
		{
			TimerS.AddComponent(EntityManager.AddEntity(), string.Empty, 1.2f, 0f, true, delegate(TimerC c)
			{
				PsDialogue dialogueByIdentifier = PsMetagameData.GetDialogueByIdentifier("ad_popup_identifier");
				PsMinigameDialogueFlow psMinigameDialogueFlow = new PsMinigameDialogueFlow(dialogueByIdentifier, 0f, delegate
				{
					this.ShowAdPopup(true);
				}, null);
			});
		}
		else
		{
			this.ShowStartUI();
		}
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0005473C File Offset: 0x00052B3C
	public override void LoadGhosts(Action<GhostData[]> _customSUCCESSCallback = null)
	{
		this.ServerGetGhosts(new GhostGetData
		{
			minigameInfo = PsState.m_activeGameLoop,
			times = new int[] { int.MaxValue },
			playerId = this.m_gameLoop.GetCreatorId()
		}, _customSUCCESSCallback);
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00054788 File Offset: 0x00052B88
	public override void CreatePlaybackGhostVisuals()
	{
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0005478C File Offset: 0x00052B8C
	public override void CreateMusic()
	{
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		if (Random.Range(0, 2) < 1)
		{
			minigame.m_music = SoundS.AddComponent(minigame.m_environmentTC, PsFMODManager.GetMusic("PuzzleMusic1"), 1f, true);
		}
		else
		{
			minigame.m_music = SoundS.AddComponent(minigame.m_environmentTC, PsFMODManager.GetMusic("PuzzleMusic2"), 1f, true);
		}
		SoundS.PlaySound(minigame.m_music, true);
		SoundS.SetSoundParameter(minigame.m_music, "MusicState", 0f);
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00054819 File Offset: 0x00052C19
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUITopPlayAdventure(_restartAction, _pauseAction);
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00054834 File Offset: 0x00052C34
	protected virtual void ShowStartUI()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterStartAdventure), typeof(PsUITopStartAdventure), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.BeginAdventure));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		this.ShowStartResources(true);
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x000548F8 File Offset: 0x00052CF8
	public void ShowAdPopup(bool _tutorialReward = false)
	{
		PsUICenterRouletteCoin.IsTutorialSpin = _tutorialReward;
		PsMetrics.AdOffered("coinRoulette_popup");
		CoinStreakStyle prize = PsCoinRoulette.GetSurpriceCoinstreak();
		if (_tutorialReward)
		{
			prize = CoinStreakStyle.ROYAL;
		}
		else if (prize == CoinStreakStyle.COPPER_AND_GOLD && PsMetagameManager.IsTimedGiftActive(EventGiftTimedType.goldCoinStreak))
		{
			prize = CoinStreakStyle.ALL_GOLD;
		}
		PsUICenterRouletteCoin.m_prize = prize;
		PsIngameMenu.CloseAll();
		if (PsMetagameManager.m_playerStats.coinDoubler)
		{
			PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterRouletteCoin), null, null, null, true, true, InitialPage.Center, false, false, false);
		}
		else
		{
			PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICoinRouletteWithBanner), null, null, null, true, true, InitialPage.Center, false, false, false);
		}
		PsIngameMenu.m_popupMenu.SetAction("Confirm", delegate
		{
			this.m_gameLoop.RestartMinigame();
			this.ReplaceCoinsWithStreak(prize);
		});
		PsIngameMenu.m_popupMenu.SetAction("Exit", delegate
		{
			if (PsState.m_activeMinigame.m_gameStartCount > 0)
			{
				this.ShowLoseUI2();
			}
			else
			{
				this.ShowStartUI();
			}
		});
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x000549F1 File Offset: 0x00052DF1
	protected virtual void ReplaceCoinsWithStreak(CoinStreakStyle _newCoinStreak)
	{
		EntityManager.RemoveEntitiesByTag("GTAG_COIN");
		base.SetCoinStreakStyle(_newCoinStreak);
		base.GenerateCollectableCoins();
		base.PlaceCoins();
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x00054A10 File Offset: 0x00052E10
	protected override void ShowLoseUI2()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterLoseAdventure), typeof(PsUITopStartAdventure), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		this.ShowStartResources(true);
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00054AD4 File Offset: 0x00052ED4
	public virtual void ShowStartResources(bool _diamonds = true)
	{
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, true, false, _diamonds, false, 0.03f, true, false, false);
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00054AFC File Offset: 0x00052EFC
	public override void StartMinigame()
	{
		MotorcycleController.m_startCreate = true;
		PsIngameMenu.CloseAll();
		PsMetagameManager.HideResources();
		this.CreatePlayMenu(new Action(this.m_gameLoop.SelfDestructPlayer), new Action(this.m_gameLoop.PauseMinigame));
		if (PsState.m_activeMinigame.m_music != null)
		{
			SoundS.SetSoundParameter(PsState.m_activeMinigame.m_music, "MusicState", 1f);
		}
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00054B6C File Offset: 0x00052F6C
	public override void WinMinigame()
	{
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = HighScores.TicksToTimeScore(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks), true);
		float num = HighScores.TimeScoreToTime(this.m_gameLoop.m_timeScoreCurrent);
		Debug.LogWarning("YOUR TIME: " + num);
		int collectedStars = PsState.m_activeMinigame.m_collectedStars;
		if (collectedStars == 3)
		{
			PsAchievementManager.IncrementProgress("gainFiftyGoldMedals", 1);
		}
		this.m_gameLoop.m_currentRunScore = collectedStars;
		if (collectedStars >= this.m_gameLoop.m_scoreCurrent)
		{
			this.m_gameLoop.m_scoreCurrent = collectedStars;
			int num2 = this.m_gameLoop.m_scoreCurrent - this.m_gameLoop.m_scoreBest;
			if (num2 > 0)
			{
				this.GiveMapPieces(num2);
			}
			if (this.m_gameLoop.m_scoreCurrent > this.m_gameLoop.m_scoreBest)
			{
				this.m_gameLoop.m_scoreBest = this.m_gameLoop.m_scoreCurrent;
				this.m_gameLoop.m_minigameMetaData.score = this.m_gameLoop.m_scoreBest;
				this.m_gameLoop.m_timeScoreBest = this.m_gameLoop.m_timeScoreCurrent;
				this.m_gameLoop.m_minigameMetaData.timeScore = this.m_gameLoop.m_timeScoreBest;
				PsMetricsData.m_isSendingGhost = true;
				PsMetricsData.m_isNewGhost = true;
				this.m_saveGhost = true;
			}
			else if (this.m_gameLoop.m_scoreCurrent == this.m_gameLoop.m_scoreBest && this.m_gameLoop.m_timeScoreCurrent < this.m_gameLoop.m_timeScoreBest)
			{
				this.m_gameLoop.m_timeScoreBest = this.m_gameLoop.m_timeScoreCurrent;
				this.m_gameLoop.m_minigameMetaData.timeScore = this.m_gameLoop.m_timeScoreBest;
				PsMetricsData.m_isSendingGhost = true;
				PsMetricsData.m_isNewGhost = true;
				this.m_saveGhost = true;
			}
		}
		base.CheckAchievementsOnWin();
		Debug.LogWarning(string.Concat(new object[]
		{
			"SEND SCORE TO SERVER! time: ",
			HighScores.TimeScoreToTime(this.m_gameLoop.m_timeScoreBest),
			", score: ",
			this.m_gameLoop.m_scoreBest
		}));
		Debug.LogWarning("OLD TIME: " + HighScores.TimeScoreToTime(this.m_gameLoop.m_timeScoreOld));
		Debug.LogWarning("OLD SCORE: " + this.m_gameLoop.m_scoreOld);
		if (this.m_saveGhost)
		{
			this.SendHighscoreAndGhost();
		}
		this.CreateWinUI();
		base.WinMinigame();
		this.m_gameLoop.m_rewardOld = collectedStars;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00054E00 File Offset: 0x00053200
	protected virtual void GiveMapPieces(int _scoreChange)
	{
		int vehicleIndex = PsState.GetVehicleIndex();
		if (PsMetagameManager.m_vehicleGachaData.m_mapPieceCount < PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax)
		{
			PsMetagameManager.m_vehicleGachaData.m_mapPieceCount = Mathf.Min(PsMetagameManager.m_vehicleGachaData.m_mapPieceCount + _scoreChange, PsMetagameManager.m_vehicleGachaData.m_mapPiecesMax);
			PsMetagameManager.SendCurrentGachaData(true);
		}
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00054E57 File Offset: 0x00053257
	public override void LoseMinigame()
	{
		base.ShowLoseUI();
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00054E60 File Offset: 0x00053260
	protected override void CreateWinUI()
	{
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterWinAdventure), typeof(PsUITopWinAdventure), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, false, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00054EF0 File Offset: 0x000532F0
	public override void PauseMinigame()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterPauseRace), typeof(PsUITopPauseRace), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", new Action(this.m_gameLoop.ResumeSelfDestruct));
		PsIngameMenu.m_popupMenu.SetAction("Resume", new Action(this.m_gameLoop.ResumeMinigame));
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00054FAC File Offset: 0x000533AC
	protected void SendHighscoreAndGhost()
	{
		this.m_waitForHighscoreAndGhost = true;
		this.m_waitForNextGhost = false;
		DataBlob dataBlob = default(DataBlob);
		if (this.m_saveGhost)
		{
			if (this.m_recordingGhost != null && !this.m_recordingGhost.m_recording)
			{
				dataBlob = ClientTools.CreateGhostDataBlob(this.m_recordingGhost);
			}
			PsMetricsData.m_lastGhostSize = dataBlob.data.Length;
		}
		else
		{
			PsMetricsData.m_lastGhostSize = 0;
		}
		Debug.LogWarning("LAST SENT GHOST SIZE: " + PsMetricsData.m_lastGhostSize);
		bool flag = this.m_gameLoop.m_path != null && this.m_gameLoop.m_path.m_startNodeId == 0;
		new PsServerQueueFlow(null, delegate
		{
			this.SendHighscoreAndGhostSafe(new StarCollect.StarCollectData(this.m_gameLoop.m_minigameId, PsState.m_activeMinigame.m_gameStartCount, PsState.m_activeMinigame.m_playerUnitName, this.m_gameLoop.m_currentRunScore, this.m_gameLoop.m_timeScoreCurrent, dataBlob, null, PsMetricsData.m_lastGhostSize));
		}, new string[] { "SetData" });
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00055098 File Offset: 0x00053498
	protected HttpC SendHighscoreAndGhostSafe(StarCollect.StarCollectData _data)
	{
		PsMetricsData.m_lastGhostSize = _data.m_ghostSize;
		return StarCollect.Win(_data, new Action<HttpC>(this.HighscoreServerRequestOK), delegate(HttpC c)
		{
			this.HighscoreServerRequestFAILED(c, _data);
		}, null);
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x000550F0 File Offset: 0x000534F0
	protected void HighscoreServerRequestFAILED(HttpC _c, StarCollect.StarCollectData _data)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), () => this.SendHighscoreAndGhostSafe(_data), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00055144 File Offset: 0x00053544
	protected void HighscoreServerRequestOK(HttpC _c)
	{
		PsMetagameManager.m_playerStats.adventureLevels++;
		Debug.LogWarning("HIGHSCORES AND GHOST SENT, LOADING NEW GHOST");
		PsState.m_activeMinigame.m_gameStartCount = 0;
		PsMetricsData.m_isSendingGhost = false;
		if (this.m_saveGhost)
		{
			this.m_waitForHighscoreAndGhost = false;
		}
	}

	// Token: 0x040007BD RID: 1981
	public bool m_saveGhost;
}
