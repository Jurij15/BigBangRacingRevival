using System;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class PsUICenterSeasonEnded : PsUIHeaderedCanvas
{
	// Token: 0x06001960 RID: 6496 RVA: 0x0011573C File Offset: 0x00113B3C
	public PsUICenterSeasonEnded(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.SetHeight(0.5f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.05f, 0.05f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x00115838 File Offset: 0x00113C38
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, PsStrings.Get(StringID.POPUP_SEASON_REWARDS_HEADER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x00115870 File Offset: 0x00113C70
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x001158F0 File Offset: 0x00113CF0
	public void CreateContent(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0f, 0f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		_parent.RemoveTouchAreas();
		string text = this.GetSeasonMessage();
		if (PsMetagameManager.m_seasonEndData.bigBangPoints > PsMetagameManager.m_playerStats.bigBangPoints)
		{
			int num = PsMetagameManager.m_seasonEndData.bigBangPoints - PsMetagameManager.m_playerStats.bigBangPoints;
			string text2 = PsStrings.Get(StringID.YOU_EARNED_BIG_BANG);
			text2 = text2.Replace("%1", num + string.Empty);
			text = text + "\n\n" + text2;
		}
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetMargins(0.05f, RelativeTo.ScreenHeight);
		PsMetagameManager.m_playerStats.bigBangPoints = PsMetagameManager.m_seasonEndData.bigBangPoints;
		PsMetagameManager.m_playerStats.mcTrophies = PsMetagameManager.m_seasonEndData.mcTrophies;
		PsMetagameManager.m_playerStats.carTrophies = PsMetagameManager.m_seasonEndData.carTrophies;
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_continue = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continue.SetAlign(1f, 1f);
		this.m_continue.SetMargins(0.02f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		if (PsMetagameManager.m_seasonEndData.teamEligibleForRewards && PsMetagameManager.m_seasonEndData.seasonTeamReward > 0)
		{
			this.m_continue.SetPurpleColors();
			this.m_continue.SetSpacing(0.02f, RelativeTo.ScreenHeight);
			UITextbox uitextbox2 = new UITextbox(this.m_continue, false, string.Empty, PsStrings.Get(StringID.BUTTON_REWARD_CLAIM), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, null);
			uitextbox2.SetWidth(0.2f, RelativeTo.ScreenHeight);
			UIHorizontalList uihorizontalList2 = new UIHorizontalList(this.m_continue, string.Empty);
			uihorizontalList2.SetHeight(0.04f, RelativeTo.ScreenHeight);
			uihorizontalList2.SetSpacing(0.01f, RelativeTo.ScreenHeight);
			uihorizontalList2.RemoveDrawHandler();
			string latestSeasonRewardIcon = PsMetagameManager.m_seasonEndData.GetLatestSeasonRewardIcon();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(latestSeasonRewardIcon, null), true, true);
			uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
			uifittedSprite.SetHorizontalAlign(0f);
			UIText uitext = new UIText(uihorizontalList2, false, string.Empty, PsMetagameManager.m_seasonEndData.seasonTeamReward.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, null, null);
			uitext.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
			uitext.SetHorizontalAlign(0f);
		}
		else
		{
			this.m_continue.SetGreenColors(true);
			this.m_continue.SetText(PsStrings.Get(StringID.CONTINUE), 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		}
	}

	// Token: 0x06001964 RID: 6500 RVA: 0x00115C28 File Offset: 0x00114028
	public override void Step()
	{
		if (this.m_continue.m_hit)
		{
			if (PsMetagameManager.m_seasonEndData.teamEligibleForRewards && PsMetagameManager.m_seasonEndData.seasonTeamReward > 0)
			{
				Vector2 vector = this.m_continue.m_camera.WorldToScreenPoint(this.m_continue.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
				PsMetagameManager.m_playerStats.CumulateCoinsWithFlyingResources(PsMetagameManager.m_seasonEndData.seasonTeamReward, vector, 0f);
				FrbMetrics.ReceiveVirtualCurrency("coins", (double)PsMetagameManager.m_seasonEndData.seasonTeamReward, "season_reward");
			}
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		base.Step();
	}

	// Token: 0x06001965 RID: 6501 RVA: 0x00115D04 File Offset: 0x00114104
	public string GetSeasonMessage()
	{
		string text = string.Empty;
		if (PsMetagameManager.m_team == null)
		{
			text = PsStrings.Get(StringID.POPUP_SEASON_NO_TEAM);
		}
		else if (!PsMetagameManager.m_seasonEndData.teamEligibleForRewards)
		{
			text = PsStrings.Get(StringID.POPUP_SEASON_NO_MEMBERS);
		}
		else if (PsMetagameManager.m_seasonEndData.seasonTeamReward > 0)
		{
			text = PsStrings.Get(StringID.POPUP_SEASON_REWARDS_TEXT);
		}
		else
		{
			text = PsStrings.Get(StringID.POPUP_SEASON_NO_TROPHIES);
		}
		return text;
	}

	// Token: 0x04001BF0 RID: 7152
	private PsUIGenericButton m_continue;
}
