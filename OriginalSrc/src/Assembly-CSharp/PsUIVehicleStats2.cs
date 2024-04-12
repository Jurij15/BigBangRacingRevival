using System;
using UnityEngine;

// Token: 0x020002AB RID: 683
public class PsUIVehicleStats2 : UISprite
{
	// Token: 0x0600148D RID: 5261 RVA: 0x000D27F4 File Offset: 0x000D0BF4
	public PsUIVehicleStats2(UIComponent _parent, Type _vehicleType, int _ccValue, int _motorValue, int _gripValue, int _handlingValue, int _powerUpValue)
		: base(_parent, false, "VehicleStats", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_cc_back", null), true)
	{
		this.m_vehicleType = _vehicleType;
		this.m_performanceValue = _ccValue;
		this.m_motorValue = _motorValue;
		this.m_gripValue = _gripValue;
		this.m_handlingValue = _handlingValue;
		this.m_powerUpValue = _powerUpValue;
		this.SetDepthOffset(-5f);
		this.SetSize(this.m_frame.width / this.m_frame.height * 0.26f, 0.26f, RelativeTo.ScreenHeight);
		this.SetMargins(0f, 0f, 0f, -0.01f, RelativeTo.OwnHeight);
		this.m_container = new UIVerticalList(this, "VehicleStats");
		this.m_container.SetVerticalAlign(0f);
		this.m_container.RemoveDrawHandler();
		this.CreateContent();
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x000D28DC File Offset: 0x000D0CDC
	private void CreateContent()
	{
		string text = string.Empty;
		if (this.m_vehicleType == typeof(OffroadCar))
		{
			text = "menu_vehicle_logo_offroader";
		}
		else
		{
			text = "menu_vehicle_logo_dirtbike";
		}
		UISprite uisprite = new UISprite(this.m_container, false, "VehicleStats", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true);
		uisprite.SetSize(uisprite.m_frame.width / uisprite.m_frame.height * 0.2f, 0.2f, RelativeTo.ParentHeight);
		UICanvas uicanvas = new UICanvas(this.m_container, false, "VehicleStats", null, string.Empty);
		uicanvas.SetHeight(0.52f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas, "VehicleStats");
		uihorizontalList.RemoveDrawHandler();
		this.m_performanceText = new UIText(uihorizontalList, false, "VehicleStats", this.m_performanceValue.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 1f, RelativeTo.ParentHeight, "#ffffff", "#000000");
		this.m_ccText = new UIText(uihorizontalList, false, "VehicleStats", "cc", PsFontManager.GetFont(PsFonts.HurmeBold), 0.65f, RelativeTo.ParentHeight, "#ffffff", "#000000");
		this.m_ccText.SetVerticalAlign(0.15f);
		int num = this.m_performanceValue / 100 * 100;
		if (PsMetagameManager.GetLastOpenedUpgradeChest(this.m_vehicleType) >= num)
		{
			UIHorizontalList uihorizontalList2 = new UIHorizontalList(this.m_container, "VehicleStats");
			uihorizontalList2.SetHeight(0.2f, RelativeTo.ParentHeight);
			uihorizontalList2.SetSpacing(0.01f, RelativeTo.ParentHeight);
			uihorizontalList2.SetMargins(0f, 0f, 0.06f, 0.06f, RelativeTo.OwnHeight);
			uihorizontalList2.RemoveDrawHandler();
			this.m_subStats = new PsUIVehicleStats2.SubStats[4];
			this.m_subStats[0] = new PsUIVehicleStats2.SubStats(uihorizontalList2, "menu_upgrade_icon_engine", this.m_motorValue);
			this.m_subStats[1] = new PsUIVehicleStats2.SubStats(uihorizontalList2, "menu_upgrade_icon_wheel", this.m_gripValue);
			this.m_subStats[2] = new PsUIVehicleStats2.SubStats(uihorizontalList2, "menu_upgrade_icon_steer", this.m_handlingValue);
			this.m_subStats[3] = new PsUIVehicleStats2.SubStats(uihorizontalList2, "menu_upgrade_icon_special", this.m_powerUpValue);
			this.m_freeChestArea = new UIHorizontalList(this.m_container, "VehicleStats");
			this.m_freeChestArea.SetHeight(0.16f, RelativeTo.ParentHeight);
			this.m_freeChestArea.SetSpacing(0.03f, RelativeTo.ParentHeight);
			this.m_freeChestArea.RemoveDrawHandler();
			this.CreateFreeChestText();
		}
		else
		{
			UICanvas uicanvas2 = new UICanvas(this.m_container, false, "VehicleStats", null, string.Empty);
			uicanvas2.SetHeight(0.36f, RelativeTo.ParentHeight);
			uicanvas2.RemoveDrawHandler();
			GachaType upgradeChestType = PsGachaManager.GetUpgradeChestType(num);
			string chestIconName = PsGachaManager.GetChestIconName(upgradeChestType);
			this.m_openChestButton = new PsUIAttentionButton(uicanvas2, default(Vector3), 0.25f, 0.25f, 0.005f);
			this.m_openChestButton.SetSound("/UI/ButtonChestOpen");
			this.m_openChestButton.SetMargins(0.02f, 0.02f, 0.009f, 0.009f, RelativeTo.ScreenHeight);
			this.m_openChestButton.SetIcon(chestIconName, 0.08f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_openChestButton.SetGreenColors(true);
			EntityManager.AddTagForEntity(this.m_openChestButton.m_TC.p_entity, "VehicleStats");
			EntityManager.AddTagForEntity(this.m_openChestButton.m_UIsprite.m_TC.p_entity, "VehicleStats");
			UIVerticalList uiverticalList = new UIVerticalList(this.m_openChestButton, "VehicleStats");
			uiverticalList.RemoveDrawHandler();
			UIText uitext = new UIText(uiverticalList, false, "VehicleStats", PsStrings.Get(StringID.OPEN).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, "#ffffff", null);
			UIText uitext2 = new UIText(uiverticalList, false, "VehicleStats", PsStrings.Get(StringID.GACHA_NAME_FREE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.022f, RelativeTo.ScreenHeight, "#055701", null);
		}
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x000D2CB8 File Offset: 0x000D10B8
	private void CreateFreeChestText()
	{
		if (this.m_freeChestArea == null)
		{
			return;
		}
		this.m_freeChestArea.DestroyChildren();
		if ((float)this.m_performanceValue < PsUpgradeManager.GetMaxPerformance(this.m_vehicleType))
		{
			int num = (this.m_performanceValue + 100) / 100 * 100;
			string text = string.Empty;
			string text2 = string.Empty;
			if (!PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && this.m_vehicleType == typeof(OffroadCar) && this.m_performanceValue >= 100 && this.m_performanceValue < 150)
			{
				text = PsStrings.Get(StringID.UNLOCK_BIKE_GARAGE);
				text = text.Replace("%1", "250cc");
				text2 = "item_player_motorcycle_icon";
			}
			else
			{
				text = PsStrings.Get(StringID.INFO_FREE_CHEST);
				text = text.Replace("%1", num.ToString());
				GachaType upgradeChestType = PsGachaManager.GetUpgradeChestType(num);
				text2 = PsGachaManager.GetChestIconName(upgradeChestType);
			}
			UISprite uisprite = new UISprite(this.m_freeChestArea, false, "VehicleStats", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text2, null), true);
			uisprite.SetSize(uisprite.m_frame.width / uisprite.m_frame.height, 1f, RelativeTo.ParentHeight);
			UIComponent uicomponent = new UIComponent(this.m_freeChestArea, false, string.Empty, null, null, string.Empty);
			uicomponent.SetWidth(0.3f, RelativeTo.ScreenHeight);
			uicomponent.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicomponent, false, "VehicleStats", text, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#aaaaff", null);
		}
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x000D2E4C File Offset: 0x000D124C
	public void Upgrade()
	{
		bool flag = this.m_performanceValue < 250;
		int num = (int)((float)(this.m_performanceValue + 100) * 0.01f) * 100;
		this.m_performanceValue += this.m_addValue;
		this.m_addValue = 0;
		bool flag2 = this.m_performanceValue >= num;
		TweenC tween = TweenS.AddTransformTween(this.m_performanceText.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.one * 1.3f, 0.06f, 0f, false);
		TweenS.AddTweenEndEventListener(tween, delegate(TweenC _c)
		{
			if (tween != null)
			{
				TweenS.RemoveComponent(tween);
				tween = null;
			}
			this.m_performanceText.SetColor("#ffffff", "#000000");
			this.m_ccText.SetColor("#ffffff", "#000000");
			TweenS.AddTransformTween(this.m_performanceText.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.one, 0.08f, 0f, true);
		});
		if (this.m_subStats != null)
		{
			for (int i = 0; i < this.m_subStats.Length; i++)
			{
				this.m_subStats[i].Upgrade();
			}
		}
		if (flag2)
		{
			this.DestroyContainerChildren();
			this.CreateContent();
			this.Update();
			if (this.m_openChestButton != null)
			{
				TweenC tween2 = TweenS.AddTransformTween(this.m_openChestButton.m_TC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.one * 1.15f, 0.15f, 0f, false);
				TweenS.AddTweenEndEventListener(tween2, delegate(TweenC _c)
				{
					if (tween2 != null)
					{
						TweenS.RemoveComponent(tween2);
						tween2 = null;
					}
					TweenS.AddTransformTween(this.m_openChestButton.m_TC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.one, 0.1f, 0f, true);
				});
			}
		}
		else if (this.m_vehicleType == typeof(OffroadCar) && flag && this.m_performanceValue >= 150)
		{
			this.CreateFreeChestText();
			if (this.m_freeChestArea != null)
			{
				this.m_freeChestArea.Update();
			}
		}
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x000D3000 File Offset: 0x000D1400
	public void Highlight(int _addValue, int _subStatsIndex)
	{
		this.m_addValue = _addValue;
		this.m_performanceText.SetColor("#66ff66", "#000000");
		this.m_ccText.SetColor("#66ff66", "#000000");
		this.m_performanceText.SetText((this.m_performanceValue + this.m_addValue).ToString());
		this.m_performanceText.m_parent.Update();
		TransformS.SetScale(this.m_performanceText.m_parent.m_TC, 1.1f);
		if (this.m_subStats != null)
		{
			for (int i = 0; i < this.m_subStats.Length; i++)
			{
				this.m_subStats[i].RemoveHighlight();
			}
			this.m_subStats[_subStatsIndex].Highlight(_addValue);
		}
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x000D30D0 File Offset: 0x000D14D0
	public void RemoveHighlight()
	{
		this.m_addValue = 0;
		this.m_performanceText.SetColor("#ffffff", "#000000");
		this.m_ccText.SetColor("#ffffff", "#000000");
		this.m_performanceText.SetText(this.m_performanceValue.ToString());
		this.m_performanceText.m_parent.Update();
		TransformS.SetScale(this.m_performanceText.m_parent.m_TC, 1f);
		if (this.m_subStats != null)
		{
			for (int i = 0; i < this.m_subStats.Length; i++)
			{
				this.m_subStats[i].RemoveHighlight();
			}
		}
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x000D3188 File Offset: 0x000D1588
	public override void Step()
	{
		if (this.m_openChestButton != null && this.m_openChestButton.m_hit)
		{
			int num = this.m_performanceValue / 100 * 100;
			PsMetagameManager.SetLastOpenedUpgradeChest(this.m_vehicleType, num);
			GachaType upgradeChestType = PsGachaManager.GetUpgradeChestType(num);
			PsGachaManager.m_lastOpenedGacha = upgradeChestType;
			PsGachaManager.m_lastGachaRewards = PsGachaManager.OpenGacha(new PsGacha(upgradeChestType), -1, true);
			PsMetrics.ChestOpened("Garage");
			FrbMetrics.ChestOpened("garage");
			this.DestroyContainerChildren();
			this.CreateContent();
			this.Update();
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterOpenGacha), null, null, null, true, true, InitialPage.Center, false, false, false);
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
				PsUICenterGarage psUICenterGarage = this.m_parent.m_parent.m_parent.m_parent as PsUICenterGarage;
				if (psUICenterGarage != null)
				{
					(this.m_parent.m_parent.m_parent.m_parent as PsUICenterGarage).UpdateUpgradeAndCustomisationItems();
				}
			});
		}
		base.Step();
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x000D325B File Offset: 0x000D165B
	private void DestroyContainerChildren()
	{
		this.m_container.DestroyChildren();
		this.m_openChestButton = null;
		this.m_freeChestArea = null;
		this.m_subStats = null;
	}

	// Token: 0x04001768 RID: 5992
	public const string PERFOMANCE_COLOR = "#ffffff";

	// Token: 0x04001769 RID: 5993
	private Type m_vehicleType;

	// Token: 0x0400176A RID: 5994
	private int m_performanceValue;

	// Token: 0x0400176B RID: 5995
	private int m_motorValue;

	// Token: 0x0400176C RID: 5996
	private int m_gripValue;

	// Token: 0x0400176D RID: 5997
	private int m_handlingValue;

	// Token: 0x0400176E RID: 5998
	private int m_powerUpValue;

	// Token: 0x0400176F RID: 5999
	private UIText m_performanceText;

	// Token: 0x04001770 RID: 6000
	private UIText m_ccText;

	// Token: 0x04001771 RID: 6001
	private int m_addValue;

	// Token: 0x04001772 RID: 6002
	private UIVerticalList m_container;

	// Token: 0x04001773 RID: 6003
	private PsUIVehicleStats2.SubStats[] m_subStats;

	// Token: 0x04001774 RID: 6004
	private UIHorizontalList m_freeChestArea;

	// Token: 0x04001775 RID: 6005
	private PsUIAttentionButton m_openChestButton;

	// Token: 0x020002AC RID: 684
	public class SubStats : UICanvas
	{
		// Token: 0x06001495 RID: 5269 RVA: 0x000D3280 File Offset: 0x000D1680
		public SubStats(UIComponent _parent, string _iconName, int _value)
			: base(_parent, false, "VehicleStats", null, string.Empty)
		{
			this.m_value = _value;
			this.SetSize(2f, 1f, RelativeTo.ParentHeight);
			this.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ParentHeight);
			this.RemoveDrawHandler();
			UICanvas uicanvas = new UICanvas(this, false, "VehicleStats", null, string.Empty);
			uicanvas.SetWidth(0.72f, RelativeTo.ParentWidth);
			uicanvas.SetHeight(0.66f, RelativeTo.ParentHeight);
			uicanvas.SetHorizontalAlign(1f);
			uicanvas.SetDrawHandler(new UIDrawDelegate(this.BackgroundDrawHandler));
			uicanvas.SetMargins(0f, 0.15f, 0f, 0f, RelativeTo.ParentHeight);
			this.m_valueText = new UIText(uicanvas, false, "VehicleStats", this.m_value.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.9f, RelativeTo.ParentHeight, "#ffffff", "#000000");
			this.m_valueText.SetHorizontalAlign(1f);
			UISprite uisprite = new UISprite(this, false, "VehicleStats", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_iconName, null), true);
			uisprite.SetSize(uisprite.m_frame.width / uisprite.m_frame.height * 1f, 1f, RelativeTo.ParentHeight);
			uisprite.SetHorizontalAlign(0f);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x000D33E0 File Offset: 0x000D17E0
		private void BackgroundDrawHandler(UIComponent _c)
		{
			PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
			Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.25f * _c.m_actualHeight, 8, Vector2.zero);
			string text = "#000000";
			if (this.m_highlighted)
			{
				text = "#00aa00";
			}
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, DebugDraw.HexToColor(text), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
			PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 1f, roundedRect, DebugDraw.HexToUint(text), DebugDraw.HexToUint(text), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x000D34B0 File Offset: 0x000D18B0
		public void Upgrade()
		{
			if (!this.m_highlighted)
			{
				return;
			}
			this.m_highlighted = false;
			this.m_value += this.m_addValue;
			this.m_addValue = 0;
			this.Update();
			TweenC tween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.one * 1.3f, 0.06f, 0f, false);
			TweenS.AddTweenEndEventListener(tween, delegate(TweenC _c)
			{
				if (tween != null)
				{
					TweenS.RemoveComponent(tween);
					tween = null;
				}
				TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.one, 0.08f, 0f, true);
			});
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000D3544 File Offset: 0x000D1944
		public void Highlight(int _addValue)
		{
			if (this.m_highlighted)
			{
				return;
			}
			this.m_highlighted = true;
			this.m_addValue = _addValue;
			this.m_valueText.SetText((this.m_value + _addValue).ToString());
			this.Update();
			TransformS.SetScale(this.m_TC, 1.1f);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000D35A4 File Offset: 0x000D19A4
		public void RemoveHighlight()
		{
			if (!this.m_highlighted)
			{
				return;
			}
			this.m_highlighted = false;
			this.m_addValue = 0;
			this.m_valueText.SetText(this.m_value.ToString());
			this.Update();
			TransformS.SetScale(this.m_TC, 1f);
		}

		// Token: 0x04001776 RID: 6006
		public bool m_highlighted;

		// Token: 0x04001777 RID: 6007
		private int m_value;

		// Token: 0x04001778 RID: 6008
		private UIText m_valueText;

		// Token: 0x04001779 RID: 6009
		private int m_addValue;
	}
}
