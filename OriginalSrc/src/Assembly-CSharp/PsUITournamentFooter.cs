using System;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class PsUITournamentFooter : UICanvas
{
	// Token: 0x060015C9 RID: 5577 RVA: 0x000E269C File Offset: 0x000E0A9C
	public PsUITournamentFooter(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		PsUITournamentFooter.m_currentTime = (int)Math.Ceiling(Main.m_EPOCHSeconds);
		this.m_tournament = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage;
		PsUITournamentFooter.m_waitTimeEnd = (double)((PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.localEndTime + this.m_waitTimeSeconds);
		PsUITournamentFooter.m_tournamentEnded = (_parent as PsUICenterTournament).m_tournamentEnded;
		bool acceptingNewScores = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.acceptingNewScores;
		if (!PsUITournamentFooter.m_endWait && PsUITournamentFooter.m_currentTime > this.m_tournament.localEndTime && acceptingNewScores)
		{
			PsUITournamentFooter.m_endWait = true;
		}
		this.m_tournamentLoop = PsState.m_activeGameLoop as PsGameLoopTournament;
		float currentPerformance = PsUpgradeManager.GetCurrentPerformance(PsState.GetCurrentVehicleType(true));
		float ccCap = this.m_tournamentLoop.GetCcCap();
		this.m_playerCC = PsUpgradeManager.GetCurrentPerformance(PsState.GetCurrentVehicleType(true));
		float overrideCC = (PsState.m_activeGameLoop as PsGameLoopTournament).GetOverrideCC();
		this.m_playerCC = ((overrideCC == -1f) ? this.m_playerCC : overrideCC);
		this.m_tournamentCC = ccCap;
		string playerUnit = this.m_tournamentLoop.m_minigameMetaData.playerUnit;
		this.m_timeScore = Mathf.Min(PsState.m_activeGameLoop.m_timeScoreCurrent, PsState.m_activeGameLoop.m_timeScoreBest);
		string text;
		if (this.m_timeScore == 2147483647 || this.m_timeScore == 0)
		{
			text = "--.---";
		}
		else
		{
			text = HighScores.TimeScoreToTimeString(this.m_timeScore);
		}
		string text2 = currentPerformance.ToString();
		string text3 = this.m_playerCC.ToString();
		if (!PsUITournamentFooter.m_tournamentEnded)
		{
			this.m_leftContainer = new UICanvas(this, false, string.Empty, null, string.Empty);
			this.m_leftContainer.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_leftContainer.SetWidth(0.35f, RelativeTo.ParentWidth);
			this.m_leftContainer.SetAlign(0f, 0f);
			this.m_leftContainer.SetDepthOffset(50f);
			this.m_leftContainer.RemoveDrawHandler();
			UICanvas uicanvas = new UICanvas(this.m_leftContainer, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
			uicanvas.SetWidth(0.65f, RelativeTo.ParentWidth);
			uicanvas.SetAlign(0f, 0f);
			uicanvas.RemoveDrawHandler();
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.SetMargins(0f, 0f, -1f - uicanvas2.m_height * 0.25f, 1f + uicanvas2.m_height * 0.25f, RelativeTo.OwnHeight);
			uicanvas2.RemoveDrawHandler();
			this.m_boosterButton = new PsUIBoosterButtonTournament(uicanvas2);
			this.m_boosterButton.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenWidth);
			this.m_boosterButton.SetVerticalAlign(0f);
			this.m_boosterButton.SetHorizontalAlign(0f);
			this.m_boosterButton.SetDepthOffset(-100f);
			if (this.m_boosterButton.IsUnavailable())
			{
				this.m_boosterButton.GreyScaleOn();
			}
			this.m_boosterButton.ShowRefillButton();
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_bottom_corner_banner", null);
			frame.flipX = true;
			UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true);
			uisprite.SetSize(frame.width / frame.height, 1f, RelativeTo.ParentHeight);
			uisprite.SetAlign(0f, 0f);
			uisprite.SetDepthOffset(1f);
			UICanvas uicanvas3 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas3.SetWidth(1.6f, RelativeTo.ParentHeight);
			uicanvas3.SetHeight(0.25f, RelativeTo.ParentHeight);
			uicanvas3.SetAlign(0f, 1.2f);
			uicanvas3.RemoveDrawHandler();
			Frame frame2;
			if (playerUnit == "OffroadCar")
			{
				frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_vehicle_logo_offroader", null);
			}
			else if (playerUnit == "Motorcycle")
			{
				frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_vehicle_logo_dirtbike", null);
			}
			else
			{
				frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_vehicle_logo_dirtbike", null);
			}
			string text4;
			if (this.m_playerCC >= ccCap)
			{
				text4 = "#F8B23D";
			}
			else
			{
				text4 = "#F95749";
			}
			UISprite uisprite2 = new UISprite(uicanvas3, false, string.Empty, PsState.m_uiSheet, frame2, true);
			uisprite2.SetSize(frame2.width / frame2.height, 1f, RelativeTo.ParentHeight);
			uisprite2.SetAlign(0.5f, 0.5f);
			UICanvas uicanvas4 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas4.SetHeight(0.55f, RelativeTo.ParentHeight);
			uicanvas4.SetWidth(1.7f, RelativeTo.ParentHeight);
			uicanvas4.SetMargins(0.25f, 0f, 0f, 0f, RelativeTo.OwnHeight);
			uicanvas4.SetAlign(0f, 0.75f);
			uicanvas4.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas4, false, string.Empty, text3 + "cc", PsFontManager.GetFont(PsFonts.HurmeBold), true, text4, "#000000");
			UICanvas uicanvas5 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas5.SetHeight(0.25f, RelativeTo.ParentHeight);
			uicanvas5.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas5.SetMargins(0.5f, 0.5f, 0f, 0f, RelativeTo.OwnHeight);
			uicanvas5.SetVerticalAlign(0.1f);
			uicanvas5.RemoveDrawHandler();
			string text5 = string.Empty;
			if (currentPerformance < ccCap)
			{
				text5 = PsStrings.Get(StringID.TOUR_BOOSTED_FROM) + text2 + "cc";
			}
			else
			{
				text5 = PsStrings.Get(StringID.TOUR_CAPPED_FROM) + text2 + "cc";
			}
			UIFittedText uifittedText2 = new UIFittedText(uicanvas5, false, string.Empty, text5, PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
		}
		this.m_rightContainer = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_rightContainer.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_rightContainer.SetWidth(0.45f, RelativeTo.ParentWidth);
		this.m_rightContainer.SetAlign(1f, 0f);
		this.m_rightContainer.RemoveDrawHandler();
		this.m_rightLeftContainer = new UICanvas(this.m_rightContainer, false, string.Empty, null, string.Empty);
		this.m_rightLeftContainer.SetHeight(0.88f, RelativeTo.ParentHeight);
		this.m_rightLeftContainer.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_rightLeftContainer.SetAlign(0f, 0f);
		this.m_rightLeftContainer.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TournamentFooterRight));
		this.m_timeHolder = new UICanvas(this.m_rightLeftContainer, false, string.Empty, null, string.Empty);
		this.m_timeHolder.SetSize(1.9f, 0.85f, RelativeTo.ParentHeight);
		this.m_timeHolder.SetAlign(0f, 1f);
		this.m_timeHolder.SetMargins(0.65f, 0f, 0.1f, 0f, RelativeTo.OwnHeight);
		this.m_timeHolder.RemoveDrawHandler();
		this.m_bestTimeTitleHolder = new UICanvas(this.m_timeHolder, false, string.Empty, null, string.Empty);
		this.m_bestTimeTitleHolder.SetHeight(0.2f, RelativeTo.ParentHeight);
		this.m_bestTimeTitleHolder.SetAlign(1f, 1f);
		this.m_bestTimeTitleHolder.RemoveDrawHandler();
		this.m_bestTimeTitle = new UIFittedText(this.m_bestTimeTitleHolder, false, string.Empty, PsStrings.Get(StringID.TOUR_PERSONAL_BEST_CONSTANT), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
		this.m_bestTimeTitle.SetHorizontalAlign(1f);
		this.m_bestTimeHolder = new UICanvas(this.m_timeHolder, false, string.Empty, null, string.Empty);
		this.m_bestTimeHolder.SetHeight(0.6f, RelativeTo.ParentHeight);
		this.m_bestTimeHolder.SetAlign(1f, 0.5f);
		this.m_bestTimeHolder.RemoveDrawHandler();
		this.m_bestTime = new UIFittedText(this.m_bestTimeHolder, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#F8B23D", null);
		this.m_bestTime.SetAlign(1f, 0f);
		if (!PsUICenterTournament.m_isHost)
		{
			this.m_bestTimeInfoHolder = new UICanvas(this.m_timeHolder, false, string.Empty, null, string.Empty);
			this.m_bestTimeInfoHolder.SetHeight(0.3f, RelativeTo.ParentHeight);
			this.m_bestTimeInfoHolder.SetAlign(1f, 0f);
			this.m_bestTimeInfoHolder.SetWidth(0f, RelativeTo.ScreenHeight);
			this.m_bestTimeInfoHolder.SetMargins(0f, 0f, 0.35f, -0.35f, RelativeTo.OwnHeight);
			this.m_bestTimeInfoHolder.RemoveDrawHandler();
			int num = ((PsState.m_activeGameLoop.m_timeScoreBest == int.MaxValue || PsState.m_activeGameLoop.m_timeScoreBest == 0) ? (-1) : PsState.m_activeGameLoop.m_timeScoreBest);
			if (PsState.m_activeGameLoop.m_timeScoreCurrent != 2147483647 && PsState.m_activeGameLoop.m_timeScoreCurrent != 0 && num == -1)
			{
				num = PsState.m_activeGameLoop.m_timeScoreCurrent;
			}
			else
			{
				num = Math.Min(num, PsState.m_activeGameLoop.m_timeScoreCurrent);
			}
			PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry = PsUICenterTournament.m_lb.m_entryDictionary[PlayerPrefsX.GetUserId()];
			this.m_player = psUITournamentLeaderboardEntry;
			this.m_player.SubscribeToPrizeChange(new Action<string, string, bool>(this.UpdatePrizeCallback));
			if ((PsUITournamentLeaderboard.m_hostId != PlayerPrefsX.GetUserId() && psUITournamentLeaderboardEntry.m_highscoreData.time != 0 && psUITournamentLeaderboardEntry.m_highscoreData.time != 2147483647) || (PsState.m_activeGameLoop.m_timeScoreCurrent != 0 && PsState.m_activeGameLoop.m_timeScoreCurrent != 2147483647))
			{
				this.m_prize = psUITournamentLeaderboardEntry.GetPrizeUI(this.m_bestTimeInfoHolder, psUITournamentLeaderboardEntry.m_position, false, string.Empty, false, false);
				this.m_prizeChestFrame = (this.m_prize.m_childs[0].m_childs[0] as UIText).m_tmc.m_textMesh.text;
			}
		}
		this.m_rightRightContainer = new UICanvas(this.m_rightContainer, false, string.Empty, null, string.Empty);
		this.m_rightRightContainer.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_rightRightContainer.SetWidth(0.53f, RelativeTo.ParentWidth);
		this.m_rightRightContainer.SetAlign(1f, 0f);
		this.m_rightRightContainer.SetDepthOffset(-20f);
		this.m_rightRightContainer.RemoveDrawHandler();
		Frame frame3 = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_bottom_corner_banner", null);
		UISprite uisprite3 = new UISprite(this.m_rightRightContainer, false, string.Empty, PsState.m_uiSheet, frame3, true);
		uisprite3.SetSize(frame3.width / frame3.height, 1f, RelativeTo.ParentHeight);
		uisprite3.SetAlign(1f, 0f);
		uisprite3.SetDepthOffset(1f);
		this.SetRightContents();
	}

	// Token: 0x060015CA RID: 5578 RVA: 0x000E3218 File Offset: 0x000E1618
	public void RoomChangeStarted()
	{
		if (this.m_start != null && !this.m_startDisabled)
		{
			this.m_startDisabled = true;
			this.m_start.SetGrayColors();
			this.m_start.Update();
			this.m_start.DisableTouchAreas(true);
		}
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x000E3264 File Offset: 0x000E1664
	public void RoomChangeCompleted()
	{
		if (this.m_start != null && this.m_startDisabled)
		{
			this.m_startDisabled = false;
			this.m_start.SetOrangeColors(true);
			this.m_start.Update();
			this.m_start.EnableTouchAreas(true);
		}
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x000E32B4 File Offset: 0x000E16B4
	public void UpdatePrizeCallback(string _text, string _chestFrameName, bool _updateHat)
	{
		if (this.m_prize != null)
		{
			this.m_prize.Destroy();
		}
		if (this.m_player != null && !this.m_footerPrizeUpdatedOnThisFrame)
		{
			this.m_prize = this.m_player.GetPrizeUI(this.m_bestTimeInfoHolder, this.m_player.m_position, false, string.Empty, false, false);
			this.m_prize.Update();
			this.m_footerPrizeUpdatedOnThisFrame = true;
		}
	}

	// Token: 0x060015CD RID: 5581 RVA: 0x000E3329 File Offset: 0x000E1729
	public override void Destroy()
	{
		if (this.m_player != null)
		{
			this.m_player.UnsubscribeFromPrizeChange();
		}
		base.Destroy();
	}

	// Token: 0x060015CE RID: 5582 RVA: 0x000E3347 File Offset: 0x000E1747
	public void TournamentEnded()
	{
		if (!PsUITournamentFooter.m_tournamentEnded)
		{
			PsUITournamentFooter.m_tournamentEnded = true;
			this.DestroyLeftSide();
			this.SetRightContents();
		}
	}

	// Token: 0x060015CF RID: 5583 RVA: 0x000E3368 File Offset: 0x000E1768
	private void DestroyLeftSide()
	{
		if (this.m_leftContainer != null)
		{
			TweenC tweenC = TweenS.AddTransformTween(this.m_leftContainer.m_TC, TweenedProperty.Position, TweenStyle.Linear, this.m_leftContainer.m_TC.transform.localPosition - new Vector3(this.m_leftContainer.m_actualWidth, 0f, 0f), 0.3f, 0f, true);
			TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
			{
				if (this.m_leftContainer != null)
				{
					this.m_leftContainer.Destroy();
					this.m_leftContainer = null;
				}
			});
		}
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x000E33E4 File Offset: 0x000E17E4
	public void SetRightContents()
	{
		if (PsUITournamentFooter.m_tournamentEnded && (PsMetagameManager.m_activeTournament.tournament.claimed || this.m_timeScore == 0 || this.m_timeScore == 2147483647) && !PsUICenterTournament.m_isHost)
		{
			UICanvas uicanvas = new UICanvas(this.m_rightRightContainer, false, string.Empty, null, string.Empty);
			uicanvas.SetMargins(0f, 0f, 0.08f, 0.08f, RelativeTo.OwnHeight);
			uicanvas.SetWidth(0.65f, RelativeTo.ParentWidth);
			uicanvas.RemoveDrawHandler();
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.2f, RelativeTo.ParentHeight);
			uicanvas2.SetVerticalAlign(1f);
			uicanvas2.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TOUR_FINAL_POSITION), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFF", "#000");
			UICanvas uicanvas3 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas3.SetHeight(0.8f, RelativeTo.ParentHeight);
			uicanvas3.SetVerticalAlign(0f);
			uicanvas3.RemoveDrawHandler();
			string text = PsUITournamentLeaderboard.GetOwnPosition().ToString();
			if (this.m_timeScore == 0 || this.m_timeScore == 2147483647)
			{
				text = PsStrings.Get(StringID.TOUR_UNQUALIFIED);
				new UIText(uicanvas3, false, string.Empty, text + ".", PsFontManager.GetFont(PsFonts.HurmeBold), 0.35f, RelativeTo.ParentHeight, "#F8B23D", null);
			}
			else
			{
				new UIText(uicanvas3, false, string.Empty, text + ".", PsFontManager.GetFont(PsFonts.HurmeBold), 1f, RelativeTo.ParentHeight, "#F8B23D", null);
			}
			if (this.m_start != null)
			{
				this.m_start.Destroy();
				this.m_start = null;
			}
		}
		else
		{
			if (this.m_start != null)
			{
				this.m_start.Destroy();
				this.m_start = null;
			}
			this.CreateButton(this.m_rightRightContainer);
		}
		this.m_rightRightContainer.Update();
	}

	// Token: 0x060015D1 RID: 5585 RVA: 0x000E35F0 File Offset: 0x000E19F0
	public void ToggleLeftCorner(bool _instant)
	{
		if (this.m_leftContainer == null)
		{
			return;
		}
		if (!this.containerMoving && this.m_leftContainer != null)
		{
			if (this.containerHidden)
			{
				this.containerHidden = false;
				if (_instant)
				{
					TransformS.Move(this.m_leftContainer.m_TC, this.m_leftContainer.m_TC.transform.localPosition + new Vector3(250f, 0f, 0f));
				}
				else
				{
					TweenC tweenC = TweenS.AddTransformTween(this.m_leftContainer.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_leftContainer.m_TC.transform.localPosition + new Vector3(this.m_leftContainer.m_actualWidth, 0f, 0f), 0.3f, 0f, true);
					TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
					{
						this.containerMoving = false;
					});
				}
			}
			else
			{
				this.containerHidden = true;
				if (_instant)
				{
					TransformS.Move(this.m_leftContainer.m_TC, this.m_leftContainer.m_TC.transform.localPosition - new Vector3(250f, 0f, 0f));
				}
				else
				{
					TweenC tweenC2 = TweenS.AddTransformTween(this.m_leftContainer.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_leftContainer.m_TC.transform.localPosition - new Vector3(this.m_leftContainer.m_actualWidth, 0f, 0f), 0.3f, 0f, true);
					TweenS.AddTweenEndEventListener(tweenC2, delegate(TweenC _c)
					{
						this.containerMoving = false;
					});
				}
			}
			if (!_instant)
			{
				this.containerMoving = true;
			}
			else
			{
				this.containerMoving = false;
			}
		}
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x000E37B0 File Offset: 0x000E1BB0
	private string GetWaitText()
	{
		string text = PsStrings.Get(StringID.TOUR_WAITING_FOR_OTHERS);
		if (this.dots < 3)
		{
			this.dots++;
			for (int i = 0; i < this.dots; i++)
			{
				text += ".";
			}
		}
		else
		{
			this.dots = 0;
		}
		return text;
	}

	// Token: 0x060015D3 RID: 5587 RVA: 0x000E3814 File Offset: 0x000E1C14
	private void CreateButton(UIComponent _parent)
	{
		if (PsUITournamentFooter.m_tournamentEnded && !PsUITournamentFooter.m_endWait)
		{
			this.m_start = new PsUIAttentionButton(_parent, default(Vector3), 0.25f, 0.25f, 0.005f);
		}
		else
		{
			this.m_start = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		}
		this.m_start.SetHorizontalAlign(0f);
		this.m_start.SetVerticalAlign(0.5f);
		this.m_start.SetHeight(0.7f, RelativeTo.ParentHeight);
		this.m_start.SetOrangeColors(true);
		float num = 3.2f;
		UICanvas uicanvas = new UICanvas(this.m_start, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(num, 1f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		if (PsUITournamentFooter.m_tournamentEnded && PsUITournamentFooter.m_endWait && !PsUICenterTournament.m_isHost)
		{
			this.m_start.SetGrayColors();
			this.m_start.DisableTouchAreas(true);
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.6f, RelativeTo.ParentHeight);
			uicanvas2.SetVerticalAlign(0.5f);
			uicanvas2.RemoveDrawHandler();
			this.m_endTime = new UITextbox(uicanvas2, false, string.Empty, this.GetWaitText(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.45f, RelativeTo.ParentHeight, false, Align.Center, Align.Top, null, true, null);
			this.m_endTime.m_tmc.m_horizontalAlign = Align.Left;
		}
		else if (PsUITournamentFooter.m_tournamentEnded && !PsUICenterTournament.m_isHost)
		{
			string chestIconName = PsGachaManager.GetChestIconName(Tournaments.GetGachaReward(PsUITournamentLeaderboard.GetOwnPosition(), PsUITournamentLeaderboard.GetPlayerCount()));
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(chestIconName, null);
			float num2 = frame.width / frame.height;
			UICanvas uicanvas3 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas3.SetWidth(num2, RelativeTo.ParentHeight);
			uicanvas3.SetHorizontalAlign(1f);
			uicanvas3.RemoveDrawHandler();
			UISprite uisprite = new UISprite(uicanvas3, false, string.Empty, PsState.m_uiSheet, frame, true);
			uisprite.SetHorizontalAlign(1f);
			uisprite.SetDepthOffset(-10f);
			UICanvas uicanvas4 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas4.SetWidth(1f - num2 / num, RelativeTo.ParentWidth);
			uicanvas4.SetHorizontalAlign(0f);
			uicanvas4.SetMargins(0f, 0.05f, 0f, 0f, RelativeTo.OwnWidth);
			uicanvas4.RemoveDrawHandler();
			UITextbox uitextbox = new UITextbox(uicanvas4, false, string.Empty, PsStrings.Get(StringID.TOUR_CLAIM_REWARD), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.3f, RelativeTo.ParentHeight, false, Align.Center, Align.Top, null, true, this.m_start.GetShadowColor());
			uitextbox.RemoveDrawHandler();
			this.m_start.EnableTouchAreas(true);
			this.m_start.SetReleaseAction(delegate
			{
				this.ClaimPrizes();
			});
		}
		else if (PsUITournamentFooter.m_tournamentEnded && PsUICenterTournament.m_isHost)
		{
			this.m_start.DisableTouchAreas(true);
			this.m_start.SetGrayColors();
			UICanvas uicanvas5 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas5.SetHeight(0.5f, RelativeTo.ParentHeight);
			uicanvas5.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas5, false, string.Empty, PsStrings.Get(StringID.RACE_START), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFF", this.m_start.GetShadowColor());
		}
		else
		{
			UICanvas uicanvas6 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas6.SetHeight(0.5f, RelativeTo.ParentHeight);
			uicanvas6.RemoveDrawHandler();
			UIFittedText uifittedText2 = new UIFittedText(uicanvas6, false, string.Empty, PsStrings.Get(StringID.RACE_START), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFF", this.m_start.GetShadowColor());
			this.m_start.SetReleaseAction(delegate
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Start");
			});
		}
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x000E3BEA File Offset: 0x000E1FEA
	private void ClaimPrizes()
	{
		if (this.m_start != null)
		{
			this.m_start.Destroy();
			this.m_start = null;
		}
		this.SetRightContents();
		(this.GetRoot() as PsUIBasePopup).CallAction("GivePrize");
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x000E3C25 File Offset: 0x000E2025
	public void DestroyNewParentContent()
	{
		if (this.m_bestTimeHolder != null)
		{
			this.m_bestTimeHolder.DestroyChildren();
		}
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x000E3C40 File Offset: 0x000E2040
	public Vector3 GetTimeTweenPosition()
	{
		return this.m_bestTimeHolder.m_TC.transform.position;
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x000E3C64 File Offset: 0x000E2064
	public float GetTimeTweenScale(RelativeTo _relative)
	{
		UIComponent uicomponent = this.m_bestTimeHolder;
		float num = uicomponent.m_height - uicomponent.m_parent.m_height * (uicomponent.m_parent.m_margins.t + uicomponent.m_parent.m_margins.b);
		while (uicomponent.m_heightRelativeTo != RelativeTo.ScreenWidth && uicomponent.m_heightRelativeTo != RelativeTo.ScreenHeight)
		{
			uicomponent = uicomponent.m_parent;
			if (uicomponent == null)
			{
				break;
			}
			float num2 = uicomponent.m_height - uicomponent.m_parent.m_height * (uicomponent.m_parent.m_margins.t + uicomponent.m_parent.m_margins.b);
			num *= num2;
		}
		if (uicomponent.m_heightRelativeTo == RelativeTo.ScreenWidth)
		{
			if (_relative == RelativeTo.ScreenHeight)
			{
				num = (float)Screen.width * num / (float)Screen.height;
			}
		}
		else if (_relative == RelativeTo.ScreenWidth)
		{
			num = (float)Screen.height * num / (float)Screen.width;
		}
		return num;
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x000E3D55 File Offset: 0x000E2155
	public UIComponent GetTimeTweenNewParent()
	{
		return this.m_bestTimeHolder;
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x000E3D60 File Offset: 0x000E2160
	public override void Step()
	{
		if (PsUITournamentFooter.m_tournamentEnded && PsState.m_activeGameLoop != null)
		{
			EventMessage eventMessage = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage;
			PsUITournamentFooter.m_waitTimeEnd = (double)(eventMessage.localEndTime + this.m_waitTimeSeconds);
			PsUITournamentFooter.m_currentTime = (int)Math.Ceiling(Main.m_EPOCHSeconds);
			int num = (int)Math.Ceiling(PsUITournamentFooter.m_waitTimeEnd - (double)PsUITournamentFooter.m_currentTime);
			if (this.m_endTime != null && PsUITournamentFooter.m_endWait && PsUITournamentFooter.m_waitTimeEnd >= (double)PsUITournamentFooter.m_currentTime && Main.m_gameTicks % 30 == 0)
			{
				this.m_endTime.SetText(this.GetWaitText());
			}
			bool acceptingNewScores = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.acceptingNewScores;
			if (PsUITournamentFooter.m_endWait && !acceptingNewScores)
			{
				PsUITournamentFooter.m_endWait = false;
				this.SetRightContents();
			}
			else if (!PsUITournamentFooter.m_endWait && acceptingNewScores && PsUITournamentFooter.m_currentTime > this.m_tournament.localEndTime)
			{
				PsUITournamentFooter.m_endWait = true;
				this.SetRightContents();
			}
		}
		if (this.m_boosterButton != null && this.m_boosterButton.m_button.m_hit)
		{
			Debug.LogError("Boosterbutton pressed");
			this.m_boosterButton.RefillButtonHit();
		}
		this.m_footerPrizeUpdatedOnThisFrame = false;
		base.Step();
	}

	// Token: 0x04001870 RID: 6256
	public PsUIGenericButton m_start;

	// Token: 0x04001871 RID: 6257
	private UICanvas m_rightContainer;

	// Token: 0x04001872 RID: 6258
	private UICanvas m_leftContainer;

	// Token: 0x04001873 RID: 6259
	private UICanvas m_rightLeftContainer;

	// Token: 0x04001874 RID: 6260
	private UICanvas m_rightRightContainer;

	// Token: 0x04001875 RID: 6261
	private UICanvas m_timeHolder;

	// Token: 0x04001876 RID: 6262
	private UICanvas m_bestTimeTitleHolder;

	// Token: 0x04001877 RID: 6263
	private UICanvas m_bestTimeHolder;

	// Token: 0x04001878 RID: 6264
	private UICanvas m_bestTimeInfoHolder;

	// Token: 0x04001879 RID: 6265
	private UICanvas m_boosterButtonParent;

	// Token: 0x0400187A RID: 6266
	private UIFittedText m_bestTimeTitle;

	// Token: 0x0400187B RID: 6267
	private UIFittedText m_bestTime;

	// Token: 0x0400187C RID: 6268
	private UITextbox m_endTime;

	// Token: 0x0400187D RID: 6269
	private PsGameLoopTournament m_tournamentLoop;

	// Token: 0x0400187E RID: 6270
	private EventMessage m_tournament;

	// Token: 0x0400187F RID: 6271
	public PsUIBoosterButtonTournament m_boosterButton;

	// Token: 0x04001880 RID: 6272
	private bool buttonMoving;

	// Token: 0x04001881 RID: 6273
	private bool containerMoving;

	// Token: 0x04001882 RID: 6274
	private bool containerHidden;

	// Token: 0x04001883 RID: 6275
	private bool firstTime = true;

	// Token: 0x04001884 RID: 6276
	private bool ccHighlit;

	// Token: 0x04001885 RID: 6277
	public bool buttonHidden = true;

	// Token: 0x04001886 RID: 6278
	private float targetYPos;

	// Token: 0x04001887 RID: 6279
	private float m_tournamentCC;

	// Token: 0x04001888 RID: 6280
	private float m_playerCC;

	// Token: 0x04001889 RID: 6281
	private static double m_waitTimeEnd;

	// Token: 0x0400188A RID: 6282
	private static int m_timeLeft;

	// Token: 0x0400188B RID: 6283
	private Vector3 originalPos = Vector3.zero;

	// Token: 0x0400188C RID: 6284
	private TweenC m_tween;

	// Token: 0x0400188D RID: 6285
	private static int m_currentTime;

	// Token: 0x0400188E RID: 6286
	private int m_timeScore;

	// Token: 0x0400188F RID: 6287
	private static bool m_tournamentEnded;

	// Token: 0x04001890 RID: 6288
	private static bool m_endWait;

	// Token: 0x04001891 RID: 6289
	private int m_waitTimeSeconds = 60;

	// Token: 0x04001892 RID: 6290
	private bool m_startDisabled;

	// Token: 0x04001893 RID: 6291
	private UICanvas m_prize;

	// Token: 0x04001894 RID: 6292
	private PsUITournamentLeaderboardEntry m_player;

	// Token: 0x04001895 RID: 6293
	private string m_prizeChestFrame;

	// Token: 0x04001896 RID: 6294
	private bool m_footerPrizeUpdatedOnThisFrame;

	// Token: 0x04001897 RID: 6295
	private int dots;
}
