using System;
using UnityEngine;

// Token: 0x02000294 RID: 660
public class PsUITournamentEventPopup : PsUIBaseEventPopup
{
	// Token: 0x060013DD RID: 5085 RVA: 0x000C72E4 File Offset: 0x000C56E4
	public PsUITournamentEventPopup(UIComponent _parent, string _tag, EventMessage _event)
		: base(_parent, _tag, _event)
	{
		base.SetColor(DebugDraw.HexToColor("#FC9C25"), DebugDraw.HexToColor("#D86E1D"));
		base.SetAboutText(PsStrings.Get(StringID.TOURNAMENT_INFO_TAGLINE));
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x000C7319 File Offset: 0x000C5719
	protected override void UpdateRightContent()
	{
		this.SetRightHeaderContent();
		this.SetRightBottomContent();
		this.SetRightButton();
		this.SetRightFooter();
		this.Update();
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x000C733C File Offset: 0x000C573C
	protected override void SetRightHeaderContent()
	{
		if (this.m_header != null)
		{
			if (this.m_event != null && this.m_event.tournament != null && !string.IsNullOrEmpty(this.m_event.tournament.ownerId))
			{
				this.m_headerCreatorHolder = new UICanvas(this.m_header, true, string.Empty, null, string.Empty);
				this.m_headerCreatorHolder.SetWidth(0.35f, RelativeTo.ParentWidth);
				this.m_headerCreatorHolder.SetHorizontalAlign(0f);
				this.m_headerCreatorHolder.SetMargins(-0.15f, 0f, -0.1f, 0f, RelativeTo.OwnWidth);
				this.m_headerCreatorHolder.RemoveDrawHandler();
				UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_headerCreatorHolder, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_logo_wreath", null), true, true);
				uifittedSprite.SetVerticalAlign(0f);
				uifittedSprite.SetDepthOffset(-4f);
				PsUIProfileImage psUIProfileImage = new PsUIProfileImage(this.m_headerCreatorHolder, false, string.Empty, this.m_event.tournament.ownerFacebookId, this.m_event.tournament.ownerId, -1, true, false, false, 0.05f, 0.06f, "fff9e6", null, false, true);
				psUIProfileImage.SetSize(0.1f, 0.1f, RelativeTo.ScreenHeight);
				psUIProfileImage.SetVerticalAlign(1f);
				UIFittedSprite uifittedSprite2 = new UIFittedSprite(this.m_headerCreatorHolder, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_logo_wings", null), true, true);
				uifittedSprite2.SetVerticalAlign(0f);
				if (!string.IsNullOrEmpty(this.m_event.tournament.youtuberId))
				{
					this.CreateYoutubeButton(this.m_headerCreatorHolder);
				}
			}
			UICanvas uicanvas = new UICanvas(this.m_header, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.78f, RelativeTo.ParentHeight);
			uicanvas.SetWidth(0.65f, RelativeTo.ParentWidth);
			uicanvas.SetMargins(0.2f, 0.1f, 0.1f, 0.1f, RelativeTo.OwnHeight);
			uicanvas.SetAlign(1f, 1f);
			uicanvas.RemoveDrawHandler();
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.5f, RelativeTo.ParentHeight);
			uicanvas2.SetVerticalAlign(1f);
			uicanvas2.RemoveDrawHandler();
			if (this.m_event != null && !string.IsNullOrEmpty(this.m_event.tournament.ownerName))
			{
				UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, this.m_event.tournament.ownerName, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
				uifittedText.SetHorizontalAlign(0f);
			}
			UICanvas uicanvas3 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas3.SetHeight(0.4f, RelativeTo.ParentHeight);
			uicanvas3.SetAlign(0f, 0f);
			uicanvas3.RemoveDrawHandler();
			UICanvas uicanvas4 = new UICanvas(uicanvas3, false, string.Empty, null, string.Empty);
			uicanvas4.SetWidth(0.8f, RelativeTo.ParentWidth);
			uicanvas4.SetHorizontalAlign(0f);
			uicanvas4.RemoveDrawHandler();
			UIFittedText uifittedText2 = new UIFittedText(uicanvas4, false, string.Empty, PsStrings.Get(StringID.TOUR_TOURNAMENT).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FCA629", "#000");
			uifittedText2.SetHorizontalAlign(0f);
			UICanvas uicanvas5 = new UICanvas(uicanvas3, false, string.Empty, null, string.Empty);
			uicanvas5.SetWidth(0.2f, RelativeTo.ParentWidth);
			uicanvas5.SetHeight(0.04f, RelativeTo.ScreenHeight);
			uicanvas5.SetHorizontalAlign(1f);
			uicanvas5.RemoveDrawHandler();
			this.m_infoButton = new UIRectSpriteButton(uicanvas5, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true, false);
			if (this.m_event == null)
			{
				uicanvas.SetAlign(0.5f, 1f);
				uicanvas.SetMargins(0.1f, RelativeTo.OwnHeight);
				uicanvas3.SetHeight(0.6f, RelativeTo.ParentHeight);
				uicanvas3.SetVerticalAlign(0.4f);
			}
			UICanvas uicanvas6 = new UICanvas(this.m_header, false, string.Empty, null, string.Empty);
			uicanvas6.SetHeight(0.03f, RelativeTo.ScreenHeight);
			uicanvas6.SetVerticalAlign(0f);
			uicanvas6.SetDrawHandler(new UIDrawDelegate(this.DrawHandlerSprite));
			if (this.m_event != null && this.m_event.tournament != null)
			{
				string text = this.m_event.tournament.playerUnit;
				string text2 = text.ToLower();
				if (text2 != null)
				{
					if (text2 == "offroadcar")
					{
						text = PsStrings.Get(StringID.EDITOR_GUI_VEHICLE_NAME_CAR);
						goto IL_4D0;
					}
					if (text2 == "motorcycle")
					{
						text = PsStrings.Get(StringID.EDITOR_GUI_VEHICLE_NAME_BIKE);
						goto IL_4D0;
					}
				}
				text = string.Empty;
				IL_4D0:
				string text3 = string.Empty;
				if (!string.IsNullOrEmpty(text))
				{
					text3 = string.Concat(new object[]
					{
						text,
						" - ",
						this.m_duration,
						PsStrings.Get(StringID.TOUR_HOURS)
					});
				}
				else
				{
					text3 = this.m_duration + PsStrings.Get(StringID.TOUR_HOURS);
				}
				UIText uitext = new UIText(uicanvas6, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeBold), 0.8f, RelativeTo.ParentHeight, "#272A19", null);
			}
		}
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x000C78A8 File Offset: 0x000C5CA8
	protected override void SetRightBottomContent()
	{
		if (this.m_bottomContainer != null && this.m_event != null && this.m_event.tournament != null)
		{
			this.m_bottomContainer.DestroyChildren();
			if (!this.m_hasEnded)
			{
				UICanvas uicanvas = new UICanvas(this.m_bottomContainer, false, string.Empty, null, string.Empty);
				uicanvas.SetHeight(0.25f, RelativeTo.ParentHeight);
				uicanvas.SetVerticalAlign(1f);
				uicanvas.RemoveDrawHandler();
				UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
				uicanvas2.SetHeight(0.4f, RelativeTo.ParentHeight);
				uicanvas2.RemoveDrawHandler();
				UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TOUR_FINAL_RANKINGS), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
				UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_bottomContainer, string.Empty);
				uihorizontalList.SetHeight(0.5f, RelativeTo.ParentHeight);
				uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
				uihorizontalList.RemoveDrawHandler();
				UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_hat_icon_GoldenCarHelmet", null), true, true);
				UIFittedSprite uifittedSprite2 = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_chest_T2", null), true, true);
				UIFittedSprite uifittedSprite3 = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_coins", null), true, true);
			}
			else
			{
				this.m_rankInfoContainer = new UICanvas(this.m_bottomContainer, true, string.Empty, null, string.Empty);
				this.m_rankInfoContainer.SetHeight(0.5f, RelativeTo.ParentHeight);
				this.m_rankInfoContainer.RemoveDrawHandler();
				UICanvas uicanvas3 = new UICanvas(this.m_rankInfoContainer, false, string.Empty, null, string.Empty);
				uicanvas3.SetHeight(0.3f, RelativeTo.ParentHeight);
				uicanvas3.SetVerticalAlign(1f);
				uicanvas3.SetMargins(0.025f, RelativeTo.OwnHeight);
				uicanvas3.RemoveDrawHandler();
				UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, PsStrings.Get(StringID.TOUR_WINNERS_LISTED), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
				UICanvas uicanvas4 = new UICanvas(this.m_rankInfoContainer, false, string.Empty, null, string.Empty);
				uicanvas4.SetHeight(0.7f, RelativeTo.ParentHeight);
				uicanvas4.SetVerticalAlign(0f);
				uicanvas4.SetMargins(0.025f, RelativeTo.OwnHeight);
				uicanvas4.RemoveDrawHandler();
				UIFittedText uifittedText3 = new UIFittedText(uicanvas4, false, string.Empty, PsStrings.Get(StringID.TOUR_WWW_BBRANKS_COM), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
			}
		}
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x000C7B28 File Offset: 0x000C5F28
	protected override void SetRightButton()
	{
		if (this.m_playButtonParent != null)
		{
			this.m_playButtonParent.DestroyChildren();
			if (this.m_event != null && this.m_event.tournament != null && this.m_event.tournament.joined && this.m_event.timeLeft <= 0 && !this.m_event.tournament.claimed)
			{
				this.m_playButton = new PsUIAttentionButton(this.m_playButtonParent, default(Vector3), 0.25f, 0.25f, 0.005f);
			}
			else
			{
				this.m_playButton = new PsUIGenericButton(this.m_playButtonParent, 0.25f, 0.25f, 0.005f, "Button");
			}
			this.m_playButton.SetOrangeColors(true);
			this.m_playButton.m_TAC.m_letTouchesThrough = false;
			UICanvas uicanvas = new UICanvas(this.m_playButton, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.11f, RelativeTo.ScreenHeight);
			uicanvas.SetWidth(0.4f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			string text = string.Empty;
			int timeLeft = this.m_timeLeft;
			string text2 = PsStrings.Get(StringID.TOUR_TOURNAMENT_ENDS_IN);
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(timeLeft, true, true);
			if (this.m_event != null)
			{
				if (!this.m_hasStarted)
				{
					text = PsStrings.Get(StringID.TOUR_JOIN);
					text2 = PsStrings.Get(StringID.TOUR_TOURNAMENT_STARTS_IN);
					this.m_playButton.SetGrayColors();
					this.m_playButton.DisableTouchAreas(true);
				}
				else if (!this.m_hasEnded && !this.m_event.tournament.joined)
				{
					text = PsStrings.Get(StringID.TOUR_JOIN);
					text2 = PsStrings.Get(StringID.TOUR_TOURNAMENT_ENDS_IN);
					this.m_playButton.SetOrangeColors(true);
					this.m_playButton.EnableTouchAreas(true);
				}
				else if (!this.m_hasEnded)
				{
					text = PsStrings.Get(StringID.TOUR_RACE);
					text2 = PsStrings.Get(StringID.TOUR_TOURNAMENT_ENDS_IN);
					this.m_playButton.SetOrangeColors(true);
					this.m_playButton.EnableTouchAreas(true);
				}
				else if (!this.m_event.tournament.claimed && this.m_event.tournament.joined)
				{
					text = PsStrings.Get(StringID.TOUR_CLAIM_REWARD);
					text2 = PsStrings.Get(StringID.TOUR_TOURNAMENT_HAS_ENDED);
					this.m_playButton.SetOrangeColors(true);
					this.m_playButton.EnableTouchAreas(true);
				}
				else if (this.m_event.tournament.joined)
				{
					text = PsStrings.Get(StringID.TOUR_ROOM_RESULTS);
					text2 = PsStrings.Get(StringID.TOUR_TOURNAMENT_HAS_ENDED);
					this.m_playButton.SetOrangeColors(true);
					this.m_playButton.EnableTouchAreas(true);
				}
				else
				{
					text = PsStrings.Get(StringID.TOUR_JOIN);
					text2 = PsStrings.Get(StringID.TOUR_NOT_AVAILABLE);
					this.m_playButton.SetGrayColors();
					this.m_playButton.DisableTouchAreas(true);
				}
			}
			else
			{
				text = PsStrings.Get(StringID.TOUR_JOIN);
				text2 = PsStrings.Get(StringID.TOUR_NOT_AVAILABLE);
				this.m_playButton.SetGrayColors();
				this.m_playButton.DisableTouchAreas(true);
			}
			text2 = text2.Replace("%1", timeStringFromSeconds);
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.6f, RelativeTo.ParentHeight);
			uicanvas2.SetVerticalAlign(1f);
			uicanvas2.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, "#727272");
			this.m_timerContainer = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			this.m_timerContainer.SetHeight(0.35f, RelativeTo.ParentHeight);
			this.m_timerContainer.SetVerticalAlign(0f);
			this.m_timerContainer.SetMargins(0.1f, RelativeTo.OwnHeight);
			this.m_timerContainer.RemoveDrawHandler();
			this.m_timeleftText = new UIFittedText(this.m_timerContainer, false, "Timer", text2, PsFontManager.GetFont(PsFonts.HurmeBoldMN), true, null, null);
			this.m_timeleftText.SetHorizontalAlign(0.5f);
		}
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x000C7F38 File Offset: 0x000C6338
	protected override void SetRightFooter()
	{
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetVerticalAlign(0f);
		uicanvas.SetHeight(0.075f, RelativeTo.ParentHeight);
		uicanvas.SetMargins(0.3f, RelativeTo.OwnHeight);
		uicanvas.RemoveDrawHandler();
		string text = PsStrings.Get(StringID.TOUR_WWW_BBRANKS_COM);
		string text2 = text;
		if (this.m_hasStarted && !this.m_hasEnded)
		{
			text2 = PsStrings.Get(StringID.TOUR_CONSTANT_BBRANKS);
		}
		text2 = text2.Replace("%1", text);
		this.m_footerLink = new UIFittedText(uicanvas, true, string.Empty, text2, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#dddddd", null);
		this.m_footerLink.SetHorizontalAlign(1f);
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x000C7FF0 File Offset: 0x000C63F0
	private void CreateYoutubeButton(UIComponent _parent)
	{
		if (this.m_event != null && this.m_event.tournament != null && !string.IsNullOrEmpty(this.m_event.tournament.youtuberId))
		{
			UIFixedYoutubeButton uifixedYoutubeButton = new UIFixedYoutubeButton(_parent, this.m_event.tournament.youtuberName, this.m_event.tournament.youtubeSubscriberCount, 0.75f, RelativeTo.ParentWidth, 0.25f, 0.25f, 0.001f, "YoutubeButton");
			uifixedYoutubeButton.SetReleaseAction(new Action(this.YoutubePressed));
			uifixedYoutubeButton.SetVerticalAlign(0f);
			uifixedYoutubeButton.SetDepthOffset(-20f);
		}
	}

	// Token: 0x060013E4 RID: 5092 RVA: 0x000C809C File Offset: 0x000C649C
	private void YoutubePressed()
	{
		if (this.m_event != null && !string.IsNullOrEmpty(this.m_event.tournament.youtuberId))
		{
			PsMetrics.YoutubePageOpened("tournamentEvent", this.m_event.tournament.youtuberId, this.m_event.tournament.youtuberName);
			Application.OpenURL("https://www.youtube.com/channel/" + this.m_event.tournament.youtuberId);
		}
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x000C8118 File Offset: 0x000C6518
	private void OpenPlayerProfilePopup()
	{
		if (this.m_event != null && this.m_event.tournament != null)
		{
			SoundS.PlaySingleShot("/UI/Popup", Vector3.zero, 1f);
			this.m_profilePopup = new PsUIBasePopup(typeof(PsUICenterProfilePopup), null, null, null, true, true, InitialPage.Center, false, false, false);
			(this.m_profilePopup.m_mainContent as PsUICenterProfilePopup).SetUser(this.m_event.tournament.ownerId, false);
			this.m_profilePopup.SetAction("Exit", delegate
			{
				this.m_profilePopup.Destroy();
			});
			this.m_profilePopup.Update();
			TweenS.AddTransformTween(this.m_profilePopup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x000C81FC File Offset: 0x000C65FC
	protected override void PlayButtonPressed()
	{
		if (PsMetagameManager.m_activeTournament != null)
		{
			if (PlayerPrefsX.GetOffroadRacing())
			{
				PsPlanetPath floatingPath = PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().GetFloatingPath();
				PsGameLoopTournament psGameLoopTournament = new PsGameLoopTournament(PsMetagameManager.m_activeTournament, PsMinigameContext.News, floatingPath);
				psGameLoopTournament.StartLoop();
			}
			else
			{
				this.m_playButton.m_hit = false;
				this.m_unlockPopup = new PsUIBasePopup(typeof(PsUITournamentUnlockPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
				this.m_unlockPopup.SetAction("Exit", delegate
				{
					this.m_unlockPopup.Destroy();
					this.m_unlockPopup = null;
				});
			}
		}
	}

	// Token: 0x060013E7 RID: 5095 RVA: 0x000C828C File Offset: 0x000C668C
	protected override void UpdateEventInfo()
	{
		if (PsMetagameManager.m_activeTournament != null && PsMetagameManager.m_activeTournament.tournament != null && this.m_event.tournament != null)
		{
			int num = (int)Math.Ceiling((double)PsMetagameManager.m_activeTournament.localStartTime - Main.m_EPOCHSeconds);
			int num2 = (int)Math.Ceiling((double)PsMetagameManager.m_activeTournament.localEndTime - Main.m_EPOCHSeconds);
			if (num > 0)
			{
				if (this.m_hasStarted)
				{
					this.m_hasStarted = false;
				}
				if (num != this.m_timeLeft)
				{
					this.m_timeLeft = num;
					if (this.m_timeleftText != null && this.m_timeLeft >= 0)
					{
						string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true);
						string text = PsStrings.Get(StringID.TOUR_TOURNAMENT_STARTS_IN);
						text = text.Replace("%1", timeStringFromSeconds);
						this.m_timeleftText.SetText(text);
						this.m_timeleftText.Update();
					}
				}
			}
			else
			{
				if (!this.m_hasStarted)
				{
					this.m_hasStarted = true;
					this.UpdateRightContent();
				}
				if (num2 != this.m_timeLeft)
				{
					this.m_timeLeft = num2;
					if (this.m_timeleftText != null && this.m_timeLeft >= 0)
					{
						if (this.m_hasEnded)
						{
							this.m_hasEnded = false;
						}
						string timeStringFromSeconds2 = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true);
						string text2 = PsStrings.Get(StringID.TOUR_TOURNAMENT_ENDS_IN);
						text2 = text2.Replace("%1", timeStringFromSeconds2);
						this.m_timeleftText.SetText(text2);
						this.m_timeleftText.Update();
					}
					else if (this.m_timeLeft < 0 && !this.m_hasEnded)
					{
						this.m_hasEnded = true;
						this.UpdateRightContent();
						this.m_timerContainer.Update();
					}
				}
			}
		}
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x000C8444 File Offset: 0x000C6844
	public override void Step()
	{
		if (this.m_event != null && this.m_event.tournament != null && this.m_headerCreatorHolder != null && this.m_headerCreatorHolder.m_hit && this.m_event.tournament.ownerId != PlayerPrefsX.GetUserId())
		{
			TouchAreaS.CancelAllTouches(null);
			this.OpenPlayerProfilePopup();
		}
		if ((this.m_footerLink != null && this.m_footerLink.m_hit) || (this.m_rankInfoContainer != null && this.m_rankInfoContainer.m_hit))
		{
			Application.OpenURL("http://www.bbranks.com");
		}
		if (this.m_infoPopup == null && this.m_infoButton != null && this.m_infoButton.m_hit)
		{
			this.m_infoButton.m_hit = false;
			this.m_infoPopup = new PsUIBasePopup(typeof(PsUITournamentInfo), null, null, null, true, true, InitialPage.Center, false, false, false);
			this.m_infoPopup.SetAction("Exit", delegate
			{
				this.m_infoPopup.Destroy();
				this.m_infoPopup = null;
			});
		}
		base.Step();
	}

	// Token: 0x060013E9 RID: 5097 RVA: 0x000C8564 File Offset: 0x000C6964
	public void DrawHandlerSprite(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_row_yellow", null);
		float num = frame.width / 2f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num / 2f, frame.y, num, frame.height);
		Frame frame4 = new Frame(frame.x, frame.y, num, frame.height, true, false);
		float num2 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * (frame2.width / frame2.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.left * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame4, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.right * (_c.m_actualWidth - num3) / 2f, 0f);
		float num4 = _c.m_actualWidth - num2 - num3;
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC3, num4, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x0400169B RID: 5787
	private UIFittedText m_footerLink;

	// Token: 0x0400169C RID: 5788
	private UICanvas m_timerContainer;

	// Token: 0x0400169D RID: 5789
	private UICanvas m_headerCreatorHolder;

	// Token: 0x0400169E RID: 5790
	private UICanvas m_rankInfoContainer;

	// Token: 0x0400169F RID: 5791
	private UIRectSpriteButton m_infoButton;

	// Token: 0x040016A0 RID: 5792
	private PsUIBasePopup m_unlockPopup;

	// Token: 0x040016A1 RID: 5793
	private PsUIBasePopup m_infoPopup;

	// Token: 0x040016A2 RID: 5794
	private PsUIBasePopup m_profilePopup;
}
