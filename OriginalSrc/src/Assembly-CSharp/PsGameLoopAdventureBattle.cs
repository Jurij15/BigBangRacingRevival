using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using Server;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class PsGameLoopAdventureBattle : PsGameLoopAdventure
{
	// Token: 0x06000666 RID: 1638 RVA: 0x0004B954 File Offset: 0x00049D54
	public PsGameLoopAdventureBattle(PsMinigameMetaData _data)
		: this(PsMinigameContext.Block, _data.id, new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any), null, -1, -1, _data.score, false, string.Empty, 0, 0, 0, string.Empty, 0.0)
	{
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0004B998 File Offset: 0x00049D98
	public PsGameLoopAdventureBattle(string _minigameId, MinigameSearchParametres _searchParametres = null, PsPlanetPath _path = null, int _id = -1, int _levelNumber = -1, int _score = 0, bool _unlocked = false, string[] _medalTimes = null)
	{
		this.m_collectedPieceGameTick = new float[3];
		base..ctor(PsMinigameContext.Block, _minigameId, _searchParametres, _path, _id, _levelNumber, _score, _unlocked, _medalTimes);
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0004B9C8 File Offset: 0x00049DC8
	public PsGameLoopAdventureBattle(PsMinigameContext _context, string _minigameId, MinigameSearchParametres _searchParametres, PsPlanetPath _path, int _id, int _levelNumber, int _score, bool _unlocked, string _unlockedProgression, int _sessionCount = 0, int _tryCount = 0, int _reachedGoalCount = 0, string _powerFuelList = "", double _firstTryDate = 0.0)
	{
		this.m_collectedPieceGameTick = new float[3];
		base..ctor(_context, _minigameId, _searchParametres, _path, _id, _levelNumber, _score, _unlocked, null);
		this.m_unlockedProgression = _unlockedProgression;
		this.m_sessionCount = _sessionCount;
		this.m_tryCount = _tryCount;
		this.m_reachedGoalCount = _reachedGoalCount;
		this.m_firstTryTimeStamp = _firstTryDate;
		this.ParsePowerFuels(_powerFuelList);
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0004BA28 File Offset: 0x00049E28
	private void ParsePowerFuels(string _powerFuelList)
	{
		if (this.m_purchasedPowerFuels == null)
		{
			this.m_purchasedPowerFuels = new List<int>();
		}
		if (!string.IsNullOrEmpty(_powerFuelList))
		{
			List<object> list = Json.Deserialize(_powerFuelList) as List<object>;
			if (list != null)
			{
				foreach (object obj in list)
				{
					int num = Convert.ToInt32(obj);
					if (!this.m_purchasedPowerFuels.Contains(num))
					{
						this.m_purchasedPowerFuels.Add(Convert.ToInt32(obj));
					}
				}
			}
		}
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0004BAD4 File Offset: 0x00049ED4
	public float GetTimeSinceStartInHours()
	{
		if (this.m_firstTryTimeStamp == 0.0)
		{
			return 0f;
		}
		double num = Main.m_EPOCHSeconds - this.m_firstTryTimeStamp;
		return (float)(num / 60.0 / 60.0);
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0004BB20 File Offset: 0x00049F20
	public override void StartLoop()
	{
		if (this.m_context == PsMinigameContext.Block && this.m_path != null && this.m_path.GetLastLevel() != this)
		{
			if (this.m_nodeId == this.m_path.m_currentNodeId && this.m_path.GetLastBlockId() > this.m_nodeId)
			{
				base.m_unlocked = true;
				this.m_backAtMenu = false;
				this.m_path.m_currentNodeId = this.m_nodeId + 1;
				this.SetAsActiveLoop();
				this.SaveLoop();
				this.BackAtMenu();
				return;
			}
			return;
		}
		else
		{
			this.m_winFirstTime = false;
			if (PsState.m_activeGameLoop != null)
			{
				return;
			}
			SoundS.PlaySingleShot("/UI/DomeSelect_Boss", Vector3.zero, 1f);
			this.m_backAtMenu = false;
			if (this.m_firstTryTimeStamp == 0.0)
			{
				this.m_firstTryTimeStamp = Main.m_EPOCHSeconds;
			}
			string session = PlayerPrefsX.GetSession();
			if (string.IsNullOrEmpty(this.m_lastSession) || this.m_lastSession != session)
			{
				this.m_sessionCount++;
				this.m_lastSession = session;
			}
			this.LoopUnlocked();
			return;
		}
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0004BC48 File Offset: 0x0004A048
	public override bool CheckIfFixNeeded()
	{
		return this.m_nodeId < this.m_path.m_currentNodeId && this == this.m_path.GetLastLevel() && base.m_unlocked;
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0004BC7F File Offset: 0x0004A07F
	public override void CreateGameMode()
	{
		this.m_gameMode = new PsGameModeAdventureBattle(this);
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0004BC8D File Offset: 0x0004A08D
	public override void CollectibleCollected()
	{
		this.m_collectedPieceGameTick[PsState.m_activeMinigame.m_collectedStars] = PsState.m_activeMinigame.m_gameTicks;
		base.CollectibleCollected();
		(this.m_gameMode as PsGameModeAdventureBattle).CollectibleCollected();
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0004BCC0 File Offset: 0x0004A0C0
	public bool CheckWin()
	{
		return PsState.m_activeMinigame.m_collectedStars == 3 && ((this.m_gameMode.m_playbackGhosts != null && this.m_gameMode.m_playbackGhosts.Count > 0 && !this.m_gameMode.m_playbackGhosts[0].ReachedGoal()) || this.m_gameMode.m_playbackGhosts == null || this.m_gameMode.m_playbackGhosts.Count < 1);
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0004BD4A File Offset: 0x0004A14A
	public override void CheckWinCondition()
	{
		this.m_reachedGoalCount++;
		if (this.CheckWin())
		{
			base.CheckWinCondition();
		}
		else
		{
			this.SaveLoop();
			this.m_gameMode.ShowLoseUI();
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0004BD81 File Offset: 0x0004A181
	public override void StartMinigame()
	{
		this.m_randomRuntimeHandicap = BossBattles.GetRandomRuntimeHandicap();
		this.m_tryCount++;
		this.SaveLoop();
		base.StartMinigame();
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0004BDA8 File Offset: 0x0004A1A8
	public override void BeginAdventure()
	{
		this.m_collectedPieceGameTick = new float[3];
		this.m_playerTimeScale = false;
		(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_powerUp = this.m_startPowerUp;
		this.m_startPowerUp = null;
		EntityManager.SetActivityOfEntitiesWithTag("GTAG_COIN", true, true, true, true, false, false);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new GameBeginAdventureBattleState());
		this.m_gameMode.StartMinigame();
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0004BE20 File Offset: 0x0004A220
	protected override HttpC SearchMetadata()
	{
		HttpC httpC = AdventureBattle.SearchMinigame(PsState.GetCurrentVehicleType(false).ToString(), new Action<PsMinigameMetaData[]>(this.SearchOnlyMetaDataSUCCESS), new Action<HttpC>(this.SearchOnlyMetaDataFAILED), null);
		httpC.objectData = PsState.GetCurrentVehicleType(false).ToString();
		return httpC;
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0004BE6C File Offset: 0x0004A26C
	protected override HttpC SearchMinigame()
	{
		HttpC httpC = AdventureBattle.SearchMinigame(PsState.GetCurrentVehicleType(false).ToString(), new Action<PsMinigameMetaData[]>(this.SearchMetaDataSUCCESS), new Action<HttpC>(this.SearchMetaDataFAILED), null);
		httpC.objectData = PsState.GetCurrentVehicleType(false).ToString();
		return httpC;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0004BEB8 File Offset: 0x0004A2B8
	protected new virtual void SearchMetaDataFAILED(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), () => AdventureBattle.SearchMinigame(_c.objectData.ToString(), new Action<PsMinigameMetaData[]>(this.SearchMetaDataSUCCESS), new Action<HttpC>(this.SearchMetaDataFAILED), null), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0004BF10 File Offset: 0x0004A310
	protected new void SearchOnlyMetaDataFAILED(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => AdventureBattle.SearchMinigame(_c.objectData.ToString(), new Action<PsMinigameMetaData[]>(this.SearchOnlyMetaDataSUCCESS), new Action<HttpC>(this.SearchOnlyMetaDataFAILED), null), null);
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0004BF58 File Offset: 0x0004A358
	public override void SetUnlockedProgressionName(string _name)
	{
		this.m_unlockedProgression = _name;
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0004BF61 File Offset: 0x0004A361
	public override string GetUnlockedProgressionName()
	{
		return this.m_unlockedProgression;
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0004BF6C File Offset: 0x0004A36C
	public override void SaveProgression(bool _save = true)
	{
		Debug.Log("Saving progression...", null);
		if (PsState.m_inLoginFlow)
		{
			new PsServerQueueFlow(delegate
			{
				this.SaveProgression(_save);
			}, null, null);
			return;
		}
		bool flag = false;
		if ((this.m_nodeId == this.m_path.m_currentNodeId && _save) || this.m_path.GetLastLevel() == this)
		{
			this.m_path.m_currentNodeId = this.m_nodeId + 1;
			flag = true;
		}
		if (flag)
		{
			if (this.m_nodeId == this.m_path.GetLastBlockId())
			{
				Debug.Log("GIVE NEXT STRIP", null);
				this.m_addedNodes = this.m_planet.GiveNextStrip(this);
				PsGameLoop nodeInfo = this.m_path.GetNodeInfo(this.m_path.GetNextBlockId(this.m_nodeId));
				base.IncreaseItemLevel(nodeInfo.GetUnlockedProgressionName());
			}
			if (this.m_path.GetPathType() == PsPlanetPathType.main)
			{
				Hashtable hashtable = ClientTools.GenerateProgressionPathJson(this.m_addedNodes, this.m_path.m_currentNodeId, this.m_path.m_planet, true, true, true, null);
				PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable, this.m_path.m_planet, true);
			}
			else
			{
				Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_path);
				PsMetagameManager.SaveProgression(hashtable2, this.m_path.m_planet, false);
			}
		}
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0004C0DC File Offset: 0x0004A4DC
	private void SaveLoop()
	{
		if (this.m_path.GetPathType() == PsPlanetPathType.main)
		{
			Hashtable hashtable = ClientTools.GenerateProgressionPathJson(this, this.m_path.m_currentNodeId, true, true, true);
			PsMetagameManager.SaveProgression(hashtable, this.m_path.m_planet, false);
		}
		else
		{
			Hashtable hashtable2 = ClientTools.GenerateProgressionPathJson(this.m_path);
			PsMetagameManager.SaveProgression(hashtable2, this.m_path.m_planet, false);
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0004C143 File Offset: 0x0004A543
	public override void BackAtMenu()
	{
		if (!this.m_backAtMenu && !this.m_released)
		{
			this.m_exitingMinigame = true;
			this.m_backAtMenu = true;
			this.StartLoopClaimedDialogueFlow();
		}
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0004C170 File Offset: 0x0004A570
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

	// Token: 0x0400076B RID: 1899
	private string m_unlockedProgression;

	// Token: 0x0400076C RID: 1900
	public float[] m_collectedPieceGameTick;

	// Token: 0x0400076D RID: 1901
	public PsPowerUp m_startPowerUp;

	// Token: 0x0400076E RID: 1902
	public string m_lastSession;

	// Token: 0x0400076F RID: 1903
	public bool m_playerTimeScale;

	// Token: 0x04000770 RID: 1904
	public int m_sessionCount;

	// Token: 0x04000771 RID: 1905
	public int m_tryCount;

	// Token: 0x04000772 RID: 1906
	public int m_reachedGoalCount;

	// Token: 0x04000773 RID: 1907
	public double m_firstTryTimeStamp;

	// Token: 0x04000774 RID: 1908
	public List<int> m_purchasedPowerFuels;

	// Token: 0x04000775 RID: 1909
	public float m_randomRuntimeHandicap;

	// Token: 0x04000776 RID: 1910
	private List<PsGameLoop> m_addedNodes;
}
