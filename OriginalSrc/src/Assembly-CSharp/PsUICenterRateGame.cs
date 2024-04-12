using System;

// Token: 0x020002C0 RID: 704
public class PsUICenterRateGame : UICanvas
{
	// Token: 0x060014DE RID: 5342 RVA: 0x000D9D00 File Offset: 0x000D8100
	public PsUICenterRateGame(UIComponent _parent)
		: base(_parent, false, "CenterContent", null, string.Empty)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DebriefBackground));
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(0.85f, RelativeTo.ParentHeight);
		this.SetVerticalAlign(0f);
		this.RemoveDrawHandler();
		this.m_offensiveButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		this.m_offensiveButton.SetText("Offensive", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_offensiveButton.SetRedColors();
		this.m_offensiveButton.SetAlign(0.02f, 0.04f);
		UIVerticalList uiverticalList = new UIVerticalList(this, "Center");
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uiverticalList.SetAlign(0.5f, 0.1f);
		uiverticalList.RemoveDrawHandler();
		this.m_titleText = new UIText(uiverticalList, false, string.Empty, "Anonymous thumbs", PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.04f, RelativeTo.ScreenHeight, "ffffff", "343b2e");
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, "Center");
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.RemoveDrawHandler();
		this.m_twoThumbsUpButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_twoThumbsUpButton.SetIcon("menu_thumbs_two_up_off", 0.1f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_twoThumbsUpButton.SetSpeechHandle(SpeechBubbleHandlePosition.BottomLeft, SpeechBubbleHandleType.SmallToCenter);
		this.m_thumbsUpButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_thumbsUpButton.SetIcon("menu_thumbs_up_off", 0.1f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_thumbsUpButton.SetSpeechHandle(SpeechBubbleHandlePosition.BottomLeft, SpeechBubbleHandleType.SmallToCenter);
		this.m_thumbsDownButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_thumbsDownButton.SetIcon("menu_thumbs_down_off", 0.1f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_thumbsDownButton.SetSpeechHandle(SpeechBubbleHandlePosition.BottomRight, SpeechBubbleHandleType.SmallToCenter);
		this.m_twoThumbsDownButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_twoThumbsDownButton.SetIcon("menu_thumbs_two_down_off", 0.1f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_twoThumbsDownButton.SetSpeechHandle(SpeechBubbleHandlePosition.BottomRight, SpeechBubbleHandleType.SmallToCenter);
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x000D9F8C File Offset: 0x000D838C
	public override void Step()
	{
		if (this.m_twoThumbsUpButton != null && this.m_twoThumbsUpButton.m_hit && !this.m_ratingDone)
		{
			this.m_ratingDone = true;
			PsMetagameManager.SendRating(PsRating.Elated, PsState.m_activeGameLoop, PsState.m_activeMinigame, false);
			PsState.m_activeGameLoop.m_minigameMetaData.rating = PsRating.Elated;
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		else if (this.m_thumbsUpButton != null && this.m_thumbsUpButton.m_hit && !this.m_ratingDone)
		{
			this.m_ratingDone = true;
			PsMetagameManager.SendRating(PsRating.Positive, PsState.m_activeGameLoop, PsState.m_activeMinigame, false);
			PsState.m_activeGameLoop.m_minigameMetaData.rating = PsRating.Positive;
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		else if (this.m_thumbsDownButton != null && this.m_thumbsDownButton.m_hit && !this.m_ratingDone)
		{
			this.m_ratingDone = true;
			PsMetagameManager.SendRating(PsRating.Negative, PsState.m_activeGameLoop, PsState.m_activeMinigame, false);
			PsState.m_activeGameLoop.m_minigameMetaData.rating = PsRating.Negative;
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		else if (this.m_twoThumbsDownButton != null && this.m_twoThumbsDownButton.m_hit && !this.m_ratingDone)
		{
			this.m_ratingDone = true;
			PsMetagameManager.SendRating(PsRating.Rejecting, PsState.m_activeGameLoop, PsState.m_activeMinigame, false);
			PsState.m_activeGameLoop.m_minigameMetaData.rating = PsRating.Rejecting;
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		else if (this.m_offensiveButton.m_hit)
		{
			this.m_offensive = new PsUIBasePopup(typeof(PsUICenterOffensive), typeof(PsUITopBanner), null, null, true, false, InitialPage.Center, false, false, false);
			this.m_offensive.SetAction("Ok", new Action(this.OkOffensive));
			this.m_offensive.SetAction("Cancel", new Action(this.CancelOffensive));
		}
		base.Step();
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x000DA1B2 File Offset: 0x000D85B2
	public void OkOffensive()
	{
		this.m_offensive.Destroy();
		this.m_offensive = null;
		(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x000DA1DC File Offset: 0x000D85DC
	public void CancelOffensive()
	{
		this.m_offensive.Destroy();
		this.m_offensive = null;
	}

	// Token: 0x04001798 RID: 6040
	private PsUIGenericButton m_twoThumbsUpButton;

	// Token: 0x04001799 RID: 6041
	private PsUIGenericButton m_thumbsUpButton;

	// Token: 0x0400179A RID: 6042
	private PsUIGenericButton m_thumbsDownButton;

	// Token: 0x0400179B RID: 6043
	private PsUIGenericButton m_twoThumbsDownButton;

	// Token: 0x0400179C RID: 6044
	private UIText m_titleText;

	// Token: 0x0400179D RID: 6045
	private PsUIGenericButton m_offensiveButton;

	// Token: 0x0400179E RID: 6046
	private PsUIBasePopup m_offensive;

	// Token: 0x0400179F RID: 6047
	private bool m_ratingDone;
}
