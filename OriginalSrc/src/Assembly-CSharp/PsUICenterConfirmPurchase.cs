using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000339 RID: 825
public class PsUICenterConfirmPurchase : PsUIHeaderedCanvas
{
	// Token: 0x06001807 RID: 6151 RVA: 0x00103054 File Offset: 0x00101454
	public PsUICenterConfirmPurchase(UIComponent _parent)
		: base(_parent, "ShopConfirmation", true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetBackground();
		this.m_container.CreateTouchAreas();
		this.SetSize();
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetPopupBackground();
		this.SetHeader();
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x001030C4 File Offset: 0x001014C4
	public virtual void SetHeader()
	{
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x00103119 File Offset: 0x00101519
	public virtual void SetPopupBackground()
	{
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x0010313E File Offset: 0x0010153E
	public virtual void SetBackground()
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x00103157 File Offset: 0x00101557
	protected virtual void SetSize()
	{
		this.SetWidth(0.5f, RelativeTo.ScreenHeight);
		this.SetHeight(0.6f, RelativeTo.ScreenHeight);
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x00103174 File Offset: 0x00101574
	private bool ExitPopup(TouchAreaC _touchAreaC)
	{
		if (_touchAreaC == this.m_buy.m_TAC)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Confirm");
			return true;
		}
		if (_touchAreaC == this.m_container.m_TAC)
		{
			return false;
		}
		(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		return true;
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x001031D4 File Offset: 0x001015D4
	public virtual void CustomTouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		Debug.LogError("jes");
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x001031E0 File Offset: 0x001015E0
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x00103260 File Offset: 0x00101660
	public void CreateHeaderContent(UIComponent _parent, string _header = null, string _subtitle = null)
	{
		string text = _header;
		if (string.IsNullOrEmpty(text))
		{
			text = PsStrings.Get(StringID.SHOP_PROMPT_PURCHASE);
		}
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetSpacing(0.001f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIComponent uicomponent = new UIComponent(uiverticalList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicomponent.SetWidth(0.9f, RelativeTo.ParentWidth);
		uicomponent.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
		if (!string.IsNullOrEmpty(_subtitle))
		{
			new UIText(uiverticalList, false, string.Empty, _subtitle, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, "#ffffff", "#000000");
		}
		this.SetHeaderIcon(uiverticalList);
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x00103345 File Offset: 0x00101745
	protected virtual void SetHeaderIcon(UIComponent _parent)
	{
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x00103348 File Offset: 0x00101748
	public virtual void CreateContent(UIComponent _parent, int _price, bool _coins, Action<UIComponent, object> _d_CreateSubContent, object _parameter)
	{
		_parent.RemoveTouchAreas();
		_d_CreateSubContent.Invoke(_parent, _parameter);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.05f, -0.05f, RelativeTo.ScreenHeight);
		this.m_buy = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_buy.SetGreenColors(true);
		this.m_buy.SetText(_price.ToString(), 0.0345f, 0.135f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		this.m_buy.SetMargins(0.03f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_buy.m_UItext.SetHorizontalAlign(0.8f);
		if (_coins)
		{
			this.m_buy.SetIcon("menu_resources_coin_icon", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		}
		else
		{
			this.m_buy.SetIcon("menu_resources_diamond_icon", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		}
		this.m_buy.SetVerticalAlign(0f);
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x0010348C File Offset: 0x0010188C
	public void SetPrice(int _price)
	{
		if (this.m_buy != null)
		{
			if (_price > 0)
			{
				this.m_buy.SetText(_price.ToString(), 0.0345f, 0.135f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
				this.m_buy.m_UItext.SetHorizontalAlign(0.8f);
			}
			else
			{
				this.m_buy.RemoveIcon();
				this.m_buy.SetText(PsStrings.Get(StringID.BUTTON_OUT_OF_STOCK), 0.0345f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
				this.m_buy.m_UItext.SetHorizontalAlign(0.5f);
				this.m_buy.SetMargins(0.03f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
				this.m_buy.SetGrayColors();
				this.m_buy.DisableTouchAreas(true);
			}
			this.m_buy.Update();
		}
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x0010356E File Offset: 0x0010196E
	private void SetText(string _name)
	{
		this.m_sub.SetText("Get " + _name + "?");
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x0010358B File Offset: 0x0010198B
	private void SetImage(string _image)
	{
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x00103590 File Offset: 0x00101990
	public void UpdateUpgradeCard()
	{
		if (this.m_upgradeInfo != null && this.m_upgradeInfo.m_upgradeItem != null && this.m_upgradeInfo.m_resourceBar != null)
		{
			this.m_upgradeInfo.m_resourceBar.SetValues(PsUpgradeManager.GetUpgradeResourceCount(this.m_upgradeInfo.m_upgradeItem.m_identifier), this.m_upgradeInfo.m_upgradeItem.m_nextLevelResourceRequrement);
		}
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x00103600 File Offset: 0x00101A00
	public void CreateUpgradeCard(UIComponent _parent, object _identifier)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(0.3f, RelativeTo.ScreenHeight);
		uiverticalList.SetHorizontalAlign(0.5f);
		uiverticalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0.0125f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		PsUpgradeItem psUpgradeItem;
		if (((string)_identifier).StartsWith("Car"))
		{
			psUpgradeItem = PsUpgradeManager.GetUpgradeItem(typeof(OffroadCar), (string)_identifier);
		}
		else
		{
			psUpgradeItem = PsUpgradeManager.GetUpgradeItem(typeof(Motorcycle), (string)_identifier);
		}
		this.m_upgradeInfo = new PsUIUpgradeView.UpgradeItemInfo(uiverticalList, psUpgradeItem, true, null, "cardfront", true);
		this.m_upgradeInfo.SetWidth(1f, RelativeTo.ParentWidth);
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x001036BC File Offset: 0x00101ABC
	public virtual void GetChest(UIComponent _parent, object _type)
	{
		GachaType gachaType = (GachaType)Enum.Parse(typeof(GachaType), (string)_type);
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetMargins(0.015f, 0.015f, 0.045f, 0.015f, RelativeTo.ScreenShortest);
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenShortest);
		uiverticalList.RemoveDrawHandler();
		UIComponent uicomponent = new UIComponent(uiverticalList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetWidth(1f, RelativeTo.ParentWidth);
		uicomponent.SetHeight(0.35f, RelativeTo.ParentHeight);
		uicomponent.SetMargins(0f, 0f, -0.15f, -0.2f, RelativeTo.ParentHeight);
		uicomponent.RemoveDrawHandler();
		PsUICenterShopAll.CreateChest(uicomponent, gachaType);
		this.m_chestContents = new List<UICanvas>();
		PsUICenterConfirmPurchase.CreateChestInformation(uiverticalList, gachaType, this.m_chestContents);
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x0010378C File Offset: 0x00101B8C
	public static UIVerticalList CreateChestInformation(UIComponent _parent, GachaType _gachatype, List<UICanvas> _list)
	{
		UIVerticalList uiverticalList = null;
		if (PsGachaManager.m_gachaRewardDatas.ContainsKey(_gachatype))
		{
			int num = PsMetagameManager.m_playerStats.gachaLevel;
			PsGachaRewardData[] array = PsGachaManager.m_gachaRewardDatas[_gachatype];
			num = Mathf.Clamp(num, 0, array.Length - 1);
			float num2 = 0.02f;
			uiverticalList = new UIVerticalList(_parent, string.Empty);
			uiverticalList.SetMargins(0.03f, RelativeTo.ScreenShortest);
			uiverticalList.SetSpacing(num2, RelativeTo.ParentWidth);
			uiverticalList.RemoveDrawHandler();
			UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
			uihorizontalList.SetSpacing(num2, RelativeTo.ParentWidth);
			uihorizontalList.SetHeight(0.16f, RelativeTo.ParentWidth);
			uihorizontalList.RemoveDrawHandler();
			UICanvas uicanvas = PsUICenterConfirmPurchase.CreateChestContentInfo(uihorizontalList, 0.4f - num2 * 0.5f, RelativeTo.ParentWidth, 1f, RelativeTo.ParentHeight, PsStrings.Get(StringID.GACHA_COINS).ToUpper(), "menu_resources_coin_icon", _list);
			UIText uitext = new UIText(uicanvas, false, string.Empty, array[num].m_minCoinReward + "-" + array[num].m_maxCoinReward, PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, "#ffffff", null);
			if (array[num].m_nitroCount > 0)
			{
				UICanvas uicanvas2 = PsUICenterConfirmPurchase.CreateChestContentInfo(uihorizontalList, 0.3f - num2 * 0.75f, RelativeTo.ParentWidth, 1f, RelativeTo.ParentHeight, PsStrings.Get(StringID.GACHA_OPEN_NITROS).ToUpper(), "menu_icon_booster", _list);
				UIText uitext2 = new UIText(uicanvas2, false, string.Empty, array[num].m_nitroCount.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.05f, RelativeTo.ScreenHeight, "#FFFFFF", null);
			}
			PsCustomisationData vehicleCustomisationData = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar));
			List<PsCustomisationItem> lockedItemsByCategory = vehicleCustomisationData.GetLockedItemsByCategory(PsCustomisationManager.CustomisationCategory.HAT);
			lockedItemsByCategory.RemoveAll((PsCustomisationItem item) => item.m_rarity == PsRarity.Exclusive);
			if (array[num].m_hatProbabilityValue >= 1f && lockedItemsByCategory.Count > 0)
			{
				UICanvas uicanvas3 = PsUICenterConfirmPurchase.CreateChestContentInfo(uihorizontalList, 0.3f - num2 * 0.75f, RelativeTo.ParentWidth, 1f, RelativeTo.ParentHeight, PsStrings.Get(StringID.GACHA_OPEN_HATS).ToUpper(), "menu_garage_icon_hats", _list);
				UIHorizontalList uihorizontalList2 = new UIHorizontalList(uicanvas3, string.Empty);
				uihorizontalList2.SetSpacing(0.01f, RelativeTo.ScreenHeight);
				uihorizontalList2.RemoveDrawHandler();
				bool flag = false;
				bool flag2 = false;
				for (int i = 0; i < lockedItemsByCategory.Count; i++)
				{
					if (lockedItemsByCategory[i].m_rarity == PsRarity.Rare)
					{
						flag = true;
					}
					else if (lockedItemsByCategory[i].m_rarity == PsRarity.Epic)
					{
						flag2 = true;
					}
					if (flag2 && flag)
					{
						break;
					}
				}
				string text = "#ffffff";
				string text2 = string.Empty;
				if (flag2 && array[num].m_guaranteedHatRarity == PsRarity.Epic)
				{
					text = "#E670D4";
					text2 = PsStrings.Get(StringID.GACHA_RARITY_EPIC);
				}
				else if (flag && array[num].m_guaranteedHatRarity != PsRarity.Common)
				{
					text = "#92FEFF";
					text2 = PsStrings.Get(StringID.GACHA_RARITY_RARE);
				}
				new UIText(uihorizontalList2, false, string.Empty, "1", PsFontManager.GetFont(PsFonts.HurmeBold), 0.05f, RelativeTo.ScreenHeight, text, null);
				if (!string.IsNullOrEmpty(text2))
				{
					new UIText(uihorizontalList2, false, string.Empty, text2.ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, text, null);
				}
			}
			UIHorizontalList uihorizontalList3 = new UIHorizontalList(uiverticalList, string.Empty);
			uihorizontalList3.SetSpacing(num2, RelativeTo.ParentWidth);
			uihorizontalList3.SetHeight(0.24f, RelativeTo.ParentWidth);
			uihorizontalList3.RemoveDrawHandler();
			UICanvas uicanvas4 = PsUICenterConfirmPurchase.CreateChestContentInfo(uihorizontalList3, 0.5f - num2 * 0.5f, RelativeTo.ParentWidth, 1f, RelativeTo.ParentHeight, PsStrings.Get(StringID.GACHA_VEHICLE_UPGRADES).ToUpper(), "menu_garage_icon_upgrade", _list);
			UIHorizontalList uihorizontalList4 = new UIHorizontalList(uicanvas4, string.Empty);
			uihorizontalList4.SetSpacing(0.02f, RelativeTo.ScreenShortest);
			uihorizontalList4.RemoveDrawHandler();
			UIText uitext3 = new UIText(uihorizontalList4, false, string.Empty, array[num].m_totalUpgradeItemCount.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.08f, RelativeTo.ScreenHeight, "#FFFFFF", null);
			if (array[num].m_minRareUpgradeItemCount > 0)
			{
				UIComponent uicomponent = new UIComponent(uihorizontalList4, false, string.Empty, null, null, string.Empty);
				uicomponent.SetSize(0.07f, 0.07f, RelativeTo.ScreenShortest);
				uicomponent.SetMargins(0.01f, RelativeTo.ScreenShortest);
				uicomponent.SetDrawHandler(new UIDrawDelegate(PsUICenterConfirmPurchase.RareDrawHandler));
				UIVerticalList uiverticalList2 = new UIVerticalList(uicomponent, string.Empty);
				uiverticalList2.SetSpacing(-0.01f, RelativeTo.ScreenShortest);
				uiverticalList2.SetMargins(0.01f, RelativeTo.ScreenShortest);
				uiverticalList2.RemoveDrawHandler();
				UIText uitext4 = new UIText(uiverticalList2, false, string.Empty, array[num].m_minRareUpgradeItemCount.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, "#92FEFF", null);
				UIText uitext5 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.GACHA_RARITY_RARE).ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.0175f, RelativeTo.ScreenHeight, "#92FEFF", null);
			}
			if (array[num].m_minEpicUpgradeItemCount > 0)
			{
				UIComponent uicomponent2 = new UIComponent(uihorizontalList4, false, string.Empty, null, null, string.Empty);
				uicomponent2.SetSize(0.07f, 0.07f, RelativeTo.ScreenShortest);
				uicomponent2.SetMargins(0.01f, RelativeTo.ScreenShortest);
				uicomponent2.SetDrawHandler(new UIDrawDelegate(PsUICenterConfirmPurchase.EpicDrawHandler));
				UIVerticalList uiverticalList3 = new UIVerticalList(uicomponent2, string.Empty);
				uiverticalList3.SetSpacing(-0.01f, RelativeTo.ScreenShortest);
				uiverticalList3.SetMargins(0.01f, RelativeTo.ScreenShortest);
				uiverticalList3.RemoveDrawHandler();
				UIText uitext6 = new UIText(uiverticalList3, false, string.Empty, array[num].m_minEpicUpgradeItemCount.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, "#E670D4", null);
				UIText uitext7 = new UIText(uiverticalList3, false, string.Empty, PsStrings.Get(StringID.GACHA_RARITY_EPIC).ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.0175f, RelativeTo.ScreenHeight, "#E670D4", null);
			}
			UICanvas uicanvas5 = PsUICenterConfirmPurchase.CreateChestContentInfo(uihorizontalList3, 0.5f - num2 * 0.5f, RelativeTo.ParentWidth, 1f, RelativeTo.ParentHeight, PsStrings.Get(StringID.GACHA_EDITOR_ITEMS).ToUpper(), "menu_icon_editor", _list);
			UIVerticalList uiverticalList4 = new UIVerticalList(uicanvas5, string.Empty);
			uiverticalList4.SetMargins(0.02f, 0.02f, 0.005f, 0f, RelativeTo.ScreenShortest);
			uiverticalList4.RemoveDrawHandler();
			UIHorizontalList uihorizontalList5 = new UIHorizontalList(uiverticalList4, string.Empty);
			uihorizontalList5.SetSpacing(0.02f, RelativeTo.ScreenShortest);
			uihorizontalList5.RemoveDrawHandler();
			UIText uitext8 = new UIText(uihorizontalList5, false, string.Empty, array[num].m_totalEditorItemCount.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.08f, RelativeTo.ScreenHeight, "#FFFFFF", null);
			if (array[num].m_minRareEditorItemCount > 0)
			{
				UIComponent uicomponent3 = new UIComponent(uihorizontalList5, false, string.Empty, null, null, string.Empty);
				uicomponent3.SetSize(0.07f, 0.07f, RelativeTo.ScreenShortest);
				uicomponent3.SetMargins(0.01f, RelativeTo.ScreenShortest);
				uicomponent3.SetDrawHandler(new UIDrawDelegate(PsUICenterConfirmPurchase.RareDrawHandler));
				UIVerticalList uiverticalList5 = new UIVerticalList(uicomponent3, string.Empty);
				uiverticalList5.SetSpacing(-0.01f, RelativeTo.ScreenShortest);
				uiverticalList5.SetMargins(0.01f, RelativeTo.ScreenShortest);
				uiverticalList5.RemoveDrawHandler();
				UIText uitext9 = new UIText(uiverticalList5, false, string.Empty, array[num].m_minRareEditorItemCount.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, "#92FEFF", null);
				UIText uitext10 = new UIText(uiverticalList5, false, string.Empty, PsStrings.Get(StringID.GACHA_RARITY_RARE).ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.0175f, RelativeTo.ScreenHeight, "#92FEFF", null);
			}
			if (array[num].m_minEpicEditorItemCount > 0)
			{
				UIComponent uicomponent4 = new UIComponent(uihorizontalList5, false, string.Empty, null, null, string.Empty);
				uicomponent4.SetSize(0.07f, 0.07f, RelativeTo.ScreenShortest);
				uicomponent4.SetMargins(0.01f, RelativeTo.ScreenShortest);
				uicomponent4.SetDrawHandler(new UIDrawDelegate(PsUICenterConfirmPurchase.EpicDrawHandler));
				UIVerticalList uiverticalList6 = new UIVerticalList(uicomponent4, string.Empty);
				uiverticalList6.SetSpacing(-0.01f, RelativeTo.ScreenShortest);
				uiverticalList6.SetMargins(0.01f, RelativeTo.ScreenShortest);
				uiverticalList6.RemoveDrawHandler();
				UIText uitext11 = new UIText(uiverticalList6, false, string.Empty, array[num].m_minEpicEditorItemCount.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, "#E670D4", null);
				UIText uitext12 = new UIText(uiverticalList6, false, string.Empty, PsStrings.Get(StringID.GACHA_RARITY_EPIC).ToUpper(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.0175f, RelativeTo.ScreenHeight, "#E670D4", null);
			}
		}
		return uiverticalList;
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x00104014 File Offset: 0x00102414
	public static UICanvas CreateChestContentInfo(UIComponent _parent, float _width, RelativeTo _widthRelativeTo, float _height, RelativeTo _heightRelativeTo, string _title, string _iconName, List<UICanvas> _list)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(_width, _widthRelativeTo);
		uicanvas.SetHeight(_height, _heightRelativeTo);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUICenterConfirmPurchase.ResourceBackground));
		_list.Add(uicanvas);
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetAlign(0f, 1f);
		uicanvas2.SetSize(0.06f, 0.06f, RelativeTo.ScreenHeight);
		uicanvas2.SetMargins(-0.008f, 0.008f, -0.008f, 0.008f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_iconName, null), true, true);
		uifittedSprite.SetAlign(0f, 1f);
		UICanvas uicanvas3 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas3.SetVerticalAlign(1f);
		uicanvas3.SetHorizontalAlign(0f);
		uicanvas3.SetHeight(0.032f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetMargins(0.06f, 0f, 0.007f, 0.004f, RelativeTo.ScreenHeight);
		uicanvas3.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas3, false, string.Empty, _title, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#8CF8FF", null);
		uifittedText.SetHorizontalAlign(0f);
		UICanvas uicanvas4 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas4.SetMargins(0f, 0f, uicanvas3.m_height, 0f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		return uicanvas4;
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x001041C0 File Offset: 0x001025C0
	public static void ResourceBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.015f * (float)Screen.height, 8, Vector2.zero);
		Color color = DebugDraw.HexToColor("#1B4461");
		uint num = DebugDraw.ColorToUInt(color);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, roundedRect, (float)Screen.height * 0.003f, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x00104284 File Offset: 0x00102684
	public static void RareDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.1f * _c.m_actualHeight, 8, Vector2.zero);
		Color color = DebugDraw.HexToColor("#92FEFF");
		uint num = DebugDraw.ColorToUInt(color);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x00104314 File Offset: 0x00102714
	public static void EpicDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.1f * _c.m_actualHeight, 8, Vector2.zero);
		Color color = DebugDraw.HexToColor("#E670D4");
		uint num = DebugDraw.ColorToUInt(color);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x001043A4 File Offset: 0x001027A4
	public static void GetGold(UIComponent _parent, object _index)
	{
		int coinAmoumt = PsUICenterShopAll.GetCoinAmoumt((int)_index);
		int num = PsMetagameManager.CoinsToDiamonds(coinAmoumt, true);
		string text = string.Format("{0:n0}", coinAmoumt).Replace(",", " ");
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(0.28f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.13f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetVerticalAlign(1f);
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(PsUICenterShopAll.m_coinPrices[(int)_index].m_nameID), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		uifittedText.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UISprite uisprite = new UISprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(PsUICenterShopAll.m_coinPrices[(int)_index].m_iconName, null), true);
		uisprite.SetSize(1f, uisprite.m_frame.height / uisprite.m_frame.width, RelativeTo.ParentWidth);
		UICanvas uicanvas2 = new UICanvas(uisprite, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.3f, RelativeTo.ParentHeight);
		uicanvas2.RemoveDrawHandler();
		uicanvas2.SetMargins(0.15f, RelativeTo.OwnHeight);
		uicanvas2.SetVerticalAlign(1f);
		string text2 = string.Format("{0:n0}", text).Replace(",", " ");
		UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", "#000000");
		uifittedText2.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x00104598 File Offset: 0x00102998
	public void SetInfo(ShopUpgradeItemData _info, string _identifier)
	{
		this.CreateContent(this, PsUpgradeManager.GetPriceForUpgrade(_info), true, new Action<UIComponent, object>(this.CreateUpgradeCard), _identifier);
		this.CreateHeaderContent(this.m_header, null, null);
		this.m_header.Update();
		this.Update();
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x001045D4 File Offset: 0x001029D4
	public void SetInfo(string _gachaType)
	{
		GachaType gachaType = (GachaType)Enum.Parse(typeof(GachaType), _gachaType);
		int shopPrice = PsGachaManager.GetShopPrice(gachaType);
		this.SetInfo(_gachaType, shopPrice, PsGachaManager.GetGachaNameWithChest(gachaType));
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x0010460C File Offset: 0x00102A0C
	public virtual void SetInfo(string _gachaType, int price, string _label)
	{
		this.CreateContent(this, price, false, new Action<UIComponent, object>(this.GetChest), _gachaType);
		this.CreateHeaderContent(this.m_header, _label, PsStrings.Get(StringID.LEVEL).ToUpper() + " " + (PsMetagameManager.m_playerStats.gachaLevel + 1));
		this.m_buy.SetSound("/UI/ButtonChestOpen");
		this.m_header.Update();
		this.Update();
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x00104688 File Offset: 0x00102A88
	public void SetInfo(int _index)
	{
		int coinAmoumt = PsUICenterShopAll.GetCoinAmoumt(_index);
		int num = PsMetagameManager.CoinsToDiamonds(coinAmoumt, false);
		this.CreateContent(this, num, false, new Action<UIComponent, object>(PsUICenterConfirmPurchase.GetGold), _index);
		this.CreateHeaderContent(this.m_header, null, null);
		this.m_header.Update();
		this.Update();
	}

	// Token: 0x06001822 RID: 6178 RVA: 0x001046F0 File Offset: 0x00102AF0
	public override void Step()
	{
		if (this.m_buy != null && this.m_buy.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			(this.GetRoot() as PsUIBasePopup).CallAction("Confirm");
		}
		else
		{
			this.HandleButtonPresses();
		}
		base.Step();
	}

	// Token: 0x06001823 RID: 6179 RVA: 0x00104745 File Offset: 0x00102B45
	public virtual void HandleButtonPresses()
	{
	}

	// Token: 0x04001AD9 RID: 6873
	protected PsUIGenericButton m_buy;

	// Token: 0x04001ADA RID: 6874
	private UITextbox m_sub;

	// Token: 0x04001ADB RID: 6875
	protected List<UICanvas> m_chestContents;

	// Token: 0x04001ADC RID: 6876
	private PsUIUpgradeView.UpgradeItemInfo m_upgradeInfo;
}
