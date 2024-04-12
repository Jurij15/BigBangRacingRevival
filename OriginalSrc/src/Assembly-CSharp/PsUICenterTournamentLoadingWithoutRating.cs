using System;
using UnityEngine;

// Token: 0x020003AE RID: 942
public class PsUICenterTournamentLoadingWithoutRating : PsUICenterRatingLoading
{
	// Token: 0x06001AF4 RID: 6900 RVA: 0x0012C0FC File Offset: 0x0012A4FC
	public PsUICenterTournamentLoadingWithoutRating(UIComponent _parent)
		: base(_parent)
	{
		this.m_event = PsMetagameManager.m_activeTournament;
		if (this.m_event != null && this.m_event.timeLeft < 0)
		{
			PsUICenterTournamentLoadingWithoutRating.m_hasEnded = true;
		}
		if (this.m_event != null)
		{
			this.m_rightCanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
			this.m_rightCanvas.SetMargins(0.05f, RelativeTo.OwnHeight);
			this.m_rightCanvas.RemoveDrawHandler();
			UICanvas uicanvas = new UICanvas(this.m_rightCanvas, false, string.Empty, null, string.Empty);
			uicanvas.SetSize(0.93f, 0.8f, RelativeTo.ParentHeight);
			uicanvas.SetDrawHandler(new UIDrawDelegate(this.OrangeDrawhandler));
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.25f, RelativeTo.ParentHeight);
			uicanvas2.SetVerticalAlign(1f);
			uicanvas2.RemoveDrawHandler();
			this.m_headerCreatorHolder = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
			this.m_headerCreatorHolder.SetWidth(0.35f, RelativeTo.ParentWidth);
			this.m_headerCreatorHolder.SetHorizontalAlign(0f);
			this.m_headerCreatorHolder.SetMargins(-0.15f, 0f, -0.1f, 0f, RelativeTo.OwnWidth);
			this.m_headerCreatorHolder.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_headerCreatorHolder, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_logo_wreath", null), true, true);
			uifittedSprite.SetVerticalAlign(0f);
			uifittedSprite.SetDepthOffset(-4f);
			PsUIProfileImage psUIProfileImage = new PsUIProfileImage(this.m_headerCreatorHolder, false, string.Empty, this.m_event.tournament.ownerFacebookId, this.m_event.tournament.ownerId, -1, true, false, false, 0.05f, 0.06f, "fff9e6", null, false, true);
			psUIProfileImage.SetSize(0.11f, 0.11f, RelativeTo.ScreenHeight);
			psUIProfileImage.SetVerticalAlign(1f);
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(this.m_headerCreatorHolder, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_logo_wings", null), true, true);
			uifittedSprite2.SetVerticalAlign(0f);
			if (!string.IsNullOrEmpty(this.m_event.tournament.youtuberId))
			{
				this.CreateYoutubeButton(this.m_headerCreatorHolder);
			}
			UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
			uicanvas3.SetHeight(0.78f, RelativeTo.ParentHeight);
			uicanvas3.SetWidth(0.65f, RelativeTo.ParentWidth);
			uicanvas3.SetMargins(0.2f, 0.1f, 0.1f, 0.1f, RelativeTo.OwnHeight);
			uicanvas3.SetAlign(1f, 1f);
			uicanvas3.RemoveDrawHandler();
			UICanvas uicanvas4 = new UICanvas(uicanvas3, false, string.Empty, null, string.Empty);
			uicanvas4.SetHeight(0.5f, RelativeTo.ParentHeight);
			uicanvas4.SetVerticalAlign(1f);
			uicanvas4.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas4, false, string.Empty, this.m_event.tournament.ownerName, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
			uifittedText.SetHorizontalAlign(0f);
			UICanvas uicanvas5 = new UICanvas(uicanvas3, false, string.Empty, null, string.Empty);
			uicanvas5.SetHeight(0.4f, RelativeTo.ParentHeight);
			uicanvas5.SetAlign(0f, 0f);
			uicanvas5.RemoveDrawHandler();
			UIFittedText uifittedText2 = new UIFittedText(uicanvas5, false, string.Empty, PsStrings.Get(StringID.TOUR_TOURNAMENT).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FCA629", "#000");
			uifittedText2.SetHorizontalAlign(0f);
			UICanvas uicanvas6 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
			uicanvas6.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uicanvas6.SetVerticalAlign(0f);
			uicanvas6.SetDrawHandler(new UIDrawDelegate(this.DrawHandlerSprite));
			string text = this.m_event.tournament.playerUnit;
			string text2 = text.ToLower();
			if (text2 != null)
			{
				if (text2 == "offroadcar")
				{
					text = PsStrings.Get(StringID.EDITOR_GUI_VEHICLE_NAME_CAR);
					goto IL_444;
				}
				if (text2 == "motorcycle")
				{
					text = PsStrings.Get(StringID.EDITOR_GUI_VEHICLE_NAME_BIKE);
					goto IL_444;
				}
			}
			text = string.Empty;
			IL_444:
			float hoursFromSeconds = PsMetagameManager.GetHoursFromSeconds((long)(this.m_event.localEndTime - this.m_event.localStartTime), 0);
			string text3 = string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				text3 = string.Concat(new object[]
				{
					text,
					" - ",
					hoursFromSeconds,
					PsStrings.Get(StringID.TOUR_HOURS)
				});
			}
			else
			{
				text3 = hoursFromSeconds + PsStrings.Get(StringID.TOUR_HOURS);
			}
			UIText uitext = new UIText(uicanvas6, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeBold), 0.8f, RelativeTo.ParentHeight, "#272A19", null);
			this.m_continueButtonParent = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			this.m_continueButtonParent.SetHeight(0.12f, RelativeTo.ScreenHeight);
			this.m_continueButtonParent.SetVerticalAlign(0f);
			this.m_continueButtonParent.SetMargins(0f, 0f, 0.5f, -0.5f, RelativeTo.OwnHeight);
			this.m_continueButtonParent.RemoveDrawHandler();
			this.m_continuebutton = new PsUIGenericButton(this.m_continueButtonParent, 0.25f, 0.25f, 0.01f, "Button");
			this.m_continuebutton.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			this.m_continuebutton.SetOrangeColors(true);
			this.m_continuebutton.SetText(PsStrings.Get(StringID.CONTINUE), 0.04f, 0.3f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
			this.m_continuebutton.SetHeight(0.12f, RelativeTo.ScreenHeight);
			this.m_continuebutton.SetDepthOffset(-10f);
			this.m_mainContent = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			this.m_mainContent.SetVerticalAlign(0f);
			this.m_mainContent.SetHeight(0.75f, RelativeTo.ParentHeight);
			this.m_mainContent.SetMargins(0.1f, 0.1f, 0.05f, 0.1f, RelativeTo.OwnHeight);
			this.m_mainContent.RemoveDrawHandler();
			this.m_bottomContainer = new UICanvas(this.m_mainContent, false, string.Empty, null, string.Empty);
			this.m_bottomContainer.SetVerticalAlign(0f);
			this.m_bottomContainer.SetHeight(0.5f, RelativeTo.ParentHeight);
			this.m_bottomContainer.SetMargins(0.025f, RelativeTo.ParentWidth);
			this.m_bottomContainer.RemoveDrawHandler();
			UITextbox uitextbox = new UITextbox(this.m_bottomContainer, false, string.Empty, PsStrings.Get(StringID.TOURNAMENT_EXIT_ALERT_TOP), PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, null);
			uitextbox.SetHeight(0.6f, RelativeTo.ParentHeight);
			uitextbox.SetWidth(0.8f, RelativeTo.ParentWidth);
			uitextbox.SetMaxRows(3);
			uitextbox.SetVerticalAlign(1f);
			UICanvas uicanvas7 = new UICanvas(uitextbox, false, string.Empty, null, string.Empty);
			uicanvas7.SetSize(0.07f, 0.07f, RelativeTo.ScreenHeight);
			uicanvas7.SetMargins(-1f, 1f, 0f, 0f, RelativeTo.OwnWidth);
			uicanvas7.SetHorizontalAlign(0f);
			uicanvas7.RemoveDrawHandler();
			UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas7, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_exclamation", null), true, true);
			PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry = PsUICenterTournament.m_lb.m_entryDictionary[PlayerPrefsX.GetUserId()];
			bool flag = PsUITournamentLeaderboard.m_hostId == PlayerPrefsX.GetUserId();
			if ((psUITournamentLeaderboardEntry != null && psUITournamentLeaderboardEntry.m_highscoreData.time != 0 && psUITournamentLeaderboardEntry.m_highscoreData.time != 2147483647) || (PsState.m_activeGameLoop.m_timeScoreCurrent != 0 && PsState.m_activeGameLoop.m_timeScoreCurrent != 2147483647) || flag)
			{
				UICanvas uicanvas8 = new UICanvas(this.m_bottomContainer, false, string.Empty, null, string.Empty);
				uicanvas8.SetHeight(0.4f, RelativeTo.ParentHeight);
				uicanvas8.SetVerticalAlign(0f);
				uicanvas8.RemoveDrawHandler();
				UICanvas uicanvas9 = new UICanvas(uicanvas8, false, string.Empty, null, string.Empty);
				uicanvas9.SetWidth(0.5f, RelativeTo.ParentWidth);
				uicanvas9.SetHorizontalAlign(0.5f);
				uicanvas9.RemoveDrawHandler();
				UICanvas uicanvas10 = new UICanvas(uicanvas9, false, string.Empty, null, string.Empty);
				uicanvas10.SetHeight(0.3f, RelativeTo.ParentHeight);
				uicanvas10.SetVerticalAlign(1f);
				uicanvas10.RemoveDrawHandler();
				string text4 = PsStrings.Get(StringID.TOUR_CURRENT_POSITION);
				if (this.m_event.timeLeft <= 0)
				{
					text4 = PsStrings.Get(StringID.TOUR_FINAL_POSITION);
				}
				if (flag)
				{
					text4 = PsStrings.Get(StringID.TOUR_TOTAL_POT);
				}
				UIFittedText uifittedText3 = new UIFittedText(uicanvas10, false, string.Empty, text4, PsFontManager.GetFont(PsFonts.HurmeSemiBold), true, "#F17634", null);
				UICanvas uicanvas11 = new UICanvas(uicanvas9, false, string.Empty, null, string.Empty);
				uicanvas11.SetHeight(0.7f, RelativeTo.ParentHeight);
				uicanvas11.SetVerticalAlign(0f);
				uicanvas11.RemoveDrawHandler();
				string text5 = psUITournamentLeaderboardEntry.m_position.ToString() + ".";
				if (flag && PsState.m_activeGameLoop != null)
				{
					UISprite uisprite = new UISprite(uicanvas11, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_icon", null), true);
					uisprite.SetSize(1.2f, 1.25f, RelativeTo.ParentHeight);
					uisprite.SetAlign(-0.2f, 0.5f);
					uisprite.SetDepthOffset(-30f);
					TournamentInfo tournament = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament;
					int totalPot = Tournaments.GetTotalPot(tournament.globalParticipants, tournament.prizeCoins, tournament.globalNitroPot);
					text5 = "<color=#FDD444>" + string.Format("{0:n0}", totalPot).Replace(",", " ") + "</color>";
				}
				UIFittedText uifittedText4 = new UIFittedText(uicanvas11, false, string.Empty, text5, PsFontManager.GetFont(PsFonts.HurmeSemiBold), true, null, null);
			}
			this.m_topContainer = new UICanvas(this.m_mainContent, false, string.Empty, null, string.Empty);
			this.m_topContainer.SetVerticalAlign(1f);
			this.m_topContainer.SetHeight(0.5f, RelativeTo.ParentHeight);
			this.m_topContainer.SetDrawHandler(new UIDrawDelegate(this.OrangeDrawhandler));
			this.SetRightBottomContent();
		}
		this.m_topBanner.SetHeight(0f, RelativeTo.ScreenHeight);
		this.m_bottomBanner.SetHeight(0f, RelativeTo.ScreenWidth);
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x0012CBB8 File Offset: 0x0012AFB8
	private void CreateYoutubeButton(UIComponent _parent)
	{
		if (this.m_event != null && !string.IsNullOrEmpty(this.m_event.tournament.youtuberId))
		{
			UIFixedYoutubeButton uifixedYoutubeButton = new UIFixedYoutubeButton(_parent, this.m_event.tournament.youtuberName, this.m_event.tournament.youtubeSubscriberCount, 0.75f, RelativeTo.ParentWidth, 0.25f, 0.25f, 0.001f, "YoutubeButton");
			uifixedYoutubeButton.SetReleaseAction(new Action(this.YoutubePressed));
			uifixedYoutubeButton.SetHeight(0.045f, RelativeTo.ScreenHeight);
			uifixedYoutubeButton.SetVerticalAlign(0f);
			uifixedYoutubeButton.SetDepthOffset(-20f);
		}
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x0012CC60 File Offset: 0x0012B060
	private void YoutubePressed()
	{
		if (this.m_event != null && !string.IsNullOrEmpty(this.m_event.tournament.youtuberId))
		{
			PsMetrics.YoutubePageOpened("tournamentExit", this.m_event.tournament.youtuberId, this.m_event.tournament.youtuberName);
			Application.OpenURL("https://www.youtube.com/channel/" + this.m_event.tournament.youtuberId);
		}
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x0012CCDB File Offset: 0x0012B0DB
	private void UpdateRightContent()
	{
		this.SetRightBottomContent();
		this.m_rightCanvas.Update();
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x0012CCF0 File Offset: 0x0012B0F0
	private void SetRightBottomContent()
	{
		if (this.m_topContainer != null)
		{
			this.m_topContainer.DestroyChildren();
			if (!PsUICenterTournamentLoadingWithoutRating.m_hasEnded)
			{
				UICanvas uicanvas = new UICanvas(this.m_topContainer, false, string.Empty, null, string.Empty);
				uicanvas.SetHeight(0.5f, RelativeTo.ParentHeight);
				uicanvas.RemoveDrawHandler();
				UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
				uicanvas2.SetHeight(0.35f, RelativeTo.ParentHeight);
				uicanvas2.SetVerticalAlign(1f);
				uicanvas2.SetMargins(0.025f, RelativeTo.OwnHeight);
				uicanvas2.RemoveDrawHandler();
				UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TOUR_TOURNAMENT_ENDS_IN_EXIT), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FA1A1D", null);
				UICanvas uicanvas3 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
				uicanvas3.SetHeight(0.65f, RelativeTo.ParentHeight);
				uicanvas3.SetVerticalAlign(0f);
				uicanvas3.SetMargins(0.025f, RelativeTo.OwnHeight);
				uicanvas3.RemoveDrawHandler();
				this.m_timeleftText = new UIFittedText(uicanvas3, false, string.Empty, PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
			}
			else
			{
				this.rankInfoContainer = new UICanvas(this.m_topContainer, true, string.Empty, null, string.Empty);
				this.rankInfoContainer.SetHeight(0.5f, RelativeTo.ParentHeight);
				this.rankInfoContainer.RemoveDrawHandler();
				UICanvas uicanvas4 = new UICanvas(this.rankInfoContainer, false, string.Empty, null, string.Empty);
				uicanvas4.SetHeight(0.3f, RelativeTo.ParentHeight);
				uicanvas4.SetVerticalAlign(1f);
				uicanvas4.SetMargins(0.025f, RelativeTo.OwnHeight);
				uicanvas4.RemoveDrawHandler();
				UIFittedText uifittedText2 = new UIFittedText(uicanvas4, false, string.Empty, PsStrings.Get(StringID.TOUR_WINNERS_LISTED), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
				UICanvas uicanvas5 = new UICanvas(this.rankInfoContainer, false, string.Empty, null, string.Empty);
				uicanvas5.SetHeight(0.7f, RelativeTo.ParentHeight);
				uicanvas5.SetVerticalAlign(0f);
				uicanvas5.SetMargins(0.025f, RelativeTo.OwnHeight);
				uicanvas5.RemoveDrawHandler();
				UIFittedText uifittedText3 = new UIFittedText(uicanvas5, false, string.Empty, PsStrings.Get(StringID.TOUR_WWW_BBRANKS_COM), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
			}
		}
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x0012CF1D File Offset: 0x0012B31D
	protected override void CreateCreatorInfo(UIComponent _parent)
	{
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x0012CF1F File Offset: 0x0012B31F
	protected override void CreateRatingBar(UIComponent _parent, int _positive, int _negative)
	{
	}

	// Token: 0x06001AFB RID: 6907 RVA: 0x0012CF21 File Offset: 0x0012B321
	public override void CreateRatingButtonList(UIComponent _parent)
	{
	}

	// Token: 0x06001AFC RID: 6908 RVA: 0x0012CF24 File Offset: 0x0012B324
	public override void Step()
	{
		if (this.m_continuebutton != null && this.m_continuebutton.m_hit && !this.m_continuePressed)
		{
			this.m_continuePressed = true;
			TouchAreaS.CancelAllTouches(null);
			TouchAreaS.Disable();
			this.Proceed();
		}
		else if (this.rankInfoContainer != null && this.rankInfoContainer.m_hit)
		{
			this.rankInfoContainer.m_hit = false;
			Application.OpenURL("http://www.bbranks.com");
		}
		if (PsMetagameManager.m_activeTournament != null && PsMetagameManager.m_activeTournament.tournament != null && this.m_event != null && this.m_event.tournament != null)
		{
			int num = (int)Math.Ceiling((double)PsMetagameManager.m_activeTournament.localEndTime - Main.m_EPOCHSeconds);
			if (num != this.m_timeLeft)
			{
				this.m_timeLeft = num;
				if (this.m_timeleftText != null && this.m_timeLeft >= 0)
				{
					if (PsUICenterTournamentLoadingWithoutRating.m_hasEnded)
					{
						PsUICenterTournamentLoadingWithoutRating.m_hasEnded = false;
					}
					string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true);
					this.m_timeleftText.SetText(timeStringFromSeconds);
					this.m_timeleftText.Update();
				}
				else if (this.m_timeLeft < 0 && !PsUICenterTournamentLoadingWithoutRating.m_hasEnded)
				{
					PsUICenterTournamentLoadingWithoutRating.m_hasEnded = true;
					if (this.m_rightCanvas != null)
					{
						this.UpdateRightContent();
					}
				}
			}
		}
		base.Step();
	}

	// Token: 0x06001AFD RID: 6909 RVA: 0x0012D088 File Offset: 0x0012B488
	public void OrangeDrawhandler(UIComponent _c)
	{
		_c.m_TC.transform.localScale = Vector3.one;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, false);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.02f, 15, Vector2.zero);
		Color color = DebugDraw.HexToColor("#D86E1D");
		Color color2 = DebugDraw.HexToColor("#FC9C25");
		Color black = Color.black;
		black.a = 0.35f;
		GGData ggdata = new GGData(roundedRect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, roundedRect, (float)Screen.height * 0.009f, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x0012D168 File Offset: 0x0012B568
	public void DrawHandlerSprite(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_row_yellow", null);
		float num = frame.width / 2f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num / 2f, frame.y, num, frame.height);
		Frame frame4 = new Frame(frame.x, frame.y, num, frame.height);
		frame4.flipX = true;
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

	// Token: 0x04001D5B RID: 7515
	private bool m_continuePressed;

	// Token: 0x04001D5C RID: 7516
	private UIFittedText m_timeleftText;

	// Token: 0x04001D5D RID: 7517
	private UICanvas m_timerContainer;

	// Token: 0x04001D5E RID: 7518
	private UICanvas m_rightCanvas;

	// Token: 0x04001D5F RID: 7519
	private UICanvas m_continueButtonParent;

	// Token: 0x04001D60 RID: 7520
	private UICanvas m_mainContent;

	// Token: 0x04001D61 RID: 7521
	private UICanvas m_topContainer;

	// Token: 0x04001D62 RID: 7522
	private UICanvas m_bottomContainer;

	// Token: 0x04001D63 RID: 7523
	private UICanvas m_headerCreatorHolder;

	// Token: 0x04001D64 RID: 7524
	private int m_timeLeft;

	// Token: 0x04001D65 RID: 7525
	private static bool m_hasEnded;

	// Token: 0x04001D66 RID: 7526
	private EventMessage m_event;

	// Token: 0x04001D67 RID: 7527
	private UICanvas rankInfoContainer;
}
