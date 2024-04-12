using System;

// Token: 0x020002F7 RID: 759
public class PsUITopWinRacingSocial : PsUITopWinRacing
{
	// Token: 0x06001650 RID: 5712 RVA: 0x000E97D0 File Offset: 0x000E7BD0
	public PsUITopWinRacingSocial(UIComponent _parent)
		: base(_parent)
	{
		this.m_exitButton = new PsUIGenericButton(this.m_leftArea, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetSound("/UI/ExitLevel");
		this.m_exitButton.SetIcon("hud_icon_menu_exit", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetOrangeColors(true);
		this.m_exitButton.SetDepthOffset(-5f);
		this.m_exitButton.MoveToIndexAtParentsChildList(0);
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x000E9868 File Offset: 0x000E7C68
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001910 RID: 6416
	private PsUIGenericButton m_exitButton;
}
