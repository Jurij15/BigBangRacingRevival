using System;

// Token: 0x02000116 RID: 278
public class PsGameModeAdventureEditor : PsGameModeAdventure
{
	// Token: 0x060007B5 RID: 1973 RVA: 0x00056110 File Offset: 0x00054510
	protected PsGameModeAdventureEditor(PsGameMode _gameMode, PsGameLoop _info)
		: base(_gameMode, _info)
	{
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0005611A File Offset: 0x0005451A
	public PsGameModeAdventureEditor(PsGameLoop _info)
		: base(PsGameMode.StarCollect, _info)
	{
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x00056124 File Offset: 0x00054524
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUIEditorAdventurePlay(_restartAction, _pauseAction);
		PsIngameMenu.OpenController(false);
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0005613D File Offset: 0x0005453D
	public override void CreateEditMenu(Action _exitAction, Action _testAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.m_playMenu = new PsUIEditorEdit(_exitAction, _testAction);
	}
}
