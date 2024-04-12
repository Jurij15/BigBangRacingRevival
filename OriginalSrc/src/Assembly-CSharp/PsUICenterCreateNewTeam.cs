using System;
using System.Collections;

// Token: 0x02000361 RID: 865
public class PsUICenterCreateNewTeam : PsUIHeaderedCanvas
{
	// Token: 0x06001928 RID: 6440 RVA: 0x00110ADC File Offset: 0x0010EEDC
	public PsUICenterCreateNewTeam(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.m_createModel = new UIModel(this, null);
		this.m_joinIndex = 0;
		this.m_requiredTrophies = 0;
		this.SetWidth(0.8f, RelativeTo.ScreenWidth);
		this.SetHeight(0.8f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateHeaderContent(this.m_header);
		this.CreateContent(this);
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x00110BF0 File Offset: 0x0010EFF0
	public virtual void CreateHeaderContent(UIComponent _parent)
	{
		UIText uitext = new UIText(_parent, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_CREATE_NEW_TEAM), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, "#84EDFF", null);
		uitext.SetVerticalAlign(1f);
		UIText uitext2 = new UIText(_parent, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_INFO), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, "#84EDFF", null);
		uitext2.SetVerticalAlign(0f);
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x00110C68 File Offset: 0x0010F068
	public void CreateContent(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetMargins(0f, 0f, 0.03f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(uicanvas, string.Empty);
		uiverticalList.SetWidth(0.625f, RelativeTo.ParentWidth);
		uiverticalList.SetAlign(0f, 1f);
		uiverticalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0.06f, 0.06f, 0f, 0f, RelativeTo.ScreenWidth);
		uiverticalList.RemoveDrawHandler();
		UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList2.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList2.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList2.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_TEAM_NAME), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, "#84EDFF", null);
		uitext.SetHorizontalAlign(0f);
		this.m_nameField = new PsUIGenericInputField(uiverticalList2, 0.36f, 0.06f, RelativeTo.ScreenWidth, RelativeTo.ScreenHeight, new cpBB(0.01f, 0.01f, 0.01f, 0.01f), true);
		this.m_nameField.SetMinMaxCharacterCount(3, 16);
		this.m_nameField.SetTextColor("#FCE63B");
		this.m_nameField.SetCallbacks(new Action<string>(this.DoneWriting), null);
		UIVerticalList uiverticalList3 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList3.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList3.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList3.RemoveDrawHandler();
		UIText uitext2 = new UIText(uiverticalList3, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_DESCRIPTION), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, "#84EDFF", null);
		uitext2.SetHorizontalAlign(0f);
		this.m_descField = new UITextArea(uiverticalList3, "DescriptionField", null, string.Empty, null, this.m_createModel, "m_teamDescription", 5, 128, "464646", "ffffff");
		this.m_descField.m_value.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.TextfieldDark));
		this.m_descField.m_value.SetMargins(0.01f, RelativeTo.ScreenHeight);
		this.m_descField.m_value.SetHeight(0.165f, RelativeTo.ScreenHeight);
		this.m_descField.m_value.m_fontSize = 0.0285f;
		UIVerticalList uiverticalList4 = new UIVerticalList(uicanvas, string.Empty);
		uiverticalList4.SetWidth(0.375f, RelativeTo.ParentWidth);
		uiverticalList4.SetAlign(1f, 1f);
		uiverticalList4.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uiverticalList4.SetMargins(0f, 0.03f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList4.RemoveDrawHandler();
		UIVerticalList uiverticalList5 = new UIVerticalList(uiverticalList4, string.Empty);
		uiverticalList5.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList5.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList5.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uiverticalList5, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_REQ_TROPHIES), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, "84EDFF", true, null);
		uitextbox.SetHorizontalAlign(0f);
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList5, string.Empty);
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHeight(0.08f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.RemoveDrawHandler();
		this.m_minus = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_minus.SetIcon("editor_draw_sensory_button_minus", 0.04f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_minus.SetMargins(0.01f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_minus.SetHeight(0.06f, RelativeTo.ScreenHeight);
		UICanvas uicanvas2 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(0.15f, RelativeTo.ScreenWidth);
		uicanvas2.SetMargins(0.05f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(0.04f, RelativeTo.ScreenHeight);
		uicanvas3.SetHorizontalAlign(0f);
		uicanvas3.SetMargins(-0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas3.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_small_full", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_trophyText = new UIFittedText(uicanvas2, false, string.Empty, this.m_requiredTrophies.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		this.m_trophyText.SetHorizontalAlign(0.5f);
		this.m_plus = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_plus.SetIcon("editor_draw_sensory_button_plus", 0.04f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_plus.SetMargins(0.01f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_plus.SetHeight(0.06f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList6 = new UIVerticalList(uiverticalList4, string.Empty);
		uiverticalList6.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList6.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList6.RemoveDrawHandler();
		UITextbox uitextbox2 = new UITextbox(uiverticalList5, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_JOIN_TYPE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, "84EDFF", true, null);
		uitextbox2.SetHorizontalAlign(0f);
		uitextbox2.SetWidth(1f, RelativeTo.ParentWidth);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList6, string.Empty);
		uihorizontalList2.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList2.SetHeight(0.08f, RelativeTo.ScreenHeight);
		uihorizontalList2.SetHorizontalAlign(0f);
		uihorizontalList2.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_triangle", null);
		frame.flipX = true;
		this.m_left = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_left.SetIcon(frame, 0.04f, "#FFFFFF", default(cpBB));
		this.m_left.SetMargins(0.01f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_left.SetHeight(0.06f, RelativeTo.ScreenHeight);
		UICanvas uicanvas4 = new UICanvas(uihorizontalList2, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(0.15f, RelativeTo.ScreenWidth);
		uicanvas4.RemoveDrawHandler();
		this.m_joinText = new UIFittedText(uicanvas4, false, string.Empty, ClientTools.GetTeamJoinTypeName(this.m_joinIndex), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		this.m_right = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_right.SetIcon("menu_icon_triangle", 0.04f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_right.SetMargins(0.02f, 0.01f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_right.SetHeight(0.06f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList7 = new UIVerticalList(uicanvas, string.Empty);
		uiverticalList7.SetMargins(0f, 0f, 0.03f, 0f, RelativeTo.ScreenHeight);
		uiverticalList7.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList7.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList7.SetAlign(0.5f, 0f);
		uiverticalList7.RemoveDrawHandler();
		this.m_proceed = new PsUIGenericButton(uiverticalList7, 0.25f, 0.25f, 0.005f, "Button");
		this.m_proceed.SetGreenColors(true);
		this.m_proceed.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_proceed.SetText(PsStrings.Get(StringID.TEAM_CREATE_BUTTON).ToUpper(), 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(this.m_proceed, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_price", null), true, true);
		uifittedSprite2.SetHeight(0.05f, RelativeTo.ScreenHeight);
		UIText uitext3 = new UIText(this.m_proceed, false, string.Empty, "1000", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, "#FEE43A", "#1f6500");
		uitext3.SetMargins(-0.01f, 0.01f, 0f, 0f, RelativeTo.ScreenHeight);
		if (!PlayerPrefsX.GetTeamJoined())
		{
			string text = PsStrings.Get(StringID.TEAMS_INSTANT_REWARD);
			text = text.Replace("%1", 50.ToString());
			UIText uitext4 = new UIText(uiverticalList7, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
			UICanvas uicanvas5 = new UICanvas(uitext4, false, string.Empty, null, string.Empty);
			uicanvas5.SetHeight(1f, RelativeTo.ParentHeight);
			uicanvas5.SetWidth(1f, RelativeTo.ParentHeight);
			uicanvas5.SetHorizontalAlign(1f);
			uicanvas5.SetMargins(0.04f, -0.04f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas5.RemoveDrawHandler();
			UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas5, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_diamond_small_full", null), true, true);
			uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
		}
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x0011164B File Offset: 0x0010FA4B
	public void DoneWriting(string _input)
	{
		this.m_teamName = _input;
	}

	// Token: 0x0600192C RID: 6444 RVA: 0x00111654 File Offset: 0x0010FA54
	private void DescriptionDoneCallback(string _value)
	{
		Debug.Log("DONE: " + _value, null);
		this.m_descField.SetValue(_value);
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x00111673 File Offset: 0x0010FA73
	private void KeyPressedCallback(string _value)
	{
		Debug.Log("PRESSED: " + _value, null);
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x00111686 File Offset: 0x0010FA86
	private void CancelCallback()
	{
		Debug.Log("CANCEL", null);
	}

	// Token: 0x0600192F RID: 6447 RVA: 0x00111694 File Offset: 0x0010FA94
	public virtual void Proceed()
	{
		if (string.IsNullOrEmpty(this.m_teamName))
		{
			PsUIBasePopup popup2 = new PsUIBasePopup(typeof(PsUICenterCreateNewTeam.PsUIPopupTeamNamePrompt), null, null, null, false, true, InitialPage.Center, false, false, false);
			popup2.SetAction("Exit", delegate
			{
				popup2.Destroy();
			});
			return;
		}
		if (PsMetagameManager.m_playerStats.coins < 1000)
		{
			PsMetagameManager.m_coinsToDiamondsConvertAmount = 1000 - PsMetagameManager.m_playerStats.coins;
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterNotEnoughCoinsConversion), null, null, null, true, true, InitialPage.Center, false, false, false);
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
			});
			popup.SetAction("Upgrade", delegate
			{
				popup.Destroy();
				PsMetagameManager.m_playerStats.coins = 1000;
				PsMetagameManager.m_playerStats.diamonds -= PsMetagameManager.CoinsToDiamonds(PsMetagameManager.m_coinsToDiamondsConvertAmount, true);
				PsMetagameManager.m_coinsToDiamondsConvertAmount = 0;
				this.Proceed();
			});
			PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, false, false, false);
			return;
		}
		this.m_proceeding = true;
		TeamData teamData = new TeamData();
		teamData.name = this.m_teamName;
		teamData.description = this.m_teamDescription;
		teamData.joinType = (JoinType)this.m_joinIndex;
		teamData.requiredTrophies = this.m_requiredTrophies;
		this.m_waitingPopup = new PsUIBasePopup(typeof(PsUICenterCreateNewTeam.PsUIPopupCreateWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
		HttpC httpC = PsMetagameManager.CreateNewTeam(teamData, new Action<TeamData>(this.CreateOK), new Action<HttpC>(this.CreateFAILED), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
	}

	// Token: 0x06001930 RID: 6448 RVA: 0x00111820 File Offset: 0x0010FC20
	private void CreateOK(TeamData _data)
	{
		if (_data != null && !string.IsNullOrEmpty(_data.id))
		{
			if (!PlayerPrefsX.GetTeamJoined())
			{
				PsMetagameManager.m_playerStats.CumulateDiamonds(50);
			}
			PsMetagameManager.m_playerStats.CumulateCoins(-1000);
			PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
			Debug.Log("CREATE TEAM SUCCEED", null);
			PsMetagameManager.m_team = _data;
			PlayerPrefsX.SetTeamId(_data.id);
			PlayerPrefsX.SetTeamName(_data.name);
			PlayerPrefsX.SetTeamRole(_data.role);
			PlayerPrefsX.SetTeamJoined(true);
			PsMetrics.TeamCreated(_data.id, _data.name, _data.joinType.ToString(), _data.requiredTrophies.ToString());
			FrbMetrics.SpendVirtualCurrency("team_creation", "coins", 1000.0);
			(this.GetRoot() as PsUIBasePopup).CallAction("Create");
		}
		else
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Error");
		}
		this.m_waitingPopup.Destroy();
		this.m_waitingPopup = null;
	}

	// Token: 0x06001931 RID: 6449 RVA: 0x00111974 File Offset: 0x0010FD74
	private void CreateFAILED(HttpC _c)
	{
		Debug.Log("CREATE TEAM FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerCreateNewTeam(_c.objectData as TeamData, new Action<TeamData>(this.CreateOK), new Action<HttpC>(this.CreateFAILED), null), null);
	}

	// Token: 0x06001932 RID: 6450 RVA: 0x001119C8 File Offset: 0x0010FDC8
	public override void Step()
	{
		if (this.m_left != null && this.m_left.m_hit)
		{
			this.m_joinIndex--;
			if (this.m_joinIndex < 0)
			{
				this.m_joinIndex = Enum.GetNames(typeof(JoinType)).Length - 1;
			}
			this.m_joinText.SetText(ClientTools.GetTeamJoinTypeName(this.m_joinIndex));
			this.m_joinText.Update();
		}
		else if (this.m_right != null && this.m_right.m_hit)
		{
			this.m_joinIndex++;
			if (this.m_joinIndex >= Enum.GetNames(typeof(JoinType)).Length)
			{
				this.m_joinIndex = 0;
			}
			this.m_joinText.SetText(ClientTools.GetTeamJoinTypeName(this.m_joinIndex));
			this.m_joinText.Update();
		}
		else if (this.m_minus != null && this.m_minus.m_hit)
		{
			this.m_requiredTrophies -= 100;
			if (this.m_requiredTrophies < 0)
			{
				this.m_requiredTrophies = 0;
			}
			this.m_trophyText.SetText(this.m_requiredTrophies.ToString());
			this.m_trophyText.Update();
		}
		else if (this.m_plus != null && this.m_plus.m_hit)
		{
			this.m_requiredTrophies += 100;
			if (this.m_requiredTrophies > 6000)
			{
				this.m_requiredTrophies = 6000;
			}
			this.m_trophyText.SetText(this.m_requiredTrophies.ToString());
			this.m_trophyText.Update();
		}
		else if (this.m_descField.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			UITextInput uitextInput = new UITextInput(null, new Action<string>(this.DescriptionDoneCallback), new Action(this.CancelCallback), null, 1, true, 128);
			uitextInput.m_textbox.SetColor("ffffff", null);
			uitextInput.m_textbox.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.TextfieldDark));
			uitextInput.SetText((string)this.m_descField.GetValue());
			uitextInput.Update();
		}
		else if (this.m_proceed != null && this.m_proceed.m_hit && !this.m_proceeding)
		{
			TouchAreaS.CancelAllTouches(null);
			this.Proceed();
		}
		base.Step();
	}

	// Token: 0x04001BB2 RID: 7090
	public UIModel m_createModel;

	// Token: 0x04001BB3 RID: 7091
	public string m_teamName = string.Empty;

	// Token: 0x04001BB4 RID: 7092
	public string m_teamDescription = string.Empty;

	// Token: 0x04001BB5 RID: 7093
	protected UITextArea m_descField;

	// Token: 0x04001BB6 RID: 7094
	protected PsUIGenericInputField m_nameField;

	// Token: 0x04001BB7 RID: 7095
	protected PsUIGenericButton m_minus;

	// Token: 0x04001BB8 RID: 7096
	protected PsUIGenericButton m_plus;

	// Token: 0x04001BB9 RID: 7097
	protected UIFittedText m_trophyText;

	// Token: 0x04001BBA RID: 7098
	protected int m_requiredTrophies;

	// Token: 0x04001BBB RID: 7099
	protected PsUIGenericButton m_left;

	// Token: 0x04001BBC RID: 7100
	protected PsUIGenericButton m_right;

	// Token: 0x04001BBD RID: 7101
	protected UIFittedText m_joinText;

	// Token: 0x04001BBE RID: 7102
	protected int m_joinIndex;

	// Token: 0x04001BBF RID: 7103
	protected PsUIGenericButton m_proceed;

	// Token: 0x04001BC0 RID: 7104
	protected bool m_proceeding;

	// Token: 0x04001BC1 RID: 7105
	protected PsUIBasePopup m_waitingPopup;

	// Token: 0x02000362 RID: 866
	private class PsUIPopupTeamNamePrompt : PsUIHeaderedCanvas
	{
		// Token: 0x06001933 RID: 6451 RVA: 0x00111C64 File Offset: 0x00110064
		public PsUIPopupTeamNamePrompt(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
			this.SetWidth(0.65f, RelativeTo.ScreenWidth);
			this.SetHeight(0.45f, RelativeTo.ScreenHeight);
			this.SetVerticalAlign(0.4f);
			this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.Destroy();
			this.CreateContent();
			UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
			uicanvas.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
			uicanvas.SetAlign(0.5f, 0f);
			uicanvas.SetMargins(0f, 0f, 0.03f, -0.03f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			this.m_ok = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
			this.m_ok.SetGreenColors(true);
			this.m_ok.SetText("OK", 0.045f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_ok.SetAlign(0.5f, 0f);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00111DE4 File Offset: 0x001101E4
		public virtual void CreateContent()
		{
			UITextbox uitextbox = new UITextbox(this, false, string.Empty, PsStrings.Get(StringID.TEAM_NAME_INFO), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			uitextbox.SetWidth(0.4f, RelativeTo.ScreenWidth);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00111E26 File Offset: 0x00110226
		public override void Step()
		{
			if (this.m_ok.m_hit)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
			base.Step();
		}

		// Token: 0x04001BC8 RID: 7112
		protected PsUIGenericButton m_ok;
	}

	// Token: 0x02000363 RID: 867
	private class PsUIPopupTeamCoinPrompt : PsUICenterCreateNewTeam.PsUIPopupTeamNamePrompt
	{
		// Token: 0x06001936 RID: 6454 RVA: 0x00111E54 File Offset: 0x00110254
		public PsUIPopupTeamCoinPrompt(UIComponent _parent)
			: base(_parent)
		{
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00111E60 File Offset: 0x00110260
		public override void CreateContent()
		{
			UITextbox uitextbox = new UITextbox(this, false, string.Empty, PsStrings.Get(StringID.TEAM_COST_INFO), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			uitextbox.SetWidth(0.4f, RelativeTo.ScreenWidth);
		}
	}

	// Token: 0x02000364 RID: 868
	private class PsUIPopupWaiting : PsUIHeaderedCanvas
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x00111EA4 File Offset: 0x001102A4
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

	// Token: 0x02000365 RID: 869
	private class PsUIPopupCreateWaiting : PsUICenterCreateNewTeam.PsUIPopupWaiting
	{
		// Token: 0x06001939 RID: 6457 RVA: 0x00111F68 File Offset: 0x00110368
		public PsUIPopupCreateWaiting(UIComponent _parent)
			: base(_parent)
		{
			UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
			uiverticalList.RemoveDrawHandler();
			uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
			new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.TEAM_CREATE_MESSAGE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			new PsUILoadingAnimation(uiverticalList, false);
		}
	}
}
