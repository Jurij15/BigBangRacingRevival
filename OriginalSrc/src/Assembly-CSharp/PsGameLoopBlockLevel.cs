using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class PsGameLoopBlockLevel : PsGameLoop
{
	// Token: 0x0600067E RID: 1662 RVA: 0x0004C2DC File Offset: 0x0004A6DC
	public PsGameLoopBlockLevel(PsMinigameMetaData _data)
		: this(PsMinigameContext.Block, _data.id, PsPathBlockType.Garage, GachaType.SILVER, 0, string.Empty, string.Empty, null, -1, _data.score, 0, 0, 0, 0, false, string.Empty)
	{
		this.m_minigameMetaData = _data;
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0004C31C File Offset: 0x0004A71C
	public PsGameLoopBlockLevel(PsMinigameContext _context, string _minigameId, PsPathBlockType _blockType, GachaType _gachaType, int _requiredStars, string _unlockedProgression, string _willUnlockProgression, PsPlanetPath _path, int _id, int _score, int _blockCoins, int _blockDiamonds, int _blockBolts, int _blockKeys, bool _unlocked, string _rewardCue)
		: base(_context, _minigameId, _path, _id, -1, _score, _unlocked, null)
	{
		this.m_blockType = _blockType;
		this.m_gachaType = _gachaType;
		this.m_requiredStars = _requiredStars;
		this.m_unlockedProgression = _unlockedProgression;
		this.m_willUnlockProgression = _willUnlockProgression;
		this.m_blockCoins = _blockCoins;
		this.m_blockDiamonds = _blockDiamonds;
		this.m_blockKeys = _blockKeys;
		this.m_rewardCue = _rewardCue;
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0004C382 File Offset: 0x0004A782
	public override void SetUnlockedProgressionName(string _name)
	{
		this.m_unlockedProgression = _name;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0004C38B File Offset: 0x0004A78B
	public override string GetUnlockedProgressionName()
	{
		return this.m_unlockedProgression;
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0004C393 File Offset: 0x0004A793
	public override string GetWillUnlockProgressionName()
	{
		return this.m_willUnlockProgression;
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0004C39C File Offset: 0x0004A79C
	public override void StartLoop()
	{
		this.m_loopStartLoginTime = PlayerPrefsX.GetLastLoginTime();
		this.m_loopStartSessionId = PlayerPrefsX.GetSession();
		this.m_winFirstTime = false;
		if (PsState.m_activeGameLoop != null)
		{
			return;
		}
		if (Input.GetKey(304) || (PsState.m_adminMode && PsState.m_adminModeSkipping))
		{
			this.m_skip = true;
		}
		else
		{
			this.m_skip = false;
		}
		if (this.m_nodeId == this.m_path.m_currentNodeId)
		{
			this.SetAsActiveLoop();
			this.LoopUnlocked();
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0004C429 File Offset: 0x0004A829
	public override void LoadMinigameMetaData(Action _callback = null)
	{
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0004C42C File Offset: 0x0004A82C
	public override float GiveCheckpointReward()
	{
		return 0f;
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0004C440 File Offset: 0x0004A840
	public void BackUpRewards()
	{
		if (this.m_blockCoins > 0)
		{
			PsMetagameManager.m_playerStats.coins += this.m_blockCoins;
		}
		if (this.m_blockDiamonds > 0)
		{
			PsMetagameManager.m_playerStats.diamonds += this.m_blockDiamonds;
		}
		if (this.m_blockKeys > 0)
		{
			PsMetagameManager.m_playerStats.mcBoosters += this.m_blockKeys;
		}
		PsMetagameManager.m_playerStats.updated = true;
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0004C4C4 File Offset: 0x0004A8C4
	public override void LoopUnlocked()
	{
		this.m_backAtMenu = true;
		if (this.m_node != null)
		{
			this.m_node.m_hideUI = true;
			TimerS.AddComponent(EntityManager.AddEntity(), "Timer", 0.3f, 0f, true, new TimerComponentDelegate(this.LoopUnlockedTask));
		}
		else
		{
			this.LoopUnlockedTask(null);
		}
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0004C524 File Offset: 0x0004A924
	public void LoopUnlockedTask(TimerC _c)
	{
		this.m_addedNodes = new List<PsGameLoop>();
		if (!string.IsNullOrEmpty(this.m_minigameId) && !this.m_skip)
		{
			if (this.m_nodeId == this.m_path.m_currentNodeId)
			{
				float num = this.GiveCheckpointReward();
				new PsMenuDialogueFlow(this, PsNodeEventTrigger.LoopStart, num, delegate
				{
					PsLoadingScreen.FadeIn(delegate
					{
						this.LoadMinigame();
					});
				}, true);
			}
			else if (this.m_nodeId < this.m_path.m_currentNodeId)
			{
				PsLoadingScreen.FadeIn(delegate
				{
					this.LoadMinigame();
				});
			}
		}
		else if (this.m_nodeId == this.m_path.m_currentNodeId)
		{
			float num2 = this.GiveCheckpointReward();
			base.m_unlocked = true;
			new PsMenuDialogueFlow(this, PsNodeEventTrigger.LoopStart, num2, delegate
			{
				if (this.m_blockType == PsPathBlockType.Chest)
				{
					int num3 = -1;
					if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
					{
						num3 = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.MOTO_PATH);
					}
					else if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar))
					{
						num3 = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.CAR_PATH);
					}
					if (num3 >= 0)
					{
						PsGachaManager.m_lastOpenedGacha = PsGachaManager.m_gachas[num3].m_gachaType;
						PsGachaManager.m_lastGachaRewards = PsGachaManager.OpenGacha(PsGachaManager.m_gachas[num3], -1, true);
						Debug.Log("E_Test Path chest type: " + PsGachaManager.m_lastOpenedGacha.ToString(), null);
						PsMetrics.ChestOpened("Path");
						FrbMetrics.ChestOpened("path");
						PsUIBaseState psUIBaseState = new PsUIBaseState(typeof(PsUICenterOpenGacha), null, null, null, false, InitialPage.Center);
						this.SaveProgression(base.m_unlocked);
						psUIBaseState.SetAction("Exit", delegate
						{
							this.m_backAtMenu = false;
							this.m_released = false;
							if (this.m_node != null)
							{
								this.m_node.SetClaimed();
							}
							Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
						});
						Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState);
					}
				}
				else
				{
					this.SaveProgression(base.m_unlocked);
				}
			}, true);
		}
		else
		{
			this.StartCancelled();
		}
		if (this.m_nodeId == this.m_path.m_currentNodeId && !base.m_unlocked && !string.IsNullOrEmpty(this.m_minigameId) && !this.m_skip)
		{
			base.m_unlocked = true;
			if (this.m_path.GetPathType() == PsPlanetPathType.main)
			{
				PsMetagameManager.m_playerStats.stars = 0;
				this.m_addedNodes.Add(this);
				Hashtable hashtable = ClientTools.GenerateProgressionPathJson(this, this.m_path.m_currentNodeId, true, true, true);
				PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable, this.m_path.m_planet, false);
			}
			else
			{
				Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_path);
				PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable2, this.m_path.m_planet, false);
			}
		}
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0004C6C0 File Offset: 0x0004AAC0
	public override void StartCancelled()
	{
		PsMetagameManager.ShowResources(null, true, true, true, true, 0.015f, true, false, false);
		this.SetActiveLoopNull();
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0004C6E8 File Offset: 0x0004AAE8
	public override void LoadMinigame()
	{
		if (this.m_loadingMetaData)
		{
			Debug.LogWarning("ALREADY LOADING MINIGAME META DATA. ADDING TO CALLBACK");
			if (this.m_loadMetadataCallback != null)
			{
				this.m_loadMetadataCallback = (Action)Delegate.Combine(this.m_loadMetadataCallback, new Action(this.LoadMinigame));
			}
			else
			{
				this.m_loadMetadataCallback = new Action(this.LoadMinigame);
			}
			return;
		}
		base.LoadMinigame();
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0004C757 File Offset: 0x0004AB57
	protected override void LoadMetaDataSUCCESS(PsMinigameMetaData _minigameMetaData)
	{
		base.LoadMetaDataSUCCESS(_minigameMetaData);
		this.LoadMinigameBytes();
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0004C766 File Offset: 0x0004AB66
	protected override void MinigameDownloadOk(byte[] _levelData)
	{
		base.MinigameDownloadOk(_levelData);
		Main.m_currentGame.m_sceneManager.ChangeScene(new GameScene("GameScene", null), null);
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0004C78C File Offset: 0x0004AB8C
	public override void EnterMinigame()
	{
		this.m_backAtMenu = false;
		PsGameLoop nodeInfo = this.m_path.GetNodeInfo(this.m_nodeId + 1);
		if (nodeInfo != null && nodeInfo.m_minigameMetaData == null && nodeInfo.m_nodeId == this.m_path.GetCurrentNodeInfo().m_nodeId)
		{
			nodeInfo.LoadMinigameMetaData(null);
		}
		CameraS.m_updateComponents = true;
		PsLoadingScreen.FadeOut(null);
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameEnter, 0.5f, delegate
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameStartState());
			PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, false, false, false);
			PsIngameMenu.OpenController(true);
		}, null);
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0004C81E File Offset: 0x0004AC1E
	public override void InitializeMinigame()
	{
		base.InitializeMinigame();
		this.SetPlaybackGhostAndCoins(true);
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0004C830 File Offset: 0x0004AC30
	public override void StartMinigame()
	{
		base.StartMinigame();
		if (PsState.m_activeMinigame.m_music != null)
		{
			SoundS.SetSoundParameter(PsState.m_activeMinigame.m_music, "MusicState", 1f);
		}
		PsMetagameManager.HideResources();
		PsState.m_activeMinigame.m_gameStarted = true;
		this.m_gameMode.CreatePlayMenu(new Action(this.RestartMinigame), new Action(this.PauseMinigame));
		Random.seed = (int)(Main.m_gameTimeSinceAppStarted * 60.0);
		PsState.m_physicsPaused = false;
		PsState.m_activeMinigame.m_gameOn = true;
		PsState.m_activeMinigame.m_gameStartCount++;
		PsState.m_activeMinigame.m_playerReachedGoal = false;
		this.CreatePlaybackGhosts();
		SoundS.PlaySingleShot("/InGame/GameStart", Vector3.zero, 1f);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GamePlayState());
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0004C916 File Offset: 0x0004AD16
	public override void CreateGameMode()
	{
		this.m_gameMode = new PsGameModeBlock(this);
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0004C924 File Offset: 0x0004AD24
	public override void PauseMinigame()
	{
		if (PsState.m_activeMinigame.m_gamePaused || !PsState.m_activeMinigame.m_gameStarted || PsState.m_activeMinigame.m_gameEnded)
		{
			return;
		}
		base.PauseMinigame();
		PsState.m_activeMinigame.SetGamePaused(true);
		base.PauseElapsedTimer();
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterPauseRace), typeof(PsUITopPauseRace), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Restart", new Action(this.RestartMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Resume", new Action(this.ResumeMinigame));
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0004C9F8 File Offset: 0x0004ADF8
	public override void CreateMinigame()
	{
		if (this.m_unlockedProgression.Equals("unlock_nitros"))
		{
			this.m_rentedVehicle = true;
			this.m_userHasSelectedVehicle = true;
			this.m_selectedVehicle = PsSelectedVehicle.CreatorVehicle;
			Vehicle vehicle = Activator.CreateInstance(Type.GetType(this.m_minigameMetaData.playerUnit), new string[0]) as Vehicle;
			PsState.m_activeGameLoop.m_rentUpgradeValues = vehicle.ParseUpgradeValues(this.m_minigameMetaData.creatorUpgrades);
			vehicle.Destroy();
		}
		base.CreateMinigame();
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0004CA78 File Offset: 0x0004AE78
	public override void ResumeMinigame()
	{
		base.ResumeMinigame();
		PsIngameMenu.CloseAll();
		this.m_gameMode.CreatePlayMenu(new Action(this.RestartMinigame), new Action(this.PauseMinigame));
		PsIngameMenu.OpenController(false);
		PsState.m_activeMinigame.SetGamePaused(false);
		base.ResumeElapsedTimer();
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0004CACC File Offset: 0x0004AECC
	public override void RestartMinigame()
	{
		base.RestartMinigame();
		PsIngameMenu.CloseAll();
		PsState.m_activeMinigame.SetGamePaused(false);
		base.ResumeElapsedTimer();
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		PsState.m_activeMinigame.m_groundNode.RevertGroundFromPlay();
		LevelManager.ResetCurrentLevel();
		this.InitializeMinigame();
		PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, false, false, false);
		PsIngameMenu.OpenController(true);
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0004CB38 File Offset: 0x0004AF38
	public override void LoseMinigame()
	{
		base.LoseMinigame();
		PsIngameMenu.CloseAll();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_gameOn = false;
		PsState.m_activeMinigame.m_gameDeathCount++;
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameLose, 1f, delegate
		{
			PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterLoseRace), typeof(PsUITopLoseRace), null, null, false, true, InitialPage.Center, false, false, false);
			PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.ExitMinigame));
			PsIngameMenu.m_popupMenu.SetAction("Restart", new Action(this.RestartMinigame));
		}, null);
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0004CB94 File Offset: 0x0004AF94
	public override void WinMinigame()
	{
		base.WinMinigame();
		PsIngameMenu.CloseAll();
		PsState.m_activeMinigame.m_gameEnded = true;
		PsState.m_activeMinigame.m_playerReachedGoal = true;
		PsState.m_activeMinigame.m_gameOn = false;
		this.SaveProgression(true);
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameWin, 1f, delegate
		{
			this.ExitMinigame();
		}, null);
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0004CBEE File Offset: 0x0004AFEE
	private void ExitMinigameAction()
	{
		new PsMinigameDialogueFlow(this, PsNodeEventTrigger.MinigameExit, 0f, new Action(this.ExitMinigame), null);
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0004CC0B File Offset: 0x0004B00B
	public override void ExitMinigame()
	{
		base.ExitMinigame();
		PsIngameMenu.CloseAll();
		Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLoadingScene(Color.black, true, 0.25f));
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0004CC42 File Offset: 0x0004B042
	public override void DestroyMinigame()
	{
		base.DestroyMinigame();
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		CameraS.RemoveAllTargetComponents();
		LevelManager.DestroyCurrentLevel();
		PsState.m_activeMinigame = null;
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0004CC64 File Offset: 0x0004B064
	public override void SaveProgression(bool _save = true)
	{
		Debug.LogError("Saving progression...");
		if (PsState.m_inLoginFlow)
		{
			new PsServerQueueFlow(delegate
			{
				this.SaveProgression(true);
			}, null, null);
			return;
		}
		if (this.m_nodeId == this.m_path.GetLastBlockId())
		{
			if (this.m_loopStartLoginTime != PlayerPrefsX.GetLastLoginTime() && this.m_loopStartSessionId == PlayerPrefsX.GetSession())
			{
				this.BackUpRewards();
			}
			Debug.Log("is last blockid", null);
			this.m_scoreBest = 0;
			this.m_path.m_currentNodeId = this.m_nodeId + 1;
			PsMetagameManager.m_playerStats.level++;
			if (this.m_path.GetPathType() == PsPlanetPathType.main)
			{
				Debug.Log("GIVE NEXT STRIP", null);
				this.m_addedNodes = this.m_planet.GiveNextStrip(this);
				PsGameLoop nodeInfo = this.m_path.GetNodeInfo(this.m_path.GetNextBlockId(this.m_nodeId));
				base.IncreaseItemLevel(nodeInfo.GetUnlockedProgressionName());
				if (_save)
				{
					Hashtable hashtable = ClientTools.GenerateProgressionPathJson(this.m_addedNodes, this.m_path.m_currentNodeId, this.m_path.m_planet, true, true, true, null);
					PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable, this.m_path.m_planet, true);
				}
			}
			else if (_save)
			{
				Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_path);
				PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable2, this.m_path.m_planet, false);
			}
		}
		else if (this.m_nodeId == this.m_path.m_currentNodeId)
		{
			if (this.m_loopStartLoginTime != PlayerPrefsX.GetLastLoginTime() && this.m_loopStartSessionId == PlayerPrefsX.GetSession())
			{
				this.BackUpRewards();
			}
			this.m_path.m_currentNodeId = this.m_nodeId + 1;
			PsMetagameManager.m_playerStats.level++;
			PsGameLoop nodeInfo2 = this.m_path.GetNodeInfo(this.m_path.GetNextBlockId(this.m_nodeId));
			base.IncreaseItemLevel(nodeInfo2.GetUnlockedProgressionName());
			if (this.m_path.GetPathType() == PsPlanetPathType.main)
			{
				Hashtable hashtable3 = ClientTools.GenerateProgressionPathJson(this, this.m_path.m_currentNodeId, true, true, true);
				PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable3, this.m_path.m_planet, true);
			}
			else
			{
				Hashtable hashtable4 = ClientTools.GenerateProgressionPathJson(this.m_path);
				PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable4, this.m_path.m_planet, false);
			}
		}
		if (this.m_skip || this.m_path.GetPathType() != PsPlanetPathType.main || (string.IsNullOrEmpty(this.m_minigameId) && this.m_blockType != PsPathBlockType.Chest))
		{
			this.StartLoopClaimedDialogueFlow();
		}
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0004CF1C File Offset: 0x0004B31C
	public override void BackAtMenu()
	{
		if (!this.m_backAtMenu && !this.m_released)
		{
			this.m_exitingMinigame = true;
			this.m_backAtMenu = true;
			this.StartLoopClaimedDialogueFlow();
		}
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x0004CF48 File Offset: 0x0004B348
	public void StartLoopClaimedDialogueFlow()
	{
		if (this.m_planet.GetIdentifier() == this.m_path.m_planet && this.m_path.GetPathType() == PsPlanetPathType.main && this.m_nodeId + 1 == this.m_path.m_currentNodeId && this.m_path.GetCurrentNodeInfo() != null && this.m_path.GetCurrentNodeInfo().m_keepNodeHidden)
		{
			new PsMenuDialogueFlow(this, PsNodeEventTrigger.LoopClaimed, 0f, delegate
			{
				this.m_planet.StartRevealSquence(this.m_addedNodes);
			}, true);
		}
		else
		{
			new PsMenuDialogueFlow(this, PsNodeEventTrigger.LoopClaimed, 0f, new Action(this.ReleaseLoop), true);
		}
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0004CFFC File Offset: 0x0004B3FC
	public override void ReleaseLoop()
	{
		PsMenuScene.m_lastState = null;
		if (this.m_released)
		{
			return;
		}
		base.ReleaseLoop();
		if (this.m_node != null)
		{
			this.m_node.RemoveHighlight();
		}
		new PsMenuDialogueFlow(this, PsNodeEventTrigger.LoopReleased, 0f, delegate
		{
			this.SetActiveLoopNull();
			this.ActivateCurrentLoop();
		}, true);
	}

	// Token: 0x04000777 RID: 1911
	public PsPathBlockType m_blockType;

	// Token: 0x04000778 RID: 1912
	public GachaType m_gachaType;

	// Token: 0x04000779 RID: 1913
	public int m_requiredStars;

	// Token: 0x0400077A RID: 1914
	public string m_unlockedProgression;

	// Token: 0x0400077B RID: 1915
	public string m_willUnlockProgression;

	// Token: 0x0400077C RID: 1916
	public int m_blockCoins;

	// Token: 0x0400077D RID: 1917
	public int m_blockDiamonds;

	// Token: 0x0400077E RID: 1918
	public int m_blockKeys;

	// Token: 0x0400077F RID: 1919
	public string m_rewardCue;

	// Token: 0x04000780 RID: 1920
	public bool m_skip;

	// Token: 0x04000781 RID: 1921
	private List<PsGameLoop> m_addedNodes;
}
