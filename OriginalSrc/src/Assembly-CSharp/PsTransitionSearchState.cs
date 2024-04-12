using System;

// Token: 0x02000179 RID: 377
public class PsTransitionSearchState : BasicState
{
	// Token: 0x06000C93 RID: 3219 RVA: 0x00076BB8 File Offset: 0x00074FB8
	public override void Enter(IStatedObject _parent)
	{
		if (PsState.m_activeGameLoop != null)
		{
			PsState.m_activeGameLoop.ReleaseLoop();
		}
		PsUIBaseState psUIBaseState = new PsUIBaseState(typeof(PsUITabbedCreate), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
		psUIBaseState.SetAction("Exit", delegate
		{
			PsMainMenuState.ExitToMainMenuState();
		});
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState);
	}
}
