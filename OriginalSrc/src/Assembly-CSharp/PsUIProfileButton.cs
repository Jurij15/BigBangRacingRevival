using System;
using UnityEngine;

// Token: 0x02000253 RID: 595
public class PsUIProfileButton : PsUIGenericButton
{
	// Token: 0x060011FC RID: 4604 RVA: 0x000B209C File Offset: 0x000B049C
	public PsUIProfileButton(UIComponent _parent, string _targetUserId, string _targetUserName, string _targetUserTag, string _fbId = null, string _countryCode = null, string _gcId = null, float _gradientSize = 0.25f, float _gradientPos = 0.25f, float _cornerSize = 0.005f, float _textAreaWidth = 0.175f)
		: base(_parent, _gradientSize, _gradientPos, _cornerSize, "Button")
	{
		this.m_user = default(PlayerData);
		this.m_user.playerId = _targetUserId;
		this.m_user.name = _targetUserName;
		this.m_user.tag = _targetUserTag;
		this.m_user.facebookId = _fbId;
		this.m_user.gameCenterId = _gcId;
		this.m_user.countryCode = _countryCode;
		this.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.CreateContent(PsMetagameManager.m_friends);
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x000B213C File Offset: 0x000B053C
	public PsUIProfileButton(UIComponent _parent, PlayerData _player, float _gradientSize = 0.25f, float _gradientPos = 0.25f, float _cornerSize = 0.005f, float _textAreaWidth = 0.175f)
		: base(_parent, _gradientSize, _gradientPos, _cornerSize, "Button")
	{
		this.m_user = _player;
		this.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.CreateContent(PsMetagameManager.m_friends);
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x000B217C File Offset: 0x000B057C
	public void CreateContent(Friends _c)
	{
		bool flag = false;
		bool flag2 = false;
		if (this.m_user.playerId == PlayerPrefsX.GetUserId())
		{
			flag2 = true;
		}
		if (!flag2)
		{
			for (int i = 0; i < _c.followees.Count; i++)
			{
				if (_c.followees[i].playerId == this.m_user.playerId)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				for (int j = 0; j < _c.friendList.Count; j++)
				{
					if (_c.friendList[j].playerId == this.m_user.playerId)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				base.SetGreenColors(true);
				base.SetIcon("menu_icon_profile_2", 0.065f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
				this.m_followingCreator = true;
			}
			else
			{
				base.SetBlueColors(true);
				base.SetIcon("menu_icon_profile_1", 0.065f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
				this.m_followingCreator = false;
			}
		}
		else
		{
			base.SetOrangeColors(true);
			base.SetIcon("menu_icon_profile_1", 0.065f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_followingCreator = false;
		}
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x000B22F0 File Offset: 0x000B06F0
	public override void Step()
	{
		if (this.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			PsUIBasePopup profile = new PsUIBasePopup(typeof(PsUICenterProfilePopup), null, null, null, true, true, InitialPage.Center, false, false, false);
			(profile.m_mainContent as PsUICenterProfilePopup).SetUser(this.m_user, false);
			profile.SetAction("Exit", delegate
			{
				profile.Destroy();
				profile = null;
				this.UpdateButtonInfo();
			});
			profile.Update();
			TweenS.AddTransformTween(profile.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		base.Step();
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x000B23BF File Offset: 0x000B07BF
	public void UpdateButtonInfo()
	{
		this.CreateContent(PsMetagameManager.m_friends);
		this.Update();
	}

	// Token: 0x0400151E RID: 5406
	private bool m_followingCreator;

	// Token: 0x0400151F RID: 5407
	private PlayerData m_user;

	// Token: 0x04001520 RID: 5408
	private bool m_destroyed;
}
