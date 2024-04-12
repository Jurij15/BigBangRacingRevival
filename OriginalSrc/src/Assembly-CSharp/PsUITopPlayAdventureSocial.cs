using System;

// Token: 0x02000359 RID: 857
public class PsUITopPlayAdventureSocial : PsUITopPlayAdventure
{
	// Token: 0x060018FD RID: 6397 RVA: 0x0010F841 File Offset: 0x0010DC41
	public PsUITopPlayAdventureSocial(Action _restartAction = null, Action _pauseAction = null)
		: base(_restartAction, _pauseAction)
	{
	}

	// Token: 0x060018FE RID: 6398 RVA: 0x0010F84B File Offset: 0x0010DC4B
	public override void CreateCoinArea()
	{
	}

	// Token: 0x060018FF RID: 6399 RVA: 0x0010F850 File Offset: 0x0010DC50
	public override void CreateRestartArea(UIComponent _parent)
	{
		this.m_pauseButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_pauseButton.SetIcon("hud_icon_pause", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_pauseButton.SetOrangeColors(true);
		this.m_restartButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
	}
}
