using System;

// Token: 0x020002B3 RID: 691
public class PsUICenterLoseRacing : PsUICenterStartRacing
{
	// Token: 0x060014B1 RID: 5297 RVA: 0x000D83DA File Offset: 0x000D67DA
	public PsUICenterLoseRacing(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x000D83E4 File Offset: 0x000D67E4
	public override void Step()
	{
		if (GameScene.m_lowPerformance && !PlayerPrefsX.GetLowEndPrompt() && this.m_lowEndPrompt == null && !this.m_lowEndShown)
		{
			this.m_lowEndPrompt = new PsUIBasePopup(typeof(PsUICenterLowPerformancePrompt), null, null, null, false, true, InitialPage.Center, true, false, false);
			this.m_lowEndPrompt.SetAction("Exit", delegate
			{
				PlayerPrefsX.SetLowEndPrompt(true);
				this.m_lowEndPrompt.Destroy();
				this.m_lowEndPrompt = null;
			});
			this.m_lowEndShown = true;
		}
		base.Step();
	}
}
