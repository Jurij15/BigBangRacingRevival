using System;

// Token: 0x02000369 RID: 873
public class PsUICenterSearchTeams : UIVerticalList
{
	// Token: 0x0600195B RID: 6491 RVA: 0x001154D0 File Offset: 0x001138D0
	public PsUICenterSearchTeams(UIComponent _parent)
		: base(_parent, "teamSearch")
	{
		PsUITabbedTeam.m_selectedTab = 2;
		this.m_searching = false;
		(this.m_parent as UIScrollableCanvas).SetScrollPositionToChild(this);
		this.SetVerticalAlign(1f);
		this.SetWidth(0.75f, RelativeTo.ParentWidth);
		this.SetMargins(0f, 0f, 0.05f, 0f, RelativeTo.ScreenHeight);
		this.SetSpacing(0.035f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.SetSpacing(0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHeight(0.16f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		this.m_input = new PsUIGenericInputField(uihorizontalList);
		this.m_input.SetTextAreaDrawhandler(new UIDrawDelegate(UIDrawHandlers.TextfieldOutlined));
		this.m_input.SetMinMaxCharacterCount(3, 40);
		this.m_input.ChangeTitleText(PsStrings.Get(StringID.TEAM_SEARCH_HEADER));
		this.m_input.SetCallbacks(new Action<string>(this.DoneWriting), null);
		this.m_input.SetVerticalAlign(1f);
		this.m_searchButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_searchButton.SetBlueColors(true);
		this.m_searchButton.SetIcon("menu_icon_search", 0.07f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_searchButton.SetVerticalAlign(0f);
		this.m_searchButton.SetDepthOffset(-15f);
		this.m_teamArea = new PsUITeamArea(this, true, false, false, null, false, null);
	}

	// Token: 0x0600195C RID: 6492 RVA: 0x00115681 File Offset: 0x00113A81
	public void DoneWriting(string _input)
	{
		this.m_searchInput = _input;
		this.SearchContent();
	}

	// Token: 0x0600195D RID: 6493 RVA: 0x00115690 File Offset: 0x00113A90
	public void SearchContent()
	{
		if (!string.IsNullOrEmpty(this.m_searchInput) && !this.m_searching)
		{
			this.m_searching = true;
			this.m_teamArea.Search(this.m_searchInput);
		}
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x001156C5 File Offset: 0x00113AC5
	public void SearchFinished()
	{
		this.m_searching = false;
		this.m_searchInput = string.Empty;
		this.m_input.SetText(this.m_searchInput);
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x001156EC File Offset: 0x00113AEC
	public override void Step()
	{
		if (this.m_searchButton != null && this.m_searchButton.m_hit && !string.IsNullOrEmpty(this.m_searchInput) && !this.m_searching)
		{
			this.SearchContent();
		}
		base.Step();
	}

	// Token: 0x04001BEA RID: 7146
	private PsUIGenericInputField m_input;

	// Token: 0x04001BEB RID: 7147
	private PsUIGenericButton m_searchButton;

	// Token: 0x04001BEC RID: 7148
	private string m_searchInput;

	// Token: 0x04001BED RID: 7149
	private PsUITeamArea m_teamArea;

	// Token: 0x04001BEE RID: 7150
	private bool m_searching;
}
