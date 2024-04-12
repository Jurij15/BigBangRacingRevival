using System;

// Token: 0x02000329 RID: 809
public class PsUITopBackButton : UICanvas
{
	// Token: 0x060017C1 RID: 6081 RVA: 0x00100638 File Offset: 0x000FEA38
	public PsUITopBackButton(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetSound("/UI/ButtonBack");
		this.m_exitButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetSound("/UI/ButtonBack");
		this.m_exitButton.SetOrangeColors(true);
	}

	// Token: 0x060017C2 RID: 6082 RVA: 0x00100708 File Offset: 0x000FEB08
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			TouchAreaS.CancelAllTouches(null);
		}
		base.Step();
	}

	// Token: 0x04001A96 RID: 6806
	private PsUIGenericButton m_exitButton;
}
