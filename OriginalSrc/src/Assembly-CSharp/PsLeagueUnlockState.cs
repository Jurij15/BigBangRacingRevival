using System;

// Token: 0x020003A3 RID: 931
public class PsLeagueUnlockState : BasicState, IStatedObject
{
	// Token: 0x06001A9A RID: 6810 RVA: 0x00128318 File Offset: 0x00126718
	public override void Enter(IStatedObject _parent)
	{
		PsMenuScene.m_lastState = "PsLeagueUnlockState";
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterLeaguesRankUp), typeof(PsUITopBackButton), null, null, true, true, InitialPage.Center, false, false, false);
		popup.SetAction("Exit", delegate
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
			popup.Destroy();
		});
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
	}
}
