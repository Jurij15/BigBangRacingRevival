using System;

// Token: 0x0200020F RID: 527
public class GameBeginAdventureState : BasicState
{
	// Token: 0x06000F55 RID: 3925 RVA: 0x0009157C File Offset: 0x0008F97C
	public override void Enter(IStatedObject _parent)
	{
		GamePlayState.m_canStart = true;
		NotificationManager.Pause();
		this.m_startPopup = new PsUIBasePopup(this.GetUIType(), null, null, null, false, true, InitialPage.Center, false, false, false);
		this.m_startPopup.SetAction("Back", new Action(PsState.m_activeGameLoop.CancelBegin));
		CameraS.BringToFront((PsIngameMenu.m_playMenu as PsUITopPlayAdventure).m_camera, true);
		GameLevelPreview.TurnCameraToStartPos();
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000915E9 File Offset: 0x0008F9E9
	protected virtual Type GetUIType()
	{
		return typeof(PsUICenterBeginAdventure);
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x000915F5 File Offset: 0x0008F9F5
	public override void Execute()
	{
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x000915F7 File Offset: 0x0008F9F7
	public override void Exit()
	{
		GamePlayState.m_canStart = false;
		this.m_startPopup.Destroy();
	}

	// Token: 0x04001233 RID: 4659
	private PsUIBasePopup m_startPopup;
}
