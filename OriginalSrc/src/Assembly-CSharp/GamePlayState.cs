using System;
using UnityEngine;

// Token: 0x02000214 RID: 532
public class GamePlayState : BasicState
{
	// Token: 0x06000F65 RID: 3941 RVA: 0x0009181A File Offset: 0x0008FC1A
	public override void Enter(IStatedObject _parent)
	{
		NotificationManager.Pause();
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x00091824 File Offset: 0x0008FC24
	public override void Execute()
	{
		if (!PsState.m_physicsPaused && PsState.m_activeMinigame.m_gameStarted && !PsState.m_activeMinigame.m_gameEnded && !PsState.m_activeMinigame.m_gameTicksFreezed)
		{
			PsState.m_activeMinigame.m_gameTicks = PsState.m_activeMinigame.m_gameTicks + 1f * Main.m_timeScale;
			PsState.m_activeMinigame.m_realTimeSpent = PsState.m_activeMinigame.m_realTimeSpent + Time.deltaTime * Main.m_timeScale;
			PsState.m_debugCoinsTime += Main.m_gameDeltaTime;
		}
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x000918B8 File Offset: 0x0008FCB8
	public void ContinueToBeamOut()
	{
		Debug.Log("CONTINUE TO BEAM OUT", null);
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x000918C5 File Offset: 0x0008FCC5
	public void ResumeGame()
	{
		Debug.Log("RESUME GAME", null);
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x000918D2 File Offset: 0x0008FCD2
	public override void Exit()
	{
	}

	// Token: 0x04001236 RID: 4662
	public static bool m_canStart;
}
