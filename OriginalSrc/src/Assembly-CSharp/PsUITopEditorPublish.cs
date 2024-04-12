using System;

// Token: 0x020002A6 RID: 678
public class PsUITopEditorPublish : UICanvas
{
	// Token: 0x06001463 RID: 5219 RVA: 0x000D02F8 File Offset: 0x000CE6F8
	public PsUITopEditorPublish(UIComponent _parent)
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
		PsUIInfoBar psUIInfoBar = new PsUIInfoBar(this, string.Empty, false);
		psUIInfoBar.SetVerticalAlign(0f);
		psUIInfoBar.SetText("Once you GO LIVE, you can not make changes to your Level.");
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x000D03DC File Offset: 0x000CE7DC
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Back");
		}
		base.Step();
	}

	// Token: 0x0400172B RID: 5931
	private PsUIGenericButton m_exitButton;
}
