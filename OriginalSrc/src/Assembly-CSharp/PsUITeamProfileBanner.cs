using System;
using UnityEngine;

// Token: 0x02000381 RID: 897
public class PsUITeamProfileBanner : UIHorizontalList
{
	// Token: 0x060019FA RID: 6650 RVA: 0x0011E7D4 File Offset: 0x0011CBD4
	public PsUITeamProfileBanner(UIComponent _parent, int _index, PlayerData _user, bool _showSeasonRewards = false, bool _showPos = true, bool _showRole = true, bool _inTeamPopup = false, bool _showLikes = false, bool _downloadProfile = false)
		: base(_parent, "TeamProfileBanner")
	{
		this.m_parentWidth = _parent.m_width;
		this.m_parentRelative = _parent.m_widthRelativeTo;
		this.m_showRole = _showRole;
		this.m_showPos = _showPos;
		this.m_showSeasonRewards = _showSeasonRewards;
		this.m_showLikes = _showLikes;
		this.m_downloadProfile = _downloadProfile;
		this.m_user = _user;
		this.m_index = _index;
		this.m_inTeamPopup = _inTeamPopup;
		this.CreateContent();
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x0011E84C File Offset: 0x0011CC4C
	public virtual void CreateContent()
	{
		this.SetMargins(0.015f, 0.015f, 0.0075f, 0.0075f, RelativeTo.ScreenHeight);
		this.SetHeight(0.05f, RelativeTo.ScreenHeight);
		this.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		this.CreateTouchAreas();
		this.m_TAC.m_letTouchesThrough = true;
		if (this.m_user.playerId == PlayerPrefsX.GetUserId())
		{
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.OwnProfileBannerDrawhandler));
		}
		else
		{
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ProfileBannerDrawhandler));
		}
		if (this.m_showPos)
		{
			UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.025f, RelativeTo.ScreenHeight);
			uicanvas.SetWidth(0.05f * this.m_parentWidth, this.m_parentRelative);
			uicanvas.SetMargins(0f, 0.015f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, this.m_index + 1 + ".", PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, null, null);
			uifittedText.SetHorizontalAlign(1f);
		}
		UICanvas uicanvas2 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(0.4f * this.m_parentWidth, this.m_parentRelative);
		uicanvas2.SetMargins(0.065f, 0.045f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas3.SetSize(0.045f, 0.045f, RelativeTo.ScreenHeight);
		uicanvas3.SetMargins(-0.055f, 0.055f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas3.SetHorizontalAlign(0f);
		uicanvas3.RemoveDrawHandler();
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uicanvas3, false, string.Empty, this.m_user.facebookId, this.m_user.gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, true, true);
		psUIProfileImage.SetSize(0.045f, 0.045f, RelativeTo.ScreenHeight);
		bool flag = PsMetagameManager.IsFriend(this.m_user.playerId);
		UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, this.m_user.name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, (!flag) ? "ffffff" : "#A7FF2B", "313131");
		uifittedText2.SetHorizontalAlign(0f);
		UICanvas uicanvas4 = new UICanvas(uifittedText2, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(0.04f, RelativeTo.ScreenHeight);
		uicanvas4.SetHorizontalAlign(1f);
		uicanvas4.SetMargins(0.05f, -0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_user.countryCode, null), true, true);
		uifittedSprite.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uifittedSprite.SetHorizontalAlign(1f);
		UICanvas uicanvas5 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas5.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas5.SetWidth(0.065f * this.m_parentWidth, this.m_parentRelative);
		uicanvas5.SetMargins(0.005f, 0.01f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas5.SetDrawHandler(new UIDrawDelegate(this.SpacerDrawhandler));
		if (this.m_showRole && this.m_user.teamRole == TeamRole.Creator)
		{
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas5, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_clan_leader_icon", null), true, true);
			uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
			uifittedSprite2.SetHorizontalAlign(1f);
		}
		if (!this.m_showLikes)
		{
			int num = this.m_user.mcTrophies + this.m_user.carTrophies;
			int num2 = num - (Mathf.Min(this.m_user.lastSeasonEndMcTrophies, 3000) + Mathf.Min(this.m_user.lastSeasonEndCarTrophies, 3000));
			string text = "#F5E141";
			string text2 = "menu_trophy_small_full";
			PsUITeamProfileBanner.ValueSubItem valueSubItem = new PsUITeamProfileBanner.ValueSubItem(this, num, num2, text, text2, 1f, 0.055f, true);
		}
		else
		{
			int creatorLikes = this.m_user.creatorLikes;
			int creatorRankingDelta = this.m_user.creatorRankingDelta;
			string text3 = "#71FF23";
			string text4 = "menu_thumbs_up_off";
			PsUITeamProfileBanner.ValueSubItem valueSubItem2 = new PsUITeamProfileBanner.ValueSubItem(this, creatorLikes, creatorRankingDelta, text3, text4, 2f, 0.065f, true);
		}
		if (this.m_showSeasonRewards && this.m_user.seasonReward > 0)
		{
			UICanvas uicanvas6 = new UICanvas(this, false, string.Empty, null, string.Empty);
			uicanvas6.SetRogue();
			uicanvas6.SetHeight(1f, RelativeTo.ParentHeight);
			uicanvas6.SetWidth(0.11f * this.m_parentWidth, this.m_parentRelative);
			uicanvas6.SetHorizontalAlign(1f);
			uicanvas6.SetMargins(0.16f * this.m_parentWidth, -0.16f * this.m_parentWidth, 0f, 0f, this.m_parentRelative);
			uicanvas6.RemoveDrawHandler();
			UICanvas uicanvas7 = new UICanvas(uicanvas6, false, string.Empty, null, string.Empty);
			uicanvas7.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas7.SetHeight(1f, RelativeTo.ParentHeight);
			uicanvas7.SetMargins(0.035f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas7.RemoveDrawHandler();
			UIFittedText uifittedText3 = new UIFittedText(uicanvas7, false, string.Empty, this.m_user.seasonReward.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "ffffff", null);
			UICanvas uicanvas8 = new UICanvas(uicanvas7, false, string.Empty, null, string.Empty);
			uicanvas8.SetHeight(0.03f, RelativeTo.ScreenHeight);
			uicanvas8.SetWidth(0.03f, RelativeTo.ScreenHeight);
			uicanvas8.SetHorizontalAlign(0f);
			uicanvas8.SetMargins(-0.035f, 0.035f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas8.RemoveDrawHandler();
			string currentSeasonRewardIcon = PsMetagameManager.m_seasonEndData.GetCurrentSeasonRewardIcon();
			UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas8, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(currentSeasonRewardIcon, null), true, true);
			uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
			this.SetHorizontalAlign(0f);
		}
		else if (this.m_showSeasonRewards)
		{
			this.SetHorizontalAlign(0f);
		}
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x0011EF0C File Offset: 0x0011D30C
	public void SpacerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] line = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f), new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f), 0);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, line, 0.005f * (float)Screen.height, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x060019FD RID: 6653 RVA: 0x0011EFA0 File Offset: 0x0011D3A0
	public override void Step()
	{
		if (this.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			SoundS.PlaySingleShot("/UI/Popup", Vector3.zero, 1f);
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterProfilePopup), null, null, null, true, true, InitialPage.Center, false, false, false);
			if (!this.m_downloadProfile)
			{
				(popup.m_mainContent as PsUICenterProfilePopup).SetUser(this.m_user, this.m_inTeamPopup);
			}
			else
			{
				(popup.m_mainContent as PsUICenterProfilePopup).SetUser(this.m_user.playerId, this.m_inTeamPopup);
			}
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
			});
			popup.Update();
			TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		base.Step();
	}

	// Token: 0x04001C4F RID: 7247
	protected bool m_showSeasonRewards;

	// Token: 0x04001C50 RID: 7248
	protected bool m_showLikes;

	// Token: 0x04001C51 RID: 7249
	protected PlayerData m_user;

	// Token: 0x04001C52 RID: 7250
	protected int m_index;

	// Token: 0x04001C53 RID: 7251
	protected bool m_showPos;

	// Token: 0x04001C54 RID: 7252
	protected bool m_showRole;

	// Token: 0x04001C55 RID: 7253
	protected bool m_inTeamPopup;

	// Token: 0x04001C56 RID: 7254
	protected bool m_downloadProfile;

	// Token: 0x04001C57 RID: 7255
	protected RelativeTo m_parentRelative;

	// Token: 0x04001C58 RID: 7256
	protected float m_parentWidth;

	// Token: 0x02000382 RID: 898
	private class ValueSubItem
	{
		// Token: 0x060019FE RID: 6654 RVA: 0x0011F0B8 File Offset: 0x0011D4B8
		public ValueSubItem(PsUITeamProfileBanner _parent, int _displayValue, int _delta, string _textColor, string _icon, float _scale, float _iconMargins, bool _showDelta = true)
		{
			UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uicanvas.SetWidth(0.225f * _parent.m_parentWidth, _parent.m_parentRelative);
			uicanvas.SetMargins(0.06f, 0.03f, 0f, 0f, RelativeTo.ScreenHeight);
			string font = PsFontManager.GetFont(PsFonts.KGSecondChancesMN);
			uicanvas.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, _displayValue.ToString(), font, true, _textColor, null);
			uifittedText.SetHorizontalAlign(1f);
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.0375f * _scale, RelativeTo.ScreenHeight);
			uicanvas2.SetWidth(0.04f * _scale, RelativeTo.ScreenHeight);
			uicanvas2.SetHorizontalAlign(0f);
			uicanvas2.SetMargins(-_iconMargins, _iconMargins, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas2.RemoveDrawHandler();
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(_icon, null);
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, frame, true, true);
			uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
			if (_showDelta)
			{
				PsUITeamProfileBanner.ValueSubItem.DrawDelta(_delta, uicanvas, font);
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0011F1F4 File Offset: 0x0011D5F4
		private static void DrawDelta(int _delta, UICanvas baseCanvas, string font)
		{
			UICanvas uicanvas = new UICanvas(baseCanvas, false, string.Empty, null, string.Empty);
			uicanvas.SetSize(1f, 1f, RelativeTo.ParentHeight);
			uicanvas.SetHorizontalAlign(1f);
			uicanvas.SetMargins(0.03f, -0.03f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			int num = 1;
			if (_delta < 0)
			{
				num = -1;
			}
			if (_delta != 0)
			{
				Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_triangle", null);
				UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true, true);
				if (_delta > 0)
				{
					uifittedSprite.SetColor(DebugDraw.HexToColor("#00ff00"));
				}
				else
				{
					uifittedSprite.SetColor(DebugDraw.HexToColor("#EA0000"));
				}
				uifittedSprite.SetHeight(0.025f, RelativeTo.ScreenHeight);
				TransformS.SetRotation(uifittedSprite.m_TC, new Vector3(0f, 0f, (float)(90 * num)));
				uifittedSprite.SetHorizontalAlign(1f);
				uifittedSprite.SetVerticalAlign(1f);
			}
			else
			{
				UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, "=", font, true, null, null);
				uifittedText.SetAlign(1f, 1f);
				uifittedText.SetMargins(0f, 0f, -0.01f, 0.01f, RelativeTo.ScreenHeight);
			}
		}
	}
}
