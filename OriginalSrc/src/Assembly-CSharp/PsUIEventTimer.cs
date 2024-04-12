using System;
using UnityEngine;

// Token: 0x02000295 RID: 661
public class PsUIEventTimer : UICanvas
{
	// Token: 0x060013ED RID: 5101 RVA: 0x000C874C File Offset: 0x000C6B4C
	public PsUIEventTimer(UIComponent _parent, EventMessage _event, string _name = null, Frame _icon = null)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.m_event = _event;
		this.m_icon = _icon;
		if (this.m_event != null)
		{
			if (this.m_event.timeToStart > 0)
			{
				this.m_hasStarted = false;
			}
			else
			{
				this.m_hasStarted = true;
				if (this.m_event.timeLeft > 0)
				{
					this.m_hasEnded = false;
				}
				else
				{
					this.m_hasEnded = true;
				}
			}
		}
		this.SetSize(0.22f, 0.09f, RelativeTo.ScreenHeight);
		this.SetMargins(0f, 0f, 1f, -1f, RelativeTo.OwnHeight);
		base.SetRogue();
		this.RemoveDrawHandler();
		if (_event != null)
		{
			this.m_eventTimerContainer = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			this.m_eventTimerContainer.SetSize(0.22f, 0.09f, RelativeTo.ScreenHeight);
			this.m_eventTimerContainer.SetMargins(0f, 0f, 1f, -1f, RelativeTo.OwnHeight);
			this.m_eventTimerContainer.SetRogue();
			this.m_eventTimerContainer.RemoveDrawHandler();
			this.m_speechBubble = new UIHorizontalList(this.m_eventTimerContainer, string.Empty);
			this.m_speechBubble.SetHeight(0.65f, RelativeTo.ParentHeight);
			this.m_speechBubble.SetVerticalAlign(0f);
			this.m_speechBubble.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.MenuTournamentNotificationBubble));
			this.CreateEventIcon();
			UICanvas uicanvas = new UICanvas(this.m_speechBubble, false, string.Empty, null, string.Empty);
			uicanvas.SetSize(4f, 1f, RelativeTo.ParentHeight);
			uicanvas.RemoveDrawHandler();
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.RemoveDrawHandler();
			uicanvas2.SetHeight(0.4f, RelativeTo.ParentHeight);
			uicanvas2.SetVerticalAlign(1f);
			uicanvas2.SetMargins(0.2f, 0.2f, 0f, 0f, RelativeTo.OwnHeight);
			if (string.IsNullOrEmpty(_name))
			{
				_name = this.m_event.eventName;
			}
			UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, _name, PsFontManager.GetFont(PsFonts.HurmeSemiBold), true, null, null);
			this.m_timerArea = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			this.m_timerArea.RemoveDrawHandler();
			this.m_timerArea.SetHeight(0.6f, RelativeTo.ParentHeight);
			this.m_timerArea.SetMargins(0.2f, 0.2f, 0.1f, 0.1f, RelativeTo.OwnHeight);
			this.m_timerArea.SetVerticalAlign(0f);
			this.CreateTimer();
		}
		else
		{
			this.RemoveUI();
		}
		if (this.m_eventTimerContainer != null)
		{
			this.m_eventTimerContainer.Update();
		}
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x000C8A10 File Offset: 0x000C6E10
	protected virtual void CreateEventIcon()
	{
		if (this.m_icon != null)
		{
			this.m_iconArea = new UICanvas(this.m_speechBubble, false, string.Empty, null, string.Empty);
			this.m_iconArea.SetSize(1f, 1f, RelativeTo.ParentHeight);
			this.m_iconArea.SetMargins(-0.15f, 0.05f, -0.05f, -0.05f, RelativeTo.OwnHeight);
			this.m_iconArea.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_iconArea, false, string.Empty, PsState.m_uiSheet, this.m_icon, true, true);
			uifittedSprite.m_TC.transform.Rotate(Vector3.forward, 5f);
			this.m_tournamentShine = new UISprite(this.m_iconArea, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shine", null), true);
			this.m_tournamentShine.SetSize(0.12f, 0.12f, RelativeTo.ScreenHeight);
			this.m_tournamentShine.SetDepthOffset(0f);
			this.m_shineRotationTween = TweenS.AddTransformTween(this.m_tournamentShine.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, -360f), 19f, 0f, false);
			this.m_shineRotationTween.repeats = -1;
			this.m_shineScaleTween = TweenS.AddTransformTween(this.m_tournamentShine.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(1.2f, 1.2f, 1f), 1.6f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_shineScaleTween, new TweenEventDelegate(this.ShineScaleSmallerTween));
		}
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x000C8BAC File Offset: 0x000C6FAC
	protected virtual void CreateTimer()
	{
		string text = string.Empty;
		if (this.m_hasStarted)
		{
			if (!this.m_hasEnded)
			{
				text = PsStrings.Get(StringID.TOUR_TOURNAMENT_ENDS_IN);
				text = text.Replace("%1", PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true));
			}
		}
		else
		{
			text = PsStrings.Get(StringID.TOUR_TOURNAMENT_STARTS_IN);
			text = text.Replace("%1", PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, false, true));
		}
		this.m_eventTimer = new UIFittedText(this.m_timerArea, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#9CDB2B", null);
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x000C8C48 File Offset: 0x000C7048
	protected virtual void UpdateTimerText()
	{
		int num = (int)Math.Ceiling((double)this.m_event.localStartTime - Main.m_EPOCHSeconds);
		int num2 = (int)Math.Ceiling((double)this.m_event.localEndTime - Main.m_EPOCHSeconds);
		if (num > 0)
		{
			if (num != this.m_timeLeft)
			{
				this.m_timeLeft = num;
				if (this.m_eventTimer != null && this.m_timeLeft >= 0)
				{
					string text = PsStrings.Get(StringID.TOUR_TOURNAMENT_STARTS_IN);
					string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true);
					text = text.Replace("%1", timeStringFromSeconds);
					this.m_eventTimer.SetText(text);
					this.m_eventTimer.Update();
				}
			}
		}
		else
		{
			if (!this.m_hasStarted)
			{
				this.m_hasStarted = true;
			}
			if (num2 != this.m_timeLeft)
			{
				this.m_timeLeft = num2;
				if (this.m_eventTimer != null && this.m_timeLeft >= 0)
				{
					string text2 = PsStrings.Get(StringID.TOUR_TOURNAMENT_ENDS_IN);
					string timeStringFromSeconds2 = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true);
					text2 = text2.Replace("%1", timeStringFromSeconds2);
					this.m_eventTimer.SetText(text2);
					this.m_eventTimer.Update();
				}
				else if (this.m_timeLeft < 0 && !this.m_hasEnded)
				{
					this.m_hasEnded = true;
					this.RemoveUI();
				}
			}
		}
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x000C8DA4 File Offset: 0x000C71A4
	protected void ShineScaleBiggerTween(TweenC _tweenC)
	{
		if (this.m_shineScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_shineScaleTween);
			this.m_shineScaleTween = null;
		}
		this.m_shineScaleTween = TweenS.AddTransformTween(this.m_tournamentShine.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(1.1f, 1.1f, 1f), 1.6f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_shineScaleTween, new TweenEventDelegate(this.ShineScaleSmallerTween));
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x000C8E1C File Offset: 0x000C721C
	protected void ShineScaleSmallerTween(TweenC _tweenC)
	{
		if (this.m_shineScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_shineScaleTween);
			this.m_shineScaleTween = null;
		}
		this.m_shineScaleTween = TweenS.AddTransformTween(this.m_tournamentShine.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(0.9f, 0.9f, 1f), 1.6f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_shineScaleTween, new TweenEventDelegate(this.ShineScaleBiggerTween));
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x000C8E94 File Offset: 0x000C7294
	protected void RemoveUI()
	{
		if (this.m_eventTimerContainer != null)
		{
			this.m_eventTimerContainer.Destroy();
			this.m_eventTimerContainer = null;
			this.m_eventTimer = null;
		}
		if (this.m_tournamentShine != null)
		{
			this.RemoveTweens();
			this.m_tournamentShine = null;
		}
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x000C8ED2 File Offset: 0x000C72D2
	private void RemoveTweens()
	{
		if (this.m_shineRotationTween != null)
		{
			this.m_shineRotationTween = null;
		}
		if (this.m_shineScaleTween != null)
		{
			this.m_shineScaleTween = null;
		}
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x000C8EF8 File Offset: 0x000C72F8
	public override void Step()
	{
		if (this.m_event != null)
		{
			this.UpdateTimerText();
		}
		base.Step();
	}

	// Token: 0x040016A3 RID: 5795
	private UICanvas m_eventTimerContainer;

	// Token: 0x040016A4 RID: 5796
	protected UISprite m_tournamentShine;

	// Token: 0x040016A5 RID: 5797
	protected TweenC m_shineRotationTween;

	// Token: 0x040016A6 RID: 5798
	protected TweenC m_shineScaleTween;

	// Token: 0x040016A7 RID: 5799
	protected UIFittedText m_eventTimer;

	// Token: 0x040016A8 RID: 5800
	protected bool m_hasStarted;

	// Token: 0x040016A9 RID: 5801
	protected bool m_hasEnded;

	// Token: 0x040016AA RID: 5802
	protected int m_timeLeft;

	// Token: 0x040016AB RID: 5803
	protected EventMessage m_event;

	// Token: 0x040016AC RID: 5804
	private UICanvas m_timerArea;

	// Token: 0x040016AD RID: 5805
	protected UICanvas m_iconArea;

	// Token: 0x040016AE RID: 5806
	protected UIHorizontalList m_speechBubble;

	// Token: 0x040016AF RID: 5807
	private Frame m_icon;
}
