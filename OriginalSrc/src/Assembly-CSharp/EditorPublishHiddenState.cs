using System;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x020001E1 RID: 481
public class EditorPublishHiddenState : BasicState
{
	// Token: 0x06000E67 RID: 3687 RVA: 0x00086450 File Offset: 0x00084850
	public override void Enter(IStatedObject _parent)
	{
		this.m_publishPopupContainer = new UICanvas(null, true, "PublishPopupContainer", null, string.Empty);
		this.m_publishPopupContainer.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_publishPopupContainer.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_publishPopupContainer.SetMargins(0.125f, 0.125f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_publishPopupContainer.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupBackground));
		UIVerticalList uiverticalList = new UIVerticalList(this.m_publishPopupContainer, "PublishPopup");
		uiverticalList.SetHeight(0.764f, RelativeTo.ParentHeight);
		uiverticalList.RemoveDrawHandler();
		new UIPopupHeader(uiverticalList, "SavePopupHeader", "publishing...", "Sending to server, please wait.");
		this.m_publishPopupContainer.Update();
		this.m_creationTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
		this.SendLevelToServer();
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x00086544 File Offset: 0x00084944
	private HttpC SendLevelToServer()
	{
		PsState.m_activeMinigame.SetLayerItems();
		byte[] array = null;
		global::Screenshot selectedScreenshot = EditorScene.GetSelectedScreenshot();
		if (selectedScreenshot != null)
		{
			array = selectedScreenshot.GetScreenshotJPGBytes();
		}
		else
		{
			Debug.LogError("NO SCREENSHOT! -- DAS IST PROBLEM");
		}
		PsState.m_activeGameLoop.m_minigameMetaData.id = string.Empty;
		HttpC httpC = MiniGame.SaveHidden(PsState.m_activeGameLoop, array, new Action<HttpC>(this.LevelSendToServerOk), new Action<HttpC>(this.LevelSendToServerFailed), this.m_creationTime, null);
		httpC.objectData = PsState.m_activeGameLoop;
		return httpC;
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x000865D0 File Offset: 0x000849D0
	private void LevelSendToServerOk(HttpC _c)
	{
		PsMinigameMetaData psMinigameMetaData = ClientTools.ParseMinigameMetaData(_c);
		this.LevelSendToServerOk(psMinigameMetaData);
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x000865EC File Offset: 0x000849EC
	private void LevelSendToServerOk(PsMinigameMetaData _metaData)
	{
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		PsState.m_activeGameLoop.m_minigameMetaData = _metaData;
		PsState.m_activeGameLoop.m_minigameId = _metaData.id;
		PsMetricsData.m_timeInEditor += minigame.GetTimeSinceInit();
		Debug.Log("MINIGAME SAVED", null);
		this.SendHighscoreAndGhost();
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x00086642 File Offset: 0x00084A42
	private void LevelSendToServerFailed(HttpC _c)
	{
		Debug.LogError("MINIGAME SAVE FAILED");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), new Func<HttpC>(this.SendLevelToServer), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x00086680 File Offset: 0x00084A80
	private HttpC SendHighscoreAndGhost()
	{
		PsState.m_activeMinigame.m_lastSentTimeScore = HighScores.TicksToTimeScore(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks), true);
		PsState.m_activeMinigame.m_lastSentScore = 3;
		DataBlob dataBlob = default(DataBlob);
		if (PsState.m_activeGameLoop.m_gameMode.m_recordingGhost != null && !PsState.m_activeGameLoop.m_gameMode.m_recordingGhost.m_recording)
		{
			PsState.m_activeGameLoop.m_minigameMetaData.timeScore = PsState.m_activeMinigame.m_lastSentTimeScore;
			PsState.m_activeGameLoop.m_minigameMetaData.score = PsState.m_activeMinigame.m_lastSentScore;
			dataBlob = ClientTools.CreateGhostDataBlob(PsState.m_activeGameLoop.m_gameMode.m_recordingGhost);
		}
		PsMetricsData.m_lastGhostSize = dataBlob.data.Length;
		if (PsState.m_activeGameLoop.m_minigameMetaData.gameMode == PsGameMode.Race)
		{
			return this.SendRacingGhost(dataBlob);
		}
		if (PsState.m_activeGameLoop.m_minigameMetaData.gameMode == PsGameMode.StarCollect)
		{
			return this.SendAdventureGhost(dataBlob);
		}
		Debug.LogError("ERROR SENDING GHOST: NO VALID GAME MODE FOR LEVEL!");
		return null;
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x00086788 File Offset: 0x00084B88
	private HttpC SendRacingGhost(DataBlob _ghostData)
	{
		RacingGhostData racingGhostData = new RacingGhostData(PsState.m_activeGameLoop.m_minigameId, null, PsState.m_activeGameLoop.m_minigameMetaData.timeScore, this.GetUpgradeSum(), PsState.m_activeMinigame.m_playerUnitName, _ghostData, true);
		return Trophy.SendScore(racingGhostData, new OpponentData[0], true, true, null, new Action<HttpC>(this.SendOK), delegate(HttpC c)
		{
			this.RacingFAILURE(c, _ghostData);
		}, null);
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x00086808 File Offset: 0x00084C08
	private HttpC SendAdventureGhost(DataBlob _ghostData)
	{
		StarCollect.StarCollectData starCollectData = new StarCollect.StarCollectData(PsState.m_activeGameLoop.m_minigameId, PsState.m_activeMinigame.m_gameStartCount, PsState.m_activeMinigame.m_playerUnitName, 3, PsState.m_activeGameLoop.m_minigameMetaData.timeScore, _ghostData, null, PsMetricsData.m_lastGhostSize);
		return StarCollect.Win(starCollectData, new Action<HttpC>(this.SendOK), delegate(HttpC c)
		{
			this.AdventureFAILURE(c, _ghostData);
		}, null);
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x00086888 File Offset: 0x00084C88
	private void SendOK(HttpC _c)
	{
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		PsCaches.m_savedList.Clear();
		PsCaches.m_publishedList.Clear();
		PsState.m_activeGameLoop.ExitEditor(true, null);
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x000868C0 File Offset: 0x00084CC0
	private void RacingFAILURE(HttpC _c, DataBlob _ghostData)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => this.SendRacingGhost(_ghostData), null);
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x00086904 File Offset: 0x00084D04
	private void AdventureFAILURE(HttpC _c, DataBlob _ghostData)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => this.SendAdventureGhost(_ghostData), null);
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x00086947 File Offset: 0x00084D47
	public override void Execute()
	{
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x00086949 File Offset: 0x00084D49
	public override void Exit()
	{
		if (this.m_publishPopupContainer != null)
		{
			this.m_publishPopupContainer.Destroy();
			this.m_publishPopupContainer = null;
		}
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x00086968 File Offset: 0x00084D68
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

	// Token: 0x04001169 RID: 4457
	private UICanvas m_publishPopupContainer;

	// Token: 0x0400116A RID: 4458
	private string m_creationTime;
}
