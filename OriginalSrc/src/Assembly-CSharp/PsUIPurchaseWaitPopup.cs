using System;

// Token: 0x020003CE RID: 974
public class PsUIPurchaseWaitPopup : PsUIPopup
{
	// Token: 0x06001B88 RID: 7048 RVA: 0x00133588 File Offset: 0x00131988
	public PsUIPurchaseWaitPopup(Action _proceed)
		: base(_proceed, null, false, "Popup")
	{
		this.m_callBack = _proceed;
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(null);
		new PsUILoadingAnimation(uiverticalList, false);
		new UIText(uiverticalList, false, PsStrings.Get(StringID.LOADING), PsStrings.Get(StringID.SHOP_PROMPT_STORELOADING), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.025f, RelativeTo.ScreenShortest, null, null);
		this.Update();
		base.UpdatePopupBG();
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x0013361F File Offset: 0x00131A1F
	public void Close()
	{
		if (this.m_callBack != null)
		{
			this.m_callBack.Invoke();
		}
		this.Destroy();
	}

	// Token: 0x04001DE9 RID: 7657
	private UIVerticalList m_verticalArea;

	// Token: 0x04001DEA RID: 7658
	private Action m_callBack;
}
