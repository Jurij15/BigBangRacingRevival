using System;

// Token: 0x02000370 RID: 880
public class PsUICenterTeamSettings : PsUICenterCreateNewTeam
{
	// Token: 0x06001991 RID: 6545 RVA: 0x001186B0 File Offset: 0x00116AB0
	public PsUICenterTeamSettings(UIComponent _parent)
		: base(_parent)
	{
		this.m_team = PsMetagameManager.m_team;
		this.m_nameField.SetText(this.m_team.name);
		this.m_teamDescription = this.m_team.description;
		this.m_requiredTrophies = this.m_team.requiredTrophies;
		this.m_joinIndex = (int)this.m_team.joinType;
		this.m_descField.m_value.SetText(this.m_team.description);
		this.m_trophyText.SetText(this.m_team.requiredTrophies.ToString());
		this.m_joinText.SetText(ClientTools.GetTeamJoinTypeName(this.m_team.joinType));
		this.m_nameField.m_textField.RemoveTouchAreas();
		this.m_nameField.m_textField.m_parent.RemoveTouchAreas();
		this.m_proceed.SetBlueColors(true);
		this.m_proceed.DestroyChildren(1);
		this.m_proceed.SetText(PsStrings.Get(StringID.TEAM_BUTTON_SAVE), 0.035f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		if (PlayerPrefsX.GetTeamRole() != TeamRole.Creator)
		{
			this.m_descField.RemoveTouchAreas();
			this.m_minus.Destroy();
			this.m_plus.Destroy();
			this.m_left.Destroy();
			this.m_right.Destroy();
			this.m_proceed.SetGrayColors();
			this.m_proceed.RemoveTouchAreas();
		}
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x00118828 File Offset: 0x00116C28
	public override void CreateHeaderContent(UIComponent _parent)
	{
		UIText uitext = new UIText(_parent, false, string.Empty, PsStrings.Get(StringID.TEAM_SETTINGS_HEADER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, "#84EDFF", null);
		uitext.SetVerticalAlign(1f);
		UIText uitext2 = new UIText(_parent, false, string.Empty, PsStrings.Get(StringID.TEAM_SETTINGS_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, "#84EDFF", null);
		uitext2.SetVerticalAlign(0f);
	}

	// Token: 0x06001993 RID: 6547 RVA: 0x001188A4 File Offset: 0x00116CA4
	public override void Proceed()
	{
		this.m_proceeding = true;
		this.m_team.description = this.m_teamDescription;
		this.m_team.joinType = (JoinType)this.m_joinIndex;
		this.m_team.requiredTrophies = this.m_requiredTrophies;
		this.m_waitingPopup = new PsUIBasePopup(typeof(PsUICenterTeamSettings.PsUIPopupSaveWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
		HttpC httpC = PsMetagameManager.SaveTeam(this.m_team, new Action<TeamData>(this.SaveOK), new Action<HttpC>(this.SaveFAILED), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
	}

	// Token: 0x06001994 RID: 6548 RVA: 0x00118940 File Offset: 0x00116D40
	private void SaveOK(TeamData _data)
	{
		Debug.Log("SAVE TEAM SUCCEED", null);
		PsMetagameManager.m_team = _data;
		PlayerPrefsX.SetTeamId(_data.id);
		PlayerPrefsX.SetTeamName(_data.name);
		PlayerPrefsX.SetTeamRole(_data.role);
		this.m_waitingPopup.Destroy();
		this.m_waitingPopup = null;
		(this.GetRoot() as PsUIBasePopup).CallAction("Save");
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x001189A8 File Offset: 0x00116DA8
	private void SaveFAILED(HttpC _c)
	{
		Debug.Log("SAVE TEAM FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerSaveTeam(_c.objectData as TeamData, new Action<TeamData>(this.SaveOK), new Action<HttpC>(this.SaveFAILED), null), null);
	}

	// Token: 0x04001C11 RID: 7185
	private TeamData m_team;

	// Token: 0x02000371 RID: 881
	private class PsUIPopupWaiting : PsUIHeaderedCanvas
	{
		// Token: 0x06001996 RID: 6550 RVA: 0x001189FC File Offset: 0x00116DFC
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

	// Token: 0x02000372 RID: 882
	private class PsUIPopupSaveWaiting : PsUICenterTeamSettings.PsUIPopupWaiting
	{
		// Token: 0x06001997 RID: 6551 RVA: 0x00118AC0 File Offset: 0x00116EC0
		public PsUIPopupSaveWaiting(UIComponent _parent)
			: base(_parent)
		{
			UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
			uiverticalList.RemoveDrawHandler();
			uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
			new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.TEAM_SAVE_INFO), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			new PsUILoadingAnimation(uiverticalList, false);
		}
	}
}
