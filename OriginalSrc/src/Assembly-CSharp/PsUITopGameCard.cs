using System;

// Token: 0x0200029F RID: 671
public class PsUITopGameCard : UICanvas
{
	// Token: 0x06001442 RID: 5186 RVA: 0x000CDA10 File Offset: 0x000CBE10
	public PsUITopGameCard(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetSound("/UI/ButtonBack");
		this.m_exitButton.SetOrangeColors(true);
	}

	// Token: 0x06001443 RID: 5187 RVA: 0x000CDAD0 File Offset: 0x000CBED0
	public override void Step()
	{
		if (this.m_exitButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001701 RID: 5889
	private PsUIGenericButton m_exitButton;

	// Token: 0x04001702 RID: 5890
	private PsUILevelHeader m_header;
}
