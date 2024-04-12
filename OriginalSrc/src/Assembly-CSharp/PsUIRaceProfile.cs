using System;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class PsUIRaceProfile : UIComponent
{
	// Token: 0x060015A0 RID: 5536 RVA: 0x000E0678 File Offset: 0x000DEA78
	public PsUIRaceProfile(UIComponent _parent, RacerProfile _profile, bool _winScreen, bool _margin, int _position)
		: base(_parent, false, string.Empty, null, null, string.Empty)
	{
		this.m_profile = _profile;
		this.m_winScreen = _winScreen;
		this.SetHeight(0.085f, RelativeTo.ScreenHeight);
		this.SetWidth(0.5f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		this.CreateContent(_margin, _position);
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x000E06D0 File Offset: 0x000DEAD0
	public void SetCC()
	{
		this.m_profile.cc = ((int)PsUpgradeManager.GetCurrentPerformance(PsState.m_activeMinigame.m_playerUnit.GetType())).ToString();
		this.m_profileCCtext.SetText(this.m_profile.cc + "cc");
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x000E072C File Offset: 0x000DEB2C
	public virtual void CreateContent(bool _margin, int _position)
	{
		Color color = Color.Lerp(Color.grey, Color.black, 0.5f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, this.m_profile.name);
		uihorizontalList.SetHorizontalAlign(1f);
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(-0.05f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		if (this.m_winScreen)
		{
			this.m_positionSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.GetPositionIconName(_position), null), true, true);
			this.m_positionSprite.SetWidth(0.08f, RelativeTo.ScreenHeight);
		}
		UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.085f, 0.085f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0f, 0f, -0.01f, 0.01f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = uicanvas;
		bool flag = false;
		string empty = string.Empty;
		string fbId = this.m_profile.fbId;
		string gcId = this.m_profile.gcId;
		string hatIdentifier = this.m_profile.hatIdentifier;
		bool flag2 = this.m_profile.playerId == PlayerPrefsX.GetUserId();
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uicanvas2, flag, empty, fbId, gcId, -1, true, false, false, 0.075f, 0.06f, "fff9e6", hatIdentifier, flag2, true);
		if (!string.IsNullOrEmpty(this.m_profile.cc))
		{
			UIComponent uicomponent = new UIComponent(psUIProfileImage, false, string.Empty, null, null, string.Empty);
			uicomponent.SetHeight(0.325f, RelativeTo.ParentHeight);
			uicomponent.SetVerticalAlign(0f);
			uicomponent.SetMargins(0f, 0f, 0.4f, -0.4f, RelativeTo.OwnHeight);
			uicomponent.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicomponent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_cc_small", null), true, true);
			this.m_profileCCtext = new UIText(uifittedSprite, false, string.Empty, this.m_profile.cc + "cc", PsFontManager.GetFont(PsFonts.HurmeBold), 0.9f, RelativeTo.ParentHeight, "#C7FF0B", null);
		}
		psUIProfileImage.SetSize(1f, 1f, RelativeTo.ParentHeight);
		TransformS.SetRotation(psUIProfileImage.m_TC, new Vector3(0f, 0f, 3.5f));
		if (this.m_profile.wonAtCreate)
		{
			psUIProfileImage.m_material.shader = Shader.Find("WOE/Unlit/ColorUnlitTransparent");
			psUIProfileImage.m_material.color = color;
		}
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList.SetSpacing(0.005f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas3 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(0.335f, RelativeTo.ScreenHeight);
		uicanvas3.SetMargins(0f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas3.RemoveDrawHandler();
		string text = ((!this.m_profile.wonAtCreate) ? "ffffff" : "808080");
		UIFittedText uifittedText = new UIFittedText(uicanvas3, false, string.Empty, this.m_profile.name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, text, null);
		uifittedText.SetAlign(0f, 1f);
		UICanvas uicanvas4 = new UICanvas(uifittedText, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(0.067f, RelativeTo.ScreenHeight);
		uicanvas4.SetHorizontalAlign(1f);
		uicanvas4.SetMargins(0.075f, -0.075f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_profile.countryCode, null), true, true);
		uifittedSprite2.SetHeight(0.035f, RelativeTo.ScreenHeight);
		if (this.m_profile.wonAtCreate)
		{
			uifittedSprite2.SetColor(color);
		}
		if (!string.IsNullOrEmpty(this.m_profile.teamName))
		{
			UICanvas uicanvas5 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas5.SetHeight(0.025f, RelativeTo.ScreenHeight);
			uicanvas5.SetWidth(0.335f, RelativeTo.ScreenHeight);
			uicanvas5.SetMargins(0f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas5.RemoveDrawHandler();
			UIFittedText uifittedText2 = new UIFittedText(uicanvas5, false, string.Empty, this.m_profile.teamName, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFC002", null);
			uifittedText2.SetAlign(0f, 1f);
		}
		this.CreatePrize(uihorizontalList);
		this.CreateTrophies(uihorizontalList);
		if (!this.m_winScreen)
		{
			if (this.m_profile.wonAtCreate)
			{
				uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.StartFadedDrawhandler));
			}
			else
			{
				uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.StartDarkDrawhandler));
			}
		}
		else if (this.m_profile.playerId == PlayerPrefsX.GetUserId())
		{
			uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TiltedBlueDrawhandler));
		}
		else if (this.m_profile.wonAtCreate)
		{
			uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TiltedFadedDrawhandler));
		}
		else
		{
			uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TiltedDrawhandler));
		}
	}

	// Token: 0x060015A3 RID: 5539 RVA: 0x000E0CF8 File Offset: 0x000DF0F8
	protected virtual void CreateTrophies(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.SetRogue();
		uihorizontalList.SetAlign(1f, 0.45f);
		uihorizontalList.SetSpacing(0.0075f, RelativeTo.ScreenHeight);
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(this.TrophyDrawhandler));
		uihorizontalList.SetDepthOffset(-5f);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_trophy", null), true, true);
		uifittedSprite.SetHeight(0.05f, RelativeTo.ScreenHeight);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, this.m_profile.trophies.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), 0.025f, RelativeTo.ScreenHeight, "#FFFA52", null);
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x000E0DBC File Offset: 0x000DF1BC
	private void TrophyDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth - 0.0175f * (float)Screen.height, _c.m_actualHeight - 0.015f * (float)Screen.height, 0.01f * (float)Screen.height, 6, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		float t = _c.m_actualMargins.t;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.right * 0.0175f * (float)Screen.height + Vector3.down * t, roundedRect, 0.0065f * (float)Screen.height, DebugDraw.HexToColor("#525246"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.right * 0.0175f * (float)Screen.height + Vector3.down * t, ggdata, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060015A5 RID: 5541 RVA: 0x000E0ED8 File Offset: 0x000DF2D8
	public virtual void CreatePrize(UIComponent _parent)
	{
		string rewardIconName = this.GetRewardIconName(this.m_profile);
		if (!string.IsNullOrEmpty(rewardIconName) && this.m_profile.rival)
		{
			UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			uicanvas.SetSize(0.085f, 0.085f, RelativeTo.ScreenHeight);
			uicanvas.SetRogue();
			uicanvas.SetHorizontalAlign(0f);
			uicanvas.SetMargins(-1f, 1f, 0f, 0f, RelativeTo.OwnWidth);
			uicanvas.RemoveDrawHandler();
			this.m_rewardIcon = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(rewardIconName, null), true, true);
			this.m_rewardIcon.SetHeight(1f, RelativeTo.ParentHeight);
			if (this.m_profile.won)
			{
				this.m_winIcon = new UIFittedSprite(this.m_rewardIcon, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_chest_badge_active", null), true, true);
				this.m_winIcon.SetHeight(1f, RelativeTo.ParentHeight);
			}
			if (this.m_profile.rival && PsMetagameManager.m_vehicleGachaData.m_rivalWonCount >= 4 && (PsState.m_activeGameLoop as PsGameLoopRacing).m_initialGachaFull)
			{
				UICanvas uicanvas2 = new UICanvas(this.m_rewardIcon, false, string.Empty, null, string.Empty);
				uicanvas2.SetHeight(0.02f, RelativeTo.ScreenHeight);
				uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
				uicanvas2.SetAlign(0.5f, 1f);
				uicanvas2.SetMargins(0f, 0f, -0.025f, 0.025f, RelativeTo.ScreenHeight);
				uicanvas2.SetRogue();
				uicanvas2.RemoveDrawHandler();
				UIText uitext = new UIText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.SLOTS_FULL), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, null, "#000000");
			}
		}
		UICanvas uicanvas3 = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas3.SetSize(0.085f, 0.085f, RelativeTo.ScreenHeight);
		uicanvas3.RemoveDrawHandler();
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x000E10E0 File Offset: 0x000DF4E0
	public string GetPositionIconName(int _position)
	{
		string text = null;
		switch (_position)
		{
		case 0:
			text = "menu_position_1st";
			break;
		case 1:
			text = "menu_position_2nd";
			break;
		case 2:
			text = "menu_position_3rd";
			break;
		case 3:
			text = "menu_position_4th";
			break;
		}
		return text;
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x000E1138 File Offset: 0x000DF538
	public string GetRewardIconName(RacerProfile _profile)
	{
		string text = null;
		if (_profile.rival && _profile.wonAtCreate)
		{
			return "menu_chest_badge_active";
		}
		if (_profile.rival && !_profile.wonAtCreate)
		{
			return "menu_chest_badge_inactive";
		}
		switch (_profile.type)
		{
		case ResourceType.Coins:
			text = "menu_scoreboard_prize_coins";
			break;
		case ResourceType.Diamonds:
			text = "menu_scoreboard_prize_diamond";
			break;
		case ResourceType.Trophies:
			text = "menu_scoreboard_prize_chest";
			break;
		case ResourceType.Shards:
			text = "menu_resources_shard_icon";
			break;
		}
		return text;
	}

	// Token: 0x04001858 RID: 6232
	public bool m_winScreen;

	// Token: 0x04001859 RID: 6233
	public RacerProfile m_profile;

	// Token: 0x0400185A RID: 6234
	public UIFittedSprite m_positionSprite;

	// Token: 0x0400185B RID: 6235
	public UIFittedSprite m_rewardIcon;

	// Token: 0x0400185C RID: 6236
	public UIComponent m_winIcon;

	// Token: 0x0400185D RID: 6237
	private UIText m_profileCCtext;
}
