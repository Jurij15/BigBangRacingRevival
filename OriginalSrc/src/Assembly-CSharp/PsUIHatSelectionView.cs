using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class PsUIHatSelectionView : PsUICurtomisationView
{
	// Token: 0x060004B1 RID: 1201 RVA: 0x0003CDB8 File Offset: 0x0003B1B8
	public PsUIHatSelectionView(UIComponent _parent, Type _vehicleType)
		: base(_parent, "HatSelectionView")
	{
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetVehicleCustomisationData(_vehicleType).GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		this.m_vehicleType = _vehicleType;
		if (installedItemByCategory != null)
		{
			this.m_selectedItem = installedItemByCategory.m_identifier;
		}
		else
		{
			this.m_selectedItem = "MotocrossHelmet";
		}
		this.m_itemButtons = new List<PsUICustomisationItemButton>();
		this.m_scrollArea = new UIScrollableCanvas(this, "ScrollCanvas");
		this.m_scrollArea.FreezeHorizontalScroll(false);
		this.m_scrollArea.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_scrollArea.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_scrollArea.SetMargins(0.15f, 0.15f, 0.15f, 0.15f, RelativeTo.ParentWidth);
		this.m_scrollArea.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this.m_scrollArea, "UpgradeTiers");
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(uiverticalList, false, "Header", null, string.Empty);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetSize(1f, 0.15f, RelativeTo.ParentWidth);
		uicanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas, string.Empty);
		uihorizontalList.SetHeight(1f, RelativeTo.ParentHeight);
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.RemoveDrawHandler();
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
		uisprite2.SetWidth(0.65f, RelativeTo.ParentWidth);
		uisprite3.SetHeight(1f, RelativeTo.ParentHeight);
		uisprite3.SetWidth(frame4.width / frame4.height, RelativeTo.ParentHeight);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uihorizontalList, "HeaderContent");
		uihorizontalList2.SetHeight(1f, RelativeTo.ParentHeight);
		uihorizontalList2.SetRogue();
		uihorizontalList2.SetSpacing(0.2f, RelativeTo.ParentHeight);
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetMargins(0f, 0f, -0.1f, 0.1f, RelativeTo.ParentHeight);
		Frame frame5 = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_icon_hats", null);
		UISprite uisprite4 = new UISprite(uihorizontalList2, false, "HeaderIcon", PsState.m_uiSheet, frame5, true);
		uisprite4.SetSize(frame5.width / frame5.height, 1f, RelativeTo.ParentHeight);
		UIText uitext = new UIText(uihorizontalList2, false, "Title", PsStrings.Get(StringID.GARAGE_HATS_TITLE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.5f, RelativeTo.ParentHeight, "#81c3eb", null);
		List<PsCustomisationItem> itemsByCategory = PsCustomisationManager.GetVehicleCustomisationData(this.m_vehicleType).GetItemsByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		Dictionary<PsRarity, List<PsCustomisationItem>> dictionary = new Dictionary<PsRarity, List<PsCustomisationItem>>();
		dictionary.Add(PsRarity.Common, itemsByCategory.FindAll((PsCustomisationItem item) => item.m_rarity == PsRarity.Common));
		dictionary.Add(PsRarity.Rare, itemsByCategory.FindAll((PsCustomisationItem item) => item.m_rarity == PsRarity.Rare));
		dictionary.Add(PsRarity.Epic, itemsByCategory.FindAll((PsCustomisationItem item) => item.m_rarity == PsRarity.Epic || (item.m_rarity == PsRarity.Exclusive && item.m_unlocked)));
		int num2 = (int)((float)dictionary[PsRarity.Common].Count / 3f) + ((dictionary[PsRarity.Common].Count % 3 != 0) ? 1 : 0);
		int num3 = (int)((float)dictionary[PsRarity.Common].Count / 3f) + ((dictionary[PsRarity.Common].Count % 3 != 0) ? 1 : 0);
		int num4 = (int)((float)dictionary[PsRarity.Common].Count / 3f) + ((dictionary[PsRarity.Common].Count % 3 != 0) ? 1 : 0);
		int num5 = (int)((float)dictionary[PsRarity.Common].Count / 3f) + ((dictionary[PsRarity.Common].Count % 3 != 0) ? 1 : 0);
		int num6 = num2 + num3 + num4 + num5;
		UICanvas uicanvas2 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetDepthOffset((float)(-(float)num6));
		uicanvas2.RemoveDrawHandler();
		uicanvas2.SetMargins(-0.015f, 0.015f, 0f, 0f, RelativeTo.OwnWidth);
		UISprite uisprite5 = new UISprite(uicanvas2, false, "Background", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_grid_row_unlocked", null), true);
		uisprite5.SetWidth(1f, RelativeTo.ParentWidth);
		uisprite5.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas2.SetSize(1.07f, uisprite5.m_frame.height / uisprite5.m_frame.width, RelativeTo.ParentWidth);
		this.m_shopButton = new PsUIGenericButton(uisprite5, 0.25f, 0.25f, 0.005f, "Button");
		this.m_shopButton.SetIcon("menu_icon_chest", 0.11f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_shopButton.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_shopButton.SetDepthOffset(-2f);
		this.m_shopButton.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		this.m_shopButton.SetVerticalAlign(0.55f);
		UIVerticalList uiverticalList2 = new UIVerticalList(this.m_shopButton, string.Empty);
		uiverticalList2.RemoveDrawHandler();
		UIText uitext2 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.BUTTON_GET_MORE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
		UIText uitext3 = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.HAT_BUTTON_GET_MORE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, null, null);
		List<PsRarity> list = new List<PsRarity>(dictionary.Keys);
		int num7 = -num6;
		for (int i = 0; i < list.Count; i++)
		{
			PsRarity psRarity = list[i];
			int num8 = (int)((float)dictionary[psRarity].Count / 3f) + ((dictionary[psRarity].Count % 3 != 0) ? 1 : 0);
			for (int j = 0; j < num8; j++)
			{
				num7++;
				UIHorizontalList uihorizontalList3 = new UIHorizontalList(uiverticalList, "Row");
				uihorizontalList3.RemoveDrawHandler();
				Frame frame6 = null;
				string text = string.Empty;
				Frame frame7;
				switch (psRarity)
				{
				case PsRarity.Rare:
					frame7 = PsState.m_uiSheet.m_atlas.GetFrame("menu_shelf_middle_rare", null);
					if (j == 0)
					{
						frame6 = PsState.m_uiSheet.m_atlas.GetFrame("menu_shelf_middle_rare_label", null);
						text = PsStrings.Get(StringID.GACHA_RARITY_RARE);
					}
					break;
				case PsRarity.Epic:
				case PsRarity.Exclusive:
					frame7 = PsState.m_uiSheet.m_atlas.GetFrame("menu_shelf_middle_epic", null);
					if (j == 0)
					{
						frame6 = PsState.m_uiSheet.m_atlas.GetFrame("menu_shelf_middle_epic_label", null);
						text = PsStrings.Get(StringID.GACHA_RARITY_EPIC);
					}
					break;
				default:
					if (j == 0)
					{
						frame7 = PsState.m_uiSheet.m_atlas.GetFrame("menu_shelf_top", null);
					}
					else
					{
						frame7 = PsState.m_uiSheet.m_atlas.GetFrame("menu_shelf_middle", null);
					}
					break;
				}
				UISprite uisprite6 = new UISprite(uihorizontalList3, false, "Shelf", PsState.m_uiSheet, frame7, true);
				uisprite6.SetRogue();
				uisprite6.SetSize(1.1f, 1.1f * (frame7.height / frame7.width), RelativeTo.ParentWidth);
				uisprite6.SetAlign(0.5f, 0f);
				uisprite6.SetDepthOffset((float)num7);
				if (frame6 != null)
				{
					UIHorizontalList uihorizontalList4 = new UIHorizontalList(uisprite6, string.Empty);
					uihorizontalList4.SetHeight(1f, RelativeTo.ParentHeight);
					uihorizontalList4.SetMargins(-0.24f, 0.24f, 0.02f, 0.23f, RelativeTo.ParentHeight);
					uihorizontalList4.SetHorizontalAlign(0f);
					uihorizontalList4.SetRogue();
					uihorizontalList4.RemoveDrawHandler();
					UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList4, false, "ShelfLabel", PsState.m_uiSheet, frame6, true, true);
					uifittedSprite.SetDepthOffset(2f);
					UIComponent uicomponent = new UIComponent(uifittedSprite, false, string.Empty, null, null, string.Empty);
					uicomponent.SetWidth(0.92f, RelativeTo.ParentHeight);
					uicomponent.SetHeight(0.8f, RelativeTo.ParentWidth);
					uicomponent.RemoveDrawHandler();
					UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
					TransformS.SetRotation(uifittedText.m_TC, new Vector3(0f, 0f, 90f));
				}
				for (int k = 3 * j; k < 3 * (j + 1); k++)
				{
					if (k < dictionary[psRarity].Count)
					{
						this.m_itemButtons.Add(new PsUICustomisationItemButton(uihorizontalList3, dictionary[psRarity][k], new PsUICustomisationItemButton.ItemButtonTouchEvent(this.HatItemButtonTouchEvent)));
						this.m_itemButtons[this.m_itemButtons.Count - 1].SetDepthOffset((float)(num7 - 1));
						if (dictionary[psRarity][k].m_identifier == this.m_selectedItem)
						{
							this.m_itemButtons[this.m_itemButtons.Count - 1].Mark(true);
						}
					}
					else
					{
						UICanvas uicanvas3 = new UICanvas(uihorizontalList3, false, string.Empty, null, string.Empty);
						uicanvas3.SetSize(0.33333334f, 0.29850748f, RelativeTo.ParentWidth);
						uicanvas3.RemoveDrawHandler();
					}
				}
			}
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0003D7FA File Offset: 0x0003BBFA
	public override void ShowUI()
	{
		if (this.m_itemChangedAction != null && !string.IsNullOrEmpty(this.m_selectedItem))
		{
			this.m_itemChangedAction.Invoke(this.m_selectedItem);
		}
		base.ShowUI();
		this.m_scrollArea.AdjustCamera();
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0003D83C File Offset: 0x0003BC3C
	public override void UpdateItems()
	{
		for (int i = 0; i < this.m_itemButtons.Count; i++)
		{
			if (this.m_itemButtons[i].m_item.m_unlocked)
			{
				this.m_itemButtons[i].DestroyChildren(1);
				this.m_itemButtons[i].m_highlightSprite = null;
				(this.m_itemButtons[i].m_childs[0].m_childs[0] as UISprite).SetColor(Color.gray);
				this.SetHatVisibility(this.m_selectedItem);
			}
		}
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x0003D8E4 File Offset: 0x0003BCE4
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

	// Token: 0x060004B5 RID: 1205 RVA: 0x0003D973 File Offset: 0x0003BD73
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

	// Token: 0x060004B6 RID: 1206 RVA: 0x0003D9A4 File Offset: 0x0003BDA4
	public bool UnselectItem(TouchAreaC _tac)
	{
		if (this.m_selectedItemButton == null || this.m_destroyed)
		{
			return true;
		}
		if (_tac.m_camera != this.m_selectedItemButton.m_camera && !EntityManager.EntityHasTag(_tac.p_entity, "RotateCanvas"))
		{
			if (this.m_selectedPreviewItemButton != null)
			{
				this.m_itemChangedAction.Invoke(this.m_selectedItem);
				this.m_selectedPreviewItemButton = null;
			}
			this.UnselectItem();
			return true;
		}
		return false;
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x0003DA25 File Offset: 0x0003BE25
	private void UnselectItem()
	{
		if (this.m_selectedItemButton != null)
		{
			this.m_selectedItemButton.HideInfo();
			this.m_selectedItemButton = null;
			SoundS.PlaySingleShot("/UI/UpgradeClose", Vector3.zero, 1f);
		}
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x0003DA58 File Offset: 0x0003BE58
	private void UnselectPreviewItem()
	{
		if (this.m_selectedPreviewItemButton != null)
		{
			this.m_selectionChanged = true;
			this.m_selectedPreviewItemButton = null;
		}
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0003DA74 File Offset: 0x0003BE74
	private void HatItemButtonTouchEvent(PsUICustomisationItemButton _button)
	{
		if (_button.m_item != null)
		{
			if (_button.m_item.m_unlocked)
			{
				this.UnselectItem();
				this.UnselectPreviewItem();
				if (this.m_selectedItem != _button.m_item.m_identifier && PsCustomisationManager.InstallVehicleItem(this.m_vehicleType, _button.m_item.m_identifier))
				{
					this.m_selectedItem = _button.m_item.m_identifier;
					this.m_selectionChanged = true;
					this.SetHatVisibility(this.m_selectedItem);
					SoundS.PlaySingleShot("/UI/ChangeHat", Vector2.zero, 1f);
				}
			}
			else if (this.m_selectedItemButton == _button)
			{
				this.UnselectItem();
				this.m_itemChangedAction.Invoke(this.m_selectedItem);
			}
			else
			{
				this.UnselectItem();
				this.m_selectedPreviewItemButton = _button;
				this.m_itemChangedAction.Invoke(this.m_selectedPreviewItemButton.m_item.m_identifier);
				this.m_selectedItemButton = _button;
				_button.ShowInfo(_button.m_item, delegate
				{
					PsPurchaseHelper.PurchaseIAP(_button.m_item.m_iapIdentifier, null, delegate
					{
						this.UnselectItem();
						_button.CreateUnlockedMark();
						_button.Update();
						this.m_selectedItem = _button.m_item.m_identifier;
						PsCustomisationManager.InstallVehicleItem(this.m_vehicleType, this.m_selectedItem);
						this.m_selectionChanged = true;
						this.SetHatVisibility(this.m_selectedItem);
					});
				});
				TouchAreaS.AddBeginTouchDelegate(new Func<TouchAreaC, bool>(this.UnselectItem));
			}
		}
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0003DBE8 File Offset: 0x0003BFE8
	public void SetHatVisibility(string _identifier)
	{
		for (int i = 0; i < this.m_itemButtons.Count; i++)
		{
			if (this.m_itemButtons[i].m_item == null)
			{
				if (string.IsNullOrEmpty(_identifier))
				{
					this.m_itemButtons[i].Mark(true);
				}
				else
				{
					this.m_itemButtons[i].Mark(false);
				}
			}
			else if (this.m_itemButtons[i].m_item.m_identifier == _identifier)
			{
				this.m_itemButtons[i].Mark(true);
			}
			else
			{
				this.m_itemButtons[i].Mark(false);
			}
		}
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0003DCAA File Offset: 0x0003C0AA
	public override void Destroy()
	{
		this.m_destroyed = true;
		base.Destroy();
	}

	// Token: 0x04000612 RID: 1554
	private UIScrollableCanvas m_scrollArea;

	// Token: 0x04000613 RID: 1555
	public List<PsUICustomisationItemButton> m_itemButtons;

	// Token: 0x04000614 RID: 1556
	public Type m_vehicleType;

	// Token: 0x04000615 RID: 1557
	private PsUIGenericButton m_shopButton;

	// Token: 0x04000616 RID: 1558
	private LastResourceView m_lastResourceView;

	// Token: 0x04000617 RID: 1559
	private bool m_destroyed;

	// Token: 0x04000618 RID: 1560
	private PsUICustomisationItemButton m_selectedItemButton;

	// Token: 0x04000619 RID: 1561
	private PsUICustomisationItemButton m_selectedPreviewItemButton;
}
