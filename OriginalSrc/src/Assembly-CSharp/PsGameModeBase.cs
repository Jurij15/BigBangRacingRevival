using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Server;
using UnityEngine;

// Token: 0x0200011A RID: 282
public abstract class PsGameModeBase
{
	// Token: 0x060007C8 RID: 1992 RVA: 0x0005289D File Offset: 0x00050C9D
	public PsGameModeBase(PsGameMode _gameMode, PsGameLoop _info)
	{
		this.m_gameMode = _gameMode;
		this.m_gameLoop = _info;
		this.m_playbackGhosts = new List<GhostObject>();
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x000528C0 File Offset: 0x00050CC0
	public virtual void CreateEnvironment()
	{
		if (!PsState.m_muteMusic)
		{
			this.CreateMusic();
		}
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

	// Token: 0x060007CA RID: 1994 RVA: 0x00052A27 File Offset: 0x00050E27
	public virtual void CreateMusic()
	{
		Debug.LogError("CreateMusic");
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00052A33 File Offset: 0x00050E33
	public virtual void ApplySettings(bool _createAreaLimits = true, bool _updateDomeGfx = false)
	{
		PsState.m_activeMinigame.ApplySettings(_createAreaLimits, _updateDomeGfx);
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00052A41 File Offset: 0x00050E41
	public virtual void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00052A4E File Offset: 0x00050E4E
	public virtual void CreateEditMenu(Action _exitAction, Action _testAction)
	{
		PsIngameMenu.CloseAll();
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x00052A55 File Offset: 0x00050E55
	public virtual void EnterMinigame()
	{
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x00052A58 File Offset: 0x00050E58
	public virtual void StartMinigame()
	{
		MotorcycleController.m_startCreate = false;
		PsIngameMenu.CloseAll();
		PsMetagameManager.HideResources();
		this.CreatePlayMenu(new Action(this.m_gameLoop.SelfDestructPlayer), new Action(this.m_gameLoop.PauseMinigame));
		if (PsState.m_activeMinigame.m_music != null)
		{
			SoundS.SetSoundParameter(PsState.m_activeMinigame.m_music, "MusicState", 1f);
		}
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x00052AC8 File Offset: 0x00050EC8
	public virtual void PauseMinigame()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterPauseRace), typeof(PsUITopPause), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", new Action(this.m_gameLoop.RestartMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Resume", new Action(this.m_gameLoop.ResumeMinigame));
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x00052B63 File Offset: 0x00050F63
	public virtual void WinMinigame()
	{
		PsAchievementManager.SendToServer();
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x00052B6A File Offset: 0x00050F6A
	protected virtual void CreateWinUI()
	{
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x00052B6C File Offset: 0x00050F6C
	public virtual void LoseMinigame()
	{
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterLoseRace), typeof(PsUITopLoseRace), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", new Action(this.m_gameLoop.RestartMinigame));
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00052BE4 File Offset: 0x00050FE4
	public void ShowLoseUI()
	{
		PsIngameMenu.CloseAll();
		PsMetagameManager.HideResources();
		if (this.m_gameLoop.m_recordingGif)
		{
			Debug.LogError("Gif is recording");
			TimerS.AddComponent(EntityManager.AddEntity(), string.Empty, 0.5f, 0f, true, delegate(TimerC c)
			{
				PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterDeathShare), null, null, null, false, true, InitialPage.Center, false, false, false);
				PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.ShowLoseUI2));
			});
		}
		else
		{
			this.ShowLoseUI2();
		}
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x00052C47 File Offset: 0x00051047
	protected virtual void ShowLoseUI2()
	{
		Debug.LogError("Create gamemode specific lose UI: " + this.ToString());
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00052C60 File Offset: 0x00051060
	public virtual bool isMaterialAvailable(int _index)
	{
		PsUnlockable psUnlockable = PsMetagameData.m_gameMaterials[0].m_items[_index];
		return psUnlockable.m_unlocked || PsState.m_adminMode || PsState.m_unlockEditorItems;
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00052CA4 File Offset: 0x000510A4
	public int GetUpgradeSum()
	{
		int num = 0;
		List<KeyValuePair<string, int>> upgrades = (PsState.m_activeMinigame.m_playerUnit as Vehicle).GetUpgrades();
		foreach (KeyValuePair<string, int> keyValuePair in upgrades)
		{
			KeyValuePair<string, int> keyValuePair2 = keyValuePair;
			if (keyValuePair2.Key != "tier")
			{
				num += keyValuePair.Value;
			}
		}
		return num;
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x00052D30 File Offset: 0x00051130
	public virtual void Destroy()
	{
		this.DestroyGhosts();
		this.DestroyGhostLoaderEntity();
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00052D3E File Offset: 0x0005113E
	public void CreateGhostLoaderEntity()
	{
		if (this.m_ghostLoaderEntity != null)
		{
			Debug.LogError("trying to create second ghost loader entity");
		}
		Debug.LogWarning("------------------------> CREATING GHOST LOADER ENTITY");
		this.m_ghostLoaderEntity = EntityManager.AddEntity("GhostLoaderEntity");
		this.m_ghostLoaderEntity.m_persistent = true;
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x00052D7B File Offset: 0x0005117B
	public virtual void DestroyGhostLoaderEntity()
	{
		if (this.m_ghostLoaderEntity != null)
		{
			Debug.LogWarning("------------------------> DESTROYING GHOST LOADER ENTITY");
			EntityManager.RemoveEntity(this.m_ghostLoaderEntity);
			this.m_ghostLoaderEntity = null;
		}
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00052DA4 File Offset: 0x000511A4
	public virtual void LoadGhosts(Action<GhostData[]> _customSUCCESSCallback = null)
	{
		this.DestroyPlaybackGhosts();
		this.m_ghostRetryCounter = 0;
		this.m_ghostLoadComplete = true;
		Debug.LogWarning("NO GHOSTS IN THIS GAME MODE");
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00052DC4 File Offset: 0x000511C4
	public virtual HttpC ServerGetGhosts(GhostGetData _data, Action<GhostData[]> _customSUCCESSCallback = null)
	{
		Action<GhostData[]> action = new Action<GhostData[]>(this.GhostsLoadSUCCEED);
		if (_customSUCCESSCallback != null)
		{
			action = _customSUCCESSCallback;
		}
		Debug.LogWarning("REQUEST GHOST FROM SERVER");
		HttpC httpC;
		if (string.IsNullOrEmpty(_data.playerId))
		{
			int currentMinigameVehicleTrophies = PsMetagameManager.m_playerStats.GetCurrentMinigameVehicleTrophies();
			string text = string.Concat(new object[]
			{
				currentMinigameVehicleTrophies,
				",",
				Mathf.Max(0, currentMinigameVehicleTrophies - 50),
				",",
				Mathf.Max(0, currentMinigameVehicleTrophies - 100)
			});
			httpC = Trophy.GetGhostsByTrophies(_data.minigameInfo.GetGameId(), text, PsState.GetCurrentVehicleType(true).ToString(), action, new Action<HttpC>(this.GhostsLoadFAILED), new Action(this.GhostLoadErrorHandler));
		}
		else
		{
			httpC = global::Server.Ghost.GetCreatorNew(_data.minigameInfo.m_minigameMetaData, action, new Action<HttpC>(this.GhostsLoadFAILED), new Action(this.GhostLoadErrorHandler));
		}
		httpC.objectData = _data;
		this.CreateGhostLoaderEntity();
		EntityManager.AddComponentToEntity(this.m_ghostLoaderEntity, httpC);
		return httpC;
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00052ED8 File Offset: 0x000512D8
	public virtual HttpC ServerGetRacingGhosts(GhostGetData _data, Action<GhostData[]> _customSUCCESSCallback = null)
	{
		Action<GhostData[]> action = new Action<GhostData[]>(this.GhostsLoadSUCCEED);
		if (_customSUCCESSCallback != null)
		{
			action = _customSUCCESSCallback;
		}
		Debug.LogWarning("REQUEST GHOST FROM SERVER");
		bool flag = false;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		if (this.m_gameLoop.m_forcedMedalTimes != null && this.m_gameLoop.m_forcedMedalTimes.Length == 3 && !string.IsNullOrEmpty(this.m_gameLoop.m_forcedMedalTimes[0]) && !string.IsNullOrEmpty(this.m_gameLoop.m_forcedMedalTimes[1]) && !string.IsNullOrEmpty(this.m_gameLoop.m_forcedMedalTimes[2]))
		{
			Debug.LogWarning("USE PRESET MEDAL TIMES");
			flag = true;
			float.TryParse(this.m_gameLoop.m_forcedMedalTimes[0], ref num);
			float.TryParse(this.m_gameLoop.m_forcedMedalTimes[1], ref num2);
			float.TryParse(this.m_gameLoop.m_forcedMedalTimes[2], ref num3);
		}
		int[] array = new int[]
		{
			HighScores.TimeToTimeScore(1f),
			HighScores.TimeToTimeScore(2f)
		};
		int num4 = 0;
		if (flag)
		{
			num4 = HighScores.TimeToTimeScore(num);
			array[0] = HighScores.TimeToTimeScore(num2 - num);
			array[1] = HighScores.TimeToTimeScore(num3 - num);
		}
		RacingGetData racingGetData = new RacingGetData(_data.minigameInfo.GetGameId(), array, num4, _data.ghostIds);
		Debug.LogWarning(string.Concat(new object[]
		{
			"Ghost variations. Trophy ghost: ",
			HighScores.TimeScoreToTime(num4),
			" secondary 1: +",
			HighScores.TimeScoreToTime(array[0]),
			" secondary 2: +",
			HighScores.TimeScoreToTime(array[1])
		}));
		HttpC httpC = null;
		if (!string.IsNullOrEmpty(racingGetData.m_trophyGhostIds))
		{
			httpC = Trophy.GetGhostsByIds(racingGetData.m_gameId, racingGetData.m_trophyGhostIds, PsState.GetCurrentVehicleType(false).ToString(), action, new Action<HttpC>(this.RacingGhostsLoadFAILED), new Action(this.GhostLoadErrorHandler));
		}
		else if (flag && !string.IsNullOrEmpty(racingGetData.m_trophyGhostTimes))
		{
			httpC = Trophy.GetGhostsByTime(racingGetData.m_gameId, racingGetData.m_trophyGhostTimes, PsState.GetCurrentVehicleType(false).ToString(), action, new Action<HttpC>(this.RacingGhostsLoadFAILED), new Action(this.GhostLoadErrorHandler));
		}
		else if (!string.IsNullOrEmpty(racingGetData.m_trophyGhostTrophies))
		{
			httpC = Trophy.GetGhostsByTrophies(racingGetData.m_gameId, racingGetData.m_trophyGhostTrophies, PsState.GetCurrentVehicleType(false).ToString(), action, new Action<HttpC>(this.RacingGhostsLoadFAILED), new Action(this.GhostLoadErrorHandler));
		}
		else
		{
			Debug.LogError("ERROR: Ghost get data has no ids, times or trophies!");
		}
		httpC.objectData = _data;
		this.CreateGhostLoaderEntity();
		EntityManager.AddComponentToEntity(this.m_ghostLoaderEntity, httpC);
		return httpC;
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x000531B0 File Offset: 0x000515B0
	public void GhostLoadErrorHandler()
	{
		this.m_waitForNextGhost = false;
		this.m_ghostLoadComplete = true;
		this.m_gameLoop.GhostsLoaded();
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x000531CB File Offset: 0x000515CB
	public virtual void GhostsLoadSUCCEED(GhostData[] _data)
	{
		this.DestroyGhostLoaderEntity();
		this.GhostsLoaded(_data);
		this.m_gameLoop.GhostsLoaded();
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x000531E8 File Offset: 0x000515E8
	public virtual void GhostsLoaded(GhostData[] _data)
	{
		this.m_ghostScores = new List<int>();
		this.m_allGhosts = new List<GhostData>();
		if (_data == null)
		{
			Debug.LogWarning("NO GHOSTS DOWNLOADED");
			this.m_allGhosts = new List<GhostData>();
			return;
		}
		Debug.LogWarning(_data.Length + " GHOSTS DOWNLOADED");
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
					string text = "menu_chest_badge_active";
					if (!(PsState.m_activeGameLoop is PsGameLoopRacing) && i == 0)
					{
						text = "menu_chest_badge_active";
					}
					else if (!_data[i].rival)
					{
						text = null;
					}
					GhostObject ghostObject = this.GetGhostObject(_data[i], ghost, text);
					this.m_playbackGhosts.Add(ghostObject);
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
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x0005339C File Offset: 0x0005179C
	protected virtual GhostObject GetGhostObject(GhostData _data, global::Ghost _ghost, string _rewardFrameName = null)
	{
		return new GhostObject(_data.name, _data.time, _data.playerId, _data.ghostId, _data.countryCode, _ghost, _rewardFrameName, _data.facebookId, _data.version);
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x000533DC File Offset: 0x000517DC
	public virtual void GhostsLoadFAILED(HttpC _c)
	{
		Debug.LogWarning("GHOST DOWNLOAD FAILED");
		if (this.m_ghostRetryCounter < 2)
		{
			this.ServerGetGhosts((GhostGetData)_c.objectData, null);
			this.m_ghostRetryCounter++;
			Debug.LogWarning("RETRYING GHOST DOWNLOAD: " + this.m_ghostRetryCounter);
		}
		else
		{
			this.GhostLoadErrorHandler();
		}
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x00053448 File Offset: 0x00051848
	public virtual void RacingGhostsLoadFAILED(HttpC _c)
	{
		Debug.LogWarning("GHOST DOWNLOAD FAILED");
		this.ServerGetRacingGhosts((GhostGetData)_c.objectData, null);
		this.m_ghostRetryCounter++;
		Debug.LogWarning("RETRYING GHOST DOWNLOAD (this will not give up until ghost is loaded): " + this.m_ghostRetryCounter);
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x0005349C File Offset: 0x0005189C
	public virtual void CreatePlaybackGhostVisuals()
	{
		for (int i = 0; i < this.m_playbackGhosts.Count; i++)
		{
			this.m_playbackGhosts[i].DestroyVisuals();
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

	// Token: 0x060007E5 RID: 2021 RVA: 0x000535B0 File Offset: 0x000519B0
	public virtual void DestroyPlaybackGhostVisuals()
	{
		for (int i = 0; i < this.m_playbackGhosts.Count; i++)
		{
			Debug.LogWarning("DESTROYING PLAYBACK GHOST VISUALS: " + this.m_playbackGhosts[i].m_name);
			this.m_playbackGhosts[i].DestroyVisuals();
		}
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x0005360C File Offset: 0x00051A0C
	public virtual void DestroyPlaybackGhosts()
	{
		while (this.m_playbackGhosts.Count > 0)
		{
			int num = this.m_playbackGhosts.Count - 1;
			Debug.LogWarning("DESTROYING PLAYBACK GHOST: " + this.m_playbackGhosts[num].m_name);
			this.m_playbackGhosts[num].Destroy();
			this.m_playbackGhosts.RemoveAt(num);
		}
		this.m_streaks = null;
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00053681 File Offset: 0x00051A81
	public void DestroyGhosts()
	{
		if (this.m_recordingGhost != null)
		{
			this.m_recordingGhost.Destroy();
			this.m_recordingGhost = null;
		}
		this.DestroyPlaybackGhosts();
		this.m_streaks = null;
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x000536B0 File Offset: 0x00051AB0
	public void CreateRecordingGhost(string _unitClass, Hashtable _upgradeValues)
	{
		this.DestroyRecordingGhost();
		if (!string.IsNullOrEmpty(_unitClass))
		{
			List<string> list;
			Dictionary<string, ObscuredInt> dictionary;
			if (Type.GetType(_unitClass) == typeof(OffroadCar))
			{
				list = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetInstalledItemIdentifiers();
				dictionary = PsUpgradeManager.m_offroadCarUpgrades;
			}
			else
			{
				list = PsCustomisationManager.GetVehicleCustomisationData(typeof(Motorcycle)).GetInstalledItemIdentifiers();
				dictionary = PsUpgradeManager.m_motorcycleUpgrades;
			}
			this.m_recordingGhost = new global::Ghost(_unitClass, _upgradeValues, list, list, dictionary, true);
		}
		else
		{
			this.m_recordingGhost = new global::Ghost(_unitClass, _upgradeValues, this.m_playbackGhosts[0].m_ghost.m_characterVisualItems, this.m_playbackGhosts[0].m_ghost.m_vehicleVisualItems, this.m_playbackGhosts[0].m_ghost.m_vehicleUpgradeItems, true);
		}
		this.m_recordingGhost.m_frameSkip = 5;
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00053790 File Offset: 0x00051B90
	public void DestroyRecordingGhost()
	{
		if (this.m_recordingGhost != null)
		{
			this.m_recordingGhost.Destroy();
			this.m_recordingGhost = null;
		}
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x000537AF File Offset: 0x00051BAF
	public void StopRecordingGhost()
	{
		if (this.m_recordingGhost != null && this.m_recordingGhost.m_recording)
		{
			this.m_recordingGhost.StopRecord();
		}
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x000537D8 File Offset: 0x00051BD8
	public virtual void UpdateGhosts(bool _updateLogic)
	{
		if (_updateLogic && this.m_recordingGhost != null)
		{
			this.m_recordingGhost.UpdateRecording(false);
		}
		for (int i = 0; i < this.m_playbackGhosts.Count; i++)
		{
			this.m_playbackGhosts[i].Update(_updateLogic);
		}
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x00053830 File Offset: 0x00051C30
	public virtual void AddGhostEvent(GhostEventType _type, int _tick)
	{
		if (this.m_recordingGhost != null)
		{
			this.m_recordingGhost.AddEvent(_type, _tick);
		}
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0005384A File Offset: 0x00051C4A
	public virtual void RecordGhostGoal(int _tick)
	{
		if (this.m_recordingGhost != null)
		{
			this.m_recordingGhost.AddGoalFrame(_tick);
		}
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x00053864 File Offset: 0x00051C64
	public virtual void CreateOffroadCarPlaybackGhostEntity(GhostObject _ghost, bool _showAtStart = false, string _identifierColor = "")
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.OffroadCarGhostNewPrefab_GameObject);
		GhostPart ghostPart = new GhostPart(gameObject.transform.Find("Body").gameObject, Vector3.zero, "chassis", false);
		GhostPart ghostPart2 = new GhostPart(gameObject.transform.Find("Parts/CarTireFront").gameObject, new Vector3(0f, 0f, -23f), "frontWheel", false);
		GhostPart ghostPart3 = new GhostPart(gameObject.transform.Find("Parts/CarTireFront").gameObject, new Vector3(0f, 0f, 23f), "frontWheel", false);
		ghostPart3.prefab.transform.rotation.eulerAngles.Set(0f, 180f, 0f);
		GhostPart ghostPart4 = new GhostPart(gameObject.transform.Find("Parts/CarTireRear").gameObject, new Vector3(0f, 0f, -23f), "rearWheel", false);
		GhostPart ghostPart5 = new GhostPart(gameObject.transform.Find("Parts/CarTireRear").gameObject, new Vector3(0f, 0f, 23f), "rearWheel", false);
		ghostPart5.prefab.transform.rotation.eulerAngles.Set(0f, 180f, 0f);
		GhostBoostEffect ghostBoostEffect = new GhostBoostEffect(ResourceManager.GetGameObject(RESOURCE.EffectBoostOffroadCarFrontGhost_GameObject), Vector3.forward * -24f, "frontWheel");
		GhostBoostEffect ghostBoostEffect2 = new GhostBoostEffect(ResourceManager.GetGameObject(RESOURCE.EffectBoostOffroadCarBackGhost_GameObject), Vector3.forward * -24f, "rearWheel");
		string text = string.Empty;
		string text2 = string.Empty;
		if (_ghost.m_ghost.m_vehicleVisualItems != null && _ghost.m_ghost.m_vehicleVisualItems.Count > 0)
		{
			PsCustomisationData customData = PsCustomisationManager.GetCustomData(_ghost.m_ghost.m_vehicleVisualItems);
			PsCustomisationItem psCustomisationItem = customData.GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
			if (psCustomisationItem != null)
			{
				text = psCustomisationItem.m_identifier;
			}
			psCustomisationItem = customData.GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.TRAIL);
			if (psCustomisationItem != null)
			{
				text2 = psCustomisationItem.m_identifier;
			}
		}
		GameObject gameObject2 = Object.Instantiate<GameObject>(PsCustomisationManager.GetHatPrefabByIdentifier(text), Vector3.zero, Quaternion.identity);
		gameObject2.name = "GhostHat";
		PrefabS.SetCameraLayer(gameObject2, 8);
		Renderer[] componentsInChildren = gameObject2.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (!(componentsInChildren[i] is ParticleSystemRenderer))
			{
				Material[] sharedMaterials = componentsInChildren[i].sharedMaterials;
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					Material material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.DirtBikeGhostTex_Material));
					material.SetTexture("_MainTexture", sharedMaterials[j].GetTexture("_MainTex"));
					sharedMaterials[j] = material;
				}
				componentsInChildren[i].sharedMaterials = sharedMaterials;
			}
		}
		GhostPart ghostPart6 = new GhostPart(gameObject2, new Vector3(0f, 24.2f, 0f), "chassis", false);
		GhostPart[] array = new GhostPart[] { ghostPart, ghostPart2, ghostPart3, ghostPart4, ghostPart5, ghostPart6 };
		string text3 = text2;
		GhostBoostEffect[] array2 = new GhostBoostEffect[] { ghostBoostEffect, ghostBoostEffect2 };
		_ghost.CreateVisuals(array, text3, array2, true, _showAtStart, _identifierColor);
		Object.Destroy(gameObject2);
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x00053C28 File Offset: 0x00052028
	public virtual void CreateMotorcyclePlaybackGhostEntity(GhostObject _ghost, bool _showAtStart = false, string _identifierColor = "")
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.DirtBikeGhostNewPrefab_GameObject);
		GhostPart ghostPart = new GhostPart(gameObject.transform.Find("DirtBike").gameObject, new Vector3(0f, -3f, 0f), "chassis", false);
		GhostPart ghostPart2 = new GhostPart(gameObject.transform.Find("Parts/Eturengas").gameObject, Vector3.zero, "frontWheel", false);
		GhostPart ghostPart3 = new GhostPart(gameObject.transform.Find("Parts/Takarengas").gameObject, Vector3.zero, "rearWheel", false);
		GhostBoostEffect ghostBoostEffect = new GhostBoostEffect(ResourceManager.GetGameObject(RESOURCE.EffectBoostMotorcycleFrontGhost_GameObject), Vector3.forward * -0.8f, "frontWheel");
		GhostBoostEffect ghostBoostEffect2 = new GhostBoostEffect(ResourceManager.GetGameObject(RESOURCE.EffectBoostMotorcycleBackGhost_GameObject), Vector3.forward * -0.8f, "rearWheel");
		string text = string.Empty;
		string text2 = string.Empty;
		if (_ghost.m_ghost.m_vehicleVisualItems != null && _ghost.m_ghost.m_vehicleVisualItems.Count > 0)
		{
			PsCustomisationData customData = PsCustomisationManager.GetCustomData(_ghost.m_ghost.m_vehicleVisualItems);
			PsCustomisationItem psCustomisationItem = customData.GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
			if (psCustomisationItem != null)
			{
				text = psCustomisationItem.m_identifier;
			}
			psCustomisationItem = customData.GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.TRAIL);
			if (psCustomisationItem != null)
			{
				text2 = psCustomisationItem.m_identifier;
			}
		}
		GameObject gameObject2 = Object.Instantiate<GameObject>(PsCustomisationManager.GetHatPrefabByIdentifier(text), Vector3.zero, Quaternion.identity);
		gameObject2.name = "GhostHat";
		PrefabS.SetCameraLayer(gameObject2, 8);
		Renderer[] componentsInChildren = gameObject2.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (!(componentsInChildren[i] is ParticleSystemRenderer))
			{
				Material[] sharedMaterials = componentsInChildren[i].sharedMaterials;
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					Material material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.DirtBikeGhostTex_Material));
					material.SetTexture("_MainTexture", sharedMaterials[j].GetTexture("_MainTex"));
					sharedMaterials[j] = material;
				}
				componentsInChildren[i].sharedMaterials = sharedMaterials;
			}
		}
		GhostPart ghostPart4 = new GhostPart(gameObject2, new Vector3(7.7f, 27.3f, 0f), "chassis", false);
		GhostPart[] array = new GhostPart[] { ghostPart, ghostPart2, ghostPart3, ghostPart4 };
		string text3 = text2;
		GhostBoostEffect[] array2 = new GhostBoostEffect[] { ghostBoostEffect, ghostBoostEffect2 };
		_ghost.CreateVisuals(array, text3, array2, true, _showAtStart, _identifierColor);
		Object.Destroy(gameObject2);
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x00053EF0 File Offset: 0x000522F0
	public void SetCoinStreakStyle(CoinStreakStyle _style)
	{
		this.m_coinStreakStyle = _style;
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00053EF9 File Offset: 0x000522F9
	public bool ShowDiamondsInHUD()
	{
		return this.m_coinStreakStyle == CoinStreakStyle.ROYAL || this.m_coinStreakStyle == CoinStreakStyle.ALL_DIAMONDS;
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x00053F14 File Offset: 0x00052314
	public void GenerateCollectableCoins()
	{
		if (this.m_playbackGhosts == null || this.m_playbackGhosts.Count == 0)
		{
			return;
		}
		global::Ghost ghost = this.m_playbackGhosts[this.m_playbackGhosts.Count - 1].m_ghost;
		this.m_streaks = new List<PsStreak>();
		Random.seed = (int)Main.m_gameTimeSinceAppStarted;
		int num = 280;
		int num2 = 0;
		float pathWorldDistance = ghost.GetPathWorldDistance("chassis");
		int num3 = ToolBox.limitBetween((int)(pathWorldDistance / 800f), 1, int.MaxValue);
		int num4 = ToolBox.limitBetween(Random.Range(num3 / 2, num3 + 1), 1, int.MaxValue);
		int num5 = 3;
		int num6 = 8;
		float num7 = 45f;
		float num8 = num7 * num7 * 0.8f;
		if (this.m_coinStreakStyle == CoinStreakStyle.ALL_GOLD || this.m_coinStreakStyle == CoinStreakStyle.GOLD_MANIAC)
		{
			num5 = 3;
			num6 = 6;
		}
		if (this.m_coinStreakStyle == CoinStreakStyle.GOLD_HOARDER)
		{
			num5 = 5;
			num6 = 5;
		}
		else if (this.m_coinStreakStyle == CoinStreakStyle.ROYAL)
		{
			num5 = 5;
			num6 = 5;
		}
		else if (this.m_coinStreakStyle == CoinStreakStyle.COPPER_AND_GOLD)
		{
			num5 = 5;
			num6 = 5;
		}
		float num9 = 640000f;
		int num10 = 14;
		List<Entity> entitiesByTag = EntityManager.GetEntitiesByTag("CollectibleStar");
		List<UnitC> list = new List<UnitC>();
		for (int i = 0; i < entitiesByTag.Count; i++)
		{
			list.Add(EntityManager.GetComponentByEntity((ComponentType)30, entitiesByTag[i]) as UnitC);
		}
		GhostNode ghostNode = ghost.m_nodes["chassis"] as GhostNode;
		int num11 = ghost.m_keyframeCount / num4;
		int num12 = 0;
		if (num11 < 1)
		{
			return;
		}
		for (int j = 0; j < num4; j++)
		{
			int num13 = Random.Range(num5, num6 + 1);
			if (num2 + num13 > num)
			{
				break;
			}
			int num14 = num12 + Random.Range(0, num11 + 1);
			num14 = ToolBox.limitBetween(num14, 0, ghost.m_keyframeCount - 1);
			List<PsCoin> list2 = new List<PsCoin>();
			PsStreak psStreak = new PsStreak();
			for (int k = 0; k < num13; k++)
			{
				Vector3 vector;
				vector..ctor(0f, (float)Random.Range(20, 40), 0f);
				vector = Quaternion.AngleAxis(ghostNode.GetKeyFrameRotation(num14), Vector3.forward) * vector;
				Vector3 vector2 = new Vector2(vector.x, vector.y);
				Vector3 vector3 = ghostNode.GetKeyFramePos(num14) + vector2;
				bool flag = true;
				int num15 = 0;
				foreach (PsCoin psCoin in list2)
				{
					Vector3 vector4 = psCoin.m_pos;
					float sqrMagnitude = (vector3 - vector4).sqrMagnitude;
					if (sqrMagnitude < num9)
					{
						num15++;
						if (num15 > num10)
						{
							flag = false;
							break;
						}
					}
					if (sqrMagnitude < num8)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					foreach (PsStreak psStreak2 in this.m_streaks)
					{
						foreach (PsCoin psCoin2 in psStreak2.m_coins)
						{
							Vector3 vector5 = psCoin2.m_pos;
							float sqrMagnitude2 = (vector3 - vector5).sqrMagnitude;
							if (sqrMagnitude2 < num9)
							{
								num15++;
								if (num15 > num10)
								{
									flag = false;
									break;
								}
							}
							if (sqrMagnitude2 < num8)
							{
								flag = false;
								break;
							}
						}
						if (!flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					foreach (UnitC unitC in list)
					{
						CollectibleStar collectibleStar = unitC.m_unit as CollectibleStar;
						if ((vector3 - collectibleStar.m_pos).sqrMagnitude < num8 * 3f)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag && (vector3 - PsState.m_activeMinigame.m_playerUnit.GetUnitCenterPosition()).sqrMagnitude < num8 * 5f)
				{
					flag = false;
				}
				if (flag)
				{
					PsCoin psCoin3 = new PsCoin(vector3, psStreak);
					switch (this.m_coinStreakStyle)
					{
					case CoinStreakStyle.COPPER_AND_GOLD:
						if (list2.Count % 2 == 0)
						{
							psCoin3.SetType(CoinType.COPPER);
						}
						else
						{
							psCoin3.SetType(CoinType.GOLD);
						}
						break;
					case CoinStreakStyle.ALL_GOLD:
						psCoin3.SetType(CoinType.GOLD);
						break;
					case CoinStreakStyle.GOLD_HOARDER:
						if (list2.Count % 2 == 0)
						{
							psCoin3.SetType(CoinType.GOLD);
						}
						else
						{
							psCoin3.SetType(CoinType.SUPER_GOLD);
						}
						break;
					case CoinStreakStyle.GOLD_MANIAC:
						psCoin3.SetType(CoinType.SUPER_GOLD);
						break;
					case CoinStreakStyle.ROYAL:
						if (list2.Count % 2 == 1)
						{
							psCoin3.SetType(CoinType.DIAMOND);
						}
						else
						{
							psCoin3.SetType(CoinType.SUPER_GOLD);
						}
						break;
					case CoinStreakStyle.ALL_DIAMONDS:
						psCoin3.SetType(CoinType.DIAMOND);
						break;
					case CoinStreakStyle.ALL_SHARDS:
						psCoin3.SetType(CoinType.SHARD);
						break;
					}
					list2.Add(psCoin3);
					num2++;
				}
				num14 = ghost.GetNextKeyframe("chassis", num14, num7);
				if (num14 >= ghost.m_keyframeCount - 1)
				{
					break;
				}
			}
			psStreak.SetCoins(list2);
			this.m_streaks.Add(psStreak);
			num12 += num11;
		}
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00054528 File Offset: 0x00052928
	public void PlaceCoins()
	{
		if (this.m_streaks == null)
		{
			return;
		}
		for (int i = 0; i < this.m_streaks.Count; i++)
		{
			for (int j = 0; j < this.m_streaks[i].m_coins.Count; j++)
			{
				PsCoin psCoin = this.m_streaks[i].m_coins[j];
				if (!psCoin.m_collected)
				{
					new CollectibleCoin(psCoin);
				}
			}
		}
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x000545B0 File Offset: 0x000529B0
	public void CheckAchievementsOnWin()
	{
		if (this.m_gameLoop.GetDifficulty() == PsGameDifficulty.Tricky)
		{
			PsAchievementManager.Complete("finishHardLevel");
			if (PsState.m_activeMinigame.m_gameStartCount == 1)
			{
				PsAchievementManager.Complete("finishHardLevelFirstTry");
			}
		}
		else if (this.m_gameLoop.GetDifficulty() == PsGameDifficulty.Insane)
		{
			PsAchievementManager.Complete("dealWithIt");
			PsAchievementManager.IncrementProgress("epicDealWithIt", 1);
		}
		else if (this.m_gameLoop.GetDifficulty() == PsGameDifficulty.Impossible)
		{
			PsAchievementManager.Complete("impossibru");
		}
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0005463D File Offset: 0x00052A3D
	public void CheckAchievementsOnEnd()
	{
		if (this.m_gameLoop.m_context == PsMinigameContext.Fresh)
		{
			PsAchievementManager.IncrementProgress("playRateThirty", 1);
		}
		else if (this.m_gameLoop.m_context == PsMinigameContext.Social)
		{
			PsAchievementManager.Complete("testFriendLevels");
		}
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0005467B File Offset: 0x00052A7B
	public virtual void SendQuit()
	{
	}

	// Token: 0x040007CB RID: 1995
	public Entity m_ghostLoaderEntity;

	// Token: 0x040007CC RID: 1996
	public bool m_waitForHighscoreAndGhost;

	// Token: 0x040007CD RID: 1997
	public bool m_waitForNextGhost;

	// Token: 0x040007CE RID: 1998
	public PsGameMode m_gameMode;

	// Token: 0x040007CF RID: 1999
	public PsGameLoop m_gameLoop;

	// Token: 0x040007D0 RID: 2000
	public List<GhostObject> m_playbackGhosts;

	// Token: 0x040007D1 RID: 2001
	public global::Ghost m_recordingGhost;

	// Token: 0x040007D2 RID: 2002
	public int m_ghostRetryCounter;

	// Token: 0x040007D3 RID: 2003
	public bool m_ghostLoadComplete;

	// Token: 0x040007D4 RID: 2004
	public CoinStreakStyle m_coinStreakStyle;

	// Token: 0x040007D5 RID: 2005
	public List<PsStreak> m_streaks;

	// Token: 0x040007D6 RID: 2006
	public List<int> m_ghostScores;

	// Token: 0x040007D7 RID: 2007
	public List<GhostData> m_allGhosts;
}
