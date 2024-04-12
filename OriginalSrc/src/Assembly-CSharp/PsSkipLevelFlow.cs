using System;

// Token: 0x02000207 RID: 519
public class PsSkipLevelFlow : Flow
{
	// Token: 0x06000F19 RID: 3865 RVA: 0x0008FDFC File Offset: 0x0008E1FC
	public PsSkipLevelFlow(Action _proceed, Action _cancel, int _diamondAmount)
		: base(_proceed, _cancel, null)
	{
		if (PsMetagameManager.m_playerStats.diamonds >= _diamondAmount)
		{
			PsMetagameManager.SkipLevel(_diamondAmount, PsState.m_activeGameLoop.GetPlayerUnit());
			this.Proceed.Invoke();
		}
		else
		{
			CameraS.CreateBlur(CameraS.m_mainCamera, null);
			new PsGetDiamondsFlow(new Action(CameraS.RemoveBlur), new Action(CameraS.RemoveBlur), null);
		}
	}
}
