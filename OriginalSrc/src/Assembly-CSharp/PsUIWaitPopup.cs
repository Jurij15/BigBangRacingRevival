using System;

// Token: 0x020003D2 RID: 978
public class PsUIWaitPopup : PsUIPopup
{
	// Token: 0x06001B95 RID: 7061 RVA: 0x00133C14 File Offset: 0x00132014
	public PsUIWaitPopup(Action _proceed = null)
		: base(_proceed, null, false, "Popup")
	{
		this.m_callBack = _proceed;
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(null);
		new PsUILoadingAnimation(uiverticalList, false);
		new UIText(uiverticalList, false, "Loading", PsStrings.Get(StringID.LOADING), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.025f, RelativeTo.ScreenShortest, null, null);
		this.Update();
		base.UpdatePopupBG();
	}

	// Token: 0x04001DF8 RID: 7672
	private UIVerticalList m_verticalArea;

	// Token: 0x04001DF9 RID: 7673
	private Action m_callBack;
}
