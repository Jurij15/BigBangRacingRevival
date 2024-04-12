using System;

// Token: 0x0200039D RID: 925
public class PsUIDoYouLikeThisGamePopup : PsUIRateThisGamePopup
{
	// Token: 0x06001A75 RID: 6773 RVA: 0x0012752E File Offset: 0x0012592E
	public PsUIDoYouLikeThisGamePopup(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x00127538 File Offset: 0x00125938
	public override void Initialize()
	{
		base.Initialize();
		this.m_headerText = PsStrings.Get(StringID.RATE_DIALOG_HEADER_START);
		this.m_contentText = PsStrings.Get(StringID.RATE_DIALOG_CONTENT_START);
		this.m_continueButtonText = PsStrings.Get(StringID.RATE_DIALOG_BUTTON_START_YES);
		this.m_cancelButtonText = PsStrings.Get(StringID.RATE_DIALOG_BUTTON_START_NO);
	}
}
