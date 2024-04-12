using System;

// Token: 0x0200039E RID: 926
public class PsUIPleaseRatePopup : PsUIRateThisGamePopup
{
	// Token: 0x06001A77 RID: 6775 RVA: 0x0012758B File Offset: 0x0012598B
	public PsUIPleaseRatePopup(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001A78 RID: 6776 RVA: 0x00127594 File Offset: 0x00125994
	public override void Initialize()
	{
		base.Initialize();
		this.m_headerText = PsStrings.Get(StringID.RATE_DIALOG_HEADER_POSITIVE);
		this.m_contentText = PsStrings.Get(StringID.RATE_DIALOG_CONTENT_POSITIVE);
		this.m_continueButtonText = PsStrings.Get(StringID.RATE_DIALOG_BUTTON_POSITIVE_YES);
		this.m_cancelButtonText = PsStrings.Get(StringID.RATE_DIALOG_BUTTON_POSITIVE_NO);
	}
}
