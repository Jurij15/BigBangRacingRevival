using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x02000121 RID: 289
public class PsGameModeTournament : PsGameModeRace
{
	// Token: 0x06000854 RID: 2132 RVA: 0x0005A268 File Offset: 0x00058668
	public PsGameModeTournament(PsGameLoop _info)
		: base(_info)
	{
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0005A29C File Offset: 0x0005869C
	public void AddGhostLoadedCallback(Action _callback)
	{
		this.m_newGhostsLoaded = (Action)Delegate.Remove(this.m_newGhostsLoaded, _callback);
		this.m_newGhostsLoaded = (Action)Delegate.Combine(this.m_newGhostsLoaded, _callback);
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0005A2CC File Offset: 0x000586CC
	public void RemoveGhostLoadedCallback(Action _callback)
	{
		this.m_newGhostsLoaded = (Action)Delegate.Remove(this.m_newGhostsLoaded, _callback);
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0005A2E8 File Offset: 0x000586E8
	public override void CreateMusic()
	{
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		minigame.m_music = SoundS.AddComponent(minigame.m_environmentTC, PsFMODManager.GetMusic("BossMusic"), 1f, true);
		SoundS.PlaySound(minigame.m_music, true);
		SoundS.SetSoundParameter(minigame.m_music, "MusicState", 0f);
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0005A342 File Offset: 0x00058742
	public override void EnterMinigame()
	{
		this.ShowStartUI();
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0005A34A File Offset: 0x0005874A
	private void ShowStartUI()
	{
		PsIngameMenu.CloseAll();
		this.CreateTournamentUI(true, true);
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0005A35C File Offset: 0x0005875C
	public override void PauseMinigame()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterPauseRace), typeof(PsUITopPause), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", delegate
		{
			this.m_gameLoop.ResumeMinigame();
			this.m_gameLoop.SelfDestructPlayer();
		});
		PsIngameMenu.m_popupMenu.SetAction("Resume", new Action(this.m_gameLoop.ResumeMinigame));
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0005A3F4 File Offset: 0x000587F4
	public override void UpdateGhosts(bool _updateLogic)
	{
		if (_updateLogic && this.m_recordingGhost != null)
		{
			this.m_recordingGhost.UpdateRecording(false);
		}
		for (int i = 0; i < this.m_playbackGhosts.Count; i++)
		{
			this.m_playbackGhosts[i].Update(_updateLogic);
		}
		if (this.m_spectateGhosts != null && this.m_spectating)
		{
			int num = -1;
			bool flag = true;
			bool flag2 = false;
			for (int j = 0; j < this.m_spectateGhosts.Count; j++)
			{
				this.m_spectateGhosts[j].Update(true);
				if (this.m_spectateGhosts[j].m_ghost.PlaybackEnded())
				{
					if (this.m_spectateGhostIndex == j)
					{
						flag2 = true;
					}
				}
				else
				{
					flag = false;
					num = j;
				}
			}
			if (flag)
			{
				this.SpectateStop();
				this.LoadSpectateGhosts(null, false);
			}
			else if (flag2 && num != -1)
			{
				this.ChangeTarget(num);
			}
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0005A500 File Offset: 0x00058900
	public void GivePrize()
	{
		PsIngameMenu.CloseAll();
		int playerCount = PsUITournamentLeaderboard.GetPlayerCount();
		int ownPosition = PsUITournamentLeaderboard.GetOwnPosition();
		string text = null;
		if (ownPosition == 1)
		{
			string text2 = "GoldenCarHelmet";
			PsCustomisationData vehicleCustomisationData = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar));
			PsCustomisationItem itemByIdentifier = vehicleCustomisationData.GetItemByIdentifier(text2);
			if (!itemByIdentifier.m_unlocked)
			{
				text = text2;
			}
		}
		GachaType gachaReward = Tournaments.GetGachaReward(ownPosition, playerCount);
		int coinPrize = Tournaments.GetCoinPrize(ownPosition, playerCount);
		Hashtable setdata = PsGachaManager.OpenCustomGacha(gachaReward, text, coinPrize);
		PsMetagameManager.m_activeTournament.tournament.claimed = true;
		new PsServerQueueFlow(null, delegate
		{
			PsGameModeTournament.Claim(setdata);
		}, new string[] { "SetData" });
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterOpenGacha), null, null, null, true, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", delegate
		{
			this.ShowStartUI();
		});
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0005A5F4 File Offset: 0x000589F4
	public static void Claim(Hashtable _setdata)
	{
		HttpC httpC = Tournament.Claim(_setdata, new Action<HttpC>(PsGameModeTournament.ClaimSucceed), new Action<HttpC>(PsGameModeTournament.ClaimFailed), null);
		httpC.objectData = _setdata;
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0005A64C File Offset: 0x00058A4C
	public static void ClaimSucceed(HttpC _c)
	{
		if (PsState.m_activeGameLoop != null && PsState.m_activeGameLoop is PsGameLoopTournament)
		{
			(PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.claimed = true;
		}
		if (PsMetagameManager.m_activeTournament != null)
		{
			PsMetagameManager.m_activeTournament.tournament.claimed = true;
		}
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0005A6A8 File Offset: 0x00058AA8
	public static void ClaimFailed(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC httpC = Tournament.Claim(_c.objectData as Hashtable, new Action<HttpC>(PsGameModeTournament.ClaimSucceed), new Action<HttpC>(PsGameModeTournament.ClaimFailed), null);
			httpC.objectData = _c.objectData;
			return httpC;
		}, null);
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0005A6EC File Offset: 0x00058AEC
	public void ShowStartResources()
	{
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, false, false, false, false, 0.03f, false, false, false);
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0005A714 File Offset: 0x00058B14
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUITopPlayRacing(_restartAction, _pauseAction);
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0005A730 File Offset: 0x00058B30
	public override void StartMinigame()
	{
		this.SpectateStop();
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

	// Token: 0x06000863 RID: 2147 RVA: 0x0005A7AC File Offset: 0x00058BAC
	public override void LoseMinigame()
	{
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = int.MaxValue;
		base.ShowLoseUI();
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0005A7CB File Offset: 0x00058BCB
	protected override void ShowLoseUI2()
	{
		PsIngameMenu.CloseAll();
		this.CreateTournamentUI(true, false);
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0005A7DC File Offset: 0x00058BDC
	public override void WinMinigame()
	{
		PsMetagameManager.HideResources();
		this.m_gameLoop.m_timeScoreCurrent = HighScores.TicksToTimeScore(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks), true);
		float num = HighScores.TimeScoreToTime(this.m_gameLoop.m_timeScoreCurrent);
		float num2 = HighScores.TimeScoreToTime(this.m_gameLoop.m_timeScoreBest);
		PsGameLoopTournament psGameLoopTournament = this.m_gameLoop as PsGameLoopTournament;
		string tournamentId = psGameLoopTournament.m_eventMessage.tournament.tournamentId;
		int room = psGameLoopTournament.m_eventMessage.tournament.room;
		int timeScoreCurrent = psGameLoopTournament.m_timeScoreCurrent;
		int time = PsUITournamentLeaderboard.GetNewestLeaderboard()[0].time;
		bool boosterUsed = psGameLoopTournament.m_boosterUsed;
		bool flag = this.m_gameLoop.m_timeScoreCurrent < this.m_gameLoop.m_timeScoreBest || this.m_gameLoop.m_timeScoreBest == 0;
		bool flag2 = flag && PsUITournamentLeaderboard.GetPlayerIndex(this.m_gameLoop.m_timeScoreCurrent, PsUITournamentLeaderboard.GetNewestLeaderboard()) < PsUITournamentLeaderboard.GetPlayerIndex((this.m_gameLoop.m_timeScoreBest == 0) ? int.MaxValue : this.m_gameLoop.m_timeScoreBest, PsUITournamentLeaderboard.GetNewestLeaderboard());
		PsMetrics.TournamentGoalReached(tournamentId, room, timeScoreCurrent, time, boosterUsed, flag, flag2);
		if (this.m_gameLoop.m_timeScoreCurrent < this.m_gameLoop.m_timeScoreBest || this.m_gameLoop.m_timeScoreBest == 0)
		{
			(this.m_gameLoop as PsGameLoopTournament).m_eventMessage.tournament.time = this.m_gameLoop.m_timeScoreCurrent;
			this.m_gameLoop.m_timeScoreBest = this.m_gameLoop.m_timeScoreCurrent;
			this.m_saveGhost = true;
			this.m_getNewGhosts = true;
			this.LoadGhosts(null);
		}
		if (this.m_saveGhost)
		{
			base.KeepBestGhost();
			Tournament.TournamentSendScoreData data = new Tournament.TournamentSendScoreData(PsState.m_activeGameLoop.m_minigameId, this.m_bestGhostTime, PsState.m_activeMinigame.m_playerUnitName, this.m_bestGhost);
			new PsServerQueueFlow(null, delegate
			{
				this.SaveScore(data);
			}, new string[] { "SetData" });
		}
		else
		{
			(this.m_gameLoop as PsGameLoopTournament).SendResources();
		}
		this.CreateWinUI();
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0005AA17 File Offset: 0x00058E17
	public void SaveScore(Tournament.TournamentSendScoreData _data)
	{
		this.SendScore(_data);
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0005AA24 File Offset: 0x00058E24
	public HttpC SendScore(Tournament.TournamentSendScoreData _data)
	{
		HttpC httpC = Tournament.SendScore(_data, new Action<HttpC>(this.SendScoreSucceed), new Action<HttpC>(this.SendScoreFailed), null);
		httpC.objectData = _data;
		return httpC;
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0005AA59 File Offset: 0x00058E59
	public void SendScoreSucceed(HttpC _c)
	{
		Debug.Log("Tournament scoresend succeed", null);
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0005AA68 File Offset: 0x00058E68
	public void SendScoreFailed(HttpC _c)
	{
		Debug.LogError("Scoresend failed!");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => this.SendScore((Tournament.TournamentSendScoreData)_c.objectData), null);
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0005AABC File Offset: 0x00058EBC
	protected void CreateTournamentUI(bool _loadSpectateGhosts = true, bool _delayedSpectate = false)
	{
		FrbMetrics.SetCurrentScreen("tournament_lobby");
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterTournament), typeof(PsUITopStartRace), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.m_scrollableCanvas.RemoveTouchAreas();
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", delegate
		{
			this.SpectateStop();
			this.m_gameLoop.RestartMinigame();
		});
		PsIngameMenu.m_popupMenu.SetAction("GivePrize", new Action(this.GivePrize));
		if (_loadSpectateGhosts)
		{
			this.LoadSpectateGhosts(null, true);
		}
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x0005AB6D File Offset: 0x00058F6D
	protected override void CreateWinUI()
	{
		this.CreateTournamentUI(false, false);
		(PsIngameMenu.m_popupMenu.m_mainContent as PsUICenterTournament).ShowTime(delegate
		{
			this.LoadSpectateGhosts(null, false);
		});
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0005AB98 File Offset: 0x00058F98
	public override HttpC ServerGetGhosts(GhostGetData _data, Action<GhostData[]> _customSUCCESSCallback = null)
	{
		this.m_waitForNextGhost = true;
		Action<GhostData[]> action = new Action<GhostData[]>(this.GhostsLoadSUCCEED);
		if (_customSUCCESSCallback != null)
		{
			action = _customSUCCESSCallback;
		}
		Debug.LogWarning("REQUEST GHOST FROM SERVER");
		HttpC ghosts = Tournament.GetGhosts(action, new Action<HttpC>(this.GhostsLoadFAILED), new Action(base.GhostLoadErrorHandler), (!this.m_getNewGhosts) ? 0 : this.m_gameLoop.m_timeScoreCurrent);
		this.m_getNewGhosts = false;
		ghosts.objectData = _data;
		base.CreateGhostLoaderEntity();
		EntityManager.AddComponentToEntity(this.m_ghostLoaderEntity, ghosts);
		return ghosts;
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0005AC2C File Offset: 0x0005902C
	public override void GhostsLoaded(GhostData[] _data)
	{
		this.m_ghostScores = new List<int>();
		this.m_allGhosts = new List<GhostData>();
		if (_data == null)
		{
			Debug.LogWarning("NO GHOSTS DOWNLOADED");
			this.m_allGhosts = new List<GhostData>();
			this.m_waitForNextGhost = false;
			this.m_newGhostsLoaded.Invoke();
			return;
		}
		bool flag = false;
		Debug.LogWarning(_data.Length + " GHOSTS DOWNLOADED");
		if (this.m_playbackGhosts.Count > 0)
		{
			flag = true;
			this.m_freshGhosts = new List<GhostObject>();
		}
		for (int i = 0; i < _data.Length; i++)
		{
			if (_data[i].ghost != null && _data[i].ghost.Length > 0)
			{
				byte[] array = FilePacker.UnZipBytes(_data[i].ghost);
				global::Ghost ghost = global::Ghost.DeSerializeFromBytes(array);
				Debug.LogWarning(string.Concat(new object[]
				{
					"GHOST NO. ",
					i + 1,
					": ",
					_data[i].ghost.Length,
					", ",
					_data[i].ghostId,
					" (unzipped: ",
					array.Length,
					")"
				}));
				if (ghost != null)
				{
					this.m_allGhosts.Add(_data[i]);
					this.m_ghostScores.Add(_data[i].time);
					GhostObject ghostObject = this.GetGhostObject(_data[i], ghost, null);
					if (!flag)
					{
						this.m_playbackGhosts.Add(ghostObject);
					}
					else
					{
						this.m_freshGhosts.Add(ghostObject);
					}
					Debug.LogWarning("CREATED PLAYBACK GHOST:" + _data[i].name);
				}
				else
				{
					Debug.LogError("GHOST DATA CORRUPTED!");
				}
			}
			else
			{
				Debug.LogWarning("GHOST DOWNLOAD FAILED - NULL OR CORRUPT DATA");
			}
		}
		this.m_newGhostsLoaded.Invoke();
		this.m_waitForNextGhost = false;
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0005AE08 File Offset: 0x00059208
	public void ClearGhosts()
	{
		for (int i = 0; i < this.m_playbackGhosts.Count; i++)
		{
			this.m_playbackGhosts[i].DestroyVisuals();
			this.m_playbackGhosts[i].m_ghost.m_playbackTick = 0f;
			this.m_playbackGhosts[i].m_ghost.ResetEvents();
		}
		if (this.m_spectateGhosts != null)
		{
			for (int j = 0; j < this.m_spectateGhosts.Count; j++)
			{
				this.m_spectateGhosts[j].DestroyVisuals();
				this.m_spectateGhosts[j].m_ghost.m_playbackTick = 0f;
				this.m_spectateGhosts[j].m_ghost.ResetEvents();
			}
		}
		if (this.m_freshGhosts != null && this.m_freshGhosts.Count > 0)
		{
			this.m_playbackGhosts = this.m_freshGhosts;
			this.m_freshGhosts = null;
		}
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0005AF0C File Offset: 0x0005930C
	public override void CreatePlaybackGhostVisuals()
	{
		List<GhostObject> list = this.m_playbackGhosts;
		if (this.m_spectating)
		{
			list = this.m_spectateGhosts;
		}
		for (int i = 0; i < this.m_playbackGhosts.Count; i++)
		{
			this.m_playbackGhosts[i].DestroyVisuals();
		}
		if (this.m_spectateGhosts != null)
		{
			for (int j = 0; j < this.m_spectateGhosts.Count; j++)
			{
				this.m_spectateGhosts[j].DestroyVisuals();
				this.m_spectateGhosts[j].m_ghost.m_playbackTick = 0f;
				this.m_spectateGhosts[j].m_ghost.ResetEvents();
			}
		}
		if (this.m_freshGhosts != null && this.m_freshGhosts.Count > 0)
		{
			list = this.m_freshGhosts;
			this.m_playbackGhosts = this.m_freshGhosts;
			this.m_freshGhosts = null;
		}
		for (int k = 0; k < list.Count; k++)
		{
			list[k].DestroyVisuals();
			list[k].m_ghost.m_playbackTick = 0f;
			list[k].m_ghost.ResetEvents();
			string text = list[k].m_ghost.m_unitClass;
			if (string.IsNullOrEmpty(text))
			{
				text = PsState.m_activeGameLoop.m_minigameMetaData.playerUnit;
			}
			string text2 = string.Empty;
			switch (k)
			{
			case 0:
				text2 = "#FF6A0B";
				break;
			case 1:
				text2 = "#0595FF";
				break;
			case 2:
				text2 = "#00ff87";
				break;
			default:
				text2 = "#FFFFFF";
				break;
			}
			if (text == "OffroadCar")
			{
				this.CreateOffroadCarPlaybackGhostEntity(list[k], k == 0, text2);
			}
			else if (text == "Motorcycle")
			{
				this.CreateMotorcyclePlaybackGhostEntity(list[k], k == 0, text2);
			}
			Debug.LogWarning("CREATED PLAYBACK GHOST VISUALS: " + list[k].m_name);
		}
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0005B130 File Offset: 0x00059530
	private string GetRandomGhostIds()
	{
		string text = string.Empty;
		List<HighscoreDataEntry> newestLeaderboard = PsUITournamentLeaderboard.GetNewestLeaderboard();
		int num = 3;
		if (newestLeaderboard != null && newestLeaderboard.Count > 0)
		{
			int num2 = 0;
			while (num2 < num && num2 < newestLeaderboard.Count)
			{
				if (num2 != 0)
				{
					text = text + "," + newestLeaderboard[num2].playerId;
				}
				else
				{
					text += newestLeaderboard[num2].playerId;
				}
				num2++;
			}
		}
		return text;
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0005B1B4 File Offset: 0x000595B4
	public void CreateSpectateGhostLoaderEntity()
	{
		if ((this.m_gameLoop as PsGameLoopTournament).m_spectateGhostLoaderEntity != null)
		{
			EntityManager.RemoveEntity((this.m_gameLoop as PsGameLoopTournament).m_spectateGhostLoaderEntity);
		}
		(this.m_gameLoop as PsGameLoopTournament).m_spectateGhostLoaderEntity = EntityManager.AddEntity();
		(this.m_gameLoop as PsGameLoopTournament).m_spectateGhostLoaderEntity.m_persistent = true;
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0005B218 File Offset: 0x00059618
	public void LoadSpectateGhosts(string _id = null, bool _delayedSpectate = false)
	{
		if (_delayedSpectate)
		{
			this.CreateSpectateGhostLoaderEntity();
			TimerS.AddComponent((this.m_gameLoop as PsGameLoopTournament).m_spectateGhostLoaderEntity, string.Empty, 0f, 0.75f, false, delegate(TimerC c)
			{
				TimerS.RemoveComponent(c);
				this.LoadSpectateGhosts(_id, false);
			});
			return;
		}
		PsGameLoopTournament psGameLoopTournament = this.m_gameLoop as PsGameLoopTournament;
		if (!string.IsNullOrEmpty(_id))
		{
			this.ghostChosen = true;
			this.CreateSpectateGhostLoaderEntity();
			HttpC ghostsByIds = Tournament.GetGhostsByIds(_id, new Action<GhostData[]>(this.SpectateGhostsLoaded), new Action<HttpC>(this.SpectateGhostsLoadFailed), null);
			EntityManager.AddComponentToEntity((this.m_gameLoop as PsGameLoopTournament).m_spectateGhostLoaderEntity, ghostsByIds);
		}
		else if (this.m_spectateGhosts != null && this.m_spectateGhosts.Count > 0 && this.spectateLoadTimeStamp != 0.0 && Main.m_EPOCHSeconds < this.spectateLoadTimeStamp + (double)this.loadNewSpectateGhostsTreshold)
		{
			this.SpectateStart(this.m_spectateGhosts, 0, true);
		}
		else
		{
			this.CreateSpectateGhostLoaderEntity();
			HttpC latestGhosts = Tournament.GetLatestGhosts(new Action<GhostData[]>(this.SpectateGhostsLoaded), new Action<HttpC>(this.SpectateGhostsLoadFailed), null);
			EntityManager.AddComponentToEntity((this.m_gameLoop as PsGameLoopTournament).m_spectateGhostLoaderEntity, latestGhosts);
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0005B37C File Offset: 0x0005977C
	public virtual void SpectateGhostsLoaded(GhostData[] _data)
	{
		List<GhostObject> list = new List<GhostObject>();
		if (_data == null)
		{
			Debug.LogWarning("NO SPECTATE GHOSTS DOWNLOADED");
			return;
		}
		Debug.LogWarning(_data.Length + " SPECTATE GHOSTS DOWNLOADED");
		for (int i = 0; i < _data.Length; i++)
		{
			if (_data[i].ghost != null && _data[i].ghost.Length > 0)
			{
				byte[] array = FilePacker.UnZipBytes(_data[i].ghost);
				global::Ghost ghost = global::Ghost.DeSerializeFromBytes(array);
				Debug.LogWarning(string.Concat(new object[]
				{
					"SPECTATE GHOST NO. ",
					i + 1,
					": ",
					_data[i].ghost.Length,
					", ",
					_data[i].ghostId,
					" (unzipped: ",
					array.Length,
					")"
				}));
				if (ghost != null)
				{
					GhostObject ghostObject = this.GetGhostObject(_data[i], ghost, null);
					ghostObject.m_spectateGhost = true;
					list.Add(ghostObject);
				}
				else
				{
					Debug.LogError("SPECTATE GHOST DATA CORRUPTED!");
				}
			}
			else
			{
				Debug.LogWarning("SPECTATE GHOST DOWNLOAD FAILED - NULL OR CORRUPT DATA");
			}
		}
		this.spectateLoadTimeStamp = Main.m_EPOCHSeconds;
		this.SpectateStart(list, 0, true);
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0005B4BE File Offset: 0x000598BE
	public virtual void SpectateGhostsLoadFailed(HttpC _c)
	{
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0005B4C0 File Offset: 0x000598C0
	public void SpectateStart(List<GhostObject> _ghosts, int _targetIndex, bool _snapToTarget = true)
	{
		if (Main.m_currentGame.m_currentScene == null || Main.m_currentGame.m_currentScene.m_stateMachine == null)
		{
			return;
		}
		if (Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() is GameBeginHeatState)
		{
			return;
		}
		if (PsState.m_activeMinigame.m_gameStarted)
		{
			if (PsState.m_activeMinigame.m_gameOn)
			{
				return;
			}
		}
		if (_ghosts != null && _ghosts.Count > 0)
		{
			(this.m_gameLoop as PsGameLoopTournament).InitializeSpectate();
			_targetIndex = Math.Min(Math.Max(0, _targetIndex), _ghosts.Count - 1);
			EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
			CameraS.RemoveAllTargetComponents();
			PsState.m_activeMinigame.m_groundNode.RevertGroundFromPlay();
			LevelManager.ResetCurrentLevel();
			this.ClearGhosts();
			this.m_spectateGhosts = _ghosts;
			this.m_spectating = true;
			this.CreatePlaybackGhostVisuals();
			bool flag = _snapToTarget || this.ghostChosen;
			this.ghostChosen = false;
			GameLevelPreview.LevelPreviewWithTarget(_ghosts[_targetIndex].m_chassis, false, flag);
		}
		this.m_spectateGhostIndex = _targetIndex;
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0005B5DC File Offset: 0x000599DC
	public void ChangeTarget(int _index)
	{
		if (this.m_spectateGhosts != null && this.m_spectateGhosts.Count > 0)
		{
			this.m_spectateGhostIndex = _index;
			GameLevelPreview.LevelPreviewWithTarget(this.m_spectateGhosts[this.m_spectateGhostIndex].m_chassis, true, true);
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0005B629 File Offset: 0x00059A29
	public void SpectateStop()
	{
		if (this.m_spectating)
		{
			this.m_spectating = false;
		}
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0005B640 File Offset: 0x00059A40
	public override void DestroyPlaybackGhosts()
	{
		base.DestroyPlaybackGhosts();
		while (this.m_spectateGhosts != null && this.m_spectateGhosts.Count > 0)
		{
			int num = this.m_spectateGhosts.Count - 1;
			Debug.LogWarning("DESTROYING SPECTATE PLAYBACK GHOST: " + this.m_spectateGhosts[num].m_name);
			this.m_spectateGhosts[num].Destroy();
			this.m_spectateGhosts.RemoveAt(num);
		}
	}

	// Token: 0x0400080D RID: 2061
	public Action m_newGhostsLoaded = delegate
	{
		Debug.Log("New ghosts loaded", null);
	};

	// Token: 0x0400080E RID: 2062
	private bool m_getNewGhosts;

	// Token: 0x0400080F RID: 2063
	public List<GhostObject> m_freshGhosts;

	// Token: 0x04000810 RID: 2064
	private int loadNewSpectateGhostsTreshold = 45;

	// Token: 0x04000811 RID: 2065
	public double spectateLoadTimeStamp;

	// Token: 0x04000812 RID: 2066
	private bool m_spectating;

	// Token: 0x04000813 RID: 2067
	public int m_spectateGhostIndex;

	// Token: 0x04000814 RID: 2068
	public List<GhostObject> m_spectateGhosts;

	// Token: 0x04000815 RID: 2069
	private bool ghostChosen;
}
