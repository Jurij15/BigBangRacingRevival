using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037C RID: 892
public class PsUITeamArea : UIVerticalList
{
	// Token: 0x060019CD RID: 6605 RVA: 0x0011B908 File Offset: 0x00119D08
	public PsUITeamArea(UIComponent _parent, bool _search = false, bool _teamUp = false, bool _topTeams = false, TeamData[] _preloadedTeams = null, bool _sRewards = false, Action _getCallback = null)
		: base(_parent, "teamArea")
	{
		this.m_sRewards = _sRewards;
		this.m_search = _search;
		this.m_teamUp = _teamUp;
		this.m_topTeams = _topTeams;
		this.m_foundTeams = _preloadedTeams;
		this.m_getCallback = _getCallback;
		if (this.m_sRewards)
		{
			this.SetHorizontalAlign(0f);
		}
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.SetMargins(0f, 0.03f, 0f, 0f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		if (this.m_foundTeams != null)
		{
			this.m_found = true;
		}
		else if (!this.m_search)
		{
			this.GetTeamsFromServer();
		}
	}

	// Token: 0x060019CE RID: 6606 RVA: 0x0011B9D4 File Offset: 0x00119DD4
	public void CreateLoadingArea()
	{
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.33f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetMargins(0.035f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		PsUILoadingAnimation psUILoadingAnimation = new PsUILoadingAnimation(uicanvas, false);
		psUILoadingAnimation.SetVerticalAlign(1f);
	}

	// Token: 0x060019CF RID: 6607 RVA: 0x0011BA34 File Offset: 0x00119E34
	public void CreateBanner(TeamData _team, int _index, bool _update = false)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetHeight(0.05f, RelativeTo.ScreenHeight);
		if (_team.id == PlayerPrefsX.GetTeamId())
		{
			uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.OwnProfileBannerDrawhandler));
		}
		else if (this.IsInactive(_team))
		{
			uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ProfileBannerInactiveDrawhandler));
		}
		else
		{
			uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ProfileBannerDrawhandler));
		}
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.CreateTouchAreas();
		uihorizontalList.m_TAC.m_letTouchesThrough = true;
		if (this.m_sRewards)
		{
			UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.04f, RelativeTo.ScreenHeight);
			uicanvas.SetWidth(0.04f, RelativeTo.ScreenWidth);
			uicanvas.SetMargins(0f, 0.01f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, _index + 1 + ".", PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#A6DAFE", null);
			uifittedText.SetHorizontalAlign(1f);
		}
		UICanvas uicanvas2 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(0.35f, RelativeTo.ScreenWidth);
		uicanvas2.RemoveDrawHandler();
		string text = ((!this.IsInactive(_team)) ? "#FBE139" : "#DBE0E6");
		UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, _team.name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, text, "#4D2121");
		uifittedText2.SetHorizontalAlign(0f);
		UICanvas uicanvas3 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(0.095f, RelativeTo.ScreenWidth);
		uicanvas3.SetMargins(0.05f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas3.SetDrawHandler(new UIDrawDelegate(this.SpacerDrawhandler));
		UIFittedText uifittedText3 = new UIFittedText(uicanvas3, false, string.Empty, _team.memberCount.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#ffffff", null);
		UICanvas uicanvas4 = new UICanvas(uicanvas3, false, string.Empty, null, string.Empty);
		uicanvas4.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicanvas4.SetHorizontalAlign(0f);
		uicanvas4.SetMargins(-0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_clan_members_icon", null), true, true);
		uifittedSprite.SetHeight(0.045f, RelativeTo.ScreenHeight);
		UICanvas uicanvas5 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas5.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas5.SetWidth(0.205f, RelativeTo.ScreenWidth);
		uicanvas5.SetMargins(0.06f, 0.03f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas5.RemoveDrawHandler();
		UIFittedText uifittedText4 = new UIFittedText(uicanvas5, false, string.Empty, _team.teamScore.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#FDE53B", null);
		uifittedText4.SetHorizontalAlign(1f);
		UICanvas uicanvas6 = new UICanvas(uicanvas5, false, string.Empty, null, string.Empty);
		uicanvas6.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicanvas6.SetWidth(0.06f, RelativeTo.ScreenHeight);
		uicanvas6.SetHorizontalAlign(0f);
		uicanvas6.SetMargins(-0.06f, 0.06f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas6.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas6, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_small_full", null), true, true);
		uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
		TransformS.SetRotation(uifittedSprite2.m_TC, new Vector3(0f, 0f, 5f));
		if (this.m_sRewards && _team.teamSeasonReward > 0)
		{
			uihorizontalList.SetHorizontalAlign(0f);
			UICanvas uicanvas7 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas7.SetRogue();
			uicanvas7.SetHorizontalAlign(1f);
			uicanvas7.SetHeight(0.03f, RelativeTo.ScreenHeight);
			uicanvas7.SetWidth(0.05f, RelativeTo.ScreenWidth);
			uicanvas7.SetMargins(0.125f, -0.125f, 0f, 0f, RelativeTo.ScreenWidth);
			uicanvas7.RemoveDrawHandler();
			UIFittedText uifittedText5 = new UIFittedText(uicanvas7, false, string.Empty, _team.teamSeasonReward.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#ffffff", null);
			uifittedText5.SetHorizontalAlign(0f);
			UICanvas uicanvas8 = new UICanvas(uifittedText5, false, string.Empty, null, string.Empty);
			uicanvas8.SetHeight(0.04f, RelativeTo.ScreenHeight);
			uicanvas8.SetWidth(0.04f, RelativeTo.ScreenHeight);
			uicanvas8.SetHorizontalAlign(0f);
			uicanvas8.SetMargins(-0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas8.RemoveDrawHandler();
			string currentSeasonRewardIcon = PsMetagameManager.m_seasonEndData.GetCurrentSeasonRewardIcon();
			UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas8, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(currentSeasonRewardIcon, null), true, true);
			uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
		}
		else if (this.m_sRewards)
		{
			uihorizontalList.SetHorizontalAlign(0f);
		}
		this.m_teams.Add(uihorizontalList);
		if (_update)
		{
			uihorizontalList.Update();
		}
	}

	// Token: 0x060019D0 RID: 6608 RVA: 0x0011C01C File Offset: 0x0011A41C
	private bool IsInactive(TeamData _team)
	{
		bool flag = false;
		if (_team.id != PlayerPrefsX.GetTeamId())
		{
			if (_team.memberCount >= 50)
			{
				flag = true;
			}
			else if (_team.joinType == JoinType.Closed)
			{
				flag = true;
			}
			else if (_team.requiredTrophies > PsMetagameManager.m_playerStats.mcTrophies + PsMetagameManager.m_playerStats.carTrophies)
			{
				flag = true;
			}
			else if (_team.joinType == JoinType.FriendsOnly)
			{
				bool flag2 = false;
				for (int i = 0; i < _team.memberIds.Length; i++)
				{
					if (PsMetagameManager.IsFriend(_team.memberIds[i]) || PsMetagameManager.IsFollowee(_team.memberIds[i]))
					{
						flag2 = true;
						break;
					}
				}
				flag = !flag2;
			}
		}
		return flag;
	}

	// Token: 0x060019D1 RID: 6609 RVA: 0x0011C0E8 File Offset: 0x0011A4E8
	public void SpacerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] line = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f), new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f), 0);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, line, 0.005f * (float)Screen.height, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x060019D2 RID: 6610 RVA: 0x0011C17C File Offset: 0x0011A57C
	public void GetTeamsFromServer()
	{
		this.m_found = false;
		this.m_foundTeams = null;
		this.m_currentIndex = 0;
		this.DestroyChildren(0);
		this.CreateLoadingArea();
		if (this.m_teamUp)
		{
			this.GetSuggestions();
		}
		else if (this.m_topTeams)
		{
			this.GetTopTeams();
		}
	}

	// Token: 0x060019D3 RID: 6611 RVA: 0x0011C1D4 File Offset: 0x0011A5D4
	public void GetSuggestions()
	{
		HttpC teamSuggestions = PsMetagameManager.GetTeamSuggestions(new Action<TeamData[]>(this.GetTeamsOK), new Action<HttpC>(this.GetTeamsFAILED), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, teamSuggestions);
	}

	// Token: 0x060019D4 RID: 6612 RVA: 0x0011C214 File Offset: 0x0011A614
	public void GetTopTeams()
	{
		HttpC teamLeaderboards = PsMetagameManager.GetTeamLeaderboards(new Action<TeamData[]>(this.GetTeamsOK), new Action<HttpC>(this.GetTeamsFAILED), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, teamLeaderboards);
	}

	// Token: 0x060019D5 RID: 6613 RVA: 0x0011C254 File Offset: 0x0011A654
	public void Search(string _input)
	{
		this.m_searchInput = _input;
		this.m_found = false;
		this.m_foundTeams = null;
		this.m_currentIndex = 0;
		this.DestroyChildren(0);
		this.m_teams.Clear();
		this.CreateLoadingArea();
		this.Update();
		this.m_parent.ArrangeContents();
		HttpC httpC = PsMetagameManager.SearchTeams(this.m_searchInput, new Action<TeamData[]>(this.GetTeamsOK), new Action<HttpC>(this.GetTeamsFAILED), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
	}

	// Token: 0x060019D6 RID: 6614 RVA: 0x0011C2DC File Offset: 0x0011A6DC
	public void GetTeamsOK(TeamData[] _data)
	{
		this.m_found = true;
		this.m_foundTeams = _data;
		if (this.m_foundTeams == null)
		{
			this.m_foundTeams = new TeamData[0];
		}
		if (this.m_search)
		{
			this.m_searchInput = string.Empty;
			if (this.m_parent is PsUICenterSearchTeams)
			{
				(this.m_parent as PsUICenterSearchTeams).SearchFinished();
			}
		}
	}

	// Token: 0x060019D7 RID: 6615 RVA: 0x0011C344 File Offset: 0x0011A744
	public void GetTeamsFAILED(HttpC _c)
	{
		Debug.Log("GET TEAMS FAILED", null);
		if (this.m_teamUp)
		{
			ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
			{
				HttpC httpC = PsServerRequest.ServerGetTeamSuggestions(new Action<TeamData[]>(this.GetTeamsOK), new Action<HttpC>(this.GetTeamsFAILED), null);
				EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
				return httpC;
			}, null);
		}
		else if (this.m_topTeams)
		{
			ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
			{
				HttpC httpC2 = PsServerRequest.ServerGetTeamLeaderboard(new Action<TeamData[]>(this.GetTeamsOK), new Action<HttpC>(this.GetTeamsFAILED), null);
				EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC2);
				return httpC2;
			}, null);
		}
		else if (this.m_search)
		{
			string networkError = ServerErrors.GetNetworkError(_c.www.error);
			ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, delegate
			{
				HttpC httpC3 = PsMetagameManager.SearchTeams(this.m_searchInput, new Action<TeamData[]>(this.GetTeamsOK), new Action<HttpC>(this.GetTeamsFAILED), null);
				EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC3);
				return httpC3;
			}, null, StringID.TRY_AGAIN_SERVER);
		}
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x0011C400 File Offset: 0x0011A800
	public void CreateBatch()
	{
		if (this.m_found)
		{
			if (this.m_getCallback != null)
			{
				this.m_getCallback.Invoke();
			}
			this.DestroyChildren(0);
			this.m_found = false;
		}
		if (this.m_currentIndex == 0 && this.m_sRewards)
		{
			this.CreateRewardPlate();
		}
		int num = Mathf.Min(this.m_currentIndex + 10, this.m_foundTeams.Length);
		for (int i = this.m_currentIndex; i < num; i++)
		{
			this.CreateBanner(this.m_foundTeams[i], i, true);
		}
		if (this.m_teamUp && this.m_foundTeams.Length < 5)
		{
			float num2 = (float)(5 - this.m_foundTeams.Length) * 0.07f - 0.02f;
			UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(num2, RelativeTo.ScreenHeight);
			uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas.SetMargins(0.035f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			uicanvas.Update();
		}
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.ArrangeContents();
		base.UpdateDimensions();
		this.UpdateSize();
		this.UpdateAlign();
		this.UpdateChildrenAlign();
		this.ArrangeContents();
		this.m_parent.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_parent.CalculateReferenceSizes();
		this.m_parent.UpdateSize();
		this.m_parent.ArrangeContents();
		(this.m_parent as UIVerticalList).UpdateDimensions();
		this.m_parent.UpdateSize();
		this.m_parent.UpdateAlign();
		this.m_parent.UpdateChildrenAlign();
		this.m_parent.ArrangeContents();
		this.m_parent.m_parent.ArrangeContents();
		this.m_currentIndex = num;
	}

	// Token: 0x060019D9 RID: 6617 RVA: 0x0011C5C8 File Offset: 0x0011A9C8
	public void CreateRewardPlate()
	{
		UICanvas uicanvas = new UICanvas(this, false, "rewardPlate", null, string.Empty);
		uicanvas.SetWidth(0.11f, RelativeTo.ScreenWidth);
		uicanvas.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetRogue();
		uicanvas.SetAlign(1f, 1f);
		float num = (float)Screen.width / (float)Screen.height;
		float num2 = 0f;
		if (num > 1.35f)
		{
			num2 = (num - 1f) / 11f;
		}
		uicanvas.SetMargins(0f, num2, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(0.11f, RelativeTo.ScreenWidth);
		float num3 = (float)this.m_foundTeams.Length * 0.07f + 0.035f;
		uicanvas2.SetHeight(num3, RelativeTo.ScreenHeight);
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
		uicanvas4.SetMargins(0.01f, 0.01f, 0.015f, 0.015f, RelativeTo.ScreenHeight);
		uicanvas4.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SeasonTopRewardDrawhandler));
		string currentSeasonRewardIcon = PsMetagameManager.m_seasonEndData.GetCurrentSeasonRewardIcon();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(currentSeasonRewardIcon, null), true, true);
		uifittedSprite.SetHeight(0.06f, RelativeTo.ScreenHeight);
		uicanvas.Update();
	}

	// Token: 0x060019DA RID: 6618 RVA: 0x0011C7F4 File Offset: 0x0011ABF4
	public override void Step()
	{
		if (this.m_foundTeams != null && (this.m_currentIndex < this.m_foundTeams.Length || this.m_found))
		{
			this.CreateBatch();
		}
		int i;
		for (i = 0; i < this.m_teams.Count; i++)
		{
			if (this.m_teams[i].m_hit && this.m_popup == null)
			{
				SoundS.PlaySingleShot("/UI/Popup", Vector3.zero, 1f);
				this.m_popup = new PsUIBasePopup(typeof(PsUICenterTeamPopup), null, null, null, false, true, InitialPage.Center, false, false, false);
				(this.m_popup.m_mainContent as PsUICenterTeamPopup).SetTeam(this.m_foundTeams[i]);
				this.m_popup.SetAction("Exit", delegate
				{
					this.m_popup.Destroy();
					this.m_popup = null;
				});
				this.m_popup.SetAction("Error", delegate
				{
					this.m_popup.Destroy();
					this.m_popup = null;
					this.RemoveTeam(i);
					ServerManager.m_dontShowLoginPopup = false;
				});
				this.m_popup.SetAction("Join", delegate
				{
					PsUITabbedTeam.m_selectedTab = 1;
					PsMainMenuState.ChangeToTeamState();
				});
				this.m_popup.Update();
				TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
				break;
			}
		}
		base.Step();
	}

	// Token: 0x060019DB RID: 6619 RVA: 0x0011C9A0 File Offset: 0x0011ADA0
	public void RemoveTeam(int _index)
	{
		this.m_foundTeams[_index] = null;
		TeamData[] array = new TeamData[this.m_foundTeams.Length - 1];
		int num = 0;
		for (int i = 0; i < this.m_foundTeams.Length; i++)
		{
			if (this.m_foundTeams[i] != null)
			{
				array[num] = this.m_foundTeams[i];
				num++;
			}
		}
		this.m_foundTeams = array;
		this.m_teams[_index].Destroy();
		this.m_teams.RemoveAt(_index);
		if (this.m_teamUp)
		{
			UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.05f, RelativeTo.ScreenHeight);
			uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas.RemoveDrawHandler();
			uicanvas.Update();
		}
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.ArrangeContents();
		base.UpdateDimensions();
		this.UpdateSize();
		this.UpdateAlign();
		this.UpdateChildrenAlign();
		this.ArrangeContents();
		this.m_parent.ArrangeContents();
	}

	// Token: 0x060019DC RID: 6620 RVA: 0x0011CAAA File Offset: 0x0011AEAA
	public override void Destroy()
	{
		if (this.m_popup != null && this.m_popup.m_TC.p_entity != null)
		{
			this.m_popup.Destroy();
		}
		this.m_popup = null;
		base.Destroy();
	}

	// Token: 0x04001C35 RID: 7221
	private bool m_search;

	// Token: 0x04001C36 RID: 7222
	private bool m_teamUp;

	// Token: 0x04001C37 RID: 7223
	private bool m_topTeams;

	// Token: 0x04001C38 RID: 7224
	private TeamData[] m_foundTeams;

	// Token: 0x04001C39 RID: 7225
	private List<UIHorizontalList> m_teams = new List<UIHorizontalList>();

	// Token: 0x04001C3A RID: 7226
	private int m_currentIndex;

	// Token: 0x04001C3B RID: 7227
	private bool m_found;

	// Token: 0x04001C3C RID: 7228
	private bool m_sRewards;

	// Token: 0x04001C3D RID: 7229
	private string m_searchInput;

	// Token: 0x04001C3E RID: 7230
	private Action m_getCallback;

	// Token: 0x04001C3F RID: 7231
	private PsUIBasePopup m_popup;
}
