using System;
using UnityEngine;

// Token: 0x02000368 RID: 872
public class PsUICenterMyTeam : UICanvas
{
	// Token: 0x06001944 RID: 6468 RVA: 0x001127BC File Offset: 0x00110BBC
	public PsUICenterMyTeam(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		PsUICenterMyTeam.m_updateMembers = false;
		PsUITabbedTeam.m_selectedTab = 1;
		this.m_sideCreated = false;
		(this.m_parent as UIScrollableCanvas).m_maxScrollInertialY = 0f;
		(this.m_parent as UIScrollableCanvas).SetScrollPosition(0f, 0f);
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas.SetWidth(0.4f, RelativeTo.ParentWidth);
		uicanvas.SetAlign(0f, 1f);
		uicanvas.SetDrawHandler(new UIDrawDelegate(this.SideDrawhandler));
		this.m_sideList = new UIVerticalList(uicanvas, "sideList");
		this.m_sideList.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_sideList.SetMargins(0.03f, 0.02f, 0.02f, 0f, RelativeTo.ScreenHeight);
		this.m_sideList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_sideList.SetVerticalAlign(1f);
		this.m_sideList.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(this.m_sideList, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(0.365f, RelativeTo.ScreenWidth);
		uicanvas2.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, PsMetagameManager.m_team.name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FCCB1D", null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas, string.Empty);
		uihorizontalList.SetVerticalAlign(0f);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0f, 0f, 0f, 0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		this.m_leave = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.0035f, "Button");
		this.m_leave.SetRedColors();
		this.m_leave.SetFittedText(PsStrings.Get(StringID.TEAM_BUTTON_LEAVE), 0.04f, 0.15f, RelativeTo.ScreenHeight, false);
		this.m_leave.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uihorizontalList, string.Empty);
		uihorizontalList2.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uihorizontalList2.SetMargins(0.01f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		uihorizontalList2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
		if (PlayerPrefsX.GetTeamRole() == TeamRole.Creator)
		{
			this.m_settings = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.0035f, "Button");
			this.m_settings.SetBlueColors(true);
			this.m_settings.SetIcon("menu_icon_settings", 0.04f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_settings.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		}
		else
		{
			uihorizontalList2.SetMargins(0.025f, 0.01f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			UICanvas uicanvas3 = new UICanvas(uihorizontalList2, false, string.Empty, null, string.Empty);
			uicanvas3.SetRogue();
			uicanvas3.SetSize(0.035f, 0.035f, RelativeTo.ScreenHeight);
			uicanvas3.SetAlign(0f, 1f);
			uicanvas3.SetMargins(-0.0315f, 0.0315f, -0.0225f, 0.0225f, RelativeTo.ScreenHeight);
			uicanvas3.SetDepthOffset(-5f);
			uicanvas3.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true, true);
			uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		}
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList2, string.Empty);
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas4 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.025f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(0.2f, RelativeTo.ScreenHeight);
		uicanvas4.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas4, false, string.Empty, ClientTools.GetTeamJoinTypeName(PsMetagameManager.m_team.joinType), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#B9D7E9", null);
		uifittedText2.SetHorizontalAlign(0f);
		UICanvas uicanvas5 = new UICanvas(uicanvas4, false, string.Empty, null, string.Empty);
		uicanvas5.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas5.SetWidth(0.035f, RelativeTo.ScreenHeight);
		uicanvas5.SetHorizontalAlign(0f);
		uicanvas5.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas5.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas5, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_lock", null), true, true);
		uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
		UICanvas uicanvas6 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas6.SetHeight(0.025f, RelativeTo.ScreenHeight);
		uicanvas6.SetWidth(0.2f, RelativeTo.ScreenHeight);
		uicanvas6.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas6.RemoveDrawHandler();
		string text = PsStrings.Get(StringID.TEAM_LIMIT_TROPHIES_REQUIRED);
		text = text.Replace("%1", PsMetagameManager.m_team.requiredTrophies + string.Empty);
		UIFittedText uifittedText3 = new UIFittedText(uicanvas6, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#B9D7E9", null);
		uifittedText3.SetHorizontalAlign(0f);
		UICanvas uicanvas7 = new UICanvas(uicanvas6, false, string.Empty, null, string.Empty);
		uicanvas7.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas7.SetWidth(0.035f, RelativeTo.ScreenHeight);
		uicanvas7.SetHorizontalAlign(0f);
		uicanvas7.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas7.RemoveDrawHandler();
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas7, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true, true);
		uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
		this.CreateRightSide();
		this.GetTeam();
	}

	// Token: 0x06001945 RID: 6469 RVA: 0x00112E34 File Offset: 0x00111234
	public virtual void CreateRightSide()
	{
		UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(this, string.Empty);
		uiscrollableCanvas.SetWidth(0.6f, RelativeTo.ParentWidth);
		uiscrollableCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uiscrollableCanvas.SetAlign(1f, 1f);
		uiscrollableCanvas.RemoveDrawHandler();
		this.m_memberList = new UIVerticalList(uiscrollableCanvas, "memberList");
		this.m_memberList.SetWidth(0.6f, RelativeTo.ScreenWidth);
		this.m_memberList.SetVerticalAlign(1f);
		this.m_memberList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_memberList.SetMargins(0.01f, 0f, 0.1f, 0.05f, RelativeTo.ScreenWidth);
		this.m_memberList.RemoveDrawHandler();
		this.m_memberList.SetVerticalAlign(1f);
	}

	// Token: 0x06001946 RID: 6470 RVA: 0x00112EFC File Offset: 0x001112FC
	public virtual void GetTeam()
	{
		this.m_sideList.DestroyChildren(1);
		this.m_memberList.DestroyChildren();
		new PsUILoadingAnimation(this.m_sideList, false);
		new PsUILoadingAnimation(this.m_memberList, false);
		PsMetagameManager.GetOwnTeam(new Action<TeamData>(this.TeamGetOK), false);
	}

	// Token: 0x06001947 RID: 6471 RVA: 0x00112F4D File Offset: 0x0011134D
	public virtual void TeamGetOK(TeamData _data)
	{
		Debug.Log("GET TEAM SUCCEED", null);
		PsMetagameManager.m_team = _data;
		this.m_memberList.DestroyChildren();
		this.m_sideList.DestroyChildren(1);
		this.m_searched = true;
	}

	// Token: 0x06001948 RID: 6472 RVA: 0x00112F80 File Offset: 0x00111380
	public void TeamGetFAILED(HttpC _c)
	{
		Debug.Log("GET TEAM FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC httpC = PsServerRequest.ServerGetTeam((string)_c.objectData, true, new Action<TeamData>(this.TeamGetOK), new Action<HttpC>(this.TeamGetFAILED), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
			return httpC;
		}, null);
	}

	// Token: 0x06001949 RID: 6473 RVA: 0x00112FD4 File Offset: 0x001113D4
	private void CreateBatch()
	{
		if (this.m_currentIndex == 0 && PsMetagameManager.m_team.memberCount >= 5)
		{
			this.CreateRewardPlate();
		}
		int num = Mathf.Min(this.m_currentIndex + 10, PsMetagameManager.m_team.memberList.Length);
		for (int i = this.m_currentIndex; i < num; i++)
		{
			PsMetagameManager.m_team.memberList[i].teamData = PsMetagameManager.m_team;
			PsUITeamProfileBanner psUITeamProfileBanner = new PsUITeamProfileBanner(this.m_memberList, i, PsMetagameManager.m_team.memberList[i], PsMetagameManager.m_team.memberCount >= 5, PsMetagameManager.m_team.memberCount >= 5, true, true, false, false);
			psUITeamProfileBanner.Update();
		}
		this.m_memberList.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_memberList.CalculateReferenceSizes();
		this.m_memberList.UpdateSize();
		this.m_memberList.ArrangeContents();
		this.m_memberList.UpdateDimensions();
		this.m_memberList.UpdateSize();
		this.m_memberList.UpdateAlign();
		this.m_memberList.UpdateChildrenAlign();
		this.m_memberList.ArrangeContents();
		this.m_memberList.m_parent.ArrangeContents();
		this.m_currentIndex = num;
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x0011311C File Offset: 0x0011151C
	public void CreateRewardPlate()
	{
		UICanvas uicanvas = new UICanvas(this.m_memberList, false, "rewardPlate", null, string.Empty);
		uicanvas.SetWidth(0.08400001f, RelativeTo.ScreenWidth);
		uicanvas.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetRogue();
		uicanvas.SetAlign(1f, 1f);
		float num = (float)Screen.width / (float)Screen.height;
		uicanvas.SetMargins(0f, 0.006f * num, 0f, 0f, RelativeTo.ScreenWidth);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(0.08400001f, RelativeTo.ScreenWidth);
		float num2 = (float)PsMetagameManager.m_team.memberCount * 0.07f + 0.02f;
		uicanvas2.SetHeight(num2, RelativeTo.ScreenHeight);
		uicanvas2.SetAlign(1f, 1f);
		uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SeasonBottomRewardDrawhandler));
		UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetHeight(0.08f, RelativeTo.ScreenHeight);
		uicanvas3.SetVerticalAlign(1f);
		uicanvas3.SetMargins(0f, 0f, -0.085f, 0.085f, RelativeTo.ScreenHeight);
		uicanvas3.RemoveDrawHandler();
		uicanvas3.SetDepthOffset(5f);
		UICanvas uicanvas4 = new UICanvas(uicanvas3, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.SetMargins(0.04f, 0.01f, 0.0175f, 0.0175f, RelativeTo.ScreenHeight);
		uicanvas4.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SeasonTopRewardDrawhandler));
		UIFittedText uifittedText = new UIFittedText(uicanvas4, false, string.Empty, PsMetagameManager.m_team.teamSeasonReward.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		UICanvas uicanvas5 = new UICanvas(uicanvas4, false, string.Empty, null, string.Empty);
		uicanvas5.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas5.SetHorizontalAlign(0f);
		uicanvas5.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas5.RemoveDrawHandler();
		string currentSeasonRewardIcon = PsMetagameManager.m_seasonEndData.GetCurrentSeasonRewardIcon();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas5, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(currentSeasonRewardIcon, null), true, true);
		uifittedSprite.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uifittedSprite.SetHorizontalAlign(0f);
		uicanvas.Update();
	}

	// Token: 0x0600194B RID: 6475 RVA: 0x001133C0 File Offset: 0x001117C0
	public virtual void UpdateLogic()
	{
		if (this.m_searched && !this.m_sideCreated)
		{
			this.CreateSideInfo();
			this.m_sideList.Update();
		}
		if (PsUICenterMyTeam.m_updateMembers)
		{
			this.m_memberList.DestroyChildren();
			this.m_currentIndex = 0;
			PsUICenterMyTeam.m_updateMembers = false;
		}
		if (this.m_searched && this.m_currentIndex < PsMetagameManager.m_team.memberCount && PsMetagameManager.m_team.memberList != null)
		{
			this.CreateBatch();
		}
		if (this.m_leave != null && this.m_leave.m_hit)
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUICenterLeaveTeam), null, null, null, true, true, InitialPage.Center, false, false, false);
			this.m_popup.SetAction("Exit", delegate
			{
				this.m_popup.Destroy();
				this.m_popup = null;
			});
			this.m_popup.SetAction("Proceed", delegate
			{
				this.LeaveTeam();
			});
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		else if (this.m_settings != null && this.m_settings.m_hit)
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUICenterTeamSettings), null, null, null, true, true, InitialPage.Center, false, false, false);
			this.m_popup.SetAction("Exit", delegate
			{
				this.m_popup.Destroy();
				this.m_popup = null;
			});
			this.m_popup.SetAction("Save", delegate
			{
				this.m_popup.Destroy();
				this.m_popup = null;
				this.UpdateTeamInfo();
			});
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
	}

	// Token: 0x0600194C RID: 6476 RVA: 0x001135AC File Offset: 0x001119AC
	public override void Step()
	{
		if (this.m_currentTimeLeft != PsMetagameManager.m_seasonTimeleft && this.m_timeText != null)
		{
			this.m_currentTimeLeft = PsMetagameManager.m_seasonTimeleft;
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(PsMetagameManager.m_seasonTimeleft);
			string text = PsStrings.Get(StringID.TEAM_SEASON_END_TIMER);
			text = text.Replace("%1", timeStringFromSeconds);
			this.m_timeText.SetText(text);
		}
		this.UpdateLogic();
		base.Step();
	}

	// Token: 0x0600194D RID: 6477 RVA: 0x0011361A File Offset: 0x00111A1A
	public void UpdateTeamInfo()
	{
		this.m_sideList.DestroyChildren(1);
		this.CreateSideInfo();
		this.m_sideList.Update();
	}

	// Token: 0x0600194E RID: 6478 RVA: 0x0011363C File Offset: 0x00111A3C
	public void LeaveTeam()
	{
		PsMetrics.PlayerLeftTeam();
		PsMetagameManager.LeaveTeam(PsMetagameManager.m_team.id, new Action<HttpC>(PsMetagameManager.LeaveTeamSUCCEED), new Action<HttpC>(PsMetagameManager.LeaveTeamFAILED), null);
		PlayerPrefsX.SetTeamId(null);
		PlayerPrefsX.SetTeamName(null);
		PlayerPrefsX.SetTeamRole(TeamRole.NotInTeam);
		PsMetagameManager.m_team = null;
		PsUITabbedTeam.m_selectedTab = 1;
		PsUITabbedTeam.m_selectedSubTab = 1;
		PsMainMenuState.ChangeToTeamState();
	}

	// Token: 0x0600194F RID: 6479 RVA: 0x001136C4 File Offset: 0x00111AC4
	public void CreateSideInfo()
	{
		this.m_sideCreated = true;
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_sideList, string.Empty);
		uihorizontalList.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas.SetWidth(0.11f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.04f, 0.01f, 0.005f, 0.005f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsMetagameManager.m_team.memberCount + "/50", PsFontManager.GetFont(PsFonts.KGSecondChances), true, "ffffff", "#011532");
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetSize(1.5f, 1.5f, RelativeTo.ParentHeight);
		uicanvas2.SetHorizontalAlign(0f);
		uicanvas2.SetMargins(-0.045f, 0.045f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_clan_members_icon", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		UICanvas uicanvas3 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas3.SetWidth(0.225f, RelativeTo.ScreenHeight);
		uicanvas3.SetMargins(0.06f, 0f, 0.005f, 0.005f, RelativeTo.ScreenHeight);
		uicanvas3.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, PsMetagameManager.m_team.teamScore.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "ffffff", "#011532");
		uifittedText2.SetHorizontalAlign(0f);
		UICanvas uicanvas4 = new UICanvas(uicanvas3, false, string.Empty, null, string.Empty);
		uicanvas4.SetSize(1.5f, 1.5f, RelativeTo.ParentHeight);
		uicanvas4.SetHorizontalAlign(0f);
		uicanvas4.SetMargins(-0.045f, 0.045f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true, true);
		uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
		string text = string.Empty;
		if (!string.IsNullOrEmpty(PsMetagameManager.m_team.description))
		{
			text = "\"" + PsMetagameManager.m_team.description + "\"";
		}
		UITextbox uitextbox = new UITextbox(this.m_sideList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "#87F1FF", true, null);
		uitextbox.m_tmc.m_textMesh.fontStyle = 2;
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
		uitextbox.SetHeight(0.1f, RelativeTo.ScreenHeight);
		uitextbox.SetHorizontalAlign(0.5f);
		uitextbox.SetMaxRows(4);
		UIVerticalList uiverticalList = new UIVerticalList(this.m_sideList, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetSpacing(0.005f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0f, 0f, 0.03f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		if (PsMetagameManager.m_team.memberCount < 5)
		{
			UICanvas uicanvas5 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas5.SetWidth(0.92f, RelativeTo.ParentWidth);
			uicanvas5.SetHeight(0.1f, RelativeTo.ScreenHeight);
			uicanvas5.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
			uicanvas5.SetDepthOffset(-20f);
			UICanvas uicanvas6 = new UICanvas(uicanvas5, false, string.Empty, null, string.Empty);
			uicanvas6.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
			uicanvas6.SetAlign(0f, 1f);
			uicanvas6.SetMargins(-0.015f, 0.015f, -0.015f, 0.015f, RelativeTo.ScreenHeight);
			uicanvas6.RemoveDrawHandler();
			UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas6, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true, true);
			uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
			string text2 = PsStrings.Get(StringID.TEAM_MEMBER_INFO);
			text2 = text2.Replace("%1", "5");
			UITextbox uitextbox2 = new UITextbox(uicanvas5, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, "#76C8E6", true, null);
			uitextbox2.SetMargins(0.025f, RelativeTo.ScreenHeight);
		}
		else
		{
			UICanvas uicanvas7 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas7.SetHeight(0.09f, RelativeTo.ScreenHeight);
			uicanvas7.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas7.SetMargins(0f, 0f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			uicanvas7.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SeasonTopDrawhandler));
			UICanvas uicanvas8 = new UICanvas(uicanvas7, false, string.Empty, null, string.Empty);
			uicanvas8.SetHeight(0.0315f, RelativeTo.ScreenHeight);
			uicanvas8.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas8.SetMargins(0.07f, 0.07f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas8.SetVerticalAlign(1f);
			uicanvas8.RemoveDrawHandler();
			UIFittedText uifittedText3 = new UIFittedText(uicanvas8, false, string.Empty, PsStrings.Get(StringID.TEAM_SEASON_HEADER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FEF18D", null);
			UICanvas uicanvas9 = new UICanvas(uicanvas7, false, string.Empty, null, string.Empty);
			uicanvas9.SetHeight(0.0275f, RelativeTo.ScreenHeight);
			uicanvas9.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas9.SetMargins(0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas9.SetVerticalAlign(0f);
			uicanvas9.RemoveDrawHandler();
			this.m_currentTimeLeft = PsMetagameManager.m_seasonTimeleft;
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(PsMetagameManager.m_seasonTimeleft);
			string text3 = PsStrings.Get(StringID.TEAM_SEASON_END_TIMER);
			text3 = text3.Replace("%1", timeStringFromSeconds);
			this.m_timeText = new UIFittedText(uicanvas9, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#423312", null);
			if (PsMetagameManager.m_team.topTenRank > 0)
			{
				this.LowerTopTeamInfo(uiverticalList);
			}
			else
			{
				this.LowerInfo(uiverticalList);
			}
		}
	}

	// Token: 0x06001950 RID: 6480 RVA: 0x00113D58 File Offset: 0x00112158
	public void LowerTopTeamInfo(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.175f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetMargins(0.01f, 0.01f, 0.025f, 0.025f, RelativeTo.ScreenHeight);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SeasonBottomDrawhandler));
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas, string.Empty);
		uihorizontalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_icon_trophy_full", null), true, true);
		uifittedSprite.SetHeight(0.145f, RelativeTo.ScreenHeight);
		TransformS.SetRotation(uifittedSprite.m_TC, new Vector3(0f, 0f, 5f));
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(0.2f, RelativeTo.ScreenWidth);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.0325f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetHorizontalAlign(0f);
		uicanvas2.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_TOP_TEAM).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", null);
		uifittedText.SetHorizontalAlign(0f);
		uifittedText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UICanvas uicanvas3 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.0325f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetHorizontalAlign(0f);
		uicanvas3.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, string.Concat(new object[]
		{
			PsStrings.Get(StringID.TEAMS_WORLD_RANK),
			" <color=#ffffff>#",
			PsMetagameManager.m_team.topTenRank,
			"</color>"
		}), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#F732FD", null);
		uifittedText2.SetHorizontalAlign(0f);
		uifittedText2.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UICanvas uicanvas4 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.0325f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.SetHorizontalAlign(0f);
		uicanvas4.RemoveDrawHandler();
		uicanvas4.SetMargins(1.1f, 0f, 0f, 0f, RelativeTo.OwnHeight);
		UIFittedText uifittedText3 = new UIFittedText(uicanvas4, false, string.Empty, PsMetagameManager.m_team.teamSeasonReward.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", null);
		uifittedText3.SetHorizontalAlign(0f);
		uifittedText3.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UICanvas uicanvas5 = new UICanvas(uifittedText3, false, string.Empty, null, string.Empty);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas5.SetWidth(1f, RelativeTo.ParentHeight);
		uicanvas5.SetHorizontalAlign(0f);
		uicanvas5.SetMargins(-1.1f, 1.1f, 0f, 0f, RelativeTo.ParentHeight);
		uicanvas5.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas5, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(PsMetagameManager.m_seasonEndData.GetCurrentSeasonRewardIcon(), null), true, true);
		uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
		UICanvas uicanvas6 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas6.SetWidth(0.2f, RelativeTo.ScreenHeight);
		uicanvas6.SetHeight(0.085f, RelativeTo.ScreenHeight);
		uicanvas6.SetVerticalAlign(0f);
		uicanvas6.SetMargins(0f, 0f, 0.105f, -0.105f, RelativeTo.ScreenHeight);
		uicanvas6.RemoveDrawHandler();
		UICanvas uicanvas7 = new UICanvas(uicanvas6, false, string.Empty, null, string.Empty);
		uicanvas7.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas7.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas7.SetMargins(0.01f, RelativeTo.ScreenHeight);
		uicanvas7.SetDrawHandler(new UIDrawDelegate(this.ScorePlateDrawhandler));
		UICanvas uicanvas8 = new UICanvas(uicanvas7, false, string.Empty, null, string.Empty);
		uicanvas8.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas8.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas8.SetVerticalAlign(1f);
		uicanvas8.RemoveDrawHandler();
		UIFittedText uifittedText4 = new UIFittedText(uicanvas8, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_TROPHIES).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#43330B", null);
		UICanvas uicanvas9 = new UICanvas(uicanvas7, false, string.Empty, null, string.Empty);
		uicanvas9.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas9.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas9.SetVerticalAlign(0f);
		uicanvas9.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas9.RemoveDrawHandler();
		UIFittedText uifittedText5 = new UIFittedText(uicanvas9, false, string.Empty, PsMetagameManager.m_team.teamScore.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFF792", null);
		UICanvas uicanvas10 = new UICanvas(uifittedText5, false, string.Empty, null, string.Empty);
		uicanvas10.SetSize(0.03f, 0.03f, RelativeTo.ScreenHeight);
		uicanvas10.SetHorizontalAlign(0f);
		uicanvas10.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas10.RemoveDrawHandler();
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas10, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true, true);
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x00114344 File Offset: 0x00112744
	public void LowerInfo(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.175f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetMargins(0.01f, 0.01f, 0.025f, 0.025f, RelativeTo.ScreenHeight);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SeasonBottomDrawhandler));
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas, string.Empty);
		uihorizontalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(0.225f, RelativeTo.ScreenWidth);
		uicanvas2.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TEAM_REWARD_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#F82EFF", null);
		uifittedText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UICanvas uicanvas3 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(0.075f, RelativeTo.ScreenWidth);
		uicanvas3.RemoveDrawHandler();
		new UIFittedText(uicanvas3, false, string.Empty, PsMetagameManager.m_team.teamSeasonReward.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", null)
		{
			m_tmc = 
			{
				m_renderer = 
				{
					material = 
					{
						shader = Shader.Find("Framework/ColorFontShader")
					}
				}
			}
		}.SetHorizontalAlign(1f);
		string currentSeasonRewardIcon = PsMetagameManager.m_seasonEndData.GetCurrentSeasonRewardIcon();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(currentSeasonRewardIcon, null), true, true);
		uifittedSprite.SetHeight(0.035f, RelativeTo.ScreenHeight);
		int num = -1;
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < PsMetagameManager.m_seasonConfig.rewardLimits.Length; i++)
		{
			if (PsMetagameManager.m_seasonConfig.rewardLimits[i] > PsMetagameManager.m_team.teamScore)
			{
				break;
			}
			num = i;
		}
		if (num == PsMetagameManager.m_seasonConfig.rewardLimits.Length - 1)
		{
			flag = true;
		}
		else if (num < 0)
		{
			flag2 = true;
		}
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uicanvas, string.Empty);
		uihorizontalList2.SetVerticalAlign(0f);
		uihorizontalList2.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList2.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList2, string.Empty);
		uiverticalList.SetWidth(0.085f, RelativeTo.ScreenWidth);
		uiverticalList.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas4 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.SetHeight(0.025f, RelativeTo.ScreenHeight);
		uicanvas4.SetHorizontalAlign(0f);
		uicanvas4.RemoveDrawHandler();
		string text = string.Empty;
		if (!flag2)
		{
			text = PsMetagameManager.m_seasonConfig.rewardLimits[num].ToString();
		}
		else
		{
			text = "0";
		}
		UIFittedText uifittedText2 = new UIFittedText(uicanvas4, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FDE93B", null);
		uifittedText2.SetHorizontalAlign(0f);
		uifittedText2.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UICanvas uicanvas5 = new UICanvas(uicanvas4, false, string.Empty, null, string.Empty);
		uicanvas5.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas5.SetHorizontalAlign(0f);
		uicanvas5.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas5.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas5, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_small_full", null), true, true);
		uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
		UICanvas uicanvas6 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas6.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas6.SetHeight(0.025f, RelativeTo.ScreenHeight);
		uicanvas6.SetHorizontalAlign(0f);
		uicanvas6.RemoveDrawHandler();
		string text2 = string.Empty;
		if (!flag)
		{
			text2 = PsMetagameManager.m_seasonConfig.GetRewardAmounts()[num + 1].ToString();
		}
		else
		{
			text2 = PsMetagameManager.m_seasonConfig.GetFinalTierReward().ToString();
		}
		UIFittedText uifittedText3 = new UIFittedText(uicanvas6, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FDE93B", null);
		uifittedText3.SetHorizontalAlign(0f);
		uifittedText3.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UICanvas uicanvas7 = new UICanvas(uicanvas6, false, string.Empty, null, string.Empty);
		uicanvas7.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas7.SetHorizontalAlign(0f);
		uicanvas7.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas7.RemoveDrawHandler();
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas7, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(PsMetagameManager.m_seasonEndData.GetCurrentSeasonRewardIcon(), null), true, true);
		uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
		UICanvas uicanvas8 = new UICanvas(uihorizontalList2, false, string.Empty, null, string.Empty);
		uicanvas8.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicanvas8.SetWidth(0.16f, RelativeTo.ScreenWidth);
		uicanvas8.SetDrawHandler(new UIDrawDelegate(this.ProgressDrawhandler));
		uicanvas8.SetDepthOffset(-5f);
		UIVerticalList uiverticalList2 = new UIVerticalList(uihorizontalList2, string.Empty);
		uiverticalList2.SetWidth(0.085f, RelativeTo.ScreenWidth);
		uiverticalList2.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		UICanvas uicanvas9 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas9.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas9.SetHeight(0.025f, RelativeTo.ScreenHeight);
		uicanvas9.SetHorizontalAlign(0f);
		uicanvas9.RemoveDrawHandler();
		string text3 = string.Empty;
		if (!flag)
		{
			text3 = PsMetagameManager.m_seasonConfig.rewardLimits[num + 1].ToString();
		}
		else
		{
			text3 = PsMetagameManager.m_team.teamScore.ToString();
		}
		UIFittedText uifittedText4 = new UIFittedText(uicanvas9, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FDE93B", null);
		uifittedText4.SetHorizontalAlign(0f);
		uifittedText4.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UICanvas uicanvas10 = new UICanvas(uicanvas9, false, string.Empty, null, string.Empty);
		uicanvas10.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas10.SetHorizontalAlign(0f);
		uicanvas10.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas10.RemoveDrawHandler();
		UIFittedSprite uifittedSprite4 = new UIFittedSprite(uicanvas10, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_small_full", null), true, true);
		uifittedSprite4.SetHeight(1f, RelativeTo.ParentHeight);
		UICanvas uicanvas11 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas11.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas11.SetHeight(0.025f, RelativeTo.ScreenHeight);
		uicanvas11.SetHorizontalAlign(0f);
		uicanvas11.RemoveDrawHandler();
		if (!flag)
		{
			string text4 = string.Empty;
			if (num + 2 == PsMetagameManager.m_seasonConfig.GetRewardAmounts().Length)
			{
				text4 = PsMetagameManager.m_seasonConfig.GetFinalTierReward().ToString();
			}
			else
			{
				text4 = PsMetagameManager.m_seasonConfig.GetRewardAmounts()[num + 2].ToString();
			}
			UIFittedText uifittedText5 = new UIFittedText(uicanvas11, false, string.Empty, text4, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FDE93B", null);
			uifittedText5.SetHorizontalAlign(0f);
			uifittedText5.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			UICanvas uicanvas12 = new UICanvas(uicanvas11, false, string.Empty, null, string.Empty);
			uicanvas12.SetSize(1f, 1f, RelativeTo.ParentHeight);
			uicanvas12.SetHorizontalAlign(0f);
			uicanvas12.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas12.RemoveDrawHandler();
			UIFittedSprite uifittedSprite5 = new UIFittedSprite(uicanvas12, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(PsMetagameManager.m_seasonEndData.GetCurrentSeasonRewardIcon(), null), true, true);
			uifittedSprite5.SetHeight(1f, RelativeTo.ParentHeight);
		}
		UICanvas uicanvas13 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas13.SetWidth(0.2f, RelativeTo.ScreenHeight);
		uicanvas13.SetHeight(0.085f, RelativeTo.ScreenHeight);
		uicanvas13.SetVerticalAlign(0f);
		uicanvas13.SetMargins(0f, 0f, 0.105f, -0.105f, RelativeTo.ScreenHeight);
		uicanvas13.RemoveDrawHandler();
		UICanvas uicanvas14 = new UICanvas(uicanvas13, false, string.Empty, null, string.Empty);
		uicanvas14.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas14.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas14.SetMargins(0.01f, RelativeTo.ScreenHeight);
		uicanvas14.SetDrawHandler(new UIDrawDelegate(this.ScorePlateDrawhandler));
		UICanvas uicanvas15 = new UICanvas(uicanvas14, false, string.Empty, null, string.Empty);
		uicanvas15.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas15.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas15.SetVerticalAlign(1f);
		uicanvas15.RemoveDrawHandler();
		UIFittedText uifittedText6 = new UIFittedText(uicanvas15, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_TROPHIES).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#43330B", null);
		UICanvas uicanvas16 = new UICanvas(uicanvas14, false, string.Empty, null, string.Empty);
		uicanvas16.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas16.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas16.SetVerticalAlign(0f);
		uicanvas16.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas16.RemoveDrawHandler();
		UIFittedText uifittedText7 = new UIFittedText(uicanvas16, false, string.Empty, PsMetagameManager.m_team.teamScore.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFF792", null);
		UICanvas uicanvas17 = new UICanvas(uifittedText7, false, string.Empty, null, string.Empty);
		uicanvas17.SetSize(0.03f, 0.03f, RelativeTo.ScreenHeight);
		uicanvas17.SetHorizontalAlign(0f);
		uicanvas17.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas17.RemoveDrawHandler();
		UIFittedSprite uifittedSprite6 = new UIFittedSprite(uicanvas17, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true, true);
	}

	// Token: 0x06001952 RID: 6482 RVA: 0x00114E68 File Offset: 0x00113268
	private void ProgressDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = _c.m_actualWidth - 0.008f * (float)Screen.height;
		float num2 = _c.m_actualHeight - 0.008f * (float)Screen.height;
		int num3 = 0;
		int num4 = 0;
		for (int i = 0; i < PsMetagameManager.m_seasonConfig.rewardLimits.Length; i++)
		{
			if (PsMetagameManager.m_seasonConfig.rewardLimits[i] > PsMetagameManager.m_team.teamScore)
			{
				num4 = PsMetagameManager.m_seasonConfig.rewardLimits[i];
				if (i > 0)
				{
					num3 = PsMetagameManager.m_seasonConfig.rewardLimits[i - 1];
				}
				break;
			}
			if (i == PsMetagameManager.m_seasonConfig.rewardLimits.Length - 1)
			{
				num3 = PsMetagameManager.m_seasonConfig.rewardLimits[i];
				num4 = PsMetagameManager.m_team.teamScore;
			}
		}
		int num5 = PsMetagameManager.m_team.teamScore - num3;
		int num6 = num4 - num3;
		float num7 = (float)num5 / (float)num6;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.3f, 8, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(num, num2, _c.m_actualHeight * 0.2f, 8, Vector2.zero);
		Vector2[] roundedRect3 = DebugDraw.GetRoundedRect(num * num7, num2, _c.m_actualHeight * 0.2f, 8, new Vector2(num * num7 * 0.5f - 0.5f * num, 0f));
		Color color = DebugDraw.HexToColor("#0c61c9");
		Color color2 = DebugDraw.HexToColor("#4db9f5");
		color = DebugDraw.HexToColor("#C9C9C0");
		color2 = DebugDraw.HexToColor("#2D2C2C");
		Color white = Color.white;
		Color color3 = DebugDraw.HexToColor("#1B1B1B");
		Color color4 = DebugDraw.HexToColor("#353535");
		Color color5 = DebugDraw.HexToColor("#FEC90A");
		Color color6 = DebugDraw.HexToColor("#C68413");
		white.a = 0.4f;
		GGData ggdata = new GGData(roundedRect2);
		GGData ggdata2 = new GGData(roundedRect3);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, roundedRect, 0.2f * _c.m_actualHeight, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1.5f, roundedRect2, 0.15f * _c.m_actualHeight, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		if (num7 > 0f)
		{
			PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 1f, ggdata2, color6, color5, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f, roundedRect3, 0.15f * _c.m_actualHeight, color6, color5, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		}
		DebugDraw.ScaleVectorArray(roundedRect2, new Vector2(0.98f, 0.95f));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, roundedRect2, 0.2f * _c.m_actualHeight, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x001151DC File Offset: 0x001135DC
	private void ScorePlateDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.0075f * (float)Screen.height, 8, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#FD8008");
		Color color2 = DebugDraw.HexToColor("#FDA115");
		Color color3 = DebugDraw.HexToColor("#FED65D");
		Color color4 = DebugDraw.HexToColor("#C6640E");
		Color black = Color.black;
		black.a = 0.8f;
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, roundedRect, 0.0085f * (float)Screen.height, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward + Vector3.down * 0.01f * (float)Screen.height, roundedRect, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001954 RID: 6484 RVA: 0x0011531C File Offset: 0x0011371C
	private void SideDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Color color = DebugDraw.HexToColor("193254");
		color.a = 0.5f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 10f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001955 RID: 6485 RVA: 0x0011539C File Offset: 0x0011379C
	public override void Destroy()
	{
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
		}
		this.m_popup = null;
		(this.m_parent as UIScrollableCanvas).m_maxScrollInertialY = 50f / (1024f / (float)Screen.width);
		if (PsMetagameManager.m_loadingTeam)
		{
			PsMetagameManager.m_loadTeamCallback = (Action<TeamData>)Delegate.Remove(PsMetagameManager.m_loadTeamCallback, new Action<TeamData>(this.TeamGetOK));
		}
		base.Destroy();
	}

	// Token: 0x04001BD6 RID: 7126
	protected PsUIGenericButton m_settings;

	// Token: 0x04001BD7 RID: 7127
	protected PsUIGenericButton m_leave;

	// Token: 0x04001BD8 RID: 7128
	protected UIVerticalList m_sideList;

	// Token: 0x04001BD9 RID: 7129
	private UIVerticalList m_memberList;

	// Token: 0x04001BDA RID: 7130
	protected int m_currentIndex;

	// Token: 0x04001BDB RID: 7131
	protected bool m_searched;

	// Token: 0x04001BDC RID: 7132
	protected bool m_sideCreated;

	// Token: 0x04001BDD RID: 7133
	protected UIFittedText m_timeText;

	// Token: 0x04001BDE RID: 7134
	protected int m_currentTimeLeft;

	// Token: 0x04001BDF RID: 7135
	protected PsUIBasePopup m_popup;

	// Token: 0x04001BE0 RID: 7136
	public static bool m_updateMembers;
}
