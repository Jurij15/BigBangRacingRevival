using System;
using Server;

// Token: 0x02000117 RID: 279
public class PsGameModeAdventureFresh : PsGameModeAdventure
{
	// Token: 0x060007B9 RID: 1977 RVA: 0x00056150 File Offset: 0x00054550
	public PsGameModeAdventureFresh(PsGameLoop _info)
		: base(_info)
	{
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00056159 File Offset: 0x00054559
	protected PsGameModeAdventureFresh(PsGameMode _gameMode, PsGameLoop _info)
		: base(_gameMode, _info)
	{
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00056164 File Offset: 0x00054564
	protected override void GiveMapPieces(int _scoreChange)
	{
		this.m_startShards = PsMetagameManager.m_playerStats.shards;
		this.m_shardChange = _scoreChange;
		this.m_diamondChange = 0;
		Debug.Log("Give big shards: " + _scoreChange, null);
		int num = _scoreChange * PsState.m_bigShardValue;
		PsMetagameManager.m_playerStats.shards += num;
		if (PsState.m_activeMinigame != null)
		{
			Minigame activeMinigame = PsState.m_activeMinigame;
			activeMinigame.m_collectedShards += num;
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
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x00056250 File Offset: 0x00054650
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUITopPlayAdventureFresh(_restartAction, _pauseAction);
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0005626C File Offset: 0x0005466C
	protected override void ShowStartUI()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterStartAdventureFresh), typeof(PsUITopStartAdventure), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.BeginAdventure));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		this.ShowStartResources(true);
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x00056330 File Offset: 0x00054730
	public override void ShowStartResources(bool _diamonds = true)
	{
		PsMetagameManager.ShowResources(PsIngameMenu.m_popupMenu.m_overlayCamera, true, false, _diamonds, false, 0.03f, true, false, true);
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00056358 File Offset: 0x00054758
	protected override void ShowLoseUI2()
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterLoseAdventureFresh), typeof(PsUITopStartAdventure), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Exit", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Skip", new Action(this.m_gameLoop.SkipMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		this.ShowStartResources(true);
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0005641C File Offset: 0x0005481C
	protected override void CreateWinUI()
	{
		PsIngameMenu.m_popupMenu = new PsUIBasePopup(typeof(PsUICenterWinAdventureFresh), typeof(PsUITopWinAdventure), null, null, false, true, InitialPage.Center, false, false, false);
		PsIngameMenu.m_popupMenu.SetAction("Continue", new Action(this.m_gameLoop.ExitMinigame));
		PsIngameMenu.m_popupMenu.SetAction("Start", new Action(this.m_gameLoop.RestartMinigame));
		this.ShowStartResources(true);
		PsMetagameManager.m_menuResourceView.m_shards.SetText(this.m_startShards.ToString());
		PsMetagameManager.m_menuResourceView.m_diamonds.SetText((PsMetagameManager.m_playerStats.diamonds - this.m_diamondChange).ToString());
		PsMetagameManager.m_playerStats.updated = false;
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x000564F4 File Offset: 0x000548F4
	public override void SendQuit()
	{
		if (PsState.m_activeMinigame.m_playerReachedGoalCount < 1 && PsState.m_activeMinigame.m_gameStartCount > 0)
		{
			SendQuitData data = new SendQuitData();
			data.startCount = PsState.m_activeMinigame.m_gameStartCount;
			data.gameLoop = PsState.m_activeGameLoop;
			data.playerUnit = PsState.m_activeMinigame.m_playerUnitName;
			new PsServerQueueFlow(null, delegate
			{
				this.ServerSendQuit(data);
			}, new string[] { "SetData" });
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00056594 File Offset: 0x00054994
	public HttpC ServerSendQuit(SendQuitData _data)
	{
		return StarCollect.Lose(_data, null, new Action<HttpC>(this.QuitSendSUCCEED), delegate(HttpC k)
		{
			this.QuitSendFAILED(k, _data);
		}, null);
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x000565DC File Offset: 0x000549DC
	public void QuitSendSUCCEED(HttpC _c)
	{
		Debug.Log("SEND QUIT SUCCEED", null);
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x000565EC File Offset: 0x000549EC
	public void QuitSendFAILED(HttpC _c, SendQuitData _data)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), () => this.ServerSendQuit(_data), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x040007C1 RID: 1985
	public int m_shardChange;

	// Token: 0x040007C2 RID: 1986
	public int m_diamondChange;

	// Token: 0x040007C3 RID: 1987
	public int m_startShards;
}
