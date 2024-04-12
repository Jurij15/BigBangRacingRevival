using System;

// Token: 0x0200029D RID: 669
public class PsUICenterGameCardChallenge : PsUICenterGameCard
{
	// Token: 0x0600143A RID: 5178 RVA: 0x000CD61B File Offset: 0x000CBA1B
	public PsUICenterGameCardChallenge(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x000CD624 File Offset: 0x000CBA24
	public override void CreateHeaderText(UICanvas _parent)
	{
		string text = "Daily Diamonds!";
		this.m_contextText = new UIFittedText(_parent, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", "#5c3a0a");
		this.m_contextText.SetHorizontalAlign(0f);
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x000CD66C File Offset: 0x000CBA6C
	public override void CreateHeaderScore(UICanvas _parent)
	{
		int num = 0;
		if (this.m_metadataLoaded)
		{
			num = PsState.m_activeGameLoop.m_scoreBest;
		}
		string text = "menu_mode_diamond_" + num;
		this.m_scoreIcon = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
		this.m_scoreIcon.SetAlign(1f, 1f);
	}

	// Token: 0x0600143D RID: 5181 RVA: 0x000CD6E4 File Offset: 0x000CBAE4
	public override void SetMetadataInfo()
	{
		this.m_animation.Destroy();
		this.m_animation = null;
		string text = "item_player_monster_car_icon";
		if (PsState.m_activeGameLoop.GetPlayerUnit() == "Motorcycle")
		{
			text = "item_player_motorcycle_icon";
		}
		this.m_unitIcon.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame(text, null));
		this.m_unitIcon.SetOverrideShader(null);
		this.m_scoreIcon.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_mode_diamond_" + PsState.m_activeGameLoop.m_scoreBest, null));
		this.m_screenshot.m_gameId = PsState.m_activeGameLoop.GetGameId();
		this.m_screenshot.LoadPicture();
		this.m_profileIcon.m_facebookId = PsState.m_activeGameLoop.GetFacebookId();
		this.m_profileIcon.LoadPicture();
		this.CreateLevelAndCreatorNames(this.m_nameList);
		this.m_description.SetText(PsState.m_activeGameLoop.GetDescription());
		this.m_playButton.SetGreenColors(true);
		this.m_playButton.EnableTouchAreas(true);
		this.m_metadataLoaded = true;
		(this.GetRoot() as PsUIBasePopup).Update();
	}
}
