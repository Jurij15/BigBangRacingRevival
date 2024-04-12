using System;

// Token: 0x020003AB RID: 939
public class PsUICenterSkipRatingLoading : PsUICenterRatingLoading
{
	// Token: 0x06001AEA RID: 6890 RVA: 0x0012BD21 File Offset: 0x0012A121
	public PsUICenterSkipRatingLoading(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x0012BD2A File Offset: 0x0012A12A
	protected override void ButtonRating(PsRating _rating, int _sound, int _timesRated, int _timesLiked, PsUIRatingButton _buttonPressed, bool skipped = false)
	{
		base.ButtonRating(_rating, _sound, _timesRated, _timesLiked, _buttonPressed, true);
	}
}
