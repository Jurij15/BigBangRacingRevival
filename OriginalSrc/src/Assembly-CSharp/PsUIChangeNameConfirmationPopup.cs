using System;

// Token: 0x0200030C RID: 780
public class PsUIChangeNameConfirmationPopup : PsUIHeaderedCanvas
{
	// Token: 0x06001713 RID: 5907 RVA: 0x000F873C File Offset: 0x000F6B3C
	public PsUIChangeNameConfirmationPopup(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.m_headerText = PsStrings.Get(StringID.NAME_CHANGE_CONFIRMATION_HEADER);
		this.m_contentText = PsStrings.Get(StringID.NAME_CHANGE_CONFIRMATION_TEXT);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001714 RID: 5908 RVA: 0x000F8834 File Offset: 0x000F6C34
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, this.m_headerText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x06001715 RID: 5909 RVA: 0x000F88AC File Offset: 0x000F6CAC
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, this.m_contentText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		uitextbox.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetSpacing(0.07f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
		this.m_open = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_open.SetGreenColors(true);
		this.m_open.SetText(PsStrings.Get(StringID.OK), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_cancel = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_cancel.SetRedColors();
		this.m_cancel.SetText(PsStrings.Get(StringID.CANCEL), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x000F89DD File Offset: 0x000F6DDD
	public void SetPrice(int _price)
	{
		if (this.m_open != null)
		{
			this.m_open.SetDiamondPrice(_price, 0.03f);
			this.m_open.Update();
		}
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x000F8A08 File Offset: 0x000F6E08
	public override void Step()
	{
		if (this.m_open != null && this.m_open.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Ok");
		}
		else if (this.m_cancel != null && this.m_cancel.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Cancel");
		}
		base.Step();
	}

	// Token: 0x040019DD RID: 6621
	private PsUIGenericButton m_open;

	// Token: 0x040019DE RID: 6622
	private PsUIGenericButton m_cancel;

	// Token: 0x040019DF RID: 6623
	protected string m_headerText;

	// Token: 0x040019E0 RID: 6624
	protected string m_contentText;
}
