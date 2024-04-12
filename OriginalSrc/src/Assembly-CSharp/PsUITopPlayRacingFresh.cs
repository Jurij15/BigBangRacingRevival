using System;

// Token: 0x0200035F RID: 863
public class PsUITopPlayRacingFresh : PsUITopPlayRacing
{
	// Token: 0x06001923 RID: 6435 RVA: 0x001109C3 File Offset: 0x0010EDC3
	public PsUITopPlayRacingFresh(Action _restartAction = null, Action _pauseAction = null)
		: base(_restartAction, _pauseAction)
	{
	}

	// Token: 0x06001924 RID: 6436 RVA: 0x001109D0 File Offset: 0x0010EDD0
	public override void CreateCoinArea()
	{
		PsMetagameManager.ShowResources(this.m_camera, false, false, true, false, 0.15f, false, false, true);
	}
}
