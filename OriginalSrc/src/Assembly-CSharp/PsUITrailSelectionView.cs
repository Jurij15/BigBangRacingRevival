using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class PsUITrailSelectionView : PsUICurtomisationView
{
	// Token: 0x060004C5 RID: 1221 RVA: 0x0003EC4C File Offset: 0x0003D04C
	public PsUITrailSelectionView(UIComponent _parent, Type _vehicleType)
		: base(_parent, "TrailSelectionView")
	{
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetVehicleCustomisationData(_vehicleType).GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.TRAIL);
		this.m_vehicleType = _vehicleType;
		if (installedItemByCategory != null)
		{
			this.m_selectedItem = installedItemByCategory.m_identifier;
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
		Frame frame5 = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_icon_trails", null);
		UISprite uisprite4 = new UISprite(uihorizontalList2, false, "HeaderIcon", PsState.m_uiSheet, frame5, true);
		uisprite4.SetSize(frame5.width / frame5.height, 1f, RelativeTo.ParentHeight);
		UIText uitext = new UIText(uihorizontalList2, false, "Title", PsStrings.Get(StringID.TRAILS_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.5f, RelativeTo.ParentHeight, "#81c3eb", null);
		uitext.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		List<PsCustomisationItem> itemsByCategory = PsCustomisationManager.GetVehicleCustomisationData(this.m_vehicleType).GetItemsByCategory(PsCustomisationManager.CustomisationCategory.TRAIL);
		itemsByCategory.RemoveAll((PsCustomisationItem item) => item.m_rarity == PsRarity.Exclusive && !item.m_unlocked);
		int num2 = (int)((float)(itemsByCategory.Count + 1) / 3f) + (((itemsByCategory.Count + 1) % 3 != 0) ? 1 : 0);
		for (int i = 0; i < Mathf.Max(num2, 4); i++)
		{
			UIHorizontalList uihorizontalList3 = new UIHorizontalList(uiverticalList, "Row");
			uihorizontalList3.RemoveDrawHandler();
			Frame frame6 = null;
			string empty = string.Empty;
			Frame frame7;
			if (i == 0)
			{
				frame7 = PsState.m_uiSheet.m_atlas.GetFrame("menu_shelf_top", null);
			}
			else
			{
				frame7 = PsState.m_uiSheet.m_atlas.GetFrame("menu_shelf_middle", null);
			}
			UISprite uisprite5 = new UISprite(uihorizontalList3, false, "Shelf", PsState.m_uiSheet, frame7, true);
			uisprite5.SetRogue();
			uisprite5.SetSize(1.1f, 1.1f * (frame7.height / frame7.width), RelativeTo.ParentWidth);
			uisprite5.SetAlign(0.5f, 0f);
			uisprite5.SetDepthOffset((float)(-(float)(num2 - i)));
			if (frame6 != null)
			{
				UIHorizontalList uihorizontalList4 = new UIHorizontalList(uisprite5, string.Empty);
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
				UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, empty, PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
				TransformS.SetRotation(uifittedText.m_TC, new Vector3(0f, 0f, 90f));
			}
			int num3 = 3 * i - 1;
			while (num3 + 1 < 3 * (i + 1))
			{
				if (num3 < itemsByCategory.Count)
				{
					if (num3 < 0)
					{
						this.m_itemButtons.Add(new PsUICustomisationItemButton(uihorizontalList3, null, new PsUICustomisationItemButton.ItemButtonTouchEvent(this.ItemButtonTouchEvent)));
						if (string.IsNullOrEmpty(this.m_selectedItem))
						{
							this.m_itemButtons[this.m_itemButtons.Count - 1].Mark(true);
						}
					}
					else
					{
						this.m_itemButtons.Add(new PsUICustomisationItemButton(uihorizontalList3, itemsByCategory[num3], new PsUICustomisationItemButton.ItemButtonTouchEvent(this.ItemButtonTouchEvent)));
						if (itemsByCategory[num3].m_identifier == this.m_selectedItem)
						{
							this.m_itemButtons[this.m_itemButtons.Count - 1].Mark(true);
						}
					}
					this.m_itemButtons[this.m_itemButtons.Count - 1].SetDepthOffset((float)(-(float)(num2 - i) - 1));
				}
				else
				{
					UICanvas uicanvas2 = new UICanvas(uihorizontalList3, false, string.Empty, null, string.Empty);
					uicanvas2.SetSize(0.33333334f, 0.29850748f, RelativeTo.ParentWidth);
					uicanvas2.RemoveDrawHandler();
				}
				num3++;
			}
		}
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0003F326 File Offset: 0x0003D726
	public override void ShowUI()
	{
		if (this.m_itemChangedAction != null && !string.IsNullOrEmpty(this.m_selectedItem))
		{
			this.m_itemChangedAction.Invoke(this.m_selectedItem);
		}
		base.ShowUI();
		this.m_scrollArea.AdjustCamera();
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0003F365 File Offset: 0x0003D765
	public override void HideUI(Action _callback)
	{
		base.HideUI(_callback);
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0003F36E File Offset: 0x0003D76E
	public override void UpdateItems()
	{
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0003F370 File Offset: 0x0003D770
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

	// Token: 0x060004CA RID: 1226 RVA: 0x0003F3F1 File Offset: 0x0003D7F1
	private void UnselectItem()
	{
		if (this.m_selectedItemButton != null)
		{
			this.m_selectedItemButton.HideInfo();
			this.m_selectedItemButton = null;
			SoundS.PlaySingleShot("/UI/UpgradeClose", Vector3.zero, 1f);
		}
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0003F424 File Offset: 0x0003D824
	private void UnselectPreviewItem()
	{
		if (this.m_selectedPreviewItemButton != null)
		{
			this.m_selectionChanged = true;
			this.m_selectedPreviewItemButton = null;
		}
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0003F440 File Offset: 0x0003D840
	private void ItemButtonTouchEvent(PsUICustomisationItemButton _button)
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
					this.SetTrailVisibility(this.m_selectedItem);
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
						this.SetTrailVisibility(this.m_selectedItem);
					});
				});
				TouchAreaS.AddBeginTouchDelegate(new Func<TouchAreaC, bool>(this.UnselectItem));
			}
		}
		else
		{
			this.UnselectItem();
			this.UnselectPreviewItem();
			if (PsCustomisationManager.UninstallVehicleItem(this.m_vehicleType, this.m_selectedItem))
			{
				this.m_selectedItem = string.Empty;
				this.m_selectionChanged = true;
				this.SetTrailVisibility(this.m_selectedItem);
				SoundS.PlaySingleShot("/UI/ChangeHat", Vector2.zero, 1f);
			}
		}
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x0003F610 File Offset: 0x0003DA10
	public void SetTrailVisibility(string _identifier)
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

	// Token: 0x060004CE RID: 1230 RVA: 0x0003F6D2 File Offset: 0x0003DAD2
	public override void Destroy()
	{
		this.m_destroyed = true;
		base.Destroy();
	}

	// Token: 0x04000623 RID: 1571
	private UIScrollableCanvas m_scrollArea;

	// Token: 0x04000624 RID: 1572
	public List<PsUICustomisationItemButton> m_itemButtons;

	// Token: 0x04000625 RID: 1573
	public Type m_vehicleType;

	// Token: 0x04000626 RID: 1574
	private bool m_destroyed;

	// Token: 0x04000627 RID: 1575
	private PsUICustomisationItemButton m_selectedItemButton;

	// Token: 0x04000628 RID: 1576
	private PsUICustomisationItemButton m_selectedPreviewItemButton;
}
