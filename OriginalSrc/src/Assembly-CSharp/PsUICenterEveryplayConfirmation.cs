using System;

// Token: 0x020002A2 RID: 674
public class PsUICenterEveryplayConfirmation : PsUIHeaderedCanvas
{
	// Token: 0x06001451 RID: 5201 RVA: 0x000CEBFC File Offset: 0x000CCFFC
	public PsUICenterEveryplayConfirmation(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.Destroy();
		new UITextbox(this, false, string.Empty, "Hey! We recorded a replay of your actions! Wanna see it and share it?", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetVerticalAlign(0f);
		uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
		this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_ok.SetText("Yes!", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_ok.SetHeight(0.075f, RelativeTo.ScreenHeight);
		this.m_ok.SetOrangeColors(true);
		this.m_cancel = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_cancel.SetText("No", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_cancel.SetHeight(0.075f, RelativeTo.ScreenHeight);
		this.m_cancel.SetOrangeColors(true);
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x000CEDDC File Offset: 0x000CD1DC
	public override void Step()
	{
		if (this.m_ok.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Proceed");
		}
		else if (this.m_cancel != null && this.m_cancel.m_TC.p_entity != null && this.m_cancel.m_TC.p_entity.m_active && (this.m_cancel.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001717 RID: 5911
	private PsUIGenericButton m_ok;

	// Token: 0x04001718 RID: 5912
	private PsUIGenericButton m_cancel;
}
