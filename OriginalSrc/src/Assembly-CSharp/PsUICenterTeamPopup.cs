using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036D RID: 877
public class PsUICenterTeamPopup : PsUIHeaderedCanvas
{
	// Token: 0x0600197D RID: 6525 RVA: 0x00116F90 File Offset: 0x00115390
	public PsUICenterTeamPopup(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.25f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.SetWidth(0.8f, RelativeTo.ScreenWidth);
		this.SetHeight(0.9f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0.0125f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.065f, 0.065f, 0.0275f, 0f, RelativeTo.ScreenHeight);
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x00117098 File Offset: 0x00115498
	public void SetTeam(TeamData _team)
	{
		this.m_team = _team;
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x0600197F RID: 6527 RVA: 0x001170B4 File Offset: 0x001154B4
	public void GetTeam(string _id)
	{
		new PsUILoadingAnimation(this, false);
		HttpC team = PsMetagameManager.GetTeam(_id, new Action<TeamData>(this.StartTeamGetOK), new Action<HttpC>(this.TeamGetFAILED), new Action(this.TeamGetError));
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, team);
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x00117105 File Offset: 0x00115505
	public void StartTeamGetOK(TeamData _data)
	{
		Debug.Log("GET TEAM SUCCEED", null);
		this.m_team = _data;
		this.DestroyChildren();
		this.SetTeam(this.m_team);
		this.Update();
		this.m_header.Update();
	}

	// Token: 0x06001981 RID: 6529 RVA: 0x0011713C File Offset: 0x0011553C
	public void StartTeamGetFAILED(HttpC _c)
	{
		Debug.Log("GET TEAM FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC httpC = PsServerRequest.ServerGetTeam((string)_c.objectData, false, new Action<TeamData>(this.StartTeamGetOK), new Action<HttpC>(this.StartTeamGetFAILED), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
			return httpC;
		}, null);
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x00117190 File Offset: 0x00115590
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetSpacing(0.005f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetHorizontalAlign(0f);
		uiverticalList.SetVerticalAlign(1f);
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.07f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.4f, RelativeTo.ScreenWidth);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetHorizontalAlign(0f);
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, this.m_team.name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FDE53B", "#5F250C");
		uifittedText.SetHorizontalAlign(0f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas2.SetWidth(0.11f, RelativeTo.ScreenHeight);
		uicanvas2.SetMargins(0.04f, 0.01f, 0.005f, 0.005f, RelativeTo.ScreenHeight);
		uicanvas2.SetDrawHandler(new UIDrawDelegate(this.SpacerDrawhandler));
		UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, this.m_team.memberCount + "/50", PsFontManager.GetFont(PsFonts.KGSecondChances), true, "ffffff", "#011532");
		UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas3.SetSize(1.5f, 1.5f, RelativeTo.ParentHeight);
		uicanvas3.SetHorizontalAlign(0f);
		uicanvas3.SetMargins(-0.045f, 0.045f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas3.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_clan_members_icon", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		UICanvas uicanvas4 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas4.SetWidth(0.225f, RelativeTo.ScreenHeight);
		uicanvas4.SetMargins(0.06f, 0f, 0.005f, 0.005f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		UIFittedText uifittedText3 = new UIFittedText(uicanvas4, false, string.Empty, this.m_team.teamScore.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "ffffff", "#011532");
		uifittedText3.SetHorizontalAlign(0f);
		UICanvas uicanvas5 = new UICanvas(uicanvas4, false, string.Empty, null, string.Empty);
		uicanvas5.SetSize(1.5f, 1.5f, RelativeTo.ParentHeight);
		uicanvas5.SetHorizontalAlign(0f);
		uicanvas5.SetMargins(-0.045f, 0.045f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas5.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas5, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true, true);
		uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
		string text = string.Empty;
		if (!string.IsNullOrEmpty(this.m_team.description))
		{
			text = "\"" + this.m_team.description + "\"";
		}
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "#87F1FF", true, null);
		uitextbox.m_tmc.m_textMesh.fontStyle = 2;
		uitextbox.SetWidth(0.4f, RelativeTo.ScreenWidth);
		uitextbox.SetHeight(0.1f, RelativeTo.ScreenHeight);
		uitextbox.SetHorizontalAlign(0f);
		uitextbox.SetMaxRows(3);
		UIVerticalList uiverticalList2 = new UIVerticalList(_parent, string.Empty);
		uiverticalList2.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		uiverticalList2.SetVerticalAlign(1f);
		uiverticalList2.SetMargins(0f, 0.05f, 0.04f, 0f, RelativeTo.ScreenHeight);
		uiverticalList2.SetHorizontalAlign(1f);
		if (PlayerPrefsX.GetTeamId() == null && (PsMetagameManager.m_playerStats.carRank >= 2 || PsMetagameManager.m_playerStats.mcRank >= 2 || PlayerPrefsX.GetTeamUnlocked() || PlayerPrefsX.GetTeamJoined()))
		{
			this.m_join = new PsUIGenericButton(uiverticalList2, 0.25f, 0.25f, 0.005f, "Button");
			this.m_join.SetReleaseAction(new Action(this.JoinTeam));
			this.m_join.SetGreenColors(true);
			this.m_join.SetFittedText(PsStrings.Get(StringID.TEAM_JOIN), 0.045f, 0.265f, RelativeTo.ScreenHeight, true);
			this.m_join.SetHeight(0.1f, RelativeTo.ScreenHeight);
			this.m_join.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			bool flag = !PlayerPrefsX.GetTeamJoined();
			int num = -1;
			if (this.m_team.memberCount >= 50)
			{
				num = 2;
			}
			if ((this.m_team.joinType == JoinType.Closed || this.m_team.joinType == JoinType.FriendsOnly) && num < 0)
			{
				bool flag2 = false;
				if (this.m_team.joinType == JoinType.FriendsOnly)
				{
					for (int i = 0; i < this.m_team.memberIds.Length; i++)
					{
						if (PsMetagameManager.IsFriend(this.m_team.memberIds[i]))
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					num = 0;
				}
			}
			if (num < 0 && this.m_team.requiredTrophies > PsMetagameManager.m_playerStats.mcTrophies + PsMetagameManager.m_playerStats.carTrophies)
			{
				num = 1;
			}
			this.CreateLockText(uiverticalList2, num);
			if (flag && num < 0)
			{
				string text2 = PsStrings.Get(StringID.TEAMS_INSTANT_REWARD);
				text2 = text2.Replace("%1", 50.ToString());
				UIText uitext = new UIText(uiverticalList2, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, "#D92EFF", null);
				uitext.SetMargins(-0.015f, 0.015f, 0f, 0f, RelativeTo.ScreenHeight);
				UICanvas uicanvas6 = new UICanvas(uitext, false, string.Empty, null, string.Empty);
				uicanvas6.SetHeight(1f, RelativeTo.ParentHeight);
				uicanvas6.SetWidth(1f, RelativeTo.ParentHeight);
				uicanvas6.SetHorizontalAlign(1f);
				uicanvas6.SetMargins(0.0375f, -0.0375f, 0f, 0f, RelativeTo.ScreenHeight);
				uicanvas6.RemoveDrawHandler();
				UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas6, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_diamond_small_full", null), true, true);
				uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
			}
		}
		else
		{
			UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList2, string.Empty);
			uihorizontalList2.SetSpacing(0.02f, RelativeTo.ScreenHeight);
			uihorizontalList2.SetMargins(0.01f, RelativeTo.ScreenHeight);
			uihorizontalList2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
			uihorizontalList2.SetMargins(0.025f, 0.01f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			UICanvas uicanvas7 = new UICanvas(uihorizontalList2, false, string.Empty, null, string.Empty);
			uicanvas7.SetRogue();
			uicanvas7.SetSize(0.035f, 0.035f, RelativeTo.ScreenHeight);
			uicanvas7.SetAlign(0f, 1f);
			uicanvas7.SetMargins(-0.0315f, 0.0315f, -0.0225f, 0.0225f, RelativeTo.ScreenHeight);
			uicanvas7.SetDepthOffset(-5f);
			uicanvas7.RemoveDrawHandler();
			UIFittedSprite uifittedSprite4 = new UIFittedSprite(uicanvas7, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true, true);
			uifittedSprite4.SetHeight(1f, RelativeTo.ParentHeight);
			UIVerticalList uiverticalList3 = new UIVerticalList(uihorizontalList2, string.Empty);
			uiverticalList3.SetSpacing(0.015f, RelativeTo.ScreenHeight);
			uiverticalList3.RemoveDrawHandler();
			UICanvas uicanvas8 = new UICanvas(uiverticalList3, false, string.Empty, null, string.Empty);
			uicanvas8.SetHeight(0.025f, RelativeTo.ScreenHeight);
			uicanvas8.SetWidth(0.2f, RelativeTo.ScreenHeight);
			uicanvas8.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas8.RemoveDrawHandler();
			UIFittedText uifittedText4 = new UIFittedText(uicanvas8, false, string.Empty, ClientTools.GetTeamJoinTypeName(this.m_team.joinType), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#B9D7E9", null);
			uifittedText4.SetHorizontalAlign(0f);
			UICanvas uicanvas9 = new UICanvas(uicanvas8, false, string.Empty, null, string.Empty);
			uicanvas9.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uicanvas9.SetWidth(0.035f, RelativeTo.ScreenHeight);
			uicanvas9.SetHorizontalAlign(0f);
			uicanvas9.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas9.RemoveDrawHandler();
			UIFittedSprite uifittedSprite5 = new UIFittedSprite(uicanvas9, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_lock", null), true, true);
			uifittedSprite5.SetHeight(1f, RelativeTo.ParentHeight);
			UICanvas uicanvas10 = new UICanvas(uiverticalList3, false, string.Empty, null, string.Empty);
			uicanvas10.SetHeight(0.025f, RelativeTo.ScreenHeight);
			uicanvas10.SetWidth(0.2f, RelativeTo.ScreenHeight);
			uicanvas10.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas10.RemoveDrawHandler();
			string text3 = PsStrings.Get(StringID.TEAM_LIMIT_TROPHIES_REQUIRED);
			text3 = text3.Replace("%1", this.m_team.requiredTrophies + string.Empty);
			UIFittedText uifittedText5 = new UIFittedText(uicanvas10, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#B9D7E9", null);
			uifittedText5.SetHorizontalAlign(0f);
			UICanvas uicanvas11 = new UICanvas(uicanvas10, false, string.Empty, null, string.Empty);
			uicanvas11.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uicanvas11.SetWidth(0.035f, RelativeTo.ScreenHeight);
			uicanvas11.SetHorizontalAlign(0f);
			uicanvas11.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas11.RemoveDrawHandler();
			UIFittedSprite uifittedSprite6 = new UIFittedSprite(uicanvas11, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true, true);
			uifittedSprite6.SetHeight(1f, RelativeTo.ParentHeight);
		}
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x00117C14 File Offset: 0x00116014
	public void CreateLockText(UIComponent _parent, int _index)
	{
		if (_index >= 0)
		{
			UIText uitext = new UIText(_parent, false, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, "#F43C39", null);
			uitext.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
			UICanvas uicanvas = new UICanvas(uitext, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uicanvas.SetWidth(0.04f, RelativeTo.ScreenHeight);
			uicanvas.SetHorizontalAlign(0f);
			uicanvas.SetMargins(-0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(string.Empty, null), true, true);
			uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
			if (_index != 0)
			{
				if (_index != 1)
				{
					if (_index == 2)
					{
						uitext.SetText(PsStrings.Get(StringID.TEAM_LIMIT_FULL));
						uifittedSprite.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_lock", null));
					}
				}
				else
				{
					string text = PsStrings.Get(StringID.TEAM_LIMIT_TROPHIES_REQUIRED);
					text = text.Replace("%1", this.m_team.requiredTrophies.ToString());
					uitext.SetText(text);
					uifittedSprite.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null));
				}
			}
			else
			{
				string text2 = PsStrings.Get(StringID.TEAM_LIMIT_CLOSED);
				if (this.m_team.joinType == JoinType.FriendsOnly)
				{
					text2 = PsStrings.Get(StringID.TEAM_LIMIT_FRIENDS_ONLY);
				}
				uitext.SetText(text2);
				uifittedSprite.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_lock", null));
			}
			this.m_join.SetDarkGrayColors();
			this.m_join.SetFittedText(PsStrings.Get(StringID.TEAM_JOIN), 0.045f, 0.265f, RelativeTo.ScreenHeight, true);
			this.m_join.RemoveTouchAreas();
		}
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x00117E1C File Offset: 0x0011621C
	public void SpacerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] line = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f), new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f), 0);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, line, 0.005f * (float)Screen.height, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x00117EB0 File Offset: 0x001162B0
	public void CreateContent(UIComponent _parent)
	{
		UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(_parent, string.Empty);
		uiscrollableCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uiscrollableCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uiscrollableCanvas.RemoveDrawHandler();
		this.m_contents = new UIVerticalList(uiscrollableCanvas, string.Empty);
		this.m_contents.SetMargins(0.0125f, 0.0125f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		this.m_contents.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_contents.SetVerticalAlign(1f);
		this.m_contents.SetWidth(0.8f, RelativeTo.ScreenWidth);
		this.m_contents.RemoveDrawHandler();
		UIText uitext = new UIText(this.m_contents, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_MEMBERS), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, "#8FCCF3", null);
		uitext.SetMargins(0f, 0f, 0.03f, 0.05f, RelativeTo.ScreenHeight);
		if (this.m_team.memberList == null || this.m_team.memberCount != this.m_team.memberList.Length)
		{
			this.GetMembers();
		}
		else
		{
			this.m_searched = true;
			this.m_currentIndex = 0;
			this.m_contents.DestroyChildren(1);
		}
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x00117FF0 File Offset: 0x001163F0
	public void GetMembers()
	{
		this.m_searched = false;
		this.m_currentIndex = 0;
		this.m_contents.DestroyChildren(1);
		new PsUILoadingAnimation(this.m_contents, false);
		HttpC team = PsMetagameManager.GetTeam(this.m_team.id, new Action<TeamData>(this.TeamGetOK), new Action<HttpC>(this.TeamGetFAILED), new Action(this.TeamGetError));
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, team);
	}

	// Token: 0x06001987 RID: 6535 RVA: 0x0011806A File Offset: 0x0011646A
	public void TeamGetOK(TeamData _data)
	{
		Debug.Log("GET TEAM SUCCEED", null);
		this.m_team.CopyValuesFrom(_data);
		this.m_contents.DestroyChildren(1);
		this.m_searched = true;
	}

	// Token: 0x06001988 RID: 6536 RVA: 0x00118098 File Offset: 0x00116498
	public void TeamGetFAILED(HttpC _c)
	{
		Debug.Log("GET TEAM FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC httpC = PsServerRequest.ServerGetTeam((string)_c.objectData, false, new Action<TeamData>(this.TeamGetOK), new Action<HttpC>(this.TeamGetFAILED), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
			return httpC;
		}, null);
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x001180EB File Offset: 0x001164EB
	public void TeamGetError()
	{
		(this.GetRoot() as PsUIBasePopup).CallAction("Error");
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x00118104 File Offset: 0x00116504
	public void CreateBatch()
	{
		int num = Mathf.Min(this.m_currentIndex + 10, this.m_team.memberList.Length);
		for (int i = this.m_currentIndex; i < num; i++)
		{
			this.m_team.memberList[i].teamData = this.m_team;
			PsUITeamProfileBanner psUITeamProfileBanner = new PsUITeamProfileBanner(this.m_contents, i, this.m_team.memberList[i], false, true, true, true, false, false);
			psUITeamProfileBanner.Update();
			psUITeamProfileBanner.m_TC.transform.localScale = Vector3.one;
		}
		this.m_contents.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_contents.CalculateReferenceSizes();
		this.m_contents.UpdateSize();
		this.m_contents.ArrangeContents();
		this.m_contents.UpdateDimensions();
		this.m_contents.UpdateSize();
		this.m_contents.UpdateAlign();
		this.m_contents.UpdateChildrenAlign();
		this.m_contents.ArrangeContents();
		this.m_contents.m_parent.ArrangeContents();
		this.m_currentIndex = num;
	}

	// Token: 0x0600198B RID: 6539 RVA: 0x00118224 File Offset: 0x00116624
	public void JoinTeam()
	{
		if (PlayerPrefsX.GetTeamId() == null)
		{
			TouchAreaS.CancelAllTouches(null);
			HttpC httpC = PsMetagameManager.JoinTeam(this.m_team.id, new Action<TeamData>(this.JoinTeamOK), new Action<HttpC>(this.JoinTeamFAILED), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
			this.m_waitingPopup = new PsUIBasePopup(typeof(PsUICenterTeamPopup.PsUIPopupJoinWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
		}
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x0011829C File Offset: 0x0011669C
	public void JoinTeamOK(TeamData _data)
	{
		if (_data != null && !string.IsNullOrEmpty(_data.id))
		{
			Debug.Log("JOIN TEAM SUCCEED", null);
			if (!PlayerPrefsX.GetTeamJoined())
			{
				PsMetagameManager.m_playerStats.CumulateDiamonds(50);
			}
			PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
			PsMetagameManager.m_team = _data;
			PlayerPrefsX.SetTeamId(_data.id);
			PlayerPrefsX.SetTeamName(_data.name);
			PlayerPrefsX.SetTeamRole(_data.role);
			PlayerPrefsX.SetTeamJoined(true);
			PsMetrics.PlayerJoinedTeam(_data.id, _data.name);
			(this.GetRoot() as PsUIBasePopup).CallAction("Join");
		}
		else
		{
			Debug.Log("JOIN TEAM FAILED", null);
			(this.GetRoot() as PsUIBasePopup).CallAction("Error");
		}
		this.m_waitingPopup.Destroy();
		this.m_waitingPopup = null;
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x001183B4 File Offset: 0x001167B4
	public void JoinTeamFAILED(HttpC _c)
	{
		Debug.Log("JOIN TEAM FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerJoinTeam((string)_c.objectData, new Action<TeamData>(this.JoinTeamOK), new Action<HttpC>(this.JoinTeamFAILED), null), null);
	}

	// Token: 0x0600198E RID: 6542 RVA: 0x00118408 File Offset: 0x00116808
	public override void Step()
	{
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Tween, this.m_parent.m_TC.p_entity);
		if (componentsByEntity.Count == 0 && this.m_searched && this.m_currentIndex < this.m_team.memberCount && this.m_team.memberList != null)
		{
			this.CreateBatch();
		}
		base.Step();
	}

	// Token: 0x04001C01 RID: 7169
	private TeamData m_team;

	// Token: 0x04001C02 RID: 7170
	private PsUIGenericButton m_join;

	// Token: 0x04001C03 RID: 7171
	private UIVerticalList m_contents;

	// Token: 0x04001C04 RID: 7172
	private UIText m_memberCount;

	// Token: 0x04001C05 RID: 7173
	private int m_currentIndex;

	// Token: 0x04001C06 RID: 7174
	private bool m_searched;

	// Token: 0x04001C07 RID: 7175
	private bool m_startUpdate = true;

	// Token: 0x04001C08 RID: 7176
	private PsUIBasePopup m_waitingPopup;

	// Token: 0x0200036E RID: 878
	private class PsUIPopupWaiting : PsUIHeaderedCanvas
	{
		// Token: 0x0600198F RID: 6543 RVA: 0x00118478 File Offset: 0x00116878
		public PsUIPopupWaiting(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
			this.SetWidth(0.65f, RelativeTo.ScreenWidth);
			this.SetHeight(0.45f, RelativeTo.ScreenHeight);
			this.SetVerticalAlign(0.4f);
			this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.Destroy();
		}
	}

	// Token: 0x0200036F RID: 879
	private class PsUIPopupJoinWaiting : PsUICenterTeamPopup.PsUIPopupWaiting
	{
		// Token: 0x06001990 RID: 6544 RVA: 0x0011853C File Offset: 0x0011693C
		public PsUIPopupJoinWaiting(UIComponent _parent)
			: base(_parent)
		{
			UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
			uiverticalList.RemoveDrawHandler();
			uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
			new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.TEAM_JOIN_MESSAGE), PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			new PsUILoadingAnimation(uiverticalList, false);
		}
	}
}
