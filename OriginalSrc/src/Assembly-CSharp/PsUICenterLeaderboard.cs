using System;
using Server;

// Token: 0x02000270 RID: 624
public class PsUICenterLeaderboard : UICanvas
{
	// Token: 0x0600129E RID: 4766 RVA: 0x000B7A90 File Offset: 0x000B5E90
	public PsUICenterLeaderboard(UIComponent _parent)
		: base(_parent, false, "LeaderboardCanvas", null, string.Empty)
	{
		this.RemoveDrawHandler();
		string text = ((!PsState.GetCurrentVehicleType(false).ToString().Equals("Motorcycle")) ? PsStrings.Get(StringID.CAR_LEADERBOARDS) : PsStrings.Get(StringID.BIKE_LEADERBOARDS));
		UIText uitext = new UIText(this, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.06f, RelativeTo.ScreenHeight, "#FFFFFF", "#111111");
		uitext.SetVerticalAlign(0.975f);
		this.m_selectedTab = 1;
		this.m_scrollableCanvas = new UIScrollableCanvas(this, string.Empty);
		this.m_scrollableCanvas.SetHeight(0.75f, RelativeTo.ScreenHeight);
		this.m_scrollableCanvas.SetVerticalAlign(0f);
		this.m_scrollableCanvas.RemoveDrawHandler();
		this.m_scrollableCanvas.m_passTouchesToScrollableParents = true;
		this.ShowContent(this.m_scrollableCanvas);
		this.CreateTabCanvas(false);
		this.LoadData();
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x000B7B9C File Offset: 0x000B5F9C
	private void CreateTabCanvas(bool _update)
	{
		if (this.m_tabCanvas != null)
		{
			this.m_tabCanvas.Destroy();
		}
		this.m_tabCanvas = new UIScrollableCanvas(this, "Header");
		this.m_tabCanvas.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_tabCanvas.SetHeight(0.175f, RelativeTo.ScreenHeight);
		this.m_tabCanvas.SetVerticalAlign(0.875f);
		this.m_tabCanvas.RemoveTouchAreas();
		this.m_tabCanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_tabCanvas, string.Empty);
		uihorizontalList.SetHeight(1f, RelativeTo.ParentHeight);
		uihorizontalList.RemoveDrawHandler();
		this.m_tab1 = new UICanvas(uihorizontalList, true, string.Empty, null, string.Empty);
		this.m_tab1.SetWidth(0.25f, RelativeTo.ScreenHeight);
		this.m_tab1.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_tab1.SetMargins(0f, 0f, 0.02f, 0f, RelativeTo.ScreenHeight);
		this.m_tab1.RemoveDrawHandler();
		this.CreateTab(this.m_tab1, PsStrings.Get(StringID.BOARD_GLOBAL), this.m_selectedTab == 1);
		this.m_tab2 = new UICanvas(uihorizontalList, true, string.Empty, null, string.Empty);
		this.m_tab2.SetWidth(0.25f, RelativeTo.ScreenHeight);
		this.m_tab2.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_tab2.SetMargins(0f, 0f, 0.02f, 0f, RelativeTo.ScreenHeight);
		this.m_tab2.RemoveDrawHandler();
		this.CreateTab(this.m_tab2, PsStrings.Get(StringID.BOARD_LOCAL), this.m_selectedTab == 2);
		this.m_tab3 = new UICanvas(uihorizontalList, true, string.Empty, null, string.Empty);
		this.m_tab3.SetWidth(0.25f, RelativeTo.ScreenHeight);
		this.m_tab3.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_tab3.SetMargins(0f, 0f, 0.02f, 0f, RelativeTo.ScreenHeight);
		this.m_tab3.RemoveDrawHandler();
		this.CreateTab(this.m_tab3, PsStrings.Get(StringID.BOARD_FRIENDS), this.m_selectedTab == 3);
		UISprite uisprite = new UISprite(this.m_tabCanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tab_border", null), true);
		uisprite.SetWidth(1f, RelativeTo.ScreenWidth);
		uisprite.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uisprite.SetVerticalAlign(0f);
		if (_update)
		{
			this.Update();
		}
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x000B7E24 File Offset: 0x000B6224
	private void CreateTab(UICanvas _parent, string _text, bool _active)
	{
		float num = 0f;
		if (_active)
		{
			UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tab_active", null), true, true);
			uifittedSprite.SetVerticalAlign(1f);
			UIText uitext = new UIText(_parent, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0375f, RelativeTo.ScreenHeight, "ffd22e", "184272");
			uitext.SetVerticalAlign(0.9f);
			num = 0.05f;
		}
		else
		{
			UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tab_deactive", null), true, true);
			uifittedSprite.SetDepthOffset(20f);
			uifittedSprite.SetVerticalAlign(0.9f);
			UIText uitext2 = new UIText(_parent, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0375f, RelativeTo.ScreenHeight, "18324d", null);
			uitext2.SetVerticalAlign(0.8f);
		}
		if (_text == PsStrings.Get(StringID.BOARD_LOCAL))
		{
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(PlayerPrefsX.GetCountryCode(), "_alien");
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, frame, true, true);
			uifittedSprite2.SetHorizontalAlign(0.075f);
			uifittedSprite2.SetVerticalAlign(0.75f + num);
			uifittedSprite2.SetDepthOffset(-5f);
			uifittedSprite2.SetHeight(0.15f, RelativeTo.ParentHeight);
		}
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x000B7F90 File Offset: 0x000B6390
	public virtual HttpC LoadData()
	{
		this.m_leaderboard = null;
		Debug.Log("starting to load leaderboards", null);
		HttpC httpC = Trophy.Leaderboard(PsState.GetCurrentVehicleType(false).ToString(), new Action<Leaderboard>(this.LeaderboardLoadSucceed), new Action<HttpC>(this.LeaderboardLoadFailed), null);
		EntityManager.AddComponentToEntity((this.GetRoot() as PsUIBasePopup).m_utilityEntity, httpC);
		return httpC;
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x000B7FF4 File Offset: 0x000B63F4
	protected virtual void LeaderboardLoadSucceed(Leaderboard _leaderboard)
	{
		Debug.Log("loading of leaderboard succeed", null);
		if (_leaderboard.global == null)
		{
			Debug.Log("Leaderbeard: global null", null);
		}
		if (_leaderboard.local == null)
		{
			Debug.Log("Leaderbeard: Local null", null);
		}
		if (_leaderboard.friends == null)
		{
			Debug.Log("Leaderbeard: friends null", null);
		}
		this.m_leaderboard = _leaderboard;
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x000B8058 File Offset: 0x000B6458
	protected virtual void LeaderboardLoadFailed(HttpC _c)
	{
		Debug.Log("Leaderboard load failed", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, () => this.LoadData(), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x000B80A3 File Offset: 0x000B64A3
	private void ShowContent(UIComponent _parent)
	{
		_parent.DestroyChildren();
		if (this.m_selectedTab == 1)
		{
			new PsUILeaderboardListGlobal(_parent);
		}
		else if (this.m_selectedTab == 2)
		{
			new PsUILeaderboardListLocal(_parent);
		}
		else
		{
			new PsUILeaderboardListFriends(_parent);
		}
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x000B80E4 File Offset: 0x000B64E4
	public override void Step()
	{
		if (this.m_tab1.m_hit)
		{
			this.m_selectedTab = 1;
			this.CreateTabCanvas(true);
			this.ShowContent(this.m_scrollableCanvas);
			this.m_scrollableCanvas.SetScrollPosition(0f, 0f);
		}
		else if (this.m_tab2.m_hit)
		{
			this.m_selectedTab = 2;
			this.CreateTabCanvas(true);
			this.ShowContent(this.m_scrollableCanvas);
			this.m_scrollableCanvas.SetScrollPosition(0f, 0f);
		}
		else if (this.m_tab3.m_hit)
		{
			this.m_selectedTab = 3;
			this.CreateTabCanvas(true);
			this.ShowContent(this.m_scrollableCanvas);
			this.m_scrollableCanvas.SetScrollPosition(0f, 0f);
		}
		base.Step();
	}

	// Token: 0x040015C0 RID: 5568
	public Leaderboard m_leaderboard;

	// Token: 0x040015C1 RID: 5569
	private UIVerticalList m_vlist;

	// Token: 0x040015C2 RID: 5570
	private UIVerticalList m_entryUIList;

	// Token: 0x040015C3 RID: 5571
	private UIScrollableCanvas m_scrollableCanvas;

	// Token: 0x040015C4 RID: 5572
	private UIText m_globalText;

	// Token: 0x040015C5 RID: 5573
	private UIText m_localText;

	// Token: 0x040015C6 RID: 5574
	private UIText m_friendText;

	// Token: 0x040015C7 RID: 5575
	private UIScrollableCanvas m_tabCanvas;

	// Token: 0x040015C8 RID: 5576
	private UICanvas m_tab1;

	// Token: 0x040015C9 RID: 5577
	private UICanvas m_tab2;

	// Token: 0x040015CA RID: 5578
	private UICanvas m_tab3;

	// Token: 0x040015CB RID: 5579
	private int m_selectedTab;

	// Token: 0x040015CC RID: 5580
	private string m_activeColor = "#FFFFFF";

	// Token: 0x040015CD RID: 5581
	private string m_passiveColor = "#a7a7a7";
}
