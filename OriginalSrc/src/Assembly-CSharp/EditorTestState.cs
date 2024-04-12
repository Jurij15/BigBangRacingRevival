using System;
using UnityEngine;

// Token: 0x020001E7 RID: 487
public class EditorTestState : BasicState
{
	// Token: 0x06000E9E RID: 3742 RVA: 0x00087DE5 File Offset: 0x000861E5
	public override void Enter(IStatedObject _parent)
	{
		EditorScene.m_cameraManIdleTimer = Random.Range(3, 6);
		EditorScene.ClearScreenshots();
		AutoGeometryManager.SetDirtyTileTracking(true);
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x00087E00 File Offset: 0x00086200
	public override void Execute()
	{
		if (!PsState.m_physicsPaused && PsState.m_activeMinigame.m_gameStarted && !PsState.m_activeMinigame.m_gameEnded && !PsState.m_activeMinigame.m_gameTicksFreezed)
		{
			PsState.m_activeMinigame.m_gameTicks = PsState.m_activeMinigame.m_gameTicks + 1f * Main.m_timeScale;
			if (PsState.m_cameraManTakeShot && EditorScene.m_cameraManShotDelay <= 0)
			{
				EditorScene.TakeScreenshot();
				PsState.m_cameraManTakeShot = false;
				EditorScene.m_cameraManShotDelay = Random.Range(120, 240);
			}
			if (EditorScene.m_cameraManShotDelay > 0)
			{
				EditorScene.m_cameraManShotDelay--;
			}
			else
			{
				EditorScene.m_cameraManIdleTimer--;
			}
			if (EditorScene.m_cameraManIdleTimer <= 0)
			{
				EditorScene.m_cameraManIdleTimer = Random.Range(3, 6);
				PsState.m_cameraManTakeShot = true;
			}
		}
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x00087EDA File Offset: 0x000862DA
	public override void Exit()
	{
	}
}
