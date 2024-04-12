using System;
using UnityEngine;

// Token: 0x020002FD RID: 765
public class PsUINewsBanner : UIHorizontalList
{
	// Token: 0x06001682 RID: 5762 RVA: 0x000ED50C File Offset: 0x000EB90C
	public PsUINewsBanner(UIComponent _parent, EventMessage _msg)
		: base(_parent, "NewsBanner")
	{
		this.m_msg = _msg;
		if (this.m_msg.eventStates != null && this.m_msg.eventStates.Count > 0)
		{
			this.SetState();
		}
		else
		{
			double num = (double)this.m_msg.localEndTime - Main.m_EPOCHSeconds;
			if (num <= 0.0)
			{
				this.m_currentTimeLeft = -1L;
			}
			else
			{
				this.m_timeLeft = (long)((int)(num + 0.5));
				this.m_currentTimeLeft = this.m_timeLeft - (long)PsUICenterNewsPopup.m_secondsSinceLoad;
			}
		}
		this.m_active = this.m_currentTimeLeft > 0L && !string.IsNullOrEmpty(this.m_msg.eventType) && (this.m_msg.eventType.ToLower().Contains("event") || this.m_msg.eventType.ToLower().Contains("creatorchallenge"));
		if (!this.m_active)
		{
			this.m_timeLeft = -1L;
			this.m_currentTimeLeft = -1L;
		}
		this.m_scaleOnTouch = true;
		this.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.CreatorChallengeActiveBannerDrawhandler));
		DateTime dateTime = DateTime.UtcNow;
		if (!string.IsNullOrEmpty(this.m_msg.eventType) && (this.m_msg.eventType.ToLower().Contains("event") || this.m_msg.eventType.ToLower().Contains("creatorchallenge")))
		{
			DateTime dateTime2;
			dateTime2..ctor(1970, 1, 1);
			dateTime = dateTime2.Add(new TimeSpan(this.m_msg.endTime * 10000L));
		}
		else
		{
			DateTime dateTime3;
			dateTime3..ctor(1970, 1, 1);
			dateTime = dateTime3.Add(new TimeSpan(this.m_msg.startTime * 10000L));
		}
		this.m_endDate = dateTime.ToString("dd.MM.yyyy");
		if (!this.m_active)
		{
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.CreatorChallengeBannerDrawhandler));
		}
		this.CreateTouchAreas();
		this.m_TAC.m_letTouchesThrough = true;
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas2.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas2.SetRogue();
		uicanvas2.SetAlign(0f, 0.5f);
		uicanvas2.SetMargins(0f, 0f, 0f, 0f, RelativeTo.OwnHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.GetIconByType(), null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetWidth(0.8f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0.03f, 0.03f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas3 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.0385f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas3, false, string.Empty, _msg.header, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", "#000000");
		uifittedText.SetHorizontalAlign(0f);
		UICanvas uicanvas4 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.034f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.RemoveDrawHandler();
		string text = string.Empty;
		if (this.m_active)
		{
			text = PsStrings.Get(StringID.CHALLENGE_TIMER_TIMELEFT) + " " + PsMetagameManager.GetTimeStringFromSeconds((int)this.m_currentTimeLeft);
		}
		else
		{
			text = this.m_endDate;
		}
		this.m_time = new UIFittedText(uicanvas4, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#A4E5FF", "#000000");
		this.m_time.SetHorizontalAlign(0f);
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x000ED9AC File Offset: 0x000EBDAC
	private void SetState()
	{
		for (int i = 0; i < this.m_msg.eventStates.Count; i++)
		{
			if (this.m_msg.eventStates[i].timeLeft - (long)PsUICenterNewsPopup.m_secondsSinceLoad > 0L)
			{
				this.m_state = this.m_msg.eventStates[i];
				break;
			}
			if (i == this.m_msg.eventStates.Count - 1)
			{
				this.m_state = this.m_msg.eventStates[i];
			}
		}
		this.m_timeLeft = this.m_state.timeLeft;
		this.m_currentTimeLeft = this.m_state.timeLeft - (long)PsUICenterNewsPopup.m_secondsSinceLoad;
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x000EDA74 File Offset: 0x000EBE74
	public override void Step()
	{
		if (this.m_currentTimeLeft != this.m_timeLeft - (long)PsUICenterNewsPopup.m_secondsSinceLoad && this.m_time != null && this.m_currentTimeLeft >= 0L)
		{
			this.m_currentTimeLeft = (long)((int)((double)this.m_state.localEndTime - Main.m_EPOCHSeconds));
			string text = PsStrings.Get(StringID.CHALLENGE_TIMER_TIMELEFT) + " " + PsMetagameManager.GetTimeStringFromSeconds((int)this.m_currentTimeLeft);
			this.m_time.SetText(text);
			if (this.m_currentTimeLeft < 0L)
			{
				bool flag = false;
				if (this.m_state != null)
				{
					this.SetState();
					if (this.m_currentTimeLeft < 0L)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					this.m_time.SetText(this.m_endDate);
					this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.CreatorChallengeBannerDrawhandler));
					this.d_Draw(this);
				}
			}
		}
		if (this.m_hit)
		{
			PsMetrics.NewsOpened(this.m_msg.eventType, this.m_msg.eventName, this.m_msg.header, "newsFeed");
			this.m_popup = new PsUIBasePopup(this.GetPopupType(), null, null, null, false, true, InitialPage.Center, false, false, false);
			(this.m_popup.m_mainContent as PsUIEventMessagePopup).SetEventMessage(this.m_msg, true);
			this.m_popup.Update();
			this.m_popup.SetAction("Continue", delegate
			{
				this.m_popup.Destroy();
				this.m_popup = null;
			});
			this.m_popup.SetAction("Exit", delegate
			{
				this.m_popup.Destroy();
				this.m_popup = null;
			});
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
			TouchAreaS.CancelAllTouches(null);
		}
		base.Step();
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x000EDC6C File Offset: 0x000EC06C
	private Type GetPopupType()
	{
		if (this.m_msg.eventName == "bestGame")
		{
			return typeof(PsUICustomEventMessagePopup);
		}
		if (this.m_msg.eventType == "Survey")
		{
			return typeof(PsUICustomerSurveyPopup);
		}
		if (string.IsNullOrEmpty(this.m_msg.eventType))
		{
			return typeof(PsUIEventMessagePopup);
		}
		if (this.m_msg.eventType.ToLower().Contains("creatorchallenge"))
		{
			return typeof(PsUICreatorChallengePopup);
		}
		return typeof(PsUIEventMessagePopup);
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x000EDD18 File Offset: 0x000EC118
	private string GetIconByType()
	{
		if (string.IsNullOrEmpty(this.m_msg.eventType) || this.m_msg.eventType.ToLower().Contains("announcement") || this.m_msg.eventType.ToLower().Contains("patch"))
		{
			return "menu_icon_news_announcement";
		}
		if (this.m_msg.eventType.ToLower().Contains("creatorchallenge") || this.m_msg.eventType.ToLower().Contains("event"))
		{
			return "menu_icon_news_live";
		}
		return "menu_icon_news_announcement";
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x000EDDC8 File Offset: 0x000EC1C8
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollIn(_touch, _secondary);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
		}
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x000EDE38 File Offset: 0x000EC238
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
		}
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x000EDEA8 File Offset: 0x000EC2A8
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollOut(_touch, _secondary);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
		}
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x000EDF18 File Offset: 0x000EC318
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
		}
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x000EDF88 File Offset: 0x000EC388
	public override void Destroy()
	{
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
		}
		this.m_popup = null;
		base.Destroy();
	}

	// Token: 0x0400193F RID: 6463
	public bool m_scaleOnTouch;

	// Token: 0x04001940 RID: 6464
	public TweenC m_touchScaleTween;

	// Token: 0x04001941 RID: 6465
	private EventMessage m_msg;

	// Token: 0x04001942 RID: 6466
	private EventState m_state;

	// Token: 0x04001943 RID: 6467
	private long m_currentTimeLeft = -1L;

	// Token: 0x04001944 RID: 6468
	private long m_timeLeft = -1L;

	// Token: 0x04001945 RID: 6469
	private UIFittedText m_time;

	// Token: 0x04001946 RID: 6470
	private string m_endDate;

	// Token: 0x04001947 RID: 6471
	public bool m_active;

	// Token: 0x04001948 RID: 6472
	private PsUIBasePopup m_popup;
}
