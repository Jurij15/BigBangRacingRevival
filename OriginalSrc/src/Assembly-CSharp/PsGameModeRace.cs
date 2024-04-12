using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class PsGameModeRace : PsGameModeBase
{
	// Token: 0x060007FB RID: 2043 RVA: 0x00056712 File Offset: 0x00054B12
	public PsGameModeRace(PsGameLoop _info)
		: base(PsGameMode.Race, _info)
	{
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x00056728 File Offset: 0x00054B28
	public void CalculateMedalTimes()
	{
		Debug.LogWarning(this.m_gameLoop.m_minigameMetaData.bestTime);
		Debug.LogWarning(this.m_gameLoop.m_minigameMetaData.medianTime);
		float num = HighScores.TimeScoreToTime(this.m_gameLoop.m_minigameMetaData.bestTime);
		float num2 = HighScores.TimeScoreToTime(this.m_gameLoop.m_minigameMetaData.medianTime);
		this.m_giveThreeStars = false;
		if (num <= 0f || num2 < num)
		{
			this.m_giveThreeStars = true;
			Debug.LogWarning(string.Concat(new object[] { "Something went wrong with medal times! Best: ", num, "/ Median: ", num2 }));
		}
		if (num2 == 0f)
		{
			num2 = num * 1.25f;
		}
		else if (num2 == num)
		{
			num2 *= 1.25f;
		}
		float num3 = num2 - (num2 - num) * 0.5f;
		float num4 = num2 + (num2 - num) * 0.3f;
		float num5 = num2 + (num2 - num) * 5f;
		if (this.m_gameLoop.m_minigameMetaData.totalWinners < 5)
		{
			num3 = num + num * 0.25f;
			num4 = num + num * 0.5f;
			num5 = num * 3f;
		}
		if (this.m_gameLoop.m_forcedMedalTimes != null && this.m_gameLoop.m_forcedMedalTimes.Length == 3 && !string.IsNullOrEmpty(this.m_gameLoop.m_forcedMedalTimes[0]) && !string.IsNullOrEmpty(this.m_gameLoop.m_forcedMedalTimes[1]) && !string.IsNullOrEmpty(this.m_gameLoop.m_forcedMedalTimes[2]))
		{
			Debug.Log("USE PRESET MEDAL TIMES", null);
			this.m_giveThreeStars = false;
			float.TryParse(this.m_gameLoop.m_forcedMedalTimes[2], ref num5);
			if (num5 == 0f)
			{
				num5 = 60f;
			}
			float.TryParse(this.m_gameLoop.m_forcedMedalTimes[1], ref num4);
			if (num4 == 0f)
			{
				num4 = num5 - 1f;
			}
			float.TryParse(this.m_gameLoop.m_forcedMedalTimes[0], ref num3);
			if (num3 == 0f)
			{
				num3 = num4 - 1f;
			}
		}
		if (!this.m_giveThreeStars)
		{
			this.m_threeMedalTime = num3;
			this.m_twoMedalTime = num4;
			this.m_oneMedalTime = num5;
		}
		else
		{
			this.m_threeMedalTime = 2.1474836E+09f;
			this.m_twoMedalTime = 2.1474836E+09f;
			this.m_oneMedalTime = 2.1474836E+09f;
		}
		Debug.LogWarning(string.Concat(new object[] { "SCORE LIMITS: ", this.m_oneMedalTime, ", ", this.m_twoMedalTime, ", ", this.m_threeMedalTime }));
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x000569F4 File Offset: 0x00054DF4
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

	// Token: 0x060007FE RID: 2046 RVA: 0x00056A7C File Offset: 0x00054E7C
	public override void ApplySettings(bool _createAreaLimits = true, bool _updateDomeGfx = false)
	{
		base.ApplySettings(_createAreaLimits, _updateDomeGfx);
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00056A88 File Offset: 0x00054E88
	public override void LoadGhosts(Action<GhostData[]> _customSUCCESSCallback = null)
	{
		this.ServerGetGhosts(new GhostGetData
		{
			minigameInfo = PsState.m_activeGameLoop,
			times = this.GetGhostTimes()
		}, _customSUCCESSCallback);
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x00056ABC File Offset: 0x00054EBC
	public override void GhostsLoaded(GhostData[] _data)
	{
		Debug.LogWarning(string.Concat(new object[] { "ghosts searched with times: ", this.m_threeMedalTime, " ", this.m_twoMedalTime, " ", this.m_oneMedalTime }));
		base.GhostsLoaded(_data);
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x00056B24 File Offset: 0x00054F24
	public int[] GetGhostTimes()
	{
		int[] array = new int[3];
		if (this.m_giveThreeStars)
		{
			array[0] = int.MaxValue;
			array[1] = int.MaxValue;
			array[2] = int.MaxValue;
		}
		else
		{
			array[0] = HighScores.TimeToTimeScore(this.m_oneMedalTime);
			array[1] = HighScores.TimeToTimeScore(this.m_twoMedalTime);
			array[2] = HighScores.TimeToTimeScore(this.m_threeMedalTime);
		}
		return array;
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00056B8B File Offset: 0x00054F8B
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUITopPlayRaceSimple(_restartAction, _pauseAction);
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x00056BA4 File Offset: 0x00054FA4
	public override void WinMinigame()
	{
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = HighScores.TicksToTimeScore(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks), true);
		float num = HighScores.TimeScoreToTime(this.m_gameLoop.m_timeScoreCurrent);
		Debug.LogWarning(string.Concat(new object[] { "YOUR TIME: ", num, ", SCORE LIMITS: ", this.m_threeMedalTime, ", ", this.m_twoMedalTime, ", ", this.m_oneMedalTime }));
		int num2 = 0;
		if (num <= this.m_threeMedalTime)
		{
			PsAchievementManager.IncrementProgress("gainFiftyGoldMedals", 1);
			num2 = 3;
		}
		else if (num <= this.m_twoMedalTime)
		{
			num2 = 2;
		}
		else if (num <= this.m_oneMedalTime)
		{
			num2 = 1;
		}
		if (this.m_gameLoop.m_timeScoreCurrent < this.m_gameLoop.m_timeScoreBest)
		{
			this.m_gameLoop.m_timeScoreBest = this.m_gameLoop.m_timeScoreCurrent;
			this.m_gameLoop.m_minigameMetaData.timeScore = this.m_gameLoop.m_timeScoreBest;
			PsMetricsData.m_isSendingGhost = true;
			PsMetricsData.m_isNewGhost = true;
			this.m_saveGhost = true;
		}
		bool flag = false;
		this.m_gameLoop.m_currentRunScore = num2;
		if (num2 > this.m_gameLoop.m_scoreCurrent)
		{
			this.m_gameLoop.m_scoreCurrent = num2;
			if (this.m_gameLoop.m_scoreCurrent > this.m_gameLoop.m_scoreBest)
			{
				this.m_gameLoop.m_scoreBest = this.m_gameLoop.m_scoreCurrent;
				this.m_gameLoop.m_minigameMetaData.score = this.m_gameLoop.m_scoreBest;
				if (this.m_gameLoop.m_scoreBest == 3)
				{
					flag = true;
				}
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
		this.KeepBestGhost();
		if (flag)
		{
			this.SendRaceEndData();
		}
		this.CreateWinUI();
		base.WinMinigame();
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00056E28 File Offset: 0x00055228
	protected void KeepBestGhost()
	{
		DataBlob dataBlob = default(DataBlob);
		if (this.m_saveGhost)
		{
			if (this.m_recordingGhost != null && !this.m_recordingGhost.m_recording)
			{
				dataBlob = ClientTools.CreateGhostDataBlob(this.m_recordingGhost);
			}
			this.m_bestGhost = dataBlob;
			this.m_bestGhostTime = this.m_gameLoop.m_timeScoreCurrent;
		}
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00056E88 File Offset: 0x00055288
	protected override void CreateWinUI()
	{
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterWinRace), typeof(PsUITopWin), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", new Action(this.m_gameLoop.RestartMinigame));
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00056F00 File Offset: 0x00055300
	public override void PauseMinigame()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterPauseRace), typeof(PsUITopPauseRace), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", new Action(this.m_gameLoop.RestartMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Resume", new Action(this.m_gameLoop.ResumeMinigame));
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x00056FBC File Offset: 0x000553BC
	private void SendRaceEndData()
	{
		this.m_waitForHighscoreAndGhost = true;
		this.m_waitForNextGhost = false;
		DataBlob bestGhost = this.m_bestGhost;
		Debug.LogWarning("LAST SENT GHOST SIZE: " + PsMetricsData.m_lastGhostSize);
		this.SendRacingEndData(bestGhost);
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00057000 File Offset: 0x00055400
	public void SendRacingEndData(DataBlob _blob)
	{
		Debug.Log("Race fresh: sending ghost data", null);
		RacingGhostData data = new RacingGhostData(PsState.m_activeGameLoop.m_minigameId, null, this.m_bestGhostTime, base.GetUpgradeSum(), PsState.m_activeMinigame.m_playerUnitName, _blob, false);
		OpponentData[] opponents = new OpponentData[this.m_allGhosts.Count];
		List<GhostData> list = new List<GhostData>(this.m_allGhosts);
		list.Reverse();
		opponents = new OpponentData[this.m_allGhosts.Count];
		for (int i = 0; i < opponents.Length; i++)
		{
			bool flag = this.m_gameLoop.GetPosition() - 1 <= i;
			int num = ((!flag) ? this.m_allGhosts[i].ghostWin : this.m_allGhosts[i].ghostLoss);
			opponents[i] = new OpponentData(this.m_allGhosts[i].ghostId, num);
		}
		if (_blob.data != null)
		{
			PsMetricsData.m_lastGhostSize = _blob.data.Length;
		}
		new PsServerQueueFlow(null, delegate
		{
			this.SendGhostData(data, opponents);
		}, new string[] { "SetData" });
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x00057148 File Offset: 0x00055548
	public HttpC SendGhostData(RacingGhostData _data, OpponentData[] _opponents)
	{
		Hashtable hashtable = null;
		if (this.m_gameLoop.m_path != null)
		{
			if (this.m_gameLoop.m_path.GetPathType() == PsPlanetPathType.main)
			{
				hashtable = ClientTools.GenerateProgressionPathJson(this.m_gameLoop, this.m_gameLoop.m_path.m_currentNodeId, true, true, true);
			}
			else
			{
				hashtable = ClientTools.GenerateProgressionPathJson(this.m_gameLoop.m_path);
			}
		}
		return Trophy.SendScore(_data, _opponents, this.m_gameLoop.m_timeScoreBest != int.MaxValue, this.m_finalSend, hashtable, new Action<HttpC>(this.EndDataSendOK), delegate(HttpC c)
		{
			this.EndDataSendFAILED(c, _data, _opponents);
		}, null);
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00057214 File Offset: 0x00055614
	public void EndDataSendOK(HttpC _c)
	{
		if (PsState.m_activeGameLoop.m_scoreBest >= 3)
		{
			PsMetagameManager.m_playerStats.racesCompleted++;
			PsMetagameManager.m_playerStats.racesThisSeason++;
		}
		Debug.LogWarning("RACING GHOSTS SENT");
		this.m_waitForHighscoreAndGhost = false;
		this.m_waitForNextGhost = false;
		PsPlanetSerializer.SaveProgressionLocally(PsPlanetManager.GetCurrentPlanet());
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		SeasonEndData seasonEndData = ClientTools.ParseSeasonEndData(dictionary);
		if (seasonEndData != null)
		{
			PsMetagameManager.SetSeasonEndData(seasonEndData);
		}
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x0005729C File Offset: 0x0005569C
	public void EndDataSendFAILED(HttpC _c, RacingGhostData _data, OpponentData[] _opponents)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => this.SendGhostData(_data, _opponents), null);
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x000572E6 File Offset: 0x000556E6
	public override void SendQuit()
	{
		this.m_finalSend = true;
		this.SendRaceEndData();
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x000572F5 File Offset: 0x000556F5
	protected void NewGhostLoaded()
	{
		this.m_waitForNextGhost = false;
		Debug.LogWarning("NEW GHOST LOADED (not really...)");
	}

	// Token: 0x040007D8 RID: 2008
	public float m_threeMedalTime;

	// Token: 0x040007D9 RID: 2009
	public float m_twoMedalTime;

	// Token: 0x040007DA RID: 2010
	public float m_oneMedalTime;

	// Token: 0x040007DB RID: 2011
	public bool m_giveThreeStars;

	// Token: 0x040007DC RID: 2012
	public bool m_saveGhost;

	// Token: 0x040007DD RID: 2013
	public DataBlob m_bestGhost;

	// Token: 0x040007DE RID: 2014
	public int m_bestGhostTime = int.MaxValue;

	// Token: 0x040007DF RID: 2015
	public bool m_finalSend;
}
