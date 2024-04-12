using System;
using UnityEngine;

// Token: 0x020002BE RID: 702
public class PsUICenterOffensive : PsUIHeaderedCanvas
{
	// Token: 0x060014CE RID: 5326 RVA: 0x000D92FC File Offset: 0x000D76FC
	public PsUICenterOffensive(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.SetHeight(0.5f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.1f, 0.1f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x000D93F8 File Offset: 0x000D77F8
	public override void CreateCloseButton()
	{
		UICanvas uicanvas = new UICanvas(this.m_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.125f, 0.125f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(1f, 1f);
		uicanvas.SetMargins(0.45f, -0.45f, -0.45f, 0.45f, RelativeTo.OwnHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetDepthOffset(-20f);
		this.m_exitButton = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetOrangeColors(true);
		this.m_exitButton.SetSound("/UI/ButtonBack");
		this.m_exitButton.SetIcon("menu_icon_close", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x000D94CC File Offset: 0x000D78CC
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, PsStrings.Get(StringID.REPORT_ABUSE).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x000D9504 File Offset: 0x000D7904
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x000D9584 File Offset: 0x000D7984
	public void CreateContent(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0f, 0f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		_parent.RemoveTouchAreas();
		string text = PsStrings.Get(StringID.REPORT_TEXT);
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetMargins(0.05f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_continue = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continue.SetAlign(1f, 1f);
		this.m_continue.SetMargins(0.02f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_continue.SetRedColors();
		this.m_continue.SetText(PsStrings.Get(StringID.BUTTON_REPORT).ToUpper(), 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x000D96CB File Offset: 0x000D7ACB
	public override void Step()
	{
		if (this.m_continue.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		base.Step();
	}

	// Token: 0x0400178E RID: 6030
	private PsUIGenericButton m_continue;
}
