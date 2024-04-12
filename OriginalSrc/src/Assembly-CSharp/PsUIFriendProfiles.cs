using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class PsUIFriendProfiles : UIVerticalList
{
	// Token: 0x0600172A RID: 5930 RVA: 0x000F9548 File Offset: 0x000F7948
	public PsUIFriendProfiles(UIComponent _parent)
		: base(_parent, string.Empty)
	{
		PsUITabbedCreate.m_selectedTab = 2;
		this.m_opened = false;
		this.m_players = new List<PsUIProfileBanner>();
		this.SetVerticalAlign(1f);
		this.SetWidth(0.75f, RelativeTo.ScreenWidth);
		this.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		this.SetMargins(0f, 0f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		(this.m_parent as UIScrollableCanvas).SetScrollPosition(0f, 0f);
		this.RemoveDrawHandler();
		this.CreateBaseContent();
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x000F95E0 File Offset: 0x000F79E0
	public void CreateBaseContent()
	{
		this.m_friendArea = new UIVerticalList(this, "friends");
		this.m_friendArea.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_friendArea.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this.m_friendArea, string.Empty);
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uiverticalList.SetHorizontalAlign(0f);
		uiverticalList.RemoveDrawHandler();
		int count = PsMetagameManager.m_friends.friendList.Count;
		UIText uitext = new UIText(uiverticalList, false, string.Empty, string.Concat(new object[]
		{
			PsStrings.Get(StringID.SOCIAL_FRIENDS),
			" <color=#ffffff>",
			count,
			"</color>"
		}), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ScreenHeight, "#A6FF32", null);
		uitext.SetHorizontalAlign(0f);
		if (count == 0 && PlayerPrefsX.GetFacebookId() == null)
		{
			UICanvas uicanvas = new UICanvas(uitext, false, string.Empty, null, string.Empty);
			uicanvas.SetSize(1f, 1f, RelativeTo.ParentHeight);
			uicanvas.SetHorizontalAlign(1f);
			uicanvas.SetMargins(2f, -2f, -0.2f, 0.2f, RelativeTo.ParentHeight);
			uicanvas.RemoveDrawHandler();
			this.m_fbButton = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
			this.m_fbButton.SetBlueColors(true);
			this.m_fbButton.SetIcon("menu_icon_facebook", 0.04f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_fbButton.SetSpacing(0.02f, RelativeTo.ScreenHeight);
			this.m_fbButton.SetHorizontalAlign(0f);
			UITextbox uitextbox = new UITextbox(this.m_fbButton, false, string.Empty, PsStrings.Get(StringID.FACEBOOK_CONNECT), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
			uitextbox.SetWidth(0.175f, RelativeTo.ScreenWidth);
			Action action = delegate
			{
				this.m_waitingPopup = new PsUIBasePopup(typeof(PsUIFriendProfiles.PsUIPopupFacebookWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
				FacebookManager.Login(new Action(this.FacebookDone));
			};
			this.m_fbButton.SetReleaseAction(action);
		}
		UICanvas uicanvas2 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.0375f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(0.4f, RelativeTo.ScreenWidth);
		uicanvas2.SetHorizontalAlign(0f);
		uicanvas2.RemoveDrawHandler();
		if (count == 0 && PlayerPrefsX.GetFacebookId() == null)
		{
			UITextbox uitextbox2 = new UITextbox(uicanvas2, false, string.Empty, PsStrings.Get(StringID.SOCIAL_CONNECT_FB), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
			uitextbox2.SetAlign(0f, 1f);
		}
		else if (count == 0 && PlayerPrefsX.GetFacebookId() != null)
		{
			UITextbox uitextbox3 = new UITextbox(uicanvas2, false, string.Empty, PsStrings.Get(StringID.FRIENDS_NOT_FOUND) + " " + PsStrings.Get(StringID.FRIENDS_GET_GAME), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
			uitextbox3.SetHorizontalAlign(0f);
		}
		else
		{
			UITextbox uitextbox4 = new UITextbox(uicanvas2, false, string.Empty, PsStrings.Get(StringID.SOCIAL_MUTUAL_FOLLOW), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
			uitextbox4.SetHorizontalAlign(0f);
		}
		this.m_followeeArea = new UIVerticalList(this, "followees");
		this.m_followeeArea.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_followeeArea.RemoveDrawHandler();
		UIVerticalList uiverticalList2 = new UIVerticalList(this.m_followeeArea, string.Empty);
		uiverticalList2.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uiverticalList2.SetHorizontalAlign(0f);
		uiverticalList2.RemoveDrawHandler();
		int count2 = PsMetagameManager.m_friends.followees.Count;
		UIText uitext2 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.SOCIAL_FOLLOWING) + " " + count2, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ScreenHeight, null, null);
		uitext2.SetHorizontalAlign(0f);
		if (count2 == 0)
		{
			UICanvas uicanvas3 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
			uicanvas3.SetHeight(0.0375f, RelativeTo.ScreenHeight);
			uicanvas3.SetWidth(0.4f, RelativeTo.ScreenWidth);
			uicanvas3.SetHorizontalAlign(0f);
			uicanvas3.RemoveDrawHandler();
			UITextbox uitextbox5 = new UITextbox(uicanvas3, false, string.Empty, PsStrings.Get(StringID.SOCIAL_NOT_FOLLOWING), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
			uitextbox5.SetAlign(0f, 1f);
		}
	}

	// Token: 0x0600172C RID: 5932 RVA: 0x000F9A34 File Offset: 0x000F7E34
	private void FacebookDone()
	{
		Debug.Log("Facebook call done", null);
		this.DestroyChildren();
		this.m_friendArea = null;
		this.m_followeeArea = null;
		this.m_openedBanner = null;
		this.m_opened = false;
		new PsUILoadingAnimation(this, false);
		this.m_parent.Update();
		this.m_players.Clear();
		PsMetagameManager.GetFriends(new Action<Friends>(this.CreateContentAgain), true);
		if (this.m_waitingPopup != null)
		{
			this.m_waitingPopup.Destroy();
			this.m_waitingPopup = null;
		}
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x000F9ABB File Offset: 0x000F7EBB
	public void CreateContentAgain(Friends _friends)
	{
		this.DestroyChildren();
		this.CreateBaseContent();
		this.m_currentFriendIndex = 0;
		this.m_currentFolloweeIndex = 0;
		this.m_parent.Update();
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x000F9AE4 File Offset: 0x000F7EE4
	private void CreateNextFriends()
	{
		if (this.m_friendArea != null)
		{
			int num = this.m_currentFriendIndex + 10;
			num = Mathf.Min(num, PsMetagameManager.m_friends.friendList.Count);
			for (int i = this.m_currentFriendIndex; i < num; i++)
			{
				PsUIProfileBanner psUIProfileBanner = new PsUIProfileBanner(this.m_friendArea, PsMetagameManager.m_friends.friendList[i], true, true);
				psUIProfileBanner.SetHorizontalAlign(0.5f);
				psUIProfileBanner.m_userLevels = PsCaches.m_friendsLevelList.GetContent(PsMetagameManager.m_friends.friendList[i].playerId);
				psUIProfileBanner.Update();
				this.m_players.Add(psUIProfileBanner);
			}
			this.m_friendArea.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_friendArea.CalculateReferenceSizes();
			this.m_friendArea.UpdateSize();
			this.m_friendArea.ArrangeContents();
			this.m_friendArea.UpdateDimensions();
			this.m_friendArea.UpdateSize();
			this.m_friendArea.ArrangeContents();
			this.SetHeight(1f, RelativeTo.ParentHeight);
			this.CalculateReferenceSizes();
			this.UpdateSize();
			this.ArrangeContents();
			base.UpdateDimensions();
			this.UpdateSize();
			this.UpdateAlign();
			this.UpdateChildrenAlign();
			this.ArrangeContents();
			if (num == PsMetagameManager.m_friends.friendList.Count)
			{
				(this.m_parent as UIScrollableCanvas).ArrangeContents();
			}
			this.m_currentFriendIndex = num;
		}
	}

	// Token: 0x0600172F RID: 5935 RVA: 0x000F9C54 File Offset: 0x000F8054
	private void CreateNextFollowees()
	{
		if (this.m_followeeArea != null)
		{
			int num = this.m_currentFolloweeIndex + 10;
			num = Mathf.Min(num, PsMetagameManager.m_friends.followees.Count);
			for (int i = this.m_currentFolloweeIndex; i < num; i++)
			{
				PsUIProfileBanner psUIProfileBanner = new PsUIProfileBanner(this.m_followeeArea, PsMetagameManager.m_friends.followees[i], true, true);
				psUIProfileBanner.SetHorizontalAlign(0.5f);
				psUIProfileBanner.m_userLevels = PsCaches.m_friendsLevelList.GetContent(PsMetagameManager.m_friends.followees[i].playerId);
				psUIProfileBanner.Update();
				this.m_players.Add(psUIProfileBanner);
			}
			this.m_followeeArea.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_followeeArea.CalculateReferenceSizes();
			this.m_followeeArea.UpdateSize();
			this.m_followeeArea.ArrangeContents();
			this.m_followeeArea.UpdateDimensions();
			this.m_followeeArea.UpdateSize();
			this.m_followeeArea.ArrangeContents();
			this.SetHeight(1f, RelativeTo.ParentHeight);
			this.CalculateReferenceSizes();
			this.UpdateSize();
			this.ArrangeContents();
			base.UpdateDimensions();
			this.UpdateSize();
			this.UpdateAlign();
			this.UpdateChildrenAlign();
			this.ArrangeContents();
			if (num == PsMetagameManager.m_friends.followees.Count)
			{
				(this.m_parent as UIScrollableCanvas).ArrangeContents();
				this.ScrollToPreviousPosition();
			}
			this.m_currentFolloweeIndex = num;
		}
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x000F9DC8 File Offset: 0x000F81C8
	public void ScrollToPreviousPosition()
	{
		if (!string.IsNullOrEmpty(PsUIFriendProfiles.m_lastPlayerId))
		{
			for (int i = 0; i < this.m_players.Count; i++)
			{
				if (this.m_players[i].m_user.playerId == PsUIFriendProfiles.m_lastPlayerId)
				{
					if (this.m_players[i].m_userLevels != null)
					{
						this.m_players[i].ShowLevels(delegate
						{
							this.Arrange(true);
						});
						(this.m_parent as UIScrollableCanvas).SetScrollPositionToChild(this.m_players[i]);
						if (!string.IsNullOrEmpty(PsUIFriendProfiles.m_lastLevelId))
						{
							for (int j = 0; j < this.m_players[i].m_buttons.Count; j++)
							{
								if (PsUIFriendProfiles.m_lastLevelId == this.m_players[i].m_buttons[j].m_gameloop.GetGameId())
								{
									(this.m_parent as UIScrollableCanvas).SetScrollPositionToChild(this.m_players[i].m_buttons[j]);
									break;
								}
							}
						}
					}
					break;
				}
			}
		}
		PsUIFriendProfiles.m_lastPlayerId = null;
		PsUIFriendProfiles.m_lastLevelId = null;
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x000F9F1C File Offset: 0x000F831C
	public override void Step()
	{
		if (this.m_openedBanner != null && !this.m_opened)
		{
			this.m_opened = true;
			Vector3 vector = (this.m_parent as UIScrollableCanvas).m_scrollTC.transform.position + new Vector3(0f, this.m_parent.m_actualHeight * 0.25f);
			float num = this.m_openedBanner.m_TC.transform.position.y - vector.y;
			Vector2 vector2 = (this.m_parent as UIScrollableCanvas).m_scrollTC.transform.position + new Vector2(0f, num);
			(this.m_parent as UIScrollableCanvas).ScrollToPosition(vector2, delegate
			{
				this.m_openedBanner.ShowLevels(delegate
				{
					this.Arrange(true);
				});
				this.Arrange(true);
			});
		}
		if (this.m_friendArea != null && PsMetagameManager.m_friends.friendList.Count > 0 && this.m_currentFriendIndex < PsMetagameManager.m_friends.friendList.Count)
		{
			this.CreateNextFriends();
		}
		else if (this.m_followeeArea != null && PsMetagameManager.m_friends.followees.Count > 0 && this.m_currentFolloweeIndex < PsMetagameManager.m_friends.followees.Count)
		{
			this.CreateNextFollowees();
		}
		bool flag = false;
		int num2 = -1;
		for (int i = 0; i < this.m_players.Count; i++)
		{
			if (this.m_players[i].m_banner.m_hit && this.m_players[i].m_user.publishedMinigameCount > 0)
			{
				SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
				if (!this.m_players[i].m_levelsShown)
				{
					this.m_openedBanner = this.m_players[i];
				}
				else if (this.m_players[i].m_levelsShown)
				{
					this.m_players[i].HideLevels();
					this.m_openedBanner = null;
				}
				this.m_opened = false;
				num2 = i;
				flag = true;
				break;
			}
		}
		if (flag)
		{
			for (int j = 0; j < this.m_players.Count; j++)
			{
				if (this.m_players[j].m_levelsShown && j != num2)
				{
					this.m_players[j].HideLevels();
				}
			}
			this.Arrange(false);
		}
		base.Step();
	}

	// Token: 0x06001732 RID: 5938 RVA: 0x000FA1C4 File Offset: 0x000F85C4
	public void Arrange(bool _updateScrollable = false)
	{
		if (this.m_friendArea != null)
		{
			this.m_friendArea.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_friendArea.CalculateReferenceSizes();
			this.m_friendArea.UpdateSize();
			this.m_friendArea.ArrangeContents();
			this.m_friendArea.UpdateDimensions();
			this.m_friendArea.UpdateSize();
			this.m_friendArea.ArrangeContents();
		}
		if (this.m_followeeArea != null)
		{
			this.m_followeeArea.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_followeeArea.CalculateReferenceSizes();
			this.m_followeeArea.UpdateSize();
			this.m_followeeArea.ArrangeContents();
			this.m_followeeArea.UpdateDimensions();
			this.m_followeeArea.UpdateSize();
			this.m_followeeArea.ArrangeContents();
		}
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.ArrangeContents();
		base.UpdateDimensions();
		this.UpdateSize();
		this.UpdateAlign();
		this.ArrangeContents();
		if (_updateScrollable)
		{
			(this.m_parent as UIScrollableCanvas).ArrangeContents();
		}
	}

	// Token: 0x06001733 RID: 5939 RVA: 0x000FA2D9 File Offset: 0x000F86D9
	public override void Destroy()
	{
		(this.m_parent as UIScrollableCanvas).ResetScroll();
		base.Destroy();
	}

	// Token: 0x040019ED RID: 6637
	public static string m_lastPlayerId;

	// Token: 0x040019EE RID: 6638
	public static string m_lastLevelId;

	// Token: 0x040019EF RID: 6639
	private List<PsUIProfileBanner> m_players;

	// Token: 0x040019F0 RID: 6640
	private UIVerticalList m_friendArea;

	// Token: 0x040019F1 RID: 6641
	private UIVerticalList m_followeeArea;

	// Token: 0x040019F2 RID: 6642
	private int m_currentFriendIndex;

	// Token: 0x040019F3 RID: 6643
	private int m_currentFolloweeIndex;

	// Token: 0x040019F4 RID: 6644
	private PsUIProfileBanner m_openedBanner;

	// Token: 0x040019F5 RID: 6645
	private bool m_opened;

	// Token: 0x040019F6 RID: 6646
	private PsUIGenericButton m_fbButton;

	// Token: 0x040019F7 RID: 6647
	private PsUIBasePopup m_waitingPopup;

	// Token: 0x02000310 RID: 784
	private class PsUIPopupWaiting : PsUIHeaderedCanvas
	{
		// Token: 0x06001738 RID: 5944 RVA: 0x000FA364 File Offset: 0x000F8764
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

	// Token: 0x02000311 RID: 785
	private class PsUIPopupFacebookWaiting : PsUIFriendProfiles.PsUIPopupWaiting
	{
		// Token: 0x06001739 RID: 5945 RVA: 0x000FA428 File Offset: 0x000F8828
		public PsUIPopupFacebookWaiting(UIComponent _parent)
			: base(_parent)
		{
			string text = PsStrings.Get(StringID.POPUP_DISCONNECTING_FROM_FB);
			if (PlayerPrefsX.GetFacebookId() == null)
			{
				text = "Connecting to Facebook...";
			}
			new UITextbox(this, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}
}
