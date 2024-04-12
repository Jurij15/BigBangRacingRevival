using System;

// Token: 0x020002CD RID: 717
public class PsUIVehicleStatsMini : UISprite
{
	// Token: 0x06001534 RID: 5428 RVA: 0x000DBBD4 File Offset: 0x000D9FD4
	public PsUIVehicleStatsMini(UIComponent _parent, Type _vehicleType, int _ccValue, int _powerFuelCc)
		: base(_parent, false, "VehicleStatsMini", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_cc_back", null), true)
	{
		this.m_vehicleType = _vehicleType;
		this.m_performanceValue = _ccValue;
		this.m_powerFuelCc = _powerFuelCc;
		this.SetDepthOffset(-5f);
		this.SetSize(this.m_frame.width / this.m_frame.height * 0.13f, 0.13f, RelativeTo.ScreenHeight);
		this.SetMargins(0f, 0f, 0f, -0.01f, RelativeTo.OwnHeight);
		this.m_container = new UIVerticalList(this, "VehicleStats");
		this.m_container.SetVerticalAlign(0f);
		this.m_container.RemoveDrawHandler();
		this.CreateContent();
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x000DBCA4 File Offset: 0x000DA0A4
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
		UISprite uisprite = new UISprite(this.m_container, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true);
		uisprite.SetSize(uisprite.m_frame.width / uisprite.m_frame.height * 0.2f, 0.2f, RelativeTo.ParentHeight);
		UICanvas uicanvas = new UICanvas(this.m_container, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.52f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		this.performanceList = new UIHorizontalList(uicanvas, string.Empty);
		this.performanceList.SetSpacing(0.1f, RelativeTo.ParentHeight);
		this.performanceList.RemoveDrawHandler();
		this.m_performanceText = new UIText(this.performanceList, false, "VehicleStats", this.m_performanceValue.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.9f, RelativeTo.ParentHeight, "#ffffff", "#000000");
		this.m_ccText = new UIText(this.performanceList, false, "VehicleStats", "cc", PsFontManager.GetFont(PsFonts.HurmeBold), 0.65f, RelativeTo.ParentHeight, "#ffffff", "#000000");
		this.m_ccText.SetVerticalAlign(0.15f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_container, string.Empty);
		uihorizontalList.SetHeight(0.36f, RelativeTo.ParentHeight);
		uihorizontalList.SetMargins(0f, 0f, 0.06f, 0.06f, RelativeTo.OwnHeight);
		uihorizontalList.SetSpacing(0.2f, RelativeTo.OwnHeight);
		uihorizontalList.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_skullrider_gas_0", null), true, true);
		this.m_powerFuelCcText = new UIFittedText(uihorizontalList, false, string.Empty, "+" + this.m_powerFuelCc, PsFontManager.GetFont(PsFonts.HurmeBold), true, "#C2FF16", null);
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x000DBEB0 File Offset: 0x000DA2B0
	public void SetPowerFuelCc(int _cc)
	{
		this.m_powerFuelCc = _cc;
		if (this.m_powerFuelCcText != null)
		{
			this.m_powerFuelCcText.SetText("+" + this.m_powerFuelCc);
			this.m_powerFuelCcText.m_parent.Update();
		}
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x000DBEFF File Offset: 0x000DA2FF
	public void SetCc(int _cc)
	{
		this.m_performanceValue = _cc;
		if (this.m_performanceText != null)
		{
			this.m_performanceText.SetText(this.m_performanceValue.ToString());
			this.performanceList.Update();
		}
	}

	// Token: 0x040017D8 RID: 6104
	public const string PERFOMANCE_COLOR = "#ffffff";

	// Token: 0x040017D9 RID: 6105
	private Type m_vehicleType;

	// Token: 0x040017DA RID: 6106
	private int m_performanceValue;

	// Token: 0x040017DB RID: 6107
	private int m_powerFuelCc;

	// Token: 0x040017DC RID: 6108
	private UIText m_performanceText;

	// Token: 0x040017DD RID: 6109
	private UIText m_ccText;

	// Token: 0x040017DE RID: 6110
	private UIFittedText m_powerFuelCcText;

	// Token: 0x040017DF RID: 6111
	private UIVerticalList m_container;

	// Token: 0x040017E0 RID: 6112
	private UIHorizontalList performanceList;
}
