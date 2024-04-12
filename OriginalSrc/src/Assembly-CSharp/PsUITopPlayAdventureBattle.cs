using System;

// Token: 0x02000358 RID: 856
public class PsUITopPlayAdventureBattle : PsUITopPlayAdventure
{
	// Token: 0x060018FB RID: 6395 RVA: 0x0010F830 File Offset: 0x0010DC30
	public PsUITopPlayAdventureBattle(Action _restartAction = null, Action _pauseAction = null)
		: base(_restartAction, _pauseAction)
	{
	}

	// Token: 0x060018FC RID: 6396 RVA: 0x0010F83A File Offset: 0x0010DC3A
	protected override string GetCollactableFrame(int _index)
	{
		return "hud_skull_checkpoint_on";
	}
}
