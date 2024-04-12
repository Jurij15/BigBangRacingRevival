using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class PsUICenterAchievements : PsUIHeaderedCanvas
{
	// Token: 0x060010F8 RID: 4344 RVA: 0x000A2CB4 File Offset: 0x000A10B4
	public PsUICenterAchievements(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.8f, RelativeTo.ScreenWidth);
		this.SetHeight(0.75f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x000A2D98 File Offset: 0x000A1198
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, "Achievements", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
		UICanvas uicanvas = new UICanvas(uitext, false, string.Empty, null, string.Empty);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetMargins(-1.5f, 0f, -0.125f, -0.125f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_settings", null);
		UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true);
		uisprite.SetSize(1f, 1f * (frame.height / frame.width), RelativeTo.ParentHeight);
		uisprite.SetHorizontalAlign(0f);
		uisprite.SetColor(DebugDraw.HexToColor("#95e5ff") * Color.gray);
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x000A2EC0 File Offset: 0x000A12C0
	public void CreateContent(UIComponent _parent)
	{
		UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(_parent, string.Empty);
		UIVerticalList uiverticalList = new UIVerticalList(uiscrollableCanvas, string.Empty);
		uiverticalList.SetWidth(0.85f, RelativeTo.ParentWidth);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetSpacing(0.035f, RelativeTo.ScreenHeight);
		List<Achievement> all = PsAchievementManager.All;
		for (int i = 0; i < all.Count; i++)
		{
			UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas.RemoveDrawHandler();
			uicanvas.SetWidth(0.5f, RelativeTo.ParentWidth);
			uicanvas.SetHeight(0.075f, RelativeTo.ScreenHeight);
			UIText uitext = new UIText(uicanvas, false, string.Empty, all[i].humanReadableName, "KGSecondChances", 0.03f, RelativeTo.ScreenHeight, null, null);
			uitext.SetAlign(0f, 1f);
			UIText uitext2 = new UIText(uicanvas, false, string.Empty, all[i].percentCompleted.ToString() + "%", "KGSecondChances", 0.03f, RelativeTo.ScreenHeight, null, null);
			uitext2.SetAlign(1f, 1f);
			UIText uitext3 = new UIText(uicanvas, false, string.Empty, all[i].humanReadableDescription, PsFontManager.GetFont(PsFonts.HurmeRegular), 0.02f, RelativeTo.ScreenHeight, null, null);
			uitext3.SetAlign(0f, 0f);
		}
	}

	// Token: 0x040013EC RID: 5100
	private const string m_font = "KGSecondChances";

	// Token: 0x040013ED RID: 5101
	private const float m_smallTextSize = 0.03f;

	// Token: 0x040013EE RID: 5102
	private const string m_textColor = "#a8e2ff";
}
