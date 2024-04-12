using System;

// Token: 0x0200028F RID: 655
public class PsCreateState : PsUIBaseState
{
	// Token: 0x060013B3 RID: 5043 RVA: 0x000C4C44 File Offset: 0x000C3044
	public PsCreateState()
		: base(typeof(PsUITabbedCreate), typeof(PsUITopBackButton), null, null, false, InitialPage.Center)
	{
		base.SetAction("Exit", delegate
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
		});
	}

	// Token: 0x060013B4 RID: 5044 RVA: 0x000C4C97 File Offset: 0x000C3097
	public override void Enter(IStatedObject _parent)
	{
		if (PsState.m_activeGameLoop != null)
		{
			PsState.m_activeGameLoop.ReleaseLoop();
		}
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		base.Enter(_parent);
	}
}
