using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class PsGameModeRacing : PsGameModeBase
{
	// Token: 0x06000833 RID: 2099 RVA: 0x000587ED File Offset: 0x00056BED
	public PsGameModeRacing(PsGameLoop _info)
		: base(PsGameMode.Race, _info)
	{
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00058802 File Offset: 0x00056C02
	public override void EnterMinigame()
	{
		this.ShowStartUI();
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0005880A File Offset: 0x00056C0A
	public void CalculateMedalTimes()
	{
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0005880C File Offset: 0x00056C0C
	public override void CreateMusic()
	{
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		if (Random.Range(0, 2) < 1)
		{
			minigame.m_music = SoundS.AddComponent(minigame.m_environmentTC, PsFMODManager.GetMusic("RacingMusic1"), 1f, true);
		}
		else
		{
			minigame.m_music = SoundS.AddComponent(minigame.m_environmentTC, "/Music/RacingMusic2", 1f, true);
		}
		SoundS.PlaySound(minigame.m_music, true);
		SoundS.SetSoundParameter(minigame.m_music, "MusicState", 0f);
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00058894 File Offset: 0x00056C94
	public override void ApplySettings(bool _createAreaLimits = true, bool _updateDomeGfx = false)
	{
		base.ApplySettings(_createAreaLimits, _updateDomeGfx);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x000588A0 File Offset: 0x00056CA0
	public override void LoadGhosts(Action<GhostData[]> _customSUCCESSCallback = null)
	{
		this.ServerGetRacingGhosts(new GhostGetData
		{
			minigameInfo = PsState.m_activeGameLoop,
			times = this.GetGhostTimes(),
			ghostIds = (this.m_gameLoop as PsGameLoopRacing).m_trophyGhostIds
		}, _customSUCCESSCallback);
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x000588EC File Offset: 0x00056CEC
	public override void GhostsLoaded(GhostData[] _data)
	{
		(this.m_gameLoop as PsGameLoopRacing).m_trophyGhostIds = _data[0].ghostId;
		if (_data.Length == 1)
		{
			_data[0].rival = true;
			(this.m_gameLoop as PsGameLoopRacing).m_rivalPos = 1;
		}
		this.m_trophyGhost = _data[0];
		if (_data.Length > 1)
		{
			PsGameLoopRacing psGameLoopRacing = this.m_gameLoop as PsGameLoopRacing;
			psGameLoopRacing.m_trophyGhostIds = psGameLoopRacing.m_trophyGhostIds + "," + _data[1].ghostId;
			_data[1].rival = true;
			(this.m_gameLoop as PsGameLoopRacing).m_rivalPos = 2;
			this.m_diamondGhost = _data[1];
			this.m_diamondWinTime = HighScores.TimeScoreToTime(this.m_diamondGhost.time);
		}
		if (_data.Length > 2)
		{
			PsGameLoopRacing psGameLoopRacing2 = this.m_gameLoop as PsGameLoopRacing;
			psGameLoopRacing2.m_trophyGhostIds = psGameLoopRacing2.m_trophyGhostIds + "," + _data[2].ghostId;
			this.m_coinGhost = _data[2];
			this.m_coinWinTime = HighScores.TimeScoreToTime(this.m_coinGhost.time);
		}
		(this.m_gameLoop as PsGameLoopRacing).m_raceGhostCount = _data.Length;
		this.m_allGhosts = new List<GhostData>(_data);
		if (this.m_gameLoop.m_forcedMedalTimes != null && this.m_gameLoop.m_forcedMedalTimes.Length == 3 && string.IsNullOrEmpty((this.m_gameLoop as PsGameLoopRacing).m_fixedTrophies) && !string.IsNullOrEmpty(this.m_gameLoop.m_forcedMedalTimes[0]) && !string.IsNullOrEmpty(this.m_gameLoop.m_forcedMedalTimes[1]) && !string.IsNullOrEmpty(this.m_gameLoop.m_forcedMedalTimes[2]))
		{
			string text = string.Empty;
			for (int i = 0; i < this.m_allGhosts.Count; i++)
			{
				int num = PsMetagameManager.m_playerStats.trophies;
				Random.seed = (int)(Time.realtimeSinceStartup * (float)Random.Range(15000, 22000));
				if (i == 0)
				{
					num += Random.Range(60, 110);
				}
				else if (i == 1)
				{
					num = Mathf.Max(0, num + Random.Range(13, 25));
				}
				else if (i == 2)
				{
					num = Mathf.Max(0, num + Random.Range(-50, -10));
				}
				text += num.ToString();
				if (i < this.m_allGhosts.Count)
				{
					text += ',';
				}
			}
			(this.m_gameLoop as PsGameLoopRacing).m_fixedTrophies = text;
		}
		base.GhostsLoaded(_data);
		this.m_trophyWinTime = HighScores.TimeScoreToTime(this.m_trophyGhost.time);
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00058B90 File Offset: 0x00056F90
	public override void CreatePlaybackGhostVisuals()
	{
		this.m_ignoredIndices.Clear();
		if ((this.m_gameLoop as PsGameLoopRacing).m_secondarysWon >= 4)
		{
			this.m_ignoredIndices.Add(0);
		}
		if ((this.m_gameLoop as PsGameLoopRacing).m_secondarysWon >= 2)
		{
			this.m_ignoredIndices.Add(1);
		}
		if ((this.m_gameLoop as PsGameLoopRacing).m_secondarysWon >= 1)
		{
			this.m_ignoredIndices.Add(2);
		}
		for (int i = 0; i < this.m_playbackGhosts.Count; i++)
		{
			this.m_playbackGhosts[i].DestroyVisuals();
			if (!this.m_ignoredIndices.Contains(i))
			{
				this.m_playbackGhosts[i].m_ghost.m_playbackTick = 0f;
				this.m_playbackGhosts[i].m_ghost.ResetEvents();
				string text = this.m_playbackGhosts[i].m_ghost.m_unitClass;
				if (string.IsNullOrEmpty(text))
				{
					text = PsState.m_activeGameLoop.m_minigameMetaData.playerUnit;
				}
				bool flag = false;
				if (this.m_playbackGhosts.Count == this.m_ignoredIndices.Count + 1)
				{
					flag = true;
				}
				if (text == "OffroadCar")
				{
					this.CreateOffroadCarPlaybackGhostEntity(this.m_playbackGhosts[i], flag, string.Empty);
				}
				else if (text == "Motorcycle")
				{
					this.CreateMotorcyclePlaybackGhostEntity(this.m_playbackGhosts[i], flag, string.Empty);
				}
				Debug.LogWarning("CREATED PLAYBACK GHOST VISUALS: " + this.m_playbackGhosts[i].m_name);
			}
		}
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00058D4C File Offset: 0x0005714C
	public int[] GetGhostTimes()
	{
		return new int[] { int.MaxValue, int.MaxValue, int.MaxValue };
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00058D7C File Offset: 0x0005717C
	public override void UpdateGhosts(bool _updateLogic)
	{
		if (!(this.m_gameLoop as PsGameLoopRacing).m_practiceRun)
		{
			if (_updateLogic && this.m_recordingGhost != null)
			{
				this.m_recordingGhost.UpdateRecording(false);
			}
			for (int i = 0; i < this.m_playbackGhosts.Count; i++)
			{
				if (!this.m_ignoredIndices.Contains(i))
				{
					this.m_playbackGhosts[i].Update(_updateLogic);
				}
			}
		}
		else
		{
			for (int j = 0; j < this.m_playbackGhosts.Count; j++)
			{
				if (!this.m_ignoredIndices.Contains(j))
				{
					this.m_playbackGhosts[j].Update(false);
				}
			}
		}
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00058E40 File Offset: 0x00057240
	public override void LoseMinigame()
	{
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = int.MaxValue;
		PsGameLoopRacing psGameLoopRacing = PsState.m_activeGameLoop as PsGameLoopRacing;
		if (psGameLoopRacing.m_heatNumber - psGameLoopRacing.m_purchasedRuns < 6 || psGameLoopRacing.m_trophiesRewarded || this.m_gameLoop.m_nodeId == this.m_gameLoop.m_path.m_currentNodeId)
		{
		}
		base.ShowLoseUI();
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00058EB4 File Offset: 0x000572B4
	private void ShowStartUI()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterStartRacing), typeof(PsUITopStartRacing), null, null, false, true, InitialPage.Center, false, false, false);
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).InitOpponents();
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Shop", delegate
		{
			this.ShowRunsShop(new Action(this.ShowStartUI), new Action(this.m_gameLoop.BeginHeat));
		});
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action((this.m_gameLoop as PsGameLoopRacing).BeginHeat));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.ConfirmForfeit));
		this.ShowStartResources(true);
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00058FA8 File Offset: 0x000573A8
	private void ShowRunsShop(Action _exitAction, Action _purchaseAction)
	{
		(this.m_gameLoop as PsGameLoopRacing).m_startPosition = (this.m_gameLoop as PsGameLoopRacing).GetPosition();
		PsIngameMenu.CloseAll();
		PsMetrics.RaceTriesOffered();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterShopRuns), null, null, null, true, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Purchased", delegate
		{
			PsIngameMenu.CloseAll();
			_purchaseAction.Invoke();
		});
		PsIngameMenu.m_popupMenu.SetAction("Exit", delegate
		{
			PsIngameMenu.CloseAll();
			_exitAction.Invoke();
		});
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00059048 File Offset: 0x00057448
	protected override void ShowLoseUI2()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterLoseRacing), typeof(PsUITopStartRacing), null, null, false, true, InitialPage.Center, false, false, false);
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).InitOpponents();
		PsIngameMenu.m_popupMenu.SetAction("Shop", delegate
		{
			this.ShowRunsShop(new Action(this.ShowLoseUI2), new Action(this.m_gameLoop.RestartMinigame));
		});
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.ConfirmForfeit));
		this.ShowStartResources(true);
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00059134 File Offset: 0x00057534
	public void SendLose()
	{
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = int.MaxValue;
		bool flag = true;
		if ((PsState.m_activeGameLoop as PsGameLoopRacing).m_heatNumber >= 6 + (PsState.m_activeGameLoop as PsGameLoopRacing).m_purchasedRuns && !(PsState.m_activeGameLoop as PsGameLoopRacing).m_trophiesRewarded && this.m_gameLoop.m_nodeId == this.m_gameLoop.m_path.m_currentNodeId)
		{
			flag = false;
			this.SendRaceEndData(true);
		}
		(this.m_gameLoop as PsGameLoopRacing).SaveBeforeExit(flag);
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x000591D0 File Offset: 0x000575D0
	public void SendLoseAndTweenUI()
	{
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).RemoveButtons();
		PsUITopStartRacing psUITopStartRacing = PsIngameMenu.m_popupMenu.m_topContent as PsUITopStartRacing;
		if (psUITopStartRacing != null)
		{
			psUITopStartRacing.RemoveButton();
		}
		PsMetagameManager.HideResources();
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = int.MaxValue;
		this.SendRaceEndData(true);
		((PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).m_opponents as PsUIWinOpponents).m_nextRacePressed = true;
		((PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).m_opponents as PsUIWinOpponents).m_nextRaceAction = delegate
		{
			(this.m_gameLoop as PsGameLoopRacing).SaveBeforeExit(false);
		};
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x0005927C File Offset: 0x0005767C
	public void ShowStartResources(bool _diamonds = true)
	{
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, true, true, _diamonds, false, 0.03f, true, false, false);
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x000592A4 File Offset: 0x000576A4
	public override void StartMinigame()
	{
		MotorcycleController.m_startCreate = false;
		this.m_finalSend = false;
		PsIngameMenu.CloseAll();
		PsMetagameManager.HideResources();
		this.CreatePlayMenu(new Action(this.m_gameLoop.SelfDestructPlayer), new Action(this.m_gameLoop.PauseMinigame));
		if (PsState.m_activeMinigame.m_music != null)
		{
			SoundS.SetSoundParameter(PsState.m_activeMinigame.m_music, "MusicState", 1f);
		}
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0005931A File Offset: 0x0005771A
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUITopPlayRacing(_restartAction, _pauseAction);
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00059334 File Offset: 0x00057734
	public override void WinMinigame()
	{
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = HighScores.TicksToTimeScore(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks), true);
		float num = HighScores.TimeScoreToTime(this.m_gameLoop.m_timeScoreCurrent);
		int num2 = 0;
		bool ghostWon = (this.m_gameLoop as PsGameLoopRacing).m_ghostWon;
		if (!(this.m_gameLoop as PsGameLoopRacing).m_ghostWon && num <= this.m_trophyWinTime && this.m_gameLoop.m_nodeId == this.m_gameLoop.m_path.m_currentNodeId && !(this.m_gameLoop as PsGameLoopRacing).m_trophiesRewarded && (this.m_gameLoop as PsGameLoopRacing).m_heatNumber <= 6 + (this.m_gameLoop as PsGameLoopRacing).m_purchasedRuns)
		{
			Debug.LogError("Won ghost!");
			PsAchievementManager.IncrementProgress("gainFiftyGoldMedals", 1);
			num2 = 3;
			this.m_rewardTrophies = true;
			(PsState.m_activeGameLoop as PsGameLoopRacing).m_ghostWon = true;
		}
		int num3 = (this.m_gameLoop as PsGameLoopRacing).m_secondarysWon;
		bool flag = num3 < 4;
		bool flag2 = num3 == 2 || num3 == 6 || num3 == 0;
		bool flag3 = num3 == 1 || num3 == 5 || num3 == 0;
		bool flag4 = flag2 || flag3 || flag;
		if (!(this.m_gameLoop as PsGameLoopRacing).m_heatLost && num <= this.m_coinWinTime && flag2)
		{
			num3++;
		}
		if (!(this.m_gameLoop as PsGameLoopRacing).m_heatLost && num <= this.m_diamondWinTime && flag3)
		{
			num3 += 2;
			if (this.m_diamondGhost.rival)
			{
				int vehicleIndex = PsState.GetVehicleIndex();
				if (PsMetagameManager.m_vehicleGachaData.m_rivalWonCount < 4)
				{
					PsMetagameManager.m_vehicleGachaData.m_rivalWonCount = Mathf.Min(PsMetagameManager.m_vehicleGachaData.m_rivalWonCount + 1, 4);
					PsMetagameManager.SendCurrentGachaData(true);
				}
				(this.m_gameLoop as PsGameLoopRacing).m_rivalWon = true;
			}
		}
		if (!(this.m_gameLoop as PsGameLoopRacing).m_heatLost && num <= this.m_trophyWinTime && flag)
		{
			num3 += 4;
			if (this.m_trophyGhost.rival)
			{
				int vehicleIndex2 = PsState.GetVehicleIndex();
				if (PsMetagameManager.m_vehicleGachaData.m_rivalWonCount < 4)
				{
					PsMetagameManager.m_vehicleGachaData.m_rivalWonCount = Mathf.Min(PsMetagameManager.m_vehicleGachaData.m_rivalWonCount + 1, 4);
					PsMetagameManager.SendCurrentGachaData(true);
				}
				(this.m_gameLoop as PsGameLoopRacing).m_rivalWon = true;
			}
		}
		PsGameLoopRacing psGameLoopRacing = this.m_gameLoop as PsGameLoopRacing;
		this.m_saveGhost = this.m_gameLoop.m_nodeId == this.m_gameLoop.m_path.m_currentNodeId && psGameLoopRacing.m_heatNumber <= psGameLoopRacing.m_purchasedRuns + 5 + 1 && !psGameLoopRacing.m_trophiesRewarded && (psGameLoopRacing.m_secondarysWon != num3 || this.m_gameLoop.m_timeScoreBest == int.MaxValue);
		bool flag5 = this.m_gameLoop.m_nodeId == this.m_gameLoop.m_path.m_currentNodeId && (psGameLoopRacing.m_secondarysWon != num3 || this.m_gameLoop.m_timeScoreBest == int.MaxValue) && !psGameLoopRacing.m_trophiesRewarded;
		if ((this.m_gameLoop as PsGameLoopRacing).m_secondarysWon == num3)
		{
		}
		(this.m_gameLoop as PsGameLoopRacing).m_secondarysWon = num3;
		if (this.m_gameLoop.m_timeScoreCurrent < this.m_gameLoop.m_timeScoreBest && !(this.m_gameLoop as PsGameLoopRacing).m_heatLost)
		{
			if (flag5 && !this.m_saveGhost)
			{
				this.m_saveGhost = true;
			}
			this.m_gameLoop.m_timeScoreBest = this.m_gameLoop.m_timeScoreCurrent;
			this.m_gameLoop.m_minigameMetaData.timeScore = this.m_gameLoop.m_timeScoreBest;
			PsMetricsData.m_isSendingGhost = true;
			PsMetricsData.m_isNewGhost = true;
		}
		this.m_gameLoop.m_currentRunScore = num2;
		if (num2 > this.m_gameLoop.m_scoreCurrent)
		{
			this.m_gameLoop.m_scoreCurrent = num2;
			if (this.m_gameLoop.m_scoreCurrent > this.m_gameLoop.m_scoreBest)
			{
				this.m_gameLoop.m_scoreBest = this.m_gameLoop.m_scoreCurrent;
				this.m_gameLoop.m_minigameMetaData.score = this.m_gameLoop.m_scoreBest;
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
		if (flag5)
		{
			this.SendRaceEndData(false);
		}
		this.CreateWinUI();
		if (!flag5)
		{
			if (this.m_gameLoop.m_path.GetPathType() == PsPlanetPathType.main)
			{
				Hashtable hashtable = ClientTools.GenerateProgressionPathJson(this.m_gameLoop, this.m_gameLoop.m_path.m_currentNodeId, true, true, true);
				PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable, this.m_gameLoop.m_path.m_planet, false);
			}
			else
			{
				Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_gameLoop.m_path);
				PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable2, this.m_gameLoop.m_path.m_planet, false);
			}
		}
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x000598F0 File Offset: 0x00057CF0
	private void ConfirmForfeit()
	{
		PsGameLoopRacing psGameLoopRacing = this.m_gameLoop as PsGameLoopRacing;
		if (psGameLoopRacing.m_heatNumber - psGameLoopRacing.m_purchasedRuns <= 5 && this.m_gameLoop == this.m_gameLoop.m_path.GetCurrentNodeInfo() && (this.m_gameLoop as PsGameLoopRacing).m_secondarysWon < 4 && !(this.m_gameLoop as PsGameLoopRacing).m_trophiesRewarded)
		{
			PsUIBasePopup forfeitpopup = new PsUIBasePopup(typeof(PsUIRacingForfeitPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
			forfeitpopup.SetAction("Forfeit", delegate
			{
				forfeitpopup.Destroy();
				this.SendLoseAndTweenUI();
			});
			forfeitpopup.SetAction("Exit", delegate
			{
				forfeitpopup.Destroy();
			});
		}
		else if (!(this.m_gameLoop as PsGameLoopRacing).m_trophiesRewarded)
		{
			this.SendLoseAndTweenUI();
		}
		else
		{
			this.SendLose();
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x000599F0 File Offset: 0x00057DF0
	protected override void CreateWinUI()
	{
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterWinRacing), typeof(PsUITopWinRacing), null, null, false, true, InitialPage.Center, false, false, false);
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterWinRacing).InitOpponents();
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.ConfirmForfeit));
		PsIngameMenu.m_popupMenu.SetAction("Shop", delegate
		{
			this.ShowRunsShop(new Action(this.CreateWinUI), new Action(this.m_gameLoop.RestartMinigame));
		});
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, false, false, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x00059ACC File Offset: 0x00057ECC
	public override void PauseMinigame()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterPauseRace), typeof(PsUITopPauseRace), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", new Action((this.m_gameLoop as PsGameLoopRacing).ResumeSelfDestruct));
		PsIngameMenu.m_popupMenu.SetAction("Resume", new Action(this.m_gameLoop.ResumeMinigame));
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00059B90 File Offset: 0x00057F90
	private void SendRaceEndData(bool _lost = false)
	{
		this.m_waitForHighscoreAndGhost = true;
		this.m_waitForNextGhost = false;
		DataBlob dataBlob = default(DataBlob);
		if (this.m_saveGhost)
		{
			Debug.LogWarning("SAVE RACING GHOST");
			if (this.m_recordingGhost != null && !this.m_recordingGhost.m_recording)
			{
				dataBlob = ClientTools.CreateGhostDataBlob(this.m_recordingGhost);
			}
			if (dataBlob.data != null)
			{
				PsMetricsData.m_lastGhostSize = dataBlob.data.Length;
			}
		}
		if (!(PsState.m_activeGameLoop as PsGameLoopRacing).m_trophiesRewarded && ((PsState.m_activeGameLoop as PsGameLoopRacing).m_secondarysWon >= 4 || _lost))
		{
			if (_lost)
			{
				this.m_saveNextNode = true;
			}
			this.m_finalSend = true;
			int num = 0;
			for (int i = 0; i < this.m_allGhosts.Count; i++)
			{
				if (i < (this.m_gameLoop as PsGameLoopRacing).GetPosition() - 1)
				{
					num += this.m_allGhosts[i].trophyLoss;
				}
				else
				{
					num += this.m_allGhosts[i].trophyWin;
				}
			}
			PsMainMenuState.SetCurrentTrophies(PsMetagameManager.m_playerStats.trophies);
			(this.m_gameLoop as PsGameLoopRacing).m_rewardTrophyAmount = num;
			if (num < 0 && PsMetagameManager.m_playerStats.trophies < Mathf.Abs(num))
			{
				(this.m_gameLoop as PsGameLoopRacing).m_rewardTrophyAmount = -PsMetagameManager.m_playerStats.trophies;
			}
			(this.m_gameLoop as PsGameLoopRacing).m_trophiesRewarded = true;
			PsMetagameManager.m_playerStats.trophies = Math.Max(0, PsMetagameManager.m_playerStats.trophies + (this.m_gameLoop as PsGameLoopRacing).m_rewardTrophyAmount);
			Debug.LogWarning("TROPHIES REWARDED");
		}
		Debug.LogWarning("LAST SENT GHOST SIZE: " + PsMetricsData.m_lastGhostSize);
		this.SendRacingEndData(dataBlob);
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00059D6C File Offset: 0x0005816C
	protected void NewGhostLoaded()
	{
		this.m_waitForNextGhost = false;
		Debug.LogWarning("NEW GHOST LOADED (not really...)");
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x00059D80 File Offset: 0x00058180
	public void SendRacingEndData(DataBlob _blob)
	{
		RacingGhostData data = new RacingGhostData(PsState.m_activeGameLoop.m_minigameId, null, this.m_gameLoop.m_timeScoreCurrent, base.GetUpgradeSum(), PsState.m_activeMinigame.m_playerUnitName, _blob, this.m_finalSend);
		OpponentData[] opponents = null;
		if (this.m_finalSend)
		{
			opponents = new OpponentData[this.m_allGhosts.Count];
			for (int i = 0; i < opponents.Length; i++)
			{
				bool flag = (this.m_gameLoop as PsGameLoopRacing).GetPosition() - 1 <= i;
				int num = ((!flag) ? this.m_allGhosts[i].ghostWin : this.m_allGhosts[i].ghostLoss);
				opponents[i] = new OpponentData(this.m_allGhosts[i].ghostId, num);
			}
		}
		if (_blob.data != null)
		{
			PsMetricsData.m_lastGhostSize = _blob.data.Length;
		}
		string planetIdentifier = this.m_gameLoop.m_path.m_planet;
		new PsServerQueueFlow(null, delegate
		{
			this.SendGhostData(data, opponents, planetIdentifier, false);
		}, new string[] { "SetData" });
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00059ECC File Offset: 0x000582CC
	public HttpC SendGhostData(RacingGhostData _data, OpponentData[] _opponents, string _planetIdentifier, bool _saveProgressionMetrics = false)
	{
		if (this.m_saveNextNode && this.m_gameLoop.m_path.m_currentNodeId == this.m_gameLoop.m_nodeId)
		{
			this.m_gameLoop.m_path.m_currentNodeId = this.m_gameLoop.m_nodeId + 1;
			_saveProgressionMetrics = true;
		}
		Hashtable hashtable;
		if (this.m_gameLoop.m_path.GetPathType() == PsPlanetPathType.main)
		{
			hashtable = ClientTools.GenerateProgressionPathJson(this.m_gameLoop, this.m_gameLoop.m_path.m_currentNodeId, true, true, true);
		}
		else
		{
			hashtable = ClientTools.GenerateProgressionPathJson(this.m_gameLoop.m_path);
		}
		return Trophy.SendScore(_data, _opponents, this.m_gameLoop.m_timeScoreBest != int.MaxValue, _data.m_final, hashtable, delegate(HttpC c)
		{
			this.EndDataSendOK(c, _planetIdentifier, _saveProgressionMetrics);
		}, delegate(HttpC c)
		{
			this.EndDataSendFAILED(c, _data, _opponents, _planetIdentifier, _saveProgressionMetrics);
		}, null);
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00059FEC File Offset: 0x000583EC
	public void EndDataSendOK(HttpC _c, string _planetIdentifier, bool _saveProgressionMetrics)
	{
		if (PsState.m_activeGameLoop.m_scoreBest >= 3)
		{
			PsMetagameManager.m_playerStats.racesCompleted++;
			PsMetagameManager.m_playerStats.racesThisSeason++;
		}
		Debug.LogWarning("RACING GHOSTS SENT");
		this.m_waitForHighscoreAndGhost = false;
		this.m_waitForNextGhost = false;
		if (_saveProgressionMetrics)
		{
			PsMetrics.Progression(PlanetTools.m_planetProgressionInfos[_planetIdentifier].GetMainPath().m_currentNodeId, _planetIdentifier);
		}
		PsPlanetSerializer.SaveProgressionLocally(_planetIdentifier);
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		SeasonEndData seasonEndData = ClientTools.ParseSeasonEndData(dictionary);
		if (seasonEndData != null)
		{
			PsMetagameManager.SetSeasonEndData(seasonEndData);
		}
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0005A090 File Offset: 0x00058490
	public void EndDataSendFAILED(HttpC _c, RacingGhostData _data, OpponentData[] _opponents, string _planetIdentifier, bool _saveProgressionMetrics)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => this.SendGhostData(_data, _opponents, _planetIdentifier, _saveProgressionMetrics), null);
	}

	// Token: 0x040007FE RID: 2046
	public float m_trophyWinTime;

	// Token: 0x040007FF RID: 2047
	public float m_diamondWinTime;

	// Token: 0x04000800 RID: 2048
	public float m_coinWinTime;

	// Token: 0x04000801 RID: 2049
	public bool m_giveThreeStars;

	// Token: 0x04000802 RID: 2050
	public bool m_saveGhost;

	// Token: 0x04000803 RID: 2051
	public GhostData m_trophyGhost;

	// Token: 0x04000804 RID: 2052
	public GhostData m_coinGhost;

	// Token: 0x04000805 RID: 2053
	public GhostData m_diamondGhost;

	// Token: 0x04000806 RID: 2054
	public bool m_rewardTrophies;

	// Token: 0x04000807 RID: 2055
	public bool m_rewardDiamonds;

	// Token: 0x04000808 RID: 2056
	public bool m_rewardCoins;

	// Token: 0x04000809 RID: 2057
	public bool m_rewardSecondaryTrophies;

	// Token: 0x0400080A RID: 2058
	private List<int> m_ignoredIndices = new List<int>();

	// Token: 0x0400080B RID: 2059
	private bool m_finalSend;

	// Token: 0x0400080C RID: 2060
	private bool m_saveNextNode;
}
