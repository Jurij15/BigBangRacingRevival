using System;

// Token: 0x02000379 RID: 889
public class PsTeamState : PsUIBaseState
{
	// Token: 0x060019BD RID: 6589 RVA: 0x0011AFDC File Offset: 0x001193DC
	public PsTeamState()
		: base(typeof(PsUITabbedJoinedTeam), typeof(PsUITopBackButton), null, null, false, InitialPage.Center)
	{
		base.SetAction("Exit", delegate
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
		});
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x0011B02F File Offset: 0x0011942F
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
