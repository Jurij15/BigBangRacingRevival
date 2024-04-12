using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x020001E3 RID: 483
public class EditorPublishState : BasicState
{
	// Token: 0x06000E7E RID: 3710 RVA: 0x00086FB8 File Offset: 0x000853B8
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
		new UIPopupHeader(uiverticalList, "SavePopupHeader", PsStrings.Get(StringID.EDITOR_PUBLISHSCREEN_HEADER), PsStrings.Get(StringID.EDITOR_PUBLISHSCREEN_TEXT));
		this.m_publishPopupContainer.Update();
		this.m_creationTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
		this.PublishLevel();
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x000870B4 File Offset: 0x000854B4
	private DataBlob GetGhostData()
	{
		PsState.m_activeMinigame.m_lastSentTimeScore = HighScores.TicksToTimeScore(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks), true);
		PsState.m_activeMinigame.m_lastSentScore = 3;
		DataBlob dataBlob = default(DataBlob);
		Debug.LogWarning("GHOST " + PsState.m_activeGameLoop.m_gameMode.m_recordingGhost.m_recording);
		if (PsState.m_activeGameLoop.m_gameMode.m_recordingGhost != null && !PsState.m_activeGameLoop.m_gameMode.m_recordingGhost.m_recording)
		{
			PsState.m_activeGameLoop.m_minigameMetaData.timeScore = PsState.m_activeMinigame.m_lastSentTimeScore;
			PsState.m_activeGameLoop.m_minigameMetaData.score = PsState.m_activeMinigame.m_lastSentScore;
			dataBlob = ClientTools.CreateGhostDataBlob(PsState.m_activeGameLoop.m_gameMode.m_recordingGhost);
			PsMetricsData.m_lastGhostSize = dataBlob.data.Length;
		}
		else
		{
			PsMetricsData.m_lastGhostSize = 0;
		}
		return dataBlob;
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x000871AC File Offset: 0x000855AC
	private HttpC PublishLevel()
	{
		if (PsState.UsingEditorResources())
		{
			PsMetagameManager.m_playerStats.CumulateEditorResources(EditorScene.m_reservedResources);
			EditorScene.m_reservedResources.Clear();
		}
		DataBlob ghostData = this.GetGhostData();
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
		if (PsState.m_activeGameLoop.m_minigameMetaData.published)
		{
			PsState.m_activeGameLoop.m_minigameMetaData.id = string.Empty;
		}
		Hashtable hashtable = new Hashtable();
		bool hidden = PsState.m_activeGameLoop.m_minigameMetaData.hidden;
		PsAchievementManager.Complete("publishLevel");
		if (PsState.m_activeGameLoop.ElapsedTime() > 10800L)
		{
			PsAchievementManager.Complete("longTimeInEditor");
		}
		return MiniGame.Publish(PsState.m_activeGameLoop, array, hashtable, PsMetagameManager.m_playerStats.GetUpdatedEditorResources(), ghostData, new Action<HttpC>(this.PublishToServerOk), new Action<HttpC>(this.PublishToServerFailed), hidden, this.m_creationTime, null);
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x000872B8 File Offset: 0x000856B8
	private void PublishToServerOk(HttpC _c)
	{
		PsMinigameMetaData psMinigameMetaData = ClientTools.ParseMinigameMetaData(_c);
		if (psMinigameMetaData == null)
		{
			return;
		}
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		PsState.m_activeGameLoop.m_minigameMetaData = psMinigameMetaData;
		PsState.m_activeGameLoop.m_minigameId = psMinigameMetaData.id;
		PsMetricsData.m_timeInEditor += minigame.GetTimeSinceInit();
		Debug.Log("MINIGAME SAVED", null);
		PsCaches.m_savedList.Clear();
		PsCaches.m_publishedList.Clear();
		this.m_wasPublishedPopup = new PsUIBasePopup(typeof(PsUICenterLevelWasPublished), null, null, null, true, true, InitialPage.Center, false, false, false);
		CameraS.CreateBlur(CameraS.m_mainCamera, null);
		this.m_wasPublishedPopup.SetAction("Continue", delegate
		{
			PsState.m_activeGameLoop.ExitEditor(true, null);
		});
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x00087380 File Offset: 0x00085780
	private void PublishToServerFailed(HttpC _c)
	{
		Debug.LogError("MINIGAME SAVE FAILED");
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, new Func<HttpC>(this.PublishLevel), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x000873CA File Offset: 0x000857CA
	public override void Execute()
	{
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x000873CC File Offset: 0x000857CC
	public override void Exit()
	{
		if (this.m_publishPopupContainer != null)
		{
			this.m_publishPopupContainer.Destroy();
			this.m_publishPopupContainer = null;
		}
		if (this.m_wasPublishedPopup != null)
		{
			CameraS.RemoveBlur();
			this.m_wasPublishedPopup.Destroy();
			this.m_wasPublishedPopup = null;
		}
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x00087418 File Offset: 0x00085818
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

	// Token: 0x04001176 RID: 4470
	private UICanvas m_publishPopupContainer;

	// Token: 0x04001177 RID: 4471
	private string m_creationTime;

	// Token: 0x04001178 RID: 4472
	private PsUIBasePopup m_wasPublishedPopup;
}
