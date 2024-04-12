using System;
using Server;

// Token: 0x02000257 RID: 599
public class PsUIRoleButton : PsUIGenericButton
{
	// Token: 0x06001214 RID: 4628 RVA: 0x000B34E8 File Offset: 0x000B18E8
	public PsUIRoleButton(UIComponent _parent, string _targetUserId, string _teamId, TeamRole _teamRole)
		: base(_parent, 0.25f, 0.25f, 0.005f, "Button")
	{
		this.m_user = default(PlayerData);
		this.m_user.playerId = _targetUserId;
		this.m_user.teamId = _teamId;
		this.m_user.teamRole = _teamRole;
		this.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_created = true;
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x000B3566 File Offset: 0x000B1966
	public void SetCustomCallback(Action _action)
	{
		this.m_customCallBackAction = _action;
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x000B3570 File Offset: 0x000B1970
	public override void Step()
	{
		if (this.m_hit)
		{
			this.m_kickPopup = new PsUIBasePopup(typeof(PsUICenterKick), null, null, null, true, true, InitialPage.Center, false, false, false);
			(this.m_kickPopup.m_mainContent as PsUICenterKick).SetCustomCallback(new Action<string>(this.InputCallback));
			this.m_kickPopup.SetAction("Continue", delegate
			{
				this.Kick();
			});
			this.m_kickPopup.SetAction("Exit", delegate
			{
				this.m_kickPopup.Destroy();
				this.m_kickPopup = null;
			});
		}
		base.Step();
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x000B3605 File Offset: 0x000B1A05
	public void InputCallback(string _input)
	{
		this.m_kickReason = _input;
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000B360E File Offset: 0x000B1A0E
	public override void Destroy()
	{
		this.m_destroyed = true;
		base.Destroy();
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x000B3620 File Offset: 0x000B1A20
	public void Kick()
	{
		this.m_waitingPopup = new PsUIBasePopup(typeof(PsUIRoleButton.PsUIPopupKickWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
		new PsServerQueueFlow(null, delegate
		{
			RoleData roleData = new RoleData();
			roleData.playerID = this.m_user.playerId;
			roleData.teamID = this.m_user.teamId;
			roleData.text = this.m_kickReason;
			Debug.Log(string.Concat(new string[] { "KICK: ", roleData.playerID, ", ", roleData.teamID, ", ", roleData.text }), null);
			HttpC httpC = Team.Kick(roleData.teamID, roleData.playerID, roleData.text, new Action<HttpC>(this.KickSucceed), new Action<HttpC>(this.KickFailed), null);
			httpC.objectData = roleData;
		}, new string[] { "Role", "SetData" });
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x000B3674 File Offset: 0x000B1A74
	public void KickSucceed(HttpC _c)
	{
		Debug.Log("KICK PLAYER SUCCEED", null);
		RoleData roleData = _c.objectData as RoleData;
		if (roleData != null)
		{
			PsMetrics.PlayerKickedFromTeam(roleData.teamID, roleData.playerID);
		}
		PsMetagameManager.GetOwnTeam(new Action<TeamData>(this.TeamGetOK), true);
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x000B36C4 File Offset: 0x000B1AC4
	public virtual void TeamGetOK(TeamData _data)
	{
		Debug.Log("GET TEAM SUCCEED", null);
		PsMetagameManager.m_team = _data;
		PsUICenterMyTeam.m_updateMembers = true;
		if (this.m_waitingPopup != null)
		{
			this.m_waitingPopup.Destroy();
		}
		if (this.m_kickPopup != null)
		{
			this.m_kickPopup.Destroy();
		}
		this.m_waitingPopup = null;
		this.m_kickPopup = null;
		if (this.m_customCallBackAction != null)
		{
			this.m_customCallBackAction.Invoke();
		}
		if (this.m_destroyed)
		{
			return;
		}
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x000B3744 File Offset: 0x000B1B44
	public void KickFailed(HttpC _c)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			RoleData roleData = (RoleData)_c.objectData;
			HttpC httpC = Team.Kick(roleData.teamID, roleData.playerID, roleData.text, new Action<HttpC>(this.KickSucceed), new Action<HttpC>(this.KickFailed), null);
			httpC.objectData = roleData;
			return httpC;
		}, null);
	}

	// Token: 0x04001544 RID: 5444
	private bool m_followingCreator;

	// Token: 0x04001545 RID: 5445
	private bool m_created;

	// Token: 0x04001546 RID: 5446
	private float m_textAreaWidth;

	// Token: 0x04001547 RID: 5447
	private PlayerData m_user;

	// Token: 0x04001548 RID: 5448
	private bool m_destroyed;

	// Token: 0x04001549 RID: 5449
	private Action m_customCallBackAction;

	// Token: 0x0400154A RID: 5450
	private string m_kickReason;

	// Token: 0x0400154B RID: 5451
	private PsUIBasePopup m_kickPopup;

	// Token: 0x0400154C RID: 5452
	private PsUIBasePopup m_waitingPopup;

	// Token: 0x02000258 RID: 600
	private class PsUIPopupWaiting : PsUIHeaderedCanvas
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x000B3868 File Offset: 0x000B1C68
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

	// Token: 0x02000259 RID: 601
	private class PsUIPopupKickWaiting : PsUIRoleButton.PsUIPopupWaiting
	{
		// Token: 0x06001221 RID: 4641 RVA: 0x000B392C File Offset: 0x000B1D2C
		public PsUIPopupKickWaiting(UIComponent _parent)
			: base(_parent)
		{
			UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
			uiverticalList.RemoveDrawHandler();
			uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
			new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.KICKIN_PROGRESS_TEXT), PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			new PsUILoadingAnimation(uiverticalList, false);
		}
	}
}
