using System;

// Token: 0x02000357 RID: 855
public class PsUITopPlayAdventureFresh : PsUITopPlayAdventure
{
	// Token: 0x060018F8 RID: 6392 RVA: 0x0010F7EC File Offset: 0x0010DBEC
	public PsUITopPlayAdventureFresh(Action _restartAction = null, Action _pauseAction = null)
		: base(_restartAction, _pauseAction)
	{
	}

	// Token: 0x060018F9 RID: 6393 RVA: 0x0010F7F6 File Offset: 0x0010DBF6
	protected override string GetCollactableFrame(int _index)
	{
		return "hud_shard" + (_index + 1);
	}

	// Token: 0x060018FA RID: 6394 RVA: 0x0010F80C File Offset: 0x0010DC0C
	public override void CreateCoinArea()
	{
		PsMetagameManager.ShowResources(this.m_camera, false, false, true, false, 0.15f, false, false, true);
	}
}
