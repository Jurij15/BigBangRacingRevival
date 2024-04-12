using System;
using System.Collections.Generic;
using Server;

// Token: 0x0200030E RID: 782
public class PsUIFriendList : UIVerticalList
{
	// Token: 0x0600171F RID: 5919 RVA: 0x000F8D5C File Offset: 0x000F715C
	public PsUIFriendList(UIComponent _parent)
		: base(_parent, "Follower List")
	{
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetSpacing(0.035f, RelativeTo.ScreenHeight);
		this.SetMargins(0.025f, 0.025f, 0.1125f, 0.05f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(1f);
		this.SetWidth(0.3f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		this.GetPlayerData(true);
	}

	// Token: 0x06001720 RID: 5920 RVA: 0x000F8DD4 File Offset: 0x000F71D4
	public void GetPlayerData(bool _forceRefresh = false)
	{
		this.m_created = false;
		if (string.IsNullOrEmpty(PlayerPrefsX.GetFacebookId()))
		{
			this.m_fbButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
			this.m_fbButton.SetIcon("menu_icon_facebook", 0.045f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_fbButton.SetText(PsStrings.Get(StringID.FACEBOOK_CONNECT), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		}
		new PsUILoadingAnimation(this, false);
		this.Update();
		PsMetagameManager.GetFriends(new Action<Friends>(this.CreateContent), _forceRefresh);
	}

	// Token: 0x06001721 RID: 5921 RVA: 0x000F8E80 File Offset: 0x000F7280
	public void CreateContent(Friends _friendData)
	{
		if (!this.m_alive)
		{
			return;
		}
		if (this.m_fbButton == null)
		{
			this.DestroyChildren(0);
		}
		else
		{
			this.DestroyChildren(1);
		}
		this.m_addFriendButtons = new List<PsUIGenericButton>();
		this.m_unfollowButtons = new List<PsUIGenericButton>();
		new UIText(this, false, string.Empty, "Friends", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, null, null);
		if (_friendData.friendList == null || _friendData.friendList.Count < 1)
		{
			new UIText(this, false, string.Empty, "Invite your friends to play!", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, "#d3d3d3", null);
		}
		else
		{
			for (int i = 0; i < _friendData.friendList.Count; i++)
			{
				UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
				uihorizontalList.RemoveDrawHandler();
				uihorizontalList.SetHorizontalAlign(0f);
				uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
				PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uihorizontalList, false, string.Empty, _friendData.friendList[i].facebookId, _friendData.friendList[i].gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, false, true);
				psUIProfileImage.SetSize(0.06f, 0.06f, RelativeTo.ScreenHeight);
				UIText uitext = new UIText(uihorizontalList, false, string.Empty, _friendData.friendList[i].name, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
			}
		}
		new UIText(this, false, string.Empty, "\nYou follow", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, null, null);
		if (_friendData.followees == null || _friendData.followees.Count < 1)
		{
			new UIText(this, false, string.Empty, "You are not following anyone yet.", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, "#d3d3d3", null);
		}
		else
		{
			for (int j = 0; j < _friendData.followees.Count; j++)
			{
				UIHorizontalList uihorizontalList2 = new UIHorizontalList(this, string.Empty);
				uihorizontalList2.RemoveDrawHandler();
				uihorizontalList2.SetSpacing(0.025f, RelativeTo.ScreenHeight);
				PsUIProfileImage psUIProfileImage2 = new PsUIProfileImage(uihorizontalList2, false, string.Empty, _friendData.followees[j].facebookId, _friendData.followees[j].gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, false, true);
				psUIProfileImage2.SetSize(0.06f, 0.06f, RelativeTo.ScreenHeight);
				UIText uitext2 = new UIText(uihorizontalList2, false, string.Empty, _friendData.followees[j].name, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
				PsUIGenericButton psUIGenericButton = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.005f, "Button");
				psUIGenericButton.SetText(PsStrings.Get(StringID.UNFOLLOW), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
				psUIGenericButton.m_identifier = _friendData.followees[j].playerId;
				this.m_unfollowButtons.Add(psUIGenericButton);
			}
		}
		new UIText(this, false, string.Empty, "\nYour followers", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, null, null);
		if (_friendData.followers == null || _friendData.followers.Count < 1)
		{
			new UIText(this, false, string.Empty, "No followers. Create great levels to get followers!", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, "#d3d3d3", null);
		}
		else
		{
			for (int k = 0; k < _friendData.followers.Count; k++)
			{
				UIHorizontalList uihorizontalList3 = new UIHorizontalList(this, string.Empty);
				uihorizontalList3.RemoveDrawHandler();
				uihorizontalList3.SetSpacing(0.025f, RelativeTo.ScreenHeight);
				PsUIProfileImage psUIProfileImage3 = new PsUIProfileImage(uihorizontalList3, false, string.Empty, _friendData.followers[k].facebookId, _friendData.followers[k].gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, false, true);
				psUIProfileImage3.SetSize(0.06f, 0.06f, RelativeTo.ScreenHeight);
				UIText uitext3 = new UIText(uihorizontalList3, false, string.Empty, _friendData.followers[k].name, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
				PsUIGenericButton psUIGenericButton2 = new PsUIGenericButton(uihorizontalList3, 0.25f, 0.25f, 0.005f, "Button");
				psUIGenericButton2.SetText("Add friend", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
				psUIGenericButton2.m_identifier = _friendData.followers[k].playerId;
				this.m_addFriendButtons.Add(psUIGenericButton2);
			}
		}
		this.Update();
		this.m_parent.Update();
		this.m_created = true;
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x000F9358 File Offset: 0x000F7758
	public override void Step()
	{
		if (this.m_fbButton != null && this.m_fbButton.m_hit)
		{
			this.m_waitingPopup = new PsUIBasePopup(typeof(PsUIPopupFacebookWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
			FacebookManager.Login(new Action(this.FacebookLoginDone));
		}
		if (this.m_created)
		{
			for (int i = 0; i < this.m_addFriendButtons.Count; i++)
			{
				if (this.m_addFriendButtons[i].m_hit)
				{
					string identifier = this.m_addFriendButtons[i].m_identifier;
					this.Follow(identifier);
				}
			}
			for (int j = 0; j < this.m_unfollowButtons.Count; j++)
			{
				if (this.m_unfollowButtons[j].m_hit)
				{
					string identifier2 = this.m_unfollowButtons[j].m_identifier;
					this.Unfollow(identifier2);
				}
			}
		}
		base.Step();
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x000F9456 File Offset: 0x000F7856
	private void FacebookLoginDone()
	{
		Debug.Log("Facebook login done", null);
		this.DestroyChildren();
		this.m_fbButton = null;
		this.GetPlayerData(true);
		if (this.m_waitingPopup != null)
		{
			this.m_waitingPopup.Destroy();
			this.m_waitingPopup = null;
		}
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x000F9494 File Offset: 0x000F7894
	private void Follow(string _id)
	{
		this.m_waitPopup = new PsUIWaitPopup(null);
		Player.Follow(_id, null, new Action<HttpC>(this.FollowOk), new Action<HttpC>(this.FollowFailed), null);
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000F94C3 File Offset: 0x000F78C3
	public void FollowOk(HttpC _c)
	{
		this.DestroyWaitPopup();
		this.DestroyChildren();
		this.GetPlayerData(true);
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x000F94D8 File Offset: 0x000F78D8
	public void FollowFailed(HttpC _c)
	{
		this.DestroyWaitPopup();
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x000F94E0 File Offset: 0x000F78E0
	private void Unfollow(string _id)
	{
		Debug.LogError("Unfollowing");
		this.m_waitPopup = new PsUIWaitPopup(null);
		Player.UnFollow(_id, new Action<HttpC>(this.FollowOk), new Action<HttpC>(this.FollowFailed), null);
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x000F9518 File Offset: 0x000F7918
	private void DestroyWaitPopup()
	{
		if (this.m_waitPopup != null)
		{
			this.m_waitPopup.Destroy();
			this.m_waitPopup = null;
		}
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x000F9537 File Offset: 0x000F7937
	public override void Destroy()
	{
		this.m_alive = false;
		base.Destroy();
	}

	// Token: 0x040019E4 RID: 6628
	private bool m_created;

	// Token: 0x040019E5 RID: 6629
	private PsUIWaitPopup m_waitPopup;

	// Token: 0x040019E6 RID: 6630
	private PsUIBasePopup m_waitingPopup;

	// Token: 0x040019E7 RID: 6631
	private PsUIGenericButton m_fbButton;

	// Token: 0x040019E8 RID: 6632
	private List<PsUIGenericButton> m_addFriendButtons;

	// Token: 0x040019E9 RID: 6633
	private List<PsUIGenericButton> m_unfollowButtons;

	// Token: 0x040019EA RID: 6634
	private bool m_alive = true;

	// Token: 0x040019EB RID: 6635
	private const float m_fbImageSize = 0.06f;

	// Token: 0x040019EC RID: 6636
	private const float m_namePicSpacing = 0.025f;
}
