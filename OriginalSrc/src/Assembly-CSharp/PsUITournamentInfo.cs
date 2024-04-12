using System;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class PsUITournamentInfo : PsUIHeaderedCanvas
{
	// Token: 0x060015F7 RID: 5623 RVA: 0x000E6094 File Offset: 0x000E4494
	public PsUITournamentInfo(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.1f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetHeight(0.75f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.45f);
		this.SetMargins(0.0125f, 0.0125f, 0.0125f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(this, string.Empty);
		uiscrollableCanvas.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(uiscrollableCanvas, string.Empty);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetAlign(0.5f, 0.5f);
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0.04f, RelativeTo.ScreenHeight);
		int num = -1;
		string text = string.Empty;
		if (PsState.m_activeGameLoop != null)
		{
			num = (int)(PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.cc;
			text = (PsState.m_activeGameLoop as PsGameLoopTournament).m_minigameMetaData.playerUnit;
		}
		else if (PsMetagameManager.m_activeTournament != null)
		{
			num = (int)PsMetagameManager.m_activeTournament.tournament.cc;
			text = PsMetagameManager.m_activeTournament.tournament.playerUnit;
		}
		string text2 = text.ToLower();
		if (text2 != null)
		{
			if (text2 == "offroadcar")
			{
				text = PsStrings.Get(StringID.EDITOR_GUI_VEHICLE_NAME_CAR);
				goto IL_199;
			}
			if (text2 == "motorcycle")
			{
				text = PsStrings.Get(StringID.EDITOR_GUI_VEHICLE_NAME_BIKE);
				goto IL_199;
			}
		}
		text = string.Empty;
		IL_199:
		UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList2.SetHorizontalAlign(0f);
		uiverticalList2.SetMargins(0.065f, 0.005f, 0.005f, 0.005f, RelativeTo.ScreenWidth);
		uiverticalList2.SetDrawHandler(new UIDrawDelegate(this.BlueBackgroundDrawhandler));
		UICanvas uicanvas = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas.SetRogue();
		uicanvas.SetMargins(-1f, 1.18f, 0f, 0f, RelativeTo.OwnWidth);
		uicanvas.SetAlign(0f, 1f);
		uicanvas.SetSize(0.06f, 0.06f, RelativeTo.ScreenWidth);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_CC", null), true, true);
		uifittedSprite.SetAlign(0f, 1f);
		string text3 = PsStrings.Get(StringID.TOURNAMENT_INFO_VEHICLE);
		if (num != -1)
		{
			text3 = text3.Replace("%1", num + "cc " + text);
		}
		else
		{
			text3 = text3.Replace("%1", text);
		}
		UITextbox uitextbox = new UITextbox(uiverticalList2, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		UIVerticalList uiverticalList3 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList3.SetHorizontalAlign(0f);
		uiverticalList3.SetMargins(0.065f, 0.005f, 0.005f, 0.005f, RelativeTo.ScreenWidth);
		uiverticalList3.SetDrawHandler(new UIDrawDelegate(this.BlueBackgroundDrawhandler));
		UICanvas uicanvas2 = new UICanvas(uiverticalList3, false, string.Empty, null, string.Empty);
		uicanvas2.SetRogue();
		uicanvas2.SetMargins(-1f, 1.18f, 0f, 0f, RelativeTo.OwnWidth);
		uicanvas2.SetAlign(0f, 1f);
		uicanvas2.SetSize(0.06f, 0.06f, RelativeTo.ScreenWidth);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_teams", null), true, true);
		uifittedSprite2.SetAlign(0f, 1f);
		text3 = PsStrings.Get(StringID.TOURNAMENT_INFO_ROOMS);
		UITextbox uitextbox2 = new UITextbox(uiverticalList3, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		UIVerticalList uiverticalList4 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList4.SetHorizontalAlign(0f);
		uiverticalList4.SetMargins(0.065f, 0.005f, 0.005f, 0.005f, RelativeTo.ScreenWidth);
		uiverticalList4.SetDrawHandler(new UIDrawDelegate(this.BlueBackgroundDrawhandler));
		UICanvas uicanvas3 = new UICanvas(uiverticalList4, false, string.Empty, null, string.Empty);
		uicanvas3.SetRogue();
		uicanvas3.SetMargins(-1f, 1.18f, 0f, 0f, RelativeTo.OwnWidth);
		uicanvas3.SetAlign(0f, 1f);
		uicanvas3.SetSize(0.06f, 0.06f, RelativeTo.ScreenWidth);
		uicanvas3.RemoveDrawHandler();
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_tournament_boost", null), true, true);
		uifittedSprite3.SetAlign(0f, 1f);
		text3 = PsStrings.Get(StringID.TOURNAMENT_INFO_NITROS) + PsStrings.Get(StringID.TOURNAMENT_INFO_NITROS_UNUSED);
		UITextbox uitextbox3 = new UITextbox(uiverticalList4, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		UIVerticalList uiverticalList5 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList5.SetHorizontalAlign(0f);
		uiverticalList5.SetMargins(0.065f, 0.005f, 0.005f, 0.005f, RelativeTo.ScreenWidth);
		uiverticalList5.SetDrawHandler(new UIDrawDelegate(this.BlueBackgroundDrawhandler));
		UICanvas uicanvas4 = new UICanvas(uiverticalList5, false, string.Empty, null, string.Empty);
		uicanvas4.SetRogue();
		uicanvas4.SetMargins(-1f, 1.18f, 0f, 0f, RelativeTo.OwnWidth);
		uicanvas4.SetAlign(0f, 1f);
		uicanvas4.SetSize(0.06f, 0.06f, RelativeTo.ScreenWidth);
		uicanvas4.RemoveDrawHandler();
		UIFittedSprite uifittedSprite4 = new UIFittedSprite(uicanvas4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_chest_T2", null), true, true);
		uifittedSprite4.SetAlign(0f, 1f);
		text3 = PsStrings.Get(StringID.TOURNAMENT_INFO_REWARDS) + PsStrings.Get(StringID.TOURNAMENT_INFO_HELMET);
		UITextbox uitextbox4 = new UITextbox(uiverticalList5, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0f, 0f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x060015F8 RID: 5624 RVA: 0x000E673C File Offset: 0x000E4B3C
	public void BlueBackgroundDrawhandler(UIComponent _c)
	{
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, (float)Screen.height * 0.018f, 5, Vector3.zero);
		uint num = DebugDraw.HexToUint("#36556F");
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, roundedRect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x000E67A8 File Offset: 0x000E4BA8
	public void CreateHeaderContent(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(this.m_header, false, string.Empty, null, string.Empty);
		uicanvas.SetAlign(0f, 0.5f);
		uicanvas.SetMargins(0f, 0f, -0.35f, -0.35f, RelativeTo.OwnHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_logo", null), true, true);
		uifittedSprite.SetDepthOffset(-30f);
		uifittedSprite.SetAlign(0f, 1f);
		UICanvas uicanvas2 = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas2.RemoveDrawHandler();
		uicanvas2.SetMargins(2f, 2f, 0.2f, 0.2f, RelativeTo.ParentHeight);
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TOUR_TOURNAMENT_INFO), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
	}
}
