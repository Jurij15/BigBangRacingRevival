using System;
using System.Collections.Generic;

// Token: 0x020003E8 RID: 1000
public class Friends
{
	// Token: 0x06001C50 RID: 7248 RVA: 0x00140710 File Offset: 0x0013EB10
	public bool HasContacts()
	{
		return (this.friends != null && this.friends.Count > 0) || (this.followees != null && this.followees.Count > 0) || (this.followers != null && this.followers.Count > 0);
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x00140774 File Offset: 0x0013EB74
	public void FollowPlayer(PlayerData _player)
	{
		bool flag = false;
		for (int i = 0; i < this.followers.Count; i++)
		{
			if (this.followers[i].playerId == _player.playerId)
			{
				flag = true;
				this.followers.RemoveAt(i);
				break;
			}
		}
		if (flag)
		{
			this.friends[_player.playerId] = _player;
		}
		else
		{
			this.followees.Add(_player);
		}
	}

	// Token: 0x06001C52 RID: 7250 RVA: 0x00140804 File Offset: 0x0013EC04
	public void UnfollowPlayer(string _playerUserId)
	{
		bool flag = false;
		if (this.friends.ContainsKey(_playerUserId))
		{
			this.followers.Add(this.friends[_playerUserId]);
			this.friends.Remove(_playerUserId);
			flag = true;
		}
		if (!flag)
		{
			for (int i = 0; i < this.followees.Count; i++)
			{
				if (this.followees[i].playerId == _playerUserId)
				{
					this.followees.RemoveAt(i);
					break;
				}
			}
		}
	}

	// Token: 0x06001C53 RID: 7251 RVA: 0x0014089C File Offset: 0x0013EC9C
	public bool IsFriend(string _userId)
	{
		return !string.IsNullOrEmpty(_userId) && this.friends.ContainsKey(_userId);
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x001408B8 File Offset: 0x0013ECB8
	public bool IsFollower(string _userId)
	{
		return this.ListHasPlayer(_userId, this.followers);
	}

	// Token: 0x06001C55 RID: 7253 RVA: 0x001408C7 File Offset: 0x0013ECC7
	public bool IsFollowee(string _userId)
	{
		return this.ListHasPlayer(_userId, this.followees);
	}

	// Token: 0x06001C56 RID: 7254 RVA: 0x001408D8 File Offset: 0x0013ECD8
	private bool ListHasPlayer(string _userId, List<PlayerData> _contactList)
	{
		for (int i = 0; i < _contactList.Count; i++)
		{
			if (_contactList[i].playerId == _userId)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04001E39 RID: 7737
	public List<PlayerData> followees;

	// Token: 0x04001E3A RID: 7738
	public List<PlayerData> followers;

	// Token: 0x04001E3B RID: 7739
	public List<PlayerData> friendList;

	// Token: 0x04001E3C RID: 7740
	public Dictionary<string, PlayerData> friends;
}
