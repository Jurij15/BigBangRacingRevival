using System;

// Token: 0x020001E6 RID: 486
public class EditorTestEndState : BasicState
{
	// Token: 0x06000E97 RID: 3735 RVA: 0x00087C2C File Offset: 0x0008602C
	public EditorTestEndState()
	{
		if (PsMetagamePlayerData.m_playerData.ContainsKey("lastPublish"))
		{
			this.m_lastPublish = double.Parse((string)PsMetagamePlayerData.m_playerData["lastPublish"]);
		}
		else
		{
			this.m_lastPublish = 0.0;
		}
		this.m_publishCooldown = 1800.0;
		this.m_timeLeft = (int)Math.Ceiling(this.m_lastPublish + this.m_publishCooldown - Main.m_EPOCHSeconds);
		string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft);
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x00087CC0 File Offset: 0x000860C0
	public override void Enter(IStatedObject _parent)
	{
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x00087CC2 File Offset: 0x000860C2
	public static void Edit()
	{
		PsState.m_activeGameLoop.ExitMinigame();
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x00087CD0 File Offset: 0x000860D0
	public static void Save()
	{
		EditorBaseState.RemoveTransformGizmo();
		UndoManager.Purge();
		Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorSavePopupState());
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		(LevelManager.m_currentLevel as Minigame).m_groundNode.RevertGroundFromPlay();
		LevelManager.ResetCurrentLevel();
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x00087D28 File Offset: 0x00086128
	public static void Publish()
	{
		EditorBaseState.RemoveTransformGizmo();
		UndoManager.Purge();
		PsState.m_activeGameLoop.m_minigameMetaData.creatorUpgrades = (PsState.m_activeMinigame.m_playerUnit as Vehicle).GetActualPerformanceValues();
		if (PsState.m_adminMode)
		{
			Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorPublishHiddenPopupState());
		}
		else
		{
			Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorPublishPopupState());
		}
		EntityManager.RemoveEntitiesByTag("GTAG_AUTODESTROY");
		(LevelManager.m_currentLevel as Minigame).m_groundNode.RevertGroundFromPlay();
		LevelManager.ResetCurrentLevel();
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x00087DD0 File Offset: 0x000861D0
	public override void Exit()
	{
		this.m_endCanvas.Destroy();
	}

	// Token: 0x04001187 RID: 4487
	public UIVerticalList m_varea;

	// Token: 0x04001188 RID: 4488
	public UIHorizontalList m_area;

	// Token: 0x04001189 RID: 4489
	public PsUIGenericButton m_publishButton;

	// Token: 0x0400118A RID: 4490
	public UIText m_timerText;

	// Token: 0x0400118B RID: 4491
	public UIRectSpriteButton m_restartButton;

	// Token: 0x0400118C RID: 4492
	public UIRectSpriteButton m_editButton;

	// Token: 0x0400118D RID: 4493
	private double m_lastPublish;

	// Token: 0x0400118E RID: 4494
	private int m_timeLeft;

	// Token: 0x0400118F RID: 4495
	private double m_publishCooldown;

	// Token: 0x04001190 RID: 4496
	private UICanvas m_endCanvas;
}
