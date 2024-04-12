using System;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class PsUIVehicleStats : UIFittedSprite
{
	// Token: 0x06001488 RID: 5256 RVA: 0x000D2184 File Offset: 0x000D0584
	public PsUIVehicleStats(UIComponent _parent, int _perfValue, int _bar1Value, int _bar2Value, int _bar3Value, int _bar4Value, int _focusPerfAdd, int _focusBar)
		: base(_parent, false, "VehicleStats", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_stats_body", null), true, true)
	{
		PsUIVehicleStats.m_performanceValue = _perfValue;
		PsUIVehicleStats.m_bar1Value = _bar1Value;
		PsUIVehicleStats.m_bar2Value = _bar2Value;
		PsUIVehicleStats.m_bar3Value = _bar3Value;
		PsUIVehicleStats.m_bar4Value = _bar4Value;
		PsUIVehicleStats.m_focusPerformanceAdd = _focusPerfAdd;
		PsUIVehicleStats.m_focusBar = _focusBar;
		this.SetHeight(0.15f, RelativeTo.ScreenHeight);
		this.SetMargins(-1.1f, 0.09f, -0.2f, -0.2f, RelativeTo.OwnHeight);
		this.m_performanceArea = new UIFittedSprite(this, false, "VehicleStats", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_stats_HP_badge", null), true, true);
		this.m_performanceArea.SetAlign(0f, 0.5f);
		this.CreatePerformanceNumbers();
		this.m_barCanvas = new UICanvas(this, false, "VehicleStats", null, string.Empty);
		this.m_barCanvas.SetDrawHandler(new UIDrawDelegate(this.VehicleStatBarBackground));
		this.m_barCanvas.SetWidth(1.3f, RelativeTo.ParentHeight);
		this.m_barCanvas.SetHeight(0.62f, RelativeTo.ParentHeight);
		this.m_barCanvas.SetAlign(1f, 0.515f);
		this.m_barCanvas.SetDepthOffset(20f);
		this.CreateBars();
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x000D22D8 File Offset: 0x000D06D8
	public void UpdateStats(int _perfValue, int _bar1Value, int _bar2Value, int _bar3Value, int _bar4Value, int _focusPerfAdd, int _focusBar)
	{
		PsUIVehicleStats.m_performanceValue = _perfValue;
		PsUIVehicleStats.m_bar1Value = _bar1Value;
		PsUIVehicleStats.m_bar2Value = _bar2Value;
		PsUIVehicleStats.m_bar3Value = _bar3Value;
		PsUIVehicleStats.m_bar4Value = _bar4Value;
		PsUIVehicleStats.m_focusPerformanceAdd = _focusPerfAdd;
		PsUIVehicleStats.m_focusBar = _focusBar;
		this.m_performanceArea.DestroyChildren();
		this.CreatePerformanceNumbers();
		this.m_performanceArea.Update();
		this.m_barCanvas.DestroyChildren();
		this.CreateBars();
		this.m_barCanvas.Update();
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x000D234C File Offset: 0x000D074C
	private void CreatePerformanceNumbers()
	{
		UIVerticalList uiverticalList = new UIVerticalList(this.m_performanceArea, "VehicleStats");
		uiverticalList.SetSpacing(-0.1f, RelativeTo.ParentHeight);
		uiverticalList.SetVerticalAlign(0.6f);
		uiverticalList.RemoveDrawHandler();
		new UIText(uiverticalList, false, "VehicleStats", PsUIVehicleStats.m_performanceValue.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.45f, RelativeTo.ParentHeight, "#6afe00", null);
		new UIText(uiverticalList, false, "VehicleStats", PsStrings.Get(StringID.PERFORMANCE_LABEL), PsFontManager.GetFont(PsFonts.HurmeBold), 0.085f, RelativeTo.ParentHeight, "#6afe00", null);
		TransformS.SetRotation(uiverticalList.m_TC, Vector3.forward * 15f);
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x000D23F8 File Offset: 0x000D07F8
	private void CreateBars()
	{
		UIVerticalList uiverticalList = new UIVerticalList(this.m_barCanvas, "VehicleStats");
		uiverticalList.SetWidth(0.34f, RelativeTo.ParentWidth);
		uiverticalList.SetHorizontalAlign(1f);
		uiverticalList.SetSpacing(0.02f, RelativeTo.OwnHeight);
		uiverticalList.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(uiverticalList, false, "VehicleStats", null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.24f, RelativeTo.ParentHeight);
		uicanvas.SetMargins(-0.25f, -0.25f, 0f, 0f, RelativeTo.ParentWidth);
		uicanvas.RemoveDrawHandler();
		new UIText(uicanvas, false, "VehicleStats", string.Empty + PsUIVehicleStats.m_bar1Value, PsFontManager.GetFont(PsFonts.HurmeBold), 1f, RelativeTo.ParentHeight, "#ffd200", null);
		UICanvas uicanvas2 = new UICanvas(uiverticalList, false, "VehicleStats", null, string.Empty);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetHeight(0.24f, RelativeTo.ParentHeight);
		uicanvas2.RemoveDrawHandler();
		uicanvas2.SetMargins(-0.25f, -0.25f, 0f, 0f, RelativeTo.ParentWidth);
		new UIText(uicanvas2, false, "VehicleStats", string.Empty + PsUIVehicleStats.m_bar2Value, PsFontManager.GetFont(PsFonts.HurmeBold), 1f, RelativeTo.ParentHeight, "#0cdddb", null);
		UICanvas uicanvas3 = new UICanvas(uiverticalList, false, "VehicleStats", null, string.Empty);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetHeight(0.24f, RelativeTo.ParentHeight);
		uicanvas3.RemoveDrawHandler();
		uicanvas3.SetMargins(-0.25f, -0.25f, 0f, 0f, RelativeTo.ParentWidth);
		new UIText(uicanvas3, false, "VehicleStats", string.Empty + PsUIVehicleStats.m_bar3Value, PsFontManager.GetFont(PsFonts.HurmeBold), 1f, RelativeTo.ParentHeight, "#fc539e", null);
		UICanvas uicanvas4 = new UICanvas(uiverticalList, false, "VehicleStats", null, string.Empty);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.SetHeight(0.24f, RelativeTo.ParentHeight);
		uicanvas4.RemoveDrawHandler();
		uicanvas4.SetMargins(-0.25f, -0.25f, 0f, 0f, RelativeTo.ParentWidth);
		new UIText(uicanvas4, false, "VehicleStats", string.Empty + PsUIVehicleStats.m_bar4Value, PsFontManager.GetFont(PsFonts.HurmeBold), 1f, RelativeTo.ParentHeight, "#aadd55", null);
		if (PsUIVehicleStats.m_focusBar == 1)
		{
			new UIText(uicanvas, false, "VehicleStats", "+" + PsUIVehicleStats.m_focusPerformanceAdd, PsFontManager.GetFont(PsFonts.HurmeBold), 1.3f, RelativeTo.ParentHeight, "#6aff00", null).SetHorizontalAlign(1f);
		}
		else if (PsUIVehicleStats.m_focusBar == 2)
		{
			new UIText(uicanvas2, false, "VehicleStats", "+" + PsUIVehicleStats.m_focusPerformanceAdd, PsFontManager.GetFont(PsFonts.HurmeBold), 1.3f, RelativeTo.ParentHeight, "#6aff00", null).SetHorizontalAlign(1f);
		}
		else if (PsUIVehicleStats.m_focusBar == 3)
		{
			new UIText(uicanvas3, false, "VehicleStats", "+" + PsUIVehicleStats.m_focusPerformanceAdd, PsFontManager.GetFont(PsFonts.HurmeBold), 1.3f, RelativeTo.ParentHeight, "#6aff00", null).SetHorizontalAlign(1f);
		}
		else if (PsUIVehicleStats.m_focusBar == 4)
		{
			new UIText(uicanvas4, false, "VehicleStats", "+" + PsUIVehicleStats.m_focusPerformanceAdd, PsFontManager.GetFont(PsFonts.HurmeBold), 1.3f, RelativeTo.ParentHeight, "#6aff00", null).SetHorizontalAlign(1f);
		}
		else if (PsUIVehicleStats.m_focusBar == 0)
		{
		}
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x000D2780 File Offset: 0x000D0B80
	private void VehicleStatBarBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, DebugDraw.HexToUint("#222222"), DebugDraw.HexToUint("#222222"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x0400175F RID: 5983
	private static int m_performanceValue;

	// Token: 0x04001760 RID: 5984
	private static int m_bar1Value;

	// Token: 0x04001761 RID: 5985
	private static int m_bar2Value;

	// Token: 0x04001762 RID: 5986
	private static int m_bar3Value;

	// Token: 0x04001763 RID: 5987
	private static int m_bar4Value;

	// Token: 0x04001764 RID: 5988
	private static int m_focusPerformanceAdd;

	// Token: 0x04001765 RID: 5989
	private static int m_focusBar;

	// Token: 0x04001766 RID: 5990
	private UICanvas m_barCanvas;

	// Token: 0x04001767 RID: 5991
	private UIFittedSprite m_performanceArea;
}
