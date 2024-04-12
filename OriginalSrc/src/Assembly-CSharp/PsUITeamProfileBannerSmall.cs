using System;

// Token: 0x02000383 RID: 899
public class PsUITeamProfileBannerSmall : PsUITeamProfileBanner
{
	// Token: 0x06001A00 RID: 6656 RVA: 0x0011F364 File Offset: 0x0011D764
	public PsUITeamProfileBannerSmall(UIComponent _parent, int _index, PlayerData _user, bool _createListInfo = false)
		: base(_parent, _index, _user, _createListInfo, true, true, false, false, false)
	{
	}

	// Token: 0x06001A01 RID: 6657 RVA: 0x0011F384 File Offset: 0x0011D784
	public override void CreateContent()
	{
		this.SetHeight(0.05f, RelativeTo.ScreenHeight);
		this.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.SetMargins(0.005f, 0.005f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		if (this.m_user.playerId == PlayerPrefsX.GetUserId())
		{
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.OwnProfileBannerDrawhandler));
		}
		else
		{
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ProfileBannerDrawhandler));
		}
		this.CreateTouchAreas();
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.0185f, RelativeTo.ScreenWidth);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, (this.m_index + 1).ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText.SetHorizontalAlign(1f);
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(this, false, string.Empty, this.m_user.facebookId, this.m_user.gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, true, true);
		psUIProfileImage.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
		UICanvas uicanvas2 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(0.175f, RelativeTo.ScreenWidth);
		uicanvas2.SetMargins(0f, 0.045f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		bool flag = PsMetagameManager.IsFriend(this.m_user.playerId);
		UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, this.m_user.name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, (!flag) ? "ffffff" : "#A7FF2B", "313131");
		uifittedText2.SetHorizontalAlign(0f);
		UICanvas uicanvas3 = new UICanvas(uifittedText2, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(0.04f, RelativeTo.ScreenHeight);
		uicanvas3.SetHorizontalAlign(1f);
		uicanvas3.SetMargins(0.05f, -0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas3.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_user.countryCode, null), true, true);
		uifittedSprite.SetHeight(0.035f, RelativeTo.ScreenHeight);
		if (this.m_showSeasonRewards)
		{
			UIText uitext = new UIText(uicanvas2, false, string.Empty, "Player", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, null, null);
			uitext.SetRogue();
			uitext.SetVerticalAlign(1f);
			uitext.SetMargins(0f, 0f, -0.06f, 0.06f, RelativeTo.ScreenHeight);
		}
		UICanvas uicanvas4 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(0.085f, RelativeTo.ScreenWidth);
		uicanvas4.RemoveDrawHandler();
		UIFittedText uifittedText3 = new UIFittedText(uicanvas4, false, string.Empty, this.m_user.teamRoleName, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#0A2440", null);
		UICanvas uicanvas5 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas5.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas5.SetWidth(0.085f, RelativeTo.ScreenWidth);
		uicanvas5.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas5.RemoveDrawHandler();
		UIFittedText uifittedText4 = new UIFittedText(uicanvas5, false, string.Empty, this.m_user.seasonReward.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FE48FF", null);
		UICanvas uicanvas6 = new UICanvas(uifittedText4, false, string.Empty, null, string.Empty);
		uicanvas6.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas6.SetWidth(0.04f, RelativeTo.ScreenHeight);
		uicanvas6.SetHorizontalAlign(0f);
		uicanvas6.SetMargins(-0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas6.RemoveDrawHandler();
		string currentSeasonRewardIcon = PsMetagameManager.m_seasonEndData.GetCurrentSeasonRewardIcon();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas6, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(currentSeasonRewardIcon, null), true, true);
		uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
		if (this.m_showSeasonRewards)
		{
			UIText uitext2 = new UIText(uicanvas5, false, string.Empty, "S Reward", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, null, null);
			uitext2.SetRogue();
			uitext2.SetVerticalAlign(1f);
			uitext2.SetMargins(-0.015f, 0.015f, -0.06f, 0.06f, RelativeTo.ScreenHeight);
		}
		UICanvas uicanvas7 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas7.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas7.SetWidth(0.085f, RelativeTo.ScreenWidth);
		uicanvas7.SetMargins(0.04f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas7.RemoveDrawHandler();
		int num = this.m_user.mcTrophies + this.m_user.carTrophies;
		UIFittedText uifittedText5 = new UIFittedText(uicanvas7, false, string.Empty, num.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#F5E141", null);
		uifittedText5.SetHorizontalAlign(0f);
		UICanvas uicanvas8 = new UICanvas(uifittedText5, false, string.Empty, null, string.Empty);
		uicanvas8.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas8.SetWidth(0.04f, RelativeTo.ScreenHeight);
		uicanvas8.SetHorizontalAlign(0f);
		uicanvas8.SetMargins(-0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas8.RemoveDrawHandler();
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas8, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_small_full", null), true, true);
		uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
		if (this.m_showSeasonRewards)
		{
			UIText uitext3 = new UIText(uicanvas7, false, string.Empty, "Trophies", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, null, null);
			uitext3.SetRogue();
			uitext3.SetVerticalAlign(1f);
			uitext3.SetMargins(0f, 0f, -0.06f, 0.06f, RelativeTo.ScreenHeight);
		}
	}
}
