using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A1 RID: 929
public class PsUICenterLeagues : UICanvas
{
	// Token: 0x06001A80 RID: 6784 RVA: 0x00127644 File Offset: 0x00125A44
	public PsUICenterLeagues(UIComponent _parent)
		: base(_parent, false, "LeaguePopup", null, string.Empty)
	{
		this.m_currentRank = PsMetagameData.GetCurrentLeagueIndex();
		this.RemoveDrawHandler();
		this.m_leagueInfos = new List<PsUILeagueInfo>();
		this.CreateLeftArea(this);
		UIComponent uicomponent = new UIComponent(this, false, string.Empty, null, null, string.Empty);
		uicomponent.RemoveDrawHandler();
		uicomponent.SetWidth(0.75f, RelativeTo.ParentWidth);
		uicomponent.SetHeight(1f, RelativeTo.ParentHeight);
		uicomponent.SetMargins(0.1f, 0.1f, 0f, 0f, RelativeTo.ScreenHeight);
		uicomponent.SetVerticalAlign(0f);
		uicomponent.SetHorizontalAlign(1f);
		this.m_highlightUI = new UIComponent(uicomponent, false, "HighLightUI", null, null, string.Empty);
		this.m_highlightUI.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.LeagueBackgroundHighlight));
		this.m_highlightUI.SetDepthOffset(-5f);
		UIComponent uicomponent2 = new UIComponent(uicomponent, false, string.Empty, null, null, string.Empty);
		uicomponent2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TransparentSmoothEdges));
		this.m_scrollArea = new UIScrollableCanvas(uicomponent2, "LeagueScrollArea");
		this.m_scrollArea.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_scrollArea.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_scrollArea.RemoveDrawHandler();
		this.m_leagueList = new UIVerticalList(this.m_scrollArea, string.Empty);
		this.m_leagueList.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_leagueList.SetMargins(0f, 0f, 0.1f, 0.25f, RelativeTo.ScreenHeight);
		this.m_leagueList.RemoveDrawHandler();
		for (int i = 0; i < PsMetagameData.m_leagueData.Count; i++)
		{
			PsUILeagueInfo psUILeagueInfo = this.CreateLeagueInfo(this.m_leagueList, i);
			if (i == this.m_currentRank)
			{
				this.m_currentLeagueInfo = psUILeagueInfo;
			}
			this.m_leagueInfos.Add(psUILeagueInfo);
		}
		this.SetScrollPosition();
		TransformS.SetPosition(this.m_highlightUI.m_TC, this.m_currentLeagueInfo.m_TC.transform.position);
	}

	// Token: 0x06001A81 RID: 6785 RVA: 0x00127878 File Offset: 0x00125C78
	public override void Step()
	{
		if (this.m_leaderboardButton != null && this.m_leaderboardButton.m_hit)
		{
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterLeaderboard), typeof(PsUITopBackButton), null, null, true, true, InitialPage.Center, false, false, false);
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
			});
		}
		else
		{
			foreach (PsUILeagueInfo psUILeagueInfo in this.m_leagueInfos)
			{
				psUILeagueInfo.CheckTouch();
			}
		}
		base.Step();
		this.HighlightStep();
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x00127948 File Offset: 0x00125D48
	protected virtual void HighlightStep()
	{
		float y = this.m_currentLeagueInfo.m_TC.transform.localPosition.y;
		this.m_highlightUI.m_TC.transform.position = new Vector3(this.m_highlightUI.m_TC.transform.position.x, this.m_scrollArea.m_scrollTC.transform.position.y * -1f + y, -5f);
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x001279D4 File Offset: 0x00125DD4
	public override void Update()
	{
		base.Update();
		this.CustomUpdate();
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x001279E2 File Offset: 0x00125DE2
	protected virtual void CustomUpdate()
	{
		this.UpdateHighlightSize(false);
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x001279EC File Offset: 0x00125DEC
	protected virtual void UpdateHighlightSize(bool _previous = false)
	{
		if (_previous)
		{
			PsUILeagueInfo psUILeagueInfo = this.m_leagueInfos[this.m_currentRank - 1];
			this.m_highlightUI.SetHeight(psUILeagueInfo.m_actualHeight / (float)Screen.height, RelativeTo.ScreenHeight);
		}
		else
		{
			this.m_highlightUI.SetHeight(this.m_currentLeagueInfo.m_actualHeight / (float)Screen.height, RelativeTo.ScreenHeight);
		}
		this.m_highlightUI.Update();
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x00127A5A File Offset: 0x00125E5A
	protected virtual void SetScrollPosition()
	{
		this.m_scrollArea.SetScrollPositionToChild(this.m_currentLeagueInfo);
	}

	// Token: 0x06001A87 RID: 6791 RVA: 0x00127A6D File Offset: 0x00125E6D
	protected virtual PsUILeagueInfo CreateLeagueInfo(UIComponent _parent, int _index)
	{
		return new PsUILeagueInfo(_parent, _index, false);
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x00127A78 File Offset: 0x00125E78
	public void CreateLeftArea(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, "leftArea");
		uiverticalList.SetHorizontalAlign(0f);
		uiverticalList.SetWidth(0.25f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0f, -0.1f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(0.5f);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uiverticalList, true, "ProfileImage", PlayerPrefsX.GetFacebookId(), PlayerPrefsX.GetGameCenterId(), -1, true, false, true, 0.075f, 0.06f, "fff9e6", null, true, true);
		psUIProfileImage.SetSize(0.185f, 0.185f, RelativeTo.ScreenHeight);
		psUIProfileImage.SetDepthOffset(10f);
		psUIProfileImage.SetMargins(0.1f, 0.1f, 0.1f, 0.1f, RelativeTo.ScreenHeight);
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.2f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PlayerPrefsX.GetUserName(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#b1f92a", "000000");
		int currentLeagueIndex = PsMetagameData.GetCurrentLeagueIndex();
		this.m_playerLeagueBanner = new UIFittedSprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(PsMetagameData.m_leagueData[currentLeagueIndex].m_bannerSprite, null), true, true);
		this.m_playerLeagueBanner.SetHeight(0.11f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_playerLeagueBanner, string.Empty);
		uihorizontalList.SetVerticalAlign(0.865f);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetDepthOffset(-3f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, " " + PsMetagameManager.m_playerStats.trophies.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0565f, RelativeTo.ScreenHeight, "#FFFC42", "#313131");
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true, true);
		uifittedSprite.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
		uifittedSprite.SetHeight(0.06f, RelativeTo.ScreenHeight);
	}

	// Token: 0x04001D04 RID: 7428
	protected UIScrollableCanvas m_scrollArea;

	// Token: 0x04001D05 RID: 7429
	private UIVerticalList m_leagueList;

	// Token: 0x04001D06 RID: 7430
	protected int m_currentRank;

	// Token: 0x04001D07 RID: 7431
	protected PsUILeagueInfo m_currentLeagueInfo;

	// Token: 0x04001D08 RID: 7432
	protected List<PsUILeagueInfo> m_leagueInfos;

	// Token: 0x04001D09 RID: 7433
	protected UIFittedSprite m_playerLeagueBanner;

	// Token: 0x04001D0A RID: 7434
	private const float m_scrollareaWidth = 0.75f;

	// Token: 0x04001D0B RID: 7435
	protected UIComponent m_highlightUI;

	// Token: 0x04001D0C RID: 7436
	private PsUIGenericButton m_leaderboardButton;
}
