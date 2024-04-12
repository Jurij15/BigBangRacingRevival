using System;
using Server;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class PsGameModeAdventureBattle : PsGameModeAdventure
{
	// Token: 0x0600079F RID: 1951 RVA: 0x000552D1 File Offset: 0x000536D1
	protected PsGameModeAdventureBattle(PsGameMode _gameMode, PsGameLoop _info)
		: base(_gameMode, _info)
	{
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x000552DB File Offset: 0x000536DB
	public PsGameModeAdventureBattle(PsGameLoop _info)
		: base(PsGameMode.StarCollect, _info)
	{
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x000552E8 File Offset: 0x000536E8
	public override void CreateMusic()
	{
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		minigame.m_music = SoundS.AddComponent(minigame.m_environmentTC, PsFMODManager.GetMusic("BossMusic"), 1f, true);
		SoundS.PlaySound(minigame.m_music, true);
		SoundS.SetSoundParameter(minigame.m_music, "MusicState", 0f);
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00055344 File Offset: 0x00053744
	public override void CreateEnvironment()
	{
		base.CreateEnvironment();
		Transform transform = null;
		Transform[] componentsInChildren = PsState.m_activeMinigame.m_backgroundPrefab.p_gameObject.GetComponentsInChildren<Transform>();
		foreach (Transform transform2 in componentsInChildren)
		{
			if (transform2.name == "skybox")
			{
				transform = transform2;
				break;
			}
		}
		if (transform != null)
		{
			Renderer component = transform.GetComponent<Renderer>();
			if (component != null)
			{
				component.material.mainTexture = ResourceManager.GetTexture(RESOURCE.sky_texture_boss_Texture2D);
			}
			else
			{
				Debug.LogError("renderer was null");
			}
		}
		else
		{
			Debug.LogError("Skybox was null");
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x000553FE File Offset: 0x000537FE
	public override void EnterMinigame()
	{
		this.ShowStartUI();
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00055406 File Offset: 0x00053806
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUITopPlayAdventureBattle(_restartAction, _pauseAction);
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x00055420 File Offset: 0x00053820
	protected override void ShowStartUI()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterStartAdventureBattle), typeof(PsUITopStartAdventureBattle), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.BeginAdventure));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		this.ShowStartResources(true);
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x000554E4 File Offset: 0x000538E4
	public override void WinMinigame()
	{
		this.m_saveGhost = false;
		this.m_gameLoop.m_timeScoreCurrent = HighScores.TicksToTimeScore(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks), true);
		float num = HighScores.TimeScoreToTime(this.m_gameLoop.m_timeScoreCurrent);
		Debug.LogWarning("YOUR TIME: " + num);
		base.CheckAchievementsOnWin();
		if ((this.m_gameLoop as PsGameLoopAdventureBattle).CheckWin())
		{
			int collectedStars = PsState.m_activeMinigame.m_collectedStars;
			this.m_gameLoop.m_currentRunScore = collectedStars;
			if (this.m_gameLoop.m_scoreCurrent > this.m_gameLoop.m_scoreBest)
			{
				this.m_gameLoop.m_scoreBest = this.m_gameLoop.m_scoreCurrent;
				this.m_gameLoop.m_minigameMetaData.score = this.m_gameLoop.m_scoreBest;
				this.m_gameLoop.m_timeScoreBest = this.m_gameLoop.m_timeScoreCurrent;
				this.m_gameLoop.m_minigameMetaData.timeScore = this.m_gameLoop.m_timeScoreBest;
				this.m_saveGhost = true;
			}
			else if (this.m_gameLoop.m_scoreCurrent == this.m_gameLoop.m_scoreBest && this.m_gameLoop.m_timeScoreCurrent < this.m_gameLoop.m_timeScoreBest)
			{
				this.m_gameLoop.m_timeScoreBest = this.m_gameLoop.m_timeScoreCurrent;
				this.m_gameLoop.m_minigameMetaData.timeScore = this.m_gameLoop.m_timeScoreBest;
				this.m_saveGhost = true;
			}
			if (this.m_gameLoop.m_nodeId == this.m_gameLoop.m_path.m_currentNodeId)
			{
				BossBattles.AlterHandicap(PsState.m_activeMinigame.m_playerUnit.GetType(), BossBattles.winActionChange + BossBattles.GetBossWinRandomHandicap());
				this.m_saveGhost = true;
				base.SendHighscoreAndGhost();
			}
			this.CreateWinUI();
		}
		else
		{
			Debug.LogError("YOU LOST: ");
			this.ShowLoseUI2();
		}
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x000556D0 File Offset: 0x00053AD0
	protected override void CreateWinUI()
	{
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterWinAdventureBattle), typeof(PsUITopWinAdventure), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, false, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00055760 File Offset: 0x00053B60
	protected override void ShowLoseUI2()
	{
		if ((PsState.m_activeGameLoop.m_gameMode as PsGameModeAdventureBattle).GetBoss() != null)
		{
			float num = PsState.m_activeMinigame.m_gameTicks - (PsState.m_activeGameLoop.m_gameMode as PsGameModeAdventureBattle).GetBoss().m_reachedGoalGameTick;
			if (num > 0f)
			{
			}
		}
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterLoseAdventureBattle), typeof(PsUITopStartAdventureBattle), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, false, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x00055840 File Offset: 0x00053C40
	public override void CreatePlaybackGhostVisuals()
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

	// Token: 0x060007AA RID: 1962 RVA: 0x00055952 File Offset: 0x00053D52
	public void CollectibleCollected()
	{
		if (this.m_playbackGhosts.Count > 0)
		{
			this.PowerUp();
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0005596C File Offset: 0x00053D6C
	public float GetHandicap()
	{
		PsGameLoopAdventureBattle psGameLoopAdventureBattle = this.m_gameLoop as PsGameLoopAdventureBattle;
		float currentVehicleHandicap = BossBattles.GetCurrentVehicleHandicap();
		float globalHandicap = BossBattles.GetGlobalHandicap();
		float levelHandicap = BossBattles.GetLevelHandicap(psGameLoopAdventureBattle.GetTimeSinceStartInHours());
		float goalReachedHandicap = BossBattles.GetGoalReachedHandicap(psGameLoopAdventureBattle.m_reachedGoalCount);
		int num = Mathf.Max(0, psGameLoopAdventureBattle.m_tryCount - psGameLoopAdventureBattle.m_reachedGoalCount);
		float tryCountHandicap = BossBattles.GetTryCountHandicap(num);
		float randomRuntimeHandicap = psGameLoopAdventureBattle.m_randomRuntimeHandicap;
		float num2 = currentVehicleHandicap + globalHandicap + levelHandicap + goalReachedHandicap + tryCountHandicap;
		return BossBattles.HandicapBounds(num2);
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x000559E8 File Offset: 0x00053DE8
	public void PowerUp()
	{
		this.m_lastPrize = BossCheckpointSymbol.closed;
		int collectedStars = PsState.m_activeMinigame.m_collectedStars;
		float num = 0f;
		if (this.GetBoss().m_collectedPieceTick.Count > collectedStars - 1)
		{
			num = (float)this.GetBoss().m_collectedPieceTick[collectedStars - 1] - this.GetBoss().m_ghost.m_playbackTick * (float)this.GetBoss().m_ghost.m_frameSkip;
		}
		else
		{
			Debug.LogError("ghost did not have all pieces");
		}
		float handicap = this.GetHandicap();
		float num2 = num - handicap * 60f;
		BossBattles.GetCurrentVehicleHandicap();
		if (num2 < 0f && PsState.m_activeMinigame.m_gameOn)
		{
			if ((PsState.m_activeMinigame.m_playerUnit as Vehicle).m_powerUp == null)
			{
				if (num2 > -60f)
				{
					this.m_lastPrize = BossCheckpointSymbol.freeze;
					this.GivePowerUp(new PsPowerUpFreeze(1f));
					this.GetBoss().AddTickOffset(60f + num2);
				}
				else
				{
					this.m_lastPrize = BossCheckpointSymbol.freeze;
					float freezeDuration = BossBattles.GetFreezeDuration(Mathf.Abs(num2 / 60f));
					this.GivePowerUp(new PsPowerUpFreeze(freezeDuration));
				}
			}
		}
		else
		{
			this.GetBoss().AddTickOffset(num2);
		}
		if (this.m_lastPrize == BossCheckpointSymbol.closed)
		{
			float num3 = Random.Range(0f, 1f);
			float checkPointBoostChance = BossBattles.checkPointBoostChance;
			float checkPointCoinChance = BossBattles.checkPointCoinChance;
			if (num3 <= checkPointBoostChance)
			{
				this.m_lastPrize = BossCheckpointSymbol.boost;
				PsMetagameManager.m_playerStats.CumulateBoosters(1);
				if (PsIngameMenu.m_controller.m_boostButton != null)
				{
					(PsIngameMenu.m_controller.m_boostButton as PsUIBoosterPowerUpButton).CumulateBooster();
				}
			}
			else if (num3 <= checkPointBoostChance + checkPointCoinChance)
			{
				this.m_lastPrize = BossCheckpointSymbol.gold;
				PsMetagameManager.m_playerStats.CumulateCoins(BossBattles.checkPointCoinAmount);
			}
		}
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x00055BBB File Offset: 0x00053FBB
	public BossCheckpointSymbol GetLastPrize()
	{
		return this.m_lastPrize;
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00055BC3 File Offset: 0x00053FC3
	private void GivePowerUp(PsPowerUp _item)
	{
		(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_powerUp = _item;
		_item.SetUI(false);
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x00055BE1 File Offset: 0x00053FE1
	protected override GhostObject GetGhostObject(GhostData _data, global::Ghost _ghost, string _rewardFrameName = null)
	{
		return new GhostObjectBoss(_data, _ghost);
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00055BEA File Offset: 0x00053FEA
	public GhostObjectBoss GetBoss()
	{
		if (this.m_playbackGhosts != null && this.m_playbackGhosts.Count > 0)
		{
			return this.m_playbackGhosts[0] as GhostObjectBoss;
		}
		return null;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00055C1C File Offset: 0x0005401C
	public override HttpC ServerGetGhosts(GhostGetData _data, Action<GhostData[]> _customSUCCESSCallback = null)
	{
		Action<GhostData[]> action = new Action<GhostData[]>(this.GhostsLoadSUCCEED);
		if (_customSUCCESSCallback != null)
		{
			action = _customSUCCESSCallback;
		}
		Debug.LogWarning("REQUEST GHOST FROM SERVER");
		HttpC ghost = AdventureBattle.GetGhost(_data.minigameInfo.GetGameId(), PsState.GetCurrentVehicleType(false).ToString(), action, new Action<HttpC>(this.GhostsLoadFAILED), new Action(base.GhostLoadErrorHandler));
		ghost.objectData = _data;
		base.CreateGhostLoaderEntity();
		EntityManager.AddComponentToEntity(this.m_ghostLoaderEntity, ghost);
		return ghost;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00055C9C File Offset: 0x0005409C
	public override void CreateOffroadCarPlaybackGhostEntity(GhostObject _ghost, bool showAtStart = false, string _identifierColor = "")
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.OffroadCarBossPrefab_GameObject);
		GhostPart ghostPart = new GhostPart(gameObject.transform.Find("OffroadCar/Chassis").gameObject, Vector3.zero, "chassis", false);
		GhostPart ghostPart2 = new GhostPart(gameObject.transform.Find("Parts/CarTireFrontT3").gameObject, new Vector3(0f, 0f, -23f), "frontWheel", false);
		GhostPart ghostPart3 = new GhostPart(gameObject.transform.Find("Parts/CarTireFrontT3").gameObject, new Vector3(0f, 0f, 23f), "frontWheel", false);
		ghostPart3.prefab.transform.rotation.eulerAngles.Set(0f, 180f, 0f);
		GhostPart ghostPart4 = new GhostPart(gameObject.transform.Find("Parts/CarTireRearT3").gameObject, new Vector3(0f, 0f, -23f), "rearWheel", false);
		GhostPart ghostPart5 = new GhostPart(gameObject.transform.Find("Parts/CarTireRearT3").gameObject, new Vector3(0f, 0f, 23f), "rearWheel", false);
		ghostPart5.prefab.transform.rotation.eulerAngles.Set(0f, 180f, 0f);
		GhostBoostEffect ghostBoostEffect = new GhostBoostEffect(ResourceManager.GetGameObject(RESOURCE.EffectBoostOffroadCarFrontGhost_GameObject), Vector3.forward * -24f, "frontWheel");
		GhostBoostEffect ghostBoostEffect2 = new GhostBoostEffect(ResourceManager.GetGameObject(RESOURCE.EffectBoostOffroadCarBackGhost_GameObject), Vector3.forward * -24f, "rearWheel");
		string empty = string.Empty;
		string text = "trail_boss";
		_ghost.CreateVisuals(new GhostPart[] { ghostPart, ghostPart2, ghostPart3, ghostPart4, ghostPart5 }, text, new GhostBoostEffect[] { ghostBoostEffect, ghostBoostEffect2 }, false, false, string.Empty);
		_ghost.SetAlienCharacter(new Vector3(-8.6f, 2f, 0f), "AlienBossPrefab_GameObject", "Drive");
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00055F10 File Offset: 0x00054310
	public override void CreateMotorcyclePlaybackGhostEntity(GhostObject _ghost, bool _showAtStart = false, string _identifierColor = "")
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.MotorcycleBossPrefab_GameObject);
		GhostPart ghostPart = new GhostPart(gameObject.transform.Find("DirtBike/Chassis").gameObject, new Vector3(0f, -3f, 0f), "chassis", false);
		GhostPart ghostPart2 = new GhostPart(gameObject.transform.Find("Parts/BikeTireFrontT3").gameObject, Vector3.zero, "frontWheel", false);
		GhostPart ghostPart3 = new GhostPart(gameObject.transform.Find("Parts/BikeTireBackT3").gameObject, Vector3.zero, "rearWheel", false);
		GhostBoostEffect ghostBoostEffect = new GhostBoostEffect(ResourceManager.GetGameObject(RESOURCE.EffectBoostMotorcycleFrontGhost_GameObject), Vector3.forward * -0.8f, "frontWheel");
		GhostBoostEffect ghostBoostEffect2 = new GhostBoostEffect(ResourceManager.GetGameObject(RESOURCE.EffectBoostMotorcycleBackGhost_GameObject), Vector3.forward * -0.8f, "rearWheel");
		string empty = string.Empty;
		string text = "trail_boss";
		_ghost.CreateVisuals(new GhostPart[] { ghostPart, ghostPart2, ghostPart3 }, text, new GhostBoostEffect[] { ghostBoostEffect, ghostBoostEffect2 }, false, false, string.Empty);
		_ghost.SetAlienCharacter(new Vector3(-5.7f, 7f, 0f), "AlienBossPrefab_GameObject", "DriveMoto");
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00056088 File Offset: 0x00054488
	public override void DestroyPlaybackGhostVisuals()
	{
		Debug.LogError("Destroyplaybackghost visuals");
		for (int i = 0; i < this.m_playbackGhosts.Count; i++)
		{
			Debug.LogWarning("DESTROYING PLAYBACK GHOST VISUALS: " + this.m_playbackGhosts[i].m_name);
			this.m_playbackGhosts[i].DestroyVisuals();
		}
		if (this.m_bossGhost != null)
		{
			this.m_bossGhost.Destroy();
			this.m_bossGhost = null;
		}
		this.m_streaks = null;
	}

	// Token: 0x040007BE RID: 1982
	private Material skyMaterial;

	// Token: 0x040007BF RID: 1983
	private BossCheckpointSymbol m_lastPrize;

	// Token: 0x040007C0 RID: 1984
	private AlienCharacter m_bossGhost;
}
