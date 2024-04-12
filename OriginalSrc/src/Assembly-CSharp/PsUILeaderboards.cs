using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000323 RID: 803
public abstract class PsUILeaderboards<TEntry> : UIVerticalList
{
	// Token: 0x06001798 RID: 6040 RVA: 0x000B81C8 File Offset: 0x000B65C8
	protected PsUILeaderboards(UIComponent _parent, string _infoText, string _infoPopupText, string _infoPopupHeader)
		: base(_parent, string.Empty)
	{
		this.m_infoText = _infoText;
		this.m_infoPopupText = _infoPopupText;
		this.m_infoPopupHeader = _infoPopupHeader;
		(this.m_parent as UIScrollableCanvas).SetScrollPosition(0f, 0f);
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetMargins(0f, 0f, 0.06f, 0.06f, RelativeTo.ScreenHeight);
		this.SetSpacing(0f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(1f);
		this.RemoveDrawHandler();
		this.CreateBaseContent();
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x000B8264 File Offset: 0x000B6664
	protected virtual void LeaderboardLoadSucceed(Leaderboard<TEntry> _leaderboard)
	{
		Debug.Log("loading of leaderboard succeed", null);
		this.m_leaderboard = _leaderboard;
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
		this.m_loading = false;
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x000B82CC File Offset: 0x000B66CC
	protected virtual void LeaderboardLoadFailed(HttpC _c)
	{
		Debug.Log("Leaderboard load failed", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, () => this.GetLeaderboards(), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x0600179B RID: 6043
	public abstract void CreateListItem(int _i);

	// Token: 0x0600179C RID: 6044 RVA: 0x000B8318 File Offset: 0x000B6718
	public void CreateBaseContent()
	{
		this.m_createSeasonInfo = true;
		this.m_seasonInfo = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_seasonInfo.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_seasonInfo.SetMargins(0.125f, 0.12f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_seasonInfo.SetHeight(0.3f, RelativeTo.ScreenHeight);
		this.m_seasonInfo.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_seasonInfo, "TopInfoList");
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.OwnWidth);
		uihorizontalList.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, "LeftContainer");
		uiverticalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(0.5f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.SetHorizontalAlign(0f);
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, this.m_infoText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
		UICanvas uicanvas = new UICanvas(uiverticalList, false, "IconA", null, string.Empty);
		uicanvas.SetRogue();
		uicanvas.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(0f, 1f);
		uicanvas.SetMargins(-0.07f, 0.07f, -0.04f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		this.m_infoButton = new UIRectSpriteButton(uicanvas, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true, false);
		this.m_infoButton.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_rightContainer = new UICanvas(uihorizontalList, false, "RightContainer", null, string.Empty);
		this.m_rightContainer.SetWidth(0.45f, RelativeTo.ParentWidth);
		this.m_rightContainer.SetMargins(0f, 0f, 0.05f, 0f, RelativeTo.ScreenHeight);
		this.m_rightContainer.SetVerticalAlign(1f);
		this.m_rightContainer.RemoveDrawHandler();
		this.m_playerArea = new UIVerticalList(this, string.Empty);
		this.m_playerArea.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_playerArea.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_playerArea.RemoveDrawHandler();
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x000B85A8 File Offset: 0x000B69A8
	public void CreateSeasonInfo()
	{
		UICanvas uicanvas = new UICanvas(this.m_seasonInfo, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.09f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.335f, RelativeTo.ScreenWidth);
		uicanvas.SetMargins(0.05f, 0.05f, 0.005f, 0.0125f, RelativeTo.ScreenHeight);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SeasonTopDrawhandler));
		uicanvas.SetVerticalAlign(0f);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetDepthOffset(5f);
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetVerticalAlign(1f);
		uicanvas2.RemoveDrawHandler();
		uicanvas2.SetMargins(0.002f, RelativeTo.ScreenHeight);
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TEAM_CURRENT_SEASON_HEADER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FCF675", null);
		UICanvas uicanvas3 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetVerticalAlign(0f);
		uicanvas3.RemoveDrawHandler();
		uicanvas3.SetMargins(0.0015f, RelativeTo.ScreenHeight);
		this.m_timeText = new UIFittedText(uicanvas3, false, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#423312", null);
		this.UpdateTime();
		uicanvas.Update();
	}

	// Token: 0x0600179E RID: 6046
	protected abstract HttpC GetLeaderboards();

	// Token: 0x0600179F RID: 6047
	protected abstract bool FriendsTabSelected();

	// Token: 0x060017A0 RID: 6048 RVA: 0x000B8730 File Offset: 0x000B6B30
	public void ChangeBoard(int _index)
	{
		if (!this.m_loading)
		{
			(this.m_parent as UIScrollableCanvas).SetScrollPosition(0f, 0f);
			this.SetVerticalAlign(1f);
			this.m_currentIndex = 0;
			this.m_currentMax = 0;
			if (this.m_playerArea != null)
			{
				this.m_playerArea.DestroyChildren();
			}
			if (this.m_leaderboard == null)
			{
				this.GetLeaderboards();
				new PsUILoadingAnimation(this.m_playerArea, false);
			}
			else
			{
				this.OnTabSelected(_index);
				if (this.m_currentMax == 0)
				{
					this.m_parent.Update();
				}
			}
		}
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x000B87D4 File Offset: 0x000B6BD4
	public void OnTabSelected(int _index)
	{
		if (_index != 1)
		{
			if (_index != 2)
			{
				if (_index == 3)
				{
					this.m_currentMax = this.m_leaderboard.friends.Count;
					this.m_currentList = this.m_leaderboard.friends;
				}
			}
			else
			{
				this.m_currentMax = this.m_leaderboard.local.Count;
				this.m_currentList = this.m_leaderboard.local;
			}
		}
		else
		{
			this.m_currentMax = this.m_leaderboard.global.Count;
			this.m_currentList = this.m_leaderboard.global;
		}
	}

	// Token: 0x060017A2 RID: 6050 RVA: 0x000B8880 File Offset: 0x000B6C80
	public void CreateNoFriendContent()
	{
		this.m_rightContainer = null;
		this.m_seasonInfo.Destroy();
		this.m_playerArea.Destroy();
		this.m_timeText = null;
		this.m_playerArea = null;
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.12f, RelativeTo.ScreenWidth);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetMargins(0.11f, 0.11f, 0.05f, 0.03f, RelativeTo.ScreenWidth);
		uicanvas.RemoveDrawHandler();
		string text = PsStrings.Get(StringID.FRIENDS_NOT_FOUND) + " " + PsStrings.Get(StringID.SOCIAL_CONNECT_FB);
		if (!string.IsNullOrEmpty(PlayerPrefsX.GetFacebookId()))
		{
			text = PsStrings.Get(StringID.FRIENDS_NOT_FOUND) + " " + PsStrings.Get(StringID.FRIENDS_GET_GAME);
		}
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, "313131");
		if (string.IsNullOrEmpty(PlayerPrefsX.GetFacebookId()))
		{
			this.m_fbButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
			this.m_fbButton.SetBlueColors(true);
			this.m_fbButton.SetIcon("menu_icon_facebook", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_fbButton.SetSpacing(0.005f, RelativeTo.ScreenHeight);
			UITextbox uitextbox = new UITextbox(this.m_fbButton, false, string.Empty, PsStrings.Get(StringID.FACEBOOK_CONNECT), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
			uitextbox.SetWidth(0.175f, RelativeTo.ScreenWidth);
			Action action = delegate
			{
				this.m_waitingPopup = new PsUIBasePopup(typeof(PsUILeaderboards<TEntry>.PsUIPopupFacebookWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
				FacebookManager.Login(new Action(this.FacebookDone));
			};
			this.m_fbButton.SetReleaseAction(action);
		}
		this.m_parent.Update();
	}

	// Token: 0x060017A3 RID: 6051 RVA: 0x000B8A3E File Offset: 0x000B6E3E
	private void FacebookDone()
	{
		this.m_waitingPopup.Destroy();
		this.m_waitingPopup = null;
		this.DestroyChildren();
		this.m_timeText = null;
		this.m_playerArea = null;
		this.m_leaderboard = null;
		this.CreateBaseContent();
		this.Update();
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x000B8A7C File Offset: 0x000B6E7C
	public void CreateBatch()
	{
		if (this.m_currentIndex == 0)
		{
			if (this.m_playerArea == null)
			{
				this.DestroyChildren();
				this.m_timeText = null;
				this.m_playerArea = null;
				this.CreateBaseContent();
				this.Update();
			}
			this.m_playerArea.DestroyChildren();
			if (this.m_createSeasonInfo)
			{
				this.CreateSeasonInfo();
				this.m_createSeasonInfo = false;
			}
			if (this.m_currentMax == 0)
			{
				this.m_currentList = null;
				if (this.FriendsTabSelected())
				{
					this.CreateNoFriendContent();
				}
			}
		}
		int num = Mathf.Min(this.m_currentIndex + 10, this.m_currentMax);
		for (int i = this.m_currentIndex; i < num; i++)
		{
			this.CreateListItem(i);
		}
		if (this.m_playerArea != null)
		{
			this.m_playerArea.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_playerArea.CalculateReferenceSizes();
			this.m_playerArea.UpdateSize();
			this.m_playerArea.ArrangeContents();
			this.m_playerArea.UpdateDimensions();
			this.m_playerArea.UpdateSize();
			this.m_playerArea.UpdateAlign();
			this.m_playerArea.UpdateChildrenAlign();
			this.m_playerArea.ArrangeContents();
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
		this.m_currentIndex = num;
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x000B8BFC File Offset: 0x000B6FFC
	public override void Step()
	{
		this.UpdateTime();
		if (!this.m_loading && this.m_leaderboard != null && this.m_currentList != null && (this.m_currentIndex < this.m_currentMax || (this.m_currentMax == 0 && this.m_currentIndex == 0)))
		{
			this.CreateBatch();
		}
		if (this.m_infoButton.m_hit && this.m_infoPopup == null)
		{
			this.m_infoPopup = PsUIPopupInfoText.Create(this.m_infoPopupHeader, this.m_infoPopupText, delegate
			{
				this.m_infoPopup.Destroy();
				this.m_infoPopup = null;
			});
		}
		base.Step();
	}

	// Token: 0x060017A6 RID: 6054 RVA: 0x000B8CA1 File Offset: 0x000B70A1
	protected void UpdateTime()
	{
		this.SetTimeRemaining(this.GetTimeRemaining());
	}

	// Token: 0x060017A7 RID: 6055 RVA: 0x000B8CAF File Offset: 0x000B70AF
	public virtual int GetTimeRemaining()
	{
		return 0;
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x000B8CB4 File Offset: 0x000B70B4
	protected virtual void SetTimeRemaining(int _seconds)
	{
		if (this.m_currentTimeLeft != PsMetagameManager.m_seasonTimeleft && this.m_timeText != null)
		{
			this.m_currentTimeLeft = _seconds;
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(_seconds);
			string text = PsStrings.Get(StringID.TEAM_SEASON_END_TIMER);
			text = text.Replace("%1", timeStringFromSeconds);
			this.m_timeText.SetText(text);
		}
	}

	// Token: 0x04001A6A RID: 6762
	protected Leaderboard<TEntry> m_leaderboard;

	// Token: 0x04001A6B RID: 6763
	protected List<TEntry> m_currentList;

	// Token: 0x04001A6C RID: 6764
	protected UIVerticalList m_playerArea;

	// Token: 0x04001A6D RID: 6765
	protected UICanvas m_seasonInfo;

	// Token: 0x04001A6E RID: 6766
	protected bool m_createSeasonInfo = true;

	// Token: 0x04001A6F RID: 6767
	protected UICanvas m_rightContainer;

	// Token: 0x04001A70 RID: 6768
	protected bool m_loading;

	// Token: 0x04001A71 RID: 6769
	protected int m_currentIndex;

	// Token: 0x04001A72 RID: 6770
	protected int m_currentMax;

	// Token: 0x04001A73 RID: 6771
	protected UIFittedText m_timeText;

	// Token: 0x04001A74 RID: 6772
	protected int m_currentTimeLeft;

	// Token: 0x04001A75 RID: 6773
	protected PsUIGenericButton m_fbButton;

	// Token: 0x04001A76 RID: 6774
	protected PsUIBasePopup m_waitingPopup;

	// Token: 0x04001A77 RID: 6775
	protected PsUIBasePopup m_infoPopup;

	// Token: 0x04001A78 RID: 6776
	protected UIRectSpriteButton m_infoButton;

	// Token: 0x04001A79 RID: 6777
	protected string m_infoText;

	// Token: 0x04001A7A RID: 6778
	protected string m_infoPopupHeader;

	// Token: 0x04001A7B RID: 6779
	protected string m_infoPopupText;

	// Token: 0x02000324 RID: 804
	private class PsUIPopupWaiting : PsUIHeaderedCanvas
	{
		// Token: 0x060017AC RID: 6060 RVA: 0x000B8D68 File Offset: 0x000B7168
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

	// Token: 0x02000325 RID: 805
	private class PsUIPopupFacebookWaiting : PsUILeaderboards<TEntry>.PsUIPopupWaiting
	{
		// Token: 0x060017AD RID: 6061 RVA: 0x000B8E2C File Offset: 0x000B722C
		public PsUIPopupFacebookWaiting(UIComponent _parent)
			: base(_parent)
		{
			string text = PsStrings.Get(StringID.POPUP_DISCONNECTING_FROM_FB);
			if (PlayerPrefsX.GetFacebookId() == null)
			{
				text = PsStrings.Get(StringID.POPUP_DISCONNECTING_FROM_FB);
			}
			new UITextbox(this, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}
}
