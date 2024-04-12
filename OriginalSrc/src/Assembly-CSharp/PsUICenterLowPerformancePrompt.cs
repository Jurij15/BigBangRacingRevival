using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class PsUICenterLowPerformancePrompt : PsUIHeaderedCanvas
{
	// Token: 0x060018A1 RID: 6305 RVA: 0x0010BCE0 File Offset: 0x0010A0E0
	public PsUICenterLowPerformancePrompt(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetHeight(0.65f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x0010BDDC File Offset: 0x0010A1DC
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.PERFORMANCE_DIALOG_TITLE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x0010BE58 File Offset: 0x0010A258
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x0010BED8 File Offset: 0x0010A2D8
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		string text = PsStrings.Get(StringID.PERFORMANCE_DIALOG_TEXT);
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		uitextbox.SetMargins(0.05f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_continue = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continue.SetAlign(0f, 1f);
		this.m_continue.SetText(PsStrings.Get(StringID.YES_OKAY_DO_IT), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_continue.SetGreenColors(true);
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x0010BFD0 File Offset: 0x0010A3D0
	public override void Step()
	{
		if (this.m_continue.m_hit)
		{
			PlayerPrefsX.SetLowEndPrompt(true);
			PsState.m_perfMode = true;
			PlayerPrefsX.SetPerfMode(PsState.m_perfMode);
			Main.SetPerfMode(PsState.m_perfMode);
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001B45 RID: 6981
	private PsUIGenericButton m_continue;
}
