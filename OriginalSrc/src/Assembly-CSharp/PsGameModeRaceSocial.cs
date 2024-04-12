using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class PsGameModeRaceSocial : PsGameModeRace
{
	// Token: 0x06000825 RID: 2085 RVA: 0x00057FC8 File Offset: 0x000563C8
	public PsGameModeRaceSocial(PsGameLoop _info)
		: base(_info)
	{
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x00057FDC File Offset: 0x000563DC
	public override void EnterMinigame()
	{
		this.ShowStartUI();
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00057FE4 File Offset: 0x000563E4
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

	// Token: 0x06000828 RID: 2088 RVA: 0x0005805A File Offset: 0x0005645A
	public override void LoseMinigame()
	{
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = int.MaxValue;
		base.ShowLoseUI();
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x00058079 File Offset: 0x00056479
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUITopPlayRacing(_restartAction, _pauseAction);
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x00058094 File Offset: 0x00056494
	private void ShowStartUI()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterStartRacingSocial), typeof(PsUITopStartRacing), null, null, false, true, InitialPage.Center, false, false, false);
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).InitOpponents();
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.BeginHeat));
		this.ShowStartResources(true);
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x0005816C File Offset: 0x0005656C
	protected override void ShowLoseUI2()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterStartRacingSocial), typeof(PsUITopStartRacing), null, null, false, true, InitialPage.Center, false, false, false);
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterStartRacing).InitOpponents();
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		this.ShowStartResources(true);
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00058244 File Offset: 0x00056644
	protected override void CreateWinUI()
	{
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterWinRacingSocial), typeof(PsUITopWinRacingSocial), null, null, false, true, InitialPage.Center, false, false, false);
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterWinRacing).InitOpponents();
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, false, false, true, false, 0.03f, false, false, false);
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0005830C File Offset: 0x0005670C
	public override void PauseMinigame()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterPauseRace), typeof(PsUITopPauseFresh), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", new Action(this.m_gameLoop.ResumeSelfDestruct));
		PsIngameMenu.m_popupMenu.SetAction("Resume", new Action(this.m_gameLoop.ResumeMinigame));
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000583A8 File Offset: 0x000567A8
	public void ShowStartResources(bool _diamonds = true)
	{
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, true, true, _diamonds, false, 0.03f, true, false, false);
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x000583D0 File Offset: 0x000567D0
	public override void WinMinigame()
	{
		this.m_gameLoop.m_timeScoreCurrent = HighScores.TicksToTimeScore(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks), true);
		int num = (this.m_gameLoop as PsGameLoopSocial).m_secondarysWon;
		bool flag = num < 4;
		bool flag2 = num == 2 || num == 6 || num == 0;
		bool flag3 = num == 1 || num == 5 || num == 0;
		bool flag4 = flag2 || flag3 || flag;
		float num2 = HighScores.TimeScoreToTime(this.m_gameLoop.m_timeScoreCurrent);
		if (num2 <= this.m_diamondWinTime && flag3 && !this.m_rewardDiamonds)
		{
			num += 2;
			this.m_rewardDiamonds = true;
		}
		if (num2 <= this.m_coinWinTime && flag2 && !this.m_rewardCoins)
		{
			num++;
			this.m_rewardCoins = true;
		}
		if (num2 <= this.m_trophyWinTime && flag && !this.m_rewardTrophies)
		{
			num += 4;
			this.m_rewardTrophies = true;
		}
		(this.m_gameLoop as PsGameLoopSocial).m_secondarysWon = num;
		if (this.m_gameLoop.m_timeScoreCurrent < this.m_gameLoop.m_timeScoreBest)
		{
			this.m_gameLoop.m_timeScoreBest = this.m_gameLoop.m_timeScoreCurrent;
			this.m_saveGhost = true;
		}
		base.KeepBestGhost();
		this.CreateWinUI();
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00058538 File Offset: 0x00056938
	public override void GhostsLoaded(GhostData[] _data)
	{
		if (_data == null)
		{
			base.GhostsLoaded(_data);
			return;
		}
		this.m_trophyGhost = _data[0];
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
		(this.m_gameLoop as PsGameLoopSocial).m_raceGhostCount = _data.Length;
		this.m_allGhosts = new List<GhostData>(_data);
		base.GhostsLoaded(_data);
		this.m_trophyWinTime = HighScores.TimeScoreToTime(this.m_trophyGhost.time);
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x000585E8 File Offset: 0x000569E8
	public override void CreatePlaybackGhostVisuals()
	{
		this.m_ignoredIndices.Clear();
		if ((this.m_gameLoop as PsGameLoopSocial).m_secondarysWon >= 4)
		{
			this.m_ignoredIndices.Add(0);
		}
		if ((this.m_gameLoop as PsGameLoopSocial).m_secondarysWon >= 2)
		{
			this.m_ignoredIndices.Add(1);
		}
		if ((this.m_gameLoop as PsGameLoopSocial).m_secondarysWon >= 1)
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

	// Token: 0x06000832 RID: 2098 RVA: 0x00058784 File Offset: 0x00056B84
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

	// Token: 0x040007F0 RID: 2032
	public int m_realReward;

	// Token: 0x040007F1 RID: 2033
	public float m_trophyWinTime;

	// Token: 0x040007F2 RID: 2034
	public float m_diamondWinTime;

	// Token: 0x040007F3 RID: 2035
	public float m_coinWinTime;

	// Token: 0x040007F4 RID: 2036
	public new bool m_giveThreeStars;

	// Token: 0x040007F5 RID: 2037
	public GhostData m_trophyGhost;

	// Token: 0x040007F6 RID: 2038
	public GhostData m_coinGhost;

	// Token: 0x040007F7 RID: 2039
	public GhostData m_diamondGhost;

	// Token: 0x040007F8 RID: 2040
	public int m_rewardedDiamonds;

	// Token: 0x040007F9 RID: 2041
	public bool m_rewardTrophies;

	// Token: 0x040007FA RID: 2042
	public bool m_rewardDiamonds;

	// Token: 0x040007FB RID: 2043
	public bool m_rewardCoins;

	// Token: 0x040007FC RID: 2044
	public bool m_rewardSecondaryTrophies;

	// Token: 0x040007FD RID: 2045
	private List<int> m_ignoredIndices = new List<int>();
}
