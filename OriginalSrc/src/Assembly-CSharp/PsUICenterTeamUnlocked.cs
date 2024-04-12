using System;
using UnityEngine;

// Token: 0x02000373 RID: 883
public class PsUICenterTeamUnlocked : PsUIHeaderedCanvas
{
	// Token: 0x06001998 RID: 6552 RVA: 0x00118B68 File Offset: 0x00116F68
	public PsUICenterTeamUnlocked(UIComponent _parent)
		: base(_parent, "TEAMUNLOCKED", false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.75f, RelativeTo.ScreenWidth);
		this.SetHeight(0.75f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0.0175f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.Destroy();
		this.CreateContent(this);
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x00118C18 File Offset: 0x00117018
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x00118C98 File Offset: 0x00117098
	public void CreateContent(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_team_illustration", null), true, true);
		uifittedSprite.SetWidth(0.5f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList2 = new UIVerticalList(uifittedSprite, string.Empty);
		uiverticalList2.SetVerticalAlign(0f);
		uiverticalList2.RemoveDrawHandler();
		uiverticalList2.SetMargins(0f, 0f, 0f, 0.03f, RelativeTo.ScreenHeight);
		UICanvas uicanvas = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.5f, RelativeTo.ScreenWidth);
		uicanvas.SetHeight(0.08f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.TEAM_JOIN_PT1).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, "000000");
		UICanvas uicanvas2 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(0.5f, RelativeTo.ScreenWidth);
		uicanvas2.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TEAM_JOIN_PT2), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, "000000");
		UICanvas uicanvas3 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.1f, RelativeTo.ScreenHeight);
		uicanvas3.SetMargins(0.04f, 0.04f, 0.025f, 0.025f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetDrawHandler(new UIDrawDelegate(this.InfoDrawhandler));
		UIFittedText uifittedText3 = new UIFittedText(uicanvas3, false, string.Empty, PsStrings.Get(StringID.TEAM_JOIN_REWARD_TEXT), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "95e5ff", null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_ok.SetAlign(1f, 1f);
		this.m_ok.SetGreenColors(true);
		this.m_ok.SetText(PsStrings.Get(StringID.TEAM_BUTTON_BROWSE_TEAMS), 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x00118F28 File Offset: 0x00117328
	private void InfoDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Vector2[] line = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.5f), new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f), 0);
		Vector2[] line2 = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f), new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f), 0);
		Color color = DebugDraw.HexToColor("#173851");
		Color color2 = DebugDraw.HexToColor("#86DFFF");
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 5f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, line, 0.005f * (float)Screen.height, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, false);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, line2, 0.005f * (float)Screen.height, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x001190A4 File Offset: 0x001174A4
	public override void Step()
	{
		if (this.m_ok != null && this.m_ok.m_TC.p_entity != null && this.m_ok.m_TC.p_entity.m_active && (this.m_ok.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001C14 RID: 7188
	private PsUIGenericButton m_ok;
}
