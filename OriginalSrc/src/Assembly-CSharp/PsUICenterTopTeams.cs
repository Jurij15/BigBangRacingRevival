using System;
using System.Collections.Generic;
using Server;

// Token: 0x02000375 RID: 885
public class PsUICenterTopTeams : UIVerticalList
{
	// Token: 0x060019A2 RID: 6562 RVA: 0x001195DC File Offset: 0x001179DC
	public PsUICenterTopTeams(UIComponent _parent)
		: base(_parent, string.Empty)
	{
		PsUITabbedTeam.m_selectedTab = 3;
		(this.m_parent as UIScrollableCanvas).SetScrollPosition(0f, 0f);
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetMargins(0.115f, 0f, 0.06f, 0.08f, RelativeTo.ScreenHeight);
		this.SetSpacing(0f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(1f);
		this.RemoveDrawHandler();
		this.m_seasonInfo = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_seasonInfo.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_seasonInfo.SetMargins(0f, 0.19f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_seasonInfo.SetHeight(0.3f, RelativeTo.ScreenHeight);
		this.m_seasonInfo.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_seasonInfo, string.Empty);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.OwnWidth);
		uihorizontalList.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(0.5f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.TEAM_SEASON_INFO_TEXT), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetRogue();
		uicanvas.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(0f, 1f);
		uicanvas.SetMargins(-0.07f, 0.07f, -0.04f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		this.m_infoButton = new UIRectSpriteButton(uicanvas, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true, false);
		this.m_infoButton.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_rightContainer = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		this.m_rightContainer.SetWidth(0.45f, RelativeTo.ParentWidth);
		this.m_rightContainer.SetMargins(0f, 0f, 0.05f, 0f, RelativeTo.ScreenHeight);
		this.m_rightContainer.SetVerticalAlign(1f);
		this.m_rightContainer.RemoveDrawHandler();
		this.CreateSeasonTop();
		PsUITeamArea psUITeamArea = new PsUITeamArea(this, false, false, true, null, true, new Action(this.CreateSeasonInfo));
	}

	// Token: 0x060019A3 RID: 6563 RVA: 0x001198B0 File Offset: 0x00117CB0
	private void CreateSeasonTop()
	{
		HttpC previousSeasons = global::Server.Season.GetPreviousSeasons(PsMetagameManager.m_seasonEndData.currentSeason.number, 10, new Action<Dictionary<int, global::Server.Season.SeasonTop>>(this.PreviousSeasonsOK), new Action<HttpC>(this.PreviousSeasonsFailed), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, previousSeasons);
	}

	// Token: 0x060019A4 RID: 6564 RVA: 0x00119900 File Offset: 0x00117D00
	private void PreviousSeasonsOK(Dictionary<int, global::Server.Season.SeasonTop> _seasons)
	{
		if (_seasons.Count > 0)
		{
			int num = PsMetagameManager.m_seasonEndData.currentSeason.number - 1;
			PsUISeasonTopTeamsBanner psUISeasonTopTeamsBanner = new PsUISeasonTopTeamsBanner(this.m_rightContainer, _seasons, PsStrings.Get(StringID.LEADERBOARD_TOP_3_TEAMS), num);
			this.m_rightContainer.m_parent.Update();
		}
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x00119953 File Offset: 0x00117D53
	private void PreviousSeasonsFailed(HttpC _httpc)
	{
		Debug.LogError("fail");
	}

	// Token: 0x060019A6 RID: 6566 RVA: 0x00119960 File Offset: 0x00117D60
	public void CreateSeasonInfo()
	{
		UICanvas uicanvas = new UICanvas(this.m_seasonInfo, false, string.Empty, null, string.Empty);
		uicanvas.SetMargins(0.05f, 0f, 0f, 0f, RelativeTo.ScreenWidth);
		uicanvas.SetHeight(0.09f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(0f, 0f);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas2.SetWidth(0.335f, RelativeTo.ScreenWidth);
		uicanvas2.SetMargins(0.05f, 0.05f, 0.005f, 0.0125f, RelativeTo.ScreenHeight);
		uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SeasonTopDrawhandler));
		uicanvas2.SetVerticalAlign(0f);
		uicanvas2.SetHorizontalAlign(0f);
		uicanvas2.SetDepthOffset(5f);
		UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetVerticalAlign(1f);
		uicanvas3.RemoveDrawHandler();
		uicanvas3.SetMargins(0.0015f, RelativeTo.ScreenHeight);
		UIFittedText uifittedText = new UIFittedText(uicanvas3, false, string.Empty, PsStrings.Get(StringID.TEAM_CURRENT_SEASON_HEADER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FCF675", null);
		UICanvas uicanvas4 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.SetVerticalAlign(0f);
		uicanvas4.RemoveDrawHandler();
		uicanvas4.SetMargins(0.002f, RelativeTo.ScreenHeight);
		this.m_currentTimeLeft = PsMetagameManager.m_seasonTimeleft;
		string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(PsMetagameManager.m_seasonTimeleft);
		string text = PsStrings.Get(StringID.TEAM_SEASON_END_TIMER);
		text = text.Replace("%1", timeStringFromSeconds);
		this.m_timeText = new UIFittedText(uicanvas4, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#423312", null);
		uicanvas.Update();
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x00119B68 File Offset: 0x00117F68
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
		if (this.m_infoButton.m_hit && this.m_infoPopup == null)
		{
			string text2 = PsStrings.Get(StringID.LEADERBOARD_INFO_TOP_TEAMS);
			string text3 = PsStrings.Get(StringID.LEADERBOARD_INFO_TEXT_TEAMS);
			this.m_infoPopup = PsUIPopupInfoText.Create(text2, text3, delegate
			{
				this.m_infoPopup.Destroy();
				this.m_infoPopup = null;
			});
		}
		base.Step();
	}

	// Token: 0x04001C1A RID: 7194
	private UICanvas m_seasonInfo;

	// Token: 0x04001C1B RID: 7195
	private UICanvas m_rightContainer;

	// Token: 0x04001C1C RID: 7196
	protected UIFittedText m_timeText;

	// Token: 0x04001C1D RID: 7197
	protected int m_currentTimeLeft;

	// Token: 0x04001C1E RID: 7198
	private UIRectSpriteButton m_infoButton;

	// Token: 0x04001C1F RID: 7199
	private PsUIBasePopup m_infoPopup;
}
