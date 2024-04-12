using System;

// Token: 0x020002B8 RID: 696
public class PsUICenterPauseSignal : UICanvas
{
	// Token: 0x060014BC RID: 5308 RVA: 0x000D8870 File Offset: 0x000D6C70
	public PsUICenterPauseSignal(UIComponent _parent)
		: base(_parent, false, "CenterContent", null, string.Empty)
	{
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetHeight(0.85f, RelativeTo.ParentHeight);
		this.SetVerticalAlign(0f);
		this.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this, "Center");
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uiverticalList.RemoveDrawHandler();
		this.m_resumeButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_resumeButton.SetText(PsStrings.Get(StringID.RESUME), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_resumeButton.SetGreenColors(true);
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x000D8922 File Offset: 0x000D6D22
	public override void Step()
	{
		if (this.m_resumeButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Resume");
		}
		base.Step();
	}

	// Token: 0x04001789 RID: 6025
	private PsUIGenericButton m_resumeButton;
}
