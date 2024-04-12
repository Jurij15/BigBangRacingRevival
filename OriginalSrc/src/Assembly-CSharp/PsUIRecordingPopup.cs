using System;

// Token: 0x0200034B RID: 843
public class PsUIRecordingPopup : PsUIHeaderedCanvas
{
	// Token: 0x060018B6 RID: 6326 RVA: 0x0010D3A0 File Offset: 0x0010B7A0
	public PsUIRecordingPopup(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
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

	// Token: 0x060018B7 RID: 6327 RVA: 0x0010D484 File Offset: 0x0010B884
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.REPLAY_KIT_STILL_RECORDING_HEADLINE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x0010D500 File Offset: 0x0010B900
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, PsStrings.Get(StringID.REPLAY_KIT_STILL_RECORDING_BODY), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		uitextbox.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetSpacing(0.1f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
		this.m_preview = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_preview.SetAlign(1f, 1f);
		this.m_preview.SetText(PsStrings.Get(StringID.REPLAY_KIT_PREVIEW_SAVE), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_preview.SetGreenColors(true);
		this.m_continue = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continue.SetAlign(1f, 1f);
		this.m_continue.SetText(PsStrings.Get(StringID.REPLAY_KIT_DISCARD), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_continue.SetGreenColors(true);
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x0010D668 File Offset: 0x0010BA68
	public override void Step()
	{
		if (this.m_continue != null && this.m_continue.m_hit)
		{
			ScreenCapture.Stop();
			ScreenCapture.Discard();
			(this.GetRoot() as PsUIBasePopup).CallAction("Discard");
		}
		else if (this.m_preview != null && this.m_preview.m_hit)
		{
			ScreenCapture.Stop();
			ScreenCapture.Preview();
			(this.GetRoot() as PsUIBasePopup).CallAction("Preview");
		}
		else if (this.m_exitButton != null && this.m_exitButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Close");
		}
		base.Step();
	}

	// Token: 0x04001B5C RID: 7004
	public const string ACTION_CLOSE = "Close";

	// Token: 0x04001B5D RID: 7005
	public const string ACTION_PREVIEW = "Preview";

	// Token: 0x04001B5E RID: 7006
	public const string ACTION_DISCARD = "Discard";

	// Token: 0x04001B5F RID: 7007
	private PsUIGenericButton m_continue;

	// Token: 0x04001B60 RID: 7008
	private PsUIGenericButton m_preview;
}
