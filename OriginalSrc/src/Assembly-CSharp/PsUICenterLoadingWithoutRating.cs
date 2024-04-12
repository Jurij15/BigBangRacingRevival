using System;

// Token: 0x020003AD RID: 941
public class PsUICenterLoadingWithoutRating : PsUICenterRatingLoading
{
	// Token: 0x06001AF0 RID: 6896 RVA: 0x0012BE78 File Offset: 0x0012A278
	public PsUICenterLoadingWithoutRating(UIComponent _parent)
		: base(_parent)
	{
		this.m_continuebutton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.01f, "Button");
		this.m_continuebutton.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_continuebutton.SetGreenColors(true);
		this.m_continuebutton.SetText(PsStrings.Get(StringID.CONTINUE), 0.04f, 0.3f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		this.m_continuebutton.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_continuebutton.SetHorizontalAlign(0.95f);
		this.m_continuebutton.SetVerticalAlign(0.1f);
		this.m_continuebutton.SetDepthOffset(-10f);
	}

	// Token: 0x06001AF1 RID: 6897 RVA: 0x0012BF38 File Offset: 0x0012A338
	protected override void CreateCreatorInfo(UIComponent _parent)
	{
		this.m_creator = new PsUICreatorInfo(_parent, true, false, true, true, true, false);
		this.m_creator.SetHorizontalAlign(0.5f);
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x0012BF5C File Offset: 0x0012A35C
	public override void CreateRatingButtonList(UIComponent _parent)
	{
		(_parent as UIVerticalList).SetSpacing(0.01f, RelativeTo.ScreenHeight);
		string text = string.Empty;
		string text2 = string.Empty;
		switch (PsState.m_activeGameLoop.GetRating())
		{
		case PsRating.SuperLike:
			text = "menu_thumbs_mega";
			text2 = PsStrings.Get(StringID.RATING_MEGALIKE);
			break;
		case PsRating.Elated:
			text = "menu_thumbs_two_up_off";
			text2 = PsStrings.Get(StringID.RATING_DOUBLE_LIKE);
			break;
		case PsRating.Positive:
			text = "menu_thumbs_up_off";
			text2 = PsStrings.Get(StringID.RATING_LIKE);
			break;
		case PsRating.Negative:
			text = "menu_thumbs_down_off";
			text2 = PsStrings.Get(StringID.RATING_DISLIKE);
			break;
		case PsRating.Rejecting:
			text = "menu_thumbs_two_down_off";
			text2 = PsStrings.Get(StringID.RATING_DOUBLE_DISLIKE);
			break;
		}
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
		uifittedSprite.SetHeight(0.175f, RelativeTo.ScreenHeight);
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.35f, RelativeTo.ScreenWidth);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", null);
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x0012C0A8 File Offset: 0x0012A4A8
	public override void Step()
	{
		if (this.m_continuebutton != null && this.m_continuebutton.m_hit && !this.m_continuePressed)
		{
			this.m_continuePressed = true;
			TouchAreaS.CancelAllTouches(null);
			TouchAreaS.Disable();
			this.Proceed();
		}
		base.Step();
	}

	// Token: 0x04001D5A RID: 7514
	private bool m_continuePressed;
}
