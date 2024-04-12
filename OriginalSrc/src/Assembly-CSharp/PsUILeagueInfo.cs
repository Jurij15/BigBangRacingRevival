using System;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class PsUILeagueInfo : UIVerticalList
{
	// Token: 0x06001A9B RID: 6811 RVA: 0x001283B8 File Offset: 0x001267B8
	public PsUILeagueInfo(UIComponent _parent, int _leagueIndex, bool _lockCurrent = false)
		: base(_parent, "SingleLeague")
	{
		this.m_lockCurrent = _lockCurrent;
		this.m_leagueIndex = _leagueIndex;
		this.m_currentRank = PsMetagameData.GetCurrentLeagueIndex();
		if (this.m_leagueIndex == 0)
		{
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DashedTopBottom));
		}
		else
		{
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DashedBottom));
		}
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		this.SetMargins(0.075f, 0.075f, 0.045f, 0.075f, RelativeTo.ScreenHeight);
		this.CreateContent();
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x00128480 File Offset: 0x00126880
	protected virtual bool IsLocked()
	{
		if (this.m_lockCurrent)
		{
			return this.m_currentRank - 1 < this.m_leagueIndex;
		}
		return this.m_currentRank < this.m_leagueIndex;
	}

	// Token: 0x06001A9D RID: 6813 RVA: 0x001284AC File Offset: 0x001268AC
	protected virtual bool isCurrent()
	{
		return !this.m_lockCurrent && this.m_currentRank == this.m_leagueIndex;
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x001284CA File Offset: 0x001268CA
	private bool UpgradeUnlocked()
	{
		if (this.m_lockCurrent)
		{
			return PsMetagameManager.m_playerStats.rank > this.m_leagueIndex;
		}
		return PsMetagameManager.m_playerStats.rank >= this.m_leagueIndex;
	}

	// Token: 0x06001A9F RID: 6815 RVA: 0x00128500 File Offset: 0x00126900
	protected virtual void CreateContent()
	{
		bool flag = this.m_leagueIndex > this.m_currentRank;
		string text = ((!flag) ? "#FFFFFF" : "#AAAAAA");
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetMargins(0f, 0f, 0.015f, -0.015f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true, true);
		uifittedSprite.SetHeight(0.04f, RelativeTo.ScreenHeight);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, PsMetagameData.m_leagueData[this.m_leagueIndex].m_trophyLimit.ToString() + "+", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, "#FFFFFF", null);
		uitext.SetVerticalAlign(0.1f);
		this.m_banner = new UIFittedSprite(this, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(PsMetagameData.m_leagueData[this.m_leagueIndex].m_bannerSprite, null), true, true);
		this.m_banner.SetHeight(0.125f, RelativeTo.ScreenHeight);
		this.m_banner.m_overrideShader = Shader.Find("WOE/Unlit/ColorUnlitTransparent");
		if (this.IsLocked())
		{
			this.m_banner.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			this.m_banner.SetColor(new Color(0.125f, 0.125f, 0.125f));
		}
		if (this.isCurrent())
		{
			this.CreateGlowBackground(false);
		}
		else
		{
			this.CreateShadowBackground(false);
		}
		UIVerticalList uiverticalList = new UIVerticalList(this.m_banner, string.Empty);
		uiverticalList.SetSpacing(-0.0095f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(0.85f);
		uiverticalList.RemoveDrawHandler();
		float num = 0.0425f;
		float num2 = 0.618f * num;
		string text2 = PsMetagameData.m_leagueData[this.m_leagueIndex].m_name.ToUpper();
		string splittedName = PsMetagameData.m_leagueData[this.m_leagueIndex].m_splittedName;
		string[] array = splittedName.Split(new string[] { "%1" }, 0);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Contains("*"))
			{
				string text3 = array[i].Replace("*", string.Empty);
				UIText uitext2 = new UIText(uiverticalList, false, string.Empty, text3.ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), num, RelativeTo.ScreenHeight, "#000000", null);
			}
			else
			{
				UIText uitext3 = new UIText(uiverticalList, false, string.Empty, array[i], PsFontManager.GetFont(PsFonts.HurmeBold), num2, RelativeTo.ScreenHeight, "#000000", null);
			}
		}
		PsUpgradeItem[] upgradeItemsByTier = PsUpgradeManager.GetVehicleUpgradeData(PsState.GetCurrentVehicleType(false)).GetUpgradeItemsByTier(this.m_leagueIndex + 1);
		if (upgradeItemsByTier != null && upgradeItemsByTier.Length > 0)
		{
			new UIText(this, false, string.Empty, PsStrings.Get(StringID.UPGRADE_UNLOCK), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, text, null);
		}
		this.m_unlockIconList = new UIHorizontalList(this, string.Empty);
		this.m_unlockIconList.RemoveDrawHandler();
		float num3 = 0.115f;
		float num4 = 0.31199998f * num3;
		this.m_unlockIconList.SetHeight(num3, RelativeTo.ScreenHeight);
		this.m_unlockIconList.SetSpacing(num4, RelativeTo.ScreenHeight);
		if (upgradeItemsByTier != null)
		{
			for (int j = 0; j < upgradeItemsByTier.Length; j++)
			{
				UIFittedSprite uifittedSprite2 = new UIFittedSprite(this.m_unlockIconList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(upgradeItemsByTier[j].m_iconName, null), true, true);
				uifittedSprite2.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
				uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
				if (!this.UpgradeUnlocked())
				{
					Color color;
					color..ctor(0.25f, 0.25f, 0.25f);
					uifittedSprite2.SetColor(color);
				}
			}
		}
		if (this.m_leagueIndex > 0)
		{
			UIVerticalList uiverticalList2 = new UIVerticalList(this.m_unlockIconList, string.Empty);
			uiverticalList2.RemoveDrawHandler();
			UIText uitext4 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.CHEST_UPGRADE_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, "#73DAE6", null);
			UIFittedSprite uifittedSprite3 = new UIFittedSprite(uiverticalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_racing_gacha", null), true, true);
			uifittedSprite3.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			uifittedSprite3.SetHeight(0.105f, RelativeTo.ScreenHeight);
			if (!this.UpgradeUnlocked())
			{
				Color color2;
				color2..ctor(0.25f, 0.25f, 0.25f);
				uifittedSprite3.SetColor(color2);
			}
			UIText uitext5 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.LEVEL_PLUS_ONE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, null, null);
		}
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x001289F4 File Offset: 0x00126DF4
	public void CheckTouch()
	{
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x001289F6 File Offset: 0x00126DF6
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x00128A00 File Offset: 0x00126E00
	public void CreateGlowBackground(bool _update = false)
	{
		if (this.m_bannerBgChild != null)
		{
			this.m_bannerBgChild.Destroy();
			this.m_bannerBgChild = null;
		}
		this.m_bannerBgChild = new UIFittedSprite(this.m_banner, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_banner_glow", null), true, true);
		this.m_bannerBgChild.SetRogue();
		this.m_bannerBgChild.SetSize(1.5f, 1.5f, RelativeTo.ParentWidth);
		this.m_bannerBgChild.SetDepthOffset(5f);
		if (_update)
		{
			this.m_bannerBgChild.Update();
		}
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x00128AA0 File Offset: 0x00126EA0
	public void CreateShadowBackground(bool _update = false)
	{
		if (this.m_bannerBgChild != null)
		{
			this.m_bannerBgChild.Destroy();
			this.m_bannerBgChild = null;
		}
		this.m_bannerBgChild = new UIComponent(this.m_banner, false, string.Empty, null, null, string.Empty);
		this.m_bannerBgChild.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_bannerBgChild.SetMargins(0f, 0f, 0.35f, -0.35f, RelativeTo.ParentHeight);
		this.m_bannerBgChild.SetDepthOffset(5f);
		this.m_bannerBgChild.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_bannerBgChild, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_banner_shadow", null), true, true);
		if (_update)
		{
			this.m_bannerBgChild.Update();
		}
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x00128B73 File Offset: 0x00126F73
	public void RankUp()
	{
		this.BannerZoomOut();
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x00128B7C File Offset: 0x00126F7C
	private void BannerZoomOut()
	{
		this.CreateGlowBackground(true);
		TweenC tweenC = TweenS.AddTransformTween(this.m_banner.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one, new Vector3(1.275f, 1.275f, 1f), 0.15f, 0f, false);
		TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
		{
			TweenS.RemoveComponent(_c);
			this.m_banner.m_rect.p_gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f);
			this.BannerZoomIn();
		});
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x00128BDC File Offset: 0x00126FDC
	private void BannerZoomIn()
	{
		TweenC tweenC = TweenS.AddTransformTween(this.m_banner.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, new Vector3(1.275f, 1.275f, 1f), Vector3.one, 0.2f, 0f, false);
		TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
		{
			TweenS.RemoveComponent(_c);
			this.UnlockUpgrades();
		});
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x00128C34 File Offset: 0x00127034
	private void UnlockUpgrades()
	{
		Debug.Log("Unlocking upgrades", null);
		int upgradeCount = this.m_unlockIconList.m_childs.Count;
		for (int i = 0; i < this.m_unlockIconList.m_childs.Count; i++)
		{
			UIFittedSprite icon = this.m_unlockIconList.m_childs[i] as UIFittedSprite;
			if (icon == null)
			{
				UIVerticalList vlist = this.m_unlockIconList.m_childs[i] as UIVerticalList;
				if (vlist == null)
				{
					break;
				}
				TweenC tweenC = TweenS.AddTransformTween(vlist.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one, new Vector3(1.275f, 1.275f, 1f), 0.2f, (float)i * 0.3f, false);
				TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
				{
					TweenS.RemoveComponent(_c);
					this.ZoomListBack(vlist);
				});
				TweenS.AddTweenStartEventListener(tweenC, delegate(TweenC _c)
				{
					SoundS.PlaySingleShot("/Ingame/Events/League_AllChestsLevelUp", Vector3.zero, 1f);
				});
				float num = Random.Range(-8f, 8f);
				TweenC tweenC2 = TweenS.AddTransformTween(vlist.m_TC, TweenedProperty.Rotation, TweenStyle.BounceOut, new Vector3(0f, 0f, num), 0.2f, (float)i * 0.3f, false);
				TweenS.SetAdditionalTweenProperties(tweenC2, 0, true, TweenStyle.CubicInOut);
			}
			else
			{
				TweenC tweenC3 = TweenS.AddTransformTween(icon.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one, new Vector3(1.275f, 1.275f, 1f), 0.2f, (float)i * 0.3f, false);
				TweenS.AddTweenEndEventListener(tweenC3, delegate(TweenC _c)
				{
					TweenS.RemoveComponent(_c);
					this.ZoomBack(icon);
				});
				TweenS.AddTweenStartEventListener(tweenC3, delegate(TweenC _c)
				{
					if (upgradeCount > 1)
					{
						if (this.unlockNumber == 5)
						{
							SoundS.PlaySingleShot("/Ingame/Events/League_AllChestsLevelUp", Vector3.zero, 1f);
						}
						else
						{
							SoundS.PlaySingleShotWithParameter("/Ingame/Events/League_ItemUnlocked", Vector3.zero, "ItemNumber", (float)(this.unlockNumber - 1), 1f);
						}
					}
					else
					{
						SoundS.PlaySingleShot("/Ingame/Events/League_AllChestsLevelUp", Vector3.zero, 1f);
					}
					this.unlockNumber++;
				});
				float num2 = Random.Range(-8f, 8f);
				TweenC tweenC4 = TweenS.AddTransformTween(icon.m_TC, TweenedProperty.Rotation, TweenStyle.BounceOut, new Vector3(0f, 0f, num2), 0.2f, (float)i * 0.3f, false);
				TweenS.SetAdditionalTweenProperties(tweenC4, 0, true, TweenStyle.CubicInOut);
			}
		}
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x00128E80 File Offset: 0x00127280
	private void ZoomListBack(UIVerticalList _vlist)
	{
		Renderer component = (_vlist.m_childs[1] as UIFittedSprite).m_rect.p_gameObject.GetComponent<Renderer>();
		if (component != null && component.material != null)
		{
			component.material.color = new Color(1f, 1f, 1f);
		}
		TweenC tweenC = TweenS.AddTransformTween(_vlist.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, new Vector3(1.275f, 1.275f, 1f), Vector3.one, 0.3f, 0f, false);
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x00128F1C File Offset: 0x0012731C
	private void ZoomBack(UIFittedSprite _icon)
	{
		Renderer component = _icon.m_rect.p_gameObject.GetComponent<Renderer>();
		if (component != null && component.material != null)
		{
			component.material.color = new Color(1f, 1f, 1f);
		}
		TweenC tweenC = TweenS.AddTransformTween(_icon.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, new Vector3(1.275f, 1.275f, 1f), Vector3.one, 0.3f, 0f, false);
	}

	// Token: 0x04001D12 RID: 7442
	public int m_currentRank;

	// Token: 0x04001D13 RID: 7443
	public int m_leagueIndex;

	// Token: 0x04001D14 RID: 7444
	protected UIComponent m_bannerBgChild;

	// Token: 0x04001D15 RID: 7445
	public UIFittedSprite m_banner;

	// Token: 0x04001D16 RID: 7446
	public UIHorizontalList m_unlockIconList;

	// Token: 0x04001D17 RID: 7447
	private PsUIBasePopup m_popup;

	// Token: 0x04001D18 RID: 7448
	private bool m_lockCurrent;

	// Token: 0x04001D19 RID: 7449
	private UIFittedSprite m_valueBg;

	// Token: 0x04001D1A RID: 7450
	private UIFittedText m_offerText;

	// Token: 0x04001D1B RID: 7451
	private int unlockNumber = 1;
}
