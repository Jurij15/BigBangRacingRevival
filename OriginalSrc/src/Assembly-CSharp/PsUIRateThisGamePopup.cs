using System;

// Token: 0x020003A0 RID: 928
public class PsUIRateThisGamePopup : PsUIHeaderedCanvas
{
	// Token: 0x06001A7B RID: 6779 RVA: 0x00127224 File Offset: 0x00125624
	public PsUIRateThisGamePopup(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.Initialize();
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001A7C RID: 6780 RVA: 0x0012730C File Offset: 0x0012570C
	public virtual void Initialize()
	{
	}

	// Token: 0x06001A7D RID: 6781 RVA: 0x00127310 File Offset: 0x00125710
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, this.m_headerText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x06001A7E RID: 6782 RVA: 0x00127388 File Offset: 0x00125788
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, this.m_contentText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetSpacing(0.1f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
		this.m_continue = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continue.SetAlign(1f, 1f);
		this.m_continue.SetText(this.m_continueButtonText, 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_continue.SetGreenColors(true);
		this.m_cancel = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_cancel.SetText(this.m_cancelButtonText, 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_cancel.SetBlueColors(true);
	}

	// Token: 0x06001A7F RID: 6783 RVA: 0x001274B4 File Offset: 0x001258B4
	public override void Step()
	{
		if (this.m_continue != null && this.m_continue.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		else if (this.m_cancel != null && this.m_cancel.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Cancel");
		}
		base.Step();
	}

	// Token: 0x04001CFC RID: 7420
	private PsUIGenericButton m_continue;

	// Token: 0x04001CFD RID: 7421
	private PsUIGenericButton m_cancel;

	// Token: 0x04001CFE RID: 7422
	protected string m_headerText;

	// Token: 0x04001CFF RID: 7423
	protected string m_contentText;

	// Token: 0x04001D00 RID: 7424
	protected string m_continueButtonText;

	// Token: 0x04001D01 RID: 7425
	protected string m_cancelButtonText;
}
