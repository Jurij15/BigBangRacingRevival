using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A8 RID: 936
public class PsUICenterRatingFresh : PsUICenterRatingLoading
{
	// Token: 0x06001AC3 RID: 6851 RVA: 0x0012B574 File Offset: 0x00129974
	public PsUICenterRatingFresh(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x0012B580 File Offset: 0x00129980
	public override void CreateRatingButtons(UIComponent _parent)
	{
		float num = 0.15f;
		float num2 = 0.11f;
		this.m_upDouble = new PsUIRatingLikeButtonDouble(_parent, num, num2, "menu_thumbs_two_up_off");
		this.m_up = new PsUIRatingLikeButton(_parent, num, num2, "menu_thumbs_up_off");
		this.m_down = new PsUIRatingDislikeButton(_parent, num, num2, "menu_thumbs_down_off");
		this.m_downDouble = new PsUIRatingDislikeDoubleButton(_parent, num, num2, "menu_thumbs_two_down_off");
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.1f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(1f, 1f);
		uicanvas.SetMargins(0f, 0.05f, 0.02f, 0f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetDepthOffset(-10f);
		this.m_offensiveButton = new PsUIRatingAbuseButton(uicanvas, num, num2, null);
		this.m_offensiveButton.SetAlign(1f, 1f);
		this.m_ratingButtonList = new List<PsUIRatingButton>();
		this.m_ratingButtonList.Add(this.m_upDouble);
		this.m_ratingButtonList.Add(this.m_up);
		this.m_ratingButtonList.Add(this.m_down);
		this.m_ratingButtonList.Add(this.m_downDouble);
		this.m_ratingButtonList.Add(this.m_offensiveButton);
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x0012B6D0 File Offset: 0x00129AD0
	protected override void CreateRatingBar(UIComponent _parent, int _positive, int _negative)
	{
		this.m_ratingBarParent = _parent;
		this.m_positive = _positive;
		this.m_negative = _negative;
	}

	// Token: 0x06001AC6 RID: 6854 RVA: 0x0012B6E8 File Offset: 0x00129AE8
	protected override void RatingBarEffect(Action _postAction)
	{
		this.m_proceedTimer = TimerS.AddComponent(this.m_TC.p_entity, "proceed timer", 1f, 0f, false, null);
		this.m_ratingBar = new PsUIRatingBar(this.m_ratingBarParent, this.m_positive, this.m_negative);
		this.m_ratingBar.MoveToIndexAtParentsChildList(0);
		this.m_ratingBar.SetWidth(0.3f, RelativeTo.ScreenWidth);
		this.m_ratingBar.SetHeight(0.0675f, RelativeTo.ScreenHeight);
		this.m_ratingBar.m_parent.Update();
		TweenC tweenC = TweenS.AddTransformTween(this.m_ratingBar.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.zero, Vector3.one, 0.5f, 0f, false);
		TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
		{
			TweenS.RemoveComponent(_c);
			_postAction.Invoke();
		});
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x0012B7C0 File Offset: 0x00129BC0
	protected override void ButtonRating(PsRating _rating, int _sound, int _timesRated, int _timesLiked, PsUIRatingButton _buttonPressed, bool skipped = false)
	{
		PsMetagameManager.m_playerStats.newLevelsRated++;
		PsState.m_activeGameLoop.m_minigameMetaData.timesRated += _timesRated;
		PsState.m_activeGameLoop.m_minigameMetaData.timesLiked += _timesLiked;
		this.m_ratingPressed = true;
		_buttonPressed.ButtonPressed();
		base.RemoveRatingButtons(_buttonPressed);
		this.SendRating(_rating, _sound, skipped);
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x0012B830 File Offset: 0x00129C30
	protected override void ButtonStep()
	{
		if (this.m_downDouble != null && this.m_downDouble.m_hit && !this.m_ratingPressed && !this.m_offensiveOpened)
		{
			this.RatingBarEffect(new Action(this.DoubleThumbDownPressed));
			this.ButtonRating(PsRating.Rejecting, -2, 2, 0, this.m_downDouble, false);
		}
		else if (this.m_upDouble != null && this.m_upDouble.m_hit && !this.m_ratingPressed && !this.m_offensiveOpened)
		{
			this.RatingBarEffect(new Action(this.DoubleThumbUpPressed));
			this.ButtonRating(PsRating.Elated, 2, 2, 2, this.m_upDouble, false);
		}
		else
		{
			base.ButtonStep();
		}
	}

	// Token: 0x06001AC9 RID: 6857 RVA: 0x0012B8F6 File Offset: 0x00129CF6
	protected void DoubleThumbUpPressed()
	{
		this.m_ratingBar.ThumbsUpDouble();
	}

	// Token: 0x06001ACA RID: 6858 RVA: 0x0012B903 File Offset: 0x00129D03
	protected void DoubleThumbDownPressed()
	{
		this.m_ratingBar.ThumbsDownDouble();
	}

	// Token: 0x04001D38 RID: 7480
	private UIComponent m_ratingBarParent;

	// Token: 0x04001D39 RID: 7481
	private int m_positive;

	// Token: 0x04001D3A RID: 7482
	private int m_negative;
}
