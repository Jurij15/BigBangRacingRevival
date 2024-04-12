using System;
using Server;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public class EditorSaveState : BasicState
{
	// Token: 0x06000E8F RID: 3727 RVA: 0x00087967 File Offset: 0x00085D67
	public EditorSaveState(bool _saveLocally = false)
	{
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x00087970 File Offset: 0x00085D70
	public override void Enter(IStatedObject _parent)
	{
		this.m_savePopupContainer = new UICanvas(null, true, "SavePopupContainer", null, string.Empty);
		this.m_savePopupContainer.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_savePopupContainer.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_savePopupContainer.SetMargins(0.125f, 0.125f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_savePopupContainer.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupBackground));
		UIVerticalList uiverticalList = new UIVerticalList(this.m_savePopupContainer, "SavePopup");
		uiverticalList.SetHeight(0.764f, RelativeTo.ParentHeight);
		uiverticalList.RemoveDrawHandler();
		new UIPopupHeader(uiverticalList, "SavePopupHeader", PsStrings.Get(StringID.EDITOR_SAVESCREEN_HEADER), PsStrings.Get(StringID.EDITOR_PUBLISHSCREEN_TEXT));
		this.m_savePopupContainer.Update();
		this.m_creationTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
		this.SendLevelToServer();
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x00087A6C File Offset: 0x00085E6C
	private HttpC SendLevelToServer()
	{
		if (PsState.UsingEditorResources())
		{
			PsMetagameManager.m_playerStats.CumulateEditorResources(EditorScene.m_reservedResources);
			EditorScene.m_reservedResources.Clear();
		}
		if (PsState.m_activeGameLoop.m_minigameMetaData.published)
		{
			PsState.m_activeGameLoop.m_minigameMetaData.id = string.Empty;
		}
		PsState.m_activeMinigame.SetLayerItems();
		return MiniGame.Save(PsState.m_activeGameLoop, null, PsMetagameManager.m_playerStats.GetUpdatedEditorResources(), new Action<HttpC>(this.LevelSendToServerOk), new Action<HttpC>(this.LevelSendToServerFailed), false, this.m_creationTime, null);
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x00087B04 File Offset: 0x00085F04
	private void LevelSendToServerOk(HttpC _c)
	{
		PsMinigameMetaData psMinigameMetaData = ClientTools.ParseMinigameMetaData(_c);
		this.LevelSendToServerOk(psMinigameMetaData);
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x00087B20 File Offset: 0x00085F20
	private void LevelSendToServerOk(PsMinigameMetaData _metaData)
	{
		PsState.m_activeGameLoop.m_minigameMetaData = _metaData;
		PsState.m_activeGameLoop.m_minigameId = _metaData.id;
		PsMetrics.LevelSaved();
		Debug.Log("MINIGAME SAVED", null);
		Debug.Log("TIME EDITED: " + _metaData.timeSpentEditing, null);
		PsMetricsData.m_timeInEditor += (int)PsState.m_activeGameLoop.ElapsedTime();
		PsCaches.m_savedList.Clear();
		Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLoadingScene(Color.black, true, 0.25f));
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x00087BC0 File Offset: 0x00085FC0
	private void LevelSendToServerFailed(HttpC _c)
	{
		Debug.LogError("MINIGAME SAVE FAILED");
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, new Func<HttpC>(this.SendLevelToServer), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x00087C0A File Offset: 0x0008600A
	public override void Execute()
	{
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x00087C0C File Offset: 0x0008600C
	public override void Exit()
	{
		if (this.m_savePopupContainer != null)
		{
			this.m_savePopupContainer.Destroy();
			this.m_savePopupContainer = null;
		}
	}

	// Token: 0x04001183 RID: 4483
	private UICanvas m_savePopupContainer;

	// Token: 0x04001184 RID: 4484
	private string m_creationTime;

	// Token: 0x04001185 RID: 4485
	private bool m_saveLocally;
}
