using System;

// Token: 0x020002A9 RID: 681
public class PsUITopGarage : UICanvas
{
	// Token: 0x06001486 RID: 5254 RVA: 0x000D2034 File Offset: 0x000D0434
	public PsUITopGarage(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.13f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetSound("/UI/ButtonBack");
		this.m_exitButton.SetOrangeColors(true);
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x000D20F4 File Offset: 0x000D04F4
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x0400175E RID: 5982
	private PsUIGenericButton m_exitButton;
}
