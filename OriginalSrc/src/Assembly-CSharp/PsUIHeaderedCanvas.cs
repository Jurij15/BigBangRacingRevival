using System;

// Token: 0x02000322 RID: 802
public class PsUIHeaderedCanvas : UIHeaderedCanvas
{
	// Token: 0x06001794 RID: 6036 RVA: 0x00076F0A File Offset: 0x0007530A
	public PsUIHeaderedCanvas(UIComponent _parent, string _tag = "", bool _hasCloseButton = true, float _headerHeight = 0.125f, RelativeTo _headerHeightRelativeTo = RelativeTo.ScreenHeight, float _footerHeight = 0f, RelativeTo _footerHeightRelativeTo = RelativeTo.ScreenHeight)
		: base(_parent, _tag, _headerHeight, _headerHeightRelativeTo, _footerHeight, _footerHeightRelativeTo)
	{
		if (_hasCloseButton)
		{
			this.CreateCloseButton();
		}
	}

	// Token: 0x06001795 RID: 6037 RVA: 0x00076F28 File Offset: 0x00075328
	public virtual void CreateCloseButton()
	{
		UICanvas uicanvas = new UICanvas(this.m_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.125f, 0.125f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(1f, 1f);
		uicanvas.SetMargins(0.25f, -0.25f, -0.25f, 0.25f, RelativeTo.OwnHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetDepthOffset(-20f);
		this.m_exitButton = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetOrangeColors(true);
		this.m_exitButton.SetSound("/UI/ButtonBack");
		this.m_exitButton.SetIcon("menu_icon_close", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
	}

	// Token: 0x06001796 RID: 6038 RVA: 0x00076FFC File Offset: 0x000753FC
	public override void Step()
	{
		string text = null;
		if (this.GetRoot() is PsUIBasePopup)
		{
			text = (this.GetRoot() as PsUIBasePopup).m_guid;
		}
		if ((this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && this.m_exitButton.m_hit) || Main.AndroidBackButtonPressed(text))
		{
			FrbMetrics.SetPreviousScreen();
			this.CallAction("Exit");
			TouchAreaS.CancelAllTouches(null);
		}
		base.Step();
	}

	// Token: 0x06001797 RID: 6039 RVA: 0x0007709D File Offset: 0x0007549D
	public void CallAction(string _action)
	{
		(this.GetRoot() as PsUIBasePopup).CallAction(_action);
	}

	// Token: 0x04001A69 RID: 6761
	protected PsUIGenericButton m_exitButton;
}
