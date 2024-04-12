using System;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class PsUIKickHatPopup : PsUIHeaderedCanvas
{
	// Token: 0x060018D8 RID: 6360 RVA: 0x0010ED04 File Offset: 0x0010D104
	public PsUIKickHatPopup(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetHeight(0.75f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_header, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIComponent uicomponent = new UIComponent(uihorizontalList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetHeight(0.06f, RelativeTo.ScreenHeight);
		uicomponent.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, "EJECTION DAY!", PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
	}

	// Token: 0x060018D9 RID: 6361 RVA: 0x0010EE7C File Offset: 0x0010D27C
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x0010EEFC File Offset: 0x0010D2FC
	public void CreateContent(PsCustomisationItem _item)
	{
		this.RemoveTouchAreas();
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(null);
		string text = PsStrings.Get(StringID.KICK_POPUP_TEXT);
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0325f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetWidth(0.88f, RelativeTo.ParentWidth);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.2f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.2f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_item.m_iconName, null), true, true);
		UIVerticalList uiverticalList2 = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList2.SetVerticalAlign(1f);
		uiverticalList2.RemoveDrawHandler();
		UITextbox uitextbox2 = new UITextbox(uiverticalList2, false, string.Empty, PsStrings.Get(_item.m_title), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.039f, RelativeTo.ScreenHeight, true, Align.Left, Align.Top, null, true, null);
		uitextbox2.SetHorizontalAlign(0f);
		uitextbox2.SetWidth(0.4f, RelativeTo.ParentWidth);
		UIText uitext = new UIText(uiverticalList2, false, string.Empty, "<color=#cfdce2>" + PsStrings.Get(StringID.GACHA_OPEN_COMMON_HAT).ToUpper() + "</color>", PsFontManager.GetFont(PsFonts.HurmeBold), 0.034f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(0f);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(this, string.Empty);
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetAlign(0.5f, 0f);
		uihorizontalList2.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_okButton = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_okButton.SetText(PsStrings.Get(StringID.RATE_DIALOG_HEADER_POSITIVE), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_okButton.SetGreenColors(true);
		this.m_okButton.SetVerticalAlign(0f);
		this.m_container.Update();
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x0010F15E File Offset: 0x0010D55E
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x060018DC RID: 6364 RVA: 0x0010F166 File Offset: 0x0010D566
	public override void Step()
	{
		if (this.m_okButton != null && this.m_okButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001B6F RID: 7023
	private PsUIGenericButton m_okButton;
}
