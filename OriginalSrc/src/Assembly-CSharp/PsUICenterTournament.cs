using System;
using Server;
using UnityEngine;

// Token: 0x020002D1 RID: 721
public class PsUICenterTournament : UICanvas
{
	// Token: 0x0600155C RID: 5468 RVA: 0x000DC36C File Offset: 0x000DA76C
	public PsUICenterTournament(UIComponent _parent)
		: base(_parent, false, "Tournament UI", null, string.Empty)
	{
		PsUICenterTournament.m_isHost = false;
		if (PsMetagameManager.m_activeTournament.tournament.ownerId == PlayerPrefsX.GetUserId())
		{
			PsUICenterTournament.m_isHost = true;
		}
		if (!this.m_tournamentEnded)
		{
			int num = (int)Math.Ceiling((double)PsMetagameManager.m_activeTournament.localEndTime - Main.m_EPOCHSeconds);
			if (num < 0)
			{
				this.m_tournamentEnded = true;
				Debug.LogError("End everything.");
			}
		}
		PsUICenterTournament.m_hideLB = false;
		this.m_chatButtonVisible = true;
		this.m_header = new PsUITournamentHeader(this);
		this.m_header.SetSize(1f, PsUICenterTournament.m_headerHeight, RelativeTo.ScreenWidth);
		this.m_header.SetAlign(0.5f, 1f);
		this.m_header.SetDepthOffset(-20f);
		this.m_header.RemoveDrawHandler();
		if (PsUICenterTournament.m_lb == null)
		{
			PsUICenterTournament.CreateLeaderboard();
		}
		else
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.m_TAC.m_letTouchesThrough = true;
			PsUICenterTournament.m_lb.SetRollInfos(0);
			PsUICenterTournament.m_lb.Activate();
		}
		PsUICenterTournament.m_lb.AddRoomChangeCompletedAction(new Action<Tournament.TournamentLeaderboard>(this.ChangeRoomCompleted));
		PsUICenterTournament.m_lb.AddRoomChangeStartedAction(new Action(this.ChangeRoomStarted));
		PsUICenterTournament.m_lb.AddRoomChangingAction(new Action(this.ChangingRooms));
		PsUICenterTournament.m_lb.SetCameraToBottom(this.m_camera);
		this.m_footer = new PsUITournamentFooter(this);
		this.m_footer.SetSize(1f, PsUICenterTournament.m_footerHeight, RelativeTo.ScreenWidth);
		this.m_footer.SetAlign(0.5f, 0f);
		this.m_footer.SetDepthOffset(-25f);
		this.m_footer.RemoveDrawHandler();
		this.m_chatParent = new UIComponent(this, false, "chatParent", null, null, string.Empty);
		this.m_chatParent.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.m_chatParent.SetHeight(1f - (float)Screen.width * 0.092f / (float)Screen.height, RelativeTo.ScreenHeight);
		this.m_chatParent.SetMargins(-1f, 1f, 0f, 0f, RelativeTo.OwnWidth);
		this.m_chatParent.SetAlign(0f, 0f);
		this.m_chatParent.RemoveDrawHandler();
		this.m_chat = new PsUIChatTournament(this.m_chatParent);
		this.m_chat.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_chat.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_chat.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_chat.SetAlign(0f, 0f);
		this.m_chat.AddChatCommentsLoadedCallback(new Action(this.NewChatCommentsLoaded));
		CameraS.MoveToBehindOther(this.m_chat.m_commentArea.m_camera, this.m_camera);
		this.m_chat.SetCamera(PsUICenterTournament.m_lb.m_camera, false, true);
		this.m_chatButtonParent = new UIComponent(this.m_chat, false, "chatbuttonParent", null, null, string.Empty);
		this.m_chatButtonParent.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_chatButtonParent.SetMargins(1f, -1f, 0f, 0f, RelativeTo.ParentWidth);
		this.m_chatButtonParent.RemoveDrawHandler();
		this.m_chatButton = new UIRectSpriteButton(this.m_chatButtonParent, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_chat_button", null), true, false);
		this.m_chatButton.SetHorizontalAlign(0f);
		this.m_chatButton.SetVerticalAlign(0.8f);
		this.m_chatButton.SetHeight(0.07f, RelativeTo.ScreenWidth);
		this.m_chatButton.SetDepthOffset(2f);
		Frame frame;
		if (!this.chatHidden)
		{
			frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_chat_close", null);
		}
		else
		{
			frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_chat", null);
		}
		this.m_chatButtonIcon = new UIFittedSprite(this.m_chatButton, false, string.Empty, PsState.m_uiSheet, frame, true, true);
		this.m_chatButtonIcon.SetWidth(0.8f, RelativeTo.ParentWidth);
		this.m_chatButtonIcon.SetHorizontalAlign(0.3f);
		this.RemoveDrawHandler();
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x000DC7F4 File Offset: 0x000DABF4
	public void NewChatCommentsLoaded()
	{
		long lastMessageTimeStamp = this.m_chat.GetLastMessageTimeStamp();
		if (this.chatHidden)
		{
			if (lastMessageTimeStamp != 0L)
			{
				int num = (int)(lastMessageTimeStamp / 1000L);
				if (num > PlayerPrefsX.GetTournamentChatTimestamp())
				{
					this.CreateNotification(this.m_chatButton, "!");
				}
			}
		}
		else
		{
			PlayerPrefsX.SetTournamentChatTimestamp((int)Main.m_EPOCHSeconds);
		}
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x000DC856 File Offset: 0x000DAC56
	public void ChangingRooms()
	{
		if (this.m_footer != null)
		{
			this.m_footer.RoomChangeStarted();
		}
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x000DC86E File Offset: 0x000DAC6E
	public void ChangeRoomStarted()
	{
		if (this.m_chat != null)
		{
			this.m_chat.RemoveOldData();
		}
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x000DC886 File Offset: 0x000DAC86
	public void ChangeRoomCompleted(Tournament.TournamentLeaderboard _data)
	{
		if (this.m_chat != null)
		{
			this.m_chat.ReloadNewData();
		}
		if (this.m_footer != null)
		{
			this.m_footer.RoomChangeCompleted();
		}
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x000DC8B4 File Offset: 0x000DACB4
	public static void CreateLeaderboard()
	{
		PsUICenterTournament.m_lb = new PsUITournamentLeaderboard(null);
		PsUICenterTournament.m_lb.SetHeight(1f - (PsUICenterTournament.m_footerHeight + PsUICenterTournament.m_headerHeight - 0.025f) * ((float)Screen.width / (float)Screen.height), RelativeTo.ScreenHeight);
		PsUICenterTournament.m_lb.SetVerticalAlign(0.525f);
		PsUICenterTournament.m_lb.SetWidth(PsUICenterTournament.m_lbWidth + 0.025f, RelativeTo.ScreenWidth);
		PsUICenterTournament.m_lb.RemoveDrawHandler();
		PsUICenterTournament.m_lb.Update();
		PsUICenterTournament.originalPosLeaderboard = PsUICenterTournament.m_lb.m_TC.transform.localPosition;
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x000DC950 File Offset: 0x000DAD50
	private void CreateNotification(UIComponent _parent, string _text)
	{
		if (this.m_notificationBase != null)
		{
			return;
		}
		this.m_notificationBase = new UICanvas(_parent, false, "notification", null, string.Empty);
		this.m_notificationBase.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
		this.m_notificationBase.SetAlign(1f, 1f);
		this.m_notificationBase.SetDepthOffset(-10f);
		this.m_notificationBase.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this.m_notificationBase, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
		uicanvas.SetMargins(0.15f, RelativeTo.OwnHeight);
		TweenC tweenC = TweenS.AddTransformTween(uicanvas.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		this.m_notificationBase.Update();
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x000DCA78 File Offset: 0x000DAE78
	private void RemoveNotification()
	{
		if (this.m_notificationBase != null)
		{
			this.m_notificationBase.Destroy();
		}
		this.m_notificationBase = null;
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x000DCA98 File Offset: 0x000DAE98
	private void ChatbuttonPressed()
	{
		PlayerPrefsX.SetTournamentChatTimestamp((int)Main.m_EPOCHSeconds);
		this.RemoveNotification();
		if (this.firstTimeChat)
		{
			this.originalPos = this.m_chat.m_TC.transform.position;
			this.targetXPos = this.originalPos.x - this.m_chatParent.m_TC.transform.position.x;
			this.firstTimeChat = false;
		}
		if (!this.chatMoving)
		{
			this.m_header.ToggleLeftCorner();
			this.m_footer.ToggleLeftCorner(false);
			if (this.chatHidden)
			{
				this.chatHidden = false;
				PsUICenterTournament.m_hideLB = false;
				this.m_chatButtonIcon.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_chat_close", null));
				this.m_chatButtonIcon.Update();
				TweenC tweenC = TweenS.AddTransformTween(this.m_chat.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, new Vector3(0f, 0f, 0f), 0.3f, 0f, true);
				TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
				{
					this.chatMoving = false;
				});
			}
			else
			{
				this.chatHidden = true;
				PsUICenterTournament.m_hideLB = false;
				this.m_chatButtonIcon.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_chat", null));
				this.m_chatButtonIcon.Update();
				TweenC tweenC2 = TweenS.AddTransformTween(this.m_chat.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, new Vector3(this.targetXPos, 0f, 0f), 0.3f, 0f, true);
				TweenS.AddTweenEndEventListener(tweenC2, delegate(TweenC _c)
				{
					this.chatMoving = false;
				});
			}
			this.chatMoving = true;
		}
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x000DCC44 File Offset: 0x000DB044
	private void LeaderBoardHide()
	{
		if (this.firstTimeLB)
		{
			this.targetXPosLeaderboard = PsUICenterTournament.originalPosLeaderboard.x + PsUICenterTournament.m_lb.m_actualWidth;
			this.firstTimeLB = false;
		}
		this.lbHidden = true;
		if (this.m_lbTween != null)
		{
			TweenS.RemoveComponent(this.m_lbTween);
			this.m_lbTween = null;
		}
		this.m_lbTween = TweenS.AddTransformTween(PsUICenterTournament.m_lb.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, new Vector3(this.targetXPosLeaderboard, PsUICenterTournament.originalPosLeaderboard.y, 0f), 0.3f, 0f, true);
		TweenS.AddTweenEndEventListener(this.m_lbTween, delegate(TweenC _tween)
		{
			if (this.m_lbTween != null)
			{
				TweenS.RemoveComponent(this.m_lbTween);
				this.m_lbTween = null;
				this.lbMoving = false;
			}
		});
		this.lbMoving = true;
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x000DCCFC File Offset: 0x000DB0FC
	private void LeaderBoardShow()
	{
		if (this.firstTimeLB)
		{
			PsUICenterTournament.originalPosLeaderboard = PsUICenterTournament.m_lb.m_TC.transform.position;
			this.targetXPosLeaderboard = PsUICenterTournament.originalPosLeaderboard.x + PsUICenterTournament.m_lb.m_actualWidth;
			this.firstTimeLB = false;
		}
		this.lbHidden = false;
		if (this.m_lbTween != null)
		{
			TweenS.RemoveComponent(this.m_lbTween);
			this.m_lbTween = null;
		}
		this.m_lbTween = TweenS.AddTransformTween(PsUICenterTournament.m_lb.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, new Vector3(PsUICenterTournament.originalPosLeaderboard.x, PsUICenterTournament.originalPosLeaderboard.y, 0f), 0.3f, 0f, true);
		TweenS.AddTweenEndEventListener(this.m_lbTween, delegate(TweenC _tween)
		{
			if (this.m_lbTween != null)
			{
				TweenS.RemoveComponent(this.m_lbTween);
				this.m_lbTween = null;
				this.lbMoving = false;
			}
		});
		this.lbMoving = true;
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x000DCDD4 File Offset: 0x000DB1D4
	public void ShowTime(Action _tapCallback)
	{
		this.m_tapTimeAction = _tapCallback;
		this.m_timeVisible = true;
		int timeScoreOld = PsState.m_activeGameLoop.m_timeScoreOld;
		int timeScoreCurrent = PsState.m_activeGameLoop.m_timeScoreCurrent;
		int timeScoreBest = PsState.m_activeGameLoop.m_timeScoreBest;
		TouchAreaS.AddBeginTouchDelegate(new Func<TouchAreaC, bool>(this.AddTweens));
		int playerPosition = PsUITournamentLeaderboard.GetPlayerPosition(timeScoreCurrent);
		bool flag = playerPosition == 1;
		this.m_newRecord = timeScoreBest >= timeScoreCurrent;
		string text = HighScores.TimeScoreToTimeString(timeScoreCurrent);
		string text2 = HighScores.TimeScoreToTimeString(timeScoreBest);
		string text3 = PsStrings.Get(StringID.TOUR_NO_NEW_RECORD);
		string text4 = PsStrings.Get(StringID.TOUR_SLOWER_THAN_YOUR_BEST);
		string text5 = "+" + HighScores.TimeScoreToTimeString(timeScoreCurrent - timeScoreBest);
		string text6 = string.Empty;
		string text7 = string.Empty;
		string text8 = "#C54334";
		if (flag && this.m_newRecord)
		{
			this.m_footer.DestroyNewParentContent();
			text3 = PsStrings.Get(StringID.TOUR_NEW_POLE_POSITION_TIME);
			if (timeScoreOld > 0 && timeScoreOld != 2147483647)
			{
				text4 = PsStrings.Get(StringID.TOUR_IMPROVEMENT);
				text5 = "-" + HighScores.TimeScoreToTimeString(timeScoreOld - timeScoreCurrent);
			}
			else
			{
				text4 = string.Empty;
				text5 = string.Empty;
			}
			int nextTimeScore = PsUITournamentLeaderboard.GetNextTimeScore(timeScoreCurrent, true);
			if (nextTimeScore > 0)
			{
				text6 = PsStrings.Get(StringID.TOUR_YOUR_LEAD);
				text7 = "-" + HighScores.TimeScoreToTimeString(nextTimeScore - timeScoreCurrent);
			}
			text8 = "#F8B23D";
		}
		else if (this.m_newRecord)
		{
			this.m_footer.DestroyNewParentContent();
			text3 = PsStrings.Get(StringID.TOUR_PERSONAL_BEST);
			text4 = PsStrings.Get(StringID.TOUR_FROM_NEXT_REWARD);
			text5 = "+" + HighScores.TimeScoreToTimeString(timeScoreCurrent - PsUITournamentLeaderboard.GetNextTimeScore(timeScoreCurrent));
			text6 = PsStrings.Get(StringID.TOUR_FROM_LEAD);
			text7 = "+" + HighScores.TimeScoreToTimeString(timeScoreCurrent - PsUITournamentLeaderboard.GetBestTimeScore());
			text8 = "#F8B23D";
		}
		this.m_skipTimeScreen = new UICanvas(this, true, "TournamentSkipTimeScreen", null, string.Empty);
		this.m_skipTimeScreen.SetCamera(PsUICenterTournament.m_lb.m_camera, false, true);
		this.m_skipTimeScreen.SetWidth(1.1f, RelativeTo.ParentWidth);
		this.m_skipTimeScreen.SetHeight(1.1f, RelativeTo.ParentHeight);
		this.m_skipTimeScreen.m_TAC.m_letTouchesThrough = true;
		this.m_skipTimeScreen.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.BgDrawhandler));
		this.m_timeholder = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_timeholder.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_timeholder.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_timeholder.SetMargins(0f, PsUICenterTournament.m_lbWidth, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_timeholder.RemoveDrawHandler();
		this.m_timeholder.SetDepthOffset(30f);
		this.m_timeInfoList = new UIVerticalList(this.m_timeholder, string.Empty);
		this.m_timeInfoList.SetWidth(0.6f, RelativeTo.ScreenHeight);
		this.m_timeInfoList.SetMargins(0f, 0.03f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_timeInfoList.SetAlign(1f, 0.5f);
		this.m_timeInfoList.SetDepthOffset(-30f);
		this.m_timeInfoList.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this.m_timeInfoList, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.11f, RelativeTo.ParentWidth);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
		uifittedText.SetHorizontalAlign(1f);
		UICanvas uicanvas2 = new UICanvas(this.m_timeInfoList, false, string.Empty, null, string.Empty);
		uicanvas2.SetSize(1f, 0.3f, RelativeTo.ParentWidth);
		uicanvas2.SetHorizontalAlign(1f);
		uicanvas2.RemoveDrawHandler();
		this.m_playerTimeTweenHolder = new UICanvas(uicanvas2, false, "playerTimeHolder", null, string.Empty);
		this.m_playerTimeTweenHolder.SetHorizontalAlign(1f);
		this.m_playerTimeTweenHolder.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(this.m_playerTimeTweenHolder, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), true, text8, "#2D2924");
		uifittedText2.SetAlign(1f, 0f);
		uifittedText2.SetDepthOffset(-50f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_timeInfoList, string.Empty);
		uihorizontalList.SetHeight(0.07f, RelativeTo.ParentWidth);
		uihorizontalList.SetHorizontalAlign(1f);
		uihorizontalList.RemoveDrawHandler();
		UICanvas uicanvas3 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetHorizontalAlign(1f);
		uicanvas3.RemoveDrawHandler();
		UIFittedText uifittedText3 = new UIFittedText(uicanvas3, false, string.Empty, text4, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#D8CBB2", null);
		uifittedText3.SetHorizontalAlign(1f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, text5, PsFontManager.GetFont(PsFonts.HurmeBoldMN), 1f, RelativeTo.ParentHeight, null, null);
		uitext.SetHorizontalAlign(1f);
		if (this.m_newRecord)
		{
			UIHorizontalList uihorizontalList2 = new UIHorizontalList(this.m_timeInfoList, string.Empty);
			uihorizontalList2.SetHeight(0.07f, RelativeTo.ParentWidth);
			uihorizontalList2.SetHorizontalAlign(1f);
			uihorizontalList2.RemoveDrawHandler();
			UICanvas uicanvas4 = new UICanvas(uihorizontalList2, false, string.Empty, null, string.Empty);
			uicanvas4.SetHorizontalAlign(1f);
			uicanvas4.RemoveDrawHandler();
			UIFittedText uifittedText4 = new UIFittedText(uicanvas4, false, string.Empty, text6, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#D8CBB2", null);
			uifittedText4.SetHorizontalAlign(1f);
			UIText uitext2 = new UIText(uihorizontalList2, false, string.Empty, text7, PsFontManager.GetFont(PsFonts.HurmeBoldMN), 1f, RelativeTo.ParentHeight, null, null);
			uitext2.SetHorizontalAlign(1f);
		}
		this.m_timeholder.Update();
		this.m_skipTimeScreen.Update();
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x000DD3A4 File Offset: 0x000DB7A4
	private bool AddTweens(TouchAreaC _touchAreaC)
	{
		float num = 1f;
		float num2 = 0.1f;
		if (this.m_chatButton != null && this.m_chatButton.m_TAC == _touchAreaC)
		{
			num = 0f;
		}
		else if (this.m_footer != null && this.m_footer.m_start != null && this.m_footer.m_start.m_TAC == _touchAreaC)
		{
			num = 0f;
			num2 = 0f;
			this.m_tapTimeAction = null;
		}
		if (this.m_tapTimeAction != null)
		{
			this.m_tapTimeAction.Invoke();
			this.m_tapTimeAction = null;
		}
		if (this.m_skipTimeScreen != null)
		{
			this.m_skipTimeScreen.Destroy();
			this.m_skipTimeScreen = null;
		}
		if (this.m_newRecord)
		{
			if (this.m_footer != null && this.m_timeInfoList != null && this.m_playerTimeTweenHolder != null)
			{
				float timeTweenScale = this.m_footer.GetTimeTweenScale(RelativeTo.ScreenWidth);
				Vector3 vector = this.m_footer.GetTimeTweenPosition();
				vector = this.m_playerTimeTweenHolder.m_TC.transform.InverseTransformPoint(vector);
				TweenC tweenC = TweenS.AddTransformTween(this.m_playerTimeTweenHolder.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_playerTimeTweenHolder.m_TC.transform.localPosition, vector, num, 0f, false, true);
				TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _twnP)
				{
					this.m_playerTimeTweenHolder.Parent(this.m_footer.GetTimeTweenNewParent());
					if (this.m_timeholder != null)
					{
						this.m_timeholder.Destroy();
						this.m_timeholder = null;
					}
					this.m_timeVisible = false;
				});
				TweenC tweenC2 = TweenS.AddTransformTween(this.m_playerTimeTweenHolder.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(timeTweenScale / (this.m_playerTimeTweenHolder.m_actualHeight / (float)Screen.width), timeTweenScale / (this.m_playerTimeTweenHolder.m_actualHeight / (float)Screen.width), 1f), num, 0f, true);
				this.m_playerTimeTweenHolder.Parent(null);
				TweenC tweenC3 = TweenS.AddTransformTween(this.m_timeInfoList.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(0f, 0f, 0f), num2, 0f, true);
			}
		}
		else if (this.m_timeInfoList != null && this.m_timeholder != null)
		{
			TweenC tweenC4 = TweenS.AddTransformTween(this.m_timeInfoList.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(0f, 0f, 0f), num2, 0f, true);
			TweenS.AddTweenEndEventListener(tweenC4, delegate(TweenC _twnSP)
			{
				if (this.m_timeholder != null)
				{
					this.m_timeholder.Destroy();
					this.m_timeholder = null;
					this.m_timeInfoList = null;
					this.m_playerTimeTweenHolder = null;
				}
				this.m_timeVisible = false;
			});
		}
		return true;
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x000DD5EC File Offset: 0x000DB9EC
	public override void Destroy()
	{
		if (this.m_playerTimeTweenHolder != null && this.m_playerTimeTweenHolder.m_parent == null)
		{
			this.m_playerTimeTweenHolder.Destroy();
		}
		this.m_timeholder = null;
		this.m_timeInfoList = null;
		this.m_playerTimeTweenHolder = null;
		if (PsUICenterTournament.m_lb != null)
		{
			PsUICenterTournament.m_lb.RemoveRoomChangeCompletedAction(new Action<Tournament.TournamentLeaderboard>(this.ChangeRoomCompleted));
			PsUICenterTournament.m_lb.RemoveRoomChangeStartedAction(new Action(this.ChangeRoomStarted));
			PsUICenterTournament.m_lb.RemoveRoomChangingAction(new Action(this.ChangingRooms));
			PsUICenterTournament.m_lb.Deactivate();
			PsUICenterTournament.m_lb.m_TC.transform.localPosition = PsUICenterTournament.originalPosLeaderboard;
		}
		base.Destroy();
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x000DD6AC File Offset: 0x000DBAAC
	public override void Step()
	{
		string text = null;
		if (this.GetRoot() is PsUIBasePopup)
		{
			text = (this.GetRoot() as PsUIBasePopup).m_guid;
		}
		if (Main.AndroidBackButtonPressed(text))
		{
			TouchAreaS.RemoveBeginTouchDelegates();
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			TouchAreaS.CancelAllTouches(null);
		}
		if (!this.m_tournamentEnded)
		{
			int num = (int)Math.Ceiling((double)PsMetagameManager.m_activeTournament.localEndTime - Main.m_EPOCHSeconds);
			if (num < 0)
			{
				this.m_tournamentEnded = true;
				this.m_footer.TournamentEnded();
				Debug.LogError("End everything.");
			}
		}
		if (this.m_chatButton != null && this.m_chatButton.m_hit)
		{
			this.ChatbuttonPressed();
		}
		if (PsUICenterTournament.m_hideLB && !this.lbHidden)
		{
			this.LeaderBoardHide();
		}
		else if (!PsUICenterTournament.m_hideLB && this.lbHidden)
		{
			this.LeaderBoardShow();
		}
		if (this.m_chat != null)
		{
			this.m_chat.UpdateLogic();
		}
		base.Step();
	}

	// Token: 0x040017FF RID: 6143
	private PsUIGenericButton m_cc;

	// Token: 0x04001800 RID: 6144
	private PsUIChatTournament m_chat;

	// Token: 0x04001801 RID: 6145
	private UIComponent m_chatParent;

	// Token: 0x04001802 RID: 6146
	private UIComponent m_playerTimeTweenHolder;

	// Token: 0x04001803 RID: 6147
	private UIRectSpriteButton m_chatButton;

	// Token: 0x04001804 RID: 6148
	private PsUITournamentHeader m_header;

	// Token: 0x04001805 RID: 6149
	public PsUITournamentFooter m_footer;

	// Token: 0x04001806 RID: 6150
	private UICanvas m_skipTimeScreen;

	// Token: 0x04001807 RID: 6151
	private UIComponent m_chatButtonParent;

	// Token: 0x04001808 RID: 6152
	private UIFittedSprite m_chatButtonIcon;

	// Token: 0x04001809 RID: 6153
	public static bool m_hideLB;

	// Token: 0x0400180A RID: 6154
	private float m_showTimeTweenWait = 4f;

	// Token: 0x0400180B RID: 6155
	private PsGameLoopTournament m_tournament;

	// Token: 0x0400180C RID: 6156
	public bool m_tournamentEnded;

	// Token: 0x0400180D RID: 6157
	public static bool m_isHost;

	// Token: 0x0400180E RID: 6158
	private static readonly float m_headerHeight = 0.1f;

	// Token: 0x0400180F RID: 6159
	private static readonly float m_footerHeight = 0.12f;

	// Token: 0x04001810 RID: 6160
	private static readonly float m_lbWidth = 0.385f;

	// Token: 0x04001811 RID: 6161
	private bool m_timeVisible;

	// Token: 0x04001812 RID: 6162
	private bool m_newRecord;

	// Token: 0x04001813 RID: 6163
	private bool m_chatButtonVisible;

	// Token: 0x04001814 RID: 6164
	public static PsUITournamentLeaderboard m_lb;

	// Token: 0x04001815 RID: 6165
	private UICanvas m_timeholder;

	// Token: 0x04001816 RID: 6166
	private UIVerticalList m_timeInfoList;

	// Token: 0x04001817 RID: 6167
	private bool m_chatButtonMoving;

	// Token: 0x04001818 RID: 6168
	public UICanvas m_notificationBase;

	// Token: 0x04001819 RID: 6169
	private bool chatMoving;

	// Token: 0x0400181A RID: 6170
	private bool chatHidden = true;

	// Token: 0x0400181B RID: 6171
	private bool lbMoving;

	// Token: 0x0400181C RID: 6172
	private bool lbHidden;

	// Token: 0x0400181D RID: 6173
	private bool firstTimeChat = true;

	// Token: 0x0400181E RID: 6174
	private bool firstTimeLB = true;

	// Token: 0x0400181F RID: 6175
	private float targetXPos;

	// Token: 0x04001820 RID: 6176
	private static Vector3 originalPosLeaderboard;

	// Token: 0x04001821 RID: 6177
	private float targetXPosLeaderboard;

	// Token: 0x04001822 RID: 6178
	private Vector3 originalPos = Vector3.zero;

	// Token: 0x04001823 RID: 6179
	private TweenC m_lbTween;

	// Token: 0x04001824 RID: 6180
	public Action m_tapTimeAction;
}
