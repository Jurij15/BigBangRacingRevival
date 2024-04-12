using System;

// Token: 0x0200011D RID: 285
public class PsGameModeRaceEditor : PsGameModeRace
{
	// Token: 0x0600080E RID: 2062 RVA: 0x0005736D File Offset: 0x0005576D
	protected PsGameModeRaceEditor(PsGameMode _gameMode, PsGameLoop _info)
		: base(_info)
	{
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00057376 File Offset: 0x00055776
	public PsGameModeRaceEditor(PsGameLoop _info)
		: base(_info)
	{
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0005737F File Offset: 0x0005577F
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUIEditorRacePlay(_restartAction, _pauseAction);
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00057398 File Offset: 0x00055798
	public override void CreateEditMenu(Action _exitAction, Action _testAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUIEditorEdit(_exitAction, _testAction);
	}
}
