using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class PsUIUpgradeView : UICanvas
{
	// Token: 0x060004E2 RID: 1250 RVA: 0x00040354 File Offset: 0x0003E754
	public PsUIUpgradeView(UIComponent _parent, Type _vehicleType)
		: base(_parent, false, "UpgradeView", null, string.Empty)
	{
		this.m_vehicleType = _vehicleType;
		PsUpgradeData vehicleUpgradeData = PsUpgradeManager.GetVehicleUpgradeData(this.m_vehicleType);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.RemoveDrawHandler();
		this.m_scrollArea = new UIScrollableCanvas(this, "UpgradeViewScrollCanvas");
		this.m_scrollArea.FreezeHorizontalScroll(false);
		this.m_scrollArea.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_scrollArea.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_scrollArea.SetMargins(0.15f, 0.1f, 0.1f, 0.18f, RelativeTo.ParentWidth);
		this.m_scrollArea.SetHorizontalAlign(0f);
		this.m_scrollArea.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this.m_scrollArea, "UpgradeTiers");
		uiverticalList.SetSpacing(-0.001f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(uiverticalList, false, "Header", null, string.Empty);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetSize(1f, 0.15f, RelativeTo.ParentWidth);
		uicanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas, string.Empty);
		uihorizontalList.SetHeight(1f, RelativeTo.ParentHeight);
		uihorizontalList.RemoveDrawHandler();
		EventGiftTimedUpgradeInstallDiscount activeEventGift = PsMetagameManager.GetActiveEventGift<EventGiftTimedUpgradeInstallDiscount>();
		if (activeEventGift != null)
		{
			string discountTag = activeEventGift.GetDiscountTag();
			this.CreateDiscountSticker(uihorizontalList, discountTag);
		}
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_header", null);
		float num = frame.width / 3f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num, frame.y, num, frame.height);
		Frame frame4 = new Frame(frame.x + num * 2f, frame.y, num, frame.height);
		UISprite uisprite = new UISprite(uihorizontalList, false, "Left", PsState.m_uiSheet, frame2, true);
		uisprite.SetDepthOffset(1f);
		UISprite uisprite2 = new UISprite(uihorizontalList, false, "Middle", PsState.m_uiSheet, frame3, true);
		uisprite2.SetDepthOffset(1f);
		UISprite uisprite3 = new UISprite(uihorizontalList, false, "Right", PsState.m_uiSheet, frame4, true);
		uisprite3.SetDepthOffset(1f);
		uisprite.SetHeight(1f, RelativeTo.ParentHeight);
		uisprite.SetWidth(frame2.width / frame2.height, RelativeTo.ParentHeight);
		uisprite2.SetHeight(1f, RelativeTo.ParentHeight);
		uisprite2.SetWidth(0.9f, RelativeTo.ParentWidth);
		uisprite3.SetHeight(1f, RelativeTo.ParentHeight);
		uisprite3.SetWidth(frame4.width / frame4.height, RelativeTo.ParentHeight);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uihorizontalList, "HeaderContent");
		uihorizontalList2.SetHeight(1f, RelativeTo.ParentHeight);
		uihorizontalList2.SetRogue();
		uihorizontalList2.SetSpacing(0.2f, RelativeTo.ParentHeight);
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetMargins(0f, 0f, -0.1f, 0.1f, RelativeTo.ParentHeight);
		Frame frame5 = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_icon_upgrade", null);
		UISprite uisprite4 = new UISprite(uihorizontalList2, false, "HeaderIcon", PsState.m_uiSheet, frame5, true);
		uisprite4.SetSize(frame5.width / frame5.height, 1f, RelativeTo.ParentHeight);
		UIText uitext = new UIText(uihorizontalList2, false, "Title", PsStrings.Get(StringID.GARAGE_UPGRADE_TITLE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.5f, RelativeTo.ParentHeight, "#81c3eb", null);
		uitext.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UISprite uisprite5 = new UISprite(uiverticalList, false, "Background", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_row_unlocked", null), true);
		uisprite5.SetSize(1f, uisprite5.m_frame.height / uisprite5.m_frame.width, RelativeTo.ParentWidth);
		this.m_shopButton = new PsUIGenericButton(uisprite5, 0.25f, 0.25f, 0.005f, "Button");
		this.m_shopButton.SetIcon("menu_icon_chest", 0.11f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_shopButton.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_shopButton.SetDepthOffset(-2f);
		this.m_shopButton.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		this.m_shopButton.SetVerticalAlign(0.55f);
		UIVerticalList uiverticalList2 = new UIVerticalList(this.m_shopButton, string.Empty);
		uiverticalList2.RemoveDrawHandler();
		UIText uitext2 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.BUTTON_GET_MORE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
		UIText uitext3 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.BUTTON_GET_MORE_UPGRADE_ITEMS), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, null, null);
		this.m_upgradeTiers = new PsUIUpgradeView.UpgradeTier[vehicleUpgradeData.m_tierCount];
		for (int i = 1; i <= vehicleUpgradeData.m_tierCount; i++)
		{
			this.m_upgradeTiers[i - 1] = new PsUIUpgradeView.UpgradeTier(uiverticalList, i, vehicleUpgradeData, new PsUIUpgradeView.UpgradeItemButtonTouchEvent(this.UpgradeItemTouchEvent));
		}
		UISprite uisprite6 = new UISprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_row_footer", null), true);
		uisprite6.SetSize(1f, uisprite6.m_frame.height / uisprite6.m_frame.width, RelativeTo.ParentWidth);
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060004E3 RID: 1251 RVA: 0x000408D8 File Offset: 0x0003ECD8
	// (remove) Token: 0x060004E4 RID: 1252 RVA: 0x00040910 File Offset: 0x0003ED10
	[field: DebuggerBrowsable(0)]
	public event PsUIUpgradeView.UpgradePurchased UpgradeWasPurchased;

	// Token: 0x060004E5 RID: 1253 RVA: 0x00040948 File Offset: 0x0003ED48
	private void CreateDiscountSticker(UIComponent _parent, string _text)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, "stickerArea", null, string.Empty);
		uicanvas.SetRogue();
		float num = 0.05f;
		float num2 = 0.5f;
		uicanvas.SetMargins(num2 - num, -num2 - num, 0f - num, 0f - num, RelativeTo.ParentWidth);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_advert_badge", null), true, true);
		string text = "#ff3b00";
		string text2 = "#ffffff";
		uifittedSprite.SetColor(DebugDraw.HexToColor(text));
		uifittedSprite.SetMargins(0.15f, RelativeTo.OwnHeight);
		UIFittedText uifittedText = new UIFittedText(uifittedSprite, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.HurmeBold), true, text2, null);
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00040A08 File Offset: 0x0003EE08
	public void UpdateCards()
	{
		for (int i = 0; i < this.m_upgradeTiers.Length; i++)
		{
			this.m_upgradeTiers[i].UpdateButtons();
		}
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00040A3C File Offset: 0x0003EE3C
	public override void Step()
	{
		if (this.m_shopButton != null && this.m_shopButton.m_hit)
		{
			PsUICenterShopAll.m_scrollIndex = 3;
			PsUIBaseState psUIBaseState = new PsUIBaseState(typeof(PsUICenterShopAll), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
			psUIBaseState.SetAction("Exit", delegate
			{
				PsUICenterGarage.m_createGarageAction.Invoke();
			});
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState);
		}
		base.Step();
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00040ACB File Offset: 0x0003EECB
	private void SetResourceView()
	{
		if (this.m_lastResourceView != null)
		{
			PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastResourceView);
			this.m_lastResourceView = null;
		}
		else
		{
			PsMetagameManager.HideResources();
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00040AFC File Offset: 0x0003EEFC
	private void UpgradeItemTouchEvent(PsUIUpgradeView.UpgradeItemButton _button)
	{
		if (this.m_selectedItemButton == _button)
		{
			this.UnselectItem(false);
		}
		else
		{
			if (this.m_selectedItemButton != null)
			{
				this.m_selectedItemButton.HideInfo();
			}
			this.m_selectedItemButton = _button;
			PsUpgradeItem upgradeItem = PsUpgradeManager.GetUpgradeItem(this.m_vehicleType, this.m_selectedItemButton.m_itemIdentifier);
			this.m_selectedItemButton.ShowInfo(upgradeItem, new Action(this.Purchase));
			int num = (int)(PsUpgradeManager.GetBasePerformance(this.m_vehicleType) / 4f);
			if (upgradeItem.m_currentLevel < upgradeItem.m_maxLevel)
			{
				int num2 = upgradeItem.GetPowerUpItemPerformance();
				int num3 = 3;
				if (!upgradeItem.m_powerUpItem)
				{
					num2 = (int)upgradeItem.m_nextLevelEfficiency;
					num3 = (int)upgradeItem.m_upgradeType;
				}
				(this.m_parent.m_parent.m_parent as PsUICenterGarage).m_vehicleStats.Highlight(num2, num3);
			}
			else
			{
				(this.m_parent.m_parent.m_parent as PsUICenterGarage).m_vehicleStats.RemoveHighlight();
			}
			TouchAreaS.AddBeginTouchDelegate(new Func<TouchAreaC, bool>(this.UnselectItem));
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00040C10 File Offset: 0x0003F010
	public bool UnselectItem(TouchAreaC _tac)
	{
		if (this.m_selectedItemButton == null || this.m_destroyed)
		{
			return true;
		}
		if ((_tac.m_camera != this.m_selectedItemButton.m_camera || this.m_shopButton.m_TAC == _tac) && this.m_popup == null)
		{
			this.UnselectItem(false);
			return true;
		}
		return false;
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00040C78 File Offset: 0x0003F078
	private void UnselectItem(bool _upgrade = false)
	{
		if (this.m_selectedItemButton != null)
		{
			this.m_selectedItemButton.HideInfo();
			this.m_selectedItemButton = null;
			SoundS.PlaySingleShot("/UI/UpgradeClose", Vector3.zero, 1f);
		}
		if (_upgrade)
		{
			(this.m_parent.m_parent.m_parent as PsUICenterGarage).m_vehicleStats.Upgrade();
		}
		else
		{
			(this.m_parent.m_parent.m_parent as PsUICenterGarage).m_vehicleStats.RemoveHighlight();
		}
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x00040D00 File Offset: 0x0003F100
	private void UpdateTiers()
	{
		PsUpgradeData vehicleUpgradeData = PsUpgradeManager.GetVehicleUpgradeData(this.m_vehicleType);
		for (int i = 0; i < this.m_upgradeTiers.Length; i++)
		{
			if (!this.m_upgradeTiers[i].m_unlocked && PsUpgradeManager.IsTierUnlocked(this.m_upgradeTiers[i].m_tierNumber, this.m_vehicleType))
			{
				this.m_upgradeTiers[i].Unlock();
			}
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00040D70 File Offset: 0x0003F170
	public void Purchase()
	{
		if (this.m_selectedItemButton != null)
		{
			PsUpgradeItem upgradeItem = PsUpgradeManager.GetUpgradeItem(this.m_vehicleType, this.m_selectedItemButton.m_itemIdentifier);
			if (upgradeItem.m_currentLevel == upgradeItem.m_maxLevel)
			{
				this.m_selectedItemButton.HideInfo();
				this.m_selectedItemButton = null;
				return;
			}
			if (PsMetagameManager.m_playerStats.coins >= upgradeItem.m_nextLevelPrice)
			{
				if (PsUpgradeManager.PurchaseUpgradeItem(this.m_vehicleType, this.m_selectedItemButton.m_itemIdentifier, false))
				{
					this.m_selectedItemButton.LevelUp();
					SoundS.PlaySingleShot("/UI/UpgradeInstall", Vector2.zero, 1f);
					if (this.UpgradeWasPurchased != null)
					{
						this.UpgradeWasPurchased();
					}
					this.UnselectItem(true);
				}
				else
				{
					this.UnselectItem(false);
				}
			}
			else
			{
				this.OpenCoinPopup(upgradeItem.m_nextLevelPrice);
			}
		}
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00040E64 File Offset: 0x0003F264
	private void OpenCoinPopup(int _price)
	{
		TouchAreaS.Enable();
		PsMetagameManager.m_coinsToDiamondsConvertAmount = _price - PsMetagameManager.m_playerStats.coins;
		this.m_popup = new PsUIBasePopup(typeof(PsUICenterNotEnoughCoinsConversion), null, null, null, true, true, InitialPage.Center, false, false, false);
		this.m_popup.SetAction("Exit", new Action(this.DestroyPopup));
		this.m_popup.SetAction("Upgrade", new Action(this.UpgradeWithDiamonds));
		TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00040F2C File Offset: 0x0003F32C
	public void UpgradeWithDiamonds()
	{
		this.DestroyPopup();
		if (PsUpgradeManager.PurchaseUpgradeItem(this.m_vehicleType, this.m_selectedItemButton.m_itemIdentifier, true))
		{
			this.m_selectedItemButton.LevelUp();
			this.UpdateTiers();
			SoundS.PlaySingleShot("/UI/UpgradeInstall", Vector2.zero, 1f);
			this.UnselectItem(true);
		}
		else
		{
			this.UnselectItem(true);
		}
		PsMetagameManager.m_coinsToDiamondsConvertAmount = 0;
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x00040F9E File Offset: 0x0003F39E
	public void DestroyPopup()
	{
		PsMetagameManager.HideResources();
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
			this.m_popup = null;
		}
		PsMetagameManager.m_coinsToDiamondsConvertAmount = 0;
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00040FC8 File Offset: 0x0003F3C8
	public override void Destroy()
	{
		this.m_destroyed = true;
		base.Destroy();
	}

	// Token: 0x04000639 RID: 1593
	private UIScrollableCanvas m_scrollArea;

	// Token: 0x0400063A RID: 1594
	private PsUIGenericButton m_purchaseButton;

	// Token: 0x0400063B RID: 1595
	private PsUIUpgradeView.UpgradeItemButton m_selectedItemButton;

	// Token: 0x0400063C RID: 1596
	private UIHorizontalList m_infoAreaContainer;

	// Token: 0x0400063D RID: 1597
	private UISprite m_infoIcon;

	// Token: 0x0400063E RID: 1598
	private UIText m_infoTitle;

	// Token: 0x0400063F RID: 1599
	private UIText m_infoEfficiency;

	// Token: 0x04000640 RID: 1600
	private UITextbox m_infoDescription;

	// Token: 0x04000641 RID: 1601
	private Type m_vehicleType;

	// Token: 0x04000642 RID: 1602
	private PsUIUpgradeView.UpgradeTier[] m_upgradeTiers;

	// Token: 0x04000643 RID: 1603
	private PsUIGenericButton m_shopButton;

	// Token: 0x04000644 RID: 1604
	private LastResourceView m_lastResourceView;

	// Token: 0x04000645 RID: 1605
	private PsUIBasePopup m_popup;

	// Token: 0x04000646 RID: 1606
	private bool m_destroyed;

	// Token: 0x020000E2 RID: 226
	// (Invoke) Token: 0x060004F4 RID: 1268
	public delegate void UpgradePurchased();

	// Token: 0x020000E3 RID: 227
	// (Invoke) Token: 0x060004F8 RID: 1272
	public delegate void UpgradeItemButtonTouchEvent(PsUIUpgradeView.UpgradeItemButton _button);

	// Token: 0x020000E4 RID: 228
	public class UpgradeItemButton : UIRectSpriteButton
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x00041658 File Offset: 0x0003FA58
		public UpgradeItemButton(UIComponent _parent, PsUpgradeItem _upgradeItem, bool _unlocked, PsUIUpgradeView.UpgradeItemButtonTouchEvent _touchEvent, bool _shopmode = false, string _tag = "cardfront")
			: base(_parent, _tag, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_upgradeItem.m_iconName, null), true, true)
		{
			this.m_tag = _tag;
			this.m_upgradeItem = _upgradeItem;
			this.m_shopmode = _shopmode;
			this.m_touchEvent = _touchEvent;
			this.m_itemIdentifier = _upgradeItem.m_identifier;
			this.m_unlocked = _unlocked;
			this.m_resourceRequirement = _upgradeItem.m_nextLevelResourceRequrement;
			this.SetSize(this.m_frame.width / this.m_frame.height, 1f, RelativeTo.ParentHeight);
			this.SetMargins(0f, 0f, 0f, 0f, RelativeTo.OwnHeight);
			string text = "#6aff00";
			if (!this.m_unlocked || (!PsUpgradeManager.IsDiscovered(_upgradeItem.m_identifier) && !this.m_shopmode))
			{
				base.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
				text = "#aaaaaa";
				if (!this.m_unlocked)
				{
					this.m_overrideMaterial.SetColor("_TintColor", new Color(0.3f, 0.3f, 0.3f));
					UISprite uisprite = new UISprite(this, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_lock", null), true);
					uisprite.SetSize(0.4f, 0.4f * (uisprite.m_frame.height / uisprite.m_frame.width), RelativeTo.ParentWidth);
				}
			}
			this.m_TAC.m_letTouchesThrough = true;
			if (_upgradeItem.m_currentLevel < _upgradeItem.m_maxLevel)
			{
				Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_HP_badge", null);
				this.m_efficiencyBackgroundSprite = new UISprite(this, false, string.Empty, PsState.m_uiSheet, frame, true);
				this.m_efficiencyBackgroundSprite.SetAlign(1f, 1f);
				float num = frame.width / this.m_frame.width;
				this.m_efficiencyBackgroundSprite.SetWidth(num, RelativeTo.ParentWidth);
				this.m_efficiencyBackgroundSprite.SetHeight(frame.height / frame.width * num, RelativeTo.ParentWidth);
				int num2;
				if (_upgradeItem.m_powerUpItem)
				{
					num2 = _upgradeItem.GetPowerUpItemPerformance();
				}
				else
				{
					num2 = Mathf.RoundToInt(_upgradeItem.m_nextLevelEfficiency);
				}
				this.m_efficiencyText = new UIText(this.m_efficiencyBackgroundSprite, false, "Efficiency", "+" + num2, PsFontManager.GetFont(PsFonts.HurmeBold), 0.7f, RelativeTo.ParentHeight, text, null);
				this.m_efficiencyText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
				if (this.m_unlocked)
				{
					this.CreateResourceProgression();
				}
			}
			if (this.m_unlocked && _upgradeItem.m_currentLevel > 0)
			{
				Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_levelbanner", null);
				this.m_levelBackgroundSprite = new UISprite(this, false, string.Empty, PsState.m_uiSheet, frame2, true);
				this.m_levelBackgroundSprite.SetAlign(0.5f, 0f);
				this.m_levelBackgroundSprite.SetWidth(1f, RelativeTo.ParentWidth);
				this.m_levelBackgroundSprite.SetHeight(frame2.height / frame2.width, RelativeTo.ParentWidth);
				this.m_levelBackgroundSprite.SetMargins(0f, 0f, 0.1f, 0f, RelativeTo.OwnHeight);
				this.m_levelText = new UIText(this.m_levelBackgroundSprite, false, "Level", PsStrings.Get(StringID.LEVEL).ToUpper() + " " + _upgradeItem.m_currentLevel, PsFontManager.GetFont(PsFonts.HurmeBold), 0.7f, RelativeTo.ParentHeight, "#91eeff", null);
				this.m_levelText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00041A1C File Offset: 0x0003FE1C
		public void UpdateButton()
		{
			if (this.m_unlocked)
			{
				if (this.m_upgradeItem.m_currentLevel < this.m_upgradeItem.m_maxLevel)
				{
					this.CreateResourceProgression();
				}
				if (PsUpgradeManager.IsDiscovered(this.m_upgradeItem.m_identifier))
				{
					base.SetOverrideShader(Shader.Find("Framework/VertexColorUnlitDouble"));
					if (this.m_efficiencyText != null)
					{
						this.m_efficiencyText.SetColor("#6aff00", null);
					}
				}
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00041A9C File Offset: 0x0003FE9C
		private void CreateResourceProgression()
		{
			if (this.m_resourceArea != null)
			{
				this.m_resourceArea.Destroy();
			}
			this.m_resourceArea = new UICanvas(this, false, string.Empty, null, string.Empty);
			this.m_resourceArea.SetHeight(0.26f, RelativeTo.ParentHeight);
			this.m_resourceArea.SetVerticalAlign(0f);
			this.m_resourceArea.RemoveDrawHandler();
			if (PsUpgradeManager.IsDiscovered(this.m_itemIdentifier) || this.m_shopmode)
			{
				this.m_resourceArea.SetMargins(0f, 0f, 1f, -1f, RelativeTo.OwnHeight);
				PsUIResourceProgressBar psUIResourceProgressBar = new PsUIResourceProgressBar(this.m_resourceArea, PsUpgradeManager.GetUpgradeResourceCount(this.m_itemIdentifier), this.m_resourceRequirement, this.m_tag, this.m_shopmode, null);
				psUIResourceProgressBar.SetHeight(1f, RelativeTo.ParentHeight);
				psUIResourceProgressBar.SetWidth(1f, RelativeTo.ParentWidth);
			}
			else
			{
				this.m_resourceArea.SetMargins(0f, 0f, 0.88f, -0.88f, RelativeTo.OwnHeight);
				Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_levelbanner", null);
				UISprite uisprite = new UISprite(this.m_resourceArea, false, string.Empty, PsState.m_uiSheet, frame, true);
				uisprite.SetAlign(0.5f, 1f);
				uisprite.SetWidth(1f, RelativeTo.ParentWidth);
				uisprite.SetMargins(0.22f, 0.22f, 0.2f, 0.1f, RelativeTo.OwnHeight);
				uisprite.SetHeight(frame.height / frame.width, RelativeTo.ParentWidth);
				UIFittedText uifittedText = new UIFittedText(uisprite, false, string.Empty, PsStrings.Get(StringID.NOT_UNLOCKED), PsFontManager.GetFont(PsFonts.HurmeSemiBold), true, "#bbbbbb", null);
				uifittedText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00041C62 File Offset: 0x00040062
		public override void Step()
		{
			if (this.m_hit && this.m_touchEvent != null)
			{
				this.m_touchEvent(this);
			}
			base.Step();
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00041C8C File Offset: 0x0004008C
		public void ShowInfo(PsUpgradeItem _upgradeItem, Action _purchaseAction)
		{
			this.m_selected = true;
			this.m_info = new PsUIUpgradeView.UpgradeItemInfo(this, _upgradeItem, this.m_unlocked, _purchaseAction, "cardfront", false);
			this.m_info.SetDepthOffset(-10f);
			this.m_info.Update();
			SoundS.PlaySingleShot("/UI/UpgradeSelect", Vector2.zero, 1f);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00041CEE File Offset: 0x000400EE
		public void HideInfo()
		{
			this.m_selected = false;
			if (this.m_info != null)
			{
				this.m_info.Destroy();
				this.m_info = null;
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00041D14 File Offset: 0x00040114
		public void Unlock()
		{
			if (this.m_unlocked)
			{
				return;
			}
			this.m_unlocked = true;
			if (this.m_efficiencyText != null)
			{
				this.m_efficiencyText.SetColor("#6aff00", null);
			}
			base.SetOverrideShader(null);
			this.CreateResourceProgression();
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00041D54 File Offset: 0x00040154
		private void LevelUpEffect()
		{
			UIText effect = new UIText(this, false, string.Empty, PsStrings.Get(StringID.ITEM_UPGRADE_EFFECT), PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, "#FFFFFF", "#000000");
			effect.Update();
			TweenC tweenC = TweenS.AddTransformTween(effect.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, new Vector3(1f, 0f), new Vector3(0f, 0f), 0.2f, 0.5f, false, false);
			TweenS.SetTweenAlphaProperties(tweenC, false, false, true, null);
			TweenC tweenC2 = TweenS.AddTransformTween(effect.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, new Vector3(0f, 37.5f, 0f), 0.5f, 0.2f, false);
			TweenS.AddTweenEndEventListener(tweenC2, delegate(TweenC c)
			{
				TweenS.RemoveComponent(c);
				effect.Destroy();
			});
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00041E2C File Offset: 0x0004022C
		public void LevelUp()
		{
			if (this.m_resourceArea != null)
			{
				this.m_resourceArea.Destroy();
				this.m_resourceArea = null;
			}
			this.LevelUpEffect();
			PsUpgradeItem upgradeItem = PsUpgradeManager.GetUpgradeItem(PsState.GetCurrentVehicleType(true), this.m_itemIdentifier);
			if (upgradeItem.m_currentLevel < upgradeItem.m_maxLevel)
			{
				this.m_resourceRequirement = upgradeItem.m_nextLevelResourceRequrement;
				this.CreateResourceProgression();
				if (this.m_efficiencyText != null)
				{
					int num;
					if (upgradeItem.m_powerUpItem)
					{
						num = upgradeItem.GetPowerUpItemPerformance();
					}
					else
					{
						num = Mathf.RoundToInt(upgradeItem.m_nextLevelEfficiency);
					}
					this.m_efficiencyText.SetText("+" + num);
				}
			}
			else if (this.m_efficiencyBackgroundSprite != null)
			{
				this.m_efficiencyBackgroundSprite.Destroy();
				this.m_efficiencyBackgroundSprite = null;
				this.m_efficiencyText = null;
			}
			if (this.m_levelText != null)
			{
				this.m_levelText.SetText(PsStrings.Get(StringID.LEVEL).ToUpper() + " " + upgradeItem.m_currentLevel);
			}
			else
			{
				Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_levelbanner", null);
				this.m_levelBackgroundSprite = new UISprite(this, false, string.Empty, PsState.m_uiSheet, frame, true);
				this.m_levelBackgroundSprite.SetAlign(0.5f, 0f);
				this.m_levelBackgroundSprite.SetWidth(1f, RelativeTo.ParentWidth);
				this.m_levelBackgroundSprite.SetHeight(frame.height / frame.width, RelativeTo.ParentWidth);
				this.m_levelBackgroundSprite.SetMargins(0f, 0f, 0.1f, 0f, RelativeTo.OwnHeight);
				this.m_levelText = new UIText(this.m_levelBackgroundSprite, false, "Level", PsStrings.Get(StringID.LEVEL).ToUpper() + " " + upgradeItem.m_currentLevel, PsFontManager.GetFont(PsFonts.HurmeBold), 0.7f, RelativeTo.ParentHeight, "#91eeff", null);
				this.m_levelText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			}
			this.Update();
		}

		// Token: 0x04000648 RID: 1608
		public const string EFFICIENCY_COLOR_UNLOCKED = "#6aff00";

		// Token: 0x04000649 RID: 1609
		public const string EFFICIENCY_COLOR_LOCKED = "#aaaaaa";

		// Token: 0x0400064A RID: 1610
		public PsUpgradeItem m_upgradeItem;

		// Token: 0x0400064B RID: 1611
		private bool m_selected;

		// Token: 0x0400064C RID: 1612
		public string m_itemIdentifier;

		// Token: 0x0400064D RID: 1613
		public bool m_unlocked;

		// Token: 0x0400064E RID: 1614
		private int m_resourceRequirement;

		// Token: 0x0400064F RID: 1615
		private PsUIUpgradeView.UpgradeItemButtonTouchEvent m_touchEvent;

		// Token: 0x04000650 RID: 1616
		private UICanvas m_resourceArea;

		// Token: 0x04000651 RID: 1617
		private UISprite m_efficiencyBackgroundSprite;

		// Token: 0x04000652 RID: 1618
		private UIText m_efficiencyText;

		// Token: 0x04000653 RID: 1619
		private UISprite m_levelBackgroundSprite;

		// Token: 0x04000654 RID: 1620
		private UIText m_levelText;

		// Token: 0x04000655 RID: 1621
		private bool m_shopmode;

		// Token: 0x04000656 RID: 1622
		private string m_tag;

		// Token: 0x04000657 RID: 1623
		private PsUIUpgradeView.UpgradeItemInfo m_info;
	}

	// Token: 0x020000E5 RID: 229
	public class GachaItemInfo : UIVerticalList
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x00042064 File Offset: 0x00040464
		public GachaItemInfo(UIComponent _parent, bool _unlocked, string _tag = "cardfront")
			: base(_parent, _tag)
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0004206E File Offset: 0x0004046E
		public void ShowBack()
		{
			EntityManager.SetVisibilityOfEntitiesWithTag(this.m_tag, false);
			EntityManager.SetVisibilityOfEntitiesWithTag("cardback", true);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00042089 File Offset: 0x00040489
		public void ShowFront()
		{
			EntityManager.SetVisibilityOfEntitiesWithTag(this.m_tag, true);
			EntityManager.SetVisibilityOfEntitiesWithTag("cardback", false);
		}

		// Token: 0x04000658 RID: 1624
		protected string m_tag;
	}

	// Token: 0x020000E6 RID: 230
	public class UpgradeItemInfo : PsUIUpgradeView.GachaItemInfo
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x000420A4 File Offset: 0x000404A4
		public UpgradeItemInfo(UIComponent _parent, PsUpgradeItem _upgradeItem, bool _unlocked, Action _purchaseAction, string _tag = "cardfront", bool _shopMode = false)
			: base(_parent, _unlocked, _tag)
		{
			this.m_tag = _tag;
			this.m_upgradeItem = _upgradeItem;
			this.m_purchaseAction = _purchaseAction;
			this.SetWidth(2.4f, RelativeTo.ParentWidth);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.UpgradeInfoBox));
			this.SetDepthOffset(-5f);
			this.SetVerticalAlign(0.72f);
			this.m_contentHolder = new UIVerticalList(this, "cardback");
			this.m_contentHolder.SetMargins(0.1f, 0.1f, 0.08f, 0.14f, RelativeTo.OwnWidth);
			this.m_contentHolder.SetSpacing(0.03f, RelativeTo.OwnWidth);
			this.m_contentHolder.RemoveDrawHandler();
			string text = PsStrings.Get(_upgradeItem.m_title).ToUpper();
			UITextbox uitextbox = new UITextbox(this.m_contentHolder, false, this.m_tag, text, PsFontManager.GetFont(PsFonts.HurmeBold), 0.08f, RelativeTo.ParentWidth, false, Align.Center, Align.Top, null, true, null);
			uitextbox.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			UICanvas uicanvas = new UICanvas(this.m_contentHolder, false, this.m_tag, null, string.Empty);
			uicanvas.SetSize(0.66f, 0.66f, RelativeTo.ParentWidth);
			uicanvas.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, this.m_tag, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_upgradeItem.m_iconName, null), true, true);
			if (!_unlocked || !PsUpgradeManager.IsDiscovered(_upgradeItem.m_identifier))
			{
				uifittedSprite.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
				if (!_unlocked)
				{
					uifittedSprite.m_overrideMaterial.SetColor("_TintColor", new Color(0.3f, 0.3f, 0.3f));
				}
			}
			if (_upgradeItem.m_currentLevel < _upgradeItem.m_maxLevel)
			{
				Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_HP_badge", null);
				UISprite uisprite = new UISprite(uifittedSprite, false, this.m_tag, PsState.m_uiSheet, frame, true);
				uisprite.SetAlign(1f, 1f);
				float num = frame.width / uifittedSprite.m_frame.width;
				uisprite.SetWidth(num, RelativeTo.ParentWidth);
				uisprite.SetHeight(frame.height / frame.width * num, RelativeTo.ParentWidth);
				int num2;
				if (_upgradeItem.m_powerUpItem)
				{
					num2 = _upgradeItem.GetPowerUpItemPerformance();
				}
				else
				{
					num2 = Mathf.RoundToInt(_upgradeItem.m_nextLevelEfficiency);
				}
				UIText uitext = new UIText(uisprite, false, this.m_tag, "+" + num2, PsFontManager.GetFont(PsFonts.HurmeBold), 0.7f, RelativeTo.ParentHeight, "#6aff00", null);
				uitext.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			}
			if (_unlocked && _upgradeItem.m_currentLevel > 0)
			{
				Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_icon_levelbanner", null);
				UISprite uisprite2 = new UISprite(uifittedSprite, false, this.m_tag, PsState.m_uiSheet, frame2, true);
				uisprite2.SetAlign(0.5f, 0f);
				uisprite2.SetWidth(1f, RelativeTo.ParentWidth);
				uisprite2.SetHeight(frame2.height / frame2.width, RelativeTo.ParentWidth);
				uisprite2.SetMargins(0f, 0f, 0.1f, 0f, RelativeTo.OwnHeight);
				UIText uitext2 = new UIText(uisprite2, false, this.m_tag, PsStrings.Get(StringID.LEVEL).ToUpper() + " " + _upgradeItem.m_currentLevel, PsFontManager.GetFont(PsFonts.HurmeBold), 0.7f, RelativeTo.ParentHeight, "#91eeff", null);
				uitext2.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			}
			TransformS.SetRotation(uifittedSprite.m_TC, new Vector3(0f, 0f, 3f));
			bool flag = _upgradeItem.m_typeName == StringID.SPECIAL;
			string text2 = PsStrings.Get(_upgradeItem.m_description) + ((!flag) ? string.Concat(new object[]
			{
				Environment.NewLine,
				"<color=",
				_upgradeItem.m_typeColor,
				">",
				PsStrings.Get(_upgradeItem.m_typeName),
				" +",
				_upgradeItem.m_powerNumber,
				"</color>"
			}) : string.Empty);
			UITextbox uitextbox2 = new UITextbox(this.m_contentHolder, false, this.m_tag, text2, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.072f, RelativeTo.ParentWidth, false, Align.Left, Align.Top, null, true, null);
			uitextbox2.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			uitextbox2.SetWidth(0.9f, RelativeTo.ParentWidth);
			uitextbox2.SetMinRows(4);
			uitextbox2.SetMaxRows(4);
			if (_upgradeItem.m_currentLevel < _upgradeItem.m_maxLevel && _unlocked)
			{
				if (PsUpgradeManager.GetUpgradeResourceCount(_upgradeItem.m_identifier) >= _upgradeItem.m_nextLevelResourceRequrement && this.m_purchaseAction != null)
				{
					this.m_levelUpButton = new PsUIGenericButton(this.m_contentHolder, 0.25f, 0.25f, 0.005f, "Button");
					EntityManager.AddTagForEntity(this.m_levelUpButton.m_TC.p_entity, this.m_tag);
					if (_upgradeItem.m_currentLevel == 0)
					{
						this.m_levelUpButton.SetFittedText(PsStrings.Get(StringID.INSTALL_ITEM), 0.03f, 0.9f, RelativeTo.ParentWidth, false);
					}
					else
					{
						this.m_levelUpButton.SetFittedText(PsStrings.Get(StringID.LEVEL_UP_ITEM), 0.03f, 0.9f, RelativeTo.ParentWidth, false);
					}
					this.m_levelUpButton.SetWidth(1f, RelativeTo.ParentWidth);
					if (_upgradeItem.m_nextLevelPrice > 0)
					{
						this.m_levelUpButton.SetCoinPrice(_upgradeItem.m_nextLevelPrice);
					}
					this.m_levelUpButton.SetGreenColors(true);
				}
				else if (PsUpgradeManager.IsDiscovered(_upgradeItem.m_identifier) || _shopMode)
				{
					this.m_resourceBar = new PsUIResourceProgressBar(this.m_contentHolder, PsUpgradeManager.GetUpgradeResourceCount(_upgradeItem.m_identifier), _upgradeItem.m_nextLevelResourceRequrement, this.m_tag, _shopMode, null);
					this.m_resourceBar.SetHeight(0.24f, RelativeTo.OwnWidth);
					this.m_resourceBar.SetWidth(1f, RelativeTo.ParentWidth);
				}
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000426F1 File Offset: 0x00040AF1
		public override void Step()
		{
			if (this.m_levelUpButton != null && this.m_levelUpButton.m_hit && this.m_purchaseAction != null)
			{
				this.m_purchaseAction.Invoke();
			}
			base.Step();
		}

		// Token: 0x04000659 RID: 1625
		private PsUIGenericButton m_levelUpButton;

		// Token: 0x0400065A RID: 1626
		private Action m_purchaseAction;

		// Token: 0x0400065B RID: 1627
		public UIVerticalList m_contentHolder;

		// Token: 0x0400065C RID: 1628
		public PsUIResourceProgressBar m_resourceBar;

		// Token: 0x0400065D RID: 1629
		public PsUpgradeItem m_upgradeItem;
	}

	// Token: 0x020000E7 RID: 231
	public class UpgradeTier : UICanvas
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0004272C File Offset: 0x00040B2C
		public UpgradeTier(UIComponent _parent, int _tierNumber, PsUpgradeData _upgradeData, PsUIUpgradeView.UpgradeItemButtonTouchEvent _touchEvent)
			: base(_parent, false, "UpgradeTier", null, string.Empty)
		{
			this.m_tierNumber = _tierNumber;
			this.m_unlocked = PsUpgradeManager.IsTierUnlocked(_tierNumber, PsState.GetCurrentVehicleType(true));
			this.m_upgradeItemButtons = new PsUIUpgradeView.UpgradeItemButton[4];
			this.RemoveDrawHandler();
			this.CreateBackground();
			if (!this.m_unlocked)
			{
				string text = PsStrings.Get(StringID.UNLOCKS_AT_LEAGUE);
				text = text.Replace("%1", PsMetagameData.GetLeague(Mathf.RoundToInt(_upgradeData.GetTierLimit(_tierNumber))).m_name);
				this.m_requirementText = new UIText(this, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.14f, RelativeTo.ParentHeight, "#aaaaaa", null);
				this.m_requirementText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
				this.m_requirementText.SetVerticalAlign(0.98f);
			}
			this.m_buttonList = new UIHorizontalList(this, "UpgradeItems");
			this.m_buttonList.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_buttonList.SetSpacing(0.048f, RelativeTo.ParentWidth);
			this.m_buttonList.RemoveDrawHandler();
			if (this.m_unlocked)
			{
				this.m_buttonList.SetMargins(0f, 0f, 0.04f, 0.07f, RelativeTo.ParentWidth);
			}
			else
			{
				this.m_buttonList.SetMargins(0f, 0f, 0.07f, 0.04f, RelativeTo.ParentWidth);
			}
			PsUpgradeItem[] upgradeItemsByTier = _upgradeData.GetUpgradeItemsByTier(_tierNumber);
			for (int i = 0; i < 4; i++)
			{
				if (upgradeItemsByTier.Length > i)
				{
					this.m_upgradeItemButtons[i] = new PsUIUpgradeView.UpgradeItemButton(this.m_buttonList, upgradeItemsByTier[i], this.m_unlocked, _touchEvent, false, "cardfront");
				}
				else
				{
					UICanvas uicanvas = new UICanvas(this.m_buttonList, true, "UpgradeItemPlaceHolder", null, string.Empty);
					uicanvas.SetSize(1f, 1f, RelativeTo.ParentHeight);
					uicanvas.m_TAC.m_letTouchesThrough = true;
				}
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0004291C File Offset: 0x00040D1C
		private void CreateBackground()
		{
			if (this.m_background != null)
			{
				this.m_background.Destroy();
				this.m_background = null;
			}
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame((!this.m_unlocked) ? "menu_garage_grid_row_locked" : "menu_garage_grid_row_unlocked", null);
			this.m_background = new UISprite(this, false, "Background", PsState.m_uiSheet, frame, true);
			this.m_background.SetSize(1f, frame.height / frame.width, RelativeTo.ParentWidth);
			this.SetSize(1f, frame.height / frame.width, RelativeTo.ParentWidth);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000429C4 File Offset: 0x00040DC4
		public void UpdateButtons()
		{
			for (int i = 0; i < this.m_upgradeItemButtons.Length; i++)
			{
				this.m_upgradeItemButtons[i].UpdateButton();
			}
			this.Update();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00042A00 File Offset: 0x00040E00
		public void Unlock()
		{
			if (this.m_unlocked)
			{
				return;
			}
			this.m_unlocked = true;
			if (this.m_requirementText != null)
			{
				this.m_requirementText.Destroy();
				this.m_requirementText = null;
			}
			this.CreateBackground();
			this.m_buttonList.SetMargins(0f, 0f, 0.04f, 0.07f, RelativeTo.ParentWidth);
			for (int i = 0; i < this.m_upgradeItemButtons.Length; i++)
			{
				this.m_upgradeItemButtons[i].Unlock();
			}
			this.Update();
		}

		// Token: 0x0400065F RID: 1631
		public const string TIER_UNLOCKED_BACKGROUND = "menu_garage_grid_row_unlocked";

		// Token: 0x04000660 RID: 1632
		public const string TIER_LOCKED_BACKGROUND = "menu_garage_grid_row_locked";

		// Token: 0x04000661 RID: 1633
		public const string EFFICIENCY_COLOR_LOCKED = "#aaaaaa";

		// Token: 0x04000662 RID: 1634
		public int m_tierNumber;

		// Token: 0x04000663 RID: 1635
		public bool m_unlocked;

		// Token: 0x04000664 RID: 1636
		private UISprite m_background;

		// Token: 0x04000665 RID: 1637
		private UIHorizontalList m_buttonList;

		// Token: 0x04000666 RID: 1638
		private PsUIUpgradeView.UpgradeItemButton[] m_upgradeItemButtons;

		// Token: 0x04000667 RID: 1639
		private UIText m_requirementText;
	}
}
