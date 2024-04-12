using System;

// Token: 0x0200039F RID: 927
public class PsUIContactUsPopup : PsUIRateThisGamePopup
{
	// Token: 0x06001A79 RID: 6777 RVA: 0x001275E7 File Offset: 0x001259E7
	public PsUIContactUsPopup(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x001275F0 File Offset: 0x001259F0
	public override void Initialize()
	{
		base.Initialize();
		this.m_headerText = PsStrings.Get(StringID.RATE_DIALOG_HEADER_NEGATIVE);
		this.m_contentText = PsStrings.Get(StringID.RATE_DIALOG_CONTENT_NEGATIVE);
		this.m_continueButtonText = PsStrings.Get(StringID.RATE_DIALOG_BUTTON_NEGATIVE_YES);
		this.m_cancelButtonText = PsStrings.Get(StringID.RATE_DIALOG_BUTTON_NEGATIVE_NO);
	}
}
