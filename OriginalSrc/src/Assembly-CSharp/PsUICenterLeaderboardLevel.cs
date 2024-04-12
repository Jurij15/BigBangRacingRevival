using System;
using System.Collections.Generic;
using Server;

// Token: 0x02000277 RID: 631
public class PsUICenterLeaderboardLevel : PsUIHeaderedCanvas
{
	// Token: 0x060012D2 RID: 4818 RVA: 0x000B9E24 File Offset: 0x000B8224
	public PsUICenterLeaderboardLevel(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0.065f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.BgDrawhandler));
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetHeight(0.9f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0.012f, 0.06f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_footer.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIFooter));
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
		this.GetLeaderboard();
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x000B9F40 File Offset: 0x000B8340
	public HttpC GetLeaderboard()
	{
		HttpC httpC = Trophy.LeaderboardByGame(PsState.m_activeGameLoop.m_minigameId, new Action<List<LeaderboardEntry>>(this.CreateLeaderboardContent), delegate(HttpC c)
		{
			Debug.Log("GetLeaderboard failed", null);
			string networkError = ServerErrors.GetNetworkError(c.www.error);
			ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, () => this.GetLeaderboard(), null, StringID.TRY_AGAIN_SERVER);
		}, null);
		httpC.p_entity = this.m_TC.p_entity;
		return httpC;
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x000B9F88 File Offset: 0x000B8388
	public void CreateHeaderContent(UIComponent _parent)
	{
		string text = "Top players";
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetSpacing(0.001f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, text + " ", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, "#95e5ff", "#000000");
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x000B9FE8 File Offset: 0x000B83E8
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(_parent, string.Empty);
		uiscrollableCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uiscrollableCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uiscrollableCanvas.SetHorizontalAlign(0.5f);
		uiscrollableCanvas.RemoveDrawHandler();
		uiscrollableCanvas.m_passTouchesToScrollableParents = true;
		this.m_uiList = new PsUILeaderboardListGame(uiscrollableCanvas);
		this.m_uiList.SetHorizontalAlign(0f);
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x000BA054 File Offset: 0x000B8454
	private void AddPlayer(List<LeaderboardEntry> _list)
	{
		int timeScoreBest = PsState.m_activeGameLoop.m_timeScoreBest;
		int num = -1;
		int num2 = -1;
		LeaderboardEntry player = this.GetPlayer();
		for (int i = 0; i < _list.Count; i++)
		{
			if (_list[i].user.playerId == PlayerPrefsX.GetUserId())
			{
				num = i;
			}
			if (_list[i].time > timeScoreBest && num2 == -1)
			{
				num2 = i;
			}
		}
		if (num != -1 && num2 != -1)
		{
			if (num > num2)
			{
				_list.RemoveAt(num);
				_list.Insert(num2, player);
			}
			else if (timeScoreBest < _list[num].time)
			{
				_list[num] = player;
			}
		}
		else if (num == -1 && num2 != -1)
		{
			_list.Insert(num2, player);
		}
		else if (num == -1 || num2 != -1)
		{
			if (_list.Count < 20 && timeScoreBest != 2147483647)
			{
				_list.Insert(_list.Count, player);
			}
		}
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x000BA17C File Offset: 0x000B857C
	private LeaderboardEntry GetPlayer()
	{
		LeaderboardEntry leaderboardEntry = default(LeaderboardEntry);
		leaderboardEntry.user.countryCode = PlayerPrefsX.GetCountryCode();
		leaderboardEntry.user.facebookId = PlayerPrefsX.GetFacebookId();
		leaderboardEntry.user.playerId = PlayerPrefsX.GetUserId();
		leaderboardEntry.user.name = PlayerPrefsX.GetUserName();
		leaderboardEntry.time = PsState.m_activeGameLoop.m_timeScoreBest;
		leaderboardEntry.trophies = PsMetagameManager.m_playerStats.trophies;
		return leaderboardEntry;
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x000BA1F8 File Offset: 0x000B85F8
	public void CreateLeaderboardContent(List<LeaderboardEntry> _list)
	{
		this.AddPlayer(_list);
		this.m_uiList.CreateLeaderBoardContent(_list);
	}

	// Token: 0x040015D4 RID: 5588
	private UIScrollableCanvas m_canvas;

	// Token: 0x040015D5 RID: 5589
	private PsUILeaderboardListGame m_uiList;
}
