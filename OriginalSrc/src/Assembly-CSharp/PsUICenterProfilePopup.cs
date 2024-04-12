using System;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x02000306 RID: 774
public class PsUICenterProfilePopup : PsUIHeaderedCanvas
{
	// Token: 0x060016BE RID: 5822 RVA: 0x000F19A8 File Offset: 0x000EFDA8
	public PsUICenterProfilePopup(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.35f, RelativeTo.ScreenHeight, 0.065f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.9f, RelativeTo.ScreenWidth);
		this.SetHeight(0.9f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0.012f, 0.06f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.05f, 0.05f, 0.025f, 0f, RelativeTo.ScreenHeight);
		this.m_footer.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIFooter));
		this.m_footer.SetMargins(0.06f, 0.06f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.loading = new PsUILoadingAnimation(this, false);
		UIText uitext = new UIText(this.m_footer, false, string.Empty, PsStrings.Get(StringID.VERSION) + " " + TrackedBundleVersion.Current.version, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.02f, RelativeTo.ScreenHeight, null, null);
		uitext.SetMargins(0f, 0.05f, 0f, 0f, RelativeTo.ParentHeight);
		uitext.SetAlign(1f, 0.5f);
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x000F1B38 File Offset: 0x000EFF38
	private void GetPlayerProfile()
	{
		HttpC playerProfile = Player.GetPlayerProfile(this.m_playerId, new Action<PlayerData>(this.GetPlayerProfileSucceed), new Action<HttpC>(this.GetPlayerProfileFailed), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, playerProfile);
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x000F1B7B File Offset: 0x000EFF7B
	private void GetPlayerProfileSucceed(PlayerData _playerData)
	{
		_playerData.teamId = null;
		this.SetUser(_playerData, false);
		this.m_parent.Update();
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x000F1B98 File Offset: 0x000EFF98
	private void GetPlayerProfileFailed(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), ServerErrors.GetNetworkError(_c.www.error), delegate
		{
			HttpC playerProfile = Player.GetPlayerProfile(this.m_playerId, new Action<PlayerData>(this.GetPlayerProfileSucceed), new Action<HttpC>(this.GetPlayerProfileFailed), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, playerProfile);
			return playerProfile;
		}, null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x000F1BCC File Offset: 0x000EFFCC
	public void SetUser(string _userId, bool m_fromTeam = false)
	{
		if (_userId != null)
		{
			this.m_playerId = _userId;
			this.GetPlayerProfile();
			Debug.Log("E_Test SetUser 1", null);
			if (this.m_playerId == PlayerPrefsX.GetUserId())
			{
				FrbMetrics.SetCurrentScreen("profile_own");
			}
			else
			{
				FrbMetrics.SetCurrentScreen("profile_other");
			}
		}
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x000F1C28 File Offset: 0x000F0028
	public void SetUser(PlayerData _user, bool _fromTeam = false)
	{
		this.m_fromTeam = _fromTeam;
		this.m_user = _user;
		this.m_playerId = this.m_user.playerId;
		this.m_friend = PsMetagameManager.IsFriend(_user.playerId);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
		Debug.Log("E_Test SetUser 2", null);
		if (this.m_user.playerId == PlayerPrefsX.GetUserId())
		{
			FrbMetrics.SetCurrentScreen("profile_own");
		}
		else
		{
			FrbMetrics.SetCurrentScreen("profile_other");
		}
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x000F1CB8 File Offset: 0x000F00B8
	public void CreateFooterContent(UIComponent _parent)
	{
		UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, PsStrings.Get(StringID.SOCIAL_BIG_BANG_POINTS_INFO), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x000F1CE4 File Offset: 0x000F00E4
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList.SetMargins(0.015f, 0f, 0.015f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetAlign(0f, 1f);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		this.m_profileImage = new PsUIProfileImage(uiverticalList, false, string.Empty, this.m_user.facebookId, this.m_user.gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, true, true);
		this.m_profileImage.SetSize(0.125f, 0.125f, RelativeTo.ScreenHeight);
		this.m_profileImage.SetAlign(0f, 1f);
		UICanvas uicanvas = new UICanvas(this.m_profileImage, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.06f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(0f, 1f);
		uicanvas.SetMargins(-0.005f, 0.005f, -0.005f, 0.005f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_user.countryCode, null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		uifittedSprite.SetAlign(0f, 1f);
		UIVerticalList uiverticalList2 = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList2.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList2.SetVerticalAlign(1f);
		uiverticalList2.RemoveDrawHandler();
		this.CreateUserName(uiverticalList2);
		if (!string.IsNullOrEmpty(this.m_user.youtubeName) || this.m_user.playerId == PlayerPrefsX.GetUserId())
		{
			int num = this.m_user.youtubeSubscriberCount;
			string text = this.m_user.youtubeName;
			if (string.IsNullOrEmpty(text))
			{
				num = -1;
				string text2 = PsStrings.Get(StringID.LINK_HEADER);
				text2 = text2.Replace("%1", "YouTube");
				text = text2;
			}
			this.m_youtubeUser = new UIFixedYoutubeButton(uiverticalList2, text, num, 0.32f, RelativeTo.ScreenWidth, 0.25f, 0.25f, 0.001f, "YoutubeButton");
			this.m_youtubeUser.SetHeight(0.055f, RelativeTo.ScreenHeight);
			if (this.m_user.playerId == PlayerPrefsX.GetUserId())
			{
				this.m_youtubeUser.SetReleaseAction(delegate
				{
					TouchAreaS.CancelAllTouches(null);
					PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterYoutubeLink), null, null, null, true, true, InitialPage.Center, false, false, false);
					TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
					popup.SetAction("Exit", delegate
					{
						popup.Destroy();
						this.YoutubeDone();
					});
				});
			}
			else if (!string.IsNullOrEmpty(this.m_user.youtubeId))
			{
				PsMetrics.YoutubeLinkOffered("profilePage", this.m_user.youtubeId, this.m_user.youtubeName, this.m_user.playerId, this.m_user.name);
				this.m_youtubeUser.SetReleaseAction(delegate
				{
					PsMetrics.YoutubePageOpened("profilePage", this.m_user.youtubeId, this.m_user.youtubeName, this.m_user.playerId, this.m_user.name);
					TouchAreaS.CancelAllTouches(null);
					Application.OpenURL("https://www.youtube.com/channel/" + this.m_user.youtubeId);
				});
			}
		}
		UICanvas uicanvas2 = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.23f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(0.5f, RelativeTo.ParentWidth);
		uicanvas2.SetMargins(0.01f, 0f, 0.165f, 0f, RelativeTo.ScreenHeight);
		uicanvas2.SetAlign(0f, 1f);
		uicanvas2.RemoveDrawHandler();
		int creatorRank = PlayerPrefsX.GetCreatorRank(this.m_user.totalLikes + this.m_user.totalSuperLikes * PsState.m_superLikeVisualMultiplier);
		UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.4f, RelativeTo.ParentHeight);
		uicanvas3.SetAlign(0f, 1f);
		uicanvas3.RemoveDrawHandler();
		string text3 = string.Empty;
		if (this.m_user.developer)
		{
			text3 = "Developer";
		}
		else
		{
			switch (creatorRank)
			{
			case 1:
				text3 = PsStrings.Get(StringID.CREATOR_RANK_FLEDGING);
				break;
			case 2:
				text3 = PsStrings.Get(StringID.CREATOR_RANK_AKNOWLEDGED);
				break;
			case 3:
				text3 = PsStrings.Get(StringID.CREATOR_RANK_ADVANCED);
				break;
			case 4:
				text3 = PsStrings.Get(StringID.CREATOR_RANK_VETERAN);
				break;
			case 5:
				text3 = PsStrings.Get(StringID.CREATOR_RANK_SUPERSTAR);
				break;
			case 6:
				text3 = PsStrings.Get(StringID.CREATER_RANK_CREATOR_OF);
				break;
			default:
				text3 = PsStrings.Get(StringID.CREATOR_RANK_UNRANKED);
				break;
			}
		}
		UIFittedText uifittedText = new UIFittedText(uicanvas3, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#84F22F", null);
		uifittedText.SetHorizontalAlign(0f);
		if (!this.m_user.developer)
		{
			PsUIStarProgressBar psUIStarProgressBar = new PsUIStarProgressBar(uicanvas2, creatorRank, 6, string.Empty);
			psUIStarProgressBar.SetHeight(0.55f, RelativeTo.ParentHeight);
			psUIStarProgressBar.SetWidth(6f, RelativeTo.OwnHeight);
			psUIStarProgressBar.SetAlign(0f, 0f);
		}
		UIVerticalList uiverticalList3 = new UIVerticalList(_parent, string.Empty);
		uiverticalList3.SetHorizontalAlign(1f);
		uiverticalList3.SetMargins(0f, 0.035f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList3.SetVerticalAlign(1f);
		uiverticalList3.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList3.RemoveDrawHandler();
		if (this.m_user.playerId == PlayerPrefsX.GetUserId())
		{
			this.m_fbButton = new PsUIGenericButton(uiverticalList3, 0.25f, 0.25f, 0.005f, "Button");
			this.m_fbButton.SetBlueColors(true);
			this.m_fbButton.SetIcon("menu_icon_facebook", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_fbButton.SetSpacing(0.02f, RelativeTo.ScreenHeight);
			Action action;
			if (PlayerPrefsX.GetFacebookId() == null)
			{
				UITextbox uitextbox = new UITextbox(this.m_fbButton, false, string.Empty, PsStrings.Get(StringID.FACEBOOK_CONNECT), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
				uitextbox.SetWidth(0.225f, RelativeTo.ScreenWidth);
				uitextbox.SetMaxRows(2);
				action = delegate
				{
					TouchAreaS.CancelAllTouches(null);
					this.m_waitingPopup = new PsUIBasePopup(typeof(PsUICenterProfilePopup.PsUIPopupFacebookWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
					FacebookManager.Login(new Action(this.FacebookDone));
				};
			}
			else
			{
				UITextbox uitextbox2 = new UITextbox(this.m_fbButton, false, string.Empty, PsStrings.Get(StringID.FACEBOOK_DISCONNECT), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
				uitextbox2.SetWidth(0.225f, RelativeTo.ScreenWidth);
				action = delegate
				{
					TouchAreaS.CancelAllTouches(null);
					this.m_waitingPopup = new PsUIBasePopup(typeof(PsUICenterProfilePopup.PsUIPopupFacebookWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
					FacebookManager.Logout(new Action(this.FacebookDone), true);
				};
				uitextbox2.SetMaxRows(2);
			}
			this.m_fbButton.SetReleaseAction(action);
			this.m_googlePlay = new PsUIGenericButton(uiverticalList3, 0.25f, 0.25f, 0.005f, "Button");
			this.m_googlePlay.SetBlueColors(true);
			this.m_googlePlay.SetIcon("menu_games_controller", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_googlePlay.SetSpacing(0.02f, RelativeTo.ScreenHeight);
			if (PlayerPrefsX.GetGameCenterId() == null)
			{
				UITextbox uitextbox3 = new UITextbox(this.m_googlePlay, false, string.Empty, "Google Play", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
				uitextbox3.SetWidth(0.225f, RelativeTo.ScreenWidth);
				uitextbox3.SetMaxRows(2);
			}
			else
			{
				UITextbox uitextbox4 = new UITextbox(this.m_googlePlay, false, string.Empty, "Google Play", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
				uitextbox4.SetWidth(0.225f, RelativeTo.ScreenWidth);
				uitextbox4.SetMaxRows(2);
			}
			this.m_googlePlay.SetReleaseAction(delegate
			{
				TouchAreaS.CancelAllTouches(null);
				PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterGooglePlay), null, null, null, true, true, InitialPage.Center, false, false, false);
				TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				popup.SetAction("Exit", delegate
				{
					popup.Destroy();
				});
			});
		}
		else
		{
			this.m_follow = new PsUIFollowButton(uiverticalList3, this.m_user, 0.25f, 0.25f, 0.005f, 0.175f, RelativeTo.ScreenHeight);
			this.m_report = new PsUIGenericButton(uiverticalList3, 0.25f, 0.25f, 0.005f, "Button");
			this.m_report.SetRedColors();
			UICanvas uicanvas4 = new UICanvas(this.m_report, false, string.Empty, null, string.Empty);
			uicanvas4.SetHeight(0.045f, RelativeTo.ScreenHeight);
			uicanvas4.SetWidth(0.245f, RelativeTo.ScreenHeight);
			uicanvas4.SetMargins(0.15f, RelativeTo.OwnHeight);
			uicanvas4.RemoveDrawHandler();
			UIFittedText uifittedText2 = new UIFittedText(uicanvas4, false, string.Empty, PsStrings.Get(StringID.BUTTON_REPORT), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
			this.m_report.SetReleaseAction(delegate
			{
				TouchAreaS.CancelAllTouches(null);
				PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUIReportProfile), null, null, null, false, true, InitialPage.Center, false, false, false);
				string text5 = PsStrings.Get(StringID.REPORT_POPUP_TEXT);
				text5 = text5.Replace("%1", "<color=#84F22F>" + this.m_name + "</color>");
				(popup.m_mainContent as PsUIReportProfile).m_textBox.SetText(text5);
				popup.SetAction("Continue", delegate
				{
					this.m_reportText = (popup.m_mainContent as PsUIReportProfile).m_input;
					this.SendReport(this.m_user.playerId, this.m_reportText);
					popup.Destroy();
				});
				popup.SetAction("Exit", delegate
				{
					popup.Destroy();
				});
				TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
			});
		}
		UICanvas uicanvas5 = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas5.SetHeight(0.085f, RelativeTo.ScreenHeight);
		uicanvas5.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas5.SetVerticalAlign(0f);
		uicanvas5.SetDrawHandler(new UIDrawDelegate(this.TeamAreaDrawhandler));
		uicanvas5.SetDepthOffset(-5f);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uicanvas5, string.Empty);
		uihorizontalList2.SetHeight(1f, RelativeTo.ParentHeight);
		uihorizontalList2.SetHorizontalAlign(0f);
		uihorizontalList2.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList2.RemoveDrawHandler();
		string text4 = this.m_user.teamName;
		if (string.IsNullOrEmpty(text4))
		{
			text4 = PsStrings.Get(StringID.SOCIAL_NOT_IN_TEAM);
		}
		UIText uitext = new UIText(uihorizontalList2, false, string.Empty, text4, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0335f, RelativeTo.ScreenHeight, "#FDFF47", null);
		if (!string.IsNullOrEmpty(this.m_user.teamId))
		{
			this.m_teamButton = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.005f, "Button");
			this.m_teamButton.SetOrangeColors(true);
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("hud_icon_back", null);
			frame.flipX = true;
			this.m_teamButton.SetIcon(frame, 0.055f, "#FFFFFF", default(cpBB));
			this.m_teamButton.SetMargins(0.01f, RelativeTo.ScreenHeight);
			this.m_teamButton.SetReleaseAction(delegate
			{
				TouchAreaS.CancelAllTouches(null);
				SoundS.PlaySingleShot("/UI/Popup", Vector3.zero, 1f);
				if (this.m_fromTeam)
				{
					this.GetRoot().Destroy();
				}
				else
				{
					PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterTeamPopup), null, null, null, false, true, InitialPage.Center, false, false, false);
					if (this.m_user.teamData == null)
					{
						(popup.m_mainContent as PsUICenterTeamPopup).GetTeam(this.m_user.teamId);
					}
					else
					{
						(popup.m_mainContent as PsUICenterTeamPopup).SetTeam(this.m_user.teamData);
					}
					popup.SetAction("Exit", delegate
					{
						popup.Destroy();
					});
					popup.SetAction("Join", delegate
					{
						popup.Destroy();
						PsMainMenuState.ChangeToTeamState();
					});
					popup.Update();
					TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				}
			});
		}
		UIHorizontalList uihorizontalList3 = new UIHorizontalList(uicanvas5, string.Empty);
		uihorizontalList3.SetHeight(1f, RelativeTo.ParentHeight);
		uihorizontalList3.SetHorizontalAlign(1f);
		uihorizontalList3.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList3.SetMargins(0f, 0.035f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList3.RemoveDrawHandler();
		if (!string.IsNullOrEmpty(PlayerPrefsX.GetTeamId()) && PlayerPrefsX.GetTeamRole() == TeamRole.Creator && this.m_user.teamId == PlayerPrefsX.GetTeamId() && this.m_user.playerId != PlayerPrefsX.GetUserId())
		{
			this.m_kick = new PsUIRoleButton(uihorizontalList3, this.m_user.playerId, this.m_user.teamId, this.m_user.teamRole);
			this.m_kick.SetRedColors();
			this.m_kick.SetText(PsStrings.Get(StringID.TEAM_BUTTON_KICK), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_kick.SetCustomCallback(new Action(this.KickDone));
		}
	}

	// Token: 0x060016C6 RID: 5830 RVA: 0x000F2820 File Offset: 0x000F0C20
	private void SendReport(string _userId, string _message)
	{
		Debug.LogError("Sending report!");
		HttpC httpC = Abuse.Report(_userId, _message, new Action<HttpC>(this.ReportOK), new Action<HttpC>(this.ReportFailed), null);
	}

	// Token: 0x060016C7 RID: 5831 RVA: 0x000F2858 File Offset: 0x000F0C58
	private void ReportOK(HttpC _httpc)
	{
		Debug.LogError("Report ok!");
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x000F2864 File Offset: 0x000F0C64
	private void ReportFailed(HttpC _httpc)
	{
		Debug.LogError("Report Failed");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _httpc.www, delegate
		{
			HttpC httpC = Abuse.Report(this.m_user.playerId, this.m_reportText, new Action<HttpC>(this.ReportOK), new Action<HttpC>(this.ReportFailed), null);
			httpC.objectData = _httpc.objectData;
			return httpC;
		}, null);
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x000F28B8 File Offset: 0x000F0CB8
	public void CreateUserName(UIComponent _parent)
	{
		this.m_name = this.m_user.name;
		if (this.m_user.playerId == PlayerPrefsX.GetUserId())
		{
			this.m_changeName = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
			this.m_changeName.SetBlueColors(true);
			UICanvas uicanvas = new UICanvas(this.m_changeName, false, string.Empty, null, string.Empty);
			uicanvas.SetWidth(0.3f, RelativeTo.ScreenWidth);
			uicanvas.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			this.m_userName = new UIFittedText(uicanvas, false, string.Empty, this.m_name + " <color=#97DAFF>@" + this.m_user.tag + "</color>", PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
			if (this.m_user.playerId == PlayerPrefsX.GetUserId() && PlayerPrefsX.GetNameChangesCount() < 2)
			{
				this.m_userName.SetMargins(0f, 0.06f, 0f, 0f, RelativeTo.ScreenHeight);
				UICanvas uicanvas2 = new UICanvas(this.m_userName, false, string.Empty, null, string.Empty);
				uicanvas2.SetHorizontalAlign(1f);
				uicanvas2.SetSize(0.045f, 0.06f, RelativeTo.ScreenHeight);
				uicanvas2.SetMargins(0.06f, -0.06f, 0f, 0f, RelativeTo.ScreenHeight);
				uicanvas2.RemoveDrawHandler();
				UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_textinput_icon", null), true, true);
				uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
			}
			else
			{
				this.m_changeName.RemoveTouchAreas();
			}
			this.m_changeName.SetReleaseAction(delegate
			{
				TouchAreaS.CancelAllTouches(null);
				PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUIChangeNameConfirmationPopup), null, null, null, false, true, InitialPage.Center, false, true, false);
				TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				popup.SetAction("Cancel", new Action(popup.Destroy));
				popup.SetAction("Ok", delegate
				{
					popup.Destroy();
					this.m_input = new UITextInput(PsStrings.Get(StringID.TEXT_ENTER_NEW_NAME), new Action<string>(this.InputDone), new Action(this.InputCancelled), null, 1, true, 128);
					this.m_input.SetMinCharacterCount(3);
					this.m_input.SetMaxCharacterCount(16);
					this.m_input.SetValue(this.m_name);
					this.m_input.m_textbox.SetColor("464646", null);
					this.m_input.SetText(this.m_name);
					this.m_input.Update();
				});
			});
		}
		else
		{
			UICanvas uicanvas3 = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			uicanvas3.SetWidth(0.3f, RelativeTo.ScreenWidth);
			uicanvas3.SetHeight(0.075f, RelativeTo.ScreenHeight);
			uicanvas3.SetMargins(0.02f, RelativeTo.ScreenHeight);
			uicanvas3.SetHorizontalAlign(0f);
			uicanvas3.RemoveDrawHandler();
			this.m_userName = new UIFittedText(uicanvas3, false, string.Empty, this.m_name + " <color=#97DAFF>@" + this.m_user.tag + "</color>", PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
			this.m_userName.SetHorizontalAlign(0f);
		}
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x000F2B1C File Offset: 0x000F0F1C
	public void YoutubeDone()
	{
		int num = PsMetagameManager.m_playerStats.youtubeSubscriberCount;
		string text = PlayerPrefsX.GetYoutubeName();
		if (string.IsNullOrEmpty(text))
		{
			num = -1;
			string text2 = PsStrings.Get(StringID.LINK_HEADER);
			text2 = text2.Replace("%1", "YouTube");
			text = text2;
		}
		this.m_youtubeUser.m_youtubeUsername = text;
		this.m_youtubeUser.m_subscribers = num;
		this.m_youtubeUser.Update();
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x000F2B88 File Offset: 0x000F0F88
	public void KickDone()
	{
		(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x000F2BA0 File Offset: 0x000F0FA0
	public void InputDone(string _input)
	{
		if (_input != PlayerPrefsX.GetUserName())
		{
			this.m_name = _input;
			PlayerPrefsX.SetUserName(this.m_name);
			this.m_userName.SetText(this.m_name);
			this.m_changeName.m_childs[0].DestroyChildren(1);
			this.m_changeName.RemoveTouchAreas();
			this.m_changeName.Update();
			this.ChangeName(this.m_name);
		}
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x000F2C19 File Offset: 0x000F1019
	public void InputCancelled()
	{
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x000F2C1C File Offset: 0x000F101C
	private void FacebookDone()
	{
		Debug.Log("Facebook call done", null);
		if (string.IsNullOrEmpty(PlayerPrefsX.GetFacebookId()))
		{
			(this.m_fbButton.m_childs[1] as UITextbox).SetText(PsStrings.Get(StringID.FACEBOOK_CONNECT));
			this.m_fbButton.SetReleaseAction(delegate
			{
				TouchAreaS.CancelAllTouches(null);
				this.m_waitingPopup = new PsUIBasePopup(typeof(PsUICenterProfilePopup.PsUIPopupFacebookWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
				FacebookManager.Login(new Action(this.FacebookDone));
			});
		}
		else
		{
			(this.m_fbButton.m_childs[1] as UITextbox).SetText(PsStrings.Get(StringID.FACEBOOK_DISCONNECT));
			this.m_fbButton.SetReleaseAction(delegate
			{
				TouchAreaS.CancelAllTouches(null);
				this.m_waitingPopup = new PsUIBasePopup(typeof(PsUICenterProfilePopup.PsUIPopupFacebookWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
				FacebookManager.Logout(new Action(this.FacebookDone), true);
			});
			PsMetagameManager.GetFriends(null, true);
		}
		this.m_profileImage.m_facebookId = PlayerPrefsX.GetFacebookId();
		this.m_profileImage.LoadPicture();
		this.m_profileImage.Update();
		this.m_fbButton.Update();
		if (this.m_waitingPopup != null)
		{
			this.m_waitingPopup.Destroy();
			this.m_waitingPopup = null;
		}
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x000F2D15 File Offset: 0x000F1115
	private void UpdateGooglePlay()
	{
		this.m_googlePlay.Update();
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x000F2D24 File Offset: 0x000F1124
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		if (this.loading != null)
		{
			this.loading.Destroy();
			this.loading = null;
		}
		UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(_parent, string.Empty);
		uiscrollableCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uiscrollableCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uiscrollableCanvas.RemoveDrawHandler();
		uiscrollableCanvas.m_passTouchesToScrollableParents = true;
		this.m_contentList = new UIVerticalList(uiscrollableCanvas, "content");
		this.m_contentList.SetMargins(0.03f, 0.03f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		this.m_contentList.SetVerticalAlign(1f);
		this.m_contentList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		this.m_contentList.RemoveDrawHandler();
		if (this.m_user.playerId == PlayerPrefsX.GetUserId())
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_contentList, string.Empty);
			uihorizontalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, string.Empty);
			uiverticalList.SetWidth(0.4f, RelativeTo.ScreenWidth);
			uiverticalList.SetHorizontalAlign(0f);
			uiverticalList.SetVerticalAlign(1f);
			uiverticalList.SetSpacing(0.035f, RelativeTo.ScreenHeight);
			uiverticalList.RemoveDrawHandler();
			UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas.SetHeight(0.065f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			uicanvas.SetHorizontalAlign(0f);
			UITextbox uitextbox = new UITextbox(uicanvas, false, string.Empty, PsStrings.Get(StringID.MUSIC), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "97DAFF", true, null);
			uitextbox.SetWidth(0.65f, RelativeTo.ParentWidth);
			uitextbox.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
			uitextbox.SetHorizontalAlign(0f);
			UICanvas uicanvas2 = new UICanvas(uitextbox, false, string.Empty, null, string.Empty);
			uicanvas2.SetSize(0.035f, 0.035f, RelativeTo.ScreenHeight);
			uicanvas2.SetHorizontalAlign(0f);
			uicanvas2.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas2.RemoveDrawHandler();
			UISprite uisprite = new UISprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_music", null), true);
			uisprite.SetColor(DebugDraw.HexToColor("#97DAFF") * Color.gray);
			uisprite.SetSize(0.035f, 0.035f, RelativeTo.ScreenHeight);
			this.m_musicSwitch = new PsUISwitch(uicanvas, !PsState.m_muteMusic, 0.03f, "On", "Off", 0.05f, null);
			this.m_musicSwitch.SetHorizontalAlign(1f);
			UICanvas uicanvas3 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas3.SetHeight(0.065f, RelativeTo.ScreenHeight);
			uicanvas3.RemoveDrawHandler();
			uicanvas3.SetHorizontalAlign(0f);
			UITextbox uitextbox2 = new UITextbox(uicanvas3, false, string.Empty, PsStrings.Get(StringID.SOUNDS), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "97DAFF", true, null);
			uitextbox2.SetWidth(0.65f, RelativeTo.ParentWidth);
			uitextbox2.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
			uitextbox2.SetHorizontalAlign(0f);
			UICanvas uicanvas4 = new UICanvas(uitextbox2, false, string.Empty, null, string.Empty);
			uicanvas4.SetSize(0.035f, 0.035f, RelativeTo.ScreenHeight);
			uicanvas4.SetHorizontalAlign(0f);
			uicanvas4.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas4.RemoveDrawHandler();
			UISprite uisprite2 = new UISprite(uicanvas4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_sounds", null), true);
			uisprite2.SetColor(DebugDraw.HexToColor("#97DAFF") * Color.gray);
			uisprite2.SetSize(0.035f, 0.035f, RelativeTo.ScreenHeight);
			this.m_soundsSwitch = new PsUISwitch(uicanvas3, !PsState.m_muteSoundFX, 0.03f, "On", "Off", 0.05f, null);
			this.m_soundsSwitch.SetHorizontalAlign(1f);
			UICanvas uicanvas5 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas5.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas5.SetHeight(0.065f, RelativeTo.ScreenHeight);
			uicanvas5.RemoveDrawHandler();
			uicanvas5.SetHorizontalAlign(0f);
			UITextbox uitextbox3 = new UITextbox(uicanvas5, false, string.Empty, PsStrings.Get(StringID.SETTINGS_BUTTON_NOTIFICATIONS), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "97DAFF", true, null);
			uitextbox3.SetWidth(0.675f, RelativeTo.ParentWidth);
			uitextbox3.SetMargins(0f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uitextbox3.SetHorizontalAlign(0f);
			this.m_notificationSwitch = new PsUISwitch(uicanvas5, PsState.m_notifications, 0.03f, "On", "Off", 0.05f, null);
			this.m_notificationSwitch.SetHorizontalAlign(1f);
			UICanvas uicanvas6 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas6.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas6.SetHeight(0.065f, RelativeTo.ScreenHeight);
			uicanvas6.RemoveDrawHandler();
			uicanvas6.SetHorizontalAlign(0f);
			UITextbox uitextbox4 = new UITextbox(uicanvas6, false, string.Empty, PsStrings.Get(StringID.DRAWING_MODE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "97DAFF", true, null);
			uitextbox4.SetWidth(0.61f, RelativeTo.ParentWidth);
			uitextbox4.SetMargins(0f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uitextbox4.SetHorizontalAlign(0f);
			this.m_handedSwitch = new PsUISwitch(uicanvas6, PsState.m_editorIsLefty, 0.03f, "On", "Off", 0.05f, null);
			this.m_handedSwitch.SetHorizontalAlign(1f);
			UICanvas uicanvas7 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas7.SetWidth(0.4f, RelativeTo.ScreenWidth);
			uicanvas7.SetHeight(0.065f, RelativeTo.ScreenHeight);
			uicanvas7.RemoveDrawHandler();
			uicanvas7.SetHorizontalAlign(0f);
			uicanvas7.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenWidth);
			UITextbox uitextbox5 = new UITextbox(uicanvas7, false, string.Empty, PsStrings.Get(StringID.PERF_MODE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "97DAFF", true, null);
			uitextbox5.SetWidth(0.61f, RelativeTo.ParentWidth);
			uitextbox5.SetMargins(0f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uitextbox5.SetHorizontalAlign(0f);
			this.m_perfSwitch = new PsUISwitch(uicanvas7, PsState.m_perfMode, 0.03f, "On", "Off", 0.05f, null);
			this.m_perfSwitch.SetHorizontalAlign(1f);
			UICanvas uicanvas8 = new UICanvas(uicanvas7, false, string.Empty, null, string.Empty);
			uicanvas8.SetHeight(0.085f, RelativeTo.ScreenHeight);
			uicanvas8.SetWidth(0.4f, RelativeTo.ScreenWidth);
			uicanvas8.SetHorizontalAlign(1f);
			uicanvas8.SetMargins(0.425f, -0.425f, 0f, 0f, RelativeTo.ScreenWidth);
			uicanvas8.RemoveDrawHandler();
			UITextbox uitextbox6 = new UITextbox(uicanvas8, false, string.Empty, PsStrings.Get(StringID.SETTINGS_LOWEND_INFO), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "97DAFF", true, null);
			uitextbox6.SetMargins(0.03f, 0.03f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			uitextbox6.SetWidth(1f, RelativeTo.ParentWidth);
			uitextbox6.SetDrawHandler(new UIDrawDelegate(this.PerformanceDrawhandler));
			UICanvas uicanvas9 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas9.SetWidth(0.4f, RelativeTo.ScreenWidth);
			uicanvas9.SetHeight(0.065f, RelativeTo.ScreenHeight);
			uicanvas9.RemoveDrawHandler();
			uicanvas9.SetHorizontalAlign(0f);
			uicanvas9.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenWidth);
			UITextbox uitextbox7 = new UITextbox(uicanvas9, false, string.Empty, "GIF", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "97DAFF", true, null);
			uitextbox7.SetWidth(0.61f, RelativeTo.ParentWidth);
			uitextbox7.SetMargins(0f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uitextbox7.SetHorizontalAlign(0f);
			this.m_gifDeathRecording = new PsUISwitch(uicanvas9, PlayerPrefsX.GetGifDeathRecording(), 0.03f, "On", "Off", 0.05f, null);
			this.m_gifDeathRecording.SetHorizontalAlign(1f);
			UIVerticalList uiverticalList2 = new UIVerticalList(uihorizontalList, string.Empty);
			uiverticalList2.SetWidth(0.4f, RelativeTo.ScreenWidth);
			uiverticalList2.SetHorizontalAlign(1f);
			uiverticalList2.SetVerticalAlign(1f);
			uiverticalList2.SetSpacing(0.035f, RelativeTo.ScreenHeight);
			uiverticalList2.RemoveDrawHandler();
			new PsUILoveUs(uiverticalList2);
			UICanvas uicanvas10 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
			uicanvas10.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas10.SetHeight(0.065f, RelativeTo.ScreenHeight);
			uicanvas10.RemoveDrawHandler();
			this.m_language = new PsUIGenericButton(uicanvas10, 0.25f, 0.25f, 0.005f, "Button");
			this.m_language.SetIcon("menu_icon_language", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_language.SetText(PsStrings.Get(StringID.BUTTON_LANGUAGE), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_language.SetHeight(0.08f, RelativeTo.ScreenHeight);
			UICanvas uicanvas11 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
			uicanvas11.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas11.SetHeight(0.065f, RelativeTo.ScreenHeight);
			uicanvas11.RemoveDrawHandler();
			this.m_support = new PsUIGenericButton(uicanvas11, 0.25f, 0.25f, 0.005f, "Button");
			this.m_support.SetSpacing(0.025f, RelativeTo.ScreenHeight);
			UIText uitext = new UIText(this.m_support, false, string.Empty, "?", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, null, null);
			this.m_support.SetText(PsStrings.Get(StringID.SUPPORT), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_support.SetHeight(0.08f, RelativeTo.ScreenHeight);
		}
		this.CreateStatsContent(this.m_contentList);
		if (!string.IsNullOrEmpty(this.m_user.teamId))
		{
			this.CreateTeamContent(this.m_contentList);
		}
		UITextbox uitextbox8 = new UITextbox(this.m_contentList, false, string.Empty, PsStrings.Get(StringID.SOCIAL_BIG_BANG_POINTS_INFO), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox8.SetWidth(0.575f, RelativeTo.ScreenHeight);
		uitextbox8.SetMargins(0.035f, 0.035f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		uitextbox8.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
		UICanvas uicanvas12 = new UICanvas(uitextbox8, false, string.Empty, null, string.Empty);
		uicanvas12.SetRogue();
		uicanvas12.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
		uicanvas12.SetAlign(0f, 1f);
		uicanvas12.SetMargins(-0.055f, 0.055f, -0.04f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas12.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas12, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		PsUILinkUrl psUILinkUrl = new PsUILinkUrl(this.m_contentList, true, string.Empty, "Privacy Notice", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "38a6ea", true, null);
		psUILinkUrl.m_url = "http://www.traplightgames.com/privacy-notice/";
		PsUILinkUrl psUILinkUrl2 = new PsUILinkUrl(this.m_contentList, true, string.Empty, "Terms of Service", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "38a6ea", true, null);
		psUILinkUrl2.m_url = "http://www.traplightgames.com/terms-of-service/";
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x000F3950 File Offset: 0x000F1D50
	public virtual void CreateTeamContent(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.3f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.SetDrawHandler(new UIDrawDelegate(this.SettingsDrawhandler));
		uicanvas.SetMargins(0.03f, 0.03f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList = new UIVerticalList(uicanvas, "middle");
		uiverticalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(0.375f, RelativeTo.ScreenWidth);
		uiverticalList.SetHorizontalAlign(0f);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.RemoveDrawHandler();
		UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas3.SetWidth(0.5f, RelativeTo.ParentWidth);
		uicanvas3.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas3.SetHorizontalAlign(0f);
		uicanvas3.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas3, false, string.Empty, " " + PsStrings.Get(StringID.TEAM_CURRENT_SEASON_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFB81D", null);
		uifittedText.SetAlign(0f, 0.5f);
		UICanvas uicanvas4 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas4.SetHorizontalAlign(0f);
		uicanvas4.SetMargins(0f, 0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas4, false, string.Empty, " " + PsStrings.Get(StringID.STAT_TROPHY_CHANGE), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#64C4E6", null);
		uifittedText2.SetHorizontalAlign(0f);
		UICanvas uicanvas5 = new UICanvas(uicanvas4, false, string.Empty, null, string.Empty);
		uicanvas5.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas5.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas5.SetHorizontalAlign(1f);
		uicanvas5.SetMargins(0.16f, -0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas5.RemoveDrawHandler();
		int num = this.m_user.mcTrophies + this.m_user.carTrophies - (Mathf.Min(this.m_user.lastSeasonEndMcTrophies, 3000) + Mathf.Min(this.m_user.lastSeasonEndCarTrophies, 3000));
		int num2 = 1;
		if (num < 0)
		{
			num2 = -1;
		}
		UIFittedText uifittedText3 = new UIFittedText(uicanvas5, false, string.Empty, (num == 0) ? "=" : Mathf.Abs(num).ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText3.SetHorizontalAlign(1f);
		UICanvas uicanvas6 = new UICanvas(uifittedText3, false, string.Empty, null, string.Empty);
		uicanvas6.SetSize(0.03f, 0.03f, RelativeTo.ScreenHeight);
		uicanvas6.SetHorizontalAlign(0f);
		uicanvas6.SetMargins(-0.0325f, 0.0325f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas6.RemoveDrawHandler();
		if (num != 0)
		{
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas6, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_triangle", null), true, true);
			if (num > 0)
			{
				uifittedSprite.SetColor(DebugDraw.HexToColor("#00ff00"));
			}
			else
			{
				uifittedSprite.SetColor(DebugDraw.HexToColor("#EA0000"));
			}
			uifittedSprite.SetHeight(0.025f, RelativeTo.ScreenHeight);
			TransformS.SetRotation(uifittedSprite.m_TC, new Vector3(0f, 0f, (float)(90 * num2)));
			uifittedSprite.SetHorizontalAlign(1f);
			uifittedSprite.SetVerticalAlign(1f);
		}
		UICanvas uicanvas7 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas7.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas7.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas7.SetHorizontalAlign(0f);
		uicanvas7.SetMargins(0f, 0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas7.RemoveDrawHandler();
		UIFittedText uifittedText4 = new UIFittedText(uicanvas7, false, string.Empty, " " + PsStrings.Get(StringID.STAT_RACES_PLAYED), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#64C4E6", null);
		uifittedText4.SetHorizontalAlign(0f);
		UICanvas uicanvas8 = new UICanvas(uicanvas7, false, string.Empty, null, string.Empty);
		uicanvas8.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas8.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas8.SetHorizontalAlign(1f);
		uicanvas8.SetMargins(0.13f, -0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas8.RemoveDrawHandler();
		UIFittedText uifittedText5 = new UIFittedText(uicanvas8, false, string.Empty, this.m_user.racesThisSeason.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText5.SetHorizontalAlign(1f);
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x000F3E70 File Offset: 0x000F2270
	public virtual void CreateStatsContent(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.3f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.SetDrawHandler(new UIDrawDelegate(this.SettingsDrawhandler));
		uicanvas.SetMargins(0.03f, 0.03f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList = new UIVerticalList(uicanvas, "middle");
		uiverticalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(0.45f, RelativeTo.ParentWidth);
		uiverticalList.SetHorizontalAlign(0f);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.RemoveDrawHandler();
		UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas3.SetWidth(0.5f, RelativeTo.ParentWidth);
		uicanvas3.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas3.SetHorizontalAlign(0f);
		uicanvas3.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas3, false, string.Empty, " " + PsStrings.Get(StringID.SOCIAL_TOTAL_TROPHIES), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFB61C", null);
		uifittedText.SetAlign(0f, 0.5f);
		int num = this.m_user.mcTrophies + this.m_user.carTrophies;
		UICanvas uicanvas4 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas4.SetWidth(0.45f, RelativeTo.ParentWidth);
		uicanvas4.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas4.SetHorizontalAlign(1f);
		uicanvas4.SetMargins(0.05f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas4, false, string.Empty, num.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText2.SetAlign(1f, 0.5f);
		UICanvas uicanvas5 = new UICanvas(uifittedText2, false, string.Empty, null, string.Empty);
		uicanvas5.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas5.SetHorizontalAlign(0f);
		uicanvas5.SetMargins(-0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas5.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas5, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_small_full", null), true, true);
		uifittedSprite.SetHeight(0.04f, RelativeTo.ScreenHeight);
		UICanvas uicanvas6 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas6.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas6.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas6.SetHorizontalAlign(0f);
		uicanvas6.SetMargins(0f, 0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas6.RemoveDrawHandler();
		UIFittedText uifittedText3 = new UIFittedText(uicanvas6, false, string.Empty, " " + PsStrings.Get(StringID.SOCIAL_LEVELS_MADE), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#64C4E6", null);
		uifittedText3.SetHorizontalAlign(0f);
		UICanvas uicanvas7 = new UICanvas(uicanvas6, false, string.Empty, null, string.Empty);
		uicanvas7.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas7.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas7.SetHorizontalAlign(1f);
		uicanvas7.SetMargins(0.13f, -0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas7.RemoveDrawHandler();
		UIFittedText uifittedText4 = new UIFittedText(uicanvas7, false, string.Empty, this.m_user.publishedMinigameCount.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText4.SetHorizontalAlign(1f);
		if (this.m_user.publishedMinigameCount > 0 && this.m_user.playerId != PlayerPrefsX.GetUserId())
		{
			UICanvas uicanvas8 = new UICanvas(uicanvas7, false, string.Empty, null, string.Empty);
			uicanvas8.SetMargins(1f, -1f, 0f, 0f, RelativeTo.OwnWidth);
			uicanvas8.RemoveDrawHandler();
			PsUIGenericButton psUIGenericButton = new PsUIGenericButton(uicanvas8, 0.25f, 0.25f, 0.005f, "Button");
			psUIGenericButton.SetOrangeColors(true);
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("hud_icon_back", null);
			frame.flipX = true;
			psUIGenericButton.SetIcon(frame, 0.03f, "#FFFFFF", default(cpBB));
			psUIGenericButton.SetMargins(0.005f, RelativeTo.ScreenHeight);
			psUIGenericButton.SetTouchAreaSizeMultipler(2f);
			psUIGenericButton.SetReleaseAction(delegate
			{
				IState state = new PsTransitionSearchState();
				PsMenuScene.m_lastIState = state;
				PsMenuScene.m_lastState = null;
				PsUICenterSearch.m_sharedUser = this.m_playerId;
				PsUITabbedCreate.m_selectedTab = 3;
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLevelEndLoadingScene(Color.black, PsState.m_activeGameLoop, 0.25f));
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
				TouchAreaS.CancelAllTouches(null);
				SoundS.PlaySingleShot("/UI/Popup", Vector3.zero, 1f);
			});
		}
		UICanvas uicanvas9 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas9.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas9.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas9.SetHorizontalAlign(0f);
		uicanvas9.SetMargins(0f, 0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas9.RemoveDrawHandler();
		UIFittedText uifittedText5 = new UIFittedText(uicanvas9, false, string.Empty, " " + PsStrings.Get(StringID.SOCIAL_FOLLOWERS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#64C4E6", null);
		uifittedText5.SetHorizontalAlign(0f);
		UICanvas uicanvas10 = new UICanvas(uicanvas9, false, string.Empty, null, string.Empty);
		uicanvas10.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas10.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas10.SetHorizontalAlign(1f);
		uicanvas10.SetMargins(0.13f, -0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas10.RemoveDrawHandler();
		UIFittedText uifittedText6 = new UIFittedText(uicanvas10, false, string.Empty, this.m_user.followerCount.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText6.SetHorizontalAlign(1f);
		UICanvas uicanvas11 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas11.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas11.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas11.SetHorizontalAlign(0f);
		uicanvas11.SetMargins(0f, 0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas11.RemoveDrawHandler();
		UIFittedText uifittedText7 = new UIFittedText(uicanvas11, false, string.Empty, " " + PsStrings.Get(StringID.EDITOR_TEXT_TOTAL_LIKES), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#64C4E6", null);
		uifittedText7.SetHorizontalAlign(0f);
		UICanvas uicanvas12 = new UICanvas(uicanvas11, false, string.Empty, null, string.Empty);
		uicanvas12.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas12.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas12.SetHorizontalAlign(1f);
		uicanvas12.SetMargins(0.13f, -0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas12.RemoveDrawHandler();
		UIFittedText uifittedText8 = new UIFittedText(uicanvas12, false, string.Empty, (this.m_user.totalLikes + this.m_user.totalSuperLikes * PsState.m_superLikeVisualMultiplier).ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText8.SetHorizontalAlign(1f);
		UICanvas uicanvas13 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas13.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas13.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas13.SetHorizontalAlign(0f);
		uicanvas13.SetMargins(0f, 0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas13.RemoveDrawHandler();
		UIFittedText uifittedText9 = new UIFittedText(uicanvas13, false, string.Empty, " " + PsStrings.Get(StringID.SOCIAL_MEGALIKES_EARNED), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#64C4E6", null);
		uifittedText9.SetHorizontalAlign(0f);
		UICanvas uicanvas14 = new UICanvas(uicanvas13, false, string.Empty, null, string.Empty);
		uicanvas14.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas14.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas14.SetHorizontalAlign(1f);
		uicanvas14.SetMargins(0.13f, -0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas14.RemoveDrawHandler();
		UIFittedText uifittedText10 = new UIFittedText(uicanvas14, false, string.Empty, this.m_user.totalSuperLikes.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText10.SetHorizontalAlign(1f);
		UIVerticalList uiverticalList2 = new UIVerticalList(uicanvas, "right");
		uiverticalList2.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList2.SetHorizontalAlign(1f);
		uiverticalList2.SetVerticalAlign(1f);
		uiverticalList2.SetWidth(0.45f, RelativeTo.ParentWidth);
		uiverticalList2.RemoveDrawHandler();
		UICanvas uicanvas15 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas15.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas15.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas15.RemoveDrawHandler();
		UICanvas uicanvas16 = new UICanvas(uicanvas15, false, string.Empty, null, string.Empty);
		uicanvas16.SetWidth(0.5f, RelativeTo.ParentWidth);
		uicanvas16.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas16.SetHorizontalAlign(0f);
		uicanvas16.RemoveDrawHandler();
		UIFittedText uifittedText11 = new UIFittedText(uicanvas16, false, string.Empty, " " + PsStrings.Get(StringID.SOCIAL_BIG_BANG_POINTS_EARNED), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFB81D", null);
		uifittedText11.SetAlign(0f, 0.5f);
		UICanvas uicanvas17 = new UICanvas(uicanvas15, false, string.Empty, null, string.Empty);
		uicanvas17.SetWidth(0.45f, RelativeTo.ParentWidth);
		uicanvas17.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas17.SetHorizontalAlign(1f);
		uicanvas17.SetMargins(0.05f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas17.RemoveDrawHandler();
		UIFittedText uifittedText12 = new UIFittedText(uicanvas17, false, string.Empty, this.m_user.bigBangPoints.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText12.SetAlign(1f, 0.5f);
		UICanvas uicanvas18 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas18.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas18.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas18.SetHorizontalAlign(0f);
		uicanvas18.SetMargins(0f, 0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas18.RemoveDrawHandler();
		UIFittedText uifittedText13 = new UIFittedText(uicanvas18, false, string.Empty, " " + PsStrings.Get(StringID.SOCIAL_ADVENTURE_LEVELS_COMPL), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#64C4E6", null);
		uifittedText13.SetHorizontalAlign(0f);
		UICanvas uicanvas19 = new UICanvas(uicanvas18, false, string.Empty, null, string.Empty);
		uicanvas19.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas19.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas19.SetHorizontalAlign(1f);
		uicanvas19.SetMargins(0.13f, -0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas19.RemoveDrawHandler();
		UIFittedText uifittedText14 = new UIFittedText(uicanvas19, false, string.Empty, this.m_user.adventureLevelsCompleted.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText14.SetHorizontalAlign(1f);
		UICanvas uicanvas20 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas20.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas20.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas20.SetHorizontalAlign(0f);
		uicanvas20.SetMargins(0f, 0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas20.RemoveDrawHandler();
		UIFittedText uifittedText15 = new UIFittedText(uicanvas20, false, string.Empty, " " + PsStrings.Get(StringID.SOCIAL_RACES_WON), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#64C4E6", null);
		uifittedText15.SetHorizontalAlign(0f);
		UICanvas uicanvas21 = new UICanvas(uicanvas20, false, string.Empty, null, string.Empty);
		uicanvas21.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas21.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas21.SetHorizontalAlign(1f);
		uicanvas21.SetMargins(0.13f, -0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas21.RemoveDrawHandler();
		UIFittedText uifittedText16 = new UIFittedText(uicanvas21, false, string.Empty, this.m_user.racesWon.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText16.SetHorizontalAlign(1f);
		UICanvas uicanvas22 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas22.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas22.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas22.SetHorizontalAlign(0f);
		uicanvas22.SetMargins(0f, 0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas22.RemoveDrawHandler();
		UIFittedText uifittedText17 = new UIFittedText(uicanvas22, false, string.Empty, " " + PsStrings.Get(StringID.SOCIAL_GORB_LEVELS_RATED), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#64C4E6", null);
		uifittedText17.SetHorizontalAlign(0f);
		UICanvas uicanvas23 = new UICanvas(uicanvas22, false, string.Empty, null, string.Empty);
		uicanvas23.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas23.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas23.SetHorizontalAlign(1f);
		uicanvas23.SetMargins(0.13f, -0.13f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas23.RemoveDrawHandler();
		UIFittedText uifittedText18 = new UIFittedText(uicanvas23, false, string.Empty, this.m_user.newLevelsRated.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText18.SetHorizontalAlign(1f);
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x000F4BEC File Offset: 0x000F2FEC
	public virtual void CreateStreamerSettings(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.3f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.SetDrawHandler(new UIDrawDelegate(this.SettingsDrawhandler));
		uicanvas.SetMargins(0.03f, 0.03f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList = new UIVerticalList(uicanvas, "middle");
		uiverticalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(0.375f, RelativeTo.ScreenWidth);
		uiverticalList.SetHorizontalAlign(0f);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, "Tuber/Streamer settings", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, "#FFB61C", null);
		uitext.SetAlign(0f, 0.5f);
		UICanvas uicanvas2 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetHeight(0.065f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		uicanvas2.SetHorizontalAlign(0f);
		UITextbox uitextbox = new UITextbox(uicanvas2, false, string.Empty, "Streamer HUD", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "97DAFF", true, null);
		uitextbox.SetWidth(0.65f, RelativeTo.ParentWidth);
		uitextbox.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uitextbox.SetHorizontalAlign(0f);
		UICanvas uicanvas3 = new UICanvas(uitextbox, false, string.Empty, null, string.Empty);
		uicanvas3.SetSize(0.035f, 0.035f, RelativeTo.ScreenHeight);
		uicanvas3.SetHorizontalAlign(0f);
		uicanvas3.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas3.RemoveDrawHandler();
		UISprite uisprite = new UISprite(uicanvas3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_music", null), true);
		uisprite.SetColor(DebugDraw.HexToColor("#97DAFF") * Color.gray);
		uisprite.SetSize(0.035f, 0.035f, RelativeTo.ScreenHeight);
		this.m_streamerHUDSwitch = new PsUISwitch(uicanvas2, PsState.m_streamerHUD, 0.03f, "On", "Off", 0.05f, null);
		this.m_streamerHUDSwitch.SetHorizontalAlign(1f);
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x000F4E50 File Offset: 0x000F3250
	private void PerformanceDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.0075f * (float)Screen.height, 8, Vector2.zero);
		List<Vector2> list = new List<Vector2>(roundedRect);
		list.Insert(16, new Vector2(_c.m_actualWidth * -0.501f, _c.m_actualHeight * 0.1f));
		list.Insert(16, new Vector2(_c.m_actualWidth * -0.5f - 0.0265f * (float)Screen.height, 0f));
		list.Insert(16, new Vector2(_c.m_actualWidth * -0.501f, _c.m_actualHeight * -0.1f));
		GGData ggdata = new GGData(list.ToArray());
		Color color = DebugDraw.HexToColor("#1B435E");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, list.ToArray(), 0.0085f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x000F4F88 File Offset: 0x000F3388
	private void SettingsDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth * 1f, _c.m_actualHeight, Vector2.zero);
		GGData ggdata = new GGData(rect);
		Color color = DebugDraw.HexToColor("#133552");
		Color color2 = DebugDraw.HexToColor("#1A415C");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 10f, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 9f, rect, 0.005f * (float)Screen.height, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x000F504C File Offset: 0x000F344C
	private void TeamAreaDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth + 0.08f * (float)Screen.height, _c.m_actualHeight, Vector2.zero);
		GGData ggdata = new GGData(rect);
		Color color = DebugDraw.HexToColor("#133552");
		Color color2 = DebugDraw.HexToColor("#1A415C");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 10f, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 9f, rect, 0.005f * (float)Screen.height, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x000F5118 File Offset: 0x000F3518
	private void ChangeName(string _name)
	{
		this.m_waitingPopup = new PsUIBasePopup(typeof(PsUICenterProfilePopup.PsUIPopupNameWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
		new PsServerQueueFlow(null, delegate
		{
			PsServerRequest.ServerChangeName(_name, new Action<HttpC>(this.NameChangeSUCCEED), new Action<HttpC>(this.NameChangeFAILED), null);
		}, null);
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x000F516C File Offset: 0x000F356C
	private void NameChangeSUCCEED(HttpC _c)
	{
		Debug.Log("NAME CHANGE SUCCEED", null);
		PsAchievementManager.Complete("uniqueSnowflake");
		PlayerPrefsX.SetNameChangesCount(PlayerPrefsX.GetNameChangesCount() + 1);
		this.m_waitingPopup.Destroy();
		this.m_waitingPopup = null;
		this.m_userName.SetText(PlayerPrefsX.GetUserName());
		this.m_userName.Update();
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x000F51C8 File Offset: 0x000F35C8
	private void NameChangeFAILED(HttpC _c)
	{
		Debug.Log("NAME CHANGE FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, () => PsServerRequest.ServerChangeName(PlayerPrefsX.GetUserName(), new Action<HttpC>(this.NameChangeSUCCEED), new Action<HttpC>(this.NameChangeFAILED), null), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x000F5214 File Offset: 0x000F3614
	public override void Step()
	{
		if (this.m_resetLocalUser == null || !this.m_resetLocalUser.m_hit)
		{
			if (this.m_musicSwitch != null && this.m_musicSwitch.m_hit)
			{
				PsState.m_muteMusic = !this.m_musicSwitch.m_enabled;
				PlayerPrefsX.SetMuteMusic(PsState.m_muteMusic);
				FrbMetrics.TrackMusicSetting();
				(Main.m_currentGame.m_currentScene as PsMenuScene).UpdateMusic();
			}
			else if (this.m_soundsSwitch != null && this.m_soundsSwitch.m_hit)
			{
				PsState.m_muteSoundFX = !this.m_soundsSwitch.m_enabled;
				PlayerPrefsX.SetMuteSoundFX(PsState.m_muteSoundFX);
				FrbMetrics.TrackSoundSetting();
				SoundS.m_canPlaySounds = !PsState.m_muteSoundFX;
			}
			else if (this.m_handedSwitch != null && this.m_handedSwitch.m_hit)
			{
				PsState.m_editorIsLefty = this.m_handedSwitch.m_enabled;
				PlayerPrefsX.SetLefty(PsState.m_editorIsLefty);
			}
			else if (this.m_perfSwitch != null && this.m_perfSwitch.m_hit)
			{
				PsState.m_perfMode = this.m_perfSwitch.m_enabled;
				PlayerPrefsX.SetPerfMode(PsState.m_perfMode);
				FrbMetrics.TrackLowEndSetting();
				Main.SetPerfMode(PsState.m_perfMode);
			}
			else if (this.m_gifDeathRecording != null && this.m_gifDeathRecording.m_hit)
			{
				PlayerPrefsX.SetGifDeathRecording(!PlayerPrefsX.GetGifDeathRecording());
			}
			else if (this.m_notificationSwitch != null && this.m_notificationSwitch.m_hit)
			{
				PsState.m_notifications = this.m_notificationSwitch.m_enabled;
				PlayerPrefsX.SetAcceptNotifications(PsState.m_notifications);
				FrbMetrics.TrackNotificationSetting();
				PsMetagameManager.UpdatePlayerSettings();
			}
			else if (this.m_language != null && this.m_language.m_hit)
			{
				PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterLanguageSelect), null, null, null, false, true, InitialPage.Center, false, false, false);
				popup.SetAction("Exit", delegate
				{
					popup.Destroy();
				});
				popup.SetAction("Changed", delegate
				{
					popup.Destroy();
					PsMetagameManager.UpdatePlayerSettings();
					PsPlanetManager.GetCurrentPlanet().UpdatePlanetUI();
					Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
					for (int i = PsState.m_openPopups.Count - 1; i >= 0; i--)
					{
						PsState.m_openPopups[i].DestroyRoot();
					}
				});
			}
			else if (this.m_support != null && this.m_support.m_hit)
			{
				PsMetrics.SupportMessageWindowOpened();
				string text = "Big Bang Racing issue";
				string text2 = string.Concat(new string[]
				{
					"Player name: ",
					PlayerPrefsX.GetUserName(),
					"\rPlayer TAG: ",
					PlayerPrefsX.GetUserTag(),
					"\rDevice: ",
					SystemInfo.deviceModel,
					"\n\rDescribe your issue below the line:\r------------------------------\r"
				});
				text = WWW.EscapeURL(text).Replace("+", "%20");
				text2 = WWW.EscapeURL(text2).Replace("+", "%20");
				string text3 = "mailto:support@traplightgames.com?subject=" + text + "&body=" + text2;
				text3 = "https://bigbangracing.zendesk.com/";
				Application.OpenURL(text3);
			}
			else if (this.m_streamerHUDSwitch != null && this.m_streamerHUDSwitch.m_hit)
			{
				PsState.m_streamerHUD = this.m_streamerHUDSwitch.m_enabled;
				PlayerPrefsX.SetStreamerHud(PsState.m_streamerHUD);
			}
		}
		base.Step();
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x000F554C File Offset: 0x000F394C
	private void BackgroundDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedBezierRect = DebugDraw.GetRoundedBezierRect(_c.m_actualWidth, _c.m_actualHeight, (float)Screen.height * 0.05f, -1f, -1f, 0.2f, true, true, true, true);
		Vector2[] roundedBezierRect2 = DebugDraw.GetRoundedBezierRect(_c.m_actualWidth - (float)Screen.height * 0.02f, _c.m_actualHeight - (float)Screen.height * 0.02f, (float)Screen.height * 0.045f, -1f, -1f, 0.2f, true, true, true, true);
		string text = "#217eb3";
		string text2 = "#165882";
		string text3 = "#86d9f9";
		string text4 = "#1fb3e9";
		string text5 = "#3c90be";
		string text6 = "#114d71";
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 10f, roundedBezierRect, DebugDraw.HexToUint(text2), DebugDraw.HexToUint(text), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		DebugDraw.ScaleVectorArray(roundedBezierRect, new Vector2(0.985f, 0.985f));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, roundedBezierRect, (float)Screen.height * 0.0225f, DebugDraw.HexToColor(text6), DebugDraw.HexToColor(text5), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 7f + Vector3.down * 2f, roundedBezierRect, (float)Screen.height * 0.0225f, DebugDraw.HexToColor("#0a3c5a"), DebugDraw.HexToColor("#0f689f"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 8f, roundedBezierRect2, DebugDraw.HexToUint("#072a3e"), DebugDraw.HexToUint("#072a3e"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f, roundedBezierRect2, (float)Screen.height * 0.0075f, DebugDraw.HexToColor(text4), DebugDraw.HexToColor(text3), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, roundedBezierRect2, (float)Screen.height * 0.025f, new Color(1f, 1f, 1f, 0.25f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Inside, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_glare", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, _c.m_actualHeight * 0.5f - (float)Screen.height * 0.0425f, -5f), 0f);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth - (float)Screen.height * 0.065f, (float)Screen.height * 0.05f);
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_shine", null);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(_c.m_actualWidth * -0.5f + (float)Screen.height * 0.055f, _c.m_actualHeight * 0.5f - (float)Screen.height * 0.035f, -6f), 25f);
		SpriteS.SetDimensions(spriteC2, (float)Screen.height * 0.03f, (float)Screen.height * 0.015f);
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC3, new Vector3(_c.m_actualWidth * 0.5f - (float)Screen.height * 0.04f, _c.m_actualHeight * -0.5f + (float)Screen.height * 0.04f, -6f), 205f);
		SpriteS.SetDimensions(spriteC3, (float)Screen.height * 0.015f, (float)Screen.height * 0.0095f);
		SpriteS.SetColor(spriteC3, new Color(1f, 1f, 1f, 0.6f));
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x000F59B4 File Offset: 0x000F3DB4
	private void ContentBGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.055f * (float)Screen.height, 8, Vector2.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 20f, roundedRect, DebugDraw.HexToUint("#072a3e"), DebugDraw.HexToUint("#072a3e"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		DebugDraw.ScaleVectorArray(roundedRect, new Vector2(1.025f, 1.025f));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 8f, roundedRect, (float)Screen.height * 0.0225f, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x0400198C RID: 6540
	protected PlayerData m_user;

	// Token: 0x0400198D RID: 6541
	private bool m_friend;

	// Token: 0x0400198E RID: 6542
	private PsUIFollowButton m_follow;

	// Token: 0x0400198F RID: 6543
	private PsUIGenericButton m_report;

	// Token: 0x04001990 RID: 6544
	private PsUIGenericButton m_changeName;

	// Token: 0x04001991 RID: 6545
	private PsUIGenericButton m_fbButton;

	// Token: 0x04001992 RID: 6546
	private PsUIRoleButton m_kick;

	// Token: 0x04001993 RID: 6547
	private UIFittedText m_userName;

	// Token: 0x04001994 RID: 6548
	private PsUIBasePopup m_waitingPopup;

	// Token: 0x04001995 RID: 6549
	private PsUIProfileImage m_profileImage;

	// Token: 0x04001996 RID: 6550
	private UIFittedText m_subscriberAmount;

	// Token: 0x04001997 RID: 6551
	private string m_reportText;

	// Token: 0x04001998 RID: 6552
	protected UIVerticalList m_contentList;

	// Token: 0x04001999 RID: 6553
	private PsUIGenericButton m_resetLocalUser;

	// Token: 0x0400199A RID: 6554
	private PsUISwitch m_perfSwitch;

	// Token: 0x0400199B RID: 6555
	private PsUISwitch m_musicSwitch;

	// Token: 0x0400199C RID: 6556
	private PsUISwitch m_soundsSwitch;

	// Token: 0x0400199D RID: 6557
	private PsUISwitch m_handedSwitch;

	// Token: 0x0400199E RID: 6558
	private PsUISwitch m_notificationSwitch;

	// Token: 0x0400199F RID: 6559
	private PsUISwitch m_gifDeathRecording;

	// Token: 0x040019A0 RID: 6560
	private PsUISwitch m_streamerHUDSwitch;

	// Token: 0x040019A1 RID: 6561
	private PsUIGenericButton m_language;

	// Token: 0x040019A2 RID: 6562
	private PsUIGenericButton m_support;

	// Token: 0x040019A3 RID: 6563
	private PsUIGenericButton m_googlePlay;

	// Token: 0x040019A4 RID: 6564
	private PsUIGenericButton m_teamButton;

	// Token: 0x040019A5 RID: 6565
	private UIFixedYoutubeButton m_youtubeUser;

	// Token: 0x040019A6 RID: 6566
	private UITextInput m_input;

	// Token: 0x040019A7 RID: 6567
	private string m_name;

	// Token: 0x040019A8 RID: 6568
	private TeamData m_teamData;

	// Token: 0x040019A9 RID: 6569
	private string m_playerId;

	// Token: 0x040019AA RID: 6570
	private bool m_fromTeam;

	// Token: 0x040019AB RID: 6571
	private PsUILoadingAnimation loading;

	// Token: 0x02000307 RID: 775
	private class PsUIPopupWaiting : PsUIHeaderedCanvas
	{
		// Token: 0x060016EA RID: 5866 RVA: 0x000F6118 File Offset: 0x000F4518
		public PsUIPopupWaiting(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
			this.SetWidth(0.5f, RelativeTo.ScreenWidth);
			this.SetHeight(0.35f, RelativeTo.ScreenHeight);
			this.SetVerticalAlign(0.4f);
			this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.Destroy();
		}
	}

	// Token: 0x02000308 RID: 776
	private class PsUIPopupFacebookWaiting : PsUICenterProfilePopup.PsUIPopupWaiting
	{
		// Token: 0x060016EB RID: 5867 RVA: 0x000F61DC File Offset: 0x000F45DC
		public PsUIPopupFacebookWaiting(UIComponent _parent)
			: base(_parent)
		{
			string text = PsStrings.Get(StringID.POPUP_DISCONNECTING_FROM_FB);
			if (PlayerPrefsX.GetFacebookId() == null)
			{
				text = PsStrings.Get(StringID.CONNECTING);
			}
			new UITextbox(this, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}

	// Token: 0x02000309 RID: 777
	private class PsUIPopupNameWaiting : PsUICenterProfilePopup.PsUIPopupWaiting
	{
		// Token: 0x060016EC RID: 5868 RVA: 0x000F6230 File Offset: 0x000F4630
		public PsUIPopupNameWaiting(UIComponent _parent)
			: base(_parent)
		{
			new UITextbox(this, false, string.Empty, PsStrings.Get(StringID.POPUP_CHANGING_NAME), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}
}
