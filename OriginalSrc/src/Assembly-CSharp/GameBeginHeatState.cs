using System;
using UnityEngine;

// Token: 0x02000212 RID: 530
public class GameBeginHeatState : BasicState
{
	// Token: 0x06000F5E RID: 3934 RVA: 0x0009163C File Offset: 0x0008FA3C
	public override void Enter(IStatedObject _parent)
	{
		GamePlayState.m_canStart = false;
		NotificationManager.Pause();
		this.m_timer = 180;
		this.m_startPopup = this.GetPopup();
		this.m_startPopup.SetAction("Back", new Action(PsState.m_activeGameLoop.CancelBegin));
		CameraS.BringToFront((PsIngameMenu.m_playMenu as PsUITopPlayRacing).m_camera, true);
		CameraS.BringToFront(this.m_startPopup.m_overlayCamera, true);
		GameLevelPreview.TurnCameraToStartPos();
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x000916B8 File Offset: 0x0008FAB8
	public virtual PsUIBasePopup GetPopup()
	{
		return new PsUIBasePopup(typeof(PsUICenterBeginRacing), typeof(PsUITopBeginRacing), null, null, false, true, InitialPage.Center, false, false, false);
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x000916E8 File Offset: 0x0008FAE8
	public override void Execute()
	{
		if (!PsState.m_activeMinigame.m_gameStarted && !PsState.m_activeMinigame.m_gameEnded && !PsState.m_activeMinigame.m_gameTicksFreezed)
		{
			this.m_timer--;
			if (this.m_timer == 120 || this.m_timer == 60)
			{
				(this.m_startPopup.m_mainContent as PsUICenterBeginRacing).CreateCountDownNumber(this.m_timer / 60, true);
			}
			if (this.m_timer <= 0 && !GamePlayState.m_canStart)
			{
				this.m_startPopup.m_mainContent.DestroyChildren(0);
				this.m_startPopup.Update();
				GamePlayState.m_canStart = true;
				PsState.m_activeGameLoop.WaitForUserToStart();
				SoundS.PlaySingleShotWithParameter("/InGame/Events/RaceCountdown", Vector3.zero, "Counter", 0f, 1f);
			}
		}
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x000917C9 File Offset: 0x0008FBC9
	public override void Exit()
	{
		GamePlayState.m_canStart = false;
		this.m_startPopup.Destroy();
	}

	// Token: 0x04001234 RID: 4660
	private int m_timer;

	// Token: 0x04001235 RID: 4661
	private PsUIBasePopup m_startPopup;
}
