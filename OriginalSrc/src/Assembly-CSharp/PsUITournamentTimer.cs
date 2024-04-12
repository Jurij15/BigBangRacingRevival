using System;
using UnityEngine;

// Token: 0x02000296 RID: 662
public class PsUITournamentTimer : PsUIEventTimer
{
	// Token: 0x060013F6 RID: 5110 RVA: 0x000C8F11 File Offset: 0x000C7311
	public PsUITournamentTimer(UIComponent _parent, EventMessage _event, string _name)
		: base(_parent, _event, _name, null)
	{
	}

	// Token: 0x060013F7 RID: 5111 RVA: 0x000C8F20 File Offset: 0x000C7320
	protected override void CreateEventIcon()
	{
		if (this.m_event.tournament != null)
		{
			this.m_iconArea = new UICanvas(this.m_speechBubble, false, string.Empty, null, string.Empty);
			this.m_iconArea.SetSize(1f, 1f, RelativeTo.ParentHeight);
			this.m_iconArea.SetMargins(-0.15f, 0.05f, -0.05f, -0.05f, RelativeTo.OwnHeight);
			this.m_iconArea.RemoveDrawHandler();
			PsUIProfileImage psUIProfileImage = new PsUIProfileImage(this.m_iconArea, false, string.Empty, this.m_event.tournament.ownerFacebookId, this.m_event.tournament.ownerId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, false, true);
			this.m_tournamentShine = new UISprite(this.m_iconArea, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shine", null), true);
			this.m_tournamentShine.SetSize(0.12f, 0.12f, RelativeTo.ScreenHeight);
			this.m_tournamentShine.SetDepthOffset(0f);
			this.m_shineRotationTween = TweenS.AddTransformTween(this.m_tournamentShine.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, -360f), 19f, 0f, false);
			this.m_shineRotationTween.repeats = -1;
			this.m_shineScaleTween = TweenS.AddTransformTween(this.m_tournamentShine.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(1.2f, 1.2f, 1f), 1.6f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_shineScaleTween, new TweenEventDelegate(base.ShineScaleSmallerTween));
		}
	}

	// Token: 0x060013F8 RID: 5112 RVA: 0x000C90D0 File Offset: 0x000C74D0
	protected override void CreateTimer()
	{
		base.CreateTimer();
		if (this.m_hasStarted && !this.m_hasEnded && !this.m_event.tournament.joined)
		{
			this.m_eventTimer.SetText(PsStrings.Get(StringID.TOUR_JOIN));
		}
		else if (this.m_hasEnded && this.m_event.tournament.joined)
		{
			if (!this.m_event.tournament.claimed && (this.m_event.tournament.time != 0 || this.m_event.tournament.time != 2147483647))
			{
				this.m_eventTimer.SetText(PsStrings.Get(StringID.TOUR_CLAIM_REWARD));
			}
			else
			{
				base.RemoveUI();
			}
		}
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x000C91AC File Offset: 0x000C75AC
	protected override void UpdateTimerText()
	{
		if (this.m_event.tournament != null)
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
					if (!this.m_event.tournament.joined)
					{
						string text2 = PsStrings.Get(StringID.TOUR_JOIN);
						this.m_eventTimer.SetText(text2);
						this.m_eventTimer.Update();
					}
				}
				if (num2 != this.m_timeLeft && this.m_event.tournament.joined)
				{
					this.m_timeLeft = num2;
					if (this.m_eventTimer != null && this.m_timeLeft >= 0)
					{
						string text3 = PsStrings.Get(StringID.TOUR_TOURNAMENT_ENDS_IN);
						string timeStringFromSeconds2 = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true);
						text3 = text3.Replace("%1", timeStringFromSeconds2);
						this.m_eventTimer.SetText(text3);
						this.m_eventTimer.Update();
					}
					else if (this.m_timeLeft < 0 && !this.m_hasEnded)
					{
						this.m_hasEnded = true;
						if (!this.m_event.tournament.claimed && (this.m_event.tournament.time != 0 || this.m_event.tournament.time != 2147483647))
						{
							string text4 = PsStrings.Get(StringID.TOUR_CLAIM_REWARD);
							this.m_eventTimer.SetText(text4);
							this.m_eventTimer.Update();
						}
						else
						{
							base.RemoveUI();
						}
					}
				}
			}
		}
	}
}
