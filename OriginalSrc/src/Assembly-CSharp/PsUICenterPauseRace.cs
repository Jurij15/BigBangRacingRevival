using System;

// Token: 0x020002B7 RID: 695
public class PsUICenterPauseRace : UICanvas
{
	// Token: 0x060014BA RID: 5306 RVA: 0x000D86DC File Offset: 0x000D6ADC
	public PsUICenterPauseRace(UIComponent _parent)
		: base(_parent, false, "CenterContent", null, string.Empty)
	{
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetHeight(0.85f, RelativeTo.ParentHeight);
		this.SetVerticalAlign(0f);
		this.SetMargins(0f, 0f, 0f, 0.05f, RelativeTo.ScreenHeight);
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DebriefBackground));
		this.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this, "Center");
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetVerticalAlign(0f);
		this.m_resumeButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.0085f, "Button");
		this.m_resumeButton.SetText(PsStrings.Get(StringID.RESUME), 0.07f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_resumeButton.SetSound("/UI/ResumeGame");
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x000D87E0 File Offset: 0x000D6BE0
	public override void Step()
	{
		if (this.m_resumeButton != null && this.m_resumeButton.m_TC.p_entity != null && this.m_resumeButton.m_TC.p_entity.m_active && (this.m_resumeButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Resume");
		}
		base.Step();
	}

	// Token: 0x04001787 RID: 6023
	private PsUIGenericButton m_resumeButton;
}
