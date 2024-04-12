using System;
using System.Collections.Generic;

// Token: 0x02000276 RID: 630
public class PsUILeaderboardListFriends : PsUILeaderboardList
{
	// Token: 0x060012CB RID: 4811 RVA: 0x000B9B61 File Offset: 0x000B7F61
	public PsUILeaderboardListFriends(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x000B9B6A File Offset: 0x000B7F6A
	public override void CreateContent(Leaderboard _leaderboard)
	{
		_leaderboard.friends = this.AddPlayerToFriendList(_leaderboard.friends);
		this.CreateContent(_leaderboard.friends);
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x000B9B8C File Offset: 0x000B7F8C
	protected override void Load()
	{
		if (string.IsNullOrEmpty(PlayerPrefsX.GetFacebookId()))
		{
			this.m_fbButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
			this.m_fbButton.SetIcon("menu_icon_facebook", 0.045f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_fbButton.SetText(PsStrings.Get(StringID.FACEBOOK_CONNECT), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		}
		new PsUILoadingAnimation(this, false);
		this.Update();
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x000B9C1C File Offset: 0x000B801C
	public override void Step()
	{
		if (this.m_fbButton != null && this.m_fbButton.m_hit)
		{
			this.m_waitingPopup = new PsUIBasePopup(typeof(PsUIPopupFacebookWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
			FacebookManager.Login(new Action(this.FacebookLoginDone));
		}
		base.Step();
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x000B9C7C File Offset: 0x000B807C
	private List<LeaderboardEntry> AddPlayerToFriendList(List<LeaderboardEntry> _entryList)
	{
		if (_entryList == null)
		{
			_entryList = new List<LeaderboardEntry>();
		}
		for (int i = 0; i < _entryList.Count; i++)
		{
			if (_entryList[i].user.playerId == PlayerPrefsX.GetUserId())
			{
				return _entryList;
			}
		}
		LeaderboardEntry leaderboardEntry = default(LeaderboardEntry);
		leaderboardEntry.user.name = PlayerPrefsX.GetUserName();
		leaderboardEntry.user.playerId = PlayerPrefsX.GetUserId();
		leaderboardEntry.user.facebookId = PlayerPrefsX.GetFacebookId();
		leaderboardEntry.user.gameCenterId = PlayerPrefsX.GetGameCenterId();
		leaderboardEntry.trophies = PsMetagameManager.m_playerStats.trophies;
		leaderboardEntry.user.countryCode = PlayerPrefsX.GetCountryCode();
		bool flag = false;
		for (int j = 0; j < _entryList.Count; j++)
		{
			if (_entryList[j].trophies <= PsMetagameManager.m_playerStats.trophies)
			{
				_entryList.Insert(j, leaderboardEntry);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			_entryList.Add(leaderboardEntry);
		}
		return _entryList;
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x000B9D9C File Offset: 0x000B819C
	private void FacebookLoginDone()
	{
		Debug.Log("Facebook login done", null);
		this.DestroyChildren();
		this.m_created = false;
		this.m_fbButton = null;
		(this.m_parent.m_parent as PsUICenterLeaderboard).LoadData();
		this.Load();
		if (this.m_waitingPopup != null)
		{
			this.m_waitingPopup.Destroy();
			this.m_waitingPopup = null;
		}
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x000B9E01 File Offset: 0x000B8201
	protected override void DestroyContent()
	{
		if (this.m_fbButton == null)
		{
			base.DestroyChildren(0);
		}
		else
		{
			base.DestroyChildren(1);
		}
	}

	// Token: 0x040015D2 RID: 5586
	private PsUIGenericButton m_fbButton;

	// Token: 0x040015D3 RID: 5587
	private PsUIBasePopup m_waitingPopup;
}
