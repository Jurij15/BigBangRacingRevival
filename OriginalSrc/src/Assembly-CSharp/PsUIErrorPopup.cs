using System;

// Token: 0x0200039C RID: 924
public class PsUIErrorPopup : PsUIHeaderedCanvas
{
	// Token: 0x06001A71 RID: 6769 RVA: 0x00126F5C File Offset: 0x0012535C
	public PsUIErrorPopup(UIComponent _parent)
		: base(_parent, "ErrorPopup", false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		CameraS.SetAsOverlayCamera(this.m_camera);
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x00127068 File Offset: 0x00125468
	public void CreateHeader(string _title)
	{
		if (!string.IsNullOrEmpty(_title))
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_header, string.Empty);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
			uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
			uihorizontalList.SetHorizontalAlign(0.5f);
			UIText uitext = new UIText(uihorizontalList, false, string.Empty, _title, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
		}
		else
		{
			this.m_header.Destroy();
			this.m_header = null;
		}
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x00127100 File Offset: 0x00125500
	public void CreateContent(string _error, string _proceedButtonTitle)
	{
		if (!string.IsNullOrEmpty(_error))
		{
			new UITextbox(this, false, string.Empty, _error, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.RemoveTouchAreas();
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(0f);
		uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
		this.m_proceedButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_proceedButton.SetText(_proceedButtonTitle, 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_proceedButton.SetOrangeColors(true);
		this.m_proceedButton.Update();
		EntityManager.RemoveAllTagsFromEntity(this.m_container.m_TC.p_entity, true);
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x001271E8 File Offset: 0x001255E8
	public override void Step()
	{
		if (this.m_proceedButton != null && this.m_proceedButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Proceed");
			return;
		}
		base.Step();
	}

	// Token: 0x04001CF7 RID: 7415
	private UIVerticalList m_verticalArea;

	// Token: 0x04001CF8 RID: 7416
	public PsUIGenericButton m_proceedButton;
}
