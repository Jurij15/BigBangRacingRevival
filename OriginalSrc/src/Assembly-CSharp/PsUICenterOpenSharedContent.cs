using System;

// Token: 0x0200017A RID: 378
public class PsUICenterOpenSharedContent : PsUIHeaderedCanvas
{
	// Token: 0x06000C95 RID: 3221 RVA: 0x000770B4 File Offset: 0x000754B4
	public PsUICenterOpenSharedContent(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.BgDrawhandler));
		this.m_title = PsStrings.Get(StringID.SHARE_LEVEL_TITLE);
		this.m_description = PsStrings.Get(StringID.SHARE_LEVEL_DESC);
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x000771E0 File Offset: 0x000755E0
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIComponent uicomponent = new UIComponent(uihorizontalList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetHeight(0.055f, RelativeTo.ScreenHeight);
		uicomponent.SetWidth(1f, RelativeTo.ParentWidth);
		uicomponent.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, this.m_title, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x00077284 File Offset: 0x00075684
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, this.m_description, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		uitextbox.SetWidth(0.9f, RelativeTo.ParentWidth);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.05f, -0.05f, RelativeTo.ScreenHeight);
		this.m_play = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_play.SetText("Yes", 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_play.SetGreenColors(true);
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x0007734E File Offset: 0x0007574E
	public override void Step()
	{
		if (this.m_play.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Proceed");
		}
		base.Step();
	}

	// Token: 0x04000C0A RID: 3082
	private PsUIGenericButton m_play;

	// Token: 0x04000C0B RID: 3083
	private string m_title;

	// Token: 0x04000C0C RID: 3084
	private string m_description;
}
