using System;
using System.Collections.Generic;
using Server;

// Token: 0x02000244 RID: 580
public class PsUIFollowButton : PsUIGenericButton
{
	// Token: 0x0600119B RID: 4507 RVA: 0x000AB0F0 File Offset: 0x000A94F0
	public PsUIFollowButton(UIComponent _parent, string _targetUserId, string _targetUserName, string _fbId, string _countryCode, string _gcId, int _mcTrophies, int _carTrophies, string _teamId, string _teamName, TeamRole _teamRole, string _teamRoleName, float _gradientSize = 0.25f, float _gradientPos = 0.25f, float _cornerSize = 0.005f, float _textAreaWidth = 0.175f, RelativeTo _textAreaWidthRelativeTo = RelativeTo.ScreenHeight)
		: base(_parent, _gradientSize, _gradientPos, _cornerSize, "Button")
	{
		this.m_textAreaWidth = _textAreaWidth;
		this.m_textAreaWidthRelativeTo = _textAreaWidthRelativeTo;
		this.m_user = default(PlayerData);
		this.m_user.playerId = _targetUserId;
		this.m_user.name = _targetUserName;
		this.m_user.facebookId = _fbId;
		this.m_user.gameCenterId = _gcId;
		this.m_user.countryCode = _countryCode;
		this.m_user.mcTrophies = _mcTrophies;
		this.m_user.carTrophies = _carTrophies;
		this.m_user.teamId = _teamId;
		this.m_user.teamName = _teamName;
		this.m_user.teamRole = _teamRole;
		this.m_user.teamRoleName = _teamRoleName;
		this.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		if (PsMetagameManager.m_friends == null)
		{
			PsMetagameManager.GetFriends(new Action<Friends>(this.CreateContent), false);
		}
		else
		{
			this.CreateContent(PsMetagameManager.m_friends);
		}
		this.m_created = true;
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x000AB20C File Offset: 0x000A960C
	public PsUIFollowButton(UIComponent _parent, PlayerData _player, float _gradientSize = 0.25f, float _gradientPos = 0.25f, float _cornerSize = 0.005f, float _textAreaWidth = 0.175f, RelativeTo _textAreaWidthRelativeTo = RelativeTo.ScreenHeight)
		: base(_parent, _gradientSize, _gradientPos, _cornerSize, "Button")
	{
		this.m_textAreaWidth = _textAreaWidth;
		this.m_textAreaWidthRelativeTo = _textAreaWidthRelativeTo;
		this.m_user = _player;
		this.SetHeight(0.085f, RelativeTo.ScreenHeight);
		if (PsMetagameManager.m_friends == null)
		{
			PsMetagameManager.GetFriends(new Action<Friends>(this.CreateContent), false);
		}
		else
		{
			this.CreateContent(PsMetagameManager.m_friends);
		}
		this.m_created = true;
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x000AB280 File Offset: 0x000A9680
	public void SetFollowText(string _text, float _fontSize = 0.03f)
	{
		base.SetFittedText(_text, _fontSize, this.m_textAreaWidth, this.m_textAreaWidthRelativeTo, false);
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x000AB297 File Offset: 0x000A9697
	public void SetCustomOkCallbackAction(Action _action)
	{
		this.m_customCallBackAction = _action;
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x000AB2A0 File Offset: 0x000A96A0
	public void SetVisualFollowAction(Action _action)
	{
		this.m_visualFollowAction = _action;
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x000AB2A9 File Offset: 0x000A96A9
	public void SetVisualUnfollowAction(Action _action)
	{
		this.m_visualUnfollowAction = _action;
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x000AB2B4 File Offset: 0x000A96B4
	public void CreateContent(Friends _c)
	{
		this.SetMargins(0.015f, 0.015f, 0f, 0f, RelativeTo.ScreenHeight);
		if (this.m_destroyed)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < _c.followees.Count; i++)
		{
			if (_c.followees[i].playerId == this.m_user.playerId)
			{
				flag = true;
				break;
			}
		}
		if (!flag && _c.IsFriend(this.m_user.playerId))
		{
			flag = true;
		}
		if (flag)
		{
			base.SetGreenColors(true);
			this.SetSpacing(0.005f, RelativeTo.ScreenHeight);
			base.SetIcon("menu_icon_profile_2", 0.9f, RelativeTo.ParentHeight, "#FFFFFF", default(cpBB));
			base.SetFittedText(PsStrings.Get(StringID.UNFOLLOW), 0.03f, this.m_textAreaWidth, this.m_textAreaWidthRelativeTo, false);
			this.m_followingCreator = true;
		}
		else
		{
			base.SetBlueColors(true);
			this.SetSpacing(0.005f, RelativeTo.ScreenHeight);
			base.SetIcon("menu_icon_profile_1", 0.9f, RelativeTo.ParentHeight, "#FFFFFF", default(cpBB));
			base.SetFittedText(PsStrings.Get(StringID.FOLLOW), 0.03f, this.m_textAreaWidth, this.m_textAreaWidthRelativeTo, false);
			this.m_followingCreator = false;
		}
		if (this.m_created)
		{
			this.Update();
		}
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x000AB420 File Offset: 0x000A9820
	public override void Step()
	{
		if (this.m_hit)
		{
			if (!this.m_followingCreator)
			{
				this.FollowCreator();
			}
			else
			{
				this.UnfollowCreator();
			}
		}
		base.Step();
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x000AB44F File Offset: 0x000A984F
	public override void Destroy()
	{
		this.m_destroyed = true;
		base.Destroy();
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x000AB460 File Offset: 0x000A9860
	public void FollowCreator()
	{
		Debug.Log("Following Creator", null);
		if (this.m_visualFollowAction != null)
		{
			this.m_visualFollowAction.Invoke();
		}
		List<string> followRewards = PsMetagameManager.GetFollowRewards(this.m_user.playerId);
		if (followRewards != null && followRewards.Count > 0)
		{
			List<PsCustomisationItem> list = new List<PsCustomisationItem>();
			for (int i = 0; i < followRewards.Count; i++)
			{
				PsCustomisationItem itemByIdentifier = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(followRewards[i]);
				if (itemByIdentifier != null && itemByIdentifier.m_category == PsCustomisationManager.CustomisationCategory.HAT && !itemByIdentifier.m_unlocked)
				{
					list.Add(itemByIdentifier);
					for (int j = 0; j < PsState.m_vehicleTypes.Length; j++)
					{
						PsCustomisationManager.UnlockItem(PsState.m_vehicleTypes[j], followRewards[i]);
					}
				}
			}
			PsUIHiddenHatPopup.CreatePopups(this.m_user.name, list, null);
		}
		new PsServerQueueFlow(null, delegate
		{
			FollowObject followObject = new FollowObject();
			followObject.playerID = this.m_user.playerId;
			followObject.customisations = PsCustomisationManager.GetUpdatedData(null);
			HttpC httpC = Player.Follow(followObject.playerID, followObject.customisations, new Action<HttpC>(this.FollowingSucceed), new Action<HttpC>(this.FollowingFailed), null);
			httpC.objectData = followObject;
		}, new string[] { "Follow", "SetData" });
		this.m_followingCreator = true;
		base.SetGreenColors(true);
		this.SetSpacing(0.005f, RelativeTo.ScreenHeight);
		base.SetIcon("menu_icon_profile_2", 0.9f, RelativeTo.ParentHeight, "#FFFFFF", default(cpBB));
		base.SetFittedText(PsStrings.Get(StringID.UNFOLLOW), 0.03f, this.m_textAreaWidth, this.m_textAreaWidthRelativeTo, false);
		this.Update();
		PsMetagameManager.m_friends.FollowPlayer(this.m_user);
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x000AB5E8 File Offset: 0x000A99E8
	public void FollowingSucceed(HttpC _c)
	{
		Debug.Log("FOLLOW PLAYER SUCCEED", null);
		if (this.m_customCallBackAction != null)
		{
			this.m_customCallBackAction.Invoke();
		}
		if (this.m_destroyed)
		{
			return;
		}
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x000AB618 File Offset: 0x000A9A18
	public void FollowingFailed(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			FollowObject followObject = (FollowObject)_c.objectData;
			HttpC httpC = Player.Follow(followObject.playerID, followObject.customisations, new Action<HttpC>(this.FollowingSucceed), new Action<HttpC>(this.FollowingFailed), null);
			httpC.objectData = followObject;
			return httpC;
		}, null);
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x000AB660 File Offset: 0x000A9A60
	public void UnfollowCreator()
	{
		Debug.Log("Unfollowing Creator", null);
		if (this.m_visualUnfollowAction != null)
		{
			this.m_visualUnfollowAction.Invoke();
		}
		new PsServerQueueFlow(null, delegate
		{
			HttpC httpC = Player.UnFollow(this.m_user.playerId, new Action<HttpC>(this.UnfollowingSucceed), new Action<HttpC>(this.UnfollowingFailed), null);
			httpC.objectData = this.m_user.playerId;
		}, new string[] { "Follow" });
		this.m_followingCreator = false;
		base.SetBlueColors(true);
		this.SetSpacing(0.005f, RelativeTo.ScreenHeight);
		base.SetIcon("menu_icon_profile_1", 0.9f, RelativeTo.ParentHeight, "#FFFFFF", default(cpBB));
		base.SetFittedText(PsStrings.Get(StringID.FOLLOW), 0.03f, this.m_textAreaWidth, this.m_textAreaWidthRelativeTo, false);
		this.Update();
		PsMetagameManager.m_friends.UnfollowPlayer(this.m_user.playerId);
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x000AB722 File Offset: 0x000A9B22
	public void UnfollowingSucceed(HttpC _c)
	{
		if (this.m_destroyed)
		{
			return;
		}
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x000AB730 File Offset: 0x000A9B30
	public void UnfollowingFailed(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC httpC = Player.UnFollow((string)_c.objectData, new Action<HttpC>(this.UnfollowingSucceed), new Action<HttpC>(this.UnfollowingFailed), null);
			httpC.objectData = (string)_c.objectData;
			return httpC;
		}, null);
	}

	// Token: 0x04001495 RID: 5269
	private bool m_followingCreator;

	// Token: 0x04001496 RID: 5270
	private bool m_created;

	// Token: 0x04001497 RID: 5271
	private float m_textAreaWidth;

	// Token: 0x04001498 RID: 5272
	private RelativeTo m_textAreaWidthRelativeTo;

	// Token: 0x04001499 RID: 5273
	private PlayerData m_user;

	// Token: 0x0400149A RID: 5274
	private bool m_destroyed;

	// Token: 0x0400149B RID: 5275
	private Action m_customCallBackAction;

	// Token: 0x0400149C RID: 5276
	private Action m_visualFollowAction;

	// Token: 0x0400149D RID: 5277
	private Action m_visualUnfollowAction;
}
