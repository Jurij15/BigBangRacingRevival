using System;

// Token: 0x0200022E RID: 558
public class PsRateAppState : BasicState
{
	// Token: 0x060010A6 RID: 4262 RVA: 0x0009EC4E File Offset: 0x0009D04E
	public override void Enter(IStatedObject _parent)
	{
		PsMetagameManager.ShowRateAppDialogue(new Action(this.EnterMainMenuState));
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x0009EC61 File Offset: 0x0009D061
	public void EnterMainMenuState()
	{
		PsMainMenuState.m_tweenIn = true;
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
	}
}
