using System;

// Token: 0x020003C6 RID: 966
public class PsUIPopupMenuLocked : PsUIPopup
{
	// Token: 0x06001B76 RID: 7030 RVA: 0x00132378 File Offset: 0x00130778
	public PsUIPopupMenuLocked(Action _proceed = null, Action _cancel = null)
		: base(_proceed, _cancel, false, "Popup")
	{
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(new UIDrawDelegate(PsUIPopup.PopupDrawHandler));
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.RemoveTouchAreas();
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_ok.SetText("Ok!", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_ok.SetGreenColors(true);
		this.m_ok.Update();
		this.Update();
		base.UpdatePopupBG();
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x0013246D File Offset: 0x0013086D
	public override void Step()
	{
		if (this.m_ok.m_hit)
		{
			this.Destroy();
			return;
		}
		base.Step();
	}

	// Token: 0x04001DD6 RID: 7638
	private PsUIGenericButton m_ok;
}
