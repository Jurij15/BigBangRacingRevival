using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011E RID: 286
public class PsGameModeRaceFresh : PsGameModeRace
{
	// Token: 0x06000812 RID: 2066 RVA: 0x000573AB File Offset: 0x000557AB
	protected PsGameModeRaceFresh(PsGameMode _gameMode, PsGameLoop _info)
		: base(_info)
	{
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x000573BF File Offset: 0x000557BF
	public PsGameModeRaceFresh(PsGameLoop _info)
		: base(_info)
	{
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x000573D3 File Offset: 0x000557D3
	public override void EnterMinigame()
	{
		this.ShowStartUI();
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x000573DC File Offset: 0x000557DC
	private void ShowStartUI()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterStartRacingFresh), typeof(PsUITopStartRacing), null, null, false, true, InitialPage.Center, false, false, false);
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).InitOpponents();
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.BeginHeat));
		this.ShowStartResources(true);
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x000574B4 File Offset: 0x000558B4
	protected override void ShowLoseUI2()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterStartRacingFresh), typeof(PsUITopStartRacing), null, null, false, true, InitialPage.Center, false, false, false);
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).InitOpponents();
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		this.ShowStartResources(true);
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x0005758C File Offset: 0x0005598C
	public void ShowStartResources(bool _diamonds = true)
	{
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, true, false, _diamonds, false, 0.03f, true, false, true);
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x000575B4 File Offset: 0x000559B4
	public override void LoseMinigame()
	{
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = int.MaxValue;
		base.ShowLoseUI();
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x000575D4 File Offset: 0x000559D4
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

	// Token: 0x0600081A RID: 2074 RVA: 0x0005764A File Offset: 0x00055A4A
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUITopPlayRacingFresh(_restartAction, _pauseAction);
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00057664 File Offset: 0x00055A64
	public override void LoadGhosts(Action<GhostData[]> _customSUCCESSCallback = null)
	{
		this.ServerGetGhosts(new GhostGetData
		{
			minigameInfo = PsState.m_activeGameLoop,
			times = base.GetGhostTimes()
		}, _customSUCCESSCallback);
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00057698 File Offset: 0x00055A98
	public override void GhostsLoaded(GhostData[] _data)
	{
		this.m_trophyGhost = _data[0];
		for (int i = 0; i < _data.Length; i++)
		{
			_data[i].trophyLoss = 0;
			_data[i].rival = false;
		}
		if (_data.Length == 1)
		{
			_data[0].trophyWin = 3;
		}
		else if (_data.Length == 2)
		{
			_data[0].trophyWin = 2;
			_data[1].trophyWin = 1;
		}
		else if (_data.Length == 3)
		{
			_data[0].trophyWin = 1;
			_data[1].trophyWin = 1;
			_data[2].trophyWin = 1;
		}
		if (_data.Length == 1)
		{
		}
		if (_data.Length > 1)
		{
			this.m_diamondGhost = _data[1];
			this.m_diamondWinTime = HighScores.TimeScoreToTime(this.m_diamondGhost.time);
		}
		if (_data.Length > 2)
		{
			this.m_coinGhost = _data[2];
			this.m_coinWinTime = HighScores.TimeScoreToTime(this.m_coinGhost.time);
		}
		(this.m_gameLoop as PsGameLoopFresh).m_raceGhostCount = _data.Length;
		this.m_allGhosts = new List<GhostData>(_data);
		base.GhostsLoaded(_data);
		this.m_trophyWinTime = HighScores.TimeScoreToTime(this.m_trophyGhost.time);
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x000577C4 File Offset: 0x00055BC4
	public override void WinMinigame()
	{
		this.m_diamondChange = 0;
		this.m_gameLoop.m_timeScoreCurrent = HighScores.TicksToTimeScore(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks), true);
		int num = (this.m_gameLoop as PsGameLoopFresh).m_secondarysWon;
		bool flag = num < 4;
		bool flag2 = num == 2 || num == 6 || num == 0;
		bool flag3 = num == 1 || num == 5 || num == 0;
		bool flag4 = flag2 || flag3 || flag;
		float num2 = HighScores.TimeScoreToTime(this.m_gameLoop.m_timeScoreCurrent);
		this.m_oldShards = PsMetagameManager.m_playerStats.shards;
		int num3 = 0;
		if (num2 <= this.m_coinWinTime && flag2 && this.m_rewardedDiamonds < 1 && !this.m_rewardCoins)
		{
			num++;
			int num4 = 1 - this.m_rewardedDiamonds;
			if (num4 > 0)
			{
				Debug.Log("Fresh: Giving: " + num4, null);
				num3 += num4 * PsState.m_bigShardValue;
			}
			this.m_rewardedDiamonds = 1;
			this.m_rewardCoins = true;
		}
		if (num2 <= this.m_diamondWinTime && flag3 && this.m_rewardedDiamonds < 2 && !this.m_rewardDiamonds)
		{
			num += 2;
			int num5 = 2 - this.m_rewardedDiamonds;
			if (num5 > 0)
			{
				Debug.Log("Fresh: Giving: " + num5, null);
				num3 += num5 * PsState.m_bigShardValue;
			}
			this.m_rewardedDiamonds = 2;
			this.m_rewardDiamonds = true;
		}
		if (num2 <= this.m_trophyWinTime && flag && this.m_rewardedDiamonds < 3 && !this.m_rewardTrophies)
		{
			num += 4;
			int num6 = 3 - this.m_rewardedDiamonds;
			if (num6 > 0)
			{
				Debug.Log("Fresh: Giving: " + num6, null);
				num3 += num6 * PsState.m_bigShardValue;
			}
			this.m_rewardedDiamonds = 3;
			this.m_rewardTrophies = true;
		}
		PsMetagameManager.m_playerStats.shards += num3;
		if (PsState.m_activeMinigame != null)
		{
			Minigame activeMinigame = PsState.m_activeMinigame;
			activeMinigame.m_collectedShards += num3;
		}
		if (PsMetagameManager.m_playerStats.shards > 99)
		{
			PsMetagameManager.m_playerStats.shards -= 100;
			PsMetagameManager.m_playerStats.shardReset = true;
			if (PsState.m_activeMinigame != null)
			{
				PsState.m_activeMinigame.m_collectedShards = PsMetagameManager.m_playerStats.shards;
			}
			this.m_diamondChange = 1;
			PsMetagameManager.m_playerStats.diamonds++;
		}
		(this.m_gameLoop as PsGameLoopFresh).m_secondarysWon = num;
		if (this.m_gameLoop.m_timeScoreCurrent < this.m_gameLoop.m_timeScoreBest)
		{
			this.m_gameLoop.m_timeScoreBest = this.m_gameLoop.m_timeScoreCurrent;
			this.m_saveGhost = true;
		}
		base.KeepBestGhost();
		this.CreateWinUI();
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00057AD0 File Offset: 0x00055ED0
	protected override void CreateWinUI()
	{
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterWinRacingFresh), typeof(PsUITopWinRacingSocial), null, null, false, true, InitialPage.Center, false, false, false);
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterWinRacing).InitOpponents();
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, false, false, true, false, 0.03f, false, false, true);
		PsMetagameManager.m_playerStats.updated = false;
		PsMetagameManager.m_menuResourceView.m_shards.SetText(this.m_oldShards.ToString());
		PsMetagameManager.m_menuResourceView.m_diamonds.SetText((PsMetagameManager.m_playerStats.diamonds - this.m_diamondChange).ToString());
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00057BF0 File Offset: 0x00055FF0
	public void SendLoseAndTweenUI()
	{
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacingFresh).RemoveButtons();
		PsUITopStartRacing psUITopStartRacing = PsIngameMenu.m_popupMenu.m_topContent as PsUITopStartRacing;
		if (psUITopStartRacing != null)
		{
			psUITopStartRacing.RemoveButton();
		}
		PsMetagameManager.HideResources();
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = int.MaxValue;
		this.SendRaceEndData();
		((PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).m_opponents as PsUIWinOpponents).m_nextRacePressed = true;
		((PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).m_opponents as PsUIWinOpponents).m_nextRaceAction = delegate
		{
			(this.m_gameLoop as PsGameLoopRacing).SaveBeforeExit(false);
		};
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00057C98 File Offset: 0x00056098
	private void SendRaceEndData()
	{
		this.m_waitForHighscoreAndGhost = true;
		this.m_waitForNextGhost = false;
		DataBlob bestGhost = this.m_bestGhost;
		int position = (PsState.m_activeGameLoop as PsGameLoopFresh).GetPosition();
		if (position != 1)
		{
			if (position != 2)
			{
				if (position == 3)
				{
				}
			}
		}
		Debug.LogWarning("LAST SENT GHOST SIZE: " + PsMetricsData.m_lastGhostSize);
		base.SendRacingEndData(bestGhost);
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x00057D14 File Offset: 0x00056114
	public override void PauseMinigame()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterPauseRace), typeof(PsUITopPauseFresh), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", new Action(this.m_gameLoop.ResumeSelfDestruct));
		PsIngameMenu.m_popupMenu.SetAction("Resume", new Action(this.m_gameLoop.ResumeMinigame));
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x00057DB0 File Offset: 0x000561B0
	public override void CreatePlaybackGhostVisuals()
	{
		this.m_ignoredIndices.Clear();
		if ((this.m_gameLoop as PsGameLoopFresh).m_secondarysWon >= 4)
		{
			this.m_ignoredIndices.Add(0);
		}
		if ((this.m_gameLoop as PsGameLoopFresh).m_secondarysWon >= 2)
		{
			this.m_ignoredIndices.Add(1);
		}
		if ((this.m_gameLoop as PsGameLoopFresh).m_secondarysWon >= 1)
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
				if (text == "OffroadCar")
				{
					this.CreateOffroadCarPlaybackGhostEntity(this.m_playbackGhosts[i], false, string.Empty);
				}
				else if (text == "Motorcycle")
				{
					this.CreateMotorcyclePlaybackGhostEntity(this.m_playbackGhosts[i], false, string.Empty);
				}
				Debug.LogWarning("CREATED PLAYBACK GHOST VISUALS: " + this.m_playbackGhosts[i].m_name);
			}
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00057F4C File Offset: 0x0005634C
	public override void UpdateGhosts(bool _updateLogic)
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

	// Token: 0x040007E0 RID: 2016
	public int m_realReward;

	// Token: 0x040007E1 RID: 2017
	public float m_trophyWinTime;

	// Token: 0x040007E2 RID: 2018
	public float m_diamondWinTime;

	// Token: 0x040007E3 RID: 2019
	public float m_coinWinTime;

	// Token: 0x040007E4 RID: 2020
	public new bool m_giveThreeStars;

	// Token: 0x040007E5 RID: 2021
	public GhostData m_trophyGhost;

	// Token: 0x040007E6 RID: 2022
	public GhostData m_coinGhost;

	// Token: 0x040007E7 RID: 2023
	public GhostData m_diamondGhost;

	// Token: 0x040007E8 RID: 2024
	public int m_rewardedDiamonds;

	// Token: 0x040007E9 RID: 2025
	public bool m_rewardTrophies;

	// Token: 0x040007EA RID: 2026
	public bool m_rewardDiamonds;

	// Token: 0x040007EB RID: 2027
	public bool m_rewardCoins;

	// Token: 0x040007EC RID: 2028
	public bool m_rewardSecondaryTrophies;

	// Token: 0x040007ED RID: 2029
	public int m_oldShards;

	// Token: 0x040007EE RID: 2030
	public int m_diamondChange;

	// Token: 0x040007EF RID: 2031
	private List<int> m_ignoredIndices = new List<int>();
}
