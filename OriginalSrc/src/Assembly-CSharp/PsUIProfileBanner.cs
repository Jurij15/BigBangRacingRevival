using System;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x02000314 RID: 788
public class PsUIProfileBanner : UIVerticalList
{
	// Token: 0x0600173F RID: 5951 RVA: 0x000FAA8C File Offset: 0x000F8E8C
	public PsUIProfileBanner(UIComponent _parent, PlayerData _user, bool _addToCache = false, bool _friendProfile = false)
		: base(_parent, "profileBanner")
	{
		this.m_buttons = new List<PsUIProfileLevelButton>();
		this.m_populateLevels = false;
		this.m_levelsShown = false;
		this.m_addToCache = _addToCache;
		this.m_friendProfile = _friendProfile;
		this.m_user = _user;
		this.RemoveDrawHandler();
		this.m_banner = new UIHorizontalList(this, string.Empty);
		this.m_banner.SetHorizontalAlign(0f);
		this.m_banner.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		this.m_banner.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ProfileBannerDrawhandler));
		this.m_banner.SetMargins(0.02f, 0.02f, 0.015f, 0.015f, RelativeTo.ScreenHeight);
		this.m_banner.CreateTouchAreas();
		this.m_banner.m_TAC.m_letTouchesThrough = true;
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(this.m_banner, false, string.Empty, this.m_user.facebookId, this.m_user.gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, true, true);
		psUIProfileImage.SetSize(0.085f, 0.085f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList = new UIVerticalList(this.m_banner, string.Empty);
		uiverticalList.SetSpacing(0.005f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.0425f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.45f, RelativeTo.ScreenWidth);
		uicanvas.SetMargins(0f, 0.1f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		bool flag = PsMetagameManager.IsFriend(_user.playerId);
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, this.m_user.name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, (!flag) ? "ffffff" : "#A6FF32", "313131");
		uifittedText.SetHorizontalAlign(0f);
		UICanvas uicanvas2 = new UICanvas(uifittedText, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.0425f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(0.0814f, RelativeTo.ScreenHeight);
		uicanvas2.SetHorizontalAlign(1f);
		uicanvas2.SetMargins(0.09f, -0.09f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_user.countryCode, "_alien"), true, true);
		uifittedSprite.SetHeight(0.0425f, RelativeTo.ScreenHeight);
		if (!string.IsNullOrEmpty(this.m_user.teamName))
		{
			UICanvas uicanvas3 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas3.SetHeight(0.0325f, RelativeTo.ScreenHeight);
			uicanvas3.SetWidth(0.45f, RelativeTo.ScreenWidth);
			uicanvas3.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas3.RemoveDrawHandler();
			string teamName = this.m_user.teamName;
			UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, teamName, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FDFF48", "313131");
			uifittedText2.SetHorizontalAlign(0f);
		}
		UICanvas uicanvas4 = new UICanvas(this.m_banner, false, string.Empty, null, string.Empty);
		uicanvas4.SetSize(0.07f, 0.07f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		if (this.m_user.publishedMinigameCount > 0)
		{
			this.m_arrow = new UIFittedSprite(uicanvas4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_triangle", null), true, true);
			this.m_arrow.SetHeight(0.07f, RelativeTo.ScreenHeight);
			this.m_arrow.SetDepthOffset(-8f);
			UICanvas uicanvas5 = new UICanvas(uicanvas4, false, string.Empty, null, string.Empty);
			uicanvas5.SetHeight(0.0325f, RelativeTo.ScreenHeight);
			uicanvas5.SetWidth(0.075f, RelativeTo.ScreenWidth);
			uicanvas5.SetMargins(-0.09f, 0.09f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas5.SetHorizontalAlign(1f);
			uicanvas5.RemoveDrawHandler();
			UIFittedText uifittedText3 = new UIFittedText(uicanvas5, false, string.Empty, this.m_user.publishedMinigameCount.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", "313131");
			uifittedText3.SetHorizontalAlign(1f);
		}
		UICanvas uicanvas6 = new UICanvas(this.m_banner, false, string.Empty, null, string.Empty);
		uicanvas6.SetRogue();
		uicanvas6.SetHeight(0.08f, RelativeTo.ScreenHeight);
		uicanvas6.SetWidth(0.08f, RelativeTo.ScreenHeight);
		uicanvas6.SetAlign(0f, 0.5f);
		uicanvas6.SetMargins(-0.12f, 0.12f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas6.RemoveDrawHandler();
		PsUIProfileButton psUIProfileButton = new PsUIProfileButton(uicanvas6, _user, 0.25f, 0.25f, 0.005f, 0.175f);
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x000FAF80 File Offset: 0x000F9380
	public void ShowLevels(Action _createDelegate = null)
	{
		this.m_createDelegate = _createDelegate;
		if (this.m_user.publishedMinigameCount > 0)
		{
			this.m_levelsShown = true;
			if (this.m_levelList == null)
			{
				this.m_levelList = new UIVerticalList(this, string.Empty);
				this.m_levelList.SetMargins(0.04f, RelativeTo.ScreenHeight);
				this.m_levelList.SetWidth(0.975f, RelativeTo.ParentWidth);
				this.m_levelList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ProfileLevelsBackground));
			}
			if (this.m_userLevels == null)
			{
				this.GetLevels();
			}
			else
			{
				this.CreateLevelContent();
			}
			TweenS.AddTransformTween(this.m_arrow.m_TC, TweenedProperty.Rotation, TweenStyle.CubicInOut, new Vector3(0f, 0f, -90f), 0.2f, 0f, true);
		}
	}

	// Token: 0x06001741 RID: 5953 RVA: 0x000FB060 File Offset: 0x000F9460
	public void HideLevels()
	{
		if (this.m_user.publishedMinigameCount > 0)
		{
			this.m_levelsShown = false;
			this.DestroyChildren(1);
			this.SetHeight(1f, RelativeTo.ParentHeight);
			this.CalculateReferenceSizes();
			this.UpdateSize();
			this.ArrangeContents();
			base.UpdateDimensions();
			this.UpdateSize();
			this.UpdateAlign();
			this.ArrangeContents();
			this.m_levelList = null;
			TweenS.AddTransformTween(this.m_arrow.m_TC, TweenedProperty.Rotation, TweenStyle.CubicInOut, new Vector3(0f, 0f, 360f), 0.2f, 0f, true);
		}
	}

	// Token: 0x06001742 RID: 5954 RVA: 0x000FB0FB File Offset: 0x000F94FB
	private void HideEventhandler(TweenC _C)
	{
		this.m_levelsShown = false;
		this.DestroyChildren(1);
		this.m_levelList = null;
	}

	// Token: 0x06001743 RID: 5955 RVA: 0x000FB114 File Offset: 0x000F9514
	public void CreateLevelContent()
	{
		if (this.m_levelList != null)
		{
			this.PopulateContent(this.m_userLevels, typeof(PsGameLoopSocial), "User has no levels", 0.035f, false, false, false);
			this.Update();
			if (this.m_createDelegate != null)
			{
				this.m_createDelegate.Invoke();
			}
		}
	}

	// Token: 0x06001744 RID: 5956 RVA: 0x000FB16C File Offset: 0x000F956C
	private void GetLevels()
	{
		new PsUILoadingAnimation(this.m_levelList, false);
		this.Update();
		HttpC publishedByPlayerId = MiniGame.GetPublishedByPlayerId(this.m_user.playerId, new Action<PsMinigameMetaData[]>(this.LevelGetSUCCEED), new Action<HttpC>(this.LevelGetFAILED), 50, null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, publishedByPlayerId);
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x000FB1CC File Offset: 0x000F95CC
	private void LevelGetSUCCEED(PsMinigameMetaData[] _data)
	{
		Debug.Log("GET PROFILE MINIGAMES SUCCEED", null);
		this.m_userLevels = _data;
		if (this.m_addToCache)
		{
			if (this.m_friendProfile)
			{
				PsCaches.m_friendsLevelList.AddItem(this.m_user.playerId, _data);
			}
			else
			{
				PsCaches.m_searchedPlayersLevelList.AddItem(this.m_user.playerId, _data);
			}
		}
		this.m_populateLevels = true;
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x000FB23C File Offset: 0x000F963C
	private void LevelGetFAILED(HttpC _c)
	{
		Debug.Log("GET PROFILE MINIGAMES FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, delegate
		{
			HttpC publishedByPlayerId = MiniGame.GetPublishedByPlayerId(this.m_user.playerId, new Action<PsMinigameMetaData[]>(this.LevelGetSUCCEED), new Action<HttpC>(this.LevelGetFAILED), 50, null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, publishedByPlayerId);
			return publishedByPlayerId;
		}, null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x000FB287 File Offset: 0x000F9687
	public override void Step()
	{
		if (this.m_populateLevels)
		{
			this.m_populateLevels = false;
			this.CreateLevelContent();
		}
		base.Step();
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x000FB2A8 File Offset: 0x000F96A8
	public void PopulateContent(PsMinigameMetaData[] _levels, Type _gameloopType, string _noLevelsTexts = "User has no levels", float _spacing = 0.02f, bool _createSlots = false, bool _showCreators = false, bool _claimable = false)
	{
		this.m_levelList.DestroyChildren(0);
		this.m_levelList.SetSpacing(_spacing, RelativeTo.ParentWidth);
		this.m_buttons.Clear();
		if (_levels.Length > 0)
		{
			int num = Mathf.CeilToInt((float)_levels.Length / 3f);
			for (int i = 0; i < num; i++)
			{
				int num2 = _levels.Length - i * 3;
				UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_levelList, string.Empty);
				uihorizontalList.SetHorizontalAlign(0f);
				uihorizontalList.SetSpacing(_spacing, RelativeTo.ParentWidth);
				uihorizontalList.RemoveDrawHandler();
				uihorizontalList.SetDepthOffset(-3f);
				float num3 = 1f - _spacing * 2f;
				for (int j = 0; j < 3; j++)
				{
					if (j + 1 > num2)
					{
						if (!_createSlots)
						{
							break;
						}
						UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
						uicanvas.SetWidth(num3 / 3f, RelativeTo.ParentWidth);
						uicanvas.SetHeight(num3 / 3f * 0.775f, RelativeTo.ParentWidth);
						uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.EmptySpaceRectDrawhandler));
					}
					else
					{
						object[] array = new object[] { _levels[i * 3 + j] };
						PsGameLoop psGameLoop = Activator.CreateInstance(_gameloopType, array) as PsGameLoop;
						UIHorizontalList uihorizontalList2 = uihorizontalList;
						PsGameLoop psGameLoop2 = psGameLoop;
						PsUIProfileLevelButton psUIProfileLevelButton = new PsUIProfileLevelButton(uihorizontalList2, psGameLoop2, true, _showCreators, _claimable);
						psUIProfileLevelButton.SetWidth(num3 / 3f, RelativeTo.ParentWidth);
						psUIProfileLevelButton.SetHeight(num3 / 3f * 0.775f, RelativeTo.ParentWidth);
						psUIProfileLevelButton.SetHorizontalAlign(0f);
						this.m_buttons.Add(psUIProfileLevelButton);
					}
				}
			}
		}
		else
		{
			this.m_levelList.SetMargins(0.04f, 0.04f, 0.04f, 0.04f, RelativeTo.ScreenHeight);
			UITextbox uitextbox = new UITextbox(this.m_levelList, false, string.Empty, _noLevelsTexts, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0385f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
			uitextbox.SetDepthOffset(-3f);
		}
	}

	// Token: 0x040019FF RID: 6655
	public PsMinigameMetaData[] m_userLevels;

	// Token: 0x04001A00 RID: 6656
	public PlayerData m_user;

	// Token: 0x04001A01 RID: 6657
	private UIFittedSprite m_arrow;

	// Token: 0x04001A02 RID: 6658
	public bool m_levelsShown;

	// Token: 0x04001A03 RID: 6659
	private bool m_addToCache;

	// Token: 0x04001A04 RID: 6660
	private bool m_friendProfile;

	// Token: 0x04001A05 RID: 6661
	public UIHorizontalList m_banner;

	// Token: 0x04001A06 RID: 6662
	private bool m_populateLevels;

	// Token: 0x04001A07 RID: 6663
	public UIVerticalList m_levelList;

	// Token: 0x04001A08 RID: 6664
	public List<PsUIProfileLevelButton> m_buttons;

	// Token: 0x04001A09 RID: 6665
	private Action m_createDelegate;
}
