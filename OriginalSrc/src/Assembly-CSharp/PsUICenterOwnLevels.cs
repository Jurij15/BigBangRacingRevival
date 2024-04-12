using System;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class PsUICenterOwnLevels : UICanvas
{
	// Token: 0x060016A4 RID: 5796 RVA: 0x000EEF94 File Offset: 0x000ED394
	public PsUICenterOwnLevels(UIComponent _parent)
		: base(_parent, true, "ownLevels", null, string.Empty)
	{
		this.progress = SoundS.AddComponent(this.m_TC, "/Metagame/CreatorRank_ProgressBarLoop", 1f, false);
		PsUICenterOwnLevels.m_creatorRank = 0;
		PsUICenterOwnLevels.m_pause = false;
		PsUITabbedCreate.m_selectedTab = 1;
		this.m_afterClaimAmount = PsMetagameManager.m_playerStats.coins;
		this.m_startLoginTime = PlayerPrefsX.GetLastLoginTime();
		this.m_startSessionId = PlayerPrefsX.GetSession();
		this.m_savedLevels = new List<PsMinigameMetaData>();
		this.m_savedBanners = new List<PsUIProfileSavedBanner>();
		PsMetagameManager.ShowResources(null, true, true, false, false, 0.025f, false, false, false);
		this.GetOwnLevels();
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
		(this.m_parent as UIScrollableCanvas).m_maxScrollInertialY = 0f;
		(this.m_parent as UIScrollableCanvas).SetScrollPosition(0f, 0f);
		this.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas.SetWidth(0.4f, RelativeTo.ParentWidth);
		uicanvas.SetAlign(0f, 1f);
		uicanvas.SetDrawHandler(new UIDrawDelegate(this.SavedAreaDrawhandler));
		this.m_savedArea = new UIVerticalList(uicanvas, "saved");
		this.m_savedArea.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_savedArea.SetAlign(0f, 1f);
		this.m_savedArea.SetSpacing(0.085f, RelativeTo.ScreenHeight);
		this.m_savedArea.SetMargins(0f, 0f, 0.05f, 0f, RelativeTo.ScreenHeight);
		this.m_savedArea.SetDepthOffset(-10f);
		this.m_savedArea.RemoveDrawHandler();
		float num = 0.115f;
		float num2 = 0.02f;
		float num3 = 0.03f;
		UICanvas uicanvas2 = new UICanvas(this.m_savedArea, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(num * 1.6f, RelativeTo.ScreenHeight);
		uicanvas2.SetMargins(num3, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uicanvas2, false, string.Empty, PlayerPrefsX.GetFacebookId(), PlayerPrefsX.GetGameCenterId(), -1, true, false, true, 0.1f, 0.06f, "fff9e6", null, true, true);
		psUIProfileImage.SetSize(num, num, RelativeTo.ScreenHeight);
		psUIProfileImage.SetAlign(0f, 1f);
		float num4 = 0.4f - (num + num2 + num3) * (float)Screen.height / (float)Screen.width;
		UIVerticalList uiverticalList = new UIVerticalList(uicanvas2, string.Empty);
		uiverticalList.SetAlign(1f, 1f);
		uiverticalList.SetSpacing(0.005f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas3 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(num4, RelativeTo.ScreenWidth);
		uicanvas3.SetMargins(0f, 0.01f, 0f, 0f, RelativeTo.ScreenWidth);
		uicanvas3.RemoveDrawHandler();
		UIComponent uicomponent = new UIComponent(uicanvas3, false, string.Empty, null, null, string.Empty);
		uicomponent.RemoveDrawHandler();
		uicomponent.SetWidth(0.9f, RelativeTo.ParentWidth);
		uicomponent.SetHorizontalAlign(0f);
		UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, PsStrings.Get(StringID.EDITOR_TEXT_TOTAL_LIKES), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#59B8E3", null);
		uifittedText.SetHorizontalAlign(0f);
		UICanvas uicanvas4 = new UICanvas(uicanvas3, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas4.SetWidth(0.13f, RelativeTo.ScreenHeight);
		uicanvas4.SetHorizontalAlign(1f);
		uicanvas4.RemoveDrawHandler();
		this.m_likesText = new UIFittedText(uicanvas4, false, string.Empty, "...", PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		this.m_likesText.SetHorizontalAlign(1f);
		UICanvas uicanvas5 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas5.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas5.SetWidth(num4, RelativeTo.ScreenWidth);
		uicanvas5.SetMargins(0f, 0.01f, 0f, 0f, RelativeTo.ScreenWidth);
		uicanvas5.RemoveDrawHandler();
		UIComponent uicomponent2 = new UIComponent(uicanvas5, false, string.Empty, null, null, string.Empty);
		uicomponent2.RemoveDrawHandler();
		uicomponent2.SetWidth(0.9f, RelativeTo.ParentWidth);
		uicomponent2.SetHorizontalAlign(0f);
		UIFittedText uifittedText2 = new UIFittedText(uicomponent2, false, string.Empty, PsStrings.Get(StringID.EDITOR_TEXT_TOTAL_EARNINGS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#59B8E3", null);
		uifittedText2.SetHorizontalAlign(0f);
		UICanvas uicanvas6 = new UICanvas(uicanvas5, false, string.Empty, null, string.Empty);
		uicanvas6.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas6.SetWidth(0.13f, RelativeTo.ScreenHeight);
		uicanvas6.SetHorizontalAlign(1f);
		uicanvas6.RemoveDrawHandler();
		this.m_earnings = new UIFittedText(uicanvas6, false, string.Empty, "...", PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		this.m_earnings.SetHorizontalAlign(1f);
		UICanvas uicanvas7 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas7.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas7.SetWidth(num4, RelativeTo.ScreenWidth);
		uicanvas7.SetMargins(0f, 0.01f, 0f, 0f, RelativeTo.ScreenWidth);
		uicanvas7.RemoveDrawHandler();
		UIComponent uicomponent3 = new UIComponent(uicanvas7, false, string.Empty, null, null, string.Empty);
		uicomponent3.RemoveDrawHandler();
		uicomponent3.SetWidth(0.9f, RelativeTo.ParentWidth);
		uicomponent3.SetHorizontalAlign(0f);
		UIFittedText uifittedText3 = new UIFittedText(uicomponent3, false, string.Empty, PsStrings.Get(StringID.EDITOR_TEXT_TOTAL_FOLLOWERS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#59B8E3", null);
		uifittedText3.SetHorizontalAlign(0f);
		UICanvas uicanvas8 = new UICanvas(uicanvas7, false, string.Empty, null, string.Empty);
		uicanvas8.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas8.SetWidth(0.13f, RelativeTo.ScreenHeight);
		uicanvas8.SetHorizontalAlign(1f);
		uicanvas8.RemoveDrawHandler();
		this.m_followers = new UIFittedText(uicanvas8, false, string.Empty, "...", PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		this.m_followers.SetHorizontalAlign(1f);
		UICanvas uicanvas9 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas9.SetHeight(0.55f, RelativeTo.ParentHeight);
		uicanvas9.SetMargins(0f, 0f, 0.5f, -0.5f, RelativeTo.OwnHeight);
		uicanvas9.SetVerticalAlign(0f);
		uicanvas9.RemoveDrawHandler();
		UICanvas uicanvas10 = new UICanvas(uicanvas9, false, string.Empty, null, string.Empty);
		uicanvas10.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas10.SetWidth(0.48f, RelativeTo.ParentWidth);
		uicanvas10.SetAlign(0f, 1f);
		uicanvas10.RemoveDrawHandler();
		UIFittedText uifittedText4 = new UIFittedText(uicanvas10, false, string.Empty, PlayerPrefsX.GetUserName(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, "313131");
		uifittedText4.SetHorizontalAlign(0f);
		UICanvas uicanvas11 = new UICanvas(uicanvas9, false, string.Empty, null, string.Empty);
		uicanvas11.SetHeight(0.25f, RelativeTo.ParentHeight);
		uicanvas11.SetWidth(0.48f, RelativeTo.ParentWidth);
		uicanvas11.SetAlign(0f, 0.4f);
		uicanvas11.RemoveDrawHandler();
		this.m_creatorTitle = new UIFittedText(uicanvas11, false, string.Empty, "...", PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#84F22F", null);
		this.m_creatorTitle.SetHorizontalAlign(0f);
		this.m_starBar = new PsUIStarProgressBar(uicanvas9, 0, 6, string.Empty);
		this.m_starBar.SetHeight(0.3f, RelativeTo.ParentHeight);
		this.m_starBar.SetWidth(6f, RelativeTo.OwnHeight);
		this.m_starBar.SetAlign(0f, 0f);
		UICanvas uicanvas12 = new UICanvas(uicanvas9, false, string.Empty, null, string.Empty);
		uicanvas12.SetAlign(1f, 0.75f);
		uicanvas12.SetMargins(0f, 0.01f, 0f, 0f, RelativeTo.ScreenWidth);
		uicanvas12.SetWidth(0.48f, RelativeTo.ParentWidth);
		uicanvas12.SetHeight(0.6f, RelativeTo.ParentHeight);
		uicanvas12.RemoveDrawHandler();
		UICanvas uicanvas13 = new UICanvas(uicanvas12, false, string.Empty, null, string.Empty);
		uicanvas13.SetAlign(1f, 1f);
		uicanvas13.SetHeight(0.4f, RelativeTo.ParentHeight);
		uicanvas13.RemoveDrawHandler();
		string text = PsStrings.Get(StringID.LIKES_TO_FIRST_RANK);
		text = text.Replace("%1", "...");
		this.m_likesToNextRankText = new UIFittedText(uicanvas13, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		this.m_likesToNextRankText.SetHorizontalAlign(1f);
		this.m_progressBar = new PsUIResourceProgressBar(uicanvas12, 0, 1, string.Empty, false, string.Empty);
		this.m_progressBar.SetDrawHandler(new UIDrawDelegate(this.m_progressBar.CreatorRankDrawHandler));
		this.m_progressBar.SetAlign(1f, 0f);
		this.m_progressBar.SetHeight(0.4f, RelativeTo.ParentHeight);
		this.m_progressBar.SetWidth(0.85f, RelativeTo.ParentWidth);
		new PsUILoadingAnimation(this.m_savedArea, false);
		this.m_liveArea = new UICanvas(null, false, string.Empty, null, string.Empty);
		this.m_liveArea.SetWidth(0.6f, RelativeTo.ScreenWidth);
		this.m_liveArea.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_liveArea.SetMargins(0.03f, 0.03f, 0f, 0.02f, RelativeTo.OwnWidth);
		this.m_liveArea.RemoveDrawHandler();
		UIComponent uicomponent4 = new UIComponent(this.m_liveArea, false, string.Empty, null, null, string.Empty);
		uicomponent4.SetWidth(1f, RelativeTo.ParentWidth);
		uicomponent4.SetHeight(0.055f, RelativeTo.ScreenHeight);
		uicomponent4.SetVerticalAlign(1f);
		uicomponent4.RemoveDrawHandler();
		this.m_liveText = new UIFittedText(uicomponent4, false, string.Empty, PsStrings.Get(StringID.EDITOR_TEXT_LIVE_LEVELS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "f4f4f4", null);
		Color color = DebugDraw.HexToColor("f4f4f4");
		color.a = 0.4f;
		this.m_liveText.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = color;
		this.m_liveText.SetVerticalAlign(1f);
		this.m_publishedLevels = new PsUIProfileLevelArea(this, 3, new cpBB(0.04f, 0.04f, 0.04f, 0.04f), RelativeTo.ScreenHeight, 5, null);
		this.m_publishedLevels.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_publishedLevels.SetWidth(0.6f, RelativeTo.ParentWidth);
		this.m_publishedLevels.SetAlign(1f, 1f);
		this.m_publishedLevels.AddTitleComponent(this.m_liveArea);
		this.m_publishedLevels.PopulateContent(null, typeof(PsGameLoopSocial), "User has no levels", 0.02f, false, false, false);
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x000EFA78 File Offset: 0x000EDE78
	public void GetOwnLevels()
	{
		HttpC own = MiniGame.GetOwn(new Action<OwnLevelsInfo>(this.DataSUCCEED), new Action<HttpC>(this.DataFAILED), 500, null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, own);
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x000EFABC File Offset: 0x000EDEBC
	private void DataSUCCEED(OwnLevelsInfo _data)
	{
		PsMinigameMetaData[] levels = _data.levels;
		this.m_publishedMinigames = new List<PsMinigameMetaData>();
		this.m_savedLevels.Clear();
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i].published)
			{
				this.m_publishedMinigames.Add(levels[i]);
				this.m_claimAllAmount += levels[i].rewardCoins;
			}
			else if (levels[i].m_state == PsMinigameServerState.saved)
			{
				this.m_savedLevels.Add(levels[i]);
			}
		}
		this.CreateSavedLevels(this.m_savedLevels);
		Color color = DebugDraw.HexToColor("ffffff");
		color.a = 1f;
		this.m_liveText.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = color;
		UIComponent uicomponent = new UIComponent(this.m_liveArea, false, string.Empty, null, null, string.Empty);
		uicomponent.SetWidth(1f, RelativeTo.ParentWidth);
		uicomponent.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicomponent.SetMargins(0.03f, 0.03f, 0f, 0f, RelativeTo.OwnWidth);
		uicomponent.SetVerticalAlign(0f);
		uicomponent.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, PsStrings.Get(StringID.CREATE_TEXT_EARN), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		this.m_publishedLevels.PopulateContent(this.GetPublishedLevelsPageRange(), typeof(PsGameLoopSocial), PsStrings.Get(StringID.CREATE_NO_LEVELS_PUBLISHED), 0.035f, true, false, true);
		if (this.m_publishedMinigames.Count > this.m_publishedLevelsPerPage)
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_liveArea, string.Empty);
			uihorizontalList.SetHeight(0.3f, RelativeTo.ParentHeight);
			uihorizontalList.SetVerticalAlign(0f);
			uihorizontalList.SetMargins(0f, 0f, 1.4f, -1.4f, RelativeTo.OwnHeight);
			uihorizontalList.SetSpacing(0.5f, RelativeTo.OwnHeight);
			uihorizontalList.RemoveDrawHandler();
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_roomchange_arrow", null);
			frame.flipX = true;
			this.m_leftPageChange = new UIRectSpriteButton(uihorizontalList, "leftPageChange", PsState.m_uiSheet, frame, true, false);
			this.m_leftPageChange.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_leftPageChange.SetHorizontalAlign(-0.5f);
			this.m_pageChangeText = new UIText(uihorizontalList, false, string.Empty, this.GetPageString(), PsFontManager.GetFont(PsFonts.HurmeBoldMN), 1f, RelativeTo.ParentHeight, null, null);
			this.m_pageChangeText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			this.m_rightPageChange = new UIRectSpriteButton(uihorizontalList, "rightPageChange", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_roomchange_arrow", null), true, false);
			this.m_rightPageChange.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_rightPageChange.SetHorizontalAlign(1.5f);
		}
		int num = _data.totalCoinsEarned;
		for (int j = 0; j < this.m_publishedMinigames.Count; j++)
		{
			num -= this.m_publishedMinigames[j].rewardCoins;
		}
		this.m_likesText.SetText((_data.totalLikes + _data.totalSuperLikes * PsState.m_superLikeVisualMultiplier).ToString());
		this.m_earnings.SetText(num.ToString());
		this.m_followers.SetText(_data.followerCount.ToString());
		PsMetagameManager.m_playerStats.levelsMade = this.m_publishedMinigames.Count;
		PsMetagameManager.m_playerStats.likesEarned = _data.totalLikes;
		PsMetagameManager.m_playerStats.megaLikesEarned = _data.totalSuperLikes;
		int creatorRank = PlayerPrefsX.GetClientConfig().creatorRank1;
		int creatorRank2 = PlayerPrefsX.GetClientConfig().creatorRank2;
		int creatorRank3 = PlayerPrefsX.GetClientConfig().creatorRank3;
		int creatorRank4 = PlayerPrefsX.GetClientConfig().creatorRank4;
		int creatorRank5 = PlayerPrefsX.GetClientConfig().creatorRank5;
		int creatorRank6 = PlayerPrefsX.GetClientConfig().creatorRank6;
		int num2 = _data.totalLikes + _data.totalSuperLikes * PsState.m_superLikeVisualMultiplier;
		this.m_seenLikes = _data.likesSeen;
		this.m_creatorLikes = num2;
		int num3 = 0;
		if (this.m_seenLikes > num2)
		{
			this.m_seenLikes = num2;
		}
		if (this.m_seenLikes >= creatorRank)
		{
			if (this.m_seenLikes >= creatorRank2)
			{
				if (this.m_seenLikes >= creatorRank3)
				{
					if (this.m_seenLikes >= creatorRank4)
					{
						if (this.m_seenLikes >= creatorRank5)
						{
							if (this.m_seenLikes >= creatorRank6)
							{
								num3 = creatorRank6;
								PsUICenterOwnLevels.m_creatorRank = 6;
							}
							else
							{
								num3 = creatorRank5;
								PsUICenterOwnLevels.m_creatorRank = 5;
							}
						}
						else
						{
							num3 = creatorRank4;
							PsUICenterOwnLevels.m_creatorRank = 4;
						}
					}
					else
					{
						num3 = creatorRank3;
						PsUICenterOwnLevels.m_creatorRank = 3;
					}
				}
				else
				{
					num3 = creatorRank2;
					PsUICenterOwnLevels.m_creatorRank = 2;
				}
			}
			else
			{
				num3 = creatorRank;
				PsUICenterOwnLevels.m_creatorRank = 1;
			}
		}
		else
		{
			PsUICenterOwnLevels.m_creatorRank = 0;
		}
		this.m_likesToAnimate = num2 - num3;
		this.UpdateCreatorRank();
		this.Update();
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x000EFFF0 File Offset: 0x000EE3F0
	private void UpdateCreatorRank()
	{
		int creatorRank = PlayerPrefsX.GetClientConfig().creatorRank1;
		int creatorRank2 = PlayerPrefsX.GetClientConfig().creatorRank2;
		int creatorRank3 = PlayerPrefsX.GetClientConfig().creatorRank3;
		int creatorRank4 = PlayerPrefsX.GetClientConfig().creatorRank4;
		int creatorRank5 = PlayerPrefsX.GetClientConfig().creatorRank5;
		int creatorRank6 = PlayerPrefsX.GetClientConfig().creatorRank6;
		string text = PsStrings.Get(StringID.CREATOR_RANK_UNRANKED);
		int num = creatorRank6;
		int num2 = creatorRank6 - creatorRank5;
		string text2 = PsStrings.Get(StringID.LIKES_TO_NEXT_RANK);
		switch (PsUICenterOwnLevels.m_creatorRank)
		{
		case 1:
			text = PsStrings.Get(StringID.CREATOR_RANK_FLEDGING);
			num = creatorRank2;
			num2 = creatorRank2 - creatorRank;
			this.m_likeAnimationMultiplier = 10;
			break;
		case 2:
			text = PsStrings.Get(StringID.CREATOR_RANK_AKNOWLEDGED);
			num = creatorRank3;
			num2 = creatorRank3 - creatorRank2;
			this.m_likeAnimationMultiplier = 100;
			break;
		case 3:
			text = PsStrings.Get(StringID.CREATOR_RANK_ADVANCED);
			num = creatorRank4;
			num2 = creatorRank4 - creatorRank3;
			this.m_likeAnimationMultiplier = 1000;
			break;
		case 4:
			text = PsStrings.Get(StringID.CREATOR_RANK_VETERAN);
			num = creatorRank5;
			num2 = creatorRank5 - creatorRank4;
			this.m_likeAnimationMultiplier = 10000;
			break;
		case 5:
			text = PsStrings.Get(StringID.CREATOR_RANK_SUPERSTAR);
			num = creatorRank6;
			num2 = creatorRank6 - creatorRank5;
			this.m_likeAnimationMultiplier = 100000;
			break;
		case 6:
			this.m_progressBar.Destroy();
			this.m_progressBar = null;
			text = PsStrings.Get(StringID.CREATER_RANK_CREATOR_OF);
			text2 = PsStrings.Get(StringID.CREATOR_RANK_MAXIMUM_RANK);
			break;
		default:
			text = PsStrings.Get(StringID.CREATOR_RANK_UNRANKED);
			num = creatorRank;
			num2 = creatorRank;
			this.m_likeAnimationMultiplier = 1;
			text2 = PsStrings.Get(StringID.LIKES_TO_FIRST_RANK);
			break;
		}
		if (this.m_creatorLikes == this.m_seenLikes)
		{
			this.m_likeAnimationMultiplier *= 2;
		}
		this.m_likesToNextRank = num - (this.m_creatorLikes - this.m_likesToAnimate);
		if (this.m_creatorTitle != null)
		{
			this.m_creatorTitle.SetText(text);
		}
		if (this.m_progressBar != null)
		{
			this.m_progressBar.SetValues(0, num2);
		}
		if (this.m_starBar != null)
		{
			this.m_starBar.SetValues(PsUICenterOwnLevels.m_creatorRank, 6);
		}
		text2 = text2.Replace("%1", this.m_likesToNextRank.ToString());
		this.m_likesToNextRankText.SetText(text2);
		this.m_likesToNextRankText.Update();
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x000F0270 File Offset: 0x000EE670
	private void UpdateLikesToNextRank()
	{
		string text = PsStrings.Get(StringID.LIKES_TO_NEXT_RANK);
		text = text.Replace("%1", this.m_likesToNextRank.ToString());
		this.m_likesToNextRankText.SetText(text);
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x000F02B4 File Offset: 0x000EE6B4
	private void OpenCreatorPopup()
	{
		FrbMetrics.TrackCreatorRank(PsUICenterOwnLevels.m_creatorRank);
		if (PsUICenterOwnLevels.m_creatorRank == 3 || PsUICenterOwnLevels.m_creatorRank == 6)
		{
			PsUICenterOwnLevels.m_pause = true;
			SoundS.PlaySingleShot("/Metagame/CreatorRank_RankUp", Vector3.zero, 1f);
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterOwnLevels.PsUICreatorRankPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
			popup.SetAction("Exit", delegate
			{
				this.UpdateCreatorRank();
				popup.Destroy();
				popup = null;
				PsUICenterOwnLevels.m_pause = false;
			});
			TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		else
		{
			this.UpdateCreatorRank();
		}
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x000F038C File Offset: 0x000EE78C
	private string GetPageString()
	{
		int num = Mathf.Min((this.m_publishedPageIndex + 1) * this.m_publishedLevelsPerPage, this.m_publishedMinigames.Count);
		return string.Concat(new object[]
		{
			string.Empty,
			this.m_publishedPageIndex * this.m_publishedLevelsPerPage + 1,
			"-",
			num,
			" / ",
			this.m_publishedMinigames.Count
		});
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x000F0410 File Offset: 0x000EE810
	private PsMinigameMetaData[] GetPublishedLevelsPageRange()
	{
		if (this.m_publishedMinigames.Count < 1)
		{
			return new PsMinigameMetaData[0];
		}
		int num = Mathf.Min(this.m_publishedPageIndex * this.m_publishedLevelsPerPage, this.m_publishedMinigames.Count - 1);
		int num2 = Mathf.Min(this.m_publishedLevelsPerPage, this.m_publishedMinigames.Count - num);
		return this.m_publishedMinigames.GetRange(num, num2).ToArray();
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x000F0480 File Offset: 0x000EE880
	private void CreateSavedLevels(List<PsMinigameMetaData> _saved)
	{
		PsUICenterOwnLevels.m_freeSlots.Clear();
		this.m_savedBanners.Clear();
		this.m_savedArea.DestroyChildren(1);
		UIVerticalList uiverticalList = new UIVerticalList(this.m_savedArea, string.Empty);
		uiverticalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		for (int i = 0; i < _saved.Count; i++)
		{
			if (_saved[i].name.StartsWith("Saved level"))
			{
				string[] array = _saved[i].name.Split(new char[] { ' ' });
				try
				{
					int num = Convert.ToInt32(array[array.Length - 1]);
					list.Add(num);
					list2.Add(i);
				}
				catch
				{
					list3.Add(i);
				}
			}
			else
			{
				list3.Add(i);
			}
		}
		if (list3.Count > 0)
		{
			for (int j = list3.Count - 1; j >= 0; j--)
			{
				int num2 = 1;
				while (list.Contains(num2))
				{
					num2++;
				}
				list.Add(num2);
				list2.Add(list3[j]);
				_saved[list3[j]].name = "Saved level " + num2;
			}
		}
		bool flag = false;
		for (int k = 0; k < 3; k++)
		{
			if (list.Contains(k + 1))
			{
				int num3 = list2[list.IndexOf(k + 1)];
				PsUIProfileSavedBanner psUIProfileSavedBanner = new PsUIProfileSavedBanner(uiverticalList, new PsGameLoopEditor(_saved[num3].id, _saved[num3], true)
				{
					m_saveNumber = k + 1
				});
				psUIProfileSavedBanner.SetHeight(0.1f, RelativeTo.ScreenHeight);
				this.m_savedBanners.Add(psUIProfileSavedBanner);
			}
			else if (!flag)
			{
				PsUICenterOwnLevels.m_freeSlots.Add(k + 1);
				this.m_createLevel = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.00875f, "Button");
				this.m_createLevel.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
				this.m_createLevel.m_textShadowColor = "#318F03";
				this.m_createLevel.SetFittedText(PsStrings.Get(StringID.EDITOR_BUTTON_CREATE), 0.0385f, 0.4f, RelativeTo.ScreenHeight, true);
				this.m_createLevel.SetGreenColors(true);
				this.m_createLevel.SetHeight(0.1f, RelativeTo.ScreenHeight);
				this.m_createLevel.m_fieldName = (k + 1).ToString();
				flag = true;
			}
			else
			{
				PsUICenterOwnLevels.m_freeSlots.Add(k + 1);
				UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
				uicanvas.SetHeight(0.09f, RelativeTo.ScreenHeight);
				uicanvas.SetWidth(0.425f, RelativeTo.ScreenHeight);
				uicanvas.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
				uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.EmptySpaceRectDrawhandler));
				UIText uitext = new UIText(uicanvas, false, string.Empty, PsStrings.Get(StringID.EDITOR_BUTTON_CREATE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0385f, RelativeTo.ScreenHeight, "f4f4f4", null);
				Color color = DebugDraw.HexToColor("f4f4f4");
				color.a = 0.4f;
				uitext.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = color;
			}
		}
		if (_saved.Count >= 3)
		{
			UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.EDITOR_PROMPT_LEVELS_FULL), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
			uitextbox.SetWidth(0.45f, RelativeTo.ScreenHeight);
		}
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x000F08A0 File Offset: 0x000EECA0
	private void SavedAreaDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Color color = DebugDraw.HexToColor("193254");
		color.a = 0.5f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x000F0918 File Offset: 0x000EED18
	private void DataFAILED(HttpC _c)
	{
		Debug.Log("GET OWN LEVELS FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, delegate
		{
			HttpC own = MiniGame.GetOwn(new Action<OwnLevelsInfo>(this.DataSUCCEED), new Action<HttpC>(this.DataFAILED), 500, null);
			EntityManager.AddComponentToEntity((this.GetRoot() as PsUIBasePopup).m_utilityEntity, own);
			return own;
		}, null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x000F0964 File Offset: 0x000EED64
	public override void Step()
	{
		if (this.m_progressBar != null && !this.m_progressBar.m_hidden && !this.m_progressBar.m_disabled && PsState.m_activeGameLoop == null && (this.m_likesToAnimate > 0 || this.m_progressBar.m_full) && !PsUICenterOwnLevels.m_pause)
		{
			if (this.m_progressBar.m_full)
			{
				PsUICenterOwnLevels.m_creatorRank++;
				if (this.progress.isPlaying)
				{
					SoundS.StopSound(this.progress);
				}
				if (this.m_starBar != null && !this.m_starBar.m_hidden && !this.m_starBar.m_disabled)
				{
					if (PsUICenterOwnLevels.m_creatorRank == 3 || PsUICenterOwnLevels.m_creatorRank == 6)
					{
						this.m_starBar.Increase(true, false);
					}
					else
					{
						this.m_starBar.Increase(true);
					}
				}
				this.OpenCreatorPopup();
			}
			else if (this.m_likeAnimationMultiplier > this.m_likesToAnimate)
			{
				if (this.progress.isPlaying)
				{
					SoundS.StopSound(this.progress);
				}
				this.m_likesToNextRank -= this.m_likesToAnimate;
				this.UpdateLikesToNextRank();
				this.m_likesToNextRankText.Update();
				this.m_progressBar.Increase(this.m_likesToAnimate);
				this.m_likesToAnimate = 0;
			}
			else
			{
				if (!this.progress.isPlaying)
				{
					SoundS.PlaySound(this.progress, false);
				}
				this.m_likesToNextRank -= this.m_likeAnimationMultiplier;
				this.UpdateLikesToNextRank();
				this.m_progressBar.Increase(this.m_likeAnimationMultiplier);
				this.m_likesToAnimate -= this.m_likeAnimationMultiplier;
				if (this.m_likesToAnimate <= 0 && this.progress.isPlaying)
				{
					SoundS.StopSound(this.progress);
				}
			}
		}
		else if (PsUICenterOwnLevels.m_pause && this.progress.isPlaying)
		{
			SoundS.PauseSound(this.progress);
		}
		if (this.m_startLoginTime != PlayerPrefsX.GetLastLoginTime() && this.m_startSessionId == PlayerPrefsX.GetSession() && PsMetagameManager.m_playerStats.coins < this.m_afterClaimAmount)
		{
			PsMetagameManager.m_playerStats.coins = this.m_afterClaimAmount;
			this.m_startLoginTime = PlayerPrefsX.GetLastLoginTime();
		}
		if (this.m_createLevel != null && this.m_createLevel.m_hit)
		{
			PsUICenterOwnLevels.m_pause = true;
			PsMetagameManager.HideResources();
			PsGameLoopEditor psGameLoopEditor = new PsGameLoopEditor(null, null, true);
			psGameLoopEditor.m_saveNumber = Convert.ToInt32(this.m_createLevel.m_fieldName);
			if (PlayerPrefsX.GetNameChanged())
			{
				psGameLoopEditor.StartLoop();
			}
			else
			{
				PsUserNameInputState psUserNameInputState = new PsUserNameInputState();
				psUserNameInputState.m_lastState = Main.m_currentGame.m_currentScene.GetCurrentState();
				psUserNameInputState.m_continueAction = new Action(psGameLoopEditor.StartLoop);
				PsMenuScene.m_lastIState = psUserNameInputState;
				PsMenuScene.m_lastState = null;
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUserNameInputState);
			}
		}
		int i;
		for (i = 0; i < this.m_savedBanners.Count; i++)
		{
			if (this.m_savedBanners[i].m_delete.m_hit)
			{
				PsUICenterOwnLevels.m_pause = true;
				PsUIBasePopup psUIBasePopup = new PsUIBasePopup(typeof(PsUICenterOwnLevels.PsUIPopupConfirmation), null, null, null, true, true, InitialPage.Center, false, false, false);
				(psUIBasePopup.m_mainContent as PsUICenterOwnLevels.PsUIPopupConfirmation).SetLevelNumberText(i + 1);
				psUIBasePopup.SetAction("Proceed", delegate
				{
					PsUICenterOwnLevels.m_pause = false;
					PsMetagameManager.m_playerStats.CumulateEditorResources(this.m_savedBanners[i].m_gameloop.m_minigameMetaData.itemsCount);
					PsMetagameManager.DeleteMinigame(this.m_savedBanners[i].m_gameloop.m_minigameId, PsMetagameManager.m_playerStats.GetUpdatedEditorResources(), null);
					this.m_savedLevels.Remove(this.m_savedBanners[i].m_gameloop.m_minigameMetaData);
					this.CreateSavedLevels(this.m_savedLevels);
					this.Update();
				});
				TweenS.AddTransformTween(psUIBasePopup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				break;
			}
			if (this.m_savedBanners[i].m_build.m_hit)
			{
				PsUICenterOwnLevels.m_pause = true;
				PsMetagameManager.HideResources();
				this.m_savedBanners[i].m_gameloop.StartLoop();
			}
		}
		if ((this.m_leftPageChange != null && this.m_leftPageChange.m_hit) || (this.m_rightPageChange != null && this.m_rightPageChange.m_hit))
		{
			if (this.m_leftPageChange.m_hit && this.m_publishedPageIndex > 0)
			{
				this.m_publishedPageIndex--;
				this.ChangePage();
			}
			else if (this.m_rightPageChange.m_hit && this.m_publishedMinigames.Count > (this.m_publishedPageIndex + 1) * this.m_publishedLevelsPerPage)
			{
				this.m_publishedPageIndex++;
				this.ChangePage();
			}
		}
		else
		{
			for (int j = 0; j < this.m_publishedLevels.m_buttons.Count; j++)
			{
				if (this.m_publishedLevels.m_buttons[j].m_hit && !this.m_publishedLevels.m_buttons[j].m_claimed && this.m_publishedLevels.m_buttons[j].m_claimAmount > 0)
				{
					SoundS.PlaySingleShot("/Metagame/ChestOpen_Coin", Vector3.zero, 1f);
					int num = Convert.ToInt32(this.m_earnings.m_text);
					num += this.m_publishedLevels.m_buttons[j].m_claimAmount;
					this.m_earnings.SetText(num.ToString());
					this.m_afterClaimAmount += this.m_publishedLevels.m_buttons[j].m_claimAmount;
				}
			}
		}
		base.Step();
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x000F0F68 File Offset: 0x000EF368
	public void ChangePage()
	{
		this.m_pageChangeText.SetText(this.GetPageString());
		this.m_pageChangeText.m_parent.Update();
		this.m_publishedLevels.PopulateContent(this.GetPublishedLevelsPageRange(), typeof(PsGameLoopSocial), PsStrings.Get(StringID.CREATE_NO_LEVELS_PUBLISHED), 0.035f, true, false, true);
		this.m_publishedLevels.Update();
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x000F0FCE File Offset: 0x000EF3CE
	public override void Destroy()
	{
		PsMetagameManager.HideResources();
		(this.m_parent as UIScrollableCanvas).m_maxScrollInertialY = 50f / (1024f / (float)Screen.width);
		base.Destroy();
	}

	// Token: 0x0400195E RID: 6494
	private UIVerticalList m_savedArea;

	// Token: 0x0400195F RID: 6495
	private PsUIProfileLevelArea m_publishedLevels;

	// Token: 0x04001960 RID: 6496
	private PsUIGenericButton m_createLevel;

	// Token: 0x04001961 RID: 6497
	private UIFittedText m_liveText;

	// Token: 0x04001962 RID: 6498
	private UICanvas m_liveArea;

	// Token: 0x04001963 RID: 6499
	private UIFittedText m_likesText;

	// Token: 0x04001964 RID: 6500
	private UIFittedText m_likesToNextRankText;

	// Token: 0x04001965 RID: 6501
	private UIFittedText m_donations;

	// Token: 0x04001966 RID: 6502
	private UIFittedText m_earnings;

	// Token: 0x04001967 RID: 6503
	private UIFittedText m_followers;

	// Token: 0x04001968 RID: 6504
	private UIFittedText m_creatorTitle;

	// Token: 0x04001969 RID: 6505
	private PsUIResourceProgressBar m_progressBar;

	// Token: 0x0400196A RID: 6506
	private PsUIStarProgressBar m_starBar;

	// Token: 0x0400196B RID: 6507
	private List<PsMinigameMetaData> m_savedLevels;

	// Token: 0x0400196C RID: 6508
	private List<PsUIProfileSavedBanner> m_savedBanners;

	// Token: 0x0400196D RID: 6509
	private int m_claimAllAmount;

	// Token: 0x0400196E RID: 6510
	private int m_creatorLikes;

	// Token: 0x0400196F RID: 6511
	private int m_seenLikes;

	// Token: 0x04001970 RID: 6512
	public static bool m_pause;

	// Token: 0x04001971 RID: 6513
	public static int m_creatorRank = 0;

	// Token: 0x04001972 RID: 6514
	private int m_likesToAnimate;

	// Token: 0x04001973 RID: 6515
	private int m_likesToNextRank;

	// Token: 0x04001974 RID: 6516
	private int m_likeAnimationMultiplier = 1;

	// Token: 0x04001975 RID: 6517
	private string m_startLoginTime;

	// Token: 0x04001976 RID: 6518
	private string m_startSessionId;

	// Token: 0x04001977 RID: 6519
	private int m_afterClaimAmount;

	// Token: 0x04001978 RID: 6520
	public static List<int> m_freeSlots = new List<int>();

	// Token: 0x04001979 RID: 6521
	private SoundC progress;

	// Token: 0x0400197A RID: 6522
	private List<PsMinigameMetaData> m_publishedMinigames;

	// Token: 0x0400197B RID: 6523
	private int m_publishedPageIndex;

	// Token: 0x0400197C RID: 6524
	private int m_publishedLevelsPerPage = 51;

	// Token: 0x0400197D RID: 6525
	private UIRectSpriteButton m_leftPageChange;

	// Token: 0x0400197E RID: 6526
	private UIRectSpriteButton m_rightPageChange;

	// Token: 0x0400197F RID: 6527
	private UIText m_pageChangeText;

	// Token: 0x04001980 RID: 6528
	private UIHorizontalList m_pageChangeList;

	// Token: 0x02000304 RID: 772
	private class PsUIPopupConfirmation : PsUIHeaderedCanvas
	{
		// Token: 0x060016B4 RID: 5812 RVA: 0x000F1058 File Offset: 0x000EF458
		public PsUIPopupConfirmation(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
			this.SetWidth(0.65f, RelativeTo.ScreenWidth);
			this.SetHeight(0.45f, RelativeTo.ScreenHeight);
			this.SetVerticalAlign(0.4f);
			this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.Destroy();
			this.m_message = new UITextbox(this, false, string.Empty, PsStrings.Get(StringID.LEVEL_DELETE_WARNING), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
			uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetVerticalAlign(0f);
			uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
			this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_ok.SetText(PsStrings.Get(StringID.DELETE).ToUpper(), 0.045f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_ok.SetHeight(0.095f, RelativeTo.ScreenHeight);
			this.m_ok.SetRedColors();
			this.m_cancel = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_cancel.SetText(PsStrings.Get(StringID.CANCEL), 0.045f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_cancel.SetHeight(0.095f, RelativeTo.ScreenHeight);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x000F1240 File Offset: 0x000EF640
		public void SetLevelNumberText(int _levelNumber)
		{
			string text = PsStrings.Get(StringID.LEVEL_DELETE_WARNING);
			string text2 = PsStrings.Get(StringID.EDITOR_BUTTON_SAVED_LEVEL) + " " + _levelNumber;
			text = text.Replace("%1", text2);
			this.m_message.SetText(text);
			this.Update();
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000F1294 File Offset: 0x000EF694
		public override void Step()
		{
			if (this.m_ok.m_hit)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Proceed");
				(this.GetRoot() as PsUIBasePopup).Destroy();
			}
			else if (this.m_cancel.m_hit)
			{
				PsUICenterOwnLevels.m_pause = false;
				(this.GetRoot() as PsUIBasePopup).Destroy();
			}
			base.Step();
		}

		// Token: 0x04001982 RID: 6530
		private PsUIGenericButton m_ok;

		// Token: 0x04001983 RID: 6531
		private PsUIGenericButton m_cancel;

		// Token: 0x04001984 RID: 6532
		private UITextbox m_message;
	}

	// Token: 0x02000305 RID: 773
	private class PsUICreatorRankPopup : PsUIHeaderedCanvas
	{
		// Token: 0x060016B7 RID: 5815 RVA: 0x000F1308 File Offset: 0x000EF708
		public PsUICreatorRankPopup(UIComponent _parent)
			: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			this.SetWidth(0.45f, RelativeTo.ScreenWidth);
			this.SetHeight(0.55f, RelativeTo.ScreenHeight);
			this.SetVerticalAlign(0.4f);
			this.SetMargins(0.0225f, 0.0225f, 0.0225f, 0.0225f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
			this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
			this.CreateContent(this);
			this.CreateHeaderContent(this.m_header);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000F13EC File Offset: 0x000EF7EC
		public void CreateHeaderContent(UIComponent _parent)
		{
			_parent.SetMargins(0.3f, RelativeTo.OwnHeight);
			UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, PsStrings.Get(StringID.RANK_UP), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000F1428 File Offset: 0x000EF828
		public void CreateContent(UIComponent _parent)
		{
			UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.2f, RelativeTo.ParentHeight);
			uicanvas.SetWidth(0.5f, RelativeTo.ParentWidth);
			uicanvas.SetVerticalAlign(0.95f);
			uicanvas.RemoveDrawHandler();
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.4f, RelativeTo.ParentHeight);
			uicanvas2.SetVerticalAlign(1f);
			uicanvas2.RemoveDrawHandler();
			string text = string.Empty;
			switch (PsUICenterOwnLevels.m_creatorRank)
			{
			case 1:
				text = PsStrings.Get(StringID.CREATOR_RANK_FLEDGING);
				break;
			case 2:
				text = PsStrings.Get(StringID.CREATOR_RANK_AKNOWLEDGED);
				break;
			case 3:
				text = PsStrings.Get(StringID.CREATOR_RANK_ADVANCED);
				break;
			case 4:
				text = PsStrings.Get(StringID.CREATOR_RANK_VETERAN);
				break;
			case 5:
				text = PsStrings.Get(StringID.CREATOR_RANK_SUPERSTAR);
				break;
			case 6:
				text = PsStrings.Get(StringID.CREATER_RANK_CREATOR_OF);
				break;
			default:
				text = PsStrings.Get(StringID.CREATOR_RANK_UNRANKED);
				break;
			}
			UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#84F22F", null);
			PsUIStarProgressBar psUIStarProgressBar = new PsUIStarProgressBar(uicanvas, PsUICenterOwnLevels.m_creatorRank, 6, string.Empty);
			psUIStarProgressBar.SetHeight(0.55f, RelativeTo.ParentHeight);
			psUIStarProgressBar.SetWidth(6f, RelativeTo.OwnHeight);
			psUIStarProgressBar.SetVerticalAlign(0f);
			this.CreateShine(this);
			_parent.RemoveTouchAreas();
			string text2 = string.Empty;
			string text3 = string.Empty;
			if (PsUICenterOwnLevels.m_creatorRank == 3)
			{
				text2 = PsStrings.Get(StringID.UNLOCKED_ADVANCED_ITEMS);
				text3 = PsStrings.Get(StringID.WORD_COOL);
			}
			else if (PsUICenterOwnLevels.m_creatorRank == 6)
			{
				text2 = PsStrings.Get(StringID.ULTIMATE_CREATOR_RANK_ACHIEVED);
				text3 = PsStrings.Get(StringID.WORD_AWESOME);
			}
			UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetAlign(0.5f, 0f);
			uihorizontalList.SetMargins(0f, 0f, 0.065f, -0.065f, RelativeTo.ScreenHeight);
			PsUIGenericButton psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			psUIGenericButton.SetText(text3, 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			psUIGenericButton.SetGreenColors(true);
			psUIGenericButton.SetReleaseAction(delegate
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			});
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000F16B0 File Offset: 0x000EFAB0
		private void CreateShine(UIComponent _parent)
		{
			this.m_tournamentShine = new UISprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shine", null), true);
			this.m_tournamentShine.SetSize(1f, 1f, RelativeTo.ScreenHeight);
			this.m_tournamentShine.SetDepthOffset(30f);
			this.m_tournamentShineRotationTween = TweenS.AddTransformTween(this.m_tournamentShine.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, -360f), 19f, 0f, false);
			this.m_tournamentShineRotationTween.repeats = -1;
			this.m_tournamentShineScaleTween = TweenS.AddTransformTween(this.m_tournamentShine.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(1.2f, 1.2f, 1f), 1.6f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_tournamentShineScaleTween, new TweenEventDelegate(this.TournamentShineScaleSmallerTween));
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000F17A4 File Offset: 0x000EFBA4
		private void TournamentShineScaleBiggerTween(TweenC _tweenC)
		{
			if (this.m_tournamentShineScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_tournamentShineScaleTween);
				this.m_tournamentShineScaleTween = null;
			}
			this.m_tournamentShineScaleTween = TweenS.AddTransformTween(this.m_tournamentShine.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(1.1f, 1.1f, 1f), 1.6f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_tournamentShineScaleTween, new TweenEventDelegate(this.TournamentShineScaleSmallerTween));
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000F181C File Offset: 0x000EFC1C
		private void TournamentShineScaleSmallerTween(TweenC _tweenC)
		{
			if (this.m_tournamentShineScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_tournamentShineScaleTween);
				this.m_tournamentShineScaleTween = null;
			}
			this.m_tournamentShineScaleTween = TweenS.AddTransformTween(this.m_tournamentShine.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(0.9f, 0.9f, 1f), 1.6f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_tournamentShineScaleTween, new TweenEventDelegate(this.TournamentShineScaleBiggerTween));
		}

		// Token: 0x04001987 RID: 6535
		private UISprite m_tournamentShine;

		// Token: 0x04001988 RID: 6536
		private TweenC m_tournamentShineRotationTween;

		// Token: 0x04001989 RID: 6537
		private TweenC m_tournamentShineScaleTween;
	}
}
