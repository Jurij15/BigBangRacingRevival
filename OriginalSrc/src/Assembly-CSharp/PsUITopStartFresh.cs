using System;

// Token: 0x020002DF RID: 735
public class PsUITopStartFresh : UICanvas
{
	// Token: 0x060015BB RID: 5563 RVA: 0x000E1E78 File Offset: 0x000E0278
	public PsUITopStartFresh(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		PsMetagameManager.HideResources();
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetSound("/UI/ExitLevel");
		this.m_exitButton.SetIcon("hud_icon_menu_exit", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetOrangeColors(true);
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x000E1F40 File Offset: 0x000E0340
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001869 RID: 6249
	private PsUIGenericButton m_exitButton;
}
